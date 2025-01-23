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
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.ToolModule
{
    public class AzManController : WorkItemController
    {
		private ILayoutService _layoutService;
        private IRoleUserSwitchView _roleUserSwitchView;

		[InjectionConstructor]
        public AzManController([ServiceDependency] ILayoutService layoutService)
		{
            _layoutService = layoutService;
		}

        public override void Run()
        {
            SetWindowsTitle(Properties.Resources.ManageAz);
            LoadDisplayedViews();
        }

        private void LoadDisplayedViews()
        {
            _layoutService.LoadPlainLayout();

            if (WorkItem.Items.Contains("IRoleUserSwitchView"))
                WorkItem.Items.Remove(_roleUserSwitchView);

            _roleUserSwitchView = WorkItem.Items.AddNew<RoleUserSwitchView>("IRoleUserSwitchView");
            WorkItem.Workspaces[WorkspaceNames.PlainLayout.CenterWorkspace].Show(_roleUserSwitchView);
       
        }
    }
}
