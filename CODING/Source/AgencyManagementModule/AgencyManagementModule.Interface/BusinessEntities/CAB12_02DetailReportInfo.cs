using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CAB12_02DetailReportInfo
    {
        string _branchId = null;
        string _branchName = null;
        int? _totalCount = 0;


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
        public int? TotalCount
        {
            get { return this._totalCount; }
            set { this._totalCount = value; }
        }
    }
}
