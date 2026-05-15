using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
public partial class Faculty_EventTypeReport : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAcademicYear();
            bindDrpCourseList();
           
            GetCollegeCodeForprincipal();
            BinddlEventType();
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
    public void BinddlEventType()
    {
        SqlCommand cmd = new SqlCommand("Sp_getEventattedance_Report", con); //ok
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlEventType.DataSource = dt;
        ddlEventType.DataTextField = "Details";
        ddlEventType.DataValueField = "Value";
        ddlEventType.DataBind();


    }
  
    public void bindDrpCourseList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetCourse_RoleMatrix", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", Session["uid"].ToString());
            cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            ddlCourse.DataSource = dt;
            ddlCourse.DataTextField = "Details";
            ddlCourse.DataValueField = "No_";
            ddlCourse.DataBind();
        }
        catch (Exception) { }
    }
    public void GetCollegeCodeForprincipal()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_GetCollegeCodeforprincipal", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", Session["uid"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            DdlCollege.DataSource = dt;
            DdlCollege.DataTextField = "Details";
            DdlCollege.DataValueField = "No_";
            DdlCollege.DataBind();
        }
        catch (Exception ex) { }
    }


    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCollegeCodeForprincipal();
        bindDrpCourseList();
        getgriddata();
    }

  

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        getgriddata();
    }

    protected void DdlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();
        getgriddata();
    }

    public void getgriddata()
    {

        try
        {
            SqlCommand cmd = new SqlCommand("Sp_GetEventTypeReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", DdlCollege.SelectedValue);
            cmd.Parameters.AddWithValue("@CourseCode", ddlCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@EventType", ddlEventType.SelectedValue);
            cmd.Parameters.AddWithValue("@FromDate", txtDateFrom.Text);
            cmd.Parameters.AddWithValue("@ToDate", txtDateTo.Text);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);

            con.Close();
            GrdEventType.DataSource = dt;
            GrdEventType.DataBind();
            if (dt.Rows.Count > 0)
            {
                BtnPrint.Visible = true;
            }
            else
            {
                BtnPrint.Visible = false;
            }


        }
        catch (Exception) { }
    }
}