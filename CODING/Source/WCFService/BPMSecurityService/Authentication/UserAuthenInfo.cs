using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace PEA.BPM.WebService.Security
{
    public class UserAuthenInfo
    {
        DateTime _lastheartbeat;
        int _idlecount;
        CachingUser _cachinguser;

        public DateTime LastHeartBeat
        {
            get { return _lastheartbeat;  }
            set { _lastheartbeat = value; }
        }
        public int IdleCount
        {
            get { return _idlecount; }
            set { _idlecount = value; }
        }
        public CachingUser User
        {
            get { return _cachinguser; }            
        }

        public UserAuthenInfo(BPMAuthenticationDS.UserRow drow)
        {
            _idlecount = 0;
            _cachinguser = new CachingUser(drow);
        }

    }
}
