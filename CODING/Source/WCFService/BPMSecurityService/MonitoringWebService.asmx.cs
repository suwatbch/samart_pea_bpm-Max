using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using PEA.BPM.WebService.Security.Core;

namespace PEA.BPM.WebService.Security
{
    /// <summary>
    /// Summary description for MonitoringWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class MonitoringWebService : System.Web.Services.WebService
    {
        ////-- เอาไว้ debug
        //[WebMethod]
        //public void ModifyStringValue(string userid, string columnname, string value)
        //{
        //    AuthenticationController.Instance.ModifyStringValue(userid, columnname, value);            
        //}

        //[WebMethod]
        //public void ModifyDateTimeValue(string userid, string columnname, DateTime value, bool isnull)
        //{
        //    AuthenticationController.Instance.ModifyDateTimeValue(userid, columnname, value, isnull);
        //}

        [WebMethod]
        public SystemInfo GetVersion()
        {
            return SystemInfo.Instance;
        }

    }
}
