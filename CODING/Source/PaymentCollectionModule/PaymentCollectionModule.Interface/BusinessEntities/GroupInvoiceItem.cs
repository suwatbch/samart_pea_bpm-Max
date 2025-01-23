using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class GroupInvoiceItem
    {
        string _caId;

        [DataMember(Order=1)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        string _name;

        [DataMember(Order=2)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        string _address;

        [DataMember(Order=3)]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        string _period;

        [DataMember(Order=4)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }

        string _branchId;

        [DataMember(Order=5)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        decimal? _toBePaidExVatAmount;

        [DataMember(Order=6)]
        public decimal? ToBePaidExVatAmount
        {
            get { return _toBePaidExVatAmount; }
            set { _toBePaidExVatAmount = value; }
        }
    }
}
