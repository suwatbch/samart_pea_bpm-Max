using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TaskScheduler;
using BaseCustomAction;
using System.Configuration;
using System.Windows.Forms;

namespace PEA.BPM.Setup.Client.CustomAction
{
    [RunInstaller(true)]
    public class ClientInstaller: System.Configuration.Install.Installer
    {
        private string[] _hourTasks = new string[] { 
            "DL001_ISOLATE_BATCH", "DL002_MASTER_BATCH", "DL003_BILLING_BATCH", "DL004_TRANSACTION_BATCH",
            "DL005_PAYFROMSAP_BATCH", "DL006_DIRECTDEBIT_BATCH"};

        private string[] _quarterTasks = new string[] { "DL007_EXPORT_TO_SAP_BATCH"  };

        private string[] _triggerTasks = new string[] { "DL008_EXPORT_TO_SAP_BY_CASH_BATCH" };

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            // aspnet_regiis -pa "NetFrameworkConfigurationKey" "TaskAccount"

            // /targetdir="[TARGETDIR]\" /dbServerName="[DBSVR_NAME]" /dbServerDbName="[DBSVR_DBNAME]" /dbServerUserID="[DBSVR_UID]" /dbServerPassword="[DBSVR_PWD]"
            // /taskUserID="[TASK_UID]" /taskPassword="[TASK_PWD]"

            string targetDir = Context.Parameters["targetDir"];
            string taskUserID = Context.Parameters["taskUserID"];
            string taskPassword = Context.Parameters["taskPassword"];

            string dbServerName = Context.Parameters["dbServerName"];
            string dbServerDbName = Context.Parameters["dbServerDbName"];
            string dbServerUserID = Context.Parameters["dbServerUserID"];
            string dbServerPassword = Context.Parameters["dbServerPassword"];
            
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(targetDir + "BPMScheduler.exe");
                List<BaseCustomAction.DatabaseAccount> dbAccounts = new List<DatabaseAccount>();
                dbAccounts.Add(new BaseCustomAction.DatabaseAccount("ACABatch_SQL", dbServerDbName, dbServerName, dbServerUserID, dbServerPassword));
                InstallerHelper.AddConnnectionString(config, dbAccounts);
                                
                config.Save();

                ScheduledTasks st = new ScheduledTasks();
                for (short i = 0; i < _hourTasks.Length; i++)
                {
                    InstallerHelper.AddScheduledTask(st, targetDir, taskUserID, taskPassword,
                        _hourTasks[i], 60, i);
                }

                for (short i = 0; i < _quarterTasks.Length; i++)
                {
                    InstallerHelper.AddScheduledTask(st, targetDir, taskUserID, taskPassword,
                        _quarterTasks[i], 15, i);
                }

                for (short i = 0; i < _triggerTasks.Length; i++)
                {
                    InstallerHelper.AddScheduledTask(st, targetDir, taskUserID, taskPassword,
                        _triggerTasks[i], 0, 0);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error");
                throw;
            }   
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);

            try
            {
                ScheduledTasks st = new ScheduledTasks();

                for (int i = 0; i < _hourTasks.Length; i++)
                {
                    InstallerHelper.RemoveScheduledTask(st, _hourTasks[i]);
                }

                for (int i = 0; i < _quarterTasks.Length; i++)
                {
                    InstallerHelper.RemoveScheduledTask(st, _quarterTasks[i]);
                }

                for (int i = 0; i < _quarterTasks.Length; i++)
                {
                    InstallerHelper.RemoveScheduledTask(st, _triggerTasks[i]);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error");
                throw;
            }
        }
    }
}