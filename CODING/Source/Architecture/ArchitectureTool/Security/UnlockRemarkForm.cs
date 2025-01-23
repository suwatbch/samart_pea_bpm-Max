using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool.Control;

namespace PEA.BPM.Architecture.ArchitectureTool.Security
{
    public partial class UnlockRemarkForm : Form
    {
        private List<UnlockRemark> _remarks;

        public UnlockRemarkForm(List<UnlockRemark> remarks) : this()
        {
            this._remarks = remarks;

            AutoCompleteTextBox<UnlockRemark> actb = new AutoCompleteTextBox<UnlockRemark>(remarkTextBox);
            actb.PopupBackColor = Color.White;
            actb.PopupHeight = 60;
            actb.PopupExtendWidth = 0;
            actb.DataSource = _remarks;
            actb.KeyPress += new KeyPressEventHandler(remarkTextBox_KeyPress);

            remarkComboBox.DataSource = _remarks;
            remarkComboBox.DisplayMember = "Description";
            remarkComboBox.ValueMember = "RemarkId";
        }

        public UnlockRemarkForm()
        {
            InitializeComponent();
        }

        public string Remark
        {
            //get { return AutoCompleteTextBox<UnlockRemark>.GetText(remarkTextBox); }
            get { return remarkComboBox.Text; }
        }

        public string Caption
        {
            set { captionTextBox.Text = value; }
        }

        private void remarkTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { okButton.Focus(); }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (Remark.Trim() != string.Empty)
                this.DialogResult = DialogResult.OK;
            else
                //remarkTextBox.Focus();
                remarkComboBox.Focus();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void remarkComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { okButton.Focus(); }
        }
    }
}