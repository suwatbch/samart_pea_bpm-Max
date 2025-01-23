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
                        
            sb.AppendLine(string.Format("���� {0}", receipt.CustomerName));
            sb.AppendLine(string.Format("������� {0}", receipt.CustomerAddress));
            if (null != invoice.BranchId)
            {
                sb.AppendLine(string.Format("{0} {1}",
                    invoice.BranchId, CodeTable.Instant.ListBranches(invoice.BranchId).BranchName));
            }
            sb.AppendLine(string.Format("�����Ţ���᷹ {0}", receipt.DisplayCaId));

            sb.AppendLine();
            sb.AppendLine("�óյѴ���Шҡ��ش���º��");
            sb.AppendLine("�Ţ���              ��.          ��Ѻ            �ҷ");

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
                    string.Format("�Թ������� {0} ��Ѻ", bill.BookTotalBillCollected.Value.ToString("#,##0")),
                    string.Format("{0} �ҷ", total.ToString("#,##0.00")),
                    columnLength));

                if (bill.BookAdvanceAmount != null)
                {
                    List<AdvancePaymentHistory> histories = _billingService.SearchAdvancePaymentHistory(bill.BillBookId);

                    sb.AppendLine("��ҧ�ԧ������Ѻ�Թ������ǧ˹�� 30%");
                    if (histories.Count > 0)
                    {
                        decimal? advanceAmount = 0;
                        foreach (AdvancePaymentHistory h in histories)
                        {
                            advanceAmount = advanceAmount + h.PaidGAmount.Value;
                            sb.AppendLine(CreateLineString(
                                string.Format("- {0} ��.{1}", h.ReceiptId, h.PaymentDt.Value.ToString("dd/MM/yy", new CultureInfo("th-TH"))),
                                string.Format(" {0} �ҷ", h.PaidGAmount.Value.ToString("#,##0.00")),
                                columnLength));
                        }
                    }
                }

                if (receipt.PrintingInvoices.Count > 1)
                {
                    sb.AppendLine(CreateLineString(
                        string.Format("����Թ�����Ţ��� {0}", bill.ShortBillBookId),
                        string.Format("{0} �ҷ", iv.ToPayGAmount.Value.ToString("#,##0.00")),
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
                        string.Format("�Թ���觢ͧ�Ţ��� {0}", iv.Bills[0].ShortBillBookId),
                        string.Format("{0} �ҷ", iv.ToPayGAmount.Value.ToString("#,##0.00")),
                        columnLength));
                }

                sb.AppendLine();
                sb.AppendLine(CreateLineString(
                    "����Թ���觷�����",
                    string.Format("{0} �ҷ", total.ToString("#,##0.00")),
                    columnLength));
            }
            else
            {

                sb.AppendLine();
                sb.AppendLine(CreateLineString(
                    "����Թ���觷�����",
                    string.Format("{0} �ҷ", invoice.ToPayGAmount.Value.ToString("#,##0.00")),
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
