using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Reporting.WebForms;
public partial class StudentContinueAbsentReport : System.Web.UI.Page
{
    TMUConnection con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["GlobalDimension1Code"].ToString() != "")
            {
                if (!IsPostBack)
                {
                    bindAcademicYear();
                    BindCourse();
                    BindSemester();
                    GetStudentList();
                }
               
            }
        }
        catch { Response.Redirect("~/Default.aspx"); }
    }
    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        ddlAcademicYear.DataSource = dt1;
        ddlAcademicYear.DataTextField = "Details";
        ddlAcademicYear.DataValueField = "No_";
        ddlAcademicYear.DataBind();
    }
    public void BindCourse()
    {
        SqlCommand cmd = new SqlCommand("Sp_GetCourseRoleWise_HOD_Role_ForAbsentReport", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@userid", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlCourse.DataSource = dt;
        ddlCourse.DataTextField = "Details";
        ddlCourse.DataValueField = "No_";
        ddlCourse.DataBind();

    }


    public void BindSemester()
    {
        string FacultyCode = "";
        //if (chkboxPrinciple.Checked == false)
        //{
        //    FacultyCode = Session["uid"].ToString();
        //}
        if (Session["UserGroup"].ToString() == "FACULTY") { FacultyCode = Session["uid"].ToString(); }
        SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlSemYear.DataSource = dt;
        ddlSemYear.DataTextField = "Details";
        ddlSemYear.DataValueField = "No_";
        ddlSemYear.DataBind();
    }

    public void BindSection()
    {
        string FacultyCode = "";
        //if (chkboxPrinciple.Checked == false)
        //{
        //    FacultyCode = Session["uid"].ToString();
        //}
        if (Session["UserGroup"].ToString() == "FACULTY") { FacultyCode = Session["uid"].ToString(); }
        SqlCommand cmd = new SqlCommand("proc_GetSection_Role", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", ddlSemYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlSection.DataSource = dt;
        ddlSection.DataTextField = "Details";
        ddlSection.DataValueField = "No_";
        ddlSection.DataBind();
    }


    public void GetStudentList()
    {
        con = new TMUConnection();
        string Course = "";
        //SqlCommand cmd = new SqlCommand("GetStudentAbsentList1", con1);
        if (Session["Hod"].ToString() != "" && Session["UserGroup"].ToString() == "FACULTY")
        {
            SqlCommand cmdHodCourse = new SqlCommand("Sp_CourseForHOD_HOD", con1);
            cmdHodCourse.CommandType = CommandType.StoredProcedure;
            cmdHodCourse.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmdHodCourse.Parameters.Add("@HodId", Session["Hod"].ToString());
            con1.Open();
            SqlDataReader dr = cmdHodCourse.ExecuteReader();
            dr.Read();
            Course = ddlCourse.SelectedValue;//dr["Course"].ToString();
           // ddlCourse.Visible = false;
            con1.Close();

        }
        else
        {
           
            Course=ddlCourse.SelectedValue;
        }
        SqlCommand cmd = new SqlCommand("GetStudentAbsentList_HOD_Role", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CourseCode", Course);
        cmd.Parameters.Add("@SemYear", ddlSemYear.SelectedValue);
        cmd.Parameters.Add("@Section", ddlSection.SelectedValue);
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.CommandTimeout = 5000;
        DataTable dt = new DataTable();
        dt.Clear();
        da.Fill(dt);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.Reset();

        ReportDataSource ds1 = new ReportDataSource("StudentContinueReport", dt);

        ReportViewer1.LocalReport.ReportPath = "Report/StudentContinueReport.rdlc";

        ReportViewer1.LocalReport.DataSources.Add(ds1);
        ReportViewer1.LocalReport.Refresh();
        
    }
   
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSemester();
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        GetStudentList();
    }
    protected void ddlSemYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSection();
    }
}