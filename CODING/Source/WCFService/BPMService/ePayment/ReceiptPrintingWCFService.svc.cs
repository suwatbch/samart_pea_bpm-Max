using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.BS;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.ePayment
{

    public class ReceiptPrintingWCFService : IReceiptPrintingWCFService
    {
        private ReceiptPrintingBS _ePayReceiptPrintingSV;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public ReceiptPrintingWCFService()
        {
            _ePayReceiptPrintingSV = new ReceiptPrintingBS();
        }

        public CompressData PrintGreenBill(ReceiptConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReceiptPrintingWCFService", "PrintGreenBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_ePayReceiptPrintingSV.PrintGreenBill(param));
                });
        }

        public CompressData GetPPrintedService(PPrintedReceiptParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReceiptPrintingWCFService", "GetPPrintedService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PPrintedReceipt>>(_ePayReceiptPrintingSV.GetPPrintedService(param));
                });
        }

        public CompressData GetAgentAllService()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReceiptPrintingWCFService", "GetAgentAllService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Company>>(_ePayReceiptPrintingSV.GetAgentAllService());
                });
        }

        public CompressData SearchAgentPaymentNumber(EPayUpload param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReceiptPrintingWCFService", "SearchAgentPaymentNumber",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<EPayUpload>>(_ePayReceiptPrintingSV.SearchAgentPaymentNumber(param));
                });
        }

        public CompressData SearchDebtClearify(PPrintedDeposit param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReceiptPrintingWCFService", "SearchDebtClearify",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PPrintedDeposit>>(_ePayReceiptPrintingSV.SearchDebtClearify(param));
                });
        }

        public CompressData GetCADepositPPrinted(CompressData param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReceiptPrintingWCFService", "GetCADepositPPrinted",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PPrintedDeposit>>(_ePayReceiptPrintingSV.GetCADepositPPrinted(ServiceHelper.DecompressData<List<PPrintedDeposit>>(param)));
                });
        }

        public CompressData GetAgentDepositPPrinted(CompressData param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReceiptPrintingWCFService", "GetAgentDepositPPrinted",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PPrintedDeposit>>(_ePayReceiptPrintingSV.GetAgentDepositPPrinted(ServiceHelper.DecompressData<List<PPrintedDeposit>>(param)));
                });
        }

    }
}
