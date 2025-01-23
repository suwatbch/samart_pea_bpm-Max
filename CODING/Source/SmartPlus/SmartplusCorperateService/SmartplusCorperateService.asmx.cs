using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using PEA.BPM.SmartPlus.BS;
using PEA.BPM.SmartPlus.Interface.BusinessEntities;
using System.Configuration;

namespace SmartplusCorperateService
{
    /// <summary>
    /// Summary description for SmartplusCorperateService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SmartplusCorperateService : System.Web.Services.WebService
    {
        private SmartPlusBS Service = new SmartPlusBS();


        public SmartplusCorperateService()
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
        public List<ARInformationInfo> SearchContractorServiceV2(
          string CaId,
          string BillFlag,
          DateTime InterestDate)
        {
            List<ARInformationInfo> arInformationInfoList = new List<ARInformationInfo>();
            string ServiceName  = "SearchContractorService";
            bool filter         = true;
            //// TRUE   -> CAN BE PROCESS
            //// FALSE  -> CAN'T
            filter = Service.RequestLogAndFilterPerform(ServiceName, CaId);
            if (filter == true)
            {
                return this.Service.SearchContractorServiceV2(CaId, BillFlag, InterestDate);
            }
            else
            {
                string SMARTPLUS_NOTICE_TEXT    = ConfigurationManager.AppSettings["SMARTPLUS_NOTICE_TEXT"]; 
                ARInformationInfo notice        = new ARInformationInfo();
                notice.Remark                   = SMARTPLUS_NOTICE_TEXT;
                arInformationInfoList.Add(notice);
                return arInformationInfoList;
            }
        }

        [WebMethod]
        public string UpdateBillMarkFlagServiceV2(string CaId, string InvoiceNo, string WsKey)
        {
            string Result       = null;
            string ServiceName  = "UpdateBillMarkFlagService";
            bool filter         = true;
            //// TRUE   -> CAN BE PROCESS
            //// FALSE  -> CAN'T
            filter = Service.RequestLogAndFilterPerform(ServiceName, CaId);
            if (filter == true)
            {  
                Result = Service.UpdateBillMarkFlagService(CaId, InvoiceNo, WsKey);                
            }
            else
            {
                string SMARTPLUS_NOTICE_TEXT = ConfigurationManager.AppSettings["SMARTPLUS_NOTICE_TEXT"];               
                Result                       = SMARTPLUS_NOTICE_TEXT; 
            }
            return Result;
        }


        [WebMethod]
        public List<ARInformationInfo> SearchContractorService(
          string    CaId,
          string    BillFlag,
          DateTime  InterestDate)
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
