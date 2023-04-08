<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="BalanceTransferWallet.aspx.cs" Inherits="Dashboard_BalanceTransferWallet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
    /* display: none; <- Crashes Chrome on hover */
    -webkit-appearance: none;
    margin: 0; /* <-- Apparently some margin are still there even though it's hidden */
}

input[type=number] {
    -moz-appearance:textfield; /* Firefox */
}
    </style>
      <div class="col-md-6">
        <div class="box box-primary">
           

             <div class="box-body">
                <div class="form-group">
                  <asp:Label ID="lbl_sender_mobile" runat="server" Text="Sender Mobile No."></asp:Label>
                    <asp:TextBox runat="server" ID="txt_sender_MobileNo" placeholder="Mobile No." Enabled="false" CssClass="form-control" required></asp:TextBox>
                  
                </div>

                 <div class="form-group">
                  <asp:Label ID="lbl_reciever_mobile" runat="server" Text="Receiver Mobile No."></asp:Label>
                    <asp:TextBox runat="server" ID="txt_reciever_MobileNo" placeholder="Mobile No." Enabled="false" CssClass="form-control" required></asp:TextBox>
                  
                </div>


                   <div class="form-group">
                  <asp:Label ID="lbl_amount" runat="server" Text="Amount">Amount *</asp:Label>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1"   ControlToValidate="txt_amount" runat="server"  ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"> </asp:RegularExpressionValidator>
                    <asp:TextBox runat="server" ID="txt_amount" placeholder="Amount" MaxLength="10" type="number" CssClass="form-control" required></asp:TextBox>
                  
                       <%--maxlength="4" size="4"--%>
                </div>
                   <div class="form-group">
                  <asp:Label ID="lbl_mpin" runat="server" Text="M-Pin">M-Pin *</asp:Label>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator2"   ControlToValidate="txt_mpin" runat="server"  ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"> </asp:RegularExpressionValidator>
                    <asp:TextBox runat="server" ID="txt_mpin" placeholder="m-pin" type="text" maxlength="4" size="4"  pattern="[0-9]*" inputmode="numeric" style=" -webkit-text-security: disc;"  autocomplete="off"  CssClass="form-control" required></asp:TextBox>
                </div>

                   <div class="box-footer">
                  <asp:Button runat="server" CssClass="btn btn-primary" Text="Submit" ID="btnSubmit" OnClick="btnSubmit_Click" />
                
              </div>
            </div>


          </div>
          </div>
</asp:Content>

