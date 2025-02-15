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
using PEA.BPM.CashManagementModule.Interface.Constants;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.CashManagementModule.Interface.Services;
using System.Collections.Generic;

namespace PEA.BPM.CashManagementModule
{
    public class ReportSummaryCloseWorkViewPresenter : Presenter<IReportSummaryCloseWorkView>
    {
        private ICashManagementServices _cashMgntServices;
        private ICashReportServices _cashReportServices;

        [InjectionConstructor]
        public ReportSummaryCloseWorkViewPresenter([ServiceDependency] ICashManagementServices cashMgntServices,
                                                                       ICashReportServices cashReportServices)
        {
            _cashMgntServices = cashMgntServices;
            _cashReportServices = cashReportServices;
        }

        /// <summary>
        /// This method is a placeholder that will be called by the view when it's been loaded <see cref="System.Winforms.Control.OnLoad"/>
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
        }

        [EventPublication(EventTopicNames.SummaryCloseWorkReport, PublicationScope.Global)]
        public event EventHandler<EventArgs<ReportParam>> SummaryCloseWorkReportHandler;
        public void ShowReport(ReportParam param)
        {
            if (SummaryCloseWorkReportHandler != null)
                SummaryCloseWorkReportHandler(this, new EventArgs<ReportParam>(param));
        }

        public List<ReportAvailableInfo> GetAvailableCloseWork(ReportParam param)
        {
            return _cashReportServices.GetCloseWorkOfDate(param);
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

