using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class EvaluateTotalBillInfo
    {
        #region "Variable Local"
        private string _branchId;
        private int? _totalBillKeepPersonRes = 0;
        private int? _totalBillKeepPersonSmall = 0;
        private int? _totalBillKeepPersonGov = 0;
        private int? _totalBillKeepCompanyRes = 0;
        private int? _totalBillKeepCompanySmall = 0;
        private int? _totalBillKeepCompanyGov = 0;
        private int? _totalBillPaste = 0;
        private int? _totalBillPaste3Month = 0;
        #endregion
        
        #region "Property"

        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=2)]
        public int? TotalBillKeepPersonResident
        {
            get { return _totalBillKeepPersonRes; }
            set { this._totalBillKeepPersonRes = value; }
        }

        [DataMember(Order=3)]
        public int? TotalBillKeepPersonSmallBiz
        {
            get { return _totalBillKeepPersonSmall; }
            set { this._totalBillKeepPersonSmall = value; }
        }

        [DataMember(Order=4)]
        public int? TotalBillKeepPersonGoverment
        {
            get { return _totalBillKeepPersonGov; }
            set { this._totalBillKeepPersonGov = value; }
        }

        [DataMember(Order=5)]
        public int? TotalBillKeepCompanyResident
        {
            get { return _totalBillKeepCompanyRes; }
            set { this._totalBillKeepCompanyRes = value; }
        }

        [DataMember(Order=6)]
        public int? TotalBillKeepCompanySmallBiz
        {
            get { return _totalBillKeepCompanySmall; }
            set { this._totalBillKeepCompanySmall = value; }
        }

        [DataMember(Order=7)]
        public int? TotalBillKeepCompanyGoverment
        {
            get { return _totalBillKeepCompanyGov; }
            set { this._totalBillKeepCompanyGov = value; }
        }

        [DataMember(Order=8)]
        public int? TotalBillPaste
        {
            get { return _totalBillPaste; }
            set { this._totalBillPaste = value; }
        }


        [DataMember(Order=9)]
        public int? TotalBillPaste3Month
        {
            get { return this._totalBillPaste3Month; }
            set { this._totalBillPaste3Month = value;}
        }
        #endregion
    }
}
