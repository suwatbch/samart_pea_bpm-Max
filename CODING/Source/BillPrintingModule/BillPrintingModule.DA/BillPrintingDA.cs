using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace PEA.BPM.BillPrintingModule.DA
{
    public class BillPrintingDA
    {
        public const int CMD_TIMEOUT = 360;
        private const int LOT_SIZE = 500;

        #region CheckExist (New)

        #region For menu 5.1,5.2

        //5.1
        public List<PrintableId> CheckPrintableBlueMass(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableBlueMass");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                //db.AddInParameter(cmd, "PrintType", DbType.String, param.BillType);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
                db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
                db.AddInParameter(cmd, "ToMruId", DbType.String, param.ToMruId);
                db.AddInParameter(cmd, "PrintAll", DbType.Int16,
                                  param.BillPrintingCondition == (int) PrintingCondition.AllPrinting ? 1 : 0);
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0 && param.BillPrintingCondition != (int) PrintingCondition.AllPrinting)
                {
                    //no data found
                    PrintableId pt = new PrintableId();
                    pt.BranchId = param.BranchId;
                    pt.MruId = param.MruId;
                    pt.PrintingStatus = 2;
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.BranchId = DaHelper.GetString(dr, "BranchId");
                        pt.MruId = DaHelper.GetString(dr, "MruId");
                        //pt.CaId = DaHelper.GetString(dr, "CaId") == null ? null : DaHelper.GetString(dr, "CaId").TrimStart('0');
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        public List<PrintableId> CheckPrintableBlueCaId(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableBlueCaId");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    //no data found.
                    PrintableId pt = new PrintableId();
                    pt.BranchId = param.BranchId;
                    pt.MruId = param.MruId;
                    pt.CaId = param.UserId.TrimStart('0');
                    pt.PrintingStatus = 2;
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.BranchId = DaHelper.GetString(dr, "BranchId");
                        pt.MruId = DaHelper.GetString(dr, "MruId");
                        pt.CaId = DaHelper.GetString(dr, "CaId").TrimStart('0');
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        public List<PrintableId> CheckPrintableSpotBillMass(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableSpotBillMass");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                //db.AddInParameter(cmd, "PrintType", DbType.String, param.BillType);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
                db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
                db.AddInParameter(cmd, "ToMruId", DbType.String, param.ToMruId);
                db.AddInParameter(cmd, "PrintAll", DbType.Int16,
                                  param.BillPrintingCondition == (int) PrintingCondition.AllPrinting ? 1 : 0);
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0 && param.BillPrintingCondition != (int) PrintingCondition.AllPrinting)
                {
                    //no data found
                    PrintableId pt = new PrintableId();
                    pt.BranchId = param.BranchId;
                    pt.MruId = param.MruId;
                    pt.PrintingStatus = 2;
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.BranchId = DaHelper.GetString(dr, "BranchId");
                        pt.MruId = DaHelper.GetString(dr, "MruId");
                        //pt.CaId = DaHelper.GetString(dr, "CaId") == null ? null : DaHelper.GetString(dr, "CaId").TrimStart('0');
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        public List<PrintableId> CheckPrintableSpotBillCaId(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableSpotBillCaId");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    //no data found.
                    PrintableId pt = new PrintableId();
                    pt.BranchId = param.BranchId;
                    pt.MruId = param.MruId;
                    pt.CaId = param.UserId.TrimStart('0');
                    pt.PrintingStatus = 2;
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.BranchId = DaHelper.GetString(dr, "BranchId");
                        pt.MruId = DaHelper.GetString(dr, "MruId");
                        pt.CaId = DaHelper.GetString(dr, "CaId").TrimStart('0');
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        public List<PrintableId> CheckPrintableGreenMass(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableGreenMass");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
                db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
                db.AddInParameter(cmd, "ToMruId", DbType.String, param.ToMruId);
                db.AddInParameter(cmd, "PrintAll", DbType.Int16,
                                  param.BillPrintingCondition == (int) PrintingCondition.AllPrinting ? 1 : 0);
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0 && param.BillPrintingCondition != (int) PrintingCondition.AllPrinting)
                {
                    //no data found.
                    PrintableId pt = new PrintableId();
                    pt.BranchId = param.BranchId;
                    pt.MruId = param.MruId;
                    pt.PrintingStatus = 2;
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.BranchId = DaHelper.GetString(dr, "BranchId");
                        pt.MruId = DaHelper.GetString(dr, "MruId");
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        public List<PrintableId> CheckPrintableGreenCaId(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableGreenCaId");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    //no data found.
                    PrintableId pt = new PrintableId();
                    pt.BranchId = param.BranchId;
                    pt.MruId = param.MruId;
                    pt.CaId = param.UserId.TrimStart('0');
                    pt.PrintingStatus = 2;
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.BranchId = DaHelper.GetString(dr, "BranchId");
                        pt.MruId = DaHelper.GetString(dr, "MruId");
                        pt.CaId = DaHelper.GetString(dr, "CaId").TrimStart('0');
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        //5.2
        public List<PrintableId> CheckPrintableA4Mass(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableA4Mass");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
                db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
                db.AddInParameter(cmd, "ToMruId", DbType.String, param.ToMruId);
                db.AddInParameter(cmd, "PrintAll", DbType.Int16,
                                  param.BillPrintingCondition == (int) PrintingCondition.AllPrinting ? 1 : 0);
                //choose only un-printed(0) or choose all(1)
                db.AddInParameter(cmd, "A4Reprint", DbType.Int16, param.A4Reprint);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0 && param.BillPrintingCondition != (int) PrintingCondition.AllPrinting)
                {
                    //no data found.
                    PrintableId pt = new PrintableId();
                    pt.BranchId = param.BranchId;
                    pt.MruId = param.MruId;
                    pt.PrintingStatus = 2;
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.BranchId = DaHelper.GetString(dr, "BranchId");
                        pt.MruId = DaHelper.GetString(dr, "MruId");
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        public List<PrintableId> CheckPrintableA4CaId(string printBranch, string billPred, string fromNo, string toNo, int? a4Reprint)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableA4CaId");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
                db.AddInParameter(cmd, "BillPred", DbType.String, billPred);
                db.AddInParameter(cmd, "FromNo", DbType.String, fromNo);
                db.AddInParameter(cmd, "ToNo", DbType.String, toNo);
                db.AddInParameter(cmd, "A4Reprint", DbType.Int16, a4Reprint);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.BranchId = DaHelper.GetString(dr, "BranchId");
                        pt.MruId = DaHelper.GetString(dr, "MruId");
                        pt.CaId = DaHelper.GetString(dr, "CaId").PadLeft(12, '0');
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
                else
                {
                    //no data found
                    PrintableId pt = new PrintableId();
                    pt.BranchId = "";
                    pt.MruId = "";

                    if (string.IsNullOrEmpty(toNo))
                        pt.CaId = fromNo;
                    else
                        pt.CaId = fromNo + "-" + toNo;

                    pt.PrintingStatus = 2;
                    _printId.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        public List<PrintableId> CheckPrintableA4BillSeq(string printBranch, string fromNo, string toNo)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableA4BillSeq");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, printBranch);
                db.AddInParameter(cmd, "FromNo", DbType.String, fromNo);
                db.AddInParameter(cmd, "ToNo", DbType.String, toNo);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.BranchId = DaHelper.GetString(dr, "BranchId");
                        pt.MruId = DaHelper.GetString(dr, "MruId");
                        pt.CaId = DaHelper.GetString(dr, "CaId").PadLeft(7, '0');
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
                else
                {
                    //no data found
                    PrintableId pt = new PrintableId();
                    pt.BranchId = "";
                    pt.MruId = "";
                    pt.CaId = fromNo + toNo == null ? "" : "-" + toNo;
                    pt.PrintingStatus = 2;
                    _printId.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        #endregion

        #region For menu 5.3

        //5.3
        public List<PrintableId> CheckRePrintableBlueCaId(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = null;

                cmd = db.GetStoredProcCommand("bp_sel_rePrintableBlueCaId");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "FromNoText", DbType.String,
                                  param.FromNumberId == null ? null : param.FromNumberId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "ToNoText", DbType.String,
                                  param.ToNumberId == null ? null : param.ToNumberId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                if (param.BillPrintingCondition == (int) PrintingCondition.BillSeqPrinting)
                    db.AddInParameter(cmd, "ParamType", DbType.String, "1");

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PrintableId pt = new PrintableId();
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.MruId = DaHelper.GetString(dr, "MruId");
                    pt.CaId = DaHelper.GetString(dr, "CaId");
                    if (!string.IsNullOrEmpty(pt.CaId))
                    {
                        if (pt.CaId.Length > 7)
                            pt.CaId = pt.CaId.TrimStart('0');
                        else
                            pt.CaId = pt.CaId.PadLeft(7, '0');
                    }
                    pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                    pt.BillPeriod = DaHelper.GetString(dr, "BillPred");
                    _printId.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _printId;
        }

        public List<PrintableId> CheckRePrintableBlueMass(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = null;

                //int printType = 1;

                cmd = db.GetStoredProcCommand("bp_sel_rePrintableBlueMass");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
                db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
                db.AddInParameter(cmd, "ToMruId", DbType.String, param.ToMruId);
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PrintableId pt = new PrintableId();
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.MruId = DaHelper.GetString(dr, "MruId");
                    pt.CaId = DaHelper.GetString(dr, "CaId") == null
                                  ? null
                                  : DaHelper.GetString(dr, "CaId").TrimStart('0');
                    pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                    //pt.BillPeriod = DaHelper.GetString(dr, "BillPred");
                    _printId.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _printId;
        }

        public List<PrintableId> CheckRePrintableSpotBillCaId(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = null;

                cmd = db.GetStoredProcCommand("bp_sel_rePrintableSpotBillCaId");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "FromNoText", DbType.String,
                                  param.FromNumberId == null ? null : param.FromNumberId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "ToNoText", DbType.String,
                                  param.ToNumberId == null ? null : param.ToNumberId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                if (param.BillPrintingCondition == (int) PrintingCondition.BillSeqPrinting)
                    db.AddInParameter(cmd, "ParamType", DbType.String, "1");

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PrintableId pt = new PrintableId();
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.MruId = DaHelper.GetString(dr, "MruId");

                    pt.CaId = DaHelper.GetString(dr, "CaId");
                    if (!string.IsNullOrEmpty(pt.CaId))
                    {
                        if (pt.CaId.Length > 7)
                            pt.CaId = pt.CaId.TrimStart('0');
                        else
                            pt.CaId = pt.CaId.PadLeft(7, '0');
                    }

                    pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                    pt.BillPeriod = DaHelper.GetString(dr, "BillPred");
                    _printId.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _printId;
        }

        public List<PrintableId> CheckRePrintableSpotBillMass(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = null;

                //int printType = 1;

                cmd = db.GetStoredProcCommand("bp_sel_rePrintableSpotBillMass");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
                db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
                db.AddInParameter(cmd, "ToMruId", DbType.String, param.ToMruId);
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PrintableId pt = new PrintableId();
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.MruId = DaHelper.GetString(dr, "MruId");
                    pt.CaId = DaHelper.GetString(dr, "CaId") == null
                                  ? null
                                  : DaHelper.GetString(dr, "CaId").TrimStart('0');
                    pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                    //pt.BillPeriod = DaHelper.GetString(dr, "BillPred");
                    _printId.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _printId;
        }

        public List<PrintableId> CheckRePrintableGreenMass(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = null;

                //green bill
                //int printType = 2;

                cmd = db.GetStoredProcCommand("bp_sel_rePrintableGreenMass");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
                db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
                db.AddInParameter(cmd, "ToMruId", DbType.String, param.ToMruId);
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PrintableId pt = new PrintableId();
                    pt.BillPeriod = DaHelper.GetString(dr, "BillPred");
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.MruId = DaHelper.GetString(dr, "MruId");
                    //pt.CaId = DaHelper.GetString(dr, "CaId") == null ? null : DaHelper.GetString(dr, "CaId").TrimStart('0');
                    pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                    _printId.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _printId;
        }

        public List<PrintableId> CheckRePrintableGreenCaId(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = null;

                //green bill
                //int printType = 2;

                cmd = db.GetStoredProcCommand("bp_sel_rePrintableGreenCaId");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "FromNoText", DbType.String,
                                  param.FromNumberId == null ? null : param.FromNumberId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "ToNoText", DbType.String,
                                  param.ToNumberId == null ? null : param.ToNumberId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);

                if (param.BillPrintingCondition == (int) PrintingCondition.BillSeqPrinting)
                    db.AddInParameter(cmd, "ParamType", DbType.String, "1");

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PrintableId pt = new PrintableId();
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.MruId = DaHelper.GetString(dr, "MruId");
                    pt.CaId = DaHelper.GetString(dr, "CaId");
                    if (!string.IsNullOrEmpty(pt.CaId))
                    {
                        if (pt.CaId.Length > 7)
                            pt.CaId = pt.CaId.TrimStart('0');
                        else
                            pt.CaId = pt.CaId.PadLeft(7, '0');
                    }

                    pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                    pt.BillPeriod = DaHelper.GetString(dr, "BillPred");
                    _printId.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _printId;
        }

        #endregion

        #region For menu 5.4,5.5

        //5.4
        public List<PrintableId> CheckPrintableGrpInv(GroupInvoiceParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableGrpInv");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "MtNo", DbType.String, param.MtNo);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    PrintableId pt = new PrintableId();
                    pt.MtNo = param.MtNo;
                    pt.GrvInvFlag = "A";
                    pt.PrintingStatus = 2; // in case where no data are found.
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.MtNo = DaHelper.GetString(dr, "MtNo").Trim('%');
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.GrvInvFlag = "A"; //DaHelper.GetString(dr, "BillFlag");
                        pt.BillCount = DaHelper.GetInt(dr, "BillCount");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        //5.5
        public List<PrintableId> CheckRePrintableGrpInvCaId(GroupInvoiceParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_rePrintableGrpInvCaId");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "PaymentDueDate", DbType.String, param.PaymentDueDate);
                db.AddInParameter(cmd, "FromCaId", DbType.String,
                                  param.FromCaId == null ? null : param.FromCaId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "ToCaId", DbType.String,
                                  param.ToCaId == null ? null : param.ToCaId.PadLeft(12, '0'));


                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    PrintableId pt = new PrintableId();
                    pt.CaId = param.ToCaId + "-" + param.FromCaId;
                    //pt.MtNo = param.MtNo;
                    pt.GrvInvFlag = "A";
                    pt.PrintingStatus = 2; // in case where no data are found.                    
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.CaId = DaHelper.GetString(dr, "CaId");
                        //pt.MtNo = DaHelper.GetString(dr, "MtNo");
                        //pt.MtNo = pt.MtNo != null ? pt.MtNo.Trim('%') : null;
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.GrvInvFlag = "A"; //DaHelper.GetString(dr, "BillFlag");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        public List<PrintableId> CheckRePrintableGrpInvMtNo(GroupInvoiceParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_rePrintableGrpInvMtNo");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "MtNo", DbType.String, param.MtNo);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    PrintableId pt = new PrintableId();
                    //pt.CaId = param.ToCaId + "-" + param.FromCaId;
                    pt.MtNo = param.MtNo;
                    pt.GrvInvFlag = "A";
                    pt.PrintingStatus = 2; // in case where no data are found.                    
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.CaId = pt.CaId == null ? null : pt.CaId.TrimStart('0');
                        pt.MtNo = DaHelper.GetString(dr, "MtNo");
                        pt.MtNo = pt.MtNo != null ? pt.MtNo.Trim('%') : null;
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.GrvInvFlag = "A"; //DaHelper.GetString(dr, "BillFlag");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        #endregion

        #region For menu 5.6,5.7

        //5.6
        public List<PrintableIdByBank> CheckPrintableGreenBank(GreenBillParam param)
        {
            List<PrintableIdByBank> printId = null;

            try
            {
                List<string> invoices = new List<string>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableGreenBank");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BankId", DbType.String, param.BankId);
                db.AddInParameter(cmd, "BankDueDate", DbType.String, param.BankDueDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    invoices.Add(DaHelper.GetString(dr, "BranchId").Substring(0, 4) + DaHelper.GetString(dr, "PrintDoc"));
                }

                printId = PartitionLot(invoices, param.BillType.Value, param.BankDueDate, param.BankId);
            }
            catch (Exception)
            {
                throw;
            }

            return printId;
        }

        public List<PrintableIdByBank> CheckPrintableBlueBank(GreenBillParam param)
        {
            List<PrintableIdByBank> printId = null;

            try
            {
                List<string> invoices = new List<string>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableBlueBank");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BankId", DbType.String, param.BankId);
                db.AddInParameter(cmd, "BankDueDate", DbType.String, param.BankDueDate);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow dr in dt.Rows)
                    invoices.Add(DaHelper.GetString(dr, "InvoiceNo"));

                printId = PartitionLot(invoices, param.BillType.Value, param.BankDueDate, param.BankId);
            }
            catch (Exception)
            {
                throw;
            }

            return printId;
        }

        //5.7
        public List<PrintableIdByBank> CheckRePrintableGreenBank(string bankId, int billType,
                                                                 string dueDate, string fromId,
                                                                 string toId, string commBranch,
                                                                 string billPred)
        {
            List<PrintableIdByBank> _printId = new List<PrintableIdByBank>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_rePrintableGreenBank");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, commBranch);
                db.AddInParameter(cmd, "BankDueDate", DbType.String, dueDate);
                db.AddInParameter(cmd, "BillPred", DbType.String, billPred);
                db.AddInParameter(cmd, "BankId", DbType.String, bankId);
                db.AddInParameter(cmd, "CaFrom", DbType.String, fromId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "CaTo", DbType.String, toId.PadLeft(12, '0'));

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableIdByBank pt = new PrintableIdByBank();
                        //pt.FromCaId = DaHelper.GetString(dr, "CaFrom").TrimStart('0');
                        //pt.ToCaId = DaHelper.GetString(dr, "CaIdTo").TrimStart('0');                        
                        pt.CaId = DaHelper.GetString(dr, "CaId");
                        pt.DueDate = DaHelper.GetString(dr, "BankDueDate");
                        pt.BankId = DaHelper.GetString(dr, "BankId");
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.BillType = billType;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        public List<PrintableIdByBank> CheckRePrintableBlueBank(string bankId, int billType,
                                                                string dueDate, string fromId,
                                                                string toId, string commBranch,
                                                                string billPred)
        {
            List<PrintableIdByBank> _printId = new List<PrintableIdByBank>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_rePrintableBlueBank");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, commBranch);
                db.AddInParameter(cmd, "BankDueDate", DbType.String, dueDate);
                db.AddInParameter(cmd, "BillPred", DbType.String, billPred);
                //db.AddInParameter(cmd, "BillType", DbType.String, billType);
                db.AddInParameter(cmd, "BankId", DbType.String, bankId);
                db.AddInParameter(cmd, "CaFrom", DbType.String, fromId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "CaTo", DbType.String, toId.PadLeft(12, '0'));

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableIdByBank pt = new PrintableIdByBank();
                        //pt.FromCaId = DaHelper.GetString(dr, "CaFrom").TrimStart('0');
                        //pt.ToCaId = DaHelper.GetString(dr, "CaIdTo").TrimStart('0');                        
                        pt.CaId = DaHelper.GetString(dr, "CaId");
                        pt.DueDate = DaHelper.GetString(dr, "BankDueDate");
                        pt.BankId = DaHelper.GetString(dr, "BankId");
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.BillType = billType;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        #endregion

        #region For menu 5.8, 5.9

        public List<PrintableId> CheckPrintableGreenReceiptMass(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableGreenReceiptMass");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
                db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
                db.AddInParameter(cmd, "ToMruId", DbType.String, param.ToMruId);
                db.AddInParameter(cmd, "PrintAll", DbType.Int16,
                                  param.BillPrintingCondition == (int) PrintingCondition.AllPrinting ? 1 : 0);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0 && param.BillPrintingCondition != (int) PrintingCondition.AllPrinting)
                {
                    //no data found.
                    PrintableId pt = new PrintableId();
                    pt.BranchId = param.BranchId;
                    pt.MruId = param.MruId;
                    pt.PrintingStatus = 2;
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.BranchId = DaHelper.GetString(dr, "BranchId");
                        pt.MruId = DaHelper.GetString(dr, "MruId");
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        public List<PrintableId> CheckPrintableGreenReceiptCaId(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("bp_sel_printableGreenReceiptCaId");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    //no data found.
                    PrintableId pt = new PrintableId();
                    pt.BranchId = param.BranchId;
                    pt.MruId = param.MruId;
                    pt.CaId = param.UserId.TrimStart('0');
                    pt.PrintingStatus = 2;
                    _printId.Add(pt);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PrintableId pt = new PrintableId();
                        pt.BranchId = DaHelper.GetString(dr, "BranchId");
                        pt.MruId = DaHelper.GetString(dr, "MruId");
                        pt.CaId = DaHelper.GetString(dr, "CaId").TrimStart('0');
                        pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                        pt.IsZeroRecord = false;
                        _printId.Add(pt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _printId;
        }

        public List<PrintableId> CheckRePrintableGreenReceiptMass(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = null;
                cmd = db.GetStoredProcCommand("bp_sel_rePrintableGreenReceiptMass");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
                db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
                db.AddInParameter(cmd, "ToMruId", DbType.String, param.ToMruId);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PrintableId pt = new PrintableId();
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.MruId = DaHelper.GetString(dr, "MruId");
                    pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                    _printId.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _printId;
        }

        public List<PrintableId> CheckRePrintableGreenReceiptCaId(BillPrintingConditionParam param)
        {
            List<PrintableId> _printId = new List<PrintableId>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = null;
                cmd = db.GetStoredProcCommand("bp_sel_rePrintableGreenReceiptCaId");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
                db.AddInParameter(cmd, "FromNoText", DbType.String,
                                  param.FromNumberId == null ? null : param.FromNumberId.PadLeft(12, '0'));
                db.AddInParameter(cmd, "ToNoText", DbType.String,
                                  param.ToNumberId == null ? null : param.ToNumberId.PadLeft(12, '0'));

                if (param.BillPrintingCondition == (int) PrintingCondition.BillSeqPrinting)
                    db.AddInParameter(cmd, "ParamType", DbType.String, "1");

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PrintableId pt = new PrintableId();
                    pt.BranchId = DaHelper.GetString(dr, "BranchId");
                    pt.MruId = DaHelper.GetString(dr, "MruId");
                    pt.CaId = DaHelper.GetString(dr, "CaId");
                    if (!string.IsNullOrEmpty(pt.CaId))
                    {
                        if (pt.CaId.Length > 7)
                            pt.CaId = pt.CaId.TrimStart('0');
                        else
                            pt.CaId = pt.CaId.PadLeft(7, '0');
                    }

                    pt.BillPeriod = DaHelper.GetString(dr, "BillPred");
                    pt.PrintingStatus = DaHelper.GetInt(dr, "PrintStatus");
                    _printId.Add(pt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _printId;
        }

        #endregion

        #endregion

        #region Print Functions

        #region For menu 5.1,5.2

        public List<BlueBill> PrintBlueMass(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printBlueMass");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }

        public List<BlueBill> PrintBlueCaId(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printBlueCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }

        public List<BlueBill> PrintSpotBillMass(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printSpotBillMass");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }

        public List<BlueBill> PrintSpotBillCaId(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printSpotBillCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }

        public List<GreenBill> PrintGreenMass(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printGreenMass");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        public List<GreenBill> PrintGreenCaId(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printGreenCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        public List<A4Bill> PrintA4Mass(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printA4Mass");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            db.AddInParameter(cmd, "A4Reprint", DbType.String, param.A4Reprint);
            db.AddInParameter(cmd, "ApproverId", DbType.String, param.ApproverId);
            db.AddInParameter(cmd, "ApproverName", DbType.String, param.ApproverName);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForA4Bill(dt);
        }

        public List<A4Bill> PrintA4CaId(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printA4CaId");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            db.AddInParameter(cmd, "ApproverId", DbType.String, param.ApproverId);
            db.AddInParameter(cmd, "ApproverName", DbType.String, param.ApproverName);
            //db.AddInParameter(cmd, "A4Reprint", DbType.String, param.A4Reprint);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForA4Bill(dt);
        }

        public List<A4Bill> PrintA4BillSeq(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printA4BillSeq");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "pBillSeqNo", DbType.String, param.UserId);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            db.AddInParameter(cmd, "ApproverId", DbType.String, param.ApproverId);
            db.AddInParameter(cmd, "ApproverName", DbType.String, param.ApproverName);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForA4Bill(dt);
        }

        #endregion

        #region For menu 5.3

        public List<BlueBill> ReprintBlueMass(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintBlueMass");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }

        public List<BlueBill> ReprintBlueCaId(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintBlueCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }

        public List<BlueBill> ReprintBlueBillSeqNo(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintBlueBillSeqNo");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "pBillSeqNo", DbType.Int32, Convert.ToInt32(param.UserId));
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }

        public List<BlueBill> ReprintSpotBillMass(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintSpotBillMass");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }

        public List<BlueBill> ReprintSpotBillCaId(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintSpotBillCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }

        public List<BlueBill> ReprintSpotBillSeqNo(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintSpotBillSeqNo");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "pBillSeqNo", DbType.Int32, Convert.ToInt32(param.UserId));
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }


        public List<GreenBill> ReprintGreenMass(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintGreenMass");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        public List<GreenBill> ReprintGreenCaId(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintGreenCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        public List<GreenBill> ReprintGreenBillSeqNo(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintGreenBillSeqNo");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "pBillSeqNo", DbType.Int32, Convert.ToInt32(param.UserId));
            db.AddInParameter(cmd, "Reverse", DbType.Int16, param.HasOrgDoc);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        #endregion

        #region For menu 5.4,5.5

        public List<A4Bill> PrintGrpInv(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = null;

            //print by MtNo
            cmd = db.GetStoredProcCommand("bp_ins_printGrpInv");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "MtNo", DbType.String, param.MtNo);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            db.AddInParameter(cmd, "ApproverId", DbType.String, param.ApproverId);
            db.AddInParameter(cmd, "ApproverName", DbType.String, param.ApproverName);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForA4Bill(dt);
        }

        public List<A4Bill> ReprintGrpInvMtNo(DbTransaction trans, BillPrintingConditionParam param)
        {
            List<A4Bill> _a4Bill = new List<A4Bill>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = null;

            cmd = db.GetStoredProcCommand("bp_ins_rePrintGrpInvMtNo");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "MtNo", DbType.String, param.MtNo);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            db.AddInParameter(cmd, "ApproverId", DbType.String, param.ApproverId);
            db.AddInParameter(cmd, "ApproverName", DbType.String, param.ApproverName);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForA4Bill(dt);
        }

        //-----Check logic again about caId--------------------------------------------------------------
        public List<A4Bill> ReprintGrpInvCaId(DbTransaction trans, BillPrintingConditionParam param)
        {
            List<A4Bill> _a4Bill = new List<A4Bill>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = null;

            //print by CaId
            cmd = db.GetStoredProcCommand("bp_ins_rePrintGrpInvCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "PaymentDueDate", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            db.AddInParameter(cmd, "ApproverId", DbType.String, param.ApproverId);
            db.AddInParameter(cmd, "ApproverName", DbType.String, param.ApproverName);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForA4Bill(dt);
        }

        //-----------------------------------------------------------------------------------------------

        #endregion

        #region For menu 5.6,5.7

        public List<GreenBill> PrintGreenBank(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printGreenBank");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "InvFrom", DbType.String, param.FromNumberId.Remove(0, 4));
            db.AddInParameter(cmd, "InvTo", DbType.String, param.ToNumberId.Remove(0, 4));
            db.AddInParameter(cmd, "BankId", DbType.String, param.BankId);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);

            if (string.IsNullOrEmpty(param.BillPeriod))
                db.AddInParameter(cmd, "BankDueDate", DbType.String, param.DueDate);
            else
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);

            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        public List<BlueBill> PrintBlueBank(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printBlueBank");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "InvFrom", DbType.String, param.FromNumberId);
            db.AddInParameter(cmd, "InvTo", DbType.String, param.ToNumberId);
            db.AddInParameter(cmd, "BankId", DbType.String, param.BankId);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);

            if (string.IsNullOrEmpty(param.BillPeriod))
                db.AddInParameter(cmd, "BankDueDate", DbType.String, param.DueDate);
            else
                db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);

            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }

        public List<GreenBill> ReprintGreenBank(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintGreenBank");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BankDueDate", DbType.String, param.DueDate);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod); //Added May, 14
            db.AddInParameter(cmd, "BankId", DbType.String, param.BankId);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        public List<BlueBill> ReprintBlueBank(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintBlueBank");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BankDueDate", DbType.String, param.DueDate);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod); //Added May, 14
            db.AddInParameter(cmd, "BankId", DbType.String, param.BankId);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForBlueBill(dt);
        }

        #endregion

        #region For menu 5.8, 5.9

        public List<GreenBill> PrintGreenReceiptMass(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printGreenReceiptMass");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        public List<GreenBill> PrintGreenReceiptCaId(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_printGreenReceiptCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        public List<GreenBill> ReprintGreenReceiptMass(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintGreenReceiptMass");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "MruId", DbType.String, param.MruId);
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        public List<GreenBill> ReprintGreenReceiptCaId(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintGreenReceiptCaId");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "CaId", DbType.String, param.UserId.PadLeft(12, '0'));
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        public List<GreenBill> ReprintGreenReceiptBillSeqNo(DbTransaction trans, BillPrintingConditionParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("bp_ins_rePrintGreenReceiptBillSeqNo");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "PrintBranch", DbType.String, param.CommBranchId);
            db.AddInParameter(cmd, "PrintBranchName", DbType.String, param.CommBranchName);
            db.AddInParameter(cmd, "BillPred", DbType.String, param.BillPeriod);
            db.AddInParameter(cmd, "pBillSeqNo", DbType.Int32, Convert.ToInt32(param.UserId));
            db.AddInParameter(cmd, "PrintedBy", DbType.String, param.PrintedBy);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            return ObjectMappingForGreenBill(dt);
        }

        #endregion

        #endregion

        #region PartitionLot

        private List<PrintableIdByBank> PartitionLot(List<string> invoices, int billType, string dueDate, string bankId)
        {
            List<PrintableIdByBank> ret = new List<PrintableIdByBank>();
            int actLotZize = 0;
            int lotNumber = 1;
            string invFrom = null;
            string invTo = null;

            for (int i = 0; i < invoices.Count; i++)
            {
                actLotZize++;

                if ((i + 1)%LOT_SIZE == 1)
                {
                    invFrom = invoices[i];
                }
                else if ((i + 1)%LOT_SIZE == 0)
                {
                    invTo = invoices[i];
                    PrintableIdByBank pt = new PrintableIdByBank();
                    pt.FromCaId = invFrom;
                    pt.ToCaId = invTo;
                    pt.BankLotNo = lotNumber;
                    pt.DueDate = dueDate;
                    pt.BankId = bankId;
                    pt.LotSize = actLotZize;
                    pt.BillType = billType;
                    pt.PrintDetail = string.Format("{0}.{1}.{2}/{3}/#Lot.{4}/({5})", pt.DueDate.Substring(6, 2),
                                                   pt.DueDate.Substring(4, 2),
                                                   pt.DueDate.Substring(0, 4), pt.BankId, pt.BankLotNo.Value.ToString(),
                                                   pt.LotSize.ToString());

                    ret.Add(pt);

                    //reset counter
                    actLotZize = 0;
                    lotNumber++;
                }
            }

            //last lot
            if (invoices.Count%LOT_SIZE != 0)
            {
                PrintableIdByBank pt = new PrintableIdByBank();
                invTo = invoices[invoices.Count - 1];
                pt.FromCaId = invFrom;
                pt.ToCaId = invTo;
                pt.BankLotNo = lotNumber;
                pt.DueDate = dueDate;
                pt.BankId = bankId;
                pt.LotSize = actLotZize;
                pt.BillType = billType;
                pt.PrintDetail = string.Format("{0}.{1}.{2}/{3}/#Lot.{4}/({5})", pt.DueDate.Substring(6, 2),
                                               pt.DueDate.Substring(4, 2),
                                               pt.DueDate.Substring(0, 4), pt.BankId, pt.BankLotNo.Value.ToString(),
                                               pt.LotSize.ToString());

                ret.Add(pt);
            }


            return ret;
        }

        #endregion

        #region Object Mapping

        private List<BlueBill> ObjectMappingForBlueBill(DataTable dt)
        {
            List<BlueBill> _blueBill = new List<BlueBill>();

            foreach (DataRow dr in dt.Rows)
            {
                BlueBill bb = new BlueBill();
                //Our hundred field...
                bb.W_10_invoice_no = DaHelper.GetString(dr, "w_10_invoice_no").Trim();
                bb.W_20_buss_place = DaHelper.GetString(dr, "w_20_buss_place").Trim();
                bb.W_90_cust_name1 = DaHelper.GetString(dr, "w_90_cust_name1").Trim();
                bb.W_90_cust_name2 = DaHelper.GetString(dr, "w_90_cust_name2").Trim();
                bb.W_130_period = DaHelper.GetString(dr, "w_130_period").Trim();
                if (!string.IsNullOrEmpty(bb.W_130_period))
                    bb.W_130_period = string.Format("{0}/{1}", bb.W_130_period.Substring(4, 2),
                                                    bb.W_130_period.Substring(0, 4));
                bb.W_140_reg = DaHelper.GetString(dr, "w_140_reg").Trim();
                bb.W_150_contract = DaHelper.GetString(dr, "w_150_contract").Trim();
                bb.W_160_device = DaHelper.GetString(dr, "w_160_device").Trim();
                bb.W_170_rate_cat = DaHelper.GetString(dr, "w_170_rate_cat").Trim();
                bb.W_171_ettat_code = DaHelper.GetString(dr, "w_171_ettat_code").Trim();
                bb.W_200_mr_date = DaHelper.GetString(dr, "w_200_mr_date").Trim();
                bb.W_216_address = DaHelper.GetString(dr, "w_216_address").Trim();
                bb.W_217_address = DaHelper.GetString(dr, "w_217_address").Trim();
                bb.W_218_address = DaHelper.GetString(dr, "w_218_address").Trim();
                bb.W_221_address = DaHelper.GetString(dr, "w_221_address").Trim();
                bb.W_222_address = DaHelper.GetString(dr, "w_222_address").Trim();
                bb.W_223_address = DaHelper.GetString(dr, "w_223_address").Trim();
                bb.W_230_post_code = DaHelper.GetString(dr, "w_230_post_code").Trim();
                bb.W_255_device_1 = DaHelper.GetString(dr, "w_255_device_1").Trim();
                bb.W_260_zwstand_1_txt = DaHelper.GetString(dr, "w_260_zwstand_1_txt").Trim();
                bb.W_270_zwstvor_1_txt = DaHelper.GetString(dr, "w_270_zwstvor_1_txt").Trim();
                bb.W_280_umwfakt_1_txt = DaHelper.GetString(dr, "w_280_umwfakt_1_txt").Trim();
                bb.W_290_abrmenge_1_txt = DaHelper.GetString(dr, "w_290_abrmenge_1_txt").Trim();
                bb.W_290_abrmenge_1_txt = DaHelper.GetString(dr, "w_290_abrmenge_1_txt").Trim();
                bb.W_295_device_2 = DaHelper.GetString(dr, "w_295_device_2").Trim();
                bb.W_300_zwstand_2_txt = DaHelper.GetString(dr, "w_300_zwstand_2_txt").Trim();
                bb.W_310_zwstvor_2_txt = DaHelper.GetString(dr, "w_310_zwstvor_2_txt").Trim();
                bb.W_320_umwfakt_2_txt = DaHelper.GetString(dr, "w_320_umwfakt_2_txt").Trim();
                bb.W_330_abrmenge_2_txt = DaHelper.GetString(dr, "w_330_abrmenge_2_txt").Trim();
                bb.W_340_peak_txt = DaHelper.GetString(dr, "w_340_peak_txt").Trim();
                bb.W_350_dash_txt = DaHelper.GetString(dr, "w_350_dash_txt").Trim();
                bb.W_355_device_3 = DaHelper.GetString(dr, "w_355_device_3").Trim();
                bb.W_360_zwstand_3_txt = DaHelper.GetString(dr, "w_360_zwstand_3_txt").Trim();
                bb.W_370_zwstvor_3_txt = DaHelper.GetString(dr, "w_370_zwstvor_3_txt").Trim();
                bb.W_380_umwfakt_3_txt = DaHelper.GetString(dr, "w_380_umwfakt_3_txt").Trim();
                bb.W_390_abrmenge_3_txt = DaHelper.GetString(dr, "w_390_abrmenge_3_txt").Trim();
                bb.W_400_off_peak_txt = DaHelper.GetString(dr, "w_400_off_peak_txt").Trim();
                bb.W_410_ene_txt = DaHelper.GetString(dr, "w_410_ene_txt").Trim();
                bb.W_415_device_4 = DaHelper.GetString(dr, "w_415_device_4").Trim();
                bb.W_420_zwstand_4_txt = DaHelper.GetString(dr, "w_420_zwstand_4_txt").Trim();
                bb.W_430_zwstvor_4_txt = DaHelper.GetString(dr, "w_430_zwstvor_4_txt").Trim();
                bb.W_440_umwfakt_4_txt = DaHelper.GetString(dr, "w_440_umwfakt_4_txt").Trim();
                bb.W_450_abrmenge_4_txt = DaHelper.GetString(dr, "w_450_abrmenge_4_txt").Trim();
                bb.W_460_hol_txt = DaHelper.GetString(dr, "w_460_hol_txt").Trim();
                bb.W_470_zwstand_1_txt = DaHelper.GetString(dr, "w_470_zwstand_1_txt").Trim();
                bb.W_480_zwstvor_1_txt = DaHelper.GetString(dr, "w_480_zwstvor_1_txt").Trim();
                bb.W_490_consumption_txt = DaHelper.GetString(dr, "W_490_consumption_txt").Trim();
                bb.W_500_txt6 = DaHelper.GetString(dr, "w_500_txt6").Trim();
                bb.W_1310_amount_txt = DaHelper.GetString(dr, "w_1310_amount_txt").Trim();
                bb.W_1320_foreign_amt = DaHelper.GetString(dr, "w_1320_foreign_amt").Trim();
                bb.W_1330_foreign_txt = DaHelper.GetString(dr, "w_1330_foreign_txt").Trim();
                bb.W_1340_foreign_uit = DaHelper.GetString(dr, "w_1340_foreign_uit").Trim();
                bb.W_1345_blue_txt1 = DaHelper.GetString(dr, "w_1345_blue_txt1").Trim();
                bb.W_1380_fttot_txt = DaHelper.GetString(dr, "w_1380_fttot_txt").Trim();
                bb.W_1381_ft_peiod_txt = DaHelper.GetString(dr, "w_1381_ft_peiod_txt").Trim();
                bb.W_1400_ftchg_txt = DaHelper.GetString(dr, "w_1400_ftchg_txt").Trim();
                bb.W_1450_mr_kw_17_6_txt = DaHelper.GetString(dr, "w_1450_mr_kw_17_6_txt").Trim();
                bb.W_1460_mr_kw_17_7 = DaHelper.GetString(dr, "w_1460_mr_kw_17_7").Trim();
                bb.W_1470_baht = DaHelper.GetString(dr, "w_1470_baht").Trim();
                bb.W_1480_net_before_vat_txt = DaHelper.GetString(dr, "w_1480_net_before_vat_txt").Trim();
                bb.W_1485_net_before_vat_left = DaHelper.GetString(dr, "w_1485_net_before_vat_left").Trim();
                bb.W_1490_tax_rate_txt = DaHelper.GetString(dr, "w_1490_tax_rate_txt").Trim();
                bb.W_1500_tax_amount_txt = DaHelper.GetString(dr, "w_1500_tax_amount_txt").Trim();
                bb.W_1505_tax_amount_left = DaHelper.GetString(dr, "w_1505_tax_amount_left").Trim();
                bb.W_1510_total_amnt_txt = DaHelper.GetString(dr, "w_1510_total_amnt_txt").Trim();
                string tmp1 = string.Format("{0}", DaHelper.GetString(dr, "w_1550_case_text1").Trim());
                string tmp2 = string.Format("{0}", DaHelper.GetString(dr, "w_1550_case_text2").Trim());
                string tmp3 = string.Format("{0}", DaHelper.GetString(dr, "w_1550_case_text3").Trim());
                string tmp4 = string.Format("{0}", DaHelper.GetString(dr, "w_1550_case_text4").Trim());
                bb.W_1550_case_text1 = tmp1.Substring(0, tmp1.Length > 40 ? 40 : tmp1.Length);
                bb.W_1550_case_text2 = tmp2.Substring(0, tmp2.Length > 40 ? 40 : tmp2.Length);
                bb.W_1550_case_text3 = tmp3.Substring(0, tmp3.Length > 40 ? 40 : tmp3.Length);
                bb.W_1550_case_text4 = tmp4.Substring(0, tmp4.Length > 40 ? 40 : tmp4.Length);
                bb.W_1550_case_text7 = DaHelper.GetString(dr, "W_1550_case_text7").Trim();
                bb.W_1550_case_text8 = DaHelper.GetString(dr, "W_1550_case_text8").Trim();
                bb.W_1580_payment_due_date = DaHelper.GetString(dr, "w_1580_payment_due_date").Trim();
                bb.W_1590_barcode1 = DaHelper.GetString(dr, "w_1590_barcode1").Trim();
                bb.W_1600_barcode2 = DaHelper.GetString(dr, "w_1600_barcode2").Trim();
                bb.W_1880_payment_lot = DaHelper.GetString(dr, "w_1880_payment_lot").Trim();
                bb.W_1950_collector_id = DaHelper.GetString(dr, "w_1950_collector_id").Trim();
                bb.BillSeqNo = DaHelper.GetString(dr, "BillSeqNo").Trim();
                bb.W_2070_taxid_p = DaHelper.GetString(dr, "w_2070_taxid_p").Trim();
                bb.W_2080_taxid_c = DaHelper.GetString(dr, "w_2080_taxid_c").Trim();

                bb.W_2081_taxid     = DaHelper.GetString(dr, "w_2081_taxid").Trim();
                bb.W_2082_taxid     = DaHelper.GetString(dr, "w_2082_taxid").Trim();
                bb.W_2120_pay_adrc  = DaHelper.GetString(dr, "w_2120_pay_adrc").Trim();
                bb.W_2130_off_tel   = DaHelper.GetString(dr, "w_2130_off_tel").Trim();
                bb.W_2140_oth_dis1  = DaHelper.GetString(dr, "w_2140_oth_dis1").Trim();
                bb.W_2150_oth_dis2  = DaHelper.GetString(dr, "w_2150_oth_dis2").Trim();
                bb.W_2160_oth_dis3  = DaHelper.GetString(dr, "w_2160_oth_dis3").Trim();
                bb.W_2170_sec_dis1  = DaHelper.GetString(dr, "w_2170_sec_dis1").Trim();
                bb.W_2180_sec_dis2  = DaHelper.GetString(dr, "w_2180_sec_dis2").Trim();
                bb.W_2190_sec_dis3  = DaHelper.GetString(dr, "w_2190_sec_dis3").Trim();
                bb.W_2200_gtot_amn  = DaHelper.GetString(dr, "w_2200_gtot_amn").Trim();   
                bb.W_2210_spell_gtot  = DaHelper.GetString(dr, "w_2210_spell_gtot").Trim();  
                bb.W_2220_mr_date_m1  = DaHelper.GetString(dr, "w_2220_mr_date_m1").Trim(); 
                bb.W_2230_mr_date_m2  = DaHelper.GetString(dr, "w_2230_mr_date_m2").Trim(); 
                bb.W_2240_mr_date_m3  = DaHelper.GetString(dr, "w_2240_mr_date_m3").Trim();
                bb.W_2250_mr_date_m4  = DaHelper.GetString(dr, "w_2250_mr_date_m4").Trim();
                bb.W_2260_mr_date_m5  = DaHelper.GetString(dr, "w_2260_mr_date_m5").Trim();
                bb.W_2270_mr_date_m6  = DaHelper.GetString(dr, "w_2270_mr_date_m6").Trim();
                bb.W_2280_unit_m1  = DaHelper.GetString(dr, "w_2280_unit_m1").Trim();
                bb.W_2290_unit_m2  = DaHelper.GetString(dr, "w_2290_unit_m2").Trim();
                bb.W_2300_unit_m3  = DaHelper.GetString(dr, "w_2300_unit_m3").Trim();
                bb.W_2310_unit_m4  = DaHelper.GetString(dr, "w_2310_unit_m4").Trim();
                bb.W_2320_unit_m5  = DaHelper.GetString(dr, "w_2320_unit_m5").Trim();
                bb.W_2330_unit_m6  = DaHelper.GetString(dr, "w_2330_unit_m6").Trim();

                bb.W_1840_mru               = DaHelper.GetString(dr, "w_1840_mru").Trim();
                bb.W_1110_basic_19_1_txt    = DaHelper.GetString(dr, "w_1110_basic_19_1_txt").Trim();
                bb.W_40_sname    = DaHelper.GetString(dr, "w_40_sname").Trim();
                bb.W_190_multi_n    = DaHelper.GetString(dr, "w_190_multi_n").Trim();
                bb.W_1100_sum_service_support_txt = DaHelper.GetString(dr, "w_1100_sum_service_support_txt").Trim();

                bb.W_2340_rec_doc = DaHelper.GetString(dr, "W_2340_REC_DOC").Trim();
                bb.W_2350_rec_date = DaHelper.GetString(dr, "W_2350_REC_DATE").Trim();
                bb.W_2360_unit_rec = DaHelper.GetString(dr, "W_2360_UNIT_REC").Trim();
                bb.W_2370_oth_dis4 = DaHelper.GetString(dr, "W_2370_OTH_DIS4").Trim();
                bb.W_2380_oth_dis5 = DaHelper.GetString(dr, "W_2380_OTH_DIS5").Trim();
                bb.W_2390_oth_dis6 = DaHelper.GetString(dr, "W_2390_OTH_DIS6").Trim();
                bb.W_2400_oth_dis7 = DaHelper.GetString(dr, "W_2400_OTH_DIS7").Trim();
                bb.W_2410_oth_dis8 = DaHelper.GetString(dr, "W_2410_OTH_DIS8").Trim();
                bb.W_2420_oth_dis9 = DaHelper.GetString(dr, "W_2420_OTH_DIS9").Trim();
                bb.W_2430_rec_text1 = DaHelper.GetString(dr, "W_2430_REC_TEXT1").Trim();
                bb.W_2440_rec_text2 = DaHelper.GetString(dr, "W_2440_REC_TEXT2").Trim();
                bb.W_2450_rec_text3 = DaHelper.GetString(dr, "W_2450_REC_TEXT3").Trim();
                bb.W_01_print_doc = DaHelper.GetString(dr, "w_01_print_doc").Trim();

                _blueBill.Add(bb);
            }

            return _blueBill;
        }

        private List<A4Bill> ObjectMappingForA4Bill(DataTable dt)
        {
            List<A4Bill> _a4Bill = new List<A4Bill>();

            foreach (DataRow dr in dt.Rows)
            {
                A4Bill a4 = new A4Bill();
                a4.W_30_invoice_no = DaHelper.GetString(dr, "w_30_invoice_no").Trim();
                a4.W_40_sname = DaHelper.GetString(dr, "w_40_sname").Trim();
                a4.W_50_day = DaHelper.GetString(dr, "w_50_day").Trim();
                a4.W_60_month = DaHelper.GetString(dr, "w_60_month").Trim();
                if (!string.IsNullOrEmpty(a4.W_60_month))
                    a4.W_60_month = ConvertToMonthName(a4.W_60_month);
                a4.W_70_year = DaHelper.GetString(dr, "w_70_year").Trim();
                a4.W_80_cust_name1_name2 = DaHelper.GetString(dr, "w_80_cust_name1_name2").Trim();
                a4.W_130_period = DaHelper.GetString(dr, "w_130_period").Trim();
                if (!string.IsNullOrEmpty(a4.W_130_period))
                    a4.W_130_period = string.Format("{0}/{1}", a4.W_130_period.Substring(4, 2),
                                                    a4.W_130_period.Substring(0, 4));
                a4.W_140_reg = DaHelper.GetString(dr, "w_140_reg").Trim();
                a4.W_150_contract = DaHelper.GetString(dr, "w_150_contract").Trim();
                a4.W_160_device = DaHelper.GetString(dr, "w_160_device").Trim();
                a4.W_170_rate_cat = DaHelper.GetString(dr, "w_170_rate_cat").Trim();
                a4.W_171_ettat_code = DaHelper.GetString(dr, "w_171_ettat_code").Trim();
                a4.W_180_voltage = DaHelper.GetString(dr, "w_180_voltage").Trim();
                a4.W_190_multi_t = DaHelper.GetString(dr, "w_190_multi_t").Trim();
                a4.W_200_mr_date = DaHelper.GetString(dr, "w_200_mr_date").Trim();
                a4.W_500_txt6 = DaHelper.GetString(dr, "w_500_txt6").Trim();
                a4.W_510_mr_kw_6_1_txt = DaHelper.GetString(dr, "w_510_mr_kw_6_1_txt").Trim();
                a4.W_520_mr_kw_6_2_txt = DaHelper.GetString(dr, "w_520_mr_kw_6_2_txt").Trim();
                a4.W_530_mr_kw_6_3_txt = DaHelper.GetString(dr, "w_530_mr_kw_6_3_txt").Trim();
                a4.W_540_mr_kw_6_4_txt = DaHelper.GetString(dr, "w_540_mr_kw_6_4_txt").Trim();
                a4.W_550_mr_kw_6_5 = DaHelper.GetString(dr, "w_550_mr_kw_6_5").Trim();
                a4.W_555_device_6_7 = DaHelper.GetString(dr, "w_555_device_6_7").Trim();
                a4.W_560_mr_kw_7_1_txt = DaHelper.GetString(dr, "w_560_mr_kw_7_1_txt").Trim();
                a4.W_570_mr_kw_7_2_txt = DaHelper.GetString(dr, "w_570_mr_kw_7_2_txt").Trim();
                a4.W_580_mr_kw_7_3_txt = DaHelper.GetString(dr, "w_580_mr_kw_7_3_txt").Trim();
                a4.W_590_mr_kw_7_4_txt = DaHelper.GetString(dr, "w_590_mr_kw_7_4_txt").Trim();
                a4.W_600_mr_kw_7_5 = DaHelper.GetString(dr, "w_600_mr_kw_7_5").Trim();
                a4.W_610_mr_kw_8_1_txt = DaHelper.GetString(dr, "w_610_mr_kw_8_1_txt").Trim();
                a4.W_620_mr_kw_8_2_txt = DaHelper.GetString(dr, "w_620_mr_kw_8_2_txt").Trim();
                a4.W_630_mr_kw_8_3_txt = DaHelper.GetString(dr, "w_630_mr_kw_8_3_txt").Trim();
                a4.W_640_mr_kw_8_5 = DaHelper.GetString(dr, "w_640_mr_kw_8_5").Trim();
                a4.W_650_mr_kw_9_1_txt = DaHelper.GetString(dr, "w_650_mr_kw_9_1_txt").Trim();
                a4.W_660_mr_kw_9_2_txt = DaHelper.GetString(dr, "w_660_mr_kw_9_2_txt").Trim();
                a4.W_670_mr_kw_9_3_txt = DaHelper.GetString(dr, "w_670_mr_kw_9_3_txt").Trim();
                a4.W_680_mr_kw_9_4_txt = DaHelper.GetString(dr, "w_680_mr_kw_9_4_txt").Trim();
                a4.W_690_mr_kw_9_5 = DaHelper.GetString(dr, "w_690_mr_kw_9_5").Trim();
                a4.W_695_device_9_7 = DaHelper.GetString(dr, "w_695_device_9_7").Trim();
                a4.W_700_mr_kw_10_1_txt = DaHelper.GetString(dr, "w_700_mr_kw_10_1_txt").Trim();
                a4.W_710_mr_kw_10_2_txt = DaHelper.GetString(dr, "w_710_mr_kw_10_2_txt").Trim();
                a4.W_720_mr_kw_10_3_txt = DaHelper.GetString(dr, "w_720_mr_kw_10_3_txt").Trim();
                a4.W_730_mr_kw_10_4_txt = DaHelper.GetString(dr, "w_730_mr_kw_10_4_txt").Trim();
                a4.W_740_mr_kw_10_5 = DaHelper.GetString(dr, "w_740_mr_kw_10_5").Trim();
                a4.W_750_mr_kw_11_1_txt = DaHelper.GetString(dr, "w_750_mr_kw_11_1_txt").Trim();
                a4.W_760_mr_kw_11_2_txt = DaHelper.GetString(dr, "w_760_mr_kw_11_2_txt").Trim();
                a4.W_770_mr_kw_11_3_txt = DaHelper.GetString(dr, "w_770_mr_kw_11_3_txt").Trim();
                a4.W_780_mr_kw_11_5 = DaHelper.GetString(dr, "w_780_mr_kw_11_5").Trim();
                a4.W_790_mr_kw_12_1_txt = DaHelper.GetString(dr, "w_790_mr_kw_12_1_txt").Trim();
                a4.W_800_mr_kw_12_2_txt = DaHelper.GetString(dr, "w_800_mr_kw_12_2_txt").Trim();
                a4.W_810_mr_kw_12_3_txt = DaHelper.GetString(dr, "w_810_mr_kw_12_3_txt").Trim();
                a4.W_820_mr_kw_12_4_txt = DaHelper.GetString(dr, "w_820_mr_kw_12_4_txt").Trim();
                a4.W_830_mr_kw_12_5 = DaHelper.GetString(dr, "w_830_mr_kw_12_5").Trim();
                a4.W_835_device_12_7 = DaHelper.GetString(dr, "w_835_device_12_7").Trim();
                a4.W_840_mr_kw_13_1_txt = DaHelper.GetString(dr, "w_840_mr_kw_13_1_txt").Trim();
                a4.W_850_mr_kw_13_2_txt = DaHelper.GetString(dr, "w_850_mr_kw_13_2_txt").Trim();
                a4.W_860_mr_kw_13_3_txt = DaHelper.GetString(dr, "w_860_mr_kw_13_3_txt").Trim();
                a4.W_870_mr_kw_13_4_txt = DaHelper.GetString(dr, "w_870_mr_kw_13_4_txt").Trim();
                a4.W_890_mr_kw_13_5 = DaHelper.GetString(dr, "w_890_mr_kw_13_5").Trim();
                a4.W_900_mr_kw_14_1_txt = DaHelper.GetString(dr, "w_900_mr_kw_14_1_txt").Trim();
                a4.W_910_mr_kw_14_2_txt = DaHelper.GetString(dr, "w_910_mr_kw_14_2_txt").Trim();
                a4.W_920_mr_kw_14_3_txt = DaHelper.GetString(dr, "w_920_mr_kw_14_3_txt").Trim();
                a4.W_930_mr_kw_14_5 = DaHelper.GetString(dr, "w_930_mr_kw_14_5").Trim();
                a4.W_940_mr_kw_15_1_txt = DaHelper.GetString(dr, "w_940_mr_kw_15_1_txt").Trim();
                a4.W_950_mr_kw_15_2_txt = DaHelper.GetString(dr, "w_950_mr_kw_15_2_txt").Trim();
                a4.W_960_mr_kw_15_3_txt = DaHelper.GetString(dr, "w_960_mr_kw_15_3_txt").Trim();
                a4.W_970_mr_kw_15_4_txt = DaHelper.GetString(dr, "w_970_mr_kw_15_4_txt").Trim();
                a4.W_980_mr_kw_15_5 = DaHelper.GetString(dr, "w_980_mr_kw_15_5").Trim();
                a4.W_985_device_15_7 = DaHelper.GetString(dr, "w_985_device_15_7").Trim();
                a4.W_990_mr_kw_16_1_txt = DaHelper.GetString(dr, "w_990_mr_kw_16_1_txt").Trim();
                a4.W_1000_mr_kw_16_2_txt = DaHelper.GetString(dr, "w_1000_mr_kw_16_2_txt").Trim();
                a4.W_1010_mr_kw_16_3_txt = DaHelper.GetString(dr, "w_1010_mr_kw_16_3_txt").Trim();
                a4.W_1020_mr_kw_16_4_txt = DaHelper.GetString(dr, "w_1020_mr_kw_16_4_txt").Trim();
                a4.W_1030_mr_kw_16_5 = DaHelper.GetString(dr, "w_1030_mr_kw_16_5").Trim();
                a4.W_1040_mr_kw_17_1_txt = DaHelper.GetString(dr, "w_1040_mr_kw_17_1_txt").Trim();
                a4.W_1050_mr_kw_17_2_txt = DaHelper.GetString(dr, "w_1050_mr_kw_17_2_txt").Trim();
                a4.W_1060_mr_kw_17_3_txt = DaHelper.GetString(dr, "w_1060_mr_kw_17_3_txt").Trim();
                a4.W_1070_mr_kw_17_5 = DaHelper.GetString(dr, "w_1070_mr_kw_17_5").Trim();
                a4.W_1080_service_txt = DaHelper.GetString(dr, "w_1080_service_txt").Trim();
                a4.W_1090_service_support_txt = DaHelper.GetString(dr, "w_1090_service_support_txt").Trim();
                a4.W_1100_sum_service_support_txt = DaHelper.GetString(dr, "w_1100_sum_service_support_txt").Trim();
                a4.W_1110_basic_19_1_txt = DaHelper.GetString(dr, "w_1110_basic_19_1_txt").Trim();
                a4.W_1120_description = DaHelper.GetString(dr, "w_1120_description").Trim();
                a4.W_1130_kvar_20_1_txt = DaHelper.GetString(dr, "w_1130_kvar_20_1_txt").Trim();
                a4.W_1140_kvar_20_2_txt = DaHelper.GetString(dr, "w_1140_kvar_20_2_txt").Trim();
                a4.W_1150_kvar_20_3_txt = DaHelper.GetString(dr, "w_1150_kvar_20_3_txt").Trim();
                a4.W_1160_kvar_20_4_txt = DaHelper.GetString(dr, "w_1160_kvar_20_4_txt").Trim();
                a4.W_1170_kvar_21_1_txt = DaHelper.GetString(dr, "w_1170_kvar_21_1_txt").Trim();
                a4.W_1180_kvar_21_2_txt = DaHelper.GetString(dr, "w_1180_kvar_21_2_txt").Trim();
                a4.W_1190_kvar_21_3_txt = DaHelper.GetString(dr, "w_1190_kvar_21_3_txt").Trim();
                a4.W_1200_kvar_21_4_txt = DaHelper.GetString(dr, "w_1200_kvar_21_4_txt").Trim();
                a4.W_1210_gen_kw_amt_txt = DaHelper.GetString(dr, "w_1210_gen_kw_amt_txt").Trim();
                a4.W_1220_trans_kw_amt_txt = DaHelper.GetString(dr, "w_1220_trans_kw_amt_txt").Trim();
                a4.W_1230_dist_kw_amt_txt = DaHelper.GetString(dr, "w_1230_dist_kw_amt_txt").Trim();
                a4.W_1240_gen_kwh_amt_txt = DaHelper.GetString(dr, "w_1240_gen_kwh_amt_txt").Trim();
                a4.W_1250_trans_kwh_amt_txt = DaHelper.GetString(dr, "w_1250_trans_kwh_amt_txt").Trim();
                a4.W_1260_dist_kwh_amt_txt = DaHelper.GetString(dr, "w_1260_dist_kwh_amt_txt").Trim();
                a4.W_1270_dis_supp_amt_txt = DaHelper.GetString(dr, "w_1270_dis_supp_amt_txt").Trim();
                a4.W_1280_gen_ft_amt_txt = DaHelper.GetString(dr, "w_1280_gen_ft_amt_txt").Trim();
                a4.W_1290_trans_ft_amt_txt = DaHelper.GetString(dr, "w_1290_trans_ft_amt_txt").Trim();
                a4.W_1300_dist_ft_amt_txt = DaHelper.GetString(dr, "w_1300_dist_ft_amt_txt").Trim();
                a4.W_1350_ftgen_txt = DaHelper.GetString(dr, "w_1350_ftgen_txt").Trim();
                a4.W_1360_fttrn_txt = DaHelper.GetString(dr, "w_1360_fttrn_txt").Trim();
                a4.W_1370_ftdis_txt = DaHelper.GetString(dr, "w_1370_ftdis_txt").Trim();
                a4.W_1380_fttot_txt = DaHelper.GetString(dr, "w_1380_fttot_txt").Trim();
                a4.W_1390_ftunit_txt = DaHelper.GetString(dr, "w_1390_ftunit_txt").Trim();
                a4.W_1400_ftchg_txt = DaHelper.GetString(dr, "w_1400_ftchg_txt").Trim();
                a4.W_1410_basic_14_6_txt = DaHelper.GetString(dr, "w_1410_basic_14_6_txt").Trim();
                a4.W_1420_amin_14_7 = DaHelper.GetString(dr, "w_1420_amin_14_7").Trim();
                a4.W_1430_ft_basic_txt = DaHelper.GetString(dr, "w_1430_ft_basic_txt").Trim();
                a4.W_1440_power_txt = DaHelper.GetString(dr, "w_1440_power_txt").Trim();
                a4.W_1450_mr_kw_17_6_txt = DaHelper.GetString(dr, "w_1450_mr_kw_17_6_txt").Trim();
                a4.W_1460_mr_kw_17_7 = DaHelper.GetString(dr, "w_1460_mr_kw_17_7").Trim();
                a4.W_1480_net_before_vat_txt = DaHelper.GetString(dr, "w_1480_net_before_vat_txt").Trim();
                a4.W_1490_tax_rate_txt = DaHelper.GetString(dr, "w_1490_tax_rate_txt").Trim();
                a4.W_1500_tax_amount_txt = DaHelper.GetString(dr, "w_1500_tax_amount_txt").Trim();
                a4.W_1510_total_amnt_txt = DaHelper.GetString(dr, "w_1510_total_amnt_txt").Trim();
                a4.W_1550_case_text1 = DaHelper.GetString(dr, "w_1550_case_text1").Trim();
                a4.W_1550_case_text2 = DaHelper.GetString(dr, "w_1550_case_text2").Trim();
                a4.W_1550_case_text3 = DaHelper.GetString(dr, "w_1550_case_text3").Trim();
                a4.W_1550_case_text4 = DaHelper.GetString(dr, "w_1550_case_text4").Trim();
                a4.W_1550_case_text5 = DaHelper.GetString(dr, "w_1550_case_text5").Trim();
                a4.W_1550_case_text6 = DaHelper.GetString(dr, "w_1550_case_text6").Trim();
                a4.W_1550_case_text7 = DaHelper.GetString(dr, "w_1550_case_text7").Trim();
                a4.W_1550_case_text8 = DaHelper.GetString(dr, "w_1550_case_text8").Trim();
                a4.W_1560_spell_amount = DaHelper.GetString(dr, "w_1560_spell_amount").Trim();
                a4.W_1580_payment_due_date = DaHelper.GetString(dr, "w_1580_payment_due_date").Trim();
                a4.W_1850_1851_adjust_amt = DaHelper.GetString(dr, "w_1850_1851_adjust_amt").Trim();
                a4.W_2030_barcode_a4 = DaHelper.GetString(dr, "w_2030_barcode_a4").Trim();
                a4.BillSeqNo = DaHelper.GetString(dr, "BillSeqNo").Trim();

                a4.Special1580Text = DaHelper.GetString(dr, "Special1580Text").Trim();

                _a4Bill.Add(a4);
            }

            return _a4Bill;
        }

        private List<GreenBill> ObjectMappingForGreenBill(DataTable dt)
        {
            List<GreenBill> _greenBill = new List<GreenBill>();

            foreach (DataRow dr in dt.Rows)
            {
                GreenBill gb = new GreenBill();
                gb.W_10_invoice_no = DaHelper.GetString(dr, "w_10_invoice_no").Trim();
                gb.W_100_sender = DaHelper.GetString(dr, "w_100_sender").Trim();
                gb.W_110_post_code = DaHelper.GetString(dr, "w_110_post_code").Trim();
                gb.W_121_mailing_person = DaHelper.GetString(dr, "w_121_mailing_person").Trim();
                gb.W_122_mailing_person = DaHelper.GetString(dr, "w_122_mailing_person").Trim();
                gb.W_130_period = DaHelper.GetString(dr, "w_130_period").Trim();
                if (!string.IsNullOrEmpty(gb.W_130_period))
                    gb.W_130_period = string.Format("{0}/{1}", gb.W_130_period.Substring(4, 2),
                                                    gb.W_130_period.Substring(0, 4));
                gb.W_140_reg = DaHelper.GetString(dr, "w_140_reg").Trim();
                gb.W_150_contract = DaHelper.GetString(dr, "w_150_contract").Trim();
                gb.W_160_device = DaHelper.GetString(dr, "w_160_device").Trim();
                gb.W_170_rate_cat = DaHelper.GetString(dr, "w_170_rate_cat").Trim();
                gb.W_171_ettat_code = DaHelper.GetString(dr, "w_171_ettat_code").Trim();
                gb.W_200_mr_date = DaHelper.GetString(dr, "w_200_mr_date").Trim();
                gb.W_211_address = DaHelper.GetString(dr, "w_211_address").Trim();
                gb.W_212_address = DaHelper.GetString(dr, "w_212_address").Trim();
                gb.W_213_address = DaHelper.GetString(dr, "w_213_address").Trim();
                gb.W_241_242_address = DaHelper.GetString(dr, "w_241_242_address").Trim();
                gb.W_243_address = DaHelper.GetString(dr, "w_243_address").Trim();
                gb.W_250_post_code = DaHelper.GetString(dr, "w_250_post_code").Trim();
                gb.W_255_device_1 = DaHelper.GetString(dr, "w_255_device_1").Trim();
                gb.W_260_zwstand_1_txt = DaHelper.GetString(dr, "w_260_zwstand_1_txt").Trim();
                gb.W_270_zwstvor_1_txt = DaHelper.GetString(dr, "w_270_zwstvor_1_txt").Trim();
                gb.W_280_umwfakt_1_txt = DaHelper.GetString(dr, "w_280_umwfakt_1_txt").Trim();
                gb.W_290_abrmenge_1_txt = DaHelper.GetString(dr, "w_290_abrmenge_1_txt").Trim();
                gb.W_295_device_2 = DaHelper.GetString(dr, "w_295_device_2").Trim();
                gb.W_300_zwstand_2_txt = DaHelper.GetString(dr, "w_300_zwstand_2_txt").Trim();
                gb.W_310_zwstvor_2_txt = DaHelper.GetString(dr, "w_310_zwstvor_2_txt").Trim();
                gb.W_320_umwfakt_2_txt = DaHelper.GetString(dr, "w_320_umwfakt_2_txt").Trim();
                gb.W_330_abrmenge_2_txt = DaHelper.GetString(dr, "w_330_abrmenge_2_txt").Trim();
                gb.W_340_peak_txt = DaHelper.GetString(dr, "w_340_peak_txt").Trim();
                gb.W_350_dash_txt = DaHelper.GetString(dr, "w_350_dash_txt").Trim();
                gb.W_355_device_3 = DaHelper.GetString(dr, "w_355_device_3").Trim();
                gb.W_360_zwstand_3_txt = DaHelper.GetString(dr, "w_360_zwstand_3_txt").Trim();
                gb.W_370_zwstvor_3_txt = DaHelper.GetString(dr, "w_370_zwstvor_3_txt").Trim();
                gb.W_380_umwfakt_3_txt = DaHelper.GetString(dr, "w_380_umwfakt_3_txt").Trim();
                gb.W_390_abrmenge_3_txt = DaHelper.GetString(dr, "w_390_abrmenge_3_txt").Trim();
                gb.W_400_off_peak_txt = DaHelper.GetString(dr, "w_400_off_peak_txt").Trim();
                gb.W_410_ene_txt = DaHelper.GetString(dr, "w_410_ene_txt").Trim();
                gb.W_415_device_4 = DaHelper.GetString(dr, "w_415_device_4").Trim();
                gb.W_420_zwstand_4_txt = DaHelper.GetString(dr, "w_420_zwstand_4_txt").Trim();
                gb.W_430_zwstvor_4_txt = DaHelper.GetString(dr, "w_430_zwstvor_4_txt").Trim();
                gb.W_440_umwfakt_4_txt = DaHelper.GetString(dr, "w_440_umwfakt_4_txt").Trim();
                gb.W_450_abrmenge_4_txt = DaHelper.GetString(dr, "w_450_abrmenge_4_txt").Trim();
                gb.W_460_hol_txt = DaHelper.GetString(dr, "w_460_hol_txt").Trim();
                gb.W_500_txt6 = DaHelper.GetString(dr, "w_500_txt6").Trim();
                gb.W_1310_amount_txt = DaHelper.GetString(dr, "w_1310_amount_txt").Trim();
                gb.W_1380_fttot_txt = DaHelper.GetString(dr, "w_1380_fttot_txt").Trim();
                gb.W_1381_ft_peiod_txt = DaHelper.GetString(dr, "w_1381_ft_peiod_txt").Trim();
                gb.W_1400_ftchg_txt = DaHelper.GetString(dr, "w_1400_ftchg_txt").Trim();
                gb.W_1450_mr_kw_17_6_txt = DaHelper.GetString(dr, "w_1450_mr_kw_17_6_txt").Trim();
                gb.W_1460_mr_kw_17_7 = DaHelper.GetString(dr, "w_1460_mr_kw_17_7").Trim();
                gb.W_1480_net_before_vat_txt = DaHelper.GetString(dr, "w_1480_net_before_vat_txt").Trim();
                gb.W_1490_tax_rate_txt = DaHelper.GetString(dr, "w_1490_tax_rate_txt").Trim();
                gb.W_1500_tax_amount_txt = DaHelper.GetString(dr, "w_1500_tax_amount_txt").Trim();
                gb.W_1510_total_amnt_txt = DaHelper.GetString(dr, "w_1510_total_amnt_txt").Trim();
                string tmp1 = DaHelper.GetString(dr, "w_1550_case_text1").Trim();
                string tmp2 = DaHelper.GetString(dr, "w_1550_case_text2").Trim();
                string tmp3 = DaHelper.GetString(dr, "w_1550_case_text3").Trim();
                string tmp4 = DaHelper.GetString(dr, "w_1550_case_text4").Trim();
                gb.W_1550_case_text1 = tmp1.Substring(0, tmp1.Length > 40 ? 40 : tmp1.Length);
                gb.W_1550_case_text2 = tmp2.Substring(0, tmp2.Length > 40 ? 40 : tmp2.Length);
                gb.W_1550_case_text3 = tmp3.Substring(0, tmp3.Length > 40 ? 40 : tmp3.Length);
                gb.W_1550_case_text4 = tmp4.Substring(0, tmp4.Length > 16 ? 16 : tmp4.Length);
                gb.W_1550_case_text7 = DaHelper.GetString(dr, "w_1550_case_text7").Trim();
                gb.W_1550_case_text8 = DaHelper.GetString(dr, "w_1550_case_text8").Trim();
                gb.W_1570_account_number = DaHelper.GetString(dr, "w_1570_account_number").Trim();

                string tmp5 = DaHelper.GetString(dr, "w_1581_bank_due_date").Trim();
                if (tmp5.Length == 8)
                {
                    if (tmp5.Substring(6, 2) == "80")
                        gb.W_1581_bank_due_date = string.Empty;
                    else
                        gb.W_1581_bank_due_date = tmp5;
                }
                else
                    gb.W_1581_bank_due_date = string.Empty;

                gb.W_1610_invoice = DaHelper.GetString(dr, "w_1610_invoice").Trim();
                gb.W_1620_buss_name = DaHelper.GetString(dr, "w_1620_buss_name").Trim();
                gb.W_1631_1632_name = DaHelper.GetString(dr, "w_1631_1632_name").Trim();
                gb.W_1633_name = DaHelper.GetString(dr, "w_1633_name").Trim();
                gb.W_1640_device_13_l1 = DaHelper.GetString(dr, "w_1640_device_13_l1").Trim();
                gb.W_1650_rate_cat_13_l2 = DaHelper.GetString(dr, "w_1650_rate_cat_13_l2").Trim();
                gb.W_1660_contract_ac_14_l1 = DaHelper.GetString(dr, "w_1660_contract_ac_14_l1").Trim();
                gb.W_1670_period_15_l1 = DaHelper.GetString(dr, "w_1670_period_15_l1").Trim();
                gb.W_1680_mr_date_15_l2 = DaHelper.GetString(dr, "w_1680_mr_date_15_l2").Trim();
                gb.W_1690_unit_after_16_l1 = DaHelper.GetString(dr, "w_1690_unit_after_16_l1").Trim();
                gb.W_1700_unit_previous_16_l2 = DaHelper.GetString(dr, "w_1700_unit_previous_16_l2").Trim();
                gb.W_1710_consumption_17_l1 = DaHelper.GetString(dr, "w_1710_consumption_17_l1").Trim();
                gb.W_1720_based_amount_18_l1 = DaHelper.GetString(dr, "w_1720_based_amount_18_l1").Trim();
                gb.W_1730_discount_amount_19_l1 = DaHelper.GetString(dr, "w_1730_discount_amount_19_l1").Trim();
                gb.W_1740_disct_descript = DaHelper.GetString(dr, "w_1740_disct_descript").Trim();
                gb.W_1750_baht = DaHelper.GetString(dr, "w_1750_baht").Trim();
                gb.W_1760_ft_amount_20_l1 = DaHelper.GetString(dr, "w_1760_ft_amount_20_l1").Trim();
                gb.W_1770_ft_price_20_l2 = DaHelper.GetString(dr, "w_1770_ft_price_20_l2").Trim();
                gb.W_1780_net_before_tax_21_l1 = DaHelper.GetString(dr, "w_1780_net_before_tax_21_l1").Trim();
                gb.W_1790_tax_amount_22_l1 = DaHelper.GetString(dr, "w_1790_tax_amount_22_l1").Trim();
                gb.W_1800_tax_rate_22_l2 = DaHelper.GetString(dr, "w_1800_tax_rate_22_l2").Trim();
                gb.W_1810_total_value_23_l1 = DaHelper.GetString(dr, "w_1810_total_value_23_l1").Trim();
                gb.W_1820_payment_date_24_l1 = DaHelper.GetString(dr, "w_1820_payment_date_24_l1").Trim();
                gb.BillSeqNo = DaHelper.GetString(dr, "BillSeqNo").Trim();
                gb.W_2070_taxid_p = DaHelper.GetString(dr, "w_2070_taxid_p").Trim();
                gb.W_2080_taxid_c = DaHelper.GetString(dr, "w_2080_taxid_c").Trim();

                _greenBill.Add(gb);
            }

            return _greenBill;
        }

        #endregion

        #region "Function"
        
        private string ConvertToMonthName(string id)
        {
            string name = string.Empty;
            switch (id)
            {
                case "01":
                    name = "มกราคม";
                    break;
                case "02":
                    name = "กุมภาพันธ์";
                    break;
                case "03":
                    name = "มีนาคม";
                    break;
                case "04":
                    name = "เมษายน";
                    break;
                case "05":
                    name = "พฤษภาคม";
                    break;
                case "06":
                    name = "มิถุนายน";
                    break;
                case "07":
                    name = "กรกฏาคม";
                    break;
                case "08":
                    name = "สิงหาคม";
                    break;
                case "09":
                    name = "กันยายน";
                    break;
                case "10":
                    name = "ตุลาคม";
                    break;
                case "11":
                    name = "พฤศจิกายน";
                    break;
                case "12":
                    name = "ธันวาคม";
                    break;
                default:
                    name = "XX";
                    break;
            }
            return name;
        }

        #endregion
    }
}
