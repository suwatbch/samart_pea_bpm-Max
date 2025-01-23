namespace PEA.BPM.PaymentCollectionModule.Views.QRPaymentView
{
    partial class QRPaymentView
    {


        private PEA.BPM.PaymentCollectionModule.Views.QRPaymentView.QRPaymentViewPresenter _presenter = null;


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
            if (disposing && (components != null))
            {
                if (_presenter != null)
                    _presenter.Dispose();

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
            this.okButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkOffLineQRPayment = new System.Windows.Forms.CheckBox();
            this.qrReferencetextBox = new System.Windows.Forms.TextBox();
            this.qrAmounttextBox = new System.Windows.Forms.TextBox();
            this.qrRef2textBox = new System.Windows.Forms.TextBox();
            this.qrRef1textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkStatusQRButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okButton.ForeColor = System.Drawing.Color.Black;
            this.okButton.Location = new System.Drawing.Point(537, 266);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(130, 36);
            this.okButton.TabIndex = 53;
            this.okButton.Text = "ตกลง";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.VisibleChanged += new System.EventHandler(this.okButton_VisibleChanged);
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(790, 247);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ชำระเงินด้วย QR Payment";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblStatus);
            this.panel4.Controls.Add(this.chkOffLineQRPayment);
            this.panel4.Controls.Add(this.qrReferencetextBox);
            this.panel4.Controls.Add(this.qrAmounttextBox);
            this.panel4.Controls.Add(this.qrRef2textBox);
            this.panel4.Controls.Add(this.qrRef1textBox);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.checkStatusQRButton);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 36);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5);
            this.panel4.Size = new System.Drawing.Size(784, 208);
            this.panel4.TabIndex = 24;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(102, 177);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(533, 19);
            this.lblStatus.TabIndex = 56;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // chkOffLineQRPayment
            // 
            this.chkOffLineQRPayment.AutoSize = true;
            this.chkOffLineQRPayment.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chkOffLineQRPayment.Location = new System.Drawing.Point(529, 83);
            this.chkOffLineQRPayment.Name = "chkOffLineQRPayment";
            this.chkOffLineQRPayment.Size = new System.Drawing.Size(102, 23);
            this.chkOffLineQRPayment.TabIndex = 60;
            this.chkOffLineQRPayment.Text = "Offline QR";
            this.chkOffLineQRPayment.UseVisualStyleBackColor = true;
            this.chkOffLineQRPayment.CheckedChanged += new System.EventHandler(this.chkOffLineQRPayment_CheckedChanged);
            // 
            // qrReferencetextBox
            // 
            this.qrReferencetextBox.BackColor = System.Drawing.SystemColors.Window;
            this.qrReferencetextBox.ForeColor = System.Drawing.Color.MediumBlue;
            this.qrReferencetextBox.Location = new System.Drawing.Point(102, 128);
            this.qrReferencetextBox.Name = "qrReferencetextBox";
            this.qrReferencetextBox.ReadOnly = true;
            this.qrReferencetextBox.Size = new System.Drawing.Size(533, 40);
            this.qrReferencetextBox.TabIndex = 59;
            this.qrReferencetextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // qrAmounttextBox
            // 
            this.qrAmounttextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.qrAmounttextBox.ForeColor = System.Drawing.Color.MediumBlue;
            this.qrAmounttextBox.Location = new System.Drawing.Point(102, 71);
            this.qrAmounttextBox.Name = "qrAmounttextBox";
            this.qrAmounttextBox.ReadOnly = true;
            this.qrAmounttextBox.Size = new System.Drawing.Size(364, 40);
            this.qrAmounttextBox.TabIndex = 58;
            this.qrAmounttextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // qrRef2textBox
            // 
            this.qrRef2textBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.qrRef2textBox.ForeColor = System.Drawing.Color.MediumBlue;
            this.qrRef2textBox.Location = new System.Drawing.Point(529, 16);
            this.qrRef2textBox.Name = "qrRef2textBox";
            this.qrRef2textBox.ReadOnly = true;
            this.qrRef2textBox.Size = new System.Drawing.Size(247, 40);
            this.qrRef2textBox.TabIndex = 57;
            // 
            // qrRef1textBox
            // 
            this.qrRef1textBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.qrRef1textBox.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.qrRef1textBox.ForeColor = System.Drawing.Color.MediumBlue;
            this.qrRef1textBox.Location = new System.Drawing.Point(102, 16);
            this.qrRef1textBox.Name = "qrRef1textBox";
            this.qrRef1textBox.ReadOnly = true;
            this.qrRef1textBox.Size = new System.Drawing.Size(364, 40);
            this.qrRef1textBox.TabIndex = 55;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(12, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 19);
            this.label4.TabIndex = 55;
            this.label4.Text = "รหัสอ้างอิง";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkStatusQRButton
            // 
            this.checkStatusQRButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.checkStatusQRButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.checkStatusQRButton.ForeColor = System.Drawing.Color.MediumBlue;
            this.checkStatusQRButton.Location = new System.Drawing.Point(641, 128);
            this.checkStatusQRButton.Name = "checkStatusQRButton";
            this.checkStatusQRButton.Size = new System.Drawing.Size(135, 40);
            this.checkStatusQRButton.TabIndex = 0;
            this.checkStatusQRButton.Text = "ตรวจสอบสถานะ";
            this.checkStatusQRButton.UseVisualStyleBackColor = false;
            this.checkStatusQRButton.Click += new System.EventHandler(this.checkStatusQRButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(15, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 19);
            this.label3.TabIndex = 51;
            this.label3.Text = "จำนวนเงิน";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(477, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 19);
            this.label1.TabIndex = 49;
            this.label1.Text = "Ref.2";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(46, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 19);
            this.label2.TabIndex = 47;
            this.label2.Text = "Ref.1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel1.Location = new System.Drawing.Point(11, 11);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(818, 315);
            this.panel1.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cancelButton.Location = new System.Drawing.Point(673, 266);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(127, 36);
            this.cancelButton.TabIndex = 55;
            this.cancelButton.Text = "ยกเลิก";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // QRPaymentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(117)))), ((int)(((byte)(191)))));
            this.Controls.Add(this.panel1);
            this.Name = "QRPaymentView";
            this.Size = new System.Drawing.Size(840, 336);
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox qrReferencetextBox;
        private System.Windows.Forms.TextBox qrAmounttextBox;
        private System.Windows.Forms.TextBox qrRef2textBox;
        private System.Windows.Forms.TextBox qrRef1textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button checkStatusQRButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox chkOffLineQRPayment;
        private System.Windows.Forms.Label lblStatus;

    }
}
