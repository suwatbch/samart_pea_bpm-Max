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
    public class NewsController : WorkItemController
    {
        private IAzManService _iAzManService;
        private ILayoutService _layoutService;
        private INewsView _newsView;
        private WindowSmartPartInfo _modalProperty;


        [InjectionConstructor]
        public NewsController([ServiceDependency] ILayoutService layoutService, IAzManService iAzManService)
        {
            _layoutService = layoutService;
            _iAzManService = iAzManService;

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
            SetWindowsTitle(Properties.Resources.News);
            LoadDisplayedViews();
            //LoadRequiredViews();
            ShellWaitCursor(false);
        }

        private void LoadRequiredViews()
        {
            ////revise
            //if (!WorkItem.Items.Contains("IRegisterView"))
            //    WorkItem.Items.Remove(_registerView);

            //if (WorkItem.Items.Contains("IServerConfigView"))
            //    WorkItem.Items.Remove(_serverConfigView);

            //_registerView = WorkItem.Items.AddNew<RegisterView>("IRegisterView");
            //_registerView.LoadDefaultValue();
            ////_serverConfigView = WorkItem.Items.AddNew<ServerConfigView>("IServerConfigView");
            ////_serverConfigView.LoadDefaultValue();
            //_enviSetupView.RegisterTab = _registerView;
        }
        private void LoadDisplayedViews()
        {
            _layoutService.LoadPlainLayout();

            //if (WorkItem.Items.Contains("INewsView"))
            //    _newsView = WorkItem.Items.Get<NewsView>("INewsView");
            //else
            //    _newsView = WorkItem.Items.AddNew<NewsView>("INewsView");

            if (WorkItem.Items.Contains("INewsView"))
                WorkItem.Items.Remove("INewsView");

            _newsView = WorkItem.Items.AddNew<NewsView>("INewsView");

            WorkItem.Workspaces[WorkspaceNames.PlainLayout.CenterWorkspace].Show(_newsView);
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
