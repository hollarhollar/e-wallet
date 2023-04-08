<%@ Page Language="C#" MasterPageFile="~/Dashboard/MasterPageTwo.master" UICulture="en-NG" Culture="en-NG" AutoEventWireup="true" CodeFile="RevenueBeatReportView.aspx.cs" Inherits="Dashboard_RevenueBeatReportView" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        $(function () {
            $('#datetimepicker2').datepicker({
                dateFormat: 'mm/dd/yyyy',
                timeFormat: 'hh:mm:ss',
                autoclose: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
    <style>
    .button-style {
        font-size: 100%;
        -webkit-appearance: none;
        margin: 0;
        outline: none;
        display: inline-block;
        box-sizing: border-box;
        text-decoration: none;
        padding: 8px 12px;
        padding: 0.5rem 0.75rem;
        background: #fbfcfd;
        border: 1px solid #ddd;
        color: #727479;
        cursor: pointer;
        text-decoration: none;
        font-weight: bold;
        font-family: 'Lato', sans-serif;
        border-radius: 4px;
    }
</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="box-header with-border">
        <h3 class="box-title">Revenue Beat Report:<asp:Label runat="server" Text="0" ID="lblCollector"></asp:Label></h3>
    </div>
    <div style="padding-left: 10px;">
        <div style="padding-left: 10px">
            <div style="text-align: center;">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="downloadCSV" CssClass="button-style">DOWNLOAD CSV</asp:LinkButton>
                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" CssClass="button-style">DOWNLOAD EXCEL</asp:LinkButton>
            </div>
            <table class="border" style="width: 100%">
                <tr>
                    <td>
                        <span class="hidden-xs" style="background-color: #fff; color: #4cae4c; padding: 10px;">
                            <asp:Label runat="server" ID="selectedDate"></asp:Label>
                        </span>
                    </td>
                    <td style="width: 110px;">START DATE</td>
                    <td>
                        <div class='input-group date' id='datetimepicker1' style="width: 160px; margin-right: -50px;">
                            <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Start Date" ID="txt_startDate" class="form-control required" />
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </td>
                    <td style="width: 110px;">END DATE</td>
                    <td>
                        <div class='input-group date' id='datetimepicker2' style="width: 160px; margin-right: -50px;">
                            <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="End Date" ID="txt_endDate" class="form-control required" />
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
        </div>
        <asp:GridView ID="grd_collector" ShowHeaderWhenEmpty="true" runat="server" AllowPaging="false"
            AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover table-dark"
            HeaderStyle-CssClass="GridHeader" ShowFooter="false">
            <PagerStyle CssClass="pagination-ys" />

            <Columns>
                <asp:BoundField DataField="location" HeaderText="Revenue Beat" />
                <asp:BoundField DataField="lga" HeaderText="LGA" />
                <asp:BoundField DataField="amount" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦) Amount" />
                <asp:BoundField DataField="OffLine_Trans_Date" HeaderText="Transaction Date" />

            </Columns>

        </asp:GridView>
        <div style="padding: inherit">
            <br />
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClientClick="javascript:history.back(1); return false;" />
        </div>


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
    <!--  <iframe    src="https://reports.nownowpay.ng/public/question/c5bee7da-4892-455c-867a-8641a7078137"    frameborder="0"    width="1100"    height="500"    allowtransparency></iframe>-->


    <%-- <span data-reactroot="" style="box-sizing: border-box; z-index: 4;">
        <div id="popuArrow" class="PopoverBody PopoverBody--withArrow" style="box-sizing: border-box; pointer-events: auto; min-width: 1em; border: 1px solid #ddd; box-shadow: 0 4px 10px rgba(0, 0, 0, .1); background-color: #fff; border-radius: 4px; position: absolute; margin-top: -220px; margin-left: 10px; display: -webkit-box; display: -ms-flexbox; display: none; -webkit-box-orient: vertical; -webkit-box-direction: normal; -ms-flex-direction: column; flex-direction: column; overflow: hidden; max-width: 270px;">
            <div class="p2" style="box-sizing: border-box; padding: 16px; padding: 1rem; max-width: 250px;">
                <h4 style="font-weight: 700; margin-top: 0; margin-bottom: 0;">Download full results</h4>
                <div class="flex flex-row mt2" style="box-sizing: border-box; display: -webkit-box; display: -ms-flexbox; display: flex; -webkit-box-orient: horizontal; -webkit-box-direction: normal; -ms-flex-direction: row; flex-direction: row; margin-top: 16px; margin-top: 1rem;">
                </div>
            </div>
        </div>
    </span>--%>
</asp:Content>

