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
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule
{
    public class InterestInquiryController : WorkItemController
    {
        private ILayoutService _layoutService;

        [InjectionConstructor]
        public InterestInquiryController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public override void Run()
        {
            ShellWaitCursor(true);

            string workDayCount = string.Format(" - กำลังทำงานกะที่ ({0})", Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString());
            SetWindowsTitle(Properties.Resources.InterestInquiry + workDayCount);

            LoadDisplayedViews();

            ShellWaitCursor(false);
        }

        private void LoadDisplayedViews()
        {
            _layoutService.LoadHorizontalLayout(230);

            ICustomerSearchView lView;
            string lViewName = typeof(ICustomerSearchView).ToString();
            if (WorkItem.Items.Contains(lViewName))
            {
                lView = WorkItem.Items.Get<ICustomerSearchView>(lViewName);
            }
            else
            {
                lView = WorkItem.Items.AddNew<CustomerSearchView>(lViewName);
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView);

            IInterestInquiryResultView rView;
            string rViewName = typeof(IInterestInquiryResultView).ToString();
            if (WorkItem.Items.Contains("IInterestInquiryResultView"))
            {
                rView = WorkItem.Items.Get<IInterestInquiryResultView>("IInterestInquiryResultView");
            }
            else
            {
                rView = WorkItem.Items.AddNew<InterestInquiryResultView>("IInterestInquiryResultView");
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView);
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
