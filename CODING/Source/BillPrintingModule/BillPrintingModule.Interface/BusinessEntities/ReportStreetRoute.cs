using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportStreetRoute
    {
        string _branchId;
        string _branchName;
        string _mruId;
        string _portionNo;
        int? _total;
        int? _number;
        string _ptcNo;
        string _meterReaderId;
        string _billControllerId;
        string _groupBranchId;
        string _groupBranchName;

        int? _readPtc;
        int? _readMat;
        int? _readOther;
        int? _totBlue;
        int? _totGreen;
        int? _totA4;
        int? _totNSpotBill;
        int? _totYSpotBill;
        int? _totOther;
        int? _totNoPrint;
        int? _totA4Addition;
        int? _totBill;

        int? _readSpotBill;
        int? _readAMR;


        [DataMember(Order=1)]
        public int? TotBill
        {
            get { return _totBill; }
            set { _totBill = value; }
        }



        [DataMember(Order=2)]
        public int? ReadSpotBill
        {
            get { return _readSpotBill; }
            set { _readSpotBill = value; }
        }


        [DataMember(Order=3)]
        public int? ReadAMR
        {
            get { return _readAMR; }
            set { _readAMR = value; }
        }
        

        [DataMember(Order=4)]
        public int? TotNoPrint
        {
            get { return _totNoPrint; }
            set { _totNoPrint = value; }
        }


        [DataMember(Order=5)]
        public int? TotA4Addition
        {
            get { return _totA4Addition; }
            set { _totA4Addition = value; }
        }


        [DataMember(Order=6)]
        public string GroupBranchId
        {
            get { return _groupBranchId; }
            set { _groupBranchId = value; }
        }


        [DataMember(Order=7)]
        public string GroupBranchName
        {
            get { return _groupBranchName; }
            set { _groupBranchName = value; }
        }


        [DataMember(Order=8)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=9)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }


        [DataMember(Order=10)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }


        [DataMember(Order=11)]
        public string PortionNo
        {
            get { return _portionNo; }
            set { _portionNo = value; }
        }
       

        [DataMember(Order=12)]
        public int? Total
        {
            get { return _total; }
            set { _total = value; }
        }


        [DataMember(Order=13)]
        public int? Number
        {
            get { return _number; }
            set { _number = value; }
        }


        [DataMember(Order=14)]
        public string PtcNo
        {
            get { return _ptcNo; }
            set { _ptcNo = value; }
        }


        [DataMember(Order=15)]
        public string MeterReaderId
        {
            get { return _meterReaderId; }
            set { _meterReaderId = value; }
        }


        [DataMember(Order=16)]
        public string BillControllerId
        {
            get { return _billControllerId; }
            set { _billControllerId = value; }
        }


        [DataMember(Order=17)]
        public int? ReadPtc
        {
            get { return _readPtc; }
            set { _readPtc = value; }
        }


        [DataMember(Order=18)]
        public int? ReadMat
        {
            get { return _readMat; }
            set { _readMat = value; }
        }


        [DataMember(Order=19)]
        public int? ReadOther
        {
            get { return _readOther; }
            set { _readOther = value; }
        }


        [DataMember(Order=20)]
        public int? TotBlue
        {
            get { return _totBlue; }
            set { _totBlue = value; }
        }


        [DataMember(Order=21)]
        public int? TotGreen
        {
            get { return _totGreen; }
            set { _totGreen = value; }
        }


        [DataMember(Order=22)]
        public int? TotA4
        {
            get { return _totA4; }
            set { _totA4 = value; }
        }


        [DataMember(Order=23)]
        public int? TotNSpotBill
        {
            get { return _totNSpotBill; }
            set { _totNSpotBill = value; }
        }


        [DataMember(Order=24)]
        public int? TotYSpotBill
        {
            get { return _totYSpotBill; }
            set { _totYSpotBill = value; }
        }


        [DataMember(Order=25)]
        public int? TotOther
        {
            get { return _totOther; }
            set { _totOther = value; }
        }
    }
}
