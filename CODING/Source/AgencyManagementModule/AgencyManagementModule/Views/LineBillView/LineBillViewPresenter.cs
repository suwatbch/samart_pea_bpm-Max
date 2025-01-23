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
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.Collections;
using System.ComponentModel;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule
{
    public class LineBillViewPresenter : Presenter<ILineBillView>
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

        [EventPublication(EventTopicNames.BackToBookManagementView, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> BackToBookManagementViewActivatedHandler;
        public void BackToBookManagementViewActivated()
        {
            if (BackToBookManagementViewActivatedHandler != null)
                BackToBookManagementViewActivatedHandler(this, new EventArgs());
        }

        [EventPublication(EventTopicNames.AddExceptionalCallingBill, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> AddExceptionalCallingBillActivatedHandler;
        public void AddExceptionalCallingBillActivated(CallingBillInfo billInfo)
        {
            if (AddExceptionalCallingBillActivatedHandler != null)
                AddExceptionalCallingBillActivatedHandler(this, new EventArgs<CallingBillInfo>(billInfo));
        }

        [EventPublication(EventTopicNames.ValidateCaIdInput, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> ValidateCaIdInputHandler;
        public void ValidateBarcodeInput(string input)
        {
            ArrayList parem = new ArrayList();
            parem.Add(input);
            parem.Add((int)3);
            if (ValidateCaIdInputHandler != null)
                ValidateCaIdInputHandler(this, new EventArgs<ArrayList>(parem));
        }

        [EventPublication(EventTopicNames.PrintBillBook, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> PrintBillBookHandler;
        public void BillBookSaveRequestClicked()
        {
            if (PrintBillBookHandler != null)
                PrintBillBookHandler(this, new EventArgs());
        }

        [EventSubscription(EventTopicNames.SetCancelBillBook, Thread = ThreadOption.UserInterface)]
        public void SetCancelBillBookHandler(object sender, EventArgs<bool> e)
        {
            View.SetCancelBillBook(e.Data);
        }

        [EventPublication(EventTopicNames.CancelBillBook, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> CancelBillBookHandler;
        public void CancelBillBookClicked()
        {
            if (CancelBillBookHandler != null)
                CancelBillBookHandler(this, new EventArgs());
        }

    }
}

