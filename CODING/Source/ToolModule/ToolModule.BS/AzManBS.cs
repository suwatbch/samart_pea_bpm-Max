using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.ToolModule.Interface.Services;
using System.Threading;
using System.Data.Common;
using PEA.BPM.ToolModule.DA;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.ToolModule.BS
{
    public class AzManBS: IAzManService
    {
        #region Permission Management

        public BindingList<Role> ListRoles()
        {
            try
            {
                AzManDA da = new AzManDA();
                BindingList<Role> roles = da.ListGroup();
                return roles;
            }
            catch (Exception)
            {
                // Write Log
                // ...
                //
                throw;
            }
        }

        public BindingList<Role> ListExpRoles(User user)
        {
            try
            {
                AzManDA da = new AzManDA();
                BindingList<Role> roles = da.ListExpRoles(user);
                return roles;
            }
            catch (Exception)
            {
                // Write Log
                // ...
                //
                throw;
            }
        }        

        public BindingList<Role> ListRolesByUser(string userId)
        {
            try
            {
                AzManDA da = new AzManDA();
                BindingList<Role> roles = da.ListRolesByUser(userId);
                return roles;
            }
            catch (Exception)
            {
                // Write Log
                // ...
                //
                throw;
            }
        }

        public BindingList<User> ListUsers(string filter)
        {
            try
            {
                AzManDA da = new AzManDA();
                BindingList<User> users = da.ListUser(filter);
                return users;
            }
            catch (Exception)
            {
                // Write Log
                // ...
                //
                throw;
            }
        }

        public BindingList<User> ListUsersByRole(string roleId)
        {
            try
            {
                AzManDA da = new AzManDA();
                BindingList<User> users = da.ListUsersByRole(roleId);
                return users;
            }
            catch (Exception)
            {
                // Write Log
                // ...
                //
                throw;
            }
        }

        public BindingList<Function> ListFunctions(string roleId)
        {
            try
            {
                AzManDA da = new AzManDA();
                BindingList<Function> functions = da.ListGroupFunctions(roleId);
                return functions;
            }
            catch (Exception ex)
            {
                // Write Log
                // ...
                //
                throw ex;
            }
        }

        public List<Function> ListAllFunctions()
        {
            try
            {
                AzManDA da = new AzManDA();
                List<Function> functions = da.ListAllFunctions();
                return functions;
            }
            catch (Exception ex)
            {
                // Write Log
                // ...
                //
                throw ex;
            }
        }

        public BindingList<User> SearchUsers(string keyword)
        {
            try
            {
                AzManDA da = new AzManDA();
                BindingList<User> users = da.SearchUsers(keyword);
                return users;
            }
            catch (Exception ex)
            {
                // Write Log
                // ...
                //
                throw ex;
            }
        }

        public void CreateUser(User user)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    da.CreateUser(trans, user);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }
        }

        public void EditUser(User user)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    da.EditUser(trans, user);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }
        }

        public void DeleteUser(User user)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    da.DeleteUser(trans, user);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }
        }

        public void CreateRole(Role role)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    string roleId = da.CreateRole(trans, role);

                    foreach (Function fnc in role.FncList)
                        da.AddRoleFunction(trans, roleId, fnc.FunctionId, role.ModifiedBy);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }
        }

        public void EditRole(Role role)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    da.EditRoleContent(trans, role);

                    foreach (Function fncId in role.FncList)
                        da.AddRoleFunction(trans, role.RoleId, fncId.FunctionId, role.ModifiedBy);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }
        }

        public void DeleteRole(Role role)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    da.DeleteRole(trans, role);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }
        }

        public void AddRoleUser(User user, Role role)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    da.AddRoleUser(trans, user, role);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }
        }

        public void RemoveRoleUser(User user, Role role)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    da.RemoveRoleUser(trans, user, role);
                    trans.Commit();

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }
        }

        public List<User> ListEmployee(string filter)
        {
            AzManDA da = new AzManDA();
            return da.ListEmployee(filter);
        }

        public int UpdateUser(DbTransaction trans, User user)
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
                        AzManDA da = new AzManDA();
                        int ret = da.UpdateUser(trans, user);

                        trans.Commit();

                        return ret;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        // Write Log
                        // ...
                        //
                        throw ex;
                    }
                }
            }
            else // If it's called from BS component, transaction should be beginned
            {
                AzManDA da = new AzManDA();
                return da.UpdateUser(trans, user);
            }
        }

        public bool ValidateUserScopeByUser(User user)
        {
            AzManDA da = new AzManDA();
            return da.ValidateUserScopeByUser(user);
        }

        public bool ValidateUserScopeByRole(Role role)
        {
            AzManDA da = new AzManDA();

            foreach (Function fnc in role.FncList)
            {
                bool ret = da.ValidateUserScopeByFnc(role, fnc);
                //if false, so return
                if (!ret) return false;   
            }

            return true;
        }

        public bool ValidateUserScope(User user, Role role)
        {
            AzManDA da = new AzManDA();
            return da.ValidateUserScope(user, role);
        }

        #endregion


        public List<PeaInfo> SearchBranchByKeyword(string keyword, string userId)
        {            
            AzManDA da = new AzManDA();
            List<PeaInfo> retVal = da.SelectBranchByKeyword(keyword, userId);
            return retVal;  
        }

        public string RegisterTerminal(DbTransaction trans, Terminal terminal, out string terminalCode)
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
                        AzManDA da = new AzManDA();
                        string ret = da.AddTerminal(trans, terminal, out terminalCode);

                        trans.Commit();

                        return ret;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        // Write Log
                        // ...
                        //
                        throw ex;
                    }
                }
            }
            else // If it's called from BS component, transaction should be beginned
            {
                AzManDA da = new AzManDA();
                return da.AddTerminal(trans, terminal, out terminalCode);
            }
        }


        public void UpdateTerminal(Terminal terminal)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    da.UpdateTerminal(trans, terminal);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }
        }
        public List<ReportUnlockingLog> GetUnlockingLogReport(UnlockingLogParam param)
        {            
            AzManDA da = new AzManDA();
            List<ReportUnlockingLog> ruAll = new List<ReportUnlockingLog>();
            try
            {
                foreach (string id in param.BranchId)
                {
                    ruAll.AddRange(da.GetUnlockingLogReport(param.BillPred, id, param.FunctionId));
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ruAll;
        }

        public List<Function> ListRemarkFunctions()
        {
            try
            {
                AzManDA da = new AzManDA();
                List<Function> functions = da.ListRemarkFunctions();
                return functions;
            }
            catch (Exception ex)
            {
                // Write Log
                // ...
                //
                throw ex;
            }
        }

        public BindingList<UnlockRemark> ListRemark(string fncId)
        {
            try
            {
                AzManDA da = new AzManDA();
                BindingList<UnlockRemark> remarks = da.ListRemark(fncId);
                return remarks;
            }
            catch (Exception ex)
            {
                // Write Log
                // ...
                //
                throw ex;
            }
        }

        // This method need to be in transaction
        public int AddRemark(UnlockRemark rmk)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    da.AddRemark(trans, rmk, Session.Branch.PostedServerId);

                    trans.Commit();

                    return (int)1;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }

        }

        public int EditRemark(UnlockRemark rmk)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    da.EditRemark(trans, rmk, Session.Branch.PostedServerId);

                    trans.Commit();

                    return (int)1;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }

        }

        public int DeleteRemark(UnlockRemark rmk)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    AzManDA da = new AzManDA();
                    da.DeleteRemark(trans, rmk, Session.Branch.PostedServerId);

                    trans.Commit();

                    return (int)1;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    // Write Log
                    // ...
                    //
                    throw ex;
                }
            }

        }

        public List<Function> ListUnlockableFunctions()
        {
            try
            {
                AzManDA da = new AzManDA();
                List<Function> functions = da.ListUnlockableFunctions();
                return functions;
            }
            catch (Exception ex)
            {
                // Write Log
                // ...
                //
                throw ex;
            }
        }

        public DateTime GetWSServerTime(string WSUrl)
        {
            try
            {
                //WSUrl is used in SG Layer
                return DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DateTime GetDBServerTime(string WSUrl)
        {
            try
            {
                //WSUrl is used in SG Layer
                AzManDA da = new AzManDA();
                return da.GetDBServerTime();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region #ISSUE LIMIT USER


        public BindingList<UserExceed> ListUserLimitExceeds()
        {
            try
            {
                AzManDA da = new AzManDA();
                BindingList<UserExceed> userLimit = da.ListUserLimitExceeds();
                return userLimit;
            }
            catch (Exception ex)
            {
                // Write Log
                // ...
                //
                throw ex;
            }
        }



        #endregion
    }
}
