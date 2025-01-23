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
    public partial class ChangeScopeView : Form
    {
        private User _user;
        private string _orgScopeId;
        private string _orgScopeText;
        private bool loaded = false;
        private int orgScopeInex;
        private RoleUserSwitchViewPresenter _presenter;

        public ChangeScopeView()
        {
            InitializeComponent();
        }

        public User ChosenUser
        {
            get { return _user; }
            set 
            { 
                _user = value;
                _orgScopeId = _user.ScopeId;
                _orgScopeId = _user.ScopeText;

                userNameText.Text = string.Format("{0}-{1}", _user.UserId, _user.FullName);

                if (_user.ScopeId == "B")
                {
                    scopeCBox.SelectedIndex = 0;
                    orgScopeInex = 0;
                }
                else if (_user.ScopeId == "R")
                {
                    scopeCBox.SelectedIndex = 1;
                    orgScopeInex = 1;
                }
                else
                {
                    scopeCBox.SelectedIndex = 2;
                    orgScopeInex = 2;
                }

                cancelBt.Focus();
                loaded = true;
            }
        }

        public RoleUserSwitchViewPresenter Presenter
        {
            set { _presenter = value; }
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            _user.PwdState = 0; //not touch password
        }

        private void scopeCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TO DO - validate user scope
            if (scopeCBox.SelectedIndex == 0) //BA
            {
                _user.ScopeId = "B";
                _user.ScopeText = "การไฟฟ้าสังกัด";
            }
            else if (scopeCBox.SelectedIndex == 1)
            {
                _user.ScopeId = "R"; //regional
                _user.ScopeText = "เขตการไฟฟ้า";
            }
            else
            {
                _user.ScopeId = "A"; //all
                _user.ScopeText = "ทุกการไฟฟ้า";
            }

            if (!_presenter.ValidateUserScope(_user))
            {
                scopeCBox.SelectedIndex = orgScopeInex;
                return;
            }

            if (loaded && orgScopeInex != scopeCBox.SelectedIndex)
                okBt.Enabled = true;
            else
                okBt.Enabled = false;
        }
    }
}
