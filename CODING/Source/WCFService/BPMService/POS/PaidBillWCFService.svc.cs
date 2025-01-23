using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.BS;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.POS
{

    public class PaidBillWCFService : IPaidBillWCFService
    {
        private PaidBillBS _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public PaidBillWCFService()
        {
            _bs = new PaidBillBS();
        }

        public CompressData SearchPaidBill(PaidBillSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "PaidBillWCFService", "SearchPaidBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Receipt>>(_bs.SearchPaidBill(param));
                });
        }

        public CompressData SearchReceipt(ReceiptSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "PaidBillWCFService", "SearchReceipt",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressDataBF<List<Receipt>>(_bs.SearchReceipt(param));
                });
        }

        public CompressData GetReceiptDetail(string receiptId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "PaidBillWCFService", "GetReceiptDetail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<ReceiptDetail>(_bs.GetReceiptDetail(receiptId));
                });
        }

        public CompressData CancelReceipt(List<string> receiptIds, string reason, string reprintReceiptId, string newReceiptId, string posId, string terminalCode, string branchId, string branchName, string cashierId, string cashierName, string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "PaidBillWCFService", "CancelReceipt",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CancelledInfo>(_bs.CancelReceipt(null, receiptIds, reason, reprintReceiptId,
                                                                                       newReceiptId, posId, terminalCode, branchId, branchName, cashierId, cashierName, workId));
                });
        }

        public CompressData GetReceiptsForPrint(List<string> receiptIds)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "PaidBillWCFService", "GetReceiptsForPrint",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PrintingInfo>>(_bs.GetReceiptsForPrint(null, receiptIds));
                });
        }

        public void IncreaseNoOfReprint(string receiptId)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "PaidBillWCFService", "IncreaseNoOfReprint",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.IncreaseNoOfReprint(receiptId);
                });
        }


        public CompressData SearchOneTouchPayment(List<string> receiptIds)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "PaidBillWCFService", "SearchOneTouchPayment",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<OneTouchLogInfo>>(_bs.SearchOneTouchPayment(receiptIds));
                });
        }

        public CompressData SearchOneSearchPaymentTypeQR(List<string> paymentIds)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "PaidBillWCFService", "SearchOneTouchPayment",
               () =>
               {
                   WebServiceSecurity.CheckAuthorization(this.authInfo);
                   return ServiceHelper.CompressData<List<string>>(_bs.SearchPaymentTypeQR(paymentIds));
               });
        }


       
    }
}
