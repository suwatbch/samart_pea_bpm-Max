<%@ Reference Control="~/DetailsControl.ascx" %>
<%@ Page language="c#" Inherits="Avanade.ACA.Batch.BatchMonitor.Enqueue" Codebehind="Enqueue.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Enqueue a Request</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="AvanadeContent.css" type="text/css" rel="stylesheet">
		<LINK href="AvanadeHdr.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="calendar2.js"></script>
	</HEAD>
	<BODY class="ContentBG">
		<FORM id="ErrorLog" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE class="BannerL" height="60" cellSpacing="0" cellPadding="0" width="124">
							<TR>
								<TD vAlign="top"><IMG height="44" hspace="30" src="./icons/on.gif" width="34" vspace="3" border="0">
								</TD>
							</TR>
						</TABLE>
					</TD>
					<TD class="BannerC" width="99%"><LABEL class="HdrBanner" id="strHdrBanner">ACA Batch 
							Monitor</LABEL>
					</TD>
					<TD class="BannerC"><SPAN style="WIDTH: 20px"></SPAN></TD>
					<TD class="BannerC" style="PADDING-BOTTOM: 10px" noWrap></TD>
					<TD class="BannerR"><SPAN style="WIDTH: 23px"></SPAN></TD>
				</TR>
			</table>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td style="HEIGHT: 52px" width="10"></td>
					<td style="HEIGHT: 52px">
						<DIV class="Head3"></SPAN>Please enter the properties for the request.</DIV>
						<br>
						<div style="FONT-SIZE: small"><asp:label id="LabelHint" runat="server">The required properties are marked with '<FONT color="red">
									*</FONT>'</asp:label></div>
					</td>
				</tr>
				<tr>
					<td colSpan="2" height="10"></td>
				</tr>
				<tr>
					<td width="10" height="30"></td>
					<td height="30"><asp:label id="LabelErrorMsg" runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"
							Width="90%"></asp:label></td>
				</tr>
				<tr>
					<td></td>
					<td align="center">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td vAlign="top">
									<table cellSpacing="3" cellPadding="3" width="100%" border="0">
										<asp:literal id="literalRequest" Runat="server"></asp:literal>
										<tr>
											<td style="WIDTH: 45%" align="right">BatchName:</td>
											<td align="left"><asp:label id="BatchName" runat="server" Width="335px" BorderStyle="None"></asp:label></td>
										</tr>
										<tr>
											<td style="WIDTH: 45%" align="right">Queue Priority Level:</td>
											<td align="left"><asp:dropdownlist id="DropDownPriority" runat="server" Width="268px"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td style="WIDTH: 45%" align="right"><FONT color="red">*</FONT>Absolute Expiration 
												Date:</td>
											<td align="left"><asp:textbox id="txtAbsExpDate" runat="server" Width="270px"></asp:textbox>&nbsp;
												<asp:hyperlink id="LinkCalendar" runat="server" NavigateUrl="javascript:CalendarFrom.popup();">
													<IMG height="16" alt="Click here to select the start date and time for the search" src="img/cal.gif"
														width="16" border="0"></asp:hyperlink></td>
										</tr>
										<tr>
											<td style="WIDTH: 45%" align="right">Destination:</td>
											<td align="left"><asp:dropdownlist id="DropDownDestination" runat="server" Width="271px"></asp:dropdownlist>&nbsp;or
												<asp:textbox id="NewDestination" runat="server" Width="270px"></asp:textbox></td>
										</tr>
										<tr>
											<td style="WIDTH: 45%" align="right"><FONT color="red">*</FONT>Batch Client Name:</td>
											<td align="left"><asp:textbox id="txtClientName" runat="server" Width="270px"></asp:textbox></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2" height="10"></td>
				</tr>
				<tr>
					<td align="center" colSpan="2"><asp:button id="btnEnqueue" runat="server" Width="160px" Text="Enqueue Requests" onclick="btnEnqueue_Click"></asp:button></td>
				</tr>
				<tr>
					<td colSpan="2" height="30"></td>
				</tr>
				<tr>
					<td></td>
					<td align="center"><A href="javascript:window.close();">Close Window</A></td>
				</tr>
				<TR>
					<td></td>
					<TD align="center">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TBODY>
								<TR>
									<TD width="90%" background="./icons/endfoot.gif"></TD>
									<TD width="1%"><IMG height="27" src="./icons/sidefoot.gif"></TD>
									<TD width="9%">
										<DIV class="Footer"></DIV>
									</TD>
								</TR>
							</TBODY>
						</TABLE>
						<IMG height="12" src="./icons/spacer.gif" width="1" border="0">&nbsp; <SPAN class="FooterText">
							2006&nbsp;Avanade Inc. All Rights Reserved.</SPAN>
					</TD>
				</TR>
			</table>
			<script language="javascript">	
			</script>
			<script language="javascript">
	var CalendarFrom = new calendar2(document.forms(0)['txtAbsExpDate']);
	CalendarFrom.year_scroll = false;
	CalendarFrom.time_comp = true;
	
	function js_PopWin(url,name)
	{
		var ContextWindow = window.open(url,name,'width=450,height=450,resizable=yes,scrollbars=yes,status=yes');
		ContextWindow.opener = this;
		ContextWindow.focus();
	}
			</script>
		</FORM>
	</BODY>
</HTML>
