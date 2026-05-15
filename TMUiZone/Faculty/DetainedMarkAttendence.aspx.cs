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

public partial class Faculty_DetainedMarkAttendence : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    DataTable dtStudent = new DataTable();
    DataTable dt12 = new DataTable();
    static String No_ = ""; static int cnt = 0; static bool Save = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblFacultyCode.Text = Session["uid"].ToString();
                lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
                txtDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                bindDrpCourseList();  //added on 24 feb 2017
                bindAcademicYear();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FDL.GetSubjectTypebySemester(drpCourse.SelectedValue, drpSemester.SelectedItem.ToString(),drpSubject.SelectedItem.ToString());//comment on 29 aug 2016
        lblSubjectType.Text = FDL.GetSubjectTypebyCourseSubject(drpCourse.SelectedValue, drpSubject.SelectedValue);

        bindLecture();
        bindUnit();


    }
    public void bindUnit()
    {
        //--------------------------05-10-2016----Free Room no---
        SqlCommand cmdCL = new SqlCommand("proc_GetSubjectClassification", con);
        cmdCL.CommandType = CommandType.StoredProcedure;
        cmdCL.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmdCL.Parameters.Add("@Subject", drpSubject.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmdCL);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        int Theory_count = 0;
        Theory_count = Convert.ToInt16(cmdCL.ExecuteScalar().ToString());
        con.Close();
        //-----------05-10-2016--Grou,Batch---by ashu--ENd

        DataTable dt = new DataTable();
        dt = FDL.GetUnitForMarkAttendance(drpCourse.SelectedValue, drpSubject.SelectedValue, drpAcademicYear.SelectedValue);
        drpUnit.DataSource = dt;
        drpUnit.DataTextField = "Details";
        drpUnit.DataValueField = "No_";
        drpUnit.DataBind();
        //-----------05-10-2016--Grou,Batch---by ashu --Start 
        if (Theory_count == 0 && dt.Rows.Count == 1)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("No_", typeof(string));
            dt1.Columns.Add("Details", typeof(string));
            dt1.Rows.Add("No Unit", "");
            drpUnit.DataSource = dt1;
            drpUnit.DataTextField = "Details";
            drpUnit.DataValueField = "No_";
            drpUnit.DataBind();
        }
        //-----------05-10-2016--Group,Batch---by ashu--ENd
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();  //added on 24 feb 2017
        bindLecture();
        bindGetSubjectList();
    }
    public void bindLecture()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetLectureFromTimeTable_Combined_Detained", con);//proc_GetLectureFromTimeTable by shubham sharma
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        //Added on 20-10-2016 --for filteration from [TMU$Time Table Generation - COL]
        cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);
        cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpLecture.DataSource = dt;

        drpLecture.DataTextField = "Details";
        drpLecture.DataValueField = "No_";
        drpLecture.DataBind();
    }
    public void bindAcademicYear()
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
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGetSubjectList();
        //  ShowRemedialClass();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ShowRemedialClass();
        bindGroupList();
        bindBatchList();
    }
    protected void drpLecture_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGetSubjectList();
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGetSubjectList();
    }
    public void bindDrpCourseList()
    {
        DataTable dt = new DataTable();
        // dt = FDL.GetCourseList(lblFacultyCode.Text);    //21-12-2016
        //  dt = FDL.GetCourseList(lblFacultyCode.Text, Session["GlobalDimension1Code"].ToString());//comment on 24-02-2017
        dt = FDL.GetCourseListFromTimeTable(lblFacultyCode.Text, Session["GlobalDimension1Code"].ToString(), txtDate.Text);  // added on 24-02-017 by ashu
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    public void bindDrpSemesterList()
    {
        //SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_Role1", con);
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromTimeTable_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID1", lblFacultyCode.Text);
        cmd.Parameters.Add("@ID", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }
    public void bindSectionList()
    {
        DataTable dt = new DataTable();
        //SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role1", con);
        SqlCommand cmd = new SqlCommand("proc_GetSectionFromTimeTable_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();

    }
    public void bindGetSubjectList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTimeTable_WithGroupBatch", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);
        cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();
    }

    //-------------add by ashu--on 25-08-2016
    public void bindGroupList()
    {
        SqlCommand cmd = new SqlCommand("sp_GetGroupFromTimeTable_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@Date", txtDate.Text);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "Details";
        ddlGroup.DataValueField = "No_";
        ddlGroup.DataBind();
    }
    public void bindBatchList()
    {
        SqlCommand cmd = new SqlCommand("sp_GetBatchFromTimeTable_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@Date", txtDate.Text);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlBatch.DataSource = dt;
        ddlBatch.DataTextField = "Details";
        ddlBatch.DataValueField = "No_";
        ddlBatch.DataBind();
    }
    //------------------------------------
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
        //  ShowRemedialClass();
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSectionList();
        bindGetSubjectList();
        bindGroupList();// add by ashu on 25-08-2016
        bindBatchList();// add by ashu on 25-08-2016

        // ShowRemedialClass();
    }
    public DataTable bindStudentGrid()
    {

        DateTime dtDate = new DateTime();
        dtDate = Convert.ToDateTime(txtDate.Text);
        string date = dtDate.ToString("MM-dd-yyyy");
        // SqlCommand cmdStudent = new SqlCommand("SP_GetCombinedStudentAsPerTimetable", con);  //comment on 30-march 2017 by ashu
        SqlCommand cmdStudent = new SqlCommand("[Sp_getStudentForMarkAttendence_Detained]", con);  //New Procedure created on 30-march 2017 by ashu
        //SP_GetCombinedStudentAsPerTimetable_medical
        cmdStudent.CommandType = CommandType.StoredProcedure;
        cmdStudent.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmdStudent.Parameters.Add("@Date", txtDate.Text);//date
        cmdStudent.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmdStudent.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmdStudent.Parameters.Add("@Section", drpSection.SelectedValue);
        cmdStudent.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmdStudent.Parameters.Add("@LectureNo", drpLecture.SelectedValue);
        cmdStudent.Parameters.Add("@Group", ddlGroup.SelectedValue);  //----Add Group on 25-08-2016 by ashu
        cmdStudent.Parameters.Add("@Batch", ddlBatch.SelectedValue); //----Add Batch on 25-08-2016 by ashu
        cmdStudent.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        SqlDataAdapter daStudent = new SqlDataAdapter(cmdStudent);
        //DataTable dtStudent = new DataTable();

        SqlDataAdapter da = new SqlDataAdapter(cmdStudent);

        da.Fill(dtStudent);

        cnt = dtStudent.Rows.Count;
        grdStudentAttendance.DataSource = dtStudent;
        grdStudentAttendance.DataBind();

        if (dtStudent.Rows.Count > 0)
        {

            btnSubmit.Visible = true;

        }

        if (grdStudentAttendance.Rows.Count > 0)
        {
            EnableDisable(false);
        }
        else
        { EnableDisable(true); }
        return dtStudent;
    }
    public void EnableDisable(bool TF)
    {


        txtDate.Enabled = TF;

    }
    public void Blank()
    {
        EnableDisable(true);

       btnSubmit.Visible = false;



        // txtTopic.Text = "";
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        No_ = grdStudentAttendance.SelectedDataKey.Value.ToString();
        // BindAttendanceSummaryGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal3').modal();", true);
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        bindStudentGrid();
        Save = true;
    }


    public string InsertStudentAttendanceHeaderAndLine(string DocumentNo, string AcademicYear, int Hour, string Date, string FacultyCode, int AttendanceType, string Unit, string Topic, string CollegeCode, DataTable dt)
    {
        string Result = "";
        if (con.State == ConnectionState.Closed)
            con.Open();
        try
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if(dt.Rows[i]["Group"]=="&nbsp;")
                //{
                //    dt.Rows[i]["Group"] = "";
                //}
                //if (dt.Rows[i]["Batch"] == "&nbsp;")
                //{
                //    dt.Rows[i]["Batch"] = "";
                //}
                //if (dt.Rows[i]["Section"] == "&nbsp;")
                //{
                //    dt.Rows[i]["Section"] = "";
                //}

                SqlCommand cmd = new SqlCommand("[dbo].[proc_insertStudentAttendanceHeaderAndLine]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
                cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
                cmd.Parameters.AddWithValue("@Course", dt.Rows[i]["Course"]);
                cmd.Parameters.AddWithValue("@Semester", dt.Rows[i]["Semester/Year"]);
                cmd.Parameters.AddWithValue("@Section", dt.Rows[i]["Section"].ToString().Replace("&nbsp;", ""));
                cmd.Parameters.AddWithValue("@Group", dt.Rows[i]["Group"].ToString().Replace("&nbsp;", ""));
                cmd.Parameters.AddWithValue("@Batch", dt.Rows[i]["Batch"].ToString().Replace("&nbsp;", ""));
                cmd.Parameters.AddWithValue("@SubjectCode", dt.Rows[i]["Subject Code"]);
                cmd.Parameters.AddWithValue("@SubjectType", dt.Rows[i]["SubjectType"]);
                cmd.Parameters.AddWithValue("@Hour", Hour);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@FacultyCode", FacultyCode);
                cmd.Parameters.AddWithValue("@AttendanceType", AttendanceType);
                cmd.Parameters.AddWithValue("@Unit", Unit);
                cmd.Parameters.AddWithValue("@Topic", Topic);
                cmd.Parameters.AddWithValue("@CollegeCode", CollegeCode);
                cmd.Parameters.AddWithValue("@EnrollmentNo", "ex");
                cmd.Parameters.AddWithValue("@StudentName", dt.Rows[i]["Student Name"]);
                cmd.Parameters.AddWithValue("@StudentNo", dt.Rows[i]["StudentNo"]);
                cmd.Parameters.AddWithValue("@Attendance", dt.Rows[i]["Today"]);// attendance absent/persent          
                cmd.Parameters.AddWithValue("@Detained", 1);
                cmd.ExecuteNonQuery();
            }
        }
        catch { Result = "Error"; }
        finally
        {
            con.Close();
        }
        return Result;
    }
    public void BindTable()
    {

        for (int i = 0; i < grdStudentAttendance.Columns.Count; i++)
        {
            // dt12.Columns.Add(grdStudentAttendance.HeaderRow.Cells[i].Text);            
            dt12.Columns.Add(grdStudentAttendance.Columns[i].HeaderText);
        }

        dt12.Columns[11].ColumnName = "SubjectType";
        dt12.Columns[12].ColumnName = "StudentNo";
        int r = 0;
        foreach (GridViewRow row in grdStudentAttendance.Rows)
        {
            DataRow dr = dt12.NewRow();
            for (int j = 0; j < grdStudentAttendance.Columns.Count; j++)
            {
                CheckBox rb = (CheckBox)row.FindControl("chkboxAttendance");
                if (j == 10) //if (j == 9)  //due to srno
                {
                    if (rb.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                if (j == 11 || j == 12)
                {

                }
                else
                {
                    dr[grdStudentAttendance.Columns[j].HeaderText] = row.Cells[j].Text;
                }
            }
            HiddenField hfSubjectType = (HiddenField)row.FindControl("hfSubjectType");
            HiddenField hfStudentNo = (HiddenField)row.FindControl("hfStudentNo");
            dt12.Rows.Add(dr);
            dt12.Rows[r]["SubjectType"] = hfSubjectType.Value;
            dt12.Rows[r]["StudentNo"] = hfStudentNo.Value;
            r = r + 1;
        }
       


    }
    protected void btnSave2_Click(object sender, EventArgs e)
    {
        string Result = "";
        BindTable();
        if (drpUnit.SelectedValue == "")
        {
            
        }

        else
        {
            if (Save == true)
            {
                Result = InsertStudentAttendanceHeaderAndLine("", drpAcademicYear.SelectedValue, Convert.ToInt16(drpLecture.SelectedValue), txtDate.Text, lblFacultyCode.Text, 0, drpUnit.SelectedValue, txtTopic.Text, Session["GlobalDimension1Code"].ToString(), dt12);  //ASHU---25-08-2016  
                if (Result != "Error")
                {
                    lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
                    //bindGrid();
                    bindStudentGrid();
                    Blank();
                    lblMessage.Visible = false;
                    btnSubmit.Enabled = true;
                    btnSave.Enabled = true;
                    Save = false;
                    System.Threading.Thread.Sleep(2000);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Something went wrong !');", true);
                }
            }
            else
            {

            }
        }
    }
}