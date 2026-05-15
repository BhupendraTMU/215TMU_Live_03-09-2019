using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FA_MM_Students_Assessment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_students_assessment_date.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            txt_students_assessment_studentName.Text = Session["MM_StudentName"].ToString();
            SP_FA_MM_Get_Semester();
            SP_FA_StudentAssessment_GetAllData();
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

    protected void btn_students_assessment_addData_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        con.SP_FA_StudentAssessment_MM_Insert(Session["MM_AutoNo"].ToString(), ddl_Semester.SelectedValue.Trim(), txt_students_assessment_studentName.Text.Trim().ToString(), txt_students_assessment_date.Text,
            ddl_students_assessment_regularity_classrooms.SelectedValue.Trim().ToString(), ddl_students_assessment_performance_study.SelectedValue.ToString(), ddl_students_assessment_participation_activities.SelectedValue.ToString(),
            ddl_students_assessment_physicalHealth_status.SelectedValue.ToString(), ddl_students_assessment_behaviour_teachers_students.SelectedValue.ToString(), ddl_students_assessment_mentalHealth_status.SelectedValue.ToString(),
            
            ddl_students_assessment_regularity_classrooms.SelectedItem.Text.Trim(), ddl_students_assessment_performance_study.SelectedItem.Text.Trim(), ddl_students_assessment_participation_activities.SelectedItem.Text.Trim(),
            ddl_students_assessment_physicalHealth_status.SelectedItem.Text.Trim(), ddl_students_assessment_behaviour_teachers_students.SelectedItem.Text.Trim(), ddl_students_assessment_mentalHealth_status.SelectedItem.Text.Trim(),
            Session["MM_StudentId"].ToString(), Session["MM_AcademicYear"].ToString(), Session["MM_Course"].ToString(),Session["uid"].ToString()
            );
        con.DisConnect();
        SP_FA_StudentAssessment_GetAllData();
    }

    public void SP_FA_StudentAssessment_GetAllData()
    {
        DataTable dt = new DataTable();
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_StudentAssessment_GetAllData(Session["MM_AutoNo"].ToString());

        dt.Load(dr);
        grdview_students_assessment_tbl.DataSource = dt;
        grdview_students_assessment_tbl.DataBind();
        dr.Close();
        con.DisConnect();

    }

    protected void grdview_students_assessment_tbl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdview_students_assessment_tbl.EditIndex = e.NewEditIndex;
        SP_FA_StudentAssessment_GetAllData();
    }

    protected void grdview_students_assessment_tbl_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = grdview_students_assessment_tbl.Rows[e.RowIndex];

        Label lbl_SrNo = (Label)row.FindControl("lblGrd_studentsAssessment_srno");
        Label lbl_AutoNogrd = (Label)row.FindControl("lblGrd_studentsAssessment_autoNo");
        Label lblGrd_specialBymentee_studentName = (Label)row.FindControl("lblGrd_studentsAssessment_studentName");

        pms_connection con = new pms_connection();
        con.SP_FA_StudentAssessment_MM_Delete(lbl_SrNo.Text.Trim(), lbl_AutoNogrd.Text.Trim(), lblGrd_specialBymentee_studentName.Text.Trim());
        con.DisConnect();
        SP_FA_StudentAssessment_GetAllData();

    }

    protected void grdview_students_assessment_tbl_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = e.RowIndex;

        // Get the GridViewRow being updated
        GridViewRow row = grdview_students_assessment_tbl.Rows[rowIndex];

        // Get the controls in the row to fetch updated values
        Label lblGrd_mentorMentee_srno = (Label)row.FindControl("lblGrd_studentsAssessment_srnoEdit");
        Label lbl_AutoNo = (Label)row.FindControl("lblGrd_studentsAssessment_autoNoEdit");
        Label lblGrd_mentorMentee_nameStudent = (Label)row.FindControl("txtGrd_studentsAssessment_studentName");

        // Fetch updated values from the textboxes within the GridView row
        string semester = ((Label)row.FindControl("txtGrd_studentsAssessment_semester")).Text;
        DateTime date = DateTime.Parse(((Label)row.FindControl("txtGrd_studentsAssessment_date")).Text);
        DropDownList ddl_students_assessment_regularity_classrooms_ = ((DropDownList)row.FindControl("ddl_students_assessment_regularity_classrooms_"));
        DropDownList ddl_students_assessment_performance_study_ = ((DropDownList)row.FindControl("ddl_students_assessment_performance_study_"));
        DropDownList ddl_students_assessment_participation_activities_ = ((DropDownList)row.FindControl("ddl_students_assessment_participation_activities_"));

        DropDownList ddl_students_assessment_physicalHealth_status_ = ((DropDownList)row.FindControl("ddl_students_assessment_physicalHealth_status_"));
        DropDownList ddl_students_assessment_behaviour_teachers_students_ = ((DropDownList)row.FindControl("ddl_students_assessment_behaviour_teachers_students_"));
        DropDownList ddl_students_assessment_mentalHealth_status_ = ((DropDownList)row.FindControl("ddl_students_assessment_mentalHealth_status_"));

        // Perform your update logic here, such as updating the database
        pms_connection con = new pms_connection();
        con.SP_FA_StudentAssessment_MM_Update(lbl_AutoNo.Text, semester.ToString(), lblGrd_mentorMentee_nameStudent.Text.Trim(),
            date.ToString(), ddl_students_assessment_regularity_classrooms_.SelectedValue.Trim().ToString(),
            ddl_students_assessment_performance_study_.SelectedValue.ToString(),
            ddl_students_assessment_participation_activities_.SelectedValue.ToString(),
            ddl_students_assessment_physicalHealth_status_.SelectedValue.ToString(),
            ddl_students_assessment_behaviour_teachers_students_.SelectedValue.ToString(),
            ddl_students_assessment_mentalHealth_status_.SelectedValue.ToString(),
            lblGrd_mentorMentee_srno.Text.Trim(),

            ddl_students_assessment_regularity_classrooms_.SelectedItem.Text.Trim(),
            ddl_students_assessment_performance_study_.SelectedItem.Text.Trim(),
            ddl_students_assessment_participation_activities_.SelectedItem.Text.Trim(),
            ddl_students_assessment_physicalHealth_status_.SelectedItem.Text.Trim(),
            ddl_students_assessment_behaviour_teachers_students_.SelectedItem.Text.Trim(),
            ddl_students_assessment_mentalHealth_status_.SelectedItem.Text.Trim()
           ); 

        con.DisConnect();

        // Exit edit mode
        grdview_students_assessment_tbl.EditIndex = -1;

        // Rebind the updated data
        SP_FA_StudentAssessment_GetAllData();

    }

    protected void grdview_students_assessment_tbl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        grdview_students_assessment_tbl.EditIndex = -1;
        // Rebind the data to the GridView
        SP_FA_StudentAssessment_GetAllData();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);


    }

    protected void grdview_students_assessment_tbl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
        {
            // Regularity Classrooms Dropdown
            DropDownList ddlRegularity = (DropDownList)e.Row.FindControl("ddl_students_assessment_regularity_classrooms_");
            if (ddlRegularity != null)
            {
                // Set the selected value based on database value
                string selectedValue = DataBinder.Eval(e.Row.DataItem, "Regularity_Classrooms").ToString();
                ddlRegularity.SelectedValue = selectedValue;
            }

            // Performance Study Dropdown
            DropDownList ddlPerformance = (DropDownList)e.Row.FindControl("ddl_students_assessment_performance_study_");
            if (ddlPerformance != null)
            {
                // Set the selected value based on database value
                string selectedValue = DataBinder.Eval(e.Row.DataItem, "Performance_Study").ToString();
                ddlPerformance.SelectedValue = selectedValue;
            }

            // Participation Curricular Activities Dropdown
            DropDownList ddlParticipation = (DropDownList)e.Row.FindControl("ddl_students_assessment_participation_activities_");
            if (ddlParticipation != null)
            {
                // Set the selected value based on database value
                string selectedValue = DataBinder.Eval(e.Row.DataItem, "Participation_CurricularActivities").ToString();
                ddlParticipation.SelectedValue = selectedValue;
            }

            // Physical Status Dropdown
            DropDownList ddlPhysical = (DropDownList)e.Row.FindControl("ddl_students_assessment_physicalHealth_status_");
            if (ddlPhysical != null)
            {
                string selectedValue = DataBinder.Eval(e.Row.DataItem, "Physical_Status").ToString();
                ddlPhysical.SelectedValue = selectedValue;
            }

            // Behaviour Teachers Students Dropdown
            DropDownList ddlBehaviour = (DropDownList)e.Row.FindControl("ddl_students_assessment_behaviour_teachers_students_");
            if (ddlBehaviour != null)
            {
                string selectedValue = DataBinder.Eval(e.Row.DataItem, "Behaviour_Teachers_Students").ToString();
                ddlBehaviour.SelectedValue = selectedValue;
            }

            // Mental Status Dropdown
            DropDownList ddlMental = (DropDownList)e.Row.FindControl("ddl_students_assessment_mentalHealth_status_");
            if (ddlMental != null)
            {
                string selectedValue = DataBinder.Eval(e.Row.DataItem, "Mental_Status").ToString();
                ddlMental.SelectedValue = selectedValue;
            }
        }
    }

}
