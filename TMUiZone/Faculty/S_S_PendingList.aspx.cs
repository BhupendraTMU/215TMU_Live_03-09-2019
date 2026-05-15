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

using System.Text;

public partial class Faculty_S_S_PendingList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                bindDrpCourseList();
                bindAcademicYear();
                //bindSubject();
                //bindApprovalList();



            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
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
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        Session["UserRole"] = dt.Rows[0]["UserRole"].ToString();
        con.Close();
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
    }
    public void BindGrid()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_GetS_S_PengingList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);

            cmd.Parameters.AddWithValue("@LoginId", Session["uid"].ToString());


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GrdExamList.DataSource = dt;
            GrdExamList.DataBind();
            if (dt.Rows.Count > 0)
            {
                GrdExamList.Visible = true;
               
            }



        }
        catch (Exception ex)
        {
        }
    }
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con);//proc_GetSemesterFromCourseWiseFaculty_Role comment on 27-12-2017
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void GrdExamList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdExamList.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {

        try
        {

            SqlCommand cmd = new SqlCommand("Sp_GetS_S_PengingList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);

            cmd.Parameters.AddWithValue("@LoginId", Session["uid"].ToString());


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
           


        }
        catch (Exception ex)
        {
        }



        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        GridView1.RenderControl(htmlWrite);

        Response.Clear();
        string str = "StudentSurveyPendingLIst" ;
        Response.AddHeader("content-disposition", "attachment;filename=" + str + "" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
}