using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Faculty_StudentAttendanceMarkEdit : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblFacultyCode.Text = Session["uid"].ToString();
                bindDrpCourseList();
                lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();               
                txtDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                bindAcademicYear();
            }
        }
        catch(Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    
    public void bindGrid()
    {
        DateTime dt11 = new DateTime();
        dt11 = Convert.ToDateTime(txtDate.Text);
        string date=dt11.ToString("MM-dd-yyyy");

        SqlCommand cmd1 = new SqlCommand("select * from [TMU$Student Attendance Line - COL] where [Staff Code]='" + lblFacultyCode.Text + "' and [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedItem.ToString() + "' and Section like'" + drpSection.SelectedValue + "%' and Hour='"+drpLecture.SelectedItem.ToString()+"' and Date='"+ date +"' and [Subject Code]='"+drpSubject.SelectedValue+"' and [Academic Year]='"+drpAcademicYear.SelectedValue+"'", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            lblMessage.Visible = true;
            pnlGrid.Visible = false;
            btnSubmit.Visible = false;
            pnlCheckBox.Visible = false;
        }
        else
        {
            pnlGrid.Visible = true;
            pnlCheckBox.Visible = true;
            btnSubmit.Visible = true;
            lblMessage.Visible = false;
            SqlCommand cmd = new SqlCommand("select [Enrollment No_],[Course Code],[Student Name],[Semester],[Section],[Academic Year],No_ " + @"
        from [TMU$Student - COLLEGE] where [Course Code]='" + drpCourse.SelectedValue + "' and Semester='" + drpSemester.SelectedItem.ToString() + "' and" + @"
        Section like'" + drpSection.SelectedValue + "%' and [Academic Year]='" + drpAcademicYear.SelectedValue + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grdAttendanceDetails.DataSource = dt;
            grdAttendanceDetails.DataBind();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    CheckBox rb = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkboxAttendance");
            //    if (dt.Rows[i]["Attendance Type"].ToString() == "0")
            //    {
            //        rb.Checked = true;
            //    }
            //}   
        }
    }
    
    public void bindDrpCourseList()
    {
        DataTable dt = new DataTable();
        dt = FDL.GetCourseList(lblFacultyCode.Text);
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    public void bindDrpSemesterList()
    {
        DataTable dt = new DataTable();
        dt = FDL.GetSemesterList(drpCourse.SelectedValue, lblFacultyCode.Text);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "Details";
        drpSemester.DataBind();
    }
    public void bindSectionList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();

    }
    public void bindGetSubjectList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTable", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)//ashu
    {
        bindGetSubjectList();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSectionList();
        bindGetSubjectList();
    }
    protected void grdAttendanceDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAttendanceDetails.PageIndex = e.NewPageIndex;
        bindGrid();
    }
    protected void chkPresentAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPresentAll.Checked == true)
        {
            for (int i = 0; i < grdAttendanceDetails.Rows.Count; i++)
            {
                CheckBox rb = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkboxAttendance");       
                rb.Checked = true;
            }
        }
    }
    protected void chkAbsentAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAbsentAll.Checked == true)
        {
            for (int i = 0; i < grdAttendanceDetails.Rows.Count; i++)
            {
                CheckBox rb = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkboxAttendance");
                rb.Checked = false;
            }
        }
    }
    DataTable dt = new DataTable();
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        btnSubmit.Enabled = false;
        BindTable();
        int AttendanceType = 0;
        if (chkBoxExtraClass.Checked == true)
            AttendanceType = 1;
        FDL.InsertStudentAttendanceLine(dt,drpCourse.SelectedValue, lblNo.Text,lblSubjectType.Text,drpSemester.SelectedItem.ToString(),
            drpSection.SelectedValue, txtDate.Text, drpAcademicYear.SelectedValue, drpSubject.SelectedValue, Session["uid"].ToString(), Convert.ToInt16(drpLecture.SelectedItem.ToString()), Session["GlobalDimension1Code"].ToString(),ddlGroup.SelectedValue,ddlBatch.SelectedValue);
        FDL.InsertStudentAttendanceHeader(lblNo.Text, lblSubjectType.Text, AttendanceType, lblFacultyCode.Text, drpCourse.SelectedValue,
            drpSemester.SelectedItem.ToString(), drpSection.SelectedValue, txtDate.Text, drpAcademicYear.SelectedValue, drpSubject.SelectedValue, Convert.ToInt16(drpLecture.Text), drpUnit.SelectedValue, txtTopic.Text, drpSubject.SelectedItem.ToString(), Session["GlobalDimension1Code"].ToString(),ddlGroup.SelectedValue,ddlBatch.SelectedValue);
        FDL.updateSeriesLineLastNoUsed(lblNo.Text);
        lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('Saved Successfully')", true);
        bindGrid();
        Blank();
        lblMessage.Visible = false;
        btnSubmit.Enabled = true;
    }
    public void BindTable()
    {        
        for (int i = 0; i < grdAttendanceDetails.Columns.Count; i++)
        {
            dt.Columns.Add(grdAttendanceDetails.HeaderRow.Cells[i].Text);
        }
        foreach (GridViewRow row in grdAttendanceDetails.Rows)
        {
            DataRow dr = dt.NewRow();
            for (int j = 0; j < grdAttendanceDetails.Columns.Count; j++)
            {
                CheckBox rb = (CheckBox)row.FindControl("chkboxAttendance");
                if (j == 3)
                {
                    if (rb.Checked==true)
                        row.Cells[j].Text = "0";
                    else
                        row.Cells[j].Text = "1";
                }
                dr[grdAttendanceDetails.HeaderRow.Cells[j].Text] = row.Cells[j].Text;
            }
            dt.Rows.Add(dr);
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        bindGrid();
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSubjectType.Text = FDL.GetSubjectTypebySemester(drpCourse.SelectedValue, drpSemester.SelectedItem.ToString(),  
            drpSubject.SelectedItem.ToString());
        bindLecture();
        bindUnit();
    }

    public void Blank()
    {
        drpCourse.SelectedIndex = 0;
        lblSubjectType.Text = "";
        drpSubject.DataSource = "";
        drpSubject.DataBind();       
        drpSemester.DataSource = "";
        drpSemester.DataBind();
        drpSection.DataSource = "";
        drpSection.DataBind();
        drpLecture.DataSource = "";
        drpLecture.DataBind();
        drpUnit.DataSource = "";
        drpUnit.DataBind();        
        pnlGrid.Visible = false;
        pnlCheckBox.Visible = false;
        btnSubmit.Visible = false;
        chkPresentAll.Checked = true;
        chkAbsentAll.Checked = false;
        txtTopic.Text = "";
    }
    public void bindLecture()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetLectureFromTimeTable", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpLecture.DataSource = dt;
        drpLecture.DataTextField = "Details";
        drpLecture.DataValueField = "No_";
        drpLecture.DataBind();
    }
    public void bindUnit()
    {
        DataTable dt = new DataTable();
        dt = FDL.GetUnitForMarkAttendance(drpCourse.SelectedValue,drpSubject.SelectedValue, Session["AcademicYear"].ToString());
        drpUnit.DataSource = dt;
        drpUnit.DataTextField = "Details";
        drpUnit.DataValueField = "No_";
        drpUnit.DataBind();
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        bindLecture();
        bindGetSubjectList();
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
}