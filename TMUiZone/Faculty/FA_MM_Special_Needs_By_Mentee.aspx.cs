using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class FA_MM_Special_Needs_By_Mentee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_specialBymentee_date.Text = System.DateTime.Now.ToString("dd MMM yyyy");

            lbl_specialBymentee_studentName.Text = Session["MM_StudentName"].ToString();
            SP_GetAllData_SpecialNeeds_Bymentee_Read();
            SP_FA_MM_Get_Semester();
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

    protected void btn_specialBymentee_addData_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        con.SP_FA_tbl_SpecialNeeds_Bymentee_Insert(ddl_Semester.SelectedValue.Trim(), lbl_specialBymentee_studentName.Text, txt_specialBymentee_date.Text.Trim(), txt_specialBymentee_provide_mentee.Text.Trim(), txt_specialBymentee_remark.Text.Trim(), Session["MM_AutoNo"].ToString(),
            Session["MM_StudentId"].ToString(), Session["MM_AcademicYear"].ToString(), Session["MM_Course"].ToString(), Session["uid"].ToString());
        con.DisConnect();
        SP_GetAllData_SpecialNeeds_Bymentee_Read();
        txt_specialBymentee_remark.Text = "";   
        txt_specialBymentee_provide_mentee.Text = "";

    }


    public void SP_GetAllData_SpecialNeeds_Bymentee_Read()
    {
        DataTable dt = new DataTable();
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_GetAllData_SpecialNeeds_Bymentee(Session["MM_AutoNo"].ToString());

        dt.Load(dr);
        grdview_speicialBymentee_tbl.DataSource = dt;
        grdview_speicialBymentee_tbl.DataBind();
        dr.Close();
        con.DisConnect();
    }

    protected void grdview_speicialBymentee_tbl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    // Assuming that the serial number starts from 1
        //    int serialNumber = e.Row.RowIndex + 1; // RowIndex is zero-based
        //    Label lblSNo = (Label)e.Row.FindControl("lblSNo");
        //    lblSNo.Text = serialNumber.ToString();
        //}

    }

    protected void grdview_speicialBymentee_tbl_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int rowIndex = e.RowIndex;

        // Get the GridViewRow being updated
        GridViewRow row = grdview_speicialBymentee_tbl.Rows[rowIndex];

        // Get the controls in the row to fetch updated values
        Label lbl_SrNo = (Label)row.FindControl("lbl_SrNo_");
        Label lbl_AutoNogrd = (Label)row.FindControl("lbl_AutoNogrd_Edit");
        Label lblGrd_specialBymentee_studentName = (Label)row.FindControl("txtGrd_specialBymentee_studentName");

        // Fetch updated values from the textboxes within the GridView row
        string semester = ((Label)row.FindControl("txtGrd_specialBymentee_semester")).Text;
        DateTime date = DateTime.Parse(((Label)row.FindControl("txtGrd_specialBymentee_date")).Text);
        string lblGrd_specialBymentee_providetheMentee = ((TextBox)row.FindControl("txtGrd_specialBymentee_providetheMentee")).Text;
        string lblGrd_specialBymentee_remark = ((TextBox)row.FindControl("txtGrd_specialBymentee_remark")).Text;

        // Perform your update logic here, such as updating the database
        pms_connection con = new pms_connection();
        con.SP_FA_tbl_SpecialNeeds_Bymentee_Update(semester.ToString(), lblGrd_specialBymentee_studentName.Text.ToString(), date.ToString("yyyy-MM-dd"), lblGrd_specialBymentee_providetheMentee.ToString(),
            lblGrd_specialBymentee_remark.ToString(), lbl_AutoNogrd.Text.ToString(), lbl_SrNo.Text.ToString());

        con.DisConnect();

        // Exit edit mode
        grdview_speicialBymentee_tbl.EditIndex = -1;

        // Rebind the updated data
        SP_GetAllData_SpecialNeeds_Bymentee_Read();

    }

    protected void grdview_speicialBymentee_tbl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdview_speicialBymentee_tbl.EditIndex = e.NewEditIndex;
        SP_GetAllData_SpecialNeeds_Bymentee_Read();

    }

    protected void grdview_speicialBymentee_tbl_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = grdview_speicialBymentee_tbl.Rows[e.RowIndex];

        Label lbl_SrNo = (Label)row.FindControl("lbl_SrNo");
        Label lbl_AutoNogrd = (Label)row.FindControl("lbl_AutoNogrd");
        Label lblGrd_specialBymentee_studentName = (Label)row.FindControl("lblGrd_specialBymentee_studentName");

        pms_connection con = new pms_connection();
        con.SP_FA_tbl_SpecialNeeds_Bymentee_Delete(lbl_SrNo.Text.Trim(), lbl_AutoNogrd.Text.Trim(), lblGrd_specialBymentee_studentName.Text.Trim());
        con.DisConnect();
        SP_GetAllData_SpecialNeeds_Bymentee_Read();
    }

    protected void grdview_speicialBymentee_tbl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdview_speicialBymentee_tbl.EditIndex = -1;
        // Rebind the data to the GridView
        SP_GetAllData_SpecialNeeds_Bymentee_Read();

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

    }
}