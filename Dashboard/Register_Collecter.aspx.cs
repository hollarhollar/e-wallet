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
using System.Configuration;
using System.Activities.Expressions;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IdentityModel.Protocols.WSTrust;

public partial class Dashboard_Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
        }

        if (Session["type_code"] == null || int.Parse(Session["type_code"].ToString()) != 100 && int.Parse(Session["type_code"].ToString()) != 104
            && int.Parse(Session["type_code"].ToString()) != 114)
        {
            Response.Redirect("../Login.aspx");
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

        if (ddlStates.Items.Count != 0)
        {
            DropDownList1.Items.Clear();
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
                DropDownList1.Items.Insert(i, new ListItem(dr["Location"].ToString(), type));
                i++;
            }
        }

        if (lgaList.Items.Count == 0)
        {
            string getLgaQuery = "";
            if (int.Parse(Session["type_code"].ToString()) == 100)
            {
                getLgaQuery = "select * from Local_Government_Areas";
            }
            else
            {
                getLgaQuery = "select * from Local_Government_Areas where lga_id = " + Session["lga"].ToString() + "";
            }

            SqlCommand qryResult = new SqlCommand(getLgaQuery, con);
            DataTable LgaDt = new DataTable();
            con.Open();
            SqlDataAdapter LgaDa = new SqlDataAdapter(qryResult);
            LgaDa.Fill(LgaDt);

            con.Close();
            i = 0;

            foreach (DataRow dr in LgaDt.Rows)
            {
                String type = dr["lga_id"].ToString();
                lgaList.Items.Insert(i, new ListItem(dr["lga"].ToString(), type));
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
        if (DropDownList666.Items.Count == 0)
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
                DropDownList666.Items.Insert(i, new ListItem(dr["description"].ToString(), type));
                i++;


            }
        }

        ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
    }

    public class Reg_Details
    {
        public int TaxPayerTypeID { get; set; }
        public int TaxPayerID { get; set; }
        public string Notes { get; set; }
        public int AssetTypeID { get; set; }
        public int AssetID { get; set; }
        public int ProfileID { get; set; }
        public int AssessmentRuleID { get; set; }
        public int TaxYear { get; set; }


    }
    public class Token
    {
        public string mfsResponseInfo { get; set; }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");
        string loginqry2 = "Select * from oyo_Registration where email=@a";
        SqlConnection con2 = new SqlConnection(OYOClass.connection);
        SqlCommand cmd2 = new SqlCommand(loginqry2, con2);
        cmd2.Parameters.AddWithValue("@a", txt_email.Text);
        DataTable dt2 = new DataTable();
        con2.Open();
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        da2.Fill(dt2);
        con2.Close();

        if (dt2.Rows.Count > 0)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Email already exist";
        }
        else
        {
            try
            {
                var txtpassword1 = "";
                if (txt_password.Value == null)
                {//defualt password
                    txtpassword1 = "Password123";
                }
                else
                {
                    txtpassword1 = txt_password.Value.ToString().Trim();
                }
                string securedPIN = CreateMD5(txt_secured_pin.Text.ToString());

                string qry = "Insert into oyo_Registration (id,mobile_no,password,first_name,last_name, gender,dob, designation,address, mapid,created_by,created_on,modified_by,modified_on, email,mpin,status,lga_id,EntityId,type_code,beat_code) values ((select isnull(max(id),0)+1 from oyo_Registration),@mobile_no,@password,@f_name,@l_name,@gender,@dob,@designation,@address,@mapid,@created_by,GETDATE(),@modified_by, getdate(), @email,@mpin,1,@lga,@entity_id,@type_code,@beat_code)";
                SqlConnection con = new SqlConnection(OYOClass.connection);
                SqlCommand cmd = new SqlCommand(qry, con);

                cmd.Parameters.AddWithValue("@mobile_no", txt_mobileno.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@password", txtpassword1);
                cmd.Parameters.AddWithValue("@f_name", txt_fname.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@l_name", txt_lname.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@gender", drp_gender.SelectedValue.ToString().Trim());
                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                cmd.Parameters.AddWithValue("@dob", DateTime.ParseExact(txt_dob.Text.ToString(), "dd/mm/yyyy", provider));
                cmd.Parameters.AddWithValue("@designation", txt_DesignationDropDown.SelectedItem.ToString().Trim());
                cmd.Parameters.AddWithValue("@address", txt_address.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@mapid", 1);
                cmd.Parameters.AddWithValue("@created_by", Session["user_id"]);
                cmd.Parameters.AddWithValue("@created_on", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@modified_by", Session["user_id"]);
                cmd.Parameters.AddWithValue("@modified_on", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@beat_code", DropDownList1.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@email", txt_email.Text);
                cmd.Parameters.AddWithValue("@mpin", securedPIN);
                cmd.Parameters.AddWithValue("@lga", lgaList.SelectedValue.ToString().Trim());
                cmd.Parameters.AddWithValue("@entity_id", "");
                cmd.Parameters.AddWithValue("@type_code", DropDownList666.SelectedValue.ToString().Trim());




                con.Open();
                int status = cmd.ExecuteNonQuery();
                con.Close();

                if (status == 1)
                {
                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = "User Created Successfully!!";
                }
                else if (status == 2)
                {
                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = "User Already Exists. Please select different username!!";
                }
            }
            catch (Exception ex)
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "Something Went Wrong!!" + ex.Message;
            }
        }
    }
    protected void Town_Changed(object sender, EventArgs e)
    {
        string loginqry = "";
        SqlConnection con = new SqlConnection(OYOClass.connection);
        int i = 0;
        string town = ddlStates.SelectedItem.ToString();
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 104 || int.Parse(Session["type_code"].ToString()) == 114)
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
            DropDownList1.Items.Insert(i, new ListItem(dr["BeatCode"].ToString(), type));
            i++;
        }
        //if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 114)
        //{
        //    string qry = "select Id, Town from BeatTown";

        //    BindDropDownList(ddlStates, qry, "Town","","");
        //    ddlStates.Enabled = true;
        //}
    }
    protected void DropDownList666_Changed(object sender, EventArgs e)
    {
        if (DropDownList666.SelectedItem.Value == "106")
        {
            txt_password.Visible = false;
            lbl_password.Visible = false;
        }
    }
   
  
    protected void lgaList_Changed(object sender, EventArgs e)
    {
        ddlStates.Items.Clear();

        string loginqry = "";
        SqlConnection con = new SqlConnection(OYOClass.connection);
        int i = 0;
        string town = lgaList.SelectedItem.ToString();
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 114 || int.Parse(Session["type_code"].ToString()) == 104)
        {
            loginqry = "Select * from BeatTown where lga =" + "'" + town + "'";
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
        //if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 114)
        //{
        //    string qry = "select Id, Town from BeatTown";

        //    BindDropDownList(ddlStates, qry, "Town","","");
        //    ddlStates.Enabled = true;
        //}
    }
    //protected void Beat_Changed(object sender, EventArgs e)
    //{
    //    if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 114)
    //    {
    //        string qry = "select Id, Location from Beat";

    //        BindDropDownList(ddlStates, qry, "Town", "Id", "Town");
    //        ddlStates.Enabled = true;
    //    }
    //}
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
    public static string CreateMD5(string input)
    {
        input = input + "Dbbwjvj$%)GE$5SGr@3VsHYUMas2323E4d57vfBfFSTRU@!DSH(*%FDSdfg13sgfsg";
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