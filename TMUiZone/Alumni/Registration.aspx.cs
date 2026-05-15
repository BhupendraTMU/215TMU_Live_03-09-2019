using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Alumni_Registration : System.Web.UI.Page
{
    DL.StudentDetailsViewDL sdvDL = new DL.StudentDetailsViewDL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                bindData(Session["uid"].ToString());
                Rdata();
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

            txtNAME.Text = dt.Rows[0]["First Name"].ToString();
            
            txtpro.Text = dt.Rows[0]["Course Code"].ToString();
            txtGender.Text = "";
            txtdob.Text = dt.Rows[0]["DOB"].ToString();
            txtGender.Text = dt.Rows[0]["Gender1"].ToString();
            txtCO.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            txtPY.Text = dt.Rows[0]["Passout Year"].ToString();
            txtmob.Text = dt.Rows[0]["Mobile Number"].ToString();
            txtEmailID.Text = dt.Rows[0]["E-Mail Address"].ToString();
            txtAdd.Text = dt.Rows[0]["Address1"].ToString();
            txtPAddress.Text = dt.Rows[0]["Address1"].ToString();
            txtEnroll.Text = dt.Rows[0]["Enrollment No_"].ToString();
       
        }
        else
        {
    
        }

    }


         public void Rdata()
         {
                         con.Open();
            SqlCommand cmd1 = new SqlCommand("GetAlumaniRegistration", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@SNo", Session["uid"].ToString());
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            con.Close();
            if (dt1.Rows.Count > 0)
            {
                btnsave.Visible = false;
                ddlEng.SelectedValue = dt1.Rows[0]["Engagement Type"].ToString();
                if (ddlEng.SelectedValue == "1")
                {
                    PnlFurther.Visible = false;
                    pnlEmployee.Visible = true;
                    SelfEmployee.Visible = false;

                }

                if (ddlEng.SelectedValue == "2")
                {
                    PnlFurther.Visible = false;
                    pnlEmployee.Visible = false;
                    SelfEmployee.Visible = true;

                }

                if (ddlEng.SelectedValue == "3")
                {
                    PnlFurther.Visible = true;
                    pnlEmployee.Visible = false;
                    SelfEmployee.Visible = false;
                   
                }

                if (ddlEng.SelectedValue == "4")
                {
                    PnlFurther.Visible = false;
                    pnlEmployee.Visible = false;
                    SelfEmployee.Visible = false;
                }




                txtDesi.Text = dt1.Rows[0]["Designation"].ToString();
                txtEmp.Text = dt1.Rows[0]["Employer"].ToString();
               // txtBusiness.Text = dt1.Rows[0]["Business Type"].ToString();
                txtComName.Text = dt1.Rows[0]["Company Name"].ToString();
                txtInstitude.Text = dt1.Rows[0]["Higher Studies Institute"].ToString();
                txtProgram.Text = dt1.Rows[0]["Higher Studies Program"].ToString();
               // txtDetails.Text = dt1.Rows[0]["Other Details"].ToString();
                txtmob.Text = dt1.Rows[0]["Mobile No_"].ToString();
               txtEmailID.Text = dt1.Rows[0]["Email ID"].ToString();
               txtLocation.Text = dt1.Rows[0]["Current Location City"].ToString();
                txtCountry.Text = dt1.Rows[0]["Current Location Country"].ToString();
               txtSA.Text = dt1.Rows[0]["Significant Achievements"].ToString();
            }
         }
    

    protected void ddlEng_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlEng.SelectedValue == "1")
        {
            pnlEmployee.Visible = true;
            SelfEmployee.Visible = false;
            PnlFurther.Visible = false;
            UnEmployee.Visible = false;
            //txtEmp.Visible = true;
            //txtDesi.Visible = true;
            //txtInstitude.Visible = false;
            //txtBusiness.Visible = false;
            //txtDetails.Visible = false;
            //txtComName.Visible = false;
            //txtProgram.Visible = false;


            //RetxtEmp.Visible = true;
            //RetxtDesi.Visible = true;
            //RetxtInstitude.Visible = false;
            //RetxtBusiness.Visible = false;
            //RetxtDetails.Visible = false;
            //RetxtComName.Visible = false;
            //RetxtProgram.Visible = false;
        
        }

        if (ddlEng.SelectedValue == "2")
        {
            pnlEmployee.Visible = false;
            SelfEmployee.Visible = true;
            PnlFurther.Visible = false;
            UnEmployee.Visible = false;
            //txtDesi.Visible = false;
            //txtDetails.Visible = false;
            //txtProgram.Visible = false;
            //txtEmp.Visible = false;
            //txtInstitude.Visible = false;
            //txtBusiness.Visible = true;
            //txtComName.Visible = true;


            //RetxtDesi.Visible = false;
            //RetxtDetails.Visible = false;
            //RetxtProgram.Visible = false;
            //RetxtEmp.Visible = false;
            //RetxtInstitude.Visible = false;
            //RetxtBusiness.Visible = true;
            //RetxtComName.Visible = true;
          
        }

        if (ddlEng.SelectedValue == "3")
        {
            PnlFurther.Visible = true;
            pnlEmployee.Visible = false;
            SelfEmployee.Visible = false;
            UnEmployee.Visible = false;


            //txtInstitude.Visible = true;
            //txtProgram.Visible = true;
            //txtEmp.Visible = false;
            //txtBusiness.Visible = false;
            //txtComName.Visible = false;
            //txtDesi.Visible = false;
            //txtDetails.Visible = false;


            //RetxtInstitude.Visible = true;
            //RetxtProgram.Visible = true;
            //RetxtEmp.Visible = false;
            //RetxtBusiness.Visible = false;
            //RetxtComName.Visible = false;
            //RetxtDesi.Visible = false;
            //RetxtDetails.Visible = false;
        }

        if (ddlEng.SelectedValue == "4")
        {
            PnlFurther.Visible = false;
            pnlEmployee.Visible = false;
            SelfEmployee.Visible = false;
            UnEmployee.Visible = true;

            //txtInstitude.Visible = false;
            //txtComName.Visible = false;
            //txtProgram.Visible = false;
            //txtBusiness.Visible = false;
            //txtEmp.Visible= false;
            //txtDesi.Visible = false;
            //txtDetails.Visible = true;


            //RetxtInstitude.Visible = false;
            //RetxtComName.Visible = false;
            //RetxtProgram.Visible = false;
            //RetxtBusiness.Visible = false;
            //RetxtEmp.Visible = false;
            //RetxtDesi.Visible = false;
            //RetxtDetails.Visible = true;
        }



    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {


            con.Open();
            SqlCommand cmd1 = new SqlCommand("GetAlumaniRegistration", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@SNo", Session["uid"].ToString());
            cmd1.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            con.Close();
            if (dt1.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You are already registered or registration date over')", true);
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertAlumaniRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SNo", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@Engtype", ddlEng.SelectedValue);
                if (ddlEng.SelectedValue == "1")
                {
                    cmd.Parameters.AddWithValue("@Designation", txtDesi.Text);
                    cmd.Parameters.AddWithValue("@Employer", txtEmp.Text);


                    cmd.Parameters.AddWithValue("@Add", txtAdd.Text);
                    cmd.Parameters.AddWithValue("@Indus", txtIndustry.Text);
                    cmd.Parameters.AddWithValue("@JobDesc", txtJobDes.Text);
                    cmd.Parameters.AddWithValue("@JobStatus", drpJstatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@StartDate", txtStartDate.Text);
                    cmd.Parameters.AddWithValue("@txtComWebSite", txtWebSite.Text);
                    cmd.Parameters.AddWithValue("@CEmail", txtCEmail.Text);
                    cmd.Parameters.AddWithValue("@CTele", txtCTele.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Designation", "");
                    cmd.Parameters.AddWithValue("@Employer", "");
                    cmd.Parameters.AddWithValue("@Add", "");
                    cmd.Parameters.AddWithValue("@Indus", "");
                    cmd.Parameters.AddWithValue("@JobDesc", "");
                    cmd.Parameters.AddWithValue("@JobStatus", "");
                    cmd.Parameters.AddWithValue("@StartDate", "");
                    cmd.Parameters.AddWithValue("@txtComWebSite", "");
                    cmd.Parameters.AddWithValue("@CEmail", "");
                    cmd.Parameters.AddWithValue("@CTele", "");
                }
                if (ddlEng.SelectedValue == "2")
                {
                    
                    cmd.Parameters.AddWithValue("@EnterPrice", txtEnterPrice.Text);
                    cmd.Parameters.AddWithValue("@Industry", txtIndustry.Text);
                    cmd.Parameters.AddWithValue("@EnAdd", txtEnAdd.Text);
                    cmd.Parameters.AddWithValue("@Role", txtRole.Text);
                    cmd.Parameters.AddWithValue("@WebSite", txtWebSite.Text);
                }
                else
                {

                    cmd.Parameters.AddWithValue("@EnterPrice", "");
                    cmd.Parameters.AddWithValue("@Industry", "");
                    cmd.Parameters.AddWithValue("@EnAdd", "");
                    cmd.Parameters.AddWithValue("@Role", "");
                    cmd.Parameters.AddWithValue("@WebSite", "");
                }
                if (ddlEng.SelectedValue == "3")
                {
                    cmd.Parameters.AddWithValue("@HigherStudiesInstitute", txtInstitude.Text);
                    cmd.Parameters.AddWithValue("@HigherStudiesProgram", txtProgram.Text);

                    cmd.Parameters.AddWithValue("@FurAddress", txtFurAddress.Text);
                    cmd.Parameters.AddWithValue("@WebURL", txtWebURL.Text);
                    cmd.Parameters.AddWithValue("@AdmissionYear", txtAdmissionYear.Text);
                    cmd.Parameters.AddWithValue("@ExpectedGraduationYear", txtExpectedGraduationYear.Text);
                    cmd.Parameters.AddWithValue("@FurPlan", txtFurPlan.Text);
                    
                }
                else
                {
                    cmd.Parameters.AddWithValue("@HigherStudiesInstitute", "");
                    cmd.Parameters.AddWithValue("@HigherStudiesProgram", "");
                    cmd.Parameters.AddWithValue("@FurAddress", "");
                    cmd.Parameters.AddWithValue("@WebURL", "");
                    cmd.Parameters.AddWithValue("@AdmissionYear", "");
                    cmd.Parameters.AddWithValue("@ExpectedGraduationYear","");
                    cmd.Parameters.AddWithValue("@FurPlan", "");
                }
                if (ddlEng.SelectedValue == "4")
                {
                    //cmd.Parameters.AddWithValue("@OtherDetails", txtDetails.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@OtherDetails", "");
                }
                cmd.Parameters.AddWithValue("@MobileNo", txtmob.Text);
                cmd.Parameters.AddWithValue("@EmailID", txtEmailID.Text);
                cmd.Parameters.AddWithValue("@CurrentCity", txtLocation.Text);
                cmd.Parameters.AddWithValue("@CurrentCountry", txtCountry.Text);
                cmd.Parameters.AddWithValue("@SignificantAchievements", txtSA.Text);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registration successfully')", true);
                    Rdata();
                    Response.Redirect("StudentDetailsView.aspx");
                }
         }
             }
          catch
        {
        }
    }
}