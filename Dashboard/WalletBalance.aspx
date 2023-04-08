<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="WalletBalance.aspx.cs" Inherits="WalletBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 206px;
            height: 244px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
    Wallet Balance
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-md-6">
        <div class="box box-primary">
            <div class="box-body">

                <div class="form-group">
                    <table class="table borderless" style="width: 100% !important; border: none !important;">
                        <tr>
                            <td>
                                <asp:Label ID="lbl_fname" Text="First Name:" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txt_fname" runat="server"></asp:Label>
                            </td>
                            <td></td>
                            <td rowspan="6">
                                <img class="auto-style1" src="img/photo.jpg" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_lname" Text="Last Name:" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txt_lname" runat="server"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_gender" Text="Gender:" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txt_gender" runat="server"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_email" Text="E-mail:" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txt_email" runat="server"></asp:Label>
                            </td>
                            <td></td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_CurrentBal" Text="Current Balance:" runat="server"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="txt_CurrentBal" runat="server"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_AvailableBal" Text="Available Balance:" runat="server"></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="txt_AvailableBal" runat="server"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

