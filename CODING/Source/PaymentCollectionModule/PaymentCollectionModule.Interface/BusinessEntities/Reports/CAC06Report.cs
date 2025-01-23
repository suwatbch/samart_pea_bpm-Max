using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC06Report
    {
        private string _paidBranchId;
        private string _paidBranchName;
        private string _branchDetail;
        private string _caId;
        private string _posId;
        private string _caName;
        private string _period;
        private string _receiptId;
        private DateTime? _paymentDt;
        private decimal? _baseAmount;
        private decimal? _ftAmount;
        private decimal? _vatAmount;
        private decimal? _gAmount;
        private string _controllerName;
        private string _dtId;
        private string _caDoc;
        private string _controllerAccName;



        [DataMember(Order=1)]
        public string PaidBranchId
        {
            get { return _paidBranchId; }
            set { _paidBranchId = value; }
        }


        [DataMember(Order=2)]
        public string PaidBranchName
        {
            get { return _paidBranchName; }
            set { _paidBranchName = value; }
        }


        [DataMember(Order=3)]
        public string BranchDetail
        {
            get { return _branchDetail; }
            set { _branchDetail = value; }
        }


        [DataMember(Order=4)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=5)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }


        [DataMember(Order=6)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }


        [DataMember(Order=7)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }


        [DataMember(Order=8)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }


        [DataMember(Order=9)]
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        //checked A
        //[DataMember(Order=10)]
        public string PaymentDate
        {
            get { return _paymentDt.Value.ToString("dd/MM/yyyy  HH:mm", new CultureInfo("th-TH")); }
        }


        [DataMember(Order=11)]
        public decimal? BaseAmount
        {
            get { return _baseAmount; }
            set { _baseAmount = value; }
        }


        [DataMember(Order=12)]
        public decimal? FtAmount
        {
            get { return _ftAmount; }
            set { _ftAmount = value; }
        }


        [DataMember(Order=13)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }


        [DataMember(Order=14)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }


        [DataMember(Order=15)]
        public string ControllerName
        {
            get { return _controllerName; }
            set { _controllerName = value; }
        }


        [DataMember(Order=16)]
        public string DtId
        {
            get { return _dtId; }
            set { _dtId = value; }
        }


        [DataMember(Order=17)]
        public string CaDoc
        {
            get { return _caDoc; }
            set { _caDoc = value; }
        }


        [DataMember(Order=18)]
        public string ControllerAccName
        {
            get { return _controllerAccName; }
            set { _controllerAccName = value; }
        }

    }
}
