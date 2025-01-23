using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Threading;
using PEA.BPM.PaymentCollectionModule.DA;
using System.Data.Common;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.BS
{
    public class PaidBillBS: IPaidBillService
    {
        #region IPaidBillService Members

        public List<Receipt> SearchPaidBill(PaidBillSearchParam param)
        {
            PaidBillDA da = new PaidBillDA();

            return da.SearchPaidBill(param);
        }

        public List<Receipt> SearchReceipt(ReceiptSearchParam param)
        {
            PaidBillDA da = new PaidBillDA();

            return da.SearchReceipt(param);
        }

        public ReceiptDetail GetReceiptDetail(string receiptId)
        {
            PaidBillDA da = new PaidBillDA();

            return da.GetReceiptDetail(receiptId);
        }

        public CancelledInfo CancelReceipt(DbTransaction trans, List<string> receiptIds, string reason, string reprintReceiptId, 
            string newReceiptId, string posId, string terminalcode, string branchId, string branchName, string cashierId,
            string cashierName, string workId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            #region รวมแผนผ่อน
            string newReceiptIdWithGroupReceipt = string.Empty;
            string[] obj = newReceiptId.Split('|');
            if (obj.Length > 1 && obj[1].StartsWith("X"))
            {
                newReceiptIdWithGroupReceipt = newReceiptId;
                newReceiptId = obj[0];  // ReAssign New receipt เพื่อส่งให้กับขุั้นตอน pc_upd_repayLastReceipt. 
            }
            else
            {
                newReceiptIdWithGroupReceipt = newReceiptId;
            }

            #endregion


            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    PaidBillDA da = new PaidBillDA();

                    CancelledInfo cancelledInfo = new CancelledInfo();

                    //string ids = String.Join("|", receiptIds.ToArray());
                    //if (reprintReceiptId != null)
                    //{
                    //    ids += string.Format("|{0}", reprintReceiptId);
                    //}

                    string postedServerId = Session.Branch.PostedServerId;

                    string cancelPaymentId = da.CancelReceipt(trans, receiptIds, reason, posId, terminalcode, branchId, branchName, cashierId, cashierName, workId, postedServerId, reprintReceiptId);

                    if (reprintReceiptId != null)
                    {
                        da.RepayLastReceipt(trans, reprintReceiptId, cancelPaymentId, branchId, postedServerId);
                        //da.SaveReprintReceipt(trans, reprintReceiptId, newReceiptId, Session.Branch.PostedServerId);

                        // DCR AUG 2021 รวมแผนผ่อน Repay เป็นกลุ่ม Receipt แผนผ่อน ต้องส่ง Receipt ที่ขึ้นต้นด้วย X ไปด้วย.
                        da.SaveReprintReceipt(trans, reprintReceiptId, newReceiptIdWithGroupReceipt, Session.Branch.PostedServerId);
                        cancelledInfo.PrintingInfo = da.GetReceiptforPrint(trans, newReceiptId);
                    }

                    cancelledInfo.PaidMethods = da.GetReturnPayment(trans, cancelPaymentId);
                    

                    trans.Commit();

                    //try
                    //{
                        
                    //    BillingBS bBS = new BillingBS();
                    //    BillingDA bDA = new BillingDA();

                    //    List<string> cancelreceiptIds = new List<string>(); ;

                    //    foreach (string r in receiptIds)
                    //    {
                    //        if (r != reprintReceiptId)
                    //        {
                    //            cancelreceiptIds.Add(r);
                    //        }
                    //    }

                    //    List<OneTouchLogInfo> OneTouchLogInfo = da.SearchOneTouchPayment(cancelreceiptIds);

                    //    foreach (OneTouchLogInfo r in OneTouchLogInfo)
                    //    {
                    //        OneTouchLogInfo OneTouchLog = new OneTouchLogInfo();
                    //        OneTouchLog.NotificationNo = r.NotificationNo;
                    //        OneTouchLog.InvoiceNo = r.InvoiceNo;
                    //        OneTouchLog.DebtId = r.DebtId.Substring(1,8); //เพิ่ม DebTyptId
                    //        OneTouchLog.ReceiptId = r.ReceiptId;
                    //        OneTouchLog.GAmount = r.GAmount;
                    //        OneTouchLog.VatAmount = r.VatAmount;
                    //        OneTouchLog.ModifiedBy = (Session.User.Id);
                    //        OneTouchLog.Action = "3"; //Cancel

                    //        //Call Web Service OneTouch
                    //        bool flag = bBS.FlagOneTouchPayment(OneTouchLog);

                    //        if (flag == false)
                    //        {
                    //            OneTouchLog.SyncFlag = "1";
                    //        }
                    //        else
                    //        {
                    //            OneTouchLog.SyncFlag = "0";  //ส่งได้
                    //        }

                    //        //Insert Log OneTouch
                    //        try
                    //        {
                    //            bDA.SaveOneTouchLog(OneTouchLog);
                    //        }
                    //        catch
                    //        {
                    //            //
                    //        }
                    //    }
                    //}
                    //catch 
                    //{
                    //    //Nothing
                    //}

                    //--if defType = 'M0080'
                    if (cancelledInfo.PrintingInfo != null)
                    {
                        cancelledInfo.PrintingInfo.PrintingReceipt = CreateReceiptForReprint(cancelledInfo.PrintingInfo)[0];
                    }

                    return cancelledInfo;
                    
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.POS, "BS.CancelReceipt", ex.ToString());
                    throw;
                }
            }
        }

        public List<PrintingInfo> GetReceiptsForPrint(DbTransaction trans, List<string> receiptIds)
        {
            PaidBillDA da = new PaidBillDA();

            List<PrintingInfo> pinfo = new List<PrintingInfo>();

            foreach (string receiptId in receiptIds)
            {
                PrintingInfo p                = da.GetReceiptforPrint(null, receiptId);
                //// รวมใบเสร็จแผนผ่อน 2021-09-28
                ////PrintingInfo tmp_GroupReceipt = da.GetReceiptforPrint(null, receiptId);

                ValidateAbnormalReceipt(p); // ของเดิม

                //#region เพิ่มใหม่ รวมใบเสร็จแผนผ่อน 2021-09-28 ยกเลิกใบเสร็จ PaidBillBS 
                //if (tmp_GroupReceipt.PrintingReceipt.GroupExtReceiptIdMapping.Count > 0)
                //{
                //    p.PrintingReceipt.GroupExtReceiptIdMapping = new List<ExtReceiptIdMapping>();
                //    foreach (var t in tmp_GroupReceipt.PrintingReceipt.GroupExtReceiptIdMapping)
                //    {
                //        if (t.ExtReceiptId == p.PrintingReceipt.ReceiptId)
                //        {
                //            var tmp = new ExtReceiptIdMapping();
                //            tmp.ExtReceiptId    = t.ExtReceiptId;
                //            tmp.ReceiptId       = t.ReceiptId;
                //            p.PrintingReceipt.GroupExtReceiptIdMapping.Add(tmp);
                //        }
                //    }
                //}
                //#endregion

                pinfo.Add(p);
            }

            return pinfo;
        }

        public void IncreaseNoOfReprint(string receiptId)
        {            
            // check biz rules
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    PaidBillDA da = new PaidBillDA();
                    da.IncreaseNoOfReprint(trans, receiptId, Session.Branch.PostedServerId);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Logger.WriteError(Logger.Module.POS, "BS.IncreaseNoOfReprint", ex.ToString());
                    throw;
                }
            }
        }

        #endregion

        private void ValidateAbnormalReceipt(PrintingInfo p)
        {
            PrintingInvoice iv = p.PrintingReceipt.PrintingInvoices[0];

            switch (iv.Bills[0].DebtId.Substring(0, 5))
            {
                case "M0080": // รายการผ่อนชำระ
                    
                    BillingDA billingDa = new BillingDA();
                    Invoice ivo = billingDa.SearchOriginalInvoiceByInstallmentItemCaDoc(iv.CaDoc);
                    ivo.Name = iv.Name;
                    ivo.Address = iv.Address;

                    //Tax13 
                    ivo.CaTaxId = iv.CaTaxId;
                    ivo.CaTaxBranch = iv.CaTaxBranch;

                    ivo.ToPayGAmount = iv.ToPayGAmount;
                    ivo.ToPayAdjAmount = iv.ToPayAdjAmount;
                    ivo.UiRefId = iv.UiRefId;
                    ivo.OriginalInvoiceNo = ivo.InvoiceNo;
                    ivo.InvoiceNo = iv.InvoiceNo;
                    ivo.OriginalInvoiceDt = ivo.InvoiceDate;
                    ivo.InvoiceDate = iv.InvoiceDate;

                    if (iv.InstallmentTotalPeriod != null && iv.InstallmentTotalPeriod > 0)
                    {
                        ivo.InstallmentPeriod = iv.InstallmentPeriod;
                        ivo.InstallmentTotalPeriod = iv.InstallmentTotalPeriod;
                        iv.Bills[0].DebtId = ivo.Bills[0].DebtId;
                        iv.Bills[0].DebtType = ivo.Bills[0].DebtType;
                        ivo.Bills[0].GAmount = iv.Bills[0].GAmount;
                        ivo.Bills[0].FullBaseAmount = iv.Bills[0].FullBaseAmount;
                        ivo.Bills[0].FullFtAmount = iv.Bills[0].FullFtAmount;
                        ivo.Bills[0].FullQty = iv.Bills[0].FullQty;
                        ivo.Bills[0].FullAmount = iv.Bills[0].FullAmount;
                        ivo.Bills[0].FullVatAmount = iv.Bills[0].FullVatAmount;
                        ivo.Bills[0].FullGAmount = iv.Bills[0].FullGAmount;
                        ivo.PartialPayment = iv.PartialPayment;
                    }

                    Decimal? exVatAmount = ivo.AmountExVat;
                    Decimal? gAmount = ivo.GAmount;
                    Decimal? vAmount = ivo.VatAmount;
                    string taxCode = ivo.Bills[0].TaxCode;
                    Decimal? taxRate = ivo.Bills[0].TaxRate;
                    Decimal? qty = ivo.Qty;


                    decimal? toPayVatAmount;
                    decimal? toPayExVatAmount;


                    // Calculate Vat
                    if (iv.ToPayVatAmount != null)
                    {
                        toPayVatAmount = iv.ToPayVatAmount;
                    }
                    else
                    {
                        if (null != vAmount && vAmount > 0 && ivo.Bills[0].DebtId != "M00100100")
                        {
                            toPayExVatAmount = ivo.ToPayGAmount / ((100 + taxRate) / 100);
                            toPayExVatAmount = Decimal.Round(toPayExVatAmount.Value, 2);
                            toPayVatAmount = ivo.ToPayGAmount - toPayExVatAmount;
                        }
                        else
                        {
                            toPayVatAmount = 0;
                        }
                    }
 
                    ivo.ToPayVatAmount = toPayVatAmount;

                    // Calculate Qty
                    if (iv.ToPayVatAmount != null)
                    {
                        ivo.ToPayQty = iv.ToPayQty;
                    }
                    else
                    {
                        if (null != qty && qty != 0)
                        {
                            ivo.ToPayQty = ivo.ToPayExVatAmount / (exVatAmount / qty);
                            ivo.ToPayQty = Decimal.Round(ivo.ToPayQty.Value, 0);
                        }
                    }


                    ivo.PaymentMethods = iv.PaymentMethods;
                    PrintingInvoice piv = new PrintingInvoice(ivo, null);
                    p.PrintingReceipt.PrintingInvoices.Clear();
                    p.PrintingReceipt.PrintingInvoices.Add(piv);

                    break;
                default:
                    break;
            }
        }

        public List<PrintingReceipt> CreateReceiptForReprint(PrintingInfo pinfo)
        {
            bool isChange = false;
            // แยกใบเสร็จตาม ลูกค้า, ชนิดฟอร์ม (Slip ใบเสร็จรับเงิน, ใบเสร็จรับเงินทั่วไป),  ชนิดใบเสร็จ (ใบเสร็จ, ใบเสร็จ/ใบกำกับภาษี, ใบรับเงิน) และใบเสร็จที่ต้องพิมพ์แยก
            Dictionary<string, PrintingReceipt> receipts = new Dictionary<string, PrintingReceipt>();

            BillingBS _billingBS = new BillingBS();
            List<PrintingInvoice> printingInvoices = pinfo.PrintingReceipt.PrintingInvoices;

            for (int k = 0; k < printingInvoices.Count; k++)
            {
                PrintingInvoice iv = printingInvoices[k];
                string debtType = iv.Bills[0].DebtId;

                // Installment
                if (debtType.Substring(0, 5) == "M0080")
                {
                    isChange = true;
                    // Get original invoice
                    OriginalInvoiceSearchParam param = new OriginalInvoiceSearchParam(iv.CaDoc);
                    param.IsOtherBranch = (iv.NetworkMode == NetworkMode.OnlineToBpmServer);
                    Invoice ivo = _billingBS.SearchOriginalInvoiceByInstallmentItemCaDoc(param);

                    //ivo.InvoiceNo = iv.InvoiceNo; //add

                    ivo.Name = iv.Name;
                    ivo.Address = iv.Address;

                    //Tax13
                    ivo.CaTaxId = iv.CaTaxId;
                    ivo.CaTaxBranch = iv.CaTaxBranch;

                    ivo.ARPmId = iv.ARPmId;
                    ivo.ToPayGAmount = iv.ToPayGAmount;
                    ivo.ToPayAdjAmount = iv.ToPayAdjAmount;
                    ivo.PaymentId = iv.PaymentId;
                    ivo.UiRefId = iv.UiRefId;


                    Decimal? exVatAmount = ivo.AmountExVat;
                    Decimal? gAmount = ivo.GAmount;
                    Decimal? vAmount = ivo.VatAmount;
                    string taxCode = ivo.Bills[0].TaxCode;
                    Decimal? taxRate = ivo.Bills[0].TaxRate;
                    Decimal? qty = ivo.Qty;

                    // TODO: Find pevious receipt values to calculate last receipt value
                    // Last Period
                    if (iv.InstallmentPeriod == iv.InstallmentTotalPeriod)//last Installment Receipt
                    {
                        if (null != taxRate || null != qty)
                        {
                            List<InstallmentInvoice> iivs = _billingBS.SearchInstallmentInvoice(iv.CaDoc);

                            decimal? totalVatAmount = 0;
                            decimal? totalQty = 0;

                            for (int j = 0; j < iivs.Count; j++)
                            {
                                InstallmentInvoice iiv = iivs[j];
                                if (iiv.InstallmentPeriod != iiv.InstallmentTotalPeriod)
                                {
                                    decimal? toPayExVatAmount = iiv.GAmount;

                                    if (null != taxRate)
                                    {
                                        decimal? toPayVatAmount;

                                        if (taxCode.StartsWith("O"))
                                        {
                                            if (iiv.InstallmentPeriod == 1)
                                            {
                                                toPayVatAmount = vAmount;
                                            }
                                            else
                                            {
                                                toPayVatAmount = 0;
                                            }

                                            toPayExVatAmount = iiv.GAmount - toPayVatAmount;
                                        }
                                        else
                                        {
                                            toPayExVatAmount = iiv.GAmount / ((100 + taxRate) / 100);
                                            toPayExVatAmount = Decimal.Round(toPayExVatAmount.Value, 2);
                                            toPayVatAmount = iiv.GAmount - toPayExVatAmount;
                                        }

                                        totalVatAmount += toPayVatAmount;
                                    }

                                    if (null != qty && qty != 0)
                                    {
                                        decimal? tQty;
                                        decimal? unitPrice = exVatAmount / qty;

                                        tQty = toPayExVatAmount / unitPrice;
                                        tQty = Decimal.Round(tQty.Value, 0);

                                        totalQty += tQty;
                                    }
                                }
                            }

                            if (null != taxRate)
                            {
                                ivo.ToPayVatAmount = ivo.VatAmount - totalVatAmount;
                            }

                            if (null != qty && qty != 0)
                            {
                                ivo.ToPayQty = qty - totalQty;
                            }
                        }

                        ivo.PaidGAmount = ivo.GAmount - iv.ToBePaidGAmount;
                    }
                    else
                    {
                        decimal? toPayVatAmount;

                        // Calculate Vat
                        if (null != taxRate)
                        {
                            if (taxCode.StartsWith("O")) // ถ้าเริ่มด้วย O เช่น O7 รายการแรกเท่านั้นที่มี Vat
                            {
                                if (iv.InstallmentPeriod == 1)
                                {
                                    toPayVatAmount = vAmount;
                                }
                                else
                                {
                                    toPayVatAmount = 0;
                                }
                            }
                            else // ถ้า มี vat แต่ไม่ใช่ "O" ให้ ใส่ vat ทุกรายการ
                            {
                                decimal? toPayExVatAmount = ivo.ToPayGAmount / ((100 + taxRate) / 100);
                                toPayExVatAmount = Decimal.Round(toPayExVatAmount.Value, 2);
                                toPayVatAmount = ivo.ToPayGAmount - toPayExVatAmount;
                            }
                        }
                        else
                        {
                            toPayVatAmount = 0;
                        }

                        ivo.ToPayVatAmount = toPayVatAmount;

                        // Calculate Qty
                        if (null != qty && qty != 0)
                        {
                            ivo.ToPayQty = ivo.ToPayExVatAmount / (exVatAmount / qty);
                            ivo.ToPayQty = Decimal.Round(ivo.ToPayQty.Value, 0);
                        }
                    }

                    PrintingConstraint pcst = new PrintingConstraint();
                    pcst.DefaultPaperSize = PrintingConstraint.PaperSize.PosSlip;

                    ivo.PaymentMethods = iv.PaymentMethods;
                    PrintingInvoice piv = new PrintingInvoice(ivo, pcst);
                    //PrintingReceipt receipt = GetReceipt(totalAmount, paidAmount, changeAmount, paymentDate, piv, isExtReceipt);
                    //string key = string.Format("{0}{1}{2}", piv.CaId, piv.PrintingConstaint.DefaultPaperSize, piv.GetHashCode());
                    //string key = string.Format("{1}{2}", piv.PrintingConstaint.DefaultPaperSize, piv.GetHashCode());
                    //receipts.Add(key, receipt);

                    pinfo.PrintingReceipt.PaidAmount = pinfo.PrintingReceipt.TotalAmount + pinfo.PrintingReceipt.AdjChangeAmount;
                    PrintingReceipt receipt = GetReceipt(pinfo.PrintingReceipt, piv); 
                    string key = string.Format("{0}{1}{2}", piv.CaId, piv.PrintingConstaint.DefaultPaperSize, piv.GetHashCode());
                    receipts.Add(key, receipt);

                }
            }

            if (isChange)
            {

                List<PrintingReceipt> rs = new List<PrintingReceipt>();
                foreach (string key in receipts.Keys)
                {
                    rs.Add(receipts[key]);
                }
                //rs.Sort();

                // Set receipt printing sequence
                short totalReceipt = (short)rs.Count;
                //for (short i = 1; i <= totalReceipt; i++)
                //{
                //    rs[i - 1].PrintingSequence = i;
                //    rs[i - 1].TotalReceipt = totalReceipt;
                //}

                return rs;
            }
            else
            {
                //Normal Receipt 
                //pinfo.PrintingReceipt.PaidAmount = pinfo.PrintingReceipt.TotalAmount + pinfo.PrintingReceipt.AdjChangeAmount;
                //pinfo.PrintingReceipt.ChangeAmount = 0;
                List<PrintingReceipt> rs = new List<PrintingReceipt>();
                rs.Add(pinfo.PrintingReceipt);
                return rs;
            }

        }

        private PrintingReceipt GetReceipt(PrintingReceipt prtR, PrintingInvoice iv)
        {
            PrintingReceipt receipt;
            string receiptId = prtR.ReceiptId;
            receipt = new PrintingReceipt(
                receiptId, iv.CaId, iv.ReceiptPrintName, iv.CaTaxId, iv.CaTaxBranch ,iv.Address, iv.Bills[0].ContractTypeId,
                prtR.TotalAmount, prtR.PaidAmount, prtR.ChangeAmount, iv.ToPayAdjAmount.Value, prtR.PaymentDate,
                prtR.CashierId, prtR.CashierName );

            receipt.PrintingInvoices.Add(iv);

            receipt.AdjChangeAmount = prtR.AdjChangeAmount;
            receipt.CustomerId = prtR.CustomerId;
            receipt.CustomerName = prtR.CustomerName;
            //receipt.CaTaxId = prtR.CaTaxId;
            //receipt.CaTaxBranch = prtR.CaTaxBranch;
            receipt.CustomerAddress = prtR.CustomerAddress;
            receipt.PrintingSequence = prtR.PrintingSequence;
            receipt.TotalReceipt = prtR.TotalReceipt;
            receipt.BranchName = prtR.BranchName;
            receipt.BranchAddress = prtR.BranchAddress;
            receipt.BranchNumber = prtR.BranchNumber;
            receipt.TerminalCode = prtR.TerminalCode;

            return receipt;
        }

        private PrintingReceipt GetReceipt(decimal totalAmount, decimal paidAmount, decimal changeAmount,
            DateTime paymentDate, PrintingInvoice iv, bool isExtReceipt)
        {
            PrintingReceipt receipt;
            //string receiptId = Running.GetReceiptId(
            //    iv.PrintingConstaint.DefaultPaperSize,
            //    iv.Bills[0].TaxCode == null || iv.Bills[0].TaxCode[0] == 'O' || iv.Bills[0].TaxRate == null ? CodeNames.ReceiptType.NonTax : CodeNames.ReceiptType.Tax,
            //    iv.Bills[0].DebtId, isExtReceipt);
            string receiptId = "00";
            receipt = new PrintingReceipt(
                receiptId, iv.CaId, iv.ReceiptPrintName, iv.CaTaxId , iv.CaTaxBranch, iv.Address, iv.Bills[0].ContractTypeId,
                totalAmount, paidAmount, changeAmount, iv.ToPayAdjAmount.Value, paymentDate,
                Session.User.Id, Session.User.Name);
            receipt.PrintingInvoices.Add(iv);

            receipt.BranchName = Session.Branch.Name;
            receipt.BranchAddress = Session.Branch.Address;
            receipt.BranchNumber = Session.Branch.Number;
            receipt.TerminalCode = Session.Terminal.Code;

            return receipt;
        }


        public List<OneTouchLogInfo> SearchOneTouchPayment(List<string> receiptIds)
        {
            PaidBillDA da = new PaidBillDA();
            return da.SearchOneTouchPayment(receiptIds);
        }

        public List<string> SearchPaymentTypeQR(List<string> paymentIds)
        {
            PaidBillDA da = new PaidBillDA();
            return da.SearchPaymentTypeQR(paymentIds);
        }

    }
}
