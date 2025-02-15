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
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.EventBroker;
using System.Windows.Forms;
using System.Collections.Generic;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using System.Drawing;
using System.Data;
using System.ComponentModel;

namespace PEA.BPM.PaymentCollectionModule
{
    public class BillSearchResultViewPresenter : Presenter<IBillSearchResultView>
    {   
		private IBillingService _billingService;

		[InjectionConstructor]
        public BillSearchResultViewPresenter([ServiceDependency] IBillingService billingService)
		{
            _billingService = billingService;
        }

        #region Search By ID
        [EventSubscription(EventTopicNames.InvoiceSearchedByCustomerId, Thread = ThreadOption.UserInterface)]
        public void InvoiceSearchedByCustomerIdHandler(object sender, EventArgs<CustomerSearchParam> e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_InvoiceSearchedByCustomerIdDoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_InvoiceSearchedByCustomerIdCompleted);
            bgWorker.RunWorkerAsync(e.Data);
            WaitingForm.ShowForm(this.FindForm(WorkspaceNames.CenterWorkspace));
        }

        void bgWorker_InvoiceSearchedByCustomerIdDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CustomerSearchParam param = (CustomerSearchParam)e.Argument;
                List<Invoice> invoices = _billingService.SearchInvoiceByCustomerId(param);
                object[] o = new object[] { invoices, param.CaId, param.IsOtherBranch };
                e.Result = o;
                WaitingForm.HideForm();
            }
            catch (Exception ex)
            {
                WaitingForm.HideForm();
                Logger.WriteError(Logger.Module.POS, "���Ң�����˹��ҡ CaId", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(this.FindForm(WorkspaceNames.CenterWorkspace), EErrorInModule.POS, ex);
            }
        }

        void bgWorker_InvoiceSearchedByCustomerIdCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (null != e.Result)
            {
                object[] o = (object[])e.Result;
                List<Invoice> invoices = (List<Invoice>)o[0];
                if (invoices.Count > 0)
                {

                    //Comment because.. Now, BP & CA are retrieved from Realtime-pos.
                    //if ((bool)o[2] == true && Session.Branch.BranchLevel == "3")
                    //{
                    //    List<CaAndBpInfo> list = _billingService.GetCaAndBpOtherBranch(invoices[0].CaId);
                    //    _billingService.SaveCaAndBpOtherBranch(list);
                    //}

                    InvoicesAddedToList(invoices);
                }
                else
                {
                    //HistoryViewParam param = new HistoryViewParam((string)o[1], (bool)o[2]);
                    //OnViewHistoryClick(param);

                    View.ConfirmShowHistory((string)o[1], invoices.Count, (bool)o[2]);

                    // DCR 67-020 
                    SearchByCaIdNoInvoiceParam paramNoInvoice = new SearchByCaIdNoInvoiceParam();
                    paramNoInvoice.CustomerId = (string)o[1];
                    OnSearchResultByCaIdNoInvoice(paramNoInvoice);
                }
            }
        }
        #endregion

        #region Search From SAP
        [EventSubscription(EventTopicNames.InvoiceSearchedFromSAP, Thread = ThreadOption.UserInterface)]
        public void InvoiceSearchedFromSAPHandler(object sender, EventArgs<SAPSearchParam> e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_InvoiceSearchedFromSAPDoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_InvoiceSearchedFromSAPCompleted);
            bgWorker.RunWorkerAsync(e.Data);
            WaitingForm.ShowForm(this.FindForm(WorkspaceNames.CenterWorkspace));
        }

        void bgWorker_InvoiceSearchedFromSAPDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SAPSearchParam param = (SAPSearchParam)e.Argument;
                List<Invoice> invoices = _billingService.SearchInvoiceFromSAP(param);
                object[] o = new object[] { invoices, param.CaId };
                e.Result = o;
                WaitingForm.HideForm();
            }
            catch (Exception ex)
            {
                WaitingForm.HideForm();
                Logger.WriteError(Logger.Module.POS, "���Ң�����˹��ҡ SAP", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(this.FindForm(WorkspaceNames.CenterWorkspace), EErrorInModule.POS, ex);
            }
        }

        void bgWorker_InvoiceSearchedFromSAPCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (null != e.Result)
                {
                    object[] o = (object[])e.Result;
                    List<Invoice> invoices = (List<Invoice>)o[0];
                    if (invoices.Count > 0)
                    {

                        //Comment because.. Now, BP & CA are retrieved from Realtime-pos.
                        //if (Session.Branch.BranchLevel == "3")
                        //{
                        //    List<CaAndBpInfo> list = _billingService.GetCaAndBpOtherBranch(invoices[0].CaId);
                        //    _billingService.SaveCaAndBpOtherBranch(list);
                        //}

                        InvoicesAddedToList(invoices);
                    }
                    else
                    {
                        HistoryViewParam param = new HistoryViewParam((string)o[1], true);
                        OnViewHistoryClick(param);
                    }
                }
            }
            catch (Exception ex)
            {
                WaitingForm.HideForm();
                Logger.WriteError(Logger.Module.POS, "������鹡�ä��Ң�����˹��ҡ SAP", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(this.FindForm(WorkspaceNames.CenterWorkspace), EErrorInModule.POS, ex);
            }
        }
        #endregion

        #region Search From OneTouch
        [EventSubscription(EventTopicNames.InvoiceSearchedFromOneTouch, Thread = ThreadOption.UserInterface)]
        public void InvoiceSearchedFromOneTouchHandler(object sender, EventArgs<OneTouchSearchParam> e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_InvoiceSearchedFromOneTouchDoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_InvoiceSearchedFromOneTouchCompleted);
            bgWorker.RunWorkerAsync(e.Data);
            WaitingForm.ShowForm(this.FindForm(WorkspaceNames.CenterWorkspace));
        }

        void bgWorker_InvoiceSearchedFromOneTouchDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                OneTouchSearchParam param = (OneTouchSearchParam)e.Argument;
                List<Invoice> invoices = _billingService.SearchInvoiceFromOneTouch(param);
                object[] o = new object[] { invoices, param.NotificationNo };
                e.Result = o;
                WaitingForm.HideForm();
            }
            catch (Exception ex)
            {
                WaitingForm.HideForm();
                Logger.WriteError(Logger.Module.POS, "���Ң�����˹��ҡ OneTouch", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(this.FindForm(WorkspaceNames.CenterWorkspace), EErrorInModule.POS, ex);
            }
        }

        void bgWorker_InvoiceSearchedFromOneTouchCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (null != e.Result)
                {
                    object[] o = (object[])e.Result;
                    List<Invoice> invoices = (List<Invoice>)o[0];
                    if (invoices.Count > 0)
                    {

                        //Comment because.. Now, BP & CA are retrieved from Realtime-pos.
                        //if (Session.Branch.BranchLevel == "3")
                        //{
                        //    List<CaAndBpInfo> list = _billingService.GetCaAndBpOtherBranch(invoices[0].CaId);
                        //    _billingService.SaveCaAndBpOtherBranch(list);
                        //}

                        InvoicesAddedToList(invoices);
                    }
                    else
                    {
                        HistoryViewParam param = new HistoryViewParam((string)o[1], true);
                        OnViewHistoryClick(param);
                    }
                }
            }
            catch (Exception ex)
            {
                WaitingForm.HideForm();
                Logger.WriteError(Logger.Module.POS, "������鹡�ä��Ң�����˹��ҡ OneTouch", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(this.FindForm(WorkspaceNames.CenterWorkspace), EErrorInModule.POS, ex);
            }
        }
        #endregion

        #region Search By Detail
        //[EventSubscription(EventTopicNames.BillSearchedByCustomerDetail, Thread = ThreadOption.UserInterface)]
        //public void BillSearchedByDetailHandler(object sender, EventArgs<CustomerSearchParam> e)
        //{
        //    BackgroundWorker bgWorker = new BackgroundWorker();
        //    bgWorker.DoWork += new DoWorkEventHandler(bgWorker_BillSearchedByDetailDoWork);
        //    bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_BillSearchedByDetailCompleted);
        //    bgWorker.RunWorkerAsync(e.Data);
        //    WaitingForm.ShowForm(this.FindForm(WorkspaceNames.CenterWorkspace));
        //}

        //void bgWorker_BillSearchedByDetailDoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        CustomerSearchParam param = (CustomerSearchParam)e.Argument;
        //        List<BillSearchDetail> bills = _billingService.SearchBillByCustomerDetail(param);
        //        e.Result = bills;
        //        WaitingForm.HideForm();
        //    }
        //    catch (Exception ex)
        //    {
        //        WaitingForm.HideForm();
        //        Logger.WriteError(Logger.Module.POS, "���Ң�����˹��ҡ��������´�١˹��", ex.ToString());
        //        ClientExceptionController.ShowExceptionDialog(this.FindForm(WorkspaceNames.CenterWorkspace), EErrorInModule.POS, ex);
        //    }
        //}

        //void bgWorker_BillSearchedByDetailCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if (null != e.Result)
        //    {
        //        List<BillSearchDetail> bills = (List<BillSearchDetail>)e.Result;

        //        if (bills.Count > 0)
        //        {
        //            View.Bills = bills;
        //            ShowView();
        //        }
        //        else
        //        {
        //            MessageBox.Show(this.FindForm(WorkspaceNames.CenterWorkspace), "��辺�����Ŵѧ�����������к�", "��ͤ���",
        //                MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //}
        #endregion

        #region Search By Group Invoice No
        [EventSubscription(EventTopicNames.InvoiceSearchedByGroupInvoiceNo, Thread = ThreadOption.UserInterface)]
        public void InvoiceSearchedByGroupInoviceNoHandler(object sender, EventArgs<GroupInvoiceSearchParam> e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_InvoiceSearchedByGroupInvoiceNoDoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_InvoiceSearchedByGroupInvoiceNoCompleted);
            bgWorker.RunWorkerAsync(e.Data);
            WaitingForm.ShowForm(this.FindForm(WorkspaceNames.CenterWorkspace));
        }

        void bgWorker_InvoiceSearchedByGroupInvoiceNoDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                GroupInvoiceSearchParam param = (GroupInvoiceSearchParam)e.Argument;
                List<Invoice> invoices = _billingService.SearchInvoiceByGroupInvoiceNo(param);
                e.Result = invoices;
                WaitingForm.HideForm();
            }
            catch (Exception ex)
            {
                WaitingForm.HideForm();
                Logger.WriteError(Logger.Module.POS, "���Ң�����˹��ҡ GroupInvoiceNo", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(this.FindForm(WorkspaceNames.CenterWorkspace), EErrorInModule.POS, ex);
            }
        }

        void bgWorker_InvoiceSearchedByGroupInvoiceNoCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (null != e.Result)
            {
                List<Invoice> invoices = (List<Invoice>)e.Result;
                List<Invoice> groupInvoices = new List<Invoice>();

                if (invoices.Count > 0)
                {
                    if (invoices[0].PmId != null && invoices[0].PmId.ToUpper() == "G")
                    {
                        SAPSearchParam _searchParam = new SAPSearchParam();
                        _searchParam.CaId = StringConvert.ToString(invoices[0].CaId.Trim());
                        _searchParam.CaDocNo = null;
                        _searchParam.InvoiceNo = null;
                        _searchParam.PosId = Session.Terminal.Id;

                        try //not show the exception when could not connect SAP
                        {
                            groupInvoices = _billingService.SearchInvoiceFromSAP(_searchParam);
                        }
                        catch
                        {   
                            //�������ö�������͡Ѻ�к� SAP ��
                            //Notthing
                            //MessageBox.Show(this.FindForm(WorkspaceNames.CenterWorkspace), "�������ö�������͡Ѻ�к� SAP ��", "��ͤ���",
                            //                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        foreach (Invoice inv in invoices)
                        {
                            if (inv.GroupInvoiceReceiptType != null && groupInvoices.Count > 0)
                            {
                                inv.GroupInvoiceReceiptType = groupInvoices[0].GroupInvoiceReceiptType;
                            }
                        }
                    }

                    InvoicesAddedToList(invoices);
                }
                else
                {
                    MessageBox.Show(this.FindForm(WorkspaceNames.CenterWorkspace), "��辺�����Ŵѧ�����������к�", "��ͤ���",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion
        
        #region Subscription Events
        [EventSubscription(EventTopicNames.ActionSuccess, Thread = ThreadOption.UserInterface)]
        public void ActionSuccessHandler(object sender, EventArgs e)
        {
            if (WorkItem.Workspaces[WorkspaceNames.ModalWindows].ActiveSmartPart == View)
            {
                ((UserControl)View).ParentForm.Close();
            }
        }

        [EventSubscription(EventTopicNames.OnCloseAllViews, Thread = ThreadOption.UserInterface)]
        public void OnCloseAllViewsHandler(object sender, EventArgs e)
        {
            OnCloseView();
        }
        #endregion

        #region Publication Events
        [EventPublication(EventTopicNames.InvoiceItemAdd, PublicationScope.Global)]
        public event EventHandler<EventArgs<List<Invoice>>> InvoicesAddedToPayingList;
        public void InvoicesAddedToList(List<Invoice> invoices)
        {
            if (InvoicesAddedToPayingList != null)
                InvoicesAddedToPayingList(this, new EventArgs<List<Invoice>>(invoices));
        }

        [EventPublication(EventTopicNames.PaymentItemAdd, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<List<Bill>>> BillsAddedToPayingList;
        public void BillsAddedToList(List<Bill> bills)
        {                    
            if (BillsAddedToPayingList != null)
                BillsAddedToPayingList(this, new EventArgs<List<Bill>> (bills));
        }

        [EventPublication(EventTopicNames.ViewHistoryClick, PublicationScope.Global)]
        public event EventHandler<EventArgs<HistoryViewParam>> ViewHistoryClick;
        public void OnViewHistoryClick(HistoryViewParam param)
        {
            if (ViewHistoryClick != null)
                ViewHistoryClick(this, new EventArgs<HistoryViewParam>(param));
        }

        [EventPublication(EventTopicNames.ViewBillDetailClick, PublicationScope.Global)]
        public event EventHandler<EventArgs<BillDetailSearchParam>> ViewBillDetailClick;
        internal void OnViewBillDetailClick(BillDetailSearchParam param)
        {
            if (ViewBillDetailClick != null)
                ViewBillDetailClick(this, new EventArgs<BillDetailSearchParam>(param));
        }

        /// <summary>
        /// DCR 67-020
        /// </summary>
        [EventPublication(EventTopicNames.SearchResultByCaIdOnInvoice, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<SearchByCaIdNoInvoiceParam>> SearchResultByCaIdNoInvoice;
        public void OnSearchResultByCaIdNoInvoice(SearchByCaIdNoInvoiceParam param) 
        {
            if (SearchResultByCaIdNoInvoice != null)
                SearchResultByCaIdNoInvoice(this, new EventArgs<SearchByCaIdNoInvoiceParam>(param));
        }

        #endregion

        public void ShowView()
        {
            WindowSmartPartInfo info = new WindowSmartPartInfo();
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
            info.Keys.Add(WindowWorkspaceSetting.AcceptButton, View.OkButton);
            info.Keys.Add(WindowWorkspaceSetting.CancelButton, View.CancelButton);
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Title = " �š�ä��Ң�����˹��������к�";
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(View, info);
        }

        public List<Invoice> GetInvoiceDetail(CustomerSearchParam param)
        {
            return _billingService.SearchInvoiceByCustomerId(param);
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

