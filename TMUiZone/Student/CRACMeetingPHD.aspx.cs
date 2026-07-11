using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_CRACMeeting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["College"] != null && Session["College"].ToString() == "TPHD")
                {
                    DateTime today = DateTime.Today;
                    bool isAllowed = (today.Month == 1 && today.Day >= 1 && today.Day <= 10) || (today.Month == 7 && today.Day >= 1 && today.Day <= 10);
                    if (isAllowed)
                    {
                        divPhd.Visible = true;
                        bindPhdData();
                        
                    }
                    else
                    {
                        divPhd.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('The Ph.D. CRAC Meeting form is available only from 1 January to 10 January and from 1 July to 10 July. Please submit your form during these periods');", true);
                    }
                }
                else
                {
                    
                }
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }



    public void bindPhdData()
    {
        try
        {
            if (Session["enroll"] == null) return;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
            using (SqlCommand cmd = new SqlCommand("usp_GetPhdStudentDetailsForCRACMeeting", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@enroll", Session["enroll"].ToString());
                cmd.Parameters.AddWithValue("@dept", Session["College"].ToString());
                cmd.Parameters.AddWithValue("@CourseCode", Session["CourseCode"].ToString());
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
                    var ctlPhdAcademicYear1 = GetControl<TextBox>("txtPhdAcademicYear1"); if (ctlPhdAcademicYear1 != null) ctlPhdAcademicYear1.Text = getFirstCol(new[] { "AcademicYear1" });
                    //var ctlPhdMobile = GetControl<TextBox>("txtPhdMobile"); if (ctlPhdMobile != null) ctlPhdMobile.Text = getFirstCol(new[] { "Mobile Number" });
                    //var ctlPhdEmail = GetControl<TextBox>("txtPhdEmail"); if (ctlPhdEmail != null) ctlPhdEmail.Text = getFirstCol(new[] { "E-Mail Address" });
                    // Capture CourseCode from SP for later use
                    string courseCode = getFirstCol(new[] { "CourseCode" });
                    var ctlCourseCode = GetControl<HiddenField>("hfCourseCode");
                    if (ctlCourseCode != null) ctlCourseCode.Value = courseCode;
                    else if (Session["CourseCode"] == null) Session["CourseCode"] = courseCode;

                    try
                    {
                        bool IsCRACMeetingExist = false;

                        // If SP returns CRACMeetingName and numeric CRACMeetingNo, prefer numeric selection
                        if (dt.Columns.Contains("CRACMeetingNo") && dr["CRACMeetingNo"] != DBNull.Value)
                        {
                            int cracNo;
                            if (int.TryParse(dr["CRACMeetingNo"].ToString(), out cracNo))
                            {
                                ddlCRACMeeting.SelectedValue = cracNo.ToString();
                                IsCRACMeetingExist = true;
                            }
                        }

                        // Fallback: if only name is returned, set text (best-effort)
                        if (!IsCRACMeetingExist && dt.Columns.Contains("CRACMeetingName") && dr["CRACMeetingName"] != DBNull.Value)
                        {
                            var name = dr["CRACMeetingName"].ToString();
                            // try to map name to ddl value
                            foreach (var item in ddlCRACMeeting.Items)
                            {
                                ListItem li = item as ListItem;
                                if (li != null && li.Text.Equals(name, StringComparison.OrdinalIgnoreCase))
                                {
                                    ddlCRACMeeting.SelectedValue = li.Value;
                                    IsCRACMeetingExist = true;                                
                                    break;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(ddlCRACMeeting.SelectedValue))
                        {
                            bindPhdFees(Convert.ToInt32(ddlCRACMeeting.SelectedValue), IsCRACMeetingExist);
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

    protected void ddlCRACMeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int cracNo;
            if (int.TryParse(ddlCRACMeeting.SelectedValue, out cracNo))
            {
                bool existFlag = false;
                 existFlag = IsCRACMeetingAlreadySaved(cracNo); 
                bindPhdFees(cracNo, existFlag);
            }
            else
            {
                gvPhdFees.DataSource = new DataTable();
                gvPhdFees.EmptyDataText = "Please select a CRAC Meeting.";
                gvPhdFees.DataBind();
                btnPhdSave.Visible = false;
                
            }
        }
        catch { }
    }


    public void bindPhdFees(int CreacMeentinNo, bool IsCRACMeetingExist)
    {
        try
        {
            if (Session["enroll"] == null || Session["College"] == null || Session["CourseCode"] == null) return;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
            using (SqlCommand cmd = new SqlCommand("usp_GetPhdStudentFeesDetailsForPHDCRACMeeting", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                // Stored procedure expects NVARCHAR parameters
                cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
                cmd.Parameters.AddWithValue("@CollegeCode", Session["College"].ToString());
                cmd.Parameters.AddWithValue("@CourseCode", Session["CourseCode"].ToString());
                cmd.Parameters.AddWithValue("@CRACMeetingNo", CreacMeentinNo);
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

                    decimal pendingAmount = Convert.ToDecimal(mappedDt.Rows[0]["PendingAmount"]);
                    string status = mappedDt.Rows[0]["Status"].ToString();
                    if (!IsCRACMeetingExist)
                    {
                        btnPhdSave.Visible = (pendingAmount == 0 && status.Equals("Paid", StringComparison.OrdinalIgnoreCase));
                    }
                        
                }
                else
                {
                    // ensure grid cleared when no fees
                    gvPhdFees.DataSource = new DataTable(); 
                    gvPhdFees.DataBind();
                    btnPhdSave.Visible = false;

                    if (gvPhdFees.Rows.Count == 0)
                    {
                        gvPhdFees.EmptyDataText = "Fee setup is pending. Please contact the Accounts Department.";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log or show error if needed
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "feeError", "alert('Error loading fee details: " + ex.Message + "');", true);
        }
    }

    private bool IsCRACMeetingAlreadySaved(int cracMeetingNo)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        using (SqlCommand cmd = new SqlCommand(@" SELECT COUNT(1) FROM tbl_PhDStudentCRACMeeting WHERE EnrollmentNo=@EnrollmentNo  AND CRACMeetingNo=@CRACMeetingNo AND IsActive=1", con))
        {
            cmd.Parameters.AddWithValue("@EnrollmentNo", Session["enroll"].ToString());
            cmd.Parameters.AddWithValue("@CRACMeetingNo", cracMeetingNo);
            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }
    protected void gvPhdFees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;

        try
        {
            var dataItem = e.Row.DataItem as DataRowView;
            if (dataItem == null) return;

            decimal pending = 0m;
            decimal.TryParse(dataItem["PendingAmount"].ToString(), out pending);

            //var lnk = e.Row.FindControl("lnkPayNow") as LinkButton;
            //if (lnk != null)
            //{
            //    // Enable Pay Now only when pending is 0
            //    lnk.Enabled = (pending == 0m);                
            //}
        }
        catch { }
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

    //protected void lnkPayNow_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        // Get the fee description from LinkButton CommandArgument
    //        LinkButton lnkPayNow = (LinkButton)sender;
    //        string feeDescription = lnkPayNow.CommandArgument;

    //        // Get enrollment number and other details
    //        string enrollmentNo = Session["enroll"] != null ? Session["enroll"].ToString() : string.Empty;
    //        string collegeCode = Session["College"] != null ? Session["College"].ToString() : string.Empty;
    //        string courseCode = Session["CourseCode"] != null ? Session["CourseCode"].ToString() : string.Empty;

    //        // Redirect to payment page with parameters
    //        // Adjust the URL based on your payment page location
    //        string paymentPageUrl = string.Format("~/Payment/PayFees.aspx?enroll={0}&college={1}&course={2}&fee={3}",
    //            Uri.EscapeDataString(enrollmentNo),
    //            Uri.EscapeDataString(collegeCode),
    //            Uri.EscapeDataString(courseCode),
    //            Uri.EscapeDataString(feeDescription));

    //        Response.Redirect(paymentPageUrl);
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "payError",
    //            "alert('Error opening payment page: " + ex.Message.Replace("'", "\"") + "');", true);
    //    }
    //}

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
            using (SqlCommand cmd = new SqlCommand("usp_InsertPhdStudentCRACMeetingFormData", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 30;

                // Add parameters matching usp_InsertPhdStudentCRACMeetingFormData signature
                cmd.Parameters.AddWithValue("@EnrollmentNo", txtPhdEnroll.Text);
                cmd.Parameters.AddWithValue("@StudentNo", txtPhdStudentNo.Text);
                cmd.Parameters.AddWithValue("@CollegeCode", collegeCode);
                cmd.Parameters.AddWithValue("@CourseCode", courseCode);
                cmd.Parameters.AddWithValue("@AcademicYear", txtPhdAcademicYear1.Text);
                // CRACMeetingNo must come from dropdown selection
                int cracNo = 0;
                int.TryParse(ddlCRACMeeting.SelectedValue, out cracNo);
                cmd.Parameters.AddWithValue("@CRACMeetingNo", cracNo);
                cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // SP returns: Id, EnrollmentNo, StudentNo, CollegeCode, CourseCode, CRACMeetingNo, AcademicYear
                        int id = reader.GetInt32(0);
                        // read CRACMeetingNo if present as int at index 5 (safe read)
                        int returnedCrac = cracNo;
                        try { if (!reader.IsDBNull(5)) returnedCrac = reader.GetInt32(5); } catch { }

                        // Hide Save button to prevent duplicate submission
                        var btnPhdSaveCtl = GetControl<Button>("btnPhdSave"); if (btnPhdSaveCtl != null) btnPhdSaveCtl.Visible = false;

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "saved",
                            "alert('PhD CRAC Meeting details have been Saved Successfully.'); window.location='CRACMeetingPHD.aspx';", true);
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
                    "alert('CRAC request already exists for this CRAC Meeting.');", true);
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
                    "alert('You have pending fees. Please clear all dues before submitting CRAC form. Use Pay Now button to pay fees.');", true);
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

    // reuse existing SMS/email implementations where possible
}
