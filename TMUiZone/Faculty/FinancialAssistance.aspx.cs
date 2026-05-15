using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_FinancialAssistance : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string FG = Session["UserGroup"].ToString();

                //if (FG == "PRINCIPAL" || Session["uid"].ToString() == "TMU04127" || Session["uid"].ToString() == "TMU04282" || Session["uid"].ToString() == "TMU08026")
                //{
                //pnlFAApplication.Visible = false;
                //pnlReimbursement.Visible = false;
               // pnlApproval.Visible = true;
                lnkApproval.Visible = true;
                notshow.Visible = false;
                //lnkFAApplication.Visible = false;
                //lnkFAReimbursement.Visible = false;
                //pnlApplicationList.Visible = false;
                BindListforApproval(Session["uid"].ToString());
                //}
                //else
                //{
                BindList(Session["uid"].ToString());
                bindData();
                Fillform(Session["uid"].ToString());

                //lnkApproval.Visible = false;
                lnkFAApplication.Visible = true;
                lnkFAReimbursement.Visible = true;
                //}

                if (Session["uid"].ToString() == "TMU05293" || Session["uid"].ToString() == "TMU04127" || Session["uid"].ToString() == "TMU08026")
                {
                    lnkReport.Visible = true;
                }
                else
                {
                    lnkReport.Visible = false;
                }




            }
            catch (Exception ex)
            {
                Response.Redirect("../Default.aspx");
            }
            //Fillform(Session["uid"].ToString());


        }
    }
    public void BindList(string UserId)
    {
        SqlCommand cmd = new SqlCommand("proc_FAList", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", UserId);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdApplicationList.DataSource = dt;
            grdApplicationList.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        String toDate = txtDateTo.Text;
        DateTime date = DateTime.ParseExact(toDate, "dd MMM yyyy", CultureInfo.InvariantCulture);
        string outputDate = date.ToString("yyyy-MM-dd");

        String FromDate = txtDateFrom.Text;
        DateTime date1 = DateTime.ParseExact(FromDate, "dd MMM yyyy", CultureInfo.InvariantCulture);
        string outputDate1 = date1.ToString("yyyy-MM-dd");

        pnlReimbursement.Visible = false;
        pnlReApplication.Visible = false;
        pnlApproval.Visible = false;
        pnlApplicationList.Visible = false;
        pnlFAApplication.Visible = false;
        divAttachmentGrid.Visible = false;
        pnlRepport.Visible = true;
        SqlCommand cmd = new SqlCommand("proc_FAApplicationReportByDate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@toDate", txtDateTo.Text);
        cmd.Parameters.AddWithValue("@fromDate", txtDateFrom.Text);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            notshow.Visible = false;
            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/FAReport.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSetFA", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
        else
        {
            ReportViewer1.Visible = false;
            Label3.Visible = false;
            notshow.Visible = true;
            notshow.Text = "No data available to display.";
        }
    }
        public void BindListforApproval(string UserId)
    {
        SqlCommand cmd = new SqlCommand("proc_FAListforprincipal", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", UserId);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
        }
    }
    public void BindListforReApproval(string UserId)
    {
        SqlCommand cmd = new SqlCommand("proc_REFAListforprincipal", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", UserId);
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = "";
            GridView2.DataBind();
        }
    }
    public void Fillform(string UserID)
    {
        SqlCommand cmd = new SqlCommand("proc_RecordforFA", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtApplicant.Text = dt.Rows[0]["First Name"].ToString(); ;
            txtDesignation.Text = dt.Rows[0]["Designation Code"].ToString(); ;
            txtCollege.Text = dt.Rows[0]["CollegeName"].ToString(); ;
            txtDep.Text = dt.Rows[0]["Department Name"].ToString(); ;
        }
    }

    public void bindData()
    {
        SqlCommand cmd = new SqlCommand("proc_fetchAttMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlAttachtype.DataSource = dt;
            ddlAttachtype.DataTextField = "description";
            ddlAttachtype.DataValueField = "id";

            ddlAttachtype.DataBind();
        }
    }

    protected void lnkFAApplication_Click(object sender, EventArgs e)
    {
        //Response.Redirect("FinancialAssistance.aspx");

        txtPOT.Text = "";
        txtDOT.Text = "";
        txtNOC.Text = "";
        rdInternal.Checked = true;
        txtVOC.Text = "";
        txtBroadarea.Text = "";
        txtApplicantRole.Text = "";
        txtArrival.Text = "";
        txtDeparture.Text = "";
        txtFarecost.Text = "";
        txtReg.Text = "";
        pnlReimbursement.Visible = false;
        pnlReApplication.Visible = false;
        pnlApproval.Visible = false;
        pnlApplicationList.Visible = false;
        pnlFAApplication.Visible = true;
        divAttachmentGrid.Visible = false;
        btnSave1.Visible = true;

    }

    protected void lnkFAReimbursement_Click(object sender, EventArgs e)
    {
        pnlFAApplication.Visible = false;
        pnlReimbursement.Visible = true;
        pnlApproval.Visible = false;
        pnlApplicationList.Visible = false;
        pnlReApplication.Visible = false;
        //grdReb.Visible = true;


        BindReb(Session["uid"].ToString());






    }



    public void BindReb(string UserId)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_FAListReb", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Close();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                grdReb.DataSource = dt;
                grdReb.DataBind();
                div1.Visible = true;
                grdReb.Visible = true;
            }
            else
            {
                grdReb.DataSource = "";
                grdReb.DataBind();
            }
        }
        catch
        {
        }
    }





    protected void lnkApproval_Click(object sender, EventArgs e)
    {
        pnlApprovalRebursement.Visible = false;
        pnlFAApplication.Visible = false;
        pnlReimbursement.Visible = false;
        pnlApproval.Visible = true;
        pnlApplicationList.Visible = false;
        BindListforApproval(Session["uid"].ToString());
    }
    protected void lnkApprovalReburs_Click(object sender, EventArgs e)
    {
        pnlFAApplication.Visible = false;
        pnlReimbursement.Visible = false;
        pnlApproval.Visible = false;
        pnlApplicationList.Visible = false;
        pnlApprovalRebursement.Visible = true;
        pnlReApplication.Visible = false;
        BindListforReApproval(Session["uid"].ToString());
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void btnClose_Click1(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btncancel_record_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave1_Click(object sender, EventArgs e)
    {
        SqlCommand cmdvalid = new SqlCommand("proc_AttType", con);
        cmdvalid.CommandType = CommandType.StoredProcedure;
        cmdvalid.Parameters.Add("@UserID", Session["uid"].ToString());
        cmdvalid.Parameters.Add("@Arrival_date", txtArrival.Text.ToString());
        cmdvalid.Parameters.Add("@Departure_date", txtDeparture.Text.ToString());
        cmdvalid.Parameters.Add("@Designation", Session["DesignationCode"].ToString());

        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdvalid);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);



        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["LeaveCount"].ToString() == "0")
            {



                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('You can not apply finance assistance for this period'); ", true);
                return;


            }
            if (dt.Rows[0]["LeaveCount"].ToString() != dt.Rows[0]["DayCount"].ToString())
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('No. of days of applied AL/OD must be equal to No. of applying AL/OD.'); ", true);
               
                return;
            }
            if (Convert.ToInt32(dt.Rows[0]["RecordCount"]) == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('You can not apply more then two times'); ", true);
               
                return;
            }
            if (Convert.ToInt32(txtFarecost.Text) > Convert.ToInt32(dt.Rows[0]["maxfund"]))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Estimate total travel fare cost must be equal/less then " + Convert.ToInt32(dt.Rows[0]["maxfund"]) + "'); ", true);
               
                return;
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('You can not apply finance assistance for this period'); ", true);           
            return;
        }
        //return;
        string Code = "";
        SqlCommand cmd = new SqlCommand("FA_InsertApplication", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@applicant_code", Session["uid"].ToString());
        cmd.Parameters.Add("@applicant_name", Session["uname"].ToString());
        cmd.Parameters.Add("@designation", Session["DesignationCode"].ToString());
        cmd.Parameters.Add("@NOC", txtCollege.Text.ToString());
        cmd.Parameters.Add("@dept", Session["Departmentcode"].ToString());
        cmd.Parameters.Add("@POT", txtPOT.Text.ToString());
        cmd.Parameters.Add("@DOT", txtDOT.Text.ToString());
        cmd.Parameters.Add("@Name_Of_Conf", txtNOC.Text.ToString());
        if (rdInternal.Checked == true)
        {
            cmd.Parameters.Add("@Nature_Of_Conf", "1");
        }
        if (rdExternal.Checked == true)
        {
            cmd.Parameters.Add("@Nature_Of_Conf", "2");
        }
        cmd.Parameters.Add("@Venue_Of_Conf", txtVOC.Text.ToString());
        cmd.Parameters.Add("@Broad_area_Of_Conf", txtBroadarea.Text.ToString());
        cmd.Parameters.Add("@Role_Of_Applicant", txtApplicantRole.Text.ToString());
        cmd.Parameters.Add("@Arrival_date", txtArrival.Text.ToString());
        cmd.Parameters.Add("@Departure_date", txtDeparture.Text.ToString());
        cmd.Parameters.Add("@Est_Total_Travel", txtFarecost.Text.ToString());
        cmd.Parameters.Add("@Reg_Fee_Detail", txtReg.Text.ToString());
        cmd.Parameters.Add("@PrincipalID", Session["hod_ID_Leave1"].ToString());
        cmd.Parameters.Add("@SecondApprovalID", "TMU04127");
        cmd.Parameters.Add("@ThirdApprovalID", "TMU05293");
        cmd.Parameters.Add("@VCID", "TMU08026");
        cmd.Parameters.Add("@leaveAgainst", dt.Rows[0]["leaveAgainst"].ToString());
        cmd.Parameters.Add("@Status", 1);
        cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100);
        cmd.Parameters["@OrderNo"].Direction = ParameterDirection.Output;
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();

        Code = cmd.Parameters["@OrderNo"].Value.ToString();


        con.Close();


       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Application No. " + Code + " is Generate Successfully'); document.location.href='FinancialAssistance.aspx';", true);
        
        
    }


    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        if (grdDocument.Rows.Count < 4)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload all documents')", true);
            return;
        }
        SqlCommand cmdvalid = new SqlCommand("proc_AttType", con);
        cmdvalid.CommandType = CommandType.StoredProcedure;
        cmdvalid.Parameters.Add("@UserID", Session["uid"].ToString());
        cmdvalid.Parameters.Add("@Arrival_date", txtArrival.Text.ToString());
        cmdvalid.Parameters.Add("@Departure_date", txtDeparture.Text.ToString());
        cmdvalid.Parameters.Add("@Designation", Session["DesignationCode"].ToString());

        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdvalid);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);



        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["LeaveCount"].ToString() == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You can not apply finance assistance for this period')", true);
                return;
            }
            if (dt.Rows[0]["LeaveCount"].ToString() != dt.Rows[0]["DayCount"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No. of days of applied AL/OD must be equal to No. of applying AL/OD.')", true);
                return;
            }
            if (Convert.ToInt32(dt.Rows[0]["RecordCount"]) == 2)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You can not apply more then two times')", true);
                return;
            }
            if (Convert.ToInt32(txtFarecost.Text) > Convert.ToInt32(dt.Rows[0]["maxfund"]))
            {

              
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Estimate total travel fare cost must be equal/less then " + Convert.ToInt32(dt.Rows[0]["maxfund"]) + "')", true);
                return;
            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You can not apply finance assistance for this period')", true);
            return;
        }
        //return;
        string Code = "";
        SqlCommand cmd = new SqlCommand("FA_UpdateApplication", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ApplicationNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@applicant_code", Session["uid"].ToString());
        cmd.Parameters.Add("@applicant_name", Session["uname"].ToString());
        cmd.Parameters.Add("@designation", Session["DesignationCode"].ToString());
        cmd.Parameters.Add("@NOC", txtCollege.Text.ToString());
        cmd.Parameters.Add("@dept", Session["Departmentcode"].ToString());
        cmd.Parameters.Add("@POT", txtPOT.Text.ToString());
        cmd.Parameters.Add("@DOT", txtDOT.Text.ToString());
        cmd.Parameters.Add("@Name_Of_Conf", txtNOC.Text.ToString());
        if (rdInternal.Checked == true)
        {
            cmd.Parameters.Add("@Nature_Of_Conf", "1");
        }
        if (rdExternal.Checked == true)
        {
            cmd.Parameters.Add("@Nature_Of_Conf", "2");
        }
        cmd.Parameters.Add("@Venue_Of_Conf", txtVOC.Text.ToString());
        cmd.Parameters.Add("@Broad_area_Of_Conf", txtBroadarea.Text.ToString());
        cmd.Parameters.Add("@Role_Of_Applicant", txtApplicantRole.Text.ToString());
        cmd.Parameters.Add("@Arrival_date", txtArrival.Text.ToString());
        cmd.Parameters.Add("@Departure_date", txtDeparture.Text.ToString());
        cmd.Parameters.Add("@Est_Total_Travel", txtFarecost.Text.ToString());
        cmd.Parameters.Add("@Reg_Fee_Detail", txtReg.Text.ToString());
        cmd.Parameters.Add("@leaveAgainst", dt.Rows[0]["leaveAgainst"].ToString());
        cmd.Parameters.Add("@Status", 2);

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();




        con.Close();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", " alert('Your Application No. " + hdfApplicationNo.Value + " is Update Successfully'); document.location.href='FinancialAssistance.aspx';", true);



        
      
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {

        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("FA_GetApplicationByNo '" + Complaint + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            btnSubmit1.Visible = true;
            btnSave1.Visible = false;
            divUploadDoc.Visible = true;
            pnlApplicationList.Visible = false;
            pnlFAApplication.Visible = true;
            pnlApproval.Visible = false;
            txtPOT.Text = dt.Rows[0]["POT"].ToString();
            txtDOT.Text = dt.Rows[0]["DOT"].ToString(); ;
            txtNOC.Text = dt.Rows[0]["Name_Of_Conf"].ToString();
            if (dt.Rows[0]["PrincipalStatus"].ToString() == "")
            {
                btnSubmit1.Visible = true;
                btnSubmit.Visible = true;

            }
            else
            {
                btnSubmit1.Visible = false;
                btnSubmit.Visible = false;
            }
            //if (dt.Rows[0]["SecondApprovalStatus"].ToString() == "2")
            //{
            //    btnSubmit1.Visible = true;
            //    btnSubmit.Visible = true;

            //}
            //if (dt.Rows[0]["ThirdApprovalStatus"].ToString() == "2")
            //{
            //    btnSubmit1.Visible = true;
            //    btnSubmit.Visible = true;

            //}
            //if (dt.Rows[0]["VCApprovalStatus"].ToString() == "2")
            //{
            //    btnSubmit1.Visible = true;
            //    btnSubmit.Visible = true;

            //}
            if (dt.Rows[0]["Nature_Of_Conf"].ToString() == "1")
            {
                rdInternal.Checked = true;
            }
            if (dt.Rows[0]["Nature_Of_Conf"].ToString() == "2")
            {
                rdExternal.Checked = true;
            }
            txtVOC.Text = dt.Rows[0]["Venue_Of_Conf"].ToString();
            txtBroadarea.Text = dt.Rows[0]["Broad_area_Of_Conf"].ToString();
            txtApplicantRole.Text = dt.Rows[0]["Role_Of_Applicant"].ToString();
            txtArrival.Text = dt.Rows[0]["Arrival_date"].ToString();
            txtDeparture.Text = dt.Rows[0]["Departure_date"].ToString();
            txtFarecost.Text = dt.Rows[0]["Est_Total_Travel"].ToString();
            txtReg.Text = dt.Rows[0]["Reg_Fee_Detail"].ToString();
            bindAttachment();
        }
        else
        {
            btnSubmit1.Visible = false;
            btnSave1.Visible = true;
            divUploadDoc.Visible = false;

        }
    }


    protected void lnkSelectRe_Click(object sender, EventArgs e)
    {

        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("FA_GetApplicationByNoRe '" + Complaint + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {


            if (dt.Rows[0]["PrincipalStatus"].ToString() == "" || dt.Rows[0]["Resubmit"].ToString() == "1")
            {
                btnSubmitRe.Visible = true;
                btnSubmitReAttach.Visible = true;
                txtReimbursementAMT.Enabled = true;
            }
            else
            {
                btnSubmitRe.Visible = false;
                btnSubmitReAttach.Visible = false;
                txtReimbursementAMT.Enabled = false;
            }
            btnApproveRe.Visible = false;
            btnRejectRe.Visible = false;
            pnlReimbursement.Visible = false;
            pnlReApplication.Visible = true;
            divReDoc.Visible = true;
            pnlReimbursement.Visible = false;
            pnlReApplication.Visible = true;
            divReDoc.Visible = true;
            txtApprovalAmount.Text= dt.Rows[0]["Approval_amount"].ToString();
            txtApprovalAmountRemark.Text = dt.Rows[0]["Approval_Amount_Remark"].ToString();
            txtAppRe.Text = dt.Rows[0]["applicant_name"].ToString();
            txtDesigRe.Text = dt.Rows[0]["designation"].ToString();
            txtNOCRe.Text = dt.Rows[0]["NOC"].ToString();
            txtDeptRe.Text = dt.Rows[0]["dept"].ToString();
            txtNOCSW.Text = dt.Rows[0]["Name_Of_Cofirence"].ToString();
            //txtNOCSW.Text = dt.Rows[0]["Name_Of_Conf"].ToString();
            txtVOCSW.Text = dt.Rows[0]["Venue_Of_Cofirence"].ToString();
            txtRegistrationRe.Text = dt.Rows[0]["Reg_Fee_Detail"].ToString();
            txtReimbursementAMT.Text= dt.Rows[0]["ReimbursementAmount"].ToString(); 
            bindReAttachmentMaster();
            bindAttachmentRe();
        }
        else
        {
            // btnSubmitRe.Visible = false;
            //btnSave1.Visible = true;
            //divUploadDoc.Visible = false;

        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("FinancialAssistance.aspx");
    }

    protected void lnkSelect1_Click(object sender, EventArgs e)
    {
        txtPOT.Enabled = false;
        txtDOT.Enabled = false;
        txtNOC.Enabled = false;
        txtVOC.Enabled = false;
        txtApplicantRole.Enabled = false;
        txtArrival.Enabled = false;
        txtDeparture.Enabled = false;
        txtFarecost.Enabled = false;
        txtReg.Enabled = false;

        txtBroadarea.Enabled = false;








        divUploadDoc.Visible = true;
        divUploadDoc.Visible = false;
        btnSave1.Visible = false;
        btnPrincipalApprove.Visible = true;
        btnPrincipalReject.Visible = true;
        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("FA_GetApplicationByNo '" + Complaint + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            pnlApproval.Visible = false;
            pnlFAApplication.Visible = true;
            txtApplicant.Text = dt.Rows[0]["applicant_name"].ToString();
            txtDesignation.Text = dt.Rows[0]["designation"].ToString();
            txtCollege.Text = dt.Rows[0]["NOC"].ToString();
            txtDep.Text = dt.Rows[0]["dept"].ToString();
            txtPOT.Text = dt.Rows[0]["POT"].ToString();
            txtDOT.Text = dt.Rows[0]["DOT"].ToString(); ;
            txtNOC.Text = dt.Rows[0]["Name_Of_Conf"].ToString();
            if (dt.Rows[0]["Nature_Of_Conf"].ToString() == "1")
            {
                rdInternal.Checked = true;
            }
            if (dt.Rows[0]["Nature_Of_Conf"].ToString() == "2")
            {
                rdExternal.Checked = true;
            }
            txtVOC.Text = dt.Rows[0]["Venue_Of_Conf"].ToString();
            txtBroadarea.Text = dt.Rows[0]["Broad_area_Of_Conf"].ToString();
            txtApplicantRole.Text = dt.Rows[0]["Role_Of_Applicant"].ToString();
            txtArrival.Text = dt.Rows[0]["Arrival_date"].ToString();
            txtDeparture.Text = dt.Rows[0]["Departure_date"].ToString();
            txtFarecost.Text = dt.Rows[0]["Est_Total_Travel"].ToString();
            txtReg.Text = dt.Rows[0]["Reg_Fee_Detail"].ToString();
            bindAttachmentP();
        }
    }


    protected void lnkSelect123_Click(object sender, EventArgs e)
    {
        if(Session["uid"].ToString()== "TMU04127")
        {
            txtApprovalAmount.Enabled = true;
            txtApprovalAmountRemark.Enabled = true;
        }
        else
        {
            txtApprovalAmount.Enabled = false;
            txtApprovalAmountRemark.Enabled = false;
        }
        divUploadDoc.Visible = true;
        btnSubmitRe.Visible = false;
        btnSubmitReAttach.Visible = false;
        btnApproveRe.Visible = true;
        btnRejectRe.Visible = true;
        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("FA_GetApplicationByNoRe '" + Complaint + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if(dt.Rows[0]["VCApprovalStatus"].ToString()=="1")
            {
                btnApproveRe.Visible = false;
                btnRejectRe.Visible = false;
            }
            else
            {
                btnApproveRe.Visible = true;
                btnRejectRe.Visible = true;
            }
            pnlReimbursement.Visible = false;
            pnlApprovalRebursement.Visible = false;
            pnlReApplication.Visible = true;
            divReDoc.Visible = true;

            txtAppRe.Text = dt.Rows[0]["applicant_name"].ToString();
            txtDesigRe.Text = dt.Rows[0]["designation"].ToString();
            txtNOCRe.Text = dt.Rows[0]["NOC"].ToString();
            txtDeptRe.Text = dt.Rows[0]["dept"].ToString();
            txtNOCSW.Text = dt.Rows[0]["Name_Of_Cofirence"].ToString();
            txtVOCSW.Text = dt.Rows[0]["Venue_Of_Cofirence"].ToString();
            txtRegistrationRe.Text = dt.Rows[0]["Reg_Fee_Detail"].ToString();
            txtReimbursementAMT.Text = dt.Rows[0]["ReimbursementAmount"].ToString(); 
            txtApprovalAmount.Text= dt.Rows[0]["Approval_amount"].ToString();
            txtApprovalAmountRemark.Text = dt.Rows[0]["Approval_Amount_Remark"].ToString();
            bindAttachmentRe();
            grdRebAttach.Columns[6].Visible = false;
        }
    }


    protected void btnPrincipalApprove_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_FAApproval]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Application No. " + hdfApplicationNo.Value + " is Update Successfully'); document.location.href='FinancialAssistance.aspx';", true);

       

        






    }

    protected void btnPrincipalReject_Click(object sender, EventArgs e)
    {
       



        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal5').modal('show');</script>", false);
    }


    //SqlCommand cmd = new SqlCommand("[SP_FAReject]", con);
    //cmd.CommandType = CommandType.StoredProcedure;
    //cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
    //cmd.Parameters.Add("@UseId", Session["uid"].ToString());

    //if (con.State == ConnectionState.Closed)
    //    con.Open();
    //cmd.ExecuteNonQuery();
    //con.Close();

    //ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application No. " + hdfApplicationNo.Value + " Reject Successfully'); document.location.href='FinancialAssistance.aspx';", true);









    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
        string contentType = FileUpload1.PostedFile.ContentType;
        using (Stream fs = FileUpload1.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                {
                    SqlCommand cmd = new SqlCommand("proc_InsertFAAttachment", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ApplicationNo", hdfApplicationNo.Value);
                    cmd.Parameters.Add("@UserId", Session["uid"].ToString());
                    cmd.Parameters.Add("@Attachment", bytes);
                    cmd.Parameters.Add("@AttachmentType", contentType);
                    cmd.Parameters.Add("@FileName", filename);
                    cmd.Parameters.Add("@DocName", ddlAttachtype.SelectedItem.Text);
                    if (con2.State == ConnectionState.Closed)
                        con2.Open();
                    cmd.ExecuteNonQuery();
                    con2.Close();



                }
            }
        }
        bindAttachment();

    }





    public void bindAttachment()
    {
        SqlCommand cmd = new SqlCommand("proc_GetFAAttachment", con);
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@ApplicationNo", hdfApplicationNo.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        if (con.State != ConnectionState.Closed)
            con.Close();
        con.Open();
        da.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            if (Convert.ToInt32(dt1.Rows[0]["Approval"]) > 1)
            {
                grdDocument.Columns[6].Visible = false;
            }

            divAttachmentGrid.Visible = true;
            grdDocument.DataSource = dt1;
            grdDocument.DataBind();
        }
        else
        {
            divAttachmentGrid.Visible = false;
            grdDocument.DataSource = "";
            grdDocument.DataBind();
        }
    }

    public void bindAttachmentP()
    {
        SqlCommand cmd = new SqlCommand("proc_GetFAAttachment", con);
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@ApplicationNo", hdfApplicationNo.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        if (con.State != ConnectionState.Closed)
            con.Close();
        con.Open();
        da.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            grdDocument.Columns[6].Visible = false;
            divAttachmentGrid.Visible = true;
            grdDocument.DataSource = dt1;
            grdDocument.DataBind();
        }
        else
        {
            divAttachmentGrid.Visible = false;
            grdDocument.DataSource = "";
            grdDocument.DataBind();
        }
    }




    protected void DownloadInboxFile(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Attachment,[Content Type],[File Name] from tbl_FAAttachment where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];
                    contentType = sdr["Content Type"].ToString();
                    fileName = sdr["File Name"].ToString();
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();

    }

    protected void DeleteFile(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;


        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "delete from tbl_FAAttachment where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }
        }
        bindAttachment();
    }




    protected void DownloadInboxFileRe(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;

        byte[] bytes;
        string fileName, contentType;
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "select Attachment,[Content Type],[File Name] from tbl_FAAttachmentRe where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                using (SqlDataReader sdr = cmd2.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachment"];
                    contentType = sdr["Content Type"].ToString();
                    fileName = sdr["File Name"].ToString();
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();

    }

    protected void DeleteFileRe(object sender, EventArgs e)
    {
        string Eval = (sender as LinkButton).CommandArgument;
        string AssignmentNo = (sender as LinkButton).CommandArgument;


        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            using (SqlCommand cmd2 = new SqlCommand())
            {
                cmd2.CommandText = "delete from tbl_FAAttachmentRe where ID='" + AssignmentNo + "' ";
                cmd2.Connection = con2;
                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();
            }
        }
        bindAttachmentRe();
    }






    protected void btnbackRe_Click(object sender, EventArgs e)
    {




    }

    protected void lnkdeleteReb_Click(object sender, EventArgs e)
    {

    }

    protected void btnSaveRe_Click(object sender, EventArgs e)
    {

    }

    protected void btnSubmitRe_Click(object sender, EventArgs e)
    {
        int Attcount = 0;
        for(int i=0; i< grdRebAttach.Rows.Count;i++)
        {
           string s= grdRebAttach.Rows[i].Cells[1].Text.ToString();

            if(s== "Registration fee receipt" || s == "Air/Rail/Bus Ticket" || s == "Certificate of Participation" || s == "Report of the event with Photographs")
            {
                Attcount++;
            }
        }


        if (Attcount < 4)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload all documents')", true);
            return;
        }
        string Code = "";
        SqlCommand cmd = new SqlCommand("FA_InsertApplicationRe", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@applicationNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@applicant_code", Session["uid"].ToString());
        cmd.Parameters.Add("@applicant_name", Session["uname"].ToString());
        cmd.Parameters.Add("@designation", Session["DesignationCode"].ToString());
        cmd.Parameters.Add("@NOC", txtCollege.Text.ToString());
        cmd.Parameters.Add("@dept", Session["Departmentcode"].ToString());
        cmd.Parameters.Add("@NameOfConfirence", txtNOCSW.Text);
        cmd.Parameters.Add("@Venue_Of_Cofirence", txtVOCSW.Text);

        cmd.Parameters.Add("@Reg_Fee_Detail", txtRegistrationRe.Text.ToString());
        cmd.Parameters.Add("@PrincipalID", Session["hod_ID_Leave1"].ToString());
        cmd.Parameters.Add("@SecondApprovalID", "TMU04127");
        cmd.Parameters.Add("@ThirdApprovalID", "TMU05293");
        cmd.Parameters.Add("@VCID", "TMU08026");

        cmd.Parameters.Add("@Status", 1);
        cmd.Parameters.Add("@ReimbursementAmount", txtReimbursementAMT.Text);
        
        cmd.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100);
        cmd.Parameters["@OrderNo"].Direction = ParameterDirection.Output;
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();

        Code = cmd.Parameters["@OrderNo"].Value.ToString();


        con.Close();
      
     

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Your Reimbursement Application No. " + Code + " is Generate Successfully'); document.location.href='FinancialAssistance.aspx';", true);



    }



    protected void btnSubmitReAttach_Click(object sender, EventArgs e)
    {
        string filename = Path.GetFileName(flAttachmentRe.PostedFile.FileName);
        string contentType = flAttachmentRe.PostedFile.ContentType;
        using (Stream fs = flAttachmentRe.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
                {
                    SqlCommand cmd = new SqlCommand("proc_InsertFAAttachmentRe", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ApplicationNo", hdfApplicationNo.Value);
                    cmd.Parameters.Add("@UserId", Session["uid"].ToString());
                    cmd.Parameters.Add("@Attachment", bytes);
                    cmd.Parameters.Add("@AttachmentType", contentType);
                    cmd.Parameters.Add("@FileName", filename);
                    cmd.Parameters.Add("@DocName", drpReimbursement.SelectedItem.Text);
                    if (con2.State == ConnectionState.Closed)
                        con2.Open();
                    cmd.ExecuteNonQuery();
                    con2.Close();



                }
            }
        }
        bindAttachmentRe();




    }
    public void bindAttachmentRe()
    {
        SqlCommand cmd = new SqlCommand("proc_GetFAAttachmentRe", con);
        cmd.Parameters.AddWithValue("@Fid", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@ApplicationNo", hdfApplicationNo.Value);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        if (con.State != ConnectionState.Closed)
            con.Close();
        con.Open();
        da.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            if (dt1.Rows[0]["ApprovalStatus"].ToString() == "0")
            {
                grdRebAttach.Columns[6].Visible = true;
            }
            else
            {
                grdRebAttach.Columns[6].Visible = false;
            }
            divRebAttach.Visible = true;
            grdRebAttach.DataSource = dt1;
            grdRebAttach.DataBind();
        }
        else
        {
            divRebAttach.Visible = false;
            grdRebAttach.DataSource = "";
            grdRebAttach.DataBind();
        }
    }

    protected void lnkReimburs_Click(object sender, EventArgs e)
    {
        btnApproveRe.Visible = false;
        btnRejectRe.Visible = false;
        pnlApproval.Visible = false;
        bindReAttachmentMaster();
        txtReimbursementAMT.Enabled = true;


        pnlFAApplication.Visible = false;
        pnlReimbursement.Visible = false;

        pnlApplicationList.Visible = false;

        string Complaint = (sender as LinkButton).CommandArgument;
        hdfApplicationNo.Value = Complaint;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("FA_GetApplicationByNo '" + Complaint + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            pnlReimbursement.Visible = false;
            pnlReApplication.Visible = true;
            divReDoc.Visible = true;
            txtAppRe.Text = dt.Rows[0]["applicant_name"].ToString();
            txtDesigRe.Text = dt.Rows[0]["designation"].ToString();
            txtNOCRe.Text = dt.Rows[0]["NOC"].ToString();
            txtDeptRe.Text = dt.Rows[0]["dept"].ToString();
            txtNOCSW.Text = dt.Rows[0]["Name_Of_Conf"].ToString();
            txtVOCSW.Text = dt.Rows[0]["Venue_Of_Conf"].ToString();
            txtRegistrationRe.Text = dt.Rows[0]["Reg_Fee_Detail"].ToString();
            bindAttachmentRe();
        }
        else
        {
            // btnSubmitRe.Visible = false;
            //btnSave1.Visible = true;
            //divUploadDoc.Visible = false;

        }
    }
    public void bindReAttachmentMaster()
    {
        SqlCommand cmd = new SqlCommand("proc_fetchAttMasterRE", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpReimbursement.DataSource = dt;
            drpReimbursement.DataTextField = "description";
            drpReimbursement.DataValueField = "id";

            drpReimbursement.DataBind();
        }
    }


    protected void btnApproveRe_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_FAApprovalRe]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@ApprovalAmount", txtApprovalAmount.Text);
        cmd.Parameters.Add("@ApprovalAmountRemark", txtApprovalAmountRemark.Text);

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();



        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application No. " + hdfApplicationNo.Value + " Approve Successfully'); document.location.href='FinancialAssistance.aspx';", true);


       


    }


    protected void btnRejectRe_Click(object sender, EventArgs e)
    {



        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal6').modal('show');</script>", false);
        





    }


    protected void Button4_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand("[SP_FAReject]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", TextBox1.Text.ToString());
        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application No. " + hdfApplicationNo.Value + " Reject Successfully'); document.location.href='FinancialAssistance.aspx';", true);




    }

    protected void btnRejectPop_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[SP_FARejectRe]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ComplaintNo", hdfApplicationNo.Value);
        cmd.Parameters.Add("@UseId", Session["uid"].ToString());
        cmd.Parameters.Add("@Remark", txtRejectRemark.Text);

        if (con.State == ConnectionState.Closed)
            con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('Application No. " + hdfApplicationNo.Value + " Reject Successfully'); document.location.href='FinancialAssistance.aspx';", true);






    }

    protected void lnkReport_Click(object sender, EventArgs e)
    {

        pnlReimbursement.Visible = false;
        pnlReApplication.Visible = false;
        pnlApproval.Visible = false;
        pnlApplicationList.Visible = false;
        pnlFAApplication.Visible = false;
        divAttachmentGrid.Visible = false;
        pnlRepport.Visible = true;
        //SqlCommand cmd = new SqlCommand("proc_FAApplicationReport", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        //con.Open();

        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //con.Close();
        //da.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    ReportViewer1.Visible = true;
        //    ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //    ReportViewer1.Visible = true;
        //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/FAReport.rdlc");
        //    ReportDataSource datasource = new ReportDataSource("DataSetFA", dt);
        //    ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportViewer1.LocalReport.DataSources.Add(datasource);
        //}

    }
}