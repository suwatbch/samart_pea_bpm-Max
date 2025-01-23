<%@ Reference Control="~/QueueControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ConfigurationTreeControl" Src="ConfigurationTreeControl.ascx" %>
<%@ Control Language="c#" Inherits="Avanade.ACA.Batch.BatchMonitor.BatchControl" Codebehind="BatchControl.ascx.cs" %>
<uc1:ConfigurationTreeControl id="ConfigurationTreeControl1" runat="server"></uc1:ConfigurationTreeControl>
<BR>
<asp:label id="Label1" runat="server" Font-Size="Medium" EnableViewState="False" Font-Names="Arial" ForeColor="Red" Width="450px"></asp:label>
<BR>
<BR>
<asp:button id="btnRefresh" runat="server" Text="Refresh" onclick="btnRefresh_Click"></asp:button>
