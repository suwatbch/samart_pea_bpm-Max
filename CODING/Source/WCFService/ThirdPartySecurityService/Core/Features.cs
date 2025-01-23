using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace PEA.BPM.ThirdParty.WebService.Security.Core
{
    public class Features
    {
        public bool IsSyncToDatabase = true;
        public int SyncToDatabaseInterval = 60000;      
        
        public bool IsCheckUserOffline = true;
        public int CheckUserOfflineInterval = 60000;
        public int CheckUserOfflineCount = 5;
        public int CheckUserOfflineClearCacheCount = 5;


        public int NewsUpdateInterval = 60000;


        public bool IsRecodeStat = true;
        public int RecodeStatInterval = 60000;

        public bool IsSaveLogToDatabase = true;
    }
}
