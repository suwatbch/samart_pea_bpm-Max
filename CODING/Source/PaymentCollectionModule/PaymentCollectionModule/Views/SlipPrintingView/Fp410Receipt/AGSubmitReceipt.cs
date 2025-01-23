using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.PaymentCollectionModule.Services;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.Architecture.ArchitectureTool;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.Fp410Receipt
{
    public class AGSubmitReceipt: ReceiptBase
    {
        private IBillingService _billingService;

        public static AGSubmitReceipt Instance()
        {
            return new AGSubmitReceipt();
        }

        public void Print(PrintingReceipt receipt, List<string> allReceiptsNo, List<PaymentMethod> paymentMethods,
            IBillingService billingService)
        {
            this._billingService = billingService;
                        
            List<object> printData = new List<object>();
            printData.Add(GetPrintHeader(receipt));
            printData.Add(GetBody(receipt, allReceiptsNo, paymentMethods));
            printData.Add(GetFooter(receipt));
            Print(printData); 
        }

        private string GetBody(PrintingReceipt receipt, List<string> allReceiptsNo, List<PaymentMethod> paymentMethods)
        {
            StringBuilder sb = new StringBuilder();

            PrintingInvoice invoice = receipt.PrintingInvoices[0];
                        
            sb.AppendLine(string.Format("ชื่อ {0}", receipt.CustomerName));
            sb.AppendLine(string.Format("ที่อยู่ {0}", receipt.CustomerAddress));
            if (null != invoice.BranchId)
            {
                sb.AppendLine(string.Format("{0} {1}",
                    invoice.BranchId, CodeTable.Instant.ListBranches(invoice.BranchId).BranchName));
            }
            sb.AppendLine(string.Format("หมายเลขตัวแทน {0}", receipt.DisplayCaId));

            sb.AppendLine();
            sb.AppendLine("กรณีตัดชำระจากสมุดจ่ายบิล");
            sb.AppendLine("เลขที่              ลว.          ฉบับ            บาท");

            foreach (PrintingInvoice iv in receipt.PrintingInvoices)
            {
                Bill bill = iv.Bills[0];

                sb.AppendLine(
                    string.Format("{0}        {1}",
                    bill.ShortBillBookId, bill.BookCreateDt.Value.ToString("dd/MM/yy", new CultureInfo("th-TH")))
                    + "        " +
                    CreateLineString(
                        bill.BookTotalBill.Value.ToString("#,##0"),
                        bill.BookTotalGAmount.Value.ToString("#,##0.00"), 16)
                    );

                decimal total = bill.BookAdvanceAmount.Value + iv.ToPayGAmount.Value;

                sb.AppendLine(CreateLineString(
                    string.Format("เงินนำส่งรวม {0} ฉบับ", bill.BookTotalBillCollected.Value.ToString("#,##0")),
                    string.Format("{0} บาท", total.ToString("#,##0.00")),
                    columnLength));

                if (bill.BookAdvanceAmount != null)
                {
                    List<AdvancePaymentHistory> histories = _billingService.SearchAdvancePaymentHistory(bill.BillBookId);

                    sb.AppendLine("อ้างอิงใบเสร็จรับเงินนำส่งล่วงหน้า 30%");
                    if (histories.Count > 0)
                    {
                        decimal? advanceAmount = 0;
                        foreach (AdvancePaymentHistory h in histories)
                        {
                            advanceAmount = advanceAmount + h.PaidGAmount.Value;
                            sb.AppendLine(CreateLineString(
                                string.Format("- {0} ลว.{1}", h.ReceiptId, h.PaymentDt.Value.ToString("dd/MM/yy", new CultureInfo("th-TH"))),
                                string.Format(" {0} บาท", h.PaidGAmount.Value.ToString("#,##0.00")),
                                columnLength));
                        }
                    }
                }

                if (receipt.PrintingInvoices.Count > 1)
                {
                    sb.AppendLine(CreateLineString(
                        string.Format("รวมเงินนำส่งเลขที่ {0}", bill.ShortBillBookId),
                        string.Format("{0} บาท", iv.ToPayGAmount.Value.ToString("#,##0.00")),
                        columnLength));
                    sb.AppendLine();
                }
            }


            if (receipt.PrintingInvoices.Count > 1)
            {
                decimal total = 0;
                foreach (PrintingInvoice iv in receipt.PrintingInvoices)
                {
                    total += iv.ToPayGAmount.Value;
                    sb.AppendLine(CreateLineString(
                        string.Format("เงินนำส่งของเลขที่ {0}", iv.Bills[0].ShortBillBookId),
                        string.Format("{0} บาท", iv.ToPayGAmount.Value.ToString("#,##0.00")),
                        columnLength));
                }

                sb.AppendLine();
                sb.AppendLine(CreateLineString(
                    "รวมเงินนำส่งทั้งสิ้น",
                    string.Format("{0} บาท", total.ToString("#,##0.00")),
                    columnLength));
            }
            else
            {

                sb.AppendLine();
                sb.AppendLine(CreateLineString(
                    "รวมเงินนำส่งทั้งสิ้น",
                    string.Format("{0} บาท", invoice.ToPayGAmount.Value.ToString("#,##0.00")),
                    columnLength));

            }
                        
            sb.Append(GetReceiptPayment(receipt, paymentMethods));
            sb.Append(GetTotalPayment(receipt, paymentMethods));
            sb.Append(GetReceiptSummary(receipt, allReceiptsNo));
            sb.Append(GetPaymentDate(receipt, invoice.Bills[0]));

            return sb.ToString();
        }
    }
}
