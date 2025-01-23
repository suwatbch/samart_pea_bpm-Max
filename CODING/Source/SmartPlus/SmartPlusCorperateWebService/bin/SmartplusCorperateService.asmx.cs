using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using PEA.BPM.SmartPlus.BS;
using PEA.BPM.SmartPlus.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace SmartplusCorperateWebService
{
    /// <summary>
    /// Summary description for SmartPlusService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SmartplusCorperateWebService 
    {
        private SmartPlusBS Service = new SmartPlusBS();

        public SmartplusCorperateWebService()
        {
            Service = new SmartPlusBS();
        }
        

        [WebMethod]
        public ContractorAccountDetailModel SearchConAccountService(string CaId)
        {
            var result = new ContractorAccountDetailModel();
            result = Service.SearchConAccountService(CaId);
            return result;
        }

        [WebMethod]
        public List<ARInformationInfo> SearchContractorService(
          string CaId,
          string BillFlag,
          DateTime InterestDate)
        {
            List<ARInformationInfo> arInformationInfoList = new List<ARInformationInfo>();
            return this.Service.SearchContractorService(CaId, BillFlag, InterestDate);
        }        

        [WebMethod]
        public string UpdateBillMarkFlagService(string CaId, string InvoiceNo, string WsKey)
        {
            string Result = null;
            Result = Service.UpdateBillMarkFlagService(CaId, InvoiceNo, WsKey);
            return Result;
        }

        [WebMethod]
        public string CancelPaymentService(string CaId, string InvoiceNo, string WsKey)
        {
            string Result = null;
            Result = Service.CancelPaymentService(CaId, InvoiceNo, WsKey);
            return Result;
        }
       
    }
}
