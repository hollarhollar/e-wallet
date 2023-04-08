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
using System.IO;
using System.Threading.Tasks;
using System.Globalization;

public partial class Dashboard_VerifyReceipt : System.Web.UI.Page
{
    public static int maxid=0;
    SqlConnection con = new SqlConnection(OYOClass.connection);
    protected void Page_Load(object sender, EventArgs e)
    {
        var receiptnumber = receiptNumber.Text;
        if (receiptnumber != "")
        {
            if (IsPostBack)
            {
                Response.Redirect("~/Dashboard/VerifyReceipt.aspx");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        string receiptNum = receiptNumber.Text.ToString();
        // String getLgaQuery = "select *  from Scratch_card where id > " + receiptNum;
        
        string query = " Select *  from  tbl_Payer_Trans where TransId like \'%" + receiptNumber.Text + "%\' or Id like \'%" + receiptNumber.Text +"\'";

        SqlCommand cmd = new SqlCommand(query, con);
        DataTable dt = new DataTable();
        dt.Clear();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            //string qry = "Select tbl_Payer_Trans.MobNo, category, tbl_Payer_Trans.Amount,  tbl_Payer_Trans.created_at From tbl_Payer_Trans join Categories ON tbl_Payer_Trans.categoryId = Categories.id where tbl_Payer_Trans.TransId like '%" + receiptNumber.Text + "%' or tbl_Payer_Trans.Id like '%" + receiptNumber.Text + "%' ";
            string qy = "Select tbl_Payer_Trans.MobNo, tbl_Payer_Trans.created_at, tbl_Payer_Trans.Amount,  tbl_Payer_Trans.AssetId, Categories.category, Categories.sub_category From tbl_Payer_Trans join Categories ON tbl_Payer_Trans.categoryId = Categories.id where tbl_Payer_Trans.TransId like '%" + receiptNumber.Text + "%' or tbl_Payer_Trans.Id like '%" + receiptNumber.Text + "%' ";
            SqlCommand ccmd = new SqlCommand(qy, con);
            DataTable dtt = new DataTable();
            dtt.Clear();
            con.Open();
            SqlDataAdapter daa = new SqlDataAdapter(ccmd);
            daa.Fill(dtt);
            con.Close();

            foreach (DataRow dr in dtt.Rows)
            {
                txt_phoneNumber.Text = dr["MobNo"].ToString();
                txt_sector.Text = dr["category"].ToString();
                txt_subsector.Text = dr["sub_category"].ToString();
                txt_amount.Text = dr["Amount"].ToString();
                txt_date.Text = dr["created_at"].ToString();
                txt_vehicleNo.Text = dr["AssetId"].ToString();
            }
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Receipt contact Issuer');", true);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!', 'You clicked the button!', 'success')", true);
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Invalid Receipt contact Issuer";
            return;
        }

    }
    
}