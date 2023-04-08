<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="EditLGACollector.aspx.cs" Inherits="Dashboard_EditLGACollector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/../code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $('#<%=txt_dob.ClientID%>').datepicker({
                     format: 'mm/dd/yyyy',
                     autoclose: true
                 });
             });
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
    <style>
        #ContentPlaceHolder1_RegularExpressionValidator1 {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">





    <div class="col-md-5">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Edit LGA Collector</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->

            <div class="box-body">
                <div class="form-group">

                    <label for="exampleInputEmail1">Search By UserName</label>
                    <asp:DropDownList runat="server" ID="drpusername" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpusername_SelectedIndexChanged"></asp:DropDownList>

                    <asp:Label ID="lbl_mobile" runat="server" Text="Mobile No.">Mobile No *</asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txt_mobileno" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"> </asp:RegularExpressionValidator>
                    <asp:TextBox runat="server" ID="txt_mobileno" placeholder="Mobile No." CssClass="form-control" required MaxLength="10" size="10" pattern="[0-9]*" inputmode="numeric" ReadOnly="true"></asp:TextBox>

                    <asp:Label ID="lbl_entityid" runat="server" Text="Entity Id"></asp:Label>
                    <asp:TextBox runat="server" ID="txt_entityid" placeholder="Enter Entity Id" CssClass="form-control" required ReadOnly="true"></asp:TextBox>

                    <asp:Label ID="lbl_fname" runat="server" Text="First Name"></asp:Label>
                    <asp:TextBox runat="server" ID="txt_fname" placeholder="Enter First Name" CssClass="form-control" required></asp:TextBox>

                    <asp:Label ID="lbl_lname" runat="server" Text="Last Name"></asp:Label>
                    <asp:TextBox runat="server" ID="txt_lname" placeholder="Enter Last Name" CssClass="form-control" required></asp:TextBox>

                    <asp:Label ID="lbl_gender" runat="server" Text="Gender"></asp:Label>
                    <asp:DropDownList runat="server" ID="drp_gender" CssClass="form-control">
                        <asp:ListItem Text="Male" Value="M" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="FeMale" Value="F"></asp:ListItem>
                        <asp:ListItem Text="Others" Value="O"></asp:ListItem>
                    </asp:DropDownList>
                   <%--<asp:DropDownList runat="server"  ID="Accesstype"  CssClass="form-control">
                   
                   </asp:DropDownList>--%>
                 <%--  <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control">--%>
                               <%-- <asp:ListItem Text="Select" Value="Select" Selected="True" disabled></asp:ListItem>
                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                <asp:ListItem Text="Director_Of_Revenue" Value="Director_Of_Revenue"></asp:ListItem>
                                <asp:ListItem Text="Sub-Admin" Value="Sub-Admin"></asp:ListItem>
                                <asp:ListItem Text="LGA-Admin" Value="LGA-Chairman"></asp:ListItem>
                                <asp:ListItem Text="Chairman" Value="Chairman"></asp:ListItem>
                                <asp:ListItem Text="Collector" Value="Collector"></asp:ListItem>
                                <asp:ListItem Text="LGA-NURTW" Value="LGA-NURTW"></asp:ListItem>
                                <asp:ListItem Text="LGA-RTEAN" Value="LGA-RTEAN"></asp:ListItem>
                                <asp:ListItem Text="LGA-ANNEWATT" Value="LGA-ANNEWATT"></asp:ListItem>
                                <asp:ListItem Text="State-NURTW" Value="State-NURTW"></asp:ListItem>
                                <asp:ListItem Text="State-RTEAN" Value="State-RTEAN"></asp:ListItem>
                                <asp:ListItem Text="State-ANNEWATT" Value="State-ANNEWATT"></asp:ListItem>--%>
                    <%--</asp:DropDownList>--%>
                </div>
            </div>
            <!-- /.box-body -->

        </div>
    </div>
    <div class="col-md-5">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">&nbsp;</h3>
            </div>
            <div class="box-body">
                <div class="form-group">

                    <asp:Label ID="lbl_dob" runat="server" Text="Date Of Birth"></asp:Label>
                    <asp:TextBox runat="server" ID="txt_dob" placeholder="Enter DOB" CssClass="form-control" required></asp:TextBox>

                   <%--  <asp:Label ID="lbl_designation" runat="server" Text="Designation">Designation *</asp:Label>--%>
                     
                    <asp:Label ID="Label1" runat="server" Text="Access Type">Access Type *</asp:Label>
                            <asp:DropDownList runat="server" ID="txt_DesignationDropDown" CssClass="form-control">
                               <%-- <asp:ListItem Text="Select" Value="Select" Selected="True" disabled></asp:ListItem>
                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                <asp:ListItem Text="Director_Of_Revenue" Value="Director_Of_Revenue"></asp:ListItem>
                                <asp:ListItem Text="Sub-Admin" Value="Sub-Admin"></asp:ListItem>
                                <asp:ListItem Text="LGA-Admin" Value="LGA-Chairman"></asp:ListItem>
                                <asp:ListItem Text="Chairman" Value="Chairman"></asp:ListItem>
                                <asp:ListItem Text="Collector" Value="Collector"></asp:ListItem>
                                <asp:ListItem Text="LGA-NURTW" Value="LGA-NURTW"></asp:ListItem>
                                <asp:ListItem Text="LGA-RTEAN" Value="LGA-RTEAN"></asp:ListItem>
                                <asp:ListItem Text="LGA-ANNEWATT" Value="LGA-ANNEWATT"></asp:ListItem>
                                <asp:ListItem Text="State-NURTW" Value="State-NURTW"></asp:ListItem>
                                <asp:ListItem Text="State-RTEAN" Value="State-RTEAN"></asp:ListItem>
                                <asp:ListItem Text="State-ANNEWATT" Value="State-ANNEWATT"></asp:ListItem>--%>
                            </asp:DropDownList>
                    <asp:Label ID="lbl_address" runat="server" Text="Address"></asp:Label>
                    <asp:TextBox runat="server" ID="txt_address" placeholder="Enter Address" CssClass="form-control" required></asp:TextBox>

                    <asp:Label ID="lbl_email" runat="server" Text="E-mail"></asp:Label>
                    <asp:TextBox runat="server" ID="txt_email" placeholder="Enter E-mail" CssClass="form-control" required></asp:TextBox>

                    <asp:Label ID="lbl_mpin" runat="server" Text="Secured PIN"></asp:Label>
                    <asp:TextBox runat="server" ID="txt_secured_pin" placeholder="Enter PIN" MaxLength="4" size="4" pattern="[0-9]*" inputmode="numeric" Style="-webkit-text-security: disc;" autocomplete="off" CssClass="form-control" required></asp:TextBox>

                    <asp:Label ID="Label2" runat="server" Text="LGA"></asp:Label>
                    <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="lgaList" AutoPostBack="true" OnSelectedIndexChanged="Country_Changed" CssClass="form-control" />

                    <asp:Label ID="Label890" runat="server" Text="TOWN">Town *</asp:Label>
                            <asp:DropDownList ID="ddlStates" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Town_Changed" CssClass="form-control">
                            </asp:DropDownList>
                    <asp:Label ID="Label3" runat="server" Text="Beat"></asp:Label>

                        <asp:DropDownList ID="beatCode" runat="server" AutoPostBack = "false"  CssClass="form-control">
                    </asp:DropDownList>

<%--                    <asp:TextBox runat="server" ID="beatCode" placeholder="Beat " CssClass="form-control" required></asp:TextBox>--%>

                </div>
                <div class="box-footer">
                </div>

            </div>
        </div>
    </div>

    <br />
    <div class="col-md-10" align="center">
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Update" ID="btnUpdate" OnClick="btnUpdate_Click" />
    </div>
</asp:Content>

