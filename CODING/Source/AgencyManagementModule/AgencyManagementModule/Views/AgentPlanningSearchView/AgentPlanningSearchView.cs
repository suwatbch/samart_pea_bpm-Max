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
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using System.ComponentModel;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using PEA.BPM.Architecture.ArchitectureTool;

namespace PEA.BPM.AgencyManagementModule
{
    [SmartPart]
    public partial class AgentPlanningSearchView : UserControl, IAgentPlanningSearchView
    {

        //line in the list
        private BindingList<LineInfo> _searchResult = null;
        private BindingList<LineInfo> _firstResult = null;
        private Hashtable _searchResultHt = null;
        //agent to assign
        private AgentInfo _agentInfo;
        //set default here 
        private PeaInfo _peaInfo;
        private bool _loaded = false;

        public void SetSpecialHelpConfig(LineInfo lineInfo)
        {
            for (int i = 0; i < _searchResult.Count; i++)
            {
                if (_searchResult[i].LineId == lineInfo.LineId)
                {
                    _searchResult[i] = lineInfo;
                    saveBt.Enabled = true;
                    break;
                }
            }

        }

        public PeaInfo FocusPeaInfo
        {
            set
            {
                _peaInfo = value;
                FillPeaInfo();
            }
            get { return _peaInfo; }
        }

        public void CloseView()
        {
            this.ParentForm.Close();
        }

        private void FillPeaInfo()
        {
            if (_peaInfo == null || _peaInfo.Id == null) //reset box
            {
                peaCodeText.ResetText();
                peaNameText.ResetText();
                peaCodeText.Focus();
                MessageBox.Show(null, "��辺���俿�ҷ���ͧ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //fill value
                peaCodeText.Text = _peaInfo.Id;
                peaNameText.Text = _peaInfo.Name;
                lineCodeText.Enabled = true;
                portionSearchBt.Enabled = true;
                lineCodeText.Focus();
            }

            _presenter.ShowStatusText("Ready");
        }

        public AgentPlanningSearchView()
        {
            InitializeComponent();
            portionSearchGridView.AutoGenerateColumns = false;
            _searchResult = new BindingList<LineInfo>();
            _firstResult = new BindingList<LineInfo>();
            _searchResultHt = new Hashtable();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public AgentPlanningSearchViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        public void ClearSearchResult()
        {
            _searchResult.Clear();
            _searchResultHt.Clear();
        }

        public BindingList<LineInfo> SearchResult
        {
            set
            {
                //add instead of replace
                BindingList<LineInfo> newLineList = value;
                foreach (LineInfo line in newLineList)
                {
                    string combo = string.Format("{0}-{1}", line.BranchId, line.LineId);
                    if (line.LineId != null)
                    {
                        if (!_searchResultHt.Contains(combo))
                        {
                            _searchResult.Add(line);
                            _searchResultHt.Add(combo, null);
                        }
                    }
                }

                LoadLineSearchResult();

                if (!portionSearchBt.Enabled)
                {
                    FillAgentInfomation();
                }
            }
        }

        public void ClearNeedUpdateFlag()
        {
            foreach (LineInfo line in _searchResult)
            {
                line.AdvNeedUpdate = false;
                line.AgentNeedUpdate = false;
            }
        }

        public void SetDefaultCursor()
        {
            _presenter.ShowStatusText("Ready");
        }

        public AgentInfo AgentInfomation
        {
            set
            {
                //to assign agent 
                _agentInfo = value;
                FillAgentInfomation();
            }
        }

        public string AgentSearchTextField
        {
            set
            {
                if (value.Length == 12)
                    agentSearchText.Text = value.Substring(4);
                else
                    agentSearchText.Text = value;
            }
        }

        //public string AgentSearchAssetTextField
        //{
        //    set { agentSearchAssignText.Text = value; }
        //}

        public string PeaSearchTextField
        {
            set
            {
                peaCodeText.Text = value;
            }
        }

        public string PortionSearchTextField
        {
            set
            {
                lineCodeText.Text = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();
            agentSearchText.Focus();
            _presenter.ShowStatusText("Ready");
            _loaded = true;
        }

        private void portionSearchBt_Click(object sender, EventArgs e)
        {
            _presenter.PortionSearchButtonClicked(peaCodeText.Text);
        }

        private void peaCodeSearchBt_Click(object sender, EventArgs e)
        {
            _presenter.PeaCodedSearchShowDialogClicked();
        }

        private void agentSearchBt_Click(object sender, EventArgs e)
        {
            _presenter.AgentSearchButtonClicked();
            lineCodeText.Focus();
        }

        private void agentSearchAssignBt_Click(object sender, EventArgs e)
        {
            _presenter.AgentSearchAssetShowDialogButtonClicked();
        }

        //private void agentIdCBox_DropDown(object sender, EventArgs e)
        //{
        //    ArrayList agentIdList = new ArrayList();
        //    foreach (DataGridViewRow r in portionSearchGridView.Rows)
        //    {
        //        if(!agentIdList.Contains(r.Cells["AgentId"].Value))
        //            agentIdList.Add(r.Cells["AgentId"].Value);
        //    }

        //    agentIdCBox.DisplayMember = "AgentId";
        //    agentIdCBox.ValueMember = "AgentId";
        //    agentIdCBox.DataSource = agentIdList;
        //}

        //private void agentIdCBox_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    string agentId = agentIdCBox.SelectedValue.ToString();
        //    _presenter.AgentSearchCBoxCommitted(peaCodeText.Text, agentId);
        //}


        #region View Procedures

        public void DisableSaveButton()
        {
            saveBt.Enabled = false;
            removeBt.Enabled = false;
        }

        //add to list instead of replace
        public void LoadLineSearchResult()
        {
            portionSearchGridView.Enabled = false;
            portionSearchGridView.DataSource = _searchResult;
            portionSearchGridView.Enabled = true;

            if (_agentInfo == null || _agentInfo.Id == null)
            {
                saveBt.Enabled = false;
                btnSelectAll.Enabled = false;
                btnCancelAll.Enabled = false;
            }
            else
            {
                saveBt.Enabled = true;
                portionSearchBt.Enabled = true;
                lineCodeText.Enabled = true;
                btnSelectAll.Enabled = true;
                btnCancelAll.Enabled = true;
            }

            removeBt.Enabled = true;
            _presenter.ShowStatusText(string.Format("{0} Items", _searchResult.Count.ToString()));
        }

        private void FillAgentInfomation()
        {
            if (_agentInfo == null || _agentInfo.Id == null)
            {
                MessageBox.Show(null, "��辺�����ŵ��᷹����ͧ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                agentSearchText.ResetText();
                agentNameText.ResetText();
                depositText.ResetText();
                agentAddressText.ResetText();
                agentType.ResetText();
            }
            else if (!string.Equals(_agentInfo.TechBranchId, Session.Branch.Id, StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show(null, "���᷹�����ŧ����¹����Ңҡ��俿�ҹ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                agentNameText.ResetText();
                depositText.ResetText();
                agentAddressText.ResetText();
                agentType.ResetText();
                agentStatusText.ResetText();
            }
            else
            {
                if (_agentInfo.Id.Trim().Length == 12)
                {
                    agentSearchText.Text = _agentInfo.Id.Trim().Substring(4);
                }
                else
                {
                    agentSearchText.Text = _agentInfo.Id.Trim();
                }
                agentNameText.Text = string.Format("{0}: {1}", _agentInfo.Id, _agentInfo.Name);
                //phoneText.Text = _agentInfo.Contact;
                depositText.Text = DaHelper.ToMoneyFormat(_agentInfo.Deposit);
                agentAddressText.Text = _agentInfo.Address;
                agentType.Text = _agentInfo.Type;
                agentStatusText.Text = _agentInfo.AgencyStatus;

                //focus next control
                if (portionSearchGridView.Rows.Count > 0)
                {
                    saveBt.Enabled = true;
                    removeBt.Enabled = true;
                }

                agentNameText.Text = _agentInfo.Name.Trim();

                if (_agentInfo.TechBranchId != null)
                {
                    PeaInfo _peaInfo = _presenter.GetBranch(_agentInfo.TechBranchId, _agentInfo.TechBranchId);
                    if (_peaInfo != null)
                    {
                        peaCodeText.Text = _peaInfo.Id;
                        peaNameText.Text = _peaInfo.Name;
                    }
                }

                peaCodeText.Focus();
            }

            _presenter.ShowStatusText("Ready");
        }

        private void ClearAgentInfoScreenInfo()
        {
            _agentInfo = null;
            agentSearchText.ResetText();
            agentNameText.Text = "";
            //phoneText.Text = "";
            depositText.Text = "";
            agentAddressText.Text = "";
            agentType.Text = "";
            agentStatusText.Text = "";
            peaCodeText.Text = "";
            peaNameText.Text = "";
        }

        private void ClearSearchScreenInfo()
        {
            peaCodeText.Text = "";
            peaNameText.Text = "";
            lineCodeText.Text = "";
            agentSearchText.Text = "";
            lineCodeText.Enabled = false;
            agentSearchText.Focus();
            _peaInfo = null;
            _searchResult.Clear();
            _searchResultHt.Clear();
            saveBt.Enabled = false;
            removeBt.Enabled = false;
            btnSelectAll.Enabled = false;
            btnCancelAll.Enabled = false;
        }

        public void ResetScreen(bool saved)
        {
            ClearSearchScreenInfo();
            ClearAgentInfoScreenInfo();

            if (saved)
                _presenter.ShowStatusText("Saved");
        }


        public ArrayList SelectedLineList
        {
            get
            {
                ArrayList lineList = new ArrayList();
                foreach (DataGridViewRow r in portionSearchGridView.Rows)
                {
                    lineList.Add(r.Cells["LineId"].Value);
                }
                return lineList;
            }
        }

        #endregion


        //private void agentSearchAssignText_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {             
        //        //in case user wanna to input peaCode at first
        //        if (agentSearchText.Text == "")
        //        {
        //            peaCodeText.Focus();
        //            return;
        //        }

        //        //verify input here
        //        // --- code ---
        //        _presenter.ShowStatusText("Searching...");                
        //        _presenter.AgentSearchTextBoxKeyDown(agentSearchText.Text);
        //    }
        //    else if (e.KeyCode == Keys.F1)
        //    {
        //        ArrayList parem = new ArrayList();
        //        parem.Add("��˹����᷹");
        //        parem.Add("��˹����᷹���͡�˹���¡�����Թ");
        //        parem.Add("���ʵ��᷹�л�Сͺ���µ���Ţ 6 ��ѡ");
        //        parem.Add("�µ��᷹˹��� ����ö�Ѻ�Դ�ͺ 1 ���");
        //        parem.Add("������Թ��ҹ�� ������ҧ \"900011\"");
        //        _presenter.ShowHint(parem);
        //    }

        //}

        private void peaCodeText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //verify input here
                _presenter.BranchSearchTextBoxKeyDown(peaCodeText.Text);
            }
            else if (e.KeyCode == Keys.F1)
            {
                ArrayList parem = new ArrayList();
                parem.Add("���俿��");
                parem.Add("��Сͺ�������� 6 ��ѡ");
                parem.Add("����ѡ�á�����ѡ�� A-L (��Ǿ�����˭�)");
                parem.Add("���� 5 ��Ƿ������ͨ��繵���Ţ");
                parem.Add("������ҧ \"B21001\"");
                _presenter.ShowHint(parem);
            }
        }

        private void lineCodeText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lineCodeText.Text == "")
                {
                    lineCodeText.Focus();
                    //agentSearchText.Focus();
                    return;
                }

                if (_peaInfo == null)
                {
                    MessageBox.Show(null, "��سһ�͹�������Ңҡ��俿�ҡ�͹", "��͹������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    peaCodeText.Focus();
                }
                else
                {
                    if (lineCodeText.Text.Length < 4)
                        lineCodeText.Text = lineCodeText.Text.PadLeft(4, '0');

                    //verify input here - it can be "0001-0006;0010"
                    char[] spliters = { ';', ':', ',' };
                    string[] sp = lineCodeText.Text.Split(spliters);
                    foreach (string line in sp)
                    {
                        try
                        {
                            //fine dash (-)
                            string[] pe = line.Split('-');
                            if (pe.Length == 2)
                            {
                                int start = Convert.ToInt32(pe[0]);
                                int end = Convert.ToInt32(pe[1]);
                                if (end < start)
                                {
                                    MessageBox.Show(null, "��͹�����¡�����Թ���١��ͧ ��سһ�͹����", "��͹��ҼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else if (pe.Length < 2)
                            {
                                int temp = Convert.ToInt32(line);
                            }
                            else
                            {
                                MessageBox.Show(null, "��͹�����¡�����Թ���١��ͧ ��سһ�͹����", "��͹��ҼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(null, "��͹�����¡�����Թ���١��ͧ ��سһ�͹����", "��͹��ҼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    _presenter.LineSearchTextBoxKeyDown(FocusPeaInfo.Id, lineCodeText.Text);
                }
            }
            else if (e.KeyCode == Keys.F1)
            {
                ArrayList parem = new ArrayList();
                parem.Add("��¡�����Թ");
                parem.Add("��Сͺ���µ���Ţ 4 ��ѡ");
                parem.Add("��¡�����Թ�������㹡��俿��˹���");
                parem.Add("�е�ͧ�кء��俿�ҡ�͹��¡�����Թ");
                parem.Add("������ҧ \"0090\"");
                _presenter.ShowHint(parem);
            }
            else if (e.KeyCode == Keys.F12)
            {
                //save
                if (_agentInfo != null && _agentInfo.Id != null && portionSearchGridView.Rows.Count != 0)
                    SaveAssigned();
            }
        }

        private void agentSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            string agentSearch = agentSearchText.Text.Trim().PadLeft(12, '0');
            if (e.KeyCode == Keys.Enter)
            {
                //verify input here
                _presenter.AgentLineSearchTextBoxKeyDown(agentSearch);
                // peaCodeText.SelectAll();
                //peaCodeText.Focus();
                //agentSearchText.SelectAll();
                
                foreach(LineInfo lineInfo in _searchResult)
                {
                    _firstResult.Add(lineInfo);
                }

                if (portionSearchGridView.Rows.Count <= 0)
                    removeBt.Enabled = false;
                else
                    removeBt.Enabled = true;

                if (agentSearchText.Text.Trim() != "")
                    lineCodeText.Focus();
                else
                    agentSearchText.Focus();
            }
            else if (e.KeyCode == Keys.F1)
            {
                ArrayList parem = new ArrayList();
                parem.Add("���Ҵ��µ��᷹");
                parem.Add("������¡�����Թ���᷹�Ѻ�Դ�ͺ");
                parem.Add("��͹���ʵ��᷹����Сͺ���µ���Ţ 6 ��ѡ");
                parem.Add("���ͤ��ҵ��᷹����٧ �¡�á�����");
                parem.Add("������ҧ \"900011\"");
                _presenter.ShowHint(parem);
            }
            else if (e.KeyCode == Keys.F12)
            {
                //save
                if (_agentInfo != null && _agentInfo.Id != null && portionSearchGridView.Rows.Count != 0)
                    SaveAssigned();
            }
        }

        private void SaveAssigned()
        {
            bool saveConfirm = false;
            if ((_searchResult == null || _searchResult.Count == 0) && _firstResult.Count == 0)
            {
                MessageBox.Show(null, "��س����͡������¡�� ���͡�˹����Ѻ���᷹", "���͡���", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (_searchResult.Count == 0 && _firstResult.Count == 0)
                MessageBox.Show(null, "��س��кء��俿�ҡ�͹�ӡ�ä��Ҵ��µ��᷹", "�кء��俿��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DialogResult dlg = MessageBox.Show(null, "��˹���¡�����Թ���¡�����Ѻ���᷹", "��س��׹�ѹ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dlg == DialogResult.OK)
                {
                    _presenter.ShowStatusText("Saving...");

                    if (_searchResult.Count == 0 && _firstResult.Count > 0)
                    {
                        _firstResult[0].AgencyName = "DEL";
                        _presenter.SaveBtClicked(_firstResult);
                        _firstResult.Clear();
                    }
                    else
                    {
                        //assign agent to lines
                        foreach (LineInfo lineInfo in _searchResult)
                        {
                            //agenctId not changed and advpayment not change
                            if (_agentInfo != null && (lineInfo.AgentId.Trim() == _agentInfo.Id.Trim()))
                            {
                                lineInfo.AgentNeedUpdate = false;
                            }
                            else if (_agentInfo != null)
                            {
                                lineInfo.AgentNeedUpdate = true;
                                if (lineInfo.AgentId.Trim().PadLeft(12,'0') != _agentInfo.Id.Trim().PadLeft(12,'0') && lineInfo.AgentId.Trim() != String.Empty)
                                {
                                    saveConfirm = true;
                                }
                            }

                            //update all of Advpayment at this time
                            lineInfo.ModifiedBy = Session.User.Id;
                            lineInfo.AdvNeedUpdate = true;
                        }

                        if (saveConfirm)
                        {
                            DialogResult dlg2 = MessageBox.Show(null, "����¡�����Թ���١�Ѻ���᷹���������� �س��ͧ��úѹ�֡��´ѧ��������������", "��س��׹�ѹ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (dlg2 == DialogResult.OK)
                            {
                                foreach (LineInfo lineInfo in _searchResult)
                                {
                                    lineInfo.AgentId = _agentInfo.Id;
                                    lineInfo.AgentName = _agentInfo.Name;
                                }
                                _presenter.SaveBtClicked(_searchResult);
                            }
                        }
                        else
                        {
                            foreach (LineInfo lineInfo in _searchResult)
                            {
                                lineInfo.AgentId = _agentInfo.Id;
                                lineInfo.AgentName = _agentInfo.Name;
                            }
                            _presenter.SaveBtClicked(_searchResult);
                        }
                    }
                }
            }
        }

        private void saveBt_Click(object sender, EventArgs e)
        {
            SaveAssigned();
        }

        private void portionSearchGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (portionSearchGridView.SelectedRows.Count == 0) return;
            }
            else if (e.KeyCode == Keys.F12)
            {
                //save
                if (_agentInfo != null && _agentInfo.Id != null && portionSearchGridView.Rows.Count != 0)
                    SaveAssigned();
            }
        }

        private void clearBt_Click(object sender, EventArgs e)
        {
            ClearSearchResult();
            ClearAgentInfoScreenInfo();
            ClearSearchScreenInfo();
            _presenter.ShowStatusText("Ready");
        }

        private void removeBt_Click(object sender, EventArgs e)
        {
            int rowCount = _searchResult.Count;
            for (int i = rowCount - 1; i >= 0; i--)
            {
                object val = portionSearchGridView.Rows[i].Cells["CheckMark"].Value;
                if (val != null)
                {
                    if ((bool)val)
                    {
                        string lineId = _searchResult[i].LineId;
                        _searchResultHt.Remove(lineId);
                        _searchResult.RemoveAt(i);
                    }
                }
            }

            //rebind
            //LoadLineSearchResult();
            if (portionSearchGridView.Rows.Count == 0)
            {
                saveBt.Enabled = true;
                removeBt.Enabled = false;
                btnCancelAll.Enabled = false;
                btnSelectAll.Enabled = false;
            }
            else
            {
                saveBt.Enabled = true;
                removeBt.Enabled = true;
                btnCancelAll.Enabled = true;
                btnSelectAll.Enabled = true;
            }

            string statusText = string.Format("{0} Items", portionSearchGridView.Rows.Count.ToString());
            _presenter.ShowStatusText(statusText);
        }

        private void portionSearchGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_loaded && portionSearchGridView.Rows.Count > 0)
            {
                saveBt.Enabled = true;
                removeBt.Enabled = true;
            }
        }

        private void portionSearchGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //open new window for spical help config
            if (e.RowIndex >= 0)
            {
                LineInfo currentLine = (LineInfo)_searchResult[e.RowIndex];
                _presenter.SpecialHelpConfigClicked(currentLine);
            }
        }

        private void portionSearchGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 && portionSearchGridView.Columns[e.ColumnIndex].Name == "AdvPayment")
                saveBt.Enabled = true;
        }

        private void agentIdCBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ArrayList parem = new ArrayList();
                parem.Add("���᷹���¡��");
                parem.Add("Dropdown ���ʵ��᷹���Թ 6/8 ��ѡ");
                parem.Add("����ҡ��˹�Ҩ�\"��¡����¡�����Թ\"");
                parem.Add("���͡���᷹���Թ���͡�˹���¡�����Թ");
                parem.Add("������ҧ \"900011\"");
                _presenter.ShowHint(parem);
            }
        }

        #region HINT


        private void agentSearchAssignImgHint_Click(object sender, EventArgs e)
        {
            ArrayList parem = new ArrayList();
            parem.Add("��˹����᷹");
            parem.Add("��˹����᷹���͡�˹���¡�����Թ");
            parem.Add("���ʵ��᷹�л�Сͺ���µ���Ţ 6 ��ѡ");
            parem.Add("�µ��᷹˹��� ����ö�Ѻ�Դ�ͺ 1 ���");
            parem.Add("������Թ��ҹ�� ������ҧ \"900011\"");
            _presenter.ShowHint(parem);
        }


        private void agentInListImg_Click(object sender, EventArgs e)
        {
            ArrayList parem = new ArrayList();
            parem.Add("���᷹���¡��");
            parem.Add("Dropdown ���ʵ��᷹���Թ 6/8 ��ѡ");
            parem.Add("����ҡ��˹�Ҩ�\"��¡����¡�����Թ\"");
            parem.Add("���͡���᷹���Թ���͡�˹���¡�����Թ");
            parem.Add("������ҧ \"900011\"");
            _presenter.ShowHint(parem);
        }

        private void peaCodeImgHint_Click(object sender, EventArgs e)
        {
            ArrayList parem = new ArrayList();
            parem.Add("���俿��");
            parem.Add("��Сͺ�������� 6 ��ѡ");
            parem.Add("����ѡ�á�����ѡ�� A-L (��Ǿ�����˭�)");
            parem.Add("���� 5 ��Ƿ������ͨ��繵���Ţ");
            parem.Add("������ҧ \"B21001\"");
            _presenter.ShowHint(parem);
        }


        private void portionSearchImgHint_Click(object sender, EventArgs e)
        {
            ArrayList parem = new ArrayList();
            parem.Add("��¡�����Թ");
            parem.Add("��Сͺ���µ���Ţ 4 ��ѡ");
            parem.Add("��¡�����Թ�������㹡��俿��˹���");
            parem.Add("�е�ͧ�кء��俿�ҡ�͹��¡�����Թ");
            parem.Add("������ҧ \"0090\"");
            _presenter.ShowHint(parem);
        }

        private void agentSerachImgHint_Click(object sender, EventArgs e)
        {
            ArrayList parem = new ArrayList();
            parem.Add("���Ҵ��µ��᷹");
            parem.Add("������¡�����Թ���᷹�Ѻ�Դ�ͺ");
            parem.Add("��͹���ʵ��᷹����Сͺ���µ���Ţ 6 ��ѡ");
            parem.Add("���ͤ��ҵ��᷹����٧ �¡�á�����");
            parem.Add("������ҧ \"900011\"");
            _presenter.ShowHint(parem);
        }

        #endregion


        private void portionSearchGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LineInfo line = (LineInfo)portionSearchGridView.Rows[e.RowIndex].DataBoundItem;
                if (line.TravelHelp == (int)TravelHelpEnum.WATERTRAVELHELP)
                {
                    portionSearchGridView.Rows[e.RowIndex].Cells["AgencyHelp"].Value = "�Թ��������ͷҧ���";
                }
                else if (line.TravelHelp == (int)TravelHelpEnum.FARLANDHELP)
                {
                    portionSearchGridView.Rows[e.RowIndex].Cells["AgencyHelp"].Value = "�Թ��������͡�û�Ժѵԧҹ㹾�鹷���áѹ���";
                }
                else
                {
                    portionSearchGridView.Rows[e.RowIndex].Cells["AgencyHelp"].Value = "��Ҿ�˹л�Шӷҧ";
                }
            }
        }


        private void agentNameText_TextChanged(object sender, EventArgs e)
        {
            if (agentNameText.Text.Trim() == "")
            {
                peaCodeText.Text = "";
                lineCodeText.Text = "";
                peaCodeText.Enabled = false;
                lineCodeText.Enabled = false;
                peaCodeSearchBt.Enabled = false;
            }
            else
            {
                peaCodeText.Text = "";
                peaCodeText.Enabled = true;
                peaCodeSearchBt.Enabled = true;
            }
        }

        private void peaNameText_TextChanged(object sender, EventArgs e)
        {
            if (peaNameText.Text.Trim() == "")
            {
                portionSearchBt.Enabled = false;
                lineCodeText.Text = "";
                lineCodeText.Enabled = false;
            }
            else
            {
                portionSearchBt.Enabled = true;
                lineCodeText.Text = "";
                lineCodeText.Enabled = true;
            }
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            SelectAllGridView(false);
        }



        private void SelectAllGridView(bool setValue)
        {
            foreach (DataGridViewRow r in portionSearchGridView.Rows)
            {
                this.portionSearchGridView["CheckMark", r.Index].Value = setValue;
                //this.portionSearchGridView["AdvPayment", r.Index].Value = setValue;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SelectAllGridView(true);
        }
    }
}

