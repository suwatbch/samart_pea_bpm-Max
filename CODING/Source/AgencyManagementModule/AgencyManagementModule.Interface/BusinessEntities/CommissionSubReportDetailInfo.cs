using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    /// <summary>
    /// Use in rptCommissionDetail report
    /// </summary>
    [DataContract]
    public class CommissionSubReportDetailInfo
    {
        string _billBookNo;
        int? _residentBillCount = 0;
        decimal? _residentBillElectric = 0;
        int? _smallBizBillCount = 0 ;
        decimal? _smallBizElectric = 0;
        int? _govermentDepartmentBillCount = 0;
        decimal _govermentDepartmentBillElectric = 0;
        int? _residentBillReceiveCount = 0;
        decimal? _residentBillReceive = 0;
        int? _smallBizBillReceiveCount = 0;
        decimal? _smallBizBillReceive = 0;
        int? _govermentDepartmentBillReceiveCount = 0;
        decimal? _govermentDepartmentBillReceive = 0;


        [DataMember(Order=1)]
        public string BillBookNo
        {
            get { return this._billBookNo; }
            set { this._billBookNo = value; }
        }

        [DataMember(Order=2)]
        public int? ResidentBillCount
        {
            get { return this._residentBillCount; }
            set { this._residentBillCount = value; }
        }

        [DataMember(Order=3)]
        public decimal? ResidentBillElectric
        {
            get { return this._residentBillElectric; }
            set { this._residentBillElectric = value; }
        }


        [DataMember(Order=4)]
        public int? SmallBizBillCount
        {
            get { return this._smallBizBillCount; }
            set { this._smallBizBillCount = value; }
        }

        [DataMember(Order=5)]
        public decimal? SmallBizElectric
        {
            get { return this._smallBizElectric; }
            set { this._smallBizElectric = value ; }
        }

        [DataMember(Order=6)]
        public int? GovermentDepartmentBillCount
        {
            get { return this._govermentDepartmentBillCount; }
            set { this._govermentDepartmentBillCount = value; }
        }

        [DataMember(Order=7)]
        public decimal GovermentDepartmentBillElectric
        {
            get { return this._govermentDepartmentBillElectric; }
            set { this._govermentDepartmentBillElectric = value; }
        }

        [DataMember(Order=8)]
        public int? ResidentBillReceiveCount
        {
            get { return this._residentBillReceiveCount; }
            set { this._residentBillReceiveCount = value; }
        }

        [DataMember(Order=9)]
        public decimal? ResidentBillReceive
        {
            get { return this._residentBillReceive; }
            set {this._residentBillReceive = value;}
        }

        [DataMember(Order=10)]
        public int? SmallBizBillReceiveCount
        {
            get { return this._smallBizBillReceiveCount; }
            set { this._smallBizBillReceiveCount = value; ; }
        }

        [DataMember(Order=11)]
        public decimal? SmalBizBillReceive
        {
            get { return this._smallBizBillReceive; }
            set { this._smallBizBillReceive = value; }
        }

        [DataMember(Order=12)]
        public int? GovermentDepartmentBillReceiveCount
        {
            get { return this._govermentDepartmentBillReceiveCount; }
            set { this._govermentDepartmentBillReceiveCount = value ; }
        }

        [DataMember(Order=13)]
        public decimal? GovermentDepartmentBillReceive
        {
            get { return this._govermentDepartmentBillReceive; }
            set { this._govermentDepartmentBillReceive = value; }
        }
    }
}
