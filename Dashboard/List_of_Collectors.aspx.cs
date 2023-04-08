using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text;

public partial class Dashboard_List_of_Collectors : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(OYOClass.connection);

   
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        if (Session["type_code"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (Session["type_code"] == null || (int.Parse(Session["type_code"].ToString()) != 100 && int.Parse(Session["type_code"].ToString()) != 104 
            && int.Parse(Session["type_code"].ToString()) != 103))
        {
            Session.Abandon();
            Response.Redirect("../Login.aspx");
        }

        DataTable dt_list = new DataTable();
        DataTable dt_list2 = new DataTable();
        DataTable dt_list3 = new DataTable();

        string qry = "";
        if (int.Parse(Session["type_code"].ToString()) == 100)
        {
            qry = "select * from oyo_Registration where mobile_no <> '7838941249' and  type_code = 106  ";
        }
        else if(int.Parse(Session["type_code"].ToString()) == 104)
        {
            qry = "select * from oyo_Registration where mobile_no <> '" + Session["user_id"].ToString() + "' and  type_code=106  and lga_id=" + Session["lga"].ToString();
        }
        else
        {
            qry = "select * from oyo_Registration where mobile_no <> '" + Session["user_id"].ToString() + "' and  type_code=106  and lga_id=" + Session["lga"].ToString();
        }

            SqlDataAdapter Adp = new SqlDataAdapter(qry, con);
            Adp.Fill(dt_list);

        dt_list.Columns.Add(new DataColumn() { ColumnName = "FullName", DataType = typeof(string) });
        dt_list.Columns.Add(new DataColumn() { ColumnName = "WalletBalance", DataType = typeof(string) });
        dt_list.Columns.Add(new DataColumn() { ColumnName = "TotalTransaction", DataType = typeof(string) });
        dt_list.Columns.Add(new DataColumn() { ColumnName = "LastFunded", DataType = typeof(string) });

        foreach (DataRow dr in dt_list.Rows)
        {
            string WalletBalance = "";
            double TotalFunded = 0.0;
            double TotalTransactionAmount = 0.0;
            string firstName = dr["first_name"].ToString();
            string lastName = dr["last_name"].ToString();
            string fullName = firstName + " " + lastName;

            dr.SetField("FullName", fullName);
            //string query = "select top(1) * from addWalletHistory where receiver_mobile='" + dr["mobile_no"] + "' order by created_at DESC";
            string query = "select * from addWalletHistory where receiver_mobile='" + dr["mobile_no"] + "' order by created_at DESC";
            SqlDataAdapter Adp2 = new SqlDataAdapter(query, con);
            dt_list2.Clear();
            Adp2.Fill(dt_list2);
            foreach (DataRow dr2 in dt_list2.Rows)
            {
                // WalletBalance = dr2["walletBalnce"].ToString();
                //TotalFunded += dr2["amount"].ToString();
                TotalFunded += Convert.ToDouble(dr2["amount"]);
            }
               WalletBalance =  OYOClass.getCurrentWalletBalance(dr["mobile_no"].ToString());

            dr.SetField("WalletBalance", WalletBalance);
            dr.SetField("LastFunded", TotalFunded.ToString());


            //string queryTrans = "select top(1)* from tbl where receiver_mobile='" + dr["mobile_no"] + "' order by created_at DESC";
            string queryTrans = "select * from tbl_Payer_Trans where loginUserId = CONVERT(varchar(50), "+ dr["Id"] +")  order by created_at DESC";
            SqlDataAdapter Adp3 = new SqlDataAdapter(queryTrans, con);
            dt_list3.Clear();
            Adp3.Fill(dt_list3);
            foreach (DataRow dr3 in dt_list3.Rows)
            {
                //TotalTransaction += dr3["Amount"].ToString();
                TotalTransactionAmount += Convert.ToDouble(dr3["Amount"]);
            }
            dr.SetField("TotalTransaction", TotalTransactionAmount.ToString());
        }

        if(dt_list.Rows.Count > 0){
            Session["dt_l"] = dt_list;
            grd_list_Collectors.DataSource = dt_list;
            grd_list_Collectors.DataBind();
            return;
        }
        modalinfo.Attributes.Add("class", "modal show");
        lblmodalbody.Text = "No collector assigned to this LGA, please create one";
        return;
    }

    protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    {
        grd_list_Collectors.PageIndex = e.NewPageIndex;
        grd_list_Collectors.DataSource = Session["dt_l"];

        grd_list_Collectors.DataBind();
    }
}