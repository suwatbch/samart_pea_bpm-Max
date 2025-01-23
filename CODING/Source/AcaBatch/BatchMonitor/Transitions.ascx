<%@ Control Language="c#" Inherits="Avanade.ACA.Batch.BatchMonitor.Transitions1" Codebehind="Transitions.ascx.cs" %>
<P>
	<asp:label id="Label1" Font-Size="Medium" runat="server" Font-Bold="True" Font-Names="Arial" CssClass="Head3"></asp:label>
	<asp:label id="LabelErrorMessage" EnableViewState="False" runat="server" Font-Bold="True" ForeColor="Red" Font-Names="Arial" CssClass="Head3"></asp:label></P>
<P>
	<asp:datagrid id="DataGrid1" Font-Size="x-small" Font-Name="Verdana" Width="95%" runat="server" AllowSorting="true" OnSortCommand="DataGrid1_Sort" EnableViewState="true"></asp:datagrid><br>
	<br>
	<asp:button id="btnRefresh" runat="server" Text="Refresh" onclick="btnRefresh_Click"></asp:button>
</P>
<asp:Literal id="Literal1" runat="server"></asp:Literal>
