<%@ Control Language="c#" Inherits="Avanade.ACA.Batch.BatchMonitor.ErrorLog1" CodeBehind="ErrorLog.ascx.cs" %>
<p>
    <asp:Label ID="Label1" Font-Size="Medium" runat="server" Font-Bold="True" Font-Names="Arial"
        CssClass="Head3"></asp:Label>
    <asp:Label ID="LabelErrorMessage" EnableViewState="False" runat="server" Font-Bold="True"
        ForeColor="Red" Font-Names="Arial" CssClass="Head3"></asp:Label></p>
<p>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="TimerAutorefresh" runat="server" Interval="5000" OnTick="TimerAutorefresh_Tick" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TimerAutorefresh" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <table border="0" Width="95%">
                <tr>
                    <td align="left" Width="110px" style="margin-left:0" ><asp:CheckBox ID="cbAutoRefresh" runat="server" Checked="True" /><font style="vertical-align:baseline">&nbsp;Auto Refresh</font></td>
                    <td align="left"><asp:Label ID="lbLastUpdateDataGrid1" runat="server" Text="Grid not refreshed yet." style="vertical-align:baseline" /></td>
                </tr>
            </table>
            <asp:DataGrid ID="DataGrid1" Font-Size="x-small" Font-Name="Verdana" Width="95%"
                runat="server" AllowSorting="true" OnSortCommand="DataGrid1_Sort" EnableViewState="true">
            </asp:DataGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click">
    </asp:Button>
</p>
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
