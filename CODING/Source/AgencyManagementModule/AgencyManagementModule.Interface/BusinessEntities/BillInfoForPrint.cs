using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillInfoForPrint
    {
        #region "Local Variable"
        private string _branchId;
        private string _mruId;
        private int? _billCount = 0;
        private decimal? _amount = 0;
        private string _remark;
        #endregion

        #region "Property"

        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId;}
            set { this._branchId = value; }
        }

        [DataMember(Order=2)]
        public string MruId
        {
            get { return _mruId; }
            set { this._mruId = value; }
        }

        [DataMember(Order=3)]
        public int? BillCount
        {
            get { return _billCount; }
            set { this._billCount = value; }
        }

        [DataMember(Order=4)]
        public decimal? Amount
        {
            get { return _amount; }
            set { this._amount = value; }
        }

        [DataMember(Order=5)]
        public string Remark
        {
            get { return _remark; }
            set { this._remark = value; }
        }
        #endregion
    }
}
