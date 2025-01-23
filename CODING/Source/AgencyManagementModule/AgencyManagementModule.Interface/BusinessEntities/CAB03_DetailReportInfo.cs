using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB03_DetailReportInfo
    {
        private string _branchId;
        private string _caId;
        private string _mruId;
        private string _period;
        private string _caName;
        private string _pmId;
        private string _strRecevieDate;
        private decimal? _totalAmount;
        private DateTime? _receiveDate;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=2)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=3)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }


        [DataMember(Order=4)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=5)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }


        [DataMember(Order=6)]
        public string PmId
        {
            get { return this._pmId; }
            set { this._pmId = value; }
        }


        [DataMember(Order=7)]
        public string StrReceiveDate
        {
            get { return this._strRecevieDate ; }
            set { this._strRecevieDate = value; }
        }


        [DataMember(Order=8)]
        public decimal? TotalAmount
        {
            get { return this._totalAmount; }
            set { this._totalAmount = value; }
        }


        [DataMember(Order=9)]
        public DateTime? ReceiveDate
        {
            get { return this._receiveDate; }
            set { this._receiveDate = value; }
        }
    }
}
