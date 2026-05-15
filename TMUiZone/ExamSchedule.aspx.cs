using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class ExamSchedule : System.Web.UI.Page
{
    TMUConnection con;
    string tbleStudentcollege = "[Ashoka University$Exam Schedule Line - COL]";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con = new TMUConnection();
            if (!IsPostBack)
            {

                Showsemester();
                showCourses();
                semester.Text = ddsesmester.Text;
            }
        }
        catch (Exception)
        {
        }
    }
    public void showCourses()
    {

        SqlDataReader odr = con.Show_ExamSchedule(tbleStudentcollege,ddsesmester.Text);
        DataTable Dt = new DataTable();
        Dt.Load(odr);
        grdCourse.DataSource = Dt;
        grdCourse.DataBind();
        odr.Close();
        con.DisConnect();
    }

    public void Showsemester()
    {

        SqlDataReader dr = con.Show_SemesterforLine(tbleStudentcollege);
        // dr.Read();
        ddsesmester.DataSource = dr;

        ddsesmester.DataTextField = "Semester";


        ddsesmester.DataBind();
        dr.Close();
        con.DisConnect();


    }
    protected void ddsesmester_SelectedIndexChanged(object sender, EventArgs e)
    {
        showCourses();
        semester.Text = ddsesmester.Text;

    }
}