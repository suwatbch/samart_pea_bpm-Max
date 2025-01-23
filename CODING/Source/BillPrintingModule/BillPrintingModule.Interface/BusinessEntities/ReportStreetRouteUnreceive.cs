using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportStreetRouteUnreceive
    {
        int? _rowId;
        string _branchId;
        string _branchName;
        string _mruId;
        string _portionNo;
        int? _total=0;
        int? _unreceived=0;
        string _groupBranchId;
        string _groupBranchName;


        [DataMember(Order=1)]
        public int? RowId
        {
            get { return _rowId; }
            set { _rowId = value; }
        }


        [DataMember(Order=2)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=3)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=4)]
        public string GroupBranchId
        {
            get { return _groupBranchId; }
            set { _groupBranchId = value; }
        }


        [DataMember(Order=5)]
        public string GroupBranchName
        {
            get { return _groupBranchName; }
            set { _groupBranchName = value; }
        }


        [DataMember(Order=6)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }


        [DataMember(Order=7)]
        public string PortionNo
        {
            get { return _portionNo; }
            set { _portionNo = value; }
        }


        [DataMember(Order=8)]
        public int? Total
        {
            get { return _total; }
            set { _total = value; }
        }


        [DataMember(Order=9)]
        public int? UnReceived
        {
            get { return _unreceived; }
            set { _unreceived = value; }
        }
    }
}
