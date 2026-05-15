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
using System.Transactions;

public partial class Faculty_StudentAttendanceMark1 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    DataTable dt2 = new DataTable();
    DataTable dtStudent = new DataTable();
    static int cnt = 0; static String No_ = ""; //static bool Save = false;
    static String No_D = ""; static int cntD = 0; static bool SaveD = false; static int cntOpen = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Chkdetained.Checked == true)
                {
                    PnlDeatinedAttendence.Visible = true;
                    PanelmainAttendence.Visible = false;
                    pnlOpen.Visible = false;


                }
                else
                {
                    PnlDeatinedAttendence.Visible = false;
                    PanelmainAttendence.Visible = true;
                    pnlOpen.Visible = false;


                }
                if (Session["GlobalDimension1Code"].ToString() == "DPT")
                {

                    chkBoxExtraClass.Enabled = true;

                }
                else
                {
                    chkBoxExtraClass.Enabled = false;
                }
                if (Session["GlobalDimension1Code"].ToString() == "TPHD")
                {

                    divAdmissionPeriod.Visible = true;

                }
                else
                {
                    divAdmissionPeriod.Visible = false;
                }
                lblFacultyCode.Text = Session["uid"].ToString();
                // bindDrpCourseList();
                lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
                txtDate.Text = Session["Date"].ToString();
                txtDate.Enabled = false;
                txtDate1.Text = Session["Date"].ToString();
                txtDate1.Enabled = false;
                //txtDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                bindDrpCourseList();  //added on 24 feb 2017
                bindAcademicYear();
                if (Session["GlobalDimension1Code"].ToString() == "TMDC")
                {
                    chkboxEditAttendance.Visible = false;
                    chkMultiplaeAttendance.Visible = false;
                }
                else
                {
                    chkboxEditAttendance.Visible = false;
                    chkMultiplaeAttendance.Visible = true;
                }
                lblFacultyCodeD.Text = Session["uid"].ToString();
                lblNoD.Text = FDL.GetNextStudentMarkAttendanceNumber();
                txtDateD.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                bindDrpCourseListD();  //added on 24 feb 2017
                bindAcademicYearD();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    public void bindUnitD()
    {
        //--------------------------05-10-2016----Free Room no---
        SqlCommand cmdCL = new SqlCommand("proc_GetSubjectClassification", con);
        cmdCL.CommandType = CommandType.StoredProcedure;
        cmdCL.Parameters.Add("@CourseCode", drpCourseD.SelectedValue);
        cmdCL.Parameters.Add("@Subject", drpSubjectD.SelectedValue);
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
        dt = FDL.GetUnitForMarkAttendance(drpCourseD.SelectedValue, drpSubjectD.SelectedValue, drpAcademicYearD.SelectedValue);
        drpUnitD.DataSource = dt;
        drpUnitD.DataTextField = "Details";
        drpUnitD.DataValueField = "No_";
        drpUnitD.DataBind();
        //-----------05-10-2016--Grou,Batch---by ashu --Start 
        if (Theory_count == 0 && dt.Rows.Count == 1)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("No_", typeof(string));
            dt1.Columns.Add("Details", typeof(string));
            dt1.Rows.Add("No Unit", "");
            drpUnitD.DataSource = dt1;
            drpUnitD.DataTextField = "Details";
            drpUnitD.DataValueField = "No_";
            drpUnitD.DataBind();
        }
        //-----------05-10-2016--Group,Batch---by ashu--ENd
    }

    public void bindLectureD()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetLectureFromTimeTable_Combined_Detained", con);//proc_GetLectureFromTimeTable by shubham sharma//proc_GetLectureFromTimeTable_Combined
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDateD.Text));
        cmd.Parameters.Add("@FacultyCode", lblFacultyCodeD.Text);
        cmd.Parameters.Add("@CourseCode", drpCourseD.SelectedValue);
        cmd.Parameters.Add("@Semester", drpSemesterD.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYearD.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubjectD.SelectedValue);
        cmd.Parameters.Add("@Section", drpSectionD.SelectedValue);
        //Added on 20-10-2016 --for filteration from [TMU$Time Table Generation - COL]
        cmd.Parameters.Add("@Group", ddlGroupD.SelectedValue);
        cmd.Parameters.Add("@Batch", ddlBatchD.SelectedValue);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpLectureD.DataSource = dt;

        drpLectureD.DataTextField = "Details";
        drpLectureD.DataValueField = "No_";
        drpLectureD.DataBind();
    }
    public void bindAcademicYearD()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        drpAcademicYearD.DataSource = dt1;
        drpAcademicYearD.DataTextField = "Details";
        drpAcademicYearD.DataValueField = "No_";
        drpAcademicYearD.DataBind();
    }
    public void bindDrpCourseListD()
    {
        DataTable dt = new DataTable();
        // dt = FDL.GetCourseList(lblFacultyCode.Text);    //21-12-2016
        //  dt = FDL.GetCourseList(lblFacultyCode.Text, Session["GlobalDimension1Code"].ToString());//comment on 24-02-2017
        dt = FDL.GetCourseListFromTimeTable(lblFacultyCodeD.Text, Session["GlobalDimension1Code"].ToString(), txtDateD.Text);  // added on 24-02-017 by ashu
        drpCourseD.DataSource = dt;
        drpCourseD.DataTextField = "Details";
        drpCourseD.DataValueField = "No_";
        drpCourseD.DataBind();
    }
    public void bindDrpSemesterListD()
    {
        //SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_Role1", con);
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromTimeTable_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID1", lblFacultyCodeD.Text);
        cmd.Parameters.Add("@ID", drpCourseD.SelectedValue);
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDateD.Text));
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSemesterD.DataSource = dt;
        drpSemesterD.DataTextField = "Details";
        drpSemesterD.DataValueField = "No_";
        drpSemesterD.DataBind();
    }
    public void bindSectionListD()
    {
        DataTable dt = new DataTable();
        //SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role1", con);
        SqlCommand cmd = new SqlCommand("proc_GetSectionFromTimeTable_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", lblFacultyCodeD.Text);
        cmd.Parameters.Add("@CourseCode", drpCourseD.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemesterD.SelectedValue);
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDateD.Text));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSectionD.DataSource = dt;
        drpSectionD.DataTextField = "Details";
        drpSectionD.DataValueField = "No_";
        drpSectionD.DataBind();

    }
    public void bindGetSubjectListD()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTimeTable_WithGroupBatch", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDateD.Text));
        cmd.Parameters.Add("@CourseCode", drpCourseD.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemesterD.SelectedValue);
        cmd.Parameters.Add("@Section", drpSectionD.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", lblFacultyCodeD.Text);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYearD.SelectedValue);
        cmd.Parameters.Add("@Group", ddlGroupD.SelectedValue);
        cmd.Parameters.Add("@Batch", ddlBatchD.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubjectD.DataSource = dt;
        drpSubjectD.DataTextField = "Details";
        drpSubjectD.DataValueField = "No_";
        drpSubjectD.DataBind();
    }

    //-------------add by ashu--on 25-08-2016
    public void bindGroupListD()
    {
        SqlCommand cmd = new SqlCommand("sp_GetGroupFromTimeTable_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourseD.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemesterD.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYearD.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@Date", txtDateD.Text);
        cmd.Parameters.Add("@Section", drpSectionD.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlGroupD.DataSource = dt;
        ddlGroupD.DataTextField = "Details";
        ddlGroupD.DataValueField = "No_";
        ddlGroupD.DataBind();
    }
    public void bindBatchListD()
    {
        SqlCommand cmd = new SqlCommand("sp_GetBatchFromTimeTable_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourseD.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemesterD.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYearD.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@Date", txtDateD.Text);
        cmd.Parameters.Add("@Section", drpSectionD.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlBatchD.DataSource = dt;
        ddlBatchD.DataTextField = "Details";
        ddlBatchD.DataValueField = "No_";
        ddlBatchD.DataBind();
    }
    //------------------------------------
    public DataTable bindStudentGridD()
    {

        DateTime dtDate = new DateTime();
        dtDate = Convert.ToDateTime(txtDateD.Text);
        string date = dtDate.ToString("MM-dd-yyyy");
        // SqlCommand cmdStudent = new SqlCommand("SP_GetCombinedStudentAsPerTimetable", con);  //comment on 30-march 2017 by ashu
        SqlCommand cmdStudent = new SqlCommand("[Sp_getStudentForMarkAttendence_Detained]", con);  //New Procedure created on 30-march 2017 by ashu
        //SP_GetCombinedStudentAsPerTimetable_medical
        cmdStudent.CommandType = CommandType.StoredProcedure;
        cmdStudent.Parameters.Add("@FacultyCode", lblFacultyCodeD.Text);
        cmdStudent.Parameters.Add("@Date", txtDateD.Text);//date
        cmdStudent.Parameters.Add("@CourseCode", drpCourseD.SelectedValue);
        cmdStudent.Parameters.Add("@Semester", drpSemesterD.SelectedValue);
        cmdStudent.Parameters.Add("@Section", drpSectionD.SelectedValue);
        cmdStudent.Parameters.Add("@SubjectCode", drpSubjectD.SelectedValue);
        cmdStudent.Parameters.Add("@LectureNo", drpLectureD.SelectedValue);
        cmdStudent.Parameters.Add("@Group", ddlGroupD.SelectedValue);  //----Add Group on 25-08-2016 by ashu
        cmdStudent.Parameters.Add("@Batch", ddlBatchD.SelectedValue); //----Add Batch on 25-08-2016 by ashu
        cmdStudent.Parameters.Add("@AcademicYear", drpAcademicYearD.SelectedValue);
        SqlDataAdapter daStudent = new SqlDataAdapter(cmdStudent);
        //DataTable dtStudent = new DataTable();

        SqlDataAdapter da = new SqlDataAdapter(cmdStudent);

        da.Fill(dtStudent);

        cntD = dtStudent.Rows.Count;
        grdStudentAttendanceD.DataSource = dtStudent;
        grdStudentAttendanceD.DataBind();

        if (dtStudent.Rows.Count > 0)
        {

            btnSubmitD.Visible = true;

        }

        if (grdStudentAttendanceD.Rows.Count > 0)
        {
            EnableDisableD(false);
        }
        else
        { EnableDisableD(true); }
        return dtStudent;
    }
    public void EnableDisableD(bool TF)
    {
        txtDateD.Enabled = TF;

    }
    public void BlankD()
    {
        EnableDisableD(true);
        btnSubmitD.Visible = false;
        // txtTopic.Text = "";
    }

    public string InsertStudentAttendanceHeaderAndLineD(string DocumentNo, string AcademicYear, int Hour, string Date, string FacultyCode, int AttendanceType, string Unit, string Topic, string CollegeCode, DataTable dt)
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
        catch (Exception ex) { Result = "Error"; }
        finally
        {
            con.Close();
        }
        return Result;
    }
    public void BindTableD()
    {

        for (int i = 0; i < grdStudentAttendanceD.Columns.Count; i++)
        {
            // dt12.Columns.Add(grdStudentAttendance.HeaderRow.Cells[i].Text);            
            dt12.Columns.Add(grdStudentAttendanceD.Columns[i].HeaderText);
        }

        dt12.Columns[11].ColumnName = "SubjectType";
        dt12.Columns[12].ColumnName = "StudentNo";
        int r = 0;
        foreach (GridViewRow row in grdStudentAttendanceD.Rows)
        {
            DataRow dr = dt12.NewRow();
            for (int j = 0; j < grdStudentAttendanceD.Columns.Count; j++)
            {
                CheckBox rb = (CheckBox)row.FindControl("chkboxAttendanceD");
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
                    dr[grdStudentAttendanceD.Columns[j].HeaderText] = row.Cells[j].Text;
                }
            }
            HiddenField hfSubjectTypeD = (HiddenField)row.FindControl("hfSubjectTypeD");
            HiddenField hfStudentNoD = (HiddenField)row.FindControl("hfStudentNoD");
            dt12.Rows.Add(dr);
            dt12.Rows[r]["SubjectType"] = hfSubjectTypeD.Value;
            dt12.Rows[r]["StudentNo"] = hfStudentNoD.Value;
            r = r + 1;
        }



    }

    protected void drpAcademicYearD_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGetSubjectListD();
    }

    protected void drpCourseD_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterListD();
    }

    protected void drpSemesterD_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSectionListD();
        bindGetSubjectListD();
        bindGroupListD();// add by ashu on 25-08-2016
        bindBatchListD();// add by ashu on 25-08-2016

        // ShowRemedialClass();
    }

    protected void drpSectionD_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGroupListD();
        bindBatchListD();
    }
    protected void ddlGroupD_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGetSubjectListD();
    }
    protected void ddlBatchD_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGetSubjectListD();
    }
    protected void drpSubjectD_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSubjectTypeD.Text = FDL.GetSubjectTypebyCourseSubject(drpCourseD.SelectedValue, drpSubjectD.SelectedValue);

        bindLectureD();
        bindUnitD();
    }
    protected void drpLectureD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnShowD_Click(object sender, EventArgs e)
    {
        bindStudentGridD();
        SaveD = true;
    }
    protected void btnSave2D_Click(object sender, EventArgs e)
    {
        string Result = "";
        BindTableD();
        if (drpUnitD.SelectedValue == "")
        {

        }
        else
        {
            if (SaveD == true)
            {
                Result = InsertStudentAttendanceHeaderAndLineD("", drpAcademicYearD.SelectedValue, Convert.ToInt16(drpLectureD.SelectedValue), txtDateD.Text, lblFacultyCodeD.Text, 0, drpUnitD.SelectedValue, txtTopicD.Text, Session["GlobalDimension1Code"].ToString(), dt12);  //ASHU---25-08-2016  
                if (Result != "Error")
                {
                    lblNoD.Text = FDL.GetNextStudentMarkAttendanceNumber();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);

                    bindStudentGridD();
                    BlankD();
                    lblMessageD.Visible = false;
                    btnSubmitD.Enabled = true;
                    btnSaveD.Enabled = true;
                    SaveD = false;
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

    protected void txtDateD_TextChanged(object sender, EventArgs e)
    {
        bindDrpCourseListD();  //added on 24 feb 2017
        bindLectureD();
        bindGetSubjectListD();
    }

    protected void grdStudentAttendanceD_SelectedIndexChanged(object sender, EventArgs e)
    {
        No_D = grdStudentAttendanceD.SelectedDataKey.Value.ToString();
        // BindAttendanceSummaryGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal3').modal();", true);
    }
    protected void chkboxEditAttendance_CheckedChanged(object sender, EventArgs e)
    {
        if (chkboxEditAttendance.Checked == true)
        {
            for (int i = 0; i < cnt; i++)
            {
                CheckBox col = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox1stAttendance");
                CheckBox col1 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox2ndAttendance");
                CheckBox col2 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox3rdAttendance");
                col.Enabled = true;
                col1.Enabled = true;
                col2.Enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < cnt; i++)
            {
                CheckBox col = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox1stAttendance");
                CheckBox col1 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox2ndAttendance");
                CheckBox col2 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox3rdAttendance");
                col.Enabled = false;
                col1.Enabled = false;
                col2.Enabled = false;
            }
        }
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
    DataTable dt12 = new DataTable();
    public void bindGrid()
    {
        string remedial = "";
        if (chkBoxExtraClass.Checked == true)
        {
            remedial = "1";
        }
        if (chkBoxExtraClass.Checked == false)
        {
            remedial = "0";
        }
        DateTime dt11 = new DateTime();
        dt11 = Convert.ToDateTime(txtDate.Text);
        string date = dt11.ToString("MM-dd-yyyy");

        SqlCommand cmd1 = new SqlCommand("proc_GetStudentDetailsForMarkAttendance1_", con);  //ok on live
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd1.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd1.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd1.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd1.Parameters.Add("@LectureNo", drpLecture.SelectedItem.ToString());
        cmd1.Parameters.Add("@Date", date);
        cmd1.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd1.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd1.Parameters.Add("@Group", ddlGroup.SelectedValue);  //----Add Group on 25-08-2016 by ashu
        cmd1.Parameters.Add("@Batch", ddlBatch.SelectedValue); //----Add Batch on 25-08-2016 by ashu
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds = new DataSet();
        da1.Fill(ds);
        Session["Unit"] = drpUnit.SelectedValue;
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMessage.Visible = true;
            pnlGrid.Visible = false;
            btnSubmit.Visible = false;
            pnlCheckBox.Visible = false;
        }
        else
        {
            System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
            if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
            {
                string Entry_No = "";
                Session["Date"] = txtDate.Text;
                //if (ChkNr.Checked == true)
                //{
                //    Session["NR"] = 1;
                //}
                //else {
                //    Session["NR"] = 0;
                //}
                Entry_No = ds.Tables[1].Rows[0]["Entry No_"].ToString();

                Response.Redirect("MarkAttendanceMobile.aspx?EntryNo=" + Entry_No + "");
            }
            else
            {
                pnlGrid.Visible = true;
                pnlCheckBox.Visible = true;
                btnSubmit.Visible = true;
                lblMessage.Visible = false;
                SqlCommand cmd = new SqlCommand("[proc_GetPreviousAttendanceOfStudent2_shubham]", con); string st = ""; //ok on live//[proc_GetPreviousAttendanceOfStudent2_Combined]proc_GetPreviousAttendanceOfStudent2_
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
                cmd.Parameters.Add("@Section", drpSection.SelectedValue);
                cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
                cmd.Parameters.Add("@LectureNo", drpLecture.SelectedValue);
                cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);  //----Add Group on 25-08-2016 by ashu
                cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue); //----Add Batch on 25-08-2016 by ashu
                cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue); //----Add Batch on 25-08-2016 by ashu
                cmd.Parameters.Add("@Remedial", remedial.Trim());
                if(divAdmissionPeriod.Visible==true)
                {
                    
                    cmd.Parameters.Add("@AdmissionPeriod", drpAdmissionPeriod.SelectedValue);
                }
                else
                {
                    cmd.Parameters.Add("@AdmissionPeriod", "");
                }
                cmd.CommandTimeout = 500000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex) { }
                cnt = dt.Rows.Count;
                grdAttendanceDetails.DataSource = dt;
                grdAttendanceDetails.DataBind();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        CheckBox rb = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox1stAttendance");
                        if (dt.Rows[i]["AAfterPrev"].ToString() == "0")
                        {
                            rb.Checked = true;
                        }
                        else
                            rb.Checked = false;

                        CheckBox rb1 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox2ndAttendance");
                        if (dt.Rows[i]["AfterPrev"].ToString() == "0")
                        {
                            rb1.Checked = true;
                        }
                        else
                            rb1.Checked = false;

                        CheckBox rb2 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox3rdAttendance");
                        if (dt.Rows[i]["prev"].ToString() == "0")
                        {
                            rb2.Checked = true;
                        }
                        else
                            rb2.Checked = false;
                    }
                    SqlCommand cmd2 = new SqlCommand("proc_GetDateAndHourForMarkAtt_Combined", con);//ok  proc_GetDateAndHourForMarkAtt_    //proc_GetDateAndHourForMarkAtt_Combined shubham sharma
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                    cmd2.Parameters.Add("@Semester", drpSemester.SelectedValue);
                    cmd2.Parameters.Add("@Section", drpSection.SelectedValue);
                    cmd2.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                    cmd2.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                    cmd2.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
                    cmd2.Parameters.Add("@LectureNo", drpLecture.SelectedValue);
                    cmd2.Parameters.Add("@Group", ddlGroup.SelectedValue);  //----Add Group on 25-08-2016 by ashu
                    cmd2.Parameters.Add("@Batch", ddlBatch.SelectedValue); //----Add Batch on 25-08-2016 by ashu
                    cmd2.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue); //----Add Batch on 28-08-2016 by ashu
                    cmd2.Parameters.Add("@Remedial", remedial.Trim());
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    da2.Fill(dt2);
                    int j = 5;
                    if (dt2.Rows.Count > 0)
                    {
                        for (int g = 0; g < dt2.Rows.Count; g++)
                        {
                            if (j > 2)
                            {
                                grdAttendanceDetails.HeaderRow.Cells[j].Text = dt2.Rows[g]["Date"].ToString() + " / L" + dt2.Rows[g]["HourNo"].ToString();
                                j--;
                            }
                        }
                    }
                }
            }
            if (grdAttendanceDetails.Rows.Count > 0)
            {
                EnableDisable(false);
            }
            else
            { EnableDisable(true); }
        }
    }
    public void bindGridOpen()
    {
        string remedial = "";
        if (chkBoxExtraClass.Checked == true)
        {
            remedial = "1";
        }
        if (chkBoxExtraClass.Checked == false)
        {
            remedial = "0";
        }
        DateTime dt11 = new DateTime();
        dt11 = Convert.ToDateTime(txtDate1.Text);
        string date = dt11.ToString("MM-dd-yyyy");

        SqlCommand cmd1 = new SqlCommand("proc_GetStudentDetailsForMarkAttendanceOpen", con);  //ok on live
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@FacultyCode", txtStaff1.Text);

        cmd1.Parameters.Add("@LectureNo", drpLecture1.SelectedItem.ToString());
        cmd1.Parameters.Add("@Date", date);
        cmd1.Parameters.Add("@SubjectCode", drpSubject1.SelectedValue);
        cmd1.Parameters.Add("@AcademicYear", drpAcademic1.SelectedValue);
        if (drpSection1.SelectedIndex == 0)
        {
            cmd1.Parameters.Add("@Section", "");
        }
        else
        {
            cmd1.Parameters.Add("@Section", drpSection1.SelectedValue);
        }
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds = new DataSet();
        da1.Fill(ds);
        Session["Unit"] = drpUnit.SelectedValue;
        if (ds.Tables[0].Rows.Count > 0)
        {
            Label2.Visible = true;
            pnlGrid.Visible = false;
            lnkSubmitOpen.Visible = false;
            Panel2.Visible = false;
        }
        else
        {
            System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
            if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
            {
                string Entry_No = "";
                Session["Date"] = txtDate.Text;
                //if (ChkNr.Checked == true)
                //{
                //    Session["NR"] = 1;
                //}
                //else {
                //    Session["NR"] = 0;
                //}
                Entry_No = ds.Tables[1].Rows[0]["Entry No_"].ToString();

                Response.Redirect("MarkAttendanceMobile.aspx?EntryNo=" + Entry_No + "");
            }
            else
            {
                pnlGrid.Visible = true;
                Panel2.Visible = true;
                lnkSubmitOpen.Visible = true;
                lblMessage.Visible = false;
                SqlCommand cmd = new SqlCommand("[proc_GetPreviousAttendanceOfStudent2_Open]", con); string st = ""; //ok on live//[proc_GetPreviousAttendanceOfStudent2_Combined]proc_GetPreviousAttendanceOfStudent2_
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                cmd.Parameters.Add("@SubjectCode", drpSubject1.SelectedValue);
                cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate1.Text));
                cmd.Parameters.Add("@LectureNo", drpLecture1.SelectedValue);

                cmd.Parameters.Add("@AcademicYear", drpAcademic1.SelectedValue); //----Add Batch on 25-08-2016 by ashu

                cmd.Parameters.Add("@Remedial", remedial.Trim());
                cmd.Parameters.Add("@Section", drpSection1.SelectedValue);
                cmd.CommandTimeout = 500000;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex) { }
                cntOpen = dt.Rows.Count;
                Panel3.Visible = true;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        CheckBox rb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkbox1stAttendance");
                        if (dt.Rows[i]["AAfterPrev"].ToString() == "0")
                        {
                            rb.Checked = true;
                        }
                        else
                            rb.Checked = false;

                        CheckBox rb1 = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkbox2ndAttendance");
                        if (dt.Rows[i]["AfterPrev"].ToString() == "0")
                        {
                            rb1.Checked = true;
                        }
                        else
                            rb1.Checked = false;

                        CheckBox rb2 = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkbox3rdAttendance");
                        if (dt.Rows[i]["prev"].ToString() == "0")
                        {
                            rb2.Checked = true;
                        }
                        else
                            rb2.Checked = false;
                    }
                    SqlCommand cmd2 = new SqlCommand("proc_GetDateAndHourForMarkAtt_CombinedOpen", con);//ok  proc_GetDateAndHourForMarkAtt_    //proc_GetDateAndHourForMarkAtt_Combined shubham sharma
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                    cmd2.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                    cmd2.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
                    cmd2.Parameters.Add("@LectureNo", drpLecture.SelectedValue);

                    cmd2.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue); //----Add Batch on 28-08-2016 by ashu
                    cmd2.Parameters.Add("@Remedial", remedial.Trim());
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    da2.Fill(dt2);
                    int j = 5;
                    if (dt2.Rows.Count > 0)
                    {
                        for (int g = 0; g < dt2.Rows.Count; g++)
                        {
                            if (j > 2)
                            {
                                GridView1.HeaderRow.Cells[j].Text = dt2.Rows[g]["Date"].ToString() + " / L" + dt2.Rows[g]["HourNo"].ToString();
                                j--;
                            }
                        }
                    }
                }
            }
            if (GridView1.Rows.Count > 0)
            {

                EnableDisableOpen(false);
            }
            else
            { EnableDisableOpen(true); }
        }
    }
    public void BindTableOpen()
    {
        for (int i = 0; i < GridView1.Columns.Count; i++)
        {
            if (i == 3)
                dt12.Columns.Add("L1");
            else if (i == 4)
                dt12.Columns.Add("L2");
            else if (i == 5)
                dt12.Columns.Add("L3");
            else
                dt12.Columns.Add(GridView1.HeaderRow.Cells[i].Text);
        }
        foreach (GridViewRow row in GridView1.Rows)
        {
            DataRow dr = dt12.NewRow();
            for (int j = 0; j < GridView1.Columns.Count; j++)
            {
                CheckBox rb = (CheckBox)row.FindControl("chkboxAttendance");
                if (j == 6)
                {
                    if (rb.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                CheckBox rb1 = (CheckBox)row.FindControl("chkbox1stAttendance");
                if (j == 3)
                {
                    if (rb1.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                CheckBox rb2 = (CheckBox)row.FindControl("chkbox2ndAttendance");
                if (j == 4)
                {
                    if (rb2.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                CheckBox rb3 = (CheckBox)row.FindControl("chkbox3rdAttendance");
                if (j == 5)
                {
                    if (rb3.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                if (j == 3)
                    dr["L1"] = row.Cells[j].Text;
                else if (j == 4)
                    dr["L2"] = row.Cells[j].Text;
                else if (j == 5)
                    dr["L3"] = row.Cells[j].Text;
                else
                    dr[GridView1.HeaderRow.Cells[j].Text] = row.Cells[j].Text;
            }
            dt12.Rows.Add(dr);
        }
    }
    public void BindTable()
    {
        for (int i = 0; i < grdAttendanceDetails.Columns.Count; i++)
        {
            if (i == 3)
                dt12.Columns.Add("L1");
            else if (i == 4)
                dt12.Columns.Add("L2");
            else if (i == 5)
                dt12.Columns.Add("L3");
            else
                dt12.Columns.Add(grdAttendanceDetails.HeaderRow.Cells[i].Text);
        }
        foreach (GridViewRow row in grdAttendanceDetails.Rows)
        {
            DataRow dr = dt12.NewRow();
            for (int j = 0; j < grdAttendanceDetails.Columns.Count; j++)
            {
                CheckBox rb = (CheckBox)row.FindControl("chkboxAttendance");
                if (j == 6)
                {
                    if (rb.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                CheckBox rb1 = (CheckBox)row.FindControl("chkbox1stAttendance");
                if (j == 3)
                {
                    if (rb1.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                CheckBox rb2 = (CheckBox)row.FindControl("chkbox2ndAttendance");
                if (j == 4)
                {
                    if (rb2.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                CheckBox rb3 = (CheckBox)row.FindControl("chkbox3rdAttendance");
                if (j == 5)
                {
                    if (rb3.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                if (j == 3)
                    dr["L1"] = row.Cells[j].Text;
                else if (j == 4)
                    dr["L2"] = row.Cells[j].Text;
                else if (j == 5)
                    dr["L3"] = row.Cells[j].Text;
                else
                    dr[grdAttendanceDetails.HeaderRow.Cells[j].Text] = row.Cells[j].Text;
            }
            dt12.Rows.Add(dr);
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {



        //SqlCommand cmd1 = new SqlCommand("sp_checkDetanieeListBlank", con1);
        //cmd1.CommandType = CommandType.StoredProcedure;
        //cmd1.Parameters.AddWithValue("@CourseCode", DrpCourse.SelectedValue);
        //cmd1.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
        //cmd1.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        //cmd1.Parameters.AddWithValue("@SemYear", drpSemester.SelectedValue);
        //cmd1.Parameters.AddWithValue("@SubjectCode", ddlSubject.SelectedValue);
        //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //DataTable dt1 = new DataTable();
        //con1.Open();
        //da1.Fill(dt1);
        //con1.Close();
        //if (dt1.Rows.Count > 0)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Detainee Report Already  Submitted')", true);
        //}


        //------------------------------04-12-2016----------------------

        DataTable dt = new DataTable();
        dt = (DataTable)Session["RemedialHourData"];
        DataView dv = new DataView(dt);
        dv.RowFilter = "Details ='" + drpLecture.SelectedValue.Trim() + "'";
        //dv.RowFilter = "Details=8";

        // int val = (int)dv[0]["Remedial"].ToString();

        if (dv[0]["Remedial"].ToString() == "1")
        {
            chkBoxExtraClass.Checked = true;
            chkBoxExtraClass.Enabled = false;
        }
        else
        {
            chkBoxExtraClass.Checked = false;
            chkBoxExtraClass.Enabled = false;

        }
        //chkMultiplaeAttendance.Visible = true; //All subject  // 11 nov 2016 

        //}
        chkboxEditAttendance.Checked = false;

        bindGrid();
        // Save = true;
        if (dv[0]["Remedial"].ToString() == "1")
        {
            grdAttendanceDetails.Columns[7].Visible = true;//1 jan2019 as per by subham gupta
        }

        if (dv[0]["Remedial"].ToString() == "0")
        {
            grdAttendanceDetails.Columns[7].Visible = true;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "Disable();", true);
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FDL.GetSubjectTypebySemester(drpCourse.SelectedValue, drpSemester.SelectedItem.ToString(),drpSubject.SelectedItem.ToString());//comment on 29 aug 2016
        lblSubjectType.Text = FDL.GetSubjectTypebyCourseSubject(drpCourse.SelectedValue, drpSubject.SelectedValue);

        bindLecture();
        bindUnit();


    }
    public void Blank()
    {
        EnableDisable(true);
        drpCourse.SelectedIndex = 0;
        lblSubjectType.Text = "";
        drpSubject.DataSource = "";
        drpSubject.DataBind();
        drpSemester.DataSource = "";
        drpSemester.DataBind();
        drpSection.DataSource = "";
        drpSection.DataBind();
        drpLecture.DataSource = "";
        drpLecture.DataBind();
        drpUnit.DataSource = "";
        drpUnit.DataBind();
        pnlGrid.Visible = false;
        pnlCheckBox.Visible = false;
        btnSubmit.Visible = false;
        chkPresentAll.Checked = true;
        chkAbsentAll.Checked = false;
        txtTopic.Text = "";
    }
    public void BlankOpen()
    {
        EnableDisableOpen(true);

        txtSubjectType1.Text = "";
        drpSubject1.DataSource = "";
        drpSubject1.DataBind();


        drpLecture1.DataSource = "";
        drpLecture1.DataBind();
        drpUnit1.DataSource = "";
        drpUnit1.DataBind();
        Panel3.Visible = false;
        Panel2.Visible = false;
        lnkSubmitOpen.Visible = false;
        CheckBox2.Checked = true;
        CheckBox3.Checked = false;
        txttopic1.Text = "";
    }
    public void bindLecture()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetLectureFromTimeTable_Combined", con);//proc_GetLectureFromTimeTable by shubham sharma
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
        Session["RemedialHourData"] = dt;
        Boolean Rem = false, Gen = false;
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            if (dt.Rows[i]["Remedial"].ToString() == "1")
            {

                Rem = true;
            }
            if (dt.Rows[i]["Remedial"].ToString() == "0")
            {
                //chkBoxExtraClass.Enabled = false;
                //chkBoxExtraClass.Checked = false;
                Gen = true;
            }
        }
        if (Rem == true && Gen == true)
        {
            chkBoxExtraClass.Enabled = true;
            chkBoxExtraClass.Checked = false;
        }
        if (Rem == true && Gen != true)
        {
            chkBoxExtraClass.Enabled = false;
            chkBoxExtraClass.Checked = true;
        }
        if (Rem != true && Gen == true)
        {
            chkBoxExtraClass.Enabled = false;
            chkBoxExtraClass.Checked = false;
        }


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
    public void bindGetSubjectList1()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromCourseSubjectLine1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject1.DataSource = dt;
        drpSubject1.DataTextField = "Details";
        drpSubject1.DataValueField = "No_";
        drpSubject1.DataBind();

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
        drpAcademic1.DataSource = dt1;
        drpAcademic1.DataTextField = "Details";
        drpAcademic1.DataValueField = "No_";
        drpAcademic1.DataBind();
    }
    public void UpdateAttendance()
    {
        int count1 = dt2.Rows.Count > 3 ? 3 : dt2.Rows.Count;

        for (int i = 0; i < count1 && i < 3; i++)
        {
            string DocumentNo = FDL.GetNextStudentMarkAttendanceNumber();
            int AttendanceType = 0;
            if (chkBoxExtraClass.Checked == true)
                AttendanceType = 1;
            SqlCommand cmd1 = new SqlCommand("proc_GetStudentDetailsForUpdateAttendance_", con); //ok
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd1.Parameters.Add("@Semesteryear", drpSemester.SelectedValue);
            cmd1.Parameters.Add("@Section", drpSection.SelectedValue);
            cmd1.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
            cmd1.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
            cmd1.Parameters.Add("@Date", Convert.ToDateTime(dt2.Rows[i]["Date"]).ToString("MM-dd-yyyy"));
            cmd1.Parameters.Add("@LectureNo", dt2.Rows[i]["HourNo"].ToString());
            cmd1.Parameters.AddWithValue("@Group", ddlGroup.SelectedValue);//ashu 25-08-2016
            cmd1.Parameters.AddWithValue("@Batch", ddlBatch.SelectedValue);//ashu 25-08-2016
            cmd1.CommandTimeout = 300;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);


            if (dt1.Rows.Count == 0)   //Add by ashu on 26-08-2016
            {
                if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("[dbo].[proc_insertStudentAttendanceHeaderAndLine_byTable_EditAttendanve]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
                cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
                cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
                cmd.Parameters.AddWithValue("@Group", ddlGroup.SelectedValue);
                cmd.Parameters.AddWithValue("@Batch", ddlBatch.SelectedValue);
                cmd.Parameters.AddWithValue("@SubjectCode", drpSubject.SelectedValue);
                cmd.Parameters.AddWithValue("@SubjectType", lblSubjectType.Text);
                cmd.Parameters.AddWithValue("@Hour", dt2.Rows[i]["HourNo"].ToString());
                cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()));
                cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@AttendanceType", AttendanceType);
                cmd.Parameters.AddWithValue("@Unit", drpUnit.SelectedValue);
                cmd.Parameters.AddWithValue("@Topic", txtTopic.Text);
                cmd.Parameters.AddWithValue("@UpdateColumn", i);
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@tblAttendance", dt12);
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand("proc_UpdateStudentAttendance_ByTable", con); //ok
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Date", Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()));
                cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                cmd.Parameters.Add("@Section", drpSection.SelectedValue);
                cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
                cmd.Parameters.Add("@Hour", dt2.Rows[i]["HourNo"].ToString());
                cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                cmd.Parameters.AddWithValue("@Group", ddlGroup.SelectedValue);//ashu 25-08-2016
                cmd.Parameters.AddWithValue("@Batch", ddlBatch.SelectedValue);//ashu 25-08-2016
                cmd.Parameters.AddWithValue("@UpdateColumn", i);
                cmd.Parameters.AddWithValue("@tblAttendance", dt12);
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
    public void bindGridHeaderOpen()
    {
        string remedial = "";
        if (chkRemedial1.Checked == true)
        {
            remedial = "1";
        }
        if (chkRemedial1.Checked == false)
        {
            remedial = "0";
        }
        SqlCommand cmd2 = new SqlCommand("proc_GetDateAndHourForMarkAttOpen", con); //ok
        cmd2.CommandType = CommandType.StoredProcedure;

        cmd2.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd2.Parameters.Add("@SubjectCode", drpSubject1.SelectedValue);
        cmd2.Parameters.Add("@Date", Convert.ToDateTime(txtDate1.Text));
        cmd2.Parameters.Add("@LectureNo", drpLecture1.SelectedValue);

        cmd2.Parameters.Add("@AcademicYear", drpAcademic1.SelectedValue); //----Add Academic Year on 30-08-2016 by ashu
        cmd2.Parameters.Add("@Remedial", remedial);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        da2.Fill(dt2);
        int j = 5;
        if (dt2.Rows.Count > 0)
        {
            for (int g = 0; g < dt2.Rows.Count; g++)
            {
                if (j > 2)
                {
                    GridView1.HeaderRow.Cells[j].Text = dt2.Rows[g]["Date"].ToString() + " / L" + dt2.Rows[g]["HourNo"].ToString();
                    j--;
                }
            }
        }
    }
    public void bindGridHeader()
    {
        string remedial = "";
        if (chkBoxExtraClass.Checked == true)
        {
            remedial = "1";
        }
        if (chkBoxExtraClass.Checked == false)
        {
            remedial = "0";
        }
        SqlCommand cmd2 = new SqlCommand("proc_GetDateAndHourForMarkAtt_", con); //ok
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd2.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd2.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd2.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd2.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd2.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd2.Parameters.Add("@LectureNo", drpLecture.SelectedValue);
        cmd2.Parameters.Add("@Group", ddlGroup.SelectedValue);  //----Add Group on 25-08-2016 by ashu
        cmd2.Parameters.Add("@Batch", ddlBatch.SelectedValue); //----Add Batch on 25-08-2016 by ashu
        cmd2.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue); //----Add Academic Year on 30-08-2016 by ashu
        cmd2.Parameters.Add("@Remedial", remedial);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        da2.Fill(dt2);
        int j = 5;
        if (dt2.Rows.Count > 0)
        {
            for (int g = 0; g < dt2.Rows.Count; g++)
            {
                if (j > 2)
                {
                    grdAttendanceDetails.HeaderRow.Cells[j].Text = dt2.Rows[g]["Date"].ToString() + " / L" + dt2.Rows[g]["HourNo"].ToString();
                    j--;
                }
            }
        }
    }
    protected void btnSave3_Click(object sender, EventArgs e)
    {
        //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
       // {
            string Step = "Step1";
            try
            {

                btnSave5.Enabled = false;
                bindGridHeaderOpen();
                Step = "Step2";  ////////////////////////////////
                btnSubmit.Enabled = false;
                BindTableOpen();
                Step = "Step3";   /////////////////////////////////////////////////
                if (CheckBox4.Checked == true)
                {
                    UpdateAttendance();
                }
                int AttendanceType = 0;
                if (chkRemedial1.Checked == true)
                    AttendanceType = 1;

                int LectureNo = Convert.ToInt32(drpLecture1.Text);
                int j = 1;
                if (CheckBox5.Checked == true)
                {
                    j = 1;
                }
                else
                {
                    j = LectureNo;
                }
                Step = "Step4";
                for (int i = j; i <= LectureNo; i++)
                {
                    string v = i.ToString();
                    ListItem selectedListItem = drpLecture1.Items.FindByValue(v);

                    if (selectedListItem != null)
                    {
                        InsertStudentAttendanceHeaderAndLineOpen(txtNo1.Text, drpAcademic1.SelectedValue, "", "", "", "", "", drpSubject1.SelectedValue, txtSubjectType1.Text, i, txtDate1.Text, txtStaff1.Text, AttendanceType, drpUnit1.SelectedValue, txttopic1.Text, Session["GlobalDimension1Code"].ToString(), dt12);
                    }
                    Step = "Step5";
                }
                txtNo1.Text = FDL.GetNextStudentMarkAttendanceNumber();
                lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
                Step = "Step6";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);

                BlankOpen();
                Label2.Visible = false;
                lnkSubmitOpen.Enabled = true;
                btnSave5.Enabled = true;

                System.Threading.Thread.Sleep(2000);
               // scope.Complete();
            }
            catch (Exception ex)
            {
                //scope.Dispose();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Contact to admin for " + Step + "');", true);
                 ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Network error due to high traffic,Please try after some time.');", true);
            }

       // }


    }

    public string InsertStudentAttendanceHeaderAndLineOpen(string DocumentNo, string AcademicYear, string CourseCode, string Semester, string Section, string Group, string Batch, string SubjectCode, string SubjectType, int Hour, string Date, string FacultyCode, int AttendanceType, string Unit, string Topic, string CollegeCode, DataTable dt)
    {
        string Result = "";
        if (con.State == ConnectionState.Closed)
            con.Open();
        //try
        //{
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            SqlCommand cmd = new SqlCommand("[dbo].[proc_insertStudentAttendanceHeaderAndLineOpen]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
            cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
            cmd.Parameters.AddWithValue("@Course", CourseCode);
            cmd.Parameters.AddWithValue("@Semester", Semester);
            cmd.Parameters.AddWithValue("@Section", Section);
            cmd.Parameters.AddWithValue("@Group", Group);
            cmd.Parameters.AddWithValue("@Batch", Batch);
            cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode);
            cmd.Parameters.AddWithValue("@SubjectType", SubjectType);
            cmd.Parameters.AddWithValue("@Hour", Hour);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@FacultyCode", FacultyCode);
            cmd.Parameters.AddWithValue("@AttendanceType", AttendanceType);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@Topic", Topic);
            cmd.Parameters.AddWithValue("@CollegeCode", CollegeCode);

            cmd.Parameters.AddWithValue("@EnrollmentNo", "ex");
            cmd.Parameters.AddWithValue("@StudentName", dt.Rows[i]["Student Name"]);
            cmd.Parameters.AddWithValue("@StudentNo", dt.Rows[i]["Student No"]);
            cmd.Parameters.AddWithValue("@Attendance", dt.Rows[i]["Today"]);// attendance absent/persent  
            if (Chkdetained.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Detained", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Detained", 0);
            }
            cmd.CommandTimeout = 300;
            cmd.ExecuteNonQuery();
        }
        //}
        //catch { Result = "Error"; }
        //finally
        //{
        //    con.Close();                
        //}
        return Result;
    }

    protected void btnSave1_Click(object sender, EventArgs e)
    {
       // using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
      //  {
            string Step = "Step1";
            try
            {

                btnSave1.Enabled = false;
                bindGridHeader();
                Step = "Step2";
                btnSubmit.Enabled = false;
                BindTable();
                Step = "Step3";
                if (chkboxEditAttendance.Checked == true)
                {
                    UpdateAttendance();
                }
                int AttendanceType = 0;
                if (chkBoxExtraClass.Checked == true)
                    AttendanceType = 1;

                int LectureNo = Convert.ToInt32(drpLecture.Text);
                int j = 1;
                if (chkMultiplaeAttendance.Checked == true)
                {
                    j = 1;
                }
                else
                {
                    j = LectureNo;
                }
                Step = "Step4";    //////////////////////////////////////////
                for (int i = j; i <= LectureNo; i++)
                {
                    string v = i.ToString();
                    ListItem selectedListItem = drpLecture.Items.FindByValue(v);

                    if (selectedListItem != null)
                    {
                        FDL.InsertStudentAttendanceHeaderAndLine(drpAcademicYear.SelectedValue, drpCourse.SelectedValue, drpSemester.SelectedItem.ToString(), drpSection.SelectedValue, ddlGroup.SelectedValue, ddlBatch.SelectedValue, drpSubject.SelectedValue, lblSubjectType.Text, i, txtDate.Text, lblFacultyCode.Text, AttendanceType, drpUnit.SelectedValue, txtTopic.Text, Session["GlobalDimension1Code"].ToString(), dt12);
                    }
                    Step = "Step5";   //////////////////////////////////////////////////////////
                }
                lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
                Step = "Step6";   //////////////////////////////////////////////////////////
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);

                Blank();
                lblMessage.Visible = false;
                btnSubmit.Enabled = true;
                btnSave1.Enabled = true;

                System.Threading.Thread.Sleep(2000);

               // scope.Complete();

            }


            catch (Exception ex)
            {
              //  scope.Dispose();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Contact to admin for " + Step + "');", true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Network error due to high traffic,Please try after some time.');", true);

            }
      //  }

    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGetSubjectList();
        //  ShowRemedialClass();
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        No_ = grdAttendanceDetails.SelectedDataKey.Value.ToString();
        BindAttendanceSummaryGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal3').modal();", true);
    }
    public void BindAttendanceSummaryGrid()
    {
        SqlCommand cmd = new SqlCommand("proc_getAttendanceRecordForMarkAttendance_", con); //ok
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StudentNo", No_);
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd.Parameters.AddWithValue("@Group", ddlGroup.SelectedValue);//ashu 25-08-2016
        cmd.Parameters.AddWithValue("@Batch", ddlBatch.SelectedValue);//ashu 25-08-2016
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAttendanceReport.DataSource = dt;
        grdAttendanceReport.DataBind();
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AttendanceView.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            grdAttendanceReport.AllowPaging = false;
            BindAttendanceSummaryGrid();
            grdAttendanceReport.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdAttendanceReport.HeaderRow.Cells)
            {
                cell.BackColor = grdAttendanceReport.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdAttendanceReport.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdAttendanceReport.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdAttendanceReport.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            grdAttendanceReport.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    public void EnableDisable(bool TF)
    {
        drpAcademicYear.Enabled = TF;
        drpCourse.Enabled = TF;
        drpSemester.Enabled = TF;
        drpSection.Enabled = TF;
        ddlGroup.Enabled = TF;
        ddlBatch.Enabled = TF;
        drpSubject.Enabled = TF;
        drpLecture.Enabled = TF;
        drpUnit.Enabled = TF;
        txtTopic.Enabled = TF;
    }
    public void EnableDisableOpen(bool TF)
    {
        drpAcademic1.Enabled = TF;


        drpSubject1.Enabled = TF;
        drpLecture1.Enabled = TF;
        drpUnit1.Enabled = TF;
        txttopic1.Enabled = TF;
    }
    protected void chkMultiplaeAttendance_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMultiplaeAttendance.Checked == true)
        {
            chkboxEditAttendance.Checked = false;
            for (int i = 0; i < cnt; i++)
            {
                CheckBox col = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox1stAttendance");
                CheckBox col1 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox2ndAttendance");
                CheckBox col2 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox3rdAttendance");
                col.Enabled = false;
                col1.Enabled = false;
                col2.Enabled = false;
            }
        }
    }
    public void ShowRemedialClass()
    {

        Connection con = new Connection();
        ServicePoratal PortalCon = new ServicePoratal();
        SqlDataReader dr = PortalCon.Show_RemedialDataforClassForHeader(Session["Company"].ToString().Trim(), drpCourse.SelectedValue.Trim(), drpSemester.SelectedValue.Trim(), drpSection.SelectedValue.Trim(), drpSubject.SelectedValue.Trim(), drpAcademicYear.SelectedValue.Trim(), Session["uid"].ToString(), txtDate.Text.Trim());
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            PortalCon.DisConnect();
            chkBoxExtraClass.Checked = true;
            chkBoxExtraClass.Enabled = false;
        }
        else
        {
            dr.Close();
            PortalCon.DisConnect();
            chkBoxExtraClass.Checked = false;
            chkBoxExtraClass.Enabled = false;

        }

        bindLecture();
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
    protected void btnSubmit_Click(object sender, EventArgs e)
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

    protected void Chkdetained_CheckedChanged(object sender, EventArgs e)
    {

        if (Chkdetained.Checked == true)
        {
            PnlDeatinedAttendence.Visible = true;
            PanelmainAttendence.Visible = false;
            Blank();
        }
        else
        {
            PnlDeatinedAttendence.Visible = false;
            PanelmainAttendence.Visible = true;
            BlankD();

        }
    }
    protected void txtDate1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnShow1_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["RemedialHourData"];
        DataView dv = new DataView(dt);
        dv.RowFilter = "Details ='" + drpLecture1.SelectedValue.Trim() + "'";
        //dv.RowFilter = "Details=8";

        // int val = (int)dv[0]["Remedial"].ToString();

        if (dv[0]["Remedial"].ToString() == "1")
        {
            chkRemedial1.Checked = true;
            chkRemedial1.Enabled = false;
        }
        else
        {
            chkRemedial1.Checked = false;
            chkRemedial1.Enabled = false;

        }
        //chkMultiplaeAttendance.Visible = true; //All subject  // 11 nov 2016 

        //}
        chkboxEditAttendance.Checked = false;

        bindGridOpen();
        // Save = true;
        if (dv[0]["Remedial"].ToString() == "1")
        {
            grdAttendanceDetails.Columns[7].Visible = true;//1 jan2019 as per by subham gupta
        }

        if (dv[0]["Remedial"].ToString() == "0")
        {
            grdAttendanceDetails.Columns[7].Visible = true;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "DisableOpen();", true);
    }


    protected void chkOpen_CheckedChanged(object sender, EventArgs e)
    {
        if (chkOpen.Checked == true)
        {

            PanelmainAttendence.Visible = false;
            pnlOpen.Visible = true;
            txtStaff1.Text = Session["uid"].ToString();
            // bindDrpCourseList();
            txtNo1.Text = FDL.GetNextStudentMarkAttendanceNumber();
            // txtDate1.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            txtDate1.Text = Session["Date"].ToString();
            txtDate1.Enabled = false;
            bindGetSubjectList1();
        }
        else
        {

            PanelmainAttendence.Visible = true;
            pnlOpen.Visible = false;
        }
    }
    public void bindLectureOpen()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetLectureFromTimeTable_CombinedOpen", con);//proc_GetLectureFromTimeTable by shubham sharma
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate1.Text));
        cmd.Parameters.Add("@FacultyCode", txtStaff1.Text);

        cmd.Parameters.Add("@AcademicYear", drpAcademic1.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject1.SelectedValue);
        if (drpSection1.SelectedIndex == 0)
        {
            cmd.Parameters.Add("@Section", "");
        }
        else
        {
            cmd.Parameters.Add("@Section", drpSection1.SelectedValue);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpLecture1.DataSource = dt;

        drpLecture1.DataTextField = "Details";
        drpLecture1.DataValueField = "No_";
        drpLecture1.DataBind();

        Session["RemedialHourData"] = dt;
        Boolean Rem = false, Gen = false;
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            if (dt.Rows[i]["Remedial"].ToString() == "1")
            {

                Rem = true;
            }
            if (dt.Rows[i]["Remedial"].ToString() == "0")
            {
                //chkBoxExtraClass.Enabled = false;
                //chkBoxExtraClass.Checked = false;
                Gen = true;
            }
        }
        if (Rem == true && Gen == true)
        {
            chkRemedial1.Enabled = true;
            chkRemedial1.Checked = false;
        }
        if (Rem == true && Gen != true)
        {
            chkRemedial1.Enabled = false;
            chkRemedial1.Checked = true;
        }
        if (Rem != true && Gen == true)
        {
            chkRemedial1.Enabled = false;
            chkRemedial1.Checked = false;
        }


    }
    public void bindUnitOpen()
    {
        //--------------------------05-10-2016----Free Room no---
        SqlCommand cmdCL = new SqlCommand("proc_GetSubjectClassificationOpen", con);
        cmdCL.CommandType = CommandType.StoredProcedure;
        cmdCL.Parameters.Add("@CourseCode", "");
        cmdCL.Parameters.Add("@Subject", drpSubject1.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmdCL);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        int Theory_count = 0;
        Theory_count = Convert.ToInt16(cmdCL.ExecuteScalar().ToString());
        txtSubjectType1.Text = dtCL.Rows[0]["Type"].ToString();
        con.Close();
        //-----------05-10-2016--Grou,Batch---by ashu--ENd

        DataTable dt = new DataTable();

        //string procName = "proc_GetUnitForMarkAttendance";
        //DataUtility objDut = new DataUtility();
        //DataTable dt = objDut.GetDataTableProc(procName, SubjectCode, AcademicYear, CourseCode);
        //return dt;

        SqlCommand cmdOpen = new SqlCommand("proc_GetUnitForMarkAttendanceOpen", con);
        cmdOpen.CommandType = CommandType.StoredProcedure;

        cmdOpen.Parameters.Add("@Subject", drpSubject1.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daOpen = new SqlDataAdapter(cmdOpen);
        DataTable dtOpen = new DataTable();
        daOpen.Fill(dtOpen);



        drpUnit1.DataSource = dtOpen;
        drpUnit1.DataTextField = "Details";
        drpUnit1.DataValueField = "No_";
        drpUnit1.DataBind();

        //-----------05-10-2016--Grou,Batch---by ashu --Start 
        if (Theory_count == 0 && dt.Rows.Count == 1)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("No_", typeof(string));
            dt1.Columns.Add("Details", typeof(string));
            dt1.Rows.Add("No Unit", "");
            drpUnit1.DataSource = dt1;
            drpUnit1.DataTextField = "Details";
            drpUnit1.DataValueField = "No_";
            drpUnit1.DataBind();
        }
        //-----------05-10-2016--Group,Batch---by ashu--ENd
    }
    protected void drpSubject1_SelectedIndexChanged(object sender, EventArgs e)
    {

        bindLectureOpen();
        bindUnitOpen();
        bindSectionListOpen();

    }
    public void bindSectionListOpen()
    {
        DataTable dt = new DataTable();
        //SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role1", con);
        SqlCommand cmd = new SqlCommand("proc_GetSectionFromTimeTable_RoleOpen", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd.Parameters.Add("@Academicyear", drpAcademic1.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject1.SelectedValue);
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate1.Text));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSection1.DataSource = dt;
        drpSection1.DataTextField = "Details";
        drpSection1.DataValueField = "No_";
        drpSection1.DataBind();

    }
    protected void drpLecture1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lnkSubmitOpen_Click(object sender, EventArgs e)
    {

    }
    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox5.Checked == true)
        {
            CheckBox5.Checked = false;
            for (int i = 0; i < cntOpen; i++)
            {
                CheckBox col = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkbox1stAttendance");
                CheckBox col1 = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkbox2ndAttendance");
                CheckBox col2 = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkbox3rdAttendance");
                col.Enabled = false;
                col1.Enabled = false;
                col2.Enabled = false;
            }
        }
    }
    protected void drpSection1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindLectureOpen();
    }
}