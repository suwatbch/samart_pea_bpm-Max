using System;
using System.Windows.Forms;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Infrastructure.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.Infrastructure.Layout
{
    public partial class ShellLayoutView : UserControl
    {
        private ShellLayoutViewPresenter _presenter;
        private System.Timers.Timer offlineTimer;
        private string _offlineIconStatus;
        private bool isWakeUp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShellLayoutView"/> class.
        /// </summary>
        public ShellLayoutView()
        {
            InitializeComponent();
            _centerWorkspace.Name = WorkspaceNames.CenterWorkspace;

            System.Timers.Timer t = new System.Timers.Timer();            
            t.Interval = 500;
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            t.Start();            

            offlineTimer = new System.Timers.Timer();
            offlineTimer.Interval = 1000;
            offlineTimer.Elapsed += new System.Timers.ElapsedEventHandler(offlineTimer_Elapsed);
            offlineTimer.Start();
        }

        private void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            _dateStatusLabel.Text = Session.BpmDateTime.Now.ToString("dd/MM/yyyy");
            _timeStatusLabel.Text = Session.BpmDateTime.Now.ToString("HH:mm:ss");

            //DF#112 2021-DEC-07 Uthen.P 
            //แสดงข้อความล่างซ้าย Ready --> Ready (Rev.1) เนื่องจากมีการแก้ไข BPM V204

            // Rev.2  1. แก้ไขการตัด Group invoice. // 2. รวมใบเสร็จแผนผ่อน จะยกเว้นใบเสร็จที่ TaxRate เป็นค่าว่าง
            //_statusLabel.Text = "Ready (Rev.2)"; // 2023-09-27 เตรียม Build QR Payment กลับไปแสดง 
            //_statusLabel.Text = "Ready";

            //แสดงข้อความล่างซ้าย Ready --> Ready (Rev.1) เนื่องจากมีการแก้ไข BPM V205 Report 2.7, PayIn.
            //แสดงข้อความล่างซ้าย Ready --> Ready (Rev.2) เนื่องจากมีการแก้ไข BPM V205 ใบนำฝาก clear value, Text box totalpayment readonly.


            //_statusLabel.Text = "Ready";   // กรณีปกติ

            _statusLabel.Text = "Ready (Rev.1)"; // 2024-11-22 การปรับปรุงหนี้ กรณีค้นหาซ้ำ build public 2.0.7.3 ปรับการยิง barcode ต่อเนื่อง


            //check after logging in
            if (Session.User.Id != null)
            {
                    //if (Session.IsNetworkConnectionAvailableReal)
                    //{

                    //    //trigger every module to be live again
                    //    if (Session.IsForcedOffline == false && isWakeUp == false)
                    //    {
                    //        //trigger every module to be live again
                    //        this.WakeUp();

                    //    }

                    //    if (_connectionStatusLabel.Text == "Offline")
                    //    {
                    //        _connectionStatusLabel.Text = "Online";
                    //        _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.green;
                    //        _connectionStatusLabel.Image.Tag = "green";
                    //        offlineTimer.Enabled = false;

                    //        MessageBox.Show("  ระบบสามารถเชื่อมต่อกับเซิร์ฟเวอร์แม่ข่ายได้แล้ว\n\n  กดปุ่ม OK เพื่อกลับมาใช้งานในโหมด Online ตามปกติ", "ข้อความ",
                    //                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    //    }
                    //    else
                    //    {
                    //        if (Session.NetworkLatency > 2000)
                    //        {
                    //            if (_connectionStatusLabel.Image == null || _connectionStatusLabel.Image.Tag == null || _connectionStatusLabel.Image.Tag.ToString() != "orange")
                    //            {
                    //                _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.orange;
                    //                _connectionStatusLabel.Image.Tag = "orange";
                    //            }
                    //        }
                    //        else if (Session.NetworkLatency > 1000)
                    //        {
                    //            if (_connectionStatusLabel.Image == null || _connectionStatusLabel.Image.Tag == null || _connectionStatusLabel.Image.Tag.ToString() != "yellow")
                    //            {
                    //                _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.yellow;
                    //                _connectionStatusLabel.Image.Tag = "yellow";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (_connectionStatusLabel.Image == null || _connectionStatusLabel.Image.Tag == null || _connectionStatusLabel.Image.Tag.ToString() != "green")
                    //            {
                    //                _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.green;
                    //                _connectionStatusLabel.Image.Tag = "green";
                    //            }
                    //        }

                    //    }

                    //    if (string.IsNullOrEmpty(_connectioninfoLab.Text))
                    //        _connectioninfoLab.Text = Session.Server.ConnectionInfo;
                    //}
                    //else
                    //{
                    //    if (_connectionStatusLabel.Text == "Online")
                    //    {
                    //        _connectionStatusLabel.Text = "Offline";
                    //        _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.red;
                    //        _connectionStatusLabel.Image.Tag = "red";
                    //        offlineTimer.Enabled = true;

                    //        MessageBox.Show("เกิดความผิดพลาดของเครือข่าย ระบบไม่สามารถติดต่อกับเครื่องแม่ข่ายได้\n\nในโหมดการทำงานนี้ ระบบจะอนุญาตให้ท่านสามารถใช้งาน\nได้เฉพาะ 'การรับชำระเงินแบบ Offline' เท่านั้น", "ข้อความเตือน",
                    //                  MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);



                    //    }


                    //    //trigger every module to be live again
                    //    if (Session.IsForcedOffline == false && isWakeUp == true)
                    //    {
                    //        //trigger every modules to sleep
                    //        this.Sleep();
                    //        _presenter.CloseAllViews();

                    //    }
                        
                    //}

                    //if (Session.IsForcedOffline == true && isWakeUp == true)
                    //{
                    //    //trigger every modules to sleep
                    //    this.Sleep();
                    //    _presenter.CloseAllViews();

                    //}

                if (Session.IsNetworkConnectionAvailable && Session.IsForcedOffline == false)
                {
                    if (_connectionStatusLabel.Text == "Offline")
                    {
                        _connectionStatusLabel.Text = "Online";
                        _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.green;
                        _connectionStatusLabel.Image.Tag = "green";
                        offlineTimer.Enabled = false;


                        MessageBox.Show("  ระบบสามารถเชื่อมต่อกับเซิร์ฟเวอร์แม่ข่ายได้แล้ว\n\n  กดปุ่ม OK เพื่อกลับมาใช้งานในโหมด Online ตามปกติ", "ข้อความ",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                        //trigger every module to be live again
                        this.WakeUp();
                    }
                    else
                    {
                        if (Session.NetworkLatency > 2000)
                        {
                            if (_connectionStatusLabel.Image == null || _connectionStatusLabel.Image.Tag == null || _connectionStatusLabel.Image.Tag.ToString() != "orange")
                            {
                                _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.orange;
                                _connectionStatusLabel.Image.Tag = "orange";
                            }
                        }
                        else if (Session.NetworkLatency > 1000)
                        {
                            if (_connectionStatusLabel.Image == null || _connectionStatusLabel.Image.Tag == null || _connectionStatusLabel.Image.Tag.ToString() != "yellow")
                            {
                                _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.yellow;
                                _connectionStatusLabel.Image.Tag = "yellow";
                            }
                        }
                        else
                        {
                            if (_connectionStatusLabel.Image == null || _connectionStatusLabel.Image.Tag == null || _connectionStatusLabel.Image.Tag.ToString() != "green")
                            {
                                _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.green;
                                _connectionStatusLabel.Image.Tag = "green";
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(_connectioninfoLab.Text))
                        _connectioninfoLab.Text = Session.Server.ConnectionInfo;
                }
                else
                {
                    if (_connectionStatusLabel.Text == "Online")
                    {
                        _connectionStatusLabel.Text = "Offline";
                        _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.red;
                        _connectionStatusLabel.Image.Tag = "red";
                        offlineTimer.Enabled = true;

                        if (Session.IsForcedOffline == true)
                        {
                            MessageBox.Show("ท่านกำลังเปลี่ยนวิธีการรับชำระเงินเป็นแบบ Offline"
                                            +"\nกรุณาตรวจสอบสถานะเครื่อข่ายเป็นระยะและเปลี่ยนโหมดการทำงานเป็น Online เมื่อสถานะเครือข่ายทำงานปกติ"
                                            +"\n\nในโหมดการทำงานนี้ ระบบจะอนุญาตให้ท่านสามารถใช้งาน\nได้เฉพาะ 'การรับชำระเงินแบบ Offline' เท่านั้น", "ข้อความเตือน",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                        }
                        else
                        {
                            //MessageBox.Show("เกิดความผิดพลาดของเครือข่าย ระบบไม่สามารถติดต่อกับเครื่องแม่ข่ายได้\n\nในโหมดการทำงานนี้ ระบบจะอนุญาตให้ท่านสามารถใช้งาน\nได้เฉพาะ 'การรับชำระเงินแบบ Offline' เท่านั้น", "ข้อความเตือน",
                            //           MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);


                            ////แก้ไขข้อความ เกิดความผิดพลาดของเครือข่าย 19-08-2558  
                            MessageBox.Show("ระบบไม่สามารถติดต่อกับเครื่องแม่ข่ายได้\n\nในโหมดการทำงานนี้ ระบบจะอนุญาตให้ท่านสามารถใช้งาน\nได้เฉพาะ 'การรับชำระเงินแบบ Offline' เท่านั้น", "ข้อความเตือน",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                        }
                        //trigger every modules to sleep
                        this.Sleep();
                        _presenter.CloseAllViews();
                    }
                }
            }
        }


        private void offlineTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_connectionStatusLabel.Text == "Offline")
            {
                if (_offlineIconStatus != "1")
                {
                    _offlineIconStatus = "1";
                    _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.red;
                }
                else
                {
                    _offlineIconStatus = "2";
                    _connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.gray;
                }
            }
        }

        private void Sleep()
        {
            isWakeUp = false;
            _presenter.SetOnlineStatus(false);
        }

        private void WakeUp()
        {
            isWakeUp = true;
            _presenter.SetOnlineStatus(true);
        }

        /// <summary>
        /// Sets the presenter.
        /// </summary>
        /// <value>The presenter.</value>
        [CreateNew]
        public ShellLayoutViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        /// <summary>
        /// Gets the main menu strip.
        /// </summary>
        /// <value>The main menu strip.</value>
        internal MenuStrip MainMenuStrip
        {
            get { return _mainMenuStrip; }
        }

        /// <summary>
        /// Gets the main status strip.
        /// </summary>
        /// <value>The main status strip.</value>
        internal StatusStrip MainStatusStrip
        {
            get { return _mainStatusStrip; }
        }

        /// <summary>
        /// Gets the main toolbar strip.
        /// </summary>
        /// <value>The main toolbar strip.</value>
        internal ToolStrip MainToolbarStrip
        {
            get { return _mainToolStrip; }
        }

        /// <summary>
        /// Close the application.
        /// </summary>
        private void OnFileExit(object sender, EventArgs e)
        {
            _presenter.OnFileExit();
        }


        //System.Timers.Timer _statusTimer;
        

        /// <summary>
        /// Sets the status label.
        /// </summary>
        /// <param name="text">The text.</param>
        internal void SetStatusLabel(string text)
        {
            _statusLabel.Text = text;

            //if (_statusTimer == null)
            //{
            //    _statusTimer = new System.Timers.Timer();
            //    _statusTimer.Elapsed += new System.Timers.ElapsedEventHandler(_statusTimer_Elapsed);
            //    _statusTimer.Interval = 5000;
            //}
            //_statusTimer.Stop();
            //_statusTimer.Start();
        }

        //internal void _statusTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    _statusLabel.Text = string.Empty;
        //    _statusTimer.Stop();
        //}

        internal void SetConnectionInfoLabel(string connectioninfo)
        {
            _connectioninfoLab.Text = connectioninfo;
        }

        internal void SetLogingLabel(string text)
        {
            _userStatusLabel.Text = string.Format(" {0}", text);
        }

        internal void ShowWorkId(bool enable)
        {
            workIdLabel.Text = string.Format("Work ID: {0}       ", Session.Work.Id);
            workIdLabel.Visible = false;
        }

    }
}
