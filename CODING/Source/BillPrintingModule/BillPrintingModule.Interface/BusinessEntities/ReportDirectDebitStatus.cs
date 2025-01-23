using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportDirectDebitStatus
    {
        private string _receiptId;
        private string _branchId;
        private string _branchName;
        private string _mruId;
        private string _caId;
        private string _caName;
        private string _period;
        private string _paymentDate;
        private DateTime? _printDt;
        private decimal? _totalAmount;
        private string _printDate;


        [DataMember(Order=1)]
        public string ReceiptId
        {
            set { _receiptId = value; }
            get { return _receiptId; }
        }


        [DataMember(Order=2)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }


        [DataMember(Order=3)]
        public string BranchName
        {
            set { _branchName = value; }
            get { return _branchName; }
        }


        [DataMember(Order=4)]
        public string MruId
        {
            set { _mruId = value; }
            get { return _mruId; }
        }


        [DataMember(Order=5)]
        public string CaId
        {
            set { _caId = value; }
            get { return _caId; }
        }


        [DataMember(Order=6)]
        public string CaName
        {
            set { _caName = value; }
            get { return _caName; }
        }


        [DataMember(Order=7)]
        public string Period
        {
            set { _period = value; }
            get { return _period; }
        }


        [DataMember(Order=8)]
        public string PaymentDate
        {
            set { _paymentDate = value; }
            get { return _paymentDate; }
        }


        [DataMember(Order=9)]
        public DateTime? PrintDt
        {
            set
            {
                _printDt = value;
                if (_printDt != null)
                _printDate = _printDt.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                else
                    _printDate = string.Empty;
            }
            get { return _printDt; }
        }


        [DataMember(Order=10)]
        public string PrintDate
        {
            set { _printDate = value; }
            get { return _printDate; }
        }


        [DataMember(Order=11)]
        public decimal? TotalAmount
        {
            set { _totalAmount = value; }
            get { return _totalAmount; }
        }

    }
}
