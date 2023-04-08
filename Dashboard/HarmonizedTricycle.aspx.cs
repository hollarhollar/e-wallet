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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Threading;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class Dashboard_HarmonizedTricycle : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        // TheMethod();
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
            //DateTime startDate = Convert.ToDateTime(values);
            //DateTime endDate = Convert.ToDateTime(valueII);
            DateTime startDate = DateTime.ParseExact(values, "MM-dd-yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(valueII, "MM-dd-yyyy", CultureInfo.InvariantCulture);

            //DateTime startDate = DateTime.ParseExact(values, dateFormat, CultureInfo.InvariantCulture);
            //DateTime endDate = DateTime.ParseExact(valueII, dateFormat, CultureInfo.InvariantCulture);
            if (startDate > endDate)
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "Date not allowed: start date is greater than end date ";
                return;
            }

            endDate = endDate.AddDays(1).AddTicks(-1);
            if (int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 108
            || int.Parse(Session["type_code"].ToString()) == 109 || int.Parse(Session["type_code"].ToString()) == 110)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL,  sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + "and created_at >='" + startDate + "' and created_at<'" + endDate + "' and lga_id = " + Session["lga"].ToString() + " group by lga";
            }
            else if (int.Parse(Session["type_code"].ToString()) == 111
                || int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113 || int.Parse(Session["type_code"].ToString()) == 115)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL, sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + " and created_at >='" + startDate + "' and created_at<'" + endDate + "'and lga_id = " + Session["lga"].ToString() + " group by lga";
            }
            else
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL,  sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + "and created_at >='" + startDate + "' and created_at<'" + endDate + "'  group by lga";
            }
        }
        else
        {
            if (int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 108
            || int.Parse(Session["type_code"].ToString()) == 109 || int.Parse(Session["type_code"].ToString()) == 110)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL,  sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + " and lga_id = " + Session["lga"].ToString() + " group by lga";
            }
            else if (int.Parse(Session["type_code"].ToString()) == 111
                || int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113 || int.Parse(Session["type_code"].ToString()) == 115)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL,   sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + " and lga_id = " + Session["lga"].ToString() + " group by lga";
            }
            else
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL,   sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + "  group by lga";
            }
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
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string det = types.SelectedValue.ToString();
        string startDate = txt_startDate.Text;
        string endDate = txt_endDate.Text;

       
        string loginqry = String.Empty;
        var sessionValue = int.Parse(Session["type_code"].ToString());
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        if (endDate.Length > 0 && startDate.Length > 0)
        {
            if (startDate.Length > endDate.Length)
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "Date not allowed: start date is greater than end date ";
                return;
            }
            loginqry = String.Empty;
            if (int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 108
            || int.Parse(Session["type_code"].ToString()) == 109 || int.Parse(Session["type_code"].ToString()) == 110)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL,  sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + "and created_at >='" + startDate + "' and created_at<'" + endDate + "' and lga_id = " + Session["lga"].ToString() + " group by lga";
            }
            else if (int.Parse(Session["type_code"].ToString()) == 111
                || int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113 || int.Parse(Session["type_code"].ToString()) == 115)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL, sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + " and created_at >='" + startDate + "' and created_at<'" + endDate + "'and lga_id = " + Session["lga"].ToString() + " group by lga";
            }
            else
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL,  sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + "and created_at >='" + startDate + "' and created_at<'" + endDate + "'  group by lga";
            }
        }
        else
        {
            if (int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 108
       || int.Parse(Session["type_code"].ToString()) == 109 || int.Parse(Session["type_code"].ToString()) == 110)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL,  sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + " and lga_id = " + Session["lga"].ToString() + " group by lga";
            }
            else if (int.Parse(Session["type_code"].ToString()) == 111
                || int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113 || int.Parse(Session["type_code"].ToString()) == 115)
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL,   sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + " and lga_id = " + Session["lga"].ToString() + " group by lga";
            }
            else
            {
                loginqry = "select Local_Government_Areas.lga as LgaName, sum(Amount) as TOTAL,   sum(Amount)/3 as LGA, sum(Amount)/3 as ANNEWAT, sum(Amount)/3  as EIRS from tbl_Payer_Trans INNER JOIN Local_Government_Areas on tbl_Payer_Trans.lgaId = Local_Government_Areas.lga_id where categoryId IN " + det + "  group by lga";
            }

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
        //Write HTTP output
        Response.End();
    }
}