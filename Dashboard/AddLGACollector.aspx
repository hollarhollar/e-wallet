<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="AddLGACollector.aspx.cs" Inherits="Dashboard_AddLGACollector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" Runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="col-md-6">
        <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Add LGA Collector</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            
              <div class="box-body">
                <div class="form-group">
                  <label for="exampleInputEmail1">Username</label>
                    <asp:TextBox runat="server" ID="txtusername" placeholder="Enter Username" CssClass="form-control" required></asp:TextBox>
                  
                </div>
                <div class="form-group">
                  <label for="exampleInputPassword1">Password</label>
                   <asp:TextBox runat="server" ID="txtpassword" placeholder="Enter password" TextMode="Password" CssClass="form-control" required></asp:TextBox>
                </div>
                
                  <div class="form-group">
                  <label for="exampleInputPassword1">Designation</label>
                   <asp:TextBox runat="server" ID="txtdesignation" placeholder="Enter Designation" CssClass="form-control" required></asp:TextBox>
                </div>

                  <div class="form-group">
                  <label for="exampleInputPassword1">Mobile Number</label>
                   <asp:TextBox runat="server" ID="txtmobileno" placeholder="Enter Mobile number"  CssClass="form-control" required></asp:TextBox>
                </div>

                  <div class="form-group">
                  <label for="exampleInputPassword1">Address</label>
                   <asp:TextBox runat="server" ID="txtaddress" placeholder="Enter address" CssClass="form-control" required></asp:TextBox>
                </div>
              </div>
              <!-- /.box-body -->

              <div class="box-footer">
                  <asp:Button runat="server" CssClass="btn btn-primary" Text="Submit" ID="btnSubmit" OnClick="btnSubmit_Click" />
                
              </div>
            </div>
    </div>

    
    
</asp:Content>

