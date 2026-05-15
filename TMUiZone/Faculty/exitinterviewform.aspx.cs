using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_exitinterviewform : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
    string constr1 = ConfigurationSettings.AppSettings["strPortal"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                txtapplyingresignation.Text = DateTime.Now.ToString("dd-MM-yyyy").ToString();
                showsaveRecord();
                hidestatus();
                //Employeeinfo();
                //desginame();(

            }

            catch
            {
                Response.Redirect("../Default.aspx");
            }
        }
    }






    public void Employeeinfo()
    {
        using (SqlConnection con1 = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("GetUserDetailsForExitForm", con1))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserID", Session["uid"].ToString());
                con1.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtEmployeeName.Text = dt.Rows[0]["First Name"].ToString();
                    txtEmployeeCode.Text = dt.Rows[0]["No_"].ToString();
                    lblInstitution.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
                    lblnameofHOD.Text = dt.Rows[0]["HOD"].ToString();
                    DateTime doj = Convert.ToDateTime(dt.Rows[0]["Employment Date"]);
                    lblDateofJoining.Text = doj.ToString("dd-MM-yyyy");
                    if (dt.Rows[0]["Resignation Date"].ToString() == "")
                    {
                        txtapplyingresignation.Text = DateTime.Now.ToString("dd-MM-yyyy").ToString();
                    }
                    else
                    {

                        
                        DateTime doj1 = Convert.ToDateTime(dt.Rows[0]["Resignation Date"]);
                        txtapplyingresignation.Text = doj1.ToString("dd-MM-yyyy");
                    }
                    txtnoticeperiod.Text = dt.Rows[0]["Employee Posting Group1"].ToString();
                    txtemployeetype.Text = dt.Rows[0]["Employee Posting Group"].ToString();
                    txtEmail.Text = dt.Rows[0]["E-Mail"].ToString();
                    txtofficial.Text = dt.Rows[0]["Company E-Mail"].ToString();
                    txtMobileNo.Text = dt.Rows[0]["Mobile Phone No_"].ToString();
                }
                con1.Close();
            }
        }
    }
    public void desginame()
    {
        using (SqlConnection con1 = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select [Designation Description] from [TMU$Designation Master]INNER JOIN  TMU$Employee on  TMU$Employee.[Designation Code]=[TMU$Designation Master].[Designation Code] where TMU$Employee.No_='" + Session["uid"].ToString() + "'", con1))
            {
                con1.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                lbldesignation.Text = dr["Designation Description"].ToString();
                con1.Close();
            }
        }
    }
    public void showsaveRecord()
    {
        using (SqlConnection con1 = new SqlConnection(constr1))
        {
            using (SqlCommand cmd = new SqlCommand("select CASE WHEN TRY_CAST([Date Of Joining] AS DATETIME) IS NOT NULL THEN FORMAT(TRY_CAST([Date Of Joining]  AS DATETIME), 'dd-MM-yyyy hh:mm:ss tt') END AS FormattedDate1, CASE WHEN TRY_CAST([Date Of Resignation] AS DATETIME) IS NOT NULL THEN FORMAT(TRY_CAST([Date Of Resignation]  AS DATETIME), 'dd-MM-yyyy hh:mm:ss tt') END AS FormattedDate,* from [tble_Exit_Interview_Form] where [Employee Code]='" + Session["uid"].ToString() + "'   and (Hod_Status!='Rejected' or Hr_Status!='Rejected By HR')", con1))

            {
                con1.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string status = dr["Status"].ToString();
                    string Hr_status = dr["Hr_Status"].ToString();
                    string Hod_status = dr["Hod_status"].ToString();
                    string VC_Approval = dr["VC_Approval"].ToString();
                    string Registrar_Approval = dr["Registrar_Approval"].ToString();
                    if (status == "Pending")
                    {
                        Panel1.Enabled = true;
                        txtEmployeeName.Text = dr["Employee Name"].ToString();
                        txtEmployeeCode.Text = dr["Employee Code"].ToString();
                        lbldesignation.Text = dr["Designation"].ToString();
                        lblInstitution.Text = dr["Institution"].ToString();
                        lblnameofHOD.Text = dr["HOD"].ToString();
                        //   lblDateofJoining.Text = dr["Date Of Joining"].ToString();
                        // txtapplyingresignation.Text = dr["Date Of Resignation"].ToString();
                        //lblDateofJoining.Text = dr["FormattedDate1"].ToString();

                        DateTime doj = Convert.ToDateTime(dr["FormattedDate1"]);
                        lblDateofJoining.Text = doj.ToString("dd-MM-yyyy");

                        //txtapplyingresignation.Text = dr["FormattedDate"].ToString();


                        DateTime doj1 = Convert.ToDateTime(dr["FormattedDate"]);
                        txtapplyingresignation.Text = doj.ToString("dd-MM-yyyy");


                        txtEmail.Text = dr["Email ID"].ToString();
                        txtofficial.Text = dr["Confirm_Email"].ToString();
                        txtMobileNo.Text = dr["Mobile No"].ToString();
                        txtnoticeperiod.Text = dr["Total Duration"].ToString();
                        txtemployeetype.Text = dr["Employee_type"].ToString();
                        txtbetterprofile.Text = dr["Better Profile"].ToString();
                        txtBetterEmoluments.Text = dr["Better Emolument"].ToString();
                        txtPersonalReason.Text = dr["Personal Reason"].ToString();
                        txtanyotherreason.Text = dr["Any Other Reason"].ToString();
                        txtNameofOrgJoining.Text = dr["Name of Organisation Joining"].ToString();
                        txttriggerdlookforchange.Text = dr["Triggered for Changed"].ToString();
                        txtGoodwithTMU.Text = dr["Experience With Organisation"].ToString();
                        txtDifficultwithtmu.Text = dr["Updating Experience with Organisation"].ToString();
                        txtovalallratingResponse.Text = dr["Rating of Organisation Response"].ToString();
                        txtovalallratingRemarks.Text = dr["Rating of Organisation Remarks"].ToString();
                        txtperformancemeasurementResponse.Text = dr["Feedback_Perf System Response"].ToString();
                        txtperformancemeasurementRemarks.Text = dr["Feedback_Perf System Remarks"].ToString();
                        txtCommunicationResponse.Text = dr["Communication with Organisation Response"].ToString();
                        txtCommunicationRemarks.Text = dr["Communication with Organisation Remarks"].ToString();
                        txtRecruitmentResponse.Text = dr["Recruitment Induction in Organisation Response"].ToString();
                        txtRecruitmentRemarks.Text = dr["Recruitment Induction in Organisation Remarks"].ToString();
                        txtWillingnessResponse.Text = dr["Willing Ness Problem Response"].ToString();
                        txtWillingnessRemarks.Text = dr["Willing Ness Problem Remarks"].ToString();
                        txtRecruitment_Proc_Response.Text = dr["Salary Structure Response"].ToString();
                        txtRecruitment_Proc_Remarks.Text = dr["Salary Structure Remarks"].ToString();
                        txtWorkingEnviron_Response.Text = dr["Working Environment Response"].ToString();
                        txtWorkingEnviron_Remarks.Text = dr["Working Environment Remarks"].ToString();
                        txtgrowthOpportuniti_Response.Text = dr["Growth Opportunities Response"].ToString();
                        txtgrowthOpportuniti_Remarks.Text = dr["Growth Opportunities Remarks"].ToString();
                        txteffectiveness_Response.Text = dr["Effectiveness of Appraisal Response"].ToString();
                        txteffectiveness_Remarks.Text = dr["Effectiveness of Appraisal Remarks"].ToString();
                        txtAnyOtherComment.Text = dr["Any Other Comments of Response"].ToString();
                        lblhodstatus.Text = dr["Hod_Status"].ToString();
                        lblhrstatus.Text = dr["Hr_Status"].ToString();
                        con1.Close();
                    }
                    else if (status == "Submit")
                    {
                        if (Hod_status == "Rejected")
                        {
                            Panel1.Enabled = true;
                        }
                        else if (Hr_status == "Rejected")
                        {
                            Panel1.Enabled = true;
                        }
                        else if (Registrar_Approval == "Rejected")
                        {
                            Panel1.Enabled = true;
                        }
                        else if (VC_Approval == "Rejected")
                        {
                            Panel1.Enabled = true;
                        }

                        else
                        {
                            Panel1.Enabled = false;

                        }
                        txtEmployeeName.Text = dr["Employee Name"].ToString();
                        txtEmployeeCode.Text = dr["Employee Code"].ToString();
                        lbldesignation.Text = dr["Designation"].ToString();
                        lblInstitution.Text = dr["Institution"].ToString();
                        lblnameofHOD.Text = dr["HOD"].ToString();
                        

                        DateTime doj = Convert.ToDateTime(dr["Date Of Joining"]);
                        lblDateofJoining.Text = doj.ToString("dd-MM-yyyy");

                        

                        DateTime doj1 = Convert.ToDateTime(dr["Date Of Resignation"]);
                        txtapplyingresignation.Text = doj1.ToString("dd-MM-yyyy");


                        txtEmail.Text = dr["Email ID"].ToString();
                        txtofficial.Text = dr["Confirm_Email"].ToString();
                        txtMobileNo.Text = dr["Mobile No"].ToString();
                        txtnoticeperiod.Text = dr["Total Duration"].ToString();
                        txtemployeetype.Text = dr["Employee_type"].ToString();
                        txtbetterprofile.Text = dr["Better Profile"].ToString();
                        txtBetterEmoluments.Text = dr["Better Emolument"].ToString();
                        txtPersonalReason.Text = dr["Personal Reason"].ToString();
                        txtanyotherreason.Text = dr["Any Other Reason"].ToString();
                        txtNameofOrgJoining.Text = dr["Name of Organisation Joining"].ToString();
                        txttriggerdlookforchange.Text = dr["Triggered for Changed"].ToString();
                        txtGoodwithTMU.Text = dr["Experience With Organisation"].ToString();
                        txtDifficultwithtmu.Text = dr["Updating Experience with Organisation"].ToString();
                        txtovalallratingResponse.Text = dr["Rating of Organisation Response"].ToString();
                        txtovalallratingRemarks.Text = dr["Rating of Organisation Remarks"].ToString();
                        txtperformancemeasurementResponse.Text = dr["Feedback_Perf System Response"].ToString();
                        txtperformancemeasurementRemarks.Text = dr["Feedback_Perf System Remarks"].ToString();
                        txtCommunicationResponse.Text = dr["Communication with Organisation Response"].ToString();
                        txtCommunicationRemarks.Text = dr["Communication with Organisation Remarks"].ToString();
                        txtRecruitmentResponse.Text = dr["Recruitment Induction in Organisation Response"].ToString();
                        txtRecruitmentRemarks.Text = dr["Recruitment Induction in Organisation Remarks"].ToString();
                        txtWillingnessResponse.Text = dr["Willing Ness Problem Response"].ToString();
                        txtWillingnessRemarks.Text = dr["Willing Ness Problem Remarks"].ToString();
                        txtRecruitment_Proc_Response.Text = dr["Salary Structure Response"].ToString();
                        txtRecruitment_Proc_Remarks.Text = dr["Salary Structure Remarks"].ToString();
                        txtWorkingEnviron_Response.Text = dr["Working Environment Response"].ToString();
                        txtWorkingEnviron_Remarks.Text = dr["Working Environment Remarks"].ToString();
                        txtgrowthOpportuniti_Response.Text = dr["Growth Opportunities Response"].ToString();
                        txtgrowthOpportuniti_Remarks.Text = dr["Growth Opportunities Remarks"].ToString();
                        txteffectiveness_Response.Text = dr["Effectiveness of Appraisal Response"].ToString();
                        txteffectiveness_Remarks.Text = dr["Effectiveness of Appraisal Remarks"].ToString();
                        txtAnyOtherComment.Text = dr["Any Other Comments of Response"].ToString();
                        lblhodstatus.Text = dr["Hod_Status"].ToString();
                        lblhrstatus.Text = dr["Hr_Status"].ToString();
                        lblregistrar.Text = dr["Registrar_Approval"].ToString();
                        lblvcstatus.Text = dr["VC_Approval"].ToString();
                        con1.Close();
                    }

                }
                else
                {
                    Employeeinfo();
                    desginame();

                }
            }
        }
    }
    public void insertexitforminfo()
    {
        using (SqlConnection con1 = new SqlConnection(constr1))
        {

            using (SqlCommand cmd = new SqlCommand("select * from [tble_Exit_Interview_Form] where [Employee Code]='" + Session["uid"].ToString() + "'", con1))
            {
                con1.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string status = dr["Status"].ToString();
                    string statusHR = dr["Hr_Status"].ToString();
                    string statusHOD = dr["Hod_Status"].ToString();
                    con1.Close();
                    if (status == "Pending" || statusHR == "Rejected" || statusHOD == "Rejected")

                    {


                        //update
                        using (SqlCommand cmdupdate = new SqlCommand("UPDATE [tble_Exit_Interview_Form]" +
                               "SET [Employee Name] = '" + txtEmployeeName.Text + "'" +
                             " ,[Employee Code] ='" + txtEmployeeCode.Text + "'" +
                             " ,[Designation] = '" + lbldesignation.Text + "'" +
                             " ,[Institution] = '" + lblInstitution.Text + "'" +
                             " ,[HOD] = '" + lblnameofHOD.Text + "'" +
                              ",[Date Of Joining] = convert(date,'" + lblDateofJoining.Text + "',105)" +
                              ",[Date Of Resignation] =convert(date,'" + txtapplyingresignation.Text + "',105)" +
                              ",[Total Duration] = '" + txtnoticeperiod.Text + "'" +
                              ",[Employee_type] = '" + txtemployeetype.Text + "'" +
                              ",[Better Profile] = '" + txtbetterprofile.Text + "'" +
                              ",[Better Emolument] = '" + txtBetterEmoluments.Text + "'" +
                              ",[Personal Reason] ='" + txtPersonalReason.Text + "'" +
                              ",[Any Other Reason] = '" + txtanyotherreason.Text + "'" +
                              ",[Name of Organisation Joining] = '" + txtNameofOrgJoining.Text + "'" +
                              ",[Triggered for Changed] = '" + txttriggerdlookforchange.Text + "'" +
                              ",[Experience With Organisation] = '" + txtGoodwithTMU.Text + "'" +
                              ",[Updating Experience with Organisation] = '" + txtDifficultwithtmu.Text + "'" +
                              ",[Rating of Organisation Response] = '" + txtovalallratingResponse.Text + "'" +
                             " ,[Rating of Organisation Remarks] = '" + txtovalallratingRemarks.Text + "'" +
                              ",[Feedback_Perf System Response] = '" + txtperformancemeasurementResponse.Text + "'" +
                              ",[Feedback_Perf System Remarks] = '" + txtperformancemeasurementRemarks.Text + "'" +
                              ",[Communication with Organisation Response] = '" + txtCommunicationResponse.Text + "'" +
                              ",[Communication with Organisation Remarks] = '" + txtCommunicationRemarks.Text + "'" +
                              ",[Recruitment Induction in Organisation Response] = '" + txtRecruitmentResponse.Text + "'" +
                              ",[Recruitment Induction in Organisation Remarks] ='" + txtRecruitmentRemarks.Text + "'" +
                              ",[Willing Ness Problem Response] = '" + txtWillingnessResponse.Text + "'" +
                              ",[Willing Ness Problem Remarks] = '" + txtWillingnessRemarks.Text + "'" +
                              ",[Salary Structure Response] ='" + txtRecruitment_Proc_Response.Text + "'" +
                              ",[Salary Structure Remarks] = '" + txtRecruitment_Proc_Remarks.Text + "'" +
                              ",[Working Environment Response] = '" + txtWorkingEnviron_Response.Text + "'" +
                              ",[Working Environment Remarks] = '" + txtWorkingEnviron_Remarks.Text + "'" +
                              ",[Growth Opportunities Response] = '" + txtgrowthOpportuniti_Response.Text + "'" +
                              ",[Growth Opportunities Remarks] = '" + txtgrowthOpportuniti_Remarks.Text + "'" +
                             " ,[Effectiveness of Appraisal Response] = '" + txteffectiveness_Response.Text + "'" +
                              ",[Effectiveness of Appraisal Remarks] = '" + txteffectiveness_Remarks.Text + "'" +
                              ",[Any Other Comments of Response] = '" + txtAnyOtherComment.Text + "'" +
                              ",[Mobile No] = '" + txtMobileNo.Text + "'" +
                             " ,[Email ID] = '" + txtEmail.Text + "'" +
                             " ,[Confirm_Email] = '" + txtofficial.Text + "'" +
                             " ,[Hod_Status] = 'Pending'" +
                             " ,[Hr_Status] = 'Pending'" +
                             " ,[Registrar_Approval] = 'Pending'" +
                             " ,[VC_Approval] = 'Pending'" +
                             " ,[HR_Id] = 'TMU05721'" +
                             " ,[Status] = 'Pending' where [Employee Code]='" + Session["uid"].ToString() + "'", con1))
                        {

                            con1.Open();
                            cmdupdate.ExecuteNonQuery();
                            con1.Close();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully  !');", true);
                        }
                    }
                    else
                    {
                        //submit msg
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Already submit  modification not allow!');", true);
                        showsaveRecord();
                    }
                }
                else
                {
                    //insert 
                    con1.Close();

                    using (SqlCommand cmdinsert = new SqlCommand("insert into [tble_Exit_Interview_Form]([Employee Name],[Employee Code],[Designation],[Institution],[HOD],[Date Of Joining],[Date Of Resignation],[Total Duration],[Employee_type],[Better Profile],[Better Emolument],[Personal Reason],[Any Other Reason],[Name of Organisation Joining],[Triggered for Changed],[Experience With Organisation],[Updating Experience with Organisation],"
                        + "[Rating of Organisation Response],[Rating of Organisation Remarks],[Feedback_Perf System Response],[Feedback_Perf System Remarks],[Communication with Organisation Response],[Communication with Organisation Remarks],[Recruitment Induction in Organisation Response],[Recruitment Induction in Organisation Remarks],[Willing Ness Problem Response],[Willing Ness Problem Remarks],[Salary Structure Response]," +
                        "[Salary Structure Remarks],[Working Environment Response],[Working Environment Remarks],[Growth Opportunities Response],[Growth Opportunities Remarks],[Effectiveness of Appraisal Response],[Effectiveness of Appraisal Remarks],[Any Other Comments of Response],[Mobile No],[Email ID],[Confirm_Email],[HR_Id],[Status],[Hod_Status],[Hr_Status],[Registrar_Approval],[VC_Approval],[Created Date])" +
                        " values('" + txtEmployeeName.Text + "','" + txtEmployeeCode.Text + "','" + lbldesignation.Text + "','" + lblInstitution.Text + "','" + lblnameofHOD.Text + "',convert(date,'" + lblDateofJoining.Text + "',105),convert(date,'" + txtapplyingresignation.Text + "',105),'" + txtnoticeperiod.Text + "','" + txtemployeetype.Text + "','" + txtbetterprofile.Text + "','" + txtBetterEmoluments.Text + "','" + txtPersonalReason.Text + "','" + txtanyotherreason.Text + "','" + txtNameofOrgJoining.Text + "','" + txttriggerdlookforchange.Text + "','" + txtGoodwithTMU.Text + "','" + txtDifficultwithtmu.Text + "','" + txtovalallratingResponse.Text + "','" + txtovalallratingRemarks.Text + "','" + txtperformancemeasurementResponse.Text + "','" + txtperformancemeasurementRemarks.Text + "','" + txtCommunicationResponse.Text + "','" + txtCommunicationRemarks.Text + "','" + txtRecruitmentResponse.Text + "','" + txtRecruitmentRemarks.Text + "','" + txtWillingnessResponse.Text + "','" + txtWillingnessRemarks.Text + "','" + txtRecruitment_Proc_Response.Text + "','" + txtRecruitment_Proc_Remarks.Text + "','" + txtWorkingEnviron_Response.Text + "','" + txtWorkingEnviron_Remarks.Text + "','" + txtgrowthOpportuniti_Response.Text + "','" + txtgrowthOpportuniti_Remarks.Text + "','" + txteffectiveness_Response.Text + "','" + txteffectiveness_Remarks.Text + "','" + txtAnyOtherComment.Text + "','" + txtMobileNo.Text + "','" + txtEmail.Text + "','" + txtofficial.Text + "','TMU05721','Pending','Pending','Pending','Pending','Pending','" + System.DateTime.Now + "')", con1))
                    {
                        con1.Open();
                        cmdinsert.ExecuteNonQuery();
                        con1.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record Save !');", true);
                    }
                }
            }
        }
    }


    public void submitfrm()
    {
        using (SqlConnection con1 = new SqlConnection(constr1))
        {
            using (SqlCommand cmd = new SqlCommand("select * from [tble_Exit_Interview_Form] where [Employee Code]='" + Session["uid"].ToString() + "'", con1))
            {
                con1.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string status = dr["Status"].ToString();
                    con1.Close();
                    if (status == "Pending")
                    {
                        using (SqlCommand cmdupdate1 = new SqlCommand("UPDATE [tble_Exit_Interview_Form]" +
                              "SET [Status] = 'Submit' where [Employee Code]='" + Session["uid"].ToString() + "'", con1))
                        {
                            con1.Open();
                            cmdupdate1.ExecuteNonQuery();
                            con1.Close();

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Form Submitted');", true);
                        }
                    }
                    else if (status == "Submit")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Already Submitted');", true);
                    }

                }
                else
                {
                    con1.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Before submit Please Save !');", true);
                }
            }
        }
    }

    public void hidestatus()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select * from [tble_Exit_Interview_Form] where [Employee Code]='" + Session["uid"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string Employee_type = dr["Employee_type"].ToString();
                    con.Close();
                    if (Employee_type == "TEACH")

                    {
                        lblvcstatus.Visible = true;
                        vcss.Visible = true;
                    }
                    else
                    {
                        lblvcstatus.Visible = false;
                        vcss.Visible = false;
                    }


                }
            }
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtapplyingresignation.Text == "")
        {
            string message1 = "Please Fill Resignation Date";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        insertexitforminfo();

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            //Your logic for OK button
            submitfrm();
            Response.Redirect("exitinterviewform.aspx");
        }
        else
        {
            //Your logic for cancel button
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('nothing to do');", true);
        }
        //

    }

    protected void txtapplyingresignation_TextChanged(object sender, EventArgs e)
    {
        // txtapplyingresignation.Text = DateTime.Now.ToString();
        //txtapplyingresignation.Text = DateTime.Now.ToString("MM-dd-yyyy");
    }
}