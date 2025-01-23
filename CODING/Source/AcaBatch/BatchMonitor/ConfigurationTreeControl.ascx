<%@ Register TagPrefix="uc1" TagName="DetailsControl" Src="DetailsControl.ascx" %>
<%@ Control Language="c#" Inherits="Avanade.ACA.Batch.BatchMonitor.ConfigurationTreeControl" Codebehind="ConfigurationTreeControl.ascx.cs" %>
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="1">
	<TBODY>
		<tr vAlign="top">
			<td width="55%">
				<asp:datalist id="DataList1" CellSpacing="0" CellPadding="0" Width="100%" OnItemCommand="DataList1_ItemCommand" ItemStyle-BorderWidth="1" Font-Name="Verdana" ItemStyle-BorderStyle="Solid" BorderStyle="None" runat="server" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="x-small">
					<HeaderStyle BackColor="black" ForeColor="white" Font-Bold="true"></HeaderStyle>
					<HeaderTemplate>
						Click on the link to see the properties for the item.
					</HeaderTemplate>
					<ItemStyle ForeColor="Navy" BorderStyle="Solid" Width="100%" BorderWidth="1"></ItemStyle>
					<ItemTemplate>
						<asp:Literal Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Indentation") %>' >
						</asp:Literal>
						<asp:imagebutton runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "Icon") %>' Enabled='<%#DataBinder.Eval(Container.DataItem, "Expandable")%>' CommandName="Expand" onclick="OnExpand_Click">
						</asp:imagebutton>
						<asp:linkbutton runat="server" CommandName="Details" ID="Linkbutton1" Enabled='<%#DataBinder.Eval(Container.DataItem, "LinkEnabled")%>'>
							<%#DataBinder.Eval(Container.DataItem, "DisplayName")%>
						</asp:linkbutton>
					</ItemTemplate>
					<FooterTemplate>
					</FooterTemplate>
				</asp:datalist>
			</td>
			<td width="45%">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td><uc1:detailscontrol id="DetailsControl1" runat="server"></uc1:detailscontrol>
						</td>
					</tr>
					<tr>
						<td></td>
					</tr>
					<tr valign="top">
						<td><asp:literal id="LiteralTransition" runat="server"></asp:literal></td>
					</tr>
					<tr>
						<td></td>
					</tr>
				</table>
			</td>
		</tr>
	</TBODY>
</TABLE>
