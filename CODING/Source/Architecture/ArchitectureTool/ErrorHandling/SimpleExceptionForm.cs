using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;

namespace PEA.BPM.Architecture.ArchitectureTool.ErrorHandling
{
    public partial class SimpleExceptionForm : Form
    {
        Exception _exception = null;

        public SimpleExceptionForm()
        {
            InitializeComponent();
        }

        public void SetException(Exception ee)
        {
            _exception = ee;

            ErrorCodeTxt.Text = "ไม่มี";
            DebuggingIDTxt.Text = "ไม่มี";
            HelpLinkIL.Text = "";

            if (ee == null) return;

            Type eetype = ee.GetType();
            if (eetype == typeof(BPMApplicationException))
            {
                BPMApplicationException bpmex = ee as BPMApplicationException;
                ErrorCodeTxt.Text = bpmex.ErrorCode.Length == 0 ? "ไม่มี" : bpmex.ErrorCode;
                DebuggingIDTxt.Text = bpmex.DebuggingId.Length == 0 ? "ไม่มี" : bpmex.DebuggingId;
                MessageTxt.Text = bpmex.Message;

                if (bpmex.THMessage.Length != 0) MessageTxt.Text = bpmex.THMessage;
                if (bpmex.Resolve.Length != 0) ResolveTxt.Text = bpmex.Resolve;

                HelpLinkIL.Text = bpmex.HelpURL;
            }
            else
            {
                MessageTxt.Text = ee.Message;
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {

        }

        private void HelpLinkIL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(HelpLinkIL.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowStackTraceForm frm = new ShowStackTraceForm();
            frm.SetException(_exception);
            frm.ShowDialog();
        }

    }
}