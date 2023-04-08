<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminUsers.aspx.cs" MasterPageFile="~/Dashboard/MasterPage.master" Inherits="Dashboard_Admin_Users" %>

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

        .button-style {
            font-size: 100%;
            -webkit-appearance: none;
            margin: 0;
            outline: none;
            display: inline-block;
            box-sizing: border-box;
            text-decoration: none;
            padding: 8px 12px;
            padding: 0.5rem 0.75rem;
            background: #fbfcfd;
            border: 1px solid #ddd;
            color: #727479;
            cursor: pointer;
            text-decoration: none;
            font-weight: bold;
            font-family: 'Lato', sans-serif;
            border-radius: 4px;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentheading" runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script>
        var header = document.getElementById("mydiv");
        var btn = header.getElementsByClassName("btn");
        for (var i = 1; i < btn.length; i++) {
            btn[i].addEventListener("click", function () {
                var current =
                    document.getElementsByClassName("active");
                current[0].className =
                    current[0].className.replace("active", "");
                this.className += " active";
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-left: 10px;">
        <!-- <iframe    src=" http://reports.nownowpay.ng/public/question/3eb68018-c506-4873-9272-c3db87879b60"   
            frameborder="0"    width="800"    height="500"    allowtransparency></iframe>-->
        <div class="box-header with-border">
            <h3 class="box-title"><b>Admin Users</b></h3>
        </div>
        <div style="padding-left: 10px;">
            <div style="text-align: center;">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="downloadCSV" CssClass="button-style">DOWNLOAD CSV</asp:LinkButton>
                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" CssClass="button-style">DOWNLOAD EXCEL</asp:LinkButton>
            </div>
            <table class="table borderless" style="border-style: none; border-color: inherit; border-width: medium; width: 101%; height: 43px;">

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


            <asp:GridView ID="grd_collector" runat="server" AllowPaging="True" AllowSorting="True" ShowHeader="true"
                AutoGenerateColumns="False" PagerSettings-PageButtonCount="5" PageSize="10"
                CssClass="table table-striped table-bordered table-hover table-dark" HeaderStyle-CssClass="GridHeader" OnPageIndexChanging="grd_paginHandler" CellPadding="12" ForeColor="#333333" GridLines="Vertical">
                <AlternatingRowStyle BackColor="white" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" Visible="false" />
                    <asp:BoundField DataField="name" HeaderText="Name" />
                    <asp:BoundField DataField="email" HeaderText="Email Address" />
                    <asp:BoundField DataField="designation" HeaderText="Role" />
                    <asp:BoundField DataField="mobile_no" HeaderText="Phone Number" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <div id="mydiv" class="btn-group">
                                <button type="button" class="btn btn-theme btn-xs md-skip" aria-haspopup="true">
                                    <asp:LinkButton ToolTip='<%#Eval("id") %>' OnClick="activebutton_Click" runat="server" ID="activebutton">Active</asp:LinkButton>
                                </button>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <div class="btn-group">
                                <button type="button" class="btn btn-theme fa fa-edit text-primary btn-info btn-xs md-skip" aria-haspopup="true">
                                    <asp:LinkButton ToolTip='<%#Eval("id") %>' OnClick="edit_Click" runat="server" ID="editbutton">Edit</asp:LinkButton>
                                </button>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <div class="btn-group">
                                <button type="button" class="btn btn-theme fa fa-edit text-primary btn-info btn-xs md-skip" aria-haspopup="true">
                                    <asp:LinkButton ToolTip='<%#Eval("id") %>' OnClick="detailsbutton_Click" runat="server" ID="detailsbutton">Details</asp:LinkButton>
                                </button>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <PagerSettings PageButtonCount="5" />
                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" BackColor="#666666" ForeColor="black" />
            </asp:GridView>

            <div style="margin-top: -60px; margin-left: 10px;" id="div_paging" runat="server">
            </div>
            <!--  <iframe    src="http://reports.nownowpay.ng/public/question/c5bee7da-4892-455c-867a-8641a7078137"    frameborder="0"    width="1100"    height="500"    allowtransparency></iframe>-->
        </div>



    </div>
</asp:Content>




