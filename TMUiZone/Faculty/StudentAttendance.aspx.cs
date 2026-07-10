using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Faculty_StudentAttendance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
    string EntryNo = "";
    DataTable dt2 = new DataTable();
    DataTable dtStudent = new DataTable();
    static int cnt = 0; static String No_ = ""; //static bool Save = false;
    static String No_D = ""; static int cntD = 0; static bool SaveD = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                SqlCommand cmdT = new SqlCommand("select [Faculty Code] from[TMU$Course Subject Line - COLLEGE] where[Faculty Code] = '" + Session["uid"].ToString() + "' and[Course Code] in ('NUR-008', 'NUR-009')", con);
                DataTable dtOrderT = new DataTable();
                SqlDataAdapter daT = new SqlDataAdapter(cmdT);
                daT.Fill(dtOrderT);
                if (Session["GlobalDimension1Code"].ToString() == "TMNS" || Session["GlobalDimension1Code"].ToString() == "TMSN" || Session["uid"].ToString()=="TMU00937" || Session["uid"].ToString()=="TMU03630" || Session["uid"].ToString()=="TMU07571" || Session["uid"].ToString()=="TMU07932" || Session["uid"].ToString()=="TMU05956" || Session["uid"].ToString()=="TMU06222" || Session["uid"].ToString()=="TMU09690")
                {
                    pnlApproval.Visible = true;
                    pnlmsg.Visible = false;

                }
                else
                {
                    pnlmsg.Visible = true;
                    pnlApproval.Visible = false;
                }

                //pnlApproval.Visible = true;


                if (Context.Request.Browser.IsMobileDevice)
                {

                }
                else
                {
                    btnSubmit.Width = 60;
                    btnSubmit.Height = 30;
                }
                //binddatatable();
                bindDrpCourseList();
                bindDrpSemesterList();

            }
            catch (Exception ex)
            {
                Response.Redirect("~/Default.aspx");
            }
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
        con.Open();
        da.Fill(dt);
        con.Close();
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }


    public void binddatatableOpen()
    {
        SqlCommand cmd = new SqlCommand("[proc_GetTimeTableUAT]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@Course", "");
        cmd.Parameters.Add("@Sem", "");
        cmd.CommandTimeout = 240;
        DataTable dt = new DataTable();
        da.Fill(dt);


        grdTimetable.DataSource = dt;
        grdTimetable.DataBind();
    }

    public void binddatatable(string Course,string Sem)
    {
        SqlCommand cmd = new SqlCommand("[proc_GetTimeTableUAT]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        cmd.Parameters.Add("@Course", Course);
        cmd.Parameters.Add("@Sem", Sem);
        cmd.CommandTimeout = 240;
        DataTable dt = new DataTable();
        da.Fill(dt);


        grdTimetable.DataSource = dt;
        grdTimetable.DataBind();
    }
    public void BindStudent(int EntryNo)
    {
        Session["EntryNo"] = EntryNo;
        SqlCommand cmd = new SqlCommand("[proc_GetStudentForAttendance]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@EntryNo", EntryNo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            SqlCommand cmdM = new SqlCommand("[proc_GetPreviousAttendanceOfStudent2_shubham]", con); string st = "";
            cmdM.CommandType = CommandType.StoredProcedure;
            cmdM.Parameters.Add("@CourseCode", dt.Rows[0]["Course Code"].ToString());
            if (dt.Rows[0]["Semester Code"].ToString() != "")
            {
                cmdM.Parameters.Add("@Semester", dt.Rows[0]["Semester Code"].ToString());
            }
            else
            {
                cmdM.Parameters.Add("@Semester", dt.Rows[0]["Year"].ToString());
            }
            cmdM.Parameters.Add("@Section", dt.Rows[0]["Section Code"].ToString());
            cmdM.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmdM.Parameters.Add("@SubjectCode", dt.Rows[0]["Subject Code"].ToString());
            cmdM.Parameters.Add("@Date", Convert.ToDateTime(dt.Rows[0]["Attendance Date"].ToString()));
            cmdM.Parameters.Add("@LectureNo", dt.Rows[0]["Hour No"].ToString());
            cmdM.Parameters.Add("@Group", dt.Rows[0]["Group"].ToString());
            cmdM.Parameters.Add("@Batch", dt.Rows[0]["Batch"].ToString());
            cmdM.Parameters.Add("@AcademicYear", dt.Rows[0]["Academic Year"].ToString());
            cmdM.Parameters.Add("@Remedial", "0");
            cmdM.CommandTimeout = 500000;
            SqlDataAdapter daM = new SqlDataAdapter(cmdM);
            DataTable dtM = new DataTable();
            try
            {
                daM.Fill(dtM);
            }
            catch (Exception ex) { }
            grdAttendanceDetails.DataSource = dtM;
            grdAttendanceDetails.DataBind();
            btnSubmit.Visible = true;
            //lblstu.Visible = true;
            lblUnit.Visible = true;
            drpUnit.Visible = true;
        }
        else
        {
            grdAttendanceDetails.DataSource = "";
            grdAttendanceDetails.DataBind();
            btnSubmit.Visible = false;
            //lblstu.Visible = false;
            lblUnit.Visible = false;
            drpUnit.Visible = false;
        }
    }
    DataTable dt12 = new DataTable();
    public void BindTable()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i == 0)
                dt12.Columns.Add("Sr. No.");
            else if (i == 1)
                dt12.Columns.Add("Roll No");
            else if (i == 2)
                dt12.Columns.Add("Student No");
            else if (i == 3)
                dt12.Columns.Add("L1");
            else if (i == 4)
                dt12.Columns.Add("L2");
            else if (i == 5)
                dt12.Columns.Add("L3");
            else if (i == 6)
                dt12.Columns.Add("Today");
            else if (i == 7)
                dt12.Columns.Add("Student Name");
            else if (i == 8)
                dt12.Columns.Add("Percentage");

        }
        foreach (GridViewRow row in grdAttendanceDetails.Rows)
        {
            DataRow dr = dt12.NewRow();
            for (int j = 0; j < 9; j++)
            {
                CheckBox rb = (CheckBox)row.FindControl("chkboxAttendance");
                Label lblName = (Label)row.FindControl("lblName");
                Label lblStudentNo = (Label)row.FindControl("lblNo");
                if (j == 3)
                {
                    if (rb.Checked == true)
                    {
                        row.Cells[j].Text = "0";
                    }
                    else
                        row.Cells[j].Text = "1";
                }
                if (j == 0)
                    dr["Sr. No."] = row.Cells[j].Text;
                else if (j == 1)
                    dr["Student Name"] = lblName.Text;
                else if (j == 2)
                    dr["Roll No"] = "";
                else if (j == 3)
                    dr["Today"] = row.Cells[j].Text;
                else if (j == 4)
                    dr["Student No"] = lblStudentNo.Text;
                else if (j == 5)
                    dr["L1"] = 1;
                else if (j == 6)
                    dr["L2"] = 1;
                else if (j == 7)
                    dr["L3"] = 1;
                else if (j == 8)
                    dr["Percentage"] = "";




            }
            dt12.Rows.Add(dr);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        BindTable();
        SqlCommand cmd = new SqlCommand("[proc_GetStudentForAttendance]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@EntryNo", Session["EntryNo"].ToString());
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            try
            {
                FDL.InsertStudentAttendanceHeaderAndLine(dt.Rows[0]["Academic Year"].ToString(), dt.Rows[0]["Course Code"].ToString(), dt.Rows[0]["Semester"].ToString(), dt.Rows[0]["Section Code"].ToString(), dt.Rows[0]["Group"].ToString(), dt.Rows[0]["Batch"].ToString(), dt.Rows[0]["Subject Code"].ToString(), dt.Rows[0]["Subject Type"].ToString(), Convert.ToInt32(dt.Rows[0]["lecture"]), dt.Rows[0]["Date"].ToString(), dt.Rows[0]["Faculty Code"].ToString(), Convert.ToInt32(dt.Rows[0]["Att Type"]), drpUnit.SelectedValue, "", Session["GlobalDimension1Code"].ToString(), dt12);
            }
            catch (Exception ex)
            {
                divmsg.InnerHtml = ex.ToString();
                ModalPopupMsg.Show();
                return;
            }
            divmsg.InnerHtml = "Attendance Marked Successfully";

            ModalPopupMsg.Show();
            return;

        }
    }
    public void bindGridHeader()
    {
        string remedial = "";
        if (true == true)
        {
            remedial = "1";
        }
        //if (chkBoxExtraClass.Checked == false)
        //{
        //    remedial = "0";
        //}
        SqlCommand cmd = new SqlCommand("[proc_GetStudentForAttendance]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@EntryNo", EntryNo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            SqlCommand cmd2 = new SqlCommand("proc_GetDateAndHourForMarkAtt_", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@CourseCode", dt.Rows[0]["Course Code"].ToString());
            cmd2.Parameters.Add("@Semester", dt.Rows[0]["Semester Code"].ToString());
            cmd2.Parameters.Add("@Section", dt.Rows[0]["Section Code"].ToString());
            cmd2.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd2.Parameters.Add("@SubjectCode", dt.Rows[0]["Subject Code"].ToString());
            cmd2.Parameters.Add("@Date", Convert.ToDateTime(dt.Rows[0]["Attendance Date"].ToString()));
            cmd2.Parameters.Add("@LectureNo", dt.Rows[0]["Hour No"].ToString());
            cmd2.Parameters.Add("@Group", dt.Rows[0]["Group"].ToString());
            cmd2.Parameters.Add("@Batch", dt.Rows[0]["Batch"].ToString());
            cmd2.Parameters.Add("@AcademicYear", dt.Rows[0]["Academic Year"].ToString());
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
    }

    protected void btnSave1_Click(object sender, EventArgs e)
    {
        string Step = "Step1";
        try
        {
            //if (Save == true)
            //{
            btnSave1.Enabled = false;
            bindGridHeader();
            Step = "Step2";  ////////////////////////////////
            btnSubmit.Enabled = false;
            BindTable();
            Step = "Step3";   /////////////////////////////////////////////////

            int AttendanceType = 0;
            //if (chkBoxExtraClass.Checked == true)
            //    AttendanceType = 1;

            int LectureNo = Convert.ToInt32(Session["1"].ToString());
            int j = 1;
            //if (chkMultiplaeAttendance.Checked == true)
            //{
            //    j = 1;
            //}
            //else
            //{
            j = LectureNo;
            //}
            Step = "Step4";    //////////////////////////////////////////


            // FDL.InsertStudentAttendanceHeaderAndLine(drpAcademicYear.SelectedValue, drpCourse.SelectedValue, drpSemester.SelectedItem.ToString(), drpSection.SelectedValue, ddlGroup.SelectedValue, ddlBatch.SelectedValue, drpSubject.SelectedValue, lblSubjectType.Text, i, txtDate.Text, lblFacultyCode.Text, AttendanceType, drpUnit.SelectedValue, txtTopic.Text, Session["GlobalDimension1Code"].ToString(), dt12);

            Step = "Step5";   //////////////////////////////////////////////////////////
        }

           // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);

          //  btnSubmit.Enabled = true;




        catch (Exception ex)
        {
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Contact to admin for " + Step + "');", true);
        }
    }
    protected void btncancelpopup_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentAttendance.aspx");
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewDetails.Hide();
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        int pk = Convert.ToInt32(grdTimetable.DataKeys[row.RowIndex].Values[0].ToString());
        string SubjectCode = grdTimetable.DataKeys[row.RowIndex].Values[1].ToString();
        string CourseCode = grdTimetable.DataKeys[row.RowIndex].Values[2].ToString();
        //string Status = grdTimetable.DataKeys[row.RowIndex].Values[3].ToString();

        string lecture = grdTimetable.DataKeys[row.RowIndex].Values[3].ToString();
        string Date = grdTimetable.DataKeys[row.RowIndex].Values[4].ToString();
        string Semester = grdTimetable.DataKeys[row.RowIndex].Values[5].ToString();
        string Status = "";
        SqlCommand cmdT = new SqlCommand(" select * from [TMU$Student Detainee List] where [Academic Year]= (Select [Code] from [TMU$Academic Year] where [Runing Year]=1) and [Course Code]='"+CourseCode+"' and [Subject Code]='"+SubjectCode+"' and Semester='"+Semester+"'", con);
        DataTable dtOrderT = new DataTable();
        SqlDataAdapter daT = new SqlDataAdapter(cmdT);
        daT.Fill(dtOrderT);
        if (dtOrderT.Rows.Count > 0 )
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Detainee List Submitted..');", true);
            return;
        }


        Session["SubjectCode"] = grdTimetable.DataKeys[row.RowIndex].Values[1].ToString();
        Session["CourseCode"] = grdTimetable.DataKeys[row.RowIndex].Values[2].ToString();
        //Session["Status"] = grdTimetable.DataKeys[row.RowIndex].Values[3].ToString();
        Session["lecture"] = grdTimetable.DataKeys[row.RowIndex].Values[3].ToString();
        Session["Date"] = grdTimetable.DataKeys[row.RowIndex].Values[4].ToString();
        Session["Semester"] = grdTimetable.DataKeys[row.RowIndex].Values[5].ToString();

        DataTable dt = new DataTable();
        dt = FDL.GetUnitForMarkAttendance(CourseCode, SubjectCode, Session["AcademicYear"].ToString());
        drpUnit.DataSource = dt;
        drpUnit.DataTextField = "Details";
        drpUnit.DataValueField = "No_";
        drpUnit.DataBind();
        if (CourseCode == "")
        {
            if (Status == "Pending" || Status == "Sent")
            {
                Div1.Visible = false;
                //divApproval.Visible = true;
                if (Status == "Sent")
                {
                    //divApproval.Visible = false;
                    div2.Visible = true;
                    GridViewDetails.Show();
                }
                else
                {
                    int temp;
                    con.Open();

                   // SqlCommand cmd = new SqlCommand("INSERT INTO HRMSPortal.[dbo].[tbl_timetableApproval]([Lecture],[Course],[Subject],[Date],[Semester],[Status],[AcademicYear],[facultyCode],[CollegeCode],requestDate,PrincipalID) VALUES ('" + lecture + "','" + CourseCode + "','" + SubjectCode + "','" + Date + "','" + Semester + "',1,(select Code from [TMU$Academic Year]  where [Runing Year]='1'),'" + Session["uid"].ToString() + "','" + Session["GlobalDimension1Coded"].ToString() + "',getdate(),(select top 1 Principal from [TMU$User Role Matrix] where [Course Code]='" + CourseCode + "' order by ID desc))", con);
                     SqlCommand cmd = new SqlCommand("INSERT INTO HRMSPortal.[dbo].[tbl_timetableApproval]([Lecture],[Course],[Subject],[Date],[Semester],[Status],[AcademicYear],[facultyCode],[CollegeCode],requestDate,PrincipalID) VALUES ('" + lecture + "','" + CourseCode + "','" + SubjectCode + "','" + Date + "','" + Semester + "',1,(select Code from [TMU$Academic Year]  where [Runing Year]='1'),'" + Session["uid"].ToString() + "','" + Session["GlobalDimension1Coded"].ToString() + "',getdate(),(select top 1 Principal from [TMU$User Role Matrix] where [Course Code]='" + CourseCode + "' order by ID desc))", con);
                    temp = cmd.ExecuteNonQuery();
                    con.Close();

                    // grdTimetable.DataKeys[row.RowIndex].Values[3] = "Sent";
                    if (chkOpen.Checked == true)
                    {
                        binddatatableOpen();
                    }
                    else
                    {
                        binddatatable(drpCourse.SelectedValue, drpSemester.SelectedValue);
                    }




                    //divApproval.Visible = true;
                    //div2.Visible = false;
                    //GridViewDetails.Show();
                }
            }
            else
            {
                Response.Redirect("StudentAttendanceMark1.aspx");
            }



        }
        else
        {

            System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
            if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
            {
                if (Status == "Pending" || Status == "Sent")
                {
                    Div1.Visible = false;
                    //divApproval.Visible = true;
                    if (Status == "Sent")
                    {
                        //divApproval.Visible = false;
                        div2.Visible = true;
                    }
                    else
                    {
                        int temp;
                        con.Open();

                      SqlCommand cmd = new SqlCommand("INSERT INTO HRMSPortal.[dbo].[tbl_timetableApproval]([Lecture],[Course],[Subject],[Date],[Semester],[Status],[AcademicYear],[facultyCode],[CollegeCode],requestDate,PrincipalID) VALUES ('" + lecture + "','" + CourseCode + "','" + SubjectCode + "','" + Date + "','" + Semester + "',1,(select Code from [TMU$Academic Year]  where [Runing Year]='1'),'" + Session["uid"].ToString() + "','" + Session["GlobalDimension1Coded"].ToString() + "',getdate(),(select top 1 Principal from [TMU$User Role Matrix] where [Course Code]='" + CourseCode + "' order by ID desc))", con);
                        temp = cmd.ExecuteNonQuery();
                        con.Close();
                        if (chkOpen.Checked == true)
                        {
                            binddatatableOpen();
                        }
                        else
                        {
                            binddatatable(drpCourse.SelectedValue, drpSemester.SelectedValue);
                        }

                    }
                }
                else
                {
                    Response.Redirect("StudentAttendanceMark1.aspx");
                    
                    //GridViewDetails.Show();
                    //Div1.Visible = true;
                    //divApproval.Visible = false;
                    //BindStudent(pk);
                   
                }

            }
            else
            {

                if (Status == "Pending" || Status == "Sent")
                {
                    Div1.Visible = false;
                    //divApproval.Visible = true;
                    if (Status == "Sent")
                    {
                        //divApproval.Visible = false;
                        div2.Visible = true;
                        GridViewDetails.Show();
                    }
                    else
                    {
                        int temp;
                        con.Open();

                        SqlCommand cmd = new SqlCommand("INSERT INTO HRMSPortal.[dbo].[tbl_timetableApproval]([Lecture],[Course],[Subject],[Date],[Semester],[Status],[AcademicYear],[facultyCode],[CollegeCode],requestDate,PrincipalID) VALUES ('" + lecture + "','" + CourseCode + "','" + SubjectCode + "','" + Date + "','" + Semester + "',1,(select Code from [TMU$Academic Year]  where [Runing Year]='1'),'" + Session["uid"].ToString() + "','" + Session["GlobalDimension1Coded"].ToString() + "',getdate(),(select top 1 Principal from [TMU$User Role Matrix] where [Course Code]='" + CourseCode + "' order by ID desc))", con);
                        temp = cmd.ExecuteNonQuery();
                        con.Close();
                        if (chkOpen.Checked == true)
                        {
                            binddatatableOpen();
                        }
                        else
                        {
                            binddatatable(drpCourse.SelectedValue, drpSemester.SelectedValue);
                        }
                    }
                }
                else
                {
                    Response.Redirect("StudentAttendanceMark1.aspx");
                }

            }
        }


    }






    protected void btnMarkAtten_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentAttendanceMark1.aspx");
    }
    protected void btnsendApproval_Click(object sender, EventArgs e)
    {
        int temp;
        con.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO HRMSPortal.[dbo].[tbl_timetableApproval]([Lecture],[Course],[Subject],[Date],[Semester],[Status],[AcademicYear],[facultyCode],[CollegeCode],requestDate,PrincipalID) VALUES ('" + Session["lecture"].ToString() + "','" + Session["CourseCode"].ToString() + "','" + Session["SubjectCode"] + "','" + Session["Date"] + "','" + Session["Semester"] + "',1,(select Code from [TMU$Academic Year]  where [Runing Year]='1'),'" + Session["uid"].ToString() + "','" + Session["GlobalDimension1Coded"].ToString() + "',getdate(),(select top 1 Principal from [TMU$User Role Matrix] where [Course Code]='" + Session["CourseCode"].ToString() + "' order by ID desc))", con);
        temp = cmd.ExecuteNonQuery();
        con.Close();
        if (chkOpen.Checked == true)
        {
            binddatatableOpen();
        }
        else
        {
            binddatatable(drpCourse.SelectedValue, drpSemester.SelectedValue);
        }


    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
    }
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSemesterFromCourseWiseFaculty_RoleTemp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 1000000;
        cmd.Parameters.Add("@ID1", Session["uid"].ToString());
        cmd.Parameters.Add("@ID", drpCourse.SelectedValue);
        
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (chkOpen.Checked == false)
        {
            binddatatable(drpCourse.SelectedValue, drpSemester.SelectedValue);
        }
        else
        {
            binddatatableOpen();
        }
    }
}