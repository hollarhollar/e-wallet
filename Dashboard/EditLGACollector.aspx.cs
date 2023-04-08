using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net;
using DocumentFormat.OpenXml.Bibliography;


public partial class Dashboard_EditLGACollector : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["type_code"] == null ||( int.Parse(Session["type_code"].ToString()) != 100 &&
            int.Parse(Session["type_code"].ToString()) != 104))
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            String getLgaQuery = "";
            if (int.Parse(Session["type_code"].ToString()) == 100)
            {
                getLgaQuery = "select * from Local_Government_Areas";
            }
            else
            {
                getLgaQuery = "select * from Local_Government_Areas Where lga_id = " + Session["lga"].ToString();
            }
            binddrop();
           // BindDropDownList(lgaList, getLgaQuery, "Lga", "Id", "Select LGA");
        }
        
        SqlConnection con = new SqlConnection(OYOClass.connection);
       
        int i = 0;
        if (ddlStates.Items.Count == 0)
        {
            //var countryId = ddlStates.Items[0].ToString();
            String loginqry = "";
            if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 114)
            {
                // string qry = "select Id, Town from BeatTown where Lga =" + " '" + countryId.ToString() + "' ";
                loginqry = " select distinct (select top 1 id from Beat t2 where t1.town= t2.town) as id, Town from [Beat] t1";
            }

            SqlCommand cmd = new SqlCommand(loginqry, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();

            i = 0;
            foreach (DataRow dr in dt.Rows)
            {

                String type = dr["Id"].ToString();
                ddlStates.Items.Insert(i, new ListItem(dr["Town"].ToString(), type));
                i++;
            }
        }

        if (txt_DesignationDropDown.Items.Count == 0)
        {
            string DesignationDropDown = "";
            if (int.Parse(Session["type_code"].ToString()) == 104)
            {
                DesignationDropDown = "select distinct [type_code],[description] from [oyo_user_types] where [Priveledge] = 104";
            }
            else
            {
                DesignationDropDown = "select distinct [type_code],[description] from [oyo_user_types]";
            }

            SqlCommand qryResult = new SqlCommand(DesignationDropDown, con);
            DataTable LgaDt = new DataTable();
            con.Open();
            SqlDataAdapter LgaDa = new SqlDataAdapter(qryResult);
            LgaDa.Fill(LgaDt);

            con.Close();
            i = 0;

            foreach (DataRow dr in LgaDt.Rows)
            {
                String type = dr["type_code"].ToString();
                txt_DesignationDropDown.Items.Insert(i, new ListItem(dr["description"].ToString(), type));
                i++;
            }
        }

        if (ddlStates.Items.Count != 0)
        {
            beatCode.Items.Clear();
            String loginqry = "";
            string town = ddlStates.SelectedItem.ToString();
            if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 114 || int.Parse(Session["type_code"].ToString()) == 104)
            {
                loginqry = "Select * from Beat where town =" + "'" + town + "'";
            }

            SqlCommand cmd = new SqlCommand(loginqry, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();

            i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                String type = dr["Id"].ToString();
                beatCode.Items.Insert(i, new ListItem(dr["Location"].ToString(), type));
                i++;
            }
        }

        //if (Accesstype.Items.Count == 0)
        //{
        //    string loginqry = "";
        //    if (int.Parse(Session["type_code"].ToString()) == 104)
        //    {
        //        loginqry = "select distinct [type_code],[description] from [oyo_user_types] where [Priveledge] = 104 ";
        //    }
        //    else
        //    {
        //        loginqry = "select distinct [type_code],[description] from [oyo_user_types]";
        //    }
               
        //    SqlCommand cmd = new SqlCommand(loginqry, con);
        //    DataTable dt = new DataTable();
        //    con.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(dt);
        //    con.Close();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        String type = dr["type_code"].ToString();
        //        Accesstype.Items.Insert(i, new ListItem(dr["description"].ToString(), type));
        //       // Accesstype.Items.Insert(i, new ListItem(dr["description"].ToString(), type));
        //        i++;
        //    }
        //}
        if (lgaList.Items.Count == 0)
        {
            String getLgaQuery = "";
            if (int.Parse(Session["type_code"].ToString()) == 100)
            {
                getLgaQuery = "select * from Local_Government_Areas";
            }
            else
            {
                getLgaQuery = "select * from Local_Government_Areas Where lga_id = " + Session["lga"].ToString();
            }
             

            SqlCommand qryResult = new SqlCommand(getLgaQuery, con);
            DataTable LgaDt = new DataTable();
            con.Open();
            SqlDataAdapter LgaDa = new SqlDataAdapter(qryResult);
            LgaDa.Fill(LgaDt);

            i = 0;

            foreach (DataRow dr in LgaDt.Rows)
            {
                String type = dr["lga_id"].ToString();
                lgaList.Items.Insert(i, new ListItem(dr["lga"].ToString(), type));
                i++;
            }
        }
    }
    protected void Country_Changed(object sender, EventArgs e)
    {
        ddlStates.Enabled = false;
        //  ddlCities.Enabled = false;
        ddlStates.Items.Clear();
        // ddlCities.Items.Clear();
        ddlStates.Items.Insert(0, new ListItem("Select Town", "0"));
        //  ddlCities.Items.Insert(0, new ListItem("Select City", "0"));
        var countryId = lgaList.SelectedItem.Text;

        if (countryId != null)
        {
            string qry = "select Id, Town from BeatTown where Lga =" + " '" + countryId.ToString() + "' ";

            BindDropDownList(ddlStates, qry, "Town", "Id", "Select Town");
            ddlStates.Enabled = true;
        }
    }
    protected void Town_Changed(object sender, EventArgs e)
    {
        beatCode.Enabled = true;
        var countryId = ddlStates.SelectedItem.Text;
        if (countryId != null)
        {
            //Select * from Beat where town =
            string qry = "select Id, Location from Beat where Town =" + " '" + countryId.ToString() + "' ";

            BindDropDownList(beatCode, qry, "Location", "Id", "Location");
            ddlStates.Enabled = true;
        }
    }
    private void BindDropDownList(DropDownList ddl, string query, string text, string value, string defaultText)
    {
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                con.Open();
                ddl.DataSource = cmd.ExecuteReader();
                ddl.DataTextField = text;
                ddl.DataValueField = value;
                ddl.DataBind();
                con.Close();
            }
        }
        ddl.Items.Insert(0, new ListItem(defaultText, "0"));
    }

    public void binddrop()
    {
        string qry = "";
        if (int.Parse(Session["type_code"].ToString()) == 100)
        {
            qry = "Select first_name+' '+ last_name as user_name,id as user_id from oyo_Registration where mobile_no <> '" + Session["user_id"] + "'";
        }
        else
        {
            qry = "Select first_name+' '+ last_name as user_name,id as user_id from oyo_Registration where mobile_no <> '" + Session["user_id"]+ "' and lga_id=" + Session["lga"].ToString();
        }
        
        DataTable dt = new DataTable();
        dt = OYOClass.fetchdata(qry);
        if (dt.Rows.Count > 0)
        {
            drpusername.DataSource = dt;
            drpusername.DataTextField = "user_name";
            drpusername.DataValueField = "user_id";
            drpusername.DataBind();
            drpusername.Items.Insert(0, "---Select---");
        }

    }
    protected void drpusername_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpusername.SelectedIndex != 0)
        {
            string qry = "Select * from oyo_Registration where id='" + drpusername.SelectedValue.ToString().Trim() + "'";
            DataTable dtuser = new DataTable();
            dtuser = OYOClass.fetchdata(qry);
            txt_DesignationDropDown.ClearSelection();
            lgaList.ClearSelection();
            beatCode.ClearSelection();
            if (dtuser.Rows.Count > 0)
            {
                var dbo = dtuser.Rows[0]["dob"].ToString();
                var kkg = dbo.Substring(0, dbo.Length - 12);
                txt_fname.Text = dtuser.Rows[0]["first_name"].ToString();
                txt_lname.Text = dtuser.Rows[0]["last_name"].ToString();
                txt_entityid.Text = dtuser.Rows[0]["EntityId"].ToString();
                txt_mobileno.Text = dtuser.Rows[0]["mobile_no"].ToString();
                drp_gender.SelectedValue = dtuser.Rows[0]["gender"].ToString();
                txt_DesignationDropDown.Items.FindByValue(dtuser.Rows[0]["type_code"].ToString()).Selected = true;
                lgaList.Items.FindByValue(dtuser.Rows[0]["lga_id"].ToString()).Selected = true;
                txt_dob.Text = kkg;
                //= dtuser.Rows[0]["designation"].ToString();              
                txt_address.Text = dtuser.Rows[0]["address"].ToString();
                txt_email.Text = dtuser.Rows[0]["email"].ToString();
                //beatCode.Items.FindByValue(dtuser.Rows[0]["beat_code"].ToString()).Selected=true;
              //  txt_secured_pin.Text = (dtuser.Rows[0]["mpin"].ToString());
                
                //string mpin = decrypt(dtuser.Rows[0]["mpin"].ToString());
            }
            else
            {
                //txt_designation.Text = "";
                txt_address.Text = "";
                txt_mobileno.Text = "";
            }
        }
        else
        {
            //txt_designation.Text = "";
            txt_address.Text = "";
            txt_mobileno.Text = "";
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");


        string securedPIN = CreateMD5(txt_secured_pin.Text.ToString());

        System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
        string qry = "Update oyo_Registration set lga_id='" + lgaList.SelectedValue.ToString().Trim() + "', " +
            "beat_code='" + beatCode.Text.ToString() + "'," + "dob='" + DateTime.ParseExact(txt_dob.Text.ToString(), "dd/mm/yyyy", provider) + "',modified_by='" + Session["user_id"] + "', address='" + txt_address.Text.Trim() + "',email='" + txt_email.Text.Trim() + "',mpin='" + securedPIN + "',type_code='" + txt_DesignationDropDown.SelectedValue.ToString().Trim() + "' where id='" + drpusername.SelectedValue.ToString().Trim() + "'";
        int status = OYOClass.insertupdateordelete(qry);
        if (status > 0)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "User updated Successfully!! (" + status + ")";
        }
        else
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Error in updating user.Please contact system administrator!! (" + status + ")";
        }
    }


    public static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}