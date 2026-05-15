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
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using WebReference;

public partial class Faculty_NoDetain : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {


                bindAcademicYear();
                College(); 
                bindDrpCourseList();
                bindSemester();
                bindSubject();
               // bindApprovalList();

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    public void College()
    {

        con.Open();
        SqlDataAdapter daP = new SqlDataAdapter("select distinct [College Code],(select Name from [TMU$Dimension Value] where Code=[College Code]) 'College Name' from [TMU$Student Detainee List] where [Academic Year]='"+drpAcademicYear.SelectedValue+"'  ", con);
        DataTable dtMinuteP = new DataTable();

        daP.Fill(dtMinuteP);
        drpColleganame.DataSource = dtMinuteP;
        drpColleganame.DataTextField = "College Name";
        drpColleganame.DataValueField = "College Code";
        drpColleganame.DataBind();
        con.Close();
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
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@ID2", drpColleganame.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        //Session["UserRole"] = dt.Rows[0]["UserRole"].ToString();
        con.Close();
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    public void bindSemester()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        //Session["UserRole"] = dt.Rows[0]["UserRole"].ToString();
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
            SqlCommand cmd = new SqlCommand("sp_getSubjrctCodeHODWise_markslock", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Open();
            da.Fill(ds);
            con.Close();
            ddlSubject.DataSource = ds.Tables[0];
            ddlSubject.DataTextField = "Description";
            ddlSubject.DataValueField = "Subject Code";
            ddlSubject.DataBind();

        }
        catch (Exception ex) { }
    }

    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSemester();
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        GetStudentList();

    }


    public void GetStudentList()
    {

        con.Open();
        SqlCommand cmd = new SqlCommand("[Pro_GetListForDetain]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Acadyear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@Course", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Sem", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Subject", ddlSubject.SelectedValue);
        cmd.Parameters.Add("@facultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.CommandTimeout = 50000;
        DataTable dt = new DataTable();
        if (con.State == ConnectionState.Open) { con.Close(); }
        con.Open();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            btnDetanie.Visible = true;
            GrdDetenee.DataSource = dt;
            GrdDetenee.DataBind();
        }
        else
        {
            btnDetanie.Visible = false;
            GrdDetenee.DataSource = "";
            GrdDetenee.DataBind();
        }


    }




    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GrdDetenee.AllowPaging = false;

        GetStudentList();

     
        GrdDetenee.Columns[8].Visible = false;




        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        GrdDetenee.RenderControl(htmlWrite);

        Response.Clear();
        string str = "NoDetainee_Report";
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void brnReject_Click(object sender, EventArgs e)
    {

    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)GrdDetenee.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow Row in GrdDetenee.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkStudent");
            if (checkBoxheader.Checked == true)
            {
                checkRows.Checked = true;

            }
            else
            {
                checkRows.Checked = false;
            }

        }
    }
    protected void chkStudent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)GrdDetenee.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow Row in GrdDetenee.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkStudent");
            if (checkRows.Checked == false)
            {
                checkBoxheader.Checked = false;

            }
            else
            {
                // checkBoxheader.Checked = true;
            }

        }
    }
    protected void btnDetanie_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GrdDetenee.Rows)
        {
            HiddenField StudenNo = (HiddenField)row.FindControl("SNumber");
            Label StudentName = (Label)row.FindControl("lblStudentName");
            Label EnrollmentNo = (Label)row.FindControl("lblEnrollment");
            Label Sem = (Label)row.FindControl("lblSemester");
            Label SubjectCode = (Label)row.FindControl("lblSubject");
            Label Course = (Label)row.FindControl("lblCourse");
            CheckBox check = (CheckBox)row.FindControl("chkStudent");

            if (check.Checked == true)
            {

                SqlCommand cmd = new SqlCommand("sp_UpdateDetanieeList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SNo", StudenNo.Value);
                cmd.Parameters.AddWithValue("@StudentName", StudentName.Text);
                cmd.Parameters.AddWithValue("@EnrollmentNo", EnrollmentNo.Text);
                cmd.Parameters.AddWithValue("@Semester", Sem.Text);
                cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode.Text);
                cmd.Parameters.AddWithValue("@CourseCode", Course.Text);
                cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Data Submit Successfully'); document.location.href='NoDetain.aspx';", true);
    }
    protected void drpColleganame_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();
    }
}