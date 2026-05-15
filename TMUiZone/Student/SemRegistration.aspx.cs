using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;

public partial class Student_SemRegistration : System.Web.UI.Page
{
    static string EnrollmentNo = "";
    string StudentNo = "";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["enroll"].ToString() == null)
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {

                if (!IsPostBack)
                {

                    EnrollmentNo = Session["enroll"].ToString();
                    StudentNo = Session["uid"].ToString();



                    getStudentInformation();


                    getExaminationDetail();
                    BINDimage();

                }

            }

        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");

        }
    }


    public void BINDimage()
    {
        string id = Session["uid"].ToString();

        byte[] bytes = GetData("select [Student Image]  from [TMU$Student - COLLEGE] where [No_]='" + id + "'").Rows[0]["Student Image"].ToString() == "" ? null : (byte[])GetData("select [Student Image]  from [TMU$Student - COLLEGE] where [No_]='" + id + "'").Rows[0]["Student Image"];
        if (bytes != null)
        {
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            ImgStudent.ImageUrl = "data:image/png;base64," + base64String;

        }
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
    public void getExaminationDetail()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_FetchCourseForsemRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            Label37.Text = dt.Rows[0]["Semester"].ToString();
            GrdAppliedExamination.DataSource = dt;
            GrdAppliedExamination.DataBind();
            //if (dt.Rows.Count > 0)
            //{
            //    btnCinvoice.Visible = true;
            //}
            //else
            //{
            //    btnCinvoice.Visible = false;
            //}

        }
        catch (Exception ex)
        {

        }
    }

    public void getStudentInformation()
    {
        try
        {
            con.Close();
            SqlCommand cmd = new SqlCommand("Sp_StudentDetailforSemRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Sem", "");
            SqlDataReader reader = cmd.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (reader.Read())
            {
                txtProgramName.Text = reader["Course Name"].ToString();
                txtAcademic.Text = "20" + reader["Academic Year"].ToString();
                TxtACategory.Text = "NS";
                TxtStudentFName.Text = reader["Student Name"].ToString();
                TxtFatherName.Text = reader["Fathers Name"].ToString();
                TxtFatherMobile.Text = reader["Father Mobile No"].ToString();
                TxtFathersOcc.Text = reader["Fathers Occupation"].ToString();
                TxtMotherName.Text = reader["Mothers Name"].ToString();
                TxtMothersMobile.Text = reader["Mother Mobile No"].ToString();
                TxtHindiMotherOcc.Text = reader["Mothers Occupation"].ToString();
                TxtNationality.Text = reader["Nationality1"].ToString();
                TxtDOB.Text = reader["DOB"].ToString();

                TxtGender.Text = reader["Gender1"].ToString();

                txtCategory.Text = reader["Category1"].ToString();

                txtEmergencyContact.Text = reader["Emergency Contact No_"].ToString();


                txtAddress.Text = reader["CAddress"].ToString();
                txtCity.Text = reader["City"].ToString();

                txtState.Text = reader["State"].ToString();

                txtCountry.Text = reader["Country"].ToString();

                txtPin.Text = reader["Post Code"].ToString();

                txtTel.Text = reader["Telephone"].ToString();

                txtFax.Text = reader["Fax"].ToString();

                txtMobile.Text = reader["Mobile Number"].ToString();

                txtEmail.Text = reader["E-Mail Address"].ToString();

                txtPAddress.Text = reader["Address1"].ToString();

                txtPCity.Text = reader["City"].ToString();

                txtPState.Text = reader["State"].ToString();

                txtPCountry.Text = reader["Country"].ToString();

                txtPPin.Text = reader["Post Code"].ToString();

                txtPTel.Text = reader["Telephone"].ToString();

                txtPFax.Text = reader["Fax"].ToString();
                txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");


                TxtFatherMobile.Text = reader["Father_Mobile_No"].ToString();
                TxtFathersOcc.Text = reader["Father_Occupation"].ToString();
                TxtMothersMobile.Text = reader["Mother_MobileNo"].ToString();
                TxtHindiMotherOcc.Text = reader["MotherOccupation"].ToString();
                txtEmergencyContact.Text = reader["EmergencyContactNo"].ToString();
                txtPin.Text = reader["CPin"].ToString();
                txtTel.Text = reader["CTel"].ToString();
                txtFax.Text = reader["CFax"].ToString();
                txtPPin.Text = reader["PPin"].ToString();
                txtPTel.Text = reader["PTel"].ToString();
                txtPFax.Text = reader["PFax"].ToString();
                txtLGName.Text = reader["LGName"].ToString();
                txtLGAddress.Text = reader["LGAddress"].ToString();
                txtLGCity.Text = reader["LGCity"].ToString();
                txtLGState.Text = reader["LGState"].ToString();
                txtLGCountry.Text = reader["LGCountry"].ToString();
                txtLGPin.Text = reader["LGPin"].ToString();
                txtLGTel.Text = reader["LGTel"].ToString();
                txtLGMobile.Text = reader["LGMobile"].ToString();
                txtLGEmail.Text = reader["LGEmail"].ToString();
                txtHAddress.Text = reader["HAddress"].ToString();
                txtHCity.Text = reader["HCity"].ToString();
                txtHPin.Text = reader["HPin"].ToString();
                txtHTel.Text = reader["HTel"].ToString();
                if (reader["Sichness"].ToString() == "1")
                {
                    chkYes.Checked = true;
                    chkNo.Checked = false;
                    DivDocDetail.Visible = true;

                }
                else
                {
                    chkNo.Checked = true;
                    chkYes.Checked = false;
                    DivDocDetail.Visible = false;

                }
                txtDrName.Text = reader["DrName"].ToString();
                txtDrAddress.Text = reader["DrAddress"].ToString();
                txtDrCity.Text = reader["DrCity"].ToString();
                txtDRState.Text = reader["DrState"].ToString();
                txtDRPin.Text = reader["DrPin"].ToString();
                txtDrTel.Text = reader["DrTel"].ToString();
                txtDRMobile.Text = reader["DrMobile"].ToString();
                txtDREmail.Text = reader["DrEmail"].ToString();
                txtBloodGroup.Text = reader["DrBloodGroup"].ToString();
                txtDate.Text = reader["Date"].ToString();
                txtPlace.Text = reader["Place"].ToString();
                TxtDDAte.Text = reader["Date1"].ToString();
                TextBox2.Text = reader["Date2"].ToString();
                TextBox3.Text = reader["Place1"].ToString();
                if (reader["StudentNo"].ToString() != "")
                {
                    BtnPrint.Visible = true;
                    btnSubmit.Visible = false;
                }
                else
                {
                    BtnPrint.Visible = false;
                    btnSubmit.Visible = true;
                }






                string ImgStudent = reader["Student Image"].ToString();
                con.Close();
                reader.Close();
            }
            else
            {

            }
        }
        catch
        {
        }
    }
    protected void chkStudent_CheckedChanged(object sender, EventArgs e)
    {

    }
    //protected void chkAll_CheckedChanged(object sender, EventArgs e)
    //{

    //}
    protected void chkYes_CheckedChanged(object sender, EventArgs e)
    {
        chkNo.Checked = false;
        DivDocDetail.Visible = true;
    }
    protected void chkNo_CheckedChanged(object sender, EventArgs e)
    {
        chkYes.Checked = false;
        DivDocDetail.Visible = false;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
         decimal total_credit = 0, MaxSubject = 0,  TotalCreditPoint = 0,countMax=0;
         int flag = 0;
         foreach (GridViewRow row in GrdAppliedExamination.Rows)
         {
             CheckBox check = (CheckBox)row.FindControl("chkStudent");
            

             if (check.Checked == true)
             {
                 flag = 1;
             }


         }
         if (flag==0)
         {
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Subject For Register.')", true); return;
         }
        foreach (GridViewRow row in  GrdAppliedExamination.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("chkStudent");
            HiddenField hdfMaxSubject = (HiddenField)row.FindControl("hdfMaxSubject");
            HiddenField hdfTotalCreditPoint = (HiddenField)row.FindControl("HdfTotalCreditPoint");
            HiddenField CreditPoint = (HiddenField)row.FindControl("HdfCreditPoint");
          
            if (check.Checked == true)
             {
                MaxSubject =Convert.ToInt32(hdfMaxSubject.Value);
                TotalCreditPoint = Convert.ToDecimal(hdfTotalCreditPoint.Value);
                total_credit = total_credit + Convert.ToDecimal(CreditPoint.Value);
                countMax=countMax+1;
            }
          
        }
        //if (MaxSubject < countMax)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You can not select more than " + MaxSubject + " Subject.')", true); return;
        //}
        if (TotalCreditPoint > total_credit)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Total selected credits is less from Total credits.')", true); return;
        }
        con.Open();
        foreach (GridViewRow row in GrdAppliedExamination.Rows)
        {
            CheckBox check = (CheckBox)row.FindControl("chkStudent");
            if (check.Checked == true)
            {
                Label LblPracticalCode = (Label)row.FindControl("LblPracticalCode");
                Label LblPracticalDesc = (Label)row.FindControl("LblPracticalName");
                Label LblSemesterCode = (Label)row.FindControl("LblSemester");
                Label LblElective = (Label)row.FindControl("LblOpenElective");
                Label LblCore = (Label)row.FindControl("LblCore");
                Label LblVAC = (Label)row.FindControl("LblVAC");
                Label LblSubjectType = (Label)row.FindControl("LblDE");

                SqlCommand cmd1 = new SqlCommand("sp_subjectsubSemesterRegistration", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
                cmd1.Parameters.AddWithValue("@Semester", LblSemesterCode.Text);
                cmd1.Parameters.AddWithValue("@SubjectCode", LblPracticalCode.Text);
                cmd1.Parameters.AddWithValue("@SubjectDesc", LblPracticalDesc.Text);
                cmd1.Parameters.AddWithValue("@Core", LblCore.Text);
                cmd1.Parameters.AddWithValue("@Elective", LblElective.Text);
                cmd1.Parameters.AddWithValue("@VAC", LblVAC.Text);
                cmd1.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
                cmd1.Parameters.AddWithValue("@SubjectType", LblSubjectType.Text);


                cmd1.ExecuteNonQuery();
            }
        }
        con.Close();
        con1.Open();
        SqlCommand cmd = new SqlCommand("Sp_SaveSemRegisterData", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Student_No", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Father_Mobile_No", TxtFatherMobile.Text);
        cmd.Parameters.AddWithValue("@Father_Occupation", TxtFathersOcc.Text);
        cmd.Parameters.AddWithValue("@Mother_MobileNo", TxtMothersMobile.Text);
        cmd.Parameters.AddWithValue("@MotherOccupation", TxtHindiMotherOcc.Text);
        cmd.Parameters.AddWithValue("@EmergencyContactNo", txtEmergencyContact.Text);
        cmd.Parameters.AddWithValue("@CPin", txtPin.Text);
        cmd.Parameters.AddWithValue("@CTel", txtTel.Text);
        cmd.Parameters.AddWithValue("@CFax", txtFax.Text);
        cmd.Parameters.AddWithValue("@PPin", txtPPin.Text);
        cmd.Parameters.AddWithValue("@PTel", txtPTel.Text);
        cmd.Parameters.AddWithValue("@PFax", txtPFax.Text);
        cmd.Parameters.AddWithValue("@LGName", txtLGName.Text);
        cmd.Parameters.AddWithValue("@LGAddress", txtLGAddress.Text);
        cmd.Parameters.AddWithValue("@LGCity", txtLGCity.Text);
        cmd.Parameters.AddWithValue("@LGState", txtLGState.Text);
        cmd.Parameters.AddWithValue("@LGCountry", txtLGCountry.Text);
        cmd.Parameters.AddWithValue("@LGPin", txtLGPin.Text);
        cmd.Parameters.AddWithValue("@LGTel", txtLGTel.Text);
        cmd.Parameters.AddWithValue("@LGMobile", txtLGMobile.Text);
        cmd.Parameters.AddWithValue("@LGEmail", txtLGEmail.Text);
        cmd.Parameters.AddWithValue("@HAddress", txtHAddress.Text);
        cmd.Parameters.AddWithValue("@HCity", txtHCity.Text);
        cmd.Parameters.AddWithValue("@HPin", txtHPin.Text);
        cmd.Parameters.AddWithValue("@HTel", txtHTel.Text);
       
        if (chkYes.Checked == true)
        {
            cmd.Parameters.AddWithValue("@Sichness", 1);
        }
        else
        {
            cmd.Parameters.AddWithValue("@Sichness", 0);
        }
        cmd.Parameters.AddWithValue("@DrName", txtDrName.Text);
        cmd.Parameters.AddWithValue("@DrAddress", txtDrAddress.Text);
        cmd.Parameters.AddWithValue("@DrCity", txtDrCity.Text);
        cmd.Parameters.AddWithValue("@DrState", txtDRState.Text);
        cmd.Parameters.AddWithValue("@DrPin", txtDRPin.Text);
        cmd.Parameters.AddWithValue("@DrTel", txtDrTel.Text);
        cmd.Parameters.AddWithValue("@DrMobile", txtDRMobile.Text);
        cmd.Parameters.AddWithValue("@DrEmail", txtDREmail.Text);
        cmd.Parameters.AddWithValue("@DrBloodGroup", txtBloodGroup.Text);
        cmd.Parameters.AddWithValue("@Date", txtDate.Text);
        cmd.Parameters.AddWithValue("@Place", txtPlace.Text);
        cmd.Parameters.AddWithValue("@Date1", TxtDDAte.Text);
        cmd.Parameters.AddWithValue("@Date2", TextBox2.Text);
        cmd.Parameters.AddWithValue("@Place1", TextBox3.Text);
       
        cmd.Parameters.AddWithValue("@CAdd", txtAddress.Text);
        cmd.Parameters.AddWithValue("@CCity", txtCity.Text);
        cmd.Parameters.AddWithValue("@CState", txtState.Text);
        cmd.Parameters.AddWithValue("@CCountry", txtCountry.Text);
        cmd.Parameters.AddWithValue("@CMobile", txtMobile.Text);
        cmd.Parameters.AddWithValue("@CMail", txtEmail.Text);

        cmd.ExecuteNonQuery();
        con1.Close();


        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Save SuccessFully')", true); 

        getStudentInformation();


        getExaminationDetail();
        BINDimage();

    }
}