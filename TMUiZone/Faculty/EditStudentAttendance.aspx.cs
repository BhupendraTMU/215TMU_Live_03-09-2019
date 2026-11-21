using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Faculty_EditStudentAttendance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bindAcademicYear();
                BindCourse();
            }
            catch
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        try
        {
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
                chkboxPrinciple.Visible = true;
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }

    }

    public void BindCourse()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_GetCourseRoleWise_HOD_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@userid", Session["uid"].ToString());
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@AcadmicYear", ddlAcademicYear.SelectedValue);
            if (ddlTypeClass.SelectedValue == "1")
            {
                cmd.Parameters.Add("@Remedial", "1");
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpCourse.DataSource = dt;
            drpCourse.DataTextField = "Details";
            drpCourse.DataValueField = "No_";
            drpCourse.DataBind();
        }
        catch
        {
        }
    }
    public void BindSemester()
    {
        try
        {
            string FacultyCode = "";
            //if (chkboxPrinciple.Checked == false)
            //{
            FacultyCode = Session["uid"].ToString();
            //}
            SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", FacultyCode);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            if (ddlTypeClass.SelectedValue == "1")
            {
                cmd.Parameters.Add("@Remedial", "1");
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpSemester.DataSource = dt;
            drpSemester.DataTextField = "Details";
            drpSemester.DataValueField = "No_";
            drpSemester.DataBind();
        }
        catch
        {
        }
    }
    public void BindSection()
    {
        string FacultyCode = "";
        //if (chkboxPrinciple.Checked == false)
        //{
        FacultyCode = Session["uid"].ToString();
        //}
        if (Session["UserGroup"].ToString() == "FACULTY") { FacultyCode = Session["uid"].ToString(); }
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role_TimeTable", con);//proc_GetSectionFromCourseWiseFaculty_Role
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (Chkdetained.Checked == true)
            {
                drpSection.DataSource = ds.Tables[1];
                drpSection.DataTextField = "Details";
                drpSection.DataValueField = "No_";
                drpSection.DataBind();
            }
            else
            {
                drpSection.DataSource = ds.Tables[0];
                drpSection.DataTextField = "Details";
                drpSection.DataValueField = "No_";
                drpSection.DataBind();
            }
        }
        catch (Exception ex) { }
    }
    //-------------add by ashu--on 25-08-2016
    public void bindAcademicYear()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            ddlAcademicYear.DataSource = dt1;
            ddlAcademicYear.DataTextField = "Details";
            ddlAcademicYear.DataValueField = "No_";
            ddlAcademicYear.DataBind();
        }
        catch
        {
        }
    }
    public void bindGroupList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_GetGroup_Role_TimeTable", con);//sp_GetGroup_Role
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (Chkdetained.Checked == true)
            {
                ddlGroup.DataSource = ds.Tables[1];
                ddlGroup.DataTextField = "Details";
                ddlGroup.DataValueField = "No_";
                ddlGroup.DataBind();
            }
            else
            {
                ddlGroup.DataSource = ds.Tables[0];
                ddlGroup.DataTextField = "Details";
                ddlGroup.DataValueField = "No_";
                ddlGroup.DataBind();
            }
        }
        catch (Exception ex) { }
    }
    public void bindBatchList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetBatchFromCourseSemester_Role_TimeTable", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (Chkdetained.Checked == true)
            {
                ddlBatch.DataSource = ds.Tables[1];
                ddlBatch.DataTextField = "Details";
                ddlBatch.DataValueField = "No_";
                ddlBatch.DataBind();
            }
            else
            {
                ddlBatch.DataSource = ds.Tables[0];
                ddlBatch.DataTextField = "Details";
                ddlBatch.DataValueField = "No_";
                ddlBatch.DataBind();
            }
        }
        catch (Exception ex) { }
        //SqlCommand cmd = new SqlCommand("proc_GetBatchFromCourseSemester_Role", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        //cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        //cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        //cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        //DataTable dt = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //da.Fill(dt);
        //ddlBatch.DataSource = dt;
        //ddlBatch.DataTextField = "Details";
        //ddlBatch.DataValueField = "No_";
        //ddlBatch.DataBind();
    }
    //------------------------------------
    public void BindSubjectCode()
    {
        try
        {
            string FCode = "";
            //string FCode=Session["uid"].ToString();
            //if( chkboxPrinciple.Checked == true){FCode="";}
            if (Session["UserGroup"].ToString() == "FACULTY") { FCode = Session["uid"].ToString(); }
            else { FCode = Session["uid"].ToString(); }

            DataTable dt = new DataTable();
            //SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance", con); sandeep 22/12/2016
            // SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance_FacultyWise_Role", con);//ashu on /10/2017
            //  SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance_FacultyWise_Role_AcademicYear", con);
            SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance_FacultyWise_Role_AcademicYearNew", con); //Sanjay Jain on /08/2024
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            cmd.Parameters.Add("@Section", drpSection.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", FCode);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            drpSubject.DataSource = dt;
            drpSubject.DataTextField = "Details";
            drpSubject.DataValueField = "No_";
            drpSubject.DataBind();
        }
        catch
        { }
    }
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCourse();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSemester();
    }
    //protected void grdStudentAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdStudentAttendance.PageIndex = e.NewPageIndex;
    //    if (chkOpen.Checked == true)
    //    {
    //        bindGridOpen();
    //    }
    //    else
    //    {
    //        bindGrid();
    //    }
    //}

    public void bindGrid()
    {
        try
        {
            grdStudentAttendanceEdit.DataSource = null;
            grdStudentAttendanceEdit.DataBind();
            string DateFilter = "", Command = "";
            // DateTime DateFrom1, DateTo1;  // comment on 22-08-2015
            if (drpCourse.SelectedItem.Text == "-- Course --")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('please select course...');", true);
                return;
            }
            if (drpSemester.SelectedItem.Text == "-- Semester --")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('please select semester...');", true);
                return;
            }
            if (txtDateFrom.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('please select date...');", true);
                return;
            }
            con.Close();
            SqlCommand cmd = new SqlCommand("SP_Get_StudentAttendanceECA_Record", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.Add("@Course", drpCourse.SelectedValue);
            cmd.Parameters.Add("@Attendance_Date", txtDateFrom.Text);
            cmd.Parameters.Add("@EmpID", Session["uid"].ToString());
            cmd.Parameters.Add("@Subject", drpSubject.SelectedValue);
            cmd.Parameters.Add("@Type", "1");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 50000;
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            grdStudentAttendanceEdit.DataSource = dt;
            grdStudentAttendanceEdit.DataBind();
            if (dt.Rows.Count > 0)
            {
                btnExport.Visible = true;
                btn_EC.Visible = false;
            }
            else
            {
                btnExport.Visible = false;
                btn_EC.Visible = false;
            }

        }
        catch (Exception ex) { }
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSection();
        BindSubjectCode();
        bindGroupList();
        bindBatchList();
    }

    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubjectCode();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        bindGrid();

    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AttendanceView.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        string headerTable = @"
<table border='1' cellspacing='0' cellpadding='3'
style='border-collapse:collapse;width:100%;font-family:Arial;font-size:10px;'>

<tr>
    <td colspan='8' align='center'
        style='background-color:#FF8C00;color:white;font-size:15px;
        font-weight:bold;height:24px;padding:3px;'>
        ATTENDANCE REPORT : " + DateTime.Now.ToString("dd-MMM-yyyy") + @"
    </td>
</tr>

<tr style='background-color:#FFF3E0;color:#333;font-size:10px;'>
    <td><b>Session:</b> " + hfR_Session.Value + @"</td>
    <td><b>From Date:</b> " + hfR_FromDate.Value + @"</td>
    <td><b>To Date:</b> " + hfR_ToDate.Value + @"</td>
    <td><b>Course:</b> " + hfR_Course.Value + @"</td>
    <td colspan='3'><b>Subject:</b> " + hfR_Subject.Value + @"</td>
</tr>

<tr style='background-color:#F2F2F2;color:#333;font-size:10px;'>
    <td><b>Section:</b> " + hfR_Section.Value + @"</td>
    <td><b>Group:</b> " + hfR_Group.Value + @"</td>
    <td><b>Batch:</b> " + hfR_Batch.Value + @"</td>
    <td colspan='4'><b>Generated By:</b> " + Convert.ToString(Session["uname"]) + @"
    </td>
</tr>

</table><br/>";
        //string headerTable = @"<Table bgcolor=gray><tr><td colspan=7 align=center bgcolor=gold ><font size=16><h1>Attendance Report :  " + DateTime.Now.ToString("dd-MMM-yyyy") + "</h1></font> </td></tr><tr><td><b>Session: " + hfR_Session.Value + "<b></td> <td><b>From Date: " + hfR_FromDate.Value + "</b></td><td><b>To Date: " + hfR_ToDate.Value + "</b></td><td><b>Course: " + hfR_Course.Value + "</b></td><td><b>Subject: " + hfR_Subject.Value + "</td></b></tr><tr><td><b>Section: " + hfR_Section.Value + "</b></td><td><b>Group: " + hfR_Group.Value + "</b></td><td><b>Batch: " + hfR_Batch.Value + "</b></td><td>By:-" + Session["uname"].ToString() + "</td></tr></Table>";
        Response.Write(headerTable);
        StringWriter sw1 = new StringWriter();
        Response.Write(sw1.ToString());

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            grdStudentAttendanceEdit.AllowPaging = false;
            if (chkOpen.Checked == true)
            {
                bindGridOpen();
            }
            else
            {
                bindGrid();
            }
            grdStudentAttendanceEdit.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdStudentAttendanceEdit.HeaderRow.Cells)
            {
                cell.BackColor = grdStudentAttendanceEdit.HeaderStyle.BackColor;
            }
            if (chkOpen.Checked == true)
            {
                foreach (GridViewRow row in grdStudentAttendanceEdit.Rows)
                {
                    row.BackColor = Color.White;

                    foreach (TableCell cell in row.Cells)
                    {
                        cell.BackColor = row.RowIndex % 2 == 0
                            ? grdStudentAttendanceEdit.AlternatingRowStyle.BackColor
                            : grdStudentAttendanceEdit.RowStyle.BackColor;

                        cell.CssClass = "textmode";
                    }

                    row.Cells[row.Cells.Count - 1].Visible = false;
                    row.Cells[row.Cells.Count - 2].Visible = false;
                    row.Cells[row.Cells.Count - 3].Visible = false;
                }

                grdStudentAttendanceEdit.HeaderRow.Cells[grdStudentAttendanceEdit.HeaderRow.Cells.Count - 1].Visible = false;
                grdStudentAttendanceEdit.HeaderRow.Cells[grdStudentAttendanceEdit.HeaderRow.Cells.Count - 2].Visible = false;
                grdStudentAttendanceEdit.HeaderRow.Cells[grdStudentAttendanceEdit.HeaderRow.Cells.Count - 3].Visible = false;
            }
            else
            {
                foreach (GridViewRow row in grdStudentAttendanceEdit.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdStudentAttendanceEdit.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdStudentAttendanceEdit.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                    row.Cells[row.Cells.Count - 1].Visible = false;
                }
                // grdStudentAttendance.Columns[grdStudentAttendance.Columns.Count - 1].Visible = true;
                grdStudentAttendanceEdit.HeaderRow.Cells[grdStudentAttendanceEdit.HeaderRow.Cells.Count - 1].Visible = false;
            }
            grdStudentAttendanceEdit.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void chkboxPrinciple_CheckedChanged(object sender, EventArgs e)
    {
        BindCourse();
    }
    public void ReviewAttandanceCountDetail(string str, int i, string EventType)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[Sp_ReviewAttandanceCountDetailNew]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudentNo_", str);
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
            cmd.Parameters.Add("@SemYear", drpSemester.SelectedValue);
            cmd.Parameters.Add("@Fromdate", txtDateFrom.Text);
            // cmd.Parameters.Add("@Todate", txtDateTo.Text);
            cmd.Parameters.Add("@Todate", txtDateFrom.Text);
            cmd.Parameters.Add("@Type", ddlTypeClass.SelectedValue == "0,4" ? "0" : ddlTypeClass.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@AttendanceType", i);
            cmd.Parameters.Add("@Section", drpSection.SelectedValue);
            cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue);
            cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    if (EventType != "Others")
            //    {

            //        grdAttandanceDetails.DataSource = dt.Select("[Event Type]='" + EventType + "'").CopyToDataTable();

            //    }
            //    else
            //    {
            //        grdAttandanceDetails.DataSource = dt.Select("[Event Type]='Others'").CopyToDataTable();
            //    }


            //    grdAttandanceDetails.DataBind();
            //}
            //else
            //{
            grdAttandanceDetails.DataSource = dt;
            grdAttandanceDetails.DataBind();
            // }


        }
        catch (Exception ex) { }
    }
    protected void BtnPresent_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            int index = row.RowIndex;

            Label lblPresent = (Label)grdStudentAttendanceEdit.Rows[index].FindControl("lblStudent");

            Label lblStudentName = (Label)grdStudentAttendanceEdit.Rows[index].FindControl("lblStudentName");
            HiddenField HfEventType = (HiddenField)grdStudentAttendanceEdit.Rows[index].FindControl("HfEventType");
            lblStudent.Text = "Student Name:- " + lblStudentName.Text;
            lblCourse.Text = "Course:- " + drpCourse.SelectedValue;
            ReviewAttandanceCountDetail(lblPresent.Text, 0, HfEventType.Value);
            GridViewDetails.Show();
        }
        catch (Exception ex) { }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewDetails.Hide();
    }

    protected void BtnAbsent_Click(object sender, EventArgs e)
    {

        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;

        Label lblPresent = (Label)grdStudentAttendanceEdit.Rows[index].FindControl("lblStudent");


        Label lblStudentName = (Label)grdStudentAttendanceEdit.Rows[index].FindControl("lblStudentName");
        HiddenField HfEventType = (HiddenField)grdStudentAttendanceEdit.Rows[index].FindControl("HfEventType");
        lblStudent.Text = "Student Name:- " + lblStudentName.Text;
        lblCourse.Text = "Course:- " + drpCourse.SelectedValue;
        ReviewAttandanceCountDetail(lblPresent.Text, 1, HfEventType.Value);
        GridViewDetails.Show();
    }
    protected void ddlTypeClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCourse();
        if (ddlTypeClass.SelectedValue == "2")
            drpSubject.Enabled = false;
        else
            drpSubject.Enabled = true;
        BindSemester();

    }

    protected void Chkdetained_CheckedChanged(object sender, EventArgs e)
    {
        BindSection();
        BindSubjectCode();
        bindGroupList();
        bindBatchList();
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
        if (Chkdetained.Checked)
            chkOpen.Checked = false;
        // bindGrid();
    }
    public void bindGetSubjectList1()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromCourseSubjectLine2", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();

    }
    protected void chkOpen_CheckedChanged(object sender, EventArgs e)
    {
        if (chkOpen.Checked == true)
        {
            drpCourse.Enabled = false;
            drpSemester.Enabled = false;
            drpSection.Enabled = false;
            ddlGroup.Enabled = false;
            ddlBatch.Enabled = false;
            btnShow.Visible = false;
            btnShow1.Visible = true;
            bindGetSubjectList1();
        }
        else
        {

            drpCourse.Enabled = true;
            drpSemester.Enabled = true;
            drpSection.Enabled = true;
            ddlGroup.Enabled = true;
            ddlBatch.Enabled = true;
            btnShow.Visible = true;
            btnShow1.Visible = false;
            drpSubject.DataSource = "";
            drpSubject.DataBind();
        }
        if (chkOpen.Checked)
            Chkdetained.Checked = false;

    }
    protected void btnShow1_Click(object sender, EventArgs e)
    {

        bindGridOpen();

    }
    public void bindGridOpen()
    {
        try
        {
            string DateFilter = "", Command = "";

            if (drpSubject.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('please select Subject...');", true);
                return;
            }
            if (txtDateFrom.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('please select date...');", true);
                return;
            }
            con.Close();
            SqlCommand cmd = new SqlCommand("SP_Get_StudentAttendanceECA_Record", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.Add("@Course", drpCourse.SelectedValue);
            cmd.Parameters.Add("@Attendance_Date", txtDateFrom.Text);
            cmd.Parameters.Add("@EmpID", Session["uid"].ToString());
            cmd.Parameters.Add("@Subject", drpSubject.SelectedValue);
            cmd.Parameters.Add("@Type", "2");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 50000;
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            grdStudentAttendanceEdit.DataSource = dt;
            grdStudentAttendanceEdit.DataBind();
            if (dt.Rows.Count > 0)
            {
                btnExport.Visible = true;
                btn_EC.Visible = false;
            }
            else
            {
                btnExport.Visible = false;
                btn_EC.Visible = false;
            }
        }
        catch (Exception ex) { }
    }

    protected void btn_EC_Click1(object sender, EventArgs e)
    {
        System.Data.DataTable dt = new System.Data.DataTable();

        dt.Columns.Add("StudentNo");
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Absent");
        // dt.Columns.Add("Subject");
        foreach (GridViewRow row in grdStudentAttendanceEdit.Rows)
        {
            CheckBox chk = row.FindControl("chkEC_ATT") as CheckBox;

            if (chk != null && chk.Checked)
            {
                Label lblStudent = row.FindControl("lblStudent") as Label;
                Label lblStudentName = row.FindControl("lblStudentName") as Label;
                Label lblAbsent = row.FindControl("lblAbsent") as Label;
                HiddenField HfAbsent1 = row.FindControl("HfAbsent1") as HiddenField;
                if (lblStudent != null && lblStudentName != null)
                {
                    System.Data.DataRow dr = dt.NewRow();

                    dr["StudentNo"] = lblStudent.Text.Trim();
                    dr["StudentName"] = lblStudentName.Text.Trim();
                    dr["Absent"] = HfAbsent1.Value.Trim();
                    // dr["Subject"] = drpSubject.SelectedItem.Text;

                    dt.Rows.Add(dr);
                }
            }
        }

        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(
                this, this.GetType(), "Key",
                "alert('Please select at least one Student.');", true);
            return;
        }

        md_grd_CE.DataSource = dt;
        md_grd_CE.DataBind();

        md_Student_EC_Details.Show();
        up_StudentListEC.Update();

    }


    protected void btn_EC_Click(object sender, EventArgs e)
    {
        try
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("StudentNo");
            dt.Columns.Add("StudentName");
            dt.Columns.Add("Absent");

            foreach (GridViewRow row in grdStudentAttendanceEdit.Rows)
            {
                CheckBox chk = row.FindControl("chkEC_ATT") as CheckBox;
                Label lblStudent = row.FindControl("lblStudent") as Label;
                Label lblStudentName = row.FindControl("lblStudentName") as Label;
                Label lblECAtatus = row.FindControl("lblECA1") as Label;
                HiddenField HfAbsent1 = row.FindControl("HfAbsent1") as HiddenField;

                if (chk == null || !chk.Checked)
                    continue;

                if (lblStudent == null || lblStudentName == null || HfAbsent1 == null)
                    continue;

                string status = lblECAtatus == null ? "" : lblECAtatus.Text.Trim();
                string absent = HfAbsent1.Value.Trim();

                // Already applied students ko skip karein
                if ((absent == "A" && status == "Pending") || (absent == "P" && status == "Approved"))
                    continue;

                System.Data.DataRow dr = dt.NewRow();
                dr["StudentNo"] = lblStudent.Text.Trim();
                dr["StudentName"] = lblStudentName.Text.Trim();
                dr["Absent"] = absent;

                dt.Rows.Add(dr);
            }

            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(), "Key",
                    "alert('Please select at least one eligible Student.');", true);
                return;
            }

            md_grd_CE.DataSource = dt;
            md_grd_CE.DataBind();

            md_Student_EC_Details.Show();
            up_StudentListEC.Update();
        }
        catch { }
    }


    protected void grdStudentAttendanceEdit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // CheckBox chkEC_ATT = (CheckBox)e.Row.FindControl("chkEC_ATT");
            Label lblECA1 = (Label)e.Row.FindControl("lblECA1");
            // HiddenField HfAbsent1 = (HiddenField)e.Row.FindControl("HfAbsent1");

            // chkEC_ATT.Visible = true;

            //if (HfAbsent1.Value == "A" && lblECA1.Text == "")
            //{
            //    chkEC_ATT.Checked = false;
            //    chkEC_ATT.Enabled = true;
            //}
            if (lblECA1.Text == "A")
            {
                // chkEC_ATT.Checked = true;
                // chkEC_ATT.Enabled = false;
                lblECA1.Text = "Pending";
                lblECA1.CssClass = "ec-status ec-pending";
            }
            else if (lblECA1.Text == "P")
            {
                // chkEC_ATT.Checked = false;
                // chkEC_ATT.Visible = false;
                lblECA1.Text = "Approved";
                lblECA1.CssClass = "ec-status ec-approved";
            }
            //else if (HfAbsent1.Value == "P" && lblECA1.Text == "")
            //{
            //    chkEC_ATT.Visible = false;
            //    chkEC_ATT.Checked = false;
            //}
        }
    }

    protected void btn_Reassign_Click(object sender, EventArgs e)
    {
        bool isChecked = false;
        string filePath = "";

        if (txtRemarks.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "UploadAlert", "alert('Please enter remarks.');", true);
            md_Student_EC_Details.Show();
            return;
        }
        //if (!fuAttachment.HasFile)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Please upload document.');", true);
        //    md_Student_EC_Details.Show();
        //    return;
        //}

        //if (fuAttachment.HasFile)
        //{
        //    string fileName = System.IO.Path.GetFileName(fuAttachment.PostedFile.FileName);
        //    fuAttachment.SaveAs(Server.MapPath("~/Uploads/ReviewAttandance/" + fileName));
        //    filePath = "~/Uploads/ReviewAttandance/" + fileName;
        //}

        foreach (GridViewRow row in md_grd_CE.Rows)
        {

            isChecked = true;
            Label lblStudent = row.FindControl("lblStudent") as Label;
            Label lblStudentName = row.FindControl("lblStudentName") as Label;
            Label lblAbsent = row.FindControl("lblAbsent") as Label;
            HiddenField HfAbsent1 = row.FindControl("HfAbsent1") as HiddenField;

            using (SqlCommand cmd = new SqlCommand("SP_Insert_Tbl_StudentAttendanceLine_Record_Principal", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@SubjectNo", drpSubject.SelectedValue);
                cmd.Parameters.AddWithValue("@StudentCode", lblStudent.Text);
                // cmd.Parameters.AddWithValue("@StudentName", lblStudentName.Text);
                cmd.Parameters.AddWithValue("@Date", txtDateFrom.Text);
                // cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
                cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim());
                if (chkOpen.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@type", "2");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@type", "1");
                }
                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd.ExecuteNonQuery();
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Approved Successfully..');", true);
        if (chkOpen.Checked == true)
        {
            bindGridOpen();
        }
        else
        {
            bindGrid();
        }
        UpdatePanel1.Update();
        if (!isChecked)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select at least one Student.');", true);
            md_Student_EC_Details.Show();
            return;
        }
    }
    protected void btnClick_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        Label lblCourse = (Label)row.FindControl("lblCourse");
        Label lblCollege = (Label)row.FindControl("lblCollege");
        Label lblSubjectNo = (Label)row.FindControl("lblSubjectNo");
        Label lblAttendance_Date = (Label)row.FindControl("lblAttendance_Date");

        string Course = lblCourse != null ? lblCourse.Text.Trim() : "";
        string College = lblCollege != null ? lblCollege.Text.Trim() : "";
        string SubjectNo = lblSubjectNo != null ? lblSubjectNo.Text.Trim() : "";
        string Attendance_Date1 = lblAttendance_Date != null ? lblAttendance_Date.Text.Trim() : "";

        // Values check
        //   Response.Write(Course + " | " + College + " | " + Attendance_Date);


        SqlCommand cmd = new SqlCommand("SP_Get_StudentAttendanceECA_Record_Principal", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.Add("@Course", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Attendance_Date", Attendance_Date1);
        cmd.Parameters.Add("@EmpID", Session["uid"].ToString());
        cmd.Parameters.Add("@Subject", SubjectNo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.CommandTimeout = 50000;
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();

        Label lblECStatus = row.FindControl("lblECA1") as Label;
        string ecStatus = lblECStatus != null ? lblECStatus.Text.Trim() : "";

        DataView dv = dt.DefaultView;

        if (ecStatus == "Pending")
            dv.RowFilter = "Attendance1 = 'A'";
        else if (ecStatus == "Approved")
            dv.RowFilter = "Attendance1 = 'P'";


        md_grd_CE.DataSource = dv;
        md_grd_CE.DataBind();

        if (dt.Rows.Count > 0)
        {
            txtRemarks.Text = dt.Rows[0]["FinalRemarks"].ToString();
            lblFacultyName1.Text = dt.Rows[0]["FacultyName"].ToString();
            lblFacultyID1.Text = dt.Rows[0]["FacultyCode"].ToString();
        }
        bool isAbsent = false;

        foreach (GridViewRow row1 in md_grd_CE.Rows)
        {
            Label Attandance1 = row1.FindControl("lblAbsent") as Label;
            if (Attandance1 != null && Attandance1.Text.Trim() == "A")
            {
                isAbsent = true;
                break;
            }
        }

        btn_Reassign.Visible = isAbsent;
        if (isAbsent)
        {
            txtRemarks.Text = "";
        }

        md_Student_EC_Details.Show();
        up_StudentListEC.Update();
    }
}
