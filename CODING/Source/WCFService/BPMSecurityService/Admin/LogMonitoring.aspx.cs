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
    public partial class LogMonitoring : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void NormalRefreshBtn_Click(object sender, EventArgs e)
        {
            TextBox1.Text = ServiceLog.Instance.GetTodayEvent(LogType.Normal);
        }

        protected void SystemLog_Click(object sender, EventArgs e)
        {
            TextBox1.Text = ServiceLog.Instance.GetTodayEvent(LogType.System);
        }

        protected void ErrorLog_Click(object sender, EventArgs e)
        {
            TextBox1.Text = ServiceLog.Instance.GetTodayEvent(LogType.Error);
        }

        protected void SysErrorLog_Click(object sender, EventArgs e)
        {
            string res = "";
            List<string> errlist = new List<string>();
            errlist = ServiceLog.Instance.GetInternalError();
            foreach (string str in errlist)
            {
                res += str + Environment.NewLine;
            }
            TextBox1.Text = res;
        }

    }
}
