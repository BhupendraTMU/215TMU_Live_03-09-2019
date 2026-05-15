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
using Utility;

public partial class Faculty_FacultyCoursePlan : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    DataTable dt = new DataTable();
    DataRow row;
    static int p = 0;
    string NextNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hfDocumentNo.Value = "";
                bindAcademicYear();
                bindDrpCourseList();
                p = 0;
               // BindAppliedCoursePlan();
                pnlunit.Visible = false;
            }

            //if (Session["GlobalDimension1Coded"].ToString() == "TMDC")
            //{
            //    txtHOD.Visible = true;
            //    RequiredFieldValidatorHODCode.Visible = true;
            //}
            //else
            //{
            //    txtHOD.Visible = false;
            //    RequiredFieldValidatorHODCode.Visible = false;
            //}

        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfDocumentNo.Value = "";
        bindGetSubjectList();
        bindUnitCode();
        BindAppliedCoursePlan();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
        bindUnitCode();
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGetSubjectList();
        bindSectionList();
        bindGroupList();
        bindBatchList();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGetSubjectList();
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubjectType();
        bindUnitCode();       
    }
    protected void drpUnitCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindUnitName();
        bindChapterCode();
    }
    protected void drpChapterCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindChapterName();
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
    public void bindGroupList()
    {
        SqlCommand cmd = new SqlCommand("sp_GetGroup_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "Details";
        ddlGroup.DataValueField = "No_";
        ddlGroup.DataBind();
    }
    public void bindBatchList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetBatchFromCourseSemester_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlBatch.DataSource = dt;
        ddlBatch.DataTextField = "Details";
        ddlBatch.DataValueField = "No_";
        ddlBatch.DataBind();
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
    public void bindGetSubjectList()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromCourseSubjectLine", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();
    }
    public void bindSubjectType()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSubjectTypeFromCourseSubjectLine", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtSubjectType.Text = dt.Rows[0]["Subject Type"].ToString();
    }
    public void bindUnitCode()
    {
        SqlCommand cmd = new SqlCommand("proc_GetUnitCode", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode",drpCourse.SelectedValue);
        cmd.Parameters.Add("@SubjectCode",drpSubject.SelectedValue);
        cmd.Parameters.Add("@AcademicYear",drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpUnitCode.DataSource = dt;
        drpUnitCode.DataTextField = "Details";
        drpUnitCode.DataValueField = "No_";
        drpUnitCode.DataBind();

    }
    public void bindUnitName()
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand("proc_GetUnitDescription", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UnitCode", drpUnitCode.SelectedValue);
        //cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        txtUnitName.Text = cmd.ExecuteScalar().ToString();
        con.Close();
    }
    public void bindChapterCode()
    {
        SqlCommand cmd = new SqlCommand("proc_GetChapterCode", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UnitCode", drpUnitCode.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpChapterCode.DataSource = dt;
        drpChapterCode.DataTextField = "Details";
        drpChapterCode.DataValueField = "No_";
        drpChapterCode.DataBind();
    }
    public void bindChapterName()
    {
        if (con.State == ConnectionState.Closed)
            con.Open();

        SqlCommand cmd = new SqlCommand("proc_GetChapterDescription", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UnitCode", drpUnitCode.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@ChapterCode", drpChapterCode.SelectedValue);       
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        
        txtChapterName.Text = cmd.ExecuteScalar().ToString();
        con.Close();
    }
   
    public DataTable GetDataAndValidateDuplicateHeader()
    { //proc_GetValidateDuplicateCoursePlanHeaderAndFillGrid
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetValidateDuplicateCoursePlanHeaderAndFillGrid_2", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());        
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);
        cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue);
        cmd.Parameters.Add("@SubjectHOD", ""); 
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        return dt;
    
    }    
    public void Clear()
    {
        drpUnitCode.SelectedIndex = 0;
        txtUnitName.Text = "";
        drpChapterCode.DataSource = "";
        drpChapterCode.DataBind();
        txtChapterName.Text = "";
       // txtNoOfMinuites.Text = "";       
        txtPeriod.Text = "";
        //txtSceduledDate.Text = "";        
        txtTopic.Text = "";
    }
    public void BindAppliedCoursePlan()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAppliedCoursePlan", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        
        SqlDataAdapter da=new SqlDataAdapter (cmd);
        DataTable dt=new DataTable ();
        da.Fill(dt);
        grdAppliedCoursePlan.DataSource = dt;
        grdAppliedCoursePlan.DataBind();
    }
    public void BindAppliedCoursePlan1()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAppliedCoursePlanFacultyCourseSubjectwise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);
        cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAppliedCoursePlan.DataSource = dt;
        grdAppliedCoursePlan.DataBind();
    }
    public void HeaderEnable(Boolean TF)
    {
        drpAcademicYear.Enabled = TF;        
        drpCourse.Enabled = TF;
        drpSemester.Enabled = TF;
        drpSection.Enabled = TF;
        drpSubject.Enabled = TF;
        ddlGroup.Enabled = TF;
        ddlBatch.Enabled = TF;        
        if (TF == false)
        {
            drpAcademicYear.BackColor = System.Drawing.Color.Gray;
            drpCourse.BackColor = System.Drawing.Color.Gray;
            drpSemester.BackColor = System.Drawing.Color.Gray;
            drpSection.BackColor = System.Drawing.Color.Gray;
            drpSubject.BackColor = System.Drawing.Color.Gray;
            ddlGroup.BackColor = System.Drawing.Color.Gray;
            ddlBatch.BackColor = System.Drawing.Color.Gray;
            pnlunit.Visible = true;
        }
        else
        {
            drpAcademicYear.BackColor = System.Drawing.Color.White;
            drpCourse.BackColor = System.Drawing.Color.White;
            drpSemester.BackColor = System.Drawing.Color.White;
            drpSection.BackColor = System.Drawing.Color.White;
            drpSubject.BackColor = System.Drawing.Color.White;
            ddlGroup.BackColor = System.Drawing.Color.White;
            ddlBatch.BackColor = System.Drawing.Color.White;
            pnlunit.Visible = false;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        btnSendForApproval.Visible = false;
        DataTable dt=new DataTable();
        dt = GetDataAndValidateDuplicateHeader();
        if (dt.Rows.Count>0 )
        {
            hfDocumentNo.Value = dt.Rows[0]["DocumentNo"].ToString();
            if (hfDocumentNo.Value != "" && dt.Rows[0]["Unit Code"].ToString()!="")
            {
                BindAppliedCoursePlan1();
                grdFacultyCoursePlan.DataSource = dt;
                grdFacultyCoursePlan.DataBind();
                ViewState["datatable"] = dt;
            }
            else
            {
                grdAppliedCoursePlan.DataSource = null;
                grdAppliedCoursePlan.DataBind();
                grdFacultyCoursePlan.DataSource = null;
                grdFacultyCoursePlan.DataBind();
            
            }
        }        
        HeaderEnable(false);
        pnlunit.Visible = true;
        

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("FacultyCoursePlan.aspx");
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["datatable"] != null) { p = 0; }
            
        if (p == 0)
        {
            DataColumn dc;
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "DocumentNo";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Unit Code";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Unit Name";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Chapter Code";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Chapter Name";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Topics";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Period";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "No Of Minuites";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Scheduled Date";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "LineNo";
            dt.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "Status";
            dt.Columns.Add(dc);
            p = 8;
        }

        if (ViewState["datatable"] != null)
            dt = (DataTable)ViewState["datatable"];
        row = dt.NewRow();
        row["DocumentNo"] = "";
        row["Unit Code"] = drpUnitCode.SelectedValue;
        row["Unit Name"] = txtUnitName.Text;
        row["Chapter Code"] = drpChapterCode.SelectedValue;
        row["Chapter Name"] = txtChapterName.Text;
        row["Topics"] = txtTopic.Text;
        row["Period"] = txtPeriod.Text;
        row["No Of Minuites"] = 0    ; //txtNoOfMinuites.Text;
        row["Scheduled Date"] = "01-01-1753";//txtSceduledDate.Text;
        row["LineNo"] = 0;
        row["Status"] = "Pending";

        dt.Rows.Add(row);
        ViewState["datatable"] = dt;
        grdFacultyCoursePlan.DataSource = dt;
        grdFacultyCoursePlan.DataBind();
        HeaderEnable(false);
        //drpAcademicYear.Enabled = false;
        //drpCourse.Enabled = false;
        //drpSemester.Enabled = false;
        //drpSection.Enabled = false;
        //drpSubject.Enabled = false;
        btnSendForApproval.Visible = true;
        Clear();
    }
    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        DataUtility objDut = new DataUtility();
        //NextNo = objDut.ExecScalarCmdText("select [dbo].[NextCoursePlanNumber]()");
        NextNo = hfDocumentNo.Value;        
        dt = (DataTable)ViewState["datatable"];
        if (NextNo == "")
        {
            InsertIntoCoursePlanHeader();
            InsertIntoCoursePlanLine();
        }
        else
        {
            ResendCoursePlanHeader();
            InsertIntoCoursePlanLine();
        }
    }
   
    public void InsertIntoCoursePlanHeader()
    {
        string PlanStatus = "1";
        if (Session["GlobalDimension1Code"].ToString() == "TMDC")
            PlanStatus = "2";
        SqlCommand cmd = new SqlCommand("proc_InsertIntoCoursePlanHeader_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@DocumentNo", NextNo);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@PlanStatus", PlanStatus);
        cmd.Parameters.Add("@Week", "");
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);
        cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue);
        cmd.Parameters.Add("@SubjectHOD", "");
        if (con.State == ConnectionState.Closed)
            con.Open();
       // cmd.ExecuteNonQuery();
        string a=(string) cmd.ExecuteScalar();
        hfDocumentNo.Value = a;
        con.Close();
    }
    public void ResendCoursePlanHeader()
    {
        string query = "proc_UpdateResendCoursePlanHeader";
        if (Session["GlobalDimension1Code"].ToString() == "TMDC")
            query = "proc_UpdateResendCoursePlanHeader_ForDental";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@DocumentNo", NextNo);        
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();        
        con.Close();
    }
    public void ResendCoursePlanLine()
    {
        SqlCommand cmd = new SqlCommand("proc_UpdateResendCoursePlanLine", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@DocumentNo", NextNo);
        if (con.State == ConnectionState.Closed)
        { con.Open(); }
        cmd.ExecuteNonQuery();
        //con.Close();
    }

    public void InsertIntoCoursePlanLine()
    {
        string Applied = "";
        if (con.State == ConnectionState.Closed)
            con.Open();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["DocumentNo"] == "")
            {
                NextNo = hfDocumentNo.Value;
                string query = "proc_InsertIntoCoursePlanLine";
                if (Session["GlobalDimension1Code"].ToString() == "TMDC")
                    query = "proc_InsertIntoCoursePlanLine_ForDental";
                SqlCommand cmd1 = new SqlCommand(query, con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DocumentNo", NextNo);
                cmd1.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                cmd1.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
                cmd1.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                cmd1.Parameters.Add("@UnitCode", dt.Rows[i]["Unit Code"].ToString());
                cmd1.Parameters.Add("@ChapterCode", dt.Rows[i]["Chapter Code"].ToString());
                cmd1.Parameters.Add("@ChapterName", dt.Rows[i]["Chapter Name"].ToString());
                cmd1.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
                cmd1.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                cmd1.Parameters.Add("@UnitName", dt.Rows[i]["Unit Name"].ToString());
                cmd1.Parameters.Add("@Week", "");
                cmd1.Parameters.Add("@Period", dt.Rows[i]["Period"].ToString());
                cmd1.Parameters.Add("@NoOfMinuites", dt.Rows[i]["No Of Minuites"].ToString());
                cmd1.Parameters.Add("@Section", drpSection.SelectedValue);
                cmd1.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                cmd1.Parameters.Add("@ScheduledDate", dt.Rows[i]["Scheduled Date"].ToString());
                cmd1.Parameters.Add("@ActualDate", "01-01-1753");
                cmd1.Parameters.Add("@Topic", dt.Rows[i]["Topics"].ToString());
                cmd1.ExecuteNonQuery();
            }
            else
            {
                if (dt.Rows[i]["Status"].ToString().ToUpper() != "APPROVED" && Applied =="")
                {
                    ResendCoursePlanLine();
                    Applied = "Applied";
                }
            }
            
        }        
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Send Successfully');", true);
        ViewState["datatable"] = null;
        dt = null;
        grdFacultyCoursePlan.DataSource = null;
        grdFacultyCoursePlan.DataBind();
        HeaderEnable(true);        
        btnSendForApproval.Visible = false;
        drpAcademicYear.SelectedIndex = 0;
        drpCourse.SelectedIndex = 0;
        drpSemester.DataSource = "";
        drpSemester.DataBind();
        drpSection.DataSource = "";
        drpSection.DataBind();
        drpSubject.DataSource = "";
        drpSubject.DataBind();
        txtSubjectType.Text = "";
    }
    protected void grdAppliedCoursePlan_SelectedIndexChanged(object sender, EventArgs e)
    {        
        GridViewRow row = grdAppliedCoursePlan.SelectedRow;
        //----------Don't change the cell No-------------
        hfDocumentNo.Value = row.Cells[0].Text; 
        drpAcademicYear.SelectedValue = row.Cells[1].Text;
        drpCourse.SelectedValue = row.Cells[2].Text;
        bindDrpSemesterList();
        drpSemester.SelectedValue = row.Cells[5].Text;
        bindSectionList();
        if (row.Cells[6].Text == "&nbsp;") row.Cells[6].Text = "";
        drpSection.SelectedValue= row.Cells[6].Text;
        bindGetSubjectList();
        drpSubject.SelectedValue = row.Cells[3].Text;
        txtSubjectType.Text = row.Cells[4].Text;
        bindGroupList();
        bindBatchList();
        if (row.Cells[7].Text == "&nbsp;") row.Cells[7].Text="";
        if (row.Cells[8].Text == "&nbsp;") row.Cells[8].Text = "";
        ddlGroup.SelectedValue = row.Cells[7].Text;
        ddlBatch.SelectedValue = row.Cells[8].Text;
        bindUnitCode();
        pnlunit.Visible = false;
        Clear();

    }
    protected void grdFacultyCoursePlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("DeleteItem"))
        {
            GridViewRow rowItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            int index = rowItem.RowIndex;

            HiddenField hfLineNo = (HiddenField)rowItem.FindControl("hfLineNo");
            if (hfLineNo.Value != "0")
            {
                SqlCommand cmd = new SqlCommand("proc_DeleteCoursePlanLine", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DocumentNo", hfDocumentNo.Value);
                cmd.Parameters.Add("@LineNo", hfLineNo.Value);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            DataTable dt = ViewState["datatable"] as DataTable;
            dt.Rows[index].Delete();
            dt.AcceptChanges();
            grdFacultyCoursePlan.DataSource = dt;
            grdFacultyCoursePlan.DataBind();
            ViewState["datatable"] = dt;
            if (grdFacultyCoursePlan.Rows.Count == 0)
            {
                HeaderEnable(true);
                btnSendForApproval.Visible = false;
                Clear();
            }
        }
    }
    protected void grdFacultyCoursePlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            string Status = (e.Row.FindControl("Status") as Label).Text;
                if (Status.ToUpper() != "APPROVED")
                {
                    if (btnSendForApproval.Visible == false && Status.ToUpper() == "PENDING")
                    {
                        btnSendForApproval.Visible = true;
                    }
                    if (Status.ToUpper() != "PENDING")
                    {
                        ImageButton imgDelete = (e.Row.FindControl("imgdelete") as ImageButton);
                        imgDelete.Visible = false;
                    }
                }
                else
                {
                    ImageButton imgDelete = (e.Row.FindControl("imgdelete") as ImageButton);
                    imgDelete.Visible = false;
                }
        } 
    }    
}