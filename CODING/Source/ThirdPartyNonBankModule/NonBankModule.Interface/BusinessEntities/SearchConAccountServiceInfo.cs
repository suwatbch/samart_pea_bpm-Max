using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.NonBankModule.Interface.BusinessEntities
{
    [DataContract]
    public class SearchConAccountServiceInfo
    {

        private string _branchId;
        private string _branchName;
        private string _caName;
        private string _caAddress;
        private string _accountClassId;
        private string _accountClassName;
        private string _meterSizeName;
        private string _meterInstallDt;
        private string _remark;
       
        [DataMember(Order = 1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order = 2)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }

        [DataMember(Order=3)]        
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }

        [DataMember(Order=4)]
        public string CaAddress
        {
            get { return this._caAddress; }
            set { this._caAddress = value; }
        }

        [DataMember(Order=5)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }

        [DataMember(Order=6)]
        public string AccountClassName
        {
            get { return this._accountClassName; }
            set { this._accountClassName = value; }
        }

        [DataMember(Order=7)]
        public string MeterSizeName
        {
            get { return this._meterSizeName; }
            set { this._meterSizeName = value; }
        }

        [DataMember(Order = 8)]
        public string MeterInstallDt
        {
            get { return this._meterInstallDt; }
            set { this._meterInstallDt = value; }
        }

        [DataMember(Order = 9)]
        public string Remark
        {
            get { return this._remark; }
            set { this._remark = value; }
        }  
    }
}
