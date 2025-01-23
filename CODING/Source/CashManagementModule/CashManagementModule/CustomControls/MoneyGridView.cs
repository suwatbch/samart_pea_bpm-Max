using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;

namespace PEA.BPM.CashManagementModule.CustomControls
{
    public class MoneyGridView : DataGridView
    {
        private bool _loadReady = false;
        private Timer timer;
        private IContainer components;
        private System.ComponentModel.IContainer _components = null;
        private TextBox _subTotalTxt;
        private Button _okButton;

        public MoneyGridView()
        {
            _components = new System.ComponentModel.Container();
            
        }

        public TextBox SubTotalTxt
        {
            set { _subTotalTxt = value; }
        }

        public Button OkButton
        {
            set { _okButton = value; }
        }

        public MoneyGridView(IContainer container)
        {
            container.Add(this);
            _components = new System.ComponentModel.Container();
            FillTemplate();

        }

        private void FillTemplate()
        {
            if (!_loadReady)
            {
                this.AutoGenerateColumns = false;
                DataGridViewTextBoxColumn ItemName = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn MCount = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn Amount = new DataGridViewTextBoxColumn();
                ItemName.Name = "ItemName";
                ItemName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                ItemName.HeaderText = "รายการ";
                ItemName.ReadOnly = true;
                ItemName.DefaultCellStyle.BackColor = SystemColors.Control;
                ItemName.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                ItemName.DefaultCellStyle.SelectionForeColor = Color.Black;
                ItemName.SortMode = DataGridViewColumnSortMode.NotSortable;

                MCount.Name = "MCount";
                MCount.HeaderText = "ฉบับ";
                MCount.Width = 80;
                MCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                MCount.SortMode = DataGridViewColumnSortMode.NotSortable;

                Amount.Name = "Amount";
                Amount.HeaderText = "จำนวนเงิน";
                Amount.Width = 100;
                Amount.ReadOnly = true;
                Amount.DefaultCellStyle.BackColor = SystemColors.Control;
                Amount.DefaultCellStyle.SelectionBackColor = SystemColors.Control;
                Amount.DefaultCellStyle.SelectionForeColor = Color.Black;
                Amount.SortMode = DataGridViewColumnSortMode.NotSortable;
                Amount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                this.Columns.Add(ItemName);
                this.Columns.Add(MCount);
                this.Columns.Add(Amount);

                this.Rows.Add(7);
                this.Rows[0].Cells[0].Value = "ธนบัตรราคา           1,000 บาท";
                this.Rows[1].Cells[0].Value = "ธนบัตรราคา              500 บาท";
                this.Rows[2].Cells[0].Value = "ธนบัตรราคา              100 บาท";
                this.Rows[3].Cells[0].Value = "ธนบัตรราคา                50 บาท";
                this.Rows[4].Cells[0].Value = "ธนบัตรราคา                20 บาท";
                this.Rows[5].Cells[0].Value = "ธนบัตรราคา                10 บาท";
                this.Rows[6].Cells[0].Value = "เหรียญชนิดต่าง ๆ";
                //------ 
                this.Rows[0].Cells[2].Value = "0.00";
                this.Rows[1].Cells[2].Value = "0.00";
                this.Rows[2].Cells[2].Value = "0.00";
                this.Rows[3].Cells[2].Value = "0.00";
                this.Rows[4].Cells[2].Value = "0.00";
                this.Rows[5].Cells[2].Value = "0.00";
                this.Rows[6].Cells[2].Value = "0.00";
                this.Rows[6].Cells[2].ReadOnly = false;
                this.Rows[6].Cells[2].Style.BackColor = Color.White;
                this.Rows[6].Cells[2].Style.ForeColor = Color.Black;
                this.Rows[6].Cells[2].Style.SelectionBackColor = this.Rows[1].Cells[1].Style.SelectionBackColor;
                this.Rows[6].Cells[2].Style.SelectionForeColor = this.Rows[1].Cells[1].Style.SelectionForeColor;
                // ----
                this.Rows[0].Cells[1].Value = "0";
                this.Rows[1].Cells[1].Value = "0";
                this.Rows[2].Cells[1].Value = "0";
                this.Rows[3].Cells[1].Value = "0";
                this.Rows[4].Cells[1].Value = "0";
                this.Rows[5].Cells[1].Value = "0";
                this.Rows[5].Cells[1].ReadOnly = true;
                this.Rows[5].Cells[1].Style.BackColor = SystemColors.Control;
                this.Rows[5].Cells[1].Style.ForeColor = Color.Black;
                this.Rows[5].Cells[1].Style.SelectionBackColor = SystemColors.Control;
                this.Rows[5].Cells[1].Style.SelectionForeColor = Color.Black;
                this.Rows[6].Cells[1].ReadOnly = true;
                this.Rows[6].Cells[1].Style.BackColor = SystemColors.Control;
                this.Rows[6].Cells[1].Style.ForeColor = Color.Black;
                this.Rows[6].Cells[1].Style.SelectionBackColor = SystemColors.Control;
                this.Rows[6].Cells[1].Style.SelectionForeColor = Color.Black;
                timer = new Timer();
                //timer.Start();
                _loadReady = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
        }


        [System.Security.Permissions.UIPermission(
            System.Security.Permissions.SecurityAction.LinkDemand,
            Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Extract the key code from the key value. 
            Keys key = (keyData & Keys.KeyCode);

            // Handle the ENTER key as if it were a RIGHT ARROW key. 
            if (key == Keys.Enter ||
                key == Keys.Up ||
                key == Keys.Down )
            {
                return this.ProcessRightKey(keyData);
            }

            return base.ProcessDialogKey(keyData);
        }

        private decimal GetValue(int index)
        {
            if (index == 0)
                return 1000;
            else if (index == 1)
                return 500;
            else if (index == 2)
                return 100;
            else if (index == 3)
                return 50;
            else if (index == 4)
                return 20;
            else if (index == 5)
                return 10;
            else
                return 1;

        }

        private void SumAmount()
        {
            decimal total = 0;
            foreach (DataGridViewRow r in this.Rows)
            {
                decimal amt = Convert.ToDecimal(r.Cells[2].Value);
                total += amt;
            }
            _subTotalTxt.Text = total == 0 ? "0.00" : total.ToString("#,###.00"); 
        }

        public new bool ProcessRightKey(Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);

            if (key == Keys.Enter || key == Keys.Down)
            {
                this.EndEdit();
                decimal total =0;

                try
                {
                    if (this.CurrentRow.Index != 6)
                        total = Convert.ToDecimal(this.Rows[this.CurrentRow.Index].Cells[1].Value) * GetValue(this.CurrentRow.Index);
                    else
                        total = Convert.ToDecimal(this.Rows[this.CurrentRow.Index].Cells[2].Value);

                    string totalTxt = total == 0 ? "0.00" : total.ToString("#,###.00");
                    this.Rows[this.CurrentRow.Index].Cells[2].Value = totalTxt;
                    SumAmount();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("กรุณาป้อนเฉพาะตัวเลขเท่านั้น", "ป้อนข้อมูลผิด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.BeginEdit(true);
                    return true;
                }

                if (this.CurrentRow.Index < this.Rows.Count - 1 && this.CurrentRow.Index != 5)
                {
                    this.CurrentCell = this.Rows[this.CurrentRow.Index + 1].Cells[1];
                    this.BeginEdit(true);
                }
                else if (this.CurrentRow.Index == 5)
                {
                    this.CurrentCell = this.Rows[this.CurrentRow.Index + 1].Cells[2];
                    this.BeginEdit(true);
                }
                else if (this.CurrentRow.Index == 6)
                {
                    this.EndEdit();
                    _okButton.Focus();
                }

                return true;
            }
            else if (key == Keys.Up)
            {
                this.EndEdit();

                decimal total = Convert.ToDecimal(this.Rows[this.CurrentRow.Index].Cells[1].Value) * GetValue(this.CurrentRow.Index);
                string totalTxt = total == 0 ? "0.00" : total.ToString("#,###.00");
                this.Rows[this.CurrentRow.Index].Cells[2].Value = totalTxt;
                SumAmount();

                if (this.CurrentRow.Index > 0)
                    this.CurrentCell = this.Rows[this.CurrentRow.Index - 1].Cells[1];
                this.BeginEdit(true);
                return true;
            }

            return base.ProcessRightKey(keyData);
        }

        [System.Security.Permissions.SecurityPermission(
            System.Security.Permissions.SecurityAction.LinkDemand, Flags =
            System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            // Handle the ENTER key as if it were a RIGHT ARROW key. 
            if (e.KeyCode == Keys.Enter ||
                e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down)
            {
                return this.ProcessRightKey(e.KeyData);
            }
            return base.ProcessDataGridViewKey(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.EndEdit();
            this.CurrentCell = this.Rows[0].Cells[1];
            this.BeginEdit(true);
            timer.Stop();
        }  

        
    }
}
