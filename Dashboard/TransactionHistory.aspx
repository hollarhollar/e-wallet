<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="TransactionHistory.aspx.cs" Inherits="Dashboard_TransactionHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
    Transaction History
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-md-6">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Search - Transaction History</h3>
                    </div>
                    <!-- /.box-header -->
                    <!-- form start -->

                    <div class="box-body">

                        <div class="form-group">
                            <asp:Label ID="lbl_taxpayer_rin" runat="server">TaxPayer RIN</asp:Label>
                            <asp:TextBox runat="server" ID="txt_taxpayerRIN" placeholder="Enter TaxPayer RIN" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <table style="text-align: center; width: 98%;" id="tbl_search" runat="server" class="table borderless">
                                <tr class="tblrw">
                                    <td>
                                        <asp:Label ID="lbl_start_date" runat="server" Text="Start Date:" Font-Bold="True" CssClass="control-label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_start_date" CssClass="form-control" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cal_start_date" TargetControlID="txt_start_date" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_end_date" runat="server" Text="End Date:" Font-Bold="True" CssClass="control-label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_end_date" CssClass="form-control" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cal_end_date" TargetControlID="txt_end_date" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </div>


                        <div class="box-footer">
                            <asp:Button runat="server" CssClass="btn btn-primary" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Label ID="lbl_no_record" runat="server" Text="No Records Found" ForeColor="Red"></asp:Label>
                        </div>

                        <div id="div_res" runat="server">
                            <div>
                                <table class="form-control">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_taxpayer_id" runat="server" Text="Tax Payer RIN:" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_taxpayer_id" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 150px;"></td>

                                        <td>
                                            <asp:Label ID="lbl_taxpayer_name" runat="server" Text="Tax Payer Name:" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_taxpayer_name" runat="server"></asp:Label>
                                        </td>
                                    </tr>

                                </table>

                            </div>
                            <br />
                            <div style="overflow: scroll; height: 250px;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grd_trans_history" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" PagerSettings-PageButtonCount="5"
                                                CssClass="GridBaseStyle" HeaderStyle-CssClass="GridHeader" 
                                                OnPageIndexChanging="grd_trans_history_PageIndexChanging" 
                                                OnSelectedIndexChanging="grd_trans_history_SelectedIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="TransId" HeaderText="Transaction ID" />
                                                    <asp:BoundField DataField="TaxPayerName" HeaderText="Tax Payer Name" />
                                                    <asp:BoundField DataField="asset_type" HeaderText="Asset Type" />
                                                    <asp:BoundField DataField="AssetId" HeaderText="Asset RIN" />
                                                    <asp:BoundField DataField="Amount" HeaderText=" (₦‎)Amount" />
                                                </Columns>
                                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Right" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

