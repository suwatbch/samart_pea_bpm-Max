//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// This class is the concrete implementation of a View in the Model-View-Presenter 
// pattern. Communication between the Presenter and this class is acheived through 
// an interface to facilitate separation and testability.
// Note that the Presenter generated by the same recipe, will automatically be created
// by CAB through [CreateNew] and bidirectional references will be added.
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool.ErrorHandling;

namespace PEA.BPM.AgencyManagementModule
{
    [SmartPart]
    public partial class AgentSearchAssetValueView : UserControl, IAgentSearchAssetValueView
    {
        private int _billAmount = 0;
        private SerachType _searchType = SerachType.All;
        private AgentSearchInfo _searchInfo;
        private List<AgentInfo> _agentList;

        public List<AgentInfo> AgentAssetSearchResult 
        {
            set
            {
                _agentList = value;
                LoadAgentSearchResultToGrid();
            }
        }

        public AgentSearchAssetValueView()
        {
            InitializeComponent();
            _searchInfo = new AgentSearchInfo();
            agentSearchGV.AutoGenerateColumns = false;
            searchText.Focus();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public AgentSearchAssetValueViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        private void LoadAgentSearchResultToGrid()
        {
            try
            {
                if (_agentList == null || _agentList.Count == 0)
                    MessageBox.Show(null, "��辺�����ŵ��᷹ ��س��кؤ������������ \n�к�����ö���Ҵ��� \n - ���ʵ��᷹\n - ����-ʡ��\n - �Թ��ӻ�Сѹ", "��辺��Ť����ѡ��Ѿ���ӻ�Сѹ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                agentSearchGV.Enabled = false;
                agentSearchGV.DataSource = _agentList;
                agentSearchGV.Enabled = true;
                
            }
            catch (Exception e)
            {
                Logger.WriteError(Logger.Module.AGENCY, "������Ť����ѡ��Ѿ���ӻ�Сѹ", e.ToString());
                ClientExceptionController.ShowExceptionDialog(EErrorInModule.Agency, e);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();
        }
     
        public void ShowBillAmount()
        {
            searchText.Text = _billAmount.ToString();
        }

        public void AddBillAmount(int amount)
        {
            _billAmount += amount; 
        }

        private void lineBillCostBt_Click(object sender, EventArgs e)
        {
            _billAmount = 0;
            searchText.Text = "";
            _presenter.BillBookCalculateBillAmountButtonClicked();
        }

        private void searchTSBt_ButtonClick(object sender, EventArgs e)
        {
            _searchInfo.Keyword = searchText.Text;
            _searchInfo.Type = _searchType;
            _presenter.AgentSearchAssetButtonClicked(_searchInfo);
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            if (agentSearchGV.SelectedRows.Count != 0)
            {
               
                    AgentInfo agentInfo = (AgentInfo)agentSearchGV.SelectedRows[0].DataBoundItem;
                    _presenter.AgentSearchAssetOkButtonClicked(agentInfo);
              
            }
        }

        private void agentSearchGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AgentInfo agentInfo = (AgentInfo)agentSearchGV.Rows[e.RowIndex].DataBoundItem;
                if (agentInfo.ContractValidFrom <= Session.BpmDateTime.Now && Session.BpmDateTime.Now <= agentInfo.ContractValidTo)
                {
                    if (string.Equals(agentInfo.TechBranchId, Session.Branch.Id, StringComparison.CurrentCultureIgnoreCase))
                    {
                        _presenter.AgentSearchAssetOkButtonClicked(agentInfo);
                        this.ParentForm.Close();                          
                    }
                    else
                    {
                        MessageBox.Show(null, "���᷹�����ŧ����¹����Ңҡ��俿�ҹ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else 
                {
                    MessageBox.Show(null, "���᷹���Թ��������ʶҹТͧ����͡���Թ��", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }


        #region Search options
        private void agentIdTSItem_Click(object sender, EventArgs e)
        {
            searchTSBt.Text = "������: ���ʵ��᷹";
            _searchType = SerachType.AgentId;
        }

        private void depositTSItem_Click(object sender, EventArgs e)
        {
            searchTSBt.Text = "������: ��Ť����ѡ��Ѿ���ӻ�Сѹ";
            _searchType = SerachType.Deposit;
        }

        private void agentNameTSItem_Click(object sender, EventArgs e)
        {
            searchTSBt.Text = "������: ���͵��᷹";
            _searchType = SerachType.AgentName;
        }

        private void allTSItem_Click(object sender, EventArgs e)
        {
            searchTSBt.Text = "������: ������";
            _searchType = SerachType.All;
        }
        #endregion

        private void searchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _searchInfo.Keyword = searchText.Text;
                _searchInfo.Type = _searchType;
                _searchInfo.BranchId = Session.Branch.Id;
                _presenter.AgentSearchAssetButtonClicked(_searchInfo);
            }
        }



    }
}

