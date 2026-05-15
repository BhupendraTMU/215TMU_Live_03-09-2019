using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Resignationformlist : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                getresignationlist();
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }
    protected void grdresignationlist_PageIndexChanged(object sender, EventArgs e)
    {

    }
    public void getresignationlist()
    {

        SqlCommand cmd = new SqlCommand("proc_getresignatiolist", con1);
        cmd.Parameters.AddWithValue("@HOD", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EMPName", txtEmployee.Text);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdresignationlist.DataSource = dtCL;
        grdresignationlist.DataBind();
    }

    protected void grdresignationlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdresignationlist.PageIndex = e.NewPageIndex;

        getresignationlist();
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        GridViewDetails.Hide();
    }

    protected void lnkbutton_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
        int index = row.RowIndex;
        Label UserId = (Label)grdresignationlist.Rows[index].FindControl("lblemployeecode");

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select CASE WHEN TRY_CAST([Date Of Joining] AS DATETIME) IS NOT NULL THEN FORMAT(TRY_CAST([Date Of Joining]  AS DATETIME), 'dd-MM-yyyy hh:mm:ss tt') END AS FormattedDate1, CASE WHEN TRY_CAST([Date Of Resignation] AS DATETIME) IS NOT NULL THEN FORMAT(TRY_CAST([Date Of Resignation]  AS DATETIME), 'dd-MM-yyyy hh:mm:ss tt') END AS FormattedDate,* from tble_Exit_Interview_Form WHERE [Employee Code]='" + UserId.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        txtEmployeeCode.Text = dt.Rows[0]["Employee Code"].ToString();
        txtEmployeeName.Text = dt.Rows[0]["Employee Name"].ToString();
        lbldesignation.Text = dt.Rows[0]["Designation"].ToString();
        lblInstitution.Text = dt.Rows[0]["Institution"].ToString();
        lblnameofHOD.Text = dt.Rows[0]["HOD"].ToString();
        lblDateofJoining.Text = dt.Rows[0]["FormattedDate1"].ToString();

        txtapplyingresignation.Text = dt.Rows[0]["FormattedDate"].ToString();

        txtEmail.Text = dt.Rows[0]["Email ID"].ToString();
        txtofficial.Text = dt.Rows[0]["Confirm_Email"].ToString();
        txtMobileNo.Text = dt.Rows[0]["Mobile No"].ToString();
        txtnoticeperiod.Text = dt.Rows[0]["Total Duration"].ToString();
        txtemployeetype.Text= dt.Rows[0]["Employee_type"].ToString();
        txtbetterprofile.Text = dt.Rows[0]["Better Profile"].ToString();
        txtBetterEmoluments.Text = dt.Rows[0]["Better Emolument"].ToString();
        txtPersonalReason.Text = dt.Rows[0]["Personal Reason"].ToString();
        txtanyotherreason.Text = dt.Rows[0]["Any Other Reason"].ToString();
        txtNameofOrgJoining.Text = dt.Rows[0]["Name of Organisation Joining"].ToString();
        txttriggerdlookforchange.Text = dt.Rows[0]["Triggered for Changed"].ToString();
        txtGoodwithTMU.Text = dt.Rows[0]["Experience With Organisation"].ToString();
        txtDifficultwithtmu.Text = dt.Rows[0]["Updating Experience with Organisation"].ToString();
        txtovalallratingResponse.Text = dt.Rows[0]["Rating of Organisation Response"].ToString();
        txtovalallratingRemarks.Text = dt.Rows[0]["Rating of Organisation Remarks"].ToString();
        txtperformancemeasurementResponse.Text = dt.Rows[0]["Feedback_Perf System Response"].ToString();
        txtperformancemeasurementRemarks.Text = dt.Rows[0]["Feedback_Perf System Remarks"].ToString();
        txtCommunicationResponse.Text = dt.Rows[0]["Communication with Organisation Response"].ToString();
        txtCommunicationRemarks.Text = dt.Rows[0]["Communication with Organisation Remarks"].ToString();
        txtRecruitmentResponse.Text = dt.Rows[0]["Recruitment Induction in Organisation Response"].ToString();
        txtRecruitmentRemarks.Text = dt.Rows[0]["Recruitment Induction in Organisation Remarks"].ToString();
        txtWillingnessResponse.Text = dt.Rows[0]["Willing Ness Problem Response"].ToString();
        txtWillingnessRemarks.Text = dt.Rows[0]["Willing Ness Problem Remarks"].ToString();
        txtRecruitment_Proc_Response.Text = dt.Rows[0]["Salary Structure Response"].ToString();
        txtRecruitment_Proc_Remarks.Text = dt.Rows[0]["Salary Structure Remarks"].ToString();
        txtWorkingEnviron_Response.Text = dt.Rows[0]["Working Environment Response"].ToString();
        txtWorkingEnviron_Remarks.Text = dt.Rows[0]["Working Environment Remarks"].ToString();
        txtgrowthOpportuniti_Response.Text = dt.Rows[0]["Growth Opportunities Response"].ToString();
        txtgrowthOpportuniti_Remarks.Text = dt.Rows[0]["Growth Opportunities Remarks"].ToString();
        txteffectiveness_Response.Text = dt.Rows[0]["Effectiveness of Appraisal Response"].ToString();
        txteffectiveness_Remarks.Text = dt.Rows[0]["Effectiveness of Appraisal Remarks"].ToString();
        txtAnyOtherComment.Text = dt.Rows[0]["Any Other Comments of Response"].ToString();
        
        
        GridViewDetails.Show();
    }

    //protected void chkall_CheckedChanged(object sender, EventArgs e)
    //{
    //    CheckBox checkBoxheader = (CheckBox)grdresignationlist.HeaderRow.FindControl("chkAll");
    //    foreach (GridViewRow Row in grdresignationlist.Rows)
    //    {
    //        CheckBox checkRows = (CheckBox)Row.FindControl("chkemployee");
    //        if (checkBoxheader.Checked == true)
    //        {
    //            checkRows.Checked = true;

    //        }
    //        else
    //        {
    //            checkRows.Checked = false;
    //        }
    //    }
    //}
    protected void Chkemployee_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBoxheader = (CheckBox)grdresignationlist.HeaderRow.FindControl("chkemployee");
        foreach (GridViewRow Row in grdresignationlist.Rows)
        {
            CheckBox checkRows = (CheckBox)Row.FindControl("chkemployee");
            if (checkRows.Checked == false)
            {
                checkBoxheader.Checked = false;

            }
            else
            {
                //checkBoxheader.Checked = true;
            }
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {


        try
        {
            int i = 0;
            foreach (GridViewRow row in grdresignationlist.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("Chkemployee");
                Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
                var id = grdresignationlist.DataKeys[row.RowIndex].Value;
                SqlCommand cmd = new SqlCommand("[HRMSPortal].dbo.pro_approve", con);
                if (check.Checked == true)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeCode", lblemployeecode.Text);
                    cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@Hod_Status", "Approved");
                    
                    cmd.Parameters.AddWithValue("@Hr_Status", "Approved");
                  
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    i++;
                }




            }
            if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Resignation Form Approved')", true); }
            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
            //BtnSubmit.Visible = false;
            getresignationlist();
        }
        catch (Exception ex)
        {
        }
    }

    //public void hidestatus()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    //    {
    //        using (SqlCommand cmd = new SqlCommand("select * from [tble_Exit_Interview_Form] where [Employee Code]='" + Session["uid"].ToString() + "'", con))
    //        {
    //            con.Open();
    //            SqlDataReader dr = cmd.ExecuteReader();
    //            if (dr.Read())
    //            {
    //                string Employee_type = dr["Employee_type"].ToString();
    //                con.Close();
    //                if (Employee_type == "TEACH")

    //                {
    //                    lblvcstatus.Visible = true;
    //                    vcss.Visible = true;
    //                }
    //                else
    //                {
    //                    lblvcstatus.Visible = false;
    //                    vcss.Visible = false;
    //                }


    //            }
    //        }
    //    }
    //}

    protected void BtnRejected_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            foreach (GridViewRow row in grdresignationlist.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("Chkemployee");
                Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
                var id = grdresignationlist.DataKeys[row.RowIndex].Value;
                SqlCommand cmd = new SqlCommand("[HRMSPortal].dbo.pro_Rejected", con);
                if (check.Checked == true)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeCode", lblemployeecode.Text);
                    cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@Hod_Status", "Rejected");
                    cmd.Parameters.AddWithValue("@Status", "Pending");
                    cmd.Parameters.AddWithValue("@Hr_Status", "Rejected");
                    cmd.Parameters.AddWithValue("@Registrar_Approval", "Rejected");
                    cmd.Parameters.AddWithValue("@VC_Approval", "Rejected");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    i++;

                }




            }
            if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Resignation Form Rejected')", true); }
            else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
            //BtnSubmit.Visible = false;
            getresignationlist();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        getresignationlist();
    }
}



