using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CashierMoneyFlowInfo
    {
        string _flowId;


        [DataMember(Order=1)]
        public string FlowId
        {
            get { return _flowId; }
            set { _flowId = value; }
        }
        string _description;


        [DataMember(Order=2)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        string _flowType;


        [DataMember(Order=3)]
        public string FlowType
        {
            get { return _flowType; }
            set { _flowType = value; }
        }
        string _glBankKey;



        [DataMember(Order=4)]
        public string GlBankKey
        {
            get { return _glBankKey; }
            set { _glBankKey = value; }
        }
        string _glAccountNo;


        [DataMember(Order=5)]
        public string GlAccountNo
        {
            get { return _glAccountNo; }
            set { _glAccountNo = value; }
        }
        decimal? _amount;


        [DataMember(Order=6)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        string _workId;


        [DataMember(Order=7)]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }
        string _transferId;



        [DataMember(Order=8)]
        public string TransferId
        {
            get { return _transferId; }
            set { _transferId = value; }
        }
        string _sapRefNo;



        [DataMember(Order=9)]
        public string SapRefNo
        {
            get { return _sapRefNo; }
            set { _sapRefNo = value; }
        }
        string _syncFlag;


        [DataMember(Order=10)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }
        DateTime? _modifiedDt;



        [DataMember(Order=11)]
        public DateTime? ModifiedDt
        {
            get { return _modifiedDt; }
            set { _modifiedDt = value; }
        }
        string _modifiedBy;


        [DataMember(Order=12)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
        string _active;



        [DataMember(Order=13)]
        public string Active
        {
            get { return _active; }
            set { _active = value; }
        }

        bool _specialTrans;

        [DataMember(Order=14)]
        public bool SpecialTrans
        {
            get { return _specialTrans; }
            set { _specialTrans = value; }
        }

        string _cashierId;

        [DataMember(Order=15)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }

        string _closeWorkBy;

        [DataMember(Order=16)]
        public string CloseWorkBy
        {
            get { return _closeWorkBy; }
            set { _closeWorkBy = value; }
        }

    }
}
