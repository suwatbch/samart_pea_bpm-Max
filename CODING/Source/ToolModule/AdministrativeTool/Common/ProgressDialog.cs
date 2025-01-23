using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;

namespace AdministrativeTool.Common
{
    public sealed class ProgressDialog
    {
        #region Variables
        private delegate void ProgressStatusChangedHandle(int? progressValue, string statusText);
        private static Thread progressThread;
        private static Type progressFormType = typeof(frmProgressDialog);
        private static Form progressForm;
        private static IProgressDialog progressInterface;
        private static int? _progressValue;
        private static string _statusText;
        private static ManualResetEvent _progressDialogShown = new ManualResetEvent(false);
        #endregion

        #region Properties
        public int? ProgressValue
        {
            get
            {
                // prevent CA1822
                this.GetType();

                return _progressValue;
            }
            set { _progressValue = value; }
        }

        public string StatusText
        {
            get
            {
                // prevent CA1822
                this.GetType();

                return _statusText;
            }
            set { _statusText = value; }
        }

        public static ProgressDialog UpdateProgressInformation
        {
            set
            {
                try
                {
                    if (progressInterface == null || progressForm == null)
                    {
                        _progressValue = value.ProgressValue;
                        _statusText = value.StatusText;
                        return;
                    }

                    // Wait until form has been created
                    _progressDialogShown.WaitOne();

                    // Invoke or BeginInvoke cannot be called on a control until the window handle has been created.
                    if (progressForm.InvokeRequired)
                    {
                        progressForm.Invoke(new ProgressStatusChangedHandle(delegate(int? a, string b)
                        {
                            progressInterface.SetProgressStatus(a, b);
                        }), new object[] { value.ProgressValue, value.StatusText });
                    }
                }
                catch (Exception exc)
                {
                    throw new Exception("Failed to update progress information.", exc);
                }
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProgressDialog()
        {
        }

        public ProgressDialog(int? progressValue, string statusText)
        {
            _progressValue = progressValue;
            _statusText = statusText;
        }
        #endregion

        #region Method Private Static : CreateInstance()
        /// <summary>
        /// Create Instance
        /// </summary>
        /// <param name="FormType">Form Type</param>
        private static void CreateInstance(Type FormType)
        {
            object obj = FormType.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
            progressForm = obj as Form;
            progressInterface = obj as IProgressDialog;

            if (progressForm == null)
            {
                throw (new Exception("Progress Form cannot be null."));
            }
            if (progressInterface == null)
            {
                throw (new Exception("Progress Interface cannot be null."));
            }

            progressInterface.SetProgressStatus(_progressValue, _statusText);
        }
        #endregion

        #region Method Public Static : Show()
        public static void Show()
        {
            Show(null, "Loading please wait ...", null);
        }

        public static void Show(string style)
        {
            Show(null, "Loading please wait ...", style);
        }

        /// <summary>
        /// แสดงผล Progress Dialog
        /// </summary>
        public static void Show(int? progressValue, string statusText, string style)
        {
            try
            {
                if (progressThread != null) return;
                if (progressFormType == null) return;

                progressThread = new Thread(new ThreadStart(delegate()
                {
                    CreateInstance(progressFormType);
                    progressForm.Shown += new EventHandler(progressForm_Shown);
                    progressForm.FormClosed += new FormClosedEventHandler(progressForm_FormClosed);
                    progressForm.StartPosition = FormStartPosition.CenterScreen;
                    progressForm.TopMost = true;
                    Application.Run(progressForm);
                }));

                progressThread.IsBackground = true;
                progressThread.SetApartmentState(ApartmentState.STA);
                progressThread.Start();

                UpdateProgressInformation = new ProgressDialog(progressValue, statusText);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        #endregion

        #region Method Public Static : Close()
        /// <summary>
        /// ทำการปิด Progress Dialog
        /// </summary>
        public static void Close()
        {
            try
            {
                if (progressThread == null) return;
                if (progressForm == null)
                {
                    // Wait until form has been created
                    _progressDialogShown.WaitOne();
                }

                if (progressForm.InvokeRequired)
                {
                    progressForm.Invoke(new MethodInvoker(progressForm.Close));
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                progressThread = null;
                progressForm = null;
            }
        }
        #endregion

        #region ProgressForm Events
        private static void progressForm_Shown(object sender, EventArgs e)
        {
            _progressDialogShown.Set();
            Application.DoEvents();
        }

        private static void progressForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _progressDialogShown.Reset();
            Application.DoEvents();
        }
        #endregion
    }
}