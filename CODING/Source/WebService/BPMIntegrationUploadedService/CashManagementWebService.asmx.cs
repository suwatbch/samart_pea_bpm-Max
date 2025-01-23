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
    public class CashManagementWebService : BaseWebService
    {
        private BPMServerService _bs;

        public CashManagementWebService()
        {
            _bs = new BPMServerService();
        }


        #region Upload DL91 to server

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadCashierWorkStatus(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadCashierWorkStatus(ServiceHelper.DecompressData<List<CashierWorkStatusInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadCashierMoneyTransfer(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadCashierMoneyTransfer(ServiceHelper.DecompressData<List<CashierMoneyTransferInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadCashierMoneyFlow(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadCashierMoneyFlow(ServiceHelper.DecompressData<List<CashierMoneyFlowInfo>>(cds), branchId);
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public int UploadCashierMoneyFlowItem(CompressData cds, string branchId)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return new BPMServerService().UploadCashierMoneyFlowItem(ServiceHelper.DecompressData<List<CashierMoneyFlowItemInfo>>(cds), branchId);
        }

        #endregion

    }
}
