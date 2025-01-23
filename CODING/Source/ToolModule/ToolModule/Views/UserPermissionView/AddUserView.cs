using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.ToolModule.Interface.BusinessEntities;

namespace PEA.BPM.ToolModule
{
    public partial class AddUserView : Form
    {
        private RoleUserSwitchViewPresenter _presenter;
        private User _user;
        private bool _userMode;

        public AddUserView()
        {
            InitializeComponent();
            userDataGV.AutoGenerateColumns = false;
        }

        public User ChosenUser
        {
            get { return _user; }
        }

        public RoleUserSwitchViewPresenter Presenter
        {
            set { _presenter = value; }
        }

        public bool UserMode
        {
            set { _userMode = value;
            userSearchText.Focus();
            }
        }

        private void userSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.Cursor = Cursors.AppStarting;

                    if (_userMode)
                        userDataGV.DataSource = _presenter.ListAllUsers(userSearchText.Text);
                    else
                        userDataGV.DataSource = _presenter.ListEmployee(userSearchText.Text);

                    if (userDataGV.Rows.Count > 0)
                        okBt.Enabled = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void okBt_Click(object sender, EventArgs e)
        {
            if (userDataGV.SelectedRows.Count > 0)
            {
                _user = (User)userDataGV.SelectedRows[0].DataBoundItem;
            }
        }

        private void userDataGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (userDataGV.SelectedRows.Count > 1)
                okBt.Enabled = false;
            else
                okBt.Enabled = true;
        }

        private void userDataGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _user = (User)userDataGV.SelectedRows[0].DataBoundItem;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
