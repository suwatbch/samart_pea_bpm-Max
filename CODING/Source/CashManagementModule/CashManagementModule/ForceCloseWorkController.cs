using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.CashManagementModule.Interface.Constants;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.CashManagementModule
{
    public class ForceCloseWorkController : WorkItemController
    {
        private ILayoutService _layoutService;
        private ICashManagementServices _cashManagementService;
        private IForceCloseWorkView _sView;

        [InjectionConstructor]
        public ForceCloseWorkController([ServiceDependency] ILayoutService layoutService,
                                                           ICashManagementServices cashManagmentService)
        {
            _layoutService = layoutService;
            _cashManagementService = cashManagmentService;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            //string dayCountTxt = Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString();
            //SetWindowsTitle(Properties.Resources.ForceCloseWorkMenuTitle + " - กำลังทำงานกะที่ (" + dayCountTxt + ")");
            SetWindowsTitle(Properties.Resources.ForceCloseWorkMenuTitle);
            if (WorkItem.Items.Contains("IForceCloseWorkView"))
                _sView = WorkItem.Items.Get<IForceCloseWorkView>("IForceCloseWorkView");
            else
                _sView = WorkItem.Items.AddNew<ForceCloseWorkView>("IForceCloseWorkView");

            //_layoutService.LoadHorizontalLayout(250);            
            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
            ShellWaitCursor(false);
        }

        [EventSubscription(EventTopicNames.ListAllOpenWork, Thread = ThreadOption.UserInterface)]
        public void ListAllOpenWorkHandler(object sender, EventArgs<string> e)
        {
            try
            {
                List<WorkInfo> blList = _cashManagementService.ListAllOpenWork(e.Data); //branchId
                _sView.OpenWorkList = blList;
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
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
