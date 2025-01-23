using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.Architecture.CommonUtilities;
using System.ComponentModel;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Globalization;

using System.ServiceProcess;

namespace PEA.BPM.CashManagementModule.DA
{
    public class CashManagementDA
    {
        public const int CMD_TIMEOUT = 10000;

        #region TongKung

        public List<CashierWorkStatusInfo> IsOpenedWork(DbTransaction trans, OpenWorkParam param)
        {
            List<CashierWorkStatusInfo> list = new List<CashierWorkStatusInfo>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_get_IsOpenedWork");
            db.AddInParameter(cmd, "PosId", DbType.String, param.PosId);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "CashierId", DbType.String, param.CashierId);
            //db.AddInParameter(cmd, "Input", DbType.String, param.Input);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CashierWorkStatusInfo cw = new CashierWorkStatusInfo();
                cw.WorkId = DaHelper.GetString(dr, "WorkId");
                cw.CashierId = DaHelper.GetString(dr, "CashierId");
                cw.PosId = DaHelper.GetString(dr, "PosId");
                cw.Status = DaHelper.GetString(dr, "Status");
                cw.OpenWorkDt = DaHelper.GetDate(dr, "OpenWorkDt");
                cw.CloseWorkDt = DaHelper.GetDate(dr, "CloseWorkDt");
                cw.BranchId = DaHelper.GetString(dr, "BranchId");
                cw.DayCount = DaHelper.GetInt(dr, "DayCount");
                list.Add(cw);
            }
            return list;
        }

        public string GetWorkPosId(DbTransaction trans, string workId)
        {
            string currentPosId = null;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_get_WorkPosId");
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
                currentPosId = DaHelper.GetString(dr, "CurrentPosId");

            return currentPosId;
        }


        public string OpenWork(DbTransaction trans, OpenWorkParam param)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_ins_OpenWork");
            db.AddInParameter(cmd, "CashierId", DbType.String, param.CashierId);
            db.AddInParameter(cmd, "CashierName", DbType.String, param.CashierName);
            db.AddInParameter(cmd, "PosId", DbType.String, param.PosId);
            db.AddInParameter(cmd, "TerminalCode", DbType.String, param.TerminalCode);
            db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
            db.AddInParameter(cmd, "Status", DbType.String, param.Status);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, param.ModifiedBy);
            db.AddInParameter(cmd, "PostedBranchId", DbType.String, param.PostedBranchId);
            return db.ExecuteScalar(cmd, trans).ToString();
        }

        public bool IsSystemInitial(DbTransaction trans, string branchId, string workId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_get_IsSystemInitial");
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "workId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
                return false;
            else
                return true;
        }

        public List<CashierMoneyTransferInfo> LoadTransferedRequestItem(DbTransaction trans, string cashierId)
        {
            List<CashierMoneyTransferInfo> list = new List<CashierMoneyTransferInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_TransferedRequestItem");
            db.AddInParameter(cmd, "CashierId", DbType.String, cashierId);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CashierMoneyTransferInfo obj = new CashierMoneyTransferInfo();
                obj.TransferId = DaHelper.GetString(dr, "TransferId");
                obj.Sender = DaHelper.GetString(dr, "Sender");
                obj.FullName = DaHelper.GetString(dr, "FullName");
                obj.Description = obj.Sender + " (" + obj.FullName + ")";
                obj.CashAmt = Decimal.Round(DaHelper.GetDecimal(dr, "CashAmt").Value, 2);
                obj.ChequeAmt = Decimal.Round(DaHelper.GetDecimal(dr, "ChequeAmt").Value, 2);
                obj.Amount = obj.CashAmt + obj.ChequeAmt;
                list.Add(obj);
            }
            return list;
        }

        public List<CashierMoneyTransferInfo> LoadTransferStatusItem(DbTransaction trans, string cashierId)
        {
            List<CashierMoneyTransferInfo> list = new List<CashierMoneyTransferInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_TransferStatusItem");
            db.AddInParameter(cmd, "CashierId", DbType.String, cashierId);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CashierMoneyTransferInfo obj = new CashierMoneyTransferInfo();
                obj.TransferId = DaHelper.GetString(dr, "TransferId");
                obj.Receiver = DaHelper.GetString(dr, "Receiver");
                obj.Receiver = obj.Receiver == null ? null : obj.Receiver;
                obj.FullName = DaHelper.GetString(dr, "FullName");
                obj.Sender = DaHelper.GetString(dr, "Sender");
                obj.Sender = obj.Sender == null ? null : obj.Sender;
                obj.SenderName = DaHelper.GetString(dr, "SenderFullName");
                obj.Description = obj.Receiver + " (" + obj.FullName + ")";
                obj.CashAmt = Decimal.Round(DaHelper.GetDecimal(dr, "CashAmt").Value, 2);
                obj.ChequeAmt = Decimal.Round(DaHelper.GetDecimal(dr, "ChequeAmt").Value, 2);
                obj.Status = DaHelper.GetString(dr, "Status");
                obj.Amount = obj.CashAmt + obj.ChequeAmt;
                obj.NoOfChq = DaHelper.GetString(dr, "NoOfChq");
                list.Add(obj);
            }
            return list;
        }

        public void UpdateCashierMoneyTransfer(DbTransaction trans, string transferId, string status, string posId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_upd_MoneyTransferForAcceptingTransfer");
            db.AddInParameter(cmd, "TransferId", DbType.String, transferId);
            db.AddInParameter(cmd, "Status", DbType.String, status);
            db.AddInParameter(cmd, "ResponsePosId", DbType.String, posId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            db.ExecuteNonQuery(cmd, trans);
        }

        public string UpdateIncommingTransfer(DbTransaction trans, string workId, string transferId, string status, string posId,
                                                    string branchId, string postedServerId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_upd_IncommingTransfer");
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            db.AddInParameter(cmd, "TransferId", DbType.String, transferId);
            db.AddInParameter(cmd, "Status", DbType.String, status);
            db.AddInParameter(cmd, "ResponsePosId", DbType.String, posId);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "postedServerId", DbType.String, postedServerId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            return (string)db.ExecuteScalar(cmd, trans);
        }

        public void UpdateCashierMoneyFlowForWorkId(DbTransaction trans, string flowId, string workId, string branchId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_upd_MoneyFlowForWorkId");
            db.AddInParameter(cmd, "FlowId", DbType.String, flowId);
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            db.ExecuteNonQuery(cmd, trans);
        }        

        public OpenWorkInfo LoadOpeningBalance(DbTransaction trans, string cashierId, string flowType)
        {
            OpenWorkInfo obj = new OpenWorkInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_get_OpeningBalanceByCash");
            db.AddInParameter(cmd, "CashierId", DbType.String, cashierId);
            db.AddInParameter(cmd, "FlowType", DbType.String, flowType);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0)
            {
                obj.CashAmt = 0;
                obj.TotalAmt = 0;
            }
            else
            {
                obj.FlowId = DaHelper.GetString(dt.Rows[0], "FlowId");
                obj.CashAmt = Decimal.Round(DaHelper.GetDecimal(dt.Rows[0], "CashAmt").Value, 2);
                //Cash+Cheque in Store-Proc
                obj.TotalAmt = Decimal.Round(DaHelper.GetDecimal(dt.Rows[0], "TotalAmt").Value, 2);
            }
            return obj;
        }

        public List<ChequeInfo> LoadOpeningCheque(DbTransaction trans, string flowId)
        {
            List<ChequeInfo> list = new List<ChequeInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_OpeningBalanceByCheque");
            db.AddInParameter(cmd, "FlowId", DbType.String, flowId);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                ChequeInfo obj = new ChequeInfo();
                obj.ChqNo = DaHelper.GetString(dr, "ChqNo");
                obj.BankKey = DaHelper.GetString(dr, "BankKey");
                obj.BankName = DaHelper.GetString(dr, "BankName");
                obj.ChqDt = DaHelper.GetDate(dr, "ChqDt");
                obj.Amount = DaHelper.GetDecimal(dr, "Amount");
                list.Add(obj);
            }
            return list;
        }

        public void InsertAR(DbTransaction trans, string dtId, decimal? totalAmt, string sapRefNo, string branchId, string modifiedBy, string postedBranchId, string posId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_AR");
                db.AddInParameter(cmd, "SAPRefNo", DbType.String, sapRefNo);
                db.AddInParameter(cmd, "DtId", DbType.String, dtId);
                db.AddInParameter(cmd, "GAmount", DbType.Decimal, totalAmt);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
                db.AddInParameter(cmd, "PostedBranchId", DbType.String, postedBranchId);
                db.AddInParameter(cmd, "PosId", DbType.String, posId);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //da.InsertPayment(trans, param.PaymentDt, param.PosId, param.CashierId, param.BranchId)
        public string InsertPayment(DbTransaction trans, DateTime? paymentDt, string posId, string cashierId, string cashierName, string branchId,
                                string workId, string modifiedBy, string postedBranchId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_Payment");
                db.AddInParameter(cmd, "PosId", DbType.String, posId);
                db.AddInParameter(cmd, "CashierId", DbType.String, cashierId);
                db.AddInParameter(cmd, "CashierName", DbType.String, cashierName);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, paymentDt);
                db.AddInParameter(cmd, "WorkId", DbType.String, workId);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
                db.AddInParameter(cmd, "PostedBranchId", DbType.String, postedBranchId);
                return db.ExecuteScalar(cmd, trans).ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //da.InsertARPaymentType(trans, paymentId, param.PaymentMethodList)
        public string InsertARPaymentType(DbTransaction trans, string paymentId, PaymentMethodInfo obj, string branchId,
                                            string modifiedBy, string postedBranchId, string posId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_ARPaymentType");
                db.AddInParameter(cmd, "PaymentId", DbType.String, paymentId);
                db.AddInParameter(cmd, "PtId", DbType.String, obj.PtId);
                db.AddInParameter(cmd, "BankKey", DbType.String, obj.BankId);

                db.AddInParameter(cmd, "BankName", DbType.String, obj.BankName);

                db.AddInParameter(cmd, "ChqNo", DbType.String, obj.ChqNo);
                db.AddInParameter(cmd, "ChqAccNo", DbType.String, obj.ChqAccNo);
                db.AddInParameter(cmd, "ChqDt", DbType.DateTime, obj.ChqDt);
                db.AddInParameter(cmd, "ClearingAccNo", DbType.String, obj.ClearingAccNo);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, obj.Amount);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
                db.AddInParameter(cmd, "PostedBranchId", DbType.String, postedBranchId);
                db.AddInParameter(cmd, "PosId", DbType.String, posId);
                return db.ExecuteScalar(cmd, trans).ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //da.InsertARPayment(trans, param.SAPRefNo, param.PmId, param.TotalAmount, param.PaymentDt, param.Pending);
        //public string InsertARPayment(DbTransaction trans, string SAPRefNo, string pmId, decimal? totalAmount, DateTime? paymentDt,
        //                                string pending, string branchId, string modifiedBy, string postedBranchId)
        public string InsertARPayment(DbTransaction trans, string SAPRefNo, string pmId, decimal? amount, decimal? adjAmount, DateTime? paymentDt,
                                                string pending, string branchId, string modifiedBy, string postedBranchId, string posId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_ARPayment");
                db.AddInParameter(cmd, "SAPRefNo", DbType.String, SAPRefNo);
                db.AddInParameter(cmd, "PmId", DbType.String, pmId);

                //db.AddInParameter(cmd, "GAmount", DbType.Decimal, totalAmount);
                db.AddInParameter(cmd, "GAmount", DbType.Decimal, amount);
                db.AddInParameter(cmd, "AdjAmount", DbType.Decimal, adjAmount);

                db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, paymentDt);
                db.AddInParameter(cmd, "Pending", DbType.String, pending);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
                db.AddInParameter(cmd, "PostedBranchId", DbType.String, postedBranchId);
                db.AddInParameter(cmd, "PosId", DbType.String, posId);
                return db.ExecuteScalar(cmd, trans).ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //da.InsertRTARPaymentTypeARPayment(trans, arPmId, arPtIdList[i], param.PaymentMethodList[i].Amount);
        public void InsertRTARPaymentTypeARPayment(DbTransaction trans, string arPmId, string arPtId, decimal? amount, string branchId, string modifiedBy)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_RTARPaymentTypeARPayment");
                db.AddInParameter(cmd, "ARPmId", DbType.String, arPmId);
                db.AddInParameter(cmd, "ARPtId", DbType.String, arPtId);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, amount);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CancelAdjustOpenBalance(DbTransaction trans, string workId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_del_CancelAdjustOpenBalance");
                db.AddInParameter(cmd, "WorkId", DbType.String, workId);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PaymentMethodInfo> LoadSystemInitial(DbTransaction trans, string workId)
        {
            List<PaymentMethodInfo> list = new List<PaymentMethodInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_SystemInitial");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                PaymentMethodInfo pmi = new PaymentMethodInfo();
                pmi.PtId = DaHelper.GetString(dr, "PtId");
                pmi.PtName = DaHelper.GetString(dr, "PtName");
                pmi.Amount = DaHelper.GetDecimal(dr, "Amount");
                pmi.ChqItemId = DaHelper.GetString(dr, "ChqItemId");
                if (DaHelper.GetString(dr, "BankKey") != null)
                {
                    Bank bank = new Bank();
                    bank.BankKey = DaHelper.GetString(dr, "BankKey");
                    bank.BankName = DaHelper.GetString(dr, "BankName");
                    pmi.Bank = new Bank();
                    pmi.Bank = bank;
                }
                pmi.ChqNo = DaHelper.GetString(dr, "ChqNo");
                pmi.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                pmi.ChqDt = DaHelper.GetDateTime(dr, "ChqDt");
                list.Add(pmi);
            }
            return list;
        }

        public List<PaymentMethodInfo> LoadAdjustOpenBalance(DbTransaction trans, string workId)
        {
            List<PaymentMethodInfo> list = new List<PaymentMethodInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_LoadAdjustOpenBalance");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                PaymentMethodInfo pmi = new PaymentMethodInfo();
                pmi.FlowId = DaHelper.GetString(dr, "FlowId");
                pmi.PtId = DaHelper.GetString(dr, "PtId");
                pmi.PtName = DaHelper.GetString(dr, "PtName");
                pmi.FlowType = DaHelper.GetString(dr, "FlowType");
                pmi.FlowDesc = DaHelper.GetString(dr, "FlowDesc");
                pmi.Comment = DaHelper.GetString(dr, "Comment");
                pmi.Amount = DaHelper.GetDecimal(dr, "Amount");
                if (DaHelper.GetString(dr, "BankKey") != null)
                {
                    Bank bank = new Bank();
                    bank.BankKey = DaHelper.GetString(dr, "BankKey");
                    bank.BankName = DaHelper.GetString(dr, "BankName");
                    pmi.Bank = bank;
                }
                pmi.BankId = DaHelper.GetString(dr, "BankKey");
                pmi.ChqNo = DaHelper.GetString(dr, "ChqNo");
                pmi.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                pmi.ChqDt = DaHelper.GetDateTime(dr, "ChqDt");
                list.Add(pmi);
            }
            return list;
        }

        public List<PaymentMethodInfo> LoadMoneyCheckedIn(DbTransaction trans, string SAPRefNo, string workId)
        {
            try
            {
                List<PaymentMethodInfo> list = new List<PaymentMethodInfo>();

                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_sel_MoneyCheckedIn");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "SAPRefNo", DbType.String, SAPRefNo);
                db.AddInParameter(cmd, "WorkId", DbType.String, workId);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PaymentMethodInfo obj = new PaymentMethodInfo();
                    obj.PtId = DaHelper.GetString(dr, "PtId");

                    Bank bk = new Bank();
                    bk.BankKey = DaHelper.GetString(dr, "BankId");
                    bk.BankName = DaHelper.GetString(dr, "BankName");
                    obj.Bank = bk;
                    obj.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    obj.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    obj.ChqDt = DaHelper.GetDate(dr, "ChqDt");
                    obj.Amount = DaHelper.GetDecimal(dr, "Amount");
                    obj.AdjAmount = DaHelper.GetDecimal(dr, "AdjAmount");
                    obj.TotalAmt = obj.Amount + obj.AdjAmount;
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ChequeInfo> GetChequeOfSAPRefNo(DbTransaction trans, string workId, string sapRefNo)
        {
            try
            {
                List<ChequeInfo> chqList = new List<ChequeInfo>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_sel_chqOfRefNo");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "SAPRefNo", DbType.String, sapRefNo);
                db.AddInParameter(cmd, "WorkId", DbType.String, workId);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ChequeInfo chq = new ChequeInfo();
                    chq.BankKey = DaHelper.GetString(dr, "BankKey");
                    chq.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    chqList.Add(chq);
                }
                return chqList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public decimal? GetCashOfSAPRefNo(DbTransaction trans, string workId, string sapRefNo)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_sel_cashOfRefNo");
                db.AddInParameter(cmd, "SAPRefNo", DbType.String, sapRefNo);
                db.AddInParameter(cmd, "WorkId", DbType.String, workId);
                return (decimal?)db.ExecuteScalar(cmd);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void CancelMoneyCheckedIn(DbTransaction trans, CancelMoneyCheckedInInfo param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_CancelMoneyCheckedIn");
                db.AddInParameter(cmd, "WorkId", DbType.String, param.WorkId);
                db.AddInParameter(cmd, "SAPRefNo", DbType.String, param.SapRefNo);
                db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, param.PaymentDt);
                db.AddInParameter(cmd, "PosId", DbType.String, param.PosId);
                db.AddInParameter(cmd, "CashierId", DbType.String, param.CashierId);
                db.AddInParameter(cmd, "CashierName", DbType.String, param.CashierName);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, param.ModifiedBy);
                db.AddInParameter(cmd, "PostedBranchId", DbType.String, param.PostedBranchId);
                db.ExecuteScalar(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region P'X

        public List<GLBankInfo> ListGLBank(DbTransaction trans, string businessPlace)
        {
            List<GLBankInfo> list = new List<GLBankInfo>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_GLBank");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "BusinessPlace", DbType.String, businessPlace);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                GLBankInfo item = new GLBankInfo();
                item.BankKey = DaHelper.GetString(dr, "BankKey");
                item.BankHouse = DaHelper.GetString(dr, "BankHouse");
                item.BankName = DaHelper.GetString(dr, "BankName");
                list.Add(item);
            }
            return list;
        }


        public List<string> ListPOSIdOfWork(DbTransaction trans, string branchId)
        {
            List<string> posList = new List<string>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_allPOSIdOfWork");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                string posId = DaHelper.GetString(dr, "PosId");
                posList.Add(posId);
            }
            return posList;
        }


        public List<CashierInfo> ListCashierOfWork(DbTransaction trans, string branchId)
        {
            List<CashierInfo> cashieList = new List<CashierInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_allCashierOfWork");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CashierInfo cashier = new CashierInfo();
                cashier.CashierId = DaHelper.GetString(dr, "CashierId");
                cashier.CashierName = DaHelper.GetString(dr, "CashierName");
                cashieList.Add(cashier);
            }
            return cashieList;
        }

        public List<GLBankAccountInfo> ListGLBankAccount(DbTransaction trans, string businessPlace)
        {
            List<GLBankAccountInfo> list = new List<GLBankAccountInfo>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_GLBankAccount");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "BusinessPlace", DbType.String, businessPlace);

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                GLBankAccountInfo item = new GLBankAccountInfo();
                item.BankKey = DaHelper.GetString(dr, "BankKey");
                item.BankName = DaHelper.GetString(dr, "BankName");
                item.BankHouse = DaHelper.GetString(dr, "BankHouse");
                item.BankAccount = DaHelper.GetString(dr, "AccountNo");
                item.AccountType = DaHelper.GetString(dr, "AccountType");
                item.ClearingAccNo = DaHelper.GetString(dr, "ClearingAccNo");
                list.Add(item);
            }
            return list;
        }

        public List<CashierInfo> ListCashier(DbTransaction trans, string keyword, string branchId)
        {
            List<CashierInfo> list = new List<CashierInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_Cashier");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "Keyword", DbType.String, keyword);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CashierInfo item = new CashierInfo();
                item.CashierId = DaHelper.GetString(dr, "UserId");
                item.CashierName = DaHelper.GetString(dr, "FullName");
                item.Status = DaHelper.GetString(dr, "UserStatus");
                item.PosId = DaHelper.GetString(dr, "PosId");
                item.WorkId = DaHelper.GetString(dr, "WorkId");
                item.TotalAmt = DaHelper.GetDecimal(dr, "TotalAmt");
                list.Add(item);
            }
            return list;
        }


        //all work except workId
        public string IsAllWorkClosed(DbTransaction trans, string workId, string branchId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_get_IsAllWorkClosed");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "WorkId", DbType.String, workId);
                return (string)db.ExecuteScalar(cmd);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SetBaseline(DbTransaction trans, string workId, string branchId, string postedBranchId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_setBaseline");
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "WorkId", DbType.String, workId);
                db.AddInParameter(cmd, "PostedBranchId", DbType.String, postedBranchId);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BaselineInfo> GetBaseline(DbTransaction trans, string branchId, DateTime baselineDt)
        {
            try
            {
                List<BaselineInfo> baseLine = new List<BaselineInfo>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_sel_baseline");
                cmd.CommandTimeout = CMD_TIMEOUT;
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "BaselineDt", DbType.DateTime, baselineDt);
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    BaselineInfo item = new BaselineInfo();
                    item.WorkId = DaHelper.GetString(dr, "WorkId");
                    item.BaselineId = DaHelper.GetInt(dr, "BaselineId");
                    item.BaselineDt = DaHelper.GetDateTime(dr, "CloseWorkDt");
                    baseLine.Add(item);
                }
                return baseLine;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void CancelBankDelivery(DbTransaction trans, BankDeliveryInfo dlInfo)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_cancelBankDelivery");
                db.AddInParameter(cmd, "APId", DbType.String, dlInfo.APId);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<BankDeliveryInfo> ListBankDelivery(DbTransaction trans, string workId)
        {
            int count = 1;
            List<BankDeliveryInfo> list = new List<BankDeliveryInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_bankDelivery");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                BankDeliveryInfo item = new BankDeliveryInfo();
                item.Order = count;
                item.APId = DaHelper.GetString(dr, "APId");
                item.BankDesc = DaHelper.GetString(dr, "BankDesc");
                item.BankAccNo = DaHelper.GetString(dr, "BankAccNo");
                item.CashAmt = DaHelper.GetDecimal(dr, "CashAmt");
                item.ChequeAmt = DaHelper.GetDecimal(dr, "ChequeAmt");
                item.TotalAmt = item.CashAmt + item.ChequeAmt;
                item.PaymentDt = DaHelper.GetDate(dr, "PaymentDt");
                list.Add(item);
                count++;
            }
            return list;
        }

        //realtime report
        public ReportBankPayInDetailInfo GetBankPayInDetailForReport(DbTransaction trans, CashierMoneyFlowInfo flowInfo)
        {
            ReportBankPayInDetailInfo reportData = new ReportBankPayInDetailInfo();
            List<ChequeInfo> chqList = new List<ChequeInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_ReportPayInDetail");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "FlowId", DbType.String, flowInfo.FlowId); //APId

            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow drow = dt.Rows[0];
                reportData.CashAmt = DaHelper.GetDecimal(drow, "TotalCashAmt");
                reportData.GLBankName = DaHelper.GetString(drow, "GLBankName");
                reportData.GLBankKey = DaHelper.GetString(drow, "GLBankKey");
                reportData.GLBankAcc = DaHelper.GetString(drow, "GLAccountNo");
                reportData.PaymentDt = DaHelper.GetDateTime(drow, "PaymentDt");
                reportData.ClearingAccNo = DaHelper.GetString(drow, "ClearingAccNo") == null ?
                                            null : DaHelper.GetString(drow, "ClearingAccNo").TrimStart('0');

                foreach (DataRow dr in dt.Rows)
                {
                    ChequeInfo item = new ChequeInfo();
                    item.BankKey = DaHelper.GetString(dr, "BankKey");
                    if (item.BankKey == null) continue;

                    item.BankName = DaHelper.GetString(dr, "BankName");
                    item.ChqNo = DaHelper.GetString(dr, "ChqNo");
                    item.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                    item.ChqDt = DaHelper.GetDateTime(dr, "ChqDt");
                    item.Amount = DaHelper.GetDecimal(dr, "Amount");
                    item.ReportType = DaHelper.GetInt(dr, "ReportType");
                    chqList.Add(item);
                }

            }

            reportData.ChqList = chqList;
            return reportData;
        }

        private decimal? Positive(decimal? money)
        {
            decimal? ret = 0;
            if (money < 0)
                ret = money * (-1);
            else
                ret = money;

            return ret;
        }  

        private void FilterFlowType(ref FlowSummaryInfo flow, DataRow dr)
        {
            // +++++++++++++++++++++++++++++++
            if (flow.FlowType == FlowType.SystemInitialCash ||
                flow.FlowType == FlowType.MoneyFromBank ||
                flow.FlowType == FlowType.MoneyFromAnotherCashier ||
                flow.FlowType == FlowType.MoneyReceivedFromPOS ||
                flow.FlowType == FlowType.MoneyOpeningBalance ||
                flow.FlowType == FlowType.CancelledPOSPaymentable ||
                flow.FlowType == FlowType.CancelledBankDelivery ||
                flow.FlowType == FlowType.Adjust_MoneyFromBank_Plus ||
                flow.FlowType == FlowType.Adjust_MoneyReceivedFromPOS_Plus ||
                flow.FlowType == FlowType.Adjust_MoneyDepositToBank_Minus ||
                flow.FlowType == FlowType.Adjust_CashOutFromPOS_Minus)
            {
                flow.CashIn = Positive(DaHelper.GetDecimal(dr, "CashAmt"));
                flow.ChequeIn = Positive(DaHelper.GetDecimal(dr, "ChequeAmt"));
            }
            // -------------------------------
            else if (flow.FlowType == FlowType.MoneyTransferedToAnotherCashier ||
                    flow.FlowType == FlowType.MoneyDepositToBank ||
                    flow.FlowType == FlowType.CashOutFromPOS ||
                    flow.FlowType == FlowType.MoneyClosingBalance ||
                    flow.FlowType == FlowType.CancelledPOSReceivable ||
                    flow.FlowType == FlowType.CancelledMoneyCheckIn ||
                    flow.FlowType == FlowType.Adjust_MoneyFromBank_Minus ||
                    flow.FlowType == FlowType.Adjust_MoneyReceivedFromPOS_Minus ||
                    flow.FlowType == FlowType.Adjust_MoneyDepositToBank_Plus ||
                    flow.FlowType == FlowType.Adjust_CashOutFromPOS_Plus)
            {
                flow.CashOut = Positive(DaHelper.GetDecimal(dr, "CashAmt"));
                flow.ChequeOut = Positive(DaHelper.GetDecimal(dr, "ChequeAmt"));
            }
            else
            {
                throw new Exception("Flow ในระบบไม่สอดคล้องกับที่มีอยู่");
            }
        }   

        public List<FlowSummaryInfo> GetWorkFlow(DbTransaction trans, string workId)
        {
            List<FlowSummaryInfo> flowList = new List<FlowSummaryInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_closeWorkSummary");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                FlowSummaryInfo flow = new FlowSummaryInfo();
                flow.FlowType = DaHelper.GetString(dr, "FlowType");
                if (flow.FlowType != null)
                {
                    flow.FlowCat = DaHelper.GetString(dr, "FlowCat");
                    flow.FlowId = DaHelper.GetString(dr, "FlowId");
                    flow.Description = DaHelper.GetString(dr, "FlowDesc");
                    flow.BankKey = DaHelper.GetString(dr, "BankKey");
                    flow.BankName = DaHelper.GetString(dr, "BankName");
                    flow.AccountNo = DaHelper.GetString(dr, "RefNo");
                    flow.ModifiedDt = DaHelper.GetDateTime(dr, "FlowDt");
                    FilterFlowType(ref flow, dr);
                    flow.CashIn = flow.CashIn == null ? 0 : flow.CashIn;
                    flow.CashOut = flow.CashOut == null ? 0 : flow.CashOut;
                    flow.ChequeIn = flow.ChequeIn == null ? 0 : flow.ChequeIn;
                    flow.ChequeOut = flow.ChequeOut == null ? 0 : flow.ChequeOut;
                    flow.WorkId = workId;
                    flowList.Add(flow);
                }
            }

            return flowList;
        }

        public TrayMoneyInfo GetMoneyInTray(DbTransaction trans, string workId)
        {
            TrayMoneyInfo trayMoneyInfo = new TrayMoneyInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_cashierWorkCash");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd, trans).Tables[0];
            DataRow cashRow = dt.Rows[0];
            trayMoneyInfo.CashAmount = DaHelper.GetDecimal(cashRow, "TotalAmount");
            trayMoneyInfo.CashPendingAmount = DaHelper.GetDecimal(cashRow, "PendingAmount");


            BindingList<ChequeInfo> chequeList = new BindingList<ChequeInfo>();
            DbCommand cmd1 = db.GetStoredProcCommand("cm_sel_cashierWorkCheque");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd1, "WorkId", DbType.String, workId);
            DataTable dt1 = db.ExecuteDataSet(cmd1, trans).Tables[0];
            foreach (DataRow dr in dt1.Rows)
            {
                ChequeInfo cq = new ChequeInfo();
                cq.BankKey = DaHelper.GetString(dr, "BankKey");
                cq.BankName = DaHelper.GetString(dr, "BankName");
                cq.ChqNo = DaHelper.GetString(dr, "ChqNo");
                cq.ChqAccNo = DaHelper.GetString(dr, "ChqAccNo");
                cq.ChqDt = DaHelper.GetDate(dr, "ChqDt");
                cq.Amount = DaHelper.GetDecimal(dr, "Amount");
                cq.TransStatus = DaHelper.GetString(dr, "TransStatus");
                chequeList.Add(cq);
            }

            trayMoneyInfo.ChequeList = chequeList;

            //BindingList<PayInInfo> payInList = new BindingList<PayInInfo>();
            //DbCommand cmd2 = db.GetStoredProcCommand("cm_sel_cashierWorkPayIn");
            //cmd.CommandTimeout = CMD_TIMEOUT;
            //db.AddInParameter(cmd2, "WorkId", DbType.String, workId);
            //DataTable dt2 = db.ExecuteDataSet(cmd2, trans).Tables[0];
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    PayInInfo cq = new PayInInfo();
            //    cq.BankKey = DaHelper.GetString(dr, "BankKey");
            //    cq.BankName = DaHelper.GetString(dr, "BankName");
            //    cq.ChqNo = DaHelper.GetString(dr, "PayInNo");
            //    cq.ChqAccNo = DaHelper.GetString(dr, "PayInAccNo");
            //    cq.ChqDt = DaHelper.GetDate(dr, "PayInDt");
            //    cq.Amount = DaHelper.GetDecimal(dr, "Amount");
            //    cq.TransStatus = DaHelper.GetString(dr, "TransStatus");
            //    payInList.Add(cq);
            //}

            //trayMoneyInfo.PayInList = payInList;

            return trayMoneyInfo;
        }

        public string InsertBankDeliveryAP(DbTransaction trans, string workId, string glBankKey, string bankName, string clearingAccno, string glBankAccNo, decimal cashAmt,
                                                  decimal chqAmt, string branchId, string modifiedBy, string postedBranchId, string sepChq, string posId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_ins_bankDeliverAP");
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            db.AddInParameter(cmd, "GLBankKey", DbType.String, glBankKey);
            db.AddInParameter(cmd, "BankName", DbType.String, bankName);
            db.AddInParameter(cmd, "ClearingAccno", DbType.String, clearingAccno);
            db.AddInParameter(cmd, "GLBankAccNo", DbType.String, glBankAccNo);
            db.AddInParameter(cmd, "CashAmt", DbType.String, cashAmt);
            db.AddInParameter(cmd, "ChqAmt", DbType.String, chqAmt);
            db.AddInParameter(cmd, "SepCheque", DbType.String, sepChq);
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            db.AddInParameter(cmd, "PostedBranchId", DbType.String, postedBranchId);
            db.AddInParameter(cmd, "PosId", DbType.String, posId);
            return (string)db.ExecuteScalar(cmd, trans);
        }


        public void InsertAPChequeItem(DbTransaction trans, string APId, string bankKey, string bankName, string chqNo,
                                        string chqAccNo, DateTime chqDt, decimal amount, string modifiedBy, string postedBranchId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_ins_APChequeItem");
            db.AddInParameter(cmd, "APId", DbType.String, APId);
            db.AddInParameter(cmd, "BankKey", DbType.String, bankKey);
            db.AddInParameter(cmd, "BankName", DbType.String, bankName);
            db.AddInParameter(cmd, "ChqNo", DbType.String, chqNo);
            db.AddInParameter(cmd, "ChqAccNo", DbType.String, chqAccNo);
            db.AddInParameter(cmd, "ChqDt", DbType.DateTime, chqDt);
            db.AddInParameter(cmd, "ChqAmt", DbType.Decimal, amount);
            db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
            db.AddInParameter(cmd, "PostedBranchId", DbType.String, postedBranchId);
            db.ExecuteNonQuery(cmd, trans);
        }


        public string InsertTransferMoneyFlow(DbTransaction trans, string workId, string sender, string senderName, string receiver, string receiverName, string commander,
                                               string reqPosId, decimal cashAmt, decimal chqAmt, string branchId, string modifiedBy, string postedBranchId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_transferFlow");
                db.AddInParameter(cmd, "WorkId", DbType.String, workId);
                db.AddInParameter(cmd, "SenderId", DbType.String, sender);
                db.AddInParameter(cmd, "SenderName", DbType.String, senderName);
                db.AddInParameter(cmd, "ReceiverId", DbType.String, receiver);
                db.AddInParameter(cmd, "ReceiverName", DbType.String, receiverName);
                db.AddInParameter(cmd, "CommanderId", DbType.String, commander);
                db.AddInParameter(cmd, "ReqPosId", DbType.String, reqPosId);
                db.AddInParameter(cmd, "CashAmt", DbType.String, cashAmt);
                db.AddInParameter(cmd, "ChequeAmt", DbType.String, chqAmt);
                db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
                db.AddInParameter(cmd, "PostedBranchId", DbType.String, postedBranchId);
                return (string)db.ExecuteScalar(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string InsertWorkFlow(DbTransaction trans, FlowSummaryInfo flow)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_workFlow");
                db.AddInParameter(cmd, "FlowType", DbType.String, flow.FlowType);
                db.AddInParameter(cmd, "FlowDesc", DbType.String, flow.FlowDesc);
                db.AddInParameter(cmd, "FlowCat", DbType.String, flow.FlowCat);
                db.AddInParameter(cmd, "GLBankKey", DbType.String, flow.BankKey);
                db.AddInParameter(cmd, "BankName", DbType.String, flow.BankName);
                db.AddInParameter(cmd, "GLAccountNo", DbType.String, flow.AccountNo);
                db.AddInParameter(cmd, "CashierId", DbType.String, flow.CashierId);
                db.AddInParameter(cmd, "TransferId", DbType.String, flow.TransferId);
                db.AddInParameter(cmd, "WorkId", DbType.String, flow.WorkId);
                db.AddInParameter(cmd, "CashAmt", DbType.Decimal, flow.CashIn - flow.CashOut);
                db.AddInParameter(cmd, "ChequeAmt", DbType.Decimal, flow.ChequeIn - flow.ChequeOut);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, flow.ModifiedBy);
                db.AddInParameter(cmd, "PostedBranchId", DbType.String, flow.PostedBranchId);
                db.AddInParameter(cmd, "FlowDt", DbType.DateTime, flow.ModifiedDt);
                db.AddInParameter(cmd, "BranchId", DbType.String, flow.BranchId);
                db.AddInParameter(cmd, "PosId", DbType.String, flow.PosId);
                return (string)db.ExecuteScalar(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateWorkFlow(DbTransaction trans, FlowSummaryInfo flow)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_upd_workFlow");
                db.AddInParameter(cmd, "FlowId", DbType.String, flow.FlowId);
                db.AddInParameter(cmd, "FlowType", DbType.String, flow.FlowType);
                db.AddInParameter(cmd, "CashAmt", DbType.Decimal, flow.CashIn - flow.CashOut);
                db.AddInParameter(cmd, "ChequeAmt", DbType.Decimal, flow.ChequeIn - flow.ChequeOut);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, flow.ModifiedBy);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public string UpdateSystemInitial(DbTransaction trans, FlowSummaryInfo flow)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_upd_systemInitial");
                db.AddInParameter(cmd, "FlowType", DbType.String, flow.FlowType);
                db.AddInParameter(cmd, "FlowCat", DbType.String, flow.FlowCat);
                db.AddInParameter(cmd, "GLBankKey", DbType.String, flow.BankKey);
                db.AddInParameter(cmd, "GLAccountNo", DbType.String, flow.AccountNo);
                db.AddInParameter(cmd, "CashierId", DbType.String, flow.CashierId);
                db.AddInParameter(cmd, "TransferId", DbType.String, flow.TransferId);
                db.AddInParameter(cmd, "WorkId", DbType.String, flow.WorkId);
                db.AddInParameter(cmd, "CashAmt", DbType.Decimal, flow.CashIn - flow.CashOut);
                db.AddInParameter(cmd, "ChequeAmt", DbType.Decimal, flow.ChequeIn - flow.ChequeOut);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, flow.CashierId);
                db.AddInParameter(cmd, "PostedBranchId", DbType.String, flow.PostedBranchId);
                db.AddInParameter(cmd, "FlowDt", DbType.DateTime, flow.ModifiedDt);
                db.AddInParameter(cmd, "BranchId", DbType.String, flow.BranchId);
                db.AddInParameter(cmd, "PosId", DbType.String, flow.PosId);
                return (string)db.ExecuteScalar(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void InsertFlowItem(DbTransaction trans, string flowId, string bankKey, string chqNo,
                                        string chqAccNo, DateTime chqDt, decimal amount, string modifiedBy, string validFlag)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_flowItem");
                db.AddInParameter(cmd, "FlowId", DbType.String, flowId);
                db.AddInParameter(cmd, "BankKey", DbType.String, bankKey);
                db.AddInParameter(cmd, "ChqNo", DbType.String, chqNo);
                db.AddInParameter(cmd, "ChqAccNo", DbType.String, chqAccNo);
                db.AddInParameter(cmd, "ValidFlag", DbType.String, validFlag);
                db.AddInParameter(cmd, "ChqDt", DbType.DateTime, chqDt);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, amount);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateFlowItem(DbTransaction trans, string flowId, string chqItemId, string bankKey, string chqNo,
                                        string chqAccNo, DateTime chqDt, decimal amount, string modifiedBy, string validFlag)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_upd_flowItem");
                db.AddInParameter(cmd, "FlowId", DbType.String, flowId);
                db.AddInParameter(cmd, "ChqItemId", DbType.String, chqItemId);
                db.AddInParameter(cmd, "BankKey", DbType.String, bankKey);
                db.AddInParameter(cmd, "ChqNo", DbType.String, chqNo);
                db.AddInParameter(cmd, "ChqAccNo", DbType.String, chqAccNo);
                db.AddInParameter(cmd, "ValidFlag", DbType.String, validFlag);
                db.AddInParameter(cmd, "ChqDt", DbType.DateTime, chqDt);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, amount);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateAdjFlowItem(DbTransaction trans, string flowId, string bankKey, string chqNo,
                                string chqAccNo, DateTime chqDt, decimal amount, string modifiedBy, string validFlag)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_upd_adjFlowItem");
                db.AddInParameter(cmd, "FlowId", DbType.String, flowId);
                db.AddInParameter(cmd, "BankKey", DbType.String, bankKey);
                db.AddInParameter(cmd, "ChqNo", DbType.String, chqNo);
                db.AddInParameter(cmd, "ChqAccNo", DbType.String, chqAccNo);
                db.AddInParameter(cmd, "ValidFlag", DbType.String, validFlag);
                db.AddInParameter(cmd, "ChqDt", DbType.DateTime, chqDt);
                db.AddInParameter(cmd, "Amount", DbType.Decimal, amount);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public void SetActiveFlowItem(DbTransaction trans, string flowId, string chqItemId, string activeFlag, string modifiedBy)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_upd_SetActiveFlowItem");
                db.AddInParameter(cmd, "FlowId", DbType.String, flowId);
                db.AddInParameter(cmd, "ChqItemId", DbType.String, chqItemId);
                db.AddInParameter(cmd, "ActiveFlag", DbType.String, activeFlag);
                db.AddInParameter(cmd, "ModifiedBy", DbType.String, modifiedBy);
                db.ExecuteNonQuery(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public string CloseWork(DbTransaction trans, CloseWorkSubmitInfo submitInfo)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("cm_ins_closeWork");
                db.AddInParameter(cmd, "WorkId", DbType.String, submitInfo.WorkId);
                db.AddInParameter(cmd, "NextCashAmt", DbType.Decimal, submitInfo.CashNextWork);
                db.AddInParameter(cmd, "NextChqAmt", DbType.Decimal, submitInfo.ChqNextWork);
                db.AddInParameter(cmd, "CloseWorkBy", DbType.String, submitInfo.CloseWorkBy);
                db.AddInParameter(cmd, "PostedBranchId", DbType.String, submitInfo.PostedBranchId);
                db.AddInParameter(cmd, "BranchId", DbType.String, submitInfo.BranchId);
                db.AddInParameter(cmd, "PosId", DbType.String, submitInfo.PosId);
                return (string)db.ExecuteScalar(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        //public List<ReportDailyRemainInfo> GetDailyRemainReport(ReportParam param)
        //{
        //    List<ReportDailyRemainInfo> reportDataList = new List<ReportDailyRemainInfo>();
        //    Database db = DatabaseFactory.CreateDatabase("POSDatabase");
        //    DbCommand cmd = db.GetStoredProcCommand("cm_sel_ReportDailyRemain");
        //    db.AddInParameter(cmd, "FromDate", DbType.DateTime, param.FromDate);
        //    db.AddInParameter(cmd, "ToDate", DbType.DateTime, param.ToDate);
        //    db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);
        //    DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        ReportDailyRemainInfo dayInfo = new ReportDailyRemainInfo();
        //        dayInfo.OverallAmt = DaHelper.GetDecimal(dr, "TotalAmt");
        //        dayInfo.CloseWorkDate = DaHelper.GetDateTime(dr, "CloseWorkDt").ToString("ddMMyyyy", new CultureInfo("th-TH"));
        //        reportDataList.Add(dayInfo);
        //    }
        //    return reportDataList;

        //}

        public bool ExistSAPRefNo(DbTransaction trans, string sapRefNo, string workId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_get_ExistSAPRefNo");
            db.AddInParameter(cmd, "SapRefNo", DbType.String, sapRefNo);
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            int ret = (int)db.ExecuteScalar(cmd);
            if (ret == 1) return true;
            else return false;
        }        

        public List<string> GetSAPReference(DbTransaction trans, string workId)
        {
            List<string> sapRefList = new List<string>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_SAPReference");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                string sapRef = DaHelper.GetString(dr, "SAPRef");
                sapRefList.Add(sapRef);
            }

            return sapRefList;
        }

        public List<CashierInfo> GetOpenWorkCashierOfBranch(DbTransaction trans, string branchId)
        {
            List<CashierInfo> cashierList = new List<CashierInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_OpenWorkCashierOfBranch");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                CashierInfo ci = new CashierInfo();
                ci.BranchName2 = DaHelper.GetString(dr, "BranchName2");
                ci.CashierId = DaHelper.GetString(dr, "CashierId");
                ci.LongName = DaHelper.GetString(dr, "LongName");
                ci.WorkId = DaHelper.GetString(dr, "WorkId");
                cashierList.Add(ci);
            }

            return cashierList;
        }

        public decimal? GetPayInOfWork(DbTransaction trans, string workId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_get_WorkPOSPayIn");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            return (decimal?)db.ExecuteScalar(cmd);
        }

        public List<WorkInfo> LoadAllOpenWork(DbTransaction trans, string branchId)
        {
            List<WorkInfo> workList = new List<WorkInfo>();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_allOpenWork");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "BranchId", DbType.String, branchId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            //force one row only
            foreach (DataRow dr in dt.Rows)
            {
                WorkInfo workInfo = new WorkInfo();
                workInfo.WorkId = DaHelper.GetString(dr, "WorkId");
                workInfo.CashierId = DaHelper.GetString(dr, "CashierId");
                workInfo.CashierName = DaHelper.GetString(dr, "CashierName");
                workInfo.PosId = DaHelper.GetString(dr, "PosId");
                workInfo.OpenWorkDt = DaHelper.GetDate(dr, "OpenWorkDt");
                workInfo.Status = DaHelper.GetString(dr, "Status");
                workList.Add(workInfo);
            }

            return workList;
        }

        public WorkInfo GetWorkInfo(DbTransaction trans, string workId)
        {
            WorkInfo workInfo = new WorkInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_sel_workInfo");
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            //force one row only
            foreach (DataRow dr in dt.Rows)
            {
                workInfo.CashierId = DaHelper.GetString(dr, "CashierId");
                workInfo.CashierName = DaHelper.GetString(dr, "CashierName");
                workInfo.PosId = DaHelper.GetString(dr, "PosId");
                workInfo.OpenWorkDt = DaHelper.GetDate(dr, "OpenWorkDt");
                workInfo.CloseWorkDt = DaHelper.GetDate(dr, "CloseWorkDt");
            }

            return workInfo;
        }

        public decimal[] GetClosingBalance(DbTransaction trans, string workId)
        {
            decimal[] ret = new decimal[3]; //cash,cheque, total
            OpenWorkInfo obj = new OpenWorkInfo();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_get_CloseWorkMoney");
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count == 0)
            {
                ret[0] = 0;
                ret[1] = 0;
                ret[2] = 0;
            }
            else
            {
                ret[0] = Decimal.Round(DaHelper.GetDecimal(dt.Rows[0], "CashAmt").Value, 2);
                ret[1] = Decimal.Round(DaHelper.GetDecimal(dt.Rows[0], "ChqAmt").Value, 2);
                ret[2] = Decimal.Round(DaHelper.GetDecimal(dt.Rows[0], "TotalAmt").Value, 2);
            }
            return ret;
        }

        #endregion

        #region add by P.Wongket

        public CashierWorkStatus GetCashierWorkStatus(string workId)
        {
            CashierWorkStatus ret = new CashierWorkStatus();
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("cm_get_CashierWorkStatus");
            db.AddInParameter(cmd, "WorkId", DbType.String, workId);
            DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
            //force one row only
            foreach (DataRow dr in dt.Rows)
            {
                ret.CloseWorkBy = DaHelper.GetString(dr, "CloseWorkBy");
                ret.FullName = DaHelper.GetString(dr, "FullName");
            }
            return ret;
        }

        #endregion
    }


}
