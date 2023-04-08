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
using System.Reflection.Emit;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Collections.ObjectModel;

public partial class Dashboard_CollectionReportSearchByDate : System.Web.UI.Page
{
    int Interval = 1;
    SqlConnection con = new SqlConnection(OYOClass.connection);

    DataTable dt_detail = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable dt01 = new DataTable();
    DataTable dt4 = new DataTable();
    DataTable dt5 = new DataTable();
    DataTable dt6 = new DataTable();
    DataTable dTable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }

        var queryResult1 = Request.QueryString["StartDate"].ToString();
        var queryResult2 = Request.QueryString["EndDate"].ToString();
    }
    protected void TheMethod()
    {
        string query = "";
        if (int.Parse(Session["type_code"].ToString()) == 100)
        {
            query = "Select first_name+' '+ last_name Name, mobile_no CollectorId, id ID From oyo_Registration Where type_code = 106 ";
        }
        else
        {
            query = "Select first_name+' '+ last_name Name, mobile_no CollectorId, id ID From oyo_Registration Where type_code = 106 and lga_id = " + Session["lga"].ToString() + "";
        }

        con.Open();

        string qry = "Select * from PaymentRecord where RegId = 0";
        SqlDataAdapter Adp = new SqlDataAdapter(query, con);
        SqlDataAdapter Adpp = new SqlDataAdapter(qry, con);
        Adp.Fill(dt1);
        con.Close();
        Session["dt_l"] = dt1;
        int c = 0;
        int i = 0;

        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        System.Web.UI.WebControls.Label lblmodalbody = (System.Web.UI.WebControls.Label)this.Master.FindControl("lblmodalbody");

        dt1.Columns.Add(new DataColumn() { ColumnName = "totalFunded", DataType = typeof(string) });
        dt1.Columns.Add(new DataColumn() { ColumnName = "totalTransAmount", DataType = typeof(string) });
        dt1.Columns.Add(new DataColumn() { ColumnName = "diffAmount", DataType = typeof(string) });
        dt1.Columns.Add(new DataColumn() { ColumnName = "startDateTrans", DataType = typeof(string) });
        dt1.Columns.Add(new DataColumn() { ColumnName = "endDateTrans", DataType = typeof(string) });

        double totalFunded = 0.0;
        double totalTransAmount = 0.0;
        string startDateTrans = string.Empty;
        string endDateTrans = string.Empty;

        foreach (DataRow dr in dt1.Rows)
        {
            totalTransAmount = 0.0;
            string queryTbl_trans = "Select Amount, MobNo, created_at from tbl_Payer_Trans where loginUserId = CONVERT(varchar(50), " + dr["ID"] + ")";
            SqlDataAdapter Adp4 = new SqlDataAdapter(queryTbl_trans, con);
            dt4.Clear();
            Adp4.Fill(dt4);
            Session["dt_l4"] = dt4;
            foreach (DataRow dr4 in dt4.Rows)
            {
                totalTransAmount += Convert.ToDouble(dr4["Amount"]);
                endDateTrans = dr4["created_at"].ToString();
            }
            if (dt4.Rows.Count > 0)
                startDateTrans = dt4.Rows[0]["created_at"].ToString();

            String CollectorId = dr["CollectorId"].ToString();
            String Name = dr["Name"].ToString();

            totalFunded = 0.0;
            string queryFunded = " Select amount from addWalletHistory where receiver_mobile = '" + dr["CollectorId"] + "'";
            SqlDataAdapter Adp6 = new SqlDataAdapter(queryFunded, con);
            dt6.Clear();
            Adp6.Fill(dt6);
            Session["dt_l6"] = dt6;
            foreach (DataRow dr6 in dt6.Rows)
            {
                totalFunded += Convert.ToDouble(dr6["amount"]);
            }

            double diffAmount = 0.0;
            diffAmount = totalFunded - totalTransAmount;

            dr.SetField("totalTransAmount", totalTransAmount);
            dr.SetField("totalFunded", totalFunded);
            dr.SetField("diffAmount", diffAmount);
            dr.SetField("startDateTrans", startDateTrans);
            dr.SetField("endDateTrans", endDateTrans);

            c++;
        }

        int pagesize = grd_collector.Rows.Count;
        int to = grd_collector.Rows.Count;
        int totalcount = grd_collector.Rows.Count;

        if (totalcount < grd_collector.PageSize)
            div_paging.Style.Add("margin-top", "0px");
        else
            div_paging.Style.Add("margin-top", "-60px");

        Session["dt_ll"] = dt1;
        grd_collector.DataSource = dt1;
        grd_collector.DataBind();

    }

    protected void basicPopup(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        System.Web.UI.WebControls.Label lblmodalbody = (System.Web.UI.WebControls.Label)this.Master.FindControl("lblmodalbody");

        selectedDate.Text = txt_startDate.Text + " To " + txt_endDate.Text;
        CultureInfo culture = new CultureInfo("en-US");
        DateTime startDate = DateTime.ParseExact(txt_startDate.Text, "MM/dd/yyyy", culture);
        DateTime endDate = DateTime.ParseExact(txt_endDate.Text, "MM/dd/yyyy", culture);

        if (endDate  <= startDate)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Date not allowed: start date is greater than end date ";
            return;
        }


       DataTable dt_list_s = new DataTable();
        dt_list_s = (DataTable)Session["dt_ll"];
        DataTable dt_filtered = new DataTable();
        DataView dt_v = dt_list_s.DefaultView;

        //startDateTrans
        //endDateTrans

        //selectedDate.Text = txt_startDate.Text + " To " + txt_endDate.Text;
        //dt_v.RowFilter = ""+ txt_startDate.Text + " >= "+ txt_endDate.Text+" ";
        //dt_v.RowFilter = "startDateTrans = 'startDateTrans' and startDateTrans >= startDateTrans and endDateTrans <= endDateTrans";
        //dt_v.RowFilter = "#" + startDate.ToString("MM/dd/yyyy") + "# >= startDateTrans OR " + endDate.ToString("MM/dd/yyyy") + " <= #endDateTrans#";

        var stDate = txt_startDate.Text;
        var enDate = txt_endDate.Text;

        //dt_v.RowFilter = " (startDateTrans >= #" + startDate + "# And startDateTrans <= #" + endDate + "# ) ";

        grd_collector.DataSource = dt_list_s;
        grd_collector.DataBind();
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