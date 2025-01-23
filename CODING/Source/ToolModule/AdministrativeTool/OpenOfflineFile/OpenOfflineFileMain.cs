using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using AdministrativeTool.DataSet;
using AdministrativeTool.Common;

namespace AdministrativeTool.OpenOfflineFile
{
    public partial class OpenOfflineFileMain : UserControl
    {
        #region Constant
        private const string TOTAL_FORMAT = "Total Record : {0}";
        #endregion

        #region Variables
        private OfflineItems offlineItems;
        #endregion

        #region Constructor
        public OpenOfflineFileMain()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void OpenOfflineFileMain_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Button Events
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "All Files|*.*";
                openFileDialog1.Title = "Open";
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                openFileDialog1.ShowDialog();

                string filename = openFileDialog1.FileName.Trim();

                if (!string.IsNullOrEmpty(filename))
                {
                    offlineItems = OpenOfflineFile.GetOfflineItemsFromFile(filename);
                    txtFilename.Text = filename;

                    ProgressDialog.Show();
                    this.LoadPayment();
                    this.LoadARPaymentTypeToGrid();
                    this.LoadARPaymentToGrid();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\r\n\r\n" + exc.StackTrace);
            }
            finally
            {
                ProgressDialog.Close();
            }
        }
        #endregion

        #region Method : LoadPayment()
        private void LoadPayment()
        {
            if (offlineItems == null) return;

            txtPaymentDate.Text = offlineItems.PaymentDt.ToString();
            txtCashierId.Text = offlineItems.CashierId;
            txtBranchId.Text = offlineItems.BranchId;
        }
        #endregion

        #region Method : LoadARPaymentTypeToGrid()
        private void LoadARPaymentTypeToGrid()
        {
            if (offlineItems == null) return;

            try
            {
                CommonDS.PaymentMethodsDataTable dt = new CommonDS.PaymentMethodsDataTable();

                // เรียง Data ลง DataTable ใหม่ (เพิ่ม Field Amount)
                foreach (PaymentMethod obj in offlineItems.PaymentMethods)
                {
                    DataRow newrow = dt.NewRow();
                    newrow["UiRefId"] = obj.UiRefId;
                    newrow["PtId"] = obj.PtId;
                    newrow["PtName"] = obj.PtName;
                    newrow["Description"] = obj.Description;
                    if (obj.ToPayAmount == null) newrow["ToPayAmount"] = DBNull.Value; else newrow["ToPayAmount"] = obj.ToPayAmount;
                    if (obj.ChangeAmount == null) newrow["ChangeAmount"] = DBNull.Value; else newrow["ChangeAmount"] = obj.ChangeAmount;
                    if (obj.FeeAmount == null) newrow["FeeAmount"] = DBNull.Value; else newrow["FeeAmount"] = obj.FeeAmount;
                    if (obj.ToPayAmountWithFee == null) newrow["ToPayAmountWithFee"] = DBNull.Value; else newrow["ToPayAmountWithFee"] = obj.ToPayAmountWithFee;
                    if (obj.ActualAmount == null) newrow["ActualAmount"] = DBNull.Value; else newrow["ActualAmount"] = obj.ActualAmount;
                    newrow["Bank"] = "";
                    newrow["BankId"] = obj.BankId;
                    newrow["BankName"] = obj.BankName;
                    newrow["ChqNo"] = obj.ChqNo;
                    newrow["ChqAccNo"] = obj.ChqAccNo;
                    if (obj.ChqDt == null) newrow["ChqDt"] = DBNull.Value; else newrow["ChqDt"] = (DateTime)obj.ChqDt;
                    newrow["DepositAccNo"] = obj.DepositAccNo;
                    if (obj.DepositDt == null) newrow["DepositDt"] = DBNull.Value; else newrow["DepositDt"] = (DateTime)obj.DepositDt;
                    if (obj.IsAGPayment == null) newrow["IsAGPayment"] = DBNull.Value; else newrow["IsAGPayment"] = obj.IsAGPayment;
                    newrow["DraftFlag"] = obj.DraftFlag;
                    newrow["CashierChequeFlag"] = obj.CashierChequeFlag;
                    newrow["ARPtId"] = obj.ARPtId;
                    newrow["TotalPayInvoiceAmount"] = obj.TotalPayInvoiceAmount;
                    newrow["TotalRemainAmount"] = obj.TotalRemainAmount;
                    newrow["Status"] = obj.Status;
                    newrow["Amount"] = this.GetPaymentMethodAmount(obj);
                    dt.Rows.Add(newrow);
                }

                dataGridViewARPaymentType.DataSource = dt;
                dataGridViewARPaymentType.Columns["UiRefId"].HeaderText = "UiRefId";
                dataGridViewARPaymentType.Columns["PtId"].HeaderText = "Payment Method";
                dataGridViewARPaymentType.Columns["PtName"].HeaderText = "PtName";
                dataGridViewARPaymentType.Columns["Description"].HeaderText = "Description";
                dataGridViewARPaymentType.Columns["ToPayAmount"].HeaderText = "ToPayAmount";
                dataGridViewARPaymentType.Columns["ChangeAmount"].HeaderText = "ChangeAmount";
                dataGridViewARPaymentType.Columns["FeeAmount"].HeaderText = "FeeAmount";
                dataGridViewARPaymentType.Columns["ToPayAmountWithFee"].HeaderText = "ToPayAmountWithFee";
                dataGridViewARPaymentType.Columns["ActualAmount"].HeaderText = "ActualAmount";
                dataGridViewARPaymentType.Columns["Bank"].HeaderText = "Bank";
                dataGridViewARPaymentType.Columns["BankId"].HeaderText = "BankId";
                dataGridViewARPaymentType.Columns["BankName"].HeaderText = "BankName";
                dataGridViewARPaymentType.Columns["ChqNo"].HeaderText = "ChqNo";
                dataGridViewARPaymentType.Columns["ChqAccNo"].HeaderText = "ChqAccNo";
                dataGridViewARPaymentType.Columns["ChqDt"].HeaderText = "ChqDt";
                dataGridViewARPaymentType.Columns["DepositAccNo"].HeaderText = "DepositAccNo";
                dataGridViewARPaymentType.Columns["DepositDt"].HeaderText = "DepositDt";
                dataGridViewARPaymentType.Columns["IsAGPayment"].HeaderText = "IsAGPayment";
                dataGridViewARPaymentType.Columns["DraftFlag"].HeaderText = "DraftFlag";
                dataGridViewARPaymentType.Columns["CashierChequeFlag"].HeaderText = "CashierChequeFlag";
                dataGridViewARPaymentType.Columns["ARPtId"].HeaderText = "ARPtId";
                dataGridViewARPaymentType.Columns["TotalPayInvoiceAmount"].HeaderText = "TotalPayInvoiceAmount";
                dataGridViewARPaymentType.Columns["TotalRemainAmount"].HeaderText = "TotalRemainAmount";
                dataGridViewARPaymentType.Columns["Status"].HeaderText = "Status";
                dataGridViewARPaymentType.Columns["Amount"].HeaderText = "Amount";

                dataGridViewARPaymentType.Columns["UiRefId"].Visible = false;
                dataGridViewARPaymentType.Columns["PtId"].Visible = true;
                dataGridViewARPaymentType.Columns["PtName"].Visible = false;
                dataGridViewARPaymentType.Columns["Description"].Visible = false;
                dataGridViewARPaymentType.Columns["ToPayAmount"].Visible = false;
                dataGridViewARPaymentType.Columns["ChangeAmount"].Visible = false;
                dataGridViewARPaymentType.Columns["FeeAmount"].Visible = false;
                dataGridViewARPaymentType.Columns["ToPayAmountWithFee"].Visible = false;
                dataGridViewARPaymentType.Columns["ActualAmount"].Visible = false;
                dataGridViewARPaymentType.Columns["Bank"].Visible = false;
                dataGridViewARPaymentType.Columns["BankId"].Visible = false;
                dataGridViewARPaymentType.Columns["BankName"].Visible = false;
                dataGridViewARPaymentType.Columns["ChqNo"].Visible = false;
                dataGridViewARPaymentType.Columns["ChqAccNo"].Visible = false;
                dataGridViewARPaymentType.Columns["ChqDt"].Visible = false;
                dataGridViewARPaymentType.Columns["DepositAccNo"].Visible = false;
                dataGridViewARPaymentType.Columns["DepositDt"].Visible = false;
                dataGridViewARPaymentType.Columns["IsAGPayment"].Visible = false;
                dataGridViewARPaymentType.Columns["DraftFlag"].Visible = false;
                dataGridViewARPaymentType.Columns["CashierChequeFlag"].Visible = false;
                dataGridViewARPaymentType.Columns["ARPtId"].Visible = false;
                dataGridViewARPaymentType.Columns["TotalPayInvoiceAmount"].Visible = false;
                dataGridViewARPaymentType.Columns["TotalRemainAmount"].Visible = false;
                dataGridViewARPaymentType.Columns["Status"].Visible = false;
                dataGridViewARPaymentType.Columns["Amount"].Visible = true;

                dataGridViewARPaymentType.Columns["PtId"].MinimumWidth = 120;

                dataGridViewARPaymentType.Columns["PtId"].Width = 120;

                labTotal1.Text = string.Format(TOTAL_FORMAT, dataGridViewARPaymentType.Rows.Count);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\r\n\r\n" + exc.StackTrace);
            }
        }
        #endregion

        #region Method : LoadARPaymentToGrid()
        private void LoadARPaymentToGrid()
        {
            if (offlineItems == null) return;

            try
            {
                dataGridViewARPayment.DataSource = offlineItems.Receipts.ToArray();

                dataGridViewARPayment.Columns["BranchName"].HeaderText = "BranchName";
                dataGridViewARPayment.Columns["BranchNumber"].HeaderText = "BranchNumber";
                dataGridViewARPayment.Columns["BranchAddress"].HeaderText = "BranchAddress";
                dataGridViewARPayment.Columns["TerminalCode"].HeaderText = "TerminalCode";
                dataGridViewARPayment.Columns["PrintingSequence"].HeaderText = "PrintingSequence";
                dataGridViewARPayment.Columns["TotalReceipt"].HeaderText = "TotalReceipt";
                dataGridViewARPayment.Columns["PrePrintedHeader"].HeaderText = "PrePrintedHeader";
                dataGridViewARPayment.Columns["Prefix"].HeaderText = "Prefix";
                dataGridViewARPayment.Columns["ReceiptId"].HeaderText = "Receipt ID";
                dataGridViewARPayment.Columns["DisplayCaId"].HeaderText = "DisplayCaId";
                dataGridViewARPayment.Columns["CustomerId"].HeaderText = "Customer ID";
                dataGridViewARPayment.Columns["CustomerName"].HeaderText = "Customer Name";
                dataGridViewARPayment.Columns["CustomerAddress"].HeaderText = "CustomerAddress";
                dataGridViewARPayment.Columns["IsNameAddressModified"].HeaderText = "IsNameAddressModified";
                dataGridViewARPayment.Columns["ContractType"].HeaderText = "ContractType";
                dataGridViewARPayment.Columns["TotalAmount"].HeaderText = "Total Amount";
                dataGridViewARPayment.Columns["PaidAmount"].HeaderText = "PaidAmount";
                dataGridViewARPayment.Columns["ChangeAmount"].HeaderText = "ChangeAmount";
                dataGridViewARPayment.Columns["AdjChangeAmount"].HeaderText = "AdjChangeAmount";
                dataGridViewARPayment.Columns["PaymentDate"].HeaderText = "PaymentDate";
                dataGridViewARPayment.Columns["CashierId"].HeaderText = "CashierId";
                dataGridViewARPayment.Columns["CashierName"].HeaderText = "CashierName";
                dataGridViewARPayment.Columns["IsTaxReceipt"].HeaderText = "IsTaxReceipt";
                dataGridViewARPayment.Columns["ReceiptType"].HeaderText = "ReceiptType";

                dataGridViewARPayment.Columns["BranchName"].Visible = false;
                dataGridViewARPayment.Columns["BranchNumber"].Visible = false;
                dataGridViewARPayment.Columns["BranchAddress"].Visible = false;
                dataGridViewARPayment.Columns["TerminalCode"].Visible = false;
                dataGridViewARPayment.Columns["PrintingSequence"].Visible = false;
                dataGridViewARPayment.Columns["TotalReceipt"].Visible = false;
                dataGridViewARPayment.Columns["PrePrintedHeader"].Visible = false;
                dataGridViewARPayment.Columns["Prefix"].Visible = false;
                dataGridViewARPayment.Columns["ReceiptId"].Visible = true;
                dataGridViewARPayment.Columns["DisplayCaId"].Visible = false;
                dataGridViewARPayment.Columns["CustomerId"].Visible = true;
                dataGridViewARPayment.Columns["CustomerName"].Visible = true;
                dataGridViewARPayment.Columns["CustomerAddress"].Visible = false;
                dataGridViewARPayment.Columns["IsNameAddressModified"].Visible = false;
                dataGridViewARPayment.Columns["ContractType"].Visible = false;
                dataGridViewARPayment.Columns["TotalAmount"].Visible = true;
                dataGridViewARPayment.Columns["PaidAmount"].Visible = false;
                dataGridViewARPayment.Columns["ChangeAmount"].Visible = false;
                dataGridViewARPayment.Columns["AdjChangeAmount"].Visible = false;
                dataGridViewARPayment.Columns["PaymentDate"].Visible = false;
                dataGridViewARPayment.Columns["CashierId"].Visible = false;
                dataGridViewARPayment.Columns["CashierName"].Visible = false;
                dataGridViewARPayment.Columns["IsTaxReceipt"].Visible = false;
                dataGridViewARPayment.Columns["ReceiptType"].Visible = false;

                dataGridViewARPayment.Columns["ReceiptId"].MinimumWidth = 120;
                dataGridViewARPayment.Columns["CustomerId"].MinimumWidth = 100;
                dataGridViewARPayment.Columns["CustomerName"].MinimumWidth = 250;

                dataGridViewARPayment.Columns["ReceiptId"].Width = 120;
                dataGridViewARPayment.Columns["CustomerId"].Width = 100;
                dataGridViewARPayment.Columns["CustomerName"].Width = 250;

                labTotal2.Text = string.Format(TOTAL_FORMAT, dataGridViewARPayment.Rows.Count);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\r\n\r\n" + exc.StackTrace);
            }
        }
        #endregion

        #region Method : GetPaymentMethodAmount()
        private decimal GetPaymentMethodAmount(PaymentMethod paymentMethod)
        {
            decimal ActualAmount = 0;
            decimal ChangeAmount = 0;

            if (paymentMethod.ActualAmount != null) ActualAmount = (int)paymentMethod.ActualAmount;
            if (paymentMethod.ChangeAmount != null) ChangeAmount = (int)paymentMethod.ChangeAmount;

            return ActualAmount - ChangeAmount;
        }
        #endregion
    }
}