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

namespace PEA.BPM.WebService.BPMIntegrationUploadedService
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
        public int UploadMaxBillSeqNo(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadMaxBillSeqNo(ServiceHelper.DecompressData<List<MaxBillSeqNoInfo>>(cds), branchId);
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

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadBillStatus(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadBillStatus(ServiceHelper.DecompressData<List<BillStatusInfo>>(cds), branchId);
        }

        //[SoapHeader("AuthInfoValue"), WebMethod]
        //public int UploadGreenReceiptPrintHistory(CompressData cds, string branchId)
        //{
        //    WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
        //    return new BPMServerService().UploadGreenReceiptPrintHistory(ServiceHelper.DecompressData<List<GreenReceiptPrintHistoryInfo>>(cds), branchId);
        //}

        #endregion
    }

}
