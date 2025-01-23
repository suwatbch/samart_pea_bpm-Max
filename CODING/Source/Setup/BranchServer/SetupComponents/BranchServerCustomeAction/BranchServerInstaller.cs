using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using TaskScheduler;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.ServiceProcess;
using Microsoft.Win32;
using System.Collections;

namespace PEA.BPM.Setup.BranchServer
{
    [RunInstaller(true)]
    public class BranchServerInstaller : System.Configuration.Install.Installer
    {
        //Easy to manage branch stuff
        private string CENTER_WS_ADDRESS;
        private string BRANCH_ID;
        private const string TASK_USER_ID = "TaskAccount";
        private const string TASK_PASSWORD = "Password123";

        //BPM Branch Database
        private const string SERVER_NAME = "(local)";
        private const string APP_DB_SERVER_DB_NAME = "BPM_BRANCH_APP_DB";
        private const string APP_DB_SERVER_USER_ID = "BpmBranchWs";
        private const string APP_DB_SERVER_PASSWORD = "Password123";
        //Batch Database
        private const string BATCH_DB_SERVER_DB_NAME = "BPM_BRANCH_BATCH_DB";
        private const string BATCH_DB_SERVER_USER_ID = "BpmBranchBatch";
        private const string BATCH_DB_SERVER_PASSWORD = "Password123";

        //ACAService parameters
        private const string ACA_BATCH_SERVER = "ACA.NET Batch Branch Server 4.1";

        //sa password
        private const string SA_USER_ID = "sa";
        private const string SA_PASSWORD = "Password123";

        //defined in global scope that every installed components will use.
        private const string ROOT_TARGET_DIR = @"c:\Avanade";

        //global paths
        private string DB_TARGET_DIR = string.Format(@"{0}\BranchDatabase", ROOT_TARGET_DIR);
        private string BATCH_TARGET_DIR = string.Format(@"{0}\ACABatchServerHost\", ROOT_TARGET_DIR);
        private string SCHEDULER_TARGET_DIR = string.Format(@"{0}\ACABatchScheduler\", ROOT_TARGET_DIR);

        private string[] _hourTasks = new string[] { 
            "DL010_ISOLATE_BATCH",          "DL020_MASTER_BATCH",       "DL021_MASTER2_BATCH",      
            "DL030_BILLING_BATCH",          "DL040_AR_BATCH",           "DL050_PAYMENT_BATCH", 
            "DL051_CASH_MANAGEMENT_BATCH",  "DL060_AGENCY_BATCH",       "DL061_EPAYMENT_BATCH",
            "DL070_TECHNICAL_BATCH"
        };

        private string[] _quarterTasks = new string[] {
            "DL071_UPLOAD_MASTER_JOB",              "DL080_UPLOAD_AP_BATCH",        "DL090_UPLOAD_PAYMENT_BATCH", 
            "DL091_UPLOAD_CASH_MANAGEMENT_BATCH",   "DL100_UPLOAD_AGENCY_BATCH",    "DL101_UPLOAD_EPAYMENT_BATCH",          
            "DL110_UPLOAD_TECHNICAL_BATCH",         "DL120_UPLOAD_BILLING_BATCH",   "DL130_UPLOAD_AG_COMPENSATION_BATCH"
        };

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            if (!Directory.Exists(ROOT_TARGET_DIR))
                Directory.CreateDirectory(ROOT_TARGET_DIR);

            base.Install(stateSaver);
            CENTER_WS_ADDRESS = Context.Parameters["CENTRALSERVER"];
            BRANCH_ID = Context.Parameters["BRANCHID"];
            //Database Installation
            try
            {
                //administrator is running this installation, so we can use integrated security
                Server branchServer = new Server(SERVER_NAME);
                try
                {
                    branchServer.ConnectionContext.LoginSecure = true;
                    branchServer.ConnectionContext.ConnectTimeout = 10000;
                    branchServer.ConnectionContext.Connect();

                    if (branchServer.ConnectionContext.IsOpen)
                    {
                        Database bpmDbCheck = branchServer.Databases["BPM_BRANCH_APP_DB"];
                        if (bpmDbCheck == null)
                        {
                            //restore database
                            Restore bpmDb = new Restore();
                            bpmDb.Database = "BPM_BRANCH_APP_DB";
                            bpmDb.Action = RestoreActionType.Database;
                            bpmDb.Devices.AddDevice(string.Format(@"{0}\BpmDB.bak", DB_TARGET_DIR), DeviceType.File);
                            bpmDb.ReplaceDatabase = false;
                            bpmDb.SqlRestore(branchServer);
                            //InstallerHelper.RegisterComponent("BPM_BRANCH_APP_DB");

                            //add role for these accounts
                            Login appLogin = new Login(branchServer, "BpmBranchWs");
                            appLogin.LoginType = LoginType.SqlLogin;
                            appLogin.DefaultDatabase = "BPM_BRANCH_APP_DB";
                            appLogin.Create("Password123");
                            //InstallerHelper.RegisterComponent("APP_DB_LOGIN");

                            //users BpmBranchWs and BpmBatchWs MUST NOT come with the restored databases
                            Database bpmDatabase = branchServer.Databases["BPM_BRANCH_APP_DB"];
                            User bpmUser = new User(bpmDatabase, "BpmBranchWs");
                            bpmUser.UserType = UserType.SqlLogin;
                            bpmUser.Login = "BpmBranchWs";
                            bpmUser.Create();

                            bpmDatabase.Roles["db_datareader"].AddMember("BpmBranchWs");
                            bpmDatabase.Roles["db_datawriter"].AddMember("BpmBranchWs");
                        }

                        branchServer.Refresh();
                        Database batchDbCheck = branchServer.Databases["BPM_BRANCH_BATCH_DB"];
                        if (batchDbCheck == null)
                        {
                            //restore database
                            Restore batchDb = new Restore();
                            batchDb.Database = "BPM_BRANCH_BATCH_DB";
                            batchDb.Action = RestoreActionType.Database;
                            batchDb.Devices.AddDevice(string.Format(@"{0}\BpmBatchDb.bak", DB_TARGET_DIR), DeviceType.File);
                            batchDb.ReplaceDatabase = false;
                            batchDb.SqlRestore(branchServer);
                            //InstallerHelper.RegisterComponent("BPM_BRANCH_BATCH_DB");

                            //add role for these accounts
                            Login batchLogin = new Login(branchServer, "BpmBranchBatch");
                            batchLogin.LoginType = LoginType.SqlLogin;
                            batchLogin.DefaultDatabase = "BPM_BRANCH_BATCH_DB";
                            batchLogin.Create("Password123");
                            //InstallerHelper.RegisterComponent("BATCH_DB_LOGIN");

                            //users BpmBranchWs and BpmBatchWs MUST NOT come with the restored databases
                            Database batchDatabase = branchServer.Databases["BPM_BRANCH_BATCH_DB"];
                            User batchUser = new User(batchDatabase, "BpmBranchBatch");
                            batchUser.UserType = UserType.SqlLogin;
                            batchUser.Login = "BpmBranchBatch";
                            batchUser.Create();

                            batchDatabase.Roles["db_datareader"].AddMember("BpmBranchBatch");
                            batchDatabase.Roles["db_datawriter"].AddMember("BpmBranchBatch");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (branchServer.ConnectionContext.IsOpen)
                        branchServer.ConnectionContext.Disconnect();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }

            //ACABatch Installation
            //Warning! username and password must be for user only. not for administrator
            string appConnString = string.Format("Database={0};Server={1};Integrated Security=False;uid={2};pwd={3};Connection Timeout=300;",
                    new object[] { APP_DB_SERVER_DB_NAME, SERVER_NAME, APP_DB_SERVER_USER_ID, APP_DB_SERVER_PASSWORD });

            try
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(BATCH_TARGET_DIR + "Avanade.ACA.Batch.BatchBranchServerHost.exe");
                List<DatabaseAccount> dbAccounts = new List<DatabaseAccount>();
                dbAccounts.Add(new DatabaseAccount("ACABatch_SQL", BATCH_DB_SERVER_DB_NAME, SERVER_NAME, BATCH_DB_SERVER_USER_ID, BATCH_DB_SERVER_PASSWORD));
                InstallerHelper.AddConnnectionString(config, dbAccounts, false);
                config.AppSettings.Settings.Clear();
                config.Save();

                config = ConfigurationManager.OpenExeConfiguration(BATCH_TARGET_DIR + "Avanade.ACA.Batch.BatchExecutionHost.exe");
                dbAccounts.Add(new DatabaseAccount("BPMDatabase", APP_DB_SERVER_DB_NAME, SERVER_NAME, APP_DB_SERVER_USER_ID, APP_DB_SERVER_PASSWORD));
                InstallerHelper.AddConnnectionString(config, dbAccounts, false);
                config.AppSettings.Settings.Clear();
                config.AppSettings.Settings.Add("WS_AR_ADDR", string.Format("{0}ARWebService.asmx", CENTER_WS_ADDRESS));
                config.AppSettings.Settings.Add("WS_AGENCY_ADDR", string.Format("{0}AgencyIntegrationWebService.asmx", CENTER_WS_ADDRESS));
                config.AppSettings.Settings.Add("WS_MASTER_ADDR", string.Format("{0}MasterIntegrationWebService.asmx", CENTER_WS_ADDRESS));
                config.AppSettings.Settings.Add("WS_BLAN_ADDR", string.Format("{0}BLANIntegrationWebService.asmx", CENTER_WS_ADDRESS));
                config.AppSettings.Settings.Add("WS_UTIL_ADDR", string.Format("{0}UtilitiesIntegrationWebService.asmx", CENTER_WS_ADDRESS));
                config.AppSettings.Settings.Add("BRANCHID", BRANCH_ID);
                config.Save();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }


            //Start ACABatch Service
            try
            {
                //service assembly path
                string servicePath = string.Format(@"{0}Avanade.ACA.Batch.BatchBranchServerHost.exe", BATCH_TARGET_DIR);
                //remove the old installed service
                if (ServiceInstaller.ServiceIsInstalled(ACA_BATCH_SERVER))
                    ServiceInstaller.Uninstall(ACA_BATCH_SERVER);

                ServiceInstaller.InstallAndStart(ACA_BATCH_SERVER, ACA_BATCH_SERVER, servicePath);
                //InstallerHelper.RegisterComponent("ACA_BATCH_SERVICE");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }


            //ACABatch Scheduler Installation
            try
            {
                //create windows account to run ACABatch schuduler
                if (!WindowsAccount.Exist(TASK_USER_ID))
                {
                    WindowsAccount.CreateLocalWindowsAccount(TASK_USER_ID, TASK_PASSWORD, "Task account", "Run batch scheduler", false, false);
                    InstallerHelper.RegisterComponent("TASK_ACCOUNT");
                }

                //create task scheduler
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(SCHEDULER_TARGET_DIR + "BPMScheduler.exe");
                List<DatabaseAccount> dbAccounts = new List<DatabaseAccount>();
                dbAccounts.Add(new DatabaseAccount("ACABatch_SQL", BATCH_DB_SERVER_DB_NAME, SERVER_NAME, BATCH_DB_SERVER_USER_ID, BATCH_DB_SERVER_PASSWORD));
                InstallerHelper.AddConnnectionString(config, dbAccounts, false);
                config.Save();

                ScheduledTasks st = new ScheduledTasks();
                //remove old
                for (int i = 0; i < _hourTasks.Length; i++)
                    InstallerHelper.RemoveScheduledTask(st, _hourTasks[i]);

                for (int i = 0; i < _quarterTasks.Length; i++)
                    InstallerHelper.RemoveScheduledTask(st, _quarterTasks[i]);

                //install new
                for (short i = 0; i < _hourTasks.Length; i++)
                    InstallerHelper.AddScheduledTask(st, SCHEDULER_TARGET_DIR, TASK_USER_ID, TASK_PASSWORD, _hourTasks[i], 240, i);

                for (short i = 0; i < _quarterTasks.Length; i++)
                    InstallerHelper.AddScheduledTask(st, SCHEDULER_TARGET_DIR, TASK_USER_ID, TASK_PASSWORD, _quarterTasks[i], 240, i);

                //InstallerHelper.RegisterComponent("TASK_SCHEDULER");
                //InstallerHelper.CommitRegisteredComponent();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }
        }

        //incase install eror
        public override void Rollback(System.Collections.IDictionary savedState)
        {
            base.Rollback(savedState);
            //MessageBox.Show("Start Rollback");
            //Server branchServer = new Server(SERVER_NAME);

            //try
            //{
            //    branchServer.ConnectionContext.LoginSecure = true;
            //    branchServer.ConnectionContext.ConnectTimeout = 10000;
            //    branchServer.ConnectionContext.Connect();

            //    //find and remove uncommited registered component
            //    RegistryKey r = Registry.LocalMachine.CreateSubKey(@"Software\Portalnet");
            //    foreach (string keyname in Registry.LocalMachine.GetSubKeyNames())
            //    {
            //        int keyValue = (int)r.GetValue(keyname);
            //        //remove uncommited installing components
            //        if (keyValue == 0)
            //        {
            //            switch (keyname)
            //            {
            //                case "BPM_BRANCH_APP_DB":
            //                    if (branchServer.ConnectionContext.IsOpen)
            //                    {
            //                        Database bpmDatabase = null;
            //                        foreach (Database db in branchServer.Databases)
            //                        {
            //                            if (db.Name == APP_DB_SERVER_DB_NAME)
            //                                bpmDatabase = db;
            //                        }

            //                        //delete users
            //                        User toDelete = null;
            //                        foreach (User user in bpmDatabase.Users)
            //                        {
            //                            if (user.Name == "BpmBranchWs")
            //                            {
            //                                toDelete = user;
            //                                break;
            //                            }
            //                        }

            //                        if (toDelete != null) toDelete.Drop();
            //                        //drop database
            //                        bpmDatabase.Drop();
            //                    }
            //                    break;
            //                case "APP_DB_LOGIN":
            //                    if (branchServer.ConnectionContext.IsOpen)
            //                    {
            //                        Login wsLogin = null;
            //                        foreach (Login login in branchServer.Logins)
            //                        {
            //                            if (login.Name == "BpmBranchWs")
            //                                wsLogin = login;
            //                        }

            //                        if (wsLogin != null) wsLogin.Drop();
            //                    }
            //                    break;
            //                case "BPM_BRANCH_BATCH_DB":
            //                    if (branchServer.ConnectionContext.IsOpen)
            //                    {
            //                        Database batchDatabase = null;
            //                        foreach (Database db in branchServer.Databases)
            //                        {
            //                            if (db.Name == BATCH_DB_SERVER_DB_NAME)
            //                                batchDatabase = db;
            //                        }

            //                        //delete users
            //                        User toDelete = null;
            //                        foreach (User user in batchDatabase.Users)
            //                        {
            //                            if (user.Name == "BpmBranchBatch")
            //                            {
            //                                toDelete = user;
            //                                break;
            //                            }
            //                        }

            //                        if (toDelete != null) toDelete.Drop();
            //                        //drop database
            //                        batchDatabase.Drop();
            //                    }
            //                    break;
            //                case "BATCH_DB_LOGIN":
            //                    if (branchServer.ConnectionContext.IsOpen)
            //                    {
            //                        Login batchLogin = null;
            //                        foreach (Login login in branchServer.Logins)
            //                        {
            //                            if (login.Name == "BpmBranchBatch")
            //                                batchLogin = login;
            //                        }

            //                        if (batchLogin != null) batchLogin.Drop();
            //                    }
            //                    break;
            //                case "ACA_BATCH_SERVICE":
            //                    {
            //                        if (ServiceInstaller.ServiceIsInstalled(ACA_BATCH_SERVER))
            //                            ServiceInstaller.Uninstall(ACA_BATCH_SERVER);
            //                    }
            //                    break;
            //                case "TASK_ACCOUNT":
            //                    {
            //                        //delete TaskAccount
            //                        WindowsAccount.DeleteLocalWindowsAcccount(TASK_USER_ID);
            //                    }
            //                    break;
            //                case "TASK_SCHEDULER":
            //                    {
            //                        ScheduledTasks st = new ScheduledTasks();

            //                        for (int i = 0; i < _hourTasks.Length; i++)
            //                            InstallerHelper.RemoveScheduledTask(st, _hourTasks[i]);

            //                        for (int i = 0; i < _quarterTasks.Length; i++)
            //                            InstallerHelper.RemoveScheduledTask(st, _quarterTasks[i]);
            //                    }
            //                    break;
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}
            //finally
            //{
            //    if (branchServer.ConnectionContext.IsOpen)
            //        branchServer.ConnectionContext.Disconnect();
            //}
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);
            //webservice will be automatically removed
            //remove task scheduler
            try
            {
                ScheduledTasks st = new ScheduledTasks();

                for (int i = 0; i < _hourTasks.Length; i++)
                    InstallerHelper.RemoveScheduledTask(st, _hourTasks[i]);

                for (int i = 0; i < _quarterTasks.Length; i++)
                    InstallerHelper.RemoveScheduledTask(st, _quarterTasks[i]);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }

            //delete TaskAccount
            WindowsAccount.DeleteLocalWindowsAcccount(TASK_USER_ID);

            //stop and uninstall ACABatch Service
            if (ServiceInstaller.ServiceIsInstalled(ACA_BATCH_SERVER))
                ServiceInstaller.Uninstall(ACA_BATCH_SERVER);

            //backup database
            Server branchServer = new Server(SERVER_NAME);
            try
            {
                branchServer.ConnectionContext.LoginSecure = true;
                branchServer.ConnectionContext.ConnectTimeout = 10000;
                branchServer.ConnectionContext.Connect();

                if (branchServer.ConnectionContext.IsOpen)
                {
                    Database bpmDatabase = null;
                    Database batchDatabase = null;
                    foreach (Database db in branchServer.Databases)
                    {
                        if (db.Name == APP_DB_SERVER_DB_NAME)
                            bpmDatabase = db;

                        if (db.Name == BATCH_DB_SERVER_DB_NAME)
                            batchDatabase = db;
                    }

                    if (bpmDatabase != null)
                    {
                        //backup
                        Backup backupAppDb = new Backup();
                        backupAppDb.Database = APP_DB_SERVER_DB_NAME;
                        backupAppDb.Action = BackupActionType.Database;
                        backupAppDb.Devices.AddDevice(string.Format(@"{0}\BpmDB_{1}.bak", DB_TARGET_DIR, DateTime.Now.ToString("yyyyMMddhhmmss")), DeviceType.File);
                        backupAppDb.LogTruncation = BackupTruncateLogType.Truncate;
                        backupAppDb.SqlBackup(branchServer);

                        //delete users
                        User toDelete = null;
                        foreach (User user in bpmDatabase.Users)
                        {
                            if (user.Name == "BpmBranchWs")
                            {
                                toDelete = user;
                                break;
                            }
                        }

                        if (toDelete != null) toDelete.Drop();
                        //drop database
                        bpmDatabase.Drop();
                    }

                    if (batchDatabase != null)
                    {
                        //backup database
                        Backup backupBatchDb = new Backup();
                        backupBatchDb.Database = BATCH_DB_SERVER_DB_NAME;
                        backupBatchDb.Action = BackupActionType.Database;
                        backupBatchDb.Devices.AddDevice(string.Format(@"{0}\BatchDB_{1}.bak", DB_TARGET_DIR, DateTime.Now.ToString("yyyyMMddhhmmss")), DeviceType.File);
                        backupBatchDb.LogTruncation = BackupTruncateLogType.Truncate;
                        backupBatchDb.SqlBackup(branchServer);

                        //delete users
                        User toDelete = null;
                        foreach (User user in batchDatabase.Users)
                        {
                            if (user.Name == "BpmBranchBatch")
                            {
                                toDelete = user;
                                break;
                            }
                        }

                        if (toDelete != null) toDelete.Drop();
                        //drop database
                        batchDatabase.Drop();
                    }

                    Login wsLogin = null;
                    Login batchLogin = null;
                    foreach (Login login in branchServer.Logins)
                    {
                        if (login.Name == "BpmBranchWs")
                            wsLogin = login;

                        if (login.Name == "BpmBranchBatch")
                            batchLogin = login;
                    }

                    if (wsLogin != null) wsLogin.Drop();
                    if (batchLogin != null) batchLogin.Drop();
                    //InstallerHelper.CleanupAllRegisteredComponent();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            finally
            {
                if(branchServer.ConnectionContext.IsOpen)
                    branchServer.ConnectionContext.Disconnect();
            }
        }
    }

    public class DatabaseAccount
    {
        public string SettingName;
        public string ServerDbName;
        public string ServerName;
        public string UserID;
        public string Password;

        public DatabaseAccount(string settingName, string serverDbName,
            string serverName, string userID, string password)
        {
            SettingName = settingName;
            ServerDbName = serverDbName;
            ServerName = serverName;
            UserID = userID;
            Password = password;
        }

        public string GetConnectionString()
        {
            return string.Format("Database={0};Server={1};Integrated Security=False;uid={2};pwd={3};Connection Timeout=300;",
                    new object[] { ServerDbName, ServerName, UserID, Password });
        }
    }

    public class InstallerHelper
    {
        public static void ValidateConnectionString(string connString)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                conn.Close();
            }
        }

        public static void AddConnnectionString(System.Configuration.Configuration config, List<DatabaseAccount> dbAccounts)
        {
            AddConnnectionString(config, dbAccounts, true);
        }

        public static void AddConnnectionString(System.Configuration.Configuration config, List<DatabaseAccount> dbAccounts, bool encryptConnectionString)
        {
            // Clear existing connection string
            config.ConnectionStrings.ConnectionStrings.Clear();

            foreach (DatabaseAccount dbAccount in dbAccounts)
            {
                string connString = dbAccount.GetConnectionString();
                ValidateConnectionString(connString);

                ConnectionStringSettings setting = new ConnectionStringSettings();
                setting.Name = dbAccount.SettingName;
                setting.ProviderName = "System.Data.SqlClient";
                setting.ConnectionString = connString;
                config.ConnectionStrings.ConnectionStrings.Add(setting);
            }

            if (encryptConnectionString)
            {
                // Encrypt the connection strings section
                ConfigurationSection section = config.GetSection("connectionStrings");
                if (section != null && section.IsReadOnly() == false)
                {
                    section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                    section.SectionInformation.ForceSave = true;
                }
            }
        }


        public static void AddScheduledTask(ScheduledTasks st, string targetDir, string taskUserID, string taskPassword,
            string taskName, int interval, short startingMin)
        {
            Task t = st.OpenTask(taskName);
            if (null != t)
            {
                st.DeleteTask(taskName);
            }
            t = st.CreateTask(taskName);

            t.WorkingDirectory = targetDir;
            t.ApplicationName = string.Format("{0}BPMScheduler.exe", targetDir);
            t.Parameters = string.Format("{0} BPMDbServer", taskName);
            t.SetAccountInformation(taskUserID, taskPassword);

            if (interval > 0)
            {
                DailyTrigger dt = new DailyTrigger(1, startingMin);
                dt.DurationMinutes = 1439;
                dt.IntervalMinutes = interval;
                t.Triggers.Add(dt);
            }

            t.Save();
        }

        public static void RemoveScheduledTask(ScheduledTasks st, string taskName)
        {
            Task t = st.OpenTask(taskName);
            if (null != t)
            {
                st.DeleteTask(taskName);
            }
        }

        public static void CleanupAllRegisteredComponent()
        {
            Registry.LocalMachine.DeleteSubKeyTree(@"Software\Portalnet");
        }

        public static void RegisterComponent(string componentName)
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(@"Software\Portalnet");
            r.SetValue(componentName, 0); //not commited
            r.Close();
        }

        public static void CommitRegisteredComponent()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(@"Software\Portalnet");

            foreach (string keyname in Registry.LocalMachine.OpenSubKey(@"Software\Portalnet").GetValueNames())
                r.SetValue(keyname, 1); //commited

            r.Close();
        }
    }
}
