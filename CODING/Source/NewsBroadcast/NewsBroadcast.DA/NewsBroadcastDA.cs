using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.NewsBroadcast.Interface.Constants;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.NewsBroadcast.DA
{
    public class NewsBroadcastDA
    {
        #region Constant
        const string strConnConfig = "NewsDatabase";
        const string strCenterConnConfig = "POSDatabase";
        //const int SQLCOMMAND_TIMEOUT = 9999999;
        #endregion

        #region Global Variable
        private string strConn;
        private SqlConnection connection;
        private SqlCommand cmd;
        #endregion

        #region Constructor
        public NewsBroadcastDA()
        {
        }
        #endregion

        #region Method

        public List<NewsBroadcastInfo> Select_NewsBroadcast_Now(DateTime _nowDt, string _userId, int _cmdId)
        {
            List<NewsBroadcastInfo> list = new List<NewsBroadcastInfo>();
            DataTable datatable = new DataTable("NewsBroadcast");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@NowDt", _nowDt.ToString("s"));
            dic.Add("@UserId", _userId);
            dic.Add("@CmdId", _cmdId.ToString());
            datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_NewsBroadcast_Now", dic);
            foreach (DataRow dr in datatable.Rows)
            {
                NewsBroadcastInfo nb = new NewsBroadcastInfo();
                nb.BroadcastId = (int)dr[ColumnInfo.NewsBroadcast.BroadcastId];
                nb.SentDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.SentDt);
                nb.ExpireDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.ExpireDt);
                nb.BroadcastTopic = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.BroadcastTopic);
                nb.Detail = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.Detail);
                nb.CmdId = (int)dr[ColumnInfo.NewsBroadcast.CmdId];
                nb.IsOpened = (bool)dr[ColumnInfo.NewsBroadcast.IsOpened];
                nb.IsRead = (bool)dr[ColumnInfo.NewsBroadcast.IsRead];
                nb.ReadSymbol = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.ReadSymbol);
                list.Add(nb);
            }
            return list;
        }

       

        public List<NewsBroadcastInfo> Select_NewsBroadcast_History(DateTime _nowDt, string _userId, int _cmdId)
        {
            List<NewsBroadcastInfo> list = new List<NewsBroadcastInfo>();
            DataTable datatable = new DataTable("NewsBroadcast");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@NowDt", _nowDt.ToString("s"));
            dic.Add("@UserId", _userId);
            dic.Add("@CmdId", _cmdId.ToString());
            datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_NewsBroadcast_History", dic);
            foreach (DataRow dr in datatable.Rows)
            {
                NewsBroadcastInfo nb = new NewsBroadcastInfo();
                nb.BroadcastId = (int)dr[ColumnInfo.NewsBroadcast.BroadcastId];
                nb.SentDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.SentDt);
                nb.ExpireDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.ExpireDt);
                nb.BroadcastTopic = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.BroadcastTopic);
                nb.Detail = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.Detail);
                nb.CmdId = (int)dr[ColumnInfo.NewsBroadcast.CmdId];
                nb.IsOpened = (bool)dr[ColumnInfo.NewsBroadcast.IsOpened];
                nb.IsRead = (bool)dr[ColumnInfo.NewsBroadcast.IsRead];
                nb.ReadSymbol = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.ReadSymbol);
                list.Add(nb);
            }
            return list;
        }

        public List<NewsBroadcastSentInfo> Select_NewsBroadcast_Sent(DateTime _sentDt)
        {
            List<NewsBroadcastSentInfo> list = new List<NewsBroadcastSentInfo>();
            DataTable datatable = new DataTable("NewsBroadcast");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@SentDt", _sentDt.ToString("s"));
            datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_NewsBroadcast_BySentDt", dic);
            foreach (DataRow dr in datatable.Rows)
            {
                NewsBroadcastSentInfo nb = new NewsBroadcastSentInfo();
                nb.BroadcastId = (int)dr[ColumnInfo.NewsBroadcast.BroadcastId];
                nb.SentDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.SentDt);
                nb.ExpireDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.ExpireDt);
                nb.BroadcastTopic = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.BroadcastTopic);
                nb.Detail = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.Detail);
                list.Add(nb);
            }
            return list;
        }

        public List<NewsBroadcastSentInfo> Select_NewsBroadcast_Now_forSender(DateTime _nowDt, int _cmdId)
        {
            List<NewsBroadcastSentInfo> list = new List<NewsBroadcastSentInfo>();
            DataTable datatable = new DataTable("NewsBroadcast");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@NowDt", _nowDt.ToString("s"));
            dic.Add("@CmdId", _cmdId.ToString());
            datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_NewsBroadcast_Now_forSender", dic);
            foreach (DataRow dr in datatable.Rows)
            {
                NewsBroadcastSentInfo nb = new NewsBroadcastSentInfo();
                nb.BroadcastId = (int)dr[ColumnInfo.NewsBroadcast.BroadcastId];
                nb.SentDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.SentDt);
                nb.ExpireDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.ExpireDt);
                nb.BroadcastTopic = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.BroadcastTopic);
                nb.Detail = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.Detail);
                list.Add(nb);
            }
            return list;
        }

        public List<NewsBroadcastSentInfo> Select_NewsBroadcast_Scheduled(DateTime _nowDt, int _cmdId)
        {
            List<NewsBroadcastSentInfo> list = new List<NewsBroadcastSentInfo>();
            DataTable datatable = new DataTable("NewsBroadcast");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@NowDt", _nowDt.ToString("s"));
            dic.Add("@CmdId", _cmdId.ToString());
            datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_NewsBroadcast_Scheduled", dic);
            foreach (DataRow dr in datatable.Rows)
            {
                NewsBroadcastSentInfo nb = new NewsBroadcastSentInfo();
                nb.BroadcastId = (int)dr[ColumnInfo.NewsBroadcast.BroadcastId];
                nb.SentDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.SentDt);
                nb.ExpireDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.ExpireDt);
                nb.BroadcastTopic = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.BroadcastTopic);
                nb.Detail = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.Detail);
                list.Add(nb);
            }
            return list;
        }

        public List<NewsBroadcastSentInfo> Select_NewsBroadcast_SentMonthYears(DateTime _sentDt)
        {
            List<NewsBroadcastSentInfo> list = new List<NewsBroadcastSentInfo>();
            DataTable datatable = new DataTable("NewsBroadcast");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@SentMonthYear", _sentDt.ToString("s"));
            datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_NewsBroadcast_BySentMonthYear", dic);
            foreach (DataRow dr in datatable.Rows)
            {
                NewsBroadcastSentInfo nb = new NewsBroadcastSentInfo();
                nb.BroadcastId = (int)dr[ColumnInfo.NewsBroadcast.BroadcastId];
                nb.SentDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.SentDt);
                nb.ExpireDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcast.ExpireDt);
                nb.BroadcastTopic = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.BroadcastTopic);
                nb.Detail = DaHelper.GetString(dr, ColumnInfo.NewsBroadcast.Detail);
                list.Add(nb);
            }
            return list;
        }

        public List<NewsBroadcastUserInfo> Select_NewsBroadcastUser_ByBroadcastId(int _broadcastId)
        {
            List<NewsBroadcastUserInfo> list = new List<NewsBroadcastUserInfo>();
            DataTable datatable = new DataTable("NewsBroadcast");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@BroadcastId", _broadcastId.ToString());
            datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_NewsBroadcastUser_ByBroadcastId", dic);
            foreach (DataRow dr in datatable.Rows)
            {
                NewsBroadcastUserInfo nb = new NewsBroadcastUserInfo();
                nb.BroadcastId = (int)dr[ColumnInfo.NewsBroadcastUser.BroadcastId];
                nb.UserId = DaHelper.GetString(dr, ColumnInfo.NewsBroadcastUser.UserId);
                nb.IsRead = (bool)dr[ColumnInfo.NewsBroadcastUser.IsRead];
                nb.IsOpened = (bool)dr[ColumnInfo.NewsBroadcastUser.IsOpened];
                nb.AreaId = DaHelper.GetString(dr, ColumnInfo.NewsBroadcastUser.AreaId);
                nb.BranchId = DaHelper.GetString(dr, ColumnInfo.NewsBroadcastUser.BranchId);
                nb.BranchName2 = DaHelper.GetString(dr, ColumnInfo.NewsBroadcastUser.BranchName2);
                nb.RoleId = DaHelper.GetString(dr, ColumnInfo.NewsBroadcastUser.RoleId);
                nb.RoleName = DaHelper.GetString(dr, ColumnInfo.NewsBroadcastUser.RoleName);
                nb.ReadDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcastUser.ReadDt);
                nb.OpenedDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcastUser.OpenedDt);
                nb.ModifiedDt = DaHelper.GetDateTime(dr, ColumnInfo.NewsBroadcastUser.ModifiedDt);
                nb.Active = (bool)dr[ColumnInfo.NewsBroadcastUser.Active];
                list.Add(nb);
            }
            return list;
        }

        public void Update_NewsBroadcastUser_Opened(int _broadcastId, string _userId, bool _isOpened)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@BroadcastId", _broadcastId);
            dic.Add("@UserId", _userId);
            dic.Add("@IsOpened", _isOpened);
            ExecuteCommand(strConnConfig, "nb_upd_NewsBroadcastUser_Opened", dic);
        }

        public void Update_NewsBroadcastUser_Read(int _broadcastId, string _userId, bool _isRead)
        { 
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@BroadcastId", _broadcastId);
            dic.Add("@UserId", _userId);
            dic.Add("@IsRead", _isRead);
            ExecuteCommand(strConnConfig, "nb_upd_NewsBroadcastUser_Read", dic);
        }

        public List<AreaInfo> Select_Area()
        {
            List<AreaInfo> list = new List<AreaInfo>();

            DataTable datatable = new DataTable("POSDatabase");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            datatable = ExecuteCommandToDataTable(strCenterConnConfig, "nb_sel_Area", dic);
            foreach (DataRow dr in datatable.Rows)
            {
                AreaInfo ar = new AreaInfo();
                ar.AreaId   = dr[ColumnInfo.Area.AreaId].ToString();
                ar.AreaName = dr[ColumnInfo.Area.AreaName].ToString();
                list.Add(ar);
            }
            return list;
        }

        public List<BranchInfo> Select_Branch(string _areaId)
        {
            List<BranchInfo> list = new List<BranchInfo>();

            DataTable datatable = new DataTable("POSDatabase");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (_areaId == String.Empty)
                dic.Add("@AreaId", DBNull.Value);
            else
                dic.Add("@AreaId", _areaId);
            datatable = ExecuteCommandToDataTable(strCenterConnConfig, "nb_sel_Branch", dic);
            foreach (DataRow dr in datatable.Rows)
            {
                BranchInfo bar = new BranchInfo();
                bar.BranchId    = dr[ColumnInfo.BranchArea.BranchId].ToString();
                bar.AreaId      = dr[ColumnInfo.BranchArea.AreaId].ToString();
                bar.BranchName  = dr[ColumnInfo.BranchArea.BranchName].ToString();
                list.Add(bar);
            }
            return list;
        }


        public List<UserInfo> Select_User(string _roleId)
        {
            List<UserInfo> list = new List<UserInfo>();

            DataTable datatable = new DataTable("NewsBroadcast");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (_roleId.Equals(String.Empty))
                dic.Add("@RoleId", DBNull.Value);
            else
                dic.Add("@RoleId", _roleId);
            datatable = ExecuteCommandToDataTable(strCenterConnConfig, "nb_sel_User", dic);
            foreach (DataRow dr in datatable.Rows)
            {
                UserInfo user = new UserInfo();
                user.UserId = dr[ColumnInfo.User.UserId].ToString();
                user.FullName = dr[ColumnInfo.User.FullName].ToString();
                user.BranchId = dr[ColumnInfo.User.BranchId].ToString();
                user.PostBranchServerId = dr[ColumnInfo.User.PostBranchServerId].ToString();
                list.Add(user);
            }
            return list;
        }

        public List<RoleInfo> Select_Role()
        {
            List<RoleInfo> list = new List<RoleInfo>();

            DataTable datatable = new DataTable("NewsBroadcast");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            datatable = ExecuteCommandToDataTable(strCenterConnConfig, "nb_sel_Role", dic);
            foreach (DataRow dr in datatable.Rows)
            {
                RoleInfo role = new RoleInfo();
                role.RoleId = dr[ColumnInfo.Role.RoleId].ToString();
                role.RoleName = dr[ColumnInfo.Role.RoleName].ToString();
                role.Description = dr[ColumnInfo.Role.Description].ToString();
                list.Add(role);
            }
            return list;
        }

        public void Insert_NewsBroadcast(string _broadcastTopic, string _detail, DateTime _sentDt, DateTime _expireDt, int _cmdId)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@BroadcastTopic", _broadcastTopic);
            dic.Add("@Detail", _detail);
            dic.Add("@SentDt", _sentDt.ToString("s"));
            dic.Add("@ExpireDt", _expireDt.ToString("s"));
            dic.Add("@CmdId", _cmdId);
            ExecuteCommand(strConnConfig, "nb_ins_NewsBroadcast", dic);
        }



        public void Insert_NewsBroadcastUser(int _broadcastId, string _userId, string _areaId, string _branchId, string _branchName2, string _roleId, string _roleName)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@BroadcastId", _broadcastId.ToString());
            dic.Add("@UserId", _userId);
            dic.Add("@AreaId", _areaId);
            dic.Add("@BranchId", _branchId);
            dic.Add("@BranchName2", _branchName2);
            dic.Add("@RoleId", _roleId);
            dic.Add("@RoleName", _roleName);
            ExecuteCommand(strConnConfig, "nb_ins_NewsBroadcastUser", dic);
        }

        public int Select_LastNewsBroadcastId()
        {
            int broadcastId = 0;
            DataTable datatable = new DataTable("NewsBroadcast");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_LastNewsBroadcastId", dic);
            foreach (DataRow dr in datatable.Rows)
            {
               broadcastId = (int)dr[ColumnInfo.NewsBroadcast.BroadcastId];
            }
            return broadcastId;
        }

        public bool Select_DuplicateUser(int _broadcastId, string _userId)
        {
             DataTable datatable = new DataTable("NewsBroadcast");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@BroadcastId", _broadcastId.ToString());
            dic.Add("@UserId", _userId);
            datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_dublicateUser", dic);
            if (datatable.Rows.Count > 0)
                return true;
            else
                return false;
             
        }


        #region old code
        //public DataTable Select_NewsBroadcastBySentDate(DateTime _sentDate)
        //{
        //    DataTable datatable = new DataTable("NewsBroadcast");
        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    dic.Add("@SentDt", _sentDate.ToString("s"));
        //    datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_NewsBroadcastBySentDate", dic);
        //    return datatable;

        //}

        //public void Insert_NewsBroadcast(string _broadcastTopic, string _detail, DateTime _sentDate, int _recieverId, string _branchId)
        //{
        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    dic.Add("@BroadcastTopic", _broadcastTopic);
        //    dic.Add("@Detail", _detail);
        //    dic.Add("@SentDt", _sentDate.ToString("s"));
        //    dic.Add("@RecieverId", _recieverId);
        //    dic.Add("@BranchId", _branchId);
        //    ExecuteCommand(strConnConfig, "nb_ins_NewsBroadcast", dic);
        //}



        //public DataTable Select_BranchArea()
        //{
        //    DataTable datatable = new DataTable("Branch");
        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_BranchArea", dic);
        //    return datatable;

        //}

        //public DataTable Select_Area()
        //{
        //    DataTable datatable = new DataTable("Area");
        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    datatable = ExecuteCommandToDataTable(strConnConfig, "nb_sel_Area", dic);
        //    return datatable;

        //}
        #endregion

        private void ExecuteCommand(string stringConn, string commandText, Dictionary<string, object> dic)
        {
            Database db = DatabaseFactory.CreateDatabase(stringConn);
            DbCommand cmd = db.GetStoredProcCommand(commandText);
            foreach (KeyValuePair<string, object> pair in dic)
            {
                cmd.Parameters.Add(new SqlParameter(pair.Key, pair.Value));
            }
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Management Connection & Execute SQLCommand in to DataTable
        /// </summary>
        /// <param name="stringConn">ConnectionString</param>
        /// <param name="commandText">String of StoreProcedure</param>
        /// <param name="dic">Dictionary of Parameters in StoreProcedure </param>
        private DataTable ExecuteCommandToDataTable(string stringConn, string commandText, Dictionary<string, object> dic)
        {
            DataTable dataTable = new DataTable("NewsBroadcast");
            Database db = DatabaseFactory.CreateDatabase(stringConn);
            DbCommand cmd = db.GetStoredProcCommand(commandText);
            foreach (KeyValuePair<string, object> pair in dic)
            {
                cmd.Parameters.Add(new SqlParameter(pair.Key, pair.Value));
            }

            dataTable.Load(db.ExecuteReader(cmd));
            return dataTable;
        }
        #endregion
    }
}
