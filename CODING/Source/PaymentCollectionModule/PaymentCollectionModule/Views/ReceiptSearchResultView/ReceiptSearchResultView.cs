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

namespace PEA.BPM.PaymentCollectionModule
{
    [SmartPart]
    public partial class ReceiptSearchResultView : UserControl, IReceiptSearchResultView
    {
        private List<Receipt> _receipts;

        public ReceiptSearchResultView()
        {
            InitializeComponent();
            searchResultDataGridView.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public ReceiptSearchResultViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        #region IReceiptSearchResultView Members

        public List<Receipt> Receipts
        {
            set
            {
                this._receipts = value;
                LoadReceiptsToGrid();
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

        #region Event Handlering

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();

            chkAllCheckBox.Checked = false;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddSelectedItems();
        }

        private void searchResultDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)13)
            {
                AddSelectedItems();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        #endregion

        #region Private Functions

        private void AddSelectedItems()
        {
            List<Receipt> receipts = new List<Receipt>();

            for (int i = 0; i < searchResultDataGridView.Rows.Count; i++)
            {
                object isChecked = searchResultDataGridView.Rows[i].Cells["checkedDgColumn"].Value;
                if (null != isChecked && true == (bool)isChecked)
                {
                    receipts.Add(((Receipt)searchResultDataGridView.Rows[i].DataBoundItem));
                }
            }

            if (receipts.Count > 0)
            {
                if (receipts.Count > 1 && _presenter.WorkItem.State["IsCancelPage"].ToString() == "True")
                {
                    string paymentId = receipts[0].PaymentId;
                    for (int i=1; i<receipts.Count; i++)
                    {
                        if (receipts[i].PaymentId != paymentId)
                        {
                            MessageBox.Show("����稷�����͡�е�ͧ����㹡�ê��Ф������ǡѹ", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        }
                    }
                }

                _presenter.OnReceiptItemAdd(receipts);
            }
            else
            {
                if (_presenter.WorkItem.State["IsCancelPage"].ToString() == "True")
                {
                    MessageBox.Show("��س����͡��¡�÷���ͧ���¡��ԡ", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("��س����͡��¡�÷���ͧ��þ�������", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }                
            }
        }

        private void LoadReceiptsToGrid()
        {
            searchResultDataGridView.Enabled = false;
            searchResultDataGridView.DataSource = _receipts.ToArray();
            searchResultDataGridView.Enabled = true;
        }

        #endregion

        private void searchResultDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                
                switch ( searchResultDataGridView.Columns[e.ColumnIndex].Name )
                {
                    case "checkedDgColumn":
                        if(chkAllCheckBox.Checked==true) chkAllCheckBox.Checked=false;
                        break;
                    default:
                        break;
                }
            }
        }

        private void chkAllCheckBox_Click(object sender, EventArgs e)
        {
            bool isChecked = chkAllCheckBox.Checked;

            for (int i = 0; i < searchResultDataGridView.Rows.Count; i++)
            {
                searchResultDataGridView.Rows[i].Cells["checkedDgColumn"].Value = isChecked;
            }
        }
    }
}

