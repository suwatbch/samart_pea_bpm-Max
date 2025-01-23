<%@ Reference Control="~/DetailsControl.ascx" %>
<%@ Control Language="c#" Inherits="Avanade.ACA.Batch.BatchMonitor.QueueControl"
    Debug="True" CodeBehind="QueueControl.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="DetailsControl" Src="DetailsControl.ascx" %>
<table id="Table1" style="font-family: Arial" cellspacing="0" cellpadding="0" width="720"
    border="0">
    <tr>
        <td style="font-weight: bold; font-size: larger" colspan="2">
            Filter&nbsp;Requests by:
        </td>
    </tr>
    <tr height="30">
        <td style="font-weight: bold; font-size: larger">
            <font color="blue" size="3">Status</font>
        </td>
        <td style="font-weight: bold; font-size: larger">
            <font color="blue" size="3">Date Enqueued</font>
        </td>
    </tr>
    <tr valign="top">
        <td>
            <table style="font-size: x-small" cellspacing="0" cellpadding="0" width="360" border="0">
                <tr height="22">
                    <td style="width: 29px">
                    </td>
                    <td style="width: 200px">
                        <input onclick="javascript:UndateCheckAll(this);" type="checkbox" name="chkNew">&nbsp;New
                        In Queue
                    </td>
                    <td style="width: 200px">
                        <input onclick="javascript:UndateCheckAll(this);" type="checkbox" name="chkRestart">&nbsp;In
                        Queue Restarted
                    </td>
                </tr>
                <tr height="22">
                    <td style="width: 29px">
                    </td>
                    <td style="width: 200px">
                        <input onclick="javascript:UndateCheckAll(this);" type="checkbox" name="chkDequeued">&nbsp;Dequeued
                    </td>
                    <td style="width: 200px">
                        <input onclick="javascript:UndateCheckAll(this);" type="checkbox" name="chkRunning">&nbsp;Executing
                    </td>
                </tr>
                <tr height="22">
                    <td style="width: 29px">
                    </td>
                    <td style="width: 200px">
                        <input onclick="javascript:UndateCheckAll(this);" type="checkbox" name="chkPaused">&nbsp;Paused
                    </td>
                    <td style="width: 200px">
                        <input onclick="javascript:UndateCheckAll(this);" type="checkbox" name="chkSuccess">&nbsp;Completed
                        Successfully
                    </td>
                </tr>
                <tr height="22">
                    <td style="width: 29px">
                    </td>
                    <td style="width: 200px">
                        <input onclick="javascript:UndateCheckAll(this);" type="checkbox" name="chkFailed">&nbsp;Failed
                    </td>
                    <td style="width: 200px">
                        <input onclick="javascript:UndateCheckAll(this);" type="checkbox" name="chkCancelled">&nbsp;Cancelled
                    </td>
                </tr>
                <tr height="22">
                    <td style="width: 29px">
                    </td>
                    <td style="width: 200px">
                        <input onclick="javascript:UndateCheckAll(this);" type="checkbox" name="chkTimeout">&nbsp;Timed-out
                    </td>
                    <td style="width: 200px">
                        <input onclick="javascript:UndateCheckAll(this);" type="checkbox" name="chkSystemCancelled">&nbsp;SystemCancelled
                    </td>
                </tr>
                <tr valign="bottom" height="22">
                    <td>
                    </td>
                    <td valign="middle">
                        <input onclick="javascript:CheckAll();" type="checkbox" name="chkAll" id="chkAll">Select
                        All Status
                    </td>
                    <td>
                        <input id="Submit" onclick="javascript:GetCheckedValues();" type="submit" value="Submit">
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <table style="font-size: x-small" cellspacing="0" cellpadding="0" width="360" border="0">
                <tr height="22">
                    <td style="width: 29px">
                    </td>
                    <td style="width: 100px">
                        <input onclick="javascript:EnableDate(this.checked, 'DateFrom');" type="checkbox"
                            name="chkDateFrom">&nbsp;From Date:
                    </td>
                    <td style="width: 260px">
                        <input disabled type="text" name="DateFrom">
                        <a href="javascript:PopUpDate('DateFrom', 'chkDateFrom', CalendarFrom);">
                            <img height="16" alt="Click here to select the start date and time for the search"
                                src="img/cal.gif" width="16" border="0"></a>
                    </td>
                </tr>
                <tr height="19">
                    <td colspan="3">
                    </td>
                </tr>
                <tr height="22">
                    <td style="height: 22px">
                    </td>
                    <td style="height: 22px">
                        <input onclick="javascript:EnableDate(this.checked, 'DateTo');" type="checkbox" alt="enable 'to date'"
                            name="chkDateTo">&nbsp;To Date:
                    </td>
                    <td style="height: 22px">
                        <input disabled type="text" name="DateTo">
                        <a href="javascript:PopUpDate('DateTo', 'chkDateTo', CalendarTo);">
                            <img height="16" alt="Click here to select the end date and time for the search"
                                src="img/cal.gif" width="16" border="0"></a><br>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" height="19">
                    </td>
                </tr>
                <tr>
                    <td style="height: 22px">
                    </td>
                    <td style="height: 22px" colspan="2">
                        <asp:CheckBox ID="cbAutoRefresh" runat="server" Checked="True" />&nbsp;Auto Refresh
                        <asp:Label ID="LabelSort" EnableViewState="False" Width="100%" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<p>
    <asp:Label ID="Label1" Width="450px" runat="server" Font-Size="Medium" Font-Names="Arial"
        ForeColor="Red" /><br>
    <table class="" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="1">
        <tbody>
            <tr valign="top">
                <td width="55%">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                        <asp:Timer ID="Timer1" runat="server" Interval="3000" 
                        ontick="Timer1_Tick" />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                            <asp:PostBackTrigger ControlID="DataGrid1" />
                        </Triggers>
                    <ContentTemplate>
                    <asp:Label ID="lbLastUpdateDataGrid1" runat="server" Text="Grid not refreshed yet." /><br />
                    <p class="Head3">
                        <!--<asp:Label ID="Label2" runat="server" Visible="false" Height="40">Click on the item to see details: </asp:Label>-->
                        <asp:DataGrid
                            ID="DataGrid1" Width="100%" runat="server" OnSortCommand="DataGrid1_SortCommand"
                            AllowSorting="True" AllowPaging="True" OnPageIndexChanged="DataGrid1_Page" CellSpacing="0"
                            OnItemCommand="DataGrid1_ItemCommand" Font-Name="Verdana" BorderWidth="1" BorderStyle="Solid"
                            ItemStyle-Font-Size="x-small" CssClass="Body" AutoGenerateColumns="false" PagerStyle-Mode="NumericPages"
                            PageSize="30" GridLines="Horizontal">
                            <ItemStyle Font-Size="X-Small" Width="100%"></ItemStyle>
                            <HeaderStyle Font-Bold="True" BackColor="Azure"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemStyle Width="10px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Image ID="SelectedImage" runat="server" ImageUrl="./icons/Nav1Rt.gif" Visible="false">
                                        </asp:Image>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Request" HeaderText="Requests">
                                    <ItemStyle Font-Size="xx-small"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CommandName="Details">
											<%# DataBinder.Eval(Container.DataItem, "Request") %>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="Status" HeaderText="Status">
                                    <ItemStyle Font-Size="xx-small"></ItemStyle>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Status") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <img src="./icons/pause2.bmp" alt="Pausing" border="0" width="17" height="17">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="pausing" runat="server" Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="QueuedDate" HeaderText="Queued Since">
                                    <ItemStyle Font-Size="xx-small"></ItemStyle>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "QueuedDate") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Options">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Options")%>
                                        <asp:LinkButton ID="btnPause" runat="server" CommandName="Pause" Visible="false"
                                            BorderStyle="None">
											<img src="./icons/pause.bmp" alt="Pause Request" border="0" onclick="javascript:return window.confirm('Are you sure you want to pause the request?');"></asp:LinkButton>
                                        <asp:LinkButton ID="btnRun" runat="server" CommandName="Run" Visible="False" BorderStyle="None">
											<img src="./icons/run.bmp" Alt="Resume Request" border="0" onclick="javascript:return window.confirm('Are you sure you want to resume the request?');"></asp:LinkButton>
                                        <asp:LinkButton ID="btnStop" runat="server" CommandName="Stop" Visible="False" BorderStyle="None">
											<img src="./icons/stop.bmp" Alt="Cancel Request" border="0" onclick="javascript:return window.confirm('Are you sure you want to cancel the request?');"></asp:LinkButton>
                                        <asp:HyperLink ID="btnRestart" runat="server" Visible="false" BorderStyle="None"
                                            ImageUrl="./icons/restart.bmp"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Height="32px" Font-Bold="True" Position="TopAndBottom" Mode="NumericPages">
                            </PagerStyle>
                        </asp:DataGrid></p>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                </td>
                <td width="45%">
                    <uc1:DetailsControl ID="DetailsControl1" runat="server"></uc1:DetailsControl>
                </td>
            </tr>
        </tbody>
    </table>
    <input type="hidden" name="Key" value='<% = Request.Form["Key"] %>'>
    <!--</TD></TR></TBODY></TABLE></TR></TBODY></TABLE>-->

    <script language="javascript">
        var CalendarFrom = new calendar2(document.forms(0).elements['DateFrom']);
        CalendarFrom.year_scroll = false;
        CalendarFrom.time_comp = true;
        var CalendarTo = new calendar2(document.forms(0).elements['DateTo']);
        CalendarTo.year_scroll = false;
        CalendarTo.time_comp = true;

        function PopUpDate(textName, boxName, calendar) {
            document.forms(0).elements[textName].disabled = false;
            document.forms(0).elements[boxName].checked = true;
            calendar.popup();
        }

        function CheckAll() {
            isChecked = document.forms(0).elements['chkAll'].checked;
            document.forms(0).elements['chkNew'].checked = isChecked;
            document.forms(0).elements['chkRestart'].checked = isChecked;
            document.forms(0).elements['chkDequeued'].checked = isChecked;
            document.forms(0).elements['chkRunning'].checked = isChecked;
            document.forms(0).elements['chkPaused'].checked = isChecked;
            document.forms(0).elements['chkSuccess'].checked = isChecked;
            document.forms(0).elements['chkFailed'].checked = isChecked;
            document.forms(0).elements['chkCancelled'].checked = isChecked;
            document.forms(0).elements['chkTimeout'].checked = isChecked;
            document.forms(0).elements['chkSystemCancelled'].checked = isChecked;
            /*
            document.forms(0).elements['chkNew'].checked = true;
            document.forms(0).elements['chkRestart'].checked = true;
            document.forms(0).elements['chkDequeued'].checked = true;
            document.forms(0).elements['chkRunning'].checked = true;
            document.forms(0).elements['chkPaused'].checked = true;
            document.forms(0).elements['chkSuccess'].checked = true; 
            document.forms(0).elements['chkFailed'].checked = true;
            document.forms(0).elements['chkCancelled'].checked = true;
            document.forms(0).elements['chkTimeout'].checked = true;
            document.forms(0).elements['chkSystemCancelled'].checked = true;
            */
        }

        function GetCheckedValues() {
            var output = '';
            if (document.forms(0).elements['chkNew'].checked == true) {
                output = output + 'chkNew' + ",";
            }
            if (document.forms(0).elements['chkRestart'].checked == true) {
                output = output + 'chkRestart' + ",";
            }
            if (document.forms(0).elements['chkDequeued'].checked == true) {
                output = output + 'chkDequeued' + ",";
            }
            if (document.forms(0).elements['chkRunning'].checked == true) {
                output = output + 'chkRunning' + ",";
            }
            if (document.forms(0).elements['chkPaused'].checked == true) {
                output = output + 'chkPaused' + ",";
            }
            if (document.forms(0).elements['chkSuccess'].checked == true) {
                output = output + 'chkSuccess' + ",";
            }
            if (document.forms(0).elements['chkFailed'].checked == true) {
                output = output + 'chkFailed' + ",";
            }
            if (document.forms(0).elements['chkCancelled'].checked == true) {
                output = output + 'chkCancelled' + ",";
            }
            if (document.forms(0).elements['chkTimeout'].checked == true) {
                output = output + 'chkTimeout' + ",";
            }
            if (document.forms(0).elements['chkSystemCancelled'].checked == true) {
                output = output + 'chkSystemCancelled';
            }
            document.forms(0).elements['Key'].value = output;
        }

        function EnableDate(toEnable, name) {
            document.forms(0).elements[name].disabled = toEnable ? false : true;
        }

        function UndateCheckAll(chkBox) {
            if (chkBox.checked == false) {
                document.forms(0).elements['chkAll'].checked = false;
            }
        }
        function RecheckAll() {
            var output = document.forms(0).elements['Key'].value;
            //alert(output.length);
            if (output.length > 0) {
                var temp = output.split(',');
                for (i = 0; i < temp.length; i++) {
                    var name = temp[i];
                    document.forms(0).elements[name].checked = true;
                }
                if (temp.length == 10)
                    document.forms(0).elements["chkAll"].checked = true;
            }
        }

    </script>
    <script language="javascript" type="text/javascript">
        //The original javascript came from 
        //http://www.sunburnt.com.au/publications/design/javascript-fade-effects
        //I modified it to work with my code

        //set the opacity of the element (between 0.0 and 1.0)       

        function setOpacity(id, level) {
            var element = document.getElementById(id);
            element.style.display = 'inline';
            element.style.zoom = 1;
            element.style.opacity = level;
            element.style.MozOpacity = level;
            element.style.KhtmlOpacity = level;
            element.style.filter = "alpha(opacity=" + (level * 100) + ");";
        }

        function fadeIn(id, steps, duration, interval, fadeOutSteps, fadeOutDuration) {
            var fadeInComplete;
            for (i = 0; i <= 1; i += (1 / steps)) {
                setTimeout("setOpacity('" + id + "', " + i + ")", i * duration);
                fadeInComplete = i * duration;
            }
            //set the timeout to start after the fade in time and the interval to display the 
            //message on the screen have both completed

            setTimeout("fadeOut('" + id + "', " + fadeOutSteps +
                   ", " + fadeOutDuration + ")", fadeInComplete + interval);
        }

        function fadeOut(id, steps, duration) {
            var fadeOutComplete;
            for (i = 0; i <= 1; i += (1 / steps)) {
                setTimeout("setOpacity('" + id + "', " +
                    (1 - i) + ")", i * duration);
                fadeOutComplete = i * duration;
            }
            //completely hide the displayed message after the fade effect is complete
            setTimeout("hide('" + id + "')", fadeOutComplete);
        }

        function hide(id) {
            document.getElementById(id).style.display = 'none';
        }
    </script>

</p>
