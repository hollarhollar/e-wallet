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


public partial class WalletBalance : System.Web.UI.Page
{
    

    public class MainJson
    {

       //  public System.Collections.ObjectModel.Collection<mfsCommonServiceResponse> mfsCommonServiceResponse { get; set; }
        public string mfsCommonServiceResponse { get; set; }
      
        // public System.Collections.ObjectModel.Collection<mfsResponseInfo> mfsResponseInfo { get; set; }
        public string mfsResponseInfo { get; set; }
        public string mfsOptionalInfo { get; set; }

    }

    public class mfsResponseInfo
    {

        public string token { get; set; }

        public string tokenType { get; set; }

        
    }


    public class mfsCommonServiceResponse
    {

        public System.Collections.ObjectModel.Collection<mfsStatusInfo> mfsStatusInfo { get; set; }

        public System.Collections.ObjectModel.Collection<mfsTransactionInfo> mfsTransactionInfo { get; set; }

        
    }

    public class mfsStatusInfo
    {

        public string status { get; set; }

        public string errorCode { get; set; }

        public string errorDescription { get; set; }


    }

    public class mfsTransactionInfo
    {

        public string timestamp { get; set; }

        public string requestId { get; set; }

        public string responseId { get; set; }


    }


    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        string val = "";
        if (Request.QueryString["mobileNo"] != null)
        {
            val = Request.QueryString["mobileNo"].ToString();
            Session["mobileNo"] = val;
            Response.Redirect("WalletBalance.aspx");
        }

        if (Session["mobileNo"] == null)
        {
            Response.Redirect("../Login.aspx");
        }

        //string URI = "https://apidev.nownowpay.com.ng/mfs-transaction-management/authManagement/get";
        string URI = "https://nownowpay.com.ng/mfs-transaction-management/authManagement/get";
        
        string myParameters = "{\r\n  \"mfsCommonServiceRequest\": {\r\n    \"mfsSourceInfo\": {\r\n      \"channelId\": 23,\r\n      \"surroundSystem\": 1\r\n    },\r\n    \"mfsTransactionInfo\": {\r\n      \"requestId\": \"1528888797115\",\r\n      \"serviceType\": \"501\",\r\n      \"timestamp\": \"1528888797115\"\r\n    }\r\n  }\r\n}";
        string InsCompRes = "";
        using (WebClient wc = new WebClient())
        {
            wc.Headers[HttpRequestHeader.Authorization] = "Basic c3luZXJneTpTeW5lcmd5IzB3IzB3==";
            
            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
            
            InsCompRes = wc.UploadString(URI, myParameters);

            var result = JObject.Parse(InsCompRes);

            JObject parsed = JObject.Parse(InsCompRes);


            if (parsed["mfsCommonServiceResponse"]["mfsStatusInfo"]["status"].ToString() == "Success")
            {
                Session["BearerToken"] = parsed["mfsResponseInfo"]["token"].ToString();
            }


            string URI_wallet_bal = "https://apidev.nownowpay.com.ng/mfs-transaction-management/userManagement/getUserInfo";
            string myParameters_wallet_bal = "{\n    \"mfsCommonServiceRequest\": {\n        \"mfsSourceInfo\": {\n            \"channelId\": \"23\",\n            \"surroundSystem\": \"1\"\n        },\n        \"mfsTransactionInfo\": {\n            \"requestId\": \"97006\",\n            \"serviceType\": \"0\",\n            \"timestamp\": 1542380597\n        }\n    },\n    \"mfsRequestInfo\": {\n        \"customerMsisdn\": \"" + Session["mobileNo"].ToString().Trim() + "\"\n    }\n}";
            string InsCompRes_wallet_bal = "";

            wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + Session["BearerToken"];

            wc.Headers[HttpRequestHeader.ContentType] = "application/json";

            InsCompRes_wallet_bal = wc.UploadString(URI_wallet_bal, myParameters_wallet_bal);
            JObject parsed_wallet = JObject.Parse(InsCompRes_wallet_bal);

            if (parsed_wallet["mfsCommonServiceResponse"]["mfsStatusInfo"]["status"].ToString() != "Failure.")
            {
                if (parsed_wallet["mfsResponseInfo"]["mfsEntityDetailsListInfo"]["mfsEntityInfo"][0]["walletInfo"]["wallet"][0]["walletType"].ToString() == "1")
                {
                    txt_fname.Text = parsed_wallet["mfsResponseInfo"]["mfsEntityDetailsListInfo"]["mfsEntityInfo"][0]["firstName"].ToString();
                    txt_lname.Text = parsed_wallet["mfsResponseInfo"]["mfsEntityDetailsListInfo"]["mfsEntityInfo"][0]["lastName"].ToString();
                    txt_gender.Text = parsed_wallet["mfsResponseInfo"]["mfsEntityDetailsListInfo"]["mfsEntityInfo"][0]["gender"].ToString();
                    txt_email.Text = parsed_wallet["mfsResponseInfo"]["mfsEntityDetailsListInfo"]["mfsEntityInfo"][0]["email"].ToString();
                    txt_CurrentBal.Text = parsed_wallet["mfsResponseInfo"]["mfsEntityDetailsListInfo"]["mfsEntityInfo"][0]["walletInfo"]["wallet"][0]["curBalance"].ToString();
                    txt_AvailableBal.Text = parsed_wallet["mfsResponseInfo"]["mfsEntityDetailsListInfo"]["mfsEntityInfo"][0]["walletInfo"]["wallet"][0]["availBalance"].ToString();


                }
            }

            else
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "User Not Registered on Now Now!!";
               // Response.Redirect("List_of_Collectors.aspx");
            }
        }
    }
}