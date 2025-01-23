
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

namespace PEA.BPM.ToolModule
{
    partial class ReportContainerView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.ToolModule.ReportContainerViewPresenter _presenter = null;

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
            this.reportNameLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.closeReportLabel = new System.Windows.Forms.Label();
            this.rdlcViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportNameLabel
            // 
            this.reportNameLabel.AutoSize = true;
            this.reportNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.reportNameLabel.ForeColor = System.Drawing.Color.White;
            this.reportNameLabel.Location = new System.Drawing.Point(3, 4);
            this.reportNameLabel.Name = "reportNameLabel";
            this.reportNameLabel.Size = new System.Drawing.Size(103, 18);
            this.reportNameLabel.TabIndex = 4;
            this.reportNameLabel.Text = "ReportName";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(211, 117, 192);
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.reportNameLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.closeReportLabel);
            this.splitContainer1.Size = new System.Drawing.Size(914, 25);
            this.splitContainer1.SplitterDistance = 301;
            this.splitContainer1.TabIndex = 6;
            // 
            // closeReportLabel
            // 
            this.closeReportLabel.AutoSize = true;
            this.closeReportLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeReportLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.closeReportLabel.ForeColor = System.Drawing.Color.White;
            this.closeReportLabel.Location = new System.Drawing.Point(557, 0);
            this.closeReportLabel.Name = "closeReportLabel";
            this.closeReportLabel.Size = new System.Drawing.Size(52, 18);
            this.closeReportLabel.TabIndex = 8;
            this.closeReportLabel.Text = "Close";
            this.closeReportLabel.Click += new System.EventHandler(this.closeReportLabel_Click);
            // 
            // rdlcViewer
            // 
            this.rdlcViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdlcViewer.Location = new System.Drawing.Point(0, 25);
            this.rdlcViewer.Name = "rdlcViewer";
            this.rdlcViewer.Size = new System.Drawing.Size(914, 704);
            this.rdlcViewer.TabIndex = 0;
            // 
            // ReportContainerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.rdlcViewer);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ReportContainerView";
            this.Size = new System.Drawing.Size(914, 729);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label reportNameLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Microsoft.Reporting.WinForms.ReportViewer rdlcViewer;
        private System.Windows.Forms.Label closeReportLabel;
    }
}

