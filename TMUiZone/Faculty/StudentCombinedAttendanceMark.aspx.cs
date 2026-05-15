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
using AjaxControlToolkit;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
public partial class Faculty_StudentCombinedAttendanceMark : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    DataTable dt2 = new DataTable(); DataTable dt12 = new DataTable(); DataTable dtStudent = new DataTable();
    DataTable dtSorting = new DataTable();
    static int cnt = 0; static String No_ = ""; static bool Save = false;
    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                Response.Redirect("Error.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                String sDate = DateTime.Now.ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
                int day = datevalue.Day;
                int mn = datevalue.Month;
                int yy = datevalue.Year;
                CalendarExtender1.StartDate = new DateTime(yy, mn, day-1);
                //CalendarExtender2.EndDate = new DateTime(yy, 8, 1);
              
                lblFacultyCode.Value = Session["uid"].ToString();
                bindAcademicYear();
                lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
                txtDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                bindLecture();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        bindLecture();

    }
    public void bindLecture()
    {
        if (txtDate.Text != "")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("[proc_GetCombinedLectureFromTimeTable]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Date", txtDate.Text);
            cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            ddlLecture.DataSource = dt;
            ddlLecture.DataTextField = "Details";
            ddlLecture.DataValueField = "No_";
            ddlLecture.DataBind();
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        bindUnit();
        bindStudentGrid();
        Save = true;
    }





    public DataTable bindStudentGrid()
    {

        DateTime dtDate = new DateTime();
        dtDate = Convert.ToDateTime(txtDate.Text);
        string date = dtDate.ToString("MM-dd-yyyy");
        // SqlCommand cmdStudent = new SqlCommand("SP_GetCombinedStudentAsPerTimetable", con);  //comment on 30-march 2017 by ashu
        SqlCommand cmdStudent = new SqlCommand("[SP_GetCombinedStudentAsPerTimetable_medical_test]", con);  //New Procedure created on 30-march 2017 by ashu
        //SP_GetCombinedStudentAsPerTimetable_medical
        cmdStudent.CommandType = CommandType.StoredProcedure;
        cmdStudent.Parameters.Add("@FacultyCode", lblFacultyCode.Value);
        cmdStudent.Parameters.Add("@Date", txtDate.Text);//date
        cmdStudent.Parameters.Add("@LectureNo", ddlLecture.SelectedItem.ToString());
        SqlDataAdapter daStudent = new SqlDataAdapter(cmdStudent);
        //DataTable dtStudent = new DataTable();

        daStudent.Fill(dtStudent);

        DataSet ds = new DataSet();
        daStudent.Fill(ds);
        if (Chkdetained.Checked == true)
        {
            grdStudentAttendance.DataSource = ds.Tables[1];

            grdStudentAttendance.DataBind();


        }
        else
        {
            grdStudentAttendance.DataSource = ds.Tables[0];
            grdStudentAttendance.DataBind();
        }

        if (dtStudent.Rows.Count > 0)
        {

            btnSubmit.Visible = true;
            pnlCheckBox.Visible = true;
        }

        if (grdStudentAttendance.Rows.Count > 0)
        {
            EnableDisable(false);
        }
        else
        { EnableDisable(true); }
        return dtStudent;
    }





    public void bindUnit()
    {
        DateTime dtDate = new DateTime();
        dtDate = Convert.ToDateTime(txtDate.Text);
        string date = dtDate.ToString("MM-dd-yyyy");
        SqlCommand cmdCL = new SqlCommand("[proc_GetCombinedSubjectClassification_test]", con);//proc_GetCombinedSubjectClassification
        cmdCL.CommandType = CommandType.StoredProcedure;
        cmdCL.Parameters.Add("@FacultyCode", lblFacultyCode.Value);
        cmdCL.Parameters.Add("@Date", txtDate.Text);//date
        cmdCL.Parameters.Add("@LectureNo", ddlLecture.SelectedItem.Text.ToString());
        if (Chkdetained.Checked == true)
        {
            cmdCL.Parameters.AddWithValue("@Detained", 1);
        }
        else
        {
            cmdCL.Parameters.AddWithValue("@Detained", 0);
        }
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmdCL);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        int Theory_count = 0;
        Theory_count = Convert.ToInt16(cmdCL.ExecuteScalar().ToString());
        con.Close();
        //-----------14-12-2016--Grou,Batch---by ashu--ENd

        DataTable dtUnit = new DataTable();
        SqlCommand cmdUnit = new SqlCommand("proc_GetUnitForCombinedMarkAttendance_test", con); //[proc_GetUnitForCombinedMarkAttendance]
        cmdUnit.CommandType = CommandType.StoredProcedure;
        cmdUnit.Parameters.Add("@FacultyCode", lblFacultyCode.Value);
        cmdUnit.Parameters.Add("@Date", date);
        cmdUnit.Parameters.Add("@LectureNo", ddlLecture.SelectedItem.Text.ToString());
        cmdUnit.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedItem.Text.ToString());
        if (Chkdetained.Checked == true)
        {
            cmdUnit.Parameters.AddWithValue("@Detained",1);
        }
        else
        {
            cmdUnit.Parameters.AddWithValue("@Detained", 0);
        }
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daUnit = new SqlDataAdapter(cmdUnit);
        daUnit.Fill(dtUnit);
        //dt = FDL.GetUnitForMarkAttendance(drpCourse.SelectedValue, drpSubject.SelectedValue, drpAcademicYear.SelectedValue);
        ddlUnit.DataSource = dtUnit;
        ddlUnit.DataTextField = "Details";
        ddlUnit.DataValueField = "No_";
        ddlUnit.DataBind();

        if (Theory_count == 0 && dtUnit.Rows.Count == 1)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("No_", typeof(string));
            dt1.Columns.Add("Details", typeof(string));
            dt1.Rows.Add("No Unit", "");
            ddlUnit.DataSource = dt1;
            ddlUnit.DataTextField = "Details";
            ddlUnit.DataValueField = "No_";
            ddlUnit.DataBind();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // int a = dtStudent.Rows.Count;  
        string Result = "";
        BindTable();
        if (ddlUnit.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('Please Select Unit !');", true);
            return;
        }

        else
        {
            if (Save == true)
            {
                Result = InsertStudentAttendanceHeaderAndLine("", ddlAcademicYear.SelectedValue, Convert.ToInt16(ddlLecture.SelectedValue), txtDate.Text, lblFacultyCode.Value, 0, ddlUnit.SelectedValue, txtTopic.Text, Session["GlobalDimension1Code"].ToString(), dt12);  //ASHU---25-08-2016  
                if (Result != "Error")
                {
                    lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
                    //bindGrid();
                    Blank();
                    lblMessage.Visible = false;
                    btnSubmit.Enabled = true;
                    btnSave.Enabled = true;
                    Save = false;
                    System.Threading.Thread.Sleep(2000);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Something went wrong !');", true);
                }
            }
            else
            {

            }
        }
    }
    public void BindTable()
    {

        for (int i = 0; i < grdStudentAttendance.Columns.Count; i++)
        {
            // dt12.Columns.Add(grdStudentAttendance.HeaderRow.Cells[i].Text);            
            dt12.Columns.Add(grdStudentAttendance.Columns[i].HeaderText);
        }

        dt12.Columns[11].ColumnName = "SubjectType";
        dt12.Columns[12].ColumnName = "StudentNo";
        int r = 0;
        foreach (GridViewRow row in grdStudentAttendance.Rows)
        {
            DataRow dr = dt12.NewRow();
            for (int j = 0; j < grdStudentAttendance.Columns.Count; j++)
            {
                CheckBox rb = (CheckBox)row.FindControl("chkboxAttendance");
                if (j == 10) //if (j == 9)  //due to srno
                {
                    if (rb.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                if (j == 11 || j == 12)
                {

                }
                else
                {
                    dr[grdStudentAttendance.Columns[j].HeaderText] = row.Cells[j].Text;
                }
            }
            HiddenField hfSubjectType = (HiddenField)row.FindControl("hfSubjectType");
            HiddenField hfStudentNo = (HiddenField)row.FindControl("hfStudentNo");
            dt12.Rows.Add(dr);
            dt12.Rows[r]["SubjectType"] = hfSubjectType.Value;
            dt12.Rows[r]["StudentNo"] = hfStudentNo.Value;
            r = r + 1;
        }


    }
    public string InsertStudentAttendanceHeaderAndLine(string DocumentNo, string AcademicYear, int Hour, string Date, string FacultyCode, int AttendanceType, string Unit, string Topic, string CollegeCode, DataTable dt)
    {
        string Result = "";
        if (con.State == ConnectionState.Closed)
            con.Open();
        try
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if(dt.Rows[i]["Group"]=="&nbsp;")
                //{
                //    dt.Rows[i]["Group"] = "";
                //}
                //if (dt.Rows[i]["Batch"] == "&nbsp;")
                //{
                //    dt.Rows[i]["Batch"] = "";
                //}
                //if (dt.Rows[i]["Section"] == "&nbsp;")
                //{
                //    dt.Rows[i]["Section"] = "";
                //}

                SqlCommand cmd = new SqlCommand("[dbo].[proc_insertStudentAttendanceHeaderAndLine]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
                cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
                cmd.Parameters.AddWithValue("@Course", dt.Rows[i]["Course"]);
                cmd.Parameters.AddWithValue("@Semester", dt.Rows[i]["Semester/Year"]);
                cmd.Parameters.AddWithValue("@Section", dt.Rows[i]["Section"].ToString().Replace("&nbsp;", ""));
                cmd.Parameters.AddWithValue("@Group", dt.Rows[i]["Group"].ToString().Replace("&nbsp;", ""));
                cmd.Parameters.AddWithValue("@Batch", dt.Rows[i]["Batch"].ToString().Replace("&nbsp;", ""));
                cmd.Parameters.AddWithValue("@SubjectCode", dt.Rows[i]["Subject Code"]);
                cmd.Parameters.AddWithValue("@SubjectType", dt.Rows[i]["SubjectType"]);
                cmd.Parameters.AddWithValue("@Hour", Hour);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@FacultyCode", FacultyCode);
                cmd.Parameters.AddWithValue("@AttendanceType", AttendanceType);
                cmd.Parameters.AddWithValue("@Unit", Unit);
                cmd.Parameters.AddWithValue("@Topic", Topic);
                cmd.Parameters.AddWithValue("@CollegeCode", CollegeCode);
                cmd.Parameters.AddWithValue("@EnrollmentNo", "ex");
                cmd.Parameters.AddWithValue("@StudentName", dt.Rows[i]["Student Name"]);
                cmd.Parameters.AddWithValue("@StudentNo", dt.Rows[i]["StudentNo"]);
                cmd.Parameters.AddWithValue("@Attendance", dt.Rows[i]["Today"]);// attendance absent/persent          
                if (Chkdetained.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Detained", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Detained", 0);
                }
                cmd.ExecuteNonQuery();
            }
        }
        catch { Result = "Error"; }
        finally
        {
            con.Close();
        }
        return Result;
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        No_ = grdStudentAttendance.SelectedDataKey.Value.ToString();
        // BindAttendanceSummaryGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal3').modal();", true);
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AttendanceView.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            grdAttendanceReport.AllowPaging = false;
            // BindAttendanceSummaryGrid();
            grdAttendanceReport.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdAttendanceReport.HeaderRow.Cells)
            {
                cell.BackColor = grdAttendanceReport.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdAttendanceReport.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdAttendanceReport.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdAttendanceReport.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            grdAttendanceReport.RenderControl(hw);

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
    public void EnableDisable(bool TF)
    {

        ddlLecture.Enabled = TF;
        txtDate.Enabled = TF;

    }
    public void Blank()
    {
        EnableDisable(true);
        ddlLecture.DataBind();
        pnlCheckBox.Visible = false;
        btnSubmit.Visible = false;
        chkPresentAll.Checked = true;
        chkAbsentAll.Checked = false;
        Chkdetained.Checked = false;

        // txtTopic.Text = "";
    }
    public void bindAcademicYear()
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

    protected void grdStudentAttendance_Sorting(object sender, GridViewSortEventArgs e)
    {
        this.studentGrid(e.SortExpression);
    }
    //added by: Bhupendra Yadav
    //Purpose : Sorting Purpose
    public DataTable studentGrid(string sortExpression = null)
    {
        BindTableforsorting();

        if (sortExpression != null)
        {

            DataView dv = dtSorting.AsDataView();
            this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
            dv.Sort = sortExpression + " " + this.SortDirection;
            grdStudentAttendance.DataSource = dv;
        }
        else
        {
            grdStudentAttendance.DataSource = dtSorting;
        }
        // grdStudentAttendance.DataSource = dtStudent;
        grdStudentAttendance.DataBind();
        if (dtSorting.Rows.Count > 0)
        {

            btnSubmit.Visible = true;
            pnlCheckBox.Visible = true;
        }

        if (grdStudentAttendance.Rows.Count > 0)
        {
            EnableDisable(false);
        }
        else
        { EnableDisable(true); }

        return dtSorting;
    }

    public void BindTableforsorting()
    {
        dtSorting.Clear();
        for (int i = 0; i < grdStudentAttendance.Columns.Count; i++)
        {
            if (i == 1)
            {
                dtSorting.Columns.Add("Enrollment No");
            }
            else if (i == 5)
            {
                dtSorting.Columns.Add("Description");
            }
            else if (i == 6)
            {
                dtSorting.Columns.Add("SemesterYear");
            }
            else if (i == 12)
            {
                dtSorting.Columns.Add("No_");
            }
            else
            {
                dtSorting.Columns.Add(grdStudentAttendance.Columns[i].HeaderText);
            }
        }

        dtSorting.Columns[11].ColumnName = "SubjectType";
        dtSorting.Columns[12].ColumnName = "No_";

        int r = 0;
        foreach (GridViewRow row in grdStudentAttendance.Rows)
        {
            DataRow dr = dtSorting.NewRow();
            for (int j = 0; j < grdStudentAttendance.Columns.Count; j++)
            {
                CheckBox rb = (CheckBox)row.FindControl("chkboxAttendance");
                if (j == 10)
                {
                    if (rb.Checked == true)
                    {
                        row.Cells[j].Text = "1";
                    }
                    else
                        row.Cells[j].Text = "0";
                }
                if (j == 1)
                {
                    if (row.Cells[j].Text == "&nbsp;")
                    {
                        row.Cells[j].Text = "";
                    }
                    dr["Enrollment No"] = row.Cells[j].Text;
                }
                else if (j == 5)
                {
                    dr["Description"] = row.Cells[j].Text;
                }
                else if (j == 6)
                {
                    dr["SemesterYear"] = row.Cells[j].Text;
                }
                else if (j == 10)
                {
                    dr["Today"] = Convert.ToInt32(row.Cells[j].Text);
                }
                else if (j == 11)
                {
                    dr["SubjectType"] = row.Cells[j].Text;
                }
                else if (j == 12)
                {
                    dr["No_"] = row.Cells[j].Text;
                }
                else
                {
                    if (row.Cells[j].Text == "&nbsp;")
                    {
                        row.Cells[j].Text = "";
                    }
                    dr[grdStudentAttendance.Columns[j].HeaderText] = row.Cells[j].Text;
                }
            }
            HiddenField hfSubjectType = (HiddenField)row.FindControl("hfSubjectType");
            HiddenField hfStudentNo = (HiddenField)row.FindControl("hfStudentNo");
            dtSorting.Rows.Add(dr);
            dtSorting.Rows[r]["SubjectType"] = hfSubjectType.Value;
            dtSorting.Rows[r]["No_"] = hfStudentNo.Value;
            r = r + 1;
        }


    }

}