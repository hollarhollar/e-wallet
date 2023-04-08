using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_List_collectors_txn : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(OYOClass.connection);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["type_code"] == null ||( int.Parse(Session["type_code"].ToString()) != 100 &&
            int.Parse(Session["type_code"].ToString()) != 104 && int.Parse(Session["type_code"].ToString()) != 103 && int.Parse(Session["type_code"].ToString()) != 105))
        {
            Response.Redirect("../Login.aspx");
        }
        DataTable dt_list = new DataTable();

        String getUsers = "";
        if(int.Parse(Session["type_code"].ToString()) == 100 ){
            getUsers="select * from oyo_Registration where mobile_no <> '" + Session["user_id"].ToString() + "' and type_code=106 ";
        }  
        else
        {
            getUsers = "select * from oyo_Registration where type_code = 106 and mobile_no <> '" + Session["user_id"].ToString() + "' and lga_id=" + Session["lga"].ToString();
        }
        SqlDataAdapter Adp = new SqlDataAdapter(getUsers, con);
        Adp.Fill(dt_list);

        Session["dt_l"] = dt_list;
        grd_list_Collectors.DataSource = dt_list;
        grd_list_Collectors.DataBind();
    }
    protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    {
        grd_list_Collectors.PageIndex = e.NewPageIndex;
        grd_list_Collectors.DataSource = Session["dt_l"];

        grd_list_Collectors.DataBind();
    }
}