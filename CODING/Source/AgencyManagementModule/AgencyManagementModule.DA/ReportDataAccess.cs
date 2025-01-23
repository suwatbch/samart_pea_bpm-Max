using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.DA
{
    public class ReportDataAccess : IReportRepository
    {

        public List<CAB05_01DetailReportInfo> GetCAB05_01(string branchId, string startAgencyId, string endAgencyId, string periodStart, string periodEnd)
        {
            List<CAB05_01DetailReportInfo> retVals = new List<CAB05_01DetailReportInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAB05_01");

            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pStartAgencyId", DbType.String, startAgencyId);
            db.AddInParameter(cmd, "pEndAgencyId", DbType.String, endAgencyId);
            db.AddInParameter(cmd, "pPeriodStart", DbType.String, periodStart);
            db.AddInParameter(cmd, "pPeriodEnd", DbType.String, periodEnd);

            int rowNo = 0;
            string preAgencyId = String.Empty;
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    CAB05_01DetailReportInfo b = new CAB05_01DetailReportInfo();
                    b.AgencyId = DaHelper.GetString(r, "CaId").Substring(4);
                    if (b.AgencyId != preAgencyId)
                    {
                        rowNo = rowNo + 1;
                    }
                    b.RowNo = rowNo;
                    b.Period = DaHelper.GetBillPeriod(DaHelper.GetString(r, "BookPeriod"));
                    b.BranchId = DaHelper.GetString(r, "BranchId");
                    b.BranchName = DaHelper.GetString(r, "BranchName");
                    b.ReceiveNo = DaHelper.GetByte(r, "ReceiveCount").Value.ToString();
                    b.BookLot = DaHelper.GetInt(r, "BookLot").Value.ToString();
                    b.AgencyName = DaHelper.GetString(r, "CaName");
                    b.RegisterDt = DaHelper.GetDate(r, "ContractValidFrom").Value.Year < 1950 ? "" : DaHelper.GetDate(r, "ContractValidFrom").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.SecurityDeposit = DaHelper.GetDecimal(r, "SecurityDeposit");
                    b.CollectValue = DaHelper.GetDecimal(r, "totalValue");
                    b.ReceiveCount = DaHelper.GetInt(r, "TotalBill");
                    b.CollectAmount = DaHelper.GetDecimal(r, "BookPaidAmount");
                    b.CollectCount = DaHelper.GetInt(r, "TotalBillCollected");
                    b.RecevieAmount = DaHelper.GetDecimal(r, "BookTotalAmount");
                    b.BillBookId = DaHelper.GetString(r, "BillBookId").Substring(ModuleConfigurationNames.BranchCodeLength, ModuleConfigurationNames.BillBookLengthOnly) ;
                    if (b.RecevieAmount == 0 || b.ReceiveCount == 0)
                    {
                        b.PercentAmount = 0;
                        b.PercentCount = 0;
                    }
                    else
                    {
                        b.PercentAmount = b.CollectAmount / b.RecevieAmount * 100;
                        b.PercentCount = (decimal)b.CollectCount / (decimal)b.ReceiveCount * 100;
                    }
                    b.CollectArea = DaHelper.GetString(r, "CollectArea");

                    preAgencyId = b.AgencyId;
                    retVals.Add(b);
                }
            }
            return retVals;
        }

        public string GetMaxAgencyIdInBranch(string branchId)
        {
            string retVal = String.Empty;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_MaxAgencyIdInBranch");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);            
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<BillPasteReportInfo> billPasteList = new List<BillPasteReportInfo>();

            if (dt.Rows.Count == 1)
            {
                retVal = DaHelper.GetString(dt.Rows[0], "MaxAgencyId");
            }                     
            return retVal;
        }

        public string GetMinAgencyIdInBranch(string branchId)
        {
            string retVal = String.Empty;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_MinAgencyIdInBranch");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<BillPasteReportInfo> billPasteList = new List<BillPasteReportInfo>();

            if (dt.Rows.Count == 1)
            {
                retVal = DaHelper.GetString(dt.Rows[0], "MinAgencyId");
            }
            return retVal;
        }

        public List<PA_7034DetailReportInfo> GetPA_7034(PA_7034ConditionReportInfo conn)
        {
            //System.Diagnostics.Debugger.Break();

            List<PA_7034DetailReportInfo> retVals = new List<PA_7034DetailReportInfo>();
            string retVal = String.Empty;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_PA_7034");
            db.AddInParameter(cmd, "pPeriod", DbType.String, conn.Period);
            db.AddInParameter(cmd, "pBranchId", DbType.String, conn.BranchId);
            db.AddInParameter(cmd, "pAgencyIdFrom", DbType.String, conn.AgencyIdFrom.PadLeft(12,'0'));
            db.AddInParameter(cmd, "pAgencyIdTo", DbType.String, conn.AgencyIdTo.PadLeft(12,'0'));
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    PA_7034DetailReportInfo b = new PA_7034DetailReportInfo();
                    b.BranchId = DaHelper.GetString(r, "BranchId");
                    b.BranchName = DaHelper.GetString(r, "BranchName");
                    b.AgencyId = DaHelper.GetString(r, "CaId").Substring(4);
                    b.AgencyName = DaHelper.GetString(r,"CaName");
                    b.MruId = DaHelper.GetString(r,"MruId");
                    b.ReceiveNo = DaHelper.GetByte(r, "ReceiveCount").ToString() ;
                    b.BookLot = DaHelper.GetInt(r, "BookLot").Value.ToString();
                    b.BillBookId = DaHelper.GetString(r, "BillBookId").Substring(ModuleConfigurationNames.BranchCodeLength, ModuleConfigurationNames.BillBookLengthOnly);
                    b.CreateDate = DaHelper.GetDate(r, "CreateDate") == null ? String.Empty : DaHelper.GetDate(r, "CreateDate").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.CheckinDate = DaHelper.GetDate(r, "CheckInDate") == null ? String.Empty :  DaHelper.GetDate(r, "CheckInDate").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.BillBookType = DaHelper.GetString(r, "AboName");
                    b.ResidentReceiveCount = DaHelper.GetInt(r,"hrTotalBill");
                    b.ResidentReceiveAmount = DaHelper.GetDecimal(r,"hrTotalAmount");
                    b.SmallBizReceiveCount = DaHelper.GetInt(r,"srTotalBill");
                    b.SmallBizReceiveAmount = DaHelper.GetDecimal(r, "srTotalAmount");
                    b.GovReceiveCount = DaHelper.GetInt(r,"grTotalBill");
                    b.GovReceiveAmount = DaHelper.GetDecimal(r,"grTotalAmount");
                    b.ResidentCollectCount = DaHelper.GetInt(r,"hcTotalBill");
                    b.ResidentCollectAmount = DaHelper.GetDecimal(r,"hcTotalAmount");
                    b.SmallBizCollectCount = DaHelper.GetInt(r,"scTotalBill");
                    b.SmallBizCollectAmount = DaHelper.GetDecimal(r,"scTotalAmount");
                    b.GovCollectCount = DaHelper.GetInt(r,"gcTotalBill");
                    b.GovCollectAmount = DaHelper.GetDecimal(r, "gcTotalAmount");
                    b.PastBillCount = DaHelper.GetInt(r,"pastBill");		                
                    retVals.Add(b);
                }
            }
            return retVals;
        }

        public List<CAB12_01DetailReportInfo> GetCAB12_01(string branchId, CAB12_01ConditionReportInfo conn)
        {
            List<CAB12_01DetailReportInfo> retVals = new List<CAB12_01DetailReportInfo>();                
            string periodFrom  = String.Format("{0}{1}", conn.StartPeriod.Value.ToString("yyyy", new CultureInfo("th-TH")),  conn.StartPeriod.Value.Month.ToString().PadLeft(2, '0'));
            string periodTo  = String.Format("{0}{1}",conn.EndPeriod.Value.ToString("yyyy", new CultureInfo("th-TH")),  conn.EndPeriod.Value.Month.ToString().PadLeft(2, '0'));
            string retVal = String.Empty;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase"); //TODO: AdHoc รอ user confirm report version ใหม่
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAB12_01");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pAgencyIdFrom", DbType.String, conn.AgencyIdFrom.PadLeft(12, '0'));
            db.AddInParameter(cmd, "pAgencyIdTo", DbType.String, conn.AgencyIdTo.PadLeft(12,'0'));
            db.AddInParameter(cmd, "pPeriodFrom", DbType.String, periodFrom);
            db.AddInParameter(cmd, "pPeriodTo", DbType.String, periodTo);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];             
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    CAB12_01DetailReportInfo b = new CAB12_01DetailReportInfo();
                    string bhId = DaHelper.GetString(r, "BookHolderId");
                    b.BranchId = DaHelper.GetString(r, "BranchId");                       
                    b.CaId = DaHelper.GetString(r, "CaId");
                    b.MruId = DaHelper.GetString(r, "MruId");
                    b.CaName = DaHelper.GetString(r, "CaName");
                    b.CaAddress = DaHelper.GetString(r, "CaAddress");

                    b.AgencyId = bhId.Substring(bhId.Length - ModuleConfigurationNames.AgentIdLength, ModuleConfigurationNames.AgentIdLength);
                    b.AgencyName = DaHelper.GetString(r, "AgName");
                    b.StartPeriod = StringConvert.FormatPeriodThai(DaHelper.GetString(r, "Period"));
                    b.TotalDiffMonth = StringConvert.DiffPeriod(DaHelper.GetString(r, "Period"), conn.CurrPeriod);
                    b.YearStart = b.TotalDiffMonth / 12;
                    b.MonthStart = b.TotalDiffMonth % 12;
                    retVals.Add(b);
                }
            }
            return retVals;
        }


        public List<CAB12_02DetailReportInfo> GetCAB12_02(string branchId, CAB12_01ConditionReportInfo conn)
        {
            List<CAB12_02DetailReportInfo> retVals = new List<CAB12_02DetailReportInfo>();
            string periodFrom = String.Format("{0}{1}", conn.StartPeriod.Value.ToString("yyyy", new CultureInfo("th-TH")), conn.StartPeriod.Value.Month.ToString().PadLeft(2, '0'));
            string periodTo = String.Format("{0}{1}", conn.EndPeriod.Value.ToString("yyyy", new CultureInfo("th-TH")), conn.EndPeriod.Value.Month.ToString().PadLeft(2, '0'));
            string retVal = String.Empty;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase"); //TODO: AdHoc รอ user confirm report version ใหม่
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAB12_02");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pAgencyIdFrom", DbType.String, conn.AgencyIdFrom);
            db.AddInParameter(cmd, "pAgencyIdTo", DbType.String, conn.AgencyIdTo);
            db.AddInParameter(cmd, "pPeriodFrom", DbType.String, periodFrom);
            db.AddInParameter(cmd, "pPeriodTo", DbType.String, periodTo);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    CAB12_02DetailReportInfo b = new CAB12_02DetailReportInfo();
                    b.BranchId = DaHelper.GetString(r, "BranchId");
                    b.BranchName = DaHelper.GetString(r, "BranchName");
                    b.TotalCount = DaHelper.GetInt(r, "TotalCount");
                    retVals.Add(b);
                }
            }
            return retVals;
        }

        public List<CAN34_01DetailReportInfo> GetCAN34_01(string branchId, CAN34_01CondtionReportInfo conn)
        {
            List<CAN34_01DetailReportInfo> retVals = new List<CAN34_01DetailReportInfo>();
            string period = conn.Period;
            string retVal = String.Empty;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase"); //TODO: AdHoc รอ user confirm report version ใหม่
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAN34_01");
            db.AddInParameter(cmd, "pPeriod", DbType.String, conn.Period);
            db.AddInParameter(cmd, "pAgencyId", DbType.String, conn.AgencyId == String.Empty ? null : conn.AgencyId);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);             
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            int rowNo = 0;
            string preAgencyId = String.Empty;
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    CAN34_01DetailReportInfo b = new CAN34_01DetailReportInfo();
                    string agId = DaHelper.GetString(r, "CaId");
                    b.AgencyId = agId == null ? String.Empty : agId.Substring(agId.Length - ModuleConfigurationNames.AgentIdLength, ModuleConfigurationNames.AgentIdLength);                         
                    if (b.AgencyId != preAgencyId)
                    {
                        rowNo = rowNo + 1;
                    }
                    b.RowNo = rowNo;                        
                    b.AgencyName = DaHelper.GetString(r,"caName");
                    b.BranchId = DaHelper.GetString (r,"BranchId");
                    b.BranchName = DaHelper.GetString(r, "BranchName");
                    b.MruId = DaHelper.GetString(r,"MruId");
                    b.BillCount =  DaHelper.GetInt(r,"TotalBill");
                    b.BillAmount = DaHelper.GetDecimal(r,"TotalAmount");			            
                    b.PortionId = DaHelper.GetString(r,"PortionId");
                    b.MeterReadDt = DaHelper.GetDate(r,"MeterReadDt") == null ? String.Empty : DaHelper.GetDate(r,"MeterReadDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.MeterReadActDt = DaHelper.GetDate(r, "MeterReadActDt") == null ? String.Empty : DaHelper.GetDate(r, "MeterReadActDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.TransferDt = DaHelper.GetDate(r,"TransferDt") == null ? String.Empty : DaHelper.GetDate(r,"TransferDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.TransferActDt = DaHelper.GetDate(r, "TransferActDt") == null ? String.Empty : DaHelper.GetDate(r, "TransferActDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.PrintBillDt = DaHelper.GetDate(r,"BillPrintDt") == null ? String.Empty : DaHelper.GetDate(r,"BillPrintDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.PrintBillActDt = DaHelper.GetDate(r, "BillPrintActDt") == null ? String.Empty : DaHelper.GetDate(r, "BillPrintActDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.CreateBookDt = DaHelper.GetDate(r,"BookCreatedDt") == null ? String.Empty : DaHelper.GetDate(r,"BookCreatedDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.CreateBookActDt = DaHelper.GetDate(r, "BookCreateActDt") == null ? String.Empty : DaHelper.GetDate(r, "BookCreateActDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.CheckInDt = DaHelper.GetDate(r, "BookCheckInDt") == null ? String.Empty :  DaHelper.GetDate(r,"BookCheckInDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    b.CheckInActDt = DaHelper.GetDate(r, "BookCheckInActDt") == null ? String.Empty : DaHelper.GetDate(r, "BookCheckInActDt").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")); ;
                    b.PlanTotalDt = DaHelper.GetInt(r, "TotalPlanDay");
                    b.ActTotalDt = DaHelper.GetInt(r, "TotalActDay");

                    retVals.Add(b);
                    preAgencyId = b.AgencyId;
                }
            }
            return retVals;
        }

        public List<CAB10_01DetailReportInfo> GetCAB10_01(string branchId, string billPeriod, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase"); //TODO: AdHoc รอ user confirm report version ใหม่
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAB10_01");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, billPeriod);               
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            int rowId = 0;
            string pId = String.Empty;
            string bId = String.Empty;                
            List<CAB10_01DetailReportInfo> retVals = new List<CAB10_01DetailReportInfo>();
            foreach (DataRow r in dt.Rows)
            {
                CAB10_01DetailReportInfo item = new CAB10_01DetailReportInfo();
                if ((bId == DaHelper.GetString(r, "BranchId")) &&
                    (pId == DaHelper.GetString(r, "PortionId")))
                {
                    item.RowId = rowId.ToString();
                }
                else
                {
                    rowId += 1;
                    item.RowId = rowId.ToString();
 
                }

                pId = DaHelper.GetString(r, "PortionId");
                bId = DaHelper.GetString(r, "BranchId");
                           
                item.BranchId = DaHelper.GetString(r, "BranchId");
                item.BranchName = DaHelper.GetString(r, "BranchName");                    
                item.PortionId = DaHelper.GetString(r, "PortionId");
                item.MeterReadDt = DaHelper.GetDate(r, "MeterReadDt");
                item.MeterReadActDt = DaHelper.GetDate(r, "MeterReadActDt");
                item.TransferDt = DaHelper.GetDate(r, "TransferDt");
                item.TransferActDt = DaHelper.GetDate(r, "TransferActDt");
                item.BillPrintDt = DaHelper.GetDate(r, "BillPrintDt");
                item.BillPrintActDt = DaHelper.GetDate(r, "BillPrintActDt");
                item.BookCreatedDt = DaHelper.GetDate(r, "BookCreatedDt");
                item.BookCreatedActDt = DaHelper.GetDate(r, "BookCreateActDt");

                item.BookCheckInDt = DaHelper.GetDate(r, "BookCheckInDt");
                item.BookCheckInActDt = DaHelper.GetDate(r, "BookCheckInActDt");

                item.AgencyId = DaHelper.GetString(r, "CaId") == null ? String.Empty : DaHelper.GetString(r, "CaId").Substring(4);                                                                                                                                      

                item.MinMruId = DaHelper.GetString(r, "MinMruId");
                item.MaxMruId = DaHelper.GetString(r, "MaxMruId");                
                retVals.Add(item);
            }
            return retVals;
        }

        public decimal? GetIntownReceive(string billbookId)
        {
            decimal? retVal = 0;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ag_get_IntownReceive");
            db.AddInParameter(cmd, "pBillBookId", DbType.String, billbookId);
            DataSet data = db.ExecuteDataSet(cmd);
            if (data.Tables.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                retVal = DaHelper.GetDecimal(row, "IntownReceive");
            }

            return retVal;
        }

        public List<CAB07_01DetailReportInfo> GetCAB07_01(string period, string branchId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase"); //TODO: AdHoc รอ user confirm report version ใหม่
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAB07_01");
            db.AddInParameter(cmd, "pPeriod", DbType.String, period);
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifiedBy);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            List<CAB07_01DetailReportInfo> myAllResume = new List<CAB07_01DetailReportInfo>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {                  
                DataRow r = dt.Rows[i];
                CAB07_01DetailReportInfo myResult = new CAB07_01DetailReportInfo();
                myResult.BranchId = DaHelper.GetString(r, "BranchId");
                myResult.BranchName = DaHelper.GetString(r, "BranchName");
                myResult.MRUId = DaHelper.GetString(r, "MruId");
                myResult.CaCount = DaHelper.GetInt(r, "CaCount");
                myResult.TotalAmount = DaHelper.GetDecimal(r, "TotalAmount");
                myResult.AgentId = DaHelper.GetString(r, "CaId") == null ? String.Empty : DaHelper.GetString(r, "CaId").Substring(4);
                myResult.PortionId = DaHelper.GetString(r, "PortionId");
                myResult.CollectCount = GetCollectCountFromPortion(myResult.PortionId); 
                myResult.PrintTime = DaHelper.GetInt(r, "PrintTime");

                myAllResume.Add(myResult);
            }
            return myAllResume;

            //                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            //                DbCommand cmd = db.GetStoredProcCommand("ag_get_AmountItemBillAndMoneyInEachBillBookByPeriodAndBranchId");
            //                db.AddInParameter(cmd, "Period", DbType.String, period);
            //                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            //                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];               
            //                List<BillDetailOfKeepMoneyPlanInfo> myAllResume = new List<BillDetailOfKeepMoneyPlanInfo>();
            //                for (int i = 0; i <= dt.Rows.Count - 1; i++)
            //                {
            //                    DataRow r = dt.Rows[i];
            //                    BillDetailOfKeepMoneyPlanInfo myResult = new BillDetailOfKeepMoneyPlanInfo();
            //                    myResult.BranchId = DaHelper.GetString(r, "BranchId");
            //                    myResult.BranchName = DaHelper.GetString(r, "BranchName");
            //                    myResult.LineNo = DaHelper.GetString(r, "MruId");
            //                    myResult.BillCount = DaHelper.GetInt(r, "BillCount");
            //                    myResult.Amount = DaHelper.GetDecimal(r, "Amount");
            //                    myResult.AgentId = DaHelper.GetString(r, "agentId");
            //                    myResult.AreaCode = DaHelper.GetString(r, "PortionId"); ;
            //                    // : TODO file KeepTimeNo
            //                    myResult.KeepTimeNo = String.Empty;// DaHelper.GetString(r, "KeepMoneyNo");
            //                    //not clear query from PrintPeriod filed in MRUPlan table not BillControllerId in MRU table
            ////                    myResult.PeriodTimeNo = DaHelper.GetString(r, "BillControllerId"); ;
            //                    myResult.Remark = "";
            //                    myAllResume.Add(myResult);
            //                }
            //                return myAllResume;
        }

        public List<CAB08_01DetailReportInfo> GetCAB08_01(string branchId, string billPeriod, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase"); //TODO: AdHoc รอ user confirm report version ใหม่
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAB08_01");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, billPeriod);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifiedBy);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            //int rowNo = 0;
            List<CAB08_01DetailReportInfo> items = new List<CAB08_01DetailReportInfo>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow r = dt.Rows[i];
                CAB08_01DetailReportInfo item = new CAB08_01DetailReportInfo();
                item.SeqNo = i + 1;
                item.BranchId = DaHelper.GetString(r, "BranchId");
                item.BranchName = DaHelper.GetString(r, "BranchName");
                item.PrintPeriod = DaHelper.GetDate(r, "PrintTime") == null ? String.Empty : DaHelper.GetDate(r, "PrintTime").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")); ;
                item.PortionId = DaHelper.GetString(r, "PortionId");
                item.AgencyId = DaHelper.GetString(r, "CaId") == String.Empty ? String.Empty : DaHelper.GetString(r, "CaId").Substring(4);
                //item.BookHolderId = DaHelper.GetString(r, "BookHolderId") == String.Empty ? String.Empty : DaHelper.GetString(r, "BookHolderId").Substring(4);
                item.SecurityDeposit = DaHelper.GetDecimal(r, "SecurityDeposit");
                item.MruId = DaHelper.GetString(r, "MruId");
                item.CaCount = DaHelper.GetInt(r, "CaCount");
                item.TotalAmount = DaHelper.GetDecimal(r, "TotalAmount");
                items.Add(item);

            }
            return items;
        }

        public List<CAB09_01DetailReportInfo> GetCAB09_01(string branchId, string billPeriod, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase"); //TODO: AdHoc รอ user confirm report version ใหม่
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAB09_01");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, billPeriod);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifiedBy);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            string preCollectCount = String.Empty;
            CAB09_01DetailReportInfo item = new CAB09_01DetailReportInfo();

            List<CAB09_01DetailReportInfo> retVals = new List<CAB09_01DetailReportInfo>();
            foreach (DataRow r in dt.Rows)
            {
                if ((item.BranchId == DaHelper.GetString(r, "BranchId"))
                    && (item.PortionId == DaHelper.GetString(r, "PortionId")))
                {
                    string mruId = DaHelper.GetString(r, "MruId");
                    string agencyId = DaHelper.GetString(r, "BookHolderId");

                    if (!item.MruList.Contains(mruId))
                        item.MruList.Add(mruId);
                    if (!item.AgencyIdList.Contains(agencyId))
                        item.AgencyIdList.Add(agencyId);
                    item.ActCaCount += DaHelper.GetInt(r, "ActCaCount") == null ? 0 : DaHelper.GetInt(r, "ActCaCount");
                    item.ActTotalAmount += DaHelper.GetDecimal(r, "ActTotalAmount") == null ? 0 : DaHelper.GetDecimal(r, "ActTotalAmount");
                    item.PlanCaCount += DaHelper.GetInt(r, "PlanCaCount") == null ? 0 : DaHelper.GetInt(r, "PlanCaCount");
                    item.PlanTotalAmount += DaHelper.GetDecimal(r, "PlanTotalAmount") == null ? 0 : DaHelper.GetDecimal(r, "PlanTotalAmount");
                    //item.AgCount += DaHelper.GetInt(r, "AgCount") == null ? 0 : DaHelper.GetInt(r, "AgCount");
                }
                else
                {
                    if ((item.BranchId != String.Empty) && (item.PortionId != String.Empty))
                    {
                        retVals.Add(item);
                    }
                    item = new CAB09_01DetailReportInfo();
                    string mruId = DaHelper.GetString(r, "MruId");
                    string agencyId = DaHelper.GetString(r, "BookHolderId");

                    if (!item.MruList.Contains(mruId))
                        item.MruList.Add(mruId);                       
                    if (!item.AgencyIdList.Contains(agencyId))
                        item.AgencyIdList.Add(agencyId);

                    item.PortionId = DaHelper.GetString(r, "PortionId");
                    item.BookCreateDay = DaHelper.GetDate(r, "BookCreateDay") == null ? String.Empty : DaHelper.GetDate(r, "BookCreateDay").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")); 
                    item.ActCaCount = DaHelper.GetInt(r, "ActCaCount");
                    item.ActTotalAmount = DaHelper.GetDecimal(r, "ActTotalAmount");
                    item.PlanCaCount = DaHelper.GetInt(r, "PlanCaCount");
                    item.PlanTotalAmount = DaHelper.GetDecimal(r, "PlanTotalAmount");                        
                    item.BranchId = DaHelper.GetString(r, "BranchId");
                    item.BranchName = DaHelper.GetString(r, "BranchName");
                }
            }
            // add last item
            if ((item.BranchId != String.Empty) && (item.PortionId != String.Empty))
            {
                retVals.Add(item);
            }
            return retVals;
            /*
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ag_set_CAB09_01");
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "Period", DbType.String, billPeriod);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                List<CAB09_01DetailReportInfo> myAllResume = new List<CAB09_01DetailReportInfo>();
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DataRow r = dt.Rows[i];
                    CAB09_01DetailReportInfo myResult = new CAB09_01DetailReportInfo();
                    myResult.KeepPeriodNo = Convert.ToInt16(DaHelper.GetString(r, "PeriodKeepMoney")).ToString() ;
                    myResult.AreaCode = DaHelper.GetString(r, "PortionId");
                    myResult.PaidDate = DaHelper.GetString(r, "PaidDate");
                    myResult.RangeOfLine = DaHelper.GetString(r, "RangeOfLine");
                    myResult.BranchId = DaHelper.GetString(r, "BranchId");
                    myResult.BranchName = DaHelper.GetString(r, "BranchName");
                    if (DaHelper.GetInt(r, "ListOfQuanlity") == null)
                    { myResult.LineOfQuanlity = 0; }
                    else
                    { myResult.LineOfQuanlity = DaHelper.GetInt(r, "CustomerOfQuanlity"); }

                    if (DaHelper.GetInt(r, "CustomerOfQuanlity") == null)
                    { myResult.CustomerOfQuanlity = 0; }
                    else
                    { myResult.CustomerOfQuanlity = DaHelper.GetInt(r, "CustomerOfQuanlity"); }

                    if (DaHelper.GetDecimal(r, "PercentCustomer") == null)
                    { myResult.PercentOfCustomer = (decimal)0.00; }
                    else
                    { myResult.PercentOfCustomer = DaHelper.GetDecimal(r, "PercentCustomer"); }

                    if (DaHelper.GetDecimal(r, "TotalAmount") == null)
                    { myResult.TotalAmount = (decimal)0.00; }
                    else
                    { myResult.TotalAmount = DaHelper.GetDecimal(r, "TotalAmount"); }

                    if (DaHelper.GetDecimal(r, "PercentAmount") == null)
                    { myResult.PercentOfAmount = (decimal)0.00; }
                    else
                    { myResult.PercentOfAmount = DaHelper.GetDecimal(r, "PercentAmount"); }

                    if (DaHelper.GetInt(r, "AgencyCounter") == null)
                    { myResult.QuanlityOfAgency = 0; }
                    else
                    { myResult.QuanlityOfAgency = DaHelper.GetInt(r, "AgencyCounter"); }

                    if (DaHelper.GetInt(r, "TotalOfKeepTimeNo") == null)
                    { myResult.TotalOfKeepTimeNo = 0; }
                    else
                    { myResult.TotalOfKeepTimeNo = DaHelper.GetInt(r, "TotalOfKeepTimeNo"); }

                    if (DaHelper.GetInt(r, "TotalOfBranch") == null)
                    { myResult.TotalOfBranch = 0; }
                    else
                    { myResult.TotalOfBranch = DaHelper.GetInt(r, "TotalOfBranch"); }

                    if (DaHelper.GetDecimal(r, "AmountOfKeepTimeNo") == null)
                    { myResult.TotalMoneyOfKeepTimeNo = (decimal)0.00; ; }
                    else
                    { myResult.TotalMoneyOfKeepTimeNo =
             * 
             * 
             * DaHelper.GetDecimal(r, "AmountOfKeepTimeNo"); }

                    if (DaHelper.GetDecimal(r, "AmountOfBranch") == null)
                    { myResult.TotalMoneyOfBranch = (decimal)0.00; ; }
                    else
                    { myResult.TotalMoneyOfBranch = DaHelper.GetDecimal(r, "AmountOfBranch"); }

                    myAllResume.Add(myResult);
                }
                return myAllResume;
            }
            catch (Exception ex)
            {
                throw;
            }
          */
        }

        public List<CAB08_02DetailReportInfo> GetCAB08_02(string branchId, string billPeriod, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase"); //TODO: AdHoc รอ user confirm report version ใหม่
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAB08_02");
            db.AddInParameter(cmd, "pBranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "pPeriod", DbType.String, billPeriod);
            db.AddInParameter(cmd, "pModifiedBy", DbType.String, modifiedBy);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            //System.Diagnostics.Debugger.Break();

            string _tempAgency = String.Empty;
            int i = 1;
            List<CAB08_02DetailReportInfo> retVals = new List<CAB08_02DetailReportInfo>();
            CAB08_02DetailReportInfo item = new CAB08_02DetailReportInfo();
            foreach (DataRow r in dt.Rows)
            {                    
                if ((item.BranchId == DaHelper.GetString(r, "BranchId")) &&
                    (item.PortionId == DaHelper.GetString(r, "PortionId")) &&
                    (item.BookCreateDay == DaHelper.GetDate(r, "BookCreateDay").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"))) &&
                    (item.AgencyId == DaHelper.GetString(r, "CaId")))
                {
                    item.MruList.Add(DaHelper.GetString(r, "MruId"));
                    item.PlanCaCount += DaHelper.GetInt(r, "PlanCaCount") == null ? 0 : DaHelper.GetInt(r, "PlanCaCount");
                    item.PlanTotalAmount += DaHelper.GetDecimal(r, "PlanTotalAmount") == null ? 0 : DaHelper.GetDecimal(r, "PlanTotalAmount");
                    item.MruCount += DaHelper.GetInt(r, "MruCount") == null ? 0 : DaHelper.GetInt(r, "MruCount");
                    item.CaCount += DaHelper.GetInt(r, "CaCount") == null ? 0 : DaHelper.GetInt(r, "CaCount");
                    item.RowNo = i;
                }
                else
                {
                    if ((item.BranchId != String.Empty) && (item.PortionId != String.Empty)
                        && (item.BookCreateDay != String.Empty) && (item.AgencyId != String.Empty) )
                    {
                        retVals.Add(item);                           
                    }                        
                    item = new CAB08_02DetailReportInfo();
                    item.RowNo = i;
                    item.MruList.Add(DaHelper.GetString(r, "MruId"));                       
                    item.BranchId = DaHelper.GetString(r, "BranchId");
                    item.BranchName = DaHelper.GetString(r, "BranchName");
                    item.PortionId = DaHelper.GetString(r, "PortionId");
                    item.AgencyId = DaHelper.GetString(r, "CaId") == null ? String.Empty : DaHelper.GetString(r, "CaId").Substring(4);
                    item.SecurityDeposit = DaHelper.GetDecimal(r, "SecurityDeposit");
                    item.BookCreateDay = DaHelper.GetDate(r, "BookCreateDay") == null ? String.Empty : DaHelper.GetDate(r, "BookCreateDay").Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));                   
                       
                    item.PlanCaCount = DaHelper.GetInt(r, "PlanCaCount");
                    item.PlanTotalAmount = DaHelper.GetDecimal(r, "PlanTotalAmount");
                    item.MruCount = DaHelper.GetInt(r, "MruCount");
                    item.CaCount = DaHelper.GetInt(r, "CaCount");
                }
                if (item.AgencyId != _tempAgency)
                    i = i + 1;
                _tempAgency = item.AgencyId; 

                    
            }
            // add last item
            if ((item.BranchId != String.Empty) && (item.PortionId != String.Empty))
            {
                retVals.Add(item);
            }
            /*
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DataRow r = dt.Rows[i];
                    CAB08_02DetailReportInfo myResult = new CAB08_02DetailReportInfo();
                    myResult.PeriodKeepMoney = Convert.ToInt16(DaHelper.GetString(r, "PeriodKeepMoney")).ToString();
                    myResult.PortionId = DaHelper.GetString(r, "PortionId");
                    myResult.AgentId = DaHelper.GetString(r, "AgentId");

                    if (DaHelper.GetDecimal(r, "CreditMoney") == null)
                    { myResult.CreditMoney = (decimal)0.00; }
                    else
                    { myResult.CreditMoney = DaHelper.GetDecimal(r, "CreditMoney"); }

                    //TODO : convert paiddate to format dd/mm/yyyy
                    myResult.PaidDate = Convert.ToString(DaHelper.GetInt(r, "PaidDate"));
                    myResult.RangeOfLine = DaHelper.GetString(r, "RangeOfLine");

                    if (DaHelper.GetInt(r, "CustomerOfQuanlity") == null)
                    { myResult.CustomerOfQuanlity = 0; }
                    else
                    { myResult.CustomerOfQuanlity = DaHelper.GetInt(r, "CustomerOfQuanlity"); }

                    if (DaHelper.GetDecimal(r, "TotalAmount") == null)
                    { myResult.TotalAmount = (decimal)0.00; }
                    else
                    { myResult.TotalAmount = DaHelper.GetDecimal(r, "TotalAmount"); }
                    myResult.BranchId = DaHelper.GetString(r, "BranchId");
                    myResult.BranchName = DaHelper.GetString(r, "BranchName");

                    myAllResume.Add(myResult);
                }
                 */
            return retVals;
        }

        public string GetCollectCountFromPortion(string portionId)
        {
            if (portionId == String.Empty)
                return String.Empty;
            else if (portionId.Length != 2)
                return String.Empty;
            else 
            {
                string preifx = portionId.Substring(0, 1);
                switch (preifx)
                {
                    case "A" :
                        return "1";
                    case "B" :
                        return "2";
                    case "C" :
                        return "3";
                    case "D" :
                        return "4";
                    default :
                        return String.Empty;
                }
            }
        }

        public List<CAB13_01RptInfo> GetRptCAB13_01RptInfoData(CAB13_01ConditionRptInfo condition)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase"); //TODO: AdHoc รอ user confirm report version ใหม่
            DbCommand cmd = db.GetStoredProcCommand("ag_sel_CAB13_01");
            db.AddInParameter(cmd, "BranchId", DbType.String, condition.BranchId);
            db.AddInParameter(cmd, "RunningBranchId", DbType.String, condition.RunningBranchId);
            db.AddInParameter(cmd, "Period", DbType.String, condition.Period);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            List<CAB13_01RptInfo> items = new List<CAB13_01RptInfo>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow r = dt.Rows[i];
                CAB13_01RptInfo item = new CAB13_01RptInfo();
                item.SeqNo = i + 1;
                item.BranchId = DaHelper.GetString(r, "BranchId");
                item.BranchName = DaHelper.GetString(r, "BranchName");
                item.TotalPersonAgency = DaHelper.GetInt(r, "TotalPAgency");
                item.TotalGovAgency = DaHelper.GetInt(r, "TotalGAgency");
                item.TotalAgency = DaHelper.GetInt(r, "TotalAgency");
                item.OverNinety = DaHelper.GetInt(r, "OverNinety");
                item.TotalBill = DaHelper.GetInt(r, "TotalBill");
                item.BookTotalAmount = DaHelper.GetDecimal(r, "BookTotalAmount");
                item.TotalBillCollect = DaHelper.GetInt(r, "TotalBillCollected");
                item.BookPaidAmount = DaHelper.GetDecimal(r, "BookPaidAmount");
                item.BaseAmount = DaHelper.GetDecimal(r, "BaseAmount");
                item.SpecialAmount = DaHelper.GetDecimal(r, "SpecialCmAmount");
                item.InvCmAmount = DaHelper.GetDecimal(r, "InvCmAmount");
                item.TransCost = DaHelper.GetDecimal(r, "TransCost");
                item.District = DaHelper.GetString(r, "District");

                items.Add(item);

            }
            return items;
        }
    }
}
