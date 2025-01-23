using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookPaymentStatusInfo
    {
        private string _peaCode;
        private string _lineId;
        private string _customerNo;
        private string _billPeriod;
        private string _paymentStatus;
        private string _paidDate;
        private double _debt;


        [DataMember(Order=1)]
        public string PeaCode
        {
            get { return _peaCode; }
            set { _peaCode = value; }
        }


        [DataMember(Order=2)]
        public string LineId
        {
            get { return _lineId; }
            set { _lineId = value; }
        }


        [DataMember(Order=3)]
        public string CustomerNo
        {
            get { return _customerNo; }
            set { _customerNo = value; }
        }


        [DataMember(Order=4)]
        public string BillPeriod
        {
            get { return _billPeriod; }
            set { _billPeriod = value; }
        }


        [DataMember(Order=5)]
        public string PaymentStatus
        {
            get { return _paymentStatus; }
            set { _paymentStatus = value; }
        }


        [DataMember(Order=6)]
        public string PaidDate
        {
            get { return _paidDate; }
            set { _paidDate = value; }
        }


        [DataMember(Order=7)]
        public double Debt
        {
            get { return _debt; }
            set { _debt = value; }
        }
    }
}
