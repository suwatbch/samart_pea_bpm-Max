using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CommissionBaseInfo
    {
        private string _customerType;
        private int _billCount_first_all = 0;
        private int _billCount_first_act = 0;
        private decimal? _billCount_first_percent = 0;
        private int _billCount_rep_all = 0;
        private int _billCount_rep_act = 0;
        private decimal? _billCount_rep_percent = 0;
        private decimal? _amount_first_all = 0;
        private decimal? _amount_first_act = 0;
        private decimal? _amount_first_percent = 0;
        private decimal? _amount_rep_all = 0;
        private decimal? _amount_rep_act = 0;
        private decimal? _amount_rep_percent = 0;
        private decimal? _regularPerson = 0;
        private decimal? _corporation = 0;
        private decimal? _totalBase = 0;


        [DataMember(Order=1)]
        public string CustomerType
        {
            get { return _customerType; }
            set { _customerType = value; }
        }


        [DataMember(Order=2)]
        public int BillCountFirstAll
        {
            get { return _billCount_first_all; }
            set { _billCount_first_all = value; }
        }


        [DataMember(Order=3)]
        public int BillCountFirstActual
        {
            get { return _billCount_first_act; }
            set { _billCount_first_act = value; }
        }


        [DataMember(Order=4)]
        public decimal? BillCountFirstPercent
        {
            get { return _billCount_first_percent; }
            set { _billCount_first_percent = value; }
        }


        [DataMember(Order=5)]
        public int BillCountRepeatAll
        {
            get { return _billCount_rep_all; }
            set { _billCount_rep_all = value; }
        }


        [DataMember(Order=6)]
        public int BillCountRepeatActual
        {
            get { return _billCount_rep_act; }
            set { _billCount_rep_act = value; }
        }


        [DataMember(Order=7)]
        public decimal? BillCountRepeatPercent
        {
            get { return _billCount_rep_percent; }
            set { _billCount_rep_percent = value; }
        }


        [DataMember(Order=8)]
        public decimal? AmountFirstAll
        {
            get { return _amount_first_all; }
            set { _amount_first_all = value; }
        }


        [DataMember(Order=9)]
        public decimal? AmountFirstActual
        {
            get { return _amount_first_act; }
            set { _amount_first_act = value; }
        }


        [DataMember(Order=10)]
        public decimal? AmountFirstPercent
        {
            get { return _amount_first_percent; }
            set { _amount_first_percent = value; }
        }


        [DataMember(Order=11)]
        public decimal? AmountRepeatAll
        {
            get { return _amount_rep_all; }
            set { _amount_rep_all = value; }
        }


        [DataMember(Order=12)]
        public decimal? AmountRepearActual
        {
            get { return _amount_rep_act; }
            set { _amount_rep_act = value; }
        }


        [DataMember(Order=13)]
        public decimal? AmountRepeatPercent
        {
            get { return _amount_rep_percent; }
            set { _amount_rep_percent = value; }
        }


        [DataMember(Order=14)]
        public decimal? RegularPerson
        {
            get { return _regularPerson; }
            set { _regularPerson = value; }
        }


        [DataMember(Order=15)]
        public decimal? Corporation
        {
            get { return _corporation; }
            set { _corporation = value; }
        }


        [DataMember(Order=16)]
        public decimal? TotalBase
        {
            get { return _totalBase; }
            set { _totalBase = value; }
        }
    }
}
