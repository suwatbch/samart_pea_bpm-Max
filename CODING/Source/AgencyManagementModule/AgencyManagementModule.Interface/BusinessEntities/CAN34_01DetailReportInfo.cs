using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAN34_01DetailReportInfo
    {
        int? _rowNo = 0;
        string _branchId = null;
        string _branchName = null;
        string _agencyId = null;
        string _agencyName = null;
        string _mruId = null;
        int? _billCount = 0;
        decimal? _billAmount = 0;
        string _portionId = null;
        string _collectNo = null;
        string _meterReadDt;
        string _meterReadActDt;
        string _transferDt;
        string _transferActDt;
        string _printBillDt;
        string _printBillActDt;
        string _createBookDt;
        string _createBookActDt;
        string _checkInDt;
        string _checkInActDt;
        int? _planTotalDt;
        int? _actTotalDt;
      

        [DataMember(Order=1)]
        public int? RowNo
        {
            get { return this._rowNo; }
            set { this._rowNo = value; }
        }


        [DataMember(Order=2)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=3)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }


        [DataMember(Order=4)]
        public string AgencyId
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
        }


        [DataMember(Order=5)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }
     

        [DataMember(Order=6)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }


        [DataMember(Order=7)]
        public int? BillCount
        {
            get { return this._billCount; }
            set { this._billCount = value; }
        }


        [DataMember(Order=8)]
        public decimal? BillAmount
        {
            get { return this._billAmount; }
            set { this._billAmount = value; }
        }   
  

        [DataMember(Order=9)]
        public string PortionId
        {
            get { return this._portionId; }
            set { this._portionId = value;  }
        }

        public string CollectNo
        {
            get { 
                if (PortionId.Trim() == String.Empty)
                    return String.Empty;
                string portionPreFix = PortionId.Trim().Substring(0,1);
                switch(portionPreFix)
                {
                    case "A" :
                        return "1";
                    case "B" :
                        return "2";
                    case "C" :
                        return "3";
                    case "D" :
                        return "4";
                    default :
                        return String.Empty;
                }
            
            }           
        }


        [DataMember(Order=10)]
        public string MeterReadDt
        {
            get { return this._meterReadDt; }
            set { this._meterReadDt = value; }
        }


        [DataMember(Order=11)]
        public string MeterReadActDt
        {
            get { return this._meterReadActDt; }
            set { this._meterReadActDt = value; }
        }


        [DataMember(Order=12)]
        public string TransferDt
        {
            get { return this._transferDt; }
            set { this._transferDt = value; }        
        }


        [DataMember(Order=13)]
        public string TransferActDt
        {
            get { return this._transferActDt; }
            set { this._transferActDt = value; }
        }


        [DataMember(Order=14)]
        public string PrintBillDt
        {
            get { return this._printBillDt; }
            set { this._printBillDt = value; }
        }


        [DataMember(Order=15)]
        public string PrintBillActDt
        {
            get { return this._printBillActDt; }
            set { this._printBillActDt = value; }
        }


        [DataMember(Order=16)]
        public string CreateBookDt
        {
            get { return this._createBookDt; }
            set { this._createBookDt = value; }
        }


        [DataMember(Order=17)]
        public string CreateBookActDt
        {
            get { return this._createBookActDt; }
            set { this._createBookActDt = value; }
        }

        [DataMember(Order=18)]
        public string CheckInDt
        {
            get { return this._checkInDt; }
            set { this._checkInDt = value; }
        }

        [DataMember(Order=19)]
        public string CheckInActDt
        {
            get { return this._checkInActDt; }
            set { this._checkInActDt = value; }
        }


        [DataMember(Order=20)]
        public int? PlanTotalDt
        {
            get { return this._planTotalDt; }
            set { this._planTotalDt = value; }
        }


        [DataMember(Order=21)]
        public int? ActTotalDt
        {
            get { return this._actTotalDt; }
            set { this._actTotalDt = value; }
        }
    
    }
}
