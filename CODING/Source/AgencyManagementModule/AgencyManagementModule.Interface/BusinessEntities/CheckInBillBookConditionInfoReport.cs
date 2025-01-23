using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CheckInBillBookConditionInfoReport
    {
        #region"Local Variable"
        private string _agentId;
        private string _billBookId;
        private string _period;
        private bool _printPreview;     
        private string _absId;
        private string _pmId;
        #endregion

        #region"Property"

        [DataMember(Order=1)]
        public string AgentId
        {
            get { return _agentId; }
            set { this._agentId = value; }
        }

        [DataMember(Order=2)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        [DataMember(Order=3)]
        public string BillBookId
        {
            get { return _billBookId; }
            set { this._billBookId = value; }
        }


        [DataMember(Order=4)]
        public bool PrintPreview
        {
            get { return _printPreview; }
            set { _printPreview = value; }
        }


        [DataMember(Order=5)]
        public string AbsId
        {
            get { return _absId; }
            set { _absId = value; }
        }


        [DataMember(Order=6)]
        public string PmId
        {
            get { return _pmId; }
            set { _pmId = value; }
        }

        #endregion
    }
}
