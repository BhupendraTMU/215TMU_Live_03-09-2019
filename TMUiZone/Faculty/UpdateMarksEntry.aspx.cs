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

using System.Text;

public partial class Faculty_UpdateMarksEntry : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                bindDrpCourseList();   
                bindAcademicYear();

     
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleFalseTrue(false, false, false, false);        
    }
    public void bindAcademicYear()
    {
        try
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
        catch
        {
        }
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleFalseTrue(true, false, false, false);
        bindDrpSemesterList();        
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourse_RoleMatrix", con);
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
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleFalseTrue(true, false, false, false);
    }
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemester_Role", con);//proc_GetSemesterFromCourseWiseFaculty_Role comment on 27-12-2017
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        bindgrid();
        VisibleFalseTrue(true, false, false, false);       
    }
    public void bindgrid()
    {
        try
        {
            VisibleFalseTrue(true, false, false, false);
            SqlCommand cmd = new SqlCommand("sp_GetInternalMarksDataForApprovalByHOD", con); //coment on usp_GetMarkEntrytable1 27-12-2017 by ashu
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            grdmarktable.DataSource = dt;
            grdmarktable.DataBind();            
            if (dt.Rows.Count > 0)
            {

            }
        }
        catch
        {
        }
    }
    protected void btntblmarksshow_Click(object sender, EventArgs e)
    {
        hf_Status.Value = "";
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow grow = btn.NamingContainer as GridViewRow;
            HiddenField hfDocumentNo = (HiddenField)grow.FindControl("hfDocumentNo");
            HiddenField hfStatus = (HiddenField)grow.FindControl("hfStatus");
            hf_DocumentNo.Value = hfDocumentNo.Value;
            hf_Status.Value = hfStatus.Value;
            if (hfStatus.Value == "2")
            {
                btnSubmit.Text = "Approve"; brnReject.Text = "Reject";
                btnSubmit.Visible = true; brnReject.Visible = true;
            }

            GetStudentMarks(hfDocumentNo.Value);

        }
        catch
        {
        }     
    }
    public void GetStudentMarks(string DocumentNo)  // 
    {
        SqlCommand cmd2 = new SqlCommand("sp_GetInternalMarksDetailsDocumentWise", con);
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.AddWithValue("@DocumentNo", DocumentNo);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        con.Open();
        da2.Fill(dt2);
        con.Close();
        grdViewmarksEntrySubmit.DataSource = dt2;
        grdViewmarksEntrySubmit.DataBind();
        if (hf_Status.Value == "2" || hf_Status.Value == "3" || hf_Status.Value == "5") { VisibleFalseTrue(false, true, true, true); }
        else { VisibleFalseTrue(false, true, false, false); }//grdViewmarksEntrySubmit.Columns[grdViewmarksEntrySubmit.Columns.Count-1].Visible = false; 
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        hf_Status.Value = "4";
        lblError.Text = "";
        Approve_Reject();
        if (lblError.Text == "") { bindgrid(); ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Send Succeccsfully');", true); }
        else { ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Something went wrong....');", true); }
    }
    protected void brnReject_Click(object sender, EventArgs e)
    {
        hf_Status.Value = "3";
        lblError.Text = "";
        Approve_Reject();
        if (lblError.Text == "") { bindgrid(); ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Rejected Succeccsfully');", true); }
        else { ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Something went wrong....');", true); }
    }
    public void Approve_Reject()
    {        
        DataTable dt = new DataTable(); dt.Columns.Add("DocumentNo", typeof(string)); dt.Columns.Add("LineNo", typeof(int)); dt.Columns.Add("Status", typeof(int));
            dt.Columns.Add("Remarks", typeof(string));
        SqlCommand cmd;
        try
        {

            foreach (GridViewRow row in grdViewmarksEntrySubmit.Rows)
            {
                DataRow dr = dt.NewRow();
                CheckBox chek = (CheckBox)row.FindControl("SChkD");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                HiddenField hfLineNo = (HiddenField)row.FindControl("hfLineNo");
                if (chek.Checked == true && chek.Enabled==true)
                {
                    dr["DocumentNo"] = hf_DocumentNo.Value;
                    dr["LineNo"] = Convert.ToInt64(hfLineNo.Value);
                    dr["Status"] = Convert.ToInt16(hf_Status.Value);
                    dr["Remarks"] = txtRemarks.Text;
                    dt.Rows.Add(dr);
                }
            }

            if ((dt.Rows.Count >= 1 & hf_Status.Value == "3") || (dt.Rows.Count == grdViewmarksEntrySubmit.Rows.Count & hf_Status.Value == "4"))
            {
                cmd = new SqlCommand("[Sp_MarksEntryupdatedByHodPrincipal]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UpdateMarkTable", dt);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();                
            }
            else
            {
                if (hf_Status.Value == "3") { lblError.Text = "There is no any record for Rejection"; }
                if (hf_Status.Value == "4") { lblError.Text = "Select All Records."; }
            }
        }
        catch
        {
            lblError.Text = "Error..!";
        }
        
    }

    public void VisibleFalseTrue(bool gMarktable,bool gApprovRej,bool bReject, bool bSubmit )
    {
        grdmarktable.Visible = gMarktable;
        grdViewmarksEntrySubmit.Visible = gApprovRej;
        brnReject.Visible = bReject;
        btnSubmit.Visible = bSubmit;        
        
    }
    
    
    
    
}