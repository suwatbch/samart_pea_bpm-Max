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
    /// Summary description for MasterIntegrationService_
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class MasterIntegrationWebService : BaseWebService
    {
        private BPMServerService _bs;

        public MasterIntegrationWebService()
        {
            _bs = new BPMServerService();
        }

       

        #region Download DL02 from BPM Server

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateBusinessPartner(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BusinessPartnerInfo>>(_bs.GetUpdateBusinessPartner(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateBranch(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BranchInfo>>(_bs.GetUpdateBranch(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateMRU(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<MRUInfo>>(_bs.GetUpdateMRU(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateMRUPlan(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<MRUPlanInfo>>(_bs.GetUpdateMRUPlan(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateContractAccount(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<ContractAccountInfo>>(_bs.GetUpdateContractAccount(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateEmployee(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<EmployeeInfo>>(_bs.GetUpdateEmployee(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateAgencyAsset(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<AgencyAssetInfo>>(_bs.GetUpdateAgencyAsset(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateBank(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BankInfo>>(_bs.GetUpdateBank(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateBankAccount(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BankAccountInfo>>(_bs.GetUpdateBankAccount(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateDebtType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<MainSubInfo>>(_bs.GetUpdateDebtType(branchId, lastModifiedDate, serverDate));
        }

        #endregion

        #region Upload DL02 from BPM Server

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadMRUPlan(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            return new BPMServerService().UploadMRUPlan(ServiceHelper.DecompressData<List<MRUPlanInfo>>(cds), branchId);
        }

        #endregion
    }
}

