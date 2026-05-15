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

public partial class Faculty_CreateTimeTable : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               
                bindAcademicYear();
                bindGetSubjectList();// on 06-10 2106
                bindGetSubjectList1();
                bindDrpCourseList();
               
                String sDate = DateTime.Now.ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

                int day = datevalue.Day;
                int mn = datevalue.Month;
                int yy = datevalue.Year;
                DateTime now = DateTime.Now;
                DateTime lastDayOfLastMonth = now.Date.AddDays(-now.Day);
                int lastMonthLastDay = lastDayOfLastMonth.Day;
                //if (day <= 7)
                //{




                 //CalendarExtender2.StartDate = new DateTime(yy, mn, day);
                 //CalendarExtender2.EndDate = new DateTime(yy, 8, 1);
                 //CalendarExtender1.StartDate = new DateTime(yy, mn, day);
                 //CalendarExtender1.EndDate = new DateTime(yy, 8, 1);

                 // CalendarExtender2.StartDate = new DateTime(yy, 8, 1);
                 // CalendarExtender1.StartDate = new DateTime(yy,8, 1);
                    
                // clndAppliedate.StartDate = new DateTime(2023, mn-1, 1);
                // clndAppliedate.StartDate = new DateTime(2023, mn, 1);
                //}
                //else
                //{

                //    CalendarExtender1.StartDate = new DateTime(yy, mn, 1);
               //clndAppliedate.StartDate = new DateTime(yy, mn, day - 1);
                //}

            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("proc_GetSubjectTypeFromCourseSubjectLineNew", con);//proc_GetSubjectTypeFromCourseSubjectLine
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        lblSubjectType.Text = dt.Rows[0]["Subject Type"].ToString();
        txtSubjectClassification.Value = dt.Rows[0]["Subject Classification"].ToString();
    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSectionList();
        bindGetSubjectList();
        // bindRoomNo();
        bindGroupList();
        bindBatchList();
    }
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetCourseFromCourseWiseFacultyCollege_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@Academic", drpAcademicYear.SelectedItem.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
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
        drpAcademicYear1.DataSource = dt1;
        drpAcademicYear1.DataTextField = "Details";
        drpAcademicYear1.DataValueField = "No_";
        drpAcademicYear1.DataBind();
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
        try {
            SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role_TimeTable", con);//proc_GetSectionFromCourseWiseFaculty_Role
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (Chkdetained.Checked == true)
            {
                drpSection.DataSource = ds.Tables[1];
                drpSection.DataTextField = "Details";
                drpSection.DataValueField = "No_";
                drpSection.DataBind();
            }
            else
            {
                drpSection.DataSource = ds.Tables[0];
                drpSection.DataTextField = "Details";
                drpSection.DataValueField = "No_";
                drpSection.DataBind();
            }
        }catch(Exception ex) { }
        }
    public void bindGroupList()
    {
        try {
            SqlCommand cmd = new SqlCommand("sp_GetGroup_Role_TimeTable", con);//sp_GetGroup_Role
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (Chkdetained.Checked == true)
            {
                ddlGroup.DataSource = ds.Tables[1];
                ddlGroup.DataTextField = "Details";
                ddlGroup.DataValueField = "No_";
                ddlGroup.DataBind();
            }
            else
            {
                ddlGroup.DataSource = ds.Tables[0];
                ddlGroup.DataTextField = "Details";
                ddlGroup.DataValueField = "No_";
                ddlGroup.DataBind();
            }
        }catch(Exception ex) { }
        }
    //public void BindBatchForDetained()
    //{
    //    try {
    //        SqlCommand cmd = new SqlCommand("sp_getbatchForDetained", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
    //        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
    //        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
    //        DataTable dt = new DataTable();
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(dt);
    //        ddlBatch.DataSource = dt;
    //        ddlBatch.DataTextField = "Details";
    //        ddlBatch.DataValueField = "No_";
    //        ddlBatch.DataBind();
    //    }catch(Exception ex) { }
    //    }
    public void bindBatchList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_GetBatchFromCourseSemester_Role_TimeTable", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (Chkdetained.Checked == true)
            {
                ddlBatch.DataSource = ds.Tables[1];
                ddlBatch.DataTextField = "Details";
                ddlBatch.DataValueField = "No_";
                ddlBatch.DataBind();
            }
            else
            {
                ddlBatch.DataSource = ds.Tables[0];
                ddlBatch.DataTextField = "Details";
                ddlBatch.DataValueField = "No_";
                ddlBatch.DataBind();
            }
        }
        catch (Exception ex) { }
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

    public void bindGetSubjectList1()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromCourseSubjectLine1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject1.DataSource = dt;
        drpSubject1.DataTextField = "Details";
        drpSubject1.DataValueField = "No_";
        drpSubject1.DataBind();

    }



    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGetSubjectList();
        bindRoomNo();
    }
    public void bindRoomNo()
    {
        SqlCommand cmd = new SqlCommand("proc_GetRoomAllocation", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@DayNo", drpDayNo.SelectedValue);
        cmd.Parameters.Add("@HourNo", drpHourNo.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);// added on 06-10-2016--qry raised by deependra rastogi
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);//added on 24-01-2017--qry raised by deependra rastogi

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpRoomAllocation.DataSource = dt;
        drpRoomAllocation.DataTextField = "Details";
        drpRoomAllocation.DataValueField = "No_";
        drpRoomAllocation.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int HourNo = Convert.ToInt16(drpHourNoTo.SelectedValue);
        int HourNoTo = Convert.ToInt16(drpHourNoTo.SelectedValue);
        if (HourNo > HourNoTo)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Hour No.');", true);
            return;
        }
        SqlCommand cmdRA = new SqlCommand("[proc_RoomAllocationValidateOntimeTable]", con);
        cmdRA.CommandType = CommandType.StoredProcedure;
        cmdRA.Parameters.Add("@DayNo", drpDayNo.SelectedValue);
        cmdRA.Parameters.Add("@HourNo", drpHourNo.SelectedValue);
        cmdRA.Parameters.Add("@Fromdate", txtFromDate.Text);
        cmdRA.Parameters.Add("@Todate", txtToDate.Text);
        cmdRA.Parameters.Add("@RoomNo", drpRoomAllocation.SelectedValue);
        cmdRA.Parameters.Add("@HourNoTo", drpHourNoTo.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daRA = new SqlDataAdapter(cmdRA);
        DataTable dtRA = new DataTable();
        daRA.Fill(dtRA);
        //--------------------------03-10-2016----Free Room no---
        SqlCommand cmdCL = new SqlCommand("proc_GetSubjectClassificationNew", con);
        cmdCL.CommandType = CommandType.StoredProcedure;
        cmdCL.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmdCL.Parameters.Add("@Subject", drpSubject.SelectedValue);
        cmdCL.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmdCL);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        int Theory_count = 0;
        Theory_count = Convert.ToInt16(cmdCL.ExecuteScalar().ToString());

        //-----------03-10-2016--Grou,Batch---by ashu
        if (dtRA.Rows.Count > 0 && Theory_count > 0 && chkCombindClasss.Checked == false)
        //if (dtRA.Rows.Count > 0 )
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'This Room has been already assigned');", true);
        }
        else
        {
            // add on 20-10-2016 fro CCSIT---Start
            //DataTable dtEF = new DataTable();
            //if (dtRA.Rows.Count > 0 && Theory_count > 0 && chkCombindClasss.Checked == true)
            //{
            //    SqlCommand cmd1 = new SqlCommand("proc_CheckExistFacultyWithRoomNo", con);
            //    cmd1.CommandType = CommandType.StoredProcedure;                
            //    cmd1.Parameters.Add("@DayNo", drpDayNo.SelectedValue);
            //    cmd1.Parameters.Add("@HourNo", drpHourNo.SelectedValue);
            //    cmd1.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
            //    cmd1.Parameters.Add("@DateFrom", txtFromDate.Text);
            //    cmd1.Parameters.Add("@DateTo", txtToDate.Text);
            //    cmd1.Parameters.Add("@RoomNo", drpRoomAllocation.SelectedValue);
            //    cmd1.Parameters.Add("@FacultyCode", Session["uid"].ToString());  // add on 20-10-2016 fro CCSIT
            //    if (con.State == ConnectionState.Closed)
            //        con.Open();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //    da.Fill(dtEF);
            //}

            // add on 20-10-2016 fro CCSIT--end -----
            SqlCommand cmd1 = new SqlCommand("proc_CheckTimeSheetAvailability", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd1.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
            cmd1.Parameters.Add("@Section", drpSection.SelectedValue);
            cmd1.Parameters.Add("@DayNo", drpDayNo.SelectedValue);
            cmd1.Parameters.Add("@HourNo", drpHourNo.SelectedValue);
            cmd1.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
            cmd1.Parameters.Add("@DateFrom", txtFromDate.Text);
            cmd1.Parameters.Add("@DateTo", txtToDate.Text);
            cmd1.Parameters.Add("@Group", ddlGroup.SelectedValue);
            cmd1.Parameters.Add("@Batch", ddlBatch.SelectedValue);
            cmd1.Parameters.Add("@Subject", drpSubject.SelectedValue);  // --pharmacy
            cmd1.Parameters.Add("@FacultyCode", Session["uid"].ToString());  // add on 03-10-2016 for BDS
            cmd1.Parameters.Add("@HourNoTo", drpHourNoTo.SelectedValue); // add on 01-12-2016 
                                                                         //-----------05-08-2016--GrouP,Batch---by ashu
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int a = 1; a <= dt.Rows.Count; a++)
            {
                if (dt.Rows[a - 1]["Faculty Code"].ToString() == Session["uid"].ToString() && Theory_count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'This lecture has been already assigned');", true);
                    return;
                }
            }
            if (dt.Rows.Count > 0 && Theory_count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'This lecture has been already assigned');", true);
            }
            else
            {
                int Remedial = 0;
                if (chkExtraClass.Checked == true)
                    Remedial = 4;
                if (ChkRem.Checked == true)
                    Remedial = 1;
                else
                    Remedial = 0;
                //Combind Code  --100000,1000001,CL1000001+1,CL1000002+1,

                try
                {
                    //Commented (17-12-18)

                    //if (Chkdetained.Checked == true)
                    //{

                    //    if (drpSection.SelectedValue == "")
                    //    {
                    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Section Selection Is mandatory for Detained Batch!!');", true);
                    //        drpSection.Focus();
                    //        return;
                    //    }

                    //    if (ddlGroup.SelectedValue == "")
                    //    {
                    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Group Selection Is mandatory for Detained Batch!!');", true);
                    //        ddlGroup.Focus();
                    //        return;
                    //    }
                    //    if (ddlBatch.SelectedValue == "")
                    //    {
                    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Batch Selection Is mandatory for Detained Batch!!');", true);
                    //        ddlBatch.Focus();
                    //        return;
                    //    }
                    //}

                    //[proc_CreateTimeTableGeneration_CombindLect1]
                    SqlCommand cmd = new SqlCommand("[proc_CreateTimeTableGeneration_CombindLect1_Detained]", con);//proc_CreateTimeTableGeneration   //proc_CreateTimeTableGeneration_CombindLect1 15-11-18
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DayNo", drpDayNo.SelectedValue);
                    cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                    cmd.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
                    cmd.Parameters.Add("@HouNo", drpHourNo.SelectedValue);
                    cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                    cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                    cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
                    cmd.Parameters.Add("@Section", drpSection.SelectedValue);
                    cmd.Parameters.Add("@SubjectType", lblSubjectType.Text);
                    cmd.Parameters.Add("@FacultyName", Session["Fulname"].ToString());
                    cmd.Parameters.Add("@SubjectDescription", drpSubject.SelectedItem.ToString());
                    cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                    cmd.Parameters.Add("@RoomAllocation", drpRoomAllocation.SelectedValue);
                    cmd.Parameters.Add("@SubjectClassification", txtSubjectClassification.Value.ToString());
                    cmd.Parameters.Add("@FromDate", txtFromDate.Text);
                    cmd.Parameters.Add("@ToDate", txtToDate.Text);
                    cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);
                    cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue);
                    cmd.Parameters.Add("@HouNoTo", drpHourNoTo.SelectedValue);//added on 01-12-2016 ashu
                    cmd.Parameters.Add("@Remedial", Remedial);
                    if (ChkCombindID.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@CL", 1);
                    }
                    else { cmd.Parameters.AddWithValue("@CL", 0); }
                    if (Chkdetained.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Detained", 1);

                    }
                    else { cmd.Parameters.AddWithValue("@Detained", 0); }
                    if (con.State == ConnectionState.Closed)
                        con.Open(); cmd.CommandTimeout = 80000000;

                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
                    Clear();

                }
                catch (Exception ex) { }
            }
        }
    }
    public void Clear()
    {
        drpAcademicYear.SelectedIndex = 0;
        drpCourse.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlGroup.SelectedIndex = 0;
        drpSemester.DataSource = "";
        drpSemester.DataBind();
        drpSection.DataSource = "";
        drpSection.DataBind();
        drpSubject.DataSource = "";
        drpSubject.DataBind();
        lblSubjectType.Text = "";
        drpDayNo.SelectedIndex = 0;
        drpHourNo.SelectedIndex = 0;
        drpRoomAllocation.DataSource = "";
        drpRoomAllocation.DataBind();
        txtFromDate.Text = "";
        txtToDate.Text = "";
        chkCombindClasss.Checked = false;
        ChkCombindID.Checked = false;
        Chkdetained.Checked = false;
    }
    public void Clear1()
    {
        drpAcademicYear1.SelectedIndex = 0;
        
        drpSubject1.DataSource = "";
        drpSubject1.DataBind();
        lblSubjectType1.Text = "";
        drpDayNo1.SelectedIndex = 0;
        drpHourNo1.SelectedIndex = 0;
        drpRoomAllocation1.DataSource = "";
        drpRoomAllocation1.DataBind();
        txtFromDate1.Text = "";
        txtToDate1.Text = "";
        chkCombindClasss1.Checked = false;
        ChkCombindID1.Checked = false;
        
    }
    protected void drpHourNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindRoomNo();
    }
    protected void drpDayNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindRoomNo();
    }
    protected void drpHourNoTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindRoomNo();
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpDayNo.SelectedIndex = -1;
    }

    protected void Chkdetained_CheckedChanged(object sender, EventArgs e)
    {
        bindSectionList();
        bindGroupList();
        bindBatchList();
       
    }
    protected void ChkOpenElective_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkOpenElective.Checked == true)
        {
            pnlMain.Visible = false;
            pnlOpenElective.Visible = true;
        }
        else
        {
            pnlMain.Visible = true;
            pnlOpenElective.Visible = false;
        }
    }
    protected void drpAcademicYear1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpSubject1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("proc_GetSubjectTypeFromCourseSubjectLineNew1", con);//proc_GetSubjectTypeFromCourseSubjectLine
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SubjectCode", drpSubject1.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear1.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        lblSubjectType1.Text = ds.Tables[0].Rows[0]["Subject Type"].ToString();
        txtSubjectClassification1.Value = ds.Tables[0].Rows[0]["Subject Classification"].ToString();
        drpSection1.DataSource = ds.Tables[1];
        drpSection1.DataValueField = "Section";
        drpSection1.DataTextField = "Section";
        drpSection1.DataBind();
        bindRoomNoOpenElec();
    }
    public void bindRoomNoOpenElec()
    {
        SqlCommand cmd = new SqlCommand("proc_GetRoomAllocationOpenElec", con);
        cmd.CommandType = CommandType.StoredProcedure;
        
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear1.SelectedValue);
       

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpRoomAllocation1.DataSource = dt;
        drpRoomAllocation1.DataTextField = "Details";
        drpRoomAllocation1.DataValueField = "No_";
        drpRoomAllocation1.DataBind();
    }
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        int HourNo = Convert.ToInt16(drpHourNoTo1.SelectedValue);
        int HourNoTo = Convert.ToInt16(drpHourNoTo1.SelectedValue);
        if (HourNo > HourNoTo)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Check Hour No.');", true);
            return;
        }
        SqlCommand cmdRA = new SqlCommand("[proc_RoomAllocationValidateOntimeTable]", con);
        cmdRA.CommandType = CommandType.StoredProcedure;
        cmdRA.Parameters.Add("@DayNo", drpDayNo1.SelectedValue);
        cmdRA.Parameters.Add("@HourNo", drpHourNo1.SelectedValue);
        cmdRA.Parameters.Add("@Fromdate", txtFromDate1.Text);
        cmdRA.Parameters.Add("@Todate", txtToDate1.Text);
        cmdRA.Parameters.Add("@RoomNo", drpRoomAllocation1.SelectedValue);
        cmdRA.Parameters.Add("@HourNoTo", drpHourNoTo1.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daRA = new SqlDataAdapter(cmdRA);
        DataTable dtRA = new DataTable();
        daRA.Fill(dtRA);
        //--------------------------03-10-2016----Free Room no---
        SqlCommand cmdCL = new SqlCommand("proc_GetSubjectClassificationNew1", con);
        cmdCL.CommandType = CommandType.StoredProcedure;
       
        cmdCL.Parameters.Add("@Subject", drpSubject1.SelectedValue);
        cmdCL.Parameters.Add("@AcademicYear", drpAcademicYear1.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmdCL);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        int Theory_count = 0;
        Theory_count = Convert.ToInt16(cmdCL.ExecuteScalar().ToString());

        //-----------03-10-2016--Grou,Batch---by ashu
        if (dtRA.Rows.Count > 0 && Theory_count > 0 && chkCombindClasss.Checked == false)
        //if (dtRA.Rows.Count > 0 )
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'This Room has been already assigned');", true);
        }
        else
        {
           
            SqlCommand cmd1 = new SqlCommand("proc_CheckTimeSheetAvailabilityOpenElect", con);
            cmd1.CommandType = CommandType.StoredProcedure;
           
            cmd1.Parameters.Add("@DayNo", drpDayNo1.SelectedValue);
            cmd1.Parameters.Add("@HourNo", drpHourNo1.SelectedValue);
            cmd1.Parameters.Add("@AcademicYear", drpAcademicYear1.SelectedValue);
            cmd1.Parameters.Add("@DateFrom", txtFromDate1.Text);
            cmd1.Parameters.Add("@DateTo", txtToDate1.Text);
            
            cmd1.Parameters.Add("@Subject", drpSubject1.SelectedValue);  // --pharmacy
            cmd1.Parameters.Add("@FacultyCode", Session["uid"].ToString());  // add on 03-10-2016 for BDS
            cmd1.Parameters.Add("@HourNoTo", drpHourNoTo1.SelectedValue); // add on 01-12-2016 
            //-----------05-08-2016--GrouP,Batch---by ashu
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int a = 1; a <= dt.Rows.Count; a++)
            {
                if (dt.Rows[a - 1]["Faculty Code"].ToString() == Session["uid"].ToString() && Theory_count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'This lecture has been already assigned');", true);
                    return;
                }
            }
            if (dt.Rows.Count > 0 && Theory_count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'This lecture has been already assigned');", true);
            }
            else
            {
                int Remedial = 0;
                if (chkExtraClass1.Checked == true)
                    Remedial = 4;
                else
                    Remedial = 0;
                //Combind Code  --100000,1000001,CL1000001+1,CL1000002+1,

                try
                {
                    
                    SqlCommand cmd = new SqlCommand("[proc_CreateTimeTableGeneration_CombindLect1_DetainedOpen]", con);//proc_CreateTimeTableGeneration   //proc_CreateTimeTableGeneration_CombindLect1 15-11-18
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DayNo", drpDayNo1.SelectedValue);
                    cmd.Parameters.Add("@CourseCode", "");
                    cmd.Parameters.Add("@SemesterYear", "");
                    cmd.Parameters.Add("@HouNo", drpHourNo1.SelectedValue);
                    cmd.Parameters.Add("@SubjectCode", drpSubject1.SelectedValue);
                    cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                    cmd.Parameters.Add("@AcademicYear", drpAcademicYear1.SelectedValue);
                    if (drpSection1.SelectedIndex == 0)
                    {
                        cmd.Parameters.Add("@Section", "");
                    }
                    else
                    {
                        cmd.Parameters.Add("@Section", drpSection1.SelectedValue);
                    }
                    cmd.Parameters.Add("@SubjectType", lblSubjectType1.Text);
                    cmd.Parameters.Add("@FacultyName", Session["Fulname"].ToString());
                    cmd.Parameters.Add("@SubjectDescription", drpSubject1.SelectedItem.ToString());
                    cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
                    cmd.Parameters.Add("@RoomAllocation", drpRoomAllocation1.SelectedValue);
                    cmd.Parameters.Add("@SubjectClassification", txtSubjectClassification1.Value.ToString());
                    cmd.Parameters.Add("@FromDate", txtFromDate1.Text);
                    cmd.Parameters.Add("@ToDate", txtToDate1.Text);
                    cmd.Parameters.Add("@Group", "");
                    cmd.Parameters.Add("@Batch", "");
                    cmd.Parameters.Add("@HouNoTo", drpHourNoTo1.SelectedValue);//added on 01-12-2016 ashu
                    cmd.Parameters.Add("@Remedial", "");
                    if (ChkCombindID1.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@CL", 1);
                    }
                    else { cmd.Parameters.AddWithValue("@CL", 0); }
                    if (Chkdetained.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Detained", 1);

                    }
                    else { cmd.Parameters.AddWithValue("@Detained", 0); }
                    if (con.State == ConnectionState.Closed)
                        con.Open(); cmd.CommandTimeout = 80000000;

                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
                    Clear1();

                }
                catch (Exception ex) { }
            }
        }

    }
    protected void drpSection1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}