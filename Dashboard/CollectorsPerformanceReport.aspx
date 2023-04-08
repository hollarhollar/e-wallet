<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CollectorsPerformanceReport.aspx.cs" EnableEventValidation="true" MasterPageFile="~/Dashboard/MasterPage.master" UICulture="en-Ng" Culture="en-NG" Inherits="Dashboard_CollectorsPerformanceReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/js/bootstrap.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker6').datepicker({
                dateFormat: 'mm/dd/yyyy',
                timeFormat: 'hh:mm:ss',
                autoclose: true
            });
        });
        $(function () {
            $('#datetimepicker5').datepicker({
                dateFormat: 'mm/dd/yyyy',
                timeFormat: 'hh:mm:ss',
                autoclose: true
            });
        });
    </script> --%>
    <script>  
        $(function () {
            $('#datetimepicker6').datepicker(
                {
                    dateFormat: 'mm/dd/yy',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: '1950:2100',
                    autoclose: true

                });
        })

        $(function () {
            $('#datetimepicker7').datepicker(
                {
                    dateFormat: 'MM yy',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: '1950:2100',
                    autoclose: true

                });
        })
        $(function () {
            $('#datetimepicker8').datepicker(
                {
                    dateFormat: 'MM yy',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: '1950:2100',
                    autoclose: true

                });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
    <style>
        #selectedid {
            float: right;
            margin-right: 10px;
            width: 570px;
        }

        #selectedid2 {
            float: right;
            margin-right: 10px;
            width: 570px;
        }

        #DropDownStatus {
            width: 100%;
            margin-bottom: 10px;
        }

        #DropDownList1 {
            width: 100%;
            margin-bottom: 10px;
        }

        .btn:hover {
            background-color: #0069d9;
            color: white;
        }

        .align-right {
            text-align: right;
        }

        .align-start {
            align-items: flex-start;
        }

        .box-title {
            font: bold 18px;
            font-family: 'Sans Serif Collection';
            font-display: auto;
            font-size: 18px;
            margin-bottom: 5px;
            padding: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="box-header with-border">
        <h3 class="box-title">Collectors Performance Report</h3>
    </div>
    <div>
        <div style="padding-left: 10px">
            <table class="border" style="width: 100%">
                <tr>
                    <td>
                        <span class="hidden-xs" style="background-color: #fff; color: #4cae4c; padding: 10px;">
                            <asp:Label runat="server" ID="selectedDate"></asp:Label>
                        </span>
                    </td>
                    <td style="width: 110px;">PICK WEEK</td>
                    <td>
                        <div class='input-group date' id='datetimepicker6' style="width: 160px; margin-right: -50px;">
                            <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Start Date" ID="txt_startDate" class="form-control " />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </td>

                    <td colspan="3">
                        <asp:Button ID="Button1" Text="Search" runat="server" CssClass="btn btn-success" OnClick="btn_search_Click" Visible="true" />
                    </td>
                </tr>
            </table>
            <div id="selectedid" class="width: 170px ;" align="right" style="margin-right: 15px;">
                <asp:DropDownList ID="DropDownStatus" runat="server" CssClass="form-control col-lg-4 align:right" Style="width: 20%">
                    <asp:ListItem Text="Best Performance" Value="BestPerformer"></asp:ListItem>
                    <asp:ListItem Text="Underperforming" Value="Underperforming"></asp:ListItem>
                    <asp:ListItem Text="Within Threshold" Value="Threshold"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button runat="server" CssClass="btn btn-primary col-lg-2 align-start" Text="Filter Status" type="submit" ID="Button2" OnClick="btnWeekly_Click" />
            </div>
        </div>
    </div>
    <br />
    <header class="h3 p4 mr4">Weekly Filter</header>
    <asp:GridView ID="grd_collector" runat="server" AllowPaging="True" AllowSorting="True" ShowHeader="true"
        AutoGenerateColumns="False" PagerSettings-PageButtonCount="10" PageSize="10"
        CssClass="table table-striped table-bordered table-hover table-dark" HeaderStyle-CssClass="GridHeader" OnPageIndexChanging="grd_paginHandler" CellPadding="12" ForeColor="#333333" GridLines="Vertical">
        <AlternatingRowStyle BackColor="white" />
      <Columns>
            <asp:BoundField DataField="Name" HeaderText="Collector Name" />
            <asp:BoundField DataField="Collector" HeaderText="Collector Monile No" />
            <asp:BoundField DataField="bestperformedweek" HeaderText="Best Performing Week" />
            <asp:BoundField DataField="transactions" DataFormatString="{0:C}" HeaderText="(₦)  Transactions" HtmlEncode="false" />
            <asp:BoundField DataField="Current_Week" HeaderText="Current Week" />
            <asp:BoundField DataField="transactions_current_week" DataFormatString="{0:C}" HeaderText="(₦) Transaction" HtmlEncode="false" />
            <asp:BoundField DataField="status" HeaderText="Status" />
        </Columns>
        <PagerSettings PageButtonCount="10" />
        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" BackColor="#666666" ForeColor="black" />
    </asp:GridView>
    <%--    <asp:GridView ID="grd_collector" ShowHeaderWhenEmpty="true" runat="server" AllowPaging="false"
        AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover table-dark"
        HeaderStyle-CssClass="GridHeader" ShowFooter="false">
        <PagerStyle CssClass="pagination-ys" />
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Collector Name" />
            <asp:BoundField DataField="Collector" HeaderText="Collector Monile No" />
            <asp:BoundField DataField="bestperformedweek" HeaderText="Best Performing Week" />
            <asp:BoundField DataField="transactions" DataFormatString="{0:C}" HeaderText="(₦)  Transactions" HtmlEncode="false" />
            <asp:BoundField DataField="Current_Week" HeaderText="Current Week" />
            <asp:BoundField DataField="transactions_current_week" DataFormatString="{0:C}" HeaderText="(₦) Transaction" HtmlEncode="false" />
            <asp:BoundField DataField="status" HeaderText="Status" />
        </Columns>
    </asp:GridView>--%>
    <div style="margin-top: 60px; margin-left: 10px; display: inline;" id="div1" runat="server">
        <%-- <a>
                <svg onclick='basicPopup()' class="Icon Icon-downarrow tether-target tether-target-attached-center tether-element-attached-right tether-element-attached-bottom tether-target-attached-top" viewBox="0 0 32 32" width="16" height="16" fill="currentcolor" title="Download this data" name="downarrow" size="16">
                    <path d="M12.2782161,19.3207547 L12.2782161,0 L19.5564322,0 L19.5564322,19.3207547 L26.8346484,19.3207547 L15.9173242,32 L5,19.3207547 L12.2782161,19.3207547 Z"></path></svg></a>--%>


        <div class='pagination-container'>

            <ul class="pagination">

                <li data-page="prev">
                    <span>< <span class="sr-only">(current)</span></span>
                </li>
                <!--	Here the JS Function Will Add the Rows -->
                <li data-page="next" id="prev">
                    <span>> <span class="sr-only">(current)</span></span>
                </li>
            </ul>

        </div>
    </div>
    <br />

    <header class="h3 p4 mr4">Monthly Filter</header>
    <div style="padding-left: 10px">
        <table class="border" style="width: 100%">
            <tr>
                <td>
                    <span class="hidden-xs" style="background-color: #fff; color: #4cae4c; padding: 10px;">
                        <asp:Label runat="server" ID="Selectedtoo"></asp:Label>
                    </span>
                </td>
                <td style="width: 110px;">PICK MONTH</td>
                 <td>
                        <span class="hidden-xs" style="background-color: #fff; color: #4cae4c; padding: 10px;">
                            <asp:Label runat="server" ID="Label2"></asp:Label>
                        </span>
                    </td>
                <td>
                    <div class='input-group date' id='datetimepicker7' style="width: 160px; margin-right: -50px;">
                        <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Start Date" ID="TextBox1" class="form-control " />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                </td>

                <td colspan="3">
                    <asp:Button ID="Button3" Text="Search" runat="server" CssClass="btn btn-success" OnClick="btnmonth_search_Click" Visible="true" />
                </td>
            </tr>
        </table>
        <div id="selectedid2" class="width: 170px ;" align="right" style="margin-right: 15px;">
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control col-lg-4 align:right" Style="width: 20%">
                <asp:ListItem Text="Best Performance" Value="BestPerformer"></asp:ListItem>
                <asp:ListItem Text="Underperforming" Value="Underperforming"></asp:ListItem>
                <asp:ListItem Text="Within Threshold" Value="Threshold"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button runat="server" CssClass="btn btn-primary col-lg-2 align-start" Text="Filter Status " type="submit" ID="Button4" OnClick="btnMonthly_Click" />
        </div>
    </div>
    <br />




    <asp:GridView ID="grd_collector1" runat="server" AllowPaging="True" AllowSorting="True" ShowHeader="true"
        AutoGenerateColumns="False" PagerSettings-PageButtonCount="10" PageSize="10"
        CssClass="table table-striped table-bordered table-hover table-dark" HeaderStyle-CssClass="GridHeader" OnPageIndexChanging="grd_paginHandler" CellPadding="12" ForeColor="#333333" GridLines="Vertical">
        <AlternatingRowStyle BackColor="white" />
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Collector Name" />
            <asp:BoundField DataField="Collector" HeaderText="Collector Mob.No" />
            <asp:BoundField DataField="bestperformedmonth" HeaderText="Best Performing Month" />
            <asp:BoundField DataField="transactions" DataFormatString="{0:C}" HeaderText="(₦)  Transactions" HtmlEncode="false" />
            <asp:BoundField DataField="Current_month" HeaderText="Current Month" />
            <asp:BoundField DataField="transactions_current_month" DataFormatString="{0:C}" HeaderText="(₦) Transaction" HtmlEncode="false" />
            <asp:BoundField DataField="status" HeaderText="Status" />
        </Columns>
        <%-- <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" CssClass="GridHeader" Font-Bold="True" ForeColor="White" />--%>
        <PagerSettings PageButtonCount="10" />
        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" BackColor="#666666" ForeColor="black" />
        <%--  <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />--%>
        <%-- <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />--%>
    </asp:GridView>


    <%--    <asp:GridView ID="grd_collector1" ShowHeaderWhenEmpty="true" runat="server" AllowPaging="false"
        AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover table-dark"
        HeaderStyle-CssClass="GridHeader" ShowFooter="false">
        <PagerStyle CssClass="pagination-ys" />
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Collector Name" />
            <asp:BoundField DataField="Collector" HeaderText="Collector Mob.No" />
            <asp:BoundField DataField="bestperformedmonth" HeaderText="Best Performing Month" />
            <asp:BoundField DataField="transactions" DataFormatString="{0:C}" HeaderText="(₦)  Transactions" HtmlEncode="false" />
            <asp:BoundField DataField="Current_month" HeaderText="Current Month" />
            <asp:BoundField DataField="transactions_current_month" DataFormatString="{0:C}" HeaderText="(₦) Transaction" HtmlEncode="false" />
            <asp:BoundField DataField="status" HeaderText="Status" />
        </Columns>

    </asp:GridView>--%>


    <%--<div style="margin-top: 60px; margin-left: 10px; display: inline;" id="div_paging" runat="server">

        <div class='pagination-container'>

            <ul class="pagination">

                <li data-page="prev">
                    <span>< <span class="sr-only">(current)</span></span>
                </li>

                <li data-page="next" id="prev1">
                    <span>> <span class="sr-only">(current)</span></span>
                </li>
            </ul>

        </div>
    </div>--%>
</asp:Content>
