using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookPaymentSummaryInfo
    {
        private int? _branchCount;
        private int? _mruCount;
        private int? _caCount;
        private int? _periodCount;
        private string _paymentMethod;
        private string _paymentDate;
        private decimal? _totalAmount;


        [DataMember(Order=1)]
        public int? BranchCount
        {
            get { return this._branchCount; }
            set { this._branchCount = value; }
        }

        [DataMember(Order=2)]
        public int? MruCount
        {
            get { return this._mruCount; }
            set { this._mruCount = value; }
        }

        [DataMember(Order=3)]
        public int? CaCount
        {
            get { return this._caCount; }
            set { this._caCount = value; }
        }

        [DataMember(Order=4)]
        public int? PeriodCount
        {
            get { return this._periodCount; }
            set { this._periodCount = value; }
        }

        [DataMember(Order=5)]
        public string PaymentMethod
        {
            get { return this._paymentMethod; }
            set { this._paymentMethod = value; }
        }

        [DataMember(Order=6)]
        public string PaymentDate
        {
            get { return this._paymentDate; }
            set { this._paymentDate = value; }
        }

        [DataMember(Order=7)]
        public decimal? TotalAmount
        {
            get { return this._totalAmount; }
            set { this._totalAmount = value; }
        }


    }
}
