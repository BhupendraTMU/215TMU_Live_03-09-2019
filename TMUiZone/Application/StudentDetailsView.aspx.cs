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

                bindData(Session["uid"].ToString());
                hideUpdate();
                Hindinamedata();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select count(*) 'C' from [tbl_StudentUpdatedRecord]  where studentno='" + Session["uid"].ToString() + "'", con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt2 = new DataTable();
                da1.Fill(dt2);
                con.Close();
                if (dt2.Rows[0]["C"].ToString() == "0")
                {
                    Popup2();

                }


                ModalPopupExtender1.Show();
                divGeneralBodyEnglish.Visible = true;
                Notenglish.Visible = true;
                CheckBox4.Checked = true;
                if (CheckBox5.Checked)
                {
                    divGeneralBodyHindi.Visible = true;
                    Notenglish.Visible = true;
                    ModalPopupExtender1.Show();
                }
                else
                {
                    divGeneralBodyHindi.Visible = false;
                    Notenglish.Visible = false;
                    divGeneralBodyEnglish.Visible = true;
                    Notenglish.Visible = true;

                }

            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public void Popup2()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("Sp_getStudentDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@STNO_", Session["uid"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                txtstno.Text = dt.Rows[0]["No_"].ToString();
                txtSTName10.Text = dt.Rows[0]["Student Name"].ToString();
                txtDOB10.Text = dt.Rows[0]["Date of Birth"].ToString();
                Panel2.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal5').modal('show');</script>", false);
            }
            else
            {
                Panel2.Visible = false;
            }

        }
        catch (Exception ex)
        {
        }
    }
    public void Hindinamedata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select * from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student Data H_E] where [Student No_]='" + Session["uid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtStudentHindi.Text = dt.Rows[0]["Student Name"].ToString();
            txtFatherHindi.Text = dt.Rows[0]["Student Father Name"].ToString();
            txtMotherHindi.Text = dt.Rows[0]["Student Mother Name"].ToString();

        }
    }
    public void bindData(string FacultyCode)
    {

        DataTable dt = new DataTable();
        dt = sdvDL.GetStudentDetails(FacultyCode);
        if (dt.Rows.Count > 0)
        {
            txtRollNo.Text = dt.Rows[0]["Enrollment No_"].ToString();
            txtName.Text = dt.Rows[0]["Student Name"].ToString();
            txtCourse.Text = dt.Rows[0]["Course Code"].ToString();
            txtfathername.Text = dt.Rows[0]["Fathers Name"].ToString();
            txtMothername.Text = dt.Rows[0]["Mothers Name"].ToString();
            txtnameofcollege.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            txtyearofaddmission.Text = dt.Rows[0]["Academic Year"].ToString();
            txtreligion.Text = dt.Rows[0]["Religion"].ToString();
            txtcategory.Text = dt.Rows[0]["Category"].ToString();
            txtgender.Text = dt.Rows[0]["Gender1"].ToString();
            txtnationality.Text = dt.Rows[0]["Nationality"].ToString();
            txtDOB.Text = dt.Rows[0]["DOB"].ToString();
            txtmobileno.Text = dt.Rows[0]["Mobile Number"].ToString();
            txtparentsmob.Text = dt.Rows[0]["Father Mobile No"].ToString();
            txtsudentEmailID.Text = dt.Rows[0]["E-Mail Address"].ToString();
            txtEmailID.Text = dt.Rows[0]["Alternate Email Address"].ToString();
            txtcorrespondence.Text = dt.Rows[0]["Address1"].ToString();
            txtdistrictcorres.Text = dt.Rows[0]["City"].ToString();
            txtstatecorres.Text = dt.Rows[0]["State"].ToString();
            txtpincodecorre.Text = dt.Rows[0]["Post Code"].ToString();
            txtcountrycorre.Text = dt.Rows[0]["Country Code"].ToString();
            txtperaddress.Text = dt.Rows[0]["Address2"].ToString();
            txtperdistrict.Text = dt.Rows[0]["City"].ToString();
            txtperstate.Text = dt.Rows[0]["State"].ToString();
            txtperpincode.Text = dt.Rows[0]["Post Code"].ToString();
            txtpercountry.Text = dt.Rows[0]["Country Code"].ToString();
            txtabcid.Text = dt.Rows[0]["Visa No_"].ToString();
            //txtMentor.Text = dt.Rows[0]["Year of Passing"].ToString();
            // TextCollege.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
        }
        else
        {
            Blank();
        }
    }
    public void Blank()
    {
        txtCourse.Text = "";
        txtName.Text = "";
        // txtEmailID.Text = "";

        txtDOB.Text = "";
        //txtMobileNo.Text = "";
        // txtCity.Text = "";

    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked)
        {
            txtperaddress.Text = txtcorrespondence.Text;
            txtperdistrict.Text = txtdistrictcorres.Text;
            txtperstate.Text = txtstatecorres.Text;
            txtpercountry.Text = txtcountrycorre.Text;
            txtperpincode.Text = txtpincodecorre.Text;
        }
        else
        {
            txtperaddress.Text = "";
            txtperdistrict.Text = "";
            txtperstate.Text = "";
            txtpercountry.Text = "";
            txtperpincode.Text = "";

        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {

        if (txtStudentHindi.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Student Name (हिन्दी में)')", true);
            return;
        }
        else
        {
            var Sresult = txtStudentHindi.Text.Substring(txtStudentHindi.Text.Length - 1);
            string SCodePattern = @"^[a-zA-Z0-9._^%$#!~@,-{}+=]*$";
            bool isEmailValid = Regex.IsMatch(Sresult, SCodePattern);
            if (isEmailValid)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Student Name only hindi')", true);
                return;
            }
        }
        if (txtFatherHindi.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Father Name (हिन्दी में)')", true);
            return;
        }
        else
        {
            var Fresult = txtFatherHindi.Text.Substring(txtFatherHindi.Text.Length - 1);
            string FCodePattern = @"^[a-zA-Z0-9._^%$#!~@,-{}+=]*$";
            bool isZipValid = Regex.IsMatch(Fresult, FCodePattern);
            if (isZipValid)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Father Name only hindi')", true);
                return;
            }
        }
        if (txtMotherHindi.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Mother Name (हिन्दी में)')", true);
            return;
        }
        else
        {
            var Mresult = txtMotherHindi.Text.Substring(txtMotherHindi.Text.Length - 1);
            string MCodePattern = @"^[a-zA-Z0-9._^%$#!~@,-{}+=]*$";
            bool isPhoneValid = Regex.IsMatch(Mresult, MCodePattern);
            if (isPhoneValid)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Mother Name only hindi')", true);
                return;
            }

        }
        if (txtabcid.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter ABC ID')", true);
            return;
        }
        if (txtStudentHindi.Text == "" || txtFatherHindi.Text == "" || txtMotherHindi.Text == "" || txtabcid.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill Blank Field')", true);
            return;

        }
        else
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_UpdateStudent", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@No_", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@StudentNo_", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Student_Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Date_of_Birth", txtDOB.Text);
            cmd.Parameters.AddWithValue("@Fathers_Name", txtfathername.Text);
            cmd.Parameters.AddWithValue("@Mothers_Name", txtMothername.Text);
            cmd.Parameters.AddWithValue("@Citizenship", txtnationality.Text);
            cmd.Parameters.AddWithValue("@Address1", txtcorrespondence.Text);
            cmd.Parameters.AddWithValue("@City", txtdistrictcorres.Text);
            cmd.Parameters.AddWithValue("@Post_Code", txtpincodecorre.Text);
            cmd.Parameters.AddWithValue("@Country_Code", txtcountrycorre.Text);
            cmd.Parameters.AddWithValue("@E_Mail_Address", txtsudentEmailID.Text);
            cmd.Parameters.AddWithValue("@Mobile_Number", txtmobileno.Text);
            cmd.Parameters.AddWithValue("@Address2", txtperaddress.Text);
            cmd.Parameters.AddWithValue("@Father_Mobile_No", txtparentsmob.Text);
            cmd.Parameters.AddWithValue("@Alternate_Email_Address", txtEmailID.Text);
            cmd.Parameters.AddWithValue("@ProfileUpdated", "2");
            cmd.Parameters.AddWithValue("@StudentNameHindi", txtStudentHindi.Text);
            cmd.Parameters.AddWithValue("@StudentMotherName", txtMotherHindi.Text);
            cmd.Parameters.AddWithValue("@StudentFatherName", txtFatherHindi.Text);
            cmd.Parameters.AddWithValue("@VisaNo_", txtabcid.Text);
            if (con1.State == ConnectionState.Open)
            { con1.Close(); }
            con1.Open();
            cmd.ExecuteNonQuery();
            con1.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your details have been Update successfully.'); document.location.href='StudentDetailsView.aspx';", true);
          
        }

    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand("", con);
        cmd.ExecuteNonQuery();
        con.Close();


        hindidata();
        bindData(Session["uid"].ToString());
        // Create string variables that contain the patterns

        if (txtStudentHindi.Text == "" || txtFatherHindi.Text == "" || txtMotherHindi.Text == "")
        {
            hindiupdate();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Okay');", true);
        }

        Response.Redirect("StudentDetailsView.aspx");
    }
    public void hideUpdate()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select [Profile Updated] as ProfileUpdated from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string ProfileUpdated = dr["ProfileUpdated"].ToString();
                    con.Close();
                    if (ProfileUpdated == "0")

                    {
                        btn_save.Visible = true;
                        btn_ok.Visible = false;

                    }
                    else if(ProfileUpdated == "2")
                    {
                        btn_ok.Visible = false;
                        
                    }
                    else
                    {
                        btn_save.Visible = false;
                        btn_ok.Visible = true;
                    }


                }
            }
        }

    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (txtStudentHindi.Text == "" || txtFatherHindi.Text == "" || txtMotherHindi.Text == "" || txtabcid.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Fill all Detail And Click Update Button')", true);
            return;
        }
        else
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("pro_StudentOK", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@No_", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@ProfileUpdated", "3");
            if (con1.State == ConnectionState.Open)
            { con1.Close(); }
            con1.Open();
            cmd.ExecuteNonQuery();
            con1.Close();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "usernotfound", "alert('Your detail Already Updated.'); document.location.href='StudentDetailsView.aspx';", true);
        }

    }
    public void hindiupdate()
    {

        if (con.State == ConnectionState.Closed)
            con.Open();

        string Sname = ""; string Fname = ""; string Mname = "";
        Sname = Request.Form["SHname"];
        Fname = Request.Form["FHname"];
        Mname = Request.Form["MHname"];
        con.Close();
        int i = validateHindiINput(Sname);
        int j = validateHindiINput(Fname);
        int k = validateHindiINput(Mname);
        if (i == 0 && j == 0 && k == 0)
        {
            SqlCommand cmd = new SqlCommand("update [TMU$Student Data H_E] set [Student Name]=N'" + Sname + "',[Student Father Name]=N'" + Fname + "',[Student Mother Name]=N'" + Mname + "' where [Student No_]='" + Session["uid"].ToString() + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        else

        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Names  in hindi only')", true);
            return;

        }
    }
    public int validateHindiINput(string input)
    {
        SqlCommand cmd = new SqlCommand("Sp_setHindiUpdateName", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@INPUT", input);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        return
        Convert.ToInt32(dt.Rows[0]["Result"]);
    }

    public void hindidata()
    {
        SqlCommand cmd = new SqlCommand("proc_fatchStudentDetailsHindi", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@ID", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Close();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            if (dt.Rows[0]["StudentHindiName"].ToString() == "" || dt.Rows[0]["FatherHindiName"].ToString() == "" || dt.Rows[0]["MotherHindiName"].ToString() == "")
            {

                txtStudentHindi.Visible = false;
                txtFatherHindi.Visible = false;
                txtMotherHindi.Visible = false;
                DSH.Visible = true;
                DFH.Visible = true;
                DMH.Visible = true;

            }
            else
            {

                txtStudentHindi.Text = dt.Rows[0]["StudentHindiName"].ToString();
                txtFatherHindi.Text = dt.Rows[0]["FatherHindiName"].ToString();
                txtMotherHindi.Text = dt.Rows[0]["MotherHindiName"].ToString();

            }
        }
        else
        {
            DSH.Visible = false;
            DFH.Visible = false;
            DMH.Visible = false;
            txtStudentHindi.Visible = true;
            txtFatherHindi.Visible = true;
            txtMotherHindi.Visible = true;
            txtStudentHindi.Enabled = false;
            txtFatherHindi.Enabled = false;
            txtMotherHindi.Enabled = false;

        }
    }
    protected void btnHide_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }

    protected void CheckBox5_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox5.Checked)
        {
            ModalPopupExtender1.Show();
            divGeneralBodyHindi.Visible = true;
            NotHindi.Visible = true;
            divGeneralBodyEnglish.Visible = false;
            Notenglish.Visible = false;
            CheckBox4.Checked = false;
         

        }
        else
        {
            divGeneralBodyEnglish.Visible = true;
            Notenglish.Visible = true;
            divGeneralBodyHindi.Visible = false;
            NotHindi.Visible = false;
            ModalPopupExtender1.Show();
            CheckBox4.Checked = true;

        }

    }

    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox4.Checked)
        {
            divGeneralBodyEnglish.Visible = true;
            Notenglish.Visible = true;
            divGeneralBodyHindi.Visible = false;
            NotHindi.Visible = false;
            ModalPopupExtender1.Show();
            CheckBox5.Checked = false;

        }
        else
        {
            divGeneralBodyEnglish.Visible = true;
            Notenglish.Visible = true;
            ModalPopupExtender1.Show();
            //CheckBox5.Checked = true;

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            SqlCommand cmd = new SqlCommand("InsertUpdatedRecord", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@studentno", txtstno.Text);
            cmd.Parameters.AddWithValue("@studentname10", txtSTName10.Text);
            cmd.Parameters.AddWithValue("@studentnameadhar", txtSTNameA.Text);
            cmd.Parameters.AddWithValue("@dob10", txtDOB10.Text);
            cmd.Parameters.AddWithValue("@doba", txtDOBA.Text);
            cmd.Parameters.AddWithValue("@mobile", txtMobileA.Text);
            cmd.Parameters.AddWithValue("@adharno", txtAdhar.Text);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result > 0)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Data Save Successfully'); document.location.href='StudentDetailsView.aspx';", true);
            }

        }
        catch (Exception ex)
        {
        }




    }
}
