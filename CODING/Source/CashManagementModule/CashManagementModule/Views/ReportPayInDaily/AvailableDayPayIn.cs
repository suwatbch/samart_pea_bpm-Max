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
    public partial class AvailableDayPayIn : Form
    {
        private List<ReportAvailableInfo> _bankKeyList;


        public AvailableDayPayIn()
        {
            InitializeComponent();
            payInGv.AutoGenerateColumns = false;
            _bankKeyList = new List<ReportAvailableInfo>();
        }

        public List<ReportAvailableInfo> BankKeyList
        {
            get { return _bankKeyList; }
        }

        public List<ReportAvailableInfo> AvailableList
        {
            set { payInGv.DataSource = value; }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in payInGv.Rows)
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
                        _bankKeyList.Add(selectedItem);
                    }
                }

                if (_bankKeyList.Count == 0)
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