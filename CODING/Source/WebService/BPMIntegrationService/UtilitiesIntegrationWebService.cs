using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using System.Collections.Generic;
using PEA.BPM.Integration.BPMIntegration.BS;
using PEA.BPM.Architecture.CommonUtilities;


namespace PEA.BPM.WebService.Integration
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

        #region Download

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadAppSetting(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<AppSettingInfo>>(_bs.DownloadAppSetting(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadTerminal(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<Terminal>>(_bs.DownloadTerminal(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadUnlockLog(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<UnlockLogInfo>>(_bs.DownloadUnlockLog(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadUser(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<UserInfo>>(_bs.DownloadUser(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadRole(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<RoleInfo>>(_bs.DownloadRole(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadFunction(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<FunctionInfo>>(_bs.DownloadFunction(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadService(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<ServiceInfo>>(_bs.DownloadService(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadRoleUser(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<RoleUserInfo>>(_bs.DownloadRoleUser(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadRoleFunction(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<RoleFunctionInfo>>(_bs.DownloadRoleFunction(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadFunctionService(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<FunctionServiceInfo>>(_bs.DownloadFunctionService(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadUnlockRemark(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<UnlockRemarkInfo>>(_bs.DownloadUnlockRemark(branchId, lastModifiedDt, serverDate));
        }

        #endregion

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

