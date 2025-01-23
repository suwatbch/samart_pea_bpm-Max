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
    public partial class AddRoleUserView : Form
    {
        private RoleUserSwitchViewPresenter _presenter;
        private User _user;
        private Role _role;

        public AddRoleUserView()
        {
            InitializeComponent();
            this.Cursor = Cursors.AppStarting;
            timer.Enabled = true;
        }

        public User ChosenUser
        {
            set { 
                    _user = value;
                    userNameText.Text = string.Format("{0}-{1}", _user.UserId, _user.FullName);
                }
            get { return _user; }
        }

        public Role ChosenRole
        {
            set { _role = value; }
            get { return _role; }
        }

        public RoleUserSwitchViewPresenter Presenter
        {
            set { _presenter = value; }
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            _role = (Role)roleCBox.SelectedItem;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                roleCBox.DataSource = _presenter.ListExpRoles(_user);
                roleCBox.DisplayMember = "RoleName";
                roleCBox.ValueMember = "RoleId";
            }
            finally
            {
                timer.Enabled = false;
                this.Cursor = Cursors.Default;
            }
        }
    }
}
