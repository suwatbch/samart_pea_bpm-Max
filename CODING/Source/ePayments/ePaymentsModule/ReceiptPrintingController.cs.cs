using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Constants;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.ePaymentsModule.Interface.Services;

namespace PEA.BPM.ePaymentsModule
{
    public class ReceiptPrintingController : WorkItemController
    {

        private ILayoutService _layoutService;
        private IReceiptPrintingService _receiptPrintingService;
        private ICommonService _commonService;

        [InjectionConstructor]
        public ReceiptPrintingController([ServiceDependency] ILayoutService layoutService, [ServiceDependency] IReceiptPrintingService receiptPrintingService, [ServiceDependency] ICommonService commonService)
        {
            _layoutService = layoutService;
            _receiptPrintingService = receiptPrintingService;
            _commonService = commonService;
        }

        public void Run(int type)
        {
            LoadDisplayedViews(type);
        }

        private void LoadDisplayedViews(int type)
        {
            _layoutService.LoadHorizontalLayout(230);
            Object obj;
            switch (type)
            {
                case 1:
                    IPrintGreenReceiptView lView1;
                    SetWindowsTitle(Properties.Resources.GreenReceiptEPay);
                    if (WorkItem.Items.Contains("IPrintGreenReceiptView"))
                    {
                        lView1 = WorkItem.Items.Get<IPrintGreenReceiptView>("IPrintGreenReceiptView");
                    }
                    else
                    {
                        lView1 = WorkItem.Items.AddNew<PrintGreenReceiptView>("IPrintGreenReceiptView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView1);
                   
                    obj = WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].ActiveSmartPart;
                    if (obj != null)
                        WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Close(obj);
                    break;

                case 2:
                    IPrintPPrintedReceiptView lView2;
                    SetWindowsTitle(Properties.Resources.PrePrinted);
                    if (WorkItem.Items.Contains("IPrintPPrintedReceiptView"))
                    {
                        lView2 = WorkItem.Items.Get<IPrintPPrintedReceiptView>("IPrintPPrintedReceiptView");
                    }
                    else
                    {
                        lView2 = WorkItem.Items.AddNew<PrintPPrintedReceiptView>("IPrintPPrintedReceiptView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView2);

                    obj = WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].ActiveSmartPart;
                    if (obj != null)
                        WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Close(obj);
                    break;

                case 3:
                    IPrintPPrintedDepositView lView3;
                    SetWindowsTitle(Properties.Resources.DepositPrePrtined);
                    if (WorkItem.Items.Contains("IPrintPPrintedDepositView"))
                    {
                        lView3 = WorkItem.Items.Get<IPrintPPrintedDepositView>("IPrintPPrintedDepositView");
                    }
                    else
                    {
                        lView3 = WorkItem.Items.AddNew<PrintPPrintedDepositView>("IPrintPPrintedDepositView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(lView3);

                    IPPrintedDepositResultView rView3;
                    if (WorkItem.Items.Contains("IPPrintedDepositResultView"))
                    {
                        rView3 = WorkItem.Items.Get<IPPrintedDepositResultView>("IPPrintedDepositResultView");
                    }
                    else
                    {
                        rView3 = WorkItem.Items.AddNew<PPrintedDepositResultView>("IPPrintedDepositResultView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView3);
                    break;
                default:
                    break;
            }
        }


    }
}
