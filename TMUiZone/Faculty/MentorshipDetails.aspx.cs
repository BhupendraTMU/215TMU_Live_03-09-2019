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
using Utility;
using System.Drawing;
public partial class Faculty_MentorshipDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    string FacultyCode = "", CourseCode="";
    static String No_ = "";
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
            string proc = "sp_GetAccessForTimeTable_Role";
            if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC")
            {
                proc = "sp_GetAccessForTimeTable_RoleMD";
            }
            SqlCommand cmd1 = new SqlCommand(proc, con1);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@userid", Session["uid"].ToString());
            cmd1.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
            cmd1.Parameters["@Return1"].Direction = ParameterDirection.Output;
            con1.Open();
            cmd1.ExecuteNonQuery();
            string res = cmd1.Parameters["@Return1"].Value.ToString();
            con1.Close();

            if (res == "1")
            {
                FacultyCode = drpFacultyCode.SelectedValue;
                con.Close();
                chkboxAsPrincipal.Visible = true;
            }
            else
            {
                con.Close();
                FacultyCode = Session["uid"].ToString();
                drpFacultyCode.SelectedIndex = -1;
                drpCourseCode1.SelectedIndex = -1;
                chkboxAsPrincipal.Visible = false;
            }

            if (!IsPostBack)
            {
                bindAcademicYear();
                bindDrpCourseList();
                bindSectionList();
                BindData(FacultyCode);
                bindCourseCode1();
                if (rblInteractionAward.SelectedValue == "Award")
                {
                    pnlAward.Visible = true;
                    pnlInteraction.Visible = false;
                   
                }
                else
                {
                    pnlAward.Visible = false;
                    pnlInteraction.Visible = true;                   
                }
                bindAbsentStudentList();
            }
            

        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
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
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role_mentorship", con);// proc_GetCourseFromCourseWiseFacultyCollege_Role
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
        con.Close();
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        
        drpCourse.DataBind();
       
    }
    
    protected void grdStudentDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdStudentDetails.PageIndex = e.NewPageIndex;
        BindData(FacultyCode);
    }
    protected void drpCourseCode1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindFacultyCode();
    }
    protected void drpFacultyCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(FacultyCode);
    }
    public void bindCourseCode1()
    {
        string proc = "Sp_GetCourseRoleWise_HOD_Role_ForMentor";
        if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC")
        {
            proc = "Sp_GetCourseRoleWise_HOD_Role_ForMentorDM";
        }
        SqlCommand cmd = new SqlCommand(proc, con);
       // SqlCommand cmd = new SqlCommand("Sp_GetCourseRoleWise_HOD_Role_ForMentor", con);//Sp_GetCourseRoleWise_HOD_Role_ForMentorDM
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@userid", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourseCode1.DataSource = dt;
        drpCourseCode1.DataTextField = "Details";
        drpCourseCode1.DataValueField = "No_";
        drpCourseCode1.DataBind();
    }
    public void bindFacultyCode()
    {
        //SqlCommand cmd = new SqlCommand("select distinct([Faculty Code]),[Faculty Name] from [TMU$Course Wise Faculty] where [Course Code]='" + drpCourseCode1.SelectedItem.ToString() + "'", con);
        SqlCommand cmd = new SqlCommand("select distinct(C.[Faculty Code]),C.[Faculty Name] from [TMU$Student Details Mentorship] M left outer join  [TMU$Course Wise Faculty] C on C.[Faculty Code]=M.Faculty   where M.[Program]='" + drpCourseCode1.SelectedValue + "' and isnull(C.[Faculty Code],'')<>'' and [Portal ID]='1' ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpFacultyCode.DataSource = dt;
        drpFacultyCode.DataTextField = "Faculty Name";
        drpFacultyCode.DataValueField = "Faculty Code";
        drpFacultyCode.DataBind();
     

    }
    public void BindData(string FacultyCode)
    {
        FacultyCode = "";  
        DataTable dt = new DataTable();
              

          SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
          string proc = "sp_GetAccessForTimeTable_Role";
          if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC")
          {
              proc = "sp_GetAccessForTimeTable_RoleMD";
          }
          SqlCommand cmd1 = new SqlCommand(proc, con1);
          //  SqlCommand cmd1 = new SqlCommand("sp_GetAccessForTimeTable_Role", con1);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@userid", Session["uid"].ToString());
            cmd1.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
            cmd1.Parameters["@Return1"].Direction = ParameterDirection.Output;
            con1.Open();
            cmd1.ExecuteNonQuery();
            string res = cmd1.Parameters["@Return1"].Value.ToString();
            con1.Close();

            if (res == "1" && chkboxAsPrincipal.Checked==true)
            {
                con.Close();
                FacultyCode = drpFacultyCode.SelectedValue;
                CourseCode = drpCourseCode1.SelectedValue;
            }
            else
            {
                con.Close();
                FacultyCode = Session["uid"].ToString();
            }

        string FacultyCodeCollegeCode = FacultyCode + "-" + Session["GlobalDimension1Code"].ToString();
        //dt = FDL.GetMentorshipDetails(FacultyCodeCollegeCode, CourseCode);
    
        DataTable dt1 = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetMentorshipDetailsFacultyCourse_test", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@GD1", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@UID", Session["uid"].ToString());
        if (drpCourse.SelectedIndex > 0)
        { cmd.Parameters.AddWithValue("@CourseCode", drpCourse.SelectedValue); }
        else { cmd.Parameters.AddWithValue("@CourseCode", ""); }
        //cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        if (drpSection.SelectedIndex > 0)
        { cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue); }
        else { cmd.Parameters.AddWithValue("@Section", ""); }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt1);
      
   
        if (dt1.Rows.Count > 0)
        {
            btnExport.Visible = true;
        }
        else { btnExport.Visible = false; }
        grdStudentDetails.DataSource = dt1;
        grdStudentDetails.DataBind();
    }
    public void ShowHide()
    {
        if (chkboxAsPrincipal.Checked == true)
        {
            lblCourse.Visible = true;
            drpCourseCode1.Visible = true;
            lblFaculty.Visible = true;
            drpFacultyCode.Visible = true;
            lblStudentAbsent.Visible = false;
            grdContinuseAbsentStudent.Visible = false;
        }
        else
        {
            drpFacultyCode.SelectedIndex = -1;
            drpCourseCode1.SelectedIndex = -1;
            lblCourse.Visible = false;
            drpCourseCode1.Visible = false;
            lblFaculty.Visible = false;
            drpFacultyCode.Visible = false;
            lblStudentAbsent.Visible = true;
            grdContinuseAbsentStudent.Visible = true;
        }
    }
    protected void chkboxAsPrincipal_CheckedChanged(object sender, EventArgs e)
    {
        ShowHide();
        BindData(FacultyCode);
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        No_ = grdStudentDetails.SelectedDataKey.Value.ToString();
        Clear();
        BindGrid();
        BindGridAward();
       //  tblF.Visible = false;
       mpe.Show();
       
    }
    public void Bindtable()
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand("select * from [TMU$Student Details Mentorship] where No_='" + No_ + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        lblStudentName.Text = dt.Rows[0]["Student Name"].ToString();
        con.Close();
    }
    public void Clear()
    {
        txtContactPerson.Text = "";
        txtDate.Text = "";
        txtPhoneNo.Text = "";
        txtInteractionSummary.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Bindtable();
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand("proc_InsertMentorInteraction", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date",txtDate.Text);
        cmd.Parameters.Add("@StudentNo",No_);
        cmd.Parameters.Add("@FacultyNo",FacultyCode);
        cmd.Parameters.Add("@PhoneNo",txtPhoneNo.Text);
        cmd.Parameters.Add("@ContactPersonName",txtContactPerson.Text);
        cmd.Parameters.Add("@InteractionSummary",txtInteractionSummary.Text);
        cmd.Parameters.Add("@StudentName", dt.Rows[0]["Student Name"].ToString());
        cmd.Parameters.Add("@Program", dt.Rows[0]["Program"].ToString());
        cmd.Parameters.Add("@Section", dt.Rows[0]["Section"].ToString());
        cmd.Parameters.Add("@FathersName", dt.Rows[0]["Fathers Name"].ToString());
        cmd.Parameters.Add("@MothersName", dt.Rows[0]["Mothers Name"].ToString());
        cmd.Parameters.Add("@StudentPhoneNo", dt.Rows[0]["Phone Number Student"].ToString());
        cmd.Parameters.Add("@EmailAdd", dt.Rows[0]["E-Mail Address Student"].ToString());
        cmd.Parameters.Add("@DOB", Convert.ToDateTime(dt.Rows[0]["Date of Birth"].ToString()));
        cmd.Parameters.Add("@CollegeCode", dt.Rows[0]["Global Dimension 1 Code"].ToString());
        cmd.Parameters.Add("@EnrollmentNo", dt.Rows[0]["Enrollment No_"].ToString());
        cmd.Parameters.Add("@Batch", dt.Rows[0]["Batch"].ToString());
        cmd.Parameters.Add("@Group", dt.Rows[0]["Group"].ToString());
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
        BindGrid();
        mpe.Show();
        txtDate.Text = ""; txtPhoneNo.Text = ""; txtContactPerson.Text = ""; txtInteractionSummary.Text = "";
    }
    public void BindGrid()
    {
        SqlCommand cmd = new SqlCommand("select convert(nvarchar,[Communication Date],106) as Date1,[Contact Person Phone No_],[Contact Person Name],[Interaction Summary],[Line No_] as [LineNo] from [TMU$Student Details Mentorship] where [No_]='" + No_ + "' and [Contact Person Phone No_]!='' order by [Communication Date] desc", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        grdInteractionDetails.DataSource = dt1;
        grdInteractionDetails.DataBind();
    }
    protected void grdInteractionDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DeleteInteraction"))
        {
            GridViewRow rowItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            int index = rowItem.RowIndex;
            HiddenField hfLineNo = (HiddenField)rowItem.FindControl("hfLineNo");
            if (hfLineNo.Value != "0")
            {
                SqlCommand cmd = new SqlCommand("proc_DeleteMentorInteraction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StudentNo", No_);
                cmd.Parameters.Add("@LineNo", hfLineNo.Value);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            BindGrid();
            mpe.Show();  
        }
    }
    [System.Web.Services.WebMethod]
    //public static string CreateSessionViaJavascript(string strTest)
    //{
    //    Page objp = new Page();
    //    objp.Session["MStudentId"] =No_;
    //    return strTest;
    //}
    protected void rblInteractionAward_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblInteractionAward.SelectedValue == "Award")
        {
            pnlAward.Visible = true;
            pnlInteraction.Visible = false;
            mpe.Show();
        }
        else
        {
            pnlAward.Visible = false ;
            pnlInteraction.Visible = true ;
            mpe.Show();
        }
    }
    protected void grdAward_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DeleteAward"))
        {
            GridViewRow rowItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            int index = rowItem.RowIndex;
            HiddenField hfLineNo = (HiddenField)rowItem.FindControl("hfLineNo");
            if (hfLineNo.Value != "0")
            {
                SqlCommand cmd = new SqlCommand("proc_DeleteAwardByMentor", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StudentNo", No_);
                cmd.Parameters.Add("@LineNo", hfLineNo.Value);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            BindGridAward();
            mpe.Show();
        }
    }
    protected void btnSaveAward_Click(object sender, EventArgs e)
    {
        //Bindtable();
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand("proc_InsertAwardByMentor", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", TextAwardDate.Text);
        cmd.Parameters.Add("@StudentNo",No_);
        cmd.Parameters.Add("@AwardType", txtAward.Text);
        cmd.Parameters.Add("@AwardSummary",txtAwardSummary.Text);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());        
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
        BindGridAward();
        mpe.Show();
        TextAwardDate.Text = ""; txtAward.Text = ""; txtAwardSummary.Text = "";
    }
    public void BindGridAward()
    {   
     //SqlCommand cmd = new SqlCommand("select convert(nvarchar,[AwardDate],106) as Date1,[AwardType],[AwardDescription],[id] as [LineNo] from [TMUStudentAward] where [StudentNo]='" + No_ + "' order by [id] desc", con);
        SqlCommand cmd = new SqlCommand("proc_GetAwardDataByMentor", con);
        cmd.CommandType = CommandType.StoredProcedure;       
        cmd.Parameters.Add("@StudentNo", No_);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter daAward = new SqlDataAdapter(cmd);
        DataTable dtAward = new DataTable();
        daAward.Fill(dtAward);
        grdAward.DataSource = dtAward;
        grdAward.DataBind();
    }

    public void bindAbsentStudentList()
    {
        SqlCommand cmd1 = new SqlCommand("GetStudentAbsentList_HOD_Role_ForMentor", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd1.Parameters.Add("@CourseCode", "");
        cmd1.Parameters.Add("@SemYear", "");
        cmd1.Parameters.Add("@Section", "");
        cmd1.Parameters.Add("@ID", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        cmd1.CommandTimeout = 5000;
        DataTable dt = new DataTable();
        dt.Clear();
        da.Fill(dt);
        if (chkboxAsPrincipal.Checked != true)
        {
            grdContinuseAbsentStudent.DataSource = dt;
            grdContinuseAbsentStudent.DataBind();
        }
    }

    protected void lnkOP_Click(object sender, EventArgs e)
    {
        if (Session["GlobalDimension1Code"].ToString() == "TMDC")
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            int index = row.RowIndex;
            //Label lblEmpCode = (Label)grdStudentDetails.Rows[index + 1].FindControl("lblEmpCode");
            HiddenField hfStudentNo = (HiddenField)grdStudentDetails.Rows[index].FindControl("hfStudentNo_");
            HiddenField hfStudentName = (HiddenField)grdStudentDetails.Rows[index].FindControl("hfStudentName");
            lblStudentENo.Text = hfStudentName.Value;
            SqlCommand cmd = new SqlCommand("proc_GetOverAllSubjectWisePercentageOnMentor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudentCode", hfStudentNo.Value);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daOP = new SqlDataAdapter(cmd);
            DataTable dtOP = new DataTable();
            daOP.Fill(dtOP);
            grdOP_Details.DataSource = dtOP;
            grdOP_Details.DataBind();
            MpOPDetails.Show();
        }
       
    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Feedback.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        grdStudentDetails.AllowPaging = false;
        BindData(FacultyCode);
        string headerTable = @"<Table bgcolor=gray><tr><td colspan=7 align=center bgcolor=gold ><font size=16><h1>Mentorship Details</h1></font> </td></tr></td></tr></Table>";
        Response.Write(headerTable);
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            grdStudentDetails.HeaderRow.BackColor = Color.YellowGreen;

            foreach (TableCell cell in grdStudentDetails.HeaderRow.Cells)
            {
                cell.BackColor = grdStudentDetails.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdStudentDetails.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdStudentDetails.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdStudentDetails.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            grdStudentDetails.Columns[grdStudentDetails.Columns.Count - 3].Visible = false;
            grdStudentDetails.Columns[grdStudentDetails.Columns.Count - 1].Visible = false;
            grdStudentDetails.RenderControl(hw);
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
    protected void grdContinuseAbsentStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdContinuseAbsentStudent.PageIndex = e.NewPageIndex;
        bindAbsentStudentList();
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();
        bindSectionList();
        BindData(FacultyCode);
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSectionList();
        BindData(FacultyCode);
    }
   
    public void bindSectionList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Roleformentorship", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
     
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(FacultyCode);
    }
    protected void btnviewAttendanced_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;
        //Label lblEmpCode = (Label)grdStudentDetails.Rows[index + 1].FindControl("lblEmpCode");
        HiddenField hfStudentNo = (HiddenField)grdStudentDetails.Rows[index].FindControl("hfStudentNo_");
        Session["MStudentId"] = hfStudentNo.Value;
        
        
        Response.Redirect("~/StudentReport.aspx");
    }
}