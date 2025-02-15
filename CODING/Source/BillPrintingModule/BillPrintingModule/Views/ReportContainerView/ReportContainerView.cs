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
using System.Data;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Reporting.WinForms;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using PEA.BPM.Infrastructure.Interface;
using System.Collections.Generic;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.PrintUtilities;
using PEA.BPM.BillPrintingModule.Interface.Constants;
using PEA.BPM.BillPrintingModule.BillPrintingUtilities;
using System.Globalization;
using System.IO;
using System.Drawing.Printing;

namespace PEA.BPM.BillPrintingModule
{
    [SmartPart]
    public partial class ReportContainerView : UserControl, IReportContainerView
    {
        #region "Properties and Variables"

        /// <summary>
        /// These variables are used for display in report directly brought from reportParam
        /// </summary>
        string _reportName;
        string _approvedPerson;
        string _deliveryPlace;
        string _toWhom;
        string _saveNumber;
        string _billPeriod;
        string _billPeriodLog;
        string _portionNo;
        string _billControllerId;
        string _reportType; //bill delivery
        int? _printType;
        DateTime? _printDate;
        DateTime? _fromDate;
        DateTime? _toDate;
        DateTime? _dataReceiveDate;
        DateTime? _toDataReceiveDate;
        string _dataReceiveTime;
        string _toDataReceiveTime;

        public string DataReceiveTime
        {
            get { return _dataReceiveTime; }
            set 
            {
                _dataReceiveTime = string.Format("{0}:{1}:{2}", value.Substring(0, 2), value.Substring(2, 2), value.Substring(4, 2));
            }
        }

        public string ToDataReceiveTime
        {
            get { return _toDataReceiveTime; }
            set 
            {
                _toDataReceiveTime = string.Format("{0}:{1}:{2}", value.Substring(0, 2), value.Substring(2, 2), value.Substring(4, 2));
            }
        }

        public DateTime? FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }
        public DateTime? ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }
        public DateTime? DataReceiveDate
        {
            get { return _dataReceiveDate; }
            set { _dataReceiveDate = value; }
        }
        public DateTime? ToDataReceiveDate
        {
            get { return _toDataReceiveDate; }
            set { _toDataReceiveDate = value; }
        }

        public string ReportType
        {
            get { return _reportType; }
            set { _reportType = value; }
        }

        public int? PrintType
        {
            get { return _printType; }
            set { _printType = value; }
        }

        public string ReportName
        {
            get { return _reportName; }
            set { _reportName = value; }
        }
        public string ApprovedPerson
        {
            get { return _approvedPerson; }
            set { _approvedPerson = value; }
        }
        public string DeliveryPlace
        {
            get { return _deliveryPlace; }
            set { _deliveryPlace = value; }
        }

        public string ToWhom
        {
            get { return _toWhom; }
            set { _toWhom = value; }
        }

        public string SaveNumber
        {
            get { return _saveNumber; }
            set { _saveNumber = value; }
        }
        public string BillPeriod
        {
            get { return _billPeriod; }
            set { _billPeriod = value; }
        }
        public string BillPeriodLog
        {
            get { return _billPeriodLog; }
            set { _billPeriodLog = value; }
        }
        public string PortionNo
        {
            get { return _portionNo; }
            set { _portionNo = value; }
        }
        public string BillControllerId
        {
            get { return _billControllerId; }
            set { _billControllerId = value; }
        }
        public DateTime? PrintDate
        {
            get { return _printDate; }
            set { _printDate = value; }
        }
        public string SetLabel
        {
            get
            {
                return reportNameLabel.Text;
            }
            set
            {
                reportNameLabel.Text = value;
            }
        }
        
        List<ReportDailyPrint> _reportDailyPrint;
        List<ReportDailyUnprint> _reportDailyUnprint;
        List<ReportBillDelivery> _reportBillDelivery;
        List<ReportStreetRoute> _reportStreetRoute;
        List<ReportStreetRouteReceive> _reportStreetRouteReceive;
        List<ReportStreetRouteUnreceive> _reportStreetRouteUnreceive;
        List<ReportF16> _reportF16;
        List<ReportGrpInvSummary> _reportGrpInvSummary;
        List<ReportPrintByBank> _reportPrintGreenByBank;
        List<ReportGroupingCrossCheck> _reportGroupingCrossCheck;
        List<ReportBillingStatus> _reportBillingStatus;
        List<ReportCAUnprint> _reportCAUnprint;
        List<ReportDirectDebitStatus> _reportDirectDebitStatus;

        public List<ReportF16> ReportF16
        {
            get { return _reportF16; }
            set
            { 
                _reportF16 = value;
                if (_reportF16.Count > 0)
                    BindReportForF16Report();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
                
            }
        }
        public List<ReportBillDelivery> ReportBillDelivery
        {
            get { return _reportBillDelivery; }
            set 
            {
                _reportBillDelivery = value;
                if (_reportBillDelivery.Count > 0)
                    BindReportForBillDeliveryReport();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer = new ReportViewer();
                }
            }
        }
        public List<ReportDailyPrint> ReportDailyPrint
        {
            get { return _reportDailyPrint; }
            set 
            { 
                _reportDailyPrint = value;
                if (_reportDailyPrint.Count > 0)
                    BindReportForDailyPrintReport();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
            }
        }
        public List<ReportDailyUnprint> ReportDailyUnprint
        {
            get { return _reportDailyUnprint; }
            set
            {
                _reportDailyUnprint = value;
                if (_reportDailyUnprint.Count > 0)
                    BindReportForDailyUnprintReport();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
            }
        }

        public void BlankReport()
        {
            rdlcViewer.Reset();
            rdlcViewer.LocalReport.DataSources.Clear();
        }

        public void EnabledPrintButton(bool enable)
        {
            rdlcViewer.ShowPrintButton = enable;
            rdlcViewer.Refresh();
        }

        public List<ReportStreetRoute> ReportStreetRoute
        {
            get { return _reportStreetRoute; }
            set
            {
                _reportStreetRoute = value;
                if (_reportStreetRoute.Count > 0)
                    BindReportForStreetRouteReport();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
            }
        }
        public List<ReportStreetRouteReceive> ReportStreetRouteReceive
        {
            get { return _reportStreetRouteReceive; }
            set
            {
                _reportStreetRouteReceive = value;
                if (_reportStreetRouteReceive.Count > 0)
                    BindReportForStreetRouteReceiveReport();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
            }
        }
        public List<ReportStreetRouteUnreceive> ReportStreetRouteUnreceive
        {
            get { return _reportStreetRouteUnreceive; }
            set
            {
                _reportStreetRouteUnreceive = value;
                if (_reportStreetRouteUnreceive.Count > 0)
                    BindReportForStreetRouteUnreceiveReport();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
            }
        }

        //new added. March, 17 '08
        public List<ReportGrpInvSummary> ReportGrpInvSummary
        {
            get { return _reportGrpInvSummary; }
            set
            {
                _reportGrpInvSummary = value;
                if (_reportGrpInvSummary.Count > 0)
                    BindReportGrpInvSummary();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
            }
        }

        public List<ReportPrintByBank> ReportPrintGreenByBank
        {
            get { return _reportPrintGreenByBank; }
            set
            {
                _reportPrintGreenByBank = value;
                if (_reportPrintGreenByBank.Count > 0)
                    BindReportPrintGreenByBank();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
            }
        }

        public List<ReportGroupingCrossCheck> ReportGroupingCrossCheck
        {
            get { return _reportGroupingCrossCheck; }
            set
            {
                _reportGroupingCrossCheck = value;
                if (_reportGroupingCrossCheck.Count > 0)
                    BindReportGroupingCrossCheck();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
            }
        }

        public List<ReportBillingStatus> ReportBillingStatus
        {
            get { return _reportBillingStatus; }
            set
            {
                _reportBillingStatus = value;
                if (_reportBillingStatus.Count > 0)
                    BindReportBillingStatus();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
            }
        }

        public List<ReportCAUnprint> ReportCAUnprint
        {
            get { return _reportCAUnprint; }
            set
            {
                _reportCAUnprint = value;
                if (_reportCAUnprint.Count > 0)
                    BindReportCAUnprint();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
            }
        }

        public List<ReportDirectDebitStatus> ReportDirectDebitStatus
        {
            get { return _reportDirectDebitStatus; }
            set
            {
                _reportDirectDebitStatus = value;
                if (_reportDirectDebitStatus.Count > 0)
                    BindReportDirectDebitStatus();
                else
                {
                    MessageBox.Show("��辺�����������͡��§ҹ ��سҵ�Ǩ�ͺ���͹䢡���͡��§ҹ�ա����", "��辺������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rdlcViewer.Reset();
                }
            }
        }

        
        public void OnWaitCursor(bool set)
        {
            if (set)
                this.Cursor = Cursors.WaitCursor;
            else
                this.Cursor = Cursors.Default;
        }


        #endregion

        #region "Code Generated"
        public ReportContainerView()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public ReportContainerViewPresenter Presenter
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
            this.rdlcViewer.Refresh();
        }
        #endregion

        #region "Event Handling"

        private void closeReportLabel_Click(object sender, EventArgs e)
        {
            _presenter.OnCloseView();
        }

        #endregion

        #region "Function"

        public void SetLabelMethod(string text)
        {
            reportNameLabel.Text = text;
        }

        private void BindReportForDailyPrintReport()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportDailyPrint";
                sReportDataSource.Value = _reportDailyPrint;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);
                string prefix = null;
                if (_printType == 0)
                    prefix = "A";
                else if (_printType == 1 || _printType == 15 || _printType == 5)
                    prefix = "B";
                else
                    prefix = "G";

                ReportParameter[] rParam = new ReportParameter[3];
                rParam[0] = new ReportParameter("parPrintDate", _printDate.Value.ToString("ddMMyyyy", new CultureInfo("th-TH")));
                rParam[1] = new ReportParameter("parPrefix", prefix);
                rParam[2] = new ReportParameter("parPrintType", _printType.ToString());
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                //rdlcViewer.RefreshReport();
                OnWaitCursor(false);
            }
            catch (Exception ex)
            {
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void BindReportForBillDeliveryReport()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.LocalReport.DataSources.Clear();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportBillDelivery";
                sReportDataSource.Value = _reportBillDelivery;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);
                DateTime sentDt = _reportBillDelivery[0].SentDt == null ? DateTime.Now : _reportBillDelivery[0].SentDt.Value;

                ReportParameter[] rParam = new ReportParameter[8];
                rParam[0] = new ReportParameter("parSaveNumber", _saveNumber);
                rParam[1] = new ReportParameter("parDeliveryPlace", _deliveryPlace);
                rParam[2] = new ReportParameter("parPrintBranchName", Session.Branch.Name);
                rParam[3] = new ReportParameter("parBillPeriodLog", _billPeriodLog);
                rParam[4] = new ReportParameter("parPeriod", _billPeriod.Substring(4,2) + "/" + _billPeriod.Substring(0,4));
                rParam[5] = new ReportParameter("parToWhom", _toWhom);
                rParam[6] = new ReportParameter("parSentDate", sentDt.Day + " " + StringConvert.GetMonthName(sentDt.Month) +
                                                            " "+ sentDt.ToString("yyyy", new CultureInfo("th-TH")));
                rParam[7] = new ReportParameter("parPrintType", _printType.ToString());
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                //rdlcViewer.RefreshReport();
                OnWaitCursor(false);
            }
            catch (Exception ex)
            {
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindReportForDailyUnprintReport()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportDailyUnprint";
                sReportDataSource.Value = _reportDailyUnprint;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[3];
                if (_billPeriod == null)
                    rParam[0] = new ReportParameter("parBillPeriod", "00");
                else
                    rParam[0] = new ReportParameter("parBillPeriod", _billPeriod);

                if (_portionNo == null || _portionNo == "")
                    rParam[1] = new ReportParameter("parPortionNo", "������");
                else
                    rParam[1] = new ReportParameter("parPortionNo", _portionNo);

                if (_billControllerId == null || _billControllerId == "")
                    rParam[2] = new ReportParameter("parBillControllerId", "������");
                else
                    rParam[2] = new ReportParameter("parBillControllerId", _billControllerId);

                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                //rdlcViewer.RefreshReport();
                OnWaitCursor(false);
            }
            catch (Exception ex)
            {
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindReportForStreetRouteReport()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportStreetRoute";
                sReportDataSource.Value = _reportStreetRoute;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[1];
                rParam[0] = new ReportParameter("parBillPeriod", _billPeriod);
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                //rdlcViewer.RefreshReport();

                OnWaitCursor(false);
            }
            catch (Exception ex)
            {
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindReportForStreetRouteReceiveReport()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportStreetRouteReceive";
                sReportDataSource.Value = _reportStreetRouteReceive;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[4];                    
                rParam[0] = new ReportParameter("parBillPeriod", _dataReceiveDate.Value.ToString("ddMMyyyy", new CultureInfo("th-TH")));
                rParam[1] = new ReportParameter("parToBillPeriod", _toDataReceiveDate.Value.ToString("ddMMyyyy", new CultureInfo("th-TH")));
                rParam[2] = new ReportParameter("parTime", _dataReceiveTime);
                rParam[3] = new ReportParameter("parToTime", _toDataReceiveTime);
                
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                //rdlcViewer.RefreshReport();
                OnWaitCursor(false);
            }
            catch (Exception ex)
            {
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindReportForStreetRouteUnreceiveReport()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;                
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportStreetRouteUnreceive";
                sReportDataSource.Value = _reportStreetRouteUnreceive;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[1];
                rParam[0] = new ReportParameter("parBillPeriod", _billPeriod);
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                //rdlcViewer.RefreshReport();
                OnWaitCursor(false);
            }
            catch (Exception ex)
            {
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindReportForF16Report()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportF16";
                sReportDataSource.Value = _reportF16;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[1];
                rParam[0] = new ReportParameter("parBillPeriod", _billPeriod);
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                //rdlcViewer.RefreshReport();
                OnWaitCursor(false);
            }
            catch (Exception ex)
            {
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //new added. March, 17 '08
        private void BindReportGrpInvSummary()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportGrpInvSummary";
                sReportDataSource.Value = _reportGrpInvSummary;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[2];
                if(_reportGrpInvSummary.Count > 0)
                    rParam[0] = new ReportParameter("parGroupingDate", _reportGrpInvSummary[0].GroupDate.Replace("/", ""));
                else
                    rParam[0] = new ReportParameter("parGroupingDate", "000000");

                rParam[1] = new ReportParameter("parBranchName", string.Format("{0}  {1}", Session.Branch.Id, Session.Branch.Name));
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                //rdlcViewer.RefreshReport();
                OnWaitCursor(false);
            }
            catch (Exception ex)
            {                
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindReportPrintGreenByBank()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportPrintByBank";
                sReportDataSource.Value = _reportPrintGreenByBank;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[4];
                rParam[0] = new ReportParameter("parPrintBranchId", Session.Branch.Id);
                rParam[1] = new ReportParameter("parPrintBranchName", Session.Branch.Name);
                rParam[2] = new ReportParameter("parFromDate", _fromDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                rParam[3] = new ReportParameter("parToDate", _toDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                //rdlcViewer.RefreshReport();
                OnWaitCursor(false);
            }
            catch (Exception ex)
            {              
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindReportBillingStatus()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportBillingStatus";
                sReportDataSource.Value = _reportBillingStatus;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[2];
                rParam[0] = new ReportParameter("parPrintType", _printType.ToString());
                rParam[1] = new ReportParameter("parBillPred", _billPeriod.Substring(4,2)+ "/"+ _billPeriod.Substring(0, 4));
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                OnWaitCursor(false);
            }
            catch (Exception ex)
            {              
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindReportDirectDebitStatus()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportDirectDebitStatus";
                sReportDataSource.Value = _reportDirectDebitStatus;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[1];
                rParam[0] = new ReportParameter("parPeriod", _billPeriod);
                //rParam[1] = new ReportParameter("parBillPred", _billPeriod.Substring(4, 2) + "/" + _billPeriod.Substring(0, 4));
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                OnWaitCursor(false);
            }
            catch (Exception ex)
            {
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindReportCAUnprint()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportCAUnprint";
                sReportDataSource.Value = _reportCAUnprint;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                //ReportParameter[] rParam = new ReportParameter[2];
                //rParam[0] = new ReportParameter("parPrintType", _printType);
                //rParam[1] = new ReportParameter("parBillPred", _billPeriod.Substring(4, 2) + "/" + _billPeriod.Substring(0, 4));
                //rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                OnWaitCursor(false);
            }
            catch (Exception ex)
            {               
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindReportGroupingCrossCheck()
        {
            try
            {
                OnWaitCursor(true);
                rdlcViewer.Reset();
                rdlcViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                rdlcViewer.LocalReport.ReportEmbeddedResource = _reportName;
                ReportDataSource sReportDataSource = new ReportDataSource();
                sReportDataSource.Name = "PEA_BPM_BillPrintingModule_Interface_BusinessEntities_ReportGroupingCrossCheck";
                sReportDataSource.Value = _reportGroupingCrossCheck;
                rdlcViewer.LocalReport.DataSources.Add(sReportDataSource);

                ReportParameter[] rParam = new ReportParameter[3];
                rParam[0] = new ReportParameter("parBillPeriod", _billPeriod);
                rParam[1] = new ReportParameter("parPrintBranchId", Session.Branch.Id);
                rParam[2] = new ReportParameter("parPrintBranchName", Session.Branch.Name);
                rdlcViewer.LocalReport.SetParameters(rParam);
                rdlcViewer.SetDisplayMode(DisplayMode.PrintLayout);
                rdlcViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                //rdlcViewer.RefreshReport();

                OnWaitCursor(false);
            }
            catch (Exception ex)
            {               
                OnWaitCursor(false);
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.BLAN, ex);
                //MessageBox.Show(MessageBoxText.MsgGeneralError + ex.Message, MessageBoxText.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        #endregion    


        //private void PrintCoverPage(ReportViewer viewer)
        //{
        //    LocalSettingHelper hp = LocalSettingHelper.Instance();
        //    PrintUtility _printer = new PrintUtility(hp.Read(LocalSettingNames.PrinterName).ToString());
        //    //PrintUtility _printer = new PrintUtility("Microsoft Office Document Image Writer");
        //    _printer.Export(viewer.LocalReport);
        //    _printer.CurrentPageIndex = 0;
        //    _printer.Print();
        //}


  

        
    }
}
