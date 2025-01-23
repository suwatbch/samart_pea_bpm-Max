<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CashierMonitoring.aspx.cs" Inherits="PEA.BPM.WebService.Security.Admin.CashierMonitoring" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="CashierBtn" runat="server" OnClick="CashierBtn_Click" Text="Cashier" />
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Height="197px" TextMode="MultiLine"></asp:TextBox><br />
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <asp:Button ID="WatchBtn" runat="server" OnClick="WatchBtn_Click" Text="Watch Cache" />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:TextBox ID="RefreshCashierIDTxt" runat="server"></asp:TextBox>
    <asp:Button ID="RefreshCacheBtn" runat="server" OnClick="RefreshCacheBtn_Click" Text="Refresh Cache" /><br />
    &nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataSourceID="CashierCache" ForeColor="#333333" GridLines="None" EmptyDataText="No data.">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:CheckBoxField DataField="IsDirty" HeaderText="IsDirty" SortExpression="IsDirty" />
            <asp:BoundField DataField="WorkId" HeaderText="WorkId" SortExpression="WorkId" />
            <asp:CheckBoxField DataField="IsWorkIdDirty" HeaderText="IsWorkIdDirty" SortExpression="IsWorkIdDirty" />
            <asp:BoundField DataField="CashierId" HeaderText="CashierId" SortExpression="CashierId" />
            <asp:CheckBoxField DataField="IsCashierIdDirty" HeaderText="IsCashierIdDirty" SortExpression="IsCashierIdDirty" />
            <asp:BoundField DataField="PosId" HeaderText="PosId" SortExpression="PosId" />
            <asp:CheckBoxField DataField="IsPosIdDirty" HeaderText="IsPosIdDirty" SortExpression="IsPosIdDirty" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            <asp:CheckBoxField DataField="IsStatusDirty" HeaderText="IsStatusDirty" SortExpression="IsStatusDirty" />
            <asp:BoundField DataField="OpenWorkDt" HeaderText="OpenWorkDt" SortExpression="OpenWorkDt" />
            <asp:CheckBoxField DataField="IsOpenWorkDtDirty" HeaderText="IsOpenWorkDtDirty" SortExpression="IsOpenWorkDtDirty" />
            <asp:BoundField DataField="CloseWorkDt" HeaderText="CloseWorkDt" SortExpression="CloseWorkDt" />
            <asp:CheckBoxField DataField="IsCloseWorkDtDirty" HeaderText="IsCloseWorkDtDirty"
                SortExpression="IsCloseWorkDtDirty" />
            <asp:BoundField DataField="CloseWorkBy" HeaderText="CloseWorkBy" SortExpression="CloseWorkBy" />
            <asp:CheckBoxField DataField="IsCloseWorkByDirty" HeaderText="IsCloseWorkByDirty"
                SortExpression="IsCloseWorkByDirty" />
            <asp:BoundField DataField="BranchId" HeaderText="BranchId" SortExpression="BranchId" />
            <asp:CheckBoxField DataField="IsBranchIdDirty" HeaderText="IsBranchIdDirty" SortExpression="IsBranchIdDirty" />
            <asp:BoundField DataField="BaseLine" HeaderText="BaseLine" SortExpression="BaseLine" />
            <asp:CheckBoxField DataField="IsBaseLineDirty" HeaderText="IsBaseLineDirty" SortExpression="IsBaseLineDirty" />
            <asp:BoundField DataField="MarkedBL" HeaderText="MarkedBL" SortExpression="MarkedBL" />
            <asp:CheckBoxField DataField="IsMarkedBLDirty" HeaderText="IsMarkedBLDirty" SortExpression="IsMarkedBLDirty" />
            <asp:BoundField DataField="PostDt" HeaderText="PostDt" SortExpression="PostDt" />
            <asp:CheckBoxField DataField="IsPostDtDirty" HeaderText="IsPostDtDirty" SortExpression="IsPostDtDirty" />
            <asp:BoundField DataField="SyncFlag" HeaderText="SyncFlag" SortExpression="SyncFlag" />
            <asp:CheckBoxField DataField="IsSyncFlagDirty" HeaderText="IsSyncFlagDirty" SortExpression="IsSyncFlagDirty" />
            <asp:BoundField DataField="ModifiedDt" HeaderText="ModifiedDt" SortExpression="ModifiedDt" />
            <asp:CheckBoxField DataField="IsModifiedDtDirty" HeaderText="IsModifiedDtDirty" SortExpression="IsModifiedDtDirty" />
            <asp:BoundField DataField="ModifiedBy" HeaderText="ModifiedBy" SortExpression="ModifiedBy" />
            <asp:CheckBoxField DataField="IsModifiedByDirty" HeaderText="IsModifiedByDirty" SortExpression="IsModifiedByDirty" />
            <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Active" />
            <asp:CheckBoxField DataField="IsActiveDirty" HeaderText="IsActiveDirty" SortExpression="IsActiveDirty" />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <asp:ObjectDataSource ID="CashierCache" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="CASGetCachingCashierWorkStatusByCashierId" TypeName="PEA.BPM.WebService.Security.MonitoringController">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBox2" Name="cashierid" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
