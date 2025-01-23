using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{

    [DataContract]
    public class EvaluateAgencyReportCondition
    {
        #region "Local Variable"
        private int _branchType;
        private string _branchId;
        private string _branchFrom;
        private string _branchTo;

        private string _periodFrom;
        private string _periodTo;

        private bool _printPreview;
        private string _baCode;       
        #endregion


        #region "Properties"

        [DataMember(Order=1)]
        public int BranchType
        {
            get { return this._branchType; }
            set { this._branchType = value; }
        }

        [DataMember(Order=2)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order=3)]
        public string BranchFrom
        {
            get { return this._branchFrom; }
            set { this._branchFrom = value; }
        }

        [DataMember(Order=4)]
        public string BranchTo
        {
            get { return this._branchTo; }
            set { this._branchTo = value; }
        }


        [DataMember(Order=5)]
        public string PeriodFrom
        {
            get { return this._periodFrom; }
            set { this._periodFrom = value; }
        }


        [DataMember(Order=6)]
        public string PeriodTo
        {
            get { return this._periodTo; }
            set { this._periodTo = value; }
        }


        [DataMember(Order=7)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }


        [DataMember(Order=8)]
        public string BaCode
        {
            get { return this._baCode; }
            set { this._baCode = value; }
        }
        #endregion


        //#region "Local Variable"
        //private string _peaStartCode;
        //private string _peaEndCode;
        //private string _conditionType;
        //private string _conditionPeriod;
        //private string _conditionYear;
        //#endregion

        //#region "Property"
        //public string PEAStartCode
        //{
        //    get { return _peaStartCode; }
        //    set { this._peaStartCode = value; }
        //}
        //public string PEAEndCode
        //{
        //    get { return _peaEndCode; }
        //    set { this._peaEndCode = value; }
        //}
        //public string ConditionType
        //{
        //    get { return _conditionType; }
        //    set {this._conditionType = value;}
        //}
        //public string ConditionPeriod
        //{
        //    get { return _conditionPeriod; }
        //    set { this._conditionPeriod = value; }
        //}
        //public string ConditionYear
        //{
        //    get { return _conditionYear; }
        //    set { this._conditionYear = value; }
        //}
        //#endregion
    }
}
