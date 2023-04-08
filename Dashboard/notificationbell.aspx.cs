using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_notificationbell : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(OYOClass.connection);

    DataTable dt_detail = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable dt01 = new DataTable();
    DataTable dt4 = new DataTable();
    DataTable dt5 = new DataTable();
    DataTable dt6 = new DataTable();
    DataTable dTable = new DataTable();
    public object lga_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }

        if (!IsPostBack)
        {

            TheMethod();
            //int notificationCount = LoadNotificationCount();
            //UpdateNotificationBadge(notificationCount);
            //SendNotification("user123", "You have a new message!");
            //setInterval(updateNotifications, 5000);
        }

    }


    protected void TheMethod()
    {
        string query = "";
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 104)
        {

            //query = "SELECT r.id, concat(r.first_name,' ',r.last_name) as Name, r.mobile_no CollectorId FROM oyo_Registration r LEFT JOIN BalanceManagement b ON r.id = b.EntityID left join tbl_Payer_Trans p on r.id = p.Id LEFT JOIN PaymentRecord m ON m.PaymentRecordId = p.Id where  b.ClosingBalance = 0";
            query = "SELECT Name, Collector, ClosingBalance FROM (SELECT Concat(r.first_name, ' ', r.last_name) AS Name, r.mobile_no AS Collector, 0 AS ClosingBalance FROM oyo_Registration r WHERE r.id IN (SELECT b.EntityID FROM BalanceManagement b WHERE b.ClosingBalance = 0) UNION SELECT NULL AS Name, NULL AS Collector, b.ClosingBalance AS ClosingBalance FROM BalanceManagement b WHERE b.ClosingBalance = 0 ) AS UnionResult where Name is not null ORDER BY Collector DESC";
        }
        else
        {
            //query = "SELECT r.id, concat(r.first_name,' ',r.last_name) as Name, r.mobile_no CollectorId FROM oyo_Registration r LEFT JOIN BalanceManagement b ON r.id = b.EntityID left join tbl_Payer_Trans p on r.id = p.Id LEFT JOIN PaymentRecord m ON m.PaymentRecordId = p.Id where  b.ClosingBalance = 0";
            query = "SELECT Name, Collector, ClosingBalance FROM (SELECT Concat(r.first_name, ' ', r.last_name) AS Name, r.mobile_no AS Collector, 0 AS ClosingBalance FROM oyo_Registration r WHERE r.id IN (SELECT b.EntityID FROM BalanceManagement b WHERE b.ClosingBalance = 0) UNION SELECT NULL AS Name, NULL AS Collector, b.ClosingBalance AS ClosingBalance FROM BalanceManagement b WHERE b.ClosingBalance = 0 ) AS UnionResult where Name is not null ORDER BY Collector DESC";
        }
        SqlConnection con = new SqlConnection(OYOClass.connection);
        SqlCommand cmd_lga = new SqlCommand(query, con);

        DataTable dt_lga_detail = new DataTable();
        con.Open();
        SqlDataAdapter da_lga = new SqlDataAdapter(cmd_lga);
        da_lga.Fill(dt_lga_detail);
        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + dt_lga_detail + ")", true);

        con.Close();
        Session["dataTableCollector"] = dt_lga_detail;
        grd_collector.DataSource = dt_lga_detail;
        grd_collector.DataBind();

        int pagesize = grd_collector.Rows.Count;
        int to = grd_collector.Rows.Count;
        int totalcount = grd_collector.Rows.Count;

        if (totalcount < grd_collector.PageSize)
            div_paging.Style.Add("margin-top", "0px");
        else
            div_paging.Style.Add("margin-top", "-60px");

        string lowBalanceQuery = "";
    

            lowBalanceQuery = "SELECT COUNT(*) lowbalance FROM oyo_Registration r LEFT JOIN BalanceManagement b ON r.id = b.EntityID left join tbl_Payer_Trans p on r.id = p.Id LEFT JOIN PaymentRecord m ON m.PaymentRecordId = p.Id where  b.ClosingBalance = 0";
        
      

        SqlCommand cmd_low_balance = new SqlCommand(lowBalanceQuery, con);
        con.Open();
        int lowBalanceCount = (int)cmd_low_balance.ExecuteScalar();
        con.Close();
        lblCollector.Text = lowBalanceCount.ToString();





    }


    protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    {
        grd_collector.PageIndex = e.NewPageIndex;
        grd_collector.DataSource = Session["dataTableCollector"];
        grd_collector.DataBind();
    }

    //public void SendNotification(string userId, string message)
    //{
    //    // TODO: Implement code to send notification to the specified user
    //}


    //protected void setInterval(string updateNotifications, DataSetDateTime 5000)
    //{

    //}



    //private int LoadNotificationCount()
    //{
    //    int count = 0;
    //    string query = "SELECT COUNT(*) FROM oyo_Registration WHERE  lga_id = 1";
    //    using (SqlConnection connection = new SqlConnection(OYOClass.connection))
    //    {
    //        using (SqlCommand command = new SqlCommand(query, connection))
    //        {
    //            command.Parameters.AddWithValue("1", Session["lga"].ToString());
    //            connection.Open();
    //            count = (int)command.ExecuteScalar();
    //            connection.Close();
    //        }
    //    }
    //    return count;
    //}

    //private void UpdateNotificationBadge(int count)
    //{
    //    var notificationBadge = FindControl("notification-badge") as HtmlGenericControl;
    //    notificationBadge.InnerText = count.ToString();
    //}

    protected void viewbutton_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string ID = lb.ToolTip;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        Session["PassedID"] = ID;
        string passedName = row.Cells[0].Text;
        Session["passedName"] = passedName;
        Response.Redirect("~/Dashboard/CollectionReportResultViewToo.aspx");


    }















}