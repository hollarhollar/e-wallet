<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Dashboard/MasterPage.master" EnableEventValidation="false" CodeFile="CollectorDetail.aspx.cs" Inherits="Dashboard_CollectorDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style2 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 72%;
            left: 374px;
            top: -15px;
            height: 53px;
            padding-left: 15px;
            padding-right: 15px;
        }

        .btn:hover {
            background-color: #3c8dbc;
            color: #fff;
        }

        .btn-theme {
            background-color: none;
            color: #fff; /* sets the text color to white */
        }

            .btn-theme.active {
                background-color: blue;
            }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentheading" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-left: 10px;">
        <!-- <iframe    src=" http://reports.nownowpay.ng/public/question/3eb68018-c506-4873-9272-c3db87879b60"   
            frameborder="0"    width="800"    height="500"    allowtransparency></iframe>-->
        <div class="box-header with-border ">
            <h3 class="box-title"><b>Collector</b></h3>
        </div>
        <div style="padding-left: 10px; height: 20px;">
            <table class="table borderless" style="border-style: none; border-color: inherit; border-width: medium; width: 101%; height: 133px;">
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
                    <td>
                </tr>
                <tr style="display: none;">
                    <td>Employer Name:</td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txt_employer_name" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>Employer TIN:</td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txt_employer_TIN" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>Business RIN:</td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txt_business_RIN" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box box-body">
        <div id="div_res" runat="server">
            <div style="overflow-x: auto;">
                <asp:Repeater ID="rptCollector" runat="server" OnItemDataBound="rptCollector_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table">
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <th>ID:</th>
                            <td><%# Eval("id") %></td>
                        </tr>
                        <tr>
                            <th>First Name:</th>
                            <td><%# Eval("first_name") %></td>
                        </tr>
                        <tr>
                            <th>Last Name:</th>
                            <td><%# Eval("last_name") %></td>
                        </tr>
                        <tr>
                            <th>Sub Phone Number:</th>
                            <td><%# Eval("Phone") %></td>
                        </tr>
                        <tr>
                            <th>LGA:</th>
                            <td><%# Eval("lga") %></td>
                        </tr>
                        <tr>
                            <th>Address:</th>
                            <td><%# Eval("address") %></td>
                        </tr>
                        <tr>
                            <th>Revenue Beat:</th>
                            <td><%# Eval("Revenuebeat") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

</asp:Content>
