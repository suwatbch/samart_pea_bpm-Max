using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using System.Collections.Generic;
using PEA.BPM.WebService.Security.BPMAuthenticationDSTableAdapters;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Data.SqlClient;

namespace PEA.BPM.WebService.Security.Cashier
{
    
    public class CashierCachingController
    {
        #region Singleton
        private static readonly CashierCachingController _instance = new CashierCachingController();
        private CashierCachingController()
        {
            _cashierstatushash = new Dictionary<string, List<CachingCashierWorkStatus>>();
            _cashierada = new CashierWorkStatusTableAdapter();
        }

        public static CashierCachingController Instance
        {
            get { return _instance; }
        }
        #endregion

        private Dictionary<string, List<CachingCashierWorkStatus>> _cashierstatushash = null; 
        private CashierWorkStatusTableAdapter _cashierada;

        private List<CachingCashierWorkStatus> GetCashierWorkStatusFromDB(string cashierid)
        {
            ServiceLog.Instance.WriteEvent(LogType.Normal, cashierid, "Get cashier [" + cashierid + "] from database.");

            BPMAuthenticationDS.CashierWorkStatusDataTable dt = _cashierada.GetAllByCashierId(cashierid);
            List<CachingCashierWorkStatus> res = new List<CachingCashierWorkStatus>();
            foreach (BPMAuthenticationDS.CashierWorkStatusRow drow in dt)
            {
                CachingCashierWorkStatus ccws = new CachingCashierWorkStatus(drow);
                res.Add(ccws);
            }
            return res;
        }
        public List<CachingCashierWorkStatus> GetCachingCashierWorkStatus(string cashierid, bool forceupdate)
        {
            lock (this)
            {
                List<CachingCashierWorkStatus> listccws;
                cashierid = cashierid.Trim(); // always trim for safety
                if (_cashierstatushash.ContainsKey(cashierid))
                {
                    if (forceupdate)
                    {
                        _cashierstatushash.Remove(cashierid);
                        listccws = GetCashierWorkStatusFromDB(cashierid);
                        _cashierstatushash.Add(cashierid, listccws);
                    }
                    else
                    {
                        listccws = _cashierstatushash[cashierid];
                    }
                }
                else
                {
                    listccws = GetCashierWorkStatusFromDB(cashierid);
                    _cashierstatushash.Add(cashierid, listccws);
                }
                return listccws;
            }
        }

        /// <summary>
        /// cm_ins_OpenWork
        /// </summary>
        public void CacheOpenWork(string workid, string cashierid)
        {
            ServiceLog.Instance.WriteEvent(LogType.Normal, cashierid, "OpenWork [" + workid + "] [" + cashierid + "]");
            GetCachingCashierWorkStatus(cashierid, true);
        }

        public List<CashierWorkStatusInfo> IsOpenedWork(string branchid, string cashierid, string posid)
        {
            #region Stored code
//-- =============================================
//-- Author:		<Cash Management>
//-- Create date: <July, 29 08>
//-- Description:	<check someone opened work or not?>
//-- =============================================
//ALTER PROCEDURE [dbo].[cm_get_IsOpenedWork] @BranchId char(6),
//    @CashierId char(8)='',
//    @PosId char(5)=''
//AS
//BEGIN
//    DECLARE  @DayCount As int
//    IF(@PosId IS NULL) SET @PosId = ''
//    SET NOCOUNT ON;
//    BEGIN TRANSACTION 
//    SELECT  TOP (1) WorkId, CashierId, PosId, Status, OpenWorkDt, CloseWorkDt, CloseWorkBy, BranchId, 
//            BaseLine, MarkedBL, PostDt, SyncFlag, ModifiedDt, ModifiedBy, Active
//    INTO	#TEMP
//    FROM	ts.CashierWorkStatus WITH (NOLOCK)
//    WHERE	BranchId=@BranchId 
//            AND	CashierId = @CashierId
//            AND [Status]='1'
//            AND Active='1'

//    -- trick if posId changed so update it
//    DECLARE  @OldPosId char(5)
//    SELECT	@OldPosId = PosId
//    FROM	#TEMP
//    IF(@OldPosId <> @PosId AND @PosId <> '')
//    BEGIN
//        UPDATE	ts.CashierWorkStatus
//        SET		PosId = @PosId,
//                SyncFlag = '1',
//                modifiedDt=getDate(),
//                PostDt=getdate()
//        WHERE	WorkId IN (SELECT WorkId 
//                            FROM #TEMP
//                )
//    END

//    SELECT	@DayCount = COUNT(W.WorkId)
//    FROM	ts.CashierWorkStatus W WITH (NOLOCK) INNER JOIN #TEMP T ON
//                W.CashierId = T.CashierId
//    WHERE	day(W.OpenWorkDt) = day(getdate()) AND
//            month(W.OpenWorkDt) = month(getdate()) AND
//            year(W.OpenWorkDt) = year(getdate()) AND
//            W.Active = '1'

//    SELECT  #TEMP.* , ISNULL(@DayCount,0) As DayCount
//    FROM	#TEMP
//    DROP TABLE #TEMP
//    COMMIT TRANSACTION
//    SET NOCOUNT OFF;
//END
            #endregion
            ServiceLog.Instance.WriteEvent(LogType.Normal, cashierid, "IsOpenedWork [" + cashierid + "] [" + branchid + "] [" + posid + "]");

            List<CashierWorkStatusInfo> res = new List<CashierWorkStatusInfo>();
            if (posid == null) posid = "";
            if (branchid == null) branchid = "";
            if (cashierid == null) cashierid = "";

            //-- หารายการ cashier ของ branch นั้นๆ ออกมา
            CachingCashierWorkStatus branchccws = null;
            List<CachingCashierWorkStatus> ccwslist = GetCachingCashierWorkStatus(cashierid, false);
            if (ccwslist.Count == 0) return res;
            foreach (CachingCashierWorkStatus ccws in ccwslist)
            {
                if (ccws.Status == null || ccws.Status != "1") continue;
                if (ccws.Active == null || ccws.Active != "1") continue;
                if (ccws.BranchId.Trim() == branchid.Trim())
                {
                    branchccws = ccws;
                    break;
                }
            }
            if (branchccws == null) return res;

            //-- if posId changed so update it
            string oldposid = branchccws.PosId;
            DateTime dtnow = System.DateTime.Now;
            if (oldposid.Trim() != posid.Trim() && posid != "")
            {
                lock (this)
                {
                    branchccws.PosId = posid;
                    branchccws.SyncFlag = "1";
                    branchccws.ModifiedDt = dtnow;
                    branchccws.PostDt = dtnow;

                    string query = "";
                    try
                    {
                        string connstr = ConfigurationManager.ConnectionStrings["POSDatabase"].ConnectionString;
                        using (SqlConnection sqlconn = new SqlConnection(connstr))
                        {
                            sqlconn.Open();
                            query = branchccws.GetUpdateQuery();
                            branchccws.UpdateToDatabase(query, sqlconn);
                        }
                    }
                    catch (Exception ee)
                    {
                        ServiceLog.Instance.WriteEvent(LogType.Error, cashierid, "Failed. Update cashier record [" + branchccws.WorkId + "," + branchccws.CashierId + "] [" + query + "]");
                    }
                }
            }

            //-- นับว่าวันนี้เปิดกะแล้วกี่ครั้ง
            int daycount = 0;
            foreach (CachingCashierWorkStatus ccws in ccwslist)
            {
                if (ccws.OpenWorkDt == null) continue;
                if (ccws.OpenWorkDt.Value.Date == dtnow.Date && ccws.Active == "1")
                {
                    daycount++;
                }
            }

            CashierWorkStatusInfo cw = new CashierWorkStatusInfo();
            cw.WorkId = branchccws.WorkId;
            cw.CashierId = branchccws.CashierId;
            cw.PosId = branchccws.PosId;
            cw.Status = branchccws.Status;
            cw.OpenWorkDt = branchccws.OpenWorkDt;
            cw.CloseWorkDt = branchccws.CloseWorkDt;
            cw.BranchId = branchccws.BranchId;
            cw.DayCount = daycount;
            res.Add(cw);

            return res;
        }

        public void CacheCloseWork(string workid, string cashierid)
        {
            ServiceLog.Instance.WriteEvent(LogType.Normal, cashierid, "CloseWork [" + workid + "] [" + cashierid + "]");
            GetCachingCashierWorkStatus(cashierid, true);
        }

        public List<string> GetCashierIdList()
        {
            List<string> cashierlist = new List<string>();
            lock (this)
            {
                foreach (string str in _cashierstatushash.Keys)
                {
                    cashierlist.Add(str);
                }
            }
            return cashierlist;
        }

        public CachingCashierWorkStatus[] GetCachingCashierWorkStatusByCashierId(string cashierid)
        {
            if (string.IsNullOrEmpty(cashierid)) return null;
            lock(this)
            {
                if (!_cashierstatushash.ContainsKey(cashierid)) return null;
                
                List<CachingCashierWorkStatus> list = _cashierstatushash[cashierid];                
                CachingCashierWorkStatus[] res = new CachingCashierWorkStatus[list.Count];
                int count = 0;
                foreach (CachingCashierWorkStatus ccws in list)
                {
                    res[count++] = ccws;
                }
                return res;
            }
        }


    }
}
