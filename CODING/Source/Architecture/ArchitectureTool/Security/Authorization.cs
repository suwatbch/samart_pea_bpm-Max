using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using System.Web.Security;
using PEA.BPM.Architecture.CommonUtilities;
using System.Security.Principal;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using ArchitectureSG;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Data.SqlClient;
using PEA.BPM.Architecture.ArchitectureSG;
using PEA.BPM.Infrastructure.Interface.Constants;
using PEA.BPM.Infrastructure.Interface;
//using BPMNewsBroadcast.Reciever;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace PEA.BPM.Architecture.ArchitectureTool.Security
{
    public class Authorization
    {
        private static Authorization _instant = new Authorization();

        private Dictionary<string, AuthorizedData> _data;
        public static Form MainView;

        //////////// DCR 67-020 : StrongPassword ////////////
        public static string tempPwAuthen = "";
        public static string tempEmail = "";
        //////////// DCR 67-020 : StrongPassword ////////////

        private Authorization()
        {
            _data = new Dictionary<string, AuthorizedData>();
            //ServerConnectionMonitor.Instant.Start();  this causes a crash at start up
        }

        #region Service Factory
        public ISecurityService GetService()
        {
            return GetService(false);
        }

        public ISecurityService GetService(bool serverService)
        {
            //if (false)
            if (serverService || Session.Branch.OnlineConnection)
            {
                return SecuritySG.Instance(true);
            }
            else
            {
                return SecuritySG.Instance(false);
            }
        }

        public ICommonService GetCommonService()
        {
            return GetCommonService(false);
        }

        public ICommonService GetCommonService(bool serverService)
        {
            //if (false)
            if (serverService || Session.Branch.OnlineConnection)
            {
                return CommonSG.Instance(true);
            }
            else
            {
                return CommonSG.Instance(false);
            }
        }

        #endregion

        public static void SignalSyncup(string batchName)
        {
            ICommonService ws = _instant.GetCommonService(false); //local
            ws.SignalSyncup(batchName);
        }

        public static void SignalExport(string batchName, string branchId, string modifiedBy)
        {
            ICommonService ws = _instant.GetCommonService(true); //direct
            ws.SignalExport(batchName, branchId, modifiedBy);
        }

        public static void SignalExportBillBook(string batchName, string billBookId, string modifiedBy)
        {
            ICommonService ws = _instant.GetCommonService(true); //direct
            ws.SignalExportBillBook(batchName, billBookId, modifiedBy);
        }

        public static WorkStatus IsForcedToCloseWork(string workId)
        {
            ICommonService ws = _instant.GetCommonService(false); //local
            return ws.IsForcedToCloseWork(workId);
        }

        public static bool ConfirmPassword()
        {
            try
            {
                PasswordForm lf = new PasswordForm();

                if (lf.ShowDialog() == DialogResult.OK)
                {
                    MainView.Cursor = Cursors.AppStarting;
                    string userId = Session.User.Id;
                    Session.User.Pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(lf.Password, "SHA1");
                    string authenStatus = IsAuthenticated(userId, lf.Password);
                    MainView.Cursor = Cursors.Default;

                    if (authenStatus == "Invalid")
                    {
                        MessageBox.Show("รหัสผ่าน ไม่ถูกต้อง", "ข้อผิดพลาด",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (authenStatus == "NoCacheData" || authenStatus == "CacheExpired")
                    {
                        //MessageBox.Show("เกิดความผิดพลาดของเครือข่าย ระบบไม่สามารถตรวจสอบ\nความถูกต้องของผู้เข้าใช้ได้ โปรดติดต่อผู้ดูแลระบบ", "ข้อผิดพลาด",
                        //    MessageBoxButtons.OK, MessageBoxIcon.Error);

                        //แก้ไขข้อความ เกิดความผิดพลาดของเครือข่าย 19-08-2558 
                        MessageBox.Show("ระบบไม่สามารถตรวจสอบ\nความถูกต้องของผู้เข้าใช้ได้ โปรดติดต่อผู้ดูแลระบบ", "ข้อผิดพลาด",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (authenStatus == "SQLLoginFailed")
                    {
                        MessageBox.Show("ชื่อหรือรหัสผ่านในการเข้าใช้ฐานข้อมูลไม่ถูกต้อง โปรดติดต่อผู้ดูแลระบบ", "ข้อผิดพลาด",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (authenStatus == "OfflineMode")
                    {
                        //MessageBox.Show("เกิดความผิดพลาดของเครือข่าย ระบบไม่สามารถติดต่อกับเครื่องแม่ข่ายได้\n\nในโหมดการทำงานนี้ ระบบจะอนุญาตให้ท่านสามารถใช้งาน\nได้เฉพาะ 'การรับชำระเงินแบบ Offline' เท่านั้น", "ข้อความเตือน",
                        //    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        //แก้ไขข้อความ เกิดความผิดพลาดของเครือข่าย 19-08-2558
                        MessageBox.Show("ระบบไม่สามารถติดต่อกับเครื่องแม่ข่ายได้\n\nในโหมดการทำงานนี้ ระบบจะอนุญาตให้ท่านสามารถใช้งาน\nได้เฉพาะ 'การรับชำระเงิน Mode Offline' เท่านั้น", "ข้อความเตือน",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                    else
                    {
                        //UserProfile up = LoadUserProfile(userId);
                        //Session.User.Name = up.Name;

                        //if (Session.Branch.OnlineConnection && Session.IsNetworkAvailable)
                        //{
                        //    LoadServerToken();
                        //}
                        //else
                        //{
                        //    Session.User.Token = null;
                        //}

                        //Session.BpmDateTime.Now = up.LoginTime.Value;

                        //// Load Authorization data
                        //IsAuthorized("", false);
                        return true;
                    }

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError("Security:" + AppDomain.CurrentDomain.FriendlyName, "Loging", ex.ToString());

                MessageBox.Show("ไม่สามารถตรวจสอบความถูกต้องของผู้เข้าใช้งานระบบได้\nเหตุผล: " + ex.ToString(), "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool Login(string userId, string password)
        {
            //LocalSettingHelper.Instance().Add("BPMDBIP", null);
            string authenStatus = null;
            try
            {
                if (Session.Branch.Id == null)
                {
                    UpdateRegisterationInfo("DUMMY");
                    authenStatus = IsAuthenticated(userId, password);
                }
                else
                {
                    UpdateRegisterationInfo(Session.Branch.Id);
                    authenStatus = IsAuthenticated(userId, password, Session.Branch.Id);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError("Security:" + AppDomain.CurrentDomain.FriendlyName, "Loging", ex.ToString());

                MessageBox.Show("ไม่สามารถตรวจสอบความถูกต้องของผู้เข้าใช้งานระบบได้\nเหตุผล: " + ex.ToString(), "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                //////////// DCR 67-020 : StrongPassword ////////////
                if (authenStatus == "Valid")
                {
                    /////////////// service reset Invalid password countFlag ///////////////
                    string resetCountFlag = _instant.GetCommonService().ISOCheckPasswordExpried(userId, 5);
                    ////////////////////////////////////////////////////////

                    string checkPassExpried = _instant.GetCommonService().ISOCheckPasswordExpried(userId, 1);
                    if (checkPassExpried == "")
                    {
                        MessageBox.Show("รหัสผ่านหมดอายุ\nกรุณาเปลี่ยนรหัสผ่านใหม่", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        UserProfileExt up = LoadUserProfile(userId);
                        Session.User.Name = up.UserProfile.Name;
                        ChangePwdForm ChangePwdForm = new ChangePwdForm(userId, up.UserProfile.Name);
                        ChangePwdForm.ShowDialog();

                        return false;
                    }

                    string checkPassExpried_Noti = _instant.GetCommonService().ISOCheckPasswordExpried(userId, 2);
                    if (checkPassExpried_Noti != "" && checkPassExpried_Noti != null)
                    {
                        if (Int32.Parse(checkPassExpried_Noti) > 0)
                        {
                            MessageBox.Show("รหัสผ่านของท่านจะหมดอายุในอีก " + checkPassExpried_Noti + " วัน", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                //////////// DCR 67-020 : StrongPassword ////////////
            }
            catch (Exception ex)
            {

            }

            try
            {
                if (authenStatus == "Invalid")
                {
                    /////////////// service check countFlag and Time ///////
                    string checkLockFlag = _instant.GetCommonService().ISOCheckPasswordExpried(userId, 3);
                    if (checkLockFlag != "" && checkLockFlag != null)
                    {
                        if (Int32.Parse(checkLockFlag) > 0)
                        {
                            int minutes = Int32.Parse(checkLockFlag) / 60;
                            int remainingSeconds = Int32.Parse(checkLockFlag) % 60;

                            string timeInMinutesSeconds = minutes.ToString() + "." + remainingSeconds.ToString();

                            MessageBox.Show("คุณใส่รหัสผ่านผิดเกินจำนวนที่กำหนด กรุณาลองใหม่อีกครั้งใน " + timeInMinutesSeconds + " นาที หรือติดต่อผู้ดูแลระบบ", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    ////////////////////////////////////////////////////////

                    /////////////// service update countFlag ///////////////
                    string updateCountFlag = _instant.GetCommonService().ISOCheckPasswordExpried(userId, 4);
                    ////////////////////////////////////////////////////////
                }
            }
            catch (Exception ex)
            {
            }

            try
            {
                if (authenStatus == "Invalid")
                {
                    MessageBox.Show("เลขประจำตัว หรือ รหัสผ่าน ไม่ถูกต้อง", "ข้อผิดพลาด",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (authenStatus == "NoCacheData" || authenStatus == "CacheExpired")
                {
                    //MessageBox.Show("เกิดความผิดพลาดของเครือข่าย ระบบไม่สามารถตรวจสอบ\nความถูกต้องของผู้เข้าใช้ได้ โปรดติดต่อผู้ดูแลระบบ", "ข้อผิดพลาด",
                    //    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //แก้ไขข้อความ เกิดความผิดพลาดของเครือข่าย 19-08-2558
                    MessageBox.Show("ระบบไม่สามารถตรวจสอบ\nความถูกต้องของผู้เข้าใช้ได้ โปรดติดต่อผู้ดูแลระบบ", "ข้อผิดพลาด",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (authenStatus == "SQLLoginFailed")
                {
                    MessageBox.Show("ชื่อหรือรหัสผ่านในการเข้าใช้ฐานข้อมูลไม่ถูกต้อง โปรดติดต่อผู้ดูแลระบบ", "ข้อผิดพลาด",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (authenStatus == "InvalidBranch")
                {
                    MessageBox.Show("พนักงานไม่ได้สังกัดการไฟฟ้าที่ลงทะเบียน \nกรุณาใช้เลขประจำตัวของสาขานี้เท่านั้น", "ไม่อนุญาติ",
                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                else if (authenStatus == "OfflineMode")
                {
                    //MessageBox.Show("เกิดความผิดพลาดของเครือข่าย ระบบไม่สามารถติดต่อกับเครื่องแม่ข่ายได้\n\nในโหมดการทำงานนี้ ระบบจะอนุญาตให้ท่านสามารถใช้งาน\nได้เฉพาะ 'การรับชำระเงินแบบ Offline' เท่านั้น", "ข้อความเตือน",
                    //    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    Session.User.Id = userId;
                    Session.User.Pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                    UserProfileExt up = LoadUserProfileOffline(userId);
                    if (up != null)
                    {
                        Session.User.Name = up.UserProfile.Name;
                        Session.BpmDateTime.Now = up.UserProfile.LoginTime.Value;
                        Session.User.ScopeId = up.ScopeId;
                    }

                    //start heart beat
                    Session.Server.ConnectionInfo = null;
                    ServerConnectionMonitor.Instant.Start();

                    //start news broadcast heart beat
#if !DEBUG
                    NewsBroadcastConnectionMonitor.Instant.Start();
#endif

                    return true;
                }
                else
                {
                    //if (Session.Branch.OnlineConnection && Session.IsNetworkConnectionAvailable)
                    //{
                    //    LoadServerToken(true);
                    //}
                    //else
                    //{
                    //    LoadServerToken(false);
                    //}

                    //to match with web service version
                    /* Heart Add Code */
                    Session.User.Token.Center = null;
                    Session.User.Token.Branch = null;
                    /*---------------- */

                    Session.User.Id = userId;
                    Session.User.Pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                    UserProfileExt up = LoadUserProfile(userId);
                    Session.User.Name = up.UserProfile.Name;
                    Session.BpmDateTime.Now = up.UserProfile.LoginTime.Value;
                    Session.User.ScopeId = up.ScopeId;

                    #region Set Client Time & Time Zone
                    try
                    {
                        //--Set Timezone By OS--
                        Time.SetToBangkokTimeZone();
                    }
                    catch { }
                    try
                    {
                        //--Set Date Time--XP,Vista,7
                        Time.SetClientTimeBySessionTime();
                        //System.Diagnostics.EventLog appLog = new System.Diagnostics.EventLog();
                        //appLog.Source = "BPM Client";
                        //appLog.WriteEntry("Set client time correct.");
                    }
                    catch { }
                    #endregion

                    // Load Authorization data
                    IsAuthorized("", false);

                    //start heart beat
                    Session.Server.ConnectionInfo = null;
                    ServerConnectionMonitor.Instant.Start();

                    //start news broadcast heart beat
#if !DEBUG
                    NewsBroadcastConnectionMonitor.Instant.Start();
#endif

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError("Security:" + AppDomain.CurrentDomain.FriendlyName, "Loging", ex.ToString());

                MessageBox.Show("ไม่สามารถตรวจสอบความถูกต้องของผู้เข้าใช้งานระบบได้\nเหตุผล: " + ex.ToString(), "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //////////// DCR 67-020 : StrongPassword ////////////
        public static int UpdateUser(string userId, string password, int PwdState, string oldpassword)
        {
            try
            {
                var checkPassExpried = _instant.GetCommonService().UpdateUser(userId, password, PwdState, oldpassword);
                return checkPassExpried;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถเปลี่ยนรหัสผ่านได้\nเหตุผล: " + ex.ToString(), "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
        //////////// DCR 67-020 : StrongPassword ////////////

        static void Instant_BPMVersionChecked(object sender, EventArgs e)
        {

        }

        public static void UpdateRegisterationInfo(string branchId)
        {
            try
            {
                //read registeration from file (branchId)
                LocalSettingHelper local = LocalSettingHelper.Instance();
                //string branchId = local.Read("BranchId") == null ? String.Empty : local.Read("BranchId").ToString();
                string branchLevel = local.Read("BranchLevel") == null ? String.Empty : local.Read("BranchLevel").ToString();
                string branchName = local.Read("BranchName") == null ? String.Empty : local.Read("BranchName").ToString();
                string branchName2 = local.Read("BranchName2") == null ? String.Empty : local.Read("BranchName2").ToString();
                string branchAddress = local.Read("BranchAddress") == null ? String.Empty : local.Read("BranchAddress").ToString();
                string branchNo = local.Read("BranchNo") == null ? String.Empty : local.Read("BranchNo").ToString();
                string baCode = local.Read("BACode") == null ? String.Empty : local.Read("BACode").ToString();
                string taxCode = local.Read("TaxId") == null ? String.Empty : local.Read("TaxId").ToString();
                string backupUri = local.Read("BackupUri") == null ? String.Empty : local.Read("BackupUri").ToString();
                string transUri = local.Read("CenterServerWsAddress") == null ? String.Empty : local.Read("CenterServerWsAddress").ToString();
                string reportUri = local.Read("ReportServerWsAddress") == null ? String.Empty : local.Read("ReportServerWsAddress").ToString();

                RegisterProfile reg = _instant.GetCommonService().LoadRegisterationInfo(branchId);
                if (reg != null)
                {
                    //update back to the BPM config file.
                    //local.Update("BranchId", reg.BranchId);
                    if (!string.Equals(reg.BranchLevel, branchLevel)) local.Update("BranchLevel", reg.BranchLevel);
                    if (!string.Equals(reg.BranchName, branchName)) local.Update("BranchName", reg.BranchName);
                    if (!string.Equals(reg.BranchName2, branchName2)) local.Update("BranchName2", reg.BranchName2);
                    if (!string.Equals(reg.Address, branchAddress)) local.Update("BranchAddress", reg.Address);
                    if (!string.Equals(reg.BranchNo, branchNo)) local.Update("BranchNo", reg.BranchNo);
                    if (!string.Equals(reg.BusinessArea, baCode)) local.Update("BACode", reg.BusinessArea);
                    if (!string.Equals(reg.PeaTaxCode, taxCode)) local.Update("TaxId", reg.PeaTaxCode);
                    if (!string.Equals(reg.BackupUri, backupUri) && !string.IsNullOrEmpty(reg.BackupUri)) local.Add("BackupUri", reg.BackupUri);
                    if (!string.Equals(reg.TransUri, transUri) && !string.IsNullOrEmpty(reg.TransUri)) local.Add("CenterServerWsAddress", reg.TransUri);
                    if (!string.Equals(reg.ReportUri, reportUri) && !string.IsNullOrEmpty(reg.ReportUri)) local.Add("ReportServerWsAddress", reg.ReportUri);
                }
            }
            catch (Exception)
            {
                //in offline mode, it will get here 
            }
        }

        public static bool LogOut(string userId, string password)
        {
            DialogResult dlg = MessageBox.Show("คุณต้องการลงทะเบียนออกและปิดโปรแกรมใช่หรือไม่\nกรุณากด OK เพื่อยืนยัน", "ออกจากระบบ",
                                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg == DialogResult.OK)
            {


            }

            return true;
        }

        public static bool IsAuthorized(string functionId)
        {
            return IsAuthorized(functionId, true);
        }

        public static bool IsAuthorized(string functionId, bool showUnAuthorizedMsg)
        {
            return IsAuthorized(functionId, null, showUnAuthorizedMsg);
        }

        public static bool IsAuthorized(string functionId, bool showUnAuthorizedMsg, string caption)
        {
            return IsAuthorized(functionId, null, showUnAuthorizedMsg, caption);
        }

        public static bool IsAuthorized(string functionId, string description, bool showUnAuthorizedMsg)
        {
            return IsAuthorized(functionId, description, showUnAuthorizedMsg, null);
        }

        public static bool IsAuthorized(string functionId, string description, bool showUnAuthorizedMsg,
            string caption)
        {
            string remark;

            return IsAuthorized(functionId, description, showUnAuthorizedMsg, caption, out remark);
        }

        public static bool IsAuthorized(string functionId, string description, bool showUnAuthorizedMsg,
            string caption, out string remark)
        {
            string userId = Session.User.Id;
            return _instant.IsAuthorized(functionId, description, showUnAuthorizedMsg, caption, userId, out remark);
        }

        public static void LoadServerToken(bool centerToken)
        {
            if (centerToken)
            {
                Session.User.Token.Center = _instant.GetCommonService(true).GetToken(Session.User.Id, Session.User.Pwd);
            }
            else
            {
                Session.User.Token.Branch = _instant.GetCommonService(false).GetToken(Session.User.Id, Session.User.Pwd);
            }
        }

        //////////// DCR 67-020 : StrongPassword ////////////
        public static string checkISO(string pwdText, string UserId, string UserName)
        {
            var ISO_MAX_LENGTH = "10";
            try
            {
                ISO_MAX_LENGTH = CodeTable.Instant.GetAppSettingValue("ISO_MAX_LENGTH");
            }
            catch (Exception ex)
            {
            }

            // รหัสผ่านต้องมีอย่างน้อย ISO_MAX_LENGTH ตัวอักษร
            if (pwdText.Length < Int32.Parse(ISO_MAX_LENGTH))
                return "รหัสผ่านใหม่ต้องมีจำนวนอย่างน้อย " + Int32.Parse(ISO_MAX_LENGTH) + " ตัวอักษร";

            // ต้องมีอักษรตัวพิมพ์ใหญ่
            if (!Regex.IsMatch(pwdText, @"[A-Z]"))
                return "รหัสผ่านใหม่ต้องมีอักษรตัวพิมพ์ใหญ่";

            // ต้องมีอักษรตัวพิมพ์เล็ก
            if (!Regex.IsMatch(pwdText, @"[a-z]"))
                return "รหัสผ่านใหม่ต้องมีอักษรตัวพิมพ์เล็ก";

            // ต้องมีตัวเลข
            if (!Regex.IsMatch(pwdText, @"[0-9]"))
                return "รหัสผ่านใหม่ต้องมีตัวเลข";

            // ต้องมีอักขระพิเศษ
            if (!Regex.IsMatch(pwdText, @"[\W_]"))
                return "รหัสผ่านใหม่ต้องมีอักขระพิเศษ";

            // ตรวจสอบว่ารหัสผ่านมี UserId หรือ UserName อยู่หรือไม่
            if (pwdText.Contains(UserId) || pwdText.Contains(UserName.Split(' ')[0])) // ใช้แค่ชื่อ
            {
                return "ห้ามมีชื่อหรือรหัสพนักงานอยู่ในรหัสผ่าน";
            }

            // ตรวจสอบว่ารหัสซ้ำหรือไม่
            string userId = UserId;
            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwdText, "SHA1");
            if (userId.Length < 8)
            {
                // แปลง UserID ให้เป็นเลข 8 หลัก
                userId = Session.User.Id.PadLeft(8, '0');
            }
            string ISOCheckPasswordHistory = _instant.GetCommonService().ISOCheckPasswordHistory(userId, pwd);
            if (ISOCheckPasswordHistory != "")
            {
                return "ห้ามตั้งรหัสผ่านใหม่ซ้ำกับรหัสผ่านเดิม";
            }

            // ต้องมีอักขระพิเศษอย่างน้อย 2 ตัว
            //if (Regex.Matches(pwdText, @"[\W_]").Count < 2)
            //    return "รหัสผ่านใหม่ต้องมีอักขระพิเศษอย่างน้อย 2 ตัว";

            return "SUCCESS";
        }

        public static string GetPasswordStrength(string pwdText)
        {
            int score = 0;
            var max = "10";
            try
            {
                max = CodeTable.Instant.GetAppSettingValue("ISO_MAX_LENGTH");
            }
            catch (Exception ex)
            {
            }

            if (pwdText.Length >= Int32.Parse(max))
                score++;
            if (Regex.IsMatch(pwdText, @"[A-Z]"))
                score++;
            if (Regex.IsMatch(pwdText, @"[a-z]"))
                score++;
            if (Regex.IsMatch(pwdText, @"[0-9]"))
                score++;
            if (Regex.IsMatch(pwdText, @"[\W_]"))
                score++;

            switch (score)
            {
                case 5:
                    return "Strong";
                case 4:
                    return "Good";
                case 3:
                    return "Medium";
                default:
                    return "Weak";
            }
        }

        public static string GetPasswordAuthenticated(string userId)
        {
            Random random = new Random();
            tempPwAuthen = random.Next(1, 9999).ToString("D4");

            var PeaEmpDataURL = "";
            int PeaEmpDataTimeout = 0;
            try
            {
                PeaEmpDataURL = CodeTable.Instant.GetAppSettingValue("PEA_SERVICE_GATEWAY");
                PeaEmpDataTimeout = Int32.Parse(CodeTable.Instant.GetAppSettingValue("PEA_SERVICE_TIMEOUT"));
            }
            catch (Exception)
            {
                return "ไม่พบข้อมูล กรุณาติดต่อ Admin";
            }

            // call service get Email
            string EmpGetEmail = "";
            try
            {
                EmpGetEmail = _instant.GetCommonService().EmpGetEmail(userId, PeaEmpDataURL, PeaEmpDataTimeout);
                // No email found
                if (EmpGetEmail == "No email found")
                {
                    if (CodeTable.Instant.GetAppSettingValue("TEST_EMAIL") != null || CodeTable.Instant.GetAppSettingValue("TEST_EMAIL") != "")
                    {
                        EmpGetEmail = CodeTable.Instant.GetAppSettingValue("TEST_EMAIL");
                    }
                    else
                    {
                        return "ไม่พบข้อมูล Email กรุณาติดต่อ Admin";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่พบข้อมูล Email กรุณาติดต่อ Admin", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "ไม่พบข้อมูล Email กรุณาติดต่อ Admin";
            }

            try
            {
                if (EmpGetEmail != "No email found")
                {
                    var userEmail = CodeTable.Instant.GetAppSettingValue("PEA_EMAIL_USER");
                    var passEmail = CodeTable.Instant.GetAppSettingValue("PEA_EMAIL_PASS");

                    string EmpSendEmail = _instant.GetCommonService().SendEmail(EmpGetEmail, tempPwAuthen, userEmail, passEmail);
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถส่ง Email ได้ กรุณาติดต่อ Admin", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "ไม่สามารถส่ง Email ได้ กรุณาติดต่อ Admin";
            }

            return "";
        }

        public static bool CheckPasswordAuthenticated(string pw4)
        {
            bool checkPW4 = false;
            if (pw4 == tempPwAuthen && pw4 != "")
            {
                checkPW4 = true;
                tempPwAuthen = "";
            }
            return checkPW4;
        }
        //////////// DCR 67-020 : StrongPassword ////////////

        private static string IsAuthenticated(string userId, string password)
        {
            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            try
            {
                string authenStatus = _instant.GetCommonService().IsAuthenticated(userId, pwd, MachineInfo.GetLocalIP());

                if (authenStatus != "Invalid")
                {

                    CacheData<string> c = new CacheData<string>(pwd, DateTime.Now);
                    LocalSettingHelper.Instance().Add(GetAuthenKeyName(userId), c);
                };

                return authenStatus;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 18456) // SQL Server Login Failed
                {
                    return "SQLLoginFailed";
                }
                else // 53 Cannot Connect
                {
                    return LoadCacheData(userId);
                }
            }
            catch (ApplicationException ex)
            {
                if (ex.ToString().IndexOf("BPM_SERVICE_020") >= -1)
                {
                    return LoadCacheData(userId);
                }
                else
                {
                    throw;
                }
            }
        }

        private static string IsAuthenticated(string userId, string password, string regBranchId)
        {
            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            try
            {
                string authenStatus = _instant.GetCommonService().IsAuthenticated(userId, pwd, MachineInfo.GetLocalIP(), regBranchId);

                if (authenStatus != "Invalid")
                {

                    CacheData<string> c = new CacheData<string>(pwd, DateTime.Now);
                    LocalSettingHelper.Instance().Add(GetAuthenKeyName(userId), c);

                    //TAG 2.0.6 QA Update VersionNo.
                    try
                    {
                        LocalSettingHelper local = LocalSettingHelper.Instance();
                        string TerminalId = local.Read("PosId") == null ? String.Empty : local.Read("PosId").ToString();
                        string TerminalCode = local.Read("PosNo") == null ? String.Empty : local.Read("PosNo").ToString();

                        _instant.GetCommonService().UpdateBPMClientVersion("2.0.7.3", TerminalCode,userId);
                    }
                    catch (Exception)
                    {
                    }
                };

                return authenStatus;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 18456) // SQL Server Login Failed
                {
                    return "SQLLoginFailed";
                }
                else // 53 Cannot Connect
                {
                    return LoadCacheData(userId);
                }
            }
            //catch (ApplicationException ex)
            //{
            //    if (ex.ToString().IndexOf("BPM_SERVICE_020") >= -1)
            //    {
            //        return LoadCacheData(userId);
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            catch
            {
                return LoadCacheData(userId);
            }
        }

        private static string LoadCacheData(string userId)
        {
            object o = LocalSettingHelper.Instance().Read(GetAuthenKeyName(userId));
            if (null != o)
            {
                CacheData<string> c = (CacheData<string>)o;
                // ตรวจสอบว่า ข้อมูลเกิน 30 วันหรือไม่ ถ้าเกินแปลว่า expired
                if (c.IsExpired(DateTime.Now, 30))
                {
                    LocalSettingHelper.Instance().Remove(GetAuthenKeyName(userId));
                    LocalSettingHelper.Instance().Remove(GetProfileKeyName(userId));
                    LocalSettingHelper.Instance().Remove(GetScopeKeyName(userId));
                    LocalSettingHelper.Instance().Remove(GetAuthorizeKeyName(userId));
                    return "CacheExpired";
                }
                else // ถ้าไม่เกิน ให้ใช้งานในสถานะของ Offline Network
                {
                    Session.IsNetworkConnectionAvailable = false;
                    return "OfflineMode";
                }
            }

            return "NoCacheData";
        }

        private static UserProfileExt LoadUserProfileOffline(string userId)
        {
            try
            {
                object o = LocalSettingHelper.Instance().Read(GetProfileKeyName(userId));
                object s = LocalSettingHelper.Instance().Read(GetScopeKeyName(userId));
                if (null != o)
                {
                    UserProfileExt upExt = new UserProfileExt();
                    if (null != s) upExt.ScopeId = (string)s;

                    UserProfile up = (UserProfile)o;
                    up.LoginTime = DateTime.Now;
                    upExt.UserProfile = up;

                    return upExt;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private static UserProfileExt LoadUserProfile(string userId)
        {
            try
            {
                UserProfileExt up = _instant.GetService().LoadUserProfile(userId);

                LocalSettingHelper.Instance().Add(GetProfileKeyName(userId), up.UserProfile);
                LocalSettingHelper.Instance().Add(GetScopeKeyName(userId), up.ScopeId);

                return up;
            }
            catch
            {
                object o = LocalSettingHelper.Instance().Read(GetProfileKeyName(userId));
                object s = LocalSettingHelper.Instance().Read(GetScopeKeyName(userId));
                if (null != o)
                {
                    UserProfileExt upExt = new UserProfileExt();
                    if (null != s) upExt.ScopeId = (string)s;

                    UserProfile up = (UserProfile)o;
                    up.LoginTime = DateTime.Now;
                    upExt.UserProfile = up;

                    return upExt;
                }

                return null;
            }
        }

        private static string GetAuthenKeyName(string userId)
        {
            return string.Format("T-{0}", userId);
        }

        private static string GetProfileKeyName(string userId)
        {
            return string.Format("P-{0}", userId);
        }

        private static string GetAuthorizeKeyName(string userId)
        {
            return string.Format("Z-{0}", userId);
        }

        private static string GetScopeKeyName(string userId)
        {
            return string.Format("S-{0}", userId);
        }

        private bool IsAuthorized(string functionId, string description, bool showUnAuthorizedMsg,
            string caption, string userId, out string remark)
        {
            AuthorizedData azData = LoadAuthorizedData(userId);
            Function function = null;
            remark = null;

            if (null != azData)
            {
                function = azData.Data.Find(delegate(Function f) { return f.Id == functionId; });
            }

            // Check Unlock function
            if (function != null)
            {
                if (function.Authorized)
                #region function.Authorized
                {
                    if (function.UnlockRemark)
                    #region function.UnlockRemark
                    {
                        // Show form with remark Input
                        List<UnlockRemark> remarks = CodeTable.Instant.ListUnlockRemark(functionId);
                        UnlockRemarkForm ulf = new UnlockRemarkForm(remarks);
                        if (caption != null)
                        {
                            ulf.Caption = caption;
                        }

                        if (ulf.ShowDialog() == DialogResult.OK)
                        {
                            UnlockLog log = new UnlockLog();
                            log.FunctionId = functionId;
                            log.BranchId = Session.Branch.Id;
                            log.BranchName = Session.Branch.Name;
                            log.CurrentUserId = userId;
                            log.UnlockUserId = userId;
                            log.Description = description;
                            log.Remark = ulf.Remark;
                            remark = log.Remark;

                            GetService().SaveUnlockLog(null, log);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    #endregion
                    else
                    #region not function.UnlockRemark
                    {
                        //UnlockLog log = new UnlockLog();
                        //log.FunctionId = functionId;
                        //log.BranchId = Session.Branch.Id;
                        //log.CurrentUserId = userId;
                        //log.UnlockUserId = userId;
                        //log.Description = description;
                        //log.Remark = "-";

                        //GetService().SaveUnlockLog(null, log);

                        return true;
                    }
                    #endregion
                }
                #endregion
                else
                #region not function.Authorized
                {
                    if (function.Unlockable)
                    {
                        if (function.UnlockRemark)
                        #region function.UnlockRemark
                        {
                            // Show form with user&pwd and remark input
                            List<UnlockRemark> remarks = CodeTable.Instant.ListUnlockRemark(functionId);
                            UnlockForm ulf = new UnlockForm(remarks);
                            if (caption != null)
                            {
                                ulf.Caption = caption;
                            }

                            if (ulf.ShowDialog() == DialogResult.OK)
                            {

                                string retAuth = IsAuthenticated(ulf.UserId, ulf.Password);
                                //if (IsAuthenticated(ulf.UserId, ulf.Password)!=string.Empty)
                                if (retAuth != string.Empty && retAuth.ToUpper() != "Invalid".ToUpper())
                                {
                                    azData = LoadAuthorizedData(ulf.UserId);
                                    if (null != azData)
                                    {
                                        function = azData.Data.Find(delegate(Function f)
                                        {
                                            return f.Id == functionId;
                                        });
                                        if (null != function && function.Authorized)
                                        {
                                            UnlockLog log = new UnlockLog();
                                            log.FunctionId = functionId;
                                            log.BranchId = Session.Branch.Id;
                                            log.BranchName = Session.Branch.Name;
                                            log.CurrentUserId = userId;
                                            log.UnlockUserId = ulf.UserId;
                                            log.Description = description;
                                            log.Remark = ulf.Remark;
                                            remark = log.Remark;

                                            GetService().SaveUnlockLog(null, log);
                                            return true;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        #endregion
                        else
                        #region not function.UnlockRemark
                        {
                            // Show form with user&pwd input
                            UnlockLoginForm lf = new UnlockLoginForm();
                            if (caption != null)
                            {
                                lf.Caption = caption;
                            }

                            if (lf.ShowDialog() == DialogResult.OK)
                            {
                                string retAuth = IsAuthenticated(lf.UserId, lf.Password);
                                //if (IsAuthenticated(lf.UserId, lf.Password)!=string.Empty)
                                if (retAuth != string.Empty && retAuth.ToUpper() != "Invalid".ToUpper())
                                {
                                    azData = LoadAuthorizedData(lf.UserId);

                                    if (null != azData)
                                    {
                                        function = azData.Data.Find(delegate(Function f)
                                        {
                                            return f.Id == functionId;
                                        });
                                        if (null != function && function.Authorized)
                                        {
                                            UnlockLog log = new UnlockLog();
                                            log.FunctionId = functionId;
                                            log.BranchId = Session.Branch.Id;
                                            log.BranchName = Session.Branch.Name;
                                            log.CurrentUserId = userId;
                                            log.UnlockUserId = lf.UserId;
                                            log.Description = description;

                                            GetService().SaveUnlockLog(null, log);
                                            return true;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        #endregion
                    } //-- end if (function.Unlockable)
                }
                #endregion
            } //-- end if (function != null)

            if (showUnAuthorizedMsg)
            {
                MessageBox.Show("ท่านไม่มีสิทธิ์ใช้งานฟังก์ชันนี้ โปรดติดต่อผู้ดูแลระบบ", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private AuthorizedData LoadAuthorizedData(string userId)
        {
            AuthorizedData azData = null;
            DateTime lastUpdatedDate = new DateTime(2000, 1, 1);

            try
            {
                if (!_data.ContainsKey(userId))
                {
                    if (Session.IsNetworkConnectionAvailable)
                        azData = GetAuthorizedData(userId);
                    else
                        throw new Exception();
                }
                else
                {
                    azData = _data[userId];
                    TimeSpan tsp = DateTime.Now.Subtract(azData.LoadTime);
                    if (Session.IsNetworkConnectionAvailable && tsp.TotalMinutes > 30)
                    {
                        _data.Remove(userId);
                        azData = GetAuthorizedData(userId);
                    }
                }

                LocalSettingHelper.Instance().Add(GetAuthorizeKeyName(userId), azData);
            }
            catch
            {
                object o = LocalSettingHelper.Instance().Read(GetAuthorizeKeyName(userId));
                if (null != o)
                {
                    azData = (AuthorizedData)o;
                }
            }

            return azData;
        }


        private AuthorizedData GetAuthorizedData(string userId)
        {
            AuthorizedData azData = null;

            List<Function> data = GetService().ListAuthorizedFunctions(userId);
            azData = new AuthorizedData(DateTime.Now, data);
            _data.Add(userId, azData);

            return azData;
        }

        [Serializable]
        private class AuthorizedData
        {
            DateTime _loadTime;

            public DateTime LoadTime
            {
                get { return _loadTime; }
                set { _loadTime = value; }
            }
            List<Function> _data;

            public List<Function> Data
            {
                get { return _data; }
                set { _data = value; }
            }

            public AuthorizedData(DateTime loadTime, List<Function> data)
            {
                _loadTime = loadTime;
                _data = data;
            }
        }


    }
}
