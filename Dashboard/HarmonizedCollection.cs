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


public partial class Dashboard_HarmonizedCollection : System.Web.UI.Page
{
    int Interval = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }

        string det = types.SelectedValue.ToString();
        
        TheMethod(det, "", "");
    }
    protected void TheMethod(string det, string values, string valueII)
    {
        string loginqry = String.Empty;
        var sessionValue = int.Parse(Session["type_code"].ToString());
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");
        if (valueII.Length > 0 && values.Length > 0)
        {
            DateTime startDate = Convert.ToDateTime(values);
            DateTime endDate = Convert.ToDateTime(valueII);
            if (startDate > endDate)
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "Date not allowed: start date is greater than end date ";
                return;
            }

            endDate = endDate.AddDays(1).AddTicks(-1);
            if (int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 108
                || int.Parse(Session["type_code"].ToString()) == 109 || int.Parse(Session["type_code"].ToString()) == 110
                || int.Parse(Session["type_code"].ToString()) == 103)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, format(sum(Amount), 'C', 'ng-ng') as TOTAL,  format(sum(Amount)/4, 'C', 'ng-ng') as LGA,format(sum(Amount)/4, 'C', 'ng-ng') as RTEAN,format(sum(Amount)/4, 'C', 'ng-ng') as NURTW,format(sum(Amount)/4, 'C', 'ng-ng')  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN" + det + "and created_at >='" + startDate + "' and created_at<'" + endDate + "' and lga_id = " + Session["lga"] + " group by lga";
            }
            else if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
                int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
                || int.Parse(Session["type_code"].ToString()) == 115)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, format(sum(Amount), 'C', 'ng-ng') as TOTAL,  format(sum(Amount)/4, 'C', 'ng-ng') as LGA,format(sum(Amount)/4, 'C', 'ng-ng') as RTEAN,format(sum(Amount)/4, 'C', 'ng-ng') as NURTW,format(sum(Amount)/4, 'C', 'ng-ng')  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id  where categoryId IN" + det + "and created_at >='" + startDate + "' and created_at<'" + endDate + "'  group by lga";
            }
            else
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, format(sum(Amount), 'C', 'ng-ng') as TOTAL,  format(sum(Amount)/4, 'C', 'ng-ng') as LGA,format(sum(Amount)/4, 'C', 'ng-ng') as RTEAN,format(sum(Amount)/4, 'C', 'ng-ng') as NURTW,format(sum(Amount)/4, 'C', 'ng-ng')  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id  where categoryId IN" + det + "and created_at >='" + startDate + "' and created_at<'" + endDate + "'and lga_id = " + Session["lga"] + " group by lga";
            }
        }
        else
        {
            if (int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 108
                || int.Parse(Session["type_code"].ToString()) == 109 || int.Parse(Session["type_code"].ToString()) == 110
                || int.Parse(Session["type_code"].ToString()) == 103)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, format(sum(Amount), 'C', 'ng-ng') as TOTAL,  format(sum(Amount)/4, 'C', 'ng-ng') as LGA,format(sum(Amount)/4, 'C', 'ng-ng') as RTEAN,format(sum(Amount)/4, 'C', 'ng-ng') as NURTW,format(sum(Amount)/4, 'C', 'ng-ng')  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id  where categoryId IN" + det + " and lga_id = " + Session["lga"] + " group by lga";
            }
            else if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
                int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
                || int.Parse(Session["type_code"].ToString()) == 115)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, format(sum(Amount), 'C', 'ng-ng') as TOTAL,  format(sum(Amount)/4, 'C', 'ng-ng') as LGA,format(sum(Amount)/4, 'C', 'ng-ng') as RTEAN,format(sum(Amount)/4, 'C', 'ng-ng') as NURTW,format(sum(Amount)/4, 'C', 'ng-ng')  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN" + det + "  group by lga";
            }
            else
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, format(sum(Amount), 'C', 'ng-ng') as TOTAL,  format(sum(Amount)/4, 'C', 'ng-ng') as LGA,format(sum(Amount)/4, 'C', 'ng-ng') as RTEAN,format(sum(Amount)/4, 'C', 'ng-ng') as NURTW,format(sum(Amount)/4, 'C', 'ng-ng')  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id  where categoryId IN" + det + "  and lga_id = " + Session["lga"] + " group by lga";
            }
        }
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
    }
    protected void basicPopup(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string det = types.SelectedValue.ToString();
        string values = txt_startDate.Text.ToString();
        string valueII = txt_endDate.Text.ToString();


        TheMethod(det, values, valueII);
        // HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        // Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        // DataTable dt_list_s = new DataTable();
        // dt_list_s = (DataTable)Session["dataTableCollector"];
        //// DataRow[] filteredRows = dt_list_s.Select("TaxPayerRIN LIKE '" + txt_employer_RIN.Text + "'");
        // DataTable dt_filtered = new DataTable();
        // DataView dt_v = dt_list_s.DefaultView;

        // if (Regex.IsMatch(txt_employer_RIN.Text, "^[a-zA-Z0-9]*$"))
        // {
        //     dt_v.RowFilter = "LgaName like '%" + txt_employer_RIN.Text + "%' or convert(LGA,'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(RTEAN,'System.String')   like '%" + txt_employer_RIN.Text + "%' or convert(NURTW,'System.String')   like '%" + txt_employer_RIN.Text + "%' or  convert(EIRS,'System.String')  like '%" + txt_employer_RIN.Text + "%'  or convert(TOTAL,'System.String') like '%" + txt_employer_RIN.Text + "%'";
        // }
        // else
        // {
        //     modalinfo.Attributes.Add("class", "modal show");
        //     lblmodalbody.Text = "Invalid characters detected => " + txt_employer_RIN.Text;
        //     TheMethod();
        //     return;
        // }

        // grd_collector.DataSource = dt_v;
        // grd_collector.DataBind();

        // int pagesize = grd_collector.Rows.Count;
        // int to = grd_collector.Rows.Count;
        // int totalcount = dt_v.Count;

        // if (totalcount <= 0)
        // {
        //     modalinfo.Attributes.Add("class", "modal show");
        //     lblmodalbody.Text = "No record for " + txt_employer_RIN.Text + " at the moment";
        //     TheMethod();
        //     return;
        // }

        // if (totalcount < grd_collector.PageSize)
        //     div_paging.Style.Add("margin-top", "0px");
        // else
        //     div_paging.Style.Add("margin-top", "-60px");
    }
    protected void btn_filter_Click(object sender, EventArgs e)
    {
        txt_startDate.Text = "";
        txt_endDate.Text = "";
    }
    protected void downloadCSV(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)grd_collector.DataSource;
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