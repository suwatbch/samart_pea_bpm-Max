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
                    sb.AppendLine(string.Format("Tax ID {0} {1}", receipt.CaTaxId.Trim(), "สำนักงานใหญ่"));
                }
                else
                {
                    sb.AppendLine(string.Format("Tax ID {0} สาขา {1}", receipt.CaTaxId.Trim(), receipt.CaTaxBranch.Trim()));
                }
            }
            
            //sb.AppendLine(string.Format("ชื่อ {0}", receipt.CustomerName));
            sb.AppendLine(string.Format("ที่อยู่ {0}", receipt.CustomerAddress));

            PrintingInvoice invoice = receipt.PrintingInvoices[0];
            Bill bill = invoice.Bills[0];
            
            if (invoice.DataState == InvoiceDataStage.Invoice && bill.MeterId != null )
            {
                //// DCR รวมใบเสร็จแผนผ่อน 2021AUG ออกใบเสร็จชำระเงินครั้งแรก (POS SLIP)
                if(receipt.GroupReceiptOrNot == "Y")
                {
                    if (bill.DisplayMeterId != null && bill.RateTypeId != null)
                    {
                        sb.AppendLine(string.Format("รหัสเครื่องวัด {0} ประเภทอัตรา {1}", bill.DisplayMeterId, bill.RateTypeId));
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
                    sb.AppendLine(string.Format("หมายเลขผู้ใช้ไฟฟ้า {0}", receipt.DisplayCaId));
                    sb.AppendLine(receipt.GroupReceiptPeriodText);
                    sb.AppendLine(""); // แทนเลขครั้งก่อนครั้งหลัง
                    //sb.AppendLine(string.Format("ประจำเดือน {0} วันที่อ่านหน่วย {1}", StringConvert.FormatPeriod(bill.Period), GetThaiDate(bill.MeterReadDate)));
                    //sb.AppendLine(string.Format("เลขอ่านครั้งหลัง {0} เลขอ่านครั้งก่อน {1}", "-", "-"));
                    sb.AppendLine(CreateLineString("หน่วยที่ใช้", string.Format("{0} หน่วย", GetAmountString4(receipt.GroupReceiptQty)), columnLength));
                    sb.AppendLine(CreateLineString("ค่าไฟฟ้าฐาน", string.Format("{0} บาท", GroupReceiptReplaceZeroBy), columnLength));
                    sb.AppendLine(CreateLineString(
                        string.Format("ค่า FT {0} บาท/หน่วย", GroupReceiptReplaceZeroBy),
                        string.Format("{0} บาท", GroupReceiptReplaceZeroBy), columnLength));
                }
                else //// POS Slip ปกติ
                {
                    sb.AppendLine(string.Format("รหัสเครื่องวัด {0} ประเภทอัตรา {1}", bill.DisplayMeterId, bill.RateTypeId));
                    if (null != invoice.BranchId)
                    {
                        sb.AppendLine(string.Format("{0} {1}",
                            invoice.BranchId, CodeTable.Instant.ListBranches(invoice.BranchId).BranchName));
                    }
                    sb.AppendLine(string.Format("หมายเลขผู้ใช้ไฟฟ้า {0}", receipt.DisplayCaId));
                    sb.AppendLine(string.Format("ประจำเดือน {0} วันที่อ่านหน่วย {1}", StringConvert.FormatPeriod(bill.Period), GetThaiDate(bill.MeterReadDate)));
                    sb.AppendLine(string.Format("เลขอ่านครั้งหลัง {0} เลขอ่านครั้งก่อน {1}", FormatUnit(bill.LastUnit), FormatUnit(bill.PreviousUnit)));
                    sb.AppendLine(CreateLineString("หน่วยที่ใช้", string.Format("{0} หน่วย", GetAmountString4(invoice.Qty)), columnLength));
                    sb.AppendLine(CreateLineString("ค่าไฟฟ้าฐาน", string.Format("{0} บาท", GetAmountString(bill.BaseAmount)), columnLength));
                    sb.AppendLine(CreateLineString(
                        string.Format("ค่า FT {0} บาท/หน่วย", GetAmountString4(bill.FtUnitPrice)),
                        //string.Format("ค่าFT{0}บาท/หน่วย", "+PE=0.0139"),
                        string.Format("{0} บาท", GetAmountString(bill.FtAmount)), columnLength));
                }
            }
            else
            {
                #region //// DCR รวมใบเสร็จแผนผ่อน 2021AUG (POS SLIP)
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

                    //// sb.AppendLine(string.Format("รหัสเครื่องวัด {0} ประเภทอัตรา {1}", bill.DisplayMeterId, bill.RateTypeId));
                    if (tmpDisplayMeterId != null && bill.RateTypeId != null)
                    {
                        sb.AppendLine(string.Format("รหัสเครื่องวัด {0} ประเภทอัตรา {1}", bill.DisplayMeterId, bill.RateTypeId));
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
                    sb.AppendLine(string.Format("หมายเลขผู้ใช้ไฟฟ้า {0}", receipt.DisplayCaId));
                    sb.AppendLine(receipt.GroupReceiptPeriodText);
                    sb.AppendLine(""); // แทนเลขครั้งก่อนครั้งหลัง
                    //sb.AppendLine(string.Format("เลขอ่านครั้งหลัง {0} เลขอ่านครั้งก่อน {1}", "-", "-"));
                    sb.AppendLine(CreateLineString("หน่วยที่ใช้", string.Format("{0} หน่วย", GetAmountString4(receipt.GroupReceiptQty)), columnLength));
                    sb.AppendLine(CreateLineString("ค่าไฟฟ้าฐาน", string.Format("{0} บาท", GetAmountString(receipt.GroupReceiptAmount)), columnLength));
                    sb.AppendLine(CreateLineString(
                        string.Format("ค่า FT {0} บาท/หน่วย", GroupReceiptReplaceZeroBy),
                        string.Format("{0} บาท", GroupReceiptReplaceZeroBy), columnLength));
                }
                #endregion
                else
                {
                    sb.AppendLine(string.Format("หมายเลขผู้ใช้ไฟฟ้า {0}", receipt.DisplayCaId));
                    //[XX]: Round1
                    sb.AppendLine(string.Format("ประจำเดือน {0}", StringConvert.FormatPeriod(bill.Period)));
                    sb.AppendLine(CreateLineString("หน่วยที่ใช้", string.Format("{0} หน่วย", GetAmountString4(invoice.Qty)), columnLength));
                }
            }

            //// DCR รวมใบเสร็จแผนผ่อน 2021AUG (POS SLIP)
            if (receipt.GroupReceiptOrNot == "Y")
            {
                sb.AppendLine(CreateLineString("รวมเงินค่าไฟฟ้า", string.Format("{0} บาท", GroupReceiptReplaceZeroBy), columnLength));
                sb.AppendLine(CreateLineString(
                string.Format("ภาษีมูลค่าเพิ่ม {0}%", GroupReceiptReplaceZeroBy),
                string.Format("{0} บาท", GroupReceiptReplaceZeroBy), columnLength));
            }
            else
            {
                sb.AppendLine(CreateLineString("รวมเงินค่าไฟฟ้า", string.Format("{0} บาท", GetAmountString(invoice.AmountExVat)), columnLength));
                sb.AppendLine(CreateLineString(
                string.Format("ภาษีมูลค่าเพิ่ม {0}%", bill.TaxRate.Value.ToString("##")),
                string.Format("{0} บาท", GetAmountString(invoice.VatAmount)), columnLength));
            }

            
            
            if (invoice.GAmount != invoice.ToPayGAmount) // จ่ายบางส่วน
            {
                //// DCR รวมใบเสร็จแผนผ่อน 2021AUG (POS SLIP)
                if (receipt.GroupReceiptOrNot == "Y")
                {
                    sb.AppendLine(CreateLineString("รวมเงินที่ต้องชำระ",
                        string.Format("{0} บาท", GroupReceiptReplaceZeroBy), columnLength));
                }
                else
                {
                    sb.AppendLine(CreateLineString("รวมเงินที่ต้องชำระ",
                        string.Format("{0} บาท", GetAmountString(invoice.GAmount)), columnLength));
                }

                sb.AppendLine();
                sb.AppendLine("การชำระเงินค่าไฟฟ้า");

                //#ISSUE ลบ หน่วยค่าไฟหลังจากการคำนวณ ออกจากใบเสร็จ Uthen 2020-05-11
                //sb.AppendLine(CreateLineString("หน่วยที่ใช้",
                //    string.Format("{0} หน่วย", GetAmountString4(invoice.ToPayQty)), columnLength));

                //// DCR รวมใบเสร็จแผนผ่อน 2021AUG (POS SLIP)
                if (receipt.GroupReceiptOrNot == "Y")
                {
                    //// งวดที่ รวมใบเสร็จแผนผ่อน
                    sb.AppendLine(CreateLineString(receipt.GroupReceiptInstallmentText,
                                string.Format("{0} บาท", GetAmountString(receipt.GroupReceiptAmount)), columnLength));
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
                                    sb.AppendLine(CreateLineString("ชำระเงินบางส่วน",
                                        string.Format("{0} บาท", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                                }
                                else
                                {
                                    sb.AppendLine(CreateLineString("ชำระเงินส่วนที่เหลือ",
                                        string.Format("{0} บาท", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                                }
                            }
                            else
                            {
                                if (invoice.Bills[0].GAmount != invoice.ToPayGAmount)
                                {
                                    sb.AppendLine(CreateLineString("ชำระเงินบางส่วน",
                                        string.Format("{0} บาท", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                                }
                                else
                                {
                                    sb.AppendLine(CreateLineString("ชำระเงินส่วนที่เหลือ",
                                        string.Format("{0} บาท", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                                }
                            }
                        }
                        else
                        {
                            sb.AppendLine(CreateLineString("ชำระเงินบางส่วน",
                                string.Format("{0} บาท", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                        }
                    }
                    else
                    {
                        if (invoice.PartialPayment != null)
                        {
                            if (invoice.PartialPayment == 1)
                            {
                                sb.AppendLine(CreateLineString("ชำระเงินบางส่วน",
                                    string.Format("{0} บาท", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                            }
                            else
                            {
                                sb.AppendLine(CreateLineString("ชำระเงินส่วนที่เหลือ",
                                    string.Format("{0} บาท", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                            }
                        }
                        else
                        {
                            if (invoice.TotalPaidAmount != invoice.ToPayGAmount)
                            {
                                sb.AppendLine(CreateLineString("ชำระเงินบางส่วน",
                                    string.Format("{0} บาท", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                            }
                            else
                            {
                                sb.AppendLine(CreateLineString("ชำระเงินส่วนที่เหลือ",
                                    string.Format("{0} บาท", GetAmountString(invoice.ToPayExVatAmount)), columnLength));
                            }
                        }
                    }
                    #endregion
                }
                
                //// DCR รวมใบเสร็จแผนผ่อน 2021AUG (POS SLIP)
                if(receipt.GroupReceiptOrNot == "Y")
                {                    
                    sb.AppendLine(CreateLineString(
                    string.Format("ภาษีมูลค่าเพิ่ม {0}%", bill.TaxRate.Value.ToString("##")),
                    string.Format("{0} บาท", GetAmountString(receipt.GroupReceiptVatTotal)), columnLength));

                    sb.AppendLine(CreateLineString("รวมเงินทั้งสิ้น",
                        string.Format("{0} บาท", GetAmountString(receipt.GroupReceiptTotal)), columnLength));
                }
                else
                {
                    sb.AppendLine(CreateLineString(
                    string.Format("ภาษีมูลค่าเพิ่ม {0}%", bill.TaxRate.Value.ToString("##")),
                    string.Format("{0} บาท", GetAmountString(invoice.ToPayVatAmount)), columnLength));
                    sb.AppendLine(CreateLineString("รวมเงินทั้งสิ้น",
                        string.Format("{0} บาท", GetAmountString(invoice.ToPayGAmount)), columnLength));
                    ////FT PE
                    //sb.AppendLine(CreateLineString("Ft 0.0100+PE 0.0039บาท/หน่วย(กย-ธค65)",
                    //    string.Format("{0}", ""), columnLength));
                }
            }
            else
            {
                //// DCR รวมใบเสร็จแผนผ่อน 2021AUG (POS SLIP)
                if (receipt.GroupReceiptOrNot == "Y")
                {
                    sb.AppendLine(CreateLineString("รวมเงินทั้งสิ้น",
                    string.Format("{0} บาท", GetAmountString(receipt.GroupReceiptTotal)), columnLength));
                }
                else
                {
                    sb.AppendLine(CreateLineString("รวมเงินทั้งสิ้น",
                    string.Format("{0} บาท", GetAmountString(bill.GAmount)), columnLength));
                    //FT PE
                    //sb.AppendLine(CreateLineString("Ft 0.0100+PE 0.0039บาท/หน่วย(กย-ธค65)",
                    //    string.Format("{0}", ""), columnLength));
                }                
            }

            sb.Append(GetReceiptPayment(receipt, paymentMethods));
            sb.Append(GetTotalPayment(receipt, paymentMethods));

            //// DCR รวมใบเสร็จแผนผ่อน 2021-OCT-05 (POS SLIP) ถ้าเป็น GroupReceipt จะไม่เลขที่ใบเสร็จที่เกี่ยวข้อง
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

            //// DCR รวมใบเสร็จแผนผ่อน 2021-OCT-04 (POS SLIP) ถ้า GroupReceipt จะไม่แสดงอ้างอิงถึงใบแจ้งค่าไฟฟ้า
            if (receipt.GroupReceiptOrNot != "Y")
            {
                if (invoice.Bills[0].GroupInvoiceId != null && invoice.Bills[0].GroupInvoiceId != string.Empty)
                {
                    sb.AppendLine(string.Format("อ้างถึงใบแจ้งค่าไฟฟ้าเลขที่ {0}", invoice.Bills[0].GroupInvoiceId));
                }
                else if (invoice.InstallmentTotalPeriod != null && invoice.InstallmentTotalPeriod > 0)
                {
                    sb.AppendLine(string.Format("อ้างถึงใบแจ้งค่าไฟฟ้าเลขที่ {0}{1}", invoice.DisplayInvoiceNo
                        , invoice.InvoiceDate == null ? "" : string.Format(" ลว.{0}", GetThaiDate(invoice.InvoiceDate))
                        ));
                }
                else if (invoice.InvoiceNo.Length == 22) // Key in Spot Bill
                {
                    sb.AppendLine(string.Format("อ้างถึงใบแจ้งค่าไฟฟ้าเลขที่ {0}", invoice.SpotBillInvoiceNo));
                }
                else
                {
                    sb.AppendLine(string.Format("อ้างถึงใบแจ้งค่าไฟฟ้าเลขที่ {0}{1}", invoice.DisplayInvoiceNo
                        , invoice.InvoiceDate == null ? "" : string.Format(" ลว.{0}", GetThaiDate(invoice.InvoiceDate))
                        ));
                }
            }

            return sb.ToString();
        }

       
    }
}
