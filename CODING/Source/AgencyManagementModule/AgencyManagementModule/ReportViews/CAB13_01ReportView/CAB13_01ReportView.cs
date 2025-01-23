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
using PEA.BPM.Architecture.PrintUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Infrastructure.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule
{
    [SmartPart]
    public partial class CAB13_01ReportView : UserControl, ICAB13_01ReportView
    {
        public CAB13_01ReportView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public CAB13_01ReportViewPresenter Presenter
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

        public void ShowReport(CAB13_01ConditionRptInfo conn, List<CAB13_01RptInfo> myDetail)
        {
            try
            {
                string month = StringConvert.GetMonthName(DateFormatter.PeriodToDateTime2(conn.Period.Trim()).Value.Month);
                string year = DateFormatter.ToYearThString(DateFormatter.PeriodToDateTime2(conn.Period.Trim()).Value);
                ReportParameter[] ps = new ReportParameter[2];
                ps[0] = new ReportParameter("pMonth", month);
                ps[1] = new ReportParameter("pYear", year);

                this.rptCAB13_01.Reset();
                this.rptCAB13_01.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                this.rptCAB13_01.LocalReport.ReportEmbeddedResource = "PEA.BPM.AgencyManagementModule.Reports.CAB13_01.rdlc";
                rptCAB13_01.LocalReport.SetParameters(ps);

                ReportDataSource sCAB13_01 = new ReportDataSource();
                sCAB13_01.Name = "PEA_BPM_AgencyManagementModule_Interface_BusinessEntities_CAB13_01RptInfo";
                sCAB13_01.Value = myDetail;
                rptCAB13_01.LocalReport.DataSources.Add(sCAB13_01);

                if (conn.PrintPreview)
                {
                    this.rptCAB13_01.RefreshReport();
                }
                else
                {
                    PrintUtility _printer = new PrintUtility(LocalSettingNames.AgencyPrinterName, ModuleConfigurationNames.ReportWidth, ModuleConfigurationNames.ReportHeight);
                    _printer.Export(this.rptCAB13_01.LocalReport);
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

