//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// This class is the concrete implementation of a View in the Model-View-Presenter 
// pattern. Communication between the Presenter and this class is acheived through 
// an interface to facilitate separation and testability.
// Note that the Presenter generated by the same recipe, will automatically be created
// by CAB through [CreateNew] and bidirectional references will be added.
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using System.Data;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Reporting.WinForms;
using PEA.BPM.Infrastructure.Interface;
using System.Collections.Generic;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ToolModule.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;

namespace PEA.BPM.ToolModule
{
    [SmartPart]
    public partial class ReportContainerView : UserControl, IReportContainerView
    {
        #region "Properties and Variables"
      
        string _setLabel;
        string _reportName;
        List<ReportUnlockingLog> _reportUnlockingLog;
        List<SyncCrossCheckInfo> _syncCrossCheckInfo;
        List<CAC05Report> _testPrintPOSReport;
        List<BillBookInfoDetailReport> _testPrintAGENYReport;
        List<ReportF16> _testPrintBLAN_F16Report;

        public List<ReportF16> TestPrintBLAN_F16Report
        {
            get { return _testPrintBLAN_F16Report; }
            set
            {
                _testPrintBLAN_F16Report = value;
                BindToTestPrintBLAN_F16Report();
            }
        }

        public List<BillBookInfoDetailReport> TestPrintAGENYReport
        {
            get { return _testPrintAGENYReport; }
            set
            {
                _testPrintAGENYReport = value;
                BindToTestPrintAGENCYReport();
            }
        }

        public List<CAC05Report> TestPrintPOSReport
        {
            get { return _testPrintPOSReport; }
            set 
            {
                _testPrintPOSReport = value;
                BindToTestPrintPOSReport();
            }
        }

        public List<SyncCrossCheckInfo> SyncCrossCheckInfo
        {
            get { return _syncCrossCheckInfo; }
            set 
            { 
                _syncCrossCheckInfo = value;
                BindToSyncReport();
            }
        }
        
        public string ReportName
        {
            get { return _reportName; }
            set { _reportName = value; }
        }

        public List<ReportUnlockingLog> ReportUnlockingLog
        {
            get { return _reportUnlockingLog; }
            set 
            { 
                _reportUnlockingLog = value;
                BindToReport();
            }
        }

        public string SetLabel
        {
            get
            {
                return reportNameLabel.Text;
            }
            set
            {
                reportNameLabel.Text = value;
            }
        }           

        #endregion

        #region "Code Generated"
        public ReportContainerView()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public ReportContainerViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();
            //this.rdlcViewer.Refresh();
        }
        #endregion

        #region "Event Handling"

        private void closeReportLabel_Click(object sender, EventArgs e)
        {
            _presenter.OnCloseView();
        }

        #endregion

        #region "Function"

        public void SetLabelMethod(string text)
        {
            reportNameLabel.Text = text;
        }

        private void BindToReport()
        {
            try
            {
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_ToolModule_Interface_BusinessEntities_ReportUnlockingLog";
                sReportDataSource.Value = _reportUnlockingLog;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                //ReportParameter[] rParam = new ReportParameter[2];
                //rParam[0] = new ReportParameter("parPrintDate", _printDate.Value.ToString("ddMMyyyy"));
                //rParam[2] = new ReportParameter("parBillPeriod", dt.Rows[0]["BillPeriod"].ToString());
                //rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                //rdlcViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindToSyncReport()
        {
            try
            {
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_ToolModule_Interface_BusinessEntities_SyncCrossCheckInfo";
                sReportDataSource.Value = _syncCrossCheckInfo;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[1];
                rParam[0] = new ReportParameter("parReportBranch", Session.Branch.Name);
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;

                //rdlcViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindToTestPrintPOSReport()
        {
            try
            {
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_PaymentCollectionModule_Interface_BusinessEntities_Reports_CAC05Report";
                sReportDataSource.Value = _testPrintPOSReport;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[3];
                rParam[0] = new ReportParameter("parTransDate", "���ͺ");
                rParam[1] = new ReportParameter("parBranchDetail", "�Ң�����Ѻ���ͺ   ");
                rParam[2] = new ReportParameter("parSearchBy", "*** ������ �ӡ�÷��ͺ��þ���� ***");

                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;

                //rdlcViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BindToTestPrintAGENCYReport()
        {
            try
            {
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_AgencyManagementModule_Interface_BusinessEntities_BillBookInfoDetailReport";
                sReportDataSource.Value = _testPrintAGENYReport;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[3];
                rParam[0] = new ReportParameter("BillBookId", "���ͺ  ");
                rParam[1] = new ReportParameter("PrintDate", "���ͺ   ");
                rParam[2] = new ReportParameter("BranchName", "*** ��§ҹ����Ѻ���ͺ ���᷹***");

                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;

                //rdlcViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BindToTestPrintBLAN_F16Report()
        {
            try
            {
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportF16";
                sReportDataSource.Value = _testPrintBLAN_F16Report;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[1];
                rParam[0] = new ReportParameter("parBillPeriod", "���ͺ  ");

                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;

                //rdlcViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion   
    }
}

