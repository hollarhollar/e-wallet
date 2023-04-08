using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        if (Session["user_id"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else 
        {
            //lblusername.Text = Session["username"].ToString();
            lblPage.Text = Session["username"].ToString();
            lblrole.Text = " " + Session["user_role"].ToString();
        }
        //scratchcard.Attributes.Add("style", "display:none");

        var typeCode = int.Parse(Session["type_code"].ToString());
        if (typeCode != 100 && typeCode != 105)
        {
            if (typeCode != 104 && typeCode != 103)
            {
                usermanagment.Style["visibility"] = "hidden";
                //scratchcard.Attributes.Add("style", "display:none;");

                if (typeCode != 104 && typeCode != 105)
                {
                    fundwallet.Style["visibility"] = "hidden";
                }
            }

        }

        if (int.Parse(Session["type_code"].ToString()) == 108 || int.Parse(Session["type_code"].ToString()) == 109 ||
            int.Parse(Session["type_code"].ToString()) == 111 || int.Parse(Session["type_code"].ToString()) == 112)
        {
            verifyReceiptNumber.Attributes.Add("style", "display:none;");
           // verifyReceiptQrCode.Attributes.Add("style", "display:none;");
            collectorsReport.Attributes.Add("style", "display:none;");
            monthWiseSummary.Attributes.Add("style", "display:none;");
            harmonizeTricycleAndBikes.Attributes.Add("style", "display:none;");
            revenueBeatReport.Attributes.Add("style", "display:none;");
            yearWiseSummary.Attributes.Add("style", "display:none;");
            collectionByRevenueStream.Attributes.Add("style", "display:none;");
        }
        if (int.Parse(Session["type_code"].ToString()) == 110 || int.Parse(Session["type_code"].ToString()) == 113)
        {
            verifyReceiptNumber.Attributes.Add("style", "display:none;");
            //verifyReceiptQrCode.Attributes.Add("style", "display:none;");
            collectorsReport.Attributes.Add("style", "display:none;");
            monthWiseSummary.Attributes.Add("style", "display:none;");
            harmonizeBusesAndTaxis.Attributes.Add("style", "display:none;");
            revenueBeatReport.Attributes.Add("style", "display:none;");
            yearWiseSummary.Attributes.Add("style", "display:none;");
            collectionByRevenueStream.Attributes.Add("style", "display:none;");
        }

        if (int.Parse(Session["type_code"].ToString()) == 103 || int.Parse(Session["type_code"].ToString()) == 105)
        {
            usermanagment.Attributes.Add("style", "display:none;");
            payment.Attributes.Add("style", "display:none;");
            fundCollectorWallet.Attributes.Add("style", "display:none;");
         //   scratchcard.Attributes.Add("style", "display:none;");
            //paymentHistory.Attributes.Add("style", "display:none;");
            //underPayment.Attributes.Add("style", "display:none;");
        }

        

        try
        {
            if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 104)
            {
                string URI = "https://nownowpay.com.ng/mfs-transaction-management/authManagement/get";
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                string myParameters = "{\"mfsCommonServiceRequest\": {\"mfsSourceInfo\": {\"channelId\": \"22\",\"surroundSystem\": \"1\"}, \"mfsTransactionInfo\": { \"requestId\": \"8447220123153311724518095445\",\"serviceType\": \"0\",\"timestamp\": \"1541084236965\"} }}";
                string BearerToken = "";
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    wc.Headers[HttpRequestHeader.Authorization] = "Basic c3luZXJneTpTeW5lcmd5IzB3IzB3==";

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Tls12
                           | SecurityProtocolType.Ssl3;

                    BearerToken = wc.UploadString(URI, myParameters);
                }

                var root = JObject.Parse(BearerToken.ToString());
                string token = root["mfsResponseInfo"]["token"].ToString();
                GetWalletRequestModel requestWalletModel = new GetWalletRequestModel();
                mfsCommonServiceRequest mfsCommonServiceRequest = new mfsCommonServiceRequest();
                mfsSourceInfo sourceInfo = new mfsSourceInfo();
                sourceInfo.channelId = "23";
                sourceInfo.surroundSystem = "1";
                mfsCommonServiceRequest.mfsSourceInfo = sourceInfo;
                mfsTransactionInfo transactionInfo = new mfsTransactionInfo();
                transactionInfo.requestId = OYOClass.getRequestId();
                transactionInfo.serviceType = "0";
                transactionInfo.timestamp = OYOClass.getCurrentTimeStamp();
                mfsCommonServiceRequest.mfsTransactionInfo = transactionInfo;
                mfsRequestInfo requestInfo = new mfsRequestInfo();
                requestInfo.customerMsisdn = Session["user_id"].ToString().ToString();
                requestWalletModel.mfsCommonServiceRequest = mfsCommonServiceRequest;
                requestWalletModel.mfsRequestInfo = requestInfo;

                var requestWalletJson = new JavaScriptSerializer().Serialize(requestWalletModel);

                //string URL = "http://apidev.nownowpay.com.ng/mfs-transaction-management/userManagement/getUserInfo";
                string URL = "http://nownowpay.com.ng/mfs-transaction-management/userManagement/getUserInfo";
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                string request = requestWalletJson.ToString();
                var response = new JObject();
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string tken = "Bearer " + token;
                    wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Tls12
                           | SecurityProtocolType.Ssl3;
                    response = JObject.Parse(wc.UploadString(URL, request).ToString());
                }
                string walletAmount;
                if (response["mfsResponseInfo"] != null)
                {
                    walletAmount = response["mfsResponseInfo"]["mfsEntityDetailsListInfo"]["mfsEntityInfo"][0]["walletInfo"]["wallet"][0]["curBalance"].ToString();
                    lblAmount.Text = walletAmount;
                }
            }
        }
        catch (Exception err)
        {

        }
    }

    //public void HideAttribut()
    //{
    //    loader.Attributes.Add("style", "display:none");
    //}
    protected void btnok_Click(object sender, EventArgs e)
    {
        modalinfo.Attributes.Add("class", "modal fade");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        // do something
        Session.Abandon();
        Response.Redirect("../Login.aspx");
    }
}
