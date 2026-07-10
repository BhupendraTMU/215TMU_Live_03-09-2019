using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;

public partial class StudentNoDuesPHD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Only show this page for TPHD college - if session not present, try to hide
                if (Session["College"] != null && Session["College"].ToString() == "TPHD")
                {
                    divPhd.Visible = true;
                    bindPhdData();
                    bindPhdFees();
                    // hide OTP panels initially
                    var pnlPhdMobileVerify = GetControl<Panel>("pnlPhdMobileVerify");
                    var pnlPhdMobileVerifyBtn = GetControl<Panel>("pnlPhdMobileVerifyBtn");
                    var pnlPhdEmailVerify = GetControl<Panel>("pnlPhdEmailVerify");
                    var pnlPhdEmailVerifyBtn = GetControl<Panel>("pnlPhdEmailVerifyBtn");
                    if (pnlPhdMobileVerify != null) pnlPhdMobileVerify.Visible = false;
                    if (pnlPhdMobileVerifyBtn != null) pnlPhdMobileVerifyBtn.Visible = false;
                    if (pnlPhdEmailVerify != null) pnlPhdEmailVerify.Visible = false;
                    if (pnlPhdEmailVerifyBtn != null) pnlPhdEmailVerifyBtn.Visible = false;
                }
                else
                {
                    // if accessed by other colleges, redirect back to main No Dues
                    //Response.Redirect("~/Alumni/StudentNoDues.aspx");
                }
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }

    private T GetControl<T>(string id) where T : Control
    {
        // First try ContentPlaceHolder in master page (common layout)
        if (this.Master != null)
        {
            var cph = this.Master.FindControl("ContentPlaceHolder1");
            if (cph != null)
            {
                var found = FindControlRecursive(cph, id);
                if (found != null) return found as T;
            }
        }

        // Fallback to page-level search
        var f = FindControlRecursive(this, id);
        return f as T;
    }

    // recursive search helper
    private Control FindControlRecursive(Control root, string id)
    {
        if (root == null) return null;
        var c = root.FindControl(id);
        if (c != null) return c;
        foreach (Control child in root.Controls)
        {
            var res = FindControlRecursive(child, id);
            if (res != null) return res;
        }
        return null;
    }

    protected void btnPhdSendOtp_Click(object sender, EventArgs e)
    {
        var txtPhdMobile = GetControl<TextBox>("txtPhdMobile");
        var pnlPhdMobileSend = GetControl<Panel>("pnlPhdMobileSend");
        var pnlPhdMobileVerify = GetControl<Panel>("pnlPhdMobileVerify");
        var pnlPhdMobileSendBtn = GetControl<Panel>("pnlPhdMobileSendBtn");
        var pnlPhdMobileVerifyBtn = GetControl<Panel>("pnlPhdMobileVerifyBtn");

        if (txtPhdMobile == null || string.IsNullOrEmpty(txtPhdMobile.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Mobile No.')", true);
            return;
        }
        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        string sRandomOTP = GenerateRandomOTP(4, saAllowedCharacters);
        SMS(txtPhdMobile.Text, "Dear Student, Your No dues confirmation OTP is " + sRandomOTP + ". Thank you. TEERTHANKER MAHAVEER UNIVERSITY");

        // store OTP and timestamp (UTC) in session for expiry enforcement
        Session["PhdMobileOTP"] = sRandomOTP;
        Session["PhdMobileOTP_Time"] = DateTime.UtcNow;

       
        if (pnlPhdMobileSend != null) pnlPhdMobileSend.Visible = false;
        if (pnlPhdMobileSendBtn != null) pnlPhdMobileSendBtn.Visible = false;
        if (pnlPhdMobileVerify != null) pnlPhdMobileVerify.Visible = true;
        if (pnlPhdMobileVerifyBtn != null) pnlPhdMobileVerifyBtn.Visible = true;
        var lbl = GetControl<Label>("lblPhdMSGOTP"); if (lbl != null) { lbl.Visible = true; lbl.Text = "OTP sent successfully to your mobile. OTP valid for 15 minutes."; }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OTP sent to mobile.')", true);
    }

    protected void btnPhdVerifyMobile_Click(object sender, EventArgs e)
    {
        var txtPhdVerifyMobileOTP = GetControl<TextBox>("txtPhdVerifyMobileOTP");
        var pnlPhdMobileSend = GetControl<Panel>("pnlPhdMobileSend");
        var pnlPhdMobileVerify = GetControl<Panel>("pnlPhdMobileVerify");
        var pnlPhdMobileSendBtn = GetControl<Panel>("pnlPhdMobileSendBtn");
        var pnlPhdMobileVerifyBtn = GetControl<Panel>("pnlPhdMobileVerifyBtn");
        // verify OTP exists and not expired
        if (Session["PhdMobileOTP"] == null || Session["PhdMobileOTP_Time"] == null)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No OTP found. Please request OTP again.')", true);
            return;
        }

        try
        {
            var sentTime = (DateTime)Session["PhdMobileOTP_Time"];
            if (DateTime.UtcNow.Subtract(sentTime) > TimeSpan.FromMinutes(15))
            {
                // expired
                Session.Remove("PhdMobileOTP");
                Session.Remove("PhdMobileOTP_Time");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OTP expired. Please request a new OTP.')", true);
                // reset UI to allow resend
                if (pnlPhdMobileVerify != null) pnlPhdMobileVerify.Visible = false;
                if (pnlPhdMobileVerifyBtn != null) pnlPhdMobileVerifyBtn.Visible = false;
                if (pnlPhdMobileSend != null) pnlPhdMobileSend.Visible = true;
                if (pnlPhdMobileSendBtn != null) pnlPhdMobileSendBtn.Visible = true;
                return;
            }
        }
        catch
        {
            // if session timestamp malformed, clear and ask to resend
            Session.Remove("PhdMobileOTP");
            Session.Remove("PhdMobileOTP_Time");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OTP error. Please request a new OTP.')", true);
            return;
        }

        if (txtPhdVerifyMobileOTP != null && txtPhdVerifyMobileOTP.Text == Session["PhdMobileOTP"].ToString())
        {
            Session["PhdMobileVerified"] = "1";
            // show send panel disabled and keep verified state
            if (pnlPhdMobileVerify != null) pnlPhdMobileVerify.Visible = false;
            if (pnlPhdMobileVerifyBtn != null) pnlPhdMobileVerifyBtn.Visible = false;
            if (pnlPhdMobileSend != null) pnlPhdMobileSend.Visible = true;
            if (pnlPhdMobileSendBtn != null) pnlPhdMobileSendBtn.Visible = false;
            var txt = GetControl<TextBox>("txtPhdMobile"); if (txt != null) txt.Enabled = false;
            // clear stored OTP after successful verify
            Session.Remove("PhdMobileOTP");
            Session.Remove("PhdMobileOTP_Time");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Mobile Verified')", true);
            EnablePhdSaveIfVerified();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Mobile OTP')", true);
        }
    }

    protected void btnPhdSendEmailOtp_Click(object sender, EventArgs e)
    {
        var txtPhdEmail = GetControl<TextBox>("txtPhdEmail");
        var pnlPhdEmailSend = GetControl<Panel>("pnlPhdEmailSend");
        var pnlPhdEmailVerify = GetControl<Panel>("pnlPhdEmailVerify");
        var pnlPhdEmailSendBtn = GetControl<Panel>("pnlPhdEmailSendBtn");
        var pnlPhdEmailVerifyBtn = GetControl<Panel>("pnlPhdEmailVerifyBtn");

        if (txtPhdEmail == null || string.IsNullOrEmpty(txtPhdEmail.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Email Id.')", true);
            return;
        }
        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        string otp = GenerateRandomOTP(6, saAllowedCharacters);
        bool emailSent = SendEmail(txtPhdEmail.Text, "Your OTP Code", "Dear Student, Your No dues confirmation OTP is : '" + otp + "'. Thank you. TEERTHANKER MAHAVEER UNIVERSITY");
        if (emailSent)
        {
            Session["PhdEmailOTP"] = otp;
            Session["PhdEmailOTP_Time"] = DateTime.UtcNow;
            // toggle to show email verify controls
            if (pnlPhdEmailSend != null) pnlPhdEmailSend.Visible = false;
            if (pnlPhdEmailSendBtn != null) pnlPhdEmailSendBtn.Visible = false;
            if (pnlPhdEmailVerify != null) pnlPhdEmailVerify.Visible = true;
            if (pnlPhdEmailVerifyBtn != null) pnlPhdEmailVerifyBtn.Visible = true;
            var lbl = GetControl<Label>("lblPhdMSGOTPEmail"); if (lbl != null) { lbl.Visible = true; lbl.Text = "OTP sent to your email. Valid for 15 minutes."; }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OTP sent to Email.')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error sending Email OTP')", true);
        }
    }

    protected void btnPhdVerifyEmail_Click(object sender, EventArgs e)
    {
        var txtPhdVerifyEmailOTP = GetControl<TextBox>("txtPhdVerifyEmailOTP");
        var pnlPhdEmailSend = GetControl<Panel>("pnlPhdEmailSend");
        var pnlPhdEmailVerify = GetControl<Panel>("pnlPhdEmailVerify");
        var pnlPhdEmailSendBtn = GetControl<Panel>("pnlPhdEmailSendBtn");
        var pnlPhdEmailVerifyBtn = GetControl<Panel>("pnlPhdEmailVerifyBtn");
        if (Session["PhdEmailOTP"] == null || Session["PhdEmailOTP_Time"] == null)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No OTP found. Please request OTP again.')", true);
            return;
        }

        try
        {
            var sentTime = (DateTime)Session["PhdEmailOTP_Time"];
            if (DateTime.UtcNow.Subtract(sentTime) > TimeSpan.FromMinutes(15))
            {
                Session.Remove("PhdEmailOTP");
                Session.Remove("PhdEmailOTP_Time");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OTP expired. Please request a new OTP.')", true);
                if (pnlPhdEmailVerify != null) pnlPhdEmailVerify.Visible = false;
                if (pnlPhdEmailVerifyBtn != null) pnlPhdEmailVerifyBtn.Visible = false;
                if (pnlPhdEmailSend != null) pnlPhdEmailSend.Visible = true;
                if (pnlPhdEmailSendBtn != null) pnlPhdEmailSendBtn.Visible = true;
                return;
            }
        }
        catch
        {
            Session.Remove("PhdEmailOTP");
            Session.Remove("PhdEmailOTP_Time");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('OTP error. Please request a new OTP.')", true);
            return;
        }

        if (txtPhdVerifyEmailOTP != null && txtPhdVerifyEmailOTP.Text == Session["PhdEmailOTP"].ToString())
        {
            Session["PhdEmailVerified"] = "1";
            if (pnlPhdEmailVerify != null) pnlPhdEmailVerify.Visible = false;
            if (pnlPhdEmailVerifyBtn != null) pnlPhdEmailVerifyBtn.Visible = false;
            if (pnlPhdEmailSend != null) pnlPhdEmailSend.Visible = true;
            if (pnlPhdEmailSendBtn != null) pnlPhdEmailSendBtn.Visible = false;
            var txt = GetControl<TextBox>("txtPhdEmail"); if (txt != null) txt.Enabled = false;
            Session.Remove("PhdEmailOTP");
            Session.Remove("PhdEmailOTP_Time");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Email Verified')", true);
            EnablePhdSaveIfVerified();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Email OTP')", true);
        }
    }

    private void EnablePhdSaveIfVerified()
    {
        if (Session["PhdMobileVerified"] != null && Session["PhdEmailVerified"] != null)
        {
            btnPhdSave.Visible = true;
        }
    }

    private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
    {
        string sOTP = String.Empty;
        Random rand = new Random();
        for (int i = 0; i < iOTPLength; i++)
        {
            sOTP += saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
        }
        return sOTP;
    }

    private string GenerateNumberSeries()
    {
        try
        {
            // Use TMUCON and tbl_PhDStudentNoDues for generating the sequence (new table)
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.tbl_PhDStudentNoDues", conn))
            {
                conn.Open();
                int cnt = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                int next = cnt + 1;
                return string.Format("TMU/Exam/NoDues/244001{0}", next);
            }
        }
        catch
        {
            return "TMU/Exam/NoDues/2440011";
        }
    }

    private bool ValidatePendingFees()
    {
        try
        {
            if (gvPhdFees == null || gvPhdFees.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "noFees", "alert('No fee details found.');", true);
                return false;
            }

            bool hasPendingFees = false;
            foreach (GridViewRow row in gvPhdFees.Rows)
            {
                // Status is at index 4 in the mapped grid (FeeDescription=0, FeeAmount=1, PaidAmount=2, PendingAmount=3, Status=4)
                if (row.Cells.Count > 4)
                {
                    string status = row.Cells[4].Text.Trim();
                    if (status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                    {
                        hasPendingFees = true;
                        break;
                    }
                }
            }

            if (hasPendingFees)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "pendingFees", 
                    "alert('You have pending fees. Please clear all dues before submitting No Dues form. Use Pay Now button to pay fees.');", true);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "feeValidation", 
                "alert('Error validating fees: " + ex.Message.Replace("'", "\"") + "');", true);
            return false;
        }
    }

    public void bindPhdData()
    {
        try
        {
            if (Session["enroll"] == null) return;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
            using (SqlCommand cmd = new SqlCommand("usp_GetPhdStudentData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@enroll", Session["enroll"].ToString());
                cmd.Parameters.AddWithValue("@dept", Session["College"].ToString());
                cmd.Parameters.AddWithValue("@CourseCode",Session["CourseCode"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    // Use GetControl to be resilient to naming/container differences and handle multiple SP column name variants
                    Func<string[], string> getFirstCol = (cols) =>
                    {
                        foreach (var c in cols)
                        {
                            if (dt.Columns.Contains(c) && dr[c] != DBNull.Value) return dr[c].ToString();
                        }
                        return string.Empty;
                    };

                    var ctlPhdName = GetControl<TextBox>("txtPhdName"); if (ctlPhdName != null) ctlPhdName.Text = getFirstCol(new[] { "NameofResearchScholar" });
                    var ctlPhdFather = GetControl<TextBox>("txtPhdFather"); if (ctlPhdFather != null) ctlPhdFather.Text = getFirstCol(new[] { "FathersName" });
                    var ctlPhdEnroll = GetControl<TextBox>("txtPhdEnroll"); if (ctlPhdEnroll != null) ctlPhdEnroll.Text = getFirstCol(new[] { "EnrollmentNo" });
                    var ctlPhdGender = GetControl<TextBox>("txtPhdGender"); if (ctlPhdGender != null) ctlPhdGender.Text = getFirstCol(new[] { "Gender" });
                    var ctlPhdCollegeDept = GetControl<TextBox>("txtPhdCollegeDept"); if (ctlPhdCollegeDept != null) ctlPhdCollegeDept.Text = getFirstCol(new[] { "CollegeName" });
                    var ctlPhdCourseName = GetControl<TextBox>("txtPhdCourseName"); if (ctlPhdCourseName != null) ctlPhdCourseName.Text = getFirstCol(new[] { "CourseName" });
                    var ctlPhdStudentNo = GetControl<TextBox>("txtPhdStudentNo"); if (ctlPhdStudentNo != null) ctlPhdStudentNo.Text = getFirstCol(new[] { "StudentNo" });
                    var ctlPhdRegNo = GetControl<TextBox>("txtPhdRegNo"); if (ctlPhdRegNo != null) ctlPhdRegNo.Text = getFirstCol(new[] { "RegistrationNo" });
                    var ctlPhdDateReg = GetControl<TextBox>("txtPhdDateReg"); if (ctlPhdDateReg != null)
                    {
                        var dateVal = getFirstCol(new[] { "DateOfRegistration" });
                        if (!string.IsNullOrEmpty(dateVal))
                        {
                            DateTime d;
                            if (DateTime.TryParse(dateVal, out d)) ctlPhdDateReg.Text = d.ToString("dd/MM/yyyy");
                            else ctlPhdDateReg.Text = dateVal;
                        }
                        else
                        {
                            ctlPhdDateReg.Text = string.Empty;
                        }
                    }

                    // Faculty may be present under different column names; prefer Faculty then FacultyDiscipline
                    var facultyVal = getFirstCol(new[] { "FacultyDiscipline" });
                    var ctlPhdFacultyDiscipline = GetControl<TextBox>("txtPhdFacultyDiscipline"); if (ctlPhdFacultyDiscipline != null) ctlPhdFacultyDiscipline.Text = facultyVal;

                    var ctlPhdAcademicYear1 = GetControl<TextBox>("txtPhdAcademicYear1"); if (ctlPhdAcademicYear1 != null) ctlPhdAcademicYear1.Text = getFirstCol(new[] { "AcademicYear1" });
                    //var ctlPhdCOEApprovalDate = GetControl<TextBox>("txtPhdCOEApprovalDate"); if (ctlPhdCOEApprovalDate != null) ctlPhdCOEApprovalDate.Text = string.Empty;

                    var ctlPhdMobile = GetControl<TextBox>("txtPhdMobile"); if (ctlPhdMobile != null) ctlPhdMobile.Text = getFirstCol(new[] { "Mobile Number" });
                    var ctlPhdEmail = GetControl<TextBox>("txtPhdEmail"); if (ctlPhdEmail != null) ctlPhdEmail.Text = getFirstCol(new[] { "E-Mail Address" });

                    // Capture CourseCode from SP for later use
                    string courseCode = getFirstCol(new[] { "CourseCode" });
                    var ctlCourseCode = GetControl<HiddenField>("hfCourseCode");
                    if (ctlCourseCode != null) ctlCourseCode.Value = courseCode;
                    else if (Session["CourseCode"] == null) Session["CourseCode"] = courseCode;
                    // No Dues Id Fetch
                    try
                    {
                        bool noDuesExists = false;
                        // First try: stored-proc may include No_Dues_Id via the LEFT JOIN
                        if (dt.Columns.Contains("No_Dues_Id") && dr["No_Dues_Id"] != DBNull.Value)
                        {
                            txtPhdNoDuesId.Text = dr["No_Dues_Id"].ToString();
                            noDuesExists = true;
                        }

                        // Fallback: query new tbl_PhDStudentNoDues in TMUCON to get latest NoDuesId--Not need
                        //if (!noDuesExists && !string.IsNullOrWhiteSpace(txtPhdEnroll.Text))
                        //{
                        //    try
                        //    {
                        //        using (SqlCommand cmd2 = new SqlCommand(@"SELECT TOP 1 NoDuesId FROM dbo.tbl_PhDStudentNoDues WHERE EnrollmentNo = @enr ORDER BY CreatedDate DESC", con))
                        //        {
                        //            cmd2.Parameters.AddWithValue("@enr", txtPhdEnroll.Text);
                        //            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        //            DataTable dt2 = new DataTable();
                        //            da2.Fill(dt2);
                        //            if (dt2.Rows.Count > 0 && dt2.Rows[0]["NoDuesId"] != DBNull.Value)
                        //            {
                        //                var ctlNoDues = GetControl<TextBox>("txtPhdNoDuesId"); if (ctlNoDues != null) ctlNoDues.Text = dt2.Rows[0]["NoDuesId"].ToString();
                        //                noDuesExists = true;
                        //            }
                        //        }
                        //    }
                        //    catch { }
                        //}

                        // If still not found, generate a new No_Dues_Id for this student (first-time nodues)
                        if (!noDuesExists)
                        {
                            var ctlNoDuesNew = GetControl<TextBox>("txtPhdNoDuesId"); if (ctlNoDuesNew != null) ctlNoDuesNew.Text = GenerateNumberSeries();
                        }

                        // If No Dues Id already exists in DB, disable/hide save and OTP send so student cannot re-save
                        if (noDuesExists)
                        {
                            var btnPhdSaveCtl = GetControl<Button>("btnPhdSave"); if (btnPhdSaveCtl != null) btnPhdSaveCtl.Visible = false;
                            var pnlMobileSendBtn = GetControl<Panel>("pnlPhdMobileSendBtn"); if (pnlMobileSendBtn != null) pnlMobileSendBtn.Visible = false;
                            var pnlEmailSendBtn = GetControl<Panel>("pnlPhdEmailSendBtn"); if (pnlEmailSendBtn != null) pnlEmailSendBtn.Visible = false;
                        }

                    }
                    catch { }
                }
            }
        }
        catch
        {
            // ignore
        }
    }

    public void bindPhdFees()
    {
        try
        {
            if (Session["enroll"] == null || Session["College"] == null || Session["CourseCode"] == null) return;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
            using (SqlCommand cmd = new SqlCommand("usp_GetPhdStudentFeesDetails", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                // Stored procedure expects NVARCHAR parameters
                cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
                cmd.Parameters.AddWithValue("@CollegeCode", Session["College"].ToString());
                cmd.Parameters.AddWithValue("@CourseCode", Session["CourseCode"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Map SP column names to GridView DataField names
                // The SP returns: [Fee Description], FeeAmount, PaidAmount, PendingAmount, [Status], [Receipt No], [Due Date], [Payment Date], [Academic Year], [Admitted Year]
                // GridView expects: FeeDescription, FeeAmount, PaidAmount, PendingAmount, Status, ReceiptNo, DueDate, PaymentDate, AcademicYear, Admitted Year

                if (dt.Rows.Count > 0)
                {
                    // Create new DataTable with GridView-compatible column names
                    DataTable mappedDt = new DataTable();
                    mappedDt.Columns.Add("FeeDescription");
                    mappedDt.Columns.Add("FeeAmount", typeof(decimal));
                    mappedDt.Columns.Add("PaidAmount", typeof(decimal));
                    mappedDt.Columns.Add("PendingAmount", typeof(decimal));
                    mappedDt.Columns.Add("Status");
                    mappedDt.Columns.Add("ReceiptNo");
                    mappedDt.Columns.Add("DueDate");
                    mappedDt.Columns.Add("PaymentDate");
                    mappedDt.Columns.Add("AcademicYear");
                    mappedDt.Columns.Add("Admitted Year");

                    // Copy data from SP columns to mapped columns
                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow newRow = mappedDt.NewRow();
                        newRow["FeeDescription"] = GetSafeValue(row, "Fee Description", string.Empty);
                        newRow["FeeAmount"] = GetSafeValue(row, "FeeAmount", 0m);
                        newRow["PaidAmount"] = GetSafeValue(row, "PaidAmount", 0m);
                        newRow["PendingAmount"] = GetSafeValue(row, "PendingAmount", 0m);
                        newRow["Status"] = GetSafeValue(row, "Status", string.Empty);
                        newRow["ReceiptNo"] = GetSafeValue(row, "Receipt No", string.Empty);
                        newRow["DueDate"] = GetSafeValue(row, "Due Date", DBNull.Value);
                        newRow["PaymentDate"] = GetSafeValue(row, "Payment Date", DBNull.Value);
                        newRow["AcademicYear"] = GetSafeValue(row, "Academic Year", string.Empty);
                        newRow["Admitted Year"] = GetSafeValue(row, "Admitted Year", string.Empty);
                        mappedDt.Rows.Add(newRow);
                    }

                    gvPhdFees.DataSource = mappedDt;
                    gvPhdFees.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            // Log or show error if needed
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "feeError", "alert('Error loading fee details: " + ex.Message + "');", true);
        }
    }

    private object GetSafeValue(DataRow row, string columnName, object defaultValue)
    {
        try
        {
            if (row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
                return row[columnName];
        }
        catch { }
        return defaultValue;
    }


    protected void btnPhdSave_Click(object sender, EventArgs e)
    {
        try
        {
            // Validate pending fees before saving
            if (!ValidatePendingFees())
            {
                return;
            }

            // Get required controls
            var txtPhdNoDuesId = GetControl<TextBox>("txtPhdNoDuesId");
            var txtPhdEnroll = GetControl<TextBox>("txtPhdEnroll");
            var txtPhdStudentNo = GetControl<TextBox>("txtPhdStudentNo");
            var txtPhdMobile = GetControl<TextBox>("txtPhdMobile");
            var txtPhdEmail = GetControl<TextBox>("txtPhdEmail");
            var txtPhdAcademicYear1 = GetControl<TextBox>("txtPhdAcademicYear1");

            // Validate required fields
            if (txtPhdEnroll == null || string.IsNullOrWhiteSpace(txtPhdEnroll.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "valError", "alert('Enrollment No is required.');", true);
                return;
            }

            if (txtPhdStudentNo == null || string.IsNullOrWhiteSpace(txtPhdStudentNo.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "valError", "alert('Student No is required.');", true);
                return;
            }

            if (txtPhdAcademicYear1 == null || string.IsNullOrWhiteSpace(txtPhdAcademicYear1.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "valError", "alert('Academic Year is required.');", true);
                return;
            }

            // Get CourseCode and CollegeCode from Session or HiddenField
            string collegeCode = Session["College"] != null ? Session["College"].ToString() : string.Empty;
            string courseCode = Session["CourseCode"] != null ? Session["CourseCode"].ToString() : string.Empty;
            var hfCourseCode = GetControl<HiddenField>("hfCourseCode");
            if (hfCourseCode != null && !string.IsNullOrWhiteSpace(hfCourseCode.Value))
                courseCode = hfCourseCode.Value;

            if (string.IsNullOrWhiteSpace(collegeCode))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "valError", "alert('College Code is required.');", true);
                return;
            }

            if (string.IsNullOrWhiteSpace(courseCode))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "valError", "alert('Course Code is required.');", true);
                return;
            }

            // Get CreatedBy from Session (adjust key based on your login mechanism)
            string createdBy = Session["enroll"] != null ? Session["enroll"].ToString() : "SYSTEM";                              

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
            using (SqlCommand cmd = new SqlCommand("usp_InsertPhdStudentNodues", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 30;

                // Add parameters matching new SP signature
                cmd.Parameters.AddWithValue("@EnrollmentNo", txtPhdEnroll.Text);
                cmd.Parameters.AddWithValue("@StudentNo", txtPhdStudentNo.Text);
                cmd.Parameters.AddWithValue("@CollegeCode", collegeCode);
                cmd.Parameters.AddWithValue("@CourseCode", courseCode);
                cmd.Parameters.AddWithValue("@MobileNo", txtPhdMobile != null ? txtPhdMobile.Text : string.Empty);
                cmd.Parameters.AddWithValue("@EmailId", txtPhdEmail != null ? txtPhdEmail.Text : string.Empty);
                cmd.Parameters.AddWithValue("@AcademicYear", txtPhdAcademicYear1.Text);
                cmd.Parameters.AddWithValue("@NoDuesId", txtPhdNoDuesId != null ? txtPhdNoDuesId.Text : GenerateNumberSeries());
                cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // SP returns: Id, NoDuesId, EnrollmentNo, AcademicYear
                        int id = reader.GetInt32(0);
                        string noDuesId = reader.GetString(1);

                        // Update textbox with returned NoDuesId
                        if (txtPhdNoDuesId != null)
                            txtPhdNoDuesId.Text = noDuesId;

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "saved", 
                            "alert('PhD No Dues details have been Saved Successfully. Your No Dues ID: " + noDuesId + "');document.location.href='StudentNoDuesPHD.aspx';", true);
                    }
                }
                con.Close();
            }
        }
        catch (SqlException sqlEx)
        {
            // Handle SP validation errors
            string errorMsg = sqlEx.Message;
            if (errorMsg.Contains("already exists"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "exists", 
                    "alert('No Dues request already exists for this Academic Year.');", true);
            }
            else if (errorMsg.Contains("required"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "validation", 
                    "alert('" + errorMsg.Replace("'", "\"") + "');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sqlError", 
                    "alert('Database error: " + errorMsg.Replace("'", "\"") + "');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", 
                "alert('Error while saving PhD details: " + ex.Message.Replace("'", "\"") + "');", true);
        }
    }
    // reuse existing SMS/email implementations where possible
    public void SMS(String MobileNo, string Msg)
    {
        try
        {
            string sendTo = MobileNo;
            string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + sendTo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
            System.Net.HttpWebRequest fr;
            Uri targetURI = new Uri(url);
            fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
            if (fr.GetResponse().ContentLength > 0)
            {
                System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
                // don't write to response
                str.Close();
            }
        }
        catch
        {
            // ignore
        }
    }

    private bool SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            var fromEmail = "naverp@tmu.ac.in";
            var fromPassword = "nwar yzam bcez rqop";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);
            smtpClient.Send(mailMessage);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void lnkPayNow_Click(object sender, EventArgs e)
    {
        try
        {
            // Get the fee description from LinkButton CommandArgument
            LinkButton lnkPayNow = (LinkButton)sender;
            string feeDescription = lnkPayNow.CommandArgument;

            // Get enrollment number and other details
            string enrollmentNo = Session["enroll"] != null ? Session["enroll"].ToString() : string.Empty;
            string collegeCode = Session["College"] != null ? Session["College"].ToString() : string.Empty;
            string courseCode = Session["CourseCode"] != null ? Session["CourseCode"].ToString() : string.Empty;

            // Redirect to payment page with parameters
            // Adjust the URL based on your payment page location
            string paymentPageUrl = string.Format("~/Payment/PayFees.aspx?enroll={0}&college={1}&course={2}&fee={3}", 
                Uri.EscapeDataString(enrollmentNo),
                Uri.EscapeDataString(collegeCode),
                Uri.EscapeDataString(courseCode),
                Uri.EscapeDataString(feeDescription));

            Response.Redirect(paymentPageUrl);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "payError", 
                "alert('Error opening payment page: " + ex.Message.Replace("'", "\"") + "');", true);
        }
    }
}
