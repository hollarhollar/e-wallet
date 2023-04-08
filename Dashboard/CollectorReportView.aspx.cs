using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_CollectorReportView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(OYOClass.connection);

    DataTable dt4 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        var queryResult1 = Request.QueryString["CollectorId"].ToString();
        if (queryResult1 == null)
            TheMethodII(queryResult1);
    }
    protected void TheMethodII(string collectorId )
    {

        string queryTbl_trans = "Select TransId,C.category,C.sub_category,T.Amount,OffLine_Trans_Date ,AssetId from tbl_Payer_Trans  T LEFT JOIN Categories C On T.categoryId = C.id  where loginUserId = CONVERT(varchar(50), " + collectorId + ")";
        SqlDataAdapter Adp4 = new SqlDataAdapter(queryTbl_trans, con);
        dt4.Clear();
        Adp4.Fill(dt4);
        Session["dt_list4"] = dt4;
      

        int pagesize = grd_collector.Rows.Count;
        int to = grd_collector.Rows.Count;
        int totalcount = grd_collector.Rows.Count;

        if (totalcount < grd_collector.PageSize)
            div_paging.Style.Add("margin-top", "0px");
        else
            div_paging.Style.Add("margin-top", "-60px");

        Session["dt_list4"] = dt4;
        grd_collector.DataSource = dt4;
        grd_collector.DataBind();

    }
    protected void basicPopup(object sender, EventArgs e)
    {

    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        DataTable dt1a = new DataTable();
        DataTable dt1b = new DataTable();
        DataTable dtc = new DataTable();

        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        System.Web.UI.WebControls.Label lblmodalbody = (System.Web.UI.WebControls.Label)this.Master.FindControl("lblmodalbody");

        selectedDate.Text = txt_startDate.Text + " To " + txt_endDate.Text;
        CultureInfo culture = new CultureInfo("en-US");
        DateTime startDate = DateTime.ParseExact(txt_startDate.Text, "MM/dd/yyyy", culture);
        DateTime endDate = DateTime.ParseExact(txt_endDate.Text, "MM/dd/yyyy", culture);

        if (endDate <= startDate)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Date not allowed: start date is greater than end date ";
            return;
        }

        dt1a.Columns.Add(new DataColumn() { ColumnName = "totalTransAmount", DataType = typeof(string) });
        // dt1b.Columns.Add(new DataColumn() { ColumnName = "totalTransAmount", DataType = typeof(string) });

        string querView = String.Empty;
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
            int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
            || int.Parse(Session["type_code"].ToString()) == 115)
        {
            querView = "Select distinct Name, mobile_no CollectorId from RegistrationAndTransaction ";
        }
        else
        {
            querView = "Select distinct Name, mobile_no CollectorId from RegistrationAndTransaction where lga_id = " + Session["lga"].ToString() + "";
        }
        SqlDataAdapter Adp1a = new SqlDataAdapter(querView, con);
        dt1a.Clear();
        Adp1a.Fill(dt1a);
        foreach (DataRow dr1a in dt1a.Rows)
        {
            double totalTransAmount = 0.0;
            string queryViewtrans = String.Empty;
            if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
            int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
                || int.Parse(Session["type_code"].ToString()) == 115)
            {
                queryViewtrans = "Select  Name, mobile_no, Amount from RegistrationAndTransaction where mobile_no = " + dr1a["CollectorId"] + " AND transDate BETWEEN '" + txt_startDate.Text + "' AND '" + txt_endDate.Text + "' ";
            }
            else
            {
                queryViewtrans = "Select  Name, mobile_no, Amount from RegistrationAndTransaction where lga_id = " + Session["lga"].ToString() + " AND mobile_no = " + dr1a["CollectorId"] + " AND transDate BETWEEN '" + txt_startDate.Text + "' AND '" + txt_endDate.Text + "' ";
            }
            SqlDataAdapter Adp1b = new SqlDataAdapter(queryViewtrans, con);
            dt1b.Clear();
            Adp1b.Fill(dt1b);
            foreach (DataRow dr1b in dt1b.Rows)
            {
                totalTransAmount += Convert.ToDouble(dr1b["Amount"]);
            }

            dr1a.SetField("totalTransAmount", totalTransAmount);
        }




        grd_collector.DataSource = dt1a;
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
    protected void btn_search_By_Date_Click(object sender, System.EventArgs e)
    {
        CultureInfo culture = new CultureInfo("en-US");
        DateTime startDate = DateTime.ParseExact(txt_startDate.Text, "MM/dd/yyyy", culture);
        DateTime endDate = DateTime.ParseExact(txt_endDate.Text, "MM/dd/yyyy", culture);
        string url = String.Empty;
        url = "~/Dashboard/CollectionReportSearchByDate.aspx?StartDate=" + startDate + "&EndDate=" + endDate;
        Response.Redirect(url);
    }
}