using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using PEA.BPM.APM2Connector.Apm;

namespace PEA.BPM.APM2Connector.Test
{
    public partial class Form1 : Form
    {
        APM2Connector connector;
        DataTable invoices;

        public Form1()
        {
            InitializeComponent();
            connector = new APM2Connector();
        }

        private void consumeBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (invoices == null)
                    //invoices = connector.SearchInvoiceByCaId(textBox1.Text, textBox2.Text);
                    invoices = connector.SearchInvoiceByCaId(textBox1.Text, textBox2.Text);
                else
                    invoices.Merge(connector.SearchInvoiceByCaId(textBox1.Text, textBox2.Text));

                List<CAInfo> caList = new List<CAInfo>();
                foreach (DataRow dr in invoices.Rows)
                {
                    CAInfo ca = new CAInfo();
                    ca.ReturnType = dr["ReturnType"].ToString();
                    ca.CaId = dr["CaId"].ToString();
                    ca.CaName = dr["CaName"].ToString();
                    ca.PmId = dr["PmId"].ToString();
                    ca.RateTypeId = dr["RateTypeId"].ToString();
                    ca.DtId = dr["DtId"].ToString();
                    ca.DtName = dr["DtName"].ToString();
                    ca.InvoiceNo = dr["InvoiceNo"].ToString();
                    ca.Period = dr["Period"].ToString();
                    ca.DueDate = dr["DueDate"].ToString();
                    ca.Amount = dr["Amount"].ToString();
                    ca.GAmount = dr["GAmount"].ToString();
                    ca.Remark = dr["Remark"].ToString();
                    caList.Add(ca);
                }

                dataGridView1.DataSource = caList;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("NoToken") > -1)
                {
                    MessageBox.Show("ยังไม่ได้ล๊อกอินเข้าใช้งานระบบ");
                }
                else if (ex.Message.IndexOf("TokenNotMatch") > -1)
                {
                    MessageBox.Show("บังคับให้ออกจากระบบเนื่องจากมีการใช้แอ๊คเค้าเดียวกันมากกว่าหนึ่งจุด");
                }
                else if (ex.Message.IndexOf("TokenExpired") > -1)
                {
                    MessageBox.Show("ระบบบังคับให้ออกจากระบบเพื่อทำการล๊อกอินใหม่อีกครั้ง");
                }
                else 
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int queueNo = 553;
            //    List<string> contractAccountList = new List<string>();

            //    foreach (DataRow dr in invoices.Rows)
            //        contractAccountList.Add((string)dr["CaId"]);

            //    APMConnector.ExportToFile(contractAccountList, queueNo, @"c:\services\");
            //    MessageBox.Show("Done.");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void loginBt_Click(object sender, EventArgs e)
        {
            string status = connector.Login(userId.Text, passwordTxt.Text);

            if (status == "VALID")
                MessageBox.Show("Welcome");
            else
                MessageBox.Show(status);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //if (invoices == null)
                //invoices = connector.SearchInvoiceByCaId(textBox1.Text, textBox2.Text);
                    invoices = connector.UpdatePaymentStatus(textBox1.Text, txtInvoiceNo.Text, textBox2.Text);
                //else
                //    invoices.Merge(connector.UpdatePaymentStatus(textBox1.Text, textBox3.Text, textBox2.Text));

                List<PrintInfo> caList = new List<PrintInfo>();
                foreach (DataRow dr in invoices.Rows)
                {
                    PrintInfo ca = new PrintInfo();
                    ca.branchName = dr["branchName"].ToString();
                    ca.branchAddress = dr["branchAddress"].ToString();
                    ca.caName = dr["caName"].ToString();
                    ca.caAddress = dr["caAddress"].ToString();
                    ca.meterId = dr["meterId"].ToString();
                    ca.rateTypeId = dr["rateTypeId"].ToString();
                    ca.caBranchId = dr["caBranchId"].ToString();
                    ca.caBranchName = dr["caBranchName"].ToString();
                    ca.caId = dr["caId"].ToString();
                    ca.period = dr["period"].ToString();
                    ca.meterReadDt = dr["meterReadDt"].ToString();
                    ca.lastUnit = dr["lastUnit"].ToString();
                    ca.readUnit = dr["readUnit"].ToString();
                    ca.qty = dr["qty"].ToString();
                    ca.baseAmount = dr["baseAmount"].ToString();
                    ca.fTUnitPrice = dr["fTUnitPrice"].ToString();
                    ca.fTAmount = dr["fTAmount"].ToString();
                    ca.amount = dr["amount"].ToString();
                    ca.taxCode = dr["taxCode"].ToString();
                    ca.vatAmount = dr["vatAmount"].ToString();
                    ca.gAmount = dr["gAmount"].ToString();
                    ca.paymentDt = dr["paymentDt"].ToString();
                    ca.controllerId = dr["controllerId"].ToString();
                    ca.invoiceNo = dr["invoiceNo"].ToString();
                    ca.invoiceDt = dr["invoiceDt"].ToString();
                    ca.branchId = dr["branchId"].ToString();
                    ca.businessArea = dr["businessArea"].ToString();
                    ca.dtId = dr["dtId"].ToString();
                    ca.dtName = dr["dtName"].ToString();
                    //ca.caDoc = dr["CaDoc"].ToString();
                    caList.Add(ca);
                }

                dataGridView2.DataSource = caList;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("NoToken") > -1)
                {
                    MessageBox.Show("ยังไม่ได้ล๊อกอินเข้าใช้งานระบบ");
                }
                else if (ex.Message.IndexOf("TokenNotMatch") > -1)
                {
                    MessageBox.Show("บังคับให้ออกจากระบบเนื่องจากมีการใช้แอ๊คเค้าเดียวกันมากกว่าหนึ่งจุด");
                }
                else if (ex.Message.IndexOf("TokenExpired") > -1)
                {
                    MessageBox.Show("ระบบบังคับให้ออกจากระบบเพื่อทำการล๊อกอินใหม่อีกครั้ง");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            CurrentIP.Text = LocalIPAddress();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();//create new datatable
            dataGridView1.DataSource = dataTable;//clears out the datagrid with empty datatable
        }

    }
}
