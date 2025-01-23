using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookSearchSavedDetail
    {
        private string _peaCode;
        private string _lineId;
        private int _canCollectBillCount;
        private double _canCollectBillAmount;
        private int _cannotCollectBillCount;
        private double _cannotCollectBillAmount;
        private int _threeMonthsBillCount;
        private string _noticeDate;
        private int _totalCount;
        private double _totalAmount;


        [DataMember(Order=1)]
        public string PeaCode
        {
            set { _peaCode = value; }
            get { return _peaCode; }
        }


        [DataMember(Order=2)]
        public string LineId
        {
            get { return _lineId; }
            set { _lineId = value; }
        }


        [DataMember(Order=3)]
        public int CanCollectBillCount
        {
            get { return _canCollectBillCount; }
            set { _canCollectBillCount = value; }
        }


        [DataMember(Order=4)]
        public double CanCollectBillAmount
        {
            get { return _canCollectBillAmount; }
            set { _canCollectBillAmount = value; }
        }


        [DataMember(Order=5)]
        public int CannotCollectBillCount
        {
            get { return _cannotCollectBillCount; }
            set { _cannotCollectBillCount = value; }
        }


        [DataMember(Order=6)]
        public double CannotCollectBillAmount
        {
            get { return _cannotCollectBillAmount; }
            set { _cannotCollectBillAmount = value; }
        }


        [DataMember(Order=7)]
        public int ThreeMonthsBillCount
        {
            get { return _threeMonthsBillCount; }
            set { _threeMonthsBillCount = value; }
        }


        [DataMember(Order=8)]
        public string NoticeDate
        {
            get { return _noticeDate; }
            set { _noticeDate = value; }
        }


        [DataMember(Order=9)]
        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }


        [DataMember(Order=10)]
        public double TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

    }
}
