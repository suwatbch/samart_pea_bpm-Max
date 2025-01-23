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
using PEA.BPM.Architecture.PrintUtilities;
using Microsoft.Reporting.WinForms;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using System.Collections.Generic;
using PEA.BPM.Infrastructure.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule
{
    [SmartPart]
    public partial class PA_7034ReportView : UserControl, IPA_7034ReportView
    {
        public PA_7034ReportView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public PA_7034ReportViewPresenter Presenter
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

        /// <summary>
        /// 
        /// </summary>
        public void ShowReport(PA_7034HeaderReportInfo master, List<PA_7034DetailReportInfo> detailList)
        {
            try
            {
                this.rptPA_7034.Reset();
                this.rptPA_7034.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                this.rptPA_7034.LocalReport.ReportEmbeddedResource = "PEA.BPM.AgencyManagementModule.Reports.PA_7034.rdlc";
                ReportParameter[] ps = new ReportParameter[3];
                ps[0] = new ReportParameter("PrintDate", master.PrintDate);
                ps[1] = new ReportParameter("BranchName", master.BranchName);
                ps[2] = new ReportParameter("Period", master.Period);
                this.rptPA_7034.LocalReport.SetParameters(ps);

                ReportDataSource sReportDataSourceDetail = new ReportDataSource();
                sReportDataSourceDetail.Name = "PEA_BPM_AgencyManagementModule_Interface_BusinessEntities_PA_7034DetailReportInfo";
                sReportDataSourceDetail.Value = detailList ;
                this.rptPA_7034.LocalReport.DataSources.Add(sReportDataSourceDetail);


                if (master.PrintPreview)
                {
                    this.rptPA_7034.Dock = DockStyle.Fill;
                    this.rptPA_7034.RefreshReport();
                }
                else
                {
                    PrintUtility _printer = new PrintUtility(LocalSettingNames.AgencyPrinterName, ModuleConfigurationNames.ReportWidth, ModuleConfigurationNames.ReportHeight);
                    _printer.Export(this.rptPA_7034.LocalReport);
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

