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
using System.Collections.Generic;
using System.Data;

using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Reporting.WinForms;

using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.DataSet;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Architecture.PrintUtilities;
using PEA.BPM.Infrastructure.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.ReportViews
{
    [SmartPart]
    public partial class CAB04_01ReportView : UserControl, ICAB04_01ReportView
    {
        public CAB04_01ReportView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public CAB04_01ReportViewPresenter Presenter
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

        }

        public void ShowReport(List<CommissionVoucherInfoReport> _myList, bool printPreivew)
        {
            DataRow rdCommission;
            DataSet2 ds = new DataSet2();
            DataTable dtCommission = ds.Tables["tblCommissionVoucher"];

            ReportParameter pAgencyName = new ReportParameter();
            ReportParameter pAgencyAddress = new ReportParameter();
            ReportParameter pBranchName = new ReportParameter();
            ReportParameter pPrintDate = new ReportParameter();

            foreach (CommissionVoucherInfoReport row in _myList)
            {
                rdCommission = dtCommission.NewRow();
                rdCommission["PEACode"] = row.PEACode;
                rdCommission["VoucherCode"] = row.VoucherCode;
                rdCommission["VoucherDate"] = row.VoucherDate;
                rdCommission["IssueCode"] = row.IssueCode;
                rdCommission["IssueName"] = row.IssueName;
                rdCommission["IssueTypeName"] = row.IssueTypeName;
                rdCommission["IssueTaxNo"] = row.IssueTaxNo;
                rdCommission["IssueAddress"] = row.IssueAddress;
                rdCommission["TotalItemOfResidentType"] = row.TotalItemOfResidentType;
                rdCommission["RateOfResidentType"] = row.RateOfResidentType;
                rdCommission["AmountCommissionOfResidentType"] = row.AmountCommissionOfResidentType;
                rdCommission["TotalItemOfSmallBizType"] = row.TotalItemOfSmallBizType;
                rdCommission["RateOfSmallBizType"] = row.RateOfSmallBizType;
                rdCommission["AmountCommissionOfSmallBizType"] = row.AmountCommissionOfSmallBizType;
                rdCommission["TotalItemOfGovermentDepartmentType"] = row.TotalItemOfGovermentDepartmentType;
                rdCommission["RateOfGovermentDepartmentType"] = row.RateOfGovermentDepartmentType;
                rdCommission["AmountCommissionOfGovermentDepartmentType"] = row.AmountCommissionOfGovermentDepartmentType;
        
                rdCommission["TotalItemOfGovermentPaidType"] = row.TotalItemOfGovermentPaidType;
                rdCommission["RateOfGovermentPaidType"] = row.RateOfGovermentPaidType;
                rdCommission["AmountCommissionOfGovermentPaidType"] = row.AmountCommissionOfGovermentPaidType;
        
                rdCommission["TotalItemOfSendBillType"] = row.TotalItemOfSentBillType;
                rdCommission["RateOfSendBillType"] = row.RateOfSentBillType;
                rdCommission["AmountCommissionOfSendBillType"] = row.AmountCommissionOfSentBillType;
                rdCommission["TransportOfCarPrice"] = row.TransportOfCarPrice;
                rdCommission["TransportOfWaterPrice"] = row.TransportOfWaterPrice;
                rdCommission["AllTransportPrice"] = row.AllTransportPrice;
                rdCommission["TotalMoneySpacialCase"] = row.TotalMoneySpacialCase;
                rdCommission["TotalAllCommission"] = row.TotalAllCommission;
                rdCommission["FineSendMoneyLate"] = row.FineSendMoneyLate;
                rdCommission["RealCommossionPaidAgency"] = row.RealCommissionPaidAgency;
                rdCommission["TotalAllCommissionText"] = row.TotalAllCommissionText;
                rdCommission["TaxAmount"] = row.TaxAmount;
                rdCommission["Vat7Percent"] = row.Vat7Percent;
                rdCommission["DebtAmount"] = row.DebtAmount;
                dtCommission.Rows.Add(rdCommission);
                dtCommission.AcceptChanges();

                pAgencyName = new ReportParameter("AgencyName", String.Format(" {0} : {1}", row.IssueCode, row.IssueName));
                pAgencyAddress = new ReportParameter("AgencyAddress", row.IssueAddress);
                pBranchName = new ReportParameter("BranchName", String.Format(" {0}, {1}", row.PEAName, row.PEACode));
                pPrintDate = new ReportParameter("PrintDate", row.PrintDate);
            }

            this.commissionVoucherReport.Reset();
            this.commissionVoucherReport.LocalReport.ReportEmbeddedResource = "PEA.BPM.AgencyManagementModule.Reports.CAB04_01.rdlc";
            ReportDataSource sCommissionVoucherReport = new ReportDataSource();

            ReportParameter[] ps = new ReportParameter[] { pAgencyName, pAgencyAddress, pBranchName, pPrintDate };

            sCommissionVoucherReport.Name = "DataSet2_tblCommissionVoucher";
            sCommissionVoucherReport.Value = ds.Tables["tblCommissionVoucher"];
            commissionVoucherReport.LocalReport.SetParameters(ps);
            commissionVoucherReport.LocalReport.DataSources.Add(sCommissionVoucherReport);   
            this.commissionVoucherReport.Width = this.Width;
            this.commissionVoucherReport.Height = this.Height;

            try
            {
                if (printPreivew)
                {
                    this.commissionVoucherReport.RefreshReport();
                }
                else
                {
                    PrintUtility _printer = new PrintUtility(LocalSettingNames.AgencyPrinterName, ModuleConfigurationNames.ReportWidth, ModuleConfigurationNames.ReportHeight);
                    _printer.Export(this.commissionVoucherReport.LocalReport);
                    _printer.CurrentPageIndex = 0;
                    _printer.Print();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

    }
}

