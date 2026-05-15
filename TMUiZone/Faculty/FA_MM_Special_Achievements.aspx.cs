using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FA_MM_Special_Achievements : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
            if (!IsPostBack)
            {
                txt_splAchievements_date.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                lbl_splAchievements_stName.Text = Session["MM_StudentName"].ToString();
                SP_FA_MM_Get_Semester();
                SP_FA_MM_Get_SpecialAchievements_Records();

            }
        //}
        //catch
        //{

        //    Response.Redirect("../Default.aspx", false);

        //}

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
            ddl_semester.DataSource = dt;
            ddl_semester.DataTextField = "Semester";
            ddl_semester.DataValueField = "Semester";
            ddl_semester.DataBind();

            // Optionally, add a default item

        }
        finally
        {

            con.DisConnect(); // Ensure the connection is closed in the finally block

        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

    }

    public void SP_FA_MM_Get_SpecialAchievements_Records()
    {
        DataTable dt = new DataTable();
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_FA_MM_Get_SpecialAchievements_Records(Session["uid"].ToString(),Session["MM_Course"].ToString(),Session["MM_AcademicYear"].ToString(),Session["MM_StudentId"].ToString());

        dt.Load(dr);
        gv_special_achievements.DataSource = dt;
        gv_special_achievements.DataBind();
        dr.Close();
        con.DisConnect();

    }

    protected void gv_special_achievements_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_special_achievements.EditIndex = e.NewEditIndex;
        SP_FA_MM_Get_SpecialAchievements_Records();
    }

    protected void gv_special_achievements_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = e.RowIndex;

        // Get the GridViewRow being updated
        GridViewRow row = gv_special_achievements.Rows[rowIndex];

        // Get the controls in the row to fetch updated values
        Label lbl_SrNo = (Label)row.FindControl("txtSr_No");
        Label lbl_AutoNogrd = (Label)row.FindControl("txtAutoNo");

        Label lbl_StudentName_Edit = (Label)row.FindControl("lbl_StudentName_Edit");
        Label lbl_Student_Id_edit = (Label)row.FindControl("lbl_Student_Id_edit");

        DropDownList ddl_Achievements_edit = (DropDownList)row.FindControl("ddl_Achievements_edit");

        // Fetch updated values from the textboxes within the GridView row
        string semester = ((Label)row.FindControl("lblSemester_Edit")).Text;

        DateTime date = DateTime.Parse(((Label)row.FindControl("txtDate")).Text);

        string txtActivityName = ((TextBox)row.FindControl("txtActivityName")).Text;
        string txtDetailsOfEvent = ((TextBox)row.FindControl("txtDetailsOfEvent")).Text;
        string txtInter_Intra_Uni = ((TextBox)row.FindControl("txtInter_Intra_Uni")).Text;
        string txtPosition = ((TextBox)row.FindControl("txtPosition")).Text;
        string txtRemarks = ((TextBox)row.FindControl("txtRemarks")).Text;



        // Perform your update logic here, such as updating the database
        pms_connection con = new pms_connection();
        con.SP_FA_MM_Update_SpecialAchievements(semester.ToString(),ddl_Achievements_edit.Text.ToString(),lbl_StudentName_Edit.Text.ToString(),date.ToString("dd MMM yyyy"),
            txtActivityName.ToString(), txtDetailsOfEvent.ToString(),txtInter_Intra_Uni.ToString(),txtPosition.ToString(),txtRemarks.ToString(),
            lbl_AutoNogrd.Text.ToString(), lbl_SrNo.Text.ToString(), lbl_Student_Id_edit.Text.ToString());

        con.DisConnect();

        // Exit edit mode
        gv_special_achievements.EditIndex = -1;

        // Rebind the updated data
        SP_FA_MM_Get_SpecialAchievements_Records();
    }

    protected void gv_special_achievements_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_special_achievements.EditIndex = -1;
        // Rebind the data to the GridView
        SP_FA_MM_Get_SpecialAchievements_Records();
    }

    protected void btn_SaveData_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();

        con.SP_FA_MM_Insert_SpecialAchievements(
            ddl_semester.SelectedValue.Trim(),
            ddl_achievement.SelectedValue.Trim(),
            lbl_splAchievements_stName.Text.Trim(),
            txt_splAchievements_date.Text.Trim(),
            txt_splAchievements_activityName.Text.Trim(),
            txt_splAchievements_retailEventOrganization.Text.Trim(),
            txt_splAchievements_interIntra.Text.Trim(),
            txt_splAchievements_position.Text.Trim(),
            txt_splAchievements_remants.Text.Trim(),
            Session["MM_AutoNo"].ToString(),
            Session["MM_StudentId"].ToString(),
            Session["uid"].ToString(),
            Session["Fulname"].ToString(),
            DateTime.Now.ToString(),
            Session["MM_Course"].ToString(),
            Session["MM_AcademicYear"].ToString()
        );

        con.DisConnect();


        //ddl_semester.SelectedIndex = -1;
        //ddl_achievement.SelectedIndex = -1;
        //lbl_splAchievements_stName.Text = "";
        //txt_splAchievements_date.Text = "";
        txt_splAchievements_activityName.Text = "";
        txt_splAchievements_retailEventOrganization.Text = "";
        txt_splAchievements_interIntra.Text = "";
        txt_splAchievements_position.Text = "";
        txt_splAchievements_remants.Text = "";
        SP_FA_MM_Get_SpecialAchievements_Records();

    }

    protected void gv_special_achievements_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = gv_special_achievements.Rows[e.RowIndex];

        Label lbl_SrNo = (Label)row.FindControl("lblSr_No");
        Label lbl_AutoNogrd = (Label)row.FindControl("lblAutoNo");
        Label lbl_Student_Id_edit = (Label)row.FindControl("lbl_Student_Id_edit");
        

        pms_connection con = new pms_connection();
        con.SP_FA_MM_Delete_SpecialAchievements(lbl_SrNo.Text.Trim(), lbl_AutoNogrd.Text.Trim(), Session["MM_StudentId"].ToString());
        con.DisConnect();
        SP_FA_MM_Get_SpecialAchievements_Records();
    }
}