using System;
using System.Collections.Generic;
using System.Text;

using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.PaymentCollectionModule.Interface.Services
{
    public interface IPaidBillService
    {
        List<Receipt> SearchPaidBill(PaidBillSearchParam param);
        List<Receipt> SearchReceipt(ReceiptSearchParam param);
        ReceiptDetail GetReceiptDetail(string receiptId);

        CancelledInfo CancelReceipt(DbTransaction trans, List<string> receiptIds, string reason, 
            string reprintReceiptId, string newReceiptId, string posId, string terminalCode, string branchId, string branchName,
            string cashierId, string cashierName, string workId);

        List<PrintingInfo> GetReceiptsForPrint(DbTransaction trans, List<string> receiptIds);
        void IncreaseNoOfReprint(string receiptId);

        List<OneTouchLogInfo> SearchOneTouchPayment(List<string> receiptIds);

        List<string> SearchPaymentTypeQR(List<string> paymentIds);

        //List<Repayment> SearchRepayment(RepaymentSearchParam param);

        //List<Repayment> UpdateRepaymentByStrLineAPId(string strLineAPId, string reason);
    }
}
