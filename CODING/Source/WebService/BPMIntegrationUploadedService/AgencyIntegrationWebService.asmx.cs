using System;
using System.Web;
using System.Collections;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Web.Services;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;
using PEA.BPM.Integration.BPMIntegration.BS;


namespace PEA.BPM.WebService.BPMIntegrationUploadedService
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


        #region Upload Agency

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadBillBook(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return _bs.UploadBillBook(ServiceHelper.DecompressData<List<BillBookInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadBillStatusInfo(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return _bs.UploadBillStatusInfo(ServiceHelper.DecompressData<List<BillStatusInfoInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadBillBookDetail(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return _bs.UploadBillBookDetail(ServiceHelper.DecompressData<List<BillBookDetailInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadBillBookInputItem(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return _bs.UploadBillBookInputItem(ServiceHelper.DecompressData<List<BillBookInputItemInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadBillBookInputSet(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return _bs.UploadBillBookInputSet(ServiceHelper.DecompressData<List<BillBookInputSetInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadAgencyCommission(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return _bs.UploadAgencyCommission(ServiceHelper.DecompressData<List<AgencyCommissionInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadRTAgencyContractMru(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return _bs.UploadRTAgencyContractMru(ServiceHelper.DecompressData<List<RTAgencyContractMruInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadRTAgencyCommissionBillBook(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return _bs.UploadRTAgencyCommissionBillBook(ServiceHelper.DecompressData<List<RTAgencyCommissionBillBookInfo>>(cds), branchId);
        }

        #endregion
    }
}

