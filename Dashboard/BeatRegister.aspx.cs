using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_BeatRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["type_code"] == null || int.Parse(Session["type_code"].ToString()) != 100 && int.Parse(Session["type_code"].ToString()) != 104
            && int.Parse(Session["type_code"].ToString()) != 114)
        {
            Response.Redirect("../Login.aspx");
        }
        var sessionValue = Session["type_code"].ToString();
        if (sessionValue != "100")
        {
            var lgaName = Session["lgaName"].ToString();
            SqlConnection con = new SqlConnection(OYOClass.connection);
            int i = 0;
            if (!IsPostBack)
            {
                string query = "select distinct (select top 1 id from BeatTown t2 where t1.Lga= t2.Lga) as id, Lga from BeatTown t1 where Lga =" + "'" + lgaName + "'";
                BindDropDownList(ddlCountries, query, "Lga", "Id", "Select LGA");
                ddlStates.Enabled = false;
                //ddlCities.Enabled = false;
                ddlStates.Items.Insert(0, new ListItem("Select Town", "0"));
            }
        }
        else
        {
            int i = 0;
            if (!IsPostBack)
            {
                string query = "select distinct (select top 1 id from BeatTown t2 where t1.Lga= t2.Lga) as id, Lga from BeatTown t1";
                BindDropDownList(ddlCountries, query, "Lga", "Id", "Select LGA");
                ddlStates.Enabled = false;
                //ddlCities.Enabled = false;
                ddlStates.Items.Insert(0, new ListItem("Select Town", "0"));
            }
        }
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


    protected void Country_Changed(object sender, EventArgs e)
    {
        ddlStates.Enabled = false;
        //  ddlCities.Enabled = false;
        ddlStates.Items.Clear();
        // ddlCities.Items.Clear();
        ddlStates.Items.Insert(0, new ListItem("Select Town", "0"));
        //  ddlCities.Items.Insert(0, new ListItem("Select City", "0"));
        var countryId = ddlCountries.SelectedItem.Text;

        if (countryId != null)
        {
            string qry = "select Id, Town from BeatTown where Lga =" + " '" + countryId.ToString() + "' ";

            BindDropDownList(ddlStates, qry, "Town", "Id", "Select Town");
            ddlStates.Enabled = true;
        }
    }
    protected void Town_Changed(object sender, EventArgs e)
    {
        TextBoxPostalCode1.Enabled = true;
        var countryId = ddlStates.SelectedItem.Text;
        if (countryId != null)
        {
            string qry = "select Id, PostCode from BeatTown where Town =" + " '" + countryId.ToString() + "' ";

            BindDropDownList(TextBoxPostalCode1, qry, "PostCode", "Id", "Post Code");
            ddlStates.Enabled = true;
        }
    }
    public class Token
    {
        public string mfsResponseInfo { get; set; }
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");
        Label lblmodalfooter = (Label)this.Master.FindControl("lblmodalfooter");

        var bc = CreateBeatCode(ddlStates.SelectedItem.ToString().Trim());
        try
        {
            string qry = "Insert into Beat (BeatCode,State,Town,PostalCode,Location,CreatedBy) values (@BeatCode,@State,@Town,@PostalCode,@Location,@created_by)";
            SqlConnection con = new SqlConnection(OYOClass.connection);
            SqlCommand cmd = new SqlCommand(qry, con);
        
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
           
            cmd.Parameters.AddWithValue("@State", "Edo State");
            cmd.Parameters.AddWithValue("@BeatCode", bc);
            cmd.Parameters.AddWithValue("@Town", ddlStates.SelectedItem.ToString().Trim());
            cmd.Parameters.AddWithValue("@PostalCode", TextBoxPostalCode1.SelectedItem.ToString().Trim());
            cmd.Parameters.AddWithValue("@created_by", Session["user_id"]);
            cmd.Parameters.AddWithValue("@Location", TextBox1.Text.ToString().Trim());
            cmd.Parameters.AddWithValue("@lga", ddlCountries.SelectedItem.ToString().Trim());
      

            con.Open();
            int status = cmd.ExecuteNonQuery();
            con.Close();

            if (status == 1)
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "Beat Created Successfully, Your Beat Code is " + bc + "";
                
            }
            else if (status == 2)
            {
                modalinfo.Attributes.Add("class", "modal show");
                lblmodalbody.Text = "Beat Already Exists. Please select different username!!";
            }
        }

        catch (Exception ex)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Something Went Wrong!!" + ex.Message;
        }
    }

    public static string CreateBeatCode(string town)
    {
        Guid guid = Guid.NewGuid();
        var txt = guid.ToString();
       txt =  txt.Substring(0, 5);
        DateTime dateTime = DateTime.Now;
        var dta = dateTime.ToString();
        dta= dta.Substring(0, 5);


        return txt+town+dta;
    }
}
