using System;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;


namespace BPMService.ARCH
{

    public class CommonWCFService : ICommonWCFService
    {
        private CommonBS _bs;

        public CommonWCFService()
        {
            _bs = new CommonBS();
        }

        public string IsAuthenticated(string userId, string hashPwd, string localip)
        {
            AuthInfo authinfo = new AuthInfo();
            authinfo.UserId = userId;
            authinfo.LocalIP = localip;
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, authinfo, "CommonWCFService", "IsAuthenticated",
                () =>
                {
                    return _bs.IsAuthenticated(userId, hashPwd, localip);
                });
        }

        public string IsAuthenticatedInBranch(string userId, string hashPwd, string localip, string regBranchId)
        {
            AuthInfo authinfo = new AuthInfo();
            authinfo.UserId = userId;
            authinfo.LocalIP = localip;
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, authinfo, "CommonWCFService", "IsAuthenticatedInBranch",
                () =>
                {
                    return _bs.IsAuthenticated(userId, hashPwd, localip, regBranchId);
                });
        }

        public string GetToken(string userId, string hashPwd)
        {
            AuthInfo authinfo = new AuthInfo();
            authinfo.UserId = userId;
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, authinfo, "CommonWCFService", "GetToken",
                () =>
                {
                    return _bs.GetToken(userId, hashPwd);
                });
        }

        public DateTime GetServerTime()
        {
            return _bs.GetServerTime();
        }

        public BPMVersion GetVersion()
        {
            return ServiceInspector.Instance.ExceptionTrail(EErrorInModule.Architecture, null, "CommonWCFService", "GetVersion",
                () =>
                {
                    return _bs.GetVersion();
                });
        }

        public void SignalExport(string batchName, string branchId, string modifiedBy)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, null, "CommonWCFService", "SignalExport",
                () =>
                {
                    _bs.SignalExport(batchName, branchId, modifiedBy);
                });
        }

        public void SignalExportBillBook(string batchName, string billBookId, string modifiedBy)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, null, "CommonWCFService", "SignalExport",
                () =>
                {
                    _bs.SignalExportBillBook(batchName, billBookId, modifiedBy);
                });
        }

        public void SignalSyncup(string batchName)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, null, "CommonWCFService", "SignalSyncup",
                () =>
                {
                    _bs.SignalSyncup(batchName);
                });
        }

        public WorkStatus IsForcedToCloseWork(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, null, "CommonWCFService", "IsForcedToCloseWork",
                () =>
                {
                    return _bs.IsForcedToCloseWork(workId);
                });
        }

        public RegisterProfile LoadRegisterationInfo(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, null, "CommonWCFService", "LoadRegisterationInfo",
                () =>
                {
                    return _bs.LoadRegisterationInfo(branchId);
                });
        }

        //////////// DCR 67-020 : StrongPassword ////////////
        public string ISOCheckPasswordExpried(string userId, int type)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, null, "CommonWCFService", "ISOCheckPasswordExpried",
                () =>
                {
                    return _bs.ISOCheckPasswordExpried(userId,type);
                });
        }

        public string ISOCheckPasswordHistory(string userId, string password)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, null, "CommonWCFService", "ISOCheckPasswordHistory",
                () =>
                {
                    return _bs.ISOCheckPasswordHistory(userId, password);
                });
        }

        public int UpdateUser(string userId, string password, int PwdState, string oldpassword)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, null, "CommonWCFService", "UpdateUser",
                () =>
                {
                    return _bs.UpdateUser(userId, password, PwdState, oldpassword);
                });
        }

        public string SendEmail(string email, string pw4, string userEmail, string passEmail)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, null, "CommonWCFService", "SendEmail",
                () =>
                {
                    return _bs.SendEmail(email,pw4,userEmail,passEmail);
                });
        }
        //////////// DCR 67-020 : StrongPassword ////////////

        #region ICommonWCFService Members

        //TAG 2.0.5 Rev.1 Update VersionNo.
        // 
        public void UpdateBPMClientVersion(string versionNo, string terminalId,string userId)
        {
            try
            {
                _bs.UpdateBPMClientVersion(versionNo, terminalId,userId);
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
