using System;
using System.Collections.Generic;
using System.ComponentModel;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ToolModule.BS;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.TOOL
{

    public class AzManWCFService : IAzManWCFService
    {
        private AzManBS _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public AzManWCFService()
        {
            _bs = new AzManBS();
        }

        public BindingList<Role> ListRoles()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListRoles",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ListRoles();
                });
        }

        public CompressData ListRoles_Compress()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListRoles_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<Role>>(_bs.ListRoles());
                });
        }

        public BindingList<Role> ListExpRoles(User user)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListExpRoles",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ListExpRoles(user);
                });
        }

        public CompressData ListExpRoles_Compress(User user)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListExpRoles_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<Role>>(_bs.ListExpRoles(user));
                });
        }

        public BindingList<Role> ListRolesByUser(string userId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListRolesByUser",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ListRolesByUser(userId);
                });
        }

        public CompressData ListRolesByUser_Compress(string userId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListRolesByUser_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<Role>>(_bs.ListRolesByUser(userId));
                });
        }

        public BindingList<User> ListUsers(string filter)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListUsers",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ListUsers(filter);
                });
        }

        public CompressData ListUsers_Compress(string filter)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListUsers_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<User>>(_bs.ListUsers(filter));
                });
        }

        public BindingList<User> ListUsersByRole(string roleId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListUsersByRole",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ListUsersByRole(roleId);
                });
        }

        public CompressData ListUsersByRole_Compress(string roleId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListUsersByRole_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<User>>(_bs.ListUsersByRole(roleId));
                });
        }

        public BindingList<Function> ListFunctions(string roleId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListFunctions",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ListFunctions(roleId);
                });
        }

        public CompressData ListFunctions_Compress(string roleId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListFunctions_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<Function>>(_bs.ListFunctions(roleId));
                });
        }

        public List<Function> ListAllFunctions()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListAllFunctions",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ListAllFunctions();
                });
        }


        public CompressData ListAllFunctions_Compress()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListAllFunctions_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Function>>(_bs.ListAllFunctions());
                });
        }

        #region

        public BindingList<User> SearchUsers(string keyword)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "SearchUsers",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.SearchUsers(keyword);
                });
        }

        public CompressData SearchUsers_Compress(string keyword)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "SearchUsers_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<User>>(_bs.SearchUsers(keyword));
                });
        }

        public void CreateUser(User user)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "CreateUser",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.CreateUser(user);
                });
        }

        public void EditUser(User user)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "EditUser",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.EditUser(user);
                });
        }

        public void DeleteUser(User user)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "DeleteUser",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.DeleteUser(user);
                });
        }

        public void CreateRole(Role role)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "CreateRole",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.CreateRole(role);
                });
        }

        public void EditRole(Role role)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "EditRole",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.EditRole(role);
                });
        }

        public void DeleteRole(Role role)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "DeleteRole",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.DeleteRole(role);
                });
        }

        public void AddRoleUser(User user, Role role)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "AddRoleUser",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.AddRoleUser(user, role);
                });
        }

        public void RemoveRoleUser(User user, Role role)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "RemoveRoleUser",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.RemoveRoleUser(user, role);
                });
        }

        public List<User> ListEmployee(string filter)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListEmployee",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ListEmployee(filter);
                });
        }

        public CompressData ListEmployee_Compress(string filter)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListEmployee_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<User>>(_bs.ListEmployee(filter));
                });
        }

        public bool ValidateUserScopeByUser(User user)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ValidateUserScopeByUser",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ValidateUserScopeByUser(user);
                });
        }

        public bool ValidateUserScopeByRole(Role role)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ValidateUserScopeByRole",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ValidateUserScopeByRole(role);
                });
        }

        public bool ValidateUserScope(User user, Role role)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ValidateUserScope",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ValidateUserScope(user, role);
                });
        }

        public int UpdateUser(User user)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "UpdateUser",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.UpdateUser(null, user);
                });
        }

        public int UpdateUser_Compress(CompressData user)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "UpdateUser_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.UpdateUser(null, ServiceHelper.DecompressData<User>(user));
                });
        }

        public List<PeaInfo> SearchBranchByKeyword(string keyword, string userId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "SearchBranchByKeyword",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.SearchBranchByKeyword(keyword, userId);
                });
        }

        public CompressData SearchBranchByKeyword_Compress(string keyword, string userId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "SearchBranchByKeyword_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PeaInfo>>(_bs.SearchBranchByKeyword(keyword, userId));
                });
        }

        public string RegisterTerminal(Terminal terminal, out string terminalCode)
        {
            string retterminalcode = "";
            string res = ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "RegisterTerminal",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.RegisterTerminal(null, terminal, out retterminalcode);
                });
            terminalCode = retterminalcode;
            return res;
        }

        public string RegisterTerminal_Compress(out string terminalCode, CompressData terminal)
        {
            string retterminalcode = "";
            string res = ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "RegisterTerminal_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.RegisterTerminal(null, ServiceHelper.DecompressData<Terminal>(terminal), out retterminalcode);
                });
            terminalCode = retterminalcode;
            return res;
        }

        public void UpdateTerminal(Terminal terminal)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "UpdateTerminal",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.UpdateTerminal(terminal);
                });
        }

        public CompressData ListRemarkFunctions()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListRemarkFunctions",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Function>>(_bs.ListRemarkFunctions());
                });
        }

        public CompressData ListRemark(string fncId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListRemark",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<UnlockRemark>>(_bs.ListRemark(fncId));
                });
        }

        public int AddRemark_Compress(CompressData rmk)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "AddRemark_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.AddRemark(ServiceHelper.DecompressData<UnlockRemark>(rmk));
                });
        }

        public int EditRemark_Compress(CompressData rmk)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "EditRemark_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.EditRemark(ServiceHelper.DecompressData<UnlockRemark>(rmk));
                });
        }

        public int DeleteRemark_Compress(CompressData rmk)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "DeleteRemark_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.DeleteRemark(ServiceHelper.DecompressData<UnlockRemark>(rmk));
                });
        }

        public CompressData ListUnlockableFunctions()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListUnlockableFunctions",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Function>>(_bs.ListUnlockableFunctions());
                });
        }

        public CompressData GetUnlockingLogReport(UnlockingLogParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "GetUnlockingLogReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<ReportUnlockingLog>>(_bs.GetUnlockingLogReport(param));
                });
        }

        public DateTime GetWSServerTime(string WSUrl)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "GetWSServerTime",
                () =>
                {
                    return _bs.GetWSServerTime(WSUrl);
                });
        }

        public DateTime GetDBServerTime(string WSUrl)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "GetDBServerTime",
                () =>
                {
                    return _bs.GetDBServerTime(WSUrl);
                });
        }

        #endregion

        #region IAzManWCFService Members

        //Added by Uthen.P #Issue User Limit 23-1-2558     

        public BindingList<UserExceed> ListUserLimitExceeds()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListUserLimitExceeds",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ListUserLimitExceeds();
                });
        }

        public CompressData ListUserLimitExceeds_Compress()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Tools, authInfo, "AzManWCFService", "ListUserLimitExceeds_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BindingList<UserExceed>>(_bs.ListUserLimitExceeds());
                });
        }

        #endregion
    }
}
