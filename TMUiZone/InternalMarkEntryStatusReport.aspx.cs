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
using System.Web.Services;
using System.Web.Script.Services;

public partial class Faculty_InternalMarkEntryStatusReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //bindDrpCourseList();  //added on 24 feb 2017
                bindAcademicYear();
                bindDrpCourseList();
            }
        }
        catch (Exception ex)
        {// Response.Redirect("~/Default.aspx");
        }
    }

    public void bindAcademicYear()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            drpAcademicYear.DataSource = dt1;
            drpAcademicYear.DataTextField = "Details";
            drpAcademicYear.DataValueField = "No_";
            drpAcademicYear.DataBind();
        }
        catch
        {
        }
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID1", Session["uid"].ToString());
        cmd.Parameters.Add("@ID", drpCourse.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
     
        bindDrpSemesterList();

    }

   

    public void bindgrid()
    {
        try
        {
            //SqlCommand cmd = new SqlCommand("sp_GetMarkEntryForFaculty", con);//usp_GetMarkEntrytable1 comment on 28-12-2017
            SqlCommand cmd = new SqlCommand("sp_GetInternalMarkEntryReport", con);//sp_GetMarkEntryForFaculty //sp_GetMarkEntryForFaculty_Calendar //comment on 02-05-2018
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            if (con.State == ConnectionState.Open) { con.Close(); }
            con.Open();
            da.Fill(dt);
            con.Close();
            grdmarktable.DataSource = dt;
            grdmarktable.DataBind();
           
            if(dt.Rows.Count>0)
            { btnPrint.Visible = true; }
            else { btnPrint.Visible = false; }
        }
        catch
        {
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        bindgrid();
    }
}