using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportDailyPayInInfo
    {
        private int? _groupCount;
        private string _bankKey;
        private string _bankName;
        private string _accNo;
        private string _clearingAccNo;
        private string _itemName;
        private string _cashier;
        private DateTime? _procDt;
        private decimal? _amount;
        private decimal? _payInAmount;
        private string _branchId;
        private string _payInDate;
        private DateTime _payInDt;


        [DataMember(Order=1)]
        public int? GroupCount
        {
            set { _groupCount = value; }
            get { return _groupCount; }
        }


        [DataMember(Order=2)]
        public DateTime PayInDt
        {
            set { _payInDt = value; }
            get { return _payInDt; }
        }

        //Checked TongKung
        public string PayInDate
        {
            get { return _payInDt.ToString("dd/MM/yyyy", new CultureInfo("th-TH")); }
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
        public string AccNo
        {
            set { _accNo = value; }
            get { return _accNo; }
        }


        [DataMember(Order=7)]
        public string ClearingAccNo
        {
            set { _clearingAccNo = value; }
            get { return _clearingAccNo; }
        }


        [DataMember(Order=8)]
        public string ItemName
        {
            set { _itemName = value; }
            get { return _itemName; }
        }


        [DataMember(Order=9)]
        public string Cashier
        {
            set { _cashier = value; }
            get { return _cashier; }
        }


        [DataMember(Order=10)]
        public DateTime? ProcDt
        {
            set { _procDt = value; }
            get { return _procDt; }
        }

        //Checked TongKung
        public string ProcDate
        {
            get { return _procDt.Value.ToString("HH:mm", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=12)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }


        [DataMember(Order=13)]
        public decimal? PayInAmount
        {
            get { return _payInAmount; }
            set { _payInAmount = value; }
        }


        [DataMember(Order=14)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }

    }
}
