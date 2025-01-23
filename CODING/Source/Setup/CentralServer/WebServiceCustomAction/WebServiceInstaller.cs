using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using System.Web.Configuration;
using BaseCustomAction;
using System.Windows.Forms;
using Microsoft.Win32;

namespace PEA.BPM.Setup.WebService.CustomAction
{
    [RunInstaller(true)]
    public class WebServiceInstaller : System.Configuration.Install.Installer
    {
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            //check IIS and ASP.NET installed
            string asp = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("ASP.NET").GetValue("RootVer").ToString();
            if (asp.Substring(0, 1) != "2")
            {
                MessageBox.Show("Please install IIS6.0 or IIS7.0 with ASP.NET enabled.");
                Application.Exit();
            }

            // aspnet_regiis -pa "NetFrameworkConfigurationKey" "ASPNET"
            // aspnet_regiis -pa "NetFrameworkConfigurationKey" "NT Authority\Network Service"

            // /targetdir="[TARGETDIR]\" /targetvdir="[TARGETVDIR]" /targetsite="[TARGETSITE]" /branchID="[BRANCH_ID]"
            // /appDbServerName="[APP_DBSVR_NAME]" /appDbServerDbName="[APP_DBSVR_DBNAME]" /appDbServerUserID="[APP_DBSVR_UID]" /appDbServerPassword="[APP_DBSVR_PWD]"
            // /batchDbServerName="[BATCH_DBSVR_NAME]" /batchDbServerDbName="[BATCH_DBSVR_DBNAME]" /batchDbServerUserID="[BATCH_DBSVR_UID]" /batchDbServerPassword="[BATCH_DBSVR_PWD]"
            // /serverType="[SERVER_TYPE]"

            string targetDir = Context.Parameters["targetDir"];
            string targetVDir = Context.Parameters["targetvdir"];
            string targetSite = Context.Parameters["targetsite"];

            string branchID = Context.Parameters["branchID"];

            string appDbServerName = Context.Parameters["appDbServerName"];
            string appDbServerDbName = Context.Parameters["appDbServerDbName"];
            string appDbServerUserID = Context.Parameters["appDbServerUserID"];
            string appDbServerPassword = Context.Parameters["appDbServerPassword"];

            string batchDbServerName = Context.Parameters["batchDbServerName"];
            string batchDbServerDbName = Context.Parameters["batchDbServerDbName"];
            string batchDbServerUserID = Context.Parameters["batchDbServerUserID"];
            string batchDbServerPassword = Context.Parameters["batchDbServerPassword"];


            string serverType = Context.Parameters["serverType"];

            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/" + targetVDir);
                List<BaseCustomAction.DatabaseAccount> dbAccounts = new List<DatabaseAccount>();

                if (!(serverType == null || serverType == string.Empty))
                {

                    dbAccounts.Add(new BaseCustomAction.DatabaseAccount("ACABatch_SQL", batchDbServerDbName, batchDbServerName, batchDbServerUserID, batchDbServerPassword));
                    dbAccounts.Add(new BaseCustomAction.DatabaseAccount("POSDatabase", appDbServerDbName, appDbServerName, appDbServerUserID, appDbServerPassword));
                    InstallerHelper.AddConnnectionString(config, dbAccounts, false);

                    config.AppSettings.Settings.Clear();
                    config.AppSettings.Settings.Add("BranchID", branchID);

                    if (serverType == "CENTER")
                    {
                        config.AppSettings.Settings.Add("SAP_CONN", "!!!PLEASE SPECIFY YOUR SAP CONNECTION STRING!!!");
                        config.AppSettings.Settings.Add("ExportBatchName", "DL008_EXPORT_TO_SAP_BY_CASH_BATCH");
                        config.AppSettings.Settings.Add("Destination", "BPMDbServer");
                    }
                    else
                    {
                        //config.AppSettings.Settings.Add("SyncUpBatchName", "DL200_REALTIME_SYNC_CASH_MANAGEMENT_BATCH");
                        config.AppSettings.Settings.Add("Destination", "BPMDbServer");
                    }
                }
                else
                {
                    dbAccounts.Add(new BaseCustomAction.DatabaseAccount("POSDatabase", appDbServerDbName, appDbServerName, appDbServerUserID, appDbServerPassword));
                    InstallerHelper.AddConnnectionString(config, dbAccounts, false);
                }

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
