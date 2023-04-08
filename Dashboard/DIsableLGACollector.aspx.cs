using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_DIsableLGACollector : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            binddrop();
            drpusername_SelectedIndexChanged(null, null);
        }
        
        if (Session["type_code"]==null || int.Parse(Session["type_code"].ToString()) != 100 && int.Parse(Session["type_code"].ToString()) != 104)
        {
            Response.Redirect("../Login.aspx");
        }

    }

    public void binddrop()
    {
        String qry = "";

        
        if (int.Parse(Session["type_code"].ToString()) == 100)
        {
            qry = "Select first_name+' '+ last_name as user_name,id as user_id from oyo_Registration where type_code=106 and mobile_no <> '" + Session["user_id"] + "'";
        }
        else
        {
            qry = "Select first_name+' '+ last_name as user_name,id as user_id from oyo_Registration where type_code=106 And lga_id = " + Session["lga"].ToString() + " and mobile_no <> '" + Session["user_id"] + "'";
        }
        DataTable dt = new DataTable();
        dt = OYOClass.fetchdata(qry);
        if (dt.Rows.Count > 0)
        {
            drpusername.DataSource = dt;
            drpusername.DataTextField = "user_name";
            drpusername.DataValueField = "user_id";
            drpusername.DataBind();
            drpusername.Items.Insert(0, "---Select---");
        }

    }
    protected void drpusername_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpusername.SelectedIndex != 0)
        {
           // string qry = "Select status from Usermaster where user_id='" + drpusername.SelectedValue.ToString().Trim() + "'";

            string qry = "Select case when status = 1 then 'A' else  'B' end as status from oyo_registration where id='" + drpusername.SelectedValue.ToString().Trim() + "'";
            DataTable dtuser = new DataTable();
            dtuser = OYOClass.fetchdata(qry);
            if (dtuser.Rows.Count > 0)
            {
                if (dtuser.Rows[0]["status"].ToString().Equals("A"))
                {
                    btnEnable.CssClass = "btn btn-primary disabled";
                    btnDisable.CssClass = "btn btn-primary active";
                }
                else
                {
                    btnEnable.CssClass = "btn btn-primary active";
                    btnDisable.CssClass = "btn btn-primary disabled";
                }
            }
            else
            {
               
            }
        }
        else
        {
          
        }
    }
    protected void btnEnable_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

      //  string qry = "Update usermaster set status = 'A' where user_id='" + drpusername.SelectedValue.ToString().Trim() + "'";

        string qry = "Update oyo_registration set status = 1 ,syncCount='"+synCount.Text.ToString().Trim()+"' where id='" + drpusername.SelectedValue.ToString().Trim() + "'";
        int status = OYOClass.insertupdateordelete(qry);
        if (status > 0)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "User Enabled Successfully!! (" + status + ")";
        }
        else
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Error occured!! (" + status + ")";
        }
    }
    protected void btnDisable_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        string qry = "Update oyo_registration set status = 0  where id='" + drpusername.SelectedValue.ToString().Trim() + "'";
        int status = OYOClass.insertupdateordelete(qry);
        if (status > 0)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "User Disabled Successfully!! (" + status + ")";
        }
        else
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Error occured!! (" + status + ")";
        }
    }
}