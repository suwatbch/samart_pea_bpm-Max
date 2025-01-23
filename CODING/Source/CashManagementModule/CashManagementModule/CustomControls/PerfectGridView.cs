using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace PEA.BPM.CashManagementModule.CustomControls
{
    public class PerfectGridView : DataGridView
    {       
        private System.ComponentModel.IContainer _components = null;

        public PerfectGridView()
        {
            _components = new System.ComponentModel.Container();
        }

        public PerfectGridView(IContainer container)
        {
            container.Add(this);
            _components = new System.ComponentModel.Container();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
        }


        [System.Security.Permissions.UIPermission(
            System.Security.Permissions.SecurityAction.LinkDemand,
            Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Extract the key code from the key value. 
            Keys key = (keyData & Keys.KeyCode);

            // Handle the ENTER key as if it were a RIGHT ARROW key. 
            if (key == Keys.Enter)
            {
                return this.ProcessRightKey(keyData);
            }

            return base.ProcessDialogKey(keyData);
        }

        public new bool ProcessRightKey(Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);

            if (key == Keys.Enter)
                return true;

            return base.ProcessRightKey(keyData);

        }

        [System.Security.Permissions.SecurityPermission(
            System.Security.Permissions.SecurityAction.LinkDemand, Flags =
            System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            // Handle the ENTER key as if it were a RIGHT ARROW key. 
            if (e.KeyCode == Keys.Enter)
            {
                return this.ProcessRightKey(e.KeyData);
            }
            return base.ProcessDataGridViewKey(e);
        }  

        
    }
}
