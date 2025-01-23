using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;
using PEA.BPM.NewsBroadcast.Interface.Services;
using PEA.BPM.NewsBroadcast.DA;
using System.Configuration;

namespace PEA.BPM.NewsBroadcast.BS
{
    public class NewsBroadcastBS : INewsBroadcastService
    {
        /// <summary>
        /// ดึงข้อมูล BroadcastId ของข่าวที่ Branch นี้จะได้รับในเวลาปัจจุบัน
        /// </summary>
        /// <param name="_nowDt">เวลาปัจจุบัน</param>
        /// <param name="_userId">รหัสผู้ใช้</param>
        /// <param name="_cmdId">CommamdId(สำหรับแจกแจง Function - สำหรับข่าวมี default คือ 1)</param>
        /// <returns></returns>
        public List<NewsBroadcastInfo> GetNewsBroadcastNow(DateTime _nowDt, string _userId, int _cmdId)
        {
            List<NewsBroadcastInfo> EmptyList = new List<NewsBroadcastInfo>();
            if (ConfigurationManager.ConnectionStrings["NewsDatabase"].ToString() == null)
                return EmptyList;

            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_NewsBroadcast_Now(_nowDt, _userId, _cmdId);
        }

      

        public List<NewsBroadcastInfo> GetNewsBroadcastHistory(DateTime _nowDt, string _userId, int _cmdId)
        {
            List<NewsBroadcastInfo> EmptyList = new List<NewsBroadcastInfo>();
            if (ConfigurationManager.ConnectionStrings["NewsDatabase"].ToString() == null)
                return EmptyList;

            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_NewsBroadcast_History(_nowDt, _userId, _cmdId);
        }

        public List<NewsBroadcastSentInfo> GetNewsBroadcastSent(DateTime _sentDt)
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_NewsBroadcast_Sent(_sentDt);
        }

        public List<NewsBroadcastSentInfo> GetNewsBroadcastNowForSender(DateTime _nowDt, int _cmdId)
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_NewsBroadcast_Now_forSender(_nowDt, _cmdId);
        }

        public List<NewsBroadcastSentInfo> GetNewsBroadcastScheduled(DateTime _nowDt, int _cmdId)
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_NewsBroadcast_Scheduled(_nowDt, _cmdId);
        }

        public List<NewsBroadcastSentInfo> GetNewsBroadcastSentMonthYears(DateTime _sentDt)
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_NewsBroadcast_SentMonthYears(_sentDt);
        }

        public List<NewsBroadcastUserInfo> GetNewsBroadcastUser(int _broadcastId)
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_NewsBroadcastUser_ByBroadcastId(_broadcastId);
        }

        public void UpdateNewsBroadcastUserOpened(int _broadcastId, string _userId)
        {
            if (ConfigurationManager.ConnectionStrings["NewsDatabase"].ToString() != null)
            {
                NewsBroadcastDA da = new NewsBroadcastDA();
                da.Update_NewsBroadcastUser_Opened(_broadcastId, _userId, true);
            }
        }

        public void UpdateNewsBroadcastUserRead(int _broadcastId, string _userId)
        {
            if (ConfigurationManager.ConnectionStrings["NewsDatabase"].ToString() != null)
            {
                NewsBroadcastDA da = new NewsBroadcastDA();
                da.Update_NewsBroadcastUser_Read(_broadcastId, _userId, true);
            }
        }

        public List<AreaInfo> GetArea()
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_Area();
        }

        public List<BranchInfo> GetBranch(string _areaId)
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_Branch(_areaId);
        }

        public List<UserInfo> GetUser(string _roleId)
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_User(_roleId);
        }

        public List<RoleInfo> GetRole()
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_Role();
        }

        public void InsertNewsBroadcast(string _broadcastTopic, string _detail, DateTime _sentDt, DateTime _expireDt, int _cmdId)
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            da.Insert_NewsBroadcast(_broadcastTopic, _detail, _sentDt, _expireDt, _cmdId);
        }

        public void InsertNewsBroadcastUser(int _broadcastId, string _userId, string _areaId, string _branchId, string _branchName2, string _roleId, string _roleName)
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            da.Insert_NewsBroadcastUser(_broadcastId, _userId, _areaId, _branchId, _branchName2, _roleId, _roleName);
        }

        public int GetLastNewsBroadcastId()
        { 
            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_LastNewsBroadcastId();
        }

        public bool IsDuplicateUser(int _broadcastId, string _userId)
        {
            NewsBroadcastDA da = new NewsBroadcastDA();
            return da.Select_DuplicateUser(_broadcastId,_userId);
        }
    }
}
