using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.CashManagementModule.Interface.Constants;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.CashManagementModule
{
    public class CancelCashCheckInController : WorkItemController
    {
        private ILayoutService _layoutService;
        private ICashManagementServices _cashManagementService;
        private ICancelMoneyCheckInView _sView;

        [InjectionConstructor]
        public CancelCashCheckInController([ServiceDependency] ILayoutService layoutService,
                                                ICashManagementServices cashManagementService)
        {
            _layoutService = layoutService;
            _cashManagementService = cashManagementService;
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            string dayCountTxt = Session.Work.DayCount == 0 ? "ข้ามวัน" : Session.Work.DayCount.ToString();
            SetWindowsTitle(Properties.Resources.CancelCashCheckingInMenuTitle + " - กำลังทำงานกะที่ (" + dayCountTxt + ")");
            if (WorkItem.Items.Contains("ICancelMoneyCheckInView"))
                _sView = WorkItem.Items.Get<ICancelMoneyCheckInView>("ICancelMoneyCheckInView");
            else
                _sView = WorkItem.Items.AddNew<CancelMoneyCheckInView>("ICancelMoneyCheckInView");

            //_layoutService.LoadHorizontalLayout(250);            
            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_sView);
            ShellWaitCursor(false);
        }

        [EventSubscription(EventTopicNames.LoadSapReference, Thread = ThreadOption.UserInterface)]
        public void LoadSapReferenceHandler(object sender, EventArgs<string> e)
        {
            try
            {
                List<string> sapRefList = _cashManagementService.LoadSAPReference(e.Data);
                _sView.SapReferenceList = sapRefList;
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
            }
        }

        [EventSubscription(EventTopicNames.OnLoadMoneyCheckedIn, Thread = ThreadOption.UserInterface)]
        public void OnLoadMoneyCheckedInHandler(object sender, EventArgs<List<string>> e)
        {
            try
            {
                _sView.MoneyCheckedIn = _cashManagementService.LoadMoneyCheckedIn(e.Data[0], e.Data[1]);                
            }
            catch (Exception ex)
            {
                ServiceHelper.TransformErrorMessage(ex);
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
