<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserMonitoring.aspx.cs" Inherits="PEA.BPM.WebService.Security.Admin.UserMonitoring" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="ActiveUserBtn" runat="server" OnClick="ActiveUserBtn_Click" Text="Active User" />&nbsp;
    <asp:Button ID="OfflineUserBtn" runat="server" OnClick="OfflineUserBtn_Click" Text="Offline User" />&nbsp;
    <asp:Button ID="StatisticBtn" runat="server" Text="Statistic" OnClick="StatisticBtn_Click" Visible="False" /><br />
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Height="800px" TextMode="MultiLine" Width="944px"
        Wrap="False"></asp:TextBox>
</asp:Content>
