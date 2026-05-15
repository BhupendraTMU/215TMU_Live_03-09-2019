using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class FA_Mentor_For_Mentee : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCourses(Session["uid"].ToString().Trim(), Session["GlobalDimension1Coded"].ToString().Trim());
            GetAcademicYearData();
            SP_GetData_MenterFor_Mentee(txt_mentorFormentee_studentEnrollmentName.Text.Trim(), ddl_mentorFormentee_course.Text.Trim(), ddl_mentorFormentee_academicYear.Text.Trim(), Session["uid"].ToString().Trim());


        }
    }

    private void BindCourses(string loginID, string SP_FA_MM_Get_CourseCode)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dtcourse = con.SP_FA_MM_Get_CourseCode(loginID.Trim(), SP_FA_MM_Get_CourseCode.Trim());

        ddl_mentorFormentee_course.DataSource = dtcourse;
        ddl_mentorFormentee_course.DataTextField = "Course Name";
        ddl_mentorFormentee_course.DataValueField = "Course Code";
        ddl_mentorFormentee_course.DataBind();
        dtcourse.Close();
        con.DisConnect();


    }

    private void GetAcademicYearData()
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_MM_Get_Academic_Year();
        DataTable dt = new DataTable();
        dt.Load(dr);
        ddl_mentorFormentee_academicYear.DataSource = dt;
        ddl_mentorFormentee_academicYear.DataTextField = "Academic Year";
        ddl_mentorFormentee_academicYear.DataValueField = "Academic Year";
        ddl_mentorFormentee_academicYear.DataBind();
        dr.Close();
        con.DisConnect();

    }
    protected void btn_mentorFormentee_get_Click(object sender, EventArgs e)
    {
        SP_GetData_MenterFor_Mentee(txt_mentorFormentee_studentEnrollmentName.Text.Trim(), ddl_mentorFormentee_course.Text.Trim(), ddl_mentorFormentee_academicYear.Text.Trim(), Session["uid"].ToString().Trim());
    }
    public void SP_GetData_MenterFor_Mentee(string StudentName, string CourseCode, string AcademicYear, string MentorID)
    {
        DataTable dt = new DataTable();
        pms_connection con = new pms_connection();
        SqlDataReader dr;
        try
        {

            dr = con.SP_GetData_MenterFor_Mentee(StudentName, CourseCode, AcademicYear, MentorID);  // Fetch data using your existing stored procedure

            if (dr != null)
            {
                dt.Load(dr);  // Load data into a DataTable for reuse
            }
            if (dt != null)
            {
                grdView_mentorForMentee.DataSource = dt;
                grdView_mentorForMentee.DataBind();
            }
            dr.Close();

        }
        catch (Exception ex)
        {
            Response.Write(ex);
            throw;
        }

        finally
        {
            con.DisConnect();
        }

    }

    protected void btn_mentorForMentee_MMrecord_Click(object sender, EventArgs e)
    {
        // Cast the sender's NamingContainer to GridViewRow
        GridViewRow parentRow = (GridViewRow)((Control)sender).NamingContainer;

        // Find the Label within the current row
        Label lblGrd_menterForMentee_studentName = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentName");
        Label lbl_AutoNo = (Label)parentRow.FindControl("lbl_AutoNo");
        Label lblGrd_menterForMentee_studentno = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentno");


        Session["MM_StudentName"] = lblGrd_menterForMentee_studentName.Text.Trim();
        Session["MM_AutoNo"] = lbl_AutoNo.Text.Trim();
        Session["MM_StudentId"] = lblGrd_menterForMentee_studentno.Text.Trim();
        Session["MM_Course"] = ddl_mentorFormentee_course.SelectedValue.Trim();
        Session["MM_AcademicYear"] = ddl_mentorFormentee_academicYear.Text.Trim();



        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('FA_MM_Meeting_Record.aspx','_newtab');", true);

    }

    protected void btn_mentorForMentee_recordCoActivities_Click(object sender, EventArgs e)
    {
        GridViewRow parentRow = (GridViewRow)((Control)sender).NamingContainer;

        // Find the Label within the current row
        Label lblGrd_menterForMentee_studentName = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentName");
        Label lbl_AutoNo = (Label)parentRow.FindControl("lbl_AutoNo");
        Label lblGrd_menterForMentee_studentno = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentno");

        Session["MM_StudentName"] = lblGrd_menterForMentee_studentName.Text.Trim();
        Session["MM_AutoNo"] = lbl_AutoNo.Text.Trim();
        Session["MM_StudentId"] = lblGrd_menterForMentee_studentno.Text.Trim();
        Session["MM_Course"] = ddl_mentorFormentee_course.SelectedValue.Trim();
        Session["MM_AcademicYear"] = ddl_mentorFormentee_academicYear.Text.Trim();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('FA_Record_Co_Extra_Curricular.aspx','_newtab');", true);

    }

    protected void btn_Students_Assessment_Click(object sender, EventArgs e)
    {
        GridViewRow parentRow = (GridViewRow)((Control)sender).NamingContainer;

        // Find the Label within the current row
        Label lblGrd_menterForMentee_studentName = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentName");
        Label lbl_AutoNo = (Label)parentRow.FindControl("lbl_AutoNo");
        Label lblGrd_menterForMentee_studentno = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentno");

        Session["MM_StudentName"] = lblGrd_menterForMentee_studentName.Text.Trim();
        Session["MM_AutoNo"] = lbl_AutoNo.Text.Trim();
        Session["MM_StudentId"] = lblGrd_menterForMentee_studentno.Text.Trim();
        Session["MM_AcademicYear"] = ddl_mentorFormentee_academicYear.SelectedValue.Trim();
        Session["MM_Course"] = ddl_mentorFormentee_course.SelectedValue.Trim();



        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('FA_MM_Students_Assessment.aspx','_newtab');", true);

    }

    protected void btn_mentorForMentee_studentBymentee_Click(object sender, EventArgs e)
    {
        GridViewRow parentRow = (GridViewRow)((Control)sender).NamingContainer;

        // Find the Label within the current row
        Label lblGrd_menterForMentee_studentName = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentName");
        Label lbl_AutoNo = (Label)parentRow.FindControl("lbl_AutoNo");
        Label lblGrd_menterForMentee_studentno = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentno");

        Session["MM_StudentName"] = lblGrd_menterForMentee_studentName.Text.Trim();
        Session["MM_AutoNo"] = lbl_AutoNo.Text.Trim();
        Session["MM_StudentId"] = lblGrd_menterForMentee_studentno.Text.Trim();
        Session["MM_AcademicYear"] = ddl_mentorFormentee_academicYear.SelectedValue.Trim();
        Session["MM_Course"] = ddl_mentorFormentee_course.SelectedValue.Trim();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('FA_MM_Special_Needs_By_Mentee.aspx','_newtab');", true);

    }

    protected void btn_mentorForMentee_attendanceUndertaking_Click(object sender, EventArgs e)
    {
        GridViewRow parentRow = (GridViewRow)((Control)sender).NamingContainer;

        // Find the Label within the current row
        //Label lblGrd_menterForMentee_studentName = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentName");
        //Label lbl_AutoNo = (Label)parentRow.FindControl("lbl_AutoNo");
        Label lblGrd_menterForMentee_studentno = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentno");

        SqlCommand cmd = new SqlCommand("[SP_GetSemforUndertaking]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", lblGrd_menterForMentee_studentno.Text);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpSemester.DataSource = dt;
            drpSemester.DataTextField = "sem_year";
            drpSemester.DataValueField = "sem_year";
            drpSemester.DataBind();
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "sem_year";
            DropDownList1.DataValueField = "sem_year";
            DropDownList1.DataBind();

            


            bindGrid(lblGrd_menterForMentee_studentno.Text, "25-26", drpSemester.SelectedValue);
            BindDate(lblGrd_menterForMentee_studentno.Text, "25-26", drpSemester.SelectedValue);

        }





        

        //Session["MM_StudentName"] = lblGrd_menterForMentee_studentno.Text.Trim();
        //Session["MM_Academic_Year"] = ddl_mentorFormentee_academicYear.SelectedValue.Trim();
        //Session["MM_Course"] = ddl_mentorFormentee_course.SelectedValue.ToString();
        //Session["MM_Program"] = ddl_mentorFormentee_course.SelectedItem.ToString();

        //Session["MM_AutoNo"] = lbl_AutoNo.Text.Trim();



        //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('FA_MM_Attendance_Form.aspx','_newtab');", true);

    }


    public void bindGrid(string StudentNo, string val, string val1)
    {


        SqlCommand cmd = new SqlCommand("[EDUCOLLEGELIVE-R2].dbo.StudentAttendanceNew '" + StudentNo + "','" + val + "','" + val1 + "','--Select--'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdAttendanceReport.DataSource = dt;
            grdAttendanceReport.DataBind();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            grdAttendanceReport.DataSource = "";
            grdAttendanceReport.DataBind();
            GridView1.DataSource = "";
            GridView1.DataBind();
        }

    }
    public void BindDate(string StudentNo, string val, string val1)
    {

        try
        {
            SqlCommand cmd = new SqlCommand("HRMSPortal.dbo.SP_Get_UndertakingforFaculty", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Enrollment", StudentNo);
            cmd.Parameters.AddWithValue("@Sem", val1);
            cmd.Parameters.AddWithValue("@Year", val1);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtCL = new DataTable();
            da.Fill(dtCL);
            con.Close();
            if (dtCL.Rows.Count > 0)
            {
                studentName.Text = dtCL.Rows[0]["Student Name"].ToString();
                studentName1.InnerText = dtCL.Rows[0]["Student Name"].ToString();
                fatherName1.InnerText = dtCL.Rows[0]["Fathers Name"].ToString();
                program.Text = dtCL.Rows[0]["program"].ToString();
                branch.Text = dtCL.Rows[0]["Global Dimension 1 Code"].ToString();
                semester.Text = dtCL.Rows[0]["Semester"].ToString();
                CollegeDepartment.Text = dtCL.Rows[0]["College1"].ToString();
                studentMobile.Text = dtCL.Rows[0]["Mobile Number"].ToString();
                studentEmail.Text = dtCL.Rows[0]["E-Mail Address"].ToString();
                fatherEmail.Text = dtCL.Rows[0]["FatherEmail"].ToString();
                fatherMobile.Text = dtCL.Rows[0]["FatherMobileNo"].ToString();
                Date1.Text= dtCL.Rows[0]["SubmitDate"].ToString();
                TextBox1.Text = dtCL.Rows[0]["SubmitDate"].ToString();
                CheckBox1.Checked = true;
                CheckBox1.Enabled = false;
                btnPrint.Visible = true;

                if (dtCL.Rows[0]["Global Dimension 1 Code"].ToString() == "TMSN" || dtCL.Rows[0]["Global Dimension 1 Code"].ToString() == "TMNS")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal2').modal('show');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);
                }

               


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('hide');</script>", false);
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal2').modal('hide');</script>", false);
            }

        }
        catch (Exception ex)
        {

            throw new Exception("Error fetching pay slip requests: " + ex.Message, ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }


    protected void btn_All_MM_Records_Click1(object sender, EventArgs e)
    {

        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('FA_MM_All_Meeting_Records.aspx','_newtab');", true);

    }

    protected void btn_All_ActivityRecords_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('FA_MM_All_Activity_Records.aspx','_newtab');", true);

    }

    protected void btn_mentorForMentee_result_Click(object sender, EventArgs e)
    {
        GridViewRow parentRow = (GridViewRow)((Control)sender).NamingContainer;

        // Find the Label within the current row
        Label lblGrd_menterForMentee_studentName = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentName");
        Label lbl_AutoNo = (Label)parentRow.FindControl("lbl_AutoNo");

        Session["MM_StudentName"] = lblGrd_menterForMentee_studentName.Text.Trim();
        Session["MM_Course"] = ddl_mentorFormentee_course.SelectedValue.Trim();
        Session["MM_Academic_Years"] = ddl_mentorFormentee_academicYear.SelectedValue.Trim();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('FA_MM_Result_Report.aspx','_newtab');", true);

    }

    protected void btn_SpecialAchievements_Click(object sender, EventArgs e)
    {
        // Cast the sender's NamingContainer to GridViewRow
        GridViewRow parentRow = (GridViewRow)((Control)sender).NamingContainer;

        // Find the Label within the current row
        Label lblGrd_menterForMentee_studentName = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentName");
        Label lbl_AutoNo = (Label)parentRow.FindControl("lbl_AutoNo");
        Label lblGrd_menterForMentee_studentno = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentno");


        Session["MM_StudentName"] = lblGrd_menterForMentee_studentName.Text.Trim();
        Session["MM_AutoNo"] = lbl_AutoNo.Text.Trim();
        Session["MM_StudentId"] = lblGrd_menterForMentee_studentno.Text.Trim();
        Session["MM_Course"] = ddl_mentorFormentee_course.SelectedValue.Trim();
        Session["MM_AcademicYear"] = ddl_mentorFormentee_academicYear.Text.Trim();



        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('FA_MM_Special_Achievements.aspx','_newtab');", true);

    }

    protected void btn_mentorForMentee_viewReports_Click(object sender, EventArgs e)
    {
        GridViewRow parentRow = (GridViewRow)((Control)sender).NamingContainer;

        // Find the Label within the current row
        Label lblGrd_menterForMentee_studentName = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentName");
        Label lbl_AutoNo = (Label)parentRow.FindControl("lbl_AutoNo");
        Label lblGrd_menterForMentee_studentno = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentno");


        //Session["MM_StudentName"] = lblGrd_menterForMentee_studentName.Text.Trim();
        //Session["MM_AutoNo"] = lbl_AutoNo.Text.Trim();
        Session["MM_StudentId"] = lblGrd_menterForMentee_studentno.Text.Trim();
        Session["MM_Course"] = ddl_mentorFormentee_course.SelectedValue.Trim();
        Session["MM_AcademicYear"] = ddl_mentorFormentee_academicYear.Text.Trim();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('FA_MM_SOP_Mentor_Mentee_Report.aspx','_newtab');", true);

    }

    protected void btn_View_Attendance_Click(object sender, EventArgs e)
    {
        GridViewRow parentRow = (GridViewRow)((Control)sender).NamingContainer;

        // Find the Label within the current row
        Label lblGrd_menterForMentee_studentName = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentName");
        Label lbl_AutoNo = (Label)parentRow.FindControl("lbl_AutoNo");
        Label lblGrd_menterForMentee_studentno = (Label)parentRow.FindControl("lblGrd_menterForMentee_studentno");

        Session["MM_StudentName"] = lblGrd_menterForMentee_studentName.Text.Trim();
        Session["MM_AutoNo"] = lbl_AutoNo.Text.Trim();
        Session["MM_StudentId"] = lblGrd_menterForMentee_studentno.Text.Trim();
        Session["MM_AcademicYear"] = ddl_mentorFormentee_academicYear.SelectedValue.Trim();
        Session["MM_Course"] = ddl_mentorFormentee_course.SelectedValue.Trim();

        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('FA_MM_View_Attendance.aspx','_newtab');", true);

    }
}