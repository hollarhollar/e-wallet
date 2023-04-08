<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateCards.aspx.cs" Inherits="Dashboard_GenerateCards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" Runat="Server">
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
         .dispNone {
             display:none !important;
         }

    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" DefaultButton="btnSubmit">
            <ContentTemplate>
               
    <div style="padding:20px;" >
        <div class="box box-primary">
            
            <!-- /.box-header -->
            <!-- form start -->
           
              <div class="box-body">
                <div class="form-group">
                  <asp:Label ID="lbl_mobile" runat="server" Text="Number Of Scratch Cards"></asp:Label>
                    <asp:TextBox runat="server" ID="cardCount" maxlength="6" placeholder="Number Of Scratch Cards" type="number" CssClass="form-control" required></asp:TextBox>
                   <input type="hidden" id="hdnConfirm" runat="server" value="false"/>
               <asp:Label ID="denom" runat="server" Text="Card Denomination"></asp:Label>
                    <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="denomination" CssClass="form-control">
                   
                      

                   </asp:DropDownList>

                   
                  
                      <asp:Label ID="Label2" runat="server" Text="LGA"></asp:Label>
                    <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="lgaList" CssClass="form-control">
                   
                      

                   </asp:DropDownList>
                 
                

                  
                </div>
                   <div align="center">
     
                             <asp:Button runat="server" CssClass="btn btn-primary" Text="Submit" type="submit" ID="btnSubmit" OnClick="btnSubmit_Click" OnClientClick="showLoader();"/>
     
                     </div>
              </div>
              <!-- /.box-body -->
            
             

              
            </div>
    </div>
    
       <div class="modal" id="modalinfo" style="z-index: 19999;" runat="server">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                 
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Default Modal</h4>
              </div>
              <div class="modal-body">
                <p><asp:Label runat="server" Text="modalbody" ID="lblmodalbody"></asp:Label></p>
              </div>
              <div class="modal-footer">

               <a href="#" class="btn btn-primary" onclick="hideModalRedirect()">Yes</a>
                <a href="#" class="btn btn-primary" onclick="hideModal()">No</a>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
                <script>        
                    function getNumber() {
                        var minNumber = 10000; // The minimum number you want
                        var maxNumber = 100000000; // The maximum number you want
                        var randomnumber = Math.floor(Math.random() * (maxNumber + 1) + minNumber); // Generates random number
                      
                        return randomnumber; 
                    }
                    function showLoader() {
                        document.getElementById("ContentPlaceHolder1_hdnConfirm").value = getNumber();
                        $(".loader").fadeIn("slow");
                    }
                    function hideLoader() {

                        $(".loader").fadeOut("slow");
                    }
                    function hideModalRedirect() {
                        hideLoader();
                        window.open('cards/' + document.getElementById("ContentPlaceHolder1_hdnConfirm").value + '.xlsx', '_blank');
                        var element = document.getElementById("ContentPlaceHolder1_modalinfo");
                        element.className = "modal fade";
                    }
                    function hideModal() {
                        hideLoader();
                        var element = document.getElementById("ContentPlaceHolder1_modalinfo");
                        element.className = "modal fade";
                    }
                </script>
     

     </ContentTemplate></asp:UpdatePanel>   
</asp:Content>

