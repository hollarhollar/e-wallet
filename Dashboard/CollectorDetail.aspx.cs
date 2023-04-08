using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_CollectorDetail : System.Web.UI.Page
{


    SqlConnection con = new SqlConnection(OYOClass.connection);

    DataTable dt_detail = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable dt01 = new DataTable();
    DataTable dt4 = new DataTable();
    DataTable dt5 = new DataTable();
    DataTable dt6 = new DataTable();
    DataTable dTable = new DataTable();
    public object lga_id;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["lga"] == null)
            {
                Response.Redirect("../Login.aspx");

            }
            TheMethod(Session["PassedId"].ToString());

        }

    }
    protected void TheMethod(string PassedId)
    {
        string query = "";
        if (int.Parse(Session["type_code"].ToString()) == 100 || int.Parse(Session["type_code"].ToString()) == 104)
        {
            query = "select distinct o.id, o.first_name, o.last_name, o.mobile_no Phone, g.lga, o.address, b.Location Revenuebeat from oyo_Registration o left join tbl_Payer_Trans p on CONVERT(varchar(50), o.id) = p.loginUserId left join Local_Government_Areas g on o.lga_id = g.lga_id left join beat b on o.beat_code = CONVERT(varchar(50), b.id) where type_code = 106 and o.id = '" + PassedId + "'";
        }
        else
        {
            query = "select distinct o.id, o.first_name, o.last_name, o.mobile_no Phone, g.lga, o.address, b.Location Revenuebeat from oyo_Registration o left join tbl_Payer_Trans p on CONVERT(varchar(50), o.id) = p.loginUserId left join Local_Government_Areas g on o.lga_id = g.lga_id left join beat b on o.beat_code = CONVERT(varchar(50), b.id) where type_code = 106 and lga_id = " + Session["lga"].ToString() + " and o.id = '" + PassedId + "'";
        }

        SqlCommand cmd_lga = new SqlCommand(query, con);
        DataTable grdtable = new DataTable();
        con.Open();
        SqlDataAdapter dtTab = new SqlDataAdapter(cmd_lga);
        grdtable.Clear();
        dtTab.Fill(grdtable);
        con.Close();
        Session["dataTableCollector"] = grdtable;
        var dataTableCollector = (DataTable)Session["dataTableCollector"];
        rptCollector.DataSource = dataTableCollector;
        rptCollector.DataBind();



    }

    //protected void grd_paginHandler(object sender, GridViewPageEventArgs e)
    //{
    //    rptCollector.PageIndex = e.NewPageIndex;
    //    rptCollector.DataSource = Session["dataTableCollector"];
    //    rptCollector.DataBind();
    //}

    //protected void rptCollector_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        // Get the data item for the current row
    //        CollectorData collectorData = (CollectorData)e.Item.DataItem;

    //        // Find the table row control
    //        HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trCollector");

    //        // Populate the table row with the data
    //        tr.Cells[0].InnerText = collectorData.ID;
    //        tr.Cells[1].InnerText = collectorData.FirstName;
    //        tr.Cells[2].InnerText = collectorData.LastName;
    //        tr.Cells[3].InnerText = collectorData.SubPhoneNumber;
    //        tr.Cells[4].InnerText = collectorData.LGA;
    //        tr.Cells[5].InnerText = collectorData.Address;
    //        tr.Cells[6].InnerText = collectorData.RevenueBeat;
    //    }
    //}

    protected void rptCollector_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            // header template
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            // item template
            DataRowView rowView = e.Item.DataItem as DataRowView;
            if (rowView != null)
            {
                Label lblId = e.Item.FindControl("lblId") as Label;
                if (lblId != null)
                {
                    lblId.Text = rowView["id"].ToString();
                }

                Label lblFirstName = e.Item.FindControl("lblFirstName") as Label;
                if (lblFirstName != null)
                {
                    lblFirstName.Text = rowView["first_name"].ToString();
                }

                Label lblLastName = e.Item.FindControl("lblLastName") as Label;
                if (lblLastName != null)
                {
                    lblLastName.Text = rowView["last_name"].ToString();
                }

                Label lblPhone = e.Item.FindControl("lblPhone") as Label;
                if (lblPhone != null)
                {
                    lblPhone.Text = rowView["Phone"].ToString();
                }

                Label lblLga = e.Item.FindControl("lblLga") as Label;
            }
        }
    }
}