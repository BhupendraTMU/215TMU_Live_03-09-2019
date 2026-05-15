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
public partial class Faculty_StudentExtraAttendanceMark : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    DataTable dt2 = new DataTable();
    static int cnt = 0;
    static String No_ = "";
    static bool Save = false;
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
                bindLecture();
                bindAcademicYear();
                BindExtraType();
            }
        }
        catch(Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }    
    
    protected void chkboxEditAttendance_CheckedChanged(object sender, EventArgs e)
    {       
            for (int i = 0; i < cnt; i++)
            {
                CheckBox col = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox1stAttendance");
                CheckBox col1 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox2ndAttendance");
                CheckBox col2 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox3rdAttendance");
                col.Enabled = false;
                col1.Enabled = false;
                col2.Enabled = false;
            }
        
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID1", Session["uid"].ToString());
        cmd.Parameters.Add("@ID", drpCourse.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }
    public void bindSectionList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();

    }
          
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
     
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSectionList();
    }   
    DataTable dt12 = new DataTable();
    public void ValidateAndBindGrid()
    {
        string Extra = "2";        
        DateTime dt11 = new DateTime();
        dt11 = Convert.ToDateTime(txtDate.Text);
        string date = dt11.ToString("MM-dd-yyyy");
        SqlCommand cmd1 = new SqlCommand("[proc_ValidateStudentForMarkExtraAttendanceFromAttendanceLine]", con);  
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd1.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd1.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd1.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd1.Parameters.Add("@LectureNo", drpLecture.SelectedItem.ToString());
        cmd1.Parameters.Add("@Date", date);        
        cmd1.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);        
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
            ViewState["dt"] = null;
            BindGrid();
       
        }
        if (grdAttendanceDetails.Rows.Count > 0)
        {
            EnableDisable(false);
        }
        else
        { EnableDisable(true); }
    }
    public void BindGrid()
    {
        pnlGrid.Visible = true;
        pnlCheckBox.Visible = true;
        btnSubmit.Visible = true;
        lblMessage.Visible = false;
        DataTable dt = new DataTable();
        if (ViewState["dt"] == null)
        {
        SqlCommand cmd = new SqlCommand("[proc_GetStudentForMarkExtraAttendanceFromStudentCollege]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ViewState["dt"] = dt;
        }
        else
        {
            dt = ViewState["dt"] as DataTable;
        }           
        cnt = dt.Rows.Count;
        grdAttendanceDetails.DataSource = dt;
        grdAttendanceDetails.DataBind();
    }
    //public void BindTable()
    //{        
    //    for (int i = 0; i < grdAttendanceDetails.Columns.Count; i++)
    //    {
    //        dt12.Columns.Add(grdAttendanceDetails.HeaderRow.Cells[i].Text);
    //    }
    //    foreach (GridViewRow row in grdAttendanceDetails.Rows)
    //    {
    //        DataRow dr = dt12.NewRow();
    //        for (int j = 0; j < grdAttendanceDetails.Columns.Count; j++)
    //        {
    //            CheckBox rb = (CheckBox)row.FindControl("chkboxAttendance");
    //            if (j == 6)
    //            {
    //                if (rb.Checked == true)
    //                {
    //                    row.Cells[j].Text = "0";
    //                }
    //                else
    //                    row.Cells[j].Text = "1";
    //            }
    //            CheckBox rb1 = (CheckBox)row.FindControl("chkbox1stAttendance");
    //            if (j == 3)
    //            {
    //                if (rb1.Checked == true)
    //                {
    //                    row.Cells[j].Text = "0";
    //                }
    //                else
    //                    row.Cells[j].Text = "1";
    //            }
    //            CheckBox rb2 = (CheckBox)row.FindControl("chkbox2ndAttendance");
    //            if (j == 4)
    //            {
    //                if (rb2.Checked == true)
    //                {
    //                    row.Cells[j].Text = "0";
    //                }
    //                else
    //                    row.Cells[j].Text = "1";
    //            }
    //            CheckBox rb3 = (CheckBox)row.FindControl("chkbox3rdAttendance");
    //            if (j == 5)
    //            {
    //                if (rb3.Checked == true)
    //                {
    //                    row.Cells[j].Text = "0";
    //                }
    //                else
    //                    row.Cells[j].Text = "1";
    //            }
    //            dr[grdAttendanceDetails.HeaderRow.Cells[j].Text] = row.Cells[j].Text;
    //        }
    //        dt12.Rows.Add(dr);
    //    }
    //}    
    public void BindTable()
    {
        for (int i = 0; i < grdAttendanceDetails.Columns.Count-1; i++)
        {
            dt12.Columns.Add(grdAttendanceDetails.HeaderRow.Cells[i].Text);
        }
        int r = 0;
        foreach (GridViewRow row in grdAttendanceDetails.Rows)
        {
            DataRow dr = dt12.NewRow();
            for (int j = 0; j < grdAttendanceDetails.Columns.Count-1; j++)
            {
                CheckBox rb = (CheckBox)row.FindControl("chkboxAttendance");
                if (j == 3)
                {
                    if (rb.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                dr[grdAttendanceDetails.HeaderRow.Cells[j].Text] = row.Cells[j].Text;
            }
            
           // HiddenField hfStudentNo = (HiddenField)row.FindControl("hfStudentNo");
            dt12.Rows.Add(dr);           
           // dt12.Rows[r]["StudentNo"] = hfStudentNo.Value;
            r = r + 1;
        }


    }
    protected void btnShow_Click(object sender, EventArgs e)
    {   
        ValidateAndBindGrid();  
        Save = true;
    }
   
    public void Blank()
    {
        EnableDisable(true);
        drpCourse.SelectedIndex = 0;        
        drpSemester.DataSource = "";
        drpSemester.DataBind();
        drpSection.DataSource = "";
        drpSection.DataBind();             
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
        SqlCommand cmd = new SqlCommand("proc_GetFreeLectureFromTimeTableForExtra", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Text);        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpLecture.DataSource = dt;
        drpLecture.DataTextField = "Details";
        drpLecture.DataValueField = "No_";
        drpLecture.DataBind();        
       
    }
    
   
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        bindLecture();
       
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
    public void BindExtraType()
    {
        SqlCommand cmd = new SqlCommand("proc_GetExtraSubject", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        drpExtra .DataSource = dt1;
        drpExtra.DataTextField = "Details";
        drpExtra.DataValueField = "No_";
        drpExtra.DataBind();
    }
    public void UpdateAttendance()
    {
        int count1 = dt2.Rows.Count > 3 ? 3 : dt2.Rows.Count;
        for (int i = 0; i < count1 && i < 3; i++)
        {
            string DocumentNo = FDL.GetNextStudentMarkAttendanceNumber();
            int AttendanceType = 2;
            SqlCommand cmd1 = new SqlCommand("proc_GetStudentDetailsForUpdateAttendance_", con); //ok
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@CourseCode",drpCourse.SelectedValue);
            cmd1.Parameters.Add("@Semesteryear",drpSemester.SelectedValue);
            cmd1.Parameters.Add("@Section",drpSection.SelectedValue);
            cmd1.Parameters.Add("@FacultyCode",lblFacultyCode.Text);
            
            cmd1.Parameters.Add("@Date",Convert.ToDateTime(dt2.Rows[i]["Date"]).ToString("MM-dd-yyyy"));
            cmd1.Parameters.Add("@LectureNo",dt2.Rows[i]["HourNo"].ToString());
            
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            for (int j = 0; j < dt12.Rows.Count; j++)
            {
               
               
                if (dt1.Rows.Count == 0)   
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("[dbo].[proc_insertStudentAttendanceHeaderAndLine]", con); //ok new
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
                cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
                cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
                cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
                cmd.Parameters.AddWithValue("@Group", "");
                cmd.Parameters.AddWithValue("@Batch", "");
                cmd.Parameters.AddWithValue("@SubjectCode", "");
                cmd.Parameters.AddWithValue("@SubjectType", "");
                cmd.Parameters.AddWithValue("@Hour", dt2.Rows[i]["HourNo"].ToString());
                cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()));
                cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@AttendanceType", AttendanceType);
                cmd.Parameters.AddWithValue("@Unit", "");
                cmd.Parameters.AddWithValue("@Topic", txtTopic.Text);
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd.Parameters.AddWithValue("@EnrollmentNo", "ex");
                cmd.Parameters.AddWithValue("@StudentName", dt12.Rows[j][1].ToString());
                cmd.Parameters.AddWithValue("@StudentNo", dt12.Rows[j][2].ToString());
                cmd.Parameters.AddWithValue("@Attendance", dt12.Rows[j][5 - i].ToString());    // attendance absent/persent                
                cmd.ExecuteNonQuery();
            }
                else
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand("proc_UpdateStudentAttendance_", con); //ok
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@StudentNo", dt12.Rows[j][2].ToString());
                    cmd.Parameters.Add("@Date", Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()));
                    cmd.Parameters.Add("@AttendanceType", dt12.Rows[j][5 - i].ToString());
                    cmd.Parameters.Add("@SubjectCode", "");
                    cmd.Parameters.Add("@Section", drpSection.SelectedValue);
                    cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
                    cmd.Parameters.Add("@Hour", dt2.Rows[i]["HourNo"].ToString());
                    cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                    cmd.Parameters.AddWithValue("@Group","");//ashu 25-08-2016
                    cmd.Parameters.AddWithValue("@Batch", "");//ashu 25-08-2016
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
    public void bindGridHeader()
    {
        string remedial = "2";        
        SqlCommand cmd2 = new SqlCommand("proc_GetDateAndHourForMarkAtt_", con); //ok---
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd2.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd2.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd2.Parameters.Add("@FacultyCode", Session["uid"].ToString());       
        cmd2.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd2.Parameters.Add("@LectureNo", drpLecture.SelectedValue);       
        cmd2.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue); 
        cmd2.Parameters.Add("@Remedial", remedial);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        da2.Fill(dt2);
        int j = 5;
        if (dt2.Rows.Count > 0)
        {
            for (int g = 0; g < dt2.Rows.Count; g++)
            {
                if (j > 2)
                {
                    grdAttendanceDetails.HeaderRow.Cells[j].Text = dt2.Rows[g]["Date"].ToString() + " / L" + dt2.Rows[g]["HourNo"].ToString();
                    j--;
                }
            }
        }
    }
  
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        if (Save == true)
        {
            btnSave1.Enabled = false;
          //  bindGridHeader();
            btnSubmit.Enabled = false;
            BindTable();
            int LectureNo = Convert.ToInt32(drpLecture.Text); int i = LectureNo;
            int AttendanceType = 2;//--added on 29-12-2016
                    FDL.InsertStudentAttendanceHeaderAndLine("", drpAcademicYear.SelectedValue, drpCourse.SelectedValue, drpSemester.SelectedItem.ToString(), drpSection.SelectedValue,
          "", "", drpExtra.SelectedValue, "Extra", i, txtDate.Text, lblFacultyCode.Text, AttendanceType, "", txtTopic.Text, Session["GlobalDimension1Code"].ToString(), dt12);  //ASHU---29-12-2016                
            lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
            //bindGrid();
            Blank();
            lblMessage.Visible = false;
            btnSubmit.Enabled = true;
            btnSave1.Enabled = true;
            Save = false;
            System.Threading.Thread.Sleep(2000);
            bindLecture();
        }
        else
        {

        }
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
       
      
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        No_ = grdAttendanceDetails.SelectedDataKey.Value.ToString();
        BindAttendanceSummaryGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal3').modal();", true);
    }
    public void BindAttendanceSummaryGrid()
    {
        SqlCommand cmd = new SqlCommand("proc_getAttendanceRecordForMarkAttendance_", con); //ok
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StudentNo",No_);       
        cmd.Parameters.Add("@CourseCode",drpCourse.SelectedValue);
        cmd.Parameters.Add("@Semester",drpSemester.SelectedValue);
       
        SqlDataAdapter da=new SqlDataAdapter (cmd);
        DataTable dt=new DataTable ();
        da.Fill(dt);
        grdAttendanceReport.DataSource=dt;
        grdAttendanceReport.DataBind();
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
            BindAttendanceSummaryGrid();
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
        drpAcademicYear.Enabled = TF;
        drpCourse.Enabled = TF;
        drpSemester.Enabled = TF;
        drpSection.Enabled = TF;     
        drpLecture.Enabled = TF;       
        txtTopic.Enabled = TF;        
    }
    


   
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ShowRemedialClass();
    }
    protected void drpLecture_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void grdAttendanceDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = ViewState["dt"] as DataTable;
        dt.Rows[index].Delete();
        dt.AcceptChanges();
        ViewState["dt"] = dt;
        
         // lblMsg.Text = "Record Deleted !";
         BindGrid();
    }
    protected void grdAttendanceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int row = e.Row.RowIndex + 1;
            ((System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("link_DeleteRow")).Attributes.Add("onclick", "delrow(" + row + ")");
        }
    }
}