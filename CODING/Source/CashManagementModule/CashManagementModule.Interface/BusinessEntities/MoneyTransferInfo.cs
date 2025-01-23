using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class MoneyTransferInfo
    {
        private string _workId;
        private string _commander;
        private string _sender;
        private string _senderName;
        private string _receiver;
        private string _receiverName;
        private string _reqPosId;
        private string _reqTerminalCode;
        private string _branchId;
        private bool _tobank;
        private decimal? _cashAmount;
        private BindingList<ChequeInfo> _chequeList = new BindingList<ChequeInfo>();
        private string _flowType;
        private bool _previewReport;
        private string _modifiedBy;
        private string _postedBranchId;
        private bool _sepChq;
        private bool _isForceTrans;

        //tobank
        private string _GLBankKey;
        private string _GLBankAcc;
        private string _BankName;
        private string _ClearingAccNo;


        [DataMember(Order=1)]
        public string WorkId
        {
            set { _workId = value; }
            get { return _workId; }
        }

        [DataMember(Order=2)]
        public string GLBankKey
        {
            set { _GLBankKey = value; }
            get { return _GLBankKey; }
        }

        [DataMember(Order=3)]
        public string BankName
        {
            set { _BankName = value; }
            get { return _BankName; }
        }

        [DataMember(Order=4)]
        public string ClearingAccno
        {
            set { _ClearingAccNo = value; }
            get { return _ClearingAccNo; }
        }

        [DataMember(Order=5)]
        public string GLBankAcc
        {
            set { _GLBankAcc = value; }
            get { return _GLBankAcc; }
        }

        [DataMember(Order=6)]
        public string FlowType
        {
            set { _flowType = value; }
            get { return _flowType; }
        }

        [DataMember(Order=7)]
        public string Commander
        {
            set { _commander = value; }
            get { return _commander; }
        }

        [DataMember(Order=8)]
        public string Sender
        {
            set { _sender = value; }
            get { return _sender; }
        }

        [DataMember(Order=9)]
        public string SenderName
        {
            set { _senderName = value; }
            get { return _senderName; }
        }

        [DataMember(Order=10)]
        public string Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }

        [DataMember(Order=11)]
        public string ReceiverName
        {
            set { _receiverName = value; }
            get { return _receiverName; }
        }

        [DataMember(Order=12)]
        public string ReqPosId
        {
            set { _reqPosId = value; }
            get { return _reqPosId; }
        }

        [DataMember(Order=13)]
        public string ReqTerminalCode
        {
            set { _reqTerminalCode = value; }
            get { return _reqTerminalCode; }
        }

        [DataMember(Order=14)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }

        [DataMember(Order=15)]
        public bool ToBank
        {
            set { _tobank = value; }
            get { return _tobank; }
        }

        [DataMember(Order=16)]
        public bool IsForceTrans
        {
            set { _isForceTrans = value; }
            get { return _isForceTrans; }
        }

        //[DataMember(Order = 14)]
        //Checked TongKung
        public decimal? ChequeAmount
        {
            get
            {
                decimal? chqAmt =0;
                foreach (ChequeInfo chq in _chequeList)
                    chqAmt += chq.Amount.Value;
                return chqAmt;
            }
        }

        [DataMember(Order=17)]
        public decimal? CashAmount
        {
            set { _cashAmount = value; }
            get { return _cashAmount; }
        }

        [DataMember(Order=18)]
        public BindingList<ChequeInfo> ChequeList
        {
            set { _chequeList = value; }
            get { return _chequeList; }
        }

        [DataMember(Order=19)]
        public bool PreviewReport
        {
            set { _previewReport = value; }
            get { return _previewReport; }
        }

        [DataMember(Order=20)]
        public bool SepChq
        {
            set { _sepChq = value; }
            get { return _sepChq; }
        }

        [DataMember(Order=21)]
        public string ModifiedBy
        {
            set { _modifiedBy = value; }
            get { return _modifiedBy; }
        }

        [DataMember(Order=22)]
        public string PostedBranchId
        {
            set { _postedBranchId = value; }
            get { return _postedBranchId; }
        }
    }
}
