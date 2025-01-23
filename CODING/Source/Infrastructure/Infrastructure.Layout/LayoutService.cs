using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface.Constants;

namespace PEA.BPM.Infrastructure.Layout
{
    public class LayoutService: ILayoutService
    {
        private WorkItem _rootWorkItem;

        [InjectionConstructor]
        public LayoutService([ServiceDependency] WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }

        #region ILayoutService Members

        public void LoadPlainLayout()
        {
            IPlainLayout pLayout;

            if (_rootWorkItem.Items.Contains("IPlainLayout"))
            {
                pLayout = _rootWorkItem.Items.Get<IPlainLayout>("IPlainLayout");
            }
            else
            {
                pLayout = _rootWorkItem.Items.AddNew<PlainLayout>("IPlainLayout");
            }

            _rootWorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(pLayout);
        }

        public void LoadHorizontalLayout(int leftSize)
        {
            IHorizontalLayout hLayout;

            if (_rootWorkItem.Items.Contains("IHorizontalLayout"))
            {
                hLayout = _rootWorkItem.Items.Get<IHorizontalLayout>("IHorizontalLayout");
            }
            else
            {
                hLayout = _rootWorkItem.Items.AddNew<HorizontalLayout>("IHorizontalLayout");
            }

            hLayout.SetLeftSize(leftSize);
            _rootWorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(hLayout);
        }

        public void LoadVerticalLayout(int topSize)
        {
            IVerticalLayout vLayout;

            if (_rootWorkItem.Items.Contains("IVerticalLayout"))
            {
                vLayout = _rootWorkItem.Items.Get<IVerticalLayout>("IVerticalLayout");
            }
            else
            {
                vLayout = _rootWorkItem.Items.AddNew<VerticalLayout>("IVerticalLayout");
            }

            vLayout.SetTopSize(topSize);
            _rootWorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(vLayout);
        }

        #endregion
    }
}