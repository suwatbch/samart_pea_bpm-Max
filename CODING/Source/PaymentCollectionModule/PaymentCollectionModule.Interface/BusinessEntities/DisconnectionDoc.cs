using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class DisconnectionDoc
    {
        private string _discNo;
        private DateTime? _createdDt;
        private string _discStatusId;
        private string _discStatus;
        private DateTime? _releaseDt;
        private string _workOrderNo;
        private DateTime? _discPlanStart;
        private DateTime? _discPlanEnd;
        private string _workCenter;
        private DateTime? _postpConfirm;
        private DateTime? _fuseRemConfirm;
        private DateTime? _meterRemConfirm;
        private string _modifiedBy;
        private DateTime? _modifiedDt;
        private string _active;
        private string _action;



        [DataMember(Order=1)]
        public string DiscNo
        {
            get { return _discNo; }
            set { _discNo = value; }
        }


        [DataMember(Order=2)]
        public DateTime? CreatedDt
        {
            get { return _createdDt; }
            set { _createdDt = value; }
        }

        [DataMember(Order=3)]
        public string DiscStatusId
        {
            get { return _discStatusId; }
            set { _discStatusId = value; }
        }


        [DataMember(Order=4)]
        public string DiscStatus
        {
            get { return _discStatus; }
            set { _discStatus = value; }
        }


        [DataMember(Order=5)]
        public DateTime? ReleaseDt
        {
            get { return _releaseDt; }
            set { _releaseDt = value; }
        }


        [DataMember(Order=6)]
        public string WorkOrderNo
        {
            get { return _workOrderNo; }
            set { _workOrderNo = value; }
        }


        [DataMember(Order=7)]
        public DateTime? DiscPlanStart
        {
            get { return _discPlanStart; }
            set { _discPlanStart = value; }
        }


        [DataMember(Order=8)]
        public DateTime? DiscPlanEnd
        {
            get { return _discPlanEnd; }
            set { _discPlanEnd = value; }
        }


        [DataMember(Order=9)]
        public string WorkCenter { 
            get { return _workCenter; }
            set { _workCenter = value; } 
        }


        [DataMember(Order=10)]
        public DateTime? PostpConfirm
        {
            get { return _postpConfirm; }
            set { _postpConfirm = value; }
        }


        [DataMember(Order=11)]
        public DateTime? FuseRemConfirm
        {
            get { return _fuseRemConfirm; }
            set { _fuseRemConfirm = value; }
        }


        [DataMember(Order=12)]
        public DateTime? MeterRemConfirm
        {
            get { return _meterRemConfirm; }
            set { _meterRemConfirm = value; }
        }


        [DataMember(Order=13)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }


        [DataMember(Order=14)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }


        [DataMember(Order=15)]
        public string Active
        {
            get { return _active; }
            set { _active = value; }
        }


        [DataMember(Order=16)]
        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }

    }
}
