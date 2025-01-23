using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace AdminToolService.OfflineErrorLog
{
    /// <summary>
    /// Summary description for OfflineErrorLogWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class OfflineErrorLogWebService : System.Web.Services.WebService
    {
        #region Variables
        private OfflineErrorLogBS _offlineErrorLogBS;
        #endregion

        #region Constructor
        public OfflineErrorLogWebService()
        {
            _offlineErrorLogBS = new OfflineErrorLogBS();
        }
        #endregion

        #region Method : GetOfflineErrorLogDisplay
        /// <summary>
        /// รับข้อมูล OfflineErrorLog สำหรับแสดงผล
        /// </summary>
        /// <param name="datetime">วันที่ต้องการแสดงผล</param>
        /// <param name="active">0=แสดงผลข้อมูลที่ Inactive, 1=แสดงผลข้อมูลที่ Active, null=แสดงทั้งหมด</param>
        /// <returns>DataTable ของข้อมูลที่ได้</returns>
        [WebMethod]
        public DataTable GetOfflineErrorLogDisplay(DateTime datetime, string active)
        {
            return _offlineErrorLogBS.GetOfflineErrorLogDisplay(datetime, active);
        }

        [WebMethod]
        public void UpdateLogStatus(string fileName, string status)
        {
            _offlineErrorLogBS.UpdateLogStatus(fileName, status);
        }

        #endregion
    }
}