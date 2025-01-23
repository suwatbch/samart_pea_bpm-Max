using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ChequeInfo
    {
        private string _bankKey;
        private string _bankName;
        private string _chqNo;
        private string _chqAccNo;
        private DateTime? _chqDt;
        private decimal? _amount=0;
        private string _transStatus;

        public ChequeInfo Clone()
        {
            ChequeInfo that = new ChequeInfo();
            that.BankKey = this.BankKey;
            that.BankName = this.BankName;
            that.ChqNo = this.ChqNo;
            that.ChqAccNo = this.ChqAccNo;
            that.ChqDt = this.ChqDt;
            that.Amount = this.Amount;
            that.TransStatus = this.TransStatus;
            return that;
        }

        //report only
        private int? _reportType;
        private decimal? _cashAmt;


        [DataMember(Order=1)]
        public string BankKey
        {
            set { _bankKey = value; }
            get { return _bankKey; }
        }


        [DataMember(Order=2)]
        public string BankName
        {
            set { _bankName = value; }
            get { return _bankName; }
        }


        [DataMember(Order=3)]
        public string ChqNo
        {
            set { _chqNo = value; }
            get { return _chqNo; }
        }


        [DataMember(Order=4)]
        public string ChqAccNo
        {
            set { _chqAccNo = value; }
            get { return _chqAccNo; }
        }


        [DataMember(Order=5)]
        public DateTime? ChqDt
        {
            set { _chqDt = value; }
            get { return _chqDt; }
        }


        [DataMember(Order=6)]
        public decimal? Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }


        [DataMember(Order=7)]
        public string TransStatus
        {
            set { _transStatus = value; }
            get { return _transStatus; }
        }

        //Checked TongKung
        public string TransStatusTxt
        {
            get {
                if (_transStatus == "0")
                    return "w";
                else if (_transStatus == "1")
                    return "!";
                else
                    return "";
            }
        }


        //Checked TongKung
        public string ChqDate
        {
            get {
                if (_chqDt != null)
                    return _chqDt.Value.ToString("dd/MM/yy", new CultureInfo("th-TH"));
                else
                    return "";
            }
        }

        //for report

        [DataMember(Order=10)]
        public int? ReportType
        {
            set { _reportType = value; }
            get { return _reportType; }
        }


        [DataMember(Order=11)]
        public decimal? CashAmt
        {
            set { _cashAmt = value; }
            get { return _cashAmt; }
        }

        public string ChqDateTxt
        {
            get
            {
                if (_chqDt != null)
                    return _chqDt.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                else
                    return "";
            }
        }

    }
}
