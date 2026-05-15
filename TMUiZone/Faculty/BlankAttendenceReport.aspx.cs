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

public partial class Faculty_BlankAttendenceReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            bindAcademicYear(); bindDrpCourseList(); bindSectionList();
            //BindReport();
        }
    }
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 1000000;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        con.Open();
        da.Fill(dt);
        con.Close();
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }
    public void bindSubject()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getSubjrctCodeFacultyWise_Lab", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlSubject.DataSource = dt;

            ddlSubject.DataTextField = "Description";
            ddlSubject.DataValueField = "Subject Code";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "--Course--");

        }
        catch (Exception ex) { }
    }
    public void bindAcademicYear()
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
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", Session["uid"].ToString());
            cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@Academic", drpAcademicYear.SelectedItem.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            drpCourse.DataSource = dt;
            drpCourse.DataTextField = "Details";
            drpCourse.DataValueField = "No_";
            drpCourse.DataBind();
        }
        catch (Exception ex) { }
    }
    public void bindSectionList()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            drpSection.DataSource = dt;
            drpSection.DataTextField = "Details";
            drpSection.DataValueField = "No_";
            drpSection.DataBind();
        }
        catch (Exception ex) { }
    }

    public void BindReport()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getStudent_attendance_Detail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GD1", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@academic", drpAcademicYear.SelectedValue);
            cmd.Parameters.Add("@Course", drpCourse.SelectedValue);
            cmd.Parameters.Add("@Sem", drpSemester.SelectedValue);
            cmd.Parameters.Add("@Subject", ddlSubject.SelectedValue);
            if (rdInternal.Checked == true)
            {
                cmd.Parameters.AddWithValue("@ExamType", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ExamType", 2);
            }
            if (drpSection.SelectedIndex > 0)
            { cmd.Parameters.AddWithValue("@section", drpSection.SelectedValue); }
            else { cmd.Parameters.AddWithValue("@section", ""); }
            if (txtDateFrom.Text != "")
            {
                cmd.Parameters.Add("@FromDate", Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy/MM/dd"));
            }
            else
            {
                cmd.Parameters.Add("@FromDate", "");
            }
            if (txtDateTo.Text != "")
            {
                cmd.Parameters.Add("@ToDate", Convert.ToDateTime(txtDateTo.Text).ToString("yyyy/MM/dd"));
            }
            else
            {
                cmd.Parameters.Add("@ToDate", "");
            }
            //if (drpAssignmentNo.SelectedIndex > 0)
            //{ cmd.Parameters.AddWithValue("@AssigNo", drpAssignmentNo.SelectedValue); }
            //else { }
            // cmd.Parameters.AddWithValue("@AssigNo", "");


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblmsg.Visible = false;
                ReportViewer.Visible = true;
                ReportViewer.ProcessingMode = ProcessingMode.Local;
                ReportViewer.Visible = true;
                ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Report/Report_blank_attendance.rdlc");
                ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
                ReportViewer.LocalReport.DataSources.Clear();
                ReportViewer.LocalReport.DataSources.Add(datasource);
            }
            else
            {
                ReportViewer.Visible = false;
                lblmsg.Visible = true;

            }
            //grdReport.DataSource = dt;
            //grdReport.DataBind();
        }
        catch (Exception ex) { }
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();
       // BindReport();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
        bindSectionList();
       // BindReport();
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();
        bindSectionList();
       // BindReport();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  BindReport();
        bindSectionList();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindReport();
    }
    protected void ddlExamtype_SelectedIndexChanged(object sender, EventArgs e) 
    {
      //  BindReport();
    }
    protected void rdInternal_CheckedChanged(object sender, EventArgs e)
    {
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
        tddate.Visible = false;
        bindDrpCourseList();
        bindDrpSemesterList();
        bindSubject();
        bindSectionList();
        BindReport();
    }
    protected void rdExternal_CheckedChanged(object sender, EventArgs e)
    {
        tddate.Visible = true;
        bindDrpCourseList();
        bindDrpSemesterList();
        bindSubject();
        bindSectionList();
        BindReport();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport();
    }
}