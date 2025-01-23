using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using System.Data.Common;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.Architecture.ArchitectureTool;
using System.Web.Services.Protocols;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PaymentSmartPlus.SG.BillingWCF;
using System.ServiceModel;
using WCFExtras.Soap;
using System.ComponentModel;
using System.Reflection;

namespace PaymentSmartPlus.SG
{
    public class BillingSG : IBillingService
    {
        private bool _onlineConnection;
        private BillingWCFServiceClient _ws;

        public BillingSG(bool onlineConnection)
        {
            _ws = new BillingWCFServiceClient();

            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Center);
                _ws = new BillingWCFServiceClient("BasicHttpBinding_IBillingWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new BillingWCFServiceClient("BasicHttpBinding_IBillingWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
        }

        private void SetInvoiceToOnlineToBpmServerMode(List<Invoice> invoices)
        {
            if (_onlineConnection)
            {
                foreach (Invoice invoice in invoices)
                {
                    SetInvoiceToOnlineToBpmServerMode(invoice);
                }
            }
        }

        private void SetInvoiceToOnlineToBpmServerMode(Invoice invoice)
        {
            if (_onlineConnection)
            {
                invoice.NetworkMode = NetworkMode.OnlineToBpmServer;
            }
        }

        private void SetInvoiceToOnlineToBpmServerMode(List<BillSearchDetail> bills)
        {
            if (_onlineConnection)
            {
                foreach (BillSearchDetail bill in bills)
                {
                    SetInvoiceToOnlineToBpmServerMode(bill);
                }
            }
        }

        private void SetInvoiceToOnlineToBpmServerMode(BillSearchDetail bill)
        {
            if (_onlineConnection)
            {
                bill.NetworkMode = NetworkMode.OnlineToBpmServer;
            }
        }

        #region IBillingService Members

        public List<Invoice> SearchInvoiceByCustomerId(CustomerSearchParam param)
        {
            try
            {
                List<Invoice> invoices = ServiceHelper.DecompressData<List<Invoice>>(_ws.SearchInvoiceByCustomerId_Compress(param));

                //Comment because.. Realtime Concept do not Pay by Direct Center Connection
                //SetInvoiceToOnlineToBpmServerMode(invoices);

                return invoices;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<Invoice> SearchInvoiceFromSAP(SAPSearchParam param)
        {
            try
            {
                List<Invoice> invoices = ServiceHelper.DecompressData<List<Invoice>>(_ws.SearchInvoiceFromSAP_Compress(param));

                //Comment because.. Realtime Concept do not Pay by Direct Center Connection
                //SetInvoiceToOnlineToBpmServerMode(invoices);

                return invoices;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }


        public Invoice SearchOriginalInvoiceByInstallmentItemCaDoc(OriginalInvoiceSearchParam param)
        {
            try
            {
                Invoice invoice = ServiceHelper.DecompressData<Invoice>(_ws.SearchOriginalInvoiceByInstallmentItemCaDoc_Compress(param));
                SetInvoiceToOnlineToBpmServerMode(invoice);
                return invoice;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<InstallmentInvoice> SearchInstallmentInvoice(string caDoc)
        {
            try
            {
                return ServiceHelper.DecompressData<List<InstallmentInvoice>>(_ws.SearchInstallmentInvoice_Compress(caDoc));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<Invoice> SearchInvoiceByGroupInvoiceNo(GroupInvoiceSearchParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Invoice>>(_ws.SearchInvoiceByGroupInvoiceNo_Compress(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<Invoice> SearchInvoiceItemByGroupInvoiceNo(InvoiceItemSearchParam param)
        {
            try
            {
                List<Invoice> invoices = ServiceHelper.DecompressData<List<Invoice>>(_ws.SearchInvoiceItemByGroupInvoiceNo_Compress(param));
                SetInvoiceToOnlineToBpmServerMode(invoices);
                return invoices;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<BillSearchDetail> SearchBillByCustomerDetail(CustomerSearchParam param)
        {
            try
            {
                List<BillSearchDetail> results = ServiceHelper.DecompressData<List<BillSearchDetail>>(_ws.SearchBillByCustomerDetail_Compress(param));
                SetInvoiceToOnlineToBpmServerMode(results);
                return results;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<Bill> SearchBillByBillBookId(string billBookId, string billBookStatus)
        {
            try
            {
                return ServiceHelper.DecompressData<List<Bill>>(_ws.SearchBillByBillBookId_Compress(billBookId, billBookStatus));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<BookSearchDetail> SearchBillByAgent(AgencySearchParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<BookSearchDetail>>(_ws.SearchBillByAgent_Compress(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<GroupInvoiceItem> GetGroupInvoiceItem(string billBookId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<GroupInvoiceItem>>(_ws.GetGroupInvoiceItem_Compress(billBookId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<PaymentMethod> SearchAGPayment(string billBookId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PaymentMethod>>(_ws.SearchAGPayment_Compress(billBookId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CaIdAndBranchId GetBranchIdByCaId(String caId)
        {
            try
            {
                return _ws.GetBranchIdByCaId(caId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public Customer GetCustomerDetailOnPaymentHistory(HistoryViewParam param)
        {
            try
            {
                return _ws.GetCustomerDetailOnPaymentHistory(param);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<PaidInvoice> GetPaymentHistory(HistoryViewParam param)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PaidInvoice>>(_ws.GetPaymentHistory_Compress(param));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public ResultPayInvoice PayInvoice(DbTransaction trans, List<Invoice> paidInvoices, List<PaymentMethod> paymentMethods,
            List<PrintingReceipt> receipts, List<PrintingReceipt> groupDividualReceipts, ExternalReceipt extReceipt,
            DateTime paymentDate, string posId, string terminalCode, string branchId, string branchName, decimal? payingAmount,
            string screenType, string cashierId, string cashierName, string workId, bool isOffline)
        {
            try
            {
                PayInvoice os = new PayInvoice();
                os.PaidInvoices = paidInvoices;
                os.PaymentMethods = paymentMethods;
                os.Receipts = receipts;
                os.GroupDividualReceipts = groupDividualReceipts;
                os.ExtReceipt = extReceipt;
                os.PaymentDate = paymentDate;
                os.PosId = posId;
                os.Terminalcode = terminalCode;
                os.BranchId = branchId;
                os.BranchName = branchName;
                os.PayingAmount = payingAmount;
                os.ScreenType = screenType;
                os.CashierId = cashierId;
                os.CashierName = cashierName;
                os.WorkId = workId;
                os.IsOffline = isOffline;

#if DEBUG
                //CompressData cd = _ws.PayInvoice(os);
                CompressData cd = _ws.PayInvoice_Compress(ServiceHelper.CompressData<PayInvoice>(os));
#else 
                CompressData cd = _ws.PayInvoice_Compress(ServiceHelper.CompressData<PayInvoice>(os));
#endif

                return ServiceHelper.DecompressData<ResultPayInvoice>(cd);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<BillBook> SearchBillBookByDetail(string billBookId, string agencyId, string agencyName)
        {
            try
            {
                return ServiceHelper.DecompressData<List<BillBook>>(_ws.SearchBillBookByDetail_Compress(billBookId, agencyId, agencyName));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BillBook GetBillBookDetail(string billBookId, string statusLineStr)
        {
            try
            {
                return _ws.GetBillBookDetail(billBookId, statusLineStr);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<AdvancePaymentHistory> SearchAdvancePaymentHistory(string billBookId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<AdvancePaymentHistory>>(_ws.SearchAdvancePaymentHistory_Compress(billBookId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public DayClosingResult CloseDayPayment(string branchId)
        {
            try
            {
                return _ws.CloseDayPayment(branchId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<DisconnectionDoc> SearchDisconnectionStatusByDiscNo(string discNo)
        {

            try
            {
                return ServiceHelper.DecompressData<List<DisconnectionDoc>>(_ws.SearchDisconnectionStatusByDiscNo_Compress(discNo));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }

            //return new List();
        }

        public bool CheckExistingInvoiceNo(string caId, string period)
        {
            try
            {
                return _ws.CheckExistingInvoiceNo(caId, period);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public DateTime GetServerTime()
        {
            return _ws.GetServerTime();
        }

        public LastDisconnect GetLastDisconnect(string caId)
        {
            try
            {
                return ServiceHelper.DecompressData<LastDisconnect>(_ws.GetLastDisconnect(caId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }


        public List<PaymentNonReceiptInfo> SearchPaymentNonReceipt(PaymentNonReceiptInfo receiptGen)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PaymentNonReceiptInfo>>(_ws.SearchPaymentNonReceipt(receiptGen));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void CreateReceiptService(List<PaymentNonReceiptInfo> paymentGenReList)
        {
            try
            {
                _ws.CreateReceiptService(ServiceHelper.CompressData<List<PaymentNonReceiptInfo>>(paymentGenReList));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CaAndBpInfo> GetCaAndBpOtherBranch(string caId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CaAndBpInfo>>(_ws.GetCaAndBpOtherBranch(caId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void SaveCaAndBpOtherBranch(List<CaAndBpInfo> list)
        {
            try
            {
                _ws.SaveCaAndBpOtherBranch(ServiceHelper.CompressData<List<CaAndBpInfo>>(list));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool GetPaidGAmount(string invoiceNo)
        {
            try
            {
                return _ws.GetPaidGAmount(invoiceNo);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool GetInActiveBillBook(string invoiceNo)
        {
            try
            {
                return _ws.GetInActiveBillBook(invoiceNo);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool GetDuplicateExtReceipt(string receiptId, string branchId)
        {
            try
            {
                return _ws.GetDuplicateExtReceipt(receiptId, branchId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public bool ValidatePaymentActive(string receiptId, bool isPayment)
        {
            try
            {
                return _ws.ValidatePaymentActive(receiptId, isPayment);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void OfflineLog(OfflineLogInfo logInfo)
        {
            try
            {
                _ws.OfflineLog(logInfo);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public ActivePayment GetActivePayment(SAPSearchParam param, bool renew)
        {
            try
            {
                //this might take so long
                int tmp = Convert.ToInt32(_ws.InnerChannel.OperationTimeout.TotalSeconds);
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);
                ActivePayment acp = ServiceHelper.DecompressData<ActivePayment>(_ws.GetActivePayment(param, renew));
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 0, tmp);
                return acp;
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void SaveActivePayment(ActivePayment acp)
        {
            try
            {
                _ws.SaveActivePayment(acp);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }


        public string CheckWorkStatus(OpenWorkParam param)
        {
            try
            {
                return _ws.CheckWorkStatus(param);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public TrayMoneyInfo GetMoneyInTray(string workId)
        {
            try
            {
                return _ws.GetMoneyInTray(workId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<LastReceiptPayment> GetRepayLastReceiptData(string receiptId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<LastReceiptPayment>>(_ws.GetRepayLastReceiptData(receiptId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }
        #endregion

        #region OneTouch
        public List<Invoice> SearchInvoiceFromOneTouch(OneTouchSearchParam param)
        {
            try
            {
                //return ServiceHelper.DecompressData<List<Invoice>>(_ws.SearchInvoiceFromOneTouch_Compress(param));
                return GetDataFromOneTouch(param);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private static List<Invoice> GetDataFromOneTouch(OneTouchSearchParam param)
        {
            OneTouch.CSS_BPM rfc = new OneTouch.CSS_BPM();
            DataTable dt = new DataTable();

            var InternalServiceDataTable        = new DataTable();          

            #region #ISSUE TAMS 2020AUG10
            string  InternalServicePrefixs      = CodeTable.Instant.GetAppSettingValue("INTERNAL_SERVICE_PREFIX");
            string  InternalServicePoolUrl      = CodeTable.Instant.GetAppSettingValue("INTERNAL_SERVICE_GATEWAY");
            string  InternalTimeoutText         = CodeTable.Instant.GetAppSettingValue("INTERNAL_SERVICE_TIMEOUT");
            int     InternalServiceTimeout      = 6000;
            bool    ConfirmUseInternalService   = false; 
            try
            {
                InternalServiceTimeout = Int32.Parse(InternalTimeoutText);
            }
            catch {
                InternalServiceTimeout = 6000;
            }
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var InternalService         = new InternalServicePools.ServicePools();
            InternalService.Url         = InternalServicePoolUrl;
            InternalService.Timeout     = InternalServiceTimeout;
            ConfirmUseInternalService   = InternalService.ConfirmServicePrefix(param.NotificationNo.Trim());
            #endregion

            #region #ISSUE VSPP 2018Jan10
            //var vsppService = new VSPPService.AppHttpWebServicesBpmWebServiceService();
            var vsppService = new VSPPService.PPIMService();
            var vsppInfos = new List<VSPPInfo>();
            bool ConfirmCallVSPP = false;
            if (ConfirmUseInternalService == true)
            {
                InternalServiceDataTable = InternalService.GetARByNotificationDataTable(param.NotificationNo.Trim());
            }
            else if (param.NotificationNo.Trim().Substring(0, 2).ToUpper() == "VS")  // อักษรสองตัวหน้า เป็น VS 
            {
                ConfirmCallVSPP = true;
                string VSPPGateWayUrl   = CodeTable.Instant.GetAppSettingValue("VSPP_GATEWAY");   //"https://ppim-intra.pea.co.th/web-services/bpm/services.wsdl";
                string VSPPTimeOut      = CodeTable.Instant.GetAppSettingValue("VSPP_TIMEOUT");      //"5000";
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                vsppService.Url         = VSPPGateWayUrl;
                vsppService.Timeout     = Int32.Parse(VSPPTimeOut);
                //VSPPService.AppHttpWebServicesPayment[] result = vsppService.SearchPayment(param.NotificationNo.Trim());   //"VSCS6000000029"
                VSPPService.Payment[] result = vsppService.SearchPayment(param.NotificationNo.Trim());   //"VSCS6000000029"                
                if (result.Length > 0)
                {
                    foreach (var r in result)
                    {
                        var m = new VSPPInfo();
                        if ((r.AmountExVat == 0 || r.Qty == null)
                            || String.IsNullOrEmpty(r.CaAddress)
                            || String.IsNullOrEmpty(r.CaId)
                            || String.IsNullOrEmpty(r.CaName)
                            || String.IsNullOrEmpty(r.CaTaxBranch)
                            || String.IsNullOrEmpty(r.CaTaxId)
                            || String.IsNullOrEmpty(r.DebtId)
                            || String.IsNullOrEmpty(r.DebtType)
                            || (r.GAmount == 0 || r.Qty == null)
                            || r.InvoiceDt == null
                            || String.IsNullOrEmpty(r.InvoiceNo)
                            || String.IsNullOrEmpty(r.NotificationNo)
                            || (r.Qty == 0 || r.Qty == null)
                            || String.IsNullOrEmpty(r.TaxCode)
                            || (r.TaxRate == 0 || r.TaxRate == null)
                            || String.IsNullOrEmpty(r.TechBranchId)
                            || (r.VatAmount == 0 || r.VatAmount == null))
                        {

                        }
                        else
                        {
                            #region
                            m.AmountExVat = r.AmountExVat;
                            m.CaAddress = r.CaAddress;
                            m.CaId = r.CaId;
                            m.CaName = r.CaName;
                            m.CaTaxBranch = r.CaTaxBranch;
                            m.CaTaxId = r.CaTaxId;
                            m.DebtId = r.DebtId;
                            m.DebtType = r.DebtType;
                            m.GAmount = r.GAmount;
                            m.InvoiceDt = r.InvoiceDt;
                            m.InvoiceNo = r.InvoiceNo;
                            m.NotificationNo = r.NotificationNo;
                            m.Qty = r.Qty;
                            m.TaxCode = r.TaxCode;
                            m.TaxRate = r.TaxRate;
                            m.TechBranchId = r.TechBranchId;
                            m.VatAmount = r.VatAmount;
                            #endregion
                            ConfirmCallVSPP = true;
                            vsppInfos.Add(m);
                        }
                    }
                }
            }
            #endregion
            else
            {
                string OneTouchGateWayConn  = CodeTable.Instant.GetAppSettingValue("ONETOUCH_GATEWAY"); //"http://172.30.7.139/css_bpm.asmx";
                string OneTouchTimeOut      = CodeTable.Instant.GetAppSettingValue("ONETOUCH_TIMEOUT"); //"5000";
                rfc.Timeout = Int32.Parse(OneTouchTimeOut);
                rfc.Url = OneTouchGateWayConn;
            }

            try
            {
                List<OneTouchInfo> OneTouchData = new List<OneTouchInfo>();
                List<Invoice> invoices = new List<Invoice>();
                //#ISSUE TAMS 2020AUG10 
                if (ConfirmUseInternalService == true)
                {
                    dt = InternalServiceDataTable;
                }
                else if (ConfirmCallVSPP == true)
                {
                    dt = ListToDataTable(vsppInfos);
                }
                else
                {
                    dt = rfc.SelectReqCharge(param.NotificationNo).ReqChargeRecord;
                }


                //var OneTouchData = ds.ReqChargeRecord.TableName[0];
                if (dt.Rows.Count > 0)
                    foreach (DataRow dr in dt.Rows)
                    {
                        Bill b = new Bill();
                        Invoice inv = new Invoice();

                        b.CustomerId = dr["CaId"].ToString();
                        b.Name = dr["CaName"].ToString();
                        b.Address = dr["CaAddress"].ToString();
                        b.AccountClass = "";
                        if (ConfirmUseInternalService == true)
                        {
                            b.DebtId = dr["DebtId"].ToString();
                        }
                        else
                        {
                            b.DebtId = "M" + dr["DebtId"].ToString();
                        }
                        b.DebtType = dr["DebtType"].ToString();
                        b.InvoiceNo = dr["InvoiceNo"].ToString();
                        b.Period = "";
                        b.Qty = DaHelper.GetDecimal(dr, "Qty"); //(decimal)dr["Qty"]; 
                        b.FullQty = b.Qty;
                        b.DueDate = DateTime.Now;
                        b.UnitTypeId = null;
                        b.UnitTypeName = null;
                        b.TaxCode = dr["TaxCode"].ToString();
                        b.TaxRate = DaHelper.GetDecimal(dr, "TaxRate"); //(decimal)dr["TaxRate"];
                        b.VatAmount = DaHelper.GetDecimal(dr, "VatAmount"); //(decimal)dr["VatAmount"];
                        b.FullVatAmount = b.VatAmount;
                        b.GAmount = DaHelper.GetDecimal(dr, "GAmount"); //(decimal)dr["GAmount"];
                        b.FullGAmount = b.GAmount;
                        b.AmountExVat = b.GAmount - b.VatAmount;
                        b.FullAmount = b.AmountExVat;
                        b.BaseAmount = b.AmountExVat;
                        b.FullBaseAmount = b.BaseAmount;
                        b.ToPayQty = b.Qty;
                        b.ToPayGAmount = b.GAmount;
                        b.ToPayVatAmount = b.VatAmount;
                        b.DisConnectDate = null;
                        b.Description = dr["DebtType"].ToString();  //#ISSUE VSPP
                        //Tax 13 
                        b.CaTaxId = dr["CaTaxId"].ToString();
                        b.CaTaxBranch = dr["CaTaxBranch"].ToString();
                        b.ControllerId = "";// _controllerId;

                        b.NotificationNo = dr["NotificationNo"].ToString();
                        //if (true)
                        //{
                        //    b.DataState = BillDataStage.Offline;
                        //    inv.DataState = InvoiceDataStage.Offline;
                        //}
                        //else
                        //{
                        b.DataState = BillDataStage.NewItemOneTouch;
                        inv.DataState = InvoiceDataStage.NewItemOneTouch;
                        //}

                        inv.InvoiceNo = b.InvoiceNo;
                        inv.SpotBillInvoiceNo = b.InvoiceNo;
                        inv.BranchId = dr["TechBranchId"].ToString();
                        inv.TechBranchName = "";
                        inv.CommBranchId = "";//_commBranchId;
                        inv.CommBranchName = "";//_commBranchName;
                        inv.CaId = b.CustomerId;
                        inv.Name = b.Name;
                        inv.Address = b.Address;
                        inv.DueDate = b.DueDate;
                        inv.ControllerId = "";//_controllerId;
                        inv.ControllerName = "";//_controllerName;
                        inv.MruId = "";//_mruId;
                        inv.AmountExVat = b.AmountExVat;
                        inv.VatAmount = b.VatAmount;
                        inv.GAmount = b.GAmount;
                        inv.PaidVatAmount = 0;
                        inv.PaidGAmount = 0;
                        inv.PaidQty = 0;
                        inv.Qty = b.Qty;
                        inv.ToPayQty = b.Qty;
                        inv.ToPayVatAmount = b.VatAmount;
                        inv.ToPayGAmount = inv.ToBePaidGAmount;

                        //Tax 13 
                        inv.CaTaxId = b.CaTaxId;
                        inv.CaTaxBranch = b.CaTaxBranch;

                        inv.Bills = new List<Bill>();
                        inv.Bills.Add(b);

                        inv.NotificationNo = b.NotificationNo;

                        invoices.Add(inv);
                    }


                return invoices;

            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.POS, "SearchInvoiceFromOneTouch", ex.ToString());
                throw;
            }
        }

        public bool FlagOneTouchPayment(OneTouchLogInfo param)
        {
            try
            {
                #region #ISSUE TAMS 2020AUG10
                string InternalServicePrefixs   = CodeTable.Instant.GetAppSettingValue("INTERNAL_SERVICE_PREFIX");
                string InternalServicePoolUrl   = CodeTable.Instant.GetAppSettingValue("INTERNAL_SERVICE_GATEWAY");
                string InternalTimeoutText      = CodeTable.Instant.GetAppSettingValue("INTERNAL_SERVICE_TIMEOUT");
                int InternalServiceTimeout      = 6000;
                bool ConfirmUseInternalService = false;
                try
                {
                    InternalServiceTimeout = Int32.Parse(InternalTimeoutText);
                }
                catch
                {
                    InternalServiceTimeout = 6000;
                }
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var InternalService         = new InternalServicePools.ServicePools();
                InternalService.Url         = InternalServicePoolUrl;
                InternalService.Timeout     = InternalServiceTimeout;
                ConfirmUseInternalService   = InternalService.ConfirmServicePrefix(param.NotificationNo.Trim());
                if (ConfirmUseInternalService == true)
                {
                    bool UpdateResult = false;
                    if (param.Action == "3")    // 3 = Cancel,1 = Add
                    {
                        return UpdateResult;           // ไม่มีการส่งไปอัพเดทถ้าเป็นการ ยกเลิก
                    }
                    else
                    {
                        UpdateResult = InternalService.UpdatePayment(param.NotificationNo, param.InvoiceNo, param.ReceiptId, param.DebtId.Substring(1, 8));
                    }
                    return UpdateResult;
                }
                #endregion

                #region #ISSUE VSPP 2017DEC21
                else if (param.NotificationNo.Trim().Substring(0, 2).ToUpper() == "VS")  // อักษรสองตัวหน้า เป็น VS 
                {
                    if (param.Action == "3")    // 3 = Cancel,1 = Add
                    {
                        return false;           // ไม่มีการส่งไปอัพเดทถ้าเป็นการ ยกเลิก
                    }
                    else
                    {
                        var vsppService = new VSPPService.PPIMService();
                        var vsppInfos = new List<VSPPInfo>();
                        string VSPPGateWayUrl = CodeTable.Instant.GetAppSettingValue("VSPP_GATEWAY");     //"http://172.30.203.106/web-services/bpm/services.wsdl";
                        string VSPPTimeOut = CodeTable.Instant.GetAppSettingValue("VSPP_TIMEOUT");     //"3000";
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                        vsppService.Url = VSPPGateWayUrl;
                        vsppService.Timeout = Int32.Parse(VSPPTimeOut);
                        string result = vsppService.UpdatePayment(param.NotificationNo, param.InvoiceNo, param.ReceiptId, param.DebtId.Substring(1, 8));   //"VSCS6000000029"
                        if (result.Trim().ToString() == "Y")
                            return true;
                        else
                            return false;
                    }
                }
                #endregion

                #region #UPDATE ONETOUCH
                else
                {
                    #region Original Onetouch Update Payment
                    string OneTouchGateWayConn = CodeTable.Instant.GetAppSettingValue("ONETOUCH_GATEWAY"); //"http://172.30.7.139/css_bpm.asmx";
                    string OneTouchTimeOut = CodeTable.Instant.GetAppSettingValue("ONETOUCH_TIMEOUT"); //"5000";

                    OneTouch.CSS_BPM rfc = new OneTouch.CSS_BPM();
                    //SAPRfcWS.SAPRfcService rfc = new SAPRfcWS.SAPRfcService();
                    rfc.Timeout = Int32.Parse(OneTouchTimeOut);
                    rfc.Url = OneTouchGateWayConn;

                    var flag = rfc.UpdateReqCharge(param.NotificationNo, param.InvoiceNo, param.DebtId.Substring(1, 8), (decimal)param.VatAmount, (decimal)param.GAmount, param.ReceiptId);

                    if (flag.Return.ToString() == "Y")
                        return true;
                    else
                        return false;
                    #endregion
                }
                #endregion
            }
            catch
            {
                return false;
            }
        }


        public void SaveOneTouchLog(OneTouchLogInfo log)
        {
            try
            {
                _ws.SaveOneTouchLog(log);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }
        #endregion

        #region Offline Payment
        public List<OfflinePayment> GetOfflinePayment(string branchId, string cashierId, string workId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<OfflinePayment>>(_ws.GetOfflinePayment_Compress(branchId, cashierId, workId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public void UpdateOfflinePayment(string branchId, string cashierId, string posId, string workId)
        {
            try
            {
                _ws.UpdateOfflinePayment(branchId, cashierId, posId, workId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ExceptionHelper.ServiceGatewayConvertException(EErrorInModule.POS, ex);
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        #endregion
    }
}
