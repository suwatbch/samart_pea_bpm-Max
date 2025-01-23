using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CallingOtherBillInfo
    {
        private int _sequence;
        private string _lineId;
        private string _userNo;
        private string _billPeriod;
        private string _customerName;
        private double _vat;
        private double _unpaid;
        private string _status;
        private string _paymentType;
        private string _paymentDate;


        [DataMember(Order=1)]
        public int Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }


        [DataMember(Order=2)]
        public string LineId
        {
            get { return _lineId; }
            set { _lineId = value; }
        }


        [DataMember(Order=3)]
        public string UserNo
        {
            get { return _userNo; }
            set { _userNo = value; }
        }


        [DataMember(Order=4)]
        public string BillPeriod
        {
            get { return _billPeriod; }
            set { _billPeriod = value; }
        }


        [DataMember(Order=5)]
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }


        [DataMember(Order=6)]
        public double Vat
        {
            get { return _vat; }
            set { _vat = value; }
        }


        [DataMember(Order=7)]
        public double UnpaidDebts
        {
            get { return _unpaid; }
            set { _unpaid = value; }
        }


        [DataMember(Order=8)]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }


        [DataMember(Order=9)]
        public string PaymentType
        {
            get { return _paymentType; }
            set { _paymentType = value; }
        }


        [DataMember(Order=10)]
        public string PaymentDate
        {
            get { return _paymentDate; }
            set { _paymentDate = value; }
        }

    }
}
