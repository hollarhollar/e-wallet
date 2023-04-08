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
using System.Activities.Expressions;

public partial class Dashboard_TxnHistoryAPI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_id"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (int.Parse(Session["type_code"].ToString()) != 100 && int.Parse(Session["type_code"].ToString()) != 104 &&
            int.Parse(Session["type_code"].ToString()) != 105 && int.Parse(Session["type_code"].ToString()) != 103)
        {
            Response.Redirect("../Login.aspx");
        }

        if (Request.QueryString["mobile"] != null)
        {
            HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
            Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

            try
            {
                SqlConnection con = new SqlConnection(OYOClass.connection);
                string mobile = Request.QueryString["mobile"].ToString().Trim();
                SqlCommand cmd_lga = new SqlCommand("select id, first_name,last_name from oyo_Registration where mobile_no='" + mobile+"'", con);

                DataTable dt_lga_detail = new DataTable();
                con.Open();
                SqlDataAdapter da_lga = new SqlDataAdapter(cmd_lga);
                da_lga.Fill(dt_lga_detail);
                if (dt_lga_detail.Rows.Count == 0)
                {
                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = " No record Found !!!";
                    return;
                }
                lblCollector.Text = dt_lga_detail.Rows[0]["first_name"].ToString() + " " + dt_lga_detail.Rows[0]["last_name"].ToString()+"("+mobile+")";
                
                divresult.Style.Add("display", "");
                
                grd_trans_history.Visible = true;
                int id = Convert.ToInt32(dt_lga_detail.Rows[0]["id"]);
                string loginqry = "";
                if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 105)
                {
                    //loginqry = "select TransId,Trans_Status,Amount,created_at,closingBalance,TaxPayerId,TaxPayerType,AssetId,Assettype from tbl_Payer_Trans where loginUserId='" + mobile + "' order by created_at DESC";
                    loginqry = "select TransId,Trans_Status, Amount, created_at, cast(closingBalance AS DECIMAL(13, 4)) closingBalance, TaxPayerId,TaxPayerType,AssetId, AssetType.asset_type from tbl_Payer_Trans inner join AssetType ON tbl_Payer_Trans.Assettype = AssetType.asset_id where loginUserId='" + id + "' order by created_at DESC";
                }
              else
                {
                    //loginqry = "select TransId,Trans_Status,Amount,created_at,closingBalance,TaxPayerId,TaxPayerType,AssetId,Assettype from tbl_Payer_Trans where loginUserId='" + mobile + "' and lgaId = '" + Session["lga"].ToString() + "' order by created_at DESC";
                    loginqry = "select TransId,Trans_Status, Amount, created_at, cast(closingBalance AS DECIMAL(13, 4)) closingBalance, TaxPayerId,TaxPayerType,AssetId, AssetType.asset_type from tbl_Payer_Trans inner join AssetType ON tbl_Payer_Trans.Assettype = AssetType.asset_id  where loginUserId=" + id + " and lgaId = '" + Session["lga"].ToString() + "' order by created_at DESC";
                }
                 con = new SqlConnection(OYOClass.connection);
                 cmd_lga = new SqlCommand(loginqry, con);

                 dt_lga_detail = new DataTable();
                con.Open();
                 da_lga = new SqlDataAdapter(cmd_lga);
                da_lga.Fill(dt_lga_detail);
                Session["dttxns"] = dt_lga_detail;
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