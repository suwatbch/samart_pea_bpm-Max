using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class CustomerDetailSearchParamx
    {
        private string _name;
        private string _address;
        private string _caId;
        private string _regId;
        private bool _isOtherBranch;
        private string _branch;


        [DataMember(Order=1)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        [DataMember(Order=2)]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }


        [DataMember(Order=3)]
        public string RegId
        {
            get { return _regId; }
            set { _regId = value; }
        }


        [DataMember(Order=4)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }


        [DataMember(Order=5)]
        public bool isOtherBranch
        {
            get { return _isOtherBranch; }
            set { _isOtherBranch = value; }
        }


        [DataMember(Order=6)]
        public string Branch
        {
            get { return _branch; }
            set { _branch = value; }
        }
    }
}
