using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class CustomerId
    {
        private int? _branchId;
        private int? _lineId;
        private int? _customerNumber;


        [DataMember(Order=1)]
        public int? BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=2)]
        public int? LineId
        {
            get { return _lineId; }
            set { _lineId = value; }
        }


        [DataMember(Order=3)]
        public int? CustomerNumber
        {
            get { return _customerNumber; }
            set { _customerNumber = value; }
        }
    }
}
