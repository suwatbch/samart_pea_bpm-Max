<%@ Control Language="c#" Inherits="Avanade.ACA.Batch.BatchMonitor.DetailsControl" Codebehind="DetailsControl.ascx.cs" %>
<Table border="0" cellpadding="0" cellspacing="0" width="100%">
	<tr height="40">
		<td class="Head3">
			<asp:Label id="LabelObjectType" runat="server" Font-Names="Arial" Font-Bold="True"></asp:Label>&nbsp;
			<asp:LinkButton ID="btnPause" Runat="server" Visible="false" BorderStyle="None" onclick="btnPause_Click">
				<img src="./icons/pause.bmp" alt="Pause Request" border="0" onclick="javascript:return window.confirm('Are you sure you want to pause the request?');"></asp:LinkButton>&nbsp;
			<asp:LinkButton ID="btnRun" Runat="server" Visible="false" BorderStyle="None" onclick="btnRun_Click">
				<img src="./icons/run.bmp" alt="Resume Request" border="0" onclick="javascript:return window.confirm('Are you sure you want to resume the request?');"></asp:LinkButton>&nbsp;
			<asp:LinkButton ID="btnStop" Runat="server" Visible="false" BorderStyle="None" onclick="btnStop_Click">
				<img src="./icons/stop.bmp" alt="Cancel Request" border="0" onclick="javascript:return window.confirm('Are you sure you want to cancel the request?');"></asp:LinkButton>&nbsp;
			<asp:HyperLink id="btnRestart" Runat="server" Visible="false" BorderStyle="None" ImageUrl="./icons/restart.bmp"></asp:HyperLink>&nbsp;
			<asp:label id="Label1" runat="server" Font-Bold="True" ForeColor="Red" Font-Names="Arial" EnableViewState="False"></asp:label>
		</td>
	</tr>
	<tr>
		<td width="100%" style="height: 153px">
			<asp:DataGrid id="DataGrid1" CssClass="Body" runat="server" Width="100%" CellSpacing="0" Font-Name="Verdana" Font-Size="x-small">
				<HeaderStyle Height="22" Font-Bold="true" HorizontalAlign="Left" BackColor="Azure"></HeaderStyle>
				<ItemStyle Height="22" CssClass="LSOddRow"></ItemStyle>
				<AlternatingItemStyle CssClass="LSEvenRow" Wrap="True"></AlternatingItemStyle>
			</asp:DataGrid>
		</td>
	</tr>
	<tr>
		<td width="100%" style="height: 19px"><asp:Literal id="Links" runat="server"></asp:Literal></td>
	</tr>
	<SCRIPT language="javascript">
<!--
function js_PopWin(url,name)
{
	var ContextWindow = window.open(url,name,'width=450,height=450,resizable=yes,scrollbars=yes,status=yes');
	ContextWindow.opener = this;
	ContextWindow.focus();
}

function js_RefreshOriginalWindow(url) {
	if ((window.opener == null) || (window.opener.closed))
	{
		var OriginalWindow = window.open(url);
		OriginalWindow.opener = this;
	}
	else 
	{
		window.opener.location=url;
	}
}

function js_ShowStatus(str)
{
	status = str;
	return true;
}
//-->
	</SCRIPT>
</Table>
