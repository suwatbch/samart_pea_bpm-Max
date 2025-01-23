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
    public partial class FillMoneyTypeForm : Form
    {
        private ReportDailyRemainInfo _reportData;

        public FillMoneyTypeForm(ReportDailyRemainInfo reportData)
        {
            InitializeComponent();
            moneyTypeGv.SubTotalTxt = subTotalTxt;
            moneyTypeGv.OkButton = previewBt;
            _reportData = reportData;
            //cash of before 15.30
            totalAmtTxt.Text = reportData.OverallCashAmt.Value.ToString("#,###.00");
        }

        private void moneyTypeGv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                moneyTypeGv.EndEdit();
                if (e.RowIndex != 6)
                    moneyTypeGv.CurrentCell = moneyTypeGv.Rows[e.RowIndex].Cells[1];
                else
                    moneyTypeGv.CurrentCell = moneyTypeGv.Rows[e.RowIndex].Cells[2];

                moneyTypeGv.BeginEdit(true);
            }
        }

        private void previewBt_Click(object sender, EventArgs e)
        {
            try
            {
                //fill count
                //_reportData.C1000 = Convert.ToInt32(moneyTypeGv.Rows[0].Cells[1].Value);
                //_reportData.C500 = Convert.ToInt32(moneyTypeGv.Rows[1].Cells[1].Value);
                //_reportData.C100 = Convert.ToInt32(moneyTypeGv.Rows[2].Cells[1].Value);
                //_reportData.C50 = Convert.ToInt32(moneyTypeGv.Rows[3].Cells[1].Value);
                //_reportData.C20 = Convert.ToInt32(moneyTypeGv.Rows[4].Cells[1].Value);
                //_reportData.C10 = Convert.ToInt32(moneyTypeGv.Rows[5].Cells[1].Value);

                _reportData.C1000 = moneyTypeGv.Rows[0].Cells[1].Value.ToString();
                _reportData.C500 = moneyTypeGv.Rows[1].Cells[1].Value.ToString();
                _reportData.C100 = moneyTypeGv.Rows[2].Cells[1].Value.ToString();
                _reportData.C50 = moneyTypeGv.Rows[3].Cells[1].Value.ToString();
                _reportData.C20 = moneyTypeGv.Rows[4].Cells[1].Value.ToString();
                _reportData.C10 = moneyTypeGv.Rows[5].Cells[1].Value.ToString();

                //fill amount
                //_reportData.Amt1000 = (moneyTypeGv.Rows[0].Cells[2].Value.ToString().Split('.'))[0];
                //_reportData.Amt500 = (moneyTypeGv.Rows[1].Cells[2].Value.ToString().Split('.'))[0];
                //_reportData.Amt100 = (moneyTypeGv.Rows[2].Cells[2].Value.ToString().Split('.'))[0];
                //_reportData.Amt50 = (moneyTypeGv.Rows[3].Cells[2].Value.ToString().Split('.'))[0];
                //_reportData.Amt20 = (moneyTypeGv.Rows[4].Cells[2].Value.ToString().Split('.'))[0];
                //_reportData.Amt10 = (moneyTypeGv.Rows[5].Cells[2].Value.ToString().Split('.'))[0];
                //_reportData.CoinAmt = (moneyTypeGv.Rows[6].Cells[2].Value.ToString().Split('.'))[0];
                //_reportData.CoinAmt_Frag = (moneyTypeGv.Rows[6].Cells[2].Value.ToString().Split('.'))[1];

                _reportData.Amt1000 = moneyTypeGv.Rows[0].Cells[2].Value.ToString();
                _reportData.Amt500 = moneyTypeGv.Rows[1].Cells[2].Value.ToString();
                _reportData.Amt100 = moneyTypeGv.Rows[2].Cells[2].Value.ToString();
                _reportData.Amt50 = moneyTypeGv.Rows[3].Cells[2].Value.ToString();
                _reportData.Amt20 = moneyTypeGv.Rows[4].Cells[2].Value.ToString();
                _reportData.Amt10 = moneyTypeGv.Rows[5].Cells[2].Value.ToString();
                _reportData.CoinAmt = moneyTypeGv.Rows[6].Cells[2].Value.ToString();
                _reportData.CoinAmt_Frag = (moneyTypeGv.Rows[6].Cells[2].Value.ToString().Split('.'))[1];

                if (Convert.ToDecimal(subTotalTxt.Text) != Convert.ToDecimal(totalAmtTxt.Text))
                {
                    MessageBox.Show("คุณตรวจนับเงินไม่ตรงกับยอดเงิน", "ตรวจนับผิด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Internal error!");
            }
        }
    }
}