using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.QueueConnector.Queue;
using System.ServiceModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Reflection;
using System.Collections;
using System.Web.Security;
using System.Configuration;

namespace PEA.BPM.QueueConnector
{
    public class QueueConnector
    {
        private QueueWebService _ws;

        public QueueConnector()
        {
            _ws = new QueueWebService();
            //_ws.Url = Path.Combine("http://localhost/QueueWCFService/", "QueueWebService.asmx");
            //_ws.Url = Path.Combine("http://172.16.166.131/QueueWCFService/", "QueueWebService.asmx"); 
            _ws.Url = Path.Combine("http://bpm.cbs2proj.pea.co.th/QueueWCFService/", "QueueWebService.asmx");
        }

        public string Login(string userId, string password)
        {
            //string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
            string localIP = LocalIPAddress();
            string pwd = password;
            Credential credential = _ws.Login(userId, pwd, localIP);

            if (credential.Status == "VALID")
            {
                AuthInfo authInfo = new AuthInfo();
                authInfo.UserId = userId;
                authInfo.AuthToken = credential.Token;
                _ws.AuthInfoValue = authInfo;
            }

            return credential.Status;
        }

        public DataTable SearchInvoiceByCaId(string caId,string TerminalId)
        {
            List<SearchInvoiceInfo> invoices = new List<SearchInvoiceInfo>(_ws.SearchInvoiceByCaId(caId,TerminalId));

            DataTable table = new DataTable();
            table.TableName = "INVOICE";
            table.Columns.Add("ReturnType", typeof(string));
            table.Columns.Add("CaId", typeof(string));
            table.Columns.Add("CaName", typeof(string));
            table.Columns.Add("CaAddress", typeof(string));
            table.Columns.Add("InvoiceNo", typeof(string));
            table.Columns.Add("Period", typeof(string));
            table.Columns.Add("DueDate", typeof(string));
            table.Columns.Add("DebtType", typeof(string));
            table.Columns.Add("Amount", typeof(string));
            table.Columns.Add("Remark", typeof(string));

            ////copy invoices to dataset here
            foreach (SearchInvoiceInfo inv in invoices)
                table.Rows.Add(
                                inv.ReturnType, 
                                inv.CaId, 
                                inv.CaName, 
                                inv.CaAddress,
                                inv.InvoiceNo, 
                                inv.Period, 
                                inv.DueDate, 
                                inv.DebtType,
                                inv.Amount, 
                                inv.Remark
                                );

            return table;
        }

        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
    }
}
