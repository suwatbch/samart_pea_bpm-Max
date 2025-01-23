using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace PEA.BPM.ePaymentsModule 
{
    public partial class PrinterSelectionForm : Form
    {
        public PrinterSelectionForm(string printerType)
        {
            InitializeComponent();

            this.Text = string.Format(" กรุณาเลือกเครื่องพิมพ์สำหรับพิมพ์ '{0}'", printerType);
            LoadPrinterList();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("ท่านต้องการยกเลิกการพิมพ์ใช่หรือไม่", "คำเตือน", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        public string SelectedPrinterName
        {
            get
            {
                return comboBox1.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadPrinterList();
        }

        private void LoadPrinterList()
        {
            comboBox1.Items.Clear();

            foreach (string p in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(p);
            }
        }
    }
}