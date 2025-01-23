using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AdministrativeTool.ConnectionSetting;
using AdministrativeTool.OpenOfflineFile;
using AdministrativeTool.CloseAccountStatus;
using AdministrativeTool.OfflineErrorLog;
using AdministrativeTool.Consolidate;
using AdministrativeTool.UnclarifyPayment;
using AdministrativeTool.NewsBroadcastSender;
using AdministrativeTool.OutOfShiftList;
using System.Reflection;
using System.Diagnostics;
using AdministrativeTool.CloseAccountStatusWS;
using AdministrativeTool.OutOfShiftWS;
using ctlDiffuseDlgLib;
using System.Collections;

namespace AdministrativeTool
{
    public partial class MainUI : Form
    {
        #region Variables
        private ConnectionSettingMain connectionSettingMain;
        private OpenOfflineFileMain openOfflineFileMain;
        private CloseAccountStatusMain closeAccountStatusMain;
        private OfflineErrorLogMain offlineErrorLogMain;
        private ConsolidateMain consolidateMain;
        private SenderUserControl newsBroadcastMain;
        private UnclarifyPaymentView unclarifyPaymentView;
        private OutOfShiftMain outOfShiftMain;

        private static CloseAccountStatusWebService closeAccountStatusWS = new CloseAccountStatusWebService();
        private static OutOfShiftWebService outOfShiftWS = new OutOfShiftWebService();
        private static Queue queueDlg;
        private static bool Accepted_allClosedBalanceStatus = false;
        private static int tenClosedBalanceNum = 0;

        private static DateTime lastOutOfShiftDateTime;
        #endregion

        #region Constructor
        public MainUI()
        {
            InitializeComponent();
            string major = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
            string minor = Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            string build = Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
            string title = string.Format("BPM Monitor v{0}.{1}.{2}", major, minor, build);
            this.Text = title;
            notifyIcon1.Text = title;
            notifyIcon1.Tag = title;

            connectionSettingMain = new ConnectionSettingMain();
            openOfflineFileMain = new OpenOfflineFileMain();
            closeAccountStatusMain = new CloseAccountStatusMain();
            offlineErrorLogMain = new OfflineErrorLogMain();
            consolidateMain = new ConsolidateMain();
            newsBroadcastMain = new SenderUserControl();
            unclarifyPaymentView = new UnclarifyPaymentView();
            unclarifyPaymentView = new UnclarifyPaymentView();
            outOfShiftMain = new OutOfShiftMain();

            connectionSettingMain.Hide();
            openOfflineFileMain.Hide();
            closeAccountStatusMain.Hide();
            offlineErrorLogMain.Hide();
            consolidateMain.Hide();
            newsBroadcastMain.Hide();
            outOfShiftMain.Hide();

            this.Controls["mainpanel"].Controls.Add(connectionSettingMain);
            this.Controls["mainpanel"].Controls.Add(openOfflineFileMain);
            this.Controls["mainpanel"].Controls.Add(closeAccountStatusMain);
            this.Controls["mainpanel"].Controls.Add(offlineErrorLogMain);
            this.Controls["mainpanel"].Controls.Add(consolidateMain);
            this.Controls["mainpanel"].Controls.Add(outOfShiftMain);

            queueDlg = new Queue();


            //DateTime tempDt = outOfShiftWS.GetDBDateTime();
            //lastOutOfShiftDateTime = new DateTime(tempDt.Year, tempDt.Month, tempDt.Day, 0, 0, 0);
            ////lastOutOfShiftDateTime = new DateTime(2010, 8, 2, 0, 0, 0);

        }
        #endregion

        #region Form Events
        private void MainUI_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime tempDt = outOfShiftWS.GetDBDateTime();
                lastOutOfShiftDateTime = new DateTime(tempDt.Year, tempDt.Month, tempDt.Day, 0, 0, 0);
            }
            catch (Exception ex) {
                MessageBox.Show(" โปรแกรมทำงานไม่ถูกต้อง\n อาจมีปัญหาที่การเชื่อมต่อ กรุณาตรวจสอบ app.config และเปิดโปรแกรมใหม่!\n\n Error Message:\n"+ex.Message, "ปัญหา", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            startTimer1_closeBalanceForAll(30);
            startTimer2_OutOfShift(35);
            startTimer_Show();

            //string strLine = string.Format("ปิดบัญชี ครบทั้งหมด \nจำนวน {0} สาขา เรียบร้อยแล้ว", 20);
            //ctlDiffuseDlg dlg = new ctlDiffuseDlg();
            //dlg.Link1Clicked += Dlg_LinkClicked;
            //queueDlg.Enqueue(new DiffuseDlgParam(DiffuseDlgParam.AlertType.Accepted_allClosedBalance, dlg, new string[] { "ประกาศ", strLine, "<<Accepted>>" }));
            //queueDlg.Enqueue(new DiffuseDlgParam(DiffuseDlgParam.AlertType.Accepted_allClosedBalance, dlg, new string[] { "ประกาศ", strLine, "<<Accepted>>" }));
            //queueDlg.Enqueue(new DiffuseDlgParam(DiffuseDlgParam.AlertType.Accepted_allClosedBalance, dlg, new string[] { "ประกาศ", strLine, "<<Accepted>>" }));

        }
        #endregion

        #region ToolStripButton Events
        private void toolStripButtonConnectionSetting_Click(object sender, EventArgs e)
        {
            connectionSettingMain.Show();
            connectionSettingMain.BringToFront();
            connectionSettingMain.Dock = DockStyle.Fill;
        }

        private void toolStripButtonOpenOfflineFile_Click(object sender, EventArgs e)
        {
            openOfflineFileMain.Show();
            openOfflineFileMain.BringToFront();
            openOfflineFileMain.Dock = DockStyle.Fill;
        }

        private void toolStripButtonCloseAccountStatus_Click(object sender, EventArgs e)
        {
            closeAccountStatusMain.Show();
            closeAccountStatusMain.BringToFront();
            closeAccountStatusMain.Dock = DockStyle.Fill;
        }

        private void toolStripButtonOfflineErrorLog_Click(object sender, EventArgs e)
        {
            offlineErrorLogMain.Show();
            offlineErrorLogMain.BringToFront();
            offlineErrorLogMain.Dock = DockStyle.Fill;
        }

        private void toolStripButtonConsolidate_Click(object sender, EventArgs e)
        {
            consolidateMain.Show();
            consolidateMain.BringToFront();
            consolidateMain.Dock = DockStyle.Fill;
        }

        private void toolStripButtonNewsBroadcast_Click(object sender, EventArgs e)
        {

            if (newsBroadcastMain.Created)
            {
                newsBroadcastMain.BringToFront();
            }
            else
            {
                this.Controls["mainpanel"].Controls.Add(newsBroadcastMain);
                //PermissionInfo permissionInfo = new PermissionInfo(false);
                //PermissionDialog permissionDlg = new PermissionDialog(permissionInfo);
                //permissionDlg.ShowDialog();
                //if (!permissionDlg.PermissionInfo.IsCorrect) return;
                newsBroadcastMain.Show();
                newsBroadcastMain.BringToFront();
                newsBroadcastMain.Dock = DockStyle.Fill;
            }
        }


        private void UnclarifyPayment_Click(object sender, EventArgs e)
        {
            this.Controls["mainpanel"].Controls.Add(unclarifyPaymentView);
            unclarifyPaymentView.Show();
            unclarifyPaymentView.BringToFront();
            unclarifyPaymentView.Dock = DockStyle.Fill;
        }


        #endregion

        #region New

        void sendToTray(Object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                toTray();
        }

        void toTray()
        {
            Hide();

            notifyIcon1.BalloonTipTitle = "Hidden mode";
            notifyIcon1.BalloonTipText =  "BPM Monitor has been minimized to the taskbar.";
            notifyIcon1.ShowBalloonTip(50);
        }

        private void MainUI_Resize(object sender, EventArgs e)
        {
            this.Deactivate += new EventHandler(this.sendToTray);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Maximized;
            }
        }

        private void startTimer_Show()
        {
            Timer tmr = new Timer();
            tmr.Interval = 11 * 1000;
            tmr.Start();
            tmr.Tick += new EventHandler(Tick_Show);
        }

        static void Tick_Show(object sender, EventArgs e)
        {
            if (queueDlg.Count > 0)
            {
                DiffuseDlgParam dlgParamShow = (DiffuseDlgParam)queueDlg.Dequeue();
                switch (dlgParamShow.alertType)
                { 
                    case DiffuseDlgParam.AlertType.Normal :
                        dlgParamShow.Show(10);
                        break;
                    case DiffuseDlgParam.AlertType.Accepted_allClosedBalance :
                        if (!Accepted_allClosedBalanceStatus)
                            dlgParamShow.Show(10);
                        break;
                    default :
                        break;
                }

            }
        }

        private void startTimer2_OutOfShift(int interval_sec)
        {
            Timer tmr = new Timer();
            tmr.Interval = interval_sec * 1000;
            tmr.Start();
            tmr.Tick += new EventHandler(Tick2_OutOfShift);
        }

        static void Tick2_OutOfShift(object sender, EventArgs e)
        { 
            DataTable dt = outOfShiftWS.SelectCountOutOfShift(lastOutOfShiftDateTime, null);
            if (dt.Rows.Count > 0 )
            {
                int count = (int)dt.Rows[0][0];
                if (count > 0)
                {
                    lastOutOfShiftDateTime = (DateTime)dt.Rows[0][1];
                    //lastOutOfShiftDateTime = new DateTime(2010, 8, 1, 0, 0, 0);

                    string strLine = string.Format("มีรายการรับเงิน นอกกะ \nจำนวน {0} รายการ  \nตั้งแต่เวลา {1}", count.ToString(), lastOutOfShiftDateTime.ToString("dd/MM/yyyy HH:mm:ss"));
                    ctlDiffuseDlg dlg = new ctlDiffuseDlg();
                    queueDlg.Enqueue(new DiffuseDlgParam(DiffuseDlgParam.AlertType.Normal, dlg, new string[] { "ประกาศ", strLine }));
                }
            }
        }

        private void startTimer1_closeBalanceForAll(int interval_sec)
        {
            
            Timer tmr = new Timer();
            tmr.Interval = interval_sec * 1000;
            tmr.Start();
            tmr.Tick += new EventHandler(Tick1_closeBalanceForAll);
        }

        static void Tick1_closeBalanceForAll(object sender, EventArgs e)
        {

            int allBranch = 0;
            int closedBranch = 0;
            int unclosedBranch = 0;
            bool allClosedBalanceStatus = false;
            GetClosedBalanceStatus(ref allBranch, ref closedBranch, ref unclosedBranch, ref allClosedBalanceStatus);

            if (!allClosedBalanceStatus)
                Accepted_allClosedBalanceStatus = false;

            if (allClosedBalanceStatus && !Accepted_allClosedBalanceStatus)
            {
                string strLine = string.Format("ปิดบัญชี ครบทั้งหมด \nจำนวน {0} สาขา เรียบร้อยแล้ว", allBranch.ToString());
                ctlDiffuseDlg dlg = new ctlDiffuseDlg();
                dlg.Link1Clicked += Dlg_LinkClicked;
                //dlg.Show("ประกาศ", strLine, "<<Accepted>>", 7);
                queueDlg.Enqueue(new DiffuseDlgParam(DiffuseDlgParam.AlertType.Accepted_allClosedBalance, dlg, new string[] { "ประกาศ", strLine, "<<Accepted>>" }));
            }
            else if (!Accepted_allClosedBalanceStatus)
            {
                if (closedBranch/10 > tenClosedBalanceNum)
                {
                    //string strLine = string.Format("ปิดบัญชี แล้วบางส่วน \nจำนวน {0}/{1} สาขา", closedBranch.ToString(), allBranch.ToString());
                    //ctlDiffuseDlg dlg = new ctlDiffuseDlg();
                    //dlg.Show("ประกาศ", strLine, 7);
                    //tenClosedBalanceNum = closedBranch/10;

                    string strLine = string.Format("ปิดบัญชี แล้วบางส่วน \nจำนวน {0}/{1} สาขา", closedBranch.ToString(), allBranch.ToString());
                    ctlDiffuseDlg dlg = new ctlDiffuseDlg();
                    queueDlg.Enqueue(new DiffuseDlgParam(DiffuseDlgParam.AlertType.Normal, dlg, new string[] { "ประกาศ", strLine }));
                    tenClosedBalanceNum = closedBranch / 10;
                }
            }
        }

        private static void GetClosedBalanceStatus(ref int allBranch, ref int closedBranch, ref int unclosedBranch, ref bool allClosedBalanceStatus)
        {
            DataTable dt = closeAccountStatusWS.GetCloseAccountStatusDisplay(DateTime.Now, null, null);
            int rowCount = dt.Rows.Count;
            if (rowCount > 0)
            {
                int closed = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    int num = (int)dr["NumOfBaseLine"];
                    if (num > 0) ++closed;
                }

                allBranch = rowCount;
                closedBranch = closed;
                unclosedBranch = rowCount - closedBranch;

                allClosedBalanceStatus = false;
                if (allBranch > 0)
                    if (closedBranch == allBranch)
                        allClosedBalanceStatus = true;
            }
        }

        private static void Dlg_LinkClicked()
        {
            Accepted_allClosedBalanceStatus = true;
        }

        #endregion

        private void toolStripButtonOutOfBalance_Click(object sender, EventArgs e)
        {
            this.Controls["mainpanel"].Controls.Add(outOfShiftMain);
            outOfShiftMain.Show();
            outOfShiftMain.BringToFront();
            outOfShiftMain.Dock = DockStyle.Fill;
        }
    }

    public class DiffuseDlgParam
    {
        public enum AlertType
        {
            Normal,
            Accepted_allClosedBalance,
        };
        public AlertType alertType;
        public ctlDiffuseDlg dlg;
        public string[] arrayString;

        public DiffuseDlgParam(AlertType type, ctlDiffuseDlg dialog, string[] arrStr)
        {
            alertType = type;
            dlg = dialog;
            arrayString = arrStr;
        }

        public DiffuseDlgParam(ctlDiffuseDlg dialog, string[] arrStr)
        {
            alertType = AlertType.Normal;
            dlg = dialog;
            arrayString = arrStr;
        }

        public void Show(int second)
        {
            if (dlg != null && arrayString.Length > 0)
            {
                switch (arrayString.Length)
                {
                    case 2:
                        dlg.Show(arrayString[0], arrayString[1], second);
                        break;
                    case 3:
                        dlg.Show(arrayString[0], arrayString[1], arrayString[2], second);
                        break;
                    default:
                        break;
                }
            }

        }

        public void Show()
        {
            this.Show(10);
        }
    }

}