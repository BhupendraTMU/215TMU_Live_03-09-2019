using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Faculty_AwardList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindAcademicYear(); bindDrpCourseList(); bindDrpSemesterList();
                bindSubject();

                bindSectionList();
                bindGroupList();
                bindApprovalList();

                Bindfaculty();
                bindDrpSemesterList();
                if (Session["UserRole"].ToString() != "HOD" && Session["UserRole"].ToString() != "Pricipal")
                {
                    faculty.Visible = false;
                }
                else { faculty.Visible = true; }
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void bindApprovalList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[Pro_GetListForReportApproval]", con); //coment on usp_GetMarkEntrytable1 27-12-2017 by ashu sp_GetInternalMarksDataForApprovalByHOD
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());

            cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);
            if (Session["UserRole"].ToString() != "HOD" && Session["UserRole"].ToString() != "Pricipal")
            {

                if (ddlSubject.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Subject is neccesary');", true);
                    return;
                }

                else
                {

                    cmd.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue);
                }
            }
            else
            {
                if (ddlSubject.SelectedIndex > 0)
                { cmd.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue); }
                else { cmd.Parameters.AddWithValue("@Subject", ""); }
            }
            if (drpFaculty.SelectedIndex > 0)
            { cmd.Parameters.AddWithValue("@facultyCode", drpFaculty.SelectedValue); }
            else { cmd.Parameters.AddWithValue("@facultyCode", ""); }
            cmd.Parameters.AddWithValue("@UserRole", Session["UserRole"].ToString());
            if (rdInternal.Checked == true)
            {
                cmd.Parameters.AddWithValue("@rdbExam", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@rdbExam", 2);
            }
            cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);

            cmd.Parameters.AddWithValue("@StudentGroup", ddlGroup.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            grdViewAwardlistApproval.DataSource = dt;
            grdViewAwardlistApproval.DataBind();
            if (dt.Rows.Count > 0)
            {


                if (Session["UserRole"].ToString() == "HOD")
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "Statust='Approved by Principal'";
                    if (dv.Count == dt.Rows.Count)
                    {
                        tblAR.Visible = false;
                    }
                    else { tblAR.Visible = true; }

                }

                else if (Session["UserRole"].ToString() == "Pricipal")
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "Statust='Approved by Principal'";
                    if (dv.Count == dt.Rows.Count)
                    {
                        tblAR.Visible = false;
                    }
                    else { tblAR.Visible = true; }
                }
                else
                {
                    tblAR.Visible = false;
                }
            }
            else
            {
                tblAR.Visible = false;
            }
        }
        catch
        {
        }
    }


    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();
        // bindReport();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
        // bindReport();
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        bindDrpCourseList();
        bindDrpSemesterList();
        bindSubject();

        //bindReport();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        bindApprovalList();
        // bindReport();

    }
    public void bindDrpSemesterList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1000000;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt);
            con.Close();
            drpSemester.DataSource = dt;
            drpSemester.DataTextField = "Details";
            drpSemester.DataValueField = "No_";
            drpSemester.DataBind();
            if (Session["drpSemester"].ToString() != null)
            {
                drpSemester.SelectedValue = Session["drpSemester"].ToString();
            }
            else { drpSemester.SelectedValue = "--Semester--"; }
        }catch(Exception ex){}
    }
    public void bindSubject()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getSubjrctCodeFacultyWise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());
            if (rdInternal.Checked == true)
            {
                cmd.Parameters.Add("@ExamType", "Internal");
            }
            else
            {
                cmd.Parameters.Add("@ExamType", "External");
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlSubject.DataSource = dt;

            ddlSubject.DataTextField = "Description";
            ddlSubject.DataValueField = "Subject Code";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "--Course--");
            if (Session["Subject"].ToString() != null)
            {
                ddlSubject.SelectedValue = Session["Subject"].ToString();
            }
            else { ddlSubject.SelectedValue = "--Course--"; }
        }
        catch (Exception ex) { }
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
            drpAcademicYear.DataSource = dt1;
            drpAcademicYear.DataTextField = "Details";
            drpAcademicYear.DataValueField = "No_";
            drpAcademicYear.DataBind();
        }
        catch
        {
        }
    }
    public void bindDrpCourseList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", Session["uid"].ToString());
            cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@Academic",drpAcademicYear.SelectedItem.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            drpCourse.DataSource = dt;
            drpCourse.DataTextField = "Details";
            drpCourse.DataValueField = "No_";
            drpCourse.DataBind();
            Session["UserRole"] = dt.Rows[0]["UserRole"].ToString();
            if (Session["drpCourse"].ToString() != null)
            {
                drpCourse.SelectedValue = Session["drpCourse"].ToString();
            }
            else { drpCourse.SelectedValue = "--Course--"; }
           
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["UserRole"].ToString() == "HOD" || dt.Rows[0]["UserRole"].ToString() == "Pricipal")
                {
                    // faculty.Visible = true;

                }
                else
                {
                    // faculty.Visible = false;
                }
            }
        }
        catch(Exception ex) { }
    }


    public void Bindfaculty()
    {
        SqlCommand cmd = new SqlCommand("Pro_GetFaculty", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
        cmd.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue);
        cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
        cmd.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        drpFaculty.DataSource = dt;
        drpFaculty.DataTextField = "Name";
        drpFaculty.DataValueField = "Code";
        drpFaculty.DataBind();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindfaculty();
        bindSectionList();


        bindGroupList();
        // bindReport();
    }
    public void bindSectionList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_RoleNew", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Subject", ddlSubject.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();
    }
    public void bindGroupList()
    {
        SqlCommand cmd = new SqlCommand("sp_GetGroup_RoleNew", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@SubjectCode", ddlSubject.SelectedValue);
        cmd.Parameters.Add("@SectionCode", drpSection.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "Details";
        ddlGroup.DataValueField = "No_";
        ddlGroup.DataBind();
    }
    public void bindReport()
    {
        SqlCommand cmd = new SqlCommand("Pro_GetAwardList", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Acadyear", Session["AcademicYear"].ToString());
        cmd.Parameters.AddWithValue("@Subject", Session["Subject"].ToString());
        if (Session["UserRole"].ToString() == "HOD" || Session["UserRole"].ToString() == "Pricipal")
        {
            cmd.Parameters.AddWithValue("@facultyCode", Session["FacultyCode"].ToString());
        }
        else
        {
            cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
        }
        cmd.Parameters.AddWithValue("@Course", Session["drpCourse"].ToString());
        cmd.Parameters.AddWithValue("@Sem", Session["drpSemester"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            lblReportStatus.Text = dt.Rows[0]["ReportStatus"].ToString();
            if (Session["UserRole"].ToString() == "HOD")
            {
                btnSendForApproval.Visible = false;
                if (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 2 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 3)
                {
                    // tblAR.Visible = false;
                }
                else
                {
                    // tblAR.Visible = true;
                }


            }
            else if (Session["UserRole"].ToString() == "Pricipal")
            {
                btnSendForApproval.Visible = false;
                if (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 4 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 5)
                {
                    tblAR.Visible = false;
                }
                else
                {
                    tblAR.Visible = true;
                }
            }

            else
            {
                if (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0)
                {
                    btnSendForApproval.Visible = true;
                    tblAR.Visible = false;
                }
                else
                {
                    btnSendForApproval.Visible = false;
                    tblAR.Visible = false;
                }
            }
            lblmsg.Visible = false;
            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            if (dt.Rows[0]["Classification"].ToString() == "THEORY")
                {
                    if ((Session["UserRole"].ToString() == "HOD" || Session["UserRole"].ToString() == "Pricipal") && Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0)
                    {
                        //     tblAR.Visible = false;
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                    }
                    else if (Session["UserRole"].ToString() == "HOD")
                    {
                        if (Convert.ToInt32(dt.Rows[0]["Award List Status"])>=1)
                        {
                            // tblAR.Visible = true;
                            ReportViewer1.Visible = true;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                        }
                        else
                        {
                               //tblAR.Visible = false;
                            ReportViewer1.Visible = false;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                        }
                    }
                    else if (Session["UserRole"].ToString() == "Pricipal")
                    {
                        if (Convert.ToInt32(dt.Rows[0]["Award List Status"])>= 2)
                        {
                            ReportViewer1.Visible = true;
                             //tblAR.Visible = true;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                        }
                        else
                        {
                            ReportViewer1.Visible = false;
                            // tblAR.Visible = false;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                        }
                    }
                    else
                    {
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/AwardlistReport.rdlc");
                    }


                }
                else
                {
                    if ((Session["UserRole"].ToString() == "HOD" || Session["UserRole"].ToString() == "Pricipal") && Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0)
                    {
                        ReportViewer1.Visible = false;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/InternalPracticalAwardlistReport.rdlc");
                        lblmsg.Visible = true;
                        lblmsg.Text = "Report is either Rejected or Not Submitted.....";
                      
                    }
                    else if (Session["UserRole"].ToString() == "HOD" && (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 3 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 5 ))
                    {
                        ReportViewer1.Visible = false;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/InternalPracticalAwardlistReport.rdlc");
                        lblmsg.Visible = true;
                        lblmsg.Text = "Report is either Rejected or Not Submitted.....";
                    }
                    else if (Session["UserRole"].ToString() == "Pricipal" && (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0 ||
                        Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 3 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 5))
                    {
                        ReportViewer1.Visible = false;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/InternalPracticalAwardlistReport.rdlc");
                        lblmsg.Visible = true;
                        lblmsg.Text = "Report is either Rejected or Not Submitted.....";
                    }
                    else
                    {
                        ReportViewer1.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/InternalPracticalAwardlistReport.rdlc");
                        lblmsg.Visible = false;
                    }

                }
                ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'No data Found.. ');", true);
                lblmsg.Visible = true;
                lblmsg.Text = "No Data Found.....";
                ReportViewer1.Visible = false;
            }
        }

    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Update_ReportApproval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue);
            cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);
            cmd.Parameters.AddWithValue("@ApprovalStatus", 1);
            cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
            cmd.Parameters.AddWithValue("@Studentgroup", ddlGroup.SelectedValue);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Sent Successfully For Approval');", true);

            }
        }
        catch (Exception ex)
        {
        }

    }
    protected void drpFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindApprovalList();
    }
    protected void SChlAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkrow1 = (CheckBox)grdViewAwardlistApproval.HeaderRow.FindControl("SChlAll");
        if (chkrow1.Checked)
        {

            foreach (GridViewRow row in grdViewAwardlistApproval.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("SChkD");
                    if (chkrow.Enabled == true)
                    {
                        chkrow.Checked = true;
                    }
                }
            }
        }
        else
        {
            foreach (GridViewRow row in grdViewAwardlistApproval.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("SChkD");
                    if (chkrow.Enabled == true)
                    {
                        chkrow.Checked = false;
                    }
                }
            }
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            foreach (GridViewRow row in grdViewAwardlistApproval.Rows)
            {
                HiddenField HfStaffcode1 = (HiddenField)row.FindControl("HfStaffcode");
                HiddenField HfSubjectCode = (HiddenField)row.FindControl("HfSubjectCode");
                CheckBox SChkD = (CheckBox)row.FindControl("SChkD");
                if (SChkD.Checked == true)
                {
                    SqlCommand cmd = new SqlCommand("Update_ReportApproval", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@Subject", HfSubjectCode.Value);
                    cmd.Parameters.AddWithValue("@facultyCode", HfStaffcode1.Value);
                    cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
                    cmd.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);
                   
                    if (Session["UserRole"].ToString() == "HOD")
                    {
                        cmd.Parameters.AddWithValue("@ApprovalStatus", 2);
                    }
                    if (Session["UserRole"].ToString() == "Pricipal")
                    {
                        cmd.Parameters.AddWithValue("@ApprovalStatus", 4);
                    }
                    cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
                    cmd.Parameters.AddWithValue("@StudentGroup", ddlGroup.SelectedValue);
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Approved Successfully.. ');", true);
                bindApprovalList();
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Nothing is selected For Approval.. ');", true);
                return;
            }



        }
        catch (Exception ex)
        {
        }

    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            foreach (GridViewRow row in grdViewAwardlistApproval.Rows)
            {
                HiddenField HfStaffcode1 = (HiddenField)row.FindControl("HfStaffcode");
                HiddenField HfSubjectCode = (HiddenField)row.FindControl("HfSubjectCode");
                CheckBox SChkD = (CheckBox)row.FindControl("SChkD");
                if (SChkD.Checked == true)
                {
                    SqlCommand cmd = new SqlCommand("Update_ReportApproval", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
                    cmd.Parameters.AddWithValue("@Subject", HfSubjectCode.Value);
                    cmd.Parameters.AddWithValue("@facultyCode", HfStaffcode1.Value);
                    cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
                    cmd.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);
                    if (Session["UserRole"].ToString() == "HOD")
                    {
                        cmd.Parameters.AddWithValue("@ApprovalStatus", 3);
                    }
                    if (Session["UserRole"].ToString() == "Pricipal")
                    {
                        cmd.Parameters.AddWithValue("@ApprovalStatus", 5);
                    }
                    cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
                    cmd.Parameters.AddWithValue("@Studentgroup", ddlGroup.SelectedValue);
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Rejected Successfully.. ');", true);
                bindApprovalList();
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Nothing is selected For Rejection.. ');", true);
                return;
            }



        }
        catch (Exception ex)
        {
        }

    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);
        HiddenField HfStaffcode = (HiddenField)row.FindControl("HfStaffcode");
        HiddenField HfSubjectCode = (HiddenField)row.FindControl("HfSubjectCode");

        Session["FacultyCode"] = HfStaffcode.Value;
        Session["AcademicYear"] = drpAcademicYear.SelectedValue;
        Session["Subject"] = HfSubjectCode.Value;
        Session["drpCourse"] = drpCourse.SelectedValue;
        Session["drpSemester"] = drpSemester.SelectedValue;
        Session["Section"] = drpSection.SelectedValue;
        Session["Group"] = ddlGroup.SelectedValue;
        if (rdInternal.Checked == true)
        {
            if (Session["UserRole"].ToString() != "HOD" && Session["UserRole"].ToString() != "Pricipal")
            {
                Session["examType1"] = 1;
            }
            else
            {
                Session["Examtype"] = 1;
            }
        }
        else
        {
            if (Session["UserRole"].ToString() != "HOD" && Session["UserRole"].ToString() != "Pricipal")
            {
                Session["examType1"] = 2;
            }
            else
            {
                Session["Examtype"] = 2;
            }

        }
        if (Session["UserRole"].ToString() != "HOD" && Session["UserRole"].ToString() != "Pricipal")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "OpenNewWindow()", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "OpenNewWindow1()", true);

        }

        //Response.Redirect("MainReport.aspx");
        //ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "OpenNewWindow()", true);
        //bindReport();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGroupList();
    }

    protected void grdViewAwardlistApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Session["UserRole"].ToString() == "HOD")
                {
                    Label grdAwardStatus = (Label)e.Row.FindControl("grdAwardStatus");
                    if (grdAwardStatus.Text == "Approved by HOD")
                    {
                        tblAR.Visible = false;
                    }
                    else
                    {
                        tblAR.Visible = true;
                    }
                }
                else if (Session["UserRole"].ToString() == "Pricipal")
                {
                    Label grdAwardStatus = (Label)e.Row.FindControl("grdAwardStatus");
                    if (grdAwardStatus.Text == "Approved by Principal")
                    {
                        tblAR.Visible = false;
                    }
                    else
                    {
                        tblAR.Visible = true;
                    }
                }
                else
                {
                    tblAR.Visible = false;
                }
            }
        }
        catch (Exception ex) { }

    }
}