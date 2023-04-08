using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Reflection;

public partial class Dashboard_Payment : System.Web.UI.Page
{
    int Interval = 1;
    SqlConnection con = new SqlConnection(OYOClass.connection);
    DateTime dtime = new DateTime(0000000000000000000);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["type_code"] == null || int.Parse(Session["type_code"].ToString()) != 100 && int.Parse(Session["type_code"].ToString()) != 104)
        {
            Response.Redirect("../Login.aspx");
        }

        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }

        int i = 0;
        if (collectorList.Items.Count == 0)
        {
            String loginqry = "";
            if (int.Parse(Session["type_code"].ToString()) == 100)
            {
                loginqry = "Select id, mobile_no, first_name +' ' + last_name as Name from oyo_Registration  where type_code = 106";
            }
            else
            {
                loginqry = "Select id, mobile_no, first_name +' ' + last_name as Name from oyo_Registration  where type_code = 106  And lga_id = " + Session["lga"].ToString();
            }

            SqlCommand cmd = new SqlCommand(loginqry, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dt.Columns.Add("id");
            dt.Columns.Add("mobile_no");
            dt.Columns.Add("Name");
            dt.Rows.Add("0", "0", "Select");
            da.Fill(dt);
            con.Close();
            Session["lstColl"] = dt;
            foreach (DataRow dr in dt.Rows)
            {
                String type = dr["id"].ToString();
                collectorList.Items.Insert(i, new System.Web.UI.WebControls.ListItem(dr["Name"].ToString(), type));
                i++;
            }
        }

        TheMethod();
    }
    //TheMethod();


    public DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);
        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }

    protected void TheMethod()
    {
    }
    protected void basicPopup(object sender, EventArgs e)
    {

    }

    protected void getFundTransactionList_Click(object sender, EventArgs e)
    {
        string values = collectorList.Text.ToString();
        int i = 0;
        if (collectorList.Items.Count != 0)
        {
            String loginqry = " select transactionId, created_at from addWalletHistory where receiver_mobile = \'" + collectorList.Text + "\'";
            SqlCommand cmd = new SqlCommand(loginqry, con);
            DataTable dt = new DataTable();
            dt.Clear();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dt.Columns.Add("transactionId");
            dt.Columns.Add("created_at");
            dt.Rows.Add("0", dtime);
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                String type = dr["transactionId"].ToString();
                i++;
            }
        }
    }
    public class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    //protected void getAmountList_Click(object sender, EventArgs e)
    //{
    //    HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
    //    Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");
    //    var values = txt_dob.Text.ToString();

    //    var val = collectorList.Text.ToString();
    //    if (values == null)
    //    {
    //        return;
    //    }
    //    if (val == "0" || val == "")
    //    {
    //        return;
    //    }
    //    DateTime dateTime = DateTime.Now;
    //    DateTime endDateTime = DateTime.Now;
    //    DataTable dt = new DataTable();
    //    if (values == "0")
    //    {
    //        dt.Clear();
    //        return;
    //    }
    //    int i = 0;
    //    DateTime date = ParseDate(values);
    //    //System.DateTime.ParseExact(values, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
    //    // if(date > DateTime.Toda)
    //    Session["date"] = date;
    //    endDateTime = date.AddDays(1).AddTicks(-1);
    //    CultureInfo provider = CultureInfo.InvariantCulture;
    //    String loginqry = "SELECT (SELECT Sum([Amount])Amount FROM [tbl_Payer_Trans]where MobNo = \'" + val + "' and created_at >=\'" + date + "' and created_at<=\'" + endDateTime + "')-(SELECT Sum(DebittedAmount)Amount FROM TransDebit where ReceiverNumber = \'" + val + "' and TransactionDate >=\'" + date + "' and TransactionDate<=\'" + endDateTime + "')As Amount ";
    //    SqlCommand cmd = new SqlCommand(loginqry, con);
    //    dt.Clear();
    //    con.Open();
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    da.Fill(dt);
    //    con.Close();
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        String type = dr["amount"].ToString();
    //        amountList.Items.Insert(i, new System.Web.UI.WebControls.ListItem(dr["amount"].ToString(), type));
    //        i++;
    //    }
    //}
    protected void getAmountList_Click(object sender, EventArgs e)
    {
        Session["det"] = "2";
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");
        var values = txt_dob.Text.ToString();

        var val = collectorList.Text.ToString();
        if (values == null)
        {
            return;
        }
        if (val == "0" || val == "")
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
        DateTime date = DateTime.ParseExact(values, "dd/MM/yyyy", null);
        Session["date"] = date;
        DataTable dx = new DataTable();
        dx = (DataTable)Session["lstColl"];
        DataView dv = new DataView(dx);
        dv.RowFilter = "id =\'" + val + "'";
        string filteredDataTable = dv[0][1].ToString();
        Session["user_id2"] = filteredDataTable;
        endDateTime = date.AddDays(1).AddTicks(-1);
        CultureInfo provider = CultureInfo.InvariantCulture;
        String loginqry = "";
        
        //loginqry = "SELECT (SELECT Sum(ISNULL([Reminder], 0 ))Amount FROM [tbl_Payer_Trans]where loginUserId = \'" + val + "'and PaymentStatus='Partial' OR PaymentStatus IS NULL  and OffLine_Trans_Date >=\'" + date + "' and OffLine_Trans_Date<\'" + endDateTime + "')+(SELECT Sum([Amount])Amount FROM [tbl_Payer_Trans]where loginUserId = \'" + val + "'and PaymentStatus IS NULL and OffLine_Trans_Date >=\'" + date + "' and OffLine_Trans_Date<\'" + endDateTime + "') As Amount";
        loginqry = "SELECT (SELECT Sum(ISNULL([Reminder], 0 )) Amount FROM [tbl_Payer_Trans] WHERE loginUserId = \'" + val + "' AND PaymentStatus = 'Partial' OR PaymentStatus IS NULL AND OffLine_Trans_Date >= CONVERT(datetime, \'" + date + "', 120) AND OffLine_Trans_Date < CONVERT(datetime, \'" + endDateTime + "', 120)) + (SELECT Sum([Amount]) Amount FROM [tbl_Payer_Trans] WHERE loginUserId = \'" + val + "' AND PaymentStatus IS NULL AND OffLine_Trans_Date >= CONVERT(datetime, '" + date + "', 120) AND OffLine_Trans_Date < CONVERT(datetime, '" + endDateTime + "', 120)) AS Amount";
        SqlCommand cmd = new SqlCommand(loginqry, con);
        dt.Clear();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        con.Close();

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["amount"].ToString() == "" || dr["amount"].ToString() == "NULL")
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "No Transaction For The Selected Date(s) ";
                return;

            }
            String type = dr["amount"].ToString();
            amountList.Items.Insert(i, new System.Web.UI.WebControls.ListItem(dr["amount"].ToString(), type));
            i++;
        }
        // }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var det = "";
        var userId = Session["user_id2"].ToString();
        var loginId = Session["user_id"].ToString();
        if (Session["det"] != null)
        {
            det = Session["det"].ToString();
        }
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");
        var values = "";
        var values1 = "";
        DateTime date = new DateTime();
        DateTime date1 = new DateTime();
        if (det != "2")
        {
            values = txt_startDate.Text.ToString();
            values1 = txt_endDate.Text.ToString();
            date = ParseDate(values);
            date1 = ParseDate(values1);
        }
        else
        {
            values = txt_dob.Text.ToString();
            date = DateTime.ParseExact(values, "dd/MM/yyyy", null);
            Session["date"] = date;
            date1 = date.AddDays(1).AddTicks(-1);
        }

        var recNumber = txt_receiptNumber.Text.ToString();
        var ret = ReceiptChecker(recNumber);
        if (ret == true)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Receipt Number Already Exist";
            return;
        }
        var listOfTrans = GetTransList(date, date1);
        var listOfDates = GetDateList(date, date1);
        List<DateTime> dates = listOfDates.ToList();

        var sameTransDate = listOfTrans.Where(a => listOfDates.Any(x => x.Date.Date == a.TransDate.Date));
        decimal total1 = sameTransDate.Sum(o => o.Amount);
        decimal total2 = sameTransDate.Sum(o => o.Reminder);
        decimal total = 0;
        //foreach (var item in sameTransDate)
        //{
        //    decimal Amount = item.Reminder;
        //    if (Amount != 0)
        //        total = +Amount;
        //    else
        //        total = total + item.Amount;
        //}

        string query = "Select id from oyo_Registration where id = \'" + collectorList.Text + "\'";
        SqlConnection conn = new SqlConnection(OYOClass.connection);
        SqlCommand cmmd = new SqlCommand(query, conn);

        cmmd.Parameters.AddWithValue("@CollectorId", collectorList.Text);
        conn.Open();
        var Id = cmmd.ExecuteScalar();
        conn.Close();
        int status = 0;
        decimal amp = Convert.ToDecimal(txt_amountPaid.Text);
        total = Convert.ToDecimal(amountList.Text);
        if (Convert.ToDecimal(total) < amp)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Amount to Pay cannot be higher than Transaction Amount to Pay";
            return;
        }
        if (listOfTrans.Count != 0)
        {

            string qry2 = "INSERT INTO TransDebit(DebittedAmount,PaymentAmount, ReceiverNumber,ReceiptNumber, TransactionDate,CreatedBy) " +
                                       "VALUES(@AmountPaid,@PaymentAmount, @CollectorId,@ReceiptNumber, @transDate, @CollectorId2)";
            SqlCommand cmd2 = new SqlCommand(qry2, conn);
            if (txt_paymentDate.Text.ToString() == null || txt_paymentDate.Text.ToString() == "")
                txt_paymentDate.Text = DateTime.Now.ToString();
            txt_dob.Text = Session["date"].ToString();
            // DateTime ddd1 = DateTime.ParseExact(txt_paymentDate.Text.ToString(), "dd/MM/yyyy", null);
            cmd2.Parameters.AddWithValue("@PaymentAmount", Convert.ToDecimal(total));
            cmd2.Parameters.AddWithValue("@AmountPaid", amp);
            cmd2.Parameters.AddWithValue("@ReceiptNumber", txt_receiptNumber.Text);
            cmd2.Parameters.AddWithValue("@transDate", Session["date"]);
            cmd2.Parameters.AddWithValue("@CollectorId", userId);
            cmd2.Parameters.AddWithValue("@CollectorId2", loginId);
            conn.Open();
            int status2 = cmd2.ExecuteNonQuery();
            conn.Close();
        }
        foreach (var item in sameTransDate)
        {
            decimal Amount = item.Amount;
            string query2 = "Select top(1) PaymentRecordId from PaymentRecord Order by PaymentRecordId desc";

            //if (item.Reminder != 0)
            //    Amount = item.Reminder;
            if (amp >= Amount)
            {
                string qry = "INSERT INTO PaymentRecord(FundedAmount, AmountPaid, ReceiptNumber, username, PaymentDate, CreatedDate, FundedTransactionId, CollectorId, RegId) " +
                                     "VALUES(@FundedAmount, @AmountPaid, @ReceiptNumber, @username, @PaymentDate, @CreatedDate,  @FundedTransactionId, @CollectorId, @RegId)";
                //string qry2 = "INSERT INTO TransDebit(DebittedAmount,PaymentAmount, ReceiverNumber,ReceiptNumber, TransactionDate,CreatedBy) " +
                //                    "VALUES(@AmountPaid,@PaymentAmount, @CollectorId,@ReceiptNumber, @transDate, @CollectorId)";
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                SqlConnection con = new SqlConnection(OYOClass.connection);
                //SqlCommand cmd2 = new SqlCommand(qry2, con);
                SqlCommand cmd5 = new SqlCommand(query2, con);
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    if (txt_paymentDate.Text.ToString() == null || txt_paymentDate.Text.ToString() == "")
                        txt_paymentDate.Text = DateTime.Now.ToString();
                    txt_dob.Text = Session["date"].ToString();
                    DateTime ddd = DateTime.ParseExact(txt_paymentDate.Text.ToString(), "dd/MM/yyyy", null);
                    cmd.Parameters.AddWithValue("@FundedAmount", Convert.ToDecimal(Amount));
                    cmd.Parameters.AddWithValue("@AmountPaid", Convert.ToDecimal(Amount));
                    //cmd2.Parameters.AddWithValue("@PaymentAmount", Convert.ToDecimal(Amount));
                    //cmd2.Parameters.AddWithValue("@AmountPaid", Convert.ToDecimal(Amount));
                    cmd.Parameters.AddWithValue("@ReceiptNumber", txt_receiptNumber.Text);
                    //cmd2.Parameters.AddWithValue("@ReceiptNumber", txt_receiptNumber.Text);
                    cmd.Parameters.AddWithValue("@username", userName);
                    cmd.Parameters.AddWithValue("@PaymentDate", ddd.ToString());
                    // cmd2.Parameters.AddWithValue("@transDate", Session["date"]);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@FundedTransactionId", item.Id.ToString());
                    cmd.Parameters.AddWithValue("@CollectorId", userId);
                    // cmd2.Parameters.AddWithValue("@CollectorId", collectorList.Text);
                    cmd.Parameters.AddWithValue("@RegId", Id);
                    con.Open();
                    status = cmd.ExecuteNonQuery();
                    con.Close();

                    amp = amp - Amount;

                    con.Open();
                    cmd.CommandText = query2;
                    var retID = cmd5.ExecuteScalar();
                    con.Close();
                    con.Open();
                    string upDate = "update tbl_Payer_Trans set PaymentStatus = 'Paid', SettlementId =\'" + retID + "'  where id = \'" + item.Id + "'";
                    SqlCommand cmd3 = new SqlCommand(upDate, con);
                    cmd3.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if (amp != 0 && amp < Amount)
            {
                string qry = "INSERT INTO PaymentRecord(FundedAmount, AmountPaid, ReceiptNumber, username, PaymentDate, CreatedDate, FundedTransactionId, CollectorId, RegId) " +
                                     "VALUES(@FundedAmount, @AmountPaid, @ReceiptNumber, @username, @PaymentDate, @CreatedDate,  @FundedTransactionId, @CollectorId, @RegId)";
                //string qry2 = "INSERT INTO TransDebit(DebittedAmount,PaymentAmount, ReceiverNumber,ReceiptNumber, TransactionDate,CreatedBy) " +
                //                    "VALUES(@AmountPaid,@PaymentAmount, @CollectorId,@ReceiptNumber, @transDate, @CollectorId)";
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                SqlConnection con = new SqlConnection(OYOClass.connection);
                //SqlCommand cmd2 = new SqlCommand(qry2, con);
                SqlCommand cmd5 = new SqlCommand(query2, con);
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    if (txt_paymentDate.Text.ToString() == null || txt_paymentDate.Text.ToString() == "")
                        txt_paymentDate.Text = DateTime.Now.ToString();
                    txt_dob.Text = Session["date"].ToString();
                    DateTime ddd = DateTime.ParseExact(txt_paymentDate.Text.ToString(), "dd/MM/yyyy", null);
                    cmd.Parameters.AddWithValue("@FundedAmount", Convert.ToDecimal(Amount));
                    cmd.Parameters.AddWithValue("@AmountPaid", amp);
                    //  cmd2.Parameters.AddWithValue("@PaymentAmount", Convert.ToDecimal(Amount));
                    // cmd2.Parameters.AddWithValue("@AmountPaid", amp);
                    cmd.Parameters.AddWithValue("@ReceiptNumber", txt_receiptNumber.Text);
                    // cmd2.Parameters.AddWithValue("@ReceiptNumber", txt_receiptNumber.Text);
                    cmd.Parameters.AddWithValue("@username", userName);
                    cmd.Parameters.AddWithValue("@PaymentDate", ddd.ToString());
                    // cmd2.Parameters.AddWithValue("@transDate", Session["date"]);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@FundedTransactionId", item.Id.ToString());
                    cmd.Parameters.AddWithValue("@CollectorId", userId);
                    // cmd2.Parameters.AddWithValue("@CollectorId", collectorList.Text);
                    cmd.Parameters.AddWithValue("@RegId", Id);
                    con.Open();
                    status = cmd.ExecuteNonQuery();
                    //   int status2 = cmd2.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                    cmd.CommandText = query2;
                    decimal reminder = Amount - amp;
                    var retID = cmd5.ExecuteScalar();
                    con.Close();

                    amp = 0;
                    con.Open();
                    string upDate = "update tbl_Payer_Trans set Reminder= \'" + reminder + "', PaymentStatus = 'Partial', SettlementId =\'" + retID + "'  where id = \'" + item.Id + "'";
                    SqlCommand cmd3 = new SqlCommand(upDate, con);
                    cmd3.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if (amp == 0)
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "Payment successful";
                return;
            }
        }
        if (status > 0)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Payment successful";
            return;
        }
        else
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Payment failed";
            return;
        }
    }

    protected void btn_searchamount_Click(object sender, EventArgs e)
    {

        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");
        var values = txt_startDate.Text.ToString();
        var values1 = txt_endDate.Text.ToString();

        var val = collectorList.Text.ToString();
        if (values == null)
        {
            return;
        }
        if (val == "0" || val == "")
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
        DateTime date1 = ParseDate(values1);
        date1 = date1.AddDays(1);
        if (date > date1)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Date not allowed: start date is greater than end date ";
            return;
        }
        Session["date"] = date;
        Session["date1"] = date1;
        endDateTime = date1;
        DataTable dx = new DataTable();
        dx = (DataTable)Session["lstColl"];
        DataView dv = new DataView(dx);
        dv.RowFilter = "id =\'" + val + "'";
        string filteredDataTable = dv[0][1].ToString();
        Session["user_id2"] = filteredDataTable;
        // var checker = PaymentChecker(filteredDataTable, date, date1);
        CultureInfo provider = CultureInfo.InvariantCulture;
        String loginqry = "";

        loginqry = "SELECT (SELECT Sum(ISNULL([Reminder], 0 ))Amount FROM [tbl_Payer_Trans]where loginUserId = \'" + val + "'and PaymentStatus='Partial' OR PaymentStatus IS NULL  and OffLine_Trans_Date >=\'" + date + "' and OffLine_Trans_Date<\'" + endDateTime + "')+(SELECT Sum([Amount])Amount FROM [tbl_Payer_Trans]where loginUserId = \'" + val + "'and PaymentStatus IS NULL and OffLine_Trans_Date >=\'" + date + "' and OffLine_Trans_Date<\'" + endDateTime + "') As Amount";

        SqlCommand cmd = new SqlCommand(loginqry, con);
        dt.Clear();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        con.Close();

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["amount"].ToString() == "" || dr["amount"].ToString() == "NULL")
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "No Transaction For The Selected Date(s) ";
                return;

            }
            String type = dr["amount"].ToString();
            amountList.Items.Insert(i, new System.Web.UI.WebControls.ListItem(dr["amount"].ToString(), type));
            i++;
        }
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
    private static DateTime NewParseDate(string s)
    {
        DateTime result;
        if (!DateTime.TryParse(s, out result))
        {
            result = DateTime.ParseExact(s, "yyyy-MM-ddT24:mm:ssK", System.Globalization.CultureInfo.InvariantCulture);
            result = result.AddDays(1);
        }
        return result;
    }

    public class TransitionList
    {
        public int Id { get; set; }
        public DateTime TransDate { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Reminder { get; set; }
    }
    private bool ReceiptChecker(string s)
    {
        bool result = false;
        String loginqry = "SELECT* FROM PaymentRecord WHERE ReceiptNumber = \'" + s + "'";
        SqlCommand cmd = new SqlCommand(loginqry, con);
        DataTable dt = new DataTable();
        dt.Clear();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
            result = true;
        con.Close();
        return result;
    }
    private bool PaymentChecker(string s, DateTime st, DateTime ed)
    {
        bool result = false;
        String loginqry = "SELECT * FROM TransDebit where ReceiverNumber = \'" + s + "' and TransactionDate >=\'" + st + "' and TransactionDate<=\'" + ed + "'";
        SqlCommand cmd = new SqlCommand(loginqry, con);
        DataTable dt = new DataTable();
        dt.Clear();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
            result = true;
        con.Close();
        return result;
    }
    private List<TransitionList> GetTransList(DateTime startDate, DateTime endDate)
    {
        var val = collectorList.Text.ToString();
        String loginqry = "SELECT* FROM tbl_Payer_Trans WHERE (PaymentStatus <> 'Paid' OR PaymentStatus is NULL) And OffLine_Trans_Date between \'" + startDate + "' And \'" + endDate + "' And loginUserId = \'" + val + "'";
        SqlCommand cmd = new SqlCommand(loginqry, con);
        DataTable dt = new DataTable();
        dt.Clear();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        List<TransitionList> transList = new List<TransitionList>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal reg = 0;
                if (dt.Rows[i]["Reminder"] != DBNull.Value)
                    reg = Convert.ToDecimal(dt.Rows[i]["Reminder"]);
                TransitionList student = new TransitionList();
                student.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                student.TransDate = Convert.ToDateTime(dt.Rows[i]["offline_trans_date"]);
                student.Amount = Convert.ToDecimal(dt.Rows[i]["Amount"]);
                student.Reminder = reg;
                transList.Add(student);
            }
        }
        con.Close();
        return transList;
    }
    private DateTime[] GetDateList(DateTime startDate, DateTime endDate)
    {
        List<DateTime> datelist = new List<DateTime>();
        while (startDate <= endDate)
        {
            datelist.Add(startDate);
            startDate = startDate.AddDays(1);
        }
        DateTime[] dates = datelist.ToArray();
        return dates;
    }


}