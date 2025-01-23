using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CloseWorkSumAll
    {
        private decimal? _sum1;
        private decimal? _sum2;
        private decimal? _sum3;
        private decimal? _sum4;
        private decimal? _sum5;
        private decimal? _sum6;
        private decimal? _sum7;
        private decimal? _sum8;
        private decimal? _sum9;
        private string _desc;


        [DataMember(Order=1)]
        public decimal? Sum1
        {
            get { return _sum1; }
            set { _sum1 = value; }
        }


        [DataMember(Order=2)]
        public decimal? Sum2
        {
            get { return _sum2; }
            set { _sum2 = value; }
        }


        [DataMember(Order=3)]
        public decimal? Sum3
        {
            get { return _sum3; }
            set { _sum3 = value; }
        }


        [DataMember(Order=4)]
        public decimal? Sum4
        {
            get { return _sum4; }
            set { _sum4 = value; }
        }


        [DataMember(Order=5)]
        public decimal? Sum5
        {
            get { return _sum5; }
            set { _sum5 = value; }
        }


        [DataMember(Order=6)]
        public decimal? Sum6
        {
            get { return _sum6; }
            set { _sum6 = value; }
        }


        [DataMember(Order=7)]
        public decimal? Sum7
        {
            get { return _sum7; }
            set { _sum7 = value; }
        }


        [DataMember(Order=8)]
        public decimal? Sum8
        {
            get { return _sum8; }
            set { _sum8 = value; }
        }


        [DataMember(Order=9)]
        public decimal? Sum9
        {
            get { return _sum9; }
            set { _sum9 = value; }
        }


        [DataMember(Order=10)]
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

    }
}
