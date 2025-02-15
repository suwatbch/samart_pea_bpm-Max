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
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule
{
    [SmartPart]
    public partial class CustomerSearchView : UserControl, ICustomerSearchView
    {
        public CustomerSearchView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public CustomerSearchViewPresenter Presenter
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
            customerIdMaskedTextBox.Focus();

            if (!Session.IsNetworkConnectionAvailable)
            {
                searchPanel.Enabled = false;
            }
            else
            {
                searchPanel.Enabled = true;
            }            
        }

        private void searchByIdButton_Click(object sender, EventArgs e)
        {
            SearchById();
        }

        private void SearchById()
        {
            string customerId = customerIdMaskedTextBox.Text.Trim();
            if (customerId == string.Empty)
            {
                MessageBox.Show("��س���������Ţ������/������/�����١˹��", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (customerId.Length == 32)
            {
                string branchId = ModuleHelper.GetBranch(customerId.Substring(0, 8));
                customerId = customerId.Substring(8, 12);
                customerIdMaskedTextBox.Text = customerId;
                LocalSettingHelper hp = LocalSettingHelper.Instance();
                if (hp.ReadString(LocalSettingHelper.BranchId) != branchId)
                {
                    otherBranchByIdCheckBox.Checked = true;
                }
            }
            if (customerId.Length < 12)
            {
                customerId = customerId.PadLeft(12, '0');
                customerIdMaskedTextBox.Text = customerId;
            }
            if (customerId.Length != 12)
            {
                MessageBox.Show("��س���������Ţ������/������/�����١˹�����ú 12 ��ѡ", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                customerIdMaskedTextBox.SelectAll();
                return;
            }

            CustomerSearchParam param = new CustomerSearchParam();
            param.CaId = StringConvert.ToString(customerId);
            param.IsOtherBranch = otherBranchByIdCheckBox.Checked;
            _presenter.InvoiceSearchedById(param);
            customerIdMaskedTextBox.SelectAll();
        }

        private void customerIdMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SearchById();
            }
        }

        private void clearSearchByIdButton_Click(object sender, EventArgs e)
        {
            ClearSearchById();
        }

        private void ClearSearchById()
        {
            customerIdMaskedTextBox.Text = string.Empty;
            otherBranchByIdCheckBox.Checked = false;

            customerIdMaskedTextBox.Focus();
        }
    }
}

