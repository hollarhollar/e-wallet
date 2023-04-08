using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_CollectionbyRevenueStream : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(OYOClass.connection);
    DataTable dt_list = new DataTable();
    int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["type_code"] == null || 
            ( int.Parse(Session["type_code"].ToString()) != 100
            && int.Parse(Session["type_code"].ToString()) != 105
            && int.Parse(Session["type_code"].ToString()) != 109
            && int.Parse(Session["type_code"].ToString()) != 103
            && int.Parse(Session["type_code"].ToString()) != 115
            && int.Parse(Session["type_code"].ToString()) != 104))
        {
            Response.Redirect("../Login.aspx");
        }

        string userQuery = string.Empty;
        var userLGA = int.Parse(Session["type_code"].ToString());
        switch (userLGA)
        {
            case 100:
                userQuery = "Select  Categories.category as category, format(sum(tbl_Payer_Trans.Amount), 'C', 'ng-ng') as amount from Categories join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId group by category";
                break;
            case 115:
                userQuery = "Select  Categories.category as category, format(sum(tbl_Payer_Trans.Amount), 'C', 'ng-ng') as amount from Categories join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId group by category";
                break;
            case 104:
                userQuery = "Select Categories.category as category, format(sum(tbl_Payer_Trans.Amount), 'C', 'ng-ng') as amount from Categories join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId and lga_id = " + Session["lga"] + "  group by category";
                
                break ;
            case 105:
                userQuery = "Select Categories.category as category, format(sum(tbl_Payer_Trans.Amount), 'C', 'ng-ng') as amount from Categories join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId and lga_id = " + Session["lga"] + "  group by category";
                break ;
            case 109:
                userQuery = "Select Categories.category as category, format(sum(tbl_Payer_Trans.Amount), 'C', 'ng-ng') as amount from Categories join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId and lga_id = " + Session["lga"] + "  group by category";
                break;
            case 103:
                userQuery = "Select Categories.category as category, format(sum(tbl_Payer_Trans.Amount), 'C', 'ng-ng') as amount from Categories join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId and lga_id = " + Session["lga"] + "  group by category";
                break;
            default:
                userQuery = string.Empty;
                break ;
        }

        SqlDataAdapter Adp = new SqlDataAdapter(userQuery, con);
        Adp.Fill(dt_list);

        //Session["dt_l"] = dt_list;
        grd_list_Collectors.DataSource = dt_list;
        grd_list_Collectors.DataBind();

        int i = 0;
        if (lga.Items.Count == 0)
        {
            string queryLga = string.Empty;
            switch (userLGA)
            {
                case 100:
                    queryLga = "  Select lga, lga_id from Local_Government_Areas ";
                    break;
                case 115:
                    queryLga = "  Select lga, lga_id from Local_Government_Areas ";
                    break;
                case 104:
                        queryLga = "  Select lga, lga_id from Local_Government_Areas where lga_id = " + Session["lga"].ToString();
                    break;
                    default :
                        queryLga = "  Select lga, lga_id from Local_Government_Areas where lga_id = " + Session["lga"].ToString();
                    break;
            }
            
            SqlCommand cmd = new SqlCommand(queryLga, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dt.Columns.Add("lga_id");
            dt.Columns.Add("lga");
            dt.Rows.Add("0", "Select");
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                String type = dr["lga"].ToString();
                lga.Items.Insert(i, new ListItem(dr["lga"].ToString(), type));
                i++;
            }
        }

    }

    protected void getLGAList_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        if (lga.Text != "Select")
        {
            string query = string.Empty;
            var userLGA = int.Parse(Session["type_code"].ToString());
            switch (userLGA)
            {
                case 100:
                    query = "Select Categories.category as category, format(sum(tbl_Payer_Trans.Amount), 'C', 'ng-ng') as amount from Categories join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId where Local_Government_Areas.lga like '%" + lga.Text + "%'  group by  category";
                    break;
                case 115:
                    query = "Select Categories.category as category,format(sum(tbl_Payer_Trans.Amount), 'C', 'ng-ng') as amount from Categories join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId where Local_Government_Areas.lga like '%" + lga.Text + "%'  group by  category";
                    break;
                case 104:
                    query = "Select Categories.category as category, format(sum(tbl_Payer_Trans.Amount), 'C', 'ng-ng') as amount from Categories join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId where Local_Government_Areas.lga like '%" + lga.Text + "%' and lga_id = " + Session["lga"] + "  group by  category";
                    break;
                case 105:
                    query = "Select Categories.category as category, format(sum(tbl_Payer_Trans.Amount), 'C', 'ng-ng') as amount from Categories join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId where Local_Government_Areas.lga like '%" + lga.Text + "%' and lga_id = " + Session["lga"] + "  group by  category";
                    break;
                case 109:
                    query = "Select Categories.category as category, format(sum(tbl_Payer_Trans.Amount), 'C', 'ng-ng') as amount from Categories join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId where Local_Government_Areas.lga like '%" + lga.Text + "%' and lga_id = " + Session["lga"] + "  group by  category";
                    break;
                default:
                    query = string.Empty;
                    break;
            }
            
         dt_list.Clear();
         SqlDataAdapter Adpp = new SqlDataAdapter(query, con);
         Adpp.Fill(dt_list);
            if(dt_list.Rows.Count < 1)
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "No record Found";
                return;
            }

         //Session["dt_l"] = dt_list;
         grd_list_Collectors.DataSource = dt_list;
         grd_list_Collectors.DataBind();
        }
    }

    protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    {
        grd_list_Collectors.PageIndex = e.NewPageIndex;
        grd_list_Collectors.DataSource = Session["dt_l"];

        grd_list_Collectors.DataBind();
    }

    //protected void btn_search_Click(object sender, EventArgs e)   
    //{
    
    //    String query = "Select Categories.category as category, sum(tbl_Payer_Trans.Amount) as amount from Categories join tbl_Payer_Trans on Categories.id = tbl_Payer_Trans.categoryId join Local_Government_Areas On Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaId where Local_Government_Areas.lga like '%"+ txt_SeachColletor .Text+ "%' group by  category";
    //    dt_list.Clear();
    //    SqlDataAdapter Adpp = new SqlDataAdapter(query, con);
    //    Adpp.Fill(dt_list);

    //    //Session["dt_l"] = dt_list;
    //    grd_list_Collectors.DataSource = dt_list;
    //    grd_list_Collectors.DataBind();

    //}
}