using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

//using 
public partial class Faculty_LoadReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindAcademicYear();
                bindReport();
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        ddlAcademicYear.DataSource = dt1;
        ddlAcademicYear.DataTextField = "Details";
        ddlAcademicYear.DataValueField = "No_";
        ddlAcademicYear.DataBind();
    }
    public void bindReport()
    {
        SqlCommand cmd = new SqlCommand("Sp_GetFacultyLoadFromCourse_Subject_Assign", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.AddWithValue("@OddEvenYear", ddlOddEvenYear.SelectedValue );
        cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //var results = dt.AsEnumerable()
        //               .Where(row => row.Field<string>("CourseCollege") != Session["GlobalDimension1Code"].ToString())
        //               .Select(row => row.Field<int>("Lecture"))
        //               .Sum();
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.Reset();
        ReportDataSource ds1 = new ReportDataSource("LoadReport", dt);
        //ReportViewer1.Visible = false;
        //if (dt.Rows.Count > 0) { ReportViewer1.Visible = true; }
        ReportViewer1.LocalReport.ReportPath = "Report/LoadReport.rdlc";        
        ReportViewer1.LocalReport.DataSources.Add(ds1);       
        ReportViewer1.LocalReport.Refresh();
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  bindAcademicYear();
        bindReport();
    }
    protected void ddlOddEvenYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindReport();
    }
}