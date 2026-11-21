using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_AttendancePost : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
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
        catch
        {
            Response.Redirect("../Default.aspx");
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
            ddlAcademicYear.DataSource = dt1;
            ddlAcademicYear.DataTextField = "Details";
            ddlAcademicYear.DataValueField = "No_";
            ddlAcademicYear.DataBind();
        }
        catch
        {
        }
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@Academic", ddlAcademicYear.SelectedValue);
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {


        SqlCommand cmd = new SqlCommand("proc_GetStudentAttendance", con);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd.Parameters.Add("@Year", ddlYear.SelectedValue);
        cmd.Parameters.Add("@Subject", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Lect", DropDownList1.SelectedValue);
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            lblTotal.Text = dt.Rows.Count.ToString();
            lblPresent.Text = dt.Select("AttendanceStatus='Present'").Length.ToString();
            lblAbsent.Text = dt.Select("AttendanceStatus='Absent'").Length.ToString();
            grdAttendance.DataSource = dt;
            grdAttendance.DataBind();
        }
        else
        {
            lblTotal.Text = "0";
            lblPresent.Text = "0";
            lblAbsent.Text = "0";
            grdAttendance.DataSource = "";
            grdAttendance.DataBind();
        }


    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        bindGetSubjectList();
    }
    public void bindGetSubjectList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromSchedule", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd.Parameters.Add("@CourseCode", ddlCourse.SelectedValue);
        cmd.Parameters.Add("@Year", ddlYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "SubjectName";
        drpCourse.DataValueField = "SubjectCode";
        drpCourse.DataBind();
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        DataTable dtAttendance = CreateAttendanceTable();

        foreach (GridViewRow row in grdAttendance.Rows)
        {
            DataRow dr = dtAttendance.NewRow();

            dr["Course Code"] = ddlCourse.SelectedValue;
            dr["Date"] = Convert.ToDateTime(txtDate.Text);
            dr["Hour"] = row.Cells[4].Text;
            dr["Staff Code"] = Session["uid"].ToString();
            dr["Academic Year"] = ddlAcademicYear.SelectedValue;
            dr["Subject Code"] = row.Cells[2].Text;
            dr["Global Dimension 1 Code"] = Session["GlobalDimension1Code"].ToString();
            dr["Subject Description"] = row.Cells[3].Text;
            dr["Year"] = ddlYear.SelectedValue;
            dr["Group"] = row.Cells[5].Text;
            Label lblAttendance = (Label)row.FindControl("lblAttendance");

            if (lblAttendance != null)
            {
                if (lblAttendance.Text.Trim() == "Present")
                {
                    dr["presentAbsentStatus"] = 0;
                }
                else
                {
                    dr["presentAbsentStatus"] = 1;
                }
            }
            else
            {
                dr["presentAbsentStatus"] = 1;
            }
            dr["Student No"] = row.Cells[0].Text;
            dr["Student Name"] = row.Cells[1].Text;

            dtAttendance.Rows.Add(dr);
        }


        SaveAttendance(dtAttendance);
    }
    private DataTable CreateAttendanceTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("Course Code", typeof(string));
        dt.Columns.Add("Date", typeof(DateTime));
        dt.Columns.Add("Hour", typeof(int));
        dt.Columns.Add("Staff Code", typeof(string));
        dt.Columns.Add("Academic Year", typeof(string));
        dt.Columns.Add("Subject Code", typeof(string));
        dt.Columns.Add("Global Dimension 1 Code", typeof(string));
        dt.Columns.Add("Subject Description", typeof(string));
        dt.Columns.Add("Year", typeof(string));
        dt.Columns.Add("presentAbsentStatus", typeof(string));
        dt.Columns.Add("Student No", typeof(string));
        dt.Columns.Add("Student Name", typeof(string));
        dt.Columns.Add("Group", typeof(string));
        return dt;
    }
    private void SaveAttendance(DataTable dt)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("sp_InsertStudentAttendance", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Attendance", dt);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        int status = Convert.ToInt32(dr["Status"]);
                        string message = dr["Message"].ToString();
                        string documentNo = dr["DocumentNo"].ToString();

                        if (message == "Attendance already posted.")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                            "alert",
                                            "alert('Attendance Already Post');",
                                            true);
                        }


                        if (status == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Attendance Post Successfully');",
                true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Attendance Not Post');",
                 true);
                        }
                    }
                }
            }
        }
    }
}