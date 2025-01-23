using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using System.Collections.Generic;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Architecture.CommonUtilities;


namespace PEA.BPM.WebService.BPMIntegrationUploadedService
{
    /// <summary>
    /// Summary description for ServerUtilityWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UtilitiesIntegrationWebService : BaseWebService
    {
        private BPMServerService _bs;

        public UtilitiesIntegrationWebService()
        {
            _bs = new BPMServerService();
        }

        [WebMethod]
        public DateTime GetServerTime()
        {
            return _bs.GetServerTime();
        }

        

        #region Upload

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadUnlockLog(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return _bs.UploadUnlockLog(ServiceHelper.DecompressData<List<UnlockLogInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadRoleUser(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return _bs.UploadRoleUser(ServiceHelper.DecompressData<List<RoleUserInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadUser(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return _bs.UploadUser(ServiceHelper.DecompressData<List<UserInfo>>(cds), branchId);
        }

        #endregion

        #region SignalExport

        [WebMethod]
        public void SignalExport(string batchName, string branchId, string modifiedBy)
        {
            _bs.SignalExport(batchName, branchId, modifiedBy);
        }


        #endregion

    }
}

