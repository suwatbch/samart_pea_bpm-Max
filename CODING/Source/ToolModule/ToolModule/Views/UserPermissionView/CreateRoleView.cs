using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;

namespace PEA.BPM.ToolModule
{
    public partial class CreateRoleView : Form
    {
        private RoleUserSwitchViewPresenter _presenter;
        private Role _role;

        public CreateRoleView()
        {
            InitializeComponent();
            functionGridView.AutoGenerateColumns = false;
            this.Cursor = Cursors.AppStarting;
        }

        public RoleUserSwitchViewPresenter Presenter
        {
            set { 
                _presenter = value;
                timer.Enabled = true;
            }
        }

        public Role CreatedRole
        {
            get { return _role; }
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (roleNameText.Text == string.Empty || descText.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาระบุชื่อกลุ่มและรายละเอียดกลุ่มที่ต้องการสร้าง", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                _role = new Role();
                _role.RoleName = roleNameText.Text;
                _role.Description = descText.Text;
                _role.ModifiedBy = Session.User.Id;
                List<Function> fncList = new List<Function>();

                //keep only checked
                foreach (DataGridViewRow r in functionGridView.Rows)
                {
                    if ((bool)r.Cells[0].Value)
                        fncList.Add((Function)r.DataBoundItem);
                }

                _role.FncList = fncList;
                _presenter.CreateRole(_role);

                MessageBox.Show("สร้างกลุ่มผู้ใช้ใหม่เสร็จเรียบร้อย", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Tools, ex);
            }
            finally
            {
                this.DialogResult = DialogResult.OK;
                this.Cursor = Cursors.Default;
                this.Close();
            }
        }

        private void functionGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                foreach (DataGridViewRow r in functionGridView.SelectedRows)
                {
                    if ((bool)r.Cells[0].Value)
                        r.Cells[0].Value = false;
                    else
                        r.Cells[0].Value = true;
                }
            }
        }

        private void chooseAllBt_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in functionGridView.Rows)
                r.Cells[0].Value = true;
        }

        private void chooseNoneBt_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in functionGridView.Rows)
                r.Cells[0].Value = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                functionGridView.DataSource = _presenter.ListAllFunction();
            }
            finally
            {
                timer.Enabled = false;
                this.Cursor = Cursors.Default;
            }
        }

    }
}
