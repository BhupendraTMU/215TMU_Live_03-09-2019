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


public partial class Faculty_StudentInOutAprove : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //fromMonth.Attributes["type"] = "month";
               // bindGatePassRequestList();
                bindGatePassList("All", DateTime.Now, DateTime.Now);
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


            string inTime = DataBinder.Eval(e.Row.DataItem, "InTime").ToString();    // "1"
            string outTime = DataBinder.Eval(e.Row.DataItem, "OutTime").ToString(); // "1"

            string ToDate = DataBinder.Eval(e.Row.DataItem, "ToDate").ToString(); // "09-12-2025 18:26:00"
            DateTime toDate1;

            string InTime1 = DataBinder.Eval(e.Row.DataItem, "InTime1").ToString(); // "09-12-2025 16:04:22"
            DateTime InTime11;

            bool isValid1 = DateTime.TryParseExact(
               InTime1,
               "dd-MM-yyyy HH:mm:ss",
               System.Globalization.CultureInfo.InvariantCulture,
               System.Globalization.DateTimeStyles.None,
               out InTime11
           );

            // Convert string → datetime
            bool isValid = DateTime.TryParseExact(
                ToDate,
                "dd-MM-yyyy HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out toDate1
            );

            DateTime now = DateTime.Now;

            bool isLate = (isValid && now > toDate1);   // time expire ho gaya

            // 1️⃣ IN + OUT → student returned
            if (inTime == "1" && outTime == "1")
            {
                if (isValid && isValid1 && InTime11 > toDate1)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;     // late return
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Green;   // returned on time
                }
                e.Row.ForeColor = System.Drawing.Color.White;
            }

            // 2️⃣ OUT but NOT returned yet
            else if (inTime == "0" && outTime == "1")
            {
                if (isLate)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;     // still out + time expired
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;  // still out but time remaining
                }
                e.Row.ForeColor = System.Drawing.Color.White;
            }

            // Optional border
            e.Row.BorderStyle = BorderStyle.Solid;
            e.Row.BorderWidth = Unit.Pixel(1);
            e.Row.BorderColor = System.Drawing.Color.Black;
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
            string CWardenStatus =  "";

            string WardenRemark = txtWardenRemark.Text;
            string CWardenRemark = "";

            if (e.CommandName == "Reject")
            {
                //if (e.CommandName.Equals("Reject", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(CWardenRemark) && txtCWardenRemark.Enabled)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('CWarden Remark is required when CWarden status is Rejected.'); document.location.href='StudentInOutAprove.aspx';", true);
                //    return;
                //}

                if (e.CommandName.Equals("Reject", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(WardenRemark) && txtWardenRemark.Enabled)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Warden Remark is required when Warden status is Rejected.');document.location.href='StudentInOutAprove.aspx';", true);
                    return;
                }

                //if (txtCWardenRemark.Enabled)
                //{
                //    UpdateGatePassRequestStatusCWarden(studentId,studentNo, 2, CWardenRemark, Session["uid"].ToString());
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CWarden Status is Rejected.');document.location.href='StudentInOutAprove.aspx';", true);
                //}
                if (txtWardenRemark.Enabled)
                {
                    UpdateGatePassRequestStatusWarden(studentId,studentNo, 2, WardenRemark, Session["uid"].ToString());
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Warden Status is Rejected.');document.location.href='StudentInOutAprove.aspx';", true);
                }
            }
            else if (e.CommandName == "Accept")
            {
                //if (txtCWardenRemark.Enabled)
                //{
                //    UpdateGatePassRequestStatusCWarden(studentId,studentNo, 1, CWardenRemark, Session["uid"].ToString());
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Warden Status is Accepted.');document.location.href='StudentInOutAprove.aspx';", true);
                //}
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string status = ddlStatus.SelectedValue;

        DateTime? fromDate = null;
        DateTime? toDate = null;

        // FROM DATE
        DateTime fDate;
        if (DateTime.TryParse(FromDate.Text, out fDate))
        {
            fromDate = fDate.Date;
        }

        // TO DATE
        DateTime tDate;
        if (DateTime.TryParse(ToDate.Text, out tDate))
        {
            toDate = tDate.Date;
        }

        bindGatePassList(status, fromDate, toDate);


    }

    public void bindGatePassList(string status, DateTime? fromDate, DateTime? toDate)
    {
        string baseQuery = @"
        SELECT 
            CASE WHEN [WardenStatus] = 0 THEN 'Pending'  
                 WHEN [WardenStatus] = 1 THEN 'Approve' 
                 WHEN [WardenStatus] = 2 THEN 'Reject'
                 ELSE 'Nothing' END AS WardenStatusText,
            CASE WHEN [InTime] = 0 THEN 'Pending'
                 WHEN [InTime] = 1 THEN 'In'
                 WHEN [InTime] = 2 THEN 'Reject'
                 ELSE 'Nothing' END AS InTimeText,
            CASE WHEN [OutTime] = 0 THEN 'Pending'
                 WHEN [OutTime] = 1 THEN 'Out'
                 WHEN [OutTime] = 2 THEN 'Reject'
                 ELSE 'Nothing' END AS OutTimeText,
            a1.ID, a1.[No_], [StudentName], [AcadmicYear], a1.[GatePassNo], 
            [Class], [College], [Hostel], [RoomNo], [FormDate], [ToDate], 
            [NoOfHours], [LunchStatus], [Reason], [WardenCode], [WardenStatus], 
            [WardenRemark], [CWardenCode], [CWardenStatus], [CWardenRemark], 
            a1.[GateMenCode], a1.OutTime,
            MAX(CASE WHEN a2.InOutStatus = 'out' THEN a2.DayDate END) AS OutTime1,
            a1.InTime,
            MAX(CASE WHEN a2.InOutStatus = 'in' THEN a2.DayDate END) AS InTime1,  
            [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt] 
        FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInfo] a1 
        LEFT JOIN [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInOutInfo] a2 
            ON a1.GatePassNo = a2.GatePassNo 
        WHERE WardenCode = @UserId
    ";

        // --------------------------------------
        //   STATUS FILTER (in / out / all)
        // --------------------------------------
        if (status == "In")      // a1.InTime = 1 → student inside
            baseQuery += " AND a1.OutTime = 1 AND a1.InTime = 1 ";

        if (status == "Out")     // a1.OutTime = 1 → student outside
            baseQuery += " AND a1.OutTime = 1 AND a1.InTime = 0 ";

        // all → no filter


        // --------------------------------------
        //      DATE FILTER (FormDate / ToDate)
        // --------------------------------------
        // Filter by FormDate
        if (fromDate.HasValue)
        {
            if (!toDate.HasValue)
            {
                // Only fromDate
                baseQuery += " AND CONVERT(date, a1.FormDate) = CONVERT(date, @fromDate) ";
            }
            else
            {
                // Both fromDate and toDate
                baseQuery += " AND CONVERT(date, a1.FormDate) BETWEEN CONVERT(date, @fromDate) AND CONVERT(date, @toDate) ";
            }
        }
        else if (toDate.HasValue)
        {
            // Only toDate
            baseQuery += " AND CONVERT(date, a1.ToDate) <= CONVERT(date, @toDate) ";
        }

        // --------------------------------------
        // GROUP + ORDER
        // --------------------------------------
        baseQuery += @"
        GROUP BY 
            a1.ID, a1.[No_], [StudentName], [AcadmicYear], a1.[GatePassNo], 
            [Class], [College], [Hostel], [RoomNo], [FormDate], [ToDate], 
            [NoOfHours], [LunchStatus], [Reason], a1.OutTime, a1.InTime,  
            [WardenCode], [WardenStatus], [WardenRemark], [CWardenCode], 
            [CWardenStatus], [CWardenRemark], a1.[GateMenCode], 
            [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]
        ORDER BY CreatedAt DESC;
    ";

        SqlCommand cmd = new SqlCommand(baseQuery, con);
        cmd.CommandType = CommandType.Text;

        // Pass date parameters only if they have values
        if (fromDate.HasValue)
            cmd.Parameters.AddWithValue("@fromDate", fromDate.Value);

        if (toDate.HasValue)
            cmd.Parameters.AddWithValue("@toDate", toDate.Value);

        try
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            getGatePassRequestList.DataSource = null;
            getGatePassRequestList.DataBind();
            getGatePassRequestList.DataSource = dt;
            getGatePassRequestList.DataBind();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        // Paging Off (Required)
        getGatePassRequestList.AllowPaging = false;
      
        // Hide Last 2 Columns
        int colCount = getGatePassRequestList.Columns.Count;
        getGatePassRequestList.Columns[colCount - 1].Visible = false;
        getGatePassRequestList.Columns[colCount - 2].Visible = false;

        // Export    
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        getGatePassRequestList.RenderControl(htmlWrite);

        Response.Clear();
        string str = "GatePass_" + Session["AcademicYear"].ToString();
        Response.AddHeader("content-disposition", "attachment;filename=" + str + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered
    }
}


