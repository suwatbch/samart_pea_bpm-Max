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
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Reporting.WinForms;

using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Utilities;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Architecture.PrintUtilities;
using PEA.BPM.Infrastructure.Interface.Constants;


namespace PEA.BPM.AgencyManagementModule.ReportViews
{
    [SmartPart]
    public partial class CAB01_01ReportView : UserControl, ICAB01_01ReportView
    {
        private List<BillBookInfoMasterReport> _billBookMasterList;
        private BillBookInfoMasterReport _billBookMasterInfo = null;
        private List<BillBookInfoDetailReport> _billBookDetailList;
       
        public BillBookInfoMasterReport SelectedAgentInfo
        {
            get { return _billBookMasterInfo; }

        }

        public CAB01_01ReportView()
        {
            InitializeComponent();
                      
        }

        public List<BillBookInfoMasterReport> ResultBillBookPrintMasterDisplay
        {
            set
            {
                _billBookMasterList = value;
                LoadAgentSearchResultToGrid();
            }
        }


        public List<BillBookInfoDetailReport> ResultBillBookPrintDetailDisplay
        {
            set
            {
                _billBookDetailList = value;
                LoadAgentSearchResultToGrid();
            }
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public CAB01_01ReportViewPresenter Presenter
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

        public void LoadAgentSearchResultToGrid()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowReport(BillBookInfoMasterReport masterReport, bool printPreview)
        {
            try
            {
                this.rdlcViewer.Reset();
                this.rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                this.rdlcViewer.LocalReport.ReportEmbeddedResource = "PEA.BPM.AgencyManagementModule.Reports.CAB01_01.rdlc";             
                ReportParameter[] ps = new ReportParameter[19];
                ps[0] = new ReportParameter("PrintDate", masterReport.PrintDate);
                ps[1] = new ReportParameter("BranchName", masterReport.BranchName);
                ps[2] = new ReportParameter("Period", masterReport.Period);
                ps[3] = new ReportParameter("ReceiveNo", masterReport.ReceiveTime);
                ps[4] = new ReportParameter("BillBookNo", masterReport.BillBookId);
                ps[5] = new ReportParameter("AgencyName", String.Format(" {0}, {1}", masterReport.AgencyID, masterReport.AgencyName));
                ps[6] = new ReportParameter("TotalAsset", masterReport.TotalAsset.Value.ToString("#,##0.00"));
                ps[7] = new ReportParameter("TotalElectricReceive", masterReport.TotalElectricReceive.Value.ToString("#,##0.00"));
                ps[8] = new ReportParameter("IntownReceive", masterReport.IntownReceive.Value.ToString("#,##0.00"));
                ps[9] = new ReportParameter("TentativeDate", masterReport.TentativeDate);
                ps[10] = new ReportParameter("CreatorName", masterReport.CreatorName);
                ps[11] = new ReportParameter("TotalBillReceive", masterReport.TotalBillReceive.Value.ToString());
                ps[12] = new ReportParameter("BillReturnedDate", masterReport.BillReturnedDate);
                ps[13] = new ReportParameter("TotalPutBill",  masterReport.TotalPutBill.Value.ToString());
                ps[14] = new ReportParameter("TotalPutBillElectric", masterReport.TotalPutBillElectric.Value.ToString("#,##0.00"));
                ps[15] = new ReportParameter("BillKeeperName", masterReport.BillKeeperName);
                ps[16] = new ReportParameter("AdvPayAmount", masterReport.AdvPayAmount.Value.ToString("#,##0.00"));
                ps[17] = new ReportParameter("CreatorName", masterReport.CreatorName);
                ps[18] = new ReportParameter("BookLot", masterReport.BookLot.Value.ToString().PadLeft(2,'0'));
                this.rdlcViewer.LocalReport.SetParameters(ps);
              
                ReportDataSource sReportDataSourceDetail = new ReportDataSource();                              
                sReportDataSourceDetail.Name = "PEA_BPM_AgencyManagementModule_Interface_BusinessEntities_BillBookDetailReportListInfo";
                sReportDataSourceDetail.Value = masterReport.BillReportList;
                this.rdlcViewer.LocalReport.DataSources.Add(sReportDataSourceDetail);
               
             
                if (printPreview)
                {
                    this.rdlcViewer.Dock = DockStyle.Fill;
                    this.rdlcViewer.RefreshReport();
                }
                else
                {
                    PrintUtility _printer = new PrintUtility(LocalSettingNames.AgencyPrinterName, ModuleConfigurationNames.ReportWidth, ModuleConfigurationNames.ReportHeight);
                    _printer.Export(this.rdlcViewer.LocalReport);
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

