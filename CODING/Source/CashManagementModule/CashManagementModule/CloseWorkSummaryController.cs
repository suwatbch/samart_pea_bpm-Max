using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using System.IO;
using System.Windows.Forms;
using PEA.BPM.CashManagementModule.Interface.Constants;

namespace PEA.BPM.CashManagementModule
{
    public class CloseWorkSummaryController : WorkItemController
    {
        private ILayoutService _layoutService;
        private ICashManagementServices _cashManagementService;
        private ICashReportServices _cashReportService;
        private ICloseWorkSummaryView _sView;

        [InjectionConstructor]
        public CloseWorkSummaryController([ServiceDependency] ILayoutService layoutService,
                                               ICashManagementServices cashManagementService,
                                               ICashReportServices cashReportService)
        {
            _layoutService = layoutService;
            _cashManagementService = cashManagementService;
            _cashReportService = cashReportService;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            string dayCountTxt = Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString();
            SetWindowsTitle(Properties.Resources.CloseWorkMenuTitle + " - กำลังทำงานกะที่ (" + dayCountTxt + ")");
            if (WorkItem.Items.Contains("ICloseWorkSummaryView"))
                _sView = WorkItem.Items.Get<ICloseWorkSummaryView>("ICloseWorkSummaryView");
            else
                _sView = WorkItem.Items.AddNew<CloseWorkSummaryView>("ICloseWorkSummaryView");

            //_layoutService.LoadHorizontalLayout(250);            
            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
            ShellWaitCursor(false);

            //force process offline file
            FeedOfflineFile();
        }



        [EventSubscription(EventTopicNames.CloseWorkFlowItem, Thread = ThreadOption.UserInterface)]
        public void CloseWorkFlowItemHandler(object sender, EventArgs<string> e)
        {
            try
            {
                CloseWorkSummaryInfo closeWorkInfo = _cashManagementService.GetCloseWorkFlowItem(e.Data); //workId
                _sView.CloseWorkSummary = closeWorkInfo;
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
        }

        [EventSubscription(EventTopicNames.CloseWorkNow, Thread = ThreadOption.UserInterface)]
        public void CloseWorkNowHandler(object sender, EventArgs<CloseWorkSubmitInfo> e)
        {
            try
            {
                _cashManagementService.CloseWork(e.Data); //workId
                SetWindowsTitle("");
                Session.Work.DayCount = 0;
                Session.Work.Id = null;
             
                //close every relative view including myself
               CloseAllViews();
                
               _sView.CloseView();
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
        }

        [EventSubscription(EventTopicNames.TriggerSyncUp, Thread = ThreadOption.UserInterface)]
        public void EnqueueTickerHandler(object sender, EventArgs e)
        {
            try
            {
                //check wheather this is connected to branch server or bpm server               
                //In case, batch does not exist, checking in BS will be ignore this case by just continuing program.
                if (!Session.Branch.OnlineConnection)
                {
                    Authorization.SignalSyncup(LocalSettingNames.DL080_UPLOAD_AP_BATCH);
                    Authorization.SignalSyncup(LocalSettingNames.DL090_UPLOAD_PAYMENT_BATCH);
                    Authorization.SignalSyncup(LocalSettingNames.DL091_UPLOAD_CASH_MANAGEMENT_BATCH);
                    Authorization.SignalSyncup(LocalSettingNames.DL100_UPLOAD_AGENCY_BATCH);
                    Authorization.SignalSyncup(LocalSettingNames.DL101_UPLOAD_EPAYMENT_BATCH);
                    Authorization.SignalSyncup(LocalSettingNames.DL110_UPLOAD_TECHNICAL_BATCH);
                    Authorization.SignalSyncup(LocalSettingNames.DL120_UPLOAD_BILLING_BATCH);
                }
            }
            catch (Exception)
            {
                //TickerErrorBox tk = new TickerErrorBox(e.Message);
                //tk.ShowDialog();
            }
        }

        [EventPublication(EventTopicNames.ProcessOfflineFile, PublicationScope.Global)]
        public event EventHandler<EventArgs> ProcessOfflineFileHandler;
        public void FeedOfflineFile()
        {
            if (ProcessOfflineFileHandler != null)
                ProcessOfflineFileHandler(this, new EventArgs());
        }

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }                    

    }
}
