using System;
using System.Data;
using System.Configuration;
using System.ServiceModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using System.Security;
using System.Threading;
using System.Web.Services.Protocols;

namespace BPMReportService
{
   /// <summary>
   /// Summary description for WebServiceSecurity
   /// </summary>
   public class WebServiceSecurity
    {
        private static WebServiceSecurity _instant = new WebServiceSecurity();
        private Dictionary<string, AuthorizedData> _data;
        private SecurityBS _bs;

        private WebServiceSecurity()
        {
            _data = new Dictionary<string, AuthorizedData>();
            _bs = new SecurityBS();
        }

        public static void CheckAuthorization(AuthInfo authInfo)
        {
            if (authInfo == null) throw new Exception("Cannot check for Authorization because AuthInfo is null.");
            _instant.CheckToken(authInfo.UserId, authInfo.AuthToken);
                        
            IIdentity identity = new GenericIdentity("WS-" + authInfo.UserId);
            GenericPrincipal principal = new GenericPrincipal(identity, new string[] { });
            Thread.CurrentPrincipal = principal;
 
            //StackTrace stackTrace = new StackTrace();
            //StackFrame stackFrame = stackTrace.GetFrame(1);
            //MethodBase methodBase = stackFrame.GetMethod();
            //string operationName = methodBase.Name.Replace("_Compress","");
            //string serviceName = methodBase.ReflectedType.Name.Replace("WebService", "Service");

            //if (!IsAuthorized(Session.User.Id, string.Format("{0}.{1}", serviceName, operationName)))
            //{
            //    throw new SecurityException("You don't have right to access this service");
            //}
        }

        public void CheckToken(string userId, string token)
        {
            string result = _bs.CheckToken(userId, token);

            switch (result)
            {
                case "NotFoundUser":
                case "TokenNotMatch":
                case "TokenExpired":
                    throw new SecurityException(result);
                default:
                    break;
            }
        }

        public static bool IsAuthorized(string userId, string serviceName)
        {
            AuthorizedData azData = _instant.LoadAuthorizedData(userId);

            Service service= azData.Data.Find(delegate(Service s) { return s.Name == serviceName; });

            if (service != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private AuthorizedData LoadAuthorizedData(string userId)
        {
            AuthorizedData azData;
            if (!_data.ContainsKey(userId))
            {
                azData = GetAuthorizedData(userId);
            }
            else
            {
                azData = _data[userId];
                TimeSpan tsp = DateTime.Now.Subtract(azData.LoadTime);
                if (tsp.TotalMinutes > 1)
                {
                    _data.Remove(userId);
                    azData = GetAuthorizedData(userId);
                }
            }
            return azData;
        }


        private AuthorizedData GetAuthorizedData(string userId)
        {
            List<Service> data = _bs.ListAuthorizedServices(userId);
            AuthorizedData azData = new AuthorizedData(DateTime.Now, data);
            _data.Add(userId, azData);

            return azData;
        }

        private class AuthorizedData
        {
            DateTime _loadTime;

            public DateTime LoadTime
            {
                get { return _loadTime; }
                set { _loadTime = value; }
            }
            List<Service> _data;

            public List<Service> Data
            {
                get { return _data; }
                set { _data = value; }
            }

            public AuthorizedData(DateTime loadTime, List<Service> data)
            {
                _loadTime = loadTime;
                _data = data;
            }
        }
    }
}
