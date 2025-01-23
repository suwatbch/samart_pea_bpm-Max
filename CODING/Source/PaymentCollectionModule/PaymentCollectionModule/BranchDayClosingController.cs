using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Constants;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;

namespace PEA.BPM.PaymentCollectionModule
{
    public class BranchDayClosingController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public BranchDayClosingController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            SetWindowsTitle(Properties.Resources.InterestInquiry);

            LoadDisplayedViews();
        }

        private void LoadDisplayedViews()
        {
            ShellWaitCursor(true);
            _layoutService.LoadPlainLayout();

            IDayClosingView lView;
            string lViewName = typeof(IDayClosingView).ToString();
            if (WorkItem.Items.Contains(lViewName))
            {
                lView = WorkItem.Items.Get<IDayClosingView>(lViewName);
            }
            else
            {
                lView = WorkItem.Items.AddNew<DayClosingView>(lViewName);
            }
            WorkItem.Workspaces[WorkspaceNames.PlainLayout.CenterWorkspace].Show(lView);

            ShellWaitCursor(false);
        }


        #region Event Handler

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }

        #endregion
    }
}
