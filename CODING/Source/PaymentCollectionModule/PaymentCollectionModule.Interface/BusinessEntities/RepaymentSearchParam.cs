using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class RepaymentSearchParam
    {
        private string _branchId;
        private string _paymentVoucherId;
        private string _cashierName;
        private string _customerId;
        private string _customerName;


        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        [DataMember(Order=2)]
        public string PaymentVoucherId
        {
            get { return _paymentVoucherId; }
            set { _paymentVoucherId = value; }
        }

        [DataMember(Order=3)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }


        [DataMember(Order=4)]
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }


        [DataMember(Order=5)]
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }
    }
}
