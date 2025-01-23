using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using System.Data.Common;
using System.ComponentModel;

namespace PEA.BPM.ToolModule.Interface.Services
{
    public interface IAzManService
    {
        //permission management
        BindingList<Role> ListRoles();
        BindingList<Role> ListExpRoles(User user);
        BindingList<User> ListUsers(string filter);
        BindingList<Role> ListRolesByUser(string userId);
        BindingList<User> ListUsersByRole(string roleId);
        BindingList<Function> ListFunctions(string roleId);
        BindingList<User> SearchUsers(string keyword);
        void CreateUser(User user);
        void EditUser(User user);
        void DeleteUser(User user);
        void CreateRole(Role role);
        void EditRole(Role role);
        void DeleteRole(Role role);
        void AddRoleUser(User user, Role role);
        void RemoveRoleUser(User user, Role role);
        List<User> ListEmployee(string filter);
        List<Function> ListAllFunctions();
        bool ValidateUserScopeByUser(User user);
        bool ValidateUserScopeByRole(Role role);
        bool ValidateUserScope(User user, Role role);
        //personal change password
        int UpdateUser(DbTransaction trans, User user);
        List<PeaInfo> SearchBranchByKeyword(string keyword, string userId);
        string RegisterTerminal(DbTransaction trans, Terminal terminal, out string terminalCode);
        void UpdateTerminal(Terminal terminal);
        List<ReportUnlockingLog> GetUnlockingLogReport(UnlockingLogParam param);
        BindingList<UnlockRemark> ListRemark(string FunctionId);
        List<Function> ListRemarkFunctions();
        int AddRemark(UnlockRemark rmk);
        int EditRemark(UnlockRemark rmk);
        int DeleteRemark(UnlockRemark rmk);
        List<Function> ListUnlockableFunctions();

        DateTime GetDBServerTime(string WSUrl);
        DateTime GetWSServerTime(string WSUrl);

        BindingList<UserExceed> ListUserLimitExceeds();
    }

}
