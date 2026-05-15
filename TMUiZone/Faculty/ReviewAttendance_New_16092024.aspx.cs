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

public partial class Faculty_ReviewAttendance_New : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bindAcademicYear();
                BindCourse();
            }
            catch
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        try
        {
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
                chkboxPrinciple.Visible = true;
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }

    }

    public void BindCourse()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_GetCourseRoleWise_HOD_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@userid", Session["uid"].ToString());
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@AcadmicYear", ddlAcademicYear.SelectedValue);
            if (ddlTypeClass.SelectedValue == "1")
            {
                cmd.Parameters.Add("@Remedial", "1");
            }


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpCourse.DataSource = dt;
            drpCourse.DataTextField = "Details";
            drpCourse.DataValueField = "No_";
            drpCourse.DataBind();
        }
        catch
        {
        }
    }
    public void BindSemester()
    {
        try
        {
            string FacultyCode = "";
            //if (chkboxPrinciple.Checked == false)
            //{
            FacultyCode = Session["uid"].ToString();
            //}
            SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", FacultyCode);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            if (ddlTypeClass.SelectedValue == "1")
            {
                cmd.Parameters.Add("@Remedial", "1");
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpSemester.DataSource = dt;
            drpSemester.DataTextField = "Details";
            drpSemester.DataValueField = "No_";
            drpSemester.DataBind();
        }
        catch
        {
        }
    }
    public void BindSection()
    {
        string FacultyCode = "";
        //if (chkboxPrinciple.Checked == false)
        //{
        FacultyCode = Session["uid"].ToString();
        //}
        if (Session["UserGroup"].ToString() == "FACULTY") { FacultyCode = Session["uid"].ToString(); }
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role_TimeTable", con);//proc_GetSectionFromCourseWiseFaculty_Role
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (Chkdetained.Checked == true)
            {
                drpSection.DataSource = ds.Tables[1];
                drpSection.DataTextField = "Details";
                drpSection.DataValueField = "No_";
                drpSection.DataBind();
            }
            else
            {
                drpSection.DataSource = ds.Tables[0];
                drpSection.DataTextField = "Details";
                drpSection.DataValueField = "No_";
                drpSection.DataBind();
            }
        }
        catch (Exception ex) { }
    }
    //-------------add by ashu--on 25-08-2016
    public void bindAcademicYear()
    {
        try
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
        catch
        {
        }
    }
    public void bindGroupList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_GetGroup_Role_TimeTable", con);//sp_GetGroup_Role
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (Chkdetained.Checked == true)
            {
                ddlGroup.DataSource = ds.Tables[1];
                ddlGroup.DataTextField = "Details";
                ddlGroup.DataValueField = "No_";
                ddlGroup.DataBind();
            }
            else
            {
                ddlGroup.DataSource = ds.Tables[0];
                ddlGroup.DataTextField = "Details";
                ddlGroup.DataValueField = "No_";
                ddlGroup.DataBind();
            }
        }
        catch (Exception ex) { }
    }
    public void bindBatchList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetBatchFromCourseSemester_Role_TimeTable", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (Chkdetained.Checked == true)
            {
                ddlBatch.DataSource = ds.Tables[1];
                ddlBatch.DataTextField = "Details";
                ddlBatch.DataValueField = "No_";
                ddlBatch.DataBind();
            }
            else
            {
                ddlBatch.DataSource = ds.Tables[0];
                ddlBatch.DataTextField = "Details";
                ddlBatch.DataValueField = "No_";
                ddlBatch.DataBind();
            }
        }
        catch (Exception ex) { }
        //SqlCommand cmd = new SqlCommand("proc_GetBatchFromCourseSemester_Role", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        //cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        //cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        //cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        //DataTable dt = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //da.Fill(dt);
        //ddlBatch.DataSource = dt;
        //ddlBatch.DataTextField = "Details";
        //ddlBatch.DataValueField = "No_";
        //ddlBatch.DataBind();
    }
    //------------------------------------
    public void BindSubjectCode()
    {
        try
        {
            string FCode = "";
            //string FCode=Session["uid"].ToString();
            //if( chkboxPrinciple.Checked == true){FCode="";}
            if (Session["UserGroup"].ToString() != "FACULTY") 
            { FCode = ""; }
            else { FCode = Session["uid"].ToString(); }

            DataTable dt = new DataTable();
            //SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance", con); sandeep 22/12/2016
            // SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance_FacultyWise_Role", con);//ashu on /10/2017
            SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance_FacultyWise_Role_AcademicYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            cmd.Parameters.Add("@Section", drpSection.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", FCode);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            drpSubject.DataSource = dt;
            drpSubject.DataTextField = "Details";
            drpSubject.DataValueField = "No_";
            drpSubject.DataBind();
        }
        catch
        { }
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCourse();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSemester();
    }
    //protected void grdStudentAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdStudentAttendance.PageIndex = e.NewPageIndex;
    //    if (chkOpen.Checked == true)
    //    {
    //        bindGridOpen();
    //    }
    //    else
    //    {
    //        bindGrid();
    //    }
    //}

    public void bindGrid()
    {
        try
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

            SqlCommand cmd12 = new SqlCommand("sp_GetAccessForTimeTable_Role", con);
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
            {
                if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC")
                {
                    AsPrincipal = "and [Staff Code]='" + Session["uid"].ToString() + "'";
                }
                else
                {
                    AsPrincipal = "";
                    // AsPrincipal = "and [Staff Code]='" + Session["uid"].ToString() + "'"; 
                }
            }

            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd1 = new SqlCommand("select [Type Of Course] from [TMU$Course - COLLEGE] where Code='" + drpCourse.SelectedValue + "'", con);
            string TypeOfCourse = cmd1.ExecuteScalar().ToString();
            con.Close();


            if (res != "1")
            {
                if (TypeOfCourse == "1") //Remedial=0 --- by Sandeep on 14-12-2016
                {
                    if (Chkdetained.Checked == true)
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE]  WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL]  WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and  [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE]  WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL]  WITH(NOLOCK) WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "  and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  group by [Student No_],[Student Name],[Event Type] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                    else
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and  [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "  and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ")  group by [Student No_],[Student Name],[Event Type] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }

                }
                if (TypeOfCourse == "2")  //add and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' by ashu on 28-08-2016
                {
                    if (Chkdetained.Checked == true)
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "'   and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "'  " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")  and Detained=1  group by [Student No_],[Student Name],[Event Type]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent] ,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "' and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + " and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ")  and Detained=1  group by [Student No_],[Student Name],[Event Type] ) as p   group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css  ";
                    }
                    else
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "'   and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "'  " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent] ,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "' and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + " and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ") group by [Student No_],[Student Name],[Event Type] ) as p   group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css  ";
                    }
                }
            }
            else
            {

                if (TypeOfCourse == "1") //Remedial=0 --- by Sandeep on 14-12-2016
                {
                    if (Chkdetained.Checked == true)
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and  [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1    group by [Student No_],[Student Name],[Event Type]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "  and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1   group by [Student No_],[Student Name],[Event Type] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                    else
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and  [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "  and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ")  group by [Student No_],[Student Name],[Event Type] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                }
                if (TypeOfCourse == "2")  //add and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' by ashu on 28-08-2016
                {
                    if (Chkdetained.Checked == true)
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "'   and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "'  " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  group by [Student No_],[Student Name],[Event Type]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "' and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + " and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1 group by [Student No_],[Student Name] ,[Event Type]) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                    else
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "'   and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "'  " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "' and Section like '" + drpSection.SelectedValue + "%' and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + " and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ") group by [Student No_],[Student Name] ,[Event Type]) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                }
            }

            //SqlCommand cmd = new SqlCommand("proc_GetStudentDetailsForReviewAttendance", con);
            SqlCommand cmd = new SqlCommand(Command, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 50000;
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
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
        catch (Exception ex) { }
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSection();
        BindSubjectCode();
        bindGroupList();
        bindBatchList();
    }

    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubjectCode();
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
        string headerTable = @"<Table bgcolor=gray><tr><td colspan=7 align=center bgcolor=gold ><font size=16><h1>Attendance Report :  " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h1></font> </td></tr><tr><td><b>Session: " + hfR_Session.Value + "<b></td> <td><b>From Date: " + hfR_FromDate.Value + "</b></td><td><b>To Date: " + hfR_ToDate.Value + "</b></td><td><b>Course: " + hfR_Course.Value + "</b></td><td><b>Subject: " + hfR_Subject.Value + "</td></b></tr><tr><td><b>Section: " + hfR_Section.Value + "</b></td><td><b>Group: " + hfR_Group.Value + "</b></td><td><b>Batch: " + hfR_Batch.Value + "</b></td><td>By:-" + Session["uname"].ToString() + "</td></tr></Table>";
        Response.Write(headerTable);
        StringWriter sw1 = new StringWriter();
        Response.Write(sw1.ToString());

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            grdStudentAttendance.AllowPaging = false;
            if (chkOpen.Checked == true)
            {
                bindGridOpen();
            }
            else
            {
                bindGrid();
            }
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
    public void ReviewAttandanceCountDetail(string str, int i, string EventType)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[Sp_ReviewAttandanceCountDetail]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudentNo_", str);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
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
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (EventType != "Others")
                {

                    grdAttandanceDetails.DataSource = dt.Select("[Event Type]='" + EventType + "'").CopyToDataTable();

                }
                else
                {
                    grdAttandanceDetails.DataSource = dt.Select("[Event Type]='Others'").CopyToDataTable();
                }


                grdAttandanceDetails.DataBind();
            }
            else
            {
                grdAttandanceDetails.DataSource = dt;
                grdAttandanceDetails.DataBind();
            }


        }
        catch (Exception ex) { }
    }
    protected void BtnPresent_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            int index = row.RowIndex;

            Label lblPresent = (Label)grdStudentAttendance.Rows[index].FindControl("lblStudent");

            Label lblStudentName = (Label)grdStudentAttendance.Rows[index].FindControl("lblStudentName");
            HiddenField HfEventType = (HiddenField)grdStudentAttendance.Rows[index].FindControl("HfEventType");
            lblStudent.Text = "Student Name:- " + lblStudentName.Text;
            lblCourse.Text = "Course:- " + drpCourse.SelectedValue;
            ReviewAttandanceCountDetail(lblPresent.Text, 0, HfEventType.Value);
            GridViewDetails.Show();
        }
        catch (Exception ex) { }
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
        HiddenField HfEventType = (HiddenField)grdStudentAttendance.Rows[index].FindControl("HfEventType");
        lblStudent.Text = "Student Name:- " + lblStudentName.Text;
        lblCourse.Text = "Course:- " + drpCourse.SelectedValue;
        ReviewAttandanceCountDetail(lblPresent.Text, 1, HfEventType.Value);
        GridViewDetails.Show();
    }
    protected void ddlTypeClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCourse();
        if (ddlTypeClass.SelectedValue == "2")
            drpSubject.Enabled = false;
        else
            drpSubject.Enabled = true;
        BindSemester();

    }

    protected void Chkdetained_CheckedChanged(object sender, EventArgs e)
    {
        BindSection();
        BindSubjectCode();
        bindGroupList();
        bindBatchList();
        txtDateFrom.Text = "";
        txtDateTo.Text = "";

        bindGrid();
    }
    public void bindGetSubjectList1()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromCourseSubjectLine1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();

    }
    protected void chkOpen_CheckedChanged(object sender, EventArgs e)
    {
        if (chkOpen.Checked == true)
        {
            drpCourse.Enabled = false;
            drpSemester.Enabled = false;
            drpSection.Enabled = false;
            ddlGroup.Enabled = false;
            ddlBatch.Enabled = false;
            btnShow.Visible = false;
            btnShow1.Visible = true;
            bindGetSubjectList1();
        }
        else
        {

            drpCourse.Enabled = true;
            drpSemester.Enabled = true;
            drpSection.Enabled = true;
            ddlGroup.Enabled = true;
            ddlBatch.Enabled = true;
            btnShow.Visible = true;
            btnShow1.Visible = false;
            drpSubject.DataSource = "";
            drpSubject.DataBind();
        }
    }
    protected void btnShow1_Click(object sender, EventArgs e)
    {

        bindGridOpen();

    }
    public void bindGridOpen()
    {
        try
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

            SqlCommand cmd12 = new SqlCommand("sp_GetAccessForTimeTable_Role", con);
            cmd12.CommandType = CommandType.StoredProcedure;
            cmd12.Parameters.Add("@userid", Session["uid"].ToString());
            cmd12.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
            cmd12.Parameters["@Return1"].Direction = ParameterDirection.Output;
            con.Open();
            cmd12.ExecuteNonQuery();
            string res = cmd12.Parameters["@Return1"].Value.ToString();
            con.Close();


            if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC")
            {
                AsPrincipal = "and [Staff Code]='" + Session["uid"].ToString() + "'";
            }
            else
            {
                AsPrincipal = "";
                // AsPrincipal = "and [Staff Code]='" + Session["uid"].ToString() + "'"; 
            }


            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd1 = new SqlCommand("select [Type Of Course] from [TMU$Subject - COLLEGE] WITH(NOLOCK) where Code='" + drpSubject.SelectedValue + "'", con);
            string TypeOfCourse = cmd1.ExecuteScalar().ToString();
            con.Close();


            if (res != "1")
            {
                if (TypeOfCourse == "1") //Remedial=0 --- by Sandeep on 14-12-2016
                {
                    if (Chkdetained.Checked == true)
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and   [Subject Code] like '" + drpSubject.SelectedValue + "%' " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "   and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  group by [Student No_],[Student Name],[Event Type] ) as p  where  [Student No_] in(select [Student No_] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Staff Code]='" + Session["uid"].ToString() + "'" + DateFilter + "   and Remedial in (" + ddlTypeClass.SelectedValue + ")  and Detained=1 ) group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                    else
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "   and Remedial in (" + ddlTypeClass.SelectedValue + ")  group by [Student No_],[Student Name],[Event Type] ) as p  where  [Student No_] in(select [Student No_] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%' and [Staff Code]='" + Session["uid"].ToString() + "'" + DateFilter + "   and Remedial in (" + ddlTypeClass.SelectedValue + ") ) group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }

                }
                if (TypeOfCourse == "2")  //add and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' by ashu on 28-08-2016
                {
                    if (Chkdetained.Checked == true)
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")  and Detained=1  group by [Student No_],[Student Name],[Event Type]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent] ,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "  and Remedial in (" + ddlTypeClass.SelectedValue + ")  and Detained=1  group by [Student No_],[Student Name],[Event Type] ) as p where  [Student No_] in(select [Student No_] from [TMU$Student Attendance Line - COL] where  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  and [Staff Code]='" + Session["uid"].ToString() + "'" + DateFilter + "  and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  )  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                    else
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent] ,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "  and Remedial in (" + ddlTypeClass.SelectedValue + ") group by [Student No_],[Student Name],[Event Type] ) as p where  [Student No_] in(select [Student No_] from [TMU$Student Attendance Line - COL] where  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  and [Staff Code]='" + Session["uid"].ToString() + "'" + DateFilter + "  and Remedial in (" + ddlTypeClass.SelectedValue + ") )  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                }
            }
            else
            {

                if (TypeOfCourse == "1") //Remedial=0 --- by Sandeep on 14-12-2016
                {
                    if (Chkdetained.Checked == true)
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and    [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1    group by [Student No_],[Student Name],[Event Type]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "   and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1   group by [Student No_],[Student Name],[Event Type] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                    else
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and    [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "   and Remedial in (" + ddlTypeClass.SelectedValue + ")  group by [Student No_],[Student Name],[Event Type] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                }
                if (TypeOfCourse == "2")  //add and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' by ashu on 28-08-2016
                {
                    if (Chkdetained.Checked == true)
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Subject Code] like '" + drpSubject.SelectedValue + "%' " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  group by [Student No_],[Student Name],[Event Type]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "  and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1 group by [Student No_],[Student Name] ,[Event Type]) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                    else
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%' " + AsPrincipal + DateFilter + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css from [TMU$Student Attendance Line - COL] where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  and [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + "  and Remedial in (" + ddlTypeClass.SelectedValue + ") group by [Student No_],[Student Name] ,[Event Type]) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css order by [Enrollment No_] ";
                    }
                }
            }

            //SqlCommand cmd = new SqlCommand("proc_GetStudentDetailsForReviewAttendance", con);
            SqlCommand cmd = new SqlCommand(Command, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 50000;
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
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
        catch (Exception ex) { }
    }
}
