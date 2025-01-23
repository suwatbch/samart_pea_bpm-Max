using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using Avanade.ACA.Batch;
using Avanade.ACA.Batch.Configuration;
using System.Net.Mail;
using System.Net;

namespace PEA.BPM.Architecture.ArchitectureDA
{
    public class SecurityDA
    {
        private const int CMD_TIMEOUT = 120; //2 minutes

        public string IsAuthenticated(string userId, string hashPassword, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_CheckAuthen");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "HashPassword", DbType.String, hashPassword);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);

            return db.ExecuteScalar(cmd).ToString();      
        }

        public string IsAuthenticated(string userId, string hashPassword, string regBranchId, string postedServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_CheckAuthen");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "HashPassword", DbType.String, hashPassword);
            db.AddInParameter(cmd, "RegBranchId", DbType.String, regBranchId);
            db.AddInParameter(cmd, "PostedServerId", DbType.String, postedServerId);

            return db.ExecuteScalar(cmd).ToString();
        }

        public string GetAuthenToken(string userId, string hashPassword)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_AuthenToken");
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "HashPassword", DbType.String, hashPassword);
            
            return db.ExecuteScalar(cmd).ToString();
        }

        public string CheckToken(string userId, string token)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_CheckToken");
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            db.AddInParameter(cmd, "Token", DbType.String, token);

            return db.ExecuteScalar(cmd).ToString();
        }

        public UserProfileExt LoadUserProfile(string userId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_UserProfile");
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                DataRow dr = dt.Rows[0];

                UserProfileExt upExt = new UserProfileExt();
                UserProfile up = new UserProfile();
                up.UserId = userId;
                up.Name = DaHelper.GetString(dr, "FullName");
                up.Department = DaHelper.GetString(dr, "Department");
                up.LoginTime = DaHelper.GetDate(dr, "LoginTime");
                upExt.ScopeId = DaHelper.GetString(dr, "ScopeId");
                upExt.UserProfile = up;
                return upExt;
            }
        }

        public List<Function> GetAuthorizedFunctions(string userId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_sel_AuthorizedFunctions");
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<Function> fcs = new List<Function>();

            foreach (DataRow dr in dt.Rows)
            {
                Function f = new Function();
                f.Id = DaHelper.GetString(dr, "FncId");
                f.Authorized = DaHelper.GetString(dr, "Authorized") == "1";
                f.Unlockable = DaHelper.GetString(dr, "UnlockableFlag") == "1";
                f.UnlockRemark = DaHelper.GetString(dr, "UnlockRemarkFlag") == "1";

                fcs.Add(f);
            }

            return fcs;
        }

        public List<Service> GetAuthorizedServices(string userId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_sel_AuthorizedServices");
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<Service> svs = new List<Service>();

            foreach (DataRow dr in dt.Rows)
            {
                Service s = new Service();
                s.Id = DaHelper.GetString(dr, "SvcId");
                s.Name = string.Format("{0}.{1}", DaHelper.GetString(dr, "SvcName"), DaHelper.GetString(dr, "MethodName"));
                svs.Add(s);
            }

            return svs;
        }

        public string InsertUnlockLog(DbTransaction trans, UnlockLog ul)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_ins_UnlockLog");
            db.AddInParameter(cmd, "BranchId", DbType.String, ul.BranchId);
            db.AddInParameter(cmd, "BranchName", DbType.String, ul.BranchName);
            db.AddInParameter(cmd, "FncId", DbType.String, ul.FunctionId);
            db.AddInParameter(cmd, "CurrentUserId", DbType.String, ul.CurrentUserId);
            db.AddInParameter(cmd, "UnlockUserId", DbType.String, ul.UnlockUserId);
            db.AddInParameter(cmd, "Description", DbType.String, ul.Description);
            db.AddInParameter(cmd, "Remark", DbType.String, ul.Remark);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, ul.CurrentUserId);

            return db.ExecuteScalar(cmd, trans).ToString();
        }

        //Use app server time
        //public DateTime GetServerTime()
        //{
        //    Database db = DatabaseFactory.CreateDatabase("POSDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("ta_get_ServerTime");

        //    return (DateTime)db.ExecuteScalar(cmd);
        //}

        public string CheckLogInDouble(string UserId, string TerminalIP)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_CheckDoubleLogIn");
            db.AddInParameter(cmd, "UserId", DbType.String, UserId);
            db.AddInParameter(cmd, "TerminalIP", DbType.String, TerminalIP);

            return db.ExecuteScalar(cmd).ToString();
        }

        public bool IsActiveIP(string IP) {
            char[] seperator = new char[] { '/' };
            string[] tmp = IP.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            IP = "";
            if (tmp.Length > 1)
            {
                for (int i = 0; i < tmp.Length; i++)
                {
                    IP += "'" + tmp[i] + "',";
                }
                IP = IP.TrimEnd(',');
            }
            else
                IP = "'" + tmp[0] + "'";

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetSqlStringCommand("select RIID from RegisteredIP where Active =1 and IP in (" + IP + ")");
            if (db.ExecuteScalar(cmd) == null)
                return false;
            return true;
        }

        public string UpdateCurIPReqFlag(string UserId, string TerminalIP, string ReqFlag)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_upd_CurIPReqFlag");
            db.AddInParameter(cmd, "UserId", DbType.String, UserId);
            db.AddInParameter(cmd, "TerminalIP", DbType.String, TerminalIP);
            db.AddInParameter(cmd, "ReqFlag", DbType.String, ReqFlag);

            int result = db.ExecuteNonQuery(cmd);
            return result.ToString();
        }

        public void EnqueueBatchJob(string batchName, string destination)
        {
            BatchExecutionRequest request = new BatchExecutionRequest(batchName);
            request.BatchClientName = "";
            request.Destination = destination;
            request.StartPollingForResultAfter = TimeSpan.FromMinutes(5);
            BatchExecutionRequestQueue queue = new BatchExecutionRequestQueue("ACABatch_SQL");
            queue.Enqueue(request);
        }

        public void EnqueueBatchJob(string batchName, string destination, string[] param)
        {
            BatchExecutionRequest request = new BatchExecutionRequest(batchName);
            request.BatchClientName = "";
            request.Destination = destination;
            request.StartPollingForResultAfter = TimeSpan.FromMinutes(5);

            ParameterData parem = new ParameterData();
            parem.Name = "BranchId";
            parem.Value = param[0];
            request.Parameters.Add(parem);

                

            BatchExecutionRequestQueue queue = new BatchExecutionRequestQueue("ACABatch_SQL");
            queue.Enqueue(request);
        }

        public void EnqueueBatchJobBillBook(string batchName, string destination, string[] param)
        {
            BatchExecutionRequest request = new BatchExecutionRequest(batchName);
            request.BatchClientName = "";
            request.Destination = destination;
            request.StartPollingForResultAfter = TimeSpan.FromMinutes(5);

            ParameterData parem = new ParameterData();
            parem.Name = "BillBookId";
            parem.Value = param[0];
            request.Parameters.Add(parem);



            BatchExecutionRequestQueue queue = new BatchExecutionRequestQueue("ACABatch_SQL");
            queue.Enqueue(request);
        }


        public int InsertBranchIdForExportingToSAP(DbTransaction trans, string branchId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("ACABatch_SQL");
            DbCommand cmd = db.GetStoredProcCommand("ACA_INT_UpdBranchIdForExportingData");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            return db.ExecuteNonQuery(cmd, trans);
        }

        public WorkStatus IsForcedToCloseWork(DbTransaction trans, string workId)
        {
            WorkStatus ci = new WorkStatus();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_get_IsForcedToCloseWork");
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);

            //expect only one row
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                ci.CloseWorkBy = DaHelper.GetString(dr, "CloseWorkBy");
                ci.CashierName = DaHelper.GetString(dr, "FullName");
            }

            return ci;
        }

        public RegisterProfile LoadRegisterationInfo(string branchId)
        {
            RegisterProfile registerInfo = new RegisterProfile();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_RegisterInfo");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);

            //expect only one row
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                registerInfo.BranchId = DaHelper.GetString(dr, "BranchId");
                registerInfo.BranchName = DaHelper.GetString(dr, "BranchName");
                registerInfo.BranchName2 = DaHelper.GetString(dr, "BranchName2");
                registerInfo.BranchLevel = DaHelper.GetString(dr, "BranchLevel");
                registerInfo.BranchNo = DaHelper.GetString(dr, "BranchNo");
                registerInfo.Address = DaHelper.GetString(dr, "Address");
                registerInfo.BusinessArea = DaHelper.GetString(dr, "BusinessArea");
                registerInfo.PeaTaxCode = DaHelper.GetString(dr, "TaxCode");
                registerInfo.TransUri = DaHelper.GetString(dr, "TransUri");
                registerInfo.ReportUri = DaHelper.GetString(dr, "ReportUri");
                registerInfo.BackupUri = DaHelper.GetString(dr, "BackupUri");
            }

            return registerInfo;
        }


        //TAG 2.0.5 Rev.1 Update VersionNo.
        // 2.0.7.4 Add parameter userId
        public void UpdateBPMClientVersion(string versionNo, string terminalId, string userId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_ins_BPMClientVersion");

                db.AddInParameter(cmd, "versionNo", DbType.String, versionNo);
                db.AddInParameter(cmd, "terminalId", DbType.String, terminalId);
                db.AddInParameter(cmd, "userId", DbType.String, userId); // 2.0.7.4 add paramter userId to SQL server.

                db.ExecuteScalar(cmd);
            }
            catch (Exception)
            {
            }
          
        }

        public string ISOCheckPasswordExpried(string userId, int type)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_chk_passwordexpried");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "UserId", DbType.String, userId);
                db.AddInParameter(cmd, "Type", DbType.Int32, type);
                return db.ExecuteScalar(cmd).ToString();
            }
            catch (Exception)
            {
            }
            return "";
        }

        public string ISOCheckPasswordHistory(string userId, string password)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_chk_passwordhistory");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "UserId", DbType.String, userId);
                db.AddInParameter(cmd, "Password", DbType.String, password);
                return db.ExecuteScalar(cmd).ToString();
            }
            catch (Exception)
            {
            }
            return "";
        }

        public int UpdateUser(string userId, string password, int PwdState, string oldpassword)
        {
            int retVal = 0;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_upd_ChangePassword");
                db.AddOutParameter(cmd, "RetId", DbType.Int32, 0);
                db.AddInParameter(cmd, "RTId", DbType.Int32, 0);
                db.AddInParameter(cmd, "UserId", DbType.String, userId);
                db.AddInParameter(cmd, "RoleId", DbType.String, "");
                db.AddInParameter(cmd, "NewRoleId", DbType.String, "");
                db.AddInParameter(cmd, "NewPassword", DbType.String, password);
                db.AddInParameter(cmd, "IsPwdChanged", DbType.Int32, PwdState);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, userId);
                db.AddInParameter(cmd, "ModifiedByPwd", DbType.String, oldpassword);
                db.ExecuteNonQuery(cmd);
                retVal = (int)db.GetParameterValue(cmd, "RetId"); // 1 = success
            }
            catch (Exception)
            {
                throw;
            }

            return retVal;
        }

        public string SendEmail(string email, string pw4, string userEmail, string passEmail)
        {
            try
            {
                // กำหนดข้อมูลผู้ส่งและผู้รับ
                string fromEmail = userEmail;
                string toEmail = email;
                string subject = "BPM Password Reset " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                string body = "หมายเลขยืนยัน : " + pw4;
                string smtpPassword = passEmail;

                // สร้าง MailMessage object
                MailMessage mail = new MailMessage(fromEmail, toEmail);
                mail.Subject = subject;
                mail.Body = body;
                // กำหนด SMTP server
                SmtpClient smtpServer = new SmtpClient("email.pea.co.th");
                smtpServer.Port = 587;
                smtpServer.EnableSsl = false;
                smtpServer.Credentials = new NetworkCredential(fromEmail, smtpPassword);
                // ส่งอีเมล
                smtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                return "Fail " + ex.ToString();
            }
            return "";
        }

    }
}
