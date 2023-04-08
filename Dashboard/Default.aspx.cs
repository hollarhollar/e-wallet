using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Dashboard_Default : System.Web.UI.Page
{
    public static int OrdedoValue = 0;
    public static int IkpobaValue = 0;
    public static DataTable stream;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["type_code"] != null && Session["lga"] != null)
        {
            if (int.Parse(Session["type_code"].ToString()) != 100 && int.Parse(Session["type_code"].ToString()) != 104)
            {
                lblAmountCollected.Text = OYOClass.elipses(OYOClass.fetchdata("SELECT format(sum(amount), 'C', 'ng-ng') as amount FROM tbl_payer_trans where lgaId ='" + Session["lga"] + "'").Rows[0]["amount"].ToString(), 13);
                lblLgaCollectors.Text = OYOClass.elipses(OYOClass.fetchdata("SELECT count(o.id) as count FROM oyo_Registration o left join tbl_Payer_Trans p on o.id = p.Id where type_code=106 and lga_id ='" + Session["lga"] + "'").Rows[0]["count"].ToString(), 13);
                if (Session["lga"].ToString() == "7")
                {
                    string val = OYOClass.fetchdata("SELECT sum(amount) as amount FROM tbl_payer_trans where lgaId ='" + Session["lga"] + "'").Rows[0]["amount"].ToString();
                    if (val == "")
                    {
                        val = "0";
                    }
                    OrdedoValue = int.Parse(val.Split(new string[] { "." }, StringSplitOptions.None)[0]);
                    IkpobaValue = 0;
                }
                else
                {
                    string val = OYOClass.fetchdata("SELECT sum(amount) as amount FROM tbl_payer_trans where lgaId ='" + Session["lga"] + "'").Rows[0]["amount"].ToString();
                    if (val == "")
                    {
                        val = "0";
                    }
                    IkpobaValue = int.Parse(val.Split(new string[] { "." }, StringSplitOptions.None)[0]);
                    OrdedoValue = 0;
                }

            }
            else
            {
                lblAmountCollected.Text = OYOClass.elipses(OYOClass.fetchdata("SELECT format(sum(amount), 'C', 'ng-ng') as amount FROM tbl_payer_trans where lgaId ='" + Session["lga"] + "'").Rows[0]["amount"].ToString(), 13);
                lblLgaCollectors.Text = OYOClass.elipses(OYOClass.fetchdata("SELECT count(o.id) as count FROM oyo_Registration o left join tbl_Payer_Trans p on o.id = p.Id where lgaId ='" + Session["lga"] + "'").Rows[0]["count"].ToString(), 13);
                string val = OYOClass.fetchdata("SELECT sum(amount) as amount FROM tbl_payer_trans where lgaId ='" + Session["lga"] + "'").Rows[0]["amount"].ToString().ToString();
                if (val == "")
                {
                    val = "0";
                }
                OrdedoValue = int.Parse(val.Split(new string[] { "." }, StringSplitOptions.None)[0]);
                val = OYOClass.fetchdata("SELECT sum(amount) as amount FROM tbl_payer_trans where lgaId ='" + Session["lga"] + "'").Rows[0]["amount"].ToString().ToString();
                if (val == "")
                {
                    val = "0";
                }
                IkpobaValue = int.Parse(val.Split(new string[] { "." }, StringSplitOptions.None)[0]);
            }
            lblTaxPayers.Text = OYOClass.elipses(OYOClass.fetchdata("SELECT count(Pay_Id) as TaxPayers FROM TaxPayers t left join tbl_Payer_Trans p on t.Pay_Id = p.Id where lgaId ='" + Session["lga"] + "'").Rows[0]["TaxPayers"].ToString(), 13);
            lblLGA.Text = "1";

            // string res = "";
            //res = OYOClass.fetchdata("SELECT SUM(AmountPaid) AS TotalPayment, SUM(Amount) AS TotalTransAmount, CASE WHEN SUM(AmountPaid) < (0.8 * SUM(Amount)) THEN 'true' ELSE NULL END AS BalanceStatus FROM PaymentRecord r LEFT JOIN tbl_Payer_Trans p ON r.PaymentRecordId = p.Id WHERE CollectorId = '9818895905' and lgaId ='" + Session["lga"] + "'").ToString();

          
            stream = OYOClass.fetchdata("select format(sum(tbl_Payer_Trans.amount), 'C','ng-ng') as Amount, Categories.category from Categories left join tbl_Payer_Trans on cast(Categories.id as varchar(50)) = tbl_Payer_Trans.categoryId where lgaId ='" + Session["lga"] + "' group by Categories.category");
            //((Label)this.Master.FindControl("lblPage")).Text = "Dashboard";
            HtmlGenericControl li = new HtmlGenericControl("li");
            // this.Master.FindControl("tabs").Controls.Add(li);

            HtmlGenericControl anchor = new HtmlGenericControl("a");
            //anchor.Attributes.Add("href", "Defaile.aspx");
            anchor.Attributes.Add("href", "Default.aspx");
            anchor.InnerText = "Dashboard";

            li.Controls.Add(anchor);
        }
    }

}