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
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.CashManagementModule.Interface.Constants;
using System.Collections.Generic;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.CashManagementModule
{
    public class CancelMoneyCheckInViewPresenter : Presenter<ICancelMoneyCheckInView>
    {
        private ICashManagementServices _services;
        private ICashReportServices _reportServices;

        public CancelMoneyCheckInViewPresenter()
        {
        }

        [InjectionConstructor]
        public CancelMoneyCheckInViewPresenter([ServiceDependency] ICashManagementServices services, ICashReportServices reportServices)
        {
            _services = services;
            _reportServices = reportServices;
        }              

        [EventPublication(EventTopicNames.OnLoadMoneyCheckedIn, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<List<string>>> OnLoadMoneyCheckedInHandler;
        public void LoadMoneyCheckedIn(string SAPRefNo, string workId)
        {
            List<string> param = new List<string>();
            param.Add(SAPRefNo);
            param.Add(workId);

            if (OnLoadMoneyCheckedInHandler != null)
                OnLoadMoneyCheckedInHandler(this, new EventArgs<List<string>>(param));
        }

        public int CancelMoneyCheckIn(CancelMoneyCheckedInInfo param)
        {
            int ret = 0;
            try
            {
                ret = _services.CancelMoneyCheckedIn(param);
            }
            catch (Exception e)
            {
                ServiceHelper.TransformErrorMessage(e);
                ret = -1;
            }

            return ret;
        }

        public bool ExistSapRefNo(string SAPRefNo, string workId)
        {
            bool ret = false;
            try
            {
                ret = _services.ExistSAPRefNo(SAPRefNo, workId);
            }
            catch (Exception e)
            {
                ServiceHelper.TransformErrorMessage(e);
            }
            return ret;
        }

        [EventPublication(EventTopicNames.CashierOpenWork, PublicationScope.Global)]
        public event EventHandler<EventArgs<string>> CashierOpenWorkHandler;
        public void OnCashierOpenWork(string tmp)
        {
            if (CashierOpenWorkHandler != null)
                CashierOpenWorkHandler(this, new EventArgs<string>(tmp));
        }

        [EventPublication(EventTopicNames.LoadSapReference, PublicationScope.Global)]
        public event EventHandler<EventArgs<string>> LoadSapReferenceHandler;
        public void LoadSapRefToCBox(string workId)
        {
            if (LoadSapReferenceHandler != null)
                LoadSapReferenceHandler(this, new EventArgs<string>(workId));
        }

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

