using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.AgencyManagementModule.SG.ReportMgtWCF;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PEA.BPM.AgencyManagementModule.SG
{
    public class ReportMgtSG : IReportMgtService
    {
        private ReportMgtWCFServiceClient _ws;

        public ReportMgtSG(bool onlineConnection)
        {
            _ws = new ReportMgtWCFServiceClient();
            string absoluteUri = _ws.Endpoint.Address.Uri.AbsoluteUri;

            if (onlineConnection)
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Report);
                _ws = new ReportMgtWCFServiceClient("BasicHttpBinding_IReportMgtWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Center == null) Authorization.LoadServerToken(true);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Center;
            }
            else
            {
                absoluteUri = absoluteUri.Replace(Session.Server.RegisterProxyAddress, Session.Server.Address.Branch);
                _ws = new ReportMgtWCFServiceClient("BasicHttpBinding_IReportMgtWCFService", new EndpointAddress(absoluteUri));
                if (Session.User.Token.Branch == null) Authorization.LoadServerToken(false);
                _ws.AuthInfoValue = new AuthInfo();
                _ws.AuthInfoValue.UserId = Session.User.Id;
                _ws.AuthInfoValue.LocalIP = MachineInfo.GetLocalIP();
                _ws.AuthInfoValue.AuthToken = Session.User.Token.Branch;
            }
            _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
        }

        #region IReportMgtService Members

        public List<BillBookDetailReportListInfo> GetBillBookDetailReportList(string billBookId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<BillBookDetailReportListInfo>>(_ws.GetBillBookDetailReportList_Compress(billBookId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<BillBookInfoDetailReport> GetBillBookInfoDetailReport(string billBookId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<BillBookInfoDetailReport>>(_ws.GetBillBookInfoDetailReport_Compress(billBookId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB02_DetailReportInfo> GetAgencyMoneyReturnReport(string agencyIdFrom, string agencyIdTo, string periodFrom, string periodTo, string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB02_DetailReportInfo>>(_ws.GetAgencyMoneyReturnReport_Compress(agencyIdFrom, agencyIdTo, periodFrom, periodTo, branchId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<AgencyBillCollectionMasterReport> GetAgencyBillCollectionReport(string period, string branchId, string branchName)
        {
            try
            {
                return ServiceHelper.DecompressData<List<AgencyBillCollectionMasterReport>>(_ws.GetAgencyBillCollectionReport_Compress(period, branchId, branchName));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public decimal GetAdvPaidByBillBookId(string billBookId)
        {
            try
            {
                return _ws.GetAdvPaidByBillBookId(billBookId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CommissionVoucherInfoReport> FindAndDisplayAgencyCommissionReportInfo(CommissionVoucherConditionPrintReport commissionCoditionInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CommissionVoucherInfoReport>>(_ws.FindAndDisplayAgencyCommissionReportInfo_Compress(ServiceHelper.CompressData<CommissionVoucherConditionPrintReport>(commissionCoditionInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CommissionRegistInfoReport> FindAndDisplayAgencyCommissionRegistryReportInfo(CommissionVoucherConditionPrintReport commissionCoditionInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CommissionRegistInfoReport>>(_ws.FindAndDisplayAgencyCommissionRegistryReportInfo_Compress(ServiceHelper.CompressData<CommissionVoucherConditionPrintReport>(commissionCoditionInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB04_03DetailReportInfo> FindAndDisplayCAB04_03Detail(CommissionVoucherConditionPrintReport commissionConditionInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB04_03DetailReportInfo>>(_ws.FindCommissionRegistryDetailReport_Compress(ServiceHelper.CompressData<CommissionVoucherConditionPrintReport>(commissionConditionInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<ReturnBillBookBillPasteStatusReportInfo> FindAndDisplayPasteBillStatus(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<ReturnBillBookBillPasteStatusReportInfo>>(_ws.FindAndDisplayPasteBillStatus_Compress(ServiceHelper.CompressData<CheckInBillBookConditionInfoReport>(conditionPrintInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        //public KeepMoneyPlaneHeaderInfoReport FindKeepMoneyPlanHeaderReport(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        //{
        //    return ServiceHelper.DecompressData<KeepMoneyPlaneHeaderInfoReport>(_ws.FindKeepMoneyPlanHeaderReport_Compress(ServiceHelper.CompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
        //}

        public List<CAB07_01DetailReportInfo> FindDetailOfDataIntoRev701460Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB07_01DetailReportInfo>>(_ws.FindDetailOfDataIntoRev701460Report_Compress(ServiceHelper.CompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB08_01DetailReportInfo> FindDetailOfDataIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB08_01DetailReportInfo>>(_ws.FindDetailOfDataIntoRev701461Report_Compress(ServiceHelper.CompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB08_01AgencyInfo> FindAgentListIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB08_01AgencyInfo>>(_ws.FindAgentListIntoRev701461Report_Compress(ServiceHelper.CompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB09_01DetailReportInfo> FindDetailOfDataIntoRev701462Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB09_01DetailReportInfo>>(_ws.FindDetailOfDataIntoRev701462Report_Compress(ServiceHelper.CompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB08_02DetailReportInfo> FindDetailOfDataIntoRev701463Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB08_02DetailReportInfo>>(_ws.FindDetailOfDataIntoRev701463Report_Compress(ServiceHelper.CompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB10_01DetailReportInfo> FindDetailOfDataIntoRev701464Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB10_01DetailReportInfo>>(_ws.FindDetailOfDataIntoRev701464Report_Compress(ServiceHelper.CompressData<KeepMoneyPlaneOfAgencyConditionInfoReport>(myCondition)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB06_01DetailReportInfo> FindAndDisplayCAB06_01Detail(EvaluateAgencyReportCondition myComdition)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB06_01DetailReportInfo>>(_ws.FindAndDisplayCAB06_01Detail_Compress(ServiceHelper.CompressData<EvaluateAgencyReportCondition>(myComdition)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CAB06_01HeaderReportInfo FindAndDisplayCAB06_01Header(EvaluateAgencyReportCondition myComdition, List<CAB06_01DetailReportInfo> reportDetail)
        {
            try
            {
                return ServiceHelper.DecompressData<CAB06_01HeaderReportInfo>(_ws.FindAndDisplayCAB06_01Header_Compress(ServiceHelper.CompressData<EvaluateAgencyReportCondition>(myComdition), ServiceHelper.CompressData<List<CAB06_01DetailReportInfo>>(reportDetail)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CAB03_HeaderReportInfo FindAndDisplayCAB03_Header(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<CAB03_HeaderReportInfo>(_ws.FindAndDisplayCAB03_Header_Compress(ServiceHelper.CompressData<CheckInBillBookConditionInfoReport>(conditionPrintInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB03_DetailReportInfo> FindAndDisplayCAB03_Detail(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB03_DetailReportInfo>>(_ws.FindAndDisplayCAB03_Detail_Compress(ServiceHelper.CompressData<CheckInBillBookConditionInfoReport>(conditionPrintInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CAB03_02DetailReportInfo FindDisplayIssueBillARInfoDetail(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<CAB03_02DetailReportInfo>(_ws.FindDisplayIssueBillARInfoDetail_Compress(ServiceHelper.CompressData<CheckInBillBookConditionInfoReport>(conditionPrintInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CAB04_03HeaderReportInfo FindAndDisplayCAB04_03Header(CommissionVoucherConditionPrintReport commissionConditionInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<CAB04_03HeaderReportInfo>(_ws.FindAndDisplayCAB04_03Header_Compress(ServiceHelper.CompressData<CommissionVoucherConditionPrintReport>(commissionConditionInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB05_01DetailReportInfo> FindAndDisplayCAB05_01Detail(CAB05_01ConditionReportInfo conditionInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB05_01DetailReportInfo>>(_ws.FindAndDisplayCAB05_01Detail_Compress(ServiceHelper.CompressData<CAB05_01ConditionReportInfo>(conditionInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<PA_7034DetailReportInfo> FindAndDisplayPA_7034Detail(PA_7034ConditionReportInfo conditionInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<PA_7034DetailReportInfo>>(_ws.FindAndDisplayPA_7034Detail_Compress(ServiceHelper.CompressData<PA_7034ConditionReportInfo>(conditionInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB12_01DetailReportInfo> FindAndDisplayCAB12_01Detail(CAB12_01ConditionReportInfo conditionInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB12_01DetailReportInfo>>(_ws.FindAndDisplayCAB12_01Detail_Compress(ServiceHelper.CompressData<CAB12_01ConditionReportInfo>(conditionInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CAB12_01HeaderReportInfo FindAndDisplayCAB12_01Header(List<CAB12_01DetailReportInfo> detail, string branchName, string branchId)
        {
            try
            {
                return ServiceHelper.DecompressData<CAB12_01HeaderReportInfo>(_ws.FindAndDisplayCAB12_01Header_Compress(ServiceHelper.CompressData<List<CAB12_01DetailReportInfo>>(detail), branchName, branchId));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAN34_01DetailReportInfo> FindAndDisplayCAN34_01Detail(CAN34_01CondtionReportInfo conditionInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAN34_01DetailReportInfo>>(_ws.FindAndDisplayCAN34_01Detail_Compress(ServiceHelper.CompressData<CAN34_01CondtionReportInfo>(conditionInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public string GetBillKeeperNameByBillBook(string billBookId)
        {
            try
            {
                return _ws.GetBillKeeperNameByBillBook(billBookId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public decimal? GetIntownReceive(string billBookId)
        {
            try
            {
                return _ws.GetIntownReceive(billBookId);
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB12_02DetailReportInfo> FindAndDisplayCAB12_02Detail(CAB12_01ConditionReportInfo conditionInfo)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB12_02DetailReportInfo>>(_ws.FindAndDisplayCAB12_02Detail_Compress(ServiceHelper.CompressData<CAB12_01ConditionReportInfo>(conditionInfo)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CAB03_02ReportInfo FindAndDisplayCAB03_02Report(CheckInBillBookConditionInfoReport condition)
        {
            try
            {
                return ServiceHelper.DecompressData<CAB03_02ReportInfo>(_ws.FindAndDisplayCAB03_02Report_Compress(ServiceHelper.CompressData<CheckInBillBookConditionInfoReport>(condition)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CAB03_03ReportInfo FindAndDisplayCAB03_03Report(CheckInBillBookConditionInfoReport condition)
        {
            try
            {
                return ServiceHelper.DecompressData<CAB03_03ReportInfo>(_ws.FindAndDisplayCAB03_03Report_Compress(ServiceHelper.CompressData<CheckInBillBookConditionInfoReport>(condition)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public CAB03_04ReportInfo FindAndDisplayCAB03_04Report(CheckInBillBookConditionInfoReport condition)
        {
            try
            {
                return ServiceHelper.DecompressData<CAB03_04ReportInfo>(_ws.FindAndDisplayCAB03_04Report_Compress(ServiceHelper.CompressData<CheckInBillBookConditionInfoReport>(condition)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public BillBookInfoMasterReport GetBillBookInfoReport(BillBookHeaderInfo bookHeader)
        {
            try
            {
                return ServiceHelper.DecompressData<BillBookInfoMasterReport>(_ws.GetBillBookInfoReport_Compress(ServiceHelper.CompressData<BillBookHeaderInfo>(bookHeader)));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
            }
            finally
            {
                if (_ws.State != CommunicationState.Closed)
                {
                    _ws.Close();
                }
            }
        }

        public List<CAB13_01RptInfo> GetRptCAB13_01RptInfo(CAB13_01ConditionRptInfo condition)
        {
            try
            {
                return ServiceHelper.DecompressData<List<CAB13_01RptInfo>>(_ws.GetRptCAB13_01RptInfo_Commpress(condition));
            }
            catch (Exception ex)
            {
                _ws.Abort();
                throw ex;
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
