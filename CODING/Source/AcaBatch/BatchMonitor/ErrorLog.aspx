<%@ Register TagPrefix="uc1" TagName="BatchLog" Src="ErrorLog.ascx" %>
<%@ Page language="c#" Inherits="Avanade.ACA.Batch.BatchMonitor.ErrorLog" Codebehind="ErrorLog.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="AvanadeContent.css" type="text/css" rel="stylesheet">
		<LINK href="AvanadeHdr.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body CLASS="ContentBG" style="position: relative">
		<form method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE class="BannerL" height="60" cellSpacing="0" cellPadding="0" width="124">
							<TR>
								<TD vAlign="top" style="height: 60px"><IMG height="44" hspace="30" src="./icons/on.gif" width="34" vspace="3" border="0">
								</TD>
							</TR>
						</TABLE>
					</TD>
					<TD class="BannerC" width="99%"><LABEL class="HdrBanner" id="strHdrBanner">ACA Batch 
							Monitor - Batch Logging</LABEL>
					</TD>
					<TD class="BannerC"><SPAN style="WIDTH: 20px"></SPAN></TD>
					<TD class="BannerC" style="PADDING-BOTTOM: 10px" noWrap></TD>
					<TD class="BannerR"><SPAN style="WIDTH: 23px"></SPAN></TD>
				</TR>
			</TABLE>
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr width="20">
					<td></td>
				</tr>
				<tr>
					<td align="center"><uc1:BatchLog id="ErrorLog1" runat="server"></uc1:BatchLog></td>
				</tr>
				<tr height="20">
					<td></td>
				</tr>
				<tr>
					<td align="center"><A href="javascript:window.close();">Close Window</A></td>
				</tr>
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
						<IMG height="12" src="./icons/spacer.gif" width="1" border="0">&nbsp; <SPAN class="FooterText">
							2006 Avanade Inc. All Rights Reserved.</SPAN>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
