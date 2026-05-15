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

public partial class Student_StudentDetailsView1 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    DL.StudentDetailsViewDL sdvDL = new DL.StudentDetailsViewDL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("select M.[Enrollment No_],M.Faculty,E.[Search Name] as Mentor  from [TMU$Student Details Mentorship] M inner join [TMU$Employee] E on M.Faculty=E.No_  where M.No_='" + Session["uid"].ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                con.Close();
                if (dt1.Rows.Count > 0)
                {
                    txtRollNo.Text = dt1.Rows[0]["Enrollment No_"].ToString();
                    txtMentor.Text = dt1.Rows[0]["Mentor"].ToString();

                }
               

                
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
                PopupForm();
                Popup();

                bindData(Session["uid"].ToString());
                Popup1();
               
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    

    public void Popup1()
    {
        try
        {

            Panel1.Visible = true;

            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal1').modal('show');</script>", false);


        }
        catch (Exception ex)
        {
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

    public void Popup()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_getStudentNotification", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@coursecode", Session["CourseCode"].ToString());
            cmd.Parameters.AddWithValue("@Semester", Session["Semester"].ToString());
            cmd.Parameters.AddWithValue("@loginId", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@AcademicYear", Session["AcademicYear"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                pnlPopup.Visible = true;
                Lblcount.Text = dt.Rows.Count.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal').modal('show');</script>", false);
            }
            else
            {
                pnlPopup.Visible = false;
            }

        }
        catch (Exception ex)
        {
        }
    }
   
    public void PopupForm()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Sp_getStudentExamFormPopup", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@coursecode", Session["CourseCode"].ToString());
            cmd.Parameters.AddWithValue("@Semester", Session["Semester"].ToString());
            cmd.Parameters.AddWithValue("@loginId", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@AcademicYear", Session["AcademicYear"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0 )
            {
                Panel3.Visible = true;
                rptErrors.DataSource = dt;
                rptErrors.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal10').modal('show');</script>", false);
            }
            else
            {
                Panel3.Visible = false;
            }

        }
        catch (Exception ex)
        {
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


    public void bindData(string FacultyCode)
    {

        DataTable dt = new DataTable();
        dt = sdvDL.GetStudentDetails(FacultyCode);
        if (dt.Rows.Count > 0)
        {
            txtStudentNo.Text = Session["uid"].ToString();
            txtRollNo.Text = dt.Rows[0]["Enrollment No_"].ToString();
            txtName.Text = dt.Rows[0]["Student Name"].ToString();
            txtCourse.Text = dt.Rows[0]["Course Code"].ToString();
            txtSection.Text = dt.Rows[0]["Section"].ToString();
            txtDOB.Text = dt.Rows[0]["DOB"].ToString();
            txtEmailID.Text = dt.Rows[0]["E-Mail Address"].ToString();
            txtFatherName.Text = dt.Rows[0]["Fathers Name"].ToString();
            txtMotherName.Text = dt.Rows[0]["Mothers Name"].ToString();
            txtMobileNo.Text = dt.Rows[0]["Mobile Number"].ToString();
            txtCity.Text = dt.Rows[0]["City"].ToString();
            txtAddress.Text = dt.Rows[0]["Address1"].ToString();
            txtCategory.Text = dt.Rows[0]["Quota"].ToString();
            txtAcademicYear.Text = dt.Rows[0]["Academic Year"].ToString();
            txtCurrentYear.Text = dt.Rows[0]["Admitted Year"].ToString();
            txtNADID.Text = dt.Rows[0]["Visa No_"].ToString();
            if (dt.Rows[0]["Type of Course"].ToString() == "1")
            {
                txtSemester.Text = dt.Rows[0]["Semester"].ToString();
            }
            else
            {
                txtSemester.Text = dt.Rows[0]["Year"].ToString();
            }

            txtBatch.Text = dt.Rows[0]["Batch"].ToString();
            // imgStudent.ImageUrl = "~/Handler.ashx?id=" + ID;


        }
        else
        {
            Blank();
        }
        hindidata();
    }
    public void Blank()
    {
        txtStudentNo.Text = "";
        txtCourse.Text = "";
        txtName.Text = "";
        txtSection.Text = "";
        txtEmailID.Text = "";
        txtAcademicYear.Text = "";
        txtFatherName.Text = "";
        txtMotherName.Text = "";
        txtDOB.Text = "";
        txtCategory.Text = "";
        txtMobileNo.Text = "";
        txtCity.Text = "";
        txtAddress.Text = "";
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



        string Sname = ""; string Fname = ""; string Mname = "";
        Sname = Request.Form["SHname"];
        Fname = Request.Form["FHname"];
        Mname = Request.Form["MHname"];
        //

        // Create string variables that contain the patterns

        string id = Session["uid"].ToString();

        byte[] bytes = GetData("select [Student Image]  from [TMU$Student - COLLEGE] where [No_]='" + id + "'").Rows[0]["Student Image"].ToString() == "" ? null : (byte[])GetData("select [Student Image]  from [TMU$Student - COLLEGE] where [No_]='" + id + "'").Rows[0]["Student Image"];
        if (bytes == null)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Update Student Image')", true);
            return;
        }


        if (txtNADID.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter ABC ID.')", true);
            return;
        }
        if (txtNADID.Text.Length < 12)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter valid ABC ID.')", true);
            return;
        }



        if (txtStudentHindi.Text == "" || txtFatherHindi.Text == "" || txtMotherHindi.Text == "")
        {

            if (Sname == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Student Name')", true);
                return;
            }
            else
            {
                var Sresult = Sname.Substring(Sname.Length - 1);
                string SCodePattern = @"^[a-zA-Z0-9._^%$#!~@,-{}+=]*$";
                bool isEmailValid = Regex.IsMatch(Sresult, SCodePattern);
                if (isEmailValid)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Student Name only hindi')", true);
                    return;
                }
            }
            if (Fname == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Father Name')", true);
                return;
            }
            else
            {
                var Fresult = Fname.Substring(Fname.Length - 1);
                string FCodePattern = @"^[a-zA-Z0-9._^%$#!~@,-{}+=]*$";
                bool isZipValid = Regex.IsMatch(Fresult, FCodePattern);
                if (isZipValid)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Father Name only hindi')", true);
                    return;
                }
            }
            if (Mname == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Mother Name')", true);
                return;
            }
            else
            {
                var Mresult = Mname.Substring(Mname.Length - 1);
                string MCodePattern = @"^[a-zA-Z0-9._^%$#!~@,-{}+=]*$";
                bool isPhoneValid = Regex.IsMatch(Mresult, MCodePattern);
                if (isPhoneValid)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Mother Name only hindi')", true);
                    return;
                }
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModalB').modal('show');</script>", false);

    }

    protected void Lblcount_Click(object sender, EventArgs e)
    {
        Response.Redirect("PlacementRegistration.aspx");
    }

    protected void BtnYes_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlCommand cmd = new SqlCommand("update [TMU$Student - COLLEGE] set [E-Mail Address]='" + txtEmailID.Text + "',[Mobile Number]='" + txtMobileNo.Text + "',City='" + txtCity.Text + "',Address1='" + txtAddress.Text + "',[Visa No_]='" + txtNADID.Text + "' where No_='" + Session["uid"].ToString() + "'", con);
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

        Response.Redirect("StudentDetailsView1.aspx");
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
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

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Data Save Successfully'); document.location.href='StudentDetailsView1.aspx';", true);
            }

        }
        catch (Exception ex)
        {
        }




    }

    protected void btnMarkAllRead_Click(object sender, EventArgs e)
    {
        string query = "UPDATE [TMU$Exam Form Fail Transaction] SET [Read By Student on Portal] = 1 WHERE  [Student No_]=@loginId and Semester=@Semester and [Program Code]=@coursecode and [Academic Year]=@AcademicYear";

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
              
                cmd.Parameters.AddWithValue("@coursecode", Session["CourseCode"].ToString());
                cmd.Parameters.AddWithValue("@Semester", Session["Semester"].ToString());
                cmd.Parameters.AddWithValue("@loginId", Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@AcademicYear", Session["AcademicYear"].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("StudentDetailsView1.aspx");
            }
        }

    }
}