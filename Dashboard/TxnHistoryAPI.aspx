<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" Culture="en-NG" UICulture="en-NG" CodeFile="TxnHistoryAPI.aspx.cs" Inherits="Dashboard_TxnHistoryAPI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
    Transaction History
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="divresult" runat="server" style="display: none;">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Transaction History :<asp:Label runat="server" Text="0" ID="lblCollector"></asp:Label></h3>
                    </div>
                    <div class="box box-body">
                        <div id="div_res" runat="server">
                            <div style="overflow-y: auto;">

                                <asp:GridView ID="grd_trans_history" runat="server" AllowPaging="True" AllowSorting="True" ShowHeader="true"
                                    AutoGenerateColumns="False" PagerSettings-PageButtonCount="5" PageSize="10"
                                    CssClass="GridBaseStyle" HeaderStyle-CssClass="GridHeader" OnPageIndexChanging="grd_trans_history_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="TransId" HeaderText="Transaction ID"  ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Trans_Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Amount" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦) Amount" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="created_at" HeaderText="Transaction Date" DataFormatString="{0:D}" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="closingBalance" HeaderText="Wallet Balance" DataFormatString="{0:c}" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="TaxPayerId" HeaderText="Tax Payer Id" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="TaxPayerType" HeaderText="Tax Payer Type" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="AssetId" HeaderText="Asset Id" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="asset_type" HeaderText="Asset Type" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" CssClass="GridHeader" Font-Bold="True" ForeColor="White" />
                                    <PagerSettings PageButtonCount="5" />
                                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" BackColor="#666666" ForeColor="White" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />

                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                        <button id="btnBack" onclick="history.go(-1)" class="btn btn-primary">Back</button>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

