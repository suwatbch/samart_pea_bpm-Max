using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Architecture.CommonUtilities;
namespace PEA.BPM.WebService.BPMIntegrationDownloadedService
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

    }
}