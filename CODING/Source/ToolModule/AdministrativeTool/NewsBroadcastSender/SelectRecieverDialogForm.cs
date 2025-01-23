using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using PEA.BPM.NewsBroadcast.SG;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;

namespace AdministrativeTool.NewsBroadcastSender
{
    public partial class SelectRecieverDialogForm : Form
    {
        //NewsBroadcastSG sg = new NewsBroadcastSG();
        List<BranchInfo> branchList = new List<BranchInfo>();
        private DateTimeFormatInfo _th_dt;

        public SelectRecieverDialogForm()
        {
            InitializeComponent();
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;
        }

        public int BroadcastId { get; set; }

        private NewsBroadcastSG GetService()
        {
            return new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
        }


        private void SelectRecieverDialogForm_Load(object sender, EventArgs e)
        {

            dataGridViewUserList.DataSource = GetService().GetNewsBroadcastUser(BroadcastId);   
            //gridViewArea.DataSource = sg.GetArea();
            //gridViewArea.Rows[0].Selected = true;
            
            
            //gridViewBranch.DataSource = sg.GetBranch(((AreaInfo)dataGridViewRow.DataBoundItem).AreaId);
            //gridViewBranch.DataSource = sg.GetBranch(String.Empty);

        }

        private void gridViewArea_SelectionChanged(object sender, EventArgs e)
        {
            //if (gridViewArea.SelectedRows.Count < 1) return;
            //DataGridViewRow dataGridViewRow = gridViewArea.SelectedRows[0];
            //gridViewBranch.DataSource = sg.GetBranch(((AreaInfo)dataGridViewRow.DataBoundItem).AreaId);
        }

        private void gridViewBranch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void gridViewBranch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //gridViewBranch.EndEdit();
            //DataGridViewRow dataGridViewRow = gridViewBranch.Rows[e.RowIndex];
            //bool s = (bool)dataGridViewRow.Cells["CheckBoxBranch"].Value;
            ///*DataGridViewCell dataGridViewCell*/ //bool s = (bool)gridViewBranch.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
            ////if ((bool)dataGridViewCell.EditedFormattedValue)
            //if (s)
            //{
            //    branchList.Add((BranchInfo)dataGridViewRow.DataBoundItem);
            //    //bool isHave = false;
            //    //foreach (BranchInfo b in branchList)
            //    //{
            //    //    if (b.BranchId == ((BranchInfo)dataGridViewRow.DataBoundItem).BranchId)
            //    //        isHave = true;
            //    //}
            //    //if (!isHave)
            //    //{
                    
            //    //}
            //}
            //else
            //{
            //    //foreach (BranchInfo b in branchList)
            //    //{
            //    //    if (b.BranchId == ((BranchInfo)dataGridViewRow.DataBoundItem).BranchId)
                        
            //    //}
            //    branchList.Remove((BranchInfo)dataGridViewRow.DataBoundItem);
            //}

            //gridViewSelected.DataSource = null;
            //gridViewSelected.DataSource = branchList;
            ////gridViewSelected.Sort(gridViewSelected.Columns["branchIdDataGridViewTextBoxColumn1"], ListSortDirection.Ascending);
            
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow dataGridViewRow in gridViewBranch.Rows)
            //{
            //    DataGridViewCell dataGridViewCell = gridViewBranch.Rows[dataGridViewRow.Index].Cells[0];
            //    dataGridViewCell.Value = true;
            //    bool isHave = false;
            //    foreach (BranchInfo b in branchList)
            //    {
            //        if (b.BranchId == ((BranchInfo)dataGridViewRow.DataBoundItem).BranchId)
            //            isHave = true;
            //    }
            //    if(!isHave)
            //    {
            //         branchList.Add((BranchInfo)dataGridViewRow.DataBoundItem);
            //    }
            //}
            //gridViewSelected.DataSource = null;
            //gridViewSelected.DataSource = branchList;
        }

        private void buttonDeselectAll_Click(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow dataGridViewRow in gridViewBranch.Rows)
            //{
            //    DataGridViewCell dataGridViewCell = gridViewBranch.Rows[dataGridViewRow.Index].Cells[0];
            //    dataGridViewCell.Value = false;
            //    //foreach (BranchInfo b in branchList)
            //    //{
            //    //    if (b.BranchId == ((BranchInfo)dataGridViewRow.DataBoundItem).BranchId)
                        
            //    //}
            //   branchList.Remove((BranchInfo)dataGridViewRow.DataBoundItem);
            //}
            //gridViewSelected.DataSource = null;
            //gridViewSelected.DataSource = branchList;
        }

        private void gridViewBranch_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow dataGridViewRow = gridViewBranch.Rows[e.RowIndex];
            //bool s = (bool)dataGridViewRow.Cells["CheckBoxBranch"].FormattedValue; 
            ///*DataGridViewCell dataGridViewCell*/
            ////bool s = (bool)gridViewBranch.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
            ////if ((bool)dataGridViewCell.EditedFormattedValue)
            //if (s)
            //{
            //    branchList.Add((BranchInfo)dataGridViewRow.DataBoundItem);
            //    //bool isHave = false;
            //    //foreach (BranchInfo b in branchList)
            //    //{
            //    //    if (b.BranchId == ((BranchInfo)dataGridViewRow.DataBoundItem).BranchId)
            //    //        isHave = true;
            //    //}
            //    //if (!isHave)
            //    //{

            //    //}
            //}
            //else
            //{
            //    //foreach (BranchInfo b in branchList)
            //    //{
            //    //    if (b.BranchId == ((BranchInfo)dataGridViewRow.DataBoundItem).BranchId)

            //    //}
            //    branchList.Remove((BranchInfo)dataGridViewRow.DataBoundItem);
            //}

            //gridViewSelected.DataSource = null;
            //gridViewSelected.DataSource = branchList;
            //gridViewSelected.Sort(gridViewSelected.Columns["branchIdDataGridViewTextBoxColumn1"], ListSortDirection.Ascending);
        }



        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewUserList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
                if ((DateTime)e.Value != DateTime.MinValue)
                {
                    e.CellStyle.BackColor = Color.LightGray;  
                }
            if (e.ColumnIndex == 7)
                if ((DateTime)e.Value != DateTime.MinValue)
                {

                    e.CellStyle.BackColor = Color.LightGray; 
                }
        }



    }
}