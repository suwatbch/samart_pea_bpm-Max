using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Interface.Constants;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface.Services;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.PaymentCollectionModule.ReportViews.CAC19CriteriaView;
using PEA.BPM.PaymentCollectionModule.ReportViews.ResultViewCenter;

namespace PEA.BPM.PaymentCollectionModule
{
    public class ReportController : WorkItemController
    {
        private ILayoutService _layoutService;
        private WindowSmartPartInfo _modalProperty;

        [InjectionConstructor]
        public ReportController([ServiceDependency] ILayoutService layoutService)
        {
            _layoutService = layoutService;

            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            _modalProperty.MaximizeBox = false;
            _modalProperty.MinimizeBox = false;
            _modalProperty.Modal = true;
        }

        public void Run(ReportName report)
        {
            LoadDisplayedViews(report);
            LoadRequiredViews(report);
        }

        private void LoadDisplayedViews(ReportName report)
        {
            ShellWaitCursor(true);
            _layoutService.LoadHorizontalLayout(230);

            switch (report)
            {
                case ReportName.CAC01_1:

                    SetWindowsTitle(Properties.Resources.RpCAC01);

                    ICAC01CriteriaView CAC01_View;
                    if (WorkItem.Items.Contains("ICAC01CriteriaView"))
                    {
                        CAC01_View = WorkItem.Items.Get<ICAC01CriteriaView>("ICAC01CriteriaView");
                    }
                    else
                    {
                        CAC01_View = WorkItem.Items.AddNew<CAC01CriteriaView>("ICAC01CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC01_View);

                    break;

                case ReportName.CAC03_1:

                    SetWindowsTitle(Properties.Resources.RpCAC03);

                    ICAC03CriteriaView CAC03_View;
                    if (WorkItem.Items.Contains("ICAC03CriteriaView"))
                    {
                        CAC03_View = WorkItem.Items.Get<ICAC03CriteriaView>("ICAC03CriteriaView");
                    }
                    else
                    {
                        CAC03_View = WorkItem.Items.AddNew<CAC03CriteriaView>("ICAC03CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC03_View);

                    break;

                case ReportName.CAC04_1:

                    SetWindowsTitle(Properties.Resources.RpCAC04);

                    ICAC04CriteriaView CAC04_View;
                    if (WorkItem.Items.Contains("ICAC04CriteriaView"))
                    {
                        CAC04_View = WorkItem.Items.Get<ICAC04CriteriaView>("ICAC04CriteriaView");
                    }
                    else
                    {
                        CAC04_View = WorkItem.Items.AddNew<CAC04CriteriaView>("ICAC04CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC04_View);

                    break;

                case ReportName.CAC05_1:

                    SetWindowsTitle(Properties.Resources.RpCAC05);

                    ICAC05CriteriaView CAC05_View;
                    if (WorkItem.Items.Contains("ICAC05CriteriaView"))
                    {
                        CAC05_View = WorkItem.Items.Get<ICAC05CriteriaView>("ICAC05CriteriaView");
                    }
                    else
                    {
                        CAC05_View = WorkItem.Items.AddNew<CAC05CriteriaView>("ICAC05CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC05_View);

                    break;

                case ReportName.CAC06_1:

                    SetWindowsTitle(Properties.Resources.RpCAC06);

                    ICAC06CriteriaView CAC06_View;
                    if (WorkItem.Items.Contains("ICAC06CriteriaView"))
                    {
                        CAC06_View = WorkItem.Items.Get<ICAC06CriteriaView>("ICAC06CriteriaView");
                    }
                    else
                    {
                        CAC06_View = WorkItem.Items.AddNew<CAC06CriteriaView>("ICAC06CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC06_View);

                    break;

                case ReportName.CAC07_1:

                    SetWindowsTitle(Properties.Resources.RpCAC07);

                    ICAC07CriteriaView CAC07_View;
                    if (WorkItem.Items.Contains("ICAC07CriteriaView"))
                    {
                        CAC07_View = WorkItem.Items.Get<ICAC07CriteriaView>("ICAC07CriteriaView");
                    }
                    else
                    {
                        CAC07_View = WorkItem.Items.AddNew<CAC07CriteriaView>("ICAC07CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC07_View);

                    break;

                case ReportName.CAC09_1:

                    SetWindowsTitle(Properties.Resources.RpCAC09);

                    ICAC09CriteriaView CAC09_View;
                    if (WorkItem.Items.Contains("ICAC09CriteriaView"))
                    {
                        CAC09_View = WorkItem.Items.Get<ICAC09CriteriaView>("ICAC09CriteriaView");
                    }
                    else
                    {
                        CAC09_View = WorkItem.Items.AddNew<CAC09CriteriaView>("ICAC09CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC09_View);

                    break;

                case ReportName.CAC11:

                    SetWindowsTitle(Properties.Resources.RpCAC11);

                    ICAC11CriteriaView CAC11_View;
                    if (WorkItem.Items.Contains("ICAC11CriteriaView"))
                    {
                        CAC11_View = WorkItem.Items.Get<ICAC11CriteriaView>("ICAC11CriteriaView");
                    }
                    else
                    {
                        CAC11_View = WorkItem.Items.AddNew<CAC11CriteriaView>("ICAC11CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC11_View);

                    break;

                case ReportName.CAC12:

                    SetWindowsTitle(Properties.Resources.RpCAC12);

                    ICAC12CriteriaView CAC12_View;
                    if (WorkItem.Items.Contains("ICAC12CriteriaView"))
                    {
                        CAC12_View = WorkItem.Items.Get<ICAC12CriteriaView>("ICAC12CriteriaView");
                    }
                    else
                    {
                        CAC12_View = WorkItem.Items.AddNew<CAC12CriteriaView>("ICAC12CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC12_View);

                    break;

                case ReportName.CAC13:

                    SetWindowsTitle(Properties.Resources.RpCAC13);

                    ICAC13CriteriaView CAC13_View;
                    if (WorkItem.Items.Contains("ICAC13CriteriaView"))
                    {
                        CAC13_View = WorkItem.Items.Get<ICAC13CriteriaView>("ICAC13CriteriaView");
                    }
                    else
                    {
                        CAC13_View = WorkItem.Items.AddNew<CAC13CriteriaView>("ICAC13CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC13_View);

                    break;

                case ReportName.CAC15:

                    SetWindowsTitle(Properties.Resources.RpCAC15);

                    ICAC15CriteriaView CAC15_View;
                    if (WorkItem.Items.Contains("ICAC15CriteriaView"))
                    {
                        CAC15_View = WorkItem.Items.Get<ICAC15CriteriaView>("ICAC15CriteriaView");
                    }
                    else
                    {
                        CAC15_View = WorkItem.Items.AddNew<CAC15CriteriaView>("ICAC15CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC15_View);

                    break;
                case ReportName.CAC18:
                    SetWindowsTitle(Properties.Resources.RpCAC18);

                    ICAC18CriteriaView CAC18_View;
                    if (WorkItem.Items.Contains("ICAC18CriteriaView"))
                    {
                        CAC18_View = WorkItem.Items.Get<ICAC18CriteriaView>("ICAC18CriteriaView");
                    }
                    else
                    {
                        CAC18_View = WorkItem.Items.AddNew<CAC18CriteriaView>("ICAC18CriteriaView");
                    }
                    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC18_View);

                    break;

                case ReportName.CAC19 :
                    SetWindowsTitle(Properties.Resources.RpCAC19);

                    ICAC19CriteriaView CAC19_View;
                    ShellWaitCursor(true);

                    if (WorkItem.Items.Contains("ICAC19CriteriaView"))
                    {
                        CAC19_View = WorkItem.Items.Get<ICAC19CriteriaView>("ICAC19CriteriaView");
                    }
                    else
                    {
                        CAC19_View = WorkItem.Items.AddNew<CAC19CriteriaView>("ICAC19CriteriaView");
                    }

                    _modalProperty.Title = Properties.Resources.RpCAC19;
                    WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(CAC19_View, _modalProperty);

                    //IResultView rView;
                    //if (WorkItem.Items.Contains("IResultView"))
                    //{
                    //    rView = WorkItem.Items.Get<IResultView>("IResultView");
                    //}
                    //else
                    //{
                    //    rView = WorkItem.Items.AddNew<ResultView>("IResultView");
                    //}
                    //WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView);

                    ShellWaitCursor(false);

                    break;

                //TODO: INSTALLMENT CASE
                //case ReportName.CAC16:

                //    SetWindowsTitle(Properties.Resources.RpCAC16);

                //    ICAC16CriteriaView CAC16_View;
                //    if (WorkItem.Items.Contains("ICAC16CriteriaView"))
                //    {
                //        CAC16_View = WorkItem.Items.Get<ICAC16CriteriaView>("ICAC16CriteriaView");
                //    }
                //    else
                //    {
                //        CAC16_View = WorkItem.Items.AddNew<CAC16CriteriaView>("ICAC16CriteriaView");
                //    }
                //    WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].Show(CAC16_View);

                //    break;
            }

            if (report != ReportName.CAC19)
            {
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
           

            //IResultView rView;
            //if (WorkItem.Items.Contains("IResultView"))
            //{
            //    rView = WorkItem.Items.Get<IResultView>("IResultView");
            //}
            //else
            //{
            //    rView = WorkItem.Items.AddNew<ResultView>("IResultView");
            //}
            //WorkItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].Show(rView);

            ShellWaitCursor(false);
        }

        private void LoadRequiredViews(ReportName report)
        {
            //
        }

        #region Event Handler

        [EventPublication(EventTopicNames.ShellWaitCursor, PublicationScope.Global)]
        public event EventHandler<EventArgs<bool>> ShellWaitCursorHandler;
        public void ShellWaitCursor(bool wait)
        {
            if (ShellWaitCursorHandler != null)
                ShellWaitCursorHandler(this, new EventArgs<bool>(wait));
        }

        #endregion
    }
}
