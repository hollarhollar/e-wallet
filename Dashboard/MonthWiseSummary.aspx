<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPageTwo.master" Culture="en-NG" UICulture="en-NG" AutoEventWireup="true" EnableEventValidation="false" CodeFile="MonthWiseSummary.aspx.cs" Inherits="Dashboard_MonthWiseSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style2 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 72%;
            left: 374px;
            top: -15px;
            height: 53px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentheading" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-left: 10px;">
        <!-- <iframe    src=" http://reports.nownowpay.ng/public/question/3eb68018-c506-4873-9272-c3db87879b60"   
            frameborder="0"    width="800"    height="500"    allowtransparency></iframe>-->
        <div class="box-header with-border">
            <h3 class="box-title">Month Wise Summary</h3>
        </div>
        <div style="padding-left: 10px;">
            <table class="table borderless" style="border-style: none; border-color: inherit; border-width: medium; width: 101%; height: 133px;">
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
                    <td>
                        <asp:DropDownList runat="server" AppendDataBoundItems="true" ID="lga" CssClass="form-control" AutoPostBack="true" OnTextChanged="getLGAList_Click" Style="margin-bottom: 0px; width: 300px; margin-right: -160px;" Visible="false"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_employer_RIN" runat="server" CssClass="form-control" placeholder="Search" AutoPostBack="true" OnTextChanged="btn_search_Click"></asp:TextBox>
                    </td>
                    <td colspan="3" style="text-align: right;">
                        <asp:Button ID="btn_search" Text="Search" runat="server" CssClass="btn btn-theme" OnClick="btn_search_Click" Visible="false" />
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>Employer Name:</td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txt_employer_name" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>Employer TIN:</td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txt_employer_TIN" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td>Business RIN:</td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txt_business_RIN" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>

            </table>
            <div class="form-group col-lg-12 align-end ">
                <div class="width: 170px ;" align="right" style=" margin-right: 15px;">
                    <asp:DropDownList ID="DropDownListYear" runat="server" CssClass="form-control col-lg-4 align:right" Style="width: 20%">
                        <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                        <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button runat="server" CssClass="btn btn-primary col-lg-2 align-start" Text="Submit" type="submit" ID="Button1" OnClick="btnSubmit_Click" />
                </div>
            </div>
            <asp:GridView ID="grd_collector" runat="server" AllowPaging="True" AllowSorting="True" PageSize="10" PagerSettings-PageButtonCount="5"
                AutoGenerateColumns="False" OnPageIndexChanging="grd_collector_paging"
                CssClass="table table-striped table-bordered table-hover table-dark"
                HeaderStyle-CssClass="GridHeader" ShowFooter="false" Style="margin-top: 68px">
                <PagerStyle CssClass="pagination-ys" />
                <Columns>
                    <asp:BoundField DataField="Local_Government_Area" HeaderText="Local Government Area" />
                    <asp:BoundField DataField="Jan" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)Jan" />
                    <asp:BoundField DataField="Feb" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)Feb" />
                    <asp:BoundField DataField="Mar" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)Mar" />
                    <asp:BoundField DataField="Apr" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)Apr" />
                    <asp:BoundField DataField="May" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)May" />
                    <asp:BoundField DataField="Jun" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)June" />
                    <asp:BoundField DataField="Jul" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)July" />
                    <asp:BoundField DataField="Aug" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)Aug" />
                    <asp:BoundField DataField="Sep" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)Sep" />
                    <asp:BoundField DataField="OCT" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)Oct" />
                    <asp:BoundField DataField="NOV" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)Nov" />
                    <asp:BoundField DataField="Dec" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)Dec" />
                    <asp:BoundField DataField="total" DataFormatString="{0:C}" HtmlEncode="false" HeaderText="(₦)TOTAL" />
                </Columns>
            </asp:GridView>

            <div style="margin-top: -60px; margin-left: 10px;" id="div_paging" runat="server">
                <a>
                    <svg onclick='basicPopup()' class="Icon Icon-downarrow tether-target tether-target-attached-center tether-element-attached-right tether-element-attached-bottom tether-target-attached-top" viewBox="0 0 32 32" width="16" height="16" fill="currentcolor" title="Download this data" name="downarrow" size="16">
                        <path d="M12.2782161,19.3207547 L12.2782161,0 L19.5564322,0 L19.5564322,19.3207547 L26.8346484,19.3207547 L15.9173242,32 L5,19.3207547 L12.2782161,19.3207547 Z"></path></svg></a>
                <br />
                Showing
                <asp:Label runat="server" CssClass="pagination-ys" ID="lblpagefrom"></asp:Label>
                -
                <asp:Label runat="server" ID="lblpageto"></asp:Label>
                entries of
                <asp:Label runat="server" ID="lbltoal"></asp:Label>
                entries
            </div>
            <!--  <iframe    src="http://reports.nownowpay.ng/public/question/c5bee7da-4892-455c-867a-8641a7078137"    frameborder="0"    width="1100"    height="500"    allowtransparency></iframe>-->
        </div>

        <span data-reactroot="" style="box-sizing: border-box; z-index: 4;">
            <div id="popuArrow" class="PopoverBody PopoverBody--withArrow" style="box-sizing: border-box; pointer-events: auto; min-width: 1em; border: 1px solid #ddd; box-shadow: 0 4px 10px rgba(0, 0, 0, .1); background-color: #fff; border-radius: 4px; position: absolute; margin-top: -140px; margin-left: 10px; display: -webkit-box; display: -ms-flexbox; display: none; -webkit-box-orient: vertical; -webkit-box-direction: normal; -ms-flex-direction: column; flex-direction: column; overflow: hidden; max-width: 250px;">
                <div class="p2" style="box-sizing: border-box; padding: 16px; padding: 1rem; max-width: 220px;">
                    <h4 style="font-weight: 700; margin-top: 0; margin-bottom: 0;">Download full results</h4>
                    <div class="flex flex-row mt2" style="box-sizing: border-box; display: -webkit-box; display: -ms-flexbox; display: flex; -webkit-box-orient: horizontal; -webkit-box-direction: normal; -ms-flex-direction: row; flex-direction: row; margin-top: 16px; margin-top: 1rem;">

                        <form class="mr1 text-uppercase text-default" method="GET" action="" style="box-sizing: border-box; color: #727479; margin-right: 8px; margin-right: 0.5rem; text-transform: uppercase; letter-spacing: 0.06em;">
                            <input type="hidden" name="parameters" value="[]" style="font-family: 'Lato', 'Helvetica Neue', Helvetica,
    sans-serif; box-sizing: border-box;">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="downloadCSV" CssClass="Button " Style="font-size: 100%; -webkit-appearance: none; margin: 0; outline: none; display: inline-block; box-sizing: border-box; text-decoration: none; padding: 8px 12px; padding: 0.5rem 0.75rem; background: #fbfcfd; border: 1px solid #ddd; color: #727479; cursor: pointer; text-decoration: none; font-weight: bold; font-family: 'Lato', sans-serif; border-radius: 4px;">

                                <div class="flex layout-centered" style="box-sizing: border-box; display: -webkit-box; display: -ms-flexbox; display: flex; -webkit-box-align: center; -ms-flex-align: center; align-items: center; -webkit-box-pack: center; -ms-flex-pack: center; justify-content: center;">
                                    <svg class="Icon Icon-downarrow mr1" viewbox="0 0 32 32" width="14" height="14" fill="currentcolor" name="downarrow" size="14" style="margin-right: 8px; margin-right: 0.5rem;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <path d="M12.2782161,19.3207547 L12.2782161,0 L19.5564322,0 L19.5564322,19.3207547 L26.8346484,19.3207547 L15.9173242,32 L5,19.3207547 L12.2782161,19.3207547 Z" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </svg><div style="box-sizing: border-box;">DOWNLOAD</div>
                                </div>
                            </asp:LinkButton>
                        </form>
                    </div>
                </div>
            </div>
        </span>

    </div>
</asp:Content>

