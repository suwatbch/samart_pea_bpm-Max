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

    public class EPayBillingWCFService : IEPayBillingWCFService
    {
        private BillingBS _ePaymentBillingService;
        private ReceiptPrintingBS _ePayReceiptPrintingSV;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public EPayBillingWCFService()
        {
            _ePaymentBillingService = new BillingBS();
            _ePayReceiptPrintingSV = new ReceiptPrintingBS();
        }

        public string UpdateBillMarkFlagService(string caId, string invoiceNo, string modifiedBy, string postServerId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "UpdateBillMarkFlagService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    string pId = Session.Branch.PostedServerId;
                    return _ePaymentBillingService.UpdateBillMarkFlagService(caId.Trim(), invoiceNo.Trim(), modifiedBy, pId);
                });
        }

        public void DelBillMarkFlagService()
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "DelBillMarkFlagService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _ePaymentBillingService.DelBillMarkFlagService();
                });
        }

        public CompressData SearchDebtService(SearchDebtParam searchDebtParam)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "SearchDebtService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<ClearifyInfo>>(_ePaymentBillingService.SearchDebtService(searchDebtParam));
                });
        }

        public CompressData SearchBPMDebtClearify(SearchDebtParam searchDebtParam)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "SearchBPMDebtClearify",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<BPMClearifyInfo>>(_ePaymentBillingService.SearchBPMDebtClearify(searchDebtParam));
                });
        }

        public CompressData GetDebtComparableService(string caInvoceNo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "GetDebtComparableService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<EpayUploadItem>>(_ePaymentBillingService.GetDebtComparableService(caInvoceNo));
                });
        }

        public CompressData SearchCompany(Company compParm)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "SearchCompany",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Company>>(_ePaymentBillingService.SearchCompany(compParm));
                });
        }

        public void InsertEPayUploadService(CompressData ePayFileCompress)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "InsertEPayUploadService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _ePaymentBillingService.InsertEPayUploadService(ServiceHelper.DecompressData<List<EPaymentUploadFile>>(ePayFileCompress));
                });
        }

        public bool IsUploadFileNameExist(string fileName)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "IsUploadFileNameExist",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _ePaymentBillingService.IsUploadFileNameExist(fileName);
                });
        }

        public CompressData GetAgentPaymentService(AgentPayment agentPayment)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "GetAgentPaymentService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<AgentPayment>>(_ePaymentBillingService.GetAgentPaymentService(agentPayment));
                });
        }

        public void InsertAgentPaymentService(CompressData agentPayCompress)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "InsertAgentPaymentService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _ePaymentBillingService.InsertAgentPaymentService(ServiceHelper.DecompressData<List<AgentPayment>>(agentPayCompress));
                });
        }

        public CompressData PrintGreenBill(ReceiptConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "PrintGreenBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_ePayReceiptPrintingSV.PrintGreenBill(param));
                });
        }

        public bool SaveClearify(CompressData saveClearifyCompress)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "SaveClearify",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _ePaymentBillingService.SaveClearify(null, ServiceHelper.DecompressData<SaveClearifyInfo>(saveClearifyCompress));
                });
        }

        public CompressData SearchAgentPayment(CancelPayment param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "SearchAgentPayment",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CancelPayment>>(_ePaymentBillingService.SearchAgentPayment(param));
                });
        }

        public void InsertCancelPayment(CompressData param)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "EPayBillingWCFService", "InsertCancelPayment",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _ePaymentBillingService.InsertCancelPayment(ServiceHelper.DecompressData<List<CancelPayment>>(param));
                });
        }

    }
}
