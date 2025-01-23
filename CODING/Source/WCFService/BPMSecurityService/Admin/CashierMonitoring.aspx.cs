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
using PEA.BPM.WebService.Security.Cashier;
using System.Collections.Generic;

namespace PEA.BPM.WebService.Security.Admin
{
    public partial class CashierMonitoring : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CashierBtn_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            List<string> list = CashierCachingController.Instance.GetCashierIdList();

            TextBox1.Text += "Total cashier = " + list.Count + Environment.NewLine;
            foreach (string str in list)
            {
                TextBox1.Text += str + Environment.NewLine;
            }
        }

        protected void WatchBtn_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void RefreshCacheBtn_Click(object sender, EventArgs e)
        {
            string cashierid = RefreshCashierIDTxt.Text;
            if (cashierid.Trim().Length == 0) return;
            CashierCachingController.Instance.GetCachingCashierWorkStatus(cashierid, true);
            RefreshCashierIDTxt.Text = "";
        }
    }
}
