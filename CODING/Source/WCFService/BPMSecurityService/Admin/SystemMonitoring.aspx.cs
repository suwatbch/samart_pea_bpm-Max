using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PEA.BPM.WebService.Security.Core;

namespace PEA.BPM.WebService.Security.Admin
{
    public partial class SystemMonitoring : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            StartUpTimeLab.Text = SystemInfo.Instance.LastStartUpTime;
            VersionLab.Text = SystemInfo.Instance.Version;

            SyncToDatbaseCB.Checked = false;
            CheckUserOfflineCB.Checked = false;
            RecordStatCB.Checked = false;
            SaveLogToDatabaseCB.Checked = false;

            if (SystemInfo.Instance.Feature.IsSyncToDatabase)
            {
                SyncToDatbaseCB.Checked = true;
                SyncToDatbaseLab.Text = string.Format("Enabled. Every {0} milliseconds.",
                    SystemInfo.Instance.Feature.SyncToDatabaseInterval);
            }
            else
            {
                SyncToDatbaseLab.Text = "Disable.";
            }

            if (SystemInfo.Instance.Feature.IsCheckUserOffline)
            {
                CheckUserOfflineCB.Checked = true;
                CheckUserOfflineLab.Text = string.Format("Enabled. Every {0} milliseconds. Offline in {1} intervals.",
                    SystemInfo.Instance.Feature.CheckUserOfflineInterval,
                    SystemInfo.Instance.Feature.CheckUserOfflineCount);
            }
            else
            {
                CheckUserOfflineLab.Text = "Disable.";
            }

            if (SystemInfo.Instance.Feature.IsRecodeStat)
            {
                RecordStatCB.Checked = true;
                RecordStatLab.Text = string.Format("Enabled. Every {0} milliseconds.",
                    SystemInfo.Instance.Feature.RecodeStatInterval);
            }
            else
            {
                RecordStatLab.Text = "Disable.";
            }

            if (SystemInfo.Instance.Feature.IsSaveLogToDatabase)
            {
                SaveLogToDatabaseCB.Checked = true;
                SaveLogToDatabaseLab.Text = "Enabled.";
            }
            else
            {
                SaveLogToDatabaseLab.Text = "Disable.";
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            SystemInfo.Instance.Feature.IsSyncToDatabase = SyncToDatbaseCB.Checked;
            SystemInfo.Instance.Feature.IsCheckUserOffline = CheckUserOfflineCB.Checked;
            SystemInfo.Instance.Feature.IsRecodeStat = RecordStatCB.Checked;
            SystemInfo.Instance.Feature.IsSaveLogToDatabase = SaveLogToDatabaseCB.Checked;
        }
    }
}
