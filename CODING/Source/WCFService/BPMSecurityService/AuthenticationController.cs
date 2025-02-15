using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Transactions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using PEA.BPM.WebService.Security.BPMAuthenticationDSTableAdapters;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.WebService.Security.Core;

namespace PEA.BPM.WebService.Security
{
    public class AuthenticationController
    {
        #region Singleton
        private static readonly AuthenticationController _instance = new AuthenticationController();
        private AuthenticationController()
        {
            _authenhash = new Dictionary<string, UserAuthenInfo>();
            _userada = new UserTableAdapter();
            _systeminfo = new SystemInfo();
        }

        public static AuthenticationController Instance
        {
            get { return _instance; }
        }
        #endregion

        /// <summary>
        /// ������ show ��������´�ͧ service ��� run ���� � �͹���
        /// </summary>
        public SystemInfo System
        {
            get { return _systeminfo; }
            set { _systeminfo = value; }
        }

        private SystemInfo _systeminfo = null; // ����������´ service
        private Dictionary<string, UserAuthenInfo> _authenhash = null; // �������� hash ���� check ��� data �ͧ user ����� memory ���������ѧ
        private UserTableAdapter _userada = null; // Adapter ��������� select/update ������� database
        

        /// <summary>
        /// ��ҹ�����Ţͧ user ���� �ҡ database �������� datatable
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private UserAuthenInfo GetAuthenInfoFromDB(string userid)
        {
            ServiceLog.Instance.WriteLine(LogType.Normal, "Get user [" + userid + "] from database.");

            BPMAuthenticationDS.UserDataTable dt = _userada.GetByUserID(userid);            
            if (dt.Count == 0) return null;

            UserAuthenInfo uai = new UserAuthenInfo(dt[0]);
            uai.IdleCount = 0;
            uai.LastHeartBeat = DateTime.Now;
            return uai;
        }
        
        /// <summary>
        /// return �����Ţͧ User ��Ѻ��´ٴ�����Ҷ���� load �����Ţͧ user ����������������Ҩҡ memory �͡� ��������� load ��� load �ҡ database
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="isresetheartbeat"></param>
        /// <returns></returns>
        private UserAuthenInfo GetAuthenInfo(string userid)
        {
            lock (this)
            {
                UserAuthenInfo uai;
                if (_authenhash.ContainsKey(userid))
                {
                    uai = _authenhash[userid];
                    if (uai.IdleCount >= System.Feature.CheckUserOfflineClearCacheCount)
                    {
                        _authenhash.Remove(userid);
                        uai = GetAuthenInfoFromDB(userid);
                        if (uai != null) _authenhash.Add(userid, uai);
                    }
                    else
                    {
                        uai.IdleCount = 0; // reset ��� idle
                        uai.LastHeartBeat = DateTime.Now;
                    }
                }
                else
                {
                    uai = GetAuthenInfoFromDB(userid);
                    if (uai != null) _authenhash.Add(userid, uai);
                }
                return uai;
            }
        }

        /// <summary>
        /// Convert logic �Ҩҡ store procedure ta_get_CheckDoubleLogIn
        /// </summary>
        /// <param name="clientip"></param>
        /// <param name="userid"></param>
        /// <param name="terminalip"></param>
        /// <returns></returns>
        internal string CheckLoginDouble(string clientip, string userid, string terminalip)
        {
            ServiceLog.Instance.WriteLine(LogType.Normal, clientip, "CheckLoginDouble user [" + userid + "] from [" + terminalip + "]");
            UserAuthenInfo uai = GetAuthenInfo(userid);
            if (uai == null) throw new Exception("Invaild userid [" + userid + "]");

            if (uai.User.CurIP == null)
            {
                lock (this)
                {
                    uai.User.CurIP = terminalip;
                    uai.User.ReqFlag = "0";
                }
                return "STAMP_IP";
            }
            else
            {
                if (uai.User.CurIP == terminalip)
                {
                    if (uai.User.ReqFlag == "0")
                    {
                        return "SAME_IP_No_REQ"; // --[== Still same current IP : no request from M2 ==]
                    }
                    else if (uai.User.ReqFlag == "1")
                    {
                        return "SAME_IP_REQ"; // --[== M2 request you to logout ==]
                    }
                    else if (uai.User.ReqFlag == "2")
                    {
                        lock (this)
                        {
                            uai.User.ReqFlag = "0";
                        }
                        return "";
                    }
                }
                else
                {
                    if (uai.User.ReqFlag == "0")
                    {
                        return "DIF_IP_REQ"; // --[== 2nd machine could ask M1 to logout by set ReqFlag = 1 ==]
                    }
                    else if (uai.User.ReqFlag == "1")
                    {
                        return "DIF_IP_WAIT"; // --[== wait for M1 to logout ==]
                    }
                    else if (uai.User.ReqFlag == "2")
                    {
                        lock (this)
                        {
                            uai.User.CurIP = terminalip;
                            uai.User.ReqFlag = "0";
                        }
                        return "DIF_IP_M1_EXIST";
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// Convert logic �Ҩҡ store procedure ta_upd_CurIPReqFlag 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="terminalip"></param>
        /// <param name="reqflag"></param>
        internal void UpdateCurIPReqFlag(string userid, string terminalip, string reqflag)
        {
            ServiceLog.Instance.WriteLine(LogType.Normal, "UpdateCurIPReqFlag user [" + userid + "] from [" + terminalip + "] with reqflag [" + reqflag + "]");
            UserAuthenInfo uai = GetAuthenInfo(userid);
            if (uai == null) throw new Exception("Invaild userid [" + userid + "]");

            lock (this)
            {
                if (terminalip.Length == 0)
                {
                    uai.User.ReqFlag = reqflag;
                }
                else
                {
                    uai.User.CurIP = terminalip;
                    uai.User.ReqFlag = reqflag;
                }
            }
        }
        
        /// <summary>
        /// ta_get_CheckAuthen
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="hashpassword"></param>
        /// <param name="postedserverid"></param>
        /// <returns></returns>
        internal string IsAuthenticated(string userid, string hashpassword, string regbranchid, string postedserverid)
        {
            #region Stored code
//-- =============================================
//-- Author:		<Author,,Name>
//-- Create date: <Create Date,,>
//-- Description:	<Description,,>
//-- =============================================
//ALTER PROCEDURE [dbo].[ta_get_CheckAuthen] @UserId char(8),
//    @HashPassword char(40),
//    @PostedServerId char(7),
//    @RegBranchId varchar(6) = '%'
//AS
//BEGIN

//    Set Nocount ON

//--    IF(@RegBranchId IS NULL) SET @RegBranchId = '%'

//    Declare @dayToExpire int,	
//            @PwdExpireDt datetime,	
//            @Rcount int,
//            @tmpId char(8),
//            @BranchId varchar(6);

//        --Select datediff(d, getDate(), IsNull(PwdExpiredDt,getDate()))
		
//--        Select @PwdExpireDt = IsNull(PwdExpiredDt,getDate()),
//--            @BranchId=BranchId
//--        From ta.[User]
//--        Where UserId=@UserId
//--            and Password=@HashPassword
//--            and IsNull(PwdExpiredDt,getDate())>=getDate()
//            --and (@RegBranchId='%' OR BranchId=SubString(@RegBranchId, 1, 4))
//--            and Active='1'
//    SET @Rcount = @@ROWCOUNT
//--    SET @dayToExpire = datediff(d, getDate(), @PwdExpireDt)
	
//    print @Rcount

//    IF @Rcount=1 
//        Begin

//            If (@RegBranchId<>'%' AND 
//                substring(@BranchId, 1, 4) <> Substring(@RegBranchId, 1 ,4)  AND
//                substring(@BranchId, 1, 4) <> substring(@RegBranchId, 1,1) + '000') 
//                Select 'InvalidBranch';
//            Else
//                Begin

//                    Update ta.[User]
//                        set LastLogin=getDate(),
//                            LastRequest=getDate(),
//                            SyncFlag='1',
//                            PostDt=getDate(),
//                            PostBranchServerId=@PostedServerId,
//                            ModifiedDt=getDate(),
//                            ModifiedBy=@UserId
//                        where UserId=@UserId;

//                    IF @dayToExpire<7
//                        Select 'PwdExpired';
//                    Else
//                        Select 'Valid';
//                End

//        End
//    Else
//        Begin
//            Select 'Invalid'	
//        End

//    Set Nocount OFF

//END
            #endregion

            if (userid == null) return "Invalid";
            UserAuthenInfo uai = GetAuthenInfo(userid);
            if (uai == null) return "Invalid"; // Where UserId=@UserId
            if (uai.User.Password == null) return "Invalid";
            if (uai.User.Active == null) return "Invalid";
            if (string.IsNullOrEmpty(regbranchid)) regbranchid = "%"; // IF(@RegBranchId IS NULL) SET @RegBranchId = '%'

            ServiceLog.Instance.WriteLine(LogType.Normal, "IsAuthenticated user [" + userid + "] [" + hashpassword + "] [" + regbranchid + "] [" + postedserverid + "]");

            DateTime pwdexpired; // ���ҧ���� Password expired date 
            DateTime dtnow = DateTime.Now;
            if (uai.User.Password != hashpassword) return "Invalid"; // and Password=@HashPassword
            if (uai.User.Active != "1") return "Invalid"; // and Active='1'
            if (uai.User.PwdExpiredDt == null) pwdexpired = dtnow; // Select @PwdExpireDt = IsNull(PwdExpiredDt,getDate()),
            else pwdexpired = uai.User.PwdExpiredDt.Value;
            if (pwdexpired < dtnow) return "Invalid"; // and IsNull(PwdExpiredDt,getDate())>=getDate() ����� null ���繨�ԧ���� ������ null �������º���Ҵٶ�ҹ��¡��Ҩ� false         


            if (uai.User.BranchId != null) // ��� BranchId �� NULL �����价ء���
            {
                if (regbranchid != "%") // ��� = % �������ء��� // If (@RegBranchId<>'%' AND 
                {
                    if (!(uai.User.BranchId.Substring(0, 4) == regbranchid.Substring(0, 4) || // substring(@BranchId, 1, 4) <> Substring(@RegBranchId, 1 ,4)  AND
                          uai.User.BranchId.Substring(0, 4) == regbranchid.Substring(0, 1) + "000")) // substring(@BranchId, 1, 4) <> substring(@RegBranchId, 1,1) + '000') 
                        return "InvalidBranch";
                }
            }

            lock (this)
            {
                uai.User.LastLogin = dtnow;
                uai.User.LastRequest = dtnow;
                uai.User.SyncFlag = "1";
                uai.User.PostDt = dtnow;
                uai.User.PostBranchServerId = postedserverid;
                uai.User.ModifiedDt = dtnow;
                uai.User.ModifiedBy = userid;
            }

            TimeSpan daytoexpire = pwdexpired.Subtract(dtnow); // SET @dayToExpire = datediff(d, getDate(), @PwdExpireDt)
            if (daytoexpire.TotalDays < 7) return "PwdExpired";
            else return "Valid";
        }

        internal string IsAuthenticated(string userid, string hashpassword, string postedserverid)
        {
            return IsAuthenticated(userid, hashpassword, null, postedserverid);
        }

        /// <summary>
        /// ta_get_CheckToken
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        internal string CheckToken(string userid, string token)
        {
            #region Stored code
            //ALTER PROCEDURE [dbo].[ta_get_CheckToken] @UserId char(8),
            //    @Token char(26)
            //AS
            //BEGIN

            //    Declare @DbToken char(26);
            //    Declare @LastRequest datetime;


            //    Select @DbToken=Token, @LastRequest=LastRequest
            //        from ta.[User]
            //        where UserId=@UserId;

            //    IF @@ROWCOUNT=1 
            //        Begin
            //            If @DbToken=@Token
            //                Begin
            //                    IF DateDiff(n, @LastRequest, GetDate())<120
            //                        Begin
            //                            Update ta.[User]
            //                                Set LastRequest=GetDate()
            //                                Where UserId=@UserId;
            //                            Select '';
            //                        End
            //                    Else
            //                        Begin
            //                            Select 'TokenExpired';
            //                        End			
            //                End
            //            Else
            //                Begin
            //                    Select 'TokenNotMatch';
            //                End
            //        End
            //    Else
            //        Begin
            //            Select 'NotFoundUser';
            //        End

            //END
            #endregion

            UserAuthenInfo uai = GetAuthenInfo(userid);
            if (uai == null) return "NotFoundUser";

            DateTime dtnow = DateTime.Now;
            if (uai.User.Token == null || uai.User.Token != token) // reverse logic ����� null ���� @DbToken <> @Token
            {
                return "TokenNotMatch";
            }
            else
            {
                if (uai.User.LastRequest == null)
                {
                    lock (this)
                    {
                        uai.User.LastRequest = dtnow; // Set LastRequest=GetDate()
                    }
                    return ""; // Select '';
                }
                else
                {
                    TimeSpan diff = dtnow.Subtract(uai.User.LastRequest.Value);
                    if (diff.TotalMinutes < 120) // IF DateDiff(n, @LastRequest, GetDate())<120
                    {
                        lock (this)
                        {
                            uai.User.LastRequest = dtnow; // Set LastRequest=GetDate()
                        }
                        return ""; // Select '';
                    }
                    else return "TokenExpired"; // Select 'TokenExpired';
                }
            }            
        }

        /// <summary>
        /// ta_get_AuthenToken
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="hashpassword"></param>
        /// <returns></returns>
        public string GetToken(string userid, string hashpassword)
        {
            #region Stored code
            //-- =============================================
            //-- Author:		<Author,,Name>
            //-- Create date: <Create Date,,>
            //-- Description:	<Description,,>
            //-- =============================================
            //ALTER PROCEDURE [dbo].[ta_get_AuthenToken] @UserId char(8),
            //    @HashPassword char(40)
            //AS
            //BEGIN

            //    Select @UserId=UserId
            //        From ta.[User]
            //        Where UserId=@UserId
            //            and Password=@HashPassword
            //            and Active='1'

            //    IF @@ROWCOUNT=1 
            //        Begin
            //            Declare @Token varchar(26);
            //            Set @Token = @UserId + '-' + convert(varchar, getdate(), 112) + '-' + convert(varchar, getdate(), 108);

            //            Update ta.[User]
            //                set Token=@Token
            //                    ,LastRequest=getDate()
            //                where UserId=@UserId;

            //            Select @Token;

            //        End
            //    Else
            //        Begin
            //            Select '';
            //        End

            //END
            #endregion

            UserAuthenInfo uai = GetAuthenInfo(userid);
            if (uai == null) return ""; // Where UserId=@UserId
            //if (uai.User.Password == null || uai.User.Password != hashpassword) return ""; // and Password=@HashPassword
            if (uai.User.Active == null || uai.User.Active != "1") return ""; // and Active='1'

            DateTime dtnow = DateTime.Now;
            string token = userid + "-" + dtnow.Year + dtnow.ToString("MMdd") + "-" + dtnow.ToString("HH:mm:ss"); // Set @Token = @UserId + '-' + convert(varchar, getdate(), 112) + '-' + convert(varchar, getdate(), 108);
            lock (this)
            {
                uai.User.Token = token; // set Token=@Token
                uai.User.LastRequest = dtnow; // ,LastRequest=getDate()
            }
            return token;
        }

        /// <summary>
        /// ta_get_UserProfile
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserProfile LoadUserProfile(string userid)
        {
            #region Stored code
//ALTER PROCEDURE [dbo].[ta_get_UserProfile] @UserId char(8)
//AS
//BEGIN

//    Update ta.[User]
//        Set LastLogin = getDate()
//        Where UserId=@UserId	And
//            Active=1;

//    Select FullName, Department, getDate() As LoginTime
//        From ta.[User]
//        Where UserId=@UserId
//            And Active='1';

//END
            #endregion

            UserAuthenInfo uai = GetAuthenInfo(userid);
            if (uai == null) return null;
            if (uai.User.Active == null || uai.User.Active != "1") return null;

            DateTime dtnow = DateTime.Now;
            lock (this)
            {
                uai.User.LastLogin = dtnow;
            }

            UserProfile up = new UserProfile();
            up.UserId = uai.User.UserId;
            up.Name = uai.User.FullName;
            up.Department = uai.User.Department;
            up.LoginTime = dtnow;
            return up;
        }

        public void LogOff(string userid)
        {

        }


        public void ModifyStringValue(string userid, string columnname, string value)
        {
            UserAuthenInfo uai = GetAuthenInfo(userid);
            if (uai == null) return;
            columnname = columnname.ToLower();

            lock (this)
            {
                switch (columnname)
                {
                    case "password": uai.User.Password = value;
                        break;
                    case "fullname": uai.User.FullName = value;
                        break;
                    case "department": uai.User.Department = value;
                        break;
                    case "token": uai.User.Token = value;
                        break;
                    case "branchid": uai.User.BranchId = value;
                        break;
                    case "postbranchserverid": uai.User.PostBranchServerId = value;
                        break;
                    case "syncflag": uai.User.SyncFlag = value;
                        break;
                    case "modifiedby": uai.User.ModifiedBy = value;
                        break;
                    case "active": uai.User.Active = value;
                        break;
                    case "curip": uai.User.CurIP = value;
                        break;
                    case "reqip": uai.User.ReqIP = value;
                        break;
                    case "reqflag": uai.User.ReqFlag = value;
                        break;
                    default:
                        throw new Exception("Invalid column name [" + columnname + "]");
                }
            }
        }
        public void ModifyDateTimeValue(string userid, string columnname, DateTime value, bool isnull)
        {
            UserAuthenInfo uai = GetAuthenInfo(userid);
            if (uai == null) return;

            lock (this)
            {
                switch (columnname)
                {
                    case "lastrequest":
                        if (isnull) uai.User.LastRequest = null;
                        else uai.User.LastRequest = value;
                        break;
                    case "lastlogin":
                        if (isnull) uai.User.LastLogin = null;
                        else uai.User.LastLogin = value;
                        break;
                    case "pwdexpireddt":
                        if (isnull) uai.User.PwdExpiredDt = null;
                        else uai.User.PwdExpiredDt = value;
                        break;
                    case "postdt":
                        if (isnull) uai.User.PostDt = null;
                        else uai.User.PostDt = value;
                        break;
                    case "modifieddt":
                        if (isnull) uai.User.ModifiedDt = null;
                        else uai.User.ModifiedDt = value;
                        break;
                    default:
                        throw new Exception("Invalid column name [" + columnname + "]");
                }
            }
        }


        #region Management method
        /// <summary>
        /// ������͡��� User ����������к���Һ�ҧ
        /// </summary>
        /// <returns></returns>
        internal List<UserAuthenInfo> GetActiveUser()
        {
            List<UserAuthenInfo> activelist = new List<UserAuthenInfo>();
            lock (this)
            {
                foreach (UserAuthenInfo uai in _authenhash.Values)
                {
                    if (uai.User.CurIP != null) activelist.Add(uai);
                }
            }
            return activelist;
        }

        /// <summary>
        /// ������͡��� User ���������к���� ���˹ offline ����Ǻ�ҧ
        /// </summary>
        /// <returns></returns>
        internal List<UserAuthenInfo> GetOfflineUser()
        {
            List<UserAuthenInfo> offlinelist = new List<UserAuthenInfo>();
            lock (this)
            {
                foreach (UserAuthenInfo uai in _authenhash.Values)
                {
                    if (uai.User.CurIP == null) offlinelist.Add(uai);
                }
            }
            return offlinelist;
        }

        /// <summary>
        /// �ӹǹ User ��� cache ������к�
        /// </summary>
        /// <returns></returns>
        internal int GetUserCountInSystem()
        {
            lock (this)
            {
                return _authenhash.Values.Count;
            }
        }

        /// <summary>
        /// Hahs �ͧ User
        /// </summary>
        /// <returns></returns>
        internal Dictionary<string, UserAuthenInfo> GetUserCachingTable()
        {
            return _authenhash;
        }

        #endregion



        #region Timer method

        #region Check offline
        internal void OnCheckOffline()
        {
            if (!System.Feature.IsCheckUserOffline) return;
            lock (this)
            {
                //-- �Ѵ�������ǡѺ��� logoff
                foreach (UserAuthenInfo uai in _authenhash.Values)
                {
                    uai.IdleCount++;

                    if (uai.User.CurIP == null) continue; // logoff ���������ͧ����ҤԴ�ա
                    if (uai.IdleCount >= this.System.Feature.CheckUserOfflineCount)
                    {
                        //-- ������ logoff �����
                        ServiceLog.Instance.WriteLine(LogType.System, "User [" + uai.User.UserId + "] offline from IP [" + uai.User.CurIP + "].");
                        uai.User.CurIP = null;
                        uai.User.ReqFlag = "0";
                        uai.IdleCount = 0;
                        uai.LastHeartBeat = DateTime.Now;
                    }
                }
            }
        }
        #endregion

        #region Sync to database
        internal void OnSyncToDatabase()
        {
            if (!System.Feature.IsSyncToDatabase) return; // ����Դ flag ��� sync ������ŧ database
            try
            {
                ServiceLog.Instance.WriteLine(LogType.System, "Update into database.");

                string connstr = ConfigurationManager.ConnectionStrings["BPMDatabase"].ConnectionString;
                using (SqlConnection sqlconn = new SqlConnection(connstr))
                {
                    sqlconn.Open();
                    using (TransactionScope trans = new TransactionScope())
                    {
                        lock (this)
                        {
                            foreach (UserAuthenInfo uai in _authenhash.Values)
                            {
                                if (!uai.User.IsDirty) continue;
                                uai.User.UpdateToDatabase(sqlconn);
                            }
                        }
                        trans.Complete();
                    }
                }
            }
            catch (Exception ee)
            {
                ServiceLog.Instance.WriteLine(LogType.Error, "Failed. Update into database. [" + ee.Message + "]");
                throw ee;
            }           
        }
        #endregion

        #region Update statistic
        internal void OnUpdateStats()
        {
            
        }
        #endregion

        #endregion


    }
}


