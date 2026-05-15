using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Alumni_GoogleForm : System.Web.UI.Page
{
    DL.StudentDetailsViewDL sdvDL = new DL.StudentDetailsViewDL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string strWidth = "1%";
                //lblprogress.Text = "0%";
                divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
                bindData(Session["uid"].ToString());
                //Rdata();
            }
            catch (Exception ex)
            {
                Response.Redirect("../Default.aspx");
            }
        }

    }
    public void bindData(string FacultyCode)
    {

        DataTable dt = new DataTable();
        dt = sdvDL.GetStudentDetails(FacultyCode);
        if (dt.Rows.Count > 0)
        {

            txtEmail.Text = dt.Rows[0]["E-Mail Address"].ToString();

            txtFname.Text = dt.Rows[0]["Student Name"].ToString();
            txtDOB.Text = dt.Rows[0]["DOB"].ToString();
            if (dt.Rows[0]["Gender"].ToString() == "1")
            {
                txtGender.Text = "Male";
            }
            else if (dt.Rows[0]["Gender"].ToString() == "2")
            {
                txtGender.Text = "Female";
            }
            else
            {
                txtGender.Text = "Other";
            }
            txtMobile.Text = dt.Rows[0]["Mobile Number"].ToString();


            txtWhatsUp.Text = dt.Rows[0]["Whatsapp No_"].ToString(); ;
            txtPresent.Text = dt.Rows[0]["Address1"].ToString();
            txtPermanent.Text = dt.Rows[0]["Address1"].ToString();
            txtLinkUrl.Text = dt.Rows[0]["LinkedInURL"].ToString();
            txtFacebook.Text = dt.Rows[0]["FacebookURL"].ToString();
            txtTwitter.Text = dt.Rows[0]["TwitterURL"].ToString();
            txtCollege.Text = dt.Rows[0]["CollegeName"].ToString();
            txtProgram.Text = dt.Rows[0]["Course Code"].ToString();
            txtEnroll.Text = dt.Rows[0]["Enrollment No_"].ToString();
            txtAdmissionYear.Text = dt.Rows[0]["Admission Year"].ToString();
            txtGraduation.Text = dt.Rows[0]["Passout Year"].ToString();
            hfCollegeCode.Value = dt.Rows[0]["College Code"].ToString();

            drpCurrent.SelectedValue = dt.Rows[0]["Engagement Type"].ToString();

            if (drpCurrent.SelectedValue == "1")
            {

                txtEmployer.Text = dt.Rows[0]["Employer"].ToString();
                txtDesignation.Text = dt.Rows[0]["Designation"].ToString();
                txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
                
                // txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
                txtJDesc.Text = dt.Rows[0]["Job Description"].ToString();
                ddlJDesc.SelectedValue = dt.Rows[0]["Job Status"].ToString();
                txtSDate.Text = dt.Rows[0]["JSD"].ToString();
                txtCompanyURL.Text = dt.Rows[0]["Company Website"].ToString();
                txtCMail.Text = dt.Rows[0]["Company Email"].ToString();
                txtCompanyTelephone.Text = dt.Rows[0]["Company Telephone"].ToString();
                txtIndustry.Text = dt.Rows[0]["Business Type"].ToString();

                txtEnterName.Text = "";
                txtSelfIndustry.Text = "";
                txtSelfAddress.Text = "";
                txtRole.Text = "";
                txtSelfCompanyURL.Text = "";
                txtCollegeName.Text = "";
                txtEduAddress.Text = "";
                txtEWebUrl.Text = "";
                txtProgramName.Text = "";
                txtEduAdmissionYear.Text = "";
                txtExGradYear.Text = "";
                txtFurtherPlan.Text = "";
            }
            if (drpCurrent.SelectedValue == "2")
            {

                txtEnterName.Text = dt.Rows[0]["Company Name"].ToString();
                txtSelfIndustry.Text = dt.Rows[0]["Business Type"].ToString();
                txtSelfAddress.Text = dt.Rows[0]["Address2"].ToString();
                txtRole.Text = dt.Rows[0]["Designation"].ToString();
                txtSelfCompanyURL.Text = dt.Rows[0]["Company Website"].ToString();


                txtEmployer.Text ="";
                txtDesignation.Text = "";
                txtEAddress.Text = "";
                hfCollegeCode.Value = "";
                // txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
                txtJDesc.Text = "";
                ddlJDesc.SelectedValue = "";
                txtSDate.Text = "";
                txtCompanyURL.Text ="";
                txtCMail.Text = "";
                txtCompanyTelephone.Text = "";
                txtIndustry.Text = "";
                txtCollegeName.Text = "";
                txtEduAddress.Text = "";
                txtEWebUrl.Text = "";
                txtProgramName.Text = "";
                txtEduAdmissionYear.Text = "";
                txtExGradYear.Text = "";
                txtFurtherPlan.Text = "";

            }
            if (drpCurrent.SelectedValue == "3")
            {
                txtCollegeName.Text = dt.Rows[0]["Higher Studies Institute"].ToString();
                txtEduAddress.Text = dt.Rows[0]["Address2"].ToString(); 
                txtEWebUrl.Text = dt.Rows[0]["Company Website"].ToString();
                txtProgramName.Text = dt.Rows[0]["Higher Studies Program"].ToString(); 
                txtEduAdmissionYear.Text = dt.Rows[0]["Higher Study Admission Yr"].ToString();
                txtExGradYear.Text = dt.Rows[0]["Higher Study Graduation Year"].ToString();
                txtFurtherPlan.Text = dt.Rows[0]["Futher Plan"].ToString(); ;

                txtEnterName.Text ="";
                txtSelfIndustry.Text = "";
                txtSelfAddress.Text = "";
                txtRole.Text = "";
                txtSelfCompanyURL.Text = "";


                txtEmployer.Text = "";
                txtDesignation.Text = "";
                txtEAddress.Text = "";
                hfCollegeCode.Value = "";
                // txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
                txtJDesc.Text = "";
                ddlJDesc.SelectedValue = "";
                txtSDate.Text = "";
                txtCompanyURL.Text = "";
                txtCMail.Text = "";
                txtCompanyTelephone.Text = "";
                txtIndustry.Text = "";
              
                
                
              
              
            }
            else
            {

            }




        }
        else
        {

        }

    }
    protected void btnsavestep1_Click(object sender, EventArgs e)
    {




        con.Open();
        SqlCommand cmd = new SqlCommand("sp_InsertAlumaniRegistration", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SNo", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@StudentName", txtFname.Text);
        cmd.Parameters.AddWithValue("@Engtype", drpCurrent.SelectedValue);
        //cmd.Parameters.AddWithValue("@Engtype", drpCurrent.SelectedValue);
      
        


      


        cmd.Parameters.AddWithValue("@ProgramName", txtProgramName.Text);
        cmd.Parameters.AddWithValue("@FurtherPlan", txtFurtherPlan.Text);
        cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
        cmd.Parameters.AddWithValue("@WhatAppNo",txtWhatsUp.Text);
        cmd.Parameters.AddWithValue("@present", txtPresent.Text);
        cmd.Parameters.AddWithValue("@permanent", txtPermanent.Text);
        cmd.Parameters.AddWithValue("@LinkDin", txtLinkUrl.Text);
        cmd.Parameters.AddWithValue("@Facebook", txtFacebook.Text);
        cmd.Parameters.AddWithValue("@Twitter", txtTwitter.Text);
       
        cmd.Parameters.AddWithValue("@Program", txtProgram.Text);
        cmd.Parameters.AddWithValue("@Enroll", txtEnroll.Text);
        cmd.Parameters.AddWithValue("@AdmissionYear", txtAdmissionYear.Text);
        cmd.Parameters.AddWithValue("@GraduationYear", txtGraduation.Text);

        cmd.Parameters.AddWithValue("@College", hfCollegeCode.Value);
    
      
       
      
      
       

       
       
       




       




    

        if (drpCurrent.SelectedValue == "1")
        {

            cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text);
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
            cmd.Parameters.AddWithValue("@EAddress", txtEAddress.Text);
           
            // txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
            cmd.Parameters.AddWithValue("@JDesc", txtJDesc.Text);
            cmd.Parameters.AddWithValue("@ddlJDesc", ddlJDesc.SelectedValue);
            cmd.Parameters.AddWithValue("@SDate", txtSDate.Text);
            cmd.Parameters.AddWithValue("@CompanyURL", txtCompanyURL.Text);
            cmd.Parameters.AddWithValue("@CMail", txtCMail.Text);
            cmd.Parameters.AddWithValue("@CompanyTelephone", txtCompanyTelephone.Text);
            cmd.Parameters.AddWithValue("@EIndustry", txtIndustry.Text);

            cmd.Parameters.AddWithValue("@Industry", "");
            cmd.Parameters.AddWithValue("@SelfAddress", "");
            cmd.Parameters.AddWithValue("@Role","");
            cmd.Parameters.AddWithValue("@SelfCompanyURL", "");

            //txtEnterName.Text = "";
            //txtSelfIndustry.Text = "";
            //txtSelfAddress.Text = "";
            //txtRole.Text = "";
            //txtSelfCompanyURL.Text = "";
            //txtCollegeName.Text = "";
            //txtEduAddress.Text = "";
            //txtEWebUrl.Text = "";
            //txtProgramName.Text = "";
            //txtEduAdmissionYear.Text = "";
            //txtExGradYear.Text = "";
            //txtFurtherPlan.Text = "";
        }
        if (drpCurrent.SelectedValue == "2")
        {

         
            cmd.Parameters.AddWithValue("@EAddress", txtSelfAddress.Text);
          
            // txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
            cmd.Parameters.AddWithValue("@JDesc", "");
            cmd.Parameters.AddWithValue("@ddlJDesc", 0);
            cmd.Parameters.AddWithValue("@SDate", "");
            cmd.Parameters.AddWithValue("@CompanyURL", txtSelfCompanyURL.Text);
            cmd.Parameters.AddWithValue("@CMail", "");
            cmd.Parameters.AddWithValue("@CompanyTelephone", "");
            cmd.Parameters.AddWithValue("@EIndustry", "");



            cmd.Parameters.AddWithValue("@EnterName", txtEnterName.Text);
            cmd.Parameters.AddWithValue("@Industry", txtSelfIndustry.Text);
            cmd.Parameters.AddWithValue("@SelfAddress", "");
            cmd.Parameters.AddWithValue("@Designation", txtRole.Text);
            cmd.Parameters.AddWithValue("@SelfCompanyURL", txtSelfCompanyURL.Text);


            //txtEmployer.Text = "";
            //txtDesignation.Text = "";
            //txtEAddress.Text = "";
            //hfCollegeCode.Value = "";
           
            //txtJDesc.Text = "";
            //ddlJDesc.SelectedValue = "";
            //txtSDate.Text = "";
            //txtCompanyURL.Text = "";
            //txtCMail.Text = "";
            //txtCompanyTelephone.Text = "";
            //txtIndustry.Text = "";
            //txtCollegeName.Text = "";
            //txtEduAddress.Text = "";
            //txtEWebUrl.Text = "";
            //txtProgramName.Text = "";
            //txtEduAdmissionYear.Text = "";
            //txtExGradYear.Text = "";
            //txtFurtherPlan.Text = "";

        }
        if (drpCurrent.SelectedValue == "3")
        {
            cmd.Parameters.AddWithValue("@HigherStudy", txtCollegeName.Text);
            cmd.Parameters.AddWithValue("@EAddress", txtEduAddress.Text);
            cmd.Parameters.AddWithValue("@CompanyURL", txtEWebUrl.Text);
            cmd.Parameters.AddWithValue("@ProgramName1", txtProgramName.Text);
            cmd.Parameters.AddWithValue("@EduAdmissionYear", txtEduAdmissionYear.Text);
            cmd.Parameters.AddWithValue("@ExGradYear", txtExGradYear.Text);
            cmd.Parameters.AddWithValue("@FurtherPlan1", txtFurtherPlan.Text);
            //txtEnterName.Text = "";
            //txtSelfIndustry.Text = "";
            //txtSelfAddress.Text = "";
            //txtRole.Text = "";
            //txtSelfCompanyURL.Text = "";


            //txtEmployer.Text = "";
            //txtDesignation.Text = "";
            //txtEAddress.Text = "";
            //hfCollegeCode.Value = "";
           
            //txtJDesc.Text = "";
            //ddlJDesc.SelectedValue = "";
            //txtSDate.Text = "";
            //txtCompanyURL.Text = "";
            //txtCMail.Text = "";
            //txtCompanyTelephone.Text = "";
            //txtIndustry.Text = "";

        }


















        //cmd.Parameters.AddWithValue("@CurrentStatus", drpCurrent.SelectedValue);


        int i = cmd.ExecuteNonQuery();
        con.Close();     

        string strWidth = "33%";
        // lblprogress.Text = "40%";
        divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
        Divpayment.Visible = true;
        STEPA.Visible = false;
        STEPB.Visible = false;
       
    }
    protected void btnpaymentNext_Click(object sender, EventArgs e)
    {



        con.Open();
        SqlCommand cmd = new SqlCommand("sp_InsertAlumaniRegistration", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SNo", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@StudentName", txtFname.Text);
        cmd.Parameters.AddWithValue("@Engtype", drpCurrent.SelectedValue);
        //cmd.Parameters.AddWithValue("@Engtype", drpCurrent.SelectedValue);




       


        cmd.Parameters.AddWithValue("@ProgramName", txtProgramName.Text);
        cmd.Parameters.AddWithValue("@FurtherPlan", txtFurtherPlan.Text);
        cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
        cmd.Parameters.AddWithValue("@WhatAppNo", txtWhatsUp.Text);
        cmd.Parameters.AddWithValue("@present", txtPresent.Text);
        cmd.Parameters.AddWithValue("@permanent", txtPermanent.Text);
        cmd.Parameters.AddWithValue("@LinkDin", txtLinkUrl.Text);
        cmd.Parameters.AddWithValue("@Facebook", txtFacebook.Text);
        cmd.Parameters.AddWithValue("@Twitter", txtTwitter.Text);

        cmd.Parameters.AddWithValue("@Program", txtProgram.Text);
        cmd.Parameters.AddWithValue("@Enroll", txtEnroll.Text);
        cmd.Parameters.AddWithValue("@AdmissionYear", txtAdmissionYear.Text);
        cmd.Parameters.AddWithValue("@GraduationYear", txtGraduation.Text);

        cmd.Parameters.AddWithValue("@College", hfCollegeCode.Value);





















        if (drpCurrent.SelectedValue == "1")
        {

            cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text);
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
            cmd.Parameters.AddWithValue("@EAddress", txtEAddress.Text);
           
            // txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
            cmd.Parameters.AddWithValue("@JDesc", txtJDesc.Text);
            cmd.Parameters.AddWithValue("@ddlJDesc", ddlJDesc.SelectedValue);
            cmd.Parameters.AddWithValue("@SDate", txtSDate.Text);
            cmd.Parameters.AddWithValue("@CompanyURL", txtCompanyURL.Text);
            cmd.Parameters.AddWithValue("@CMail", txtCMail.Text);
            cmd.Parameters.AddWithValue("@CompanyTelephone", txtCompanyTelephone.Text);
            cmd.Parameters.AddWithValue("@EIndustry", txtIndustry.Text);

            cmd.Parameters.AddWithValue("@Industry", "");
            cmd.Parameters.AddWithValue("@SelfAddress", "");
            cmd.Parameters.AddWithValue("@Role", "");
            cmd.Parameters.AddWithValue("@SelfCompanyURL", "");

            //txtEnterName.Text = "";
            //txtSelfIndustry.Text = "";
            //txtSelfAddress.Text = "";
            //txtRole.Text = "";
            //txtSelfCompanyURL.Text = "";
            //txtCollegeName.Text = "";
            //txtEduAddress.Text = "";
            //txtEWebUrl.Text = "";
            //txtProgramName.Text = "";
            //txtEduAdmissionYear.Text = "";
            //txtExGradYear.Text = "";
            //txtFurtherPlan.Text = "";
        }
        if (drpCurrent.SelectedValue == "2")
        {

            cmd.Parameters.AddWithValue("@Designation", "");
            cmd.Parameters.AddWithValue("@EAddress", txtSelfAddress.Text);
            cmd.Parameters.AddWithValue("@College", "");
            // txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
            cmd.Parameters.AddWithValue("@JDesc", "");
            cmd.Parameters.AddWithValue("@ddlJDesc", 0);
            cmd.Parameters.AddWithValue("@SDate", "");
            cmd.Parameters.AddWithValue("@CompanyURL", "");
            cmd.Parameters.AddWithValue("@CMail", "");
            cmd.Parameters.AddWithValue("@CompanyTelephone", "");
            cmd.Parameters.AddWithValue("@EIndustry", "");



            cmd.Parameters.AddWithValue("@EnterName", txtEnterName.Text);
            cmd.Parameters.AddWithValue("@Industry", txtSelfIndustry.Text);
            cmd.Parameters.AddWithValue("@SelfAddress", "");
            cmd.Parameters.AddWithValue("@Role", txtRole.Text);
            cmd.Parameters.AddWithValue("@SelfCompanyURL", txtSelfCompanyURL.Text);


            //txtEmployer.Text = "";
            //txtDesignation.Text = "";
            //txtEAddress.Text = "";
            //hfCollegeCode.Value = "";

            //txtJDesc.Text = "";
            //ddlJDesc.SelectedValue = "";
            //txtSDate.Text = "";
            //txtCompanyURL.Text = "";
            //txtCMail.Text = "";
            //txtCompanyTelephone.Text = "";
            //txtIndustry.Text = "";
            //txtCollegeName.Text = "";
            //txtEduAddress.Text = "";
            //txtEWebUrl.Text = "";
            //txtProgramName.Text = "";
            //txtEduAdmissionYear.Text = "";
            //txtExGradYear.Text = "";
            //txtFurtherPlan.Text = "";

        }
        if (drpCurrent.SelectedValue == "3")
        {
            cmd.Parameters.AddWithValue("@HigherStudy", txtCollegeName.Text);
            cmd.Parameters.AddWithValue("@EAddress", txtEduAddress.Text);
            cmd.Parameters.AddWithValue("@CompanyURL", txtEWebUrl.Text);
            cmd.Parameters.AddWithValue("@ProgramName1", txtProgramName.Text);
            cmd.Parameters.AddWithValue("@EduAdmissionYear", txtEduAdmissionYear.Text);
            cmd.Parameters.AddWithValue("@ExGradYear", txtExGradYear.Text);
            cmd.Parameters.AddWithValue("@FurtherPlan1", txtFurtherPlan.Text);
            //txtEnterName.Text = "";
            //txtSelfIndustry.Text = "";
            //txtSelfAddress.Text = "";
            //txtRole.Text = "";
            //txtSelfCompanyURL.Text = "";


            //txtEmployer.Text = "";
            //txtDesignation.Text = "";
            //txtEAddress.Text = "";
            //hfCollegeCode.Value = "";

            //txtJDesc.Text = "";
            //ddlJDesc.SelectedValue = "";
            //txtSDate.Text = "";
            //txtCompanyURL.Text = "";
            //txtCMail.Text = "";
            //txtCompanyTelephone.Text = "";
            //txtIndustry.Text = "";

        }






        int i = cmd.ExecuteNonQuery();
        con.Close();


        if (drpCurrent.SelectedValue == "1")
        {
            
            divEmployee.Visible = true;
            divSelfEmployee.Visible = false;
            divFurther.Visible = false;
            divUnEmployee.Visible = false;
        }
        if (drpCurrent.SelectedValue == "2")
        {
           
            divEmployee.Visible = false;
            divSelfEmployee.Visible = true;
            divFurther.Visible = false;
            divUnEmployee.Visible = false;
        }
        if (drpCurrent.SelectedValue == "3")
        {
            
            divEmployee.Visible = false;
            divSelfEmployee.Visible = false;
            divFurther.Visible = true;
            divUnEmployee.Visible = false;
        }
        if (drpCurrent.SelectedValue == "4")
        {
           
            divEmployee.Visible = false;
            divSelfEmployee.Visible = false;
            divFurther.Visible = false;
            divUnEmployee.Visible = true;
        }
        if (drpCurrent.SelectedValue == "0")
        {
            
            divEmployee.Visible = false;
            divSelfEmployee.Visible = false;
            divFurther.Visible = false;
            divUnEmployee.Visible = false;
        }




        string strWidth = "66%";
        //lblprogress.Text = "65%";
        divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
        STEPA.Visible = false;
        STEPB.Visible = true;

        Divpayment.Visible = false;
    }
    protected void btnbackPayment_Click(object sender, EventArgs e)
    {
        string strWidth = "1%";
        //lblprogress.Text = "0%";
        divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
        STEPA.Visible = true;
        STEPB.Visible = false;

        Divpayment.Visible = false;
    }
    protected void btnbackPayment1_Click(object sender, EventArgs e)
    {
        string strWidth = "33%";
        //lblprogress.Text = "40%";
        divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
        Divpayment.Visible = true;
        STEPA.Visible = false;
        STEPB.Visible = false;
    }
    protected void btnpaymentNext1_Click(object sender, EventArgs e)
    {


        con.Open();
        SqlCommand cmd = new SqlCommand("sp_InsertAlumaniRegistration", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SNo", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@StudentName", txtFname.Text);
        cmd.Parameters.AddWithValue("@Engtype", drpCurrent.SelectedValue);
        //cmd.Parameters.AddWithValue("@Engtype", drpCurrent.SelectedValue);




        


        cmd.Parameters.AddWithValue("@ProgramName", txtProgramName.Text);
        cmd.Parameters.AddWithValue("@FurtherPlan", txtFurtherPlan.Text);
        cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
        cmd.Parameters.AddWithValue("@WhatAppNo", txtWhatsUp.Text);
        cmd.Parameters.AddWithValue("@present", txtPresent.Text);
        cmd.Parameters.AddWithValue("@permanent", txtPermanent.Text);
        cmd.Parameters.AddWithValue("@LinkDin", txtLinkUrl.Text);
        cmd.Parameters.AddWithValue("@Facebook", txtFacebook.Text);
        cmd.Parameters.AddWithValue("@Twitter", txtTwitter.Text);

        cmd.Parameters.AddWithValue("@Program", txtProgram.Text);
        cmd.Parameters.AddWithValue("@Enroll", txtEnroll.Text);
        cmd.Parameters.AddWithValue("@AdmissionYear", txtAdmissionYear.Text);
        cmd.Parameters.AddWithValue("@GraduationYear", txtGraduation.Text);



        cmd.Parameters.AddWithValue("@College", hfCollegeCode.Value);



















        if (drpCurrent.SelectedValue == "1")
        {

            cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text);
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
            cmd.Parameters.AddWithValue("@EAddress", txtEAddress.Text);
        
            // txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
            cmd.Parameters.AddWithValue("@JDesc", txtJDesc.Text);
            cmd.Parameters.AddWithValue("@ddlJDesc", ddlJDesc.SelectedValue);
            cmd.Parameters.AddWithValue("@SDate", txtSDate.Text);
            cmd.Parameters.AddWithValue("@CompanyURL", txtCompanyURL.Text);
            cmd.Parameters.AddWithValue("@CMail", txtCMail.Text);
            cmd.Parameters.AddWithValue("@CompanyTelephone", txtCompanyTelephone.Text);
            cmd.Parameters.AddWithValue("@Industry", txtIndustry.Text);

            
            cmd.Parameters.AddWithValue("@SelfAddress", "");
            cmd.Parameters.AddWithValue("@Role", "");
            cmd.Parameters.AddWithValue("@SelfCompanyURL", "");

            //txtEnterName.Text = "";
            //txtSelfIndustry.Text = "";
            //txtSelfAddress.Text = "";
            //txtRole.Text = "";
            //txtSelfCompanyURL.Text = "";
            //txtCollegeName.Text = "";
            //txtEduAddress.Text = "";
            //txtEWebUrl.Text = "";
            //txtProgramName.Text = "";
            //txtEduAdmissionYear.Text = "";
            //txtExGradYear.Text = "";
            //txtFurtherPlan.Text = "";
        }
        if (drpCurrent.SelectedValue == "2")
        {

            cmd.Parameters.AddWithValue("@Designation", txtRole.Text);
            cmd.Parameters.AddWithValue("@EAddress", txtSelfAddress.Text);
           
            // txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
            cmd.Parameters.AddWithValue("@JDesc", "");
            cmd.Parameters.AddWithValue("@ddlJDesc", 0);
            cmd.Parameters.AddWithValue("@SDate", "");
            cmd.Parameters.AddWithValue("@CompanyURL", txtSelfCompanyURL.Text);
            cmd.Parameters.AddWithValue("@CMail", "");
            cmd.Parameters.AddWithValue("@CompanyTelephone", "");
            cmd.Parameters.AddWithValue("@EIndustry", "");



            cmd.Parameters.AddWithValue("@EnterName", txtEnterName.Text);
            cmd.Parameters.AddWithValue("@Industry", txtSelfIndustry.Text);
            cmd.Parameters.AddWithValue("@SelfAddress", "");
            cmd.Parameters.AddWithValue("@Role", txtRole.Text);
            cmd.Parameters.AddWithValue("@SelfCompanyURL", txtSelfCompanyURL.Text);


            //txtEmployer.Text = "";
            //txtDesignation.Text = "";
            //txtEAddress.Text = "";
            //hfCollegeCode.Value = "";

            //txtJDesc.Text = "";
            //ddlJDesc.SelectedValue = "";
            //txtSDate.Text = "";
            //txtCompanyURL.Text = "";
            //txtCMail.Text = "";
            //txtCompanyTelephone.Text = "";
            //txtIndustry.Text = "";
            //txtCollegeName.Text = "";
            //txtEduAddress.Text = "";
            //txtEWebUrl.Text = "";
            //txtProgramName.Text = "";
            //txtEduAdmissionYear.Text = "";
            //txtExGradYear.Text = "";
            //txtFurtherPlan.Text = "";

        }
        if (drpCurrent.SelectedValue == "3")
        {
            cmd.Parameters.AddWithValue("@HigherStudy", txtCollegeName.Text);
            cmd.Parameters.AddWithValue("@EAddress", txtEduAddress.Text);
            cmd.Parameters.AddWithValue("@CompanyURL", txtEWebUrl.Text);
            cmd.Parameters.AddWithValue("@ProgramName1", txtProgramName.Text);
            cmd.Parameters.AddWithValue("@EduAdmissionYear", txtEduAdmissionYear.Text);
            cmd.Parameters.AddWithValue("@ExGradYear", txtExGradYear.Text);
            cmd.Parameters.AddWithValue("@FurtherPlan1", txtFurtherPlan.Text);
            //txtEnterName.Text = "";
            //txtSelfIndustry.Text = "";
            //txtSelfAddress.Text = "";
            //txtRole.Text = "";
            //txtSelfCompanyURL.Text = "";


            //txtEmployer.Text = "";
            //txtDesignation.Text = "";
            //txtEAddress.Text = "";
            //hfCollegeCode.Value = "";

            //txtJDesc.Text = "";
            //ddlJDesc.SelectedValue = "";
            //txtSDate.Text = "";
            //txtCompanyURL.Text = "";
            //txtCMail.Text = "";
            //txtCompanyTelephone.Text = "";
            //txtIndustry.Text = "";

        }







        int i = cmd.ExecuteNonQuery();



        SqlCommand cmdsubmit = new SqlCommand("sp_SubmitAlumaniRegistration", con);
        cmdsubmit.CommandType = CommandType.StoredProcedure;
        cmdsubmit.Parameters.AddWithValue("@SNo", Session["uid"].ToString());
        int j = cmdsubmit.ExecuteNonQuery();
        con.Close();





        string strWidth = "100%";
        //lblprogress.Text = "65%";
        divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
        STEPA.Visible = false;
        STEPB.Visible = false;


        Response.Redirect("StudentAdmitCard.aspx");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "S", "alert('Data Submit Successfully.');", true);
        //

    }
    protected void drpCurrent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpCurrent.SelectedValue == "1")
        {
            string strWidth = "66%";
            //lblprogress.Text = "65%";
            divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
            divEmployee.Visible = true;
            divSelfEmployee.Visible = false;
            divFurther.Visible = false;
            divUnEmployee.Visible = false;
        }
        if (drpCurrent.SelectedValue == "2")
        {
            string strWidth = "66%";
            //lblprogress.Text = "65%";
            divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
            divEmployee.Visible = false;
            divSelfEmployee.Visible = true;
            divFurther.Visible = false;
            divUnEmployee.Visible = false;
        }
        if (drpCurrent.SelectedValue == "3")
        {
            string strWidth = "66%";
            //lblprogress.Text = "65%";
            divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
            divEmployee.Visible = false;
            divSelfEmployee.Visible = false;
            divFurther.Visible = true;
            divUnEmployee.Visible = false;
        }
        if (drpCurrent.SelectedValue == "4")
        {
            string strWidth = "66%";
            // lblprogress.Text = "65%";
            divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
            divEmployee.Visible = false;
            divSelfEmployee.Visible = false;
            divFurther.Visible = false;
            divUnEmployee.Visible = true;
        }
        if (drpCurrent.SelectedValue == "0")
        {
            string strWidth = "66%";
            // lblprogress.Text = "65%";
            divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
            divEmployee.Visible = false;
            divSelfEmployee.Visible = false;
            divFurther.Visible = false;
            divUnEmployee.Visible = false;
        }
    }
    protected void btnbackPayment2_Click(object sender, EventArgs e)
    {
        string strWidth = "66%";
        //lblprogress.Text = "65%";
        divTrackDistributor.Style.Add(HtmlTextWriterStyle.Width, strWidth);
        STEPA.Visible = false;
        STEPB.Visible = true;
        
        Divpayment.Visible = false;
    }

  
}