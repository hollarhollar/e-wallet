<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="PaymentList.aspx.cs" Inherits="Dashboard_PaymentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .GridBaseStyle td{
            padding: 10px;
            border: solid 1px #737272;
            width: 60px;
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-md-9" style="width: 100% !important;">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Payment History and  Collector's Balance </h3>
            </div>

            <div>
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
                        <td>
                            <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="Collector" CssClass="form-control"  AutoPostBack="true" OnTextChanged="getCollectorList_Click" Style="margin-bottom: -30px; width:250px; margin-left: 270px;"></asp:DropDownList>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </div>

            <div style="overflow-y: auto; padding: 10px !important;">
                <table style="margin-bottom: 50.7px;">
                    <tr>
                        <td>
                            <asp:GridView ID="grd_list_Collectors" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" PagerSettings-PageButtonCount="5" OnPageIndexChanging="grd_paginHandler"
                                CssClass="GridBaseStyle" HeaderStyle-CssClass="GridHeader">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Collector Name" />
                                    <asp:BoundField DataField="CollectorId" HeaderText="Collector Id" />
                                    <asp:BoundField DataField="totalFunded" HeaderText="(₦) Total Funded"   DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false"  />
                                    <asp:BoundField DataField="totalTransAmount" HeaderText="(₦)  Transaction"  DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false"  />
                                    <asp:BoundField DataField="totalPayment" HeaderText="(₦) Total Payment"   DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false"  />
                                    <asp:BoundField DataField="WalletBalance" HeaderText="(₦) Wallet Balance"   DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false"  />
                                    <asp:BoundField DataField="diffAmount" HeaderText="(₦) Difference (Total transaction - total Payment)"  DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false"  />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <button type="button" class="btn btn-theme btn-xs md-skip dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    Action <span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu">
                                                    <%-- <li>
                                                        <asp:LinkButton PostBackUrl= '<%#"~/Dashboard/WalletBalance.aspx?mobileNo="+Eval("mobile_no")+" &EntityId="+Eval("EntityId")+""%>' runat="server" ID="lnkDetails"> Wallet Balance </asp:LinkButton>
                                                    </li>--%>

<%--                                                    <li>
                                                        <asp:LinkButton PostBackUrl='<%#"~/Dashboard/BalanceTransferWallet.aspx?mobileNo="+Eval("mobile_no")+" &EntityId="+Eval("EntityId")+""%>' runat="server" ID="LinkButton1"> Fund Transfer </asp:LinkButton>
                                                    </li>--%>
                                                    <li>
                                                        <asp:LinkButton PostBackUrl='<%#"~/Dashboard/PaymentListII.aspx?mobile="+Eval("CollectorId")+" &EntityId="+Eval("CollectorId")+""%>' runat="server" ID="LinkButton1">Payment History </asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

