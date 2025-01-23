using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract, Serializable]
    public class CAC15Report
    {
        private string _caId;
        private string _caName;
        private string _debtName;
        private string _receiptId;
        private string _realReceiptId;
        private decimal? _amount;
        private decimal? _adjAmount;
        private decimal? _gAmount;
        private decimal? _paidCashAmount;
        private decimal? _paidChqAmount;
        private decimal? _paidDepositAmount;
        private DateTime? _paymentDt;
        private DateTime? _extReceiptDt;
        private string _cashierId;
        private string _cashierName;
        private string _branchPosID;
        private string _paymentActive;
        private string _cancelActive;
        private string _repayActive;
        private string _validateFlag;
        private string _active;
        private string _offlineStatus;
        private string _installmentReceiptId;
        private decimal? _paidQRAmount;

        [DataMember(Order=1)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=2)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }


        [DataMember(Order=3)]
        public string DebtName
        {
            get { return _debtName; }
            set { _debtName = value; }
        }


        [DataMember(Order=4)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }


        [DataMember(Order=5)]
        public string RealReceiptId
        {
            get { return _realReceiptId; }
            set { _realReceiptId = value; }
        }


        [DataMember(Order=6)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }


        [DataMember(Order=7)]
        public decimal? AdjAmount
        {
            get { return _adjAmount; }
            set { _adjAmount = value; }
        }


        [DataMember(Order=8)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }


        [DataMember(Order=9)]
        public decimal? PaidCashAmount
        {
            get { return _paidCashAmount; }
            set { _paidCashAmount = value; }
        }


        [DataMember(Order=10)]
        public decimal? PaidChqAmount
        {
            get { return _paidChqAmount; }
            set { _paidChqAmount = value; }
        }


        [DataMember(Order=11)]
        public decimal? PaidDepositAmount
        {
            get { return _paidDepositAmount; }
            set { _paidDepositAmount = value; }
        }


        [DataMember(Order=12)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        //Checked A
        //[DataMember(Order=13)]
        public string PaymentDate
        {
            get { return (_paymentDt == null) ? null : _paymentDt.Value.ToString("dd/MM/yyyy  HH:mm", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=14)]
        public DateTime? ExtReceiptDt
        {
            get { return _extReceiptDt; }
            set { _extReceiptDt = value; }
        }

        //Checked A
        //[DataMember(Order=15)]
        public string ExtReceiptDate
        {
            get { return (_extReceiptDt == null) ? null : _extReceiptDt.Value.ToString("dd/MM/yyyy  HH:mm", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=16)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }


        [DataMember(Order=17)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }


        [DataMember(Order=18)]
        public string BranchPosID
        {
            get { return _branchPosID; }
            set { _branchPosID = value; }
        }


        [DataMember(Order=19)]
        public string PaymentActive
        {
            get { return _paymentActive; }
            set { _paymentActive = value; }
        }


        [DataMember(Order=20)]
        public string CancelActive
        {
            get { return _cancelActive; }
            set { _cancelActive = value; }
        }


        [DataMember(Order=21)]
        public string RepayActive
        {
            get { return _repayActive; }
            set { _repayActive = value; }
        }


        [DataMember(Order=22)]
        public string ValidateFlag
        {
            get { return _validateFlag; }
            set { _validateFlag = value; }
        }


        [DataMember(Order=23)]
        public string Active
        {
            get { return _active; }
            set { _active = value; }
        }

        [DataMember(Order = 24)]
        public string OfflineStatus
        {
            get { return _offlineStatus; }
            set { _offlineStatus = value; }
        }

        //Installment receipt id
        [DataMember(Order = 25)]
        public string InstallmentReceiptId
        {
            get { return _installmentReceiptId; }
            set { _installmentReceiptId = value; }
        }

        //Paid QR Amount 
        [DataMember(Order=26)]
        public decimal? PaidQRAmount {
            get { return _paidQRAmount; }
            set { _paidQRAmount = value; }
        }

    }
}
