<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Dashboard/MasterPage.master" EnableEventValidation="false" CodeFile="Collectors.aspx.cs" Inherits="Dashboard_Collectors" %>

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
        <div class="box-header with-border ">
            <h3 class="box-title"><b>Collector</b></h3>
        </div>
        <div style="padding-left: 10px; height: 20px; ">
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
            <div style="overflow-y: auto;">

                <asp:GridView ID="grd_collector" runat="server" AllowPaging="True" AllowSorting="True" ShowHeader="true"
                    AutoGenerateColumns="False" PagerSettings-PageButtonCount="5" PageSize="10"
                    CssClass="table table-striped table-bordered table-hover table-dark" HeaderStyle-CssClass="GridHeader" OnPageIndexChanging="grd_paginHandler" CellPadding="12" ForeColor="#333333" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="white" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" Visible="true" />
                        <asp:BoundField DataField="first_name" HeaderText="First Name" />
                        <asp:BoundField DataField="last_name" HeaderText="Last Name" />
                        <asp:BoundField DataField="Phone" HeaderText="Sub Phone Number" DataFormatString="" />
                        <asp:BoundField DataField="lga" HeaderText="LGA" />
                        <asp:BoundField DataField="Revenuebeat" HeaderText="Revenue Beat" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <div class="btn-group">
                                    <button type="button" class="btn btn-theme fa fa-edit text-primary btn-info btn-xs md-skip" aria-haspopup="true">
                                        <asp:LinkButton ToolTip='<%#Eval("id") %>' OnClick="viewbutton_Click" runat="server" ID="viewbutton">View</asp:LinkButton>
                                    </button>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <%-- <EditRowStyle BackColor="#7C6F57" />
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#1C5E55" CssClass="GridHeader" Font-Bold="True" ForeColor="White" />--%>
                    <PagerSettings PageButtonCount="5" />
                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" BackColor="#666666" ForeColor="black" />
                    <%--  <RowStyle BackColor="#E3EAEB" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />--%>
                    <%-- <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                <SortedDescendingHeaderStyle BackColor="#15524A" />--%>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>




