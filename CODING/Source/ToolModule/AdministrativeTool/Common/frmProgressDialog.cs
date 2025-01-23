using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AdministrativeTool.Common
{
    public partial class frmProgressDialog : Form, IProgressDialog
    {
        #region Constrcutor
        public frmProgressDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void frmProgressDialog_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Method : SetProgressStatus()
        /// <summary>
        /// ปรับปรุงการแสดงผล
        /// </summary>
        /// <param name="progressValue">ค่าของ Progress (%)</param>
        /// <param name="statusText">ข้อความที่ต้องการแสดง</param>
        public void SetProgressStatus(int? progressValue, string statusText)
        {
            try
            {
                if (progressValue != null) progressBar1.Value = (int)progressValue;
                labStatus.Text = statusText;
            }
            catch (Exception exc)
            {
                throw new Exception("Failed to set progress status.", exc);
            }
        }
        #endregion
    }
}