<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="CollectionbyRevenueStream.aspx.cs" Inherits="Dashboard_CollectionbyRevenueStream" %>

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
                <h3 class="box-title">Collection by Revenue Stream</h3>
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
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="L.G.A"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="lga" CssClass="form-control"  AutoPostBack="true" OnTextChanged="getLGAList_Click" Style="margin-bottom: -30px; width:360px"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="overflow: scroll; padding: 10px !important">
                <table style="width: 1000px">
                    <tr>

                        <td>
                            <asp:GridView ID="grd_list_Collectors" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" PagerSettings-PageButtonCount="5" OnPageIndexChanging="grd_paginHandler"
                                CssClass="GridBaseStyle" HeaderStyle-CssClass="GridHeader" Width="920px">
                                <Columns>
                                    <asp:BoundField DataField="category" HeaderText=" Revenue Stream" />
                                    <asp:BoundField DataField="amount" DataFormatString="{0:0.00###}"  HeaderText="(₦) Total collection" />
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

