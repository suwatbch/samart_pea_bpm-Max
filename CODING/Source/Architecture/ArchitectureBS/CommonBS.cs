using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureDA;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using System.Configuration;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace PEA.BPM.Architecture.ArchitectureBS
{
    public class CommonBS: ICommonService
    {
        public CommonBS()
        {
        }

        public string ISOCheckPasswordExpried(string userId , int type)
        {
            SecurityDA da = new SecurityDA();
            return da.ISOCheckPasswordExpried(userId,type);
        }

        public string ISOCheckPasswordHistory(string userId, string password)
        {
            SecurityDA da = new SecurityDA();
            return da.ISOCheckPasswordHistory(userId, password);
        }

        public int UpdateUser(string userId, string password, int PwdState, string oldpassword)
        {
            SecurityDA da = new SecurityDA();
            return da.UpdateUser(userId, password, PwdState, oldpassword);
        }

        public string EmpGetEmail(string userId, string url, int timeout)
        {
            return "";
        }

        public string SendEmail(string email, string pw4, string userEmail, string passEmail)
        {
            SecurityDA da = new SecurityDA();
            return da.SendEmail(email,pw4,userEmail,passEmail);
        }

        public string IsAuthenticated(string userId, string hashPwd, string localip)
        {
            SecurityDA da = new SecurityDA();
            return da.IsAuthenticated(userId, hashPwd, Session.Branch.PostedServerId);
        }

        public string IsAuthenticated(string userId, string hashPwd, string localip, string regBranchId)
        {
            SecurityDA da = new SecurityDA();
            return da.IsAuthenticated(userId, hashPwd, regBranchId, Session.Branch.PostedServerId);
        }

        private string OriginalGetToken(string userId, string hashPwd)
        {
            SecurityDA da = new SecurityDA();
            return da.GetAuthenToken(userId, hashPwd);
        }

        public string GetToken(string userId, string hashPwd)
        {
            int state = 0;
            string authenurl = null;
            while (true)
            {
                switch (state)
                {
                    case 0: authenurl = ConfigurationManager.AppSettings["SECURITY_GATEWAY"]; break;
                    case 1: authenurl = ConfigurationManager.AppSettings["SECURITY_GATEWAY2"]; break;
                    case 2: return OriginalGetToken(userId, hashPwd);
                }

                if (!string.IsNullOrEmpty(authenurl))
                {
                    try
                    {
                        AuthenticationWS.AuthenticationWebService authenws = new PEA.BPM.Architecture.ArchitectureBS.AuthenticationWS.AuthenticationWebService();
                        authenws.Url = authenurl + "AuthenticationWebService.asmx";
                        return authenws.GetToken(userId, hashPwd);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
                state++;
            }
        }

        public string CheckToken(string userId, string token)
        {
            string authenurl = null;
            authenurl = ConfigurationManager.AppSettings["SECURITY_GATEWAY"];
            try
            {
                AuthenticationWS.AuthenticationWebService authenws = new PEA.BPM.Architecture.ArchitectureBS.AuthenticationWS.AuthenticationWebService();
                authenws.Url = authenurl + "AuthenticationWebService.asmx";
                return authenws.CheckToken(userId, token);
            }
            catch
            {
                throw;
            }
        }

        public DateTime GetServerTime()
        {
            //June, 24 09 v1.5.0
            //SecurityDA da = new SecurityDA();
            //return da.GetServerTime();
            DateTime dt = DateTime.Now;
            return DateTime.SpecifyKind(dt, DateTimeKind.Unspecified);
        }

        private static BPMVersion _bpmversion = null;

        public BPMVersion GetVersion()
        {
            if (_bpmversion == null)
            {
                lock (typeof(CommonBS))
                {
                    if (_bpmversion == null)
                    {
                        _bpmversion = new BPMVersion();
                        _bpmversion.Version = version.ProductVersion;
                        _bpmversion.ProductName = version.ProductName;
                        _bpmversion.Company = version.CompanyName;
                        _bpmversion.CopyRight = version.ProductCopyRight;

                        #if DEBUG
                        Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                        using (DbConnection dbcon = db.CreateConnection())
                        {
                            _bpmversion.POSDatabase = "[POS: " + dbcon.DataSource + " -> " + dbcon.Database + "]";
                        }
                        #else
                        _bpmversion.POSDatabase = "";
                        #endif
                    }
                }
            }
            return _bpmversion;
        }

        public void SignalSyncup(string batchName)
        {
            //string batchCashSyncUpName = ConfigurationManager.AppSettings["SyncUpBatchName"];
            string destination = ConfigurationManager.AppSettings["Destination"];
            SecurityDA da = new SecurityDA();
            da.EnqueueBatchJob(batchName, destination);
        }

        public void SignalExport(string batchName, string branchId, string modifiedBy)
        {
            //InsertBranchIdForExportingToSAP(branchId, modifiedBy);
            //string batchName = ConfigurationManager.AppSettings["ExportBatchName"];
            string destination = ConfigurationManager.AppSettings["Destination"];
            SecurityDA da = new SecurityDA();
            da.EnqueueBatchJob(batchName, destination, new string[] { branchId });
        }

        public void SignalExportBillBook(string batchName, string billBookId, string modifiedBy)
        {
            string destination = ConfigurationManager.AppSettings["Destination"];
            SecurityDA da = new SecurityDA();
            da.EnqueueBatchJobBillBook(batchName, destination, new string[] { billBookId });
        }

        private void InsertBranchIdForExportingToSAP(string branchId, string modifiedBy)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    SecurityDA da = new SecurityDA();
                    int rows = da.InsertBranchIdForExportingToSAP(trans, branchId, modifiedBy);

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public WorkStatus IsForcedToCloseWork(string workId)
        {
            DbTransaction trans;
            WorkStatus workStatus = null;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    SecurityDA da = new SecurityDA();
                    workStatus = da.IsForcedToCloseWork(trans, workId);

                    trans.Commit();
                    return workStatus;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public RegisterProfile LoadRegisterationInfo(string branchId)
        {
            SecurityDA da = new SecurityDA();
            return da.LoadRegisterationInfo(branchId);
        }




        #region ICommonService Members

        //TAG 2.0.5 Rev.1 Update VersionNo.
        // 2.0.7.4 add paramter userId
        public void UpdateBPMClientVersion(string versionNo, string terminalId, string userId)
        {
            try
            {
                SecurityDA da = new SecurityDA();
                da.UpdateBPMClientVersion(versionNo, terminalId, userId); // 2.0.7.4 add paramter userId to DA class
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
