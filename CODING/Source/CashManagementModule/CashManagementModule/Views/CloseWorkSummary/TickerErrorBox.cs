using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.CashManagementModule
{
    public partial class TickerErrorBox : Form
    {
        private string _msg;

        public TickerErrorBox(string msg)
        {
            InitializeComponent();
            _msg = msg;
        }

        private void detailBt_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_msg, "รายละเอียดข้อผิดพลาดของการส่งข้อมูลไปยัง SAP ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}