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

public partial class Faculty_BlankAwardList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtDateTo.Attributes.Add("autocomplete", "off"); 
                bindAcademicYear(); bindDrpCourseList(); bindSectionList();

            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();
        bindSectionList();
        //bindReport();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
        bindSectionList();
        //bindReport();
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();
        bindDrpCourseList();
       // bindReport();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        bindReport();

    }
    public void bindDrpSemesterList()
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
    }
    public void bindSubject()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getSubjrctCodeFacultyWise_Lab", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlSubject.DataSource = dt;

            ddlSubject.DataTextField = "Description";
            ddlSubject.DataValueField = "Subject Code";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "--Subject--");

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
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["UserRole"].ToString() == "HOD" || dt.Rows[0]["UserRole"].ToString() == "Pricipal")
            {
                faculty.Visible = false;
            }
            else
            {
                faculty.Visible = false;
            }
        }
    }
    public void bindSectionList()
    {
        try
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
        catch (Exception ex) { }
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
        //bindReport();
        bindSectionList();
    }
    public void bindReport()
    {
        SqlCommand cmd = new SqlCommand("[Pro_GetAwardList_Blank1]", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
        cmd.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue);
        if (Session["UserRole"].ToString() == "HOD" || Session["UserRole"].ToString() == "Pricipal")
        {
            cmd.Parameters.AddWithValue("@facultyCode", drpFaculty.SelectedValue);
        }
        else
        {
            cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
        }
        cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
        cmd.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);

        if (rdInternal.Checked == true)
        {
            cmd.Parameters.AddWithValue("@ExamType1", 1);
        }
        else
        {
            cmd.Parameters.AddWithValue("@ExamType1", 2);
        }
        if (drpSection.SelectedIndex > 0)
        { cmd.Parameters.AddWithValue("@section", drpSection.SelectedValue); }
        else { cmd.Parameters.AddWithValue("@section", ""); }
        if (txtDateFrom.Text != "")
        {
            cmd.Parameters.Add("@FromDate", Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy/MM/dd"));
        }
        else
        {
            cmd.Parameters.Add("@FromDate", "");
        }
        if (txtDateTo.Text != "")
        {
            cmd.Parameters.Add("@ToDate", Convert.ToDateTime(txtDateTo.Text).ToString("yyyy/MM/dd"));
        }
        else
        {
            cmd.Parameters.Add("@ToDate", "");
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ReportViewer1.Visible = true;
         //   tblAR.Visible = true;
            lblmsg.Visible = false;

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/BlankAwardlistReport.rdlc");
            //lblReportStatus.Text = dt.Rows[0]["ReportStatus"].ToString();
            //if (Session["UserRole"].ToString() == "HOD")
            //{
               
            //    if (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 2 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 3)
            //    {
            //        tblAR.Visible = false;
            //    }
            //    else
            //    {
            //        tblAR.Visible = true;
            //    }


            //}
            //else if (Session["UserRole"].ToString() == "Pricipal")
            //{
               
            //    if (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 4 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 5)
            //    {
            //        tblAR.Visible = false;
            //    }
            //    else
            //    {
            //        tblAR.Visible = true;
            //    }
            //}

            //else
            //{
            //    if (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0)
            //    {
                   
            //        tblAR.Visible = false;
            //    }
            //    else
            //    {
                   
            //        tblAR.Visible = false;
            //    }
            //}
            //lblmsg.Visible = false;
            //ReportViewer1.Visible = true;
            //ReportViewer1.ProcessingMode = ProcessingMode.Local;
            //if (dt.Rows[0]["Classification"].ToString() == "THEORY")
            //{
            //    if ((Session["UserRole"].ToString() == "HOD" || Session["UserRole"].ToString() == "Pricipal") && Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0)
            //    {
            //        tblAR.Visible = false;
            //        ReportViewer1.Visible = false;
            //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/BlankAwardlistReport.rdlc");

            //    }
            //    else if (Session["UserRole"].ToString() == "HOD")
            //    {
            //        if (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 1)
            //        {
            //            tblAR.Visible = true;
            //            ReportViewer1.Visible = true;
                        
            //                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/BlankAwardlistReport.rdlc");
                       
            //        }
            //        else
            //        {
            //            tblAR.Visible = false;
            //            ReportViewer1.Visible = false;
                       
            //                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/BlankAwardlistReport.rdlc");
                        
            //        }
            //    }
            //    else if (Session["UserRole"].ToString() == "Pricipal")
            //    {
            //        if (Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 2)
            //        {
            //            ReportViewer1.Visible = true;
            //            tblAR.Visible = true;
                        
            //                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/BlankAwardlistReport.rdlc");
                       
            //        }
            //        else
            //        {
            //            ReportViewer1.Visible = false;
            //            tblAR.Visible = false;
                        
            //                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/BlankAwardlistReport.rdlc");
                        
            //        }
            //    }
            //    else
            //    {
            //        ReportViewer1.Visible = true;
                    
            //            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/BlankAwardlistReport.rdlc");
                    
            //    }


            //}
            //else
            //{
            //    if ((Session["UserRole"].ToString() == "HOD" || Session["UserRole"].ToString() == "Pricipal") && Convert.ToInt32(dt.Rows[0]["Award List Status"]) == 0)
            //    {
            //        ReportViewer1.Visible = false;
                   
            //            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/BlankInternalPracticalAwardlistReport.rdlc");
                    

            //    }
            //    else if (Session["UserRole"].ToString() == "HOD" && Convert.ToInt32(dt.Rows[0]["Award List Status"]) != 1)
            //    {
            //        ReportViewer1.Visible = false;
                    
            //            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/BlankInternalPracticalAwardlistReport.rdlc");
                   
            //    }
            //    else if (Session["UserRole"].ToString() == "Pricipal" && (Convert.ToInt32(dt.Rows[0]["Award List Status"]) != 2 || Convert.ToInt32(dt.Rows[0]["Award List Status"]) != 3))
            //    {
            //        ReportViewer1.Visible = false;
                    
            //            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/BlankInternalPracticalAwardlistReport.rdlc");
                    
            //    }
            //    else
            //    {
            //        ReportViewer1.Visible = true;
                    
            //            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/BlankInternalPracticalAwardlistReport.rdlc");
                    
            //    }

            //}
            ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "No Data Found.....";
            ReportViewer1.Visible = false;
        }
    }
  
    protected void drpFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Update_ReportApproval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue);
            cmd.Parameters.AddWithValue("@facultyCode", drpFaculty.SelectedValue);
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
            con.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Approved Successfully.. ');", true);

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
            SqlCommand cmd = new SqlCommand("Update_ReportApproval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue);
            cmd.Parameters.AddWithValue("@facultyCode", drpFaculty.SelectedValue);
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

            con.Open();
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Rejected Successfully.. ');", true);

            }
        }
        catch (Exception ex)
        {
        }

    }

    protected void rdInternal_CheckedChanged(object sender, EventArgs e)
    {
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
        tddate.Visible = false;
        //bindReport();
    }
    protected void rdExternal_CheckedChanged(object sender, EventArgs e)
    {
        tddate.Visible = true;
       // bindReport();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
       // bindReport();
    }
}