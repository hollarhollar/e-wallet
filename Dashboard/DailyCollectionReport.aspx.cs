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

public partial class Dashboard_DailyCollectionReport : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            TheMethod();
        }
        
    }
    protected void TheMethod()
    {
        string query = "";
        if (int.Parse(Session["type_code"].ToString()) == 100)
        {
            query = "Select a.first_name+' '+ a.last_name Name, a.mobile_no CollectorId, a.id ID, sum(b.Amount) as [Total Collection] From oyo_Registration a left join tbl_Payer_Trans b on CONVERT(varchar(50), a.id) = b.loginUserId group by a.first_name,a.last_name,a.mobile_no,a.id";
        }
        else
        {
            query = "Select a.first_name+' '+ a.last_name Name, a.mobile_no CollectorId, a.id ID, sum(b.Amount) as [Total Collection] From oyo_Registration a left join tbl_Payer_Trans b on CONVERT(varchar(50), a.id) = b.loginUserId group by a.first_name,a.last_name,a.mobile_no,a.id Where type_code = 106 and lga_id = " + Session["lga"].ToString() + "";
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
        //    string queryTbl_trans = "Select Amount,  MobNo, OffLine_Trans_Date from tbl_Payer_Trans where loginUserId = CONVERT(varchar(50), " + dr["ID"] + ")";
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
        DataTable dt1a = new DataTable();
        DataTable dt1b = new DataTable();
        DataTable dtc = new DataTable();

        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        System.Web.UI.WebControls.Label lblmodalbody = (System.Web.UI.WebControls.Label)this.Master.FindControl("lblmodalbody");

        //CultureInfo culture = new CultureInfo("en-US");
       
        var values = txt_startDate.Text;

        if (values == null)
        {
            return;
        }
        DateTime dateTime = DateTime.Now;
        DateTime endDateTime = DateTime.Now;
        DataTable dt = new DataTable();
        if (values == "0")
        {
            dt.Clear();
            return;
        }
        int i = 0;

        DateTime date = ParseDate(values);
        //System.DateTime date = System.DateTime.ParseExact(values, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
        endDateTime = date.AddDays(1).AddTicks(-1);

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
                queryViewtrans = "Select  Name, mobile_no, Amount from RegistrationAndTransaction where mobile_no =\'" + dr1a["CollectorId"] + "' AND  transDate >=\'" + date + "' and transDate<=\'" + endDateTime + "' ";
            }
            else
            {
                queryViewtrans = "Select  Name, mobile_no, Amount from RegistrationAndTransaction where lga_id = " + Session["lga"].ToString() + " AND mobile_no = \'" + dr1a["CollectorId"] + "' AND  transDate >=\'" + date + "' and transDate<=\'" + endDateTime + "'";
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
        string attachment = "attachment; filename=DailySummary.xls";
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
    protected void btn_search_By_Date_Click(object sender, System.EventArgs e)
    {
        CultureInfo culture = new CultureInfo("en-US");
        DateTime startDate = DateTime.ParseExact(txt_startDate.Text, "MM/dd/yyyy", culture);
        DateTime endDate = DateTime.ParseExact(txt_startDate.Text, "MM/dd/yyyy", culture);
        string url = String.Empty;
        url = "~/Dashboard/CollectionReportSearchByDate.aspx?StartDate=" + startDate + "&EndDate=" + endDate;
        Response.Redirect(url);
    }

    private static DateTime ParseDate(string s)
    {
        DateTime result;
        if (!DateTime.TryParse(s, out result))
        {
            result = DateTime.ParseExact(s, "yyyy-MM-ddT24:mm:ssK", System.Globalization.CultureInfo.InvariantCulture);
            result = result.AddDays(1);
        }
        return result;
    }
}