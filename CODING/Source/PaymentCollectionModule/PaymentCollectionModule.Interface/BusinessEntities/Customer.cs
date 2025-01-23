using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class Customer
    {
        private string _customerId;
        private string _name;
        private string _address;
        private string _branchId;
        private string _techBranchName;
        private string _commBranchId;
        private string _commBranchName;
        private string _ctId;
        private string _accountClassId;
        private decimal? _securityDeposit;
        private string _meterSizeId;
        private string _meterSizeName;
        private string _contractTypeId;
        private string _controllerId;
        private string _controllerName;
        private string _mruId;
        private string _caTaxId;
        private string _caTaxBranch;

        [DataMember(Order=1)]
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }


        [DataMember(Order=2)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        [DataMember(Order=3)]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }


        [DataMember(Order=4)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        [DataMember(Order = 5)]
        public string TechBranchName
        {
            get { return _techBranchName; }
            set { _techBranchName = value; }
        }

        [DataMember(Order = 6)]
        public string CommBranchId
        {
            get { return _commBranchId; }
            set { _commBranchId = value; }
        }

        [DataMember(Order = 7)]
        public string CommBranchName
        {
            get { return _commBranchName; }
            set { _commBranchName = value; }
        }


        [DataMember(Order=8)]
        public string CtId
        {
            get { return _ctId; }
            set { _ctId = value; }
        }


        [DataMember(Order=9)]
        public string AccountClassId
        {
            get { return _accountClassId; }
            set { _accountClassId = value; }
        }


        [DataMember(Order=10)]
        public decimal? SecurityDeposit
        {
            get { return _securityDeposit; }
            set { _securityDeposit = value; }
        }


        [DataMember(Order=11)]
        public string MeterSizeId
        {
            get { return _meterSizeId; }
            set { _meterSizeId = value; }
        }


        [DataMember(Order=12)]
        public string MeterSizeName
        {
            get { return _meterSizeName; }
            set { _meterSizeName = value; }
        }


        [DataMember(Order=13)]
        public string ContractTypeId
        {
            get { return _contractTypeId; }
            set { _contractTypeId = value; }
        }

        [DataMember(Order = 14)]
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }

        [DataMember(Order = 15)]
        public string ControllerName
        {
            get { return _controllerName; }
            set { _controllerName = value; }
        }

        [DataMember(Order = 16)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }

        [DataMember(Order = 17)]
        public string CaTaxId
        {
            get { return _caTaxId; }
            set { _caTaxId = value; }
        }

        [DataMember(Order = 18)]
        public string CaTaxBranch
        {
            get { return _caTaxBranch; }
            set { _caTaxBranch = value; }
        }
    }
}
