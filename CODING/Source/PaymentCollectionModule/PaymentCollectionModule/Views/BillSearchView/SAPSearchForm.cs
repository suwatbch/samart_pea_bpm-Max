using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.Views.BillSearchView
{
    public partial class SAPSearchForm : Form
    {
        SAPSearchParam _searchParam;
        public SAPSearchParam SearchParam
        {
            get { return _searchParam; }
        }

        public SAPSearchForm()
        {
            InitializeComponent();
        }

        private void SAPSearchForm_Load(object sender, EventArgs e)
        {
            caIdTextBox.Focus();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void DoSearch()
        {
            _searchParam = new SAPSearchParam();
            _searchParam.CaId = StringConvert.ToString(caIdTextBox.Text.Trim());
            _searchParam.CaDocNo = null;
            _searchParam.InvoiceNo = null;
            _searchParam.PosId = Session.Terminal.Id;

            if (_searchParam.CaId != null)
            {
                if (_searchParam.CaId.Length == 32)
                {
                    _searchParam.CaId = _searchParam.CaId.Substring(8, 12);
                }
                else if ((_searchParam.CaId.Length == 33) )
                {
                    _searchParam.CaId = _searchParam.CaId.Substring(0, 12);
                }
                else if ((_searchParam.CaId.Length == 34) )
                {
                    _searchParam.CaId = _searchParam.CaId.Substring(1, 12);
                }
                //Begin of New Barcode
                else if (_searchParam.CaId.Length >= 36)
                {
                    _searchParam.CaId = _searchParam.CaId.Substring(16, 12);
                }
                //End of New Barcode

                if (_searchParam.CaId != null && _searchParam.CaId.Length < 12)
                {
                    _searchParam.CaId = _searchParam.CaId.PadLeft(12, '0');
                }
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void caIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (caIdTextBox.Text.Trim().Substring(0, 1) == "|")
                {
                    if (caIdTextBox.Text.Trim().Length >= 36)
                    {
                        DoSearch();
                    }
                }
                else
                {
                    DoSearch();
                }
            }
        }
    }
}