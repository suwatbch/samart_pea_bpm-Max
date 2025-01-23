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
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;

namespace PEA.BPM.PaymentCollectionModule
{
    [SmartPart]
    public partial class RemeterView : UserControl, IRemeterView
    {
        private bool _isOffLine = false;
        private string _branchId;
        private string _techBranchName;
        private string _commBranchId;
        private string _commBranchName;
        private string _caId;
        private string _disconnectStrLine;
        private string _reconnectStrLine;
        private string _contractType;
        private string _controllerId;
        private string _controllerName;
        private string _mruId;


        private string _originalInvoiceNo;
        private LastDisconnect _aLastDisconnect;

        private string caId = "";
        private string disconnectDocNo = "";

        private List<ToBePaidInvoice> _tmpList;

        public List<ToBePaidInvoice> TmpList
        {
            get { return _tmpList; }
            set { this._tmpList = value; }
        }

        public RemeterView()
        {
            InitializeComponent();          
        }

        public string CaId
        {
            set
            {
                this._caId = value;
            }
        }

        public string OriginalInvoiceNo
        {
            set
            {
                this._originalInvoiceNo = value;
            }
        }

        public string disconnectStrLine 
        {
            set
            {
                this._disconnectStrLine = value;
            }
        }

        public string reconnectStrLine 
        {
            set
            {
                this._reconnectStrLine = value;
            }
        }



        private void LoadComboBox()
        {
            cutOffDateDateTimePicker.Value = Session.BpmDateTime.Now;
            List<MeterSize> meterSizes = new List<MeterSize>(CodeTable.Instant.ListMeterSizes());
            meterSizes.Insert(0, new MeterSize("", "", null));

            meterSizeComboBox.DisplayMember = "MeterSizeName";
            meterSizeComboBox.ValueMember = "MeterSizeId";
            meterSizeComboBox.DataSource = meterSizes;


            List<TaxCode> taxCodes = CodeTable.Instant.ListTaxCode().FindAll(delegate(TaxCode tc)
            {
                return tc.Name.IndexOf("���") > -1;
            }
            );

            DebtType reconnectDebtType = CodeTable.Instant.ListDebtTypes().Find(delegate(DebtType dt)
                {
                    return dt.DebtId == CodeTable.Instant.GetAppSettingValue(CodeNames.DebtType.ReConnectMeter.Id);
                }
            );

            vatRateComboBox.DisplayMember = "Name";
            vatRateComboBox.ValueMember = "Code";
            vatRateComboBox.DataSource = taxCodes;
            if (reconnectDebtType != null && reconnectDebtType.DefaultTaxCode!=null)
            {
                vatRateComboBox.SelectedValue = reconnectDebtType.DefaultTaxCode;
            }            
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public RemeterViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        private void LoadData()
        {
            LoadComboBox();

            customerIdMaskedTextBox.Text = string.Empty;
            nameMaskedTextBox.Text = string.Empty;
            addressMaskedTextBox.Text = string.Empty;
            cutOffDateDateTimePicker.Value = Session.BpmDateTime.Now;
            meterSizeComboBox.SelectedIndex = 0;
            taxLabel.Text = string.Empty;
            amouontExVatLabel.Text = string.Empty;
            reConnectMeterRateLabel.Text = string.Empty;

            onlineStatusLabel.Text = "�к��������ö�Դ��͡Ѻ����ͧ�������� ��سҡ�͡������ ����-�������";
            onlineStatusLabel.Visible = false;

            if(!Session.IsNetworkConnectionAvailable) //==Offline==
            {
                onlineStatusLabel.Visible = true;
                nameMaskedTextBox.ReadOnly = false;
                addressMaskedTextBox.ReadOnly = false;
                meterSizeComboBox.Enabled = true;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadData();

            if (_disconnectStrLine != "")
            {
                List<ToBePaidInvoice> workItemInvoices = _tmpList;
                bool isBreak = false;
                string[] strTemp = _disconnectStrLine.Split(',')[0].Split(':');
                caId = strTemp[0];
                disconnectDocNo = strTemp[1];
                
                foreach (Invoice inv in workItemInvoices)
                {
                    foreach (Bill bill in inv.Bills)
                    {
                        if (bill.CustomerId.Trim() == caId.Trim()
                            && bill.DisconnectDocNo != null
                            && bill.DisconnectDocNo.Trim() == disconnectDocNo.Trim())
                        {
                            cutOffDateDateTimePicker.Value = bill.DisConnectDate!=null ? bill.DisConnectDate.Value : DateTime.Now;
                            customerIdMaskedTextBox.Text = bill.CustomerId;
                            customerIdMaskedTextBox.SelectAll();
                            this.OriginalInvoiceNo = inv.InvoiceNo;
                            isBreak = true;
                            break;
                        }
                    }
                    if (isBreak == true) { break; }
                }
                if(customerIdMaskedTextBox.Text.Length > 0 )
                    SearchCustomerDetail();
            }
            
            _presenter.OnViewReady();
        }

        #region +++ Command Handler +++
        private void searchButton_Click(object sender, EventArgs e)
        {           
            SearchCustomerDetail();
        }

        private void customerIdMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) 
            { 
                customerIdMaskedTextBox.Text = customerIdMaskedTextBox.Text.PadLeft(12, '0'); 
                SearchCustomerDetail(); 
            }
        }

        private void meterSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReCalculateAmount();
        }

        private void ReCalculateAmount()
        {
            if (meterSizeComboBox.SelectedIndex > 0)
            {
                MeterSize ms = (MeterSize)meterSizeComboBox.SelectedItem;

                decimal amount = ms.ReconnectMeterRate.Value;
                reConnectMeterRateLabel.Text = amount.ToString("#,##0.00");

                TaxCode taxCode = (TaxCode)vatRateComboBox.SelectedItem;
                if (null != taxCode.Rate)
                {
                    decimal amountExVat = (amount * 100) / (100 + taxCode.Rate.Value);
                    decimal vat = amount - amountExVat;

                    amouontExVatLabel.Text = amountExVat.ToString("#,##0.00");
                    taxLabel.Text = string.Format("({0})", vat.ToString("#,##0.00"));
                }
                else
                {
                    amouontExVatLabel.Text = amount.ToString("#,##0.00");
                    taxLabel.Text = "(-)";
                }
            }
            else
            {
                taxLabel.Text = string.Empty;
                amouontExVatLabel.Text = string.Empty;
                reConnectMeterRateLabel.Text = string.Empty;
            }
        }

        private bool IsRepeatReconnect()
        { 
            if( _reconnectStrLine != "" )
                if( _reconnectStrLine.IndexOf( customerIdMaskedTextBox.Text.Trim() ) > -1 )
                {
                    MessageBox.Show("��˹���ҵ�͡�Ѻ�������������¡�÷���ͧ��ê�������", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            return false;
        }

        /// <summary>
        /// ��Ǩ�ͺ��� ��Ҹ���������õ�͡�Ѻ������ �ա�ê������ѧ
        /// �� ��ԧ ��� ���Ф�Ҹ���������Ѻ���������� ��������ͧ����(�ó�����¶١�Ѵ�)
        /// �� �� ��� �դ�Ҹ���������õ�͡�Ѻ������ ����ѧ��������
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is paid disconnect]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsPaidDisconnect()
        {
            if (_aLastDisconnect != null)
                if (_aLastDisconnect.PaidDisconnectFlag == true) //�������� ���� ��辺��á�� disconnect �ҡ�͹ �ͧ caId
                {
                    MessageBox.Show("�����˹���ҵ�͡�Ѻ���������ͧ����", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            customerIdMaskedTextBox.SelectAll();
            return _aLastDisconnect.PaidDisconnectFlag;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) { return; }


            if ( !IsRepeatReconnect() )//����ѧ�������¡�����¡�÷���ͧ��ê���
            {
                List<Invoice> invoices = new List<Invoice>();
                Invoice inv = new Invoice();

                Bill b = new Bill();
                b.CustomerId = customerIdMaskedTextBox.Text.Trim();
                b.Name = StringConvert.ToString(nameMaskedTextBox.Text);
                b.Address = StringConvert.ToString(addressMaskedTextBox.Text);
                b.DebtId = CodeTable.Instant.GetAppSettingValue(CodeNames.DebtType.ReConnectMeter.Id);
                b.DebtType = CodeTable.Instant.GetAppSettingValue(CodeNames.DebtType.ReConnectMeter.Name);
                b.Qty = null;
                b.UnitTypeId = null;
                b.UnitTypeName = null;
                
                MeterSize meterSize = (MeterSize)meterSizeComboBox.SelectedItem;
                b.GAmount = meterSize.ReconnectMeterRate;
                b.FullGAmount = b.GAmount;

                TaxCode taxCode = (TaxCode)vatRateComboBox.SelectedItem;
                b.TaxCode = taxCode.Code;
                b.TaxRate = taxCode.Rate;

                if (null != b.TaxRate)
                {
                    b.AmountExVat = (b.GAmount * 100) / ( 100 + b.TaxRate.Value);
                    b.FullAmount = b.AmountExVat;
                    b.VatAmount = b.GAmount - b.AmountExVat;                
                    b.FullVatAmount = b.VatAmount;
                }
                else
                {
                    b.AmountExVat = b.GAmount;
                    b.FullAmount = b.AmountExVat;
                }

                b.ToPayGAmount = b.GAmount;
                b.ToPayVatAmount = b.VatAmount;
                b.DisConnectDate = cutOffDateDateTimePicker.Value;

                b.DisconnectDocNo = disconnectDocNo != "" ? disconnectDocNo : _aLastDisconnect.LastDisconnectDoc;
                b.ContractTypeId = _contractType;

                if (_isOffLine)
                {
                    b.DataState = BillDataStage.Offline;
                    inv.DataState = InvoiceDataStage.Offline;
                }
                else
                {
                    b.DataState = BillDataStage.NewItem;
                    inv.DataState = InvoiceDataStage.NewItem;
                }

                inv.BranchId = _branchId;
                inv.TechBranchName = _techBranchName;
                inv.CommBranchId = _commBranchId;
                inv.CommBranchName = _commBranchName;
                inv.CaId = b.CustomerId;
                inv.Name = b.Name;
                inv.Address = b.Address;
                inv.DueDate = b.DueDate;
                inv.ControllerId = _controllerId;
                inv.ControllerName = _controllerName;
                inv.MruId = _mruId;

                inv.AmountExVat = b.AmountExVat;
                inv.VatAmount = b.VatAmount;
                inv.GAmount = b.GAmount;
                inv.PaidVatAmount = 0;
                inv.PaidGAmount = 0;
                
                inv.ToPayGAmount = inv.ToBePaidGAmount;
                inv.ToPayVatAmount = inv.ToBePaidVatAmount;

                inv.OriginalInvoiceNo = this._originalInvoiceNo;
                inv.Bills = new List<Bill>();
                inv.Bills.Add(b);
                invoices.Add(inv);

                _presenter.InvoicesAddedToList(invoices);
                this.ParentForm.Close();
            }
        }

        private void cutOffDateDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (meterSizeComboBox.Enabled)
                {
                    meterSizeComboBox.Focus();
                }
                else
                {
                    okButton.Focus();
                }
            }
        }

        private void meterSizeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                okButton.Focus();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        public Button CancelButton
        {
            get { return cancelButton; }
        }
        #endregion

        #region +++ Custom Function +++

        private void SearchCustomerDetail()
        {
            customerIdMaskedTextBox.Text = customerIdMaskedTextBox.Text.Trim().Replace(" ","").PadLeft(12);

            if (customerIdMaskedTextBox.Text.Trim() == "")
            {
                MessageBox.Show("��س���������Ţ������", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (customerIdMaskedTextBox.Text.Trim().Length != 12)
            {
                MessageBox.Show("��س���������Ţ���������ú 12 ��ѡ", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Session.IsNetworkConnectionAvailable)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Customer customer = _presenter.GetCustomerDetail(customerIdMaskedTextBox.Text);
                    this.Cursor = Cursors.Default;

                    if (customer != null)
                    {
                        nameMaskedTextBox.Text = customer.Name;
                        addressMaskedTextBox.Text = customer.Address;
                        meterSizeComboBox.SelectedValue = customer.MeterSizeId != null ? customer.MeterSizeId : "";
                        _branchId = customer.BranchId;
                        _techBranchName = customer.TechBranchName;
                        _commBranchId = customer.CommBranchId;
                        _commBranchName = customer.CommBranchName;
                        _contractType = customer.ContractTypeId;
                        _controllerId = customer.ControllerId;
                        _controllerName = customer.ControllerName;
                        _mruId = customer.MruId;

                        //Get Last Disconnection By CaId
                        _aLastDisconnect = _presenter.GetLastDisconnectByCaId(customerIdMaskedTextBox.Text);
                        IsRepeatReconnect();

                        if (_aLastDisconnect.PaidDisconnectFlag == false && _aLastDisconnect.LastDisconnectDate != null)
                            cutOffDateDateTimePicker.Value = _aLastDisconnect.LastDisconnectDate.Value;

                        cutOffDateDateTimePicker.Focus();
                    }
                    else
                    {
                        nameMaskedTextBox.Text = string.Empty;
                        addressMaskedTextBox.Text = string.Empty;
                        cutOffDateDateTimePicker.Value = Session.BpmDateTime.Now;
                        meterSizeComboBox.SelectedIndex = 0;
                        taxLabel.Text = string.Empty;
                        amouontExVatLabel.Text = string.Empty;
                        reConnectMeterRateLabel.Text = string.Empty;
                        MessageBox.Show("��辺�����Ţͧ�����Ţ�����俴ѧ�����", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    ClientExceptionController.ShowExceptionDialog(EErrorInModule.POS, ex);
                }
            }
        }

        private bool ValidateForm()
        {
            if (Session.IsNetworkConnectionAvailable)
            {
                try
                {
                    Customer customer = _presenter.GetCustomerDetail(customerIdMaskedTextBox.Text);

                    if (customer == null)
                    {
                        MessageBox.Show("��辺�����Ţͧ�����Ţ�����俴ѧ�����", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ClientExceptionController.ShowExceptionDialog(EErrorInModule.POS, ex);
                    return false;
                }
            }

            if (customerIdMaskedTextBox.Text.Trim() == "")
            {
                MessageBox.Show("��س���������Ţ������", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (customerIdMaskedTextBox.Text.Trim().Length != 12)
            {
                MessageBox.Show("��س���������Ţ���������ú 12 ��ѡ", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (nameMaskedTextBox.Text.Trim() == "")
            {
                MessageBox.Show("��س�������", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (addressMaskedTextBox.Text.Trim() == "")
            {
                MessageBox.Show("��س����������", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (meterSizeComboBox.SelectedIndex == 0 || meterSizeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("��س����͡��Ҵ������", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }
        #endregion

        private void maskedTextBox_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
                {
                    MaskedTextBox mTb = (sender as MaskedTextBox);
                    if (mTb.Text == string.Empty || mTb.Text == null)
                        mTb.Select(0, 0);
                    else
                        mTb.SelectAll();
                });
        }

    }
}

