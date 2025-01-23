using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;
using PEA.BPM.NewsBroadcast.Interface.Constants;
using PEA.BPM.NewsBroadcast.SG;
using System.Globalization;
using System.Text.RegularExpressions;
using AdministrativeTool.Common;


namespace AdministrativeTool.NewsBroadcastSender
{
    public partial class SenderUserControl : UserControl
    {
        #region Variable
        private DateTime _bpmDateTime = Session.BpmDateTime.Now;
        private DateTimeFormatInfo _th_dt;
        private string serviceStringTemp;
        #endregion

        #region Constructor
        public SenderUserControl()
        {
            InitializeComponent();
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;

            //#region Initialize MaskedTextbox and MonthCalendar up to date
            //monthCalendarFilter.SelectionStart = _bpmDateTime.Date;
            //maskedTextBoxDateFilter.Text = _bpmDateTime.Date.ToString("dd/MM/yyyy",_th_dt);
            //#endregion

            #region Initialize Control
            radioButton2.Checked = true;
            dateTimePickerDisplayDt.Value = DateTime.Today;
            dateTimePickerSentDt.Value = DateTime.Today;
            dateTimePickerSentDt.Enabled = false;
            numericUpDownYears.Value = DateTime.Now.Year+543;
            #endregion

        }
        #endregion

        #region Event

        #region Form
        private void SenderUserControl_Load(object sender, EventArgs e)
        {
            CultureInfo culture = CultureInfo.GetCultureInfo("th-TH"); 
            comboBoxMonth.SelectedItem = DateTime.Now.ToString("MMMM",culture);
            RefreshData();
        }
        #endregion 
       
        #region Control
        private void gridViewBroadcastLog_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //try
            //{               
            //    //แสดง format เฉพาะ "เวลา" ในคอลัมน์แรก 
            //    if (e.ColumnIndex == gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.SentDt].Index)                
            //    {                    
            //        if (e.Value == null || e.Value.ToString() == "") return;                
            //        DateTime value = Convert.ToDateTime(e.Value);
            //        e.Value = value.ToString("HH:mm");                  
            //        e.FormattingApplied = true;                
            //    }            
            //}            
            //catch (Exception ex)            
            //{
            //    throw new Exception(ex.ToString());        
            //}
        }

        private void gridViewBroadcastLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex == -1)
            //    return;

            //if (gridViewBroadcastLog.SelectedRows.Count < 1) return;
            //foreach (DataGridViewRow dataGridViewRow in gridViewBroadcastLog.SelectedRows)
            //{
            //    NewsBroadcastSentInfo newsInfo = new NewsBroadcastSentInfo();
            //    try
            //    {
            //        newsInfo = (NewsBroadcastSentInfo)dataGridViewRow.DataBoundItem;
            //    }
            //    catch (Exception)
            //    {
                   
            //    }


            //    //bool tempIsRead = newsInfo.IsRead;

            //    SenderDialogForm recDlg = new SenderDialogForm(newsInfo);
            //    recDlg.ShowDialog();

            //    //if (newsInfo.IsRead != tempIsRead)
            //    //{
            //    //    this.RefreshData();
            //    //}
            //}
        }

        //private void monthCalendarFilter_DateChanged(object sender, DateRangeEventArgs e)
        //{
        //    RefreshData();
        //    maskedTextBoxDateFilter.Text = monthCalendarFilter.SelectionStart.ToString("dd/MM/yyyy",_th_dt);
        //}

        private void maskedTextBoxDateFilter_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (maskedTextBoxDateFilter.MaskCompleted)
            //    {
            //        string date = "";
            //        date += maskedTextBoxDateFilter.Text.Substring(3, 2) + "/";
            //        date += maskedTextBoxDateFilter.Text.Substring(0, 2) + "/";
            //        date += maskedTextBoxDateFilter.Text.Substring(6, 4);

            //        try
            //        {
            //            monthCalendarFilter.SelectionStart = Convert.ToDateTime(date).AddYears(-543);
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("วันที่ที่ท่านกรอก ไม่อยู่ในช่วงเวลาที่ถูกต้อง");
            //        }
            //    }
            //}
        }
        #endregion

        #region Button
        private void buttonEditService_Click(object sender, EventArgs e)
        {
            //if (buttonEditService.Text.Equals("แก้ไข"))
            //{
            //    textBoxServiceConnect.ReadOnly = false;
            //    buttonEditService.Text = "ตกลง";
            //    buttonCancel.Visible = true;
            //}
            //else //if ตกลง
            //{
            //    string serviceText = textBoxServiceConnect.Text;
            //    if (!serviceText.Substring(serviceText.Length - 1).Equals("/"))
            //        serviceText += "/";
            //    UpdateAppSettings("BPMNewsBroadcast", serviceText + "TOOL/BroadcastWCFService.svc");
            //    serviceStringTemp = serviceText;
            //    textBoxServiceConnect.ReadOnly = true;
            //    buttonEditService.Text = "แก้ไข";
            //    buttonCancel.Visible = false;
            //}
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //textBoxServiceConnect.Text = serviceStringTemp;
            //textBoxServiceConnect.ReadOnly = true;
            //buttonEditService.Text = "แก้ไข";
            //buttonCancel.Visible = false;
        }

        private void buttonNewMessage_Click(object sender, EventArgs e)
        {
            SenderDialogForm sendDlg = new SenderDialogForm();
            sendDlg.Mode = OperationMode.Add;
            sendDlg.ShowDialog();
            RefreshData();
            //SenderDialogForm sendDlg = new SenderDialogForm();
            //sendDlg.Mode = OperationMode.Add;
            //sendDlg.ShowDialog();
            //monthCalendarFilter.SelectionStart = DateTime.Today;
            //maskedTextBoxDateFilter.Text = monthCalendarFilter.SelectionStart.ToString("dd/MM/yyyy", _th_dt);
            //RefreshData();
        }
        #endregion

        #endregion

        #region Method

        private NewsBroadcastSG GetService()
        {
            return new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
        }

        public void RefreshData()
        {

            try
            {
                //BroadcastSG ws = new BroadcastSG("http://172.30.241.72/BPMNewsBroadcast/");

                string[] part = SplitWords(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
                textBoxServiceConnect.Text = part[0];
                serviceStringTemp = part[0];
            }
            catch (Exception)
            {
                throw;
            }

            //if (monthCalendarFilter.SelectionRange.Start.Year < 1500)
            //{
            //    MessageBox.Show("วันที่ที่ท่านกรอก ไม่อยู่ในช่วงเวลาที่ถูกต้อง");
            //    return;
            //}

            try
            {
                ProgressDialog.Show();
                if (radioButton1.Checked)
                {

                    dataGridViewNewsList.DataSource = GetService().GetNewsBroadcastSent(dateTimePickerSentDt.Value);
                }
                else if (radioButton2.Checked)
                {
                    dataGridViewNewsList.DataSource = GetService().GetNewsBroadcastNowForSender(dateTimePickerDisplayDt.Value, 1);
                }
                else if (radioButton3.Checked)
                {
                    int month =0;
                    switch (comboBoxMonth.Text)
                    {
                        case "มกราคม" : month = 1; break;
                        case "กุมภาพันธ์": month = 2; break;
                        case "มีนาคม": month = 3; break;
                        case "เมษายน": month = 4; break;
                        case "พฤษภาคม": month = 5; break;
                        case "มิถุนายน": month = 6; break;
                        case "กรกฎาคม": month = 7; break;
                        case "สิงหาคม": month = 8; break;
                        case "กันยายน": month = 9; break;
                        case "ตุลาคม": month = 10; break;
                        case "พฤศจิกายน": month = 11; break;
                        case "ธันวาคม": month = 12; break;
                }

                    DateTime dt = new DateTime(((int)numericUpDownYears.Value)-543, month, 1);
                    dataGridViewNewsList.DataSource = GetService().GetNewsBroadcastSentMonthYears(dt);
                }
                else
                {
                    dataGridViewNewsList.DataSource = GetService().GetNewsBroadcastScheduled(DateTime.Now, 1);
                }

                //if (radioButton1.Checked)
                //{
                //    dataGridViewNewsList.DataSource = GetService().GetNewsBroadcastNow(
                //}
                //dataGridViewNewsList.DataSource = GetService().GetNewsBroadcastSent(dateTimePickerDisplayDt.Value);
                //gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.BroadcastId].Visible = false;
                //gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.Detail].Visible = false;
                ////gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.RecieverId].Visible = false;
                ////gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.RecieverName].Visible = true;
                //gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.IsRead].Visible = false;
                //gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.BranchId].Visible = false;

                ////gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.AreaId].Visible = false;
                //gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.ReadSymbol].Visible = false;
                //gridViewBroadcastLog.Sort(gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.SentDt], ListSortDirection.Ascending);
                //gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.SentDt].HeaderText = "เวลาที่ส่ง";
                //gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.BroadcastTopic].HeaderText = "หัวข้อข่าว";
                //gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.Detail].HeaderText = "ข้อความ";
                //gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.SentDt].Width = 70;
                ////gridViewBroadcastLog.Columns[ColumnInfo.NewsBroadcast.RecieverName].HeaderText = "ผู้รับ";
            }
            catch (Exception)
            {
                //MessageBox.Show("พบปัญหาเกี่ยวกับโครงสร้างฐานข้อมูล");
            }
            finally
            {
                ProgressDialog.Close();
            }
        }

        //เขียนทับไฟล์ AppSetting
        private void UpdateAppSettings(string settingName, string settingValue)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection appSettings = config.AppSettings;
            KeyValueConfigurationElement setting = appSettings.Settings[settingName];
            setting.Value = settingValue;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private string[] SplitWords(string s)
        {
            return Regex.Split(s, @"TOOL/BroadcastWCFService.svc");
            // @      special verbatim string syntax
        }
        #endregion

        private void buttonEditWebService_Click(object sender, EventArgs e)
        {
            InputWebServiceDialog inputDlg = new InputWebServiceDialog();
            inputDlg.ShowDialog();
            RefreshData();
        }

        private void dateTimePickerDisplayDt_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                comboBoxMonth.Enabled = false;
                numericUpDownYears.Enabled = false;
                dateTimePickerDisplayDt.Enabled = false;
                dateTimePickerSentDt.Enabled = true;
                RefreshData(); //dataGridViewNewsList.DataSource = GetService().GetNewsBroadcastSent(dateTimePickerDisplayDt.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dateTimePickerSentDt_ValueChanged(object sender, EventArgs e)
        {
            RefreshData(); 
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerSentDt.Enabled = false;
            dateTimePickerDisplayDt.Enabled = false;
            comboBoxMonth.Enabled = true;
            numericUpDownYears.Enabled = true;
            RefreshData();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxMonth.Enabled = false;
            numericUpDownYears.Enabled = false;
            dateTimePickerSentDt.Enabled = false;
            dateTimePickerDisplayDt.Enabled = true;

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxMonth.Enabled = false;
            numericUpDownYears.Enabled = false;
            dateTimePickerSentDt.Enabled = false;
            dateTimePickerDisplayDt.Enabled = false;

        }

        private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void numericUpDownYears_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dataGridViewNewsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void dataGridViewNewsList_DoubleClick(object sender, EventArgs e)
        {
            NewsBroadcastSentInfo currentDataRowView = (NewsBroadcastSentInfo)dataGridViewNewsList.CurrentRow.DataBoundItem;
            //DataRow row = currentDataRowView.Row;
            
            SenderDialogForm sendDlg = new SenderDialogForm();
            sendDlg.Mode = OperationMode.View;
            sendDlg.NbInfo = currentDataRowView;
            //sendDlg.BroadcastSelectedRow = row;
            sendDlg.ShowDialog();

        }
    }
}