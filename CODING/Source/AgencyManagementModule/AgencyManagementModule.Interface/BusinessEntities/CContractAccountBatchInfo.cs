using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CContractAccountBatchInfo 
    {

        private string _caId;
        private string _caName;
        private string _caAddress;
        private string _techBranchId;
        private string _commBranchId;
        private decimal? _tsHelp;
        private decimal? _farLandHelp;
        private decimal? _extraMoneyHelp;
        private DateTime? _contractValidFrom;
        private DateTime? _contractValidTo;
        private string _classId;
        private string _ctId;
        private string _bpId;


        [DataMember(Order=1)]
        public string CaId
        {
            set { _caId = value; }
            get { return _caId; }
        }


        [DataMember(Order=2)]
        public string CaName
        {
            set { _caName = value; }
            get { return _caName; }
        }


        [DataMember(Order=3)]
        public string CaAddress
        {
            set { _caAddress = value; }
            get { return _caAddress; }
        }


        [DataMember(Order=4)]
        public string TechBranchId
        {
            set { _techBranchId = value; }
            get { return _techBranchId; }
        }


        [DataMember(Order=5)]
        public string CommBranchId
        {
            set { _commBranchId = value; }
            get { return _commBranchId; }
        }


        [DataMember(Order=6)]
        public decimal? TransportHelp
        {
            set { _tsHelp = value; }
            get { return _tsHelp; }
        }


        [DataMember(Order=7)]
        public decimal? FarLandHelp
        {
            set { _farLandHelp = value; }
            get { return _farLandHelp; }
        }


        [DataMember(Order=8)]
        public decimal? ExtraMoneyHelp
        {
            set { _extraMoneyHelp = value; }
            get { return _extraMoneyHelp; }
        }


        [DataMember(Order=9)]
        public DateTime? ContractValidFrom
        {
            set { _contractValidFrom = value ; }
            get { return _contractValidTo; }
        }


        [DataMember(Order=10)]
        public DateTime? ContractValidTo
        {
            set { _contractValidTo = value; }
            get { return _contractValidTo; }
        }


        [DataMember(Order=11)]
        public string AccountClassId
        {
            set { _classId = value; }
            get { return _classId; }
        }


        [DataMember(Order=12)]
        public string CtId
        {
            set { _ctId = value; }
            get { return _ctId; }
        }


        [DataMember(Order=13)]
        public string BpId
        {
            set { _bpId = value; }
            get { return _bpId; }
        }


    }
}
