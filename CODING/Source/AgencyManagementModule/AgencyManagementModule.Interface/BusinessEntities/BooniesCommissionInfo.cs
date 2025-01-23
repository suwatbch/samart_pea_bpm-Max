using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BooniesCommissionInfo
    {
        private decimal? _transport=0;
        private decimal? _specialOffer=0;
        private decimal? _extraForBoonies=0;
        private decimal? _total=0;


        [DataMember(Order=1)]
        public decimal? Transport
        {
            get { return _transport; }
            set { _transport = value; }
        }


        [DataMember(Order=2)]
        public decimal? SpecialOffer
        {
            get { return _specialOffer; }
            set { _specialOffer = value; }
        }


        [DataMember(Order=3)]
        public decimal? ExtraForBoonies
        {
            get { return _extraForBoonies; }
            set { _extraForBoonies = value; }
        }


        [DataMember(Order=4)]
        public decimal? Total
        {
            get { return _total; }
            set { _total = value; }
        }
    }
}
