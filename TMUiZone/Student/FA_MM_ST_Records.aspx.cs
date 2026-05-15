using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_FA_MM_ST_Records : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                SP_FA_MM_Get_Semester();
                GetAcademicYearData();
                lbl_StudentName.Text = Session["uid"].ToString();
                lbl_AcademicYear.SelectedValue = Session["AcademicYear"].ToString();
                lbl_Program.Text = Session["CourseCode"].ToString();
                ddl_Semester.SelectedValue = Session["Semester"].ToString().Trim();
                SP_FA_Get_All_Meeting_Records_For_Student();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }

    private void GetAcademicYearData()
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_MM_Get_Academic_Year();
        DataTable dt = new DataTable();
        dt.Load(dr);
        lbl_AcademicYear.DataSource = dt;
        lbl_AcademicYear.DataTextField = "Academic Year";
        lbl_AcademicYear.DataValueField = "Academic Year";
        lbl_AcademicYear.DataBind();
        dr.Close();
        con.DisConnect();

    }

    public void SP_FA_Get_All_Meeting_Records_For_Student()
    {
        DataTable dt = new DataTable();
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_Get_All_Meeting_Records_For_Student(lbl_Program.Text.Trim(), lbl_AcademicYear.SelectedValue.Trim(), lbl_StudentName.Text.Trim(),ddl_Semester.SelectedValue.Trim());

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

    protected void lbl_AcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        SP_FA_Get_All_Meeting_Records_For_Student();

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
            ddl_Semester.DataSource = dt;
            ddl_Semester.DataTextField = "Semester";
            ddl_Semester.DataValueField = "Semester";
            ddl_Semester.DataBind();

            // Optionally, add a default item

        }
        finally
        {

            con.DisConnect(); // Ensure the connection is closed in the finally block

        }
    }
    protected void ddl_Semester_SelectedIndexChanged(object sender, EventArgs e)
    {
        SP_FA_Get_All_Meeting_Records_For_Student();
    }
}