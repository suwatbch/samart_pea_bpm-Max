using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI;
using System.Drawing;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.Constants;
using PEA.BPM.ePaymentsModule.Interface.Services;
using PEA.BPM.ePaymentsModule.Services;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.Architecture.CommonUtilities;
using System.ComponentModel;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;


namespace PEA.BPM.ePaymentsModule
{
    public class ModuleController : WorkItemController, IDisposable
    {
        private ToolStripMenuItem ePaymentCollectionMenuItem;

        #region Run
        public override void Run()
        {
            AddServices();
            ExtendMenu();
            ExtendToolStrip();

            base.Run();
        }      

        protected virtual void AddServices()
        {
            WorkItem.Services.AddNew<BillingService, IBillingService>();
            WorkItem.Services.AddNew<ReceiptPrintingService, IReceiptPrintingService>();
            WorkItem.Services.AddNew<ReportService, IReportService>();
        }

        private void ExtendMenu()
        {
            ePaymentCollectionMenuItem = new ToolStripMenuItem("6. ÃÑºªÓÃÐà§Ô¹ E-Payment");
            
            WorkItem.UIExtensionSites[UIExtensionSiteNames.MainMenu].Add(ePaymentCollectionMenuItem);

            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.UploadPaymentFile , CommandNames.UploadPaymentFile ,0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.VendorPayment , CommandNames.VendorPayment, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.CancelPayment, CommandNames.CancelPayment, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.DebtClearify, CommandNames.DebtClearify, 0);
            AddMenuSeparator(ePaymentCollectionMenuItem);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.GreenReceiptEPay, CommandNames.GreenReceiptEPay, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.PrePrinted, CommandNames.PrePrinted, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.DepositPrePrtined, CommandNames.DepositPrePrtined, 0);
            AddMenuSeparator(ePaymentCollectionMenuItem);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.RE_01, CommandNames.RE_01, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.RE_02, CommandNames.RE_02, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.RE_03, CommandNames.RE_03, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.RE_04, CommandNames.RE_04, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.RE_05, CommandNames.RE_05, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.RE_06, CommandNames.RE_06, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.RE_07, CommandNames.RE_07, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.RE_08, CommandNames.RE_08, 0);
            AddMenuItem(ePaymentCollectionMenuItem, Properties.Resources.RE_09, CommandNames.RE_09, 0);

            if (Session.Branch.Id == null)
            {
                ePaymentCollectionMenuItem.Enabled = false;
            }
        }      

        private void ExtendToolStrip()
        {
            bool enable = Session.Branch.Id != null;

             //UIExtensionSite toolstrip = WorkItem.UIExtensionSites[UIExtensionSiteNames.MainToolbar];
           // AddToolStripButton(toolstrip, Properties.Resources.GeneralPaymentCollection,
            //    Properties.Resources.PaymentCollectionIcon, CommandNames.ElectricalUserPaymentCollection, enable);
            //AddToolStripButton(toolstrip, Properties.Resources.PaymentOffline,
             //   Properties.Resources.PaymentOfflineIcon, CommandNames.PaymentOffline, enable);
        }
        #endregion

        #region Command Handler

        [CommandHandler(CommandNames.UploadPaymentFile)]
        public void ViewUploadPaymentFileHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.UploadPaymentFile))
            {
                this.LoadUseCase<ePaymentCollectionController>().Controller.Run(1);
            }
        }

        [CommandHandler(CommandNames.VendorPayment)]
        public void ViewVenderPaymentHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.VendorPayment))
            {
                this.LoadUseCase<ePaymentCollectionController>().Controller.Run(2);
            }
        }

        [CommandHandler(CommandNames.CancelPayment)]
        public void ViewCancelPaymentHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CancelPayment))
            {
                this.LoadUseCase<ePaymentCollectionController>().Controller.Run(4);
            }
        }

        [CommandHandler(CommandNames.DebtClearify)]
        public void ViewDetbClearifyHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.DebtClearify))
            {
                this.LoadUseCase<ePaymentCollectionController>().Controller.Run(3);
            }
        }

        [CommandHandler(CommandNames.GreenReceiptEPay)]
        public void ViewGreenReceiptHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.GreenReceipt))
            {
                this.LoadUseCase<ReceiptPrintingController>().Controller.Run(1);
            }
        }

        [CommandHandler(CommandNames.PrePrinted)]
        public void ViewPrePrintedHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.AgentReceipt))
            {
                this.LoadUseCase<ReceiptPrintingController>().Controller.Run(2);
            }
        }

        [CommandHandler(CommandNames.DepositPrePrtined)]
        public void ViewDepositPrePrtinedHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.DepositPrePrtined))
            {
                this.LoadUseCase<ReceiptPrintingController>().Controller.Run(3);
            }
        }

        [CommandHandler("RE_01")]
        public void ViewReportRE01Handler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.RE_01))
            //{
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.RE_01);
            //}
        }

        [CommandHandler("RE_02")]
        public void ViewReportRE02Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.RE_02))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.RE_02);
            }
        }

        [CommandHandler("RE_03")]
        public void ViewReportRE03Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.RE_03))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.RE_03);
            }
        }

        [CommandHandler("RE_04")]
        public void ViewReportRE04Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.RE_04))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.RE_04);
            }
        }

        [CommandHandler("RE_05")]
        public void ViewReportRE05Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.RE_05))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.RE_05);
            }
        }

        [CommandHandler("RE_06")]
        public void ViewReportRE06Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.RE_06))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.RE_06);
            }
        }

        [CommandHandler("RE_07")]
        public void ViewReportRE07Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.RE_07))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.RE_07);
            }
        }

        [CommandHandler("RE_08")]
        public void ViewReportRE08Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.RE_08))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.RE_08);
            }
        }

        [CommandHandler("RE_09")]
        public void ViewReportRE09Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.RE_09))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.RE_09);
            }
        }


        #endregion

        #region Event Handler

        [EventSubscription(EventTopicNames.OnlineStatus, Thread = ThreadOption.UserInterface)]
        public void OnlineStatusHandler(object sender, EventArgs<bool> e)
        {
            if (e.Data)
                ePaymentCollectionMenuItem.Enabled = true;
            else
                ePaymentCollectionMenuItem.Enabled = false;
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
