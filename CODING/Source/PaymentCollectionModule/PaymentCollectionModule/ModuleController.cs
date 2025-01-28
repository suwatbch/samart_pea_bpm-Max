using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using PEA.BPM.Infrastructure.Interface;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI;
using System.Drawing;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Services;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using System.ComponentModel;
using PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView;
using PEA.BPM.PaymentCollectionModule.Views.PaymentMethodSelectionView;
using PEA.BPM.Infrastructure.Layout;
using System.Data;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.Architecture.ArchitectureInterface.Services;


namespace PEA.BPM.PaymentCollectionModule
{

    public class ModuleController : WorkItemController, IDisposable
    {
        SlipPrintingMonitor spm;
        PaymentOfflineMonitor pom;
        BillSearchView bsm;
        ToolStripMenuItem paymentCollectionMenuItem;
        ToolStripButton posTSButton;
        ToolStripButton posOfflineButton;   //Offline by User

        private bool forcedOfflineStatus = false; //Offline by User

        #region Run
        public override void Run()
        {
            AddServices();
            ExtendMenu();
            ExtendToolStrip();

            spm = new SlipPrintingMonitor(WorkItem);
            //bsm = new BillSearchView(WorkItem);
            pom = new PaymentOfflineMonitor(WorkItem.Services.Get<IBillingService>(), this);

            base.Run();
        }

        protected virtual void AddServices()
        {
            WorkItem.Services.AddNew<BillingService, IBillingService>();
            WorkItem.Services.AddNew<PaidBillService, IPaidBillService>();
            WorkItem.Services.AddNew<ReportService, IReportService>();
        }

        private void ExtendMenu()
        {
            paymentCollectionMenuItem = new ToolStripMenuItem("2. �Ѻ�����Թ");

            

            WorkItem.UIExtensionSites[UIExtensionSiteNames.MainMenu].Add(paymentCollectionMenuItem);

            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.GeneralPaymentCollection, CommandNames.ElectricalUserPaymentCollection, Keys.Control | Keys.P);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.BillBookPaymentCollection, CommandNames.AgencyGroupInvoicingPaymentCollection, 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.PaymentCancellation, CommandNames.PaymentCancellation, 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.ReceiptReprinting, CommandNames.ReceiptReprinting, 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.InterestInquiry, CommandNames.InterestInquiry, 0);

            AddMenuSeparator(paymentCollectionMenuItem);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.QueueRequest, CommandNames.QueueRequest, Keys.Control | Keys.Q);

            AddMenuSeparator(paymentCollectionMenuItem);

            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC01, "CAC01", 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC05, "CAC05", 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC03, "CAC03", 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC04, "CAC04", 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC06, "CAC06", 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC07, "CAC07", 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC09, "CAC09", 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC11, "CAC11", 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC12, "CAC12", 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC13, "CAC13", 0);

            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC15, "CAC15", 0);

            // DCR QR Payment 2023-03 : Add menu of report 
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC18, "CAC18", 0);
            AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC19, "CAC19", 0);

            //TODO: INSTALLMENT CASE
            //AddMenuItem(paymentCollectionMenuItem, Properties.Resources.RpCAC16, "CAC16", 0);

            //AddMenuSeparator(paymentCollectionMenuItem);
            //AddMenuItem(paymentCollectionMenuItem, Properties.Resources.DEL_POS_TRANSACTION, "DEL_POS_TRANSACTION", 0);

            //AddMenuSeparator(paymentCollectionMenuItem);
            //AddMenuItem(paymentCollectionMenuItem, Properties.Resources.GenerateReceipt, CommandNames.GenerateReceipt, 0);

            //AddMenuSeparator(paymentCollectionMenuItem);

            //AddMenuItem(paymentCollectionMenuItem, "�Դ����Ѻ�����Թ��Ш��ѹ (�觢����š�Ѻ��ѧ SAP)", CommandNames.BranchDayClosing, 0);

            if (Session.Branch.Id == null)
            {
                paymentCollectionMenuItem.Enabled = false;
            }
        }

        private void ExtendToolStrip()
        {
            //bool enable = Session.Branch.Id != null;

            string paymentMenuText = "�Ѻ�����Թ - ����Ѻ�����俿��/�١˹������";
            UIExtensionSite toolstrip = WorkItem.UIExtensionSites[UIExtensionSiteNames.MainToolbar];
            posTSButton = AddToolStripButton(toolstrip, paymentMenuText, Properties.Resources.PaymentCollectionIcon,
                            paymentMenuText, CommandNames.ElectricalUserPaymentCollection, false);

            ToolStripSeparator separator = new ToolStripSeparator();
            toolstrip.Add(separator);
            //AddToolStripButton(toolstrip, Properties.Resources.PaymentOffline,
            //    Properties.Resources.PaymentOfflineIcon, CommandNames.PaymentOffline, enable);


            //Offline by User ,Begin

            string offlineMenuText = "Offline �¼����";
            UIExtensionSite offlineToolstrip = WorkItem.UIExtensionSites[UIExtensionSiteNames.MainToolbar];
            posOfflineButton = AddToolStripButton(offlineToolstrip, offlineMenuText, Properties.Resources.ForcedOfflineOffIcon,
                            offlineMenuText, CommandNames.ForcedOffline, true);


            //offlineToolstrip.Add(separator);

            //Offline by User ,End   
        }
        #endregion

        #region Command Handler

        [CommandHandler(CommandNames.ElectricalUserPaymentCollection)]
        public void ViewElectricalUserPaymentCollectionHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.POSObserver, false) ||
                Authorization.IsAuthorized(SecurityNames.PaymentCollection))
            {
                this.LoadUseCase<PaymentCollectionController>().Controller.Run(1);
            }
        }

        [CommandHandler(CommandNames.AgencyGroupInvoicingPaymentCollection)]
        public void ViewAgencyGroupInvoicingPaymentCollectionHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.POSObserver, false) ||
            Authorization.IsAuthorized(SecurityNames.AgencyGroupInvoicingPaymentCollection))
            {
                this.LoadUseCase<PaymentCollectionController>().Controller.Run(2);
            }
        }

        [CommandHandler(CommandNames.PaymentCancellation)]
        public void ViewPaymentCancellationHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.POSObserver, false) ||
                Authorization.IsAuthorized(SecurityNames.ReceiptCancellation, true))
            {
                this.LoadUseCase<PaymentCancellationController>().Controller.Run();
            }
        }

        [CommandHandler(CommandNames.ReceiptReprinting)]
        public void ViewReceiptReprintingHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.POSObserver, false) ||
                Authorization.IsAuthorized(SecurityNames.ReceiptReprinting, "�Ŵ��ͤ���� 2.4", true))
            {
                this.LoadUseCase<ReceiptReprintingController>().Controller.Run();
            }
        }

        [CommandHandler(CommandNames.InterestInquiry)]
        public void ViewInterestInquiryHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.POSObserver, false) ||
                Authorization.IsAuthorized(SecurityNames.InterestInquiry, true))
            {
                this.LoadUseCase<InterestInquiryController>().Controller.Run();
            }
        }

        [CommandHandler(CommandNames.QueueRequest)]
        public void QueueRequestHandler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.InterestInquiry, true))
            //{
            this.LoadUseCase<QueueRequestController>(false).Controller.Run();
            //}
        }

        [CommandHandler("CAC01")]
        public void ViewReportCAC01Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAC01))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC01_1);
            }
        }

        [CommandHandler("CAC03")]
        public void ViewReportCAC03Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAC03))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC03_1);
            }
        }

        [CommandHandler("CAC04")]
        public void ViewReportCAC04Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAC04, true))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC04_1);
            }
        }

        [CommandHandler("CAC05")]
        public void ViewReportCAC05Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAC05, true))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC05_1);
            }
        }

        [CommandHandler("CAC06")]
        public void ViewReportCAC06Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAC06, true))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC06_1);
            }
        }

        [CommandHandler("CAC07")]
        public void ViewReportCAC07Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAC07, true))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC07_1);
            }
        }

        [CommandHandler("CAC09")]
        public void ViewReportCAC09Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAC09, true))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC09_1);
            }
        }

        [CommandHandler("CAC10")]
        public void ViewReportCAC10Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAC10, true))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC10);
            }
        }

        [CommandHandler("CAC11")]
        public void ViewReportCAC11Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAC11, true))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC11);
            }
        }

        [CommandHandler("CAC12")]
        public void ViewReportCAC12Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAC12, true))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC12);
            }
        }

        [CommandHandler("CAC13")]
        public void ViewReportCAC13Handler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.CAC13, true))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC13);
            }
        }

        [CommandHandler("CAC14")]
        public void ViewReportCAC14Handler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.CAC14, true))
            //{
            this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC14);
            //}
        }

        [CommandHandler("CAC15")]
        public void ViewReportCAC15Handler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.CAC15, true))
            //{
            this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC15);
            //}
        }


        // DCR QR Payment 2023-03 : add menu report 2.18 QR Payment report.
        [CommandHandler("CAC18")]
        public void ViewReportCAC18Handler(object sender, EventArgs e)
        {
            this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC18);

            // Authorized ������͹�ѹ��§ҹ 2.8 
            if (Authorization.IsAuthorized(SecurityNames.CAC05, true))
            {
                this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC18);
            }
        }

        // DCR QR Payment 2023-03 : add menu report 2.19 QR Payment report.
        [CommandHandler("CAC19")]
        public void ViewReportCAC19Handler(object sender, EventArgs e)
        {
            // Authorized ������͹�ѹ��§ҹ 2.8 
            if (Authorization.IsAuthorized(SecurityNames.CAC05, true))
            {
                this.LoadUseCase<ReportBankQRPaymentController>().Controller.Run();
            }
        }


        //TODO: INSTALLMENT CASE
        //[CommandHandler("CAC16")]
        //public void ViewReportCAC16Handler(object sender, EventArgs e)
        //{
        //    //if (Authorization.IsAuthorized(SecurityNames.CAC16, true))
        //    //{
        //    this.LoadUseCase<ReportController>().Controller.Run(ReportName.CAC16);
        //    //}
        //}

        [CommandHandler("DEL_POS_TRANSACTION")]
        public void Del_Pos_TransactionHandler(object sender, EventArgs e)
        {
            this.LoadUseCase<PaymentCollectionController>().Controller.Run(3);
        }

        [CommandHandler(CommandNames.BranchDayClosing)]
        public void BranchDayClosingHandler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.InterestInquiry, true))
            //{
            this.LoadUseCase<BranchDayClosingController>().Controller.Run();
            //}
        }

        [CommandHandler(CommandNames.GenerateReceipt)]
        public void ViewPaymentNonReceiptHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.GenerateReceipt))
            {
                this.LoadUseCase<PaymentCollectionController>().Controller.Run(3);
            }
        }


        //Offline by User ,Begin
        private bool GetTimeAndCheckIsNetworkOnline()
        {
            ICommonService commonservice = ServerConnectionMonitor.Instant.GetCommonService();
            int retrycount = 0;
        getservertimeandcheckonline:
            try
            {
                //-- ��ҹ������Ҩҡ server ���������� 㹢�����ǡѹ��Ѻ���Ҵ� response ������繤�� latency �ͧ network Ẻ������
                Stopwatch sw = Stopwatch.StartNew();
                DateTime dt = commonservice.GetServerTime();
                sw.Stop();
            }
            catch
            {
                retrycount++;
                if (retrycount < 2)
                {
                    Thread.Sleep(1000);
                    goto getservertimeandcheckonline;
                }

                return false;
            }
            return true;
        }

        [CommandHandler(CommandNames.ForcedOffline)]
        public void ViewForcedOfflineHandler(object sender, EventArgs e)  //������ Offline �¼����
        {
            posOfflineButton.Enabled = false;

            if (forcedOfflineStatus == true) //switch on/off 
                forcedOfflineStatus = false; //off  ���ѧ�Ѻ  Offline
            else
                forcedOfflineStatus = true;  //on �ѧ�Ѻ Offline


            if (forcedOfflineStatus == true)  //on �ѧ�Ѻ Offline 
            {
                //Change Icon
                posOfflineButton.Image = Properties.Resources.ForcedOfflineOnIcon;

                Session.IsNetworkConnectionAvailable = false;
                Session.IsForcedOffline = true;
            }
            else  //off  ���ѧ�Ѻ  Offline
            {
                //Change Icon
                posOfflineButton.Image = Properties.Resources.ForcedOfflineOffIcon;

                //��ͧ���� Code �� Network �ա�ͺ
                if (GetTimeAndCheckIsNetworkOnline())
                    Session.IsNetworkConnectionAvailable = true;

                Session.IsForcedOffline = false;
            }

            posOfflineButton.Enabled = true;
        }
        //Offline by User ,End

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

        #region Event Handling


        [EventSubscription(EventTopicNames.ProcessOfflineFile, Thread = ThreadOption.UserInterface)]
        public void ProcessOfflineFileHandler(object sender, EventArgs e)
        {
            pom.CheckFileNow(false);
        }

        [EventSubscription(EventTopicNames.OnlineStatus, Thread = ThreadOption.UserInterface)]
        public void OnlineStatusHandler(object sender, EventArgs<bool> e)
        {
            if (e.Data)
            {
                pom.CheckFileNow(true);
                paymentCollectionMenuItem.DropDownItems[1].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[2].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[3].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[4].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[5].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[6].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[7].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[8].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[9].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[10].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[11].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[12].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[13].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[14].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[15].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[16].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[17].Enabled = true;
            }
            else
            {
                posTSButton.Enabled = true;
                paymentCollectionMenuItem.DropDownItems[0].Enabled = true;
                paymentCollectionMenuItem.DropDownItems[1].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[2].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[3].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[4].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[5].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[6].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[7].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[8].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[9].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[10].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[11].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[12].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[13].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[14].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[15].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[16].Enabled = false;
                paymentCollectionMenuItem.DropDownItems[17].Enabled = false;

            }
        }


        [EventSubscription(Constants.EventTopicNames.ClosePaymentView, Thread = ThreadOption.UserInterface)]
        public void ClosePaymentViewHandler(object sender, EventArgs e)
        {
            this.ClearExistingUseCase();
        }


        [EventSubscription(EventTopicNames.TestPrintPOSReceipt, Thread = ThreadOption.UserInterface)]
        public void TestPrintPOSReceipt(object sender, EventArgs<List<PrintingInfo>> e)
        {
            try
            {
                List<PrintingInfo> receipts = e.Data;
                SlipPrinting sp = new SlipPrinting();
                foreach (PrintingInfo r in receipts)
                {
                    sp.Print(r.PrintingReceipt, r.PaymentMethods, sp.GetAllReceiptsNo(r.ReceiptStatus));

                }
            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.POS, ex);
                //MessageBox.Show(ex.ToString(), "�Դ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
