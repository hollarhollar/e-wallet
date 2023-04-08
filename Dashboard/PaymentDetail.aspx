<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="PaymentDetail.aspx.cs" Inherits="Dashboard_PaymentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .GridBaseStyle td {
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
                <h3 class="box-title">Payment Details </h3>
            </div>

            <div style="overflow-y: auto; padding: 10px !important;">
                <table style="margin-bottom: 50.7px;">
                    <tr>
                        <td>
                            <asp:GridView ID="grd_list_Collectors" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" PagerSettings-PageButtonCount="5" OnPageIndexChanging="grd_list_Collectors_PageIndexChanging"
                                CssClass="GridBaseStyle" HeaderStyle-CssClass="GridHeader">
                                <Columns>
                                    <asp:BoundField DataField="created_at" HeaderText="Trans Date" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="TransId" HeaderText="Trans Id" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Amount" HeaderText="Trans Amount" ItemStyle-HorizontalAlign="Center"  />
                                    <asp:BoundField DataField="AmountPaid" HeaderText="Amount Paid" ItemStyle-HorizontalAlign="Center"  />
                                    <asp:BoundField DataField="diffAmount" HeaderText="Balance" ItemStyle-HorizontalAlign="Center"  />
                                    <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt No" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" ItemStyle-HorizontalAlign="Center" />

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

