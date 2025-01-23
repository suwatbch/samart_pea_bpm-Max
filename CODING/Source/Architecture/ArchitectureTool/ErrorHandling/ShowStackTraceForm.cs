using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.Architecture.ArchitectureTool.ErrorHandling
{
    public partial class ShowStackTraceForm : Form
    {
        public ShowStackTraceForm()
        {
            InitializeComponent();
        }

        private void CopyST1Btn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(StackTrackTxt.Text);
        }

        public void SetException(Exception ee)
        {
            if (ee == null) return;

            Type eetype = ee.GetType();
            if (eetype == typeof(BPMApplicationException))
            {
                BPMApplicationException bpmex = ee as BPMApplicationException;
                StackTrackTxt.Text = bpmex.FullStackTrace;
            }
            else
            {
                StackTrackTxt.Text = ee.Message + Environment.NewLine + Environment.NewLine + ee.StackTrace;
            }
            StackTrackTxt.Select(0, 0);
        }

    }
}
