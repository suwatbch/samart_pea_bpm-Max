using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.Globalization;
using Microsoft.Reporting.WinForms;
using PEA.BPM.Architecture.PrintUtilities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool;
using System.Web.Util;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.PreprintedReceipt
{
    public class ElectricReceipt
    {
        private List<Header> _hs;
        private List<Detail> _ds;
        private List<ReceiptPaymentMethod> _pm;
        private CultureInfo ci = new CultureInfo("th-TH");

        public static ElectricReceipt Instance()
        {
            return new ElectricReceipt();
        }

        public void Print(PrintingReceipt receipt, List<string> allReceiptsNo, List<PaymentMethod> paymentMethods)
        {
            decimal? subTotal = 0;
            decimal? vatAmount = 0;
            decimal? totalAmount = 0;

            bool        IsGroupReceipt          = false;
            decimal?    GroupReceiptQty         = 0;
            decimal?    GroupReceiptAmount      = 0;
            decimal?    GroupReceiptVatTotal    = 0;
            decimal?    GroupReceiptTotal       = 0;
            string GroupReceiptMeterIdText      = null;
            string GroupReceiptRateTypeText     = null;
            string GroupReceiptInstallmentText  = null;
            var  GroupPaymentMethod             = new List<PaymentMethod>();
            GroupPaymentMethod                  = paymentMethods;

            List<Detail> ds = new List<Detail>();
            foreach (Invoice iv in receipt.PrintingInvoices)
            {
                List<DebtType> dt = CodeTable.Instant.ListDebtTypes().FindAll(delegate(DebtType d) { return d.DebtId == iv.Bills[0].DebtId && d.PrintDescription != null && d.PrintDescription != ""; });

                if (dt.Count > 0)
                {
                    Detail d = new Detail();
                    string receiptType = iv.Bills[0].TaxCode == null || iv.Bills[0].TaxCode[0] == 'O' || iv.Bills[0].TaxRate == null ? "" : iv.GAmount != iv.ToPayGAmount ? "" : "รายละเอียดตามใบแนบใบกำกับภาษี";

                    //d.ProductDesc = string.Format("{0} {1}", dt[0].PrintDescription, receiptType);


                    if (iv.GAmount == iv.ToPayGAmount)
                    {
                        d.ProductDesc = string.Format("{0} {1}", dt[0].PrintDescription, receiptType);
                    }
                    else
                    {
                        #region
                        if (iv.InstallmentTotalPeriod != null && iv.InstallmentTotalPeriod > 0)
                        {
                            if (iv.InstallmentPeriod == iv.InstallmentTotalPeriod)
                            {
                                if (iv.PartialPayment != null)
                                {
                                    if (iv.PartialPayment == 1)
                                    {
                                        d.ProductDesc = string.Format("{0} บางส่วน {1}", dt[0].PrintDescription, receiptType);
                                        //d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                                    }
                                    else
                                    {
                                        d.ProductDesc = string.Format("{0} ส่วนที่เหลือ {1}", dt[0].PrintDescription, receiptType);
                                        //d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                                    }
                                }
                                else
                                {
                                    if (iv.Bills[0].GAmount != iv.ToPayGAmount)
                                    {
                                        d.ProductDesc = string.Format("{0} บางส่วน {1}", dt[0].PrintDescription, receiptType);
                                        //d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                                    }
                                    else
                                    {
                                        d.ProductDesc = string.Format("{0} ส่วนที่เหลือ {1}", dt[0].PrintDescription, receiptType);
                                        //d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                                    }
                                }
                            }
                            else
                            {
                                d.ProductDesc = string.Format("{0} บางส่วน {1}", dt[0].PrintDescription, receiptType);
                                //d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                            }
                        }
                        else
                        {
                            if (iv.PartialPayment != null)
                            {
                                if (iv.PartialPayment == 1)
                                {
                                    d.ProductDesc = string.Format("{0} บางส่วน {1}", dt[0].PrintDescription, receiptType);
                                    //d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                                }
                                else
                                {
                                    d.ProductDesc = string.Format("{0} ส่วนที่เหลือ {1}", dt[0].PrintDescription, receiptType);
                                    //d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                                }
                            }
                            else
                            {
                                if (iv.TotalPaidAmount != iv.ToPayGAmount)
                                {
                                    d.ProductDesc = string.Format("{0} บางส่วน {1}", dt[0].PrintDescription, receiptType);
                                    //d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                                }
                                else
                                {
                                    d.ProductDesc = string.Format("{0} ส่วนที่เหลือ {1}", dt[0].PrintDescription, receiptType);
                                    //d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                                }
                            }
                        }
                        #endregion
                    }
                    

                    d.Amount = iv.ToPayGAmount - ((iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount);
                    subTotal += iv.ToPayGAmount - ((iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount);
                    vatAmount += (iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount;
                    totalAmount += iv.ToPayGAmount;

                    ////DCR รวมใบเสร็จแผนผ่อน 2021AUG (ElectricReceipt PrePrinted)
                    //// STEP 1) Receipt Detail
                    if (!String.IsNullOrEmpty(iv.GroupReceiptInstallmentText))
                    {
                        IsGroupReceipt          = true;
                        d.ProductDesc           = iv.GroupReceiptPeriodText;
                        d.Amount                = iv.GroupReceiptAmount;
                        d.Quantity              = receipt.GroupReceiptQty.ToString();
                        d.GroupReceiptInstallmentText   = iv.GroupReceiptInstallmentText;
                        d.GroupReceiptQty               = iv.GroupReceiptQty;
                        d.GroupReceiptVatTotal          = iv.GroupReceiptVatTotal;
                        d.GroupReceiptTotal             = iv.GroupReceiptTotal;
                        d.GroupReceiptMeterIdText       = iv.GroupReceiptMeterIdText;
                        d.GroupReceiptRateTypeText      = iv.GroupReceiptRateTypeText;

                        //Internal Variables Data for Header
                        GroupReceiptQty             = iv.GroupReceiptQty;
                        GroupReceiptAmount          = iv.GroupReceiptAmount;
                        GroupReceiptVatTotal        = iv.GroupReceiptVatTotal;
                        GroupReceiptTotal           = iv.GroupReceiptTotal;
                        GroupReceiptMeterIdText     = iv.GroupReceiptMeterIdText;
                        GroupReceiptRateTypeText    = iv.GroupReceiptRateTypeText;
                        GroupReceiptInstallmentText = iv.GroupReceiptInstallmentText;
                    }
                    else if (receipt.GroupReceiptOrNot == "Y")
                    {
                        IsGroupReceipt              = true;
                        d.ProductDesc               = receipt.GroupReceiptPeriodText;
                        d.Amount                    = receipt.GroupReceiptAmount;
                        d.Quantity                  = receipt.GroupReceiptQty.ToString();
                        d.GroupReceiptInstallmentText = receipt.GroupReceiptInstallmentText;
                        d.GroupReceiptQty           = receipt.GroupReceiptQty;
                        d.GroupReceiptVatTotal      = receipt.GroupReceiptVatTotal;
                        d.GroupReceiptTotal         = receipt.GroupReceiptTotal;
                        d.GroupReceiptMeterIdText   = receipt.GroupReceiptMeterIdText;
                        d.GroupReceiptRateTypeText  = receipt.GroupReceiptRateTypeText;
                        d.GroupXReceiptId           = receipt.GroupXReceiptId;

                        
                        GroupReceiptQty             = receipt.GroupReceiptQty;
                        GroupReceiptAmount          = receipt.GroupReceiptAmount;
                        GroupReceiptVatTotal        = receipt.GroupReceiptVatTotal;
                        GroupReceiptTotal           = receipt.GroupReceiptTotal;
                        GroupReceiptMeterIdText     = receipt.GroupReceiptMeterIdText;
                        GroupReceiptRateTypeText    = receipt.GroupReceiptRateTypeText;
                        GroupReceiptInstallmentText = receipt.GroupReceiptInstallmentText;
                    }
                    
                    ds.Add(d);
                }
                else
                {
                    Detail d = new Detail();

                    
                    
                    //d.AccountId = GetAccountNo(b.DebtId);

                    if (iv.GAmount == iv.ToPayGAmount)
                    {
                        d.ProductDesc = iv.Bills[0].PrePrintedItemDescription;
                    }
                    else
                    {
                        #region
                        if (iv.InstallmentTotalPeriod != null && iv.InstallmentTotalPeriod > 0)
                        {
                            if (iv.InstallmentPeriod == iv.InstallmentTotalPeriod)
                            {
                                if (iv.PartialPayment != null)
                                {
                                    if (iv.PartialPayment == 1)
                                    {
                                        d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                                    }
                                    else
                                    {
                                        d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                                    }
                                }
                                else
                                {
                                    if (iv.Bills[0].GAmount != iv.ToPayGAmount)
                                    {
                                        d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                                    }
                                    else
                                    {
                                        d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                                    }
                                }
                            }
                            else
                            {
                                d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                            }
                        }
                        else
                        {
                            if (iv.PartialPayment != null)
                            {
                                if (iv.PartialPayment == 1)
                                {
                                    d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                                }
                                else
                                {
                                    d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                                }
                            }
                            else
                            {
                                if (iv.TotalPaidAmount != iv.ToPayGAmount)
                                {
                                    d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                                }
                                else
                                {
                                    d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                                }
                            }
                        }
                        #endregion
                    }

                    if (iv.ToPayQty != null && iv.ToPayQty > 0)
                    {
                        d.Quantity = iv.ToPayQty.ToString();
                        d.UnitPrice = iv.Bills[0].UnitPrice;
                    }
                    d.Amount = iv.ToPayGAmount - ((iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount);
                    subTotal += iv.ToPayGAmount - ((iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount);
                    vatAmount += (iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount;
                    totalAmount += iv.ToPayGAmount;

                    


                    Detail energy = new Detail();
                    if (iv.EnergyAmount > 0) 
                    {
                        d.ProductDesc = d.ProductDesc.Replace("\nรายละเอียดตามใบแนบใบกำกับภาษี", "");
                        d.Amount = d.Amount - iv.EnergyAmount;
                        
                        energy.ProductDesc = "ค่าจัดการพลังงาน\nรายละเอียดตามใบแนบใบกำกับภาษี";
                        energy.Amount = iv.EnergyAmount;

                    }


                    ////DCR รวมใบเสร็จแผนผ่อน 2021AUG (ElectricReceipt PrePrinted)
                    //// STEP 1) Receipt Detail
                    
                    if (!String.IsNullOrEmpty(iv.GroupReceiptId))
                    {
                        IsGroupReceipt              = true;
                        d.ProductDesc               = iv.GroupReceiptPeriodText;
                        d.Amount                    = iv.GroupReceiptAmount;
                        d.GroupReceiptInstallmentText = iv.GroupReceiptInstallmentText;
                        d.GroupReceiptQty           = iv.GroupReceiptQty;
                        d.GroupReceiptVatTotal      = iv.GroupReceiptVatTotal;
                        d.GroupReceiptTotal         = iv.GroupReceiptTotal;
                        d.GroupReceiptMeterIdText   = iv.GroupReceiptMeterIdText;
                        d.GroupReceiptRateTypeText  = iv.GroupReceiptRateTypeText;
                        d.Quantity                  = iv.GroupReceiptQty.ToString();


                        //Internal Variables Data for Header
                        GroupReceiptQty             = iv.GroupReceiptQty;
                        GroupReceiptAmount          = iv.GroupReceiptAmount;
                        GroupReceiptVatTotal        = iv.GroupReceiptVatTotal;
                        GroupReceiptTotal           = iv.GroupReceiptTotal;
                        GroupReceiptMeterIdText     = iv.GroupReceiptMeterIdText;
                        GroupReceiptRateTypeText    = iv.GroupReceiptRateTypeText;
                        GroupReceiptInstallmentText = receipt.GroupReceiptInstallmentText;
                    }
                    
                    if (receipt.GroupReceiptOrNot == "Y")
                    {
                        IsGroupReceipt      = true;
                        d.ProductDesc       = receipt.GroupReceiptPeriodText;
                        d.Amount            = receipt.GroupReceiptAmount;
                        d.GroupReceiptInstallmentText = receipt.GroupReceiptInstallmentText;
                        d.GroupReceiptQty   = receipt.GroupReceiptQty;
                        d.GroupReceiptVatTotal      = receipt.GroupReceiptVatTotal;
                        d.GroupReceiptTotal         = receipt.GroupReceiptTotal;
                        d.GroupReceiptMeterIdText   = receipt.GroupReceiptMeterIdText;
                        d.GroupReceiptRateTypeText  = receipt.GroupReceiptRateTypeText;
                        d.Quantity                  = receipt.GroupReceiptQty.ToString();

                        GroupReceiptQty             = receipt.GroupReceiptQty;
                        GroupReceiptAmount          = receipt.GroupReceiptAmount;
                        GroupReceiptVatTotal        = receipt.GroupReceiptVatTotal;
                        GroupReceiptTotal           = receipt.GroupReceiptTotal;
                        GroupReceiptMeterIdText     = receipt.GroupReceiptMeterIdText;
                        GroupReceiptRateTypeText    = receipt.GroupReceiptRateTypeText;
                        GroupReceiptInstallmentText = receipt.GroupReceiptInstallmentText;
                    }
                    
                    ds.Add(d);

                    if (iv.EnergyAmount > 0)
                    {
                        ds.Add(energy);
                    }

                }

                #region NOT USE CODE
                ////ค่าขยายเขต
                //if (iv.Bills[0].DebtId.Substring(0, 6) == "M00176")
                //{
                //    Detail d = new Detail();
                //    //d.AccountId = GetAccountNo(b.DebtId);

                //    if (iv.GAmount == iv.ToPayGAmount)
                //    {
                //        d.ProductDesc = "ค่าขยายเขต";
                //    }
                //    else
                //    {
                //        if (iv.InstallmentTotalPeriod != null && iv.InstallmentTotalPeriod > 0)
                //        {
                //            if (iv.InstallmentPeriod == iv.InstallmentTotalPeriod)
                //            {
                //                if (iv.Bills[0].GAmount != iv.ToPayGAmount)
                //                {
                //                    d.ProductDesc = string.Format("ค่าขยายเขต บางส่วน");
                //                }
                //                else
                //                {
                //                    d.ProductDesc = string.Format("ค่าขยายเขต ส่วนที่เหลือ");
                //                }
                //            }
                //            else
                //            {
                //                d.ProductDesc = string.Format("ค่าขยายเขต บางส่วน");
                //            }
                //        }
                //        else
                //        {
                //            if (iv.PartialPayment != null)
                //            {
                //                if (iv.PartialPayment == 1)
                //                {
                //                    d.ProductDesc = string.Format("ค่าขยายเขต บางส่วน");
                //                }
                //                else
                //                {
                //                    d.ProductDesc = string.Format("ค่าขยายเขต ส่วนที่เหลือ");
                //                }
                //            }
                //            else
                //            {
                //                if (iv.TotalPaidAmount != iv.ToPayGAmount)
                //                {
                //                    d.ProductDesc = string.Format("ค่าขยายเขต บางส่วน");
                //                }
                //                else
                //                {
                //                    d.ProductDesc = string.Format("ค่าขยายเขต ส่วนที่เหลือ");
                //                }
                //            }
                //        }
                //    }


                //    d.Amount = iv.ToPayGAmount - ((iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount);
                //    subTotal += iv.ToPayGAmount - ((iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount);
                //    vatAmount += (iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount;
                //    totalAmount += iv.ToPayGAmount;
                //    ds.Add(d);
                //}
                ////ค่าบริการ
                //else if (iv.Bills[0].DebtId.Substring(0, 5) == "M0017" || iv.Bills[0].DebtId.Substring(0, 5) == "M0018")
                //{
                //    Detail d = new Detail();
                //    //d.AccountId = GetAccountNo(b.DebtId);

                //    if (iv.GAmount == iv.ToPayGAmount)
                //    {
                //        d.ProductDesc = "ค่าบริการ";
                //    }
                //    else
                //    {
                //        if (iv.InstallmentTotalPeriod != null && iv.InstallmentTotalPeriod > 0)
                //        {
                //            if (iv.InstallmentPeriod == iv.InstallmentTotalPeriod)
                //            {
                //                if (iv.Bills[0].GAmount != iv.ToPayGAmount)
                //                {
                //                    d.ProductDesc = string.Format("ค่าบริการ บางส่วน");
                //                }
                //                else
                //                {
                //                    d.ProductDesc = string.Format("ค่าบริการ ส่วนที่เหลือ");
                //                }
                //            }
                //            else
                //            {
                //                d.ProductDesc = string.Format("ค่าบริการ บางส่วน");
                //            }
                //        }
                //        else
                //        {
                //            if (iv.PartialPayment != null)
                //            {
                //                if (iv.PartialPayment == 1)
                //                {
                //                    d.ProductDesc = string.Format("ค่าบริการ บางส่วน");
                //                }
                //                else
                //                {
                //                    d.ProductDesc = string.Format("ค่าบริการ ส่วนที่เหลือ");
                //                }
                //            }
                //            else
                //            {
                //                if (iv.TotalPaidAmount != iv.ToPayGAmount)
                //                {
                //                    d.ProductDesc = string.Format("ค่าบริการ บางส่วน");
                //                }
                //                else
                //                {
                //                    d.ProductDesc = string.Format("ค่าบริการ ส่วนที่เหลือ");
                //                }
                //            }
                //        }
                //    }

                //    d.Amount = iv.ToPayGAmount - ((iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount);
                //    subTotal += iv.ToPayGAmount - ((iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount);
                //    vatAmount += (iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount;
                //    totalAmount += iv.ToPayGAmount;
                //    ds.Add(d);
                //}
                ////ค่าอื่นๆที่เหลือที่ไม่ได้อยู่ loop ข้างบน
                //else
                //{
                //    Detail d = new Detail();
                //    //d.AccountId = GetAccountNo(b.DebtId);

                //    if (iv.GAmount == iv.ToPayGAmount)
                //    {
                //        d.ProductDesc = iv.Bills[0].PrePrintedItemDescription;
                //    }
                //    else
                //    {
                //        if (iv.InstallmentTotalPeriod != null && iv.InstallmentTotalPeriod > 0)
                //        {
                //            if (iv.InstallmentPeriod == iv.InstallmentTotalPeriod)
                //            {
                //                if (iv.PartialPayment != null)
                //                {
                //                    if (iv.PartialPayment == 1)
                //                    {
                //                        d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                //                    }
                //                    else
                //                    {
                //                        d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                //                    }
                //                }
                //                else
                //                {
                //                    if (iv.Bills[0].GAmount != iv.ToPayGAmount)
                //                    {
                //                        d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                //                    }
                //                    else
                //                    {
                //                        d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                //                    }
                //                }
                //            }
                //            else
                //            {
                //                d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                //            }
                //        }
                //        else
                //        {
                //            if (iv.PartialPayment != null)
                //            {
                //                if (iv.PartialPayment == 1)
                //                {
                //                    d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                //                }
                //                else
                //                {
                //                    d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                //                }
                //            }
                //            else
                //            {
                //                if (iv.TotalPaidAmount != iv.ToPayGAmount)
                //                {
                //                    d.ProductDesc = string.Format("{0} บางส่วน", iv.Bills[0].PrePrintedItemDescription);
                //                }
                //                else
                //                {
                //                    d.ProductDesc = string.Format("{0} ส่วนที่เหลือ", iv.Bills[0].PrePrintedItemDescription);
                //                }
                //            }
                //        }
                //    }

                //    //if (iv.Bills[0].Qty > 0)
                //    //{
                //    //    d.Quantity = iv.Bills[0].Qty.ToString();
                //    //    d.UnitPrice = iv.Bills[0].UnitPrice;
                //    //}
                //    if (iv.ToPayQty != null && iv.ToPayQty > 0)
                //    {
                //        d.Quantity = iv.ToPayQty.ToString();
                //        d.UnitPrice = iv.Bills[0].UnitPrice;
                //    }
                //    d.Amount = iv.ToPayGAmount - ((iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount);
                //    subTotal += iv.ToPayGAmount - ((iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount);
                //    vatAmount += (iv.ToPayVatAmount == null) ? 0 : iv.ToPayVatAmount;
                //    totalAmount += iv.ToPayGAmount;
                //    ds.Add(d);
                //} 
                #endregion
            }


            List<Header> hs = new List<Header>();
            Header h = new Header();
            h.ReceiptNo = receipt.PrePrintedHeader;

            //Check Head Office
            if (Session.Branch.Number == "0000" || Session.Branch.Number == "00000")
            {
                h.BranchName = string.Format("{0} ({1})", Session.Branch.Name, "สำนักงานใหญ่");
            }
            else 
            {
                //h.BranchName = string.Format("{0} (สาขาที่ {1})", Session.Branch.Name, Session.Branch.Number);
                // ความยาวตัวอักษร 1 บรรทัด 48 ตัวอักษร หักลบ ความยามของสาขา "(สาขาที่ xxxx)" 11 ตัวอักษร 

                //if (StringConvert.TextLength(Session.Branch.Name) >= 30)
                //{
                //    h.BranchName = string.Format("{0}", Session.Branch.Name);
                //    h.BranchNameLine2 = string.Format("(สาขาที่ {0})", Session.Branch.Number);
                //}
                //else 
                //{
                //    h.BranchName = string.Format("{0} (สาขาที่ {1})", Session.Branch.Name, Session.Branch.Number);
                //}


                //แก้ไขความยาวชื่อ ให้มีการตัดเป็น 2 บรรทัด 06/01/2559
                if (StringConvert.TextLength(Session.Branch.Name) >= 30)
                {                                   
                    int nameLenght = 50;

                    string branchName1 = "";
                    string branchName2 = "";
                    string tmpBranchName1 = ""; 
                    string branchName = Session.Branch.Name;

                    int lastSpaceIndex = Session.Branch.Name.LastIndexOfAny(new char[] { ' ' });
                    if (lastSpaceIndex < 0)
                    {
                        lastSpaceIndex = 0;
                        var a = Session.Branch.Name.Substring(0, Session.Branch.Name.Length);
                        var b = string.Format("(สาขาที่ {0})", Session.Branch.Number);
                        tmpBranchName1 = String.Format("{0} {1}", a, b);
                        if (tmpBranchName1.Length >= 45)
                        {
                            if (branchName.Length == 50)
                            {
                                branchName1 = branchName.Substring(0, nameLenght);
                                branchName2 = b;
                            }
                            else if (branchName.Length > 50)
                            {
                                branchName1 = branchName.Substring(0, nameLenght);
                                var tmp2 = branchName.Substring(nameLenght, branchName.Length - nameLenght);
                                branchName2 = String.Format("{0} {1}", tmp2, b);
                            }
                            else
                            {
                                branchName1 = branchName;
                                branchName2 = b;
                            }
                        }
                        else
                        {
                            branchName1 = tmpBranchName1;
                        }
                    }
                    else
                    {
                        branchName1 = Session.Branch.Name.Substring(0, lastSpaceIndex);
                        branchName2 = string.Format("{0} (สาขาที่ {1})", Session.Branch.Name.Substring(lastSpaceIndex, Session.Branch.Name.Length - lastSpaceIndex), Session.Branch.Number);

                        if (branchName1.Length > nameLenght || branchName2.Length > nameLenght)
                        {
                            branchName1 = Session.Branch.Name.Substring(0, nameLenght);
                            branchName2 = string.Format("{0} (สาขาที่ {1})", Session.Branch.Name.Substring(nameLenght, Session.Branch.Name.Length - nameLenght), Session.Branch.Number);
                        }
                    }
                   
                    h.BranchName = branchName1.Trim();
                    h.BranchNameLine2 = branchName2.Trim();

                    #region Old logic comment by Uthen 6-01-2016
                    ////h.BranchName = string.Format("{0}", Session.Branch.Name);
                    ////h.BranchNameLine2 = string.Format("(สาขาที่ {0})", Session.Branch.Number);
                    //int nameLenght = 50;

                    //string branchName1 = "";
                    //string branchName2 = "";

                    //int lastSpaceIndex = Session.Branch.Name.LastIndexOfAny(new char[] { ' ' });
                    //if (lastSpaceIndex < 0) lastSpaceIndex = 0;

                    //branchName1 = Session.Branch.Name.Substring(0,lastSpaceIndex);
                    //branchName2 = string.Format("{0} (สาขาที่ {1})", Session.Branch.Name.Substring(lastSpaceIndex, Session.Branch.Name.Length - lastSpaceIndex), Session.Branch.Number);

                    //if (branchName1.Length > nameLenght || branchName2.Length > nameLenght)
                    //{
                    //    branchName1 = Session.Branch.Name.Substring(0, nameLenght);
                    //    branchName2 = string.Format("{0} (สาขาที่ {1})", Session.Branch.Name.Substring(nameLenght, Session.Branch.Name.Length - nameLenght), Session.Branch.Number);
                    //}

                    //h.BranchName = branchName1.Trim();
                    //h.BranchNameLine2 = branchName2.Trim();
                    #endregion
                }
                else
                {
                    h.BranchName = string.Format("{0} (สาขาที่ {1})", Session.Branch.Name, Session.Branch.Number);
                }

            }
            h.Address         = Session.Branch.Address;

            //Begin Add new code for format branch address --Uthen 2018-05-25
            int addressLength = StringConvert.TextLength(h.Address);
            try
            {
                addressLength = Convert.ToInt32(CodeTable.Instant.GetAppSettingValue("BRANCH_ADDRESS_LENGTH"));
                h.Address     = CutoffExceedLengthOfAddressElectricReceipt(addressLength, h.Address);
            }
            catch
            {
                h.Address     = Session.Branch.Address; 
            }          
            //End 

            h.CustomerId = receipt.DisplayCaId;
            h.CustomerAddress = (receipt.CustomerAddress == null) ? "" : StringConvert.FormatText(receipt.CustomerAddress, 80);

            //Tax13
            h.CaTaxId = receipt.CaTaxId;
            h.CaTaxBranch = receipt.CaTaxBranch;

            h.CustomerName = receipt.CustomerName;
            h.IssueDate = receipt.PaymentDate.ToString("d/M/yyyy", ci);
            h.InvoiceId = receipt.PrintingInvoices[0].DisplayInvoiceNo;
            h.SubTotal = subTotal;
            h.VatRate = (receipt.PrintingInvoices[0].Bills[0].TaxRate == null || receipt.PrintingInvoices[0].Bills[0].TaxCode[0] == 'O') ? null : StringConvert.ToInt32(receipt.PrintingInvoices[0].Bills[0].TaxRate.Value.ToString("#")); ;
            h.VatAmount = (receipt.PrintingInvoices[0].Bills[0].TaxRate == null || receipt.PrintingInvoices[0].Bills[0].TaxCode[0] == 'O') ? null : vatAmount;
            h.TotalAmount = totalAmount;

            if ((receipt.TotalReceipt == receipt.PrintingSequence || receipt.TotalReceipt == 1) && receipt.AdjChangeAmount != 0)
            {
                h.MiscRev = (receipt.AdjChangeAmount != 0) ? "MISC.REV " + String.Format("{0:#0.00}", receipt.AdjChangeAmount) : "";
            }
            else
            {
                h.MiscRev = "";
            }
            h.TextAmount = string.Format("({0})", StringConvert.ConvertAmountToText(string.Format("{0:###.00}", totalAmount)));
            h.PageSequence = (receipt.TotalReceipt == 1) ? "" : string.Format("({0}/{1})", receipt.PrintingSequence, receipt.TotalReceipt);
            
            ////DCR รวมใบเสร็จแผนผ่อน 2021AUG (ElectricReceipt PrePrinted)
            //// STEP 2) Receipt Header
            if(IsGroupReceipt == true)
            {
                h.InvoiceId     = "";  // ไม่ต้องแสดง InvoiceNo
                h.VatAmount     = GroupReceiptVatTotal;
                h.SubTotal      = GroupReceiptAmount;
                h.TotalAmount   = GroupReceiptTotal;
                h.TextAmount    = string.Format("({0})", StringConvert.ConvertAmountToText(string.Format("{0:###.00}", GroupReceiptTotal)));                
            }

            //h.DtId = dtId;
            if (receipt.PrintingInvoices.Count == 1)
                if (receipt.PrintingInvoices[0].Bills.Count == 1)
                    h.DebtId = receipt.PrintingInvoices[0].Bills[0].DebtId;

            //// DCR GroupInvoiceText 2021-OCT-25 Uthen.P
            if (receipt.GroupInvoiceDescriptionText != null)
            {
                try
                {
                    string tmpHeaderReceipt = h.ReceiptNo;
                    string ReceiptWithTax   = "ใบเสร็จรับเงิน/ใบกำกับภาษี";
                    string ReceiptNoTax     = "ใบเสร็จรับเงิน";
                    string[] HeaderReceipts = h.ReceiptNo.Split(' ');
                    int LastIndex           = HeaderReceipts.Length-1;
                    if (vatAmount > 0)
                    {
                        HeaderReceipts[0] = ReceiptWithTax;
                    }
                    else
                    {
                        HeaderReceipts[0] = ReceiptNoTax;
                    }
                    h.ReceiptNo = string.Format("{0} เลขที่ {2}", HeaderReceipts[0], HeaderReceipts[LastIndex]);
                }
                catch (Exception ex)
                { 
                    
                }                
            }

            hs.Add(h);


            List<ReceiptPaymentMethod> pm = new List<ReceiptPaymentMethod>();
            List<InvoicePaymentMethod> pp = new List<InvoicePaymentMethod>();
            foreach (PrintingInvoice iv in receipt.PrintingInvoices)
            {
                foreach (InvoicePaymentMethod ivpm in iv.PaymentMethods)
                {
                    pp.Add(ivpm);
                }
            }

            //// DCR รวมใบเสร็จแผนผ่อน แก้ไข Print รายละเอียดการชำระเงินไม่ครบ
            if (receipt.GroupReceiptOrNot == "Y")
            {
                if (receipt.GroupReceiptPaymentMethodsWithPipe != null)
                {
                    pm = GetPaymentMethodGroupReceipt(receipt.GroupReceiptPaymentMethodsWithPipe);
                }
                else
                {
                    pm = GetPaymentMethod(pp, paymentMethods);
                }
            }
            else
            {
                pm = GetPaymentMethod(pp, paymentMethods);
            }

            //// DCR GroupInvoiceText 2021-OCT-25 Uthen.P
            if (receipt.GroupInvoiceDescriptionText != null)
            {
                string line1 = null;
                string line2 = null;
                string line3 = null;
                string line4 = null;
                line1        = receipt.GroupInvoiceDescriptionText;                            
                line2        = "ชำระตามหนังสือแจ้งเลขที่";
                line3        = receipt.PrintingInvoices[0].DisplayInvoiceNo;
                if (vatAmount > 0)
                {
                    line4 = "รายละเอียดตามใบแนบใบกำกับภาษี";
                }
                else
                {
                    line4 = "รายละเอียดตามใบแนบใบเสร็จรับเงิน";
                }

                foreach (var d in ds)
                {
                    d.ProductDesc = string.Format("{0}\n{1}\n{2}\n{3}",line1,line2,line3,line4);
                }
            }

            Preview p = new Preview();
            /*Preview receipt form*/
            //p.SetDatasource(hs, ds, pm);
            //p.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //p.ShowDialog();

            /*Print receipt form through printer*/
            p.SetDatasource(hs, ds, pm);
            p.PrintReport();
        }

        private string CutoffExceedLengthOfAddressElectricReceipt(int length, string prm)
        {
            string Address          = prm;
            int addressLength       = StringConvert.TextLength(Address);
            if (addressLength >= length)
            {
                string postCode     = "";                
                string ampurText    = "";
                string provinceText = "";

                int spaceIndex      = Address.Trim().LastIndexOfAny(new char[] { ' ' });
                postCode            = Address.Substring(spaceIndex, Address.Length - spaceIndex).Trim();
                Address             = Address.Replace(postCode, String.Empty).Trim();

                if (StringConvert.TextLength(Address) >= length)       //เช็ค Length หลังจากตัดรหัสไปรณีย์ แล้วตัวอักษรยังเกิน (1)
                {
                    //2nd
                    spaceIndex      = Address.Trim().LastIndexOfAny(new char[] { ' ' });
                    provinceText    = Address.Substring(spaceIndex, Address.Length - spaceIndex);
                    Address         = Address.Replace(provinceText, String.Empty).Trim();

                    if (StringConvert.TextLength(Address) >= length)   //เช็ค Length หลังจากตัดชื่อจังหวัด แล้วตัวอักษรยังเกิน (2)
                    {
                        spaceIndex  = Address.Trim().LastIndexOfAny(new char[] { ' ' });
                        ampurText   = Address.Substring(spaceIndex, Address.Length - spaceIndex);
                        Address     = Address.Replace(ampurText, String.Empty).Trim();
                        ampurText   = "\n" + ampurText.Trim() + " " + provinceText.Trim() + " " + postCode;
                        Address     = Address + ampurText;
                    }
                    else
                    {
                        try
                        {
                            Address = Address.Replace(provinceText, String.Empty);
                            provinceText = "\n" + provinceText.Trim() + " " + postCode;
                            Address = Address + " " + provinceText;
                        }
                        catch
                        {
                            Address = prm;
                        }
                    }
                }
                else
                {
                    postCode        = "\n" + postCode;
                    Address         = Address + " " + postCode;
                }
            }           
            return Address;
        }


        private List<ReceiptPaymentMethod> GetPaymentMethodGroupReceipt(string paymentMethodWithPipe)
        {
            List<ReceiptPaymentMethod> rp = new List<ReceiptPaymentMethod>();
            string[] subs = paymentMethodWithPipe.Split('|');
            foreach (var txt in subs)
            {
                ReceiptPaymentMethod r = new ReceiptPaymentMethod();
                r.PaymentDetail = txt;
                rp.Add(r);
            }           
            return rp;
        }


        private List<ReceiptPaymentMethod> GetPaymentMethod(List<InvoicePaymentMethod> paymentMethods, List<PaymentMethod> pms)
        {
            List<ReceiptPaymentMethod> rp = new List<ReceiptPaymentMethod>();
            foreach (InvoicePaymentMethod p in paymentMethods)
            {
                ReceiptPaymentMethod r = new ReceiptPaymentMethod();
                PaymentMethod pm = p.GetPaymentMethod(pms);
                switch (pm.PtId)
                {
                    case CodeNames.PaymentType.Cash.Id:
                        r.PaymentDetail = StringConvert.FormatText("โดยเงินสด " + ((paymentMethods.Count > 1) ? string.Format("{0:#,###.00}", p.Amount) + " บาท" : ""), 35);
                        break;
                    case CodeNames.PaymentType.Cheque.Id:
                        r.PaymentDetail = StringConvert.FormatText(string.Format("โดยเช็ค {0} เลขที่ {1} วันที่ {2} จำนวนเงิน {3} บาท",
                            pm.BankName,
                            pm.ChqNo,
                            pm.ChqDt.Value.ToString("d/M/yyyy", ci),
                            string.Format("{0:#,###.00}", pm.ToPayAmount)), 35);
                        break;
                    case CodeNames.PaymentType.Deposit.Id:
                        r.PaymentDetail = StringConvert.FormatText(string.Format("โดยใบนำฝากเข้าบัญชี {0} เลขที่ {1} จำนวนเงิน {2} บาท",
                            pm.BankName,
                            pm.DepositAccNo,
                            string.Format("{0:#,###.00}", pm.ToPayAmount)), 35);
                        break;
                    case CodeNames.PaymentType.QRPayment.Id : 
                        // DCR : QR Payment
                        r.PaymentDetail = StringConvert.FormatText("โดย QR Payment " + ((paymentMethods.Count > 1) ? string.Format("{0:#,###.00}", p.Amount) + " บาท" : ""), 35);
                        break;
                    default:
                        r.PaymentDetail = "";
                        break;
                }
                rp.Add(r);
            }
            return rp;
        }

        //private string GetAccountNo(string DtId)
        //{
        //    List<DebtType> debtType = new List<DebtType>(CodeTable.Instant.ListDebtTypes());
        //    foreach (DebtType d in debtType)
        //    {
        //        if (d.DebtId == DtId)
        //        {
        //            return d.AccountNo;
        //        }
        //    }
        //    return "";
        //}

        internal void SetDatasource(List<Header> hs, List<Detail> ds, List<ReceiptPaymentMethod> pm)
        {
            this._hs = hs;
            this._ds = ds;
            this._pm = pm;
        }

    }
}
