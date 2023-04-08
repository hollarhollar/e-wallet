using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text;
using System.Activities.Expressions;

public partial class Dashboard_PaymentList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(OYOClass.connection);
    DataTable dt1 = new DataTable();
    DataTable dt4 = new DataTable();
    DataTable dt5 = new DataTable();
    DataTable dt6 = new DataTable();
    DataTable dTable = new DataTable();

    public object Protect { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["type_code"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (Session["type_code"] == null || (int.Parse(Session["type_code"].ToString()) != 100 && int.Parse(Session["type_code"].ToString()) != 104
                && int.Parse(Session["type_code"].ToString()) != 103 && int.Parse(Session["type_code"].ToString()) != 105) && int.Parse(Session["type_code"].ToString()) != 115)
            {
                Session.Abandon();
                Response.Redirect("../Login.aspx");
            }
        }

            //string query = "";
            //if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 115)
            //{
            //    query = "SELECT CONCAT(o.first_name, ' ', o.last_name) Name, o.mobile_no CollectorId, o.id ID, format(SUM(p.Amount),'C','ng-ng') AS TotalTransAmount, format(SUM(r.AmountPaid),'C','ng-ng') AS TotalPayment, format(SUM(d.Amount),'C','ng-ng') AS TotalFunded, format(SUM(d.amount),'C','ng-ng') WalletBalance, format(SUM(p.Amount) - sum(r.AmountPaid), 'C','ng-ng') AS diffAmount FROM oyo_Registration o LEFT JOIN tbl_payer_trans p ON CONVERT(varchar(50), o.id) = p.loginUserId LEFT JOIN PaymentRecord r ON p.id = r.PaymentRecordId LEFT JOIN addWalletHistory d ON o.mobile_no = d.receiver_mobile WHERE type_code = '106' GROUP BY o.first_name, o.last_name, o.mobile_no, o.id";
            //}
            //else
            //{
            //    query = " Select  first_name+' '+ last_name Name, mobile_no CollectorId, id ID From oyo_Registration Where type_code = 106 and lga_id = " + Session["lga"].ToString();
            //}

            //SqlDataAdapter Adp = new SqlDataAdapter(query, con);
            //con.Open();
            //DataTable dt1 = new DataTable();
            //Adp.Fill(dt1);
            //con.Close();

            //Session["dt_ll"] = dt1;
            //grd_list_Collectors.DataSource = dt1;
            //grd_list_Collectors.DataBind();

            //int pagesize = grd_list_Collectors.Rows.Count;
            //int to = grd_list_Collectors.Rows.Count;
            //int totalcount = grd_list_Collectors.Rows.Count;

            ////if (totalcount < grd_list_Collectors.PageSize)
            ////    div_paging.Style.Add("margin-top", "0px");
            ////else
            ////    div_paging.Style.Add("margin-top", "-60px");
            //HtmlGenericControl div_paging = (HtmlGenericControl)grd_list_Collectors.BottomPagerRow.FindControl("div_paging");

            //if (totalcount < grd_list_Collectors.PageSize)
            //{
            //    div_paging.Style["margin-top"] = "0px";
            //}
            //else
            //{
            //    div_paging.Style["margin-top"] = "-60px";
            //}


            //}
            string query = "";
            if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 115)
            {
                query = " Select  first_name+' '+ last_name Name, mobile_no CollectorId, id ID From oyo_Registration Where type_code = 106";
            }
            else
            {
                query = " Select  first_name+' '+ last_name Name, mobile_no CollectorId, id ID From oyo_Registration Where type_code = 106 and lga_id = " + Session["lga"].ToString();
            }

            SqlDataAdapter Adp = new SqlDataAdapter(query, con);
            Adp.Fill(dt1);

            dt1.Columns.Add(new DataColumn() { ColumnName = "totalFunded", DataType = typeof(string) });
            dt1.Columns.Add(new DataColumn() { ColumnName = "totalTransAmount", DataType = typeof(string) });
            dt1.Columns.Add(new DataColumn() { ColumnName = "totalPayment", DataType = typeof(string) });
            dt1.Columns.Add(new DataColumn() { ColumnName = "walletBalance", DataType = typeof(string) });
            dt1.Columns.Add(new DataColumn() { ColumnName = "diffAmount", DataType = typeof(string) });

            int c = 0;
            int i = 0;
            var collect_ID = Collector.Text;

            double totalFunded = 0.0;
            double totalTransAmount = 0.0;
            double totalPayment = 0.0;

            foreach (DataRow dr in dt1.Rows)
            {

                var WalletBalance = OYOClass.getCurrentWalletBalance(dr["CollectorId"].ToString());
                totalTransAmount = 0.0;
                string queryTbl_trans = "Select Amount, MobNo from tbl_Payer_Trans where loginUserId = CONVERT(varchar(50), " + dr["ID"] + ")";
                SqlDataAdapter Adp4 = new SqlDataAdapter(queryTbl_trans, con);
                dt4.Clear();
                Adp4.Fill(dt4);
                Session["dt_l4"] = dt4;
                foreach (DataRow dr4 in dt4.Rows)
                {
                    totalTransAmount += Convert.ToDouble(dr4["Amount"]);
                }

                totalPayment = 0.0;
                string queryPayment = "Select AmountPaid from PaymentRecord where CollectorId = '" + dr["CollectorId"] + "'";
                con.Open();
                SqlDataAdapter Adp5 = new SqlDataAdapter(queryPayment, con);
                dt5.Clear();
                con.Close();
                Adp5.Fill(dt5);
                Session["dt_l4"] = dt5;
                foreach (DataRow dr5 in dt5.Rows)
                {
                    totalPayment += Convert.ToDouble(dr5["AmountPaid"]);
                }


                totalFunded = 0.0;
                string queryFunded = "Select amount from addWalletHistory where receiver_mobile = '" + dr["CollectorId"] + "'";
                con.Open();
                SqlDataAdapter Adp6 = new SqlDataAdapter(queryFunded, con);
                dt6.Clear();
                con.Close();
                Adp6.Fill(dt6);
                Session["dt_l6"] = dt6;
                foreach (DataRow dr6 in dt6.Rows)
                {
                    totalFunded += Convert.ToDouble(dr6["amount"]);
                }

                double diffAmount = 0.0;
                diffAmount = totalTransAmount - totalPayment;

                dr.SetField("totalTransAmount", totalTransAmount);
                dr.SetField("totalPayment", totalPayment);
                dr.SetField("walletBalance", WalletBalance);
                dr.SetField("totalFunded", totalFunded);
                dr.SetField("diffAmount", diffAmount);

                c++;
            }

            Session["dt_ll"] = dt1;
            grd_list_Collectors.DataSource = dt1;
            grd_list_Collectors.DataBind();


            int ii = 0;
            if (Collector.Items.Count == 0)
            {
                string loginqry = "";
                if (int.Parse(Session["type_code"].ToString()) == 100)
                {
                    loginqry = "Select first_name + ' ' + last_name Name, mobile_no  from oyo_Registration where type_code = 106";
                }
                else
                {
                    loginqry = "Select first_name + ' ' + last_name Name, mobile_no  from oyo_Registration where type_code = 106  and lga_id = " + Session["lga"].ToString();
                }
                SqlCommand cmd = new SqlCommand(loginqry, con);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt.Columns.Add("mobile_no");
                dt.Columns.Add("Name");
                dt.Rows.Add("0", "Select Collector");
                dt.Clear();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    String type = dr["mobile_no"].ToString();
                    Collector.Items.Insert(ii, new ListItem(dr["Name"].ToString(), type));
                    ii++;
                }
            }
        }

        protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
        {
            grd_list_Collectors.PageIndex = e.NewPageIndex;
            grd_list_Collectors.DataSource = Session["dt_l"];
            grd_list_Collectors.DataBind();
        }

        protected void getCollectorList_Click(object sender, EventArgs e)
        {
            //var result = Session["dt_ll"];

            if (Collector.Text != "0")
            {
                DataTable sessionDate = (DataTable)Session["dt_ll"];

                DataTable tblFiltered = sessionDate.AsEnumerable()
              .Where(row => row.Field<String>("CollectorId") == Collector.Text)
              .OrderByDescending(row => row.Field<String>("CollectorId"))
              .CopyToDataTable();

                grd_list_Collectors.DataSource = tblFiltered;
                grd_list_Collectors.DataBind();
            }
        }
    }