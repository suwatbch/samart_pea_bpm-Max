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
    public partial class UserProperties : Form
    {
        private User _user;
        private RoleUserSwitchViewPresenter _presenter;

        public UserProperties()
        {
            InitializeComponent();
            roleGv.AutoGenerateColumns = false;
            this.Cursor = Cursors.AppStarting;
        }

        public User ChosenUser
        {
            set { _user = value; 
                fullNameTxt.Text = string.Format("{0}-{1}", _user.UserId, _user.FullName );
                timer.Enabled = true;
            }
        }

        public RoleUserSwitchViewPresenter Presenter
        {
            set { _presenter = value; }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                BindingList<Role> roles = _presenter.ListRolesByUser(_user.UserId);
                roleGv.DataSource = roles;
            }
            finally
            {
                timer.Enabled = false;
                this.Cursor = Cursors.Default;
            }
        }


    }
}
