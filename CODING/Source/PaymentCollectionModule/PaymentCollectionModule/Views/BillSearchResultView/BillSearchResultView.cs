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
using System.Collections.Generic;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Data;
using System.ComponentModel;

namespace PEA.BPM.PaymentCollectionModule
{
    [SmartPart]
    public partial class BillSearchResultView : UserControl, IBillSearchResultView
    {
        private List<BillSearchDetail> _bills;
       
        public BillSearchResultView()
        {
            InitializeComponent();
            customerDataGridView.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public BillSearchResultViewPresenter Presenter
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
        }

        #region ICustomerSearchResultView Members

        public List<BillSearchDetail> Bills
        {
            set
            {
                this._bills = value;
                LoadBillsToGrid();
            }
        }

        public Button OkButton
        {
            get { return addButton; }
        }

        public Button CancelButton
        {
            get { return cancelButton; }
        }

        #endregion

        #region +++ Command Handler +++

        private void addButton_Click(object sender, EventArgs e)
        {
            AddSelectedItems();
        }

        private void chkAllCheckBox_Click(object sender, EventArgs e)
        {
            UpdateRowCheckedBox();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void customerDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4))
            {
                BillSearchDetail bill = (BillSearchDetail)customerDataGridView.Rows[e.RowIndex].DataBoundItem;

                if (bill.ToPayAmount.Value <= 0)
                {
                    MessageBox.Show("�������ö���͡��¡�ôѧ�������  �����˹���ҧ����", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                chkAllCheckBox.Checked = false;
                UpdateRowCheckedBox();
                customerDataGridView.Rows[e.RowIndex].Cells[0].Value = true;

                AddSelectedItems();
            }
        }

        private void customerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                customerDataGridView.EndEdit();
                BillSearchDetail bill = (BillSearchDetail)customerDataGridView.Rows[e.RowIndex].DataBoundItem;
                string customerId = bill.CustomerId;

                switch (customerDataGridView.Columns[e.ColumnIndex].Name)
                {
                    case "checkedDgColumn":
                        if (bill.ToPayAmount.Value <= 0)
                        {
                            MessageBox.Show("�������ö���͡��¡�ôѧ�������  �����˹���ҧ����", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            customerDataGridView.Rows[e.RowIndex].Cells[0].Value = false;
                            return;
                        }
                        for (int i = 0; i < customerDataGridView.Rows.Count; i++)
                        {
                            if (customerDataGridView.Rows[i].Cells["checkedDgColumn"].Value == null || (bool)customerDataGridView.Rows[i].Cells["checkedDgColumn"].Value == false)
                            {
                                chkAllCheckBox.Checked = false;
                                return;
                            }
                        }
                        chkAllCheckBox.Checked = true;
                        break;
                    case "customerIdDgColumn":
                        HistoryViewParam param = new HistoryViewParam(customerId, bill.NetworkMode == NetworkMode.OnlineToBpmServer);
                        _presenter.OnViewHistoryClick(param);
                        break;
                    case "amountDgColumn":
                        if (bill.ToPayAmount.Value <= 0)
                        {
                            MessageBox.Show("�����˹���ҧ��������Ѻ�ѭ���ʴ��ѭ�ҹ��", "��ͤ���", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                        }
                        else
                        {
                            BillDetailSearchParam bParam = new BillDetailSearchParam(customerId);
                            bParam.IsOtherBranch = bill.NetworkMode == NetworkMode.OnlineToBpmServer;
                            _presenter.OnViewBillDetailClick(bParam);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void customerDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddSelectedItems();
            }
        }

        private void BillSearchResultView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ParentForm.Close();
            }
        }

        #endregion

        #region  +++ Custom Function +++

        private void LoadBillsToGrid()
        {
            chkAllCheckBox.Checked = false;
            customerDataGridView.Enabled = false;
            customerDataGridView.DataSource = _bills.ToArray();
            customerDataGridView.Enabled = true;

            for (int i = 0; i < customerDataGridView.Rows.Count; i++)
            {
                if ((decimal?)customerDataGridView.Rows[i].Cells[4].Value <= 0)
                {
                    DataGridViewCheckBoxCell chkBoxCell = (DataGridViewCheckBoxCell)customerDataGridView.Rows[i].Cells[0];
                    chkBoxCell.ReadOnly = true;
                }
            }
        }

        private void AddSelectedItems()
        {
            List<Invoice> invoices = new List<Invoice>();

            for (int i = 0; i < customerDataGridView.Rows.Count; i++)
            {
                object isChecked = customerDataGridView.Rows[i].Cells["checkedDgColumn"].Value;
                if (null != isChecked && true == (bool)isChecked)
                {
                    BillSearchDetail bill = (BillSearchDetail)customerDataGridView.Rows[i].DataBoundItem;

                    CustomerSearchParam param = new CustomerSearchParam();
                    param.CaId = bill.CustomerId;
                    param.IsOtherBranch = bill.NetworkMode == NetworkMode.OnlineToBpmServer;
                    
                    invoices.AddRange(_presenter.GetInvoiceDetail(param));
                }
            }

            if (invoices.Count > 0)
            {
                _presenter.InvoicesAddedToList(invoices);
            }
            else
            {
                MessageBox.Show("��س����͡��¡�÷���ͧ��ê���˹��", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void UpdateRowCheckedBox()
        {
            if (chkAllCheckBox.Checked == true)
            {
                for (int i = 0; i < customerDataGridView.Rows.Count; i++)
                {
                    BillSearchDetail bill = (BillSearchDetail)customerDataGridView.Rows[i].DataBoundItem;

                    if (bill.ToPayAmount.Value > 0)
                    {
                        customerDataGridView.Rows[i].Cells["checkedDgColumn"].Value = true;
                        customerDataGridView.Rows[i].Cells["checkedDgColumn"].Selected = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < customerDataGridView.Rows.Count; i++)
                {
                    customerDataGridView.Rows[i].Cells["checkedDgColumn"].Value = false;
                    customerDataGridView.Rows[i].Cells["checkedDgColumn"].Selected = false;
                }
            }
        }

        #endregion


        #region IBillSearchResultView Members


        public void ConfirmShowHistory(string caId, int itemCount, bool isOtherBranch)
        {
            // DCR 67-020 �ʴ� messagebox confirm
            //if (itemCount == 0)
            //{
            //    if (MessageBox.Show("��辺������˹���ҧ���� ��ͧ��ô٢����Ż���ѵԡ�ê���������� ?", "��ͤ���", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //        _presenter.OnViewHistoryClick(new HistoryViewParam(caId, isOtherBranch));
            //}


            // DCR 67-020 Rev.1 �� messagebox confirm �͡
            _presenter.OnViewHistoryClick(new HistoryViewParam(caId, isOtherBranch));

        }

        #endregion
    }
}

