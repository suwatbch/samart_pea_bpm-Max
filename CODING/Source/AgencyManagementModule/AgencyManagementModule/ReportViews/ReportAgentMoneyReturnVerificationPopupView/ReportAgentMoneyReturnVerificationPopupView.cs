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
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;

using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.AgencyManagementModule.Interface.Constants;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule.ReportViews
{
    [SmartPart]
    public partial class ReportAgentMoneyReturnVerificationPopupView : UserControl, IReportAgentMoneyReturnVerificationPopupView
    {
        private DateTimeFormatInfo _th_dt;

        public ReportAgentMoneyReturnVerificationPopupView()
        {
            InitializeComponent();
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public ReportAgentMoneyReturnVerificationPopupViewPresenter Presenter
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
             
        protected void PrintButton_Click(object sender, EventArgs e)
        {
            if ((agentIDFromText.Text == String.Empty) ||
                     (periodFromText.Text.Replace("/",String.Empty).Trim() == String.Empty))
            {
                MessageBox.Show(null, "��س��к����ʵ��᷹���ͺ�Ż�Ш���͹", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            _presenter.LoadCAB02_01ReportClicked(agentIDFromText.Text, agentIDToText.Text,
                                                       periodFromText.Text, periodToText.Text);

            //ClearText();            
        }

        private void  ClearText()
        {
            periodFromText.Clear();
            periodToText.Clear();

            agentIDFromText.Clear();
            agentIDToText.Clear();
        }

        private void agentIDFromText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                agentIDToText.Enabled = true;
                agentIDToText.Focus();
                agentIDToText.SelectAll();
            }
            else if (agentIDFromText.Text.Length == ModuleConfigurationNames.AgentIdLength)
            {
                agentIDToText.Focus();
                agentIDToText.SelectAll();
            }            
        }

        private void periodFromText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || periodFromText.Text.Length == ModuleConfigurationNames.BillPeriodLength)
            {
                periodToText.Focus();
            }
            else if (e.KeyCode == Keys.N)
            {
                periodFromText.Text = Session.BpmDateTime.Now.ToString("MM/yyyy", _th_dt);
                periodToText.Focus();
                periodToText.SelectAll();
            }
        }

        private void periodToText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PrintButton.Focus();
            }
            else if (e.KeyCode == Keys.N)
            {
                periodToText.Text = Session.BpmDateTime.Now.ToString("MM/yyyy", _th_dt);
                PrintButton.Focus();
            }
        }

        private void agentIDToText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                periodFromText.Focus();
            }
            else if (agentIDToText.Text.Length == ModuleConfigurationNames.AgentIdLength)
            {
                periodFromText.Focus();
            }
        }

        private void agentIDFromText_Leave(object sender, EventArgs e)
        {
            if (agentIDFromText.Text.Trim() != "")
                agentIDToText.Enabled = true;
        }

       
    }
}

