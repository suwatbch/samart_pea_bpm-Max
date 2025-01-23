using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using Timer = System.Windows.Forms.Timer;
using ArchitectureSG;
using PEA.BPM.Architecture.ArchitectureSG;

namespace PEA.BPM.Architecture.ArchitectureTool
{
    public class ServerConnectionMonitor
    {
        private const string compatible_version = "2.0.7";

        #region Singleton
        private static ServerConnectionMonitor _instant = new ServerConnectionMonitor();
        private ServerConnectionMonitor()
        {
        }
        public static ServerConnectionMonitor Instant
        {
            get
            {
                return _instant;
            }
        }
        #endregion

        private int _pingCounter = 0;
        private int _reqCounter = 0;
        private bool _doneValidateVersion = false;
        private Timer _monitorTimer;

        public void Start()
        {
            _monitorTimer = new Timer();
            _monitorTimer.Interval = 10;
            _monitorTimer.Tick += _monitorTimer_Tick;
            _monitorTimer.Enabled = true;
        }

        private bool GetTimeAndCheckIsNetworkOnline()
        {
            ICommonService commonservice = GetCommonService();

            int retrycount = 0;
        getservertimeandcheckonline:
            try
            {
                //-- อ่านค่าเวลาจาก server มาใช้ในโปรแกรม ในขณะเดียวกันก็จับเวลาดู response เอามาเป็นค่า latency ของ network แบบคร่าวๆ
                Stopwatch sw = Stopwatch.StartNew();
                DateTime dt = commonservice.GetServerTime();
                sw.Stop();

                Session.NetworkLatency = (int)sw.ElapsedMilliseconds;
                Session.GetServerTimeRetryCount = retrycount;
                Session.BpmDateTime.Now = dt; // set เวลาจาก server มาใส่ไว้ใน session เอาไว้ให้ code ส่วนอื่นๆ ใช้งาน (ถ้าต้องการเวลาให้เอาจาก session ห้ามเอาจาก DateTime.Now)
            }
            catch
            {
                retrycount++;
                if (retrycount < 5)
                {
                    Thread.Sleep(1000);
                    goto getservertimeandcheckonline;
                }

                _pingCounter = 0;  //reset the counter
                _reqCounter = 0; // ใช้กับ CheckDoubleLogin, clear ทิ้งตอน offline
                //Session.IsNetworkConnectionAvailable = false;
                Session.IsNetworkConnectionAvailable = false;
                return false;
            }
            return true;
        }

        private void CheckDoubleLogin()
        {
            if (Session.User.Id == null) return; // ยังไม่มี userid ไม่ต้อง check
           
            string localIP = MachineInfo.GetLocalIP();

            ISecurityService securityservice = GetSecurityService();
            string checklogindouble = securityservice.CheckLogInDouble(Session.User.Id, localIP, Session.NetworkLatency, Session.GetServerTimeRetryCount);
            switch (checklogindouble)
            {
                case "STAMP_IP":
                    break;
                case "SAME_IP_No_REQ":
                    break;
                case "SAME_IP_REQ":
                    MessageBox.Show("มีการ LogIn ด้วยรหัสผู้ใช้(UserID : " + Session.User.Id + ")เดียวกัน. กดปุ่ม Ok เพื่อปิดการทำงาน.", "BPM System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //[== 2BDone : Call another web service to update ReqFlag = 2 to told M2 that M1 is already exist ==]
                    string IPReqFlag_SAME_IP_REQ = securityservice.UpdateCurIPReqFlag(Session.User.Id, "", "2");
                    Environment.Exit(0);
                    break;
                case "DIF_IP_REQ":
                    DialogResult result;
                    result = MessageBox.Show("มีการ LogIn ด้วยรหัสผู้ใช้(UserID : " + Session.User.Id + ")อยู่ก่อนแล้ว \nหรือ\nผู้ใช้เข้าระบบด้วยเครื่องที่แตกต่างจากการ LogIn ครั้งก่อน \nคุณยังต้องการ LogIn เข้าสู่ระบบต่อหรือไม่\n(ผู้ใช้ระบบด้วยรหัสผู้ใช้นี้ก่อนหน้าจะถูก Log Off จากระบบ)", "BPM System", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        //[== Call Web service to update ReqFlag = 1 : To let M1 know that M2 request M1 to Logout ==]
                        string IPReqFlag_DIF_IP_REQ = securityservice.UpdateCurIPReqFlag(Session.User.Id, "", "1");
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    break;
                case "DIF_IP_WAIT":
                    _reqCounter++;
                    if (_reqCounter >= 2)
                    {
                        //[== Wait for M1 to logout for 4 time but M1 still not logout yet : assume M1 is hang ==]
                        //[== Call web service to take owner Current IP and ReqFlag = 0 ==]
                        string IPReqFlag_DIF_IP_WAIT = securityservice.UpdateCurIPReqFlag(Session.User.Id, localIP, "0");
                        _reqCounter = 0;
                    }
                    break;
                case "DIF_IP_M1_EXIST":
                    break;
                default:
                    break;
            }
        }

        private void ValidateBPMVersion()
        {
            //available connection so validate bpm version
            //bpm might start from offline and then online so this step has to be put in ping
            ICommonService commonservice = GetCommonService();
            BPMVersion ver = commonservice.GetVersion();
            if (Session.Application.Version != ver.Version && compatible_version != ver.Version)
            {
                MessageBox.Show("เวอร์ชันของ BPM Client ไม่ตรงกับ BPM Server ที่ให้บริการอยู่\nกรุณาติดตั้งเวอร์ชันใหม่ล่าสุดหรือติดต่อ BPM Support\n\n", "Invalid version", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Session.Work.OnCloseNotify = false; //not to notify
                Application.Exit();
            }

            if (Session.Branch.OnlineConnection)
                Session.Server.ConnectionInfo = Session.Server.Address.Center + (string.IsNullOrEmpty(ver.POSDatabase) ? "" : " -> " + ver.POSDatabase);
            else
                Session.Server.ConnectionInfo = Session.Server.Address.Branch;
        }

        private void _monitorTimer_Tick(object sender, EventArgs e)
        {
            _monitorTimer.Interval = 30000;

            _monitorTimer.Enabled = false; // หยุด timer ไว้ก่อน
            try
            {
                //checking avaiability must be started after logging and registered 
                if (Session.IsNetworkConnectionAvailable && Session.User.Id != null && Session.Branch.Id != null) // ถ้า online อยู่
                {
                    //-- อ่านเวลาจาก server + check ดูว่า online อยู่ไหม                    
                    if (!GetTimeAndCheckIsNetworkOnline()) return; // ถ้า offline ก็ไม่ต้องทำอะไรต่อแล้ว

                    if (!_doneValidateVersion) // check BPM version, check ครั้งแรกครั้งเดียว
                    {
                        ValidateBPMVersion();
                        _doneValidateVersion = true;
                    }

                    try
                    {
                        CheckDoubleLogin();
                    }
                    catch
                    {
                        //-- suppress error, do nothing
                    }
                }
                else // ถ้าระบบ offline
                {
                    _pingCounter++;

                    if (_pingCounter >= 4) // ครบ 4 รอบ = ประมาณ 2 minutes
                    {
                        _pingCounter = 0;
                        //-- Check ดูอีกทีว่า online แล้วยัง
                        if (!GetTimeAndCheckIsNetworkOnline()) return; // ถ้า offline ก็ไม่ต้องทำอะไรต่อแล้ว  
                        //Session.IsNetworkConnectionAvailable = true;
                        if (Session.IsForcedOffline == true) return; // ถ้า Forced offline ก็ไม่ต้องทำอะไรต่อแล้ว    //Offline by User
                        Session.IsNetworkConnectionAvailable = true;
                        
                    }
                }
            }
            finally
            {
                _monitorTimer.Enabled = true; // เริ่ม timer อีกที
            }

        }


        #region Service Factory
        public ICommonService GetCommonService()
        {
            return CommonSG.Instance(Session.Branch.OnlineConnection);
        }

        public ISecurityService GetSecurityService()
        {
            return SecuritySG.Instance(Session.Branch.OnlineConnection);
        }
        #endregion
    }
}
