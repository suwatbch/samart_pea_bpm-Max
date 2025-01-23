using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.NewsBroadcast.Interface.Constants
{
    /// <summary>
    /// ใช้เก็บชื่อ Column ในแต่ละ DataTable
    /// </summary>
    public class ColumnInfo
    {  
        public struct NewsBroadcast
        {
            public static string BroadcastId = "BroadcastId";
            public static string BroadcastTopic = "BroadcastTopic";
            public static string Detail = "Detail";
            public static string SentDt = "SentDt";
            public static string ExpireDt = "ExpireDt";
            public static string BranchId = "BranchId";
            public static string CmdId = "CmdId";
            public static string IsOpened = "IsOpened";
            public static string IsRead = "IsRead";
            public static string ReadSymbol = "ReadSymbol";
        }

        public struct NewsBroadcastUser
        {
            public static string BroadcastId = "BroadcastId";
            public static string UserId = "UserId";
            public static string IsRead = "IsRead";
            public static string IsOpened = "IsOpened";
            public static string AreaId = "AreaId";
            public static string BranchId = "BranchId";
            public static string BranchName2 = "BranchName2";
            public static string RoleId = "RoleId";
            public static string RoleName = "RoleName";
            public static string ReadDt = "ReadDt";
            public static string OpenedDt = "OpenedDt";
            public static string ModifiedDt = "ModifiedDt";
            public static string Active = "Active";

        }

        public struct BranchArea
        { 
            public static string BranchId = "BranchId";
            public static string AreaId = "AreaId";
            public static string BranchName = "BranchName";
        }

        public struct Area
        {
            public static string AreaId = "AreaId";
            public static string AreaName = "AreaName";
        }

        public struct User
        {
            public static string UserId = "UserId";
            public static string FullName = "FullName";
            public static string BranchId = "BranchId";
            public static string PostBranchServerId = "PostBranchServerId";
        }

        public struct Role
        {
            public static string RoleId = "RoleId";
            public static string RoleName = "RoleName";
            public static string Description = "Description";
        }
    }
}
