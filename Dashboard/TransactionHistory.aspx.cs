using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_TransactionHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            div_res.Attributes.Add("style", "display:none");
            lbl_no_record.Visible = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string qry = "select *,case when Trans_Status=1 then 'Success' else 'Failed'  end as T_Status, first_name +' '+ middle_name+' '+last_name as TaxPayerName FROM tbl_Payer_Trans a, Individuals b, Asset_Type c where a.TaxPayerId=b.user_rin and a.AssetType=c.asset_id and taxpayerid='" + txt_taxpayerRIN.Text.ToString().Trim() + "'";
        DataTable dtuser = new DataTable();
        dtuser = OYOClass.fetchdata(qry);
        if (dtuser.Rows.Count > 0)
        {
            div_res.Attributes.Add("style", "display:block");
            txt_taxpayer_id.Text = dtuser.Rows[0]["taxPayerId"].ToString();
            txt_taxpayer_name.Text = dtuser.Rows[0]["TaxPayerName"].ToString();
            lbl_no_record.Visible = false;
            grd_trans_history.DataSource = dtuser;
            grd_trans_history.DataBind();
        }
        else
        {
            lbl_no_record.Visible = true;
            div_res.Attributes.Add("style", "display:none");
        }

        Session["dt_trans_history"] = dtuser;

    }
    protected void grd_trans_history_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        

    }
    protected void grd_trans_history_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_trans_history.PageIndex = e.NewPageIndex;
        grd_trans_history.DataSource = Session["dt_trans_history"];

        grd_trans_history.DataBind();
    }
}