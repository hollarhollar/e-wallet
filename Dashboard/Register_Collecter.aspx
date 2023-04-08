<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="Register_Collecter.aspx.cs" Inherits="Dashboard_Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #ContentPlaceHolder1_RegularExpressionValidator1 {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.13.0/css/all.min.css" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/css/bootstrap-datepicker3.standalone.min.css" />
    <script src="https://code.jquery.com/jquery-2.2.4.min.js" integrity="sha256-BbhdlvQf/xTY9gja0Dq3HiwQF8LaCRTXxZKRutelT44="
        crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/js/bootstrap.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/js/bootstrap-datepicker.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            });
        });


    </script>
    <style type="text/css">
        input, input[type=password] {
            width: 300px;
            height: 30px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
        }

        #toggle_pwd {
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        //$(function () {
        //    debugger
        //    $("#toggle_pwd").click(function () {
        //        $(this).toggleClass("fa-eye fa-eye-slash");
        //        var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
        //        debugger
        //        $("#txtPassword").attr("type", type);
        //    });
        //});

        //$(document).on('click', '.toggle-password', function () {
        //    debugger
        //    $(this).toggleClass("fa-eye fa-eye-slash");
        //    debugger

        //    var input = $("#txtpassword");
        //    var x = $("#txtpassword");

        //    console.log(input);
        //    console.log(inputType);

        //    debugger

        //    if (input.attr("type") === "password") {
        //        input.attr("type", "text");
        //    } else {
        //        input.attr("type", "password");
        //    }
        //    /* input.attr('type') === 'password' ? input.attr('type', 'text') : input.attr('type', 'password')*/
        //});
        function viewPassword() {

            const passwordInput = document.getElementById("ContentPlaceHolder1_txt_password");
            const passwordIcon = document.getElementById("txt_passstatus");


            if (passwordInput.type === "password") {

                passwordInput.type = "text";
                passwordIcon.classList.remove("fa-eye");
                passwordIcon.classList.add("fa-eye-slash");
            } else {
                passwordInput.type = "password";
                passwordIcon.classList.remove("fa-eye-slash");
                passwordIcon.classList.add("fa-eye");

            }
        }
        //function viewPassword() {
        //    debugger
        //    var passwordInput = document.getElementById('txt_password');
        //    var passStatus = document.getElementById('txt_passstatus');

        //    if (passwordInput.type == 'password') {
        //        passwordInput.type = 'text';
        //        passStatus.className = 'fa fa-eye-slash';

        //    }
        //    else {
        //        passwordInput.type = 'password';
        //        passStatus.className = 'fa fa-eye';
        //    }
        //}
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
            <div class="col-md-5">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Registration</h3>
                    </div>

                    <div class="box-body">
                        <div class="form-group">

                            <asp:Label ID="lbl_mobile" runat="server" Text="Mobile No.">Mobile No. *</asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txt_mobileno" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"> </asp:RegularExpressionValidator>
                            <asp:TextBox runat="server" ID="txt_mobileno" placeholder="Mobile No." CssClass="form-control" required MaxLength="10" size="10" pattern="[0-9]*" inputmode="numeric"></asp:TextBox>


                            <asp:Label ID="lbl_fname" runat="server" Text="First Name">First Name *</asp:Label>
                            <asp:TextBox runat="server" ID="txt_fname" placeholder="Enter First Name" CssClass="form-control" required></asp:TextBox>

                            <asp:Label ID="lbl_lname" runat="server" Text="Last Name">Last Name *</asp:Label>
                            <asp:TextBox runat="server" ID="txt_lname" placeholder="Enter Last Name" CssClass="form-control" required>
                            </asp:TextBox>

                            <asp:Label ID="lbl_gender" runat="server" Text="Gender">Gender *</asp:Label>

                            <asp:DropDownList runat="server" ID="drp_gender" CssClass="form-control">
                                <asp:ListItem Text="Male" Value="M" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="FeMale" Value="F"></asp:ListItem>
                                <asp:ListItem Text="Others" Value="O"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:Label ID="Label2" runat="server" Text="LGA">LGA *</asp:Label>
                            <asp:DropDownList runat="server" AutoPostBack="true" AppendDataBoundItems="true" ID="lgaList" OnSelectedIndexChanged="lgaList_Changed" CssClass="form-control" />

                            <%--   <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="DropDownList666" CssClass="form-control">
                            </asp:DropDownList>--%>

                            <asp:Label ID="Label89" runat="server" Text="TOWN">Town *</asp:Label>
                            <asp:DropDownList ID="ddlStates" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Town_Changed" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:Label ID="Label4" runat="server" Text="Beat">Beat *</asp:Label>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="false" CssClass="form-control">
                            </asp:DropDownList>

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
                            <asp:Label ID="lbl_dob" runat="server" Text="Date Of Birth">Date Of Birth *</asp:Label>
                            <div class='input-group date' id='datetimepicker1'>
                                <asp:TextBox type='text' runat="server" AutoComplete="off" placeholder="Enter DOB" ID="txt_dob" class="form-control" required />
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>

                            <asp:Label ID="lbl_designation" runat="server" Text="Designation">Designation *</asp:Label>
                            <%--<asp:TextBox runat="server" ID="txt_designation" placeholder="Enter designation" CssClass="form-control" required></asp:TextBox>--%>
                            <asp:DropDownList runat="server" AutoPostBack="true" AppendDataBoundItems="true" ID="txt_DesignationDropDown" CssClass="form-control" />

                            <%--<asp:DropDownList runat="server" ID="txt_DesignationDropDown" CssClass="form-control">
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

                            <asp:Label ID="lbl_address" runat="server" Text="Address">Address *</asp:Label>
                            <asp:TextBox runat="server" ID="txt_address" placeholder="Enter Address" CssClass="form-control" required></asp:TextBox>
                            <asp:Label ID="lbl_email" runat="server" Text="E-mail">E-mail *</asp:Label>
                            <asp:TextBox runat="server" type="email" ID="txt_email" placeholder="Enter E-mail" CssClass="form-control" required></asp:TextBox>
                            <asp:Label ID="lbl_mpin" runat="server" Text="Secured PIN">Secured PIN *</asp:Label>
                            <asp:TextBox runat="server" ID="txt_secured_pin" placeholder="Enter PIN" CssClass="form-control" required></asp:TextBox>

                            <asp:Label ID="Label666" runat="server" Text="Access Type">Access Type *</asp:Label>

                            <asp:DropDownList runat="server" ID="DropDownList666" AutoPostBack="true" OnSelectedIndexChanged="DropDownList666_Changed" CssClass="form-control">
                                <%-- <asp:ListItem Text="Select" Value="Select" Selected="True" disabled></asp:ListItem>
                                <asp:ListItem Text="Admin" Value="100"></asp:ListItem>
                                <asp:ListItem Text="Director_Of_Revenue" Value="114"></asp:ListItem>
                                <asp:ListItem Text="Sub-Admin" Value="103"></asp:ListItem>
                                <asp:ListItem Text="LGA-Admin" Value="104"></asp:ListItem>
                                <asp:ListItem Text="Chairman" Value="105"></asp:ListItem>
                                <asp:ListItem Text="Collector" Value="106"></asp:ListItem>
                                <asp:ListItem Text="LGA-NURTW" Value="108"></asp:ListItem>
                                <asp:ListItem Text="LGA-RTEAN" Value="109"></asp:ListItem>
                                <asp:ListItem Text="LGA-ANNEWATT" Value="110"></asp:ListItem>
                                <asp:ListItem Text="State-NURTW" Value="111"></asp:ListItem>
                                <asp:ListItem Text="State-RTEAN" Value="112"></asp:ListItem>
                                <asp:ListItem Text="State-ANNEWATT" Value="113"></asp:ListItem>--%>
                            </asp:DropDownList>

                            <asp:Label ID="lbl_password" runat="server" Text="Password">Password *</asp:Label><br>
                            <input type="password" runat="server" id="txt_password" placeholder="Enter password" />
                            <i id="txt_passstatus" class="fa fa-eye" aria-hidden="true" onclick="viewPassword()"></i>
                            <br>
                        </div>
                    </div>
                    <div class="col-md-10" align="center" style="margin-top: 15px; margin-left: -130px;">
                        <asp:Button runat="server" CssClass="btn btn-primary" Text="Submit" type="submit" ID="Button1" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
            <br />

            <%--<div class="col-md-10" align="center">

                <asp:Button runat="server" CssClass="btn btn-primary" Text="Submit" type="submit" ID="btnSubmit" OnClick="btnSubmit_Click" />

            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



