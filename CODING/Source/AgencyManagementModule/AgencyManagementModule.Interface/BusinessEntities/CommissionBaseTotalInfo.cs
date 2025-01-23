using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CommissionBaseTotalInfo
    {        
        private string _totalText = "√«¡‡ß‘π  ";
        private decimal? _totalValue =0;
        private int? _sumBillCountFirstAll = 0;
        private int? _sumBillCountFirstActual = 0;
        private int? _sumBillCountRepeatAll = 0;
        private int? _sumBillCountRepeatActual = 0;
        private decimal? _sumAmountFirstAll = 0;
        private decimal? _sumAmountFirstActual = 0;
        private decimal? _sumAmountRepeatAll = 0;
        private decimal? _sumAmountRepeatActual = 0;

        //Checked TongKung
        //[DataMember(Order=1)]
        public string TotalText
        {
            get { return _totalText; }
        }


        [DataMember(Order=2)]
        public decimal? TotalValue
        {
            get { return _totalValue; }
            set { _totalValue = value; }
        }


        [DataMember(Order=3)]
        public int? SumBillCountFirstAll
        {
            get { return _sumBillCountFirstAll; }
            set { _sumBillCountFirstAll = value; }
        }


        [DataMember(Order=4)]
        public int? SumBillCountFirstActual
        {
            get { return _sumBillCountFirstActual; }
            set { _sumBillCountFirstActual = value; }
        }

        //Checked TongKung
        public decimal? SumBillCountFirstPercent
        {
            get 
            {
                if (_sumBillCountFirstAll == 0)
                    return 0;
                else
                    return ((decimal?)_sumBillCountFirstActual /(decimal?)_sumBillCountFirstAll) * 100;
            }
        }


        [DataMember(Order=5)]
        public int? SumBillCountRepeatAll
        {
            get 
            {
                return _sumBillCountRepeatAll;
            }
            set 
            {
                _sumBillCountRepeatAll = value;
            }
        }

        [DataMember(Order=6)]
        public int? SumBillCountRepeatActual
        {
            get 
            {
                return _sumBillCountRepeatActual;
            }
            set 
            {
                _sumBillCountRepeatActual = value;
            }
        }

        //Checked TongKung
        public decimal? SumBillCountRepeatPercent
        {
            get 
            {
                if (_sumBillCountRepeatAll == 0)
                    return 0;
                else
                    return ((decimal?)_sumBillCountRepeatActual / (decimal?)_sumBillCountRepeatAll) * 100;
            }
        }


        [DataMember(Order=7)]
        public decimal? SumAmountFirstAll
        {
            get
            {
                return _sumAmountFirstAll;
            }
            set
            {
                _sumAmountFirstAll = value;
            }
        }


        [DataMember(Order=8)]
        public decimal? SumAmountFirstActual
        {
            get
            {
                return _sumAmountFirstActual;
            }
            set
            {
                _sumAmountFirstActual = value;
            }
        }

        //Checked TongKung
        public decimal? SumAmountFirstPercent
        {
            get 
            {
                if (_sumAmountFirstAll == 0)
                    return 0;
                else
                    return ((decimal?)_sumAmountFirstActual / (decimal?)_sumAmountFirstAll) * 100;
            }
        }


        [DataMember(Order=9)]
        public decimal? SumAmountRepeatAll
        {
            get
            {
                return _sumAmountRepeatAll;
            }
            set
            {
                _sumAmountRepeatAll = value;
            }
        }


        [DataMember(Order=10)]
        public decimal? SumAmountRepeatActual
        {
            get
            {
                return _sumAmountRepeatActual;
            }
            set
            {
                _sumAmountRepeatActual = value;
            }
        }

        //Checked TongKung
        public decimal? SumAmountRepeatPercent
        {
            get
            {
                if (_sumAmountRepeatAll == 0)
                    return 0;
                else
                    return ((decimal?)_sumAmountRepeatActual / (decimal?)_sumAmountRepeatAll) * 100;
            }
        }
    }
}
