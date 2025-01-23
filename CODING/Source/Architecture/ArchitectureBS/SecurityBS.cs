using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureDA;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.Architecture.ArchitectureBS
{
    public class SecurityBS : ISecurityService
    {
        private string OriginalCheckToken(string userId, string token)
        {
            SecurityDA da = new SecurityDA();
            return da.CheckToken(userId, token);
        }
        public string CheckToken(string userId, string token)
        {
            int state = 0;
            string authenurl = null;
            while (true)
            {
                switch (state)
                {
                    case 0: authenurl = ConfigurationManager.AppSettings["SECURITY_GATEWAY"]; break;
                    case 1: authenurl = ConfigurationManager.AppSettings["SECURITY_GATEWAY2"]; break;
                    case 2: return OriginalCheckToken(userId, token);
                }

                if (!string.IsNullOrEmpty(authenurl))
                {
                    try
                    {
                        AuthenticationWS.AuthenticationWebService authenws = new AuthenticationWS.AuthenticationWebService();
                        authenws.Url = authenurl + "AuthenticationWebService.asmx";
                        return authenws.CheckToken(userId, token);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
                state++;
            }
        }

        private string OriginalCheckLogInDouble(string UserId, string TerminalIP)
        {
            SecurityDA da = new SecurityDA();
            return da.CheckLogInDouble(UserId, TerminalIP);
        }
        public string CheckLogInDouble(string UserId, string TerminalIP, int latency, int retrycount)
        {
            int state = 0;
            string authenurl = null;
            while (true)
            {
                switch (state)
                {
                    case 0: authenurl = ConfigurationManager.AppSettings["SECURITY_GATEWAY"]; break;
                    case 1: authenurl = ConfigurationManager.AppSettings["SECURITY_GATEWAY2"]; break;
                    case 2: return OriginalCheckLogInDouble(UserId, TerminalIP);
                }

                if (!string.IsNullOrEmpty(authenurl))
                {
                    try
                    {
                        AuthenticationWS.AuthenticationWebService authenws = new AuthenticationWS.AuthenticationWebService();
                        authenws.Url = authenurl + "AuthenticationWebService.asmx";
                        return authenws.CheckLogInDouble(UserId, TerminalIP, latency, retrycount);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
                state++;
            }
        }

        private string OriginalUpdateCurIPReqFlag(string UserId, string TerminalIP, string ReqFlag)
        {
            SecurityDA da = new SecurityDA();
            return da.UpdateCurIPReqFlag(UserId, TerminalIP, ReqFlag);
        }
        public string UpdateCurIPReqFlag(string UserId, string TerminalIP, string ReqFlag)
        {
            int state = 0;
            string authenurl = null;
            while (true)
            {
                switch (state)
                {
                    case 0: authenurl = ConfigurationManager.AppSettings["SECURITY_GATEWAY"]; break;
                    case 1: authenurl = ConfigurationManager.AppSettings["SECURITY_GATEWAY2"]; break;
                    case 2: return OriginalUpdateCurIPReqFlag(UserId, TerminalIP, ReqFlag);
                }

                if (!string.IsNullOrEmpty(authenurl))
                {
                    try
                    {
                        AuthenticationWS.AuthenticationWebService authenws = new AuthenticationWS.AuthenticationWebService();
                        authenws.Url = authenurl + "AuthenticationWebService.asmx";
                        return authenws.UpdateCurIPReqFlag(UserId, TerminalIP, ReqFlag);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
                state++;
            }
        }

        public UserProfileExt LoadUserProfile(string userId)
        {
            SecurityDA da = new SecurityDA();
            return da.LoadUserProfile(userId);
        }

        public List<Function> ListAuthorizedFunctions(string userId)
        {
            SecurityDA da = new SecurityDA();
            return da.GetAuthorizedFunctions(userId);
        }
        
        public List<Service> ListAuthorizedServices(string userId)
        {
            SecurityDA da = new SecurityDA();
            return da.GetAuthorizedServices(userId);
        }
        
        public void SaveUnlockLog(DbTransaction trans, UnlockLog unlockLog)
        {
            // If it's called from UI or SI component, transaction will be null
            if (null == trans)
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    try
                    {
                        SecurityDA da = new SecurityDA();
                        da.InsertUnlockLog(trans, unlockLog);

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        // Write Log
                        // ...
                        //
                        throw;
                    }
                }
            }
            else // If it's called from BS component, transaction should be beginned
            {
                SecurityDA da = new SecurityDA();
                da.InsertUnlockLog(trans, unlockLog);
            }
        }        
    }
}
