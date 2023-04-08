using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        if (int.Parse(Session["type_code"].ToString()) != 100 && int.Parse(Session["type_code"].ToString()) != 104
            && int.Parse(Session["type_code"].ToString()) != 115)
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
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No record Found');", true);
                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = "No record Found";
                    return;
                }
                lblCollector.Text = dt_lga_detail.Rows[0]["first_name"].ToString() + " " + dt_lga_detail.Rows[0]["last_name"].ToString() + "(" + mobile + ")";

                divresult.Style.Add("display", "");

                grd_trans_history.Visible = true;
                DataTable dttxns = new DataTable();
                dttxns.Columns.Add("ReceiptNo", typeof(System.String));
                dttxns.Columns.Add("AmountPaid", typeof(System.String));
                dttxns.Columns.Add("RecievedBy", typeof(System.String));
                dttxns.Columns.Add("PaymentDate", typeof(System.String));
              //  dttxns.Columns.Add("business_sector_type", typeof(System.String));

                Session["dttxns"] = dttxns;


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
                    return;
                }
                grd_trans_history.DataSource = dt_lga_detail;
                grd_trans_history.DataBind();
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
    protected void grd_trans_history_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_trans_history.PageIndex = e.NewPageIndex;
        grd_trans_history.DataSource = (DataTable)Session["dttxns"];
        grd_trans_history.DataBind();
    }


}