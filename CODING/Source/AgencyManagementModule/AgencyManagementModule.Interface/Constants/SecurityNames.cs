using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.Constants
{
    public class SecurityNames : PEA.BPM.Infrastructure.Interface.Constants.SecurityNames
    {
        //planning
        public const string AgencyPlanning          = "A100";

        //create book
        public const string CreateAgentBillBook     = "A200";
        public const string CreateEmployeeBillBook  = "A201";
        //public const string ViewOldBillBook = "A202";
        public const string OverBookDepositLimit = "A205";
        public const string EditBillBookDueDate = "A203";
        public const string CancelBillBook = "A204";
        

        //check in book
        public const string CheckInBillBook = "A300";
        public const string CheckiInGroupInvoice = "A301";
        public const string BillStatusChecking = "A302";
        public const string CancelCheckIn = "A303";
        public const string ReprintCheckInReport = "A304";

        //commission
        public const string CommissionAndFine = "A400";
        public const string FineDetail = "A401";
        public const string TaxDetail = "A402";
        public const string CancelFine = "A403";
        public const string EditSpecialHelp = "A404";
        public const string ReprintCommissionReport = "A405";     

        //Reprint 
        public const string ReprintBillbookReport = "A500";

        //Reports
        public const string AgentMoneyReturnReport = "A600";
        public const string AgentMoneyCollectByQuarterReport = "A601";
        public const string AgentMoneyCollectPlan = "A602";

        public const string CAN34_01Report = "A610";
        public const string CAB12_01Report = "A611";
        public const string CAB13_01Report = "A612";
        public const string CAB05_01Report = "A613";
        public const string PA_7034Report = "A614";
        //public const string AgentMoneyCollectByAgentReport = "A603";
        //public const string BillingPlanByTimeReport = "A604";
        //public const string BillingPlanByBillOutReport = "A605";
        //public const string DebtAgeReport = "A606";
        public const string AgentPerformanceReport = "A607";

        //config
        public const string AgencyConfiguration = "A700";


    }
}
