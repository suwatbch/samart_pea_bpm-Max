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
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Collections.Generic;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Control;
using System.Drawing;

namespace PEA.BPM.PaymentCollectionModule
{
    [SmartPart]
    public partial class CAC11CriteriaView : UserControl, ICAC11CriteriaView
    {
        public CAC11CriteriaView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public CAC11CriteriaViewPresenter Presenter
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
            LoadBranch();
            LoadTerminal();
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

        private void LoadTerminal()
        {
            Terminal t = new Terminal("", "", "", "", "");
            List<Terminal> ta = new List<Terminal>(CodeTable.Instant.ListTerminals(Session.Branch.Id));
            ta.Insert(0, t);
            ta.Sort(delegate(Terminal ta1, Terminal ta2) { return ta1.PosCode.CompareTo(ta2.PosCode); });
            terminalCodeComboBox.DisplayMember = "PosCode";
            terminalCodeComboBox.ValueMember = "PosId";
            terminalCodeComboBox.DataSource = ta;
        }

        private void displayReportButton_Click(object sender, EventArgs e)
        {
            displayReport();
        }

        private void branchIdMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                displayReport();
            }
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
                MessageBox.Show("��س����͡�ѹ����Ѻ���������¡���������ҡѺ�ѹ��� " + String.Format("{0:dd/MM/yyyy}", toDate), "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (DayDiff > 31)
            {
                MessageBox.Show("��س����͡��ǧ�ѹ����Ѻ��������Թ 31 �ѹ", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CAC11Param param = new CAC11Param();
            param.BranchId = (AutoCompleteTextBox<Branch>.GetSelectedItem(branchIdMaskedTextBox) == null) ? null : AutoCompleteTextBox<Branch>.GetSelectedItem(branchIdMaskedTextBox).BranchId;
            param.ControllerId = (controllerIdMaskedTextBox.Text.Trim() == string.Empty) ? null : controllerIdMaskedTextBox.Text.Trim().PadLeft(8, '0');
            param.PosId = (terminalCodeComboBox.Text.Trim() == string.Empty) ? null : ((Terminal)terminalCodeComboBox.SelectedItem).PosId;
            param.TransFromDate = DateTime.SpecifyKind(transactionFromDateTimePicker.Value, DateTimeKind.Unspecified);
            param.TransToDate = DateTime.SpecifyKind(transactionToDateTimePicker.Value, DateTimeKind.Unspecified);
            //param.TransToDate = toDate;
            param.Report = ReportName.CAC11;

            displayReportButton.Enabled = false;
            _presenter.OnShowReport(param);
            displayReportButton.Enabled = true;
        }

    }
}

