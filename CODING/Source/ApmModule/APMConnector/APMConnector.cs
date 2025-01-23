using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.APMConnector.Apm;
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

namespace PEA.BPM.APMConnector
{
    public class APMConnector
    {
        private ApmWebService _ws;

        public APMConnector()
        {
            _ws = new ApmWebService();
            //_ws.Url = Path.Combine("http://localhost/APMService/", "ApmWebService.asmx");
            //_ws.Url = Path.Combine("http://172.16.166.131/APMWCFService/", "ApmWebService.asmx"); 
            _ws.Url = Path.Combine("http://cbsbpm00.cbsproj.pea.co.th/APMWCFService/", "ApmWebService.asmx"); 
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
            table.Columns.Add("InvoiceNo", typeof(string));
            table.Columns.Add("Period", typeof(string));
            table.Columns.Add("DueDate", typeof(string));
            table.Columns.Add("Amount", typeof(string));
            table.Columns.Add("Remark", typeof(string));

            ////copy invoices to dataset here
            foreach (SearchInvoiceInfo inv in invoices)
                table.Rows.Add(
                                inv.ReturnType, 
                                inv.CaId, 
                                inv.CaName, 
                                inv.InvoiceNo, 
                                inv.Period, 
                                inv.DueDate, 
                                inv.Amount, 
                                inv.Remark
                                );

            return table;
        }

        public DataTable UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId)
        {
            List<PrintInvoiceInfo> invoices = new List<PrintInvoiceInfo>(_ws.UpdatePaymentStatus(caId, InvoiceNo, TerminalId));

            DataTable table = new DataTable();
            table.TableName = "PRINTINVOICE";
            table.Columns.Add("BranchName", typeof(string));
            table.Columns.Add("BranchAddress", typeof(string));
            table.Columns.Add("CaName", typeof(string));
            table.Columns.Add("CaAddress", typeof(string));
            table.Columns.Add("MeterId", typeof(string));
            table.Columns.Add("RateTypeId", typeof(string));
            table.Columns.Add("CaBranchId", typeof(string));
            table.Columns.Add("CaBranchName", typeof(string));
            table.Columns.Add("CaId", typeof(string));
            table.Columns.Add("Period", typeof(string));
            table.Columns.Add("MeterReadDt", typeof(string));
            table.Columns.Add("LastUnit", typeof(string));
            table.Columns.Add("ReadUnit", typeof(string));
            table.Columns.Add("Qty", typeof(string));
            table.Columns.Add("BaseAmount", typeof(string));
            table.Columns.Add("FTUnitPrice", typeof(string));
            table.Columns.Add("FTAmount", typeof(string));
            table.Columns.Add("Amount", typeof(string));
            table.Columns.Add("TaxCode", typeof(string));
            table.Columns.Add("VatAmount", typeof(string));
            table.Columns.Add("GAmount", typeof(string));
            table.Columns.Add("PaymentDt", typeof(string));
            table.Columns.Add("ControllerId", typeof(string));
            table.Columns.Add("InvoiceNo", typeof(string));
            table.Columns.Add("InvoiceDt", typeof(string));
            table.Columns.Add("BranchId", typeof(string));
            table.Columns.Add("BusinessArea", typeof(string));

            //copy invoices to dataset here
            foreach (PrintInvoiceInfo inv in invoices)
                table.Rows.Add(
                        inv.BranchName, 
                        inv.BranchAddress, 
                        inv.CaName, 
                        inv.CaAddress, 
                        inv.MeterId, 
                        inv.RateTypeId, 
                        inv.CaBranchId, 
                        inv.CaBranchName, 
                        inv.CaId, 
                        inv.Period, 
                        inv.MeterReadDt, 
                        inv.LastUnit, 
                        inv.ReadUnit, 
                        inv.Qty, 
                        inv.BaseAmount, 
                        inv.FTUnitPrice, 
                        inv.FTAmount, 
                        inv.Amount, 
                        inv.TaxCode, 
                        inv.VatAmount, 
                        inv.GAmount, 
                        inv.PaymentDt, 
                        inv.ControllerId, 
                        inv.InvoiceNo, 
                        inv.InvoiceDt, 
                        inv.BranchId, 
                        inv.BusinessArea);

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
