using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DL;


public partial class Faculty_PHDStudentNoDuesApproval : System.Web.UI.Page
{
    //PhdNoDuesDL phdNoDuesDL = new PhdNoDuesDL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (Session["uid"] == null)
    
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                // Load approval records (bind all on load)
                LoadApprovalRecords();
                // Design: ensure enrollment search box is empty on initial load
                try { txtEnrollmentSearch.Text = string.Empty; } catch { }
                // Clear modal hidden values so details modal does not auto-open
                try { hiddenRecordId.Value = string.Empty; hiddenApprovalAction.Value = string.Empty; } catch { }
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }

    // Handle GridView paging
    protected void gvNoDuesApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvNoDuesApproval.PageIndex = e.NewPageIndex;
            LoadApprovalRecords();
        }
        catch
        {
            lblError.Text = "Unable to change page.";
        }
    }

    // Approve multiple selected records
    protected void btnApproveSelected_Click(object sender, EventArgs e)
    {
        ProcessSelectedBulkAction(1); // 1 = Approve
    }

    // Reject multiple selected records
    protected void btnRejectSelected_Click(object sender, EventArgs e)
    {
        ProcessSelectedBulkAction(2); // 2 = Reject
    }

    private void ProcessSelectedBulkAction(int action)
    {
        try
        {
            string loggedInUserId = Session["uid"] != null ? Session["uid"].ToString() : string.Empty;
            int approvalLevel = DetermineApprovalLevel(loggedInUserId);

            if (approvalLevel == 0)
            {
                lblError.Text = "You do not have permission to perform this action.";
                return;
            }

            List<int> ids = new List<int>();
            foreach (GridViewRow row in gvNoDuesApproval.Rows)
            {
                CheckBox chk = row.FindControl("chkSelect") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gvNoDuesApproval.DataKeys[row.RowIndex].Value);
                    ids.Add(id);
                }
            }

            if (ids.Count == 0)
            {
                // Inform user to select at least one record
                lblMessage.Text = string.Empty;
                lblError.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "noSelection", "alert('Please select at least one record.');", true);
                return;
            }

            con.Open();
            foreach (int id in ids)
            {
                string result = UpdatePhdNoDuesApprovalStatus(id, action, approvalLevel, loggedInUserId);
                // Optionally check result and log if needed
            }
            con.Close();
            // Refresh grid and show success message
            LoadApprovalRecords();
            lblError.Text = string.Empty;
            lblMessage.Text = "Selected records processed successfully.";
            // Clear client-side selection (header and item checkboxes)
            string clearSelectionScript = "try{var hdr=document.getElementById('chkAll'); if(hdr) hdr.checked=false; var items=document.querySelectorAll('.item-checkbox'); for(var i=0;i<items.length;i++){items[i].checked=false;}}catch(e){}";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bulkResult", "alert('Selected records processed successfully.');" + clearSelectionScript, true);
        }
        catch (Exception ex)
        {
            lblError.Text = "Error processing selected records.";
        }
        finally
        {
            if (con.State == ConnectionState.Open) con.Close();
        }
    }


    // Enrollment search button click
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string enroll = txtEnrollmentSearch.Text.Trim();
            string loggedInUserId = Session["uid"].ToString();
            int approvalLevel = DetermineApprovalLevel(loggedInUserId);

            con.Open();
            DataTable dt = GetPhdNoDuesForApproval(loggedInUserId, approvalLevel);
            con.Close();

            if (!string.IsNullOrEmpty(enroll) && dt != null && dt.Rows.Count > 0)
            {
                DataRow[] rows = dt.Select("EnrollmentNo = '" + enroll.Replace("'", "''") + "'");
                if (rows.Length > 0)
                {
                    DataTable filtered = dt.Clone();
                    foreach (DataRow r in rows) filtered.ImportRow(r);
                    gvNoDuesApproval.DataSource = filtered;
                    gvNoDuesApproval.DataBind();
                    lblMessage.Text = "Total " + filtered.Rows.Count + " record(s) found.";
                    return;
                }
                else
                {
                    gvNoDuesApproval.DataSource = null;
                    gvNoDuesApproval.DataBind();
                    lblMessage.Text = "No records found for Enrollment No: " + enroll;
                    return;
                }
            }

            // If search empty, reload all
            LoadApprovalRecords();
        }
        catch
        {
            lblError.Text = "Something went wrong. Please contact support team.";
        }
        finally
        {
            if (con.State == ConnectionState.Open) con.Close();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtEnrollmentSearch.Text = string.Empty;
        LoadApprovalRecords();
    }

       /// <summary>
    /// Load PhD No Dues records based on selected approval level
    /// </summary>
    private void LoadApprovalRecords()
    {
        try
        {
            string loggedInUserId = Session["uid"].ToString();
            int approvalLevel = DetermineApprovalLevel(loggedInUserId);

            if (approvalLevel == 0)
            {
                lblError.Text = "You do not have permission to access this page.";
                gvNoDuesApproval.DataSource = null;
                gvNoDuesApproval.DataBind();
                return;
            }

            con.Open();
            DataTable dt = GetPhdNoDuesForApproval(loggedInUserId, approvalLevel);
            con.Close();

            if (dt != null && dt.Rows.Count > 0)
            {
                gvNoDuesApproval.DataSource = dt;
                gvNoDuesApproval.DataBind();
                lblMessage.Text = "Total " + dt.Rows.Count + " record(s) found for approval.";
                lblError.Text = "";
            }
            else
            {
                lblMessage.Text = "No records pending for your approval.";
                gvNoDuesApproval.DataSource = null;
                gvNoDuesApproval.DataBind();
                lblError.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Something went wrong. Please contact support team.";
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    /// <summary>
    /// Determine approval level based on user ID and role
    /// </summary>
    private int DetermineApprovalLevel(string userId)
    {
        // Determine approval level based on Session values:
        // HR Department (D228) - only TMU05721 allowed (Level 1)
        // Payroll Department (D213) - all payroll users allowed (Level 2)
        // Accounts Department (D039) - only TMU00245 allowed (Level 3)
        try
        {
            string dept = Session["Departmentcode"] != null ? Session["Departmentcode"].ToString() : string.Empty;
            string uid = Session["uid"] != null ? Session["uid"].ToString() : string.Empty;

            if (dept == "D228" && uid == "TMU05721")
                return 1; // HR

            if (dept == "D213")
                return 2; // Payroll

            if (dept == "D039" && uid == "TMU00245")
                return 3; // Accounts

            return 0; // Not authorized
        }
        catch
        {
            return 0;
        }
    }

   
    /// <summary>
    /// GridView Row Command Event Handler
    /// </summary>
    protected void gvNoDuesApproval_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int recordId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "ViewDetails")
            {
                // Load and display student details and fee information
                DisplayStudentDetails(recordId);
                // Show modal and clear last navigation entry so browser reload won't repost this command
                string scriptShow = "jQuery('#detailsModal').modal('show');history.replaceState(null, null, location.pathname + location.search);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showDetailsModal", scriptShow, true);
            }
            else if (e.CommandName == "ApproveRecord")
            {
                // Show approval confirmation modal
                ShowApprovalConfirmation(recordId, 1); // 1 = Approve
                string scriptApprove = "jQuery('#approvalModal').modal('show');history.replaceState(null, null, location.pathname + location.search);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showApprovalModal", scriptApprove, true);
            }
            else if (e.CommandName == "RejectRecord")
            {
                // Show rejection confirmation modal
                ShowApprovalConfirmation(recordId, 2); // 2 = Reject
                string scriptReject = "jQuery('#approvalModal').modal('show');history.replaceState(null, null, location.pathname + location.search);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showApprovalModal", scriptReject, true);
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Something went wrong. Please contact support team.";
        }
    }

    /// <summary>
    /// Display student details and fee information
    /// </summary>
    private void DisplayStudentDetails(int recordId)
    {
        try
        {
            // Find the row with this record ID
            int rowIndex = -1;
            for (int i = 0; i < gvNoDuesApproval.Rows.Count; i++)
            {
                if (gvNoDuesApproval.DataKeys[i].Value.ToString() == recordId.ToString())
                {
                    rowIndex = i;
                    break;
                }
            }

            if (rowIndex < 0)
                return;

            GridViewRow row = gvNoDuesApproval.Rows[rowIndex];

            // Extract values from row and DataKeys (based on GridView column order)
            // Column order: ID(0-hidden), StudentName(1), EnrollmentNo(2), CollegeCode(3), CourseName(4), AcademicYear(5), MobileNo(6), EmailId(7), Status(8), Actions(9)
            string studentName = row.Cells[1].Text;
            string enrollmentNo = row.Cells[2].Text;
            string fathersName = gvNoDuesApproval.DataKeys[rowIndex].Values["FathersName"].ToString();
            string collegeCode = gvNoDuesApproval.DataKeys[rowIndex].Values["CollegeCode"].ToString();
            string courseCode = gvNoDuesApproval.DataKeys[rowIndex].Values["CourseCode"].ToString();
            string collegeName = row.Cells[3].Text;
            string courseName = row.Cells[4].Text;
            string academicYear = row.Cells[5].Text;
            string mobileNo = row.Cells[6].Text;
            string emailId = row.Cells[7].Text;

            // Display student information
            lblDetailStudentName.Text = studentName;
            lblDetailEnrollmentNo.Text = enrollmentNo;
            lblDetailFathersName.Text = fathersName;
            lblDetailCollege.Text = collegeName;
            lblDetailCourse.Text = courseName;
            lblDetailAcademicYear.Text = academicYear;
            lblDetailMobile.Text = mobileNo;
            lblDetailEmail.Text = emailId;

            // Load fee details from stored procedure
            con.Open();
            DataTable dtFees = GetPhdStudentFeesDetails(enrollmentNo, collegeCode, courseCode);
            con.Close();

            if (dtFees != null && dtFees.Rows.Count > 0)
            {
                gvFeeDetails.DataSource = dtFees;
                gvFeeDetails.DataBind();
            }
            else
            {
                gvFeeDetails.DataSource = null;
                gvFeeDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Something went wrong. Please contact support team.";
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    /// <summary>
    /// Show approval confirmation details
    /// </summary>
    private void ShowApprovalConfirmation(int recordId, int approvalAction)
    {
        try
        {
            int rowIndex = -1;
            for (int i = 0; i < gvNoDuesApproval.Rows.Count; i++)
            {
                if (gvNoDuesApproval.DataKeys[i].Value.ToString() == recordId.ToString())
                {
                    rowIndex = i;
                    break;
                }
            }

            if (rowIndex >= 0)
            {
                GridViewRow row = gvNoDuesApproval.Rows[rowIndex];
                // Column indices: ID(0-hidden), StudentName(1), EnrollmentNo(2), CollegeCode(3), CourseName(4), AcademicYear(5), MobileNo(6), EmailId(7), Status(8), Actions(9)
                lblApprovalStudentName.Text = row.Cells[1].Text;
                lblApprovalEnrollmentNo.Text = row.Cells[2].Text;
                lblApprovalAction.Text = approvalAction == 1 ? "APPROVE" : "REJECT";
                lblApprovalAction.ForeColor = approvalAction == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red;

                hiddenRecordId.Value = recordId.ToString();
                hiddenApprovalAction.Value = approvalAction.ToString();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Something went wrong. Please contact support team.";
        }
    }

    /// <summary>
    /// Confirm Approval Button Click
    /// </summary>
    protected void btnConfirmApproval_Click(object sender, EventArgs e)
    {
        try
        {
            int recordId = Convert.ToInt32(hiddenRecordId.Value);
            int approvalAction = Convert.ToInt32(hiddenApprovalAction.Value);
            string loggedInUserId = Session["uid"].ToString();
            int approvalLevel = DetermineApprovalLevel(loggedInUserId);

            if (approvalLevel == 0)
            {
                lblError.Text = "You do not have permission to approve records.";
                return;
            }

            con.Open();
            // Update approval status in database
            string result = UpdatePhdNoDuesApprovalStatus(recordId,approvalAction,approvalLevel,loggedInUserId);
            con.Close();

            if (result == "Success")
            {
                string actionText = approvalAction == 1 ? "Approved" : "Rejected";
                lblMessage.Text = "Record " + actionText + " successfully!";
                lblError.Text = "";

                // Reload records
                LoadApprovalRecords();

                // Close modal via script
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
                    "jQuery('#approvalModal').modal('hide');", true);
            }
            else
            {
                lblError.Text = "Failed to update record status.";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error processing approval: " + ex.Message;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    /// <summary>
    /// GridView Row Data Bound Event
    /// </summary>
    protected void gvNoDuesApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Customize row styling based on approval status
            DataRowView drv = e.Row.DataItem as DataRowView;
            if (drv != null)
            {
                string statusText = drv["ApprovalStatusText"].ToString();

                if (statusText.Contains("Reject"))
                {
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[8].Font.Bold = true;
                }
                else if (statusText.Contains("Approved"))
                {
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Green;
                    e.Row.Cells[8].Font.Bold = true;
                }
                else
                {
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Orange;
                    e.Row.Cells[8].Font.Bold = true;
                }
            }
        }
    }


    /// <summary>
    /// Get PhD Student No Dues records for approval based on user role
    /// </summary>
    /// <param name="loggedInUserId">Current user ID from Session</param>
    /// <param name="approvalLevel">1=HR, 2=PayRole, 3=Account</param>
    /// <returns>DataTable with No Dues records</returns>
    public DataTable GetPhdNoDuesForApproval(string loggedInUserId, int approvalLevel)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("usp_GetPhdNoDuesForApproval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LoggedInUserId", loggedInUserId);
            cmd.Parameters.AddWithValue("@ApprovalLevel", approvalLevel);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Something went wrong. Please contact support team.");
        }
    }

    /// <summary>
    /// Update PhD Student No Dues approval status
    /// </summary>
    /// <param name="id">Record ID</param>
    /// <param name="approvalAction">1=Approve, 2=Reject</param>
    /// <param name="approvalLevel">1=HR, 2=PayRole, 3=Account</param>
    /// <param name="approvedBy">User ID who approved</param>
    /// <returns>Success message</returns>
    public string UpdatePhdNoDuesApprovalStatus(int id, int approvalAction, int approvalLevel, string approvedBy)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("usp_UpdatePhdNoDuesApprovalStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@ApprovalAction", approvalAction);
            cmd.Parameters.AddWithValue("@ApprovalLevel", approvalLevel);
            cmd.Parameters.AddWithValue("@ApprovedBy", approvedBy);
            cmd.Parameters.AddWithValue("@RejectionReason", DBNull.Value);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Result"].ToString();
            }

            return "Failed";
        }
        catch (Exception ex)
        {
            throw new Exception("Something went wrong. Please contact support team.");
        }
    }

    /// <summary>
    /// Get Ph.D. Student Fee Details
    /// </summary>
    public DataTable GetPhdStudentFeesDetails(string enrollmentNo, string collegeCode, string courseCode)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("usp_GetPhdStudentFeesDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", enrollmentNo);
            cmd.Parameters.AddWithValue("@CollegeCode", collegeCode);
            cmd.Parameters.AddWithValue("@CourseCode", courseCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Something went wrong. Please contact support team.");
        }
    }

    /// <summary>
    /// Get approval level based on user ID and role
    /// </summary>
    public int GetApprovalLevel(string userId)
    {
        try
        {
            // This method is no longer used. Approval level is determined by DetermineApprovalLevel(session)
            return DetermineApprovalLevel(userId);
        }
        catch (Exception ex)
        {
            throw new Exception("Something went wrong. Please contact support team.");
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
        catch { }
    }
}
