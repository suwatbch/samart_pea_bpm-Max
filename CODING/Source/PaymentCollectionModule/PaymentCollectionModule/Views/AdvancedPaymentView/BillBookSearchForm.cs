using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.Services;

namespace PEA.BPM.PaymentCollectionModule.Views.AdvancedPaymentView
{
    public partial class BillBookSearchForm : Form
    {
        private IBillingService _billingService;

        private BillBook _selectedBillBook;
        public BillBook SelectedBillBook
        {
            get { return _selectedBillBook; }
        }        

        public BillBookSearchForm(IBillingService billingService)
        {
            InitializeComponent();

            _billingService = billingService;
            billBookDataGridView.AutoGenerateColumns = false;
            idSearchTextBox.Focus();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _selectedBillBook = null;
            this.DialogResult = DialogResult.Cancel;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectBillBook();
        }

        private void billBookDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SelectBillBook();
            }
        }

        private void SelectBillBook()
        {
            DataGridViewRow currentRow = billBookDataGridView.CurrentRow;

            if (null == currentRow)
            {
                MessageBox.Show("กรุณาเลือกสมุดจ่ายบิลที่ต้องการ หรือ กดปุ่ม 'ยกเลิก' เพื่อออกจากหน้าจอ", "ข้อความ",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                nameSearchTextBox.SelectAll();
                nameSearchTextBox.Focus();
            }
            else
            {
                _selectedBillBook = (BillBook)billBookDataGridView.CurrentRow.DataBoundItem;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchBillBook();
        }

        private void searchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchBillBook();
            }
        }

        private void SearchBillBook()
        {
            string bookId = StringConvert.ToString(bookIdSearchTextBox.Text);
            if (bookId != string.Empty)
            {
                bookId = string.Format("{0}{1}", Session.Branch.Id, bookId);
            }

            string agentId = StringConvert.ToString(idSearchTextBox.Text);
            string agentName = StringConvert.ToString(nameSearchTextBox.Text);

            List<BillBook> results = _billingService.SearchBillBookByDetail(bookId, agentId, agentName);

            billBookDataGridView.DataSource = new BindingList<BillBook>(results);
            billBookDataGridView.Focus();
        }

        private void billBookDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            System.Diagnostics.Debug.Print("rowIndex{0} : ColumnIndex{1} ", e.RowIndex, e.ColumnIndex);

            if (e.RowIndex > -1 && (e.ColumnIndex >= 1 && e.ColumnIndex <= 5))
            {
                SelectBillBook();
            }
        }
    }
}