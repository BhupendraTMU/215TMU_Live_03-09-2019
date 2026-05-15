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


public partial class Faculty_ECAAttendance : System.Web.UI.Page
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

                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('access denied ?'); document.location.href='FacultyDetails.aspx';", true);

                lblFacultyCode.Text = Session["uid"].ToString();

                lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();

                txtDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");


                bindDrpCourseList();
                bindAcademicYear();



            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
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

        DateTime dt11 = new DateTime();
        dt11 = Convert.ToDateTime(txtDate.Text);
        string date = dt11.ToString("MM-dd-yyyy");



        pnlGrid.Visible = true;

        btnSubmit.Visible = true;
        lblMessage.Visible = false;
        SqlCommand cmd = new SqlCommand("[proc_GetPreviousAttendanceOfStudentECA]", con); string st = ""; //ok on live//[proc_GetPreviousAttendanceOfStudent2_Combined]proc_GetPreviousAttendanceOfStudent2_
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd.Parameters.Add("@LectureNo", drpLecture.SelectedValue);
        cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);
        cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue); //----Add Batch on 25-08-2016 by ashu

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

            SqlCommand cmd2 = new SqlCommand("proc_GetDateAndHourForMarkAtt_CombinedECA", con);//ok  proc_GetDateAndHourForMarkAtt_    //proc_GetDateAndHourForMarkAtt_Combined shubham sharma
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

            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            int j = 5;

        }

        if (grdAttendanceDetails.Rows.Count > 0)
        {
            EnableDisable(false);
        }
        else
        { EnableDisable(true); }

    }


    protected void btnShow_Click(object sender, EventArgs e)
    {

        bindGrid();


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

        btnSubmit.Visible = false;

        txtTopic.Text = "";
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
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);


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





    protected void btnSave1_Click(object sender, EventArgs e)
    {

        try
        {
            string DocNo = "";

            DataTable dt12 = new DataTable();
          
            for (int i = 0; i < grdAttendanceDetails.Columns.Count; i++)
            {
                dt12.Columns.Add(grdAttendanceDetails.HeaderRow.Cells[i].Text);
            }
            foreach (GridViewRow row in grdAttendanceDetails.Rows)
            {
                DocNo = (string)grdAttendanceDetails.DataKeys[0]["Document No_"];
                DataRow dr = dt12.NewRow();
                for (int j = 0; j < grdAttendanceDetails.Columns.Count; j++)
                {
                    CheckBox rb = (CheckBox)row.FindControl("chkboxAttendance");
                    if (j == 4)
                    {
                        if (rb.Checked == true)
                        {
                            row.Cells[j].Text = "0";
                        }
                        else
                        {
                            row.Cells[j].Text = "1";
                        }

                        dr[grdAttendanceDetails.HeaderRow.Cells[j].Text] = row.Cells[j].Text;
                    }
                    else if(j == 3)
                    {
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
                        dr[grdAttendanceDetails.HeaderRow.Cells[j].Text] = txtRemark.Text;
                    }

                    else
                    {
                        dr[grdAttendanceDetails.HeaderRow.Cells[j].Text] = row.Cells[j].Text;
                    }
                }
                dt12.Rows.Add(dr);
            }

            SqlCommand cmd = new SqlCommand("[dbo].[proc_updateStudentAttendanceLineECA]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DocumentNo", DocNo);
            cmd.Parameters.AddWithValue("@dtfinal", dt12);
            cmd.CommandTimeout = 300;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
            Blank();
            lblMessage.Visible = false;
            btnSubmit.Enabled = true;
            btnSave1.Enabled = true;

            System.Threading.Thread.Sleep(2000);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Something went wrong .Please contact to Admin');", true);
        }
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


    protected void txtDate1_TextChanged(object sender, EventArgs e)
    {

    }



}