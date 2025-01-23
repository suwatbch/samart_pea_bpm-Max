using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.Fp410Receipt
{
    public class TaxReceipt: ReceiptBase
    {
        public static TaxReceipt Instance()
        {
            return new TaxReceipt();
        }

        public void Print(PrintingReceipt receipt, List<string> allReceiptsNo, List<PaymentMethod> paymentMethods)
        {
            List<object> printData = new List<object>();
            printData.Add(GetPrintHeader(receipt));
            printData.Add(GetBody(receipt, allReceiptsNo, paymentMethods));
            printData.Add(GetFooter(receipt));            
            Print(printData);            
        }

        private string GetBody(PrintingReceipt receipt,
            List<string> allReceiptsNo, List<PaymentMethod> paymentMethods)
        {
            StringBuilder sb = new StringBuilder();

            PrintingInvoice invoice = receipt.PrintingInvoices[0];
            Bill bill = invoice.Bills[0];
            string debtId = bill.DebtId;

            sb.AppendLine(string.Format("���� {0}", receipt.CustomerName));

            if (receipt.CaTaxId == null)
            {
                receipt.CaTaxId = "";
            }

            if (receipt.CaTaxBranch == null)
            {
                receipt.CaTaxBranch = "";
            }

            if (receipt.CaTaxId.Trim() == "")
            {

            }
            else
            {
                if (receipt.CaTaxBranch.Trim() == "")
                {
                    //sb.AppendLine(string.Format("Tax ID {0}", receipt.CaTaxId.Trim()));
                }
                else if (StringConvert.ToInt32(receipt.CaTaxBranch.Trim()) == 0)
                {
                    sb.AppendLine(string.Format("Tax ID {0} {1}", receipt.CaTaxId.Trim(), "�ӹѡ�ҹ�˭�"));
                }
                else
                {
                    sb.AppendLine(string.Format("Tax ID {0} �Ң� {1}", receipt.CaTaxId.Trim(), receipt.CaTaxBranch.Trim()));
                }
            }

            //sb.AppendLine(string.Format("���� {0}", receipt.CustomerName));
            sb.AppendLine(string.Format("������� {0}", receipt.CustomerAddress));
            if (null != invoice.BranchId)
            {
                sb.AppendLine(string.Format("{0} {1}",
                    invoice.BranchId, CodeTable.Instant.ListBranches(invoice.BranchId).BranchName));
            }
            if (receipt.ContractType == CodeNames.ContractType.Electric.Id)
            {
                sb.AppendLine(string.Format("�����Ţ�����俿�� {0}", receipt.DisplayCaId));
            }
            else
            {
                sb.AppendLine(string.Format("�����Ţ�ѭ���ʴ��ѭ�� {0}", receipt.DisplayCaId));
            }
            sb.AppendLine();

            //// DCR ��������Ἱ��͹ 㹡ó��������� ¡�ҹ�

            #region 
            //// �Թ��� M00171 ��зء�ѹ���������˹�Ҵ��� M0017             
            //if (debtId.Substring(0, 5) != "M0017" || debtId.Substring(0, 6) == "M00171")
            //{
            //    sb.AppendLine(string.Format("��¡�� {0}", bill.PrintDescription));

            //    if (null != invoice.Qty && 0 != invoice.Qty.Value)
            //    {
            //        if (debtId.Substring(0, 5) != "M0018")
            //        {
            //            sb.AppendLine(CreateLineString(
            //                "�ӹǹ",
            //                string.Format("{0} {1}", bill.Qty.Value.ToString("#,##0"), bill.UnitTypeName), columnLength));
            //        }

            //        if (bill.UnitPrice != null)
            //        {
            //            sb.AppendLine(CreateLineString(
            //                "�Ҥ�/˹���",
            //                string.Format("{0} �ҷ", bill.UnitPrice.Value.ToString("#,##0.00")), columnLength));
            //        }
            //    }
            //}
            //else
            //{
            //    if (debtId.Substring(0, 6) == "M00176")
            //    {
            //        sb.AppendLine(string.Format("��Ң���ࢵ ��ҧ�֧���˹���Ţ��� {0}\nŧ�ѹ��� {1}", invoice.DisplayInvoiceNo, GetThaiDate(invoice.InvoiceDate)));
            //    }
            //    else
            //    {
            //        sb.AppendLine(string.Format("��Һ�ԡ�� ��ҧ�֧���˹���Ţ��� {0}\nŧ�ѹ��� {1}", invoice.DisplayInvoiceNo, GetThaiDate(invoice.InvoiceDate)));
            //    }
            //}
            #endregion

            List<DebtType> dt = CodeTable.Instant.ListDebtTypes().FindAll(delegate(DebtType d) { return d.DebtId == debtId && d.PrintDescription != null && d.PrintDescription != ""; });

            if (dt.Count > 0)
            {
                string receiptType = bill.TaxCode == null || bill.TaxCode[0] == 'O' || bill.TaxRate == null ? "" : invoice.GAmount != invoice.ToPayGAmount ? "" : "��������´����Ṻ㺡ӡѺ����";

                sb.AppendLine(string.Format("��¡�� {0} {1}", dt[0].PrintDescription, receiptType));
            }
            else
            {
                if (debtId.Substring(0, 5) != "M0017" 
                    || debtId.Substring(0, 6) == "M00171" 
                    || debtId == "M00175800" // ��ҨѴ��þ�ѧ�ҹ
                    || debtId == "M99080012" // ��ҨѴ��þ�ѧ�ҹ (¡�ҹ�)
                ) 
                {
                    sb.AppendLine(string.Format("��¡�� {0}", bill.PrintDescription));

                    if (null != invoice.Qty && 0 != invoice.Qty.Value)
                    {
                        if (debtId.Substring(0, 5) != "M0018")
                        {
                            sb.AppendLine(CreateLineString(
                                "�ӹǹ",
                                string.Format("{0} {1}", bill.Qty.Value.ToString("#,##0"), bill.UnitTypeName), columnLength));
                        }

                        if (bill.UnitPrice != null)
                        {
                            sb.AppendLine(CreateLineString(
                                "�Ҥ�/˹���",
                                string.Format("{0} �ҷ", bill.UnitPrice.Value.ToString("#,##0.00")), columnLength));
                        }
                    }
                }
            }

            sb.AppendLine(CreateLineString(
                "�ӹǹ�Թ",
                string.Format("{0} �ҷ", invoice.AmountExVat.Value.ToString("#,##0.00")), columnLength));
            sb.AppendLine(CreateLineString(
                string.Format("������Ť������ {0}%", invoice.Bills[0].TaxRate.Value.ToString("##")),
                string.Format("{0} �ҷ", invoice.VatAmount.Value.ToString("#,##0.00")), columnLength));

            if (invoice.GAmount != invoice.ToPayGAmount) // ���ºҧ��ǹ
            {
                sb.AppendLine(CreateLineString(
                    "����Թ����ͧ����",
                    string.Format("{0} �ҷ", invoice.GAmount.Value.ToString("#,##0.00")),
                    columnLength));

                sb.AppendLine();
                sb.AppendLine("��ê����Թ");

                if ((debtId.Substring(0, 5) != "M0017" 
                    || debtId.Substring(0, 6) == "M00171"
                    || debtId == "M00175800" // ��ҨѴ��þ�ѧ�ҹ
                    || debtId == "M99080012" // ��ҨѴ��þ�ѧ�ҹ (¡�ҹ�)
                    ) && (invoice.ToPayQty != null) && (invoice.ToPayQty > 0))
                {
                    sb.AppendLine(CreateLineString("�ӹǹ",
                        string.Format("{0} {1}", invoice.ToPayQty.Value.ToString("#,##0.##"), invoice.Bills[0].UnitTypeName), columnLength));
                }


                if (invoice.InstallmentTotalPeriod != null && invoice.InstallmentTotalPeriod > 0)
                {
                    if (invoice.InstallmentPeriod == invoice.InstallmentTotalPeriod)
                    {
                        if (invoice.PartialPayment != null)
                        {
                            if (invoice.PartialPayment == 1)
                            {
                                sb.AppendLine(CreateLineString("�����Թ�ҧ��ǹ",
                                    string.Format("{0} �ҷ", invoice.ToPayExVatAmount.Value.ToString("#,##0.00")), columnLength));
                            }
                            else
                            {
                                sb.AppendLine(CreateLineString("�����Թ��ǹ��������",
                                    string.Format("{0} �ҷ", invoice.ToPayExVatAmount.Value.ToString("#,##0.00")), columnLength));
                            }
                        }
                        else
                        {
                            if (invoice.Bills[0].GAmount != invoice.ToPayGAmount)
                            {
                                sb.AppendLine(CreateLineString("�����Թ�ҧ��ǹ",
                                    string.Format("{0} �ҷ", invoice.ToPayExVatAmount.Value.ToString("#,##0.00")), columnLength));
                            }
                            else
                            {
                                sb.AppendLine(CreateLineString("�����Թ��ǹ��������",
                                    string.Format("{0} �ҷ", invoice.ToPayExVatAmount.Value.ToString("#,##0.00")), columnLength));
                            }
                        }
                    }
                    else
                    {
                        sb.AppendLine(CreateLineString("�����Թ�ҧ��ǹ",
                            string.Format("{0} �ҷ", invoice.ToPayExVatAmount.Value.ToString("#,##0.00")), columnLength));
                    }
                }
                else
                {
                    if (invoice.PartialPayment != null)
                    {
                        if (invoice.PartialPayment == 1)
                        {
                            sb.AppendLine(CreateLineString("�����Թ�ҧ��ǹ",
                                string.Format("{0} �ҷ", invoice.ToPayExVatAmount.Value.ToString("#,##0.00")), columnLength));
                        }
                        else
                        {
                            sb.AppendLine(CreateLineString("�����Թ��ǹ��������",
                                string.Format("{0} �ҷ", invoice.ToPayExVatAmount.Value.ToString("#,##0.00")), columnLength));
                        }
                    }
                    else
                    {
                        if (invoice.TotalPaidAmount != invoice.ToPayGAmount)
                        {
                            sb.AppendLine(CreateLineString("�����Թ�ҧ��ǹ",
                                string.Format("{0} �ҷ", invoice.ToPayExVatAmount.Value.ToString("#,##0.00")), columnLength));
                        }
                        else
                        {
                            sb.AppendLine(CreateLineString("�����Թ��ǹ��������",
                                string.Format("{0} �ҷ", invoice.ToPayExVatAmount.Value.ToString("#,##0.00")), columnLength));
                        }
                    }
                }


                sb.AppendLine(CreateLineString(
                    string.Format("������Ť������ {0}%", invoice.Bills[0].TaxRate.Value.ToString("##")),
                    string.Format("{0} �ҷ", invoice.ToPayVatAmount.Value.ToString("#,##0.00")), columnLength));
                sb.AppendLine(CreateLineString("����Թ������",
                    string.Format("{0} �ҷ", invoice.ToPayGAmount.Value.ToString("#,##0.00")), columnLength));
            }
            else
            {
                sb.AppendLine(CreateLineString(
                   "����Թ������",
                   string.Format("{0} �ҷ", invoice.GAmount.Value.ToString("#,##0.00")),
                   columnLength));
            }

            sb.Append(GetReceiptPayment(receipt, paymentMethods));
            sb.Append(GetTotalPayment(receipt, paymentMethods));
            sb.Append(GetReceiptSummary(receipt, allReceiptsNo));
            sb.Append(GetPaymentDate(receipt, bill));
            sb.Append(GetInvoiceReference(invoice));

            return sb.ToString();
        }        
    }
}
