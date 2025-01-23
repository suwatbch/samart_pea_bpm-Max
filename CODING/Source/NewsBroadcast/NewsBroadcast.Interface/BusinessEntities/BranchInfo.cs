using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.NewsBroadcast.Interface.BusinessEntities
{

    [DataContract]
    public class BranchInfo
    {
        private string branchId;
        private string areaId;
        private string branchName;


        [DataMember(Order=1)]
	    public string BranchId
	    {
		    get { return branchId;}
		    set { branchId = value;}
	    }


        [DataMember(Order=2)]
        public string AreaId
        {
            get { return areaId; }
            set { areaId = value; }
        }


        [DataMember(Order=3)]
        public string BranchName
        {
            get { return branchName; }
            set { branchName = value; }
        }
     
    }
}
