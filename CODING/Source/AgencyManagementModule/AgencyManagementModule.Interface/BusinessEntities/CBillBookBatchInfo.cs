using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CBillBookBatchInfo
    {
        private BillBookHeaderInfo _bookHeader;
        private List<BillStatusInfo> _billList = new List<BillStatusInfo>();


        [DataMember(Order=1)]
        public BillBookHeaderInfo BookHeader
        {
            get { return _bookHeader; }
            set { _bookHeader = value; }
        }


        [DataMember(Order=2)]
        public List<BillStatusInfo> BillList
        {
            get { return _billList; }
            set { _billList = value; }
        }
    }

}
