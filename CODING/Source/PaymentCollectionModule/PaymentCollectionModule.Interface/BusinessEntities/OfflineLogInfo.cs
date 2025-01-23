using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class OfflineLogInfo
    {
        private string _fileName;
        private string _errorMsg;
        private string _posId;
        private string _clientIp;
        private string _solved; //0 = success, 1 = error
        private string _runCashier;
        private string _branchId;
        private string _appFilePath;


        [DataMember(Order=1)]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }


        [DataMember(Order=2)]
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { _errorMsg = value; }
        }


        [DataMember(Order=3)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }


        [DataMember(Order=4)]
        public string ClientIP
        {
            get { return _clientIp; }
            set { _clientIp = value; }
        }


        [DataMember(Order=5)]
        public string Solved
        {
            get { return _solved; }
            set { _solved = value; }
        }


        [DataMember(Order=6)]
        public string RunCashier
        {
            get { return _runCashier; }
            set { _runCashier = value; }
        }


        [DataMember(Order=7)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }


        [DataMember(Order=8)]
        public string BranchName
        {
            get { return _appFilePath; }
            set { _appFilePath = value; }
        }
        
    }
}
