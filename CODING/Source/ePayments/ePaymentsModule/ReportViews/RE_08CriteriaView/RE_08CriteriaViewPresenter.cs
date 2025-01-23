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
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.ePaymentsModule.Interface.Constants;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;

namespace PEA.BPM.ePaymentsModule
{
    public class RE_08CriteriaViewPresenter : Presenter<IRE_08CriteriaView>
    {
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
     

        [EventPublication(EventTopicNames.LoadCompanyList, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<CompanyParamInfo>> LoadCompanyListHandler;
        public void LoadCompanyList(CompanyParamInfo comParam)
        {
            if (LoadCompanyListHandler != null)
            {
                LoadCompanyListHandler(this, new EventArgs<CompanyParamInfo>(comParam));
            }
        }


        [EventPublication(EventTopicNames.LoadAccountClassList, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<ReportName>> LoadAccountClassListHandler;
        public void LoadAccountClassList(ReportName reportName)
        {
            if (LoadAccountClassListHandler != null)
            {
                LoadAccountClassListHandler(this, new EventArgs<ReportName>(reportName));
            }
        }

        [EventPublication(EventTopicNames.RE_08Report, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<RE08ParamInfo>> Show08ReportHandler;
        public void Show08Report(RE08ParamInfo reportParam)
        {
            if (Show08ReportHandler != null)
            {
                Show08ReportHandler(this, new EventArgs<RE08ParamInfo>(reportParam));
            }
        }
    }
}

