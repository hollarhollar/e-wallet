using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_Collectors : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            if (Session["lga"] == null)
            {
                Response.Redirect("../Login.aspx");

            }
            TheMethod();

        }

    }
    protected void TheMethod()
    {
        string query = "";
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
            int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
            || int.Parse(Session["type_code"].ToString()) == 115)
        {
            query = "select distinct o.id, o.first_name, o.last_name, o.mobile_no Phone, g.lga, b.Location Revenuebeat from oyo_Registration o left join tbl_Payer_Trans p on CONVERT(varchar(50), o.id) = p.loginUserId left join Local_Government_Areas g on o.lga_id = g.lga_id left join beat b on o.beat_code = CONVERT(varchar(50), b.id) where type_code = 106";
        }
        else
        {
            query = "select distinct o.id, o.first_name, o.last_name, o.mobile_no Phone, g.lga, b.Location Revenuebeat from oyo_Registration o left join tbl_Payer_Trans p on CONVERT(varchar(50), o.id) = p.loginUserId left join Local_Government_Areas g on o.lga_id = g.lga_id left join beat b on o.beat_code = CONVERT(varchar(50), b.id) where type_code = 106";
        }

        SqlCommand cmd_lga = new SqlCommand(query, con);
        DataTable grdtable = new DataTable();
        con.Open();
        SqlDataAdapter dtTab = new SqlDataAdapter(cmd_lga);
        grdtable.Clear();
        dtTab.Fill(grdtable);
        con.Close();
        Session["dataTableCollector"] = grdtable;
        var dataTableCollector = (DataTable)Session["dataTableCollector"];
        grd_collector.DataSource = dataTableCollector;
        grd_collector.DataBind();
      
       

    }

    protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    {
        grd_collector.PageIndex = e.NewPageIndex;
        grd_collector.DataSource = Session["dataTableCollector"];
        grd_collector.DataBind();
    }
    protected void viewbutton_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string id = lb.ToolTip;
        Session["PassedId"] = id;
        Response.Redirect("~/Dashboard/CollectorDetail.aspx");
    }
}