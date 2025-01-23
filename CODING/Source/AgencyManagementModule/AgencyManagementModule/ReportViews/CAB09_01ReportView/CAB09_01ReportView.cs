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
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using System.Data;
using PEA.BPM.AgencyManagementModule.DataSet;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.PrintUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Infrastructure.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.ReportViews
{
    [SmartPart]
    public partial class CAB09_01ReportView : UserControl, ICAB09_01ReportView
    {
        public CAB09_01ReportView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public CAB09_01ReportViewPresenter Presenter
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

        public void ShowReport(KeepMoneyPlaneHeaderInfoReport _myMaster, List<CAB09_01DetailReportInfo> _myDetail)
        {
            try
            {                                                            
                ReportParameter pPrintDate = new ReportParameter("PrintDate", _myMaster.PrintDate);
                ReportParameter pBranchName = new ReportParameter("BranchName", String.Format("{0}, {1}", _myMaster.BranchMasterName, _myMaster.BranchMasterId));

                string year = _myMaster.Period.Substring(0, 4);
                int _monthNo = Convert.ToInt16(_myMaster.Period.Substring(4, 2));
                ReportParameter pPeriod = new ReportParameter("Period", String.Format(" {0} {1}", StringConvert.GetMonthName(_monthNo), year));           
               
                ReportParameter[] ps = new ReportParameter[] {pPrintDate, pBranchName, pPeriod};
                
                this.IssueKeepReportView.Reset();
                this.IssueKeepReportView.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                this.IssueKeepReportView.LocalReport.ReportEmbeddedResource = "PEA.BPM.AgencyManagementModule.Reports.CAB09_01.rdlc";
                IssueKeepReportView.LocalReport.SetParameters(ps);

                ReportDataSource sRev701462Report = new ReportDataSource();
                sRev701462Report.Name = "PEA_BPM_AgencyManagementModule_Interface_BusinessEntities_CAB09_01DetailReportInfo";
                sRev701462Report.Value = _myDetail;
                IssueKeepReportView.LocalReport.DataSources.Add(sRev701462Report);
               

                if (_myMaster.PrintPreview)
                {
                    this.IssueKeepReportView.RefreshReport();
                }
                else
                {
                    PrintUtility _printer = new PrintUtility(LocalSettingNames.AgencyPrinterName, ModuleConfigurationNames.ReportWidth, ModuleConfigurationNames.ReportHeight);
                    _printer.Export(this.IssueKeepReportView.LocalReport);
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

