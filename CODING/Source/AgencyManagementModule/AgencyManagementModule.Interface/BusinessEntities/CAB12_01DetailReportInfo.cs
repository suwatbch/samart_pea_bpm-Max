using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB12_01DetailReportInfo
    {
        string _branchId = null;
        string _mruId = null;
        string _caId = null;
        string _caName = null;
        string _caAddress = null;
        string _agencyId = null;
        string _agencyName = null;
        string _startPeriod = null;

        int? _totalDiffMonth = 0;
        int? _yearStart = 0;
        int? _monthStart = 0;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=2)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }

        [DataMember(Order=3)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        [DataMember(Order=4)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }

        [DataMember(Order=5)]
        public string CaAddress
        {
            get { return this._caAddress; }
            set { this._caAddress = value; }
        }


        [DataMember(Order=6)]
        public string AgencyId
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
        }

        [DataMember(Order=7)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }

        [DataMember(Order=8)]
        public string StartPeriod
        {
            get { return this._startPeriod; }
            set { this._startPeriod = value; }
        }

        [DataMember(Order=9)]
        public int? TotalDiffMonth
        {
            get { return this._totalDiffMonth; }
            set { this._totalDiffMonth = value; }
        }

        [DataMember(Order=10)]
        public int? YearStart
        {
            get { return this._yearStart; }
            set { this._yearStart = value; }
        }

        [DataMember(Order=11)]
        public int? MonthStart
        {
            get { return this._monthStart; }
            set { this._monthStart = value; }
        }
    }
}
