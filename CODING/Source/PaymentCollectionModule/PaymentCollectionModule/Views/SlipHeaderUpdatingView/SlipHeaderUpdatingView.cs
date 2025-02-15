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
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;


namespace PEA.BPM.PaymentCollectionModule
{
    [SmartPart]
    public partial class SlipHeaderUpdatingView : UserControl, ISlipHeaderUpdatingView
    {
        private Invoice _invoice;

        public SlipHeaderUpdatingView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public SlipHeaderUpdatingViewPresenter Presenter
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

            nameTextBox.Text = _invoice.Name;
            addressTextBox.Text = _invoice.Address;
            caTaxIdTextBox.Text = _invoice.CaTaxId;
            caTaxBranchTextBox.Text = _invoice.CaTaxBranch;
            payByNameTextBox.Text = _invoice.PayByName;
        }

        #region ISlipHeaderUpdatingView Members

        public Button CancelButton
        {
            get { return cancelButton; }
        }

        public Invoice Invoice
        {
            set { _invoice = value; }
        }

        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            //if (caTaxIdTextBox.Text.Trim() == "")
            //{
            //    MessageBox.Show("��س�����Ţ����Шӵ�Ǽ���������� 13 ��ѡ", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            //    caTaxIdTextBox.Text = "";
            //    caTaxIdTextBox.Focus();

            //    return;
            //}
            
            if (caTaxIdTextBox.Text.Trim()!="" && caTaxIdTextBox.Text.Trim().Length < 13)
            {
                MessageBox.Show("��س�����Ţ��Шӵ�Ǽ�������������ú 13 ��ѡ", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                caTaxIdTextBox.Focus();

                return;
            }
            
            if (caTaxBranchTextBox.Text.Trim() != "" && caTaxBranchTextBox.Text.Trim().Length < 4 )
            {
                MessageBox.Show("��س�����Ң� 4 ���� 5 ��ѡ", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                caTaxBranchTextBox.Focus();

                return;
            }

            if (caTaxIdTextBox.Text.Trim() == "" && caTaxBranchTextBox.Text.Trim() != "")
            {
                MessageBox.Show("��س�����Ţ��Шӵ�Ǽ���������� 13 ��ѡ", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                caTaxIdTextBox.Focus();

                return;
            }

            if ( caTaxBranchTextBox.Text.Trim() == "" && caTaxIdTextBox.Text.Trim() != "")
            {
                MessageBox.Show("��س�����Ң�", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                caTaxBranchTextBox.Focus();

                return;
            }

            if (nameTextBox.Text.IndexOf(' ') > 70 || ((nameTextBox.Text.Length > 70) && (nameTextBox.Text.IndexOf(' ') == -1)))
            {
                MessageBox.Show("�������ö�кت��ͷ���դ�������ҡ���� 70 ����ѡ������������ä", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                nameTextBox.Focus();
                return;
            }

            if (addressTextBox.Text.IndexOf(' ') > 70 || ((addressTextBox.Text.Length > 70) && (addressTextBox.Text.IndexOf(' ') == -1)))
            {
                MessageBox.Show("�������ö�кط���������դ�������ҡ���� 70 ����ѡ������������ä", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                addressTextBox.Focus();
                return;
            }

            _invoice.Name = nameTextBox.Text.Trim();
            _invoice.Address = addressTextBox.Text.Trim();
            _invoice.CaTaxId = caTaxIdTextBox.Text.Trim();
            _invoice.CaTaxBranch = caTaxBranchTextBox.Text.Trim();
            _invoice.PayByName = payByNameTextBox.Text.Trim();

            ParentForm.Close();

        }

        private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { caTaxIdTextBox.Focus(); }
        }

        private void caTaxIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8) e.Handled = true;

            if (e.KeyChar == 13) { caTaxBranchTextBox.Focus(); }
            caTaxIdTextBox.OnKeyPressValidateDecimal(e);
        }

        private void caTaxBranchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8) e.Handled = true;

            if (e.KeyChar == 13) { addressTextBox.Focus(); }
            caTaxBranchTextBox.OnKeyPressValidateDecimal(e);
        }

        private void addressTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { okButton.Focus(); }
            
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (nameTextBox.Text.IndexOf(' ') > 70 || ((nameTextBox.Text.Length > 70) && (nameTextBox.Text.IndexOf(' ') == -1)))
                MessageBox.Show("�������ö�кت��ͷ���դ�������ҡ���� 70 ����ѡ������������ä", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void addressTextBox_TextChanged(object sender, EventArgs e)
        {
            if (addressTextBox.Text.IndexOf(' ') > 70 || ((addressTextBox.Text.Length > 70) && (addressTextBox.Text.IndexOf(' ') == -1)))
                MessageBox.Show("�������ö�кط���������դ�������ҡ���� 70 ����ѡ������������ä", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }
}

