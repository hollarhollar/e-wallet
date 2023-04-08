<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Dashboard_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="cnthdng" ContentPlaceHolderID="contentheading" runat="server">
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!-- Main content -->
  
        <section class="content">
    <%--<iframe src="https://reports.nownowpay.ng/public/dashboard/6577100e-017a-4bd6-994e-442261bd7776"  frameborder="0"    width="1100" height="800"  allowtransparency></iframe>--%>
        <!-- =========================================================== -->

      <!-- Small boxes (Stat box) -->
      <div class="row">
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><asp:Label runat="server" Text="0" ID="lblAmountCollected"></asp:Label></h3>
              <h3><asp:Label runat="server" Text="0" ID="lbllowbalance"></asp:Label></h3>

              <p>Total Amount</p>
            </div>
            <div class="icon">
              <i class="fa fa-shopping-cart"></i>
            </div>
            <a href="#" class="small-box-footer">
              More info <i class="fa fa-arrow-circle-right"></i>
            </a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label runat="server" Text="0" ID="lblLgaCollectors"></asp:Label></h3>

              <p>LGA Collectors</p>
            </div>
            <div class="icon">
              <i class="ion ion-stats-bars"></i>
            </div>
            <a href="#" class="small-box-footer">
              More info <i class="fa fa-arrow-circle-right"></i>
            </a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-yellow">
            <div class="inner">
              <h3><asp:Label runat="server" Text="0" ID="lblTaxPayers"></asp:Label></h3>

              <p>Total Tax Payers</p>
            </div>
            <div class="icon">
              <i class="ion ion-person-add"></i>
            </div>
            <a href="#" class="small-box-footer">
              More info <i class="fa fa-arrow-circle-right"></i>
            </a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-red">
            <div class="inner">
              <h3><asp:Label runat="server" Text="0" ID="lblLGA"></asp:Label></h3>

              <p>LGAs</p>
            </div>
            <div class="icon">
              <i class="ion ion-pie-graph"></i>
            </div>
            <a href="#" class="small-box-footer">
              More info <i class="fa fa-arrow-circle-right"></i>
            </a>
          </div>
        </div>
        <!-- ./col -->
      </div>
      <!-- /.row -->

      <!-- =========================================================== -->
       <div class="col-md-6">
       <!-- DONUT CHART -->
          <div class="box box-danger">
            <div class="box-header with-border">
              <h3 class="box-title">LGA WISE COLLECTION</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>

                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>

            <div class="box-body chart-responsive ">
               
              <div class="chart" id="sales-chart" style="height: 300px; position: relative;"></div>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
           </div>
       <div class="col-md-6">
       <!-- BAR CHART -->
           
            <div class="box box-success">
            <div class="box-header with-border">
             <h3 class="box-title">Transactions Amount Done Sector Wise</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>
            <div class="box-body">
                        <iframe    src="https://reports.nownowpay.ng/public/question/e3fa605d-b3cf-4ce9-bb0e-b0d6c5d7fc8c"    frameborder="0"    width="100%"    height="290"    allowtransparency></iframe>

            </div>
            <!-- /.box-body -->
          </div>
         
          <!-- /.box -->
        </div>
      
        </section>
       
        <!-- /.col (LEFT) -->
    <!-- jQuery 3 -->
<script src="../bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- Morris.js charts -->
<script src="../bower_components/raphael/raphael.min.js"></script>
    <!-- ChartJS -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.js"></script>
<script src="../bower_components/morris.js/morris.min.js"></script>


    <!-- page script -->
    <script>
        //DONUT CHART
        var donut = new Morris.Donut({
            element: 'sales-chart',
            resize: true,
            colors: ["#3c8dbc", "#f56954"],
            data: [
                <% if (OrdedoValue > 0)
                   {
             %> { label: "Oredo", value: <%=OrdedoValue%> }<% }%>

              <% if(IkpobaValue>0){
                     if(OrdedoValue>0){
                         %>,<%} %> { label: "Ikpoba-Okha", value:  <%=IkpobaValue%> }<% }%>
                 
              
            ],
            hideHover: 'auto'
        });
                
        // Get the value of the lbllowbalance label
        var lbllowbalance = document.getElementById('<%=lbllowbalance.ClientID%>').innerText;

        // Check if the BalanceStatus is 'true'
        if (lbllowbalance.indexOf('true') !== -1) {
              // Display a pop-up message
              alert('Low balance!');
        }
    


       
    </script>
        
</asp:Content>

