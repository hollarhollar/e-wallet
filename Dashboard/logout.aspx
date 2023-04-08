<%@ Page Language="C#"  MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="logout.aspx.cs" Inherits="logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>



<body>
    <div id="domMessage" style="display:none;">
        <h4></h4>
    </div>

    <div class="container">
        <div class="row">
            <form id="form2" runat="server">

                <div class="form-buttons">
                    <asp:Button CssClass="btn btn-danger" Text="Logout" ID="btnlogout" runat="server" OnClick="btnLogout_Click" />
                </div>
            </form>
          </div>
    </div>

</body>
</html>
