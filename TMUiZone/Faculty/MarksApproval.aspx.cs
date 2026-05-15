
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
using System.Net;
using System.Text;
using WebReference;

public partial class Faculty_MarksApproval : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindAcademicYear();
                bindDrpCourseList();
               
                bindSubject();
                bindSectionList();
                bindGroupList();
                bindApprovalList();
                panelmarksentry.Visible = false;
                PanelReport.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
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
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleFalseTrue(false, false, false, false);
        bindDrpCourseList();
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
        bindSubject();
        //  bindApprovalList();
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        Session["UserRole"] = dt.Rows[0]["UserRole"].ToString();
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleFalseTrue(true, false, false, false);
        bindSubject();
        //  if (Rblist.SelectedValue == "2") { bindApprovalList(); }

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
        BtnPrint.Visible = false;
        lblError.Text = "";
        bindgrid();

        VisibleFalseTrue(true, false, false, false);
        if (Rblist.SelectedValue == "2")
        {
            if (chkPiv.Checked == true)
            {
                bindApprovalList(); onetime.Visible = true;
            }
            else { bindgrid(); VisibleFalseTrue(true, false, false, false); }
        }
        else { onetime.Visible = false; }
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGroupList();
    }
    public void bindgrid()
    {
        try
        {
            VisibleFalseTrue(true, false, false, false);
            SqlCommand cmd = new SqlCommand("sp_GetInternalMarksDataForApprovalByHOD_PNC1", con); //coment on usp_GetMarkEntrytable1 27-12-2017 by ashu sp_GetInternalMarksDataForApprovalByHOD
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcadmicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.AddWithValue("@facultyCode", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
            if (ddlSubject.SelectedIndex > 0)
            { cmd.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue); }
            else { cmd.Parameters.AddWithValue("@Subject", ""); }

            cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
            cmd.Parameters.AddWithValue("@Studentgroup", ddlGroup.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            if (con.State == ConnectionState.Open) { con.Close(); }
            con.Open();
            da.Fill(ds);
            con.Close();
            grdmarktable.DataSource = ds.Tables[0];
            grdmarktable.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Rblist.SelectedValue == "2")
                {
                    if (chkPiv.Checked == true)
                    {
                        panelmarksentry.Visible = false;
                        PanelReport.Visible = true;
                    }
                    else
                    {
                        panelmarksentry.Visible = true;
                        PanelReport.Visible = false;
                    }
                }
                else
                {
                    panelmarksentry.Visible = true;
                    PanelReport.Visible = false;

                    if (Convert.ToInt32(ds.Tables[1].Rows[0]["ExamCountinHeader"]) == 0)
                    {

                        btnview.Visible = false;

                    }
                    if (Convert.ToInt32(ds.Tables[1].Rows[0]["ExamCountinHeader"]) > 0)
                    {
                        if (Convert.ToInt32(ds.Tables[1].Rows[0]["ExamCountinHeader"]) == Convert.ToInt32(ds.Tables[1].Rows[0]["ExamCountinExGroup"]))
                        {

                            btnview.Visible = true;

                        }
                        else
                        {

                            btnview.Visible = false;
                        }
                    }
                    grdmarktable.DataSource = ds.Tables[0];
                    grdmarktable.DataBind();


                }



            }
            else
            {
                btnview.Visible = false;
              
                bindApprovalList();
                // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'No data Found....');", true);
            }

        }

        catch(Exception ex)
        {
            btnview.Visible = false;
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
            HiddenField HFExamType = (HiddenField)grow.FindControl("HFExamType");
            Label grdlblDocumentNo = (Label)grow.FindControl("grdlblDocumentNo");
            HiddenField hfStudentNo = (HiddenField)grow.FindControl("hfStudentNo");
            HiddenField hfSubject = (HiddenField)grow.FindControl("hfSubject");
            hf_DocumentNo.Value = hfDocumentNo.Value;
            hf_Status.Value = hfStatus.Value;
            hf_ExamType.Value = HFExamType.Value;
            hf_groupno.Value = grdlblDocumentNo.Text;
            Hf_subjectCode.Value = hfSubject.Value;
            if (hfStatus.Value == "2")
            {
                btnSubmit.Text = "Approve"; brnReject.Text = "Reject";
                btnSubmit.Visible = true; brnReject.Visible = true;
                BtnPrint.Visible = true;
            }

            GetStudentMarks(hfDocumentNo.Value, hf_ExamType.Value);
            if (hf_Status.Value.ToString() == "4" || hf_Status.Value.ToString() == "1" || hf_Status.Value.ToString() == "6" || hf_Status.Value.ToString() == "7")
            { btnSubmit.Visible = false; brnReject.Visible = false; }
            else
            { btnSubmit.Visible = true; brnReject.Visible = true; }

        }
        catch
        {
        }
    }
    public void GetStudentMarks(string DocumentNo, string ExamType)  // 
    {
        SqlCommand cmd2 = new SqlCommand("sp_GetInternalMarksDetailsDocumentWise1", con);
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.AddWithValue("@DocumentNo", DocumentNo);
        cmd2.Parameters.AddWithValue("@ExamType", ExamType);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        con.Open();
        da2.Fill(dt2);
        if (dt2.Rows.Count > 0) { BtnPrint.Visible = true; } else { BtnPrint.Visible = false; }
        con.Close();
        grdViewmarksEntrySubmit.DataSource = dt2;
        grdViewmarksEntrySubmit.DataBind();
        if (hf_Status.Value == "2" || hf_Status.Value == "5" || hf_Status.Value == "3") { VisibleFalseTrue(false, true, true, true); }
        else { VisibleFalseTrue(false, true, false, false); }//grdViewmarksEntrySubmit.Columns[grdViewmarksEntrySubmit.Columns.Count-1].Visible = false; 
    }
    
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string UpdateUFMdata(string documentNo, string StudentNo, string Obtainedmarks, string Remarks, string Method, int UFMStatus)
    {
        try
        {
            string result;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
            SqlCommand cmd;
            cmd = new SqlCommand("UpdateMarksByHod", con);//Sp_MarksEntryupdatedByHodPrincipal
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@documentNo", documentNo);
            cmd.Parameters.AddWithValue("@StudentNo", StudentNo);
            cmd.Parameters.AddWithValue("@Obtainedmarks", Obtainedmarks);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Method", Method);
            cmd.Parameters.AddWithValue("@UFMStatus", UFMStatus);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                DataTable dtNAV = new DataTable();
                SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
                cmdNAV.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
                daNAV.Fill(dtNAV);
                VoucherPosting nvp = new VoucherPosting();
                nvp.UseDefaultCredentials = true;
                nvp.Url = dtNAV.Rows[0]["URL"].ToString();
                nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
                result = nvp.InternalTotalMarks(dt.Rows[0]["Subject Exam Craiteria"].ToString().Trim(), dt.Rows[0]["Global Dimension 1 Code"].ToString().Trim(), dt.Rows[0]["Academic Year"].ToString(), dt.Rows[0]["Subject Code"].ToString(), dt.Rows[0]["Course"].ToString(), StudentNo);

            }
            con.Close();

            return "Success";
           
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string UpdateUFMdataHOD(string documentNo, string StudentNo, string Obtainedmarks, string Remarks, string Method, int UFMStatus, string DocumentNo)
    {
        try
        {
            string result;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
            SqlCommand cmd;
            cmd = new SqlCommand("UpdateMarksByHod", con);//Sp_MarksEntryupdatedByHodPrincipal
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@documentNo", DocumentNo);
            cmd.Parameters.AddWithValue("@StudentNo", StudentNo);
            cmd.Parameters.AddWithValue("@Obtainedmarks", Obtainedmarks);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Method", Method);
            cmd.Parameters.AddWithValue("@UFMStatus", UFMStatus);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                DataTable dtNAV = new DataTable();
                SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
                cmdNAV.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
                daNAV.Fill(dtNAV);
                VoucherPosting nvp = new VoucherPosting();
                nvp.UseDefaultCredentials = true;
                nvp.Url = dtNAV.Rows[0]["URL"].ToString();
                nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
                result = nvp.InternalTotalMarks(dt.Rows[0]["Subject Exam Craiteria"].ToString().Trim(), dt.Rows[0]["Global Dimension 1 Code"].ToString().Trim(), dt.Rows[0]["Academic Year"].ToString(), dt.Rows[0]["Subject Code"].ToString(), dt.Rows[0]["Course"].ToString(), StudentNo);

            }
            con.Close();

            return "Success";
           
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        hf_Status.Value = "4";
        lblError.Text = "";
        Approve_Reject();
        if (lblError.Text == "") { bindgrid(); ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Send Successfully');", true); BtnPrint.Visible = false; }
        else { ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Something went wrong....');", true); }
    }
    int approverejectflag = 0;
    protected void brnReject_Click(object sender, EventArgs e)
    {
        hf_Status.Value = "3";
        lblError.Text = "";

        Approve_Reject();
        if (approverejectflag == 1)
        {
            if (lblError.Text == "")
            {

                bindgrid();

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Rejected Successfully');", true);
                BtnPrint.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Something went wrong....');", true);
            }
        }
    }

    public void Approve_Reject()
    {
        int Applied = 0;
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
                if (chek.Checked == true)
                {
                    Applied++;
                }
                if (chek.Checked == true && chek.Enabled == true)
                {
                    dr["DocumentNo"] = hf_DocumentNo.Value;
                    dr["LineNo"] = Convert.ToInt64(hfLineNo.Value);
                    dr["Status"] = Convert.ToInt16(hf_Status.Value);

                    dr["Remarks"] = txtRemarks.Text;
                    if (txtRemarks.Text == "" && hf_Status.Value == "3")
                    {
                        dt.Clear();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Remarks is mandotary....');", true);
                        txtRemarks.Focus();
                        approverejectflag = 0;
                        return;
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        approverejectflag = 1;
                    }

                }

            }

            if ((dt.Rows.Count >= 1 & hf_Status.Value == "3") || (Applied == grdViewmarksEntrySubmit.Rows.Count & hf_Status.Value == "4"))
            {
                cmd = new SqlCommand("[Sp_MarksEntryupdatedByHodPrincipal1]", con);//Sp_MarksEntryupdatedByHodPrincipal
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UpdateMarkTable", dt);
                cmd.Parameters.AddWithValue("@ExamType", hf_ExamType.Value);
                cmd.CommandTimeout = 300;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
             

            }
            else
            {
                if (hf_Status.Value == "3")
                {
                    lblError.Text = "There is no any record for Rejection";
                }

                if (hf_Status.Value == "4")
                {
                    lblError.Text = "Select All Records.";
                }
            }
        }
        catch
        {
            lblError.Text = "Error..!";
        }

    }

    public void VisibleFalseTrue(bool gMarktable, bool gApprovRej, bool bReject, bool bSubmit)
    {
        grdmarktable.Visible = gMarktable;
        grdViewmarksEntrySubmit.Visible = gApprovRej;
        brnReject.Visible = bReject;
        btnSubmit.Visible = bSubmit;

    }





    protected void SChlAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkrow1 = (CheckBox)grdViewmarksEntrySubmit.HeaderRow.FindControl("SChlAll");
        if (chkrow1.Checked)
        {

            foreach (GridViewRow row in grdViewmarksEntrySubmit.Rows)
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
            foreach (GridViewRow row in grdViewmarksEntrySubmit.Rows)
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

    public void bindSubject()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("sp_getSubjrctCodeHODWise_marksapproval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Open();
            da.Fill(ds);
            con.Close();

            if (Rblist.SelectedValue == "1")
            {
                ddlSubject.DataSource = ds.Tables[0];
            }
            else { ddlSubject.DataSource = ds.Tables[1]; }
            ddlSubject.DataTextField = "Description";
            ddlSubject.DataValueField = "Subject Code";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "--Course--");
        }
        catch (Exception ex) { }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        GridViewRow grow = btn.NamingContainer as GridViewRow;
        Label grdlblMethod = (Label)grow.FindControl("grdlblMethod");
        Label lblfacultyCode = (Label)grow.FindControl("lblfacultyCode");
        Label lblSection = (Label)grow.FindControl("grdlblSection");
        Label lblSgroup = (Label)grow.FindControl("grdlblSGroup");
        HiddenField hfSubject = (HiddenField)grow.FindControl("hfSubject");
        Session["AcademicYear"] = drpAcademicYear.SelectedValue;
        Session["Subject"] = hfSubject.Value;
        Session["drpCourse"] = drpCourse.SelectedValue;
        Session["drpSemester"] = drpSemester.SelectedValue;
        Session["ExamMethod"] = grdlblMethod.Text;
        Session["FacultyCode1"] = lblfacultyCode.Text;
        Session["Section"] = lblSection.Text;
        Session["StudGroup"] = lblSgroup.Text;

        ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "OpenNewWindow()", true);

    }

    //public void getmethodcount()
    //{
    //    SqlCommand cmd2 = new SqlCommand("Pro_GetMethodCount", con);
    //    cmd2.CommandType = CommandType.StoredProcedure;
    //    cmd2.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
    //    cmd2.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
    //    cmd2.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);
    //    cmd2.Parameters.AddWithValue("@Subject",Hf_subjectCode.Value);

    //    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
    //    DataTable dt2 = new DataTable();
    //    con.Open();
    //    da2.Fill(dt2);
    //    if (dt2.Rows.Count > 0)
    //    {
    //        if (Convert.ToInt32(dt2.Rows[0]["ExamCountinHeader"]) == Convert.ToInt32(dt2.Rows[0]["ExamCountinExGroup"]))
    //        {
    //            try
    //            {
    //                string result = "";
    //                foreach (GridViewRow row in grdViewmarksEntrySubmit.Rows)
    //                {
    //                    string documentno = hf_groupno.Value;
    //                    string collegeCode = Session["GlobalDimension1Code"].ToString();
    //                    string academicyear = drpAcademicYear.SelectedValue;
    //                    string subjectcode = Hf_subjectCode.Value;
    //                    string coursecode = drpCourse.SelectedValue;
    //                    HiddenField hfStudentNo = (HiddenField)row.FindControl("hfStudentNo");

    //                    VoucherPosting nvp = new VoucherPosting();
    //                    nvp.UseDefaultCredentials = true;
    //                    nvp.Url = "http://172.0.10.149:7047/DynamicsNAV71/WS/TMU/Codeunit/VoucherPosting";
    //                    nvp.Credentials = new NetworkCredential("Administrator", "corp@1234");
    //                    result = nvp.InternalTotalMarks(documentno, collegeCode, academicyear, subjectcode, coursecode, hfStudentNo.Value);

    //                }
    //                con.Close();
    //                bindgrid();

    //                //if(result=="Ok")
    //                //{
    //                //     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Approved Successfully');", true);
    //                //    // bindgrid();
    //                //}

    //            }
    //            catch (Exception ex) { }
    //        }
    //        else { con.Close(); }
    //    }
    //    else { con.Close(); }
    //}

    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow grow = btn.NamingContainer as GridViewRow;

            Session["AcademicYear"] = drpAcademicYear.SelectedValue;
            Session["Subject"] = ddlSubject.SelectedValue;
            Session["drpCourse"] = drpCourse.SelectedValue;
            Session["drpSemester"] = drpSemester.SelectedValue;
            Session["UserRole"] = Session["UserRole"].ToString();
            Session["FacultyCode"] = Session["uid"].ToString();
            DataTable dtNAV = new DataTable();
            SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
            cmdNAV.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
            daNAV.Fill(dtNAV);
            VoucherPosting nvp = new VoucherPosting();
            nvp.UseDefaultCredentials = true;
            nvp.Url = dtNAV.Rows[0]["URL"].ToString();
            nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());

            if (drpSemester.SelectedValue.Contains("YEAR"))
            {
                nvp.UpdateTotalValues_New("NORMAL", drpCourse.SelectedValue, drpAcademicYear.SelectedValue, Session["GlobalDimension1Code"].ToString(), "", drpSemester.SelectedValue, ddlSubject.SelectedValue, "", drpSection.SelectedValue, ddlGroup.SelectedValue);
            }
            else
            {
                nvp.UpdateTotalValues_New("NORMAL", drpCourse.SelectedValue, drpAcademicYear.SelectedValue, Session["GlobalDimension1Code"].ToString(), drpSemester.SelectedValue, "", ddlSubject.SelectedValue, "", drpSection.SelectedValue, ddlGroup.SelectedValue);
            }

            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "OpenNewWindow2()", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Some thing went wrong,Please try after some time');", true);
            return;
        }
    }
    protected void Rblist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Rblist.SelectedValue == "1") { bindSubject(); bindgrid(); PanelReport.Visible = false; onetime.Visible = false; printarea.Visible = true; PnlUFM.Visible = false; }
        else { bindSubject(); bindApprovalList(); panelmarksentry.Visible = false; btnview.Visible = false; onetime.Visible = true;printarea.Visible = false; }

    }
    public void bindApprovalList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[Pro_GetListForReportApproval_practical]", con); //coment on usp_GetMarkEntrytable1 27-12-2017 by ashu sp_GetInternalMarksDataForApprovalByHOD
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());

            cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
            cmd.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);
            if (ddlSubject.SelectedIndex > 0)
            { cmd.Parameters.AddWithValue("@Subject", ddlSubject.SelectedValue); }
            else { cmd.Parameters.AddWithValue("@Subject", ""); }

            // if (drpFaculty.SelectedIndex > 0)
            //   { 
            cmd.Parameters.AddWithValue("@facultyCode", ""); //}
            //  else { cmd.Parameters.AddWithValue("@facultyCode", ""); }
            cmd.Parameters.AddWithValue("@UserRole", Session["UserRole"].ToString());
            cmd.Parameters.AddWithValue("@Type", Rblist.SelectedValue);
            cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
            cmd.Parameters.AddWithValue("@Studentgroup", ddlGroup.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            grdViewAwardlistApproval.DataSource = dt;
            grdViewAwardlistApproval.DataBind();
            if (dt.Rows.Count > 0)
            {
                panelmarksentry.Visible = false;
                PanelReport.Visible = true;
                tblAR.Visible = true;

            }
            else { tblAR.Visible = false; }
            if (grdViewAwardlistApproval.Rows.Count > 0)
            {
                PnlUFM.Visible = true;

                DataTable dtUFM = new DataTable();
                foreach (GridViewRow row in grdViewAwardlistApproval.Rows)
                {
                    HiddenField HfStaffcode1 = (HiddenField)row.FindControl("HfStaffcode");
                    HiddenField HfSubjectCode = (HiddenField)row.FindControl("HfSubjectCode");
                    CheckBox SChkD = (CheckBox)row.FindControl("SChkD1");
                  
                        SqlCommand cmdUFM = new SqlCommand("Sp_GetUFMData", con);
                        cmdUFM.CommandType = CommandType.StoredProcedure;
                        cmdUFM.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
                        cmdUFM.Parameters.AddWithValue("@Subject", HfSubjectCode.Value);
                        cmdUFM.Parameters.AddWithValue("@facultyCode", HfStaffcode1.Value);
                        cmdUFM.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
                        cmdUFM.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);
                        con.Open();
                        SqlDataAdapter daUFM = new SqlDataAdapter(cmdUFM);
                        daUFM.Fill(dtUFM);
                        con.Close();
                  
                }
                grdViewmarksEntryUFM.DataSource = dtUFM;
                grdViewmarksEntryUFM.DataBind();
                if (dtUFM.Rows[0]["intMarks"].ToString() == "-1" )
                {
                    if (dtUFM.Rows[0]["Status"].ToString() == "3" || dtUFM.Rows[0]["Status"].ToString() == "5")
                    {
                        btnUFMApprove.Visible = false;
                    }
                    else
                    {
                        btnUFMApprove.Visible = true;
                    }
                }
                else
                {
                    btnUFMApprove.Visible = false;
                }

            }
            else
            {
                PnlUFM.Visible = false;
            }
        }
        catch
        {
        }
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

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            foreach (GridViewRow row in grdViewAwardlistApproval.Rows)
            {
                HiddenField HfStaffcode1 = (HiddenField)row.FindControl("HfStaffcode");
                HiddenField HfSubjectCode = (HiddenField)row.FindControl("HfSubjectCode");
                CheckBox SChkD = (CheckBox)row.FindControl("SChkD1");
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
                    cmd.Parameters.AddWithValue("@StudentGroup", ddlGroup.SelectedValue);
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
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            int validate = 0;
            foreach (GridViewRow row in grdViewmarksEntryUFM.Rows)
            {
                Label AttandenceType = (Label)row.FindControl("lblAttendancetype");
                DropDownList ddlUFMStatus = (DropDownList)row.FindControl("drpUFM");
                if (AttandenceType.Text.ToUpper() == "UFM" && ddlUFMStatus.SelectedItem.Text == "Select")
                {
                    validate = 1;
                }


            }
            if (validate == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error','UFM Student Approval is mandatory First.. ');", true);
                bindApprovalList();
                return;
                
            }
            else
            {
                int result = 0;
                foreach (GridViewRow row in grdViewAwardlistApproval.Rows)
                {
                    HiddenField HfStaffcode1 = (HiddenField)row.FindControl("HfStaffcode");
                    HiddenField HfSubjectCode = (HiddenField)row.FindControl("HfSubjectCode");
                    CheckBox SChkD = (CheckBox)row.FindControl("SChkD1");
                    if (SChkD.Checked == true)
                    {
                        SqlCommand cmd = new SqlCommand("Update_ReportApproval", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Acadyear", drpAcademicYear.SelectedValue);
                        cmd.Parameters.AddWithValue("@Subject", HfSubjectCode.Value);
                        cmd.Parameters.AddWithValue("@facultyCode", HfStaffcode1.Value);
                        cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
                        cmd.Parameters.AddWithValue("@Sem", drpSemester.SelectedValue);
                        //if (Session["UserRole"].ToString() == "HOD")
                        //{
                            cmd.Parameters.AddWithValue("@ApprovalStatus", 2);
                        //}
                        //if (Session["UserRole"].ToString() == "Pricipal")
                        //{
                        //    cmd.Parameters.AddWithValue("@ApprovalStatus", 4);
                        //}
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
        }
        catch (Exception ex)
        {
        }
   
    }

    protected void btnview1_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);
        HiddenField HfStaffcode = (HiddenField)row.FindControl("HfStaffcode");
        HiddenField HfSubjectCode = (HiddenField)row.FindControl("HfSubjectCode");
        HiddenField HfExamType = (HiddenField)row.FindControl("HfExamType");
        Session["FacultyCode"] = HfStaffcode.Value;
        Session["AcademicYear"] = drpAcademicYear.SelectedValue;
        Session["Subject"] = HfSubjectCode.Value;
        Session["drpCourse"] = drpCourse.SelectedValue;
        Session["drpSemester"] = drpSemester.SelectedValue;
        Session["Examtype"] = HfExamType.Value;
        if (drpSection.SelectedIndex > 0)
        {
            Session["Section"] = drpSection.SelectedValue;
        }
        else
        {
            Session["Section"] = "";
        }
        if (ddlGroup.SelectedIndex > 0)
        {
            Session["Group"] = ddlGroup.SelectedValue;
        }
        else
        {
            Session["Group"] = "";
        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "OpenNewWindow1()", true);


    }
    protected void SChlAll1_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkrow1 = (CheckBox)grdViewAwardlistApproval.HeaderRow.FindControl("SChlAll1");
        if (chkrow1.Checked)
        {

            foreach (GridViewRow row in grdViewAwardlistApproval.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("SChkD1");
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
                    CheckBox chkrow = (CheckBox)row.FindControl("SChkD1");
                    if (chkrow.Enabled == true)
                    {
                        chkrow.Checked = false;
                    }
                }
            }
        }
    }
    protected void chkPiv_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPiv.Checked == true)
        {
            PanelReport.Visible = true;
            panelmarksentry.Visible = false;
        }
        else if (chkPiv.Checked == false && Rblist.SelectedValue == "2")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'One time approval is mandatory for Practical');", true);
            chkPiv.Checked = true;
            return;
        }
        else
        {

            PanelReport.Visible = false;
            panelmarksentry.Visible = true;
        }
        }
    protected void grdViewmarksEntrySubmit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblName = (Label)e.Row.FindControl("lblObtainedmark");
            DropDownList drpUFM = (DropDownList)e.Row.FindControl("drpUFM");
            string lblText = lblName.Text;
            if (lblName.Text.ToUpper() == "UFM")
            {

                TextBox txt = (TextBox)e.Row.FindControl("txtRemarks");
                txt.Enabled = true;
                TextBox txtBox = new TextBox();
                txtBox.ID = "lblObtainedmark";
                txtBox.Text = lblText;

                e.Row.Cells[8].Controls.Add(txtBox);
                e.Row.Cells[8].Controls.Remove(lblName);
                
            }
            Label lblatttype = (Label)e.Row.FindControl("lblAttendancetype");
            if (lblatttype.Text.ToUpper() == "UFM")
            {
                drpUFM.Visible = true;
                HiddenField hfUFMStatus = (HiddenField)e.Row.FindControl("hfUFMStatus");
                drpUFM.SelectedValue = hfUFMStatus.Value;
               
                
            }

        }
    }
    protected void grdViewmarksEntryUFM_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          
            DropDownList drpUFM = (DropDownList)e.Row.FindControl("drpUFM");
            
            Label lblatttype = (Label)e.Row.FindControl("lblAttendancetype");
            if (lblatttype.Text.ToUpper() == "UFM")
            {
                drpUFM.Visible = true;
                HiddenField hfUFMStatus = (HiddenField)e.Row.FindControl("hfUFMStatus");
                drpUFM.SelectedValue = hfUFMStatus.Value;


            }

        }
    }
    protected void btnUFMApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string result;
            foreach (GridViewRow row in grdViewmarksEntryUFM.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lblDocumentNo = (Label)row.FindControl("lblDocNo");
                    Label lblStudentNo = (Label)row.FindControl("lblEnrollment");
                    TextBox txtObtainedmarks = (TextBox)row.FindControl("lblObtainedmark");
                    TextBox txtremark = (TextBox)row.FindControl("txtRemarks");
                    Label lblMethod = (Label)row.FindControl("lblMethod");
                    DropDownList drpUFMstatus = (DropDownList)row.FindControl("drpUFM");
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
                    SqlCommand cmd;
                    cmd = new SqlCommand("UpdateMarksByHod", con);//Sp_MarksEntryupdatedByHodPrincipal
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@documentNo", lblDocumentNo.Text);
                    cmd.Parameters.AddWithValue("@StudentNo", lblStudentNo.Text);
                    cmd.Parameters.AddWithValue("@Obtainedmarks", txtObtainedmarks.Text);
                    cmd.Parameters.AddWithValue("@Remarks", txtremark.Text);
                    cmd.Parameters.AddWithValue("@Method", lblMethod.Text);
                    cmd.Parameters.AddWithValue("@UFMStatus", drpUFMstatus.SelectedValue);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtNAV = new DataTable();
                        SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con);
                        cmdNAV.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
                        daNAV.Fill(dtNAV);
                        VoucherPosting nvp = new VoucherPosting();
                        nvp.UseDefaultCredentials = true;
                        nvp.Url = dtNAV.Rows[0]["URL"].ToString();
                        nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
                        result = nvp.InternalTotalMarks(dt.Rows[0]["Subject Exam Craiteria"].ToString().Trim(), dt.Rows[0]["Global Dimension 1 Code"].ToString().Trim(), dt.Rows[0]["Academic Year"].ToString(), dt.Rows[0]["Subject Code"].ToString(), dt.Rows[0]["Course"].ToString(), lblStudentNo.Text);

                    }
                    con.Close();
                }
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'UFM Data Approved Successfully');", true);
            chkPiv.Checked = true;
            bindgrid();
            return;

        }
        catch (Exception ex)
        {
           
        }
    }


    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSectionList();


        bindGroupList();
    }
}