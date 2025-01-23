//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// This class is the concrete implementation of a View in the Model-View-Presenter 
// pattern. Communication between the Presenter and this class is acheived through 
// an interface to facilitate separation and testability.
// Note that the Presenter generated by the same recipe, will automatically be created
// by CAB through [CreateNew] and bidirectional references will be added.
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Collections.Generic;
using System.ComponentModel;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule
{
    [SmartPart]
    public partial class ToBeReprintedView : UserControl, IToBeReprintedView
    {
        public ToBeReprintedView()
        {
            InitializeComponent();
            transactionDataGridView.AutoGenerateColumns = false;

            //if (Authorization.IsAuthorized(SecurityNames.POSObserver, false))
            if (Session.Work.Id == null)
                okButton.Enabled = false;
            else
                okButton.Enabled = true;
        }

        public void EnableSaveButton(bool enable)
        {
            okButton.Enabled = enable;
        }

        public bool AddReceipts(List<Receipt> receipts)
        {
            if (receipts.Count > 0)
            {
                List<ToBeReprintedReceipt> toBeReprintReceipts = (List<ToBeReprintedReceipt>)_presenter.WorkItem.State["ToBeReprintedReceipts"];
                if (ModuleHelper.CheckAddedReprintReceiptItem(toBeReprintReceipts, receipts))
                {
                    MessageBox.Show("��¡�ôѧ��������������¡�÷���ͧ��þ�����������", "��ͼԴ��Ҵ",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                foreach (Receipt r in receipts)
                {
                    ToBeReprintedReceipt tbr = new ToBeReprintedReceipt(r);
                    tbr.IsChecked = true;

                    toBeReprintReceipts.Add(tbr);
                }

                _presenter.WorkItem.State["ToBeReprintedReceipts"] = toBeReprintReceipts;

                RedrawScreen();
                okButton.Focus();
            }

            return true;
        }

        private void RedrawScreen()
        {
            List<ToBeReprintedReceipt> toBeReprintReceipts = (List<ToBeReprintedReceipt>)_presenter.WorkItem.State["ToBeReprintedReceipts"];
            transactionDataGridView.DataSource = new BindingList<ToBeReprintedReceipt>(toBeReprintReceipts);
        }


        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public ToBeReprintedViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();
            _presenter.WorkItem.State["IsCancelPage"] = "False";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        public void ClearData()
        {
            _presenter.WorkItem.State["ToBeReprintedReceipts"] = new List<ToBeReprintedReceipt>();
            RedrawScreen();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.ReceiptReprintingNow, true, "��������������Ѻ�Թ"))
            {
                List<ToBeReprintedReceipt> toBeReprintReceipts = (List<ToBeReprintedReceipt>)_presenter.WorkItem.State["ToBeReprintedReceipts"];

                List<string> receiptIds = new List<string>();

                foreach (ToBeReprintedReceipt r in toBeReprintReceipts)
                {
                    if (r.IsChecked)
                    {
                        receiptIds.Add(r.ReceiptId);
                    }
                }

                _presenter.Print(receiptIds);
            }
        }
    }
}

