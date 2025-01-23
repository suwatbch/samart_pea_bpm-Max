using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Globalization;
using System.Configuration;

namespace SAP
{
    /// <summary>
    /// Summary description for SAPGateWayService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class SAPRfcService : System.Web.Services.WebService
    {

        [WebMethod]
        public DataSet GetDataFromSAP(string caId)
        {
            Object zCaDoc = null;
            string refDoc = "";
            string caNo = "";
            string posId = "";
            caNo = caId.PadLeft(12, '0');
            string action = "0";
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
            string startDate = DateTime.Now.AddDays(-Convert.ToInt32(ConfigurationManager.AppSettings["RFC_StartDate"])).ToString("yyyyMMdd", formatDate);

            SAPConnector sap = new SAPConnector();
            DataSet dsRet = sap.Zpos_Zcaci029("BPM_RFC", action, zCaDoc, caNo, posId, refDoc, startDate);
            return dsRet;
        }
    }
}
