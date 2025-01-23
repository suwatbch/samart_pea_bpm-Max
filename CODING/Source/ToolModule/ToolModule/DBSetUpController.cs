using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.ToolModule.Interface.Constants;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.ToolModule.Interface.Services;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;



namespace PEA.BPM.ToolModule
{
    public class DBSetUpController : WorkItemController
    {
        private IServerConfigView _serverConfigView;
        private ILayoutService _layoutService;        
        private WindowSmartPartInfo _modalProperty;        

		[InjectionConstructor]
        public DBSetUpController([ServiceDependency] ILayoutService layoutService)
		{
            _layoutService = layoutService;

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
            SetWindowsTitle(Properties.Resources.Tool);
            LoadDisplayedViews();
            ShellWaitCursor(false);
        }
        
        private void LoadDisplayedViews()
        {
            _layoutService.LoadPlainLayout();

            if (WorkItem.Items.Contains("IServerConfigView"))
                WorkItem.Items.Remove(_serverConfigView);

            _serverConfigView = WorkItem.Items.AddNew<ServerConfigView>("IServerConfigView");
            WorkItem.Workspaces[WorkspaceNames.PlainLayout.CenterWorkspace].Show(_serverConfigView);
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
