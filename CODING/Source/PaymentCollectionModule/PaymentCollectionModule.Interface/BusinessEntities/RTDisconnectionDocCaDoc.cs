using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class RTDisconnectionDocCaDoc
    {
        private string _discNo;
        private string _caDocNo;
        private string _cancelFlag;
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
        public string CaDocNo
        {
            get { return _caDocNo; }
            set { _caDocNo = value; }
        }


        [DataMember(Order=3)]
        public string CancelFlag
        {
            get { return _cancelFlag; }
            set { _cancelFlag = value; }
        }


        [DataMember(Order=4)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }


        [DataMember(Order=5)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }


        [DataMember(Order=6)]
        public string Active
        {
            get { return _active; }
            set { _active = value; }
        }


        [DataMember(Order=7)]
        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }

    }
}
