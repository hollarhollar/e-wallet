<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="List_collectors_txn.aspx.cs" Inherits="Dashboard_List_collectors_txn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style>
        .GridBaseStyle td{
            padding: 10px;
            border: solid 1px #737272;
            width: 60px;
            height: 31px;
        }
      .pagination-ys table > tbody > tr > td {
         display: inline-block !important;
         padding-bottom: 45px;
        }
   
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-9" style="width: 100% !important;">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Transaction History - Collectors List</h3>
            </div>


            <div style="overflow: scroll; padding: 10px !important">
                <table style="width: 1000px">
                    <tr>
                        <td>
                            <asp:GridView ID="grd_list_Collectors" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" PagerSettings-PageButtonCount="5" OnPageIndexChanging="grd_paginHandler"
                                CssClass="GridBaseStyle" HeaderStyle-CssClass="GridHeader" Width="920px">
                                <Columns>
                                    <asp:BoundField DataField="mobile_no" HeaderText="Mobile No." />
                                    <asp:BoundField DataField="first_name" HeaderText="First Name" />
                                    <asp:BoundField DataField="last_name" HeaderText="Last Name" />
                                    <asp:BoundField DataField="designation" HeaderText="Designation" />
                                    <asp:BoundField DataField="address" HeaderText="Address" />
                                    <asp:BoundField DataField="EntityId" HeaderText="Entity Id" />
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <button type="button" class="btn btn-theme btn-xs md-skip dropdown-toggle" data-toggle="dropdown" style="margin-left: 40%;" aria-haspopup="true" aria-expanded="false">
                                                    Action <span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu">
                                                    <li>
                                                        <asp:LinkButton PostBackUrl='<%#"~/Dashboard/TxnHistoryAPI.aspx?mobile="+Eval("mobile_no")+""%>' runat="server" ID="lnktxn"> Transaction History </asp:LinkButton>
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

