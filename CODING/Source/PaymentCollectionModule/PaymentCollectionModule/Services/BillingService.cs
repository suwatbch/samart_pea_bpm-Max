using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Threading;
using System.Collections;
using System.Data;
using PaymentCollectionModule.SG;
using System.Configuration;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Services
{
    [Service(typeof(IBillingService))]
    public class BillingService : IBillingService
    {
        public BillingService()
		{

        }

        #region Service Factory
        public IBillingService GetService()
        {
            return GetService(false);
        }

        public IBillingService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new BillingSG(true);
            }
            else
            {
                return new BillingSG(false);
            }
        }
        #endregion

        #region IBillingService Members

        //---Real Time POS--Start------------------------------------------------------------------------------------------
        public List<Invoice> SearchInvoiceByCustomerId(CustomerSearchParam param)
        {

            if (!Session.Branch.OnlineConnection )
            {
                //RealTime POS: Get Online Data -> Branch Data
                //GetData=Center, SaveData=Register
                if( param.IsOtherBranch )
                    RealTimeSyncOfPaymentForOtherBranch(param.CaId);
                else if (!IsArOwnerBranch(param.CaId))
                    RealTimeSyncOfPaymentForOtherBranch(param.CaId);
            }

            //GetData=Register
            return GetService().SearchInvoiceByCustomerId(param);
        }

        public List<Invoice> SearchInvoiceFromSAP(SAPSearchParam param)
        {
            //RealTime POS: Get RFC Data -> Online Data -> Branch Data
            //GetData=RFC->Center, SaveData=Register
            RealTimeSyncOfPaymentForRFC(param.CaId);

            return GetService().SearchInvoiceByCustomerId(new CustomerSearchParam(param.CaId));
        }

        //---Real Time POS--End------------------------------------------------------------------------------------------

        public Invoice SearchOriginalInvoiceByInstallmentItemCaDoc(OriginalInvoiceSearchParam param)
        {
            IBillingService bs;

            if (param.IsOtherBranch)
            {
                bs = GetService(true);
            }
            else
            {
                bs = GetService();
            }

            return bs.SearchOriginalInvoiceByInstallmentItemCaDoc(param);            
        }

        public List<InstallmentInvoice> SearchInstallmentInvoice(string caDoc)
        {
            return GetService().SearchInstallmentInvoice(caDoc);
        }

        public List<Invoice> SearchInvoiceByGroupInvoiceNo(GroupInvoiceSearchParam param)
        {
            return GetService().SearchInvoiceByGroupInvoiceNo(param);
        }

        public List<Invoice> SearchInvoiceItemByGroupInvoiceNo(InvoiceItemSearchParam param)
        {
            IBillingService bs;

            if (param.IsOtherBranch)
            {
                bs = GetService(true);
            }
            else
            {
                bs = GetService();
            }

            return bs.SearchInvoiceItemByGroupInvoiceNo(param);
        }

        public List<BillSearchDetail> SearchBillByCustomerDetail(CustomerSearchParam param)
        {
            IBillingService bs;

            if (param.IsOtherBranch)
            {
                bs = GetService(true);
            }
            else
            {
                bs = GetService();
            }

            return bs.SearchBillByCustomerDetail(param);
        }

        public List<Bill> SearchBillByBillBookId(string billBookId, string billBookStatus)
        {
            return GetService().SearchBillByBillBookId(billBookId, billBookStatus);
        }

        public List<BookSearchDetail> SearchBillByAgent(AgencySearchParam param)
        {
            return GetService().SearchBillByAgent(param);
        }

        public List<PaymentMethod> SearchAGPayment(string billBookId)
        {
            return GetService().SearchAGPayment(billBookId);
        }

        public List<GroupInvoiceItem> GetGroupInvoiceItem(string billBookId)
        {
            return GetService().GetGroupInvoiceItem(billBookId);
        }

        public CaIdAndBranchId GetBranchIdByCaId(String caId)
        {
            return GetService().GetBranchIdByCaId(caId);
        }

        public Customer GetCustomerDetailOnPaymentHistory(HistoryViewParam param)
        {
            IBillingService bs;

            //if not direct connect, update bp and ca first
            if (!Session.Branch.OnlineConnection)
            {
                List<CaAndBpInfo> caBpList = GetService(true).GetCaAndBpOtherBranch(param.CaId);
                caBpList[0].CaObj.ModifiedBy = Session.User.Id;
                caBpList[0].BpObj.ModifiedBy = Session.User.Id;
                GetService().SaveCaAndBpOtherBranch(caBpList);
            }

            if (param.IsOtherBranch)
            {
                bs = GetService(true);
            }
            else
            {
                bs = GetService();
            }

            return bs.GetCustomerDetailOnPaymentHistory(param);
        }

        public List<PaidInvoice> GetPaymentHistory(HistoryViewParam param)
        {
            IBillingService bs;
            if (param.IsOtherBranch)
            {
                bs = GetService(true);
            }
            else
            {
                bs = GetService();
            }

            return bs.GetPaymentHistory(param);
        }

        public ResultPayInvoice PayInvoice(DbTransaction trans, List<Invoice> paidInvoices, List<PaymentMethod> paymentMethods,
            List<PrintingReceipt> receipts, List<PrintingReceipt> groupDividualReceipts, ExternalReceipt extReceipt,
            DateTime paymentDate, string posId, string terminalCode, string branchId, string branchName, decimal? payingAmount,
            string screenType, string cashierId, string cashierName, string workId, bool isOffline)
        {
            //clone ca&bp to branch server first
            //offline and not direct connection
            if (isOffline && !Session.Branch.OnlineConnection) 
            {
                Hashtable caHt = new Hashtable();
                foreach (Invoice inv in paidInvoices)
                {
                    if (!caHt.Contains(inv.CaId)) //no dumplicated sync
                    {
                        List<CaAndBpInfo> caBpList = GetService(true).GetCaAndBpOtherBranch(inv.CaId);
                        caBpList[0].CaObj.ModifiedBy = Session.User.Id;
                        caBpList[0].BpObj.ModifiedBy = Session.User.Id;
                        GetService().SaveCaAndBpOtherBranch(caBpList);
                        caHt.Add(inv.CaId, null);
                    }
                }
            }

            IBillingService billingService;
            billingService = GetService();

            return billingService.PayInvoice(trans, paidInvoices, paymentMethods, receipts, groupDividualReceipts, extReceipt,
                paymentDate, posId, terminalCode, branchId, branchName, payingAmount, screenType, cashierId, cashierName, workId, isOffline);
        }

        public List<AdvancePaymentHistory> SearchAdvancePaymentHistory(string billBookId)
        {
            return GetService().SearchAdvancePaymentHistory(billBookId);
        }

        public List<BillBook> SearchBillBookByDetail(string billBookId, string agencyId, string agencyName)
        {
            return GetService().SearchBillBookByDetail(billBookId, agencyId, agencyName);
        }

        public BillBook GetBillBookDetail(string billBookId, string statusLineStr)
        {
            return GetService().GetBillBookDetail(billBookId, statusLineStr);
        }

        //public void SaveReceipt(DbTransaction trans, List<PrintingReceipt> receipts, ExternalReceipt extReceipt)
        //{
        //    IBillingService billingService;

        //    if (Session.Branch.OnlineConnection)
        //    {
        //        billingService = GetService(true);
        //    }
        //    else
        //    {
        //        // check network mode. If at least 1 invoice from center server, it will be online payment
        //        PrintingReceipt receipt = receipts.Find(delegate(PrintingReceipt receiptx)
        //            {
        //                return receiptx.PrintingInvoices[0].NetworkMode == NetworkMode.OnlineToBpmServer;
        //            }
        //        );

        //        if (receipt != null)
        //        {
        //            billingService = GetService(true);
        //        }
        //        else
        //        {
        //            billingService = GetService(false);
        //        }
        //    }

        //    billingService.SaveReceipt(trans, receipts, extReceipt);
        //}


        public DayClosingResult CloseDayPayment(string branchId)
        {
            return GetService(true).CloseDayPayment(branchId);            
        }

        public List<DisconnectionDoc> SearchDisconnectionStatusByDiscNo(string discNo)
        {
            return GetService().SearchDisconnectionStatusByDiscNo(discNo);
        }

        public bool CheckExistingInvoiceNo(string caId, string period)
        {
            return GetService().CheckExistingInvoiceNo(caId, period);
        }

        public DateTime GetServerTime()
        {
            return GetService().GetServerTime();
        }

        public LastDisconnect GetLastDisconnect(string caId)
        {
            return GetService().GetLastDisconnect(caId);
        }


        public List<PaymentNonReceiptInfo> SearchPaymentNonReceipt(PaymentNonReceiptInfo receiptGen)
        {
            return GetService().SearchPaymentNonReceipt(receiptGen);
        }

        public void CreateReceiptService(List<PaymentNonReceiptInfo> paymentGenReList)
        {
            GetService().CreateReceiptService(paymentGenReList);
        }

        public List<CaAndBpInfo> GetCaAndBpOtherBranch(string caId)
        {
            return GetService(true).GetCaAndBpOtherBranch(caId);
        }       

        public void SaveCaAndBpOtherBranch(List<CaAndBpInfo> list)
        {
            //save to branch only
            GetService().SaveCaAndBpOtherBranch(list);
        }

        public bool GetPaidGAmount(string invoiceNo)
        {
            return GetService().GetPaidGAmount(invoiceNo);
        }

        public bool GetInActiveBillBook(string invoiceNo)
        {
            return GetService().GetInActiveBillBook(invoiceNo);
        }

        public bool GetDuplicateExtReceipt(string receiptId, string branchId)
        {
            return GetService().GetDuplicateExtReceipt(receiptId, branchId);
        }

        public bool ValidatePaymentActive(string receiptId, bool isPayment)
        {
            return GetService().ValidatePaymentActive(receiptId, isPayment);
        }

        public void OfflineLog(OfflineLogInfo log)
        {
            //direct to center !
            GetService(true).OfflineLog(log);
        }
        /// <summary>
        /// Search Invoice By Param and Source
        ///     
        /// </summary>
        /// <param name="param">Invoice Detail</param>
        /// <param name="renew">true=RFC false=Center</param>
        /// <returns></returns>
        public ActivePayment GetActivePayment(SAPSearchParam param, bool renew)
        {
            //direct to center !
            //param.caId, renew(true=RFC, false=Center or OtherBranch)
            return GetService(true).GetActivePayment(param, renew);
        }

        /// <summary>
        /// Search Invoice By Param and Source
        ///     
        /// </summary>
        /// <param name="param">Invoice Detail</param>
        /// <param name="renew">true=RFC false=Center</param>
        /// <returns></returns>
        public ActivePayment GetActivePayment(SAPSearchParam param, SearchType searchType)
        {
            //direct to center !
            bool isRFC = false;

            switch (searchType)
            {
                case SearchType.RFC:
                    isRFC = true;
                    break;
                case SearchType.CENTER:
                default:
                    isRFC = false;
                    break;
            }
            return GetService(true).GetActivePayment(param, isRFC);
        }

        public void SaveActivePayment(ActivePayment acp)
        {
            //save to branch server only
            if (!Session.Branch.OnlineConnection)
            {
                GetService(false).SaveActivePayment(acp);
            }
        }

        public void RealTimeSyncOfPaymentForOtherBranch(string caId)
        {
            ActivePayment acp = new ActivePayment();
            acp = GetActivePayment(new SAPSearchParam(caId, null, null, null), SearchType.CENTER);
            SaveActivePayment(acp);
        }

        public void RealTimeSyncOfPaymentForRFC(string caId)
        {
            ActivePayment acp = new ActivePayment();
            acp = GetActivePayment(new SAPSearchParam(caId, null, null, null), SearchType.RFC);
            SaveActivePayment(acp);
        }

        public List<LastReceiptPayment> GetRepayLastReceiptData(string receiptId)
        {
            return GetService().GetRepayLastReceiptData(receiptId);
        }

        #endregion

        [Flags]
        public enum SearchType : int { RFC, CENTER }


        public string CheckWorkStatus(OpenWorkParam param)
        {
            return  GetService().CheckWorkStatus(param);
        }

        public TrayMoneyInfo GetMoneyInTray(string workId)
        {
            return GetService().GetMoneyInTray(workId);
        }

        public bool IsArOwnerBranch(String caId)
        {
            CaIdAndBranchId caIdAndBranchId = GetBranchIdByCaId(caId);

            if (caIdAndBranchId == null)
                return false;
            else
                return (Session.Branch.Id == caIdAndBranchId.TechBranchId) || (Session.Branch.Id == caIdAndBranchId.CommBranchId);
        }

        public List<Invoice> SearchInvoiceFromOneTouch(OneTouchSearchParam param)
        {
            return GetService().SearchInvoiceFromOneTouch(param);
            
            ////string OneTouchGateWayConn = ConfigurationManager.AppSettings["ONETOUCH_GATEWAY"];
            ////string OneTouchTimeOut = ConfigurationManager.AppSettings["ONETOUCH_TIMEOUT"];
            //string OneTouchGateWayConn = "http://172.30.7.139/css_bpm.asmx";
            //string OneTouchTimeOut = "5000";

            //OneTouch.CSS_BPM rfc = new OneTouch.CSS_BPM();
            //rfc.Timeout = Int32.Parse(OneTouchTimeOut);
            //rfc.Url = OneTouchGateWayConn;


            ////Noti = FBRR57000030

            //DataTable dt;

            //try
            //{
            //    List<OneTouchInfo> OneTouchData = new List<OneTouchInfo>();
            //    List<Invoice> invoices = new List<Invoice>();

            //    dt = rfc.SelectReqCharge(param.NotificationNo).ReqChargeRecord;

            //    //var OneTouchData = ds.ReqChargeRecord.TableName[0];

            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        Bill b = new Bill();
            //        Invoice inv = new Invoice();

            //        b.CustomerId = dr["CaId"].ToString();
            //        b.Name = dr["CaName"].ToString();
            //        b.Address = dr["CaAddress"].ToString();
            //        b.AccountClass = "";
            //        b.DebtId = "M" + dr["DebtId"].ToString();
            //        b.DebtType = dr["DebtType"].ToString();
            //        b.InvoiceNo = dr["InvoiceNo"].ToString();
            //        b.Period = "";
            //        b.Qty = DaHelper.GetDecimal(dr, "Qty"); //(decimal)dr["Qty"]; 
            //        b.FullQty = b.Qty;
            //        b.DueDate = DateTime.Now;
            //        b.UnitTypeId = null;
            //        b.UnitTypeName = null;
            //        b.TaxCode = dr["TaxCode"].ToString();
            //        b.TaxRate = DaHelper.GetDecimal(dr, "TaxRate"); //(decimal)dr["TaxRate"];
            //        b.VatAmount = DaHelper.GetDecimal(dr, "VatAmount"); //(decimal)dr["VatAmount"];
            //        b.FullVatAmount = b.VatAmount;
            //        b.GAmount = DaHelper.GetDecimal(dr, "GAmount"); //(decimal)dr["GAmount"];
            //        b.FullGAmount = b.GAmount;
            //        b.AmountExVat = b.GAmount - b.VatAmount;
            //        b.FullAmount = b.AmountExVat;
            //        b.BaseAmount = b.AmountExVat;
            //        b.FullBaseAmount = b.BaseAmount;
            //        b.ToPayQty = b.Qty;
            //        b.ToPayGAmount = b.GAmount;
            //        b.ToPayVatAmount = b.VatAmount;
            //        b.DisConnectDate = null;

            //        //Tax 13 
            //        b.CaTaxId = dr["CaTaxId"].ToString();
            //        b.CaTaxBranch = dr["CaTaxBranch"].ToString();
            //        b.ControllerId = "";// _controllerId;

            //        b.NotificationNo = dr["NotificationNo"].ToString();
            //        //if (true)
            //        //{
            //        //    b.DataState = BillDataStage.Offline;
            //        //    inv.DataState = InvoiceDataStage.Offline;
            //        //}
            //        //else
            //        //{
            //        b.DataState = BillDataStage.NewItemOneTouch;
            //        inv.DataState = InvoiceDataStage.NewItemOneTouch;
            //        //}

            //        inv.InvoiceNo = b.InvoiceNo;
            //        inv.SpotBillInvoiceNo = b.InvoiceNo;
            //        inv.BranchId = dr["TechBranchId"].ToString();
            //        inv.TechBranchName = "";
            //        inv.CommBranchId = "";//_commBranchId;
            //        inv.CommBranchName = "";//_commBranchName;
            //        inv.CaId = b.CustomerId;
            //        inv.Name = b.Name;
            //        inv.Address = b.Address;
            //        inv.DueDate = b.DueDate;
            //        inv.ControllerId = "";//_controllerId;
            //        inv.ControllerName = "";//_controllerName;
            //        inv.MruId = "";//_mruId;
            //        inv.AmountExVat = b.AmountExVat;
            //        inv.VatAmount = b.VatAmount;
            //        inv.GAmount = b.GAmount;
            //        inv.PaidVatAmount = 0;
            //        inv.PaidGAmount = 0;
            //        inv.PaidQty = 0;
            //        inv.Qty = b.Qty;
            //        inv.ToPayQty = b.Qty;
            //        inv.ToPayVatAmount = b.VatAmount;
            //        inv.ToPayGAmount = inv.ToBePaidGAmount;

            //        //Tax 13 
            //        inv.CaTaxId = b.CaTaxId;
            //        inv.CaTaxBranch = b.CaTaxBranch;

            //        inv.Bills = new List<Bill>();
            //        inv.Bills.Add(b);

            //        inv.NotificationNo = b.NotificationNo;


            //        invoices.Add(inv);
            //    }

            //    return invoices;

            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteError(Logger.Module.POS, "SearchInvoiceFromOneTouch", ex.ToString());
            //    throw;
            //}
        
        }

        public void SaveOneTouchLog(OneTouchLogInfo log)
        {
            GetService().SaveOneTouchLog(log);
        }

        public bool FlagOneTouchPayment(OneTouchLogInfo param)
        {
            return GetService().FlagOneTouchPayment(param);    
        }
        
        public List<OfflinePayment> GetOfflinePayment(string branchId, string cashierId, string workId)
        {
            return GetService().GetOfflinePayment(branchId, cashierId, workId);
        }

        public void UpdateOfflinePayment(string branchId, string cashierId, string posId, string workId)
        {
            GetService().UpdateOfflinePayment(branchId, cashierId, posId, workId);
        }

        //// รวมใบเสร็จแผนผ่อน 2021-10-07 Check ใบเสร็จรวม Enable Status from ta.AppSetting
        public string CheckSettingGroupReceiptEnableStatus()
        {
            return GetService().CheckSettingGroupReceiptEnableStatus();
        }

        /// <summary>
        /// DCR QR Payment 2023-03
        /// Check status QR Payment
        /// </summary>
        /// <returns> </returns>
        public QRPaymentResponse CheckStatusQRPayment(QRPaymentInfo qrpaymentInfo)
        {
            return GetService().CheckStatusQRPayment(qrpaymentInfo);     
        }

        public string QRPaymentMethodByBranchId(string branchId)
        {
            return GetService().QRPaymentMethodByBranchId(branchId);
        }


        #region IBillingService Members


        public QRRefundResponse QRPaymentRefund(QRPaymentInfo qrpaymentInfo)
        {
            return GetService().QRPaymentRefund(qrpaymentInfo);
        }

        public QRRefundResponse QRPostOfflinePayment(QRPaymentInfo qrpaymentInfo)
        {
            return GetService().QRPostOfflinePayment(qrpaymentInfo);
        }

        #endregion

        
    }
}
