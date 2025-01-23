using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace AdminToolService.UnclarifyPayment
{
    /// <summary>
    /// Summary description for UnclarifyPayment
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class UnclarifyPaymentService : System.Web.Services.WebService
    {
        private UnclarifyPaymentBS _unclarifyPaymentBS;

        public UnclarifyPaymentService()
        {
            _unclarifyPaymentBS = new UnclarifyPaymentBS();
        }

        [WebMethod]
        public DataTable GetUnclarifyPayment(DateTime importDate, string errorType)
        {
            return _unclarifyPaymentBS.GetUnclarifyPayment(importDate, errorType);
        }

    }
}
