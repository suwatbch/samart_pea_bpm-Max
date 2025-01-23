using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class DeliveryPlaceInfo
    {
        private string _DeliveryPlaceId;
        private string _DeliveryBranchId;
        private string _DeliveryPlaceName;
        private string _CreateBranchId;
        private string _PostBranchServerId;
        private DateTime? _PostDt;
        private string _SyncFlag;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string DeliveryPlaceId
        {
            get { return _DeliveryPlaceId; }
            set { _DeliveryPlaceId = value; }
        }
        [DataMember(Order = 2)]
        public string DeliveryBranchId
        {
            get { return _DeliveryBranchId; }
            set { _DeliveryBranchId = value; }
        }
        [DataMember(Order = 3)]
        public string DeliveryPlaceName
        {
            get { return _DeliveryPlaceName; }
            set { _DeliveryPlaceName = value; }
        }
        [DataMember(Order = 4)]
        public string CreateBranchId
        {
            get { return _CreateBranchId; }
            set { _CreateBranchId = value; }
        }
        [DataMember(Order = 5)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 6)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 7)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 8)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 9)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 10)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 11)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
