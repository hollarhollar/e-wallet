using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_RevenueStream : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)this.Master.FindControl("lblPage")).Text = "Revenue Stream 123";
        HtmlGenericControl li = new HtmlGenericControl("li");
        this.Master.FindControl("tabs").Controls.Add(li);

        HtmlGenericControl anchor = new HtmlGenericControl("a");
        anchor.Attributes.Add("href", "RevenueStream.aspx");
        anchor.InnerText = "Revenue Stream";

        li.Controls.Add(anchor);
    }
}