using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using BPMSAPConnector;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using System.Configuration;

namespace SAPGateWayService
{
    /// <summary>
    /// Summary description for SAPGateWayService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class SAPGateWayService : System.Web.Services.WebService
    {

        [WebMethod]
        public CompressData GetDataFromSAP(string caId, string conn)
        {
            BPMSAPProxy sapConn = new BPMSAPProxy(conn);

            ZCA_MTR0060Table z60 = new ZCA_MTR0060Table();
            ZCA_MTR0090Table z90 = new ZCA_MTR0090Table();
            ZCA_TRR0010Table z10 = new ZCA_TRR0010Table();
            ZCA_TRR0020Table z20 = new ZCA_TRR0020Table();
            ZCA_TRR0040Table z40 = new ZCA_TRR0040Table();
            ZCA_TRR0045Table z45 = new ZCA_TRR0045Table();

            ZCADOC zCaDoc = null;
            string refDoc = "";
            string caNo = "";
            string posId = "";
            caNo = caId.PadLeft(12, '0');

            string action = "0";
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
            string startDate = DateTime.Now.AddDays(-Convert.ToInt32(ConfigurationManager.AppSettings["RFC_StartDate"])).ToString("yyyyMMdd", formatDate);

            sapConn.Zpos_Zcaci029(action, zCaDoc, caNo, posId, refDoc, startDate, ref z60, ref z90, ref z10, ref z20, ref z40, ref z45);

            DataSet dsRet = new DataSet();
            dsRet.Tables.Add(z60.ToADODataTable());
            dsRet.Tables.Add(z90.ToADODataTable());
            dsRet.Tables.Add(z10.ToADODataTable());
            dsRet.Tables.Add(z20.ToADODataTable());
            dsRet.Tables.Add(z40.ToADODataTable());
            dsRet.Tables.Add(z45.ToADODataTable());

            return ServiceHelper.CompressDataBF<DataSet>(dsRet);
        }
    }
}
