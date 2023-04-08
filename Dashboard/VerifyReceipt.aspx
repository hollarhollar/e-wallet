<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="VerifyReceipt.aspx.cs" Inherits="Dashboard_VerifyReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
    <style>
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            /* display: none; <- Crashes Chrome on hover */
            -webkit-appearance: none;
            margin: 0; /* <-- Apparently some margin are still there even though it's hidden */
        }

        input[type=text] {
            -moz-appearance: textfield; /* Firefox */
        }

        .dispNone {
            display: none !important;
        }
    </style>
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" DefaultButton="btnSubmit">
        <ContentTemplate>

            <div style="padding: 20px;">
                <div class="box box-primary">
                    <div class="box-header with-border">
                <h3 class="box-title">Verify Receipt</h3>
            </div>
                    <!-- /.box-header -->
                    <!-- form start -->

                    <div class="box-body">

                        <div class="form-group">
                            <div class="raw">
                                <div class="col-sm-2">
                                    <asp:Label ID="lbl_mobile" runat="server" Text="Receipt Number">Receipt Number *</asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="receiptNumber" MaxLength="30" AutoPostBack="false"  placeholder="Enter Receipt Number" type="text" CssClass="form-control" required></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button runat="server" CssClass="btn btn-success" Text="Verify" type="submit" ID="Button1" AutoPostBack="true" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                            <input type="hidden" id="hdnConfirm" runat="server" value="false" />
                        </div>

                        <br />
                        <br />
                        <hr class=" mt-5" />
                        <div id="showVerification">
                            <div class="form-group">
                                <%--<div >
                                    <asp:Label ID="message" runat="server">No such record in the database</asp:Label>
                                </div>--%>
                                <table class="table borderless" style="width: 100% !important; border: none !important;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="phoneNumber" Text="Phone Number:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_phoneNumber" runat="server"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <asp:Label ID="lbl_collector" Text="Vehicle No:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_vehicleNo" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_sector" Text="Sector:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_sector" runat="server"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_subSector" Text="Sub-Sector:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_subsector" runat="server"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_amount" Text="Amount:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_amount" runat="server"></asp:Label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_date" Text="Date:" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txt_date" runat="server"></asp:Label>
                                        </td>
                                        <td></td>
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

