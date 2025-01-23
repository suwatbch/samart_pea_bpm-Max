using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class OfflinePayment
    {
        private string _cashierId;
        private string _cashierName;
        private decimal? _gAmount;

        [DataMember(Order = 1)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }

        [DataMember(Order = 2)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

        [DataMember(Order = 3)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

    }
}
