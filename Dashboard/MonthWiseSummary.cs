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
using System.Threading;
using System.IO;
using ClosedXML.Excel;

public partial class Dashboard_MonthWiseSummary : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(OYOClass.connection);
    DataTable dt_list = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }


        //((Label)this.Master.FindControl("lblPage")).Text = "";
        //HtmlGenericControl li = new HtmlGenericControl("li");
        //this.Master.FindControl("tabs").Controls.Add(li);

        //HtmlGenericControl anchor = new HtmlGenericControl("a");
        //anchor.Attributes.Add("href", "MonthWiseSummary.aspx");
        //anchor.InnerText = "";

        //li.Controls.Add(anchor);
        string yr = TextBox1.Text.ToString().Trim();
        if (yr != "")
        {
            btnSubmit_Click("",e);
        }
        else
        {
            TheMethod("");
        }
        int i = 0;
        if (lga.Items.Count == 0)
        {
            String loginqry = "Select lga, lga_id from Local_Government_Areas";
            SqlCommand cmd = new SqlCommand(loginqry, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dt.Columns.Add("lga_id");
            dt.Columns.Add("lga");
            dt.Rows.Add("0", "Select L.G.A.");
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                String type = dr["lga"].ToString();
                lga.Items.Insert(i, new ListItem(dr["lga"].ToString(), type));
                i++;
            }
        }

    }

    protected void TheMethod(string yr)
    {
        if (yr == null || yr == "")
            yr = "2022";
        string loginqry = "";
        //if (int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 105)
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
            int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
            || int.Parse(Session["type_code"].ToString()) == 115)
        {
            loginqry = " SELECT * FROM (SELECT Local_Government_Areas.lga as Local_Government_Area , left(datename(month,created_at),3)as [month],tbl_Payer_Trans.Amount  FROM tbl_Payer_Trans INNER join Local_Government_Areas on Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaid where DATEPART(yyyy, created_at)  = " + yr + ") as s PIVOT (SUM(Amount) FOR [MONTH] in([Jan],[Feb],[Mar],[Apr],[May],[Jun],[Jul],[Aug],[Sep],[OCT],[NOV],[Dec]) )AS piv";
        }
        else
        {
            loginqry = " SELECT * FROM (SELECT Local_Government_Areas.lga as Local_Government_Area , left(datename(month,created_at),3)as [month],tbl_Payer_Trans.Amount  FROM tbl_Payer_Trans INNER join Local_Government_Areas on Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaid where DATEPART(yyyy, created_at)  = " + yr + " and tbl_Payer_Trans.lgaId=" + Session["lga"].ToString() + ") as s PIVOT (SUM(Amount) FOR [MONTH] in([Jan],[Feb],[Mar],[Apr],[May],[Jun],[Jul],[Aug],[Sep],[OCT],[NOV],[Dec]) )AS piv";
        }

        SqlConnection con = new SqlConnection(OYOClass.connection);
        SqlCommand cmd_lga = new SqlCommand(loginqry, con);

        DataTable dt_lga_detail = new DataTable();
        con.Open();
        SqlDataAdapter da_lga = new SqlDataAdapter(cmd_lga);
        da_lga.Fill(dt_lga_detail);
        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + dt_lga_detail + ")", true);
        dt_lga_detail.Columns.Add("TOTAL");
        for (int i = 0; i < dt_lga_detail.Rows.Count; i++)
        {
            int total = 0;
            for (int j = 1; j <= (dt_lga_detail.Columns.Count - 2); j++)
            {
                if (!dt_lga_detail.Rows[i][j].ToString().Equals(""))
                {
                    total += int.Parse(dt_lga_detail.Rows[i][j].ToString().Remove(dt_lga_detail.Rows[i][j].ToString().Length - 6));
                }
            }
            dt_lga_detail.Rows[i]["total"] = total;
        }
        con.Close();

        Session["dataTableCollector"] = dt_lga_detail;
        grd_collector.DataSource = dt_lga_detail;
        grd_collector.DataBind();

        int pagesize = grd_collector.Rows.Count;
        int from_pg = 1;
        int to = grd_collector.Rows.Count;
        int totalcount = grd_collector.Rows.Count;
        lblpagefrom.Text = from_pg.ToString();
        lblpageto.Text = (from_pg + pagesize - 1).ToString();
        lbltoal.Text = totalcount.ToString();

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
        NewDownload();
        Response.Redirect("MonthWiseSummary.aspx");
    }

    private string NewDownload()
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");
        DataTable dt = new DataTable();
        try
        {
            dt = (DataTable)grd_collector.DataSource;
        }
        catch (Exception)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Unable to download report at the moment";
            return "0";
        }
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "Customers");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Collector_Report.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                // Response.AddHeader("Refresh", "3; url=YearWiseSummary.aspx");
                Response.Flush();
                Response.End();
                Server.Transfer("/YearWiseSummary.aspx");
            }
        }


        return "";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string yr = TextBox1.Text.ToString().Trim();
        TheMethod(yr);
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

        if (Regex.IsMatch(txt_employer_RIN.Text, "^[a-zA-Z0-9/\\s/g]*$"))
        {
            dt_v.RowFilter = "Local_Government_Area like '%" + txt_employer_RIN.Text + "%' or convert(Jan,'System.String') like '%" + txt_employer_RIN.Text + "%' or Convert(Feb, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(Mar, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(Apr, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(May, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(Jun, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(Jul, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(Aug, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(Sep, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(OCT, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(Nov, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(Dec, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(total, 'System.String') like '%" + txt_employer_RIN.Text + "%'";
        }
        else
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Invalid characters detected => " + txt_employer_RIN.Text;
            TheMethod("");
            return;
        }
        grd_collector.DataSource = dt_v;
        grd_collector.DataBind();

        int pagesize = grd_collector.Rows.Count;
        int from_pg = 1;
        int to = grd_collector.Rows.Count;
        int totalcount = dt_v.Count;
        lblpagefrom.Text = from_pg.ToString();
        lblpageto.Text = (from_pg + pagesize - 1).ToString();
        lbltoal.Text = totalcount.ToString();

        if (totalcount <= 0)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "No record for " + txt_employer_RIN.Text + " at the moment";
            TheMethod("");
            return;
        }

        if (totalcount < grd_collector.PageSize)
            div_paging.Style.Add("margin-top", "0px");
        else
            div_paging.Style.Add("margin-top", "-60px");
    }

    protected void getLGAList_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");
        if (lga.Text != "Select L.G.A.")
        {
            DataTable dt_list_s = new DataTable();
            dt_list_s = (DataTable)Session["dataTableCollector"];
            // DataRow[] filteredRows = dt_list_s.Select("TaxPayerRIN LIKE '" + txt_RIN.Text + "'");
            DataTable dt_filtered = new DataTable();
            DataView dt_v = dt_list_s.DefaultView;

            if (Regex.IsMatch(txt_employer_RIN.Text, "^[a-zA-Z0-9]*$"))
            {
                dt_v.RowFilter = "Local_Government_Area like '%" + lga.Text + "%' or convert(Jan,'System.String') like '%" + lga.Text + "%' or Convert(Feb, 'System.String') like '%" + lga.Text + "%' or convert(Mar, 'System.String') like '%" + lga.Text + "%' or convert(Apr, 'System.String') like '%" + lga.Text + "%' or convert(May, 'System.String') like '%" + lga.Text + "%' or convert(Jun, 'System.String') like '%" + lga.Text + "%' or convert(Jul, 'System.String') like '%" + lga.Text + "%' or convert(Aug, 'System.String') like '%" + lga.Text + "%' or convert(Sep, 'System.String') like '%" + lga.Text + "%' or convert(OCT, 'System.String') like '%" + lga.Text + "%' or convert(Nov, 'System.String') like '%" + lga.Text + "%' or convert(Dec, 'System.String') like '%" + lga.Text + "%' or convert(total, 'System.String') like '%" + lga.Text + "%'";
            }
            else
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "Invalid characters detected => " + txt_employer_RIN.Text;
                //TheMethod();
                return;
            }

            grd_collector.DataSource = dt_v;
            grd_collector.DataBind();

            int pagesize = grd_collector.Rows.Count;
            int from_pg = 1;
            int to = grd_collector.Rows.Count;
            int totalcount = dt_v.Count;
            lblpagefrom.Text = from_pg.ToString();
            lblpageto.Text = (from_pg + pagesize - 1).ToString();
            lbltoal.Text = totalcount.ToString();

            if (totalcount <= 0)
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "No record for " + lga.Text + " LGA at the moment";
                TheMethod("");
                return;
            }

            if (totalcount < grd_collector.PageSize)
                div_paging.Style.Add("margin-top", "0px");
            else
                div_paging.Style.Add("margin-top", "-60px");
        }
    }
    protected void grd_collector_paging(object sender, GridViewPageEventArgs e)
    {

        grd_collector.PageIndex = e.NewPageIndex;
        grd_collector.DataSource = Session["dataTableCollector"];
        grd_collector.DataBind();

        int pagesize = grd_collector.Rows.Count;
        int from_pg = 1;
        int to = grd_collector.Rows.Count;
        int totalcount = grd_collector.Rows.Count;
        lblpagefrom.Text = from_pg.ToString();
        lblpageto.Text = (from_pg + pagesize - 1).ToString();
        lbltoal.Text = totalcount.ToString();

        if (totalcount < grd_collector.PageSize)
            div_paging.Style.Add("margin-top", "0px");
        else
            div_paging.Style.Add("margin-top", "-60px");
        if (e.NewPageIndex + 1 == 1)
        {
            lblpagefrom.Text = "1";
        }
        else
        {
            lblpagefrom.Text = ((grd_collector.Rows.Count * e.NewPageIndex) + 1).ToString();
        }

        lblpageto.Text = ((e.NewPageIndex + 1) * grd_collector.Rows.Count).ToString();

    }
}