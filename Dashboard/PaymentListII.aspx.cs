using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_PaymentListII : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_id"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (Request.QueryString["mobile"] != null)
        {
            try
            {
                HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
                Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

                SqlConnection con = new SqlConnection(OYOClass.connection);
                string mobile = Request.QueryString["mobile"].ToString().Trim();
                SqlCommand cmd_lga = new SqlCommand("select first_name,last_name from oyo_Registration where mobile_no='" + mobile + "'", con);

                DataTable dt_lga_detail = new DataTable();
                con.Open();
                SqlDataAdapter da_lga = new SqlDataAdapter(cmd_lga);
                da_lga.Fill(dt_lga_detail);
                if (dt_lga_detail.Rows.Count == 0)
                {
                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = "No record Found";
                    return;
                }
                
                grd_list_Collectors.Visible = true;
                DataTable dttxns = new DataTable();
                dttxns.Columns.Add("ReceiptNo", typeof(System.String));
                dttxns.Columns.Add("AmountPaid", typeof(System.String));
                dttxns.Columns.Add("RecievedBy", typeof(System.String));
                dttxns.Columns.Add("PaymentDate", typeof(System.String));

                string loginqry = "";
                if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 115 || int.Parse(Session["type_code"].ToString()) == 103 || int.Parse(Session["type_code"].ToString()) == 104)
                {
                    loginqry = "select * From  TransDebit where ReceiverNumber = \'" + mobile + "'";
                }
                con = new SqlConnection(OYOClass.connection);
                cmd_lga = new SqlCommand(loginqry, con);

                dt_lga_detail = new DataTable();
                con.Open();
                da_lga = new SqlDataAdapter(cmd_lga);
                da_lga.Fill(dt_lga_detail);
                if (dt_lga_detail.Rows.Count == 0)
                {
                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = "No record Found";
                    lblmodalbody.Attributes.Clear();
                    return;
                }

                Session["dttxns"] = dt_lga_detail;
                grd_list_Collectors.DataSource = dt_lga_detail;
                grd_list_Collectors.DataBind();
               // return;
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }

    }
    protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    {
        grd_list_Collectors.PageIndex = e.NewPageIndex;
        grd_list_Collectors.DataSource = Session["dttxns"];
        grd_list_Collectors.DataBind();
    }


    protected void downloadCSV(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)grd_list_Collectors.DataSource;
        string attachment = "attachment; filename=DailySummary.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);

        string tab = "";
        foreach (DataColumn dc in dt.Columns)
        {
            Response.Write(tab + dc.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");

        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString());
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
}