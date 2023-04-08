<%@ Page Language="C#" AutoEventWireup="true" CodeFile="notificationbell.aspx.cs" EnableEventValidation="true" MasterPageFile="~/Dashboard/MasterPage.master" Inherits="Dashboard_notificationbell" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--  <script>
        function showNotifications() {
            var dropdown = document.getElementById("notification-dropdown");
            if (dropdown.style.display === "none") {
                dropdown.style.display = "block";
            } else {
                dropdown.style.display = "none";
            }
        }

        document.addEventListener("click", function (event) {
            var dropdown = document.getElementById("notification-dropdown");
            var button = document.getElementById("notification-button");
            if (event.target !== dropdown && event.target !== button) {
                dropdown.style.display = "none";
            }
        });

        function openModal() {
            $('#myModal').modal('show');
        }


    </script>--%>
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
        .notification-wrapper {
            position: relative;
            display: inline-block;
        }

        .notification-dropdown {
            position: absolute;
            top: 50px;
            right: 0;
            width: 200px;
            background-color: #fff;
            border: 1px solid #ccc;
            display: none;
            z-index: 1;
        }

        .notification-list {
            list-style: none;
            margin: 0;
            padding: 0;
        }

        .notification-item {
            padding: 10px;
            border-bottom: 1px solid #ccc;
        }

        #notification-button {
            background-color: #3498db;
            color: #fff;
            border: none;
            padding: 10px;
            border-radius: 5px;
            cursor: pointer;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="box-header with-border">
        <h2 class="box-title"><b>NOTIFICATION  </b></h2>
    </div>
    <div style="padding-left: 10px;">
        <div style="padding-left: 10px">
            <div class="box-header with-border">
                <h3 class="box-title font-sm">Collectors with Low Balance:
                    <asp:Label runat="server" Text=" 0 " ID="lblCollector"></asp:Label></h3>
            </div>

        </div>
        <asp:GridView ID="grd_collector" ShowHeaderWhenEmpty="true" runat="server" AllowPaging="false"
            AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover table-dark"
            HeaderStyle-CssClass="GridHeader" ShowFooter="false">
            <PagerStyle CssClass="pagination-ys" />

            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Collector"   HeaderText="CollectorId" />
                <asp:BoundField DataField="ClosingBalance" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦) ClosingBalance" />
                <%-- <asp:BoundField DataField="BalanceStatus" HeaderText="BalanceStatus" />--%>
                <%--          <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="BalanceTransferWallet.aspx" HeaderText="View" Text="Fund Wallet Now" />--%>
                <asp:HyperLinkField runat="server" NavigateUrl="BalanceTransferWallet.aspx" HeaderText="Fund Collector" Text="Fund Wallet Now" />
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
    <!--  <iframe    src="https://reports.nownowpay.ng/public/question/c5bee7da-4892-455c-867a-8641a7078137"    frameborder="0"    width="1100"    height="500"    allowtransparency></iframe>-->


    <%-- <span data-reactroot="" style="box-sizing: border-box; z-index: 4;">
        <div id="popuArrow" class="PopoverBody PopoverBody--withArrow" style="box-sizing: border-box; pointer-events: auto; min-width: 1em; border: 1px solid #ddd; box-shadow: 0 4px 10px rgba(0, 0, 0, .1); background-color: #fff; border-radius: 4px; position: absolute; margin-top: -220px; margin-left: 10px; display: -webkit-box; display: -ms-flexbox; display: none; -webkit-box-orient: vertical; -webkit-box-direction: normal; -ms-flex-direction: column; flex-direction: column; overflow: hidden; max-width: 270px;">
            <div class="p2" style="box-sizing: border-box; padding: 16px; padding: 1rem; max-width: 250px;">
                <h4 style="font-weight: 700; margin-top: 0; margin-bottom: 0;">Download full results</h4>
                <div class="flex flex-row mt2" style="box-sizing: border-box; display: -webkit-box; display: -ms-flexbox; display: flex; -webkit-box-orient: horizontal; -webkit-box-direction: normal; -ms-flex-direction: row; flex-direction: row; margin-top: 16px; margin-top: 1rem;">
                </div>
            </div>
        </div>
    </span>--%>
</asp:Content>









