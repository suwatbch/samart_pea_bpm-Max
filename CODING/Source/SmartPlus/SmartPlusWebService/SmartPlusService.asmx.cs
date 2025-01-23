using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using PEA.BPM.SmartPlus.BS;
using PEA.BPM.SmartPlus.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Configuration;

namespace SmartPlusService
{
    /// <summary>
    /// Summary description for SmartPlusService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SmartPlusService 
    {
        private SmartPlusBS Service = new SmartPlusBS();

        public SmartPlusService()
        {
            Service = new SmartPlusBS();
        }

        #region ..UPGRADE FROM V1 TO V2 (2022-05-30)

        [WebMethod]
        public List<ARInformationInfo> SearchContractorService(
          string CaId,
          string BillFlag)
        {
            List<ARInformationInfo> arInformationInfoList = new List<ARInformationInfo>();
            string ServiceName  = "SearchContractorService";
            bool filter = false;
            //// TRUE   -> CAN BE PROCESS
            //// FALSE  -> CAN'T
            //// SmartPlusBS svc = new SmartPlusBS();
            filter          = Service.RequestLogAndFilterPerform(ServiceName, CaId);
            //// filter              = CheckServiceAvailableByCaId(CaId, ServiceName);
            if (filter == true)
            {
                return this.Service.SearchContractorServiceV2(CaId, BillFlag);
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
        public List<ARInformationInfo> SearchContractorServiceV2(
          string CaId,
          string BillFlag,
          DateTime InterestDate)
        {
            List<ARInformationInfo> arInformationInfoList = new List<ARInformationInfo>();
            string ServiceName = "SearchContractorService";
            bool filter = true;
            //// TRUE   -> CAN BE PROCESS
            //// FALSE  -> CAN'T
            filter = Service.RequestLogAndFilterPerform(ServiceName, CaId);
            if (filter == true)
            {
                return this.Service.SearchContractorServiceV2(CaId, BillFlag, InterestDate);
            }
            else
            {
                string SMARTPLUS_NOTICE_TEXT = ConfigurationManager.AppSettings["SMARTPLUS_NOTICE_TEXT"];
                ARInformationInfo notice = new ARInformationInfo();
                notice.Remark = SMARTPLUS_NOTICE_TEXT;
                arInformationInfoList.Add(notice);
                return arInformationInfoList;
            }
        }


        [WebMethod]
        public string UpdateBillMarkFlagService(string CaId, string InvoiceNo, string WsKey)
        {
            string Result       = null;
            string ServiceName  = "UpdateBillMarkFlagService";
            bool filter         = true;
            //// TRUE   -> CAN BE PROCESS
            //// FALSE  -> CAN'T
            //// SmartPlusBS svc = new SmartPlusBS();
            filter              = Service.RequestLogAndFilterPerform(ServiceName, CaId);
            //// filter              = CheckServiceAvailableByCaId(CaId, ServiceName);
            if (filter == true)
            {
                Result = Service.UpdateBillMarkFlagService(CaId, InvoiceNo, WsKey);
            }
            else
            {
                string SMARTPLUS_NOTICE_TEXT = ConfigurationManager.AppSettings["SMARTPLUS_NOTICE_TEXT"];
                Result = SMARTPLUS_NOTICE_TEXT;
            }
            return Result;
        }

        [WebMethod]
        public ContractorAccountDetailModel SearchConAccountService(string CaId)
        {            
            string ServiceName  = "SearchConAccountService";
            bool filter         = true;
            filter              = CheckServiceAvailableByCaId(CaId, ServiceName);
            var result          = new ContractorAccountDetailModel();

            if (filter == true)
            {
                result = Service.SearchConAccountService(CaId);
            }
            else
            {
                string SMARTPLUS_NOTICE_TEXT = ConfigurationManager.AppSettings["SMARTPLUS_NOTICE_TEXT"];
                result.Remark = SMARTPLUS_NOTICE_TEXT;
            }

            return result;
        }

        [WebMethod]
        public string CancelPaymentService(string CaId, string InvoiceNo, string WsKey)
        {
            string Result      = null;
            string ServiceName = "CancelPaymentService";
            bool filter        = true;
            filter             = CheckServiceAvailableByCaId(CaId, ServiceName);
            
            if (filter == true)
            {
                Result = Service.CancelPaymentService(CaId, InvoiceNo, WsKey);
            }
            else
            {
                string SMARTPLUS_NOTICE_TEXT = ConfigurationManager.AppSettings["SMARTPLUS_NOTICE_TEXT"];
                Result                       = SMARTPLUS_NOTICE_TEXT;
            }
            
            return Result;
        }

        private bool CheckServiceAvailableByCaId(string caId, string serviceName)
        {
            bool isAvailable = true;
            try
            {
                //// SmartPlusBS bs  = new SmartPlusBS();
                isAvailable     = Service.RequestLogAndFilterPerform(serviceName, caId);
            }
            catch (Exception exp)
            {

                isAvailable = true;
            }

            return isAvailable;
        }

        #endregion


        #region ..V1 
        //[WebMethod]
        //public ContractorAccountDetailModel SearchConAccountService(string CaId)
        //{
        //    var result = new ContractorAccountDetailModel();
        //    result = Service.SearchConAccountService(CaId);
        //    return result;
        //}

        //[WebMethod]
        //public List<ARInformationInfo> SearchContractorService(
        //  string CaId,
        //  string BillFlag,
        //  DateTime? InterestDate)
        //{
        //    List<ARInformationInfo> arInformationInfoList = new List<ARInformationInfo>();
        //    return this.Service.SearchContractorService(CaId, BillFlag, InterestDate);
        //}        

        //[WebMethod]
        //public string UpdateBillMarkFlagService(string CaId, string InvoiceNo, string WsKey)
        //{
        //    string Result = null;
        //    Result = Service.UpdateBillMarkFlagService(CaId, InvoiceNo, WsKey);
        //    return Result;
        //}

        //[WebMethod]
        //public string CancelPaymentService(string CaId, string InvoiceNo, string WsKey)
        //{
        //    string Result = null;
        //    Result = Service.CancelPaymentService(CaId, InvoiceNo, WsKey);
        //    return Result;
        //}

        #endregion
    }
}
