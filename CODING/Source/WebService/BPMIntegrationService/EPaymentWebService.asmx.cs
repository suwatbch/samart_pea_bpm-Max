using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.WebService.Integration;

namespace BPMIntegrationService
{
    /// <summary>
    /// Summary description for EPaymentWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class EPaymentWebService : BaseWebService
    {
        private BPMServerService _bs;

        public EPaymentWebService()
        {
            _bs = new BPMServerService();
        }

        #region Download DL61 from BPM Server

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateEPayClarify(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<EPayClarifyInfo>>(_bs.GetUpdateEPayClarify(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateEPayUpload(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<EPayUploadInfo>>(_bs.GetUpdateEPayUpload(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateEPayUploadItem(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<EPayUploadItemInfo>>(_bs.GetUpdateEPayUploadItem(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateRTEPayUploadPayment(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<RTEPayUploadPaymentInfo>>(_bs.GetUpdateRTEPayUploadPayment(branchId, lastModifiedDate, serverDate));
        }

        #endregion

        #region Upload DL101 to server

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadEPayClarify(CompressData cds, string branchId)
        {          
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadEPayClarify(ServiceHelper.DecompressData<List<EPayClarifyInfo>>(cds), branchId);
        }

        #endregion
    }
}
