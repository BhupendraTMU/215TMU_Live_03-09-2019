using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

public partial class Faculty_TeachersFeedback : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ConnectionString);
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //SqlCommand cmd = new SqlCommand("select * from SyllabusFeedback where StudentNo='" + Session["uid"].ToString() + "' and AcademicYear='" + Session["AcademicYear"].ToString() + "' and ([Semester]='" + Session["Semester"].ToString() + "' or [Semester]='" + Session["Year"].ToString() + "')", con);
                //DataTable dt = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    Student.Visible = false;
                //    pnlError.Visible = true;
                //}
                //else
                //{
                    Student.Visible = true;
                    pnlError.Visible = false;
                    BindEmployeeDetail(Session["uid"].ToString());
                    BindAssessmentTable("");
                    bindDrpCourseList();
                 
                   
                //}
            }
            

        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    public void BindEmployeeDetail(string ID)
    {
        string ProcName = "";
        ProcName = "Get_TeacherDetails";
        SqlCommand cmd = new SqlCommand(ProcName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        lblCLG.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
        //lblPRG.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
       


    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        drpProgram.DataSource = dt;
        drpProgram.DataTextField = "Details";
        drpProgram.DataValueField = "No_";
        drpProgram.DataBind();
    }
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_RoleTemp", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 1000000;
        cmd.Parameters.Add("@ID1", Session["uid"].ToString());
        cmd.Parameters.Add("@ID", drpProgram.SelectedValue);
        cmd.Parameters.Add("@AcYear", "21-22");
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        con.Open();
        da.Fill(dt);
        con.Close();
        drpSession.DataSource = dt;
        drpSession.DataTextField = "Details";
        drpSession.DataValueField = "No_";
        drpSession.DataBind();
    }

    public void BindAssessmentTable(string d)
    {
       
        string ProcName = "";
        ProcName = "Get_TeacherFeedbackQuestion";
        SqlCommand cmd = new SqlCommand(ProcName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        grdData.DataSource = dt;
        grdData.DataBind();
       
        

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void drpProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
    }
  
    //public void bindSubject()
    //{
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand("sp_getSubjrctCodeFacultyWise", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
    //        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedItem.Value.ToString());
    //        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
    //        cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());
    //        if (rdInternal.Checked == true)
    //        {
    //            cmd.Parameters.Add("@ExamType", "Internal");
    //        }
    //        else
    //        {
    //            cmd.Parameters.Add("@ExamType", "External");
    //        }


    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);
    //        ddlSubject.DataSource = dt;
    //        ddlSubject.DataTextField = "Description";
    //        ddlSubject.DataValueField = "Subject Code";
    //        ddlSubject.DataBind();
    //        ddlSubject.Items.Insert(0, "--Course--");
    //    }
    //    catch (Exception ex) { }
    //}
}