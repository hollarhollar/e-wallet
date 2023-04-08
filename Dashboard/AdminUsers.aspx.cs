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
 
    private object id;

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
            query = "select id, concat(first_name,' ',last_name) as name, email, designation, mobile_no from oyo_Registration";
        }
        else
        {
            query = "select id, concat(first_name,' ',last_name) as name, email, designation, mobile_no from oyo_Registration Where type_code = 106 and lga_id = " + Session["lga"].ToString() + "";
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
    }


    protected void activebutton_Click(object sender, EventArgs e)
    {
        Session["PassedId"] = id;
        LinkButton lb = (LinkButton)sender;
        lb.CssClass = "btn btn-theme btn-xs md-skip active";
        string st = lb.ToolTip;

        string qry = "Update oyo_Registration set status = '1' where id='" + st + "'";
        //string qry = "Update oyo_registration set status = 1 ,syncCount='" + synCount.Text.ToString().Trim() + "' where id='" + st + "'";
        int status = OYOClass.insertupdateordelete(qry);
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        if (status >= 1)
        {
           
            if (status == 1)
            {
                lb.CssClass = "btn btn-theme btn-info btn-xs md-skip active";
                
                
                    lb.CssClass = "btn btn-info disabled";
                    lb.CssClass = "btn btn-primary active";
                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = "User Enabled Successfully!! (" + status + ")";
                
            }
          
         
        }
        else
        {
            
            if (status< 1)
            {
                lb.CssClass = "btn btn-theme btn-xs md-skip inactive";
            }
            else
            {
                lb.CssClass = "btn btn-primary active";
                lb.CssClass = "btn btn-primary disabled";
            }
            
        }
    }



    protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    {
        grd_collector.PageIndex = e.NewPageIndex;
        grd_collector.DataSource = Session["dt_list4"];
        grd_collector.DataBind();
    }

    private object activebutton_Click()
    {
        throw new NotImplementedException();
    }

    protected void edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string id = lb.ToolTip;
        Session["PassedId"] = id;
        Response.Redirect("~/Dashboard/EditLGACollector.aspx");
    }

    protected void detailsbutton_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string id =lb.ToolTip;
        Session["PassedId"] =id;   
        Response.Redirect("~/Dashboard/AdminUsersDetailsView.aspx");
    }

    protected void downloadCSV(object sender, EventArgs e)
    {

        // DataTable dt = (DataTable)grd_collector.DataSource;

        string attachment = "attachment; filename=RevenueReportSummary.txt";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);

        if (grd_collector.Rows.Count > 0)
        {
            string tab = "";

            for (int i = 0; i < grd_collector.Columns.Count; i++)
            {
                Response.Write(tab + grd_collector.Columns[i].HeaderText);
                tab = "\t";
            }

            Response.Write("\n");

            foreach (GridViewRow dr in grd_collector.Rows)
            {
                tab = "";
                for (int i = 0; i < grd_collector.Columns.Count; i++)
                {
                    Response.Write(tab + dr.Cells[i].Text);
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=RevenueReportSummary.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        if (grd_collector.Rows.Count > 0)
        {
            string tab = "";

            for (int i = 0; i < grd_collector.Columns.Count; i++)
            {
                Response.Write(tab + grd_collector.Columns[i].HeaderText);
                tab = "\t";
            }

            Response.Write("\n");

            foreach (GridViewRow dr in grd_collector.Rows)
            {
                tab = "";
                for (int i = 0; i < grd_collector.Columns.Count; i++)
                {
                    Response.Write(tab + dr.Cells[i].Text);
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

    }
    //protected void drpusername_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //if (drpusername.SelectedIndex != 0)
    //    //{
    //        // string qry = "Select status from Usermaster where user_id='" + drpusername.SelectedValue.ToString().Trim() + "'";

    //        string qry = "Select case when status = 1 then 'A' else  'B' end as status from oyo_registration where id='" + drpusername.SelectedValue.ToString().Trim() + "'";
    //        DataTable dtuser = new DataTable();
    //        dtuser = OYOClass.fetchdata(qry);
    //        if (dtuser.Rows.Count > 0)
    //        {
    //            if (dtuser.Rows[0]["status"].ToString().Equals("A"))
    //            {
    //                btnEnable.CssClass = "btn btn-info disabled";
    //                btnDisable.CssClass = "btn btn-primary active";
    //            }
    //            else
    //            {
    //                btnEnable.CssClass = "btn btn-primary active";
    //                btnDisable.CssClass = "btn btn-primary disabled";
    //            }
    //        }
    //        else
    //        {

    //        }
    //    }
    //    else
    //    {

    //    }
    //}

    //protected void btnEnable_Click(object sender, EventArgs e)
    //{
    //    HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
    //    Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

    //    //  string qry = "Update usermaster set status = 'A' where user_id='" + drpusername.SelectedValue.ToString().Trim() + "'";

    //    string qry = "Update oyo_registration set status = 1 ,syncCount='" + synCount.Text.ToString().Trim() + "' where id='" + drpusername.SelectedValue.ToString().Trim() + "'";
    //    int status = OYOClass.insertupdateordelete(qry);
    //    if (status > 0)
    //    {
    //        modalinfo.Attributes.Add("class", "modal show");
    //        lblmodalbody.Text = "User Enabled Successfully!! (" + status + ")";
    //    }
    //    else
    //    {
    //        modalinfo.Attributes.Add("class", "modal show");
    //        lblmodalbody.Text = "Error occured!! (" + status + ")";
    //    }
    //}
    //protected void btnDisable_Click(object sender, EventArgs e)
    //{
    //    HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
    //    Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

    //    string qry = "Update oyo_registration set status = 0  where id='" + drpusername.SelectedValue.ToString().Trim() + "'";
    //    int status = OYOClass.insertupdateordelete(qry);
    //    if (status > 0)
    //    {
    //        modalinfo.Attributes.Add("class", "modal show");
    //        lblmodalbody.Text = "User Disabled Successfully!! (" + status + ")";
    //    }
    //    else
    //    {
    //        modalinfo.Attributes.Add("class", "modal show");
    //        lblmodalbody.Text = "Error occured!! (" + status + ")";
    //    }
    //}
}