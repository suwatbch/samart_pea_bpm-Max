using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract,Serializable]
    public class ContractAccountOffline
    {
        private string _caId;

        [DataMember(Order=1)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        private string _caName;

        [DataMember(Order=2)]
        public string CaName
        {
            get { return _caName; }
            set { _caName = value; }
        }

        private string _caTaxId;

        [DataMember(Order=3)]
        public string CaTaxId
        {
            get { return _caTaxId; }
            set { _caTaxId = value; }
        }

        private string _caTaxBranch;

        [DataMember(Order=4)]
        public string CaTaxBranch
        {
            get { return _caTaxBranch; }
            set { _caTaxBranch = value; }
        }

        private string _caAddress;

        [DataMember(Order = 5)]
        public string CaAddress
        {
            get { return _caAddress; }
            set { _caAddress = value; }
        }

        private string _techBranchId;

        [DataMember(Order = 6)]
        public string TechBranchId
        {
            get { return _techBranchId; }
            set { _techBranchId = value; }
        }

        public ContractAccountOffline(string CaId, string CaName, string CaTaxId, string CaTaxBranch, string CaAddress, string TechBranchId)
        {
            this._caId = CaId;
            this._caName = CaName;
            this._caTaxId = CaTaxId;
            this._caTaxBranch = CaTaxBranch;
            this._caAddress = CaAddress;
            this._techBranchId = TechBranchId;
        }

        #region IListItem Members

        #endregion
    }
}
