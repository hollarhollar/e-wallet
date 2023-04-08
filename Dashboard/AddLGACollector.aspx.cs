using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_AddLGACollector : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
        Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

        SqlParameter[] pram = new SqlParameter[7];
        pram[0] = new SqlParameter("@user_name", txtusername.Text.ToString().Trim());
        pram[1] = new SqlParameter("@password", txtpassword.Text.ToString().Trim());
        pram[2] = new SqlParameter("@designation", txtdesignation.Text.ToString().Trim());
        pram[3] = new SqlParameter("@mobileno", txtmobileno.Text.ToString().Trim());
        pram[4] = new SqlParameter("@address", txtaddress.Text.ToString().Trim());
        pram[5] = new SqlParameter("@createdby", Session["user_id"].ToString().Trim());
        pram[6] = new SqlParameter("@successId", "1");
        pram[6].Direction = System.Data.ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(OYOClass.connection, CommandType.StoredProcedure, "ADD_USER", pram);
        int status = int.Parse(pram[6].Value.ToString());
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
        else
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "Error in creating user. Please contact system administrator.";
        }
    }
}