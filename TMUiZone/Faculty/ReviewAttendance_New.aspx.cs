using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

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
      //  FacultyCode = Session["uid"].ToString();
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
            if (Session["UserGroup"].ToString() == "FACULTY") { FCode = Session["uid"].ToString(); }
            else { FCode = Session["uid"].ToString(); }

            DataTable dt = new DataTable();
            //SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance", con); sandeep 22/12/2016
            // SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance_FacultyWise_Role", con);//ashu on /10/2017
            //  SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance_FacultyWise_Role_AcademicYear", con);
            SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance_FacultyWise_Role_AcademicYearNew", con); //Sanjay Jain on /08/2024
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
            grdStudentAttendance.DataSource = null;
            grdStudentAttendance.DataBind();
            string DateFilter = "", Command = "";
            // DateTime DateFrom1, DateTo1;  // comment on 22-08-2015
            if (drpCourse.SelectedItem.Text == "-- Course --")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('please select course...');", true);
                return;
            }
            if (drpSemester.SelectedItem.Text == "-- Semester --")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('please select semester...');", true);
                return;
            }
            if (txtDateFrom.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('please select date...');", true);
                return;
            }
            String DateFrom1, DateTo1;



            // if (txtDateFrom.Text != "" && txtDateFrom.Text != "")
            if (txtDateFrom.Text != "" && txtDateTo.Text != "")
            {
                //DateFrom1 = Convert.ToDateTime(txtDateFrom.Text);    //DateTo1 = Convert.ToDateTime(txtDateTo.Text);
                DateFrom1 = txtDateFrom.Text;
                DateTo1 = txtDateTo.Text;
                DateFilter = "and Date between '" + DateFrom1 + "' and '" + DateTo1 + "'";  // change kiya
                                                                                            // DateFilter = "and Date between '" + DateFrom1 + "' and '" + DateFrom1 + "'";
            }
            string AsPrincipal = "";
            SqlCommand cmd12 = new SqlCommand("sp_GetAccessForTimeTable_Role", con);
            cmd12.CommandType = CommandType.StoredProcedure;
            cmd12.Parameters.Add("@userid", Session["uid"].ToString());
            cmd12.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
            cmd12.Parameters["@Return1"].Direction = ParameterDirection.Output;
            con.Close();
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
            String subValue = "";
            bool subValue1 = false;
            if (drpSubject.SelectedValue != "")
            {
                subValue = "and [Subject Code] like '" + drpSubject.SelectedValue + "%'";
                subValue1 = drpSubject.SelectedValue.ToString().Contains("ECA");
            }
            String groupValue = "";
            if (ddlGroup.SelectedValue != "")
            {
                groupValue = "and [Group Code]='" + ddlGroup.SelectedValue + "'";
            }


            string value = ddlTypeClass.SelectedValue;

            string ddlTypeClassA = value.Split(',')[0];
            string ddlTypeClassB = value.Split(',')[1];

            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd1 = new SqlCommand("select [Type Of Course] from [TMU$Course - COLLEGE] where Code='" + drpCourse.SelectedValue + "'", con);
            string TypeOfCourse = cmd1.ExecuteScalar().ToString();
            con.Close();


            if (res != "1")  // by Varinder on 12 - 11 - 2026
            {
                if (Chkdetained.Checked == true)
                {

                    // Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',sum(case when [ECA TYPE]='' then 0 else 1 end) as 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE]  WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL]  WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and  [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + subValue + groupValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type],[ECA TYPE]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE]  WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL]  WITH(NOLOCK) WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%'   " + AsPrincipal + DateFilter + subValue + groupValue + " and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  group by [Student No_],[Student Name],[Event Type],[ECA TYPE] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";
                    // Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',sum(case when [ECA TYPE]='' then 0 else 1 end) as 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE]  WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL]  WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and  [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + subValue + groupValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type],[ECA TYPE]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE]  WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL]  WITH(NOLOCK) WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%'   " + AsPrincipal + DateFilter + subValue + groupValue + " and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  group by [Student No_],[Student Name],[Event Type],[ECA TYPE] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";

                    SqlCommand cmd = new SqlCommand("sp_GetStudentAttendanceSummary", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
                    cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                    cmd.Parameters.Add("Semester", drpSemester.SelectedValue);
                    cmd.Parameters.Add("@Section", drpSection.SelectedValue);
                    cmd.Parameters.Add("@BatchCode", ddlBatch.SelectedValue);

                    cmd.Parameters.Add("@FromDate", txtDateFrom.Text);
                    cmd.Parameters.Add("@ToDate", txtDateTo.Text);
                    cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                    cmd.Parameters.Add("@Remedial1", Convert.ToInt32(ddlTypeClassA));
                    cmd.Parameters.Add("@Remedial2", Convert.ToInt32(ddlTypeClassB));
                    cmd.Parameters.Add("@Type", "2");

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
                       // btn_EC.Visible = true;

                        hfR_Session.Value = (ddlAcademicYear.SelectedIndex != 0 ? ddlAcademicYear.SelectedItem.Text : "");
                        hfR_Course.Value = (drpCourse.SelectedIndex != 0 ? drpCourse.SelectedItem.Text : "");
                        hfR_SemYear.Value = (drpSemester != null && drpSemester.SelectedItem != null && drpSemester.SelectedIndex != 0) ? drpSemester.SelectedItem.Text : "";
                        hfR_Section.Value = (drpSection != null && drpSection.SelectedItem != null && drpSection.SelectedIndex != 0) ? drpSection.SelectedItem.Text : "";
                        hfR_Subject.Value = (drpSubject != null && drpSubject.SelectedItem != null && drpSubject.SelectedIndex != 0) ? drpSubject.SelectedItem.Text : "";
                        hfR_Group.Value = (ddlGroup != null && ddlGroup.SelectedItem != null && ddlGroup.SelectedIndex != 0) ? ddlGroup.SelectedItem.Text : "";
                        hfR_Batch.Value = (ddlBatch != null && ddlBatch.SelectedItem != null && ddlBatch.SelectedIndex != 0) ? ddlBatch.SelectedItem.Text : "";
                        hfR_FromDate.Value = txtDateFrom.Text;
                        hfR_ToDate.Value = txtDateTo.Text;
                    }
                    else
                    {
                        btnExport.Visible = false;
                       // btn_EC.Visible = false;
                    }
                }
                else
                {
                    // Command = "select [Enrollment No_] ,[Student No_],[Student Name],CASE WHEN SUM(Present) = 0 THEN 'P' WHEN SUM(Present) = 1 THEN 'A' END AS Present, CASE WHEN SUM([Absent]) = 0 THEN 'P' WHEN SUM(Absent) = 1 THEN 'A' END AS Absent,CASE WHEN (Attancance) = 0 THEN 'P' WHEN (Attancance) = 1 THEN 'A' END AS Attancance,(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',CASE WHEN SUM(CASE WHEN p.[ECA TYPE] = '' THEN 0 ELSE 1 END) = 0 THEN '' WHEN SUM(CASE WHEN p.[ECA TYPE] = 0 THEN 1 ELSE 0 END) > 0 THEN 'Pending' WHEN SUM(CASE WHEN p.[ECA TYPE] = 1 THEN 1 ELSE 0 END) > 0 THEN 'Approved' ELSE '' END AS 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],[Attendance Type] as Attancance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and  [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%'  and [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + subValue + groupValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ")  group by [Student No_],[Student Name],[Event Type],[ECA TYPE],[Attendance Type] union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],[Attendance Type] as Attancance, case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%'   " + AsPrincipal + DateFilter + subValue + groupValue + "  and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ")  group by [Student No_],[Student Name],[Event Type],[ECA TYPE],[Attendance Type] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler,Attancance order by [Enrollment No_] asc";
                    // Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',sum(case when [ECA TYPE]='' then 0 else 1 end) as 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and  [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%'  and [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + subValue + groupValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type],[ECA TYPE]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%'   " + AsPrincipal + DateFilter + subValue + groupValue + "  and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ")  group by [Student No_],[Student Name],[Event Type],[ECA TYPE] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";

                    SqlCommand cmd = new SqlCommand("sp_GetStudentAttendanceSummary", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
                    cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                    cmd.Parameters.Add("Semester", drpSemester.SelectedValue);
                    cmd.Parameters.Add("@Section", drpSection.SelectedValue);
                    cmd.Parameters.Add("@BatchCode", ddlBatch.SelectedValue);

                    cmd.Parameters.Add("@FromDate", txtDateFrom.Text);
                    cmd.Parameters.Add("@ToDate", txtDateTo.Text);
                    cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                    cmd.Parameters.Add("@Remedial1", Convert.ToInt32(ddlTypeClassA));
                    cmd.Parameters.Add("@Remedial2", Convert.ToInt32(ddlTypeClassB));
                    cmd.Parameters.Add("@Type", "1");

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
                       // btn_EC.Visible = true;

                        hfR_Session.Value = (ddlAcademicYear.SelectedIndex != 0 ? ddlAcademicYear.SelectedItem.Text : "");
                        hfR_Course.Value = (drpCourse.SelectedIndex != 0 ? drpCourse.SelectedItem.Text : "");
                        hfR_SemYear.Value = (drpSemester != null && drpSemester.SelectedItem != null && drpSemester.SelectedIndex != 0) ? drpSemester.SelectedItem.Text : "";
                        hfR_Section.Value = (drpSection != null && drpSection.SelectedItem != null && drpSection.SelectedIndex != 0) ? drpSection.SelectedItem.Text : "";
                        hfR_Subject.Value = (drpSubject != null && drpSubject.SelectedItem != null && drpSubject.SelectedIndex != 0) ? drpSubject.SelectedItem.Text : "";
                        hfR_Group.Value = (ddlGroup != null && ddlGroup.SelectedItem != null && ddlGroup.SelectedIndex != 0) ? ddlGroup.SelectedItem.Text : "";
                        hfR_Batch.Value = (ddlBatch != null && ddlBatch.SelectedItem != null && ddlBatch.SelectedIndex != 0) ? ddlBatch.SelectedItem.Text : "";
                        hfR_FromDate.Value = txtDateFrom.Text;
                        hfR_ToDate.Value = txtDateTo.Text;
                    }
                    else
                    {
                        btnExport.Visible = false;
                       // btn_EC.Visible = false;
                    }
                }
            }
            else
            {
                if (TypeOfCourse == "1") //Remedial=0 --- by Sandeep on 14-12-2016
                {
                    if (Chkdetained.Checked == true)
                    {


                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],CASE WHEN SUM(Present) = 0 THEN 'P' WHEN SUM(Present) = 1 THEN 'A' END AS Present, CASE WHEN SUM([Absent]) = 0 THEN 'P' WHEN SUM(Absent) = 1 THEN 'A' END AS Absent,CASE WHEN (Attancance) = 0 THEN 'P' WHEN (Attancance) = 1 THEN 'A' END AS Attancance,(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',CASE WHEN SUM(CASE WHEN p.[ECA TYPE] = '' THEN 0 ELSE 1 END) = 0 THEN '' WHEN SUM(CASE WHEN p.[ECA TYPE] = 0 THEN 1 ELSE 0 END) > 0 THEN 'Pending' WHEN SUM(CASE WHEN p.[ECA TYPE] = 1 THEN 1 ELSE 0 END) > 0 THEN 'Approved' ELSE '' END AS 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],[Attendance Type] as Attancance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and  [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%' and [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + subValue + groupValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  group by [Student No_],[Student Name],[Event Type],[ECA TYPE],[Attendance Type] union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],[Attendance Type] as Attancance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedValue + "'  and Section like '" + drpSection.SelectedValue + "%'   " + AsPrincipal + DateFilter + subValue + groupValue + " and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1   group by [Student No_],[Student Name],[Event Type],[ECA TYPE],[Attendance Type] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler group by [Enrollment No_],[Student No_],[Student Name],[Present],[Absent],Total,Percentage,[Event Type],css,Hosteler,Attancance order by [Enrollment No_] ";
                    }
                    else
                    {
                        //if (subValue1)
                        //{
                        //    Command = "select [Enrollment No_],[Student No_],[Student Name],[Present],[Absent],Total,Percentage,[Event Type],css,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',sum(case when [ECA TYPE]='' then 0 else 1 end) as 'ECA' from (SELECT [Student No_],[Student Name],(select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], IIF([Event Type] != '',COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END)-COUNT(CASE WHEN [Event Type] = '' THEN 1 END), COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END)) AS Present, COUNT(CASE WHEN [Attendance Type] = 1 THEN 1 END) AS Absent,COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END) + COUNT(CASE WHEN [Attendance Type] = 1 THEN 1 END) AS Total, CASE   WHEN (COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END) + COUNT(CASE WHEN [Attendance Type] = 1 THEN 1 END)) = 0 THEN 0 ELSE CEILING(ROUND((COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END) * 100.0) / (COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END) +  COUNT(CASE WHEN [Attendance Type] = 1 THEN 1 END)),0)) END AS Percentage,[Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] FROM [TMU$Student Attendance Line - COL] WITH(NOLOCK) WHERE [Academic Year]='" + ddlAcademicYear.SelectedValue + "' AND [Course Code]='" + drpCourse.SelectedValue + "' AND Semester='" + drpSemester.SelectedValue + "' AND Section like '" + drpSection.SelectedValue + "%' AND [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + subValue + groupValue + " AND [Remedial] in (" + ddlTypeClass.SelectedValue + ")GROUP BY [Student No_], [Student Name],[Event Type],[ECA TYPE]) as s order by s.[Student Name]";
                        //}
                        //else
                        //{
                            // Command = "select [Enrollment No_],[Student No_],[Student Name],[Present],[Absent],Total,Percentage,[Event Type],css,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',sum(case when [ECA TYPE]='' then 0 else 1 end) as 'ECA' from (SELECT [Student No_],[Student Name],(select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], IIF([Event Type] != '',COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END)-COUNT(CASE WHEN [Event Type]! = 0 THEN 1 END), COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END)) AS Present, COUNT(CASE WHEN [Attendance Type] = 1 THEN 1 END) AS Absent,COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END) + COUNT(CASE WHEN [Attendance Type] = 1 THEN 1 END) AS Total, CASE   WHEN (COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END) + COUNT(CASE WHEN [Attendance Type] = 1 THEN 1 END)) = 0 THEN 0 ELSE CEILING(ROUND((COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END) * 100.0) / (COUNT(CASE WHEN [Attendance Type] = 0 THEN 1 END) +  COUNT(CASE WHEN [Attendance Type] = 1 THEN 1 END)),0)) END AS Percentage,[Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] FROM [TMU$Student Attendance Line - COL] WITH(NOLOCK) WHERE [Academic Year]='" + ddlAcademicYear.SelectedValue + "' AND [Course Code]='" + drpCourse.SelectedValue + "' AND Semester='" + drpSemester.SelectedValue + "' AND Section like '" + drpSection.SelectedValue + "%' AND [Batch Code]='" + ddlBatch.SelectedValue + "' " + AsPrincipal + DateFilter + subValue + groupValue + " AND [Remedial] in (" + ddlTypeClass.SelectedValue + ")GROUP BY [Student No_], [Student Name],[Event Type],[ECA TYPE]) as s where [Event Type]='' group by [Enrollment No_],[Student No_],[Student Name],[Present],[Absent],Total,Percentage,[Event Type],css,Hosteler order by s.[Student Name]";
                            SqlCommand cmd = new SqlCommand("sp_GetStudentAttendanceSummary_Principal", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
                            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                            cmd.Parameters.Add("Semester", drpSemester.SelectedValue);
                            cmd.Parameters.Add("@Section", drpSection.SelectedValue);
                            cmd.Parameters.Add("@BatchCode", ddlBatch.SelectedValue);

                            cmd.Parameters.Add("@FromDate", txtDateFrom.Text);
                            cmd.Parameters.Add("@ToDate", txtDateTo.Text);
                            cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                            cmd.Parameters.Add("@Remedial1", Convert.ToInt32(ddlTypeClassA));
                            cmd.Parameters.Add("@Remedial2", Convert.ToInt32(ddlTypeClassB));
                            cmd.Parameters.Add("@GlobalDimension1Code", Session["GlobalDimension1Code"].ToString());
                            cmd.Parameters.Add("@Type", "1");

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
                                //btn_EC.Visible = true;

                                hfR_Session.Value = (ddlAcademicYear.SelectedIndex != 0 ? ddlAcademicYear.SelectedItem.Text : "");
                                hfR_Course.Value = (drpCourse.SelectedIndex != 0 ? drpCourse.SelectedItem.Text : "");
                                hfR_SemYear.Value = (drpSemester != null && drpSemester.SelectedItem != null && drpSemester.SelectedIndex != 0) ? drpSemester.SelectedItem.Text : "";
                                hfR_Section.Value = (drpSection != null && drpSection.SelectedItem != null && drpSection.SelectedIndex != 0) ? drpSection.SelectedItem.Text : "";
                                hfR_Subject.Value = (drpSubject != null && drpSubject.SelectedItem != null && drpSubject.SelectedIndex != 0) ? drpSubject.SelectedItem.Text : "";
                                hfR_Group.Value = (ddlGroup != null && ddlGroup.SelectedItem != null && ddlGroup.SelectedIndex != 0) ? ddlGroup.SelectedItem.Text : "";
                                hfR_Batch.Value = (ddlBatch != null && ddlBatch.SelectedItem != null && ddlBatch.SelectedIndex != 0) ? ddlBatch.SelectedItem.Text : "";
                                hfR_FromDate.Value = txtDateFrom.Text;
                                hfR_ToDate.Value = txtDateTo.Text;
                            }
                            else
                            {
                                btnExport.Visible = false;
                               // btn_EC.Visible = false;
                            }

                       // }
                    }
                }
                if (TypeOfCourse == "2")  //add and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' by ashu on 28-08-2016
                {
                    if (Chkdetained.Checked == true)
                    {
                        //1
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage ,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',sum(case when [ECA TYPE]='' then 0 else 1 end) as 'ECA','' as Attancance  (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "'   and Section like '" + drpSection.SelectedValue + "%'  and [Batch Code]='" + ddlBatch.SelectedValue + "'  " + AsPrincipal + DateFilter + subValue + groupValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  group by [Student No_],[Student Name],[Event Type]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler' from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "' and Section like '" + drpSection.SelectedValue + "%'   " + AsPrincipal + DateFilter + subValue + groupValue + "  and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1 group by [Student No_],[Student Name] ,[Event Type]) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";
                    }
                    else
                    {
                        // Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',sum(case when [ECA TYPE]='' then 0 else 1 end) as 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "'   and Section like '" + drpSection.SelectedValue + "%'   and [Batch Code]='" + ddlBatch.SelectedValue + "'  " + AsPrincipal + DateFilter + subValue + groupValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type],[ECA TYPE]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Course Code]='" + drpCourse.SelectedValue + "' and [Year]='" + drpSemester.SelectedValue + "' and Section like '" + drpSection.SelectedValue + "%'   " + AsPrincipal + DateFilter + subValue + groupValue + "  and [Batch Code]='" + ddlBatch.SelectedValue + "' and Remedial in (" + ddlTypeClass.SelectedValue + ") group by [Student No_],[Student Name] ,[Event Type],[ECA TYPE]) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";
                        SqlCommand cmd = new SqlCommand("sp_GetStudentAttendanceSummary_Principal", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
                        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                        cmd.Parameters.Add("Semester", drpSemester.SelectedValue);
                        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
                        cmd.Parameters.Add("@BatchCode", ddlBatch.SelectedValue);

                        cmd.Parameters.Add("@FromDate", txtDateFrom.Text);
                        cmd.Parameters.Add("@ToDate", txtDateTo.Text);
                        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                        cmd.Parameters.Add("@Remedial1", Convert.ToInt32(ddlTypeClassA));
                        cmd.Parameters.Add("@Remedial2", Convert.ToInt32(ddlTypeClassB));
                        cmd.Parameters.Add("@GlobalDimension1Code", Session["GlobalDimension1Code"].ToString());
                        cmd.Parameters.Add("@Type", "2");

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
                           // btn_EC.Visible = true;

                            hfR_Session.Value = (ddlAcademicYear.SelectedIndex != 0 ? ddlAcademicYear.SelectedItem.Text : "");
                            hfR_Course.Value = (drpCourse.SelectedIndex != 0 ? drpCourse.SelectedItem.Text : "");
                            hfR_SemYear.Value = (drpSemester != null && drpSemester.SelectedItem != null && drpSemester.SelectedIndex != 0) ? drpSemester.SelectedItem.Text : "";
                            hfR_Section.Value = (drpSection != null && drpSection.SelectedItem != null && drpSection.SelectedIndex != 0) ? drpSection.SelectedItem.Text : "";
                            hfR_Subject.Value = (drpSubject != null && drpSubject.SelectedItem != null && drpSubject.SelectedIndex != 0) ? drpSubject.SelectedItem.Text : "";
                            hfR_Group.Value = (ddlGroup != null && ddlGroup.SelectedItem != null && ddlGroup.SelectedIndex != 0) ? ddlGroup.SelectedItem.Text : "";
                            hfR_Batch.Value = (ddlBatch != null && ddlBatch.SelectedItem != null && ddlBatch.SelectedIndex != 0) ? ddlBatch.SelectedItem.Text : "";
                            hfR_FromDate.Value = txtDateFrom.Text;
                            hfR_ToDate.Value = txtDateTo.Text;
                        }
                        else
                        {
                            btnExport.Visible = false;
                          //  btn_EC.Visible = false;
                        }
                    }
                }
            }

            ////SqlCommand cmd = new SqlCommand("proc_GetStudentDetailsForReviewAttendance", con);
            //SqlCommand cmd = new SqlCommand(Command, con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //cmd.CommandTimeout = 50000;
            //DataTable dt = new DataTable();
            //con.Open();
            //da.Fill(dt);
            //con.Close();
            //grdStudentAttendance.DataSource = dt;
            //grdStudentAttendance.DataBind();
            //if (dt.Rows.Count > 0)
            //{
            //    btnExport.Visible = true;
            //    btn_EC.Visible = true;

            //    hfR_Session.Value = (ddlAcademicYear.SelectedIndex != 0 ? ddlAcademicYear.SelectedItem.Text : "");
            //    hfR_Course.Value = (drpCourse.SelectedIndex != 0 ? drpCourse.SelectedItem.Text : "");
            //    hfR_SemYear.Value = drpSemester.SelectedIndex != 0 ? drpSemester.SelectedItem.Text : "";
            //    hfR_Section.Value = drpSection.SelectedIndex != 0 ? drpSection.SelectedItem.Text : "";
            //    hfR_Subject.Value = drpSubject.SelectedIndex != 0 ? drpSubject.SelectedItem.Text : "";
            //    hfR_Group.Value = ddlGroup.SelectedIndex != 0 ? ddlGroup.SelectedItem.Text : "";
            //    hfR_Batch.Value = ddlBatch.SelectedIndex != 0 ? ddlBatch.SelectedItem.Text : "";
            //    hfR_FromDate.Value = txtDateFrom.Text;
            //    hfR_ToDate.Value = txtDateTo.Text;
            //}
            //else
            //{
            //    btnExport.Visible = false;
            //    btn_EC.Visible = false;
            //}
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

        string headerTable = @"
<table border='1' cellspacing='0' cellpadding='3'
       style='border-collapse:collapse;width:100%;font-family:Arial;'>

<tr>
    <td colspan='9' align='center'
        style='background-color:#FFD700;font-size:22px;
               font-weight:bold;height:25px;padding:1px;'>
        Attendance Report : " + DateTime.Now.ToString("dd-MMM-yyyy") + @"
    </td>
</tr>

<tr style='background-color:#808080;color:black;font-size:14px;font-weight:bold;'>
    <td><b>Session:</b> " + hfR_Session.Value + @"</td>
    <td><b>From Date:</b> " + hfR_FromDate.Value + @"</td>
    <td><b>To Date:</b> " + hfR_ToDate.Value + @"</td>
    <td><b>Course:</b> " + hfR_Course.Value + @"</td>
    <td colspan='3'><b>Subject:</b> " + hfR_Subject.Value + @"</td>
</tr>

<tr style='background-color:#808080;color:black;font-size:14px;font-weight:bold;'>
    <td><b>Section:</b> " + hfR_Section.Value + @"</td>
    <td><b>Group:</b> " + hfR_Group.Value + @"</td>
    <td><b>Batch:</b> " + hfR_Batch.Value + @"</td>
    <td colspan='4'><b>By:</b> " + Convert.ToString(Session["uname"]) + @"</td>
</tr>

</table><br/>";
        //string headerTable = @"<Table bgcolor=gray><tr><td colspan=7 align=center bgcolor=gold ><font size=16><h1>Attendance Report :  " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h1></font> </td></tr><tr><td><b>Session: " + hfR_Session.Value + "<b></td> <td><b>From Date: " + hfR_FromDate.Value + "</b></td><td><b>To Date: " + hfR_ToDate.Value + "</b></td><td><b>Course: " + hfR_Course.Value + "</b></td><td><b>Subject: " + hfR_Subject.Value + "</td></b></tr><tr><td><b>Section: " + hfR_Section.Value + "</b></td><td><b>Group: " + hfR_Group.Value + "</b></td><td><b>Batch: " + hfR_Batch.Value + "</b></td><td>By:-" + Session["uname"].ToString() + "</td></tr></Table>";
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
                row.Cells[row.Cells.Count - 1].Visible = false;
            }
            // grdStudentAttendance.Columns[grdStudentAttendance.Columns.Count - 1].Visible = true;
            grdStudentAttendance.HeaderRow.Cells[grdStudentAttendance.HeaderRow.Cells.Count - 1].Visible = false;
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
            SqlCommand cmd = new SqlCommand("[Sp_ReviewAttandanceCountDetailNew]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudentNo_", str);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
            cmd.Parameters.Add("@SemYear", drpSemester.SelectedValue);
            cmd.Parameters.Add("@Fromdate", txtDateFrom.Text);
            // cmd.Parameters.Add("@Todate", txtDateTo.Text);
            cmd.Parameters.Add("@Todate", txtDateFrom.Text);
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
            //if (dt.Rows.Count > 0)
            //{
            //    if (EventType != "Others")
            //    {

            //        grdAttandanceDetails.DataSource = dt.Select("[Event Type]='" + EventType + "'").CopyToDataTable();

            //    }
            //    else
            //    {
            //        grdAttandanceDetails.DataSource = dt.Select("[Event Type]='Others'").CopyToDataTable();
            //    }


            //    grdAttandanceDetails.DataBind();
            //}
            //else
            //{
            grdAttandanceDetails.DataSource = dt;
            grdAttandanceDetails.DataBind();
            // }


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
        if (Chkdetained.Checked)
            chkOpen.Checked = false;

        // bindGrid();
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
        if (chkOpen.Checked)
            Chkdetained.Checked = false;

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

            if (txtDateFrom.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('please select date...');", true);
                return;
            }
            // DateTime DateFrom1, DateTo1;  // comment on 22-08-2015
            String DateFrom1, DateTo1;
            //if (txtDateFrom.Text != "" && txtDateFrom.Text != "")
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
            con.Close();
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

            string value = ddlTypeClass.SelectedValue;

            string ddlTypeClassA = value.Split(',')[0];
            string ddlTypeClassB = value.Split(',')[1];

            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd1 = new SqlCommand("select [Type Of Course] from [TMU$Subject - COLLEGE] WITH(NOLOCK) where Code='" + drpSubject.SelectedValue + "'", con);
            string TypeOfCourse = cmd1.ExecuteScalar().ToString();
            con.Close();

            String subValue = "";
            if (drpSubject.SelectedValue != "")
            {
                subValue = "and [Subject Code] like '" + drpSubject.SelectedValue + "%'";
            }
            if (res != "1")
            {
                if (TypeOfCourse == "1") //Remedial=0 --- by Sandeep on 14-12-2016
                {
                    if (Chkdetained.Checked == true)
                    {

                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],CASE WHEN SUM(Present) = 0 THEN 'P' WHEN SUM(Present) = 1 THEN 'A' END AS Present, CASE WHEN SUM([Absent]) = 0 THEN 'P' WHEN SUM(Absent) = 1 THEN 'A' END AS Absent,CASE WHEN MAX(Attendance) = 0 THEN 'P' ELSE 'A' END AS Attancance,(sum(Present)+ sum([Absent])) Total,[Event Type],css,CASE WHEN (SUM(Present)+SUM([Absent]))>0 THEN CAST((SUM(Present)*100.0)/(SUM(Present)+SUM([Absent])) AS DECIMAL(10,2)) ELSE CAST(0 AS DECIMAL(10,2)) END AS Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',CASE WHEN SUM(CASE WHEN p.[ECA TYPE] = '' THEN 0 ELSE 1 END) = 0 THEN '' WHEN SUM(CASE WHEN p.[ECA TYPE] = 0 THEN 1 ELSE 0 END) > 0 THEN 'Pending' WHEN SUM(CASE WHEN p.[ECA TYPE] = 1 THEN 1 ELSE 0 END) > 0 THEN 'Approved' ELSE '' END AS 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],[Attendance Type] as Attendance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH (NOLOCK) where [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and  [Subject Code] like '" + drpSubject.SelectedValue + "%' " + AsPrincipal + DateFilter + subValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],[Attendance Type] as Attendance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + "  and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE] ) as p  where [Student No_] in(select [Student No_] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'   and [Staff Code]='" + Session["uid"].ToString() + "'" + DateFilter + subValue + "   and Remedial in (" + ddlTypeClass.SelectedValue + ")  and Detained=1 ) group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";
                    }
                    else
                    {
                        // Command = "select[Enrollment No_],[Student No_],[Student Name],CASE WHEN SUM(Present) = 0 THEN 'P' WHEN SUM(Present) = 1 THEN 'A' END AS Present, CASE WHEN SUM([Absent]) = 0 THEN 'P' WHEN SUM(Absent) = 1 THEN 'A' END AS Absent, CASE WHEN MAX(Attendance) = 0 THEN 'P' ELSE 'A' END AS Attancance,(sum(Present) + sum([Absent])) as Total,[Event Type],css,CASE WHEN (SUM(Present)+SUM([Absent]))>0 THEN CAST((SUM(Present)*100.0)/(SUM(Present)+SUM([Absent])) AS DECIMAL(10,2)) ELSE CAST(0 AS DECIMAL(10,2)) END AS Percentage, case when Hosteler= 1 then 'Yes' else 'No' end as Hosteler, CASE WHEN SUM(CASE WHEN p.[ECA TYPE] = '' THEN 0 ELSE 1 END) = 0 THEN '' WHEN SUM(CASE WHEN p.[ECA TYPE] = 0 THEN 1 ELSE 0 END) > 0 THEN 'Pending' WHEN SUM(CASE WHEN p.[ECA TYPE] = 1 THEN 1 ELSE 0 END) > 0 THEN 'Approved' ELSE '' END AS 'ECA' from (select(select[Enrollment No_] from[TMU$Student - COLLEGE] WITH(NOLOCK) where No_ = [Student No_]) as [Enrollment No_], [Student No_], [Student Name], count([Attendance Type]) as Present, 0 as [Absent], [Attendance Type] as Attendance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select[Hostel Alloted] from[TMU$Student - COLLEGE] WITH(NOLOCK) where No_ = [Student No_]) as Hosteler,[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH (NOLOCK) where [Attendance Type] = 0 and [Academic Year] = '" + ddlAcademicYear.SelectedValue + "' " + AsPrincipal + DateFilter + subValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ") group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE] union all select(select[Enrollment No_] from[TMU$Student - COLLEGE] WITH(NOLOCK) where No_ = [Student No_]) as [Enrollment No_],[Student No_],[Student Name],0 as Present, count([Attendance Type]) as [Absent],[Attendance Type] as Attendance, case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type], case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select[Hostel Alloted] from[TMU$Student - COLLEGE] WITH(NOLOCK) where No_ = [Student No_]) as Hosteler,[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where[Attendance Type] = 1 and [Academic Year] = '" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + "  and Remedial in (" + ddlTypeClass.SelectedValue + ") group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE] ) as p where [Student No_] in (select [Student No_] from[TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Academic Year] = '" + ddlAcademicYear.SelectedValue + "'   and [Staff Code] = '" + Session["uid"].ToString() + "'" + DateFilter + subValue + "   and Remedial in (" + ddlTypeClass.SelectedValue + ")) group by [Student No_],[Student Name],[Enrollment No_], [Event Type], css, Hosteler order by [Enrollment No_]";
                        // Command = "select [Enrollment No_] ,[Student No_],[Student Name],CASE WHEN SUM(Present) = 0 THEN 'P' WHEN SUM(Present) = 1 THEN 'A' END AS Present, CASE WHEN SUM([Absent]) = 0 THEN 'P' WHEN SUM(Absent) = 1 THEN 'A' END AS Absent,CASE WHEN (Attancance) = 0 THEN 'P' WHEN (Attancance) = 1 THEN 'A' END AS Attancance,(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',CASE WHEN SUM(CASE WHEN p.[ECA TYPE] = '' THEN 0 ELSE 1 END) = 0 THEN '' WHEN SUM(CASE WHEN p.[ECA TYPE] = 0 THEN 1 ELSE 0 END) > 0 THEN 'Pending' WHEN SUM(CASE WHEN p.[ECA TYPE] = 1 THEN 1 ELSE 0 END) > 0 THEN 'Approved' ELSE '' END AS 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],[Attendance Type] as Attancance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type],[ECA TYPE],[Attendance Type]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],[Attendance Type] as Attancance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + "   and Remedial in (" + ddlTypeClass.SelectedValue + ")  group by [Student No_],[Student Name],[Event Type],[ECA TYPE],[Attendance Type] ) as p  where  [Student No_] in(select [Student No_] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'   and [Staff Code]='" + Session["uid"].ToString() + "'" + DateFilter + subValue + "   and Remedial in (" + ddlTypeClass.SelectedValue + ") ) group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler,Attancance order by [Enrollment No_] ";
                       // Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',0 as 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler' from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type]   union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler' from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + "   and Remedial in (" + ddlTypeClass.SelectedValue + ")  group by [Student No_],[Student Name],[Event Type] ) as p  where  [Student No_] in(select [Student No_] from [TMU$Student Attendance Line - COL] WITH(NOLOCK) where  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'   and [Staff Code]='" + Session["uid"].ToString() + "'" + DateFilter + subValue + "   and Remedial in (" + ddlTypeClass.SelectedValue + ") ) group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";
                        SqlCommand cmd11 = new SqlCommand("sp_GetStudentAttendanceSummary_OpenElective", con);
                        cmd11.CommandType = CommandType.StoredProcedure;
                        cmd11.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
                        cmd11.Parameters.Add("@StaffCode", Session["uid"].ToString());
                        cmd11.Parameters.Add("@FromDate", txtDateFrom.Text);
                        cmd11.Parameters.Add("@ToDate", txtDateTo.Text);
                        cmd11.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                        cmd11.Parameters.Add("@Remedial1", Convert.ToInt32(ddlTypeClassA));
                        cmd11.Parameters.Add("@Remedial2", Convert.ToInt32(ddlTypeClassB));                       
                        cmd11.Parameters.Add("@Type", "1");

                        SqlDataAdapter da1 = new SqlDataAdapter(cmd11);
                        cmd1.CommandTimeout = 50000;
                        DataTable dt1 = new DataTable();
                        con.Open();
                        da1.Fill(dt1);
                        con.Close();
                        grdStudentAttendance.DataSource = dt1;
                        grdStudentAttendance.DataBind();
                        if (dt1.Rows.Count > 0)
                        {
                            btnExport.Visible = true;
                            // btn_EC.Visible = true;


                            hfR_Session.Value = (ddlAcademicYear.SelectedIndex != 0 ? ddlAcademicYear.SelectedItem.Text : "");
                            hfR_Course.Value = (drpCourse.SelectedIndex != 0 ? drpCourse.SelectedItem.Text : "");
                            hfR_SemYear.Value = (drpSemester != null && drpSemester.SelectedItem != null && drpSemester.SelectedIndex != 0) ? drpSemester.SelectedItem.Text : "";
                            hfR_Section.Value = (drpSection != null && drpSection.SelectedItem != null && drpSection.SelectedIndex != 0) ? drpSection.SelectedItem.Text : "";
                            hfR_Subject.Value = (drpSubject != null && drpSubject.SelectedItem != null && drpSubject.SelectedIndex != 0) ? drpSubject.SelectedItem.Text : "";
                            hfR_Group.Value = (ddlGroup != null && ddlGroup.SelectedItem != null && ddlGroup.SelectedIndex != 0) ? ddlGroup.SelectedItem.Text : "";
                            hfR_Batch.Value = (ddlBatch != null && ddlBatch.SelectedItem != null && ddlBatch.SelectedIndex != 0) ? ddlBatch.SelectedItem.Text : "";
                            hfR_FromDate.Value = txtDateFrom.Text;
                            hfR_ToDate.Value = txtDateTo.Text;
                           // return;
                        }
                        else
                        {
                            btnExport.Visible = false;
                            //  btn_EC.Visible = false;
                           // return;
                        }
                    }

                }
                if (TypeOfCourse == "2")  //add and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' by ashu on 28-08-2016
                {
                    if (Chkdetained.Checked == true)
                    {
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],CASE WHEN SUM(Present) = 0 THEN 'P' WHEN SUM(Present) = 1 THEN 'A' END AS Present, CASE WHEN SUM([Absent]) = 0 THEN 'P' WHEN SUM(Absent) = 1 THEN 'A' END AS Absent,CASE WHEN MAX(Attendance) = 0 THEN 'P' ELSE 'A' END AS Attancance,(sum(Present)+ sum([Absent])) Total,[Event Type],css,CASE WHEN (SUM(Present)+SUM([Absent]))>0 THEN CAST((SUM(Present)*100.0)/(SUM(Present)+SUM([Absent])) AS DECIMAL(10,2)) ELSE CAST(0 AS DECIMAL(10,2)) END AS Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',CASE WHEN SUM(CASE WHEN p.[ECA TYPE] = '' THEN 0 ELSE 1 END) = 0 THEN '' WHEN SUM(CASE WHEN p.[ECA TYPE] = 0 THEN 1 ELSE 0 END) > 0 THEN 'Pending' WHEN SUM(CASE WHEN p.[ECA TYPE] = 1 THEN 1 ELSE 0 END) > 0 THEN 'Approved' ELSE '' END AS 'ECA' from (select (select [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],[Attendance Type] as Attendance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH (NOLOCK) where [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ")  and Detained=1  group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE] union select (select [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent] ,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + "  and Remedial in (" + ddlTypeClass.SelectedValue + ")  and Detained=1  group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE] ) as p where [Student No_] in(select [Student No_] from [TMU$Student Attendance Line - COL] where  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    and [Staff Code]='" + Session["uid"].ToString() + "'" + DateFilter + subValue + "  and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  )  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";
                    }
                    else
                    {                        
                        //Done Add Attancance 
                        Command = "select [Enrollment No_] ,[Student No_],[Student Name],sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)+ sum([Absent])) Total,[Event Type],css,(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler', CASE WHEN SUM(CASE WHEN [ECA TYPE] = '' THEN 0 ELSE 1 END) = 0 THEN '' WHEN SUM(CASE WHEN [ECA TYPE] = 0 THEN 1 ELSE 0 END) > 0 THEN 'Pending' WHEN SUM(CASE WHEN [ECA TYPE] = 1 THEN 1 ELSE 0 END) > 0 THEN 'Approved' ELSE '' END AS 'ECA' , CASE WHEN (SUM(Present) + SUM([Absent])) = 1 THEN CASE WHEN SUM([Absent]) = 0 THEN 'P' ELSE 'A' END ELSE '' END AS Attancance from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,0 as [Absent],0 as [Attendance Type],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] where  [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type],[ECA TYPE]  union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],0 as Present, count([Attendance Type]) as [Absent],1 as [Attendance Type] ,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + "  and Remedial in (" + ddlTypeClass.SelectedValue + ") group by [Student No_],[Student Name],[Event Type],[ECA TYPE] ) as p where  [Student No_] in(select [Student No_] from [TMU$Student Attendance Line - COL] where  [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    and [Staff Code]='" + Session["uid"].ToString() + "'" + DateFilter + subValue + "  and Remedial in (" + ddlTypeClass.SelectedValue + ") )  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";
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
                            // btn_EC.Visible = true;

                            hfR_Session.Value = (ddlAcademicYear.SelectedIndex != 0 ? ddlAcademicYear.SelectedItem.Text : "");
                            hfR_Course.Value = (drpCourse.SelectedIndex != 0 ? drpCourse.SelectedItem.Text : "");
                            hfR_SemYear.Value = (drpSemester != null && drpSemester.SelectedItem != null && drpSemester.SelectedIndex != 0) ? drpSemester.SelectedItem.Text : "";
                            hfR_Section.Value = (drpSection != null && drpSection.SelectedItem != null && drpSection.SelectedIndex != 0) ? drpSection.SelectedItem.Text : "";
                            hfR_Subject.Value = (drpSubject != null && drpSubject.SelectedItem != null && drpSubject.SelectedIndex != 0) ? drpSubject.SelectedItem.Text : "";
                            hfR_Group.Value = (ddlGroup != null && ddlGroup.SelectedItem != null && ddlGroup.SelectedIndex != 0) ? ddlGroup.SelectedItem.Text : "";
                            hfR_Batch.Value = (ddlBatch != null && ddlBatch.SelectedItem != null && ddlBatch.SelectedIndex != 0) ? ddlBatch.SelectedItem.Text : "";
                            hfR_FromDate.Value = txtDateFrom.Text;
                            hfR_ToDate.Value = txtDateTo.Text;
                        }
                        else
                        {
                            btnExport.Visible = false;
                            //  btn_EC.Visible = false;
                        }
                    }
                }
            }
            else
            {

                //if (TypeOfCourse == "1") //Remedial=0 --- by Sandeep on 14-12-2016
                //{
                //    if (Chkdetained.Checked == true)
                //    {
                //        Command = "select [Enrollment No_] ,[Student No_],[Student Name],CASE WHEN SUM(Present) = 0 THEN 'P' WHEN SUM(Present) = 1 THEN 'A' END AS Present, CASE WHEN SUM([Absent]) = 0 THEN 'P' WHEN SUM(Absent) = 1 THEN 'A' END AS Absent,CASE WHEN MAX(Attendance) = 0 THEN 'P' ELSE 'A' END AS Attancance,(sum(Present)+ sum([Absent])) Total,[Event Type],css,CASE WHEN (SUM(Present)+SUM([Absent]))>0 THEN CAST((SUM(Present)*100.0)/(SUM(Present)+SUM([Absent])) AS DECIMAL(10,2)) ELSE CAST(0 AS DECIMAL(10,2)) END AS Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',CASE WHEN SUM(CASE WHEN p.[ECA TYPE] = '' THEN 0 ELSE 1 END) = 0 THEN '' WHEN SUM(CASE WHEN p.[ECA TYPE] = 0 THEN 1 ELSE 0 END) > 0 THEN 'Pending' WHEN SUM(CASE WHEN p.[ECA TYPE] = 1 THEN 1 ELSE 0 END) > 0 THEN 'Approved' ELSE '' END AS 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],[Attendance Type] as Attendance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH (NOLOCK) where [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and    [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + subValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1    group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE] union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE]  from [TMU$Student Attendance Line - COL] where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + "   and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1   group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";
                //    }
                //    else
                //    {
                //        Command = "select [Enrollment No_] ,[Student No_],[Student Name],CASE WHEN SUM(Present) = 0 THEN 'P' WHEN SUM(Present) = 1 THEN 'A' END AS Present, CASE WHEN SUM([Absent]) = 0 THEN 'P' WHEN SUM(Absent) = 1 THEN 'A' END AS Absent,CASE WHEN MAX(Attendance) = 0 THEN 'P' ELSE 'A' END AS Attancance,(sum(Present)+ sum([Absent])) Total,[Event Type],css,CASE WHEN (SUM(Present)+SUM([Absent]))>0 THEN CAST((SUM(Present)*100.0)/(SUM(Present)+SUM([Absent])) AS DECIMAL(10,2)) ELSE CAST(0 AS DECIMAL(10,2)) END AS Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',CASE WHEN SUM(CASE WHEN p.[ECA TYPE] = '' THEN 0 ELSE 1 END) = 0 THEN '' WHEN SUM(CASE WHEN p.[ECA TYPE] = 0 THEN 1 ELSE 0 END) > 0 THEN 'Pending' WHEN SUM(CASE WHEN p.[ECA TYPE] = 1 THEN 1 ELSE 0 END) > 0 THEN 'Approved' ELSE '' END AS 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],[Attendance Type] as Attendance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH (NOLOCK) where [Attendance Type]=0 and  [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and    [Subject Code] like '" + drpSubject.SelectedValue + "%'  " + AsPrincipal + DateFilter + subValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ") group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE] union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] where [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + "   and Remedial in (" + ddlTypeClass.SelectedValue + ")  group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE] ) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";
                //    }
                //}
                //if (TypeOfCourse == "2")  //add and [Group Code]='" + ddlGroup.SelectedValue + "' and [Batch Code]='" + ddlBatch.SelectedValue + "' by ashu on 28-08-2016
                //{
                //    if (Chkdetained.Checked == true)
                //    {
                //        Command = "select [Enrollment No_] ,[Student No_],[Student Name],CASE WHEN SUM(Present) = 0 THEN 'P' WHEN SUM(Present) = 1 THEN 'A' END AS Present, CASE WHEN SUM([Absent]) = 0 THEN 'P' WHEN SUM(Absent) = 1 THEN 'A' END AS Absent,CASE WHEN MAX(Attendance) = 0 THEN 'P' ELSE 'A' END AS Attancance,(sum(Present)+ sum([Absent])) Total,[Event Type],css,CASE WHEN (SUM(Present)+SUM([Absent]))>0 THEN CAST((SUM(Present)*100.0)/(SUM(Present)+SUM([Absent])) AS DECIMAL(10,2)) ELSE CAST(0 AS DECIMAL(10,2)) END AS Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',CASE WHEN SUM(CASE WHEN p.[ECA TYPE] = '' THEN 0 ELSE 1 END) = 0 THEN '' WHEN SUM(CASE WHEN p.[ECA TYPE] = 0 THEN 1 ELSE 0 END) > 0 THEN 'Pending' WHEN SUM(CASE WHEN p.[ECA TYPE] = 1 THEN 1 ELSE 0 END) > 0 THEN 'Approved' ELSE '' END AS 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],[Attendance Type] as Attendance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH (NOLOCK) where [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'  " + AsPrincipal + DateFilter + subValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1  group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE] union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE]  from [TMU$Student Attendance Line - COL] where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + "  and Remedial in (" + ddlTypeClass.SelectedValue + ") and Detained=1 group by [Student No_],[Student Name] ,[Event Type],[Attendance Type],[ECA TYPE]) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";
                //    }
                //    else
                //    {
                //        Command = "select [Enrollment No_] ,[Student No_],[Student Name],CASE WHEN SUM(Present) = 0 THEN 'P' WHEN SUM(Present) = 1 THEN 'A' END AS Present, CASE WHEN SUM([Absent]) = 0 THEN 'P' WHEN SUM(Absent) = 1 THEN 'A' END AS Absent,CASE WHEN MAX(Attendance) = 0 THEN 'P' ELSE 'A' END AS Attancance,(sum(Present)+ sum([Absent])) Total,[Event Type],css,CASE WHEN (SUM(Present)+SUM([Absent]))>0 THEN CAST((SUM(Present)*100.0)/(SUM(Present)+SUM([Absent])) AS DECIMAL(10,2)) ELSE CAST(0 AS DECIMAL(10,2)) END AS Percentage,case when Hosteler=1 then 'Yes' else 'No' end 'Hosteler',CASE WHEN SUM(CASE WHEN p.[ECA TYPE] = '' THEN 0 ELSE 1 END) = 0 THEN '' WHEN SUM(CASE WHEN p.[ECA TYPE] = 0 THEN 1 ELSE 0 END) > 0 THEN 'Pending' WHEN SUM(CASE WHEN p.[ECA TYPE] = 1 THEN 1 ELSE 0 END) > 0 THEN 'Approved' ELSE '' END AS 'ECA' from (select (select  [Enrollment No_] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],count([Attendance Type] ) as Present,'' as [Absent],[Attendance Type] as Attendance,case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE] from [TMU$Student Attendance Line - COL] WITH (NOLOCK) where [Attendance Type]=0 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'   " + AsPrincipal + DateFilter + subValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ")   group by [Student No_],[Student Name],[Event Type],[Attendance Type],[ECA TYPE] union select (select  [Enrollment No_] from [TMU$Student - COLLEGE] where No_=[Student No_]) as [Enrollment No_], [Student No_],[Student Name],'0' as Present, count([Attendance Type]) as [Absent],case [Event Type] when 'Others' then '' else [Event Type] end as [Event Type],case [Event Type] when 'Others' then 'CSS2' when '' then 'CSS2' else 'CSS1' end as css,(select  [Hostel Alloted] from [TMU$Student - COLLEGE] WITH(NOLOCK) where No_=[Student No_]) as 'Hosteler',[ECA TYPE]  from [TMU$Student Attendance Line - COL] where  [Attendance Type]=1 and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'    " + AsPrincipal + DateFilter + subValue + " and Remedial in (" + ddlTypeClass.SelectedValue + ") group by [Student No_],[Student Name] ,[Event Type],[Attendance Type],[ECA TYPE]) as p  group by [Student No_],[Student Name],[Enrollment No_],[Event Type],css,Hosteler order by [Enrollment No_] ";
                //    }
                //}
                return;
            }

            ////SqlCommand cmd = new SqlCommand("proc_GetStudentDetailsForReviewAttendance", con);
            //SqlCommand cmd = new SqlCommand(Command, con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //cmd.CommandTimeout = 50000;
            //DataTable dt = new DataTable();
            //con.Open();
            //da.Fill(dt);
            //con.Close();
            //grdStudentAttendance.DataSource = dt;
            //grdStudentAttendance.DataBind();
            //if (dt.Rows.Count > 0)
            //{
            //    btnExport.Visible = true;
            //    btn_EC.Visible = true;

            //    hfR_Session.Value = (ddlAcademicYear.SelectedIndex != 0 ? ddlAcademicYear.SelectedItem.Text : "");
            //    hfR_Course.Value = (drpCourse.SelectedIndex != 0 ? drpCourse.SelectedItem.Text : "");
            //    hfR_SemYear.Value = (drpSemester != null && drpSemester.SelectedItem != null && drpSemester.SelectedIndex != 0 ? drpSemester.SelectedItem.Text : "");
            //    hfR_Section.Value = (drpSection != null && drpSection.SelectedItem != null && drpSection.SelectedIndex != 0 ? drpSection.SelectedItem.Text : "");
            //    hfR_Subject.Value = (drpSubject != null && drpSubject.SelectedItem != null && drpSubject.SelectedIndex != 0 ? drpSubject.SelectedItem.Text : "");
            //    hfR_Group.Value = (ddlGroup != null && ddlGroup.SelectedItem != null && ddlGroup.SelectedIndex != 0 ? ddlGroup.SelectedItem.Text : "");
            //    hfR_Batch.Value = (ddlBatch != null && ddlBatch.SelectedItem != null && ddlBatch.SelectedIndex != 0 ? ddlBatch.SelectedItem.Text : "");
            //    hfR_FromDate.Value = txtDateFrom.Text;
            //    hfR_ToDate.Value = txtDateTo.Text;
            //}
            //else
            //{
            //    btnExport.Visible = false;
            //    btn_EC.Visible = false;
            //}
        }
        catch (Exception ex) { }
    }

    protected void btn_EC_Click1(object sender, EventArgs e)
    {
        System.Data.DataTable dt = new System.Data.DataTable();

        dt.Columns.Add("StudentNo");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Absent");
        // dt.Columns.Add("Subject");
        foreach (GridViewRow row in grdStudentAttendance.Rows)
        {
            CheckBox chk = row.FindControl("chkEC_ATT") as CheckBox;

            if (chk != null && chk.Checked)
            {
                Label lblStudent = row.FindControl("lblStudent") as Label;
                Label lblStudentName = row.FindControl("lblStudentName") as Label;
                Label lblAbsent = row.FindControl("lblAbsent") as Label;
                HiddenField HfAbsent1 = row.FindControl("HfAbsent1") as HiddenField;
                if (lblStudent != null && lblStudentName != null)
                {
                    System.Data.DataRow dr = dt.NewRow();

                    dr["StudentNo"] = lblStudent.Text.Trim();
                    dr["StudentName"] = lblStudentName.Text.Trim();
                    dr["Absent"] = HfAbsent1.Value.Trim();
                    // dr["Subject"] = drpSubject.SelectedItem.Text;

                    dt.Rows.Add(dr);
                }
            }
        }

        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(
                this, this.GetType(), "Key",
                "alert('Please select at least one Student.');", true);
            return;
        }

        md_grd_CE.DataSource = dt;
        md_grd_CE.DataBind();

        md_Student_EC_Details.Show();
        up_StudentListEC.Update();

    }


    protected void btn_EC_Click(object sender, EventArgs e)
    {
        try
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("StudentNo");
            dt.Columns.Add("StudentName");
            dt.Columns.Add("Absent");

            foreach (GridViewRow row in grdStudentAttendance.Rows)
            {
                CheckBox chk = row.FindControl("chkEC_ATT") as CheckBox;
                Label lblStudent = row.FindControl("lblStudent") as Label;
                Label lblStudentName = row.FindControl("lblStudentName") as Label;
                Label lblECAtatus = row.FindControl("lblECA1") as Label;
                HiddenField HfAbsent1 = row.FindControl("HfAbsent1") as HiddenField;

                if (chk == null || !chk.Checked)
                    continue;

                if (lblStudent == null || lblStudentName == null || HfAbsent1 == null)
                    continue;

                string status = lblECAtatus == null ? "" : lblECAtatus.Text.Trim();
                string absent = HfAbsent1.Value.Trim();

                // Already applied students ko skip karein
                if ((absent == "A" && status == "Pending") || (absent == "P" && status == "Approved"))
                    continue;

                System.Data.DataRow dr = dt.NewRow();
                dr["StudentNo"] = lblStudent.Text.Trim();
                dr["StudentName"] = lblStudentName.Text.Trim();
                dr["Absent"] = absent;

                dt.Rows.Add(dr);
            }

            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(), "Key",
                    "alert('Please select at least one eligible Student.');", true);
                return;
            }

            md_grd_CE.DataSource = dt;
            md_grd_CE.DataBind();

            md_Student_EC_Details.Show();
            up_StudentListEC.Update();
        }
        catch { }
    }


    protected void grdStudentAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkEC_ATT = (CheckBox)e.Row.FindControl("chkEC_ATT");
            Label lblECA1 = (Label)e.Row.FindControl("lblECA1");
            Label lblDeliveredLecture = (Label)e.Row.FindControl("lblDeliveredLecture");
            HiddenField HfAbsent1 = (HiddenField)e.Row.FindControl("HfAbsent1");

            chkEC_ATT.Visible = true;

            if (HfAbsent1.Value == "A" && lblECA1.Text == "")
            {
                chkEC_ATT.Checked = false;
                chkEC_ATT.Enabled = true;
            }
            else if (HfAbsent1.Value == "A" && lblECA1.Text == "Pending")
            {
                chkEC_ATT.Checked = true;
                chkEC_ATT.Enabled = false;
                lblECA1.CssClass = "ec-status ec-pending";
            }
            else if (HfAbsent1.Value == "P" && lblECA1.Text == "Approved")
            {
                chkEC_ATT.Checked = false;
                chkEC_ATT.Visible = false;
                lblECA1.CssClass = "ec-status ec-approved";
            }
            else if (HfAbsent1.Value == "P" && lblECA1.Text == "")
            {
                chkEC_ATT.Visible = false;
                chkEC_ATT.Checked = false;
            }

            int deliveredLecture = 0;

            int.TryParse(lblDeliveredLecture.Text.Trim(), out deliveredLecture);

            if (deliveredLecture == 1)
            {
                grdStudentAttendance.Columns[5].Visible = false;
                grdStudentAttendance.Columns[6].Visible = false;
                grdStudentAttendance.Columns[7].Visible = true;
                grdStudentAttendance.Columns[10].Visible = true;
                grdStudentAttendance.Columns[11].Visible = true;
                btn_EC.Visible = true;
            }
            else
            {
                grdStudentAttendance.Columns[5].Visible = true;
                grdStudentAttendance.Columns[6].Visible = true;
                grdStudentAttendance.Columns[7].Visible = false;
                grdStudentAttendance.Columns[10].Visible = false;
                grdStudentAttendance.Columns[11].Visible = false;
                btn_EC.Visible = false;
            }
        }
    }
    

    protected void btn_Reassign_Click(object sender, EventArgs e)
    {
        bool isChecked = false;
        string filePath = "";

        if (txtRemarks.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "UploadAlert", "alert('Please enter remarks.');", true);
            md_Student_EC_Details.Show();
            return;
        }
        if (!fuAttachment.HasFile)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please upload document.');", true);
            md_Student_EC_Details.Show();
            return;
        }

        if (fuAttachment.HasFile)
        {
            string ext = System.IO.Path.GetExtension(fuAttachment.FileName);
            if (ext.ToLower() != ".pdf")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Only PDF files are allowed.');", true);
                md_Student_EC_Details.Show();
                return;
            }


            // string fileName = System.IO.Path.GetFileName(fuAttachment.PostedFile.FileName);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(fuAttachment.FileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
            fuAttachment.SaveAs(Server.MapPath("~/Uploads/ReviewAttandance/" + fileName));
            filePath = "~/Uploads/ReviewAttandance/" + fileName;
        }
        string CollageType = "";
        if (chkOpen.Checked == true)
        {
            //if (con.State == ConnectionState.Closed)
            //    con.Open();
            //SqlCommand cmd2 = new SqlCommand("select Distinct ([Global Dimension 1 Code])as GlobalDimension1 from [TMU$Course Subject Line - COLLEGE] where [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Subject Type]='OPENELEC' and [Subject Code]='" + drpSubject.SelectedValue + "' and [Faculty Code]='" + Session["uid"].ToString() + "'", con);
            // CollageType = cmd2.ExecuteScalar().ToString();
            //con.Close();

            if (con.State == ConnectionState.Closed) con.Open();
            SqlCommand cmd2 = new SqlCommand("select Distinct ([Global Dimension 1 Code]) as GlobalDimension1 from [TMU$Course Subject Line - COLLEGE] where [Academic Year]='" + ddlAcademicYear.SelectedValue + "' and [Subject Type]='OPENELEC' and [Subject Code]='" + drpSubject.SelectedValue + "' and [Faculty Code]='" + Session["uid"].ToString() + "'", con);
            object result = cmd2.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                CollageType = result.ToString();
            }
            else
            {
                CollageType = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data not found');", true);
                return;
            }
            con.Close();
        }
        else
        {
            CollageType = Session["GlobalDimension1Code"].ToString().Trim();
        }
        foreach (GridViewRow row in md_grd_CE.Rows)
        {

            isChecked = true;
            Label lblStudent = row.FindControl("lblStudent") as Label;
            Label lblStudentName = row.FindControl("lblStudentName") as Label;
            Label lblAbsent = row.FindControl("lblAbsent") as Label;
            HiddenField HfAbsent1 = row.FindControl("HfAbsent1") as HiddenField;

            using (SqlCommand cmd = new SqlCommand("SP_Insert_Tbl_StudentAttendanceLine_Record", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@SubjectNo", drpSubject.SelectedValue);
                cmd.Parameters.AddWithValue("@StudentCode", lblStudent.Text);
                cmd.Parameters.AddWithValue("@StudentName", lblStudentName.Text);
                cmd.Parameters.AddWithValue("@Date", txtDateFrom.Text);
                cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
                cmd.Parameters.AddWithValue("@FilePath", filePath);
                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim());
                cmd.Parameters.AddWithValue("@College", CollageType);//Session["GlobalDimension1Code"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue.Trim());

                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd.ExecuteNonQuery();
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Submitted Successfully..');", true);
        if (chkOpen.Checked == true)
        {
            bindGridOpen();
        }
        else
        {
            bindGrid();
        }
        UpdatePanel1.Update();
        if (!isChecked)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select at least one Student.');", true);
            md_Student_EC_Details.Show();
            return;
        }
    }   
}
