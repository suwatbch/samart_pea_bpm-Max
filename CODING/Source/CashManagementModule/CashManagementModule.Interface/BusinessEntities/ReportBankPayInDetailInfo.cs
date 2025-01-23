using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportBankPayInDetailInfo
    {
        private List<ChequeInfo> _chqList = new List<ChequeInfo>() ;
        private decimal? _cashAmt;
        private string _glBankKey;
        private string _glBankName;
        private string _glBankAcc;
        private string _clearingAccNo;  // บัญชีแยกประเภท
        private DateTime? _paymentDt;

        //filter
        private List<ChequeInfo> _chqSameBranch = new List<ChequeInfo>();
        private List<ChequeInfo> _chqSameBank = new List<ChequeInfo>();
        private List<ChequeInfo> _chqOtherBank = new List<ChequeInfo>();


        [DataMember(Order=1)]
        public List<ChequeInfo> ChqSameBranch
        {
            set { _chqSameBranch = value; }
            get { return _chqSameBranch; }
        }


        [DataMember(Order=2)]
        public List<ChequeInfo> ChqSameBank
        {
            set { _chqSameBank = value; }
            get { return _chqSameBank; }
        }


        [DataMember(Order=3)]
        public List<ChequeInfo> ChqOtherBank
        {
            set { _chqOtherBank = value; }
            get { return _chqOtherBank; }
        }


        [DataMember(Order=4)]
        public List<ChequeInfo> ChqList
        {
            set { _chqList = value; }
            get { return _chqList; }
        }


        [DataMember(Order=5)]
        public decimal? CashAmt
        {
            set { _cashAmt = value; }
            get { return _cashAmt; }
        }


        [DataMember(Order=6)]
        public string GLBankKey
        {
            set { _glBankKey = value; }
            get { return _glBankKey; }
        }


        [DataMember(Order=7)]
        public string GLBankName
        {
            set { _glBankName = value; }
            get { return _glBankName; }
        }


        [DataMember(Order=8)]
        public string GLBankAcc
        {
            set { _glBankAcc = value; }
            get { return _glBankAcc; }
        }



        [DataMember(Order=9)]
        public string ClearingAccNo
        {
            set { _clearingAccNo = value; }
            get { return _clearingAccNo; }
        }


        [DataMember(Order=10)]
        public DateTime? PaymentDt
        {
            set { _paymentDt = value; }
            get { return _paymentDt; }
        }

        //Checked TongKung
        //[DataMember(Order=11)]
        public string PaymentDate
        {
            get { return _paymentDt.Value.ToString("dd/MM/yyyy H:mm", new CultureInfo("th-TH")); }
        }
    }
}
