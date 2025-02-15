
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
    partial class TransferResponseView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.CashManagementModule.TransferResponseViewPresenter _presenter = null;

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
            this.acceptGv = new System.Windows.Forms.DataGridView();
            this.GCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CashAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChequeAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rejectBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.refreshBt = new System.Windows.Forms.Button();
            this.acceptBt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.acceptGv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // acceptGv
            // 
            this.acceptGv.AllowUserToAddRows = false;
            this.acceptGv.AllowUserToDeleteRows = false;
            this.acceptGv.AllowUserToResizeRows = false;
            this.acceptGv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.acceptGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.acceptGv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GCheck,
            this.Description,
            this.CashAmt,
            this.ChequeAmt,
            this.Amount,
            this.TransferId});
            this.acceptGv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acceptGv.GridColor = System.Drawing.SystemColors.Control;
            this.acceptGv.Location = new System.Drawing.Point(3, 20);
            this.acceptGv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.acceptGv.Name = "acceptGv";
            this.acceptGv.RowHeadersVisible = false;
            this.acceptGv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.acceptGv.Size = new System.Drawing.Size(483, 232);
            this.acceptGv.TabIndex = 0;
            this.acceptGv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.acceptGv_CellContentClick);
            // 
            // GCheck
            // 
            this.GCheck.HeaderText = "*";
            this.GCheck.Name = "GCheck";
            this.GCheck.TrueValue = "";
            this.GCheck.Width = 20;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "��¡���͹";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // CashAmt
            // 
            this.CashAmt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CashAmt.DataPropertyName = "CashAmt";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            this.CashAmt.DefaultCellStyle = dataGridViewCellStyle1;
            this.CashAmt.HeaderText = "�Թʴ";
            this.CashAmt.Name = "CashAmt";
            this.CashAmt.ReadOnly = true;
            this.CashAmt.Width = 80;
            // 
            // ChequeAmt
            // 
            this.ChequeAmt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ChequeAmt.DataPropertyName = "ChequeAmt";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.ChequeAmt.DefaultCellStyle = dataGridViewCellStyle2;
            this.ChequeAmt.HeaderText = "��";
            this.ChequeAmt.Name = "ChequeAmt";
            this.ChequeAmt.ReadOnly = true;
            this.ChequeAmt.Width = 80;
            // 
            // Amount
            // 
            this.Amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.Amount.DefaultCellStyle = dataGridViewCellStyle3;
            this.Amount.HeaderText = "���";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 80;
            // 
            // TransferId
            // 
            this.TransferId.DataPropertyName = "TransferId";
            this.TransferId.HeaderText = "TransferId";
            this.TransferId.Name = "TransferId";
            this.TransferId.ReadOnly = true;
            this.TransferId.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.acceptGv);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(489, 256);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "���͡��¡�÷��еͺ�Ѻ";
            // 
            // rejectBt
            // 
            this.rejectBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rejectBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.rejectBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.rejectBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rejectBt.Location = new System.Drawing.Point(318, 263);
            this.rejectBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rejectBt.Name = "rejectBt";
            this.rejectBt.Size = new System.Drawing.Size(85, 25);
            this.rejectBt.TabIndex = 5;
            this.rejectBt.Text = "����ʸ";
            this.rejectBt.UseVisualStyleBackColor = false;
            this.rejectBt.Click += new System.EventHandler(this.rejectBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBt.Location = new System.Drawing.Point(409, 264);
            this.cancelBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(85, 25);
            this.cancelBt.TabIndex = 3;
            this.cancelBt.Text = "�͡";
            this.cancelBt.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 311);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.refreshBt);
            this.panel2.Controls.Add(this.rejectBt);
            this.panel2.Controls.Add(this.cancelBt);
            this.panel2.Controls.Add(this.acceptBt);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(9, 7);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(503, 296);
            this.panel2.TabIndex = 0;
            // 
            // refreshBt
            // 
            this.refreshBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.refreshBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshBt.Image = global::PEA.BPM.CashManagementModule.Properties.Resources.RepeatHSR;
            this.refreshBt.Location = new System.Drawing.Point(8, 263);
            this.refreshBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.refreshBt.Name = "refreshBt";
            this.refreshBt.Size = new System.Drawing.Size(88, 25);
            this.refreshBt.TabIndex = 16;
            this.refreshBt.Text = "  �����";
            this.refreshBt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.refreshBt.UseVisualStyleBackColor = false;
            this.refreshBt.Click += new System.EventHandler(this.refreshBt_Click);
            // 
            // acceptBt
            // 
            this.acceptBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.acceptBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acceptBt.Location = new System.Drawing.Point(227, 263);
            this.acceptBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.acceptBt.Name = "acceptBt";
            this.acceptBt.Size = new System.Drawing.Size(85, 25);
            this.acceptBt.TabIndex = 4;
            this.acceptBt.Text = "�ͺ�Ѻ";
            this.acceptBt.UseVisualStyleBackColor = false;
            this.acceptBt.Click += new System.EventHandler(this.acceptBt_Click);
            // 
            // TransferResponseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Name = "TransferResponseView";
            this.Size = new System.Drawing.Size(520, 311);
            ((System.ComponentModel.ISupportInitialize)(this.acceptGv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView acceptGv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button rejectBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button acceptBt;
        private System.Windows.Forms.Button refreshBt;
        private System.Windows.Forms.DataGridViewCheckBoxColumn GCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn CashAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChequeAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferId;





    }
}

