using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using BaseCustomAction;
using System.Xml;
using System.IO;
using System.Data.SqlClient;

namespace PEA.BPM.Setup.CenterBatchServer.CustomAction
{
    [RunInstaller(true)]
    public class CenterBatchServerInstaller : System.Configuration.Install.Installer
    {
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            // /targetdir="[TARGETDIR]\" /appDbServerName="[APP_DBSVR_NAME]" /appDbServerDbName="[APP_DBSVR_DBNAME]" /appDbServerUserID="[APP_DBSVR_UID]" /appDbServerPassword="[APP_DBSVR_PWD]"
            // /batchDbServerName="[BATCH_DBSVR_NAME]" /batchDbServerDbName="[BATCH_DBSVR_DBNAME]" /batchDbServerUserID="[BATCH_DBSVR_UID]" /batchDbServerPassword="[BATCH_DBSVR_PWD]"
            // /inboundPath="[INBOUND_PATH]\" /outboundPath="[OUTBOUND_PATH]\" /processPath="[PROCESS_PATH]\" /bulkPath="[BULK_PATH]\"

            string targetDir = Context.Parameters["targetDir"];

            string batchDbServerName = Context.Parameters["batchDbServerName"];
            string batchDbServerDbName = Context.Parameters["batchDbServerDbName"];
            string batchDbServerUserID = Context.Parameters["batchDbServerUserID"];
            string batchDbServerPassword = Context.Parameters["batchDbServerPassword"];
                        
            string appDbServerName = Context.Parameters["appDbServerName"];
            string appDbServerDbName = Context.Parameters["appDbServerDbName"];
            string appDbServerUserID = Context.Parameters["appDbServerUserID"];
            string appDbServerPassword = Context.Parameters["appDbServerPassword"];
            
            string inboundPath = Context.Parameters["inboundPath"];
            string outboundPath = Context.Parameters["outboundPath"];
            string processPath = Context.Parameters["processPath"];
            string bulkPath = Context.Parameters["bulkPath"];

            try
            {
                if (!Directory.Exists(inboundPath))
                {
                    Directory.CreateDirectory(inboundPath);
                }

                if (!Directory.Exists(outboundPath))
                {
                    Directory.CreateDirectory(outboundPath);
                }

                CreateBpmDataProcessingPath(processPath);
                CreateBpmBulkLoadPath(bulkPath);

                string sHostPath = targetDir + "Avanade.ACA.Batch.BatchCenterServerHost.exe";
                string xHostPath = string.Format("{0}Avanade.ACA.Batch.BatchExecutionHost.exe", targetDir);

                StreamReader sr = new StreamReader(sHostPath + ".config");
                string x = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                x = x.Replace("Avanade.ACA.Batch.BatchExecutionHost.exe", xHostPath);
                StreamWriter sw = new StreamWriter(sHostPath + ".config", false);
                sw.Write(x);
                sw.Flush();
                sw.Close();
                sw.Dispose();

                Configuration config = ConfigurationManager.OpenExeConfiguration(sHostPath);
                List<BaseCustomAction.DatabaseAccount> dbAccounts = new List<DatabaseAccount>();
                dbAccounts.Add(new BaseCustomAction.DatabaseAccount("ACABatch_SQL", batchDbServerDbName, batchDbServerName, batchDbServerUserID, batchDbServerPassword));
                InstallerHelper.AddConnnectionString(config, dbAccounts, false);
                config.AppSettings.Settings.Clear();
                config.Save();            

                config = ConfigurationManager.OpenExeConfiguration(xHostPath);
                dbAccounts.Add(new BaseCustomAction.DatabaseAccount("POSDatabase", appDbServerDbName, appDbServerName, appDbServerUserID, appDbServerPassword));
                InstallerHelper.AddConnnectionString(config, dbAccounts, false);
                config.AppSettings.Settings.Clear();
                config.AppSettings.Settings.Add("INBOUND_PATH", inboundPath);
                config.AppSettings.Settings.Add("OUTBOUND_PATH", outboundPath);
                config.AppSettings.Settings.Add("PROCESS_PATH", processPath);
                config.AppSettings.Settings.Add("BULK_PATH", bulkPath);
                
                config.AppSettings.Settings.Add("SAP_CONN", "PLEASE SPECIFY YOUR SAP CONNNECTION STRING");
                config.AppSettings.Settings.Add("WS_BPM_ADDR", "http://172.30.241.181/BpmCenterAppServices/ARCH/NotificationWCFService.svc");
                config.AppSettings.Settings.Add("WS_BPM_ADDR2", "http://172.30.241.182/BpmCenterAppServices/ARCH/NotificationWCFService.svc");
                config.Save();

                SetupBillPrintingImport(targetDir, processPath, dbAccounts[1], bulkPath);
                SetupARImport(targetDir, processPath, dbAccounts[1], bulkPath);
                SetupPayFromSAPImport(targetDir, processPath, dbAccounts[1], bulkPath);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }        
        }

        private void CreateBpmBulkLoadPath(string bulkPath)
        {
            Directory.CreateDirectory(string.Format(@"{0}Format", bulkPath));
            Directory.CreateDirectory(string.Format(@"{0}Data", bulkPath));            
        }

        private void SetupBillPrintingImport(string targetDir, string processPath, DatabaseAccount databaseAccount, string bulkPath)
        {
            string formatFile = string.Format(@"{0}{1}\{2}",
                new string[] { bulkPath, "Format", "billformat.xml" });
            string dataFile = string.Format(@"{0}{1}\{2}",
                new string[] { bulkPath, "Data", "billdata.txt" });

            File.Copy(string.Format("{0}{1}", targetDir, "billformat.xml"), formatFile, true);
            File.Copy(string.Format("{0}{1}", targetDir, "billdata.txt"), dataFile, true);

            string importSpCmd = GetCommand(targetDir, "1-BillingImportSp.txt");
            importSpCmd = importSpCmd.Replace("DB_NAME", databaseAccount.ServerDbName);
            importSpCmd = importSpCmd.Replace("'billformat.xml'", string.Format("'{0}'", formatFile));  
            importSpCmd = importSpCmd.Replace("'billdata.txt'", string.Format("'{0}'",dataFile));          
              
            using(SqlConnection conn =
                    new SqlConnection(
                    string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True; Connection Timeout=120", 
                    databaseAccount.ServerName, databaseAccount.ServerDbName)))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = importSpCmd;
                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        private void SetupARImport(string targetDir, string processPath, DatabaseAccount databaseAccount, string bulkPath)
        {
            string formatFile = string.Format(@"{0}{1}\{2}",
                new string[] { bulkPath, "Format", "arformat.xml" });
            string dataFile = string.Format(@"{0}{1}\{2}",
                new string[] { bulkPath, "Data", "ardata.txt" });

            File.Copy(string.Format("{0}{1}", targetDir, "arformat.xml"), formatFile, true);
            File.Copy(string.Format("{0}{1}", targetDir, "ardata.txt"), dataFile, true);

            string importSpCmd = GetCommand(targetDir, "2-ARImportSp.txt");
            importSpCmd = importSpCmd.Replace("DB_NAME", databaseAccount.ServerDbName);
            importSpCmd = importSpCmd.Replace("'arformat.xml'", string.Format("'{0}'", formatFile));
            importSpCmd = importSpCmd.Replace("'ardata.txt'", string.Format("'{0}'", dataFile));

            using (SqlConnection conn =
                    new SqlConnection(
                    string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True; Connection Timeout=120",
                    databaseAccount.ServerName, databaseAccount.ServerDbName)))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = importSpCmd;
                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        private void SetupPayFromSAPImport(string targetDir, string processPath, DatabaseAccount databaseAccount, string bulkPath)
        {
            string formatFile = string.Format(@"{0}{1}\{2}",
                new string[] { bulkPath, "Format", "pfsformat.xml" });
            string dataFile = string.Format(@"{0}{1}\{2}",
                new string[] { bulkPath, "Data", "pfsdata.txt" });

            File.Copy(string.Format("{0}{1}", targetDir, "pfsformat.xml"), formatFile, true);
            File.Copy(string.Format("{0}{1}", targetDir, "pfsdata.txt"), dataFile, true);

            string importSpCmd = GetCommand(targetDir, "3-PayFromSAPImportSp.txt");
            importSpCmd = importSpCmd.Replace("DB_NAME", databaseAccount.ServerDbName);
            importSpCmd = importSpCmd.Replace("'pfsformat.xml'", string.Format("'{0}'", formatFile));
            importSpCmd = importSpCmd.Replace("'pfsdata.txt'", string.Format("'{0}'", dataFile));

            using (SqlConnection conn =
                    new SqlConnection(
                    string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True; Connection Timeout=120",
                    databaseAccount.ServerName, databaseAccount.ServerDbName)))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = importSpCmd;
                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        private string GetCommand(string targetDir, string fileName)
        {
            FileInfo fs = new FileInfo(targetDir + fileName);
            return fs.OpenText().ReadToEnd().Replace("GO", "");

        }

        private void CreateBpmDataProcessingPath(string processPath)
        {
            Dictionary<string, string[]> directoryTree = new Dictionary<string, string[]>();
            directoryTree.Add("DL001_ISOLATE",
                new string[] { "CFR0010", "CFR0030", "CFR0050", "CFR0060", "CFR0070", "CFR0080", "MTR0080" });
            directoryTree.Add("DL002_MASTER",
                new string[] { "MTR0020", "MTR0030", "MTR0040", "MTR0050", "MTR0060A", "MTR0060B", 
                    "MTR0090A", "MTR0090B", "MTR0090C", "MTR0090D", "MTR0100", "MTR0110", "MTR0120", "MTR0130" });
            directoryTree.Add("DL003_BILLING",
                new string[] { "TRR0030" });
            directoryTree.Add("DL004_TRANSACTION",
                new string[] { "TRR0010A", "TRR0010B", "TRR0010C", "TRR0010D", 
                    "TRR0010E", "TRR0010F", "TRR0011A", "TRR0040", "TRR0045" });
            directoryTree.Add("DL005_PAYFROMSAP",
                new string[] { "TRR0020" });
            directoryTree.Add("DL006_DIRECTDEBIT",
                new string[] { "billupdate", "billingStatus" });
            directoryTree.Add("DL007_EXPORT_TO_SAP",
                new string[] { "TRP0010", "TRP0020A", "TRP0020B", "TRP0020C", "TRP0020D",
                    "TRP0020E", "TRP0030A", "TRP0030B", "TRP0030C", "TRP0030D", "TRP0040A" });
            directoryTree.Add("DL008_EXPORT_TO_SAP_BY_CASH_BATCH",
                new string[] { "TRP0010", "TRP0020A", "TRP0020B", "TRP0020C", "TRP0020D",
                    "TRP0020E", "TRP0030A", "TRP0030B", "TRP0030C", "TRP0030D", "TRP0040A" });     

            foreach (string key in directoryTree.Keys)
            {
                string[] subDirs = directoryTree[key];
                foreach (string subDir in subDirs)
                {
                    Directory.CreateDirectory(
                        string.Format(@"{0}{1}\{2}", processPath, key, subDir));
                    Directory.CreateDirectory(
                        string.Format(@"{0}{1}\{2}.Failed", processPath, key, subDir));
                    Directory.CreateDirectory(
                        string.Format(@"{0}{1}\{2}.Succeeded", processPath, key, subDir));
                }
            }
        }

    }
}
