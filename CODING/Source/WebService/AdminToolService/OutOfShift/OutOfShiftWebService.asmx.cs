using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace AdminToolService.OutOfShift
{
    /// <summary>
    /// Summary description for OutOfShiftWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class OutOfShiftWebService : System.Web.Services.WebService
    {

        #region Variables
        private OutOfShiftBS _outOfShiftBS;
        #endregion

        #region Constructor
        public OutOfShiftWebService()
        {
            _outOfShiftBS = new OutOfShiftBS();
        }
        #endregion

        #region Method : SelectOutOfShiftByDateTime
        /// <summary>
        /// การรับเงินที่มีปัญหา เกิดขึ้นนอกกะของแคชเชียร์ ตาม วันและรหัส
        /// </summary>
        /// <param name="datetime">วันที่ต้องการแสดงผล</param>
        /// <param name="caseCode">รหัส</param>
        /// <returns>DataTable ของข้อมูลที่ได้</returns>
        [WebMethod]
        public DataTable SelectOutOfShift(DateTime startDt, DateTime endDt, string caseCode)
        {
            return _outOfShiftBS.SelectOutOfShift(startDt, endDt, caseCode);
        }

        [WebMethod]
        public DateTime GetDBDateTime()
        {
            return _outOfShiftBS.GetDBDateTime();
        }

        [WebMethod]
        public DataTable SelectCountOutOfShift(DateTime stDt, string caseCode)
        {
            return _outOfShiftBS.SelectCountOutOfShift(stDt, caseCode);
        }

        #endregion
    }
}
