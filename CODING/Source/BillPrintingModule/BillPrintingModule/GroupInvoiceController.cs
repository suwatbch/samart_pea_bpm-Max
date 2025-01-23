using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.BillPrintingModule.Constants;
using PEA.BPM.BillPrintingModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.BillPrintingModule
{
    public class GroupInvoiceController : WorkItemController
    {
        private ILayoutService _layoutService;
        private IControlServices _controlServices;
        private GroupInvoiceView _sView;

        [InjectionConstructor]
        public GroupInvoiceController([ServiceDependency] ILayoutService layoutService, IControlServices controlServices)
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
                SetWindowsTitle(Properties.Resources.GroupInvoice);
                if (WorkItem.Items.Contains("IGroupInvoiceView"))
                {
                    _sView = WorkItem.Items.Get<GroupInvoiceView>("IGroupInvoiceView");
                }
                else
                {
                    _sView = WorkItem.Items.AddNew<GroupInvoiceView>("IGroupInvoiceView");
                }

                _layoutService.LoadHorizontalLayout(250);
                WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(_sView);

                if (!WorkItem.Items.Contains("IBillProcessingListView"))
                {
                    BillProcessingListView rView = WorkItem.Items.AddNew<BillProcessingListView>("IBillProcessingListView");
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
