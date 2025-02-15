
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
    partial class TransferStatusView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.CashManagementModule.TransferStatusViewPresenter _presenter = null;

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
            this.acceptGv = new System.Windows.Forms.DataGridView();
            this.GCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CashAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChequeAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DispStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Receiver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rejectBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.printSlipBt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.acceptGv)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // acceptGv
            // 
            this.acceptGv.AllowUserToAddRows = false;
            this.acceptGv.AllowUserToDeleteRows = false;
            this.acceptGv.AllowUserToResizeRows = false;
            this.acceptGv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.acceptGv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.acceptGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.acceptGv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GCheck,
            this.Description,
            this.CashAmt,
            this.ChequeAmt,
            this.TotalAmt,
            this.DispStatus,
            this.TransferId,
            this.Receiver,
            this.RFullName,
            this.SFullName,
            this.Status,
            this.Sender});
            this.acceptGv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acceptGv.GridColor = System.Drawing.SystemColors.Control;
            this.acceptGv.Location = new System.Drawing.Point(3, 20);
            this.acceptGv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.acceptGv.Name = "acceptGv";
            this.acceptGv.RowHeadersVisible = false;
            this.acceptGv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.acceptGv.Size = new System.Drawing.Size(555, 248);
            this.acceptGv.TabIndex = 0;
            this.acceptGv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.acceptGv_CellFormatting);
            this.acceptGv.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.acceptGv_CellPainting);
            // 
            // GCheck
            // 
            this.GCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GCheck.HeaderText = "*";
            this.GCheck.Name = "GCheck";
            this.GCheck.ReadOnly = true;
            this.GCheck.Width = 22;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "����Ѻ�͹";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // CashAmt
            // 
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
            this.ChequeAmt.DataPropertyName = "ChequeAmt";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.ChequeAmt.DefaultCellStyle = dataGridViewCellStyle2;
            this.ChequeAmt.HeaderText = "��";
            this.ChequeAmt.Name = "ChequeAmt";
            this.ChequeAmt.ReadOnly = true;
            this.ChequeAmt.Width = 80;
            // 
            // TotalAmt
            // 
            this.TotalAmt.DataPropertyName = "Amount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.TotalAmt.DefaultCellStyle = dataGridViewCellStyle3;
            this.TotalAmt.HeaderText = "���";
            this.TotalAmt.Name = "TotalAmt";
            this.TotalAmt.ReadOnly = true;
            this.TotalAmt.Width = 80;
            // 
            // DispStatus
            // 
            this.DispStatus.DataPropertyName = "DispStatus";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DispStatus.DefaultCellStyle = dataGridViewCellStyle4;
            this.DispStatus.HeaderText = "ʶҹ�";
            this.DispStatus.Name = "DispStatus";
            this.DispStatus.ReadOnly = true;
            // 
            // TransferId
            // 
            this.TransferId.DataPropertyName = "TransferId";
            this.TransferId.HeaderText = "TransferId";
            this.TransferId.Name = "TransferId";
            this.TransferId.Visible = false;
            // 
            // Receiver
            // 
            this.Receiver.DataPropertyName = "Receiver";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Receiver.DefaultCellStyle = dataGridViewCellStyle5;
            this.Receiver.HeaderText = "Receiver";
            this.Receiver.Name = "Receiver";
            this.Receiver.Visible = false;
            this.Receiver.Width = 195;
            // 
            // RFullName
            // 
            this.RFullName.DataPropertyName = "FullName";
            this.RFullName.HeaderText = "ReceiverFullName";
            this.RFullName.Name = "RFullName";
            this.RFullName.Visible = false;
            // 
            // SFullName
            // 
            this.SFullName.DataPropertyName = "SenderName";
            this.SFullName.HeaderText = "SenderFullName";
            this.SFullName.Name = "SFullName";
            this.SFullName.Visible = false;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle6;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Visible = false;
            this.Status.Width = 90;
            // 
            // Sender
            // 
            this.Sender.DataPropertyName = "Sender";
            this.Sender.HeaderText = "Sender";
            this.Sender.Name = "Sender";
            this.Sender.Visible = false;
            // 
            // rejectBt
            // 
            this.rejectBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rejectBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.rejectBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.rejectBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rejectBt.Location = new System.Drawing.Point(369, 280);
            this.rejectBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rejectBt.Name = "rejectBt";
            this.rejectBt.Size = new System.Drawing.Size(95, 29);
            this.rejectBt.TabIndex = 5;
            this.rejectBt.Text = "¡��ԡ�͹";
            this.rejectBt.UseVisualStyleBackColor = false;
            this.rejectBt.Click += new System.EventHandler(this.rejectBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBt.Location = new System.Drawing.Point(470, 280);
            this.cancelBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(93, 29);
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
            this.panel1.Size = new System.Drawing.Size(590, 331);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.printSlipBt);
            this.panel2.Controls.Add(this.rejectBt);
            this.panel2.Controls.Add(this.cancelBt);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(9, 7);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(573, 316);
            this.panel2.TabIndex = 0;
            // 
            // printSlipBt
            // 
            this.printSlipBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printSlipBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.printSlipBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printSlipBt.Image = global::PEA.BPM.CashManagementModule.Properties.Resources.PrintHS;
            this.printSlipBt.Location = new System.Drawing.Point(8, 280);
            this.printSlipBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.printSlipBt.Name = "printSlipBt";
            this.printSlipBt.Size = new System.Drawing.Size(95, 29);
            this.printSlipBt.TabIndex = 6;
            this.printSlipBt.Text = "  �������Ի";
            this.printSlipBt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.printSlipBt.UseVisualStyleBackColor = false;
            this.printSlipBt.Click += new System.EventHandler(this.printSlipBt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.acceptGv);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(561, 272);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "��¡���͹�͡";
            // 
            // TransferStatusView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Name = "TransferStatusView";
            this.Size = new System.Drawing.Size(590, 331);
            ((System.ComponentModel.ISupportInitialize)(this.acceptGv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView acceptGv;
        private System.Windows.Forms.Button rejectBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button printSlipBt;
        private System.Windows.Forms.DataGridViewCheckBoxColumn GCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn CashAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChequeAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Receiver;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sender;






    }
}

