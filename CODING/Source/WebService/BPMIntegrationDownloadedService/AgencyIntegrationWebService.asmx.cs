using System;
using System.Web;
using System.Collections;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Web.Services;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.BS;


namespace PEA.BPM.WebService.BPMIntegrationDownloadedService
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AgencyIntegrationWebService : BaseWebService
    {
        private BPMServerService _bs;

        public AgencyIntegrationWebService()
        {
            _bs = new BPMServerService();
        }

        #region Download Agency

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadBillBook(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BillBookInfo>>(_bs.GetUpdateBillBook(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadBillStatusInfo(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BillStatusInfoInfo>>(_bs.GetUpdateBillStatusInfo(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadBillBookDetail(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BillBookDetailInfo>>(_bs.GetUpdateBillBookDetail(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadBillBookInputItem(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BillBookInputItemInfo>>(_bs.GetUpdateBillBookInputItem(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadBillBookInputSet(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<BillBookInputSetInfo>>(_bs.GetUpdateBillBookInputSet(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadAgencyCommission(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<AgencyCommissionInfo>>(_bs.GetUpdateAgencyCommission(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadRTAgencyContractMru(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<RTAgencyContractMruInfo>>(_bs.GetUpdateRTAgencyContractMru(branchId, lastModifiedDt, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData DownloadRTAgencyCommissionBillBook(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<RTAgencyCommissionBillBookInfo>>(_bs.GetUpdateRTAgencyCommissionBillBook(branchId, lastModifiedDt, serverDate));
        }

        //[SoapHeader("AuthInfoValue"), WebMethod]
        //public CompressData DownloadAgencyModuleConfig(string branchId, DateTime lastModifiedDt, DateTime serverDate)
        //{
        //    WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

        //    return ServiceHelper.CompressData<List<AgencyModuleConfigInfo>>(_bs.GetUpdateAgencyModuleConfig(branchId, lastModifiedDt, serverDate));
        //}

        #endregion

     
    }
}

