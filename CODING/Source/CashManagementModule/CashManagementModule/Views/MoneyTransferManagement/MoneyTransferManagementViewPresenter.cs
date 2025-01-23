//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// A presenter calls methods of a view to update the information that the view displays. 
// The view exposes its methods through an interface definition, and the presenter contains
// a reference to the view interface. This allows you to test the presenter with different 
// implementations of a view (for example, a mock view).
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.Constants;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using System.Collections.Generic;

namespace PEA.BPM.CashManagementModule
{
    public class MoneyTransferManagementViewPresenter : Presenter<IMoneyTransferManagementView>
    {
        private ICashManagementServices _cashMgntServices;
        private ICashReportServices _cashReportServices;

        [InjectionConstructor]
        public MoneyTransferManagementViewPresenter([ServiceDependency] ICashManagementServices cashMgntServices,
                                                                        ICashReportServices cashReportServices)
        {
            _cashMgntServices = cashMgntServices;
            _cashReportServices = cashReportServices;
        }

        [EventPublication(EventTopicNames.DisplayMoneyInTay, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<string>> DisplayMoneyInTay;
        public void ShowMoneyInTray(string workId)
        {
            if (DisplayMoneyInTay != null)
                DisplayMoneyInTay(this, new EventArgs<string>(workId));
        }

        [EventPublication(EventTopicNames.TransferMoney, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<MoneyTransferInfo>> TransferMoneyHandler;
        public void Transfer(MoneyTransferInfo transferMoney)
        {
            if (TransferMoneyHandler != null)
                TransferMoneyHandler(this, new EventArgs<MoneyTransferInfo>(transferMoney));
        }


        [EventPublication(EventTopicNames.CashierSearchBox, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<List<string>>> CashierSearchBoxHandler;
        public void ShowCashierSearchBox(List<string> param)
        {
            if (CashierSearchBoxHandler != null)
                CashierSearchBoxHandler(this, new EventArgs<List<string>>(param));
        }

        [EventPublication(EventTopicNames.GLBankAccount, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<string>> GLBankAccountHandler;
        public void LoadBankAccount(string businessPlace)
        {
            if (GLBankAccountHandler != null)
                GLBankAccountHandler(this, new EventArgs<string>(businessPlace));
        }

        [EventPublication(EventTopicNames.CashierOpenWork, PublicationScope.Global)]
        public event EventHandler<EventArgs<string>> CashierOpenWorkHandler;
        public void OnCashierOpenWork(string tmp)
        {
            if (CashierOpenWorkHandler != null)
                CashierOpenWorkHandler(this, new EventArgs<string>(tmp));
        }

        [EventPublication(EventTopicNames.MoneyInTray, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<string>> MoneyInTray;
        public void GetMoneyInTray(string workId)
        {
            if (MoneyInTray != null)
                MoneyInTray(this, new EventArgs<string>(workId));
        }

        //public string GetSenderWorkId(OpenWorkParam param)
        //{
        //    List<CashierWorkStatusInfo> openWorkList = _cashMgntServices.IsOpenedWork(param);
        //    if (openWorkList.Count > 0)
        //        return openWorkList[0].WorkId;
        //    else
        //        return null;
        //}

        /// <summary>
        /// This method is a placeholder that will be called by the view when it's been loaded <see cref="System.Winforms.Control.OnLoad"/>
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
        }

        /// <summary>
        /// Close the view
        /// </summary>
        public void OnCloseView()
        {
            base.CloseView();
        }
    }
}

