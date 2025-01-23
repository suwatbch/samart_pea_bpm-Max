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
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using System.ComponentModel;
using PEA.BPM.BillPrintingModule.BillPrintingUtilities;
using System.Globalization;

namespace PEA.BPM.BillPrintingModule
{
    [SmartPart]
    public partial class GreenBillView : UserControl, IDirectDebitBillView
    {
        #region "Variables and Properties"

        private bool _clearCmdFlag = false;

        private List<Bank> _bank;
        private List<PrintableIdByBank> _billByBank;
        private PrintMode _printMode= PrintMode.DDGreen;
        private string _selectedBankId = null;

        private enum PrintMode
        {
            DDGreen = 1,
            DDBlue
        }

        public List<PrintableIdByBank> BillByBank
        {
            get { return _billByBank; }
            set 
            {
                _billByBank = value;
                BindDataToDataGrid();
            }
        }
        public List<Bank> Bank
        {
            get { return _bank; }
            set {  _bank = value; }
        }

        #endregion

        #region "Code Generated"

        public GreenBillView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public GreenBillViewPresenter Presenter
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
            InitialControlValue();
            //bind bank date into comboBox
            _presenter.LoadBankComboBox(Session.Branch.Id);

            if (Session.Branch.Id == "Z00000")
            {
                _printMode = PrintMode.DDBlue;
                blueRb.Checked = true;
                printCaptionTxt.Text = "�������駤��俿��\n �ѡ�ѭ�ո�Ҥ�� (��ſ��)";
            }
            else
            {
                _printMode = PrintMode.DDGreen;
                greenRb.Checked = true;
                printCaptionTxt.Text = "�������駤��俿��\n �ѡ�ѭ�ո�Ҥ�� (�������)";
            }
        }

        public void OnWaitCursor(bool set)
        {
            if (set)
            {
                this.Cursor = Cursors.WaitCursor;
                printButton.Enabled = false;
            }
            else
            {
                this.Cursor = Cursors.Default;
                printButton.Enabled = true;
            }
        }

        #endregion

        #region "Event Handling"

        private void Print()
        {
            if (ValidateBeforeSubmit())
            {
                DialogResult result = MessageBox.Show("- ��  Yes \t���ͷӡ�þ������¡�÷ѹ��\n- ��  No \t����¡��ԡ��þ����", MessageBoxText.CaptionConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                        this.Cursor = Cursors.AppStarting;
                        List<PrintableIdByBank> _recs = new List<PrintableIdByBank>();
                        List<int> toRm = new List<int>();

                        for (int i = 0; i < greenBillDataGridView.Rows.Count; i++)
                        {
                            PrintableIdByBank _rec = new PrintableIdByBank();

                            object isChecked = greenBillDataGridView.Rows[i].Cells["checkColumn"].Value;
                            if (isChecked != null && (bool)isChecked == true)
                            {
                                toRm.Add(i); //add index to be removed items
                                _rec = (PrintableIdByBank)greenBillDataGridView.Rows[i].DataBoundItem;                            
                                _recs.Add(_rec);
                            }
                        }

                        _presenter.PrintGreenBillByBank(_recs);

                        //remove printed lots
                        for(int j = toRm.Count-1; j >= 0; j--)
                            greenBillDataGridView.Rows.RemoveAt(toRm[j]);

                        this.Cursor = Cursors.Default;
                }
            }
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            InitialControlValue();
        }       
        
        private void SearchBankLot()
        {
            if (ValidateDate(bankDueDateText.Text))
            {
                if (_selectedBankId == null)
                {
                    MessageBox.Show(null, "��س��к� \"��Ҥ��\" ����ͧ��ä������˹��", "���������ú��ǹ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bankIdText.Focus();
                    bankIdText.SelectAll();
                    return;
                }

                this.Cursor = Cursors.AppStarting;
                GreenBillParam param = new GreenBillParam();
                param.CommBranchId = Session.Branch.Id;
                param.PrintedBy = Session.User.Id;

                if (bankIdText.Text != string.Empty && bankIdText.Text.Length == 5)
                    param.BankId = bankIdText.Text;

                if (greenRb.Checked)
                    param.BillType = (int)PEA.BPM.BillPrintingModule.Interface.BusinessEntities.BillType.GreenBillByBank;
                else if (blueRb.Checked)
                    param.BillType = (int)PEA.BPM.BillPrintingModule.Interface.BusinessEntities.BillType.BlueBillByBank;
                else
                {
                    MessageBox.Show(MessageBoxText.MsgWrongBillType, "���������˹�����١��ͧ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bankDueDateText.Focus();
                    return;
                }

                if (billPredRb.Checked)
                    param.BillPred = billPredText.Text.Substring(3, 4) + billPredText.Text.Substring(0, 2);
                else //yyyymmdd
                    param.BankDueDate = bankDueDateText.Text.Substring(6, 4) + bankDueDateText.Text.Substring(3, 2) + bankDueDateText.Text.Substring(0, 2);

                _presenter.CheckExistByBank(param);

                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show(MessageBoxText.MsgWrongFormatDueDate, MessageBoxText.CaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bankDueDateText.Focus();
            }

            checkAllCheckBox.Checked = false;
        }

        private void bankComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchBankLot();
            }
        }

        private void searchBt_Click(object sender, EventArgs e)
        {
            SearchBankLot();
        }
        
        private void checkAllCheckBox_Click(object sender, EventArgs e)
        {
            if (checkAllCheckBox.Checked == true)
            {
                //checkAllTextBox
                for (int i = 0; i < greenBillDataGridView.Rows.Count; i++)
                {
                    greenBillDataGridView.Rows[i].Cells["checkColumn"].Value = (Object)true;
                }
            }
            else
            {
                for (int i = 0; i < greenBillDataGridView.Rows.Count; i++)
                {
                    greenBillDataGridView.Rows[i].Cells["checkColumn"].Value = (Object)false;
                }
            }
        }

        private void greenBillDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //for (int i = 0; i < greenBillDataGridView.Rows.Count - 1; i++)
                //{
                //    greenBillDataGridView.Rows[i].Cells["checkColumn"].Value = (object)false;
                //}
                //greenBillDataGridView.Rows[e.RowIndex].Cells["checkColumn"].Value = (object)true;
            }
        }
       
        private void bankIdText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (bankIdText.MaskCompleted)
                {
                    bankDueDateText.Focus();
                    bankDueDateText.SelectAll();
                }
                else
                {
                    bankSearchBt.Focus();
                }
            }
            else if (e.KeyCode == Keys.F12)
            {
                if (greenBillDataGridView.Rows.Count > 0)
                    Print();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (_clearCmdFlag)
                {
                    _clearCmdFlag = false;
                    DialogResult dlg = MessageBox.Show(null, "�س��ͧ�����ҧ�����ŷ���͹�����������������", "����͹", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dlg == DialogResult.Yes)
                        InitialControlValue();
                }
                else
                {
                    bankIdText.ResetText();
                    bankIdText.Focus();
                    _clearCmdFlag = true;
                }
            }
        }

        private void bankIdText_TextChanged(object sender, EventArgs e)
        {
            _clearCmdFlag = false;
            if (bankIdText.Text.Length > 0)
            {
                _selectedBankId = null;
                if (bankIdText.MaskCompleted)
                    _selectedBankId = bankIdText.Text;
            }
        }

        private void bankDueDateText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (greenRb.Checked)
                    greenRb.Focus();
                else if (blueRb.Checked)
                    blueRb.Focus();
            }
            else if (e.KeyCode == Keys.N)
            {
                IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
                bankDueDateText.Text = DateTime.Now.ToString("10MMyyyy", formatDate);
                searchBt.Focus();
            }
        }

        private void bankDueDateText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (bankDueDateText.Text.Replace("/", "").Trim() == string.Empty)
                    return;

                if (ValidateDate(bankDueDateText.Text))
                {
                    if (bankDueDateText.MaskCompleted)
                        searchBt.Focus();
                }
                else
                {
                    MessageBox.Show(MessageBoxText.MsgWrongFormatDueDate, MessageBoxText.CaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    bankDueDateText.ResetText();
                    bankDueDateText.SelectAll();
                }
            }
        }

        private void billPredRb_CheckedChanged(object sender, EventArgs e)
        {
            if (billPredRb.Checked)
            {
                billPredText.Enabled = true;
                billPredText.Focus();
                bankDueDateText.Enabled = false;
                bankDueDateText.ResetText();
            }
        }

        private void billPredText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (billPredText.Text.Replace("/", "").Trim() == string.Empty)
                    return;

                if (ValidateDate(billPredText.Text))
                {
                    if (billPredText.MaskCompleted)
                        bankIdText.Focus();
                }
                else
                {
                    MessageBox.Show(MessageBoxText.MsgWrongFormatBillPeriod, MessageBoxText.CaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    billPredText.ResetText();
                    billPredText.SelectAll();
                }
            }
            else if (e.KeyCode == Keys.N)
            {
                IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
                billPredText.Text = DateTime.Now.ToString("MMyyyy", formatDate);
                bankIdText.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (greenRb.Checked)
                    greenRb.Focus();
                else if (blueRb.Checked)
                    blueRb.Focus();
            }

        }

        private void greenRb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (billPredRb.Checked)
                    billPredText.Focus();
                else
                    bankDueDateText.Focus();
            }
        }

        private void blueRb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (billPredRb.Checked)
                    billPredText.Focus();
                else
                    bankDueDateText.Focus();
            }
        }


        #endregion

        #region "Function"

        private void BindDataToDataGrid()
        {
            if (_billByBank.Count == 0)
            {
                MessageBox.Show(null, "�к���辺��¡�����˹��  �Ҩ���ͧ�ҡ�ҡ\n\n   - ��͹���͹䢡�þ����Դ\n   - �١������������\n   - �ѧ����ա�á��껨ҡ SAP", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BindingList<PrintableIdByBank> bl = new BindingList<PrintableIdByBank>(_billByBank);
            bl.AllowNew = false;

            greenBillDataGridView.Rows.Clear();
            greenBillDataGridView.AutoGenerateColumns = false;
            greenBillDataGridView.DataSource = bl;
            greenBillDataGridView.Refresh();
        }

        private void InitialControlValue()
        {
            bankDueDateText.Clear();
            billPredText.Clear();
            greenBillDataGridView.Rows.Clear();
            bankIdText.Clear();
            bankIdText.Focus();
        }

        private bool ValidateBeforeSubmit()
        {
            bool isValidated = false;

            if (greenBillDataGridView.Rows.Count != 0)
            {
                for (int i = 0; i < greenBillDataGridView.Rows.Count; i++)
                {
                    object isChecked = greenBillDataGridView.Rows[i].Cells["checkColumn"].Value;
                    if (isChecked != null && (bool)isChecked == true)
                    {
                        isValidated = true;
                        break;
                    }
                }
            }

            if (!isValidated)
            {
                MessageBox.Show(MessageBoxText.MsgMissingPrePrintItem, MessageBoxText.CaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bankIdText.Focus();
                bankIdText.SelectAll();
            }

            return isValidated;
        }

        private bool ValidateDate(string date)
        {
            try
            {
            //    string _date = date;
            //    //input is dd/mm/yyyy
            //    date = date.Replace("/", "");
            //    int day = Convert.ToInt16(date.Substring(0, 2));
            //    int month = Convert.ToInt16(date.Substring(2, 2));
            //    int year = Convert.ToInt16(date.Substring(4, 4));

            //    if (day == 80)
            //    {
            //        if (month > 0 && month < 13)
            //            if (year > 1900 && year < 3000)
            //                return true;
            //    }
            //    else
                return CustomValidation.ValidateDate(date);
            }
            catch
            {
                return false;
            }
        }

        #endregion                 


        private void CountLotSize()
        {
            int lotSize = 0;
            foreach (DataGridViewRow r in greenBillDataGridView.Rows)
            {
                string lotDetail = (string)r.Cells["PrintDetail"].Value;
                string [] sp = lotDetail.Split('/');
                string lotSizeText = sp[sp.Length - 1];
                lotSizeText = lotSizeText.Replace("(","");
                lotSizeText = lotSizeText.Replace(")", "");
                lotSize += Convert.ToInt32(lotSizeText);
            }

            greenBillListViewGroupBox.Text = string.Format("��¡�á�͹����� ( {0} ��¡�� )", lotSize.ToString());
        }

        private void greenBillDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CountLotSize();
        }

        private void greenBillDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CountLotSize();
        }

        private void bankSearchBt_Click(object sender, EventArgs e)
        {
            BABankSelection bankDlg = new BABankSelection(_bank);
            bankDlg.ShowDialog();
            if (bankDlg.DialogResult == DialogResult.OK)
            {
                Bank chosenBank = bankDlg.ChesenBank;
                _selectedBankId = chosenBank.BankKey;
                bankIdText.Text = chosenBank.BankKey;
            }
        }

    }
}

