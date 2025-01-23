using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Globalization;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class PaymentMethodInfo
    {
        private string _flowId;

        [DataMember(Order=1)]
        public string FlowId
        {
            get { return _flowId; }
            set { _flowId = value; }
        }

        private string _ptId;

        [DataMember(Order=2)]
        public string PtId
        {
            get { return _ptId; }
            set { _ptId = value; }
        }

        private string _ptName;

        [DataMember(Order=3)]
        public string PtName
        {
            get { return _ptName; }
            set { _ptName = value; }
        }
        
        //Checked TongKung
        public string Description
        {
            get
            {
                switch (_ptId)
                {
                    case "1":
                        return FlowDesc + " เลขที่อ้างอิง: " + Comment;
                    case "2":
                        return string.Format("{0} เลขที่อ้างอิง: {1}, {2} เลขที่เช็ค:{3} เลขที่บัญชีเช็ค:{4} วันที่เช็ค:{5}",
                            _flowDesc,
                            _comment,
                            _bank == null ? "" : _bank.BankName,
                            _chqNo == null ? "" : _chqNo,
                            _chqAccNo == null ? "" : _chqAccNo,
                            _chqDt == null? "": _chqDt.Value.ToString("dd/MM/yyyy"), new CultureInfo("th-TH"));
                    default:
                        return "";
                }
            }
        }

        private string _flowDesc;

        [DataMember(Order=5)]
        public string FlowDesc
        {
            get { return _flowDesc; }
            set { _flowDesc = value; }
        }

        private string _comment;

        [DataMember(Order=6)]
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        private Bank _bank;

        [DataMember(Order=7)]
        public Bank Bank
        {
            get { return _bank; }
            set { _bank = value; }
        }

        private string _bankId;

        [DataMember(Order=8)]
        public string BankId
        {
            set { _bankId = value; }
            get
            {
                return null == _bank ? null : _bank.BankKey;
            }
        }

        private string _bankName;

        [DataMember(Order=9)]
        public string BankName
        {
            set { _bankName = value; }
            get
            {
                return null == _bank ? null : _bank.BankName;
            }
        }

        private string _chqItemId;//use for SystemInitial

        [DataMember(Order=10)]
        public string ChqItemId
        {
            get { return _chqItemId; }
            set { _chqItemId = value; }
        }

        private string _chqNo;

        [DataMember(Order=11)]
        public string ChqNo
        {
            get { return _chqNo; }
            set { _chqNo = value; }
        }

        private string _chqAccNo;

        [DataMember(Order=12)]
        public string ChqAccNo
        {
            get { return _chqAccNo; }
            set { _chqAccNo = value; }
        }

        private DateTime? _chqDt;

        [DataMember(Order=13)]
        public DateTime? ChqDt
        {
            get { return _chqDt; }
            set { _chqDt = value; }
        }

        private string _clearingAccNo;

        [DataMember (Order=14)]
        public string ClearingAccNo
        {
            get { return _clearingAccNo; }
            set { _clearingAccNo = value;}
        }

        private string _draftFlag;

        [DataMember(Order=15)]
        public string DraftFlag
        {
            get { return _draftFlag; }
            set { _draftFlag = value; }
        }

        private string _cashierChequeFlag;

        [DataMember(Order=16)]
        public string CashierChequeFlag
        {
            get { return _cashierChequeFlag; }
            set { _cashierChequeFlag = value; }
        }

        private string _arptId;

        [DataMember(Order=17)]
        public string ARPtId
        {
            get { return _arptId; }
            set { _arptId = value; }
        }

        decimal? _amount;

        [DataMember(Order=18)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        decimal? _adjAmount;

        [DataMember(Order=19)]
        public decimal? AdjAmount
        {
            get { return _adjAmount; }
            set { _adjAmount = value; }
        }

        private decimal? _totalAmt;

        [DataMember(Order=20)]
        public decimal? TotalAmt
        {
            get { return _totalAmt; }
            set { _totalAmt = value; }
        }

        private string _cahierId;

        [DataMember(Order=21)]
        public string CahierId
        {
            get { return _cahierId; }
            set { _cahierId = value; }
        }

        private string _branchId;

        [DataMember(Order=22)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private string _workId;

        [DataMember(Order=23)]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }

        private string _flowType;

        [DataMember(Order=24)]
        public string FlowType
        {
            get { return _flowType; }
            set { _flowType = value; }
        }

        private string _postedServerId;

        [DataMember(Order=25)]
        public string PostedServerId
        {
            get { return _postedServerId; }
            set { _postedServerId = value; }
        }
   
    }
}
