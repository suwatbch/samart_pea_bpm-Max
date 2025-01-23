using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class LineSearchBoxInfo
    {
        private string _mruId;
        private string _branchId;
        //private LineSearchType _searchType = LineSearchType.All;

        //public enum LineSearchType
        //{
        //    All = 1,
        //    AreaCode, 
        //    Id
        //}


        [DataMember(Order=1)]
        public string MruId
        {
            set { _mruId = value; }
            get { return _mruId; }
        }


        [DataMember(Order=2)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }

        //public LineSearchType SearchType
        //{
        //    set { _searchType = value; }
        //    get { return _searchType; }
        //}

    }
}
