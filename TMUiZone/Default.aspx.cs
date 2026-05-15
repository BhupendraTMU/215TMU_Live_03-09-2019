using DL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
//-----------------------HR----------------
using System.Text;
using System.Web.UI;

public partial class Default : System.Web.UI.Page
{
    TMUConnection TMUcon;
    DL.CommonDL commonDl = new CommonDL();
    //------------------HR--------------
    Connection Portalcon;
    ServicePoratal con;

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = "ERP Login | TMU Moradabad | Best Private University in UP";
        Page.MetaDescription = "Welcome to the Teerthanker Mahaveer University (TMU) ERP portal. Get access to your attendance sheet, holidays, profile, event details, and more. Login Now!";
        Page.MetaKeywords = "Best private university in UP, TMU ERP portal, ERP login, Teerthanker Mahaveer University, Best Private university in Moradabad";

        TMUcon = new TMUConnection();
        //------------------HR--------------' OR '1'='1'
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
        }
        catch (Exception)
        {
            //  ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please try again');", true);
        }
        //------------------HR--------------
        if (!IsPostBack)
        {
            if ((Session["Enroll"] != null || Session["uid"] != null) && (Session["Pass"] != null || Session["Passw"] != null) && (Session["Enroll"] != null || Session["uid"] != null) && (Session["Pass"] != null || Session["Passw"] != null))
            {


                if (Session["Enroll"] != null && Session["Enroll"] != "")
                {
                    txtUserid.Text = Session["Enroll"].ToString();
                }
                else
                {
                    txtUserid.Text = Session["uid"].ToString();
                }
                if (Session["Pass"] != null && Session["Pass"] != "")
                {
                    txtpassword.Text = Session["Pass"].ToString();
                }
                else
                {

                    txtpassword.Text = Session["Passw"].ToString();
                }
                Session["Enroll"] = "";
                Session["Pass"] = "";
                ImgBttn_Login_Click1(sender, e);

            }
            else
            {
                if (Request.Cookies["userid1"] != null)
                    txtUserid.Attributes.Add("value", Request.Cookies["userid1"].Value);
                if (Request.Cookies["pwd"] != null)
                    txtpassword.Attributes.Add("value", Request.Cookies["pwd"].Value);
                if (Request.Cookies["userid"] != null && Request.Cookies["pwd"] != null)
                    chkrem.Checked = true;
            }
        }
    }


    //protected void ImgBttn_Login_Click(object sender, ImageClickEventArgs e)
    //{


    //}
    protected void ImgBttn_Login_Click1(object sender, EventArgs e)
    {
        //SqlDataAdapter da = new SqlDataAdapter("select [Enrollment No_],s.[No_] as [Login ID],s.Password,'Student' [User Group],s.[Student Name],s.[Admitted Year],s.[Academic Year], s.[Course Code],s.Semester,s.Year,s.Section,s.[Global Dimension 1 Code],s.[Student Status],(select  Count(*) from HRMSPortal.dbo.Tbl_EnrollmentTable with (NOLOCK) where Student_Number=Upper('" + txtUserid.Text + "') and (Doc_Verify_Status='Approved'  and s.[Enrollment No_]!='')) as Status from [TMU$Student - COLLEGE] s where [No_]=Upper('" + txtUserid.Text + "' ) and (Password='" + txtpassword.Text + "' or 'SRPAdmin@123'= '" + txtpassword.Text + "') and [Global Dimension 1 Code] !='TMMC'", Portalcon.Con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);


        SqlCommand cmd = new SqlCommand("proc_GetStudentDetailbyNo_", Portalcon.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentNo_", txtUserid.Text);
        cmd.Parameters.AddWithValue("@Password_", txtpassword.Text);
        SqlDataAdapter daStudent = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        daStudent.Fill(dt);


        SqlCommand cmd1 = new SqlCommand("proc_GetStudentDetailbyNo1_", Portalcon.Con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@StudentNo_", txtUserid.Text);
        cmd1.Parameters.AddWithValue("@Password_", txtpassword.Text);
        SqlDataAdapter daStudent1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        daStudent1.Fill(dt1);

        string UserGroup = Portalcon.UserGroup(txtUserid.Text.Trim().ToUpper());

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Status"].ToString() == "0")
            {
                Session["enroll"] = txtUserid.Text.Trim();
                Session["uid"] = dt.Rows[0]["Login ID"].ToString();
                Session["Name"] = dt.Rows[0]["Student Name"].ToString();
                Session["CourseCode"] = dt.Rows[0]["Course Code"].ToString();
                Session["Semester"] = dt.Rows[0]["Semester"].ToString();
                Session["Section"] = dt.Rows[0]["Section"].ToString();
                Session["Year"] = dt.Rows[0]["Year"].ToString();
                Session["College"] = dt.Rows[0]["Global Dimension 1 Code"].ToString();
                Session["AcademicYear"] = dt.Rows[0]["Academic Year"].ToString();
                Session["Passw"] = dt.Rows[0]["Password"].ToString();
                Response.Redirect("Application/StudentDetailsView.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Login through Enrollment No.');", true);
            }
        }

        if (dt1.Rows.Count > 0)
        {
            if (UserGroup == "STUDENT")
            {
                if (dt1.Rows[0]["Status"].ToString() != "0" || dt1.Rows[0]["Admitted Year"].ToString() != "25-26" )
                {
                    txtUserid.Text = txtUserid.Text.Trim().ToUpper();
                    Session["AcademicYear"] = TMUcon.AcademicYear();
                    LoginDetail();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload All Documents through ST No. Login');", true);
                }
            }
        }


        else
        {
            txtUserid.Text = txtUserid.Text.Trim().ToUpper();
            Session["AcademicYear"] = TMUcon.AcademicYear();
            LoginDetail();
        }


    }


    //-------------------------HR----------------------
    public void companyShow()
    {
        //SqlDataReader dr = Portalcon.CompanyName();
        //// dr.Read();
        //ddCompanyName.DataSource = dr;
        //ddCompanyName.DataTextField = "Name";
        //ddCompanyName.DataBind();
        //dr.Close();
        //Portalcon.DisConnect();
    }
    public void showhodEmailid()
    {
        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$Employee" + "]";
        SqlDataReader dr = Portalcon.Show_HODEmail(tblNameEmployee, Session["HODLoginPage"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            Session["hod_email2"] = dr["Company E-Mail"].ToString();
            Session["hod_Name2"] = dr["First Name"].ToString() + "  " + dr["Middle Name"].ToString() + " " + dr["Last Name"].ToString();
            Session["hodofhod"] = dr["HOD"].ToString();

        }
        dr.Close();
        con.DisConnect();
    }
    public void hrID()
    {
        string stable = Session["Company"].ToString();
        string rtable = stable.Replace(".", "_");
        string tblNameEmployee = "[" + rtable + "$Employee" + "]";
        SqlDataReader dr = Portalcon.Show_HRID(tblNameEmployee, Session["HRID"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblHRUserId.Text = Session["HRID"].ToString();
            Session["hr_email2"] = dr["Company E-Mail"].ToString();
            Session["HRName"] = dr["First Name"].ToString() + "  " + dr["Middle Name"].ToString() + " " + dr["Last Name"].ToString();

            dr.Close();
            Portalcon.DisConnect();
        }

        else
        {
            Session["hr_email2"] = "";
            Session["HRName"] = "";
            dr.Close();
            Portalcon.DisConnect();
        }

    }

    public void LoginDetail()
    {
        try
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Company with (NOLOCK) where [Web Portal Access]='1' order by Name desc", Portalcon.Con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Company");
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                string CompanyName = ds.Tables[0].Rows[i]["Name"].ToString();
                try
                {
                    CompanywiseDetails(CompanyName);
                }
                catch (Exception ex)
                {

                }

            }
            Portalcon.DisConnect();
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Connection couldnot established please contact your system administrator');", true);
        }
    }
    public void CompanywiseDetails(string CompanyName)
    {
        string stable = CompanyName;
        string rtable = stable.Replace(".", "_");
        string UserGroup = Portalcon.UserGroup(txtUserid.Text.Trim().ToUpper());
        string comanyname;
        Session["UserGroup"] = UserGroup;
        Session["FacultyCollege"] = Portalcon.FacultyCollege(txtUserid.Text.Trim());
        if (UserGroup == "FACULTY" || UserGroup == "COUNSELLOR" || UserGroup == "COURSE CO" || UserGroup == "STAFF" || UserGroup == "PRINCIPAL" || UserGroup == "HR"
            || UserGroup == "ADMIN" || UserGroup == "LAB INCHARGE" || UserGroup == "REGISTRAR" || UserGroup == "HOD" || UserGroup == "SECURITY" || UserGroup == "FINE" || UserGroup == "VC" || UserGroup == "DEAN")
        {
            comanyname = "[" + rtable + "$Employee" + "]";
            Session["CompanyTableEmployee"] = comanyname.ToString();
            SqlDataReader dr = Portalcon.LoginDetail(comanyname, txtUserid.Text.Trim().ToUpper(), txtpassword.Text.Trim());
            dr.Read();
            if (dr.HasRows)
            {
                Session["enroll"] = dr["No_"].ToString();
                Session["Pass"] = dr["Web Portal Password"].ToString();
                Session["uid"] = dr["No_"].ToString();
                Session["Passw"] = dr["Web Portal Password"].ToString();
                Session["Fulname"] = dr["First Name"].ToString() + "  " + dr["Middle Name"].ToString() + " " + dr["Last Name"].ToString();
                Session["uname"] = dr["First Name"].ToString();
                Session["CardNo"] = dr["Card No"].ToString();
                Session["DesignationCode"] = dr["Designation Code"].ToString();
                Session["HODLoginPage"] = dr["HOD"].ToString();
                Session["EmployeeMachineCodeD"] = dr["Employee Machine Code"].ToString();
                Session["ShiftPattern"] = dr["Shift Pattern"].ToString();
                Session["ShiftDay"] = dr["Weekly Off only Fixed Shift"].ToString();
                Session["CompanyHolidayAllowed"] = dr["Company Holiday Allowed"].ToString();
                Session["hod_ID_Leave1"] = dr["HOD"].ToString();
                Session["hod_Name_Leave1"] = dr["HOD Name"].ToString();
                Session["hod_ID_Leave2"] = dr["HOD 1"].ToString();
                Session["hod_Name_Leave2"] = dr["HOD Name 1"].ToString();
                Session["EmployeePostingGroupl"] = dr["Employee Posting Group"].ToString();
                Session["HODLoginPage1"] = dr["HOD 1"].ToString();
                Session["IndentApproval"] = dr["HOD 1"].ToString();  //08 Nov 2016


                //Session["[HODNameLeave"] = dr["HOD Name"].ToString();

                //Session["[HODNameLeave1"] = dr["HOD Name 1"].ToString();
                Session["HRID"] = dr["HR"].ToString();
                Session["HRID_leave"] = dr["HR"].ToString();
                // Session["BandCodereim"] = dr["Occupation Code"].ToString();
                Session["Gradereim"] = dr["Job Title_Grade"].ToString();
                Session["LocationCodereim"] = dr["Location Code"].ToString();
                Session["Departmentcode"] = dr["Global Dimension 2 Code"].ToString();

                Session["GlobalDimension1Coded"] = dr["Global Dimension 1 Code"].ToString();
                Session["GlobalDimension1Code"] = Session["FacultyCollege"];  //  dr["Global Dimension 1 Code"].ToString(); //comment on 22-08-2016
                //Session["HRLoginPage"] = dr["HOD"].ToString();
                Session["UserType"] = dr["Web Portal Type"].ToString();
                string changepassstatus = dr["Change_PasswordStatus"].ToString();
                string ccname = CompanyName;
                string rccname = stable.Replace(".", "_");
                Session["Company"] = rccname.ToString();
                Session["CompanyEmail"] = dr["Company E-Mail"].ToString();
                string profilePhoto = dr["ProfilePhoto"].ToString();
                Session["PMS_Job_Title_Grade_Desc"] = dr["Job Title_Grade Desc"].ToString().Trim();
                Session["PMS_Employee_Posting_Group"] = dr["Employee Posting Group"].ToString().Trim();
                Session["PMS_DepartmentName"] = dr["Department Name"].ToString().Trim();



                if (profilePhoto == "")
                {
                    Session["ProfilePhoto"] = "~/ProfileImage/" + "Person.png";
                }
                else
                {
                    string ph = dr["ProfilePhoto"].ToString();
                    Session["ph1"] = dr["ProfilePhoto"].ToString();

                    Session["ProfilePhoto"] = "~/ProfileImage/" + dr["ProfilePhoto"].ToString();
                }
                if (chkrem.Checked == true)
                {
                    Response.Cookies["userid1"].Value = txtUserid.Text;
                    Response.Cookies["pwd"].Value = txtpassword.Text;
                    Response.Cookies["userid1"].Expires = DateTime.Now.AddDays(15);
                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(15);

                }
                else
                {
                    Response.Cookies["userid1"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
                }

                Session["IndentApproval_DH"] = dr["Indent Approval"].ToString();
                Session["IndentApproval_Dept_DH"] = dr["Department"].ToString();
                Session["IndentEntry_DH"] = dr["Indent Entry"].ToString();
                Session["IndentApproval_DHIT"] = dr["Indent Approval IT"].ToString();
                Session["IndentApproval_Dept_DHIT"] = dr["Department"].ToString();
                Session["IndentEntry_DHIT"] = dr["Indent Entry IT"].ToString();
                dr.Close();
                Portalcon.DisConnect();
                hrID();
                showhodEmailid();
                Session["HRID"] = lblHRUserId.Text;

                //---------------Dhirendra For Indent on 15-12-2016 start
                string IndentApprovalID = Portalcon.IndentApprovalID(txtUserid.Text.Trim());
                Session["IndentApprovalID"] = IndentApprovalID;
                //---------------Dhirendra For Indent on 15-12-2016 End

                SqlDataAdapter da = new SqlDataAdapter("select top 1 [Indent Approval IT] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] WITH(NOLOCK) where [Indent Approval IT]='" + txtUserid.Text.Trim() + "'", Portalcon.Con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Session["IndentApprovalIDIT"] = dt.Rows[0]["Indent Approval IT"].ToString();
                }
                else
                {
                    Session["IndentApprovalIDIT"] = "";
                }


                //  Session["NavDBName"] = "[EDUCOLLEGELIVE-R2_12012022].dbo."; //--------------Change [Live Database].dbo.
                if (UserGroup == "FACULTY" || UserGroup == "COURSE CO" || UserGroup == "STAFF" || UserGroup == "PRINCIPAL" || UserGroup == "COUNSELLOR" || UserGroup == "HR" || UserGroup == "ADMIN" || UserGroup == "LAB INCHARGE" || UserGroup == "REGISTRAR" || UserGroup == "HOD" || UserGroup == "SECURITY" || UserGroup == "FINE" || UserGroup == "VC" || UserGroup == "DEAN")
                {

                    Session["Princpal"] = ""; Session["Hod"] = ""; Session["CourseCo-Ordinator"] = ""; Session["ClassCo-Ordinator"] = "";
                    Session["Proctor"] = ""; Session["LabIncharge"] = ""; Session["EventCo-Ordinator"] = "";
                    Session["Security"] = ""; Session["FINE"] = "";
                    if (UserGroup == "SECURITY") { Session["Security"] = "SECURITY"; }
                    if (UserGroup == "FINE") { Session["FINE"] = "FINE"; }


                    if (UserGroup == "FACULTY" || UserGroup == "PRINCIPAL" || UserGroup == "STAFF" || UserGroup == "FINE" || UserGroup == "HOD")
                    {
                        bindCollege();
                        if (ddlCollege.Items.Count > 1)
                        {
                            mpeLog.Show();
                            userrolematrix();
                        }
                        else
                        { // this code section use for check Role
                            userrolematrix();
                            //end code section
                            Response.Redirect("Faculty/FacultyDetails.aspx");
                        }
                    }
                    else
                    {
                        userrolematrix();
                        Response.Redirect("Faculty/FacultyDetails.aspx");
                    }
                }
            }

            else
            {
                dr.Close();
                Portalcon.DisConnect();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please contact erp@tmu.ac.in from your registered email id to obtain your credentials!');", true);
            }
        }
        else if (UserGroup == "STUDENT")
        {
            comanyname = "[" + rtable + "$Student - COLLEGE" + "]";
            Session["CompanyTableStudent"] = comanyname.ToString();

            SqlCommand cmd = new SqlCommand("proc_GetStudentDetailbyNo_2", Portalcon.Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentNo_", txtUserid.Text);
            cmd.Parameters.AddWithValue("@Password_", txtpassword.Text);
            SqlDataAdapter daStudent = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            daStudent.Fill(dt);

            //string s = "select s.[Enrollment No_],s.[No_] as [Login ID],s.Password,p.[User Group],s.[Student Name],s.[Academic Year],s.[Course Code],s.Semester,s.Year,s.Section,s.[Global Dimension 1 Code],s.[Student Status],case when s.Year='' then s.[Semester Registration] else 1 end as 'Semester Registration' from [Portal Users] p inner join [TMU$Student - COLLEGE] s on p.[Login ID]=s.[Enrollment No_]  where p.[Login ID]='" + txtUserid.Text.Trim() + "' and (s.Password='" + txtpassword.Text.Trim() + "' OR '" + txtpassword.Text.Trim() + "'='SERPAdmin@123') and s.[Student Status]<>'2'";
            //SqlCommand cmd = new SqlCommand(s, Portalcon.Con);
            //SqlDataReader dr = cmd.ExecuteReader();
            //dr.Read();
            if (dt.Rows.Count>0)
            {
                Session["enroll"] = dt.Rows[0]["Enrollment No_"].ToString();
                Session["Pass"] = dt.Rows[0]["Password"].ToString();
                Session["uid"] = dt.Rows[0]["Login ID"].ToString();
                Session["Name"] = dt.Rows[0]["Student Name"].ToString();
                Session["CourseCode"] = dt.Rows[0]["Course Code"].ToString();
                Session["Semester"] = dt.Rows[0]["Semester"].ToString();
                Session["Section"] = dt.Rows[0]["Section"].ToString();
                Session["Year"] = dt.Rows[0]["Year"].ToString();
                Session["College"] = dt.Rows[0]["Global Dimension 1 Code"].ToString();
                Session["AcademicYear"] = dt.Rows[0]["Academic Year"].ToString();
                Session["Passw"] = dt.Rows[0]["Password"].ToString();
                if (chkrem.Checked == true)
                {
                    Response.Cookies["userid1"].Value = txtUserid.Text;
                    Response.Cookies["pwd"].Value = txtpassword.Text;
                    Response.Cookies["userid1"].Expires = DateTime.Now.AddDays(15);
                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(15);

                }
                else
                {
                    Response.Cookies["userid1"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
                }
                if (dt.Rows[0]["User Group"].ToString() == "STUDENT" && dt.Rows[0]["Student Status"].ToString() != "3")
                {

                    if (dt.Rows[0]["Semester Registration"].ToString() == "0")
                    {
                        Session["SemReg"] = 0;
                        Response.Redirect("Student/SemRegistration.aspx");

                    }
                    else
                    {


                        Response.Redirect("Student/StudentDetailsView1.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Alumni/StudentDetailsView.aspx");
                }

                
            }
            else
            {
               
                SqlDataAdapter daP = new SqlDataAdapter("Select [Student Status] from [TMU$Student - COLLEGE]  where  [Enrollment No_] ='" + txtUserid.Text.Trim() + "'", Portalcon.Con);
                DataTable dt1 = new DataTable();
                daP.Fill(dt1);
                if (dt1.Rows[0]["Student Status"].ToString() == "3")
                {

                    Portalcon.DisConnect();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please contact alumni@tmu.ac.in or 9258118526 to obtain your credentials');", true);
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please contact alumni@tmu.ac.in to obtain your credentials');", true);
                }
                else
                {





                    Portalcon.DisConnect();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Sorry ,Enter Correct Password  !');", true);
                }
            }
        }
        else
        {

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Sorry ,you are not registered contact Admin !');", true);
        }     
    }
    public void userrolematrix()
    {
        SqlDataReader Roledr = Portalcon.CheckRole(Session["uid"].ToString(), Session["GlobalDimension1Code"].ToString());
        if (Roledr.Read())
        {
            Session["Princpal"] = Roledr["Principal"].ToString();
            Session["Hod"] = Roledr["HOD"].ToString();
            Session["CourseCo-Ordinator"] = Roledr["Course Co-Ordinator"].ToString();
            Session["ClassCo-Ordinator"] = Roledr["Class Co-Ordinator"].ToString();
            Session["Proctor"] = Roledr["Proctor"].ToString();
            Session["LabIncharge"] = Roledr["Lab Incharge"].ToString();
            Session["EventCo-Ordinator"] = Roledr["Event Co-Ordinator"].ToString();
        }
        //end code section
    }
    //
    private static string sKey = "UJYHCX783her*&5@$%#(MJCX**38n*#6835ncv56tvbry(&#MX98cn342cn4*&X#&dhirendracorporateserve9877222";

    public static string Encrypt(string sPainText)
    {
        if (sPainText.Length == 0)
            return (sPainText);
        return (EncryptString(sPainText, sKey));
    }
    public static string Decrypt(string sEncryptText)
    {
        if (sEncryptText.Length == 0)
            return (sEncryptText);
        return (DecryptString(sEncryptText, sKey));
    }
    protected static string EncryptString(string InputText, string Password)
    {

        RijndaelManaged RijndaelCipher = new RijndaelManaged();

        byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);

        byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

        ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16));

        MemoryStream memoryStream = new MemoryStream();

        CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(PlainText, 0, PlainText.Length);

        cryptoStream.FlushFinalBlock();

        byte[] CipherBytes = memoryStream.ToArray();

        memoryStream.Close();
        cryptoStream.Close();

        string EncryptedData = Convert.ToBase64String(CipherBytes);

        return EncryptedData;
    }
    protected static string DecryptString(string InputText, string Password)
    {
        try
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] EncryptedData = Convert.FromBase64String(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(16), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream(EncryptedData);

            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            byte[] PlainText = new byte[EncryptedData.Length];

            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            memoryStream.Close();
            cryptoStream.Close();

            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

            return DecryptedData;
        }
        catch (Exception exception)
        {
            return (exception.Message);
        }
    }
    protected void btnloginclick_Click(object sender, EventArgs e)
    {
        // link.Visible = true;
        LoginDetail();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        GetPassword();
        mpe.Show();

    }
    public void GetPassword()
    {
        string Result = "";

        try
        {
            lblMsg.Text = "";
            Result = commonDl.GetPassword(txtLoginUserId.Text, txtMobileNo.Text);
            if (Result.Substring(0, 25) != "Your Login Password is : ")
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('" + Result + " ) ! ')", true);
                lblMsg.Text = Result;
                return;
            }
            else
            {
                // txtMobileNo.Text="7503335183";
                SendMessage(txtMobileNo.Text, Result);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = Result;
            return;
        }
    }
    public bool SendMessage(string MobileNo, string Message)
    {
        MobileNo = MobileNo.Substring(MobileNo.Length - 10, 10);
        SMS(MobileNo, Message);
        lblMsg.Text = "Password send to Your Mobile No. !";
        return true;
    }
    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        // MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 7503335183;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            Response.Write(str.ReadToEnd());
            str.Close();
        }
    }

    public void bindCollege()
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("Proc_GetUserCollege", Portalcon.Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserID", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlCollege.DataSource = dt;
        ddlCollege.DataTextField = "Details";
        ddlCollege.DataValueField = "No_";
        ddlCollege.DataBind();
        Portalcon.DisConnect();
    }
    protected void btnCollege_Click(object sender, EventArgs e)
    {
        Session["GlobalDimension1Code"] = ddlCollege.SelectedValue;
        string UserGroup = Portalcon.UserGroup(txtUserid.Text.Trim(), Session["GlobalDimension1Code"].ToString());
        Session["UserGroup"] = UserGroup;
        Response.Redirect("Faculty/FacultyDetails.aspx");
    }
    protected void lnkPay_Click(object sender, EventArgs e)
    {
        Response.Redirect("OnLineFeePayment.aspx");
    }
}