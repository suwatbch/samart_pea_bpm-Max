using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ToolModule.Interface.Services;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using System.Threading;
using System.Collections;
using PEA.BPM.ToolModule.SG;
using System.Data.Common;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.ToolModule.Services
{
    public class AzManService: IAzManService
    {
        public AzManService()
		{

        }

        #region Service Factory
        public IAzManService GetService()
        {
            return GetService(false);
        }

        public IAzManService GetService(bool serverService)
        {
            try
            {
                if (serverService || Session.Branch.OnlineConnection)
                    return new AzManSG(true);
                else
                    return new AzManSG(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IAzManService Members

        public string RegisterTerminal(DbTransaction trans, Terminal terminal, out string terminalCode)
        {
           IAzManService bs = GetService(true);
           return bs.RegisterTerminal(null, terminal, out terminalCode);
        }

        public void UpdateTerminal(Terminal terminal)
        {
            IAzManService bs = GetService(true);
            bs.UpdateTerminal(terminal);
        }

        public BindingList<Role> ListRoles()
        {
            IAzManService bs = GetService(true);
            return bs.ListRoles();
        }

        public BindingList<Role> ListExpRoles(User user)
        {
            IAzManService bs = GetService(true);
            return bs.ListExpRoles(user);
        }

        public BindingList<Role> ListRolesByUser(string userId)
        {
            IAzManService bs = GetService(true);
            return bs.ListRolesByUser(userId);
        }

        public BindingList<User> ListUsers(string filter)
        {
            IAzManService bs = GetService(true);
            return bs.ListUsers(filter);
        }

        public BindingList<User> ListUsersByRole(string roleId)
        {
            IAzManService bs = GetService(true);
            return bs.ListUsersByRole(roleId);
        }

        public BindingList<Function> ListFunctions(string roleId)
        {
            IAzManService bs = GetService(true);
            return bs.ListFunctions(roleId);
        }

        public BindingList<User> SearchUsers(string keyword)
        {
            IAzManService bs = GetService(true);
            return bs.SearchUsers(keyword); 
        }      

        public void CreateUser(User user)
        {
            IAzManService bs = GetService(true);
            bs.CreateUser(user);
        }

        public void EditUser(User user)
        {
            IAzManService bs = GetService(true);
            bs.EditUser(user);
        }

        public void DeleteUser(User user)
        {
            IAzManService bs = GetService(true);
            bs.DeleteUser(user);
        }

        public void CreateRole(Role role)
        {
            IAzManService bs = GetService(true);
            bs.CreateRole(role);
        }

        public void EditRole(Role role)
        {
            IAzManService bs = GetService(true);
            bs.EditRole(role);
        }

        public void DeleteRole(Role role)
        {
            IAzManService bs = GetService(true);
            bs.DeleteRole(role);
        }

        public void AddRoleUser(User user, Role role)
        {
            IAzManService bs = GetService(true);
            bs.AddRoleUser(user, role);
        }

        public void RemoveRoleUser(User user, Role role)
        {
            IAzManService bs = GetService(true);
            bs.RemoveRoleUser(user, role);
        }

        public List<User> ListEmployee(string filter)
        {
            IAzManService bs = GetService(true);
            return bs.ListEmployee(filter);
        }

        public List<Function> ListAllFunctions()
        {
            IAzManService bs = GetService(true);
            return bs.ListAllFunctions();
        }

        public bool ValidateUserScopeByUser(User user)
        {
            IAzManService bs = GetService(true);
            return bs.ValidateUserScopeByUser(user);
        }

        public bool ValidateUserScopeByRole(Role role)
        {
            IAzManService bs = GetService(true);
            return bs.ValidateUserScopeByRole(role);
        }

        public bool ValidateUserScope(User user, Role role)
        {
            IAzManService bs = GetService(true);
            return bs.ValidateUserScope(user, role);
        }

        public int UpdateUser(DbTransaction trans, User user)
        {
            IAzManService bs = GetService(true);
            return bs.UpdateUser(trans, user);
        }

        public List<PeaInfo> SearchBranchByKeyword(string keyword, string userId)
        {
            IAzManService bs = GetService(true);
            return bs.SearchBranchByKeyword(keyword, userId);
        }

        public List<ReportUnlockingLog> GetUnlockingLogReport(UnlockingLogParam param)
        {
            IAzManService bs = GetService(true);
            return bs.GetUnlockingLogReport(param);
        }

        public List<Function> ListRemarkFunctions()
        {
            IAzManService bs = GetService(true);
            return bs.ListRemarkFunctions();
        }

        public BindingList<UnlockRemark> ListRemark(string fncId)
        {
            IAzManService bs = GetService(true);
            return bs.ListRemark(fncId);
        }

        public int AddRemark(UnlockRemark rmk)
        {
            IAzManService bs = GetService(true);
            return bs.AddRemark(rmk);
        }

        public int EditRemark(UnlockRemark rmk)
        {
            IAzManService bs = GetService(true);
            return bs.EditRemark(rmk);
        }

        public int DeleteRemark(UnlockRemark rmk)
        {
            IAzManService bs = GetService();
            return bs.DeleteRemark(rmk);
        }

        public List<Function> ListUnlockableFunctions()
        {
            IAzManService bs = GetService(true);
            return bs.ListUnlockableFunctions();
        }

        public DateTime GetWSServerTime(string WSUrl)
        {
            IAzManService bs = GetService(true);
            return bs.GetWSServerTime(WSUrl);
        }


        public DateTime GetDBServerTime(string WSUrl)
        {
            IAzManService bs = GetService(true);
            return bs.GetDBServerTime(WSUrl);
        }


        #endregion

        #region #UserLimit Issue
        //Added by Uthen.P #UserLimit Issue 23-1-2558 2nd Checked in

        public BindingList<UserExceed> ListUserLimitExceeds()
        {
            IAzManService bs = GetService(true);
            return bs.ListUserLimitExceeds();
        }

        #endregion
    }
}
