using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using System.Collections;

namespace PEA.BPM.AgencyManagementModule
{
    public class CommissionManagementController : WorkItemController
    {
        //private List<CollectionPortionDetail> _portionSearch = new List<CollectionPortionDetail>();
        private IAgencyCommonService _agencyCommonService;
        private ICommissionMgtService _commissionMgtService;
        private ICommissionManagementView _commissionManagementView;
        private WindowSmartPartInfo _modalProperty;
        private string _activeCommissionId;
        private FeeBaseElement _commissionRate;
        private CommissionInfo _comInfo;
        private BooniesCommissionInfo _helpInfo;
        private bool _printPreview = false;
        private bool _enableFine = true;

        [InjectionConstructor]
        public CommissionManagementController([ServiceDependency] 
                IAgencyCommonService agencyCommonService,
                ICommissionMgtService commissionMgtService
            )
        {
            _agencyCommonService = agencyCommonService;
            _commissionMgtService = commissionMgtService;

            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.MaximizeBox = false;
            _modalProperty.MinimizeBox = false;
            _modalProperty.Modal = true;
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            //WorkItem.State["AgentPlanning"] = _portionSearch;           
            if (WorkItem.Items.Contains("ICommissionManagementView"))
            {
                _commissionManagementView = WorkItem.Items.Get<ICommissionManagementView>("ICommissionManagementView");
            }
            else
            {
                _commissionManagementView = WorkItem.Items.AddNew<CommissionManagementView>("ICommissionManagementView");
            }
            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_commissionManagementView);

           // ((UserControl)_commissionManagementView).ParentForm.Text = "จัดการค่าตอบแทนและค่าปรับ - Billing & Payment Management";
            SetWindowsTitle("จัดการค่าตอบแทนและค่าปรับ");

            ShellWaitCursor(false);

        }

        #region subscription event

        private void CalculateCommission(BookSearchInfo searchInfo)
        {
            TravelHelpRateConditionInfo spcCondition = new TravelHelpRateConditionInfo();
            _helpInfo = new BooniesCommissionInfo();
            //set parameter to get special help rate
            spcCondition.IsReprint = searchInfo.IsReprint;
            spcCondition.AgencyId = searchInfo.AgentId.PadLeft(12, '0');
            spcCondition.BookPeriod = searchInfo.BillPeriod;
            spcCondition.CreateDate = searchInfo.CreateDate;
            spcCondition.CalculateDate = Session.BpmDateTime.Now;
            spcCondition.BranchId = Session.Branch.Id;
            searchInfo.BranchId = Session.Branch.Id;
            _commissionRate = _commissionMgtService.LoadCommissionRate(searchInfo);
            _comInfo = _commissionMgtService.CalculateCommissionAndFine(searchInfo, _commissionRate, _helpInfo);

            _commissionManagementView.CommissionBaseList = _comInfo.BaseCommission;
            _commissionManagementView.SpecialCommission = _comInfo.SpecialCommission;
            _commissionManagementView.InvoiceCommission = _comInfo.InvoiceCommission;
            _commissionManagementView.Fine = _comInfo.FineInfo;
            _commissionManagementView.AdvPaymentAmount = _commissionMgtService.GetAndDisplayAdvPaymentAmount(searchInfo);
            _commissionManagementView.PlusSpecialMoney();
            //_commissionManagementView.PriorTax = 0; // find calculate t //_comInfo.TaxInfo.TotalTax; //tax later      
            _commissionManagementView.VatRate = _comInfo.VatRate;
            _commissionManagementView.TaxRate = _comInfo.TaxRate ;
            _commissionManagementView.TravelHelpRate = _commissionMgtService.GetTravelHelpRate(spcCondition);
            _commissionManagementView.Recalculate();
            _commissionManagementView.SaveButton = true;
            _commissionManagementView.FocusRecordButton();
            _enableFine = true;
        }

        //[EventSubscription(EventTopicNames.LoadCommissionEvent, Thread = ThreadOption.UserInterface)]
        //public void LoadCommissionEventFiredHandler(object sender, EventArgs<BookSearchInfo> e)
        //{
        //    BookSearchInfo searchInfo = (BookSearchInfo)e.Data;
        //    CalculateCommission(searchInfo);
        //}

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }

        [EventSubscription(EventTopicNames.ClearActiveCommissionId, Thread = ThreadOption.UserInterface)]
        public void ClearActiveCommissionIdClickedHandler(object sender, EventArgs e)
        {
            _activeCommissionId = "";
        }

        [EventSubscription(EventTopicNames.DisableFineEvent, Thread = ThreadOption.UserInterface)]
        public void DisableFineEventHandler(object sender, EventArgs e)
        {
            
            _enableFine = false;
            _commissionManagementView.ResetFine();           
        }

        [EventSubscription(EventTopicNames.CommissionPrintPreview, Thread = ThreadOption.UserInterface)]
        public void CommissionPrintPreviewHandler(object sender, EventArgs<bool> e)
        {
            _printPreview = (bool)e.Data;
        }

        [EventSubscription(EventTopicNames.AgencyCommissionSaveButton, Thread = ThreadOption.UserInterface)]
        public void AgencyCommissionSaveButtonClickedHandler(object sender, EventArgs<HelpOfferInfo> e)
        {
            try
            {
                HelpOfferInfo saveCommissionInfo = e.Data;
                saveCommissionInfo.RunningBranchId = Session.Branch.Id;
                saveCommissionInfo.SaveInfo = _comInfo.SaveInfo;
                saveCommissionInfo.EnableFine = _enableFine;
                saveCommissionInfo.FineList = _comInfo.FineInfo.FineList;
                saveCommissionInfo.BookList = _comInfo.HelpInfo.BookList;
                _activeCommissionId = _commissionMgtService.SaveCommission(null, saveCommissionInfo);
                //LoadCommissionIDToReportActivated(cmId);
                //_commissionManagementView.ClearScreen();


                //--Start--RealTime-AG: Compensation BPM-->SAP -----
                if (Session.IsNetworkConnectionAvailable)
                {
                    if (!Session.Branch.OnlineConnection)
                    {
                        Authorization.SignalSyncup(PEA.BPM.Infrastructure.Interface.Constants.LocalSettingNames.DL130_UPLOAD_AG_COMPENSATION_BATCH);
                    }
                    else
                    {
                        Authorization.SignalExport(PEA.BPM.Infrastructure.Interface.Constants.LocalSettingNames.DL008_EXPORT_AG_TO_SAP_BATCH, Session.Branch.Id, Session.User.Id);
                    }
                }
                //--End----RealTime-AG------------------------------


                DialogResult dlg = MessageBox.Show(null, "ทำการบันทึกเรียบร้อยแล้ว กรุณากดปุ่ม OK เพื่อทำการพิมพ์รายงาน", "การบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dlg == DialogResult.OK)
                {
                    CommissionVoucherConditionPrintReport commissionCondition = _commissionManagementView.GetAgencyCommissionScreenInfo(_activeCommissionId);
                    //BroadCastCommissionScreenActivated(cv);
                    commissionCondition.PrintPreview = _printPreview;
                    LoadCAB04_01ReportClicked(commissionCondition);
                    LoadCAB04_02ReportClicked(commissionCondition);
                    LoadCAB04_03ReportClicked(commissionCondition);
                    MessageBox.Show(null, "พิมพ์รายงานค่าตอบแทนเสร็จเรียบร้อย", "สถานะการพิมพ์", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _commissionManagementView.CalculateButton = false;
                }

                _commissionManagementView.PrintButton = true;
                //_commissionManagementView.ClearScreen(false);
            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.AGENCY, "คำนวณค่าตอบแทนและค่าปรับ", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
                
        }

        private void ReprintCommissionReport(string commissionId, bool showMsg)
        {
            string comId = "";

            if (commissionId != "")
            {
                comId = commissionId;
            }
            else if (_activeCommissionId != "")
                comId = _activeCommissionId;

            if (showMsg)
            {
                DialogResult dlg = MessageBox.Show(null, "พิมพ์ซ่อมรายงานค่าตอบแทน กรุณากดปุ่ม OK", "การบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dlg == DialogResult.OK)
                {
                    CommissionVoucherConditionPrintReport commissionCondition = _commissionManagementView.GetAgencyCommissionScreenInfo(comId);

                    commissionCondition.PrintPreview = _printPreview;
                    //BroadCastCommissionScreenActivated(cv);
                    LoadCAB04_01ReportClicked(commissionCondition);
                    LoadCAB04_02ReportClicked(commissionCondition);
                    LoadCAB04_03ReportClicked(commissionCondition);
                    
                    //_commissionManagementView.ClearScreen(false);
                }
                else
                {
                    _commissionManagementView.CalculateButton = false;
                    //_commissionManagementView.ClearCalculation();
                }
            }
            else
            {
                CommissionVoucherConditionPrintReport commissionCondition = _commissionManagementView.GetAgencyCommissionScreenInfo(comId);
                commissionCondition.PrintPreview = _printPreview;
                //BroadCastCommissionScreenActivated(cv);
                LoadCAB04_01ReportClicked(commissionCondition);
                LoadCAB04_02ReportClicked(commissionCondition);
                LoadCAB04_03ReportClicked(commissionCondition);
            }
        }

        [EventSubscription(EventTopicNames.ReprintCommissionReport, Thread = ThreadOption.UserInterface)]
        public void ReprintCommissionReportClickedHandler(object sender, EventArgs<string> e)
        {
            try
            {
                if (Authorization.IsAuthorized(SecurityNames.ReprintCommissionReport, true))
                {
                    ReprintCommissionReport(e.Data, true);
                }
            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventPublication(EventTopicNames.BroadCastCommissionScreen, PublicationScope.Global)]
        public event EventHandler<EventArgs> BroadCastCommissionScreenHandler;
        public void BroadCastCommissionScreenActivated(CommissionVoucherConditionPrintReport cv)
        {
            if (BroadCastCommissionScreenHandler != null)
                BroadCastCommissionScreenHandler(this, new EventArgs<CommissionVoucherConditionPrintReport>(cv));
        }

        //This Event use for Load Registry Commission Report Screen
        //Create By Chettha Pattananitisak Date 29/03/2007 Time 17:35
        [EventPublication(EventTopicNames.ShowCAB04_02Report, PublicationScope.Global)]
        public event EventHandler<EventArgs> LoadCAB04_02ReportHandler;
        public void LoadCAB04_02ReportClicked(CommissionVoucherConditionPrintReport conditionVoucherInfo)
        {
            if (LoadCAB04_02ReportHandler != null)
                LoadCAB04_02ReportHandler(this, new EventArgs<CommissionVoucherConditionPrintReport>(conditionVoucherInfo));
        }

        //This Event use for Show Quanlity and Amount Of Item Of Bill in BillBook compair between Bill Out With Bill that can keep money  
        //Create By Chettha Pattananitisak Date 18/04/2007 Time 18:30
        [EventPublication(EventTopicNames.ShowCAB04_03Report, PublicationScope.Global)]
        public event EventHandler<EventArgs> LoadCAB04_03ReportHandler;
        public void LoadCAB04_03ReportClicked(CommissionVoucherConditionPrintReport conditionVoucherInfo)
        {
            if (LoadCAB04_03ReportHandler != null)
                LoadCAB04_03ReportHandler(this, new EventArgs<CommissionVoucherConditionPrintReport>(conditionVoucherInfo));
        }

        //This Event use for Load Registry Commission Report Screen
        //Create By Chettha Pattananitisak Date 25/03/2007 Time 01:35
        [EventPublication(EventTopicNames.ShowCAB04_01Report, PublicationScope.Global)]
        public event EventHandler<EventArgs> LoadCAB04_01ReportHandler;
        public void LoadCAB04_01ReportClicked(CommissionVoucherConditionPrintReport conditionVoucherInfo)
        {
            if (LoadCAB04_01ReportHandler != null)
                LoadCAB04_01ReportHandler(this, new EventArgs<CommissionVoucherConditionPrintReport>(conditionVoucherInfo));
        }

        //This Event use for Load Customer don't paid Money by don't pass Agentcy Report Screen
        //Create By Chettha Pattananitisak Date 10/04/2007 Time 11:30
        [EventPublication(EventTopicNames.ShowCAB03_01Report, PublicationScope.Global)]
        public event EventHandler<EventArgs> LoadCAB03_01ReportHandler;
        public void LoadCAB03_01ReportClicked(CommissionVoucherConditionPrintReport conditionVoucherInfo)
        {
            if (LoadCAB03_01ReportHandler != null)
                LoadCAB03_01ReportHandler(this, new EventArgs<CommissionVoucherConditionPrintReport>(conditionVoucherInfo));
        }

        [EventPublication(EventTopicNames.ShowCAB03_03Report, PublicationScope.Global)]
        public event EventHandler<EventArgs> LoadCAB03_03ReportHandler;
        public void LoadCAB03_03ReportClicked(CommissionVoucherConditionPrintReport conditionVoucherInfo)
        {
            if (LoadCAB03_03ReportHandler != null)
                LoadCAB03_03ReportHandler(this, new EventArgs<CommissionVoucherConditionPrintReport>(conditionVoucherInfo));
        }


        [EventSubscription(EventTopicNames.CommissionAgentIdTextCommitted, Thread = ThreadOption.UserInterface)]
        public void CommissionAgentIdTextCommittedHandler(object sender, EventArgs<string> e)
        {
            try
            {
                WaitingFormHelper.ShowWaitingForm();
                string agentId = (string)e.Data;
                AgentInfo agentInfo = _commissionMgtService.GetAgentHelpInformation(agentId);
                WaitingFormHelper.HideWaitingForm();
                if (!string.Equals(Session.Branch.Id, agentInfo.TechBranchId, StringComparison.CurrentCultureIgnoreCase))
                    agentInfo = null;
                _commissionManagementView.AgentInfo = agentInfo;
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาตัวแทน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.CommissionBookCreateDateDropDown, Thread = ThreadOption.UserInterface)]
        public void CommissionBookCreateDateDropDownHandler(object sender, EventArgs<BookSearchInfo> e)
        {
            try
            {
                BookSearchInfo searchInfo = (BookSearchInfo)e.Data;
                List<string> createDateList = _commissionMgtService.GetListOfCreatedDate(searchInfo);
                _commissionManagementView.BookCreateDateList = createDateList;
            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }


        [EventSubscription(EventTopicNames.BookCreateDate, Thread = ThreadOption.UserInterface)]
        public void BookCreateDateSelectedHandler(object sender, EventArgs<BookSearchInfo> e)
        {
            try 
            {
                WaitingFormHelper.ShowWaitingForm();
                BookSearchInfo searchCon = (BookSearchInfo)e.Data;
                List<string> bookList = new List<string>();
                bookList = _commissionMgtService.BookListByCreateDate(searchCon);
                BookSearchInfo bookSearch = (BookSearchInfo)e.Data;
                FeeBaseElement feeBase = new FeeBaseElement();
                string returnDt = bookList[0];
                string bookRange = bookList[1];
                _commissionManagementView.BookRange = bookRange;
                _commissionManagementView.ReturnDate = returnDt;
                //no ready billbook to calculate commission
                if (bookRange == null) return;
                if (!e.Data.AllowCalculate) return;
                feeBase = _commissionMgtService.GetFeeBase(Session.Branch.Id);

                if (!_commissionMgtService.IsBookAvailable(searchCon))
                {
                    WaitingFormHelper.HideWaitingForm();
                    MessageBox.Show(null, "สมุดจ่ายบิลไม่อยู่ในสถานะพร้อมในการคิดค่าตอบแทน \nสมุดจ่ายบิลอาจจะถูกตัดชำระไม่ครบทุกเล่ม", "กรุณาตรวจสอบสถานะสมุดจ่ายบิล", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //_commissionManagementView.ClearScreen(true);
                    _commissionManagementView.PrintButton = false;
                }//verify create date that has commission Id - already calculated commission                            
                else if (!_commissionMgtService.IsBookAlreadyPaid(searchCon))
                {
                    WaitingFormHelper.HideWaitingForm();
                    MessageBox.Show(null, "สมุดจ่ายบิลไม่อยู่ในสถานะพร้อมในการคิดค่าตอบแทน \nกรุณาชำระเงินส่วนที่เหลือของสมุดจ่ายบิลให้ครบทุกเล่ม", "กรุณาตรวจสอบสถานะสมุดจ่ายบิล", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //_commissionManagementView.ClearScreen(true);
                    _commissionManagementView.PrintButton = false;
                }
                else
                    if (_commissionMgtService.IsCommissionCalculated(searchCon))
                    {
                        List<CalculatedCommissionInfo> calList = _commissionMgtService.GetCalCountOfPeriodByAgentId(searchCon);                    //DialogResult dlg = MessageBox.Show(null, "ได้คำนวณค่าตอบแทนของงวดนี้ไปแล้ว\nคุณต้องการพิมพ์รายงานซ่อมใช่หรือไม่", "พิมพ์ซ่อม", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        List<CommissionHistoryInfo> comList = _commissionMgtService.GetCommissionHistory(searchCon);
                        bookSearch.IsReprint = true;
                        //just populate commission screen                       
                        e.Data.IsReprint = true;
                        CalculateCommission(bookSearch);
                        //_commissionManagementView.VatRate = feeBase.VatRate;
                        _commissionManagementView.SetTravelHelpRateReadOnly(bookSearch.IsReprint);
                        _commissionManagementView.CalculateButton = false;
                        _commissionManagementView.SaveButton = false;
                        _commissionManagementView.PrintButton = true;

                        _activeCommissionId = calList[0].CmId;
                        _commissionManagementView.SetFine = comList[0].FineAmount;
                        _commissionManagementView.SetFarLandHelp = comList[0].FarLandHelp;
                        _commissionManagementView.SetTransCost = comList[0].TransCost;
                        _commissionManagementView.SetSpecialMoney = comList[0].SpecialMoney;
                        _commissionManagementView.setDBFine();
                        //_commissionManagementView.ClearCalculation();
                        WaitingFormHelper.HideWaitingForm();
                    }
                    else
                    {

                        //limit the number of commission calculation 
                        if (feeBase == null)
                        {
                            WaitingFormHelper.HideWaitingForm();
                            MessageBox.Show(null, "ระบบยังไม่ได้กำหนดอัตราค่าตอบแทน กรุณาติดต่อผู้ดูแลระบบ", "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (feeBase.HasCommissionCalLimit)
                        {
                            int maxLimit = feeBase.MaxCommissionCalCount.Value;
                            int calCount = _commissionMgtService.GetCommissionCountOfPeriod(bookSearch);

                            if (calCount >= maxLimit)
                            {
                                WaitingFormHelper.HideWaitingForm();
                                _commissionManagementView.SetOverLimit(true);

                                CalculateCommission(e.Data);
                                bookSearch.IsReprint = false;
                                _commissionManagementView.CalculateButton = false;
                                _commissionManagementView.SaveButton = false;
                                _commissionManagementView.PrintButton = false;                                
                                return;                                
                            }
                        }
                        bookSearch.IsReprint = false;
                        _commissionManagementView.CalculateButton = true;
                        _commissionManagementView.SaveButton = true;
                        _commissionManagementView.PrintButton = false;
                        CalculateCommission(e.Data);
                        WaitingFormHelper.HideWaitingForm();
                    }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "คำนวณค่าตอบแทน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }
        //
        [EventSubscription(EventTopicNames.LoadFineDetail, Thread = ThreadOption.UserInterface)]
        public void LoadFineDetailHandler(object sender, EventArgs e)
        {
            if (WorkItem.State["LoadFineDetail"] != null)
            {
                if (_commissionManagementView.PrintButton || _commissionManagementView.OverLimit)
                {
                    _comInfo.FineDetail.IsCalculated = true;
                }
                else
                {
                    _comInfo.FineDetail.IsCalculated = false;
                }
                WorkItem.State["LoadFineDetail"] = null;
                ShowFineDetail(_comInfo.FineDetail);
            }
        }

        [EventPublication(EventTopicNames.FillFineDetail, PublicationScope.Global)]
        public event EventHandler<EventArgs> FillFineDetailHandler;
        public void ShowFineDetail(FineDetailInfo fineDetailInfo)
        {
            if (FillFineDetailHandler != null)
            {
                WorkItem.State["FillFineDetail"] = true;
                FillFineDetailHandler(this, new EventArgs<FineDetailInfo>(fineDetailInfo));
            }
        }

        //show fine information view
        [EventSubscription(EventTopicNames.FineDetailView, Thread = ThreadOption.UserInterface)]
        public void FineDetailVieweHandler(object sender, EventArgs e)
        {
            if (WorkItem.State["FineDetailView"] != null)
            {
                if (Authorization.IsAuthorized(SecurityNames.FineDetail, true))
                {
                    //well, we also load the first page of another tab 
                    IFineDetailView fineDetailView = null;
                    if (WorkItem.Items.Contains("IFineDetailView"))
                    {
                        fineDetailView = WorkItem.Items.Get<IFineDetailView>("IFineDetailView");
                    }
                    else
                    {
                        fineDetailView = WorkItem.Items.AddNew<FineDetailView>("IFineDetailView");
                    }

                    _modalProperty.Title = "รายละเอียดค่าปรับ";
                    WorkItem.State["FineDetailView"] = null;
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(fineDetailView, _modalProperty);
                }
            }
        }

        [EventSubscription(EventTopicNames.TaxDetailView, Thread = ThreadOption.UserInterface)]
        public void TaxDetailViewHandler(object sender, EventArgs e)
        {
            if (Authorization.IsAuthorized(SecurityNames.TaxDetail, true))
            {
                MessageBox.Show(null, "ฟังก์ชันนี้จะมีใน Phase 2", "Pending", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        [EventSubscription(EventTopicNames.GetTravelHelpRate, Thread = ThreadOption.UserInterface)]
        public void GetTravelRateHandler(object sender, EventArgs<TravelHelpRateConditionInfo> e)
        {
            TravelHelpRateConditionInfo spc = (TravelHelpRateConditionInfo)e.Data;
            spc.BranchId = Session.Branch.Id;
            TravelHelpRate helpRate = _commissionMgtService.GetTravelHelpRate(spc);
            _commissionManagementView.TravelHelpRate = helpRate;
        }

        #endregion
    }
}
