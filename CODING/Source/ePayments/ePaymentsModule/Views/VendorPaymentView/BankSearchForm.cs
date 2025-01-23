using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.ePaymentsModule.Views.VendorPaymentView
{
    public partial class BankSearchForm : Form
    {
        List<Bank> _banks;
        bool _isDeposit;

        private Bank _selectedBank;
        public Bank SelectedBank
        {
            get { return _selectedBank; }
        }

        public BankSearchForm(bool isDeposit)
        {
            InitializeComponent();
            _isDeposit = isDeposit;
            if (_isDeposit == true)
            {
                //_banks = CodeTable.Instant.ListBanksByBusinessPlace(Session.Branch.Id.Substring(0, 4));
                _banks = CodeTable.Instant.ListBanksFromDepositByBusinessPlace(Session.Branch.Id.Substring(0, 4));
            }
            else
            {
                _banks = CodeTable.Instant.ListBanks();
            }
            bankDataGridView.AutoGenerateColumns = false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectBank();
        }

        private void SelectBank()
        {
            DataGridViewRow currentRow = bankDataGridView.CurrentRow;

            if (null == currentRow)
            {
                MessageBox.Show("กรุณาเลือกธนาคารที่ต้องการ หรือ กดปุ่ม 'ยกเลิก' เพื่อออกจากหน้าจอ", "ข้อ",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                keySearchTextBox.SelectAll();
                keySearchTextBox.Focus();
            }
            else
            {
                _selectedBank = (Bank)bankDataGridView.CurrentRow.DataBoundItem;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _selectedBank = null;
            this.DialogResult = DialogResult.Cancel;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchBank();
        }

        private void searchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchBank();
            }
        }

        private void SearchBank()
        {
            List<Bank> results;

            string key = keySearchTextBox.Text.Trim();
            string name = nameSearchTextBox.Text.Trim();

            //if (key + name == "")
            //{
            //    results = new List<Bank>();
            //}
            //else if(key==string.Empty)
            //{
            //    results = _banks.FindAll(delegate(Bank bank)
            //    {
            //        return bank.BankName.IndexOf(name) > -1;
            //    }
            //    );                
            //}
            //else if (name == string.Empty)
            //{
            //    results = _banks.FindAll(delegate(Bank bank)
            //    {
            //        return bank.BankKey.IndexOf(key) > -1;
            //    }
            //    );
            //}
            //else
            //{
            results = _banks.FindAll(delegate(Bank bank)
            {
                return (bank.BankKey.IndexOf(key) > -1) && (bank.BankName.IndexOf(name) > -1);
            }
            );
            //}

            bankDataGridView.DataSource = new BindingList<Bank>(results);
            bankDataGridView.Focus();
        }

        private void BankSearchForm_Shown(object sender, EventArgs e)
        {
            keySearchTextBox.Focus();
        }

        private void bankDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SelectBank();
        }

        private void bankDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectBank();
        }
    }
}