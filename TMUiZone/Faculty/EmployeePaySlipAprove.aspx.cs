using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;


public partial class Faculty_EmployeePaySlipAprove : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    
    protected void Page_Load(object sender, EventArgs e)
    {
       try
        {
            if (!IsPostBack)
            {
                fromMonth.Attributes["type"] = "month";
                bindPaySlipRequestList();          
              }       
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
 
    public void bindPaySlipRequestList()
    {
        string query = "SELECT ID, [EmployeeNo],[EmployeeName],[DepartmentName],[DepartmentCode],[DesignationName],[DesignationCode],[FromMonth],[ToMonth],[HODCode],[HODStatus],[HODRemark],[HRCode],[HRStatus],[HRRemark],[Status],[CreatedBy],[CreatedAt],[UpdatedBy],[UpdatedAt] FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$EmployeePaySlipInfo] WHERE (HODCode = @UserId) OR  (HRCode = @UserId AND HODStatus = 1) order by ID desc";

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());

        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter daCL = new SqlDataAdapter(cmd);
            DataTable dtCL = new DataTable();
            daCL.Fill(dtCL);

            dtCL.Columns.Add("StatusText", typeof(string));
            dtCL.Columns.Add("HODStatusText", typeof(string));
            dtCL.Columns.Add("HrStatusText", typeof(string));

            foreach (DataRow row in dtCL.Rows)
            {
                int HODstatus = row["HODStatus"] != DBNull.Value ? Convert.ToInt32(row["HODStatus"]) : -1;
                switch (HODstatus)
                {
                    case 0:
                        row["HODStatusText"] = "Pending";
                        break;
                    case 2:
                        row["HODStatusText"] = "Reject";
                        break;
                    case 1:
                        row["HODStatusText"] = "Accept";
                        break;
                    default:
                        row["HODStatusText"] = "Unknown";
                        break;
                }

                int hrstatus = row["HRStatus"] != DBNull.Value ? Convert.ToInt32(row["HRStatus"]) : -1;
                switch (hrstatus)
                {
                    case 0:
                        row["HrStatusText"] = "Pending";
                        break;
                    case 2:
                        row["HrStatusText"] = "Reject";
                        break;
                    case 1:
                        row["HrStatusText"] = "Accept";
                        break;
                    default:
                        row["HrStatusText"] = "Unknown";
                        break;
                }

                int status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : -1;
                switch (status)
                {
                    case 0:
                        row["StatusText"] = "Pending";
                        break;
                    case 2:
                        row["StatusText"] = "Reject";
                        break;
                    case 1:
                        row["StatusText"] = "Accept";
                        break;
                    default:
                        row["StatusText"] = "Unknown";
                        break;
                }
            }

            getPaySlipRequestList.DataSource = dtCL;
            getPaySlipRequestList.DataBind();
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching pay slip requests: " + ex.Message, ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
    protected void getPaySlipRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string userId = Session["uid"].ToString();

            DataRowView dataItem = (DataRowView)e.Row.DataItem;

            string hodCode = dataItem["HODCode"].ToString();
            string hrCode = dataItem["HRCode"].ToString();

            TextBox txtHODRemark = (TextBox)e.Row.FindControl("txtHODRemark");
            Label lblHODStatus = (Label)e.Row.FindControl("HODStatusText");

            TextBox txtHRRemark = (TextBox)e.Row.FindControl("txtHRRemark");
            Label lblHRStatus = (Label)e.Row.FindControl("HrStatusText");

            Button btnAccept = (Button)e.Row.FindControl("AcceptButton");
            Button btnReject = (Button)e.Row.FindControl("RejectButton");

            bool isHOD = (hodCode == userId);
            bool isHR = (hrCode == userId);

            if (txtHODRemark != null)
                txtHODRemark.Enabled = isHOD;
            if (lblHODStatus != null)
                lblHODStatus.Enabled = isHOD;

            if (txtHRRemark != null)
                txtHRRemark.Enabled = isHR;
            if (lblHRStatus != null)
                lblHRStatus.Enabled = isHR;

            bool isHODPending = (lblHODStatus != null && lblHODStatus.Text.Trim().ToLower() == "pending");
            bool isHRPending = (lblHRStatus != null && lblHRStatus.Text.Trim().ToLower() == "pending");

            bool enableButtons = (isHOD && isHODPending) || (isHR && isHRPending);

            if (btnAccept != null)
                btnAccept.Enabled = enableButtons;
            if (btnReject != null)
                btnReject.Enabled = enableButtons;
        }
    }

    protected void getPaySlipRequestList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Accept" || e.CommandName == "Reject")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = getPaySlipRequestList.Rows[rowIndex];

            HiddenField hfEmployeeID = row.FindControl("EmployeeID") as HiddenField;
            Label lblEmployeeNo = row.FindControl("EmployeeNo") as Label;
            Label lblHODStatus = row.FindControl("HODStatusText") as Label;
            Label lblHRStatus = row.FindControl("HrStatusText") as Label;

            TextBox txtHODRemark = row.FindControl("txtHODRemark") as TextBox;
            TextBox txtHRRemark = row.FindControl("txtHRRemark") as TextBox;
            string employeeID = hfEmployeeID.Value.Trim() ?? "";
            string employeeNo = lblEmployeeNo.Text.Trim() ?? "";
            string hodStatus = lblHODStatus.Text.Trim() ?? "";
            string hrStatus = lblHRStatus.Text.Trim() ?? "";

            string hodRemark = txtHODRemark.Text;
            string hrRemark = txtHRRemark.Text;

            if (e.CommandName == "Reject")
            {
               if (e.CommandName.Equals("Reject", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(hrRemark) && txtHRRemark.Enabled)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('HR Remark is required when HR status is Rejected.');", true);
                    return;
                }

                if (e.CommandName.Equals("Reject", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(hodRemark) && txtHODRemark.Enabled)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('HOD Remark is required when HOD status is Rejected.');", true);
                    return;
                }

                if (txtHRRemark.Enabled)
                {
                    UpdatePaySlipRequestStatusHr(employeeID,employeeNo, 2, hrRemark, Session["uid"].ToString());
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('HR Status is Rejected.');document.location.href='EmployeePaySlipAprove.aspx';", true);
                }
                if (txtHODRemark.Enabled) {
                    UpdatePaySlipRequestStatusHod(employeeID,employeeNo, 2, hodRemark, Session["uid"].ToString());
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('HOD Status is Rejected.');document.location.href='EmployeePaySlipAprove.aspx';", true);
                }
            }
            else if (e.CommandName == "Accept")
            {
                if (txtHRRemark.Enabled)
                {
                    UpdatePaySlipRequestStatusHr(employeeID,employeeNo, 1, hrRemark, Session["uid"].ToString());
                     ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('HOD Status is Accepted.');document.location.href='EmployeePaySlipAprove.aspx';", true);
                }
                if (txtHODRemark.Enabled) {
                    UpdatePaySlipRequestStatusHod(employeeID,employeeNo, 1, hodRemark, Session["uid"].ToString());
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('HOD Status is Accepted.');document.location.href='EmployeePaySlipAprove.aspx';", true);
                }
            }

            bindPaySlipRequestList();
        }
    }
    private void UpdatePaySlipRequestStatusHod(string employeeID,string employeeNo, int hodStatus, string hodRemark, string updatedBy)
    {
        string query = @"
        UPDATE [dbo].[TMU$EmployeePaySlipInfo]
        SET 
            HODStatus = @HODStatus,
            HODRemark = @HODRemark,
            UpdatedBy = @UpdatedBy,
            UpdatedAt = GETDATE()
        WHERE EmployeeNo = @EmployeeNo and ID = @employeeID";

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@HODStatus", hodStatus);
        cmd.Parameters.AddWithValue("@HODRemark", (object)hodRemark ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);
        cmd.Parameters.AddWithValue("@EmployeeNo", employeeNo);
        cmd.Parameters.AddWithValue("@employeeID", employeeID);

        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            cmd.ExecuteNonQuery();
           
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating pay slip HOD status: " + ex.Message, ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    private void UpdatePaySlipRequestStatusHr(string employeeID, string employeeNo, int hrStatus, string hrRemark, string updatedBy)
    {
        string query = @"
        UPDATE [EDUCOLLEGELIVE-R2].[dbo].[TMU$EmployeePaySlipInfo]
        SET 
            HRStatus = @HRStatus,
            HRRemark = @HRRemark,
            UpdatedBy = @UpdatedBy,
            UpdatedAt = GETDATE()
         WHERE EmployeeNo = @EmployeeNo and ID = @employeeID";

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@HRStatus", hrStatus);
        cmd.Parameters.AddWithValue("@HRRemark", (object)hrRemark ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);
        cmd.Parameters.AddWithValue("@EmployeeNo", employeeNo);
        cmd.Parameters.AddWithValue("@employeeID", employeeID);
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating pay slip Hr status: " + ex.Message, ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}


