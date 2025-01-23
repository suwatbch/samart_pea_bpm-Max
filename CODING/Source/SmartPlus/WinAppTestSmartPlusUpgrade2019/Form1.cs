using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinAppTestSmartPlus.SmartPlusWS;
using PEA.BPM.SmartPlus.Interface.BusinessEntities;

namespace WinAppTestSmartPlus
{
    public partial class Form1 : Form
    {
        SmartPlusServiceSoapClient WS = new SmartPlusWS.SmartPlusServiceSoapClient();

        public Form1()
        {
            InitializeComponent();
            WS = new SmartPlusWS.SmartPlusServiceSoapClient();
        }

        #region ..GET AR INFORMATION 

        private void btnSearchContractorService_Click(object sender, EventArgs e)
        {
            btnSearchContractorService.Enabled = false;

            rchTxtListInvoiceNo.Clear();
            rchMarkFlagResult.Clear();
            rchCancelResult.Clear();
            gvArResult.DataSource = null;
            try
            {
                string  caid    = txtCaId.Text.Trim();
                var     result  = WS.SearchContractorService(caid);
                int     RecCnt  = 0;
                foreach (var r in result)
                {
                    string invoiceNo    = null;
                    invoiceNo           = r.InvoiceNo;
                    RecCnt              += 1;
                    AppendTextToRichTextInvoiceNo(invoiceNo);
                              
                }

                var source                      = new BindingSource(result, null);
                gvArResult.DataSource           = result;
                groupBoxListOfInvoiceNo.Text    = "AR Information Result Total " + RecCnt.ToString() + " Records.";
                groupBoxARResult.Text           = "List of InvoiceNo Total " +RecCnt.ToString() + " Invoices.";
                SettingMarkFlagAndCancelParameters();
                

            }
            catch (Exception)
            {
                btnSearchContractorService.Enabled = true;
                throw;
            }
            btnSearchContractorService.Enabled = true;
        }

        private void SettingMarkFlagAndCancelParameters()
        {
            var ListInvoiceNo    = rchTxtListInvoiceNo.Text.Split('\n');
            StringBuilder sbd    = new StringBuilder();
            foreach (var r in ListInvoiceNo)
            {
                sbd.Append(r);
                sbd.Append("|");
            }
            int InvoiceNoLength         = sbd.ToString().Length;
            string tmpInvoiceNo         = sbd.ToString().Substring(0,InvoiceNoLength - 1);

            txtMarkFlagCaid.Text        = txtCaId.Text.Trim();
            txtMarkFlagInvoiceNo.Text   = tmpInvoiceNo;

            txtCancelCaid.Text          = txtCaId.Text.Trim();
            txtCancelInvoiceNo.Text     = tmpInvoiceNo;
        }

        private void AppendTextToRichTextInvoiceNo(string text)
        {
            if (!String.IsNullOrEmpty(rchTxtListInvoiceNo.Text)) 
            {
                rchTxtListInvoiceNo.AppendText("\r\n" + text); 
            } 
            else 
            {
                rchTxtListInvoiceNo.AppendText(text); 
            }
            rchTxtListInvoiceNo.ScrollToCaret();
        }

        #endregion

        #region ..MARK FLAG

        private void btnMarkFlag_Click(object sender, EventArgs e)
        {
            btnMarkFlag.Enabled = false;
            try
            {
                string CaId = txtMarkFlagCaid.Text.Trim();
                string ListInvoiceNo = txtMarkFlagInvoiceNo.Text.Trim();
                string MarkStatus = null;
                MarkStatus = WS.UpdateBillMarkFlagService(CaId, ListInvoiceNo);
                AppendTextToRichTextMarkFlagResult(MarkStatus);
            }
            catch (Exception)
            {
                btnMarkFlag.Enabled = true;
                throw;
            }
            btnMarkFlag.Enabled = true;
        }

        private void AppendTextToRichTextMarkFlagResult(string text)
        {
            if (!String.IsNullOrEmpty(rchMarkFlagResult.Text))
            {
                rchMarkFlagResult.AppendText("\r\n" + text);
            }
            else
            {
                rchMarkFlagResult.AppendText(text);
            }
            rchMarkFlagResult.ScrollToCaret();
        }

        #endregion
         
        #region ..CANCEL PAYMENT

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            try
            {
                string CaId             = txtCancelCaid.Text.Trim();
                string ListInvoiceNo    = txtCancelInvoiceNo.Text.Trim();
                string CancelStatus     = null;
                CancelStatus            = WS.CancelPaymentService(CaId, ListInvoiceNo);
                AppendTextToRichTextCancelResult(CancelStatus);
            }
            catch (Exception)
            {
                btnCancel.Enabled = true;
                throw;
            }
            btnCancel.Enabled = true;
        }

        private void AppendTextToRichTextCancelResult(string text)
        {
            if (!String.IsNullOrEmpty(rchCancelResult.Text))
            {
                rchCancelResult.AppendText("\r\n" + text);
            }
            else
            {
                rchCancelResult.AppendText(text);
            }
            rchCancelResult.ScrollToCaret();
        }

        #endregion

    }
}
