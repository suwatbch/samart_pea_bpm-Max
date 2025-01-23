//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-210-Creating%20a%20Smart%20Client%20Solution.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

namespace PEA.BPM.Infrastructure.Interface.Constants
{
    /// <summary>
    /// Constants for event topic names.
    /// </summary>
    public class EventTopicNames
    {
        ///// <summary>
        ///// Event fired to tell the shell to update the status panel
        ///// </summary>
        public const string StatusUpdate = "StatusUpdate";
        public const string LoginNameUpdate = "LoginNameUpdate";
        public const string ConnectionInfoUpdate = "ConnectionInfoUpdate";        
        public const string WindowsTitleUpdate = "WindowsTitleUpdate";
        public const string ShowStatusBar = "ShowStatusBar";
        public const string PrintSetupEvent = "PrintSetupEvent";
        public const string CashierOpenWork = "CashierOpenWork";
        public const string EnableMenuItem = "EnableMenuItem";
        public const string DisableMenuItem = "DisableMenuItem";
        public const string OnCloseAllViews = "OnCloseAllViews";
        public const string OnlineStatus = "OnlineStatus";
        public const string OnCloseViewDisconnect = "OnCloseViewDisconnect";
        public const string ShellWaitCursor = "ShellWaitCursor";
        public const string FinishOfflineUpload = "FinishOfflineUpload";
        public const string ProcessOfflineFile = "ProcessOfflineFile";
        public const string ShowCurrentWork = "ShowCurrentWork";
        public const string EnablePOSSaveButton = "EnablePOSSaveButton";
        public const string LoadCashInTray = "LoadCashInTray";

        public const string TestPrintPOSReceipt = "TestPrintPOSReceipt";
        public const string TestPrintPOSReport = "TestPrintPOSReport";
        public const string TestPrintAGENCYReport = "TestPrintAGENCYReport";
        public const string TestPrintBLAN_F16 = "TestPrintBLAN_F16";
        public const string ShowReportCAC19Click = "ShowReportCAC19Click";
    }
}
