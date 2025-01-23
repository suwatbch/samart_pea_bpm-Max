using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.BillPrintingModule.Constants;
using PEA.BPM.BillPrintingModule.Interface.Services;

namespace PEA.BPM.BillPrintingModule
{
    public class ReportStreetRouteReceiveController : WorkItemController
    {
        private ILayoutService _layoutService;
        private IControlServices _controlServices;
        private ReportStreetRouteReceiveView _sView;

        [InjectionConstructor]
        public ReportStreetRouteReceiveController([ServiceDependency] ILayoutService layoutService, IControlServices controlServices)
        {
            _layoutService = layoutService;
            _controlServices = controlServices;
        }

        public override void Run()
        {
            try
            {
                ShellWaitCursor(true);
                CloseAllViews();
                SetWindowsTitle(Properties.Resources.StreetRouteReceiveReport);
                if (WorkItem.Items.Contains("IReportStreetRouteReceiveView"))
                {
                    _sView = WorkItem.Items.Get<ReportStreetRouteReceiveView>("IReportStreetRouteReceiveView");
                }
                else
                {
                    _sView = WorkItem.Items.AddNew<ReportStreetRouteReceiveView>("IReportStreetRouteReceiveView");
                }

                _layoutService.LoadHorizontalLayout(250);
                WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(_sView);

                if (!WorkItem.Items.Contains("IReportContainerView"))
                {
                    ReportContainerView rView = WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");
                }

                Object obj = WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].ActiveSmartPart;
                if (obj != null)
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Close(obj);

                ShellWaitCursor(false);
            }
            catch
            {                
                ShellWaitCursor(false);
                throw;
            }
        }

        [EventSubscription(EventTopicNames.GetPortion, Thread = ThreadOption.UserInterface)]
        public void GetPortionHandler(object sender, EventArgs<string> e)
        {
            _sView.Portion = _controlServices.GetPortion(e.Data);
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
