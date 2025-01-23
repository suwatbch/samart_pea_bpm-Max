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
using System.Globalization;

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
    public partial class CAB04_02ReportView : UserControl, ICAB04_02ReportView
    {
        public CAB04_02ReportView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public CAB04_02ReportViewPresenter Presenter
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


        public void ShowReport(List<CommissionRegistInfoReport> _myList, bool printPreview)
        {
            try
            {
                string strDate = "";
                DataRow rdRegistryCommission;
                DataSet2 ds = new DataSet2();
                DataTable dtRegistryCommission = ds.Tables["tblRegistryCommission"];

                ReportParameter pPrintDate = new ReportParameter();
                ReportParameter pBranchName = new ReportParameter();
                ReportParameter pAgencyName = new ReportParameter();
                ReportParameter pPeriod = new ReportParameter();
                ReportParameter pTimeNo = new ReportParameter();
                ReportParameter pRegisterDate = new ReportParameter();
                ReportParameter pCalculateDate = new ReportParameter();

                foreach (CommissionRegistInfoReport row in _myList)
                {
                    rdRegistryCommission = dtRegistryCommission.NewRow();
                    rdRegistryCommission["PEABranch"] = row.PEABranch;
                    rdRegistryCommission["PEAName"] = row.PEAName;
                    rdRegistryCommission["AgencyName"] = row.AgencyName;
                    rdRegistryCommission["AgencyCode"] = row.AgencyCode;
                    rdRegistryCommission["AgencyType"] = row.AgencyType;
                    rdRegistryCommission["AgencyTaxNo"] = row.AgencyTaxNo;                  
                    rdRegistryCommission["RegisterDate"] = strDate;
                    rdRegistryCommission["BillMonth"] = row.BillMonth;
                    rdRegistryCommission["TimeNo"] = row.TimeNo;
                    rdRegistryCommission["BillOutToAgencyForResident"] = row.BillOutToAgencyForResident;
                    rdRegistryCommission["ValueOfBillOutForResident"] = row.ValueOfBillOutForResident;
                    rdRegistryCommission["CanKeepElectricForResident"] = row.CanKeepElectricForResident;
                    rdRegistryCommission["ValueOfKeepElectricForResident"] = row.ValueOfKeepElectricForResident;
                    rdRegistryCommission["RateAgencyPersonTypeForResident"] = row.RateAgencyPersonTypeForResident;
                    rdRegistryCommission["RateAgencyCompanyTypeForResident"] = row.RateAgencyCompanyTypeForResident;
                    rdRegistryCommission["AmountCommissionOfResident"] = row.AmountCommissionOfResident;
                    rdRegistryCommission["BillOutToAgencyForSmallBiz"] = row.BillOutToAgencyForSmallBiz;
                    rdRegistryCommission["ValueOfBillOutForSmallBiz"] = row.ValueOfBillOutForSmallBiz;
                    rdRegistryCommission["CanKeepElectricForSmallBiz"] = row.CanKeepElectricForSmallBiz;
                    rdRegistryCommission["ValueOfKeepElectricForSmallBiz"] = row.ValueOfKeepElectricForSmallBiz;
                    rdRegistryCommission["RateAgencyPersonTypeForSmallBiz"] = row.RateAgencyPersonTypeForSmallBiz;
                    rdRegistryCommission["RateAgencyCompanyTypeForSmallBiz"] = row.RateAgencyCompanyTypeForSmallBiz;
                    rdRegistryCommission["AmountCommissionOfSmallBiz"] = row.AmountCommissionOfSmallBiz;

                    rdRegistryCommission["BillOutToAgencyForGovermentDepartment"] = row.BillOutToAgencyForGovermentDepartment;
                    rdRegistryCommission["ValueOfBillOutForGovermentDepartment"] = row.ValueOfBillOutForGovermentDepartment;
                    rdRegistryCommission["CanKeepElectricForGovermentDepartment"] = row.CanKeepElectricForGovermentDepartment;
                    rdRegistryCommission["ValueOfKeepElectricForGovermentDepartment"] = row.ValueOfKeepElectricForGovermentDepartment;
                    rdRegistryCommission["RateAgencyPersonTypeForGovermentDepartment"] = row.RateAgencyPersonTypeForGovermentDepartment;
                    rdRegistryCommission["RateAgencyCompanyTypeForGovermentDepartment"] = row.RateAgencyCompanyTypeForGovermentDepartment;
                    rdRegistryCommission["AmountCommissionOfGovermentDepartment"] = row.AmountCommissionOfGovermentDepartment;

                    rdRegistryCommission["BillOutToAgencyForGovermentPaid"] = row.BillOutToAgencyForGovermentPaid;
                    rdRegistryCommission["ValueOfBillOutForGovermentPaid"] = row.ValueOfBillOutForGovermentPaid;
                    rdRegistryCommission["CanKeepElectricForGovermentPaid"] = row.CanKeepElectricForGovermentPaid;
                    rdRegistryCommission["ValueOfKeepElectricForGovermentPaid"] = row.ValueOfKeepElectricForGovermentPaid;
                    rdRegistryCommission["RateAgencyPersonTypeForGovermentPaid"] = row.RateAgencyPersonTypeForGovermentPaid;
                    rdRegistryCommission["RateAgencyCompanyTypeForGovermentPaid"] = row.RateAgencyCompanyTypeForGovermentPaid;
                    rdRegistryCommission["AmountCommissionOfGovermentPaid"] = row.AmountCommissionOfGovermentPaid;

                    rdRegistryCommission["TotalItemRepeatBillOfCommissionSpacial"] = row.TotalItemRepeatBillOfCommissionSpecial;
                    rdRegistryCommission["ValueRepeatBillOfCommissionSpacial"] = row.ValueRepeatBillOfCommissionSpecial;
                    rdRegistryCommission["TotalItemRepeatBillOfCommissionSpacialFollowStandard"] = row.TotalItemRepeatBillOfCommissionSpecialFollowStandard;
                    rdRegistryCommission["ValueRepeatBillOfCommissionSpacialFollowStandard"] = row.ValueRepeatBillOfCommissionSpecialFollowStandard;
                    rdRegistryCommission["TotalBillItemOf75To90PercentForCommissionSpecial"] = row.TotalBillItemOf75To90PercentForCommissionSpecial;
                    rdRegistryCommission["RateOf75To90PercentForCommissionSpecial"] = row.RateOf75To90PercentForCommissionSpecial;
                    rdRegistryCommission["AmountCommission75To90PercentForCommissionSpecial"] = row.AmountCommission75To90PercentForCommissionSpecial;
                    rdRegistryCommission["TotalBillItemOfMorethan90PercentForCommissionSpecial"] = row.TotalBillItemOfMorethan90PercentForCommissionSpecial;
                    rdRegistryCommission["RateOfMorethan90PercentForCommissionSpecial"] = row.RateOfMorethan90PercentForCommissionSpecial;
                    rdRegistryCommission["AmountCommissionMorethan90PercentForCommissionSpecial"] = row.AmountCommissionMorethan90PercentForCommissionSpecial;

                    rdRegistryCommission["TotalItemSendBill"] = row.TotalItemSendBill;
                    rdRegistryCommission["RateOfSendBill"] = row.RateOfSendBill;
                    rdRegistryCommission["AmountOfSendBill"] = row.AmountOfSendBill;
                    rdRegistryCommission["TotalItemSendBillThreeMonth"] = row.TotalItemSendBillThreeMonth;
                    rdRegistryCommission["RateOfSendBillThreeMonth"] = row.RateOfSendBillThreeMonth;
                    rdRegistryCommission["AmountOfSendBillThreeMonth"] = row.AmountOfSendBillThreeMonth;
                    rdRegistryCommission["TransportOfCarPrice"] = row.TransportOfCarPrice;
                    rdRegistryCommission["TransportOfWaterPrice"] = row.TransportOfWaterPrice;
                    rdRegistryCommission["AmountAllCommission"] = row.AmountAllCommission;
                    rdRegistryCommission["FineSendMoneyLate"] = row.FineSendMoneyLate;

                    rdRegistryCommission["FinalAmountCommission"] = row.FinalAmountCommission;
                    rdRegistryCommission["TotalItemInclude20PercentForCommissionBase"] = row.TotalItemInclude20PercentForCommissionBase;
                    rdRegistryCommission["AmountInclude20PercentForCommissionBase"] = row.AmountInclude20PercentForCommissionBase;
                    rdRegistryCommission["Rate100PercentForAllKeep"] = row.Rate100PercentForAllKeep;                 
                    rdRegistryCommission["Tax7Percent"] = row.Vat7Percent;
                    rdRegistryCommission["IsPersonType"] = row.IsPersonType;
                    rdRegistryCommission["TaxAmount"] = row.TaxAmount;
                    dtRegistryCommission.Rows.Add(rdRegistryCommission);
                    dtRegistryCommission.AcceptChanges();

                    pPrintDate = new ReportParameter("PrintDate", row.PrintDate);
                    pBranchName = new ReportParameter("BranchName", String.Format("{0}, {1}", row.PEAName, row.PEABranch));
                    pAgencyName = new ReportParameter("AgencyName", String.Format("{0} : {1}", row.AgencyCode.Substring(row.AgencyCode.Length - ModuleConfigurationNames.AgentIdLength, ModuleConfigurationNames.AgentIdLength), row.AgencyName));
                    pPeriod = new ReportParameter("Period", row.BillMonth);
                    pTimeNo = new ReportParameter("TimeNo", row.TimeNo);
                    pRegisterDate = new ReportParameter("RegisterDate", row.RegisterDate);
                    pCalculateDate = new ReportParameter("CalculateDate", row.CalculateDate);

                }

                this.registryCommissionReport.Reset();
                this.registryCommissionReport.LocalReport.ReportEmbeddedResource = "PEA.BPM.AgencyManagementModule.Reports.CAB04_02.rdlc";
                ReportDataSource sRegistryCommissionReport = new ReportDataSource();
                ReportParameter[] ps = new ReportParameter[] { pPrintDate, pBranchName, pAgencyName, pPeriod, pTimeNo, pRegisterDate, pCalculateDate };

                sRegistryCommissionReport.Name = "DataSet2_tblRegistryCommission";
                sRegistryCommissionReport.Value = ds.Tables["tblRegistryCommission"];
                registryCommissionReport.LocalReport.SetParameters(ps);
                registryCommissionReport.LocalReport.DataSources.Add(sRegistryCommissionReport);
                this.registryCommissionReport.Width = this.Width;
                this.registryCommissionReport.Height = this.Height;

                if (printPreview)
                {
                    this.registryCommissionReport.RefreshReport();
                }
                else
                {
                    PrintUtility _printer = new PrintUtility(LocalSettingNames.AgencyPrinterName, ModuleConfigurationNames.ReportWidth, ModuleConfigurationNames.ReportHeight);
                    _printer.Export(this.registryCommissionReport.LocalReport);
                    _printer.CurrentPageIndex = 0;
                    _printer.Print();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
       }

    }
}

