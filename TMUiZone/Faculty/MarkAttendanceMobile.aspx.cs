using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_MarkAttendanceMobile : System.Web.UI.Page
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
                SqlCommand cmd = new SqlCommand("[proc_GetTimeTable]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
                cmd.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString());

                DataTable dt = new DataTable();
                da.Fill(dt);


                grdTimetable.DataSource = dt;
                grdTimetable.DataBind();

            }
            catch (Exception ex)
            {
                Response.Redirect("~/Default.aspx");
            }



           
                
        }
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
            lblstu.Visible = true;
            lblUnit.Visible = true;
            drpUnit.Visible = true;
        }


        else
        {
            grdAttendanceDetails.DataSource = "";
            grdAttendanceDetails.DataBind();
            btnSubmit.Visible = false;
            lblstu.Visible = false;
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
            return ;
           
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
        Response.Redirect("StudentAttendanceMark1.aspx");
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        int pk = Convert.ToInt32(grdTimetable.DataKeys[row.RowIndex].Values[0].ToString());
        string SubjectCode = grdTimetable.DataKeys[row.RowIndex].Values[1].ToString();
        string CourseCode = grdTimetable.DataKeys[row.RowIndex].Values[2].ToString();
        string Semester = grdTimetable.DataKeys[row.RowIndex].Values[3].ToString();
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlDataAdapter da1 = new SqlDataAdapter("select count(*) as Q from [TMU$Student Detainee List] where [Academic Year]='" + Session["AcademicYear"].ToString() + "' and [Course Code]='" + CourseCode + "' and [Subject Code]='" + SubjectCode + "' and Semester='" + Semester + "'", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

        con.Close();
        if (Convert.ToInt32(dt1.Rows[0]["Q"]) > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Student Detainee List Already Submitted..');", true);
            return;
        }






        DataTable dt = new DataTable();
        dt = FDL.GetUnitForMarkAttendance(CourseCode, SubjectCode, Session["AcademicYear"].ToString());
        drpUnit.DataSource = dt;
        drpUnit.DataTextField = "Details";
        drpUnit.DataValueField = "No_";
        drpUnit.DataBind();
        BindStudent(pk);
    }
}