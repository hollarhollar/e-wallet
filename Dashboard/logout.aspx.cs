using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (btn.ID == "btnLogout")
        {
            // Logout code
            Session.Abandon();
            Response.Redirect("../Login.aspx");
        }
        else
        {
            // Other button click code
            Response.Redirect("../Login.aspx");
        }
    }

}