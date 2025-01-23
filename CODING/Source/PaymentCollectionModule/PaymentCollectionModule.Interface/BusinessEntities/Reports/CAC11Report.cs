using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC11Report
    {
        private string _branchId;
        private DateTime? _paymentDt;
        private DateTime? _cancelDt;
        private string _controllerId;
        private string _receiptId;
        private string _collectorName;
        private string _posId;
        private string _returnCollectorName;
        private string _returnPosId;
        private string _caId;
        private string _caName;
        private string _period;
        private string _dtName;
        private decimal? _amount;
        private decimal? _vatAmount;
        private decimal? _gAmount;
        private string _cancelReason;
        private string _ptName;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        //checked A
        //[DataMember(Order=3)]
        public string PaymentDate
        {
            get { return _paymentDt.Value.ToString("dd/MM/yy", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=4)]
        public DateTime? CancelDt
        {
            get { return _cancelDt; }
            set { _cancelDt = value; }
        }


        [DataMember(Order=5)]
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }


        [DataMember(Order=6)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }


        [DataMember(Order=7)]
        public string CollectorName
        {
            get { return _collectorName; }
            set { _collectorName = value; }
        }


        [DataMember(Order=8)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }


        [DataMember(Order=9)]
        public string ReturnCollectorName
        {
            get { return _returnCollectorName; }
            set { _returnCollectorName = value; }
        }


        [DataMember(Order=10)]
        public string ReturnPosId
        {
            get { return _returnPosId; }
            set { _returnPosId = value; }
        }


        [DataMember(Order=11)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=12)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }


        [DataMember(Order=13)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }


        [DataMember(Order=14)]
        public string DtName
        {
            get { return _dtName; }
            set { _dtName = value; }
        }


        [DataMember(Order=15)]
        public decimal? Amount 
        {
            get { return _amount; }
            set { _amount = value; }
        }


        [DataMember(Order=16)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }


        [DataMember(Order=17)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }


        [DataMember(Order=18)]
        public string CancelReason
        {
            get { return _cancelReason; }
            set { _cancelReason = value; }
        }

        [DataMember(Order = 19)]
        public string PtName
        {
            get { return _ptName; }
            set { _ptName = value; }
        }

    }
}
