using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.UI;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Dashboard_FundWalletHistory : System.Web.UI.Page
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
                dttxns.Columns.Add("TaxPayerId", typeof(System.String));
                dttxns.Columns.Add("tax_payer_type", typeof(System.String));
                dttxns.Columns.Add("AssetId", typeof(System.String));
                dttxns.Columns.Add("asset_type", typeof(System.String));
                dttxns.Columns.Add("business_sector_type", typeof(System.String));

                Session["dttxns"] = dttxns;


                string loginqry = "";
                if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 115|| int.Parse(Session["type_code"].ToString()) == 103|| int.Parse(Session["type_code"].ToString()) == 104)
                {
                    //loginqry = "select * from addWalletHistory where receiver_mobile='" + mobile + "' order by created_at DESC";
                    loginqry = "select amount, walletBalnce, transactionId, CASE WHEN (walletBalnce - amount) < 0 THEN 0 ELSE (walletBalnce-amount) END AS balanceBeforeFounding, created_at, sender_mobile, receiver_mobile from addWalletHistory where receiver_mobile ='" + mobile + "' order by created_at DESC";
                }
                //if (int.Parse(Session["type_code"].ToString()) == 104)
                //{
                //    loginqry = "select * from addWalletHistory inner join oyo_registration on mobile_no=sender_mobile where sender_mobile='"+Session["user_id"].ToString() +"' and receiver_mobile='" + mobile + "' and lga_id = '" + Session["lga"].ToString() + "' order by created_at DESC";
                //}
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {


    }
    protected void grd_trans_history_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_trans_history.PageIndex = e.NewPageIndex;
        grd_trans_history.DataSource = (DataTable)Session["dttxns"];
        grd_trans_history.DataBind();
    }


}