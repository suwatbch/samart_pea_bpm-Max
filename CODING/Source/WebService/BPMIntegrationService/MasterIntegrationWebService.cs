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

        #region Download DL01 from BPM Server

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateNonWorkingDay(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<NonWorkingDayInfo>>(_bs.GetUpdateNonWorkingDay(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateAccountClass(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<AccountClassInfo>>(_bs.GetUpdateAccountClass(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateContractType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<ContractTypeInfo>>(_bs.GetUpdateContractType(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateMeterSize(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<MeterSizeInfo>>(_bs.GetUpdateMeterSize(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdatePaymentMethod(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<PaymentMethodInfo>>(_bs.GetUpdatePaymentMethod(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateTaxCode(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<TaxCodeInfo>>(_bs.GetUpdateTaxCode(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateUnitType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<UnitTypeInfo>>(_bs.GetUpdateUnitType(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateBusinessPartnerType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BusinessPartnerTypeInfo>>(_bs.GetUpdateBusinessPartnerType(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateCalendar(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<CalendarInfo>>(_bs.GetUpdateCalendar(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdatePaymentType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<PaymentTypeInfo>>(_bs.GetUpdatePaymentType(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateInterestKey(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<InterestKeyInfo>>(_bs.GetUpdateInterestKey(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateAgencyBillCollectionStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<AgencyBillCollectionStatusInfo>>(_bs.GetUpdateAgencyBillCollectionStatus(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateAgencyBillOutStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<AgencyBillOutStatusInfo>>(_bs.GetUpdateAgencyBillOutStatus(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateAgencyCommissionRate(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<AgencyCommissionRateInfo>>(_bs.GetUpdateAgencyCommissionRate(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateBillBookStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BillBookStatusInfo>>(_bs.GetUpdateBillBookStatus(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateAgencyCollectArea(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<AgencyCollectAreaInfo>>(_bs.GetUpdateAgencyCollectArea(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateDisconnectionStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<DisconnectionStatusInfo>>(_bs.GetUpdateDisconnectionStatus(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateCashierMoneyFlowType(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<CashierMoneyFlowTypeInfo>>(_bs.GetUpdateCashierMoneyFlowType(branchId, lastModifiedDate, serverDate));
        }

        #endregion

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

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdatePaymentSequence(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<PaymentSequenceInfo>>(_bs.GetUpdatePaymentSequence(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateOldCaMapping(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<OldCaMappingInfo>>(_bs.GetUpdateOldCaMapping(branchId, lastModifiedDate, serverDate));
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

