<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" Culture="en-NG" UICulture="en-NG" AutoEventWireup="true" CodeFile="PaymentListII.aspx.cs" Inherits="Dashboard_PaymentListII" %>

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
                <h3 class="box-title">Payment History</h3>
            </div>

            <div style="overflow-y: auto; padding: 10px !important;">
                <table style="margin-bottom: 50.7px;">
                    <tr>
                        <td>
                            <asp:GridView ID="grd_list_Collectors" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" PagerSettings-PageButtonCount="5" OnPageIndexChanging="grd_paginHandler"
                                CssClass="GridBaseStyle" HeaderStyle-CssClass="GridHeader">
                                <Columns>
                                    <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt No" />
                                    <asp:BoundField DataField="DebittedAmount" HeaderText="(₦) Amount Paid" DataFormatString="{0:C}" HtmlEncode="false"  />
                                    <asp:BoundField DataField="ReceiverNumber" HeaderText="Recieved By" />
                                    <asp:BoundField DataField="TransactionDate" HeaderText="Payment Date" />
                                    <%--<asp:BoundField DataField="totalPayment" HeaderText="Total Payment" />
                                    <asp:BoundField DataField="WalletBalance" HeaderText="Wallet Balance" />--%>
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
                                                        <asp:LinkButton PostBackUrl='<%#"~/Dashboard/PaymentDetail.aspx?mobile="+Eval("ReceiptNumber")+""%>' runat="server" ID="LinkButton1">Payment History </asp:LinkButton>
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

