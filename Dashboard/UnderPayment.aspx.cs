using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Drawing;

public partial class Dashboard_UnderPayment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(OYOClass.connection);
    SqlConnection con1 = new SqlConnection(OYOClass.connection);
    DataTable dt1 = new DataTable();
    DataTable dt01 = new DataTable();
    DataTable dt4 = new DataTable();
    DataTable dt5 = new DataTable();
    DataTable dt6 = new DataTable();
    DataTable dTable = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["type_code"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (Session["type_code"] == null || (int.Parse(Session["type_code"].ToString()) != 100 && int.Parse(Session["type_code"].ToString()) != 104
            && int.Parse(Session["type_code"].ToString()) != 103 && int.Parse(Session["type_code"].ToString()) != 105))
        {
            Session.Abandon();
            Response.Redirect("../Login.aspx");
        }
        string query = "";
        if (int.Parse(Session["type_code"].ToString()) == 100)
        {
            query = " Select first_name+' '+ last_name Name, mobile_no CollectorId, id ID From oyo_Registration Where type_code = 106";
        }
        else
        {
            query = " Select first_name+' '+ last_name Name, mobile_no CollectorId, id ID From oyo_Registration Where type_code = 106  and lga_id= " + Session["lga"].ToString();
        }
        string qry = "Select * from PaymentRecord where RegId = 0";
        SqlDataAdapter Adp = new SqlDataAdapter(query, con);
        SqlDataAdapter Adpp = new SqlDataAdapter(qry, con);
        Adp.Fill(dt1);
      
        Session["dt_l"] = dt1;
        int c = 0;
        int i = 0;
        var collect_ID = Collector.Text;


        dt1.Columns.Add(new DataColumn() { ColumnName = "totalFunded", DataType = typeof(string) });
        dt1.Columns.Add(new DataColumn() { ColumnName = "totalTransAmount", DataType = typeof(string) });
        dt1.Columns.Add(new DataColumn() { ColumnName = "totalPayment", DataType = typeof(string) });
        dt1.Columns.Add(new DataColumn() { ColumnName = "walletBalance", DataType = typeof(string) });
        dt1.Columns.Add(new DataColumn() { ColumnName = "diffAmount", DataType = typeof(string) });
        double totalFunded = 0.0;
        double totalTransAmount = 0.0;
        double totalPayment = 0.0;
        foreach (DataRow dr in dt1.Rows)
        {
            con1.Open();
            totalTransAmount = 0.0;
            string queryTbl_trans = "Select Amount from tbl_Payer_Trans where loginUserId = CONVERT(varchar(50), " + dr["ID"] + ")";
            SqlDataAdapter Adp4 = new SqlDataAdapter(queryTbl_trans, con1);
            Adp4.Fill(dt4);
            Session["dt_l4"] = dt4;
            //DataRow[] dr4 = dt4.Select("Amount");
            //totalTransAmount = Convert.ToDouble(dr4);
            foreach (DataRow dr4 in dt4.Rows)
            {
                var kk =dr4["Amount"].ToString();
                if (kk == "")
                {
                    totalTransAmount = 0;
                }
                else
                {
                    double amount;
                    if (Double.TryParse(dr4["Amount"].ToString(), out amount))
                    {
                        totalTransAmount = amount;
                    }
                    else
                    {
                        totalTransAmount = 0;
                    }
                }
            }
            totalPayment = 0.0;
            string queryPayment = "Select Sum(AmountPaid)AmountPaid from PaymentRecord where CollectorId = '" + dr["CollectorId"] + "'";
            SqlDataAdapter Adp5 = new SqlDataAdapter(queryPayment, con);
            Adp5.Fill(dt5);
            Session["dt_l4"] = dt5;
            //DataRow[] dr5 = dt5.Select("AmountPaid");
            //totalPayment += Convert.ToDouble(dr5);
            foreach (DataRow dr5 in dt5.Rows)
            {
                var kk = dr5["AmountPaid"].ToString();
                if (kk == "")
                {
                    totalPayment = 0.00;
                }
                else
                {
                    totalPayment = Convert.ToDouble(dr5["AmountPaid"]);
                }
            }
            String CollectorId = dr["CollectorId"].ToString();
            String Name = dr["Name"].ToString();

            var WalletBalance = OYOClass.getCurrentWalletBalance(dr["CollectorId"].ToString());

            totalFunded = 0.0;
            string queryFunded = " Select Sum(amount)amount from addWalletHistory where receiver_mobile = '" + dr["CollectorId"] + "'";
            SqlDataAdapter Adp6 = new SqlDataAdapter(queryFunded, con);
            Adp6.Fill(dt6);
            Session["dt_l6"] = dt6;
            foreach (DataRow dr6 in dt6.Rows)
            {
                var kk = dr6["amount"].ToString();
                if (kk == "")
                {
                    totalFunded = 0.00;
                }
                else
                {
                    totalFunded = Convert.ToDouble(dr6["amount"]);
                }
            }

            double diffAmount = 0.0;
            diffAmount = totalTransAmount - totalPayment;

            if (totalTransAmount > totalPayment)
            {
                if (collect_ID == "")
                {
                    String type = dr["CollectorId"].ToString();
                    Collector.Items.Insert(i, new ListItem(dr["Name"].ToString(), type));
                    if (i < 1)
                    {
                        dt01.Columns.Add("CollectorId");
                        Collector.Items.Insert(i, new ListItem("Select", "0"));
                        dt01.Dispose();
                        i++;
                    }
                }
            }

     
            dr.SetField("totalTransAmount", totalTransAmount);
            dr.SetField("totalPayment", totalPayment);
            dr.SetField("walletBalance", WalletBalance);
            dr.SetField("totalFunded", totalFunded);
            dr.SetField("diffAmount", diffAmount);

            if (c < 1)
            {
                dTable.Columns.Add("Name");
                dTable.Columns.Add("CollectorId");
                dTable.Columns.Add("totalTransAmount");
                dTable.Columns.Add("totalPayment");
                dTable.Columns.Add("WalletBalance");
                dTable.Columns.Add("totalFunded");
                dTable.Columns.Add("diffAmount");
            }


            String Names = dr["Name"].ToString();
            String CollectorIds = dr["CollectorId"].ToString();
            String totalTransAmounst = dr["totalTransAmount"].ToString();
            String totalPayments = dr["totalPayment"].ToString();
            String WalletBalances = dr["WalletBalance"].ToString();
            String totalFundeds = dr["totalFunded"].ToString();
            String diffAmounts = dr["diffAmount"].ToString();

            //totalTransAmounst = "N" + totalTransAmounst;
            //WalletBalances = "N" + WalletBalances;
            //totalFundeds = "N" + totalFundeds;
            //diffAmounts = "N" + diffAmounts;

            dTable.Rows.Add(Names, CollectorIds, totalTransAmounst, totalPayments, WalletBalances, totalFundeds, diffAmounts);

            c++;
            con1.Close();
        }

        Adp.Dispose();

        Adpp.Fill(dTable);
        Session["dt_ll"] = dTable;
        grd_list_Collectors.DataSource = dTable;

        grd_list_Collectors.DataBind();

    }

    protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    {
        grd_list_Collectors.PageIndex = e.NewPageIndex;
        grd_list_Collectors.DataSource = Session["dt_l"];

        grd_list_Collectors.DataBind();
    }
    protected void grd_list_Collectors_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // modify it accoording to your datasource, use the debugger if you're not sure
            DataRow row = ((DataRowView)e.Row.DataItem).Row;
            // just an example:
            bool redCondition = Convert.ToDouble(row.Field<string>("totalTransAmount")) < Convert.ToDouble(Convert.ToDouble(row.Field<string>("totalPayment"))/0.8);
            e.Row.BackColor = redCondition ? Color.Red : grd_list_Collectors.RowStyle.BackColor;
        }
    }
    protected void getCollectorList_Click(object sender, EventArgs e)
    {
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

    protected void DisableColletor(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        if (int.Parse(Session["type_code"].ToString()) == 103 || int.Parse(Session["type_code"].ToString()) == 105)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = " You have no right to disable collectors !!!";
            return;
        }

        string CollectorId = ((LinkButton)sender).CommandArgument.ToString();
        string query = " UPDATE oyo_Registration SET status = 0 where mobile_no = \'" + CollectorId + "\'";

        SqlConnection conn = new SqlConnection(OYOClass.connection);
        SqlCommand cmmd = new SqlCommand(query, conn);

        cmmd.Parameters.AddWithValue("@CollectorId", CollectorId);
        conn.Open();
        var Id = cmmd.ExecuteNonQuery();
        conn.Close();
        if (Id > 0)
        {
            string qry = "Select first_name + ' ' + last_name Name from  oyo_Registration where mobile_no = \'" + CollectorId + "\'";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@CollectorId", CollectorId);
            conn.Open();
            var user = cmd.ExecuteScalar();
            conn.Close();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+ user + " has been disable successfully');", true);
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = user + " has been disable successfully";
            return;
        }
    }

    protected void SweepColletorBalance(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        if (int.Parse(Session["type_code"].ToString()) == 103 || int.Parse(Session["type_code"].ToString()) == 105)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = " You have no right to sweep collector's account !!!";
            return;
        }

        string CollectorId = ((LinkButton)sender).CommandArgument.ToString();

        try
        {
            string URI = "https://nownowpay.com.ng/mfs-transaction-management/authManagement/get";
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            string myParameters = "{\"mfsCommonServiceRequest\": {\"mfsSourceInfo\": {\"channelId\": \"22\",\"surroundSystem\": \"1\"}, \"mfsTransactionInfo\": { \"requestId\": \"8447220123153311724518095445\",\"serviceType\": \"0\",\"timestamp\": \"1541084236965\"} }}";
            string BearerToken = "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                wc.Headers[HttpRequestHeader.Authorization] = "Basic c3luZXJneTpTeW5lcmd5IzB3IzB3==";

                BearerToken = wc.UploadString(URI, myParameters);
            }

            var root = JObject.Parse(BearerToken.ToString());

            string toQuery = "Select first_name + ' ' + last_name Name, EntityId from oyo_Registration where mobile_no = '" + CollectorId + "'";
            SqlConnection conTo = new SqlConnection(OYOClass.connection);
            SqlCommand cmd_to = new SqlCommand(toQuery, conTo);

            DataTable dt_to_detail = new DataTable();
            conTo.Open();
            SqlDataAdapter da_lga = new SqlDataAdapter(cmd_to);
            da_lga.Fill(dt_to_detail);
            conTo.Close();

            var fromName = dt_to_detail.Rows[0][0].ToString();
            var fromEntityId = dt_to_detail.Rows[0][1].ToString();

            string URI_transferMoney = "https://apidev.nownowpay.com.ng/mfs-transaction-management/balanceManagement/transfer";
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            TimeSpan Now = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1));
            long TimeStamp = Convert.ToInt64(Math.Floor(Now.TotalMilliseconds));
            string timestamp = TimeStamp.ToString();
            Random random = new Random();
            String randomNumber = OYOClass.getRequestId();

            String collectrorWalletBalance = OYOClass.getCurrentWalletBalance(CollectorId);
            var sessionUserEntityId = Session["EntityId"].ToString();
            string mpin = "1234";

            string para = "{ \"mfsRequestInfo\": { \"senderMsisdn\": \"" + CollectorId + "\", \"fromEntityId\": \""
                    + fromEntityId + "\", \"amount\": \"" + collectrorWalletBalance + "\", \"remark\": \"Refund Money to EIRS Admin Wallet\"," +
                    " \"toEntityId\": \"" + sessionUserEntityId + "\", \"walletTypeToCredit\": \"1\" }, \"mfsCommonServiceRequest\": { " +
                    "\"mfsSourceInfo\": { \"senderMsisdn\": \"" + CollectorId + "\", \"surroundSystem\": \"1\", \"entityId\": \"" +
                    Session["EntityId"].ToString() + "\", \"mpin\": \"" +
                    OYOClass.CreateMD5(mpin.ToString()) + "\", \"channelId\": \"23\" }, \"mfsTransactionInfo\": { \"serviceType\": \"501\", \"requestId\": " +
                    randomNumber + ", \"timestamp\": " + timestamp.ToString() + " }}} ";


            string SendMoneyResult = "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + root["mfsResponseInfo"]["token"].ToString();
                SendMoneyResult = wc.UploadString(URI_transferMoney, para);

                var result = JObject.Parse(SendMoneyResult);

                JObject parsed = JObject.Parse(SendMoneyResult);

                if (parsed["mfsCommonServiceResponse"]["mfsStatusInfo"]["status"].ToString().Contains("Failure"))
                {
                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = "Something went wrong <br />  error: " + parsed["mfsCommonServiceResponse"]["mfsStatusInfo"]["errorDescription"].ToString();

                    return;
                }

                else
                {

                    string query = "Update addWalletHistory set Refound = walletBalnce where receiver_mobile = \'" + CollectorId + "\'";

                    SqlConnection conn = new SqlConnection(OYOClass.connection);
                    SqlCommand cmmd = new SqlCommand(query, conn);

                    cmmd.Parameters.AddWithValue("@CollectorId", CollectorId);
                    conn.Open();
                    var swepStatus = cmmd.ExecuteNonQuery();
                    conn.Close();
                    if (swepStatus > 0)
                    {
                        string qry = "Update addWalletHistory set walletBalnce = 0 where receiver_mobile = \'" + CollectorId + "\'";

                        SqlConnection conection = new SqlConnection(OYOClass.connection);
                        SqlCommand cmmand = new SqlCommand(qry, conection);

                        cmmand.Parameters.AddWithValue("@CollectorId", CollectorId);
                        conection.Open();
                        var walletStatus = cmmand.ExecuteNonQuery();
                        conection.Close();

                        if (walletStatus > 0)
                        {
                            string qy = "Select first_name + ' ' + last_name Name from  oyo_Registration where mobile_no = \'" + CollectorId + "\'";
                            SqlCommand cm = new SqlCommand(qy, conn);
                            cm.Parameters.AddWithValue("@CollectorId", CollectorId);
                            conn.Open();
                            var user = cm.ExecuteScalar();
                            conn.Close();
                            modalinfo.Attributes.Add("class", "modal show");
                            lblmodalbody.Text = user + "'s account has been sweep successfully";
                            return;
                        }
                    }
                    else
                    {
                        modalinfo.Attributes.Add("class", "modal show");
                        lblmodalbody.Text = "Unable to sweep account ";
                    }

                    string balance = OYOClass.getCurrentWalletBalance(CollectorId);
                    ((Label)Master.FindControl("lblAmount")).Text = balance;

                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = "Transaction successfull";
                }
            }
        }

        catch (Exception ex)
        {
            var msg = ex.Message;
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Something Went Wrong!!";
        }


    }

}