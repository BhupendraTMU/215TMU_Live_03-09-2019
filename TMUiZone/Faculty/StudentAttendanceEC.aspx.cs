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

public partial class Faculty_StudentAttendanceEC : System.Web.UI.Page
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
               
               

                lblFacultyCode.Text = Session["uid"].ToString();
              
                lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
                txtDate.Text = Session["Date"].ToString();
                bindAcademicYear();
                bindDrpCourseList();


                if (Session["GlobalDimension1Code"].ToString() == "TMDC")
                {
                    chkboxEditAttendance.Visible = false;
                    chkMultiplaeAttendance.Visible = false;
                }
                else
                {
                    chkboxEditAttendance.Visible = false;
                    chkMultiplaeAttendance.Visible = true;
                }
                
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
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
    public void bindDrpCourseList()
    {
        DataTable dt = new DataTable();
      
        dt = FDL.GetCourseListFromTimeTable(lblFacultyCode.Text, Session["GlobalDimension1Code"].ToString(), txtDate.Text); 
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
}