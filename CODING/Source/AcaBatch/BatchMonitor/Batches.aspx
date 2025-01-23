<%@ Page language="c#" Inherits="Avanade.ACA.Batch.BatchMonitor.Batches" Codebehind="Batches.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="BatchControl" Src="BatchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ConfigurationTreeControl" Src="ConfigurationTreeControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Batch Database</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="AvanadeContent.css" type="text/css" rel="stylesheet">
		<LINK href="AvanadeHdr.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body CLASS="ContentBG">
		<form id="Form1" method="post" runat="server">
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
						<TD class="BannerC" width="99%">
							<LABEL class="HdrBanner" id="strHdrBanner">ACA Batch Monitor</LABEL>
						</TD>
						<TD class="BannerC"><SPAN style="WIDTH: 20px"></SPAN></TD>
						<TD class="BannerC" style="PADDING-BOTTOM: 10px" noWrap>
						</TD>
						<TD class="BannerR"><SPAN style="WIDTH: 23px"></SPAN></TD>
					</TR>
					<TR bgcolor="black">
						<td colspan="8">
							<table align="left" bgcolor="black">
								<tr bgcolor="black">
									<td width="118">&nbsp;</td>
									<TD bgColor="black" class="HdrNavMenuLink"><a href="./default.aspx">Request Queue</a></TD>
									<td bgColor="black"><font color="orange"> | </font>
									</td>
									<TD bgColor="black" class="HdrNavMenuLink"><a href="./Batches.aspx">Batch Database</a></TD>
									<td bgColor="black"><font color="orange">| </font>
									</td>
									<TD class="HdrNavMenuLink" bgColor="black"><A href="./Utilities.aspx">Utilities</A></TD>
								</tr>
							</table>
						</td>
					</TR>
				</TBODY>
			</TABLE>
			<table width="100%" height="400">
				<tr valign="top">
					<td>
						<uc1:BatchControl id="BatchControl1" runat="server"></uc1:BatchControl>
					</td>
				</tr>
			</table>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="center">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TBODY>
								<TR>
									<TD width="90%" background="./icons/endfoot.gif"></TD>
									<TD width="1%"><IMG height="27" src="./icons/sidefoot.gif"></TD>
									<TD width="9%">
										<DIV CLASS="Footer">
										</DIV>
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
	</body>
</HTML>
