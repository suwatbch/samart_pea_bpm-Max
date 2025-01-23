using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPMInternalServicePools.SG;
using System.Dynamic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace TestCallApi
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestTAMSRESTApi();          //WORK 100%
            TestWebBillingService();
        }
        
        static void TestWebBillingService()
        {
          var ser = new WebBillingService.WebBillingServices();
          var result = ser.GetElectricBillingByCaid("020015814480");

          string x = "X";

            
        }

        static void TestTAMSRESTApi()
        {
            var TAMS_SG = new TAMSServiceGateway();
            string url = @"https://tams.pea.co.th/restapi/rest-auth/login/";
            string username = "bpmsystem";
            string password = "BPM@system1!";
            string certFile = @"D:\TAMSCert.cer";

            dynamic body = new ExpandoObject();
            body.username = username;
            body.password = password;

            string json = Regex.Unescape(JsonConvert.SerializeObject(body, Newtonsoft.Json.Formatting.Indented));
            var temp = TAMS_SG.RequestLogin(url, args: json, username: username, password: password, certFile: certFile);
        }
    }
}
