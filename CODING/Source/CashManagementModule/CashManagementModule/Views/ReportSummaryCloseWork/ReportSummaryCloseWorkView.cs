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
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.CashManagementModule
{
    [SmartPart]
    public partial class ReportSummaryCloseWorkView : UserControl, IReportSummaryCloseWorkView
    {

        #region "Code Generated"

        public ReportSummaryCloseWorkView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public ReportSummaryCloseWorkViewPresenter Presenter
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


        #endregion

        #region "Properties and Variables"
        
        public void OnWaitCursor(bool set)
        {
            if (set)
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }


        #endregion   

       
        private void previewBt_Click(object sender, EventArgs e)
        {
            try
            {
                ReportParam param = new ReportParam();
                param.FromDate = new DateTime(fromDateTxt.Value.Year, fromDateTxt.Value.Month, fromDateTxt.Value.Day, 0, 0, 0);
                param.BranchId = Session.Branch.Id;

                if (allSumRb.Checked)
                {
                    param.ReportCondition = "1";
                    param.ConditionDesc = allSumRb.Text;
                }
                else if (allRb.Checked)
                {
                    param.ReportCondition = "2";
                    param.ConditionDesc = allRb.Text;
                }
                else
                {
                    AvailableCashierList ap = new AvailableCashierList();
                    List<ReportAvailableInfo> aviList = _presenter.GetAvailableCloseWork(param);
                    ap.AvailableList = aviList;
                    DialogResult dlg = ap.ShowDialog();

                    if (dlg == DialogResult.OK)
                    {
                        //reutrn list of pay-in
                        param.ReportCondition = "2";
                        param.InputList = ap.CashierList;
                    }
                    else
                        return;

                    param.ReportCondition = "3";
                    param.ConditionDesc = listRb.Text;
                }

                _presenter.ShowReport(param);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                ServiceHelper.TransformErrorMessage(ex);
            }
        }



      
    }
}



