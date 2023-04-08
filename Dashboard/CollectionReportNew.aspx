﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="CollectionReportNew.aspx.cs" Inherits="Dashboard_CollectionReportNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/css/bootstrap-datepicker3.standalone.min.css" />
    <script
        src="https://code.jquery.com/jquery-2.2.4.min.js"
        integrity="sha256-BbhdlvQf/xTY9gja0Dq3HiwQF8LaCRTXxZKRutelT44="
        crossorigin="anonymous">
    </script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/js/bootstrap.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datepicker({
                dateFormat: 'mm/dd/yyyy',
                timeFormat: 'hh:mm:ss',
                autoclose: true
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker2').datepicker({
                dateFormat: 'mm/dd/yyyy',
                timeFormat: 'hh:mm:ss',
                autoclose: true
            });
        });
    </script>
    <script>
        $("#summaryBtn").click(function () {
            $("#summary").toggle();
        });
    </script>
    <%--    <script>  
        $(function () {
            $("#datepick-1").datepicker({
                appendText: "(yy-mm-dd)",
                dateFormat: "yy-mm-dd",
                altField: "#datepick-2",
                altFormat: "DD, d MM, yy",
                form
            });
        });
    </script> 
    --%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="box-header with-border">
        <h3 class="box-title">Collectors Report</h3>
    </div>
    <div style="padding-left: 10px;">
        <table class="table borderless" style="width: 100% !important; border: none !important;">
            <tr>
                <td>
                    <span class="hidden-xs" style="background-color: #fff; color: #4cae4c; padding: 10px;">
                        <asp:Label runat="server" ID="selectedDate"></asp:Label>
                    </span>
                </td>
                <td style="width: 110px;">START DATE</td>
                <td>
                    <div class='input-group date' id='datetimepicker1' style="width: 160px; margin-right: -50px;">
                        <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Start Date" ID="txt_startDate" class="form-control" required />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                </td>
                <td style="width: 110px;">END DATE</td>
                <td>
                    <div class='input-group date' id='datetimepicker2' style="width: 160px; margin-right: -50px;">
                        <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="End Date" ID="txt_endDate" class="form-control" required />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:Button ID="btn_search" Text="Search" runat="server" CssClass="btn btn-success" OnClick="btn_search_Click" Visible="true" />
                </td>
                <td>
                    <%--<asp:Button ID="btn_searchbyDate" Text="Search by Date" runat="server" CssClass="btn btn-primary" OnClick="btn_search_By_Date_Click" Visible="true" />--%>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>

        <asp:GridView ID="grd_collector" ShowHeaderWhenEmpty="true" runat="server" AllowPaging="false"
            AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover table-dark"
            HeaderStyle-CssClass="GridHeader" ShowFooter="false">
            <PagerStyle CssClass="pagination-ys" />

            <Columns>
                <asp:BoundField DataField="name" HeaderText="collector name" />
                <asp:BoundField DataField="collectorid" HeaderText="collector id" />
                <asp:BoundField DataField="totaltransamount" DataFormatString="{0:0.00###}" HeaderText="(₦) total collection" />
                <asp:TemplateField HeaderText="actions">
                    <ItemTemplate>
                        <div class="btn-group">
                            <button type="button" id="summarybtn" role="button" class="btn btn-theme btn-xs md-skip dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                action <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu">
                                <li>
                                    <a href='<%#"~/Dashboard/CollectorReportView.aspx?mobileNo="+Eval("CollectorId")+" &CollectorId="+Eval("CollectorId")+""%>'  onclick="summaryLink_Click"  runat="server" id="summaryLink">View Summary</a>
                                </li>
                            </ul>     
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <%-- <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Collector Name" />
                                    <asp:BoundField DataField="CollectorId" HeaderText="Collector ID"/>
                                    
                                    <asp:BoundField DataField="totalTransAmount" DataFormatString="{0:0.00###}" HeaderText="Total Collection" />
                                 
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <button type="button" class="btn btn-theme btn-xs md-skip dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    Action <span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu">
                                                    <li>
                                                        <asp:LinkButton ID="btn_disablecolletor" runat="server" CommandArgument='<%#Eval("CollectorId")%>' OnClick="TheMethodII">View Summary</asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>--%>
            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
        </asp:GridView>

        <div style="margin-top: 60px; margin-left: 10px; display: inline;" id="div_paging" runat="server">

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
    </div>

</asp:Content>

