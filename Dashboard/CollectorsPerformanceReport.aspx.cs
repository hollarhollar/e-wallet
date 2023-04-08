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

public partial class Dashboard_CollectorsPerformanceReport : System.Web.UI.Page
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
            string selectedStatus = DropDownStatus.SelectedValue;
            string stat = selectedStatus;
            string selectedStatus2 = DropDownStatus.SelectedValue;
            string stat2 = selectedStatus2;
            TheMethod();


        }
    }
    protected void TheMethod()
    {
        //query is running weekly report

        DateTime currentDate = DateTime.Now;
        DateTime currentMonday = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
        DateTime currentFriday = currentMonday.AddDays(4);

        string CurrentWeekStart = currentMonday.ToString("MM/dd/yyyy");
        string CurrentWeekEnd = currentFriday.ToString("MM/dd/yyyy");

        string query = "";
        if (int.Parse(Session["type_code"].ToString()) == 100)
        {
            query = "select Name,collector,bestperformedweek,transactions,Current_Week,transactions_current_week, CASE WHEN transactions_current_week > transactions THEN 'Best Performer' WHEN transactions_current_week < transactions * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status from (SELECT distinct CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(day, 4, DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date)), 101) AS bestperformedweek, SUM(p.Amount) AS transactions, ('" + CurrentWeekStart + "' + ' to ' + '" + CurrentWeekEnd + "') AS Current_Week, (SELECT distinct SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND OffLine_Trans_Date >= '" + CurrentWeekStart + "' AND OffLine_Trans_Date <= '" + CurrentWeekEnd + "') AS transactions_current_week FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.OffLine_Trans_Date IS NOT NULL GROUP BY o.id, o.mobile_no, o.first_name, o.last_name, DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date))a where transactions = (SELECT MAX(transactions) FROM (SELECT distinct CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(day, 4, DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date)), 101) AS bestperformedweek, SUM(p.Amount) AS transactions, ('" + CurrentWeekStart + "' + ' to ' + '" + CurrentWeekEnd + "')  AS Current_Week, (SELECT distinct SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND OffLine_Trans_Date >= '" + CurrentWeekStart + "' AND OffLine_Trans_Date <= '" + CurrentWeekEnd + "') AS transactions_current_week FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.OffLine_Trans_Date IS NOT NULL GROUP BY o.id, o.mobile_no, o.first_name, o.last_name, DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date)) b WHERE b.Collector = a.Collector) GROUP BY Name, Collector, bestperformedweek,transactions,Current_Week,transactions_current_week";
            //query = "select Name,collector,bestperformedweek,transactions,Current_Week,transactions_current_week,status from (SELECT distinct CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(day, 4, DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date)), 101) AS bestperformedweek, SUM(p.Amount) AS transactions, ('" + CurrentWeekStart + "' + ' to ' + '" + CurrentWeekEnd + "') AS Current_Week, (SELECT distinct SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND OffLine_Trans_Date >= '" + CurrentWeekStart + "' AND OffLine_Trans_Date <= '" + CurrentWeekEnd + "') AS transactions_current_week, CASE WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.OffLine_Trans_Date IS NOT NULL GROUP BY o.id, o.mobile_no, o.first_name, o.last_name, DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date) HAVING DATEPART(dw, MAX(p.OffLine_Trans_Date)) <= 5 AND CASE WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END in ('Threshold', 'Underperforming', 'BestPerformer')) a where transactions = (SELECT MAX(transactions) FROM (SELECT distinct CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(day, 4, DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date)), 101) AS bestperformedweek, SUM(p.Amount) AS transactions, ('" + CurrentWeekStart + "' + ' to ' + '" + CurrentWeekEnd + "') AS Current_Week, (SELECT distinct SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND OffLine_Trans_Date >= '" + CurrentWeekStart + "' AND OffLine_Trans_Date <= '" + CurrentWeekEnd + "') AS transactions_current_week, CASE WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.OffLine_Trans_Date IS NOT NULL GROUP BY o.id, o.mobile_no, o.first_name, o.last_name, DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date) HAVING DATEPART(dw, MAX(p.OffLine_Trans_Date)) <= 5 AND CASE WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END in ('Threshold', 'Underperforming', 'BestPerformer')) b WHERE b.Collector = a.Collector) GROUP BY Name, Collector, bestperformedweek,transactions,Current_Week,transactions_current_week,status";
        }
        else
        {
            //query = "SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at) + 4, 101) AS bestperformedweek, SUM(p.Amount) AS transactions, CONVERT(VARCHAR(10), DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 4), 101) AS Current_Week, SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) AS transactions_current_week, CASE WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'Best Performer' WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.created_at IS NOT NULL GROUP BY o.mobile_no, o.first_name, o.last_name, DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at) HAVING DATEPART(dw, MAX(p.created_at)) <= 5 AND CASE WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'Best Performer' WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END = 'Threshold' union all SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at) + 4, 101) AS bestperformedweek, SUM(p.Amount) AS transactions, CONVERT(VARCHAR(10), DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 4), 101) AS Current_Week, SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) AS transactions_current_week, CASE WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'Best Performer' WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.created_at IS NOT NULL GROUP BY o.mobile_no, o.first_name, o.last_name, DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at) HAVING DATEPART(dw, MAX(p.created_at)) <= 5 AND CASE WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'Best Performer' WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END = 'Underperforming' union all SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at) + 4, 101) AS bestperformedweek, SUM(p.Amount) AS transactions, CONVERT(VARCHAR(10), DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 4), 101) AS Current_Week, SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) AS transactions_current_week,  CASE WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'Best Performer' WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status  FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.created_at IS NOT NULL GROUP BY o.mobile_no, o.first_name, o.last_name, DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at) HAVING DATEPART(dw, MAX(p.created_at)) <= 5 AND CASE WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'Best Performer' WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END = 'Best Performer'";
            query = " SELECT distinct CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(day, 4, DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date)), 101) AS bestperformedweek, SUM(p.Amount) AS transactions, ('" + CurrentWeekStart + "' + ' to ' + '" + CurrentWeekStart + "') AS Current_Week, (SELECT distinct SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND OffLine_Trans_Date >= '" + CurrentWeekStart + "' AND OffLine_Trans_Date <= '" + CurrentWeekStart + "') AS transactions_current_week, CASE WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.OffLine_Trans_Date IS NOT NULL GROUP BY o.id, o.mobile_no, o.first_name, o.last_name, DATEADD(dd, -(DATEPART(dw, p.OffLine_Trans_Date)-2), p.OffLine_Trans_Date) HAVING DATEPART(dw, MAX(p.OffLine_Trans_Date)) <= 5 AND CASE WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END in ('Threshold', 'Underperforming', 'BestPerformer')";
        }

        con.Open();

        

        SqlDataAdapter Adp = new SqlDataAdapter(query, con);
        Adp.Fill(dt1);
        con.Close();
        Session["dt_l"] = dt1;
        //dt4 = dt4.DefaultView.ToTable( /*distinct*/ true);


        //int pagesize = grd_collector.Rows.Count;
        //int to = grd_collector.Rows.Count;
        //int totalcount = grd_collector.Rows.Count;

        //if (totalcount < grd_collector.PageSize)
        //    div_paging.Style.Add("margin-top", "0px");
        //else
        //    div_paging.Style.Add("margin-top", "-60px");
        Session["dt_ll"] = dt1;
        grd_collector.DataSource = dt1;
        grd_collector.DataBind();

        //query is running monthly report
        string query2 = "";
        if (int.Parse(Session["type_code"].ToString()) == 100)
        {
            query2 = "SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(7), p.OffLine_Trans_Date, 126) AS bestperformedmonth, SUM(p.Amount) AS transactions, CONVERT(VARCHAR(7), GETDATE(), 126) AS Current_Month, SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) AS transactions_current_month, CASE WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.OffLine_Trans_Date IS NOT NULL GROUP BY o.id, o.mobile_no, o.first_name, o.last_name, CONVERT(VARCHAR(7), p.OffLine_Trans_Date, 126)";
        }
        else
        {
            query2 = "SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(7), p.OffLine_Trans_Date, 126) AS bestperformedmonth, SUM(p.Amount) AS transactions, CONVERT(VARCHAR(7), GETDATE(), 126) AS Current_Month, SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) AS transactions_current_month, CASE WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.OffLine_Trans_Date IS NOT NULL GROUP BY o.id, o.mobile_no, o.first_name, o.last_name, CONVERT(VARCHAR(7), p.OffLine_Trans_Date, 126)";
        }

        con.Open();

        SqlDataAdapter Adp2 = new SqlDataAdapter(query2, con);
        Adp2.Fill(dt6);
        con.Close();
        Session["dt_lll"] = dt6;



        //int pagesize1 = grd_collector1.Rows.Count;
        //int to1 = grd_collector1.Rows.Count;
        //int totalcount1 = grd_collector1.Rows.Count;

        //if (totalcount < grd_collector1.PageSize)
        //    div_paging.Style.Add("margin-top", "0px");
        //else
        //    div_paging.Style.Add("margin-top", "-60px");

        Session["dt_lll"] = dt6;
        grd_collector1.DataSource = dt6;
        grd_collector1.DataBind();

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

        //// Get the current date
        //DateTime currentDate = DateTime.Now;

        //// Calculate the number of days until next Monday
        //int daysUntilMonday = ((int)DayOfWeek.Monday - (int)currentDate.DayOfWeek + 7) % 7;

        //// Calculate the number of days until next Friday
        //int daysUntilFriday = ((int)DayOfWeek.Friday - (int)currentDate.DayOfWeek + 7) % 7;

        //// Calculate the dates for next Monday and Friday
        //DateTime nextMonday = currentDate.AddDays(daysUntilMonday);
        //DateTime nextFriday = currentDate.AddDays(daysUntilFriday + 4);




        selectedDate.Text = txt_startDate.Text;
        DateTime date;
        //DateTime startdate;
        if (DateTime.TryParseExact(txt_startDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            date = date.AddDays(4);
            //enddate.text = date.ToString("MM/dd/yyyy");

        }
        else
        {
            selectedDate.Text = "Invalid Date";
        }
        string endDate = date.ToString("MM/dd/yyyy");

        DateTime currentDate = DateTime.Now;
        DateTime currentMonday = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
        DateTime currentFriday = currentMonday.AddDays(4);

        string CurrentWeekStart = currentMonday.ToString("MM/dd/yyyy");
        string CurrentWeekEnd = currentFriday.ToString("MM/dd/yyyy");


        string querView = String.Empty;
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
            int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
            || int.Parse(Session["type_code"].ToString()) == 115)
        {

            querView = "SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector,  ('" + selectedDate.Text + "' + ' to ' + '" + endDate + "') AS bestperformedweek, SUM(P.Amount) AS transactions,  ('" + CurrentWeekStart + "' + ' to ' + '" + CurrentWeekEnd + "') AS Current_Week, (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND OffLine_Trans_Date >= '" + CurrentWeekStart + "' AND OffLine_Trans_Date <= '" + CurrentWeekEnd + "' ) AS transactions_current_week, CASE WHEN (SUM(P.Amount) < (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND OffLine_Trans_Date >= '" + selectedDate.Text + "' AND OffLine_Trans_Date <= '" + endDate + "')) THEN 'bestperformed' WHEN (SUM(P.Amount)*0.8 <= (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND OffLine_Trans_Date >= '" + selectedDate.Text + "' AND OffLine_Trans_Date <= '" + endDate + "')) THEN 'threshold' WHEN (SUM(P.Amount)*0.8 > (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND OffLine_Trans_Date >= '" + selectedDate.Text + "' AND OffLine_Trans_Date <= '" + endDate + "')) THEN 'underperforming' ELSE NULL END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON CAST(o.id AS VARCHAR(50)) = p.loginUserId WHERE p.OffLine_Trans_Date >= ('12/26/2022') AND p.OffLine_Trans_Date < ('12/30/2022') GROUP BY o.mobile_no, o.first_name, o.last_name, o.id, status";

        }
        else
        {
            querView = "SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, ('" + selectedDate.Text + "' + ' to ' + '" + endDate + "') AS bestperformedweek, SUM(P.Amount) AS transactions, ('" + currentMonday + "' + ' to ' + '" + currentFriday + "') AS Current_Week, (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND created_at >= '03/13/2023' AND created_at <= '03/18/2023') AS transactions_current_week, CASE WHEN (SUM(P.Amount) < (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND created_at >= '03/13/2023' AND created_at <= '03/18/2023')) THEN 'bestperformed' WHEN (SUM(P.Amount)*0.8 <= (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND created_at >= '03/13/2023' AND created_at <= '03/18/2023')) THEN 'threshold' WHEN (SUM(P.Amount)*0.8 > (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND created_at >= '03/13/2023' AND created_at <= '03/18/2023')) THEN 'underperforming' ELSE NULL END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON CAST(o.id AS VARCHAR(50)) = p.loginUserId WHERE p.created_at >= ('12/26/2022') AND p.created_at < ('12/30/2022') GROUP BY o.mobile_no, o.first_name, o.last_name, o.id, status";
        }

        con.Open();

        SqlDataAdapter Adp = new SqlDataAdapter(querView, con);
        Adp.Fill(dt1);
        con.Close();
        Session["dt_l"] = dt1;

        //int pagesize = grd_collector.Rows.Count;
        //int to = grd_collector.Rows.Count;
        //int totalcount = grd_collector.Rows.Count;

        //if (totalcount < grd_collector.PageSize)
        //    div_paging.Style.Add("margin-top", "0px");
        //else
        //    div_paging.Style.Add("margin-top", "-60px");
        Session["dt_ll"] = dt1;
        grd_collector.DataSource = dt1;
        grd_collector.DataBind();


        //MONTH SEARCH
        /////////////////////////////////////////////////////////////////////////////////////

        //selectedDate.Text = TextBox1.Text;
        //DateTime month;
        //if (DateTime.TryParseExact(TextBox1.Text, "MMM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out month))
        //{
        //    month = month.AddDays(4);
        //    selectedDate.Text = month.ToString("MMM/yyyy");
        //}
        //else
        //{
        //    selectedDate.Text = "Invalid Date";
        //}

        ////string yrmm = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();

        //string querView2 = String.Empty;
        //if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
        //    int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
        //    || int.Parse(Session["type_code"].ToString()) == 115)
        //{

        //    querView2 = "SELECT Name, Collector, 'Jan-2022' AS bestperformedmonth, transactions, Current_month, transactions_current_month, status FROM ( SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, 'Jan-2022' AS bestperformedmonth, SUM(p.Amount) AS transactions, FORMAT(GETDATE(), 'MMM-yyyy') AS Current_month, SUM(CASE WHEN YEAR(p.created_at) = YEAR(GETDATE()) AND MONTH(p.created_at) = MONTH(GETDATE()) THEN p.Amount ELSE 0 END) AS transactions_current_month, CASE WHEN SUM(CASE WHEN YEAR(p.created_at) = YEAR(GETDATE()) AND MONTH(p.created_at) = MONTH(GETDATE()) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN YEAR(p.created_at) = YEAR(GETDATE()) AND MONTH(p.created_at) = MONTH(GETDATE()) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p on cast(o.id as varchar(50)) = p.loginUserId WHERE p.created_at IS NOT NULL GROUP BY o.mobile_no, o.first_name, o.last_name) AS subquery ORDER BY bestperformedmonth asc";
        //}
        //else
        //{
        //    querView2 = "SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, ('" + txt_startDate.Text + "' + ' to ' + '" + date + "') AS bestperformedweek, SUM(P.Amount) AS transactions, ('03/13/2023' + ' to ' + '03/18/2023') AS Current_Week, (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND created_at >= '03/13/2023' AND created_at <= '03/18/2023') AS transactions_current_week, CASE WHEN (SUM(P.Amount) < (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND created_at >= '03/13/2023' AND created_at <= '03/18/2023')) THEN 'bestperformed' WHEN (SUM(P.Amount)*0.8 <= (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND created_at >= '03/13/2023' AND created_at <= '03/18/2023')) THEN 'threshold' WHEN (SUM(P.Amount)*0.8 > (SELECT SUM(amount) FROM tbl_Payer_Trans WHERE loginUserId = CAST(o.id AS VARCHAR(50)) AND created_at >= '03/13/2023' AND created_at <= '03/18/2023')) THEN 'underperforming' ELSE NULL END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON CAST(o.id AS VARCHAR(50)) = p.loginUserId WHERE p.created_at >= ('12/26/2022') AND p.created_at < ('12/30/2022') GROUP BY o.mobile_no, o.first_name, o.last_name, o.id, status";
        //}

        //con.Open();

        //SqlDataAdapter Adp2 = new SqlDataAdapter(querView2, con);
        //Adp2.Fill(dt6);
        //con.Close();
        //Session["dt_lll"] = dt6;

        //int pagesize1 = grd_collector1.Rows.Count;
        //int to1 = grd_collector1.Rows.Count;
        //int totalcount1 = grd_collector1.Rows.Count;

        //if (totalcount < grd_collector1.PageSize)
        //    div_paging.Style.Add("margin-top", "0px");
        //else
        //    div_paging.Style.Add("margin-top", "-60px");

        //Session["dt_lll"] = dt6;
        //grd_collector1.DataSource = dt6;
        //grd_collector1.DataBind();


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
    //protected void btn_search_By_Date_Click(object sender, System.EventArgs e)
    //{
    //    CultureInfo culture = new CultureInfo("en-US");
    //    DateTime startDate = DateTime.ParseExact(txt_startDate.Text, "MM/dd/yyyy", culture);
    //    DateTime endDate = DateTime.ParseExact(txt_endDate.Text, "MM/dd/yyyy", culture);
    //    string url = String.Empty;
    //    url = "~/Dashboard/CollectionReportSearchByDate.aspx?StartDate=" + startDate + "&EndDate=" + endDate;
    //    Response.Redirect(url);
    //}

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

    protected void btnWeekly_Click(object sender, EventArgs e)
    {
        string selectedStatus = DropDownStatus.SelectedValue;
        string stat = selectedStatus.Trim();


        if (stat != null)
        {
            string queryTbl_trans = " SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at) + 4, 101) AS bestperformedweek, SUM(p.Amount) AS transactions, CONVERT(VARCHAR(10), DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0), 101) + ' to ' + CONVERT(VARCHAR(10), DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 4), 101) AS Current_Week, SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) AS transactions_current_week, CASE WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'Best Performer' WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.created_at IS NOT NULL GROUP BY o.mobile_no, o.first_name, o.last_name, DATEADD(dd, -(DATEPART(dw, p.created_at)-2), p.created_at) HAVING DATEPART(dw, MAX(p.created_at)) <= 5 AND CASE WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'Best Performer' WHEN SUM(CASE WHEN p.created_at >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) AND p.created_at < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END = '" + stat + "'";
            SqlDataAdapter Adp4 = new SqlDataAdapter(queryTbl_trans, con);
            dt4.Clear();
            Adp4.Fill(dt4);
            Session["dt_list4"] = dt4;

            grd_collector.DataSource = dt4;
            grd_collector.DataBind();

        }
        else
        {
            TheMethod();
        }

    }

    protected void btnMonthly_Click(object sender, EventArgs e)
    {
        string selectedStatus2 = DropDownList1.SelectedValue;
        string stat2 = selectedStatus2.Trim();

        string queryTbl_trans = "SELECT Name, Collector, bestperformedmonth, transactions, Current_month, transactions_current_month, status FROM (SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, FORMAT(p.created_at, 'MMM-yyyy') AS bestperformedmonth, SUM(p.Amount) AS transactions, FORMAT(GETDATE(), 'MMM-yyyy') AS Current_month, SUM(CASE WHEN YEAR(p.created_at) = YEAR(GETDATE()) AND MONTH(p.created_at) = MONTH(GETDATE()) THEN p.Amount ELSE 0 END) AS transactions_current_month, CASE WHEN SUM(CASE WHEN YEAR(p.created_at) = YEAR(GETDATE()) AND MONTH(p.created_at) = MONTH(GETDATE()) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN YEAR(p.created_at) = YEAR(GETDATE()) AND MONTH(p.created_at) = MONTH(GETDATE()) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p on cast(o.id as varchar(50)) = p.loginUserId WHERE p.created_at IS NOT NULL GROUP BY o.mobile_no, o.first_name, o.last_name, FORMAT(p.created_at, 'MMM-yyyy')) AS subquery WHERE status = '" + stat2 + "' ORDER BY bestperformedmonth asc";
        SqlDataAdapter Adp6 = new SqlDataAdapter(queryTbl_trans, con);
        dt6.Clear();
        Adp6.Fill(dt6);
        Session["dt_list4"] = dt6;

        grd_collector1.DataSource = dt6;
        grd_collector1.DataBind();
    }

    protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    {
        grd_collector.PageIndex = e.NewPageIndex;
        grd_collector.DataSource = Session["dt_ll"];
        grd_collector.DataBind();

      
    }
    protected void grd_paginHandler1(object sender, GridViewPageEventArgs e)
    {


        grd_collector.PageIndex = e.NewPageIndex;
        grd_collector.DataSource = Session["dt_lll"];
        grd_collector.DataBind();
    }

    protected void btnmonth_search_Click(object sender, EventArgs e)
    {
        DataTable dt1a = new DataTable();
        DataTable dt1b = new DataTable();
        DataTable dtc = new DataTable();
        DataTable dtc1 = new DataTable();


        Selectedtoo.Text = TextBox1.Text;
        DateTime dt = DateTime.ParseExact(Selectedtoo.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        string monthdate = dt.ToString("yyyy-MM");


        string querView = String.Empty;
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 111 ||
            int.Parse(Session["type_code"].ToString()) == 112 || int.Parse(Session["type_code"].ToString()) == 113
            || int.Parse(Session["type_code"].ToString()) == 115)
        {

            querView = "SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(7), p.OffLine_Trans_Date, 126) AS bestperformedmonth, SUM(p.Amount) AS transactions, CONVERT(VARCHAR(7), GETDATE(), 126) AS Current_Month, SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) AS transactions_current_month, CASE WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.OffLine_Trans_Date IS NOT NULL AND CONVERT(VARCHAR(7), p.OffLine_Trans_Date, 126) LIKE '" + monthdate + "' GROUP BY o.id, o.mobile_no, o.first_name, o.last_name, CONVERT(VARCHAR(7), p.OffLine_Trans_Date, 126)";

        }
        else
        {
            querView = "SELECT CONCAT(o.first_name, ' ', o.last_name) AS Name, o.mobile_no AS Collector, CONVERT(VARCHAR(7), p.OffLine_Trans_Date, 126) AS bestperformedmonth, SUM(p.Amount) AS transactions, CONVERT(VARCHAR(7), GETDATE(), 126) AS Current_Month, SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) AS transactions_current_month, CASE WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) > SUM(p.Amount) THEN 'BestPerformer' WHEN SUM(CASE WHEN p.OffLine_Trans_Date >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0) AND p.OffLine_Trans_Date < DATEADD(dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)) THEN p.Amount ELSE 0 END) < SUM(p.Amount) * 0.8 THEN 'Underperforming' ELSE 'Threshold' END AS status FROM oyo_Registration o LEFT JOIN tbl_Payer_Trans p ON cast(o.id as varchar(50)) = p.loginUserId WHERE p.OffLine_Trans_Date IS NOT NULL AND CONVERT(VARCHAR(7), p.OffLine_Trans_Date, 126) LIKE '" + monthdate + "' GROUP BY o.id, o.mobile_no, o.first_name, o.last_name, CONVERT(VARCHAR(7), p.OffLine_Trans_Date, 126)";
        }

        con.Open();

        SqlDataAdapter Adp22 = new SqlDataAdapter(querView, con);
        dt6.Clear();
        Adp22.Fill(dt6);
        Session["dt_list4"] = dt6;

        grd_collector1.DataSource = dt6;
        grd_collector1.DataBind();

    }

}



