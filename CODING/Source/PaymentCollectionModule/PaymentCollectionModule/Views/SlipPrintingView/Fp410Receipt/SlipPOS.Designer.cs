namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.Fp410Receipt
{
    partial class SlipPOS
    {
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
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rdlcViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rdlcViewer
            // 
            this.rdlcViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdlcViewer.LocalReport.ReportEmbeddedResource = "Receipt.Receipt.rdlc";
            this.rdlcViewer.Location = new System.Drawing.Point(0, 0);
            this.rdlcViewer.Name = "rdlcViewer";
            this.rdlcViewer.Size = new System.Drawing.Size(392, 516);
            this.rdlcViewer.TabIndex = 1;
            // 
            // SlipPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 516);
            this.Controls.Add(this.rdlcViewer);
            this.Name = "SlipPOS";
            this.Text = "SlipPOS";
            this.Load += new System.EventHandler(this.SlipPOS_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rdlcViewer;
    }
}