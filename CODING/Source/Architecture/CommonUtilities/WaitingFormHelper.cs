using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public class WaitingFormHelper
    {
        private static bool _isHide = true;
        private static WaitingForm _waitingForm = new WaitingForm();

        private static long x = long.MinValue;

        public static void ShowWaitingForm()
        {
            if (DateTime.Now.ToBinary() - x > 20)
            {
                x = DateTime.Now.ToBinary();

                if (_isHide)
                {
                    _isHide = false;
                    BackgroundWorker bgw = new BackgroundWorker();
                    bgw.DoWork +=new DoWorkEventHandler(bgw_DoWork);
                    bgw.RunWorkerAsync();
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(Show), null);
                }
            }
        }

        public static void ShowWaitingForm(Form parent)
        {
            if (DateTime.Now.ToBinary() - x > 20)
            {
                x = DateTime.Now.ToBinary();

                if (_isHide)
                {
                    _isHide = false;
                    BackgroundWorker bgw = new BackgroundWorker();
                    bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
                    bgw.RunWorkerAsync(parent);
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(Show), parent);
                }
            }
        }

        private static void bgw_DoWork(object sender, DoWorkEventArgs e)
        {   
            if (e.Argument != null)
            {
                _waitingForm.StartPosition = FormStartPosition.CenterParent;
                _waitingForm.ShowDialog((Form)e.Argument);
            }
            else
            {
                _waitingForm.StartPosition = FormStartPosition.CenterScreen;
                _waitingForm.ShowDialog();
            }                        
        }

        //private static void Show(object parent)
        //{
        //    if (parent != null)
        //    {
        //        _waitingForm.StartPosition = FormStartPosition.CenterParent;
        //        _waitingForm.ShowDialog((Form)parent);
        //    }
        //    else
        //    {
        //        _waitingForm.StartPosition = FormStartPosition.CenterScreen;
        //        _waitingForm.ShowDialog();
        //    }
        //}

        public static void HideWaitingForm()
        {
            if (!_isHide)
            {
                Thread.Sleep(50);
                //_waitingForm.Close(); // S : เปลี่ยนจาก close() เป็น hide() เพราะ close() จะ dispose form ทิ้งทำให้ show dialog อีกครั้งแล้ว error
                              
                _waitingForm.Hide();  //UNCOMMENT BEFORE DEPPLOY UTHEN.P
                if (null != _waitingForm.Owner)
                {
                    _waitingForm.Owner.Focus();
                }
                _isHide = true;
                
            }
        }
    }
}
