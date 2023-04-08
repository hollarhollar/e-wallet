<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="UnderPayment.aspx.cs" Inherits="Dashboard_UnderPayment" %>

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
                <h3 class="box-title">Under Payment </h3>
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
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="Collector" CssClass="form-control"  AutoPostBack="true" OnTextChanged="getCollectorList_Click" Style="margin-bottom: -30px; width:250px;margin-left:310px"></asp:DropDownList>
                        </td>
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
                                CssClass="GridBaseStyle" HeaderStyle-CssClass="GridHeader" onrowdatabound="grd_list_Collectors_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Collector Name" />
                                    <asp:BoundField DataField="CollectorId" HeaderText="CollectorId"/>
                                    <asp:BoundField DataField="totalFunded" HeaderText=" (₦) Total Funded"  DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false" />
                                    <asp:BoundField DataField="totalTransAmount" HeaderText="(₦) Total transaction"  DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false" />
                                    <asp:BoundField DataField="totalPayment"  HeaderText="(₦) Total Payment"  DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false" />
                                    <asp:BoundField DataField="WalletBalance" HeaderText="(₦) Wallet Balance"  DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false" />
                                    <asp:BoundField DataField="diffAmount" HeaderText="(₦) Difference (Total transaction - total Payment)"   DataFormatString="<span>&#8358;</span> {0:NG}" HtmlEncode="false" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <button type="button" class="btn btn-theme btn-xs md-skip dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    Action <span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu">
                                                    <li>
                                                        <asp:LinkButton ID="btn_disablecolletor" runat="server" CommandArgument='<%#Eval("CollectorId")%>' OnClick="DisableColletor">Disable  Collector</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="btn_sweepbalance" runat="server" CommandArgument='<%#Eval("CollectorId")%>' OnClick="SweepColletorBalance">Sweep  Wallet Balance</asp:LinkButton>
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

