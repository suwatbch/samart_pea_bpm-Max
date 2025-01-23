using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.Constants
{
    public class LocalSettingNames
    {
        public const string BranchId = "BranchId";
        public const string BranchName = "BranchName";


        public const string PrinterName = "PrinterName";
        public const string PrinterChoice = "PrinterChoice";
        public const string Barcode1Start = "Barcode1Start";
        public const string Barcode2Start = "Barcode2Start";
        public const string Barcode1Stop = "Barcode1Stop";
        public const string Barcode2Stop = "Barcode2Stop";
        
        #region #ISSUE NEW FORM
        public const string Barcode3Start = "Barcode3Start";
        public const string Barcode3Stop = "Barcode3Stop";
        #endregion

        public const string BarcodeA4Start = "BarcodeA4Start";
        public const string BarcodeA4Stop = "BarcodeA4Stop";

        public const string IsGraphicMode = "IsGraphicMode";
        public const string SlipPOSPrinterName = "SlipPOSPrinterName";
        public const string PrePrintedPrinterName = "PrePrintedPrinterName";
        public const string AgencyPrinterName = "AgencyPrinterName";

        public const string DL080_UPLOAD_AP_BATCH = "DL080_UPLOAD_AP_BATCH";
        public const string DL090_UPLOAD_PAYMENT_BATCH = "DL090_UPLOAD_PAYMENT_BATCH";
        public const string DL091_UPLOAD_CASH_MANAGEMENT_BATCH = "DL091_UPLOAD_CASH_MANAGEMENT_BATCH";
        public const string DL100_UPLOAD_AGENCY_BATCH = "DL100_UPLOAD_AGENCY_BATCH";
        public const string DL101_UPLOAD_EPAYMENT_BATCH = "DL101_UPLOAD_EPAYMENT_BATCH";
        public const string DL110_UPLOAD_TECHNICAL_BATCH = "DL110_UPLOAD_TECHNICAL_BATCH";
        public const string DL120_UPLOAD_BILLING_BATCH = "DL120_UPLOAD_BILLING_BATCH";
        public const string DL070_TECHNICAL_BATCH = "DL070_TECHNICAL_BATCH";
        public const string DL130_UPLOAD_AG_COMPENSATION_BATCH = "DL130_UPLOAD_AG_COMPENSATION_BATCH";

        public const string DL008_EXPORT_AG_TO_SAP_BATCH = "DL008_EXPORT_AG_TO_SAP_BATCH";
        public const string DL010_EXPORT_BILLBOOK_INVOICE_BATCH = "DL010_EXPORT_BILLBOOK_INVOICE_BATCH";

        public const string BlueBillPrinterName = "BlueBillPrinterName";
        public const string BlueBillPrinterChoice = "BlueBillPrinterChoice";
        public const string GreenBillPrinterName = "GreenBillPrinterName";
        public const string A4BillPrinterName = "A4BillPrinterName";
        public const string A4BillPrinterChoice = "A4BillPrinterChoice";
        public const string BlueBillBarcode1Start = "BlueBillBarcode1Start";
        public const string BlueBillBarcode2Start = "BlueBillBarcode2Start";
        public const string BlueBillBarcode1Stop = "BlueBillBarcode1Stop";
        public const string BlueBillBarcode2Stop = "BlueBillBarcode2Stop";

        #region #ISSUE NEW FORM
        public const string BlueBillBarcode3Start = "BlueBillBarcode3Start";       
        public const string BlueBillBarcode3Stop = "BlueBillBarcode3Stop";
        #endregion

        public const string A4BillBarcode1Start = "A4BillBarcode1Start";
        public const string A4BillBarcode2Start = "A4BillBarcode2Start";
        public const string A4BillBarcode1Stop = "A4BillBarcode1Stop";
        public const string A4BillBarcode2Stop = "A4BillBarcode2Stop";

        #region #ISSUE EDC Setup Comport        
        public const string EDCComPort      = "EDCComPort";
        public const string EDCBaudRate     = "EDCBaudRate";
        public const string EDCDataBits     = "EDCDataBits";
        public const string EDCStopBits     = "EDCStopBits";
        public const string EDCHandShake    = "EDCHandShake";
        public const string EDCParity       = "EDCParity"; 
        #endregion
    }
}
