using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.SG;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule.Services
{
    [Service(typeof(IAgencyModuleConfigService))]
    public class AgencyModuleConfigServiceSwitcher : IAgencyModuleConfigService
    {
        public AgencyModuleConfigServiceSwitcher()
		{
        }

        #region Service Factory
        public IAgencyModuleConfigService GetService()
        {
            return GetService(false);
        }

        public IAgencyModuleConfigService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new AgencyModuleConfigSG(true);
            }
            else
            {
                return new AgencyModuleConfigSG(false);
            }
        }
        #endregion


        #region IAgencyModuleConfigService Members

        public FeeBaseElement GetBaseCommissionRate(string branchId)
        {
            return GetService().GetBaseCommissionRate(branchId);
        }

        public bool UpdateCommissionRate(FeeBaseElement feeBase)
        {
            return GetService().UpdateCommissionRate(feeBase);
        }

        #endregion
    }
}
