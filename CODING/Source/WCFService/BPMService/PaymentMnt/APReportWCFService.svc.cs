using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentManagementModule.BS;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.PaymentMnt
{

    public class APReportWCFService : IAPReportWCFService
    {
        private ReportBS _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public APReportWCFService()
        {
            _bs = new ReportBS();
        }

        public CompressData GetReportAP_Compress(APParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POSจ่ายเงิน, authInfo, "APReportWCFService", "GetReportAP_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<APReport>>(_bs.GetReportAP(param));
                });
        }

    }
}
