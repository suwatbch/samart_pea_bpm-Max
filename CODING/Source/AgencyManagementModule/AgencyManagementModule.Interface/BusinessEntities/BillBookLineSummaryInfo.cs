using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{


    [DataContract]
    public class BillBookLineSummaryInfo
    {
        int? _countLineNo = 0;
        int? _sumCollectBillCount = 0;
        decimal? _sumCollectBillAmount = 0;
        int? _sumNotCollectBillCount = 0;
        decimal? _sumNotCollectBillAmount = 0;
        int? _sumThreeMonthBillCount = 0;
        int? _sumTotalCount = 0;
        decimal? _sumTotalAmount = 0;


        [DataMember(Order=1)]
        public int? CountLineNo
        {
            get { return this._countLineNo; }
            set { this._countLineNo = value; }
        }

        [DataMember(Order=2)]
        public int? SumCollectBillCount
        {
            get { return this._sumCollectBillCount; }
            set { this._sumCollectBillCount = value; }
        }

        [DataMember(Order=3)]
        public decimal? SumCollectBillAmount
        {
            get { return this._sumCollectBillAmount; }
            set { this._sumCollectBillAmount = value; }
        }

        [DataMember(Order=4)]
        public int? SumNotCollectBillCount
        {
            get { return this._sumNotCollectBillCount; }
            set { this._sumNotCollectBillCount = value; }
        }

        [DataMember(Order=5)]
        public decimal? SumNotCollectBillAmount
        {
            get { return this._sumNotCollectBillAmount; }
            set { this._sumNotCollectBillAmount = value; }
        }

        [DataMember(Order=6)]
        public int? SumThreeMonthBillCount
        {
            get { return this._sumThreeMonthBillCount; }
            set { this._sumThreeMonthBillCount = value; ; }
        }

        [DataMember(Order=7)]
        public int? SumTotalCount
        {
            get { return this._sumTotalCount; }
            set { this._sumTotalCount = value; }
        }

        [DataMember(Order=8)]
        public decimal? SumTotalAmount
        {
            get { return this._sumTotalAmount; }
            set { this._sumTotalAmount = value; }
        }
    }

}
