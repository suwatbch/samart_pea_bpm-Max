using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using PEA.BPM.NewsBroadcast.SG;
using AdministrativeTool.Common;

namespace AdministrativeTool.NewsBroadcastSender
{
    public partial class InputWebServiceDialog : Form
    {

        private string serviceStringTemp;

        public InputWebServiceDialog()
        {
            InitializeComponent();
        }

        private void InputWebServiceDialog_Load(object sender, EventArgs e)
        {
            string[] part = SplitWords(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
            textBoxBPMWebService.Text = part[0];
            serviceStringTemp = part[0];
        }

        private NewsBroadcastSG GetService()
        {
            return new NewsBroadcastSG(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
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

        private void buttonAccept_Click(object sender, EventArgs e)
        {
          
            try
            {
                ProgressDialog.Show();
                string serviceText = textBoxBPMWebService.Text;
                if (!serviceText.Substring(serviceText.Length - 1).Equals("/"))
                    serviceText += "/";
                UpdateAppSettings("BPMNewsBroadcast", serviceText + "TOOL/BroadcastWCFService.svc");
                ProgressDialog.Close();
            }
            catch (Exception)
            {
                ProgressDialog.Close();
                throw;
            }



            try
            {
                ProgressDialog.Show();
                //--- Testing Connection --//
                try
                {
                    GetService().GetNewsBroadcastSent(DateTime.Today);
                }
                catch (Exception)
                {
                    // for refresh appconfig will not sent the error of connection.
                }
                try
                {
                    GetService().GetNewsBroadcastSent(DateTime.Today);
                    string[] part = SplitWords(ConfigurationManager.AppSettings["BPMNewsBroadcast"].ToString());
                    serviceStringTemp = part[0];
                }
                catch (Exception)
                {
                    throw;
                }
                ProgressDialog.Close();
                this.Close();
            }
            catch (Exception)
            {
                ProgressDialog.Close();
                MessageBox.Show("ไม่สามารถเชื่อมต่อ Web Service ได้ ...กรุณาลองใหม่");
            }
          
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ProgressDialog.Show();
                string serviceText = serviceStringTemp;
                if (!serviceText.Substring(serviceText.Length - 1).Equals("/"))
                    serviceText += "/";
                UpdateAppSettings("BPMNewsBroadcast", serviceText + "TOOL/BroadcastWCFService.svc");
                ProgressDialog.Close();
            }
            catch (Exception ex)
            {
                ProgressDialog.Close();
                throw ex;
            }
            this.Close();
        }
    }
}
