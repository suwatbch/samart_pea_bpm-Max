using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;

namespace PEA.BPM.CashManagementModule
{
    public partial class AvailableCashierList : Form
    {
        private List<string> _cashierList;

        public AvailableCashierList()
        {
            InitializeComponent();
            cashierGv.AutoGenerateColumns = false;
            _cashierList = new List<string>();
        }

        public List<string> CashierList
        {
            get { return _cashierList; }
        }

        public List<ReportAvailableInfo> AvailableList
        {
            set { 
                cashierGv.DataSource = value;
                if (value.Count <= 0)
                    okBt.Enabled = false;
                else
                    okBt.Enabled = true;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in cashierGv.Rows)
                {
                    bool check = false;
                    object obj = r.Cells["Select"].Value;
                    if (obj != null)
                        check = (bool)obj;
                    else
                        continue;

                    if (check)
                    {
                        ReportAvailableInfo selectedItem = (ReportAvailableInfo)r.DataBoundItem;
                        _cashierList.Add(selectedItem.CashierId);
                    }
                }


                if (_cashierList.Count == 0)
                    MessageBox.Show("กรุณาเลือกรายการที่ต้องการออกรายงาน", "กรุณาเลือก", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดภายใน", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}