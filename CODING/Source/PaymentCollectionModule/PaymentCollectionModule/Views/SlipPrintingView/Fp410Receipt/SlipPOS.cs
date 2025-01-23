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

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.Fp410Receipt
{
    public partial class SlipPOS : Form
    {
        private List<Receipt> _toPrintData;

        public SlipPOS()
        {
            InitializeComponent();
        }

        private void SlipPOS_Load(object sender, EventArgs e)
        {
            ReportDataSource rc_dataSource = new ReportDataSource();
            rc_dataSource.Name = "PEA_BPM_PaymentCollectionModule_Views_SlipPrintingView_Fp410Receipt_Receipt";
            rc_dataSource.Value = _toPrintData;

            rdlcViewer.Reset();
            rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            rdlcViewer.LocalReport.ReportEmbeddedResource = "PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.Fp410Receipt.Receipt.rdlc";
            rdlcViewer.LocalReport.DataSources.Add(rc_dataSource);
            rdlcViewer.RefreshReport();
        }

        internal void SetDatasource(string toPrintData)
        {
            _toPrintData = new List<Receipt>();

            Receipt r = new Receipt();

            r.Body = toPrintData;

            this._toPrintData.Add(r);           
        }

        public void PrintReport()
        {
            ReportDataSource rc_dataSource = new ReportDataSource();
            rc_dataSource.Name = "PEA_BPM_PaymentCollectionModule_Views_SlipPrintingView_Fp410Receipt_Receipt";
            rc_dataSource.Value = _toPrintData;

            ReportViewer rdlcViewer = new ReportViewer();
            rdlcViewer.LocalReport.ReportEmbeddedResource = "PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.Fp410Receipt.Receipt.rdlc";
            rdlcViewer.LocalReport.DataSources.Add(rc_dataSource);

            OutputToPrinter(rdlcViewer);
        }


        private void OutputToPrinter(ReportViewer viewer)
        {
            //LocalSettingHelper hp = LocalSettingHelper.Instance();
            ////bool isCutOff = (hp.Read(LocalSettingNames.IsGraphicMode).ToString() == "TRUE" && hp.Read(LocalSettingNames.IsCutOff).ToString() == "TRUE" ? true : false);
            //int baudRate = (hp.Read(LocalSettingNames.SlipPOSBaudRate).Equals("")) ? 0 : Convert.ToInt32(hp.Read(LocalSettingNames.SlipPOSBaudRate).ToString());
            //PrintUtility _printer = new PrintUtility(hp.Read(LocalSettingNames.SlipPOSPrinterName).ToString(), 300, 1000, baudRate);
            //_printer.Export(viewer.LocalReport);
            //_printer.CurrentPageIndex = 0;
            //_printer.Print();
        }
    }
}