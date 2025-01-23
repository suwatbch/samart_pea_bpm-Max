using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.BPMConnector.PosWs;
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

namespace PEA.BPM.BPMConnector
{
    public class BPMConnector
    {
        private POSWebService _ws;

        public BPMConnector(string serviceUrl)
        {
            _ws = new POSWebService();
            _ws.Url = Path.Combine(serviceUrl, "POSWebService.asmx");      
        }

        public string Login(string userId, string password)
        {
            //string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
            string pwd = "0987654321";
            Credential credential = _ws.Login(userId, pwd);

            if (credential.Status == "VALID")
            {
                AuthInfo authInfo = new AuthInfo();
                authInfo.UserId = userId;
                authInfo.AuthToken = credential.Token;
                _ws.AuthInfoValue = authInfo;
            }

            return credential.Status;
        }

        public DataTable SearchInvoiceByCustomerId(string contractAccount)
        {
            List<InvoiceInfo> invoices = new List<InvoiceInfo>(_ws.SearchInvoiceByContractAccount(contractAccount));

            DataTable table = new DataTable();
            table.TableName = "INVOICE";
            table.Columns.Add("CaId", typeof(string));
            table.Columns.Add("CaName", typeof(string));
            table.Columns.Add("CaAddress", typeof(string));
            table.Columns.Add("DtName", typeof(string));
            table.Columns.Add("Period", typeof(string));
            table.Columns.Add("DueDate", typeof(string));
            table.Columns.Add("ToPayTotalAmount", typeof(decimal));

            //copy invoices to dataset here
            foreach (InvoiceInfo inv in invoices)
                table.Rows.Add(inv.ContractAccountId, inv.ContractAccountName, inv.ContractAccountAddress, inv.DebtTypeName, inv.InvoicePeriod, inv.PaymentDueDate, inv.ToPayTotalAmount);

            return table;
        }

        public static void ExportToFile(List<string> contractAccounts, int queueNo, string outputDirectory)
        {
            DataTable caTable = new DataTable();
            caTable.TableName = "CAINFO";
            caTable.Columns.Add("CaId", typeof(string));

            foreach (string ca in contractAccounts)
            {
                DataRow dr = caTable.NewRow();
                dr["CaId"] = ca;
                caTable.Rows.Add(dr);
            }

            //write to target
            string xmlFileSuffix = DateTime.Now.ToString("yyyyMMddHHmmss", new CultureInfo("th-TH"));
            string xmlFileName = string.Format("Q{0}{1}.xml", queueNo.ToString().PadLeft(4, '0'), xmlFileSuffix);
            string fullSavePath = Path.Combine(outputDirectory, xmlFileName);
            //allow only one file at a moment
            foreach (string file in Directory.GetFiles(outputDirectory, "Q*.xml", SearchOption.TopDirectoryOnly))
                File.Delete(file);

            caTable.WriteXml(fullSavePath, XmlWriteMode.WriteSchema);
        }
    }
}
