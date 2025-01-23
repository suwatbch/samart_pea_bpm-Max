using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using System;


namespace BPMService.ARCH
{

    public class SecurityWCFService : ISecurityWCFService
    {
        private ISecurityService _bs;

        public SecurityWCFService()
        {
            _bs = new SecurityBS();
        }

        public CompressData ListAuthorizedFunctions(string userId)
        {
            AuthInfo authinfo = new AuthInfo();
            authinfo.UserId = userId;
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, authinfo, "SecurityWCFService", "ListAuthorizedFunctions",
                () =>
                {
                    return ServiceHelper.CompressData<List<Function>>(_bs.ListAuthorizedFunctions(userId));
                });            
        }

        public void SaveUnlockLog(UnlockLog unlockLog)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, null, "SecurityWCFService", "SaveUnlockLog",
                () =>
                {
                    _bs.SaveUnlockLog(null, unlockLog);
                });
        }

        public UserProfileExt LoadUserProfile(string userId)
        {
            AuthInfo authinfo = new AuthInfo();
            authinfo.UserId = userId;
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, authinfo, "SecurityWCFService", "LoadUserProfile",
                () =>
                {
                    return _bs.LoadUserProfile(userId);
                });            
        }

        public string CheckLogInDouble(string UserId, string TerminalIP, int latency, int retrycount)
        {
            AuthInfo authinfo = new AuthInfo();
            authinfo.UserId = UserId;
            authinfo.LocalIP = TerminalIP;
            return ServiceInspector.Instance.ExceptionTrail(EErrorInModule.Architecture, authinfo, "SecurityWCFService", "CheckLogInDouble",
                () =>
                {
                    return _bs.CheckLogInDouble(UserId, TerminalIP, latency, retrycount);
                });
        }

        public string UpdateCurIPReqFlag(string UserId, string TerminalIP, string ReqFlag)
        {
            AuthInfo authinfo = new AuthInfo();
            authinfo.UserId = UserId;
            authinfo.LocalIP = TerminalIP;
            return ServiceInspector.Instance.ExceptionTrail(EErrorInModule.Architecture, authinfo, "SecurityWCFService", "UpdateCurIPReqFlag",
                () =>
                {
                    return _bs.UpdateCurIPReqFlag(UserId, TerminalIP, ReqFlag);
                });
        }

    }
}
