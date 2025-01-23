using System;
using System.Collections.Generic;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Xml;
using System.Reflection;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using System.Net;
using System.Globalization;
using System.Diagnostics;
using System.IO;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public enum EErrorInModule
    {
        Cash = 10, POS = 20, POSจ่ายเงิน = 30, Agency = 40, BLAN = 50,
        EPayment = 60, Tools = 70, Other = 80, Security = 81, Unhandler = 82, Architecture = 83, Unknow = 99
    }
    public enum EErrorInLayer
    {
        Database = 1, InternalNetwork = 2, WebApplication = 3,
        Internet = 4, ServiceGateway = 5, Client = 6, Unknow = 9
    }

    public class ExceptionHelper
    {
        #region create exception for testing
        public static SqlException CreateSqlException(string errorMessage, int errorNumber)
        {
            SqlErrorCollection collection = GetErrorCollection();
            SqlError error = GetError(errorNumber, errorMessage);
            MethodInfo addMethod = collection.GetType().
            GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance);
            addMethod.Invoke(collection, new object[] { error });
            Type[] types = new Type[] { typeof(string), typeof(SqlErrorCollection) };
            object[] parameters = new object[] { errorMessage, collection };
            ConstructorInfo constructor = typeof(SqlException).
            GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
            SqlException exception = (SqlException)constructor.Invoke(parameters);
            return exception;
        }
        private static SqlError GetError(int errorCode, string message)
        {
            object[] parameters = new object[] {
            errorCode, (byte)0, (byte)10, "server", message, "procedure", 0 };
            Type[] types = new Type[] {
            typeof(int), typeof(byte), typeof(byte), typeof(string), typeof(string),
            typeof(string), typeof(int) };
            ConstructorInfo constructor = typeof(SqlError).
             GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
            SqlError error = (SqlError)constructor.Invoke(parameters);
            return error;
        }
        private static SqlErrorCollection GetErrorCollection()
        {
            ConstructorInfo constructor = typeof(SqlErrorCollection).
            GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);
            SqlErrorCollection collection = (SqlErrorCollection)constructor.Invoke(new object[] { });
            return collection;
        }
        #endregion

        public static void GenerateDummyException(EErrorInModule module)
        {
            //StackTrace st = new StackTrace(true);
            //StackFrame sf = st.GetFrame(1); // frame 0 เป็นของตัวเอง ไม่เอา
            //string msg = "Dummy exception " + Path.GetFileName(sf.GetFileName().ToString()) + " Line [" +
            //    sf.GetFileLineNumber() + "] " + sf.GetMethod().ToString();

            string msg = "Token not match.";
            throw new Exception(msg);
        }
        public static void GenerateSeverDummyException(EErrorInModule module)
        {
            StackTrace st = new StackTrace(true);
            StackFrame sf = st.GetFrame(1); // frame 0 เป็นของตัวเอง ไม่เอา
            string stackstr = "Dummy server exception " + Path.GetFileName(sf.GetFileName().ToString()) + " Line [" +
                sf.GetFileLineNumber() + "] " + sf.GetMethod().ToString();
            throw new Exception(stackstr);
        }

        public static string GetCurrentStackTrace(int startat)
        {
            StringBuilder builder = new StringBuilder(0xff);            
            StackTrace st = new StackTrace(true);
            StackFrame[] sfarr = st.GetFrames();
            for (int i = startat; i < sfarr.Length; i++)
            {
                StackFrame sf = sfarr[i];
                string filename = null;
                try { filename = sf.GetFileName(); } catch (SecurityException) { }
                if (filename == null) continue; // ไม่มี filename ไม่สนใจ
                
                MethodBase method = sf.GetMethod();
                if (method != null)
                {
                    builder.Append("   at ");
                    Type declaringtype = method.DeclaringType;
                    if (declaringtype != null)
                    {
                        builder.Append(declaringtype.FullName.Replace('+', '.'));
                        builder.Append(".");
                    }
                    builder.Append(method.Name);

                    if ((method is MethodInfo) && method.IsGenericMethod)
                    {
                        Type[] genericArguments = method.GetGenericArguments();
                        builder.Append("[");
                        int index = 0;
                        bool flag2 = true;
                        while (index < genericArguments.Length)
                        {
                            if (!flag2) builder.Append(","); else flag2 = false;
                            builder.Append(genericArguments[index].Name);
                            index++;
                        }
                        builder.Append("]");
                    }

                    //-- add parameter
                    builder.Append("(");
                    ParameterInfo[] parameters = method.GetParameters();
                    bool flag3 = true;
                    for (int j = 0; j < parameters.Length; j++)
                    {
                        if (!flag3) builder.Append(", "); else flag3 = false;
                        string name = "<UnknownType>";
                        if (parameters[j].ParameterType != null) name = parameters[j].ParameterType.Name;
                        builder.Append(name + " " + parameters[j].Name);
                    }
                    builder.Append(")");

                    //-- add file and line number
                    if (sf.GetILOffset() != -1)
                    {
                        builder.Append(' ');
                        builder.AppendFormat("in {0}:line {1}", filename, sf.GetFileLineNumber());
                    }

                }
                builder.Append(Environment.NewLine);
            } // end for
            

            return builder.ToString();
        }

        public static EErrorInLayer GetClientExceptionLayer(Exception ee)
        {
            Type type = ee.GetType();
            if (type == typeof(BPMApplicationException)) return ((BPMApplicationException)ee).Layer;
            if (type == typeof(WebException)) return EErrorInLayer.ServiceGateway;
            return EErrorInLayer.Client;
        }
        public static EErrorInLayer GetServerExceptionLayer(Exception ee)
        {
            Type type = ee.GetType();
            if (type == typeof(BPMApplicationException)) return ((BPMApplicationException)ee).Layer;
            if (type == typeof(SqlException)) return EErrorInLayer.Database;
            return EErrorInLayer.WebApplication;
        }

        #region Create WCF FaultExcpetion
        public static FaultException<BPMApplicationExceptionInfo> CreateTypedFaultException(BPMApplicationExceptionInfo info)
        {            
            FaultException<BPMApplicationExceptionInfo> bpmex = new FaultException<BPMApplicationExceptionInfo>(info,
                new FaultReason(info.Message));
            return bpmex;
        }
        #endregion

        #region Create soap exception
        private static void AddErrorNodeDetail(XmlDocument xmldoc, XmlNode parentnode, string elementname, string data)
        {
            XmlNode detailnode = xmldoc.CreateNode(XmlNodeType.Element, elementname, "");
            detailnode.InnerText = data;
            parentnode.AppendChild(detailnode);
        }
        public static SoapException CreateSoapException(BPMApplicationExceptionInfo info)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode rootnode = xmldoc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);

            //-- สร้าง node เก็บรายละเอียด error
            XmlNode errornode = xmldoc.CreateNode(XmlNodeType.Element, "Error", "");

            AddErrorNodeDetail(xmldoc, errornode, "ErrorCode", info.ErrorCode);
            AddErrorNodeDetail(xmldoc, errornode, "OriginalType", info.OriginalType);
            AddErrorNodeDetail(xmldoc, errornode, "OccurWhen", info.OccurWhen.ToString("dd/MM/yyyy HH:mm:ss"));
            AddErrorNodeDetail(xmldoc, errornode, "Module", ((int)info.Module).ToString());
            AddErrorNodeDetail(xmldoc, errornode, "Layer", ((int)info.Layer).ToString());
            AddErrorNodeDetail(xmldoc, errornode, "DebuggingId", info.DebuggingId);
            AddErrorNodeDetail(xmldoc, errornode, "Message", info.Message);
            AddErrorNodeDetail(xmldoc, errornode, "ServerStackTrace", info.StackTrace);
            AddErrorNodeDetail(xmldoc, errornode, "Source", info.Source);
            AddErrorNodeDetail(xmldoc, errornode, "CanContinue", info.CanContinue.ToString());
            AddErrorNodeDetail(xmldoc, errornode, "THMessage", info.THMessage);
            AddErrorNodeDetail(xmldoc, errornode, "Cause", info.Cause);
            AddErrorNodeDetail(xmldoc, errornode, "Resolve", info.Resolve);
            AddErrorNodeDetail(xmldoc, errornode, "HelpURL", info.HelpURL);

            rootnode.AppendChild(errornode);

            SoapException soapee = new SoapException(info.Message, SoapException.ServerFaultCode, "", rootnode);
            return soapee;
        }

        private static BPMApplicationExceptionInfo GetInfoFromSoapException(SoapException soapex)
        {
            BPMApplicationExceptionInfo info = new BPMApplicationExceptionInfo();
            info.Module = (int)EErrorInModule.Unknow;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(soapex.Detail.OuterXml);

                XmlNode categorynode = doc.DocumentElement.SelectSingleNode("Error");

                info.ErrorCode = categorynode.SelectSingleNode("ErrorCode").InnerText;
                info.OriginalType = categorynode.SelectSingleNode("OriginalType").InnerText;
                info.OccurWhen = DateTime.ParseExact(categorynode.SelectSingleNode("OccurWhen").InnerText, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                info.Module = Convert.ToInt32(categorynode.SelectSingleNode("Module").InnerText);
                info.Layer = Convert.ToInt32(categorynode.SelectSingleNode("Layer").InnerText);
                info.DebuggingId = categorynode.SelectSingleNode("DebuggingId").InnerText;
                info.Message = categorynode.SelectSingleNode("Message").InnerText.Replace("\n", Environment.NewLine);
                info.StackTrace = categorynode.SelectSingleNode("ServerStackTrace").InnerText.Replace("\n", Environment.NewLine);
                info.Source = categorynode.SelectSingleNode("Source").InnerText;
                string tcancontinue = categorynode.SelectSingleNode("CanContinue").InnerText.ToLower();
                info.CanContinue = tcancontinue == "false" ? false : true;
                info.THMessage = categorynode.SelectSingleNode("THMessage").InnerText.Replace("\n", Environment.NewLine);
                info.Cause = categorynode.SelectSingleNode("Cause").InnerText.Replace("\n", Environment.NewLine);
                info.Resolve = categorynode.SelectSingleNode("Resolve").InnerText.Replace("\n", Environment.NewLine);
                info.HelpURL = categorynode.SelectSingleNode("HelpURL").InnerText;
            }
            catch
            {
                info.Layer = (int)EErrorInLayer.WebApplication;

                string msg = soapex.Message.Replace("\n", Environment.NewLine);
                int newlineidx = msg.IndexOf(Environment.NewLine);
                info.Message = msg.Substring(0, newlineidx);
                newlineidx += Environment.NewLine.Length; // เลื่อนข้าม newline ไป
                info.StackTrace = msg.Substring(newlineidx, msg.Length - newlineidx);
            }
            return info;
        }
        #endregion

        public static void PreserveStackTrace(Exception exception)
        {
            MethodInfo preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace",
              BindingFlags.Instance | BindingFlags.NonPublic);
            preserveStackTrace.Invoke(exception, null);
        }

        public static Exception ServiceGatewayConvertException(EErrorInModule module, Exception ee)
        {
            Type type = ee.GetType();
            if (type == typeof(SoapException)) // ถ้าเป็น SoapException ให้เปลี่ยนเป็น BPMApplicationException
            {
                SoapException soapex = ee as SoapException;
                BPMApplicationExceptionInfo info = GetInfoFromSoapException(soapex);
                return new BPMApplicationException(
                    info.ErrorCode,
                    info.OriginalType,
                    info.OccurWhen,
                    (EErrorInModule)info.Module,
                    (EErrorInLayer)info.Layer,
                    info.DebuggingId,
                    info.StackTrace,
                    info.Message,
                    info.Source,
                    info.CanContinue,
                    info.THMessage,
                    info.Cause,
                    info.Resolve,
                    info.HelpURL
                    );
            }
            else if (type == typeof(FaultException<BPMApplicationExceptionInfo>))
            {
                FaultException<BPMApplicationExceptionInfo> faultex = ee as FaultException<BPMApplicationExceptionInfo>;
                BPMApplicationExceptionInfo info = faultex.Detail;            
                return new BPMApplicationException(
                    info.ErrorCode,
                    info.OriginalType,
                    info.OccurWhen,
                    (EErrorInModule)info.Module,
                    (EErrorInLayer)info.Layer,
                    info.DebuggingId,
                    info.StackTrace,
                    info.Message,
                    info.Source,
                    info.CanContinue,
                    info.THMessage,
                    info.Cause,
                    info.Resolve,
                    info.HelpURL
                    );
            }

 //catch (TimeoutException timeProblem)
 //   {
 //   }
 //   catch (FaultException<GreetingFault> greetingFault)
 //   {
 //   }
 //   catch (FaultException unknownFault)
 //   {
 //   }
 //   catch (CommunicationException commProblem)
 //   {
 //   }
            return ee;
        }

        public static BPMApplicationException ConvertToBPMApplicationException(EErrorInModule module, EErrorInLayer layer, DateTime occurwhen, Exception ee)
        {
            return new BPMApplicationException(module, layer, occurwhen, ee);
        }
    }
}