using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_Admin_Users : System.Web.UI.Page
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
            if (Session["lga"]== null)
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
            query = "select lga_id, concat(first_name,' ',last_name) as name, email, designation, mobile_no from oyo_Registration";
        }
        else
        {
            query = "select lga_id, concat(first_name,' ',last_name) as name, email, designation, mobile_no from oyo_Registration Where type_code = 106 and lga_id = " + Session["lga"].ToString() + "";
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
        //grd_collector.DataSource = dtTab;
        //grd_collector.DataBind();

        int pagesize = grd_collector.Rows.Count;
        int to = grd_collector.Rows.Count;
        int totalcount = grd_collector.Rows.Count;

        if (totalcount < grd_collector.PageSize)
            div_paging.Style.Add("margin-top", "0px");
        else
            div_paging.Style.Add("margin-top", "-60px");

    }


    protected void activebutton_Click(object sender, EventArgs e)
    {
        Session["PassedId"] = lga_id;
        LinkButton lb = (LinkButton)sender;
        lb.CssClass = "btn btn-theme btn-xs md-skip active";
        string st = lb.ToolTip;

        string qry = "Update oyo_Registration set status = '1' where lga_id='" + st + "'";
        int status = OYOClass.insertupdateordelete(qry);
      

        if (status > 1)
        {
            lb.CssClass = "btn btn-theme btn-xs md-skip active";
        }
        else
        {
            lb.CssClass = "btn btn-theme btn-xs md-skip inactive";
        }
    }





    private object activebutton_Click()
    {
        throw new NotImplementedException();
    }

    protected void edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string lga_id = lb.ToolTip;
        Session["PassedId"] = lga_id;
        Response.Redirect("~/Dashboard/EditLGACollector.aspx");
    }

    protected void detailsbutton_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string lga_id =lb.ToolTip;
        Session["PassedId"] =lga_id;   
        Response.Redirect("~/Dashboard/AdminUsersDetailsView.aspx");
    }
}