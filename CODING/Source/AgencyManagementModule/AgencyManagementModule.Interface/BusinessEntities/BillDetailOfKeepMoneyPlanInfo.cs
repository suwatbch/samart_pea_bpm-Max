using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillDetailOfKeepMoneyPlanInfo
    {
        #region"Variable Local"
        private string _lineNo;
        private int? _billCounter = 0;
        private decimal? _amount = 0;
        private string _agentId;
        private string _areaCode;
        private string _remark;
        private string _periodTimeNo;
        private string _printTimeNo;
        private string _branchId;
        #endregion

        #region"Property"

        [DataMember(Order=1)]
        public string LineNo
        {
            get { return _lineNo; }
            set { this._lineNo = value; }
        }

        [DataMember(Order=2)]
        public int? BillCount
        {
            get { return _billCounter; }
            set { this._billCounter = value; }
        }

        [DataMember(Order=3)]
        public decimal? Amount
        {
            get { return _amount; }
            set { this._amount = value; }
        }

        [DataMember(Order=4)]
        public string AgentId
        {
            get { return _agentId; }
            set { this._agentId = value; }
        }

        [DataMember(Order=5)]
        public string AreaCode
        {
            get { return _areaCode; }
            set { this._areaCode = value; }
        }

        [DataMember(Order=6)]
        public string Remark
        {
            get { return _remark; }
            set { this._remark = value; }
        }

        [DataMember(Order=7)]
        public string PrintTime
        {
            get { return _printTimeNo; }
            set { this._printTimeNo = value; }
        }


        [DataMember(Order=8)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }
        #endregion
    }
}
