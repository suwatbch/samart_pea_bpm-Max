using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.ToolModule
{
    public partial class EditRoleView : Form
    {
        private Role _role;
        private BindingList<Function> _fncList;
        private RoleUserSwitchViewPresenter _presenter;

        public EditRoleView()
        {
            InitializeComponent();
            functionGridView.AutoGenerateColumns = false;
        }

        public Role ChosenRole
        {
            set
            {
                _role = value;
                roleNameText.Text = _role.RoleName;
                descText.Text = _role.Description;
                timer.Enabled = true;
                functionGridView.Focus();
            }
        }

        public RoleUserSwitchViewPresenter Presenter
        {
            set { _presenter = value; }
        }

        private void selectAllBt_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in functionGridView.Rows)
                ((Function)r.DataBoundItem).Check = true;

            functionGridView.Refresh();
            okBt.Enabled = true;
        }

        private void selectNoneBt_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in functionGridView.Rows)
                ((Function)r.DataBoundItem).Check = false;

            functionGridView.Refresh();
            okBt.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.AppStarting;
                _fncList = _presenter.ListFunctions(_role.RoleId);
                functionGridView.DataSource = _fncList;
                cancelBt.Focus();
            }
            finally
            {
                timer.Enabled = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            try
            {
                List<Function> chosenFnc = new List<Function>();
                _role.RoleName = roleNameText.Text;
                _role.Description = descText.Text;

                foreach (Function fnc in _fncList)
                    if (fnc.Check) chosenFnc.Add(fnc);

                _role.FncList = chosenFnc;
                //TO DO - validate user scope
                if (_presenter.ValidateUserScope(_role))
                {
                    _presenter.EditRole(_role);
                    MessageBox.Show("แก้ไขกลุ่มผู้ใช้งานเสร็จเรียบร้อย", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Tools, ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
