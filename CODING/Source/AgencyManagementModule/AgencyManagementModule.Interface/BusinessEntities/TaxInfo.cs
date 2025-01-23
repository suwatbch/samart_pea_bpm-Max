using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class TaxInfo
    {
        private string _agentId;
        private string _taxCodeId;
        private decimal? _percent = 0;       
        private DateTime? _validFrom;
        private DateTime? _validTo;


        [DataMember(Order=1)]
        public string TaxCodeId
        {
            get { return _taxCodeId; }
            set { _taxCodeId = value; }
        }


        [DataMember(Order=2)]
        public decimal? Percent
        {
            get { return _percent; }
            set { _percent = value; }
        }


        [DataMember(Order=3)]
        public string AgentId
        {
            get { return _agentId; }
            set { _agentId = value; }
        }

        //Checked TongKung
        //[DataMember(Order=4)]
        public bool AgentStatus
        {
            get 
            {
                if (ValidTo == null || ValidFrom == null)
                    return false;
                else
                    return (Session.BpmDateTime.Now <= ValidTo) && (Session.BpmDateTime.Now >= ValidFrom); 
            }
           
        }

        [DataMember(Order=5)]
        public DateTime? ValidFrom
        {
            get { return this._validFrom; }
            set { this._validFrom = value; }
 
        }


        [DataMember(Order=6)]
        public DateTime? ValidTo
        {
            get { return this._validTo; }
            set { this._validTo = value; }
        }
    }
}
