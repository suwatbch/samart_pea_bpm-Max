using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CommissionReportDetailInfo
    {
        private string _agencyID;
        private string _agencyName;
        private DateTime _printDate;
        private DateTime _paymentDate;
        private string _period;
      
        private DateTime _calculateDate;
        private List<CommissionSubReportDetailInfo> _commissionDetailList = new List<CommissionSubReportDetailInfo>();



        [DataMember(Order=1)]
        public string AgencyID
        {
            get { return this._agencyID; }
            set { this._agencyID = value; }
        }


        [DataMember(Order=2)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }


        [DataMember(Order=3)]
        public DateTime PrintDate
        {
            get { return this._printDate; }
            set { this._printDate = value; }
        }


        [DataMember(Order=4)]
        public DateTime PaymentDate
        {
            get { return this._paymentDate; }
            set { this._paymentDate = value; }
        }


        [DataMember(Order=5)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }



        [DataMember(Order=6)]
        public DateTime CalculateDate
        {
            get { return this._calculateDate; }
            set { this._calculateDate = value; }
        }


        [DataMember(Order=7)]
        public List<CommissionSubReportDetailInfo> CommissionDetailList
        {
            get { return this._commissionDetailList; }
            set { this._commissionDetailList = value; }
        }

    }
}
