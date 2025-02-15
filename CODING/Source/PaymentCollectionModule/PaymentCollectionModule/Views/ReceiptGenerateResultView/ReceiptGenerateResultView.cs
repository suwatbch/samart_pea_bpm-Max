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
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureTool.Control;
using System.Drawing;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using System.Globalization;
using System.Data;
using System.IO;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;

namespace PEA.BPM.PaymentCollectionModule
{
    [SmartPart]
    public partial class ReceiptGenerateResultView : UserControl, IReceiptGenerateResultView
    {

        #region Global Variable

        private List<PaymentNonReceiptInfo> paymentList;
        private List<PaymentNonReceiptInfo> paymentGenReList;

        #endregion

        #region Constructure

        public ReceiptGenerateResultView()
        {
            InitializeComponent();
            PaymentNonReceiptGV.AutoGenerateColumns = false;
        }

        #endregion

        #region System Init

        [CreateNew]
        public ReceiptGenerateResultViewPresenter Presenter
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
            paymentList = new List<PaymentNonReceiptInfo>();
            paymentGenReList = new List<PaymentNonReceiptInfo>();
        }

        #endregion


        #region Member of IReceiptGenerateResultView

        public void SetPaymentSearchResult(List<PaymentNonReceiptInfo> param)
        {
            paymentList = param;
            BindingPaymentData();
        }

        public void UpdatePaymentList()
        {
            foreach (PaymentNonReceiptInfo payGenList in paymentGenReList)
            {
                paymentList.Remove(payGenList);
            }
            paymentGenReList.Clear();
            BindingPaymentData();
        }

        public void ResetPaymentSearchResult()
        {

            paymentList.Clear();
            paymentGenReList.Clear();
            BindingPaymentData();
        }

        #endregion

        #region Custom Function

        private void BindingPaymentData()
        {
            chkAll.Checked = false;
            PaymentNonReceiptGV.DataSource = paymentList.ToArray();
            PaymentNonReceiptGV.Refresh();
            if (paymentList.Count > 0)
            {
                if (paymentList[0].PmId.ToUpper() != "C")
                {
                    SelectAllGridView(true);
                    PaymentNonReceiptGV.Columns[0].ReadOnly = true;
                    chkAll.Checked = true;
                    chkAll.Enabled = false;
                }
                else
                {
                    SelectAllGridView(false);
                    PaymentNonReceiptGV.Columns[0].ReadOnly = false;
                    chkAll.Checked = false;
                    chkAll.Enabled = true;
                }
            }
            else
            {
                SelectAllGridView(false);
                PaymentNonReceiptGV.Columns[0].ReadOnly = false;
            }
            SummaryPaymentGenReceipt();
        }

        private void SelectAllGridView(bool setValue)
        {
            foreach (DataGridViewRow r in  PaymentNonReceiptGV.Rows)
            {
                this.PaymentNonReceiptGV["CheckboxGV", r.Index].Value = setValue;
            }
        }

        #endregion

        #region Event Handler

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ResetPaymentSearchResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPaymentGenRec.Text.Trim() != "0")
                {
                    if (DialogResult.OK == MessageBox.Show("���׹�ѹ: �س��ͧ������ҧ������Ѻ�Թ�����¡�÷�����͡ ���������\n\n�ô������ 'OK' �����׹�ѹ������ҧ������Ѻ�Թ",
                        "��ͤ����׹�ѹ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        CreateReceipt();
                    }
                }
                else
                {
                    MessageBox.Show("��س����͡��¡���Ѻ���� ����ͧ������ҧ������Ѻ�Թ", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }                
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                SelectAllGridView(true);
                SummaryPaymentGenReceipt();
            }
            else
            {
                SelectAllGridView(false);
                SummaryPaymentGenReceipt();
            }
        }

        private void SummaryPaymentGenReceipt()
        {
            int paymentAll = 0;
            int paymentGenRec = 0;
            decimal amountPaymentAll = 0M;
            decimal amountPaymentGenRec = 0M;
            foreach (DataGridViewRow r in PaymentNonReceiptGV.Rows)
            {
                paymentAll++;
                amountPaymentAll += Convert.ToDecimal(this.PaymentNonReceiptGV["GAmountGV", r.Index].Value);
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)PaymentNonReceiptGV["CheckboxGV", r.Index];
                if (Convert.ToBoolean(chk.Value))
                {
                    paymentGenRec++;
                    amountPaymentGenRec += Convert.ToDecimal(this.PaymentNonReceiptGV["GAmountGV", r.Index].Value);
                }
            }
            txtPaymentAll.Text = paymentAll.ToString();
            txtPaymentGenRec.Text = paymentGenRec.ToString();
            txtAmountAll.Text = amountPaymentAll.ToString("#,###,##0.00");
            txtGenRecAmount.Text = amountPaymentGenRec.ToString("#,###,##0.00");
        }

        private void CreateReceipt()
        {
            BindingPaymentGenReceipt();
            if (paymentGenReList.Count > 0)
            {
                WaitingFormHelper.ShowWaitingForm();
                _presenter.CreateReceiptService(paymentGenReList);
                UpdatePaymentList();
                WaitingFormHelper.HideWaitingForm();
                MessageBox.Show("�к����ҧ������Ѻ�Թ���º���� \n�س����ö�ӡ�þ�������������Ѻ�Թ��ѹ��", "�š�÷ӧҹ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BindingPaymentGenReceipt()
        {
            paymentGenReList = new List<PaymentNonReceiptInfo>();
            foreach (DataGridViewRow r in PaymentNonReceiptGV.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)PaymentNonReceiptGV["CheckboxGV", r.Index];
                if (Convert.ToBoolean(chk.Value))
                {
                    PaymentNonReceiptInfo paymentGenReceipt = (PaymentNonReceiptInfo)PaymentNonReceiptGV.Rows[r.Index].DataBoundItem;
                    paymentGenReceipt.ReceiptId = GetReceiptId(paymentGenReceipt.PaperSize, paymentGenReceipt.TaxCode, paymentGenReceipt.TaxRate, paymentGenReceipt.DtId);
                    paymentGenReceipt.ReceiptType = paymentGenReceipt.TaxRate == null ? CodeNames.ReceiptType.NonTax : CodeNames.ReceiptType.Tax;
                    paymentGenReceipt.Cashier = Session.User.Id;
                    paymentGenReceipt.BranchId = Session.Branch.Id;
                    paymentGenReList.Add(paymentGenReceipt);
                }
            }
        }

        #endregion

        private void AgentPaymentGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PaymentNonReceiptGV.EndEdit();
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                SummaryPaymentGenReceipt();
            }
        }

        private string GetReceiptId(string paperSize, string taxCode, decimal? taxRate, string debtTypeId)
        {
            bool isExtReceipt = false;
            string _paperSize = "S";
            if (paperSize != null)
            {
                _paperSize = paperSize=="3"?"P":"S";
            }
            IDSettingHelper hp = IDSettingHelper.Instance();
            string receiptId = Running.GetReceiptId(_paperSize, taxCode == null || taxCode[0] == 'O' || taxRate == null ? CodeNames.ReceiptType.NonTax : CodeNames.ReceiptType.Tax, debtTypeId, isExtReceipt, hp);
            hp.Save(hp);
            return receiptId;
        }

        public void EnablePOSPanel(bool enable)
        {
            if (!enable)
            {
                ResetPaymentSearchResult();
                panelPayment.Enabled = false;
                panelSummary.Enabled = false;
                panelButton.Enabled = false;
            }
            else
            {
                ResetPaymentSearchResult();
                panelPayment.Enabled = true;
                panelSummary.Enabled = true;
                panelButton.Enabled = true;
            }
        }

    }
}

