using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using System.Collections;
using PEA.BPM.AgencyManagementModule.DA;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PEA.BPM.AgencyManagementModule.BS
{
    public class AgencyModuleConfigService: IAgencyModuleConfigService
    {
        public AgencyModuleConfigService()
        {

        }

        public FeeBaseElement GetBaseCommissionRate(string branchId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            FeeBaseElement feeBase = null;
            try
            {
                feeBase = agentDa.GetBaseCommissionRate(branchId);
                
            }
            catch (Exception e)
            {
                
                throw;
            }

            return feeBase;
        }

        private bool UpdateCommissionRateTransaction(DbTransaction trans, FeeBaseElement feeBase)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();

            try
            {
                agentDa.UpdateCommissionRate(trans, feeBase);
                
            }
            catch (Exception e)
            {
                
                throw;
            }

            return true;
        }

        public bool UpdateCommissionRate(FeeBaseElement feeBase)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    bool ret = UpdateCommissionRateTransaction(trans, feeBase);
                    trans.Commit();
                    return ret;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw;
                }
            }

        }
    }
}
