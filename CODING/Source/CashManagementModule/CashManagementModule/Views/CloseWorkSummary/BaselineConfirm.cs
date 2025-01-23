using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.CashManagementModule
{
    public partial class BaselineConfirm : Form
    {
        public BaselineConfirm()
        {
            InitializeComponent();
        }

        public string Desc
        {
            set { desc.Text = value; }
        }
    }
}