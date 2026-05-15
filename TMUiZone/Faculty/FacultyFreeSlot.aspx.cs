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

public partial class Faculty_FacultyFreeSlot : System.Web.UI.Page
{
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()); 
    
    static string Name = "";
    static string FacultyCode = "";
    static DateTime ArrangmentDate;
    static int LectureNo = 0; 
    static String CourseCode = "";
    static string Semester = "";
    static string Section = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindFacultWhoApply();
                bindDrpCourseList();
                BindGrid();
                bindInboxGrid();
                BindSubstituteEmployeeName();
                BindRequestedByName();
                BindReportGrid();
                bindAcademicYear();
            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {
                btnShowReport.Visible = true;
                tdFaculty.Visible = true;
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader", "$('#grdInboxBody').hide(); ", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader1", "$('#grdOutboxBody').hide(); ", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader2", "$('[id$=pnlReport]').hide(); ", true);
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSemester();
        BindFaculty();
    }
    protected void drpSemesterYear_SelectedIndexChanged(object sender, EventArgs e)
    {
       // bindDrpCourseList();
        BindFaculty();
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd1 = new SqlCommand("proc_checkEntryInArrangementDetails", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        if (ddlFaculty.SelectedValue == "")
        {
            cmd1.Parameters.Add("@RequestedBy", Session["uid"].ToString());
        }
        else
        { 
            cmd1.Parameters.Add("@RequestedBy", ddlFaculty.SelectedValue); 
        }
        cmd1.Parameters.Add("@Hour", drpLecture.SelectedValue);
        cmd1.Parameters.Add("@SubstituteEmployeeCode", drpFacultyName.SelectedValue);
        cmd1.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', '" + drpFacultyName.SelectedItem.ToString() + " has been assigned the lecture');", true);

        }
        else
        {
            SqlCommand cmd = new SqlCommand("[proc_InsertIntoArrangementDetails_CourseCollege]", con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (ddlFaculty.SelectedValue == "")
                cmd.Parameters.Add("@RequestedBy", Session["uid"].ToString());
            else
                cmd.Parameters.Add("@RequestedBy", ddlFaculty.SelectedValue);
            cmd.Parameters.Add("@HourNo", drpLecture.SelectedValue);
            cmd.Parameters.Add("@SubstituteEmployeeCode", drpFacultyName.SelectedValue);
            cmd.Parameters.Add("@ArrangementDate", Convert.ToDateTime(txtDate.Text));
            cmd.Parameters.Add("@SubstituteEmployeeName", drpFacultyName.SelectedItem.ToString());
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());  // added on 22-02-2017
            // cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);  // added on 22-02-2017
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("proc_GetPhoneNoOfEmployee", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@FacultyCode", drpFacultyName.SelectedValue);
            string MobileNo = cmd2.ExecuteScalar().ToString();
            // MobileNo = "";//comment it 
            if (!string.IsNullOrEmpty(MobileNo))
            {
                SMS(MobileNo, "Lecture Arrangement Request sent by '"+ Session["Fulname"].ToString() +"' ");
            }
            //SMS("", "");
            Clear();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Assign Successfully');", true);
        }
        con.Close();
        BindGrid();
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        bindLecture();
    }
    public void bindDrpCourseList()
    {
        DataTable dt = new DataTable();
        if (ddlFaculty.SelectedValue == "")
        {
            SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", Session["uid"].ToString());
            cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            drpCourse.DataSource = dt;
            drpCourse.DataTextField = "Details";
            drpCourse.DataValueField = "No_";
            drpCourse.DataBind();
        }
        else
        {
            SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", ddlFaculty.SelectedValue);
            cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            drpCourse.DataSource = dt;
            drpCourse.DataTextField = "Details";
            drpCourse.DataValueField = "No_";
            drpCourse.DataBind();
        }
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    public void bindSemester()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlFaculty.SelectedValue == "")
            cmd.Parameters.Add("@ID1", Session["uid"].ToString());
        else
            cmd.Parameters.Add("@ID1", ddlFaculty.SelectedValue);
        cmd.Parameters.Add("@ID", drpCourse.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSemesterYear.DataSource = dt;
        drpSemesterYear.DataTextField = "Details";
        drpSemesterYear.DataValueField = "No_";
        drpSemesterYear.DataBind();
    }
    public void bindLecture()
    {
        if (txtDate.Text != "")
        {
            SqlCommand cmd = new SqlCommand("proc_GetLecture", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
            if (ddlFaculty.SelectedValue == "") 
            { cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString()); }
            else 
            { cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue); }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpLecture.DataSource = dt;
            drpLecture.DataTextField = "Details";
            drpLecture.DataValueField = "No_";
            drpLecture.DataBind();
        }
    }
    protected void drpLecture_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFaculty();
        chkLab.Checked = false;
        BindLabPracticalFaculty();
    }
    public void BindFaculty()
    {
        if (txtDate.Text != "")
        {
            SqlCommand cmd = new SqlCommand("proc_GetFreeFaculty", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@HourNo", drpLecture.SelectedValue);
            cmd.Parameters.Add("@Date", txtDate.Text);
            cmd.Parameters.Add("@Semester", drpSemesterYear.SelectedValue);
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            //cmd.Parameters.Add("@CollegeCode", Session["FacultyCollege"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpFacultyName.DataSource = dt;
            drpFacultyName.DataTextField = "Details";
            drpFacultyName.DataValueField = "No_";
            drpFacultyName.DataBind();
        }
    }
    public void BindLabPracticalFaculty()
    {
        if (txtDate.Text != "" && drpLecture.SelectedValue.ToString() !="" )
        {
            SqlCommand cmd = new SqlCommand("proc_GetLabFaculty", con);//@FacultyCode varchar(20),@CollegeCode varchar(20))
            cmd.CommandType = CommandType.StoredProcedure;           
            cmd.Parameters.Add("@AttendanceDate", txtDate.Text);
            cmd.Parameters.Add("@HourNo", drpLecture.SelectedValue);
             if (ddlFaculty.SelectedValue == "")
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            else
            cmd.Parameters.Add("@FacultyCode", ddlFaculty.SelectedValue);           
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpFacultyName.DataSource = dt;
            if (dt.Rows.Count > 1)
            {
                chkLab.Visible = true;
                if (chkLab.Checked == true)
                {
                    drpCourse.SelectedIndex = -1;
                    drpSemesterYear.SelectedIndex = -1;
                    drpCourse.Enabled = false;
                    drpSemesterYear.Enabled = false;
                    drpFacultyName.DataTextField = "Details";
                    drpFacultyName.DataValueField = "No_";
                    drpFacultyName.DataBind();
                }
            }
            else
            {
                drpCourse.Enabled = true;
                drpSemesterYear.Enabled = true;
                chkLab.Visible = false;
            }
        }
    }
    public void BindGrid()
    {
        SqlCommand cmd = new SqlCommand("proc_GetArrangementDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode",Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdArrangementDetails.DataSource = dt;
        grdArrangementDetails.DataBind();
    }
    public void Clear()
    {
        drpCourse.SelectedIndex = 0;
        drpSemesterYear.DataSource = "";
        drpSemesterYear.DataBind();
        txtDate.Text = "";
        drpLecture.DataSource = "";
        drpLecture.DataBind();
        drpFacultyName.DataSource = "";
        drpFacultyName.DataBind();
    }
    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
         MobileNo = "91" + MobileNo;
      // MobileNo = "91" + 9015762885;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            Response.Write(str.ReadToEnd());
            str.Close();
        }
    }
    public void bindInboxGrid()
    {
        SqlCommand cmd = new SqlCommand("proc_GetArrangementDetailsInInbox", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        grdInbox.DataSource = dt;
        grdInbox.DataBind();
    }    
    protected void grdInbox_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string s = e.CommandName;
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvRow = grdInbox.Rows[index];
        Name = gvRow.Cells[0].Text;
        FacultyCode = gvRow.Cells[1].Text;
        CourseCode = gvRow.Cells[2].Text;
        Semester = gvRow.Cells[3].Text;
        if (gvRow.Cells[4].Text == "&nbsp;")
            Section = "";
        else
            Section = gvRow.Cells[4].Text;
        ArrangmentDate = Convert.ToDateTime(gvRow.Cells[6].Text);
        LectureNo = Convert.ToInt16(gvRow.Cells[7].Text);
        Clear();
        BindGrid();
        if (s == "Reject")
        {
            tblAccept.Visible = false;
            tblRejection.Visible = true;
            PAccept.Visible = false;
            PRejection.Visible = true;            
            mpe.Show();
        }
        else if (s == "Accept")
        {
            tblAccept.Visible = true;
            tblRejection.Visible = false;
            PAccept.Visible = true;
            PRejection.Visible = false;
            UpdateArrangmentDetailsForAccept();   //On 26-04-2017         
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd1 = new SqlCommand("proc_UpdateTimeTableGeneration", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@FacultyCode", FacultyCode);
            cmd1.Parameters.Add("@SubstituteFacultyCode", Session["uid"].ToString());
            cmd1.Parameters.Add("@SubstituteFacultyName", Session["Fulname"].ToString());
            cmd1.Parameters.Add("@AttendanceDate", ArrangmentDate.ToString("MM-dd-yyyy"));
            cmd1.Parameters.Add("@CourseCode", CourseCode);
            cmd1.Parameters.Add("@HourNo", LectureNo);
            cmd1.ExecuteNonQuery();
            con.Close();
            string LectureWithDate= "Date- "+ArrangmentDate.ToString("dd MMM yyyy")+" LectureNo - "+LectureNo+"";
            FindMobNoAndSendSMS(FacultyCode, "Accepted", LectureWithDate);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Message', 'ThankYou');", true);
            bindInboxGrid();
        }
        
    }
    public void UpdateArrangmentDetailsForAccept() // on 26-04-2017 by ashu
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand("proc_UpdateArrangmentDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SubstituteEmployeeCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", CourseCode);
        cmd.Parameters.Add("@Semester", Semester);
        cmd.Parameters.Add("@Section", Section);
        cmd.Parameters.Add("@ArrangementDate", ArrangmentDate);
        cmd.Parameters.Add("@HourNo", LectureNo);
        cmd.Parameters.Add("@Status", 1);
        cmd.Parameters.Add("@Remarks", "");
        cmd.Parameters.Add("@Subject", "");
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(con.State==ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand("proc_UpdateArrangmentDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SubstituteEmployeeCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", CourseCode);
        cmd.Parameters.Add("@Semester", Semester);
        cmd.Parameters.Add("@Section", Section);
        cmd.Parameters.Add("@ArrangementDate", ArrangmentDate);
        cmd.Parameters.Add("@HourNo", LectureNo);
        if (tblRejection.Visible == true)
        { 
            cmd.Parameters.Add("@Status", 2);
            cmd.Parameters.Add("@Remarks", txtReasonForRejection.Text);
            cmd.Parameters.Add("@Subject", "");
            cmd.ExecuteNonQuery();
            con.Close();
            string LectureWithDate = "Date- " + ArrangmentDate.ToString("dd MMM yyyy") + " LectureNo - " + LectureNo + "";
            FindMobNoAndSendSMS(FacultyCode, "Rejected", LectureWithDate);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Message', 'Okey');", true);
        }
        //else
        //{ 
        //    cmd.Parameters.Add("@Status", 1);
        //    cmd.Parameters.Add("@Remarks", "");
        //    cmd.Parameters.Add("@Subject", ddlSubject.SelectedValue );
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}       
       
        bindInboxGrid();
    }
    public void BindReportGrid()
    {
        SqlCommand cmd = new SqlCommand("proc_GetArrangementDetailsForReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID",Session["uid"].ToString());
        cmd.Parameters.Add("@RequestedBy",drpRequstedByCode.SelectedValue);
        cmd.Parameters.Add("@SubstituteEmployeeCode",drpSubstituteEmployeeCode.SelectedValue);
        cmd.Parameters.Add("@Status",drpStatus.SelectedValue);
        SqlDataAdapter da=new SqlDataAdapter (cmd);
        DataTable dt=new DataTable ();
        da.Fill(dt);
        grdArrangmentDetailsReport.DataSource=dt;
        grdArrangmentDetailsReport.DataBind();
        if (dt.Rows.Count > 0)
            btnExport.Visible = true;
        else
            btnExport.Visible = false;
    }
    public void BindReportGrid1()
    {
        SqlCommand cmd = new SqlCommand("proc_GetArrangementDetailsForReport1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@RequestedBy", drpRequstedByCode.SelectedValue);
        cmd.Parameters.Add("@SubstituteEmployeeCode", drpSubstituteEmployeeCode.SelectedValue);
        cmd.Parameters.Add("@Status", drpStatus.SelectedValue);
        cmd.Parameters.Add("@FromDate", txtDateFrom.Text);
        cmd.Parameters.Add("@ToDate", txtDateTo.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdArrangmentDetailsReport.DataSource = dt;
        grdArrangmentDetailsReport.DataBind();
        if (dt.Rows.Count > 0)
            btnExport.Visible = true;
        else
            btnExport.Visible = false;
    }
    public void BindRequestedByName()
    {
        SqlCommand cmd=new SqlCommand ("proc_GetRequestedByCodeFromArrangementDetails",con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpRequstedByCode.DataSource = dt;
        drpRequstedByCode.DataTextField = "Details";
        drpRequstedByCode.DataValueField = "No_";
        drpRequstedByCode.DataBind();
    }
    public void BindSubstituteEmployeeName()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSubstituteEmployeeCodeFromArrangementDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSubstituteEmployeeCode.DataSource = dt;
        drpSubstituteEmployeeCode.DataTextField = "Details";
        drpSubstituteEmployeeCode.DataValueField = "No_";
        drpSubstituteEmployeeCode.DataBind();
    }
    protected void drpRequstedByCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReportGrid();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader3", "$('[id$=pnlReport]').show(); ", true);
    }
    protected void drpSubstituteEmployeeCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReportGrid();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader4", "$('[id$=pnlReport]').show(); ", true);
    }
    protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReportGrid();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader5", "$('[id$=pnlReport]').show(); ", true);
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ArrangmentReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages
            grdArrangmentDetailsReport.AllowPaging = false;
            BindReportGrid();
            grdArrangmentDetailsReport.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdArrangmentDetailsReport.HeaderRow.Cells)
            {
                cell.BackColor = grdArrangmentDetailsReport.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdArrangmentDetailsReport.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdArrangmentDetailsReport.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdArrangmentDetailsReport.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            grdArrangmentDetailsReport.RenderControl(hw);

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
    public void ShowHide()
    {
        if (chkboxForAllCourse.Checked == true)
        {
            CourseRow.Visible = false;
            drpCourse.SelectedIndex = 0;
            drpSemesterYear.DataSource = "";
            drpSemesterYear.DataBind();
            txtDate.Text = "";
            drpLecture.DataSource = "";
            drpLecture.DataBind();
            drpFacultyName.DataSource = "";
            drpFacultyName.DataBind();
        }
        else
        {
            CourseRow.Visible = true;
            txtDate.Text = "";
            drpLecture.DataSource = "";
            drpLecture.DataBind();
            drpFacultyName.DataSource = "";
            drpFacultyName.DataBind();
        }
    }
    protected void chkboxForAllCourse_CheckedChanged(object sender, EventArgs e)
    {
        ShowHide();
    }
    protected void txtDateFrom_TextChanged(object sender, EventArgs e)
    {
        if (txtDateTo.Text != "")
        {
            BindReportGrid1();
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader5", "$('[id$=pnlReport]').show(); ", true);
    }
    protected void txtDateTo_TextChanged(object sender, EventArgs e)
    {
        if (txtDateFrom.Text != "")
        {
            BindReportGrid1();
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "fader6", "$('[id$=pnlReport]').show(); ", true);
    }
    //protected void chkPrincipal_CheckedChanged(object sender, EventArgs e)
    //{

    //}
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
       bindDrpCourseList();  
    }
    private void BindFacultWhoApply()
    {
        SqlCommand cmd = new SqlCommand("Sp_FacultyCollegeWiseFromTimeTable", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlFaculty.DataSource = dt;
        ddlFaculty.DataTextField = "Name";
        ddlFaculty.DataValueField = "Code";
        ddlFaculty.DataBind();
    
    }
    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        hfAcademic.Value = dt1.Rows[0][0].ToString();
        
    }
    public void FindMobNoAndSendSMS(String ToFacultyCode, String Status, String LectureWithDate)
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd2 = new SqlCommand("proc_GetPhoneNoOfEmployee", con);
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.Add("@FacultyCode", ToFacultyCode);
        string MobileNo = cmd2.ExecuteScalar().ToString();
       // MobileNo = "9015762885";
        con.Close();
        if (!string.IsNullOrEmpty(MobileNo))
        {
            SMS(MobileNo, "Lecture ( " + LectureWithDate + " ) Arrangement Request " + Status + " by " + Session["Fulname"].ToString() + " ");
        }
    }
    protected void chkLab_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLab.Checked == true)
        {
            BindLabPracticalFaculty();
        }
        else
        {
            drpCourse.Enabled = true;
            drpSemesterYear.Enabled = true;
            bindDrpCourseList();
            BindFaculty();
        }
    }
    
}