using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.ToolModule.Views.OptionView
{
    public partial class BarcodesForm : Form
    {
        public BarcodesForm()
        {
            InitializeComponent();
        }

        private string _barcode1Start;

        public string Barcode1Start
        {
            get { return _barcode1Start; }
            set
            {
                _barcode1Start = value;
                printerCode1StartTextBox.Text = value;
            }
        }

        private string _barcode2Start;

        public string Barcode2Start
        {
            get { return _barcode2Start; }
            set
            {
                _barcode2Start = value;
                printerCode2StartTextBox.Text = value;
            }
        }

        private string _barcode1Stop;

        public string Barcode1Stop
        {
            get { return _barcode1Stop; }
            set
            {
                _barcode1Stop = value;
                printerCode1StopTextBox.Text = value;
            }
        }

        private string _barcode2Stop;

        public string Barcode2Stop
        {
            get { return _barcode2Stop; }
            set
            {
                _barcode2Stop = value;
                printerCode2StopTextBox.Text = value;
            }
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            Barcode1Start = printerCode1StartTextBox.Text;
            Barcode1Stop = printerCode1StopTextBox.Text;
            Barcode2Start = printerCode2StartTextBox.Text;
            Barcode2Stop = printerCode2StopTextBox.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
