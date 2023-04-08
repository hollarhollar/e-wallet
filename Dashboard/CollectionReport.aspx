<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="CollectionReport.aspx.cs" Inherits="Dashboard_CollectionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-left: 10px;">
        <table class="table borderless" style="width: 100% !important; border: none !important;">
            <tr>

                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td>Search:<asp:TextBox ID="txt_employer_RIN" runat="server" CssClass="form-control" placeholder="Search" AutoPostBack="true" OnTextChanged="btn_search_Click"></asp:TextBox></td>
                <td colspan="3" style="text-align: right;">
                    <asp:Button ID="btn_search" Text="Search" runat="server" CssClass="btn btn-theme" OnClick="btn_search_Click" Visible="false" /></td>
            </tr>
            <tr style="display: none;">
                <td>Employer Name:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txt_employer_name" runat="server" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr style="display: none;">
                <td>Employer TIN:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txt_employer_TIN" runat="server" CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr style="display: none;">
                <td>Business RIN:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txt_business_RIN" runat="server" CssClass="form-control"></asp:TextBox></td>
            </tr>




        </table>
        <asp:GridView ID="grd_collector" ShowHeaderWhenEmpty="true" runat="server" AllowPaging="false"
            AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover table-dark"
            HeaderStyle-CssClass="GridHeader" ShowFooter="false">
            <PagerStyle CssClass="pagination-ys" />
            <Columns>
                <asp:BoundField DataField="collector" HeaderText="Collector" />
                <asp:BoundField DataField="lga" HeaderText="LGA" />
                <asp:BoundField DataField="beat_code" HeaderText="Revenue Beat" />
                <asp:BoundField DataField="wallet_fund" HeaderText="Wallet Fund" />
                <asp:BoundField DataField="revenue_collected" HeaderText="Revenue Collected" />
                <asp:BoundField DataField="wallet_balance" HeaderText="Wallet Balance" />
                <asp:BoundField DataField="mobile_number_1" HeaderText="Mobile Number" />


            </Columns>

        </asp:GridView>


        <div style="margin-top: 60px; margin-left: 10px; display: inline;" id="div_paging" runat="server">
            <a>
                <svg onclick='basicPopup()' class="Icon Icon-downarrow tether-target tether-target-attached-center tether-element-attached-right tether-element-attached-bottom tether-target-attached-top" viewBox="0 0 32 32" width="16" height="16" fill="currentcolor" title="Download this data" name="downarrow" size="16">
                    <path d="M12.2782161,19.3207547 L12.2782161,0 L19.5564322,0 L19.5564322,19.3207547 L26.8346484,19.3207547 L15.9173242,32 L5,19.3207547 L12.2782161,19.3207547 Z"></path></svg></a>


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


    <span data-reactroot="" style="box-sizing: border-box; z-index: 4;">
        <div id="popuArrow" class="PopoverBody PopoverBody--withArrow" style="box-sizing: border-box; pointer-events: auto; min-width: 1em; border: 1px solid #ddd; box-shadow: 0 4px 10px rgba(0, 0, 0, .1); background-color: #fff; border-radius: 4px; position: absolute; margin-top: -220px; margin-left: 10px; display: -webkit-box; display: -ms-flexbox; display: none; -webkit-box-orient: vertical; -webkit-box-direction: normal; -ms-flex-direction: column; flex-direction: column; overflow: hidden; max-width: 270px;">
            <div class="p2" style="box-sizing: border-box; padding: 16px; padding: 1rem; max-width: 250px;">
                <h4 style="font-weight: 700; margin-top: 0; margin-bottom: 0;">Download full results</h4>
                <div class="flex flex-row mt2" style="box-sizing: border-box; display: -webkit-box; display: -ms-flexbox; display: flex; -webkit-box-orient: horizontal; -webkit-box-direction: normal; -ms-flex-direction: row; flex-direction: row; margin-top: 16px; margin-top: 1rem;">

                    <form class="mr1 text-uppercase text-default" method="GET" action="" style="box-sizing: border-box; color: #727479; margin-right: 8px; margin-right: 0.5rem; text-transform: uppercase; letter-spacing: 0.06em;">
                        <input type="hidden" name="parameters" value="[]" style="font-family: 'Lato', 'Helvetica Neue', Helvetica,
    sans-serif; box-sizing: border-box;">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="downloadCSV" CssClass="Button " Style="font-size: 100%; -webkit-appearance: none; margin: 0; outline: none; display: inline-block; box-sizing: border-box; text-decoration: none; padding: 8px 12px; padding: 0.5rem 0.75rem; background: #fbfcfd; border: 1px solid #ddd; color: #727479; cursor: pointer; text-decoration: none; font-weight: bold; font-family: 'Lato', sans-serif; border-radius: 4px;">
            
<div class="flex layout-centered" style="box-sizing: border-box; display: -webkit-box; display: -ms-flexbox; display: flex; -webkit-box-align: center; -ms-flex-align: center; align-items: center; -webkit-box-pack: center; -ms-flex-pack: center; justify-content: center;">
<svg class="Icon Icon-downarrow mr1" viewbox="0 0 32 32" width="14" height="14" fill="currentcolor" name="downarrow" size="14" style="margin-right: 8px; margin-right: 0.5rem;">
<path d="M12.2782161,19.3207547 L12.2782161,0 L19.5564322,0 L19.5564322,19.3207547 L26.8346484,19.3207547 L15.9173242,32 L5,19.3207547 L12.2782161,19.3207547 Z"/>
</svg><div style="box-sizing: border-box;">DOWNLOAD</div></div> </asp:LinkButton>
                    </form>
                </div>
            </div>
        </div>
    </span>



</asp:Content>

