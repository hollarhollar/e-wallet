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

public partial class Dashboard_CollectionReportResultViewToo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(OYOClass.connection);

    DataTable dt4 = new DataTable();

    public object PassedID { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        //if (CollectorId != null)
        //{
        //    var queryResult1 = Request.QueryString["CollectorId"].ToString();
        //    if (queryResult1 == null)
        //        TheMethodII(queryResult1);

        //}
        //else
        //{
        //    Response.Redirect("../Login.aspx");
        //}

        if (!IsPostBack)
        {
            string det = types.SelectedValue.ToString();
            TheMethodII(Session["PassedID"].ToString(), "");
        }


    }
    protected void TheMethodII(string PassedID, string Values)
    {


        string queryTbl_trans = "select p.Id, p.TransId, c.category, c.sub_category, p.Amount, p.OffLine_Trans_Date, p.AssetId from tbl_Payer_Trans p left join Categories c on p.categoryId=cast(c.id as varchar(50)) left join oyo_Registration a on p.loginUserId = cast(a.id as varchar(50)) where cast( loginUserId as varchar(50)) = '" + PassedID + "'";
        SqlDataAdapter Adp4 = new SqlDataAdapter(queryTbl_trans, con);
        dt4.Clear();
        Adp4.Fill(dt4);
        Session["dt_list4"] = dt4;
        grd_collector.DataSource = dt4;
        grd_collector.DataBind();


        //int pagesize = grd_collector.Rows.Count;
        //int to = grd_collector.Rows.Count;
        //int totalcount = grd_collector.Rows.Count;
        //int totalPages = (int)Math.Ceiling((double)totalcount / pagesize);
        //int currentPage = 1;
        //if (!string.IsNullOrEmpty(Request.QueryString["page"]))
        //{
        //    currentPage = int.Parse(Request.QueryString["page"]);
        //}
        //int startIndex = (currentPage - 1) * pagesize;
        //DataTable currentPageData = dt4.AsEnumerable().Skip(startIndex).Take(pagesize).CopyToDataTable();
        //grd_collector.DataSource = currentPageData;
        //grd_collector.DataBind();


        //if (totalcount < grd_collector.PageSize)
        //    div_paging.Style.Add("margin-top", "0px");
        //else
        //    div_paging.Style.Add("margin-top", "-60px");

        //Session["dt_list4"] = dt4;
        //grd_collector.DataSource = dt4;
       
            lblCollectorId.Text = Session["PassedName"].ToString();
        
       // grd_collector.DataBind();

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
        if (!string.IsNullOrEmpty(Session["PassedID"].ToString()))
        {
            PassedID = Session["PassedID"].ToString();
        }

        //dt1a.Columns.Add(new DataColumn() { ColumnName = "totalTransAmount", DataType = typeof(string) });
        //// dt1b.Columns.Add(new DataColumn() { ColumnName = "totalTransAmount", DataType = typeof(string) });
       
        string querView = String.Empty;
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
            int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
            || int.Parse(Session["type_code"].ToString()) == 115)
        {
            querView = "select p.TransId, c.category, c.sub_category, p.Amount, p.OffLine_Trans_Date , p.AssetId from tbl_Payer_Trans p left join Categories c on p.categoryId = c.id left join oyo_Registration a on p.loginUserId = a.id where cast(p.loginUserId as varchar(50)) = '" + PassedID + "' AND OffLine_Trans_Date BETWEEN convert(date, '" + txt_startDate.Text + "', 101) AND convert(date, '" + txt_endDate.Text + "', 101)";
        }
        else
        {
            querView = "select p.TransId, c.category, c.sub_category, p.Amount, p.OffLine_Trans_Date , p.AssetId from tbl_Payer_Trans p left join Categories c on p.categoryId = c.id left join oyo_Registration a on p.loginUserId = a.id where cast(p.loginUserId as varchar(50)) = '" + PassedID + "' AND lga_id = " + Session["lga"].ToString() + " and OffLine_Trans_Date BETWEEN convert(date, '" + txt_startDate.Text + "', 101) AND convert(date, '" + txt_endDate.Text + "', 101)";
        }
        SqlDataAdapter Adp1a = new SqlDataAdapter(querView, con);
        dt1a.Clear();
        Adp1a.Fill(dt1a);
        grd_collector.DataSource = dt1a;
        grd_collector.DataBind();
        //foreach (DataRow dr1a in dt1a.Rows)
        //{

        //    double totalTransAmount = 0.0;
        //    string queryViewtrans = String.Empty;
        //    if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
        //    int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
        //        || int.Parse(Session["type_code"].ToString()) == 115)
        //    {

        //        //queryV iewtrans = "select p.TransId, c.category, c.sub_category, p.Amount, p.OffLine_Trans_Date, p.AssetId from tbl_Payer_Trans p left join Categories c on p.categoryId=c.id  left join oyo_Registration a  on p.loginUserId = a.id where cast( loginUserId as varchar(50)) = " + dr1a["TransId"] + " AND transDate BETWEEN '" + txt_startDate.Text + "' AND '" + txt_endDate.Text + "' ";
        //        /* queryViewtrans = "select p.TransId, c.category, c.sub_category, p.Amount, p.OffLine_Trans_Date as TranDate, p.AssetId from tbl_Payer_Trans p left join Categories c on p.categoryId=c.id left join oyo_Registration a on p.loginUserId = a.id where cast(p.loginUserId as varchar(50)) = " + dr1a["TransId"] + " AND OffLine_Trans_Date BETWEEN '" + txt_startDate + "' AND '" + txt_endDate.Text + "' "*/
        //        ;
        //        queryViewtrans = "select p.TransId, c.category, c.sub_category, p.Amount, p.OffLine_Trans_Date as TranDate, p.AssetId from tbl_Payer_Trans p left join Categories c on p.categoryId = c.id left join oyo_Registration a on p.loginUserId = a.id where cast(p.loginUserId as varchar(50)) = " + dr1a["Id"] + " AND OffLine_Trans_Date BETWEEN convert(date, '" + txt_startDate.Text + "', 103) AND convert(date, '" + txt_endDate.Text + "', 103)";
        //    }
        //    else
        //    {
        //        queryViewtrans = "select p.TransId, c.category, c.sub_category, p.Amount, p.OffLine_Trans_Date, p.AssetId from tbl_Payer_Trans p left join Categories c on p.categoryId=c.id  left join oyo_Registration a  on p.loginUserId = a.id where cast( loginUserId as varchar(50))  = " + dr1a["PassedID"] + " AND transDate BETWEEN '" + txt_startDate.Text + "' AND '" + txt_endDate.Text + "' ";
        //    }

        //    SqlDataAdapter Adp1b = new SqlDataAdapter(queryViewtrans, con);
        //    dt1b.Clear();
        //    Adp1b.Fill(dt1b);

        //    grd_collector.DataSource = dt1b;
        //    grd_collector.DataBind();

        //    foreach (DataRow dr1b in dt1b.Rows)
        //    {
        //        totalTransAmount += Convert.ToDouble(dr1b["Amount"]);
        //    }

        //    dr1a.SetField("totalTransAmount", totalTransAmount);
        //}




        //grd_collector.DataSource = dt4;
        //grd_collector.DataBind();
    }
    protected void downloadCSV(object sender, EventArgs e)
    {

       // DataTable dt = (DataTable)grd_collector.DataSource;

        string attachment = "attachment; filename=TransactionHistory.xls";
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
        DateTime endDate = DateTime.ParseExact(txt_endDate.Text, "MM/dd/yyyy", culture);
        string url = String.Empty;
        url = "~/Dashboard/CollectionReportSearchByDate.aspx?StartDate=" + startDate + "&EndDate=" + endDate;
        Response.Redirect(url);
    }

    protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    {
        grd_collector.PageIndex = e.NewPageIndex;
        grd_collector.DataSource = Session["dt_list4"];
        grd_collector.DataBind();
    }

    protected void btn_filter_Click(object sender, EventArgs e)
    {
        string det = types.SelectedValue.ToString();

        string queryTbl_trans = "select p.TransId, c.category, c.sub_category, p.Amount, p.OffLine_Trans_Date, p.AssetId from tbl_Payer_Trans p left join Categories c on p.categoryId=c.id  left join oyo_Registration a  on p.loginUserId = a.id where categoryId = '" +  det  + "' and cast( loginUserId as varchar(50)) = '"+ Session["PassedID"].ToString() + "'";
        SqlDataAdapter Adp4 = new SqlDataAdapter(queryTbl_trans, con);
        dt4.Clear();
        Adp4.Fill(dt4);
        Session["dt_list4"] = dt4;

        grd_collector.DataSource = dt4; 
        grd_collector.DataBind();


    }

    protected void btn_search_Click1(object sender, EventArgs e)
    {
        txt_startDate.Text = "";
        txt_endDate.Text = "";
    }


}