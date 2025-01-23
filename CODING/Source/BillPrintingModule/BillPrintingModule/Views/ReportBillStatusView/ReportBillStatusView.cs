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
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.BillPrintingModule.BillPrintingUtilities;
using System.Collections.Generic;
using System.Globalization;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.BillPrintingModule
{
    [SmartPart]
    public partial class ReportBillStatusView : UserControl, IReportBillStatusView
    {
        enum PrintState
        {
            All = 0,
            Branch
        }

        #region "Variables & Properties"
        private List<Branch> _childBranch;
        private bool _clearCmdFlag = false;
        private PrintConditionEnum _condState = PrintConditionEnum.Branch;
        private List<Portion> _portion;

        public void OnWaitCursor(bool set)
        {
            if (set)
            {
                this.Cursor = Cursors.WaitCursor;
                showButton.Enabled = false;
            }
            else
            {
                this.Cursor = Cursors.Default;
                showButton.Enabled = true;
            }
        }

        public List<Portion> Portion
        {
            get { return _portion; }
            set
            {
                _portion = value;
                LoadPortion();
            }
        }

        #endregion

        #region "Code Generated"

        public ReportBillStatusView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public ReportBillStatusViewPresenter Presenter
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
            InitializeControlValue();
            _childBranch = CodeTable.Instant.ListBranches();
            _presenter.GetPortion(Session.Branch.Id);
        }

        #endregion

        #region "Event Handling"

        private void PrintPreview()
        {
            try
            {
                if (ValidateBeforeSubmit())
                {
                    DialogResult dlg = MessageBox.Show("��سҡ� OK �����׹�ѹ����ʴ���§ҹ", "�ʴ���§ҹ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dlg == DialogResult.OK)
                    {
                        ReportConditionParam param = new ReportConditionParam();
                        param.PrintBranch = Session.Branch.Id;
                        param.PrintedBy = Session.User.Id;
                        param.BillPeriod = billPeriodMaskedTextBox.Text.Substring(3, 4) + billPeriodMaskedTextBox.Text.Substring(0, 2);

                        if( printAllRadioButton.Checked)
                            param.PrintingCondition = (int)PrintingCondition.AllPrinting;
                        else if(printBranchRadioButton.Checked)
                            param.PrintingCondition = (int)PrintingCondition.BranchPrinting;
                        else
                            param.PrintingCondition = (int)PrintingCondition.MruPrinting;

                        param.Portion = (billPeriodAllPortionRadioButton.Checked == true ? "0" : "1");
                        if (billPeriodSpecificPortionRadioButton.Checked)
                            param.PortionNo = billPeriodSpecificPortionComboBox.Text;

                        if (blueRb.Checked)
                            param.PrintType = 1;
                        else if (greenRb.Checked)
                            param.PrintType = 2;
                        else if (a4Rb.Checked)
                            param.PrintType = 0;
                        else if (spotBillRb.Checked)
                            param.PrintType = 5;

                        List<InputParam> paramList = new List<InputParam>();
                        if (reportStreetRouteDataGridView.Rows.Count != 0)
                        {
                            for (int i = 0; i < reportStreetRouteDataGridView.Rows.Count; i++)
                            {
                                InputParam p = new InputParam();
                                p.InputStr = reportStreetRouteDataGridView.Rows[i].Cells["electricityIdColumn"].Value.ToString();
                                paramList.Add(p);
                            }

                            param.InputParamList = paramList;
                        }

                        _presenter.PreviewBillStatusReport(param);
                    }
                }
            }
            catch (Exception ex)
            {                
                this.Cursor = Cursors.Default;
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(ex.ToString(), "�Դ��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPortion()
        {
            billPeriodSpecificPortionComboBox.ValueMember = "PortionKey";
            billPeriodSpecificPortionComboBox.DisplayMember = "PortionNo";
            billPeriodSpecificPortionComboBox.DataSource = _portion;
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            PrintPreview();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            InitializeControlValue();
        }

        private void billPeriodMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)Keys.Return && billPeriodMaskedTextBox.MaskCompleted)
            {
                if (CustomValidation.ValidateDate(billPeriodMaskedTextBox.Text))
                {
                    if (billPeriodMaskedTextBox.MaskCompleted == true)
                        branchIdMaskedTextBox.Focus();
                }
                else
                {
                    MessageBox.Show("��سҵ�Ǩ�ͺ�ѹ���ͧ��Ż�Ш���͹", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    billPeriodMaskedTextBox.Clear();
                }
            }
        }

        private void mruIdMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (printMruRadioButton.Checked == true &&
               branchIdMaskedTextBox.Text.Length == 6 &&
               mruIdMaskedTextBox.Text.Length == 4)
                {
                    SetMaskedTextBehavior(toMruIdMaskedTextBox, MaskedValue.ElectricMruId, true);
                }
            }
        }

        private void toMruIdMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (printMruRadioButton.Checked &&
                 branchIdMaskedTextBox.MaskCompleted &&
                 mruIdMaskedTextBox.MaskCompleted &&
                 (toMruIdMaskedTextBox.MaskCompleted ||
                 toMruIdMaskedTextBox.Text == string.Empty))
                {
                    string branchId = ToUpperCase(branchIdMaskedTextBox);
                    if (toMruIdMaskedTextBox.MaskCompleted)
                    {
                        int fromMru = Convert.ToInt32(mruIdMaskedTextBox.Text);
                        int toMru = Convert.ToInt32(toMruIdMaskedTextBox.Text);
                        if (fromMru > toMru)
                        {
                            mruIdMaskedTextBox.Text = toMru.ToString().PadLeft(4, '0');
                            toMruIdMaskedTextBox.Text = fromMru.ToString().PadLeft(4, '0');
                        }
                        AddToDataGridView(branchId + "-" + mruIdMaskedTextBox.Text + "-" + toMruIdMaskedTextBox.Text);
                    }
                    else //user leave blank and press enter = no mruId-range required
                        AddToDataGridView(branchId + "-" + mruIdMaskedTextBox.Text);
                    SetMaskedTextBehavior(toMruIdMaskedTextBox, MaskedValue.ElectricMruId, false);
                    SetMaskedTextBehavior(mruIdMaskedTextBox, MaskedValue.ElectricMruId, true);
                }
            }
        }

        private void branchIdMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (branchIdMaskedTextBox.Text.Length == 6)
                {
                    if (printBranchRadioButton.Checked == true)
                    {
                        AddToDataGridView(ToUpperCase(branchIdMaskedTextBox));
                        SetMaskedTextBehavior(branchIdMaskedTextBox, MaskedValue.ElectricBranchId, true);
                    }
                    else if (printMruRadioButton.Checked == true)
                    {
                        //then focus on mruIdMaskedTextBox
                        SetMaskedTextBehavior(mruIdMaskedTextBox, MaskedValue.ElectricMruId, true);
                    }
                }
                else if (printBranchRadioButton.Checked) //special feature added May, 6 '08, wildcard selection
                {
                    if (branchIdMaskedTextBox.Text.Length > 2 && branchIdMaskedTextBox.Text.Length < 6)
                    {
                        List<string> branchList = CustomValidation.FindBranchByWildCard(branchIdMaskedTextBox.Text, _childBranch);
                        branchList.Sort();
                        foreach (string branch in branchList)
                            AddToDataGridView(branch.ToUpper());
                    }
                }
            }
        }

        private void reportStreetRouteDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                //MessageBox.Show("�С� Column Header 价����ѹ��Ѻ");
            }
            else if (reportStreetRouteDataGridView.Columns[e.ColumnIndex].Name == "delColumn")
                reportStreetRouteDataGridView.Rows.RemoveAt(e.RowIndex);
        }

        #endregion

        #region "Function"

        private void AddToDataGridView(string elecId)
        {
            bool isRepeatedItem = false;
            reportStreetRouteDataGridView.Enabled = true;

            isRepeatedItem = CheckRepeatedItem(elecId, isRepeatedItem);

            if (isRepeatedItem == false)
            {
                try
                {
                    int i = reportStreetRouteDataGridView.Rows.Count;
                    reportStreetRouteDataGridView.Rows.Add(1);
                    reportStreetRouteDataGridView.Rows[i].Cells["electricityIdColumn"].Value = elecId;
                    reportStreetRouteDataGridView.Rows[i].Selected = true;

                    for (int j = 0; j <= i - 1; j++)
                    {
                        reportStreetRouteDataGridView.Rows[j].Selected = false;
                    }
                }
                catch (Exception ex)
                {
                    ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                    //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private bool CheckRepeatedItem(string txt, bool isRepeated)
        {
            if (reportStreetRouteDataGridView.Rows.Count != 0)
            {
                for (int i = 0; i < reportStreetRouteDataGridView.Rows.Count; i++)
                {

                    if (txt == (string)reportStreetRouteDataGridView.Rows[i].Cells["electricityIdColumn"].Value)
                    {
                        isRepeated = true;
                        break;
                    }
                    else
                        isRepeated = false;
                }
            }
            else
            {
                isRepeated = false;
            }
            return isRepeated;
        }

        private void SetMaskedTextBehavior(MaskedTextBox mskTxtBox, string mskValue, bool isContAddToList)
        {
            if (isContAddToList)
            {
                mskTxtBox.Focus();
            }
            else
            {
                billPeriodMaskedTextBox.Focus();
            }

            mskTxtBox.Enabled = true;
            mskTxtBox.Text = string.Empty;
            //mskTxtBox.Mask = mskValue;
        }

        private bool ValidateBeforeSubmit()
        {

            if (reportStreetRouteDataGridView.Rows.Count == 0 && printBranchRadioButton.Checked)
            {                
                MessageBox.Show(MessageBoxText.MsgMissingElectricId, MessageBoxText.CaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                branchIdMaskedTextBox.Focus();
                branchIdMaskedTextBox.SelectAll();
                return false;
            }
      

            if (!billPeriodMaskedTextBox.MaskCompleted || !CustomValidation.ValidateDate(billPeriodMaskedTextBox.Text))
            {
                MessageBox.Show(MessageBoxText.MsgWrongFormatBillPeriod, MessageBoxText.CaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                billPeriodMaskedTextBox.Focus();
                billPeriodMaskedTextBox.SelectAll();
                return false;
            }

            return true;
        }

        private void InitializeControlValue()
        {
            reportStreetRouteDataGridView.Enabled = true;
            reportStreetRouteDataGridView.Rows.Clear();
            reportStreetRouteDataGridView.AutoGenerateColumns = false;

            billPeriodMaskedTextBox.Text = string.Empty;
            billPeriodMaskedTextBox.Mask = MaskedValue.BillPeriodMonthYear;
            printBranchRadioButton.Checked = true;
            branchIdMaskedTextBox.Text = string.Empty;
            branchIdMaskedTextBox.Enabled = true;
            
            billPeriodMaskedTextBox.Focus();
        }

        private string ToUpperCase(Control control)
        {
            return control.Text.Substring(0, 1).ToUpper() + control.Text.Substring(1, control.Text.Length - 1);            
        }

        #endregion

        private void billPeriodMaskedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N)
            {
                IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
                billPeriodMaskedTextBox.Text = DateTime.Now.ToString("MMyyyy", formatDate);
                if (printAllRadioButton.Checked)
                    showButton.Focus();
                else
                    branchIdMaskedTextBox.Focus();

            }
            else if (e.KeyCode == Keys.F12)
            {
                if (printAllRadioButton.Checked)
                    PrintPreview();
            }
        }

        private void branchIdMaskedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                if (reportStreetRouteDataGridView.Rows.Count > 0)
                    PrintPreview();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (_clearCmdFlag)
                {
                    _clearCmdFlag = false;
                    DialogResult dlg = MessageBox.Show(null, "�س��ͧ�����ҧ�����ŷ���͹�����������������", "����͹", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dlg == DialogResult.Yes)
                        InitializeControlValue();
                }
                else
                {
                    branchIdMaskedTextBox.ResetText();
                    branchIdMaskedTextBox.Focus();
                    _clearCmdFlag = true;
                }
            }
            else if (e.KeyCode == Keys.N)
            {
                branchIdMaskedTextBox.Text = Session.Branch.Id;
            }
        }

        private void branchIdMaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            _clearCmdFlag = false;
        }

        private void PrintAllCheckd(bool renew)
        {
            if (renew)
                reportStreetRouteDataGridView.Rows.Clear();

            reportStreetRouteDataGridView.Enabled = false;
            branchIdMaskedTextBox.Enabled = false;
            mruIdMaskedTextBox.Enabled = false;
            toMruIdMaskedTextBox.Enabled = false;
            branchIdMaskedTextBox.Clear();
            mruIdMaskedTextBox.Clear();
            toMruIdMaskedTextBox.Clear();
            showButton.Focus();
        }

        private void PrintBranchChecked(bool renew)
        {
            if (renew)
                reportStreetRouteDataGridView.Rows.Clear();

            reportStreetRouteDataGridView.Enabled = true;
            branchIdMaskedTextBox.Enabled = true;
            mruIdMaskedTextBox.Enabled = false;
            toMruIdMaskedTextBox.Enabled = false;
            branchIdMaskedTextBox.Clear();
            mruIdMaskedTextBox.Clear();
            toMruIdMaskedTextBox.Clear();
            branchIdMaskedTextBox.Focus();
        }

        private void PrintMruChecked(bool renew)
        {
            if (renew)
                reportStreetRouteDataGridView.Rows.Clear();

            reportStreetRouteDataGridView.Enabled = true;
            branchIdMaskedTextBox.Enabled = true;
            mruIdMaskedTextBox.Enabled = true;
            toMruIdMaskedTextBox.Enabled = true;
            branchIdMaskedTextBox.Clear();
            mruIdMaskedTextBox.Clear();
            toMruIdMaskedTextBox.Clear();
            branchIdMaskedTextBox.Focus();
        }

        private void printBranchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (printBranchRadioButton.Checked)
            {
                if (_condState != PrintConditionEnum.Branch && reportStreetRouteDataGridView.Rows.Count > 0)
                {
                    DialogResult dr = MessageBox.Show(MessageBoxText.MsgConfirmConditionChanged, MessageBoxText.CaptionConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                        PrintBranchChecked(true);
                    else
                    {
                        if (_condState == PrintConditionEnum.All)
                            PrintAllCheckd(false);
                        else //mru
                            PrintMruChecked(false);
                    }
                }
                else
                    PrintBranchChecked(false);

                _condState = PrintConditionEnum.Branch;
            }
        }

        private void printMruRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (printMruRadioButton.Checked)
            {
                if (_condState != PrintConditionEnum.Mru && reportStreetRouteDataGridView.Rows.Count > 0)
                {
                    DialogResult dr = MessageBox.Show(MessageBoxText.MsgConfirmConditionChanged, MessageBoxText.CaptionConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                        PrintMruChecked(true);
                    else
                    {
                        if (_condState == PrintConditionEnum.All)
                            PrintAllCheckd(false);
                        else //mru
                            PrintBranchChecked(false);
                    }
                }
                else
                    PrintMruChecked(false);

                _condState = PrintConditionEnum.Mru;
            }
        }

        private void mruIdMaskedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                if (reportStreetRouteDataGridView.Rows.Count > 0)
                    PrintPreview();
            }
        }

        private void billPeriodSpecificPortionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (billPeriodSpecificPortionRadioButton.Checked)
                billPeriodSpecificPortionComboBox.Enabled = true;
            else
                billPeriodSpecificPortionComboBox.Enabled = false;
        }
      
    }
}

