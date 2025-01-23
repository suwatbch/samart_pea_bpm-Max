using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    public class AccountReceiveInfo : IListUtility<AccountReceiveInfo>
    {
        #region IListUtility<AccountReceiveInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public AccountReceiveInfo ParseExtract(string txt)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
