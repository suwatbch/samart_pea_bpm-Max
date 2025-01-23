using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
	public class DeliveryPlace
	{
        string _id;
        string _deliveryBranchId;
        string _placeName;
        string _commBranchId;
        string _modifiedBy;
        string _idPrefix;


        [DataMember(Order=1)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }


        [DataMember(Order=2)]
        public string DeliveryBranchId
        {
            get { return _deliveryBranchId; }
            set { _deliveryBranchId = value; }
        }


        [DataMember(Order=3)]
        public string CreateBranchId
        {
            get { return _commBranchId; }
            set { _commBranchId = value; }
        }


        [DataMember(Order=4)]
        public string PlaceName
        {
            get { return _placeName; }
            set { _placeName = value; }
        }


        [DataMember(Order=5)]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }


        [DataMember(Order=6)]
        public string IdPrefix
        {
            get { return _idPrefix; }
            set { _idPrefix = value; }
        }
	}
}
