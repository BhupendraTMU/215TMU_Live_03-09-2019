using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

using System.IO;

public partial class Faculty_Teststudentattandence : System.Web.UI.Page
{
    TMUConnection con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    static string FacultyCode = ""; static string CollegeCode = ""; DataTable dts = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "ADMIN" || Session["uid"].ToString() == "TMU01005")
            {
                try
                {

                    CollegeCode = Session["GlobalDimension1Code"].ToString();
                    if (!IsPostBack)
                    {
                        Session["Studentdata"] = null;                       //   BindGrid();
                        BinddlEventType();
                        BindGrid();
                    }

                }

                catch (Exception ex)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        catch
        { Response.Redirect("~/Default.aspx"); }



    }

        public void BinddlEventType()
    {
        SqlCommand cmd = new SqlCommand("Sp_getEventattedance", con1); //ok
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlEventType.DataSource = dt;
        ddlEventType.DataTextField = "Details";
        ddlEventType.DataValueField = "Value";
        ddlEventType.DataBind();


    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                //  cmd.CommandText = "select [Student Name],No_ from [TMU$Student - COLLEGE] where  Upper([Student Name]) like '" + prefixText.ToUpper() + "%'" + @"
                //                and [Course Code] in (select distinct([Course Code]) from [TMU$Course Wise Faculty] where [Global Dimension 1 Code]=
                //(select [Global Dimension 1 Code] from [TMU$Employee] where [No_]='" + FacultyCode + "'))"; 
                cmd.CommandText = "select [Student Name],[Enrollment No_] from [TMU$Student - COLLEGE] where  Upper([Student Name]) like '" + prefixText.ToUpper() + "%'" + @"
                    and [Course Code] in (select distinct([Course Code]) from [TMU$Course Wise Faculty] where [Global Dimension 1 Code]='" + CollegeCode + "') and [Student Status]=1";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        // customers.Add(sdr["Student Name"].ToString() + " (" + sdr["Enrollment No_"].ToString() + ")");
                        customers.Add(sdr["Student Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    protected DataTable DeleteDuplicateFromDataTable(DataTable dtDuplicate, string columnName)

    {
        Hashtable hashT = new Hashtable();

        ArrayList arrDuplicate = new ArrayList();

        foreach (DataRow row in dtDuplicate.Rows)

        {

            if (hashT.Contains(row[columnName]))

                arrDuplicate.Add(row);

            else

                hashT.Add(row[columnName], string.Empty);

        }

        foreach (DataRow row in arrDuplicate)

            dtDuplicate.Rows.Remove(row);

 

        return dtDuplicate;

    }








     private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            string str = TextBox1.Text;
            string str1 = str.Insert(0, "'");
            int l = str1.Length;
            string str2 = str1.Insert(l, "'");
            string strFinal = str2.Replace(",", "','");

            using (SqlCommand cmd = new SqlCommand("select  * from [TMU$Student - COLLEGE] where [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and [No_] in(" + strFinal + ") ", con)) 
            //new SqlCommand("Get_StudentEditeAttandance", con))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
               // cmd.Parameters.AddWithValue("@collegecode", Session["GlobalDimension1Code"].ToString());
               // cmd.Parameters.AddWithValue("@studentno", TextBox1.Text);                
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();



                   // sda.Fill(dt);
                    
                    
                    if (Session["Studentdata"] == null)
                    {
                        sda.Fill(dt);
                        Session["Studentdata"] = dt;
                        dts = (DataTable)(Session["Studentdata"]);
                    }
                    else
                    {
                        
                        dts = (DataTable)(Session["Studentdata"]);
                        sda.Fill(dts);
                        Session["Studentdata"] = dts;
                    }
                  // sda.Fill(dts);
                   dts = DeleteDuplicateFromDataTable(dts, "No_");

                    GridView1.DataSource = dts;
                    GridView1.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        Btnupdate.Visible = true;
                    }
                    else
                        Btnupdate.Visible = false;
                }
            }
        }

    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        hfStudentId.Value += "','"+hfStudentId.Value;  //by sir

        this.BindGrid();
    }
    protected void Btnupdate_Click(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkboxSelectAll");
        string DateCommited = txtDateCommited.Text;
        string dateform = txtDateFrom.Text;
        string Dateto = txtDateTo.Text;
        string remarks = txtRemarks.Text;
        string ddlevents = ddlEventType.SelectedValue;
        string cordinat = Session["uid"].ToString();
        string Lactures = ddlLecture.SelectedValue;
       // string chkManyDays = "";

        if (chkManyDays.Checked == true)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                // Only look in data rows, ignore header and footer rows
                if (row.RowType == DataControlRowType.DataRow)
                {
                     CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");

                     if (ChkBoxHeader.Checked == true)
                     {
                        // ChkBoxRows.Checked = true;
                         var id = GridView1.DataKeys[row.RowIndex].Value;
                         string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
                         SqlConnection con = new SqlConnection(constr);

                         string qry = "update [TMU$Student Attendance Line - COL] set [Attendance Type]=0 ,[Coordinator]='" + cordinat + "',[Remark]='" + remarks + "',[Event Type]='" + ddlevents + "',[Updated Date]=convert(date,getdate(),106) where [Student No_]= @studentno and Date between  '" + dateform + "' and '" + Dateto + "' and [Hour] like '%' +'" + Lactures + "'";

                         SqlCommand cmd = new SqlCommand(qry, con);
                         cmd.Parameters.AddWithValue("@studentno", id);
                         con.Open();
                         cmd.ExecuteNonQuery();
                         con.Close();

                     }

                     else
                     {
                         if (ChkBoxRows.Checked == true)
                         {
                            // ChkBoxRows.Checked = true;
                             var id = GridView1.DataKeys[row.RowIndex].Value;
                             string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
                             SqlConnection con = new SqlConnection(constr);
                             string qry = "update [TMU$Student Attendance Line - COL] set [Attendance Type]=0 ,[Coordinator]='" + cordinat + "',[Remark]='" + remarks + "',[Event Type]='" + ddlevents + "',[Updated Date]=convert(date,getdate(),106) where [Student No_]= @studentno and Date between  '" + dateform + "' and '" + Dateto + "' and [Hour] like '%' +'" + Lactures + "'";

                             SqlCommand cmd = new SqlCommand(qry, con);
                             cmd.Parameters.AddWithValue("@studentno", id);
                             con.Open();
                             cmd.ExecuteNonQuery();
                             con.Close();
                         }
                     }
                     

                }
            }
        }
        else
        {
            foreach (GridViewRow row in GridView1.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                     CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");

                     
                         if (ChkBoxHeader.Checked == true)
                         {
                           // ChkBoxRows.Checked = true;
                         var id = GridView1.DataKeys[row.RowIndex].Value;

                         string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
                         SqlConnection con = new SqlConnection(constr);
                         string qry = "update [TMU$Student Attendance Line - COL] set [Attendance Type]=0 ,[Coordinator]='" + cordinat + "',[Remark]='" + remarks + "',[Event Type]='" + ddlevents + "',[Updated Date]=convert(date,getdate(),106) where [Student No_]= @studentno and Date ='" + DateCommited + "'  and [Hour] like '%' +'" + Lactures + "'";

                         SqlCommand cmd = new SqlCommand(qry, con);
                         cmd.Parameters.AddWithValue("@studentno", id);
                         // cmd.Parameters.AddWithValue("@fromdate", txtDateFrom.Text);
                         con.Open();
                         cmd.ExecuteNonQuery();
                         con.Close();
                         // BindGrid();
                     }

                     else
                     {
                         if (ChkBoxRows.Checked == true)
                         {
                            // ChkBoxRows.Checked = true;
                             var id = GridView1.DataKeys[row.RowIndex].Value;

                             string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
                             SqlConnection con = new SqlConnection(constr);
                             string qry = "update [TMU$Student Attendance Line - COL] set [Attendance Type]=0 ,[Coordinator]='" + cordinat + "',[Remark]='" + remarks + "',[Event Type]='" + ddlevents + "',[Updated Date]=convert(date,getdate(),106) where [Student No_]= @studentno and Date ='" + DateCommited + "'  and [Hour] like '%' +'" + Lactures + "'";

                             SqlCommand cmd = new SqlCommand(qry, con);
                             cmd.Parameters.AddWithValue("@studentno", id);
                             // cmd.Parameters.AddWithValue("@fromdate", txtDateFrom.Text);
                             con.Open();
                             cmd.ExecuteNonQuery();
                             con.Close();

                         }
                     }

                     

                }
            }
        }
       // GridView1.Visible = false;
    }
    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }

        }
    }
    protected void gdlbtnRemove_Click(object sender, EventArgs e)
    {
   

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
         DataTable dt1 = (DataTable)Session["Studentdata"];
       // DataTable dt = new DataTable();
         if (dt1.Rows.Count > 0)
         {
             dt1.Rows[e.RowIndex].Delete();
             GridView1.DataSource = dt1;
             GridView1.DataBind();
         }
    }
   }