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
                //-- ��ҹ������Ҩҡ server ���������� 㹢�����ǡѹ��Ѻ���Ҵ� response ������繤�� latency �ͧ network Ẻ������
                Stopwatch sw = Stopwatch.StartNew();
                DateTime dt = commonservice.GetServerTime();
                sw.Stop();

                Session.NetworkLatency = (int)sw.ElapsedMilliseconds;
                Session.GetServerTimeRetryCount = retrycount;
                Session.BpmDateTime.Now = dt; // set ���Ҩҡ server ��������� session ��������� code ��ǹ���� ��ҹ (��ҵ�ͧ������������Ҩҡ session ������Ҩҡ DateTime.Now)
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
                _reqCounter = 0; // ��Ѻ CheckDoubleLogin, clear ��駵͹ offline
                //Session.IsNetworkConnectionAvailable = false;
                Session.IsNetworkConnectionAvailable = false;
                return false;
            }
            return true;
        }

        private void CheckDoubleLogin()
        {
            if (Session.User.Id == null) return; // �ѧ����� userid ����ͧ check
           
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
                    MessageBox.Show("�ա�� LogIn �������ʼ����(UserID : " + Session.User.Id + ")���ǡѹ. ������ Ok ���ͻԴ��÷ӧҹ.", "BPM System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //[== 2BDone : Call another web service to update ReqFlag = 2 to told M2 that M1 is already exist ==]
                    string IPReqFlag_SAME_IP_REQ = securityservice.UpdateCurIPReqFlag(Session.User.Id, "", "2");
                    Environment.Exit(0);
                    break;
                case "DIF_IP_REQ":
                    DialogResult result;
                    result = MessageBox.Show("�ա�� LogIn �������ʼ����(UserID : " + Session.User.Id + ")�����͹���� \n����\n���������к���������ͧ���ᵡ��ҧ�ҡ��� LogIn ���駡�͹ \n�س�ѧ��ͧ��� LogIn �������к�����������\n(������к��������ʼ�������͹˹�Ҩж١ Log Off �ҡ�к�)", "BPM System", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                MessageBox.Show("�����ѹ�ͧ BPM Client ���ç�Ѻ BPM Server �������ԡ������\n��سҵԴ��������ѹ��������ش���͵Դ��� BPM Support\n\n", "Invalid version", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

            _monitorTimer.Enabled = false; // ��ش timer ����͹
            try
            {
                //checking avaiability must be started after logging and registered 
                if (Session.IsNetworkConnectionAvailable && Session.User.Id != null && Session.Branch.Id != null) // ��� online ����
                {
                    //-- ��ҹ���Ҩҡ server + check ����� online �������                    
                    if (!GetTimeAndCheckIsNetworkOnline()) return; // ��� offline ������ͧ�����õ������

                    if (!_doneValidateVersion) // check BPM version, check �����á��������
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
                else // ����к� offline
                {
                    _pingCounter++;

                    if (_pingCounter >= 4) // �ú 4 �ͺ = ����ҳ 2 minutes
                    {
                        _pingCounter = 0;
                        //-- Check ���ա����� online �����ѧ
                        if (!GetTimeAndCheckIsNetworkOnline()) return; // ��� offline ������ͧ�����õ������  
                        //Session.IsNetworkConnectionAvailable = true;
                        if (Session.IsForcedOffline == true) return; // ��� Forced offline ������ͧ�����õ������    //Offline by User
                        Session.IsNetworkConnectionAvailable = true;
                        
                    }
                }
            }
            finally
            {
                _monitorTimer.Enabled = true; // ����� timer �ա��
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
