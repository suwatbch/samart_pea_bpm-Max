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
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureTool.Control;
using System.Drawing;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Views.VendorPaymentView;

namespace PEA.BPM.ePaymentsModule
{
    [SmartPart]
    public partial class VendorPaymentView : UserControl, IVendorPaymentView
    {
        private List<AgentPayment> agentPayList;
        private AgentPayment agentPayInfo;

        #region Default Constructor

        public VendorPaymentView()
        {
            InitializeComponent();
            VenderPaymentGV.AutoGenerateColumns = false;
        }

        #endregion

        #region VenderPayment Init

        [CreateNew]
        public VendorPaymentViewPresenter Presenter
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
            LoadBank();
            LoadBankAccount();
            agentPayList = new List<AgentPayment>();
            agentPayInfo = new AgentPayment();
        }

        #endregion

        #region Payment Event Handler

        private void cmbBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBankName.Text != "")
            {
                Deposit ba = new Deposit("", "", "", "", "", "", "", "");
                List<Deposit> bankAccounts = new List<Deposit>(CodeTable.Instant.ListDepositByBankKey(Session.Branch.Id.Substring(0, 4), ((Deposit)cmbBankName.SelectedItem).BankKey));
                bankAccounts.Insert(0, ba);
                bankAccounts.Sort(delegate(Deposit ba1, Deposit ba2) { return ba1.AccountNo.CompareTo(ba2.AccountNo); });
                cmbAccountNo.DisplayMember = "AccountNo";
                cmbAccountNo.ValueMember = "AccountNo";
                cmbAccountNo.DataSource = bankAccounts;
                if (cmbAccountNo.Items.Count > 1)
                {
                    cmbAccountNo.SelectedIndex = 1;
                }
            }
            else
            {
                LoadBank();
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (StringConvert.ToDecimal(txtAmount.Text.Trim()) == null)
                {
                    MessageBox.Show("�ӹǹ�Թ�㺹ӽҡ���١��ͧ", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmount.Focus();
                    txtAmount.SelectAll();
                }
                else
                {
                    txtAmount.Text = Convert.ToDecimal(txtAmount.Text.Trim()).ToString("#,###,##0.00");
                    cmbBankName.Focus();
                }
            }
        }

        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblVendorName.Text.Replace("_", "").Trim() != "")
                {
                    if (ValidateAddPayment() && CheckDepositDate())
                    {
                        if (!IsMoreAccount())
                        {
                            AddPaymentItem();
                            BindingPaymentList();
                            SummaryAmount();
                            ResetPaymentControl();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("��س����͡���᷹����Ѻ����Ѻ�����Թ", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetPaymentControl();
        }

        private void depBankButton_Click(object sender, EventArgs e)
        {
            FindDepositBank();
        }

        private void cmbAccountNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAccountNo.Text != "")
            {
                cmbBankName.SelectedValue = ((Deposit)cmbAccountNo.SelectedItem).BankKey;
            }
            else
            {
                LoadBankAccount();
            }
        }


        private void cmbAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && cmbAccountNo.SelectedIndex > -1)
            {
                cmbPaymentDate.Focus();
            }
        }

        private void cmbPaymentDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CheckDepositDate())
                {
                    btnAddAcc.Focus();
                }
            }
        }

        private void cmbBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbAccountNo.Focus();
            }
        }

        #endregion

        #region Payment Validation

        private bool ValidateAddPayment()
        {
            bool result = true;
            if (cmbBankName.Text.Trim() == "")
            {
                result = false;
                MessageBox.Show("��سҡ�͡�����Ÿ�Ҥ�÷���ͧ����Ѻ����", "����͹", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                cmbBankName.Focus();
            }
            else if (txtAmount.Text.Trim() == "")
            {
                result = false;
                MessageBox.Show("��سҡ�͡�����Ũӹǹ�Թ�㺹ӽҡ", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
            }
            else if (StringConvert.ToDecimal(txtAmount.Text.Trim()) == null)
            {
                result = false;
                MessageBox.Show("�ӹǹ�Թ�㺹ӽҡ���١��ͧ", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                txtAmount.SelectAll();
            }
            else if (StringConvert.ToDecimal(txtAmount.Text.Trim()) != null && StringConvert.ToDecimal(txtAmount.Text.Trim()) <= 0)
            {
                result = false;
                MessageBox.Show("�ӹǹ�Թ�㺹ӽҡ��ͧ�դ���ҡ���� 0 �ҷ", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                txtAmount.SelectAll();
            }
            else if(cmbAccountNo.Text.Trim() == "")
            {
                result = false;
                MessageBox.Show("��سҡ�͡�Ţ���ѭ�շ���ͧ����Ѻ����", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAccountNo.Focus();
            }
            return result;
        }

        private bool CheckDepositDate()
        {
            bool result = true;
            if (cmbPaymentDate.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("�ѹ���ӽҡ��ͧ����ҡ�����ѹ���Ѩ�غѹ", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPaymentDate.Select();
                result = false;
            }
            return result;
        }

        private bool IsMoreAccount()
        {
            bool result = false;
            string tranfAccNo = cmbAccountNo.Text.Trim();
            foreach (AgentPayment tmp in agentPayList)
            {
                if(!tmp.IsSysData)
                {
                    if (!tmp.TranfAccNo.Equals(tranfAccNo))
                    {
                        result = true;
                        MessageBox.Show("����Ѻ�����Թ�ͧ���᷹���Ф��� ����ö���͡�Ţ���ѭ�շ��ӽҡ�� 1 �ѭ����ҹ��", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbAccountNo.Focus();
                        break;
                    }
                }
            }
            return result;
        }

        #endregion

        #region VenderPayment Grid View Event Handler 

        private void VenderPaymentGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (VenderPaymentGV.Rows[e.RowIndex].Cells["DelStatus"].Value == null)
            {
                AgentPayment payment = (AgentPayment)VenderPaymentGV.Rows[e.RowIndex].DataBoundItem;

                if (payment.IsSysData)
                {
                    VenderPaymentGV.Rows[e.RowIndex].Cells["DelStatus"].Value = Properties.Resources.Transparent;
                }
                else
                {
                    VenderPaymentGV.Rows[e.RowIndex].Cells["DelStatus"].Value = Properties.Resources.delete;
                    VenderPaymentGV.Rows[e.RowIndex].Cells["DelStatus"].ToolTipText = "ź��¡���Ѻ�����Թ";
                    
                }
            }
        }

        private void VenderPaymentGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (VenderPaymentGV.Columns[e.ColumnIndex].Name == "DelStatus" && (!agentPayList[e.RowIndex].IsSysData))
            {
                if (MessageBox.Show("��ͧ���ź��¡�ê����Թ��� ���������", "ź������", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RemovePaymentItem(e.RowIndex);
                    BindingPaymentList();
                    SummaryAmount();
                }
            }
        }

        #endregion

        #region InsertAgentPayment Event Handler

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ResetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblVendorName.Text.Replace("_", "").Trim() == "")
                {
                    MessageBox.Show("��س����͡���᷹����Ѻ����Ѻ�����Թ", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (!IsPaymentExist())
                {
                    MessageBox.Show("��سҡ�͡�����š���Ѻ�����Թ�ͧ���᷹", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (Convert.ToDecimal(txtSumAmount.Text.Replace(",", "").Trim()) != Convert.ToDecimal(lblAmount.Text.Replace(",", "").Trim()))
                    {
                        if (MessageBox.Show("�ʹ˹������ ���ç�Ѻ�ʹ�Թ������ �س��ͧ��ê����ʹ�Թ��� ���������", "��ͤ����׹�ѹ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            InsertAgentPayment();
                            MessageBox.Show("�ѹ�֡���������º����", "�š�úѹ�֡", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetAll();
                        }
                    }
                    else
                    {
                        InsertAgentPayment();
                        MessageBox.Show("�ѹ�֡���������º����", "�š�úѹ�֡", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetAll();
            }
        }

        #endregion

        #region Bank (Custom Function)

        private void LoadBank()
        {
            Deposit b = new Deposit("", "", "", "", "", "", "", "");
            List<Deposit> deposit = new List<Deposit>(CodeTable.Instant.ListDepositsByBusinessPlace(Session.Branch.Id.Substring(0, 4)));
            List<Deposit> banks = new List<Deposit>();

            foreach (Deposit d in deposit)
            {
                if (!banks.Exists(delegate(Deposit dd)
                    {
                        return dd.BankName == d.BankName;
                    }
                        ))
                {
                    banks.Add(d);
                }
            }

            banks.Insert(0, b);
            banks.Sort(delegate(Deposit b1, Deposit b2) { return b1.BankName.CompareTo(b2.BankName); });
            cmbBankName.DisplayMember = "BankName";
            cmbBankName.ValueMember = "BankKey";
            cmbBankName.DataSource = banks;

        }

        private void LoadBankAccount()
        {
            Deposit ba = new Deposit("", "", "", "", "", "", "", "");
            List<Deposit> bankAccounts = new List<Deposit>(CodeTable.Instant.ListDepositByBusinessPlace(Session.Branch.Id.Substring(0, 4)));
            bankAccounts.Insert(0, ba);
            bankAccounts.Sort(delegate(Deposit ba1, Deposit ba2) { return ba1.AccountNo.CompareTo(ba2.AccountNo); });
            cmbAccountNo.DisplayMember = "AccountNo";
            cmbAccountNo.ValueMember = "AccountNo";
            cmbAccountNo.DataSource = bankAccounts;

        }

        private void FindDepositBank()
        {
            FindBank(cmbBankName, true);
        }

        private void FindBank(ComboBox bankComboBox, bool isDeposit)
        {
            using (BankSearchForm bsForm = new BankSearchForm(isDeposit))
            {
                if (bsForm.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    SetBankDataSource(bankComboBox, bsForm.SelectedBank, isDeposit);
                }
            }
        }

        private void SetBankDataSource(ComboBox bankComboBox, Bank bank, bool isDeposit)
        {
            if (isDeposit == true)
            {
                cmbBankName.SelectedValue = bank.BankKey;
                if (cmbAccountNo.Items.Count > 1)
                {
                    cmbAccountNo.SelectedIndex = 1;
                }
            }
        }

        #endregion

        #region ResetControl (Custom Function)

        public void ResetAll()
        {
            ResetPaymentControl();
            ResetInfoControl();
            ClearAgentPaymentAll();
            BindingPaymentList();
        }

        private void ResetPaymentControl()
        {
            if (cmbBankName.Items.Count > 0)
            {
                cmbBankName.SelectedIndex = 0;
            }
            if (cmbAccountNo.Items.Count > 0)
            {
                cmbAccountNo.SelectedIndex = 0;
            }
            txtAmount.ResetText();
            cmbPaymentDate.Value = DateTime.Now;
        }

        private void ResetInfoControl()
        {
            txtSumAmount.Text = "0.00";

            lblVendorName.Text = "_______________________________";
            lblUploadDate.Text = "____________";
            lblAmount.Text = "______________";
        }
    
    #endregion

        #region IVendorPaymentView Members

        public void SetAgentPaymentData(List<AgentPayment> agentPaymentList)
        {
            try
            {
                List<AgentPayment> tmpAgentPayList = new List<AgentPayment>();
                agentPayList = new List<AgentPayment>();

                agentPayInfo = agentPaymentList[0];
                tmpAgentPayList = agentPaymentList;
               
                foreach (AgentPayment agentPay in tmpAgentPayList)
                {
                    if (agentPay.DepositDt != null)
                    {
                        agentPayList.Add(agentPay);
                    }
                }
                SetValueInfoControl();
                BindingPaymentList();
                SummaryAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Custom Function

        private void SetValueInfoControl()
        {
            lblVendorName.Text = agentPayInfo.AgentName;
            if (agentPayInfo.PostDt != null)
            {
                lblUploadDate.Text = DateFormatter.ToDateTimeThString((DateTime)agentPayInfo.PostDt);
            }
            if (agentPayInfo.TotalBillAmount != null)
            {
                lblAmount.Text = ((decimal)agentPayInfo.TotalBillAmount).ToString("#,###,###,###,##0.00");
            }
        }

        private void SummaryAmount()
        {
            Decimal sumAmount = 0M;
            for (int i = 0; i < VenderPaymentGV.Rows.Count; i++)
            {
                sumAmount += (Decimal)VenderPaymentGV.Rows[i].Cells["GAmountGV"].Value;
            }
            txtSumAmount.Text = sumAmount.ToString("#,###,##0.00");
        }

        private void ClearAgentPaymentAll()
        {
            if (agentPayList != null)
            {
                agentPayList.Clear();
            }
        }

        private void AddPaymentItem()
        {
            if (agentPayList != null)
            {
                AgentPayment agentPay = new AgentPayment();
                agentPay.PostDt = agentPayInfo.PostDt;
                agentPay.AgentId = agentPayInfo.AgentId;
                agentPay.AgentName = agentPayInfo.AgentName;
                agentPay.AgentAddr = agentPayInfo.AgentAddr;
                agentPay.Prefix = "D";
                agentPay.DtId = "MY0500010";
                agentPay.PmId = "N";
                agentPay.PosId = Session.Terminal.Id;
                agentPay.BranchId = Session.Branch.Id;
                agentPay.PtId = "3";
                agentPay.ReceiptType = "7";

                agentPay.BankName = cmbBankName.Text.Trim();
                agentPay.BankKey = cmbBankName.SelectedValue.ToString().Trim();
                agentPay.TranfAccNo = cmbAccountNo.Text.Trim();
                agentPay.GAmount = Convert.ToDecimal(txtAmount.Text.Trim());
                agentPay.DepositDt = cmbPaymentDate.Value.Date;
                agentPay.TranfDt = cmbPaymentDate.Value.Date;
                agentPay.IsSysData = false;

                agentPayList.Add(agentPay);
            }
        }

        private void RemovePaymentItem(int row)
        {
            if (agentPayList.Count > 0)
            {
                agentPayList.RemoveAt(row);
            }
        }

        private void BindingPaymentList()
        {
            try
            {
                VenderPaymentGV.DataSource = agentPayList.ToArray();
                VenderPaymentGV.Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void InsertAgentPayment()
        {
            decimal paymentTotal = 0M;
            int loopCount = 0;
            int firstNewDeposit = 0;
            try
            {
                if (agentPayList.Count > 0)
                {
                    foreach (AgentPayment agentPay in agentPayList)
                    {
                        if (!agentPay.IsSysData)
                        {
                            if (firstNewDeposit == 0)
                            {
                                firstNewDeposit = loopCount;
                            }
                            paymentTotal += agentPay.GAmount.Value;
                        }
                        loopCount++;
                    }
                    agentPayList[0].TotalBillAmount = paymentTotal;
                    agentPayList[firstNewDeposit].TotalBillAmount = paymentTotal;
                    _presenter.InsertAgentPayment(agentPayList);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool IsPaymentExist()
        {
            bool result = false;
            foreach(AgentPayment tmpAent in agentPayList)
            {
                if(!tmpAent.IsSysData)
                {
                    result = true;
                }
            }
            return result;
        }

        #endregion

        private void txtAmount_Click(object sender, EventArgs e)
        {
            txtAmount.Focus();
            txtAmount.SelectAll();
        }
         
    }
}

