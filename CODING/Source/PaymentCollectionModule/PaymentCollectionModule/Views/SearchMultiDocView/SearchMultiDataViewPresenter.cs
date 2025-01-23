using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using System.Windows.Forms;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.ComponentModel;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.Views.SearchMultiDocView
{
    /// <summary>
    /// DCR 67-020 Search invoice by multi doc.
    /// </summary>
    public class SearchMultiDataViewPresenter : Presenter<ISearchMultiDataView>
    {
        SearchByMultiDocParam _searchTypeParam;

        public IBillingService _billingService;
        //List<Invoice> _invoiceResultList;

        CustomerSearchParam _searchByCaIdParam;
        GroupInvoiceSearchParam _searchByGroupInvoinceNoParam;
        OneTouchSearchParam _searchByNotificationParam;

        BackgroundWorker bgWorker;


        [InjectionConstructor]
        public SearchMultiDataViewPresenter([ServiceDependency] IBillingService billingService)
        {
            _billingService = billingService;

            // Set properties of project.
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_InvoiceSearchedByMultiDocNoDoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_InvoiceSearchedByMultiDocNoCompleted);
        }

        /// <summary>
        /// DCR 67-020 Search invoice by CaId
        /// </summary>
        /// 
        [EventSubscription(EventTopicNames.InvoiceSearchedByMultiCustomerId, Thread = ThreadOption.UserInterface)]
        public void InvoiceSearchedByMultiCustomerIdHandler(object sender, EventArgs<SearchByMultiDocParam> e)
        {
            // Search type parameter
            _searchTypeParam = e.Data;
            
            // Set search type to view.
            View.SearchType = e.Data;

            ShowView();
        }

        private void ShowView()
        {
            PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
            // ปิดปุ่ม Cancel ไว้ก่อน. 
            //info.Keys.Add(WindowWorkspaceSetting.CancelButton, View.CancelButton);
            info.MaximizeBox = false;
            info.MinimizeBox = false;

            // Clear infomation on view.
            View.initView();

            // Search multi doc type 
            // "1" : CaId , "2" : Group invoicing , "3" : Notification.
            switch (_searchTypeParam.SearchTypeParam)
            {
                case "1" :
                    info.Title = " ## ค้นหาข้อมูลหนี้โดยหมายเลขผู้ใช้ไฟฟ้า";
                    break; 
                case "2" :
                    info.Title = " ## ค้นหาข้อมูลหนี้โดยเลขที่ มท.";
                    break;
                case "3":
                    info.Title = " ## ค้นหาข้อมูลหนี้โดยเลขที่ใบคำร้อง";
                    break; 
            }

            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(View, info);
        }

        public void ConvertStringToDocumentNoList(string stringMultiDocumentNo)
        {
            List<string> splitDocNoList = new List<string>();
            List<SearchByMultiDoc> docNoList = new List<SearchByMultiDoc>();
            string caIdFromText = string.Empty; 
            try
            {
                if (View.InvoiceResultList.Count > 0 )
                    View.InvoiceResultList.Clear();

                splitDocNoList = stringMultiDocumentNo.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Distinct().ToList();

                // Assign data back to view. 
                foreach (var splitDocNo in splitDocNoList)
                {
                    caIdFromText = string.Empty;
                    if (splitDocNo.Trim().Length > 0)  // check length text 
                    {
                        if (View.SearchType.SearchTypeParam == "1")
                            caIdFromText = GetCaIDFromText(splitDocNo); // Get caId from text.
                        else
                            caIdFromText = splitDocNo.Trim();

                        if (caIdFromText.Trim().Length > 0 )
                        {
                            SearchByMultiDoc caIdResult = new SearchByMultiDoc();
                            caIdResult.DocumentNo = caIdFromText;
                            caIdResult.Status = string.Empty;
                            caIdResult.Result = string.Empty;
                            docNoList.Add(caIdResult);   
                        }
                    }
                }

                // Call view add data. 
                View.AddSearchByMuliDocList(docNoList);
            }
            catch (Exception)
            {
            }

        }

        private string GetCaIDFromText(string inputCaIdText) 
        {
            string result = inputCaIdText;
            //decimal caIdDec = 0; 
            //bool resultConvert = false;
            try
            {
                if (inputCaIdText.Trim() == string.Empty)
                    return string.Empty;

                // Length >= 6 And < 12
                if (inputCaIdText.Trim().Length >= 6 && inputCaIdText.Trim().Length <= 12)
                    result = inputCaIdText.Trim().PadLeft(12, '0');
                else 
                    result = string.Empty;

                // Test convert to numeric  
                //resultConvert = decimal.TryParse(result, out caIdDec);
                //if (resultConvert == false)
                //    result = string.Empty;

            }
            catch (Exception)
            {
            }

            return result; 
        }

        public void ProcessStepByStep()
        {
            //if (View.InvoiceResultList.Count > 0)
            View.PrepareSearch();

            bgWorker.RunWorkerAsync();
        }

        [EventPublication(EventTopicNames.InvoiceItemAddByMultiSearch, PublicationScope.Global)]
        public event EventHandler<EventArgs<List<Invoice>>> InvoicesAddedToPayingList;
        public void InvoicesAddedToList() 
        {
            if (InvoicesAddedToPayingList != null)
                InvoicesAddedToPayingList(this, new EventArgs<List<Invoice>>(View.InvoiceResultList));
        }

        void bgWorker_InvoiceSearchedByMultiDocNoDoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                if (View.InvoiceResultList == null)
                    View.InvoiceResultList = new List<Invoice>();

                View.InvoiceResultList = new List<Invoice>();

                // Set properties of search parameter 
                switch (_searchTypeParam.SearchTypeParam)
                {
                    case "1":
                        _searchByCaIdParam = new CustomerSearchParam();
                        _searchByCaIdParam.IsSearByBP = true;
                        break;

                    case "2":
                        _searchByGroupInvoinceNoParam = new GroupInvoiceSearchParam();
                        _searchByGroupInvoinceNoParam.BranchId = Session.Branch.Id;
                        break;
                    case "3":
                        _searchByNotificationParam = new OneTouchSearchParam();
                        break;
                }

                // Loop by item 
                foreach (var item in this.View.SearchByMuliDocList)
                {
                    // ต้องการเก็บ Error ที่เกิดขึ้นในการค้นหาหนี้ของแต่ละ CaId
                    try
                    {
                        // Search by type 
                        switch (_searchTypeParam.SearchTypeParam)
                        {
                            case "1": // Search by contract account 
                                _searchByCaIdParam.CaId = item.DocumentNo;
                                List<Invoice> invoices = _billingService.SearchInvoiceByCustomerId(_searchByCaIdParam);
                                if (invoices != null && invoices.Count > 0)
                                    View.InvoiceResultList.AddRange(invoices);

                                // Status and result.
                                item.Status = "สำเร็จ";
                                item.InvoiceCount = invoices.Count;

                                View.SearchSuccessCount = View.SearchSuccessCount + 1;
                                View.InvoiceCount = View.InvoiceCount + invoices.Count;
                                break;

                            case "2": // Search by group invoice number.
                                // Param of group invoice
                                _searchByGroupInvoinceNoParam.InvoiceNo = item.DocumentNo;
                                List<Invoice> groupInvoicesList = _billingService.SearchInvoiceByGroupInvoiceNo(_searchByGroupInvoinceNoParam);
                                if (groupInvoicesList != null && groupInvoicesList.Count > 0)
                                    View.InvoiceResultList.AddRange(groupInvoicesList);

                                // Status and result.
                                item.Status = "สำเร็จ";
                                item.InvoiceCount = groupInvoicesList.Count;

                                View.SearchSuccessCount = View.SearchSuccessCount + 1;
                                View.InvoiceCount = View.InvoiceCount + groupInvoicesList.Count;
                                break;

                            case "3": // Search by notification number. 
                                _searchByNotificationParam.NotificationNo = item.DocumentNo;
                                List<Invoice> notiInvoiceList = _billingService.SearchInvoiceFromOneTouch(_searchByNotificationParam);
                                if (notiInvoiceList != null && notiInvoiceList.Count > 0)
                                {
                                    if (ValidateOneTouchData(notiInvoiceList) == true)
                                    {
                                        // Status and result.
                                        item.Status = "สำเร็จ";
                                        item.InvoiceCount = notiInvoiceList.Count;

                                        View.SearchSuccessCount = View.SearchSuccessCount + 1;
                                        View.InvoiceCount = View.InvoiceCount + notiInvoiceList.Count;
                                        View.InvoiceResultList.AddRange(notiInvoiceList);
                                    }
                                    else 
                                    {
                                        // Status and result.
                                        item.Status = "ข้อมูลไม่ถูกต้อง";
                                        item.Result = "พบข้อมูลบางรายการไม่ถูกต้อง";
                                    }
                                }
                                else if (notiInvoiceList != null && notiInvoiceList.Count == 0)
                                {
                                    item.Status = "สำเร็จ";
                                    item.InvoiceCount = notiInvoiceList.Count;

                                    View.SearchSuccessCount = View.SearchSuccessCount + 1;
                                }

                               
                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        item.Status = "ค้นหาไม่สำเร็จ";
                        item.Result = ex.Message;
                        View.SearchFailCount = View.SearchFailCount + 1;
                    }
                    this.View.RefreshGridDataSource();

                    if (bgWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        e.Result = "Canceled";
                        return;
                    }
                }

                e.Result = "Completed";

                View.NormalMode();

            }
            catch (Exception)
            {
            }
        }

        void bgWorker_InvoiceSearchedByMultiDocNoCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    View.DisplayCancelComplated();
                } if (e.Error != null)
                {
                    MessageBox.Show(e.Error.Message, "ค้นหาข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                } if (e.Result != null &&  e.Result == "Completed")
                {
                    View.DisplaySuccuess();
                }

                // Set view to normal
                View.NormalMode();
            }
            catch (Exception)
            {
            }
        }

        public void CancelWorkAsync() 
        {
            bgWorker.CancelAsync();
        }

        private bool ValidateOneTouchData(List<Invoice> invoices)
        {
            foreach (Invoice inv in invoices)
            {
                if (inv.NotificationNo != null)
                {
                    try
                    {
                        if (
                            inv.CaId == null ||
                            inv.Name == null ||
                            inv.Address == null ||
                            inv.BranchId == null ||
                            inv.InvoiceNo == null ||
                            inv.Bills[0].DebtId == null ||
                            inv.DebtType == null ||
                            inv.Bills[0].TaxCode == null ||
                            inv.Bills[0].TaxRate == null ||
                            inv.Qty == null ||
                            inv.AmountExVat == null ||
                            inv.VatAmount == null ||
                            inv.GAmount == null
                        )
                        {
                            return false;
                        }
                       
                    }
                    catch (Exception)
                    {
                        // object มีปัญหาจะเข้า catch.
                        return false;
                    }
                   
                }
            }
            return true;
        }

    }
}
