using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using PEA.BPM.AgencyManagementModule.DA;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule.BS
{
    //[Service(typeof(IReportMgtService))]
    public class ReportMgtService : IReportMgtService
    {
        private readonly IAgencyRepository _agencyrepo;
        private readonly IBillBookRepository _billbookrepo;
        private readonly ICommissionRepository _commissionrepo;
        private readonly IReportRepository _reportrepo;
        public ReportMgtService() : this(new AgencyDataAccess(), new BillBookDataAccess(), new CommissionDataAccess(), new ReportDataAccess())
        {
        }
        public ReportMgtService(IAgencyRepository agencyrepo, IBillBookRepository billbookrepo, ICommissionRepository commissionrepo, IReportRepository reportrepo)
        {
            _agencyrepo = agencyrepo;
            _billbookrepo = billbookrepo;
            _commissionrepo = commissionrepo;
            _reportrepo = reportrepo;
        }

        public decimal GetAdvPaidByBillBookId(string billBookid)
        {
            return _billbookrepo.GetAdvancePaymentByMRU(null, billBookid);
        }

        public List<BillBookDetailReportListInfo> GetBillBookDetailReportList(string billBookId)
        {
            return _billbookrepo.GetBillBookDetailReportList(billBookId);
        }

        public List<BillBookInfoDetailReport> GetBillBookInfoDetailReport(string billBookId)
        {
            return _billbookrepo.GetBillBookInfoDetailReportList(billBookId);            
        }

        public List<CAB02_DetailReportInfo> GetAgencyMoneyReturnReport(string agencyIdFrom, string agencyIdTo,
                                                                       string periodFrom, string periodTo, string branchId)
        {
            List<CAB02_DetailReportInfo> retVals = new List<CAB02_DetailReportInfo>();
            List<BranchIdInfoItem> branches = _commissionrepo.GetBranchUnderByBranchId(branchId);

            //branches.Add(new BranchIdInfoItem(branchId, string.Empty));

            foreach (BranchIdInfoItem b in branches)
            {
                List<CAB02_DetailReportInfo> itemList = _billbookrepo.GetAgencyMoneyReturnReportList(agencyIdFrom, agencyIdTo, periodFrom, periodTo, b.BranchId);
                foreach (CAB02_DetailReportInfo a in itemList)
                {
                    if (!retVals.Contains(a))
                        retVals.Add(a);
                }
            }

            return retVals;            
        }
        public List<AgencyBillCollectionMasterReport> GetAgencyBillCollectionReport(string period, string branchId, string branchName)
        {
            IAgencyRepository repo = _agencyrepo.NewInstance();            
            return repo.GetAgencyBillCollection(period, branchId, branchName);            
        }

        public List<CAB13_01RptInfo> GetRptCAB13_01RptInfo(CAB13_01ConditionRptInfo condition)
        {
            return _reportrepo.GetRptCAB13_01RptInfoData(condition);
        }

        #region "CommonMgtReportService"

        //Use For Calculate in Special Commission Section 
        //Create By Chettha Pattananitisak (Boy)
        private decimal? CalculateSpecialCommissionForVoucher(string branchid, int? billOut, decimal? amountBillOut, int? totalBillKeep, decimal? totalBillAmountKeep)
        {            
            CommissionRateInfo lineInfoRate1 = null;
            lineInfoRate1 = _commissionrepo.GetRateCommissionForCalculate(branchid);
            decimal? percentItemBill = (decimal)0.00; decimal? percentMomeyBill = (decimal)0.00;
            int? itemBill75Percent = 0; int? itemBill90Percent = 0;
            int? diffItemBill75To90Percent = 0; int? diffItemMoreThan90Percent = 0;
            decimal? amountCommission75To90 = (decimal)0.00; decimal? amountCommissionMorethan90 = (decimal)0.00;
            decimal? returnAmount = (decimal)0.00;
            percentItemBill = Convert.ToDecimal(string.Format("{0:#,###.00}", (Convert.ToDecimal(totalBillKeep) / Convert.ToDecimal(billOut)) * 100));
            if (amountBillOut != 0)
                percentMomeyBill = Convert.ToDecimal(string.Format("{0:#,###.00}", (totalBillAmountKeep / amountBillOut) * 100));

            if ((percentItemBill > 75) && (percentItemBill <= 90))
            {
                decimal billOut1 = Convert.ToDecimal(billOut);
                decimal rate75 = (decimal)0.75;
                itemBill75Percent = Convert.ToInt32(billOut1 * rate75);
                if (percentItemBill > 90)
                {
                    itemBill90Percent = billOut * (90 / 100);
                    diffItemBill75To90Percent = itemBill75Percent - itemBill90Percent;
                }
                else
                {
                    itemBill90Percent = Convert.ToInt32(billOut * (percentItemBill / 100));
                    diffItemBill75To90Percent = itemBill90Percent - itemBill75Percent;
                }
                amountCommission75To90 = diffItemBill75To90Percent * Convert.ToDecimal(lineInfoRate1.Special70To90Rate);
            }

            if ((percentItemBill > 90) && (percentItemBill < 100))
            {
                if (percentItemBill < percentMomeyBill)
                {
                    itemBill90Percent = billOut * itemBill90Percent;
                    diffItemMoreThan90Percent = (itemBill90Percent - itemBill75Percent - diffItemBill75To90Percent) - 1;
                    amountCommissionMorethan90 = diffItemMoreThan90Percent * Convert.ToDecimal(lineInfoRate1.SpecialMoreThan90Rate);
                }
                else
                {
                    itemBill90Percent = Convert.ToInt32(billOut * percentMomeyBill);
                    diffItemMoreThan90Percent = (itemBill90Percent - itemBill75Percent - diffItemBill75To90Percent) - 1;
                    amountCommissionMorethan90 = itemBill90Percent * Convert.ToDecimal(lineInfoRate1.SpecialMoreThan90Rate);
                }
            }
            returnAmount = amountCommission75To90 + amountCommissionMorethan90;
             
            return returnAmount;        
        }

        #endregion

        #region "CommissionMgtReportService"
        //Use For Calculate of Commission of Agency and issue Voucher for send to Account Section
        //Create By Chettha Pattananitisak (Boy)
        public List<CommissionVoucherInfoReport> FindAndDisplayAgencyCommissionReportInfo(CommissionVoucherConditionPrintReport commissionConditionInfo)
        {            
            IAgencyRepository repo = _agencyrepo.NewInstance();

            //Declare Variable Block
            //----------------------------------------Start Block--------------------------------------------------
            string agentType = ""; string agentId = ""; string bookPeriod = ""; string voucherId = "";
            int billOut = 0; decimal amountBillOut = 0; int billKeepResident = 0; decimal amountBillKeepResident = 0;
            int billKeepSmallBiz = 0; decimal amountBillKeepSmallBiz = 0; int billKeepGoverment = 0; string taxId = "";
            decimal amountBillKeepGoverment = 0; int totalBilPaste = 0; //int totalBillOutPasteThreeMonth = 0;
            int totalBillKeep = 0; decimal totalBillAmountKeep = 0; string cmdId = "";
            int billOutResident = 0; decimal amountBillOutResident = 0; int billOutSmallBiz = 0;
            decimal amountBillOutSmallBiz = 0; int billOutGoverment = 0; decimal amountBillOutGoverment = 0;
            decimal? specialInclude100Percent = 0;
            decimal? rateResidentCommission = 0; decimal? rateSmallBizCommission = 0; decimal? rateGovermentCommission = 0;
            int? billKeepComGovPaid = 0; decimal? amountBillKeepGovPaid = 0; 
            //decimal? rateGovPaidCommission = 0;

            //-----------------------------------------End Block--------------------------------------------------



            //Set Variable Block From Screen in Commission
            //----------------------------------------Start Block--------------------------------------------------                
            agentId = commissionConditionInfo.AgencyId;
            bookPeriod = commissionConditionInfo.PeriodBook;
            billOutResident = Convert.ToInt32(commissionConditionInfo.TotalBillOutResident);
            amountBillOutResident = Convert.ToDecimal(commissionConditionInfo.AmountBillOutResident);
            billOutSmallBiz = Convert.ToInt32(commissionConditionInfo.TotalBillOutSmallBiz);
            amountBillOutSmallBiz = Convert.ToDecimal(commissionConditionInfo.AmountBillOutSmallBiz);
            billOutGoverment = Convert.ToInt32(commissionConditionInfo.TotalBillOutGoverment);
            amountBillOutGoverment = Convert.ToDecimal(commissionConditionInfo.AmountBillOutGoverment);
            billOut = billOutResident + billOutSmallBiz + billOutGoverment;
            amountBillOut = amountBillOutResident + amountBillOutSmallBiz + amountBillOutGoverment;
            billKeepResident = Convert.ToInt32(commissionConditionInfo.TotalBillKeepResident);
            amountBillKeepResident = Convert.ToDecimal(commissionConditionInfo.AmountBillKeepResident);
            billKeepSmallBiz = Convert.ToInt32(commissionConditionInfo.TotalBillKeepSmallBiz);
            amountBillKeepSmallBiz = Convert.ToDecimal(commissionConditionInfo.AmountBillKeepSmallBiz);
            
            billKeepComGovPaid = Convert.ToInt32(commissionConditionInfo.TotalBillKeepGovPaid);
            amountBillKeepGovPaid = Convert.ToDecimal(commissionConditionInfo.AmountBillKeepGovPaid);

            billKeepGoverment = Convert.ToInt32(commissionConditionInfo.TotalBillKeepGoverment);
            amountBillKeepGoverment = Convert.ToDecimal(commissionConditionInfo.AmountBillKeepGoverment);
            totalBillKeep = billKeepResident + billKeepSmallBiz + billKeepGoverment;
            totalBillAmountKeep = amountBillKeepResident + amountBillKeepSmallBiz + amountBillKeepGoverment;
            totalBilPaste = Convert.ToInt32(commissionConditionInfo.TotalBillPasteCommission);
            //totalBillOutPasteThreeMonth = Convert.ToInt32(commissionConditionInfo.TotalBillPasteThreeMonth);
            cmdId = Convert.ToString(commissionConditionInfo.CmdId);
            taxId = _commissionrepo.GetTaxIdInAgencyByAgentId(agentId);
            //rateTaxNotRevenue = 0.07;//commissionDA.GetTaxRateOfTaxNotRevenue(agentId);

            //-----------------------------------------End Block--------------------------------------------------

            List<CommissionVoucherInfoReport> lineInfoList = new List<CommissionVoucherInfoReport>();
            CommissionRateInfo lineInfoRate = null;
            lineInfoRate = _commissionrepo.GetRateCommissionForCalculate(commissionConditionInfo.BranchId);
            agentType = _commissionrepo.GetAgencyTypeById(agentId);
            if (commissionConditionInfo != null)
            {
                CommissionVoucherInfoReport lineItem = new CommissionVoucherInfoReport();
                lineItem.TotalItemOfResidentType = commissionConditionInfo.TotalBillKeepResident;
                lineItem.TotalItemOfSmallBizType = commissionConditionInfo.TotalBillKeepSmallBiz;
                lineItem.TotalItemOfGovermentDepartmentType = commissionConditionInfo.TotalBillKeepGoverment;
                lineItem.TotalItemOfGovermentPaidType = commissionConditionInfo.TotalBillKeepGovPaid;
                lineItem.TotalItemOfSentBillType = (int?)commissionConditionInfo.TotalBillPasteCommission;
                lineItem.DebtAmount = _commissionrepo.GetAgencyDebtAmount(agentId.PadLeft(12, '0'));
               
                PeaInfo pea = repo.GetBranchInfoByAgencyId(agentId);
                lineItem.PEACode = pea.Id;
                lineItem.PEAName = pea.Name;

                if (agentType == "1")
                {

                    if (lineInfoRate.PersonRateForResident == null)
                    { rateResidentCommission = (decimal)0.00; }
                    else
                    { rateResidentCommission = lineInfoRate.PersonRateForResident; }

                    if (lineInfoRate.PersonRateForSmallBiz == null)
                    { rateSmallBizCommission = (decimal)0.00; }
                    else
                    { rateSmallBizCommission = lineInfoRate.PersonRateForSmallBiz; }

                    if (lineInfoRate.PersonRateForGoverment == null)
                    { rateGovermentCommission = (decimal)0.00; }
                    else
                    { rateGovermentCommission = lineInfoRate.PersonRateForGoverment; }

                }
                else
                {
                    if (lineInfoRate.CompanyRateForResident == null)
                    { rateResidentCommission = (decimal)0.00; }
                    else
                    { rateResidentCommission = lineInfoRate.CompanyRateForResident; }

                    if (lineInfoRate.CompanyRateForSmallBiz == null)
                    { rateSmallBizCommission = (decimal)0.00; }
                    else
                    { rateSmallBizCommission = lineInfoRate.CompanyRateForSmallBiz; }

                    if (lineInfoRate.CompanyRateForGoverment == null)
                    { rateGovermentCommission = (decimal)0.00; }
                    else
                    { rateGovermentCommission = lineInfoRate.CompanyRateForGoverment; }
                }
                decimal? amountComResident = billKeepResident * rateResidentCommission;
                lineItem.AmountCommissionOfResidentType = amountComResident;
                lineItem.RateOfResidentType = rateResidentCommission;
                decimal? amountComSmallBiz = billKeepSmallBiz * rateSmallBizCommission;
                lineItem.AmountCommissionOfSmallBizType = amountComSmallBiz;
                lineItem.RateOfSmallBizType = rateSmallBizCommission;
                decimal? amountComGoverment = billKeepGoverment * rateGovermentCommission;
                lineItem.AmountCommissionOfGovermentDepartmentType = amountComGoverment;
                lineItem.RateOfGovermentDepartmentType = rateGovermentCommission;

                decimal? amountComGovPaid = billKeepComGovPaid * rateResidentCommission;
                lineItem.AmountCommissionOfGovermentPaidType = amountComGovPaid;
                lineItem.RateOfGovermentPaidType = rateResidentCommission;


                lineItem.TotalMoneySpacialCase = commissionConditionInfo.SeventyFiveToNighty +
                                            commissionConditionInfo.NightyUp + commissionConditionInfo.Hundread;
                                            //+ commissionConditionInfo.TotalBillPasteCommission;
                                            


                lineItem.RateOfSentBillType = lineInfoRate.PasteBill;
                lineItem.AmountCommissionOfSentBillType = (totalBilPaste * lineInfoRate.PasteBill);
                lineItem.IssueCode = agentId;
                lineItem.PEACode = Session.Branch.Id;
                lineItem.PEAName = Session.Branch.Name;
                ArrayList myResultMoneyHelp = new ArrayList();
                myResultMoneyHelp = _commissionrepo.GetExtraMoneyHelpCommissionById(cmdId);
                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0:
                            lineItem.TransportOfCarPrice = Convert.ToDecimal(myResultMoneyHelp[i]);
                            break;
                        case 1:
                            lineItem.TransportOfWaterPrice = Convert.ToDecimal(myResultMoneyHelp[1]) + Convert.ToDecimal(myResultMoneyHelp[2]);
                            break;
                    }
                }
                lineItem.AllTransportPrice = lineItem.TransportOfCarPrice + lineItem.TransportOfWaterPrice;
                ArrayList myResultAgentDet = new ArrayList();
                myResultAgentDet = _commissionrepo.GetDetailAgencyById(agentId);
                for (int i = 0; i < 2; i++)
                {
                    switch (i)
                    {
                        case 0:
                            lineItem.IssueName = Convert.ToString(myResultAgentDet[i]);
                            break;
                        case 1:
                            lineItem.IssueAddress = Convert.ToString(myResultAgentDet[i]);
                            break;
                    }
                }
                lineItem.IssueTypeName = _commissionrepo.GetBusinessTypeDeesc(agentType);
                IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
                string currYear = Session.BpmDateTime.Now.ToString(formatDate);

                voucherId = commissionConditionInfo.CmdId.Substring(6, commissionConditionInfo.CmdId.Length - 6);
                lineItem.VoucherCode = voucherId;
                string currDate = Session.BpmDateTime.Now.ToString(formatDate);
                lineItem.VoucherDate = currYear;
                lineItem.IssueTaxNo = taxId;
                if (commissionConditionInfo.FineSendMoneyLate == null)
                {
                    lineItem.FineSendMoneyLate = (decimal)0.00;
                }
                else
                {
                    lineItem.FineSendMoneyLate = commissionConditionInfo.FineSendMoneyLate;
                }
                lineItem.TotalAllCommission = (lineItem.AmountCommissionOfResidentType +
                                                lineItem.AmountCommissionOfSmallBizType +
                                                lineItem.AmountCommissionOfGovermentDepartmentType +
                                                lineItem.AmountCommissionOfGovermentPaidType +
                                                lineItem.TotalMoneySpacialCase +
                                                lineItem.AllTransportPrice +
                                                specialInclude100Percent +
                                                lineItem.AmountCommissionOfSentBillType) ;
                lineItem.Vat7Percent = (decimal?)Math.Round((lineItem.TotalAllCommission.Value * _commissionrepo.GetAgencyVatRate(agentId).Value) / 100, 2, MidpointRounding.AwayFromZero);
                decimal? netCommission = 0;
                lineItem.TaxAmount = (decimal?)Math.Round((lineItem.TotalAllCommission.Value * _commissionrepo.GetAgencyTaxRate(agentId).Value) / 100, 2, MidpointRounding.AwayFromZero);
                netCommission = lineItem.TotalAllCommission + lineItem.Vat7Percent - lineItem.FineSendMoneyLate - lineItem.TaxAmount;

                lineItem.RealCommissionPaidAgency = netCommission;
                if (lineItem.RealCommissionPaidAgency != null && lineItem.RealCommissionPaidAgency >= 0)
                {
                    lineItem.TotalAllCommissionText = StringConvert.ConvertAmountToText(DaHelper.ToMoneyFormat(lineItem.RealCommissionPaidAgency));
                }
                lineItem.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                lineInfoList.Add(lineItem);
            }
             
            //bool result = UpdateVoucherIdToCommissionTable(cmdId, voucherId);
            return lineInfoList;
        }

        //Use For update VoucherId in Commission Table 
        //Create By Chettha Pattananitisak (Boy)
        private bool UpdateVoucherIdToCommissionTable(int cmdId, string voucherId)
        {
            bool result = _commissionrepo.UpdateVoucherIdByCmdId(cmdId, voucherId);
            return result;
        }

        //Comment because it duplicate between  CommonMgtReportService and CommissionMgtReportService
        ////Use For Calculate in Special Commission Section in Voucher   
        ////Create By Chettha Pattananitisak (Boy)
        //private decimal? CalculateSpecialCommissionForVoucher(int? billOut, decimal? amountBillOut, int? totalBillKeep, decimal? totalBillAmountKeep)
        //{
        //    CommissionRateInfo lineInfoRate1 = null;
        //    CommissionDataAccess commissionDA = new CommissionDataAccess();
        //    lineInfoRate1 = commissionDA.GetRateCommissionForCalculate();
        //    decimal? percentItemBill = (decimal)0.00; decimal? percentMomeyBill = (decimal)0.00;
        //    int? itemBill75Percent = 0; int? itemBill90Percent = 0;
        //    int? diffItemBill75To90Percent = 0; int? diffItemMoreThan90Percent = 0;
        //    decimal? amountCommission75To90 = (decimal)0.00; decimal? amountCommissionMorethan90 = (decimal)0.00;
        //    decimal? returnAmount = (decimal)0.00;
        //    percentItemBill = Convert.ToDecimal(string.Format("{0:#,###.00}", (Convert.ToDecimal(totalBillKeep) / Convert.ToDecimal(billOut)) * 100));
        //    percentMomeyBill = Convert.ToDecimal(string.Format("{0:#,###.00}", (totalBillAmountKeep / amountBillOut) * 100));
        //    if ((percentItemBill > 75) && (percentItemBill <= 90))
        //    {
        //        decimal billOut1 = Convert.ToDecimal(billOut);
        //        decimal rate75 = (decimal)0.75;
        //        itemBill75Percent = Convert.ToInt32(billOut1 * rate75);
        //        if (percentItemBill > 90)
        //        {
        //            itemBill90Percent = billOut * (90 / 100);
        //            diffItemBill75To90Percent = itemBill75Percent - itemBill90Percent;
        //        }
        //        else
        //        {
        //            itemBill90Percent = Convert.ToInt32(billOut * (percentItemBill / 100));
        //            diffItemBill75To90Percent = itemBill90Percent - itemBill75Percent;
        //        }
        //        amountCommission75To90 = diffItemBill75To90Percent * Convert.ToDecimal(lineInfoRate1.Special70To90Rate);
        //    }

        //    if ((percentItemBill > 90) && (percentItemBill < 100))
        //    {
        //        if (percentItemBill < percentMomeyBill)
        //        {
        //            itemBill90Percent = billOut * itemBill90Percent;
        //            diffItemMoreThan90Percent = (itemBill90Percent - itemBill75Percent - diffItemBill75To90Percent) - 1;
        //            amountCommissionMorethan90 = diffItemMoreThan90Percent * Convert.ToDecimal(lineInfoRate1.SpecialMoreThan90Rate);
        //        }
        //        else
        //        {
        //            itemBill90Percent = Convert.ToInt32(billOut * percentMomeyBill);
        //            diffItemMoreThan90Percent = (itemBill90Percent - itemBill75Percent - diffItemBill75To90Percent) - 1;
        //            amountCommissionMorethan90 = itemBill90Percent * Convert.ToDecimal(lineInfoRate1.SpecialMoreThan90Rate);
        //        }
        //    }
        //    returnAmount = amountCommission75To90 + amountCommissionMorethan90;
        //    return returnAmount;
        //}

        //Use For Get Detail of Commission of Agency that's Report include with Voucher
        //Create By Chettha Pattananitisak (Boy)
        public List<CommissionRegistInfoReport> FindAndDisplayAgencyCommissionRegistryReportInfo(CommissionVoucherConditionPrintReport commissionConditionInfo)
        {
            //Declare Variable Block
            //----------------------------------------Start Block--------------------------------------------------
            decimal? amountPrice = 0; string agentType = ""; string agentId = ""; string bookPeriod = ""; string taxId = "";
            int? billOut = 0; decimal? amountBillOut = 0; int? billKeepResident = 0; decimal? amountBillKeepResident = 0;
            int? billKeepSmallBiz = 0; decimal? amountBillKeepSmallBiz = 0; int? billKeepGoverment = 0;
            decimal? amountBillKeepGoverment = 0;  int? totalBillOutPasteThreeMonth = 0; 
            int? totalBillKeep = 0; decimal? totalBillAmountKeep = 0; string cmdId = "";
            int? billOutResident = 0; decimal? amountBillOutResident = 0; int? billOutSmallBiz = 0;
            decimal? amountBillOutSmallBiz = 0; int? billOutGoverment = 0; decimal? amountBillOutGoverment = 0;
            int? agentTypeId = 0; decimal? allTotalCommission = 0; decimal? totalBillPasteCommission = 0;
            decimal? rateVatNotRevenue = 0; int? billKeepGovPaid = 0; decimal? amountBillKeepGovPaid = 0;
            int? billOutGovPaid = 0; decimal? amountBIllOutGovPaid = 0; int? totalBillPaste = 0;
            decimal? taxRate = 0;
            //-----------------------------------------End Block--------------------------------------------------

            //Set Variable Block From Screen in Commission
            //----------------------------------------Start Block--------------------------------------------------
            agentId = commissionConditionInfo.AgencyId;
            bookPeriod = commissionConditionInfo.PeriodBook;
            billOutResident = Convert.ToInt32(commissionConditionInfo.TotalBillOutResident);
            amountBillOutResident = Convert.ToDecimal(commissionConditionInfo.AmountBillOutResident);
            billOutSmallBiz = Convert.ToInt32(commissionConditionInfo.TotalBillOutSmallBiz);
            amountBillOutSmallBiz = Convert.ToDecimal(commissionConditionInfo.AmountBillOutSmallBiz);
            billOutGoverment = Convert.ToInt32(commissionConditionInfo.TotalBillOutGoverment);
            amountBillOutGoverment = Convert.ToDecimal(commissionConditionInfo.AmountBillOutGoverment);
            billOutGovPaid = Convert.ToInt32(commissionConditionInfo.TotalBillKeepGovPaid);
            amountBIllOutGovPaid = Convert.ToDecimal(commissionConditionInfo.AmountBillOutGovPaid);

            billOut = billOutResident + billOutSmallBiz + billOutGoverment + billOutGovPaid;
            amountBillOut = amountBillOutResident + amountBillOutSmallBiz + amountBillOutGoverment + amountBIllOutGovPaid;
           
            billKeepResident = Convert.ToInt32(commissionConditionInfo.TotalBillKeepResident);
            amountBillKeepResident = Convert.ToDecimal(commissionConditionInfo.AmountBillKeepResident);
            billKeepSmallBiz = Convert.ToInt32(commissionConditionInfo.TotalBillKeepSmallBiz);
            amountBillKeepSmallBiz = Convert.ToDecimal(commissionConditionInfo.AmountBillKeepSmallBiz);                
            billKeepGoverment = Convert.ToInt32(commissionConditionInfo.TotalBillKeepGoverment);
            amountBillKeepGoverment = Convert.ToDecimal(commissionConditionInfo.AmountBillKeepGoverment);
            billKeepGovPaid = Convert.ToInt32(commissionConditionInfo.TotalBillKeepGovPaid);
            amountBillKeepGovPaid = Convert.ToDecimal(commissionConditionInfo.AmountBillKeepGovPaid);

            totalBillKeep = billKeepResident + billKeepSmallBiz + billKeepGoverment + billKeepGovPaid;
            totalBillAmountKeep = amountBillKeepResident + amountBillKeepSmallBiz + amountBillKeepGoverment + amountBillKeepGovPaid;
            totalBillPasteCommission = commissionConditionInfo.TotalBillPasteCommission;
            totalBillPaste = Convert.ToInt32(commissionConditionInfo.TotalBillPaste);
            
            totalBillOutPasteThreeMonth = Convert.ToInt32(commissionConditionInfo.TotalBillPasteThreeMonth);
            cmdId = Convert.ToString(commissionConditionInfo.CmdId);
            taxId = _commissionrepo.GetTaxIdInAgencyByAgentId(agentId);
            rateVatNotRevenue = _commissionrepo.GetAgencyVatRate(agentId);
            taxRate = _commissionrepo.GetAgencyTaxRate(agentId);
            //-----------------------------------------End Block--------------------------------------------------

            List<CommissionRegistInfoReport> lineInfoList = new List<CommissionRegistInfoReport>();
            CommissionRateInfo lineInfoRate = null;
            lineInfoRate = _commissionrepo.GetRateCommissionForCalculate(commissionConditionInfo.BranchId);
            agentType = _commissionrepo.GetAgencyTypeById(agentId);
            if (commissionConditionInfo != null)
            {
                CommissionRegistInfoReport lineItem = new CommissionRegistInfoReport();
                lineItem.AgencyName = commissionConditionInfo.AgencyName;
                lineItem.PEABranch = _commissionrepo.GetBranchIdByAgentId(agentId);
                lineItem.PEAName = _commissionrepo.GetBranchNameByAgentId(agentId);

                //Set Value Of Enitities from Variable by receive From Screen in Commission
                //----------------------------------------Start Block--------------------------------------------------
                lineItem.BillOutToAgencyForResident = billOutResident;
                lineItem.BillOutToAgencyForSmallBiz = billOutSmallBiz;
                lineItem.BillOutToAgencyForGovermentDepartment = billOutGoverment;
                lineItem.BillOutToAgencyForGovermentPaid = billOutGovPaid;

                lineItem.ValueOfBillOutForResident = amountBillOutResident;
                lineItem.ValueOfBillOutForSmallBiz = amountBillOutSmallBiz;
                lineItem.ValueOfBillOutForGovermentDepartment = amountBillOutGoverment;
                lineItem.ValueOfBillOutForGovermentPaid = amountBillKeepGovPaid;
                
                lineItem.CanKeepElectricForResident = billKeepResident;
                lineItem.CanKeepElectricForSmallBiz = billKeepSmallBiz;
                lineItem.CanKeepElectricForGovermentDepartment = billKeepGoverment;
                lineItem.CanKeepElectricForGovermentPaid = billKeepGovPaid;

                lineItem.ValueOfKeepElectricForResident = amountBillKeepResident;
                lineItem.ValueOfKeepElectricForSmallBiz = amountBillKeepSmallBiz;
                lineItem.ValueOfKeepElectricForGovermentDepartment = amountBillKeepGoverment;
                lineItem.ValueOfKeepElectricForGovermentPaid = amountBillKeepGovPaid;

                lineItem.RateOf75To90PercentForCommissionSpecial = lineInfoRate.Special70To90Rate;
                lineItem.RateOfMorethan90PercentForCommissionSpecial = lineInfoRate.SpecialMoreThan90Rate;
                lineItem.RateOfSendBill = lineInfoRate.PasteBill;
                lineItem.RateOfSendBillThreeMonth = lineInfoRate.PasteBillThreeMonthRate;
                lineItem.Rate100PercentForAllKeep = lineInfoRate.IncludeRateForKeepAllBill;
                lineItem.BillMonth = bookPeriod;
                lineItem.RegisterDate = commissionConditionInfo.CreateDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                lineItem.AgencyCode = agentId;
                lineItem.IsPersonType = commissionConditionInfo.IsPersonType;

                //-----------------------------------------End Block--------------------------------------------------

                //Condition Of Calculate amount Of Commission 
                //----------------------------------------Start Block--------------------------------------------------
                agentTypeId = Convert.ToInt32(_commissionrepo.GetAgencyTypeById(agentId));
                if (agentTypeId == 1)
                {
                    amountPrice = lineInfoRate.PersonRateForResident * billKeepResident;
                    allTotalCommission += amountPrice;
                    lineItem.AmountCommissionOfResident = amountPrice;
                    
                    amountPrice = lineInfoRate.PersonRateForSmallBiz * billKeepSmallBiz;
                    allTotalCommission += amountPrice;
                    lineItem.AmountCommissionOfSmallBiz = amountPrice;
                    
                    amountPrice = lineInfoRate.PersonRateForGoverment * billKeepGoverment;
                    allTotalCommission += amountPrice;
                    lineItem.AmountCommissionOfGovermentDepartment = amountPrice;

                    amountPrice = lineInfoRate.PersonRateForResident * billKeepGovPaid;
                    allTotalCommission += amountPrice;
                    lineItem.AmountCommissionOfGovermentPaid = amountPrice;

                    lineItem.RateAgencyPersonTypeForResident = lineInfoRate.PersonRateForResident;
                    lineItem.RateAgencyPersonTypeForSmallBiz = lineInfoRate.PersonRateForSmallBiz;
                    lineItem.RateAgencyPersonTypeForGovermentDepartment = lineInfoRate.PersonRateForGoverment;
                    lineItem.RateAgencyPersonTypeForGovermentPaid = lineInfoRate.PersonRateForResident;

                    lineItem.RateAgencyCompanyTypeForResident = lineInfoRate.CompanyRateForResident;
                    lineItem.RateAgencyCompanyTypeForSmallBiz = lineInfoRate.CompanyRateForSmallBiz; ;
                    lineItem.RateAgencyCompanyTypeForGovermentDepartment = lineInfoRate.CompanyRateForGoverment;
                    lineItem.RateAgencyCompanyTypeForGovermentPaid = lineInfoRate.CompanyRateForResident;
                }
                else
                {
                    amountPrice = lineInfoRate.CompanyRateForResident * billKeepResident;
                    allTotalCommission += amountPrice;
                    lineItem.AmountCommissionOfResident = amountPrice;

                    amountPrice = lineInfoRate.CompanyRateForSmallBiz * billKeepSmallBiz;
                    allTotalCommission += amountPrice;
                    lineItem.AmountCommissionOfSmallBiz = amountPrice;
                    
                    amountPrice = lineInfoRate.CompanyRateForGoverment * billKeepGoverment;
                    allTotalCommission += amountPrice;
                    lineItem.AmountCommissionOfGovermentDepartment = amountPrice;

                    amountPrice = lineInfoRate.CompanyRateForResident * billKeepGovPaid;
                    allTotalCommission += amountPrice;
                    lineItem.AmountCommissionOfGovermentPaid = amountPrice;

                    lineItem.RateAgencyCompanyTypeForResident = lineInfoRate.CompanyRateForResident;
                    lineItem.RateAgencyCompanyTypeForSmallBiz = lineInfoRate.CompanyRateForSmallBiz;
                    lineItem.RateAgencyCompanyTypeForGovermentDepartment = lineInfoRate.CompanyRateForGoverment;
                    lineItem.RateAgencyCompanyTypeForGovermentPaid = lineInfoRate.CompanyRateForResident;

                    lineItem.RateAgencyPersonTypeForResident = lineInfoRate.PersonRateForResident;
                    lineItem.RateAgencyPersonTypeForSmallBiz = lineInfoRate.PersonRateForSmallBiz;
                    lineItem.RateAgencyPersonTypeForGovermentDepartment = lineInfoRate.PersonRateForGoverment;
                    lineItem.RateAgencyPersonTypeForGovermentPaid = lineInfoRate.PersonRateForResident;

                }
                //-----------------------------------------End Block--------------------------------------------------

                lineItem.AgencyType = _commissionrepo.GetBusinessTypeDeesc(Convert.ToString(agentTypeId));
                lineItem.AgencyTaxNo = taxId;
                lineItem.TotalItemRepeatBillOfCommissionSpecial = 0;
                lineItem.ValueRepeatBillOfCommissionSpecial = (decimal)0.00;
                lineItem.TotalItemRepeatBillOfCommissionSpecialFollowStandard = 0;
                lineItem.ValueRepeatBillOfCommissionSpecialFollowStandard = (decimal)0.00;                    
                lineItem.TotalBillItemOf75To90PercentForCommissionSpecial = commissionConditionInfo.Bill75To90;
                lineItem.TotalBillItemOfMorethan90PercentForCommissionSpecial = commissionConditionInfo.Bill90Up;
                amountPrice = lineItem.TotalBillItemOf75To90PercentForCommissionSpecial * lineInfoRate.Special70To90Rate;
                lineItem.AmountCommission75To90PercentForCommissionSpecial = amountPrice;
                allTotalCommission += amountPrice;
                amountPrice = lineItem.TotalBillItemOfMorethan90PercentForCommissionSpecial * lineInfoRate.SpecialMoreThan90Rate;
                lineItem.AmountCommissionMorethan90PercentForCommissionSpecial = amountPrice;
                allTotalCommission += amountPrice;


                if ((billOut == totalBillKeep) && (totalBillKeep != 0))
                {
                    decimal? totalCom100Percent = lineItem.AmountCommissionOfResident + lineItem.AmountCommissionOfSmallBiz + lineItem.AmountCommissionOfGovermentDepartment;
                    lineItem.TotalItemInclude20PercentForCommissionBase = totalBillKeep;
                    amountPrice = (totalCom100Percent * (lineInfoRate.IncludeRateForKeepAllBill / 100));
                    lineItem.AmountInclude20PercentForCommissionBase = amountPrice;
                    allTotalCommission += amountPrice;
                }
                else
                {
                    lineItem.TotalItemInclude20PercentForCommissionBase = 0;
                    lineItem.AmountInclude20PercentForCommissionBase = (decimal)0.00;
                }

                //lineItem.TotalItemSendBill = totalBillPaste;
                lineItem.TotalItemSendBill = Convert.ToInt32(totalBillPasteCommission);
                lineItem.TotalItemSendBillThreeMonth = totalBillOutPasteThreeMonth;

                amountPrice = totalBillPasteCommission * lineInfoRate.PasteBill;
                allTotalCommission += amountPrice;

                //lineItem.AmountOfSendBill = totalBillPaste * lineInfoRate.PasteBill;
                lineItem.AmountOfSendBill = totalBillPasteCommission * lineInfoRate.PasteBill;

                /* Comment By Heart */
                //if (lineInfoRate.PasteBillThreeMonthRate != null)
                //{ 
                //    amountPrice = totalBillOutPasteThreeMonth * lineInfoRate.PasteBillThreeMonthRate; 
                //}
                //else
                //{ 
                //    amountPrice = totalBillOutPasteThreeMonth * (decimal)0.00; 
                //}

                //allTotalCommission += amountPrice;
                lineItem.AmountOfSendBillThreeMonth = amountPrice;

                ArrayList myResultMoneyHelp = new ArrayList();
                myResultMoneyHelp = _commissionrepo.GetExtraMoneyHelpCommissionById(cmdId);
                for (int i = 0; i < 6; i++)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                amountPrice = Convert.ToDecimal(myResultMoneyHelp[i]);
                                lineItem.TransportOfCarPrice = Convert.ToDecimal(string.Format("{0:#,###.00}", amountPrice));
                                allTotalCommission += amountPrice;
                                break;
                            }
                        case 1:
                            {
                                amountPrice = Convert.ToDecimal(myResultMoneyHelp[1]) + Convert.ToDecimal(myResultMoneyHelp[2]);
                                lineItem.TransportOfWaterPrice = Convert.ToDecimal(string.Format("{0:#,###.00}", amountPrice));
                                allTotalCommission += amountPrice;
                                break;
                            }
                        case 3:
                            {
                                DateTime? calDate = (DateTime?)myResultMoneyHelp[3];
                                lineItem.CalculateDate = calDate == null ? String.Empty : String.Format("{0} {1}", calDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), calDate.Value.ToString("hh:mm:ss", new CultureInfo("th-TH")));
                                break;
                            }
                        case 4:
                            {
                                int timeRec = Convert.ToInt32(myResultMoneyHelp[4]);
                                lineItem.TimeNo = timeRec.ToString("00");
                                break;
                            }
                    }
                }
                decimal? baseCommission = allTotalCommission;
                lineItem.Vat7Percent =  (decimal?)Math.Round(((allTotalCommission.Value * rateVatNotRevenue.Value) / 100),2, MidpointRounding.AwayFromZero);
                lineItem.TaxAmount = (decimal?)Math.Round(((allTotalCommission.Value * taxRate.Value) / 100), 2, MidpointRounding.AwayFromZero);
                allTotalCommission = allTotalCommission + lineItem.Vat7Percent - lineItem.TaxAmount;
                lineItem.AmountAllCommission = Convert.ToDecimal(string.Format("{0:#,###.00}", allTotalCommission));

                if (commissionConditionInfo.FineSendMoneyLate == null)
                {
                    lineItem.FineSendMoneyLate = (decimal)0;
                    amountPrice = allTotalCommission;
                }
                else
                {
                    lineItem.FineSendMoneyLate = commissionConditionInfo.FineSendMoneyLate;
                    amountPrice = allTotalCommission - commissionConditionInfo.FineSendMoneyLate;

                }
                //recal vat 
                //decimal? vat = Convert.ToDecimal(string.Format("{0:#,###.00}", (baseCommission * 1) / 100));
                //lineItem.TaxRevenue = vat;
                //amountPrice = amountPrice - vat;
                lineItem.FinalAmountCommission = Convert.ToDecimal(string.Format("{0:#,###.00}", amountPrice));
                lineItem.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                lineInfoList.Add(lineItem);
            }
             
            return lineInfoList;
        }


        //Use For Calculate in Special Commission Section in Register
        //Return Value is Array List
        //Create By Chettha Pattananitisak (Boy)
        private ArrayList CalculateSpecialForCommissionRegistty(int? totalBillOut, decimal? amountBillOut, int? totalCanKeep, decimal? amountCanKeep)
        {
            decimal? percentItemBill = 0; decimal? percentMomeyBill = 0;
            int? itemBill75Percent = 0; int? itemBill90Percent = 0;
            int? diffItemBill75To90Percent = 0; int? diffItemMoreThan90Percent = 0;
            percentItemBill = Convert.ToDecimal(string.Format("{0:#,###.00}", ((Convert.ToDecimal(totalCanKeep) / Convert.ToDecimal(totalBillOut)) * 100)));
            percentMomeyBill = Convert.ToDecimal(string.Format("{0:#,###.00}", ((Convert.ToDecimal(amountCanKeep) / Convert.ToDecimal(amountBillOut)) * 100)));
            if ((percentItemBill > 75) && (percentItemBill <= 90))
            {
                decimal billOut1 = Convert.ToDecimal(totalBillOut);
                decimal rate75 = (decimal)0.75;
                itemBill75Percent = Convert.ToInt32(billOut1 * rate75);
                if (percentItemBill > 90)
                {
                    itemBill90Percent = totalBillOut * (90 / 100);
                    diffItemBill75To90Percent = itemBill75Percent - itemBill90Percent;
                }
                else
                {
                    itemBill90Percent = Convert.ToInt32(totalBillOut * (percentItemBill / 100));
                    diffItemBill75To90Percent = itemBill90Percent - itemBill75Percent;
                }
            }

            if ((percentItemBill > 90) && (percentItemBill < 100))
            {
                if (percentItemBill < percentMomeyBill)
                {
                    itemBill90Percent = totalBillOut * itemBill90Percent;
                    diffItemMoreThan90Percent = (itemBill90Percent - itemBill75Percent - diffItemBill75To90Percent) - 1;
                }
                else
                {
                    itemBill90Percent = Convert.ToInt32(totalBillOut * percentMomeyBill);
                    diffItemMoreThan90Percent = (itemBill90Percent - itemBill75Percent - diffItemBill75To90Percent) - 1;
                }
            }
            ArrayList myResult = new ArrayList();
            myResult.Add(diffItemBill75To90Percent);
            myResult.Add(diffItemMoreThan90Percent);
            return myResult;
        }


        public CAB04_03HeaderReportInfo FindAndDisplayCAB04_03Header(CommissionVoucherConditionPrintReport commissionConditionInfo)
        {            
            CAB04_03HeaderReportInfo header = new CAB04_03HeaderReportInfo();
            string agentId = commissionConditionInfo.AgencyId;
            string cmdId = commissionConditionInfo.CmdId;
            string billPeriod = commissionConditionInfo.PeriodBook;

            header.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
            header.BranchName = String.Format("{0} {1}", _commissionrepo.GetBranchIdByAgentId(agentId), _commissionrepo.GetBranchNameByAgentId(agentId));

            header.AgencyName = _commissionrepo.GetNameAgencyById(agentId);
            header.Period = String.Format(" {0} {1}", StringConvert.GetMonthName(Convert.ToInt32(billPeriod.Substring(0, 2))), billPeriod.Substring(3, 4));
            header.RegisterDate = commissionConditionInfo.CreateDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));

            ArrayList myResultMoneyHelp = new ArrayList();
            myResultMoneyHelp = _commissionrepo.GetExtraMoneyHelpCommissionById(cmdId);
            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 3:
                        {
                            DateTime calDate = Convert.ToDateTime(myResultMoneyHelp[3]);
                            header.CalculateDate = calDate.ToString("dd/MM/yyyy hh:mm", new CultureInfo("th-TH"));
                            break;
                        }
                    case 4:
                        {
                            int timeRec = Convert.ToInt32(myResultMoneyHelp[4]);
                            header.TimeNo = timeRec.ToString("00");
                            break;
                        }
                }
            }

            return header;
        }


        //Use For Get Total Of Bill and Amount between Bill out and Bill that can keep money in Each Bill Book   
        //Create By Chettha Pattananitisak (Boy)
        public List<CAB04_03DetailReportInfo> FindAndDisplayCAB04_03Detail(CommissionVoucherConditionPrintReport commissionConditionInfo)
        {
            //Declare Variable Block
            //----------------------------------------Start Block--------------------------------------------------
            string agentId = String.Empty;
            string billPeriod = String.Empty;
            string createDate = String.Empty;
            string billBookId = String.Empty;
            int billOutResident = 0;

            //Set Variable Block From Screen in Commission
            //----------------------------------------Start Block--------------------------------------------------
            agentId = commissionConditionInfo.AgencyId;
            billPeriod = DaHelper.SetBillPeriod(commissionConditionInfo.PeriodBook);
            createDate = commissionConditionInfo.CreateDate.Value.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
            billOutResident = Convert.ToInt32(commissionConditionInfo.TotalBillOutResident);

            List<CAB04_03DetailReportInfo> lineInfoList = new List<CAB04_03DetailReportInfo>();

            if (commissionConditionInfo != null)
            {
                billBookId = _commissionrepo.GetBillBookIdByAgentIdPeriodAndCreateDate(agentId, billPeriod, createDate);
                lineInfoList = _commissionrepo.GetTotalBillItemAndAmountGroupByBillBookId(agentId.PadLeft(12, '0'), billPeriod, createDate);
            }
             
            return lineInfoList;
        }

        #endregion

        #region "ReturnBillBookReportService"
        //Use For Get Electric No. and Detail Of AR Of Customer don't paid Money pass Agent Of PEA
        //Create By Chettha Pattananitisak (Boy)
        public List<ReturnBillBookBillPasteStatusReportInfo> FindAndDisplayPasteBillStatus(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            //Declare Variable Block
            //----------------------------------------Start Block--------------------------------------------------
            string agentId = ""; string billBookId = ""; string taxId = "";
            agentId = conditionPrintInfo.AgentId;
            billBookId = conditionPrintInfo.BillBookId;
            agentId = agentId.Substring(agentId.Length - ModuleConfigurationNames.AgentIdLength, ModuleConfigurationNames.AgentIdLength);
            billBookId = billBookId.Replace("-", "");
            taxId = _commissionrepo.GetTaxIdInAgencyByAgentId(agentId);
            //-----------------------------------------End Block--------------------------------------------------

            //Set Static Variable From DataBase
            BranchIdInfoItem branchInfo = new BranchIdInfoItem();
            branchInfo = _commissionrepo.GetBranchByBillBookId(billBookId);
            //string peaCode = commissionDA.GetBranchIdByAgentId(agentId);
            //string peaName = commissionDA.GetBranchNameByAgentId(agentId);
            string agentName = _commissionrepo.GetNameAgencyById(agentId);

            List<ReturnBillBookBillPasteStatusReportInfo> lineInfoList = new List<ReturnBillBookBillPasteStatusReportInfo>();
            if (conditionPrintInfo != null)
            {
                string billBookId1 = billBookId;
                List<BillPasteReportInfo> myList = _commissionrepo.GetBillPasteInBillBookReturn(agentId.PadLeft(12, '0'), billBookId1);
                if (myList != null)
                {
                    for (int i = 0; i < myList.Count; i++)
                    {
                        ReturnBillBookBillPasteStatusReportInfo lineItem = new ReturnBillBookBillPasteStatusReportInfo();
                        lineItem.AgentId = agentId;
                        lineItem.AgentName = agentName;
                        lineItem.BranchId = branchInfo.BranchId;
                        lineItem.BranchName = branchInfo.BranchName;
                        lineItem.MruId = myList[i].MruId;
                        lineItem.PmId = myList[i].PmId;
                        lineItem.AgentName = agentName;
                        lineItem.TaxNo = taxId;
                        lineItem.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                        lineItem.RefBillBookId = myList[i].BillBookRef.Substring(6, myList[i].BillBookRef.Length - 6);
                        lineItem.CheckInDate = myList[i].CheckInDate.Value.ToString("dd MMMM yyyy", new CultureInfo("th-TH"));
                        lineItem.Period = DaHelper.GetBillPeriod(myList[i].BillPeroid);
                        lineItem.ElectricNo = myList[i].ElectricNo;
                        lineItem.ElectricPrice = myList[i].ElectricPrice;
                        if (myList[i].FTPrice == null)
                        { lineItem.FTPrice = (decimal)0.00; }
                        else
                        { lineItem.FTPrice = myList[i].FTPrice; }
                        if (myList[i].VatPrice == null)
                        { lineItem.VatPrice = (decimal)0.00; }
                        else
                        { lineItem.VatPrice = myList[i].VatPrice; }
                        if (myList[i].BaseAmount == null)
                        { lineItem.BaseAmount = (decimal)0.00; }
                        else { lineItem.BaseAmount = myList[i].BaseAmount; };
                        lineInfoList.Add(lineItem);
                    }
                }
            }
             
            return lineInfoList;
        }

        ////Use For Bill Info in Each Line Of Agency Group by Status For Header Report 
        ////Create By Chettha Pattananitisak (Boy)
        //public List<ReturnBillBookIssueBillInBillBookHeadReportInfo> FindDisplayIssueBillARInfoMaster(CheckInBillBookConditionInfoReport conditionPrintInfo)
        //{
        //    CommissionDataAccess commissionDA = new CommissionDataAccess();
        //    BillBookDataAccess billBookDA = new BillBookDataAccess();
        //    try
        //    {
        //        decimal prePaid = 0;
        //        decimal totalCash = 0;
        //        decimal totalCheque = 0;
        //        decimal collectionAmount = 0;

        //        string peaCodeSub = ""; string peaCodeMas = ""; string peaMasName = "";
        //        string agentId = ""; string agentName = ""; string billBookId = ""; string taxId = "";
        //        string period = "";

        //        agentId = conditionPrintInfo.AgentId;
        //        billBookId = conditionPrintInfo.BillBookId;
        //        period = conditionPrintInfo.Period;
        //        agentId = agentId.Substring(6, 6);
        //        billBookId = billBookId.Replace("-", "");
        //        //peaCodeSub = commissionDA.GetBranchIdByAgentId(agentId);
        //        peaCodeMas = commissionDA.GetHeaderCodeOfPEAByBranchId(peaCodeSub);
        //        peaMasName = commissionDA.GetHeaderNameOfPEAByBranchId(peaCodeSub);
        //        agentName = commissionDA.GetNameAgencyById(agentId);
        //        taxId = commissionDA.GetTaxIdInAgencyByAgentId(agentId);

        //        // get 30% paid
        //        prePaid = billBookDA.GetPrePaidAccountReceive(null, billBookId);
        //        List<BillBookCheckinDetailInfo> billBookDetails = billBookDA.GetBillBookCheckInDetail(billBookId, period);
        //        // get paid by checque and cash
        //        foreach (BillBookCheckinDetailInfo b in billBookDetails)
        //        {
        //            if (b.AbsId == AbsIdEnum.COLLECTED )
        //            {
        //                collectionAmount += b.TotalAmount == null ? 0 : (decimal)b.TotalAmount;

        //                if (b.PaidType == PaidTypeEnum.CHEQUE)
        //                {
        //                    totalCheque += b.TotalAmount == null ? 0 : (decimal)b.TotalAmount;
        //                }
        //                else
        //                {
        //                    totalCash += b.TotalAmount == null ? 0 : (decimal)b.TotalAmount;
        //                }
        //            }
        //        }

        //        string billBookId1 = billBookId;
        //        if (billBookId1.Length == 10)
        //        { billBookId1 = peaCodeSub + billBookId; }
        //        else
        //        { billBookId1 = billBookId; }

        //        //IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
        //        //string currYear = Session.BpmDateTime.Now.ToString(formatDate);
        //        //if (currYear.Substring(8, 1) == " ")
        //        //{ currYear = currYear.Substring(0, 8); }
        //        //else if (currYear.Substring(9, 1) == " ")
        //        //{ currYear = currYear.Substring(0, 9); }
        //        //else if (currYear.Substring(10, 1) == " ")
        //        //{ currYear = currYear.Substring(0, 10); }

        //        List<ReturnBillBookIssueBillInBillBookHeadReportInfo> returnBookMasterList = new List<ReturnBillBookIssueBillInBillBookHeadReportInfo>();
        //        ReturnBillBookIssueBillInBillBookHeadReportInfo itemList = new ReturnBillBookIssueBillInBillBookHeadReportInfo();
        //        itemList.AgentId = agentId;
        //        itemList.AgentName = agentName;
        //        itemList.PEACode = peaCodeMas;
        //        itemList.PEAName = peaMasName;
        //        itemList.ReportDate = Session.BpmDateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th-TH"));
        //        itemList.PrintDate = String.Format("{0} {1}", Session.BpmDateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), Session.BpmDateTime.Now.ToString("hh:mm:ss", new CultureInfo("th-TH")));
        //        itemList.BillBookId = billBookId;
        //        itemList.AgentTaxNo = taxId;
        //        itemList.PrePaid = prePaid;
        //        itemList.CashPaid = totalCash;
        //        itemList.ChequePaid = totalCheque;
        //        itemList.Amount = collectionAmount;
        //        itemList.AmountTxt = collectionAmount.ToString("#,###.##");
        //        returnBookMasterList.Add(itemList);
        //         
        //        return returnBookMasterList;
        //    }
        //    catch (Exception ex)
        //    {
        //         
        //        throw;
        //    }
        //}

        //Use For Bill Info in Each Line Of Agency Group by Status For Header Report 
        //Create By Chettha Pattananitisak (Boy)
        public CAB03_02DetailReportInfo FindDisplayIssueBillARInfoDetail(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            BillbookCheckInService billService = new BillbookCheckInService();

            string peaCode = ""; string agentId = ""; string billBookId = "";
            agentId = conditionPrintInfo.AgentId;
            billBookId = conditionPrintInfo.BillBookId;
            agentId = agentId.Substring(6, 6);
            billBookId = billBookId.Replace("-", "");
            peaCode = _commissionrepo.GetBranchIdByAgentId(agentId);
            CAB03_02DetailReportInfo retVals = new CAB03_02DetailReportInfo();

            if (conditionPrintInfo != null)
            {
                if (billBookId.Length < ModuleConfigurationNames.BillBookIdLength)
                {
                    billBookId = peaCode + billBookId;
                }

                retVals.BillLineItem = _commissionrepo.GetBillInfoInEachBillBookId(billBookId);
                retVals.PrePaidItem = _billbookrepo.GetBillBookPrePaid(billBookId);
                // get billbook info
                BillBookCheckInInfo billBook = billService.GetBillBookCheckInInfo(billBookId);
                foreach (BillBookCheckinDetailInfo b in billBook.BillBookCheckInDetail)
                {
                    if ((b.AbsId == AbsIdEnum.COLLECTED) || (b.PaidType == (int)PaidTypeEnum.CHEQUE))
                    {
                        if (b.PaidType == (int)PaidTypeEnum.CHEQUE)
                            retVals.ChequeAmount += b.TotalAmount;
                        else
                            retVals.CashAmount += b.TotalAmount;
                    }

                }

                retVals.TotalAmount = retVals.ChequeAmount + retVals.CashAmount;
                retVals.TotalNetAmount = retVals.TotalAmount - retVals.PrePaidAmount; ;
            }
             
            return retVals;
        }

        #endregion

        #region "DailyMgtReportService"       
        public List<CAB07_01DetailReportInfo> FindDetailOfDataIntoRev701460Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            string periodBillBook = ""; string branchId = "";
            branchId = myCondition.PEACode;  
            periodBillBook = myCondition.Period;

            List<CAB07_01DetailReportInfo> myListInfo = new List<CAB07_01DetailReportInfo>();
            List<BranchIdInfoItem> branches = new List<BranchIdInfoItem>();
            branches = _commissionrepo.GetBranchUnderByBranchId(branchId);

            //branches.Add(new BranchIdInfoItem(branchId, String.Empty));
            //add current branch
            //branches.Add(new BranchIdInfoItem(branchId, string.Empty));
            branches.Sort();

            foreach (BranchIdInfoItem b in branches)
            {
                //commissionDA.GetBillInfoInKeepMoneyPlan(b.BranchId, periodBillBook, myCondition.ModifiedBy);
                 Regex rg = new Regex(myCondition.BranchCon + "[0-9]*", RegexOptions.IgnoreCase);
                 if (rg.IsMatch(b.BranchId))
                 {
                     List<CAB07_01DetailReportInfo> billList = _reportrepo.GetCAB07_01(periodBillBook, b.BranchId, myCondition.ModifiedBy);
                     foreach (CAB07_01DetailReportInfo bd in billList)
                     {
                         myListInfo.Add(bd);
                     }
                 }
            }                 
            return myListInfo;
        }

        public List<CAB08_01DetailReportInfo> FindDetailOfDataIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            string periodBill = String.Empty;
            string branchId = String.Empty;
            string modifiedBy = String.Empty;
            string portionGroup = String.Empty;

            string oldBillControllerId = String.Empty;
            string oldProtionId = String.Empty;
            string oldAgencyId = String.Empty;


            branchId = myCondition.PEACode;
            periodBill = myCondition.Period;
            modifiedBy = myCondition.ModifiedBy;

            List<CAB08_01DetailReportInfo> myListInfo = new List<CAB08_01DetailReportInfo>();
            List<BranchIdInfoItem> branchListInfo = GetBranchUnderInBranch(branchId);
            if (branchListInfo != null)
            {
                foreach (BranchIdInfoItem b in branchListInfo)
                {
                    //commissionDA.GetBillInfoInKeepMoneyPlan(b.BranchId, periodBillBook, myCondition.ModifiedBy);
                    Regex rg = new Regex(myCondition.BranchCon + "[0-9]*", RegexOptions.IgnoreCase);
                    if (rg.IsMatch(b.BranchId))
                    {
                        List<CAB08_01DetailReportInfo> myList = _reportrepo.GetCAB08_01(b.BranchId, periodBill, modifiedBy);
                        if (myList != null)
                        {
                            foreach (CAB08_01DetailReportInfo item in myList)
                            {
                                myListInfo.Add(item);
                            }
                        }
                    }
                }
            }
             
            return myListInfo;
        }

        public List<CAB08_01AgencyInfo> FindAgentListIntoRev701461Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            string periodBill = ""; string branchId = "";

            branchId = myCondition.PEACode;
            periodBill = myCondition.Period;

            List<CAB08_01AgencyInfo> myListInfo = new List<CAB08_01AgencyInfo>();
            List<BranchIdInfoItem> branchListInfo = GetBranchUnderInBranch(branchId);
            if (branchListInfo != null)
            {
                foreach (BranchIdInfoItem b in branchListInfo)
                {
                      Regex rg = new Regex(myCondition.BranchCon + "[0-9]*", RegexOptions.IgnoreCase);
                      if (rg.IsMatch(b.BranchId))
                      {
                          List<CAB08_01AgencyInfo> myList = _commissionrepo.GetCAB08_01AgencyList(b.BranchId, periodBill);
                          if (myList != null)
                          {
                              foreach (CAB08_01AgencyInfo row in myList)
                              {
                                  if (!myListInfo.Contains(row))
                                  {
                                      myListInfo.Add(row);
                                  }
                              }
                          }
                      }
                }
            }
             
            return myListInfo;
        }

        public List<CAB09_01DetailReportInfo> FindDetailOfDataIntoRev701462Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            string periodBill = ""; string branchId = ""; string modifiedBy = "";

            branchId = myCondition.PEACode;
            periodBill = myCondition.Period;
            modifiedBy = myCondition.ModifiedBy;

            List<CAB09_01DetailReportInfo> myListInfo = new List<CAB09_01DetailReportInfo>();
            List<BranchIdInfoItem> branchListInfo = GetBranchUnderInBranch(branchId);
            if (branchListInfo != null)
            {
                decimal? totalAllBill = GetTotalBillInArrBranch(branchListInfo, periodBill);
                decimal? totalMoneyAllBill = GetTotalMoneyBillInArrBranch(branchListInfo, periodBill);
                foreach (BranchIdInfoItem b in branchListInfo)
                {
                    Regex rg = new Regex(myCondition.BranchCon + "[0-9]*", RegexOptions.IgnoreCase);
                    if (rg.IsMatch(b.BranchId))
                    {
                        List<CAB09_01DetailReportInfo> myList = _reportrepo.GetCAB09_01(b.BranchId, periodBill, modifiedBy);
                        if (myList != null)
                        {
                            foreach (CAB09_01DetailReportInfo row in myList)
                            {
                                if (row.PortionId != null)
                                {
                                    myListInfo.Add(row);
                                }
                            }
                        }
                    }
                }
            }
             
            return myListInfo;
        }

        private decimal? GetTotalBillInArrBranch(List<BranchIdInfoItem> myBranchList, string periodBill)
        {
            int arrCount = myBranchList.Count - 1;
            string startBranchId = myBranchList[0].BranchId;
            string endBranchId = myBranchList[arrCount].BranchId;
            decimal? myResult = _commissionrepo.GetAmountOfBillArrOfBranch(startBranchId, endBranchId, periodBill);

            return myResult;
        }

        private decimal? GetTotalMoneyBillInArrBranch(List<BranchIdInfoItem> myBranchList, string periodBill)
        {
            int arrCount = myBranchList.Count - 1;
            string startBranchId = myBranchList[0].BranchId;
            string endBranchId = myBranchList[arrCount].BranchId;
            decimal? myResult = _commissionrepo.GetAmountMoneyOfBillArrOfBranch(startBranchId, endBranchId, periodBill);
             
            return myResult;
        }

        public List<CAB08_02DetailReportInfo> FindDetailOfDataIntoRev701463Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {
            string periodBill = ""; string branchId = ""; string modifiedBy;

            branchId = myCondition.PEACode;
            periodBill = myCondition.Period;
            modifiedBy = myCondition.ModifiedBy;

            List<CAB08_02DetailReportInfo> myListInfo = new List<CAB08_02DetailReportInfo>();
            List<BranchIdInfoItem> branchListInfo = GetBranchUnderInBranch(branchId);
            if (branchListInfo != null)
            {
                foreach (BranchIdInfoItem b in branchListInfo)
                {
                    Regex rg = new Regex(myCondition.BranchCon + "[0-9]*", RegexOptions.IgnoreCase);
                    if (rg.IsMatch(b.BranchId))
                    {
                        List<CAB08_02DetailReportInfo> myList = _reportrepo.GetCAB08_02(b.BranchId, periodBill, modifiedBy);
                        foreach (CAB08_02DetailReportInfo row in myList)
                        {
                            if (row.AgencyId != null)
                            {
                                myListInfo.Add(row);
                            }
                        }
                    }
                }
            }

            return myListInfo;
        }

        public List<CAB10_01DetailReportInfo> FindDetailOfDataIntoRev701464Report(KeepMoneyPlaneOfAgencyConditionInfoReport myCondition)
        {            
            string periodBill = String.Empty;
            string branchId = String.Empty;
            string modifiedBy = String.Empty;

            branchId = myCondition.PEACode;
            periodBill = myCondition.Period;
            modifiedBy = myCondition.ModifiedBy;

            List<CAB10_01DetailReportInfo> myListInfo = new List<CAB10_01DetailReportInfo>();
            List<BranchIdInfoItem> branchListInfo = GetBranchUnderInBranch(branchId);
            if (branchListInfo != null)
            {
                foreach (BranchIdInfoItem b in branchListInfo)
                {
                    Regex rg = new Regex(myCondition.BranchCon + "[0-9]*", RegexOptions.IgnoreCase);
                    if (rg.IsMatch(b.BranchId))
                    {
                        List<CAB10_01DetailReportInfo> myList = _reportrepo.GetCAB10_01(b.BranchId, periodBill, modifiedBy);
                        if (myList != null)
                        {
                            foreach (CAB10_01DetailReportInfo row in myList)
                            {
                                myListInfo.Add(row);
                            }
                        }
                    }
                }
            }                
            return myListInfo;
        }

        private List<BranchIdInfoItem> GetBranchUnderInBranch(string branchId)
        {
            List<BranchIdInfoItem> myListInfo = new List<BranchIdInfoItem>();
            myListInfo = _commissionrepo.GetBranchUnderByBranchId(branchId);

            // add current branch
            BranchIdInfoItem b = new BranchIdInfoItem();
            b.BranchId = branchId;
            if (myListInfo == null) myListInfo = new List<BranchIdInfoItem>();
            //myListInfo.Add(b);
            myListInfo.Sort();
                 
            return myListInfo;
        }

        public CAB06_01HeaderReportInfo FindAndDisplayCAB06_01Header(EvaluateAgencyReportCondition myComdition, List<CAB06_01DetailReportInfo> reportDetail)
        {            
            decimal? totalCommission = 0;
            int? totalBillCollected = 0;
            CAB06_01HeaderReportInfo itemInfo = new CAB06_01HeaderReportInfo();

            if (reportDetail.Count > 0)
            {

                itemInfo.StartBranchId = GetStartBranch(reportDetail).PeaCode;
                itemInfo.StartBranchName = GetStartBranch(reportDetail).PeaName;

                itemInfo.EndBranchId = GetEndBranch(reportDetail).PeaCode;
                itemInfo.EndBranchName = GetEndBranch(reportDetail).PeaName;
            }
            else
            {
                itemInfo.StartBranchId = myComdition.BranchFrom;
                itemInfo.StartBranchName = _commissionrepo.GetBranchNameByBranchId(myComdition.BranchFrom);

                itemInfo.EndBranchId = myComdition.BranchTo;
                itemInfo.EndBranchName = _commissionrepo.GetBranchNameByBranchId(myComdition.BranchTo);
            }
            foreach (CAB06_01DetailReportInfo detail in reportDetail)
            {
                totalCommission += detail.BaseCommission + detail.SpacialCommission + detail.SendInvoice + detail.ExtraMoney;
                totalBillCollected += detail.CanKeepBill;
            }


            itemInfo.TotalBillCollected = totalBillCollected;
            itemInfo.TotalCommission = totalCommission;

            itemInfo.PeriodStartMonth = StringConvert.GetMonthName(Convert.ToInt32(myComdition.PeriodFrom.Substring(4, 2)));
            itemInfo.PeriodEndMonth = StringConvert.GetMonthName(Convert.ToInt32(myComdition.PeriodTo.Substring(4, 2)));

            itemInfo.PeriodYear = myComdition.PeriodFrom.Substring(0, 4);
            itemInfo.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                 
            return itemInfo;
        }

        public List<CAB06_01DetailReportInfo> FindAndDisplayCAB06_01Detail(EvaluateAgencyReportCondition myComdition)
        {
            List<BranchIdInfoItem> branchList = new List<BranchIdInfoItem>();
            List<BranchIdInfoItem> branchScope = new List<BranchIdInfoItem>();
            BranchIdInfoItem b = new BranchIdInfoItem();
            if (myComdition.BranchType == 1)
            {
                branchList = _commissionrepo.GetBranchUnderByBranchId(myComdition.BranchId);                                        
                //branchList.Add(new BranchIdInfoItem(myComdition.BranchId, commissionDA.GetBranchNameByBranchId(myComdition.BranchId)));                    
            }              
            else
            {
                b.BranchId = myComdition.BranchId;
                branchScope = _commissionrepo.GetBranchUnderParent(b);
                branchList = _commissionrepo.GetFromToBranch(myComdition.BranchFrom, myComdition.BranchTo, branchScope);
            }

            // loop all branch
            List<CAB06_01DetailReportInfo> myListInfo = new List<CAB06_01DetailReportInfo>();
            foreach (BranchIdInfoItem branch in branchList)
            {
                //commissionDA.GetBillInfoInKeepMoneyPlan(b.BranchId, periodBillBook, myCondition.ModifiedBy);
                Regex rg = new Regex(myComdition.BaCode + "[0-9]*", RegexOptions.IgnoreCase);
                if (rg.IsMatch(branch.BranchId))
                {
                    CAB06_01DetailReportInfo itemInfo = new CAB06_01DetailReportInfo();
                    List<BillBookCommissionBranchInfo> commissionList = new List<BillBookCommissionBranchInfo>();
                    commissionList = _commissionrepo.GetBillBookCommissionBranch(branch.BranchId, myComdition.PeriodFrom, myComdition.PeriodTo);

                    if (commissionList.Count > 0)
                    {
                        List<string> agencyTypeCommany = new List<string>();
                        List<string> agencyTypePerson = new List<string>();
                        List<string> agencyOverNinety = new List<string>();

                        itemInfo.PeaCode = branch.BranchId;
                        itemInfo.PeaName = branch.BranchName;
                        itemInfo.BranchGroup = branch.BranchGroupId;
                        itemInfo.BranchLevel = branch.BranchLevel;

                        foreach (BillBookCommissionBranchInfo bbc in commissionList)
                        {
                            if ((!agencyTypePerson.Contains(bbc.AgencyId)) && bbc.BpTypeId == "1")
                            {
                                agencyTypePerson.Add(bbc.AgencyId);
                            }
                            if ((!agencyTypeCommany.Contains(bbc.AgencyId)) && bbc.BpTypeId == "2")
                            {
                                agencyTypeCommany.Add(bbc.AgencyId);
                            }

                            if ((!agencyOverNinety.Contains(bbc.AgencyId)) && (bbc.OverNinety == "Y"))
                            {
                                agencyOverNinety.Add(bbc.AgencyId);
                            }

                            itemInfo.AmountBillOut = itemInfo.AmountBillOut == null ? 0 : itemInfo.AmountBillOut + bbc.TotalBookAmount;
                            itemInfo.BillOut = itemInfo.BillOut == null ? 0 : itemInfo.BillOut + bbc.TotalBill;
                            itemInfo.CanKeepBill = itemInfo.CanKeepBill == null ? 0 : itemInfo.CanKeepBill + bbc.TotalBillCollected;
                            itemInfo.TotalMoney = itemInfo.TotalMoney == null ? 0 : itemInfo.TotalMoney + bbc.TotalCollectedAmount;
                            itemInfo.BaseCommission = itemInfo.BaseCommission == null ? 0 : itemInfo.BaseCommission + bbc.BaseCmAmount;
                            itemInfo.SpacialCommission = itemInfo.SpacialCommission == null ? 0 : itemInfo.SpacialCommission + bbc.SpecialCmAmount; ;
                            itemInfo.SendInvoice = itemInfo.SendInvoice == null ? 0 : itemInfo.SendInvoice + bbc.InvCmAmount;
                            itemInfo.ExtraMoney = itemInfo.ExtraMoney == null ? 0 : itemInfo.ExtraMoney + bbc.ExtraMoney;
                            itemInfo.AreaName = bbc.AreaName;
                        }
                        itemInfo.KeepMore90Percent = agencyOverNinety.Count;
                        itemInfo.Area = branch.BranchId.Substring(0, 1);
                        
                        itemInfo.AgentPersonType = agencyTypePerson.Count;
                        itemInfo.AgentCompanyType = agencyTypeCommany.Count;
                        itemInfo.TotalAgent = itemInfo.AgentPersonType + itemInfo.AgentCompanyType;

                        myListInfo.Add(itemInfo);
                    }
                }
            }


            #region "Old code"
            #endregion

                 
            return myListInfo;
        }

        public CAB03_HeaderReportInfo FindAndDisplayCAB03_Header(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {            
            CAB03_HeaderReportInfo header = new CAB03_HeaderReportInfo();
            BranchIdInfoItem branchInfo = new BranchIdInfoItem();

            branchInfo = _commissionrepo.GetBranchByBillBookId(conditionPrintInfo.BillBookId);

            string agentId = conditionPrintInfo.AgentId;
            header.BillBookNo = conditionPrintInfo.BillBookId;


            header.BranchName = String.Format("{0}, {1}", branchInfo.BranchId, branchInfo.BranchName);
            header.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
            header.AgencyTaxNo = _commissionrepo.GetTaxIdInAgencyByAgentId(agentId);
            header.Period = _commissionrepo.GetBillBookCheckInDate(conditionPrintInfo.BillBookId).Value.ToString("dd MMMM yyyy", new CultureInfo("th-TH"));
            header.AgencyName = _commissionrepo.GetNameAgencyById(agentId);
            return header;
        }

        public CAB03_02ReportInfo FindAndDisplayCAB03_02Report(CheckInBillBookConditionInfoReport condition)
        {
            CAB03_02ReportInfo reportInfo = new CAB03_02ReportInfo();
            reportInfo.Header = FindAndDisplayCAB03_Header(condition);
            reportInfo.Detail = FindDisplayIssueBillARInfoDetail(condition);
            return reportInfo;
        }

        public List<CAB03_DetailReportInfo> FindAndDisplayCAB03_Detail(CheckInBillBookConditionInfoReport conditionPrintInfo)
        {
            List<CAB03_DetailReportInfo> retVals = new List<CAB03_DetailReportInfo>();
            retVals = _commissionrepo.GetCAB03(conditionPrintInfo.BillBookId, conditionPrintInfo.AbsId.ToString(), conditionPrintInfo.PmId.ToString());
            return retVals;
        }

        public CAB03_03ReportInfo FindAndDisplayCAB03_03Report(CheckInBillBookConditionInfoReport condition)
        {
            CAB03_03ReportInfo reportInfo = new CAB03_03ReportInfo();
            reportInfo.Header = FindAndDisplayCAB03_Header(condition);
            reportInfo.Details = FindAndDisplayCAB03_Detail(condition);
            return reportInfo;
        }

        public CAB03_04ReportInfo FindAndDisplayCAB03_04Report(CheckInBillBookConditionInfoReport condition)
        {
            CAB03_04ReportInfo reportInfo = new CAB03_04ReportInfo();
            reportInfo.Header = FindAndDisplayCAB03_Header(condition);
            reportInfo.Details = FindAndDisplayCAB03_Detail(condition);
            return reportInfo;
        }

        private int GetCountAgencyMorethan90PercentByPeriod(string branchId, int sMonth, int eMonth, int periodYear)
        {            
            int counter = 0;
            List<EvaluateAgencyCounter> MyList = _commissionrepo.GetIssueBillAndAmountOfAgentInEachBranchForPeriod(branchId, sMonth, eMonth, periodYear);
            if (MyList != null)
            {
                foreach (EvaluateAgencyCounter row in MyList)
                {
                    if ((row.PercentOfAmount > (decimal)90.00) && (row.PercentOfItem > (decimal)90.00))
                    { counter += 1; }
                    else if (row.PercentOfAmount < row.PercentOfItem)
                    {
                        if (row.PercentOfAmount > (decimal)90.00)
                        { counter += 1; }
                    }
                    else if (row.PercentOfItem < row.PercentOfAmount)
                    {
                        if (row.PercentOfItem > (decimal)90.00)
                        { counter += 1; }
                    }
                }
            }
                 
            return counter;
        }

        private int GetCountAgencyMorethan90PercentByMonthly(string branchId, int periodMonth, int periodYear)
        {
            int counter = 0;
            List<EvaluateAgencyCounter> MyList = _commissionrepo.GetIssueBillAndAmountOfAgentInEachBranchForMonthly(branchId, periodMonth, periodYear);
            if (MyList != null)
            {
                foreach (EvaluateAgencyCounter row in MyList)
                {
                    if ((row.PercentOfAmount > (decimal)90.00) && (row.PercentOfItem > (decimal)90.00))
                    { counter += 1; }
                    else if (row.PercentOfAmount < row.PercentOfItem)
                    {
                        if (row.PercentOfAmount > (decimal)90.00)
                        { counter += 1; }
                    }
                    else if (row.PercentOfItem < row.PercentOfAmount)
                    {
                        if (row.PercentOfItem > (decimal)90.00)
                        { counter += 1; }
                    }
                }
            }
                 
            return counter;
        }

        private decimal? CalculateTotalPaste(int? totalBill, int? totalPasteBill, int? totalPasteBill3Month, decimal? pasteRate, decimal? threeMontRate)
        {

            int? tenPercent = (int?)Math.Round(((decimal)totalBill * (decimal)0.1));

            decimal? retVal = 0;
            if ((totalPasteBill + totalPasteBill3Month) == 0)
                return 0;
            else if (totalPasteBill3Month > tenPercent)
            {
                retVal = tenPercent * threeMontRate;
            }
            else if ((totalPasteBill3Month + totalPasteBill) <= tenPercent)
            {
                retVal = (totalPasteBill3Month * threeMontRate) + (totalPasteBill + pasteRate);
            }
            else
            {
                retVal = totalPasteBill3Month * threeMontRate;
                retVal += (tenPercent - totalPasteBill3Month) > totalPasteBill ? (totalPasteBill * pasteRate) : ((tenPercent - totalPasteBill3Month) * pasteRate);
            }

            return retVal;
        }

        private CAB06_01DetailReportInfo GetStartBranch(List<CAB06_01DetailReportInfo> itemList)
        {
            CAB06_01DetailReportInfo retVal = new CAB06_01DetailReportInfo();
            if (itemList.Count > 0)
            {
                itemList.Sort();
                retVal = itemList[0];
            }
            return retVal;
        }

        private CAB06_01DetailReportInfo GetEndBranch(List<CAB06_01DetailReportInfo> itemList)
        {
            CAB06_01DetailReportInfo retVal = new CAB06_01DetailReportInfo();
            if (itemList.Count > 0)
            {
                itemList.Sort();
                retVal = itemList[itemList.Count - 1];
            }
            return retVal;
        }

        public List<CAB05_01DetailReportInfo> FindAndDisplayCAB05_01Detail(CAB05_01ConditionReportInfo conditionInfo)
        {
            List<CAB05_01DetailReportInfo> retVals = new List<CAB05_01DetailReportInfo>();
            List<CAB05_01DetailReportInfo> temps = new List<CAB05_01DetailReportInfo>();            
            string branchId = String.Empty;
            string periodStart = String.Empty;
            string periodEnd = String.Empty;
            string startAgencyId = String.Empty;
            string endAgencyId = String.Empty;

            branchId = conditionInfo.BranchId; ;
            periodStart = conditionInfo.StartPeriod ;
            periodEnd = conditionInfo.EndPeriod;
            startAgencyId = conditionInfo.StartAgencyId;
            endAgencyId = conditionInfo.EndAgencyId;

            List<BranchIdInfoItem> branchListInfo = GetBranchUnderInBranch(conditionInfo.BranchId);
            if (branchListInfo != null)
            {
                foreach (BranchIdInfoItem rowBranch in branchListInfo)
                {
                    temps = _reportrepo.GetCAB05_01(rowBranch.BranchId, startAgencyId, endAgencyId, periodStart, periodEnd);
                    foreach (CAB05_01DetailReportInfo item in temps)
                    {
                        retVals.Add(item);
                    }
                }
            }
           return retVals;
        }

        public List<PA_7034DetailReportInfo> FindAndDisplayPA_7034Detail(PA_7034ConditionReportInfo conn)
        {            
            List<PA_7034DetailReportInfo> retVals = new List<PA_7034DetailReportInfo>();
            //find agency length
            if (conn.AgencyIdFrom == String.Empty && conn.AgencyIdTo == String.Empty)
            {
                conn.AgencyIdFrom = _reportrepo.GetMinAgencyIdInBranch(conn.BranchId);
                conn.AgencyIdTo = _reportrepo.GetMaxAgencyIdInBranch(conn.BranchId);
            }

            retVals = _reportrepo.GetPA_7034(conn);
            return retVals;
        }

        public List<CAB12_01DetailReportInfo> FindAndDisplayCAB12_01Detail(CAB12_01ConditionReportInfo conditionInfo)
        {
            List<CAB12_01DetailReportInfo> retVals = new List<CAB12_01DetailReportInfo>();
            List<CAB12_01DetailReportInfo> temps = new List<CAB12_01DetailReportInfo>();            
            DateTime? startPeriod = conditionInfo.StartPeriod;
            DateTime? endPeriod = conditionInfo.EndPeriod;
            string branchId = conditionInfo.BranchId; ;
            List<BranchIdInfoItem> branchListInfo = GetBranchUnderInBranch(branchId);
            if (branchListInfo != null)
            {
                foreach (BranchIdInfoItem rowBranch in branchListInfo)
                {
                    //find agency length
                    if (conditionInfo.AgencyIdFrom == String.Empty && conditionInfo.AgencyIdTo == String.Empty)
                    {
                        conditionInfo.AgencyIdFrom = _reportrepo.GetMinAgencyIdInBranch(branchId);
                        conditionInfo.AgencyIdTo = _reportrepo.GetMaxAgencyIdInBranch(branchId);
                    }
                    temps = _reportrepo.GetCAB12_01(rowBranch.BranchId, conditionInfo);
                    foreach (CAB12_01DetailReportInfo item in temps)
                    {
                        retVals.Add(item);
                    }
                }
            }
            return retVals;
        }

        public List<CAB12_02DetailReportInfo> FindAndDisplayCAB12_02Detail(CAB12_01ConditionReportInfo conditionInfo)
        {
            List<CAB12_02DetailReportInfo> retVals = new List<CAB12_02DetailReportInfo>();
            List<CAB12_02DetailReportInfo> temps = new List<CAB12_02DetailReportInfo>();            
            DateTime? startPeriod = conditionInfo.StartPeriod;
            DateTime? endPeriod = conditionInfo.EndPeriod;
            string branchId = conditionInfo.BranchId; ;
            List<BranchIdInfoItem> branchListInfo = GetBranchUnderInBranch(branchId);
            if (branchListInfo != null)
            {
                foreach (BranchIdInfoItem rowBranch in branchListInfo)
                {
                    //find agency length
                    if (conditionInfo.AgencyIdFrom == String.Empty && conditionInfo.AgencyIdTo == String.Empty)
                    {
                        conditionInfo.AgencyIdFrom = _reportrepo.GetMinAgencyIdInBranch(branchId);
                        conditionInfo.AgencyIdTo = _reportrepo.GetMaxAgencyIdInBranch(branchId);
                    }
                    temps = _reportrepo.GetCAB12_02(rowBranch.BranchId, conditionInfo);
                    foreach (CAB12_02DetailReportInfo item in temps)
                    {
                        retVals.Add(item);
                    }
                }
            }
            return retVals;
        }

        public CAB12_01HeaderReportInfo FindAndDisplayCAB12_01Header(List<CAB12_01DetailReportInfo> detail, string branchName, string branchId)
        {
            CAB12_01HeaderReportInfo retVal = new CAB12_01HeaderReportInfo();
            retVal.BranchName = String.Format("{0}, {1}", branchName, branchId);
            retVal.PrintDate = Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
            foreach (CAB12_01DetailReportInfo d in detail)
            {
                if (d.TotalDiffMonth <= 3)
                {
                    retVal.Past1_3Month = retVal.Past1_3Month + 1;
                }
                else if (d.TotalDiffMonth <= 6)
                {
                    retVal.Past4_6Month = retVal.Past4_6Month + 1;
                }
                else if (d.TotalDiffMonth <= 12)
                {
                    retVal.Past7_12Month = retVal.Past4_6Month + 1;
                }
                else 
                {
                    retVal.Past1Year = retVal.Past1Year + 1;
                }
 
            }
            return retVal;
        }

        public List<CAN34_01DetailReportInfo> FindAndDisplayCAN34_01Detail(CAN34_01CondtionReportInfo conn)
        {         
            List<CAN34_01DetailReportInfo> retVals = new List<CAN34_01DetailReportInfo>();
            List<CAN34_01DetailReportInfo> temps = new List<CAN34_01DetailReportInfo>();            

            string branchId = conn.BranchId; ;
            List<BranchIdInfoItem> branchListInfo = GetBranchUnderInBranch(branchId);
            if (branchListInfo != null)
            {
                foreach (BranchIdInfoItem rowBranch in branchListInfo)
                {                    
                    temps = _reportrepo.GetCAN34_01(rowBranch.BranchId, conn);
                    foreach (CAN34_01DetailReportInfo item in temps)
                    {
                        retVals.Add(item);
                    }
                }
            }            
            return retVals;
        }

        public string GetBillKeeperNameByBillBook(string billBookId)
        {            
            string retVal = String.Empty;
            retVal = _billbookrepo.GetBillKeeperNameByBillBook(billBookId);            
            return retVal;
        }

        public decimal? GetIntownReceive(string billBookId)
        {            
            decimal? retVal = 0;
            retVal = _reportrepo.GetIntownReceive(billBookId);            
            return retVal;
        }

        public BillBookInfoMasterReport GetBillBookInfoReport(BillBookHeaderInfo bookHeader)
        {
            BillBookInfoMasterReport reportInfo = new BillBookInfoMasterReport();

            reportInfo.BillBookId = bookHeader.BillBookId.Insert(2, "-");
            reportInfo.Period = bookHeader.Period;
            reportInfo.AgencyID = bookHeader.AgentId;
            reportInfo.AgencyName = bookHeader.AgentName;
            reportInfo.ReceiveTime = bookHeader.ReceiveCount.ToString().PadLeft(2, '0');
            reportInfo.BillReturnedDate = bookHeader.ReturnedBillDt.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
            reportInfo.TotalAsset = bookHeader.TotalAsset;
            reportInfo.TentativeDate = bookHeader.AdvancePaymentDt.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")); ;
            reportInfo.AdvPayAmount = GetAdvPaidByBillBookId(bookHeader.BillBookId);
            reportInfo.BillKeeperName = GetBillKeeperNameByBillBook(bookHeader.BillBookId);
            reportInfo.BillBookList = GetBillBookDetailReportList(bookHeader.BillBookId);
            reportInfo.IntownReceive = GetIntownReceive(bookHeader.BillBookId);
            reportInfo.BillReportList = ConvertToBillBookDetailReport(reportInfo.BillBookList);
            reportInfo.CreatorName = bookHeader.CreatorName;
            reportInfo.BookLot = bookHeader.BookLot;
            return reportInfo;
        }
        #endregion

        #region "Helper"
        private List<BillBookDetailReportListInfo> ConvertToBillBookDetailReport(List<BillBookDetailReportListInfo> billBookList)
        {
            List<BillBookDetailReportListInfo> retVals = new List<BillBookDetailReportListInfo>();
            List<BillBookDetailReportListInfo> currBills = new List<BillBookDetailReportListInfo>();
            List<BillBookDetailReportListInfo> preBills = new List<BillBookDetailReportListInfo>();

            foreach (BillBookDetailReportListInfo b in billBookList)
            {
                if (b.BillType == 0)
                {
                    bool _found = false;
                    for (int i = 0; i < currBills.Count; i++)
                    {
                        if ((currBills[i].BranchId == b.BranchId) && (currBills[i].MRUId == b.MRUId))
                        {
                            currBills[i].BillCount += b.BillCount;
                            currBills[i].ElectricPrice += b.ElectricPrice;
                            currBills[i].FtPrice += b.FtPrice;
                            currBills[i].BaseAmount += b.BaseAmount;
                            currBills[i].Vat += b.Vat;
                            _found = true;
                            break;
                        }
                    }
                    if (!_found)
                    {
                        BillBookDetailReportListInfo currBill = new BillBookDetailReportListInfo();
                        currBill.PayRepeat = b.PayRepeat;
                        currBill.BranchId = b.BranchId;
                        currBill.BillType = b.BillType;
                        currBill.MRUId = b.MRUId;
                        currBill.BillCount = b.BillCount;
                        currBill.ElectricPrice = b.ElectricPrice;
                        currBill.FtPrice = b.FtPrice;
                        currBill.BaseAmount = b.BaseAmount;
                        currBill.Vat = b.Vat;
                        currBills.Add(currBill);
                    }

                }
                else
                {
                    bool _found = false;
                    for (int i = 0; i < preBills.Count; i++)
                    {
                        if ((preBills[i].BranchId == b.BranchId) && (preBills[i].MRUId == b.MRUId))
                        {
                            preBills[i].BillCount += b.BillCount;
                            preBills[i].ElectricPrice += b.ElectricPrice;
                            preBills[i].FtPrice += b.FtPrice;
                            preBills[i].BaseAmount += b.BaseAmount;
                            preBills[i].Vat += b.Vat;
                            _found = true;
                            break;
                        }
                    }
                    if (!_found)
                    {
                        BillBookDetailReportListInfo preBill = new BillBookDetailReportListInfo();
                        preBill.PayRepeat = b.PayRepeat;
                        preBill.BranchId = b.BranchId;
                        preBill.BillType = b.BillType;
                        preBill.MRUId = b.MRUId;
                        preBill.BillCount = b.BillCount;
                        preBill.ElectricPrice = b.ElectricPrice;
                        preBill.FtPrice = b.FtPrice;
                        preBill.BaseAmount = b.BaseAmount;
                        preBill.IsAdvPaid = b.IsAdvPaid;
                        preBill.Vat = b.Vat;
                        preBills.Add(preBill);
                    }
                }
            }
            foreach (BillBookDetailReportListInfo b in currBills)
            {
                retVals.Add(b);
            }
            foreach (BillBookDetailReportListInfo b in preBills)
            {
                retVals.Add(b);
            }

            return retVals;

        }
        #endregion

    }
}
