using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.CashManagementModule.Interface.Constants;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.CashManagementModule.CashManagementUtilities;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.Services;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.SmartParts;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using System.IO;
using PEA.BPM.PaymentCollectionModule.Views.PaymentMethodSelectionView;
using PEA.BPM.PaymentCollectionModule.Interface.Services;

namespace PEA.BPM.CashManagementModule
{
    public class ModuleController : WorkItemController, IDisposable
    {
        ICashManagementServices _service;
        ICashReportServices _reportService;
        List<CashierWorkStatusInfo> openWorkList;
        ControlledWorkItem<ReportController> _reportController;
        ToolStripMenuItem cashManagementMenuItem;
        ToolStripButton _openWorkButton;
        ToolStripButton _closeWorkButton;
        ToolStripSeparator _separatorRight;
        bool _firstTimeStarted = true;
        WorkState _workState = WorkState.Hide;
        PaymentOfflineMonitor pom;

        enum WorkState { Show, Hide };

        private System.Timers.Timer _timerRefresh;
        private int counterRefreshDefault = 30;
        private int counterRefresh = 30;
                    
         
        #region Run
        public override void Run()
        {
            AddServices();
            ExtendMenu();
            ExtendToolStrip();
            LoadReportController();

            _timerRefresh = new System.Timers.Timer(10);
            _timerRefresh.AutoReset = true;
            _timerRefresh.Elapsed += new System.Timers.ElapsedEventHandler(OnLoad_TimerRefresh);
            //_timerRefresh.Start();

            base.Run();
        }


        private void OnLoad_TimerRefresh(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timerRefresh.Interval = 1000;
            if (counterRefresh > 0)
            {
                --counterRefresh;
                //_closeWorkButton.Text = counterRefresh.ToString();
                _timerRefresh.Start();
            }
            else //(counterRefreshBt <= 0)
            {
                _timerRefresh.Stop();
                _timerRefresh.Interval = 10;
                //UpdateMenu();

                if (!string.IsNullOrEmpty(Session.Work.Id))
                {
                    _openWorkButton.Enabled = true;
                    _closeWorkButton.Enabled = true;
                
                }
                
            }
        }

        protected virtual void AddServices()
        {
            WorkItem.Services.AddNew<CashManagementServices, ICashManagementServices>();
            WorkItem.Services.AddNew<CashManagementServices, ICashReportServices>();
        }

        private void ExtendMenu()
        {
            cashManagementMenuItem = new ToolStripMenuItem(Properties.Resources.CashManagementMenu);
            WorkItem.UIExtensionSites[UIExtensionSiteNames.MainMenu].Add(cashManagementMenuItem);

            AddMenuItem(cashManagementMenuItem, Properties.Resources.OpenWorkMenuTitle, CommandNames.OpenWork, Keys.Control | Keys.O);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.TransferManagementMenuTitle, CommandNames.TransferManagement, 0);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.CancelBankDeliveryMenuTitle, CommandNames.CancelBankDelivery, 0);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.TransferStatusMonitorMenuTitle, CommandNames.TransferStatusMonitor, 0);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.TransferOutStatusMenuTitle, CommandNames.TransferOutStatus, 0);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.CashCheckingInMenuTitle, CommandNames.CashCheckingIn, 0);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.CancelCashCheckingInMenuTitle, CommandNames.CancelCashCheckingIn, 0);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.CloseWorkMenuTitle, CommandNames.CloseWork, Keys.Control | Keys.S);
            AddMenuSeparator(cashManagementMenuItem);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.ForceCloseWorkMenuTitle, CommandNames.ForceCloseWork, 0);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.DailyRemainReportMenuTitle, CommandNames.DailyRemainReportCommand, 0);
            AddMenuSeparator(cashManagementMenuItem);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.PayInDailyReportMenuTitle, CommandNames.PayInDailyReportCommand, 0);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.MoneyFlowReportMenuTitle, CommandNames.MoneyFlowReportCommand, 0);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.SummaryCloseWorkReportMenuTitle, CommandNames.SummaryCloseWorkCommand, 0);
            AddMenuSeparator(cashManagementMenuItem);
            AddMenuItem(cashManagementMenuItem, Properties.Resources.AdjustOpeningBalance, CommandNames.AdjustOpeningBalance, 0);
            AddMenuItem(cashManagementMenuItem, "", CommandNames.SystemInitial, Keys.F9);
            SetVisible(CommandNames.SystemInitial, false);
            AddMenuItem(cashManagementMenuItem, "", CommandNames.WorkIdVisible, Keys.F10);
            SetVisible(CommandNames.WorkIdVisible, false);
            //AddMenuSeparator(cashManagementMenuItem);

            if (Session.Branch.Id == null )
                cashManagementMenuItem.Enabled = false;
        }

        private void ExtendToolStrip()
        {
            UIExtensionSite toolstrip = WorkItem.UIExtensionSites[UIExtensionSiteNames.MainToolbar];
            _separatorRight = new ToolStripSeparator();
            string openWorkTitle = "เริ่มต้นกะใหม่";
            string closeWorkTitle = "สรุปยอดเงินคงเหลือ (ปิดกะ)";
            _openWorkButton = AddToolStripButton(toolstrip, openWorkTitle, Properties.Resources.PlayHS, openWorkTitle, CommandNames.OpenWork);
            _closeWorkButton = AddToolStripButton(toolstrip, closeWorkTitle, Properties.Resources.RecordHS, closeWorkTitle, CommandNames.CloseWork);
            toolstrip.Add(_separatorRight);
                
            _openWorkButton.Visible = true;
            _openWorkButton.Enabled = false;
            _closeWorkButton.Enabled = false;
            _closeWorkButton.Visible = false;
        }

        #endregion

        #region Custom Function

        private void LoadReportController()
        {
            _reportController = WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportController>>();
            _reportController.Controller.Run();
        }

        private void GetCashierWorkStatus()
        {
            try
            {
                //if offline, disable cashmenagement and then leave other module be the same
                if (!Session.IsNetworkConnectionAvailable || Session.Branch.Id == null || Session.User.Id == null)
                    return;

                //connection can be online/offline while working so notify user only once
                OpenWorkParam param = new OpenWorkParam();
                param.BranchId = Session.Branch.Id;
                param.PosId = Session.Terminal.Id; //not lock at all
                param.CashierId = Session.User.Id;

                _service = WorkItem.Services.Get<ICashManagementServices>();

                //Important! what this returns
                //Return list of work information, it must contain flag to say system starting of the system.
                openWorkList = _service.IsOpenedWork(param); 

                //opened work across the day?
                if (openWorkList.Count > 0 && _firstTimeStarted)
                {
                    DateTime dt = openWorkList[0].OpenWorkDt.Value;
                    string currentCashierId = openWorkList[0].CashierId;
                    
                    if (currentCashierId == Session.User.Id && dt.Day != Session.BpmDateTime.Now.Day)
                        MessageBox.Show("คุณเปิดกะข้ามวัน! กรุณาตรวจสอบเงินในลิ้นชัก\nกดปุ่ม OK เพื่อเริ่มต้นทำงาน", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    _firstTimeStarted = false;

                    
                }

                //offline and then online -> close pos screen
                if(openWorkList.Count == 0)
                    CloseAllViews();

                //menu control disabled/enabled
                UpdateMenu();

                
            }
            catch (Exception ex)
            {
                EnableCashierMenu(false);
                EnablePOSMenu(false);
                MessageBox.Show(ex.ToString());
            }
        }

        private void OpenInquireyPOSMenu()
        {
            EnableMenuItem(PEA.BPM.PaymentManagementModule.Interface.Constants.CommandNames.PaymentVoucherCancellation);
            EnableMenuItem(PEA.BPM.PaymentManagementModule.Interface.Constants.CommandNames.AccountPayablePayment);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.AgencyGroupInvoicingPaymentCollection);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.BranchDayClosing);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.CashPaymentType);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.ChequePaymentType);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.DepositPaymentType);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.ElectricalUserPaymentCollection);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.InterestInquiry);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.OtherDebtPaymentCollection);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.PaymentCancellation);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.PaymentOffline);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.RepaymentCancellation);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.ReceiptReprinting);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.QueueRequest);
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.GenerateReceipt);
        }

        private void EnablePOSMenu(bool enable)
        {
            if (enable || Authorization.IsAuthorized(PEA.BPM.PaymentCollectionModule.Interface.Constants.SecurityNames.POSObserver, false))
            {
                EnableMenuItem(PEA.BPM.PaymentManagementModule.Interface.Constants.CommandNames.PaymentVoucherCancellation);
                EnableMenuItem(PEA.BPM.PaymentManagementModule.Interface.Constants.CommandNames.AccountPayablePayment);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.AgencyGroupInvoicingPaymentCollection);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.BranchDayClosing);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.CashPaymentType);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.ChequePaymentType);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.DepositPaymentType);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.ElectricalUserPaymentCollection);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.InterestInquiry);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.OtherDebtPaymentCollection);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.PaymentCancellation);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.PaymentOffline);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.RepaymentCancellation);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.ReceiptReprinting);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.QueueRequest);
                EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.GenerateReceipt);
            }
            else
            {
                DisableMenuItem(PEA.BPM.PaymentManagementModule.Interface.Constants.CommandNames.PaymentVoucherCancellation);
                DisableMenuItem(PEA.BPM.PaymentManagementModule.Interface.Constants.CommandNames.AccountPayablePayment);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.AgencyGroupInvoicingPaymentCollection);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.BranchDayClosing);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.CashPaymentType);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.ChequePaymentType);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.DepositPaymentType);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.ElectricalUserPaymentCollection);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.InterestInquiry);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.OtherDebtPaymentCollection);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.PaymentCancellation);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.PaymentOffline);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.RepaymentCancellation);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.ReceiptReprinting);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.QueueRequest);
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.GenerateReceipt);
            }
        }

        private void EnableCashierMenu(bool enable)
        {
            if (enable)
            {
                EnableMenuItem(CommandNames.CancelCashCheckingIn);
                EnableMenuItem(CommandNames.CashCheckingIn);
                EnableMenuItem(CommandNames.CloseWork);
                EnableMenuItem(CommandNames.TransferManagement);
                EnableMenuItem(CommandNames.TransferStatusMonitor);
                EnableMenuItem(CommandNames.TransferOutStatus);
                EnableMenuItem(CommandNames.CancelBankDelivery);
               
            }
            else
            {
                DisableMenuItem(CommandNames.CancelCashCheckingIn);
                DisableMenuItem(CommandNames.CashCheckingIn);
                DisableMenuItem(CommandNames.CloseWork);
                DisableMenuItem(CommandNames.TransferManagement);
                DisableMenuItem(CommandNames.TransferStatusMonitor);
                DisableMenuItem(CommandNames.TransferOutStatus);
                DisableMenuItem(CommandNames.CancelBankDelivery);
                DisableMenuItem(CommandNames.ForceCloseWork);

                if (Authorization.IsAuthorized(SecurityNames.DailyRemainReport, false))
                    EnableMenuItem(CommandNames.DailyRemainReportCommand);
                else
                    DisableMenuItem(CommandNames.DailyRemainReportCommand);
               
            }
        }

        private void CleanCashierTS()
        {
            UIExtensionSite toolstrip = WorkItem.UIExtensionSites[UIExtensionSiteNames.MainToolbar];

            if (toolstrip.Contains(_separatorRight))
                toolstrip.Remove(_separatorRight);

            if (toolstrip.Contains(_openWorkButton))
                RemoveToolStripButton(toolstrip, _openWorkButton, CommandNames.OpenWork);

            if (toolstrip.Contains(_closeWorkButton))
                RemoveToolStripButton(toolstrip, _closeWorkButton, CommandNames.CloseWork);

        }

        private void UpdateMenu()
        {
            if (!Authorization.IsAuthorized(SecurityNames.AdjustOpenBalance, false))
                DisableMenuItem(CommandNames.AdjustOpeningBalance);

            //if no permission on POS at all so, disable all things
            if (!Authorization.IsAuthorized(PEA.BPM.PaymentCollectionModule.Interface.Constants.SecurityNames.PaymentCollection, false))
            {
                EnablePOSMenu(false);
                EnableCashierMenu(false);
                DisableMenuItem(CommandNames.OpenWork);
                //disable icon
                _openWorkButton.Visible = true;
                _closeWorkButton.Visible = false;
                _openWorkButton.Enabled = false;

                if (Authorization.IsAuthorized(PEA.BPM.PaymentCollectionModule.Interface.Constants.SecurityNames.POSObserver, false))
                    EnableMenuItem(CommandNames.ForceCloseWork);

                return;
            }

            ////found work
            if (openWorkList.Count != 0)
            {
                if (openWorkList[0].CashierId == Session.User.Id)
                {
                    if (openWorkList[0].WorkId != null)
                    {
                        //enable cashier menu
                        EnableCashierMenu(true);

                        //enable POS menu
                        EnablePOSMenu(true);

                        //disable OpenWork Menu
                        DisableMenuItem(CommandNames.OpenWork);

                        //enable/disable special function
                        if (Authorization.IsAuthorized(SecurityNames.ForceCloseWork, false) ||
                            Authorization.IsAuthorized(PEA.BPM.PaymentCollectionModule.Interface.Constants.SecurityNames.POSObserver, false))
                            EnableMenuItem(CommandNames.ForceCloseWork);
                        else
                            DisableMenuItem(CommandNames.ForceCloseWork);

                        if (Authorization.IsAuthorized(SecurityNames.DailyRemainReport, false))
                            EnableMenuItem(CommandNames.DailyRemainReportCommand);
                        else
                            DisableMenuItem(CommandNames.DailyRemainReportCommand);

                        //MessageBox.Show("เปิดกะแล้ว");

                        Session.Work.Id = openWorkList[0].WorkId;
                        Session.Work.OpenWorkDt = openWorkList[0].OpenWorkDt.Value;
                        Session.Work.DayCount = openWorkList[0].DayCount.Value;
                        //set title bar text
                        SetWindowsTitle(string.Format("PEA - Bill Printing & Payment Collection Management - กำลังทำงานกะที่ ({0})", 
                            Session.Work.DayCount==0? "ข้ามวัน": Session.Work.DayCount.ToString()));
                        
                        //if(_firstTimeStarted)
                            CheckIncommingTransfer();

                        //now open already, show close work dialog
                        if (Session.Branch.Id != null)
                        {
                            _closeWorkButton.Visible = true;
                            _closeWorkButton.Enabled = true;
                            _openWorkButton.Visible = false;
                        }
                    }
                }
                else
                {
                    //Modified By Tong on 22/09/2008 following the requirement from key user to un-fix user with terminal
                    //check for the other posId
                    OpenWorkParam param = new OpenWorkParam();
                    param.BranchId = Session.Branch.Id;
                    param.CashierId = Session.User.Id;
                    param.PosId = Session.Terminal.Id;

                    _service = WorkItem.Services.Get<ICashManagementServices>();
                    openWorkList = _service.IsOpenedWork(param); //Nobody opened work @ this branch/posId, you can open it...or not.

                    //another cashier would like to use this POS machine
                    if (openWorkList.Count == 0)
                    {
                        //disable cashier menu (leave report & OpenWork menu)
                        EnableCashierMenu(false);
                        EnableMenuItem(CommandNames.OpenWork);

                        //disable POS menu
                        //Added, July 28 allow inquiry for POS users
                        OpenInquireyPOSMenu();

                        //show shotcut
                        if (Session.Branch.Id != null)
                        {
                            _openWorkButton.Visible = true;
                            _openWorkButton.Enabled = true;
                            _closeWorkButton.Visible = false;
                        }
                    }
                   
                }
            }
            else //not found open work in this posId
            {
                //check for the other posId
                OpenWorkParam param = new OpenWorkParam();
                param.BranchId = Session.Branch.Id;
                param.CashierId = Session.User.Id;
                param.PosId = Session.Terminal.Id;

                _service = WorkItem.Services.Get<ICashManagementServices>();
                openWorkList = _service.IsOpenedWork(param); //Nobody opened work @ this branch/posId, you can open it...or not.

                //another cashier would like to use this POS machine
                if (openWorkList.Count == 0)
                {
                    //disable cashier menu (leave report & OpenWork menu)
                    EnableCashierMenu(false);
                    EnableMenuItem(CommandNames.OpenWork);

                    //disable POS menu
                    //Added, July 28 allow inquiry for POS users
                    OpenInquireyPOSMenu();

                    //show shotcut
                    if (Session.Branch.Id != null)
                    {
                        _openWorkButton.Visible = true;
                        _openWorkButton.Enabled = true;
                        _closeWorkButton.Visible = false;
                    }
                }
            }
        }

        private bool CanOpenWork()
        {
            if (openWorkList.Count == 0) // no one has opened work
                return true;
            else // someone has opened work
                return false; 
            
        }

        #endregion
        
        #region Command Handler

        [CommandHandler(CommandNames.TransferManagement)]
        public void TransferManagementHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.TransferManagement, true))
                this.LoadUseCase<MoneyTransferManagementController>().Controller.Run();
        }

        [CommandHandler(CommandNames.OpenWork)]
        public void OpenWorkHandler(object sender, EventArgs e)
        {
            //PaymentOfflineMonitor pom = new PaymentOfflineMonitor(WorkItem.Services.Get<IBillingService>(), this);
            
            //use POS permission
            if (CanOpenWork())
            {
                if (Session.IsNetworkConnectionAvailable && Authorization.ConfirmPassword()) 
                //if (pom.ConfirmGetOfflineFile() && Session.IsNetworkConnectionAvailable && Authorization.ConfirmPassword())  //Offline by User
                {
                    //only main cashier

                    if (Authorization.IsAuthorized(SecurityNames.ForceCloseWork, false))
                    {
                        OpenWorkParam param = new OpenWorkParam();
                        param.BranchId = Session.Branch.Id;
                        param.CashierId = Session.User.Id;
                        param.CashierName = Session.User.Name;
                        param.PosId = Session.Terminal.Id;
                        param.TerminalCode = Session.Terminal.Code;
                        param.ModifiedBy = Session.User.Id;                        
                        bool init = _service.IsSystemInitial(Session.Branch.Id, Session.Work.Id);
                        if (init)
                        {
                            param.IsSystemInit = true;  //no need to find opening balance
                            Session.Work.Id = _service.OpenWork(param);
                            StartOpenBalance();
                            GetCashierWorkStatus();
                            CheckIncommingTransfer();
                            

                            ////เมื่อเปิดกะ และได้ work id แล้วให้ทำการ Sync Offline ไฟล์
                            //pom.CheckFileNow(false); // Offline by User  Check Offline file
                        
                        }
                        else
                            this.LoadUseCase<OpenWorkItemController>().Controller.Run();
                    }
                    else
                    {
                        this.LoadUseCase<OpenWorkItemController>().Controller.Run();
                    }

                    try
                    {
                        //sure! this is connecting
                        if (!Session.Branch.OnlineConnection)
                            Authorization.SignalSyncup("DL091_UPLOAD_CASH_MANAGEMENT_BATCH");
                    }
                    catch (Exception)
                    {
                        //ignored
                    }
                }
            }
            else // can not open work
            {
                MessageBox.Show("มีการเปิดกะอยู่ที่เครื่องนี้แล้ว ต้องทำการปิดกะก่อนจึงจะเริ่มกะใหม่ได้", "ผิดพลาด");
            }
        }

        [CommandHandler(CommandNames.CloseWork)]
        public void CloseWorkHandler(object sender, EventArgs e)
        {
            //ถ้าปิดปุ่มก็เข้าใช้งานไม่ได้  บล็อคเพิ่ม เพื่อป้อนกันการใช้ Short Key
            if (cashManagementMenuItem.Enabled == false) return;

            PaymentOfflineInterface poi = new PaymentOfflineInterface(WorkItem.Services.Get<IBillingService>());
            poi.OfflineFileWithOutWorkID();
            
            //use POS permission

            this.LoadUseCase<CloseWorkSummaryController>().Controller.Run();
            if (!String.IsNullOrEmpty(Session.Work.Id))
            {
                _openWorkButton.Enabled = false;
                _closeWorkButton.Enabled = false;
                counterRefresh = counterRefreshDefault;
                _timerRefresh.Start();
            }

        }

        [CommandHandler(CommandNames.ForceCloseWork)]
        public void ForceCloseWorkHandler(object sender, EventArgs e)
        {
            //controlled by enable/disable menu
            this.LoadUseCase<ForceCloseWorkController>().Controller.Run();
        }

        [CommandHandler(CommandNames.CashCheckingIn)]
        public void CashCheckInHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.MoneyCheckIn, true))
                this.LoadUseCase<CashCheckInController>().Controller.Run();
        }

        [CommandHandler(CommandNames.TransferStatusMonitor)]
        public void TransferStatusMonitorHandler(object sender, EventArgs e)
        {
            ControlledWorkItem<TransferResponseController> transRespWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<TransferResponseController>>();
            transRespWorkItem.Controller.Run();
            //this.LoadUseCase<TransferResponseController>().Controller.Run();
        }

        [CommandHandler(CommandNames.TransferOutStatus)]
        public void TransferOutStatusHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.TransferManagement, true))
            {
                //this.LoadUseCase<TransferStatusController>().Controller.Run();
                ControlledWorkItem<TransferStatusController> transStatusWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<TransferStatusController>>();
                transStatusWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.CancelBankDelivery)]
        public void CancelBankDeliveryHandler(object sender, EventArgs e)
        {
            this.LoadUseCase<CancelBankDeliveryController>().Controller.Run();
        }

        [CommandHandler(CommandNames.CancelCashCheckingIn)]
        public void CancelCashCheckingInHandler(object sender, EventArgs e)
        {
            this.LoadUseCase<CancelCashCheckInController>().Controller.Run();
        }

        [CommandHandler(CommandNames.AdjustOpeningBalance)]
        public void AdjustOpeningBalanceHandler(object sender, EventArgs e)
        {
            this.LoadUseCase<AdjustOpenBalanceController>().Controller.Run();
        }

        //report
        [CommandHandler(CommandNames.DailyRemainReportCommand)]
        public void DailyRemainReportHandler(object sender, EventArgs e)
        {
            this.LoadUseCase<ReportDailyRemainController>().Controller.Run();
        }

        //report
        [CommandHandler(CommandNames.MoneyFlowReportCommand)]
        public void MoneyFlowReportHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.MoneyFlowReport, true))
            {
                ControlledWorkItem<ReportMoneyFlowController> reportMoneyFlowWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportMoneyFlowController>>();
                reportMoneyFlowWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.SummaryCloseWorkCommand)]
        public void SummaryCloseWorkHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.SummaryCloseWork, true))
            {
                ControlledWorkItem<ReportSumCloseWorkController> reportSumCloseWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportSumCloseWorkController>>();
                reportSumCloseWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.PayInDailyReportCommand)]
        public void PayInDailyReportHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.DailyPayInReport, true))
            {
                ControlledWorkItem<ReportDailyPayInController> reportDailyPayInWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportDailyPayInController>>();
                reportDailyPayInWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.SystemInitial)]
        public void SystemInitialHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.ForceCloseWork, false) && Session.Work.Id != null)  //already open work
            {
                //OpenWorkParam param = new OpenWorkParam();
                //param.BranchId = Session.Branch.Id;
                //param.CashierId = Session.User.Id;
                //param.PosId = Session.Terminal.Id;
                //param.ModifiedBy = Session.User.Id;
                //param.IsSystemInit = true;  //no need to find opening balance
                bool init = _service.IsSystemInitial(Session.Branch.Id, Session.Work.Id);

                if (init)
                {
                    //Session.Work.Id = _service.OpenWork(param);
                    StartOpenBalance();
                }
            }
        }

        [CommandHandler(CommandNames.WorkIdVisible)]
        public void WorkIdVisibleHandler(object sender, EventArgs e)
        {
            if (_workState == WorkState.Hide)
            {
                WorkVisible(true);
                _workState = WorkState.Show;
            }
            else
            {
                WorkVisible(false);
                _workState = WorkState.Hide;
            }
        }

        public void StartOpenBalance()
        {
            //only main cashier can use this - do not show any dialog when no permission
            //if (Authorization.IsAuthorized(SecurityNames.StartOpenBalance, false))
                this.LoadUseCase<StartOpenBalanceController>().Controller.Run();
        }

        #endregion

        #region Event Handler

        [EventPublication(EventTopicNames.ShowCurrentWork, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShowCurrentWorkHandler;
        public void WorkVisible(bool visible)
        {
            if (ShowCurrentWorkHandler != null)
                ShowCurrentWorkHandler(this, new EventArgs<bool>(visible));
        }

        //Refresh work when invalide workId identified
        [EventSubscription(EventTopicNames.CashierOpenWork, Thread = ThreadOption.UserInterface)]
        public void CashierOpenWorkHandler(object sender, EventArgs<string> e)
        {
            GetCashierWorkStatus();
        }

        //Enable Tray Menu
        [EventSubscription(EventTopicNames.EnableTrayMenu, Thread = ThreadOption.UserInterface)]
        public void EnableTrayMenuHandler(object sender, EventArgs<bool> e)
        {
            //MessageBox.Show("Subscription-EnableTrayMenu");

            //if ((bool)e.Data == false)
            //{
            //    _openWorkButton.Enabled = false;
            //    _closeWorkButton.Enabled = false;
            //}
            //else
            //{
            //    _openWorkButton.Enabled = true;
            //    _closeWorkButton.Enabled = true;
            //}


            if ((bool)e.Data == false)
            {
                _openWorkButton.Enabled = false;
                _closeWorkButton.Enabled = false;
                counterRefresh = counterRefreshDefault;
                _timerRefresh.Start();
            }
        }

        //Disable POS menu and Cashier menu after closework
        [EventSubscription(EventTopicNames.RenewWork, Thread = ThreadOption.UserInterface)]
        public void RenewWorkHandler(object sender, EventArgs e)
        {
            GetCashierWorkStatus();
        }

        //start open work
        [EventSubscription(EventTopicNames.StartWork, Thread = ThreadOption.UserInterface)]
        public void StartWorkHandler(object sender, EventArgs e)
        {
            GetCashierWorkStatus();
            CheckIncommingTransfer();
            EnablePOSButton(true);
        }

        [EventPublication(EventTopicNames.EnablePOSSaveButton, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> EnablePOSSaveButtonHandler;
        public void EnablePOSButton(bool enable)
        {
            if (EnablePOSSaveButtonHandler != null)
                EnablePOSSaveButtonHandler(this, new EventArgs<bool>(enable));
        }


        [EventSubscription(EventTopicNames.FinishOfflineUpload, Thread = ThreadOption.UserInterface)]
        public void FinishOfflineUpload(object sender, EventArgs e)
        {
            cashManagementMenuItem.Enabled = true;
            GetCashierWorkStatus();
            EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.GenerateReceipt);
        }

        [EventSubscription(EventTopicNames.OnlineStatus, Thread = ThreadOption.UserInterface)]
        public void OnlineStatusHandler(object sender, EventArgs<bool> e)
        {
            if (!e.Data)
            {
                cashManagementMenuItem.Enabled = false;
                _openWorkButton.Enabled = false;
                _closeWorkButton.Enabled = false;
                DisableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.GenerateReceipt);
            }
            else
            {
                if (Authorization.IsAuthorized(SecurityNames.PaymentCollection, false))
                {
                    cashManagementMenuItem.Enabled = true;
                    _openWorkButton.Enabled = true;
                    _closeWorkButton.Enabled = true;
                    EnableMenuItem(PEA.BPM.PaymentCollectionModule.Interface.Constants.CommandNames.GenerateReceipt);
                }
            }
        }

        private void CheckIncommingTransfer()
        {
            //check incomming transfer 
            ControlledWorkItem<TransferResponseController> transferResponse;
            transferResponse = WorkItem.WorkItems.AddNew<ControlledWorkItem<TransferResponseController>>();
            transferResponse.Controller.ShowDialogResult(false);
            transferResponse.Controller.Run();
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


