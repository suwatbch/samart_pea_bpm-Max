using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.BillPrintingModule.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace PEA.BPM.BillPrintingModule
{
    public class ModuleController : WorkItemController, IDisposable
    {
        private ToolStripMenuItem _reportMenuItem;        
        private ToolStripMenuItem billPrintingMenuItem;

        #region Run
        public override void Run()
        {
            AddServices();
            ExtendMenu();
            ExtendToolStrip();
            SetEnvVariable();
        }

        protected virtual void AddServices()
        {
        }

        private void ExtendMenu()
        {
            billPrintingMenuItem = new ToolStripMenuItem(Properties.Resources.BillPrinting);
            WorkItem.UIExtensionSites[UIExtensionSiteNames.MainMenu].Add(billPrintingMenuItem);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.DualBill, CommandNames.DualBill, Keys.Control | Keys.B);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.DualBillReprint, CommandNames.DualBillReprint, 0);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.A4Bill, CommandNames.A4Bill, Keys.Control | Keys.A);
            //ToolStripMenuItem groupInvoiceMenuItem = AddGroupMenu(billPrintingMenuItem, Properties.Resources.GroupInvoice);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.GroupInvoice, CommandNames.GroupInvoice, Keys.Control | Keys.G);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.GroupInvoiceReprint, CommandNames.GroupInvoiceReprint, 0);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.GreenBill, CommandNames.GreenBill, Keys.Control | Keys.D);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.GreenBillReprint, CommandNames.GreenBillReprint, 0);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.GreenReceipt, CommandNames.GreenReceipt, Keys.Control | Keys.R);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.GreenReceiptReprint, CommandNames.GreenReceiptReprint, 0);
            AddMenuSeparator(billPrintingMenuItem);
            _reportMenuItem = AddGroupMenu(billPrintingMenuItem, Properties.Resources.Report);
            AddMenuItem(_reportMenuItem, Properties.Resources.StreetRouteReport, CommandNames.StreetRouteReport, 0);
            //AddMenuItem(_reportMenuItem, Properties.Resources.StreetRouteUnreceiveReport, CommandNames.StreetRouteUnreceiveReport, 0);
            AddMenuItem(_reportMenuItem, Properties.Resources.StreetRouteReceiveReport, CommandNames.StreetRouteReceiveReport, 0);
            AddMenuItem(_reportMenuItem, Properties.Resources.DailyPrintReport, CommandNames.DailyPrintReport, 0);
            AddMenuItem(_reportMenuItem, Properties.Resources.DailyUnprintReport, CommandNames.DailyUnprintReport, 0);
            AddMenuItem(_reportMenuItem, Properties.Resources.BillDeliveryReport, CommandNames.BillDeliveryReport, 0);
            AddMenuItem(_reportMenuItem, Properties.Resources.F16Report, CommandNames.F16Report, 0);
            //New added: March, 12 '08 
            AddMenuItem(_reportMenuItem, Properties.Resources.GrpInvSummaryReport, CommandNames.GrpInvSummaryReport, 0);
            AddMenuItem(_reportMenuItem, Properties.Resources.PrintGreenByBankReport, CommandNames.PrintBankGreenBillReport, 0);
            AddMenuItem(_reportMenuItem, Properties.Resources.BillingStatusReport, CommandNames.BillingStatusReport, 0);
            AddMenuItem(_reportMenuItem, Properties.Resources.DirectDebitStatusReport, CommandNames.DirectDebitStatusReport, 0);
            //AddMenuItem(_reportMenuItem, Properties.Resources.GroupingCrossCheckReport, CommandNames.GroupingCrossCheckReport, 0);
            
            AddMenuSeparator(billPrintingMenuItem);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.DeliveryPlace, CommandNames.DeliveryPlace, 0);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.AuthorizedPerson, CommandNames.AuthorizedPerson, 0);
            AddMenuItem(billPrintingMenuItem, Properties.Resources.InvoiceHistory, CommandNames.InvoiceHistory, 0);

            if (Session.Branch.Id != "Z00000")
            {
                billPrintingMenuItem.DropDownItems[5].Enabled = false;
                billPrintingMenuItem.DropDownItems[6].Enabled = false;
                //billPrintingMenuItem.DropDownItems[14].Enabled = false;
                _reportMenuItem.DropDownItems[7].Enabled = false;
            }
            
            AddMenuItem(billPrintingMenuItem, Properties.Resources.ManageBarcode, CommandNames.ManageBarcode, 0);

#if (DEBUG)
            AddMenuItem(billPrintingMenuItem, Properties.Resources.PrintTargetLocation, CommandNames.PrintTargetLocation, 0);
#endif

            if (Session.Branch.Id == null)
                billPrintingMenuItem.Enabled = false;

        }

        private void ExtendToolStrip()
        {
            UIExtensionSite toolstrip = WorkItem.UIExtensionSites[UIExtensionSiteNames.MainToolbar];
            //AddToolStripButton(toolstrip, Properties.Resources.DualBill, Properties.Resources.DualBillMenuPic, CommandNames.DualBill);            
        }

        private void SetEnvVariable()
        {
            //set to default
            try
            {
                LocalSettingHelper hp = LocalSettingHelper.Instance();
                string printTarget = (string)hp.Read(LocalSettingNames.PrintTarget);

                if (string.IsNullOrEmpty(printTarget))
                {
                    //hp.Add(LocalSettingNames.PrintTarget, "A");
                    hp.Add(LocalSettingNames.PrintTarget, "P");  //printer only
                    hp.Add(LocalSettingNames.FilePrintTargetPath, "c:\\blanprintlog");

                    if (!System.IO.Directory.Exists("c:\\blanprintlog"))
                        System.IO.Directory.CreateDirectory("c:\\blanprintlog");
                }
            }
            catch //TODO: ตั้งใจ catch ไว้จริงๆ หรือเปล่า ต้อง remove ทิ้งไหม ? กลับมาดูอีกที
            {
            }
        }

        #endregion

        #region Command Handler
        
        [CommandHandler(CommandNames.DualBill)]
        public void DualBillHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.DualBillPrinting, true))
            {
                ControlledWorkItem<DualBillController> dualBillWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<DualBillController>>();
                dualBillWorkItem.Controller.Run();
            } 
        }

        [CommandHandler(CommandNames.A4Bill)]
        public void A4BillHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.A4BillPrinting, true))
            {
                ControlledWorkItem<A4BillController> a4BillWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<A4BillController>>();
                a4BillWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.DualBillReprint)]
        public void DualBillReprintHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.DualBillReprinting, true))
            {
                ControlledWorkItem<DualBillReprintController> dualBillReprintWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<DualBillReprintController>>();
                dualBillReprintWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.GroupInvoice)]
        public void GroupInvoiceHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.GroupInvoicePrinting, true))
            {
                ControlledWorkItem<GroupInvoiceController> groupInvoiceWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<GroupInvoiceController>>();
                groupInvoiceWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.GroupInvoiceReprint)]
        public void GroupInvoiceReprintHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.GroupInvoiceReprinting, true))
            {
                ControlledWorkItem<GroupInvoiceReprintController> groupInvoiceReprintWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<GroupInvoiceReprintController>>();
                groupInvoiceReprintWorkItem.Controller.Run();
            }
        }

        //print by bank
        [CommandHandler(CommandNames.GreenBill)]
        public void GreenBillHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.DirectDebitByBankPrinting, true))
            {
                ControlledWorkItem<GreenBillController> greenBillWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<GreenBillController>>();
                greenBillWorkItem.Controller.Run();
            }
        }

        //reporint by bank
        [CommandHandler(CommandNames.GreenBillReprint)]
        public void GreenBillReprintHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.DirectDebitByBankRePrinting, true))
            {
                ControlledWorkItem<GreenBillReprintController> greenBillReprintWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<GreenBillReprintController>>();
                greenBillReprintWorkItem.Controller.Run();
            }
        }


        //CommandHandler for report menu
        [CommandHandler(CommandNames.StreetRouteReport)]
        public void StreetRouteReportHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.StreetRouteReport, true))
            {
                ControlledWorkItem<ReportStreetRouteController> streetRouteReportWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportStreetRouteController>>();
                streetRouteReportWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.StreetRouteReceiveReport)]
        public void StreetRouteReceiveReportHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.StreetRouteReceiveReport, true))
            {
                ControlledWorkItem<ReportStreetRouteReceiveController> streetRouteReceiveReportWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportStreetRouteReceiveController>>();
                streetRouteReceiveReportWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.DailyPrintReport)]
        public void DailyPrintReportReportHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.DailyPrintReport, true))
            {
                ControlledWorkItem<ReportDailyPrintController> dailyPrintReportWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportDailyPrintController>>();
                dailyPrintReportWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.DailyUnprintReport)]
        public void DailyUnprintReportHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.DailyUnprintReport, true))
            {
                ControlledWorkItem<ReportDailyUnprintController> dailyUnprintReportWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportDailyUnprintController>>();
                dailyUnprintReportWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.BillDeliveryReport)]
        public void BillDeliveryReportHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.BillDeliveryReport, true))
            {
                ControlledWorkItem<ReportBillDeliveryController> billDeliveryReportWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportBillDeliveryController>>();
                billDeliveryReportWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.F16Report)]
        public void F16ReportHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.F16Report, true))
            {
                ControlledWorkItem<ReportF16Controller> f16ReportWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportF16Controller>>();
                f16ReportWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.GrpInvSummaryReport)]
        public void GrpInvSummaryHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.GrpInvSummaryReport, true))
            {
                ControlledWorkItem<ReportGrpInvSummaryController> grpInvSummaryWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportGrpInvSummaryController>>();
                grpInvSummaryWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.BillingStatusReport)]
        public void BillingStatusReportHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.BillingStatusReport, true))
            {
                ControlledWorkItem<ReportBillingStatusController> billingStatusWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportBillingStatusController>>();
                billingStatusWorkItem.Controller.Run();
            }
        }


        [CommandHandler(CommandNames.PrintBankGreenBillReport)]
        public void PrintBankGreenBillHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.PrintBankGreenBillReport, true))
            {
                ControlledWorkItem<ReportPrintGreenByBankController> bankGreenBillWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportPrintGreenByBankController>>();
                bankGreenBillWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.DeliveryPlace)]
        public void DeliveryPlaceHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.DeliveryPlace, true))
            {
                ControlledWorkItem<DeliveryPlaceController> deliveryPlaceWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<DeliveryPlaceController>>();
                deliveryPlaceWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.AuthorizedPerson)]
        public void AuthorizedPersonHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.AuthorizedPerson, true))
            {
                ControlledWorkItem<ApproverController> approverWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ApproverController>>();
                approverWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.InvoiceHistory)]
        public void ChangePrintTypeHandler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.ChangePrintType, true))
            //{
                ControlledWorkItem<InvoiceHistoryController> changePrintTypeWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<InvoiceHistoryController>>();
                changePrintTypeWorkItem.Controller.Run();
            //}
        }

        [CommandHandler(CommandNames.PrintTargetLocation)]
        public void PrintTargetLocationHandler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.PrintTargetLocation, true))
            //{
                ControlledWorkItem<PrinterSetupController> printerSetupWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<PrinterSetupController>>();
                printerSetupWorkItem.Controller.Run();
            //}
        }

        [CommandHandler(CommandNames.ManageBarcode)]
        public void ManageBarcodeHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.ManageBarcode, true))
            {
                ControlledWorkItem<ManageBarcodeController> manageBarcodeWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ManageBarcodeController>>();
                manageBarcodeWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.DirectDebitStatusReport)]
        public void DirectDebitStatusReportHandler(object sender, EventArgs e)
        {
            ControlledWorkItem<ReportDirectDebitStatusController> directDebitStatusWorkItem =
                WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportDirectDebitStatusController>>();
            directDebitStatusWorkItem.Controller.Run();
        }

        [CommandHandler(CommandNames.GreenReceipt)]
        public void GreenReceiptHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.GreenReceipt, true))
            {
                ControlledWorkItem<GreenReceiptController> greenReceiptWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<GreenReceiptController>>();
                greenReceiptWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.GreenReceiptReprint)]
        public void GreenReceiptReprintHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.GreenReceiptReprint, true))
            {
                ControlledWorkItem<GreenReceiptReprintController> greenReceiptReprintWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<GreenReceiptReprintController>>();
                greenReceiptReprintWorkItem.Controller.Run();
            }
        }

        #endregion

        #region Event Handler

        [EventSubscription(EventTopicNames.DeliveryPlacePopup, Thread = ThreadOption.UserInterface)]
        public void ShowDeliveryPlaceScreen(object sender, EventArgs e)
        {
            ControlledWorkItem<DeliveryPlaceController> deliveryPlaceWorkItem =
                WorkItem.WorkItems.AddNew<ControlledWorkItem<DeliveryPlaceController>>();
            deliveryPlaceWorkItem.Controller.Run();
        }

        [EventSubscription(EventTopicNames.OnlineStatus, Thread = ThreadOption.UserInterface)]
        public void OnlineStatusHandler(object sender, EventArgs<bool> e)
        {
            if (e.Data)
                billPrintingMenuItem.Enabled = true;
            else
                billPrintingMenuItem.Enabled = false;
        }


        #endregion

        #region Dispose
        ~ModuleController()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {                
            }
        }
        #endregion
    }
}


