using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_ViewLowBalanceCollector : System.Web.UI.Page
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
            //SendNotification("user123", "You have a new message!");
            //setInterval(updateNotifications, 5000);
        }

    }


    protected void TheMethod()
    {




        string query = "";
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 104)
        {

            query = "SELECT r.id, concat(r.first_name,' ',r.last_name) as Name, r.mobile_no CollectorId, CASE WHEN SUM(b.ClosingBalance) >= 0 THEN 1 ELSE 0 END AS BalanceStatus FROM oyo_Registration r LEFT JOIN BalanceManagement b ON r.id = b.TransactionID left join tbl_Payer_Trans p on r.id = p.Id LEFT JOIN PaymentRecord m ON m.PaymentRecordId = p.Id WHERE lga_id =' " + Session["lga"].ToString() + "' and type_code = ' " + Session["type_code"].ToString() + "' GROUP BY r.id, r.first_name, r.last_name, r.mobile_no";
        }
        else
        {
            query = "select distinct o.id, o.first_name, o.last_name, o.mobile_no Phone, g.lga, o.address, b.Location Revenuebeat from oyo_Registration o left join tbl_Payer_Trans p on CONVERT(varchar(50), o.id) = p.loginUserId left join Local_Government_Areas g on o.lga_id = g.lga_id left join beat b on o.beat_code = CONVERT(varchar(50), b.id) where type_code = 106 and lga_id = " + Session["lga"].ToString() + "";
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

    }


    protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    {
        grd_collector.PageIndex = e.NewPageIndex;
        grd_collector.DataSource = Session["dataTableCollector"];
        grd_collector.DataBind();
    }

   













}
