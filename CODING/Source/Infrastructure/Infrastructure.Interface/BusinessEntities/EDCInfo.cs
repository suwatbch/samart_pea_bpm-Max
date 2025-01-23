using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    public class EDCInfo
    {
        public class ResponseMessageModel
        {
            public object ResponseMessage;
            public object InvoiceNo;
            public string TransactionType4Byte  { get; set; }
            public string DataLen3Byte          { get; set; }
            public string ReponseCode2Byte      { get; set; }
            public string SystemTrace6Byte      { get; set; }
            public string BatchNumber6Byte      { get; set; }
            public string TraceInvoice6Byte     { get; set; }
            public string ApproveCode6Byte      { get; set; }
            public string ReferenceNumber12Byte { get; set; }
            public string CardNumber20Byte      { get; set; }
            public string CardExpireDateYYMM4Byte { get; set; }
            public string CardLabel10Byte       { get; set; }
            public string ReponseText10Byte     { get; set; }
            public string TxnDateYYMMDD6Byte    { get; set; }
            public string TxnTimeHHMMSS6Byte    { get; set; }
            public string Tid8Byte              { get; set; }
            public string Mid15Byte             { get; set; }
            public string Nli4Byte              { get; set; }
            public string SlipHeadingLineA23Byte { get; set; }
            public string SlipHeadingLineB23Byte { get; set; }
            public string SlipHeadingLineC23Byte { get; set; }
            public string CardHolderName22Byte   { get; set; }

        }

        public class EdcCustomerDetail
        {
            public string       CaId;
            public string       PersonalCardId;
            public string       CaFullName;
            public string       InvoiceNo;
            public bool         OpenCardTab;
            public bool         OpenNonCardTab;
            public bool         AddCardToGrid;
            public bool         AddNonCardToGrid;
            public int          DataRowIndex;
            public bool         ConfirmManualAddInvoice { get; set; }
            public List<string> ManualAddInvoiceList { get; set; }
        }
    }

    public static class EdcSetting
    {
        public static SerialPort EDCComport;
        public static string InputData;
        public static string AppMode;
        public readonly static string CreditMode    = "CARD";
        public readonly static string QRMode        = "NONCARD";
        public readonly static string VoidMode      = "VOID";
    }
}
