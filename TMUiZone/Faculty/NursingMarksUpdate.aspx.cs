using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_NursingMarksUpdate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {



                bindAcademicYear();
                bindDrpCourseList();


            }
        }
        catch (Exception ex)
        {
            // Response.Redirect("~/Default.aspx");
        }
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
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();

        bindSubject();


    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

        bindDrpSemesterList();
        //bindSubject();


    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSectionList();

        bindGroupList();

        bindSubject();






    }
    protected void ddlSubject_TextChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("proc_GetExamMethod", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@ExamType", ddlexamtype.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlexamMethod.DataSource = dt;
        ddlexamMethod.DataTextField = "Exam Method";
        ddlexamMethod.DataValueField = "No_";
        ddlexamMethod.DataBind();



    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseForMarksUpdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@Academic", drpAcademicYear.SelectedValue);
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
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterForMarksUpdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 1000000;
        cmd.Parameters.Add("@ID1", Session["uid"].ToString());
        cmd.Parameters.Add("@ID", drpCourse.SelectedValue);
        cmd.Parameters.Add("@AcYear", drpAcademicYear.SelectedValue);
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
            SqlCommand cmd = new SqlCommand("sp_getSubjectforMarksUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@ExamType", ddlexamtype.SelectedValue);
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
    public void bindSectionList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSectionforMarksUpdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedItem.Value.ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
        cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());
        cmd.Parameters.Add("@ExamType", ddlexamtype.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();
    }
    public void bindGroupList()
    {
        SqlCommand cmd = new SqlCommand("sp_GetGroupforMarkUpdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());

        cmd.Parameters.Add("@SectionCode", drpSection.SelectedValue);
        cmd.Parameters.Add("@ExamType", ddlexamtype.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "Details";
        ddlGroup.DataValueField = "No_";
        ddlGroup.DataBind();
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        bindGroupList();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string tableName = "";
        string marksColumn = "";

        if (ddlexamtype.SelectedValue == "0")
        {
            tableName = "[TMU$Student Internal Line -TMU]";
            marksColumn = "[Internal Assesment (IA) Marks]";
        }
        else
        {
            tableName = "[TMU$Student External Line - COL]";
            marksColumn = "[External Mark]";
        }

        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            con1.Open();

            SqlTransaction trans = con1.BeginTransaction();

            try
            {
                foreach (GridViewRow row in gvStudentMarks.Rows)
                {
                    Label lblPreviousMarks = (Label)row.FindControl("lblPreviousMarks");
                    TextBox txtIAMarks = (TextBox)row.FindControl("txtIAMarks");

                    string oldMarks = lblPreviousMarks.Text.Trim();
                    string newMarks = txtIAMarks.Text.Trim();

                    decimal oldMark;
                    if (decimal.TryParse(oldMarks, out oldMark))
                    {
                        oldMarks = oldMark.ToString("0.00");
                    }

                    decimal marks;
                    if (decimal.TryParse(newMarks, out marks))
                    {
                        newMarks = marks.ToString("0.00");
                    }

                    // History Insert
                    SqlCommand cmdHistory = new SqlCommand(@"
                INSERT INTO StudentMarksHistory
                (
                    DocumentNo,
                    EnrollmentNo,
                    AcademicYear,
                    Course,
                    SubjectCode,
                    ExamMethod,
                    ExamType,
                    OldMarks,
                    NewMarks,
                    UpdatedBy,
                    UpdatedOn
                )
                VALUES
                (
                    @DocumentNo,
                    @EnrollmentNo,
                    @AcademicYear,
                    @Course,
                    @SubjectCode,
                    @ExamMethod,
                    @ExamType,
                    @OldMarks,
                    @NewMarks,
                    @UpdatedBy,
                    GETDATE()
                )", con1, trans);

                    cmdHistory.Parameters.AddWithValue("@DocumentNo", gvStudentMarks.DataKeys[row.RowIndex]["Document No_"]);
                    cmdHistory.Parameters.AddWithValue("@EnrollmentNo", gvStudentMarks.DataKeys[row.RowIndex]["Enrollement No"]);
                    cmdHistory.Parameters.AddWithValue("@AcademicYear", gvStudentMarks.DataKeys[row.RowIndex]["Academic Year"]);
                    cmdHistory.Parameters.AddWithValue("@Course", gvStudentMarks.DataKeys[row.RowIndex]["Course"]);
                    cmdHistory.Parameters.AddWithValue("@SubjectCode", gvStudentMarks.DataKeys[row.RowIndex]["Subject Code"]);
                    cmdHistory.Parameters.AddWithValue("@ExamMethod", gvStudentMarks.DataKeys[row.RowIndex]["Exam Method"]);
                    cmdHistory.Parameters.AddWithValue("@ExamType", ddlexamtype.SelectedValue);
                    cmdHistory.Parameters.AddWithValue("@OldMarks", oldMarks);
                    cmdHistory.Parameters.AddWithValue("@NewMarks", newMarks);
                    cmdHistory.Parameters.AddWithValue("@UpdatedBy", Session["uid"].ToString());

                    cmdHistory.ExecuteNonQuery();

                    // Main Table Update
                    string query = "UPDATE " + tableName +
               " SET " + marksColumn + " = CASE    WHEN UPPER(@Marks) = 'AB'        THEN CAST(-1 AS DECIMAL(38,20))    ELSE CAST(@Marks AS DECIMAL(38,20)) END " +
               " WHERE [Document No_] = @DocumentNo " +
               " AND [Enrollement No] = @EnrollmentNo " +
               " AND [Academic Year] = @AcademicYear " +
               " AND Course = @Course " +
               " AND [Subject Code] = @SubjectCode " +
               " AND [Exam Method] = @ExamMethod";

                    SqlCommand cmdUpdate = new SqlCommand(query, con1, trans);

                    cmdUpdate.Parameters.AddWithValue("@Marks", newMarks);
                    cmdUpdate.Parameters.AddWithValue("@DocumentNo", gvStudentMarks.DataKeys[row.RowIndex]["Document No_"]);
                    cmdUpdate.Parameters.AddWithValue("@EnrollmentNo", gvStudentMarks.DataKeys[row.RowIndex]["Enrollement No"]);
                    cmdUpdate.Parameters.AddWithValue("@AcademicYear", gvStudentMarks.DataKeys[row.RowIndex]["Academic Year"]);
                    cmdUpdate.Parameters.AddWithValue("@Course", gvStudentMarks.DataKeys[row.RowIndex]["Course"]);
                    cmdUpdate.Parameters.AddWithValue("@SubjectCode", gvStudentMarks.DataKeys[row.RowIndex]["Subject Code"]);
                    cmdUpdate.Parameters.AddWithValue("@ExamMethod", gvStudentMarks.DataKeys[row.RowIndex]["Exam Method"]);

                    cmdUpdate.ExecuteNonQuery();
                }

                trans.Commit();

                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    "alert('Marks Updated Successfully.');", true);

                BindGrid();
            }
            catch (Exception ex)
            {
                trans.Rollback();

                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    "alert('" + ex.Message.Replace("'", "") + "');", true);
            }
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {

        BindGrid();





    }

    public void BindGrid()
    {
        SqlCommand cmd = new SqlCommand("proc_GetStudentFormethod", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@DocumentNo", ddlexamMethod.SelectedValue);
        cmd.Parameters.Add("@ExamType", ddlexamtype.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        gvStudentMarks.DataSource = dt;
        gvStudentMarks.DataBind();

    }


}