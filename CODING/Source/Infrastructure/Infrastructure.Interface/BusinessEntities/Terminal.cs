using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class Terminal
    {
        private string _posId;
        private string _posCode;
        private string _branchId;
        private string _taxCode;
        private string _ip;

        public Terminal(string posId, string posCode, string branchId, string taxCode, string ip)
        {
            this._posId = posId;
            this._posCode = posCode;
            this._branchId = branchId;
            this._taxCode = taxCode;
            this._ip = ip;
        }


        [DataMember(Order=1)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }


        [DataMember(Order=2)]
        public string PosCode
        {
            get { return _posCode; }
            set { _posCode = value; }
        }


        [DataMember(Order=3)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=4)]
        public string TaxCode
        {
            get { return _taxCode; }
            set { _taxCode = value; }
        }


        [DataMember(Order=5)]
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
    }
}
