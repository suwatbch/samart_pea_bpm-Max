<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
    Codebehind="LogMonitoring.aspx.cs" Inherits="PEA.BPM.WebService.Security.Admin.LogMonitoring"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="NormalRefreshBtn" runat="server" OnClick="NormalRefreshBtn_Click"
        Text="Normal Log" />
    &nbsp;&nbsp;<asp:Button ID="SystemLog" runat="server" OnClick="SystemLog_Click" Text="System Log" />&nbsp;
    <asp:Button ID="ErrorLog" runat="server" OnClick="ErrorLog_Click" Text="Error Log" />
    &nbsp;<asp:Button ID="SysErrorLog" runat="server" OnClick="SysErrorLog_Click" Text="System-Error Log" /><br />
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Height="800px" TextMode="MultiLine" Width="944px"
        Wrap="False"></asp:TextBox>
</asp:Content>
