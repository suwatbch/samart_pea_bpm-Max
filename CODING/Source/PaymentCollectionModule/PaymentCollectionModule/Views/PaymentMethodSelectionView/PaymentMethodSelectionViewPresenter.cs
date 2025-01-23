//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// A presenter calls methods of a view to update the information that the view displays. 
// The view exposes its methods through an interface definition, and the presenter contains
// a reference to the view interface. This allows you to test the presenter with different 
// implementations of a view (for example, a mock view).
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.Windows.Forms;
using PEA.BPM.Infrastructure.Library.UI;
using System.Data;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Collections.Generic;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using PEA.BPM.PaymentCollectionModule.Views.PaymentMethodSelectionView;
using System.Runtime.Serialization;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView;
using System.Security.AccessControl;
using ProtoBuf;

namespace PEA.BPM.PaymentCollectionModule
{
    public class PaymentMethodSelectionViewPresenter : Presenter<IPaymentMethodSelectionView>
    {
		public IBillingService _billingService;

		[InjectionConstructor]
        public PaymentMethodSelectionViewPresenter([ServiceDependency] IBillingService billingService)
		{
            _billingService = billingService;
		}

        public bool CheckPaidGAmount(string invoiceNo)
        {
            return _billingService.GetPaidGAmount(invoiceNo);
        }

        public bool CheckInActiveBillBook(string invoiceNo)
        {
            return _billingService.GetInActiveBillBook(invoiceNo);
        }

        public bool CheckDuplicateExtReceipt(string receiptId, string branchId)
        {
            return _billingService.GetDuplicateExtReceipt(receiptId, branchId);
        }

        [EventPublication(EventTopicNames.CashierOpenWork, PublicationScope.Global)]
        public event EventHandler<EventArgs<string>> CashierOpenWorkHandler;
        public void OnCashierOpenWork(string tmp)
        {
            if (CashierOpenWorkHandler != null)
                CashierOpenWorkHandler(this, new EventArgs<string>(tmp));
        }
        
        [EventSubscription(Constants.EventTopicNames.InvoicePaymentMethodClick, Thread = ThreadOption.UserInterface)]
        public void InvoicePaymentMethodClickHandler(object sender, EventArgs<List<Invoice>> e)
        {
            View.PayingInvoices = e.Data;
            ShowView();
        }

        private void ShowView()
        {   
            PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
            info.Keys.Add(WindowWorkspaceSetting.CancelButton, View.CancelButton);
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Title = " ## ��س��к��Ըա�ê����Թ";
            View.ResetForm();

            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(View, info);
        }

        public ResultPayInvoice PayInvoice(List<Invoice> paidInvoices, List<PaymentMethod> paymentMethods,
            List<PrintingReceipt> receipts, List<PrintingReceipt> groupDividualReceipts, ExternalReceipt extReceipt,
            DateTime paymentDate, string posId, decimal? payingAmount, string screenType)
        {
            if (paidInvoices[0].DataState == InvoiceDataStage.Offline)
            {
                //ensure that BPM is in offline mode
                Session.IsNetworkConnectionAvailable = false;

                List<PrintingReceipt> printingReceipts = (List<PrintingReceipt>)WorkItem.State["PrintingReceipts"];
                OfflineReceipt offlineReceipt = new OfflineReceipt(printingReceipts, extReceipt, paymentDate);

                OfflineItems items = new OfflineItems();
                items.Invoices = paidInvoices;
                items.PaymentMethods = paymentMethods;
                items.PaymentDt = paymentDate;
                items.PosId = posId;
                items.TerminalCode = Session.Terminal.Code;
                items.PayingAmount = payingAmount;
                items.ScreenType = screenType;
                items.BranchId = Session.Branch.Id;
                items.BranchName = Session.Branch.Name;
                items.Receipts = receipts;
                items.GroupDividualReceipts = groupDividualReceipts;
                items.ExtReceipt = extReceipt;
                items.CashierId = Session.User.Id;
                items.CashierName = Session.User.Name;
                items.WorkId = Session.Work.Id;
                PayOffLineBill(items);

                ResultPayInvoice obj = new ResultPayInvoice();
                obj.PaidInvoices = paidInvoices;
                obj.Receipts = null;

                return obj;
            }
            else
            {
                ResultPayInvoice obj = new ResultPayInvoice();

                obj = _billingService.PayInvoice(null, paidInvoices, paymentMethods, receipts, groupDividualReceipts, extReceipt,
                                                  paymentDate, Session.Terminal.Id, Session.Terminal.Code, Session.Branch.Id, 
                                                  Session.Branch.Name, payingAmount, screenType, Session.User.Id, Session.User.Name,
                                                  Session.Work.Id, false);

                
                

                    //Insert Log OneTouch
                obj.OneTouchFlag = true;   //
                foreach (PrintingReceipt r in obj.Receipts)
                {
                    if (r.PrintingInvoices[0].DataState == InvoiceDataStage.NewItemOneTouch)
                    {

                        OneTouchLogInfo OneTouchLog = new OneTouchLogInfo();
                        OneTouchLog.NotificationNo  = r.PrintingInvoices[0].NotificationNo;
                        OneTouchLog.InvoiceNo       = r.PrintingInvoices[0].SpotBillInvoiceNo;
                        OneTouchLog.DebtId          = r.PrintingInvoices[0].Bills[0].DebtId; //���� DebTyptId
                        OneTouchLog.ReceiptId       = r.ReceiptId.Replace("-", "");
                        OneTouchLog.Action          = "1"; //Add
                        OneTouchLog.GAmount         = r.PrintingInvoices[0].ToPayGAmount;
                        OneTouchLog.VatAmount       = r.PrintingInvoices[0].ToPayVatAmount;
                        OneTouchLog.ModifiedBy      = Session.User.Id;


                        //Call Web Service OneTouch --> SCS
                        //#ISSUE VSPP "Incase VSPP we edit in BillingSG.cs already ,but need to SaveVSPPLog aswell". 2017Dec20
                        bool flag = _billingService.FlagOneTouchPayment(OneTouchLog);

                        if (flag == false)
                        {
                            OneTouchLog.SyncFlag    = "1";
                            obj.OneTouchFlag        = false;
                        }
                        else
                        {
                            OneTouchLog.SyncFlag    = "0";  //����
                            obj.OneTouchFlag        = true;
                        }

                        //Insert Log OneTouch
                        try
                        {
                            _billingService.SaveOneTouchLog(OneTouchLog);
                        }
                        catch (Exception ex)
                        {
                            //
                        }
                    }
                }

                return obj;
            }
        }

        private void PayOffLineBill(OfflineItems items)
        {
            string dirPath = BPMPath.ConfigPath + "\\OfflineData";
            //IFormatter serializer = new BinaryFormatter();
            //protect interupt process while writing
            string rawName = string.Format(dirPath + "\\{0}-{1}.", Session.Terminal.Id, items.PaymentDt.Value.ToString("yyyyMMdd-hhmmss"));
            string tmpName = string.Format("{0}{1}", rawName, "tmp");
            using (Stream writer = new FileStream(tmpName, FileMode.Create))
            {
                Serializer.Serialize<OfflineItems>(writer, items);
                //serializer.Serialize(writer, items);
                writer.Close();
            }

            string fileName = string.Format("{0}{1}", rawName, "txt");
            File.Move(tmpName, fileName);

            //set file attribute
            FileInfo file = new FileInfo(fileName);
            file.Attributes = FileAttributes.ReadOnly | FileAttributes.Hidden;
        }

        public List<PaymentMethod> SearchAGPayment(string billBookId)
        {
            return _billingService.SearchAGPayment(billBookId);
        }

        internal List<GroupInvoiceItem> GetGroupInvoiceItem(string billBookId)
        {
            return _billingService.GetGroupInvoiceItem(billBookId);
        }

        [EventPublication(EventTopicNames.PaymentItemSave, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> PaymentItemSave;
        internal void OnPaymentItemSaving()
        {
            if (PaymentItemSave != null)
                PaymentItemSave(this, new EventArgs());
        }

        [EventPublication(EventTopicNames.PaymentMethodSave, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<string>> PaymentMethodSave;
        internal void OnPaymentMethodSaving(string paperSize)
        {
            if (PaymentMethodSave != null)
                PaymentMethodSave(this, new EventArgs<string>(paperSize));
        }

        [EventPublication(EventTopicNames.ShowingChangeAmount, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<decimal>> ShowingChangeAmount;
        public void OnShowingChangeAmount(decimal changeAmount)
        {
            if (ShowingChangeAmount != null)
                ShowingChangeAmount(this, new EventArgs<decimal>(changeAmount));
        }

        [EventPublication(EventTopicNames.ClosePaymentView, PublicationScope.Global)]
        public event EventHandler<EventArgs> ClosePaymentView;
        public void OnClosePaymentView()
        {
            if (ClosePaymentView != null)
                ClosePaymentView(this, new EventArgs());
        }

        [EventPublication(EventTopicNames.PrintingTypeSet, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> PrintingTypeSet;
        internal void OnPrintingTypeSet()
        {
            if (PrintingTypeSet != null)
                PrintingTypeSet(this, new EventArgs());
        }

        [EventSubscription(EventTopicNames.OnCloseAllViews, Thread = ThreadOption.UserInterface)]
        public void OnCloseAllViewsHandler(object sender, EventArgs e)
        {
            OnCloseView();
        }

        /// <summary>
        /// 2023-03-09 QRPayment.
        /// </summary>
        [EventPublication(EventTopicNames.QRPaymentMethodClick, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<PaymentMethod>> QRPaymentMethodClicked;
        internal void OnQRPaymentMethodClicked(PaymentMethod paymentMethod)
        {
            if (QRPaymentMethodClicked != null)
                QRPaymentMethodClicked(this, new EventArgs<PaymentMethod>(paymentMethod));
        }

        public decimal CalculateChangeAmount(decimal _totalAmount, List<PaymentMethod> payingMethods)
        {
            decimal cash = 0;
            for (int i = 0; i < payingMethods.Count; i++)
            {
                _totalAmount = _totalAmount - ((payingMethods[i].ToPayAmountWithFee != null) ? payingMethods[i].ToPayAmountWithFee.Value : 0);
                
                // find total paying cash
                if (payingMethods[i].PtId == CodeNames.PaymentType.Cash.Id)
                {
                    cash = cash + ((payingMethods[i].ToPayAmountWithFee != null) ? payingMethods[i].ToPayAmountWithFee.Value : 0);
                }
            }

            if (_totalAmount == 0)
            {
                return 0;
                
            }
            else if (_totalAmount > 0)
            {
                return -_totalAmount;
            }
            else
            {
                decimal change = -_totalAmount;

                // maximum change is not more than cash
                if (change < cash)
                {
                    return change;
                }
                else
                {
                    return cash;
                }
            }
        }

        public bool CheckOffline()
        {
            if (!Session.IsNetworkConnectionAvailable)
            {
                return false;
            }
            else
            {
                try
                {
                    _billingService.GetServerTime();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// This method is a placeholder that will be called by the view when it's been loaded <see cref="System.Winforms.Control.OnLoad"/>
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
        }

        /// <summary>
        /// Close the view
        /// </summary>
        public void OnCloseView()
        {
            base.CloseView();
        }

        //// ��������Ἱ��͹ 2021-10-07 Check �������� Enable Status from ta.AppSetting
        public string CheckSettingGroupReceiptEnableStatus()
        {
            return _billingService.CheckSettingGroupReceiptEnableStatus();
        }

        //// QRPayment 2023-08
        public void CheckQRPaymentEnableByBranchId() {

            if (Session.IsNetworkConnectionAvailable == false || Session.IsForcedOffline == true)
            {
                View.EnableQRPayment(false);
            }
            else {
                // Get configuration by from 
                string qrPaymentStatus = _billingService.QRPaymentMethodByBranchId(Session.Branch.Id);

                if (qrPaymentStatus == "1")
                {
                    // Enable QRPayment tab. 
                    View.EnableQRPayment(true);
                }
                else
                {
                    // Disable QRPayment tab.
                    View.EnableQRPayment(false);
                }
            
            }
        }

    }
}

