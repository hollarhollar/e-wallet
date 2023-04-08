<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPageTwo.master" EnableEventValidation="false" UICulture="en-NG" Culture="en-NG" AutoEventWireup="true" CodeFile="RevenueBeatReport.aspx.cs" Inherits="Dashboard_RevenueBeatReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<%--    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/js/bootstrap.js"></script>
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
    </script>--%>
    <script>  
        $(function () {
            $('#datetimepicker1').datepicker(
                {
                    dateFormat: 'mm/dd/yy',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: '1950:2100'

                });
        })
        $(function () {
            $('#datetimepicker2').datepicker(
                {
                    dateFormat: 'mm/dd/yy',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: '1950:2100'

                });
        })
    </script>t>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="box-header with-border">
        <h3 class="box-title">Revenue Beat Report</h3>
    </div>
    <div style="padding-left: 10px;">
        <table class="table borderless" style="width: 100% !important; border: none !important;">
            <tr>

                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>Search:<asp:TextBox ID="txt_employer_RIN" runat="server" CssClass="form-control" placeholder="Search" AutoPostBack="true" OnTextChanged="btn_search_Click"></asp:TextBox></td>
                <td colspan="3" style="text-align: right;">
                    <asp:Button ID="btn_search" Text="Search" runat="server" CssClass="btn btn-theme" OnClick="btn_search_Click" Visible="false" /></td>
            </tr>
            <tr style="display: none;">
                <td>Employer Name:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txt_employer_name" runat="server" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr style="display: none;">
                <td>Employer TIN:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txt_employer_TIN" runat="server" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr style="display: none;">
                <td>Business RIN:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txt_business_RIN" runat="server" CssClass="form-control"></asp:TextBox></td>
            </tr>
        </table>
         <div style="padding-left:10px">
        <table class="border" style="width:100%">
            <tr>
                 <td>
                    <span class="hidden-xs" style="background-color: #fff; color: #4cae4c; padding: 10px;">
                        <asp:Label runat="server" ID="selectedDate"></asp:Label>
                    </span>
                </td>
                <td style="width: 110px;">START DATE</td>
                <td>
                    <div class='input-group date' id='datetimepicker1' style="width: 160px; margin-right: -50px;">
                        <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Start Date" ID="txt_startDate" class="form-control required"  />
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-calendar" style="left: -13px; top: -3px; width: 18px"></span>
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
                    <asp:Button ID="Button1" Text="Search" runat="server" CssClass="btn btn-success" OnClick="btn_search_ClickII" Visible="true" />
                     <asp:Button ID="Button2" Text="Clear" runat="server" CssClass="btn btn-danger" OnClick="basicPopup" Visible="true" />
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
                <asp:BoundField DataField="Id" HeaderText="id"  visible="false"/>
                <asp:BoundField DataField="location" HeaderText="Revenue Beat" />
                <asp:BoundField DataField="designation" HeaderText="Designation" Visible="false" />
                <asp:BoundField DataField="lga" HeaderText="LGA" />
                <asp:BoundField DataField="collector" HeaderText="Collector Name" />
                <asp:BoundField DataField="amount" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦) Revenue Collected" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <div class="btn-group">
                            <button type="button" class="btn btn-theme btn-xs md-skip dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                Action <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu">


                                <li>
                                    <asp:LinkButton ToolTip='<%#Eval("Id") %>' onclick="viewbutton_Click" runat="server" ID="viewbutton"> View Collections</asp:LinkButton>
                                    <%--<asp:LinkButton PostBackUrl='<%#"~/Dashboard/RevenueBeatReportView.aspx"%>' runat="server" ID="viewbutton"> View Collections</asp:LinkButton>--%>
                                </li>

                            </ul>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>


        <div style="margin-top: 60px; margin-left: 10px; display: inline;" id="div_paging" runat="server">
     <%--   <a>
                <svg onclick='basicPopup()' class="Icon Icon-downarrow tether-target tether-target-attached-center tether-element-attached-right tether-element-attached-bottom tether-target-attached-top" viewBox="0 0 32 32" width="16" height="16" fill="currentcolor" title="Download this data" name="downarrow" size="16">
                    <path d="M12.2782161,19.3207547 L12.2782161,0 L19.5564322,0 L19.5564322,19.3207547 L26.8346484,19.3207547 L15.9173242,32 L5,19.3207547 L12.2782161,19.3207547 Z"></path></svg>--%>


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

                    <form class="mr1 text-uppercase text-default" method="GET" action="" style="box-sizing: border-box; color: #727479; margin-right: 8px; margin-right: 0.5rem; text-transform: uppercase; letter-spacing: 0.06em;">
                        <input type="hidden" name="parameters" value="[]" style="font-family: 'Lato', 'Helvetica Neue', Helvet
   ica, sans-serif; box-sizing: border-box;">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="downloadCSV" CssClass="Button " Style="font-size: 100%; -webkit-appearance: none; margin: 0; outline: none; display: inline-block; box-sizing: border-box; text-decoration: none; padding: 8px 12px; padding: 0.5rem 0.75rem; background: #fbfcfd; border: 1px solid #ddd; color: #727479; cursor: pointer; text-decoration: none; font-weight: bold; font-family: 'Lato', sans-serif; border-radius: 4px;">
            
                            <div class="flex layout-centered" style="box-sizing: border-box; display: -webkit-box; display: -ms-flexbox; display: flex; -webkit-box-align: center; -ms-flex-align: center; align-items: center; -webkit-box-pack: center; -ms-flex-pack: center; justify-content: center;">
                            <svg class="Icon Icon-downarrow mr1" viewbox="0 0 32 32" width="14" height="14" fill="currentcolor" name="downarrow" size="14" style="margin-right: 8px; margin-right: 0.5rem;">
                            <path d="M12.2782161,19.3207547 L12.2782161,0 L19.5564322,0 L19.5564322,19.3207547 L26.8346484,19.3207547 L15.9173242,32 L5,19.3207547 L12.2782161,19.3207547 Z"/>
                            </svg><div style="box-sizing: border-box;">DOWNLOAD</div></div> </asp:LinkButton>
                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                    </form>
                </div>
            </div>
        </div>
    </span>--%>



</asp:Content>

