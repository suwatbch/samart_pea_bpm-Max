using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.HelpDeskUnlockMarkFlagModule.Interface.BusinessEntities
{
    [DataContract]
    public class UnlockMarkFlagServiceInfo
    {
        private string _unlockMarkFlagServiceResult; 

        [DataMember(Order=1)]
        public string UnlockMarkFlagServiceResult
        {
            get { return this._unlockMarkFlagServiceResult; }
            set { this._unlockMarkFlagServiceResult = value; }
        }    
    }
}
