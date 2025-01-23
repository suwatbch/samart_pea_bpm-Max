using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.PrintUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using System.Globalization;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.PreprintedReceipt
{
    public partial class ReportPreview : Form
    {
        private IReportService _reportService;

        [InjectionConstructor]
        public ReportPreview([ServiceDependency] IReportService reportService)
        {
            _reportService = reportService;
        }

        private string _groupInvoiceNo;
        private string _paymentId;
        private string _receiptId;

        public ReportPreview()
        {
            InitializeComponent();
        }

        private void ReportPreview_Load(object sender, EventArgs e)
        {
            CAC14Param param = new CAC14Param();
            param.GroupInvoiceNo = _groupInvoiceNo;
            param.PaymentId = _paymentId;
            param.ReceiptId = _receiptId;
            param.BranchId = Session.Branch.Id;
            List<CAC14Report>  reportCAC14Data = _reportService.GetReportCAC14(param);
            ReportDataSource dataSource = new ReportDataSource();
            dataSource.Name = "PEA_BPM_PaymentCollectionModule_Interface_BusinessEntities_Reports_CAC14Report";
            dataSource.Value = reportCAC14Data;

            ReportParameter[] rParam = new ReportParameter[5];
            rParam[0] = new ReportParameter("parCashierId", reportCAC14Data[0].CashierId);
            rParam[1] = new ReportParameter("parPaymentDt", reportCAC14Data[0].PaymentDt.Value.ToString("d MMMM yyyy", new CultureInfo("th-TH")));
            rParam[2] = new ReportParameter("parReceiptId", reportCAC14Data[0].ReceiptId);  

            Branch branch = CodeTable.Instant.ListBranches(Session.Branch.Id);
            rParam[3] = new ReportParameter("parBranchDetail", branch.BranchName);

            rParam[4] = new ReportParameter("parCAC14HeaderText", reportCAC14Data[0].GroupInvoiceHeaderText);  

            rdlcViewer.Reset();
            rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            rdlcViewer.LocalReport.ReportEmbeddedResource = "PEA.BPM.PaymentCollectionModule.Reports.CAC14Report.rdlc";
            rdlcViewer.LocalReport.DataSources.Add(dataSource);
            rdlcViewer.LocalReport.SetParameters(rParam);
            rdlcViewer.RefreshReport();
        }

        internal void SetDatasource(string groupInvoiceNo, string paymentId, string receiptId, IReportService reportService)
        {
            this._groupInvoiceNo = groupInvoiceNo;
            this._paymentId = paymentId;
            this._receiptId = receiptId;
            this._reportService = reportService;
        }
    }
}