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
using System.Net;
using System.Net.Mail;


public partial class Faculty_FA_MM_View_Attendance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        
            if (!IsPostBack)
            {
                BindAcademicYear();
                bindSemester();

                BindSubject();
                bindGrid();
            }


    }

    public void BindAcademicYear()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            ddlAcademicYear.DataSource = dt1;
            ddlAcademicYear.DataTextField = "Details";
            ddlAcademicYear.DataValueField = "No_";
            ddlAcademicYear.DataBind();
        }
        catch
        {
        }
    }
    public void bindSemester()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("proc_GetSmester", con);
            cmd.Parameters.Add("@AcademicSession", ddlAcademicYear.SelectedValue);
            cmd.Parameters.Add("@StudentNo", Session["MM_StudentId"].ToString().Trim());
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            ddlSem.DataSource = dt1;
            ddlSem.DataTextField = "Semester";
            ddlSem.DataValueField = "Semester";
            ddlSem.DataBind();
        }
        catch
        {
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        bindGrid();

    }

    public void ReviewAttandanceCountDetail(string str, string CourseCode, int i, string EventType)
    {
        try
        {
            GetStudentCollege(Session["MM_StudentId"].ToString().Trim());
            SqlCommand cmd = new SqlCommand("[Sp_ReviewAttandanceCountDetailStudent]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudentNo_", str);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.Add("@CourseCode", Session["MM_Course"].ToString());
            cmd.Parameters.Add("@SubjectCode", CourseCode);
            cmd.Parameters.Add("@SemYear", ddlSem.SelectedValue);
            //cmd.Parameters.Add("@Type", ddlTypeClass.SelectedValue == "0,4" ? "0" : ddlTypeClass.SelectedValue);
            //cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@CollegeCode", ViewState["CollegeCode"].ToString().Trim());
            cmd.Parameters.Add("@AttendanceType", i);




            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grdAttandanceDetails.DataSource = dt;

                grdAttandanceDetails.DataBind();
            }


        }
        catch (Exception ex) { }
    }


    protected void grdAttendanceReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAttendanceReport.PageIndex = e.NewPageIndex;
        //bindGrid();
    }
    public void bindGrid()
    {


        SqlCommand cmd = new SqlCommand("StudentAttendanceNew '" + Session["MM_StudentId"].ToString() + "','" + ddlAcademicYear.SelectedValue + "','" + ddlSem.SelectedValue + "','" + drpSubject.SelectedValue + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdAttendanceReport.DataSource = dt;
            grdAttendanceReport.DataBind();
            grdAttendanceReport.ShowFooter = true;
            decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Per"));
            grdAttendanceReport.FooterRow.Cells[3].Text = "Total Percentage : ";
            grdAttendanceReport.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            grdAttendanceReport.FooterRow.Cells[4].Text = ((int)(total / dt.Rows.Count)).ToString() + " %";
        }
        else
        {
            grdAttendanceReport.DataSource = "";
            grdAttendanceReport.DataBind();
        }

    }



    public void BindSubject()
    {

        SqlCommand cmd = new SqlCommand("Sp_GetStudentSubjects", con); //added on 07-10-2017 Created procedure
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", Session["MM_Course"].ToString().Trim());
        cmd.Parameters.Add("@SemesterCode", ddlSem.SelectedValue);
        cmd.Parameters.Add("@Year", ddlSem.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue.Trim());
        cmd.Parameters.Add("@StudentNo", Session["MM_StudentId"].ToString().Trim());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpSubject.DataSource = dt;
            drpSubject.DataTextField = "Details";
            drpSubject.DataValueField = "No_";
            drpSubject.DataBind();
        }
        else
        {
            drpSubject.DataSource = "";

            drpSubject.DataBind();
        }
    }


    public void GetStudentCollege(string StudentNo)
    {
        pms_connection conp = new pms_connection();
        SqlDataReader dr = conp.SP_FA_Get_CollegeCode_Of_StudentNo(StudentNo.Trim());
        dr.Read();
        if (dr.HasRows)
        {
            ViewState["CollegeCode"] = dr["CollegeCode"].ToString().Trim();
            dr.Close();
            conp.DisConnect();
        }
        else
        {
            ViewState["CollegeCode"] = "";
            dr.Close();
            conp.DisConnect();
        }
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSemester();
        BindSubject();
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubject();
    }
    protected void grdAttendanceReport1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdAttendanceReport1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGrid();
    }



    protected void lblDel_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            int index = row.RowIndex;

            Label lblSubject = (Label)grdAttendanceReport.Rows[index].FindControl("lblCourseCode");



            ReviewAttandanceCountDetail(Session["MM_StudentId"].ToString(), lblSubject.Text, 0, "0");
            GridViewDetails.Show();
        }
        catch (Exception ex) { }
    }
}