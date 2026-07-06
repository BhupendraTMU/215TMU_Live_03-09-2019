using DL;
using Microsoft.ReportingServices.Interfaces;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_FacultyDetails : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con1;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
         try
        {
            if (!IsPostBack)
            {
                BindDate(Session["uid"].ToString());

                bindDesignation();

            }
            if (Session["UserGroup"].ToString() == "PRINCIPAL" || Session["UserGroup"].ToString() == "DEAN")
            {
                chkboxAsPrincipal.Visible = true;
            }
            if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
            {
                //Show_RecommendLeaveCount();
                Leave_Pending_ApprovalHOD_Count();
            }

            else
            {

                Leave_Pending_ApprovalHOD_Count();
                // Assign_submit_count();
            }

        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    //sandeep

    public void Leave_Pending_ApprovalHOD_Count()
    {
        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]))
        {
            SqlCommand cmd = new SqlCommand("AssigmentCountForNotification", Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
            cmd.Parameters.Add("@GroupCode", Session["UserGroup"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 50000;
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
               
                ModalPopupExtender1.Show();
                main.Visible = true;

                grdInbox.DataSource = dt;
                grdInbox.DataBind();
                Social.Visible = false;
            }
            else
            {
                ModalPopupExtender1.Show();
                main.Visible = false;
                Social.Visible = true;

                
              

             
                
            }

        }
    }
   

    public void bindDesignation()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT distinct [Job Title_Grade Desc] as No_,[Job Title_Grade Desc] as Details FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$Employee] where [Department Name]!=''", con); // Replace with actual table name
            cmd.CommandType = CommandType.Text; // Changed from StoredProcedure to Text
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            con.Open();
            da.Fill(dt1);
            con.Close();
            ddlDesignationName11.DataSource = dt1;
            ddlDesignationName11.DataTextField = "Details";
            ddlDesignationName11.DataValueField = "No_";
            ddlDesignationName11.DataBind();
        }
        catch
        {
            // Consider logging or handling the exception
        }
    }

    public void Blank()
    {
        txtAddress.Text = "";
        txtAdharCard.Text = "";
        txtBirthName.Text = "";
        txtBloodGroup.Text = "";
        txtBranchCode.Text = "";
        txtCardNo.Text = "";
        txtCollegeCode.Text = "";
        txtDepartmantCode.Text = "";
        txtDesignationCode.Text = "";
        txtEmergencyContactPer.Text = "";
        txtEmergencyPhoneNo.Text = "";
        txtEmployeeType.Text = "";
        txtEmployementDate.Text = "";
        txtExtension.Text = "";
        txtFacultyNo.Text = "";
        txtFatherName.Text = "";
        txtGrade.Text = "";
        txtHOD.Text = "";
        txtHODName.Text = "";
        txtMobileNo.Text = "";
        txtMotherName.Text = "";
        txtName.Text = "";
        txtPFNo.Text = "";
        txtPhoneNo.Text = "";
        txtReligion.Text = "";
        txtReportingInchargeName.Text = "";
        txtSearchName.Text = "";
        txtState.Text = "";
        txtTitle.Text = "";
        txtVoterID.Text = "";
        txtUANNo.Text = "";
        txtGender.Text = "";
        txtMaritalStatus.Text = "";
        txtHod1.Text = "";
        txtHod1Name.Text = "";
    }
    public void bindCourseCode1()
    {
        SqlCommand cmd = new SqlCommand("select distinct([Course Code]) from [TMU$Course Wise Faculty] where [Global Dimension 1 Code]=  '" + Session["GlobalDimension1Code"] + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourseCode1.DataSource = dt;
        drpCourseCode1.DataTextField = "Course Code";
        drpCourseCode1.DataBind();
        drpCourseCode1.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string Save(string MobileNo, string PhoneNo, string Extension, string Email, string EmergencyContactPer, string EmergencyPhoneNo, string State,
        string Address, string GoogleSiteLink, string FacultyCode)
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
        SqlCommand cmd = new SqlCommand("update [TMU$Employee] set [Mobile Phone No_]='" + MobileNo + "',[Phone No_]='" + PhoneNo + "',Extension='" + Extension + "',[E-Mail]='" + Email + "',[Emergency Contact Person]='" + EmergencyContactPer + "',[Emergency Phone No_]='" + EmergencyPhoneNo + "',State='" + State + "',Address='" + Address + "',[Google Site Link]='" + GoogleSiteLink + "' where No_='" + FacultyCode + "'", con1);
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        cmd.ExecuteNonQuery();
        con1.Close();
        return "Save Successfully";
    }
    public class Customer
    {
        public string FacultyCode { get; set; }
        public string FacultyName { get; set; }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<Customer> BindFacultyCode(string CourseCode)
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con1 = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select distinct([Faculty Code]),[Faculty Name] from [TMU$Course Wise Faculty] where [Course Code]='" + CourseCode + "'", con1))
            {
                cmd.Connection = con1;
                List<Customer> customers = new List<Customer>();
                con1.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(new Customer
                        {
                            FacultyCode = sdr["Faculty Code"].ToString(),
                            FacultyName = sdr["Faculty Name"].ToString(),
                        });
                    }
                }
                con1.Close();
                return customers;
            }
        }
    }
    public void BindDate(string FacultyCode)
    {
        try
        {
            DL.FacultyPortalDL FDL = new FacultyPortalDL();
            DataTable dt = new DataTable();
            dt = FDL.GetFacultyDetails(FacultyCode);
            if (dt.Rows.Count > 0)
            {


                hfprofilereq.Value= dt.Rows[0]["profileReq"].ToString(); 
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtAdharCard.Text = dt.Rows[0]["Aadhar Card"].ToString();
                txtAdharCard11.Text = dt.Rows[0]["Aadhar Card"].ToString();
                txtBirthName.Text = dt.Rows[0]["DOB"].ToString();
                DateTime dob = Convert.ToDateTime(dt.Rows[0]["DOB"]);
                txtBirthName11.Text = dob.ToString("yyyy-MM-dd");
                txtBloodGroup.Text = dt.Rows[0]["Blood Group"].ToString();
                txtBranchCode.Text = dt.Rows[0]["Branch Code"].ToString();
                
                txtCardNo.Text = dt.Rows[0]["Card No"].ToString();
                txtCollegeCode.Text = dt.Rows[0]["Global Dimension 1 Code"].ToString();
                txtDepartmantCode.Text = dt.Rows[0]["Department Code"].ToString();
                txtDesignationCode.Text = dt.Rows[0]["Designation Code"].ToString();
                txtEmail.Text = dt.Rows[0]["E-Mail"].ToString();
                txtEmail11.Text = dt.Rows[0]["E-Mail"].ToString();
                txtEmergencyContactPer.Text = dt.Rows[0]["Emergency Contact Person"].ToString();
                txtEmergencyPhoneNo.Text = dt.Rows[0]["Emergency Phone No_"].ToString();
                txtEmployeeType.Text = dt.Rows[0]["Employee Posting Group"].ToString();
                txtEmployementDate.Text = dt.Rows[0]["EmploymentDate"].ToString();
                txtExtension.Text = dt.Rows[0]["Extension"].ToString();
                txtFacultyNo.Text = Session["uid"].ToString();
               
                txtFatherName.Text = dt.Rows[0]["Father Name"].ToString();
                txtFatherName11.Text = dt.Rows[0]["Father Name"].ToString();
                txtGrade.Text = dt.Rows[0]["Job Title_Grade"].ToString();
                txtHOD.Text = dt.Rows[0]["HOD"].ToString();
                txtHODName.Text = dt.Rows[0]["HOD Name"].ToString();
                txtHod1.Text = dt.Rows[0]["HOD 1"].ToString();
                txtHod1Name.Text = dt.Rows[0]["HOD Name 1"].ToString();
                txtMobileNo.Text = dt.Rows[0]["Mobile Phone No_"].ToString();
                txtMobileNo11.Text = dt.Rows[0]["Mobile Phone No_"].ToString();
                txtMotherName.Text = dt.Rows[0]["Mother Name"].ToString();
                txtName.Text = dt.Rows[0]["Full Name"].ToString();
                txtName11.Text = dt.Rows[0]["Full Name"].ToString();
                txtPFNo.Text = dt.Rows[0]["PF No"].ToString();
               
                txtPhoneNo.Text = dt.Rows[0]["Phone No_"].ToString();
                txtReligion.Text = dt.Rows[0]["Religion"].ToString();
                txtReportingInchargeName.Text = dt.Rows[0]["Reporting Incharge Name"].ToString();
                txtSearchName.Text = dt.Rows[0]["Search Name"].ToString();
                txtState.Text = dt.Rows[0]["State"].ToString();
                txtTitle.Text = dt.Rows[0]["Title"].ToString();
                txtVoterID.Text = dt.Rows[0]["Voter ID"].ToString();
                txtUANNo.Text = dt.Rows[0]["UAN No"].ToString();
              
                txtGoogleSiteLink.Text = dt.Rows[0]["Google Site Link"].ToString();
                ddlGender11.SelectedValue = dt.Rows[0]["Gender"].ToString();
              
                ddlDesignationName11.SelectedValue = dt.Rows[0]["Job Title_Grade Desc"].ToString();

                if (dt.Rows[0]["Gender"].ToString() == "0")
                {
                    txtGender.Text = "";
                  }
                else if (dt.Rows[0]["Gender"].ToString() == "1")
                { txtGender.Text = "Female";
                }
                else if (dt.Rows[0]["Gender"].ToString() == "2")
                { 
                    txtGender.Text = "Male";
                 }
                txtEmployeeStatus.Text = dt.Rows[0]["Employee Status"].ToString() == "0" ? "Not Confirm" : "Confirm";
                if (dt.Rows[0]["Marital Status"].ToString() == "0")
                    txtMaritalStatus.Text = "Single";
                else if (dt.Rows[0]["Marital Status"].ToString() == "1")
                    txtMaritalStatus.Text = "Married";
                else if (dt.Rows[0]["Marital Status"].ToString() == "2")
                    txtMaritalStatus.Text = "Divorced";
                else if (dt.Rows[0]["Marital Status"].ToString() == "3")
                    txtMaritalStatus.Text = "Widowed";
            }
            else
            {
                Blank();
            }
            if (!IsPostBack)
            {
                bindCourseCode1();
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    public class Employee
    {
        public string Address { get; set; }
        public string AdharCard { get; set; }
        public string DOB { get; set; }
        public string BloodGroup { get; set; }
        public string BranchCode { get; set; }
        public string CardNo { get; set; }
        public string CollegeCode { get; set; }
        public string DepartmantCode { get; set; }
        public string DesignationCode { get; set; }
        public string Email { get; set; }
        public string EmergencyContactPer { get; set; }
        public string EmergencyPhoneNo { get; set; }
        public string EmployeeType { get; set; }
        public string EmployementDate { get; set; }
        public string Extension { get; set; }
        public string FacultyNo { get; set; }
        public string FatherName { get; set; }
        public string Grade { get; set; }
        public string HOD { get; set; }
        public string HODName { get; set; }
        public string MobileNo { get; set; }
        public string MotherName { get; set; }
        public string Name { get; set; }
        public string PFNo { get; set; }
        public string PhoneNo { get; set; }
        public string Religion { get; set; }
        public string ReportingInchargeName { get; set; }
        public string SearchName { get; set; }
        public string State { get; set; }
        public string Title { get; set; }
        public string VoterID { get; set; }
        public string UANNo { get; set; }
        public string Gender { get; set; }
        public string EmployeeStatus { get; set; }
        public string MaritalStatus { get; set; }
        public string GoogleSiteLink { get; set; }
        public string HOD1 { get; set; }
        public string HOD1Name { get; set; }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<Employee> bindData(string FacultyCode)
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
        using (SqlConnection con1 = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select *,convert(varchar,[Birth Date],106) as DOB,convert(varchar,[Employment Date],106) as EmploymentDate from [TMU$Employee] where [No_]='" + FacultyCode + "'", con1))
            {
                cmd.Connection = con1;
                List<Employee> Employee = new List<Employee>();
                con1.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Employee.Add(new Employee
                        {
                            Address = sdr["Address"].ToString(),
                            AdharCard = sdr["Aadhar Card"].ToString(),
                            DOB = sdr["DOB"].ToString(),
                            BloodGroup = sdr["Blood Group"].ToString(),
                            BranchCode = sdr["Branch Code"].ToString(),
                            CardNo = sdr["Card No"].ToString(),
                            CollegeCode = sdr["Global Dimension 1 Code"].ToString(),
                            DepartmantCode = sdr["Department Code"].ToString(),
                            DesignationCode = sdr["Designation Code"].ToString(),
                            Email = sdr["E-Mail"].ToString(),
                            EmergencyContactPer = sdr["Emergency Contact Person"].ToString(),
                            EmergencyPhoneNo = sdr["Emergency Phone No_"].ToString(),
                            EmployeeType = sdr["Employee Posting Group"].ToString(),
                            EmployementDate = sdr["EmploymentDate"].ToString(),
                            Extension = sdr["Extension"].ToString(),
                            FacultyNo = sdr["No_"].ToString(),
                            FatherName = sdr["Father Name"].ToString(),
                            Grade = sdr["Job Title_Grade"].ToString(),
                            HOD = sdr["HOD"].ToString(),
                            HODName = sdr["HOD Name"].ToString(),
                            MobileNo = sdr["Mobile Phone No_"].ToString(),
                            MotherName = sdr["Mother Name"].ToString(),
                            Name = sdr["Full Name"].ToString(),
                            PFNo = sdr["PF No"].ToString(),
                            PhoneNo = sdr["Phone No_"].ToString(),
                            Religion = sdr["Religion"].ToString(),
                            ReportingInchargeName = sdr["Reporting Incharge Name"].ToString(),
                            SearchName = sdr["Search Name"].ToString(),
                            State = sdr["State"].ToString(),
                            Title = sdr["Title"].ToString(),
                            VoterID = sdr["Voter ID"].ToString(),
                            UANNo = sdr["UAN No"].ToString(),
                            Gender = sdr["Gender"].ToString(),
                            EmployeeStatus = sdr["Employee Status"].ToString(),
                            MaritalStatus = sdr["Marital Status"].ToString(),
                            GoogleSiteLink = sdr["Google Site Link"].ToString(),
                            HOD1 = sdr["HOD 1"].ToString(),
                            HOD1Name = sdr["HOD Name 1"].ToString(),
                        });
                    }
                }
                con1.Close();
                return Employee;
            }
        }
    }
    protected void grdInbox_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Honorarium Approval")
        {
            Response.Redirect("../Faculty/Honorarium_E_Visit.aspx");
        }


        if (e.CommandName == "Leave")
        {
            Response.Redirect("../Faculty/Leave.aspx");
        }
       
        if (e.CommandName == "Assignment")
        {
            Response.Redirect("../Faculty/AssignmentUploadDownload.aspx");
        }
        if (e.CommandName == "Course Plan")
        {
            Response.Redirect("../Faculty/FacultyCoursePlanList.aspx");
        }
        if (e.CommandName == "Class Arragement")
        {
            Response.Redirect("../Faculty/FacultyFreeSlot.aspx");
        }
        if (e.CommandName == "New Indent")
        {
            Response.Redirect("../Faculty/IndentApproval.aspx");
        }
        if (e.CommandName == "Examination Form Pending")
        {
            Response.Redirect("../Faculty/ExaminationApprovalForm.aspx");
        }

        if (e.CommandName == "Examination Form Rejected by Principal")
        {
            Response.Redirect("../Faculty/ExaminationApprovalForm.aspx");
        }
        if (e.CommandName == "Examination Form Rejected by COE")
        {
            Response.Redirect("../Faculty/ExaminationApprovalForm.aspx");
        }
        if (e.CommandName == "Exam Form Pending")
        {
            Response.Redirect("../Faculty/ExaminationFormRelease.aspx");
        }

        if (e.CommandName == "Exam Form Rejected by COE")
        {
            Response.Redirect("../Faculty/ExaminationFormRelease.aspx");
        }
        if (e.CommandName == "Admit Card Approval")
        {
            Response.Redirect("../Faculty/AdmitCardApproval.aspx");
        }
        if (e.CommandName == "Admit Card Release")
        {
            Response.Redirect("../Faculty/AdmitCardRelease.aspx");
        }
        if (e.CommandName == "Admit Card Download Allow")
        {
            Response.Redirect("../Faculty/AdmitCardDownloadAllow.aspx");
        }

        if (e.CommandName == "Admit Card")
        {
            Response.Redirect("../Faculty/AdmitCard.aspx");
        }
        if (e.CommandName == "Marks Entry")
        {
            Response.Redirect("../Faculty/MarksEntry.aspx");
        }
        if (e.CommandName == "Student No Dues")
        {
            Response.Redirect("../Faculty/StudentNoDuesApproval.aspx");
        }
        if (e.CommandName == "Migration Certificate")
        {
            Response.Redirect("../Faculty/MigrationApproval.aspx");
        }
    }

    protected void btnChangeRequest_Click(object sender, EventArgs e)
    {
        pnlPopup.Visible = true;
        Social.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        string txtName = txtName11.Text;
        string txtGender = ddlGender11.Text;
       
        string txtBirthDate = txtBirthName11.Text;  // consider renaming control too
       
        string ddlDesignationName = ddlDesignationName11.Text;
        string txtFatherName = txtFatherName11.Text;
        string txtAdharCard = txtAdharCard11.Text;
        string txtPANCardNo = txtPANCardNo11.Text;
        string txtMobileNo = txtMobileNo11.Text;
        string txtEmail = txtEmail11.Text;

        try
        {
            using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
            {
                string fileName = "";
                if (FileUpload1.HasFile)
                {
                    try
                    {
                        string folderPath = Server.MapPath("~/Uploads/doc/");
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        string originalFileName = Path.GetFileNameWithoutExtension(FileUpload1.FileName);
                        string extension = Path.GetExtension(FileUpload1.FileName);
                        string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                        // Final unique filename
                         fileName = originalFileName + "_" + timeStamp + extension;
                        string fullPath = Path.Combine(folderPath, fileName);

                        FileUpload1.SaveAs(fullPath);
                    }
                    catch(Exception ex)
                    {

                    }
            }



                        string query = @"
                INSERT INTO [TMU$EmployeeUpdateInfo] 
                (EmployeeNo, [Name], [Gender] , BirthDate, DesignationName, FatherName, AdharCard, PANCard, MobileNo, Email, Status,FileUpload, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
                VALUES
                (@EmployeeNo, @Name, @Gender, @BirthDate, @DesignationName, @FatherName, @AdharCard, @PANCard, @MobileNo, @Email, @Status,@FileUpload, @CreatedBy, @CreatedAt, @UpdatedBy, @UpdatedAt)";

                using (SqlCommand cmd = new SqlCommand(query, con1))
                {
                    cmd.Parameters.AddWithValue("@EmployeeNo", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@Name", txtName);
                    cmd.Parameters.AddWithValue("@Gender", txtGender);
                   
                    cmd.Parameters.AddWithValue("@BirthDate", Convert.ToDateTime(txtBirthDate)); // Ensure valid date format                    
                    cmd.Parameters.AddWithValue("@DesignationName", ddlDesignationName);
                    cmd.Parameters.AddWithValue("@FatherName", txtFatherName);
                    cmd.Parameters.AddWithValue("@AdharCard", txtAdharCard);
                    cmd.Parameters.AddWithValue("@PANCard", txtPANCardNo);
                                       
                    cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo);
                    cmd.Parameters.AddWithValue("@Email", txtEmail);
                   
                    cmd.Parameters.AddWithValue("@Status", 0); 
                    cmd.Parameters.AddWithValue("@FileUpload", fileName);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    con1.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con1.Close();

                    if (rowsAffected > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Successfull'); document.location.href='FacultyDetails.aspx';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Failed: No rows affected');document.location.href='FacultyDetails.aspx';", true);
                    }
                }
            }
        }
        catch (SqlException sqlEx)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Failed: No rows affected');document.location.href='FacultyDetails.aspx';", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Failed: No rows affected');document.location.href='FacultyDetails.aspx';", true);
        }
    }


}