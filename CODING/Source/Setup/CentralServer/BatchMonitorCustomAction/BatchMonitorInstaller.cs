using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using System.Web.Configuration;
using BaseCustomAction;

namespace PEA.BPM.Setup.BatchMonitor.CustomAction
{
    [RunInstaller(true)]
    public class BatchMonitorInstaller : System.Configuration.Install.Installer
    {
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            // /targetdir="[TARGETDIR]\" /targetvdir="[TARGETVDIR]" /targetsite="[TARGETSITE]" /dbServerName="[DBSVR_NAME]" /dbServerDbName="[DBSVR_DBNAME]" /dbServerUserID="[DBSVR_UID]" /dbServerPassword="[DBSVR_PWD]"

            string targetDir = Context.Parameters["targetDir"];
            string targetVDir = Context.Parameters["targetvdir"];
            string targetSite = Context.Parameters["targetsite"];

            string dbServerName = Context.Parameters["dbServerName"];
            string dbServerDbName = Context.Parameters["dbServerDbName"];
            string dbServerUserID = Context.Parameters["dbServerUserID"];
            string dbServerPassword = Context.Parameters["dbServerPassword"];

            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/" + targetVDir);
                List<BaseCustomAction.DatabaseAccount> dbAccounts = new List<DatabaseAccount>();
                dbAccounts.Add(new BaseCustomAction.DatabaseAccount("ACABatch_SQL", dbServerDbName, dbServerName, dbServerUserID, dbServerPassword));               
                InstallerHelper.AddConnnectionString(config, dbAccounts, false);
                config.Save();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }

        }
    }
}
