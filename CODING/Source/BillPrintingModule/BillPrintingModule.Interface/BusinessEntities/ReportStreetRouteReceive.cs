using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportStreetRouteReceive
    {
        string _billPred;
        int? _rowId;
        string _branchId;
        string _branchName;
        string _mruId;
        string _portionNo;
        int? _total=0;
        int? _received=0;
        int? _unReceiverd = 0;
        string _groupBranchId;
        string _groupBranchName;

        int? _normalBlue;
        int? _fixBlue;
        int? _normalGreen;
        int? _fixGreen;
        int? _normalA4;
        int? _fixA4;
        int? _normalSpotBill;
        int? _fixSpotBill;
        int? _normalA4Add;
        int? _fixA4Add;
        int? _totBlue;
        int? _totGreen;
        int? _totA4;
        int? _totSpotBill;
        int? _totA4Add;
        int? _totNBill;
        int? _totFBill;
        int? _totBill;


        [DataMember(Order=1)]
        public int? TotBlue
        {
            get { return _totBlue; }
            set { _totBlue = value; }
        }


        [DataMember(Order=2)]
        public int? TotGreen
        {
            get { return _totGreen; }
            set { _totGreen = value; }
        }


        [DataMember(Order=3)]
        public int? TotA4
        {
            get { return _totA4; }
            set { _totA4 = value; }
        }


        [DataMember(Order=4)]
        public int? TotSpotBill
        {
            get { return _totSpotBill; }
            set { _totSpotBill = value; }
        }


        [DataMember(Order=5)]
        public int? TotA4Add
        {
            get { return _totA4Add; }
            set { _totA4Add = value; }
        }


        [DataMember(Order=6)]
        public int? TotNBill
        {
            get { return _totNBill; }
            set { _totNBill = value; }
        }


        [DataMember(Order=7)]
        public int? TotFBill
        {
            get { return _totFBill; }
            set { _totFBill = value; }
        }


        [DataMember(Order=8)]
        public int? TotBill
        {
            get { return _totBill; }
            set { _totBill = value; }
        }


        [DataMember(Order=9)]
        public int? NormalBlue
        {
            get { return _normalBlue; }
            set { _normalBlue = value; }
        }


        [DataMember(Order=10)]
        public int? FixBlue
        {
            get { return _fixBlue; }
            set { _fixBlue = value; }
        }


        [DataMember(Order=11)]
        public int? NormalGreen
        {
            get { return _normalGreen; }
            set { _normalGreen = value; }
        }


        [DataMember(Order=12)]
        public int? FixGreen
        {
            get { return _fixGreen; }
            set { _fixGreen = value; }
        }


        [DataMember(Order=13)]
        public int? NormalA4
        {
            get { return _normalA4; }
            set { _normalA4 = value; }
        }


        [DataMember(Order=14)]
        public int? FixA4
        {
            get { return _fixA4; }
            set { _fixA4 = value; }
        }


        [DataMember(Order=15)]
        public int? NormalSpotBill
        {
            get { return _normalSpotBill; }
            set { _normalSpotBill = value; }
        }


        [DataMember(Order=16)]
        public int? FixSpotBill
        {
            get { return _fixSpotBill; }
            set { _fixSpotBill = value; }
        }


        [DataMember(Order=17)]
        public int? NormalA4Add
        {
            get { return _normalA4Add; }
            set { _normalA4Add = value; }
        }


        [DataMember(Order=18)]
        public int? FixA4Add
        {
            get { return _fixA4Add; }
            set { _fixA4Add = value; }
        }


        [DataMember(Order=19)]
        public string BillPred
        {
            get { return _billPred; }
            set { _billPred = value; }
        }


        [DataMember(Order=20)]
        public int? RowId
        {
            get { return _rowId; }
            set { _rowId = value; }
        }


        [DataMember(Order=21)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=22)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=23)]
        public string GroupBranchId
        {
            get { return _groupBranchId; }
            set { _groupBranchId = value; }
        }


        [DataMember(Order=24)]
        public string GroupBranchName
        {
            get { return _groupBranchName; }
            set { _groupBranchName = value; }
        }


        [DataMember(Order=25)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }


        [DataMember(Order=26)]
        public string PortionNo
        {
            get { return _portionNo; }
            set { _portionNo = value; }
        }


        [DataMember(Order=27)]
        public int? Total
        {
            get { return _total; }
            set { _total = value; }
        }


        [DataMember(Order=28)]
        public int? Received
        {
            get { return _received; }
            set { _received = value; }
        }


        [DataMember(Order=29)]
        public int? UnReceived
        {
            get { return _unReceiverd; }
            set { _unReceiverd = value; }
        }
    }
}
