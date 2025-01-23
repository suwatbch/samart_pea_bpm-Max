using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ARPaymentTypeInfo
    {
        string _aRPtId;
        string _paymentId;
        decimal? _amount = 0;
        string _ptId;
        string _description;
        decimal? _actualAmount = 0;
        string _modifiedBy;
        string _bankKey;
        string _chqNo;
        string _chqAccNo;
        DateTime? _chqDt;
        string _tranfAccNo;
        DateTime? _tranfDt;
        DateTime? _modifiedDt;
        
        bool _active;


        [DataMember(Order=1)]
        public string ARPtId
        {
            get { return this._aRPtId; }
            set { this._aRPtId = value; }
        }


        [DataMember(Order=2)]
        public string PaymentId
        {
            get { return this._paymentId; }
            set { this._paymentId = value; }
        }

        [DataMember(Order=3)]
        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }
        

        [DataMember(Order=4)]
        public decimal? ActualAmount
        {
            get { return this._actualAmount; }
            set { this._actualAmount = value; }
        }
        

        [DataMember(Order=5)]
        public string PtId
        {
            get { return this._ptId; }
            set { this._ptId = value; }
        }

        [DataMember(Order=6)]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }
        

        [DataMember(Order=7)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=8)]
        public string BankKey
        {
            get { return this._bankKey; }
            set { this._bankKey = value; }
        }


        [DataMember(Order=9)]
        public string ChqNo
        {
            get { return this._chqNo; }
            set { this._chqNo = value; }
        }


        [DataMember(Order=10)]
        public string ChqAccNo
        {
            get { return this._chqAccNo; }
            set { this._chqAccNo = value; }
        }

        [DataMember(Order=11)]
        public DateTime? ChqDt
        {
            get { return this._chqDt; }
            set { this._chqDt = value; }
        }

        [DataMember(Order=12)]
        public string TranfAccNo
        {
            get { return this._tranfAccNo; }
            set { this._tranfAccNo = value; }
        }

        [DataMember(Order=13)]
        public DateTime? TranfDt
        {
            get { return this._tranfDt; }
            set { this._tranfDt = value; }
        }

        [DataMember(Order=14)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }

        [DataMember(Order=15)]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        
    }
}
