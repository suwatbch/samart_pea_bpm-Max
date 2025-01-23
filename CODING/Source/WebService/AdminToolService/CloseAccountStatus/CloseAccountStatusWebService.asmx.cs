using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace AdminToolService.CloseAccountStatus
{
    /// <summary>
    /// Summary description for CloseAccountStatusWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class CloseAccountStatusWebService : System.Web.Services.WebService
    {
        #region Variables
        private CloseAccountStatusBS _closeAccountStatusBS;
        #endregion

        #region Constructor
        public CloseAccountStatusWebService()
        {
            _closeAccountStatusBS = new CloseAccountStatusBS();
        }
        #endregion

        #region Method : GetCloseAccountStatusDisplay()
        /// <summary>
        /// รับข้อมูลการปิดบัญชีสำหรับแสดงผล
        /// </summary>
        /// <param name="datetime">วันที่ต้องการให้แสดงผล</param>
        /// <param name="status">สถานะของการปิดบัญชี</param>
        /// <returns>DataTable ของข้อมูลที่ได้</returns>
        [WebMethod]
        public DataTable GetCloseAccountStatusDisplay(DateTime datetime, string status, string region)
        {
            return _closeAccountStatusBS.GetCloseAccountStatusDisplay(datetime, status, region);
        }
        #endregion
    }
}