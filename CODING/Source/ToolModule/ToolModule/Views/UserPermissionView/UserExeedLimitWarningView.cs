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
    public partial class UserExeedLimitWarningView : Form
    {
        private RoleUserSwitchViewPresenter _presenter;
        private UserExceed _userExceed;


        public UserExeedLimitWarningView()
        {
            InitializeComponent();
            btnOK.Focus();
        }

        public UserExceed MappedUserExceededDetail
        {
            set { _userExceed = value; }
            get { return _userExceed; }
        }

        public RoleUserSwitchViewPresenter Presenter
        {
            set { _presenter = value; }
        }       

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserExeedLimitWarningView_Load(object sender, EventArgs e)
        {
            UserExceed userDetails = new UserExceed();
            userDetails = this.MappedUserExceededDetail;

            txtUserLimitQty.Text    = userDetails.UserLimit.ToString("#,###");
            txtUserCurrentUsed.Text = userDetails.UserCurrentUsed.ToString("#,###");                 
        }
    }
}
