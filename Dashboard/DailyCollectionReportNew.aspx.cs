using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//using static ClosedXML.Excel.XLPredefinedFormat;

public partial class Dashboard_DailyCollectionReportNew : System.Web.UI.Page
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

    public string PassedID { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["lga"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            TheMethod();
        }
    }
    protected void TheMethod()
    {
        string query = "";
        if (int.Parse(Session["type_code"].ToString()) == 100)
        {
            query = "select o.id AS ID, concat(o.first_name,' ',o.last_name) AS Name, o.mobile_no AS CollectorId, sum(p.Amount) AS [Total Collection] from oyo_Registration o LEFT JOIN tbl_payer_trans p ON CONVERT(varchar(50), o.id) = p.loginUserId where type_code = 106 group by o.id, o.first_name, o.last_name, o.mobile_no";
        }
        else
        {
            //query = "Select first_name+' '+ last_name Name, mobile_no CollectorId, id ID From oyo_Registration Where type_code = 106 and lga_id = " + Session["lga"].ToString() + "";
            //query = "select o.id AS ID, concat(o.first_name,' ',o.last_name) AS Name, o.mobile_no AS CollectorId, sum(p.Amount) AS [Total Collection] from oyo_Registration o LEFT JOIN tbl_payer_trans p ON CONVERT(varchar(50), o.id) = p.loginUserId Where type_code = 106 and lga_id = " + Session["lga"].ToString() + "";
            query = "select o.id AS ID, concat(o.first_name,' ',o.last_name) AS Name, o.mobile_no AS CollectorId, sum(p.Amount) AS [Total Collection] from oyo_Registration o LEFT JOIN tbl_payer_trans p ON CONVERT(varchar(50), o.id) = p.loginUserId where type_code = 106 and lga_id = " + Session["lga"].ToString() + " group by o.id, o.first_name, o.last_name, o.mobile_no";
        }

        con.Open();

        string qry = "Select * from PaymentRecord where RegId = 0";
        SqlDataAdapter Adp = new SqlDataAdapter(query, con);
        SqlDataAdapter Adpp = new SqlDataAdapter(qry, con);
        Adp.Fill(dt1);
        con.Close();
        Session["dt_l"] = dt1;
        //int c = 0;
        //int i = 0;

        //HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        //System.Web.UI.WebControls.Label lblmodalbody = (System.Web.UI.WebControls.Label)this.Master.FindControl("lblmodalbody");

        //dt1.Columns.Add(new DataColumn() { ColumnName = "totalTransAmount", DataType = typeof(string) });

        //double totalTransAmount = 0.0;

        //foreach (DataRow dr in dt1.Rows)
        //{
        //    totalTransAmount = 0.0;
        //    string queryTbl_trans = "Select Amount, MobNo, OffLine_Trans_Date from tbl_Payer_Trans where loginUserId = CONVERT(varchar(50), " + dr["ID"] + ")";
        //    SqlDataAdapter Adp4 = new SqlDataAdapter(queryTbl_trans, con);
        //    dt4.Clear();
        //    Adp4.Fill(dt4);
        //    Session["dt_l4"] = dt4;
        //    foreach (DataRow dr4 in dt4.Rows)
        //    {
        //        totalTransAmount += Convert.ToDouble(dr4["Amount"]);
        //    }
        //    dr.SetField("totalTransAmount", totalTransAmount);

        //    c++;
        //}


        Session["dt_ll"] = dt1;
        grd_collector.DataSource = dt1;
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
        DataTable dt1a = new DataTable();
        DataTable dt1b = new DataTable();
        DataTable dtc = new DataTable();

        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        System.Web.UI.WebControls.Label lblmodalbody = (System.Web.UI.WebControls.Label)this.Master.FindControl("lblmodalbody");

        //pick date mm/dd/yyyy

        var values = txt_startDate.Text;

        if (values == null)
        {
            return;
        }
        DataTable dt = new DataTable();
        if (values == "0")
        {
            dt.Clear();
            return;
        }
        DateTime endDateTime;
        if (DateTime.TryParseExact(txt_startDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateTime))
        {
            endDateTime = endDateTime.AddDays(1);
            selectedDate.Text = endDateTime.ToString("MM/dd/yyyy");
        }
        else
        {
            selectedDate.Text = "Invalid Date";
        }



        //DateTime date = ParseDate(values);
        //endDateTime = date.AddDays(1).AddTicks(-1);

        dt1a.Columns.Add(new DataColumn() { ColumnName = "totalTransAmount", DataType = typeof(string) });
        // dt1b.Columns.Add(new DataColumn() { ColumnName = "totalTransAmount", DataType = typeof(string) });

        string querView = String.Empty;
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
            int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
            || int.Parse(Session["type_code"].ToString()) == 115)
        {
            //querView = "Select  Name, mobile_no, Amount from RegistrationAndTransaction where mobile_no =\'" + dr1a["CollectorId"] + "' AND  transDate >=\'" + date + "' and transDate<=\'" + endDateTime + "' "; 
            querView = "SELECT o.id AS ID, CONCAT(o.first_name,' ',o.last_name) AS Name, o.mobile_no AS CollectorId, SUM(p.Amount) AS [Total Collection] FROM oyo_Registration o LEFT JOIN tbl_payer_trans p ON CONVERT(varchar(50), o.id) = p.loginUserId WHERE type_code = 106 AND p.OffLine_Trans_Date >= CONVERT(datetime, '" + values + "', 101) AND p.OffLine_Trans_Date <= CONVERT(datetime, '" + endDateTime + "', 103) GROUP BY o.id, o.first_name, o.last_name, o.mobile_no ";
        }
        else
        {
            //querView = "Select distinct Name, mobile_no CollectorId from RegistrationAndTransaction where lga_id = " + Session["lga"].ToString() + "";

            querView = "SELECT o.id AS ID, CONCAT(o.first_name,' ',o.last_name) AS Name, o.mobile_no AS CollectorId, SUM(p.Amount) AS [Total Collection] FROM oyo_Registration o LEFT JOIN tbl_payer_trans p ON CONVERT(varchar(50), o.id) = p.loginUserId WHERE type_code = 106 AND lga_id = " + Session["lga"].ToString() + " and p.OffLine_Trans_Date >= CONVERT(datetime, '" + values + "', 101) AND p.OffLine_Trans_Date <= CONVERT(datetime, '" + endDateTime + "', 103) GROUP BY o.id, o.first_name, o.last_name, o.mobile_no";
        }
        SqlDataAdapter Adp1a = new SqlDataAdapter(querView, con);
        dt1a.Clear();
        Adp1a.Fill(dt1a);
        //foreach (DataRow dr1a in dt1a.Rows)
        //{
        //    //double totalTransAmount = 0.0;
        //    string queryViewtrans = String.Empty;
        //    if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
        //    int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
        //        || int.Parse(Session["type_code"].ToString()) == 115)
        //    {
        //        queryViewtrans = "Select  Name, mobile_no, Amount from RegistrationAndTransaction where mobile_no =\'" + dr1a["CollectorId"] + "' AND  transDate >=\'" + date + "' and transDate<=\'" + endDateTime + "' ";
        //    }
        //    else
        //    {
        //        queryViewtrans = "Select  Name, mobile_no, Amount from RegistrationAndTransaction where lga_id = " + Session["lga"].ToString() + " AND mobile_no = \'" + dr1a["CollectorId"] + "' AND  transDate >=\'" + date + "' and transDate<=\'" + endDateTime + "'";
        //    }
        //    SqlDataAdapter Adp1b = new SqlDataAdapter(queryViewtrans, con);
        //    dt1b.Clear();
        //    Adp1b.Fill(dt1b);
        //    //foreach (DataRow dr1b in dt1b.Rows)
        //    //{
        //    //    totalTransAmount += Convert.ToDouble(dr1b["Amount"]);
        //    //}

        //    //dr1a.SetField("totalTransAmount", totalTransAmount);
        //}




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
        DateTime endDate = DateTime.ParseExact(txt_startDate.Text, "MM/dd/yyyy", culture);
        string url = String.Empty;
        url = "~/Dashboard/CollectionReportSearchByDate.aspx?StartDate=" + startDate + "&EndDate=" + endDate;
        Response.Redirect(url);
    }

    //private static DateTime ParseDate(string s)
    //{
    //    DateTime result;
    //    if (!DateTime.TryParse(s, out result))
    //    {
    //        result = DateTime.ParseExact(s, "MM-dd-yyyyT24:mm:ssK", System.Globalization.CultureInfo.InvariantCulture);
    //        result = result.AddDays(1);
    //    }
    //    return result;

    //}

    //viewbbutton is used to send id and name to Collection Report Result
    protected void viewbutton_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string ID = lb.ToolTip;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        Session["PassedID"] = ID;
        string passedName = row.Cells[0].Text;
        Session["passedName"] = passedName;
        Response.Redirect("~/Dashboard/CollectionReportResultViewToo.aspx");


    }
}
