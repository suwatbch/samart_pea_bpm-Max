<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SystemMonitoring.aspx.cs" Inherits="PEA.BPM.WebService.Security.Admin.SystemMonitoring" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 800px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 163px; background-color: black;">
                <asp:Label ID="Label2" runat="server" ForeColor="White" Text="Service information"></asp:Label></td>
            <td style="background-color: black">
            </td>
        </tr>
        <tr>
            <td style="width: 163px">
                Start up</td>
            <td>
    <asp:Label ID="StartUpTimeLab" runat="server" Text="System startup time"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 163px">
                Version</td>
            <td>
    <asp:Label ID="VersionLab" runat="server" Text="Version"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 163px">
            </td>
            <td>
            </td>
        </tr>
    </table>

    <table style="width: 800px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 163px; background-color: black; height: 19px;">
                <asp:Label ID="Label1" runat="server" Text="Features:" ForeColor="White"></asp:Label></td>
            <td style="width: 75px; height: 19px; background-color: black">
            </td>
            <td style="background-color: black; height: 19px;">
            </td>
        </tr>
        <tr>
            <td style="width: 163px; height: 21px">
                Update to database</td>
            <td style="width: 75px; height: 21px">
                <asp:CheckBox ID="SyncToDatbaseCB" runat="server" />
            </td>
            <td style="height: 21px">
    <asp:Label ID="SyncToDatbaseLab" runat="server" Text="Sync to database"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 163px">
                Check user offline</td>
            <td style="width: 75px">
                <asp:CheckBox ID="CheckUserOfflineCB" runat="server" />
            </td>
            <td>
    <asp:Label ID="CheckUserOfflineLab" runat="server" Text="Check user offline"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 163px; height: 19px">
                Record statistics</td>
            <td style="width: 75px; height: 19px">
                <asp:CheckBox ID="RecordStatCB" runat="server" />
            </td>
            <td style="height: 19px">
                <asp:Label ID="RecordStatLab" runat="server" Text="Record statistics"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 163px; height: 19px">
                Save log to database</td>
            <td style="width: 75px; height: 19px">
                <asp:CheckBox ID="SaveLogToDatabaseCB" runat="server" />
            </td>
            <td style="height: 19px">
                <asp:Label ID="SaveLogToDatabaseLab" runat="server" Text="Save log to database"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 163px; height: 19px">
            </td>
            <td style="width: 75px; height: 19px">
                <asp:Button ID="SaveBtn" runat="server" OnClick="SaveBtn_Click" Text="Save" /></td>
            <td style="height: 19px">
            </td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>
