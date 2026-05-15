using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Faculty_FormActiveInactive : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if ((this.Master as IndexMaster).GetLinkYesNo("FormActiveInactive") == "True")
                {

                    BindData();
                }
                else
                { Response.Redirect("~/Default.aspx"); }
                BindData();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    public void BindData()
    {
        SqlCommand cmd = new SqlCommand("SP_GetFormRoleData", con);
        cmd.CommandType = CommandType.StoredProcedure;        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdFormActiveInactive.DataSource = dt;
        grdFormActiveInactive.DataBind();

    }
    protected void grdFormActiveInactive_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdFormActiveInactive.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void grdFormActiveInactive_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdFormActiveInactive.EditIndex = -1;
        BindData();
    }
    protected void grdFormActiveInactive_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
        int id = Convert.ToInt32(grdFormActiveInactive.DataKeys[e.RowIndex].Value);
        CheckBox chkPrincipal = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkPrincipal");
        CheckBox chkHOD = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkHOD");
        CheckBox chkCourseCo = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkCourseCo");
        CheckBox chkLab = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkLab");
        CheckBox chkEventCo = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkEventCo");
        CheckBox chkFaculty = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkFaculty");
        CheckBox chkProctor = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkProctor");
        CheckBox chkCounsellor = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkCounsellor");
        CheckBox chkAdmin = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkAdmin");
        CheckBox chkRegistrar = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkRegistrar");
        CheckBox chkVC = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkVC");
        CheckBox chkHR = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkHR");
        CheckBox chkStaff = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkStaff");
        CheckBox chktransport = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chktransport");
        CheckBox chkMgmt = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkMgmt");
      //CheckBox chkInActive = (CheckBox)grdFormActiveInactive.Rows[e.RowIndex].FindControl("chkInActive");
        
        SqlCommand cmd = new SqlCommand("SP_UpdateFormRoleActive", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@id", id);
        cmd.Parameters.Add("@Principal", (chkPrincipal.Checked));
        cmd.Parameters.Add("@HOD", (chkHOD.Checked));
        cmd.Parameters.Add("@CourseCoOrdinator", (chkCourseCo.Checked));
        cmd.Parameters.Add("@EventCoOrdinator", (chkEventCo.Checked));
        cmd.Parameters.Add("@Proctor", (chkProctor.Checked));
        cmd.Parameters.Add("@LabIncharge", (chkLab.Checked));
        cmd.Parameters.Add("@Faculty", (chkFaculty.Checked));
        cmd.Parameters.Add("@Counsellor", (chkCounsellor.Checked));
        cmd.Parameters.Add("@HR", (chkHR.Checked));
        cmd.Parameters.Add("@Admin", (chkAdmin.Checked));
        cmd.Parameters.Add("@VC", (chkVC.Checked));
        cmd.Parameters.Add("@Staff", (chkStaff.Checked));
        cmd.Parameters.Add("@transport", (chktransport.Checked));
        cmd.Parameters.Add("@Mgmt", (chkMgmt.Checked));
        cmd.Parameters.Add("@UserCode", Session["uid"].ToString());
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        grdFormActiveInactive.EditIndex = -1;
        BindData();
    }
    protected void grdFormActiveInactive_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {

    }
    protected void grdFormActiveInactive_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }
    protected void grdFormActiveInactive_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {

    }
   
   
}