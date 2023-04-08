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

public partial class Dashboard_DailySummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!string.IsNullOrEmpty(Request.Params["min"]))
        {
            string min = Request.Params["min"].ToString().Trim();
            string max = Request.Params["max"].ToString().Trim();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg",
   " document.getElementById(\"min-date\").value='" + min + "'; document.getElementById(\"max-date\").value='" + max + "'", true);
        }
       
       

        TheMethod();
    }
   
    protected void TheMethod()
    {
        string loginqry = "";
        if (!string.IsNullOrEmpty(Request.Params["min"]))
        {
            string min = Request.Params["min"].ToString().Trim();
            string max = Request.Params["max"].ToString().Trim();
            if (int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 103 || int.Parse(Session["type_code"].ToString()) == 105)
            {
                loginqry = "select Local_Government_Areas.lga ,format(tbl_Payer_Trans.created_at,'dd/MM/yyyy') as date, SUM(Amount) as amount FROM tbl_Payer_Trans INNER JOIN Local_Government_Areas ON tbl_Payer_Trans.lgaId=Local_Government_Areas.lga_id where lgaId=" + Session["lga"].ToString() + "  where cast(created_at as date) >= Convert(datetime,'"+min+"',103) and cast(created_at as date)<= Convert(datetime,'"+max+"',103) Group BY Local_Government_Areas.lga,tbl_Payer_Trans.created_at  order by created_at DESC ";
            }
            else
            {
                loginqry = "select Local_Government_Areas.lga ,format(created_at,'dd/MM/yyyy') as date, SUM(Amount) as amount FROM tbl_Payer_Trans INNER JOIN Local_Government_Areas ON tbl_Payer_Trans.lgaId=Local_Government_Areas.lga_id where cast(created_at as date) >= Convert(datetime,'" + min + "',103) and cast(created_at as date)<= Convert(datetime,'" + max + "',103) Group BY Local_Government_Areas.lga,created_at  order by created_at  DESC ";
            }
        }
        else
        {
            if (int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 103 || int.Parse(Session["type_code"].ToString()) == 105)
            {
                loginqry = "select Local_Government_Areas.lga ,format(tbl_Payer_Trans.created_at,'dd/MM/yyyy') as date, SUM(Amount) as amount FROM tbl_Payer_Trans INNER JOIN Local_Government_Areas ON tbl_Payer_Trans.lgaId=Local_Government_Areas.lga_id where lgaId=" + Session["lga"].ToString() + "  Group BY Local_Government_Areas.lga,tbl_Payer_Trans.created_at  order by created_at DESC ";
            }
            else
            {
                loginqry = "select Local_Government_Areas.lga ,format(created_at,'dd/MM/yyyy') as date, SUM(Amount) as amount FROM tbl_Payer_Trans INNER JOIN Local_Government_Areas ON tbl_Payer_Trans.lgaId=Local_Government_Areas.lga_id Group BY Local_Government_Areas.lga,created_at  order by created_at  DESC ";
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
        if (dt_lga_detail.Rows.Count == 0)
        {
            return;
        }
        grd_collector.FooterRow.Cells[0].Text = "Total";
        int total = 0;
        for (int i = 0; i < dt_lga_detail.Rows.Count; i++)
        {
          
            
                if (!dt_lga_detail.Rows[i][2].ToString().Equals(""))
                {
                    total += int.Parse(dt_lga_detail.Rows[i][2].ToString().Remove(dt_lga_detail.Rows[i][2].ToString().Length - 6));
                }
            
            
        }
        grd_collector.FooterRow.Cells[2].Text = total.ToString();
        
       
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

        DataTable dt_list_s = new DataTable();
        dt_list_s = (DataTable)Session["dataTableCollector"];
        // DataRow[] filteredRows = dt_list_s.Select("TaxPayerRIN LIKE '" + txt_RIN.Text + "'");
        DataTable dt_filtered = new DataTable();
        DataView dt_v = dt_list_s.DefaultView;
        if (txt_employer_RIN.Text != "")
        {
            dt_v.RowFilter = "lga like '%" + txt_employer_RIN.Text + "%'";


        }


        

        grd_collector.DataSource = dt_v;
        grd_collector.DataBind();
        dt_list_s = dt_v.ToTable();
       
        grd_collector.FooterRow.Cells[0].Text = "Total";
        int total = 0;
        for (int i = 0; i < dt_list_s.Rows.Count; i++)
        {


            if (!dt_list_s.Rows[i][2].ToString().Equals(""))
            {
                total += int.Parse(dt_list_s.Rows[i][2].ToString().Remove(dt_list_s.Rows[i][2].ToString().Length - 6));
            }


        }
        grd_collector.FooterRow.Cells[2].Text = total.ToString();
       
        grd_collector.FooterRow.Cells[0].Text = "Total";
        
        int pagesize = grd_collector.Rows.Count;
        int from_pg = 1;
        int to = grd_collector.Rows.Count;
        int totalcount = dt_v.Count;
      

        if (totalcount < grd_collector.PageSize)
            div_paging.Style.Add("margin-top", "0px");
        else
            div_paging.Style.Add("margin-top", "-60px");
    }
    protected void downloadCSV(object sender, EventArgs e)
    {


        DataTable dt = (DataTable)grd_collector.DataSource;
   
        int total = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            total += int.Parse(dt.Rows[i]["amount"].ToString().Remove(dt.Rows[i]["amount"].ToString().Length - 6));
        }
        DataRow row = dt.NewRow();
        row["LGA"] = "Total";
        row["amount"] = "" + total;
        dt.Rows.Add(row);
       
        
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