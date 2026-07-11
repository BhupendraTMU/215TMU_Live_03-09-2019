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


public partial class Faculty_ViewStudentCRACFormDetails : System.Web.UI.Page
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
                // Load CRAC meeting records (bind all on load)
                ViewStudentCRACMeetingDetails();
                // Design: ensure enrollment search box is empty on initial load
                try { txtSearchEnrollmentCRAC.Text = string.Empty; } catch { }
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
    protected void gvCRACMeeting_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCRACMeeting.PageIndex = e.NewPageIndex;
            ViewStudentCRACMeetingDetails();
        }
        catch
        {
            lblErrCRAC.Text = "Unable to change page.";
        }
    }





    // Enrollment search button click
    protected void btnSearchCRAC_Click(object sender, EventArgs e)
    {
        try
        {
            string enroll = txtSearchEnrollmentCRAC.Text.Trim();
            string loggedInUserId = Session["uid"].ToString();
            //int approvalLevel = DetermineApprovalLevel(loggedInUserId);

            con.Open();
            DataTable dt = GetStudentCRACMeetingDetails(loggedInUserId);
            con.Close();

            if (!string.IsNullOrEmpty(enroll) && dt != null && dt.Rows.Count > 0)
            {
                DataRow[] rows = dt.Select("EnrollmentNo = '" + enroll.Replace("'", "''") + "'");
                if (rows.Length > 0)
                {
                    DataTable filtered = dt.Clone();
                    foreach (DataRow r in rows) filtered.ImportRow(r);
                    gvCRACMeeting.DataSource = filtered;
                    gvCRACMeeting.DataBind();
                    lblMsgCRAC.Text = "Total " + filtered.Rows.Count + " record(s) found.";
                    return;
                }
                else
                {
                    gvCRACMeeting.DataSource = null;
                    gvCRACMeeting.DataBind();
                    lblMsgCRAC.Text = "No records found for Enrollment No: " + enroll;
                    return;
                }
            }

            // If search empty, reload all
            ViewStudentCRACMeetingDetails();
        }
        catch
        {
            lblErrCRAC.Text = "Something went wrong. Please contact support team.";
        }
        finally
        {
            if (con.State == ConnectionState.Open) con.Close();
        }
    }

    protected void btnClearCRAC_Click(object sender, EventArgs e)
    {
        txtSearchEnrollmentCRAC.Text = string.Empty;
        ViewStudentCRACMeetingDetails();
    }

       /// <summary>
    /// Load PhD No Dues records based on selected approval level
    /// </summary>
    private void ViewStudentCRACMeetingDetails()
    {
        try
        {
            string loggedInUserId = Session["uid"].ToString();
            //int approvalLevel = DetermineApprovalLevel(loggedInUserId);
            con.Open();
            DataTable dt = GetStudentCRACMeetingDetails(loggedInUserId);
            con.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    gvCRACMeeting.DataSource = dt;
                    gvCRACMeeting.DataBind();
                    lblMsgCRAC.Text = "Total " + dt.Rows.Count + " record(s) found.";
                    lblErrCRAC.Text = "";
                }
                else
                {
                    lblMsgCRAC.Text = "No records found.";
                    gvCRACMeeting.DataSource = null;
                    gvCRACMeeting.DataBind();
                    lblErrCRAC.Text = "";
                }
        }
        catch (Exception ex)
        {
            lblErrCRAC.Text = "Something went wrong. Please contact support team.";
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

  

   
    /// <summary>
    /// GridView Row Command Event Handler
    /// </summary>
    protected void gvCRACMeeting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int recordId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "ViewDetails")
            {
                // Load and display student details and fee information
                DisplayStudentDetails(recordId);
                string scriptShow = "jQuery('#detailsModal').modal('show');history.replaceState(null, null, location.pathname + location.search);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showDetailsModal", scriptShow, true);
            }
        }
        catch (Exception ex)
        {
            lblErrCRAC.Text = "Something went wrong. Please contact support team.";
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
            for (int i = 0; i < gvCRACMeeting.Rows.Count; i++)
            {
                if (gvCRACMeeting.DataKeys[i].Value.ToString() == recordId.ToString())
                {
                    rowIndex = i;
                    break;
                }
            }

            if (rowIndex < 0)
                return;

            GridViewRow row = gvCRACMeeting.Rows[rowIndex];

            // Extract values from row and DataKeys (based on GridView column order)
            // Column order: ID(0-hidden), StudentName(1), EnrollmentNo(2), CollegeCode(3), CourseName(4), AcademicYear(5), MobileNo(6), EmailId(7), Status(8), Actions(9)
            string studentName = row.Cells[1].Text;
            string enrollmentNo = row.Cells[2].Text;
            string fathersName = gvCRACMeeting.DataKeys[rowIndex].Values["FathersName"].ToString();
            string collegeCode = gvCRACMeeting.DataKeys[rowIndex].Values["CollegeCode"].ToString();
            string courseCode = gvCRACMeeting.DataKeys[rowIndex].Values["CourseCode"].ToString();
            int cracNo = 0;
            try { cracNo = Convert.ToInt32(gvCRACMeeting.DataKeys[rowIndex].Values["CRACMeetingNo"] ?? 0); } catch { cracNo = 0; }
            // Prefer CRACMeetingName when available
            string cracMeetingName = string.Empty;
            if (gvCRACMeeting.DataKeys[rowIndex] != null && gvCRACMeeting.DataKeys[rowIndex].Values.Contains("CRACMeetingName"))
            {
                cracMeetingName = Convert.ToString(gvCRACMeeting.DataKeys[rowIndex]["CRACMeetingName"]);
            }
            //try { cracMeetingName = gvCRACMeeting.DataKeys[rowIndex].Values["CRACMeetingName"]?.ToString() ?? string.Empty; } catch { cracMeetingName = string.Empty; }
            string collegeName = row.Cells[3].Text;
            string courseName = row.Cells[4].Text;
            string academicYear = row.Cells[5].Text;
            string mobileNo = row.Cells[6].Text;
            string emailId = row.Cells[7].Text;

            // Display student information
            lblDetailStudentNameCRAC.Text = studentName;
            lblDetailEnrollmentNoCRAC.Text = enrollmentNo;
            lblDetailFathersNameCRAC.Text = fathersName;
            lblDetailCollegeCRAC.Text = collegeName;
            lblDetailCourseCRAC.Text = courseName;
            lblDetailAcademicYearCRAC.Text = academicYear;
            lblDetailMobileCRAC.Text = mobileNo;
            lblDetailEmailCRAC.Text = emailId;
            // Show CRAC meeting name if present, otherwise map number to name
            if (!string.IsNullOrEmpty(cracMeetingName))
            {
                lblDetailCRACMeeting.Text = cracMeetingName;
            }
            else
            {
                switch (cracNo)
                {
                    case 1: lblDetailCRACMeeting.Text = "I-CRAC"; break;
                    case 2: lblDetailCRACMeeting.Text = "II-CRAC"; break;
                    case 3: lblDetailCRACMeeting.Text = "III-CRAC"; break;
                    case 4: lblDetailCRACMeeting.Text = "IV-CRAC"; break;
                    case 5: lblDetailCRACMeeting.Text = "V-CRAC"; break;
                    case 6: lblDetailCRACMeeting.Text = "VI-CRAC"; break;
                    default: lblDetailCRACMeeting.Text = string.Empty; break;
                }
            }

            // Load fee details from stored procedure
            con.Open();
            DataTable dtFees = GetPhdStudentFeesDetails(enrollmentNo, collegeCode, courseCode, cracNo);
            con.Close();

            if (dtFees != null && dtFees.Rows.Count > 0)
            {
                gvFeeDetailsCRAC.DataSource = dtFees;
                gvFeeDetailsCRAC.DataBind();
            }
            else
            {
                gvFeeDetailsCRAC.DataSource = null;
                gvFeeDetailsCRAC.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblErrCRAC.Text = "Something went wrong. Please contact support team.";
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }




    /// <summary>
    /// Get PhD Student No Dues records for approval based on user role
    /// </summary>
    /// <param name="loggedInUserId">Current user ID from Session</param>
    /// <param name="approvalLevel">1=HR, 2=PayRole, 3=Account</param>
    /// <returns>DataTable with No Dues records</returns>
    public DataTable GetStudentCRACMeetingDetails(string loggedInUserId)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("usp_GetStudentCRACMeetingDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LoggedInUserId", loggedInUserId);
            //cmd.Parameters.AddWithValue("@ApprovalLevel", approvalLevel);

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
    /// Get Ph.D. Student Fee Details for CRAC meeting (requires CRACMeetingNo)
    /// </summary>
    public DataTable GetPhdStudentFeesDetails(string enrollmentNo, string collegeCode, string courseCode, int cracMeetingNo)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("usp_GetPhdStudentFeesDetailsForPHDCRACMeeting", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentNo", enrollmentNo);
            cmd.Parameters.AddWithValue("@CollegeCode", collegeCode);
            cmd.Parameters.AddWithValue("@CourseCode", courseCode);
            cmd.Parameters.AddWithValue("@CRACMeetingNo", cracMeetingNo);

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
