using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class PaidInvoice
    {
        [DataContract]
        public class Debt
        {
            private string _id;

            [DataMember(Order = 1)]
            public string Id
            {
                get { return _id; }
                set { _id = value; }
            }

            private string _name;

            [DataMember(Order = 2)]
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private decimal? _amount;

            [DataMember(Order = 3)]     
            public decimal? Amount
            {
                get { return _amount; }
                set { _amount = value; }
            }

            public Debt() { }

            public Debt(string id, string name, decimal? amount)
            {
                this._id = id;
                this._name = name;
                this._amount = amount;
            }
        }

        private string _receiptId;

        [DataMember(Order = 4)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        private string _realReceiptId;

        [DataMember(Order = 5)]
        public string RealReceiptId
        {
            get { return _realReceiptId; }
            set { _realReceiptId = value; }
        }

        private string _realInvoiceNo;
        private string _invoiceNo;
        private string _period;
        private string _rateTypeId;
        private DateTime? _cancelDate;
        private decimal? _paidGAmount;
        private DateTime? _paidDate;
        private string _posId;
        private string _cashierId;
        private string _pmName;

        private List<Debt> _debtTypes = new List<Debt>();

        [DataMember(Order = 6)]
        public List<Debt> DebtTypes
        {
            get { return _debtTypes; }
            set { _debtTypes = value; }
        }

        [DataMember(Order = 7)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        //Checked TongKung
        //[DataMember(Order = 8)]
        public string DisplayInvoiceNo
        {
            get
            {
                if (_invoiceNo.Length == 22)
                {
                    return string.Empty;
                }
                else
                {
                    return _invoiceNo.TrimStart('0');
                }
            }
        }

        //Checked TongKung
        //[DataMember(Order = 9)]
        public string DebtType
        {
            get
            {
                if (_debtTypes.Count == 1)
                {
                    return _debtTypes[0].Name;
                }
                else if (_debtTypes.Count > 1)
                {
                    return "++";
                }
                else
                {
                    return "-";
                }
            }
        }


        [DataMember(Order = 10)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }

        //Checked TongKung
        //[DataMember(Order = 11)]
        public string PeriodString
        {
            get
            {
                if (_debtTypes[0].Id == "M00800010")
                {
                    return string.Format("ß«¥∑’Ë {0}/{1}", _installmentPeriod, _installmentTotalPeriod);
                }
                else
                {
                    return (_period == null) ? "" : StringConvert.FormatPeriod(_period);
                }
            }
        }

        private int? _installmentPeriod;

        [DataMember(Order = 12)]
        public int? InstallmentPeriod
        {
            get { return _installmentPeriod; }
            set { _installmentPeriod = value; }
        }

        private int? _installmentTotalPeriod;

        [DataMember(Order = 13)]
        public int? InstallmentTotalPeriod
        {
            get { return _installmentTotalPeriod; }
            set { _installmentTotalPeriod = value; }
        }


        [DataMember(Order = 14)]
        public string RateTypeId
        {
            get { return _rateTypeId; }
            set { _rateTypeId = value; }
        }


        [DataMember(Order = 15)]
        public DateTime? CancelDate
        {
            get { return _cancelDate; }
            set { _cancelDate = value; }
        }


        [DataMember(Order = 16)]
        public decimal? PaidGAmount
        {
            get { return _paidGAmount; }
            set { _paidGAmount = value; }
        }


        [DataMember(Order = 17)]
        public DateTime? PaidDate
        {
            get { return _paidDate; }
            set { _paidDate = value; }
        }


        [DataMember(Order = 18)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }


        [DataMember(Order = 19)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }

        //Checked TongKung
        //[DataMember(Order = 20)]
        public string DisplayCashierId
        {
            get { return (_cashierId == null) ? "" : _cashierId.TrimStart('0'); }
        }


        [DataMember(Order = 21)]
        public string PmName
        {
            get { return _pmName; }
            set { _pmName = value; }
        }

        [DataMember(Order = 22)]
        public string RealInvoiceNo
        {
            get { return _realInvoiceNo; }
            set { _realInvoiceNo = value; }
        }

    }
}
