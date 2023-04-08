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

public partial class Dashboard_RevenueBeatReportView : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        string startDay = txt_startDate.Text;
        string endDay = txt_endDate.Text;

        if (startDay == null && endDay == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {

            TheMethod();
        }

    }
    protected void TheMethod()
    {
        string loginqry = "";
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 115 || int.Parse(Session["type_code"].ToString()) == 114)
        {
            loginqry = "select concat(o.first_name,' ', o.last_name) as collector, l.lga, b.Location,p.OffLine_Trans_Date, sum(p.Amount) as Amount from oyo_Registration o left join Beat b on o.beat_code = convert(varchar(50), b.id) left join tbl_payer_trans p on convert(varchar(50), o.id) = p.loginUserId left join local_government_areas l on o.lga_id = l.lga_id group by o.first_name, o.last_name, l.lga, b.location,p.OffLine_Trans_Date order by location asc";
        }
        else
        {
            //select o.first_name, o.last_name, l.lga, b.Location, sum(p.Amount) from oyo_Registration o left join Beat b on o.beat_code = convert(varchar(50), b.id) left join tbl_payer_trans p on convert(varchar(50), o.id) = p.loginUserId left join local_government_areas l on loginUserId=mobile_no where lgaId=" + Session["lga"].ToString() + " group by o.first_name, o.last_name, l.lga, b.location order by location asc";
            loginqry = "select o.first_name, o.last_name, l.lga, b.Location,p.OffLine_Trans_Date, sum(p.Amount) Amount from oyo_Registration o left join Beat b on o.beat_code = convert(varchar(50), b.id) left join tbl_payer_trans p on convert(varchar(50), o.id) = p.loginUserId left join local_government_areas l on o.lga_id = l.lga_id where lgaId=" + Session["lga"].ToString() + " group by o.first_name, o.last_name, l.lga, b.location,p.OffLine_Trans_Date order by location asc";
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
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string st = txt_startDate.Text;
        string ed = txt_endDate.Text;

        string loginqry = "select concat(o.first_name,' ', o.last_name) as collector, l.lga, b.Location,p.OffLine_Trans_Date, p.Amount as Amount from oyo_Registration o  left join Beat b on o.beat_code = convert(varchar(50), b.id) left join tbl_payer_trans p on convert(varchar(50), o.id) = p.loginUserId left join local_government_areas l on o.lga_id = l.lga_id  where p.OffLine_Trans_Date >= '" + st + "' and p.OffLine_Trans_Date <= '" + ed + "' order by location asc";


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

        if (dt_lga_detail.Rows.Count > 0)
        {
            lblCollector.Text = dt_lga_detail.Rows[0]["collector"].ToString();
        }
        // lblCollector.Text =
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
}