using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class Alumni_StudentDetailsView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.StudentDetailsViewDL sdvDL = new DL.StudentDetailsViewDL();
    static int updateCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Popup();
                bindData(Session["uid"].ToString());
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void Popup()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[Sp_getAlumaniCount]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            if (dt.Rows[0]["Result"].ToString() == "False")
            {
                // Response.Redirect("../Alumni/StudentDetailsView.aspx",false);
            }
            else
            {
                Response.Redirect("../Alumni/GoogleForm.aspx", false);
            }
        }
        catch (Exception ex) { }
    }







    public void bindData(string FacultyCode)
    {

        DataTable dt = new DataTable();
        dt = sdvDL.GetStudentDetails(FacultyCode);
        if (dt.Rows.Count > 0)
        {

            txtRollNo.Text = dt.Rows[0]["Enrollment No_"].ToString();
            //txtName.Text = dt.Rows[0]["Student Name"].ToString();
            txtCourse.Text = dt.Rows[0]["Course Code"].ToString();

            txtDOB.Text = dt.Rows[0]["DOB"].ToString();
            drpGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
            txtMentor.Text = dt.Rows[0]["Year of Passing"].ToString();
            TextCollege.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            AlumaniRegistration();
        }
        else
        {
            Blank();
        }

    }


    public void AlumaniRegistration()
    {

        SqlCommand cmd = new SqlCommand("Sp_getStudentAddressAlumani", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtFName.Text = dt.Rows[0]["Student Name"].ToString();
            txtEmailID.Text = dt.Rows[0]["Email ID"].ToString();
            txtMobileNo.Text = dt.Rows[0]["Mobile No_"].ToString();
            txtWhatsNo.Text = dt.Rows[0]["Whatsapp No_"].ToString();
            TextCollege.Text = dt.Rows[0]["College Code"].ToString();

            txtCity.Text = dt.Rows[0]["Current Location City"].ToString();
            txtCountry.Text = dt.Rows[0]["Current Location Country"].ToString();
            txtAddress.Text = dt.Rows[0]["Address1"].ToString();

            txtPAddress.Text = dt.Rows[0]["Address3"].ToString();
            txtLinkin.Text = dt.Rows[0]["LinkedIN URL"].ToString();
            txtFacebook.Text = dt.Rows[0]["Facebook URL"].ToString();
            txtTwitter.Text = dt.Rows[0]["Twitter URL"].ToString();
            //txtDOB1.Text = dt.Rows[0]["Date of Birth"].ToString();
            ddlEng.SelectedValue = dt.Rows[0]["Engagement Type"].ToString();
            txtDesig.Text = dt.Rows[0]["Designation"].ToString();
            txtEmployer.Text = dt.Rows[0]["Employer"].ToString();
            //txtBustype.Text = dt.Rows[0]["Business Type"].ToString();
            //txtCompanyName.Text = dt.Rows[0]["Company Name"].ToString();
            txtHSinstitute.Text = dt.Rows[0]["Higher Studies Institute"].ToString();
            txtHSProgram.Text = dt.Rows[0]["Higher Studies Program"].ToString();
         
           // Textachivemnt.Text = dt.Rows[0]["Significant Achievements"].ToString();
            if (ddlEng.SelectedValue == "1")
            {


                pnl1.Visible = true;
                pnl2.Visible = false;
                pnl3.Visible = false;
                pnl4.Visible = false;



                txtEmployer.Text = dt.Rows[0]["Employer"].ToString();
                txtDesig.Text = dt.Rows[0]["Designation"].ToString();
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
                txtHSinstitute.Text = "";
                txtHAdress.Text = "";
                txtEWebUrl.Text = "";
                txtHSProgram.Text = "";
                txtEduAdmissionYear.Text = "";
                txtExGradYear.Text = "";
                txtFurtherPlan.Text = "";


            }

            if (ddlEng.SelectedValue == "2")
            {
               
                pnl1.Visible = false;
                pnl2.Visible = true;
                pnl3.Visible = false;
                pnl4.Visible = false;

                txtEmployer.Text = "";
                txtDesig.Text = "";
                txtEAddress.Text ="";

                // txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
                txtJDesc.Text = "";
                ddlJDesc.SelectedValue = "";
                txtSDate.Text = "";
                txtCompanyURL.Text = "";
                txtCMail.Text = "";
                txtCompanyTelephone.Text = "";
                txtIndustry.Text ="";

              
               
                txtHSinstitute.Text = "";
                txtHAdress.Text = "";
                txtEWebUrl.Text = "";
                txtHSProgram.Text = "";
                txtEduAdmissionYear.Text = "";
                txtExGradYear.Text = "";
                txtFurtherPlan.Text = "";

                txtEnterName.Text = dt.Rows[0]["Company Name"].ToString();
                txtSelfIndustry.Text = dt.Rows[0]["Business Type"].ToString();
                txtSelfAddress.Text = dt.Rows[0]["Address2"].ToString();
                txtRole.Text = dt.Rows[0]["Designation"].ToString();
                txtSelfCompanyURL.Text = dt.Rows[0]["Company Website"].ToString();




            }

            if (ddlEng.SelectedValue == "3")
            {

                
                pnl1.Visible = false;
                pnl2.Visible = false;
                pnl3.Visible = true;
                pnl4.Visible = false;

                txtEmployer.Text = "";
                txtDesig.Text = "";
                txtEAddress.Text = "";

                // txtEAddress.Text = dt.Rows[0]["Address2"].ToString();
                txtJDesc.Text = "";
                ddlJDesc.SelectedValue = "";
                txtSDate.Text = "";
                txtCompanyURL.Text = "";
                txtCMail.Text = "";
                txtCompanyTelephone.Text = "";
                txtIndustry.Text = "";



              
             
                txtEWebUrl.Text = "";
               
                txtEduAdmissionYear.Text = "";
                txtExGradYear.Text = "";
                txtFurtherPlan.Text = "";

                txtEnterName.Text = "";
                txtSelfIndustry.Text = "";
                txtSelfAddress.Text = "";
                txtRole.Text = "";
                txtSelfCompanyURL.Text = "";

                txtHSinstitute.Text = dt.Rows[0]["Higher Studies Institute"].ToString();
                txtHAdress.Text = dt.Rows[0]["Address2"].ToString();
                txtEWebUrl.Text = dt.Rows[0]["Company Website"].ToString();
                txtHSProgram.Text = dt.Rows[0]["Higher Studies Program"].ToString();
                txtEduAdmissionYear.Text = dt.Rows[0]["Higher Study Admission Yr"].ToString();
                txtExGradYear.Text = dt.Rows[0]["Higher Study Graduation Year"].ToString();
                txtFurtherPlan.Text = dt.Rows[0]["Futher Plan"].ToString(); ;


            }

            if (ddlEng.SelectedValue == "4")
            {
                pnl1.Visible = false;
                pnl2.Visible = false;
                pnl3.Visible = false;
                pnl4.Visible = true;
               
            }

        }
    }


    public void Blank()
    {

        txtCourse.Text = "";
        //txtName.Text = "";
        txtEmailID.Text = "";

        txtDOB.Text = "";
        txtMobileNo.Text = "";
        txtCity.Text = "";

    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {


        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalB').modal('show');</script>", false);

    }

    protected void Lblcount_Click(object sender, EventArgs e)
    {
        Response.Redirect("EventList.aspx");
    }

    protected void BtnYes_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        // SqlCommand cmd = new SqlCommand("update [TMU$Alumni Registration]   set [Email ID]='" + txtEmailID.Text + "',[Mobile No_]='" + txtMobileNo.Text + "',[Current Location City]='" + txtCity.Text + "' ,[Current Location Country]='" + txtCountry.Text + "',[Designation]='" + txtDesig.Text + "',[Business Type]='" + txtBustype.Text + "',[Company Name]='" + txtCompanyName.Text + "',[Higher Studies Institute]='" + txtHSinstitute.Text + "',[Higher Studies Program]='" + txtHSProgram + "',[Employer]='"+txtEmployer.Text+"',[Other Details]='"+txtOtherDetails.Text+"' where [Student No_]='" + Session["uid"].ToString() + "'", con);
        SqlCommand cmd = new SqlCommand("sp_UpdateAluminiInfo", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Email", txtEmailID.Text);
        cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
        cmd.Parameters.AddWithValue("@WhatsNo", txtWhatsNo.Text);
        cmd.Parameters.AddWithValue("@CurrentCity", txtCity.Text);
        cmd.Parameters.AddWithValue("@CurrentCountry", txtCountry.Text);
        cmd.Parameters.AddWithValue("@PresentAdd", txtAddress.Text);
        cmd.Parameters.AddWithValue("@PerAdd", txtPAddress.Text);
        cmd.Parameters.AddWithValue("@Link", txtLinkin.Text);
        cmd.Parameters.AddWithValue("@Facebook", txtFacebook.Text);
        cmd.Parameters.AddWithValue("@Twitter", txtTwitter.Text);
        cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Eng", ddlEng.SelectedValue);







    


        if (ddlEng.SelectedValue == "1")
        {

            cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text);
            cmd.Parameters.AddWithValue("@Designation", txtDesig.Text);
            cmd.Parameters.AddWithValue("@EAddress", txtEAddress.Text);
            cmd.Parameters.AddWithValue("@JDesc", txtJDesc.Text);
            cmd.Parameters.AddWithValue("@ddlJDesc", ddlJDesc.SelectedValue);
            cmd.Parameters.AddWithValue("@SDate", txtSDate.Text);
            cmd.Parameters.AddWithValue("@CompanyURL", txtCompanyURL.Text);
            cmd.Parameters.AddWithValue("@CMail", txtCMail.Text);
            cmd.Parameters.AddWithValue("@CompanyTelephone", txtCompanyTelephone.Text);
           cmd.Parameters.AddWithValue("@Industry", txtIndustry.Text);
            //cmd.Parameters.AddWithValue("@Industry", "");
            //cmd.Parameters.AddWithValue("@SelfAddress", "");
            //cmd.Parameters.AddWithValue("@Role", "");
            //cmd.Parameters.AddWithValue("@SelfCompanyURL", "");
 
        }
        if (ddlEng.SelectedValue == "2")
        {

          
            cmd.Parameters.AddWithValue("@EAddress", txtSelfAddress.Text);
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
        }
        if (ddlEng.SelectedValue == "3")
        {
            cmd.Parameters.AddWithValue("@HigherStudy", txtHSinstitute.Text);
            cmd.Parameters.AddWithValue("@EAddress", txtHAdress.Text);
            cmd.Parameters.AddWithValue("@CompanyURL", txtEWebUrl.Text);
            cmd.Parameters.AddWithValue("@ProgramName1", txtHSProgram.Text);
            cmd.Parameters.AddWithValue("@EduAdmissionYear", txtEduAdmissionYear.Text);
            cmd.Parameters.AddWithValue("@ExGradYear", txtExGradYear.Text);
            cmd.Parameters.AddWithValue("@FurtherPlan", txtFurtherPlan.Text);
        
        }
       

        updateCount = cmd.ExecuteNonQuery();
        con.Close();
        if (updateCount > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Data Submitted Successfully');", true);
            //Response.Redirect("StudentDetailsView.aspx");

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Something went wrong!!!');", true);
            return;
        }
    }
    protected void grdAluminiInbox_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Event")
        {
            Response.Redirect("../Alumni/Eventlist.aspx");
        }


        if (e.CommandName == "Registration")
        {
            Response.Redirect("../Alumni/Registration.aspx");
        }
    }
    protected void ddlEng_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEng.SelectedValue == "1")
        {
            txtEmployer.Visible = true;
            txtDesig.Visible = true;


            txtHSinstitute.Visible = false;
            txtHSProgram.Visible = false;

           
          
           
            lblEmployer.Visible = true;
            lblDesi.Visible = true;
            lblCompanyName.Visible = false;
          
            lblBusinessType.Visible = false;
            pnl1.Visible = true;
            pnl2.Visible = false;
            pnl3.Visible = false;
            pnl4.Visible = false;
        }

        if (ddlEng.SelectedValue == "2")
        {
            txtEmployer.Visible = false;
            txtDesig.Visible = false;
            txtHSinstitute.Visible = false;
            txtHSProgram.Visible = false;
           
          
           
            lblEmployer.Visible = false;
            lblDesi.Visible = false;
            lblCompanyName.Visible = true;
           
            lblBusinessType.Visible = true;
            pnl1.Visible = false;
            pnl2.Visible = true;
            pnl3.Visible = false;
            pnl4.Visible = false;

        }

        if (ddlEng.SelectedValue == "3")
        {

            txtEmployer.Visible = false;
            txtDesig.Visible = false;
            txtHSinstitute.Visible = true;
            txtHSProgram.Visible = true;
          
            
            lblEmployer.Visible = false;
            lblDesi.Visible = false;
            lblCompanyName.Visible = false;
           
            lblBusinessType.Visible = false;
            pnl1.Visible = false;
            pnl2.Visible = false;
            pnl3.Visible = true;
            pnl4.Visible = false;
        }

        if (ddlEng.SelectedValue == "4")
        {
            txtEmployer.Visible = false;
            txtDesig.Visible = false;
            txtHSinstitute.Visible = false;
            txtHSProgram.Visible = false;
           
            
            lblEmployer.Visible = false;
            lblDesi.Visible = false;
            lblCompanyName.Visible = false;
         
            lblBusinessType.Visible = false;
            pnl1.Visible = false;
            pnl2.Visible = false;
            pnl3.Visible = false;
            pnl4.Visible = true;
        }

    }
}