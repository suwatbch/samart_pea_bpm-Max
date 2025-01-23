using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class FineBookInfo
    {
        private string _billbookId;
        private FineInBookInfo _fineInfo;


        [DataMember(Order=1)]
        public string BillBookId
        {
            set { _billbookId = value; }
            get { return _billbookId; }
        }


        [DataMember(Order=2)]
        public FineInBookInfo BookFineInfo
        {
            set { _fineInfo = value; }
            get { return _fineInfo; }
        }

    }
}
