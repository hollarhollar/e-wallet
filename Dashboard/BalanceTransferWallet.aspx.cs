//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Net;
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Text.RegularExpressions;
//using System.Data.SqlTypes;
//using System.Text;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.IO;
//using System.Collections;
//using System.Web.UI.HtmlControls;
//using System.Security.Cryptography;

//public partial class Dashboard_BalanceTransferWallet : System.Web.UI.Page
//{
//    SqlConnection conn = new SqlConnection(OYOClass.connection);
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if(Session["user_id"]==null){
//             Response.Redirect("../Login.aspx");
//        }
//        if (!IsPostBack)
//        {
//            txt_sender_MobileNo.Text = "7838941249";
//            //txt_sender_MobileNo.Text = Session["user_id"].ToString();

//            if (Request.QueryString["mobileNo"] != null)
//            {
//                Session["mobileNo_reciever"] = Request.QueryString["mobileNo"].ToString();
//                Session["EntityId_reciever"] = Request.QueryString["EntityId"].ToString();

//                Response.Redirect("BalanceTransferWallet.aspx");
//            }
//        }

//        if (Session["mobileNo_reciever"] == null)
//        {
//            Response.Redirect("../Login.aspx");
//        }
//        ((Label)this.Master.FindControl("lblPage")).Text = "Fund Wallet Balance";
//        HtmlGenericControl li = new HtmlGenericControl("li");
//        this.Master.FindControl("tabs").Controls.Add(li);

//        HtmlGenericControl anchor = new HtmlGenericControl("a");
//        anchor.Attributes.Add("href", "BalanceTransferWallet.aspx");
//        anchor.InnerText = "Fund Wallet Balance";
//        li.Controls.Add(anchor);
//        txt_reciever_MobileNo.Text = Session["mobileNo_reciever"].ToString().Trim();
//    }
//    protected void btnSubmit_Click(object sender, EventArgs e)
//    {
//         HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
//        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

//        try
//        {
//            string URI = "https://nownowpay.com.ng/mfs-transaction-management/authManagement/get";
//            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
//            string myParameters = "{\"mfsCommonServiceRequest\": {\"mfsSourceInfo\": {\"channelId\": \"22\",\"surroundSystem\": \"1\"}, \"mfsTransactionInfo\": { \"requestId\": \"8447220123153311724518095445\",\"serviceType\": \"0\",\"timestamp\": \"1541084236965\"} }}";
//            string BearerToken = "";
//            using (WebClient wc = new WebClient())
//            {
//                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
//                wc.Headers[HttpRequestHeader.Authorization] = "Basic c3luZXJneTpTeW5lcmd5IzB3IzB3==";

//                BearerToken = wc.UploadString(URI, myParameters);
//            }

//            var root = JObject.Parse(BearerToken.ToString());

//            //string toQuery = "Select first_name + ' ' + last_name Name, EntityId from oyo_Registration where mobile_no = \'" + txt_reciever_MobileNo.Text + "\' and type_code = 106";
//            string toQuery = "Select first_name + ' ' + last_name Name, EntityId from oyo_Registration where mobile_no = '" + txt_reciever_MobileNo.Text + "' OR mobile_no = '7838941249'";
//           // string toQuery = "Select first_name + ' ' + last_name Name, EntityId from oyo_Registration where mobile_no = '" + txt_reciever_MobileNo.Text + "' OR mobile_no = '" + txt_sender_MobileNo.Text + "'";
//            SqlConnection conTo = new SqlConnection(OYOClass.connection);
//            SqlCommand cmd_to = new SqlCommand(toQuery, conTo);

//            DataTable dt_to_detail = new DataTable();
//            conTo.Open();
//            SqlDataAdapter da_lga = new SqlDataAdapter(cmd_to);
//            da_lga.Fill(dt_to_detail);
//            conTo.Close();

//            var fromName = dt_to_detail.Rows[0][0].ToString();
//            var fromEntityId = dt_to_detail.Rows[0][1].ToString();

//            var toName = dt_to_detail.Rows[1][0].ToString();
//            var toEntityId = dt_to_detail.Rows[1][1].ToString();




//            //string URI_transferMoney = "http://apidev.nownowpay.com.ng/mfs-transaction-management/balanceManagement/transfer";
//            string URI_transferMoney = "https://apidev.nownowpay.com.ng/mfs-transaction-management/balanceManagement/transfer";
//            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
//            TimeSpan Now = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1));
//            long TimeStamp = Convert.ToInt64(Math.Floor(Now.TotalMilliseconds));
//            string timestamp = TimeStamp.ToString();
//            Random random = new Random();
//            String randomNumber = OYOClass.getRequestId();


//            //string myParameters_transferMoney1 = "{\"mfsCommonServiceRequest\": {\"mfsSourceInfo\": {\"channelId\": \"23\"," +
//            //    "\"entityId\": \"" + Session["EntityId"].ToString() + "\",\"entityType\":\"85\",\"mpin\": \"" + CreateMD5(txt_mpin.Text.ToString()) + "\"," +
//            //    "\"msisdn\": \"" + txt_sender_MobileNo.Text + "\",\"surroundSystem\": \"1\"}," +
//            //    "\"mfsTransactionInfo\": {\"requestId\": \"" + randomNumber + "\",\"serviceType\": \"509\",\"timestamp\": \"" + timestamp + "\"}}," +
//            //    "\"mfsRequestInfo\": {\"amount\":\"" + txt_amount.Text + "\",\"bankDetails\": {\"benificiaryAccount\": \"" + txt_reciever_MobileNo.Text + "\",\"bin\": \"999099\"," +
//            //    "\"destinationName\": \"" +toName.ToString() + "\",\"msisdn\": \"" + txt_sender_MobileNo.Text + "\"}," +
//            //    "\"fromEntityId\": \"" + Session["EntityId"].ToString() + "\",\"walletTypeToDeduct\": \"1\"}}";




//            //string myParameters_transferMoney = "{\"mfsRequestInfo\": {\"senderMsisdn\": \"" + txt_reciever_MobileNo.Text + "\",\"fromEntityId\": \"" + Session["EntityId"].ToString() + "\"," +
//            //    "\"amount\": \"" + txt_amount.Text + "\",\"remark\": \"Send Money to EIRS Wallet\",\"toEntityId\": \"" + toEntityId.ToString() + "\",\"walletTypeToCredit\": \"1\"}," +
//            //    "\"mfsCommonServiceRequest\": {\"mfsSourceInfo\": {\"senderMsisdn\": \"" + txt_reciever_MobileNo.Text + "\",\"surroundSystem\": \"1\"," +
//            //    "\"entityId\": \"" + toEntityId.ToString() + "\",\"mpin\": \"" + CreateMD5(txt_mpin.Text.ToString()) + "\",\"channelId\": \"23\"}," +
//            //    "\"mfsTransactionInfo\": {\"serviceType\": \"501\",\"requestId\"" + randomNumber + ",\"timestamp\": " + timestamp + "}}}";


//            //string para1 = "{ \"mfsRequestInfo\": { \"senderMsisdn\": \"" + agentMobilNo + "\", \"fromEntityId\": \""
//            //        + FromEntityId + "\", \"amount\": \"" + amount + "\", \"remark\": \"Send Money to EIRS Wallet\", \"toEntityId\": \"" +
//            //        ConfigurationManager.AppSettings["ToEntityId"].ToString() + "\", \"walletTypeToCredit\": \"1\" }, \"mfsCommonServiceRequest\": { \"mfsSourceInfo\": { \"senderMsisdn\": \"" +
//            //         agentMobilNo + "\", \"surroundSystem\": \"1\", \"entityId\": \"" +
//            //        //ConfigurationManager.AppSettings["ToEntityId"].ToString() + "\", \"mpin\": \"" + 
//            //        FromEntityId + "\", \"mpin\": \"" +
//            //        Mpin + "\", \"channelId\": \"23\" }, \"mfsTransactionInfo\": { \"serviceType\": \"501\", \"requestId\": " +
//            //        RequestId + ", \"timestamp\": " + TimeStamp.ToString() + " }}} ";
//            //txt_reciever_MobileNo.Text = "7838941249";
//            string entityId = "82000001308";
//            string para = "{ \"mfsRequestInfo\": { \"senderMsisdn\": \"" + txt_sender_MobileNo.Text + "\", \"fromEntityId\": \""
//                    + entityId + "\", \"amount\": \"" + txt_amount.Text + "\", \"remark\": \"Send Money to EIRS Wallet\", \"toEntityId\": \"" +
//                    toEntityId + "\", \"walletTypeToCredit\": \"1\" }, \"mfsCommonServiceRequest\": { " +
//                    "\"mfsSourceInfo\": { \"senderMsisdn\": \"" + txt_sender_MobileNo.Text + "\", \"surroundSystem\": \"1\", \"entityId\": \"" +
//                    //ConfigurationManager.AppSettings["ToEntityId"].ToString() + "\", \"mpin\": \"" + 
//                    Session["EntityId"].ToString() + "\", \"mpin\": \"" +OYOClass.CreateMD5(txt_mpin.Text.ToString()) + "\", \"channelId\": \"23\" }, \"mfsTransactionInfo\": { \"serviceType\": \"501\", \"requestId\": " +
//                    randomNumber + ", \"timestamp\": " + timestamp.ToString() + " }}} ";


//            string SendMoneyResult = "";
//            using (WebClient wc = new WebClient())
//            {
//                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
//                wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + root["mfsResponseInfo"]["token"].ToString();
//                SendMoneyResult = wc.UploadString(URI_transferMoney, para);

//                var result = JObject.Parse(SendMoneyResult);

//                JObject parsed = JObject.Parse(SendMoneyResult);

//                if (parsed["mfsCommonServiceResponse"]["mfsStatusInfo"]["status"].ToString().Contains("Failure"))
//                {
//                    modalinfo.Attributes.Add("class", "modal show");
//                    lblmodalbody.Text = "Something went wrong <br />  error: " + parsed["mfsCommonServiceResponse"]["mfsStatusInfo"]["errorDescription"].ToString();

//                    return;
//                }

//                else
//                {
//                    String senderMobile = "7838941249";
//                   // String senderMobile = parsed["mfsResponseInfo"]["senderMsisdn"].ToString();
//                    String receiverMobile = parsed["mfsResponseInfo"]["receiverMsisdn"].ToString();
//                    String transactionid = parsed["mfsResponseInfo"]["transactionId"].ToString();
//                    String amount = parsed["mfsResponseInfo"]["transactionAmount"].ToString();
//                    String requestId = parsed["mfsCommonServiceResponse"]["mfsTransactionInfo"]["requestId"].ToString();
//                    String balancewallet = OYOClass.getCurrentWalletBalance(receiverMobile);
//                    string qry = "Insert into addWalletHistory (sender_mobile,receiver_mobile,amount,requestId,transactionId,created_at,walletBalnce)" +
//                        " values ('" + senderMobile + "','" + receiverMobile + "','" + amount + "','" + requestId + "','" + transactionid + "',CURRENT_TIMESTAMP,'" + balancewallet + "')";
//                    SqlConnection con = new SqlConnection(OYOClass.connection);
//                    SqlCommand cmd = new SqlCommand(qry, con);
//                    con.Open();
//                    int status = cmd.ExecuteNonQuery();

//                    con.Close();
//                    string balance=OYOClass.getCurrentWalletBalance(Session["user_id"].ToString());
//                    ((Label)Master.FindControl("lblAmount")).Text = balance;

//                    modalinfo.Attributes.Add("class", "modal show");
//                    lblmodalbody.Text = "Transaction successfull";
//                }
//            }
//        }

//        catch (Exception ex)
//        {
//            var mst = ex.Message;
//            modalinfo.Attributes.Add("class", "modal show");
//            lblmodalbody.Text = "Something Went Wrong!!";
//        }
//    }

//  //  public static string CreateMD5(string input)
//  //  {
//		//input= input + "Dbbwjvj$%)GE$5SGr@3VsHYUMas2323E4d57vfBfFSTRU@!DSH(*%FDSdfg13sgfsg";
//  //      // Use input string to calculate MD5 hash
//  //      using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
//  //      {
//  //          //byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
//  //          byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

//  //          // Convert the byte array to hexadecimal string
//  //          StringBuilder sb = new StringBuilder();
//  //          for (int i = 0; i < hashBytes.Length; i++)
//  //          {
//  //              sb.Append(hashBytes[i].ToString("x2"));
//  //          }
//  //          return sb.ToString();
//  //     //'
//  //     }
//  //  }

//    private static string GenerateHash(string value)
//    {
//        var data = System.Text.Encoding.ASCII.GetBytes(value);
//        data = System.Security.Cryptography.MD5.Create().ComputeHash(data);
//        return Convert.ToBase64String(data);
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data.SqlTypes;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;

public partial class Dashboard_BalanceTransferWallet : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(OYOClass.connection);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_id"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            // = "7838941249";
            txt_sender_MobileNo.Text = Session["user_id"].ToString();

            if (Request.QueryString["mobileNo"] != null)
            {
                Session["mobileNo_reciever"] = Request.QueryString["mobileNo"].ToString();
                Session["EntityId_reciever"] = Request.QueryString["EntityId"].ToString();

                Response.Redirect("BalanceTransferWallet.aspx");
            }
        }

        if (Session["mobileNo_reciever"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        ((Label)this.Master.FindControl("lblPage")).Text = "Fund Wallet Balance";
        HtmlGenericControl li = new HtmlGenericControl("li");
        this.Master.FindControl("tabs").Controls.Add(li);

        HtmlGenericControl anchor = new HtmlGenericControl("a");
        anchor.Attributes.Add("href", "BalanceTransferWallet.aspx");
        anchor.InnerText = "Fund Wallet Balance";
        li.Controls.Add(anchor);
        txt_reciever_MobileNo.Text = Session["mobileNo_reciever"].ToString().Trim();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");


        try
        {

            //string toQuery = "Select first_name + ' ' + last_name Name, EntityId from oyo_Registration where mobile_no = \'" + txt_reciever_MobileNo.Text + "\' and type_code = 106";
            string toQuery = "Select first_name + ' ' + last_name Name, EntityId from oyo_Registration where mobile_no = '" + txt_reciever_MobileNo.Text + "' OR mobile_no = '7838941249'";
            // string toQuery = "Select first_name + ' ' + last_name Name, EntityId from oyo_Registration where mobile_no = '" + txt_reciever_MobileNo.Text + "' OR mobile_no = '" + txt_sender_MobileNo.Text + "'";
            SqlConnection conTo = new SqlConnection(OYOClass.connection);
            SqlCommand cmd_to = new SqlCommand(toQuery, conTo);

            DataTable dt_to_detail = new DataTable();
            conTo.Open();
            SqlDataAdapter da_lga = new SqlDataAdapter(cmd_to);
            da_lga.Fill(dt_to_detail);
            conTo.Close();

            var fromName = dt_to_detail.Rows[0][0].ToString();
            var fromEntityId = dt_to_detail.Rows[0][1].ToString();

            var toName = dt_to_detail.Rows[1][0].ToString();
            var toEntityId = dt_to_detail.Rows[1][1].ToString();




            //string URI_transferMoney = "http://apidev.nownowpay.com.ng/mfs-transaction-management/balanceManagement/transfer";
            string URI_transferMoney = "http://52.56.39.59:2076/api/BalanceManagement/credit-collector";
            // string URI_transferMoney = "https://apidev.nownowpay.com.ng/mfs-transaction-management/balanceManagement/transfer";
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //TimeSpan Now = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1));
            //long TimeStamp = Convert.ToInt64(Math.Floor(Now.TotalMilliseconds));
            //string timestamp = TimeStamp.ToString();
            //Random random = new Random();
            //String randomNumber = OYOClass.getRequestId();

            string ent = Session["user_id"].ToString();
            string para = "{ \"senderMsisdn\": \"" + txt_sender_MobileNo.Text + "\", \"userId\": \""
                    + ent + "\", \"value\": \"" + txt_amount.Text + "\", \"receiverMsisdn\":\"" + txt_reciever_MobileNo.Text + "\"} ";

            string SendMoneyResult = "";
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                // wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + root["mfsResponseInfo"]["token"].ToString();
                SendMoneyResult = wc.UploadString(URI_transferMoney, para);

                var result = JObject.Parse(SendMoneyResult);

                JObject parsed = JObject.Parse(SendMoneyResult);
                //     ResponseModel account = JsonConvert.DeserializeObject<ResponseModel>(parsed);
                var kk = parsed["status"].ToString();

                if (kk.ToLower() != "true")
                {
                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = "Something went wrong <br />  error: " + parsed["mfsCommonServiceResponse"]["mfsStatusInfo"]["errorDescription"].ToString();

                    return;
                }
                else
                {
                    string senderMobile = txt_sender_MobileNo.Text;
                    //sender mobile was masked to the login user to bypass some implementation errors.
                    String userId = Session["user_id"].ToString();
                    String receiverMobile = txt_reciever_MobileNo.Text;
                    String transactionid = parsed["transId"].ToString();
                    String amount = txt_amount.Text;
                    String requestId = "";
                    String balancewallet = parsed["amount"].ToString();
                    string qry = "Insert into addWalletHistory (sender_mobile,receiver_mobile,UserPhoneNumber,amount,requestId,transactionId,created_at,walletBalnce)" +
                        " values ('" + senderMobile + "','" + receiverMobile + "','" + userId + "','" + amount + "','" + requestId + "','" + transactionid + "',CURRENT_TIMESTAMP,'" + balancewallet + "')";
                    SqlConnection con = new SqlConnection(OYOClass.connection);
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    int status = cmd.ExecuteNonQuery();

                    con.Close();
                    string balance = OYOClass.getCurrentWalletBalance(Session["user_id"].ToString());
                    ((Label)Master.FindControl("lblAmount")).Text = balance;

                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = "Transaction successfull";
                }
            }
        }

        catch (Exception ex)
        {
            var mst = ex.Message;
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Something Went Wrong!!";
        }
    }


    //  public static string CreateMD5(string input)
    //  {
    //input= input + "Dbbwjvj$%)GE$5SGr@3VsHYUMas2323E4d57vfBfFSTRU@!DSH(*%FDSdfg13sgfsg";
    //      // Use input string to calculate MD5 hash
    //      using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
    //      {
    //          //byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
    //          byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

    //          // Convert the byte array to hexadecimal string
    //          StringBuilder sb = new StringBuilder();
    //          for (int i = 0; i < hashBytes.Length; i++)
    //          {
    //              sb.Append(hashBytes[i].ToString("x2"));
    //          }
    //          return sb.ToString();
    //     //'
    //     }
    //  }

    private static string GenerateHash(string value)
    {
        var data = System.Text.Encoding.ASCII.GetBytes(value);
        data = System.Security.Cryptography.MD5.Create().ComputeHash(data);
        return Convert.ToBase64String(data);
    }
}