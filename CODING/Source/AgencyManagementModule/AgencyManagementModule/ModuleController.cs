using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure;
using PEA.BPM.AgencyManagementModule.ReportsController;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Views;
using PEA.BPM.AgencyManagementModule.ReportViews;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.AgencyManagementModule.Services;
using System.Collections;
using PEA.BPM.Architecture.CommonUtilities;


namespace PEA.BPM.AgencyManagementModule
{
    public class ModuleController : WorkItemController, IDisposable
    {
        private string _runningBranch = Session.Branch.Id;
        private string _runningBranchName = Session.Branch.Name;
        private ControlledWorkItem<BillBookController> _billBookWorkItem;
        private IUserHintView _userHint;
        private WindowSmartPartInfo _modalProperty;
        private ToolStripMenuItem _agencyManagementTSMenuItem;

        public string RunningBranchId
        {
            set { _runningBranch = value; }
            get { return _runningBranch; }
        }

        public string RunningBranchName
        {
            get { return this._runningBranchName; }
            set { this._runningBranchName = value; }
        }

        public ModuleController()
        {
            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.MaximizeBox = false;
            _modalProperty.MinimizeBox = false;
            _modalProperty.Modal = true;
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
        }

        #region Run
        public override void Run()
        {
            AddServices();
            ExtendMenu();
            ExtendToolStrip();
            LoadReportController();
        }

        protected virtual void AddServices()
        {
            WorkItem.Services.AddNew<AgencyCommonServiceSwitcher, IAgencyCommonService>();
            WorkItem.Services.AddNew<AgencyPlanningServiceSwitcher, IAgencyPlanningService>();
            WorkItem.Services.AddNew<CreateBillbookServiceSwitcher, ICreateBillbookService>();
            WorkItem.Services.AddNew<BillbookCheckInServiceSwitcher, IBillbookCheckInService>();
            WorkItem.Services.AddNew<ReportMgtServiceSwitcher, IReportMgtService>();
            WorkItem.Services.AddNew<CommissionMgtServiceSwitcher, ICommissionMgtService>();          
            WorkItem.Services.AddNew<AgencyModuleConfigServiceSwitcher, IAgencyModuleConfigService>(); 
    
        }

        private void ExtendMenu()
        {
            _agencyManagementTSMenuItem = new ToolStripMenuItem(Properties.Resources.AgencyManagement);
            WorkItem.UIExtensionSites[UIExtensionSiteNames.MainMenu].Add(_agencyManagementTSMenuItem);

            //Add use cases
            ToolStripMenuItem agencyPlanningTSMenuItem = new ToolStripMenuItem(Properties.Resources.AgentPlanning);
            agencyPlanningTSMenuItem.Name = "AgencyPlanningTSMenuItem";
            agencyPlanningTSMenuItem.Size = new System.Drawing.Size(152, 22);

            ToolStripMenuItem billBookTSMenuItem = new ToolStripMenuItem(Properties.Resources.BillBook);
            billBookTSMenuItem.Name = "BillBookTSMenuItem";
            billBookTSMenuItem.Size = new System.Drawing.Size(152, 22);

            ToolStripMenuItem advancePaymentTSMenuItem = new ToolStripMenuItem(Properties.Resources.AdvancePayment);
            advancePaymentTSMenuItem.Name = "AdvancePaymentTSMenuItem";
            advancePaymentTSMenuItem.Size = new System.Drawing.Size(152, 22);

            ToolStripMenuItem billBookPaymentTSMenuItem = new ToolStripMenuItem(Properties.Resources.BillBookCheckIn);
            billBookPaymentTSMenuItem.Name = "BillBookPaymentTSMenuItem";
            billBookPaymentTSMenuItem.Size = new System.Drawing.Size(152, 22);

            ToolStripMenuItem commissionManagementTSMenuItem = new ToolStripMenuItem(Properties.Resources.CommissionManagement);
            commissionManagementTSMenuItem.Name = "CommissionManagementTSMenuItem";
            commissionManagementTSMenuItem.Size = new System.Drawing.Size(152, 22);

            ToolStripMenuItem dataManagementTSMenuItem = new ToolStripMenuItem(Properties.Resources.BillBookStatus);
            dataManagementTSMenuItem.Name = "DataManagementTSMenuItem";
            dataManagementTSMenuItem.Size = new System.Drawing.Size(152, 22);

            ToolStripMenuItem criteriaManagementTSMenuItem = new ToolStripMenuItem(Properties.Resources.RadialChartHS);
            criteriaManagementTSMenuItem.Name = "CriteriaManagementTSMenuItem";
            criteriaManagementTSMenuItem.Size = new System.Drawing.Size(152, 22);

             //add submenu for main menu strip item of module
            AddMenuItem(_agencyManagementTSMenuItem, Properties.Resources.AgentPlanning, CommandNames.AgentPlanning, 0);
            AddMenuItem(_agencyManagementTSMenuItem, Properties.Resources.BillBook, CommandNames.BillBook, 0);
            //AddMenuItem(agencyManagementTSMenuItem, Properties.Resources.AdvancePayment, CommandNames.AdvancePayment, 0);
            AddMenuItem(_agencyManagementTSMenuItem, Properties.Resources.BillBookCheckIn, CommandNames.BillBookCheckIn, 0);
            AddMenuItem(_agencyManagementTSMenuItem, Properties.Resources.CommissionManagement, CommandNames.CommissionManagement, 0);
            AddMenuItem(_agencyManagementTSMenuItem, Properties.Resources.BillBookStatus, CommandNames.BillBookStatus, 0);


            AddMenuSeparator(_agencyManagementTSMenuItem);

            ToolStripMenuItem reportMenuItem = AddGroupMenu(_agencyManagementTSMenuItem, Properties.Resources.Report);
            AddMenuItem(reportMenuItem, Properties.Resources.ReportAgencyMoneyReturnVerification, CommandNames.ReportAgencyMoneyReturnVerification, 0);
            //AddMenuItem(reportMenuItem, Properties.Resources.ReportAgencyBillCollectorCondition, CommandNames.ReportAgencyBillCollectorCondition, 0);
            AddMenuItem(reportMenuItem, Properties.Resources.ReportAgencyPlanKeepElectric, CommandNames.ReportAgencyPlanKeepElectric, 0);
            AddMenuItem(reportMenuItem, Properties.Resources.ReportEvaluateAgencyAboutKeepElectric, CommandNames.ReportEvaluateAgencyAboutKeepElectric, 0);
            AddMenuItem(reportMenuItem, Properties.Resources.CAN34_01ReportPopup, CommandNames.CAN34_01ReportPopup, 0);
            AddMenuItem(reportMenuItem, Properties.Resources.PA_7034ReportPopup, CommandNames.PA_7034ReportPopup, 0);
            AddMenuItem(reportMenuItem, Properties.Resources.CAB05_01ReportPopup, CommandNames.CAB05_01ReportPopup, 0);
            AddMenuItem(reportMenuItem, Properties.Resources.CAB12_01ReportPopup, CommandNames.CAB12_01ReportPopup, 0);
            AddMenuItem(reportMenuItem, Properties.Resources.CAB13_01ReportPopup, CommandNames.CAB13_01ReportPopup, 0);
            //AddMenuItem(reportMenuItem, Properties.Resources.ReportHistoryForDepositBillStatus, CommandNames.ReportHistoryForDepositBillStatus, 0);

            AddMenuItem(_agencyManagementTSMenuItem, Properties.Resources.CriteriaManagement, CommandNames.CriteriaManagement, 0);

            //ToolStripMenuItem calendarItem = AddGroupMenu(agencyManagementTSMenuItem, Properties.Resources.Calendar);
            //AddMenuItem(calendarItem, Properties.Resources.NewCalendar, CommandNames.NewCalendar, 0);

            if (Session.Branch.Id == null)
            {
                _agencyManagementTSMenuItem.Enabled = false;
            }
        }

        private void ExtendToolStrip()
        {
            //bool enable = Session.Branch.Id != null;

            //check to load only once
            //UIExtensionSite toolstrip = WorkItem.UIExtensionSites[UIExtensionSiteNames.MainToolbar];
            //AddToolStripButton(toolstrip, Properties.Resources.AgentPlanning, Properties.Resources.ExpirationHS, 
            //    CommandNames.AgentPlanning);
            //AddToolStripButton(toolstrip, Properties.Resources.BillBook, Properties.Resources.Book_angleHS,
            //    CommandNames.BillBook);
            ////AddToolStripButton(toolstrip, Properties.Resources.AdvancePayment, Properties.Resources.CalculatorHS, CommandNames.AdvancePayment);
            //AddToolStripButton(toolstrip, Properties.Resources.BillBookCheckIn, Properties.Resources.PageUpHS,
            //    CommandNames.BillBookCheckIn);
            //AddToolStripButton(toolstrip, Properties.Resources.CommissionManagement, Properties.Resources.ShowGridlinesHS,
            //    CommandNames.CommissionManagement);
            //AddToolStripButton(toolstrip, Properties.Resources.CriteriaManagement, Properties.Resources.RadialChartHS, CommandNames.CriteriaManagement);
            //AddToolStripButton(toolstrip, Properties.Resources.DataImportManagement, Properties.Resources.ImportXMLHS, CommandNames.DataImportManagement);
        }
        #endregion

        private void LoadReportController()
        {
            ControlledWorkItem<ReportManagementController> reportNotMainMenuControlItem =
                WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportManagementController>>("ReportController");
            reportNotMainMenuControlItem.Controller.Run();
        }

        #region Command Handler

        [CommandHandler(CommandNames.ReportAgencyMoneyReturnVerification)]
        public void AgencyMoneyReturnPopupHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.AgentMoneyReturnReport, true))
            {
                ShowAgencyMoneyVerificationConditionPopUpClicked();
            }
        }

        [CommandHandler(CommandNames.CAN34_01ReportPopup)]
        public void CAN34_01ReportPopupHandler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.CAN34_01Report, true))
            //{
                ShowCAN34_01ReportPopUpClicked();
            //}
        }

        [CommandHandler(CommandNames.PA_7034ReportPopup)]
        public void PA_7034ReportPopupHandler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.PA_7034Report, true))
            //{
                ShowPA_7034ReportPopUpClicked();
           // }
        }


        [CommandHandler(CommandNames.CAB05_01ReportPopup)]
        public void CAB05_01ReportPopupHandler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.CAB05_01Report, true))
            //{
                ShowCAB05_01ReportPopUpClicked();
            //}
        }

        [CommandHandler(CommandNames.CAB12_01ReportPopup)]
        public void CAB12_01ReportPopupHandler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.CAB12_01Report, true))
            //{
                ShowCAB12_01ReportPopUpClicked();
            //}
        }

        [CommandHandler(CommandNames.CAB13_01ReportPopup)]
        public void CAB13_01ReportPopupHandler(object sender, EventArgs e)
        {
            //if (Authorization.IsAuthorized(SecurityNames.CAB12_01Report, true))
            //{
            ShowCAB13_01ReportPopUpClicked();
            //}
        }



        [CommandHandler(CommandNames.AgentPlanning)]
        public void AgentPlanningHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(PEA.BPM.AgencyManagementModule.Interface.Constants.SecurityNames.AgencyPlanning, true))
            {
                ControlledWorkItem<AgentPlanningController> agentPlanningWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<AgentPlanningController>>();
                agentPlanningWorkItem.Controller.Run();            
            }
        }

        [CommandHandler(CommandNames.BillBook)]
        public void BillBookHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(PEA.BPM.AgencyManagementModule.Interface.Constants.SecurityNames.CreateAgentBillBook, true))
            {
                if (_billBookWorkItem == null)
                {
                    _billBookWorkItem = WorkItem.WorkItems.AddNew<ControlledWorkItem<BillBookController>>();
                    _billBookWorkItem.Controller.Run();
                }
                else
                {
                    _billBookWorkItem.Controller.ShowMainView();
                }
            }
        }

        [CommandHandler(CommandNames.BillBookCheckIn)]
        public void BillBookCheckInPaymentHandler(object sender, EventArgs e)
        {
            
            if (Authorization.IsAuthorized(PEA.BPM.AgencyManagementModule.Interface.Constants.SecurityNames.CheckInBillBook, true))
            {
                ControlledWorkItem<BillBookCheckInController> billBookCheckInWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<BillBookCheckInController>>();
                billBookCheckInWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.CommissionManagement)]
        public void AgentCommissionManagementHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(PEA.BPM.AgencyManagementModule.Interface.Constants.SecurityNames.CommissionAndFine, true))
            {
                ControlledWorkItem<CommissionManagementController> agentCommissionManagementWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<CommissionManagementController>>();
                agentCommissionManagementWorkItem.Controller.Run();
            }
        }

        [CommandHandler(CommandNames.BillBookStatus)]
        public void BillBookStatusHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(PEA.BPM.AgencyManagementModule.Interface.Constants.SecurityNames.ReprintBillbookReport, true))
            {
                IBillBookStatusView bookStatusView = null;
                if (_billBookWorkItem == null)
                {
                    //load billbook controller
                    _billBookWorkItem = WorkItem.WorkItems.AddNew<ControlledWorkItem<BillBookController>>();
                    _billBookWorkItem.Controller.HideMainView = true;
                    _billBookWorkItem.Controller.Run();
                }

                //well, we also load the first page of another tab 
                if (WorkItem.Items.Contains("IBillBookStatusView"))
                {
                    bookStatusView = WorkItem.Items.Get<IBillBookStatusView>("IBillBookStatusView");
                    WorkItem.Items.Remove(bookStatusView);
                }
                else
                {
                    bookStatusView = WorkItem.Items.AddNew<BillBookStatusView>("IBillBookStatusView");
                }

                _billBookWorkItem.Controller.BookStatusView = bookStatusView;
                WindowSmartPartInfo modalProperty = new WindowSmartPartInfo();
                modalProperty.MaximizeBox = false;
                modalProperty.MinimizeBox = false;
                modalProperty.Modal = true;
                modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
                modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
                modalProperty.Title = "สถานะสมุดจ่ายบิลและพิมพ์ซ่อมรายงาน";
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(bookStatusView, modalProperty);
            }
        }

        [CommandHandler(CommandNames.CriteriaManagement)]
        public void CriteriaManagementHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.AgencyConfiguration, true))
            {
                ControlledWorkItem<CriteriaManagementController> criteriaManagementWorkItem =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<CriteriaManagementController>>();
                criteriaManagementWorkItem.Controller.Run();
            }
        }
/*
        [CommandHandler(CommandNames.ReportAgencyBillCollectorCondition)]
        public void ReportAgencyillCollectorPopupHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.AgentMoneyCollectByQuarterReport, true))
            {
                ControlledWorkItem<ReportAgencyBillCollectorController> _reportAgencyBillCollectorController =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<ReportAgencyBillCollectorController>>();
                _reportAgencyBillCollectorController.Controller.Run();
            }
        }
*/
        public void LoadAgencyModuleConfigController()
        {
            //_moduleConfigController = WorkItem.WorkItems.AddNew<ControlledWorkItem<ModuleConfigController>>("ModuleConfigController");
            //_moduleConfigController.Controller.Run();
        }

        [CommandHandler(CommandNames.ReportAgencyPlanKeepElectric)]
        public void ReportAgencyPlanKeepElectricHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.AgentMoneyCollectPlan, true))
            {
                ControlledWorkItem<KeepMoneyPlanMonthlyReportController> _keepMoneyPlanCollectorController =
                    WorkItem.WorkItems.AddNew<ControlledWorkItem<KeepMoneyPlanMonthlyReportController>>();
                _keepMoneyPlanCollectorController.Controller.Run();
            }
        }


        [CommandHandler(CommandNames.ReportEvaluateAgencyAboutKeepElectric)]
        public void ReportAgencyAboutKeepElectricViewHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.AgentPerformanceReport, true))
            {
                ShowEvaluateAgencyAboutKeepElectricViewClicked();
            }
        }

        //[CommandHandler(CommandNames.ReportHistoryForDepositBillStatus)]
        //public void ReportHistoryForDepositBillStatusViewHandler(object sender, EventArgs e)
        //{
        //    ShowHistoryForDepositBillStatusViewClicked();
        //}

        #endregion

        #region Event Handler

        //show report agency bill collection condition popup
        //[EventPublication(EventTopicNames.ShowAgencyBillCollectionPopUp, PublicationScope.Global)]
        //public event EventHandler<EventArgs> ShowAgencyBillCollectionConditionPopUpHandler;
        //public void ShowAgencyBillCollectionPopupClicked()
        //{
        //    if (ShowAgencyBillCollectionConditionPopUpHandler != null)            
        //        ShowAgencyBillCollectionConditionPopUpHandler(this, new EventArgs());            
        //}


        [EventPublication(EventTopicNames.ShowAgencyMoneyReturnVerificationPopup, PublicationScope.Global)]
        public event EventHandler<EventArgs> ShowAgencyMoneyVerificationConditionPopUpHandler;
        public void ShowAgencyMoneyVerificationConditionPopUpClicked()
        {
            if (ShowAgencyMoneyVerificationConditionPopUpHandler != null)
                ShowAgencyMoneyVerificationConditionPopUpHandler(this, new EventArgs());        
        }

        
        [EventPublication(EventTopicNames.ShowCAN34_01ReportPopup, PublicationScope.Global)]
        public event EventHandler<EventArgs> ShowCAN34_01ReportPopUpHandler;
        public void ShowCAN34_01ReportPopUpClicked()
        {
            if (ShowCAN34_01ReportPopUpHandler != null)
                ShowCAN34_01ReportPopUpHandler(this, new EventArgs());        
        }


        [EventPublication(EventTopicNames.ShowCAB05_01ReportPopup, PublicationScope.Global)]
        public event EventHandler<EventArgs> ShowCAB05_01ReportPopUpHandler;
        public void ShowCAB05_01ReportPopUpClicked()
        {
            if (ShowCAB05_01ReportPopUpHandler != null)
                ShowCAB05_01ReportPopUpHandler(this, new EventArgs());
        }


        [EventPublication(EventTopicNames.ShowCAB12_01ReportPopup, PublicationScope.Global)]
        public event EventHandler<EventArgs> ShowCAB12_01ReportPopUpHandler;
        public void ShowCAB12_01ReportPopUpClicked()
        {
            if (ShowCAB12_01ReportPopUpHandler != null)
                ShowCAB12_01ReportPopUpHandler(this, new EventArgs());
        }

        [EventPublication(EventTopicNames.ShowCAB13_01ReportPopup, PublicationScope.Global)]
        public event EventHandler<EventArgs> ShowCAB13_01ReportPopUpHandler;
        public void ShowCAB13_01ReportPopUpClicked()
        {
            if (ShowCAB13_01ReportPopUpHandler != null)
                ShowCAB13_01ReportPopUpHandler(this, new EventArgs());
        }

        [EventPublication(EventTopicNames.ShowPA_7034ReportPopup, PublicationScope.Global)]
        public event EventHandler<EventArgs> ShowPA_7034ReportPopUpHandler;
        public void ShowPA_7034ReportPopUpClicked()
        {
            if (ShowPA_7034ReportPopUpHandler != null)
                ShowPA_7034ReportPopUpHandler(this, new EventArgs());
        }        

        [EventPublication(EventTopicNames.ShowKeepPlanningAgencyView, PublicationScope.Global)]
        public event EventHandler<EventArgs> ShowKeepPlanningAgencyViewHandler;
        public void ShowKeepPlanningAgencyViewClicked()
        {
            if (ShowKeepPlanningAgencyViewHandler != null)
                ShowKeepPlanningAgencyViewHandler(this, new EventArgs());
        }

        [EventPublication(EventTopicNames.ShowEvaluateAgencyAboutKeepElectricView, PublicationScope.Global)]
        public event EventHandler<EventArgs> ShowEvaluateAgencyAboutKeepElectricViewHandler;
        public void ShowEvaluateAgencyAboutKeepElectricViewClicked()
        {
            if (ShowEvaluateAgencyAboutKeepElectricViewHandler != null)
                ShowEvaluateAgencyAboutKeepElectricViewHandler(this, new EventArgs());
        }

        //[EventPublication(EventTopicNames.ShowHistoryForDepositBillStatusView, PublicationScope.Global)]
        //public event EventHandler<EventArgs> ShowHistoryForDepositBillStatusViewHandler;
        //public void ShowHistoryForDepositBillStatusViewClicked()
        //{
        //    if (ShowHistoryForDepositBillStatusViewHandler != null)
        //        ShowHistoryForDepositBillStatusViewHandler(this, new EventArgs());
        //}

        [EventSubscription(EventTopicNames.ShowHintEvent, Thread = ThreadOption.UserInterface)]
        public void ShowHintHandler(object sender, EventArgs<ArrayList> e)
        {
            //parem [0] - header
            //[1] - [4] - description

            if (WorkItem.Items.Contains("IUserHintView"))
                WorkItem.Items.Remove(_userHint);

            _userHint = WorkItem.Items.AddNew<UserHintView>("IUserHintView");
            _modalProperty.Title = "ข้อความช่วยเหลือ";
            _userHint.Header = (string)e.Data[0];
            _userHint.Caption1 = (string)e.Data[1];
            _userHint.Caption2 = (string)e.Data[2];
            _userHint.Caption3 = (string)e.Data[3];
            _userHint.Caption4 = (string)e.Data[4];
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_userHint, _modalProperty);

        }

        [EventSubscription(PEA.BPM.Infrastructure.Interface.Constants.EventTopicNames.OnlineStatus, Thread = ThreadOption.UserInterface)]
        public void OnlineStatusHandler(object sender, EventArgs<bool> e)
        {
            if(e.Data)
                _agencyManagementTSMenuItem.Enabled = true;
            else
                _agencyManagementTSMenuItem.Enabled = false;
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
