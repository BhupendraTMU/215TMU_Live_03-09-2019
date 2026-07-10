using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class PMS : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    string conStr = ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ConnectionString;
    string ReportingManagerID = ""; string HRID = ""; string VCID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //if (Session["GlobalDimension1Coded"].ToString().Trim() == "TMCT" || Session["GlobalDimension1Coded"].ToString().Trim() == "TMEG" || Session["GlobalDimension1Coded"].ToString().Trim() == "TMPT")
                //{

                //    Response.Redirect("AppraisalForm.aspx", false);        //write redirect
                //    Context.ApplicationInstance.CompleteRequest(); // end response

                //}
                drpMonth.SelectedValue = DateTime.Now.Month.ToString();
                BindFinancialYears_For_Filter();
                SP_PMS_Get_Department_Bind();
                Set_Permission();
                sp_Get_PMS_All_Data(dd_AcademicYear.SelectedValue.Trim(), txtFilterBy.Text.Trim(), Session["uid"].ToString().Trim(), Session["Departmentcode"].ToString().Trim(), dd_Staus.SelectedValue.Trim());
                bindTypeOfReseach();
                if (grd_Data.Rows.Count > 0)
                {
                    btnCreateNew.Visible = false;
                }
                else
                {
                    btnCreateNew.Visible = true;
                    //(DateTime.Now.Day >= 19 && DateTime.Now.Day <= 30);
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    public void GetNoActivity()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("proc_GetNoActivity", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", Session["uid"].ToString());
            cmd.Parameters.Add("@month", hfmonth.Value);
            cmd.Parameters.Add("@academicyear", dd_AcademicYear.SelectedValue.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow row in dt1.Rows)
                {
                    string code = row["Applicable_For"].ToString().Trim();

                    // ---------- f ----------
                    if (code == "f.1")
                    {
                        drpactivityYESNO.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1.Visible = false;
                        Button3.Visible = true;
                    }
                    else if (code == "f.2")
                    {
                        DropDownList1.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2.Visible = false;
                        Button4.Visible = true;
                    }
                    else if (code == "f.3")
                    {
                        DropDownList2.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3.Visible = false;
                        Button5.Visible = true;
                    }
                    else if (code == "f.4")
                    {
                        DropDownList3.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4.Visible = false;
                        Button6.Visible = true;
                    }
                    else if (code == "f.5")
                    {
                        DropDownList4.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5.Visible = false;
                        Button7.Visible = true;
                    }
                    else if (code == "f.6")
                    {
                        DropDownList5.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6.Visible = false;
                        Button8.Visible = true;
                    }
                    else if (code == "f.7")
                    {
                        DropDownList6.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7.Visible = false;
                        Button9.Visible = true;
                    }

                    // ---------- h ----------
                    else if (code == "h.1")
                    {
                        DropDownList10.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1.Visible = false;
                        Button10.Visible = true;
                    }
                    else if (code == "h.2")
                    {
                        DropDownList11.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2.Visible = false;
                        Button11.Visible = true;
                    }
                    else if (code == "h.3")
                    {
                        DropDownList12.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3.Visible = false;
                        Button12.Visible = true;
                    }
                    else if (code == "h.4")
                    {
                        DropDownList13.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4.Visible = false;
                        Button13.Visible = true;
                    }
                    else if (code == "h.5")
                    {
                        DropDownList14.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5.Visible = false;
                        Button14.Visible = true;
                    }
                    else if (code == "h.6")
                    {
                        DropDownList15.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6.Visible = false;
                        Button15.Visible = true;
                    }
                    else if (code == "h.7")
                    {
                        DropDownList16.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7.Visible = false;
                        Button16.Visible = true;
                    }
                    else if (code == "h.8")
                    {
                        DropDownList17.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8.Visible = false;
                        Button17.Visible = true;
                    }
                    else if (code == "h.9")
                    {
                        DropDownList18.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9.Visible = false;
                        Button34.Visible = true;
                    }

                    // ---------- i ----------
                    else if (code == "i.1")
                    {
                        DropDownList19.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1.Visible = false;
                        Button20.Visible = true;
                    }
                    else if (code == "i.2")
                    {
                        DropDownList20.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2.Visible = false;
                        Button21.Visible = true;
                    }
                    else if (code == "i.3")
                    {
                        DropDownList21.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3.Visible = false;
                        Button22.Visible = true;
                    }
                    else if (code == "i.4")
                    {
                        DropDownList22.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4.Visible = false;
                        Button23.Visible = true;
                    }
                    else if (code == "i.5")
                    {
                        DropDownList23.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5.Visible = false;
                        Button24.Visible = true;
                    }

                    // ---------- j ----------
                    else if (code == "j.1")
                    {
                        DropDownList24.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1.Visible = false;
                        Button18.Visible = true;
                    }
                    else if (code == "j.2")
                    {
                        DropDownList25.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2.Visible = false;
                        Button19.Visible = true;
                    }

                    // ---------- k ----------
                    else if (code == "k.3")
                    {
                        DropDownList28.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3.Visible = false;
                        Button25.Visible = true;
                    }
                    else if (code == "k.4")
                    {
                        DropDownList29.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4.Visible = false;
                        Button26.Visible = true;
                    }
                    else if (code == "k.5")
                    {
                        DropDownList30.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5.Visible = false;
                        Button27.Visible = true;
                    }
                    else if (code == "k.6")
                    {
                        DropDownList31.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6.Visible = false;
                        Button28.Visible = true;
                    }
                    else if (code == "k.7")
                    {
                        DropDownList32.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7.Visible = false;
                        Button29.Visible = true;
                    }
                    else if (code == "k.8")
                    {
                        DropDownList33.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8.Visible = false;
                        Button30.Visible = true;
                    }
                    else if (code == "k.9")
                    {
                        DropDownList34.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9.Visible = false;
                        Button31.Visible = true;
                    }
                    else if (code == "k.10")
                    {
                        DropDownList35.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10.Visible = false;
                        Button32.Visible = true;
                    }
                    else if (code == "k.11")
                    {
                        DropDownList36.SelectedValue = "2";
                        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11.Visible = false;
                        Button33.Visible = true;
                    }
                }



            }



        }
        catch
        {
        }
    }
    public void bindTypeOfReseach()
    {
        SqlCommand cmd = new SqlCommand("proc_fetchRIDocumentType", con);
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpResearchIncentive.DataSource = dt;
            drpResearchIncentive.DataTextField = "RI_Type";
            drpResearchIncentive.DataValueField = "ID";

            drpResearchIncentive.DataBind();
        }
    }
    public void SP_PMS_Get_Department_Bind()
    {
        dd_department.Items.Clear();
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_PMS_Get_Department();
        if (dr.HasRows)
        {
            dd_department.DataSource = dr;
            dd_department.DataTextField = "Department Name";
            dd_department.DataValueField = "Department Name";
            dd_department.DataBind();
        }
        dr.Close();
        con.DisConnect();
        dd_department.Items.Insert(0, new ListItem("All", "All"));
    }

    public void Enable_Designation_dropdown()
    {
        if (lbl_designation.Text.ToUpper() == "LECTURER".ToUpper() || lbl_designation.Text.ToUpper() == "Tutor UG".ToString()
            || lbl_designation.Text.ToUpper() == "Demonstrator".ToUpper() || lbl_designation.Text.ToUpper() == "Tutor PG".ToUpper() ||
            lbl_designation.Text.ToUpper() == "Senior Resident".ToUpper() || lbl_designation.Text.ToUpper() == "Clinical Instructor".ToUpper()
            || lbl_designation.Text.ToUpper() == "Trainer".ToUpper()
            )
        {
            lbl_designation.Visible = true;
            dd_Designation.Visible = false;
        }
        else if (lbl_designation.Text.ToUpper() == "ASSISTANT PROFESSOR".ToUpper())
        {

            lbl_designation.Visible = true;
            dd_Designation.Visible = false;
        }
        else if (lbl_designation.Text.ToUpper() == "ASSOCIATE PROFESSOR".ToUpper())
        {
            lbl_designation.Visible = true;
            dd_Designation.Visible = false;
        }
        else if (lbl_designation.Text.ToUpper() == "PROFESSOR".ToUpper())
        {
            lbl_designation.Visible = true;
            dd_Designation.Visible = false;
        }
        else
        {
            lbl_designation.Visible = false;
            dd_Designation.Visible = true;
            lbl_designation.Text = dd_Designation.SelectedValue.Trim();
        }
    }
    public void SP_PMS_Count_Filled_Or_Not_Filed_Emaployee_Details()
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_PMS_Count_Filled_Or_Not_Filed_Emaployee_Details(dd_AcademicYear.SelectedValue.Trim(), "");
        dr.Read();
        lnk_Total_PMS_Pending.Text = dr["Pending_data"].ToString().Trim();
        lnk_Total_PMS_Filled.Text = dr["filled_data"].ToString().Trim();
        dr.Close();
        con.DisConnect();
    }
    public void Set_Permission()
    {
        if (Session["Departmentcode"].ToString().Trim() == "D228")
        {
            dd_department.Enabled = true;

            pnl_Count_HR_Details.Visible = true;
            SP_PMS_Count_Filled_Or_Not_Filed_Emaployee_Details();
        }

        if (Session["EmployeePostingGroupl"].ToString().Trim().Trim().ToUpper() == "TEACH")
        {
            dd_department.SelectedValue = Session["PMS_DepartmentName"].ToString().Trim();
            dd_department.Enabled = false;
            if (Session["Departmentcode"].ToString().Trim() == "D228" || Session["uid"].ToString().Trim() == "TMU08026")
            {
                dd_department.Enabled = true;
            }
            pms_connection con = new pms_connection();
            SqlDataReader dr = con.sp_PMS_Check_Filled_Or_Not(dd_AcademicYear.SelectedValue.Trim(), Session["uid"].ToString().Trim());
            dr.Read();
            int countdata = Convert.ToInt32(dr["CountData"].ToString().Trim());
            //string IsFaculty_Approval = dr["IsFaculty_Approval"].ToString().Trim();
            //string IsAssessment_Approval = dr["IsAssessment_Approval"].ToString().Trim();
            //string IsHR_Approval = dr["IsHR_Approval"].ToString().Trim();
            //string IsVC_Approval = dr["IsVC_Approval"].ToString().Trim();

            if (countdata > 0)
            {
                btnCreateNew.Visible = false;
            }
            if (countdata <= 0)
            {
                if (grd_Data.Rows.Count > 0)
                {
                    btnCreateNew.Visible = false;
                }
                else
                {
                    btnCreateNew.Visible = true;
                }
            }
            dr.Close();
            con.DisConnect();
        }

        if (Session["EmployeePostingGroupl"].ToString().Trim().Trim().ToUpper() != "TEACH")
        {
            btnCreateNew.Visible = false;

        }


    }
    public void EnableFileUpload()
    {

        fu_a1.Enabled = true;
        fu_b1.Enabled = true;
        fu_b2.Enabled = true;
        fu_b3.Enabled = true;
        fu_b4.Enabled = true;
        fu_b5.Enabled = true;
        fu_b6.Enabled = true;
        fu_c1.Enabled = true;
        fu_c2.Enabled = true;
        fu_d1.Enabled = true;
        fu_d2.Enabled = true;
        fu_d3.Enabled = true;
        fu_d4.Enabled = true;
        fu_d5.Enabled = true;
        fu_d6.Enabled = true;
        fu_e1.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_g1.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_g2.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_g3.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k1.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k2.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_l1.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_l2.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_l3.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_m1.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_m2.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_m3.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_m4.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_n1.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_n2.Enabled = true;
        fu_CriteriaB_ResearchAndDevelopment_FileUpload_n3.Enabled = true;
        fu_CriteriaD_Administration_FileUpload_o1.Enabled = true;
        fu_CriteriaD_Administration_FileUpload_o2.Enabled = true;
        fu_CriteriaD_Administration_FileUpload_o3.Enabled = true;
        fu_CriteriaD_Administration_FileUpload_o4.Enabled = true;
        fu_CriteriaD_Administration_FileUpload_o5.Enabled = true;

    }
    private void BindFinancialYears_For_Filter()
    {
        // Get the current year
        int currentYear = DateTime.Now.Year - 1;

        // Create a list to hold financial years
        List<string> financialYears = new List<string>();

        // Populate the list with financial years for the last 10 years
        for (int i = 2023; i <= currentYear; i++)
        {
            int year = i;
            string financialYear = year.ToString().Substring(2) + "-" + (year + 1).ToString().Substring(2);
            financialYears.Add(financialYear);
        }

        // Bind the list to the dropdown
        dd_AcademicYear.DataSource = financialYears;
        dd_AcademicYear.DataBind();
        dd_AcademicYear.SelectedValue = currentYear.ToString().Substring(2) + "-" + (currentYear + 1).ToString().Substring(2);
    }

    public void sp_Get_PMS_All_Data(string AcademicYear, string filtertext, string loginID, string DepartmentID, string status)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.sp_Get_PMS_All_Data(AcademicYear.Trim(), filtertext.Trim(), loginID.Trim(), DepartmentID.Trim(), status.Trim(), dd_department.SelectedValue.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grd_Data.DataSource = dt;
        grd_Data.DataBind();
        dr.Close();
        con.DisConnect();
    }


    public void calculatedata()
    {
        if (txt_a1_actualtotalactivites.Text.Trim() == "")
        {
            lbl_a1_apiscore_through_self_assessment.Text = "0";
            lbl_a1_final_obtained_value.Text = "0";
            txt_a1_reporting_authority_assessment.Text = "0";
            fu_a1.Enabled = false;
        }

        else
        {
            //conversion of label and input
            int lbl_a1_Maxapiscore_int = Convert.ToInt32(lbl_a1_Maxapiscore.Text);
            int lbl_a1_maxscoresperacitivity_int = Convert.ToInt32(lbl_a1_maxscoresperacitivity.Text);
            int txt_a1_actualtotalactivites_int = Convert.ToInt32(txt_a1_actualtotalactivites.Text);
            //Multiply: Q = AxB
            int lbl_a1_apiscore_through_self_assessment_int = lbl_a1_maxscoresperacitivity_int * txt_a1_actualtotalactivites_int;
            lbl_a1_apiscore_through_self_assessment.Text = lbl_a1_apiscore_through_self_assessment_int.ToString();

            if (lbl_a1_apiscore_through_self_assessment_int > lbl_a1_Maxapiscore_int)
            {
                lbl_a1_final_obtained_value.Text = lbl_a1_Maxapiscore.Text;
            }
            else
            {
                lbl_a1_final_obtained_value.Text = lbl_a1_apiscore_through_self_assessment.Text;
            }
            if (txt_a1_actualtotalactivites_int <= 0)
            {
                fu_a1.Enabled = false;
            }
            else
            {
                fu_a1.Enabled = true;
            }
            // Always assign the final obtained value to the reporting authority textbox
            txt_a1_reporting_authority_assessment.Text = lbl_a1_final_obtained_value.Text;

            // Convert the value in the reporting authority textbox to integer
            int txt_a1_reporting_authority_assessment_int = Convert.ToInt32(txt_a1_reporting_authority_assessment.Text.Trim());

            // Check if the value exceeds the maximum API score
            if (txt_a1_reporting_authority_assessment_int > lbl_a1_Maxapiscore_int)
            {
                // If the value is greater than the maximum API score, run the JavaScript alert
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);

                // Set the reporting authority textbox value to 0
                txt_a1_reporting_authority_assessment.Text = "0";
            }

        }

        if (txt_b1_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_b1_APIScoreThroughSelfAssessment.Text = "0";
            lbl_b1_FinalObtainedValue.Text = "0";
            txt_b1_ReportingAuthorityAssessment.Text = "0";
            fu_b1.Enabled = false;
        }
        else
        {
            // Conversion of label and input
            int lbl_b1_MaxAPIScore_int = Convert.ToInt32(lbl_b1_MaxAPIScore.Text);
            int lbl_b1_MaxScoresPerActivity_int = Convert.ToInt32(lbl_b1_MaxScoresPerActivity.Text);
            int txt_b1_ActualTotalActivities_int = Convert.ToInt32(txt_b1_ActualTotalActivities.Text);

            // Multiply: Q = AxB
            int lbl_b1_APIScoreThroughSelfAssessment_int = lbl_b1_MaxScoresPerActivity_int * txt_b1_ActualTotalActivities_int;
            lbl_b1_APIScoreThroughSelfAssessment.Text = lbl_b1_APIScoreThroughSelfAssessment_int.ToString();

            if (lbl_b1_APIScoreThroughSelfAssessment_int > lbl_b1_MaxAPIScore_int)
            {
                lbl_b1_FinalObtainedValue.Text = lbl_b1_MaxAPIScore.Text;
            }
            else
            {
                lbl_b1_FinalObtainedValue.Text = lbl_b1_APIScoreThroughSelfAssessment.Text;
            }

            // Always assign the final obtained value to the reporting authority textbox
            txt_b1_ReportingAuthorityAssessment.Text = lbl_b1_FinalObtainedValue.Text;

            int txt_b1_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_b1_ReportingAuthorityAssessment.Text.Trim());

            if (txt_b1_ReportingAuthorityAssessment_int > lbl_b1_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_b1_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_b1_ActualTotalActivities_int <= 0)
            {
                fu_b1.Enabled = false;
            }
            else
            {
                fu_b1.Enabled = true;
            }

        }

        if (txt_b2_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_b2_APIScoreThroughSelfAssessment.Text = "0";
            lbl_b2_FinalObtainedValue.Text = "0";
            txt_b2_ReportingAuthorityAssessment.Text = "0";
            fu_b2.Enabled = false;
        }
        else
        {
            // Conversion of label and input for B2
            int lbl_b2_MaxAPIScore_int = Convert.ToInt32(lbl_b2_MaxAPIScore.Text);
            int lbl_b2_MaxScoresPerActivity_int = Convert.ToInt32(lbl_b2_MaxScoresPerActivity.Text);
            int txt_b2_ActualTotalActivities_int = Convert.ToInt32(txt_b2_ActualTotalActivities.Text);

            // Multiply: Q = AxB for B2
            int lbl_b2_APIScoreThroughSelfAssessment_int = lbl_b2_MaxScoresPerActivity_int * txt_b2_ActualTotalActivities_int;
            lbl_b2_APIScoreThroughSelfAssessment.Text = lbl_b2_APIScoreThroughSelfAssessment_int.ToString();

            if (lbl_b2_APIScoreThroughSelfAssessment_int > lbl_b2_MaxAPIScore_int)
            {
                lbl_b2_FinalObtainedValue.Text = lbl_b2_MaxAPIScore.Text;
            }
            else
            {
                lbl_b2_FinalObtainedValue.Text = lbl_b2_APIScoreThroughSelfAssessment.Text;
            }

            // Always assign the final obtained value to the reporting authority textbox
            txt_b2_ReportingAuthorityAssessment.Text = lbl_b2_FinalObtainedValue.Text;

            int txt_b2_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_b2_ReportingAuthorityAssessment.Text.Trim());

            if (txt_b2_ReportingAuthorityAssessment_int > lbl_b2_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_b2_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_b2_ActualTotalActivities_int <= 0)
            {
                fu_b2.Enabled = false;
            }
            else
            {
                fu_b2.Enabled = true;
            }
        }

        if (txt_b3_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_b3_APIScoreThroughSelfAssessment.Text = "0";
            lbl_b3_FinalObtainedValue.Text = "0";
            txt_b3_ReportingAuthorityAssessment.Text = "0";
            fu_b3.Enabled = false;
        }
        else
        {
            // Conversion of label and input for B3
            int lbl_b3_MaxAPIScore_int = Convert.ToInt32(lbl_b3_MaxAPIScore.Text);
            int lbl_b3_MaxScoresPerActivity_int = Convert.ToInt32(lbl_b3_MaxScoresPerActivity.Text);
            int txt_b3_ActualTotalActivities_int = Convert.ToInt32(txt_b3_ActualTotalActivities.Text);

            // Multiply: Q = AxB for B3
            int lbl_b3_APIScoreThroughSelfAssessment_int = lbl_b3_MaxScoresPerActivity_int * txt_b3_ActualTotalActivities_int;
            lbl_b3_APIScoreThroughSelfAssessment.Text = lbl_b3_APIScoreThroughSelfAssessment_int.ToString();

            if (lbl_b3_APIScoreThroughSelfAssessment_int > lbl_b3_MaxAPIScore_int)
            {
                lbl_b3_FinalObtainedValue.Text = lbl_b3_MaxAPIScore.Text;
            }
            else
            {
                lbl_b3_FinalObtainedValue.Text = lbl_b3_APIScoreThroughSelfAssessment.Text;
            }

            // Always assign the final obtained value to the reporting authority textbox
            txt_b3_ReportingAuthorityAssessment.Text = lbl_b3_FinalObtainedValue.Text;

            int txt_b3_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_b3_ReportingAuthorityAssessment.Text.Trim());

            if (txt_b3_ReportingAuthorityAssessment_int > lbl_b3_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_b3_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_b3_ActualTotalActivities_int <= 0)
            {
                fu_b3.Enabled = false;
            }
            else
            {
                fu_b3.Enabled = true;
            }

        }

        if (txt_b4_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_b4_APIScoreThroughSelfAssessment.Text = "0";
            lbl_b4_FinalObtainedValue.Text = "0";
            txt_b4_ReportingAuthorityAssessment.Text = "0";
            fu_b4.Enabled = false;
        }
        else
        {
            // Conversion of label and input for B4
            int lbl_b4_MaxAPIScore_int = Convert.ToInt32(lbl_b4_MaxAPIScore.Text);
            int lbl_b4_MaxScoresPerActivity_int = Convert.ToInt32(lbl_b4_MaxScoresPerActivity.Text);
            int txt_b4_ActualTotalActivities_int = Convert.ToInt32(txt_b4_ActualTotalActivities.Text);

            // Multiply: Q = AxB for B4
            int lbl_b4_APIScoreThroughSelfAssessment_int = lbl_b4_MaxScoresPerActivity_int * txt_b4_ActualTotalActivities_int;
            lbl_b4_APIScoreThroughSelfAssessment.Text = lbl_b4_APIScoreThroughSelfAssessment_int.ToString();

            if (lbl_b4_APIScoreThroughSelfAssessment_int > lbl_b4_MaxAPIScore_int)
            {
                lbl_b4_FinalObtainedValue.Text = lbl_b4_MaxAPIScore.Text;
            }
            else
            {
                lbl_b4_FinalObtainedValue.Text = lbl_b4_APIScoreThroughSelfAssessment.Text;
            }

            // Always assign the final obtained value to the reporting authority textbox
            txt_b4_ReportingAuthorityAssessment.Text = lbl_b4_FinalObtainedValue.Text;

            int txt_b4_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_b4_ReportingAuthorityAssessment.Text.Trim());

            if (txt_b4_ReportingAuthorityAssessment_int > lbl_b4_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_b4_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_b4_ActualTotalActivities_int <= 0)
            {
                fu_b4.Enabled = false;
            }
            else
            {
                fu_b4.Enabled = true;
            }
        }

        if (txt_b5_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_b5_APIScoreThroughSelfAssessment.Text = "0";
            lbl_b5_FinalObtainedValue.Text = "0";
            txt_b5_ReportingAuthorityAssessment.Text = "0";
            fu_b5.Enabled = false;
        }
        else
        {
            // Conversion of label and input for B5
            int lbl_b5_MaxAPIScore_int = Convert.ToInt32(lbl_b5_MaxAPIScore.Text);
            int lbl_b5_MaxScoresPerActivity_int = Convert.ToInt32(lbl_b5_MaxScoresPerActivity.Text);
            int txt_b5_ActualTotalActivities_int = Convert.ToInt32(txt_b5_ActualTotalActivities.Text);

            // Multiply: Q = AxB for B5
            int lbl_b5_APIScoreThroughSelfAssessment_int = lbl_b5_MaxScoresPerActivity_int * txt_b5_ActualTotalActivities_int;
            lbl_b5_APIScoreThroughSelfAssessment.Text = lbl_b5_APIScoreThroughSelfAssessment_int.ToString();

            if (lbl_b5_APIScoreThroughSelfAssessment_int > lbl_b5_MaxAPIScore_int)
            {
                lbl_b5_FinalObtainedValue.Text = lbl_b5_MaxAPIScore.Text;
            }
            else
            {
                lbl_b5_FinalObtainedValue.Text = lbl_b5_APIScoreThroughSelfAssessment.Text;
            }

            // Always assign the final obtained value to the reporting authority textbox
            txt_b5_ReportingAuthorityAssessment.Text = lbl_b5_FinalObtainedValue.Text;

            int txt_b5_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_b5_ReportingAuthorityAssessment.Text.Trim());

            if (txt_b5_ReportingAuthorityAssessment_int > lbl_b5_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_b5_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_b5_ActualTotalActivities_int <= 0)
            {
                fu_b5.Enabled = false;
            }
            else
            {
                fu_b5.Enabled = true;
            }

        }

        if (txt_b6_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_b6_APIScoreThroughSelfAssessment.Text = "0";
            lbl_b6_FinalObtainedValue.Text = "0";
            txt_b6_ReportingAuthorityAssessment.Text = "0";
            fu_b6.Enabled = false;
        }
        else
        {
            // Conversion of label and input for B6
            int lbl_b6_MaxAPIScore_int = Convert.ToInt32(lbl_b6_MaxAPIScore.Text);
            int lbl_b6_MaxScoresPerActivity_int = Convert.ToInt32(lbl_b6_MaxScoresPerActivity.Text);
            int txt_b6_ActualTotalActivities_int = Convert.ToInt32(txt_b6_ActualTotalActivities.Text);

            // Multiply: Q = AxB for B6
            int lbl_b6_APIScoreThroughSelfAssessment_int = lbl_b6_MaxScoresPerActivity_int * txt_b6_ActualTotalActivities_int;
            lbl_b6_APIScoreThroughSelfAssessment.Text = lbl_b6_APIScoreThroughSelfAssessment_int.ToString();

            if (lbl_b6_APIScoreThroughSelfAssessment_int > lbl_b6_MaxAPIScore_int)
            {
                lbl_b6_FinalObtainedValue.Text = lbl_b6_MaxAPIScore.Text;
            }
            else
            {
                lbl_b6_FinalObtainedValue.Text = lbl_b6_APIScoreThroughSelfAssessment.Text;
            }

            // Always assign the final obtained value to the reporting authority textbox
            txt_b6_ReportingAuthorityAssessment.Text = lbl_b6_FinalObtainedValue.Text;

            int txt_b6_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_b6_ReportingAuthorityAssessment.Text.Trim());

            if (txt_b6_ReportingAuthorityAssessment_int > lbl_b6_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_b6_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_b6_ActualTotalActivities_int <= 0)
            {
                fu_b6.Enabled = false;
            }
            else
            {
                fu_b6.Enabled = true;
            }

        }

        if (txt_c1_RequiredActivities.Text.Trim() == "")
        {
            lbl_c1_TotalAPIScore.Text = "0";
            lbl_c1_FinalScore.Text = "0";
            txt_c1_AssessmentByAuthority.Text = "0";
            fu_c1.Enabled = false;
        }
        else
        {
            // Conversion of label and input for C1
            int lbl_c1_MaxAPIScore_int = Convert.ToInt32(lbl_c1_MaxAPIScore.Text);
            int lbl_c1_ScoresPerActivity_int = Convert.ToInt32(lbl_c1_ScoresPerActivity.Text);
            int txt_c1_RequiredActivities_int = Convert.ToInt32(txt_c1_RequiredActivities.Text);

            // Multiply: Q = AxB for C1
            int lbl_c1_TotalAPIScore_int = lbl_c1_ScoresPerActivity_int * txt_c1_RequiredActivities_int;
            lbl_c1_TotalAPIScore.Text = lbl_c1_TotalAPIScore_int.ToString();

            if (lbl_c1_TotalAPIScore_int > lbl_c1_MaxAPIScore_int)
            {
                lbl_c1_FinalScore.Text = lbl_c1_MaxAPIScore.Text;
            }
            else
            {
                lbl_c1_FinalScore.Text = lbl_c1_TotalAPIScore.Text;
            }

            // Always assign the final obtained value to the authority assessment textbox
            txt_c1_AssessmentByAuthority.Text = lbl_c1_FinalScore.Text;

            int txt_c1_AssessmentByAuthority_int = Convert.ToInt32(txt_c1_AssessmentByAuthority.Text.Trim());

            if (txt_c1_AssessmentByAuthority_int > lbl_c1_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_c1_AssessmentByAuthority.Text = "0";
            }

            if (txt_c1_RequiredActivities_int <= 0)
            {
                fu_c1.Enabled = false;
            }
            else
            {
                fu_c1.Enabled = true;
            }

        }

        if (txt_c2_RequiredActivities.Text.Trim() == "")
        {
            lbl_c2_TotalAPIScore.Text = "0";
            lbl_c2_FinalScore.Text = "0";
            txt_c2_AssessmentByAuthority.Text = "0";
            fu_c2.Enabled = false;
        }
        else
        {
            // Conversion of label and input for C2
            int lbl_c2_MaxAPIScore_int = Convert.ToInt32(lbl_c2_MaxAPIScore.Text);
            int lbl_c2_ScoresPerActivity_int = Convert.ToInt32(lbl_c2_ScoresPerActivity.Text);
            int txt_c2_RequiredActivities_int = Convert.ToInt32(txt_c2_RequiredActivities.Text);

            // Multiply: Q = AxB for C2
            int lbl_c2_TotalAPIScore_int = lbl_c2_ScoresPerActivity_int * txt_c2_RequiredActivities_int;
            lbl_c2_TotalAPIScore.Text = lbl_c2_TotalAPIScore_int.ToString();

            if (lbl_c2_TotalAPIScore_int > lbl_c2_MaxAPIScore_int)
            {
                lbl_c2_FinalScore.Text = lbl_c2_MaxAPIScore.Text;
            }
            else
            {
                lbl_c2_FinalScore.Text = lbl_c2_TotalAPIScore.Text;
            }

            // Always assign the final obtained value to the authority assessment textbox
            txt_c2_AssessmentByAuthority.Text = lbl_c2_FinalScore.Text;

            int txt_c2_AssessmentByAuthority_int = Convert.ToInt32(txt_c2_AssessmentByAuthority.Text.Trim());

            if (txt_c2_AssessmentByAuthority_int > lbl_c2_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_c2_AssessmentByAuthority.Text = "0";
            }

            if (txt_c2_RequiredActivities_int <= 0)
            {
                fu_c2.Enabled = false;
            }
            else
            {
                fu_c2.Enabled = true;
            }

        }

        if (txt_d1_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_d1_APIScoreThroughSelfAssessment.Text = "0";
            lbl_d1_FinalObtainedValue.Text = "0";
            txt_d1_ReportingAuthorityAssessment.Text = "0";
            fu_d1.Enabled = false;
        }
        else
        {
            // Convert label and input values for D1
            int lbl_d1_MaxAPIScore_int = Convert.ToInt32(lbl_d1_MaxAPIScore.Text);
            int lbl_d1_ScoresPerActivity_int = Convert.ToInt32(lbl_d1_MaxScoresPerActivity.Text);
            int txt_d1_ActualTotalActivities_int = Convert.ToInt32(txt_d1_ActualTotalActivities.Text);

            // Multiply: Q = A * B for D1
            int lbl_d1_TotalAPIScore_int = lbl_d1_ScoresPerActivity_int * txt_d1_ActualTotalActivities_int;
            lbl_d1_APIScoreThroughSelfAssessment.Text = lbl_d1_TotalAPIScore_int.ToString();

            // Compare total API score with the max API score
            if (lbl_d1_TotalAPIScore_int > lbl_d1_MaxAPIScore_int)
            {
                lbl_d1_FinalObtainedValue.Text = lbl_d1_MaxAPIScore_int.ToString();
            }
            else
            {
                lbl_d1_FinalObtainedValue.Text = lbl_d1_TotalAPIScore_int.ToString();
            }

            // Validate Reporting Authority Assessment input
            // Always assign the final obtained value to the reporting authority assessment textbox
            txt_d1_ReportingAuthorityAssessment.Text = lbl_d1_FinalObtainedValue.Text;

            int txt_d1_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_d1_ReportingAuthorityAssessment.Text.Trim());

            if (txt_d1_ReportingAuthorityAssessment_int > lbl_d1_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d1_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_d1_ActualTotalActivities_int <= 0)
            {
                fu_d1.Enabled = false;
            }
            else
            {
                fu_d1.Enabled = true;
            }

        }

        if (txt_d2_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_d2_APIScoreThroughSelfAssessment.Text = "0";
            lbl_d2_FinalObtainedValue.Text = "0";
            txt_d2_ReportingAuthorityAssessment.Text = "0";
            fu_d2.Enabled = false;
        }
        else
        {
            // Convert label and input values for D2
            int lbl_d2_MaxAPIScore_int = Convert.ToInt32(lbl_d2_MaxAPIScore.Text);
            int lbl_d2_ScoresPerActivity_int = Convert.ToInt32(lbl_d2_MaxScoresPerActivity.Text);
            int txt_d2_ActualTotalActivities_int = Convert.ToInt32(txt_d2_ActualTotalActivities.Text);

            // Multiply: Q = A * B for D2
            int lbl_d2_TotalAPIScore_int = lbl_d2_ScoresPerActivity_int * txt_d2_ActualTotalActivities_int;
            lbl_d2_APIScoreThroughSelfAssessment.Text = lbl_d2_TotalAPIScore_int.ToString();

            // Compare total API score with the max API score
            if (lbl_d2_TotalAPIScore_int > lbl_d2_MaxAPIScore_int)
            {
                lbl_d2_FinalObtainedValue.Text = lbl_d2_MaxAPIScore_int.ToString();
            }
            else
            {
                lbl_d2_FinalObtainedValue.Text = lbl_d2_TotalAPIScore_int.ToString();
            }

            // Validate Reporting Authority Assessment input
            // Always assign the final obtained value to the reporting authority assessment textbox
            txt_d2_ReportingAuthorityAssessment.Text = lbl_d2_FinalObtainedValue.Text;

            int txt_d2_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_d2_ReportingAuthorityAssessment.Text.Trim());

            if (txt_d2_ReportingAuthorityAssessment_int > lbl_d2_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d2_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_d2_ActualTotalActivities_int <= 0)
            {
                fu_d2.Enabled = false;
            }
            else
            {
                fu_d2.Enabled = true;
            }

        }

        if (txt_d3_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_d3_APIScoreThroughSelfAssessment.Text = "0";
            lbl_d3_FinalObtainedValue.Text = "0";
            txt_d3_ReportingAuthorityAssessment.Text = "0";
            fu_d3.Enabled = false;
        }
        else
        {
            // Convert label and input values for D3
            int lbl_d3_MaxAPIScore_int = Convert.ToInt32(lbl_d3_MaxAPIScore.Text);
            int lbl_d3_ScoresPerActivity_int = Convert.ToInt32(lbl_d3_MaxScoresPerActivity.Text);
            int txt_d3_ActualTotalActivities_int = Convert.ToInt32(txt_d3_ActualTotalActivities.Text);

            // Multiply: Q = A * B for D3
            int lbl_d3_TotalAPIScore_int = lbl_d3_ScoresPerActivity_int * txt_d3_ActualTotalActivities_int;
            lbl_d3_APIScoreThroughSelfAssessment.Text = lbl_d3_TotalAPIScore_int.ToString();

            // Compare total API score with the max API score
            if (lbl_d3_TotalAPIScore_int > lbl_d3_MaxAPIScore_int)
            {
                lbl_d3_FinalObtainedValue.Text = lbl_d3_MaxAPIScore_int.ToString();
            }
            else
            {
                lbl_d3_FinalObtainedValue.Text = lbl_d3_TotalAPIScore_int.ToString();
            }

            // Validate Reporting Authority Assessment input
            // Always assign the final obtained value to the reporting authority assessment textbox
            txt_d3_ReportingAuthorityAssessment.Text = lbl_d3_FinalObtainedValue.Text;

            int txt_d3_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_d3_ReportingAuthorityAssessment.Text.Trim());

            if (txt_d3_ReportingAuthorityAssessment_int > lbl_d3_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d3_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_d3_ActualTotalActivities_int <= 0)
            {
                fu_d3.Enabled = false;
            }
            else
            {
                fu_d3.Enabled = true;
            }

        }
        if (txt_d4_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_d4_APIScoreThroughSelfAssessment.Text = "0";
            lbl_d4_FinalObtainedValue.Text = "0";
            txt_d4_ReportingAuthorityAssessment.Text = "0";
            fu_d4.Enabled = false;
        }
        else
        {
            // Convert label and input values for D4
            int lbl_d4_MaxAPIScore_int = Convert.ToInt32(lbl_d4_MaxAPIScore.Text);
            int lbl_d4_ScoresPerActivity_int = Convert.ToInt32(lbl_d4_MaxScoresPerActivity.Text);
            int txt_d4_ActualTotalActivities_int = Convert.ToInt32(txt_d4_ActualTotalActivities.Text);

            // Multiply: Q = A * B for D4
            int lbl_d4_TotalAPIScore_int = lbl_d4_ScoresPerActivity_int * txt_d4_ActualTotalActivities_int;
            lbl_d4_APIScoreThroughSelfAssessment.Text = lbl_d4_TotalAPIScore_int.ToString();

            // Compare total API score with the max API score
            if (lbl_d4_TotalAPIScore_int > lbl_d4_MaxAPIScore_int)
            {
                lbl_d4_FinalObtainedValue.Text = lbl_d4_MaxAPIScore_int.ToString();
            }
            else
            {
                lbl_d4_FinalObtainedValue.Text = lbl_d4_TotalAPIScore_int.ToString();
            }

            // Validate Reporting Authority Assessment input
            // Always assign the final obtained value to the reporting authority assessment textbox
            txt_d4_ReportingAuthorityAssessment.Text = lbl_d4_FinalObtainedValue.Text;

            int txt_d4_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_d4_ReportingAuthorityAssessment.Text.Trim());

            if (txt_d4_ReportingAuthorityAssessment_int > lbl_d4_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d4_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_d4_ActualTotalActivities_int <= 0)
            {
                fu_d4.Enabled = false;
            }
            else
            {
                fu_d4.Enabled = true;
            }

        }
        if (txt_d5_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_d5_APIScoreThroughSelfAssessment.Text = "0";
            lbl_d5_FinalObtainedValue.Text = "0";
            txt_d5_ReportingAuthorityAssessment.Text = "0";
            fu_d5.Enabled = false;
        }
        else
        {
            // Convert label and input values for D5
            int lbl_d5_MaxAPIScore_int = Convert.ToInt32(lbl_d5_MaxAPIScore.Text);
            int lbl_d5_ScoresPerActivity_int = Convert.ToInt32(lbl_d5_MaxScoresPerActivity.Text);
            int txt_d5_ActualTotalActivities_int = Convert.ToInt32(txt_d5_ActualTotalActivities.Text);

            // Multiply: Q = A * B for D5
            int lbl_d5_TotalAPIScore_int = lbl_d5_ScoresPerActivity_int * txt_d5_ActualTotalActivities_int;
            lbl_d5_APIScoreThroughSelfAssessment.Text = lbl_d5_TotalAPIScore_int.ToString();

            // Compare total API score with the max API score
            if (lbl_d5_TotalAPIScore_int > lbl_d5_MaxAPIScore_int)
            {
                lbl_d5_FinalObtainedValue.Text = lbl_d5_MaxAPIScore_int.ToString();
            }
            else
            {
                lbl_d5_FinalObtainedValue.Text = lbl_d5_TotalAPIScore_int.ToString();
            }

            // Validate Reporting Authority Assessment input
            // Always assign the final obtained value to the reporting authority assessment textbox
            txt_d5_ReportingAuthorityAssessment.Text = lbl_d5_FinalObtainedValue.Text;

            int txt_d5_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_d5_ReportingAuthorityAssessment.Text.Trim());

            if (txt_d5_ReportingAuthorityAssessment_int > lbl_d5_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d5_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_d5_ActualTotalActivities_int <= 0)
            {
                fu_d5.Enabled = false;
            }
            else
            {
                fu_d5.Enabled = true;
            }

        }
        if (txt_d6_ActualTotalActivities.Text.Trim() == "")
        {
            lbl_d6_APIScoreThroughSelfAssessment.Text = "0";
            lbl_d6_FinalObtainedValue.Text = "0";
            txt_d6_ReportingAuthorityAssessment.Text = "0";
            fu_d6.Enabled = false;
        }
        else
        {
            // Convert label and input values for D6
            int lbl_d6_MaxAPIScore_int = Convert.ToInt32(lbl_d6_MaxAPIScore.Text);
            int lbl_d6_MaxSctxt_d6_ActualTotalActivitiesoresPerActivity_int = Convert.ToInt32(lbl_d6_MaxSctxt_d6_ActualTotalActivitiesoresPerActivity.Text);
            int txt_d6_ActualTotalActivities_int = Convert.ToInt32(txt_d6_ActualTotalActivities.Text);

            // Multiply: Q = A * B for D6
            int lbl_d6_TotalAPIScore_int = lbl_d6_MaxSctxt_d6_ActualTotalActivitiesoresPerActivity_int * txt_d6_ActualTotalActivities_int;
            lbl_d6_APIScoreThroughSelfAssessment.Text = lbl_d6_TotalAPIScore_int.ToString();

            // Compare total API score with the max API score
            if (lbl_d6_TotalAPIScore_int > lbl_d6_MaxAPIScore_int)
            {
                lbl_d6_FinalObtainedValue.Text = lbl_d6_MaxAPIScore_int.ToString();
            }
            else
            {
                lbl_d6_FinalObtainedValue.Text = lbl_d6_TotalAPIScore_int.ToString();
            }

            // Validate Reporting Authority Assessment input
            // Always assign the final obtained value to the reporting authority assessment textbox
            txt_d6_ReportingAuthorityAssessment.Text = lbl_d6_FinalObtainedValue.Text;

            int txt_d6_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_d6_ReportingAuthorityAssessment.Text.Trim());

            if (txt_d6_ReportingAuthorityAssessment_int > lbl_d6_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d6_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_d6_ActualTotalActivities_int <= 0)
            {
                fu_d6.Enabled = false;
            }
            else
            {
                fu_d6.Enabled = true;
            }

        }
        if (txt_e1_ScoresPerActivity.Text.Trim() == "")
        {
            lbl_e1_APIScoreThroughSelfAssessment.Text = "0";
            lbl_e1_FinalObtainedValue.Text = "0";
            txt_e1_ReportingAuthorityAssessment.Text = "0";
            fu_e1.Enabled = false;
        }
        else
        {
            // Convert label and input values for E1
            int lbl_e1_MaxAPIScore_int = Convert.ToInt32(lbl_e1_MaxAPIScore.Text);
            int lbl_e1_ScoresPerActivity_int = Convert.ToInt32(lbl_e1_MaxScoresPerActivity.Text);
            int txt_e1_ActualTotalActivities_int = Convert.ToInt32(txt_e1_ScoresPerActivity.Text);

            // Multiply: Q = A * B for E1
            int lbl_e1_TotalAPIScore_int = lbl_e1_ScoresPerActivity_int * txt_e1_ActualTotalActivities_int;
            lbl_e1_APIScoreThroughSelfAssessment.Text = lbl_e1_TotalAPIScore_int.ToString();

            // Compare total API score with the max API score
            if (lbl_e1_TotalAPIScore_int > lbl_e1_MaxAPIScore_int)
            {
                lbl_e1_FinalObtainedValue.Text = lbl_e1_MaxAPIScore_int.ToString();
            }
            else
            {
                lbl_e1_FinalObtainedValue.Text = lbl_e1_TotalAPIScore_int.ToString();
            }

            // Validate Reporting Authority Assessment input
            // Always assign the final obtained value to the reporting authority assessment textbox
            txt_e1_ReportingAuthorityAssessment.Text = lbl_e1_FinalObtainedValue.Text;

            int txt_e1_ReportingAuthorityAssessment_int = Convert.ToInt32(txt_e1_ReportingAuthorityAssessment.Text.Trim());

            if (txt_e1_ReportingAuthorityAssessment_int > lbl_e1_MaxAPIScore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_e1_ReportingAuthorityAssessment.Text = "0";
            }

            if (txt_e1_ActualTotalActivities_int <= 0)
            {
                fu_e1.Enabled = false;
            }
            else
            {
                fu_e1.Enabled = true;
            }

        }
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1_2.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2.Text = "0";


        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1.Text == "" || txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2.Text == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1.Enabled = false;
        }
        else
        {
            //LabelConversion
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f1.Text.Trim());
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f1_2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f1_2.Text.Trim());
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f1.Text.Trim());


            //Conversion
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2.Text.Trim());
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1.Text.Trim());
            int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text.Trim());

            //Multiplication
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_int = (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_int * lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f1_int) + (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2_int * lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f1_2_int);
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_int.ToString();

            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f1_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f1.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1.Text;
            }
            //Reporting Authority
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1.Text;
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f1_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text = "0";
            }
            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1.Enabled = true;
            }
        }




        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2.Enabled = false;
        }
        else
        {
            // Convert label and input values for F2
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f2.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f2.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Text);

            // Multiply: Total API Score = Scores per Activity * Number of Activities
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f2_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f2_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f2_int.ToString();
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2_int.ToString();
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f2_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2.Enabled = true;
            }
        }

        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3_2.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2.Text = "0";

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3.Text == "" || txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2.Text == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3.Enabled = false;
        }
        else
        {
            // LabelConversion
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f3.Text.Trim());
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f3_2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f3_2.Text.Trim());
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f3.Text.Trim());

            // Conversion
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2.Text.Trim());
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3.Text.Trim());
            int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text.Trim());

            // Multiplication
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_int = (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_int * lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f3_int) + (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2_int * lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f3_2_int);
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_int.ToString();

            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f3_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f3.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3.Text;
            }

            // Reporting Authority
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3.Text;
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f3_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text = "0";
            }
            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3.Enabled = true;
            }
        }


        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4.Enabled = false;
        }
        else
        {
            // Convert label and input values for F4
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f4.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f4.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Text);

            // Multiply: Total API Score = Scores per Activity * Number of Activities
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f4_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f4_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f4_int.ToString();
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4_int.ToString();
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f4_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for f5
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f5.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f5.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Text);

            // Multiply: Q = A * B for f5
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f5_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f5_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f5_int.ToString();
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f5_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for f6
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f6_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f6.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f6_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f6.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Text);

            // Multiply: Q = A * B for f6
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f6_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f6_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f6_int.ToString();
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f6_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f7.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for f7
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f7_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f7.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f7_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f7.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Text);

            // Multiply: Q = A * B for f7
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f7_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_f7_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f7_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f7_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f7_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f7_int.ToString();
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f7.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f7_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_g1.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for g1
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g1.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g1.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1.Text);

            // Multiply: Q = A * B for g1
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g1_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g1_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g1_int.ToString();
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g1_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g1.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g1.Enabled = true;
            }
        }


        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_g2.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for g2
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g2.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g2.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2.Text);

            // Multiply: Q = A * B for g2
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g2_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g2_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g2.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g2_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g2.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g2.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_g3.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for g3
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g3.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g3.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3.Text);

            // Multiply: Q = A * B for g3
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_g3_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g3_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g3.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g3_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g3.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g3.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for h1
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h1.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h1.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Text);

            // Multiply: Q = A * B for h1
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h1_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h1_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h1.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h1_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for h2
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h2.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h2.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Text);

            // Multiply: Q = A * B for h2
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h2_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h2_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h2.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h2_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for h3
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h3.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h3.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Text);

            // Multiply: Q = A * B for h3
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h3_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h3_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h3.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h3_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for h4
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h4.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h4.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Text);

            // Multiply: Q = A * B for h4
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h4_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h4_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h4.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h4_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for h5
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h5.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h5.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Text);

            // Multiply: Q = A * B for h5
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h5_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h5_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h5.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h5_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for h6
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h6_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h6.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h6_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h6.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Text);

            // Multiply: Q = A * B for h6
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h6_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h6_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h6.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h6_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for h7
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h7_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h7.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h7_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h7.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Text);

            // Multiply: Q = A * B for h7
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h7_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h7_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h7.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h7_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for h8
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h8_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h8.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Text);

            // Multiply: Q = A * B for h8
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h8_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for h9
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h9_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h9.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h9_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h9.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Text);

            // Multiply: Q = A * B for h9
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_h9_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h9_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h9.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h9_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i1.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for i1
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i1.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i1.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1.Text);

            // Multiply: Q = A * B for i1
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i1_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i1_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i1.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i1.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i1.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i1_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for i2
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i2.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i2.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Text);

            // Multiply: Q = A * B for i2
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i2_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i2_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i2.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i2_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for i3
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i3.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i3.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Text);

            // Multiply: Q = A * B for i3
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i3_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i3_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i3.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i3_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for i4
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i4.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i4.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Text);

            // Multiply: Q = A * B for i4
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i4_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i4_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i4.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i4_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for i5
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i5.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i5.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Text);

            // Multiply: Q = A * B for i5
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_i5_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i5_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i5.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i5_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for j1
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j1.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_j1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_j1.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Text);

            // Multiply: Q = A * B for j1
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_j1_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j1_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j1.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j1_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for j2
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j2.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_j2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_j2.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Text);

            // Multiply: Q = A * B for j2
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_j2_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j2_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j2.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j2_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k1.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for k1
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k1.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k1.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1.Text);

            // Multiply: Q = A * B for k1
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k1_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k1_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k1.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k1_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k1.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k1.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k2.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for k2
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k2.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k2.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2.Text);

            // Multiply: Q = A * B for k2
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k2_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k2_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k2.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k2_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k2.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k2.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for k3
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k3.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k3.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Text);

            // Multiply: Q = A * B for k3
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k3_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k3_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k3.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k3_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for k4
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k4.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k4.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Text);

            // Multiply: Q = A * B for k4
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k4_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k4_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k4.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k4_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4.Enabled = true;
            }
        }



        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for k5
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k5.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k5.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Text);

            // Multiply: Q = A * B for k5
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k5_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k5_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k5.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k5_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for k6
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k6_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k6.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k6_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k6.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Text);

            // Multiply: Q = A * B for k6
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k6_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k6_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k6.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k6_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6.Enabled = true;
            }
        }


        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for k7
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k7_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k7.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k7_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k7.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Text);

            // Multiply: Q = A * B for k7
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k7_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k7_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k7.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k7_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for k8
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k8_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k8.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k8_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k8.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Text);

            // Multiply: Q = A * B for k8
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k8_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k8_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k8.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k8_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for k9
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k9_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k9.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k9_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k9.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Text);

            // Multiply: Q = A * B for k9
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k9_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k9_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k9.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k9_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for k10
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k10_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k10.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k10_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k10.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Text);

            // Multiply: Q = A * B for k10
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k10_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k10_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k10.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k10_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for k11
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k11_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k11.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k11_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k11.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Text);

            // Multiply: Q = A * B for k11
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_k11_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k11_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k11.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k11_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_l1.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for l1
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l1.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l1.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1.Text);

            // Multiply: Q = A * B for l1
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l1_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l1_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l1.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l1_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l1.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l1.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_l2.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for l2
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l2.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l2.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2.Text);

            // Multiply: Q = A * B for l2
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l2_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l2_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l2.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l2_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l2.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l2.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_l3.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for l3
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l3.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l3.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3.Text);

            // Multiply: Q = A * B for l3
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_l3_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l3_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l3.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l3_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l3.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l3.Enabled = true;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_m1.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for m1
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m1.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m1.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1.Text);

            // Multiply: Q = A * B for m1
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m1_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m1_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m1.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m1_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m1.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m1.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_m2.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for m2
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m2.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m2.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2.Text);

            // Multiply: Q = A * B for m2
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m2_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m2_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m2.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m2_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m2.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m2.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_m3.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for m3
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m3.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m3.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3.Text);

            // Multiply: Q = A * B for m3
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m3_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m3_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m3.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m3_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m3.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m3.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_m4.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for m4
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m4.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m4.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4.Text);

            // Multiply: Q = A * B for m4
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_m4_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m4_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m4.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m4_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m4.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m4.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_n1.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for n1
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n1.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n1.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1.Text);

            // Multiply: Q = A * B for n1
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n1_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n1_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n1.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1.Text;
            }

            // Validate Reporting Authority Assessment input

            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n1_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n1.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n1.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_n2.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for n2
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n2.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n2.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2.Text);

            // Multiply: Q = A * B for n2
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n2_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n2_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n2.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n2_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n2.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n2.Enabled = true;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3.Text.Trim() == "")
        {
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3.Text = "0";
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3.Text = "0";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text = "0";
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_n3.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for n3
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n3.Text);
            int lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n3.Text);
            int txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3.Text);

            // Multiply: Q = A * B for n3
            int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3_int = lbl_CriteriaB_ResearchAndDevelopment_ScoresPerActivity_n3_int * txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3_int;
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n3_int)
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3.Text = lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n3.Text;
            }
            else
            {
                lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3.Text;
            }

            // Validate Reporting Authority Assessment input
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3.Text;

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3_int > lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n3_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text = "0";
            }

            if (txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3_int <= 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n3.Enabled = false;
            }
            else
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n3.Enabled = true;
            }
        }
        if (txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1.Text.Trim() == "")
        {
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1.Text = "0";
            lbl_CriteriaD_Administration_TotalorFaculty_o1.Text = "0";
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text = "0";
            fu_CriteriaD_Administration_FileUpload_o1.Enabled = false;
        }
        else
        {
            // Conversion of label and input values for o1
            int lbl_CriteriaD_Administration_MaxAPIScore_o1_int = Convert.ToInt32(lbl_CriteriaD_Administration_MaxAPIScore_o1.Text);
            int lbl_CriteriaD_Administration_ScoresPerActivity_o1_int = Convert.ToInt32(lbl_CriteriaD_Administration_ScoresPerActivity_o1.Text);
            int txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1_int = Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1.Text);

            // Multiply: Q = A * B for o1
            int lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1_int = lbl_CriteriaD_Administration_ScoresPerActivity_o1_int * txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1_int;
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1.Text = lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1_int.ToString();

            // Compare total API score with the max API score
            if (lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1_int > lbl_CriteriaD_Administration_MaxAPIScore_o1_int)
            {
                lbl_CriteriaD_Administration_TotalorFaculty_o1.Text = lbl_CriteriaD_Administration_MaxAPIScore_o1.Text;
            }
            else
            {
                lbl_CriteriaD_Administration_TotalorFaculty_o1.Text = lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1.Text;
            }

            // Validate Reporting Authority Assessment input
            // Set initial value from the label
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text = lbl_CriteriaD_Administration_TotalorFaculty_o1.Text;

            int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1_int = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text.Trim());

            // Check if the entered value exceeds the maximum API score
            if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1_int > lbl_CriteriaD_Administration_MaxAPIScore_o1_int)
            {
                // JavaScript alert for exceeding maximum API score
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text = "0";
            }

            if (txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1_int <= 0)
            {
                fu_CriteriaD_Administration_FileUpload_o1.Enabled = false;
            }
            else
            {
                fu_CriteriaD_Administration_FileUpload_o1.Enabled = true;
            }

        }
        if (txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2.Text.Trim() == "")
        {
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2.Text = "0";
            lbl_CriteriaD_Administration_TotalorFaculty_o2.Text = "0";
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text = "0";
            fu_CriteriaD_Administration_FileUpload_o2.Enabled = false;
        }
        else
        {
            int lbl_CriteriaD_Administration_MaxAPIScore_o2_int = Convert.ToInt32(lbl_CriteriaD_Administration_MaxAPIScore_o2.Text);
            int lbl_CriteriaD_Administration_ScoresPerActivity_o2_int = Convert.ToInt32(lbl_CriteriaD_Administration_ScoresPerActivity_o2.Text);
            int txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2_int = Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2.Text);

            int lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2_int = lbl_CriteriaD_Administration_ScoresPerActivity_o2_int * txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2_int;
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2.Text = lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2_int.ToString();

            if (lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2_int > lbl_CriteriaD_Administration_MaxAPIScore_o2_int)
            {
                lbl_CriteriaD_Administration_TotalorFaculty_o2.Text = lbl_CriteriaD_Administration_MaxAPIScore_o2.Text;
            }
            else
            {
                lbl_CriteriaD_Administration_TotalorFaculty_o2.Text = lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2.Text;
            }

            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text = lbl_CriteriaD_Administration_TotalorFaculty_o2.Text;
            int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2_int = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text.Trim());

            if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2_int > lbl_CriteriaD_Administration_MaxAPIScore_o2_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text = "0";
            }

            if (txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2_int <= 0)
            {
                fu_CriteriaD_Administration_FileUpload_o2.Enabled = false;
            }
            else
            {
                fu_CriteriaD_Administration_FileUpload_o2.Enabled = true;
            }
        }
        if (txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3.Text.Trim() == "")
        {
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3.Text = "0";
            lbl_CriteriaD_Administration_TotalorFaculty_o3.Text = "0";
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text = "0";
            fu_CriteriaD_Administration_FileUpload_o3.Enabled = false;
        }
        else
        {
            int lbl_CriteriaD_Administration_MaxAPIScore_o3_int = Convert.ToInt32(lbl_CriteriaD_Administration_MaxAPIScore_o3.Text);
            int lbl_CriteriaD_Administration_ScoresPerActivity_o3_int = Convert.ToInt32(lbl_CriteriaD_Administration_ScoresPerActivity_o3.Text);
            int txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3_int = Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3.Text);

            int lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3_int = lbl_CriteriaD_Administration_ScoresPerActivity_o3_int * txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3_int;
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3.Text = lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3_int.ToString();

            if (lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3_int > lbl_CriteriaD_Administration_MaxAPIScore_o3_int)
            {
                lbl_CriteriaD_Administration_TotalorFaculty_o3.Text = lbl_CriteriaD_Administration_MaxAPIScore_o3.Text;
            }
            else
            {
                lbl_CriteriaD_Administration_TotalorFaculty_o3.Text = lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3.Text;
            }

            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text = lbl_CriteriaD_Administration_TotalorFaculty_o3.Text;
            int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3_int = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text.Trim());

            if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3_int > lbl_CriteriaD_Administration_MaxAPIScore_o3_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text = "0";
            }

            if (txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3_int <= 0)
            {
                fu_CriteriaD_Administration_FileUpload_o3.Enabled = false;
            }
            else
            {
                fu_CriteriaD_Administration_FileUpload_o3.Enabled = true;
            }
        }
        if (txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4.Text.Trim() == "")
        {
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4.Text = "0";
            lbl_CriteriaD_Administration_TotalorFaculty_o4.Text = "0";
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text = "0";
            fu_CriteriaD_Administration_FileUpload_o4.Enabled = false;
        }
        else
        {
            int lbl_CriteriaD_Administration_MaxAPIScore_o4_int = Convert.ToInt32(lbl_CriteriaD_Administration_MaxAPIScore_o4.Text);
            int lbl_CriteriaD_Administration_ScoresPerActivity_o4_int = Convert.ToInt32(lbl_CriteriaD_Administration_ScoresPerActivity_o4.Text);
            int txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4_int = Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4.Text);

            int lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4_int = lbl_CriteriaD_Administration_ScoresPerActivity_o4_int * txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4_int;
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4.Text = lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4_int.ToString();

            if (lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4_int > lbl_CriteriaD_Administration_MaxAPIScore_o4_int)
            {
                lbl_CriteriaD_Administration_TotalorFaculty_o4.Text = lbl_CriteriaD_Administration_MaxAPIScore_o4.Text;
            }
            else
            {
                lbl_CriteriaD_Administration_TotalorFaculty_o4.Text = lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4.Text;
            }

            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text = lbl_CriteriaD_Administration_TotalorFaculty_o4.Text;
            int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4_int = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text.Trim());

            if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4_int > lbl_CriteriaD_Administration_MaxAPIScore_o4_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text = "0";
            }

            if (txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4_int <= 0)
            {
                fu_CriteriaD_Administration_FileUpload_o4.Enabled = false;
            }
            else
            {
                fu_CriteriaD_Administration_FileUpload_o4.Enabled = true;
            }
        }
        if (txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5.Text.Trim() == "")
        {
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5.Text = "0";
            lbl_CriteriaD_Administration_TotalorFaculty_o5.Text = "0";
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text = "0";
            fu_CriteriaD_Administration_FileUpload_o5.Enabled = false;
        }
        else
        {
            int lbl_CriteriaD_Administration_MaxAPIScore_o5_int = Convert.ToInt32(lbl_CriteriaD_Administration_MaxAPIScore_o5.Text);
            int lbl_CriteriaD_Administration_ScoresPerActivity_o5_int = Convert.ToInt32(lbl_CriteriaD_Administration_ScoresPerActivity_o5.Text);
            int txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5_int = Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5.Text);

            int lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5_int = lbl_CriteriaD_Administration_ScoresPerActivity_o5_int * txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5_int;
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5.Text = lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5_int.ToString();

            if (lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5_int > lbl_CriteriaD_Administration_MaxAPIScore_o5_int)
            {
                lbl_CriteriaD_Administration_TotalorFaculty_o5.Text = lbl_CriteriaD_Administration_MaxAPIScore_o5.Text;
            }
            else
            {
                lbl_CriteriaD_Administration_TotalorFaculty_o5.Text = lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5.Text;
            }

            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text = lbl_CriteriaD_Administration_TotalorFaculty_o5.Text;
            int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5_int = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text.Trim());

            if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5_int > lbl_CriteriaD_Administration_MaxAPIScore_o5_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text = "0";
            }

            if (txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5_int <= 0)
            {
                fu_CriteriaD_Administration_FileUpload_o5.Enabled = false;
            }
            else
            {
                fu_CriteriaD_Administration_FileUpload_o5.Enabled = true;
            }
        }

        if (txt_p1_ReportingAuthority.Text == "")
        {
            txt_p1_ReportingAuthority.Text = "0";

        }
        else
        {
            int txt_p1_ReportingAuthority_int = Convert.ToInt32(txt_p1_ReportingAuthority.Text.Trim());
            int lbl_p1_maxapiscore_int = Convert.ToInt32(lbl_p1_maxapiscore.Text.Trim());

            if (txt_p1_ReportingAuthority_int > lbl_p1_maxapiscore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_p1_ReportingAuthority.Text = "0";
            }


        }

        if (txt_p2_ReportingAuthority.Text == "")
        {
            // Handle empty case
            txt_p2_ReportingAuthority.Text = "0";

        }
        else
        {
            int txt_p2_ReportingAuthority_int = Convert.ToInt32(txt_p2_ReportingAuthority.Text.Trim());
            int lbl_p2_maxapiscore_int = Convert.ToInt32(lbl_p2_maxapiscore.Text.Trim());

            if (txt_p2_ReportingAuthority_int > lbl_p2_maxapiscore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_p2_ReportingAuthority.Text = "0";
            }
        }

        // Repeat the same for p3 to p10
        if (txt_p3_ReportingAuthority.Text == "")
        {
            // Handle empty case
            txt_p3_ReportingAuthority.Text = "0";

        }
        else
        {
            int txt_p3_ReportingAuthority_int = Convert.ToInt32(txt_p3_ReportingAuthority.Text.Trim());
            int lbl_p3_maxapiscore_int = Convert.ToInt32(lbl_p3_maxapiscore.Text.Trim());

            if (txt_p3_ReportingAuthority_int > lbl_p3_maxapiscore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_p3_ReportingAuthority.Text = "0";
            }
        }

        // Continue for p4 to p10 similarly
        if (txt_p4_ReportingAuthority.Text == "")
        {
            // Handle empty case
            txt_p4_ReportingAuthority.Text = "0";

        }
        else
        {
            int txt_p4_ReportingAuthority_int = Convert.ToInt32(txt_p4_ReportingAuthority.Text.Trim());
            int lbl_p4_maxapiscore_int = Convert.ToInt32(lbl_p4_maxapiscore.Text.Trim());

            if (txt_p4_ReportingAuthority_int > lbl_p4_maxapiscore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_p4_ReportingAuthority.Text = "0";
            }
        }

        // For p5
        if (txt_p5_ReportingAuthority.Text == "")
        {
            // Handle empty case
            txt_p5_ReportingAuthority.Text = "0";

        }
        else
        {
            int txt_p5_ReportingAuthority_int = Convert.ToInt32(txt_p5_ReportingAuthority.Text.Trim());
            int lbl_p5_maxapiscore_int = Convert.ToInt32(lbl_p5_maxapiscore.Text.Trim());

            if (txt_p5_ReportingAuthority_int > lbl_p5_maxapiscore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_p5_ReportingAuthority.Text = "0";
            }
        }

        // For p6
        if (txt_p6_ReportingAuthority.Text == "")
        {
            // Handle empty case
            txt_p6_ReportingAuthority.Text = "0";

        }
        else
        {
            int txt_p6_ReportingAuthority_int = Convert.ToInt32(txt_p6_ReportingAuthority.Text.Trim());
            int lbl_p6_maxapiscore_int = Convert.ToInt32(lbl_p6_maxapiscore.Text.Trim());

            if (txt_p6_ReportingAuthority_int > lbl_p6_maxapiscore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_p6_ReportingAuthority.Text = "0";
            }
        }

        // For p7
        if (txt_p7_ReportingAuthority.Text == "")
        {
            txt_p7_ReportingAuthority.Text = "0";
        }
        else
        {
            int txt_p7_ReportingAuthority_int = Convert.ToInt32(txt_p7_ReportingAuthority.Text.Trim());
            int lbl_p7_maxapiscore_int = Convert.ToInt32(lbl_p7_maxapiscore.Text.Trim());

            if (txt_p7_ReportingAuthority_int > lbl_p7_maxapiscore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_p7_ReportingAuthority.Text = "0";
            }
        }

        // For p8
        if (txt_p8_ReportingAuthority.Text == "")
        {
            // Handle empty case
            txt_p8_ReportingAuthority.Text = "0";

        }
        else
        {
            int txt_p8_ReportingAuthority_int = Convert.ToInt32(txt_p8_ReportingAuthority.Text.Trim());
            int lbl_p8_maxapiscore_int = Convert.ToInt32(lbl_p8_maxapiscore.Text.Trim());

            if (txt_p8_ReportingAuthority_int > lbl_p8_maxapiscore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_p8_ReportingAuthority.Text = "0";
            }
        }

        // For p9
        if (txt_p9_ReportingAuthority.Text == "")
        {
            // Handle empty case
            txt_p9_ReportingAuthority.Text = "0";

        }
        else
        {
            int txt_p9_ReportingAuthority_int = Convert.ToInt32(txt_p9_ReportingAuthority.Text.Trim());
            int lbl_p9_maxapiscore_int = Convert.ToInt32(lbl_p9_maxapiscore.Text.Trim());

            if (txt_p9_ReportingAuthority_int > lbl_p9_maxapiscore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_p9_ReportingAuthority.Text = "0";
            }
        }

        // For p10
        if (txt_p10_ReportingAuthority.Text == "")
        {
            txt_p10_ReportingAuthority.Text = "0";
        }
        else
        {
            int txt_p10_ReportingAuthority_int = Convert.ToInt32(txt_p10_ReportingAuthority.Text.Trim());
            int lbl_p10_maxapiscore_int = Convert.ToInt32(lbl_p10_maxapiscore.Text.Trim());

            if (txt_p10_ReportingAuthority_int > lbl_p10_maxapiscore_int)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_p10_ReportingAuthority.Text = "0";
            }
        }




        lbl_e1_APIScoreThroughSelfAssessment_Total.Text = "0";
        lbl_e1_FinalObtainedValue_Total.Text = "0";
        lbl_CriteriaA_AcademicPerformance_Total_Total.Text = "0";


        int lbl_a1_apiscore_through_self_assessment_int_ = Convert.ToInt32(lbl_a1_apiscore_through_self_assessment.Text.Trim());
        int lbl_b1_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_b1_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_b2_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_b2_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_b3_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_b3_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_b4_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_b4_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_b5_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_b5_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_b6_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_b6_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_c1_TotalAPIScore_int_ = Convert.ToInt32(lbl_c1_TotalAPIScore.Text.Trim());
        int lbl_c2_TotalAPIScore_int_ = Convert.ToInt32(lbl_c2_TotalAPIScore.Text.Trim());
        int lbl_d1_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_d1_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_d2_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_d2_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_d3_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_d3_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_d4_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_d4_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_d5_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_d5_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_d6_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_d6_APIScoreThroughSelfAssessment.Text.Trim());
        int lbl_e1_APIScoreThroughSelfAssessment_int_ = Convert.ToInt32(lbl_e1_APIScoreThroughSelfAssessment.Text.Trim());

        int lbl_e1_APIScoreThroughSelfAssessment_Total_int =
            lbl_a1_apiscore_through_self_assessment_int_ +
            lbl_b2_APIScoreThroughSelfAssessment_int_ +
            lbl_b3_APIScoreThroughSelfAssessment_int_ +
            lbl_b4_APIScoreThroughSelfAssessment_int_ +
            lbl_b5_APIScoreThroughSelfAssessment_int_ +
            lbl_b6_APIScoreThroughSelfAssessment_int_ +
            lbl_c1_TotalAPIScore_int_ +
            lbl_c2_TotalAPIScore_int_ +
            lbl_d1_APIScoreThroughSelfAssessment_int_ +
            lbl_d2_APIScoreThroughSelfAssessment_int_ +
            lbl_d3_APIScoreThroughSelfAssessment_int_ +
            lbl_d4_APIScoreThroughSelfAssessment_int_ +
            lbl_d5_APIScoreThroughSelfAssessment_int_ +
            lbl_d1_APIScoreThroughSelfAssessment_int_ +
            lbl_d6_APIScoreThroughSelfAssessment_int_ +
            lbl_e1_APIScoreThroughSelfAssessment_int_;

        lbl_e1_APIScoreThroughSelfAssessment_Total.Text = lbl_e1_APIScoreThroughSelfAssessment_Total_int.ToString();


        int lbl_e1_FinalObtainedValue_Total_int = Convert.ToInt32(lbl_e1_FinalObtainedValue_Total.Text.Trim());
        int lbl_a1_final_obtained_value_int_ = Convert.ToInt32(lbl_a1_final_obtained_value.Text.Trim());
        int lbl_b1_FinalObtainedValue_int_ = Convert.ToInt32(lbl_b1_FinalObtainedValue.Text.Trim());
        int lbl_b2_FinalObtainedValue_int_ = Convert.ToInt32(lbl_b2_FinalObtainedValue.Text.Trim());
        int lbl_b3_FinalObtainedValue_int_ = Convert.ToInt32(lbl_b3_FinalObtainedValue.Text.Trim());
        int lbl_b4_FinalObtainedValue_int_ = Convert.ToInt32(lbl_b4_FinalObtainedValue.Text.Trim());
        int lbl_b5_FinalObtainedValue_int_ = Convert.ToInt32(lbl_b5_FinalObtainedValue.Text.Trim());
        int lbl_b6_FinalObtainedValue_int_ = Convert.ToInt32(lbl_b6_FinalObtainedValue.Text.Trim());
        int lbl_c1_FinalScore_int_ = Convert.ToInt32(lbl_c1_FinalScore.Text.Trim());
        int lbl_c2_FinalScore_int_ = Convert.ToInt32(lbl_c2_FinalScore.Text.Trim());
        int lbl_d1_FinalObtainedValue_int_ = Convert.ToInt32(lbl_d1_FinalObtainedValue.Text.Trim());
        int lbl_d2_FinalObtainedValue_int_ = Convert.ToInt32(lbl_d2_FinalObtainedValue.Text.Trim());
        int lbl_d3_FinalObtainedValue_int_ = Convert.ToInt32(lbl_d3_FinalObtainedValue.Text.Trim());
        int lbl_d4_FinalObtainedValue_int_ = Convert.ToInt32(lbl_d4_FinalObtainedValue.Text.Trim());
        int lbl_d5_FinalObtainedValue_int_ = Convert.ToInt32(lbl_d5_FinalObtainedValue.Text.Trim());
        int lbl_d6_FinalObtainedValue_int_ = Convert.ToInt32(lbl_d6_FinalObtainedValue.Text.Trim());
        int lbl_e1_FinalObtainedValue_int_ = Convert.ToInt32(lbl_e1_FinalObtainedValue.Text.Trim());

        int lbl_e1_FinalObtainedValue_Total_int_ = lbl_a1_final_obtained_value_int_ + lbl_b1_FinalObtainedValue_int_ + lbl_b2_FinalObtainedValue_int_ +
               lbl_b3_FinalObtainedValue_int_ + lbl_b4_FinalObtainedValue_int_ + lbl_b5_FinalObtainedValue_int_ +
               lbl_b6_FinalObtainedValue_int_ + lbl_c1_FinalScore_int_ + lbl_c2_FinalScore_int_ +
               lbl_d1_FinalObtainedValue_int_ + lbl_d2_FinalObtainedValue_int_ + lbl_d3_FinalObtainedValue_int_ +
               lbl_d4_FinalObtainedValue_int_ + lbl_d5_FinalObtainedValue_int_ + lbl_d6_FinalObtainedValue_int_ +
               lbl_e1_FinalObtainedValue_int_;

        lbl_e1_FinalObtainedValue_Total.Text = lbl_e1_FinalObtainedValue_Total_int_.ToString();

        lbl_CriteriaA_AcademicPerformance_Total_Total.Text = "0";
        int txt_a1_reporting_authority_assessment_int_ = Convert.ToInt32(txt_a1_reporting_authority_assessment.Text.Trim());
        int txt_b1_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b1_ReportingAuthorityAssessment.Text.Trim());
        int txt_b2_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b2_ReportingAuthorityAssessment.Text.Trim());
        int txt_b3_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b3_ReportingAuthorityAssessment.Text.Trim());
        int txt_b4_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b4_ReportingAuthorityAssessment.Text.Trim());
        int txt_b5_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b5_ReportingAuthorityAssessment.Text.Trim());
        int txt_b6_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b6_ReportingAuthorityAssessment.Text.Trim());
        int txt_c1_AssessmentByAuthority_int_ = Convert.ToInt32(txt_c1_AssessmentByAuthority.Text.Trim());
        int txt_c2_AssessmentByAuthority_int_ = Convert.ToInt32(txt_c2_AssessmentByAuthority.Text.Trim());
        int txt_d1_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d1_ReportingAuthorityAssessment.Text.Trim());
        int txt_d2_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d2_ReportingAuthorityAssessment.Text.Trim());
        int txt_d3_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d3_ReportingAuthorityAssessment.Text.Trim());
        int txt_d4_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d4_ReportingAuthorityAssessment.Text.Trim());
        int txt_d5_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d5_ReportingAuthorityAssessment.Text.Trim());
        int txt_d6_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d6_ReportingAuthorityAssessment.Text.Trim());
        int txt_e1_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_e1_ReportingAuthorityAssessment.Text.Trim());

        int lbl_CriteriaA_AcademicPerformance_Total_Total_ = txt_a1_reporting_authority_assessment_int_ + txt_b1_ReportingAuthorityAssessment_int_ +
               txt_b2_ReportingAuthorityAssessment_int_ + txt_b3_ReportingAuthorityAssessment_int_ +
               txt_b4_ReportingAuthorityAssessment_int_ + txt_b5_ReportingAuthorityAssessment_int_ +
               txt_b6_ReportingAuthorityAssessment_int_ + txt_c1_AssessmentByAuthority_int_ +
               txt_c2_AssessmentByAuthority_int_ + txt_d1_ReportingAuthorityAssessment_int_ +
               txt_d2_ReportingAuthorityAssessment_int_ + txt_d3_ReportingAuthorityAssessment_int_ +
               txt_d4_ReportingAuthorityAssessment_int_ + txt_d5_ReportingAuthorityAssessment_int_ +
               txt_d6_ReportingAuthorityAssessment_int_ + txt_e1_ReportingAuthorityAssessment_int_;

        lbl_CriteriaA_AcademicPerformance_Total_Total.Text = lbl_CriteriaA_AcademicPerformance_Total_Total_.ToString();


        lbl_api_score_through_self_assessment_total.Text = "0";
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1.Text.Trim());
        //int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_2.Text.Trim());

        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2.Text.Trim());

        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3.Text.Trim());
        //int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2.Text.Trim());

        int lbl_api_score_through_self_assessment_total_int = lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_int_ +
                    //lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_2_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_int_ +
                    //lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_2_int_ +

                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1_int_ +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2_int_;

        // Assign the sum to the label
        lbl_api_score_through_self_assessment_total.Text = lbl_api_score_through_self_assessment_total_int.ToString();

        lbl_finally_obtained_score_total.Text = "0";
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1.Text.Trim());
        //int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1_2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1_2.Text.Trim());

        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2.Text.Trim());
        //int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3_2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3_2.Text.Trim());

        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2.Text.Trim());


        int lbl_finally_obtained_score_total_int =
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1_int_ +
    //lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1_2_int_ +

    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3_int_ +
    //lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3_2_int_ +

    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i1_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2_int_;

        lbl_finally_obtained_score_total.Text = lbl_finally_obtained_score_total_int.ToString();

        lbl_assessmentby_reportingauthority_total.Text = "0";
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2.Text.Trim());

        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2.Text.Trim());

        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text.Trim());

        int lbl_assessmentby_reportingauthority_total_int = txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2_int

            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2_int_

        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1_int_
        + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2_int_;

        lbl_assessmentby_reportingauthority_total.Text = lbl_assessmentby_reportingauthority_total_int.ToString();

        lbl_api_score_through_self_assessment_total_criteriac.Text = "0";
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3_int_ = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3.Text.Trim());

        int lbl_api_score_through_self_assessment_total_criteriac_int =
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2_int_ +
    lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3_int_;

        // Assigning the total value to the label
        lbl_api_score_through_self_assessment_total_criteriac.Text = lbl_api_score_through_self_assessment_total_criteriac_int.ToString();


        lbl_finally_obtained_score_total_criteriac.Text = "0";
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11.Text.Trim());

        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3.Text.Trim());

        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4.Text.Trim());

        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2.Text.Trim());
        int lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3.Text.Trim());

        // Sum all  values
        int lbl_finally_obtained_score_total_criteriac_int = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2_int +
                    lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3_int;



        // You can now use `total_sum` as needed, for example, to display it
        lbl_finally_obtained_score_total_criteriac.Text = lbl_finally_obtained_score_total_criteriac_int.ToString();


        lbl_assessmentby_reportingauthority_total_Criteriac.Text = "0";
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text.Trim());

        // Sum all l values
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text.Trim());

        // Sum all m values
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text.Trim());

        // Sum all n values
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text.Trim());
        int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text.Trim());

        int lbl_assessmentby_reportingauthority_total_Criteriac_int =
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2_int_ +
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3_int_;

        lbl_assessmentby_reportingauthority_total_Criteriac.Text = lbl_assessmentby_reportingauthority_total_Criteriac_int.ToString();


        lbl_api_score_through_self_assessment_total_criteriad.Text = "0";
        int lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1_int_ = Convert.ToInt32(lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1.Text.Trim());
        int lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2_int_ = Convert.ToInt32(lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2.Text.Trim());
        int lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3_int_ = Convert.ToInt32(lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3.Text.Trim());
        int lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4_int_ = Convert.ToInt32(lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4.Text.Trim());
        int lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5_int_ = Convert.ToInt32(lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5.Text.Trim());

        int lbl_api_score_through_self_assessment_total_criteriad_int = lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1_int_ +
    lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2_int_ +
    lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3_int_ +
    lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4_int_ +
    lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5_int_;
        lbl_api_score_through_self_assessment_total_criteriad.Text = lbl_api_score_through_self_assessment_total_criteriad_int.ToString();


        lbl_finally_obtained_score_total_criteriad.Text = "0";
        int lbl_CriteriaD_Administration_TotalorFaculty_o1_int_ = Convert.ToInt32(lbl_CriteriaD_Administration_TotalorFaculty_o1.Text.Trim());
        int lbl_CriteriaD_Administration_TotalorFaculty_o2_int_ = Convert.ToInt32(lbl_CriteriaD_Administration_TotalorFaculty_o2.Text.Trim());
        int lbl_CriteriaD_Administration_TotalorFaculty_o3_int_ = Convert.ToInt32(lbl_CriteriaD_Administration_TotalorFaculty_o3.Text.Trim());
        int lbl_CriteriaD_Administration_TotalorFaculty_o4_int_ = Convert.ToInt32(lbl_CriteriaD_Administration_TotalorFaculty_o4.Text.Trim());
        int lbl_CriteriaD_Administration_TotalorFaculty_o5_int_ = Convert.ToInt32(lbl_CriteriaD_Administration_TotalorFaculty_o5.Text.Trim());

        int lbl_finally_obtained_score_total_criteriad_int = lbl_CriteriaD_Administration_TotalorFaculty_o1_int_ +
            lbl_CriteriaD_Administration_TotalorFaculty_o2_int_ +
            lbl_CriteriaD_Administration_TotalorFaculty_o3_int_ +
            lbl_CriteriaD_Administration_TotalorFaculty_o4_int_ +
            lbl_CriteriaD_Administration_TotalorFaculty_o5_int_;

        lbl_finally_obtained_score_total_criteriad.Text = lbl_finally_obtained_score_total_criteriad_int.ToString();


        lbl_assessmentby_reportingauthority_total_Criteriad.Text = "0";
        int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1_int_ = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text.Trim());
        int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2_int_ = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text.Trim());
        int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3_int_ = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text.Trim());
        int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4_int_ = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text.Trim());
        int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5_int_ = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text.Trim());

        int lbl_assessmentby_reportingauthority_total_Criteriad_int_ = txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1_int_ +
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2_int_ +
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3_int_ +
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4_int_ +
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5_int_;

        lbl_assessmentby_reportingauthority_total_Criteriad.Text = lbl_assessmentby_reportingauthority_total_Criteriad_int_.ToString();

        lbl_p_ReportingAuthority_total.Text = "0";
        int txt_p1_ReportingAuthority_int_ = Convert.ToInt32(txt_p1_ReportingAuthority.Text.Trim());
        int txt_p2_ReportingAuthority_int_ = Convert.ToInt32(txt_p2_ReportingAuthority.Text.Trim());
        int txt_p3_ReportingAuthority_int_ = Convert.ToInt32(txt_p3_ReportingAuthority.Text.Trim());
        int txt_p4_ReportingAuthority_int_ = Convert.ToInt32(txt_p4_ReportingAuthority.Text.Trim());
        int txt_p5_ReportingAuthority_int_ = Convert.ToInt32(txt_p5_ReportingAuthority.Text.Trim());
        int txt_p6_ReportingAuthority_int_ = Convert.ToInt32(txt_p6_ReportingAuthority.Text.Trim());
        int txt_p7_ReportingAuthority_int_ = Convert.ToInt32(txt_p7_ReportingAuthority.Text.Trim());
        int txt_p8_ReportingAuthority_int_ = Convert.ToInt32(txt_p8_ReportingAuthority.Text.Trim());
        int txt_p9_ReportingAuthority_int_ = Convert.ToInt32(txt_p9_ReportingAuthority.Text.Trim());
        int txt_p10_ReportingAuthority_int_ = Convert.ToInt32(txt_p10_ReportingAuthority.Text.Trim());

        int lbl_p_ReportingAuthority_total_int = txt_p1_ReportingAuthority_int_ +
                                 txt_p2_ReportingAuthority_int_ +
                                 txt_p3_ReportingAuthority_int_ +
                                 txt_p4_ReportingAuthority_int_ +
                                 txt_p5_ReportingAuthority_int_ +
                                 txt_p6_ReportingAuthority_int_ +
                                 txt_p7_ReportingAuthority_int_ +
                                 txt_p8_ReportingAuthority_int_ +
                                 txt_p9_ReportingAuthority_int_ +
                                 txt_p10_ReportingAuthority_int_;
        lbl_p_ReportingAuthority_total.Text = lbl_p_ReportingAuthority_total_int.ToString();

        ////Logic for feedback table
        //if (lbl_facultyfeedback_inpercentage.Text == "")
        //{

        //}
        //else
        //{


        //    // Assume you have a TextBox where the user inputs the feedback percentage.
        //    double facultyFeedbackPercentage = Convert.ToDouble(lbl_facultyfeedback_inpercentage.Text.Trim());
        //    int facultyObtainedMarks = 0;

        //    // Determine marks based on feedback percentage
        //    if (facultyFeedbackPercentage >= 90)
        //    {
        //        facultyObtainedMarks = 50;
        //    }
        //    else if (facultyFeedbackPercentage >= 80 && facultyFeedbackPercentage < 90)
        //    {
        //        facultyObtainedMarks = 45;
        //    }
        //    else if (facultyFeedbackPercentage >= 70 && facultyFeedbackPercentage < 80)
        //    {
        //        facultyObtainedMarks = 40;
        //    }
        //    else if (facultyFeedbackPercentage >= 60 && facultyFeedbackPercentage < 70)
        //    {
        //        facultyObtainedMarks = 35;
        //    }
        //    else if (facultyFeedbackPercentage >= 50 && facultyFeedbackPercentage < 60)
        //    {
        //        facultyObtainedMarks = 30;
        //    }
        //    else if (facultyFeedbackPercentage >= 40 && facultyFeedbackPercentage < 50)
        //    {
        //        facultyObtainedMarks = 25;
        //    }
        //    else if (facultyFeedbackPercentage >= 30 && facultyFeedbackPercentage < 40)
        //    {
        //        facultyObtainedMarks = 20;
        //    }
        //    else if (facultyFeedbackPercentage >= 20 && facultyFeedbackPercentage < 30)
        //    {
        //        facultyObtainedMarks = 15;
        //    }
        //    else if (facultyFeedbackPercentage >= 10 && facultyFeedbackPercentage < 20)
        //    {
        //        facultyObtainedMarks = 10;
        //    }
        //    else if (facultyFeedbackPercentage >= 0 && facultyFeedbackPercentage < 10)
        //    {
        //        facultyObtainedMarks = 5;
        //    }

        //    // Update the marks label
        //    lbl_facultyobtained_total.Text = facultyObtainedMarks.ToString();

        //}

        if (lbl_CriteriaA_AcademicPerformance_Total_Total.Text == "")
        {

        }
        else
        {
            lbl_apiscorecalculation_criteriaA.Text = lbl_CriteriaA_AcademicPerformance_Total_Total.Text;
        }

        if (lbl_assessmentby_reportingauthority_total.Text == "")
        {

        }
        else
        {
            lbl_apiscorecalculation_criteriaB.Text = lbl_assessmentby_reportingauthority_total.Text;
        }
        if (lbl_assessmentby_reportingauthority_total_Criteriac.Text == "")
        {

        }
        else
        {
            lbl_apiscorecalculation_criteriaC.Text = lbl_assessmentby_reportingauthority_total_Criteriac.Text;
        }
        if (lbl_assessmentby_reportingauthority_total_Criteriad.Text == "")
        {

        }
        else
        {
            lbl_apiscorecalculation_criteriaD.Text = lbl_assessmentby_reportingauthority_total_Criteriad.Text;
        }
        if (lbl_p_ReportingAuthority_total.Text != "")
        {
            lbl_apiscorecalculation_criteriaE.Text = lbl_p_ReportingAuthority_total.Text;
        }

        if (lbl_facultyobtained_total.Text == "")
        {

        }
        else
        {
            lbl_apiscorecalculation_criteriaF.Text = lbl_facultyobtained_total.Text;
        }

        int lbl_totalAPIScore_int = 0;

        // Check and add lbl_apiscorecalculation_criteriaA
        if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaA.Text))
        {
            lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaA.Text);
        }

        // Check and add lbl_apiscorecalculation_criteriaB
        if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaB.Text))
        {
            lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaB.Text);
        }

        // Check and add lbl_apiscorecalculation_criteriaC
        if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaC.Text))
        {
            lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaC.Text);
        }

        // Check and add lbl_apiscorecalculation_criteriaD
        if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaD.Text))
        {
            lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaD.Text);
        }

        // Check and add lbl_apiscorecalculation_criteriaE
        if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaE.Text))
        {
            lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaE.Text);
        }

        // Check and add lbl_apiscorecalculation_criteriaF
        if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaF.Text))
        {
            lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaF.Text);
        }

        //Compare designation with Reporting Authority

        if (lbl_designation.Text.Trim().ToUpper() == "LECTURER" ||
 lbl_designation.Text.Trim().ToUpper() == "TUTOR UG" ||
 lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR" ||
 lbl_designation.Text.Trim().ToUpper() == "TUTOR PG" ||
 lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT" ||
 lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR" ||
 lbl_designation.Text.Trim().ToUpper() == "TRAINER" && lbl_CriteriaA_AcademicPerformance_Total_Total != null)
        {
            int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_100_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_100.Text.Trim());
            int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_100_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
            {
                txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
            }
            else
            {
                txt_hrdepartment_required_improvement_1.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR" && lbl_CriteriaA_AcademicPerformance_Total_Total != null)
        {
            int lbl_RequiredMinimumAPIScore_AssistantProfessor_120_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_120.Text.Trim());
            int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_AssistantProfessor_120_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
            {
                txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
            }
            else
            {
                txt_hrdepartment_required_improvement_1.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR".ToUpper() && lbl_CriteriaA_AcademicPerformance_Total_Total != null)
        {
            int lbl_RequiredMinimumAPIScore_AssociateProfessor_130_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssociateProfessor_130.Text.Trim());
            int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_AssociateProfessor_130_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
            {
                txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
            }
            else
            {
                txt_hrdepartment_required_improvement_1.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR" && lbl_CriteriaA_AcademicPerformance_Total_Total != null)
        {
            int lbl_RequiredMinimumAPIScore_Professor_140_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_140.Text.Trim());
            int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_Professor_140_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
            {
                txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
            }
            else
            {
                txt_hrdepartment_required_improvement_1.Text = "";
            }
        }
        if (lbl_designation.Text.Trim().ToUpper() == "LECTURER" ||
            lbl_designation.Text.Trim().ToUpper() == "TUTOR UG" ||
            lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR" ||
            lbl_designation.Text.Trim().ToUpper() == "TUTOR PG" ||
            lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT" ||
            lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR" ||
            (lbl_designation.Text.Trim().ToUpper() == "TRAINER" && lbl_assessmentby_reportingauthority_total != null))
        {
            int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_80_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_80.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_80_int > lbl_assessmentby_reportingauthority_total_int_)
            {
                txt_hrdepartment_required_improvement_2.Text = "Research & Development";
            }
            else
            {
                txt_hrdepartment_required_improvement_2.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR" && lbl_assessmentby_reportingauthority_total != null)
        {
            int lbl_RequiredMinimumAPIScore_AssistantProfessor_110_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_110.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_AssistantProfessor_110_int > lbl_assessmentby_reportingauthority_total_int_)
            {
                txt_hrdepartment_required_improvement_2.Text = "Research & Development";
            }
            else
            {
                txt_hrdepartment_required_improvement_2.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR" && lbl_assessmentby_reportingauthority_total != null)
        {
            int lbl_RequiredMinimumAPIScore_AssociateProfessor_140_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssociateProfessor_140.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_AssociateProfessor_140_int > lbl_assessmentby_reportingauthority_total_int_)
            {
                txt_hrdepartment_required_improvement_2.Text = "Research & Development";
            }
            else
            {
                txt_hrdepartment_required_improvement_2.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR" && lbl_assessmentby_reportingauthority_total != null)
        {
            int lbl_RequiredMinimumAPIScore_Professor_150_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_150.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_Professor_150_int > lbl_assessmentby_reportingauthority_total_int_)
            {
                txt_hrdepartment_required_improvement_2.Text = "Research & Development";
            }
            else
            {
                txt_hrdepartment_required_improvement_2.Text = "";
            }
        }
        if (lbl_designation.Text.Trim().ToUpper() == "LECTURER" ||
    lbl_designation.Text.Trim().ToUpper() == "TUTOR UG" ||
    lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR" ||
    lbl_designation.Text.Trim().ToUpper() == "TUTOR PG" ||
    lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT" ||
    lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR" ||
    (lbl_designation.Text.Trim().ToUpper() == "TRAINER" && lbl_assessmentby_reportingauthority_total_Criteriac != null))
        {
            int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_40_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_40.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_40_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
            {
                txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
            }
            else
            {
                txt_hrdepartment_required_improvement_3.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriac != null)
        {
            int lbl_RequiredMinimumAPIScore_AssistantProfessor_45_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_45.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_AssistantProfessor_45_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
            {
                txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
            }
            else
            {
                txt_hrdepartment_required_improvement_3.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriac != null)
        {
            int lbl_RequiredMinimumAPIScore_AssistantProfessor_45_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssociateProfessor_50.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_AssistantProfessor_45_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
            {
                txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
            }
            else
            {
                txt_hrdepartment_required_improvement_3.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriac != null)
        {
            int lbl_RequiredMinimumAPIScore_Professor_60_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_60.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_Professor_60_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
            {
                txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
            }
            else
            {
                txt_hrdepartment_required_improvement_3.Text = "";
            }
        }
        if (lbl_designation.Text.Trim().ToUpper() == "LECTURER" ||
    lbl_designation.Text.Trim().ToUpper() == "TUTOR UG" ||
    lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR" ||
    lbl_designation.Text.Trim().ToUpper() == "TUTOR PG" ||
    lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT" ||
    lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR" ||
    (lbl_designation.Text.Trim().ToUpper() == "TRAINER" && lbl_assessmentby_reportingauthority_total_Criteriad != null))
        {
            int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_25_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_25.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_25_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
            {
                txt_hrdepartment_required_improvement_4.Text = "Administration";
            }
            else
            {
                txt_hrdepartment_required_improvement_4.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriad != null)
        {
            int lbl_RequiredMinimumAPIScore_AssistantProfessor_40_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_40.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_AssistantProfessor_40_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
            {
                txt_hrdepartment_required_improvement_4.Text = "Administration";
            }
            else
            {
                txt_hrdepartment_required_improvement_4.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriad != null)
        {
            int lbl_RequiredMinimumAPIScore_AssociateProfessor_60_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssociateProfessor_60.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_AssociateProfessor_60_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
            {
                txt_hrdepartment_required_improvement_4.Text = "Administration";
            }
            else
            {
                txt_hrdepartment_required_improvement_4.Text = "";
            }
        }

        if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriad != null)
        {
            int lbl_RequiredMinimumAPIScore_Professor_70_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_70.Text.Trim());
            int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text.Trim());

            if (lbl_RequiredMinimumAPIScore_Professor_70_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
            {
                txt_hrdepartment_required_improvement_4.Text = "Administration";
            }
            else
            {
                txt_hrdepartment_required_improvement_4.Text = "";
            }
        }


        // Display the total API score in a label
        lbl_totalAPIScore.Text = lbl_totalAPIScore_int.ToString();
        if (lbl_designation.Text.ToUpper() == "LECTURER".ToUpper() || lbl_designation.Text.ToUpper() == "Tutor UG".ToUpper()
            || lbl_designation.Text.ToUpper() == "Demonstrator".ToUpper() || lbl_designation.Text.ToUpper() == "Tutor PG".ToUpper()
            || lbl_designation.Text.ToUpper() == "Senior Resident".ToUpper() || lbl_designation.Text.ToUpper() == "Clinical Instructor".ToUpper()
            || lbl_designation.Text.ToUpper() == "Trainer".ToUpper())
        {
            int lbl_totalAPIScore_int_lecturer = Convert.ToInt32(lbl_totalAPIScore.Text.Trim());
            if (lbl_totalAPIScore_int_lecturer < 300)
            {
                lbl_facultyCategory.Text = "Average".ToUpper();
            }
            else if (lbl_totalAPIScore_int_lecturer >= 300 && lbl_totalAPIScore_int_lecturer <= 380)
            {
                lbl_facultyCategory.Text = "Good";
            }
            else if (lbl_totalAPIScore_int_lecturer > 380)
            {
                lbl_facultyCategory.Text = "Excellent";
            }
        }
        else if (lbl_designation.Text.ToUpper() == "ASSISTANT PROFESSOR".ToUpper())
        {
            int lbl_totalAPIScore_int_lecturer = Convert.ToInt32(lbl_totalAPIScore.Text.Trim());
            if (lbl_totalAPIScore_int_lecturer < 380)
            {
                lbl_facultyCategory.Text = "Average";
            }
            else if (lbl_totalAPIScore_int_lecturer >= 380 && lbl_totalAPIScore_int_lecturer <= 450)
            {
                lbl_facultyCategory.Text = "Good";
            }
            else if (lbl_totalAPIScore_int_lecturer >= 451)
            {
                lbl_facultyCategory.Text = "Excellent";
            }
        }
        else if (lbl_designation.Text.ToUpper() == "ASSOCIATE PROFESSOR".ToUpper())
        {
            int lbl_totalAPIScore_int_lecturer = Convert.ToInt32(lbl_totalAPIScore.Text.Trim());
            if (lbl_totalAPIScore_int_lecturer < 450)
            {
                lbl_facultyCategory.Text = "Average";
            }
            else if (lbl_totalAPIScore_int_lecturer >= 450 && lbl_totalAPIScore_int_lecturer <= 500)
            {
                lbl_facultyCategory.Text = "Good";
            }
            else if (lbl_totalAPIScore_int_lecturer >= 501)
            {
                lbl_facultyCategory.Text = "Excellent";
            }
        }
        else if (lbl_designation.Text.ToUpper() == "PROFESSOR".ToUpper())
        {
            int lbl_totalAPIScore_int_lecturer = Convert.ToInt32(lbl_totalAPIScore.Text.Trim());
            if (lbl_totalAPIScore_int_lecturer < 500)
            {
                lbl_facultyCategory.Text = "Average";
            }
            else if (lbl_totalAPIScore_int_lecturer >= 500 && lbl_totalAPIScore_int_lecturer <= 550)
            {
                lbl_facultyCategory.Text = "Good";
            }
            else if (lbl_totalAPIScore_int_lecturer >= 551)
            {
                lbl_facultyCategory.Text = "Excellent";
            }
        }

        // Set hrdepartment labels
        if (!string.IsNullOrEmpty(lbl_totalAPIScore.Text))
        {
            lbl_hrdepartment_totalapiscore.Text = lbl_totalAPIScore.Text;
        }
        if (!string.IsNullOrEmpty(lbl_facultyCategory.Text))
        {
            lbl_hrdepartment_faculty_categorybasedscore.Text = lbl_facultyCategory.Text;
        }

    }

    public void Calculation_For_HOD()
    {

        if (txt_a1_reporting_authority_assessment.Text.Trim() == "")
        {
            txt_a1_reporting_authority_assessment.Text = "0";
        }
        else
        {
            int a1_MaxAPIScore = Convert.ToInt32(lbl_a1_Maxapiscore.Text.Trim());
            int a1_AssessmentByAuthority = Convert.ToInt32(txt_a1_reporting_authority_assessment.Text.Trim());

            if (a1_AssessmentByAuthority <= a1_MaxAPIScore)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key",
                    "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);

                txt_a1_reporting_authority_assessment.Text = lbl_a1_final_obtained_value.Text;
            }
        }

        if (txt_b1_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_b1_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int b1_MaxAPIScore = Convert.ToInt32(lbl_b1_MaxAPIScore.Text.Trim());
            int b1_AssessmentByAuthority = Convert.ToInt32(txt_b1_ReportingAuthorityAssessment.Text.Trim());

            if (b1_AssessmentByAuthority <= b1_MaxAPIScore)
            {
                // Your additional logic here (if required)
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key",
                    "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);

                txt_b1_ReportingAuthorityAssessment.Text = lbl_b1_FinalObtainedValue.Text;
            }
        }
        // Validation for b2
        if (txt_b2_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_b2_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int b2_MaxAPIScore = Convert.ToInt32(lbl_b2_MaxAPIScore.Text.Trim());
            int b2_AssessmentByAuthority = Convert.ToInt32(txt_b2_ReportingAuthorityAssessment.Text.Trim());

            if (b2_AssessmentByAuthority <= b2_MaxAPIScore)
            {
                // Add your logic here if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "b2_Alert",
                    "alert('The entered value for b.2 cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_b2_ReportingAuthorityAssessment.Text = lbl_b2_FinalObtainedValue.Text;
            }
        }

        // Validation for b3
        if (txt_b3_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_b3_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int b3_MaxAPIScore = Convert.ToInt32(lbl_b3_MaxAPIScore.Text.Trim());
            int b3_AssessmentByAuthority = Convert.ToInt32(txt_b3_ReportingAuthorityAssessment.Text.Trim());

            if (b3_AssessmentByAuthority <= b3_MaxAPIScore)
            {
                // Add your logic here if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "b3_Alert",
                    "alert('The entered value for b.3 cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_b3_ReportingAuthorityAssessment.Text = lbl_b3_FinalObtainedValue.Text;
            }
        }

        // Validation for b4
        if (txt_b4_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_b4_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int b4_MaxAPIScore = Convert.ToInt32(lbl_b4_MaxAPIScore.Text.Trim());
            int b4_AssessmentByAuthority = Convert.ToInt32(txt_b4_ReportingAuthorityAssessment.Text.Trim());

            if (b4_AssessmentByAuthority <= b4_MaxAPIScore)
            {
                // Add your logic here if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "b4_Alert",
                    "alert('The entered value for b.4 cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_b4_ReportingAuthorityAssessment.Text = lbl_b4_FinalObtainedValue.Text;
            }
        }
        // Validation for b5
        if (txt_b5_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_b5_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int b5_MaxAPIScore = Convert.ToInt32(lbl_b5_MaxAPIScore.Text.Trim());
            int b5_AssessmentByAuthority = Convert.ToInt32(txt_b5_ReportingAuthorityAssessment.Text.Trim());

            if (b5_AssessmentByAuthority <= b5_MaxAPIScore)
            {
                // Add your logic here if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "b5_Alert",
                    "alert('The entered value for b.5 cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_b5_ReportingAuthorityAssessment.Text = lbl_b5_FinalObtainedValue.Text;
            }
        }

        // Validation for b6
        if (txt_b6_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_b6_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int b6_MaxAPIScore = Convert.ToInt32(lbl_b6_MaxAPIScore.Text.Trim());
            int b6_AssessmentByAuthority = Convert.ToInt32(txt_b6_ReportingAuthorityAssessment.Text.Trim());

            if (b6_AssessmentByAuthority <= b6_MaxAPIScore)
            {
                // Add your logic here if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "b6_Alert",
                    "alert('The entered value for b.6 cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_b6_ReportingAuthorityAssessment.Text = lbl_b6_FinalObtainedValue.Text;
            }
        }



        if (txt_c1_AssessmentByAuthority.Text.Trim() == "")
        {
            txt_c1_AssessmentByAuthority.Text = "0";

        }
        else
        {
            int c1_MaxAPIScore = Convert.ToInt32(lbl_c1_MaxAPIScore.Text.Trim());
            int c1_AssessmentByAuthority = Convert.ToInt32(txt_c1_AssessmentByAuthority.Text.Trim());
            if (c1_AssessmentByAuthority <= c1_MaxAPIScore)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_c1_AssessmentByAuthority.Text = lbl_c1_FinalScore.Text;
            }
        }
        if (txt_c2_AssessmentByAuthority.Text.Trim() == "")
        {
            txt_c2_AssessmentByAuthority.Text = "0";
        }
        else
        {
            int c2_MaxAPIScore = Convert.ToInt32(lbl_c2_MaxAPIScore.Text.Trim());
            int c2_AssessmentByAuthority = Convert.ToInt32(txt_c2_AssessmentByAuthority.Text.Trim());
            if (c2_AssessmentByAuthority <= c2_MaxAPIScore)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_c2_AssessmentByAuthority.Text = lbl_c2_FinalScore.Text;
            }
        }

        if (txt_d1_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_d1_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int d1_MaxAPIScore = Convert.ToInt32(lbl_d1_MaxAPIScore.Text.Trim());
            int d1_ReportingAuthorityAssessment = Convert.ToInt32(txt_d1_ReportingAuthorityAssessment.Text.Trim());
            if (d1_ReportingAuthorityAssessment <= d1_MaxAPIScore)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d1_ReportingAuthorityAssessment.Text = lbl_d1_FinalObtainedValue.Text;
            }
        }

        if (txt_d2_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_d2_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int d2_MaxAPIScore = Convert.ToInt32(lbl_d2_MaxAPIScore.Text.Trim());
            int d2_ReportingAuthorityAssessment = Convert.ToInt32(txt_d2_ReportingAuthorityAssessment.Text.Trim());
            if (d2_ReportingAuthorityAssessment <= d2_MaxAPIScore)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d2_ReportingAuthorityAssessment.Text = lbl_d2_FinalObtainedValue.Text;
            }
        }
        //d2

        if (txt_d3_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_d3_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int d3_MaxAPIScore = Convert.ToInt32(lbl_d3_MaxAPIScore.Text.Trim());
            int d3_ActualTotalActivities = Convert.ToInt32(txt_d3_ReportingAuthorityAssessment.Text.Trim());
            if (d3_ActualTotalActivities <= d3_MaxAPIScore)
            {
                // Valid input handling can go here if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d3_ReportingAuthorityAssessment.Text = lbl_d3_FinalObtainedValue.Text;
            }
        }

        if (txt_d4_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_d4_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int d4_MaxAPIScore = Convert.ToInt32(lbl_d4_MaxAPIScore.Text.Trim());
            int d4_ActualTotalActivities = Convert.ToInt32(txt_d4_ReportingAuthorityAssessment.Text.Trim());
            if (d4_ActualTotalActivities <= d4_MaxAPIScore)
            {
                // Valid input handling can go here if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d4_ReportingAuthorityAssessment.Text = lbl_d4_FinalObtainedValue.Text;
            }
        }

        if (txt_d5_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_d5_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int d5_MaxAPIScore = Convert.ToInt32(lbl_d5_MaxAPIScore.Text.Trim());
            int d5_ActualTotalActivities = Convert.ToInt32(txt_d5_ReportingAuthorityAssessment.Text.Trim());
            if (d5_ActualTotalActivities <= d5_MaxAPIScore)
            {
                // Valid input handling can go here if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d5_ReportingAuthorityAssessment.Text = lbl_d5_FinalObtainedValue.Text;
            }
        }
        if (txt_d6_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_d6_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int d6_MaxAPIScore = Convert.ToInt32(lbl_d6_MaxAPIScore.Text.Trim());
            int d6_ReportingAuthorityAssessment = Convert.ToInt32(txt_d6_ReportingAuthorityAssessment.Text.Trim());
            if (d6_ReportingAuthorityAssessment <= d6_MaxAPIScore)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_d6_ReportingAuthorityAssessment.Text = lbl_d6_FinalObtainedValue.Text;
            }
        }

        if (txt_e1_ReportingAuthorityAssessment.Text.Trim() == "")
        {
            txt_e1_ReportingAuthorityAssessment.Text = "0";
        }
        else
        {
            int e1_MaxAPIScore = Convert.ToInt32(lbl_e1_MaxAPIScore.Text.Trim());
            int e1_ReportingAuthorityAssessment = Convert.ToInt32(txt_e1_ReportingAuthorityAssessment.Text.Trim());
            if (e1_ReportingAuthorityAssessment <= e1_MaxAPIScore)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_e1_ReportingAuthorityAssessment.Text = lbl_e1_FinalObtainedValue.Text;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f1_int)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1.Text;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f2.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f2_int)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2.Text;
            }
        }

        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f3.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f3_int)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3.Text;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f4.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f4_int)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4.Text;
            }
        }
        // For f.5
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f5.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f5_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5.Text;
            }
        }

        // For f.6
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f6_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f6.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f6_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6.Text;
            }
        }

        // For f.7
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f7_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f7.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_f7_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7.Text;
            }
        }
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text.Trim());

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g1_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1.Text;
            }
        }
        // For g2
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text = "0";
        }
        else
        {
            int CriteriaB_ResearchAndDevelopment_MaxAPIScore_g2 = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g2.Text.Trim());
            int CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2 = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text.Trim());
            if (CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2 <= CriteriaB_ResearchAndDevelopment_MaxAPIScore_g2)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2.Text;
            }
        }
        // For g3
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text = "0";
        }
        else
        {
            int CriteriaB_ResearchAndDevelopment_MaxAPIScore_g3 = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_g3.Text.Trim());
            int CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3 = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text.Trim());
            if (CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3 <= CriteriaB_ResearchAndDevelopment_MaxAPIScore_g3)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3.Text;
            }
        }
        // For h1
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h1_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1.Text;
            }
        }
        // For h2
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h2.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h2_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2.Text;
            }
        }
        // For h3
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h3.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h3_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3.Text;
            }
        }
        // For h4
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h4.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h4_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4.Text;
            }
        }
        // For h5
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h5.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h5_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5.Text;
            }
        }
        // For h6
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h6_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h6.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h6_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6.Text;
            }
        }
        // For h7
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h7_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h7.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h7_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7.Text;
            }
        }
        // For h8
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8.Text;
            }
        }
        // For h8
        if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text.Trim() == "")
        {
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text = "0";
        }
        else
        {
            int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text.Trim());
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h8_int)
            {
                // Additional logic if needed
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8.Text;
            }
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h9_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h9.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_h9_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9.Text;
                }
            }


            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i1.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i1_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i1.Text;
                }
            }
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i2.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i2_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2.Text;
                }
            }
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i3.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i3_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3.Text;
                }
            }
            // For i4
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i4.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i4_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4.Text;
                }
            }
            // For i5
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i5.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_i5_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5.Text;
                }
            }
            // For j1
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j1.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j1_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1.Text;
                }
            }
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j2.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_j2_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2.Text;
                }
            }

            // For k1
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k1.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k1_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1.Text;
                }
            }

            // For k2
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k2.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k2_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2.Text;
                }
            }

            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k3.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k3_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3.Text;
                }
            }


            // For k4
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k4.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k4_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4.Text;
                }
            }

            // For k5
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k5_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k5.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k5_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5.Text;
                }
            }

            // For k6
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k6_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k6.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k6_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6.Text;
                }
            }

            // For k7
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k7_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k7.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k7_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7.Text;
                }
            }

            // For k8
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k8_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k8.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k8_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8.Text;
                }
            }

            // For k9
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k9_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k9.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k9_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9.Text;
                }
            }

            // For k10
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k10_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k10.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k10_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10.Text;
                }
            }

            // For k11
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k11_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k11.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_k11_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11.Text;
                }
            }
            // For l1
            // For l1
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l1.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l1_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1.Text;
                }
            }

            // For l2
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l2.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l2_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2.Text;
                }
            }

            // For l3
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l3.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_l3_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3.Text;
                }
            }

            // For m1
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m1.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m1_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1.Text;
                }
            }

            // For m2
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m2.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m2_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2.Text;
                }
            }

            // For m3
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m3.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m3_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3.Text;
                }
            }

            // For m4
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m4_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m4.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_m4_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4.Text;
                }
            }
            // For n1
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n1_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n1.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n1_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1.Text;
                }
            }

            // For n2
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n2_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n2.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n2_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2.Text;
                }
            }

            // For n3
            if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text.Trim() == "")
            {
                txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text = "0";
            }
            else
            {
                int lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n3_int = Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n3.Text.Trim());
                int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text.Trim());
                if (txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3_int <= lbl_CriteriaB_ResearchAndDevelopment_MaxAPIScore_n3_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text = lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3.Text;
                }
            }

            // For o1
            if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text.Trim() == "")
            {
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text = "0";
            }
            else
            {
                int lbl_CriteriaD_Administration_MaxAPIScore_o1_int = Convert.ToInt32(lbl_CriteriaD_Administration_MaxAPIScore_o1.Text.Trim());
                int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1_int = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text.Trim());
                if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1_int <= lbl_CriteriaD_Administration_MaxAPIScore_o1_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text = lbl_CriteriaD_Administration_TotalorFaculty_o1.Text;
                }
            }
            // For o2
            if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text.Trim() == "")
            {
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text = "0";
            }
            else
            {
                int lbl_CriteriaD_Administration_MaxAPIScore_o2_int = Convert.ToInt32(lbl_CriteriaD_Administration_MaxAPIScore_o2.Text.Trim());
                int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2_int = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text.Trim());
                if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2_int <= lbl_CriteriaD_Administration_MaxAPIScore_o2_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text = lbl_CriteriaD_Administration_TotalorFaculty_o2.Text;
                }
            }
            // For o3
            if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text.Trim() == "")
            {
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text = "0";
            }
            else
            {
                int lbl_CriteriaD_Administration_MaxAPIScore_o3_int = Convert.ToInt32(lbl_CriteriaD_Administration_MaxAPIScore_o3.Text.Trim());
                int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3_int = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text.Trim());
                if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3_int <= lbl_CriteriaD_Administration_MaxAPIScore_o3_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text = lbl_CriteriaD_Administration_TotalorFaculty_o3.Text;
                }
            }
            // For o4
            if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text.Trim() == "")
            {
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text = "0";
            }
            else
            {
                int lbl_CriteriaD_Administration_MaxAPIScore_o4_int = Convert.ToInt32(lbl_CriteriaD_Administration_MaxAPIScore_o4.Text.Trim());
                int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4_int = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text.Trim());
                if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4_int <= lbl_CriteriaD_Administration_MaxAPIScore_o4_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text = lbl_CriteriaD_Administration_TotalorFaculty_o4.Text;
                }
            }
            // For o5
            if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text.Trim() == "")
            {
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text = "0";
            }
            else
            {
                int lbl_CriteriaD_Administration_MaxAPIScore_o5_int = Convert.ToInt32(lbl_CriteriaD_Administration_MaxAPIScore_o5.Text.Trim());
                int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5_int = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text.Trim());
                if (txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5_int <= lbl_CriteriaD_Administration_MaxAPIScore_o5_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text = lbl_CriteriaD_Administration_TotalorFaculty_o5.Text;
                }
            }

            if (txt_p1_ReportingAuthority.Text.Trim() == "")
            {
                txt_p1_ReportingAuthority.Text = "0";
            }
            else
            {
                int lbl_p1_maxapiscore_int = Convert.ToInt32(lbl_p1_maxapiscore.Text.Trim());
                int txt_p1_ReportingAuthority_int = Convert.ToInt32(txt_p1_ReportingAuthority.Text.Trim());
                if (txt_p1_ReportingAuthority_int <= lbl_p1_maxapiscore_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_p1_ReportingAuthority.Text = "0";
                }
            }

            // For p2
            if (txt_p2_ReportingAuthority.Text.Trim() == "")
            {
                txt_p2_ReportingAuthority.Text = "0";
            }
            else
            {
                int lbl_p2_maxapiscore_int = Convert.ToInt32(lbl_p2_maxapiscore.Text.Trim());
                int txt_p2_ReportingAuthority_int = Convert.ToInt32(txt_p2_ReportingAuthority.Text.Trim());
                if (txt_p2_ReportingAuthority_int <= lbl_p2_maxapiscore_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_p2_ReportingAuthority.Text = "0";
                }
            }

            // For p3
            if (txt_p3_ReportingAuthority.Text.Trim() == "")
            {
                txt_p3_ReportingAuthority.Text = "0";
            }
            else
            {
                int lbl_p3_maxapiscore_int = Convert.ToInt32(lbl_p3_maxapiscore.Text.Trim());
                int txt_p3_ReportingAuthority_int = Convert.ToInt32(txt_p3_ReportingAuthority.Text.Trim());
                if (txt_p3_ReportingAuthority_int <= lbl_p3_maxapiscore_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_p3_ReportingAuthority.Text = "0";
                }
            }

            // For p4
            if (txt_p4_ReportingAuthority.Text.Trim() == "")
            {
                txt_p4_ReportingAuthority.Text = "0";
            }
            else
            {
                int lbl_p4_maxapiscore_int = Convert.ToInt32(lbl_p4_maxapiscore.Text.Trim());
                int txt_p4_ReportingAuthority_int = Convert.ToInt32(txt_p4_ReportingAuthority.Text.Trim());
                if (txt_p4_ReportingAuthority_int <= lbl_p4_maxapiscore_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_p4_ReportingAuthority.Text = "0";
                }
            }

            // For p5
            if (txt_p5_ReportingAuthority.Text.Trim() == "")
            {
                txt_p5_ReportingAuthority.Text = "0";
            }
            else
            {
                int lbl_p5_maxapiscore_int = Convert.ToInt32(lbl_p5_maxapiscore.Text.Trim());
                int txt_p5_ReportingAuthority_int = Convert.ToInt32(txt_p5_ReportingAuthority.Text.Trim());
                if (txt_p5_ReportingAuthority_int <= lbl_p5_maxapiscore_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_p5_ReportingAuthority.Text = "0";
                }
            }

            // For p6
            if (txt_p6_ReportingAuthority.Text.Trim() == "")
            {
                txt_p6_ReportingAuthority.Text = "0";
            }
            else
            {
                int lbl_p6_maxapiscore_int = Convert.ToInt32(lbl_p6_maxapiscore.Text.Trim());
                int txt_p6_ReportingAuthority_int = Convert.ToInt32(txt_p6_ReportingAuthority.Text.Trim());
                if (txt_p6_ReportingAuthority_int <= lbl_p6_maxapiscore_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_p6_ReportingAuthority.Text = "0";
                }
            }

            // For p7
            if (txt_p7_ReportingAuthority.Text.Trim() == "")
            {
                txt_p7_ReportingAuthority.Text = "0";
            }
            else
            {
                int lbl_p7_maxapiscore_int = Convert.ToInt32(lbl_p7_maxapiscore.Text.Trim());
                int txt_p7_ReportingAuthority_int = Convert.ToInt32(txt_p7_ReportingAuthority.Text.Trim());
                if (txt_p7_ReportingAuthority_int <= lbl_p7_maxapiscore_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_p7_ReportingAuthority.Text = "0";
                }
            }

            // For p8
            if (txt_p8_ReportingAuthority.Text.Trim() == "")
            {
                txt_p8_ReportingAuthority.Text = "0";
            }
            else
            {
                int lbl_p8_maxapiscore_int = Convert.ToInt32(lbl_p8_maxapiscore.Text.Trim());
                int txt_p8_ReportingAuthority_int = Convert.ToInt32(txt_p8_ReportingAuthority.Text.Trim());
                if (txt_p8_ReportingAuthority_int <= lbl_p8_maxapiscore_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_p8_ReportingAuthority.Text = "0";
                }
            }

            // For p9
            if (txt_p9_ReportingAuthority.Text.Trim() == "")
            {
                txt_p9_ReportingAuthority.Text = "0";
            }
            else
            {
                int lbl_p9_maxapiscore_int = Convert.ToInt32(lbl_p9_maxapiscore.Text.Trim());
                int txt_p9_ReportingAuthority_int = Convert.ToInt32(txt_p9_ReportingAuthority.Text.Trim());
                if (txt_p9_ReportingAuthority_int <= lbl_p9_maxapiscore_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_p9_ReportingAuthority.Text = "0";
                }
            }

            // For p10
            if (txt_p10_ReportingAuthority.Text.Trim() == "")
            {
                txt_p10_ReportingAuthority.Text = "0";
            }
            else
            {
                int lbl_p10_maxapiscore_int = Convert.ToInt32(lbl_p10_maxapiscore.Text.Trim());
                int txt_p10_ReportingAuthority_int = Convert.ToInt32(txt_p10_ReportingAuthority.Text.Trim());
                if (txt_p10_ReportingAuthority_int <= lbl_p10_maxapiscore_int)
                {
                    // Additional logic if needed
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The entered value cannot be greater than the maximum API score. Please enter a valid value.');", true);
                    txt_p10_ReportingAuthority.Text = "0";
                }
            }




            lbl_CriteriaA_AcademicPerformance_Total_Total.Text = "0";
            int txt_a1_reporting_authority_assessment_int_ = Convert.ToInt32(txt_a1_reporting_authority_assessment.Text.Trim());
            int txt_b1_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b1_ReportingAuthorityAssessment.Text.Trim());
            int txt_b2_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b2_ReportingAuthorityAssessment.Text.Trim());
            int txt_b3_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b3_ReportingAuthorityAssessment.Text.Trim());
            int txt_b4_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b4_ReportingAuthorityAssessment.Text.Trim());
            int txt_b5_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b5_ReportingAuthorityAssessment.Text.Trim());
            int txt_b6_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_b6_ReportingAuthorityAssessment.Text.Trim());
            int txt_c1_AssessmentByAuthority_int_ = Convert.ToInt32(txt_c1_AssessmentByAuthority.Text.Trim());
            int txt_c2_AssessmentByAuthority_int_ = Convert.ToInt32(txt_c2_AssessmentByAuthority.Text.Trim());
            int txt_d1_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d1_ReportingAuthorityAssessment.Text.Trim());
            int txt_d2_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d2_ReportingAuthorityAssessment.Text.Trim());
            int txt_d3_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d3_ReportingAuthorityAssessment.Text.Trim());
            int txt_d4_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d4_ReportingAuthorityAssessment.Text.Trim());
            int txt_d5_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d5_ReportingAuthorityAssessment.Text.Trim());
            int txt_d6_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_d6_ReportingAuthorityAssessment.Text.Trim());
            int txt_e1_ReportingAuthorityAssessment_int_ = Convert.ToInt32(txt_e1_ReportingAuthorityAssessment.Text.Trim());

            int lbl_CriteriaA_AcademicPerformance_Total_Total_ = txt_a1_reporting_authority_assessment_int_ + txt_b1_ReportingAuthorityAssessment_int_ +
                   txt_b2_ReportingAuthorityAssessment_int_ + txt_b3_ReportingAuthorityAssessment_int_ +
                   txt_b4_ReportingAuthorityAssessment_int_ + txt_b5_ReportingAuthorityAssessment_int_ +
                   txt_b6_ReportingAuthorityAssessment_int_ + txt_c1_AssessmentByAuthority_int_ +
                   txt_c2_AssessmentByAuthority_int_ + txt_d1_ReportingAuthorityAssessment_int_ +
                   txt_d2_ReportingAuthorityAssessment_int_ + txt_d3_ReportingAuthorityAssessment_int_ +
                   txt_d4_ReportingAuthorityAssessment_int_ + txt_d5_ReportingAuthorityAssessment_int_ +
                   txt_d6_ReportingAuthorityAssessment_int_ + txt_e1_ReportingAuthorityAssessment_int_;

            lbl_CriteriaA_AcademicPerformance_Total_Total.Text = lbl_CriteriaA_AcademicPerformance_Total_Total_.ToString();

            lbl_assessmentby_reportingauthority_total.Text = "0";
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2_int = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2.Text.Trim());

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2.Text.Trim());

            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text.Trim());

            int lbl_assessmentby_reportingauthority_total_int = txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2_int

                + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2_int_

            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1_int_
            + txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2_int_;

            lbl_assessmentby_reportingauthority_total.Text = lbl_assessmentby_reportingauthority_total_int.ToString();

            lbl_assessmentby_reportingauthority_total_Criteriac.Text = "0";
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text.Trim());

            // Sum all l values
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text.Trim());

            // Sum all m values
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text.Trim());

            // Sum all n values
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text.Trim());
            int txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3_int_ = Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text.Trim());

            int lbl_assessmentby_reportingauthority_total_Criteriac_int =
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2_int_ +
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3_int_;

            lbl_assessmentby_reportingauthority_total_Criteriac.Text = lbl_assessmentby_reportingauthority_total_Criteriac_int.ToString();

            lbl_assessmentby_reportingauthority_total_Criteriad.Text = "0";
            int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1_int_ = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text.Trim());
            int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2_int_ = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text.Trim());
            int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3_int_ = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text.Trim());
            int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4_int_ = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text.Trim());
            int txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5_int_ = Convert.ToInt32(txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text.Trim());

            int lbl_assessmentby_reportingauthority_total_Criteriad_int_ = txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1_int_ +
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2_int_ +
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3_int_ +
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4_int_ +
                txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5_int_;


            lbl_assessmentby_reportingauthority_total_Criteriad.Text = lbl_assessmentby_reportingauthority_total_Criteriad_int_.ToString();


            lbl_apiscorecalculation_criteriaA.Text = lbl_CriteriaA_AcademicPerformance_Total_Total.Text.Trim();
            lbl_apiscorecalculation_criteriaB.Text = lbl_assessmentby_reportingauthority_total.Text.Trim();
            lbl_apiscorecalculation_criteriaC.Text = lbl_assessmentby_reportingauthority_total_Criteriac.Text;
            lbl_apiscorecalculation_criteriaD.Text = lbl_assessmentby_reportingauthority_total_Criteriad.Text;



            int txt_p1_ReportingAuthority_int_ = Convert.ToInt32(txt_p1_ReportingAuthority.Text.Trim());
            int txt_p2_ReportingAuthority_int_ = Convert.ToInt32(txt_p2_ReportingAuthority.Text.Trim());
            int txt_p3_ReportingAuthority_int_ = Convert.ToInt32(txt_p3_ReportingAuthority.Text.Trim());
            int txt_p4_ReportingAuthority_int_ = Convert.ToInt32(txt_p4_ReportingAuthority.Text.Trim());
            int txt_p5_ReportingAuthority_int_ = Convert.ToInt32(txt_p5_ReportingAuthority.Text.Trim());
            int txt_p6_ReportingAuthority_int_ = Convert.ToInt32(txt_p6_ReportingAuthority.Text.Trim());
            int txt_p7_ReportingAuthority_int_ = Convert.ToInt32(txt_p7_ReportingAuthority.Text.Trim());
            int txt_p8_ReportingAuthority_int_ = Convert.ToInt32(txt_p8_ReportingAuthority.Text.Trim());
            int txt_p9_ReportingAuthority_int_ = Convert.ToInt32(txt_p9_ReportingAuthority.Text.Trim());
            int txt_p10_ReportingAuthority_int_ = Convert.ToInt32(txt_p10_ReportingAuthority.Text.Trim());

            int lbl_p_ReportingAuthority_total_int = txt_p1_ReportingAuthority_int_ +
                                     txt_p2_ReportingAuthority_int_ +
                                     txt_p3_ReportingAuthority_int_ +
                                     txt_p4_ReportingAuthority_int_ +
                                     txt_p5_ReportingAuthority_int_ +
                                     txt_p6_ReportingAuthority_int_ +
                                     txt_p7_ReportingAuthority_int_ +
                                     txt_p8_ReportingAuthority_int_ +
                                     txt_p9_ReportingAuthority_int_ +
                                     txt_p10_ReportingAuthority_int_;
            lbl_p_ReportingAuthority_total.Text = lbl_p_ReportingAuthority_total_int.ToString();



            lbl_apiscorecalculation_criteriaE.Text = lbl_p_ReportingAuthority_total.Text.Trim();


            //lbl_p_ReportingAuthority_total.Text = "0";

            int lbl_totalAPIScore_int = 0;

            // Check and add lbl_apiscorecalculation_criteriaA
            if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaA.Text))
            {
                lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaA.Text);
            }

            // Check and add lbl_apiscorecalculation_criteriaB
            if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaB.Text))
            {
                lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaB.Text);
            }

            // Check and add lbl_apiscorecalculation_criteriaC
            if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaC.Text))
            {
                lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaC.Text);
            }

            // Check and add lbl_apiscorecalculation_criteriaD
            if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaD.Text))
            {
                lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaD.Text);
            }

            // Check and add lbl_apiscorecalculation_criteriaE
            if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaE.Text))
            {
                lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaE.Text);
            }

            // Check and add lbl_apiscorecalculation_criteriaF
            if (!string.IsNullOrEmpty(lbl_apiscorecalculation_criteriaF.Text))
            {
                lbl_totalAPIScore_int += Convert.ToInt32(lbl_apiscorecalculation_criteriaF.Text);
            }

            lbl_totalAPIScore.Text = lbl_totalAPIScore_int.ToString();
            //Compare designation with Reporting Authority
            lbl_hrdepartment_totalapiscore.Text = lbl_totalAPIScore_int.ToString();
            if (lbl_designation.Text.Trim().ToUpper() == "LECTURER" ||
     lbl_designation.Text.Trim().ToUpper() == "TUTOR UG" ||
     lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR" ||
     lbl_designation.Text.Trim().ToUpper() == "TUTOR PG" ||
     lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT" ||
     lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR" ||
     lbl_designation.Text.Trim().ToUpper() == "TRAINER" && lbl_CriteriaA_AcademicPerformance_Total_Total != null)
            {
                int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_100_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_100.Text.Trim());
                int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_100_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
                {
                    txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
                }
                else
                {
                    txt_hrdepartment_required_improvement_1.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR" && lbl_CriteriaA_AcademicPerformance_Total_Total != null)
            {
                int lbl_RequiredMinimumAPIScore_AssistantProfessor_120_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_120.Text.Trim());
                int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_AssistantProfessor_120_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
                {
                    txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
                }
                else
                {
                    txt_hrdepartment_required_improvement_1.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR".ToUpper() && lbl_CriteriaA_AcademicPerformance_Total_Total != null)
            {
                int lbl_RequiredMinimumAPIScore_AssociateProfessor_130_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssociateProfessor_130.Text.Trim());
                int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_AssociateProfessor_130_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
                {
                    txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
                }
                else
                {
                    txt_hrdepartment_required_improvement_1.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR" && lbl_CriteriaA_AcademicPerformance_Total_Total != null)
            {
                int lbl_RequiredMinimumAPIScore_Professor_140_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_140.Text.Trim());
                int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_Professor_140_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
                {
                    txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
                }
                else
                {
                    txt_hrdepartment_required_improvement_1.Text = "";
                }
            }
            if (lbl_designation.Text.Trim().ToUpper() == "LECTURER" ||
                lbl_designation.Text.Trim().ToUpper() == "TUTOR UG" ||
                lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR" ||
                lbl_designation.Text.Trim().ToUpper() == "TUTOR PG" ||
                lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT" ||
                lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR" ||
                (lbl_designation.Text.Trim().ToUpper() == "TRAINER" && lbl_assessmentby_reportingauthority_total != null))
            {
                int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_80_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_80.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_80_int > lbl_assessmentby_reportingauthority_total_int_)
                {
                    txt_hrdepartment_required_improvement_2.Text = "Research & Development";
                }
                else
                {
                    txt_hrdepartment_required_improvement_2.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR" && lbl_assessmentby_reportingauthority_total != null)
            {
                int lbl_RequiredMinimumAPIScore_AssistantProfessor_110_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_110.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_AssistantProfessor_110_int > lbl_assessmentby_reportingauthority_total_int_)
                {
                    txt_hrdepartment_required_improvement_2.Text = "Research & Development";
                }
                else
                {
                    txt_hrdepartment_required_improvement_2.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR" && lbl_assessmentby_reportingauthority_total != null)
            {
                int lbl_RequiredMinimumAPIScore_AssociateProfessor_140_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssociateProfessor_140.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_AssociateProfessor_140_int > lbl_assessmentby_reportingauthority_total_int_)
                {
                    txt_hrdepartment_required_improvement_2.Text = "Research & Development";
                }
                else
                {
                    txt_hrdepartment_required_improvement_2.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR" && lbl_assessmentby_reportingauthority_total != null)
            {
                int lbl_RequiredMinimumAPIScore_Professor_150_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_150.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_Professor_150_int > lbl_assessmentby_reportingauthority_total_int_)
                {
                    txt_hrdepartment_required_improvement_2.Text = "Research & Development";
                }
                else
                {
                    txt_hrdepartment_required_improvement_2.Text = "";
                }
            }
            if (lbl_designation.Text.Trim().ToUpper() == "LECTURER" ||
        lbl_designation.Text.Trim().ToUpper() == "TUTOR UG" ||
        lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR" ||
        lbl_designation.Text.Trim().ToUpper() == "TUTOR PG" ||
        lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT" ||
        lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR" ||
        (lbl_designation.Text.Trim().ToUpper() == "TRAINER" && lbl_assessmentby_reportingauthority_total_Criteriac != null))
            {
                int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_40_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_40.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_40_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
                {
                    txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
                }
                else
                {
                    txt_hrdepartment_required_improvement_3.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriac != null)
            {
                int lbl_RequiredMinimumAPIScore_AssistantProfessor_45_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_45.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_AssistantProfessor_45_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
                {
                    txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
                }
                else
                {
                    txt_hrdepartment_required_improvement_3.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriac != null)
            {
                int lbl_RequiredMinimumAPIScore_AssistantProfessor_45_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssociateProfessor_50.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_AssistantProfessor_45_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
                {
                    txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
                }
                else
                {
                    txt_hrdepartment_required_improvement_3.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriac != null)
            {
                int lbl_RequiredMinimumAPIScore_Professor_60_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_60.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_Professor_60_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
                {
                    txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
                }
                else
                {
                    txt_hrdepartment_required_improvement_3.Text = "";
                }
            }
            if (lbl_designation.Text.Trim().ToUpper() == "LECTURER" ||
        lbl_designation.Text.Trim().ToUpper() == "TUTOR UG" ||
        lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR" ||
        lbl_designation.Text.Trim().ToUpper() == "TUTOR PG" ||
        lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT" ||
        lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR" ||
        (lbl_designation.Text.Trim().ToUpper() == "TRAINER" && lbl_assessmentby_reportingauthority_total_Criteriad != null))
            {
                int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_25_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_25.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_25_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
                {
                    txt_hrdepartment_required_improvement_4.Text = "Administration";
                }
                else
                {
                    txt_hrdepartment_required_improvement_4.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriad != null)
            {
                int lbl_RequiredMinimumAPIScore_AssistantProfessor_40_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_40.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_AssistantProfessor_40_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
                {
                    txt_hrdepartment_required_improvement_4.Text = "Administration";
                }
                else
                {
                    txt_hrdepartment_required_improvement_4.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriad != null)
            {
                int lbl_RequiredMinimumAPIScore_AssociateProfessor_60_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssociateProfessor_60.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_AssociateProfessor_60_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
                {
                    txt_hrdepartment_required_improvement_4.Text = "Administration";
                }
                else
                {
                    txt_hrdepartment_required_improvement_4.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR" && lbl_assessmentby_reportingauthority_total_Criteriad != null)
            {
                int lbl_RequiredMinimumAPIScore_Professor_70_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_70.Text.Trim());
                int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text.Trim());

                if (lbl_RequiredMinimumAPIScore_Professor_70_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
                {
                    txt_hrdepartment_required_improvement_4.Text = "Administration";
                }
                else
                {
                    txt_hrdepartment_required_improvement_4.Text = "";
                }
            }


            // Display the total API score in a label
            lbl_totalAPIScore.Text = lbl_totalAPIScore_int.ToString();
            if (lbl_designation.Text.ToUpper() == "LECTURER".ToUpper() || lbl_designation.Text.ToUpper() == "Tutor UG".ToUpper()
                || lbl_designation.Text.ToUpper() == "Demonstrator".ToUpper() || lbl_designation.Text.ToUpper() == "Tutor PG".ToUpper()
                || lbl_designation.Text.ToUpper() == "Senior Resident".ToUpper() || lbl_designation.Text.ToUpper() == "Clinical Instructor".ToUpper()
                || lbl_designation.Text.ToUpper() == "Trainer".ToUpper())
            {
                int lbl_totalAPIScore_int_lecturer = Convert.ToInt32(lbl_totalAPIScore.Text.Trim());
                if (lbl_totalAPIScore_int_lecturer < 300)
                {
                    lbl_facultyCategory.Text = "Average".ToUpper();
                }
                else if (lbl_totalAPIScore_int_lecturer >= 300 && lbl_totalAPIScore_int_lecturer <= 380)
                {
                    lbl_facultyCategory.Text = "Good";
                }
                else if (lbl_totalAPIScore_int_lecturer > 380)
                {
                    lbl_facultyCategory.Text = "Excellent";
                }
            }
            else if (lbl_designation.Text.ToUpper() == "ASSISTANT PROFESSOR".ToUpper())
            {
                int lbl_totalAPIScore_int_lecturer = Convert.ToInt32(lbl_totalAPIScore.Text.Trim());
                if (lbl_totalAPIScore_int_lecturer < 380)
                {
                    lbl_facultyCategory.Text = "Average";
                }
                else if (lbl_totalAPIScore_int_lecturer >= 380 && lbl_totalAPIScore_int_lecturer <= 450)
                {
                    lbl_facultyCategory.Text = "Good";
                }
                else if (lbl_totalAPIScore_int_lecturer >= 451)
                {
                    lbl_facultyCategory.Text = "Excellent";
                }
            }
            else if (lbl_designation.Text.ToUpper() == "ASSOCIATE PROFESSOR".ToUpper())
            {
                int lbl_totalAPIScore_int_lecturer = Convert.ToInt32(lbl_totalAPIScore.Text.Trim());
                if (lbl_totalAPIScore_int_lecturer < 450)
                {
                    lbl_facultyCategory.Text = "Average";
                }
                else if (lbl_totalAPIScore_int_lecturer >= 450 && lbl_totalAPIScore_int_lecturer <= 500)
                {
                    lbl_facultyCategory.Text = "Good";
                }
                else if (lbl_totalAPIScore_int_lecturer >= 501)
                {
                    lbl_facultyCategory.Text = "Excellent";
                }
            }
            else if (lbl_designation.Text.ToUpper() == "PROFESSOR".ToUpper())
            {
                int lbl_totalAPIScore_int_lecturer = Convert.ToInt32(lbl_totalAPIScore.Text.Trim());
                if (lbl_totalAPIScore_int_lecturer < 500)
                {
                    lbl_facultyCategory.Text = "Average";
                }
                else if (lbl_totalAPIScore_int_lecturer >= 500 && lbl_totalAPIScore_int_lecturer <= 550)
                {
                    lbl_facultyCategory.Text = "Good";
                }
                else if (lbl_totalAPIScore_int_lecturer >= 551)
                {
                    lbl_facultyCategory.Text = "Excellent";
                }
            }

            // Set hrdepartment labels
            if (!string.IsNullOrEmpty(lbl_totalAPIScore.Text))
            {
                lbl_hrdepartment_totalapiscore.Text = lbl_totalAPIScore.Text;
            }
            if (!string.IsNullOrEmpty(lbl_facultyCategory.Text))
            {
                lbl_hrdepartment_faculty_categorybasedscore.Text = lbl_facultyCategory.Text;
            }



            //Compare designation with Reporting Authority

            if (lbl_designation.Text.Trim().ToUpper() == "LECTURER".ToUpper() ||
     lbl_designation.Text.Trim().ToUpper() == "TUTOR UG".ToUpper() ||
     lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR".ToUpper() ||
     lbl_designation.Text.Trim().ToUpper() == "TUTOR PG".ToUpper() ||
     lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT".ToUpper() ||
     lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR".ToUpper() ||
     (lbl_designation.Text.Trim().ToUpper() == "TRAINER".ToUpper() && lbl_CriteriaA_AcademicPerformance_Total_Total != null))
            {
                int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_100_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_100.Text);
                int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text);

                if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_100_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
                {
                    txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
                }
                else
                {
                    txt_hrdepartment_required_improvement_1.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR".ToUpper() && lbl_assessmentby_reportingauthority_total != null)
            {
                int lbl_RequiredMinimumAPIScore_AssistantProfessor_120_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_120.Text);
                int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text);

                if (lbl_RequiredMinimumAPIScore_AssistantProfessor_120_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
                {
                    txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
                }
                else
                {
                    txt_hrdepartment_required_improvement_1.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR".ToUpper() && lbl_CriteriaA_AcademicPerformance_Total_Total.Text != null)
            {
                int lbl_RequiredMinimumAPIScore_AssociateProfessor_130_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssociateProfessor_130.Text);
                int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text);

                if (lbl_RequiredMinimumAPIScore_AssociateProfessor_130_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
                {
                    txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
                }
                else
                {
                    txt_hrdepartment_required_improvement_1.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR".ToUpper() && lbl_assessmentby_reportingauthority_total != null)
            {
                int lbl_RequiredMinimumAPIScore_Professor_140_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_140.Text);
                int lbl_CriteriaA_AcademicPerformance_Total_Total_int = Convert.ToInt32(lbl_CriteriaA_AcademicPerformance_Total_Total.Text);

                if (lbl_RequiredMinimumAPIScore_Professor_140_int > lbl_CriteriaA_AcademicPerformance_Total_Total_int)
                {
                    txt_hrdepartment_required_improvement_1.Text = "Academic Performance";
                }
                else
                {
                    txt_hrdepartment_required_improvement_1.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "LECTURER".ToUpper() ||
                lbl_designation.Text.Trim().ToUpper() == "TUTOR UG".ToUpper() ||
                lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR".ToUpper() ||
                lbl_designation.Text.Trim().ToUpper() == "TUTOR PG".ToUpper() ||
                lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT".ToUpper() ||
                lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR".ToUpper() ||
                (lbl_designation.Text.Trim().ToUpper() == "TRAINER".ToUpper() && lbl_assessmentby_reportingauthority_total != null))
            {
                int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_80_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_80.Text);
                int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text);

                if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_80_int > lbl_assessmentby_reportingauthority_total_int_)
                {
                    txt_hrdepartment_required_improvement_2.Text = "Research & Development";
                }
                else
                {
                    txt_hrdepartment_required_improvement_2.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR".ToUpper() && lbl_assessmentby_reportingauthority_total != null)
            {
                int lbl_RequiredMinimumAPIScore_AssistantProfessor_110_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_110.Text);
                int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text);

                if (lbl_RequiredMinimumAPIScore_AssistantProfessor_110_int > lbl_assessmentby_reportingauthority_total_int_)
                {
                    txt_hrdepartment_required_improvement_2.Text = "Research & Development";
                }
                else
                {
                    txt_hrdepartment_required_improvement_2.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR".ToUpper() && lbl_assessmentby_reportingauthority_total != null)
            {
                int lbl_RequiredMinimumAPIScore_AssociateProfessor_140_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssociateProfessor_140.Text);
                int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text);

                if (lbl_RequiredMinimumAPIScore_AssociateProfessor_140_int > lbl_assessmentby_reportingauthority_total_int_)
                {
                    txt_hrdepartment_required_improvement_2.Text = "Research & Development";
                }
                else
                {
                    txt_hrdepartment_required_improvement_2.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR".ToUpper() && lbl_assessmentby_reportingauthority_total != null)
            {
                int lbl_RequiredMinimumAPIScore_Professor_150_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_150.Text);
                int lbl_assessmentby_reportingauthority_total_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total.Text);

                if (lbl_RequiredMinimumAPIScore_Professor_150_int > lbl_assessmentby_reportingauthority_total_int_)
                {
                    txt_hrdepartment_required_improvement_2.Text = "Research & Development";
                }
                else
                {
                    txt_hrdepartment_required_improvement_2.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "LECTURER".ToUpper() ||
      lbl_designation.Text.Trim().ToUpper() == "TUTOR UG".ToUpper() ||
      lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR".ToUpper() ||
      lbl_designation.Text.Trim().ToUpper() == "TUTOR PG".ToUpper() ||
      lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT".ToUpper() ||
      lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR".ToUpper() ||
      (lbl_designation.Text.Trim().ToUpper() == "TRAINER".ToUpper() && lbl_assessmentby_reportingauthority_total_Criteriac != null))
            {
                int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_40_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_40.Text);
                int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text);

                if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_40_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
                {
                    txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
                }
                else
                {
                    txt_hrdepartment_required_improvement_3.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR".ToUpper() && lbl_assessmentby_reportingauthority_total_Criteriac != null)
            {
                int lbl_RequiredMinimumAPIScore_AssistantProfessor_45_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_45.Text);
                int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text);

                if (lbl_RequiredMinimumAPIScore_AssistantProfessor_45_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
                {
                    txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
                }
                else
                {
                    txt_hrdepartment_required_improvement_3.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR".ToUpper() && lbl_assessmentby_reportingauthority_total_Criteriac != null)
            {
                int lbl_RequiredMinimumAPIScore_AssociateProfessor_45_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_45.Text);
                int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text);

                if (lbl_RequiredMinimumAPIScore_AssociateProfessor_45_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
                {
                    txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
                }
                else
                {
                    txt_hrdepartment_required_improvement_3.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR".ToUpper() && lbl_assessmentby_reportingauthority_total_Criteriac != null)
            {
                int lbl_RequiredMinimumAPIScore_Professor_60_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_60.Text);
                int lbl_assessmentby_reportingauthority_total_Criteriac_int_ = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriac.Text);

                if (lbl_RequiredMinimumAPIScore_Professor_60_int > lbl_assessmentby_reportingauthority_total_Criteriac_int_)
                {
                    txt_hrdepartment_required_improvement_3.Text = "Professional & Personal Competency";
                }
                else
                {
                    txt_hrdepartment_required_improvement_3.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "LECTURER".ToUpper() ||
                lbl_designation.Text.Trim().ToUpper() == "TUTOR UG".ToUpper() ||
                lbl_designation.Text.Trim().ToUpper() == "DEMONSTRATOR".ToUpper() ||
                lbl_designation.Text.Trim().ToUpper() == "TUTOR PG".ToUpper() ||
                lbl_designation.Text.Trim().ToUpper() == "SENIOR RESIDENT".ToUpper() ||
                lbl_designation.Text.Trim().ToUpper() == "CLINICAL INSTRUCTOR".ToUpper() ||
                (lbl_designation.Text.Trim().ToUpper() == "TRAINER".ToUpper() && lbl_assessmentby_reportingauthority_total_Criteriad != null))
            {
                int lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_25_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_25.Text);
                int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text);

                if (lbl_RequiredMinimumAPIScore_Demonstrator_Lecturer_25_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
                {
                    txt_hrdepartment_required_improvement_4.Text = "Administration";
                }
                else
                {
                    txt_hrdepartment_required_improvement_4.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSISTANT PROFESSOR".ToUpper() && lbl_assessmentby_reportingauthority_total_Criteriad != null)
            {
                int lbl_RequiredMinimumAPIScore_AssistantProfessor_40_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssistantProfessor_40.Text);
                int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text);

                if (lbl_RequiredMinimumAPIScore_AssistantProfessor_40_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
                {
                    txt_hrdepartment_required_improvement_4.Text = "Administration";
                }
                else
                {
                    txt_hrdepartment_required_improvement_4.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "ASSOCIATE PROFESSOR".ToUpper() && lbl_assessmentby_reportingauthority_total_Criteriad != null)
            {
                int lbl_RequiredMinimumAPIScore_AssociateProfessor_60_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_AssociateProfessor_60.Text);
                int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text);

                if (lbl_RequiredMinimumAPIScore_AssociateProfessor_60_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
                {
                    txt_hrdepartment_required_improvement_4.Text = "Administration";
                }
                else
                {
                    txt_hrdepartment_required_improvement_4.Text = "";
                }
            }

            if (lbl_designation.Text.Trim().ToUpper() == "PROFESSOR".ToUpper() && lbl_assessmentby_reportingauthority_total_Criteriad != null)
            {
                int lbl_RequiredMinimumAPIScore_Professor_70_int = Convert.ToInt32(lbl_RequiredMinimumAPIScore_Professor_70.Text);
                int lbl_assessmentby_reportingauthority_total_Criteriad_int = Convert.ToInt32(lbl_assessmentby_reportingauthority_total_Criteriad.Text);

                if (lbl_RequiredMinimumAPIScore_Professor_70_int > lbl_assessmentby_reportingauthority_total_Criteriad_int)
                {
                    txt_hrdepartment_required_improvement_4.Text = "Administration";
                }
                else
                {
                    txt_hrdepartment_required_improvement_4.Text = "";
                }
            }


        }


    }
    protected void txt_a1_actualtotalactivites_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_a1_reporting_authority_assessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();
    }

    protected void txt_b1_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_b1_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_b2_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_b2_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_b3_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_b3_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_b4_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_b4_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_b5_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_b5_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_b6_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_b6_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_c1_RequiredActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_c1_AssessmentByAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_c2_AssessmentByAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_c2_RequiredActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_d1_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_d2_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_d2_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_d3_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_d3_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_d4_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_d4_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_d5_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_d5_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_d6_ActualTotalActivities_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_d6_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_e1_ScoresPerActivity_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_e1_ReportingAuthorityAssessment_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }


    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5_TextChanged(object sender, EventArgs e)
    {
        calculatedata();

    }

    protected void txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_p_ReportingAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();


    }

    protected void txt_p2_ReportingAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_p3_ReportingAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_p4_ReportingAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_p5_ReportingAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_p6_ReportingAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_p7_ReportingAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_p8_ReportingAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();
    }

    protected void txt_p9_ReportingAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();

    }

    protected void txt_p10_ReportingAuthority_TextChanged(object sender, EventArgs e)
    {
        Calculation_For_HOD();
    }
    public void Save_InDraft()
    {
        string UserType = "";
        string ApplicableFor = ViewState["ApplicableFor"].ToString().Trim();
        string ID_For_Updated = ViewState["ID_For_Updated"].ToString().Trim();
        string Created_On = System.DateTime.Now.ToString("dd MMM yyyy HH:mm").Trim();
        string Created_By_ID = lbl_emp_code.Text;
        string Created_By = lbl_faculty_name.Text;
        string Modified_On = System.DateTime.Now.ToString("dd MMM yyyy HH:mm").Trim();
        string Modified_By_ID = "";
        string Modified_By = "";
        string IsFaculty_Approval = "";
        string FacultyApproval_Id = "";
        string IsAssessment_Approval = "";
        string Assessment_Approval_Id = "";
        string IsHR_Approval = "";
        string HR_Approval_Id = "";
        string IsVC_Approval = "";
        string VC_Approval_Id = "";
        string Faculty_Approval_On = System.DateTime.Now.ToString("dd MMM yyyy HH:mm").Trim();
        string Assessment_Approval_On = System.DateTime.Now.ToString("dd MMM yyyy HH:mm").Trim();
        string HR_Approval_On = System.DateTime.Now.ToString("dd MMM yyyy HH:mm").Trim();
        string VC_Approval_On = System.DateTime.Now.ToString("dd MMM yyyy HH:mm").Trim();
        string Status = "Pending";

        pms_connection PMS = new pms_connection();
        PMS.SP_Tbl_PMS_Insert(ddl_academic_session.Text.Trim(), txt_a1_actualtotalactivites.Text.Trim(), lbl_a1_apiscore_through_self_assessment.Text.Trim(), lbl_a1_final_obtained_value.Text.Trim(), txt_a1_reporting_authority_assessment.Text.Trim(), txt_b1_ActualTotalActivities.Text.Trim(), lbl_b1_APIScoreThroughSelfAssessment.Text.Trim(), lbl_b1_FinalObtainedValue.Text.Trim(), txt_b1_ReportingAuthorityAssessment.Text.Trim(),
            txt_b2_ActualTotalActivities.Text.Trim(), lbl_b2_APIScoreThroughSelfAssessment.Text.Trim(), lbl_b2_FinalObtainedValue.Text.Trim(), txt_b2_ReportingAuthorityAssessment.Text.Trim(),
            txt_b3_ActualTotalActivities.Text.Trim(), lbl_b3_APIScoreThroughSelfAssessment.Text.Trim(), lbl_b3_FinalObtainedValue.Text.Trim(), txt_b3_ReportingAuthorityAssessment.Text.Trim(),

txt_b4_ActualTotalActivities.Text.Trim(), lbl_b4_APIScoreThroughSelfAssessment.Text.Trim(), lbl_b4_FinalObtainedValue.Text.Trim(), txt_b4_ReportingAuthorityAssessment.Text.Trim(),

txt_b5_ActualTotalActivities.Text.Trim(), lbl_b5_APIScoreThroughSelfAssessment.Text.Trim(), lbl_b5_FinalObtainedValue.Text.Trim(), txt_b5_ReportingAuthorityAssessment.Text.Trim(),

txt_b6_ActualTotalActivities.Text.Trim(), lbl_b6_APIScoreThroughSelfAssessment.Text.Trim(), lbl_b6_FinalObtainedValue.Text.Trim(), txt_b6_ReportingAuthorityAssessment.Text.Trim(),

txt_c1_RequiredActivities.Text.Trim(),
lbl_c1_TotalAPIScore.Text.Trim(),
lbl_c1_FinalScore.Text.Trim(),
txt_c1_AssessmentByAuthority.Text.Trim(),
txt_c2_RequiredActivities.Text.Trim(),
lbl_c2_TotalAPIScore.Text.Trim(),
lbl_c2_FinalScore.Text.Trim(),
txt_c2_AssessmentByAuthority.Text.Trim(),
txt_d1_ActualTotalActivities.Text.Trim(),
lbl_d1_APIScoreThroughSelfAssessment.Text.Trim(),
lbl_d1_FinalObtainedValue.Text.Trim(),
txt_d1_ReportingAuthorityAssessment.Text.Trim(),
txt_d2_ActualTotalActivities.Text.Trim(),
lbl_d2_APIScoreThroughSelfAssessment.Text.Trim(),
lbl_d2_FinalObtainedValue.Text.Trim(),
txt_d2_ReportingAuthorityAssessment.Text.Trim(),
txt_d3_ActualTotalActivities.Text.Trim(),
lbl_d3_APIScoreThroughSelfAssessment.Text.Trim(),
lbl_d3_FinalObtainedValue.Text.Trim(),
txt_d3_ReportingAuthorityAssessment.Text.Trim(),
txt_d4_ActualTotalActivities.Text.Trim(),
lbl_d4_APIScoreThroughSelfAssessment.Text.Trim(),
lbl_d4_FinalObtainedValue.Text.Trim(),
txt_d4_ReportingAuthorityAssessment.Text.Trim(),
txt_d5_ActualTotalActivities.Text.Trim(),
lbl_d5_APIScoreThroughSelfAssessment.Text.Trim(),
lbl_d5_FinalObtainedValue.Text.Trim(),
txt_d5_ReportingAuthorityAssessment.Text.Trim(),
txt_d6_ActualTotalActivities.Text.Trim(),
lbl_d6_APIScoreThroughSelfAssessment.Text.Trim(),
lbl_d6_FinalObtainedValue.Text.Trim(),
txt_d6_ReportingAuthorityAssessment.Text.Trim(),
txt_e1_ScoresPerActivity.Text.Trim(),
lbl_e1_APIScoreThroughSelfAssessment.Text.Trim(),
lbl_e1_FinalObtainedValue.Text.Trim(),
txt_e1_ReportingAuthorityAssessment.Text.Trim(),
lbl_e1_APIScoreThroughSelfAssessment_Total.Text.Trim(),
lbl_e1_FinalObtainedValue_Total.Text.Trim(),
lbl_CriteriaA_AcademicPerformance_Total_Total.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text.Trim(),
// For f2
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text.Trim(),

// For f3
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text.Trim(),

// For f4
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text.Trim(),

// For f5
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text.Trim(),

// For f6
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text.Trim(),

// For f7
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f7.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text.Trim(),
// For g1
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text.Trim(),

// For g2
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text.Trim(),

// For g3
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text.Trim(),
// For h1
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text.Trim(),

// For h2
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text.Trim(),

// For h3
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text.Trim(),

// For h4
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text.Trim(),

// For h5
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text.Trim(),

// For h6
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text.Trim(),

// For h7
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text.Trim(),

// For h8
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text.Trim(),

// For h9
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text.Trim(),
// For i1
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i1.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text.Trim(),

// For i2
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text.Trim(),

// For i3
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text.Trim(),

// For i4
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text.Trim(),

// For i5
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text.Trim(),
// For j1
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text.Trim(),

// For j2
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text.Trim(),
lbl_api_score_through_self_assessment_total.Text.Trim(),
lbl_finally_obtained_score_total.Text.Trim(),
lbl_assessmentby_reportingauthority_total.Text.Trim(),
// For k1
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text.Trim(),

// For k2
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text.Trim(),

// For k3
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text.Trim(),

// For k4
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text.Trim(),

// For k5
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text.Trim(),

// For k6
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text.Trim(),

// For k7
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text.Trim(),

// For k8
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text.Trim(),

// For k9
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text.Trim(),

// For k10
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text.Trim(),

// For k11
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text.Trim(),
// For l1
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text.Trim(),

// For l2
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text.Trim(),

// For l3
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text.Trim(),
// For m1
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text.Trim(),

// For m2
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text.Trim(),

// For m3
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text.Trim(),

// For m4
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text.Trim(),
// For n1
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text.Trim(),

// For n2
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text.Trim(),

// For n3
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text.Trim(),
lbl_api_score_through_self_assessment_total_criteriac.Text.Trim(),
lbl_finally_obtained_score_total_criteriac.Text.Trim(),
lbl_assessmentby_reportingauthority_total_Criteriac.Text.Trim(),
// For o1
txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1.Text.Trim(),
lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1.Text.Trim(),
lbl_CriteriaD_Administration_TotalorFaculty_o1.Text.Trim(),
txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text.Trim(),
// For o2
txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2.Text.Trim(),
lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2.Text.Trim(),
lbl_CriteriaD_Administration_TotalorFaculty_o2.Text.Trim(),
txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text.Trim(),

// For o3
txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3.Text.Trim(),
lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3.Text.Trim(),
lbl_CriteriaD_Administration_TotalorFaculty_o3.Text.Trim(),
txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text.Trim(),

// For o4
txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4.Text.Trim(),
lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4.Text.Trim(),
lbl_CriteriaD_Administration_TotalorFaculty_o4.Text.Trim(),
txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text.Trim(),

// For o5
txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5.Text.Trim(),
lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5.Text.Trim(),
lbl_CriteriaD_Administration_TotalorFaculty_o5.Text.Trim(),
txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text.Trim(),
lbl_api_score_through_self_assessment_total_criteriad.Text.Trim(),
lbl_finally_obtained_score_total_criteriad.Text.Trim(),
lbl_assessmentby_reportingauthority_total_Criteriad.Text.Trim(),
// For p1
txt_p1_ReportingAuthority.Text.Trim(),

// For p2
txt_p2_ReportingAuthority.Text.Trim(),

// For p3
txt_p3_ReportingAuthority.Text.Trim(),

// For p4
txt_p4_ReportingAuthority.Text.Trim(),

// For p5
txt_p5_ReportingAuthority.Text.Trim(),

// For p6
txt_p6_ReportingAuthority.Text.Trim(),

// For p7
txt_p7_ReportingAuthority.Text.Trim(),

// For p8
txt_p8_ReportingAuthority.Text.Trim(),

// For p9
txt_p9_ReportingAuthority.Text.Trim(),

// For p10
txt_p10_ReportingAuthority.Text.Trim(),
lbl_p_ReportingAuthority_total.Text.Trim(),

lbl_facultyfeedback_inpercentage.Text.Trim(),
lbl_facultyobtained_total.Text.Trim(),
lbl_apiscorecalculation_criteriaA.Text.Trim(),
lbl_apiscorecalculation_criteriaB.Text.Trim(),
lbl_apiscorecalculation_criteriaC.Text.Trim(),
lbl_apiscorecalculation_criteriaD.Text.Trim(),
lbl_apiscorecalculation_criteriaE.Text.Trim(),
lbl_apiscorecalculation_criteriaF.Text.Trim(),
lbl_totalAPIScore.Text.Trim(),
lbl_facultyCategory.Text.Trim(),
txt_commentsAndsuggestion_a.Text.Trim(),
txt_commentsAndsuggestion_b.Text.Trim(),
txt_commentsAndsuggestion_c.Text.Trim(),
txt_commentsAndsuggestion_d.Text.Trim(),
txt_commentsAndsuggestion_e.Text.Trim(),

lbl_hrdepartment_totalapi_mAX_score.Text.Trim(),
lbl_hrdepartment_totalapiscore.Text.Trim(),
lbl_hrdepartment_faculty_categorybasedscore.Text.Trim(),
txt_hrdepartment_required_improvement_1.Text.Trim(),
txt_hrdepartment_required_improvement_2.Text.Trim(),
txt_hrdepartment_required_improvement_3.Text.Trim(),
txt_hrdepartment_required_improvement_4.Text.Trim(),
txt_hr_recommendations_a.Text.Trim(),
txt_hr_recommendations_b.Text.Trim(),
txt_hr_recommendations_c.Text.Trim(),
txt_hr_recommendations_d.Text.Trim(),
txt_hr_recommendations_e.Text.Trim(),
Created_On.Trim(),
Created_By_ID.Trim(),
Created_By.Trim(),
Modified_On.Trim(),
Modified_By_ID.Trim(),
Modified_By.Trim(),
IsFaculty_Approval.Trim(),
FacultyApproval_Id.Trim(),
IsAssessment_Approval.Trim(),
Assessment_Approval_Id.Trim(),
IsHR_Approval.Trim(),
HR_Approval_Id.Trim(),
IsVC_Approval.Trim(),
VC_Approval_Id.Trim(),
Faculty_Approval_On.Trim(),
Assessment_Approval_On.Trim(),
HR_Approval_On.Trim(),
VC_Approval_On.Trim(), Status.Trim(), txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2.Text.Trim()
, lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_2.Text.Trim()
, lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1_2.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2.Text.Trim(),
txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2.Text.Trim(),
lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_2.Text.Trim()
, lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3_2.Text.Trim()
, txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2.Text.Trim(), lbl_faculty_name.Text.Trim(), lbl_designation.Text.Trim(), lbl_emp_code.Text.Trim(), lbl_college_department.Text.Trim(),
UserType.Trim(), ApplicableFor.Trim(), ID_For_Updated.Trim(), lbl_New_College.Text.Trim(), lbl_New_Department.Text.Trim(),
lbl_ff_odd_sem.Text.Trim(),
lbl_ff_even_sem.Text.Trim()
);
        PMS.DisConnect();
    }
    protected void Btn_Save_Click1(object sender, EventArgs e)
    {

        Save_InDraft();
        sp_Get_PMS_All_Data(dd_AcademicYear.SelectedValue.Trim(), txtFilterBy.Text.Trim(), Session["uid"].ToString().Trim(), Session["Departmentcode"].ToString().Trim(), dd_Staus.SelectedValue.Trim());
        pnlcreate.Visible = false;
        pnl_Dashboard.Visible = true;
        btnCreateNew.Visible = false;
    }
    public SqlDataReader sp_Get_PMS_DataWithID(string ID)
    {
        SqlCommand cmd = new SqlCommand("sp_Get_PMS_DataWithID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", ID.Trim());
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    //public void bindTypeOfReseach()
    //{
    //    SqlCommand cmd = new SqlCommand("proc_fetchRIDocumentType", con);
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    con.Open();

    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    con.Close();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        drpResearchIncentive.DataSource = dt;
    //        drpResearchIncentive.DataTextField = "RI_Type";
    //        drpResearchIncentive.DataValueField = "ID";

    //        drpResearchIncentive.DataBind();
    //    }
    //}

    protected void btnViewDetails_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        ViewState["ApplicableFor"] = "Update";
        ViewState["ID_For_Updated"] = id.ToString().Trim();
        Session["PMS_PDF_VIEW_ID"] = id.ToString().Trim();
        lblMonth.Text = drpMonth.SelectedItem.Text;
        hfmonth.Value = drpMonth.SelectedValue;
        pnlcreate.Visible = true;
        pnl_Dashboard.Visible = false;
        string IsFaculty_Approval = ""; string IsAssessment_Approval = ""; string IsHR_Approval = ""; string IsVC_Approval = "";
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.sp_Get_PMS_DataWithID(id);
        dr.Read();
        if (dr.HasRows)
        {
            dd_Designation.Items.Add(dr["Designation"].ToString().Trim());



            lbl_faculty_name.Text = dr["Faculty_Name"].ToString().Trim();
            lbl_designation.Text = dr["Designation"].ToString().Trim();
            dd_Designation.SelectedValue = lbl_designation.Text.ToUpper();
            dd_Designation.Visible = false;
            lbl_designation.Visible = true;
            lbl_emp_code.Text = dr["Employee_Code"].ToString().Trim();
            lbl_college_department.Text = dr["Department"].ToString().Trim();
            lbl_New_College.Text = dr["College"].ToString();
            lbl_New_Department.Text = dr["New_Department"].ToString();
            ddl_academic_session.Text = dr["Academic_Year"].ToString().Trim();
            drpMonth.SelectedValue = DateTime.Now.Month.ToString();
            txt_a1_actualtotalactivites.Text = dr["A1_Total_Numbers_B"].ToString().Trim();
            lbl_a1_apiscore_through_self_assessment.Text = dr["A1_API_Score_AxB"].ToString().Trim();
            lbl_a1_final_obtained_value.Text = dr["A1_Finally_Obtained_PQ"].ToString().Trim();
            txt_a1_reporting_authority_assessment.Text = dr["A1_AssessmentBy_RM"].ToString().Trim();

            txt_b1_ActualTotalActivities.Text = dr["B1_Total_Numbers_B"].ToString().Trim();
            lbl_b1_APIScoreThroughSelfAssessment.Text = dr["B1_API_Score_AxB"].ToString().Trim();
            lbl_b1_FinalObtainedValue.Text = dr["B1_Finally_Obtained_PQ"].ToString().Trim();
            txt_b1_ReportingAuthorityAssessment.Text = dr["B1_AssessmentBy_RM"].ToString().Trim();

            txt_b2_ActualTotalActivities.Text = dr["B2_Total_Numbers_B"].ToString().Trim();
            lbl_b2_APIScoreThroughSelfAssessment.Text = dr["B2_API_Score_AxB"].ToString().Trim();
            lbl_b2_FinalObtainedValue.Text = dr["B2_Finally_Obtained_PQ"].ToString().Trim();
            txt_b2_ReportingAuthorityAssessment.Text = dr["B2_AssessmentBy_RM"].ToString().Trim();

            txt_b3_ActualTotalActivities.Text = dr["B3_Total_Numbers_B"].ToString().Trim();
            lbl_b3_APIScoreThroughSelfAssessment.Text = dr["B3_API_Score_AxB"].ToString().Trim();
            lbl_b3_FinalObtainedValue.Text = dr["B3_Finally_Obtained_PQ"].ToString().Trim();
            txt_b3_ReportingAuthorityAssessment.Text = dr["B3_AssessmentBy_RM"].ToString().Trim();

            txt_b4_ActualTotalActivities.Text = dr["B4_Total_Numbers_B"].ToString().Trim();
            lbl_b4_APIScoreThroughSelfAssessment.Text = dr["B4_API_Score_AxB"].ToString().Trim();
            lbl_b4_FinalObtainedValue.Text = dr["B4_Finally_Obtained_PQ"].ToString().Trim();
            txt_b4_ReportingAuthorityAssessment.Text = dr["B4_AssessmentBy_RM"].ToString().Trim();

            txt_b5_ActualTotalActivities.Text = dr["B5_Total_Numbers_B"].ToString().Trim();
            lbl_b5_APIScoreThroughSelfAssessment.Text = dr["B5_API_Score_AxB"].ToString().Trim();
            lbl_b5_FinalObtainedValue.Text = dr["B5_Finally_Obtained_PQ"].ToString().Trim();
            txt_b5_ReportingAuthorityAssessment.Text = dr["B5_AssessmentBy_RM"].ToString().Trim();

            txt_b6_ActualTotalActivities.Text = dr["B6_Total_Numbers_B"].ToString().Trim();
            lbl_b6_APIScoreThroughSelfAssessment.Text = dr["B6_API_Score_AxB"].ToString().Trim();
            lbl_b6_FinalObtainedValue.Text = dr["B6_Finally_Obtained_PQ"].ToString().Trim();
            txt_b6_ReportingAuthorityAssessment.Text = dr["B6_AssessmentBy_RM"].ToString().Trim();

            txt_c1_RequiredActivities.Text = dr["C1_Total_Numbers_B"].ToString().Trim();
            lbl_c1_TotalAPIScore.Text = dr["C1_API_Score_AxB"].ToString().Trim();
            lbl_c1_FinalScore.Text = dr["C1_Finally_Obtained_PQ"].ToString().Trim();
            txt_c1_AssessmentByAuthority.Text = dr["C1_AssessmentBy_RM"].ToString().Trim();

            txt_c2_RequiredActivities.Text = dr["C2_Total_Numbers_B"].ToString().Trim();
            lbl_c2_TotalAPIScore.Text = dr["C2_API_Score_AxB"].ToString().Trim();
            lbl_c2_FinalScore.Text = dr["C2_Finally_Obtained_PQ"].ToString().Trim();
            txt_c2_AssessmentByAuthority.Text = dr["C2_AssessmentBy_RM"].ToString().Trim();

            txt_d1_ActualTotalActivities.Text = dr["D1_Total_Numbers_B"].ToString().Trim();
            lbl_d1_APIScoreThroughSelfAssessment.Text = dr["D1_API_Score_AxB"].ToString().Trim();
            lbl_d1_FinalObtainedValue.Text = dr["D1_Finally_Obtained_PQ"].ToString().Trim();
            txt_d1_ReportingAuthorityAssessment.Text = dr["D1_AssessmentBy_RM"].ToString().Trim();

            txt_d2_ActualTotalActivities.Text = dr["D2_Total_Numbers_B"].ToString().Trim();
            lbl_d2_APIScoreThroughSelfAssessment.Text = dr["D2_API_Score_AxB"].ToString().Trim();
            lbl_d2_FinalObtainedValue.Text = dr["D2_Finally_Obtained_PQ"].ToString().Trim();
            txt_d2_ReportingAuthorityAssessment.Text = dr["D2_AssessmentBy_RM"].ToString().Trim();

            txt_d3_ActualTotalActivities.Text = dr["D3_Total_Numbers_B"].ToString().Trim();
            lbl_d3_APIScoreThroughSelfAssessment.Text = dr["D3_API_Score_AxB"].ToString().Trim();
            lbl_d3_FinalObtainedValue.Text = dr["D3_Finally_Obtained_PQ"].ToString().Trim();
            txt_d3_ReportingAuthorityAssessment.Text = dr["D3_AssessmentBy_RM"].ToString().Trim();

            txt_d4_ActualTotalActivities.Text = dr["D4_Total_Numbers_B"].ToString().Trim();
            lbl_d4_APIScoreThroughSelfAssessment.Text = dr["D4_API_Score_AxB"].ToString().Trim();
            lbl_d4_FinalObtainedValue.Text = dr["D4_Finally_Obtained_PQ"].ToString().Trim();
            txt_d4_ReportingAuthorityAssessment.Text = dr["D4_AssessmentBy_RM"].ToString().Trim();

            txt_d5_ActualTotalActivities.Text = dr["D5_Total_Numbers_B"].ToString().Trim();
            lbl_d5_APIScoreThroughSelfAssessment.Text = dr["D5_API_Score_AxB"].ToString().Trim();
            lbl_d5_FinalObtainedValue.Text = dr["D5_Finally_Obtained_PQ"].ToString().Trim();
            txt_d5_ReportingAuthorityAssessment.Text = dr["D5_AssessmentBy_RM"].ToString().Trim();

            txt_d6_ActualTotalActivities.Text = dr["D6_Total_Numbers_B"].ToString().Trim();
            lbl_d6_APIScoreThroughSelfAssessment.Text = dr["D6_API_Score_AxB"].ToString().Trim();
            lbl_d6_FinalObtainedValue.Text = dr["D6_Finally_Obtained_PQ"].ToString().Trim();
            txt_d6_ReportingAuthorityAssessment.Text = dr["D6_AssessmentBy_RM"].ToString().Trim();

            txt_e1_ScoresPerActivity.Text = dr["E1_Total_Numbers_B"].ToString().Trim();
            lbl_e1_APIScoreThroughSelfAssessment.Text = dr["E1_API_Score_AxB"].ToString().Trim();
            lbl_e1_FinalObtainedValue.Text = dr["E1_Finally_Obtained_PQ"].ToString().Trim();
            txt_e1_ReportingAuthorityAssessment.Text = dr["E1_AssessmentBy_RM"].ToString().Trim();

            lbl_e1_APIScoreThroughSelfAssessment_Total.Text = dr["CriteriaA_API_Score_AxB_Total"].ToString().Trim();
            lbl_e1_FinalObtainedValue_Total.Text = dr["CriteriaA_Finally_Obtained_PQ_Total"].ToString().Trim();
            lbl_CriteriaA_AcademicPerformance_Total_Total.Text = dr["CriteriaA_AssessmentBy_RM_Total"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1.Text = dr["F1_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1.Text = dr["F1_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1.Text = dr["F1_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text = dr["F1_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Text = dr["F2_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2.Text = dr["F2_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2.Text = dr["F2_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text = dr["F2_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3.Text = dr["F3_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3.Text = dr["F3_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3.Text = dr["F3_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text = dr["F3_AssessmentBy_RM"].ToString().Trim();


            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Text = dr["F4_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4.Text = dr["F4_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4.Text = dr["F4_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text = dr["F4_AssessmentBy_RM"].ToString().Trim();


            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Text = dr["F5_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5.Text = dr["F5_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5.Text = dr["F5_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text = dr["F5_AssessmentBy_RM"].ToString().Trim();


            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Text = dr["F6_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6.Text = dr["F6_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6.Text = dr["F6_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text = dr["F6_AssessmentBy_RM"].ToString().Trim();


            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Text = dr["F7_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f7.Text = dr["F7_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7.Text = dr["F7_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text = dr["F7_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1.Text = dr["G1_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1.Text = dr["G1_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1.Text = dr["G1_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text = dr["G1_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2.Text = dr["G2_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2.Text = dr["G2_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2.Text = dr["G2_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text = dr["G2_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3.Text = dr["G3_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3.Text = dr["G3_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3.Text = dr["G3_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text = dr["G3_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Text = dr["H1_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1.Text = dr["H1_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1.Text = dr["H1_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text = dr["H1_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Text = dr["H2_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2.Text = dr["H2_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2.Text = dr["H2_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text = dr["H2_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Text = dr["H3_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3.Text = dr["H3_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3.Text = dr["H3_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text = dr["H3_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Text = dr["H4_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4.Text = dr["H4_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4.Text = dr["H4_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text = dr["H4_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Text = dr["H5_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5.Text = dr["H5_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5.Text = dr["H5_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text = dr["H5_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Text = dr["H6_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6.Text = dr["H6_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6.Text = dr["H6_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text = dr["H6_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Text = dr["H7_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7.Text = dr["H7_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7.Text = dr["H7_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text = dr["H7_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Text = dr["H8_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8.Text = dr["H8_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8.Text = dr["H8_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text = dr["H8_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Text = dr["H9_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9.Text = dr["H9_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9.Text = dr["H9_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text = dr["H9_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1.Text = dr["I1_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i1.Text = dr["I1_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i1.Text = dr["I1_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Text = dr["I1_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Text = dr["I2_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2.Text = dr["I2_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2.Text = dr["I2_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text = dr["I2_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Text = dr["I3_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3.Text = dr["I3_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3.Text = dr["I3_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text = dr["I3_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Text = dr["I4_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4.Text = dr["I4_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4.Text = dr["I4_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text = dr["I4_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Text = dr["I5_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5.Text = dr["I5_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5.Text = dr["I5_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text = dr["I5_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Text = dr["J1_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1.Text = dr["J1_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1.Text = dr["J1_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text = dr["J1_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Text = dr["J2_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2.Text = dr["J2_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2.Text = dr["J2_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text = dr["J2_AssessmentBy_RM"].ToString().Trim();

            lbl_api_score_through_self_assessment_total.Text = dr["CriteriaB_API_Score_AxB_Total"].ToString().Trim();
            lbl_finally_obtained_score_total.Text = dr["CriteriaB_Finally_Obtained_PQ_Total"].ToString().Trim();
            lbl_assessmentby_reportingauthority_total.Text = dr["CriteriaB_AssessmentBy_RM_Total"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1.Text = dr["K1_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1.Text = dr["K1_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1.Text = dr["K1_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text = dr["K1_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2.Text = dr["K2_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2.Text = dr["K2_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2.Text = dr["K2_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text = dr["K2_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Text = dr["K3_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3.Text = dr["K3_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3.Text = dr["K3_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text = dr["K3_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Text = dr["K4_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4.Text = dr["K4_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4.Text = dr["K4_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text = dr["K4_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Text = dr["K5_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5.Text = dr["K5_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5.Text = dr["K5_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text = dr["K5_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Text = dr["K6_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6.Text = dr["K6_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6.Text = dr["K6_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text = dr["K6_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Text = dr["K7_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7.Text = dr["K7_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7.Text = dr["K7_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text = dr["K7_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Text = dr["K8_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8.Text = dr["K8_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8.Text = dr["K8_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text = dr["K8_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Text = dr["K9_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9.Text = dr["K9_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9.Text = dr["K9_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text = dr["K9_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Text = dr["K10_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10.Text = dr["K10_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10.Text = dr["K10_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text = dr["K10_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Text = dr["K11_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11.Text = dr["K11_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11.Text = dr["K11_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text = dr["K11_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1.Text = dr["L1_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1.Text = dr["L1_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1.Text = dr["L1_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text = dr["L1_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2.Text = dr["L2_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2.Text = dr["L2_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2.Text = dr["L2_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text = dr["L2_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3.Text = dr["L3_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3.Text = dr["L3_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3.Text = dr["L3_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text = dr["L3_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1.Text = dr["M1_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1.Text = dr["M1_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1.Text = dr["M1_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text = dr["M1_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2.Text = dr["M2_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2.Text = dr["M2_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2.Text = dr["M2_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text = dr["M2_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3.Text = dr["M3_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3.Text = dr["M3_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3.Text = dr["M3_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text = dr["M3_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4.Text = dr["M4_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4.Text = dr["M4_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4.Text = dr["M4_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text = dr["M4_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1.Text = dr["N1_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1.Text = dr["N1_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1.Text = dr["N1_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text = dr["N1_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2.Text = dr["N2_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2.Text = dr["N2_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2.Text = dr["N2_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text = dr["N2_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3.Text = dr["N3_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3.Text = dr["N3_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3.Text = dr["N3_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text = dr["N3_AssessmentBy_RM"].ToString().Trim();

            lbl_api_score_through_self_assessment_total_criteriac.Text = dr["CriteriaC_API_Score_AxB_Total"].ToString().Trim();
            lbl_finally_obtained_score_total_criteriac.Text = dr["CriteriaC_Finally_Obtained_PQ_Total"].ToString().Trim();
            lbl_assessmentby_reportingauthority_total_Criteriac.Text = dr["CriteriaC_AssessmentBy_RM_Total"].ToString().Trim();

            txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1.Text = dr["O1_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1.Text = dr["O1_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaD_Administration_TotalorFaculty_o1.Text = dr["O1_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text = dr["O1_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2.Text = dr["O2_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2.Text = dr["O2_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaD_Administration_TotalorFaculty_o2.Text = dr["O2_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text = dr["O2_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3.Text = dr["O3_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3.Text = dr["O3_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaD_Administration_TotalorFaculty_o3.Text = dr["O3_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text = dr["O3_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4.Text = dr["O4_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4.Text = dr["O4_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaD_Administration_TotalorFaculty_o4.Text = dr["O4_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text = dr["O4_AssessmentBy_RM"].ToString().Trim();

            txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5.Text = dr["O5_Total_Numbers_B"].ToString().Trim();
            lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5.Text = dr["O5_API_Score_AxB"].ToString().Trim();
            lbl_CriteriaD_Administration_TotalorFaculty_o5.Text = dr["O5_Finally_Obtained_PQ"].ToString().Trim();
            txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text = dr["O5_AssessmentBy_RM"].ToString().Trim();

            lbl_api_score_through_self_assessment_total_criteriad.Text = dr["CriteriaD_API_Score_AxB_Total"].ToString().Trim();
            lbl_finally_obtained_score_total_criteriad.Text = dr["CriteriaD_Finally_Obtained_PQ_Total"].ToString().Trim();
            lbl_assessmentby_reportingauthority_total_Criteriad.Text = dr["CriteriaD_AssessmentBy_RM_Total"].ToString().Trim();

            txt_p1_ReportingAuthority.Text = dr["P1_ScoreGivenBy_RM"].ToString().Trim();
            txt_p2_ReportingAuthority.Text = dr["P2_ScoreGivenBy_RM"].ToString().Trim();
            txt_p3_ReportingAuthority.Text = dr["P3_ScoreGivenBy_RM"].ToString().Trim();
            txt_p4_ReportingAuthority.Text = dr["P4_ScoreGivenBy_RM"].ToString().Trim();
            txt_p5_ReportingAuthority.Text = dr["P5_ScoreGivenBy_RM"].ToString().Trim();
            txt_p6_ReportingAuthority.Text = dr["P6_ScoreGivenBy_RM"].ToString().Trim();
            txt_p7_ReportingAuthority.Text = dr["P7_ScoreGivenBy_RM"].ToString().Trim();
            txt_p8_ReportingAuthority.Text = dr["P8_ScoreGivenBy_RM"].ToString().Trim();
            txt_p9_ReportingAuthority.Text = dr["P9_ScoreGivenBy_RM"].ToString().Trim();
            txt_p10_ReportingAuthority.Text = dr["P10_ScoreGivenBy_RM"].ToString().Trim();

            lbl_p_ReportingAuthority_total.Text = dr["CriteriaE_ScoreGivenBy_RM_Total"].ToString().Trim();
            lbl_facultyfeedback_inpercentage.Text = dr["CriteriaF_FacultyFeedback_Percentage"].ToString().Trim();
            lbl_facultyobtained_total.Text = dr["CriteriaF_FacultyFeedback_ObtainedMarks_Total"].ToString().Trim();

            lbl_apiscorecalculation_criteriaA.Text = dr["APIScore_CriteriaA_Faculty_Total"].ToString().Trim();
            lbl_apiscorecalculation_criteriaB.Text = dr["APIScore_CriteriaB_Faculty_Total"].ToString().Trim();
            lbl_apiscorecalculation_criteriaC.Text = dr["APIScore_CriteriaC_Faculty_Total"].ToString().Trim();
            lbl_apiscorecalculation_criteriaD.Text = dr["APIScore_CriteriaD_Faculty_Total"].ToString().Trim();
            lbl_apiscorecalculation_criteriaE.Text = dr["APIScore_CriteriaE_RM_Total"].ToString().Trim();
            lbl_apiscorecalculation_criteriaF.Text = dr["APIScore_CriteriaF_RM_Total"].ToString().Trim();
            lbl_totalAPIScore.Text = dr["HR_TotalObtainedScore_Total"].ToString().Trim();
            lbl_facultyCategory.Text = dr["HR_FacultyCateogory"].ToString().Trim();

            txt_commentsAndsuggestion_a.Text = dr["RM_Comments_A"].ToString().Trim();
            txt_commentsAndsuggestion_b.Text = dr["RM_Comments_B"].ToString().Trim();
            txt_commentsAndsuggestion_c.Text = dr["RM_Comments_C"].ToString().Trim();
            txt_commentsAndsuggestion_d.Text = dr["RM_Comments_D"].ToString().Trim();
            txt_commentsAndsuggestion_e.Text = dr["RM_Comments_E"].ToString().Trim();

            //here, maxscore total not worked on the id 

            lbl_hrdepartment_totalapiscore.Text = dr["HR_TotalObtainedScore_Total"].ToString().Trim();
            lbl_hrdepartment_faculty_categorybasedscore.Text = dr["HR_FacultyCateogory"].ToString().Trim();

            txt_hrdepartment_required_improvement_1.Text = dr["HR_RequiredImprovement_txt1"].ToString().Trim();
            txt_hrdepartment_required_improvement_2.Text = dr["HR_RequiredImprovement_txt2"].ToString().Trim();
            txt_hrdepartment_required_improvement_3.Text = dr["HR_RequiredImprovement_txt3"].ToString().Trim();
            txt_hrdepartment_required_improvement_4.Text = dr["HR_RequiredImprovement_txt4"].ToString().Trim();

            txt_hr_recommendations_a.Text = dr["HR_Recommendations_A"].ToString().Trim();
            txt_hr_recommendations_b.Text = dr["HR_Recommendations_B"].ToString().Trim();
            txt_hr_recommendations_c.Text = dr["HR_Recommendations_C"].ToString().Trim();
            txt_hr_recommendations_d.Text = dr["HR_Recommendations_D"].ToString().Trim();
            txt_hr_recommendations_e.Text = dr["HR_Recommendations_E"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2.Text = dr["F1_2_Total_Numbers_B"].ToString().Trim();

            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_2.Text = dr["F1_2_API_Score_AxB"].ToString().Trim();


            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1_2.Text = dr["F1_2_Finally_Obtained_PQ"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2.Text = dr["F1_2_AssessmentBy_RM"].ToString().Trim();



            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2.Text = dr["F3_2_Total_Numbers_B"].ToString().Trim();

            lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_2.Text = dr["F3_2_API_Score_AxB"].ToString().Trim();

            lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3_2.Text = dr["F3_2_Finally_Obtained_PQ"].ToString().Trim();

            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2.Text = dr["F3_2_AssessmentBy_RM"].ToString().Trim();

            //Created_On = dr["Created_On"].ToString().Trim();
            lbl_emp_code.Text = dr["Created_By_ID"].ToString().Trim();
            //Created_By = dr["Created_By"].ToString().Trim();
            //Modified_On = dr["Modified_On"].ToString().Trim();
            //Modified_By_ID = dr["Modified_By_ID"].ToString().Trim();
            //Modified_By = dr["Modified_By"].ToString().Trim();
            IsFaculty_Approval = dr["IsFaculty_Approval"].ToString().Trim().ToUpper();

            //FacultyApproval_Id = dr["Faculty_Approval_ID"].ToString().Trim();
            IsAssessment_Approval = dr["IsAssessment_Approval"].ToString().Trim().ToUpper();
            //Assessment_Approval_Id = dr["Assessment_Approval_ID"].ToString().Trim();
            IsHR_Approval = dr["IsHR_Approval"].ToString().Trim().ToUpper();
            //HR_Approval_Id = dr["HR_Approval_ID"].ToString().Trim();
            IsVC_Approval = dr["IsVC_Approval"].ToString().Trim().ToUpper();
            //VC_Approval_Id = dr["VC_Approval_ID"].ToString().Trim();
            //Faculty_Approval_On = dr["Faculty_Approval_On"].ToString().Trim();
            //Assessment_Approval_On = dr["Assesment_Approval_On"].ToString().Trim();
            //HR_Approval_On = dr["HR_Approval_On"].ToString().Trim();
            //VC_Approval_On = dr["VC_Approval_On"].ToString().Trim();
            //Status = dr["Status"].ToString().Trim();
            lbl_ff_even_sem.Text = dr["Feed_Even_Sem"].ToString().Trim();
            lbl_ff_odd_sem.Text = dr["Feed_Odd_Sem"].ToString().Trim();


            GetNoActivity();











        }

        dr.Close();
        con.DisConnect();



        if (IsFaculty_Approval == "FALSE" && IsAssessment_Approval.Trim() == "FALSE" && IsHR_Approval.Trim() == "FALSE" && IsVC_Approval == "FALSE")
        {

            Faculty_Enabledata();
            assesmentDisable();
            HR_Disable();
            Btn_Save.Visible = true;
            if (lblMonth.Text == "Annual")
            {
                Btn_Approval.Visible = true;
            }
            else
            {
                Btn_Approval.Visible = false;
            }
            ViewState["Applicable_For_Department"] = "TEACH";
            btn_Fu_A1.Visible = true;
            btnAttachmentSave_Fu_A1.Visible = true;
        }

        if (IsFaculty_Approval == "TRUE" && IsAssessment_Approval.Trim() == "FALSE" && IsHR_Approval.Trim() == "FALSE" && IsVC_Approval == "FALSE")
        {
            btn_Fu_A1.Visible = false;
            btnAttachmentSave_Fu_A1.Visible = false;
            SqlDataReader drr = con.sp_PMS_Check_Reporting_Manager(lbl_emp_code.Text.Trim(), Session["uid"].ToString().Trim());
            drr.Read();
            int checkReportingManager = Convert.ToInt32(drr["CountData"].ToString().Trim());
            drr.Close();
            con.DisConnect();

            if (checkReportingManager > 0)
            {

                Faculty_Disabledata();
                assesmentEnable();
                HR_Disable();
                Btn_Save.Visible = true;
                //visible false for temp by Bhupii

                Btn_Approval.Visible = false;

                ViewState["Applicable_For_Department"] = "RM";
            }
            if (checkReportingManager <= 0)
            {
                Faculty_Disabledata();
                assesmentDisable();
                HR_Disable();
                Btn_Save.Visible = false;

                Btn_Approval.Visible = false;

                ViewState["Applicable_For_Department"] = "TEACH";
            }
        }

        if (IsFaculty_Approval == "TRUE" && IsAssessment_Approval.Trim() == "TRUE" && IsHR_Approval.Trim() == "FALSE" && IsVC_Approval == "FALSE")
        {
            btn_Fu_A1.Visible = false;
            btnAttachmentSave_Fu_A1.Visible = false;
            Faculty_Disabledata();
            assesmentDisable();
            if (Session["Departmentcode"].ToString().Trim() == "D228")
            {
                HR_Enable();
                Btn_Save.Visible = true;
                //visible false for temp by Bhupii

                Btn_Approval.Visible = true;

                ViewState["Applicable_For_Department"] = "HR";
            }
            if (Session["Departmentcode"].ToString().Trim() != "D228")
            {
                HR_Disable();
                Btn_Save.Visible = false;

                Btn_Approval.Visible = false;


            }
        }
        if (IsFaculty_Approval == "TRUE" && IsAssessment_Approval.Trim() == "TRUE" && IsHR_Approval.Trim() == "TRUE" && IsVC_Approval == "FALSE")
        {
            Btn_Approval.Text = "Approve";
            btn_Fu_A1.Visible = false;
            btnAttachmentSave_Fu_A1.Visible = false;
            Faculty_Disabledata();
            assesmentDisable();
            HR_Disable();
            if (Session["uid"].ToString().Trim() == "TMU08026")
            {
                //visible false for temp by Bhupii

                Btn_Approval.Visible = false;

                Btn_Save.Visible = false;
                ViewState["Applicable_For_Department"] = "VC";
            }
            else
            {
                Btn_Save.Visible = false;

                Btn_Approval.Visible = false;

            }
        }
        if (IsFaculty_Approval == "TRUE" && IsAssessment_Approval.Trim() == "TRUE" && IsHR_Approval.Trim() == "TRUE" && IsVC_Approval == "TRUE")
        {
            btn_Fu_A1.Visible = false;
            btnAttachmentSave_Fu_A1.Visible = false;
            Faculty_Disabledata();
            assesmentDisable();
            HR_Disable();

            Btn_Approval.Visible = false;

            Btn_Save.Visible = false;
        }
        GetUploadFileData();
        Upload_Button_Enable_For_Faculty();
    }
    public void Upload_Button_Enable_For_Faculty()
    {
        try
        {
            if (Convert.ToInt32(txt_a1_actualtotalactivites.Text) > 0)
            {

                fu_a1.Enabled = true;
            }
            if (Convert.ToInt32(txt_b1_ActualTotalActivities.Text) > 0)
            {
                fu_b1.Enabled = true;
            }
            if (Convert.ToInt32(txt_b2_ActualTotalActivities.Text) > 0)
            {
                fu_b2.Enabled = true;
            }
            if (Convert.ToInt32(txt_b3_ActualTotalActivities.Text) > 0)
            {
                fu_b3.Enabled = true;
            }

            if (Convert.ToInt32(txt_b4_ActualTotalActivities.Text) > 0)
            {
                fu_b4.Enabled = true;
            }

            if (Convert.ToInt32(txt_b5_ActualTotalActivities.Text) > 0)
            {
                fu_b5.Enabled = true;
            }

            if (Convert.ToInt32(txt_b6_ActualTotalActivities.Text) > 0)
            {
                fu_b6.Enabled = true;
            }

            if (Convert.ToInt32(txt_c1_RequiredActivities.Text) > 0)
            {
                fu_c1.Enabled = true;
            }

            if (Convert.ToInt32(txt_c2_RequiredActivities.Text) > 0)
            {
                fu_c2.Enabled = true;
            }

            if (Convert.ToInt32(txt_d1_ActualTotalActivities.Text) > 0)
            {
                fu_d1.Enabled = true;
            }

            if (Convert.ToInt32(txt_d2_ActualTotalActivities.Text) > 0)
            {
                fu_d2.Enabled = true;
            }

            if (Convert.ToInt32(txt_d3_ActualTotalActivities.Text) > 0)
            {
                fu_d3.Enabled = true;
            }

            if (Convert.ToInt32(txt_d4_ActualTotalActivities.Text) > 0)
            {
                fu_d4.Enabled = true;
            }

            if (Convert.ToInt32(txt_d5_ActualTotalActivities.Text) > 0)
            {
                fu_d5.Enabled = true;
            }

            if (Convert.ToInt32(txt_d6_ActualTotalActivities.Text) > 0)
            {
                fu_d6.Enabled = true;
            }

            if (Convert.ToInt32(txt_e1_ScoresPerActivity.Text) > 0)
            {
                fu_e1.Enabled = true;
            }

            if (Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2.Enabled = true;
            }

            if (Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g1.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g2.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g3.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k1.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k2.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l1.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l2.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l3.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m1.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m2.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m3.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m4.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n1.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n2.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3.Text) > 0)
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n3.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1.Text) > 0)
            {
                fu_CriteriaD_Administration_FileUpload_o1.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2.Text) > 0)
            {
                fu_CriteriaD_Administration_FileUpload_o2.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3.Text) > 0)
            {
                fu_CriteriaD_Administration_FileUpload_o3.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4.Text) > 0)
            {
                fu_CriteriaD_Administration_FileUpload_o4.Enabled = true;
            }

            if (Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5.Text) > 0)
            {
                fu_CriteriaD_Administration_FileUpload_o5.Enabled = true;
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please ensure all fields are filled out correctly. Values cannot be empty.');", true);

        }

    }
    public void clear_data()
    {
        //lbl_faculty_name.Text = "";

        ////lbl_emp_code.Text = "";
        //lbl_college_department.Text = "";

        txt_a1_actualtotalactivites.Text = "";
        lbl_a1_apiscore_through_self_assessment.Text = "";
        lbl_a1_final_obtained_value.Text = "";
        txt_a1_reporting_authority_assessment.Text = "";

        txt_b1_ActualTotalActivities.Text = "";
        lbl_b1_APIScoreThroughSelfAssessment.Text = "";
        lbl_b1_FinalObtainedValue.Text = "";
        txt_b1_ReportingAuthorityAssessment.Text = "";

        txt_b2_ActualTotalActivities.Text = "";
        lbl_b2_APIScoreThroughSelfAssessment.Text = "";
        lbl_b2_FinalObtainedValue.Text = "";
        txt_b2_ReportingAuthorityAssessment.Text = "";

        txt_b3_ActualTotalActivities.Text = "";
        lbl_b3_APIScoreThroughSelfAssessment.Text = "";
        lbl_b3_FinalObtainedValue.Text = "";
        txt_b3_ReportingAuthorityAssessment.Text = "";

        txt_b4_ActualTotalActivities.Text = "";
        lbl_b4_APIScoreThroughSelfAssessment.Text = "";
        lbl_b4_FinalObtainedValue.Text = "";
        txt_b4_ReportingAuthorityAssessment.Text = "";

        txt_b5_ActualTotalActivities.Text = "";
        lbl_b5_APIScoreThroughSelfAssessment.Text = "";
        lbl_b5_FinalObtainedValue.Text = "";
        txt_b5_ReportingAuthorityAssessment.Text = "";

        txt_b6_ActualTotalActivities.Text = "";
        lbl_b6_APIScoreThroughSelfAssessment.Text = "";
        lbl_b6_FinalObtainedValue.Text = "";
        txt_b6_ReportingAuthorityAssessment.Text = "";

        txt_c1_RequiredActivities.Text = "";
        lbl_c1_TotalAPIScore.Text = "";
        lbl_c1_FinalScore.Text = "";
        txt_c1_AssessmentByAuthority.Text = "";

        txt_c2_RequiredActivities.Text = "";
        lbl_c2_TotalAPIScore.Text = "";
        lbl_c2_FinalScore.Text = "";
        txt_c2_AssessmentByAuthority.Text = "";

        txt_d1_ActualTotalActivities.Text = "";
        lbl_d1_APIScoreThroughSelfAssessment.Text = "";
        lbl_d1_FinalObtainedValue.Text = "";
        txt_d1_ReportingAuthorityAssessment.Text = "";

        txt_d2_ActualTotalActivities.Text = "";
        lbl_d2_APIScoreThroughSelfAssessment.Text = "";
        lbl_d2_FinalObtainedValue.Text = "";
        txt_d2_ReportingAuthorityAssessment.Text = "";

        txt_d3_ActualTotalActivities.Text = "";
        lbl_d3_APIScoreThroughSelfAssessment.Text = "";
        lbl_d3_FinalObtainedValue.Text = "";
        txt_d3_ReportingAuthorityAssessment.Text = "";

        txt_d4_ActualTotalActivities.Text = "";
        lbl_d4_APIScoreThroughSelfAssessment.Text = "";
        lbl_d4_FinalObtainedValue.Text = "";
        txt_d4_ReportingAuthorityAssessment.Text = "";

        txt_d5_ActualTotalActivities.Text = "";
        lbl_d5_APIScoreThroughSelfAssessment.Text = "";
        lbl_d5_FinalObtainedValue.Text = "";
        txt_d5_ReportingAuthorityAssessment.Text = "";

        txt_d6_ActualTotalActivities.Text = "";
        lbl_d6_APIScoreThroughSelfAssessment.Text = "";
        lbl_d6_FinalObtainedValue.Text = "";
        txt_d6_ReportingAuthorityAssessment.Text = "";

        txt_e1_ScoresPerActivity.Text = "";
        lbl_e1_APIScoreThroughSelfAssessment.Text = "";
        lbl_e1_FinalObtainedValue.Text = "";
        txt_e1_ReportingAuthorityAssessment.Text = "";

        lbl_e1_APIScoreThroughSelfAssessment_Total.Text = "";
        lbl_e1_FinalObtainedValue_Total.Text = "";
        lbl_CriteriaA_AcademicPerformance_Total_Total.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f2.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Text = "";


        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f4.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f4.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Text = "";


        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f5.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f5.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Text = "";


        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f6.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f6.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Text = "";


        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f7.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f7.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g1.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g2.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_g3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_g3.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h1.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h2.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h3.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h4.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h4.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h5.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h5.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h6.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h6.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h7.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h7.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h8.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h8.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_h9.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_h9.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i2.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i3.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i4.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i4.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_i5.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_i5.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j1.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_j2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_j2.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Text = "";

        lbl_api_score_through_self_assessment_total.Text = "";
        lbl_finally_obtained_score_total.Text = "";
        lbl_assessmentby_reportingauthority_total.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k1.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k2.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k3.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k4.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k4.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k5.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k5.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k6.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k6.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k7.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k7.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k8.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k8.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k9.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k9.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k10.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k10.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_k11.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_k11.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l1.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l2.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_l3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_l3.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m1.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m2.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m3.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_m4.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_m4.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n1.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n1.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n2.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n2.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_n3.Text = "";
        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_n3.Text = "";
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Text = "";

        lbl_api_score_through_self_assessment_total_criteriac.Text = "";
        lbl_finally_obtained_score_total_criteriac.Text = "";
        lbl_assessmentby_reportingauthority_total_Criteriac.Text = "";

        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1.Text = "";
        lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o1.Text = "";
        lbl_CriteriaD_Administration_TotalorFaculty_o1.Text = "";
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Text = "";

        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2.Text = "";
        lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o2.Text = "";
        lbl_CriteriaD_Administration_TotalorFaculty_o2.Text = "";
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Text = "";

        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3.Text = "";
        lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o3.Text = "";
        lbl_CriteriaD_Administration_TotalorFaculty_o3.Text = "";
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Text = "";

        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4.Text = "";
        lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o4.Text = "";
        lbl_CriteriaD_Administration_TotalorFaculty_o4.Text = "";
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Text = "";

        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5.Text = "";
        lbl_CriteriaD_Administration_TotalAPIScorethroughSelfAssessment_o5.Text = "";
        lbl_CriteriaD_Administration_TotalorFaculty_o5.Text = "";
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Text = "";

        lbl_api_score_through_self_assessment_total_criteriad.Text = "";
        lbl_finally_obtained_score_total_criteriad.Text = "";
        lbl_assessmentby_reportingauthority_total_Criteriad.Text = "";
        txt_p1_ReportingAuthority.Text = "";
        txt_p2_ReportingAuthority.Text = "";
        txt_p3_ReportingAuthority.Text = "";
        txt_p4_ReportingAuthority.Text = "";
        txt_p5_ReportingAuthority.Text = "";
        txt_p6_ReportingAuthority.Text = "";
        txt_p7_ReportingAuthority.Text = "";
        txt_p8_ReportingAuthority.Text = "";
        txt_p9_ReportingAuthority.Text = "";
        txt_p10_ReportingAuthority.Text = "";

        lbl_p_ReportingAuthority_total.Text = "";
        lbl_facultyfeedback_inpercentage.Text = "";
        lbl_facultyobtained_total.Text = "";

        lbl_apiscorecalculation_criteriaA.Text = "";
        lbl_apiscorecalculation_criteriaB.Text = "";
        lbl_apiscorecalculation_criteriaC.Text = "";
        lbl_apiscorecalculation_criteriaD.Text = "";
        lbl_apiscorecalculation_criteriaE.Text = "";
        lbl_apiscorecalculation_criteriaF.Text = "";

        lbl_hrdepartment_totalapiscore.Text = "";
        lbl_totalAPIScore.Text = "";
        lbl_facultyCategory.Text = "";

        txt_commentsAndsuggestion_a.Text = "";
        txt_commentsAndsuggestion_b.Text = "";
        txt_commentsAndsuggestion_c.Text = "";
        txt_commentsAndsuggestion_d.Text = "";
        txt_commentsAndsuggestion_e.Text = "";

        lbl_hrdepartment_totalapiscore.Text = "";
        lbl_hrdepartment_faculty_categorybasedscore.Text = "";
        txt_hrdepartment_required_improvement_1.Text = "";
        txt_hrdepartment_required_improvement_2.Text = "";
        txt_hrdepartment_required_improvement_3.Text = "";
        txt_hrdepartment_required_improvement_4.Text = "";

        txt_hr_recommendations_a.Text = "";
        txt_hr_recommendations_b.Text = "";
        txt_hr_recommendations_c.Text = "";
        txt_hr_recommendations_d.Text = "";
        txt_hr_recommendations_e.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2.Text = "";

        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1_2.Text = "";


        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f1_2.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2.Text = "";



        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2.Text = "";

        lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3_2.Text = "";

        lbl_CriteriaB_ResearchAndDevelopment_TotalorFaculty_f3_2.Text = "";

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2.Text = "";



    }
    public void assesmentDisable()
    {

        txt_a1_reporting_authority_assessment.Enabled = false;
        txt_b1_ReportingAuthorityAssessment.Enabled = false;
        txt_b2_ReportingAuthorityAssessment.Enabled = false;
        txt_b3_ReportingAuthorityAssessment.Enabled = false;
        txt_b4_ReportingAuthorityAssessment.Enabled = false;
        txt_b5_ReportingAuthorityAssessment.Enabled = false;
        txt_b6_ReportingAuthorityAssessment.Enabled = false;
        txt_c1_AssessmentByAuthority.Enabled = false;
        txt_c2_AssessmentByAuthority.Enabled = false;
        txt_d1_ReportingAuthorityAssessment.Enabled = false;
        txt_d2_ReportingAuthorityAssessment.Enabled = false;
        txt_d3_ReportingAuthorityAssessment.Enabled = false;
        txt_d4_ReportingAuthorityAssessment.Enabled = false;
        txt_d5_ReportingAuthorityAssessment.Enabled = false;
        txt_d6_ReportingAuthorityAssessment.Enabled = false;
        txt_e1_ReportingAuthorityAssessment.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Enabled = false;

        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Enabled = false;
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Enabled = false;
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Enabled = false;
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Enabled = false;
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Enabled = false;

        txt_p1_ReportingAuthority.Enabled = false;
        txt_p2_ReportingAuthority.Enabled = false;
        txt_p3_ReportingAuthority.Enabled = false;
        txt_p4_ReportingAuthority.Enabled = false;
        txt_p5_ReportingAuthority.Enabled = false;
        txt_p6_ReportingAuthority.Enabled = false;
        txt_p7_ReportingAuthority.Enabled = false;
        txt_p8_ReportingAuthority.Enabled = false;
        txt_p9_ReportingAuthority.Enabled = false;
        txt_p10_ReportingAuthority.Enabled = false;

        txt_commentsAndsuggestion_a.Enabled = false;
        txt_commentsAndsuggestion_b.Enabled = false;
        txt_commentsAndsuggestion_c.Enabled = false;
        txt_commentsAndsuggestion_d.Enabled = false;
        txt_commentsAndsuggestion_e.Enabled = false;

    }





    protected void btnCreateNew_Click(object sender, EventArgs e)
    {

        int currentYear = DateTime.Now.Year - 1;

        ddl_academic_session.Text = currentYear.ToString().Substring(2) + "-" + (currentYear + 1).ToString().Substring(2);
        lblMonth.Text = drpMonth.SelectedItem.Text;
        hfmonth.Value = drpMonth.SelectedValue;
        Faculty_Enabledata();
        assesmentDisable();
        clear_data();
        Get_FacultyIn_percentage(lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        pnlcreate.Visible = true;
        pnl_Dashboard.Visible = false;


        ViewState["ApplicableFor"] = "Insert";
        ViewState["ID_For_Updated"] = "";
        lbl_emp_code.Text = Session["uid"].ToString().Trim();
        lbl_faculty_name.Text = Session["Fulname"].ToString().Trim();
        lbl_designation.Text = Session["PMS_Job_Title_Grade_Desc"].ToString().Trim();
        lbl_college_department.Text = Session["EmployeePostingGroupl"].ToString().Trim();
        lbl_New_College.Text = Session["GlobalDimension1Coded"].ToString().Trim();
        lbl_New_Department.Text = Session["PMS_DepartmentName"].ToString().Trim();
        HR_Disable();
        btn_Fu_A1.Visible = true;
        btnAttachmentSave_Fu_A1.Visible = true;
        Enable_Designation_dropdown();

        GetFeedback_Data();

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PMS.aspx");
        //pnlcreate.Visible = false;
        //pnl_Dashboard.Visible = true;
        //Set_Permission();
    }

    protected void txtFilterBy_TextChanged(object sender, EventArgs e)
    {
        sp_Get_PMS_All_Data(dd_AcademicYear.SelectedValue.Trim(), txtFilterBy.Text.Trim(), Session["uid"].ToString().Trim(), Session["Departmentcode"].ToString().Trim(), dd_Staus.SelectedValue.Trim());
    }

    protected void dd_AcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        sp_Get_PMS_All_Data(dd_AcademicYear.SelectedValue.Trim(), txtFilterBy.Text.Trim(), Session["uid"].ToString().Trim(), Session["Departmentcode"].ToString().Trim(), dd_Staus.SelectedValue.Trim());
    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3_2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }

    protected void txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1_2_TextChanged(object sender, EventArgs e)
    {
        calculatedata();
    }


    ///////////





    public void Faculty_Disabledata()
    {
        txt_a1_actualtotalactivites.Enabled = false;
        txt_b1_ActualTotalActivities.Enabled = false;
        txt_b2_ActualTotalActivities.Enabled = false;
        txt_b3_ActualTotalActivities.Enabled = false;
        txt_b4_ActualTotalActivities.Enabled = false;
        txt_b5_ActualTotalActivities.Enabled = false;
        txt_b6_ActualTotalActivities.Enabled = false;
        txt_c1_RequiredActivities.Enabled = false;
        txt_c2_RequiredActivities.Enabled = false;
        txt_d1_ActualTotalActivities.Enabled = false;
        txt_d2_ActualTotalActivities.Enabled = false;
        txt_d3_ActualTotalActivities.Enabled = false;
        txt_d4_ActualTotalActivities.Enabled = false;
        txt_d5_ActualTotalActivities.Enabled = false;
        txt_d6_ActualTotalActivities.Enabled = false;
        txt_e1_ScoresPerActivity.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2.Enabled = false;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3.Enabled = false;

        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1.Enabled = false;
        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2.Enabled = false;
        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3.Enabled = false;
        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4.Enabled = false;
        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2.Enabled = false;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2.Enabled = false;

    }


    public void Faculty_Enabledata()
    {
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2.Enabled = true;

        txt_a1_actualtotalactivites.Enabled = true;
        txt_b1_ActualTotalActivities.Enabled = true;
        txt_b2_ActualTotalActivities.Enabled = true;
        txt_b3_ActualTotalActivities.Enabled = true;
        txt_b4_ActualTotalActivities.Enabled = true;
        txt_b5_ActualTotalActivities.Enabled = true;
        txt_b6_ActualTotalActivities.Enabled = true;
        txt_c1_RequiredActivities.Enabled = true;
        txt_c2_RequiredActivities.Enabled = true;
        txt_d1_ActualTotalActivities.Enabled = true;
        txt_d2_ActualTotalActivities.Enabled = true;
        txt_d3_ActualTotalActivities.Enabled = true;
        txt_d4_ActualTotalActivities.Enabled = true;
        txt_d5_ActualTotalActivities.Enabled = true;
        txt_d6_ActualTotalActivities.Enabled = true;
        txt_e1_ScoresPerActivity.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3.Enabled = true;

        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1.Enabled = true;
        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2.Enabled = true;
        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3.Enabled = true;
        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4.Enabled = true;
        txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5.Enabled = true;
    }

    public void assesmentEnable()
    {
        txt_a1_reporting_authority_assessment.Enabled = true;
        txt_b1_ReportingAuthorityAssessment.Enabled = true;
        txt_b2_ReportingAuthorityAssessment.Enabled = true;
        txt_b3_ReportingAuthorityAssessment.Enabled = true;
        txt_b4_ReportingAuthorityAssessment.Enabled = true;
        txt_b5_ReportingAuthorityAssessment.Enabled = true;
        txt_b6_ReportingAuthorityAssessment.Enabled = true;
        txt_c1_AssessmentByAuthority.Enabled = true;
        txt_c2_AssessmentByAuthority.Enabled = true;
        txt_d1_ReportingAuthorityAssessment.Enabled = true;
        txt_d2_ReportingAuthorityAssessment.Enabled = true;
        txt_d3_ReportingAuthorityAssessment.Enabled = true;
        txt_d4_ReportingAuthorityAssessment.Enabled = true;
        txt_d5_ReportingAuthorityAssessment.Enabled = true;
        txt_d6_ReportingAuthorityAssessment.Enabled = true;
        txt_e1_ReportingAuthorityAssessment.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f3.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f4.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f5.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f6.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f7.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_g3.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h3.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h4.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h5.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h6.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h7.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h8.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_h9.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i3.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i4.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_i5.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_j2.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k3.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k4.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k5.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k6.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k7.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k8.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k9.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k10.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_k11.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_l3.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m3.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_m4.Enabled = true;

        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n1.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n2.Enabled = true;
        txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_n3.Enabled = true;

        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o1.Enabled = true;
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o2.Enabled = true;
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o3.Enabled = true;
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o4.Enabled = true;
        txt_CriteriaD_Administration_AssessmentbyReportingAuthority_o5.Enabled = true;

        txt_p1_ReportingAuthority.Enabled = true;
        txt_p2_ReportingAuthority.Enabled = true;
        txt_p3_ReportingAuthority.Enabled = true;
        txt_p4_ReportingAuthority.Enabled = true;
        txt_p5_ReportingAuthority.Enabled = true;
        txt_p6_ReportingAuthority.Enabled = true;
        txt_p7_ReportingAuthority.Enabled = true;
        txt_p8_ReportingAuthority.Enabled = true;
        txt_p9_ReportingAuthority.Enabled = true;
        txt_p10_ReportingAuthority.Enabled = true;

    }


    public void HR_Enable()
    {
        txt_hrdepartment_required_improvement_1.Enabled = true;
        txt_hrdepartment_required_improvement_2.Enabled = true;
        txt_hrdepartment_required_improvement_3.Enabled = true;
        txt_hrdepartment_required_improvement_4.Enabled = true;
        txt_hr_recommendations_a.Enabled = true;
        txt_hr_recommendations_b.Enabled = true;
        txt_hr_recommendations_c.Enabled = true;
        txt_hr_recommendations_d.Enabled = true;
        txt_hr_recommendations_e.Enabled = true;

    }

    public void HR_Disable()
    {
        txt_hrdepartment_required_improvement_1.Enabled = false;
        txt_hrdepartment_required_improvement_2.Enabled = false;
        txt_hrdepartment_required_improvement_3.Enabled = false;
        txt_hrdepartment_required_improvement_4.Enabled = false;
        txt_hr_recommendations_a.Enabled = false;
        txt_hr_recommendations_b.Enabled = false;
        txt_hr_recommendations_c.Enabled = false;
        txt_hr_recommendations_d.Enabled = false;
        txt_hr_recommendations_e.Enabled = false;

    }
    public void Get_FacultyIn_percentage(string FacultyCode, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.sp_Get_PMS_Faculty_FeedBack_In_Perntage(FacultyCode.Trim(), AcademicYear.Trim());
        dr.Read();
        if (dr.HasRows)
        {
            lbl_facultyfeedback_inpercentage.Text = dr["FacultyInPercentage"].ToString();

            lbl_ff_even_sem.Text = dr["Feed_Even_Per"].ToString().Trim();
            lbl_ff_odd_sem.Text = dr["Feed_Odd_Per"].ToString().Trim();
            if (lbl_ff_even_sem.Text.Trim() == "")
            {
                lbl_ff_even_sem.Text = "0";
            }
            if (lbl_ff_odd_sem.Text.Trim() == "")
            {
                lbl_ff_odd_sem.Text = "0";
            }

            if (lbl_facultyfeedback_inpercentage.Text.Trim() == "")
            {
                lbl_facultyfeedback_inpercentage.Text = "0";
            }
        }
        else
        {
            lbl_facultyfeedback_inpercentage.Text = "0";
        }
        dr.Close();
        con.DisConnect();
    }
    protected void btnPDFView_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        Session["PMS_PDF_VIEW_ID"] = id;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('PMS_Report.aspx','_newtab');", true);

    }


    private void DownloadFile(int autoNo)
    {
        pms_connection con = new pms_connection();

        SqlCommand cmd = new SqlCommand("sp_GetAttachmentByID_For_Download_PMS", con.Con);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@AutoNo", autoNo);

        con.Connect();
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                string fileName = reader["Attachment_FileName"].ToString();
                string fileType = reader["Attachment_FileType"].ToString();
                byte[] fileData = (byte[])reader["Attachment_Data"];

                Response.Clear();
                Response.ContentType = fileType;
                //Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));

                Response.OutputStream.Write(fileData, 0, fileData.Length);
                Response.Flush();
                Response.End();
            }
            reader.Close();
        }

    }

    public void Get_A_1_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gv_a1.DataSource = dt;
        gv_a1.DataBind();
        dr.Close();
        con.DisConnect();
    }

    public void Get_Common_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvAttachment.DataSource = dt;
        gvAttachment.DataBind();
        dr.Close();
        con.DisConnect();
    }



    public void Get_A_2_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvData.DataSource = dt;
        gvData.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_A_3_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvF3.DataSource = dt;
        gvF3.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_A_4_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvF4.DataSource = dt;
        gvF4.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_A_5_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvF5.DataSource = dt;
        gvF5.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public SqlDataReader sp_GetAttachmentByApplicable_for_PMS(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        SqlCommand cmd = new SqlCommand("sp_GetAttachmentByApplicable_for_PMS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Applicable_For", Applicable_For.Trim());
        cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID.Trim());
        cmd.Parameters.AddWithValue("@AcademicYear", AcademicYear.Trim());
        cmd.Parameters.AddWithValue("@Month", hfmonth.Value);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }



    protected void lnk_A1_Delete_Command(object sender, CommandEventArgs e)
    {
        string AutoNo = e.CommandArgument.ToString();
        pms_connection con = new pms_connection();
        con.sp_Delete_Attachment_PMS(AutoNo);
        con.DisConnect();
        Get_A_1_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        GetUploadFileData();
        md_FileUpload.Show();
    }

    protected void lnk_A1_Downalod_Command(object sender, CommandEventArgs e)
    {
        string autoNo = e.CommandArgument.ToString();
        DownloadFile(Convert.ToInt32(autoNo));
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!fuAttachment.HasFile)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                "alert('Please select an attachment.');", true);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtBriefDescription.Text))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                "alert('Please enter brief description.');", true);
            return;
        }



        if (fuAttachment.HasFile)
        {
            string fileExtension = System.IO.Path.GetExtension(fuAttachment.FileName).ToLower();


            string[] allowedExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };


            if (allowedExtensions.Contains(fileExtension))
            {
                if (fuAttachment.PostedFile.ContentLength <= 204800) // 200 KB file size check
                {
                    string fileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                    string fileType = Path.GetExtension(fileName);
                    byte[] fileData = fuAttachment.FileBytes;
                    pms_connection con = new pms_connection();
                    SqlCommand cmd = new SqlCommand("sp_InsertAttachment_PMS_back", con.Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReferenceID", 123); // example ReferenceID
                    cmd.Parameters.AddWithValue("@Attachment_FileName", fileName);
                    cmd.Parameters.AddWithValue("@Attachment_FileType", fileType);
                    cmd.Parameters.AddWithValue("@Attachment_Data", fileData);
                    cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
                    cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
                    cmd.Parameters.AddWithValue("@Applicable_For", lblApplicablefor.Text.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
                    cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
                    cmd.Parameters.AddWithValue("@month", hfmonth.Value);
                    cmd.Parameters.AddWithValue("@desc", txtBriefDescription.Text);

                    con.Connect();
                    cmd.ExecuteNonQuery();
                    con.DisConnect();
                    Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
                    GetUploadFileData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('File size must be less than or equal to 200 KB.');", true);
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only PDF and image files are allowed.');", true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select a file to upload.');", true);

        }
        md_FileUpload.Show();
    }

    protected void btn_Fu_A1_Click(object sender, EventArgs e)
    {
        VisibleFalse("a_1");
        lblApplicablefor.Text = "a_1";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();

    }

    protected void fu_b1_Click(object sender, EventArgs e)
    {
        VisibleFalse("b_1");
        lblApplicablefor.Text = "b_1";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();

    }


    protected void fu_b3_Click(object sender, EventArgs e)
    {
        VisibleFalse("b_3");
        lblApplicablefor.Text = "b_3";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_b2_Click(object sender, EventArgs e)
    {
        VisibleFalse("b_2");
        lblApplicablefor.Text = "b_2";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_b4_Click(object sender, EventArgs e)
    {
        VisibleFalse("b_4");
        lblApplicablefor.Text = "b_4";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_b5_Click(object sender, EventArgs e)
    {
        VisibleFalse("b_5");
        lblApplicablefor.Text = "b_5";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_b6_Click(object sender, EventArgs e)
    {
        VisibleFalse("b_6");
        lblApplicablefor.Text = "b_6";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_c1_Click(object sender, EventArgs e)
    {
        VisibleFalse("c_1");
        lblApplicablefor.Text = "c_1";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_c2_Click(object sender, EventArgs e)
    {
        VisibleFalse("c_2");
        lblApplicablefor.Text = "c_2";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_d1_Click(object sender, EventArgs e)
    {
        VisibleFalse("d_1");
        lblApplicablefor.Text = "d_1";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_d2_Click(object sender, EventArgs e)
    {
        VisibleFalse("d_2");
        lblApplicablefor.Text = "d_2";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_d3_Click(object sender, EventArgs e)
    {
        VisibleFalse("d_3");
        lblApplicablefor.Text = "d_3";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_d4_Click(object sender, EventArgs e)
    {
        VisibleFalse("d_4");
        lblApplicablefor.Text = "d_4";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_d5_Click(object sender, EventArgs e)
    {
        VisibleFalse("d_5");
        lblApplicablefor.Text = "d_5";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_d6_Click(object sender, EventArgs e)
    {
        VisibleFalse("d_6");
        lblApplicablefor.Text = "d_6";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_e1_Click(object sender, EventArgs e)
    {
        VisibleFalse("e_1");
        lblApplicablefor.Text = "e_1";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }
    public void VisibleFalse(string id)
    {
        Control[] controls =
        {
        F_1, F_2, F_3, F_4, F_5, F_6, F_7,
        h_1, h_2, h_3, h_4, h_5, h_6, h_7, h_8, h_9,
        i_1, i_2, i_3, i_4, i_5,
        j_1, j_2,
        k_3, k_4, k_5, k_6, k_7, k_8, k_9, k_10, k_11
    };

        bool found = false;

        foreach (Control ctrl in controls)
        {
            if (ctrl.ID.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                ctrl.Visible = true;
                found = true;
            }
            else
            {
                ctrl.Visible = false;
            }
        }

        // Agar passed ID upar wale controls me nahi mili
        divcommon.Visible = !found;
    }
    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "f_1";
        Get_A_1_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("F_1");

        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "f_2";
        Get_A_2_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("F_2");

        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "f_3";
        Get_A_3_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("F_3");

        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "f_4";
        Get_A_4_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("F_4");

        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "f_5";
        Get_A_5_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("F_5");

        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "f_6";
        Get_A_6_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("F_6");

        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "f_7";
        Get_A_7_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("F_7");

        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_g1_Click(object sender, EventArgs e)
    {
        VisibleFalse("g_1");
        lblApplicablefor.Text = "g_1";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_g2_Click(object sender, EventArgs e)
    {
        VisibleFalse("g_2");
        lblApplicablefor.Text = "g_2";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_g3_Click(object sender, EventArgs e)
    {
        VisibleFalse("g_3");
        lblApplicablefor.Text = "g_3";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1_Click(object sender, EventArgs e)
    {

        lblApplicablefor.Text = "h_1";
        Get_h_1_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("h_1");
        h_1.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "h_2";
        Get_h_2_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("h_2");
        h_2.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "h_3";
        Get_h_3_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("h_3");
        h_3.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "h_4";
        Get_h_4_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("h_4");
        h_4.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "h_5";
        Get_h_5_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("h_5");
        h_5.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "h_6";
        Get_h_6_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("h_6");
        h_6.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "h_7";
        Get_h_7_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("h_7");
        h_7.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "h_8";
        Get_h_8_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("h_8");
        h_8.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "h_9";
        Get_h_9_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("h_9");
        h_9.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "i_1";
        Get_i_1_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("i_1");
        i_1.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "i_2";
        Get_i_2_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("i_2");
        i_2.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3_Click(object sender, EventArgs e)
    {

        lblApplicablefor.Text = "i_3";
        Get_i_3_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("i_3");
        i_3.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4_Click(object sender, EventArgs e)
    {

        lblApplicablefor.Text = "i_4";
        Get_i_4_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("i_4");
        i_4.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5_Click(object sender, EventArgs e)
    {

        lblApplicablefor.Text = "i_5";
        Get_i_5_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("i_5");
        i_5.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1_Click(object sender, EventArgs e)
    {

        lblApplicablefor.Text = "j_1";
        Get_j_1_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("j_1");
        j_1.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2_Click(object sender, EventArgs e)
    {

        lblApplicablefor.Text = "j_2";
        Get_j_2_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("j_2");
        j_2.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_k1_Click(object sender, EventArgs e)
    {
        VisibleFalse("k_1");
        lblApplicablefor.Text = "k_1";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_k2_Click(object sender, EventArgs e)
    {
        VisibleFalse("k_2");
        lblApplicablefor.Text = "k_2";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "k_3";
        Get_k_3_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("k_3");
        k_3.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "k_4";
        Get_k_4_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("k_4");
        k_4.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "k_5";
        Get_k_5_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("k_5");
        k_5.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "k_6";
        Get_k_6_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("k_6");
        k_6.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "k_7";
        Get_k_7_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("k_7");
        k_7.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "k_8";
        Get_k_8_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("k_8");
        k_8.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "k_9";
        Get_k_9_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("k_9");
        k_9.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "k_10";
        Get_k_10_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("k_10");
        k_10.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11_Click(object sender, EventArgs e)
    {
        lblApplicablefor.Text = "k_11";
        Get_k_11_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        VisibleFalse("k_11");
        k_11.Visible = true;
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_l1_Click(object sender, EventArgs e)
    {
        VisibleFalse("l_1");
        lblApplicablefor.Text = "l_1";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_l2_Click(object sender, EventArgs e)
    {
        VisibleFalse("l_2");
        lblApplicablefor.Text = "l_2";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_l3_Click(object sender, EventArgs e)
    {
        VisibleFalse("l_3");
        lblApplicablefor.Text = "l_3";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_m1_Click(object sender, EventArgs e)
    {
        VisibleFalse("m_1");
        lblApplicablefor.Text = "m_1";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_m2_Click(object sender, EventArgs e)
    {
        VisibleFalse("m_2");
        lblApplicablefor.Text = "m_2";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_m3_Click(object sender, EventArgs e)
    {
        VisibleFalse("m_3");
        lblApplicablefor.Text = "m_3";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_m4_Click(object sender, EventArgs e)
    {
        VisibleFalse("m_4");
        lblApplicablefor.Text = "m_4";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_n1_Click(object sender, EventArgs e)
    {
        VisibleFalse("n_1");
        lblApplicablefor.Text = "n_1";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_n2_Click(object sender, EventArgs e)
    {
        VisibleFalse("n_2");
        lblApplicablefor.Text = "n_2";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaB_ResearchAndDevelopment_FileUpload_n3_Click(object sender, EventArgs e)
    {

        VisibleFalse("n_3");
        lblApplicablefor.Text = "n_3";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaD_Administration_FileUpload_o1_Click(object sender, EventArgs e)
    {
        VisibleFalse("o_1");
        lblApplicablefor.Text = "o_1";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaD_Administration_FileUpload_o2_Click(object sender, EventArgs e)
    {
        VisibleFalse("o_2");
        lblApplicablefor.Text = "o_2";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaD_Administration_FileUpload_o3_Click(object sender, EventArgs e)
    {
        VisibleFalse("o_3");
        lblApplicablefor.Text = "o_3";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaD_Administration_FileUpload_o4_Click(object sender, EventArgs e)
    {
        VisibleFalse("o_4");
        lblApplicablefor.Text = "o_4";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    protected void fu_CriteriaD_Administration_FileUpload_o5_Click(object sender, EventArgs e)
    {
        VisibleFalse("o_5");
        lblApplicablefor.Text = "o_5";
        Get_Common_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        md_FileUpload.Show();
    }

    public void GetUploadFileData()
    {
        string ApplicableFor;
        pms_connection con = new pms_connection();
        con.Connect();
        SqlDataAdapter da = new SqlDataAdapter("sp_Check_File_Upload_In_PMS", con.Con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
        da.SelectCommand.Parameters.AddWithValue("@Employee_Code", lbl_emp_code.Text.Trim());
        da.SelectCommand.Parameters.AddWithValue("@month", hfmonth.Value);
        DataSet ds = new DataSet();
        da.Fill(ds, "sp_Check_File_Upload_In_PMS");
        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {

            ApplicableFor = ds.Tables[0].Rows[i]["Applicable_For"].ToString();
            if (ApplicableFor == "a_1")
            {
                fu_a1.Text = "View/ Download";
                fu_a1.Enabled = true;

            }

            if (ApplicableFor == "b_1")
            {
                fu_b1.Text = "View/ Download";
                fu_b1.Enabled = true;

            }
            if (ApplicableFor == "b_2")
            {
                fu_b2.Text = "View/ Download";
                fu_b2.Enabled = true;

            }
            if (ApplicableFor == "b_3")
            {
                fu_b3.Text = "View/ Download";
                fu_b3.Enabled = true;

            }
            if (ApplicableFor == "b_4")
            {
                fu_b4.Text = "View/ Download";
                fu_b4.Enabled = true;

            }
            if (ApplicableFor == "b_5")
            {
                fu_b5.Text = "View/ Download";
                fu_b5.Enabled = true;

            }
            if (ApplicableFor == "b_6")
            {
                fu_b6.Text = "View/ Download";
                fu_b6.Enabled = true;

            }
            if (ApplicableFor == "c_1")
            {
                fu_c1.Text = "View/ Download";
                fu_c1.Enabled = true;

            }
            if (ApplicableFor == "c_2")
            {
                fu_c2.Text = "View/ Download";
                fu_c2.Enabled = true;

            }
            if (ApplicableFor == "d_1")
            {
                fu_d1.Text = "View/ Download";
                fu_d1.Enabled = true;
            }
            if (ApplicableFor == "d_2")
            {
                fu_d2.Text = "View/ Download";
                fu_d2.Enabled = true;

            }
            if (ApplicableFor == "d_3")
            {
                fu_d3.Text = "View/ Download";
                fu_d3.Enabled = true;

            }
            if (ApplicableFor == "d_4")
            {
                fu_d4.Text = "View/ Download";
                fu_d4.Enabled = true;

            }
            if (ApplicableFor == "d_5")
            {
                fu_d5.Text = "View/ Download";
                fu_d5.Enabled = true;

            }
            if (ApplicableFor == "d_6")
            {
                fu_d6.Text = "View/ Download";
                fu_d6.Enabled = true;

            }
            if (ApplicableFor == "e_1")
            {
                fu_e1.Text = "View/ Download";
                fu_e1.Enabled = true;

            }
            if (ApplicableFor == "f_1")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1.Enabled = true;
            }
            if (ApplicableFor == "f_2")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2.Enabled = true;

            }
            if (ApplicableFor == "f_3")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3.Enabled = true;

            }
            if (ApplicableFor == "f_4")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4.Enabled = true;

            }
            if (ApplicableFor == "f_5")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5.Enabled = true;

            }
            if (ApplicableFor == "f_6")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6.Enabled = true;

            }
            if (ApplicableFor == "f_7")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7.Enabled = true;

            }
            if (ApplicableFor == "g_1")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g1.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g1.Enabled = true;

            }
            if (ApplicableFor == "g_2")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g2.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g2.Enabled = true;
            }
            if (ApplicableFor == "g_3")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g3.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_g3.Enabled = true;

            }
            if (ApplicableFor == "h_1")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1.Enabled = true;

            }
            if (ApplicableFor == "h_2")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2.Enabled = true;

            }
            if (ApplicableFor == "h_3")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3.Enabled = true;

            }
            if (ApplicableFor == "h_4")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4.Enabled = true;

            }
            if (ApplicableFor == "h_5")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5.Enabled = true;

            }
            if (ApplicableFor == "h_6")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6.Enabled = true;

            }
            if (ApplicableFor == "h_7")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7.Enabled = true;

            }
            if (ApplicableFor == "h_8")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8.Enabled = true;

            }
            if (ApplicableFor == "h_9")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9.Enabled = true;

            }
            if (ApplicableFor == "i_1")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1.Enabled = true;

            }
            if (ApplicableFor == "i_2")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2.Enabled = true;

            }
            if (ApplicableFor == "i_3")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3.Enabled = true;

            }
            if (ApplicableFor == "i_4")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4.Enabled = true;

            }
            if (ApplicableFor == "i_5")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5.Enabled = true;

            }
            if (ApplicableFor == "j_1")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1.Enabled = true;

            }
            if (ApplicableFor == "j_2")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2.Enabled = true;

            }
            if (ApplicableFor == "k_1")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k1.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k1.Enabled = true;

            }
            if (ApplicableFor == "k_2")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k2.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k2.Enabled = true;

            }
            if (ApplicableFor == "k_3")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3.Enabled = true;

            }
            if (ApplicableFor == "k_4")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4.Enabled = true;

            }
            if (ApplicableFor == "k_5")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5.Enabled = true;

            }
            if (ApplicableFor == "k_6")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6.Enabled = true;

            }
            if (ApplicableFor == "k_7")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7.Enabled = true;

            }
            if (ApplicableFor == "k_8")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8.Enabled = true;

            }
            if (ApplicableFor == "k_9")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9.Enabled = true;

            }
            if (ApplicableFor == "k_10")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10.Enabled = true;

            }
            if (ApplicableFor == "k_11")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11.Enabled = true;

            }
            if (ApplicableFor == "l_1")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l1.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l1.Enabled = true;

            }
            if (ApplicableFor == "l_2")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l2.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l2.Enabled = true;

            }
            if (ApplicableFor == "l_3")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l3.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_l3.Enabled = true;

            }
            if (ApplicableFor == "m_1")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m1.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m1.Enabled = true;

            }
            if (ApplicableFor == "m_2")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m2.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m2.Enabled = true;

            }
            if (ApplicableFor == "m_3")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m3.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m3.Enabled = true;

            }
            if (ApplicableFor == "m_4")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m4.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_m4.Enabled = true;

            }
            if (ApplicableFor == "n_1")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n1.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n1.Enabled = true;

            }
            if (ApplicableFor == "n_2")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n2.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n2.Enabled = true;

            }
            if (ApplicableFor == "n_3")
            {
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n3.Text = "View/ Download";
                fu_CriteriaB_ResearchAndDevelopment_FileUpload_n3.Enabled = true;

            }
            if (ApplicableFor == "o_1")
            {
                fu_CriteriaD_Administration_FileUpload_o1.Text = "View/ Download";
                fu_CriteriaD_Administration_FileUpload_o1.Enabled = true;

            }
            if (ApplicableFor == "o_2")
            {
                fu_CriteriaD_Administration_FileUpload_o2.Text = "View/ Download";
                fu_CriteriaD_Administration_FileUpload_o2.Enabled = true;

            }
            if (ApplicableFor == "o_3")
            {
                fu_CriteriaD_Administration_FileUpload_o3.Text = "View/ Download";
                fu_CriteriaD_Administration_FileUpload_o3.Enabled = true;

            }
            if (ApplicableFor == "o_4")
            {
                fu_CriteriaD_Administration_FileUpload_o4.Text = "View/ Download";
                fu_CriteriaD_Administration_FileUpload_o4.Enabled = true;

            }
            if (ApplicableFor == "o_5")
            {
                fu_CriteriaD_Administration_FileUpload_o5.Text = "View/ Download";
                fu_CriteriaD_Administration_FileUpload_o5.Enabled = true;

            }
        }
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('PMS_Report.aspx','_newtab');", true);

    }

    public void Send_For_Approval()
    {
        try
        {
            if (Convert.ToInt32(txt_a1_actualtotalactivites.Text) > 0 && fu_a1.Text == "Upload File")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in a.1');", true);
            }
            else if (Convert.ToInt32(txt_b1_ActualTotalActivities.Text) > 0 && fu_b1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in b.1');", true);
            }
            else if (Convert.ToInt32(txt_b2_ActualTotalActivities.Text) > 0 && fu_b2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in b.2');", true);
            }
            else if (Convert.ToInt32(txt_b3_ActualTotalActivities.Text) > 0 && fu_b3.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in b.3');", true);
            }
            else if (Convert.ToInt32(txt_b4_ActualTotalActivities.Text) > 0 && fu_b4.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in b.4');", true);
            }
            else if (Convert.ToInt32(txt_b5_ActualTotalActivities.Text) > 0 && fu_b5.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in b.5');", true);
            }
            else if (Convert.ToInt32(txt_b6_ActualTotalActivities.Text) > 0 && fu_b6.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in b.6');", true);
            }
            else if (Convert.ToInt32(txt_c1_RequiredActivities.Text) > 0 && fu_c1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in c.1');", true);
            }
            else if (Convert.ToInt32(txt_c2_RequiredActivities.Text) > 0 && fu_c2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in c.2');", true);
            }
            else if (Convert.ToInt32(txt_d1_ActualTotalActivities.Text) > 0 && fu_d1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in d.1');", true);
            }
            else if (Convert.ToInt32(txt_d2_ActualTotalActivities.Text) > 0 && fu_d2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in d.2');", true);
            }
            else if (Convert.ToInt32(txt_d3_ActualTotalActivities.Text) > 0 && fu_d3.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in d.3');", true);
            }
            else if (Convert.ToInt32(txt_d4_ActualTotalActivities.Text) > 0 && fu_d4.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in d.4');", true);
            }
            else if (Convert.ToInt32(txt_d5_ActualTotalActivities.Text) > 0 && fu_d5.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in d.5');", true);
            }
            else if (Convert.ToInt32(txt_d6_ActualTotalActivities.Text) > 0 && fu_d6.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in d.6');", true);
            }
            else if (Convert.ToInt32(txt_e1_ScoresPerActivity.Text) > 0 && fu_e1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in e.1');", true);
            }
            else if (Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f1.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in f.1');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in f.2');", true);
            }
            else if (Convert.ToInt32(lbl_CriteriaB_ResearchAndDevelopment_TotalAPIScorethroughSelfAssessment_f3.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in f.3');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in f.4');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in f.5');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in f.6');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in f.7');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g1.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_g1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in g.1');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g2.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_g2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in g.2');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_g3.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_g3.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in g.3');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in h.1');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in h.2');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in h.3');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in h.4');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in h.5');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in h.6');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in h.7');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in h.8');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in h.9');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in i.1');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in i.2');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in i.3');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in i.4');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in i.5');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in j.1');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in j.2');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k1.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_k1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in k.1');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k2.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_k2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in k.2');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in k.3');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in k.4');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in k.5');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in k.6');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in k.7');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in k.8');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in k.9');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in k.10');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in k.11');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l1.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_l1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in l.1');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l2.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_l2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in l.2');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_l3.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_l3.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in l.3');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m1.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_m1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in m.1');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m2.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_m2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in m.2');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m3.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_m3.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in m.3');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_m4.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_m4.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in m.4');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n1.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_n1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in n.1');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n2.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_n2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in n.2');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_n3.Text) > 0 && fu_CriteriaB_ResearchAndDevelopment_FileUpload_n3.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in n.3');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o1.Text) > 0 && fu_CriteriaD_Administration_FileUpload_o1.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in o.1');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o2.Text) > 0 && fu_CriteriaD_Administration_FileUpload_o2.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in o.2');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o3.Text) > 0 && fu_CriteriaD_Administration_FileUpload_o3.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in o.3');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o4.Text) > 0 && fu_CriteriaD_Administration_FileUpload_o4.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in o.4');", true);
            }
            else if (Convert.ToInt32(txt_CriteriaD_Administration_RequiredActivitiesInNumbers_o5.Text) > 0 && fu_CriteriaD_Administration_FileUpload_o5.Text == "Upload File")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload File in o.5');", true);
            }

            else
            {
                pms_connection con = new pms_connection();
                string ApplicableFor = ViewState["Applicable_For_Department"].ToString().Trim();
                con.SP_PMS_Send_For_Approval(ViewState["ID_For_Updated"].ToString(), ApplicableFor.Trim(), Session["uid"].ToString().Trim(), Session["Fulname"].ToString().Trim(), System.DateTime.Now.ToString("dd MMM yyyy HH:mm"));
                con.DisConnect();
                sp_Get_PMS_All_Data(dd_AcademicYear.SelectedValue.Trim(), txtFilterBy.Text.Trim(), Session["uid"].ToString().Trim(), Session["Departmentcode"].ToString().Trim(), dd_Staus.SelectedValue.Trim());
                pnlcreate.Visible = false;
                pnl_Dashboard.Visible = true;
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please ensure all fields are filled out correctly. Values cannot be empty.');", true);

        }

    }


    protected void Btn_Approval_Click(object sender, EventArgs e)
    {
        Save_InDraft();
        Send_For_Approval();

    }

    protected void grd_Data_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Data.PageIndex = e.NewPageIndex;
        sp_Get_PMS_All_Data(dd_AcademicYear.SelectedValue.Trim(), txtFilterBy.Text.Trim(), Session["uid"].ToString().Trim(), Session["Departmentcode"].ToString().Trim(), dd_Staus.SelectedValue.Trim());
    }

    [WebMethod]
    public static string UploadFiless()
    {
        HttpContext context = HttpContext.Current;
        if (context.Request.Files.Count > 0)
        {
            try
            {
                // Get the file from the request
                HttpPostedFile file = context.Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    // Define the file path to save
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = context.Server.MapPath("~/Uploads/") + fileName;

                    // Save the file to the specified path
                    file.SaveAs(filePath);

                    // Save file details to the database
                    //string connectionString = "your_connection_string_here";
                    //using (SqlConnection conn = new SqlConnection(connectionString))
                    //{
                    //    conn.Open();
                    //    string query = "INSERT INTO FileUploads (FileName, FilePath, UploadDate) VALUES (@FileName, @FilePath, @UploadDate)";
                    //    using (SqlCommand cmd = new SqlCommand(query, conn))
                    //    {
                    //        cmd.Parameters.AddWithValue("@FileName", fileName);
                    //        cmd.Parameters.AddWithValue("@FilePath", filePath);
                    //        cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now);
                    //        cmd.ExecuteNonQuery();
                    //    }
                    //}

                    // Return success message
                    return "File uploaded and saved to database successfully!";
                }
                else
                {
                    return "No file selected.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        else
        {
            return "No file uploaded.";
        }
    }
    protected void dd_Staus_SelectedIndexChanged(object sender, EventArgs e)
    {
        sp_Get_PMS_All_Data(dd_AcademicYear.SelectedValue.Trim(), txtFilterBy.Text.Trim(), Session["uid"].ToString().Trim(), Session["Departmentcode"].ToString().Trim(), dd_Staus.SelectedValue.Trim());
    }
    protected void CloseModal(object sender, EventArgs e)
    {

        // Optionally handle OK button click and close modal
        md_FileUpload.Hide();
    }
    protected void CloseModal_Employee_Details(object sender, EventArgs e)
    {

        // Optionally handle OK button click and close modal
        md_Employee_Count_Details.Hide();
    }
    public void SaveAttachment()
    {

        if (drpResearchIncentive.SelectedValue == "1")
        {
            if (txttitlePaper.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Title of the Paper')", true);
                return;
            }
            if (txtNameofJournal.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Name of the Journal')", true);
                return;
            }
            if (txtISSNno.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter ISSN No.')", true);
                return;
            }
            if (txtLOA.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Link of Article')", true);
                return;
            }


        }
        if (drpResearchIncentive.SelectedValue == "2")
        {
            if (txttitlePaper.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Title')", true);
                md_FileUpload.Show();
                return;
            }
            if (txtNameofJournal.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Application No.')", true);
                md_FileUpload.Show();
                return;
            }

            if (txtDOP.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Date of Publication')", true);
                md_FileUpload.Show();
                return;
            }
        }
        if (Convert.ToInt32(drpResearchIncentive.SelectedValue) > 2)
        {
            if (txttitlePaper.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Title')", true);
                md_FileUpload.Show();
                return;
            }
            if (txtNameofJournal.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Name of the Book/Publisher')", true);
                md_FileUpload.Show();
                return;
            }
            if (txtDOP.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Date of Publication')", true);
                md_FileUpload.Show();
                return;
            }
            if (txtISSNno.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter ISBN No.')", true);
                md_FileUpload.Show();
                return;
            }

        }


        if (btn_Fu_A1.HasFile)
        {
            string fileExtension = System.IO.Path.GetExtension(btn_Fu_A1.FileName).ToLower();

            // Define allowed extensions
            string[] allowedExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };

            // Check if the file extension is allowed
            if (allowedExtensions.Contains(fileExtension))
            {
                if (btn_Fu_A1.PostedFile.ContentLength <= 204800) // 200 KB file size check
                {
                    string fileName = Path.GetFileName(btn_Fu_A1.PostedFile.FileName);
                    string fileType = Path.GetExtension(fileName);
                    byte[] fileData = btn_Fu_A1.FileBytes;
                    pms_connection con = new pms_connection();
                    SqlCommand cmd = new SqlCommand("sp_InsertAttachment_PMS", con.Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReferenceID", 123); // example ReferenceID
                    cmd.Parameters.AddWithValue("@Attachment_FileName", fileName);
                    cmd.Parameters.AddWithValue("@Attachment_FileType", fileType);
                    cmd.Parameters.AddWithValue("@Attachment_Data", fileData);
                    cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
                    cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
                    cmd.Parameters.AddWithValue("@Applicable_For", lblApplicablefor.Text.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
                    cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
                    cmd.Parameters.AddWithValue("@type_of_research", drpResearchIncentive.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@title_of_paper", txttitlePaper.Text);
                    cmd.Parameters.AddWithValue("@Name_of_journal", txtNameofJournal.Text);
                    cmd.Parameters.AddWithValue("@Volume", txtVolume.Text);
                    cmd.Parameters.AddWithValue("@No_Issue", txtISSNno.Text);
                    cmd.Parameters.AddWithValue("@PageNo", txtPageNop.Text.ToString());
                    cmd.Parameters.AddWithValue("@ISSNno", txtISSNno.Text.ToString());
                    cmd.Parameters.AddWithValue("@Date_Of_Publication", txtDOP.Text);
                    cmd.Parameters.AddWithValue("@No_of_author", txtNumberOfAuthor.Text);
                    cmd.Parameters.AddWithValue("@Link_of_Author", txtLOA.Text);
                    cmd.Parameters.AddWithValue("@month", hfmonth.Value);
                    cmd.Parameters.AddWithValue("@ActivityType", 2);
                    con.Connect();
                    cmd.ExecuteNonQuery();
                    con.DisConnect();
                    Get_A_1_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
                    GetUploadFileData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('File size must be less than or equal to 200 KB.');", true);
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only PDF and image files are allowed.');", true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select a file to upload.');", true);

        }
        md_FileUpload.Show();
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public static object UploadFile()
    {
        try
        {
            HttpPostedFile file = HttpContext.Current.Request.Files["file"];
            if (file != null && file.ContentLength > 0)
            {
                // Check file size limit
                if (file.ContentLength <= 204800)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string fileType = Path.GetExtension(fileName);
                    byte[] fileData = new byte[file.ContentLength];
                    file.InputStream.Read(fileData, 0, file.ContentLength);

                    // Perform database saving logic (save filename, file data, etc.)
                    pms_connection con = new pms_connection();
                    SqlCommand cmd = new SqlCommand("sp_InsertAttachment_PMS", con.Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // You may pass real data for ReferenceID, Employee Code, etc.
                    cmd.Parameters.AddWithValue("@ReferenceID", 123); // Example
                    cmd.Parameters.AddWithValue("@Attachment_FileName", fileName);
                    cmd.Parameters.AddWithValue("@Attachment_FileType", fileType);
                    cmd.Parameters.AddWithValue("@Attachment_Data", fileData);
                    cmd.Parameters.AddWithValue("@Empoyee_Code", "Test1"); // Example Employee Code
                    cmd.Parameters.AddWithValue("@Academic_Year", "2023"); // Example Academic Year
                    cmd.Parameters.AddWithValue("@Applicable_For", "Test"); // Example Applicable For
                    cmd.Parameters.AddWithValue("@CreatedBy_ID", "Test1");
                    cmd.Parameters.AddWithValue("@CreatedBy_Name", "John Doe");
                    cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now);

                    con.Connect();
                    cmd.ExecuteNonQuery();
                    con.DisConnect();

                    return new { Success = true, Message = "File uploaded successfully." };
                }
                else
                {
                    return new { Success = false, Message = "File size must be less than or equal to 200 KB." };
                }
            }
            else
            {
                return new { Success = false, Message = "Please select a file to upload." };
            }
        }
        catch (Exception ex)
        {
            return new { Success = false, Message = "Error: " + ex.Message };
        }
    }

    //protected void UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    //{
    //    string path = Server.MapPath("~/PMSReportView/") + e.FileName;
    //    AjaxFileUpload1.SaveAs(path);
    //}

    [System.Web.Services.WebMethod]
    public static string UploadAttachment()
    {
        HttpPostedFile file = HttpContext.Current.Request.Files["file"];
        string employeeCode = HttpContext.Current.Request.Form["employeeCode"];
        string academicYear = HttpContext.Current.Request.Form["academicYear"];
        string applicableFor = HttpContext.Current.Request.Form["applicableFor"];

        if (file != null && file.ContentLength > 0)
        {
            string fileName = Path.GetFileName(file.FileName);
            string fileType = file.ContentType;
            byte[] fileData;

            using (BinaryReader reader = new BinaryReader(file.InputStream))
            {
                fileData = reader.ReadBytes(file.ContentLength);
            }

            pms_connection con = new pms_connection();
            string query = @"INSERT INTO [dbo].[tbl_PMS_Attachment] 
                             ([ReferenceID], [Attachment_FileName], [Attachment_FileType], [Attachment_Data], 
                              [Empoyee_Code], [Academic_Year], [Applicable_For], [CreatedBy_ID], [CreatedBy_Name], [Created_Date]) 
                             VALUES 
                             (@ReferenceID, @Attachment_FileName, @Attachment_FileType, @Attachment_Data, 
                              @EmployeeCode, @AcademicYear, @ApplicableFor, @CreatedBy_ID, @CreatedBy_Name, @Created_Date)";

            using (SqlCommand cmd = new SqlCommand(query, con.Con))
            {
                cmd.Parameters.AddWithValue("@ReferenceID", DBNull.Value); // Set according to your logic
                cmd.Parameters.AddWithValue("@Attachment_FileName", fileName);
                cmd.Parameters.AddWithValue("@Attachment_FileType", fileType);
                cmd.Parameters.AddWithValue("@Attachment_Data", fileData);
                cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                cmd.Parameters.AddWithValue("@AcademicYear", academicYear);
                cmd.Parameters.AddWithValue("@ApplicableFor", applicableFor);
                cmd.Parameters.AddWithValue("@CreatedBy_ID", "YourUserID");  // Replace with actual user ID
                cmd.Parameters.AddWithValue("@CreatedBy_Name", "YourUserName");  // Replace with actual user name
                cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now);

                try
                {
                    con.Connect();
                    cmd.ExecuteNonQuery();
                    return "Success";
                }
                catch (Exception ex)
                {
                    return "Error: " + ex.Message;
                }
            }

        }
        else
        {
            return "No file uploaded.";
        }
    }
    protected void btnAttachmentSave_Fu_A1_Click(object sender, EventArgs e)
    {
        SaveAttachment();
        Save_InDraft();
        sp_Get_PMS_All_Data(dd_AcademicYear.SelectedValue.Trim(), txtFilterBy.Text.Trim(), Session["uid"].ToString().Trim(), Session["Departmentcode"].ToString().Trim(), dd_Staus.SelectedValue.Trim());
        pnlcreate.Visible = false;
        pnl_Dashboard.Visible = true;
        btnCreateNew.Visible = false;
    }

    protected void btnexportinexcel_Click(object sender, EventArgs e)
    {
        grd_Employee_Details.AllowPaging = false;
        SP_PMS_Get_Filled_Or_Not_Filed_Emaployee_Details(dd_AcademicYear.SelectedValue.Trim(), ViewState["lbl_Employee_Status_Details"].ToString().Trim());

        // Export the GridView to Excel
        ExportGridToExcel(ViewState["lbl_Employee_Status_Details"].ToString().Trim());

        // Re-enable paging after export
        grd_Employee_Details.AllowPaging = true;
        SP_PMS_Get_Filled_Or_Not_Filed_Emaployee_Details(dd_AcademicYear.SelectedValue.Trim(), ViewState["lbl_Employee_Status_Details"].ToString().Trim());

    }

    protected void lnk_Total_PMS_Filled_Click(object sender, EventArgs e)
    {
        lbl_Employee_Status_Details.Text = "Filled";
        ViewState["lbl_Employee_Status_Details"] = "Filled";
        SP_PMS_Get_Filled_Or_Not_Filed_Emaployee_Details(dd_AcademicYear.SelectedValue.Trim(), ViewState["lbl_Employee_Status_Details"].ToString().Trim());
        md_Employee_Count_Details.Show();
    }
    public void SP_PMS_Get_Filled_Or_Not_Filed_Emaployee_Details(string AcademicYear, string Applicablefor)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = con.SP_PMS_Get_Filled_Or_Not_Filed_Emaployee_Details(AcademicYear.Trim(), Applicablefor.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grd_Employee_Details.DataSource = dt;
        grd_Employee_Details.DataBind();
        dr.Close();
        con.DisConnect();
    }


    protected void lnk_Total_PMS_Pending_Click(object sender, EventArgs e)
    {
        lbl_Employee_Status_Details.Text = "Pending";
        ViewState["lbl_Employee_Status_Details"] = "Pending";
        SP_PMS_Get_Filled_Or_Not_Filed_Emaployee_Details(dd_AcademicYear.SelectedValue.Trim(), ViewState["lbl_Employee_Status_Details"].ToString().Trim());
        md_Employee_Count_Details.Show();
    }

    protected void grd_Employee_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Employee_Details.PageIndex = e.NewPageIndex;
        SP_PMS_Get_Filled_Or_Not_Filed_Emaployee_Details(dd_AcademicYear.SelectedValue.Trim(), ViewState["lbl_Employee_Status_Details"].ToString().Trim());
        md_Employee_Count_Details.Show();
    }

    protected void dd_Designation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_designation.Text = dd_Designation.SelectedValue.Trim().ToUpper();

        int currentYear = DateTime.Now.Year;
        calculatedata();

        //        ddl_academic_session.Text = currentYear.ToString().Substring(2) + "-" + (currentYear + 1).ToString().Substring(2);

        //      Faculty_Enabledata();
        //    assesmentDisable();
        // clear_data();
        //Get_FacultyIn_percentage(lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());
        //  pnlcreate.Visible = true;
        //pnl_Dashboard.Visible = false;


        //ViewState["ApplicableFor"] = "Insert";
        //ViewState["ID_For_Updated"] = "";
        //lbl_emp_code.Text = Session["uid"].ToString().Trim();
        //lbl_faculty_name.Text = Session["Fulname"].ToString().Trim();
        //lbl_designation.Text = Session["PMS_Job_Title_Grade_Desc"].ToString().Trim();
        //lbl_college_department.Text = Session["EmployeePostingGroupl"].ToString().Trim();
        //lbl_New_College.Text = Session["GlobalDimension1Coded"].ToString().Trim();
        //lbl_New_Department.Text = Session["PMS_DepartmentName"].ToString().Trim();
        //HR_Disable();
        //btn_Fu_A1.Visible = true;
        //btnAttachmentSave_Fu_A1.Visible = true;
        //Enable_Designation_dropdown();

        //GetFeedback_Data();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Are you sure you want to proceed?');", true);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
    }

    public void ExportGridToExcel(string pending)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + pending + ".xls");
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            // To Export all pages, we need to render the GridView with all data
            grd_Employee_Details.RenderControl(hw);
            // Style to format the cells
            string style = @"<style> .textmode { } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }


    protected void txt_d1_ReportingAuthorityAssessment_TextChanged1(object sender, EventArgs e)
    {
        Calculation_For_HOD();
    }

    public void GetFeedback_Data()
    {
        //Logic for feedback table
        if (lbl_facultyfeedback_inpercentage.Text == "")
        {

        }
        else
        {
            // Assume you have a TextBox where the user inputs the feedback percentage.
            double facultyFeedbackPercentage = Convert.ToDouble(lbl_facultyfeedback_inpercentage.Text.Trim());
            int facultyObtainedMarks = 0;

            // Determine marks based on feedback percentage
            if (facultyFeedbackPercentage >= 90)
            {
                facultyObtainedMarks = 50;
            }
            else if (facultyFeedbackPercentage >= 80 && facultyFeedbackPercentage < 90)
            {
                facultyObtainedMarks = 45;
            }
            else if (facultyFeedbackPercentage >= 70 && facultyFeedbackPercentage < 80)
            {
                facultyObtainedMarks = 40;
            }
            else if (facultyFeedbackPercentage >= 60 && facultyFeedbackPercentage < 70)
            {
                facultyObtainedMarks = 35;
            }
            else if (facultyFeedbackPercentage >= 50 && facultyFeedbackPercentage < 60)
            {
                facultyObtainedMarks = 30;
            }
            else if (facultyFeedbackPercentage >= 40 && facultyFeedbackPercentage < 50)
            {
                facultyObtainedMarks = 25;
            }
            else if (facultyFeedbackPercentage >= 30 && facultyFeedbackPercentage < 40)
            {
                facultyObtainedMarks = 20;
            }
            else if (facultyFeedbackPercentage >= 20 && facultyFeedbackPercentage < 30)
            {
                facultyObtainedMarks = 15;
            }
            else if (facultyFeedbackPercentage >= 10 && facultyFeedbackPercentage < 20)
            {
                facultyObtainedMarks = 10;
            }
            else if (facultyFeedbackPercentage >= 0 && facultyFeedbackPercentage < 10)
            {
                facultyObtainedMarks = 5;
            }

            // Update the marks label
            lbl_facultyobtained_total.Text = facultyObtainedMarks.ToString();

        }
    }

    protected void dd_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        sp_Get_PMS_All_Data(dd_AcademicYear.SelectedValue.Trim(), txtFilterBy.Text.Trim(), Session["uid"].ToString().Trim(), Session["Departmentcode"].ToString().Trim(), dd_Staus.SelectedValue.Trim());
    }

    protected void lnk_PMS_Report_Click(object sender, EventArgs e)
    {
        Response.Redirect("FA_AppraisalForm.aspx", false);
    }
    protected void drpResearchIncentive_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpResearchIncentive.SelectedValue == "1")
        {
            lNameOfJournal.Text = "Name of the Journal";
            lblTitlepaper.Text = "Title of the Paper";
            txtNoIssue.Enabled = true;
            txtISSNno.Enabled = true;
            lblISBN.Text = " ISSN no.";

        }
        if (drpResearchIncentive.SelectedValue == "2")
        {

            lNameOfJournal.Text = "Application No";
            //lblTitlepaper.Text = "Name of the Book/ Publisher";
            lblTitlepaper.Text = "Title";
            txtNoIssue.Text = "";
            txtISSNno.Text = "";
            txtNoIssue.Enabled = false;
            txtISSNno.Enabled = false;

        }
        if (Convert.ToInt32(drpResearchIncentive.SelectedValue) > 2)
        {
            lNameOfJournal.Text = "Name of the Book/Publisher";
            lblTitlepaper.Text = "Title";

            lblISBN.Text = "ISBN no.";
            //lblTitlepaper.Text = "Application No";
            txtNoIssue.Enabled = true;
            txtISSNno.Enabled = true;
        }
        md_FileUpload.Show();
    }

    protected void drpactivityYESNO_TextChanged(object sender, EventArgs e)
    {
        if (drpactivityYESNO.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1.Enabled = true;
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1.Visible = true;
            Button3.Visible = false;
        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1.Text = "";
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_TextChanged(this, EventArgs.Empty);
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2_TextChanged(this, EventArgs.Empty);
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1.Enabled = false;
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f1_2.Enabled = false;

            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f1.Visible = false;
            Button3.Visible = true;
        }

    }

    protected void DropDownList1_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "1")
        {

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Enabled = true;
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2.Visible = true;
            Button4.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Text = "";
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Text = "";
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2_TextChanged(this, EventArgs.Empty);
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f2.Enabled = false;
            txt_CriteriaB_ResearchAndDevelopment_AssessmentbyReportingAuthority_f2.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f2.Visible = false;
            Button4.Visible = true;
        }
    }

    protected void DropDownList2_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue == "1")
        {

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3.Enabled = true;
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3.Visible = true;
            Button5.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3.Text = "";
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2.Text = "";
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_TextChanged(this, EventArgs.Empty);
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2_TextChanged(this, EventArgs.Empty);
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3.Enabled = false;
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f3_2.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f3.Visible = false;
            Button5.Visible = true;
        }
    }

    protected void DropDownList3_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList3.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4.Visible = true;
            Button6.Visible = false;
        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f4.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f4.Visible = false;
            Button6.Visible = true;

        }
    }

    protected void DropDownList4_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList4.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5.Visible = true;
            Button7.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f5.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f5.Visible = false;
            Button7.Visible = true;

        }
    }

    protected void DropDownList5_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList5.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6.Visible = true;
            Button8.Visible = false;
        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f6.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f6.Visible = false;
            Button8.Visible = true;

        }
    }

    protected void DropDownList6_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList6.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7.Visible = true;
            Button9.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_f7.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_f7.Visible = false;
            Button9.Visible = true;

        }
    }

    protected void DropDownList10_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList10.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1.Visible = true;
            Button10.Visible = false;


        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h1.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h1.Visible = false;
            Button10.Visible = true;

        }
    }

    protected void DropDownList11_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList11.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2.Visible = true;
            Button11.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h2.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h2.Visible = false;
            Button11.Visible = true;

        }

    }

    protected void DropDownList12_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList12.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3.Visible = true;
            Button12.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3_TextChanged(this, EventArgs.Empty);


            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h3.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h3.Visible = false;
            Button12.Visible = true;
        }
    }

    protected void DropDownList13_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList13.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4.Visible = true;
            Button13.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h4.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h4.Visible = false;
            Button13.Visible = true;

        }
    }

    protected void DropDownList14_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList14.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5.Visible = true;
            Button14.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h5.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h5.Visible = false;
            Button14.Visible = true;

        }
    }

    protected void DropDownList15_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList15.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6.Visible = true;
            Button15.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h6.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h6.Visible = false;
            Button15.Visible = true;

        }
    }

    protected void DropDownList16_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList16.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7.Visible = true;
            Button16.Visible = false;
        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h7.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h7.Visible = false;
            Button16.Visible = true;

        }
    }

    protected void DropDownList17_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList17.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8.Visible = true;
            Button17.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h8.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h8.Visible = false;
            Button17.Visible = true;

        }
    }

    protected void DropDownList18_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList18.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9.Visible = true;
            Button34.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_h9.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_h9.Visible = false;
            Button34.Visible = true;

        }
    }

    protected void DropDownList19_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList19.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1.Enabled = true;

            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1.Visible = true;
            Button20.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i1.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i1.Visible = false;
            Button20.Visible = true;

        }
    }

    protected void DropDownList20_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList20.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2.Visible = true;
            Button21.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i2.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i2.Visible = false;
            Button21.Visible = true;

        }
    }

    protected void DropDownList21_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList21.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3.Visible = true;
            Button22.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i3.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i3.Visible = false;
            Button22.Visible = true;

        }
    }

    protected void DropDownList22_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList22.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4.Visible = true;
            Button23.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i4.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i4.Visible = false;
            Button23.Visible = true;

        }
    }

    protected void DropDownList23_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList23.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5.Visible = true;
            Button24.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_i5.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_i5.Visible = false;
            Button24.Visible = true;

        }
    }

    protected void DropDownList24_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList24.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Enabled = true;

            fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1.Visible = true;
            Button18.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j1.Enabled = false;

            fu_CriteriaB_ResearchAndDevelopment_FileUpload_j1.Visible = false;
            Button18.Visible = true;

        }
    }

    protected void DropDownList25_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList25.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2.Visible = true;
            Button19.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_j2.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_j2.Visible = false;
            Button19.Visible = true;

        }
    }

    protected void DropDownList28_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList28.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3.Visible = true;
            Button25.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k3.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k3.Visible = false;
            Button25.Visible = true;

        }
    }

    protected void DropDownList29_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList29.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4.Visible = true;
            Button26.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k4.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k4.Visible = false;
            Button26.Visible = true;

        }
    }

    protected void DropDownList30_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList30.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5.Visible = true;
            Button27.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k5.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k5.Visible = false;
            Button27.Visible = true;

        }
    }

    protected void DropDownList31_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList31.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6.Visible = true;
            Button28.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k6.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k6.Visible = false;
            Button28.Visible = true;

        }
    }

    protected void DropDownList32_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList32.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7.Visible = true;
            Button29.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7_TextChanged(this, EventArgs.Empty);


            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k7.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k7.Visible = false;
            Button29.Visible = true;
        }
    }

    protected void DropDownList33_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList33.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8.Visible = true;
            Button30.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k8.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k8.Visible = false;
            Button30.Visible = true;
        }
    }

    protected void DropDownList34_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList34.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9.Visible = true;
            Button31.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k9.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k9.Visible = false;
            Button31.Visible = true;
        }
    }

    protected void DropDownList35_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList35.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10.Visible = true;
            Button32.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k10.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k10.Visible = false;
            Button32.Visible = true;
        }
    }

    protected void DropDownList36_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList36.SelectedValue == "1")
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Enabled = true;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11.Visible = true;
            Button33.Visible = false;

        }
        else
        {
            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Text = "";

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11_TextChanged(this, EventArgs.Empty);

            txt_CriteriaB_ResearchAndDevelopment_RequiredActivitiesInNumbers_k11.Enabled = false;
            fu_CriteriaB_ResearchAndDevelopment_FileUpload_k11.Visible = false;
            Button33.Visible = true;
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "f.1");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);


    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "f.2");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "f.3");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "f.4");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "f.5");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "f.6");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "f.7");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "h.1");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "h.2");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button12_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "h.3");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button13_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "h.4");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button14_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "h.5");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button15_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "h.6");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button16_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "h.7");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button17_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "h.8");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button18_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "j.1");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button19_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "j.2");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button20_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "i.1");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button21_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "i.2");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button22_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "i.3");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button23_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "i.4");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button24_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "i.5");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button25_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "k.3");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button26_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "k.4");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button27_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "k.5");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button28_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "k.6");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button29_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "k.7");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button30_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "k.8");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button31_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "k.9");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button32_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "k.10");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button33_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "k.11");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void Button34_Click(object sender, EventArgs e)
    {
        pms_connection con = new pms_connection();
        SqlCommand cmd = new SqlCommand("sp_updateNoActivity_PMS", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ReferenceID", 123);
        cmd.Parameters.AddWithValue("@Empoyee_Code", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
        cmd.Parameters.AddWithValue("@Applicable_For", "h.9");
        cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedBy_Name", lbl_faculty_name.Text.Trim());
        cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("dd MMM yyyy HH:mm"));
        cmd.Parameters.AddWithValue("@month", hfmonth.Value);
        cmd.Parameters.AddWithValue("@ActivityType", "1");
        con.Connect();
        cmd.ExecuteNonQuery();
        con.DisConnect();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly Activity Update Successfully');", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {



        if (fileUpload.HasFile)
        {
            string fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();

            // Define allowed extensions
            string[] allowedExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };

            // Check if the file extension is allowed
            if (allowedExtensions.Contains(fileExtension))
            {
                if (fileUpload.PostedFile.ContentLength <= 204800) // 200 KB file size check
                {
                    string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                    string fileType = Path.GetExtension(fileName);
                    byte[] fileData = fileUpload.FileBytes;
                    pms_connection con = new pms_connection();
                    SqlCommand cmd = new SqlCommand("sp_InsertAttachment_F2", con.Con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Attachment_FileName", fileName);
                    cmd.Parameters.AddWithValue("@Attachment_FileType", fileType);
                    cmd.Parameters.AddWithValue("@Attachment_Data", fileData);
                    cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text.Trim());
                    cmd.Parameters.AddWithValue("@Url", txtUrl.Text.Trim());
                    cmd.Parameters.AddWithValue("@PublishDate", txtDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@title_of_paper", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@MagazineName", txtMagazine.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                    cmd.Parameters.AddWithValue("@month", hfmonth.Value);
                    cmd.Parameters.AddWithValue("@ActivityType", 2);
                    con.Connect();
                    cmd.ExecuteNonQuery();
                    con.DisConnect();
                    lblApplicablefor.Text = "f_2";
                    Save_InDraft();
                    Get_A_1_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

                    lblerror.Text = "";
                    md_FileUpload.Show();
                }
                else
                {
                    lblerror.Text = "File size must be less than or equal to 200 KB.";

                    md_FileUpload.Show();
                }
            }
            else
            {

                lblerror.Text = "Only PDF and image files are allowed.";

                md_FileUpload.Show();

            }
        }
        else
        {
            lblerror.Text = "Please select a file to upload.";
            F_2.Visible = true;
            F_1.Visible = false;
            md_FileUpload.Show();

        }






    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_f_2", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvData.DataSource = dt;
            gvData.DataBind();
        }
    }

    protected void gvData_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            DownloadFile1(id);
        }
        else if (e.CommandName == "DeleteRow")
        {
            DeleteRecord(id);
            BindGrid();
            F_2.Visible = true;
            F_1.Visible = false;
            md_FileUpload.Show();
        }
    }

    private void DownloadFile1(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_f_2 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void DeleteRecord(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM tbl_f_2 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }


    protected void btnSaveF3_Click(object sender, EventArgs e)
    {
        if (ddlLevel.SelectedIndex == 0)
        {
            lblErrorF3.Text = "Select Level";
            return;
        }

        if (!fileUploadF3.HasFile)
        {
            lblErrorF3.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadF3.FileName).ToLower();
        string[] allowed = { ".pdf", ".jpg", ".jpeg", ".png" };

        if (!allowed.Contains(ext))
        {
            lblErrorF3.Text = "Invalid file type";
            return;
        }

        if (fileUploadF3.PostedFile.ContentLength > 204800)
        {
            lblErrorF3.Text = "File must be <= 200KB";
            return;
        }

        byte[] data = fileUploadF3.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_InsertAttachment_F3", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Level", ddlLevel.SelectedValue);
            cmd.Parameters.AddWithValue("@Category", ddlCategoryF3.SelectedValue);
            cmd.Parameters.AddWithValue("@ActivityType", 2);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkF3.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadF3.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            Save_InDraft();
            Get_A_3_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

            lblerror.Text = "";
            md_FileUpload.Show();
        }

        lblErrorF3.Text = "Saved Successfully";

    }

    private void BindGridF3()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_f_3", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvF3.DataSource = dt;
            gvF3.DataBind();
        }
    }
    protected void gvF3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            DownloadF3(id);
        }
        else if (e.CommandName == "DeleteRow")
        {
            DeleteF3(id);
            BindGridF3();
        }
    }
    private void DownloadF3(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_f_3 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void DeleteF3(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM tbl_f_3 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }


    protected void btnSaveF4_Click(object sender, EventArgs e)
    {
        if (ddlBookType.SelectedIndex == 0)
        {
            lblErrorF4.Text = "Select Book Type";
            return;
        }

        if (ddlLevelF4.SelectedIndex == 0)
        {
            lblErrorF4.Text = "Select Level";
            return;
        }

        if (!fileUploadF4.HasFile)
        {
            lblErrorF4.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadF4.FileName).ToLower();
        string[] allowed = { ".pdf", ".jpg", ".jpeg", ".png" };

        if (!allowed.Contains(ext))
        {
            lblErrorF4.Text = "Invalid file type";
            return;
        }

        if (fileUploadF4.PostedFile.ContentLength > 204800)
        {
            lblErrorF4.Text = "File must be <= 200KB";
            return;
        }

        byte[] data = fileUploadF4.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_InsertAttachment_F4", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BookType", ddlBookType.SelectedValue);
            cmd.Parameters.AddWithValue("@Category", ddlLevelF4.SelectedValue);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkF4.Text);
            cmd.Parameters.AddWithValue("@ActivityType", 2);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadF4.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
        }
        Save_InDraft();
        lblErrorF4.Text = "Saved Successfully";
        Get_A_4_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        lblerror.Text = "";
        md_FileUpload.Show();
    }
    protected void gvF4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            DownloadF4(id);
        }

    }
    private void DownloadF4(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_f_4 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    protected void btnSaveF5_Click(object sender, EventArgs e)
    {
        if (ddlBookTypeF5.SelectedIndex == 0)
        {
            lblErrorF5.Text = "Select Book Type";
            return;
        }

        if (ddlLevelF5.SelectedIndex == 0)
        {
            lblErrorF5.Text = "Select Category";
            return;
        }

        if (!fileUploadF5.HasFile)
        {
            lblErrorF5.Text = "Upload file";
            return;
        }

        string ext = Path.GetExtension(fileUploadF5.FileName).ToLower();
        byte[] data = fileUploadF5.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_InsertAttachment_F5", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BookType", ddlBookTypeF5.Text);
            cmd.Parameters.AddWithValue("@Category", ddlLevelF5.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkF5.Text);
            cmd.Parameters.AddWithValue("@ActivityType", 2);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadF5.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
        }
        Save_InDraft();
        lblErrorF4.Text = "Saved Successfully";
        Get_A_5_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        lblerror.Text = "";
        md_FileUpload.Show();
    }
    protected void gvF5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            DownloadF5(id);
        }

    }
    protected void gvF6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            DownloadF6(id);
        }

    }
    protected void gvF7_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            DownloadF7(id);
        }

    }
    protected void gvh1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadh1(id);
        }

    }
    protected void gvh2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadh2(id);
        }

    }
    protected void gvh3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadh3(id);
        }

    }
    protected void gvh4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadh4(id);
        }

    }
    protected void gvh5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadh5(id);
        }

    }
    protected void gvh6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadh6(id);
        }

    }

    protected void gvh7_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadh7(id);
        }

    }
    protected void gvh8_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            DownloadF6(id);
        }

    }
    protected void gvh9_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadh9(id);
        }

    }

    protected void gvi1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadi1(id);
        }

    }
    protected void gvi2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadi2(id);
        }

    }
    protected void gvi3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadi3(id);
        }

    }
    protected void gvi4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadi4(id);
        }

    }
    protected void gvi5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadi5(id);
        }

    }

    protected void gvj1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadj1(id);
        }

    }

    protected void gvj2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadj2(id);
        }

    }

    protected void gvk3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadk3(id);
        }

    }
    protected void gvk4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadk4(id);
        }

    }
    protected void gvk5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadk5(id);
        }

    }
    protected void gvk6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadk6(id);
        }

    }
    protected void gvk7_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadk7(id);
        }

    }
    protected void gvk8_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadk8(id);
        }

    }
    protected void gvk9_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadk9(id);
        }

    }
    protected void gvk10_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadk10(id);
        }

    }
    protected void gvk11_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Download")
        {
            Downloadk11(id);
        }

    }
    private void DownloadF5(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_f_5 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void DownloadF6(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_f_6 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void DownloadF7(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_f_7 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadh1(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_1 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadh2(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_2 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void Downloadh3(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_3 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void Downloadh4(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_4 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadh5(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_5 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadh6(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_6 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadh7(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_7 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadh8(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_8 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadh9(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_9 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void Downloadi1(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_i_1 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void Downloadi2(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_i_2 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadi3(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_i_3 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void Downloadi4(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_i_4 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void Downloadi5(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_i_5 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadj1(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_j_1 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadj2(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_j_2 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void Downloadk3(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_3 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadk4(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_4 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadk5(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_5 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadk6(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_6 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    private void Downloadk7(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_7 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void Downloadk8(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_8 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void Downloadk9(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_9 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void Downloadk10(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_10 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }

    private void Downloadk11(int id)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_11 WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Response.Clear();
                Response.ContentType = dr["Attachment_FileType"].ToString();
                Response.AddHeader("content-disposition",
                    "attachment;filename=" + dr["Attachment_FileName"].ToString());

                Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                Response.End();
            }
        }
    }
    protected void btnSaveF6_Click(object sender, EventArgs e)
    {
        if (ddlBookTypeF6.SelectedIndex == 0)
        {
            lblErrorF6.Text = "Select Book Type";
            return;
        }

        if (ddlLevelF6.SelectedIndex == 0)
        {
            lblErrorF6.Text = "Select Category";
            return;
        }

        if (!fileUploadF6.HasFile)
        {
            lblErrorF6.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadF6.FileName).ToLower();
        string[] allowed = { ".pdf", ".jpg", ".jpeg", ".png" };

        if (!allowed.Contains(ext))
        {
            lblErrorF6.Text = "Invalid file type";
            return;
        }

        if (fileUploadF6.PostedFile.ContentLength > 204800)
        {
            lblErrorF6.Text = "File must be <= 200KB";
            return;
        }

        byte[] data = fileUploadF6.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_InsertAttachment_F6", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BookType", ddlBookTypeF6.Text);
            cmd.Parameters.AddWithValue("@Category", ddlLevelF6.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkF6.Text);
            cmd.Parameters.AddWithValue("@ActivityType", 2);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadF6.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
        }
        Save_InDraft();
        lblErrorF4.Text = "Saved Successfully";
        Get_A_6_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        lblerror.Text = "";
        md_FileUpload.Show();
    }

    public void Get_A_6_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvF6.DataSource = dt;
        gvF6.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_A_7_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvF7.DataSource = dt;
        gvF7.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_h_1_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvH1.DataSource = dt;
        gvH1.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_h_2_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvH2.DataSource = dt;
        gvH2.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_h_3_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvH3.DataSource = dt;
        gvH3.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_h_4_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvH4.DataSource = dt;
        gvH4.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_h_5_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvH5.DataSource = dt;
        gvH5.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_h_6_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvH6.DataSource = dt;
        gvH6.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_h_7_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvH7.DataSource = dt;
        gvH7.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_h_8_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvH8.DataSource = dt;
        gvH8.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_h_9_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvH9.DataSource = dt;
        gvH9.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_i_1_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvI1.DataSource = dt;
        gvI1.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_i_2_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvI2.DataSource = dt;
        gvI2.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_i_3_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvI3.DataSource = dt;
        gvI3.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_i_4_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvI4.DataSource = dt;
        gvI4.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_i_5_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvI5.DataSource = dt;
        gvI5.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_j_1_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvJ1.DataSource = dt;
        gvJ1.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_j_2_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvJ2.DataSource = dt;
        gvJ2.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_k_3_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvK3.DataSource = dt;
        gvK3.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_k_4_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvK4.DataSource = dt;
        gvK4.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_k_5_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvK5.DataSource = dt;
        gvK5.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_k_6_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvK6.DataSource = dt;
        gvK6.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_k_7_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvK7.DataSource = dt;
        gvK7.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_k_8_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvK8.DataSource = dt;
        gvK8.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_k_9_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvK9.DataSource = dt;
        gvK9.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_k_10_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvK10.DataSource = dt;
        gvK10.DataBind();
        dr.Close();
        con.DisConnect();
    }
    public void Get_k_11_Attachment(string Applicable_For, string EmployeeID, string AcademicYear)
    {
        pms_connection con = new pms_connection();
        SqlDataReader dr = sp_GetAttachmentByApplicable_for_PMS(Applicable_For.Trim(), EmployeeID.Trim(), AcademicYear.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        gvK11.DataSource = dt;
        gvK11.DataBind();
        dr.Close();
        con.DisConnect();
    }
    protected void btnSave_F7_Click(object sender, EventArgs e)
    {


        if (ddlBookType_F7.SelectedIndex == 0)
        {
            lblErrorF7.Text = "Select Book Type";
            return;
        }

        if (ddlCategory_F7.SelectedIndex == 0)
        {
            lblErrorF7.Text = "Select Category";
            return;
        }

        if (!fileUpload_F7.HasFile)
        {
            lblErrorF7.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUpload_F7.FileName).ToLower();
        string[] allowed = { ".pdf", ".jpg", ".jpeg", ".png" };

        if (!allowed.Contains(ext))
        {
            lblErrorF7.Text = "Invalid file type";
            return;
        }

        if (fileUpload_F7.PostedFile.ContentLength > 204800)
        {
            lblErrorF7.Text = "File must be <= 200KB";
            return;
        }

        byte[] data = fileUpload_F7.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_InsertAttachment_F7", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BookType", ddlBookType_F7.Text);
            cmd.Parameters.AddWithValue("@Category", ddlCategory_F7.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemark_F7.Text);
            cmd.Parameters.AddWithValue("@ActivityType", 2);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUpload_F7.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
        }
        Save_InDraft();
        lblErrorF7.Text = "Saved Successfully";

        Get_A_7_Attachment(
            lblApplicablefor.Text.Trim(),
            lbl_emp_code.Text.Trim(),
            ddl_academic_session.Text.Trim()
        );

        lblerror.Text = "";
        md_FileUpload.Show();



    }
    protected void btnSaveH1_Click(object sender, EventArgs e)
    {
        if (ddlCategoryH1.SelectedIndex == 0)
        {
            lblErrorH1.Text = "Select Category";
            return;
        }

        if (ddlPatentCategoryH1.SelectedIndex == 0)
        {
            lblErrorH1.Text = "Select Patent Category";
            return;
        }

        if (!fileUploadH1.HasFile)
        {
            lblErrorH1.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadH1.FileName).ToLower();
        byte[] data = fileUploadH1.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_H1", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Main Fields
            cmd.Parameters.AddWithValue("@Category", ddlCategoryH1.SelectedValue);
            cmd.Parameters.AddWithValue("@PatentNumber_ApplicationNo", txtPatentNoH1.Text);
            cmd.Parameters.AddWithValue("@Title", txtTitleH1.Text);
            cmd.Parameters.AddWithValue("@PatentCategory", ddlPatentCategoryH1.SelectedValue);
            cmd.Parameters.AddWithValue("@DateOfAward", txtDateH1.Text);
            cmd.Parameters.AddWithValue("@PatentAwardingAgency", txtAgencyH1.Text);
            cmd.Parameters.AddWithValue("@URL", txtURLH1.Text);
            cmd.Parameters.AddWithValue("@Inventors", txtInventorsH1.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkH1.Text);

            // File (Same as your pattern)
            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadH1.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            // Common Fields (your exact format)
            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorH1.ForeColor = System.Drawing.Color.Green;
        lblErrorH1.Text = "Data Saved Successfully!";
        Get_h_1_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        lblerror.Text = "";
        md_FileUpload.Show();
    }
    protected void gvH1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_1 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void gvH2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_2 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveH2_Click(object sender, EventArgs e)
    {
        if (ddlCategoryH2.SelectedIndex == 0)
        {
            lblErrorH2.Text = "Select Category";
            return;
        }

        if (ddlPatentCategoryH2.SelectedIndex == 0)
        {
            lblErrorH2.Text = "Select Patent Category";
            return;
        }

        if (!fileUploadH2.HasFile)
        {
            lblErrorH2.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadH2.FileName).ToLower();
        byte[] data = fileUploadH2.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_H2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Main Fields
            cmd.Parameters.AddWithValue("@Category", ddlCategoryH2.SelectedValue);
            cmd.Parameters.AddWithValue("@PatentNumber_ApplicationNo", txtPatentNoH2.Text);
            cmd.Parameters.AddWithValue("@Title", txtTitleH2.Text);
            cmd.Parameters.AddWithValue("@PatentCategory", ddlPatentCategoryH2.SelectedValue);
            cmd.Parameters.AddWithValue("@DateOfAward", txtDateH2.Text);
            cmd.Parameters.AddWithValue("@PatentAwardingAgency", txtAgencyH2.Text);
            cmd.Parameters.AddWithValue("@URL", txtURLH2.Text);
            cmd.Parameters.AddWithValue("@Inventors", txtInventorsH2.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkH2.Text);

            // File
            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadH2.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            // Common Fields
            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorH2.ForeColor = System.Drawing.Color.Green;
        lblErrorH2.Text = "Data Saved Successfully!";

        // Bind Grid (IMPORTANT)
        Get_h_2_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        lblerror.Text = "";
        md_FileUpload.Show();
    }
    protected void btnSaveH3_Click(object sender, EventArgs e)
    {
        if (ddlFundingTypeH3.SelectedIndex == 0)
        {
            lblErrorH3.Text = "Select Funding Type";
            return;
        }

        if (!fileUploadH3.HasFile)
        {
            lblErrorH3.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadH3.FileName).ToLower();
        byte[] data = fileUploadH3.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_H3", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FundingType", ddlFundingTypeH3.SelectedValue);
            cmd.Parameters.AddWithValue("@PI_CoPI_Name", txtPIH3.Text);
            cmd.Parameters.AddWithValue("@Title", txtTitleH3.Text);
            cmd.Parameters.AddWithValue("@AgencyName", txtAgencyH3.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationH3.Text);
            cmd.Parameters.AddWithValue("@YearOfAwardSanction", txtYearH3.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmountH3.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkH3.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadH3.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorH3.ForeColor = System.Drawing.Color.Green;
        lblErrorH3.Text = "Data Saved Successfully!";

        Get_h_3_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        lblerror.Text = "";
        md_FileUpload.Show();
    }
    protected void gvH3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_3 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveH4_Click(object sender, EventArgs e)
    {
        if (ddlFundingTypeH4.SelectedIndex == 0)
        {
            lblErrorH4.Text = "Select Funding Type";
            return;
        }

        if (!fileUploadH4.HasFile)
        {
            lblErrorH4.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadH4.FileName).ToLower();
        byte[] data = fileUploadH4.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_H4", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FundingType", ddlFundingTypeH4.SelectedValue);
            cmd.Parameters.AddWithValue("@PI_CoPI_Name", txtPIH4.Text);
            cmd.Parameters.AddWithValue("@Title", txtTitleH4.Text);
            cmd.Parameters.AddWithValue("@AgencyName", txtAgencyH4.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationH4.Text);
            cmd.Parameters.AddWithValue("@YearOfAwardSanction", txtYearH4.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmountH4.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkH4.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadH4.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorH4.ForeColor = System.Drawing.Color.Green;
        lblErrorH4.Text = "Data Saved Successfully!";

        Get_h_4_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvH4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_4 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveH5_Click(object sender, EventArgs e)
    {
        if (ddlFundingTypeH5.SelectedIndex == 0)
        {
            lblErrorH5.Text = "Select Funding Type";
            return;
        }

        if (!fileUploadH5.HasFile)
        {
            lblErrorH5.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadH5.FileName).ToLower();
        byte[] data = fileUploadH5.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_H5", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FundingType", ddlFundingTypeH5.SelectedValue);
            cmd.Parameters.AddWithValue("@PI_CoPI_Name", txtPIH5.Text);
            cmd.Parameters.AddWithValue("@Title", txtTitleH5.Text);
            cmd.Parameters.AddWithValue("@AgencyName", txtAgencyH5.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationH5.Text);
            cmd.Parameters.AddWithValue("@YearOfAwardSanction", txtYearH5.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmountH5.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkH5.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadH5.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorH5.ForeColor = System.Drawing.Color.Green;
        lblErrorH5.Text = "Data Saved Successfully!";

        Get_h_5_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvH5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_5 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveH6_Click(object sender, EventArgs e)
    {
        if (ddlFundingTypeH6.SelectedIndex == 0)
        {
            lblErrorH6.Text = "Select Funding Type";
            return;
        }

        if (!fileUploadH6.HasFile)
        {
            lblErrorH6.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadH6.FileName).ToLower();
        byte[] data = fileUploadH6.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_H6", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FundingType", ddlFundingTypeH6.SelectedValue);
            cmd.Parameters.AddWithValue("@PI_CoPI_Name", txtPIH6.Text);
            cmd.Parameters.AddWithValue("@Title", txtTitleH6.Text);
            cmd.Parameters.AddWithValue("@AgencyName", txtAgencyH6.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationH6.Text);
            cmd.Parameters.AddWithValue("@YearOfAwardSanction", txtYearH6.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmountH6.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkH6.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadH6.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorH6.ForeColor = System.Drawing.Color.Green;
        lblErrorH6.Text = "Data Saved Successfully!";

        Get_h_6_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvH6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_6 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveH7_Click(object sender, EventArgs e)
    {
        if (ddlFundingTypeH7.SelectedIndex == 0)
        {
            lblErrorH7.Text = "Select Funding Type";
            return;
        }

        if (!fileUploadH7.HasFile)
        {
            lblErrorH7.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadH7.FileName).ToLower();
        byte[] data = fileUploadH7.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_H7", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FundingType", ddlFundingTypeH7.SelectedValue);
            cmd.Parameters.AddWithValue("@PI_CoPI_Name", txtPIH7.Text);
            cmd.Parameters.AddWithValue("@Title", txtTitleH7.Text);
            cmd.Parameters.AddWithValue("@AgencyName", txtAgencyH7.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationH7.Text);
            cmd.Parameters.AddWithValue("@YearOfAwardSanction", txtYearH7.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmountH7.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkH7.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadH7.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorH7.ForeColor = System.Drawing.Color.Green;
        lblErrorH7.Text = "Data Saved Successfully!";

        Get_h_7_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvH7_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_7 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveH8_Click(object sender, EventArgs e)
    {
        if (txtProjectTitleH8.Text.Trim() == "")
        {
            lblErrorH8.Text = "Enter Project Title";
            return;
        }

        if (!fileUploadH8.HasFile)
        {
            lblErrorH8.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadH8.FileName).ToLower();
        byte[] data = fileUploadH8.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_H8", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProjectTitle", txtProjectTitleH8.Text);
            cmd.Parameters.AddWithValue("@PI_CoPI_Name", txtPIH8.Text);
            cmd.Parameters.AddWithValue("@DateOfGranted", txtDateH8.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmountH8.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkH8.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadH8.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorH8.ForeColor = System.Drawing.Color.Green;
        lblErrorH8.Text = "Data Saved Successfully!";

        Get_h_8_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvH8_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_8 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveH9_Click(object sender, EventArgs e)
    {
        if (txtTitleH9.Text.Trim() == "")
        {
            lblErrorH9.Text = "Enter Title";
            return;
        }

        if (!fileUploadH9.HasFile)
        {
            lblErrorH9.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadH9.FileName).ToLower();
        byte[] data = fileUploadH9.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_H9", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", txtTitleH9.Text);
            cmd.Parameters.AddWithValue("@Publisher", txtPublisherH9.Text);
            cmd.Parameters.AddWithValue("@URL", txtURLH9.Text);
            cmd.Parameters.AddWithValue("@DateOfPublication", txtDateH9.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkH9.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadH9.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorH9.ForeColor = System.Drawing.Color.Green;
        lblErrorH9.Text = "Data Saved Successfully!";

        Get_h_9_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvH9_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_h_9 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveI1_Click(object sender, EventArgs e)
    {
        if (txtTitleI1.Text.Trim() == "")
        {
            lblErrorI1.Text = "Enter Title";
            return;
        }

        if (ddlCategoryI1.SelectedIndex == 0)
        {
            lblErrorI1.Text = "Select Category";
            return;
        }

        if (ddlModeI1.SelectedIndex == 0)
        {
            lblErrorI1.Text = "Select Mode";
            return;
        }

        if (!fileUploadI1.HasFile)
        {
            lblErrorI1.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadI1.FileName).ToLower();
        byte[] data = fileUploadI1.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_I1", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", txtTitleI1.Text);
            cmd.Parameters.AddWithValue("@Category", ddlCategoryI1.SelectedValue);
            cmd.Parameters.AddWithValue("@ConferenceName", txtConferenceI1.Text);
            cmd.Parameters.AddWithValue("@HostingInstitute", txtInstituteI1.Text);
            cmd.Parameters.AddWithValue("@DateOfConference", txtDateI1.Text);
            cmd.Parameters.AddWithValue("@Mode", ddlModeI1.SelectedValue);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkI1.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadI1.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorI1.ForeColor = System.Drawing.Color.Green;
        lblErrorI1.Text = "Data Saved Successfully!";

        Get_i_1_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvI1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_i_1 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveI2_Click(object sender, EventArgs e)
    {
        if (txtTitleI2.Text.Trim() == "")
        {
            lblErrorI2.Text = "Enter Title";
            return;
        }

        if (ddlCategoryI2.SelectedIndex == 0)
        {
            lblErrorI2.Text = "Select Category";
            return;
        }

        if (ddlModeI2.SelectedIndex == 0)
        {
            lblErrorI2.Text = "Select Mode";
            return;
        }

        if (!fileUploadI2.HasFile)
        {
            lblErrorI2.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadI2.FileName).ToLower();
        byte[] data = fileUploadI2.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_I2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", txtTitleI2.Text);
            cmd.Parameters.AddWithValue("@Category", ddlCategoryI2.SelectedValue);
            cmd.Parameters.AddWithValue("@ConferenceName", txtConferenceI2.Text);
            cmd.Parameters.AddWithValue("@HostingInstitute", txtInstituteI2.Text);
            cmd.Parameters.AddWithValue("@DateOfConference", txtDateI2.Text);
            cmd.Parameters.AddWithValue("@Mode", ddlModeI2.SelectedValue);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkI2.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadI2.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorI2.ForeColor = System.Drawing.Color.Green;
        lblErrorI2.Text = "Data Saved Successfully!";

        Get_i_2_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvI2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_i_2 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveI3_Click(object sender, EventArgs e)
    {
        if (txtTitleI3.Text.Trim() == "")
        {
            lblErrorI3.Text = "Enter Title";
            return;
        }

        if (ddlCategoryI3.SelectedIndex == 0)
        {
            lblErrorI3.Text = "Select Category";
            return;
        }

        if (ddlModeI3.SelectedIndex == 0)
        {
            lblErrorI3.Text = "Select Mode";
            return;
        }

        if (!fileUploadI3.HasFile)
        {
            lblErrorI3.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadI3.FileName).ToLower();
        byte[] data = fileUploadI3.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_I3", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", txtTitleI3.Text);
            cmd.Parameters.AddWithValue("@Category", ddlCategoryI3.SelectedValue);
            cmd.Parameters.AddWithValue("@ConferenceName", txtConferenceI3.Text);
            cmd.Parameters.AddWithValue("@HostingInstitute", txtInstituteI3.Text);
            cmd.Parameters.AddWithValue("@DateOfConference", txtDateI3.Text);
            cmd.Parameters.AddWithValue("@Mode", ddlModeI3.SelectedValue);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkI3.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadI3.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorI3.ForeColor = System.Drawing.Color.Green;
        lblErrorI3.Text = "Data Saved Successfully!";

        Get_i_3_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvI3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_i_3 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveI4_Click(object sender, EventArgs e)
    {
        if (ddlEventCategoryI4.SelectedIndex == 0)
        {
            lblErrorI4.Text = "Select Event Category";
            return;
        }

        if (ddlRoleI4.SelectedIndex == 0)
        {
            lblErrorI4.Text = "Select Role";
            return;
        }

        if (ddlCategoryI4.SelectedIndex == 0)
        {
            lblErrorI4.Text = "Select Category";
            return;
        }

        if (ddlModeI4.SelectedIndex == 0)
        {
            lblErrorI4.Text = "Select Mode";
            return;
        }

        if (!fileUploadI4.HasFile)
        {
            lblErrorI4.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadI4.FileName).ToLower();
        byte[] data = fileUploadI4.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_I4", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventCategory", ddlEventCategoryI4.SelectedValue);
            cmd.Parameters.AddWithValue("@Role", ddlRoleI4.SelectedValue);
            cmd.Parameters.AddWithValue("@Title", txtTitleI4.Text);
            cmd.Parameters.AddWithValue("@EventDate", txtDateI4.Text);
            cmd.Parameters.AddWithValue("@Category", ddlCategoryI4.SelectedValue);
            cmd.Parameters.AddWithValue("@HostingInstitute", txtInstituteI4.Text);
            cmd.Parameters.AddWithValue("@Mode", ddlModeI4.SelectedValue);
            cmd.Parameters.AddWithValue("@Duration", txtDurationI4.Text);
            cmd.Parameters.AddWithValue("@AttendeesDetails", txtAttendeesI4.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkI4.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadI4.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorI4.ForeColor = System.Drawing.Color.Green;
        lblErrorI4.Text = "Data Saved Successfully!";

        Get_i_4_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvI4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_i_4 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveI5_Click(object sender, EventArgs e)
    {
        if (ddlEventCategoryI5.SelectedIndex == 0)
        {
            lblErrorI5.Text = "Select Event Category";
            return;
        }

        if (ddlRoleI5.SelectedIndex == 0)
        {
            lblErrorI5.Text = "Select Role";
            return;
        }

        if (ddlCategoryI5.SelectedIndex == 0)
        {
            lblErrorI5.Text = "Select Category";
            return;
        }

        if (ddlModeI5.SelectedIndex == 0)
        {
            lblErrorI5.Text = "Select Mode";
            return;
        }

        if (!fileUploadI5.HasFile)
        {
            lblErrorI5.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadI5.FileName).ToLower();
        byte[] data = fileUploadI5.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_I5", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventCategory", ddlEventCategoryI5.SelectedValue);
            cmd.Parameters.AddWithValue("@Role", ddlRoleI5.SelectedValue);
            cmd.Parameters.AddWithValue("@Title", txtTitleI5.Text);
            cmd.Parameters.AddWithValue("@EventDate", txtDateI5.Text);
            cmd.Parameters.AddWithValue("@Category", ddlCategoryI5.SelectedValue);
            cmd.Parameters.AddWithValue("@HostingInstitute", txtInstituteI5.Text);
            cmd.Parameters.AddWithValue("@Mode", ddlModeI5.SelectedValue);
            cmd.Parameters.AddWithValue("@Duration", txtDurationI5.Text);
            cmd.Parameters.AddWithValue("@AttendeesDetails", txtAttendeesI5.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkI5.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadI5.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        lblErrorI5.ForeColor = System.Drawing.Color.Green;
        lblErrorI5.Text = "Data Saved Successfully!";

        Get_i_5_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvI5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_i_5 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveJ1_Click(object sender, EventArgs e)
    {
        if (txtTitleJ1.Text.Trim() == "")
        {
            lblErrorJ1.Text = "Enter Title";
            return;
        }

        if (ddlModeJ1.SelectedIndex == 0)
        {
            lblErrorJ1.Text = "Select Mode";
            return;
        }

        if (!fileUploadJ1.HasFile)
        {
            lblErrorJ1.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadJ1.FileName).ToLower();
        byte[] data = fileUploadJ1.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_J1", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", txtTitleJ1.Text);
            cmd.Parameters.AddWithValue("@ProjectTitle", txtProjectTitleJ1.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationJ1.Text);
            cmd.Parameters.AddWithValue("@ActivityDate", txtDateJ1.Text);
            cmd.Parameters.AddWithValue("@Mode", ddlModeJ1.SelectedValue);
            cmd.Parameters.AddWithValue("@AgencyName", txtAgencyJ1.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmountJ1.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkJ1.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadJ1.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorJ1.ForeColor = System.Drawing.Color.Green;
        lblErrorJ1.Text = "Data Saved Successfully!";

        Get_j_1_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvJ1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_j_1 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveJ2_Click(object sender, EventArgs e)
    {
        if (txtTitleJ2.Text.Trim() == "")
        {
            lblErrorJ2.Text = "Enter Title";
            return;
        }

        if (ddlModeJ2.SelectedIndex == 0)
        {
            lblErrorJ2.Text = "Select Mode";
            return;
        }

        if (!fileUploadJ2.HasFile)
        {
            lblErrorJ2.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadJ2.FileName).ToLower();
        byte[] data = fileUploadJ2.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_J2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", txtTitleJ2.Text);
            cmd.Parameters.AddWithValue("@ProjectTitle", txtProjectTitleJ2.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationJ2.Text);
            cmd.Parameters.AddWithValue("@ActivityDate", txtDateJ2.Text);
            cmd.Parameters.AddWithValue("@Mode", ddlModeJ2.SelectedValue);
            cmd.Parameters.AddWithValue("@AgencyName", txtAgencyJ2.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmountJ2.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkJ2.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadJ2.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorJ2.ForeColor = System.Drawing.Color.Green;
        lblErrorJ2.Text = "Data Saved Successfully!";

        Get_j_2_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvJ2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_j_2 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveK3_Click(object sender, EventArgs e)
    {
        if (ddlModeK3.SelectedIndex == 0)
        {
            lblErrorK3.Text = "Select Mode";
            return;
        }

        if (ddlLevelK3.SelectedIndex == 0)
        {
            lblErrorK3.Text = "Select Level";
            return;
        }

        if (ddlEventCategoryK3.SelectedIndex == 0)
        {
            lblErrorK3.Text = "Select Event Category";
            return;
        }

        if (ddlRoleK3.SelectedIndex == 0)
        {
            lblErrorK3.Text = "Select Role";
            return;
        }

        if (!fileUploadK3.HasFile)
        {
            lblErrorK3.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadK3.FileName).ToLower();
        byte[] data = fileUploadK3.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_K3", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Mode", ddlModeK3.SelectedValue);
            cmd.Parameters.AddWithValue("@Level", ddlLevelK3.SelectedValue);
            cmd.Parameters.AddWithValue("@EventCategory", ddlEventCategoryK3.SelectedValue);
            cmd.Parameters.AddWithValue("@Role", ddlRoleK3.SelectedValue);
            cmd.Parameters.AddWithValue("@Title", txtTitleK3.Text);
            cmd.Parameters.AddWithValue("@EventDate", txtDateK3.Text);
            cmd.Parameters.AddWithValue("@HostingInstitute", txtInstituteK3.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationK3.Text);
            cmd.Parameters.AddWithValue("@OtherInformation", txtOtherInfoK3.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkK3.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadK3.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorK3.ForeColor = System.Drawing.Color.Green;
        lblErrorK3.Text = "Data Saved Successfully!";

        Get_k_3_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvK3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_3 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveK4_Click(object sender, EventArgs e)
    {
        if (ddlModeK4.SelectedIndex == 0)
        {
            lblErrorK4.Text = "Select Mode";
            return;
        }

        if (ddlLevelK4.SelectedIndex == 0)
        {
            lblErrorK4.Text = "Select Level";
            return;
        }

        if (ddlEventCategoryK4.SelectedIndex == 0)
        {
            lblErrorK4.Text = "Select Event Category";
            return;
        }

        if (ddlRoleK4.SelectedIndex == 0)
        {
            lblErrorK4.Text = "Select Role";
            return;
        }

        if (!fileUploadK4.HasFile)
        {
            lblErrorK4.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadK4.FileName).ToLower();
        byte[] data = fileUploadK4.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_K4", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Mode", ddlModeK4.SelectedValue);
            cmd.Parameters.AddWithValue("@Level", ddlLevelK4.SelectedValue);
            cmd.Parameters.AddWithValue("@EventCategory", ddlEventCategoryK4.SelectedValue);
            cmd.Parameters.AddWithValue("@Role", ddlRoleK4.SelectedValue);
            cmd.Parameters.AddWithValue("@Title", txtTitleK4.Text);
            cmd.Parameters.AddWithValue("@EventDate", txtDateK4.Text);
            cmd.Parameters.AddWithValue("@HostingInstitute", txtInstituteK4.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationK4.Text);
            cmd.Parameters.AddWithValue("@OtherInformation", txtOtherInfoK4.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkK4.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadK4.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorK4.ForeColor = System.Drawing.Color.Green;
        lblErrorK4.Text = "Data Saved Successfully!";

        Get_k_4_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvK4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_4 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveK5_Click(object sender, EventArgs e)
    {
        if (ddlModeK5.SelectedIndex == 0)
        {
            lblErrorK5.Text = "Select Mode";
            return;
        }

        if (ddlLevelK5.SelectedIndex == 0)
        {
            lblErrorK5.Text = "Select Level";
            return;
        }

        if (ddlEventCategoryK5.SelectedIndex == 0)
        {
            lblErrorK5.Text = "Select Event Category";
            return;
        }

        if (ddlRoleK5.SelectedIndex == 0)
        {
            lblErrorK5.Text = "Select Role";
            return;
        }

        if (!fileUploadK5.HasFile)
        {
            lblErrorK5.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadK5.FileName).ToLower();
        byte[] data = fileUploadK5.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_K5", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Mode", ddlModeK5.SelectedValue);
            cmd.Parameters.AddWithValue("@Level", ddlLevelK5.SelectedValue);
            cmd.Parameters.AddWithValue("@EventCategory", ddlEventCategoryK5.SelectedValue);
            cmd.Parameters.AddWithValue("@Role", ddlRoleK5.SelectedValue);
            cmd.Parameters.AddWithValue("@Title", txtTitleK5.Text);
            cmd.Parameters.AddWithValue("@EventDate", txtDateK5.Text);
            cmd.Parameters.AddWithValue("@HostingInstitute", txtInstituteK5.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationK5.Text);
            cmd.Parameters.AddWithValue("@OtherInformation", txtOtherInfoK5.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkK5.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadK5.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorK5.ForeColor = System.Drawing.Color.Green;
        lblErrorK5.Text = "Data Saved Successfully!";

        Get_k_5_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvK5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_5 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveK6_Click(object sender, EventArgs e)
    {
        if (ddlActivityTypeK6.SelectedIndex == 0)
        {
            lblErrorK6.Text = "Select Activity Type";
            return;
        }

        if (ddlLevelK6.SelectedIndex == 0)
        {
            lblErrorK6.Text = "Select Level";
            return;
        }

        if (ddlModeK6.SelectedIndex == 0)
        {
            lblErrorK6.Text = "Select Mode";
            return;
        }

        if (!fileUploadK6.HasFile)
        {
            lblErrorK6.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadK6.FileName).ToLower();
        byte[] data = fileUploadK6.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_K6", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ActivitySubType", ddlActivityTypeK6.SelectedValue);
            cmd.Parameters.AddWithValue("@Level", ddlLevelK6.SelectedValue);
            cmd.Parameters.AddWithValue("@Mode", ddlModeK6.SelectedValue);
            cmd.Parameters.AddWithValue("@HostingInstitute", txtInstituteK6.Text);
            cmd.Parameters.AddWithValue("@ActivityDate", txtDateK6.Text);
            cmd.Parameters.AddWithValue("@PurposeOfInvitation", txtPurposeK6.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkK6.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadK6.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorK6.ForeColor = System.Drawing.Color.Green;
        lblErrorK6.Text = "Data Saved Successfully!";

        Get_k_6_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvK6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_6 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveK7_Click(object sender, EventArgs e)
    {
        if (txtTitleK7.Text.Trim() == "")
        {
            lblErrorK7.Text = "Enter Title";
            return;
        }

        if (!fileUploadK7.HasFile)
        {
            lblErrorK7.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadK7.FileName).ToLower();
        byte[] data = fileUploadK7.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_K7", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", txtTitleK7.Text);
            cmd.Parameters.AddWithValue("@HostingInstitute", txtInstituteK7.Text);
            cmd.Parameters.AddWithValue("@ActivityDate", txtDateK7.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkK7.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadK7.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorK7.ForeColor = System.Drawing.Color.Green;
        lblErrorK7.Text = "Data Saved Successfully!";

        Get_k_7_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvK7_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_7 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveK8_Click(object sender, EventArgs e)
    {
        if (txtBodyNameK8.Text.Trim() == "")
        {
            lblErrorK8.Text = "Enter Body Name";
            return;
        }

        if (ddlLevelK8.SelectedIndex == 0)
        {
            lblErrorK8.Text = "Select Level";
            return;
        }

        if (ddlMembershipTypeK8.SelectedIndex == 0)
        {
            lblErrorK8.Text = "Select Membership Type";
            return;
        }

        if (!fileUploadK8.HasFile)
        {
            lblErrorK8.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadK8.FileName).ToLower();
        byte[] data = fileUploadK8.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_K8", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BodyName", txtBodyNameK8.Text);
            cmd.Parameters.AddWithValue("@Level", ddlLevelK8.SelectedValue);
            cmd.Parameters.AddWithValue("@Discipline", txtDisciplineK8.Text);
            cmd.Parameters.AddWithValue("@NatureOfBody", ddlNatureK8.SelectedValue);
            cmd.Parameters.AddWithValue("@MembershipType", ddlMembershipTypeK8.SelectedValue);
            cmd.Parameters.AddWithValue("@MembershipID", txtMembershipIDK8.Text);
            cmd.Parameters.AddWithValue("@DateOfRegistration", txtDateK8.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationK8.Text);
            cmd.Parameters.AddWithValue("@MembershipLevel", ddlMembershipLevelK8.SelectedValue);
            cmd.Parameters.AddWithValue("@RegistrationFee", txtFeeK8.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkK8.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadK8.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorK8.ForeColor = System.Drawing.Color.Green;
        lblErrorK8.Text = "Data Saved Successfully!";

        Get_k_8_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvK8_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_8 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveK9_Click(object sender, EventArgs e)
    {
        if (ddlLevelK9.SelectedIndex == 0)
        {
            lblErrorK9.Text = "Select Level";
            return;
        }

        if (ddlCategoryK9.SelectedIndex == 0)
        {
            lblErrorK9.Text = "Select Category";
            return;
        }

        if (ddlNatureK9.SelectedIndex == 0)
        {
            lblErrorK9.Text = "Select Nature";
            return;
        }

        if (!fileUploadK9.HasFile)
        {
            lblErrorK9.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadK9.FileName).ToLower();
        byte[] data = fileUploadK9.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_K9", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Level", ddlLevelK9.SelectedValue);
            cmd.Parameters.AddWithValue("@AwardName", txtAwardNameK9.Text);
            cmd.Parameters.AddWithValue("@Category", ddlCategoryK9.SelectedValue);
            cmd.Parameters.AddWithValue("@Nature", ddlNatureK9.SelectedValue);
            cmd.Parameters.AddWithValue("@DateOfAward", txtAwardDateK9.Text);
            cmd.Parameters.AddWithValue("@DateOfRegistration", txtRegDateK9.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationK9.Text);
            cmd.Parameters.AddWithValue("@AwardingAgency", txtAgencyK9.Text);
            cmd.Parameters.AddWithValue("@OrganizationType", ddlOrgTypeK9.SelectedValue);
            cmd.Parameters.AddWithValue("@Title", txtTitleK9.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmountK9.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkK9.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadK9.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorK9.ForeColor = System.Drawing.Color.Green;
        lblErrorK9.Text = "Data Saved Successfully!";

        Get_k_9_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvK9_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_9 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveK10_Click(object sender, EventArgs e)
    {
        if (ddlLevelK10.SelectedIndex == 0)
        {
            lblErrorK10.Text = "Select Level";
            return;
        }

        if (ddlCategoryK10.SelectedIndex == 0)
        {
            lblErrorK10.Text = "Select Category";
            return;
        }

        if (ddlNatureK10.SelectedIndex == 0)
        {
            lblErrorK10.Text = "Select Nature";
            return;
        }

        if (!fileUploadK10.HasFile)
        {
            lblErrorK10.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadK10.FileName).ToLower();
        byte[] data = fileUploadK10.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_K10", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Level", ddlLevelK10.SelectedValue);
            cmd.Parameters.AddWithValue("@AwardName", txtAwardNameK10.Text);
            cmd.Parameters.AddWithValue("@Category", ddlCategoryK10.SelectedValue);
            cmd.Parameters.AddWithValue("@Nature", ddlNatureK10.SelectedValue);
            cmd.Parameters.AddWithValue("@DateOfAward", txtAwardDateK10.Text);
            cmd.Parameters.AddWithValue("@DateOfRegistration", txtRegDateK10.Text);
            cmd.Parameters.AddWithValue("@Duration", txtDurationK10.Text);
            cmd.Parameters.AddWithValue("@AwardingAgency", txtAgencyK10.Text);
            cmd.Parameters.AddWithValue("@OrganizationType", ddlOrgTypeK10.SelectedValue);
            cmd.Parameters.AddWithValue("@Title", txtTitleK10.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmountK10.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemarkK10.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadK10.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorK10.ForeColor = System.Drawing.Color.Green;
        lblErrorK10.Text = "Data Saved Successfully!";

        Get_k_10_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvK10_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_10 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
    protected void btnSaveK11_Click(object sender, EventArgs e)
    {
        if (ddlStatusK11.SelectedIndex == 0)
        {
            lblErrorK11.Text = "Select Course Status";
            return;
        }

        if (!fileUploadK11.HasFile)
        {
            lblErrorK11.Text = "Select file";
            return;
        }

        string ext = Path.GetExtension(fileUploadK11.FileName).ToLower();
        byte[] data = fileUploadK11.FileBytes;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_K11", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CourseFile", txtCourseFileK11.Text);
            cmd.Parameters.AddWithValue("@Platform", txtPlatformK11.Text);
            cmd.Parameters.AddWithValue("@HostingInstitute", txtInstituteK11.Text);
            cmd.Parameters.AddWithValue("@Discipline", txtDisciplineK11.Text);
            cmd.Parameters.AddWithValue("@DurationWeeks", txtDurationWeeksK11.Text);
            cmd.Parameters.AddWithValue("@CourseStartDate", txtStartDateK11.Text);
            cmd.Parameters.AddWithValue("@CourseEndDate", txtEndDateK11.Text);
            cmd.Parameters.AddWithValue("@CourseStatus", ddlStatusK11.SelectedValue);
            cmd.Parameters.AddWithValue("@Result", txtResultK11.Text);

            cmd.Parameters.AddWithValue("@FundingType", ddlFundingK11.SelectedValue);
            cmd.Parameters.AddWithValue("@PI_CoPI_Name", txtPIK11.Text);
            cmd.Parameters.AddWithValue("@ProjectTitle", txtProjectTitleK11.Text);
            cmd.Parameters.AddWithValue("@AgencyName", txtAgencyK11.Text);
            cmd.Parameters.AddWithValue("@ProjectDuration", txtProjDurationK11.Text);
            cmd.Parameters.AddWithValue("@YearOfAwardSanction", txtYearK11.Text);
            cmd.Parameters.AddWithValue("@Amount", txtAmountK11.Text);

            cmd.Parameters.AddWithValue("@Remark", txtRemarkK11.Text);

            cmd.Parameters.AddWithValue("@Attachment_FileName", fileUploadK11.FileName);
            cmd.Parameters.AddWithValue("@Attachment_FileType", ext);
            cmd.Parameters.AddWithValue("@Attachment_Data", data);

            cmd.Parameters.AddWithValue("@Academic_Year", ddl_academic_session.Text);
            cmd.Parameters.AddWithValue("@CreatedBy_ID", lbl_emp_code.Text);
            cmd.Parameters.AddWithValue("@Month", hfmonth.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        Save_InDraft();
        lblErrorK11.ForeColor = System.Drawing.Color.Green;
        lblErrorK11.Text = "Data Saved Successfully!";

        Get_k_11_Attachment(lblApplicablefor.Text.Trim(), lbl_emp_code.Text.Trim(), ddl_academic_session.Text.Trim());

        md_FileUpload.Show();
    }
    protected void gvK11_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_k_11 WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Response.Clear();
                    Response.ContentType = dr["Attachment_FileType"].ToString();
                    Response.AddHeader("content-disposition",
                        "attachment;filename=" + dr["Attachment_FileName"].ToString());

                    Response.BinaryWrite((byte[])dr["Attachment_Data"]);
                    Response.End();
                }
            }
        }
    }
}