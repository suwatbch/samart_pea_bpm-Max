using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.BillPrintingModule.BillPrintingUtilities
{
    public partial class CAInputList : Form
    {
        public CAInputList()
        {
            InitializeComponent();
        }

        private void CAInputList_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CAInputList_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}