using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AdminToolService.UnclarifyPayment
{
    public class UnclarifyPaymentBS
    {
        public DataTable GetUnclarifyPayment(DateTime importDate, string errorType)
        {
            try
            {
                UnclarifyPaymentDA da = new UnclarifyPaymentDA();
                DataTable dt = da.SelectUnclarifyPayment(importDate, errorType);
                return dt;
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

    }
}
