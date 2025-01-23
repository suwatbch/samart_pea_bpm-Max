using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class InvoiceCommissionInfo
    {
        private int _percentSlip;
        private int _actualSlipToCustomer;
        private int _threeMonthNoPaidSlip;
        private int _totalBill;
        private decimal _percent;
        private decimal _total;


        [DataMember(Order=1)]
        public int PercentSlip
        {
            get { return _percentSlip; }
            set { _percentSlip = value; }
        }


        [DataMember(Order=2)]
        public int ActualSlipToCustomer
        {
            get { return _actualSlipToCustomer; }
            set { _actualSlipToCustomer = value; }
        }


        [DataMember(Order=3)]
        public int ThreeMonthNoPaidSlip
        {
            get { return _threeMonthNoPaidSlip; }
            set { _threeMonthNoPaidSlip = value; }
        }


        [DataMember(Order=4)]
        public decimal Percent
        {
            get { return _percent; }
            set { _percent = value; }
        }


        [DataMember(Order=5)]
        public int TotalBill
        {
            get { return _totalBill; }
            set { _totalBill = value; }
        }


        [DataMember(Order=6)]
        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }

    }
}
