using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Threading;
using System.Collections;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;
using PaymentCollectionModule.SG;

namespace PEA.BPM.PaymentCollectionModule.Services 
{
    [Service(typeof(IPaidBillService))]
    public class PaidBillService : IPaidBillService
    {

        public PaidBillService()
        {

        }

        #region Service Factory
        public IPaidBillService GetService()
        {
            return GetService(false);
        }

        public IPaidBillService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new PaidBillSG(true);
            }
            else
            {
                return new PaidBillSG(false);
            }
        }
        #endregion

        #region IPaidBillService Member

        public List<Receipt> SearchPaidBill(PaidBillSearchParam param)
        {
            IPaidBillService bs = GetService();

            return bs.SearchPaidBill(param);
        }

        public List<Receipt> SearchReceipt(ReceiptSearchParam param)
        {
            IPaidBillService bs = GetService();

            return bs.SearchReceipt(param);
        }

        public ReceiptDetail GetReceiptDetail(string receiptId)
        {
            IPaidBillService bs = GetService();

            return bs.GetReceiptDetail(receiptId);
        }

        public CancelledInfo CancelReceipt(DbTransaction trans, List<string> receiptIds, string reason,
            string reprintReceiptId, string newReceiptId, string posId, string terminalCode, string branchId, string branchName,
            string cashierId, string cashierName, string workId)
        {
            IPaidBillService bs = GetService();

            return bs.CancelReceipt(null, receiptIds, reason, reprintReceiptId, newReceiptId, posId, terminalCode, branchId, branchName, cashierId, cashierName, workId);
        }

        public List<PrintingInfo> GetReceiptsForPrint(DbTransaction trans, List<string> receiptIds)
        {
            IPaidBillService bs = GetService();

            return bs.GetReceiptsForPrint(null, receiptIds);
        }

        public void IncreaseNoOfReprint(string receiptId)
        {
            IPaidBillService bs = GetService();

            bs.IncreaseNoOfReprint(receiptId);
        }


        public List<OneTouchLogInfo> SearchOneTouchPayment(List<string> receiptIds)
        {
            IPaidBillService bs = GetService();

            return bs.SearchOneTouchPayment(receiptIds);
        }

        public List<string> SearchPaymentTypeQR(List<string> paymentIds)
        {
            IPaidBillService bs = GetService();
            return bs.SearchPaymentTypeQR(paymentIds);
            
        }
        #endregion

        public bool ICSCancelReceipt(List<string> _receiptCancelICS)
        {
            IPaidBillService bs = GetService();
            return bs.ICSCancelReceipt(_receiptCancelICS);
        }

    }


}
