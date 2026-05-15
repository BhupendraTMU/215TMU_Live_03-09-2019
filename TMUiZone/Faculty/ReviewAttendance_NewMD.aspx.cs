using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;

public partial class Faculty_ReviewAttendance_NewMD : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (!IsPostBack)
                {

                    bindAcademicYear();
                    BindCourse();


                }


            }
            catch
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }
    public void AccessRights()
    {
        SqlCommand cmd12 = new SqlCommand("sp_GetAccessForTimeTable_RoleMD", con);
        cmd12.CommandType = CommandType.StoredProcedure;
        cmd12.Parameters.Add("@userid", Session["uid"].ToString());
        cmd12.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
        cmd12.Parameters["@Return1"].Direction = ParameterDirection.Output;
        con.Open();
        cmd12.ExecuteNonQuery();

        con.Close();
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




    public void BindCourse()
    {


        SqlCommand cmd = new SqlCommand("[MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.Sp_GetCourseRoleWise_HOD_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@userid", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AcadmicYear", ddlAcademicYear.SelectedValue);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();

    }
    //-------------add by ashu--on 25-08-2016   
    public void BindSemester()
    {
        string FacultyCode = "";
        {
            FacultyCode = Session["uid"].ToString();
        }
        SqlCommand cmd = new SqlCommand("[MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.proc_GetSemester_Role", con); //proc_GetSemester_RoleMD
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }
    public void BindSection()
    {
        string FacultyCode = "";
        if (Session["UserGroup"].ToString() == "FACULTY") { FacultyCode = Session["uid"].ToString(); }
        SqlCommand cmd = new SqlCommand("proc_GetSectionReviewAtt_RoleMD", con); //proc_GetSection_RoleMD
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();
    }
    public void bindGroupList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetGroupFromCourseSemester_RoleMD", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
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
        SqlCommand cmd = new SqlCommand("proc_GetBatchFromCourseSemester_RoleMD", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlBatch.DataSource = dt;
        ddlBatch.DataTextField = "Details";
        ddlBatch.DataValueField = "No_";
        ddlBatch.DataBind();
    }
    public void BindSubjectCode()
    {
        string FCode = "";
        if (Session["UserGroup"].ToString() == "FACULTY") { FCode = Session["uid"].ToString(); }
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("[MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.proc_GetSubjectFromTomeTableForReviewAttendance_FacultyWise_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FCode);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();
    }
    //------------------------------------

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCourse();
        //bindGrid();
        grdStudentAttendance.Visible = false;
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSemester();
        grdStudentAttendance.Visible = false;
    }
    protected void grdStudentAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdStudentAttendance.PageIndex = e.NewPageIndex;
        bindGrid();
    }



    public void bindGrid()
    {
        
        string DateFilter = "", Command = "";
        // DateTime DateFrom1, DateTo1;  // comment on 22-08-2015
        String DateFrom1, DateTo1;
        if (txtDateFrom.Text != "" && txtDateTo.Text != "")
        {
            //DateFrom1 = Convert.ToDateTime(txtDateFrom.Text);    //DateTo1 = Convert.ToDateTime(txtDateTo.Text);
            DateFrom1 = txtDateFrom.Text;
            DateTo1 = txtDateTo.Text;
            DateFilter = "and Date between '" + DateFrom1 + "' and '" + DateTo1 + "'";
        }
        string AsPrincipal = "";
        //if (chkboxPrinciple.Checked == true)
        //    AsPrincipal = " and [Global Dimension 1 Code]='"+Session["GlobalDimension1Code"].ToString()+"'";
        //else
        //    AsPrincipal = " and [Staff Code]='" + Session["uid"].ToString() + "'";

        SqlCommand cmd12 = new SqlCommand("[MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.sp_GetAccessForTimeTable_Role", con);
        cmd12.CommandType = CommandType.StoredProcedure;
        cmd12.Parameters.Add("@userid", Session["uid"].ToString());
        cmd12.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
        cmd12.Parameters["@Return1"].Direction = ParameterDirection.Output;
        con.Open();
        cmd12.ExecuteNonQuery();
        string res = cmd12.Parameters["@Return1"].Value.ToString();
        con.Close();
        if (res == "1")
            AsPrincipal = " and [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'";
        else
            AsPrincipal = "and  ([Staff Code]='" + Session["uid"].ToString() + "'   or [Faculty Group Code] Collate Latin1_General_100_CS_AS in (select Group_code from HRMSPortal14122020.dbo.[FacultyGroup] where [Group Faculty Code]='" + Session["uid"].ToString() + "' or [Faculty Code]='" + Session["uid"].ToString() + "')) ";

        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd1 = new SqlCommand("select [Type Of Course] from [MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.[TMU$Course - COLLEGE] where Code='" + drpCourse.SelectedValue + "'", con);
        string TypeOfCourse = cmd1.ExecuteScalar().ToString();
        con.Close();


        if (TypeOfCourse == "1") //Remedial=0 --- by Sandeep on 14-12-2016
        {
            Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],SUM([Present]+[Absent]) as Total,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.[TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent] from [MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.[TMU$Student Attendance Line - COL] where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and  [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name]   union select (select  [Enrollment No_] from [MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.[TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent] from [TMU$Student Attendance Line - COL] where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "  and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ")  group by [Student No_],[Student Name] ) as p  group by [Student No_],[Student Name],[Enrollment No_] order by [Enrollment No_] ";
        }
        if (TypeOfCourse == "2")  //add and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' by ashu on 28-08-2016
        {
            Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],SUM([Present]+[Absent]) as Total,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.[TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent] from [MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.[TMU$Student Attendance Line - COL] where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "'   and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "'  " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name]  union select (select  [Enrollment No_] from [MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.[TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent] from [MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.[TMU$Student Attendance Line - COL] where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "' and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + " and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ") group by [Student No_],[Student Name] ) as p  group by [Student No_],[Student Name],[Enrollment No_] order by [Enrollment No_] ";
        }
        //SqlCommand cmd = new SqlCommand("proc_GetStudentDetailsForReviewAttendance", con);
        SqlCommand cmd = new SqlCommand(Command, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdStudentAttendance.Visible = true;
        grdStudentAttendance.DataSource = dt;
        grdStudentAttendance.DataBind();
        if (dt.Rows.Count > 0)
        {
            btnExport.Visible = true;
            hfR_Session.Value = (ddlAcademicYear.SelectedIndex != 0 ? ddlAcademicYear.SelectedItem.Text : "");
            hfR_Course.Value = (drpCourse.SelectedIndex != 0 ? drpCourse.SelectedItem.Text : "");
            hfR_SemYear.Value = drpSemester.SelectedIndex != 0 ? drpSemester.SelectedItem.Text : "";
            hfR_Section.Value = drpSection.SelectedIndex != 0 ? drpSection.SelectedItem.Text : "";
            hfR_Subject.Value = drpSubject.SelectedIndex != 0 ? drpSubject.SelectedItem.Text : "";
            hfR_Group.Value = ddlGroup.SelectedIndex != 0 ? ddlGroup.SelectedItem.Text : "";
            hfR_Batch.Value = ddlBatch.SelectedIndex != 0 ? ddlBatch.SelectedItem.Text : "";
            hfR_FromDate.Value = txtDateFrom.Text;
            hfR_ToDate.Value = txtDateTo.Text;
        }
        else
            btnExport.Visible = false;
    }

    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdStudentAttendance.Visible = false;
        BindSection();
        BindSubjectCode();
        bindGroupList();
        bindBatchList();
        //bindGrid();
        BindCombinationCode();

    }
    public void BindCombinationCode()
    {
        string FCode = "";
        if (Session["UserGroup"].ToString() == "FACULTY") { FCode = Session["uid"].ToString(); }
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetCombinationFromCourseSubjectLine", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubjectCode();
        bindGrid();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        bindGrid();

    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AttendanceView.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        string headerTable = @"<Table bgcolor=gray><tr><td colspan=7 align=center bgcolor=gold ><font size=16><h1>Attendance Report :  " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h1></font> </td></tr><tr><td><b>Academic Year: " + hfR_Session.Value + "<b></td> <td><b>From Date: " + hfR_FromDate.Value + "</b></td><td><b>To Date: " + hfR_ToDate.Value + "</b></td><td><b>Course: " + hfR_Course.Value + "</b></td><td><b>Subject: " + hfR_Subject.Value + "</td></b></tr><tr><td><b>Section: " + hfR_Section.Value + "</b></td><td><b>Group: " + hfR_Group.Value + "</b></td><td><b>Batch: " + hfR_Batch.Value + "</b></td><td>By:-" + Session["uname"].ToString() + "</td></tr></Table>";
        Response.Write(headerTable);
        StringWriter sw1 = new StringWriter();
        Response.Write(sw1.ToString());

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            grdStudentAttendance.AllowPaging = false;
            bindGrid();
            grdStudentAttendance.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdStudentAttendance.HeaderRow.Cells)
            {
                cell.BackColor = grdStudentAttendance.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdStudentAttendance.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdStudentAttendance.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdStudentAttendance.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            grdStudentAttendance.RenderControl(hw);

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
    protected void chkboxPrinciple_CheckedChanged(object sender, EventArgs e)
    {
        BindCourse();
    }
    public void ReviewAttandanceCountDetail(string str, int i, string academic)
    {
        SqlCommand cmd = new SqlCommand("[MEDICAL].[EDUCOLLEGELIVE-R214122020].dbo.[Sp_ReviewAttandanceCountDetail]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StudentNo_", str);
        cmd.Parameters.Add("@AcademicYear", academic);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@SemYear", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Fromdate", txtDateFrom.Text);
        cmd.Parameters.Add("@Todate", txtDateTo.Text);
        cmd.Parameters.Add("@Type", ddlTypeClass.SelectedValue == "0,4" ? "0" : ddlTypeClass.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AttendanceType", i);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue);
        cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.CommandTimeout = 50000;
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAttandanceDetails.DataSource = dt;
        grdAttandanceDetails.DataBind();


    }
    protected void BtnPresent_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;

        Label lblPresent = (Label)grdStudentAttendance.Rows[index].FindControl("lblStudent");

        Label lblStudentName = (Label)grdStudentAttendance.Rows[index].FindControl("lblStudentName");
        Label lblAcademicYear = (Label)grdStudentAttendance.Rows[index].FindControl("lblAcademicYear");
        lblStudent.Text = "Student Name:- " + lblStudentName.Text;
        lblCourse.Text = "Course:- " + drpCourse.SelectedValue;
        ReviewAttandanceCountDetail(lblPresent.Text, 0, lblAcademicYear.Text);
        GridViewDetails.Show();
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewDetails.Hide();
    }

    protected void BtnAbsent_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;

        Label lblPresent = (Label)grdStudentAttendance.Rows[index].FindControl("lblStudent");


        Label lblStudentName = (Label)grdStudentAttendance.Rows[index].FindControl("lblStudentName");
        Label lblAcademicYear = (Label)grdStudentAttendance.Rows[index].FindControl("lblAcademicYear");
        lblStudent.Text = "Student Name:- " + lblStudentName.Text;
        lblCourse.Text = "Course:- " + drpCourse.SelectedValue;
        ReviewAttandanceCountDetail(lblPresent.Text, 1, lblAcademicYear.Text);
        GridViewDetails.Show();
    }
    protected void ddlTypeClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTypeClass.SelectedValue == "2")
            drpSubject.Enabled = false;
        else
            drpSubject.Enabled = true;
    }

    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGrid();
    }

    public void AccessRightsMD()
    {
        SqlCommand cmd12 = new SqlCommand("sp_GetAccessForTimeTable_RoleMD", con);
        cmd12.CommandType = CommandType.StoredProcedure;
        cmd12.Parameters.Add("@userid", Session["uid"].ToString());
        cmd12.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
        cmd12.Parameters["@Return1"].Direction = ParameterDirection.Output;
        con.Open();
        cmd12.ExecuteNonQuery();

        con.Close();
    }



}