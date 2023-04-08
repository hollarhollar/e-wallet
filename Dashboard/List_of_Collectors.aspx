<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="List_of_Collectors.aspx.cs" Inherits="Dashboard_List_of_Collectors" UICulture="en-NG" Culture="en-NG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .GridBaseStyle td{
            padding: 10px;
            border: solid 1px #737272;
            width: 60px;
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div class="col-md-9" style="width:100% !important;">
        <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Fund Collectors Wallet</h3>
            </div>


             <div style="overflow-y: auto; padding: 10px !important;">
                <table>
                    <tr>
                        <td>

                            <asp:GridView ID="grd_list_Collectors" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" PagerSettings-PageButtonCount="5" OnPageIndexChanging="grd_paginHandler"
                                CssClass="GridBaseStyle" HeaderStyle-CssClass="GridHeader">


                                <Columns>

                                    <asp:BoundField DataField="mobile_no" HeaderText="Mobile No." />
                                    <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                    <asp:BoundField DataField="EntityId" HeaderText="Entity Id" />
                                    <asp:BoundField DataField="LastFunded" HeaderText="Total Amount Funded" DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false" />
                                    <asp:BoundField DataField="TotalTransaction" HeaderText="Total Transaction" DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false" />
                                    <asp:BoundField DataField="WalletBalance" HeaderText="Wallet Balance" DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false"/>

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

                                                    <li>
                                                        <asp:LinkButton PostBackUrl='<%#"~/Dashboard/BalanceTransferWallet.aspx?mobileNo="+Eval("mobile_no")+" &EntityId="+Eval("EntityId")+""%>' runat="server" ID="LinkButton1"> Fund Transfer </asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton PostBackUrl='<%#"~/Dashboard/FundWalletHistory.aspx?mobile="+Eval("mobile_no")+" &EntityId="+Eval("EntityId")+""%>' runat="server" ID="LinkButton2"> Fund Wallet History </asp:LinkButton>
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

