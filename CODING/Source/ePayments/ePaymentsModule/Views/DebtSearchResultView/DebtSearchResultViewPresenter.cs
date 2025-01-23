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
using System.Collections.Generic;
using System.Windows.Forms;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.ePaymentsModule.Interface.Services;
using PEA.BPM.ePaymentsModule.Interface.Constants;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;


namespace PEA.BPM.ePaymentsModule
{
    public class DebtSearchResultViewPresenter : Presenter<IDebtSearchResultView>
    {
        private IBillingService _billingService;

        [InjectionConstructor]
        public DebtSearchResultViewPresenter([ServiceDependency] IBillingService billingService)
        {
            _billingService = billingService;
        }

        [EventPublication(EventTopicNames.GetDebtDetail, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<ClearifyInfo>> GetDebtDetail;
        internal void OnGetDebtDetail(ClearifyInfo clearifyItem)
        {
            if (GetDebtDetail != null)
                GetDebtDetail(this, new EventArgs<ClearifyInfo>(clearifyItem));
        }

        [EventSubscription(EventTopicNames.SearchDebt, Thread = ThreadOption.UserInterface)]
        public void SearchDebtHandler(object sender, EventArgs<SearchDebtParam> e)
        {
            try
            {
                SearchDebtParam searchConn = (SearchDebtParam)e.Data;
                List<ClearifyInfo> debtList = _billingService.SearchDebtService(searchConn);
                if (debtList.Count > 0)
                {
                    View.AddDebtList(debtList);
                    ShowView();
                }
                else
                {
                    MessageBox.Show("��辺�����Ŵѧ�����������к�", "��ͤ���", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.EPAYMENT, "�������������´��ش���º��", ex.ToString());
                MessageBox.Show("�������ö�Դ��͡Ѻ�ҹ�������� �ô�Դ��ͼ������к�\n", "��ͼԴ��Ҵ",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowView()
        {
            PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
            //info.Keys.Add(WindowWorkspaceSetting.CancelButton, View.CancelButton);
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.Title = " :: ��������´������˹����Դ��Ҵ";
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(View, info);
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

