using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace PEA.BPM.WebService.Security.Core
{
    public class SystemInfo
    {
        #region Singleton
        private static readonly SystemInfo _instance = new SystemInfo();
        public SystemInfo()
        {
        }

        public static SystemInfo Instance
        {
            get { return _instance; }
        }
        #endregion

        public string LastStartUpTime = "";
        public string Version = "1.13";

        public Features Feature = new Features();
        public SystemActivity SysActivity = new SystemActivity();



        #region Record stats
        internal void OnRecodeStats()
        {
            if (!Feature.IsRecodeStat) return;

            int activeuser = AuthenticationController.Instance.GetActiveUserCount();
            int cacheuser = AuthenticationController.Instance.GetUserCountInSystem();
            ServiceLog.Instance.WriteUserStat(activeuser, cacheuser);
        }
        #endregion

    }
}
