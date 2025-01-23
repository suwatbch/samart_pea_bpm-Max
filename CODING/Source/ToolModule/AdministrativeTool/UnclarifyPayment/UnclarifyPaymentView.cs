using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using AdministrativeTool.UnclarifyPaymentWS;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AdministrativeTool.Common;

namespace AdministrativeTool.UnclarifyPayment
{
    public partial class UnclarifyPaymentView : UserControl
    {
        private UnclarifyPaymentService ws = new UnclarifyPaymentService();

        public UnclarifyPaymentView()
        {
            InitializeComponent();
            errorTypeCBox.SelectedIndex = 0;
            unclarifyPaymentGv.AutoGenerateColumns = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string errType = errorTypeCBox.SelectedIndex.ToString();
                DateTime impDt = impDatePicker.Value;
                unclarifyPaymentGv.DataSource = ws.GetUnclarifyPayment(impDt, errType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "เกิดข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}