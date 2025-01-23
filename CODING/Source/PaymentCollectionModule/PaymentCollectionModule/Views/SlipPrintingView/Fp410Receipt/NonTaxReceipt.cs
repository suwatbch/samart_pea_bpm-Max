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
    public class NonTaxReceipt: ReceiptBase
    {
        public static NonTaxReceipt Instance()
        {
            return new NonTaxReceipt();
        }

        public void Print(PrintingReceipt receipt, List<string> allReceiptsNo, List<PaymentMethod> paymentMethods)
        {
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
            string debtId = bill.DebtId;

            sb.AppendLine(string.Format("ชื่อ {0}", receipt.CustomerName));

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
                    sb.AppendLine(string.Format("Tax ID {0} {1}", receipt.CaTaxId.Trim(), "สำนักงานใหญ่"));
                }
                else
                {
                    sb.AppendLine(string.Format("Tax ID {0} สาขา {1}", receipt.CaTaxId.Trim(), receipt.CaTaxBranch.Trim()));
                }
            }

            //sb.AppendLine(string.Format("ชื่อ {0}", receipt.CustomerName));
            sb.AppendLine(string.Format("ที่อยู่ {0}", receipt.CustomerAddress));
            if (null != invoice.BranchId)
            {
                sb.AppendLine(string.Format("{0} {1}",
                    invoice.BranchId, CodeTable.Instant.ListBranches(invoice.BranchId).BranchName));
            }
            if (receipt.ContractType == CodeNames.ContractType.Electric.Id)
            {
                sb.AppendLine(string.Format("หมายเลขผู้ใช้ไฟฟ้า {0}", receipt.DisplayCaId));
            }
            else if (receipt.ContractType == CodeNames.ContractType.Agency.Id)
            {
                sb.AppendLine(string.Format("หมายเลขตัวแทน {0}", receipt.DisplayCaId));
            }
            else
            {
                sb.AppendLine(string.Format("หมายเลขบัญชีแสดงสัญญา {0}", receipt.DisplayCaId));
            }
            
            sb.AppendLine();


            //// สินค้า M00171 และทุกอันที่ไม่ใช่นำหน้าด้วย M0017             
            //if (debtId.Substring(0, 5) != "M0017" || debtId.Substring(0, 6) == "M00171")
            //{
            //    //รายการใบเสร็จรับฝาก
            //    if (debtId.IndexOf("M00900010") > -1 && bill.PrintDescription.IndexOf("รายการ") > -1)
            //    {
            //        sb.AppendLine(string.Format("รายการ {0}", bill.PrintDescription.Replace("รายการ", "")));
            //    }
            //    else
            //        sb.AppendLine(string.Format("รายการ {0}", bill.PrintDescription));


            //    if (null != invoice.Qty && 0 != invoice.Qty.Value && debtId != CodeNames.DebtType.Interest.Id)
            //    {
            //        sb.AppendLine(CreateLineString(
            //            "จำนวน",
            //            string.Format("{0} {1}", bill.Qty.Value.ToString("#,##0"), bill.UnitTypeName), columnLength));
            //        if (bill.UnitPrice != null)
            //        {
            //            sb.AppendLine(CreateLineString(
            //                "ราคา/หน่วย",
            //                string.Format("{0} บาท", bill.UnitPrice.Value.ToString("#,##0.00")), columnLength));
            //        }
            //    }
            //}
            //else
            //{
            //    if (debtId.Substring(0, 6) == "M00176")
            //    {
            //        sb.AppendLine(string.Format("ค่าขยายเขต อ้างถึงใบแจ้งหนี้เลขที่ {0}\nลงวันที่ {1}", invoice.DisplayInvoiceNo, GetThaiDate(invoice.InvoiceDate)));
            //    }
            //    else
            //    {
            //        sb.AppendLine(string.Format("ค่าบริการ อ้างถึงใบแจ้งหนี้เลขที่ {0}\nลงวันที่ {1}", invoice.DisplayInvoiceNo, GetThaiDate(invoice.InvoiceDate)));
            //    }
            //}


            List<DebtType> dt = CodeTable.Instant.ListDebtTypes().FindAll(delegate(DebtType d) { return d.DebtId == debtId && d.PrintDescription != null && d.PrintDescription != ""; });

            if (dt.Count > 0)
            {
                string receiptType = bill.TaxCode == null || bill.TaxCode[0] == 'O' || bill.TaxRate == null ? "" : invoice.GAmount != invoice.ToPayGAmount ? "" : "รายละเอียดตามใบแนบใบกำกับภาษี";

                sb.AppendLine(string.Format("รายการ {0} {1}", dt[0].PrintDescription, receiptType));
            }
            else
            {
                if
                (
                    debtId.Substring(0, 5) != "M0017" 
                    || debtId.Substring(0, 6) == "M00171"
                    || debtId == "M00175800" // ค่าจัดการพลังงาน
                    || debtId == "M99080012" // ค่าจัดการพลังงาน (ยกฐานะ)
                )
                {
                    //รายการใบเสร็จรับฝาก
                    if (debtId.IndexOf("M00900010") > -1 && bill.PrintDescription.IndexOf("รายการ") > -1)
                    {
                        sb.AppendLine(string.Format("รายการ {0}", bill.PrintDescription.Replace("รายการ", "")));
                    }
                    else
                        sb.AppendLine(string.Format("รายการ {0}", bill.PrintDescription));


                    if (null != invoice.Qty && 0 != invoice.Qty.Value && debtId != CodeNames.DebtType.Interest.Id)
                    {
                        sb.AppendLine(CreateLineString(
                            "จำนวน",
                            string.Format("{0} {1}", bill.Qty.Value.ToString("#,##0"), bill.UnitTypeName), columnLength));
                        if (bill.UnitPrice != null)
                        {
                            sb.AppendLine(CreateLineString(
                                "ราคา/หน่วย",
                                string.Format("{0} บาท", bill.UnitPrice.Value.ToString("#,##0.00")), columnLength));
                        }
                    }
                }
            }

            if (invoice.GAmount != invoice.ToPayGAmount) // จ่ายบางส่วน
            {
                sb.AppendLine(CreateLineString(
                    "รวมเงินที่ต้องชำระ",
                    string.Format("{0} บาท", invoice.GAmount.Value.ToString("#,##0.00")), columnLength));
                
                sb.AppendLine();
                sb.AppendLine("การชำระเงิน");

                if ((debtId.Substring(0, 5) != "M0017" 
                    || debtId.Substring(0, 6) == "M00171"
                    || debtId == "M00175800" // ค่าจัดการพลังงาน
                    || debtId == "M99080012" // ค่าจัดการพลังงาน (ยกฐานะ)
                    ) && (invoice.ToPayQty != null))
                {
                    sb.AppendLine(CreateLineString("จำนวน",
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
                                sb.AppendLine(CreateLineString("ชำระเงินบางส่วน",
                                    string.Format("{0} บาท", invoice.ToPayGAmount.Value.ToString("#,##0.00")), columnLength));
                            }
                            else
                            {
                                sb.AppendLine(CreateLineString("ชำระเงินส่วนที่เหลือ",
                                    string.Format("{0} บาท", invoice.ToPayGAmount.Value.ToString("#,##0.00")), columnLength));
                            }
                        }
                        else
                        {
                            if (invoice.Bills[0].GAmount != invoice.ToPayGAmount)
                            {
                                sb.AppendLine(CreateLineString("ชำระเงินบางส่วน",
                                    string.Format("{0} บาท", invoice.ToPayGAmount.Value.ToString("#,##0.00")), columnLength));
                            }
                            else
                            {
                                sb.AppendLine(CreateLineString("ชำระเงินส่วนที่เหลือ",
                                    string.Format("{0} บาท", invoice.ToPayGAmount.Value.ToString("#,##0.00")), columnLength));
                            }
                        }
                    }
                    else
                    {
                        sb.AppendLine(CreateLineString("ชำระเงินบางส่วน",
                            string.Format("{0} บาท", invoice.ToPayGAmount.Value.ToString("#,##0.00")), columnLength));
                    }
                }
                else
                {
                    if (invoice.PartialPayment != null)
                    {
                        if (invoice.PartialPayment == 1)
                        {
                            sb.AppendLine(CreateLineString("ชำระเงินบางส่วน",
                                string.Format("{0} บาท", invoice.ToPayGAmount.Value.ToString("#,##0.00")), columnLength));
                        }
                        else
                        {
                            sb.AppendLine(CreateLineString("ชำระเงินส่วนที่เหลือ",
                                string.Format("{0} บาท", invoice.ToPayGAmount.Value.ToString("#,##0.00")), columnLength));
                        }
                    }
                    else
                    {
                        if (invoice.TotalPaidAmount != invoice.ToPayGAmount)
                        {
                            sb.AppendLine(CreateLineString("ชำระเงินบางส่วน",
                                string.Format("{0} บาท", invoice.ToPayGAmount.Value.ToString("#,##0.00")), columnLength));
                        }
                        else
                        {
                            sb.AppendLine(CreateLineString("ชำระเงินส่วนที่เหลือ",
                                string.Format("{0} บาท", invoice.ToPayGAmount.Value.ToString("#,##0.00")), columnLength));
                        }
                    }
                }
            }
            else
            {
                sb.AppendLine(CreateLineString(
                    "รวมเงินทั้งสิ้น",
                    string.Format("{0} บาท", invoice.GAmount.Value.ToString("#,##0.00")), columnLength));
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
