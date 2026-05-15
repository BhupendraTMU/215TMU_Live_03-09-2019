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


public partial class Faculty_StudentInOutAprove1 : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //fromMonth.Attributes["type"] = "month";
                bindGatePassRequestList();
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    public void bindGatePassRequestList()
    {
        string query = "SELECT  '' CWardenStatusText,[ID],[No_],[StudentName],[AcadmicYear],[GatePassNo],[Class],[College],[Hostel],[RoomNo],[FormDate],[ToDate],[NoOfHours],[LunchStatus],[Reason],[WardenCode],[WardenStatus],[WardenRemark],[CWardenCode],[CWardenStatus],[CWardenRemark],[GateMenCode],[OutTime],[InTime],[CreatedBy],[CreatedAt],[UpdatedBy],[UpdatedAt],case when [WardenStatus]=0 then 'Pending'  when [WardenStatus]=1 then 'Approve' when [WardenStatus]=2 then 'Reject'else 'Nothing' end  WardenStatusText FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInfo] WHERE WardenCode = @UserId order by FormDate desc";

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
            getGatePassRequestList.DataSource = dtCL;
            getGatePassRequestList.DataBind();
        }
        catch (Exception ex)
        {
           //tCWardenow new Exception("Error fetching pay slip requests: " + ex.Message, ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
    protected void getGatePassRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string userId = Session["uid"].ToString();

            DataRowView dataItem = (DataRowView)e.Row.DataItem;

            string WardenCode = dataItem["WardenCode"].ToString();
            string CWardenCode = dataItem["CWardenCode"].ToString();
            string GateMenCode = dataItem["GateMenCode"].ToString();
            TextBox txtWardenRemark = (TextBox)e.Row.FindControl("txtWardenRemark");
            Label lblWardenStatus = (Label)e.Row.FindControl("WardenStatusText");

            TextBox txtCWardenRemark = (TextBox)e.Row.FindControl("txtCWardenRemark");
            Label lblCWardenStatus = (Label)e.Row.FindControl("CWardenStatusText");

            Button btnAccept = (Button)e.Row.FindControl("AcceptButton");
            Button btnReject = (Button)e.Row.FindControl("RejectButton");

            bool isWarden = (WardenCode == userId);
            bool isCWarden = (CWardenCode == userId);

            if (txtWardenRemark != null)
                txtWardenRemark.Enabled = isWarden;
            if (lblWardenStatus != null)
                lblWardenStatus.Enabled = isWarden;

            if (txtCWardenRemark != null)
                txtCWardenRemark.Enabled = isCWarden;
            if (lblCWardenStatus != null)
                lblCWardenStatus.Enabled = isCWarden;

            bool isWardenPending = (lblWardenStatus != null && lblWardenStatus.Text.Trim().ToLower() == "pending");
            bool isCWardenPending = (lblCWardenStatus != null && lblCWardenStatus.Text.Trim().ToLower() == "pending");

            bool enableButtons = (isWarden && isWardenPending) || (isCWarden && isCWardenPending);

            if (btnAccept != null)
                btnAccept.Enabled = enableButtons;
            if (btnReject != null)
                btnReject.Enabled = enableButtons;
        }
    }

    protected void getGatePassRequestList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Accept" || e.CommandName == "Reject")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = getGatePassRequestList.Rows[rowIndex];

            HiddenField hfStudentID = row.FindControl("StudentID") as HiddenField;
            Label lblStudentNo = row.FindControl("StudentNo") as Label;
            Label lblWardenStatus = row.FindControl("WardenStatusText") as Label;
            Label lblCWardenStatus = row.FindControl("CWardenStatusText") as Label;

            TextBox txtWardenRemark = row.FindControl("txtWardenRemark") as TextBox;
            TextBox txtCWardenRemark = row.FindControl("txtCWardenRemark") as TextBox;
            string studentId = hfStudentID.Value.Trim() ?? "";
            string studentNo = lblStudentNo.Text.Trim() ?? "";
            string WardenStatus = lblWardenStatus.Text.Trim() ?? "";
            string CWardenStatus = lblCWardenStatus.Text.Trim() ?? "";

            string WardenRemark = txtWardenRemark.Text;
            string CWardenRemark = txtCWardenRemark.Text;

            if (e.CommandName == "Reject")
            {
                if (e.CommandName.Equals("Reject", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(CWardenRemark) && txtCWardenRemark.Enabled)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('CWarden Remark is required when CWarden status is Rejected.'); document.location.href='StudentInOutAprove.aspx';", true);
                    return;
                }

                if (e.CommandName.Equals("Reject", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(WardenRemark) && txtWardenRemark.Enabled)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Warden Remark is required when Warden status is Rejected.');document.location.href='StudentInOutAprove.aspx';", true);
                    return;
                }

                if (txtCWardenRemark.Enabled)
                {
                    UpdateGatePassRequestStatusCWarden(studentId,studentNo, 2, CWardenRemark, Session["uid"].ToString());
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CWarden Status is Rejected.');document.location.href='StudentInOutAprove.aspx';", true);
                }
                if (txtWardenRemark.Enabled)
                {
                    UpdateGatePassRequestStatusWarden(studentId,studentNo, 2, WardenRemark, Session["uid"].ToString());
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Warden Status is Rejected.');document.location.href='StudentInOutAprove.aspx';", true);
                }
            }
            else if (e.CommandName == "Accept")
            {
                if (txtCWardenRemark.Enabled)
                {
                    UpdateGatePassRequestStatusCWarden(studentId,studentNo, 1, CWardenRemark, Session["uid"].ToString());
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Warden Status is Accepted.');document.location.href='StudentInOutAprove.aspx';", true);
                }
                if (txtWardenRemark.Enabled)
                {
                    UpdateGatePassRequestStatusWarden(studentId,studentNo, 1, WardenRemark, Session["uid"].ToString());
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Warden Status is Accepted.');document.location.href='StudentInOutAprove.aspx';", true);
                }
            }

            bindGatePassRequestList();
        }
    }
    private void UpdateGatePassRequestStatusWarden(string studentId, string studentNo, int WardenStatus, string WardenRemark, string updatedBy)
    {
        string query = @"
        UPDATE [dbo].[TMU$StudentGatePassInfo]
        SET 
            WardenStatus = @WardenStatus,
            WardenRemark = @WardenRemark,
            UpdatedBy = @UpdatedBy,
            UpdatedAt = GETDATE()
        WHERE No_ = @studentNo and ID=@studentId";

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@WardenStatus", WardenStatus);
        cmd.Parameters.AddWithValue("@WardenRemark", (object)WardenRemark ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);
        cmd.Parameters.AddWithValue("@studentNo", studentNo);
        cmd.Parameters.AddWithValue("@studentId", studentId);

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
        //    tCWardenow new Exception("Error updating pay slip HOD status: " + ex.Message, ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    private void UpdateGatePassRequestStatusCWarden(string studentId, string studentNo, int CWardenStatus, string CWardenRemark, string updatedBy)
    {
        string query = @"
        UPDATE [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInfo]
        SET 
            CWardenStatus = @CWardenStatus,
            CWardenRemark = @CWardenRemark,
            UpdatedBy = @UpdatedBy,
            UpdatedAt = GETDATE()
        WHERE No_ = @studentNo and ID=@studentId";

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;

        cmd.Parameters.AddWithValue("@CWardenStatus", CWardenStatus);
        cmd.Parameters.AddWithValue("@CWardenRemark", (object)CWardenRemark ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);
        cmd.Parameters.AddWithValue("@studentNo", studentNo);
        cmd.Parameters.AddWithValue("@studentId", studentId);
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
           // tCWardenow new Exception("Error updating pay slip CWarden status: " + ex.Message, ex);
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


