using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.BS;
using System.IO;

namespace PEA.BPM.WebService.Integration
{

    /// <summary>
    /// Summary description for BLANIntegrationWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class BLANIntegrationWebService : BaseWebService
    {
        private BPMServerService _bs;

        public BLANIntegrationWebService()
        {
            _bs = new BPMServerService();
        }

        #region Download

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdatePrintPool(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<PrintPoolInfo>>(_bs.GetUpdatePrintPool(branchId, modDate, serverTime));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateGrpPrintPool(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<GrpPrintPoolInfo>>(_bs.GetUpdateGrpPrintPool(branchId, modDate, serverTime));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateGreenReceiptDetail(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<GreenReceiptDetailInfo>>(_bs.GetUpdateGreenReceiptDetail(branchId, modDate, serverTime));
        }

        //[SoapHeader("AuthInfoValue"), WebMethod]
        //public CompressData GetUpdateGreenReceiptPrintHistory(string branchId, DateTime modDate, DateTime serverTime)
        //{
        //    WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

        //    return ServiceHelper.CompressData<List<GreenReceiptPrintHistoryInfo>>(_bs.GetUpdateGreenReceiptPrintHistory(branchId, modDate, serverTime));
        //}

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateMaxBillSeqNo(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<MaxBillSeqNoInfo>>(_bs.GetUpdateMaxBillSeqNo(branchId, modDate, serverTime));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateBillingDetail(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            try
            {
                return ServiceHelper.CompressData<List<BillingDetailInfo>>(_bs.GetUpdateBillingDetail(branchId, modDate, serverTime));
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter(@"d:\log.txt", true);
                sw.WriteLine(string.Format("{0} - {1}", DateTime.Now.ToString(), ex.ToString()));
                sw.Flush();
                sw.Close();

                throw;
            }
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdatePrintHistory(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<PrintHistoryInfo>>(_bs.GetUpdatePrintHistory(branchId, modDate, serverTime));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateDeliveryHistory(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<DeliveryHistoryInfo>>(_bs.GetUpdateDeliveryHistory(branchId, modDate, serverTime));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateApprover(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<ApproverInfo>>(_bs.GetUpdateApprover(branchId, modDate, serverTime));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateDeliveryPlace(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<DeliveryPlaceInfo>>(_bs.GetUpdateDeliveryPlace(branchId, modDate, serverTime));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdatePWBBillStatus(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<PWBBillStatusInfo>>(_bs.GetUpdatePWBBillStatus(branchId, modDate, serverTime));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateBillUpdate(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BillUpdateInfo>>(_bs.GetUpdateBillUpdate(branchId, modDate, serverTime));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateBarcodeMRU(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BarcodeMRUInfo>>(_bs.GetUpdateBarcodeMRU(branchId, modDate, serverTime));
        }
        
        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateBillStatus(string branchId, DateTime modDate, DateTime serverTime)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BillStatusInfo>>(_bs.GetUpdateBillStatus(branchId, modDate, serverTime));
        }

        #endregion

        #region Upload

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadPrintPool(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadPrintPool(ServiceHelper.DecompressData<List<PrintPoolInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadGrpPrintPool(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadGrpPrintPool(ServiceHelper.DecompressData<List<GrpPrintPoolInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadGreenReceiptDetail(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadGreenReceiptDetail(ServiceHelper.DecompressData<List<GreenReceiptDetailInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadPrintHistory(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadPrintHistory(ServiceHelper.DecompressData<List<PrintHistoryInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadDeliveryHistory(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadDeliveryHistory(ServiceHelper.DecompressData<List<DeliveryHistoryInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadMaxBillSeqNo(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadMaxBillSeqNo(ServiceHelper.DecompressData<List<MaxBillSeqNoInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadApprover(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadApprover(ServiceHelper.DecompressData<List<ApproverInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadDeliveryPlace(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadDeliveryPlace(ServiceHelper.DecompressData<List<DeliveryPlaceInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadBarcodeMRU(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadBarcodeMRU(ServiceHelper.DecompressData<List<BarcodeMRUInfo>>(cds), branchId);
        }   

        //[SoapHeader("AuthInfoValue"), WebMethod]
        //public int UploadBillSentTracking(CompressData cds, string branchId)
        //{
        //    WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
        //    return new BPMServerService().UploadDeliveryHistory(ServiceHelper.DecompressData<List<DeliveryHistoryInfo>>(cds), branchId);
        //}

        //[SoapHeader("AuthInfoValue"), WebMethod]
        //public int UploadGreenReceiptPrintHistory(CompressData cds, string branchId)
        //{
        //    WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
        //    return new BPMServerService().UploadGreenReceiptPrintHistory(ServiceHelper.DecompressData<List<GreenReceiptPrintHistoryInfo>>(cds), branchId);
        //}
        #endregion
    }
    
}
