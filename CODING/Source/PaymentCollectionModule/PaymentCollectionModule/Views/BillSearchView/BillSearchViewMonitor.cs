using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.IO.Ports;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using System.Globalization;
using PEA.BPM.Infrastructure.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Views;

namespace PEA.BPM.PaymentCollectionModule
{
    internal class BillSearchViewMonitor
    {
        private WorkItem _workItem;
        internal System.Windows.Forms.Timer _queueTimer;
        private IBillSearchView _b;

        public BillSearchViewMonitor(WorkItem workItem)
        {
            _workItem = workItem;

                _queueTimer = new System.Windows.Forms.Timer();
                _queueTimer.Interval = 5000;
                _queueTimer.Tick += new EventHandler(timer_Tick);
                _queueTimer.Start();

                _b = new BillSearchView();
            
            
            
        }
        public void queueTimer_Start()
        {
            _queueTimer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _queueTimer.Enabled = false;
            CheckFiles();
            _queueTimer.Enabled = true;
        }

        private void CheckFiles()
        {

            //MessageBox.Show("CenterWorkspace Count :" + _workItem.Workspaces[WorkspaceNames.CenterWorkspace].SmartParts.Count.ToString()
            //    + "\nModalWindows Count :" + _workItem.Workspaces[WorkspaceNames.ModalWindows].SmartParts.Count.ToString()
            //    + "\nLayoutWorkspace Count :" + _workItem.Workspaces[WorkspaceNames.LayoutWorkspace].SmartParts.Count.ToString()
            //    + "\nRightWorkspace Count :" + _workItem.Workspaces[WorkspaceNames.HorizontalLayout.RightWorkspace].SmartParts.Count.ToString()
            //    );
            //_bill.SearchById_from_Queue();
            if (_workItem.Workspaces[WorkspaceNames.CenterWorkspace].SmartParts.Count > 0)
            {
                if (_workItem.Workspaces[WorkspaceNames.CenterWorkspace].ActiveSmartPart.ToString() == "PEA.BPM.Infrastructure.Layout.HorizontalLayout"
                    && _workItem.Workspaces[WorkspaceNames.HorizontalLayout.LeftWorkspace].ActiveSmartPart.ToString() == "PEA.BPM.PaymentCollectionModule.BillSearchView"
                    && _workItem.Workspaces[WorkspaceNames.ModalWindows].SmartParts.Count.ToString() == "0"
                    )
                {
                    MessageBox.Show("Check Queue");
                }
            }
            
        }

        public void SearchById_from_Queue()
        {

            string queuePath = BPMPath.ConfigPath + "\\queueData";

            string fileName = string.Format("q" + ".txt");

            if (!Directory.Exists(queuePath))
            {
                Directory.CreateDirectory(queuePath);
            }
            else
            {

                //-----read file name list--------------------
                string[] fileNameList = Directory.GetFiles(queuePath, fileName);
                if (fileNameList.Length > 0)
                {
                    foreach (string line in File.ReadAllLines(@queuePath + "\\" + fileName))
                    {
                        
                        //customerIdMaskedTextBox.Text = line.Trim();
                        if (line.Trim() != "")
                        {
                            _b.SearchById();
                        }
                    }



                    //Directory.Delete(@queuePath + "\\" + fileName);
                    //foreach (string aFilename in fileNameList)
                    //{
                    //FileStream fs = new FileStream(aFilename, FileMode.Open);
                    //fs.
                    //fs.Close();
                    //customerIdMaskedTextBox.Text = "020017244151";
                    //SearchById();
                    //}
                }
            }
        }
    }
}
