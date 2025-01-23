using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class BaseParam
    {
        protected bool _isOtherBranch;

        [DataMember(Order=1)]
        public bool IsOtherBranch
        {
            get { return _isOtherBranch; }
            set { _isOtherBranch = value; }
        }

        public BaseParam(){}

        public BaseParam(bool isOtherBranch)
        {
            _isOtherBranch = isOtherBranch;
        }
    }
}
