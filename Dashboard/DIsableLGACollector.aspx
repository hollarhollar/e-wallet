<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="DIsableLGACollector.aspx.cs" Inherits="Dashboard_DIsableLGACollector" %>

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
                    <asp:DropDownList runat="server" ID="drpusername" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpusername_SelectedIndexChanged"></asp:DropDownList>
					<label for="syncCount">Sync Count</label>
                    <asp:TextBox runat="server" ID="synCount" type="number" placeholder="Enter SynCount" CssClass="form-control" required></asp:TextBox>
                </div>
                             
              </div>
              <!-- /.box-body -->

              <div class="box-footer">
                  <asp:Button runat="server" Text="Enable User" CssClass="btn btn-primary active" ID="btnEnable" OnClick="btnEnable_Click"/>
                  <asp:Button runat="server" Text="Disable User" CssClass="btn btn-primary disabled" ID="btnDisable" OnClick="btnDisable_Click"/>
                
              </div>
            </div>
    </div>
</asp:Content>

