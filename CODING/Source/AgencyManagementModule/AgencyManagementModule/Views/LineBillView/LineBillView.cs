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
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule
{
    [SmartPart]
    public partial class LineBillView : UserControl, ILineBillView
    {
        private BindingList<CallingBillInfo> _billInfoList;
        private bool _loaded;
        private bool _isEditBillBook = false;

        public BindingList<CallingBillInfo> BillInfoList
        {
            set
            {
                _billInfoList = value;
                FillBillInformationList();
                FillSummaryGv();
            }
        }

        public void SetCancelBillBook(bool enable)
        {
            cancelBt.Enabled = enable;
            removeBt.Enabled = !enable;
            findBt.Enabled = !enable;
            savePrintBt.Enabled = !enable;
            billBookCallingDataGV.ReadOnly = enable;
            _isEditBillBook = enable;
        }

        public LineBillView()
        {
            InitializeComponent();
            billBookCallingDataGV.AutoGenerateColumns = false;
            summaryGv.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public LineBillViewPresenter Presenter
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
            InitialSummaryGV();
            _loaded = true;
        }

        public void Clear()
        {
            if (_billInfoList != null)
                _billInfoList.Clear();

            FillZero();
        }

        private void FillZero()
        {
            List<CallingBillViewSummaryFooterInfo> footerList = new List<CallingBillViewSummaryFooterInfo>();
            CallingBillViewSummaryFooterInfo footer = new CallingBillViewSummaryFooterInfo();
            footer.BillCount = 0;
            footer.Header = " ��ػ��� ";
            footer.LineCount = 0;
            footer.TotalAmount = (decimal)0.0;
            footerList.Add(footer);
            summaryGv.DataSource = footerList;
        }

        private void FillBillInformationList()
        {
            billBookCallingDataGV.Enabled = false;
            billBookCallingDataGV.DataSource = _billInfoList;
            billBookCallingDataGV.Enabled = true;
        }

        private void backBt_Click(object sender, EventArgs e)
        {
            _presenter.BackToBookManagementViewActivated();
        }

        private void billBookCallingDataGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (!_isEditBillBook))
            {
                CallingBillInfo billInfo = _billInfoList[e.RowIndex];
                billBookCallingDataGV.Rows.RemoveAt(e.RowIndex);
                _presenter.AddExceptionalCallingBillActivated(billInfo);
                FillSummaryGv();
            }
        }

        private void removeBt_Click(object sender, EventArgs e)
        {
            //remove all checked lines
            List<int> toRemove = new List<int>();
            for (int i = billBookCallingDataGV.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow r = billBookCallingDataGV.Rows[i];
                if (r.Cells["Mark"].Value != null)
                {
                    bool marked = (bool)r.Cells["Mark"].Value;
                    if (marked)
                        toRemove.Add(r.Index);
                }
            }

            for (int j = 0; j < toRemove.Count; j++)
            {
                CallingBillInfo billInfo = _billInfoList[toRemove[j]];
                billBookCallingDataGV.Rows.RemoveAt(toRemove[j]);
                _presenter.AddExceptionalCallingBillActivated(billInfo);
            }
        }

        private void savePrintBt_Click(object sender, EventArgs e)
        {
            _presenter.BillBookSaveRequestClicked();
        }

        private void FillSummaryGv()
        {
            if (_billInfoList != null)
            {
                Hashtable lineList = new Hashtable();
                List<CallingBillViewSummaryFooterInfo> footerList = new List<CallingBillViewSummaryFooterInfo>();
                CallingBillViewSummaryFooterInfo footer = new CallingBillViewSummaryFooterInfo();

                foreach (CallingBillInfo bill in _billInfoList)
                {
                    //count number of lines in this book
                    string compId = bill.PeaCode + bill.LineId;
                    if (!lineList.Contains(compId))
                    {
                        lineList.Add(compId, null);
                        footer.LineCount++;
                    }

                    footer.BillCount++;
                    footer.TotalAmount += bill.Amount;
                }

                footer.Header = " ��ػ��� ";
                footerList.Add(footer);
                summaryGv.DataSource = footerList;
                RefreshSummaryGvDisplayIndex();

            }
        }

        private void RefreshSummaryGvDisplayIndex()
        {
            summaryGv.Columns["Header"].DisplayIndex = 0;
            summaryGv.Columns["TLineId"].DisplayIndex = 1;
            summaryGv.Columns["BillCount"].DisplayIndex = 2;
            summaryGv.Columns["TInvoiceNo"].DisplayIndex = 3;
            summaryGv.Columns["Sp"].DisplayIndex = 4;
            summaryGv.Columns["TotalAmount"].DisplayIndex = 5;
            summaryGv.Columns["Dummy"].DisplayIndex = 6;
        }

        private void InitialSummaryGV()
        {
            //header            
            summaryGv.Columns["Header"].Width = billBookCallingDataGV.RowHeadersWidth + billBookCallingDataGV.Columns["Mark"].Width + billBookCallingDataGV.Columns["PeaCode"].Width;
            //line sum
            summaryGv.Columns["TLineId"].Width = billBookCallingDataGV.Columns["LineId"].Width;
            //bill sum
            summaryGv.Columns["BillCount"].Width = billBookCallingDataGV.Columns["CaId"].Width;
            summaryGv.Columns["TInvoiceNo"].Width = billBookCallingDataGV.Columns["InvoiceNo"].Width;
            summaryGv.Columns["Sp"].Width = billBookCallingDataGV.Columns["PaymentType"].Width + billBookCallingDataGV.Columns["BillPeriod"].Width;
            //amount sum
            summaryGv.Columns["TotalAmount"].Width = billBookCallingDataGV.Columns["Amount"].Width;
            summaryGv.Columns["Dummy"].Width = billBookCallingDataGV.Width;
        }

        private void billBookCallingDataGV_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if(_loaded)
                InitialSummaryGV();
        }

        public void FillBillSearchInfo(string branchId, string mruId, string caId)
        {
            peaText.Text = branchId;
            lineIdText.Text = mruId;
            caIdText.Text = caId;
            FindInvoice(branchId, mruId, caId);
        }

        private void FindInvoice(string branchId, string mruId, string caId)
        {
            billBookCallingDataGV.ClearSelection();
            if (_billInfoList == null) return;
            for (int i = 0; i < _billInfoList.Count; i++)
            {
                if (string.Equals(_billInfoList[i].PeaCode, branchId, StringComparison.CurrentCultureIgnoreCase) && _billInfoList[i].LineId == mruId && _billInfoList[i].CaId == caId)
                {
                    //select the line and invert checkbox
                    billBookCallingDataGV.Rows[i].Selected = true;
                    if (Convert.ToBoolean(billBookCallingDataGV.Rows[i].Cells["Mark"].Value))
                        billBookCallingDataGV.Rows[i].Cells["Mark"].Value = false;
                    else
                        billBookCallingDataGV.Rows[i].Cells["Mark"].Value = true;
                }
            }
        }

        private void caIdtext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (caIdText.Text.Length == 32)
                {
                    _presenter.ValidateBarcodeInput(caIdText.Text);
                    caIdText.SelectAll();
                }
                else if (caIdText.Text.Length == ModuleConfigurationNames.CustomerNoLength)
                {
                    if (peaText.Text.Length == 0)
                        peaText.Focus();
                    else if (lineIdText.Text.Length == 0)
                        lineIdText.Focus();
                    else
                    {
                        FindInvoice(peaText.Text, lineIdText.Text, caIdText.Text);
                        caIdText.SelectAll();
                    }
                }
                else
                {
                    MessageBox.Show(null, "��͹���������ʼ����俿�����١��ͧ", "��͹�Դ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void peaText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (peaText.Text.Length == 32)
                {
                    _presenter.ValidateBarcodeInput(peaText.Text);
                    peaText.SelectAll();
                }
                else if (peaText.Text.Length == ModuleConfigurationNames.BranchCodeLength)
                {
                    lineIdText.Focus();
                    lineIdText.SelectAll();
                }
                else
                {
                    MessageBox.Show(null, "��͹���������ʼ����俿�����١��ͧ", "��͹�Դ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void lineIdText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lineIdText.Text.Length == 32)
                {
                    _presenter.ValidateBarcodeInput(lineIdText.Text);
                    lineIdText.SelectAll();
                }
                else if (lineIdText.Text.Length == ModuleConfigurationNames.LineIdLength)
                {
                    if (peaText.Text.Length == 0)
                        peaText.Focus();
                    else
                    {
                        caIdText.Focus();
                        caIdText.SelectAll();
                    }
                }
                else
                {
                    MessageBox.Show(null, "��͹���������ʼ����俿�����١��ͧ", "��͹�Դ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void findBt_Click(object sender, EventArgs e)
        {
            if (peaText.Text.Replace(" ", "") == "")
            {
                peaText.Focus();
                peaText.SelectAll();
            }
            else if (lineIdText.Text.Replace(" ", "") == "")
            {
                lineIdText.Focus();
                lineIdText.SelectAll();
            }
            else if (caIdText.Text.Replace(" ", "") == "")
            {
                caIdText.Focus();
                caIdText.SelectAll();
            }
            else
            {
                //presenter find
                caIdText.Focus();
                caIdText.SelectAll();
            }
        }

        private void cancelBt_Click(object sender, EventArgs e)
        {
            _presenter.CancelBillBookClicked();
        }

    }
}

