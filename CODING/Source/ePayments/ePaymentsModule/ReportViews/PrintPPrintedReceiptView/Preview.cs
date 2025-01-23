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
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.ePaymentsModule.Interface.Constants;

namespace PEA.BPM.ePaymentsModule 
{
    public partial class Preview : Form
    {
        private List<PPrintedReceipt> pPrintedList;
        private List<PPrintedDeposit> pPrintedDepositList;
        public Preview()
        {
            InitializeComponent();
        }

        private void Preview_Load(object sender, EventArgs e)
        {

            ReportDataSource dataSource = new ReportDataSource();
            dataSource.Name = "PEA_BPM_ePaymentsModule_Interface_BusinessEntities_ReceiptPrinting_PPrintedReceipt";
            dataSource.Value = pPrintedList;

            rdlcViewer.Reset();
            rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            rdlcViewer.LocalReport.ReportEmbeddedResource = "PEA.BPM.ePaymentsModule.Receipts.Preprinted.rdlc";
            rdlcViewer.LocalReport.DataSources.Add(dataSource);
            rdlcViewer.RefreshReport();
        }

        internal void SetReceiptDatasource(List<PPrintedReceipt> pList)
        {
            this.pPrintedList = pList;
        }

        internal void SetDepositDatasource(PPrintedDeposit pPrinted)
        {
            this.pPrintedDepositList = new List<PPrintedDeposit>();
            this.pPrintedDepositList.Add(pPrinted);
        }

        public void PrintReceiptReport()
        {
            ReportDataSource dataSource = new ReportDataSource();
            dataSource.Name = "PEA_BPM_ePaymentsModule_Interface_BusinessEntities_ReceiptPrinting_PPrintedReceipt";
            dataSource.Value = pPrintedList;

            ReportViewer rdlcViewer = new ReportViewer();
            rdlcViewer.LocalReport.ReportEmbeddedResource = "PEA.BPM.ePaymentsModule.Receipts.Preprinted.rdlc";
            rdlcViewer.LocalReport.DataSources.Add(dataSource);

            OutputToPrinter(rdlcViewer);
        }

        public void PrintCaDepositReport()
        {
            ReportDataSource dataSource = new ReportDataSource();
            dataSource.Name = "PEA_BPM_ePaymentsModule_Interface_BusinessEntities_ReceiptPrinting_PPrintedDeposit";
            dataSource.Value = pPrintedDepositList;

            ReportViewer rdlcViewer = new ReportViewer();
            rdlcViewer.LocalReport.ReportEmbeddedResource = "PEA.BPM.ePaymentsModule.Receipts.CaDepositPreprinted.rdlc";
            rdlcViewer.LocalReport.DataSources.Add(dataSource);

            OutputToPrinter(rdlcViewer);
        }

        public void PrintAgentDepositReport()
        {
            ReportDataSource dataSource = new ReportDataSource();
            dataSource.Name = "PEA_BPM_ePaymentsModule_Interface_BusinessEntities_ReceiptPrinting_PPrintedDeposit";
            dataSource.Value = pPrintedDepositList;

            ReportViewer rdlcViewer = new ReportViewer();
            rdlcViewer.LocalReport.ReportEmbeddedResource = "PEA.BPM.ePaymentsModule.Receipts.AgentDepositPreprinted.rdlc";
            rdlcViewer.LocalReport.DataSources.Add(dataSource);

            OutputToPrinter(rdlcViewer);
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