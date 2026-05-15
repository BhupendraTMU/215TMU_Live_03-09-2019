using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Faculty_FA_MM_All_Meeting_Records : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_mentorMentee_date.Text = System.DateTime.Now.ToString("dd MMM yyyy");

            GetAcademicYearData();
            SP_FA_MM_Get_Semester();

            BindCourses(Session["uid"].ToString().Trim(), Session["GlobalDimension1Coded"].ToString().Trim());
            // SP_FA_Get_All_Meeting_Records();
            SP_GetData_MenterFor_Mentee();

        }
    }

    public void SP_FA_MM_Get_Semester()
    {
        pms_connection con = new pms_connection();
        con.Connect();

        try
        {

            SqlCommand cmd = new SqlCommand("SP_FA_MM_Get_Semester", con.Con);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            // Bind the dropdown list to the data
            ddl_mentorMentee_semester.DataSource = dt;
            ddl_mentorMentee_semester.DataTextField = "Semester";
            ddl_mentorMentee_semester.DataValueField = "Semester";
            ddl_mentorMentee_semester.DataBind();

            // Optionally, add a default item

        }
        finally
        {

            con.DisConnect(); // Ensure the connection is closed in the finally block

        }
    }


    private void GetAcademicYearData()
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_MM_Get_Academic_Year();
        DataTable dt = new DataTable();
        dt.Load(dr);

        ddl_mentorFormentee_academicYear.DataSource = dt;
        ddl_mentorFormentee_academicYear.DataTextField = "Academic Year";
        ddl_mentorFormentee_academicYear.DataValueField = "Academic Year";
        ddl_mentorFormentee_academicYear.DataBind();
        dr.Close();
        con.DisConnect();

    }


    private void BindCourses(string loginID, string SP_FA_MM_Get_CourseCode)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dtcourse = con.SP_FA_MM_Get_CourseCode(loginID.Trim(), SP_FA_MM_Get_CourseCode.Trim());

        ddl_mentorFormentee_course.DataSource = dtcourse;
        ddl_mentorFormentee_course.DataTextField = "Course Name";
        ddl_mentorFormentee_course.DataValueField = "Course Code";
        ddl_mentorFormentee_course.DataBind();
        dtcourse.Close();
        con.DisConnect();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

    }

    protected void grdview_mentorMentee_tbl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Find the label that holds the status
            Label lbl_MM_Record_Status = (Label)e.Row.FindControl("lbl_MM_Record_Status");

            //if (lbl_MM_Record_Status.Text=="")
            //{
            //    // Hide the edit button if status is not "Pending"
            //    LinkButton editButton = (LinkButton)e.Row.FindControl("lnkEdit");
            //    editButton.Visible = true;
            //}

        }
    }

    protected void grdview_mentorMentee_tbl_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Get the current row index
        int rowIndex = e.RowIndex;

        // Get the GridViewRow being updated
        GridViewRow row = grdview_mentorMentee_tbl.Rows[rowIndex];

        // Get the controls in the row to fetch updated values
        Label lblGrd_mentorMentee_srno = (Label)row.FindControl("lblGrd_mentorMentee_srno");
        Label lbl_AutoNo = (Label)row.FindControl("lbl_AutoNo");
        Label lblGrd_mentorMentee_nameStudent = (Label)row.FindControl("lblGrd_mentorMentee_nameStudent");

        // Fetch updated values from the textboxes within the GridView row
        string semester = ((Label)row.FindControl("lblGrd_mentorMentee_semester_")).Text;
        DateTime date1 = DateTime.Parse(((Label)row.FindControl("txtGrd_mentorMentee_date")).Text);
        string issueDiscussed = ((TextBox)row.FindControl("txtGrd_mentorMentee_issueProblems")).Text;
        string providedByMentor = ((TextBox)row.FindControl("txtGrd_mentorMentee_providedBymentor")).Text;
        string summaryRemark = ((TextBox)row.FindControl("txtGrd_mentorMentee_summaryRemark")).Text;

        // Perform your update logic here, such as updating the database
        pms_connection con = new pms_connection();
        con.SP_FA_MM_Edit_Record(
            lblGrd_mentorMentee_srno.Text.Trim(),
            lbl_AutoNo.Text.Trim(),
            semester.Trim(),
            date1.ToString("dd MMM yyyy"),
            lblGrd_mentorMentee_nameStudent.Text.Trim(),
            issueDiscussed.Trim(),
            providedByMentor.Trim(),
            summaryRemark.Trim()
        );

        con.DisConnect();

        // Exit edit mode
        grdview_mentorMentee_tbl.EditIndex = -1;

        // Rebind the updated data
        SP_FA_Get_All_Meeting_Records();
    }

    public void SP_FA_Get_All_Meeting_Records()
    {
        DataTable dt = new DataTable();
        pms_connection con = new pms_connection();

        SqlDataReader dr = con.SP_FA_Get_All_Meeting_Records(Session["uid"].ToString(), ddl_mentorFormentee_course.SelectedValue.ToString(), ddl_mentorFormentee_academicYear.SelectedValue.ToString(), "");

        dt.Load(dr);
        grdview_mentorMentee_tbl.DataSource = dt;
        grdview_mentorMentee_tbl.DataBind();
        con.DisConnect();

        // Iterate through the GridView rows
        foreach (GridViewRow row in grdview_mentorMentee_tbl.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    // Find the Label within the current row
                    Label lbl_MM_Record_Status = row.FindControl("lbl_MM_Record_Status") as Label;
                    LinkButton lnk_Record_Status = row.FindControl("lnk_Record_Status") as LinkButton;


                    if (lbl_MM_Record_Status.Text == "Rejected")
                    {
                        lbl_MM_Record_Status.Visible = false;
                        lnk_Record_Status.Visible = true;
                    }
                }
                catch (Exception ex)
                { }
            }
        }
    }





    protected void grdview_mentorMentee_tbl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdview_mentorMentee_tbl.EditIndex = -1;

        // Rebind the data to the GridView
        SP_FA_Get_All_Meeting_Records();
    }

    protected void grdview_mentorMentee_tbl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdview_mentorMentee_tbl.EditIndex = e.NewEditIndex;

        SP_FA_Get_All_Meeting_Records();
    }

    protected void grdview_mentorMentee_tbl_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Get the row that is being deleted
        GridViewRow row = grdview_mentorMentee_tbl.Rows[e.RowIndex];

        // Get the value of the AutoNo (assuming AutoNo is the primary key)
        Label lbl_AutoNo = (Label)row.FindControl("lbl_AutoNo");
        Label lblGrd_mentorMentee_srno = (Label)row.FindControl("lblGrd_mentorMentee_srno");
        Label lblGrd_mentorMentee_nameStudent = (Label)row.FindControl("lblGrd_mentorMentee_nameStudent");

        pms_connection con = new pms_connection();
        con.SP_FA_DelteMM_Record(lblGrd_mentorMentee_srno.Text.Trim(), lbl_AutoNo.Text.Trim(), lblGrd_mentorMentee_nameStudent.Text.Trim());
        con.DisConnect();
        SP_FA_Get_All_Meeting_Records();
    }

    protected void btn_mentorFormentee_get_Click(object sender, EventArgs e)
    {
        //DataTable dt = new DataTable();
        //pms_connection con = new pms_connection();

        //SqlDataReader dr = con.SP_FA_Get_All_Meeting_Records(Session["uid"].ToString(), ddl_mentorFormentee_course.SelectedValue.ToString(), ddl_mentorFormentee_academicYear.SelectedValue.ToString(), txt_mentorFormentee_studentEnrollmentName.Text.Trim().ToString());

        //dt.Load(dr);
        //grdview_mentorMentee_tbl.DataSource = dt;
        //grdview_mentorMentee_tbl.DataBind();
        //con.DisConnect();
        SP_GetData_MenterFor_Mentee();
    }

    protected void ddl_mentorFormentee_course_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataTable dt = new DataTable();
        //pms_connection con = new pms_connection();

        //SqlDataReader dr = con.SP_FA_Get_All_Meeting_Records(Session["uid"].ToString(), ddl_mentorFormentee_course.SelectedValue.ToString(), ddl_mentorFormentee_academicYear.SelectedValue.ToString(), txt_mentorFormentee_studentEnrollmentName.Text.Trim().ToString());

        //dt.Load(dr);
        //grdview_mentorMentee_tbl.DataSource = dt;
        //grdview_mentorMentee_tbl.DataBind();
        //con.DisConnect();
    }

    protected void ddl_mentorFormentee_academicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataTable dt = new DataTable();
        //pms_connection con = new pms_connection();

        //SqlDataReader dr = con.SP_FA_Get_All_Meeting_Records(Session["uid"].ToString(), ddl_mentorFormentee_course.SelectedValue.ToString(), ddl_mentorFormentee_academicYear.SelectedValue.ToString(), txt_mentorFormentee_studentEnrollmentName.Text.Trim().ToString());

        //dt.Load(dr);
        //grdview_mentorMentee_tbl.DataSource = dt;
        //grdview_mentorMentee_tbl.DataBind();
        //con.DisConnect();
    }


    protected void btnAddMenteeRecord_Click(object sender, EventArgs e)
    {
        foreach (ListItem list in lb_Student_list.Items)
        {
            // Check if the item is selected
            if (list.Selected)
            {
                string autoNo = list.Attributes["AutoNo"];

                // Create a new connection
                pms_connection con = new pms_connection();

                // Call your stored procedure with the selected item text and other parameters
                con.SP_FA_Mentor_Mentee_MeetingRecord_Insert(
                    ddl_mentorMentee_semester.Text.Trim(),
                    txt_mentorMentee_date.Text.Trim(),
                    list.Text, // Student name
                    txt_mentorMentee_issue_identified.Text.Trim(),
                    txt_mentorMentee_providedAdvice_bymentor.Text.Trim(),
                    txt_mentorMentee_summaryRemark.Text.Trim(),
                    "", // Empty value placeholder
                    list.Value, // No
                    Session["uid"].ToString(),
                    Session["Fulname"].ToString(),
                    DateTime.Now.ToString("dd MMM yyyy"),
                    ddl_mentorFormentee_course.Text.Trim(),
                    ddl_mentorFormentee_academicYear.Text.Trim()
                );

                // Disconnect the connection
                con.DisConnect();
            }
        }

        // Refresh meeting records
        // SP_FA_Get_All_Meeting_Records();
        txt_mentorMentee_issue_identified.Text = "";
        txt_mentorMentee_providedAdvice_bymentor.Text = "";
        txt_mentorMentee_summaryRemark.Text = "";
    }
    private void SP_GetData_MenterFor_Mentee()
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_GetData_MenterFor_Mentee(txt_mentorFormentee_studentEnrollmentName.Text.Trim().ToString(), ddl_mentorFormentee_course.SelectedValue.Trim().ToString(), ddl_mentorFormentee_academicYear.SelectedValue.Trim().ToString(), Session["uid"].ToString());

        if (dr.HasRows)
        {
            lb_Student_list.Items.Clear(); // Clear any existing items

            while (dr.Read())
            {
                ListItem item = new ListItem
                {
                    Text = dr["StudentName"].ToString(),
                    Value = dr["No"].ToString()
                };

                // Add AutoNo as an attribute
                item.Attributes["AutoNo"] = dr["AutoNo"].ToString();

                lb_Student_list.Items.Add(item);
            }
        }

        dr.Close();
        con.DisConnect();

    }
}