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
    public partial class RoleFunctionView : Form
    {
        private Role _role;
        private RoleUserSwitchViewPresenter _presenter;

        public RoleFunctionView()
        {
            InitializeComponent();
            functionGv.AutoGenerateColumns = false;
            this.Cursor = Cursors.AppStarting;
        }

        public RoleUserSwitchViewPresenter Presenter
        {
            set { _presenter = value;  }
        }

        public Role ChosenRole
        {
            set { _role = value;
                timer.Enabled = true;
                functionListGb.Text = value.RoleName;
            }
            get { return _role; }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                BindingList<Function> bindList = new BindingList<Function>();
                BindingList<Function> fncList = _presenter.ListFunctions(_role.RoleId);
                foreach(Function fnc in fncList)
                    if(fnc.Check) bindList.Add(fnc);

                functionGv.DataSource = bindList;
            }
            finally
            {
                timer.Enabled = false;
                this.Cursor = Cursors.Default;

            }
        }


    }
}
