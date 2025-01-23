using System;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.BS;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.WebService;
using WCFExtras.Soap;
using SAP.Middleware.Connector;


namespace BPMService.POS
{

    public class BillingWCFService : IBillingWCFService
    {
        private BillingBS _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public BillingWCFService()
        {
            _bs = new BillingBS();
        }

        public List<Invoice> SearchInvoiceByCustomerId(CustomerSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchInvoiceByCustomerId",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.SearchInvoiceByCustomerId(param);
                });
        }

        public CompressData SearchInvoiceByCustomerId_Compress(CustomerSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchInvoiceByCustomerId_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Invoice>>(_bs.SearchInvoiceByCustomerId(param));
                });
        }

        public CompressData SearchInvoiceFromSAP_Compress(SAPSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchInvoiceFromSAP_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Invoice>>(_bs.SearchInvoiceFromSAP(param));
                });
        }

        public CompressData SearchOriginalInvoiceByInstallmentItemCaDoc_Compress(OriginalInvoiceSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchOriginalInvoiceByInstallmentItemCaDoc_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<Invoice>(_bs.SearchOriginalInvoiceByInstallmentItemCaDoc(param));
                });
        }

        public CompressData SearchInstallmentInvoice_Compress(string caDoc)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchInstallmentInvoice_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<InstallmentInvoice>>(_bs.SearchInstallmentInvoice(caDoc));
                });
        }

        public CompressData SearchInvoiceByGroupInvoiceNo_Compress(GroupInvoiceSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchInvoiceByGroupInvoiceNo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Invoice>>(_bs.SearchInvoiceByGroupInvoiceNo(param));
                });
        }

        public CompressData SearchInvoiceItemByGroupInvoiceNo_Compress(InvoiceItemSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchInvoiceItemByGroupInvoiceNo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Invoice>>(_bs.SearchInvoiceItemByGroupInvoiceNo(param));
                });
        }

        public CompressData PayInvoice_Compress(CompressData cd)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "PayInvoice_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    PayInvoice os = ServiceHelper.DecompressData<PayInvoice>(cd);
                    ResultPayInvoice obj = _bs.PayInvoice(null,
                                                  os.PaidInvoices, os.PaymentMethods,
                                                  os.Receipts, os.GroupDividualReceipts, os.ExtReceipt,
                                                  os.PaymentDate, os.PosId, os.Terminalcode, os.BranchId, os.BranchName,
                                                  os.PayingAmount, os.ScreenType, os.CashierId, os.CashierName, os.WorkId, os.IsOffline);

                    return ServiceHelper.CompressData<ResultPayInvoice>(obj);
                });
        }

        public CompressData PayInvoice(PayInvoice os)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "PayInvoice_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    ResultPayInvoice obj = _bs.PayInvoice(null,
                                                  os.PaidInvoices, os.PaymentMethods,
                                                  os.Receipts, os.GroupDividualReceipts, os.ExtReceipt,
                                                  os.PaymentDate, os.PosId, os.Terminalcode, os.BranchId, os.BranchName,
                                                  os.PayingAmount, os.ScreenType, os.CashierId, os.CashierName, os.WorkId, os.IsOffline);

                    return ServiceHelper.CompressData<ResultPayInvoice>(obj);
                });
        }

        public CompressData SearchBillByCustomerDetail_Compress(CustomerSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchBillByCustomerDetail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<BillSearchDetail>>(_bs.SearchBillByCustomerDetail(param));
                });
        }

        public CompressData SearchBillByBillBookId_Compress(string billBookId, string billBookStatus)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchBillByBillBookId_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bill>>(_bs.SearchBillByBillBookId(billBookId, billBookStatus));
                });
        }

        public CompressData SearchBillByAgent_Compress(AgencySearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchBillByAgent_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<BookSearchDetail>>(_bs.SearchBillByAgent(param));
                });
        }

        public CompressData GetGroupInvoiceItem_Compress(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetGroupInvoiceItem_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<GroupInvoiceItem>>(_bs.GetGroupInvoiceItem(billBookId));
                });
        }

        public CaIdAndBranchId GetBranchIdByCaId(string caId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetBranchIdByCaId",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.GetBranchIdByCaId(caId);
                });
        }

        public Customer GetCustomerDetailOnPaymentHistory(HistoryViewParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetCustomerDetailOnPaymentHistory",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.GetCustomerDetailOnPaymentHistory(param);
                });
        }

        public CompressData GetPaymentHistory_Compress(HistoryViewParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetPaymentHistory_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PaidInvoice>>(_bs.GetPaymentHistory(param));
                });
        }

        public CompressData SearchBillBookByDetail_Compress(string billBookId, string agencyId, string agencyName)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchBillBookByDetail_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<BillBook>>(_bs.SearchBillBookByDetail(billBookId, agencyId, agencyName));
                });
        }

        public BillBook GetBillBookDetail(string billBookId, string statusLineStr)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetBillBookDetail",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.GetBillBookDetail(billBookId, statusLineStr);
                });
        }

        public CompressData SearchAdvancePaymentHistory_Compress(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchAdvancePaymentHistory_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<AdvancePaymentHistory>>(_bs.SearchAdvancePaymentHistory(billBookId));
                });
        }

        public CompressData SearchAGPayment_Compress(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchAGPayment_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PaymentMethod>>(_bs.SearchAGPayment(billBookId));
                });
        }

        public DayClosingResult CloseDayPayment(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "CloseDayPayment",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.CloseDayPayment(branchId);
                });
        }

        public CompressData SearchDisconnectionStatusByDiscNo_Compress(string discNo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchDisconnectionStatusByDiscNo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<DisconnectionDoc>>(_bs.SearchDisconnectionStatusByDiscNo(discNo));
                });
        }

        public bool CheckExistingInvoiceNo(string caId, string period)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "CheckExistingInvoiceNo",
                () =>
                {
                    return _bs.CheckExistingInvoiceNo(caId, period);
                });
        }

        public DateTime GetServerTime()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetServerTime",
                () =>
                {
                    return _bs.GetServerTime();
                });
        }

        public CompressData GetLastDisconnect(string caId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetLastDisconnect",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<LastDisconnect>(_bs.GetLastDisconnect(caId));
                });
        }

        public CompressData SearchPaymentNonReceipt(PaymentNonReceiptInfo receiptGen)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchPaymentNonReceipt",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PaymentNonReceiptInfo>>(_bs.SearchPaymentNonReceipt(receiptGen));
                });
        }

        public void CreateReceiptService(CompressData param)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "CreateReceiptService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.CreateReceiptService(ServiceHelper.DecompressData<List<PaymentNonReceiptInfo>>(param));
                });
        }

        public CompressData GetCaAndBpOtherBranch(string caId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetCaAndBpOtherBranch",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CaAndBpInfo>>(_bs.GetCaAndBpOtherBranch(caId));
                });
        }

        public void SaveCaAndBpOtherBranch(CompressData param)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SaveCaAndBpOtherBranch",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.SaveCaAndBpOtherBranch(ServiceHelper.DecompressData<List<CaAndBpInfo>>(param));
                });
        }

        public bool GetPaidGAmount(string invoiceNo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetPaidGAmount",
                () =>
                {
                    return _bs.GetPaidGAmount(invoiceNo);
                });
        }

        public bool GetInActiveBillBook(string invoiceNo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetInActiveBillBook",
                () =>
                {
                    return _bs.GetInActiveBillBook(invoiceNo);
                });
        }

        public bool GetDuplicateExtReceipt(string receiptId, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetDuplicateExtReceipt",
                () =>
                {
                    return _bs.GetDuplicateExtReceipt(receiptId, branchId);
                });
        }

        public bool ValidatePaymentActive(string receiptId, bool isPayment)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "ValidatePaymentActive",
                () =>
                {
                    return _bs.ValidatePaymentActive(receiptId, isPayment);
                });
        }

        public CompressData GetActivePayment(SAPSearchParam param, bool renew)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetActivePayment",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<ActivePayment>(_bs.GetActivePayment(param, renew));
                });
        }

        public void SaveActivePayment(ActivePayment acp)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SaveActivePayment",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.SaveActivePayment(acp);
                });
        }

        public void OfflineLog(OfflineLogInfo log)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "OfflineLog",
                () =>
                {
                    _bs.OfflineLog(log);
                });
        }

        public string CheckWorkStatus(OpenWorkParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "CheckWorkStatus",
                () =>
                {
                    return _bs.CheckWorkStatus(param);
                });
        }

        public TrayMoneyInfo GetMoneyInTray(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetMoneyInTray",
                () =>
                {
                    return _bs.GetMoneyInTray(workId);
                });
        }

        public CompressData GetRepayLastReceiptData(string receiptId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetRepayLastReceiptData",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<LastReceiptPayment>>(_bs.GetRepayLastReceiptData(receiptId));
                });
        }

        public void SaveOneTouchLog(OneTouchLogInfo log)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SaveOneTouchLog",
                () =>
                {
                    _bs.SaveOneTouchLog(log);
                });
        }

        public List<Invoice> SearchInvoiceFromOneTouch(OneTouchSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchInvoiceFromOneTouch",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.SearchInvoiceFromOneTouch(param);
                    //return ServiceHelper.CompressData<List<Invoice>>(_bs.SearchInvoiceFromOneTouch(param));
                });
        }

        public CompressData SearchInvoiceFromOneTouch_Compress(OneTouchSearchParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "SearchInvoiceFromOneTouch_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Invoice>>(_bs.SearchInvoiceFromOneTouch(param));
                });
        }

        public CompressData GetOfflinePayment_Compress(string branchId, string cashierId, string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "GetOfflinePayment_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<OfflinePayment>>(_bs.GetOfflinePayment(branchId, cashierId, workId));
                });
        }

        public void UpdateOfflinePayment(string branchId, string cashierId, string posId, string workId)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "UpdateOfflinePayment",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.UpdateOfflinePayment(branchId, cashierId, posId, workId);
                });
        }

        //// รวมใบเสร็จแผนผ่อน 2021-10-07 Check ใบเสร็จรวม Enable Status from ta.AppSetting
        public string CheckSettingGroupReceiptEnableStatus()
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "CheckSettingGroupReceiptEnableStatus",
                () =>
                {
                    return _bs.CheckSettingGroupReceiptEnableStatus();
                });
        }


        public string QRPaymentMethodByBranchId(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "BillingWCFService", "QRPaymentMethodByBranchId",
              () =>
              {
                  return _bs.QRPaymentMethodByBranchId(branchId);
              });
        }

    }
}
