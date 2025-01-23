<%@ Reference Control="~/DetailsControl.ascx" %>
<%@ Reference Control="~/QueueControl.ascx" %>
<%@ Page language="c#" Inherits="Avanade.ACA.Batch.BatchMonitor.Utilities" Codebehind="Utilities.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Utilities</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="AvanadeContent.css" type="text/css" rel="stylesheet">
		<LINK href="AvanadeHdr.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="calendar2.js"></script>
	</HEAD>
	<body class="ContentBG">
		<form method="post" runat="server" onsubmit="javascript:return window.confirm('Are you sure you want to perform this action?');">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<TR>
						<TD>
							<TABLE class="BannerL" height="60" cellSpacing="0" cellPadding="0" width="124">
								<TBODY>
									<TR>
										<TD vAlign="top"><IMG height="44" hspace="30" src="./icons/on.gif" width="34" vspace="3" border="0">
										</TD>
									</TR>
								</TBODY>
							</TABLE>
						</TD>
						<TD class="BannerC" width="99%"><LABEL class="HdrBanner" id="strHdrBanner">ACA Batch 
								Monitor</LABEL>
						</TD>
						<TD class="BannerC"><SPAN style="WIDTH: 20px"></SPAN></TD>
						<TD class="BannerC" style="PADDING-BOTTOM: 10px" noWrap></TD>
						<TD class="BannerR"><SPAN style="WIDTH: 23px"></SPAN></TD>
					</TR>
					<TR bgColor="black">
						<td colSpan="8">
							<table align="left" bgColor="black">
								<tr bgColor="black">
									<td width="118">&nbsp;</td>
									<TD class="HdrNavMenuLink" bgColor="black"><A href="./default.aspx">Request Queue</A></TD>
									<td bgColor="black"><font color="orange">| </font>
									</td>
									<TD class="HdrNavMenuLink" bgColor="black"><A href="./Batches.aspx">Batch Database</A></TD>
									<td bgColor="black"><font color="orange">| </font>
									</td>
									<TD class="HdrNavMenuLink" bgColor="black"><A href="./Utilities.aspx">Utilities</A></TD>
								</tr>
							</table>
						</td>
					</TR>
				</TBODY>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr height="30">
					<td width="60"></td>
					<td class="head3" vAlign="top" colSpan="3">Utilities</td>
				</tr>
				<tr height="30">
					<td width="60"></td>
					<td class="head3" vAlign="top" colSpan="3"><asp:label id="LabelErrorMsg" runat="server" ForeColor="Red" EnableViewState="False" Width="450px"></asp:label></td>
				</tr>
				<tr vAlign="top">
					<td width="60"></td>
					<td style="WIDTH: 172px" vAlign="top"><asp:button id="btnCleanArchive" runat="server" Text="Clean Entire Archive" Width="160px" onclick="btnCleanArchive_Click"></asp:button></td>
					<td vAlign="top">Delete all&nbsp;the archived requests from the batch database, 
						including the completed, canceled, failed and timed out ones.</td>
					<td width="80"></td>
				</tr>
				<tr height="20">
					<td colSpan="4"></td>
				</tr>
				<tr vAlign="top">
					<td></td>
					<td style="WIDTH: 172px" vAlign="top"><asp:button id="btnCleanOlder" runat="server" Text="Delete Archive" Width="160px" onclick="btnCleanOlder_Click"></asp:button></td>
					<td vAlign="top">Delete requests enqueued before:
						<asp:literal id="LiteralDate" Runat="server"></asp:literal><A href="javascript:CalendarFrom.popup();"><IMG height="16" alt="Click here to select the start date and time for the search" src="img/cal.gif"
								width="16" border="0"></A>
					</td>
					<td></td>
				</tr>
				<tr height="20">
					<td colSpan="4"></td>
				</tr>
				<tr>
					<td></td>
					<td style="WIDTH: 172px" vAlign="top"><asp:button id="btnArchiveExpired" runat="server" Text="Archive Expired Requests" Width="160px" onclick="btnArchiveExpired_Click"></asp:button></td>
					<td vAlign="top">Archive all expired requests with the status of "New In Queue" or 
						"In Queue Restarted".</td>
					<td></td>
				</tr>
			</table>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr height="200">
					<td>&nbsp;</td>
				</tr>
			</TABLE>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
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
						<IMG height="12" src="./icons/spacer.gif" width="1" border="0"> <SPAN class="FooterText">
							2006 Avanade Inc. All Rights Reserved.</SPAN>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD width="47%"><IMG height="1" src="./icons/spacer.gif" width="48" border="0"></TD>
					<TD><IMG height="48" alt="Avanade" src="./icons/avanade.logo.white.large.gif" width="304"
							vspace="48" border="0"></TD>
					<TD><IMG height="1" src="./icons/spacer.gif" width="48" border="0"></TD>
				</TR>
			</TABLE>
		</form>
		<script language="javascript">
	var CalendarFrom = new calendar2(document.forms(0).elements['DateOlder']);
	CalendarFrom.year_scroll = false;
	CalendarFrom.time_comp = true;
		</script>
	</body>
</HTML>
