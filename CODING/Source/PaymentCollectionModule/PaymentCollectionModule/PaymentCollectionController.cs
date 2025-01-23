using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.PaymentCollectionModule.Views;
using System.Data.Common;
using System.Data;
using PEA.BPM.Architecture.CommonUtilities;
using System.Collections;
using System.ComponentModel;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using PEA.BPM.PaymentCollectionModule.Views.QRPaymentView;
using PEA.BPM.PaymentCollectionModule.Views.SearchMultiDocView;


namespace PEA.BPM.PaymentCollectionModule
{
    public class PaymentCollectionController : WorkItemController
    {
        private ILayoutService _layoutService;
        private IBillingService _billingService;

        [InjectionConstructor]
        public PaymentCollectionController([ServiceDependency] ILayoutService layoutService,
                                                           IBillingService billingService)
        {
            _layoutService = layoutService;
            _billingService = billingService;
        }

        private WindowSmartPartInfo info;

        public void Run(int paymentCollectionType)
        {
            ShellWaitCursor(true);
            WorkItem.State["ToBePaidBills"] = new List<ToBePaidBill>();
            WorkItem.State["ToBePaidInvoices"] = new List<ToBePaidInvoice>();

            LoadDisplayedViews(paymentCollectionType);
            LoadRequiredViews();
            ShellWaitCursor(false);
        }

        private void LoadDisplayedViews(int paymentCollectionType)
        {
            _layoutService.LoadHorizontalLayout(230);
            string workDayCount = string.Format(" - กำลังทำงานกะที่ ({0})", Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString());

            switch (paymentCollectionType)
            {
                case 1:
                    WorkItem.State["ScreenType"] = ScreenType.General;
                    IBillSearchView lView1;
                    SetWindowsTitle(Properties.Resources.GeneralPaymentCollection + workDayCount);
                    if (WorkItem.Items.Contains("IBillSearchView"))
                    {
                        lView1 = WorkItem.Items.Get<IBillSearchView>("IBillSearchView");
                    }
                    else
                    {
                        lView1 = WorkItem.Items.AddNew<BillSearchView>("IBillSearchView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView1);

                    IToBePaidInvoiceView rView1;
                    if (WorkItem.Items.Contains("IToBePaidInvoiceView"))
                    {
                        rView1 = WorkItem.Items.Get<IToBePaidInvoiceView>("IToBePaidInvoiceView");
                    }
                    else
                    {
                        rView1 = WorkItem.Items.AddNew<ToBePaidInvoiceView>("IToBePaidInvoiceView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView1);
                    break;
                case 2:
                    WorkItem.State["ScreenType"] = ScreenType.BillBook;
                    IBookSearchView lView2;
                    SetWindowsTitle(Properties.Resources.BillBookPaymentCollection + workDayCount);
                    if (WorkItem.Items.Contains("IBookSearchView"))
                    {
                        lView2 = WorkItem.Items.Get<IBookSearchView>("IBookSearchView");
                    }
                    else
                    {
                        lView2 = WorkItem.Items.AddNew<BookSearchView>("IBookSearchView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView2);

                    IToBePaidBillBookView rView2;
                    if (WorkItem.Items.Contains("IToBePaidBillBookView"))
                    {
                        rView2 = WorkItem.Items.Get<IToBePaidBillBookView>("IToBePaidBillBookView");
                    }
                    else
                    {
                        rView2 = WorkItem.Items.AddNew<ToBePaidBillBookView>("IToBePaidBillBookView");
                    }

                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView2);
                    break;
                case 3:
                    IReceiptGeneratetSearchView lView3;
                    SetWindowsTitle(Properties.Resources.GenerateReceipt + workDayCount);
                    if (WorkItem.Items.Contains("IReceiptGeneratetSearchView"))
                    {
                        lView3 = WorkItem.Items.Get<IReceiptGeneratetSearchView>("IReceiptGeneratetSearchView");
                    }
                    else
                    {
                        lView3 = WorkItem.Items.AddNew<ReceiptGeneratetSearchView>("IReceiptGeneratetSearchView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView3);

                    IReceiptGenerateResultView rView3;
                    if (WorkItem.Items.Contains("IReceiptGenerateResultView"))
                    {
                        rView3 = WorkItem.Items.Get<IReceiptGenerateResultView>("IReceiptGenerateResultView");
                    }
                    else
                    {
                        rView3 = WorkItem.Items.AddNew<ReceiptGenerateResultView>("IReceiptGenerateResultView");
                    }

                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView3);
                    break;
                default:
                    break;
            }            
        }

        private void LoadRequiredViews()
        {
            if (!WorkItem.Items.Contains("IBillSearchResultView"))
            {
                WorkItem.Items.AddNew<BillSearchResultView>("IBillSearchResultView");
            }

            if (!WorkItem.Items.Contains("IBookSearchResultView"))
            {
                WorkItem.Items.AddNew<BookSearchResultView>("IBookSearchResultView");
            }

            if (!WorkItem.Items.Contains("IBillDetailView"))
            {
                WorkItem.Items.AddNew<BillDetailView>("IBillDetailView");
            }

            if (!WorkItem.Items.Contains("IPaymentHistoryView"))
            {
                WorkItem.Items.AddNew<PaymentHistoryView>("IPaymentHistoryView");
            }

            if (!WorkItem.Items.Contains("INewPaymentItemView"))
            {
                WorkItem.Items.AddNew<NewPaymentItemView>("INewPaymentItemView");
            }

            if (!WorkItem.Items.Contains("IElectricPaymentView"))
            {
                WorkItem.Items.AddNew<ElectricPaymentView>("IElectricPaymentView");
            }

            if (!WorkItem.Items.Contains("IRemeterView"))
            {
                WorkItem.Items.AddNew<RemeterView>("IRemeterView");
            }

            if (!WorkItem.Items.Contains("IAdvancedPaymentView"))
            {
                WorkItem.Items.AddNew<AdvancedPaymentView>("IAdvancedPaymentView");
            }

            if (!WorkItem.Items.Contains("IPaymentMethodSelectionView"))
            {
                WorkItem.Items.AddNew<PaymentMethodSelectionView>("IPaymentMethodSelectionView");
            }

            //if (!WorkItem.Items.Contains("IPaymentItemLinkView"))
            //{
            //    WorkItem.Items.AddNew<PaymentItemLinkView>("IPaymentItemLinkView");
            //}

            if (!WorkItem.Items.Contains("ISlipPrePrintingView"))
            {
                WorkItem.Items.AddNew<SlipPrePrintingView>("ISlipPrePrintingView");
            }

            if (!WorkItem.Items.Contains("ISlipHeaderUpdatingView"))
            {
                WorkItem.Items.AddNew<SlipHeaderUpdatingView>("ISlipHeaderUpdatingView");
            }

            if (!WorkItem.Items.Contains("ISlipPrintingView"))
            {
                WorkItem.Items.AddNew<SlipPrintingView>("ISlipPrintingView");
            }

            if (!WorkItem.Items.Contains("IInvoiceDetailView"))
            {
                WorkItem.Items.AddNew<InvoiceDetailView>("IInvoiceDetailView");
            }

            if (!WorkItem.Items.Contains("IGroupInvoicingReport"))
            {
                WorkItem.Items.AddNew<GroupInvoicingReport>("IGroupInvoicingReport");
            }

            if (!WorkItem.Items.Contains("IReceiptGenerateResultView"))
            {
                WorkItem.Items.AddNew<ReceiptGenerateResultView>("IReceiptGenerateResultView");
            }

            if (!WorkItem.Items.Contains("IReceiptGeneratetSearchView"))
            {
                WorkItem.Items.AddNew<ReceiptGeneratetSearchView>("IReceiptGeneratetSearchView");
            }

            // DCR QR Payment 2023-03 : Create work item for QR Payment.
            if (!WorkItem.Items.Contains("IQRPaymentView"))
            {
                WorkItem.Items.AddNew<QRPaymentView>("IQRPaymentView");
            }

            // DCR 67-020  Search by multi caid.
            if (!WorkItem.Items.Contains("ISearchMultiDataView"))
            {
                WorkItem.Items.AddNew<SearchMultiDataView>("ISearchMultiDataView");
            }
        }

        #region Event Handler

        [EventSubscription(Constants.EventTopicNames.ElectricPaymentItemAdd, Thread = ThreadOption.UserInterface)]
        public void ElectricPaymentItemAddHandler(object sender, EventArgs e)
        {
            ControlledWorkItem<ElectricPaymentController> electricPaymentWorkItem = WorkItem.WorkItems.AddNew<ControlledWorkItem<ElectricPaymentController>>();
            electricPaymentWorkItem.Controller.Run();
        }

        [EventSubscription(Constants.EventTopicNames.ReMeterItemAdd, Thread = ThreadOption.UserInterface)]
        public void ElectricPaymentItemAddHandler(object sender, EventArgs<string> e)
        {
            object a = WorkItem.State["ToBePaidInvoices"];

            ArrayList obj = new ArrayList();
            obj.Add(a);
            obj.Add(e.Data);

            ControlledWorkItem<RemeterController> reMeterWorkItem = WorkItem.WorkItems.AddNew<ControlledWorkItem<RemeterController>>();
            reMeterWorkItem.Controller.Run(obj);
        }


        [EventSubscription(Constants.EventTopicNames.NewPaymentItemAdd, Thread = ThreadOption.UserInterface)]
        public void NewPaymentItemAddHandler(object sender, EventArgs<bool> e)
        {
            ArrayList obj = new ArrayList();
            obj.Add(e.Data);

            ControlledWorkItem<NewPaymentItemController> newPaymentItemWorkItem = WorkItem.WorkItems.AddNew<ControlledWorkItem<NewPaymentItemController>>();
            newPaymentItemWorkItem.Controller.Run(obj);
        }


        [EventSubscription(EventTopicNames.BillSearchedByCustomerDetail, Thread = ThreadOption.UserInterface)]
        public void BillSearchedByDetailHandler(object sender, EventArgs<CustomerSearchParam> e)
        {
            ControlledWorkItem<BillSearchResultController> billSearchResultWorkItem = WorkItem.WorkItems.AddNew<ControlledWorkItem<BillSearchResultController>>();
            billSearchResultWorkItem.Controller.BillSearchResultController_Run(e.Data);
        }


        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }


        //[EventPublication(EventTopicNames.DefaultOutputPrint, PublicationScope.WorkItem)]
        //public event EventHandler<EventArgs> DefaultOutputPrint;

        //[EventPublication(EventTopicNames.PrintingOutputModify, PublicationScope.WorkItem)]
        //public event EventHandler<EventArgs> PrintingOutputModify;

        //[EventSubscription(EventTopicNames.PaymentMethodSave, Thread = ThreadOption.UserInterface)]
        //public void PaymentMethodSaveHandler(object sender, EventArgs e)
        //{
        //    DialogResult result = MessageBox.Show(
        //        "ระบบกำลังจะพิมพ์ใบเสร็จตามค่าปกติที่ถูกกำหนดไว้\nกดปุ่ม 'OK' หากไม่ต้องการแก้ไข หรือกดปุ่ม 'Cancel' เพื่อกำหนดรูปแบบการพิมพ์ใบเสร็จเอง",
        //        "พิมพ์ใบเสร็จ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

        //    if (result != DialogResult.Cancel)
        //    {
        //        if (DefaultOutputPrint != null)
        //            DefaultOutputPrint(this, new EventArgs());
        //    }
        //    else
        //    {
        //        if (PrintingOutputModify != null)
        //            PrintingOutputModify(this, new EventArgs());
        //    }
        //}

        #endregion
    }
}
