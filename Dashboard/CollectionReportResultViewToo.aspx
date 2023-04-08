<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Dashboard/MasterPage.master" EnableEventValidation="false" CodeFile="CollectionReportResultViewToo.aspx.cs" Inherits="Dashboard_CollectionReportResultViewToo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/js/bootstrap.js"></script>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="box-header with-border">
        <h3 class="box-title">Transaction History</h3>
    </div>
    <div style="padding-left: 10px">
        <table class="border" style="width: 900px; height: 45px;">
            <tr>
                <td class="auto-style1">Transaction History: </td>
                <td></td>
                <td>
                    <asp:Label ID="lblCollectorId" runat="server" CssClass="bg-red-pink"></asp:Label>
                </td>
                <td>
                    <span class="hidden-xs" style="background-color: #fff; color: #4cae4c; padding: 10px;">
                        <asp:Label runat="server" ID="selectedDate"></asp:Label>
                    </span>
                </td>
                <td style="width: 110px;">START DATE</td>
                <td>
                    <%--  <div class='input-group date' id='datetimepicker1' style="width: 160px; margin-right: -50px;">
                        <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Start Date" ID="txt_startDate" class="form-control required" />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>--%>
                    <div class='input-group date' id='datetimepicker5' style="width: 160px; margin-right: -50px;">
                        <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="start Date" ID="txt_startDate" class="form-control" />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                </td>
                <td style="width: 150px;">END DATE</td>
                <td>
                    <div class='input-group date' id='datetimepicker6' style="width: 160px; margin-right: -50px;">
                        <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="End Date" ID="txt_endDate" class="form-control " />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </span>
                    </div>
                </td>
                <td style="width: 110px;">Filter:</td>
                <td>
                    <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="types" CssClass="form-control" AutoPostBack="true" Style="width: 160px; margin-right: -50px;" Visible="true">
                        <asp:ListItem Text="All" Value="(39, 40, 42, 43, 44, 45)"></asp:ListItem>
                        <asp:ListItem Text="Harmonized Tricycle" Value="45"></asp:ListItem>
                        <asp:ListItem Text="Harmonized Buses" Value="40"></asp:ListItem>
                        <asp:ListItem Text="Harmonized Taxi" Value="43"></asp:ListItem>
                        <asp:ListItem Text="Buses" Value="39"></asp:ListItem>
                        <asp:ListItem Text="Taxi" Value="42"></asp:ListItem>
                        <asp:ListItem Text="Tricycle" Value="44"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="3">
                    <asp:Button ID="btn_search" Text="Filter" runat="server" CssClass="btn btn-success" OnClick="btn_filter_Click" Visible="true" />
                </td>
                <td colspan="3">
                    <asp:Button ID="btn_clear" Text="Search" runat="server" CssClass="btn btn-danger" OnClick="btn_search_Click" Visible="true" />
                </td>
                <td>
                    <a>
                        <svg onclick='basicPopup()' class="Icon Icon-box tether-target tether-target-attached-center tether-element-attached-right tether-element-attached-bottom tether-target-attached-top" viewBox="0 0 32 32" width="36" height="36" fill="currentcolor" title="Download this data" name="downbox" size="30">
                            <path d="M12.2782161,19.3207547 L12.2782161,0 L19.5564322,0 L19.5564322,19.3207547 L26.8346484,19.3207547 L15.9173242,32 L5,19.3207547 L12.2782161,19.3207547 Z"></path>
                        </svg>
                    </a>


                </td>
            </tr>
        </table>
    </div>

    <contenttemplate>

        <%--      <div id="divresult" runat="server" style="display: none;">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Transaction History :<asp:Label runat="server" Text="0" ID="lblCollector"></asp:Label></h3>
                </div>--%>
        <div class="box box-body">
            <div id="div_res" runat="server">
                <div style="overflow-y: auto;">

                    <asp:GridView ID="grd_collector" runat="server" AllowPaging="True" AllowSorting="True" ShowHeader="true"
                        AutoGenerateColumns="False" PagerSettings-PageButtonCount="5" PageSize="10"
                        CssClass="table table-striped table-bordered table-hover table-dark" HeaderStyle-CssClass="GridHeader" OnPageIndexChanging="grd_paginHandler" CellPadding="12" ForeColor="#333333" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="white" />
                        <Columns>
                            
                            <asp:BoundField DataField="TransId" HeaderText="Transaction ID" />
                            <asp:BoundField DataField="category" HeaderText="Category" />
                            <asp:BoundField DataField="sub_category" HeaderText="Sub Category" />
                            <asp:BoundField DataField="Amount" DataFormatString="{0:0.00###}" HeaderText="(₦) Amount" />
                            <asp:BoundField DataField="OffLine_Trans_Date" HeaderText="Transaction Date" />
                            <asp:BoundField DataField="AssetId" HeaderText="Asset ID" />
                        </Columns>
                        <%-- <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" CssClass="GridHeader" Font-Bold="True" ForeColor="White" />--%>
                        <PagerSettings PageButtonCount="5" />
                        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" BackColor="#666666" ForeColor="black" />
                        <%--  <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />--%>
                        <%-- <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />--%>
                    </asp:GridView>
                </div>
            </div>
            <br />
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClientClick="javascript:history.back(1); return false;" />
        </div>
        <%--       </div>
        </div>--%>
    </contenttemplate>

    <%--    <asp:GridView class="padding-top:30%" ID="grd_collector" ShowHeaderWhenEmpty="True" runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-striped table-bordered table-hover table-dark"
        HeaderStyle-CssClass="GridHeader">
        <PagerStyle CssClass="pagination-ys" />
        <Columns>
            <asp:BoundField DataField="TransId" HeaderText="Transaction ID" />
            <asp:BoundField DataField="category" HeaderText="Category" />
            <asp:BoundField DataField="sub_category" HeaderText="Sub Category" />
            <asp:BoundField DataField="Amount" DataFormatString="{0:0.00###}" HeaderText="(₦) Amount" />
            <asp:BoundField DataField="OffLine_Trans_Date" HeaderText="Transaction Date" />
            <asp:BoundField DataField="AssetId" HeaderText="Asset ID" />
        </Columns>
         <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
    </asp:GridView>

    <div style="margin-top: 60px; margin-left: 10px; display: inline;" id="div_paging" runat="server">
            <a>
                <svg onclick='basicPopup()' class="Icon Icon-downarrow tether-target tether-target-attached-center tether-element-attached-right tether-element-attached-bottom tether-target-attached-top" viewBox="0 0 32 32" width="16" height="16" fill="currentcolor" title="Download this data" name="downarrow" size="16">
                    <path d="M12.2782161,19.3207547 L12.2782161,0 L19.5564322,0 L19.5564322,19.3207547 L26.8346484,19.3207547 L15.9173242,32 L5,19.3207547 L12.2782161,19.3207547 Z"></path></svg></a>


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
    </div>--%>

    <!--  <iframe    src="https://reports.nownowpay.ng/public/question/c5bee7da-4892-455c-867a-8641a7078137"    frameborder="0"    width="1100"    height="500"    allowtransparency></iframe>-->


    <span data-reactroot="" style="box-sizing: border-box; z-index: 4;">
        <div id="popuArrow" class="PopoverBody PopoverBody--withArrow" style="box-sizing: border-box; pointer-events: auto; min-width: 1em; border: 1px solid #ddd; box-shadow: 0 4px 10px rgba(0, 0, 0, .1); background-color: #fff; border-radius: 4px; position: absolute; margin-top: -220px; margin-left: 10px; display: -webkit-box; display: -ms-flexbox; display: none; -webkit-box-orient: vertical; -webkit-box-direction: normal; -ms-flex-direction: column; flex-direction: column; overflow: hidden; max-width: 270px;">
            <div class="p2" style="box-sizing: border-box; padding: 16px; padding: 1rem; max-width: 250px;">
                <h4 style="font-weight: 700; margin-top: 0; margin-bottom: 0;">Download full results</h4>
                <div class="flex flex-row mt2" style="box-sizing: border-box; display: -webkit-box; display: -ms-flexbox; display: flex; -webkit-box-orient: horizontal; -webkit-box-direction: normal; -ms-flex-direction: row; flex-direction: row; margin-top: 16px; margin-top: 1rem;">

                    <form class="mr1 text-uppercase text-default" method="GET" action="" style="box-sizing: border-box; color: #727479; margin-right: 8px; margin-right: 0.5rem; text-transform: uppercase; letter-spacing: 0.06em;">
                        <input type="hidden" name="parameters" value="[]" style="font-family: 'Lato', 'Helvetica Neue', Helvetica, sans-serif; box-sizing: border-box;">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="downloadCSV" CssClass="Button " Style="font-size: 100%; -webkit-appearance: none; margin: 0; outline: none; display: inline-block; box-sizing: border-box; text-decoration: none; padding: 8px 12px; padding: 0.5rem 0.75rem; background: #fbfcfd; border: 1px solid #ddd; color: #727479; cursor: pointer; text-decoration: none; font-weight: bold; font-family: 'Lato', sans-serif; border-radius: 4px;">
            
                            <div class="flex layout-centered" style="box-sizing: border-box; display: -webkit-box; display: -ms-flexbox; display: flex; -webkit-box-align: center; -ms-flex-align: center; align-items: center; -webkit-box-pack: center; -ms-flex-pack: center; justify-content: center;">
                           <%-- <svg class="Icon Icon-downarrow mr1" viewbox="0 0 32 32" width="14" height="14" fill="currentcolor" name="downarrow" size="14" style="margin-right: 8px; margin-right: 0.5rem;">--%>
                            <svg class="Icon Icon-box tether-target tether-target-attached-center tether-element-attached-right tether-element-attached-bottom tether-target-attached-top" viewBox="0 0 32 32" width="36" height="36" fill="currentcolor" title="Download this data" name="downbox" size="30">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <path d="M12.2782161,19.3207547 L12.2782161,0 L19.5564322,0 L19.5564322,19.3207547 L26.8346484,19.3207547 L15.9173242,32 L5,19.3207547 L12.2782161,19.3207547 Z"/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </svg><div style="box-sizing: border-box;">DOWNLOAD</div></div> </asp:LinkButton>
                    </form>
                </div>
            </div>
        </div>
    </span>



</asp:Content>
