//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The FormShell class represent the main form of your application.
// 
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-210-Creating%20a%20Smart%20Client%20Solution.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System.Windows.Forms;
using PEA.BPM.Infrastructure.Interface.Constants;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using System;
using System.IO;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.Infrastructure.Shell
{
    /// <summary>
    /// Main application shell view.
    /// </summary>
    public partial class ShellForm : Form
    {
        private const string version = "2.0.7";       

        /// <summary>
        /// Default class initializer.
        /// </summary>
        public ShellForm()
        {
            InitializeComponent();

            _layoutWorkspace.Name = WorkspaceNames.LayoutWorkspace;
            Session.Application.Version = version;
            this.Text = string.Format("{0} v{1}", "BPM System®", Session.Application.Version);
        }

        [EventSubscription(EventTopicNames.WindowsTitleUpdate, Thread = ThreadOption.UserInterface)]
        public void PaymentItemAddedHandler(object sender, EventArgs<string> e)
        {
            string branch = string.Format("@@@ {0} - {1}", Session.Branch.Id, Session.Branch.Name);
            this.Text = string.Format("{0} - {1} v{2} {3}", e.Data, "BPM System®", Session.Application.Version, branch);
        }

        private void ShellForm_Shown(object sender, System.EventArgs e)
        {
            try
            {
                //--Hide Task Manager-----
                //SetHideTaskManager(true);
                //------------------------
                
                //-- show webservice ที่กำลังใช้งานอยู่ตั้งแต่ก่อน login เลย
#if(!DEBUG)
                 string s = LocalSettingHelper.Instance().ReadString("BackupUri");
                 if (string.IsNullOrEmpty(s))
                 {
                     s = BPMConnection.ProdConnIp;
                     //s = BPMConnection.TestConnIp;
                     LocalSettingHelper.Instance().Add("BackupUri", s);
                 }
#endif
                string server;
                if (Session.Branch.OnlineConnection)
                    server = LocalSettingHelper.Instance().ReadString("CenterServerWsAddress");
                else
                    server = LocalSettingHelper.Instance().ReadString("BranchServerWsAddress");
                Session.Server.ConnectionInfo = server;


                Authorization.MainView = this;

                //LoginForm lf = new LoginForm();
                //if (lf.ShowDialog() == DialogResult.OK)
                if (true)
                {
                    // ShowStatusText("Signing in...");
                    //this.Cursor = Cursors.AppStarting;

                    if (!Authorization.Login("99000065", "1234"))
                    {
                        //Session.Work.OnCloseNotify = false;
                        //this.Close();
                        ShellForm_Shown(sender,e);
                    }
                    else
                    {                        
                        OnConnectionInfoUpdate(null); // clear ทิ้งให้มัน update ตัวเองใหม่, ถ้าเป็น debug mode ข้อมูลที่ได้จะมี server + dbname ของ database ติดมาด้วย
                        OnLoginNameUpdate(string.Format("{0} {1}", Session.User.Id.TrimStart('0'), Session.User.Name));
                        //OnCashierOpenWork("tmp");

                        //begin of Offline by user
                        try
                        {
                            //Load Folder 
                            const string _Path = BPMPath.ConfigPath + "\\XMLData";
                            string _FileName = _Path + "\\branch.txt";

                            if (!Directory.Exists(_Path))
                            {
                                Directory.CreateDirectory(_Path);
                            }
                            
                            File.WriteAllText(_FileName, Session.Branch.Id);
                        }
                        catch 
                        { 
                            //nothing
                        }
                        //end of Offline by user
                    }

                    ShowStatusText("Ready");

                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Session.Work.OnCloseNotify = false;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Session.Work.OnCloseNotify = false;
                MessageBox.Show(ex.ToString());
            }
        }

        [EventPublication(EventTopicNames.LoginNameUpdate, PublicationScope.Global)]
        public event EventHandler<EventArgs<string>> LoginNameUpdate;
        internal void OnLoginNameUpdate(string loginName)
        {
            if (LoginNameUpdate != null)
                LoginNameUpdate(this, new EventArgs<string>(loginName));
        }

        [EventPublication(EventTopicNames.ConnectionInfoUpdate, PublicationScope.Global)]
        public event EventHandler<EventArgs<string>> ConnectionInfoUpdate;
        internal void OnConnectionInfoUpdate(string connectioninfo)
        {
            if (ConnectionInfoUpdate != null)
                ConnectionInfoUpdate(this, new EventArgs<string>(connectioninfo));
        }


        [EventPublication(EventTopicNames.CashierOpenWork, PublicationScope.Global)]
        public event EventHandler<EventArgs<string>> CashierOpenWorkHandler;
        internal void OnCashierOpenWork(string tmp)
        {
            if (CashierOpenWorkHandler != null)
                CashierOpenWorkHandler(this, new EventArgs<string>(tmp));
        }

        [EventPublication(EventTopicNames.StatusUpdate, PublicationScope.Global)]
        public event EventHandler<EventArgs> StatusUpdateHandler;
        public void ShowStatusText(string statusText)
        {
            if (StatusUpdateHandler != null)
                StatusUpdateHandler(this, new EventArgs<string>(statusText));
        }

        private void ShellForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //#ISSUE EDC Close EDC Comport
            //try
            //{
            //    EdcSetting.EDCComport.Close();
            //}
            //catch (Exception ex)
            //{

            //}
        }

        //private void ShellForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    //log off 
        //    if (Session.Work.OnCloseNotify)
        //    {
        //        DialogResult dlg = MessageBox.Show("คุณต้องการปิดโปรแกรม BPM ใช่หรือไม่", "กรุณายืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
        //        if (dlg == DialogResult.No)
        //        {
        //            e.Cancel = true;
        //        }
        //    }
        //}

        //---Function : Hide Task Manager---------------
        #region Function : Hide Task Manager

        //[DllImport("user32.dll")]
        //public static extern int FindWindow(string lpClassName, string lpWindowName);

        //[DllImport("User32.dll")]
        //public static extern Int32 SendMessage(
        //    int hWnd, // handle to destination window
        //    int Msg, // message
        //    int wParam, // first message parameter
        //    int lParam); // second message parameter

        //private void SetHideTaskManager(bool status)
        //{
        //    if (status)
        //    {
        //        Process p = new Process();
        //        p.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);
        //        p.StartInfo.FileName = "taskmgr.exe";
        //        p.StartInfo.CreateNoWindow = true;
        //        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //        p.Start();
        //    }
        //    else 
        //    {
        //        const int WM_CLOSE = 0x0010;
        //        int taskManager = FindWindow("#32770", "Windows Task Manager");
        //        SendMessage(taskManager, WM_CLOSE, 0, 0);
        //    }
        //}

        #endregion Function : Hide Task Manager

    }
}
