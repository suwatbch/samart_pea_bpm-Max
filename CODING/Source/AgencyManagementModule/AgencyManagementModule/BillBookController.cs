using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.ReportViews;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using System.Globalization;
using PEA.BPM.AgencyManagementModule.Utilities;
using System.Collections.Generic;
using PEA.BPM.Infrastructure.Library.UI;



namespace PEA.BPM.AgencyManagementModule
{
    public class BillBookController : WorkItemController
    {
        //global branch setting
        private string _basedBranchId;

        //exceptional bill for this book
        private List<CallingBillInfo> _exceptionalBillList;

        //book input set
        private BillBookItemListInputInfo _bookInputList;

        //Services
        private IAgencyCommonService _agencyCommonService;
        private ICreateBillbookService _createBillbookService;

        //Views
        private IBillBookStatusView _billBookStatusView;
        private IBillBookSearchView _billBookSearchView;
        private IBillBookSummarizeCallingBillView _billBookSummarizeCallingBillView;
        private IBillBookCallingBillView _billBookCallingBillView;
        private IBillingBookNoneCallingBillView _billingBookNoneCallingBillView;
        private ILineBillView _lineBillView;
        private ILineNoneBillView _lineNoneBillView;
        private IBillBookManagementView _billBookManagementView;
        private List<string> _fucusedLine;
        private WindowSmartPartInfo _modalProperty;
        private bool _hideMainView;
        private DateTimeFormatInfo _th_dt;
        private string bookPeriod = null;


        public string BasedBranchId
        {
            set { _basedBranchId = value; }
        }

        [InjectionConstructor]
        public BillBookController([ServiceDependency] IAgencyCommonService agencyCommonService, ICreateBillbookService createBillbookService)
        {
            _agencyCommonService = agencyCommonService;
            _createBillbookService = createBillbookService;

            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.MaximizeBox = false;
            _modalProperty.MinimizeBox = false;
            _modalProperty.Modal = true;
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);

            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;

        }

        private void LoadAllViews()
        {
            _billBookSearchView = WorkItem.Items.AddNew<BillBookSearchView>("IBillBookSearchView");
            _billBookManagementView = WorkItem.Items.AddNew<BillBookManagementView>("IBillBookManagementView");
            _billBookSummarizeCallingBillView = WorkItem.Items.AddNew<BillBookSummarizeCallingBillView>("IBillBookSummarizeCallingBillView");
            _billBookCallingBillView = WorkItem.Items.AddNew<BillBookCallingBillView>("IBillBookCallingBillView");
            _billingBookNoneCallingBillView = WorkItem.Items.AddNew<BillingBookNoneCallingBillView>("IBillingBookNoneCallingBillView");
            _lineBillView = WorkItem.Items.AddNew<LineBillView>("ILineBillView");
            _lineNoneBillView = WorkItem.Items.AddNew<LineNoneBillView>("ILineNoneBillView");
            //_billBookTheOtherBillsView = WorkItem.Items.AddNew<BillBookTheOtherBillsView>("IBillBookTheOtherBillsView");
            _billBookSearchView.BillBookMgtView = _billBookManagementView;
        }

        public bool HideMainView
        {
            set { _hideMainView = value; }

        }

        public IBillBookStatusView BookStatusView
        {
            set { _billBookStatusView = value; }
        }

        public override void Run()
        {
            ShellWaitCursor(true);

            LoadAllViews();
            _billBookSearchView = WorkItem.Items.Get<IBillBookSearchView>("IBillBookSearchView");
            if (!_hideMainView)
            {
                WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_billBookSearchView);
                // ((UserControl)_billBookSearchView).ParentForm.Text = "สร้างสมุดจ่ายบิล - Billing & Payment Management";
                SetWindowsTitle("ตัดชำระสมุดจ่ายบิล /Group Invoicing");
            }
            _exceptionalBillList = new List<CallingBillInfo>();

            ShellWaitCursor(false);
        }

        public void ShowMainView()
        {
            ClearBookScreenHeaderClicked();
            _billBookManagementView.ClearInput();
            _exceptionalBillList.Clear();
            _billBookSearchView.ActivateBookCreationPanel();
            SetCancelBillBook(false);
            ClearAllBillViews();

            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_billBookSearchView);
            ((UserControl)_billBookSearchView).ParentForm.Text = "สร้างสมุดจ่ายบิล - Billing & Payment Management";
        }

        private void ClearAllBillViews()
        {
            _billBookCallingBillView.Clear();
            _billingBookNoneCallingBillView.Clear();
            _lineBillView.Clear();
            _lineNoneBillView.Clear();
            _billBookSummarizeCallingBillView.Clear();
        }

        [EventSubscription(EventTopicNames.NewBillBook, Thread = ThreadOption.UserInterface)]
        public void NewBillBookHandler(object sender, EventArgs e)
        {
            ShowMainView();
        }

        [EventSubscription(EventTopicNames.LoadBillbookListForReprint, Thread = ThreadOption.UserInterface)]
        public void LoadBillbookListForReprintHandler(object sender, EventArgs<BookSearchStatusEnum> e)
        {
            try
            {
                WaitingFormHelper.ShowWaitingForm();
                _billBookStatusView.BillBookList = _createBillbookService.LoadBillBookList(e.Data, Session.Branch.Id);
                WaitingFormHelper.HideWaitingForm();
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาข้อมูลสมุดจ่ายบิล", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.LoadBillbookListByKeywordForReprint, Thread = ThreadOption.UserInterface)]
        public void LoadBillbookListByKeywordForReprintHandler(object sender, EventArgs<BillbookInfoReprint> e)
        {
            _billBookStatusView.BillBookList = _createBillbookService.LoadBillBookByIdKeyword(e.Data, Session.Branch.Id);
        }


        private void ValidateExceptinalBillList(BillBookItemListInputInfo bookInputList)
        {
            List<CallingBillInfo> tempBillList = new List<CallingBillInfo>();
            foreach (CallingBillInfo bill in _exceptionalBillList)
            {
                BindingList<BillBookCreationExtraInfo> extraList = bookInputList.BookExtraItems;
                foreach (BillBookCreationExtraInfo extra in extraList)
                {
                    if (bill.CaId == extra.Number)
                        tempBillList.Add(bill); //keep it
                }

                BindingList<BillBookCreationInfo> inputList = bookInputList.CreationItemList;
                foreach (BillBookCreationInfo cr in inputList)
                {
                    if (bill.PeaCode == cr.PeaCode && bill.LineId == cr.LineId)
                        if (!tempBillList.Contains(bill))
                            tempBillList.Add(bill);
                }
            }

            _exceptionalBillList = tempBillList;
        }

        [EventSubscription(EventTopicNames.RetrieveBookSummary, Thread = ThreadOption.UserInterface)]
        public void RetreiveSummmaryBillBookHandler(object sender, EventArgs e)
        {
            try
            {
                //WaitingFormHelper.ShowWaitingForm();
                _billBookSummarizeCallingBillView = WorkItem.Items.Get<IBillBookSummarizeCallingBillView>("IBillBookSummarizeCallingBillView");
                
                _billBookSearchView.SumCallingBillDeckWorkSpace.Show(_billBookSummarizeCallingBillView);
                
                ClearAllBillViews();
                BillBookItemListInputInfo bookInputList = _billBookManagementView.BookInputList;
                BindingList<CallingBillSummaryInfo> pastBook = _createBillbookService.RetreiveBookSummary(bookInputList.HeaderInfo.BillBookId, bookInputList.HeaderInfo.Period);

                //set default if has only one line
                if ((pastBook.Count > 0) && _fucusedLine == null)
                {
                    _fucusedLine = new List<string>();
                    _fucusedLine.Add(pastBook[0].PeaCode);
                    _fucusedLine.Add(pastBook[0].LineId);
                }

                _billBookSummarizeCallingBillView.SetCancelBillBook(bookInputList.IsEditBillBook);
                _billBookSummarizeCallingBillView.BillSummaryInfoList = pastBook;
                _billBookSearchView.ActivateFindResultPanel(2);
                //WaitingFormHelper.HideWaitingForm();
            }
            catch (Exception ex)
            {
                //WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาข้อมูลผู้ใช้ไฟเพื่อสร้างสมุด", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.RetrieveBookDetail, Thread = ThreadOption.UserInterface)]
        public void RetrieveBookDetailHandler(object sender, EventArgs e)
        {
            try
            {
                //WaitingFormHelper.ShowWaitingForm();
                _billBookCallingBillView = WorkItem.Items.Get<IBillBookCallingBillView>("IBillBookCallingBillView");
                _billBookSearchView.CallingBillDeckWorkSpace.Show(_billBookCallingBillView);
                ClearAllBillViews();
                BillBookItemListInputInfo bookInputList = _billBookManagementView.BookInputList;
                BindingList<CallingBillInfo> pastBook = _createBillbookService.RetreiveBookDetail(bookInputList.HeaderInfo.BillBookId);
                _billBookCallingBillView.BillInfoList = pastBook;
                _billBookSearchView.ActivateFindResultPanel(0);
                //WaitingFormHelper.HideWaitingForm();
            }
            catch (Exception ex)
            {
                //WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาข้อมูลผู้ใช้ไฟเพื่อสร้างสมุด", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }

        }

        [EventSubscription(EventTopicNames.RetrieveBookLine, Thread = ThreadOption.UserInterface)]
        public void RetrieveBookLineDetailHandler(object sender, EventArgs e)
        {
            try
            {
                //WaitingFormHelper.ShowWaitingForm();
                _lineBillView = WorkItem.Items.Get<ILineBillView>("ILineBillView");
                _billBookSearchView.LineBillDeckWorkSpace.Show(_lineBillView);
                ClearAllBillViews();
                BillBookItemListInputInfo bookInputList = _billBookManagementView.BookInputList;
                if (_fucusedLine != null)
                {
                    BindingList<CallingBillInfo> pastBook = _createBillbookService.RetreiveBookLineDetail(bookInputList.HeaderInfo.BillBookId, _fucusedLine.ToArray());
                    _lineBillView.BillInfoList = pastBook;
                }
                _billBookSearchView.ActivateFindResultPanel(3);
                //WaitingFormHelper.HideWaitingForm();
            }
            catch (Exception ex)
            {
                //WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาข้อมูลผู้ใช้ไฟเพื่อสร้างสมุด", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }


        [EventSubscription(EventTopicNames.CallingBillButton, Thread = ThreadOption.UserInterface)]
        public void CallingBillButtonClickedHandler(object sender, EventArgs e)
        {
            try
            {
                //WaitingFormHelper.ShowWaitingForm();
                _billBookCallingBillView = WorkItem.Items.Get<IBillBookCallingBillView>("IBillBookCallingBillView");
                _billBookSearchView.CallingBillDeckWorkSpace.Show(_billBookCallingBillView);
                ClearAllBillViews();
                //fill information    
                BillBookItemListInputInfo bookInputList = _billBookManagementView.BookInputList;                
                ValidateExceptinalBillList(bookInputList);
                
                //set enable/disable cancelbutton
                SetCancelBillBook(bookInputList.IsEditBillBook);
                
                if (bookInputList != null && bookInputList.CreationItemList.Count != 0)
                {
                    BillBookHeaderInfo header = _billBookSearchView.GetBillBookHeader();
                    if (!_billBookManagementView.IsValidBookHeader(header)) return;
                    //bookInputList.HeaderInfo.Period = e.Data;
                    bookInputList.HeaderInfo = header;
                    BindingList<CallingBillInfo> billInfoList = _createBillbookService.DisplayBillBookCallingBill(bookInputList);
                    BindingList<CallingBillInfo> temp = new BindingList<CallingBillInfo>();
                    if (_exceptionalBillList.Count != 0)
                    {
                        foreach (CallingBillInfo bill in billInfoList)
                        {
                            //work around for webservice, not allow IDictionary                    
                            CallingBillInfo ci = new CallingBillInfo();
                            ci.InvoiceNo = bill.InvoiceNo;
                            _exceptionalBillList.Sort();
                            int index = _exceptionalBillList.BinarySearch(ci);
                            if (index < 0) //not found
                                temp.Add(bill);
                            else // found remove it
                                temp.Remove(bill);
                        }
                        billInfoList = temp;
                    }
                    _billBookCallingBillView.BillInfoList = billInfoList;
                    _billBookSearchView.ActivateFindResultPanel(0);

                    //WaitingFormHelper.HideWaitingForm();
                }
                else
                {
                    //WaitingFormHelper.HideWaitingForm();
                    MessageBox.Show(null, "กรุณาป้อนข้อมูลบิลก่อนการค้นหา ", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                //WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาข้อมูลผู้ใช้ไฟเพื่อสร้างสมุด", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }


        [EventSubscription(EventTopicNames.SummarizeCallingBillButton, Thread = ThreadOption.UserInterface)]
        public void SummarizeCallingBillButtonClickedHandler(object sender, EventArgs e)
        {
            try
            {
                //WaitingFormHelper.ShowWaitingForm();
                BillBookItemListInputInfo bookInputList = _billBookManagementView.BookInputList;
                if (bookInputList.HeaderInfo.Period == null)
                {
                    bookPeriod = null;
                }
                else
                {
                    bookPeriod = UnFormatPeriod(bookInputList.HeaderInfo.Period.Trim());
                }
                ValidateExceptinalBillList(bookInputList);

                _billBookSummarizeCallingBillView = WorkItem.Items.Get<IBillBookSummarizeCallingBillView>("IBillBookSummarizeCallingBillView");                
                _billBookSearchView.SumCallingBillDeckWorkSpace.Show(_billBookSummarizeCallingBillView);
                ClearAllBillViews();

                //set enable/disable cancelbutton
                SetCancelBillBook(bookInputList.IsEditBillBook);
                if (bookInputList != null && bookInputList.CreationItemList.Count != 0)
                {
                    BillBookHeaderInfo header = _billBookSearchView.GetBillBookHeader();
                    if (!_billBookManagementView.IsValidBookHeader(header)) return;

                    bookInputList.HeaderInfo = header;
                    BindingList<CallingBillSummaryInfo> callingSummaryInfoList = _createBillbookService.DisplayBillBookSummarizeCallingBill(bookInputList);
                    RemoveExceptionalBills(callingSummaryInfoList);
                    _billBookSummarizeCallingBillView.BillSummaryInfoList = callingSummaryInfoList;
                    //set default if has only one line
                    if ((callingSummaryInfoList.Count > 0) && _fucusedLine == null)
                    {
                        _fucusedLine = new List<string>();
                        _fucusedLine.Add(callingSummaryInfoList[0].PeaCode);
                        _fucusedLine.Add(callingSummaryInfoList[0].LineId);
                    }
                    _billBookSummarizeCallingBillView.setInitialSelectedLine();
                    _billBookSearchView.ActivateFindResultPanel(2);
                    //WaitingFormHelper.HideWaitingForm();
                }
                else
                {
                    //WaitingFormHelper.HideWaitingForm();
                    MessageBox.Show(null, "กรุณาป้อนข้อมูลบิลก่อนการค้นหา ", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                //WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาข้อมูลผู้ใช้ไฟเพื่อสร้างสมุด", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.NoneCallingBillButton, Thread = ThreadOption.UserInterface)]
        public void NoneCallingBillButtonClickedHandler(object sender, EventArgs e)
        {
            try
            {
                //WaitingFormHelper.ShowWaitingForm();
                _billingBookNoneCallingBillView = WorkItem.Items.Get<IBillingBookNoneCallingBillView>("IBillingBookNoneCallingBillView");
                _billBookSearchView.NoneCallingBillDeckWorkSpace.Show(_billingBookNoneCallingBillView);
                ClearAllBillViews();

                BindingList<CallingBillInfo> ncBillList = new BindingList<CallingBillInfo>();
                BillBookItemListInputInfo bookInputList = _billBookManagementView.BookInputList;
                ValidateExceptinalBillList(bookInputList);

                //set enable/disable cancelbutton
                SetCancelBillBook(bookInputList.IsEditBillBook);
                if (bookInputList != null && bookInputList.CreationItemList.Count != 0)
                {
                    BillBookHeaderInfo header = _billBookSearchView.GetBillBookHeader();
                    if (!_billBookManagementView.IsValidBookHeader(header)) return;

                    bookInputList.HeaderInfo = header;
                    BindingList<CallingBillInfo> billInfoList = _createBillbookService.DisplayBillBookNoneCallingBill(bookInputList);

                    foreach (CallingBillInfo bl in billInfoList)
                        ncBillList.Add(bl);

                    //add exceptional list
                    foreach (CallingBillInfo ci in _exceptionalBillList)
                    {
                        CallingBillInfo inst = ci.Clone();
                        ncBillList.Add(inst);
                    }

                    _billingBookNoneCallingBillView.BillInfoList = ncBillList;
                    _billBookSearchView.ActivateFindResultPanel(1);
                    //WaitingFormHelper.HideWaitingForm();
                }
                else
                {
                    //WaitingFormHelper.HideWaitingForm();
                    MessageBox.Show(null, "กรุณาป้อนข้อมูลบิลก่อนการค้นหา ", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                //WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาข้อมูลผู้ใช้ไฟที่ไม่ได้ออกเก็บ", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }

        }

        [EventSubscription(EventTopicNames.PortionBillButton, Thread = ThreadOption.UserInterface)]
        public void PortionBillButtonClickedHandler(object sender, EventArgs<LineSearchInfo> e)
        {
            try
            {
                //WaitingFormHelper.ShowWaitingForm();
                _lineBillView = WorkItem.Items.Get<ILineBillView>("ILineBillView");
                _billBookSearchView.LineBillDeckWorkSpace.Show(_lineBillView);
                ClearAllBillViews();
                //fill information 

                if (_fucusedLine != null)
                {
                    BillBookItemListInputInfo bookInputList = _billBookManagementView.BookInputList;
                    ValidateExceptinalBillList(bookInputList);

                    //set enable/disable cancelbutton
                    SetCancelBillBook(bookInputList.IsEditBillBook);
                    if (bookInputList != null && bookInputList.CreationItemList.Count != 0)
                    {
                        BillBookHeaderInfo header = _billBookSearchView.GetBillBookHeader();
                        if (!_billBookManagementView.IsValidBookHeader(header)) return;

                        bookInputList.HeaderInfo = header;
                        BindingList<CallingBillInfo> billInfoList = _createBillbookService.DisplayBillBookLineBill(bookInputList, _fucusedLine);
                        //remove exceptional list
                        BindingList<CallingBillInfo> temp = new BindingList<CallingBillInfo>();
                        if (_exceptionalBillList.Count > 0)
                        {
                            foreach (CallingBillInfo bill in billInfoList)
                            {
                                //work around for webservice, not allow IDictionary                    
                                _exceptionalBillList.Sort();
                                int index = _exceptionalBillList.BinarySearch(bill);
                                if (index < 0) //not found
                                    temp.Add(bill);
                            }
                            billInfoList = temp;
                        }
                        _lineBillView.BillInfoList = billInfoList;
                        _billBookSearchView.ActivateFindResultPanel(3);
                        //WaitingFormHelper.HideWaitingForm();
                    }
                    else
                    {
                        //WaitingFormHelper.HideWaitingForm();
                        MessageBox.Show(null, "กรุณาป้อนข้อมูลบิลก่อนการค้นหา ", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    //WaitingFormHelper.HideWaitingForm();
                }
            }
            catch (Exception ex)
            {
                //WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาข้อมูลผู้ใช้ไฟเพื่อสร้างสมุด", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }


        [EventSubscription(EventTopicNames.PortionNoCallBillButton, Thread = ThreadOption.UserInterface)]
        public void PortionNoCallBillButtonClickedHandler(object sender, EventArgs<LineSearchInfo> e)
        {
            try
            {
                _lineNoneBillView = WorkItem.Items.Get<ILineNoneBillView>("ILineNoneBillView");
                _billBookSearchView.LineNoBillDeckWorkSpace.Show(_lineNoneBillView);
                ClearAllBillViews();
                BindingList<CallingBillInfo> ncBillList = new BindingList<CallingBillInfo>();
                //fill information 
                if (_fucusedLine != null)
                {
                    BillBookItemListInputInfo bookInputList = _billBookManagementView.BookInputList;
                    ValidateExceptinalBillList(bookInputList);

                    //set enable/disable cancelbutton
                    SetCancelBillBook(bookInputList.IsEditBillBook);
                    if (bookInputList != null && bookInputList.CreationItemList.Count != 0)
                    {
                        BillBookHeaderInfo header = _billBookSearchView.GetBillBookHeader();
                        if (!_billBookManagementView.IsValidBookHeader(header)) return;

                        bookInputList.HeaderInfo = header;
                        BindingList<CallingBillInfo> billInfoList = _createBillbookService.DisplayBillBookLineNoBill(bookInputList, _fucusedLine);
                        //remove exceptional list
                        BindingList<CallingBillInfo> temp = new BindingList<CallingBillInfo>();
                        foreach (CallingBillInfo ci in billInfoList)
                            ncBillList.Add(ci);

                        foreach (CallingBillInfo billInfo in _exceptionalBillList)
                        {
                            if (_fucusedLine != null)
                            {
                                string peaCode = _fucusedLine[0];
                                string lineId = _fucusedLine[1];
                                if ((billInfo.PeaCode == peaCode) && (billInfo.LineId == lineId))
                                    ncBillList.Add(billInfo.Clone());
                            }
                        }

                        _lineNoneBillView.BillInfoList = ncBillList;
                        _billBookSearchView.ActivateFindResultPanel(4);

                    }
                    else
                    {
                        //WaitingFormHelper.HideWaitingForm();
                        MessageBox.Show(null, "กรุณาป้อนข้อมูลบิลก่อนการค้นหา ", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                //WaitingFormHelper.HideWaitingForm();

            }
            catch (Exception ex)
            {
                //WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาข้อมูลผู้ใช้ไฟเพื่อสร้างสมุด", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }

        }

        [EventSubscription(EventTopicNames.BookGeneratorTabActive, Thread = ThreadOption.UserInterface)]
        public void BookGeneratorTabActiveHandler(object sender, EventArgs e)
        {
            try
            {
                _billBookManagementView = WorkItem.Items.Get<IBillBookManagementView>("IBillBookManagementView");
                _billBookSearchView.BookManagerDeckWorkspace.Show(_billBookManagementView);
                ClearAllBillViews();
                _billBookManagementView.ParentView = _billBookSearchView;

                //well, we also load the first page of another tab 
                if (WorkItem.Items.Contains("IBillBookCallingBillView"))
                {
                    _billBookCallingBillView = WorkItem.Items.Get<IBillBookCallingBillView>("IBillBookCallingBillView");
                }
                else
                {
                    _billBookCallingBillView = WorkItem.Items.AddNew<BillBookCallingBillView>("IBillBookCallingBillView");
                }

                _billBookSearchView.CallingBillDeckWorkSpace.Show(_billBookCallingBillView);
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "สร้างสมุดจ่ายบิล", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }


        #region Keydown Event

        [EventSubscription(EventTopicNames.AvailableDeposit, Thread = ThreadOption.UserInterface)]
        public void AvailableDepositRequestHandler(object sender, EventArgs<string> e)
        {
            _billBookSearchView.UsedDeposit = _createBillbookService.GetUsedDeposit(e.Data);
        }

        [EventSubscription(EventTopicNames.BillBookAgentIdSearchText, Thread = ThreadOption.UserInterface)]
        public void BillBookAgentIdSearchTextCommittedHandler(object sender, EventArgs<string> e)
        {
            try
            {
                WaitingFormHelper.ShowWaitingForm();
                AgentInfo agentInfo = _createBillbookService.BookSearchAgenctInfo(e.Data, _billBookSearchView.BillPeriod);
                WaitingFormHelper.HideWaitingForm();


                //check valid agency            
                if (agentInfo.Id == null)
                {
                    MessageBox.Show(null, "ไม่พบข้อมูลตัวแทนที่ต้องการ\n", "การค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _billBookSearchView.SearchAgentInfo = agentInfo;                                 
                    _billBookSearchView.FocusAgentInput();
                    return; // not found
                }

                //not allowed creating book in this branch
                if (!string.Equals(_createBillbookService.GetAgentBACode(agentInfo.TechBranchId) , Session.Branch.BACode, StringComparison.CurrentCultureIgnoreCase))
                {
                    MessageBox.Show(null, "ตัวแทนไม่ได้จดทะเบียนในสาขานี้\n", "การค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    _billBookSearchView.SearchAgentInfo = (AgentInfo)null;
                    _billBookSearchView.FocusAgentInput();
                    return;
                }

                //check agency status
                //Fix me!! to match with SAP status
                if (agentInfo.BookHolder == (int)BookHolderState.Agent && (agentInfo.ContractValidFrom > Session.BpmDateTime.Now || agentInfo.ContractValidTo < Session.BpmDateTime.Now))
                {
                    MessageBox.Show(null, "ไม่สามารถออกสมุดจ่ายบิลด้วยสถานะของตัวแทนนี้\n\n\tสถานะ: " + agentInfo.AgencyStatus, "สถานะตัวแทน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    agentInfo.Id = null;
                    _billBookSearchView.SearchAgentInfo = agentInfo;
                    return;
                }

                _billBookSearchView.SearchAgentInfo = agentInfo;
                _billBookManagementView.BookValidPeaCode = agentInfo.TechBranchId;

                if (agentInfo.BookHolder == (int)BookHolderState.Agent)
                {
                    if (_billBookSearchView.BillPeriod.Replace("/", "").Trim() == "")
                    {
                        _billBookSearchView.StartInput();
                        return;
                    }

                    if (Authorization.IsAuthorized(SecurityNames.CreateAgentBillBook, "Create billbook for agency", true))
                    {
                        //enable calling bill manager and others
                        _billBookManagementView.EnableBillBookCreating(agentInfo.Id);
                        _billBookManagementView.IsAgencyBillBook = true;
                        if (TextUtility.IsInPeriod(_billBookSearchView.BillPeriod,2))
                        {
                            _billBookManagementView.PrintBt(true);
                            _billBookSearchView.ReceiveCount = String.Format("{0}/{1}", agentInfo.BookLot.ToString().PadLeft(2, '0'), agentInfo.ReceiveCount.ToString().PadLeft(2, '0'));
                        }
                        else
                        {
                            _billBookManagementView.PrintBt(false);
                            if (agentInfo.ReceiveCount == 1)
                            {
                                //no exist billbook for this past period, so disable receivecount
                                _billBookSearchView.EnableReceiveCount(false);
                                _billBookSearchView.BillbookHeaderEnable(false);
                                _billBookSearchView.FocusAgentInput();
                            }
                            else
                            {
                                _billBookSearchView.ReceiveCount = String.Format("{0}/{1}", agentInfo.BookLot.ToString().PadLeft(2, '0'), agentInfo.ReceiveCount.ToString().PadLeft(2, '0'));
                                _billBookSearchView.FocusReceiveCount();
                                _billBookSearchView.EnableReceiveCount(true);
                            }
                        }
                    }
                }
                else if (agentInfo.BookHolder == (int)BookHolderState.Employee)
                {
                    if (_billBookSearchView.BillPeriod.Replace("/", "") == "")
                    {
                        _billBookSearchView.StartInput();
                    }

                    if (Authorization.IsAuthorized(SecurityNames.CreateEmployeeBillBook, "Create billbook for employee", true))
                    {
                        //enable calling bill manager and others
                        _billBookManagementView.EnableBillBookCreating(agentInfo.Id);
                        _billBookManagementView.IsAgencyBillBook = false;
                        if (TextUtility.IsInPeriod(_billBookSearchView.BillPeriod,2))
                        {
                            _billBookManagementView.PrintBt(true);
                            _billBookSearchView.ReceiveCount = String.Format("{0}/{1}", agentInfo.BookLot.ToString().PadLeft(2, '0'), agentInfo.ReceiveCount.ToString().PadLeft(2, '0'));
                        }
                        else
                        {
                            _billBookManagementView.PrintBt(false);
                            if (agentInfo.ReceiveCount == 1)
                            {
                                //no exist billbook for this past period, so disable receivecount
                                _billBookSearchView.EnableReceiveCount(false);
                                _billBookSearchView.BillbookHeaderEnable(false);
                                _billBookSearchView.FocusAgentInput();
                            }
                            else
                            {
                                _billBookSearchView.ReceiveCount = String.Format("{0}/{1}", agentInfo.BookLot.ToString().PadLeft(2, '0'), agentInfo.ReceiveCount.ToString().PadLeft(2, '0'));
                                _billBookSearchView.FocusReceiveCount();
                                _billBookSearchView.EnableReceiveCount(true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาข้อมูลตัวแทน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }


        [EventSubscription(EventTopicNames.CheckEmployeeNo, Thread = ThreadOption.UserInterface)]
        public void CheckEmployeeNoHandler(object sender, EventArgs<string> e)
        {

            try
            {
                WaitingFormHelper.ShowWaitingForm();
                AgentInfo empInfo = _createBillbookService.EmployeeSearchInfo(e.Data);
                WaitingFormHelper.HideWaitingForm();

                //check valid agency            
                if (empInfo == null)
                {

                    MessageBox.Show(null, "ไม่พบข้อมูลพนักงานคุมใบเสร็จ\n", "การค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _billBookSearchView.FocusEmployeeInput();
                }
                else
                {
                    _billBookSearchView.FoundEmployeeNo();
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาข้อมูลพนักงานคุมใบเสร็จ\n", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.JumpToStartCallingBillCursor, Thread = ThreadOption.UserInterface)]
        public void JumpToStartCallingBillCursorActivatedHandler(object sender, EventArgs e)
        {
            _billBookManagementView.MoveCursorToStart();
        }


        [EventSubscription(EventTopicNames.BillBookLoadValidationData, Thread = ThreadOption.UserInterface)]
        public void BillBookLoadValidationDataActivedHandler(object sender, EventArgs<List<string>> e)
        {
            _billBookManagementView.BookValidLineList = _createBillbookService.LoadBookItemValidationData(e.Data);
        }

        [EventSubscription(EventTopicNames.LoadBranchValidation, Thread = ThreadOption.UserInterface)]
        public void LoadBranchValidationHandler(object sender, EventArgs e)
        {
            _billBookManagementView.BookValidBranchList = _agencyCommonService.GetBranches(Session.Branch.Id);
        }

        #endregion

        //summary calling bill
        private void RemoveExceptionalBills(BindingList<CallingBillSummaryInfo> callingSummaryInfoList)
        {
            //string crPeriod = Session.BpmDateTime.Now.ToString("yyyyMM", _th_dt);
            int curInt = Convert.ToInt32(bookPeriod);
            List<int> rmList = new List<int>();

            foreach (CallingBillInfo ci in _exceptionalBillList)
            {
                int period = Convert.ToInt32(ci.BillPeriodOrg);

                if (period == curInt) //current period
                {
                    //update bill summary list of the current period
                    foreach (CallingBillSummaryInfo sm in callingSummaryInfoList)
                    {
                        //verify for only branchId and Mru are allowed
                        if ((sm.PeaCode == ci.PeaCode) && (sm.LineId == ci.LineId))
                        {
                            sm.AmountCurrent -= ci.Amount;
                            sm.BillCountCurrent--;
                            sm.TotalAmount -= ci.Amount;
                            sm.TotalCount--;
                        }
                    }
                }
                else if (period < curInt) //past period
                {
                    //update bill summary list of the past period
                    foreach (CallingBillSummaryInfo sm in callingSummaryInfoList)
                    {
                        //verify for only branchId and Mru are allowed
                        if ((sm.PeaCode == ci.PeaCode) && (sm.LineId == ci.LineId))
                        {
                            sm.AmountPast -= ci.Amount;
                            sm.BillCountPast--;
                            sm.TotalAmount -= ci.Amount;
                            sm.TotalCount--;
                        }
                    }
                }
                else
                {
                    //should not get here
                }
            }

            //check for zero bill line
            for (int i = callingSummaryInfoList.Count - 1; i >= 0; i--)
            {
                //add to remove line Id
                if (callingSummaryInfoList[i].BillCountCurrent == 0 && callingSummaryInfoList[i].BillCountPast == 0)
                    rmList.Add(i);
            }

            foreach (int rm in rmList)
                callingSummaryInfoList.RemoveAt(rm);

        }

        [EventSubscription(EventTopicNames.ClearExceptionalBillList, Thread = ThreadOption.UserInterface)]
        public void ClearExceptionalBillListHandler(object sender, EventArgs e)
        {
            _exceptionalBillList.Clear();
        }


        //for reprint purpose
        [EventPublication(EventTopicNames.ShowCAB03_01Report, PublicationScope.Global)]
        public event EventHandler<EventArgs> LoadCAB03_01ReportHandler;
        public void PrintCAB03_01Report(CheckInBillBookConditionInfoReport printCondition)
        {
            try
            {
                if (LoadCAB03_01ReportHandler != null)
                    LoadCAB03_01ReportHandler(this, new EventArgs<CheckInBillBookConditionInfoReport>(printCondition));
            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }

        }

        //for reprint purpose
        [EventPublication(EventTopicNames.ShowCAB03_03Report, PublicationScope.Global)]
        public event EventHandler<EventArgs> LoadCAB03_03ReportHandler;
        public void PrintCAB03_03Report(CheckInBillBookConditionInfoReport printCondition)
        {
            try
            {
                if (LoadCAB03_03ReportHandler != null)
                    LoadCAB03_03ReportHandler(this, new EventArgs<CheckInBillBookConditionInfoReport>(printCondition));
            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }

        }

        //for reprint purpose
        [EventPublication(EventTopicNames.ShowCAB03_04Report, PublicationScope.Global)]
        public event EventHandler<EventArgs> LoadCAB03_04ReportHandler;
        public void PrintCAB03_04Report(CheckInBillBookConditionInfoReport printCondition)
        {
            try
            {
                if (LoadCAB03_04ReportHandler != null)
                    LoadCAB03_04ReportHandler(this, new EventArgs<CheckInBillBookConditionInfoReport>(printCondition));
            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }

        }

        //for reprint purpose
        [EventPublication(EventTopicNames.ShowCAB03_02Report, PublicationScope.Global)]
        public event EventHandler<EventArgs> LoadCAB03_02ReportHandler;
        public void PrintCAB03_02Report(CheckInBillBookConditionInfoReport printCondition)
        {
            try
            {
                if (LoadCAB03_02ReportHandler != null)
                    LoadCAB03_02ReportHandler(this, new EventArgs<CheckInBillBookConditionInfoReport>(printCondition));
            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.BillBookReprintButton, Thread = ThreadOption.UserInterface)]
        public void BillBookReprintButtonHandler(object sender, EventArgs<ArrayList> e)
        {
            List<string> bookList = (List<string>)e.Data[0];
            bool[] printFlag = (bool[])e.Data[1];

            foreach (string bookId in bookList)
            {
                //put-front running branchId
                string billbookId = string.Format("{0}{1}", Session.Branch.Id, bookId);
                BillBookItemListInputInfo bookItemList = _createBillbookService.LoadPastBillBookInfo(billbookId);
                BillBookHeaderInfo bookHeader = bookItemList.HeaderInfo;

                if (printFlag[0]) //billbook
                {
                    bookHeader.IsPrintDetail = false;
                    bookHeader.IsPrintPreview = printFlag[4];
                    bookHeader.IsPrintBillbook = true;
                    PrintBillBook(bookHeader);
                }

                if (printFlag[1]) //billbook detail
                {
                    bookHeader.IsPrintDetail = true;
                    bookHeader.IsPrintPreview = printFlag[4];
                    bookHeader.IsPrintBillbook = false;
                    PrintBillBook(bookHeader);
                }

                if (printFlag[2]) //cannot collect bill
                {
                    CheckInBillBookConditionInfoReport param = new CheckInBillBookConditionInfoReport();
                    param.AgentId = bookHeader.AgentId.PadLeft(12, '0');
                    param.BillBookId = billbookId;
                    param.PrintPreview = printFlag[4];
                    PrintCAB03_01Report(param);
                    PrintCAB03_03Report(param);
                    PrintCAB03_04Report(param);
                }

                if (printFlag[3]) //summary collect bill
                {
                    CheckInBillBookConditionInfoReport parem = new CheckInBillBookConditionInfoReport();
                    parem.AgentId = bookHeader.AgentId.PadLeft(12, '0');
                    parem.BillBookId = billbookId;
                    parem.PrintPreview = printFlag[4];
                    PrintCAB03_02Report(parem);
                }
            }
        }

        [EventSubscription(EventTopicNames.CheckAndLoadExistingReceiveCount, Thread = ThreadOption.UserInterface)]
        public void CheckAndLoadExistingReceiveCountHandler(object sender, EventArgs<BillBookItemListInputInfo> e)
        {
            string bookId = _createBillbookService.CheckExistingReceiveCount(e.Data);
            if (bookId != null)
            {
                //check for checking in status of billbook
                if (!_createBillbookService.IsBillBookAlreadyCheckedIn(bookId))
                {
                    //input list
                    BillBookItemListInputInfo bookItemList = _createBillbookService.LoadPastBillBookInfo(bookId);

                    //just query past billbook and disable inputs
                    bookItemList.EnableSavePrint = false;
                    _billBookSearchView.FillPastBillBookHeader(bookItemList, true);
                    //set flag that this is old billbook, not search for bill needed
                    _billBookManagementView.FillPastBillBookInputSet(bookItemList, true);

                    EnableBookInput(false);
                    _billBookManagementView.MoveCursorToStart();
                }
                else
                {
                    MessageBox.Show(null, "สมุดจ่ายบิลเล่มนี้ถูกตัดชำระไปแล้ว", "ตัดชะระแล้ว", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _billBookSearchView.FocusReceiveCount();
                }
            }
            else
            {
                EnableBookInput(true);
                _billBookSearchView.FocusBillManInput();
            }
        }

        [EventSubscription(EventTopicNames.BillBookSaveRequest, Thread = ThreadOption.UserInterface)]
        public void BillBookSaveRequestClickedHandler(object sender, EventArgs<BillBookItemListInputInfo> e)
        {
            try
            {
                if (e.Data == null || e.Data.CreationItemList.Count == 0) //save without find
                {
                    if (_bookInputList == null || _bookInputList.CreationItemList.Count == 0)  // Find button clicked
                    {
                        MessageBox.Show(null, "ไม่มีรายการใบเสร็จที่จะทำการสร้างสมุด", "กรุณาป้อนข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    _bookInputList = e.Data;
                }


                //put running branchId
                _bookInputList.HeaderInfo.RunningBranchId = Session.Branch.Id;
                _bookInputList.HeaderInfo.ModifiedBy = Session.User.Id;
                _bookInputList.ExceptionalBill = _exceptionalBillList;
                string bookId = _createBillbookService.CheckExistingReceiveCount(_bookInputList);

                if ((!_bookInputList.IsEditBillBook) && (bookId != null))
                {
                    //WaitingFormHelper.ShowWaitingForm();
                    //check for checking in status of billbook
                    if (!_createBillbookService.IsBillBookAlreadyCheckedIn(bookId))
                    {
                        //WaitingFormHelper.HideWaitingForm();
                        //just query past billbook and disable inputs
                        _billBookSearchView.FillPastBillBookHeader(_bookInputList, true);
                        _billBookManagementView.FillPastBillBookInputSet(_bookInputList, true);
                        EnableBookInput(false);

                    }
                    else
                    {
                        //WaitingFormHelper.HideWaitingForm();
                        MessageBox.Show(null, "สมุดจ่ายบิลเล่มที่เลือกได้ถูกตัดชำระไปแล้ว", "ไม่สามารถทำรายการได้", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else 
                {
                
                    string showMessage = String.Empty;
                    if (_bookInputList.IsEditBillBook)
                    {
                        showMessage = "คุณต้องการแก้ไขสมุดจ่ายบิลใช่หรือไม่\nระบบจะทำการยกเลิกสมุดเล่มดังกล่าวและสร้างสมุดจ่ายบิลใหม่";
                        _bookInputList.HeaderInfo.OriginalBillBookId = _bookInputList.HeaderInfo.BillBookId;
                    }
                    else
                    {
                        showMessage = "คุณต้องการสร้างสมุดจ่ายบิลใช่หรือไม่";
                    }
                    DialogResult dlg = MessageBox.Show(null, showMessage, "กรุณายืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    
                    if (dlg == DialogResult.Yes)
                    {
                        _bookInputList.ExceptionalBill = _exceptionalBillList;

                        //WaitingFormHelper.ShowWaitingForm();
                        //check deposit first
                        DepositBillBookAmountInfo depInfo = _createBillbookService.IsOverLimitAgentDeposit(_bookInputList);
                        BillBookHeaderInfo bookHeader = null;
                        //WaitingFormHelper.HideWaitingForm();

                        if (depInfo.IsOverLimit && _bookInputList.HeaderInfo.IsAgency)
                        {
                            string caption = string.Format("มูลค่าหลักทรัพย์ค้ำประกันไม่พอ! วงเงินคงเหลือ: {0} บาท จำนวนเงินรวมของสมุดเล่มนี้ {1} บาท", DaHelper.ToMoneyFormat(depInfo.RemainAmount), DaHelper.ToMoneyFormat(depInfo.TotalBillAmount));

                            if (Authorization.IsAuthorized(SecurityNames.OverBookDepositLimit, true, caption))
                            {
                                WaitingFormHelper.ShowWaitingForm();
                                string billBookId = _createBillbookService.GetNewBillBookId(Session.Branch.Id);
                                WaitingFormHelper.HideWaitingForm();

                                _bookInputList.HeaderInfo.BillBookId = billBookId;
                                WaitingFormHelper.ShowWaitingForm();
                                bookHeader = _createBillbookService.CreateBillBookAndSendPrintEvent(null, _bookInputList);                                
                                WaitingFormHelper.HideWaitingForm();
                                bookHeader.CreatorName = String.Format(" {0} {1}", Session.User.Id, Session.User.Name);
                                //try
                                //{
                                    //--Start--RealTime-AG: BillBookInvoice BPM-->SAP -----
                                    //--NO USE FOR NOW!!
                                    //if (Session.IsNetworkConnectionAvailable)
                                    //{
                                    //    if (Session.Branch.OnlineConnection)
                                    //    {
                                    //        Authorization.SignalExportBillBook(PEA.BPM.Infrastructure.Interface.Constants.LocalSettingNames.DL010_EXPORT_BILLBOOK_INVOICE_BATCH, bookHeader.BillBookId, Session.User.Id);
                                    //    }
                                    //}
                                    //--End----RealTime-AG------------------------------
                                //}
                                //catch (Exception)
                                //{ }
                            }
                            else
                                return;
                        }
                        else //just create
                        {

                            //verify! remove the input prompt line (last row)
                            if (_bookInputList.CreationItemList.Count > 0)
                            {
                                if (_bookInputList.CreationItemList[_bookInputList.CreationItemList.Count - 1].LineId.Trim() == "9999")
                                {
                                    _bookInputList.CreationItemList.RemoveAt(_bookInputList.CreationItemList.Count - 1);
                                }
                            }
                            
                            WaitingFormHelper.ShowWaitingForm();
                            string billBookId = _createBillbookService.GetNewBillBookId(Session.Branch.Id);
                            WaitingFormHelper.HideWaitingForm();
                            _bookInputList.HeaderInfo.BillBookId = billBookId;
                            //put exceptional bill list         
                            WaitingFormHelper.ShowWaitingForm();
                            bookHeader = _createBillbookService.CreateBillBookAndSendPrintEvent(null, _bookInputList);
                            WaitingFormHelper.HideWaitingForm();
                            bookHeader.CreatorName = String.Format(" {0} {1}", Session.User.Id, Session.User.Name);

                            //try
                            //{
                                //--Start--RealTime-AG: BillBookInvoice BPM-->SAP -----
                                //--NO USE FOR NOW!!
                                //if (Session.IsNetworkConnectionAvailable)
                                //{
                                //    if (Session.Branch.OnlineConnection)
                                //    {
                                //        Authorization.SignalExportBillBook(PEA.BPM.Infrastructure.Interface.Constants.LocalSettingNames.DL010_EXPORT_BILLBOOK_INVOICE_BATCH, bookHeader.BillBookId, Session.User.Id);
                                //    }
                                //}
                                //--End----RealTime-AG------------------------------
                            //}
                            //catch (Exception)
                            //{ }
                        }

                        if (bookHeader != null && bookHeader.BillBookId != null)
                        {
                            WaitingFormHelper.HideWaitingForm();
                            PrintBillBook(bookHeader);
                            _billBookManagementView.PrintBt(false);
                            MessageBox.Show(null, "พิมพ์เรียบร้อยแล้ว กดปุ่ม OK เพื่อทำรายการต่อ", "บันทึก", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //reset billbook
                            ShowMainView();
                            _billBookSearchView.ActivateBookCreationPanel();
                            _billBookManagementView.MoveCursorToStart();
                            _billBookSearchView.StartInput();

                        }
                        else
                        {
                            WaitingFormHelper.HideWaitingForm();
                            MessageBox.Show(null, "ไม่สามารถสร้างสมุดจ่ายบิลได้ \nเหตุผล: " + bookHeader.CreateFailReason, "บันทึกผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "สร้างสมุด", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }


        [EventPublication(EventTopicNames.ClearBookScreenHeader, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> ClearBookScreenHeaderHandler;
        public void ClearBookScreenHeaderClicked()
        {
            if (ClearBookScreenHeaderHandler != null)
                ClearBookScreenHeaderHandler(this, new EventArgs());
        }

        //for display current information of billbook before cancel
        [EventPublication(EventTopicNames.RetrieveBookSummary, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> RetrieveBookSummaryHandler;
        public void RetrieveOldBookSummary()
        {
            if (RetrieveBookSummaryHandler != null)
                RetrieveBookSummaryHandler(this, new EventArgs());
        }

        [EventPublication(EventTopicNames.BillBookSaveAndPrint, PublicationScope.Global)]
        public event EventHandler<EventArgs> PrintBillBookReportHandler;
        public void PrintBillBook(BillBookHeaderInfo billBookHeader)
        {
            if (PrintBillBookReportHandler != null)
                PrintBillBookReportHandler(this, new EventArgs<BillBookHeaderInfo>(billBookHeader));
        }

        [EventPublication(EventTopicNames.EnableBookCreationInput, PublicationScope.Global)]
        public event EventHandler<EventArgs> EnableBookCreationInputHandler;
        public void EnableBookInput(bool enable)
        {
            if (EnableBookCreationInputHandler != null)
                EnableBookCreationInputHandler(this, new EventArgs<bool>(enable));
        }


        [EventPublication(EventTopicNames.SetCancelBillBook, PublicationScope.Global)]
        public event EventHandler<EventArgs> SetCancelBillBookHandler;
        public void SetCancelBillBook(bool status)
        {
            if (SetCancelBillBookHandler != null)
                SetCancelBillBookHandler(this, new EventArgs<bool>(status));
        }


        //}
        [EventSubscription(EventTopicNames.SummaryViewSelectEvent, Thread = ThreadOption.UserInterface)]
        public void SummaryViewSelectEventClickedHandler(object sender, EventArgs<List<string>> e)
        {
            _fucusedLine = e.Data;
        }

        [EventSubscription(EventTopicNames.CheckAndLoadExistingBillBook, Thread = ThreadOption.UserInterface)]
        public void CheckAndLoadExistingBillBookHandler(object sender, EventArgs<string> e)
        {
            string billbookId = (string)e.Data;
            try
            {
                WaitingFormHelper.ShowWaitingForm();
                int status = _createBillbookService.IsReadyToCancelBillBook(billbookId);
                WaitingFormHelper.HideWaitingForm();
                if (status == 0)
                {
                    
                        //input list
                        BillBookItemListInputInfo bookItemList = _createBillbookService.LoadPastBillBookInfo(billbookId);

                        //just query past billbook and disable inputs
                        bookItemList.EnableSavePrint = true;
                        bookItemList.IsEditBillBook = true;                        

                        _billBookSearchView.SearchAgentInfo = bookItemList.AgencyInfo;
                        _billBookSearchView.FillPastBillBookHeader(bookItemList, true);
                        //set flag that this is old billbook, not search for bill needed
                        _billBookManagementView.FillPastBillBookInputSet(bookItemList, true);

                        SetCancelBillBook(true);
                        EnableBookInput(false);
                        RetrieveOldBookSummary();                    
                }
                else if (status == 1)
                {
                    WaitingFormHelper.HideWaitingForm();
                    MessageBox.Show(null, "สมุดจ่ายบิลมีรายการชำระเงินในบัญชี \nกรุณายกเลิกการชำระเงินก่อนยกเลิกสมุดจ่ายบิล", "ไม่อนุญาติ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    _billBookSearchView.SetDefaultCursor();

                }
                else if (status == 2)
                {
                    WaitingFormHelper.HideWaitingForm();
                    MessageBox.Show(null, "สมุดจ่ายบิลเล่มที่เลือกได้ถูกชำระเงินล่วงหน้า 30% จากตัวแทนแล้ว", "ไม่สามารถทำรายการได้", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    _billBookSearchView.SetDefaultCursor();
                }
                else if (status == 3)
                {
                    WaitingFormHelper.HideWaitingForm();
                    MessageBox.Show(null, "สมุดจ่ายบิลเล่มที่เลือกได้ถูกยกเลิกไปแล้ว \nกรุณายกเลิกการตัดชำระก่อนยกเลิกสมุดจ่ายบิล", "ไม่สามารถทำรายการได้", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    _billBookSearchView.SetDefaultCursor();
                }
                else
                {
                    WaitingFormHelper.HideWaitingForm();
                    MessageBox.Show(null, "ไม่พบสมุดจ่ายบิล \nกรุณาระบุหมายเลขสมุดจ่ายบิลอีกครั้ง", "ไม่สามารถทำรายการได้", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    _billBookSearchView.FocusCancelBillBook();
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "แก้ไขสมุดจ่ายบิล", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.CancelBillBook, Thread = ThreadOption.UserInterface)]
        public void CancelBillBookHandler(object sender, EventArgs e)
        {
            try
            {
                string billBookId = _billBookSearchView.CancelBillBookId;
                if (Authorization.IsAuthorized(SecurityNames.CancelBillBook, String.Format("Edit billbook ID: {0}" , billBookId), true))
                {                   
                    if (billBookId != String.Empty)
                    {
                        WaitingFormHelper.ShowWaitingForm();
                        BillBookItemListInputInfo bookItemList = _createBillbookService.LoadPastBillBookInfo(billBookId);
                        WaitingFormHelper.HideWaitingForm();
                        //leave this run for showing default cursor
                        _billBookSearchView.SetDefaultCursor();

                        if (bookItemList != null)
                        {
                            DialogResult dlg = MessageBox.Show("คุณต้องการยกเลิกสมุดจ่ายบิลใช่หรือไม่", "ยืนยันการยกเลิกสมุด", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (dlg == DialogResult.OK)
                            {

                                WaitingFormHelper.ShowWaitingForm();
                                bool cancelResult = _createBillbookService.CancelBillBook(bookItemList.HeaderInfo.BillBookId);
                                WaitingFormHelper.HideWaitingForm();
                                if (cancelResult)
                                {
                                    //try
                                    //{
                                    //    //--Start--RealTime-AG: BillBookInvoice BPM-->SAP -----
                                    //    if (Session.IsNetworkConnectionAvailable)
                                    //    {
                                    //        if (Session.Branch.OnlineConnection)
                                    //        {
                                    //            Authorization.SignalExportBillBook(PEA.BPM.Infrastructure.Interface.Constants.LocalSettingNames.DL010_EXPORT_BILLBOOK_INVOICE_BATCH, bookItemList.HeaderInfo.BillBookId, Session.User.Id);
                                    //        }
                                    //    }
                                    //    //--End----RealTime-AG------------------------------
                                    //}
                                    //catch (Exception)
                                    //{ }

                                    _billBookSearchView.FillPastBillBookHeader(bookItemList, false);
                                    _billBookManagementView.FillPastBillBookInputSet(bookItemList, false);

                                    //reset period, deposit, receive count, adv date, return date
                                    _billBookSearchView.RenewOldBook();
                                    _billBookManagementView.MoveCursorToStart();
                                    WaitingFormHelper.HideWaitingForm();
                                    MessageBox.Show(null, "ยกเลิกสมุดจ่ายบิลเรียบร้อย", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    SetCancelBillBook(false);
                                    _billBookSearchView.ClearHeader();
                                    _billBookManagementView.ClearInput();
                                    _billBookCallingBillView.Clear();

                                }
                                else
                                {
                                    WaitingFormHelper.HideWaitingForm();
                                    MessageBox.Show(null, "ไม่สามารถยกเลิกสมุดจ่ายบิล", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    _billBookSearchView.SetDefaultCursor();
                                }

                            }
                            else
                            {
                                _billBookSearchView.ClearHeader();
                                _billBookSearchView.StartInput();
                            }
                        }
                        else
                        {
                            WaitingFormHelper.HideWaitingForm();
                            MessageBox.Show(null, "ไม่พบสมุดจ่ายบิลเล่มที่ระบุ", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            _billBookSearchView.SetDefaultCursor();
                        }
                    }
                    else
                    {
                        WaitingFormHelper.HideWaitingForm();
                        MessageBox.Show(null, "กรุณาระบุหมายเลขสมุดจ่ายบิล เพื่อทำการยกเลิกสมุด", "ไม่อนุญาติ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        _billBookSearchView.SetDefaultCursor();

                    }
                }
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.AGENCY, "ยกเลิกการสร้างสมุด", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

        [EventSubscription(EventTopicNames.AddExceptionalCallingBill, Thread = ThreadOption.UserInterface)]
        public void AddExceptionalCallingBillActivatedHandler(object sender, EventArgs<CallingBillInfo> e)
        {
            CallingBillInfo bInfo = (CallingBillInfo)e.Data;
            _exceptionalBillList.Sort();
            int index = _exceptionalBillList.BinarySearch(bInfo);

            if (index < 0)
                _exceptionalBillList.Add(e.Data);
        }

        [EventSubscription(EventTopicNames.RemoveExceptionalCallingBill, Thread = ThreadOption.UserInterface)]
        public void RemoveExceptionalCallingBillActivatedHandler(object sender, EventArgs<CallingBillInfo> e)
        {
            CallingBillInfo bInfo = (CallingBillInfo)e.Data;
            _exceptionalBillList.Sort();
            int index = _exceptionalBillList.BinarySearch(bInfo);

            if (index >= 0)
                _exceptionalBillList.RemoveAt(index);
        }

        [EventSubscription(EventTopicNames.BackToBookManagementView, Thread = ThreadOption.UserInterface)]
        public void BackToBookManagementViewActivatedHandler(object sender, EventArgs e)
        {
            _billBookSearchView.ActivateBookCreationPanel();
            _billBookManagementView.MoveCursorToStart();
        }

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }

        [EventSubscription(EventTopicNames.ValidateCaIdInput, Thread = ThreadOption.UserInterface)]
        public void ValidateCaIdInputHandler(object sender, EventArgs<ArrayList> e)
        {
            ArrayList parem = e.Data;
            string input = (string)parem[0];
            int viewType = (int)parem[1];
            string caId = null;
            string mruId = null;
            string branchId = null;
            bool valid = ValidateInput(input, ref caId, ref mruId, ref branchId);
            if (valid)
            {
                if (viewType == 1)
                    _billBookCallingBillView.FillBillSearchInfo(branchId, mruId, caId);
                else if (viewType == 2)
                    _billingBookNoneCallingBillView.FillBillSearchInfo(branchId, mruId, caId);
                else if (viewType == 3)
                    _lineBillView.FillBillSearchInfo(branchId, mruId, caId);
                else if (viewType == 4)
                    _lineNoneBillView.FillBillSearchInfo(branchId, mruId, caId);
            }
        }

        private bool ValidateInput(string input, ref string cId, ref string mId, ref string bId)
        {
            bool ret = false;

            if (input.Length == 32) //barcode
            {
                //extract bracnhId and caId 
                //string temp = input.Substring(1, 7);
                //string branchId = Utilities.TextUtility.MapBranch(temp);
                string caId = input.Substring(8, 12);
                string[] bret = _createBillbookService.GetMruByCaId(caId);
                bId = bret[0];
                mId = bret[1];
                cId = caId;
                ret = true;
            }

            return ret;
        }

        private string UnFormatPeriod(string period)
        {
            string[] elements = period.Split('/');
            if (elements.Length == 2)
            {
                string month = elements[0];
                string year = elements[1];
                if (year.Length == 2)
                {
                    year = "25" + year;
                }

                return string.Format("{0}{1}", year, month);
            }
            else
            {
                return "000000";
            }
        }
    }
}
