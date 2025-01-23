using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB05_01DetailReportInfo
    {
        int _rowNo = 0;
        string _period = null;
        string _branchId = null;
        string _branchName = null;
        string _agencyId = null;
        string _agencyName = null;
        string _registerDt = null;
        decimal? _securityDeposit = 0;
        decimal? _collectValue = 0;
        string _billBookId = null;
        int?  _receiveCount = 0;
        string _bookLot = null;
        decimal? _recevieAmount = 0;
        int? _collectCount = 0;
        decimal? _collectAmount = 0;
        string _receiveNo = null;
        string _collectArea = null;
        decimal? _percentCount = 0;
        decimal? _percentAmount = 0;


        [DataMember(Order=1)]
        public int RowNo
        {
            get { return this._rowNo; }
            set { this._rowNo = value; }
        }


        [DataMember(Order=2)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        [DataMember(Order=3)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=4)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }

        [DataMember(Order=5)]
        public string AgencyId
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
        }

        [DataMember(Order=6)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }

        [DataMember(Order=7)]
        public string RegisterDt
        {
            get { return this._registerDt; }
            set { this._registerDt = value; }
        }

        [DataMember(Order=8)]
        public decimal? SecurityDeposit        
        {
            get { return this._securityDeposit; }
            set { this._securityDeposit = value; }
        }

        [DataMember(Order=9)]
        public decimal? CollectValue
        {
            get { return this._collectValue; }
            set { this._collectValue = value; }
        }

        [DataMember(Order=10)]
        public string BillBookId
        {
            get { return this._billBookId; }
            set { this._billBookId = value; }
        }

        [DataMember(Order=11)]
        public int? ReceiveCount
        {
            get { return this._receiveCount; }
            set { this._receiveCount = value; }
        }

        [DataMember(Order=12)]
        public decimal? RecevieAmount
        {
            get { return this._recevieAmount; }
            set { this._recevieAmount = value; }
        }

        [DataMember(Order=13)]
        public int? CollectCount
        {
            get { return this._collectCount; }
            set { this._collectCount = value; }
        }

        [DataMember(Order=14)]
        public decimal? CollectAmount
        {
            get { return this._collectAmount; }
            set { this._collectAmount = value; }
        }

        [DataMember(Order=15)]
        public string ReceiveNo
        {
            get { return this._receiveNo; }
            set { this._receiveNo = value; }
        }


        [DataMember(Order=16)]
        public string BookLot
        {
            get { return this._bookLot; }
            set { this._bookLot = value; }
        }

        [DataMember(Order=17)]
        public string CollectArea
        {
            get { return this._collectArea; }
            set { this._collectArea = value; }
        }


        [DataMember(Order=18)]
        public decimal? PercentCount
        {
            get { return this._percentCount; }
            set { this._percentCount = value; }
        }


        [DataMember(Order=19)]
        public decimal? PercentAmount
        {
            get { return this._percentAmount; }
            set { this._percentAmount = value; }
        }
    }
}
