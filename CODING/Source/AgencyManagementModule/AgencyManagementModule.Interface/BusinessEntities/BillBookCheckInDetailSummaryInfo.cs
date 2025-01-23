using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookCheckInDetailSummaryInfo
    {
        #region "Properties"
        private int? _caCount = 0;
        private int? _periodCount ;
        private string _paymentMethod = String.Empty;
        private string _collectStatus = String.Empty;
        private string _paymentDate = String.Empty;
        private decimal? _totalAmount = 0;
        #endregion

        #region

        [DataMember(Order=1)]
        public int? CaCount
        {
            get { return this._caCount; }
            set { this._caCount = value; }
        }


        [DataMember(Order=2)]
        public int? PeriodCount 
        {
            get { return this._periodCount; }
            set { this._periodCount = value; }
        }


        [DataMember(Order=3)]
        public string PaymentMethod
        {
            get { return this._paymentMethod; }
            set { this._paymentMethod = value; }
        }


        [DataMember(Order=4)]
        public string CollectStatus
        {
            get { return this._collectStatus; }
            set { this._collectStatus = value; }
        }


        [DataMember(Order=5)]
        public string PaymentDate
        {
            get { return this._paymentDate; }
            set { this._paymentDate = value; }
        }


        [DataMember(Order=6)]
        public decimal? TotalAmount
        {
            get { return this._totalAmount; }
            set { this._totalAmount = value; }
        }
        #endregion
    }
}
