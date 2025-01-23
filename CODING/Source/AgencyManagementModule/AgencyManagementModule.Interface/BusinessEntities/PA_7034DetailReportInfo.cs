using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class PA_7034DetailReportInfo
    {
        string _branchId = null;
        string _branchName = null;
        string _agencyId = null;
        string _agencyName = null;
        string _mruId = null;
        string _receiveNo = null;
        string _bookLot = null;
        string _billBookId = null;
        string _createDate;
        string _checkinDate;
        string _billBookType = null;
        int? _residentReceiveCount = 0;
        decimal? _residentReceiveAmount = 0;
        int? _smallBizReceiveCount = 0;
        decimal? _smallBizReceiveAmount = 0;
        int? _govReceiveCount = 0;
        decimal? _govReceiveAmount = 0;

        int? _residentCollectCount = 0;
        decimal? _residentCollectAmount = 0;
        int? _smallBizCollectCount = 0;
        decimal? _smallBizCollectAmount = 0;
        int? _govCollectCount = 0;
        decimal? _govCollectAmount = 0;

        decimal? _residentPercent = 0;
        decimal? _residentPercentAmount = 0;
        decimal? _smallBizPercent = 0;
        decimal? _smallBizPercentAmount = 0;
        decimal? _govPercent = 0;
        decimal? _govPercentAmount = 0;

        int? _pastBillCount = 0;



        [DataMember(Order=1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=2)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }

        [DataMember(Order=3)]
        public string AgencyId
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
        }

        [DataMember(Order=4)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }

        [DataMember(Order=5)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }

        [DataMember(Order=6)]
        public string ReceiveNo
        {
            get { return this._receiveNo; }
            set { this._receiveNo = value; }
        }


        [DataMember(Order=7)]
        public string BookLot
        {
            get { return this._bookLot; }
            set { this._bookLot = value; }
        }

        [DataMember(Order=8)]
        public string BillBookId
        {
            get { return this._billBookId; }
            set { this._billBookId = value; }
        }

        [DataMember(Order=9)]
        public string CreateDate
        {
            get { return this._createDate; }
            set { this._createDate = value; }
        }

        [DataMember(Order=10)]
        public string CheckinDate
        {
            get { return this._checkinDate; }
            set { this._checkinDate = value; }
        }

        [DataMember(Order=11)]
        public string BillBookType
        {
            get { return this._billBookType; }
            set { this._billBookType = value; }
        }

        [DataMember(Order=12)]
        public int? ResidentReceiveCount
        {
            get { return this._residentReceiveCount; }
            set { this._residentReceiveCount = value; }
        }

        [DataMember(Order=13)]
        public decimal? ResidentReceiveAmount
        {
            get { return this._residentReceiveAmount; }
            set { this._residentReceiveAmount = value; }
        }

        [DataMember(Order=14)]
        public int? SmallBizReceiveCount
        {
            get { return this._smallBizReceiveCount; }
            set { this._smallBizReceiveCount = value; }
        }

        [DataMember(Order=15)]
        public decimal? SmallBizReceiveAmount
        {
            get { return this._smallBizReceiveAmount; }
            set { this._smallBizReceiveAmount = value; }
        }

        [DataMember(Order=16)]
        public int? GovReceiveCount
        {
            get { return this._govReceiveCount; }
            set { this._govReceiveCount = value ; }
        }

        [DataMember(Order=17)]
        public decimal? GovReceiveAmount
        {
            get { return this._govReceiveAmount; }
            set { this._govReceiveAmount = value; }
        }


        [DataMember(Order=18)]
        public int? ResidentCollectCount
        {
            get { return this._residentCollectCount; }
            set { this._residentCollectCount = value; }
        }

        [DataMember(Order=19)]
        public decimal? ResidentCollectAmount
        {
            get { return this._residentCollectAmount; }
            set { this._residentCollectAmount = value; }
        }


        [DataMember(Order=20)]
        public int? SmallBizCollectCount
        {
            get { return this._smallBizCollectCount; }
            set { this._smallBizCollectCount = value ; }
        }

        [DataMember(Order=21)]
        public decimal? SmallBizCollectAmount
        {
            get { return this._smallBizCollectAmount; }
            set { this._smallBizCollectAmount = value; }
        }

        [DataMember(Order=22)]
        public int? GovCollectCount
        {
            get { return this._govCollectCount; }
            set { this._govCollectCount = value; }
        }


        [DataMember(Order=23)]
        public decimal? GovCollectAmount
        {
            get { return this._govCollectAmount; }
            set { this._govCollectAmount = value; }
        }


        [DataMember(Order=24)]
        public int? PastBillCount
        {
            get { return this._pastBillCount; }
            set { this._pastBillCount = value; }
 
        }

        //Checked TongKung
        //[DataMember(Order=25)]
        public decimal? ResidentPercent
        {
            get
            {
                decimal? retVal = 0;
                if (_residentReceiveCount != 0 && _residentCollectCount != 0)
                    retVal = (decimal?)_residentCollectCount / (decimal?)_residentReceiveCount * 100;
                return retVal;
            }
        }

        //Checked TongKung
        //[DataMember(Order=26)]
        public decimal? ResidentPercentAmount
        {
            get
            {
                decimal? retVal = 0;
                if (_residentReceiveAmount != 0 && _residentCollectAmount != 0)
                    retVal = _residentCollectAmount / _residentReceiveAmount * 100;
                return retVal;
            }
            
        }

        //Checked TongKung
        //[DataMember(Order=27)]
        public decimal? SmallBizPercent
        {
            get 
            {
                decimal? retVal = 0;
                if (_smallBizCollectCount != 0 && _smallBizReceiveCount != 0)
                    retVal = (decimal?)_smallBizCollectCount / (decimal?)_smallBizReceiveCount * 100;
                return retVal;
            }
        }

        //Checked TongKung
        //[DataMember(Order=28)]
        public decimal? SmallBizPercentAmount
        {
            get 
            {
                decimal? retVal = 0;
                if (_smallBizReceiveAmount != 0 && _smallBizCollectAmount != 0)
                    retVal = _smallBizCollectAmount / _smallBizReceiveAmount * 100;
                return retVal;
            }
        }

        //Checked TongKung
        //[DataMember(Order=29)]
        public decimal? GovPercent
        {
            get
            {
                decimal? retVal = 0;
                if (_govCollectCount != 0 && _govReceiveCount != 0)
                    retVal = (decimal?)_govCollectCount / (decimal?)_govReceiveCount * 100;
                return retVal;
            }
        }

        //Checked TongKung
        //[DataMember(Order=30)]
        public decimal? GovPercentAmount
        {
            get
            {
                decimal? retVal = 0;
                if (_govReceiveAmount != 0 && _govCollectAmount != 0)
                    retVal = _govCollectAmount / _govReceiveAmount * 100;
                return retVal;
            }
        }

    }
}
