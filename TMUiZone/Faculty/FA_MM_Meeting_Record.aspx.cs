using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class FA_MM_Meeting_Record : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            txt_mentorMentee_date.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            lbl_mentorMentee_student_Name.Text = Session["MM_StudentName"].ToString();
            lbl_mentorMentee_student_Id.Text = Session["MM_StudentId"].ToString();
            SP_Tbl_FA_Mentor_Mentee_MeetingRecord_Insert_Read();
            SP_FA_MM_Get_Semester();
        }
    }

    protected void btn_mentorMentee_dataAdd_Click1(object sender, EventArgs e)
    {
        //DateTime parsedate = DateTime.Parse(txt_mentorMentee_date.Text.Trim().ToString());
        pms_connection con = new pms_connection();
        con.SP_FA_Mentor_Mentee_MeetingRecord_Insert(ddl_mentorMentee_semester.Text.Trim(), txt_mentorMentee_date.Text.Trim().ToString(), lbl_mentorMentee_student_Name.Text, txt_mentorMentee_issue_identified.Text.Trim(), txt_mentorMentee_providedAdvice_bymentor.Text.Trim(), txt_mentorMentee_summaryRemark.Text.Trim(), Session["MM_AutoNo"].ToString(), Session["MM_StudentId"].ToString(),Session["uid"].ToString(), Session["Fulname"].ToString(),System.DateTime.Now.ToString("dd MMM yyyy"), Session["MM_Course"].ToString(), Session["MM_AcademicYear"].ToString());
        con.DisConnect();
        SP_Tbl_FA_Mentor_Mentee_MeetingRecord_Insert_Read();
        txt_mentorMentee_issue_identified.Text = "";
        txt_mentorMentee_providedAdvice_bymentor.Text = "";
        txt_mentorMentee_summaryRemark.Text = "";
    }


    public void SP_Tbl_FA_Mentor_Mentee_MeetingRecord_Insert_Read()
    {
        DataTable dt = new DataTable();
        pms_connection con = new pms_connection();

        try
        {
            // Fetch data using the connection
            SqlDataReader dr = con.SP_FA_Get_All_Meeting_Records(
                Session["uid"].ToString(),
                Session["MM_Course"].ToString(),
                Session["MM_AcademicYear"].ToString(),
                Session["MM_StudentName"].ToString()
            );

            dt.Load(dr);
            dr.Close();
        }
        finally
        {
            con.DisConnect(); // Ensure connection is closed
        }

        // Bind data to GridView
        grdview_mentorMentee_tbl.DataSource = dt;
        grdview_mentorMentee_tbl.DataBind();

        // Iterate through the GridView rows
        foreach (GridViewRow row in grdview_mentorMentee_tbl.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                // Find controls within the current row
                Label lbl_MM_Record_Status = row.FindControl("lbl_MM_Record_Status") as Label;
                Label lbl_MM_Record_Status_ = row.FindControl("lbl_MM_Record_Status_") as Label;

                LinkButton lnk_Record_Status = row.FindControl("lnk_Record_Status") as LinkButton;
                LinkButton lnk_Record_Status_ = row.FindControl("lnk_Record_Status_") as LinkButton;

                LinkButton lnkEdit = row.FindControl("lnkEdit") as LinkButton;
                LinkButton lnkDelete = row.FindControl("lnkDelete") as LinkButton;

                // Default visibility settings
                bool isEditVisible = true;
                bool isDeleteVisible = true;

                // Handle non-edit mode (initially on page load)
                if (lbl_MM_Record_Status != null && !string.IsNullOrEmpty(lbl_MM_Record_Status.Text))
                {
                    // Handle lbl_MM_Record_Status logic when not in edit mode
                    switch (lbl_MM_Record_Status.Text)
                    {
                        case "Pending":
                            isEditVisible = true;
                            isDeleteVisible = true;
                            break;

                        case "Rejected":
                            lbl_MM_Record_Status.Visible = false;
                            if (lnk_Record_Status != null) lnk_Record_Status.Visible = true;
                            isEditVisible = false;
                            isDeleteVisible = false;
                            break;

                        case "Approved":
                            isEditVisible = false;
                            isDeleteVisible = false;
                            break;
                    }
                }

                // Handle edit mode (when the row is in edit state)
                if (lbl_MM_Record_Status_ != null && !string.IsNullOrEmpty(lbl_MM_Record_Status_.Text))
                {
                    // Handle lbl_MM_Record_Status_ logic when in edit mode
                    switch (lbl_MM_Record_Status_.Text)
                    {
                        case "Pending":
                            isEditVisible = true;
                            isDeleteVisible = true;
                            break;

                        case "Rejected":
                            lbl_MM_Record_Status_.Visible = false;
                            if (lnk_Record_Status_ != null) lnk_Record_Status_.Visible = true;
                            isEditVisible = false;
                            isDeleteVisible = false;
                            break;

                        case "Approved":
                            isEditVisible = false;
                            isDeleteVisible = false;
                            break;
                    }
                }

                // Apply visibility settings
                if (lnkEdit != null) lnkEdit.Visible = isEditVisible;
                if (lnkDelete != null) lnkDelete.Visible = isDeleteVisible;
            }
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
    protected void grdview_mentorMentee_tbl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdview_mentorMentee_tbl.EditIndex = e.NewEditIndex;

        SP_Tbl_FA_Mentor_Mentee_MeetingRecord_Insert_Read();
    }

    protected void grdview_mentorMentee_tbl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdview_mentorMentee_tbl.EditIndex = -1;

        // Rebind the data to the GridView
        SP_Tbl_FA_Mentor_Mentee_MeetingRecord_Insert_Read();
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
        SP_Tbl_FA_Mentor_Mentee_MeetingRecord_Insert_Read();
    }


    protected void grdview_mentorMentee_tbl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Find the label that holds the status
            Label lbl_MM_Record_Status_ = (Label)e.Row.FindControl("lbl_MM_Record_Status_");
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            

        }
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
        SP_Tbl_FA_Mentor_Mentee_MeetingRecord_Insert_Read();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

    }
}