using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class SpecialCommissionInfo
    {
        private List<InBoundCollectionInfo> _inBoundCollectionInfoList = new List<InBoundCollectionInfo>();
        private decimal? _completedBillPercent = 0;
        private decimal? _completedBillToal = 0;
        private decimal? _inBoundBillTotal = 0;
        private decimal? _totalBillAmount = 0;


        [DataMember(Order=1)]
        public decimal? TotalBillAmount
        {
            get { return _totalBillAmount; }
            set { _totalBillAmount = value; }
        }


        [DataMember(Order=2)]
        public List<InBoundCollectionInfo> InBoundCollectionInfoList
        {
            get { return _inBoundCollectionInfoList; }
            set { _inBoundCollectionInfoList = value; }
        }


        [DataMember(Order=3)]
        public decimal? CompletedBillPercent
        {
            get { return _completedBillPercent; }
            set { _completedBillPercent = value; }
        }


        [DataMember(Order=4)]
        public decimal? CompletedBillTotal
        {
            get { return _completedBillToal; }
            set { _completedBillToal = value; }
        }


        [DataMember(Order=5)]
        public decimal? InBoundBillTotal
        {
            get { return _inBoundBillTotal; }
            set { _inBoundBillTotal = value; }
        }
    }
}
