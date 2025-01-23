using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.ToolModule.Interface.Constants;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace PEA.BPM.ToolModule
{
    public class ConfigurationController : WorkItemController
    {
		private ILayoutService _layoutService;

		[InjectionConstructor]
        public ConfigurationController([ServiceDependency] ILayoutService layoutService)
		{
            _layoutService = layoutService;
		}

        public override void Run()
        {

                ShellWaitCursor(true);
                SetWindowsTitle(Properties.Resources.Tool);

                LoadDisplayedViews();
                LoadRequiredViews();
                ShellWaitCursor(false);

        }

        private void LoadDisplayedViews()
        {
            _layoutService.LoadPlainLayout();
            
            IOptionView view;
            if (WorkItem.Items.Contains("IOptionView"))
            {
                view = WorkItem.Items.Get<IOptionView>("IOptionView");
            }
            else
            {
                view = WorkItem.Items.AddNew<OptionView>("IOptionView");
            }
            //WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(view);
            WorkItem.Workspaces[WorkspaceNames.PlainLayout.CenterWorkspace].Show(view);
        }

        private void LoadRequiredViews()
        {

            if (!WorkItem.Items.Contains("IReportContainerView"))
            {
                WorkItem.Items.AddNew<ReportContainerView>("IReportContainerView");
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
