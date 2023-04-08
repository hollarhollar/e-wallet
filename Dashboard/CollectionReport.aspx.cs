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
using System.Web.UI.HtmlControls;


public partial class Dashboard_CollectionReport : System.Web.UI.Page
{
    int Interval = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lga"] == null)
        {
            Response.Redirect("../Login.aspx");
        }

        ((Label)this.Master.FindControl("lblPage")).Text = "Collection Report";
        HtmlGenericControl li = new HtmlGenericControl("li");
        this.Master.FindControl("tabs").Controls.Add(li);

        HtmlGenericControl anchor = new HtmlGenericControl("a");
        anchor.Attributes.Add("href", "CollectionReport.aspx");
        anchor.InnerText = "Collection Report";

        li.Controls.Add(anchor)
;
        TheMethod();



    }
    protected void TheMethod()
    {
        string loginqry = "";
        if (int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 105)
        {
            loginqry = "select 0 as wallet_fund,0 as wallet_balance,(case when(amount is null) then 0 else amount end)as revenue_collected ,beat_code,mobile_no as mobile_number_1,first_name+' '+last_name as collector,(select lga from Local_Government_Areas where Local_Government_Areas.lga_id=oyo_Registration.lga_id) as lga from oyo_Registration left join (select sum(amount) as amount,loginUserId from tbl_Payer_Trans group by loginUserId )as b on mobile_no=loginUserId where type_code=106 and lga_id=" + Session["lga"].ToString();
        }
        else
        {
            loginqry = "select 0 as wallet_fund,0 as wallet_balance,(case when(amount is null) then 0 else amount end)as revenue_collected ,beat_code,mobile_no as mobile_number_1,first_name+' '+last_name as collector,(select lga from Local_Government_Areas where Local_Government_Areas.lga_id=oyo_Registration.lga_id) as lga from oyo_Registration left join (select sum(amount) as amount,loginUserId from tbl_Payer_Trans group by loginUserId )as b on mobile_no=loginUserId where type_code=106 ";
        }

        SqlConnection con = new SqlConnection(OYOClass.connection);
        SqlCommand cmd_lga = new SqlCommand(loginqry, con);

        DataTable dt_lga_detail = new DataTable();
        con.Open();
        SqlDataAdapter da_lga = new SqlDataAdapter(cmd_lga);
        da_lga.Fill(dt_lga_detail);
        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + dt_lga_detail + ")", true);

        con.Close();
        Session["dataTableCollector"] = dt_lga_detail;
        //dt_lga_detail.Columns["wallet_fund"].DataType = typeof(System.String);
        string LGAqury = "Select * from Local_Government_Areas where lga_id=" + Session["lga"].ToString();

        SqlCommand cmd_LGAqury = new SqlCommand(LGAqury, con);

        DataTable dt_lga_detail_MB = new DataTable();
        con.Open();
        SqlDataAdapter da_lga_MB = new SqlDataAdapter(cmd_LGAqury);
        da_lga_MB.Fill(dt_lga_detail_MB);
        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + dt_lga_detail + ")", true);

        con.Close();
        if (dt_lga_detail.Rows.Count == 0)
        {
            grd_collector.DataSource = dt_lga_detail;
            grd_collector.DataBind();
            return;
        }
        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        Session["reciever_mobileno"] = Session["user_id"].ToString();
        String number = Session["reciever_mobileno"].ToString();
        string URI = "https://nownowpay.com.ng/mfs-transaction-management/authManagement/get";
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        string myParameters = "{\"mfsCommonServiceRequest\": {\"mfsSourceInfo\": {\"channelId\": \"22\",\"surroundSystem\": \"1\"}, \"mfsTransactionInfo\": { \"requestId\": \"8447220123153311724518095445\",\"serviceType\": \"0\",\"timestamp\": \"1541084236965\"} }}";
        string BearerToken = "";
        string method = "POST";
        using (WebClient wc = new WebClient())
        {
            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
            wc.Headers[HttpRequestHeader.Authorization] = "Basic YYXBpQ2xpZW50OmFwaUNsaWVudFNlY3JldA==";

            BearerToken = wc.UploadString(URI, method, myParameters);

        }

        var root = JObject.Parse(BearerToken.ToString());
        String entityId = Session["EntityId"].ToString();

        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        string URI_get_history = "https://nownowpay.com.ng/mfs-transaction-management/transactionManagement/newHistory";
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        string Historyrequest = "{\"mfsCommonServiceRequest\": {  \"mfsAssistedTransaction\": {      },  \"mfsSourceInfo\": {    \"channelId\": \"23\",    \"surroundSystem\": \"4\"  },  \"mfsTransactionInfo\": {    \"requestId\": 397245733990663,    \"serviceType\": \"0\",    \"timestamp\": 1536937408412  }},\"mfsOptionalInfo\": {  },\"mfsRequestInfo\": {  \"bankDetails\": {      },  \"count\": \"\",  \"deviceId\": \"de8c7f52c25420d5\",  \"deviceIp\": \"192.168.0.3\",  \"entityId\": \"" + entityId + "\",  \"entityType\": \"85\",  \"fieldList\": {      },  \"language\": \"ENG\",  \"offSet\": \"0\",  \"walletType\": \"1\"}}";
        string InsCompResHistory = "";
        JObject Historyres;
        using (WebClient wc = new WebClient())
        {
            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
            wc.Headers[HttpRequestHeader.Authorization] = "Basic YYXBpQ2xpZW50OmFwaUNsaWVudFNlY3JldA==";

            //  wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + root["mfsResponseInfo"]["token"].ToString();
            // string json = JsonConvert.SerializeObject(Assessment);
            InsCompResHistory = wc.UploadString(URI_get_history, method, Historyrequest);

            Historyres = JObject.Parse(InsCompResHistory);

        }
        if (int.Parse(Historyres["mfsCommonServiceResponse"]["mfsStatusInfo"]["errorCode"].ToString()) == 100)
        {
            for (int i = 0; i < Historyres["mfsResponseInfo"]["transactionListInfo"]["transactionInfo"].Count(); i++)
            {
                if (Historyres["mfsResponseInfo"]["transactionListInfo"]["transactionInfo"][i]["transactionStatus"].ToString().Equals("SUCCESS") && Historyres["mfsResponseInfo"]["transactionListInfo"]["transactionInfo"][i]["transactionInfo"].ToString().Equals("DR"))
                {
                    DataRow[] rows = dt_lga_detail.Select("mobile_number_1 = '" + Historyres["mfsResponseInfo"]["transactionListInfo"]["transactionInfo"][i]["receiverMsisdn"].ToString() + "'");
                    if (rows.Count() > 0)
                    {
                        int index = dt_lga_detail.Rows.IndexOf(rows[0]);
                        dt_lga_detail.Rows[index]["wallet_fund"] = int.Parse(dt_lga_detail.Rows[index]["wallet_fund"].ToString()) + int.Parse(Historyres["mfsResponseInfo"]["transactionListInfo"]["transactionInfo"][i]["transactionAmount"].ToString());
                    }
                }
            }
        }
        List<String> msisdnList = new List<String>();
        for (int i = 0; i < dt_lga_detail.Rows.Count; i++)
        {
            String msisdn = dt_lga_detail.Rows[i]["mobile_number_1"].ToString();
            msisdnList.Add(msisdn);
        }
        string jsonMsisdnList = JsonConvert.SerializeObject(msisdnList);


        String getWalletRequest = "{\"msisdn\":" + jsonMsisdnList + "}";
        string getWalletUrl = "http://34.254.53.229:8080/admin-panel/partner/getBalance";
       // string getWalletUrl = "https://nownowpay.com.ng/admin-panel/partner/getBalance";
        string getWalletResponse = "";
        JObject getWalletJson;
        using (WebClient wc = new WebClient())
        {
            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
            wc.Headers["token"] = "B4BC4BA0A8E1CD8F1DBDEE86AC3CBF31F59C32C25C9162F02EFFA6ED966";
            // string json = JsonConvert.SerializeObject(Assessment);
            getWalletResponse = wc.UploadString(getWalletUrl, method, getWalletRequest);

            getWalletJson = JObject.Parse(getWalletResponse);

        }
        for (int i = 0; i < getWalletJson["data"].Count(); i++)
        {
            DataRow[] rows = dt_lga_detail.Select("mobile_number_1 = '" + getWalletJson["data"][i]["msisdn"].ToString() + "'");
            if (rows.Count() > 0)
            {
                int index = dt_lga_detail.Rows.IndexOf(rows[0]);
                dt_lga_detail.Rows[index]["wallet_balance"] = int.Parse(getWalletJson["data"][i]["balance"].ToString());
            }
        }

        grd_collector.DataSource = dt_lga_detail;
        grd_collector.DataBind();



        int pagesize = grd_collector.Rows.Count;
        int from_pg = 1;
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

        DataTable dt_list_s = new DataTable();
        dt_list_s = (DataTable)Session["dataTableCollector"];
        // DataRow[] filteredRows = dt_list_s.Select("TaxPayerRIN LIKE '" + txt_RIN.Text + "'");
        DataTable dt_filtered = new DataTable();
        DataView dt_v = dt_list_s.DefaultView;
        if (txt_employer_RIN.Text != "")
        {
            dt_v.RowFilter = "collector like '%" + txt_employer_RIN.Text + "%' or lga like '%" + txt_employer_RIN.Text + "%' or Convert(wallet_fund, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(revenue_collected, 'System.String') like '%" + txt_employer_RIN.Text + "%' or convert(wallet_balance, 'System.String') like '%" + txt_employer_RIN.Text + "%'";


        }




        grd_collector.DataSource = dt_v;
        grd_collector.DataBind();

        int pagesize = grd_collector.Rows.Count;
        int from_pg = 1;
        int to = grd_collector.Rows.Count;
        int totalcount = dt_v.Count;


        if (totalcount < grd_collector.PageSize)
            div_paging.Style.Add("margin-top", "0px");
        else
            div_paging.Style.Add("margin-top", "-60px");
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