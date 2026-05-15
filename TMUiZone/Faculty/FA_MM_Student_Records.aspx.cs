using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_FA_MM_Student_Records : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["UserGroup"].ToString() == "STUDENT")
            {
                lbl_StudentName.Text = Session["enroll"].ToString();
                lbl_AcademicYear.Text = Session["AcademicYear"].ToString();
                lbl_Course.Text = Session["CourseCode"].ToString();
                
            }
            else
            {
                lbl_AcademicYear.Text = Session["MM_Academic_Year"].ToString();
                lbl_StudentName.Text = Session["MM_StudentName"].ToString();
                lbl_Course.Text = Session["MM_Course"].ToString();
                lbl_Program.Text = Session["MM_Program"].ToString();
                lbl_MentorName.Text = Session["uid"].ToString();
            }

            SP_FA_Get_All_Meeting_Records_For_Student();
        }
    }

    public void SP_FA_Get_All_Meeting_Records_For_Student()
    {
        DataTable dt = new DataTable();
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_Get_All_Meeting_Records_For_Student(Session["CourseCode"].ToString(), Session["AcademicYear"].ToString(), Session["enroll"].ToString(),"");

        dt.Load(dr);
        grdview_mentorMentee_tbl.DataSource = dt;
        grdview_mentorMentee_tbl.DataBind();
        dr.Close();
        con.DisConnect();
    }


    protected void btn_Approve_grd_Click(object sender, EventArgs e)
    {
        ViewState["FA_MM_Status"] = "Approved";

        // Get the button that triggered the event
        Button btn_Approve_grd = (Button)sender;

        // Get the GridViewRow containing the button
        GridViewRow parentRow = (GridViewRow)btn_Approve_grd.NamingContainer;

        // Access controls within the row
        Label lblGrd_mentorMentee_srno = (Label)parentRow.FindControl("lblGrd_mentorMentee_srno");
        Label lbl_AutoNo = (Label)parentRow.FindControl("lbl_AutoNo");
        Label lblGrd_mentorMentee_nameStudent = (Label)parentRow.FindControl("lblGrd_mentorMentee_nameStudent");


        // Perform database operation
        pms_connection con = new pms_connection();
        con.SP_FA_StudentStatus_Update(
            lblGrd_mentorMentee_srno.Text.Trim(),
            lbl_AutoNo.Text.Trim(),
            lblGrd_mentorMentee_nameStudent.Text.Trim(),
            ViewState["FA_MM_Status"].ToString(),
            ""
        );
        con.DisConnect();

        // Update GridView and UI
        SP_FA_Get_All_Meeting_Records_For_Student();

    }


    protected void grdview_mentorMentee_tbl_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the "Status" column value for the current row
            string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

            // Find the Approve and Reject buttons in the row
            Button btn_Approve = (Button)e.Row.FindControl("btn_Approve_grd");
            Button btn_Reject = (Button)e.Row.FindControl("btn_Reject_grd");
            Label lbl_Action = (Label)e.Row.FindControl("lbl_Action");


            // Set visibility based on the Status column value
            if (status == "Approved" || status == "Rejected")
            {
                btn_Approve.Visible = false;
                btn_Reject.Visible = false;
                lbl_Action.Text = "Meeting is " + status.ToString();
            }
        }
    }

    protected void btn_Approve_Rejection_Click(object sender, EventArgs e)
    {
        ViewState["FA_MM_Status"] = "Rejected";

        // Get the button that triggered the event
        Button btn_Approve_grd = (Button)sender;

        // Get the GridViewRow containing the button
        GridViewRow parentRow = (GridViewRow)btn_Approve_grd.NamingContainer;

        // Access controls within the row
        Label lblGrd_mentorMentee_srno = (Label)parentRow.FindControl("lblGrd_mentorMentee_srno");
        Label lbl_AutoNo = (Label)parentRow.FindControl("lbl_AutoNo");
        Label lblGrd_mentorMentee_nameStudent = (Label)parentRow.FindControl("lblGrd_mentorMentee_nameStudent");
        TextBox txt_Remarks = (TextBox)parentRow.FindControl("txt_Remarks");


        // Perform database operation
        pms_connection con = new pms_connection();
        con.SP_FA_StudentStatus_Update(
            lblGrd_mentorMentee_srno.Text.Trim(),
            lbl_AutoNo.Text.Trim(),
            lblGrd_mentorMentee_nameStudent.Text.Trim(),
            ViewState["FA_MM_Status"].ToString(), txt_Remarks.Text.Trim().ToString()

        );
        con.DisConnect();

        // Update GridView and UI
        SP_FA_Get_All_Meeting_Records_For_Student();
    }
}