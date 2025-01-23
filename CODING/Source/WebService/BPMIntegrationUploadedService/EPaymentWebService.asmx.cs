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

namespace PEA.BPM.WebService.BPMIntegrationUploadedService
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
