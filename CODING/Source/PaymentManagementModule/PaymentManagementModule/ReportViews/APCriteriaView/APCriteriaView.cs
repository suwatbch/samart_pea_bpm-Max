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
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureTool.Control;
using System.Drawing;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentManagementModule
{
    [SmartPart]
    public partial class APCriteriaView : UserControl, IAPCriteriaView
    {
        public APCriteriaView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public APCriteriaViewPresenter Presenter
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
            //allReportRadioButton.Checked = true;
            LoadBranch();
        }

        private void LoadBranch()
        {
            List<Branch> branch = new List<Branch>(CodeTable.Instant.ListBranches());

            AutoCompleteTextBox<Branch> actb = new AutoCompleteTextBox<Branch>(branchIdMaskedTextBox);
            actb.PopupBackColor = Color.White;
            actb.PopupHeight = 100;
            actb.PopupExtendWidth = 80;
            actb.DataSource = branch;
            actb.KeyPress += new KeyPressEventHandler(branchIdMaskedTextBox_KeyPress);
            AutoCompleteTextBox<Branch>.SetSelectedItem(branchIdMaskedTextBox, Session.Branch.Id);
        }

        private void APCriteriaView_Load(object sender, EventArgs e)
        {

        }

        private void displayReportButton_Click(object sender, EventArgs e)
        {
            displayReport();
        }

        private void displayReport()
        {
            DateTime fromDate = transactionFromDateTimePicker.Value.Date;
            DateTime toDate = transactionToDateTimePicker.Value.Date;
            //DateTime? toDate = null;

            TimeSpan DaySpan = toDate.Subtract(fromDate);
            int DayDiff = DaySpan.Days + 1;

            if (AutoCompleteTextBox<Branch>.GetSelectedItem(branchIdMaskedTextBox) == null)
            {
                MessageBox.Show("��س�������� ���.", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (fromDate > toDate)
            {
                MessageBox.Show("��س����͡�ѹ�����������¡���������ҡѺ�ѹ���  " + String.Format("{0:dd/MM/yyyy}", toDate), "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (DayDiff > 31)
            {
                MessageBox.Show("��س����͡��ǧ�ѹ����������Թ 31 �ѹ", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            APParam param = new APParam();
            param.BranchId = (AutoCompleteTextBox<Branch>.GetSelectedItem(branchIdMaskedTextBox) == null) ? null : AutoCompleteTextBox<Branch>.GetSelectedItem(branchIdMaskedTextBox).BranchId;
            param.cashierId = (cashierIdMaskedTextBox.Text.Trim() == string.Empty) ? null : cashierIdMaskedTextBox.Text.Trim().PadLeft(8, '0');
            param.posId = (posIdMaskedTextBox.Text.Trim() == string.Empty) ? null : posIdMaskedTextBox.Text.Trim();
            param.TransFromDate = transactionFromDateTimePicker.Value;
            param.TransToDate = transactionToDateTimePicker.Value;
            //param.TransToDate = toDate;


            param.Report = ReportName.AP;

            _presenter.OnShowReport(param);
        }

        private void branchIdMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                displayReport();
            }
        }
    }
}

