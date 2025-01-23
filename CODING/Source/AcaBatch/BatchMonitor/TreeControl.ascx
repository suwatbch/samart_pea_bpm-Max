<%@ Reference Control="~/DetailsControl.ascx" %>
<%@ Reference Control="~/QueueControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DetailsControl" Src="DetailsControl.ascx" %>
<%@ Control Language="c#" Inherits="Avanade.ACA.Batch.BatchMonitor.TreeControl" Codebehind="TreeControl.ascx.cs" %>
<TABLE class="" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td width="95%">
			<P class="Head3"><asp:placeholder id="clientTreeObj" Runat="server" EnableViewState="False">
			</asp:placeholder></P>
                <asp:TreeView ID="tree1" runat="server" PathSeparator = "\" BorderStyle = "Double" Enabled =true ShowLines="True" Height="100px" Width="100%">
                    <ParentNodeStyle ForeColor="Gray" />
                    <SelectedNodeStyle ForeColor="#FFC080" />
                    <RootNodeStyle ForeColor="Black" />
                    <NodeStyle ForeColor="Gray" />
            </asp:TreeView>
		</td>
	</tr>
	<tr>
		<td width="95%"><uc1:detailscontrol id="DetailsControl1" runat="server"></uc1:detailscontrol></td>
	</tr>
	<tr>
		<td><asp:label id="Label1" runat="server" Font-Size="medium" Font-Names="Arial" Font-Bold="True"></asp:label></td>
	</tr>
	<tr height="20">
		<td></td>
	</tr>
	<tr>
		<td align="middle"><asp:button id="btnRefresh" runat="server" Text="Refresh"></asp:button></td>
	</tr>
</TABLE>
<asp:TextBox runat =server Visible =false Width =0 ID="State"></asp:TextBox>
<asp:TextBox runat =server Visible =false Width =0 ID="Key"></asp:TextBox>
<P><INPUT type="hidden" name="Key"> <INPUT type="hidden" name="State"><input type="hidden" name="VBScript">
	
</P>
