using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_PaymentDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_id"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (int.Parse(Session["type_code"].ToString()) != 100 && int.Parse(Session["type_code"].ToString()) != 104
            && int.Parse(Session["type_code"].ToString()) != 115)
        {
            Response.Redirect("../Login.aspx");
        }
        if (Request.QueryString["mobile"] != null)
        {
            try
            {
                HtmlContainerControl modalinfo = (HtmlContainerControl)this.Master.FindControl("modalinfo");
                Label lblmodalbody = (Label)this.Master.FindControl("lblmodalbody");

                SqlConnection con = new SqlConnection(OYOClass.connection);
                string mobile = Request.QueryString["mobile"].ToString().Trim();
                SqlCommand cmd_lga = new SqlCommand();

                //divresult.Style.Add("display", "");

                grd_list_Collectors.Visible = true;
                DataTable dt1 = new DataTable();
                con.Open();
                SqlDataAdapter da_lga = new SqlDataAdapter(cmd_lga);
                
                


                string loginqry = "";
                if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 115 || int.Parse(Session["type_code"].ToString()) == 103 || int.Parse(Session["type_code"].ToString()) == 104)
                {
                    loginqry = "SELECT * FROM PaymentRecord P LEFT JOIN tbl_Payer_Trans T on P.FundedTransactionId = T.Id where P.ReceiptNumber = \'" + mobile + "' order by p.PaymentDate desc";
                }

                con = new SqlConnection(OYOClass.connection);
                cmd_lga = new SqlCommand(loginqry, con);

                dt1 = new DataTable();
                SqlDataAdapter Adp = new SqlDataAdapter(loginqry, con);
                dt1.Columns.Add(new DataColumn() { ColumnName = "diffAmount", DataType = typeof(string) });
                Adp.Fill(dt1);
                
                if (dt1.Rows.Count == 0)
                {
                    modalinfo.Attributes.Add("class", "modal show");
                    lblmodalbody.Text = "No record Found";
                    return;
                }
                foreach (DataRow dr in dt1.Rows)
                {
                    double diffAmount = 0.0;
                    diffAmount = Convert.ToDouble(dr["FundedAmount"])-Convert.ToDouble(dr["AmountPaid"]);

                    dr.SetField("diffAmount", diffAmount);

                }
               
                //grd_list_Collectors.Visible = true;
                Session["dttxns"] = dt1;
                grd_list_Collectors.DataSource = dt1;
                grd_list_Collectors.DataBind();
                
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void grd_list_Collectors_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_list_Collectors.PageIndex = e.NewPageIndex;
        grd_list_Collectors.DataSource = (DataTable)Session["dttxns"];
        grd_list_Collectors.DataBind();
    }
}