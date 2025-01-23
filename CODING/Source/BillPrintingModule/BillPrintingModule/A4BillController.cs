using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.BillPrintingModule.Constants;
using PEA.BPM.BillPrintingModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.BillPrintingModule
{
    public class A4BillController : WorkItemController
    {
        private ILayoutService _layoutService;
        private IControlServices _controlServices;
        private A4BillView _sView;

        [InjectionConstructor]
        public A4BillController([ServiceDependency] ILayoutService layoutService, IControlServices controlServices)
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
                if (WorkItem.Items.Contains("IA4BillView"))
                {
                    _sView = WorkItem.Items.Get<A4BillView>("IA4BillView");
                }
                else
                {
                    _sView = WorkItem.Items.AddNew<A4BillView>("IA4BillView");
                }

                _layoutService.LoadHorizontalLayout(250);
                SetWindowsTitle(Properties.Resources.A4Bill);
                WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(_sView);

                if (!WorkItem.Items.Contains("IBillProcessingListView"))
                {
                    BillProcessingListView rView = WorkItem.Items.AddNew<BillProcessingListView>("IBillProcessingListView");
                }

                //Close right view when click at another menuItem
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

        [EventSubscription(EventTopicNames.LoadApprover, Thread = ThreadOption.UserInterface)]
        public void LoadApproverHandler(object sender, EventArgs<string> e)
        {
            try
            {
                _sView.ApproverList = _controlServices.GetApprover(e.Data);
            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
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
