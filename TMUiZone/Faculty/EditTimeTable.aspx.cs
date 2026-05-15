using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;


public partial class Faculty_EditTimeTable : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindCourseCode1();
            bindFacultyCode();
        }
    }
    public void bindCourseCode1()
    {



        SqlCommand cmd = new SqlCommand("Sp_GetCourseRoleWise_HOD_Role_ForTimeTableDM", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@userid", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourseCode.DataSource = dt;
        drpCourseCode.DataTextField = "No_";
        drpCourseCode.DataBind();
    }
    protected void drpCourseCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindFacultyCode();
        bindFacultyCode1();
    }
    public void bindFacultyCode()
    {
        SqlCommand cmd = new SqlCommand("select distinct([Faculty Code]),[Faculty Name] from [TMU$Course Wise Faculty] where [Course Code]='" + drpCourseCode.SelectedItem.ToString() + "' and  [Portal ID]='1' order by [Faculty Name] asc", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpFaculty.DataSource = dt;
        drpFaculty.DataTextField = "Faculty Name";
        drpFaculty.DataValueField = "Faculty Code";
        drpFaculty.DataBind();
        drpFaculty.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));


    }
    public void bindFacultyCode1()
    {
        SqlCommand cmd = new SqlCommand("select distinct([Faculty Code]),[Faculty Name] from [TMU$Course Wise Faculty] where [Course Code]='" + drpCourseCode.SelectedItem.ToString() + "' and  [Portal ID]='1' order by [Faculty Name] asc", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpFaculty1.DataSource = dt;
        drpFaculty1.DataTextField = "Faculty Name";
        drpFaculty1.DataValueField = "Faculty Code";
        drpFaculty1.DataBind();
        drpFaculty1.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));


    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        BindTable(drpFaculty.SelectedValue);
        ModifyTR.Visible = false;
    }
    public void BindTable(string FacultyCode)
    {

        string qur = "";
        if (drpCourseCode.SelectedValue != "-- Select --")
            qur = "and [Course Code] like '" + drpCourseCode.SelectedValue + "%'";

        string Command = "";


        if (txtFromDate.Text == "")
        {
            txtFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        }
        if (txtToDate.Text == "")
        {
            txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        }
        if (drpCourseCode.SelectedValue == "-- Select --" || drpCourseCode.SelectedValue == "")
        {
            Command = "select * from [TMU$Time Table Generation - COL] where [Faculty Code]='" + FacultyCode + "'and [Course Code] like '%' and [Day No] between  1 and 6 and [Hour No] between 1 and 10 and    [Attendance Date] between convert(date, '" + txtFromDate.Text + "') and  convert(date, '" + txtToDate.Text + "')";
        }
        else
        {
            Command = "select * from [TMU$Time Table Generation - COL] where [Faculty Code]='" + FacultyCode + "'and [Course Code] ='" + drpCourseCode.SelectedValue + "' and [Day No] between  1 and 6 and [Hour No] between 1 and 10 and    [Attendance Date] between convert(date, '" + txtFromDate.Text + "') and  convert(date, '" + txtToDate.Text + "')";
        }

        SqlCommand cmd = new SqlCommand(Command, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdData.DataSource = dt;
        grdData.DataBind();

    }

    protected void lnkModify_Click(object sender, EventArgs e)
    {
        GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        Label lblID = (Label)clickedRow.FindControl("lblEntryNo");
        hdfEntryNo.Value = lblID.Text;
        Label lblSubject = (Label)clickedRow.FindControl("lblSubject");
        Label lblSem = (Label)clickedRow.FindControl("lblSemester");
        Label lblSec = (Label)clickedRow.FindControl("lblSection");
        Label lblCourse = (Label)clickedRow.FindControl("lblCourse");
        Label lblDay = (Label)clickedRow.FindControl("lblDay");
        hfSubject.Value = lblSubject.Text;
        hfSem.Value = lblSem.Text;
        hfSec.Value = lblSec.Text;
        hfCourse.Value = lblCourse.Text;
        hfDay.Value = lblDay.Text;
        HiddenField AcYear1 = (HiddenField)clickedRow.FindControl("hfYear");
        HiddenField FacultyCode1 = (HiddenField)clickedRow.FindControl("hfFaculty");
        HiddenField hfHour1 = (HiddenField)clickedRow.FindControl("hfHour");
        HiddenField hfRoom1 = (HiddenField)clickedRow.FindControl("hfRoom");
        AcYear.Value = AcYear1.Value;
        FacultyCode.Value = FacultyCode1.Value;
        hfRoom.Value = hfRoom1.Value;
        hfHour.Value = hfHour1.Value;







        DataTable dt = new DataTable();

        //--------------------------Bind Section-----------------------

        SqlCommand cmd1 = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role_TimeTable", con);//proc_GetSectionFromCourseWiseFaculty_Role
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@FacultyCode", FacultyCode.Value);
        cmd1.Parameters.Add("@CourseCode", lblCourse.Text);
        cmd1.Parameters.Add("@SemesterCode", lblSem.Text);
        cmd1.Parameters.Add("@AcademicYear", AcYear.Value);

        DataSet ds = new DataSet();
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.Fill(ds);

        drpSection.DataSource = ds.Tables[0];
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();
        //--------------------------Bind Subject-----------------------




        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromCourseSubjectLine", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", lblCourse.Text);
        cmd.Parameters.Add("@SemesterCode", lblSem.Text);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode.Value);
        cmd.Parameters.Add("@AcademicYear", AcYear.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();


        //-----------------------------Bind Room----------------------------------
        SqlCommand cmdRoom = new SqlCommand("proc_GetRoomAllocation", con);
        cmdRoom.CommandType = CommandType.StoredProcedure;
        cmdRoom.Parameters.Add("@CourseCode", lblCourse.Text);
        cmdRoom.Parameters.Add("@Semester", lblSem.Text);
        cmdRoom.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmdRoom.Parameters.Add("@AcademicYear", AcYear.Value);
        cmdRoom.Parameters.Add("@DayNo", lblDay.Text);
        cmdRoom.Parameters.Add("@HourNo", hfHour.Value);
        cmdRoom.Parameters.Add("@SubjectCode", lblSubject.Text);// added on 06-10-2016--qry raised by deependra rastogi
        cmdRoom.Parameters.Add("@Section", drpSection.SelectedValue);//added on 24-01-2017--qry raised by deependra rastogi

        SqlDataAdapter daRoom = new SqlDataAdapter(cmdRoom);
        DataTable dtRoom = new DataTable();
        daRoom.Fill(dtRoom);
        drpRoom.DataSource = dtRoom;
        drpRoom.DataTextField = "Details";
        drpRoom.DataValueField = "No_";
        drpRoom.DataBind();


        ModifyTR.Visible = true;
        drpHour.SelectedValue = hfHour.Value;
        drpRoom.SelectedValue = hfRoom.Value;
        drpFaculty1.SelectedValue = drpFaculty.SelectedValue;
        if (dt.Rows.Count > 1)
        {
            drpSubject.SelectedValue = lblSubject.Text;
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string s = hdfEntryNo.Value;
        con.Open();
        SqlCommand cmd = new SqlCommand("update [TMU$Time Table Generation - COL] set [Faculty Code]='" + drpFaculty1.SelectedValue + "',[Faculty Name]='" + drpFaculty1.SelectedItem.Text + "',[Section Code]='" + drpSection.SelectedValue + "',[Room Allocation]='" + drpRoom.SelectedValue + "',[Subject Code]='" + drpSubject.SelectedValue + "',[Subject Description]='" + drpSubject.SelectedItem.Text + "',[Hour No]='"+drpHour.SelectedValue+"' where [Entry No_] ='" + s + "'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        ModifyTR.Visible = false;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Date Sheet Update Successfully.');", true);
        BindTable(drpFaculty.SelectedValue);

    }

    protected void drpFaculty1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd1 = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role_TimeTable", con);//proc_GetSectionFromCourseWiseFaculty_Role
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@FacultyCode", drpFaculty1.SelectedValue);
        cmd1.Parameters.Add("@CourseCode", hfCourse.Value);
        cmd1.Parameters.Add("@SemesterCode", hfSem.Value);
        cmd1.Parameters.Add("@AcademicYear", AcYear.Value);

        DataSet ds = new DataSet();
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.Fill(ds);

        drpSection.DataSource = ds.Tables[0];
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();


        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromCourseSubjectLine", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", hfCourse.Value);
        cmd.Parameters.Add("@SemesterCode", hfSem.Value);
        cmd.Parameters.Add("@Section", hfSec.Value);
        cmd.Parameters.Add("@FacultyCode", drpFaculty1.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", AcYear.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();


        //-----------------------------Bind Room----------------------------------
        SqlCommand cmdRoom = new SqlCommand("proc_GetRoomAllocation", con);
        cmdRoom.CommandType = CommandType.StoredProcedure;
        cmdRoom.Parameters.Add("@CourseCode", hfCourse.Value);
        cmdRoom.Parameters.Add("@Semester", hfSem.Value);
        cmdRoom.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmdRoom.Parameters.Add("@AcademicYear", AcYear.Value);
        cmdRoom.Parameters.Add("@DayNo", hfDay.Value);
        cmdRoom.Parameters.Add("@HourNo", hfHour.Value);
        cmdRoom.Parameters.Add("@SubjectCode", hfSubject.Value);// added on 06-10-2016--qry raised by deependra rastogi
        cmdRoom.Parameters.Add("@Section", drpSection.SelectedValue);//added on 24-01-2017--qry raised by deependra rastogi

        SqlDataAdapter daRoom = new SqlDataAdapter(cmdRoom);
        DataTable dtRoom = new DataTable();
        daRoom.Fill(dtRoom);
        drpRoom.DataSource = dtRoom;
        drpRoom.DataTextField = "Details";
        drpRoom.DataValueField = "No_";
        drpRoom.DataBind();





    }
}