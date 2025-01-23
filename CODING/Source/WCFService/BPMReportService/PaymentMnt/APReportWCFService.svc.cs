using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using WCFExtras.Soap;
using BPMLINQReport;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;


namespace BPMReportService.PaymentMnt
{

    public class APReportWCFService : IAPReportWCFService
    {
        private PAMReport _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public APReportWCFService()
        {
            _bs = new PAMReport();
        }

        public CompressData GetReportAP_Compress(APParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POSจ่ายเงิน, authInfo, "APReportWCFService", "GetReportAP_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<APReport>>(_bs.GetReportPAM33(param.BranchId, param.posId, param.cashierId, param.TransFromDate, param.TransToDate));
                });
        }

    }
}
