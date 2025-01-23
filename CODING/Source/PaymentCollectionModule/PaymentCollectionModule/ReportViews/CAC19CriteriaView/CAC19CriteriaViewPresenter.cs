using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.ReportViews.CAC19CriteriaView;

namespace PEA.BPM.PaymentCollectionModule
{
    public class CAC19CriteriaViewPresenter : Presenter<ICAC19CriteriaView>
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

        [EventPublication(EventTopicNames.ShowReportCAC19Click, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<ReportParam>> ShowReport;
        internal void OnShowReport(CAC19Param param)
        {
            if (ShowReport != null)
            {
                OnCloseView();
                ShowReport(this, new EventArgs<ReportParam>(param));
            }
        }
    }
}
