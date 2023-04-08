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
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.IO;

public partial class Dashboard_RevenueBeatReport : System.Web.UI.Page
{
    private DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }

        if (!IsPostBack)
        {
            TheMethod();
        }




    }




    protected void TheMethod()
    {
        string loginqry = "";
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 115 || int.Parse(Session["type_code"].ToString()) == 114)
        {
            loginqry = "select concat(o.first_name,' ', o.last_name) as collector, o.id, l.lga, o.designation, b.Location, sum(p.Amount) as Amount from oyo_Registration o left join Beat b on o.beat_code = convert(varchar(50), b.id) left join tbl_payer_trans p on convert(varchar(50), o.id) = p.loginUserId left join local_government_areas l on o.lga_id = l.lga_id where o.designation = 'collector' group by o.first_name, o.last_name, l.lga,o.id, b.location, o.designation order by location asc";
        }
        else
        {
            //select o.first_name, o.last_name, l.lga, b.Location, sum(p.Amount) from oyo_Registration o left join Beat b on o.beat_code = convert(varchar(50), b.id) left join tbl_payer_trans p on convert(varchar(50), o.id) = p.loginUserId left join local_government_areas l on loginUserId=mobile_no where lgaId=" + Session["lga"].ToString() + " group by o.first_name, o.last_name, l.lga, b.location order by location asc";
            loginqry = "select concat(o.first_name,' ', o.last_name) as collector, o.id, l.lga, o.designation, b.Location, sum(p.Amount) as Amount from oyo_Registration o left join Beat b on o.beat_code = convert(varchar(50), b.id) left join tbl_payer_trans p on convert(varchar(50), o.id) = p.loginUserId left join local_government_areas l on o.lga_id = l.lga_id where o.designation = 'collector' and l.lga_id = '" + Session["lga"].ToString() + " ' group by o.first_name, o.last_name, l.lga,o.id, b.location, o.designation order by location asc";
        }

        SqlConnection con = new SqlConnection(OYOClass.connection);
        SqlCommand cmd_lga = new SqlCommand(loginqry, con);

        DataTable dt_lga_detail = new DataTable();
        con.Open();
        SqlDataAdapter da_lga = new SqlDataAdapter(cmd_lga);
        da_lga.Fill(dt_lga_detail);
        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + dt_lga_detail + ")", true);

        con.Close();
        Session["dataTableCollector"] = dt_lga_detail;
        //dt_lga_detail.Columns["wallet_fund"].DataType = typeof(System.String);
        //string LGAqury = "Select * from Local_Government_Areas where lga_id=" + Session["lga"].ToString();

        //SqlCommand cmd_LGAqury = new SqlCommand(LGAqury, con);

        //DataTable dt_lga_detail_MB = new DataTable();
        //con.Open();
        //SqlDataAdapter da_lga_MB = new SqlDataAdapter(cmd_LGAqury);
        //da_lga_MB.Fill(dt_lga_detail_MB);
        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + dt_lga_detail + ")", true);

        grd_collector.DataSource = dt_lga_detail;
        grd_collector.DataBind();

        int pagesize = grd_collector.Rows.Count;
        int to = grd_collector.Rows.Count;
        int totalcount = grd_collector.Rows.Count;

        if (totalcount < grd_collector.PageSize)
            div_paging.Style.Add("margin-top", "0px");
        else
            div_paging.Style.Add("margin-top", "-60px");
    }
    protected void basicPopup(object sender, EventArgs e)
    {
        txt_startDate.Text = "";
        txt_endDate.Text = "";
        TheMethod();
    }
    protected void btn_search_ClickII(object sender, EventArgs e)
    {
        string st = txt_startDate.Text;
        string ed = txt_endDate.Text;
        string loginqry = "select concat(o.first_name,' ', o.last_name) as collector, o.id, l.lga, b.Location, sum(p.Amount) as Amount from oyo_Registration o left join Beat b on o.beat_code = convert(varchar(50), b.id) left join tbl_payer_trans p on convert(varchar(50), o.id) = p.loginUserId left join local_government_areas l on o.lga_id = l.lga_id where p.OffLine_Trans_Date >= '" + st + "' and p.OffLine_Trans_Date <= '" + ed + "' group by o.first_name, o.last_name, l.lga,o.id, b.location  order by location asc";

        //string loginqry = "select concat(o.first_name,' ', o.last_name) as collector, l.lga, b.Location,p.OffLine_Trans_Date, p.Amount as Amount from oyo_Registration o  left join Beat b on o.beat_code = convert(varchar(50), b.id) left join tbl_payer_trans p on convert(varchar(50), o.id) = p.loginUserId left join local_government_areas l on o.lga_id = l.lga_id  where p.OffLine_Trans_Date >= '" + st + "' and p.OffLine_Trans_Date <= '" + ed + "' order by location asc";


        SqlConnection con = new SqlConnection(OYOClass.connection);
        SqlCommand cmd_lga = new SqlCommand(loginqry, con);

        DataTable dt_lga_detail = new DataTable();
        con.Open();
        SqlDataAdapter da_lga = new SqlDataAdapter(cmd_lga);
        da_lga.Fill(dt_lga_detail);
        con.Close();
        Session["dataTableCollector"] = dt_lga_detail;
        grd_collector.DataSource = dt_lga_detail;
        grd_collector.DataBind();

        int pagesize = grd_collector.Rows.Count;
        int to = grd_collector.Rows.Count;
        int totalcount = grd_collector.Rows.Count;

        if (totalcount < grd_collector.PageSize)
            div_paging.Style.Add("margin-top", "0px");
        else
            div_paging.Style.Add("margin-top", "-60px");


        // lblCollector.Text =
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        DataTable dt_list_s = new DataTable();
        dt_list_s = (DataTable)Session["dataTableCollector"];
        // DataRow[] filteredRows = dt_list_s.Select("TaxPayerRIN LIKE '" + txt_RIN.Text + "'");
        DataTable dt_filtered = new DataTable();
        DataView dt_v = dt_list_s.DefaultView;

        if (Regex.IsMatch(txt_employer_RIN.Text, "^[a-zA-Z0-9]*$"))
        {
            dt_v.RowFilter = "beat_code like '%" + txt_employer_RIN.Text + "%'";
        }
        else
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Invalid characters detected => " + txt_employer_RIN.Text;
            TheMethod();
            return;
        }

        grd_collector.DataSource = dt_v;
        grd_collector.DataBind();

        int pagesize = grd_collector.Rows.Count;
        int to = grd_collector.Rows.Count;
        int totalcount = dt_v.Count;

        if (totalcount <= 0)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "No record for \"" + txt_employer_RIN.Text + "\" at the moment";
            TheMethod();
            return;
        }

        if (totalcount < grd_collector.PageSize)
            div_paging.Style.Add("margin-top", "0px");
        else
            div_paging.Style.Add("margin-top", "-60px");
    }
    protected void downloadCSV(object sender, EventArgs e)
    {

      

        if (grd_collector.DataSource != null)
        {
            if (grd_collector.DataSource is DataTable)
            {
                dt = (DataTable)grd_collector.DataSource;
            }

           
            string attachment = "attachment; filename=RevenueReport.xls";
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
        else
        {
            // handle the case where DataSource is null
        }


    }

    protected void viewbutton_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string Id = lb.ToolTip;
        Session["PassedId"] = Id;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        string passedName = row.Cells[3].Text;
        Session["PassedName"] = passedName;
        Response.Redirect("~/Dashboard/RevenueBeatReportView.aspx");
    }
  

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=exported_data.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
            
                GridView gv = new GridView();
                gv.DataSource = grd_collector; 
                gv.DataBind();
                gv.Columns[0].Visible = false;
                gv.Columns[2].Visible = false;
                gv.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
    }

}