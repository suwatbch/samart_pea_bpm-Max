using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class CancelReason
    {
        private string _crId;
        private string _description;


        [DataMember(Order=1)]
        public string CrId
        {
            get { return _crId; }
            set { _crId = value; }
        }


        [DataMember(Order=2)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public CancelReason(string crId, string description)
        {
            this._crId = crId;
            this._description = description;
        }


    }
}
