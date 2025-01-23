using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.Architecture.ArchitectureTool;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.Fp410Receipt
{
    public class AGAdvanceReceipt: ReceiptBase
    {
        private IBillingService _billingService;

        public static AGAdvanceReceipt Instance()
        {
            return new AGAdvanceReceipt();
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
            Bill bill = invoice.Bills[0];

            sb.AppendLine(string.Format("���� {0}", receipt.CustomerName));
            sb.AppendLine(string.Format("������� {0}", receipt.CustomerAddress));
            if (null != invoice.BranchId)
            {
                sb.AppendLine(string.Format("{0} {1}",
                    invoice.BranchId, CodeTable.Instant.ListBranches(invoice.BranchId).BranchName));
            }
            sb.AppendLine(string.Format("�����Ţ���᷹ {0}", receipt.DisplayCaId));

            sb.AppendLine();
            sb.AppendLine("�ó��Ѻ�Թ������ǧ˹�������� 30");

            BillBook billBook = _billingService.GetBillBookDetail(bill.BillBookId, "N,T"); //billbook can be printed in 30% report although it's status is T.

            sb.AppendLine("��ش���º���Ţ���        ��.      ��Ѻ             �ҷ");
            sb.AppendLine(
                string.Format("{0}     {1}",
                billBook.ShortBillBookId, billBook.BookCreateDt.Value.ToString("dd/MM/yy", new CultureInfo("th-TH")))
                + "       " +
                CreateLineString(
                    billBook.TotalBill.Value.ToString("#,##0"),
                    billBook.BookTotalGAmount.Value.ToString("#,##0.00"), 19)
                );

            sb.AppendLine(CreateLineString(
                "�ʹ�Թ 30% ����ͧ����",
                string.Format("{0}", billBook.AdvancePayment.Value.ToString("#,##0.00")),
                columnLength));                     

            sb.AppendLine();
            sb.AppendLine(CreateLineString(
                "����Թ������",
                string.Format("{0} �ҷ", invoice.GAmount.Value.ToString("#,##0.00")),
                columnLength));

            sb.Append(GetReceiptPayment(receipt, paymentMethods));
            sb.Append(GetTotalPayment(receipt, paymentMethods));
            sb.Append(GetReceiptSummary(receipt, allReceiptsNo));
            sb.Append(GetPaymentDate(receipt, bill));

            return sb.ToString();
        }
    }
}
