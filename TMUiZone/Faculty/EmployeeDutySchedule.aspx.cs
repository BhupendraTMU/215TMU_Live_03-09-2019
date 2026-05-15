using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Faculty_EmployeeDutySchedule : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAcademicYear();
            BindExamMethod();
        }
    }
    public void BindAcademicYear()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            ddlAcademicYear.DataSource = dt1;
            ddlAcademicYear.DataTextField = "Details";
            ddlAcademicYear.DataValueField = "No_";
            ddlAcademicYear.DataBind();
        }
        catch
        {
        }
    }
    public void BindExamMethod()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("sp_GetExamMethodForDuty", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamType", DdlExamType.SelectedValue);
            cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            DdlExamMethod.DataSource = dt1;
            DdlExamMethod.DataTextField = "Details";
            DdlExamMethod.DataValueField = "Details";
            DdlExamMethod.DataBind();
        }
        catch
        {
        }
    }
    protected void DdlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindExamMethod();
        bindgrid();
    }

    protected void DdlExamMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindgrid();
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindExamMethod();
        bindgrid();
    }
    public void bindgrid()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_getFacultySchedule", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
            cmd.Parameters.AddWithValue("@ExamType", DdlExamType.SelectedValue);
            cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@ExamMethod", DdlExamMethod.SelectedValue);
            cmd.Parameters.AddWithValue("@date", txtDateD.Text);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            GrdExamSchedule.DataSource = dt;
            GrdExamSchedule.DataBind();

        }
        catch (Exception ex) { }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {

        bindgrid();
    }



protected void lnkAp_Click(object sender, EventArgs e)
{
    try
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow grow = (GridViewRow)btn.NamingContainer;
        SqlCommand cmd = new SqlCommand("Sp_EmployeeApprovalStatusupdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Shift", (grow.FindControl("hdShift") as HiddenField).Value);
        cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.AddWithValue("@ExamType", DdlExamType.SelectedValue);
        cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@ExamMethod", DdlExamMethod.SelectedValue);
        cmd.Parameters.AddWithValue("@date", txtDateD.Text);
        cmd.Parameters.AddWithValue("@Status", 1);
        cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Updated Successfully');", true);
            bindgrid();
    }
    catch (Exception ex) { }
}

protected void lnkRj_Click(object sender, EventArgs e)
{
    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalB').modal('show');</script>", false);
    LinkButton btn = (LinkButton)sender;
    GridViewRow row = (GridViewRow)btn.NamingContainer;
    int i = Convert.ToInt32(row.RowIndex);
    HiddenField hdShift = (HiddenField)row.FindControl("hdShift");
    Session["Shift"] = hdShift.Value;

}
protected void BtnYes_Click(object sender, EventArgs e)
{


    if (txtRemarks.Text == "")
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Remarks is Neccesarry !')", true);
        txtRemarks.Focus();
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalB').modal('show');</script>", false);

    }
    else
    {
        SqlCommand cmd = new SqlCommand("Sp_EmployeeApprovalStatusupdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Shift", Session["Shift"].ToString());
        cmd.Parameters.AddWithValue("@AcademicYear", ddlAcademicYear.SelectedValue);
        cmd.Parameters.AddWithValue("@ExamType", DdlExamType.SelectedValue);
        cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@ExamMethod", DdlExamMethod.SelectedValue);
        cmd.Parameters.AddWithValue("@date", txtDateD.Text);
        cmd.Parameters.AddWithValue("@Status", 2);
        cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Rejected Successfully');", true);
            bindgrid();

        }

}

    protected void txtDateD_TextChanged(object sender, EventArgs e)
    {
        bindgrid();
    }
}
