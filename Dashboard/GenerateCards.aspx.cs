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

public partial class Dashboard_GenerateCards : System.Web.UI.Page
{
    public static int maxid=0;
    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (Session["type_code"] == null || int.Parse(Session["type_code"].ToString()) != 100)
        {
            Response.Redirect("../Login.aspx");
        }
        ((Label)this.Master.FindControl("lblPage")).Text = "Generate Scratch Cards";
        HtmlGenericControl li = new HtmlGenericControl("li");
        this.Master.FindControl("tabs").Controls.Add(li);

        HtmlGenericControl anchor = new HtmlGenericControl("a");
        anchor.Attributes.Add("href", "GenerateCarts.aspx");
        anchor.InnerText = "Generate Scratch Cards";

        li.Controls.Add(anchor);
        

        SqlConnection con = new SqlConnection(OYOClass.connection);
        
            String loginqry = "select max(id)as id from Scratch_card";

            SqlCommand cmd = new SqlCommand(loginqry, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
             DataRow drs = dt.Rows[0];
             if (!drs["id"].ToString().Equals("") && !IsPostBack)
             {
               
                maxid = int.Parse(drs["id"].ToString());
            }
            
            int i = 0;
            if (lgaList.Items.Count == 0)
            {
                String getLgaQuery = "select * from Local_Government_Areas";

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
             i = 0;
             if (denomination.Items.Count == 0)
            {
                String getLgaQuery = "select * from cardDenomination";

                SqlCommand qryResult = new SqlCommand(getLgaQuery, con);
                DataTable LgaDt = new DataTable();
                con.Open();
                SqlDataAdapter LgaDa = new SqlDataAdapter(qryResult);
                LgaDa.Fill(LgaDt);

                i = 0;
                con.Close();
                foreach (DataRow dr in LgaDt.Rows)
                {
                    String type = dr["denomination"].ToString();
                    denomination.Items.Insert(i, new ListItem(dr["denomination"].ToString(), type));
                    i++;


                }
            }


    

    }
   
    
    protected void btnNo_Click(object sender, EventArgs e)
    {
        modalinfo.Attributes.Add("class", "modal fade");
    }
    

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
       
        
        int amount = int.Parse(cardCount.Text.ToString());
        if (amount > 100000)
        {
            modalinfo.Attributes.Add("class", "modal show");
            lblmodalbody.Text = "count should not be greate then 100000";

            return;
        }

        // Create a new DataTable.    
        DataTable cardsTable = new DataTable("cards");
        DataColumn dtColumn;
        DataRow myDataRow;

        // Create id column  
        dtColumn = new DataColumn();
        dtColumn.DataType = typeof(int);
        dtColumn.ColumnName = "lgaId";
        dtColumn.Caption = "lgaId";
        dtColumn.ReadOnly = false;
        dtColumn.Unique = false;
        // Add column to the DataColumnCollection.  
        cardsTable.Columns.Add(dtColumn);

        

        dtColumn = new DataColumn();
        dtColumn.DataType = typeof(String);
        dtColumn.ColumnName = "card_no";
        dtColumn.Caption = "card_no";
        dtColumn.ReadOnly = false;
        dtColumn.Unique = false; 
        cardsTable.Columns.Add(dtColumn);

        // Create Name column.    
        dtColumn = new DataColumn();
        dtColumn.DataType = typeof(int);
        dtColumn.ColumnName = "status";
        dtColumn.Caption = "status";
        dtColumn.AutoIncrement = false;
        dtColumn.ReadOnly = false;
        dtColumn.Unique = false;
        /// Add column to the DataColumnCollection.  
        cardsTable.Columns.Add(dtColumn);

        // Create Address column.    
        dtColumn = new DataColumn();
        dtColumn.DataType = typeof(String);
        dtColumn.ColumnName = "updated_at";
        dtColumn.Caption = "updated_at";
        dtColumn.ReadOnly = false;
        dtColumn.Unique = false;
        // Add column to the DataColumnCollection.    
        cardsTable.Columns.Add(dtColumn);

        dtColumn = new DataColumn();
        dtColumn.DataType = typeof(String);
        dtColumn.ColumnName = "created_at";
        dtColumn.Caption = "created_at";
        dtColumn.ReadOnly = false;
        dtColumn.Unique = false;
        // Add column to the DataColumnCollection.    
        cardsTable.Columns.Add(dtColumn);

        dtColumn = new DataColumn();
        dtColumn.DataType = typeof(String);
        dtColumn.ColumnName = "created_by";
        dtColumn.Caption = "created_by";
        dtColumn.ReadOnly = false;
        dtColumn.Unique = false;
        // Add column to the DataColumnCollection.    
        cardsTable.Columns.Add(dtColumn);

        dtColumn = new DataColumn();
        dtColumn.DataType = typeof(int);
        dtColumn.ColumnName = "amount";
        dtColumn.Caption = "amount";
        dtColumn.ReadOnly = false;
        dtColumn.Unique = false;
        // Add column to the DataColumnCollection.    
        cardsTable.Columns.Add(dtColumn);

        // Add data rows to the custTable using NewRow method    
        // I add three customers with their addresses, names and ids   
        Random rnd = new Random();
        for (int i = 0; i < amount; i++)
        {
            myDataRow = cardsTable.NewRow();
            
            int myRandomNo = rnd.Next(10000000, 99999999);
            int myRandomNo2 = rnd.Next(10000000, 99999999);
            string cardNo = myRandomNo + "" + myRandomNo2;
            myDataRow["lgaId"] = int.Parse(lgaList.SelectedValue.ToString());
            myDataRow["status"] = 1;
            myDataRow["card_no"] = cardNo;
            myDataRow["created_at"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            myDataRow["updated_at"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            myDataRow["created_by"] = Session["user_id"];
            myDataRow["amount"] = int.Parse(denomination.SelectedValue.ToString());
            cardsTable.Rows.Add(myDataRow); 
        }
       



       
        SqlConnection con = new SqlConnection(OYOClass.connection);
        con.Open();
        //creating object of SqlBulkCopy  
        SqlBulkCopy objbulk = new SqlBulkCopy(con);
        //assigning Destination table name  
        objbulk.DestinationTableName = "Scratch_card";
        //Mapping Table column  
        objbulk.ColumnMappings.Add("lgaId", "lgaId");
        objbulk.ColumnMappings.Add("status", "status");
        objbulk.ColumnMappings.Add("card_no", "card_no");
        objbulk.ColumnMappings.Add("created_at", "created_at");
        objbulk.ColumnMappings.Add("updated_at", "updated_at");
        objbulk.ColumnMappings.Add("created_by", "created_by");
        objbulk.ColumnMappings.Add("amount", "amount");
        //inserting bulk Records into DataBase   
        objbulk.WriteToServer(cardsTable);
        con.Close();

      
        String getLgaQuery = "select * from Scratch_card where id > " + maxid;

        SqlCommand qryResult = new SqlCommand(getLgaQuery, con);
        DataTable dt = new DataTable();
        con.Open();
        SqlDataAdapter LgaDa = new SqlDataAdapter(qryResult);
        LgaDa.Fill(dt);
        con.Close();
        string fileName = hdnConfirm.Value.ToString()+".xlsx";
        OYOClass.ExportToExcel(dt, Server.MapPath("cards/" + fileName));
        modalinfo.Attributes.Add("class", "modal show");

        String loginqry = "select max(id)as id from Scratch_card";


        SqlCommand cmd = new SqlCommand(loginqry, con);
        DataTable dts = new DataTable();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dts);
        con.Close();
        DataRow drs = dts.Rows[0];
        if (!drs["id"].ToString().Equals(""))
        {

            maxid = int.Parse(drs["id"].ToString());
        }

        modalinfo.Attributes.Add("class", "modal show");
        lblmodalbody.Text = "Scratch Cards Created Successfully do you want to download the created batch??";

        
    }

    
}