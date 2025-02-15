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
using PEA.BPM.CashManagementModule.Interface.Constants;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.Architecture.CommonUtilities;
using System.ComponentModel;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.CashManagementModule.CashManagementUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;

namespace PEA.BPM.CashManagementModule
{
    [SmartPart]
    public partial class CancelBankDeliveryView : UserControl, ICancelBankDeliveryView
    {
        private List<BankDeliveryInfo> _bankDeliveryList;
        private System.Timers.Timer _timer;
        private bool _loadReady = false;

        #region "Code Generated"

        public CancelBankDeliveryView()
        {
            InitializeComponent();
            bankDeliveryGv.AutoGenerateColumns = false;
            _timer = new System.Timers.Timer();
            _timer.Interval = 100;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(OnLoadTicker);
            _timer.Start(); 
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public CancelBankDeliveryViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        private void OnLoadTicker(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (!_loadReady)
            {
                _timer.Stop();
                _loadReady = true;
                _presenter.ListBankDelivery(Session.Work.Id);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();
        }

        #endregion

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

        public List<BankDeliveryInfo> BankDeliveryList
        {
            set { _bankDeliveryList = value;
                    FillBankDelivery();
               }
            get { return _bankDeliveryList; }
        }

        private void FillBankDelivery()
        {
            bankDeliveryGv.DataSource = _bankDeliveryList;

            //select first row
            if (_bankDeliveryList.Count > 0 && bankDeliveryGv.SelectedRows.Count > 0)
            {
                BankDeliveryInfo bl = (BankDeliveryInfo)bankDeliveryGv.SelectedRows[0].DataBoundItem;
                decimal sumAmt = bl.CashAmt.Value + bl.ChequeAmt.Value;

                if (sumAmt == 0)
                    totalAmtTxt.Text = "0.00";
                else
                    totalAmtTxt.Text = sumAmt.ToString("#,###.00");

                reportBt.Enabled = true;
            }

            this.Cursor = Cursors.Default;
        }

        private bool CheckExisted()
        {
            foreach (DataGridViewRow r in bankDeliveryGv.Rows)
            {
                bool check = (bool)r.Cells["CancelCheck"].Value;
                if (check) return true;
            }

            return false;
        }

        private WorkStatus IsClosedWork()
        {
            return Authorization.IsForcedToCloseWork(Session.Work.Id);
        }

        private void CancelBankDelivery()
        {
            try
            {
                WorkStatus ci = IsClosedWork();
                if (ci.CloseWorkBy == null)
                {
                    if (bankDeliveryGv.SelectedRows.Count > 0)
                    {
                        BankDeliveryInfo binfo = (BankDeliveryInfo)bankDeliveryGv.SelectedRows[0].DataBoundItem;

                        DialogResult dlg = MessageBox.Show("  �س��ͧ���¡��ԡ��¡�ùӽҡ��Ҥ�� \n  " + binfo.BankDesc + "\n  ������ OK �����׹�ѹ���¡��ԡ", "��س��׹�ѹ���¡��ԡ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                        if (dlg == DialogResult.OK)
                        {
                            if (Authorization.IsAuthorized(SecurityNames.CancelBankDelivery, true))
                            {
                                this.Cursor = Cursors.WaitCursor;
                                _presenter.CancelBankDelivery(binfo);

                                //reload
                                _presenter.ListBankDelivery(Session.Work.Id);
                                totalAmtTxt.Text = "0.00";

                                if (bankDeliveryGv.Rows.Count == 0)
                                    reportBt.Enabled = false;

                                this.Cursor = Cursors.Default;
                            }
                        }
                    }
                }
                else
                {
                    NotifyMsg.ShowForceCloseWorkMsg(ci);
                    _presenter.OnCashierOpenWork("tmp");
                    _presenter.OnCloseView();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                ServiceHelper.TransformErrorMessage(ex);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            CancelBankDelivery();
        }

        private void bankDeliveryGv_SelectionChanged(object sender, EventArgs e)
        {
            if (bankDeliveryGv.SelectedRows.Count > 0)
            {
                BankDeliveryInfo deliveryInfo = (BankDeliveryInfo)bankDeliveryGv.SelectedRows[0].DataBoundItem;
                decimal sumAmt = deliveryInfo.CashAmt.Value + deliveryInfo.ChequeAmt.Value;

                if (sumAmt == 0)
                    totalAmtTxt.Text = "0.00";
                else
                    totalAmtTxt.Text = sumAmt.ToString("#,###.00");
            }
        }

        private void reportBt_Click(object sender, EventArgs e)
        {
            if (bankDeliveryGv.SelectedRows.Count > 0)
            {
                BankDeliveryInfo deliveryInfo = (BankDeliveryInfo)bankDeliveryGv.SelectedRows[0].DataBoundItem;
                if (deliveryInfo.ChequeAmt <= 0)
                    MessageBox.Show("��¡�ùӽҡ��Ҥ�ù������¡���Թʴ", "��辺��������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    DialogResult dlg = MessageBox.Show("�س��ͧ����ʴ���§ҹ��������´Ṻ��ù��Թ�ҡ��Ҥ��\n������ OK �����ʴ���§ҹ ", "��س��׹�ѹ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dlg == DialogResult.OK)
                        _presenter.ShowBankPayInDetail(deliveryInfo.APId);
                }
            }
        }

        private void cancelAllButton_Click(object sender, EventArgs e)
        {
            _presenter.OnCloseView();
        }

        private void bankDeliveryGv_DoubleClick(object sender, EventArgs e)
        {
            CancelBankDelivery();
        }
    }
}



