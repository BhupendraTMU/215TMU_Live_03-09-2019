using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_StudentEnrolment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Hindinamedata();
                binddata();
                hideUpload();
                bindenrollment();
                EnrollmentFee();
                //bindAttachment();
                hidesubmit();
                txtdate.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy"));
                lblDAte.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy"));
                txtdateenrollmentfee.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy"));
                TextBox19.Text = String.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy"));

            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }


        }
    }

    //public void bindAttachment()
    //{
    //    DataTable dt = GetData("select datalength([HighSchoolMarksheet]) HighSchoolMarksheet,datalength([InterMarksheet]) InterMarksheet,datalength([Diploma_final_Year]) Diploma_final_Year,datalength([UG_Final_Year]) UG_Final_Year,datalength([Transfer_Certificate]) as 'Transfer_Certificate',datalength([Character_Certificate]) 'Character_Certificate',datalength([Migration]) Migration,datalength([Gap_Affidavit]) Gap_Affidavit,datalength([Domicile]) Domicile,datalength([Student_Aadhar]) Student_Aadhar,datalength([Guardian_Aadhar]) Guardian_Aadhar,datalength([Addmission_Form]) Addmission_Form from [HRMSPortal].dbo.Tbl_EnrollmentTable where  Student_Number='" + Session["uid"].ToString() + "'");

        //    if (dt.Rows.Count > 0)
        //    {

        //        grdAttachment.DataSource = dt;
        //        grdAttachment.DataBind();
        //    }
        //    else
        //    {
        //        grdAttachment.DataSource = "";
        //        grdAttachment.DataBind();
        //    }
        //}

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
            txtstudendtnameHindi.Text = dt.Rows[0]["Student Name"].ToString();
            txtFathernameHindi.Text = dt.Rows[0]["Student Father Name"].ToString();
            txtMotherNameHindi.Text = dt.Rows[0]["Student Mother Name"].ToString();

            lblSTNameH.Text = dt.Rows[0]["Student Name"].ToString();
            lblFatherH.Text = dt.Rows[0]["Student Father Name"].ToString();
            lblMotherH.Text = dt.Rows[0]["Student Mother Name"].ToString();


        }
    }
    public void EnrollmentFee()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select [Transaction No_],[Sales (LCY)] from [TMU$Cust_ Ledger Entry] where[Customer No_] = '" + Session["uid"].ToString() + "' and Description = 'Enrollment Fee' and [Document Type] = '1'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtrs.Text = dt.Rows[0]["[Sales (LCY)]"].ToString();
            TextBox17.Text = dt.Rows[0]["[Sales (LCY)]"].ToString();
            txtreceipt.Text= dt.Rows[0]["[Transaction No_]"].ToString();
            TextBox18.Text = dt.Rows[0]["[Transaction No_]"].ToString();

        }
    }
    public void binddata()
    {


        byte[] bytes = GetData("select [Student Image] as FacultyImage from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where [No_] = '" + Session["uid"].ToString() + "'").Rows[0]["FacultyImage"].ToString() == "" ? null : (byte[])GetData("select [Student Image] as FacultyImage  from[TMU$Student - COLLEGE] where [No_] = '" + Session["uid"].ToString() + "'").Rows[0]["FacultyImage"];
        if (bytes != null)
        {
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            ImgPrv.ImageUrl = "data:image/png;base64," + base64String;
            Image5.ImageUrl = "data:image/png;base64," + base64String;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        string strSQL = "select [Student Name],[Mothers Name],[Fathers Name],City,State,[Country Code],[E-Mail Address],[Mobile Number],[Father Mobile No], convert(date, [Date of Birth],10) AS [Date of Birth],[Course Name],[Academic Year],[Global Dimension 1 Code],[Address1],[Address2],case when[Gender]='1' then 'Male' else 'Female' end as [Gender],[E-Mail Address],[Mobile Number], Nationality,Category,[Religion] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where [No_] = '" + Session["uid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtstudentname.Text = dt.Rows[0]["Student Name"].ToString();
            lblSTName.Text = dt.Rows[0]["Student Name"].ToString();
            txtprogrambranch.Text = dt.Rows[0]["Course Name"].ToString();
            lblProgram.Text = dt.Rows[0]["Course Name"].ToString();
            txtyearofaddmission.Text = dt.Rows[0]["Academic Year"].ToString();
            lblAdmission.Text = dt.Rows[0]["Academic Year"].ToString();
            lblAC.Text = dt.Rows[0]["Academic Year"].ToString();
           
            txtnameofcollege.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            lblCN.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
            txtdateofbirth.Text = dt.Rows[0]["Date of Birth"].ToString();
            lblDOB.Text= dt.Rows[0]["Date of Birth"].ToString().Remove(10);
            txtmothername.Text = dt.Rows[0]["Mothers Name"].ToString();
            lblMother.Text = dt.Rows[0]["Mothers Name"].ToString();
            txtfathername.Text = dt.Rows[0]["Fathers Name"].ToString();
            lblFather.Text = dt.Rows[0]["Fathers Name"].ToString();
            txtgender.Text = dt.Rows[0]["Gender"].ToString();
            lblGender.Text = dt.Rows[0]["Gender"].ToString();
            txtnationality.Text = dt.Rows[0]["Nationality"].ToString();
            lblNationality.Text = dt.Rows[0]["Nationality"].ToString();
            txtreligion.Text = dt.Rows[0]["Religion"].ToString();
            lblReligion.Text = dt.Rows[0]["Religion"].ToString();
            txtcategory.Text = dt.Rows[0]["Category"].ToString();
            lblCategory.Text = dt.Rows[0]["Category"].ToString();
            lblMinority.Text= "";
            txtaddress.Text = dt.Rows[0]["Address1"].ToString();
            lblAdressC.Text = dt.Rows[0]["Address1"].ToString();
            txtperaddress.Text = dt.Rows[0]["Address2"].ToString();
            lblAddressP.Text = dt.Rows[0]["Address2"].ToString();
            txtdistrict.Text = dt.Rows[0]["City"].ToString();
            lblDistrictC.Text = dt.Rows[0]["City"].ToString();
            txtperdistrict.Text = dt.Rows[0]["City"].ToString();
            lblDistrictP.Text = dt.Rows[0]["City"].ToString();
            txtstate.Text = dt.Rows[0]["State"].ToString();
            lblStateC.Text = dt.Rows[0]["State"].ToString();
            txtperstate.Text = dt.Rows[0]["State"].ToString();
            lblStateP.Text = dt.Rows[0]["State"].ToString();
            txtcountry.Text = dt.Rows[0]["Country Code"].ToString();
            lblCountryC.Text = dt.Rows[0]["Country Code"].ToString();
            txtpercountry.Text = dt.Rows[0]["Country Code"].ToString();
            lblCountryP.Text = dt.Rows[0]["Country Code"].ToString();
            txtstudentmob.Text = dt.Rows[0]["Mobile Number"].ToString();
            lblStMobile.Text = dt.Rows[0]["Mobile Number"].ToString();
            txtparentsmob.Text = dt.Rows[0]["Father Mobile No"].ToString();
            lblParentsMobile.Text = dt.Rows[0]["Father Mobile No"].ToString();
            txtemailid.Text = dt.Rows[0]["E-Mail Address"].ToString();
            lblEmailP.Text = dt.Rows[0]["E-Mail Address"].ToString();
            lblunderteking.Text = dt.Rows[0]["Student Name"].ToString();
            lblunderteking.Text = dt.Rows[0]["Student Name"].ToString();

            //txtperdistrict.Text= dt.Rows[0]["District"].ToString();
            //txtdistrict.Text= dt.Rows[0]["District"].ToString();
            //txtID.Text = dt.Rows[0]["ID"].ToString();
        }
    }
    public void bindenrollment()
    {



        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        string strSQL = "select * from Tbl_EnrollmentTable where [Student_Number] = '" + Session["uid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        if (dt.Rows.Count > 0)
        {
            txtID.Text = dt.Rows[0]["ID"].ToString();
            txtboard10.Text = dt.Rows[0]["10th_Board"].ToString();
            TextBox1.Text = dt.Rows[0]["10th_Board"].ToString();
            txtyearofpassing10.Text = dt.Rows[0]["10th_Passing_Year"].ToString();
            TextBox2.Text = dt.Rows[0]["10th_Passing_Year"].ToString();
            txtnameofcollege10.Text = dt.Rows[0]["10th_Name_college"].ToString();
            TextBox3.Text = dt.Rows[0]["10th_Name_college"].ToString();
            txtboard12.Text = dt.Rows[0]["12th_Board"].ToString();
            TextBox4.Text = dt.Rows[0]["12th_Board"].ToString();
            txtyearofpassing12.Text = dt.Rows[0]["12th_Passing_Year"].ToString();
            TextBox5.Text = dt.Rows[0]["12th_Passing_Year"].ToString();
            txtnameofcollege12.Text = dt.Rows[0]["12th_Name_College"].ToString();
            TextBox6.Text = dt.Rows[0]["12th_Name_College"].ToString();
            txtboardgraduation.Text = dt.Rows[0]["Graduation_Board"].ToString();
            TextBox7.Text = dt.Rows[0]["Graduation_Board"].ToString();
            txtyearofpassinggraduation.Text = dt.Rows[0]["Graduation_Passing_Year"].ToString();
            TextBox8.Text = dt.Rows[0]["Graduation_Passing_Year"].ToString();
            txtnameofcollegegraduation.Text = dt.Rows[0]["Graduation_Passing_Year"].ToString();
            TextBox9.Text = dt.Rows[0]["Graduation_Passing_Year"].ToString();
            txtboardpost.Text = dt.Rows[0]["PostGraduate_Board"].ToString();
            TextBox10.Text = dt.Rows[0]["PostGraduate_Board"].ToString();
            txtyearofpassingpost.Text = dt.Rows[0]["PostGraduate_Passing_Year"].ToString();
            TextBox11.Text = dt.Rows[0]["PostGraduate_Passing_Year"].ToString();
            txtnameofcollegepost.Text = dt.Rows[0]["PostGraduate_Name_College"].ToString();
            TextBox12.Text = dt.Rows[0]["PostGraduate_Name_College"].ToString();
            txtboardany.Text = dt.Rows[0]["Any_Board"].ToString();
            TextBox13.Text = dt.Rows[0]["Any_Board"].ToString();
            txtyearofpassingany.Text = dt.Rows[0]["Any_Passing_Year"].ToString();
            TextBox14.Text = dt.Rows[0]["Any_Passing_Year"].ToString();
            txtnameofcollegeany.Text = dt.Rows[0]["Any_Name_College"].ToString();
            TextBox15.Text = dt.Rows[0]["Any_Name_College"].ToString();
            lblhodstatus1.Text = dt.Rows[0]["Pri_Approval"].ToString();
           
            lblAddmissionDirector.Text= dt.Rows[0]["Director_Approval"].ToString();
            lblenrollmentdept.Text=dt.Rows[0]["EnrollmentDept_Approval"].ToString();
            lblRejectionReasons.Text= dt.Rows[0]["RejectRemarks"].ToString();

        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (txtboard10.Text == "")
        {
            string message1 = "Please Fill 10th Board";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtyearofpassing10.Text == "")
        {
            
            string message1 = "Please Fill 10th Passing Year";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtnameofcollege10.Text == "")
        {
            
            string message1 = "Please Fill 10th College Name";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
       
        
        if (txtboard12.Text == "")
        {

            string message1 = "Please Fill 12th Board";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtyearofpassing12.Text == "")
        {

            string message1 = "Please Fill 12th Passing Year";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
        if (txtnameofcollege12.Text == "")
        {

            string message1 = "Please Fill 12th College Name";
            string script1 = "window.onload = function(){ alert('";
            script1 += message1;
            script1 += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
            return;
        }
      
        if (txtboardgraduation.Enabled == true)
        {

            if (txtboardgraduation.Text == "")
            {
                string message1 = "Please Fill UG Board";
                string script1 = "window.onload = function(){ alert('";
                script1 += message1;
                script1 += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                return;
            }
        }
        if (txtyearofpassinggraduation.Enabled == true)
        {

            if (txtyearofpassinggraduation.Text == "")
            {
                string message1 = "Please Fill UG Passing Year";
                string script1 = "window.onload = function(){ alert('";
                script1 += message1;
                script1 += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                return;
            }
            
        }
        if (txtnameofcollegegraduation.Enabled == true)
        {

            if (txtnameofcollegegraduation.Text == "")
            {
                string message1 = "Please Fill UG College Name";
                string script1 = "window.onload = function(){ alert('";
                script1 += message1;
                script1 += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
                return;
            }
            
        }
        
        //if (txtboardpost.Enabled == true)
        //{

        //    if (txtboardpost.Text == "")
        //    {
        //        string message1 = "Please Fill Post-Graduate Board";
        //        string script1 = "window.onload = function(){ alert('";
        //        script1 += message1;
        //        script1 += "')};";
        //        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //        return;
        //    }            
        //}
        //if (txtyearofpassingpost.Enabled == true)
        //{

        //    if (txtyearofpassingpost.Text == "")
        //    {
        //        string message1 = "Please Fill Post-Graduate Passing Year";
        //        string script1 = "window.onload = function(){ alert('";
        //        script1 += message1;
        //        script1 += "')};";
        //        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //        return;
        //    }
        //}
        //if (txtnameofcollegepost.Enabled == true)
        //{

        //    if (txtnameofcollegepost.Text == "")
        //    {
        //        string message1 = "Please Fill Post-Graduate College Name";
        //        string script1 = "window.onload = function(){ alert('";
        //        script1 += message1;
        //        script1 += "')};";
        //        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script1, true);
        //        return;
        //    }
        //}
      
        SqlCommand cmd = new SqlCommand("pro_InsertEnrollmentDate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Student_Number", Session["enroll"].ToString());
        cmd.Parameters.AddWithValue("@Programee_Branch", txtprogrambranch.Text);
        cmd.Parameters.AddWithValue("@Year_Of_Admission", txtyearofaddmission.Text);
        cmd.Parameters.AddWithValue("@Name_Of_College", txtnameofcollege.Text);
        cmd.Parameters.AddWithValue("@Date_Of_Birth", txtdateofbirth.Text);
        cmd.Parameters.AddWithValue("@Student_Name", Session["Name"].ToString());
        cmd.Parameters.AddWithValue("@Father_Name", txtfathername.Text);
        cmd.Parameters.AddWithValue("@Mother_Name", txtmothername.Text);
        cmd.Parameters.AddWithValue("@Gender", txtgender.Text);
        cmd.Parameters.AddWithValue("@Nationality", txtnationality.Text);
        cmd.Parameters.AddWithValue("@Religion", txtreligion.Text);
        cmd.Parameters.AddWithValue("@Category", txtcategory.Text);
        cmd.Parameters.AddWithValue("@Minority_Status", txtminoritystatus.Text);
        cmd.Parameters.AddWithValue("@Correspondence_Address", txtaddress.Text);
        cmd.Parameters.AddWithValue("@Permanent_Address", txtperaddress.Text);
        cmd.Parameters.AddWithValue("@District", txtdistrict.Text);
        cmd.Parameters.AddWithValue("@State", txtstate.Text);
        cmd.Parameters.AddWithValue("@Country", txtcountry.Text);
        cmd.Parameters.AddWithValue("@Student_Mobile", txtstudentmob.Text);
        cmd.Parameters.AddWithValue("@Parents_Mobile", txtparentsmob.Text);
        cmd.Parameters.AddWithValue("@Email_Id", txtemailid.Text);
        cmd.Parameters.AddWithValue("@10th_Board", txtboard10.Text);
        cmd.Parameters.AddWithValue("@10th_Passing_Year", txtyearofpassing10.Text);
        cmd.Parameters.AddWithValue("@10th_Name_college", txtnameofcollege10.Text);
        cmd.Parameters.AddWithValue("@12th_Board", txtboard12.Text);
        cmd.Parameters.AddWithValue("@12th_Passing_Year", txtyearofpassing12.Text);
        cmd.Parameters.AddWithValue("@12th_Name_College", txtnameofcollege12.Text);
        cmd.Parameters.AddWithValue("@Graduation_Board", txtboardgraduation.Text);
        cmd.Parameters.AddWithValue("@Graduation_Passing_Year", txtyearofpassinggraduation.Text);
        cmd.Parameters.AddWithValue("@Graduate_College_Name", txtnameofcollegegraduation.Text);
        cmd.Parameters.AddWithValue("@PostGraduate_Board", txtboardpost.Text);
        cmd.Parameters.AddWithValue("@PostGraduate_Passing_Year", txtyearofpassingpost.Text);
        cmd.Parameters.AddWithValue("@PostGraduate_Name_College", txtnameofcollegepost.Text);
        cmd.Parameters.AddWithValue("@Any_Board", txtboardany.Text);
        cmd.Parameters.AddWithValue("@Any_Passing_Year", txtyearofpassingany.Text);
        cmd.Parameters.AddWithValue("@Any_Name_College", txtnameofcollegeany.Text);
        cmd.Parameters.AddWithValue("@Undertaking_By", lblunderteking.Text);
        cmd.Parameters.AddWithValue("@Undertaking_Date", txtdate.Text);
        cmd.Parameters.AddWithValue("@Rs", txtrs.Text);
        cmd.Parameters.AddWithValue("@Receipt_No", txtreceipt.Text);
        cmd.Parameters.AddWithValue("@Fee_Date", txtdateenrollmentfee.Text);
        cmd.Parameters.AddWithValue("@Status", "Submit");
        cmd.Parameters.AddWithValue("@Pri_Approval", "Pending");
        cmd.Parameters.AddWithValue("@Director_Approval", "Pending");
        cmd.Parameters.AddWithValue("@EnrollmentDept_Approval", "Pending");
        cmd.Parameters.AddWithValue("@StudentNameHindi", txtstudendtnameHindi.Text);
        cmd.Parameters.AddWithValue("@FatherNameHindi", txtFathernameHindi.Text );
        cmd.Parameters.AddWithValue("@MotherNameHindi", txtMotherNameHindi.Text );
        cmd.Parameters.AddWithValue("@ID", txtID.Text);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string message = "Your details have been saved successfully.";
        string script = "window.onload = function(){ alert('";
        script += message;
        script += "')};";
        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);


    }

    //protected void UploadBtn_Click(object sender, EventArgs e)
    //{
    //    string contentType1 = "";
    //    byte[] Photo = new byte[720];
    //    if (FileUpload1.HasFile)
    //    {
    //        contentType1 = FileUpload1.PostedFile.ContentType;

    //        string filename = Path.GetFileName(FileUpload2.PostedFile.FileName);


    //        using (Stream fs = FileUpload1.PostedFile.InputStream)
    //        {
    //            using (BinaryReader br = new BinaryReader(fs))
    //            {
    //                Photo = br.ReadBytes((Int32)fs.Length);

    //            }

    //        }
    //        if (contentType1 != "application/pdf")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload  PDF File Only');", true);

    //            return;
    //        }

    //        SqlCommand cmd = new SqlCommand("pro_InsertImageData", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
    //        cmd.Parameters.AddWithValue("@Attachment", Photo);
    //        cmd.Parameters.AddWithValue("@AttachmentName", drpuploaddpocument.SelectedItem.Text);
    //        if (con.State == ConnectionState.Open)
    //        { con.Close(); }
    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //        string message = "File Upload Successfully.";
    //        string script = "window.onload = function(){ alert('";
    //        script += message;
    //        script += "')};";
    //        ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
    //        bindAttachment();
    //    }
    //}


    protected void lbl10th_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [HighSchoolMarksheet] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["HighSchoolMarksheet"];
                        contentType = "application/pdf";
                        fileName = "HighSchoolMarksheet";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lbl12th_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [InterMarksheet] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["InterMarksheet"];
                        contentType = "application/pdf";
                        fileName = "InterMarksheet";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lbldipthe_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Diploma_final_Year] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Diploma_final_Year"];
                        contentType = "application/pdf";
                        fileName = "Diploma_final_Year";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblUG_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [UG_Final_Year] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["UG_Final_Year"];
                        contentType = "application/pdf";
                        fileName = "UG_Final_Year";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblTran_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Transfer_Certificate] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Transfer_Certificate"];
                        contentType = "application/pdf";
                        fileName = "Transfer_Certificate";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblCharacter_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Character_Certificate] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Character_Certificate"];
                        contentType = "application/pdf";
                        fileName = "Character_Certificate";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblMigration_Click(object sender, EventArgs e)
    {

        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Migration] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Migration"];
                        contentType = "application/pdf";
                        fileName = "Migration";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblGap_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Gap_Affidavit] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Gap_Affidavit"];
                        contentType = "application/pdf";
                        fileName = "Gap_Affidavit";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblDomicile_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Domicile] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Domicile"];
                        contentType = "application/pdf";
                        fileName = "Domicile";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblAadhar_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Student_Aadhar] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Student_Aadhar"];
                        contentType = "application/pdf";
                        fileName = "Student_Aadhar";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch

        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblGuardian_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Guardian_Aadhar] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Guardian_Aadhar"];
                        contentType = "application/pdf";
                        fileName = "Guardian_Aadhar";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }

    protected void lblAdmission_Click(object sender, EventArgs e)
    {
        try
        {

            byte[] bytes;
            string fileName, contentType;
            using (SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "select [Addmission_Form] from [HRMSPortal].dbo.Tbl_EnrollmentTable where Student_Number ='" + Session["enroll"].ToString() + "'  ";
                    cmd2.Connection = con2;
                    con2.Open();
                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Addmission_Form"];
                        contentType = "application/pdf";
                        fileName = "Addmission_Form";
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+ex+"')", true);
        }
    }
    public void hidesubmit()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select * from Tbl_EnrollmentTable where Student_Number='" + Session["uid"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string Status = dr["Status"].ToString();
                    con.Close();
                    if (Status == "Submit")

                    {
                        btn_save.Visible = false;

                    }
                    else
                    {
                        btn_save.Visible = true;

                    }


                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string contentType1 = "";
        byte[] Photo = new byte[720];
        if (FileUpload1.HasFile)
        {
            contentType1 = FileUpload1.PostedFile.ContentType;

            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);


            using (Stream fs = FileUpload1.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)fs.Length);

                }

            }
            if (contentType1 != "application/pdf")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload  PDF File Only');", true);

                return;
            }

            SqlCommand cmd = new SqlCommand("pro_InsertImageData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Attachment", Photo);
            cmd.Parameters.AddWithValue("@AttachmentName", Label51.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "File Upload Successfully.";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
           // bindAttachment();
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string contentType1 = "";
        byte[] Photo = new byte[720];
        if (FileUpload3.HasFile)
        {
            contentType1 = FileUpload3.PostedFile.ContentType;

            string filename = Path.GetFileName(FileUpload3.PostedFile.FileName);


            using (Stream fs = FileUpload3.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)fs.Length);

                }

            }
            if (contentType1 != "application/pdf")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload  PDF File Only');", true);

                return;
            }

            SqlCommand cmd = new SqlCommand("pro_InsertImageData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Attachment", Photo);
            cmd.Parameters.AddWithValue("@AttachmentName", Label52.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "File Upload Successfully.";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
          //  bindAttachment();

        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string contentType1 = "";
        byte[] Photo = new byte[720];
        if (FileUpload4.HasFile)
        {
            contentType1 = FileUpload4.PostedFile.ContentType;

            string filename = Path.GetFileName(FileUpload4.PostedFile.FileName);


            using (Stream fs = FileUpload4.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)fs.Length);

                }

            }
            if (contentType1 != "application/pdf")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload  PDF File Only');", true);

                return;
            }

            SqlCommand cmd = new SqlCommand("pro_InsertImageData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Attachment", Photo);
            cmd.Parameters.AddWithValue("@AttachmentName", Label53.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "File Upload Successfully.";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            //bindAttachment();

        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        string contentType1 = "";
        byte[] Photo = new byte[720];
        if (FileUpload5.HasFile)
        {
            contentType1 = FileUpload5.PostedFile.ContentType;

            string filename = Path.GetFileName(FileUpload5.PostedFile.FileName);


            using (Stream fs = FileUpload5.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)fs.Length);

                }

            }
            if (contentType1 != "application/pdf")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload  PDF File Only');", true);

                return;
            }

            SqlCommand cmd = new SqlCommand("pro_InsertImageData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Attachment", Photo);
            cmd.Parameters.AddWithValue("@AttachmentName", Label54.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "File Upload Successfully.";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            //bindAttachment();
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        string contentType1 = "";
        byte[] Photo = new byte[720];
        if (FileUpload6.HasFile)
        {
            contentType1 = FileUpload6.PostedFile.ContentType;

            string filename = Path.GetFileName(FileUpload6.PostedFile.FileName);


            using (Stream fs = FileUpload6.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    Photo = br.ReadBytes((Int32)fs.Length);

                }

            }
            if (contentType1 != "application/pdf")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Upload  PDF File Only');", true);

                return;
            }

            SqlCommand cmd = new SqlCommand("pro_InsertImageData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@Attachment", Photo);
            cmd.Parameters.AddWithValue("@AttachmentName", Label55.Text);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            string message = "File Upload Successfully.";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

            //bindAttachment();
        }
    }
    public void hideUpload()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select Graduation from [EDUCOLLEGELIVE-R2].dbo.[TMU$Course - COLLEGE] where Code In(select [Course Code] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Student - COLLEGE] where No_='" + Session["uid"].ToString() + "')", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string Graduation = dr["Graduation"].ToString();
                    con.Close();
                    if (Graduation == "UG")
                    {
                        txtboard10.Enabled = true;
                        txtyearofpassing10.Enabled = true;
                        txtnameofcollege10.Enabled = true;
                        FileUpload1.Enabled = true;
                        Button1.Enabled = true;
                        lbl10th.Enabled = true;
                        txtboard12.Enabled = true;
                        txtyearofpassing12.Enabled = true;
                        txtnameofcollege12.Enabled = true;
                        FileUpload3.Enabled = true;
                        lbl12th.Enabled = true;
                        txtboardgraduation.Enabled = false;
                        txtyearofpassinggraduation.Enabled = false;
                        txtnameofcollegegraduation.Enabled = false;
                        FileUpload4.Enabled = false;
                        Button3.Enabled = false;
                        lblUG.Enabled = false;
                        txtboardpost.Enabled = false;
                        txtyearofpassingpost.Enabled = false;
                        txtnameofcollegepost.Enabled = false;
                        FileUpload5.Enabled = false;
                        lbldipthe.Enabled = false;
                        Button4.Enabled = false;
                    }
                    else
                    {
                        //txtboard12.Enabled = true;
                        txtboardgraduation.Enabled = true;
                        txtyearofpassinggraduation.Enabled = true;
                        txtnameofcollegegraduation.Enabled = true;
                        FileUpload4.Enabled = true;
                        Button3.Enabled = true;
                        lblUG.Enabled = true;
                        txtboardpost.Enabled = true;
                        txtyearofpassingpost.Enabled = true;
                        txtnameofcollegepost.Enabled = true;
                        FileUpload5.Enabled = true;
                        lbldipthe.Enabled = true;
                        Button4.Enabled = true;
                    }


                }
            }
        }
    }
}



