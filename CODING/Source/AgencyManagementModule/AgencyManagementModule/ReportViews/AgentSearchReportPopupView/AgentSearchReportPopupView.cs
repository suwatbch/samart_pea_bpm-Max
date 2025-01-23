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

namespace PEA.BPM.AgencyManagementModule
{
    [SmartPart]
    public partial class AgentSearchReportPopupView : UserControl, IAgentSearchReportPopupView
    {
        private List<AgentInfo> _agentList;
        private AgentInfo _agentInfo = null;
        private SerachType _searchType;
        private int _sendType = 1;

        public AgentInfo SelectedAgentInfo
        {
            get { return _agentInfo; }
        }

        public AgentSearchReportPopupView()
        {
            InitializeComponent();
            _agentInfo = new AgentInfo();
            _searchType = SerachType.All;            
            agentSearchGV.AutoGenerateColumns = false;
            searchText.Focus();
        }

        public int SendType
        {
            get { return this._sendType; }
            set { this._sendType = value; }
        }

        public List<AgentInfo> AgentSearchResult
        {
            set
            {
                _agentList = value;
                LoadAgentSearchResultToGrid();
            }
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public AgentSearchReportPopupViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();
        }

        private void LoadAgentSearchResultToGrid()
        {
            if (_agentList == null || _agentList.Count == 0)
                MessageBox.Show(null, "��辺�����ŵ��᷹ ��س��кؤ������������ \n�к�����ö���Ҵ��� \n - ���ʵ��᷹\n - ����-ʡ��\n - ��Ť����ѡ��Ѿ���ӻ�Сѹ", "��辺�����ŵ��᷹", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            agentSearchGV.Enabled = false;
            agentSearchGV.DataSource = _agentList;
            agentSearchGV.Enabled = true;            
        }

        private void agentSearchGV_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            okBt.Enabled = true;
        }

        private void agentSearchGV_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            okBt.Enabled = false;
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
                        agentInfo.SendType = SendType;
                        _presenter.AgentSearchRowSelectionClicked(agentInfo);
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

        private void okBt_Click(object sender, EventArgs e)
        {
            if (agentSearchGV.SelectedRows.Count != 0)
            {
                AgentInfo agentInfo = (AgentInfo)agentSearchGV.SelectedRows[0].DataBoundItem;
                agentInfo.SendType = SendType;
                _presenter.AgentSearchRowSelectionClicked(agentInfo);                
            }                  
        }

        private void searchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {               
                AgentSearchInfo searchInfo = new AgentSearchInfo();
                searchInfo.Keyword = searchText.Text;
                searchInfo.Type = _searchType;
                searchInfo.BranchId = Session.Branch.Id;
                _presenter.AgentSearchFindButtonClicked(searchInfo);
            }
        }

        private void searchTSBt_ButtonClick(object sender, EventArgs e)
        {           
            AgentSearchInfo searchInfo = new AgentSearchInfo();
            searchInfo.Keyword = searchText.Text;
            searchInfo.Type = _searchType;
            searchInfo.BranchId = Session.Branch.Id;
            _presenter.AgentSearchFindButtonClicked(searchInfo);
        }

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

    }
}

