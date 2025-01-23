using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CallingBillInfo : IComparable<CallingBillInfo>
    {
        private int _sequence;
        private string _peaCode;
        private string _lineId;
        private string _billNo;
        private string _billPeriod;
        private string _paymentMethod;
        private decimal? _amount = 0;
        private string _billStatus;
        private string _caId;
        private string _customerName;
        private string _note;
        private int? _allowRemove = 0;  // 1= yes, 0 = no
        private string _transferCode;
        private string _noticeFlag;
        private string _paymentSlip;
        private string _billPrintId;
        private string _floodId;

        public CallingBillInfo Clone()
        {
            CallingBillInfo that = new CallingBillInfo();
            that.Sequence = this.Sequence;
            that.PeaCode = this.PeaCode;
            //reformat
            that.BillPeriod = string.Format("{0}{1}", this.BillPeriod.Substring(3, 4), this.BillPeriod.Substring(0, 2));
            that.LineId = this.LineId;
            that.BillNo = this.BillNo;
            that.InvoiceNo = this.InvoiceNo;
            that.PaymentType = this.PaymentType;
            that.Amount = this.Amount;
            that.BillStatus = this.BillStatus;
            that.CaId = this.CaId;
            that.CustomerName = this.CustomerName;
            //that.Note = this.Note;
            that.AllowRemove = this.AllowRemove;
            that.TransferCode = this.TransferCode;
            that.NoticeFlag = this.NoticeFlag;
            return that;
        }


        [DataMember(Order=1)]
        public string BillPeriod
        {
            get { return string.Format("{0}/{1}", _billPeriod.Substring(4,2), _billPeriod.Substring(0, 4)); }
            set { _billPeriod = value; }
        }


        [DataMember(Order=2)]
        public string BillPeriodOrg
        {
            get { return _billPeriod; }
            set { _billPeriod = value; }
        }


        [DataMember(Order=3)]
        public int? AllowRemove
        {
            get { return _allowRemove; }
            set { _allowRemove = value; }
        }


        [DataMember(Order=4)]
        public int Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }


        [DataMember(Order=5)]
        public string PeaCode
        {
            get { return _peaCode; }
            set { _peaCode = value; }
        }


        [DataMember(Order=6)]
        public string LineId
        {
            get { return _lineId; }
            set { _lineId = value; }
        }


        [DataMember(Order=7)]
        public string BillNo
        {
            get { return _billNo; }
            set { _billNo = value; }
        }


        [DataMember(Order=8)]
        public string PaymentType
        {
            get { return _paymentMethod; }
            set { _paymentMethod = value; }
        }


        [DataMember(Order=9)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }


        [DataMember(Order=10)]
        public string BillStatus
        {
            get { return _billStatus; }
            set { _billStatus = value; }
        }


        [DataMember(Order=11)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=12)]
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        //public string Note
        //{
        //    get { return _note; }
        //    set { _note = value; }
        //}


        [DataMember(Order=13)]
        public string TransferCode
        {
            get { return _transferCode; }
            set { _transferCode = value; }
        }


        [DataMember(Order=14)]
        public string NoticeFlag
        {
            get { return _noticeFlag; }
            set { _noticeFlag = value; }
        }


        [DataMember(Order=15)]
        public string InvoiceNo
        {
            get { return _paymentSlip; }
            set { _paymentSlip = value; }
        }


        [DataMember(Order=16)]
        public string BillBookId
        {
            get { return _billPrintId; }
            set { _billPrintId = value; }
        }


        [DataMember(Order=17)]
        public string FloodingId
        {
            get { return _floodId; }
            set { _floodId = value; }
        }


        #region IComparable<CallingBillInfo> Members

        public int CompareTo(CallingBillInfo other)
        {
            return InvoiceNo.CompareTo(other.InvoiceNo);
        }

        #endregion
    }
}
