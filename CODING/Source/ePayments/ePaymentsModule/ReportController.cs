using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.ePaymentsModule.Interface.Constants;
using Microsoft.ReportingServices.Extensions;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.Services;

namespace PEA.BPM.ePaymentsModule
{   
    public class ReportController : WorkItemController
    {
        private ILayoutService _layoutService;
        private IBillingService _billingService;
        private IReportService _reportService;
        private ICommonService _commService;

        [InjectionConstructor]
        public ReportController([ServiceDependency] ILayoutService layoutService, [ServiceDependency] IBillingService billingService, [ServiceDependency] IReportService reportService,[ServiceDependency] ICommonService commService )
        {
            _layoutService = layoutService;
            _billingService = billingService;
            _reportService = reportService;
            _commService = commService;
        }

        public void Run(ReportName report)
        {
            LoadDisplayedViews(report);
            LoadRequiredViews(report);
        }

        private void LoadDisplayedViews(ReportName report)
        {
            _layoutService.LoadHorizontalLayout(250);

            switch (report)
            {
                case ReportName.RE_01:

                    SetWindowsTitle(Properties.Resources.RE_01);

                    IRE_01CriteriaView RE_01CriteriaView;
                    if (WorkItem.Items.Contains("IRE_01CriteriaView"))
                    {
                        RE_01CriteriaView = WorkItem.Items.Get<IRE_01CriteriaView>("IRE_01CriteriaView");
                    }
                    else
                    {
                        RE_01CriteriaView = WorkItem.Items.AddNew<RE_01CriteriaView>("IRE_01CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(RE_01CriteriaView);

                    break;

                case ReportName.RE_02:

                    SetWindowsTitle(Properties.Resources.RE_02);

                    IRE_02CriteriaView RE_02CriteriaView;
                    if (WorkItem.Items.Contains("IRE_02CriteriaView"))
                    {
                        RE_02CriteriaView = WorkItem.Items.Get<IRE_02CriteriaView>("IRE_02CriteriaView");
                    }
                    else
                    {
                        RE_02CriteriaView = WorkItem.Items.AddNew<RE_02CriteriaView>("IRE_02CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(RE_02CriteriaView);

                    break;

                case ReportName.RE_03:

                    SetWindowsTitle(Properties.Resources.RE_03);

                    IRE_03CriteriaView RE_03CriteriaView;
                    if (WorkItem.Items.Contains("ICAC04CriteriaView"))
                    {
                        RE_03CriteriaView = WorkItem.Items.Get<IRE_03CriteriaView>("IRE_03CriteriaView");
                    }
                    else
                    {
                        RE_03CriteriaView = WorkItem.Items.AddNew<RE_03CriteriaView>("IRE_03CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(RE_03CriteriaView);

                    break;

                case ReportName.RE_04:

                    SetWindowsTitle(Properties.Resources.RE_04);

                    IRE_04CriteriaView RE_04CriteriaView;
                    if (WorkItem.Items.Contains("IRE_04CriteriaView"))
                    {
                        RE_04CriteriaView = WorkItem.Items.Get<IRE_04CriteriaView>("IRE_04CriteriaView");
                    }
                    else
                    {
                        RE_04CriteriaView = WorkItem.Items.AddNew<RE_04CriteriaView>("IRE_04CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(RE_04CriteriaView);

                    break;

                case ReportName.RE_05:

                    SetWindowsTitle(Properties.Resources.RE_05);

                    IRE_05CriteriaView RE_05CriteriaView;
                    if (WorkItem.Items.Contains("IRE_05CriteriaView"))
                    {
                        RE_05CriteriaView = WorkItem.Items.Get<IRE_05CriteriaView>("IRE_05CriteriaView");
                    }
                    else
                    {
                        RE_05CriteriaView = WorkItem.Items.AddNew<RE_05CriteriaView>("IRE_05CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(RE_05CriteriaView);

                    break;

                case ReportName.RE_06:

                    SetWindowsTitle(Properties.Resources.RE_06);

                    IRE_06CriteriaView RE_06CriteriaView;
                    if (WorkItem.Items.Contains("IRE_06CriteriaView"))
                    {
                        RE_06CriteriaView = WorkItem.Items.Get<IRE_06CriteriaView>("IRE_06CriteriaView");
                    }
                    else
                    {
                        RE_06CriteriaView = WorkItem.Items.AddNew<RE_06CriteriaView>("IRE_06CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(RE_06CriteriaView);

                    break;
                case ReportName.RE_07:

                    SetWindowsTitle(Properties.Resources.RE_07);

                    IRE_07CriteriaView RE_07CriteriaView;
                    if (WorkItem.Items.Contains("IRE_07CriteriaView"))
                    {
                        RE_07CriteriaView = WorkItem.Items.Get<IRE_07CriteriaView>("IRE_07CriteriaView");
                    }
                    else
                    {
                        RE_07CriteriaView = WorkItem.Items.AddNew<RE_07CriteriaView>("IRE_07CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(RE_07CriteriaView);

                    break;
                case ReportName.RE_08:

                    SetWindowsTitle(Properties.Resources.RE_08);

                    IRE_08CriteriaView RE_08CriteriaView;
                    if (WorkItem.Items.Contains("IRE_08CriteriaView"))
                    {
                        RE_08CriteriaView = WorkItem.Items.Get<IRE_08CriteriaView>("IRE_08CriteriaView");
                    }
                    else
                    {
                        RE_08CriteriaView = WorkItem.Items.AddNew<RE_08CriteriaView>("IRE_08CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(RE_08CriteriaView);

                    break;

                case ReportName.RE_09:

                    SetWindowsTitle(Properties.Resources.RE_09);

                    IRE_09CriteriaView RE_09CriteriaView;
                    if (WorkItem.Items.Contains("IRE_09CriteriaView"))
                    {
                        RE_09CriteriaView = WorkItem.Items.Get<IRE_09CriteriaView>("IRE_09CriteriaView");
                    }
                    else
                    {
                        RE_09CriteriaView = WorkItem.Items.AddNew<RE_09CriteriaView>("IRE_09CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(RE_09CriteriaView);

                    break;      


            }

            IResultView rView;
            if (WorkItem.Items.Contains("IResultView"))
            {
                rView = WorkItem.Items.Get<IResultView>("IResultView");
            }
            else
            {
                rView = WorkItem.Items.AddNew<ResultView>("IResultView");
            }
            WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView);
        }

        private void LoadRequiredViews(ReportName report)
        {
            //
        }

        #region Event Handler

        [EventSubscription(EventTopicNames.LoadAccountClassList, Thread = ThreadOption.UserInterface)]
        public void LoadAccountClassListHandler(object sender, EventArgs<ReportName> e)
        {
            List<AccountClassInfo> retList = new List<AccountClassInfo>();
            List<AccountClassInfo> acList = new List<AccountClassInfo>();
            ReportName reportName = e.Data;
            retList = _commService.GetAccountClassList(new AccountClassInfo());
            
            AccountClassInfo acAll = new AccountClassInfo();
            acAll.AccountClassId = "0000";
            acAll.AccountClassName = "ทั้งหมด";
            acList.Add(acAll);

            foreach (AccountClassInfo ac in retList)
            {
                acList.Add(ac);
            }

            switch (reportName)
            {
                case ReportName.RE_01:

                    IRE_01CriteriaView RE_01CriteriaView;
                    if (WorkItem.Items.Contains("IRE_01CriteriaView"))
                    {
                        RE_01CriteriaView = WorkItem.Items.Get<IRE_01CriteriaView>("IRE_01CriteriaView");
                    }
                    else
                    {
                        RE_01CriteriaView = WorkItem.Items.AddNew<RE_01CriteriaView>("IRE_01CriteriaView");
                    }
                    RE_01CriteriaView.AccountClassList = acList;
                    break;

                case ReportName.RE_02:

                    IRE_02CriteriaView RE_02CriteriaView;
                    if (WorkItem.Items.Contains("IRE_02CriteriaView"))
                    {
                        RE_02CriteriaView = WorkItem.Items.Get<IRE_02CriteriaView>("IRE_02CriteriaView");
                    }
                    else
                    {
                        RE_02CriteriaView = WorkItem.Items.AddNew<RE_02CriteriaView>("IRE_02CriteriaView");
                    }
                    RE_02CriteriaView.AccountClassList = acList;
                    break;

                case ReportName.RE_03:

                    IRE_03CriteriaView RE_03CriteriaView;
                    if (WorkItem.Items.Contains("IRE_03CriteriaView"))
                    {
                        RE_03CriteriaView = WorkItem.Items.Get<IRE_03CriteriaView>("IRE_03CriteriaView");
                    }
                    else
                    {
                        RE_03CriteriaView = WorkItem.Items.AddNew<RE_03CriteriaView>("IRE_03CriteriaView");
                    }
                    RE_03CriteriaView.AccountClassList = acList;
                    break;
                case ReportName.RE_04:

                    IRE_04CriteriaView RE_04CriteriaView;
                    if (WorkItem.Items.Contains("IRE_04CriteriaView"))
                    {
                        RE_04CriteriaView = WorkItem.Items.Get<IRE_04CriteriaView>("IRE_04CriteriaView");
                    }
                    else
                    {
                        RE_04CriteriaView = WorkItem.Items.AddNew<RE_04CriteriaView>("IRE_04CriteriaView");
                    }
                    RE_04CriteriaView.AccountClassList = acList;
                    break;


                case ReportName.RE_05:

                    IRE_05CriteriaView RE_05CriteriaView;
                    if (WorkItem.Items.Contains("IRE_05CriteriaView"))
                    {
                        RE_05CriteriaView = WorkItem.Items.Get<IRE_05CriteriaView>("IRE_05CriteriaView");
                    }
                    else
                    {
                        RE_05CriteriaView = WorkItem.Items.AddNew<RE_05CriteriaView>("IRE_05CriteriaView");
                    }
                    RE_05CriteriaView.AccountClassList = acList;
                    break;

                case ReportName.RE_06:              
     
                    IRE_06CriteriaView RE_06CriteriaView;
                    if (WorkItem.Items.Contains("IRE_06CriteriaView"))
                    {
                        RE_06CriteriaView = WorkItem.Items.Get<IRE_06CriteriaView>("IRE_06CriteriaView");
                    }
                    else
                    {
                        RE_06CriteriaView = WorkItem.Items.AddNew<RE_06CriteriaView>("IRE_06CriteriaView");
                    }
                    RE_06CriteriaView.AccountClassList = acList;
                    break;

                case ReportName.RE_07:

                    IRE_07CriteriaView RE_07CriteriaView;
                    if (WorkItem.Items.Contains("IRE_07CriteriaView"))
                    {
                        RE_07CriteriaView = WorkItem.Items.Get<IRE_07CriteriaView>("IRE_07CriteriaView");
                    }
                    else
                    {
                        RE_07CriteriaView = WorkItem.Items.AddNew<RE_07CriteriaView>("IRE_07CriteriaView");
                    }
                    RE_07CriteriaView.AccountClassList = acList;
                    break;

                case ReportName.RE_08:

                    IRE_08CriteriaView RE_08CriteriaView;
                    if (WorkItem.Items.Contains("IRE_08CriteriaView"))
                    {
                        RE_08CriteriaView = WorkItem.Items.Get<IRE_08CriteriaView>("IRE_08CriteriaView");
                    }
                    else
                    {
                        RE_08CriteriaView = WorkItem.Items.AddNew<RE_08CriteriaView>("IRE_08CriteriaView");
                    }
                    RE_08CriteriaView.AccountClassList = acList;
                    break;

                case ReportName.RE_09:

                    IRE_09CriteriaView RE_09CriteriaView;
                    if (WorkItem.Items.Contains("IRE_09CriteriaView"))
                    {
                        RE_09CriteriaView = WorkItem.Items.Get<IRE_09CriteriaView>("IRE_09CriteriaView");
                    }
                    else
                    {
                        RE_09CriteriaView = WorkItem.Items.AddNew<RE_09CriteriaView>("IRE_09CriteriaView");
                    }
                    RE_09CriteriaView.AccountClassList = acList;
                    break;
            }

        }


        [EventSubscription(EventTopicNames.LoadCompanyList, Thread = ThreadOption.UserInterface)]
        public void LoadCompanyListHandler(object sender, EventArgs<CompanyParamInfo> e)
        {
            List<Company> retList = new List<Company>();
            List<Company> comList = new List<Company>();
            CompanyParamInfo comParam = e.Data;
            retList = _commService.GetCompanyList(comParam);

            Company comAll = new Company();
            comAll.CompanyId = "0000";
            comAll.CompanyName = "ทั้งหมด";
            comList.Add(comAll);

            foreach (Company ac in retList)
            {
                comList.Add(ac);
            }

            switch (comParam.TargetReport)
            {
                case ReportName.RE_01:

                    IRE_01CriteriaView RE_01CriteriaView;
                    if (WorkItem.Items.Contains("IRE_01CriteriaView"))
                    {
                        RE_01CriteriaView = WorkItem.Items.Get<IRE_01CriteriaView>("IRE_01CriteriaView");
                    }
                    else
                    {
                        RE_01CriteriaView = WorkItem.Items.AddNew<RE_01CriteriaView>("IRE_01CriteriaView");
                    }
                    RE_01CriteriaView.CompanyList = comList;
                    break;

                case ReportName.RE_02:

                    IRE_02CriteriaView RE_02CriteriaView;
                    if (WorkItem.Items.Contains("IRE_02CriteriaView"))
                    {
                        RE_02CriteriaView = WorkItem.Items.Get<IRE_02CriteriaView>("IRE_02CriteriaView");
                    }
                    else
                    {
                        RE_02CriteriaView = WorkItem.Items.AddNew<RE_02CriteriaView>("IRE_02CriteriaView");
                    }
                    RE_02CriteriaView.CompanyList = comList;
                    break;
             
                case ReportName.RE_04:

                    IRE_04CriteriaView RE_04CriteriaView;
                    if (WorkItem.Items.Contains("IRE_04CriteriaView"))
                    {
                        RE_04CriteriaView = WorkItem.Items.Get<IRE_04CriteriaView>("IRE_04CriteriaView");
                    }
                    else
                    {
                        RE_04CriteriaView = WorkItem.Items.AddNew<RE_04CriteriaView>("IRE_04CriteriaView");
                    }
                    RE_04CriteriaView.CompanyList = comList;
                    break;

                case ReportName.RE_05:

                    IRE_05CriteriaView RE_05CriteriaView;
                    if (WorkItem.Items.Contains("IRE_05CriteriaView"))
                    {
                        RE_05CriteriaView = WorkItem.Items.Get<IRE_05CriteriaView>("IRE_05CriteriaView");
                    }
                    else
                    {
                        RE_05CriteriaView = WorkItem.Items.AddNew<RE_05CriteriaView>("IRE_05CriteriaView");
                    }
                    RE_05CriteriaView.CompanyList = comList;
                    break;

                case ReportName.RE_06:

                    IRE_06CriteriaView RE_06CriteriaView;
                    if (WorkItem.Items.Contains("IRE_06CriteriaView"))
                    {
                        RE_06CriteriaView = WorkItem.Items.Get<IRE_06CriteriaView>("IRE_06CriteriaView");
                    }
                    else
                    {
                        RE_06CriteriaView = WorkItem.Items.AddNew<RE_06CriteriaView>("IRE_06CriteriaView");
                    }
                    RE_06CriteriaView.CompanyList = comList;
                    break;
                case ReportName.RE_07:

                    IRE_07CriteriaView RE_07CriteriaView;
                    if (WorkItem.Items.Contains("IRE_07CriteriaView"))
                    {
                        RE_07CriteriaView = WorkItem.Items.Get<IRE_07CriteriaView>("IRE_07CriteriaView");
                    }
                    else
                    {
                        RE_07CriteriaView = WorkItem.Items.AddNew<RE_07CriteriaView>("IRE_07CriteriaView");
                    }
                    RE_07CriteriaView.CompanyList = comList;
                    break;

                case ReportName.RE_08:

                    IRE_08CriteriaView RE_08CriteriaView;
                    if (WorkItem.Items.Contains("IRE_08CriteriaView"))
                    {
                        RE_08CriteriaView = WorkItem.Items.Get<IRE_08CriteriaView>("IRE_08CriteriaView");
                    }
                    else
                    {
                        RE_08CriteriaView = WorkItem.Items.AddNew<RE_08CriteriaView>("IRE_08CriteriaView");
                    }
                    RE_08CriteriaView.CompanyList = comList;
                    break;

                case ReportName.RE_09:

                    IRE_09CriteriaView RE_09CriteriaView;
                    if (WorkItem.Items.Contains("IRE_09CriteriaView"))
                    {
                        RE_09CriteriaView = WorkItem.Items.Get<IRE_09CriteriaView>("IRE_09CriteriaView");
                    }
                    else
                    {
                        RE_09CriteriaView = WorkItem.Items.AddNew<RE_09CriteriaView>("IRE_09CriteriaView");
                    }
                    RE_09CriteriaView.CompanyList = comList;
                    break;
            }

        }
      
        #endregion
    }
}
