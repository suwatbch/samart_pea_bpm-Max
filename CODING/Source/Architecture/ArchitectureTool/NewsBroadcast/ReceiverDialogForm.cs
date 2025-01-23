using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;
using PEA.BPM.NewsBroadcast.SG;
using System.Globalization;
using System.Threading;

namespace PEA.BPM.Architecture.ArchitectureTool.NewsBroadcast
{
    public partial class RecieverDialogForm : Form
    {
        #region Variable
        private DateTimeFormatInfo _th_dt;
        #endregion

        #region Properties
        private NewsBroadcastInfo _newsInfo;
        public NewsBroadcastInfo NewsInfo
        {
            get { return _newsInfo; }
            set { _newsInfo = value; }
        }
        #endregion

        #region Constructor
        public RecieverDialogForm(NewsBroadcastInfo newsInfo)
        {
            _newsInfo = newsInfo;
            InitializeComponent();
        }
        #endregion

        #region Event
        #region Form
        private void RecieverDialogForm_Load(object sender, EventArgs e)
        {
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;
            labelDate.Text = String.Format("ณ วันที่: {0} เวลา: {1} น."
                                            , NewsInfo.SentDt.ToString("d MMMM yyyy ", _th_dt) 
                                            , NewsInfo.SentDt.ToString("HH:mm")
                                            );
            if (NewsInfo.IsRead)
            {
                checkBoxRead.Enabled = false;
                checkBoxRead.Checked = true;
            }
            else
            {
                checkBoxRead.Enabled = true;
                checkBoxRead.Checked = false;
            }
            textBoxTopic.Text = NewsInfo.BroadcastTopic;
            richTextBoxDetail.Rtf = NewsInfo.Detail;
        }
        #endregion

        #region Button
        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxRead.Checked)
                {
                    NewsBroadcastSG sg = new NewsBroadcastSG();
                    sg.UpdateNewsBroadcastUserRead(_newsInfo.BroadcastId, Session.User.Id);
                    NewsInfo.IsRead = true;
                }
            }
            catch
            { }
           
            this.Close();
        }
        #endregion

        #region RichTextBox
        private void richTextBoxDetail_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
        #endregion
        #endregion
    }
}