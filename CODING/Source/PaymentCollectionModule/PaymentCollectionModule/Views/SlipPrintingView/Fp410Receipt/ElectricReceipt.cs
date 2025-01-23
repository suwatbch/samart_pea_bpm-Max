using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.Architecture.ArchitectureTool;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.Fp410Receipt
{
    public class ElectricReceipt: ReceiptBase
    {
        public static ElectricReceipt Instance()
        {
            return new ElectricReceipt();
        }

        public void Print(PrintingReceipt receipt, List<string> allReceiptsNo, List<PaymentMethod> paymentMethods)
        {
            List<object> printData = new List<object>();
            printData.Add(GetPrintHeader(receipt));
            printData.Add(GetBody(receipt, allReceiptsNo, paymentMethods));
            printData.Add(GetFooter(receipt));
            Print(printData);            
        }

        private string GetAmountString(decimal? value)
        {
            if (value == null)
            {
                return "-";
            }
            else
            {
                return value.Value.ToString("#,##0.00");
            }
        }

        private string GetAmountString4(decimal? value)
        {
            if (value == null)
            {
                return "-";
            }
            else
            {
                return value.Value.ToString("#,##0.####");
            }
        }
        
        private string GetBody(PrintingReceipt receipt, List<string> allReceiptsNo, List<PaymentMethod> paymentMethods)
        {
            string GroupReceiptReplaceZeroBy = "-";

            StringBuilder sb = new StringBuilder();

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
                receipt.CaTaxId = "";
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

            PrintingInvoice invoice = receipt.PrintingInvoices[0];
            Bill bill = invoice.Bills[0];
            
            if (invoice.DataState == InvoiceDataStage.Invoice && bill.MeterId != null )
            {
                //// DCR ��������Ἱ��͹ 2021AUG �͡����稪����Թ�����á (POS SLIP)
                if(receipt.GroupReceiptOrNot == "Y")
                {
                    if (bill.DisplayMeterId != null && bill.RateTypeId != null)
                    {
                        sb.AppendLine(string.Format("��������ͧ�Ѵ {0} �������ѵ�� {1}", bill.DisplayMeterId, bill.RateTypeId));
                    }
                    else
                    {
                        sb.AppendLine("");
                    }
                    if (null != invoice.BranchId)
                    {
                        sb.AppendLine(string.Format("{0} {1}",
                            invoice.BranchId, CodeTable.Instant.ListBranches(invoice.BranchId).BranchName));
                    }
                    sb.AppendLine(string.Format("�����Ţ�����俿�� {0}", receipt.DisplayCaId));
                    sb.AppendLine(receipt.GroupReceiptPeriodText);
                    sb.AppendLine(""); // ᷹�Ţ���駡�͹������ѧ
                    //sb.AppendLine(string.Format("��Ш���͹ {0} �ѹ�����ҹ˹��� {1}", StringConvert.FormatPeriod(bill.Period), GetThaiDate(bill.MeterReadDate)));
                    //sb.AppendLine(string.Format("�Ţ��ҹ������ѧ {0} �Ţ��ҹ���駡�͹ {1}", "-", "-"));
                    sb.AppendLine(CreateLineString("˹��·����", string.Format("{0} ˹���", GetAmountString4(receipt.GroupReceiptQty)), columnLength));
                    sb.AppendLine(CreateLineString("���俿�Ұҹ", string.Format("{0} �ҷ", GroupReceiptReplaceZeroBy), columnLength));
                    sb.AppendLine(CreateLineString(
                        string.Format("��� FT {0} �ҷ/˹���", GroupReceiptReplaceZeroBy),
                        string.Format("{0} �ҷ", GroupReceiptReplaceZeroBy), columnLength));
                }
                else //// POS Slip ����
                {
                    sb.AppendLine(string.Format("��������ͧ�Ѵ {0} �������ѵ�� {1}", bill.DisplayMeterId, bill.RateTypeId));
                    if (null != invoice.BranchId)
                    {
                        sb.AppendLine(string.Format("{0} {1}",
                            invoice.BranchId, CodeTable.Instant.ListBranches(invoice.BranchId).BranchName));
                    }
                    sb.AppendLine(string.Format("�����Ţ�����俿�� {0}", receipt.DisplayCaId));
                    sb.AppendLine(string.Format("��Ш���͹ {0} �ѹ�����ҹ˹��� {1}", StringConvert.FormatPeriod(bill.Period), GetThaiDate(bill.MeterReadDate)));
                    sb.AppendLine(string.Format("�Ţ��ҹ������ѧ {0} �Ţ��ҹ���駡�͹ {1}", FormatUnit(bill.LastUnit), FormatUnit(bill.PreviousUnit)));
                    sb.AppendLine(CreateLineString("˹��·����", string.Format("{0} ˹���", GetAmountString4(invoice.Qty)), columnLength));
                    sb.AppendLine(CreateLineString("���俿�Ұҹ", string.Format("{0} �ҷ", GetAmountString(bill.BaseAmount)), columnLength));
                    sb.AppendLine(CreateLineString(
                        string.Format("��� FT {0} �ҷ/˹���", GetAmountString4(bill.FtUnitPrice)),
                        //string.Format("���FT{0}�ҷ/˹���", "+PE=0.0139"),
                        string.Format("{0} �ҷ", GetAmountString(bill.FtAmount)), columnLength));
                }
            }
            else
            {
                #region //// DCR ��������Ἱ��͹ 2021AUG (POS SLIP)
                if (receipt.GroupReceiptOrNot == "Y")
                {
                    string tmpDisplayMeterId = null;
                    try
                    {
                        tmpDisplayMeterId = bill.DisplayMeterId;
                    }
                    catch (Exception ex)
                    {
                        string debugX = "";
                    }

                    //// sb.AppendLine(string.Format("��������ͧ�Ѵ {0} �������ѵ�� {1}", bill.DisplayMeterId, bill.RateTypeId));
                    if (tmpDisplayMeterId != null && bill.RateTypeId != null)
                    {
                        sb.AppendLine(string.Format("��������ͧ�Ѵ {0} �������ѵ�� {1}", bill.DisplayMeterId, bill.RateTypeId));
                    }
                    else
                    {
                        sb.AppendLine("");
                    }

                    if (null != invoice.BranchId)
                    {
                        sb.AppendLine(string.Format("{0} {1}",
                            invoice.BranchId, CodeTable.Instant.ListBranches(invoice.BranchId).BranchName));
                    }
                    sb.AppendLine(string.Format("�����Ţ�����俿�� {0}", receipt.DisplayCaId));
                    sb.AppendLine(receipt.GroupReceiptPeriodText);
                    sb.AppendLine(""); // ᷹�Ţ���駡�͹������ѧ
                    //sb.AppendLine(string.Format("�Ţ��ҹ������ѧ {0} �Ţ��ҹ���駡�͹ {1}", "-", "-"));
                    sb.AppendLine(CreateLineString("˹��·����", string.Format("{0} ˹���", GetAmountString4(receipt.GroupReceiptQty)), columnLength));
                    sb.AppendLine(CreateLineString("���俿�Ұҹ", string.Format("{0} �ҷ", GetAmountString(receipt.GroupReceiptAmount)), columnLength));
                    sb.AppendLine(CreateLineString(
                        string.Format("��� FT {0} �ҷ/˹���", GroupReceiptReplaceZeroBy),
                        string.Format("{0} �ҷ", GroupReceiptReplaceZeroBy), columnLength));
                }
                #endregion
                else
                {
                    sb.AppendLine(string.Format("�����Ţ�����俿�� {0}", receipt.DisplayCaId));
                    //[XX]: Round1
                    sb.AppendLine(string.Format("��Ш���͹ {0}", StringConvert.FormatPeriod(bill.Period)));
                    sb.AppendLine(CreateLineString("˹��·����", string.Format("{0} ˹���", GetAmountString4(invoice.Qty)), columnLength));
                }
            }

            //// DCR ��������Ἱ��͹ 2021AUG (POS SLIP)
            if (receipt.GroupReceiptOrNot == "Y")
            {
                sb.AppendLine(CreateLineString("����Թ���俿��", string.Format("{0} �ҷ", GroupReceiptReplaceZeroBy), columnLength));
                sb.AppendLine(CreateLineString(
                string.Format("������Ť������ {0}%", GroupReceiptReplaceZeroBy),
                string.Format("{0} �ҷ", GroupReceiptReplaceZeroBy), columnLength));
            }
            else
            {
                sb.AppendLine(CreateLineString("����Թ���俿��", string.Format("{0} �ҷ", GetAmountString(invoice.AmountExVat)), columnLength));
                sb.AppendLine(CreateLineString(
                string.Format("������Ť������ {0}%", bill.TaxRate.Value.ToString("##")),
                string.Format("{0} �ҷ", GetAmountString(invoice.VatAmount)), columnLength));
            }

            
            
            if (invoice.GAmount != invoice.ToPayGAmount) // ���ºҧ��ǹ
            {
                //// DCR ��������Ἱ��͹ 2021AUG (POS SLIP)
                if (receipt.GroupReceiptOrNot == "Y")
                {
                    sb.AppendLine(CreateLineString("����Թ����ͧ����",
                        string.Format("{0} �ҷ", GroupReceiptReplaceZeroBy), columnLength));
                }
                else
                {
                    sb.AppendLine(CreateLineString("����Թ����ͧ����",
                        string.Format("{0} �ҷ", GetAmountString(invoice.GAmount)), columnLength));
                }

                sb.AppendLine();
                sb.AppendLine("��ê����Թ���俿��");

                //#ISSUE ź ˹��¤�����ѧ�ҡ��äӹǳ �͡�ҡ����� Uthen 2020-05-11
                //sb.AppendLine(CreateLineString("˹��·����",
                //    string.Format("{0} ˹���", GetAmountString4(invoice.ToPayQty)), columnLength));

                //// DCR ��������Ἱ��͹ 2021AUG (POS SLIP)
                if (receipt.GroupReceiptOrNot == "Y")
                {
                    //// �Ǵ��� ��������Ἱ��͹
                    sb.AppendLine(CreateLineString(receipt.GroupReceiptInstallmentText,
                                string.Format("{0} �ҷ", GetAmountString(receipt.GroupReceiptAmount)), columnLength));
                }
                else
                {
                    #region
                    if (invoice.InstallmentTotalPeriod != null && invoice.InstallmentTotalPeriod > 0)
                    {
                        if (invoice.InstallmentPeriod == invoice.InstallmentTotalPeriod)
                        {
                            if (invoice.PartialPayment != null)
                            {
                                if (invoice.PartialPayment == 1)
                                {
                                    sb.AppendLine(CreateLineString("�����Թ�ҧ��ǹ",
                                        string.Format("{0} �ҷ", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                                }
                                else
                                {
                                    sb.AppendLine(CreateLineString("�����Թ��ǹ��������",
                                        string.Format("{0} �ҷ", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                                }
                            }
                            else
                            {
                                if (invoice.Bills[0].GAmount != invoice.ToPayGAmount)
                                {
                                    sb.AppendLine(CreateLineString("�����Թ�ҧ��ǹ",
                                        string.Format("{0} �ҷ", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                                }
                                else
                                {
                                    sb.AppendLine(CreateLineString("�����Թ��ǹ��������",
                                        string.Format("{0} �ҷ", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                                }
                            }
                        }
                        else
                        {
                            sb.AppendLine(CreateLineString("�����Թ�ҧ��ǹ",
                                string.Format("{0} �ҷ", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                        }
                    }
                    else
                    {
                        if (invoice.PartialPayment != null)
                        {
                            if (invoice.PartialPayment == 1)
                            {
                                sb.AppendLine(CreateLineString("�����Թ�ҧ��ǹ",
                                    string.Format("{0} �ҷ", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                            }
                            else
                            {
                                sb.AppendLine(CreateLineString("�����Թ��ǹ��������",
                                    string.Format("{0} �ҷ", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                            }
                        }
                        else
                        {
                            if (invoice.TotalPaidAmount != invoice.ToPayGAmount)
                            {
                                sb.AppendLine(CreateLineString("�����Թ�ҧ��ǹ",
                                    string.Format("{0} �ҷ", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                            }
                            else
                            {
                                sb.AppendLine(CreateLineString("�����Թ��ǹ��������",
                                    string.Format("{0} �ҷ", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                            }
                        }
                    }
                    #endregion
                }
                
                //// DCR ��������Ἱ��͹ 2021AUG (POS SLIP)
                if(receipt.GroupReceiptOrNot == "Y")
                {                    
                    sb.AppendLine(CreateLineString(
                    string.Format("������Ť������ {0}%", bill.TaxRate.Value.ToString("##")),
                    string.Format("{0} �ҷ", GetAmountString(receipt.GroupReceiptVatTotal)), columnLength));

                    sb.AppendLine(CreateLineString("����Թ������",
                        string.Format("{0} �ҷ", GetAmountString(receipt.GroupReceiptTotal)), columnLength));
                }
                else
                {
                    sb.AppendLine(CreateLineString(
                    string.Format("������Ť������ {0}%", bill.TaxRate.Value.ToString("##")),
                    string.Format("{0} �ҷ", GetAmountString(invoice.ToPayVatAmount)), columnLength));
                    sb.AppendLine(CreateLineString("����Թ������",
                        string.Format("{0} �ҷ", GetAmountString(invoice.ToPayGAmount)), columnLength));
                    ////FT PE
                    //sb.AppendLine(CreateLineString("Ft 0.0100+PE 0.0039�ҷ/˹���(��-��65)",
                    //    string.Format("{0}", ""), columnLength));
                }
            }
            else
            {
                //// DCR ��������Ἱ��͹ 2021AUG (POS SLIP)
                if (receipt.GroupReceiptOrNot == "Y")
                {
                    sb.AppendLine(CreateLineString("����Թ������",
                    string.Format("{0} �ҷ", GetAmountString(receipt.GroupReceiptTotal)), columnLength));
                }
                else
                {
                    sb.AppendLine(CreateLineString("����Թ������",
                    string.Format("{0} �ҷ", GetAmountString(bill.GAmount)), columnLength));
                    //FT PE
                    //sb.AppendLine(CreateLineString("Ft 0.0100+PE 0.0039�ҷ/˹���(��-��65)",
                    //    string.Format("{0}", ""), columnLength));
                }                
            }

            sb.Append(GetReceiptPayment(receipt, paymentMethods));
            sb.Append(GetTotalPayment(receipt, paymentMethods));

            //// DCR ��������Ἱ��͹ 2021-OCT-05 (POS SLIP) ����� GroupReceipt ������Ţ�������稷������Ǣ�ͧ
            string SummaryReceipts = GetReceiptSummary(receipt, allReceiptsNo);
            bool isContainsX       = false;
            if (SummaryReceipts.Contains("X") == true)
            {
                isContainsX = true;
            }
            //if (receipt.GroupReceiptOrNot != "Y")
            if(isContainsX == false)
            {
                sb.Append(GetReceiptSummary(receipt, allReceiptsNo));
            }

            sb.Append(GetPaymentDate(receipt, bill));

            //// DCR ��������Ἱ��͹ 2021-OCT-04 (POS SLIP) ��� GroupReceipt ������ʴ���ҧ�ԧ�֧��駤��俿��
            if (receipt.GroupReceiptOrNot != "Y")
            {
                if (invoice.Bills[0].GroupInvoiceId != null && invoice.Bills[0].GroupInvoiceId != string.Empty)
                {
                    sb.AppendLine(string.Format("��ҧ�֧��駤��俿���Ţ��� {0}", invoice.Bills[0].GroupInvoiceId));
                }
                else if (invoice.InstallmentTotalPeriod != null && invoice.InstallmentTotalPeriod > 0)
                {
                    sb.AppendLine(string.Format("��ҧ�֧��駤��俿���Ţ��� {0}{1}", invoice.DisplayInvoiceNo
                        , invoice.InvoiceDate == null ? "" : string.Format(" ��.{0}", GetThaiDate(invoice.InvoiceDate))
                        ));
                }
                else if (invoice.InvoiceNo.Length == 22) // Key in Spot Bill
                {
                    sb.AppendLine(string.Format("��ҧ�֧��駤��俿���Ţ��� {0}", invoice.SpotBillInvoiceNo));
                }
                else
                {
                    sb.AppendLine(string.Format("��ҧ�֧��駤��俿���Ţ��� {0}{1}", invoice.DisplayInvoiceNo
                        , invoice.InvoiceDate == null ? "" : string.Format(" ��.{0}", GetThaiDate(invoice.InvoiceDate))
                        ));
                }
            }

            return sb.ToString();
        }

       
    }
}
