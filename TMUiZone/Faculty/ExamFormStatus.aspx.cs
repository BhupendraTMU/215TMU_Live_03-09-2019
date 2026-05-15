using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using Utility;
using System.Configuration;
using System;

public partial class Faculty_ExamFormStatus : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal conn;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Portalcon = new Connection();
        conn = new ServicePoratal();
        if (!IsPostBack)
        {
            //Sp_GetSemYearFilterByOddEven // Bind dropdown from Procedure new on 29-05-2018
            BindAcademicYear();
            bindDrpCourseList();
            BindSemYear();
            //bindStudentExamDetails();
        }
    }
    public void BindSemYear()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_GetSemYearFilterByOddEvenExamSpForStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", Session["uid"].ToString());
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
            if (Chekap.Checked == true)
            {
                cmd.Parameters.Add("@Examtype", "1");
            }
            else
            {
                cmd.Parameters.Add("@Examtype", "0");
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            ddlSem.DataSource = dt1;
            ddlSem.DataTextField = "Details";
            ddlSem.DataValueField = "Code";
            ddlSem.DataBind();
        }
        catch
        {
        }
    }
    public void bindDrpCourseList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetHODCourse_RoleMatrixForStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
          
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            ddlCourse.DataSource = dt;
            ddlCourse.DataTextField = "Details";
            ddlCourse.DataValueField = "No_";
            ddlCourse.DataBind();
            if (dt.Rows.Count < 2)
            {
                Response.Redirect("../Default.aspx");
            }
        }
        catch
        {
        }
    }
    public void BindAcademicYear()
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
            ddlAcademicYear.DataSource = dt1;
            ddlAcademicYear.DataTextField = "Details";
            ddlAcademicYear.DataValueField = "No_";
            ddlAcademicYear.DataBind();
        }
        catch
        {
        }
    }



    protected void Rblist_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSemYear();
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void BtnRejected_Click(object sender, EventArgs e)
    {

    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Chekap_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void GrdExamList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdExamList.PageIndex = e.NewPageIndex;
        bindStudentExamDetails();
    }
    public void bindStudentExamDetails()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_GetStudentExaminationDetailsforStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", ddlSem.SelectedValue);
            cmd.Parameters.AddWithValue("@Appear", ddlReaapear.SelectedValue);
            cmd.Parameters.AddWithValue("@LoginId", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());

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

            else
            {

            }

        }
        catch (Exception ex)
        {
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
   
    protected void btnReport_Click(object sender, EventArgs e)
    {
        GrdExamList.AllowPaging = false;

        bindStudentExamDetails();






        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        GrdExamList.RenderControl(htmlWrite);

        Response.Clear();
        string str = "ExamFormStatusReport";
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {

        bindStudentExamDetails();

    }
   
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void chkStudent_CheckedChanged(object sender, EventArgs e)
    {

    }
  
}