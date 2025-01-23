using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB10_01DetailReportInfo : IComparable<CAB10_01DetailReportInfo>
    {

        private string _rowId;
        private string _branchId;
        private string _branchName;
        private string _portionId;
        private DateTime? _meterReadDt;
        private DateTime? _meterReadActDt;
        private DateTime? _transferDt;
        private DateTime? _transferActDt;
        private DateTime? _billPrintDt;
        private DateTime? _billPrintActDt;
        private DateTime? _bookCreatedDt;
        private DateTime? _bookCreatedActDt;
        private DateTime? _bookCheckInDt;
        private DateTime? _bookCheckInActDt;
        private string _agencyId;
        private string _minMruId;
        private string _maxMruId;



        [DataMember(Order=1)]
        public string RowId
        {
            get { return this._rowId; }
            set { this._rowId = value; }
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
        public string PortionId
        {
            get { return this._portionId; }
            set { this._portionId = value; }
        }

        [DataMember(Order=5)]
        public DateTime? MeterReadDt
        {
            get { return this._meterReadDt; }
            set { this._meterReadDt = value; }
        }

        [DataMember(Order=6)]
        public DateTime? MeterReadActDt
        {
            get { return this._meterReadActDt; }
            set { this._meterReadActDt = value; }
        }

        [DataMember(Order=7)]
        public DateTime? TransferDt
        {
            get { return this._transferDt; }
            set { this._transferDt = value; }
        }

        [DataMember(Order=8)]
        public DateTime? TransferActDt
        {
            get { return this._transferActDt; }
            set { this._transferActDt = value; }
        }

        [DataMember(Order=9)]
        public DateTime? BillPrintDt
        {
            get { return this._billPrintDt; }
            set { this._billPrintDt = value; }
        }

        [DataMember(Order=10)]
        public DateTime? BillPrintActDt
        {
            get { return this._billPrintActDt; }
            set { this._billPrintActDt = value; }
        }


        [DataMember(Order=11)]
        public DateTime? BookCreatedDt
        {
            get { return this._bookCreatedDt; }
            set { this._bookCreatedDt = value; }
        }

        [DataMember(Order=12)]
        public DateTime? BookCreatedActDt
        {
            get { return this._bookCreatedActDt; }
            set { this._bookCreatedActDt = value; }
        }


        [DataMember(Order=13)]
        public DateTime? BookCheckInDt
        {
            get { return this._bookCheckInDt; }
            set { this._bookCheckInDt = value; }
        }


        [DataMember(Order=14)]
        public DateTime? BookCheckInActDt
        {
            get { return this._bookCheckInActDt; }
            set { this._bookCheckInActDt = value; }
        }


        [DataMember(Order=15)]
        public string AgencyId
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
        }

        [DataMember(Order=16)]
        public string MinMruId
        {
            get { return this._minMruId; }
            set { this._minMruId = value; }
        }

        [DataMember(Order=17)]
        public string MaxMruId
        {
            get { return this._maxMruId; }
            set { this._maxMruId = value; }
        }


        #region IComparable<CAB10_01DetailReportInfo> Members

        public int CompareTo(CAB10_01DetailReportInfo reportItem)
        {

            return BranchId.CompareTo(reportItem.BranchId);

        }

        #endregion
    }
}
