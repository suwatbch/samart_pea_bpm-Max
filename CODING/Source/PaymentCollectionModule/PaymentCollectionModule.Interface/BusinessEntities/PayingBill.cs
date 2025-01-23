using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class PayingBill
    {
        private bool _toPay;
        private int _branchId;
        private int _lineId;
        private int _customerNumber;


        [DataMember(Order=1)]
        public int CustomerNumber
        {
            get { return _customerNumber; }
            set { _customerNumber = value; }
        }


        [DataMember(Order=2)]
        public bool ToPay
        {
            get { return _toPay; }
            set { _toPay = value; }
        }


        [DataMember(Order=3)]
        public int BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=4)]
        public int LineId
        {
            get { return _lineId; }
            set { _lineId = value; }
        }
    }
}
