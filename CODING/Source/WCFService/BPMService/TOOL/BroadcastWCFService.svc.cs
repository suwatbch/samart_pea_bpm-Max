using System;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.NewsBroadcast.BS;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;
using WCFExtras.Soap;


namespace BPMService.TOOL
{

    public class BroadcastWCFService : IBroadcastWCFService
    {
        private NewsBroadcastBS _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public BroadcastWCFService()
        {
            _bs = new NewsBroadcastBS();
        }

        public CompressData GetNewsBroadcastNow(DateTime _nowDt, string _userId, int _cmdId)
        {
            return ServiceInspector.Instance.ExceptionTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetNewsBroadcastNow",
                () =>
                {
                    return ServiceHelper.CompressData<List<NewsBroadcastInfo>>(_bs.GetNewsBroadcastNow(_nowDt, _userId, _cmdId));
                });
        }

        public CompressData GetNewsBroadcastHistory(DateTime _nowDt, string _userId, int _cmdId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetNewsBroadcastHistory",
                () =>
                {
                    return ServiceHelper.CompressData<List<NewsBroadcastInfo>>(_bs.GetNewsBroadcastHistory(_nowDt, _userId, _cmdId));
                });
        }

        public CompressData GetNewsBroadcastSent(DateTime _sentDt)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetNewsBroadcastSent",
                () =>
                {
                    return ServiceHelper.CompressData<List<NewsBroadcastSentInfo>>(_bs.GetNewsBroadcastSent(_sentDt));
                });
        }

        public CompressData GetNewsBroadcastNowForSender(DateTime _nowDt, int _cmdId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetNewsBroadcastNowForSender",
                () =>
                {
                    return ServiceHelper.CompressData<List<NewsBroadcastSentInfo>>(_bs.GetNewsBroadcastNowForSender(_nowDt, _cmdId));
                });
        }

        public CompressData GetNewsBroadcastScheduled(DateTime _nowDt, int _cmdId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetNewsBroadcastScheduled",
                () =>
                {
                    return ServiceHelper.CompressData<List<NewsBroadcastSentInfo>>(_bs.GetNewsBroadcastScheduled(_nowDt, _cmdId));
                });
        }

        public CompressData GetNewsBroadcastSentMonthYears(DateTime _sentDt)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetNewsBroadcastSentMonthYears",
                () =>
                {
                    return ServiceHelper.CompressData<List<NewsBroadcastSentInfo>>(_bs.GetNewsBroadcastSentMonthYears(_sentDt));
                });
        }

        public CompressData GetNewsBroadcastUser(int _broadcastId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetNewsBroadcastUser",
                () =>
                {
                    return ServiceHelper.CompressData<List<NewsBroadcastUserInfo>>(_bs.GetNewsBroadcastUser(_broadcastId));
                });
        }

        public void UpdateNewsBroadcastUserOpened(int _broadcastId, string _userId)
        {
            ServiceInspector.Instance.ExceptionTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "UpdateNewsBroadcastUserOpened",
                () =>
                {
                    _bs.UpdateNewsBroadcastUserOpened(_broadcastId, _userId);
                });
        }

        public void UpdateNewsBroadcastUserRead(int _broadcastId, string _userId)
        {
            ServiceInspector.Instance.ExceptionTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "UpdateNewsBroadcastUserRead",
                () =>
                {
                    _bs.UpdateNewsBroadcastUserRead(_broadcastId, _userId);
                });
        }

        public CompressData GetArea()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetArea",
                () =>
                {
                    return ServiceHelper.CompressData<List<AreaInfo>>(_bs.GetArea());
                });
        }

        public CompressData GetBranch(string _areaId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetBranch",
                () =>
                {
                    return ServiceHelper.CompressData<List<BranchInfo>>(_bs.GetBranch(_areaId));
                });
        }

        public CompressData GetUser(string _roleId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetUser",
                () =>
                {
                    return ServiceHelper.CompressData<List<UserInfo>>(_bs.GetUser(_roleId));
                });
        }

        public CompressData GetRole()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetRole",
                () =>
                {
                    return ServiceHelper.CompressData<List<RoleInfo>>(_bs.GetRole());
                });
        }

        public void InsertNewsBroadcast(string _broadcastTopic, string _detail, DateTime _sentDt, DateTime _expireDt, int _cmdId)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "InsertNewsBroadcast",
                () =>
                {
                    _bs.InsertNewsBroadcast(_broadcastTopic, _detail, _sentDt, _expireDt, _cmdId);
                });
        }

        public void InsertNewsBroadcastUser(int _broadcastId, string _userId, string _areaId, string _branchId, string _branchName2, string _roleId, string _roleName)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "InsertNewsBroadcastUser",
                () =>
                {
                    _bs.InsertNewsBroadcastUser(_broadcastId, _userId, _areaId, _branchId, _branchName2, _roleId, _roleName);
                });
        }

        public int GetLastNewsBroadcastId()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "GetLastNewsBroadcastId",
                () =>
                {
                    return _bs.GetLastNewsBroadcastId();
                });
        }

        public bool IsDuplicateUser(int _broadcastId, string _userId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "BroadcastWCFService", "IsDuplicateUser",
                () =>
                {
                    return _bs.IsDuplicateUser(_broadcastId, _userId);
                });
        }

    }
}
