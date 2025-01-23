using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PEA.BPM.PaymentManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class TrayMoneyInfo
    {
        private decimal? _cashAmount;
        private decimal? _cashPendingAmount;
        private decimal? _chequeAmount;
        private decimal? _chequePendingAmount;


        [DataMember(Order=1)]
        public decimal? ChequeAmount
        {
            set { _chequeAmount = value; }
            get { return _chequeAmount; }
        }


        [DataMember(Order=2)]
        public decimal? ChequePendingAmount
        {
            set { _chequePendingAmount = value; }
            get { return _chequePendingAmount; }
        }


        [DataMember(Order=3)]
        public decimal? CashAmount
        {
            set { _cashAmount = value; }
            get { return _cashAmount; }
        }


        [DataMember(Order=4)]
        public decimal? CashPendingAmount
        {
            set { _cashPendingAmount = value; }
            get { return _cashPendingAmount; }
        }


        //[DataMember(Order=5)]
        public decimal? LeftAmount
        {
            get { return ((_cashAmount - _cashPendingAmount)); }
        }
    }
}
