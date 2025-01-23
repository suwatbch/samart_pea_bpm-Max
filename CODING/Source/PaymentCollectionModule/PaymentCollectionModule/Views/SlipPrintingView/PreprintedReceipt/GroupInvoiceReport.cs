using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using Microsoft.Practices.ObjectBuilder;


namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.PreprintedReceipt
{
    public class GroupInvoiceReport : WorkItemController
    {
        //private IGroupInvoicingReport _GroupInvoicingReport;
        //private GroupInvoicingReport _groupInvoicingReport;
        //private ISlipPrintingView sp = new ISlipPrintingView();

        public static GroupInvoiceReport Instance()
        {
            return new GroupInvoiceReport();
        }

        //private SlipPrePrintingViewPresenter _presenter = new SlipPrePrintingViewPresenter();        

        public void Print(string groupInvoiceNo, string paymentId, string receiptId, IReportService reportService)
        {
            ReportPreview p = new ReportPreview();
            p.SetDatasource(groupInvoiceNo, paymentId, receiptId, reportService);
            p.WindowState   = System.Windows.Forms.FormWindowState.Maximized;
            p.Focus();
            p.ShowDialog();

            #region NOT USE CODE
            //ISlipPrintingView sp = new ISlipPrintingView();
            //ISlipPrintingView sp ;

            //GroupInvoicingReportParam param = new GroupInvoicingReportParam();
            //param.GroupInvoiceNo = groupInvoiceNo;
            //param.ReceiptId = receiptId;
            //param.ReportService = reportService;
            
            
            
            //sp.ShowReport(param);

            //GroupInvoicingReport _presenter = _groupInvoicingReport.Presenter;
            //_presenter.ShowReport(groupInvoiceNo,receiptId,reportService);

            //_presenter.OnShowGroupInvoicingReport(param);

            //_presenter.ShowGroupInvoicingReport(param);

            //IGroupInvoicingReport _GroupInvoicingReport = new WorkItem.Items.AddNew<GroupInvoicingReport>("IGroupInvoicingReport");
            //_GroupInvoicingReport = WorkItem.Items.Get<GroupInvoicingReport>("IGroupInvoicingReport");   
            //_GroupInvoicingReport.ShowReport(GroupInvoiceNo, receiptId, reportService);
            #endregion
        }
    }
}
