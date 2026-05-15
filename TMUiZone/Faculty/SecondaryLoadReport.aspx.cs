using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Faculty_SecondaryLoadReport : System.Web.UI.Page
{

    TMUConnection con; string CollegeCode = "";
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try {              
               if (!IsPostBack)
                    {
                       bindAcademicYear();                       
                       BindGrid();                       
                    }
            }
       catch (Exception ex)
            {
              Response.Redirect("~/Default.aspx");
            }
    }
    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        drpAcademicYear.DataSource = dt1;
        drpAcademicYear.DataTextField = "Details";
        drpAcademicYear.DataValueField = "No_";
        drpAcademicYear.DataBind();
    }
   
    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_secondryloadReport", con))
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);               
                cmd.Parameters.AddWithValue("@Employeecode", Session["uid"].ToString());               
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        }
                }
            }
        }
    }

    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
   
}