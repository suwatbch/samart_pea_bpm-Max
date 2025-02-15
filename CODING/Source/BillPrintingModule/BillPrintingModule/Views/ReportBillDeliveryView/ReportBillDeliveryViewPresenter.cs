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
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.BillPrintingModule.Constants;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.BillPrintingModule.Interface.Services;
using System.Collections.Generic;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.BillPrintingModule
{
    public class ReportBillDeliveryViewPresenter : Presenter<IReportBillDeliveryView>
    {
        private IReportServices _reportServices;
        private IControlServices _controlServices;
        
        [InjectionConstructor]
        public ReportBillDeliveryViewPresenter([ServiceDependency] IReportServices reportServices, [ServiceDependency] IControlServices controlServices)
		{
            _reportServices = reportServices;
            _controlServices = controlServices;
		}

        [EventPublication(EventTopicNames.PrintBillDeliveryReport, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<ReportConditionParam>> PrintBillDeliveryReport;
        public void PreviewBillDeliveryReport(ReportConditionParam param)
        {
            if (PrintBillDeliveryReport != null)
                PrintBillDeliveryReport(this, new EventArgs<ReportConditionParam>(param));
        }

        [EventPublication(EventTopicNames.DeliveryPlacePopup, PublicationScope.Global)]
        public event EventHandler<EventArgs> DeliveryPlacePopupHandler;
        public void ShowDeliveryPlaceScreen()
        {
            if (DeliveryPlacePopupHandler != null)
                DeliveryPlacePopupHandler(this, new EventArgs());
        }

        [EventPublication(EventTopicNames.ShowBlankReport, PublicationScope.Global)]
        public event EventHandler<EventArgs> ShowBlankReportHandler;
        public void BlankReport()
        {
            if (ShowBlankReportHandler != null)
                ShowBlankReportHandler(this, new EventArgs());
        }

        [EventSubscription(EventTopicNames.WaitCursor, Thread = ThreadOption.UserInterface)]
        public void ShowWaitCursor(object sender, EventArgs<bool> e)
        {
            View.OnWaitCursor(e.Data);
        }

        [EventSubscription(EventTopicNames.DeliveryToUpdated, Thread = ThreadOption.UserInterface)]
        public void DeliveryToUpdatedSubsciber(object sender, EventArgs e)
        {
            View.RefreshDeliveryTo();
        }

        [EventSubscription(EventTopicNames.InitBillDeliveryUI, Thread = ThreadOption.UserInterface)]
        public void InitBillDeliveryUIHandler(object sender, EventArgs<bool> e)
        {
            View.ResetUI(e.Data);
        }

        [EventSubscription(EventTopicNames.FreezePanel, Thread = ThreadOption.UserInterface)]
        public void FreezePanelHandler(object sender, EventArgs e)
        {
            View.DisabledUI();
        }

        public void GetBranchForBillDelivery(ReportConditionParam param)
        {
            View.BillPeriod = param.BillPeriod;
            View.PrintableId = _reportServices.GetBranchForBillDeliveryReport(param);
        }

        public void LoadComboBox()
        {
            //View.AuthorizedPerson = _controlServices.GetAuthorizedPerson();
            View.DeliveryPlace = _controlServices.GetDeliveryPlace(Session.Branch.Id);
        }


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
        }
        #endregion
    }
}

