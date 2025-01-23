using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.BPMConnector.PosWs;
using System.Security;
using System.Configuration;

namespace PEA.BPM.BPMConnector.Test
{
    public partial class Form1 : Form
    {
        BPMConnector connector;
        DataTable invoices;

        public Form1()
        {
            InitializeComponent();
            connector = new BPMConnector(ConfigurationManager.AppSettings["BPM_GATEWAY"]);
        }

        private void consumeBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (invoices == null)
                    invoices = connector.SearchInvoiceByCustomerId(textBox1.Text);
                else
                    invoices.Merge(connector.SearchInvoiceByCustomerId(textBox1.Text));

                List<CAInfo> caList = new List<CAInfo>();
                foreach (DataRow dr in invoices.Rows)
                {
                    CAInfo ca = new CAInfo();
                    ca.CaId = dr["CaId"].ToString();
                    ca.CaName = dr["CaName"].ToString();
                    ca.CaAddress = dr["CaAddress"].ToString();
                    ca.DtName = dr["DtName"].ToString();
                    ca.Period = dr["Period"].ToString();
                    ca.DueDate = dr["DueDate"].ToString();
                    ca.ToPayTotalAmount = Convert.ToDecimal(dr["ToPayTotalAmount"]);
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
            try
            {
                int queueNo = 553;
                List<string> contractAccountList = new List<string>();

                foreach (DataRow dr in invoices.Rows)
                    contractAccountList.Add((string)dr["CaId"]);

                BPMConnector.ExportToFile(contractAccountList, queueNo, @"c:\services\");
                MessageBox.Show("Done.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loginBt_Click(object sender, EventArgs e)
        {
            if (connector.Login(userId.Text, passwordTxt.Text) == "VALID")
                MessageBox.Show("Welcome");
            else
                MessageBox.Show("Invalid Username or password");
                
        }

    }
}
