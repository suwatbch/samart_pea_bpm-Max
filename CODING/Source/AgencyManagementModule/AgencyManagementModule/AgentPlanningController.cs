using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Library.UI;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using System.ComponentModel;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;
using System.Collections.Generic;

namespace PEA.BPM.AgencyManagementModule.Views
{
    public class AgentPlanningController : WorkItemController
    {
        private string _basedBranchId = "A10011";

        //keep current search results

        private List<LineInfo> _portionSearch = new List<LineInfo>();
        private ISpecialHelpConfigView _specialHelpConfigView;
        private IAgentPlanningSearchView _agentPlanningSearchView;
        private IAgencyPlanningService _agencyPlanningService;
        private IAgencyCommonService _agencyCommonService;
        private IAgentSearchView _agentSearchView;
        private IAdvancePortionSearchView _advancePortionSearchView;
        private IAgentSearchAssetValueView _agentSearchAssetValueView;
        private IPEACodeSearchView _peaCodeSearchView;
        private WindowSmartPartInfo _modalProperty;
      

        [InjectionConstructor]
        public AgentPlanningController(
            [ServiceDependency] IAgencyPlanningService agencyPlanningService,
            [ServiceDependency] IAgencyCommonService agencyCommonService
           
            )
        {
            _agencyPlanningService = agencyPlanningService;
            _agencyCommonService = agencyCommonService;            

            _modalProperty = new WindowSmartPartInfo();
            _modalProperty.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            _modalProperty.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedDialog);
            _modalProperty.MaximizeBox = false;
            _modalProperty.MinimizeBox = false;
            _modalProperty.Modal = true;

        }

        private void LoadRequiredViews()
        {
            //_advancePortionSearchView = WorkItem.Items.AddNew<AdvancePortionSearchView>("IAdvancePortionSearchView");
            //_peaCodeSearchView = WorkItem.Items.AddNew<PEACodeSearchView>("IPEACodeSearchView");
            //_agentSearchView = WorkItem.Items.AddNew<AgentSearchView>("IAgentSearchView");
            //_agentSearchAssetValueView = WorkItem.Items.AddNew<AgentSearchAssetValueView>("IAgentSearchAssetValueView");
        }

        public string BasedBranchId
        {
            set { _basedBranchId = value; }
            get { return _basedBranchId; }
        }

        public override void Run()
        {
            ShellWaitCursor(true);
            WorkItem.State["AgentPlanning"] = _portionSearch;
            if (WorkItem.Items.Contains("IAgentPlanningSearchView"))
            {
                _agentPlanningSearchView = WorkItem.Items.Get<IAgentPlanningSearchView>("IAgentPlanningSearchView");
            }
            else
            {
                _agentPlanningSearchView = WorkItem.Items.AddNew<AgentPlanningSearchView>("IAgentPlanningSearchView");
            }

            WorkItem.Workspaces[WorkspaceNames.CenterWorkspace].Show(_agentPlanningSearchView);
            SetWindowsTitle("วางแผนการเก็บเงินให้กับตัวแทน ");
            LoadRequiredViews();
            ShellWaitCursor(false);
        }

        [EventSubscription(EventTopicNames.PortionSearch, Thread = ThreadOption.UserInterface)]
        public void PortionSearchButtonClickedHandler(object sender, EventArgs<string> e)
        {
            if (WorkItem.State["IAdvancePortionSearchView"] != null)
            {
                if (WorkItem.Items.Contains("IAdvancePortionSearchView"))
                {
                    Object tmp = WorkItem.Items["IAdvancePortionSearchView"];
                    WorkItem.Items.Remove(tmp);
                }

                _advancePortionSearchView = WorkItem.Items.AddNew<AdvancePortionSearchView>("IAdvancePortionSearchView");
                _advancePortionSearchView.BranchId = e.Data;
                _modalProperty.Title = "ค้นหาสายการเก็บเงิน";
                WorkItem.State["IAdvancePortionSearchView"] = null;
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_advancePortionSearchView, _modalProperty);
            }


        }

        [EventSubscription(EventTopicNames.AgentSearch, Thread = ThreadOption.UserInterface)]
        public void AgentSearchButtonClickedHandler(object sender, EventArgs e)
        {
            if (WorkItem.State["IAgentSearchView"] != null)
            {
                if (WorkItem.Items.Contains("IAgentSearchView"))
                {
                    Object tmp = WorkItem.Items["IAgentSearchView"];
                    WorkItem.Items.Remove(tmp);
                }

                _agentSearchView = WorkItem.Items.AddNew<AgentSearchView>("IAgentSearchView");
                _modalProperty.Title = "ค้นหาตัวแทนเก็บเงิน";
                WorkItem.State["IAgentSearchView"] = null;
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_agentSearchView, _modalProperty);
            }
        }

        [EventSubscription(EventTopicNames.AgentSearchAssetShowDialog, Thread = ThreadOption.UserInterface)]
        public void AgentSearchAssetShowDialogButtonClickedHandler(object sender, EventArgs e)
        {
            if (WorkItem.Items.Contains("IAgentSearchAssetValueView"))
                WorkItem.Items.Remove(_agentSearchAssetValueView);

            _agentSearchAssetValueView = WorkItem.Items.AddNew<AgentSearchAssetValueView>("IAgentSearchAssetValueView");
            _modalProperty.Title = "ค้นหาตัวแทนเก็บเงินเพื่อกำหนดสาย";
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_agentSearchAssetValueView, _modalProperty);
        }

        [EventSubscription(EventTopicNames.PeaCodedSearchShowDialog, Thread = ThreadOption.UserInterface)]
        public void PeaCodedSearchShowDialogClickedHandler(object sender, EventArgs e)
        {
            if (WorkItem.State["IPEACodeSearchView"] != null)
            {
                if (WorkItem.Items.Contains("IPEACodeSearchView"))
                {
                    Object tmp = WorkItem.Items["IPEACodeSearchView"];
                    WorkItem.Items.Remove(tmp);
                }

                _peaCodeSearchView = WorkItem.Items.AddNew<PEACodeSearchView>("IPEACodeSearchView");
                _modalProperty.Title = "ค้นหาการไฟฟ้า";
                WorkItem.State["IPEACodeSearchView"] = null;
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_peaCodeSearchView, _modalProperty);
            }
        }

        // show special help config window
        [EventSubscription(EventTopicNames.ShowSpecialHelpConfigPopUp, Thread = ThreadOption.UserInterface)]
        public void SpecialHelpConfigPopUpHandler(object sender, EventArgs<LineInfo> e)
        {
            if (WorkItem.State["ISpecialHelpConfigView"] != null)
            {
                LineInfo line = (LineInfo)e.Data;
                Object tmpPopup = WorkItem.Items["ISpecialHelpConfigView"];
                if (WorkItem.Items.Contains("ISpecialHelpConfigView"))
                    WorkItem.Items.Remove(tmpPopup);

                _specialHelpConfigView = WorkItem.Items.AddNew<SpecialHelpConfigView>("ISpecialHelpConfigView");
                _specialHelpConfigView.SetData(line);
                _modalProperty.Title = "กำหนดเงินช่วยเหลือพิเศษ";
                WorkItem.State["ISpecialHelpConfigView"] = null;
                WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(_specialHelpConfigView, _modalProperty);
            }

        }

        [EventSubscription(EventTopicNames.SetSpeicalHelpConfig, Thread = ThreadOption.UserInterface)]
        public void SetSpeicalHelpConfigHandler(object sender, EventArgs<LineInfo> e)
        {
            LineInfo line = (LineInfo)e.Data;

            _agentPlanningSearchView.SetSpecialHelpConfig(line);
        }

        //serach box
        [EventSubscription(EventTopicNames.AgentSearchButton, Thread = ThreadOption.UserInterface)]
        public void AgentSearchFindButtonHandler(object sender, EventArgs<AgentSearchInfo> e)
        {
            try
            {
                _agentSearchView.AgentSearchResult = _agencyPlanningService.AcquireAgentAssetSearchInformation(e.Data);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }



        [EventSubscription(EventTopicNames.AgentSearchRowSelection, Thread = ThreadOption.UserInterface)]
        public void AgentSearchRowSelectionClickedHandler(object sender, EventArgs<AgentInfo> e)
        {
            try
            {
                string branchId = Session.Branch.Id;
                PeaInfo peaInfo = _agencyCommonService.FindAndDisplayBranchSearchInfo(_basedBranchId, branchId);
                if (peaInfo != null)
                    _agentPlanningSearchView.FocusPeaInfo = peaInfo;
                else
                    _agentPlanningSearchView.FocusPeaInfo = null;

                _agentPlanningSearchView.AgentSearchTextField = (string)e.Data.Id.Clone();
                _agentPlanningSearchView.ClearSearchResult();                                               
                _agentPlanningSearchView.AgentInfomation = _agencyCommonService.FindAndDisplayAgentSearchInfo(e.Data.Id);
                BindingList<LineInfo> lineList = _agencyPlanningService.FindAndDisplayLineOfAgentSearchInfo(e.Data.Id);
                _agentPlanningSearchView.SearchResult = lineList;
                //WorkItem.Workspaces[WorkspaceNames.ModalWindows].Hide(_agentSearchView);
            }
            catch (Exception ie)
            {
                //debug
                throw ie;
            }
        }

        //pea text box search
        [EventSubscription(EventTopicNames.PeaSearchRowSelection, Thread = ThreadOption.UserInterface)]
        public void PeaSearchRowSelectionClickedHandler(object sender, EventArgs<PeaInfo> e)
        {
            _agentPlanningSearchView.FocusPeaInfo = e.Data;
            //WorkItem.Workspaces[WorkspaceNames.ModalWindows].Hide(_peaCodeSearchView);
        }

        [EventSubscription(EventTopicNames.PeaSearchFindButton, Thread = ThreadOption.UserInterface)]
        public void PeaSearchFindButtonClickedHandler(object sender, EventArgs<string> e)
        {
            _peaCodeSearchView.PeaSearchResult = _agencyPlanningService.FindAndDisplayBranchByKeyword(e.Data, Session.Branch.Id);
        }

        //advance portion search box

        [EventSubscription(EventTopicNames.AdvancePortionFindButton, Thread = ThreadOption.UserInterface)]
        public void AdvancePortionFindButtonClickedHandler(object sender, EventArgs<LineSearchBoxInfo> e)
        {
            try
            {
                _advancePortionSearchView.PortionSearchResult = _agencyPlanningService.FindAndDisplayLineByKeyword(e.Data);
            }
            catch (Exception ex)
            {
                _agentPlanningSearchView.SetDefaultCursor();
                Logger.WriteError(Logger.Module.AGENCY, "ค้นหาตัวแทนเก็บเงิน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);               
            }
        }

        [EventSubscription(EventTopicNames.AdvancePortionSearchOkButton, Thread = ThreadOption.UserInterface)]
        public void AdvancePortionSearchOkButtonClickedHandler(object sender, EventArgs<ArrayList> e)
        {
            try
            {
                ArrayList parem = e.Data;
                BindingList<LineInfo> lineList = (BindingList<LineInfo>)parem[0];
                string branchId = (string)parem[1];
                string lineText = null;

                foreach (LineInfo ln in lineList)
                {
                    string lineId = ln.LineId;
                    _agencyCommonService.FindAndDisplayLineSearchInfo(branchId, lineId);
                    lineText += lineId + ";";
                }

                if (lineText != null)
                {
                    lineText = lineText.TrimEnd(';');
                    _agentPlanningSearchView.PortionSearchTextField = lineText;
                    DisplayLineList(branchId, lineText);
                }
            }
            catch (Exception ex)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
                //throw ex;
            }

        }

        //running search for asset searching 
        [EventSubscription(EventTopicNames.AgentSearchAssetButton, Thread = ThreadOption.UserInterface)]
        public void AgentSearchAssetButtonClickedHandler(object sender, EventArgs<AgentSearchInfo> e)
        {
            _agentSearchAssetValueView.AgentAssetSearchResult = _agencyPlanningService.AcquireAgentAssetSearchInformation(e.Data);
        }

        //search asset ok button clicked
        [EventSubscription(EventTopicNames.AgentSearchAssetOkButton, Thread = ThreadOption.UserInterface)]
        public void AgentSearchAssetOkButtonHandler(object sender, EventArgs<AgentInfo> e)
        {
            try
            {
                //_agentPlanningSearchView.AgentSearchAssetTextField = (string)e.Data.Id.Clone();
                _agentPlanningSearchView.AgentInfomation = e.Data.Clone();
                //WorkItem.Workspaces[WorkspaceNames.ModalWindows].Hide(_agentSearchAssetValueView);
            }
            catch (Exception ei)
            {
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ei);
            }
        }

        [EventSubscription(EventTopicNames.AgentSearchCBoxCommitted, Thread = ThreadOption.UserInterface)]
        public void AgentSearchCBoxCommittedHandler(object sender, EventArgs<string> e)
        {
            AgentInfo ag = _agencyCommonService.FindAndDisplayAgentSearchInfo(e.Data);
            _agentPlanningSearchView.AgentInfomation = ag;
        }


        #region Keydown Textbox

        [EventSubscription(EventTopicNames.AgentSearchTextBoxKeyDown, Thread = ThreadOption.UserInterface)]
        public void AgentSearchTextBoxKeyDownHandler(object sender, EventArgs<string> e)
        {
            _agentPlanningSearchView.AgentInfomation = _agencyCommonService.FindAndDisplayAgentSearchInfo(e.Data);
        }

        [EventSubscription(EventTopicNames.BranchSearchTextBoxKeyDown, Thread = ThreadOption.UserInterface)]
        public void BranchSearchTextBoxKeyDownHandler(object sender, EventArgs<string> e)
        {
            string branchId = (string)e.Data;
            //PeaInfo peaInfo = _agencyCommonService.FindAndDisplayBranchSearchInfo(_basedBranchId, branchId);
            List<PeaInfo> peaList = _agencyPlanningService.FindAndDisplayBranchByKeyword(branchId, branchId);
            PeaInfo peaInfo = peaList.Count > 0? peaList[0]: null;
            if (peaInfo != null)
                _agentPlanningSearchView.FocusPeaInfo = peaInfo;
            else
                _agentPlanningSearchView.FocusPeaInfo = null;
        }

        private void DisplayLineList(string branchId, string lineKey)
        {
            List<PeaInfo> peaInfo = _agencyPlanningService.FindAndDisplayBranchByKeyword(branchId, branchId);
            if (peaInfo.Count > 0)
                _agentPlanningSearchView.FocusPeaInfo = peaInfo[0];
            else
                _agentPlanningSearchView.FocusPeaInfo = null;

            BindingList<LineInfo> lineInfoList = _agencyCommonService.FindAndDisplayLineSearchInfo(branchId, lineKey);
            if (lineInfoList.Count != 0)
                _agentPlanningSearchView.SearchResult = lineInfoList;
            else
                _agentPlanningSearchView.SetDefaultCursor();
        }

        [EventSubscription(EventTopicNames.LineSearchTextBoxKeyDown, Thread = ThreadOption.UserInterface)]
        public void LineSearchTextBoxKeyDownHandler(object sender, EventArgs<List<string>> e)
        {
            DisplayLineList(e.Data[0], e.Data[1]);
        }

        [EventSubscription(EventTopicNames.AgentLineSearchTextBoxKeyDown, Thread = ThreadOption.UserInterface)]
        public void AgentLineSearchTextBoxKeyDownHandler(object sender, EventArgs<string> e)
        {

            string branchId = Session.Branch.Id;
            PeaInfo peaInfo = _agencyCommonService.FindAndDisplayBranchSearchInfo(_basedBranchId, branchId);
            if (peaInfo != null)
                _agentPlanningSearchView.FocusPeaInfo = peaInfo;
            else
                _agentPlanningSearchView.FocusPeaInfo = null;

            AgentInfo agentInfo = _agencyCommonService.FindAndDisplayAgentSearchInfo(e.Data);
  
            if (!string.Equals(agentInfo.TechBranchId, Session.Branch.Id, StringComparison.CurrentCultureIgnoreCase))
            {
                agentInfo.TechBranchId = "";
                _agentPlanningSearchView.ClearSearchResult();
                _agentPlanningSearchView.ResetScreen(false);
                _agentPlanningSearchView.AgentInfomation = agentInfo;
                return;
            }

            BindingList<LineInfo> lineInfoList = _agencyPlanningService.FindAndDisplayLineOfAgentSearchInfo(e.Data);
            
            if (lineInfoList.Count != 0)
            {
                //clear all first
                _agentPlanningSearchView.ClearSearchResult();
                _agentPlanningSearchView.AgentInfomation = agentInfo;
                _agentPlanningSearchView.SearchResult = lineInfoList;
            }
            else
            {
                _agentPlanningSearchView.SearchResult = lineInfoList;
                _agentPlanningSearchView.AgentInfomation = agentInfo;
                _agentPlanningSearchView.ClearSearchResult();
                _agentPlanningSearchView.SetDefaultCursor();
            }
        }

        #endregion


        #region Button Clicked

        [EventSubscription(EventTopicNames.SaveBtClicked, Thread = ThreadOption.UserInterface)]
        public void SaveBtClickedHandler(object sender, EventArgs<BindingList<LineInfo>> e)
        {
            try
            {
                bool success = _agencyPlanningService.SaveAssignedLineofAgent(null, e.Data);
                if (success)
                {
                    //update search list
                    if (e.Data[0].AgencyName != "DEL")
                    {
                        _agentPlanningSearchView.SearchResult = (BindingList<LineInfo>)e.Data;
                    }
                    _agentPlanningSearchView.DisableSaveButton();
                    //clear all flag
                    _agentPlanningSearchView.ClearNeedUpdateFlag();
                    MessageBox.Show(null, "บันทึกข้อมูลเรียบร้อยแล้ว", "บันทึก", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _agentPlanningSearchView.ResetScreen(true);
                }
            }
            catch (Exception ex)
            {
                _agentPlanningSearchView.SetDefaultCursor();
                Logger.WriteError(Logger.Module.AGENCY, "กำหนดสายให้กับตัวแทน", ex.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, ex);
            }
        }

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
