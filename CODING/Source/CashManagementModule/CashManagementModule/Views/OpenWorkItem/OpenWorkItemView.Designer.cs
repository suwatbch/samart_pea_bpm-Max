
//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

namespace PEA.BPM.CashManagementModule
{
    partial class OpenWorkItemView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.CashManagementModule.OpenWorkItemViewPresenter _presenter = null;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_presenter != null)
                    _presenter.Dispose();

                if (components != null)
                    components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.couponGridView = new System.Windows.Forms.DataGridView();
            this.CouponId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CouponBankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CouponAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CouponType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlowId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancelButton = new System.Windows.Forms.Button();
            this.cashInitShiftTxt = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.totalTxt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkSumFooterGv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.couponGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkSumFooterGv)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-124, -19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "�Թʴ";
            // 
            // couponGridView
            // 
            this.couponGridView.AllowUserToAddRows = false;
            this.couponGridView.AllowUserToDeleteRows = false;
            this.couponGridView.AllowUserToResizeRows = false;
            this.couponGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.couponGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.couponGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.couponGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.couponGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.couponGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.couponGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CouponId,
            this.CouponBankName,
            this.CouponAmount,
            this.CouponType,
            this.FlowId});
            this.couponGridView.Location = new System.Drawing.Point(6, 56);
            this.couponGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.couponGridView.MultiSelect = false;
            this.couponGridView.Name = "couponGridView";
            this.couponGridView.ReadOnly = true;
            this.couponGridView.RowHeadersVisible = false;
            this.couponGridView.RowTemplate.ReadOnly = true;
            this.couponGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.couponGridView.Size = new System.Drawing.Size(477, 171);
            this.couponGridView.TabIndex = 17;
            // 
            // CouponId
            // 
            this.CouponId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CouponId.DataPropertyName = "ChqNo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CouponId.DefaultCellStyle = dataGridViewCellStyle2;
            this.CouponId.FillWeight = 1F;
            this.CouponId.HeaderText = "�Ţ�����";
            this.CouponId.MinimumWidth = 70;
            this.CouponId.Name = "CouponId";
            this.CouponId.ReadOnly = true;
            this.CouponId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // CouponBankName
            // 
            this.CouponBankName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CouponBankName.DataPropertyName = "BankName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CouponBankName.DefaultCellStyle = dataGridViewCellStyle3;
            this.CouponBankName.FillWeight = 1F;
            this.CouponBankName.HeaderText = "��Ҥ��";
            this.CouponBankName.Name = "CouponBankName";
            this.CouponBankName.ReadOnly = true;
            // 
            // CouponAmount
            // 
            this.CouponAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CouponAmount.DataPropertyName = "Amount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.CouponAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.CouponAmount.FillWeight = 1F;
            this.CouponAmount.HeaderText = "�ӹǹ�Թ";
            this.CouponAmount.Name = "CouponAmount";
            this.CouponAmount.ReadOnly = true;
            // 
            // CouponType
            // 
            this.CouponType.DataPropertyName = "ChqType";
            this.CouponType.HeaderText = "CouponType";
            this.CouponType.Name = "CouponType";
            this.CouponType.ReadOnly = true;
            this.CouponType.Visible = false;
            // 
            // FlowId
            // 
            this.FlowId.DataPropertyName = "FlowId";
            this.FlowId.HeaderText = "FlowId";
            this.FlowId.Name = "FlowId";
            this.FlowId.ReadOnly = true;
            this.FlowId.Visible = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Enabled = false;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 15.75F);
            this.cancelButton.Location = new System.Drawing.Point(244, 355);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(118, 40);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "���";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Visible = false;
            // 
            // cashInitShiftTxt
            // 
            this.cashInitShiftTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cashInitShiftTxt.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cashInitShiftTxt.ForeColor = System.Drawing.Color.Red;
            this.cashInitShiftTxt.Location = new System.Drawing.Point(262, 19);
            this.cashInitShiftTxt.Name = "cashInitShiftTxt";
            this.cashInitShiftTxt.ReadOnly = true;
            this.cashInitShiftTxt.Size = new System.Drawing.Size(180, 27);
            this.cashInitShiftTxt.TabIndex = 18;
            this.cashInitShiftTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.addButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.addButton.Font = new System.Drawing.Font("Tahoma", 15.75F);
            this.addButton.Location = new System.Drawing.Point(368, 355);
            this.addButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(118, 40);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "��ŧ";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // totalTxt
            // 
            this.totalTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.totalTxt.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalTxt.ForeColor = System.Drawing.Color.Green;
            this.totalTxt.Location = new System.Drawing.Point(149, 19);
            this.totalTxt.Name = "totalTxt";
            this.totalTxt.ReadOnly = true;
            this.totalTxt.Size = new System.Drawing.Size(337, 53);
            this.totalTxt.TabIndex = 14;
            this.totalTxt.Text = "0.00";
            this.totalTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.checkSumFooterGv);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cashInitShiftTxt);
            this.groupBox1.Controls.Add(this.couponGridView);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(5, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 259);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "��������´�ʹ¡��";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(448, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "�ҷ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(8, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 16);
            this.label3.TabIndex = 21;
            this.label3.Text = "��¡�� ";
            // 
            // checkSumFooterGv
            // 
            this.checkSumFooterGv.AllowUserToAddRows = false;
            this.checkSumFooterGv.AllowUserToDeleteRows = false;
            this.checkSumFooterGv.AllowUserToResizeRows = false;
            this.checkSumFooterGv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkSumFooterGv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.checkSumFooterGv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.checkSumFooterGv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.checkSumFooterGv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.checkSumFooterGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.checkSumFooterGv.ColumnHeadersVisible = false;
            this.checkSumFooterGv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.checkSumFooterGv.GridColor = System.Drawing.SystemColors.Control;
            this.checkSumFooterGv.Location = new System.Drawing.Point(6, 226);
            this.checkSumFooterGv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkSumFooterGv.Name = "checkSumFooterGv";
            this.checkSumFooterGv.ReadOnly = true;
            this.checkSumFooterGv.RowHeadersVisible = false;
            this.checkSumFooterGv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.checkSumFooterGv.Size = new System.Drawing.Size(477, 27);
            this.checkSumFooterGv.TabIndex = 20;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "Desc";
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(173, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "�Թʴ¡��:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.totalTxt);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(493, 80);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(8, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 33);
            this.label5.TabIndex = 20;
            this.label5.Text = "�ʹ¡��:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 420);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cancelButton);
            this.panel2.Controls.Add(this.addButton);
            this.panel2.Location = new System.Drawing.Point(8, 8);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.panel2.Size = new System.Drawing.Size(502, 404);
            this.panel2.TabIndex = 3;
            // 
            // OpenWorkItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Name = "OpenWorkItemView";
            this.Size = new System.Drawing.Size(517, 420);
            ((System.ComponentModel.ISupportInitialize)(this.couponGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkSumFooterGv)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView couponGridView;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox cashInitShiftTxt;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TextBox totalTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView checkSumFooterGv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn CouponId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CouponBankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CouponAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CouponType;
        private System.Windows.Forms.DataGridViewTextBoxColumn FlowId;







    }
}

