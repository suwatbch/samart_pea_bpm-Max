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
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureTool.Control;
using System.Drawing;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;

namespace PEA.BPM.ePaymentsModule
{
    [SmartPart]
    public partial class RE_01CriteriaView : UserControl, IRE_01CriteriaView
    {
        public RE_01CriteriaView()
        {
            InitializeComponent();
        }


        public List<AccountClassInfo> AccountClassList
        {
            set
            {
                List<AccountClassInfo> acList = value;
                if (acList.Count > 0)
                {
                    ddlAccountClass.DisplayMember = "DisplayName";
                    ddlAccountClass.DataSource = acList;
                }
            }
        }

        public List<Company> CompanyList
        {
            set
            {
                List<Company> acList = value;
                if (acList.Count > 0)
                {
                    ddlAgent.DisplayMember = "DisplayName";
                    ddlAgent.DataSource = acList;
                }
            }
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public RE_01CriteriaViewPresenter Presenter
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
            loadAccountClassList();
            txtBranchId.Text = Session.Branch.Id;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                RE01Param param = new RE01Param();
                param.BranchId = txtBranchId.Text;
                param.CompanyId = ((Company)ddlAgent.SelectedItem).CompanyId;
                param.AccountClassId = ((AccountClassInfo)ddlAccountClass.SelectedItem).AccountClassId;
                param.UploadDt = cmbUploadDt.Value.Date;
                param.RunningBranchId = Session.Branch.Id;

                _presenter.OnRE_01Report(param);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearData();
        }

        #region "Helper Function"
        private void clearData()
        {
            txtBranchId.Text = Session.Branch.Id;           
            ddlAccountClass.SelectedIndex = 0;
            ddlAgent.SelectedIndex = 0;
            cmbUploadDt.Value = DateTime.Now;
        }

        private void loadAccountClassList()
        {
            _presenter.LoadAccountClassList(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.ReportName.RE_01);
        }

        private bool IsValid()
        {
            if (txtBranchId.Text.Trim() == String.Empty)
            {
                MessageBox.Show("��س��к��Ңҡ��俿�ҷ���ͧ����͡��§ҹ", "����͹", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }         
            return true;
        }
        #endregion

        private void ddlAccountClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AccountClassInfo ac = ddlAccountClass.SelectedItem as AccountClassInfo;
                CompanyParamInfo comParam = new CompanyParamInfo();
                comParam.TargetReport = ReportName.RE_01;
                comParam.AccountClassId = ac.AccountClassId;
                _presenter.LoadCompanyList(comParam);
            }
            catch (Exception ex)
            {
                WaitingFormHelper.HideWaitingForm();
                Logger.WriteError(Logger.Module.EPAYMENT, "�ʴ������� Company RE_01", ex.ToString());
                MessageBox.Show("�������ö�Դ��͡Ѻ�ҹ�������� �ô�Դ��ͼ������к�\n", "��ͼԴ��Ҵ",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBranchId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddlAccountClass.Focus();
            }
        }

    }
}

