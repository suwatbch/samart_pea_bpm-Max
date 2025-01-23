using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Architecture.CommonUtilities;
namespace PEA.BPM.WebService.BPMIntegrationUploadedService
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