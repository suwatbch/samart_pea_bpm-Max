using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Services;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class GroupInvoicingReportParam
    {
        string _groupInvoiceNo;

        [DataMember(Order=1)]
        public string GroupInvoiceNo
        {
            get { return _groupInvoiceNo; }
            set { _groupInvoiceNo = value; }
        }

        string _receiptId;

        [DataMember(Order=2)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        IReportService _reportService;

        [DataMember(Order=3)]
        public IReportService ReportService
        {
            get { return _reportService; }
            set { _reportService = value; }
        }
    }
}
