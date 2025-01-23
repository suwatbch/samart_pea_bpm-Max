using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace PEA.BPM.WebService.Security.Admin
{
    public partial class UserMonitoring : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ActiveUserBtn_Click(object sender, EventArgs e)
        {
            TextBox1.Visible = true;            
            List<UserAuthenInfo> activeuser = AuthenticationController.Instance.GetActiveUser();
            TextBox1.Text = "Total user in system = " + AuthenticationController.Instance.GetUserCountInSystem() + Environment.NewLine;
            TextBox1.Text += "Total active user = " + activeuser.Count + Environment.NewLine;
            int count = 0;
            foreach (UserAuthenInfo uai in activeuser)
            {
                TextBox1.Text += string.Format("[{0}] [{1}] [{2}] [{3}]" + Environment.NewLine,
                    uai.User.UserId, uai.LastHeartBeat.ToString("dd/MM/yyyy HH:mm:ss"), uai.User.CurIP, uai.IdleCount);
                count++;
                if (count > 100) break;
            }
        }

        protected void OfflineUserBtn_Click(object sender, EventArgs e)
        {
            TextBox1.Visible = true;            
            List<UserAuthenInfo> offlineuser = AuthenticationController.Instance.GetOfflineUser();
            TextBox1.Text = "Total user in system = " + AuthenticationController.Instance.GetUserCountInSystem() + Environment.NewLine;
            TextBox1.Text += "Total offline user = " + offlineuser.Count + Environment.NewLine;
            int count = 0;
            foreach (UserAuthenInfo uai in offlineuser)
            {
                TextBox1.Text += string.Format("[{0}] [{1}] [{2}]" + Environment.NewLine,
                    uai.User.UserId, uai.LastHeartBeat.ToString("dd/MM/yyyy HH:mm:ss"), uai.IdleCount);
                count++;
                if (count > 100) break;
            }
        }


        protected void StatisticBtn_Click(object sender, EventArgs e)
        {

        }

    }
}
