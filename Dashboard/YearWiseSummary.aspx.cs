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
using System.IO;
using System.Text.RegularExpressions;

public partial class Dashboard_YearWiseSummary : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(OYOClass.connection);
    DataTable dt_list = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        TheMethod();


        int i = 0;
        if (lga.Items.Count == 0)
        {
            String loginqry = "  Select lga, lga_id from Local_Government_Areas";
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

    protected void TheMethod()
    {
        string loginqry = "";
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
            int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113 
            || int.Parse(Session["type_code"].ToString()) == 115)
        {
           loginqry = "SELECT lga, [2022], [2023], ([2022] + [2023]) AS Total FROM (SELECT Local_Government_Areas.lga AS lga, SUM(CASE WHEN YEAR(tbl_Payer_Trans.created_at) = 2022 THEN tbl_Payer_Trans.Amount ELSE 0 END) AS [2022], SUM(CASE WHEN YEAR(tbl_Payer_Trans.created_at) = 2023 THEN tbl_Payer_Trans.Amount ELSE 0 END) AS [2023] FROM tbl_Payer_Trans INNER JOIN Local_Government_Areas ON Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaid GROUP BY Local_Government_Areas.lga) AS subquery";

        }
        else
        {
            //loginqry = " SELECT * FROM (SELECT  Local_Government_Areas.lga as lga, year(created_at) as [year]  , tbl_Payer_Trans.Amount FROM tbl_Payer_Trans   INNER join Local_Government_Areas on Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaid where tbl_Payer_Trans.lgaId="+Session["lga"].ToString()+ " )  as s  PIVOT ( SUM(Amount) FOR [Year] in ([2019],[2020],[2021], [2022]) )AS piv";
            loginqry = " SELECT lga, [2022], [2023], ([2022] + [2023]) AS Total FROM (SELECT Local_Government_Areas.lga AS lga, SUM(CASE WHEN YEAR(tbl_Payer_Trans.created_at) = 2022 THEN tbl_Payer_Trans.Amount ELSE 0 END) AS [2022], SUM(CASE WHEN YEAR(tbl_Payer_Trans.created_at) = 2023 THEN tbl_Payer_Trans.Amount ELSE 0 END) AS [2023] FROM tbl_Payer_Trans INNER JOIN Local_Government_Areas ON Local_Government_Areas.lga_id = tbl_Payer_Trans.lgaid WHERE tbl_Payer_Trans.lgaId='" + Session["lga"].ToString() + "' GROUP BY Local_Government_Areas.lga) AS subquery";
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

        //dt_lga_detail.Columns.Add("TOTAL");
        ////for (int i = 0; i < dt_lga_detail.Rows.Count; i++)
        ////{
        ////    int total = 1;
        ////    for (int j = 1; j <= (dt_lga_detail.Columns.Count - 2); j++)
        ////    {
        ////        if (!dt_lga_detail.Rows[i][j].ToString().Equals(""))
        ////        {
        ////            total += int.Parse(dt_lga_detail.Rows[i][j].ToString().Remove(dt_lga_detail.Rows[i][j].ToString().Length - 6));
        ////        }
        //////    }
        //////    dt_lga_detail.Rows[i]["total"] = total;
        ////}
        //for (int i = 0; i < dt_lga_detail.Rows.Count; i++)
        //{
        //    int total = 0;
        //    for (int j = 1; j <= (dt_lga_detail.Columns.Count - 2); j++)
        //    {
        //        if (!string.IsNullOrEmpty(dt_lga_detail.Rows[i][j].ToString()))
        //        {
        //            string stringValue = dt_lga_detail.Rows[i][j].ToString();
        //            if (stringValue.Length > 6)
        //            {
        //                stringValue = stringValue.Remove(stringValue.Length - 2);
        //                //total += int.Parse(stringValue);

        //                ////int amount;
        //                //string amountString = dt_lga_detail.Rows[i][j].ToString().Replace("₦", "stringValue").Trim();
        //                int amount;
        //                if (int.TryParse(stringValue, out amount))
        //                {
        //                    //total += amount;
        //                    total += int.Parse(stringValue);
        //                }
        //                //total += int.Parse(amountString);
        //            }
        //        }
        //    }
        //    dt_lga_detail.Rows[i]["total"] = total;
        //}
        if (!string.IsNullOrEmpty(dt_lga_detail.Rows.ToString()))
        {
            string stringValue = dt_lga_detail.Rows.ToString();

            if (stringValue.Length > 6)
            {
                stringValue = stringValue.Remove(stringValue.Length - 2);
            }
        }
       

        grd_collector.DataSource = dt_lga_detail;
        grd_collector.DataBind();

        int pagesize = grd_collector.Rows.Count;
        int from_pg = 1;
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
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        DataTable dt_list_s = new DataTable();
        dt_list_s = (DataTable)Session["dataTableCollector"];
        // DataRow[] filteredRows = dt_list_s.Select("TaxPayerRIN LIKE '" + txt_RIN.Text + "'");
        DataTable dt_filtered = new DataTable();
        DataView dt_v = dt_list_s.DefaultView;
        
        if (Regex.IsMatch(txt_employer_RIN.Text, "^[a-zA-Z0-9/\\s/g]*$"))
        {
            dt_v.RowFilter = " convert(2019,'System.String') like '%" + txt_employer_RIN.Text + "%' or lga like '%" + txt_employer_RIN.Text + "%' or convert(2020,'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(2021,'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(2022,'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(total,'System.String') like '%" + txt_employer_RIN.Text + "%'";
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
            lblmodalbody.Text = "No record for " + txt_employer_RIN.Text + " at the moment";
            TheMethod();
            return;
        }

        if (totalcount < grd_collector.PageSize)
        {
            div_paging.Style.Add("margin-top", "0px");
        }
        else
        {
            div_paging.Style.Add("margin-top", "-60px");
        }
    }


    protected void getLGAList_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");
        if (lga.Text != "Select L.G.A.")
        {
            DataTable dt_list_s = new DataTable();
            dt_list_s = (DataTable)Session["dataTableCollector"];
            DataTable dt_filtered = new DataTable();
            DataView dt_v = dt_list_s.DefaultView;
            if (lga.Text != "")
            {
                dt_v.RowFilter = "convert(2019,'System.String') like '%" + lga.Text + "%' or lga like '%" + lga.Text + "%' or convert(2020,'System.String') like '%" + lga.Text + "%' or convert(2021,'System.String') like '%" + lga.Text + "%' or convert(2022,'System.String') like '%" + lga.Text + "%' or convert(total,'System.String') like '%" + lga.Text + "%'";
            }

            grd_collector.DataSource = dt_v;
            grd_collector.DataBind();

            int pagesize = grd_collector.Rows.Count;
            int to = grd_collector.Rows.Count;
            int totalcount = dt_v.Count;

            if(totalcount <= 0)
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "No record for " + lga.Text +" LGA at the moment";
                TheMethod();
                return;
            }

            if (totalcount < grd_collector.PageSize)
                div_paging.Style.Add("margin-top", "0px");
            else
                div_paging.Style.Add("margin-top", "-60px");

        }
    }



    protected void downloadCSV(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

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