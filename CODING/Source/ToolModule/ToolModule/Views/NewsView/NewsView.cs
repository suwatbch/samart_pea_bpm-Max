using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.SmartParts;
using PEA.BPM.NewsBroadcast.SG;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;
using PEA.BPM.NewsBroadcast.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;
using System.Globalization;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.Architecture.ArchitectureTool.NewsBroadcast;
using PEA.BPM.ToolModule.Interface.Constants;

namespace PEA.BPM.ToolModule
{
    [SmartPart]
    public partial class NewsView : UserControl, INewsView
    {
        #region Variable
        State _btState = State.Enable;

        enum State
        {
            Enable = 0,
            Diable
        };

        #endregion

        #region Constructor
        public NewsView()
        {
            InitializeComponent();

            if (Authorization.IsAuthorized(SecurityNames.EnabledNews, false))
                enableBt.Visible = true;
            else
                enableBt.Visible = false;

        }
        #endregion

        #region Presenter
        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public NewsViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }
        #endregion

        #region Button Event
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshData();
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();
            this.RefreshData();
        }

        private void closeBt_Click(object sender, EventArgs e)
        {
            _presenter.OnCloseView();
        }

        private void enableBt_Click(object sender, EventArgs e)
        {
            if (_btState == State.Enable)
            {
                Session.NewsBroadcast.Enabled = false;
                _btState = State.Diable;
                enableBt.Text = "Enable";
            }
            else
            {
                Session.NewsBroadcast.Enabled = true;
                _btState = State.Enable;
                enableBt.Text = "Disable";
            }
        }
        #endregion

        #region DataGrid
        private void gridViewBroadcastLog_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == gridViewBroadcastLog.Columns["readSymbolDataGridViewTextBoxColumn"].Index)
                {
                    if (e.Value == null) return;
                    if (e.Value.ToString() == "*")
                    {
                        gridViewBroadcastLog.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LemonChiffon;
                        Font f = new Font("Tahoma", 16);
                        e.CellStyle.Font = f;
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        e.FormattingApplied = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteError(Logger.Module.TOOL, "Cannot formatting cell in datagrid!", string.Format("Source: {0}, Stack: {1}", ex.Source, ex.ToString()));
            }
        }

        private void gridViewBroadcastLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex == (gridViewBroadcastLog.Columns["ButtonOpen"].Index))//check whether is it button column or not.....
            {
                DataGridViewRow dataGridViewRow = gridViewBroadcastLog.Rows[e.RowIndex];
                NewsBroadcastInfo newsInfo = new NewsBroadcastInfo();

                try
                {
                    newsInfo = (NewsBroadcastInfo)dataGridViewRow.DataBoundItem;
                }
                catch (Exception ex)
                {
                    Logger.WriteError(Logger.Module.TOOL, "DataRowView out of bound!", string.Format("Source: {0}, Stack: {1}", ex.Source, ex.ToString()));
                }

                bool tempIsRead = newsInfo.IsRead;

                RecieverDialogForm recDlg = new RecieverDialogForm(newsInfo);
                recDlg.ShowDialog();
            
                if (newsInfo.IsRead != tempIsRead)
                {
                    this.RefreshData();
                }
            }
        }

        private void gridViewBroadcastLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (gridViewBroadcastLog.SelectedRows.Count < 1) return;
            foreach (DataGridViewRow dataGridViewRow in gridViewBroadcastLog.SelectedRows)
            {
                NewsBroadcastInfo newsInfo = new NewsBroadcastInfo();
                try
                {
                    newsInfo = (NewsBroadcastInfo)dataGridViewRow.DataBoundItem;
                }
                catch (Exception ex)
                {
                    Logger.WriteError(Logger.Module.TOOL, "DataRowView out of bound!", string.Format("Source: {0}, Stack: {1}", ex.Source, ex.ToString()));
                }


                bool tempIsRead = newsInfo.IsRead;

                if (!newsInfo.IsOpened)
                {
                    NewsBroadcastSG sg = new NewsBroadcastSG();
                    sg.UpdateNewsBroadcastUserOpened(newsInfo.BroadcastId, Session.User.Id);
                    newsInfo.IsOpened = true;
                }

                RecieverDialogForm recDlg = new RecieverDialogForm(newsInfo);
                recDlg.ShowDialog();

                if (newsInfo.IsRead != tempIsRead)
                {
                    this.RefreshData();
                }
            }
        }
        #endregion

        #region Method
        public void RefreshData()
        {
            try
            {
                if (Session.IsNetworkConnectionAvailable)
                {
                    labelDate.Text = String.Format("วันที่ {0}", Session.BpmDateTime.Now.Date.ToString("d MMMM yyyy", new CultureInfo("th-TH")));

                    NewsBroadcastSG sg = new NewsBroadcastSG();
                    List<NewsBroadcastInfo> newsInfo = new List<NewsBroadcastInfo>();
                    newsInfo = sg.GetNewsBroadcastHistory(Session.BpmDateTime.Now, Session.User.Id, 1);

                    gridViewBroadcastLog.DataSource = newsInfo;
                }
            }
            catch (Exception)
            { return; }
        }
        #endregion

    }
}
