using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI;
using System.Drawing;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using PEA.BPM.PaymentManagementModule.Services;
using System.ComponentModel;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.PaymentManagementModule.Interface;
using PEA.BPM.PaymentManagementModule.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentManagementModule;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using PEA.BPM.PaymentManagementModule.Interface.Services;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.CashManagementModule.Interface.Services;

namespace PEA.BPM.PaymentManagementModule
{
    public class ModuleController : WorkItemController, IDisposable
    {
        private ToolStripMenuItem paymentManagementMenuItem;

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
            //WorkItem.Services.AddNew<BillingService, IBillingService>();
            WorkItem.Services.AddNew<PaymentMntService, IPaymentMntService>();
            WorkItem.Services.AddNew<ReportService, IReportService>();
        }

        private void ExtendMenu()
        {
            paymentManagementMenuItem = new ToolStripMenuItem("3. ®Ë“¬‡ß‘π");

            WorkItem.UIExtensionSites[UIExtensionSiteNames.MainMenu].Add(paymentManagementMenuItem);

            AddMenuItem(paymentManagementMenuItem, Properties.Resources.AccountPayablePayment, CommandNames.AccountPayablePayment, 0);
            AddMenuItem(paymentManagementMenuItem, Properties.Resources.AccountPayableCancellation, CommandNames.PaymentVoucherCancellation, 0);
            AddMenuSeparator(paymentManagementMenuItem);
            AddMenuItem(paymentManagementMenuItem, Properties.Resources.RpAP, "AP", 0);

            if (Session.Branch.Id == null)
                paymentManagementMenuItem.Enabled = false;
        }      

        private void ExtendToolStrip()
        {

        }
        #endregion

        #region Command Handler

        [CommandHandler(CommandNames.AccountPayablePayment)]
        public void BranchDayClosingHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.AccountPayablePayment, true))
            {
            this.LoadUseCase<AccountPayablePaymentController>().Controller.Run();
            }
        }
        
        [CommandHandler(CommandNames.PaymentVoucherCancellation)]
        public void ViewPaymentVoucherCancellationHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.PaymentVoucherCancellation, true))
            {
                this.LoadUseCase<PaymentVoucherCancellationController>().Controller.Run();
            }
        }

        [CommandHandler("AP")]
        public void ViewReportAPHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.AP01, true))
            {
            this.LoadUseCase<ReportController>().Controller.Run(ReportName.AP);
            }
        }
        
        #endregion


        #region Event handler

        [EventSubscription(EventTopicNames.OnlineStatus, Thread = ThreadOption.UserInterface)]
        public void OnlineStatusHandler(object sender, EventArgs<bool> e)
        {
            if (e.Data)
                paymentManagementMenuItem.Enabled = true;
            else
                paymentManagementMenuItem.Enabled = false;
        }

        #endregion

        #region Event Handling

        [EventSubscription(Constants.EventTopicNames.ClosePaymentView, Thread = ThreadOption.UserInterface)]
        public void ClosePaymentViewHandler(object sender, EventArgs e)
        {
            this.ClearExistingUseCase();
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
