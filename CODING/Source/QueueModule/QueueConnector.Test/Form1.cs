using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security;
using PEA.BPM.QueueConnector.Queue;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace PEA.BPM.QueueConnector.Test
{
    public partial class Form1 : Form
    {
        QueueConnector connector;
        DataTable invoices;

        public Form1()
        {
            InitializeComponent();
            connector = new QueueConnector();
        }

        private void consumeBt_Click(object sender, EventArgs e)
        {
            try
            {
                string chkCa = "0";
                string tmpCa = "";
                
                if (invoices == null)
                    //invoices = connector.SearchInvoiceByCaId(textBox1.Text, textBox2.Text);
                    invoices = connector.SearchInvoiceByCaId(textBox1.Text, textBox2.Text);
                else
                    invoices.Merge(connector.SearchInvoiceByCaId(textBox1.Text, textBox2.Text));

                TextWriter tw = new StreamWriter("C:\\BPM\\BPMClient\\QueueData\\CAList.txt");

                List<CAInfo> caList = new List<CAInfo>();
                foreach (DataRow dr in invoices.Rows)
                {
                    CAInfo ca = new CAInfo();
                    chkCa = dr["ReturnType"].ToString();

                    ca.ReturnType = dr["ReturnType"].ToString();
                    ca.CaId = dr["CaId"].ToString();
                    ca.CaName = dr["CaName"].ToString();
                    ca.CaAddress = dr["CaAddress"].ToString();
                    ca.InvoiceNo = dr["InvoiceNo"].ToString();
                    ca.Period = dr["Period"].ToString();
                    ca.DueDate = dr["DueDate"].ToString();
                    ca.DebtType = dr["DebtType"].ToString();
                    ca.Amount = dr["Amount"].ToString();
                    ca.Remark = dr["Remark"].ToString();
                    caList.Add(ca);

                    if (chkCa == "1")
                    {
                        //Write Queue File
                        if (tmpCa != ca.CaId)
                        {
                            tw.WriteLine(ca.CaId);
                        }
                    }
                    tmpCa = dr["CaId"].ToString();
                }

                dataGridView1.DataSource = caList;
                tw.Close();
                

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
            if(File.Exists("C:\\BPM\\BPMClient\\QueueData\\CAList.txt"))
            {
                TextWriter tw = new StreamWriter("C:\\BPM\\BPMClient\\QueueData\\CAList.ok");
                tw.WriteLine(textBox1.Text);
                tw.Close();
                dataGridView1.DataSource = null;
                invoices.Clear();
            }
            
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

    }
}
