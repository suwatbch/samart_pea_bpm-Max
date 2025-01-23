using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class FineSummaryInfo
    {
        private string _sumBookAmount;
        private string _sumPaidDate;
        private string _sumAdvAmount;
        private string _sumPaidAmount;
        private string _sumRemainAmount;
        private string _sumAdvFineAmount;
        private string _sumAdvOverDay;
        private string _sumReturnFineAmount;
        private string _sumReturnOverDay;
        private string _sumTotalFine;



        [DataMember(Order=1)]
        public string SumBookAmount
        {
            get { return _sumBookAmount; }
            set { _sumBookAmount = value; }
        }


        [DataMember(Order=2)]
        public string SumPaidDate
        {
            get { return _sumPaidDate; }
            set { _sumPaidDate = value; }
        }


        [DataMember(Order=3)]
        public string SumPaidAmount
        {
            get { return _sumPaidAmount; }
            set { _sumPaidAmount = value; }
        }


        [DataMember(Order=4)]
        public string SumAdvAmount
        {
            get { return _sumAdvAmount; }
            set { _sumAdvAmount = value; }
        }


        [DataMember(Order=5)]
        public string SumRemainAmount
        {
            get { return _sumRemainAmount; }
            set { _sumRemainAmount = value; }
        }


        [DataMember(Order=6)]
        public string SumAdvFineAmount
        {
            get { return _sumAdvFineAmount; }
            set { _sumAdvFineAmount = value; }
        }


        [DataMember(Order=7)]
        public string SumAdvOverDay
        {
            get { return _sumAdvOverDay; }
            set { _sumAdvOverDay = value; }
        }


        [DataMember(Order=8)]
        public string SumReturnFineAmount
        {
            get { return _sumReturnFineAmount; }
            set { _sumReturnFineAmount = value; }
        }


        [DataMember(Order=9)]
        public string SumReturnOverDay
        {
            get { return _sumReturnOverDay; }
            set { _sumReturnOverDay = value; }
        }


        [DataMember(Order=10)]
        public string SumTotalFine
        {
            get { return _sumTotalFine; }
            set { _sumTotalFine = value; }
        }


    }
}
