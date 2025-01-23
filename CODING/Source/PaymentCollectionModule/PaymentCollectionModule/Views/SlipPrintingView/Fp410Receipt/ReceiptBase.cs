using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using System.IO.Ports;
using System.IO;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.Globalization;
using System.Windows.Forms;
using System.Threading;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using System.Linq;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.Fp410Receipt
{
    public class ReceiptBase
    {
        CultureInfo ci = new CultureInfo("th-TH");
        //[XX]
        protected IDSettingHelper hp = IDSettingHelper.Instance();
        //protected LocalSettingHelper hp = LocalSettingHelper.Instance();
        protected const int columnLength = 48;

        // สระที่ไม่นับ ก่ก้ก๊ก๋ก็ก์กิกีกึกืกุกูกั
        protected char[] noCountChar = new char[] { (char)3656, (char)3657, (char)3658, (char)3659, (char)3655, (char)3660, 
                (char)3636, (char)3637, (char)3638, (char)3639, (char)3640, (char)3641, (char)3633 };

        protected string GetPrintHeader(PrintingReceipt receipt)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();

            if (receipt.IsTaxReceipt)
            {
                sb.AppendLine(string.Format("ใบเสร็จรับเงิน/ใบกำกับภาษี เลขที่ {0}", receipt.ReceiptId));                
            }
            else
            {
                sb.AppendLine(string.Format("ใบเสร็จรับเงิน เลขที่ {0}", receipt.ReceiptId));
            }

            //Check Head Office
            if (receipt.BranchNumber.Trim() == "0000" || receipt.BranchNumber.Trim() == "00000")
            {
                sb.AppendLine(string.Format("{0} ({1})", receipt.BranchName, "สำนักงานใหญ่"));    
            }
            else
            {
                sb.AppendLine(string.Format("{0} (สาขาที่ {1})", receipt.BranchName, receipt.BranchNumber));
            }
            //sb.AppendLine(string.Format("{0} (สาขาที่ {1})", receipt.BranchName, receipt.BranchNumber));
            sb.AppendLine(receipt.BranchAddress);
            sb.AppendLine(string.Format("เลขประจำตัวผู้เสียภาษีอากร {0}", Session.Terminal.TaxId));
            sb.AppendLine(string.Format("เลขประจำเครื่อง {0}", receipt.TerminalCode));
            sb.AppendLine("----------------------------------------------------");

            return sb.ToString();
        }

        protected string CreateLineString(string a, string b, int lenght)
        {
            //string aX = a, bX = b;
            //for (int i = 0; i < noCountChar.Length; i++)
            //{
            //    aX = aX.Replace(noCountChar[i].ToString(), "");
            //    bX = bX.Replace(noCountChar[i].ToString(), "");
            //}

            //int space = lenght - (aX.Length + bX.Length);
            //if (space <= 0)
            //{
            //    space = space + lenght + 1;
            //}
            //return string.Format("{0}{1}{2}", a, "".PadLeft(space+1, ' '), b);

            return string.Format("{0}\t{1}", a, b);
        }

        protected string FormatUnit(decimal? unit)
        {
            if (null == unit)
            {
                return "0";
            }
            else
            {
                return unit.Value.ToString("#,##0.##");
            }
        }       

        protected string GetReceiptPayment(PrintingReceipt receipt, List<PaymentMethod> paymentMethods)
        {
            StringBuilder sb = new StringBuilder();
            PrintingInvoice invoice = receipt.PrintingInvoices[0];
            Bill bill = invoice.Bills[0];
            string debtId = bill.DebtId;

            sb.AppendLine();

            if (receipt.TotalReceipt == 1)
            {
                if (receipt.GroupReceiptOrNot == "Y")
                {
                    sb.AppendLine("");
                }
                else
                {
                    sb.AppendLine(CreateLineString(
                        string.Format("ชำระ  {0} บาท", receipt.PaidAmount.ToString("#,##0.00")),
                        string.Format("ทอนเงิน  {0} บาท", receipt.ChangeAmount.ToString("#,##0.00")),
                        columnLength));
                }

                if (receipt.GroupReceiptPaymentMethodsWithPipePOSSlip != null && receipt.GroupReceiptPaymentMethodsWithPipePOSSlip.Length > 5)
                {
                    sb.AppendLine(GetPaymentMethodsTextByStoredProcedure(receipt.GroupReceiptPaymentMethodsWithPipePOSSlip));
                }
                else
                {
                    sb.Append(GetPaymentMethod(paymentMethods));
                }

                if (receipt.AdjChangeAmount != 0)
                {
                    sb.AppendLine(CreateLineString("MISC.REV", string.Format("{0} บาท", receipt.AdjChangeAmount.ToString("#,##0.00")), columnLength));
                }

                if (null != debtId && debtId == "M00710010" && bill.Description.Contains("@"))
                {
                    sb.AppendLine();
                    sb.AppendLine(string.Format("ออกแทนใบเสร็จรับเงิน เลขที่ {0}\nลว.{1} ซึ่งไม่สมบูรณ์\nเนื่องจากธนาคารปฏิเสธการจ่ายเงิน", bill.Description.Split('@')[1].Replace(" ", ""), GetThaiDate(StringConvert.ToDateTime(bill.Description.Split('@')[2].Replace(" ", "").Substring(0, 8).ToString()))));
                }
            }
            else
            {

                if (receipt.GroupReceiptOrNot == "Y")
                {
                    sb.AppendLine("");
                }
                else
                {
                    sb.AppendLine(CreateLineString("ชำระ",
                    string.Format("{0} บาท", receipt.PrintingInvoices[0].ToPayGAmount.Value.ToString("#,##0.00")), columnLength));
                }

                if (receipt.GroupReceiptPaymentMethodsWithPipePOSSlip != null && receipt.GroupReceiptPaymentMethodsWithPipePOSSlip.Length > 5)
                {
                    sb.AppendLine(GetPaymentMethodsTextByStoredProcedure(receipt.GroupReceiptPaymentMethodsWithPipePOSSlip));
                }
                else
                {
                    sb.AppendLine(GetPaymentMethod(receipt.PrintingInvoices[0].PaymentMethods, paymentMethods));
                }                
                
                if (null != debtId && debtId == "M00710010" && bill.Description.Contains("@"))
                {                    
                    sb.AppendLine();
                    sb.AppendLine(string.Format("ออกแทนใบเสร็จรับเงิน เลขที่ {0}\nลว.{1} ซึ่งไม่สมบูรณ์\nเนื่องจากธนาคารปฏิเสธการจ่ายเงิน", bill.Description.Split('@')[1].Replace(" ", ""), GetThaiDate(StringConvert.ToDateTime(bill.Description.Split('@')[2].Replace(" ", "").Substring(0, 8).ToString()))));
                    sb.AppendLine();
                }

                //// ใบเสร็จรวมแผนผ่อน PrintingSequence ของ POS Slip
                sb.AppendLine(
                    CreateLineString(
                    "",
                    string.Format("({0}/{1})", receipt.PrintingSequence, receipt.TotalReceipt),
                    columnLength)
                    );
            }

            sb.AppendLine("----------------------------------------------------");

            return sb.ToString();
        }

        private string GetPaymentMethod(List<InvoicePaymentMethod> paymentMethods, List<PaymentMethod> pms)
        {
            StringBuilder sb = new StringBuilder();

            if (null != paymentMethods && paymentMethods.Count>0)
            {
                if (paymentMethods.Count > 1 || paymentMethods[0].PtId != CodeNames.PaymentType.Cash.Id)
                {
                    paymentMethods.Sort();

                    foreach (InvoicePaymentMethod pmx in paymentMethods)
                    {
                        PaymentMethod pm = pmx.GetPaymentMethod(pms);

                        switch (pm.PtId)
                        {
                            case CodeNames.PaymentType.Cash.Id:
                                sb.AppendLine(
                                    CreateLineString(
                                    string.Format("  - {0}", pm.PtName),
                                    string.Format("{0} บาท", pmx.Amount.ToString("#,##0.00")),
                                    columnLength));
                                break;
                            case CodeNames.PaymentType.Deposit.Id:
                                sb.AppendLine(
                                    CreateLineString(
                                    string.Format("  - {0} {1} {2}", pm.PtName, pm.DepositAccNo, pm.BankName),
                                    string.Format("{0} บาท", pm.ToPayAmount.Value.ToString("#,##0.00")),
                                    columnLength));
                                break;
                            case CodeNames.PaymentType.Cheque.Id:
                                sb.AppendLine(
                                    CreateLineString(
                                    string.Format("  - {0} {1} {2} ลว.{3}", pm.PtName, pm.BankName, pm.ChqNo, pm.ChqDt.Value.ToString("dd/MM/yyyy", ci)),
                                    string.Format("{0} บาท", pm.ToPayAmount.Value.ToString("#,##0.00")),
                                    columnLength));
                                break;
                            case CodeNames.PaymentType.QRPayment.Id:
                                sb.AppendLine(
                                  CreateLineString(
                                  string.Format("  - {0}", pm.PtName),
                                  string.Format("{0} บาท", pmx.Amount.ToString("#,##0.00")),
                                  columnLength));
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            return sb.ToString();
        }

        protected string GetTotalPayment(PrintingReceipt receipt, List<PaymentMethod> paymentMethods)
        {
            StringBuilder sb = new StringBuilder();

            if (receipt.PrintingSequence == receipt.TotalReceipt && receipt.TotalReceipt > 1)
            {
                sb.AppendLine(CreateLineString("รวมทั้งหมด",
                    string.Format("{0} บาท", receipt.TotalAmount.ToString("#,##0.00")), columnLength));
                sb.Append(GetPaymentMethod(paymentMethods));
                sb.AppendLine(CreateLineString(
                    string.Format("ชำระ  {0} บาท", receipt.PaidAmount.ToString("#,##0.00")),
                    string.Format("ทอนเงิน  {0} บาท", receipt.ChangeAmount.ToString("#,##0.00")),
                    columnLength));

                if (receipt.AdjChangeAmount != 0)
                {
                    sb.AppendLine(CreateLineString("MISC.REV", string.Format("{0} บาท", receipt.AdjChangeAmount.ToString("#,##0.00")), columnLength));
                }
            }

            return sb.ToString();
        }

        private string GetPaymentMethodsTextByStoredProcedure(string paymentMethodWithComma)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                string[] comms = paymentMethodWithComma.Split('@');
                foreach (var s in comms)
                {
                    string[] subs = s.Split('|');
                    sb.AppendLine(CreateLineString(string.Format("  - {0}", subs[0]),
                                                   string.Format("{0} บาท", subs[1]),
                                                   columnLength));
                } 
            }
            catch (Exception ex)
            {
                
            }
                      

            return sb.ToString();
        }

        private string GetPaymentMethod(List<PaymentMethod> paymentMethods)
        {
            StringBuilder sb = new StringBuilder();

            if (null != paymentMethods)
            {
                if (paymentMethods.Count > 1 || paymentMethods[0].PtId != CodeNames.PaymentType.Cash.Id)
                {
                    paymentMethods.Sort();
                    foreach (PaymentMethod pm in paymentMethods)
                    {
                        switch (pm.PtId)
                        {
                            case CodeNames.PaymentType.Cash.Id:
                                sb.AppendLine(
                                    CreateLineString(
                                    string.Format("  - {0}", pm.PtName),
                                    string.Format("{0} บาท", pm.ToPayAmount.Value.ToString("#,##0.00")),
                                    columnLength));
                                break;
                            case CodeNames.PaymentType.Deposit.Id:
                                sb.AppendLine(
                                    CreateLineString(
                                    string.Format("  - {0} {1} {2}", pm.PtName, pm.DepositAccNo, pm.BankName),
                                    string.Format("{0} บาท", pm.ToPayAmount.Value.ToString("#,##0.00")),
                                    columnLength));
                                break;
                            case CodeNames.PaymentType.Cheque.Id:
                                sb.AppendLine(
                                    CreateLineString(
                                    string.Format("  - {0} {1} {2} ลว.{3}", pm.PtName, pm.BankName, pm.ChqNo, pm.ChqDt.Value.ToString("dd/MM/yyyy", ci)),
                                    string.Format("{0} บาท", pm.ToPayAmount.Value.ToString("#,##0.00")),
                                    columnLength));
                                break;
                            case CodeNames.PaymentType.QRPayment.Id:
                                sb.AppendLine(
                                    CreateLineString(
                                    string.Format("  - {0}", pm.PtName),
                                    string.Format("{0} บาท", pm.ToPayAmount.Value.ToString("#,##0.00")),
                                    columnLength));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return sb.ToString();
        }

        protected string GetReceiptSummary(PrintingReceipt receipt, List<string> allReceiptsNo)
        {
            StringBuilder sb = new StringBuilder();

            //// รวมใบเสร็จแผนผ่อน DefectLog #104 เรียงลำดับ การแสดงใบเสร็จ  สำหรับการรับชำระครั้งแรก
            ////allReceiptsNo = allReceiptsNo.OrderBy(r=>r).ToList();

            if (null != allReceiptsNo && allReceiptsNo.Count>0)
            {
                if (receipt.PrintingSequence == receipt.TotalReceipt)
                {
                    sb.AppendLine(
                        CreateLineString(
                        string.Format("ชำระใบเสร็จเลขที่ {0}", allReceiptsNo[0]),
                        "",
                        columnLength)
                        );

                    int i;
                    for (i = 1; i < allReceiptsNo.Count; i++)
                    {
                        if (allReceiptsNo[i].Length == 0)
                        {
                            i++;
                            sb.AppendLine(
                                CreateLineString(
                                string.Format("ยกเลิกใบเสร็จเลขที่ {0}", allReceiptsNo[i]),
                                "",
                                columnLength)
                                );

                        }
                        else
                        {
                            sb.AppendLine("               " + allReceiptsNo[i]);
                        }
                    }


                }
            }

            return sb.ToString();
        }

        protected string GetPaymentDate(PrintingReceipt receipt, Bill bill)
        {
            StringBuilder sb = new StringBuilder();

            if (bill.ControllerId == null || bill.ControllerId.Trim().Length == 0)
            {
                sb.AppendLine(string.Format("วันที่ชำระเงิน {0} เวลา {1} น.",
                    GetThaiDate(receipt.PaymentDate), receipt.PaymentDate.ToString("HH:mm")));
            }
            else
            {
                sb.AppendLine(string.Format("วันที่ชำระเงิน {0} เวลา {1} น. คุมใบเสร็จ {2}",
                    GetThaiDate(receipt.PaymentDate), receipt.PaymentDate.ToString("HH:mm"), bill.ControllerId.TrimStart('0')));
            }
            sb.AppendLine("----------------------------------------------------");

            return sb.ToString();
        }

        protected string GetInvoiceReference(PrintingInvoice invoice)
        {
            StringBuilder sb = new StringBuilder();

            string debtId = invoice.Bills[0].DebtId;
            //if (debtId.Substring(0, 5) != "M0017" || debtId.Substring(0, 6) == "M00171")
            //{
            //    if (debtId.Substring(0, 5) != "M0017" || debtId.Substring(0, 6) == "M00171")
            //    {
                    if (debtId == CodeNames.DebtType.AgencyGroupInvoicing.Id)
                    {
                        sb.AppendLine(string.Format("อ้างถึงหนังสือ มท {0}{1}", invoice.Bills[0].GroupInvoiceId
                            , invoice.InvoiceDate == null ? "" : string.Format("   ลว. {0}", GetThaiDate(invoice.InvoiceDate))
                            ));
                    }
                    else if (invoice.Bills[0].GroupInvoiceId != null)//AJ
                    {
                        sb.AppendLine(string.Format("อ้างถึงหนังสือ มท {0}{1}", invoice.Bills[0].GroupInvoiceId
                            , invoice.InvoiceDate == null ? "" : string.Format("   ลว. {0}", GetThaiDate(invoice.InvoiceDate))
                            ));
                    }
                    else if (invoice.DisplayInvoiceNo != string.Empty)
                    {
                        if (debtId == "M00100100")
                        {
                            sb.AppendLine(string.Format("อ้างถึงใบเพิ่มหนี้เลขที่ {0}{1}", invoice.DisplayInvoiceNo
                                , invoice.InvoiceDate == null ? "" : string.Format("   ลว. {0}", GetThaiDate(invoice.InvoiceDate))
                                ));
                        }
                        else
                        {
                            sb.AppendLine(string.Format("อ้างถึงใบแจ้งหนี้เลขที่ {0}{1}", invoice.DisplayInvoiceNo
                                , invoice.InvoiceDate == null ? "" : string.Format("   ลว. {0}", GetThaiDate(invoice.InvoiceDate))
                                ));
                        }
                    }
            //    }
            //}

            return sb.ToString();
        }

        protected string GetThaiDate(DateTime? dateTime)
        {
            if (null == dateTime)
            {
                return string.Empty;
            }
            else
            {
                return dateTime.Value.ToString("dd/MM/yyyy", ci);
            }
        }

        string poolDir = string.Format("{0}/{1}", BPMPath.ConfigPath, CodeNames.SlipPrintintPoolDir);

        public void Print(List<object> printData)
        {
            string fname = string.Format("{0}/{1}{2}.txt", poolDir, DateTime.Now.ToString("yyyyMMddHHmmss"), DateTime.Now.Millisecond.ToString("000"));

            WriteToFile(fname, printData);

            Thread.Sleep(100);
        }

        private static object syncRoot = new object();

        protected static void WriteToFile(string fileName, List<object> printData)
        {
            lock (syncRoot)
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    foreach (object o in printData)
                    {
                        if (o.GetType() == typeof(string))
                        {
                            sw.Write((string)o);
                        }
                    }
                    sw.Close();
                }
            }
        }
        
        protected string GetFooter(PrintingReceipt receipt)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(string.Format("ผู้รับเงิน {0}   รหัสผู้รับเงิน {1}", receipt.CashierName, receipt.CashierId.TrimStart('0')));

            return sb.ToString();
        }

        protected char C(int value)
        {
            return ((char)value);
        }

        protected string Ch(int value)
        {
            return ((char)value).ToString();
        }
        
        protected string RemoveNoCountChars(string text)
        {
            for (int i = 0; i < noCountChar.Length; i++)
            {
                text = text.Replace(noCountChar[i].ToString(), "");
            }
            return text;
        }
    }
}
