using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using WebReference;
//using Microsoft.Reporting.WebForms;
public partial class StudentDetaineeReport : System.Web.UI.Page
{
    TMUConnection con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "FACULTY")
            {
                chkPrincipal.Visible = false;

                if (Session["UserGroup"].ToString() == "FACULTY")
                {
                    chkPrincipal.Checked = false;
                    chkPrincipal.Visible = false;
                }
                if (Session["GlobalDimension1Code"].ToString() != "")
                {
                    if (!IsPostBack)
                    {
                        bindAcademicYear();
                        BindCourse();
                        //  GetSubjectList();                       
                    }
                    // GetStudentList();
                }
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    public void CheckDetainee()
    {
        try
        {
            SqlCommand cmd1 = new SqlCommand("sp_checkDetanieeListBlank", con1);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@CourseCode", DrpCourse.SelectedValue);
            cmd1.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
            cmd1.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd1.Parameters.AddWithValue("@SemYear", drpSemester.SelectedValue);
            cmd1.Parameters.AddWithValue("@SubjectCode", ddlSubject.SelectedValue);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            con1.Open();
            da1.Fill(dt1);
            con1.Close();
            if (dt1.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Detainee Report Already  Submitted')", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GetFromToDate()
    {
        SqlCommand cmd1 = new SqlCommand("sp_GetFromdateToDateforDetainee", con1);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@CourseCode", DrpCourse.SelectedValue);
        cmd1.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
        cmd1.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd1.Parameters.AddWithValue("@SemYear", drpSemester.SelectedValue);
        cmd1.Parameters.AddWithValue("@SubjectCode", ddlSubject.SelectedValue);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        con1.Open();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            txtFromtDate.Text = dt1.Rows[0]["from"].ToString();
            txtToDate.Text = dt1.Rows[0]["To"].ToString();
        }
    }
    public void GetStudentList()
    {

        if (txtFromtDate.Text == "" || txtToDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Attendance not Marked')", true);
            return ;
        }


        SqlCommand cmd1 = new SqlCommand("sp_checkDetanieeListBlank", con1);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@CourseCode", DrpCourse.SelectedValue);
        cmd1.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
        cmd1.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd1.Parameters.AddWithValue("@SemYear", drpSemester.SelectedValue);
        cmd1.Parameters.AddWithValue("@SubjectCode", ddlSubject.SelectedValue);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        con1.Open();
        da1.Fill(dt1);
        con1.Close();
        if (dt1.Rows.Count > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Detainee Report Already  Submitted')", true);
        }
        else
        {


            if (chkOpen.Checked == true)
            {
                string FacultyCode = "";
                if (Session["UserGroup"].ToString() == "FACULTY") { FacultyCode = Session["uid"].ToString(); }
                con = new TMUConnection();
                if (txtFromtDate.Text != "" && txtToDate.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Date Could Not be Blank');", true);
                    return;
                }
                if (txtFromtDate.Text == "" && txtToDate.Text != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Date Could Not be Blank');", true);
                    return;
                }
                if (txtFromtDate.Text != "" && txtToDate.Text != "" && Convert.ToDateTime(txtFromtDate.Text) > Convert.ToDateTime(txtToDate.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'From Date Could Not be greater than To Date');", true);
                    return;
                }



                SqlCommand cmd = new SqlCommand("[Proc_GetStudAttendanceDetaineeListDELOpen]", con1); // veerendra 14-01-2019
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.Add("@FromDate", txtFromtDate.Text);
                cmd.Parameters.Add("@ToDate", txtToDate.Text);
                cmd.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
                cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());

                cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
                cmd.Parameters.Add("@Detainee", Checkdetainee.Checked);

                int PercentageFrom = 0, PercentageTo = 100;
                if (txtPercentFrom.Text != "")
                { PercentageFrom = Convert.ToInt16(txtPercentFrom.Text); }
                if (txtPercentTo.Text != "")
                { PercentageTo = Convert.ToInt16(txtPercentTo.Text); }

                cmd.Parameters.Add("@PercentageFrom", PercentageFrom);
                cmd.Parameters.Add("@PercentageTo", PercentageTo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 50000;
                DataTable dt = new DataTable();
                //dt.Clear();
                con1.Open();
                da.Fill(dt);
                con1.Close();
                //con.DisConnect();
                GrdDetenee.DataSource = dt;
                GrdDetenee.DataBind();
                if (dt.Rows.Count > 0)
                {
                    confirmModalB.Visible = false;
                    btnDetanie.Visible = true;
                }
                else
                {
                    btnDetanie.Visible = false;
                    confirmModalB.Visible = true;

                }
            }
            else
            {


                string FacultyCode = "";
                if (Session["UserGroup"].ToString() == "FACULTY") { FacultyCode = Session["uid"].ToString(); }
                con = new TMUConnection();
                if (txtFromtDate.Text != "" && txtToDate.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Date Could Not be Blank');", true);
                    return;
                }
                if (txtFromtDate.Text == "" && txtToDate.Text != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Date Could Not be Blank');", true);
                    return;
                }
                if (txtFromtDate.Text != "" && txtToDate.Text != "" && Convert.ToDateTime(txtFromtDate.Text) > Convert.ToDateTime(txtToDate.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'From Date Could Not be greater than To Date');", true);
                    return;
                }



                SqlCommand cmd = new SqlCommand("[Proc_GetStudAttendanceDetaineeListDEL]", con1); // veerendra 14-01-2019
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.Add("@FromDate", txtFromtDate.Text);
                cmd.Parameters.Add("@ToDate", txtToDate.Text);
                cmd.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
                cmd.Parameters.Add("@FacultyCode", FacultyCode);
                cmd.Parameters.Add("@coursecode1", DrpCourse.SelectedValue);
                cmd.Parameters.Add("@semyear", drpSemester.SelectedValue);
                cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
                cmd.Parameters.Add("@Detainee", Checkdetainee.Checked);

                int PercentageFrom = 0, PercentageTo = 100;
                if (txtPercentFrom.Text != "")
                { PercentageFrom = Convert.ToInt16(txtPercentFrom.Text); }
                if (txtPercentTo.Text != "")
                { PercentageTo = Convert.ToInt16(txtPercentTo.Text); }

                cmd.Parameters.Add("@PercentageFrom", PercentageFrom);
                cmd.Parameters.Add("@PercentageTo", PercentageTo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 50000;
                DataTable dt = new DataTable();
                //dt.Clear();
                con1.Open();
                da.Fill(dt);
                con1.Close();
                //con.DisConnect();
                GrdDetenee.DataSource = dt;
                GrdDetenee.DataBind();
                if (dt.Rows.Count > 0)
                {
                    confirmModalB.Visible = false;
                    btnDetanie.Visible = true;
                }
                else
                {
                    btnDetanie.Visible = false;
                    confirmModalB.Visible = true;

                }
            }
        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        GetStudentList();
    }
    public void GetSubjectList()
    {
        string FacultyCode = "";
        if (Session["UserGroup"].ToString() == "FACULTY") { FacultyCode = Session["uid"].ToString(); }
        con = new TMUConnection();

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromStudenttagging", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@academicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CourseCode", DrpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlSubject.DataSource = dt;
        ddlSubject.DataTextField = "Details";
        ddlSubject.DataValueField = "No_";
        ddlSubject.DataBind();
    }
    public void GetSubjectListOpen()
    {
        string FacultyCode = "";
        if (Session["UserGroup"].ToString() == "FACULTY") { FacultyCode = Session["uid"].ToString(); }
        con = new TMUConnection();

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance_FacultyWise_RoleOpen", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlSubject.DataSource = dt;
        ddlSubject.DataTextField = "Details";
        ddlSubject.DataValueField = "No_";
        ddlSubject.DataBind();
    }
    public void BindCourse()
    {
        SqlCommand cmd = new SqlCommand("Sp_GetCourseRoleWise_HOD_Role", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@userid", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        DrpCourse.DataSource = dt;
        DrpCourse.DataTextField = "Details";
        DrpCourse.DataValueField = "No_";
        DrpCourse.DataBind();
    }

    protected void chkPrincipal_CheckedChanged(object sender, EventArgs e)
    {
        GetSubjectList();

    }





    protected void DrpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
        GetSubjectList();
    }


    public void bindDrpSemesterList()
    {

        string FacultyCode = "";
        if (Session["UserGroup"].ToString() == "FACULTY")
        {
            FacultyCode = Session["uid"].ToString();
        }
        SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", DrpCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();

    }

    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        ddlAcademicYear.DataSource = dt1;
        ddlAcademicYear.DataTextField = "Details";
        ddlAcademicYear.DataValueField = "No_";
        ddlAcademicYear.DataBind();
    }

    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubjectList();
    }





    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        GetStudentList();
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)GrdDetenee.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow Row in GrdDetenee.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkStudent");
            if (checkBoxheader.Checked == true)
            {
                checkRows.Checked = true;

            }
            else
            {
                checkRows.Checked = false;
            }

        }

    }
    protected void chkStudent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)GrdDetenee.HeaderRow.FindControl("chkAll");
        foreach (GridViewRow Row in GrdDetenee.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkStudent");
            if (checkRows.Checked == false)
            {
                checkBoxheader.Checked = false;

            }
            else
            {
                // checkBoxheader.Checked = true;
            }

        }
    }
    protected void btnDetanie_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow row in GrdDetenee.Rows)
        {
            HiddenField StudenNo = (HiddenField)row.FindControl("SNumber");
            Label StudentName = (Label)row.FindControl("lblStudentName");
            Label EnrollmentNo = (Label)row.FindControl("lblEnrollment");
            Label Sem = (Label)row.FindControl("lblSemester");
            Label SubjectCode = (Label)row.FindControl("lblSubject");
            Label Course = (Label)row.FindControl("lblCourse");
            HiddenField hdnfrom = (HiddenField)row.FindControl("hdnperfrom");
            HiddenField hdnTo = (HiddenField)row.FindControl("hdnperTo");
            Label PerObtain = (Label)row.FindControl("lblObtainPer");
            CheckBox check = (CheckBox)row.FindControl("chkStudent");
            if (check.Checked == true)
            {
                SqlCommand cmd1 = new SqlCommand("sp_checkDetanieeList", con1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@SNo", StudenNo.Value);
                cmd1.Parameters.AddWithValue("@Semester", Sem.Text);
                cmd1.Parameters.AddWithValue("@SubjectCode", SubjectCode.Text);
                cmd1.Parameters.AddWithValue("@CourseCode", Course.Text);
                cmd1.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                con1.Open();
                da1.Fill(dt1);
                con1.Close();
                if (dt1.Rows.Count > 0)
                {

                }
                else
                {

                    SqlCommand cmd = new SqlCommand("sp_InsertDetanieeList", con1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SNo", StudenNo.Value);
                    cmd.Parameters.AddWithValue("@StudentName", StudentName.Text);
                    cmd.Parameters.AddWithValue("@EnrollmentNo", EnrollmentNo.Text);
                    cmd.Parameters.AddWithValue("@Semester", Sem.Text);
                    cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode.Text);
                    cmd.Parameters.AddWithValue("@CourseCode", Course.Text);
                    cmd.Parameters.AddWithValue("@PerFrom", Convert.ToInt32(hdnfrom.Value));
                    cmd.Parameters.AddWithValue("@PerTo", Convert.ToInt32(hdnTo.Value));
                    cmd.Parameters.AddWithValue("@PerObtain", Convert.ToInt32(PerObtain.Text));
                    cmd.Parameters.AddWithValue("@FromDate", txtFromtDate.Text);
                    cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                    cmd.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());

                    con1.Open();
                    cmd.ExecuteNonQuery();
                    con1.Close();

                }
            }
        }
        Response.Redirect("StudentDetaineeReport.aspx");
    }

    protected void Btnblank_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd1 = new SqlCommand("sp_checkDetanieeListBlank", con1);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@CourseCode", DrpCourse.SelectedValue);
            cmd1.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
            cmd1.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd1.Parameters.AddWithValue("@SemYear", drpSemester.SelectedValue);
            cmd1.Parameters.AddWithValue("@SubjectCode", ddlSubject.SelectedValue);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            con1.Open();
            da1.Fill(dt1);
            con1.Close();
            if (dt1.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Detainee Report Already  Submitted')", true);
            }
            else
            {

                SqlCommand cmd = new SqlCommand("sp_InsertDetanieeListblank", con1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CourseCode", DrpCourse.SelectedValue);
                cmd.Parameters.AddWithValue("@AcadmicYear", ddlAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@SemYear", drpSemester.SelectedValue);
                cmd.Parameters.AddWithValue("@SubjectCode", ddlSubject.SelectedValue);
                cmd.Parameters.AddWithValue("@FromDate", txtFromtDate.Text);
                cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                cmd.Parameters.AddWithValue("@FPer", txtPercentFrom.Text);
                cmd.Parameters.AddWithValue("@ToPer", txtPercentTo.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtremarsk.Text);
                cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());
                con1.Open();
                cmd.ExecuteNonQuery();
                con1.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Detainee Report Submitted')", true);


                txtFromtDate.Text = "";
                txtToDate.Text = "";
                txtPercentFrom.Text = "";
                txtPercentTo.Text = "";
                BindCourse();
                bindDrpSemesterList();
                GetSubjectList();
                txtremarsk.Text = "";
                confirmModalB.Visible = false;
            }
        }
        catch
        {
        }
    }
    protected void chkOpen_CheckedChanged(object sender, EventArgs e)
    {
        if (chkOpen.Checked == true)
        {
            DrpCourse.Enabled = false;
            drpSemester.Enabled = false;
            GetSubjectListOpen();
            BtnShow.Visible = false;
            Button1.Visible = true;
        }
        else
        {
            BtnShow.Visible = true;
            Button1.Visible = false;
            DrpCourse.Enabled = true;
            drpSemester.Enabled = true;
            ddlSubject.DataSource = "";
            ddlSubject.DataBind();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetStudentList();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFromToDate();








    }
}