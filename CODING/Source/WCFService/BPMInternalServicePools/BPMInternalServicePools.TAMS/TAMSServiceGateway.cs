using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using RestSharp;
using System.Security.Cryptography.X509Certificates;
using RestSharp.Deserializers;
using System.IO;
using System.Xml.Serialization;

namespace BPMInternalServicePools.SG

{
    public class TAMSServiceGateway
    {
        public string RequestLogin(string url,string args =null,string username=null,string password=null,string certFile=null)
        {
            Method httpMethod = Method.POST;
            try
            {
                #region ..Begin Try
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                    var client = new RestClient(url);
                    if (!string.IsNullOrEmpty(certFile))
                    {
                        X509Certificate2 certificates = new X509Certificate2();
                        certificates.Import(certFile);
                        client.ClientCertificates = new X509CertificateCollection() { certificates };
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        var request = new RestRequest(httpMethod);
                        if (!string.IsNullOrEmpty(username))
                            request.AddHeader("username", username);
                        if (!string.IsNullOrEmpty(password))
                            request.AddHeader("password", password);

                        request.AddHeader("Accept", "application/json");
                        request.AddHeader("content-type", "application/json;charset=UTF-8");
                        request.AddHeader("Cache-Control", "no-cache");
                        if (!string.IsNullOrEmpty(args))
                            request.AddParameter("application/json;charset=UTF-8", args, ParameterType.RequestBody);

                        request.XmlSerializer = new RestSharp.Serializers.DotNetXmlSerializer();
                        request.RequestFormat = DataFormat.Xml;
                        var response = client.Execute(request);

                        string x = "A";
                    }
                #endregion
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return "";
        }
    }
}
