using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;
using PEA.BPM.NewsBroadcast.Interface.Constants;
using PEA.BPM.NewsBroadcast.SG;
using AdministrativeTool.Common;


namespace AdministrativeTool.NewsBroadcastSender
{
    public partial class SenderDialogForm : Form
    {
        #region Variable
        DataTable _AreaDT;
        DataTable _BranchAreaDT;
        private DateTimeFormatInfo _th_dt;
        //BroadcastWebService.BroadcastWebService ws = new BroadcastWebService.BroadcastWebService();
        
        int _reciever;
        #endregion

        #region Properties
        private OperationMode _mode;
        public OperationMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        private NewsBroadcastSentInfo _nbInfo;
        public NewsBroadcastSentInfo NbInfo
        {
            get { return _nbInfo; }
            set { _nbInfo = value; }
        }


        private DataRow _broadcastSelectedRow;
        public DataRow BroadcastSelectedRow
        { 
            get { return _broadcastSelectedRow; }
            set { _broadcastSelectedRow = value; }
        }

        private NewsBroadcastSG GetService()
        {
            return new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
        }

        #endregion

        #region Constructor
        public SenderDialogForm()
        {
            InitializeComponent();
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;
        }

        public SenderDialogForm(NewsBroadcastSentInfo senderInfo)
        {
            InitializeComponent();

            #region Initialize DataTable
            List<AreaInfo> areaList = new List<AreaInfo>();
            List<BranchInfo> branchList = new List<BranchInfo>();
            List<NewsBroadcastUserInfo> broadcastUserList = new List<NewsBroadcastUserInfo>();
            try
            {
                //if (Session.IsNetworkConnectionAvailable) //already registered
                //{
                //    //BroadcastWebService.BroadcastWebService ws = new BroadcastWebService.BroadcastWebService();
                //    //BroadcastSG ws = new BroadcastSG("http://172.30.241.72/BPMNewsBroadcast/");
                //    NewsBroadcastSG ws = new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
                //    areaList = ws.GetArea();
                //    branchList = ws.GetBranch(null);
                //    dataGridViewRole.DataSource = ws.GetRole();
                //}
                //NewsBroadcastSG ws = new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
                areaList = GetService().GetArea();
                branchList = GetService().GetBranch(String.Empty);
                dataGridViewRole.DataSource = GetService().GetRole();
                comboBoxBranch.DataSource = GetService().GetBranch(String.Empty);
                textBoxTopic.Text = senderInfo.BroadcastTopic;
                richTextBoxDetail.Rtf = senderInfo.Detail;
                broadcastUserList = GetService().GetNewsBroadcastUser(senderInfo.BroadcastId);
             

            }
            catch (Exception)
            {
                throw;
            }
            
            #endregion
        }
        #endregion

        #region Event

        #region Form
        private void SenderDialogForm_Load(object sender, EventArgs e)
        {
            try
            {
                //List<AreaInfo> areaList = new List<AreaInfo>();
                //List<BranchInfo> branchList = new List<BranchInfo>();
                //NewsBroadcastSG ws = new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
                //areaList = ws.GetArea();
                //branchList = ws.GetBranch(null);
                comboBoxArea.DataSource = GetService().GetArea();
                dataGridViewRole.DataSource = GetService().GetRole();
                comboBoxBranch.DataSource = GetService().GetBranch(String.Empty);
                dataGridViewRole.ClearSelection();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            switch (Mode)
            {
                case OperationMode.View:
                    textBoxTopic.ReadOnly = true;
                    richTextBoxDetail.ReadOnly = true;
                    buttonSend.Visible = false;
                    //textBoxTopic.Text = _broadcastSelectedRow[ColumnInfo.NewsBroadcast.BroadcastTopic].ToString();
                    //richTextBoxDetail.Rtf = _broadcastSelectedRow[ColumnInfo.NewsBroadcast.Detail].ToString();

                    textBoxTopic.Text = _nbInfo.BroadcastTopic;
                    richTextBoxDetail.Rtf = _nbInfo.Detail;
                    dateTimePicker1.Value = _nbInfo.SentDt;
                    dateTimePicker2.Value = _nbInfo.ExpireDt;
                    maskedTextBox1.Text = _nbInfo.SentDt.ToString("HH:mm");
                    maskedTextBox2.Text = _nbInfo.ExpireDt.ToString("HH:mm");
                    //comboBoxArea.DataSource = _AreaDT;
                    //comboBoxArea.DisplayMember = ColumnInfo.Area.AreaId;
                    //comboBoxArea.ValueMember = ColumnInfo.Area.AreaId;
                    //comboBoxArea.SelectedValue = _broadcastSelectedRow[ColumnInfo.Area.AreaId].ToString();
                    //switch (_broadcastSelectedRow[ColumnInfo.NewsBroadcast.RecieverId].ToString())
                    //{
                    //    case "1":
                    //        radioButton1All.Checked = true;
                    //        break;
                    //    case "2":
                    //        radioButton2Center.Checked = true;
                    //        break;
                    //    case "3":
                    //        radioButton3Branch.Checked = true;
                    //        break;
                    //    case "4":
                    //        radioButton4Private.Checked = true;
                    //        comboBoxBranch.SelectedValue = _broadcastSelectedRow[ColumnInfo.NewsBroadcast.BranchId].ToString();
                    //        break;
                    //}

                    radioButton1All.Enabled = false;
                    radioButton4Area.Enabled = false;
                    //radioButton2Center.Enabled = false;
                    //radioButton3Branch.Enabled = false;
                    radioButton2FixedRegis.Enabled = false;
                    radioButton5Branch.Enabled = false;
                    radioButton3NonFixedRegis.Enabled = false;
                    //comboBoxArea.Enabled = false;
                    //comboBoxBranch.Enabled = false;
                  
                    radioButton6SentDateNow.Enabled = false;
                    radioButton7SentDateFixed.Enabled = false;
                    radioButton8EndDateNow.Enabled = false;
                    radioButton9EndDateFixed.Enabled = false;
                    dateTimePicker1.Enabled = false;
                    dateTimePicker2.Enabled = false;
                    maskedTextBox1.Enabled = false;
                    maskedTextBox2.Enabled = false;
                    dataGridViewRole.ReadOnly = true;
                    buttonCopyMessage.Visible = true;
                    buttonPasteMessage.Visible = false;
                    buttonViewSentUserList.Visible = true;
                    break;
                case OperationMode.Edit:
                    textBoxTopic.ReadOnly = false;
                    richTextBoxDetail.ReadOnly = false;
                    buttonSend.Text = "Save";
                    buttonSend.Visible = true;
                 
                    break;
                case OperationMode.Add:
                    //_reciever = (int)RecieverInfo.All;
                    textBoxTopic.ReadOnly = false;
                    richTextBoxDetail.ReadOnly = false;
                    buttonSend.Visible = true;
                    buttonRoleSelectAll.Enabled = true;
                    buttonRoleClear.Enabled = true;
                    //dataGridViewRole.ReadOnly = false;
                    buttonCopyMessage.Visible = true;
                    buttonPasteMessage.Visible = true;
                    buttonViewSentUserList.Visible = false;
                    //comboBoxArea.Enabled = false;
                    //comboBoxBranch.Enabled = false;

                    //comboBoxArea.DataSource = _AreaDT;
                    //comboBoxArea.DisplayMember = ColumnInfo.Area.AreaId;
                    //comboBoxArea.ValueMember = ColumnInfo.Area.AreaId;

                    //comboBoxArea.SelectedIndex = 0;

                    textBoxTopic.Focus();
                    break;
            }

        }
        #endregion

        #region Button
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            if (textBoxTopic.Text.Trim() == "" || richTextBoxDetail.Rtf.Trim() == "")
            {
                MessageBox.Show("กรุณากรอกหัวข้อข่าวและรายละเอียดให้ครบ");
                return;
            }

            #region Role Selected Check
            bool isRoleSelected = false;
            foreach (DataGridViewRow dataGridViewRow in dataGridViewRole.Rows)
            {
                DataGridViewCell dataGridViewCell = dataGridViewRow.Cells["CheckBoxRole"];
                if ((bool)dataGridViewCell.EditedFormattedValue == true)
                   isRoleSelected = true;
            }

            if (!isRoleSelected)
            {
                MessageBox.Show("กรุณาเลือกรายการสิทธิ์อย่างน้อย 1 รายการ");
                return;
            }
            #endregion Role Selected Check

            try
            {
                ProgressDialog.Show();

                List<UserInfo> userInfo = new List<UserInfo>();
                //userInfo = ws.GetUser();
                    //Get User by Role
                    foreach (DataGridViewRow dataGridViewRow in dataGridViewRole.Rows)
                    {
                        //ws = new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
                        DataGridViewCell dataGridViewCell = dataGridViewRow.Cells["CheckBoxRole"];// dataGridViewRole.Rows[dataGridViewRow.Index].Cells["CheckBoxRole"];
                        if ((bool)dataGridViewCell.EditedFormattedValue == true)
                            userInfo.AddRange(GetService().GetUser(((RoleInfo)dataGridViewRow.DataBoundItem).RoleId));

                    }
                List<UserInfo> tempU = new List<UserInfo>();
                if (radioButton4Area.Checked)
                {

                    //Filter by Area

                    foreach (UserInfo u in userInfo)
                    {
                        string area = "";
                        switch (comboBoxArea.SelectedValue.ToString())
                        {
                            case "ก0": area = "Z"; break;
                            case "ก1": area = "G"; break;
                        }

                        if (u.BranchId == null || u.BranchId.Length == 0)
                        {
                            tempU.Add(u);
                            continue;
                        }
                        if (u.BranchId.Substring(0, 1) != area)
                        {
                            tempU.Add(u);
                        }
                    }
                    foreach (UserInfo tu in tempU)
                    {
                        userInfo.Remove(tu);
                    }

                    tempU.Clear();
                }
                if (radioButton5Branch.Checked)
                {
                    //Filter by Branch 

                    foreach (UserInfo u in userInfo)
                    {
                        if (u.BranchId == null || u.BranchId.Length == 0)
                        {
                            tempU.Add(u);
                            continue;
                        }
                        if (u.BranchId.Substring(0, 4) != comboBoxBranch.SelectedValue.ToString().Substring(0, 4))
                        {
                            tempU.Add(u);
                        }

                    }
                    foreach (UserInfo tu in tempU)
                    {
                        userInfo.Remove(tu);
                    }
                    tempU.Clear();
                }
                if (radioButton2FixedRegis.Checked)
                {
                    foreach (UserInfo u in userInfo)
                    {
                        if (u.BranchId == null) continue;
                        if (u.BranchId.Length == 0)
                            tempU.Add(u);
                    }
                    foreach (UserInfo tu in tempU)
                    {
                        userInfo.Remove(tu);
                    }
                    tempU.Clear();
                }
                if (radioButton3NonFixedRegis.Checked)
                { 
                    foreach (UserInfo u in userInfo)
                    {
                        if (u.BranchId == null) continue;
                        if (u.BranchId.Length != 0)
                            tempU.Add(u);
                    }
                    foreach (UserInfo tu in tempU)
                    {
                         userInfo.Remove(tu);
                    }
                    tempU.Clear();
                }


                //broadcastTopic
                //detail
                //sentDt
                //expireDt
                //cmdId

                DateTime sentDt = new DateTime();
                DateTime expireDt = new DateTime();
                if (radioButton6SentDateNow.Checked)
                {
                    sentDt = DateTime.Now;
                }
                else
                {
                    sentDt = sentDt.AddDays(dateTimePicker1.Value.Day - 1);
                    sentDt = sentDt.AddMonths(dateTimePicker1.Value.Month - 1);
                    sentDt = sentDt.AddYears(dateTimePicker1.Value.Year - 1);
                    //Assign time
                    sentDt = sentDt.AddHours(Convert.ToInt32(maskedTextBox1.Text.Substring(0, 2)));
                    sentDt = sentDt.AddMinutes(Convert.ToInt32(maskedTextBox1.Text.Substring(3, 2)));

                }
                if (radioButton8EndDateNow.Checked)
                    expireDt = DateTime.Today.AddDays(1).AddSeconds(-1);
                else
                {
                    expireDt = expireDt.AddDays(dateTimePicker2.Value.Day - 1);
                    expireDt = expireDt.AddMonths(dateTimePicker2.Value.Month - 1);
                    expireDt = expireDt.AddYears(dateTimePicker2.Value.Year - 1);
                    //Assign time
                    expireDt = expireDt.AddHours(Convert.ToInt32(maskedTextBox2.Text.Substring(0, 2)));
                    expireDt = expireDt.AddMinutes(Convert.ToInt32(maskedTextBox2.Text.Substring(3, 2)));

                }

                //ws = new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
                GetService().InsertNewsBroadcast(textBoxTopic.Text
                                       , richTextBoxDetail.Rtf
                                       , sentDt
                                       , expireDt
                                       , 1);

                //ws = new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
                int newsBroadcastId = GetService().GetLastNewsBroadcastId();


                foreach (UserInfo user in userInfo)
                {
                    //ws = new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
                    if (radioButton5Branch.Checked)
                    {
                        if (user.BranchId.Trim() == comboBoxBranch.SelectedValue.ToString().Substring(0, 4) || user.BranchId == comboBoxBranch.SelectedValue.ToString())
                        {
                            if (!GetService().IsDuplicateUser(newsBroadcastId, user.UserId))
                            {
                                //ws = new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
                                GetService().InsertNewsBroadcastUser(newsBroadcastId
                                               , user.UserId
                                               , " "
                                               , user.BranchId
                                               , user.PostBranchServerId
                                               , " "
                                               , " ");
                            }
                        }
                    }
                    else
                    {
                        if (!GetService().IsDuplicateUser(newsBroadcastId, user.UserId))
                        {
                            //ws = new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
                            GetService().InsertNewsBroadcastUser(newsBroadcastId
                                                    , user.UserId
                                                    , " "
                                                    , user.BranchId
                                                    , user.PostBranchServerId
                                                    , " "
                                                    , " ");
                        }
                    }
                }

                //BroadcastSG ws = new BroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString()); //BroadcastWebService.BroadcastWebService ws = new BroadcastWebService.BroadcastWebService(); //BroadcastSG ws = new BroadcastSG("http://172.30.241.72/BPMNewsBroadcast/");
                //ws.InsertNewBroadcast(textBoxTopic.Text.Trim(), richTextBoxDetail.Rtf, Session.BpmDateTime.Now, _reciever, _branchId);
                // }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถเชื่อมต่อ WebServices ได้ Error:" + ex);
            }
            finally
            {
                ProgressDialog.Close();
                MessageBox.Show("ส่งข้อความเรียบร้อย");
            }
            this.Close();
        }

        #endregion

        #region Radio Button
        private void radioButton1All_CheckedChanged(object sender, EventArgs e)
        {
            //dataGridViewRole.Enabled = false;
            //_reciever = (int)RecieverInfo.All;
            comboBoxBranch.Enabled = false;
            comboBoxArea.Enabled = false;
        }

        private void radioButton2Center_CheckedChanged(object sender, EventArgs e)
        {
            //_reciever = (int)RecieverInfo.ConnectToCenter;
        }

        private void radioButton4Private_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Combobox
        private void comboBoxArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView view = new DataView(_BranchAreaDT);
            //view.RowFilter = String.Format("{0} = '{1}'", ColumnInfo.Area.AreaId, comboBoxArea.Text);

            //comboBoxBranch.DataSource = view;
            //comboBoxBranch.DisplayMember = ColumnInfo.BranchArea.BranchName;
            //comboBoxBranch.ValueMember = ColumnInfo.BranchArea.BranchId;
        }
        #endregion

        private void buttonSelectBranch_Click(object sender, EventArgs e)
        {
            SelectRecieverDialogForm selDlg = new SelectRecieverDialogForm();
            selDlg.ShowDialog();
        }

        #endregion

        private void radioButtonRole_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewRole.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxBranch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelBranch_Click(object sender, EventArgs e)
        {

        }

        private void labelArea_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                e.Cancel = true;
                MessageBox.Show("เวลาที่ส่งไม่ถูกต้อง");
            }
        }

        private void maskedTextBox2_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                e.Cancel = true;
                MessageBox.Show("เวลาสิ้นสุดการส่งไม่ถูกต้อง");
            }
        }

        private void buttonRoleSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dataGridViewRow in dataGridViewRole.Rows)
            {
                DataGridViewCell dataGridViewCell = dataGridViewRow.Cells["CheckBoxRole"];
                dataGridViewCell.Value = true;
            }
        }

        private void buttonRoleClear_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dataGridViewRow in dataGridViewRole.Rows)
            {
                DataGridViewCell dataGridViewCell = dataGridViewRow.Cells["CheckBoxRole"];
                dataGridViewCell.Value = false;
            }
        }

        private void radioButton4AllRole_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewRole.Enabled = false;
            buttonRoleSelectAll.Enabled = false;
            buttonRoleClear.Enabled = false;
        }

        private void radioButton5EachRole_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewRole.Enabled = true;
            buttonRoleSelectAll.Enabled = true;
            buttonRoleClear.Enabled = true;
        }

        private void radioButton2FixedRegis_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxArea.Enabled = false;
            comboBoxBranch.Enabled = false;
        }

        private void radioButton3NonFixedRegis_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxArea.Enabled = false;
            comboBoxBranch.Enabled = false;
        }

        private void buttonCopyMessage_Click(object sender, EventArgs e)
        {
            richTextBoxDetail.SelectAll();
            richTextBoxDetail.Copy();
            MessageBox.Show("ทำการ Copy เรียบร้อย");
        }

        private void buttonPasteMessage_Click(object sender, EventArgs e)
        {
            richTextBoxDetail.Paste();
        }

        private void radioButton4Area_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxArea.Enabled = true;
            comboBoxBranch.Enabled = false;
        }

        private void radioButton5Branch_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxBranch.Enabled = true;
            comboBoxArea.Enabled = false;
        }

        private void buttonViewSentUserList_Click(object sender, EventArgs e)
        {
            SelectRecieverDialogForm srdForm = new SelectRecieverDialogForm();
            srdForm.BroadcastId = _nbInfo.BroadcastId;
            srdForm.ShowDialog();
        }

        
        

    }
}