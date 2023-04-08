<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewLowBalanceCollector.aspx.cs" EnableEventValidation="true" MasterPageFile="~/Dashboard/MasterPage.master"  Inherits="Dashboard_ViewLowBalanceCollector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
    <script>
        function updateNotifications() {
            $.ajax({
                url: '/notifications',
                type: 'GET',
                success: function (data) {
                    // TODO: Parse the data and display the notifications in the dropdown
                },
                error: function () {
                    // TODO: Handle error case
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentheading" runat="Server">
    <%--  <style>
       
    </style>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="box-header with-border">
       <header class="h2">Low Balance Collectors </header>
    </div>
    <div style="padding-left: 10px;">
        </div>
        <asp:GridView ID="grd_collector" ShowHeaderWhenEmpty="true" runat="server" AllowPaging="false"
            AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover table-dark"
            HeaderStyle-CssClass="GridHeader" ShowFooter="false">
            <PagerStyle CssClass="pagination-ys" />

            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="CollectorId" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦) CollectorId" />
                <asp:BoundField DataField="BalanceStatus" HeaderText="BalanceStatus" />
            </Columns>


        </asp:GridView>
        <div style="padding: inherit">
            <br />
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClientClick="javascript:history.back(1); return false;" />
        </div>


        <div style="margin-top: 60px; margin-left: 10px; display: inline;" id="div_paging" runat="server">
            <div class='pagination-container'>

                <ul class="pagination">

                    <li data-page="prev">
                        <span>< <span class="sr-only">(current)</span></span>
                    </li>
                    <!--	Here the JS Function Will Add the Rows -->
                    <li data-page="next" id="prev">
                        <span>> <span class="sr-only">(current)</span></span>
                    </li>
                </ul>

            </div>
        </div>
    </div>
</asp:Content>











