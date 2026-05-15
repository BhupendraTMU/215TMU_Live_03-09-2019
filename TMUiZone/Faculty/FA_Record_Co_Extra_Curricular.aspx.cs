using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class FA_Record_Co_Extra_Curricular : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_recordCo_Curricular_date.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            lbl_recordCo_Curricular_studentName.Text = Session["MM_StudentName"].ToString();

            SP_FA_MM_Get_Semester();
            SP_GetAllData_CurricularActivities();
        }

    }

    protected void btn_recordCo_Curricular_addData_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        con.SP_FA_CurricularActivities_Insert(lbl_recordCo_Curricular_semester.SelectedValue.Trim(), lbl_recordCo_Curricular_studentName.Text, txt_recordCo_Curricular_activityName.Text.Trim(), txt_recordCo_Curricular_date.Text.Trim().ToString(), txt_recordCo_Curricular_eventDetails.Text.Trim(), txt_recordCo_Curricular_detailEvent_organizer.Text.ToString(), txt_recordCo_Curricular_level_CUSNI.Text.Trim(), txt_recordCo_Curricular_certificaltionPosition.Text.Trim(), ddl_recordCo_CurricularActivities.Text.Trim(), Session["MM_AutoNo"].ToString(),Session["MM_StudentId"].ToString(),Session["uid"].ToString(),Session["Fulname"].ToString(),System.DateTime.Now.ToString("dd MMM yyyy"), Session["MM_Course"].ToString(), Session["MM_AcademicYear"].ToString());
        con.DisConnect();
        SP_GetAllData_CurricularActivities();
        txt_recordCo_Curricular_activityName.Text = "";
        txt_recordCo_Curricular_eventDetails.Text = "";
        txt_recordCo_Curricular_detailEvent_organizer.Text = "";
        txt_recordCo_Curricular_level_CUSNI.Text = "";
        txt_recordCo_Curricular_certificaltionPosition.Text = "";
    }

    public void SP_GetAllData_CurricularActivities()
    {
        DataTable dt = new DataTable();
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_Get_All_Activity_Records(Session["uid"].ToString(),Session["MM_Course"].ToString(),Session["MM_AcademicYear"].ToString(), lbl_recordCo_Curricular_studentName.Text.Trim().ToString());

        dt.Load(dr);
        grdview_recordCo_curricular_tbl.DataSource = dt;
        grdview_recordCo_curricular_tbl.DataBind();
        dr.Close();
        con.DisConnect();
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
            lbl_recordCo_Curricular_semester.DataSource = dt;
            lbl_recordCo_Curricular_semester.DataTextField = "Semester";
            lbl_recordCo_Curricular_semester.DataValueField = "Semester";
            lbl_recordCo_Curricular_semester.DataBind();

            // Optionally, add a default item

        }
        finally
        {

            con.DisConnect(); // Ensure the connection is closed in the finally block

        }
    }

    protected void grdview_recordCo_curricular_tbl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Find the label that holds the status
            //Label lbl_MM_Record_Status = (Label)e.Row.FindControl("lbl_MM_Record_Status");


        }
    }

    protected void grdview_recordCo_curricular_tbl_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = e.RowIndex;

        // Get the GridViewRow being updated
        GridViewRow row = grdview_recordCo_curricular_tbl.Rows[rowIndex];

        // Get the unique identifiers for the record (Auto_No, Sr_No)
        Label lbl_AutoNo = (Label)row.FindControl("lbl_AutoNo_grd");
        Label lbl_SrNo = (Label)row.FindControl("lbl_SrNo_grd_");
        string autoNo = lbl_AutoNo.Text;
        string srNo = lbl_SrNo.Text;

        // Get the non-editable fields (Semester and Student Name)
        string semester = ((Label)row.FindControl("lbl_Semester_grd")).Text;
        string studentName = ((Label)row.FindControl("lbl_StudentName_grd")).Text;

        // Get the editable fields from the EditItemTemplate
        string activityName = ((TextBox)row.FindControl("txt_ActivityName_grd")).Text;
        string eventDetails = ((TextBox)row.FindControl("txt_EventDetails_grd")).Text;
        string eventOrganizer = ((TextBox)row.FindControl("txt_EventOrganizer_grd")).Text;
        string level = ((TextBox)row.FindControl("txt_CUSNI_grd")).Text;
        string certification = ((TextBox)row.FindControl("txt_Certification_grd")).Text;
        string activityType = ((DropDownList)row.FindControl("ddl_recordCo_CurricularActivities")).SelectedValue;

        // Parse the date field
        DateTime activityDate;
        if (DateTime.TryParse(((Label)row.FindControl("txt_Date_grd")).Text, out activityDate))
        {
            // Call the update stored procedure method and pass the required parameters
            pms_connection con = new pms_connection();
            con.SP_FA_CurricularActivities_Update(semester, studentName, activityName, activityDate.ToString("yyyy-MM-dd"), eventDetails, eventOrganizer,
                level, certification, activityType, srNo, autoNo);

            // Exit edit mode
            grdview_recordCo_curricular_tbl.EditIndex = -1;

            con.DisConnect();
            // Rebind the updated data
            SP_GetAllData_CurricularActivities();
        }
        else
        {
            // Handle date parsing error (optional)
            // Show error message or perform necessary action
        }
    }


    protected void grdview_recordCo_curricular_tbl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdview_recordCo_curricular_tbl.EditIndex = -1;
        // Rebind the data to the GridView
        SP_GetAllData_CurricularActivities();
    }

    protected void grdview_recordCo_curricular_tbl_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = grdview_recordCo_curricular_tbl.Rows[e.RowIndex];

        // Get the value of the AutoNo (assuming AutoNo is the primary key)
        Label lbl_AutoNo_grd = (Label)row.FindControl("lbl_AutoNo_grd");
        Label lbl_SrNo_grd_ = (Label)row.FindControl("lbl_SrNo_grd_");
        Label lbl_StudentName_grd = (Label)row.FindControl("lbl_StudentName_grd");

        pms_connection con = new pms_connection();
        con.SP_FA_CurricularActivites_Delete(lbl_SrNo_grd_.Text.Trim(), lbl_AutoNo_grd.Text.Trim(), lbl_StudentName_grd.Text.Trim());
        con.DisConnect();
        SP_GetAllData_CurricularActivities();

    }

    protected void grdview_recordCo_curricular_tbl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdview_recordCo_curricular_tbl.EditIndex = e.NewEditIndex;
        SP_GetAllData_CurricularActivities();

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);


    }
}