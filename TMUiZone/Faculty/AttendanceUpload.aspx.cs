using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;


using System.Globalization;
using OfficeOpenXml;


public partial class Faculty_AttendanceUpload : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"].ToString() != null)
            {
                if (Session["UserGroup"].ToString() == "FACULTY" || Session["UserGroup"].ToString() == "PRINCIPAL")
                {
                    if (!IsPostBack)
                    {

                    }
                }
                else
                {
                    Response.Redirect("../Default.aspx");
                }
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ExcelUpload();

    }
    public void ExcelUpload()// Sandeep
    {
        if (FileUpload1.HasFile)
        {
            if (Path.GetExtension(FileUpload1.FileName) == ".xlsx" || Path.GetExtension(FileUpload1.FileName) == ".xls")
            {
                DataTable dtvs = new DataTable();
                // MemoryStream stream = new MemoryStream();
                ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
                grdUpload.DataSource = package.ToDataTable();
                grdUpload.DataBind();
                dtvs = package.ToDataTable();
                if (dtvs.Rows.Count > 0)
                {
                    Session["dt"] = dtvs;
                    ValidateData(dtvs);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Please Check Excell Sheeet');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload Excell Sheeet Only !');", true);
                return;
            }

        }

    }
    public DataTable GetDataTable(GridView dtg)//Sandeep
    {
        DataTable dt = new DataTable();

        // add the columns to the datatable            
        if (dtg.HeaderRow != null)
        {

            for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
            {
                dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
            }
        }

        //  add each of the data rows to the table
        foreach (GridViewRow row in dtg.Rows)
        {
            DataRow dr;
            dr = dt.NewRow();

            for (int i = 0; i < row.Cells.Count; i++)
            {
                dr[i] = row.Cells[i].Text.Replace(" ", " ");
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtRecord = (DataTable)Session["dt"];
            string[] selectedColumns = new[] { "Date", "Hour" };

            DataTable dt = new DataView(dtRecord).ToTable(true, selectedColumns);
            DataView view = new DataView(dtRecord);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int AP = 0;
                if (dtRecord.Rows[i]["Today"].ToString() == "P")
                {
                    AP = 0;
                }
                else
                {
                    AP = 1;
                }
                view.RowFilter = "Date = '" + dt.Rows[i]["Date"].ToString() + "' AND Hour = " + dt.Rows[i]["Hour"].ToString() + "";
                DataTable dtDistinctRecord = view.ToTable();
                InsertStudentAttendanceHeaderAndLine1(dtRecord.Rows[i]["Academic Year"].ToString(), dtRecord.Rows[i]["Course"].ToString(), dtRecord.Rows[i]["Semester"].ToString(), dtRecord.Rows[i]["Section"].ToString(), dtRecord.Rows[i]["Group"].ToString(), dtRecord.Rows[i]["Batch"].ToString(), dtRecord.Rows[i]["Subject"].ToString(), dtRecord.Rows[i]["Subject Type"].ToString(), Convert.ToInt32(dt.Rows[i]["Hour"]), dt.Rows[i]["Date"].ToString(), dtRecord.Rows[i]["Staff"].ToString(), AP, "", dtRecord.Rows[i]["Remark"].ToString(), Session["GlobalDimension1Code"].ToString(), dtDistinctRecord);




                // FDL.InsertStudentAttendanceHeaderAndLine
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
            grdUpload.DataSource = null;
            grdUpload.DataBind();
            btnSave.Visible = false;
        }
        catch (Exception ex)
        {

        }
        // hfUploadSave.Value = "SAVE";
        //Import_To_Grid(hfFilePath.Value, hfExtension.Value, rbHDR.SelectedItem.Text);
    }

    public void Save(DataTable dt)
    {
        DataTable dtRecord = new DataTable();
        dtRecord = dt;
        if (con.State == ConnectionState.Closed) { con.Open(); }
        for (int i = 0; i < dtRecord.Rows.Count; i++)
        {
            SqlCommand cmd1 = new SqlCommand("proc_GetSubjectDetailsForUploadTimeTable", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@CourseCode", dtRecord.Rows[i]["Course Code"].ToString());
            cmd1.Parameters.Add("@Subject", dtRecord.Rows[i]["Subject Code"].ToString());
            cmd1.Parameters.Add("@AcademicYear", dtRecord.Rows[i]["Academic Year"].ToString());
            // if (con.State == ConnectionState.Closed) { con.Open(); }
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            cmd1.ExecuteNonQuery();
            // con.Close();
            SqlCommand cmd = new SqlCommand("proc_CreateTimeTableGeneration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 130;
            cmd.Parameters.Add("@DayNo", dtRecord.Rows[i]["Day No"].ToString());
            cmd.Parameters.Add("@CourseCode", dtRecord.Rows[i]["Course Code"].ToString());
            cmd.Parameters.Add("@SemesterYear", dtRecord.Rows[i]["Semester/Year"].ToString());
            cmd.Parameters.Add("@HouNo", dtRecord.Rows[i]["From Hour No"].ToString());
            cmd.Parameters.Add("@SubjectCode", dtRecord.Rows[i]["Subject Code"].ToString());
            cmd.Parameters.Add("@FacultyCode", dtRecord.Rows[i]["Faculty Code"].ToString());//Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", dtRecord.Rows[i]["Academic Year"].ToString());
            if (dtRecord.Rows[i]["Section"].ToString() == "&nbsp;")
            {
                cmd.Parameters.Add("@Section", "");
            }
            else
            {
                cmd.Parameters.Add("@Section", dtRecord.Rows[i]["Section"].ToString());
            }
            cmd.Parameters.Add("@SubjectType", dt1.Rows[0]["Subject Type"].ToString());
            cmd.Parameters.Add("@FacultyName", Session["Fulname"].ToString());
            cmd.Parameters.Add("@SubjectDescription", dt1.Rows[0]["Description"].ToString());
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@RoomAllocation", dtRecord.Rows[i]["Room No"].ToString());
            cmd.Parameters.Add("@SubjectClassification", dt1.Rows[0]["Subject Classification"].ToString());
            cmd.Parameters.Add("@FromDate", Convert.ToDateTime(dtRecord.Rows[i]["From Date"].ToString()).ToString("dd/MMM/yyyy"));
            cmd.Parameters.Add("@ToDate", Convert.ToDateTime(dtRecord.Rows[i]["To Date"].ToString()).ToString("dd/MMM/yyyy"));
            if (dtRecord.Rows[i]["Group"].ToString() == "&nbsp;")
            {
                cmd.Parameters.Add("@Group", "");
            }
            else
            {
                cmd.Parameters.Add("@Group", dtRecord.Rows[i]["Group"].ToString());
            }
            if (dtRecord.Rows[i]["Batch"].ToString() == "&nbsp;")
            {
                cmd.Parameters.Add("@Batch", "");
            }
            else
            {
                cmd.Parameters.Add("@Batch", dtRecord.Rows[i]["Batch"].ToString());
            }
            cmd.Parameters.Add("@HouNoTo", dtRecord.Rows[i]["To Hour No"].ToString());
            //  if (con.State == ConnectionState.Closed)
            //     con.Open();
            int a = cmd.ExecuteNonQuery();
            // con.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
            grdUpload.DataSource = null;
            grdUpload.DataBind();
            btnSave.Visible = false;

        }
        if (con.State == ConnectionState.Open) { con.Close(); }
    }
    public void ValidateData(DataTable dtRecord)
    {
        String Error = "";

        if (dtRecord.Columns.Count != 19)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Your Excel Sheet Format !');", true);
            //Error = "Error";
            return;
        }

        if (dtRecord.Columns[0].ColumnName == "Course" && dtRecord.Columns[1].ColumnName == "Semester" && dtRecord.Columns[2].ColumnName == "Subject" && dtRecord.Columns[3].ColumnName == "Date" && dtRecord.Columns[4].ColumnName == "Hour" && dtRecord.Columns[5].ColumnName == "Student No" && dtRecord.Columns[6].ColumnName == "Today" && dtRecord.Columns[7].ColumnName == "Academic Year" && dtRecord.Columns[8].ColumnName == "Student Name" &&  dtRecord.Columns[9].ColumnName == "Subject Type" && dtRecord.Columns[10].ColumnName == "Section" && dtRecord.Columns[11].ColumnName == "College Code" && dtRecord.Columns[12].ColumnName == "Year" && dtRecord.Columns[13].ColumnName == "Batch" && dtRecord.Columns[14].ColumnName == "Group" && dtRecord.Columns[15].ColumnName == "Staff" && dtRecord.Columns[16].ColumnName == "Remedial" && dtRecord.Columns[17].ColumnName == "Detained" && dtRecord.Columns[18].ColumnName == "Remark")
        {

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Your Excel Sheet Format !');", true);
            Error = "Error";
            return;
        }
        grdUpload.DataSource = dtRecord; grdUpload.DataBind();
        DataTable dtValidate = new DataTable(); dtValidate = dtRecord;

        string Result = "";
        if (con.State == ConnectionState.Closed)
            con.Open();
        //try
        //{
        SqlCommand cmd = new SqlCommand("[dbo].[proc_ValidateTimetableToUploadData]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@tblAttendance", dtRecord);
        cmd.CommandTimeout = 300;
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        if (dtCL.Rows.Count > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Time Table Not Created !');", true);
            Error = "Error";
            return;
        }






        for (int i = 0; i < dtRecord.Rows.Count; i++)
        {

            for (int j = 0; j < dtRecord.Columns.Count; j++)
            {
                if ((dtRecord.Rows[i]["Course"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Course") || (dtRecord.Rows[i]["Subject"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Subject") || ((dtRecord.Rows[i]["Semester"].ToString() == "" && dtRecord.Rows[i]["Year"].ToString() == "") && (dtRecord.Columns[j].ColumnName == "Semester" && dtRecord.Columns[j].ColumnName == "Year")) || (dtRecord.Rows[i]["Date"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Date") || (dtRecord.Rows[i]["Hour"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Hour") || (dtRecord.Rows[i]["Student No"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Student No") || (dtRecord.Rows[i]["Today"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Today") || (dtRecord.Rows[i]["Academic Year"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Academic Year") || (dtRecord.Rows[i]["Student Name"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Student Name") || (dtRecord.Rows[i]["Subject Type"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Subject Type") || (dtRecord.Rows[i]["College Code"].ToString() == "" && dtRecord.Columns[j].ColumnName == "College Code") || (dtRecord.Rows[i]["Staff"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Staff") || (dtRecord.Rows[i]["Remedial"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Remedial") || (dtRecord.Rows[i]["Detained"].ToString() == "" && dtRecord.Columns[j].ColumnName == "Detained"))
                {
                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Your Data !');", true);
                    Error = "Error";
                    grdUpload.Rows[i].Cells[j].BackColor = System.Drawing.Color.Yellow;//column no
                    // return;
                }
                else
                {
                    btnSave.Visible = true;
                    //FDL.InsertStudentAttendanceHeaderAndLine(drpAcademicYear.SelectedValue, drpCourse.SelectedValue, drpSemester.SelectedItem.ToString(), drpSection.SelectedValue, ddlGroup.SelectedValue, ddlBatch.SelectedValue, drpSubject.SelectedValue, lblSubjectType.Text, i, txtDate.Text, lblFacultyCode.Text, AttendanceType, drpUnit.SelectedValue, txtTopic.Text, Session["GlobalDimension1Code"].ToString(), dt12);
                }
            }
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {

    }
    public string InsertStudentAttendanceHeaderAndLine1(string AcademicYear, string CourseCode, string Semester, string Section, string Group, string Batch, string SubjectCode, string SubjectType, int Hour, string Date, string FacultyCode, int AttendanceType, string Unit, string Topic, string CollegeCode, DataTable dt)
    {
        string Result = "";
        if (con.State == ConnectionState.Closed)
            con.Open();
        //try
        //{
        SqlCommand cmd = new SqlCommand("[dbo].[proc_insertStudentAttendanceHeaderAndLine_byTable1]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear);
        cmd.Parameters.AddWithValue("@Course", CourseCode);
        cmd.Parameters.AddWithValue("@Semester", Semester);
        cmd.Parameters.AddWithValue("@Section", Section);
        cmd.Parameters.AddWithValue("@Group", Group);
        cmd.Parameters.AddWithValue("@Batch", Batch);
        cmd.Parameters.AddWithValue("@SubjectCode", SubjectCode);
        cmd.Parameters.AddWithValue("@SubjectType", SubjectType);
        cmd.Parameters.AddWithValue("@Hour", Hour);
        cmd.Parameters.AddWithValue("@Date", Date);
        cmd.Parameters.AddWithValue("@FacultyCode", FacultyCode);
        cmd.Parameters.AddWithValue("@AttendanceType", AttendanceType);
        cmd.Parameters.AddWithValue("@Unit", Unit);
        cmd.Parameters.AddWithValue("@Topic", Topic);
        cmd.Parameters.AddWithValue("@CollegeCode", CollegeCode);
        cmd.Parameters.AddWithValue("@tblAttendance", dt);
        cmd.CommandTimeout = 300;
        cmd.ExecuteNonQuery();
        //}
        //catch { Result = "Error"; }
        //finally
        //{
        //    con.Close();
        //}
        return Result;
    }


}