using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.ToolModule.Interface.Constants;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.ToolModule.Interface.Services;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using System.ComponentModel;

namespace PEA.BPM.ToolModule
{
    public class RemarkController : WorkItemController
    {
		private ILayoutService _layoutService;
        private IAzManService _azManService;
        private IRemarkListView _remarkListView;
        private IFunctionRemarkView _functionRemarkView;
        private IRemarkAddView _remarkAddView;
        private IRemarkEditView _remarkEditView;
        private WindowSmartPartInfo _modalProperty;

		[InjectionConstructor]
        public RemarkController([ServiceDependency] ILayoutService layoutService, IAzManService azManService)
		{
            _layoutService = layoutService;
            _azManService = azManService;

            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            _modalProperty.MaximizeBox = false;
            _modalProperty.MinimizeBox = false;
            _modalProperty.Modal = true;   
		}

        public override void Run()
        {
            ShellWaitCursor(true);
            SetWindowsTitle(Properties.Resources.ManageRemark);
            LoadDisplayedViews();
            ShellWaitCursor(false);
        }

        private void LoadDisplayedViews()
        {
            _layoutService.LoadHorizontalLayout(230);

            if (WorkItem.Items.Contains("IRemarkListView"))
            {
                _remarkListView = WorkItem.Items.Get<RemarkListView>("IRemarkListView");
            }
            else
            {
                _remarkListView = WorkItem.Items.AddNew<RemarkListView>("IRemarkListView");
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(_remarkListView);

            if (WorkItem.Items.Contains("IFunctionRemarkView"))
            {
                _functionRemarkView = WorkItem.Items.Get<FunctionRemarkView>("IFunctionRemarkView");
            }
            else
            {
                _functionRemarkView = WorkItem.Items.AddNew<FunctionRemarkView>("IFunctionRemarkView");
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(_functionRemarkView);                     
        }

        [EventSubscription(EventTopicNames.PopupAddRemarkDialog, Thread = ThreadOption.UserInterface)]
        public void PopupAddRemarkDialogHandler(object sender, EventArgs<String> e)
        {
            if (WorkItem.Items.Contains("IRemarkAddView"))
                WorkItem.Items.Remove(_remarkAddView);

            _remarkAddView = WorkItem.Items.AddNew<RemarkAddView>("IRemarkAddView");
            _remarkAddView.FncId = e.Data;
            _modalProperty.Title = "สร้าง Remark ใหม่";
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_remarkAddView, _modalProperty);
        }

        [EventSubscription(EventTopicNames.PopupEditRemarkDialog, Thread = ThreadOption.UserInterface)]
        public void PopupEditRemarkDialogHandler(object sender, EventArgs<UnlockRemark> e)
        {
            if (WorkItem.Items.Contains("IRemarkEditView"))
                WorkItem.Items.Remove(_remarkEditView);

            _remarkEditView = WorkItem.Items.AddNew<RemarkEditView>("IRemarkEditView");
            _remarkEditView.UnlockRemarkInfo = e.Data;
            _modalProperty.Title = "แก้ไขข้อมูล Remark";

            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_remarkEditView, _modalProperty);
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
