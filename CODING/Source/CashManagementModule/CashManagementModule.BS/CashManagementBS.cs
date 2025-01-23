using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.CashManagementModule.DA;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;
using System.Configuration;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.CashManagementModule.BS
{
    public class CashManagementBS : ICashManagementServices
    {

        #region TongKung

        public List<CashierWorkStatusInfo> OriginalIsOpenedWork(OpenWorkParam param)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<CashierWorkStatusInfo> list = da.IsOpenedWork(trans, param);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }
        //Created By TongKung
        public List<CashierWorkStatusInfo> IsOpenedWork(OpenWorkParam param)
        {
            string authenurl = ConfigurationManager.AppSettings["CASHIER_GATEWAY"];
            if (string.IsNullOrEmpty(authenurl))
            {
                //return OriginalIsOpenedWork(param);

                List<CashierWorkStatusInfo> temp = OriginalIsOpenedWork(param);

                return temp;
            }
            try
            {
                CashierCachingWS.CashierCachingWebService ccws = new PEA.BPM.CashManagementModule.BS.CashierCachingWS.CashierCachingWebService();
                ccws.Url = authenurl + "CashierCachingWebService.asmx";
                List<CashierWorkStatusInfo> res = new List<CashierWorkStatusInfo>(ccws.IsOpenedWork(param));
                return res;
            }
            catch (System.Net.WebException)
            {
                return OriginalIsOpenedWork(param);
            }
        }

        public string GetWorkPosId(string workId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    string ret = da.GetWorkPosId(trans, workId);

                    trans.Commit();
                    return ret;
            
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
	}
        }

        //Created By TongKung
        public bool IsSystemInitial(string branchId, string workId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    bool ret = da.IsSystemInitial(trans, branchId,workId);

                    trans.Commit();
                    return ret;

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        //Created By TongKung
        public List<CashierMoneyTransferInfo> LoadTransferedRequestItem(string cashierId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<CashierMoneyTransferInfo> list = da.LoadTransferedRequestItem(trans, cashierId);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        //Created By TongKung
        public List<CashierMoneyTransferInfo> LoadTransferStatusItem(string cashierId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<CashierMoneyTransferInfo> list = da.LoadTransferStatusItem(trans, cashierId);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        //Created By TongKung
        public void CancelTransferItem(List<String> list, string modifiedBy)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    CashManagementDA da = new CashManagementDA();
                    string status = "3"; // cancel
                    foreach (String obj in list)
                        da.UpdateCashierMoneyTransfer(trans, obj, status, null, modifiedBy);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        //Created By TongKung
        public string ResponseTransferedItems(DbTransaction trans, List<String> transferList, string workId, string status, string posId, string branchId, string modifiedBy)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string ret = null;

            if (trans == null)
            {
                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    try
                    {
                        CashManagementDA da = new CashManagementDA();
                        foreach (String transferId in transferList)
                            //ret = da.UpdateIncommingTransfer(trans, workId, transferId, status, posId, Session.Branch.PostedServerId, modifiedBy);
                            ret = da.UpdateIncommingTransfer(trans, workId, transferId, status, posId, branchId, Session.Branch.PostedServerId, modifiedBy);

                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw e;
                    }
                }
            }
            else
            {
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    foreach (String transferId in transferList)
                        //ret = da.UpdateIncommingTransfer(trans, workId, transferId, status, posId, Session.Branch.PostedServerId, modifiedBy);
                        ret = da.UpdateIncommingTransfer(trans, workId, transferId, status, posId, branchId, Session.Branch.PostedServerId, modifiedBy);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return ret;
        }


        public OpenWorkInfo LoadOpeningBalance(string cashierId, string flowType)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    OpenWorkInfo obj = da.LoadOpeningBalance(trans, cashierId, flowType);
                    obj.OpeningCheque = da.LoadOpeningCheque(trans, obj.FlowId);

                    trans.Commit();
                    return obj;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
	}
        }       

        public void CheckInMoney(MoneyCheckInInfo param)
        {

            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            param.PostedBranchId = Session.Branch.PostedServerId;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    CashManagementDA da = new CashManagementDA();
                    //insert into AR ( 1 SAPRefNo )
                    //da.InsertAR(trans, param.DtId, param.TotalAmount, param.SAPRefNo, param.BranchId, param.ModifiedBy, param.PostedBranchId);
                    da.InsertAR(trans, param.DtId, param.Amount, param.SAPRefNo, param.BranchId, param.ModifiedBy, param.PostedBranchId, param.PosId);

                    //insert into Payment as of detail of payment ( Who proceed this SAPRefNo )
                    string paymentId = da.InsertPayment(trans, param.PaymentDt, param.PosId, param.CashierId, param.CashierName, param.BranchId, param.WorkId, param.ModifiedBy, param.PostedBranchId);

                    //insert into ARPaymentType as of list of items (e.g. 1 cash, 10 cheques)
                    //loop through List<PaymentMethodInfo>
                    List<String> arPtIdList = new List<string>();
                    for (int i = 0; i < param.PaymentMethodList.Count; i++)
                        arPtIdList.Add(da.InsertARPaymentType(trans, paymentId, param.PaymentMethodList[i], param.BranchId, param.ModifiedBy, param.PostedBranchId, param.PosId));

                    //where the is payment happened and how much does it cost?
                    //string arPmId = da.InsertARPayment(trans, param.SAPRefNo, param.PmId, param.TotalAmount, param.PaymentDt, param.Pending, param.BranchId, param.ModifiedBy, param.PostedBranchId);
                    string arPmId = da.InsertARPayment(trans, param.SAPRefNo, param.PmId, param.Amount, param.AdjAmount, param.PaymentDt, param.Pending, param.BranchId, param.ModifiedBy, param.PostedBranchId, param.PosId);

                    //insert the relationship betweeen ARPt and ARPm
                    for (int i = 0; i < arPtIdList.Count; i++)
                        da.InsertRTARPaymentTypeARPayment(trans, arPmId, arPtIdList[i], param.PaymentMethodList[i].Amount, param.BranchId, param.ModifiedBy);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public List<PaymentMethodInfo> LoadMoneyCheckedIn(string SAPRefNo, string workId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<PaymentMethodInfo> list = da.LoadMoneyCheckedIn(trans, SAPRefNo, workId);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public int CancelMoneyCheckedIn(CancelMoneyCheckedInInfo param)
        {
            List<ChequeInfo> refNoChq;
            TrayMoneyInfo moneyIntray;
            param.PostedBranchId = Session.Branch.PostedServerId;

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            CashManagementDA da = new CashManagementDA();
            

            DbTransaction trans;
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    //check available to cancel
                    refNoChq = da.GetChequeOfSAPRefNo(trans, param.WorkId, param.SapRefNo);

                    decimal? refNoCash = da.GetCashOfSAPRefNo(trans, param.WorkId, param.SapRefNo);
                    moneyIntray = da.GetMoneyInTray(trans, param.WorkId);

                    //check chq of SAPRefNo are available in tray or not
                    decimal? availableCash = moneyIntray.CashAmount - moneyIntray.CashPendingAmount;
                    if (refNoCash > availableCash)
                        return 1; //เงินสดในเก๊ะไม่เพียงพอ หรืออยู่ในสถานะการโอนออก

                
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
	        }

            foreach (ChequeInfo chq in refNoChq)
            {
                bool ret = false;
                string refKey = chq.BankKey.Trim() + chq.ChqNo.Trim();

                foreach (ChequeInfo trayChq in moneyIntray.ChequeList)
                {
                    if (trayChq.TransStatus != "0") //Pending of Transfer
                    {
                        string trayKey = trayChq.BankKey.Trim() + chq.ChqNo.Trim();
                        if (trayKey == refKey)
                        {
                            ret = true; //found cheque
                            break;
                        }
                    }
                }

                if (ret == false)
                    return 2; // if not found cheque then return 2
            }

            //DbTransaction trans;
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    da.CancelMoneyCheckedIn(trans, param);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return 0;
        }

        #endregion

        #region P'X

        public List<CashierInfo> ListCashier(string keyword, string branchId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<CashierInfo> list = da.ListCashier(trans, keyword, branchId);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public List<BankDeliveryInfo> ListBankDelivery(string workId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<BankDeliveryInfo> list = da.ListBankDelivery(trans, workId);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public string IsAllWorkClosed(string workId, string branchId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    string ret = da.IsAllWorkClosed(trans, workId, branchId);

                    trans.Commit();
                    return ret;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public void SetBaseline(DbTransaction trans, string workId, string branchId)
        {
            try
            {
                CashManagementDA da = new CashManagementDA();
                if (trans == null)
                {
                    Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                    using (DbConnection conn = db.CreateConnection())
                    {
                        conn.Open();
                        trans = conn.BeginTransaction();

                        try
                        {
                            da.SetBaseline(trans, workId, branchId, Session.Branch.PostedServerId);
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
                else
                {
                    da.SetBaseline(trans, workId, branchId, Session.Branch.PostedServerId);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

         public void CancelBankDelivery(DbTransaction trans, BankDeliveryInfo blInfo)
        {
            try
            {
                CashManagementDA da = new CashManagementDA();
                if (trans == null)
                {
                    Database db = DatabaseFactory.CreateDatabase("POSDatabase");

                    using (DbConnection conn = db.CreateConnection())
                    {
                        conn.Open();
                        trans = conn.BeginTransaction();

                        try
                        {
                            da.CancelBankDelivery(trans, blInfo);
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
                else
                {
                    da.CancelBankDelivery(trans, blInfo);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<GLBankInfo> ListGLBank(string businessPlace)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<GLBankInfo> list = da.ListGLBank(trans, businessPlace);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public List<GLBankAccountInfo> ListGLBankAccount(string businessPlace, string bankId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<GLBankAccountInfo> list = da.ListGLBankAccount(trans, businessPlace); // select all bank
                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        private CashierMoneyFlowInfo ProcessTransfer(DbTransaction trans, MoneyTransferInfo tm, ref bool isopennewwork)
        {
            //don't commit here
            try
            {
                CashierMoneyFlowInfo ret = new CashierMoneyFlowInfo();
                CashManagementDA da = new CashManagementDA();
                tm.PostedBranchId = Session.Branch.PostedServerId;

                if (tm.ToBank) // deliver to bank 
                {
                    //1=separated cheque, 0 = not separated
                    string apId = da.InsertBankDeliveryAP(trans, tm.WorkId, tm.GLBankKey, tm.BankName, tm.ClearingAccno, tm.GLBankAcc, tm.CashAmount.Value,
                                                tm.ChequeAmount.Value, tm.BranchId, tm.ModifiedBy, tm.PostedBranchId,
                                                tm.SepChq ? "1" : "0", tm.ReqPosId);

                    if (apId != null)
                    {
                        ret.FlowId = apId;
                        if (tm.ChequeList.Count > 0)
                        {
                            foreach (ChequeInfo chq in tm.ChequeList)
                                da.InsertAPChequeItem(trans, apId, chq.BankKey, chq.BankName, chq.ChqNo, chq.ChqAccNo, chq.ChqDt.Value, chq.Amount.Value, tm.Commander, tm.PostedBranchId);
                        }
                    }
                    else
                    {
                        throw new Exception("Insert money flow error! please check DA or store procedure!");
                    }
                }
                else //transfer to another cashier
                {
                    //check if it is force transfer, so check openwork first
                    string[] transferInfo = new string[2];

                    if (tm.IsForceTrans)
                    {
                        //if not open, then open work of sender
                        OpenWorkParam openWorkParam = new OpenWorkParam();
                        openWorkParam.BranchId = tm.BranchId;
                        openWorkParam.CashierId = tm.Sender;

                        List<CashierWorkStatusInfo> openWorkList = da.IsOpenedWork(trans, openWorkParam);
                        //the sender is in close, so open new work
                        if (openWorkList.Count == 0)
                        {
                            //load opening balance 
                            OpenWorkInfo openWorkInfo = da.LoadOpeningBalance(trans, tm.Sender, FlowType.MoneyOpeningBalance);
                            OpenWorkParam param = new OpenWorkParam();
                            param.BranchId = tm.BranchId;
                            param.CashierId = tm.Sender;
                            param.CashierName = tm.SenderName;
                            param.PosId = tm.ReqPosId;
                            param.TerminalCode = tm.ReqTerminalCode;
                            param.ModifiedBy = tm.Commander;
                            param.FlowType = FlowType.MoneyOpeningBalance;
                            param.FlowId = openWorkInfo.FlowId;
                            //fill values
                            param.Status = "1";
                            param.PostedBranchId = Session.Branch.PostedServerId;
                            //received workId generated by Stored-Proc
                            string workId = da.OpenWork(trans, param);

                            //if flow type is opening balance, we must update workId on "CashierMoneyFlow"
                            if (param.FlowType == FlowType.MoneyOpeningBalance)
                                da.UpdateCashierMoneyFlowForWorkId(trans, param.FlowId, workId, param.PostedBranchId, param.ModifiedBy);
                            tm.WorkId = workId;
                            //tm.WorkId = OpenWork(trans, param);
                            isopennewwork = true; // บอกให้ข้างนอกรู้ว่ามีการเปิดกะใหม่แล้ว พวก cache จะได้ update
                            ret.WorkId = tm.WorkId;
                            ret.CashierId = tm.Sender;
                            ret.CloseWorkBy = tm.Commander;
                            ret.SpecialTrans = true;
                        }
                    }

                    //insert flow
                    string flowId = da.InsertTransferMoneyFlow(trans, tm.WorkId, tm.Sender, tm.SenderName, tm.Receiver, tm.ReceiverName, tm.Commander, tm.ReqPosId, tm.CashAmount.Value,
                                        tm.ChequeAmount.Value, tm.BranchId, tm.ModifiedBy, tm.PostedBranchId);

                    if (flowId != null)
                    {
                        transferInfo = flowId.Split('|');
                        ret.FlowId = transferInfo[0];
                        ret.TransferId = transferInfo[1];
                        if (tm.ChequeList.Count > 0)
                        {
                            //valid flag = 0 b/c destinatio has not accepted the transfer yet
                            foreach (ChequeInfo chq in tm.ChequeList)
                                da.InsertFlowItem(trans, ret.FlowId, chq.BankKey, chq.ChqNo, chq.ChqAccNo, chq.ChqDt.Value, chq.Amount.Value, tm.Commander, "0");
                        }
                    }
                    else
                    {
                        throw new Exception("Insert money flow error! please check DA or store procedure!");
                    }
                }

                return ret;
            }
            catch (Exception e)
            {
                throw e;
            }
        }      


        public CashierMoneyFlowInfo Transfer(DbTransaction trans, MoneyTransferInfo transferMoney)
        {
            //-- Input parameter trans มีค่าเป็น null เสมอ

            transferMoney.PostedBranchId = Session.Branch.PostedServerId;
            CashierMoneyFlowInfo ret = null;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    bool isopennewwork = false;
                    ret = ProcessTransfer(trans, transferMoney, ref isopennewwork);
                    trans.Commit();

                    if (isopennewwork)
                    {
                        //-- เรียก web service เพื่อ update cache หลัง commit
                        string authenurl = ConfigurationManager.AppSettings["CASHIER_GATEWAY"];
                        if (!string.IsNullOrEmpty(authenurl))
                        {
                            try
                            {
                                CashierCachingWS.CashierCachingWebService ccws = new PEA.BPM.CashManagementModule.BS.CashierCachingWS.CashierCachingWebService();
                                ccws.Url = authenurl + "CashierCachingWebService.asmx";
                                ccws.CacheOpenWork(ret.WorkId, ret.CashierId);
                            }
                            catch (System.Net.WebException)
                            {
                                //-- do nothing
                            }
                        }
                    }

                    return ret;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw e;
                }
            }
        }

        public void SaveStartOpenBalance(MoneyCheckInInfo param)
        {
            DbTransaction trans;
            decimal? cashAmt = 0;
            decimal? chqAmt = 0;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    CashManagementDA da = new CashManagementDA();

                    //clean the old flow before inserting
                    if (param.EditMode)
                    {

                    }

                    foreach (PaymentMethodInfo pmInfo in param.PaymentMethodList)
                    {
                        if (pmInfo.PtId == "1")
                            cashAmt += pmInfo.Amount;
                        else
                            chqAmt += pmInfo.Amount;
                    }

                    FlowSummaryInfo workFlow = new FlowSummaryInfo();
                    workFlow.WorkId = param.WorkId;
                    workFlow.PostedBranchId = Session.Branch.PostedServerId;
                    workFlow.ModifiedDt = param.PaymentDt.Value;
                    workFlow.FlowType = param.FlowType;
                    workFlow.FlowDesc = param.FlowDesc;
                    workFlow.FlowCat = param.FlowCat;
                    workFlow.CashIn = cashAmt;
                    workFlow.CashOut = 0;
                    workFlow.ChequeIn = chqAmt;
                    workFlow.ChequeOut = 0;
                    workFlow.CashierId = param.CashierId;
                    workFlow.BranchId = param.BranchId;
                    workFlow.PosId = param.PosId;

                    //string flowId = da.InsertWorkFlow(trans, workFlow);
                    string flowId = da.UpdateSystemInitial(trans, workFlow);

                    da.SetActiveFlowItem(trans, flowId, null, "0", param.ModifiedBy);
                    foreach (PaymentMethodInfo pmInfo in param.PaymentMethodList)
                    {
                        if (pmInfo.PtId == "2") //cheque
                        {
                            //da.InsertFlowItem(trans, flowId, pmInfo.BankId, pmInfo.ChqNo, pmInfo.ChqAccNo, pmInfo.ChqDt.Value, pmInfo.Amount.Value,
                            //                param.ModifiedBy, "1");
                            da.UpdateFlowItem(trans, flowId, pmInfo.ChqItemId, pmInfo.BankId, pmInfo.ChqNo, pmInfo.ChqAccNo, pmInfo.ChqDt.Value, pmInfo.Amount.Value,
                                            param.ModifiedBy, "1");
                        }
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

        public void SaveAdjustOpenBalance(MoneyCheckInInfo param)
        {
            DbTransaction trans = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                CashManagementDA da = new CashManagementDA();

                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    //cancel all flowType = 2%
                    da.CancelAdjustOpenBalance(trans, param.WorkId);

                    foreach (PaymentMethodInfo payment in param.PaymentMethodList)
                    {
                        FlowSummaryInfo workFlow = new FlowSummaryInfo();
                        workFlow.WorkId = param.WorkId;
                        workFlow.PostedBranchId = Session.Branch.PostedServerId;
                        workFlow.ModifiedDt = param.PaymentDt.Value;
                        workFlow.FlowId = payment.FlowId;
                        workFlow.FlowType = payment.FlowType;
                        workFlow.FlowDesc = payment.FlowDesc;
                        workFlow.FlowCat = "1"; //normal
                        workFlow.CashIn = (payment.PtId == "1") ? payment.Amount : 0;
                        workFlow.CashOut = 0;
                        workFlow.ChequeIn = (payment.PtId == "2") ? payment.Amount : 0;
                        workFlow.ChequeOut = 0;
                        workFlow.CashierId = param.CashierId;
                        workFlow.AccountNo = payment.Comment;  //borrowed field
                        workFlow.ModifiedBy = param.ModifiedBy;
                        workFlow.BranchId = param.BranchId;
                        workFlow.PosId = param.PosId;

                        //insert
                        if (payment.FlowId == null)
                        {
                            string flowId = da.InsertWorkFlow(trans, workFlow);

                            //insert chqeue
                            if (payment.PtId == "2")
                            {
                                da.InsertFlowItem(trans, flowId, payment.BankId, payment.ChqNo, payment.ChqAccNo, payment.ChqDt.Value, payment.Amount.Value,
                                                param.ModifiedBy, "1");
                            }
                        }
                        else //here we update
                        {
                            da.UpdateWorkFlow(trans, workFlow);

                            //update cheque
                            if (payment.PtId == "2")
                            {
                                da.UpdateAdjFlowItem(trans, payment.FlowId, payment.BankId, payment.ChqNo, payment.ChqAccNo, payment.ChqDt.Value, payment.Amount.Value,
                                                param.ModifiedBy, "1");
                            }
                        }
                    }

                    trans.Commit();
                }
            }
            catch (Exception)
            {
                trans.Rollback();
                throw;
            }
        }

        public List<PaymentMethodInfo> LoadSystemInitial(string workId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<PaymentMethodInfo> list = da.LoadSystemInitial(trans, workId);
                    trans.Commit();
                    return list;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public List<PaymentMethodInfo> LoadAdjustOpenBalance(string workId)
        {
            List<PaymentMethodInfo> pmList = null;

            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    pmList = da.LoadAdjustOpenBalance(trans, workId);
                    //foreach (PaymentMethodInfo pm in pmList)
                    //{
                    //    if (pm.FlowType == FlowType.Adjust_CashOutFromPOS_Minus ||
                    //        pm.FlowType == FlowType.Adjust_MoneyDepositToBank_Minus ||
                    //        pm.FlowType == FlowType.Adjust_MoneyFromBank_Minus ||
                    //        pm.FlowType == FlowType.Adjust_MoneyReceivedFromPOS_Minus)
                    //        pm.Amount = pm.Amount * (-1);
                    //}

                    trans.Commit();
                    return pmList;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

        }

        public void CloseWork(CloseWorkSubmitInfo submitInfo)
        {
            DbTransaction trans;
            CashManagementDA da = new CashManagementDA();
            submitInfo.PostedBranchId = Session.Branch.PostedServerId;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();

                try
                {
                    //Find money in tray to get NextCashAmt and NextChqAmt
                    TrayMoneyInfo trayMoney = da.GetMoneyInTray(trans, submitInfo.WorkId);
                    decimal? sumChq = SumCheque(trayMoney.ChequeList);

                    //Insert work flow summary items
                    List<FlowSummaryInfo> flowList = da.GetWorkFlow(trans, submitInfo.WorkId);
                    foreach (FlowSummaryInfo flow in flowList)
                    {
                        if (flow.FlowId == null)
                        {
                            flow.PostedBranchId = submitInfo.PostedBranchId;
                            flow.CashierId = submitInfo.CashierId;
                            flow.ModifiedBy = submitInfo.CloseWorkBy;
                            flow.BranchId = submitInfo.BranchId;
                            flow.PosId = submitInfo.PosId;
                            da.InsertWorkFlow(trans, flow);
                        }
                    }

                    //Close work and get FlowId of NextWork Flow
                    submitInfo.CashNextWork = trayMoney.CashAmount;
                    submitInfo.ChqNextWork = sumChq;
                    string flowId = da.CloseWork(trans, submitInfo);

                    if (flowId == null)
                        throw new Exception("Returned flowId from CloseWork() is null!");

                    //Insert cheque items of NextWork Flow
                    foreach (ChequeInfo chq in trayMoney.ChequeList)
                        da.InsertFlowItem(trans, flowId, chq.BankKey, chq.ChqNo, chq.ChqAccNo, chq.ChqDt.Value, chq.Amount.Value, submitInfo.CashierId, "1");

                    //ok go ahead submit it
                    trans.Commit();

                    //-- เรียก web service เพื่อ update cache หลัง commit
                    string authenurl = ConfigurationManager.AppSettings["CASHIER_GATEWAY"];
                    if (!string.IsNullOrEmpty(authenurl))
                    {
                        try
                        {                            
                            CashierCachingWS.CashierCachingWebService ccws = new PEA.BPM.CashManagementModule.BS.CashierCachingWS.CashierCachingWebService();
                            ccws.Url = authenurl + "CashierCachingWebService.asmx";
                            ccws.CacheCloseWork(submitInfo.WorkId, submitInfo.CashierId);
                        }
                        catch (System.Net.WebException )
                        {
                            //-- do nothing
                        }
                    }

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }       


        public ReportBankPayInDetailInfo GetBankPayInDetailForReport(CashierMoneyFlowInfo flowInfo)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    ReportBankPayInDetailInfo reportBankPayInDetailInfo = new ReportBankPayInDetailInfo();
                    reportBankPayInDetailInfo = da.GetBankPayInDetailForReport(trans, flowInfo);

                    trans.Commit();
                    return reportBankPayInDetailInfo;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }


        public List<ReportDailyRemainInfo> GetHistDailyRemainReport(ReportParam param)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    List<ReportDailyRemainInfo> rdList = new List<ReportDailyRemainInfo>();
                    ReportDailyRemainInfo rd = new ReportDailyRemainInfo();
                    CashManagementDA da = new CashManagementDA();

                    //15.30
                    BaselineInfo blBefore = param.Baseline[0];
                    TrayMoneyInfo moneyBefore = da.GetMoneyInTray(trans, blBefore.WorkId);
                    rd.OverallCashAmt = moneyBefore.CashAmount;
                    rd.OverallChqAmt = SumCheque(moneyBefore.ChequeList);
                    //rd.OverallPayInAmt = SumPayIn(moneyBefore.PayInList);
                    rd.OverallAmt = rd.OverallCashAmt + rd.OverallChqAmt;
                    rd.ChqCount = moneyBefore.ChequeList.Count;
                    //rd.PayInCount = moneyBefore.PayInList.Count;
                    rd.RemainCheque = moneyBefore.ChequeList; //might be replaced

                    //after 15.30
                    if (param.Baseline.Count > 1)
                    {
                        BaselineInfo blAfter = param.Baseline[1];
                        TrayMoneyInfo moneyAfter = da.GetMoneyInTray(trans, blAfter.WorkId);
                        rd.OverallCashAmt_Af = moneyAfter.CashAmount - moneyBefore.CashAmount;

                        //PayIn
                        //if (SumPayIn(moneyAfter.PayInList) > rd.OverallPayInAmt)
                        //    rd.OverallPayInAmt_Af = SumPayIn(moneyAfter.PayInList) - rd.OverallPayInAmt;

                        //if (moneyAfter.PayInList.Count > rd.PayInCount)
                        //    rd.PayInCount_Af = moneyAfter.PayInList.Count - rd.PayInCount;  //minus before
                        
                        //Cheque
                        //if(SumCheque(moneyAfter.ChequeList) > rd.OverallChqAmt)
                        //    rd.OverallChqAmt_Af = (SumCheque(moneyAfter.ChequeList) - rd.OverallChqAmt) + rd.OverallPayInAmt_Af;
                        
                        //if(moneyAfter.ChequeList.Count > rd.ChqCount)
                        //    rd.ChqCount_Af = (moneyAfter.ChequeList.Count - rd.ChqCount) + rd.PayInCount_Af;  //minus before

                        if (SumCheque(moneyAfter.ChequeList) > rd.OverallChqAmt)
                            rd.OverallChqAmt_Af = (SumCheque(moneyAfter.ChequeList) - rd.OverallChqAmt);

                        if (moneyAfter.ChequeList.Count > rd.ChqCount)
                            rd.ChqCount_Af = (moneyAfter.ChequeList.Count - rd.ChqCount);  //minus before

                        rd.OverallAmt_Af = rd.OverallCashAmt_Af + rd.OverallChqAmt_Af;
                        
                        //rd.RemainPayin = moneyAfter.PayInList;
                        rd.RemainCheque = moneyAfter.ChequeList;
                    }

                    rd.CloseWorkDate = param.DateString;
                    rdList.Add(rd);

                    trans.Commit();
                    return rdList;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public bool ExistSAPRefNo(string sapRefNo, string workId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    bool ret = da.ExistSAPRefNo(trans, sapRefNo, workId);

                    trans.Commit();
                    return ret;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public ReportBankPayInDetailInfo GetHistBankPayInDetailReport(ReportParam param)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public ReportWorkFlowSummary GetHistWorkFlowReport(ReportParam param)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private decimal? SumCheque(BindingList<ChequeInfo> chqList)
        {
            decimal? sumAmt = 0;
            foreach (ChequeInfo chq in chqList)
                sumAmt += chq.Amount.Value;

            return sumAmt;
        }

        //private decimal? SumPayIn(BindingList<PayInInfo> payInList)
        //{
        //    decimal? sumAmt = 0;
        //    foreach (PayInInfo PayIn in payInList)
        //        sumAmt += PayIn.Amount.Value;

        //    return sumAmt;
        //}


        public void ForceCloseWork(DbTransaction trans, WorkInfo workInfo)
        {
            try
            {
                CloseWorkSubmitInfo closeWorkInfo = new CloseWorkSubmitInfo();
                closeWorkInfo.WorkId = workInfo.WorkId;
                closeWorkInfo.CashNextWork = workInfo.WorkCashAmt;
                closeWorkInfo.CloseWorkBy = workInfo.CloseWorkBy;
                closeWorkInfo.BranchId = workInfo.BranchId;
                closeWorkInfo.PosId = workInfo.PosId;
                CloseWork(closeWorkInfo);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public CashierPosIdInfo LoadCashierPosId(string branchId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    CashierPosIdInfo cp = new CashierPosIdInfo();
                    cp.CashierList = da.ListCashierOfWork(trans, branchId);
                    //cp.PosList = da.ListPOSIdOfWork(branchId);

                    trans.Commit();
                    return cp;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public List<BaselineInfo> GetBaseline(string branchId, DateTime baselineDt)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<BaselineInfo> list = da.GetBaseline(trans, branchId, baselineDt);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public List<string> LoadSAPReference(string workId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<string> list = da.GetSAPReference(trans, workId);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public List<ChequeInfo> GetChequeDailyRemainReport(ReportParam param)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //Created By TongKung
        //wrapped up, not use anymore 
        public string OpenWork(DbTransaction trans, OpenWorkParam param)
        {
            try
            {
                CashManagementDA da = new CashManagementDA();
                //open work
                param.Status = "1";
                param.PostedBranchId = Session.Branch.PostedServerId;
                //received workId generated by Stored-Proc
                string workId = da.OpenWork(trans, param);

                //if (!param.IsSystemInit && param.FlowType == FlowType.MoneyOpeningBalance) // มี code อีกแบบเรียก IsSystemInit ด้วย แต่คิดว่าไม่ต้องใช้

                //if flow type is opening balance, we must update workId on "CashierMoneyFlow"
                if (param.FlowType == FlowType.MoneyOpeningBalance)
                    da.UpdateCashierMoneyFlowForWorkId(trans, param.FlowId, workId, param.PostedBranchId, param.ModifiedBy);

                //return workId for using in cashmangement system
                return workId;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        public string OpenWork(OpenWorkParam param)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            CashManagementDA da = new CashManagementDA();

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    param.Status = "1";
                    param.PostedBranchId = Session.Branch.PostedServerId;
                    string workid = da.OpenWork(trans, param);

                    //if flow type is opening balance, we must update workId on "CashierMoneyFlow"
                    if (param.FlowType == FlowType.MoneyOpeningBalance)
                        da.UpdateCashierMoneyFlowForWorkId(trans, param.FlowId, workid, param.PostedBranchId, param.ModifiedBy);

                    //string workid = OpenWork(trans, param);
                    trans.Commit();

                    //-- เรียก web service เพื่อ update cache หลัง commit
                    string authenurl = ConfigurationManager.AppSettings["CASHIER_GATEWAY"];
                    if (!string.IsNullOrEmpty(authenurl))
                    {
                        try
                        {
                            CashierCachingWS.CashierCachingWebService ccws = new PEA.BPM.CashManagementModule.BS.CashierCachingWS.CashierCachingWebService();
                            ccws.Url = authenurl + "CashierCachingWebService.asmx";
                            ccws.CacheOpenWork(workid, param.CashierId);
                        }
                        catch (System.Net.WebException)
                        {
                            //-- do nothing
                        }
                    }

                    return workid;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public string ResponseTransferedItems(List<string> transferId, string workId, string status, string posId, string branchId, string modifiedBy)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            string ret = null;

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    ret = ResponseTransferedItems(trans, transferId, workId, status, posId, branchId, modifiedBy);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return ret;
        }


        public List<CashierInfo> GetOpenWorkCashierOfBranch(string branchId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<CashierInfo> list = da.GetOpenWorkCashierOfBranch(trans, branchId);

                    trans.Commit();
                    return list;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public List<WorkInfo> ListAllOpenWork(string branchId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    List<WorkInfo> workList = da.LoadAllOpenWork(trans, branchId);

                    //find money remain of each work
                    foreach (WorkInfo work in workList)
                    {
                        if (work.Status == "เปิดกะ") //opening
                        {
                            TrayMoneyInfo workMoneyInfo = da.GetMoneyInTray(trans, work.WorkId);
                            work.WorkChequeAmt = SumCheque(workMoneyInfo.ChequeList);
                            work.WorkCashAmt = workMoneyInfo.CashAmount;
                            if (workMoneyInfo.CashAmount == null)
                                workMoneyInfo.CashAmount = Decimal.Zero;
                            work.TotalWorkMoneyAmt = work.WorkChequeAmt + work.WorkCashAmt;
                        }
                        else //closed
                        {
                            //ปิดกะแล้วดึงจากยอดยกไป จะเร็วกว่ามาก
                            //To Fix
                            decimal[] closingBalance = da.GetClosingBalance(trans, work.WorkId);
                            work.WorkCashAmt = closingBalance[0];
                            work.WorkChequeAmt = closingBalance[1];
                            work.TotalWorkMoneyAmt = closingBalance[2];
                        }
                    }

                    trans.Commit();
                    return workList;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public TrayMoneyInfo GetMoneyInTray(string workId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    TrayMoneyInfo trayMoneyInfo = new TrayMoneyInfo();
                    CashManagementDA da = new CashManagementDA();
                    trayMoneyInfo = da.GetMoneyInTray(trans, workId);

                    trans.Commit();
                    return trayMoneyInfo;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        private List<FlowSummaryInfo> GetNetFlow(List<FlowSummaryInfo> allFlow)
        {
            List<FlowSummaryInfo> flowList = new List<FlowSummaryInfo>();
            foreach (FlowSummaryInfo flow in allFlow)
            {
                if (flow.FlowCat == "1")
                    flowList.Add(flow);
            }

            return flowList;
        }

        private List<FlowSummaryInfo> GetCancelledFlow(List<FlowSummaryInfo> allFlow)
        {
            List<FlowSummaryInfo> flowList = new List<FlowSummaryInfo>();
            foreach (FlowSummaryInfo flow in allFlow)
            {
                if (flow.FlowCat == "2")
                    flowList.Add(flow);
            }

            return flowList;
        }   

        public CloseWorkSummaryInfo GetCloseWorkFlowItem(string workId)
        {
            CloseWorkSummaryInfo closeInfo = new CloseWorkSummaryInfo();
            CashManagementDA da = new CashManagementDA();

            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    List<FlowSummaryInfo> flowList = da.GetWorkFlow(trans, workId);
                    closeInfo.FlowList = GetNetFlow(flowList);
                    closeInfo.CancelledFlowList = GetCancelledFlow(flowList);
                    closeInfo.SumCashIn = 0;
                    closeInfo.SumChequeIn = 0;
                    closeInfo.SumCashOut = 0;
                    closeInfo.SumChequeOut = 0;
                    closeInfo.TotalCheque = 0;
                    closeInfo.TotalCash = 0;
                    closeInfo.CashLastWork = 0;
                    closeInfo.CashNextWork = 0;
                    closeInfo.ChequeLastWork = 0;
                    closeInfo.ChequeNextWork = 0;
                    closeInfo.TotalPayIn = 0;

                    //summary net 
                    foreach (FlowSummaryInfo flow in closeInfo.FlowList)
                    {
                        //1) เงินสดยกมา
                        //2) เช็คยกมา
                        if (flow.FlowType == FlowType.MoneyOpeningBalance ||
                            flow.FlowType == FlowType.SystemInitialCash)
                        {
                            closeInfo.CashLastWork += flow.CashIn;
                            closeInfo.ChequeLastWork += flow.ChequeIn;
                        }
                        //รับเงินสด
                        else if (flow.FlowType == FlowType.MoneyFromBank ||
                                    flow.FlowType == FlowType.MoneyFromAnotherCashier ||
                                    flow.FlowType == FlowType.MoneyReceivedFromPOS ||
                                    flow.FlowType == FlowType.CancelledPOSPaymentable ||
                                    flow.FlowType == FlowType.CancelledBankDelivery ||
                                    flow.FlowType == FlowType.Adjust_MoneyFromBank_Plus ||
                                    flow.FlowType == FlowType.Adjust_MoneyReceivedFromPOS_Plus ||
                                    flow.FlowType == FlowType.Adjust_MoneyDepositToBank_Minus ||
                                    flow.FlowType == FlowType.Adjust_CashOutFromPOS_Minus)
                        {
                            closeInfo.SumCashIn += flow.CashIn;
                            closeInfo.SumChequeIn += flow.ChequeIn;
                        }
                        else if (flow.FlowType == FlowType.MoneyTransferedToAnotherCashier ||
                                  flow.FlowType == FlowType.MoneyDepositToBank ||
                                  flow.FlowType == FlowType.CashOutFromPOS ||
                                  flow.FlowType == FlowType.CancelledPOSReceivable ||
                                  flow.FlowType == FlowType.CancelledMoneyCheckIn ||
                                  flow.FlowType == FlowType.Adjust_MoneyFromBank_Minus ||
                                  flow.FlowType == FlowType.Adjust_MoneyReceivedFromPOS_Minus ||
                                  flow.FlowType == FlowType.Adjust_MoneyDepositToBank_Plus ||
                                  flow.FlowType == FlowType.Adjust_CashOutFromPOS_Plus)
                        {
                            closeInfo.SumCashOut += flow.CashOut;
                            closeInfo.SumChequeOut += flow.ChequeOut;
                        }
                        else
                        {
                            throw new Exception("BS: Invalid flow type!");
                        }
                    }

                    //summary outbound 
                    //มีการยกเลิกนอกกะ รายการที่เป็นเงินเข้า จะต้องนำมารวมในฝั่ง เงินเข้า, และรายการที่เป็นการจ่ายออก ให้ลงในช่องจ่ายออก

                    //รวมเงินสด = เงินสดยกมา + รับเงินสด
                    closeInfo.TotalCash = closeInfo.SumCashIn + closeInfo.CashLastWork;

                    //รวมเช็ค = เช็คยกมา + รับเช็ค
                    closeInfo.TotalCheque = closeInfo.SumChequeIn + closeInfo.ChequeLastWork;

                    //เงินสดยกไป
                    closeInfo.CashNextWork = closeInfo.TotalCash - closeInfo.SumCashOut;

                    //เช็คยกไป
                    closeInfo.ChequeNextWork = closeInfo.TotalCheque - closeInfo.SumChequeOut;

                    //ใบนำฝาก
                    decimal? totalPayIn = da.GetPayInOfWork(trans, workId);
                    closeInfo.TotalPayIn = totalPayIn;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

            return closeInfo;
        }

        public ReportWorkFlowSummary GetWorkFlowReport(string workId)
        {
            DbTransaction trans;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    ReportWorkFlowSummary ret = new ReportWorkFlowSummary();
                    CashManagementDA da = new CashManagementDA();
                    ret.FlowList = da.GetWorkFlow(trans, workId);

                    //fill header
                    WorkInfo workInfo = da.GetWorkInfo(trans, workId);
                    ret.CashierId = workInfo.CashierId;
                    ret.CashierName = workInfo.CashierName;
                    ret.PosId = workInfo.PosId;
                    ret.OpenWorkDt = workInfo.OpenWorkDt;
                    ret.CloseWorkDt = workInfo.CloseWorkDt;

                    trans.Commit();
                    return ret;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        #endregion


        #region add by P.Wongket

        public CashierWorkStatus GetCashierWorkStatus(string workId)
        {
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                try
                {
                    CashManagementDA da = new CashManagementDA();
                    return da.GetCashierWorkStatus(workId);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion
    }
}
