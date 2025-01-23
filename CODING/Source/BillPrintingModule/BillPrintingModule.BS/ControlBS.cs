using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using PEA.BPM.BillPrintingModule.DA;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.BillPrintingModule.Interface.Services;

namespace PEA.BPM.BillPrintingModule.BS
{
    public class ControlBS : IControlServices
    {
        public List<DeliveryPlace> GetDeliveryPlace(string createBranchId)
        {
            ControlDA da = new ControlDA();
            return da.GetDeliveryPlace(createBranchId);
        }

        public List<AuthorizedPerson> GetApprover(string createBranchId)
        {
            ControlDA da = new ControlDA();
            return da.GetApprover(createBranchId);
        }

        public List<Bank> GetBank(string branchId)
        {
            ControlDA da = new ControlDA();
            return da.GetBank(branchId);
        }

        public string InsertDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            string dpId = string.Empty;
            dp.IdPrefix = Session.Branch.PostedServerId;

            if (trans == null)
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    try
                    {
                        ControlDA da = new ControlDA();
                        dp.IdPrefix = Session.Branch.PostedServerId;
                        dpId = da.InsertDeliveryPlace(trans, dp);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        // Write Log
                        // ...
                        //
                        throw ex;
                    }
                }
            }
            else
            {
                ControlDA da = new ControlDA();
                dpId = da.InsertDeliveryPlace(trans, dp);
            }

            return dpId;
        }

        public string InsertApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            string dpId = string.Empty;

            if (trans == null)
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    try
                    {
                        ControlDA da = new ControlDA();
                        approver.IdPrefix = Session.Branch.PostedServerId;
                        dpId = da.InsertApprover(trans, approver);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        // Write Log
                        // ...
                        //
                        throw ex;
                    }
                }
            }
            else
            {
                ControlDA da = new ControlDA();
                dpId = da.InsertApprover(trans, approver);
            }

            return dpId;
        }

        public string UpdateDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            string dpId = string.Empty;

            if (trans == null)
            {

                Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    try
                    {
                        ControlDA da = new ControlDA();
                        dpId = da.UpdateDeliveryPlace(trans, dp);

                        trans.Commit();

                        return dpId;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        // Write Log
                        // ...
                        //
                        throw ex;
                    }
                }
            }
            else
            {
                ControlDA da = new ControlDA();
                dpId = da.UpdateDeliveryPlace(trans, dp);
            }

            return dpId;
        }

        public string UpdateApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            string dpId = string.Empty;

            if (trans == null)
            {

                Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    try
                    {
                        ControlDA da = new ControlDA();
                        dpId = da.UpdateApprover(trans, approver);

                        trans.Commit();

                        return dpId;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        // Write Log
                        // ...
                        //
                        throw ex;
                    }
                }
            }
            else
            {
                ControlDA da = new ControlDA();
                dpId = da.UpdateApprover(trans, approver);
            }

            return dpId;
        }

        public string DeleteDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            string dpId = string.Empty;

            if (trans == null)
            {

                Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    try
                    {
                        ControlDA da = new ControlDA();
                        dpId = da.DeleteDeliveryPlace(trans, dp);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        // Write Log
                        // ...
                        //
                        throw ex;
                    }
                }
            }
            else
            {
                ControlDA da = new ControlDA();
                dpId = da.DeleteDeliveryPlace(trans, dp);
            }

            return dpId;
        }


        public string DeleteApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            string dpId = string.Empty;

            if (trans == null)
            {

                Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    try
                    {
                        ControlDA da = new ControlDA();
                        dpId = da.DeleteApprover(trans, approver);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        // Write Log
                        // ...
                        //
                        throw ex;
                    }
                }
            }
            else
            {
                ControlDA da = new ControlDA();
                dpId = da.DeleteApprover(trans, approver);
            }

            return dpId;
        }

        public List<String> GetChildBranch(string branchId)
        {
            throw new Exception("This function is obsolete");
        }

        public List<Portion> GetPortion(string branchId)
        {
            ControlDA da = new ControlDA();
            return da.GetPortion(branchId);
        }

        public List<Invoice> GetContractAccountHistory(string caId, string printBranch)
        {
            ControlDA da = new ControlDA();
            return da.GetContractAccountHistory(caId, printBranch);
        }

        public List<BarcodeMRU> GetBarcodeMRU(ManageBarcodeParam param)
        {
            ControlDA da = new ControlDA();
            return da.GetBarcodeMRU(param);
        }

        public void UpdateBarcodeMRU(BarcodeMRU param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    ControlDA da = new ControlDA();
                    string reset = "1";
                    foreach (BarcodeMRU obj in param.List)
                    {
                        obj.ModifiedBy = param.ModifiedBy;
                        da.UpdateBarcodeMRU(trans, obj, reset);
                        reset = "0";
                    }

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

    }
}
