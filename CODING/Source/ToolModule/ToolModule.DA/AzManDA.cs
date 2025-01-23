using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using PEA.BPM.Architecture.CommonUtilities;
using System.ComponentModel;
using System.Globalization;

namespace PEA.BPM.ToolModule.DA
{
    public class AzManDA
    {
        public BindingList<Role> ListGroup()
        {
            BindingList<Role> groups = new BindingList<Role>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_sel_Role");
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    Role g = new Role();
                    g.RoleId = DaHelper.GetString(dr, "RoleId");
                    g.RoleName = DaHelper.GetString(dr, "RoleName");
                    g.Description = DaHelper.GetString(dr, "Description");
                    g.UserCount = DaHelper.GetInt(dr, "UserCount");
                    g.ModifiedBy = DaHelper.GetDate(dr, "ModifiedDt").Value.ToString("dd-MM-yyyy HH:mm:ss", new CultureInfo("th-TH"));
                    groups.Add(g);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return groups;        
        }

        public BindingList<Role> ListExpRoles(User user)
        {
            BindingList<Role> groups = new BindingList<Role>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_sel_RoleExpUser");
                db.AddInParameter(cmd, "UserId", DbType.String, user.UserId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    Role g = new Role();
                    g.RoleId = DaHelper.GetString(dr, "RoleId");
                    g.RoleName = DaHelper.GetString(dr, "RoleName");
                    g.Description = DaHelper.GetString(dr, "Description");
                    g.UserCount = DaHelper.GetInt(dr, "UserCount");
                    g.ModifiedBy = DaHelper.GetDate(dr, "ModifiedDt").Value.ToString("dd-MM-yyyy HH:mm:ss", new CultureInfo("th-TH"));
                    groups.Add(g);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return groups;
        }

        public BindingList<Role> ListRolesByUser(string userId)
        {
            BindingList<Role> groups = new BindingList<Role>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_sel_RoleByUser");
                db.AddInParameter(cmd, "UserId", DbType.String, userId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    Role g = new Role();
                    g.RoleId = DaHelper.GetString(dr, "RoleId");
                    g.RoleName = DaHelper.GetString(dr, "RoleName");
                    g.UserCount = DaHelper.GetInt(dr, "UserCount");
                    g.ModifiedBy = DaHelper.GetDate(dr, "ModifiedDt").Value.ToString("dd-MM-yyyy HH:mm:ss", new CultureInfo("th-TH"));
                    groups.Add(g);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return groups;
        }

        public BindingList<User> ListUser(string filter)
        {
            BindingList<User> users = new BindingList<User>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_sel_User");
                db.AddInParameter(cmd, "Filter", DbType.String, filter);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    User user = new User();
                    user.UserId = DaHelper.GetString(dr, "UserId");
                    user.FullName = DaHelper.GetString(dr, "FullName");
                    user.Department = DaHelper.GetString(dr, "Department");
                    user.BranchId = DaHelper.GetString(dr, "BranchId");
                    user.BranchName = DaHelper.GetString(dr, "BranchName");
                    user.ScopeId = DaHelper.GetString(dr, "ScopeId");
                    user.ScopeText = DaHelper.GetString(dr, "ScopeText");
                    user.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    users.Add(user);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return users;
        }

        public BindingList<User> ListUsersByRole(string roleId)
        {
            BindingList<User> users = new BindingList<User>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_sel_UserByRole");
                db.AddInParameter(cmd, "RoleId", DbType.String, roleId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    User user = new User();
                    user.UserId = DaHelper.GetString(dr, "UserId");
                    user.FullName = DaHelper.GetString(dr, "FullName");
                    user.Department = DaHelper.GetString(dr, "Department");
                    user.BranchId = DaHelper.GetString(dr, "BranchId");
                    user.ScopeId = DaHelper.GetString(dr, "ScopeId");
                    user.ScopeText = DaHelper.GetString(dr, "ScopeText");
                    user.ModifiedBy = DaHelper.GetString(dr, "ModifiedBy");
                    users.Add(user);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return users;
        }

        public BindingList<Function> ListGroupFunctions(string roleId)
        {
            BindingList<Function> functionList = new BindingList<Function>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_sel_GroupFunctions");
            db.AddInParameter(cmd, "RoleId", DbType.String, roleId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                Function g = new Function();
                g.FunctionId = DaHelper.GetString(dr, "FncId");
                g.FunctionName = DaHelper.GetString(dr, "FncName");
                g.SubFunctionName = DaHelper.GetString(dr, "SubFncName");
                g.ModuleName = DaHelper.GetString(dr, "Module");
                g.Check = DaHelper.GetString(dr, "Active") == "1" ? true : false;
                g.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                g.Internal = DaHelper.GetInt(dr, "Internal") == 1 ? true : false;
                functionList.Add(g);
            }

            return functionList;
        }        

        public List<Function> ListAllFunctions()
        {
            List<Function> functionList = new List<Function>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_sel_Functions");
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                Function g = new Function();
                g.FunctionId = DaHelper.GetString(dr, "FncId");
                g.FunctionName = DaHelper.GetString(dr, "FncName");
                g.SubFunctionName = DaHelper.GetString(dr, "SubFncName");
                g.ModuleName = DaHelper.GetString(dr, "Module");
                g.Internal = DaHelper.GetInt(dr, "Internal") == 1 ? true : false;
                functionList.Add(g);
            }

            return functionList;
        }
        
        public BindingList<User> SearchUsers(string keyword)
        {
            BindingList<User> users = new BindingList<User>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_sel_UserByKeyword");
            db.AddInParameter(cmd, "Keyword", DbType.String, keyword);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                User u = new User();
                u.UserId = DaHelper.GetString(dr, "UserId");
                u.FullName = DaHelper.GetString(dr, "FullName");
                u.Department = DaHelper.GetString(dr, "Department");
                u.BranchId = DaHelper.GetString(dr, "BranchId");
                u.ModifiedDt = DaHelper.GetDate(dr, "ModifiedDt");
                users.Add(u);
            }

            return users;
        }

        public void CreateUser(DbTransaction trans, User user)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_ins_User");
            db.AddInParameter(cmd, "UserId", DbType.String, user.UserId);
            db.AddInParameter(cmd, "Password", DbType.String, user.Password);
            db.AddInParameter(cmd, "FullName", DbType.String, user.FullName);
            db.AddInParameter(cmd, "Department", DbType.String, user.Department);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.AddInParameter(cmd, "BranchId", DbType.String, user.BranchId);
            db.AddInParameter(cmd, "ScopeId", DbType.String, user.ScopeId);
            db.ExecuteScalar(cmd, trans);

            //return rowEffect;
        }


        public void EditUser(DbTransaction trans, User user)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_upd_User");
                db.AddInParameter(cmd, "UserId", DbType.String, user.UserId);
                db.AddInParameter(cmd, "NewPassword", DbType.String, user.NewPassword);
                db.AddInParameter(cmd, "ScopeId", DbType.String, user.ScopeId);
                db.AddInParameter(cmd, "IsPwdChanged", DbType.Int32, user.PwdState);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, user.ChangerId);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteUser(DbTransaction trans, User user)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_del_User");
                db.AddInParameter(cmd, "UserId", DbType.String, user.UserId);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string CreateRole(DbTransaction trans, Role role)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_ins_Role");
            db.AddInParameter(cmd, "RoleName", DbType.String, role.RoleName);
            db.AddInParameter(cmd, "RoleDesc", DbType.String, role.Description);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            object ret = db.ExecuteScalar(cmd, trans);
            return (string)ret;
        }

        public void EditRoleContent(DbTransaction trans, Role role)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_upd_Role");
            db.AddInParameter(cmd, "RoleId", DbType.String, role.RoleId);
            db.AddInParameter(cmd, "RoleName", DbType.String, role.RoleName);
            db.AddInParameter(cmd, "RoleDesc", DbType.String, role.Description);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteScalar(cmd, trans);
        }

        public void DeleteRole(DbTransaction trans, Role role)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_del_Role");
            db.AddInParameter(cmd, "RoleId", DbType.String, role.RoleId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteNonQuery(cmd, trans);
        }

        public void AddRoleUser(DbTransaction trans, User user, Role role)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_ins_RoleUser");
            db.AddInParameter(cmd, "RoleId", DbType.String, role.RoleId);
            db.AddInParameter(cmd, "UserId", DbType.String, user.UserId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteNonQuery(cmd, trans);
        }

        public void RemoveRoleUser(DbTransaction trans, User user, Role role)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_del_RoleUser");
            db.AddInParameter(cmd, "RoleId", DbType.String, role.RoleId);
            db.AddInParameter(cmd, "UserId", DbType.String, user.UserId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteNonQuery(cmd, trans);
        }

        public void AddRoleFunction(DbTransaction trans, string roleId, string fncId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_ins_RoleFunction");
            db.AddInParameter(cmd, "RoleId", DbType.String, roleId);
            db.AddInParameter(cmd, "FncId", DbType.String, fncId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteNonQuery(cmd, trans);
        }

        //ListEmployee
        public List<User> ListEmployee(string filter)
        {
            List<User> empList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_sel_Employee");
            db.AddInParameter(cmd, "Filter", DbType.String, filter);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                User ep = new User();
                ep.UserId = DaHelper.GetString(dr, "UserId");
                ep.FullName = DaHelper.GetString(dr, "FullName");
                ep.Department = DaHelper.GetString(dr, "Department");
                ep.ScopeText = DaHelper.GetString(dr, "ScopeText");
                ep.ScopeId = DaHelper.GetString(dr, "ScopeId");
                ep.BranchId = DaHelper.GetString(dr, "BranchId");
                empList.Add(ep);
            }

            return empList;
        }

        public bool ValidateUserScopeByRole(Role role)
        {
            List<User> empList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_UserScopeByRole");
            db.AddInParameter(cmd, "RoleId", DbType.String, role.RoleId);
            int ret = (int)db.ExecuteScalar(cmd);
            return ret == 1 ? true : false;
        }

        public bool ValidateUserScopeByUser(User user)
        {
            List<User> empList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_UserScopeByUser");
            db.AddInParameter(cmd, "UserId", DbType.String, user.UserId);
            db.AddInParameter(cmd, "ScopeId", DbType.String, user.ScopeId);
            int ret = (int)db.ExecuteScalar(cmd);
            return ret==1?true:false;
        }

        public bool ValidateUserScope(User user, Role role)
        {
            List<User> empList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_UserScope");
            db.AddInParameter(cmd, "UserId", DbType.String, user.UserId);
            db.AddInParameter(cmd, "ScopeId", DbType.String, user.ScopeId);
            db.AddInParameter(cmd, "RoleId", DbType.String, role.RoleId);
            int ret = (int)db.ExecuteScalar(cmd);
            return ret == 1 ? true : false;
        }

        public bool ValidateUserScopeByFnc(Role role, Function fnc)
        {
            List<User> empList = new List<User>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_UserScopeFnc");
            db.AddInParameter(cmd, "FncId", DbType.String, fnc.FunctionId);
            db.AddInParameter(cmd, "RoleId", DbType.String, role.RoleId);
            int ret = (int)db.ExecuteScalar(cmd);
            return ret == 1 ? true : false;
        }

        public int UpdateUser(DbTransaction trans, User user)
        {
            int retVal = 0;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_upd_ChangePassword");
                db.AddOutParameter(cmd, "RetId", DbType.Int32, 8);
                db.AddInParameter(cmd, "RTId", DbType.Int32, user.RTId);
                db.AddInParameter(cmd, "UserId", DbType.String, user.UserId);
                db.AddInParameter(cmd, "RoleId", DbType.String, user.RoleId);
                db.AddInParameter(cmd, "NewRoleId", DbType.String, user.NewRoleId);
                db.AddInParameter(cmd, "NewPassword", DbType.String, user.NewPassword);
                db.AddInParameter(cmd, "IsPwdChanged", DbType.Int32, user.PwdState);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, user.ChangerId);
                db.AddInParameter(cmd, "ModifiedByPwd", DbType.String, user.ChangerPwd);
                db.ExecuteNonQuery(cmd, trans);
                retVal = (int)db.GetParameterValue(cmd, "RetId");
            }
            catch (Exception)
            {
                throw;
            }

            return retVal;
        }

        public List<PeaInfo> SelectBranchByKeyword(string keyword, string userId)
        {
            List<PeaInfo> branchList = new List<PeaInfo>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_sel_Branchs");
            db.AddInParameter(cmd, "Branch", DbType.String, keyword);
            db.AddInParameter(cmd, "UserId", DbType.String, userId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                PeaInfo branchInfo = new PeaInfo();
                branchInfo.BranchId = DaHelper.GetString(r, "BranchId");
                branchInfo.BranchName = DaHelper.GetString(r, "BranchName");
                branchInfo.BranchName2 = DaHelper.GetString(r, "BranchName2");
                branchInfo.Address = DaHelper.GetString(r, "Address");
                branchInfo.BranchNo = DaHelper.GetString(r, "BranchNo");
                branchInfo.BranchLevel = DaHelper.GetString(r, "BranchLevel") == null ? "0" : DaHelper.GetString(r, "BranchLevel");
                branchInfo.BACode = DaHelper.GetString(r, "BusinessArea");
                branchList.Add(branchInfo);
            }

            return branchList;
        }

        public string AddTerminal(DbTransaction trans, Terminal terminal, out string terminalCode)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_ins_Terminal");
            db.AddOutParameter(cmd, "pTerminalId", DbType.String, 6);
            db.AddOutParameter(cmd, "pTerminalCode", DbType.String, 15);
            db.AddInParameter(cmd, "pBranchId ", DbType.String, terminal.BranchId);
            db.AddInParameter(cmd, "pIP", DbType.String, terminal.IP);
            db.AddInParameter(cmd, "pTaxCode", DbType.String, terminal.TaxCode);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, terminal.ModifiedBy);
            db.ExecuteNonQuery(cmd, trans);

            string retVal = (string)db.GetParameterValue(cmd, "pTerminalId");
            terminalCode = (string)db.GetParameterValue(cmd, "pTerminalCode");

            return retVal;
        }

        public void UpdateTerminal(DbTransaction trans, Terminal terminal)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_upd_Terminal");
            db.AddInParameter(cmd, "pTerminalId", DbType.String, terminal.TerminalId);
            db.AddInParameter(cmd, "pBranchId ", DbType.String, terminal.BranchId);
            db.AddInParameter(cmd, "pIP", DbType.String, terminal.IP);
            db.AddInParameter(cmd, "pTaxCode", DbType.String, terminal.TaxCode);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, terminal.ModifiedBy);
            db.ExecuteNonQuery(cmd, trans);
        }

        public List<ReportUnlockingLog> GetUnlockingLogReport(DateTime billPred, string id, string fncId)
        {
            List<ReportUnlockingLog> report = new List<ReportUnlockingLog>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_sel_unlockLog");
                db.AddInParameter(cmd, "PrintDate", DbType.DateTime, billPred);
                db.AddInParameter(cmd, "BranchId", DbType.String, id);
                if (!string.Equals(fncId, "0000"))
                    db.AddInParameter(cmd, "FunctionId", DbType.String, fncId);
                //else select all

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    ReportUnlockingLog r = new ReportUnlockingLog();
                    r.BranchId = DaHelper.GetString(dr, "BranchId");
                    r.BranchName = DaHelper.GetString(dr, "BranchName");
                    r.UnlockDt = DaHelper.GetString(dr, "UnlockDt");
                    r.CurrentUserId = DaHelper.GetString(dr, "CurrentUserId");
                    r.UnlockUserId = DaHelper.GetString(dr, "UnlockUserId");
                    r.UDescription = DaHelper.GetString(dr, "UDescription");
                    r.Remark = DaHelper.GetString(dr, "Remark");
                    r.FncId = DaHelper.GetString(dr, "FncId");
                    r.Module = DaHelper.GetString(dr, "Module");
                    r.FncName = DaHelper.GetString(dr, "FncName");
                    r.SubFncName = DaHelper.GetString(dr, "SubFncName");
                    r.FDescription = DaHelper.GetString(dr, "FDescription");
                    report.Add(r);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return report;
        }

        //Manage UnlockRemark
        public List<Function> ListRemarkFunctions()
        {
            List<Function> functionList = new List<Function>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_sel_RemarkFunctions");
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                Function g = new Function();
                g.FunctionId = DaHelper.GetString(dr, "FncId");
                g.FunctionName = string.Format("[{0}] {1}", DaHelper.GetString(dr, "Module"), DaHelper.GetString(dr, "SubFncName"));
                g.Description = DaHelper.GetString(dr, "Description");
                functionList.Add(g);
            }

            return functionList;
        }

        public BindingList<UnlockRemark> ListRemark(string fncId)
        {
            BindingList<UnlockRemark> list = new BindingList<UnlockRemark>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_sel_Remarks");
            db.AddInParameter(cmd, "FunctionId", DbType.String, fncId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                UnlockRemark g = new UnlockRemark();
                g.FncId = DaHelper.GetString(dr, "FncId");
                g.RemarkId = DaHelper.GetString(dr, "RemarkId");
                g.Description = DaHelper.GetString(dr, "Description");
                list.Add(g);
            }

            return list;
        }

        public void AddRemark(DbTransaction trans, UnlockRemark rmk, string postBranchServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_ins_UnlockRemark");
            db.AddInParameter(cmd, "FncId", DbType.String, rmk.FncId);
            db.AddInParameter(cmd, "Description", DbType.String, rmk.Description);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteScalar(cmd, trans);
        }

        public void EditRemark(DbTransaction trans, UnlockRemark rmk, string postBranchServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_upd_UnlockRemark");
            db.AddInParameter(cmd, "FncId", DbType.String, rmk.FncId);
            db.AddInParameter(cmd, "RemarkId", DbType.String, rmk.RemarkId);
            db.AddInParameter(cmd, "Description", DbType.String, rmk.Description);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteScalar(cmd, trans);
        }

        public void DeleteRemark(DbTransaction trans, UnlockRemark rmk, string postBranchServerId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_del_UnlockRemark");
            db.AddInParameter(cmd, "FncId", DbType.String, rmk.FncId);
            db.AddInParameter(cmd, "RemarkId", DbType.String, rmk.RemarkId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, Session.User.Id);
            db.ExecuteScalar(cmd, trans);
        }

        public List<Function> ListUnlockableFunctions()
        {
            List<Function> functionList = new List<Function>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_sel_UnlockableFunctions");
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                Function g = new Function();
                g.FunctionId = DaHelper.GetString(dr, "FncId");
                g.FunctionName = string.Format("[{0}] {1}", DaHelper.GetString(dr, "Module"), DaHelper.GetString(dr, "SubFncName"));
                g.Description = DaHelper.GetString(dr, "Description");
                functionList.Add(g);
            }

            return functionList;
        }

        public DateTime GetDBServerTime()
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ta_get_ServerTime");

            return (DateTime)db.ExecuteScalar(cmd);
        }

        public BindingList<UserExceed> ListUserLimitExceeds()
        {
            BindingList<UserExceed> UserExceedDetail = new BindingList<UserExceed>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ta_get_UsersLimit");
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    UserExceed g = new UserExceed();
                    g.UserLimit = Convert.ToInt32(DaHelper.GetInt(dr, "UserLimit"));
                    g.UserCurrentUsed = Convert.ToInt32(DaHelper.GetInt(dr, "UserCurrentUsed"));
                    g.IsExceed = DaHelper.GetInt(dr, "IsExeedLimit") == 1 ? true : false;

                    UserExceedDetail.Add(g);
                }
                return UserExceedDetail;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
