using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using System.Globalization;

namespace PEA.BPM.CashManagementModule
{
    public partial class ConfirmCashierTransfering : Form
    {
        private CashierInfo _commander;
        private string _receiverName;
        private string _noOfChq;
        private List<String> _list;


        public ConfirmCashierTransfering(CashierInfo commander, string fromCashier, string toCashier, string cashAmt, string chqAmt, string totalAmt, int NumOfChq, ref List<String> list)
        {
            InitializeComponent();
            _commander = commander;
            senderTxt.Text = fromCashier;
            receiverTxt.Text = toCashier;
            cashAmtTxt.Text = cashAmt;
            chqAmtTxt.Text = chqAmt;
            totalAmtTxt.Text = totalAmt;
            _list = list;
            NoOfChqTxt.Text = " " + NumOfChq.ToString() + "   รายการ";
            _noOfChq = NumOfChq.ToString();
            okBt.Focus();


            char[] spliter = { '<', '>', '-' };
            string[] sp = receiverTxt.Text.Split(spliter);
            if (sp.Length > 2)
                _receiverName = sp[2];
        }

        private void okBt_Click(object sender, EventArgs e)
        {

            if (printNoteCheckBox.Checked)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(string.Format("โอนเงิน ณ วันที่ {0} เวลา {1} น.", DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), DateTime.Now.ToString("HH:mm", new CultureInfo("th-TH"))));
                sb.AppendLine();
                sb.AppendFormat(string.Format("ผู้ทำการโอน <{0}-{1}>", _commander.CashierId, _commander.CashierName));
                sb.AppendLine();
                sb.AppendFormat(string.Format("{0}", senderTxt.Text));
                sb.AppendLine();
                sb.AppendFormat(string.Format("{0}", receiverTxt.Text));
                sb.AppendLine();
                sb.AppendLine("จำนวนเงินสด ");
                sb.AppendFormat(string.Format("เช็ค {0} ใบ จำนวนเงิน",_noOfChq));
                sb.AppendLine();
                sb.AppendLine("จำนวนเงินรวม ");
                sb.AppendLine();
                sb.AppendFormat(string.Format("{0}", "  .........................................."));
                sb.AppendLine();
                sb.AppendFormat(string.Format("(     {0}     )", _receiverName));
                sb.AppendLine();
                sb.AppendLine();
                sb.Append('\t');
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine(string.Format("{0} บาท", cashAmtTxt.Text));
                sb.AppendLine(string.Format("{0} บาท", chqAmtTxt.Text));
                sb.AppendLine(string.Format("{0} บาท", totalAmtTxt.Text));
                sb.AppendLine();

                _list.Add(sb.ToString());

            }
        }


    }
}