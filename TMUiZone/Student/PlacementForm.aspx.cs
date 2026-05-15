using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

public partial class Student_PlacementForm : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindStudentData();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string contentType1 = "", contentType2 = "";
        byte[] Resume = new byte[720], Photo = new byte[720];
        if (txtResumeUploader.HasFile)
        {
            contentType1 = txtResumeUploader.PostedFile.ContentType;
            string filename = Path.GetFileName(txtResumeUploader.PostedFile.FileName);
            using (Stream fs = txtResumeUploader.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Resume = br.ReadBytes((Int32)fs.Length);
                }
            }
        }
        if (txtPhotoUploader.HasFile)
        {
            contentType2 = txtPhotoUploader.PostedFile.ContentType;
            string filename = Path.GetFileName(txtPhotoUploader.PostedFile.FileName);
            using (Stream fs = txtPhotoUploader.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)fs.Length);
                }
            }
        }

        string email = txtMail.Text;
        string emailAlter = txtAlternateMobile.Text;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        if (!match.Success)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'You have entered wrong email id.');", true);

            return;

        }
        Regex regex1 = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match1 = regex1.Match(email);
        if (!match1.Success)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'You have entered wrong Alternate email id.');", true);

            return;

        }
        if (contentType1 != "application/pdf")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload Resume PDF File Only');", true);

            return;
        }
        if (contentType2 != "image/jpeg")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload Photo JPEG File Only');", true);

            return;
        }


        SqlCommand cmd = new SqlCommand("sp_InsertPlacementData", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentName", txtName.Text);
        cmd.Parameters.AddWithValue("@PerAddress", txtPAddress.Text);
        cmd.Parameters.AddWithValue("@CurrAddress", CAddress.Text);
        cmd.Parameters.AddWithValue("@State", txtState.Text);
        cmd.Parameters.AddWithValue("@Mail", txtMail.Text);
        cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
        cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
        cmd.Parameters.AddWithValue("@Gender", drpGender.SelectedValue);
        cmd.Parameters.AddWithValue("@Category", drpCategory.SelectedValue);
        cmd.Parameters.AddWithValue("@PgDegree", txtPGDegree.Text);
        cmd.Parameters.AddWithValue("@PgBranch", txtPGBranch.Text);
        cmd.Parameters.AddWithValue("@Major", txtMajor.Text);
        cmd.Parameters.AddWithValue("@Minor", txtMinor.Text);
        cmd.Parameters.AddWithValue("@PGCollege", PGCollege.Text);
        cmd.Parameters.AddWithValue("@PGUniversity", PGUniv.Text);
        cmd.Parameters.AddWithValue("@PGPer", txtPer.Text);
        cmd.Parameters.AddWithValue("@PGPassingYear", txtPasingYearPG.Text);
        cmd.Parameters.AddWithValue("@UGDegree", txtUGDegree.Text);
        cmd.Parameters.AddWithValue("@UGBranch", txtUGBranch.Text);
        cmd.Parameters.AddWithValue("@UGCollege", txtUGCollege.Text);
        cmd.Parameters.AddWithValue("@UGUniversity", txtUGUniversity.Text);
        cmd.Parameters.AddWithValue("@UGPer", txtUGPer.Text);
        cmd.Parameters.AddWithValue("@UGPassingYear", txtPassingYearUG.Text);
        cmd.Parameters.AddWithValue("@Diploma", txtDiploma.Text);
        cmd.Parameters.AddWithValue("@DiplomaBranch", txtDiplomaBranch.Text);
        cmd.Parameters.AddWithValue("@DiplomaCollege", txtDiplomaCol.Text);
        cmd.Parameters.AddWithValue("@DiplomaUniversity", txtDiplomaUniv.Text);
        cmd.Parameters.AddWithValue("@DiplomaPer", txtDiplomaPer.Text);
        cmd.Parameters.AddWithValue("@DiplomaPassingYear", txtPassingYearDip.Text);
        cmd.Parameters.AddWithValue("@12thSchoolName", txt12SchoolName.Text);
        cmd.Parameters.AddWithValue("@12BoardName", txtboardName12.Text);
        cmd.Parameters.AddWithValue("@12thPer", txt12per.Text);
        cmd.Parameters.AddWithValue("@12thPassingYear", passingYear12.Text);

        cmd.Parameters.AddWithValue("@10thSchoolName", txt10thSchoolName.Text);
        cmd.Parameters.AddWithValue("@10BoardName", txt10thBoardName.Text);
        cmd.Parameters.AddWithValue("@10thPer", txt10thPer.Text);
        cmd.Parameters.AddWithValue("@10thPassingYear", txt10PassingYear.Text);
        cmd.Parameters.AddWithValue("@TotalBacklog", txtTBacklogNumber.Text);
        cmd.Parameters.AddWithValue("@Clearedbacklog", txtCbacklogNumber.Text);
        cmd.Parameters.AddWithValue("@Pendingbacklog", txtPBacklogNumber.Text);


        cmd.Parameters.AddWithValue("@Internship", "0");
        cmd.Parameters.AddWithValue("@Topic", "");
        cmd.Parameters.AddWithValue("@Duration", "");
        cmd.Parameters.AddWithValue("@NameOfCom", "");

        if (drpPlacement.Text == "0")
        {

            cmd.Parameters.AddWithValue("@NeedPlacementAssistance", "0");
        }
        if (drpPlacement.Text == "1")
        {

            cmd.Parameters.AddWithValue("@NeedPlacementAssistance", "1");
        }


        cmd.Parameters.AddWithValue("@Status", 0);
        cmd.Parameters.AddWithValue("@AlterMobile", txtAlternateMobile.Text);
        cmd.Parameters.AddWithValue("@PersonalEmail", txtPersonalEmail.Text);
        cmd.Parameters.AddWithValue("@EnrollmentNo", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@OpentoRelocate", drpRelocate.SelectedValue);
        cmd.Parameters.AddWithValue("@Disability", drpDisability.SelectedValue);
        cmd.Parameters.AddWithValue("@VAC1", txtVAC1.Text);
        cmd.Parameters.AddWithValue("@VAC2", txtVAC2.Text);
        cmd.Parameters.AddWithValue("@VAC3", txtVAC3.Text);
        cmd.Parameters.AddWithValue("@VAC4", txtVAC4.Text);
        cmd.Parameters.AddWithValue("@VAC5", txtVAC5.Text);

        cmd.Parameters.AddWithValue("@HigherStudies", drpHigher.SelectedValue);
        cmd.Parameters.AddWithValue("@PUniversity", txtPUniversity.Text);
        cmd.Parameters.AddWithValue("@PCourse", txtPCourse.Text);

        cmd.Parameters.AddWithValue("@FamilyBusiness", drpFamily.SelectedValue);

        cmd.Parameters.AddWithValue("@NameOfEnterPrise", NameOfEnterPrise.Text);
        cmd.Parameters.AddWithValue("@NLocation", NLocation.Text);
        cmd.Parameters.AddWithValue("@GSTINNo", GSTINNo.Text);
        cmd.Parameters.AddWithValue("@StartUp", drpStartUp.SelectedValue);
        cmd.Parameters.AddWithValue("@TypeOfStartUp", txtTypeofStartUp.Text);
        cmd.Parameters.AddWithValue("@Other", txtOthers.Text);
        cmd.Parameters.AddWithValue("@ProgLanguagesknown", "");
        cmd.Parameters.AddWithValue("@Training", "");
        cmd.Parameters.AddWithValue("@Placedcompany", "");
        cmd.Parameters.AddWithValue("@Package", "");
        cmd.Parameters.AddWithValue("@OfferLetter", "");
        cmd.Parameters.AddWithValue("@FatherName", "");

        cmd.Parameters.AddWithValue("@Resume", Resume);
        cmd.Parameters.AddWithValue("@Photo", Photo);






        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Form Submitted Successfully');", true);


        bindStudentData();


    }
    public void bindStudentData()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("Sp_StudentDataForPlacement", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StudentNo_", Session["uid"].ToString());
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtName.Text = dt.Rows[0]["Student Name"].ToString();
            txtPAddress.Text = dt.Rows[0]["Address1"].ToString();
            txtState.Text = dt.Rows[0]["State"].ToString();
            txtDOB.Text = dt.Rows[0]["DOB"].ToString();
            drpGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
            drpCategory.SelectedValue = dt.Rows[0]["Category"].ToString();



            txtName.Text = dt.Rows[0]["Student Name"].ToString();
            txtPAddress.Text = dt.Rows[0]["Address1"].ToString();
            CAddress.Text = dt.Rows[0]["Address1"].ToString();
            txtState.Text = dt.Rows[0]["State"].ToString();
            txtMail.Text = dt.Rows[0]["Mail"].ToString();
            txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
            txtDOB.Text = dt.Rows[0]["DOB"].ToString();
            drpGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
            drpCategory.SelectedValue = dt.Rows[0]["Category"].ToString();
            txtPGDegree.Text = dt.Rows[0]["PgDegree"].ToString();
            txtPGBranch.Text = dt.Rows[0]["PgBranch"].ToString();
            txtMajor.Text = dt.Rows[0]["Major"].ToString();
            txtMinor.Text = dt.Rows[0]["Minor"].ToString();
            PGCollege.Text = dt.Rows[0]["PGCollege"].ToString();
            PGUniv.Text = dt.Rows[0]["PGUniversity"].ToString();
            txtPer.Text = dt.Rows[0]["PGPer"].ToString();
            txtPasingYearPG.Text = dt.Rows[0]["PGPassingYear"].ToString();
            txtUGDegree.Text = dt.Rows[0]["UGDegree"].ToString();
            txtUGBranch.Text = dt.Rows[0]["UGBranch"].ToString();
            txtUGCollege.Text = dt.Rows[0]["UGCollege"].ToString();
            txtUGUniversity.Text = dt.Rows[0]["UGUniversity"].ToString();
            txtUGPer.Text = dt.Rows[0]["UGPer"].ToString();
            txtPassingYearUG.Text = dt.Rows[0]["UGPassingYear"].ToString();
            txtDiploma.Text = dt.Rows[0]["Diploma"].ToString();
            txtDiplomaBranch.Text = dt.Rows[0]["DiplomaBranch"].ToString();
            txtDiplomaCol.Text = dt.Rows[0]["DiplomaCollege"].ToString();
            txtDiplomaUniv.Text = dt.Rows[0]["DiplomaUniversity"].ToString();
            txtDiplomaPer.Text = dt.Rows[0]["DiplomaPer"].ToString();
            txtPassingYearDip.Text = dt.Rows[0]["DiplomaPassingYear"].ToString();
            txt12SchoolName.Text = dt.Rows[0]["12thSchoolName"].ToString();
            txtboardName12.Text = dt.Rows[0]["12BoardName"].ToString();
            txt12per.Text = dt.Rows[0]["12thPer"].ToString();
            passingYear12.Text = dt.Rows[0]["12thPassingYear"].ToString();

            txt10thSchoolName.Text = dt.Rows[0]["10thSchoolName"].ToString();
            txt10thBoardName.Text = dt.Rows[0]["10BoardName"].ToString();
            txt10thPer.Text = dt.Rows[0]["10thPer"].ToString();
            txt10PassingYear.Text = dt.Rows[0]["10thPassingYear"].ToString();


            txtTBacklogNumber.Text = dt.Rows[0]["TotalBacklog"].ToString();
            txtCbacklogNumber.Text = dt.Rows[0]["Clearedbacklog"].ToString();
            txtPBacklogNumber.Text = dt.Rows[0]["Pendingbacklog"].ToString();
            txtVAC1.Text = dt.Rows[0]["VAC1"].ToString();
            txtVAC2.Text = dt.Rows[0]["VAC2"].ToString();
            txtVAC3.Text = dt.Rows[0]["VAC3"].ToString();
            txtVAC4.Text = dt.Rows[0]["VAC4"].ToString();
            txtVAC5.Text = dt.Rows[0]["VAC5"].ToString();
            txtOthers.Text = dt.Rows[0]["Others"].ToString();
            drpDisability.SelectedValue = dt.Rows[0]["Disability"].ToString();

            drpPlacement.SelectedValue = dt.Rows[0]["NeedPlacementAssistance"].ToString();

            drpHigher.SelectedValue = dt.Rows[0]["HigherStudies"].ToString();

            drpRelocate.SelectedValue = dt.Rows[0]["OpentoRelocate"].ToString();
            if (dt.Rows[0]["Resume"].ToString() == "")
            {

                txtResumeUploader.Visible = true;
                Label75.Visible = false;
                txtPhotoUploader.Visible = true;
                Label76.Visible = false;
            }
            else
            {
                txtResumeUploader.Visible = false;
                Label75.Visible = true;
                txtPhotoUploader.Visible = false;
                Label76.Visible = true;
            }

            if (dt.Rows[0]["NeedPlacementAssistance"].ToString() == "1")
            {
                DivAdditional.Visible = true;

            }
            else
            {
                DivAdditional.Visible = false;
            }


            if (dt.Rows[0]["HigherStudies"].ToString() == "2")
            {
                divHigherYes.Visible = true;

                txtPUniversity.Text = dt.Rows[0]["PUniversity"].ToString();
                txtPCourse.Text = dt.Rows[0]["PCourse"].ToString();

            }
            else
            {
                divHigherYes.Visible = false;

            }
            drpFamily.SelectedValue = dt.Rows[0]["FamilyBusiness"].ToString();
            if (dt.Rows[0]["FamilyBusiness"].ToString() == "2")
            {
                divFamily.Visible = true;
                NameOfEnterPrise.Text = dt.Rows[0]["NameOfEnterPrise"].ToString();
                NLocation.Text = dt.Rows[0]["NLocation"].ToString();
                GSTINNo.Text = dt.Rows[0]["GSTINNo"].ToString();
              

            }
            else
            {
                divFamily.Visible = false;
                NameOfEnterPrise.Text = "";
                NLocation.Text = "";
                GSTINNo.Text = ""; 
            }
            drpStartUp.SelectedValue = dt.Rows[0]["StartUp"].ToString();
            if (dt.Rows[0]["StartUp"].ToString() == "2")
            {
                divStartUp.Visible = true;
                txtTypeofStartUp.Text = dt.Rows[0]["TypeOfStartUp"].ToString();
              
            }
            else
            {
                divStartUp.Visible = false;
                txtTypeofStartUp.Text = ""; 
            }








            txtAlternateMobile.Text = dt.Rows[0]["AlterMobile"].ToString();
            txtPersonalEmail.Text = dt.Rows[0]["PersonalEmail"].ToString();


            if (dt.Rows[0]["Status"].ToString() == "0")
            {
                btnSubmit.Visible = false;

            }
            if (dt.Rows[0]["Grad"].ToString() == "UG")
            {
                divUG.Visible = true;
                divpg.Visible = false;
                divDiploma.Visible = true;
            }
            if (dt.Rows[0]["Grad"].ToString() == "PG")
            {
                divUG.Visible = true;
                divpg.Visible = true;
                divDiploma.Visible = true;
            }
            if (dt.Rows[0]["Grad"].ToString() == "DIP")
            {
                divUG.Visible = false;
                divpg.Visible = false;
                divDiploma.Visible = true;
            }
            //if (drpInternship.Text == "1")
            //{
            //    Divtopic.Visible = true;
            //    topic.Visible = true;
            //    duration.Visible = true;
            //    DivDuration.Visible = true;
            //    duration.Visible = true;
            //    DivDuration.Visible = true;
            //    NameOfCompany.Visible = true;
            //    DivNameOfCompany.Visible = true;



            //}
            //else
            //{
            //    Divtopic.Visible = false;
            //    topic.Visible = false;
            //    duration.Visible = false;
            //    DivDuration.Visible = false;
            //    duration.Visible = false;
            //    DivDuration.Visible = false;
            //    NameOfCompany.Visible = false;
            //    DivNameOfCompany.Visible = false;
            //}
        }
    }
    protected void drpPlacement_SelectedIndexChanged(object sender, EventArgs e)
    {






    }
    protected void drpInternship_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drpInternship.Text == "1")
        //{
        //    Divtopic.Visible = true;
        //    topic.Visible = true;
        //    duration.Visible = true;
        //    DivDuration.Visible = true;
        //    duration.Visible = true;
        //    DivDuration.Visible = true;
        //    NameOfCompany.Visible = true;
        //    DivNameOfCompany.Visible = true;
        //}
        //else
        //{
        //    Divtopic.Visible = false;
        //    topic.Visible = false;
        //    duration.Visible = false;
        //    DivDuration.Visible = false;
        //    duration.Visible = false;
        //    DivDuration.Visible = false;
        //    NameOfCompany.Visible = false;
        //    DivNameOfCompany.Visible = false;
        //}
    }
    protected void drpHigher_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpHigher.SelectedIndex == 1)
        {
            divHigherYes.Visible = true;


        }
        else if (drpHigher.SelectedIndex == 2)
        {
            divHigherYes.Visible = false;

            txtPUniversity.Text = "";
            txtPCourse.Text = "";
        }
        else
        {
            divHigherYes.Visible = false;

        }

    }


    protected void drpStartUp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpStartUp.SelectedIndex == 1)
        {
            divStartUp.Visible = true;
        }
        else if (drpFamily.SelectedIndex == 2)
        {
            divStartUp.Visible = false;

            txtTypeofStartUp.Text = "";
        }
        else
        {
            divStartUp.Visible = false;

            txtTypeofStartUp.Text = "";
        }

    }
    protected void drpFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpFamily.SelectedIndex == 1)
        {
            divFamily.Visible = true;
        }
        else if (drpFamily.SelectedIndex == 2)
        {
            divFamily.Visible = false;
            NameOfEnterPrise.Text = "";
            NLocation.Text = "";
            GSTINNo.Text = "";
        }
        else
        {
            divFamily.Visible = false;
            NameOfEnterPrise.Text = "";
            NLocation.Text = "";
            GSTINNo.Text = "";
        }
    }
    protected void drpPlacement_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (drpPlacement.SelectedIndex == 1)
        {
            DivAdditional.Visible = true;

        }
        else
        {
            DivAdditional.Visible = false;
            drpHigher.SelectedIndex = 0;
            txtPUniversity.Text = "";
            txtPCourse.Text = "";
            drpFamily.SelectedIndex = 0;
            NameOfEnterPrise.Text = "";
            NLocation.Text = "";
            GSTINNo.Text = "";
            drpStartUp.SelectedIndex = 0;
            txtTypeofStartUp.Text = "";
        }
    }
}