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
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.Constants;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.BillPrintingModule.Interface.Services;

namespace PEA.BPM.BillPrintingModule
{
    public class GreenReceiptReprintViewPresenter : Presenter<IGreenReceiptReprintView>
    {
        private IControlServices _controlServices;
        
        [InjectionConstructor]
        public GreenReceiptReprintViewPresenter([ServiceDependency] IControlServices controlServices)
		{
            _controlServices = controlServices;
		}

        [EventPublication(EventTopicNames.ReprintGreenReceipt, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<BillPrintingConditionParam>> ReprintGreenReceipt;
        public void ReprintGreenReceiptBill(BillPrintingConditionParam param)
        {
            if (ReprintGreenReceipt != null)
                ReprintGreenReceipt(this, new EventArgs<BillPrintingConditionParam>(param));
        }

        [EventPublication(EventTopicNames.PrintSelectedBill, PublicationScope.WorkItem)]
        public event EventHandler PrintSelectedBill;
        public void PrintSelectedBillHandler()
        {
            if (PrintSelectedBill != null)
                PrintSelectedBill(this, new EventArgs());
        }

        [EventPublication(EventTopicNames.ClearBillProcessingListView, PublicationScope.WorkItem)]
        public event EventHandler ClearBillProcessingListView;
        public void ClearBillProcessingListViewHandler()
        {
            if (ClearBillProcessingListView != null)
                ClearBillProcessingListView(this, new EventArgs());
        }

        [EventSubscription(EventTopicNames.ChangeStatusForLeftView, Thread = ThreadOption.UserInterface)]
        public void LockConditionView(object sender, EventArgs<bool> e)
        {
            View.LockView(e.Data);
        }

        [EventSubscription(EventTopicNames.WaitCursor, Thread = ThreadOption.UserInterface)]
        public void ShowWaitCursor(object sender, EventArgs<bool> e)
        {
            View.OnWaitCursor(e.Data);
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

