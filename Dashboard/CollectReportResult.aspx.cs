using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Collections.ObjectModel;

public partial class Dashboard_CollectReportResult : System.Web.UI.Page
{

   
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
            // query = "Select first_name+' '+ last_name Name, mobile_no CollectorId, id ID From oyo_Registration Where type_code = 106 ";

            query = "  Select a.first_name+' '+ a.last_name Name, a.mobile_no CollectorId, a.id ID, sum(b.Amount) as totalTransAmount  From oyo_Registration a  left join tbl_Payer_Trans b on CONVERT(varchar(50), a.id) = b.loginUserId  Where a.type_code = 106 group by a.first_name,a.last_name,a.mobile_no,a.id";
        }
        else
        {
            // query = "Select first_name+' '+ last_name Name, mobile_no CollectorId, id ID From oyo_Registration Where type_code = 106 and lga_id = " + Session["lga"].ToString() + "";

            query = " Select a.first_name+' '+ a.last_name Name, a.mobile_no CollectorId, a.id ID, sum(b.Amount) as totalTransAmount " +
" From oyo_Registration a " +
" left join tbl_Payer_Trans b on CONVERT(varchar(50), a.id) = b.loginUserId " +
"  Where a.type_code = 106 and a.lga_id = " + Session["lga"].ToString() + " group by a.first_name,a.last_name,a.mobile_no,a.id ";
        }

        con.Open();

        //check the need for this query that loads Adpp. what is Adpp used for
        //   string qry = "Select * from PaymentRecord where RegId = 0";

        //   SqlDataAdapter Adpp = new SqlDataAdapter(qry, con);

        SqlDataAdapter Adp = new SqlDataAdapter(query, con);
        Adp.Fill(dt1);
        con.Close();
        Session["dt_l"] = dt1;
        int c = 0;
        int i = 0;

        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        System.Web.UI.WebControls.Label lblmodalbody = (System.Web.UI.WebControls.Label)this.Master.FindControl("lblmodalbody");


        //Confirm this is necessary or not
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

    bool isToggled = false;
    protected void summaryLink_Click(object sender, EventArgs e)
    {
        if (isToggled)
        {
            isToggled = false;
        }
        else
        {
            isToggled = true;
        }
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
            querView = "Select distinct Name, id ID, r.mobile_no CollectorId from RegistrationAndTransaction r left join oyo_Registration o on r.mobile_no = o.mobile_no";
        }
        else
        {
            querView = "Select distinct Name, id ID, r.mobile_no CollectorId from RegistrationAndTransaction r left join oyo_Registration o on r.mobile_no = o.mobile_no where lga_id = " + Session["lga"].ToString() + "";
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
                queryViewtrans = "Select  Name, r.mobile_no,o.id ID, r.Amount from RegistrationAndTransaction r left join oyo_Registration o on r.mobile_no = o.mobile_no where id ='" + dr1a["ID"] + "'  AND transDate BETWEEN '" + txt_startDate.Text + "' AND '" + txt_endDate.Text + "' ";
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

    protected void viewbutton_Click(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.LinkButton lb = (System.Web.UI.WebControls.LinkButton)sender;
        string ID = lb.ToolTip;
        System.Web.UI.WebControls.GridViewRow row = (System.Web.UI.WebControls.GridViewRow)lb.NamingContainer;
        Session["PassedID"] = ID;
        //  int i = Convert.ToInt32(row.RowIndex);
        string passedName = row.Cells[0].Text;
        Session["PassedName"] = passedName;
        Response.Redirect("~/Dashboard/CollectionReportResultViewToo.aspx");
    }

    protected void grd_collector_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
