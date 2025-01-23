using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC01Report
    {
        private string _paidBranchId;
        private string _paidBranchName;
        private string _controllerId;
        private string _caId;
        private int? _quantity;
        private string _period;
        private decimal? _baseAmount;
        private decimal? _ftAmount;
        private decimal? _vatAmount;
        private decimal? _gAmount;
        private string _remark;
        private string _billbookId;
        private string _installmentFlag;
        private string _lastInstallmentFlag;
        private string _type;
        private string _groupType;



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
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }


        [DataMember(Order=4)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=5)]
        public int? Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        [DataMember(Order=6)]
        public string Period
        {
            get { return (_period == null) ? "" : (_period.Length == 7) ? _period : StringConvert.FormatPeriod(_period); }
            set { _period = value; }
        }


        [DataMember(Order=7)]
        public decimal? BaseAmount
        {
            get { return _baseAmount; }
            set { _baseAmount = value; }
        }


        [DataMember(Order=8)]
        public decimal? FtAmount
        {
            get { return _ftAmount; }
            set { _ftAmount = value; }
        }


        [DataMember(Order=9)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }


        [DataMember(Order=10)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }


        [DataMember(Order=11)]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }


        [DataMember(Order=12)]
        public string BillBookId
        {
            get { return _billbookId; }
            set { _billbookId = value; }
        }


        [DataMember(Order=13)]
        public string InstallmentFlag
        {
            get { return _installmentFlag; }
            set { _installmentFlag = value; }
        }


        [DataMember(Order=14)]
        public string LastInstallmentFlag
        {
            get { return _lastInstallmentFlag; }
            set { _lastInstallmentFlag = value; }
        }


        [DataMember(Order=15)]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


        [DataMember(Order=16)]
        public string GroupType
        {
            get { return _groupType; }
            set { _groupType = value; }
        }

    }
}
