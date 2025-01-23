using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public partial class WaitingForm : Form
    {
        private static WaitingForm _instant = new WaitingForm();

        public WaitingForm()
        {
            InitializeComponent();
        }

        public static void ShowForm(Form parent)
        {
            if (null == parent)
            {
                //Pakdee Comment
                //_instant.ShowDialog();
            }
            else
            {
                if (_instant.IsDisposed)
                {
                    _instant = new WaitingForm();
                }
                //Pakdee Comment
                //_instant.ShowDialog(parent);
            }
        }

        public static void HideForm()
        {
            try
            {
                _instant.Close();
            }
            catch { }
        }
    }
}