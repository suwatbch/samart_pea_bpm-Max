using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB08_01DetailReportInfo
    {
        #region "Variable Local"
        private int? _seqNo = 0;
        private string _branchId = null;
        private string _branchName = null;
        private string _printPeriod = null;
        private string _portionId = null;
        private string _agencyId = null;
        private string _bookHolderId = null;
        private decimal? _securityDeposit;
        private string _mruId = null;
        private int? _caCount = 0;
        private decimal? _totalAmount = 0;
      
        #endregion
        
        #region "Property"


        [DataMember(Order=1)]
        public int? SeqNo
        {
            get { return this._seqNo; }
            set { this._seqNo = value; }
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
        public string PrintPeriod
        {
            get { return this._printPeriod; }
            set { this._printPeriod = value; }
        }

        [DataMember(Order=5)]
        public string PortionId
        {
            get { return this._portionId; }
            set { this._portionId = value; }
        }

        [DataMember(Order=6)]
        public string AgencyId
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
        }

        [DataMember(Order=7)]
        public string BookHolderId
        {
            get { return this._bookHolderId; }
            set { this._bookHolderId = value; }
        }

        [DataMember(Order=8)]
        public decimal? SecurityDeposit
        {
            get { return this._securityDeposit; }
            set { this._securityDeposit = value; }
        }

        [DataMember(Order=9)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }

        [DataMember(Order=10)]
        public int? CaCount
        {
            get { return this._caCount; }
            set { this._caCount = value; }
        }

        [DataMember(Order=11)]
        public decimal? TotalAmount
        {
            get { return this._totalAmount; }
            set { this._totalAmount = value; }
        }
      
        #endregion

    }
}
