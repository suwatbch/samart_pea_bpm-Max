using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.Infrastructure.Interface;

namespace PEA.BPM.PaymentCollectionModule.ReportViews.ResultViewCenter
{
    public class ReportContainerViewPresenter : Presenter<IReportContainerView>
    {

        public ReportContainerViewPresenter()
        {
        }

        /// <summary>
        /// View Cursor Setting 
        /// </summary>

        #region "Code Generated"
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
            base.Dispose();
        }
        #endregion
    }
}

