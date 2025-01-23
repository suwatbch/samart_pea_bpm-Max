using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace AdminToolService.ConnectionSetting
{
    /// <summary>
    /// Summary description for ConnectionSettingWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class ConnectionSettingWebService : System.Web.Services.WebService
    {
        #region Variables
        private ConnectionSettingBS _connectionSettingBS;
        #endregion

        #region Constructor
        public ConnectionSettingWebService()
        {
            _connectionSettingBS = new ConnectionSettingBS();
        }
        #endregion

        #region Method : GetConnectionSettingDisplay()
        /// <summary>
        /// รับข้อมูล ConnectionSetting สำหรับแสดงผล
        /// </summary>
        /// <param name="branchId">Branch ID ที่ต้องการแสดงผล (null = แสดงทั้งหมด)</param>
        /// <param name="active">0=แสดงผลข้อมูลที่ Inactive, 1=แสดงผลข้อมูลที่ Active, null=แสดงทั้งหมด</param>
        /// <returns>DataTable ของข้อมูลที่ได้</returns>
        [WebMethod]
        public DataTable GetConnectionSettingDisplay(string branchId, string active)
        {
            return _connectionSettingBS.GetConnectionSettingDisplay(branchId, active);
        }
        #endregion

        #region Method : GetConnectionSettingInfo()
        /// <summary>
        /// รับข้อมูลของ ConnectionSetting ที่กำหนด
        /// </summary>
        /// <param name="branchId">Branch ID ของ  ConnectionSetting ที่ต้องการ</param>
        /// <returns>ConnectionSetting ของข้อมูลที่ได้</returns>
        [WebMethod]
        public ConnectionSettingInfo GetConnectionSettingInfo(string branchId)
        {
            return _connectionSettingBS.GetConnectionSettingInfo(branchId);
        }
        #endregion

        #region Method : AddConnectionSetting()
        /// <summary>
        /// เพิ่ม ConnectionSetting ใหม่
        /// </summary>
        /// <param name="connectionSettingInfo">ข้อมูล ConnectionSetting ที่ต้องการเพิ่ม</param>
        /// <returns>จำนวนแถวของข้อมูลที่มีผล</returns>
        [WebMethod]
        public int AddConnectionSetting(ConnectionSettingInfo connectionSettingInfo)
        {
            return _connectionSettingBS.AddConnectionSetting(connectionSettingInfo);
        }
        #endregion

        #region Method : UpdateConnectionSetting()
        /// <summary>
        /// แก้ไข ConnectionSetting เดิม
        /// </summary>
        /// <param name="targetBranchId">Branch ID ของ  ConnectionSetting ที่ต้องการแก้ไข</param>
        /// <param name="connectionSettingInfo">ข้อมูล ConnectionSetting ที่แก้ไข</param>
        /// <returns>จำนวนแถวของข้อมูลที่มีผล</returns>
        [WebMethod]
        public int UpdateConnectionSetting(string targetBranchId, ConnectionSettingInfo connectionSettingInfo)
        {
            return _connectionSettingBS.UpdateConnectionSetting(targetBranchId, connectionSettingInfo);
        }
        #endregion

        #region Method : UpdateConnectionSetting()
        /// <summary>
        /// ลบกลุ่มของ ConnectionSetting ที่กำหนด
        /// </summary>
        /// <param name="branchIdList">กลุ่มของ Branch ID ที่ต้องการลบ คั่นด้วยเครื่องหมาย (,)</param>
        /// <returns>จำนวนแถวของข้อมูลที่มีผล</returns>
        [WebMethod]
        public int DeleteConnectionSettingByBranchIdList(string branchIdList)
        {
            return _connectionSettingBS.DeleteConnectionSettingByBranchIdList(branchIdList);
        }
        #endregion
    }
}