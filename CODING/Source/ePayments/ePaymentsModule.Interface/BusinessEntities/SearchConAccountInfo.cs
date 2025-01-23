using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class SearchConAccountInfo
    {
        private string _branchId;
        private string _caName;
        private string _caAddress;
        private string _accountClassId;
        private string _accountClassName;
        private string _meterSizeName;
        private string _meterInstallDt;



        [DataMember(Order=1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=2)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }


        [DataMember(Order=3)]
        public string CaAddress
        {
            get { return this._caAddress; }
            set { this._caAddress = value; }
        }


        [DataMember(Order=4)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }


        [DataMember(Order=5)]
        public string AccountClassName
        {
            get { return this._accountClassName; }
            set { this._accountClassName = value; }
        }


        [DataMember(Order=6)]
        public string MeterSizeName
        {
            get { return this._meterSizeName; }
            set { this._meterSizeName = value; }
        }


        [DataMember(Order=7)]
        public string MeterInstallDt
        {
            get { return this._meterInstallDt; }
            set { this._meterInstallDt = value; }
        }


 
    }

}
