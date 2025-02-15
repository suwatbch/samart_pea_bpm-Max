//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// This class is the concrete implementation of a View in the Model-View-Presenter 
// pattern. Communication between the Presenter and this class is acheived through 
// an interface to facilitate separation and testability.
// Note that the Presenter generated by the same recipe, will automatically be created
// by CAB through [CreateNew] and bidirectional references will be added.
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureTool.Control;
using System.Drawing;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.PrintUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.ePaymentsModule.Interface.Constants;
using System.Data;
using System.IO;
using System.Text;

namespace PEA.BPM.ePaymentsModule
{
    [SmartPart]
    public partial class FileResultView : UserControl, IFileResultView
    {
        private List<EpayUploadItem> uploadItemList;
        private List<EpayUploadItem> tmpUploadItemList;
        private List<EPaymentUploadFile> ePayUploadFileList;
        private DataTable uploadDt;
        private List<Company> compList;
        private List<PaymentUploadSummary> paySumList;
        private DateTime? fileUploadDt;

        #region Constructure

        public FileResultView()
        {
            compList = new List<Company>();
            InitializeComponent();
            UploadFileGV.AutoGenerateColumns = false;
            ClearSummarizeGV.AutoGenerateColumns = false;
        }

        #endregion

        #region System Init

        [CreateNew]
        public FileResultViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();
            uploadItemList = new List<EpayUploadItem>();
            tmpUploadItemList = new List<EpayUploadItem>();
            ePayUploadFileList = new List<EPaymentUploadFile>();
            paySumList = new List<PaymentUploadSummary>();
        }

        #endregion

        #region IFileResultView Members

        public void AddEPayUploadFile(List<EpayUploadItem> ePayFile)
        {
            try
            {
                this.uploadItemList = ePayFile;
                if (this.uploadItemList.Count > 0)
                {
                    fileUploadDt = this.uploadItemList[0].EpayUploads.UploadDt;
                }
                BindingPaymentUploadFile();
                if (tabView.SelectedIndex == 1)
                {
                    SetValueByCompany();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetCompanyGroup(List<Company> compList)
        {
            if (compList != null)
                this.compList = compList;
        }

        #endregion

        #region Event Handler

        private void UploadFileGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (UploadFileGV.Rows[e.RowIndex].Cells["statusDGView"].Value == null)
            {
                EpayUploadItem payment = (EpayUploadItem)UploadFileGV.Rows[e.RowIndex].DataBoundItem;

                if (payment.UploadStatus == "0")
                {
                    UploadFileGV.Rows[e.RowIndex].Cells["statusDGView"].Value = Properties.Resources.Pass;
                }
                else
                {
                    UploadFileGV.Rows[e.RowIndex].Cells["statusDGView"].Value = Properties.Resources.Exclamation;
                }
            }
        }

        private void UploadFileGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EpayUploadItem payment = (EpayUploadItem)UploadFileGV.Rows[e.RowIndex].DataBoundItem;
                List<EpayUploadItem> epayList = new List<EpayUploadItem>();
                for (int i = 0; i < 7; i++)
                {
                    epayList.Add(payment);
                }
                _presenter.OnGetEPaymentDetail(epayList);
            }
        }

        private void tabView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabView.SelectedIndex == 1 && UploadFileGV.Rows.Count > 0)
            {
                SetValueByCompany();
            }
            else
            {
                if (paySumList != null)
                {
                    paySumList.Clear();
                    BindingPaymentByCompany();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (uploadItemList.Count > 0)
                {
                    WaitingFormHelper.ShowWaitingForm();
                    BindingEPaymentUploadFile();
                    _presenter.InsertEPayUpload(ePayUploadFileList);
                    WaitingFormHelper.HideWaitingForm();

                    MessageBox.Show("�ѹ�֡���������º����", "�š�úѹ�֡", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetControl();
                    _presenter.OnResetFileName();
                }
                else
                {
                    MessageBox.Show("��سҷӡ���ѻ��Ŵ�����š�͹���ѹ�֡", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                MessageBox.Show(ex.Message, "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetControl();
            }
        }

        #endregion

        #region Receipt Printing

     


        #endregion

        #region Custom Function

        private void BindingPaymentUploadFile()
        {
            EPayUploadItemCompare epayCompare = new EPayUploadItemCompare(EPayUploadItemColumn.PayUploadFormat, SortDirection.Ascending);
            uploadItemList.Sort(epayCompare);
            List<EpayUploadItem> tmpList = new List<EpayUploadItem>();
            foreach (EpayUploadItem tmp in uploadItemList)
            {
                EpayUploadItem epayItem = (EpayUploadItem)ObjectClone.GetClone(tmp);
                if (epayItem.InvoiceNo != null)
                {
                    epayItem.InvoiceNo = epayItem.InvoiceNo.Substring(4, 12);
                }
                if (epayItem.SysInvoiceNo != null)
                {
                    epayItem.SysInvoiceNo = epayItem.SysInvoiceNo.Substring(4, 12);
                }
                tmpList.Add(epayItem);
            }

            UploadFileGV.DataSource = tmpList.ToArray();
            UploadFileGV.Refresh();

            SetSummaryValue();
        }

        private void ResetControl()
        {
            if (uploadItemList.Count > 0)
            {
                paySumList.Clear();
                uploadItemList.Clear();
                BindingPaymentUploadFile();
                BindingPaymentByCompany();
                tabView.SelectedIndex = 0;
            }
        }

        private DataTable CreateUploadDT(string tableName)
        {
            DataTable upLoadFileDT;
            DataColumn dtc;

            upLoadFileDT = new DataTable(tableName);
            for (int i = 0; i < EPayUploadName.strName.Length; i++)
            {
                dtc = new DataColumn();
                if (i == 8 || i == 9)
                {
                    dtc.DataType = System.Type.GetType("System.DateTime");
                }
                else if (i == 10 || i == 11 || i == 12)
                {
                    dtc.DataType = System.Type.GetType("System.Decimal");
                }
                else
                {
                    dtc.DataType = System.Type.GetType("System.String");
                }
                dtc.ColumnName = EPayUploadName.strName[i];
                dtc.AutoIncrement = false;
                dtc.ReadOnly = false;
                dtc.Unique = false;
                upLoadFileDT.Columns.Add(dtc);
            }
            return upLoadFileDT;
        }

        private void SetSummaryValue()
        {
            decimal sumDebtAmountSys = 0M;
            decimal sumDebtAmountFile = 0M;
            decimal sumDebNoSys = 0M;
            decimal sumDebNoFile = 0M;
            decimal sumClearDebtAmount = 0M;
            decimal sumUnClearDebtAmount = 0M;
            decimal sumClearDebNo = 0M;
            decimal sumUnClearDebNo = 0M;

            sumDebNoFile = uploadItemList.Count;

            foreach (EpayUploadItem payment in uploadItemList)
            {
                sumDebtAmountFile += payment.OutSourceAmount == null ? 0M : Convert.ToDecimal(payment.OutSourceAmount);

                if (payment.SysGAmount != null)
                {
                    sumDebNoSys++;
                    sumDebtAmountSys += Convert.ToDecimal(payment.SysGAmount);
                }

                if (payment.UploadStatus == "0")
                {
                    sumClearDebtAmount += payment.OutSourceAmount == null ? 0M : Convert.ToDecimal(payment.OutSourceAmount);
                    sumClearDebNo += 1;
                }
                else
                {
                    sumUnClearDebtAmount += payment.OutSourceAmount == null ? 0M : Convert.ToDecimal(payment.OutSourceAmount);
                    sumUnClearDebNo += 1;
                }
            }
            txtNoDebSys.Text = sumDebNoSys.ToString("#,###,##0");
            txtNoDebFile.Text = sumDebNoFile.ToString("#,###,##0");
            txtDebtAmountSys.Text = sumDebtAmountSys.ToString("#,###,##0.00");
            txtDebtAmountFile.Text = sumDebtAmountFile.ToString("#,###,##0.00");
            txtClearNo.Text = sumClearDebNo.ToString("#,###,##0");
            txtBackNo.Text = sumUnClearDebNo.ToString("#,###,##0");
            txtClearAmount.Text = sumClearDebtAmount.ToString("#,###,##0.00");
            txtBackAmount.Text = sumUnClearDebtAmount.ToString("#,###,##0.00");
        }

        private void SetValueByCompany()
        {
            uploadDt = CreateUploadDT("UploadPaymentDT");
            foreach (EpayUploadItem ePayUpload in uploadItemList)
            {
                DataRow dr = uploadDt.NewRow();
                dr[EPayUploadName.strName[0]] = ePayUpload.BranchId;
                dr[EPayUploadName.strName[1]] = ePayUpload.CaId;
                dr[EPayUploadName.strName[2]] = ePayUpload.RecNo;
                dr[EPayUploadName.strName[3]] = ePayUpload.Period;
                dr[EPayUploadName.strName[4]] = ePayUpload.CompanyId;
                dr[EPayUploadName.strName[5]] = ePayUpload.ActCode;
                dr[EPayUploadName.strName[6]] = ePayUpload.PosNo;
                dr[EPayUploadName.strName[7]] = ePayUpload.InvoiceNo;
                dr[EPayUploadName.strName[8]] = ePayUpload.PayDt;
                dr[EPayUploadName.strName[9]] = ePayUpload.DueDt;
                dr[EPayUploadName.strName[10]] = ePayUpload.OutSourceAmount == null ? 0M : ePayUpload.OutSourceAmount;
                dr[EPayUploadName.strName[11]] = ePayUpload.VatAmount == null ? 0M : ePayUpload.VatAmount; ;
                dr[EPayUploadName.strName[12]] = ePayUpload.SysGAmount == null ? 0M : ePayUpload.SysGAmount;
                dr[EPayUploadName.strName[13]] = ePayUpload.CompanyId + ":" + ePayUpload.EpayUploads.CompanyName;
                dr[EPayUploadName.strName[14]] = ePayUpload.UploadStatus;
                dr[EPayUploadName.strName[15]] = ePayUpload.EpayUploads.FileName;

                uploadDt.Rows.Add(dr);
            }
            paySumList = new List<PaymentUploadSummary>();
            foreach (Company comp in compList)
            {
                DataRow[] dr = uploadDt.Select(String.Format("COMPANY_ID LIKE '{0}'", comp.CompanyId));
                BindingValueByCompany(dr);
            }
            BindingPaymentByCompany();
        }

        private void BindingValueByCompany(DataRow[] dr)
        {
            decimal sumDebtAmountSys = 0M;
            decimal sumDebtAmountFile = 0M;
            int sumDebtNoSys = 0;
            int sumDebtNoFile = 0;
            decimal sumClearDebtAmount = 0M;
            decimal sumUnClearDebtAmount = 0M;
            int sumClearDebNo = 0;
            int sumUnClearDebNo = 0;
            decimal tmpSysAmount = 0M;
            decimal tmpFileAmount = 0M;

            PaymentUploadSummary payUploadSum = new PaymentUploadSummary();
            sumDebtNoFile = dr.Length;
            for (int i = 0; i < dr.Length; i++)
            {
                tmpSysAmount = Convert.ToDecimal(dr[i][EPayUploadName.strName[12]]);
                tmpFileAmount = Convert.ToDecimal(dr[i][EPayUploadName.strName[10]]);
                if (tmpSysAmount != 0)
                {
                    sumDebtNoSys++;
                }
                sumDebtAmountSys += tmpSysAmount;
                sumDebtAmountFile += tmpFileAmount;

                if (dr[i][EPayUploadName.strName[14]].ToString().Trim() == "0")
                {
                    sumClearDebtAmount += tmpFileAmount;
                    sumClearDebNo += 1;
                }
                else
                {
                    sumUnClearDebtAmount += tmpFileAmount;
                    sumUnClearDebNo += 1;
                }
            }
            if (dr.Length > 0)
            {
                payUploadSum.CompanyName = dr[0][EPayUploadName.strName[13]].ToString();
            }
            payUploadSum.CompanyId = dr[0][EPayUploadName.strName[4]].ToString();
            payUploadSum.SysDebtNo = sumDebtNoSys;
            payUploadSum.FileDebtNo = sumDebtNoFile;
            payUploadSum.SysDebtAmount = sumDebtAmountSys;
            payUploadSum.FileDebtAmount = sumDebtAmountFile;
            payUploadSum.ClearDebtAmount = sumClearDebtAmount;
            payUploadSum.BackDebtAmount = sumUnClearDebtAmount;
            payUploadSum.ClearDebtNo = sumClearDebNo;
            payUploadSum.BackDebtNo = sumUnClearDebNo;

            paySumList.Add(payUploadSum);
        }

        private void BindingPaymentByCompany()
        {
            PayUploadSummaryCompare paySumCompare = new PayUploadSummaryCompare(PayUploadSummaryColumn.CompanyId, SortDirection.Ascending);
            paySumList.Sort(paySumCompare);
            ClearSummarizeGV.DataSource = paySumList.ToArray();
            ClearSummarizeGV.Refresh();
        }

        private void BindingEPaymentUploadFile()
        {
            try
            {
                if (paySumList == null)
                {
                    SetValueByCompany();
                }
                else if (paySumList.Count == 0)
                {
                    SetValueByCompany();
                }

                if (paySumList.Count > 0)
                {
                    EPayUpload ePay;
                    EpayUploadItem tmpUpload;
                    EPayClearify tmpClearify;
                    EPaymentUploadFile tmpUploadFile;
                    List<EpayUploadItem> uploadList;
                    List<EPayClearify> clearifyList;
                    DateTime currentDate = DateTime.Now;
                    ePayUploadFileList = new List<EPaymentUploadFile>();
                    foreach (PaymentUploadSummary payUpSum in paySumList)
                    {
                        ePay = new EPayUpload();
                        ePay.UploadDt = fileUploadDt;
                        ePay.CompanyId = payUpSum.CompanyId;
                        ePay.BillCount = payUpSum.ClearDebtNo;
                        ePay.BillAmount = payUpSum.ClearDebtAmount;
                        ePay.TotalBillCount = payUpSum.FileDebtNo;
                        ePay.TotalBillAmount = payUpSum.FileDebtAmount;
                        ePay.SyncFlag = "1";
                        ePay.PostDt = currentDate;
                        ePay.PostBranchId = Session.Branch.Id;
                        ePay.ModifiedDt = currentDate;
                        ePay.ModifiedBy = Session.User.Id;
                        ePay.Active = "1";
                  
                        DataRow[] dr = uploadDt.Select(String.Format("COMPANY_ID LIKE '{0}'", payUpSum.CompanyId));
                        if (dr.Length > 0)
                        {
                            ePay.FileName = dr[0][EPayUploadName.strName[15]].ToString();
                        }
                        uploadList = new List<EpayUploadItem>();
                        clearifyList = new List<EPayClearify>();

                        for (int i = 0; i < dr.Length; i++)
                        {
                            tmpUpload = new EpayUploadItem();
                            tmpClearify = new EPayClearify();
                            tmpUpload.BranchId = dr[i][EPayUploadName.strName[0]].ToString();
                            tmpUpload.CaId = dr[i][EPayUploadName.strName[1]].ToString();
                            tmpUpload.RecNo = dr[i][EPayUploadName.strName[2]].ToString();
                            tmpUpload.Period = dr[i][EPayUploadName.strName[3]].ToString();
                            tmpUpload.CompanyId = dr[i][EPayUploadName.strName[4]].ToString();
                            tmpUpload.ActCode = dr[i][EPayUploadName.strName[5]].ToString();
                            tmpUpload.PosNo = dr[i][EPayUploadName.strName[6]].ToString();
                            tmpUpload.InvoiceNo = dr[i][EPayUploadName.strName[7]].ToString();
                            tmpUpload.PayDt = Convert.ToDateTime(dr[i][EPayUploadName.strName[8]]);
                            tmpUpload.DueDt = Convert.ToDateTime(dr[i][EPayUploadName.strName[9]]);
                            tmpUpload.OutSourceAmount = Convert.ToDecimal(dr[i][EPayUploadName.strName[10]]);
                            tmpUpload.VatAmount = Convert.ToDecimal(dr[i][EPayUploadName.strName[11]]);
                            tmpUpload.UploadStatus = dr[i][EPayUploadName.strName[14]].ToString();
                            tmpUpload.SyncFlag = "1";
                            tmpUpload.PostBranchId = Session.Branch.Id;
                            tmpUpload.PostDt = currentDate;
                            tmpUpload.ModifiedDt = currentDate;
                            tmpUpload.ModifiedBy = Session.User.Id;
                            tmpUpload.Active = "1";

                            if (!tmpUpload.UploadStatus.Equals(EPaymentUploadStatus.CLEAR))
                            {
                                tmpClearify.EpayUploadItems.InvoiceNo = tmpUpload.InvoiceNo;
                                tmpClearify.SyncFlag = "1";
                                tmpClearify.PostDt = currentDate;
                                tmpClearify.PostBranchId = Session.Branch.Id;
                                tmpClearify.ModifiedDt = currentDate;
                                tmpClearify.ModifiedBy = Session.User.Id;
                                tmpClearify.Active = "1";

                            }
                            uploadList.Add(tmpUpload);
                            clearifyList.Add(tmpClearify);
                        }
                        tmpUploadFile = new EPaymentUploadFile(ePay, uploadList, clearifyList);
                        tmpUploadFile.PosId = Session.Terminal.Id;
                        tmpUploadFile.CurrentDate = currentDate;
                        ePayUploadFileList.Add(tmpUploadFile);
                    }
                }

            }
            catch
            {
                throw;
            }
        }
        
        #endregion

    }
}

