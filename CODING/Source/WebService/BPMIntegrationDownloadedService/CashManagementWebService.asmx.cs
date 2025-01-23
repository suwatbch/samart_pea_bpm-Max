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

        #region Download DL51 from BPM Server

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateCashierWorkStatus(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<CashierWorkStatusInfo>>(_bs.GetUpdateCashierWorkStatus(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateCashierMoneyTransfer(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<CashierMoneyTransferInfo>>(_bs.GetUpdateCashierMoneyTransfer(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateCashierMoneyFlow(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<CashierMoneyFlowInfo>>(_bs.GetUpdateCashierMoneyFlow(branchId, lastModifiedDate, serverDate));
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public CompressData GetUpdateCashierMoneyFlowItem(string branchId, DateTime lastModifiedDate, DateTime serverDate)
        {
            WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            return ServiceHelper.CompressData<List<CashierMoneyFlowItemInfo>>(_bs.GetUpdateCashierMoneyFlowItem(branchId, lastModifiedDate, serverDate));
        }

        #endregion  

    }
}
