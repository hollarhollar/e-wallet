<%--<%@ Page Language="C#"MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="BeatRegister.aspx.cs" Inherits="Dashboard_BeatRegister" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="BeatRegister.aspx.cs" Inherits="Dashboard_BeatRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <style>
        .GridBaseStyle td{
            padding: 10px;
            border: solid 1px #737272;
            width: 60px;
            height: 31px;
        }
    </style>
    <style>
        #ContentPlaceHolder1_RegularExpressionValidator1 {
          color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/css/bootstrap-datepicker3.standalone.min.css" />
    <script
        src="https://code.jquery.com/jquery-2.2.4.min.js"
        integrity="sha256-BbhdlvQf/xTY9gja0Dq3HiwQF8LaCRTXxZKRutelT44="
        crossorigin="anonymous"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/js/bootstrap.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            });
        });


    </script>
    <script>
        // Add below functions in your JS web resource 
        // Register only onLoadEvents function in the form onload
        function onLoadEvents() {
            Xrm.Page.getControl('mobilephone').addOnKeyPress(AllowOnlyNumbers);
        }

        function AllowOnlyNumbers() {
            var phoneNo = Xrm.Page.getControl("mobilephone").getValue();
            phoneNo = phoneNo.replace(/\D/g, '');
            Xrm.Page.getAttribute('mobilephone').setValue(phoneNo);
            if (phoneNo.length == 11) {
                var formattedPhone = phoneNo.replace(/(\d{3})(\d{3})(\d{4})/, '$1-$2-$3');
                Xrm.Page.getAttribute('mobilephone').setValue(formattedPhone);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-md-10">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Registration</h3>
                    </div>
                    <!-- /.box-header -->
                    <!-- form start -->

                    <div class="box-body">
                        <div class="form-group">
                             <asp:Label ID="lbl_address" runat="server" Text="State">State *</asp:Label>
                            <asp:TextBox runat="server" ID="txt_address" placeholder="EDO State" CssClass="form-control" disabled></asp:TextBox>
                           
                         <%--   <asp:Label ID="Label2" runat="server" Text="LGA">LGA *</asp:Label>
                            <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="lgaList" CssClass="form-control" />--%>
                               <asp:Label ID="Label88" runat="server" Text="LGA">LGA *</asp:Label>
                        <asp:DropDownList ID="ddlCountries" runat="server" AutoPostBack = "true" OnSelectedIndexChanged = "Country_Changed" CssClass="form-control">
                    </asp:DropDownList>
                            <asp:Label ID="Label89" runat="server" Text="TOWN">TOWN *</asp:Label>
                        <asp:DropDownList ID="ddlStates" runat="server" AutoPostBack = "true" OnSelectedIndexChanged = "Town_Changed" CssClass="form-control">
                    </asp:DropDownList>
                            <%--<asp:DropDownList ID="ddlStates" runat="server">
                    </asp:DropDownList>--%>
                             <asp:Label ID="Label1" runat="server" Text="PostalCode">Postal Code  *</asp:Label>
                            <asp:DropDownList ID="TextBoxPostalCode1" runat="server" AutoPostBack = "true"  CssClass="form-control">
                    </asp:DropDownList>

                              <%--<asp:Label ID="Label2" runat="server" Text="PostalCode">Postal Code1 *</asp:Label>
                            <asp:TextBox runat="server" ID="TextBoxPostalCode1"  CssClass="form-control" disabled></asp:TextBox>
                           --%>
                                <asp:Label ID="LabelLocation" runat="server" Text="Location">Location *</asp:Label>
                            <asp:TextBox runat="server" ID="TextBox1" placeholder="Enter Beat Location" CssClass="form-control"></asp:TextBox>
                           
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
            </div>
          <%--  <div class="col-md-5">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">&nbsp;</h3>
                    </div>--%>
                   <%-- <div class="box-body">
                        <div class="form-group">
                            <asp:Label ID="lbl_dob" runat="server" Text="Date Of Birth">Date Of Birth *</asp:Label>
                            <div class='input-group date' id='datetimepicker1'>
                                <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Enter DOB" ID="txt_dob" class="form-control" required />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>

                            <asp:Label ID="lbl_designation" runat="server" Text="Designation">Designation *</asp:Label>--%>
                            <%--<asp:TextBox runat="server" ID="txt_designation" placeholder="Enter designation" CssClass="form-control" required></asp:TextBox>--%>

              <%--             <asp:DropDownList runat="server" ID="txt_DesignationDropDown" CssClass="form-control">
                                <asp:ListItem Text="Select" Value="Select" Selected="True" disabled></asp:ListItem>
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
                                <asp:ListItem Text="State-ANNEWATT" Value="State-ANNEWATT"></asp:ListItem>
                            </asp:DropDownList>--%>

                      <%--      <asp:Label ID="lbl_email" runat="server" Text="E-mail">E-mail *</asp:Label>
                            <asp:TextBox runat="server" type="email" ID="txt_email" placeholder="Enter E-mail" CssClass="form-control" required></asp:TextBox>
                            <asp:Label ID="lbl_mpin" runat="server" Text="Secured PIN">Secured PIN *</asp:Label>
                            <asp:TextBox runat="server" ID="txt_secured_pin" placeholder="Enter PIN" CssClass="form-control" required></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" Text="Beat Code">Beat Code *</asp:Label>
                            <asp:TextBox runat="server" ID="beatCode" placeholder="Beat Code" CssClass="form-control" required></asp:TextBox>
                        </div>
                    </div>--%>
                    <div class="col-md-10" align="center" style="margin-top: 15px; margin-left: -130px;">
                        <asp:Button runat="server" CssClass="btn btn-primary" Text="Submit" type="submit" ID="Button1" OnClick="btnSubmit_Click" />
                    </div>
              <%--  </div>
            </div>--%>
            <br />

            <%--<div class="col-md-10" align="center">

                <asp:Button runat="server" CssClass="btn btn-primary" Text="Submit" type="submit" ID="btnSubmit" OnClick="btnSubmit_Click" />

            </div>--%>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




