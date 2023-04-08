<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Dashboard_Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
</asp:Content>--%>


<asp:Content ID="Content4" ContentPlaceHolderID="contentheading" runat="Server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/css/bootstrap-datepicker3.standalone.min.css" />
    <script
        src="https://code.jquery.com/jquery-2.2.4.min.js"
        integrity="sha256-BbhdlvQf/xTY9gja0Dq3HiwQF8LaCRTXxZKRutelT44="
        crossorigin="anonymous">
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>

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
    </script>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker5').datepicker({
                dateFormat: 'mm/dd/yyyy',
                timeFormat: 'hh:mm:ss',
                autoclose: true
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            });
        });
        $(function () {
            $('#datetimepicker2').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            });
        });

        function getval(sel) {
            debugger
            var x = document.getElementById("form33");
            var y = document.getElementById("form34");

            if (sel.value === "1") {
                x.style.display = "block";
                y.style.display = "none";
            } else {
                x.style.display = "none";
                y.style.display = "block";
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-left: 10px;" />
    <div class="box-body">
        <div class="paymentContainer" style="width: 68%">
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Collector Name">Collector *</asp:Label>
                <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="collectorList" CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <select onchange="getval(this);" cssclass="form-control">
                    <option value="0">Select Payment Duration</option>
                    <option value="1">Daily</option>
                    <option value="2">Date Range</option>
                </select>
                <%--<asp:DropDownList runat="server" AppendDataBoundItems="true" AutoPostBack="true" ID="periodList" CssClass="form-control">
                    <asp:ListItem Text="Select Duration" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Daily" Value="D"></asp:ListItem>
                    <asp:ListItem Text="Date Range" Value="R"></asp:ListItem>
                </asp:DropDownList>--%>
            </div>


            <div class="form-group" id="form33" hidden="hidden">
                <asp:Label ID="Label3" runat="server" Text="Funded Date">Transaction Date *</asp:Label>

                <div class='input-group date' id='datetimepicker1'>
                    <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Enter Trans Date" ID="txt_dob" class="form-control" AutoPostBack="true" OnTextChanged="getAmountList_Click" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </div>
            <div class="form-group" id="form34" hidden="hidden">
                <table class="table borderless" style="width: 100% !important; border: none !important;">
                    <tr>
                        <%--<td>
                            <span class="hidden-xs" style="background-color: #fff; color: #4cae4c; padding: 10px;">
                                <asp:Label runat="server" ID="selectedDate"></asp:Label>
                            </span>
                        </td>--%>
                        <td style="width: 110px;">START DATE</td>
                        <td>
                            <div class='input-group date' id='datetimepicker5' style="width: 160px; margin-right: -50px;">
                                <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Start Date" ID="txt_startDate" class="form-control"  />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </td>
                        <td style="width: 110px;">END DATE</td>
                        <td>
                            <div class='input-group date' id='datetimepicker6' style="width: 160px; margin-right: -50px;">
                                <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="End Date" ID="txt_endDate" class="form-control"  />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </td>
                        <td colspan="3">
                            <asp:Button ID="btn_search" Text="Search" runat="server" CssClass="btn btn-success" OnClick="btn_searchamount_Click" Visible="true" />
                        </td>
                        <td>
                            <%--<asp:Button ID="btn_searchbyDate" Text="Search by Date" runat="server" CssClass="btn btn-primary" OnClick="btn_search_By_Date_Click" Visible="true" />--%>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <%-- <asp:Label ID="Label3a" runat="server" Text="Funded Date">Start Date *</asp:Label>

                <div class='input-group date' id='datetimepicker5'>
                    <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Enter Start Date" ID="txt_start_dob" class="form-control" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div> 
                <asp:Label ID="Label3b" runat="server" Text="Funded Date">End Date *</asp:Label>

                <div class='input-group date' id='datetimepicker6'>
                    <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Enter Enter Date" ID="txt_end_dob" class="form-control" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
                --%>
            </div>

            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Fund Transfer Amount">Transaction Amount To Pay*</asp:Label>
                <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="amountList" CssClass="form-control" disabled="disabled" />
            </div>

            <div class="form-group">Amount
                <asp:Label ID="lbl_mobile" runat="server" Text="Amount to Pay"> to pay *</asp:Label>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txt_amountPaid" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"> </asp:RegularExpressionValidator>
                <asp:TextBox runat="server" ID="txt_amountPaid" placeholder="Enter amount you wish to pay" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="lbl_receiptNumber" runat="server" Text="Receipt Number"> Receipt number *</asp:Label>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txt_receiptNumber" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"> </asp:RegularExpressionValidator>
                <asp:TextBox runat="server" ID="txt_receiptNumber" placeholder="Receipt number" CssClass="form-control"></asp:TextBox>
            </div>

            <asp:Label ID="lbl_dob" runat="server" Text="Payment date">Payment date *</asp:Label>
            <div class='input-group date' id='datetimepicker2'>
                <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Payment date" ID="txt_paymentDate" class="form-control"  />
                <span class="input-group-addon">
                    <span class="glyphicon glyphicon-calendar"></span>
                </span>
            </div>
            <br />
            <div class="col-md-10" align="center">
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Submit" type="submit" ID="btnSubmit" OnClick="btnSubmit_Click" />
            </div>


            <asp:GridView ID="grd_collector" ShowHeaderWhenEmpty="true" runat="server" AllowPaging="false"
                AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered table-hover table-dark"
                HeaderStyle-CssClass="GridHeader" ShowFooter="false">
                <PagerStyle CssClass="pagination-ys" />
                <Columns>
                </Columns>

            </asp:GridView>


            <%--<div style="margin-top: 60px; margin-left: 10px; display: inline;" id="div_paging" runat="server">
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
            </div>--%>
        </div>
    </div>
    <!--  <iframe    src="http://reports.nownowpay.ng/public/question/c5bee7da-4892-455c-867a-8641a7078137"    frameborder="0"    width="1100"    height="500"    allowtransparency></iframe>-->

</asp:Content>

