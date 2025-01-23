using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Architecture.CommonUtilities;
namespace PEA.BPM.WebService.Integration
{

    /// <summary>
    /// Summary description for ARWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ARWebService : BaseWebService
    {
        public ARWebService()
        {
            
        }

        #region Download Transaction
        
        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadAR(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<AR>>(new BPMServerService().GetUpdateAR(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadARPayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<ARPayment>>(new BPMServerService().GetUpdateARPayment(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadPayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<Payment>>(new BPMServerService().GetUpdatePayment(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadARPaymentType(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<ARPaymentType>>(new BPMServerService().GetUpdateARPaymentType(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadRTARPaymentTypeARPayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<RTARPaymentTypeARPayment>>(new BPMServerService().GetUpdateRTARPaymentTypeARPayment(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadReceipt(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<Receipt>>(new BPMServerService().GetUpdateReceipt(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadReceiptItem(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<ReceiptItem>>(new BPMServerService().GetUpdateReceiptItem(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadDisconnectionDoc(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<DisconnectionDocInfo>>(new BPMServerService().GetUpdateDisconnectionDoc(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadRTDisconnectionDocCaDoc(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<RTDisconnectionDocCaDocInfo>>(new BPMServerService().GetUpdateRTDisconnectionDocCaDoc(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadAP(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<APInfo>>(new BPMServerService().GetUpdateAP(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadAPChequePayment(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<APChequePaymentInfo>>(new BPMServerService().GetUpdateAPChequePayment(branchId, lastModifiedDt, serverDate));
        }

        #endregion

        #region Upload Transaction

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadAR(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadAR(ServiceHelper.DecompressData<List<AR>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadARPayment(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadARPayment(ServiceHelper.DecompressData<List<ARPayment>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadPayment(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadPayment(ServiceHelper.DecompressData<List<Payment>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadARPaymentType(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadARPaymentType(ServiceHelper.DecompressData<List<ARPaymentType>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadRTARPaymentTypeARPayment(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadRTARPaymentTypeARPayment(ServiceHelper.DecompressData<List<RTARPaymentTypeARPayment>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadReceipt(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadReceipt(ServiceHelper.DecompressData<List<Receipt>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadReceiptItem(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadReceiptItem(ServiceHelper.DecompressData<List<ReceiptItem>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadPaymentLog(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadPaymentLog(ServiceHelper.DecompressData<List<PaymentLogInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadAP(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadAP(ServiceHelper.DecompressData<List<APInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadAPChequePayment(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadAPChequePayment(ServiceHelper.DecompressData<List<APChequePaymentInfo>>(cds), branchId);
        }

        #endregion
    }    
}