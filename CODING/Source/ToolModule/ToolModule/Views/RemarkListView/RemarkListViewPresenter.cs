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
using PEA.BPM.ToolModule.Interface.Constants;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.ToolModule.Interface.Services;
using PEA.BPM.ToolModule.Services;
using System.ComponentModel;
using System.Windows.Forms;

namespace PEA.BPM.ToolModule
{
    public class RemarkListViewPresenter : Presenter<IRemarkListView>
    {
        IAzManService _azManService;
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

        [EventSubscription(EventTopicNames.FunctionSelect, Thread = ThreadOption.UserInterface)]
        public void FunctionSelectHandler(object sender, EventArgs<Function> e)
        {
            try
            {
                _azManService = new AzManService();
                View.SetFunction = e.Data;
                View.RemarkList = _azManService.ListRemark(e.Data.FunctionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [EventPublication(EventTopicNames.FunctionSelect, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<Function>> RefreshRemarkListHandler;
        internal void RefreshRemarkListView(Function fnc)
        {
            if (RefreshRemarkListHandler != null)
                RefreshRemarkListHandler(this, new EventArgs<Function>(fnc));
        }

        [EventSubscription(EventTopicNames.RemarkAdd, Thread = ThreadOption.UserInterface)]
        public void RemarkAddHandler(object sender, EventArgs<UnlockRemark> e)
        {
            _azManService = new AzManService();
            _azManService.AddRemark(e.Data);

            MessageBox.Show(null, "���ҧ������ Remark �����������º����", "�������", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //refresh gridview 
            View.RefreshRemarkList();
            
        }

        [EventSubscription(EventTopicNames.RemarkEdit, Thread = ThreadOption.UserInterface)]
        public void RemarkEditHandler(object sender, EventArgs<UnlockRemark> e)
        {
            _azManService = new AzManService();
            _azManService.EditRemark(e.Data);

            MessageBox.Show(null, "��䢢����� Remark �������º����", "�������", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //refresh gridview 
            View.RefreshRemarkList();

        }

        [EventPublication(EventTopicNames.PopupAddRemarkDialog, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<String>> PopupAddRemarkDialogHandler;
        internal void ShowCreateRemarkDialog(String fncId)
        {
            if (PopupAddRemarkDialogHandler != null)
                PopupAddRemarkDialogHandler(this, new EventArgs<String>(fncId));
        }

        [EventPublication(EventTopicNames.PopupEditRemarkDialog, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<UnlockRemark>> PopupEditRemarkDialogHandler;
        internal void ShowEditRemarkDialog(UnlockRemark remark)
        {
            if (PopupEditRemarkDialogHandler != null)
                PopupEditRemarkDialogHandler(this, new EventArgs<UnlockRemark>(remark));
        }

        [EventSubscription(EventTopicNames.OnCloseViewDisconnect, Thread = ThreadOption.UserInterface)]
        public void OnCloseViewDisconnectHandler(object sender, EventArgs e)
        {
            base.CloseView();
        }

        public void Delete(BindingList<UnlockRemark> rmks)
        {
            _azManService = new AzManService();

            foreach (UnlockRemark rmk in rmks)
                _azManService.DeleteRemark(rmk);

            MessageBox.Show(null, "ź������ Remark �������º����", "�������", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //refresh gridview 
            View.RefreshRemarkList();
        }
    }
}

