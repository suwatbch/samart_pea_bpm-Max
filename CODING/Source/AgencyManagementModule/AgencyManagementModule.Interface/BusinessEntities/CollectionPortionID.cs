using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CollectionPortionID
    {
        private int? _SAP_Portion = 0;
        private int? _SAP_Mru = 0;


        [DataMember(Order=1)]
        public int? SAP_Portion
        {
            get { return _SAP_Portion; }
            set { _SAP_Portion = value; }
        }


        [DataMember(Order=2)]
        public int? SAP_MRU
        {
            get { return _SAP_Mru; }
            set { _SAP_Mru = value; }
        }

    }
}
