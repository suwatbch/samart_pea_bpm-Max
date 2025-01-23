using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.SmartPlus.Interface.BusinessEntities
{
    [DataContract]
    public class UpdateBillMarkFlagServiceInfo
    {
        private string _updateBillMarkFlagServiceResult; 

        [DataMember(Order=1)]
        public string UpdateBillMarkFlagServiceResult
        {
            get { return this._updateBillMarkFlagServiceResult; }
            set { this._updateBillMarkFlagServiceResult = value; }
        }    
    }
}
