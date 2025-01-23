using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportAvailableInfo
    {
        private string _cashierId;
        private string _cashierName;
        private string _posId;
        private DateTime? _itemDt;
        private int _type;
        private string _itemId;
        private decimal? _chequeAmt;
        private decimal? _cashAmt;
        private string _bankKey;
        private string _bankName;
        private string _bankAccNo;
        private string _closeWorkBy;
        private int? _dayCount;


        [DataMember(Order=1)]
        public int? DayCount
        {
            set { _dayCount = value; }
            get { return _dayCount; }
        }

        //Checked TongKung
        public string Bank
        {
            get { return string.Format("({0}) {1}", _bankKey, _bankName); }
        }


        [DataMember(Order=3)]
        public string CloseWorkBy
        {
            set { _closeWorkBy = value; }
            get { return _closeWorkBy; }
        }


        [DataMember(Order=4)]
        public string BankKey
        {
            set { _bankKey = value; }
            get { return _bankKey; }
        }


        [DataMember(Order=5)]
        public string BankName
        {
            set { _bankName = value; }
            get { return _bankName; }
        }


        [DataMember(Order=6)]
        public string BankAccNo
        {
            set { _bankAccNo = value; }
            get { return _bankAccNo; }
        }


        [DataMember(Order=7)]
        public string CashierId
        {
            set { _cashierId = value; }
            get { return _cashierId; }
        }


        [DataMember(Order=8)]
        public string CashierName
        {
            set { _cashierName = value; }
            get { return _cashierName; }
        }


        [DataMember(Order=9)]
        public string PosId
        {
            set { _posId = value; }
            get { return _posId; }
        }


        [DataMember(Order=10)]
        public DateTime? ItemDt
        {
            set { _itemDt = value; }
            get { return _itemDt; }
        }

        //Checked TongKung
        public string ItemDate
        {
            get { return _itemDt.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")); }
        }

        //Checked TongKung
        public string ItemTime
        {
            get { return _itemDt.Value.ToString("HH:mm", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=12)]
        public decimal? ChequeAmt
        {
            set { _chequeAmt = value; }
            get { return _chequeAmt; }
        }


        [DataMember(Order=13)]
        public decimal? CashAmt
        {
            set { _cashAmt = value; }
            get { return _cashAmt; }
        }


        [DataMember(Order=14)]
        public string ItemId
        {
            set { _itemId = value; }
            get { return _itemId; }
        }


        [DataMember(Order=15)]
        public int ReportType
        {
            set { _type = value; }
            get { return _type; }
        }

        //Checked TongKung
        //[DataMember(Order=17)]
        public decimal? TotalAmt
        {
            get { return _cashAmt+ _chequeAmt; }
        }
    }
}
