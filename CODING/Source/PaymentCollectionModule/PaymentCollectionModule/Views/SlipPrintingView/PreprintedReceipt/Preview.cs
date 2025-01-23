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

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.PreprintedReceipt
{
    public partial class Preview : Form
    {
        private List<Header> _hs;
        private List<Detail> _ds;
        private List<ReceiptPaymentMethod> _pm;

        public Preview()
        {
            InitializeComponent();
        }

        private void Preview_Load(object sender, EventArgs e)
        {
            ReportDataSource hs_dataSource = new ReportDataSource();
            hs_dataSource.Name = "PEA_BPM_PaymentCollectionModule_Views_SlipPrintingView_PreprintedReceipt_Header";
            hs_dataSource.Value = _hs;

            ReportDataSource ds_dataSource = new ReportDataSource();
            ds_dataSource.Name = "PEA_BPM_PaymentCollectionModule_Views_SlipPrintingView_PreprintedReceipt_Detail";
            ds_dataSource.Value = _ds;

            ReportDataSource pm_dataSource = new ReportDataSource();
            pm_dataSource.Name = "PEA_BPM_PaymentCollectionModule_Views_SlipPrintingView_PreprintedReceipt_ReceiptPaymentMethod";
            pm_dataSource.Value = _pm;

            rdlcViewer.Reset();
            rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            rdlcViewer.LocalReport.ReportEmbeddedResource = "PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.PreprintedReceipt.Preprinted.rdlc";
            rdlcViewer.LocalReport.DataSources.Add(hs_dataSource);
            rdlcViewer.LocalReport.DataSources.Add(ds_dataSource);
            rdlcViewer.LocalReport.DataSources.Add(pm_dataSource);
            rdlcViewer.RefreshReport();
        }

        public void PrintReport()
        {
            ReportDataSource hs_dataSource = new ReportDataSource();
            hs_dataSource.Name = "PEA_BPM_PaymentCollectionModule_Views_SlipPrintingView_PreprintedReceipt_Header";
            hs_dataSource.Value = _hs;

            ReportDataSource ds_dataSource = new ReportDataSource();
            ds_dataSource.Name = "PEA_BPM_PaymentCollectionModule_Views_SlipPrintingView_PreprintedReceipt_Detail";
            ds_dataSource.Value = _ds;

            ReportDataSource pm_dataSource = new ReportDataSource();
            pm_dataSource.Name = "PEA_BPM_PaymentCollectionModule_Views_SlipPrintingView_PreprintedReceipt_ReceiptPaymentMethod";
            pm_dataSource.Value = _pm;

            ReportViewer rdlcViewer = new ReportViewer();
            rdlcViewer.LocalReport.ReportEmbeddedResource = "PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.PreprintedReceipt.Preprinted.rdlc";
            rdlcViewer.LocalReport.DataSources.Add(hs_dataSource);
            rdlcViewer.LocalReport.DataSources.Add(ds_dataSource);
            rdlcViewer.LocalReport.DataSources.Add(pm_dataSource);

            OutputToPrinter(rdlcViewer);
        }

        internal void SetDatasource(List<Header> hs, List<Detail> ds, List<ReceiptPaymentMethod> pm)
        {
            this._hs = hs;
            this._ds = ds;
            this._pm = pm;
        }

        //Note: Set paper size for receipt form at the printer
        //Width = 15.5 cm., Height = 25.4 cm. || Width = 6.1 Inch, Height = 10 Inch
        //Send "Preprinted Report" to print on default printer
        private void OutputToPrinter(ReportViewer viewer)
        {
            LocalSettingHelper hp = LocalSettingHelper.Instance();
            string preprintedPrinter = hp.ReadString(LocalSettingNames.PrePrintedPrinterName);

            if (null == preprintedPrinter || preprintedPrinter == string.Empty)
            {
                PrinterSelectionForm form = new PrinterSelectionForm("ใบเสร็จรับเงินทั่วไป (Pre-Printed Form)");
                if (DialogResult.OK == form.ShowDialog())
                {
                    preprintedPrinter = form.SelectedPrinterName;
                    hp.Update(LocalSettingNames.PrePrintedPrinterName, preprintedPrinter);
                }
                else
                {
                    return;
                }
            }

            PrintUtility _printer = new PrintUtility(preprintedPrinter, 610, 1000);
            _printer.Export(viewer.LocalReport);
            _printer.CurrentPageIndex = 0;
            _printer.Print();
        }
    }
}