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

public partial class Faculty_StudentAttendanceMark : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    DataTable dt2 = new DataTable();
    static int cnt = 0;  static String No_ = "";  static bool Save = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                


                lblFacultyCode.Text = Session["uid"].ToString();
               // bindDrpCourseList();
                lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();               
                txtDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                bindDrpCourseList();  //added on 24 feb 2017
                bindAcademicYear();
            }
        }
        catch(Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }    
    
    protected void chkboxEditAttendance_CheckedChanged(object sender, EventArgs e)
    {
        if (chkboxEditAttendance.Checked == true)
        {
            for (int i = 0; i < cnt; i++)
            {
                CheckBox col = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox1stAttendance");
                CheckBox col1 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox2ndAttendance");
                CheckBox col2 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox3rdAttendance");
                col.Enabled = true;
                col1.Enabled = true;
                col2.Enabled = true;
            }
        }
        else
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
    }
    public void bindDrpCourseList()
    {
        DataTable dt = new DataTable();
       // dt = FDL.GetCourseList(lblFacultyCode.Text);    //21-12-2016
      //  dt = FDL.GetCourseList(lblFacultyCode.Text, Session["GlobalDimension1Code"].ToString());//comment on 24-02-2017
        dt = FDL.GetCourseListFromTimeTable(lblFacultyCode.Text, Session["GlobalDimension1Code"].ToString(), txtDate.Text);  // added on 24-02-017 by ashu
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_Role1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID1", lblFacultyCode.Text);
        cmd.Parameters.Add("@ID", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Date",Convert.ToDateTime(txtDate.Text));
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
        SqlCommand cmd = new SqlCommand("proc_GetSectionFromCourseWiseFaculty_Role1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
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
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTimeTable", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();
    }

    //-------------add by ashu--on 25-08-2016
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
        SqlCommand cmd = new SqlCommand("sp_GetBatch_Role", con);
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
    //------------------------------------
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
      //  ShowRemedialClass();
    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSectionList();
        bindGetSubjectList();
        bindGroupList();// add by ashu on 25-08-2016
        bindBatchList();// add by ashu on 25-08-2016

       // ShowRemedialClass();
    }   
    DataTable dt12 = new DataTable();
    public void bindGrid()
    {
        string remedial = "";
        if (chkBoxExtraClass.Checked == true)
        {
            remedial = "1";
        }
        if (chkBoxExtraClass.Checked == false)
        {
            remedial = "0";
        }
        DateTime dt11 = new DateTime();
        dt11 = Convert.ToDateTime(txtDate.Text);
        string date = dt11.ToString("MM-dd-yyyy");

        SqlCommand cmd1 = new SqlCommand("proc_GetStudentDetailsForMarkAttendance1_", con);  //ok on live
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd1.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd1.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd1.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd1.Parameters.Add("@LectureNo", drpLecture.SelectedItem.ToString());
        cmd1.Parameters.Add("@Date", date);
        cmd1.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd1.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd1.Parameters.Add("@Group", ddlGroup.SelectedValue);  //----Add Group on 25-08-2016 by ashu
        cmd1.Parameters.Add("@Batch", ddlBatch.SelectedValue); //----Add Batch on 25-08-2016 by ashu
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
            pnlGrid.Visible = true;
            pnlCheckBox.Visible = true;
            btnSubmit.Visible = true;
            lblMessage.Visible = false;

            SqlCommand cmd = new SqlCommand("proc_GetPreviousAttendanceOfStudent2_", con);  //ok on live
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
            cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
            cmd.Parameters.Add("@Section", drpSection.SelectedValue);
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
            cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
            cmd.Parameters.Add("@LectureNo", drpLecture.SelectedValue);
            cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);  //----Add Group on 25-08-2016 by ashu
            cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue); //----Add Batch on 25-08-2016 by ashu
            cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue); //----Add Batch on 25-08-2016 by ashu

            cmd.Parameters.Add("@Remedial", remedial.Trim());
            cmd.CommandTimeout = 500000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cnt = dt.Rows.Count;

            grdAttendanceDetails.DataSource = dt;
            grdAttendanceDetails.DataBind();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    CheckBox rb = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox1stAttendance");
                    if (dt.Rows[i]["AAfterPrev"].ToString() == "0")
                    {
                        rb.Checked = true;
                    }
                    else
                        rb.Checked = false;

                    CheckBox rb1 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox2ndAttendance");
                    if (dt.Rows[i]["AfterPrev"].ToString() == "0")
                    {
                        rb1.Checked = true;
                    }
                    else
                        rb1.Checked = false;

                    CheckBox rb2 = (CheckBox)grdAttendanceDetails.Rows[i].Cells[0].FindControl("chkbox3rdAttendance");
                    if (dt.Rows[i]["prev"].ToString() == "0")
                    {
                        rb2.Checked = true;
                    }
                    else
                        rb2.Checked = false;
                }
                SqlCommand cmd2 = new SqlCommand("proc_GetDateAndHourForMarkAtt_", con);//ok
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                cmd2.Parameters.Add("@Semester", drpSemester.SelectedValue);
                cmd2.Parameters.Add("@Section", drpSection.SelectedValue);
                cmd2.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                cmd2.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                cmd2.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
                cmd2.Parameters.Add("@LectureNo", drpLecture.SelectedValue);
                cmd2.Parameters.Add("@Group", ddlGroup.SelectedValue);  //----Add Group on 25-08-2016 by ashu
                cmd2.Parameters.Add("@Batch", ddlBatch.SelectedValue); //----Add Batch on 25-08-2016 by ashu
                cmd2.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue); //----Add Batch on 28-08-2016 by ashu
                cmd2.Parameters.Add("@Remedial", remedial.Trim());
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
        }
        if (grdAttendanceDetails.Rows.Count > 0)
        {
            EnableDisable(false);
        }
        else
        { EnableDisable(true); }
    }
    public void BindTable()
    {        
        for (int i = 0; i < grdAttendanceDetails.Columns.Count; i++)
        {
            dt12.Columns.Add(grdAttendanceDetails.HeaderRow.Cells[i].Text);
        }
        foreach (GridViewRow row in grdAttendanceDetails.Rows)
        {
            DataRow dr = dt12.NewRow();
            for (int j = 0; j < grdAttendanceDetails.Columns.Count; j++)
            {
                CheckBox rb = (CheckBox)row.FindControl("chkboxAttendance");
                if (j == 6)
                {
                    if (rb.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                CheckBox rb1 = (CheckBox)row.FindControl("chkbox1stAttendance");
                if (j == 3)
                {
                    if (rb1.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                CheckBox rb2 = (CheckBox)row.FindControl("chkbox2ndAttendance");
                if (j == 4)
                {
                    if (rb2.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                CheckBox rb3 = (CheckBox)row.FindControl("chkbox3rdAttendance");
                if (j == 5)
                {
                    if (rb3.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                dr[grdAttendanceDetails.HeaderRow.Cells[j].Text] = row.Cells[j].Text;
            }
            dt12.Rows.Add(dr);
        }
    }    
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //------------------------------04-12-2016----------------------
        
        DataTable dt = new DataTable();
        dt =(DataTable) Session["RemedialHourData"];
        DataView dv = new DataView(dt);
        dv.RowFilter = "Details ='" + drpLecture.SelectedValue.Trim() + "'";
        //dv.RowFilter = "Details=8";

       // int val = (int)dv[0]["Remedial"].ToString();
      
       if (dv[0]["Remedial"].ToString() == "1")
        {
            chkBoxExtraClass.Checked = true;
            chkBoxExtraClass.Enabled = false;
        }
        else
        {
            chkBoxExtraClass.Checked = false;
            chkBoxExtraClass.Enabled = false;

        }
        
        //--------------------------------------------------






        chkMultiplaeAttendance.Visible = true; //All subject  // 11 nov 2016 
       // if (drpCourse.SelectedValue == "BDS-001" || drpCourse.SelectedValue == "BARCH-001")  // 11 nov 2016 --BARCH-001
       //{
       //    chkMultiplaeAttendance.Visible=true;
       //}
       //else
       //{
       //    // chkMultiplaeAttendance.Visible=false;
       //    chkMultiplaeAttendance.Visible = true; //All subject 
       //}
        chkboxEditAttendance.Checked = false;
        
        bindGrid();
        Save = true;
        if (dv[0]["Remedial"].ToString() == "1")
        {
            grdAttendanceDetails.Columns[7].Visible = false;
        }

        if (dv[0]["Remedial"].ToString() == "0")
        {
            grdAttendanceDetails.Columns[7].Visible = true;
        }
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FDL.GetSubjectTypebySemester(drpCourse.SelectedValue, drpSemester.SelectedItem.ToString(),drpSubject.SelectedItem.ToString());//comment on 29 aug 2016
        lblSubjectType.Text = FDL.GetSubjectTypebyCourseSubject(drpCourse.SelectedValue, drpSubject.SelectedValue);
        
        bindLecture();
        bindUnit();

        
    }
    public void Blank()
    {
        EnableDisable(true);
        drpCourse.SelectedIndex = 0;
        lblSubjectType.Text = "";
        drpSubject.DataSource = "";
        drpSubject.DataBind();       
        drpSemester.DataSource = "";
        drpSemester.DataBind();
        drpSection.DataSource = "";
        drpSection.DataBind();
        drpLecture.DataSource = "";
        drpLecture.DataBind();
        drpUnit.DataSource = "";
        drpUnit.DataBind();        
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
        SqlCommand cmd = new SqlCommand("proc_GetLectureFromTimeTable", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd.Parameters.Add("@FacultyCode", lblFacultyCode.Text);
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        //Added on 20-10-2016 --for filteration from [TMU$Time Table Generation - COL]
        cmd.Parameters.Add("@Group", ddlGroup.SelectedValue);
        cmd.Parameters.Add("@Batch", ddlBatch.SelectedValue);
       
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpLecture.DataSource = dt;
       
        drpLecture.DataTextField = "Details";
        drpLecture.DataValueField = "No_";
        drpLecture.DataBind();
        Session["RemedialHourData"] = dt;
        Boolean Rem = false, Gen=false ; 
        for (int i = 0; i < dt.Rows.Count; i++)
        {
           
            if (dt.Rows[i]["Remedial"].ToString() == "1")
            {
                
                Rem = true;
            }
            if (dt.Rows[i]["Remedial"].ToString() == "0")
            {
                //chkBoxExtraClass.Enabled = false;
                //chkBoxExtraClass.Checked = false;
                Gen = true;
            }
        }
        if (Rem== true && Gen==true)
        {
            chkBoxExtraClass.Enabled = true;
            chkBoxExtraClass.Checked = false;
        }
        if (Rem == true && Gen != true)
        {
            chkBoxExtraClass.Enabled = false;
            chkBoxExtraClass.Checked = true;
        }
        if (Rem != true && Gen == true)
        {
            chkBoxExtraClass.Enabled = false;
            chkBoxExtraClass.Checked = false;
        }

        
    }
    //public void bindUnit()
    //{
    //    DataTable dt = new DataTable();
    //    dt = FDL.GetUnitForMarkAttendance(drpSubject.SelectedValue, drpAcademicYear.SelectedValue);
    //    drpUnit.DataSource = dt;
    //    drpUnit.DataTextField = "Details";
    //    drpUnit.DataValueField = "No_";
    //    drpUnit.DataBind();
    //}
    public void bindUnit()
    {
        //--------------------------05-10-2016----Free Room no---
        SqlCommand cmdCL = new SqlCommand("proc_GetSubjectClassification", con);
        cmdCL.CommandType = CommandType.StoredProcedure;
        cmdCL.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmdCL.Parameters.Add("@Subject", drpSubject.SelectedValue);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmdCL);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        int Theory_count = 0;
        Theory_count = Convert.ToInt16(cmdCL.ExecuteScalar().ToString());
        con.Close();
        //-----------05-10-2016--Grou,Batch---by ashu--ENd

        DataTable dt = new DataTable();
        dt = FDL.GetUnitForMarkAttendance(drpCourse.SelectedValue, drpSubject.SelectedValue, drpAcademicYear.SelectedValue);
        drpUnit.DataSource = dt;
        drpUnit.DataTextField = "Details";
        drpUnit.DataValueField = "No_";
        drpUnit.DataBind();
        //-----------05-10-2016--Grou,Batch---by ashu --Start 
        if (Theory_count == 0 && dt.Rows.Count == 1)
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("No_", typeof(string));
            dt1.Columns.Add("Details", typeof(string));
            dt1.Rows.Add("No Unit", "");
            drpUnit.DataSource = dt1;
            drpUnit.DataTextField = "Details";
            drpUnit.DataValueField = "No_";
            drpUnit.DataBind();
        }
        //-----------05-10-2016--Group,Batch---by ashu--ENd
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        bindDrpCourseList();  //added on 24 feb 2017
        bindLecture();
        bindGetSubjectList();
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
    public void UpdateAttendance()
    {
        int count1 = dt2.Rows.Count > 3 ? 3 : dt2.Rows.Count;
        for (int i = 0; i < count1 && i < 3; i++)
        {
            string DocumentNo = FDL.GetNextStudentMarkAttendanceNumber();
            int AttendanceType = 0;
            if (chkBoxExtraClass.Checked == true)
                AttendanceType = 1;
            SqlCommand cmd1 = new SqlCommand("proc_GetStudentDetailsForUpdateAttendance_", con); //ok
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@CourseCode",drpCourse.SelectedValue);
            cmd1.Parameters.Add("@Semesteryear",drpSemester.SelectedValue);
            cmd1.Parameters.Add("@Section",drpSection.SelectedValue);
            cmd1.Parameters.Add("@FacultyCode",lblFacultyCode.Text);
            cmd1.Parameters.Add("@SubjectCode",drpSubject.SelectedValue);
            cmd1.Parameters.Add("@Date",Convert.ToDateTime(dt2.Rows[i]["Date"]).ToString("MM-dd-yyyy"));
            cmd1.Parameters.Add("@LectureNo",dt2.Rows[i]["HourNo"].ToString());
            cmd1.Parameters.AddWithValue("@Group", ddlGroup.SelectedValue);//ashu 25-08-2016
            cmd1.Parameters.AddWithValue("@Batch", ddlBatch.SelectedValue);//ashu 25-08-2016
            cmd1.CommandTimeout = 300;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            for (int j = 0; j < dt12.Rows.Count; j++)
            {
               
                
                if (dt1.Rows.Count == 0)   //Add by ashu on 26-08-2016
                {
                    if (con.State == ConnectionState.Closed) { con.Open(); }
                SqlCommand cmd = new SqlCommand("[dbo].[proc_insertStudentAttendanceHeaderAndLine]", con); //ok new
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@DocumentNo", DocumentNo);
                cmd.Parameters.AddWithValue("@AcademicYear", drpAcademicYear.SelectedValue);
                cmd.Parameters.AddWithValue("@Course", drpCourse.SelectedValue);
                cmd.Parameters.AddWithValue("@Semester", drpSemester.SelectedValue);
                cmd.Parameters.AddWithValue("@Section", drpSection.SelectedValue);
                cmd.Parameters.AddWithValue("@Group", ddlGroup.SelectedValue);
                cmd.Parameters.AddWithValue("@Batch", ddlBatch.SelectedValue);
                cmd.Parameters.AddWithValue("@SubjectCode", drpSubject.SelectedValue);
                cmd.Parameters.AddWithValue("@SubjectType", lblSubjectType.Text);
                cmd.Parameters.AddWithValue("@Hour", dt2.Rows[i]["HourNo"].ToString());
                cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(dt2.Rows[i]["Date"].ToString()));
                cmd.Parameters.AddWithValue("@FacultyCode", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@AttendanceType", AttendanceType);
                cmd.Parameters.AddWithValue("@Unit", drpUnit.SelectedValue);
                cmd.Parameters.AddWithValue("@Topic", txtTopic.Text);
                cmd.Parameters.AddWithValue("@CollegeCode", Session["GlobalDimension1Code"].ToString());

                cmd.Parameters.AddWithValue("@EnrollmentNo", "ex");
                cmd.Parameters.AddWithValue("@StudentName", dt12.Rows[j]["Student Name"].ToString());
                cmd.Parameters.AddWithValue("@StudentNo", dt12.Rows[j]["Student No"].ToString());
                cmd.Parameters.AddWithValue("@Attendance", dt12.Rows[j][5 - i].ToString());// attendance absent/persent                
                cmd.CommandTimeout = 300;
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
                    cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
                    cmd.Parameters.Add("@Section", drpSection.SelectedValue);
                    cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
                    cmd.Parameters.Add("@Hour", dt2.Rows[i]["HourNo"].ToString());
                    cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
                    cmd.Parameters.AddWithValue("@Group", ddlGroup.SelectedValue);//ashu 25-08-2016
                    cmd.Parameters.AddWithValue("@Batch", ddlBatch.SelectedValue);//ashu 25-08-2016
                    cmd.CommandTimeout = 300;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

           
        }
    }
    public void bindGridHeader()
    {
        string remedial = "";
        if (chkBoxExtraClass.Checked == true)
        {
            remedial = "1";
        }
        if (chkBoxExtraClass.Checked == false)
        {
            remedial = "0";
        }



        SqlCommand cmd2 = new SqlCommand("proc_GetDateAndHourForMarkAtt_", con); //ok
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd2.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd2.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd2.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd2.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        cmd2.Parameters.Add("@Date", Convert.ToDateTime(txtDate.Text));
        cmd2.Parameters.Add("@LectureNo", drpLecture.SelectedValue);
        cmd2.Parameters.Add("@Group", ddlGroup.SelectedValue);  //----Add Group on 25-08-2016 by ashu
        cmd2.Parameters.Add("@Batch", ddlBatch.SelectedValue); //----Add Batch on 25-08-2016 by ashu
        cmd2.Parameters.Add("@AcademicYear", drpAcademicYear.SelectedValue); //----Add Academic Year on 30-08-2016 by ashu
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
  //  protected void btnSave1_Click(object sender, EventArgs e)
  //  {       
  //      if (Save == true)
  //      {
  //          btnSave1.Enabled = false;
  //          bindGridHeader();
  //          btnSubmit.Enabled = false;
  //          BindTable();
  //          if (chkboxEditAttendance.Checked == true)
  //          {
  //              UpdateAttendance();
  //          }
  //          int AttendanceType = 0;
  //          if (chkBoxExtraClass.Checked == true)
  //              AttendanceType = 1;
  //          //FDL.InsertStudentAttendanceLine(dt12, drpCourse.SelectedValue, FDL.GetNextStudentMarkAttendanceNumber(), lblSubjectType.Text, drpSemester.SelectedItem.ToString(),
  //          //    drpSection.SelectedValue, txtDate.Text, drpAcademicYear.SelectedValue, drpSubject.SelectedValue, Session["uid"].ToString(), Convert.ToInt16(drpLecture.SelectedItem.ToString()), Session["GlobalDimension1Code"].ToString(), ddlGroup.SelectedValue, ddlBatch.SelectedValue);//ASHU---25-08-2016
  //          //FDL.InsertStudentAttendanceHeader(FDL.GetNextStudentMarkAttendanceNumber(), lblSubjectType.Text, AttendanceType, lblFacultyCode.Text, drpCourse.SelectedValue,
  //          //    drpSemester.SelectedItem.ToString(), drpSection.SelectedValue, txtDate.Text, drpAcademicYear.SelectedValue, drpSubject.SelectedValue, Convert.ToInt16(drpLecture.Text), drpUnit.SelectedValue, txtTopic.Text, drpSubject.SelectedItem.ToString(), Session["GlobalDimension1Code"].ToString(), ddlGroup.SelectedValue, ddlBatch.SelectedValue);  //ASHU---25-08-2016

  //         string Result=FDL.InsertStudentAttendanceHeaderAndLine("", drpAcademicYear.SelectedValue, drpCourse.SelectedValue, drpSemester.SelectedItem.ToString(), drpSection.SelectedValue,
  //ddlGroup.SelectedValue, ddlBatch.SelectedValue, drpSubject.SelectedValue, lblSubjectType.Text, Convert.ToInt16(drpLecture.Text), txtDate.Text, lblFacultyCode.Text, AttendanceType, drpUnit.SelectedValue, txtTopic.Text, Session["GlobalDimension1Code"].ToString(), dt12);  //ASHU---25-08-2016
          
  //          // FDL.updateSeriesLineLastNoUsed(FDL.GetNextStudentMarkAttendanceNumber());
  //         if (Result != "Error")
  //         {
  //             lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
  //             ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);

  //             Blank();
  //             lblMessage.Visible = false;
  //             btnSubmit.Enabled = true;
  //             btnSave1.Enabled = true;
  //             Save = false;
  //             System.Threading.Thread.Sleep(2000);
  //         }
  //         else
  //         {
  //             ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Someting went wrong Contact Administrator');", true);
  //         }
  //      }
  //      else
  //      {
        
  //      }
  //  }
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        if (Save == true)
        {
            btnSave1.Enabled = false;
            bindGridHeader();
            btnSubmit.Enabled = false;
            BindTable();
            if (chkboxEditAttendance.Checked == true)
            {
                UpdateAttendance();
            }
            int AttendanceType = 0;
            if (chkBoxExtraClass.Checked == true)
                AttendanceType = 1;

            int LectureNo = Convert.ToInt32(drpLecture.Text);
            int i = 1;
            if (chkMultiplaeAttendance.Checked == true)
            {
                i = 1;
            }
            else
            {
                i = LectureNo;
            }
            for (i = 1; i <= LectureNo; i++)
            {
                string v = i.ToString();
                ListItem selectedListItem = drpLecture.Items.FindByValue(v);
                if (selectedListItem != null)
                {                  
                    FDL.InsertStudentAttendanceHeaderAndLine("", drpAcademicYear.SelectedValue, drpCourse.SelectedValue, drpSemester.SelectedItem.ToString(), drpSection.SelectedValue,
          ddlGroup.SelectedValue, ddlBatch.SelectedValue, drpSubject.SelectedValue, lblSubjectType.Text, i, txtDate.Text, lblFacultyCode.Text, AttendanceType, drpUnit.SelectedValue, txtTopic.Text, Session["GlobalDimension1Code"].ToString(), dt12);  //ASHU---25-08-2016                
                }
            }
            lblNo.Text = FDL.GetNextStudentMarkAttendanceNumber();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
            //bindGrid();
            Blank();
            lblMessage.Visible = false;
            btnSubmit.Enabled = true;
            btnSave1.Enabled = true;
            Save = false;
            System.Threading.Thread.Sleep(2000);
        }
        else
        {

        }
    }
    protected void drpAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGetSubjectList();
      //  ShowRemedialClass();
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
        cmd.Parameters.Add("@SubjectCode",drpSubject.SelectedValue);
        cmd.Parameters.Add("@CourseCode",drpCourse.SelectedValue);
        cmd.Parameters.Add("@Semester",drpSemester.SelectedValue);
        cmd.Parameters.AddWithValue("@Group", ddlGroup.SelectedValue);//ashu 25-08-2016
        cmd.Parameters.AddWithValue("@Batch", ddlBatch.SelectedValue);//ashu 25-08-2016
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
        ddlGroup.Enabled = TF;
        ddlBatch.Enabled = TF;
        drpSubject.Enabled = TF;
        drpLecture.Enabled = TF;
        drpUnit.Enabled = TF;
        txtTopic.Enabled = TF;        
    }
    protected void chkMultiplaeAttendance_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMultiplaeAttendance.Checked == true)
        {
            chkboxEditAttendance.Checked = false;
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
    }


    public void ShowRemedialClass()
    {

       Connection con = new Connection();
   ServicePoratal PortalCon = new ServicePoratal();
   SqlDataReader dr = PortalCon.Show_RemedialDataforClassForHeader(Session["Company"].ToString().Trim(), drpCourse.SelectedValue.Trim(), drpSemester.SelectedValue.Trim(), drpSection.SelectedValue.Trim(), drpSubject.SelectedValue.Trim(), drpAcademicYear.SelectedValue.Trim(), Session["uid"].ToString(), txtDate.Text.Trim());
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            PortalCon.DisConnect();
            chkBoxExtraClass.Checked = true;
            chkBoxExtraClass.Enabled = false;
        }
        else
        {
            dr.Close();
            PortalCon.DisConnect();
            chkBoxExtraClass.Checked = false;
            chkBoxExtraClass.Enabled = false;
         
        }

        bindLecture();
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
}