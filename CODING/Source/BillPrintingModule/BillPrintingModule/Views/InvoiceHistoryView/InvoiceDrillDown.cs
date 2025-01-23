using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;

namespace PEA.BPM.BillPrintingModule
{
    public partial class InvoiceDrillDown : Form
    {
        private List<Invoice> _invoiceList;

        public List<Invoice> InvoiceList
        {
            set {
                _invoiceList = value;
                InvGv.DataSource = _invoiceList;

                invDetailGb.Text = "ºÔÅà´×Í¹ - " + _invoiceList[0].W_130_period.Substring(4, 2) + "/" + _invoiceList[0].W_130_period.Substring(0, 4);
            }
        }

        public InvoiceDrillDown()
        {
            InitializeComponent();
            InvGv.AutoGenerateColumns = false;
        }
    }
}