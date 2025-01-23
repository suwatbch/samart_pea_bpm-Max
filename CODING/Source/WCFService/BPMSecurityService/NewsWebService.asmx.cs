using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using PEA.BPM.WebService.Security.News;

namespace PEA.BPM.WebService.Security
{
    /// <summary>
    /// Summary description for NewsWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class NewsWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public DataTable GetBroadcastListBySentDate(DateTime _sentDate)
        {
            return NewsController.Instance.GetBroadcastListBySentDate(_sentDate);
        }
        [WebMethod]
        public DataTable GetBroadcastListBySentDateWithForceUpdate(DateTime _sentDate, bool forceupdate)
        {
            return NewsController.Instance.GetBroadcastListBySentDate(_sentDate, forceupdate);
        }
    }
}
