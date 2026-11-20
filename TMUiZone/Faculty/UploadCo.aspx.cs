using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using System.Activities.Statements;

public partial class Faculty_UploadCo : System.Web.UI.Page
{


    private const string Maker1 = "TMU00650";
    private const string Maker2 = "TMU07987";



    private const string Checker = "TMU00049";


    // =========================================================
    // PAGE LOAD
    // =========================================================

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            string userId = GetLoggedInUserID();


            if (string.IsNullOrEmpty(userId))
            {
                ShowAccessDenied("User session not found.");
                return;
            }


            // =====================================================
            // MAKER
            // =====================================================

            if (userId.Equals(Maker1,
                StringComparison.OrdinalIgnoreCase)
                ||
                userId.Equals(Maker2,
                StringComparison.OrdinalIgnoreCase))
            {

                pnlMaker.Visible = true;
                pnlChecker.Visible = false;
                pnlAccessDenied.Visible = false;

                return;
            }


            // =====================================================
            // CHECKER
            // =====================================================

            if (userId.Equals(Checker,
                StringComparison.OrdinalIgnoreCase))
            {

                pnlMaker.Visible = false;
                pnlChecker.Visible = true;
                pnlAccessDenied.Visible = false;

                if (!IsPostBack)
                {
                    LoadPendingApplications();
                }

                return;
            }


            // =====================================================
            // OTHER USER
            // =====================================================

            ShowAccessDenied(
                "You are not authorized to access Co-Leave Application."
            );
        }
    }


    // =========================================================
    // GET LOGGED IN USER
    // =========================================================

    private string GetLoggedInUserID()
    {

        string userId = "";


        // IMPORTANT:
        // Agar aapke login mein Session ka naam different hai
        // to yahan change karein.

        if (Session["uid"] != null)
        {
            userId =
                Session["uid"].ToString().Trim();
        }


        return userId;
    }


    private void ShowAccessDenied(string message)
    {

        pnlMaker.Visible = false;
        pnlChecker.Visible = false;

        pnlAccessDenied.Visible = true;

        lblAccessDenied.Text = message;
    }



    protected void btnPreview_Click(object sender, EventArgs e)
    {

        lblMessage.Text = "";
        lblMessage.CssClass = "message";


        if (!IsMaker())
        {
            lblMessage.Text =
                "You are not authorized.";

            lblMessage.CssClass =
                "message error";

            return;
        }


        if (!fuExcel.HasFile)
        {

            lblMessage.Text =
                "Please select Excel file.";

            lblMessage.CssClass =
                "message error";

            return;
        }


        string extension =
            Path.GetExtension(
                fuExcel.FileName).ToLower();


        if (extension != ".xls"
            &&
            extension != ".xlsx")
        {

            lblMessage.Text =
                "Please upload only .xls or .xlsx file.";

            lblMessage.CssClass =
                "message error";

            return;
        }


        string filePath = "";


        try
        {

            string folder =
                Server.MapPath("~/Uploads/");


            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }


            filePath =
                Path.Combine(
                    folder,
                    Guid.NewGuid().ToString()
                    + extension);


            fuExcel.SaveAs(filePath);


            string connectionString = "";


            if (extension == ".xlsx")
            {

                connectionString =
                    "Provider=Microsoft.ACE.OLEDB.12.0;"
                    +
                    "Data Source=" + filePath + ";"
                    +
                    "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";
            }
            else
            {

                connectionString =
                    "Provider=Microsoft.ACE.OLEDB.12.0;"
                    +
                    "Data Source=" + filePath + ";"
                    +
                    "Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
            }


            DataTable dt =
                new DataTable();


            using (
                OleDbConnection con =
                new OleDbConnection(
                    connectionString))
            {

                con.Open();


                DataTable sheets =
                    con.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables,
                        null);


                if (sheets == null
                    ||
                    sheets.Rows.Count == 0)
                {

                    throw new Exception(
                        "Excel sheet not found.");
                }


                string sheetName =
                    sheets.Rows[0]["TABLE_NAME"]
                    .ToString();


                using (
                    OleDbDataAdapter da =
                    new OleDbDataAdapter(
                        "SELECT * FROM [" +
                        sheetName +
                        "]",
                        con))
                {

                    da.Fill(dt);
                }

            }


            // =================================================
            // REQUIRED COLUMNS
            // =================================================

            if (!dt.Columns.Contains("Employee Code")
                ||
                !dt.Columns.Contains("Employee Name")
                ||
                !dt.Columns.Contains("Atte_Date")
                ||
                !dt.Columns.Contains("Remarks")
                ||
                !dt.Columns.Contains("Purpose"))
            {

                lblMessage.Text =
                    "Excel columns must be: Employee Code, Employee Name, Atte_Date, Remarks, Purpose";

                lblMessage.CssClass =
                    "message error";

                return;
            }




            for (int i = dt.Rows.Count - 1;
                 i >= 0;
                 i--)
            {

                if (
                    string.IsNullOrWhiteSpace(
                        dt.Rows[i]["Employee Code"]
                        .ToString())
                    &&
                    string.IsNullOrWhiteSpace(
                        dt.Rows[i]["Employee Name"]
                        .ToString())
                )
                {

                    dt.Rows.RemoveAt(i);
                }

            }


            if (dt.Rows.Count == 0)
            {

                lblMessage.Text =
                    "No valid records found in Excel.";

                lblMessage.CssClass =
                    "message error";

                return;
            }


            // =================================================
            // STORE IN SESSION
            // =================================================

            Session["CoLeaveExcelData"] = dt;


            gvExcel.DataSource = dt;
            gvExcel.DataBind();


            btnSave.Visible =
                dt.Rows.Count > 0;


            lblMessage.Text =
                dt.Rows.Count
                +
                " records loaded successfully. Please verify and submit.";

            lblMessage.CssClass =
                "message success";

        }
        catch (Exception ex)
        {

            lblMessage.Text =
                "Error: " + ex.Message;

            lblMessage.CssClass =
                "message error";

        }
        finally
        {

            if (!string.IsNullOrEmpty(filePath)
                &&
                File.Exists(filePath))
            {

                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                }

            }

        }

    }


    // =========================================================
    // MAKER - SAVE / SUBMIT
    // =========================================================

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (!IsMaker())
        {

            lblMessage.Text =
                "You are not authorized.";

            lblMessage.CssClass =
                "message error";

            return;
        }


        DataTable dt =
            Session["CoLeaveExcelData"]
            as DataTable;


        if (dt == null
            ||
            dt.Rows.Count == 0)
        {

            lblMessage.Text =
                "No Excel data found.";

            lblMessage.CssClass =
                "message error";

            return;
        }


        string makerUserID =
            GetLoggedInUserID();


        string cs =
            ConfigurationManager
            .ConnectionStrings[
                "HRMSPortalConnectionString"]
            .ConnectionString;


        int success = 0;
        int duplicate = 0;
        int error = 0;
        int skipped = 0;


        using (
            SqlConnection con =
            new SqlConnection(cs))
        {

            con.Open();


            SqlTransaction tran =
                con.BeginTransaction();


            try
            {

                foreach (GridViewRow gridRow
                         in gvExcel.Rows)
                {

                    CheckBox chk =
                        gridRow.FindControl(
                            "chkSelect")
                        as CheckBox;


                    // =========================================
                    // ONLY SELECTED RECORDS
                    // =========================================

                    if (chk == null
                        ||
                        !chk.Checked)
                    {

                        skipped++;
                        continue;
                    }


                    int rowIndex =
                        gridRow.RowIndex;


                    DataRow row =
                        dt.Rows[rowIndex];


                    string employeeCode =
                        row["Employee Code"]
                        .ToString()
                        .Trim();


                    string excelEmployeeName =
                        row["Employee Name"]
                        .ToString()
                        .Trim();


                    string remarks =
                        row["Remarks"]
                        .ToString()
                        .Trim();


                    string purpose =
                        row["Purpose"]
                        .ToString()
                        .Trim();


                    // =========================================
                    // DATE
                    // =========================================

                    DateTime atteDate;


                    if (!TryGetExcelDate(
                        row["Atte_Date"],
                        out atteDate))
                    {

                        error++;
                        continue;
                    }


                    if (string.IsNullOrEmpty(
                        employeeCode))
                    {

                        error++;
                        continue;
                    }


                    // =========================================
                    // CHECK EMPLOYEE
                    // =========================================

                    string employeeQuery = @"

SELECT TOP 1
       [No_],
       [First Name]
FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee]
WHERE [No_] = @EmployeeCode

";


                    string dbEmployeeName = "";


                    using (
                        SqlCommand cmdEmp =
                        new SqlCommand(
                            employeeQuery,
                            con,
                            tran))
                    {

                        cmdEmp.Parameters.AddWithValue(
                            "@EmployeeCode",
                            employeeCode);


                        using (
                            SqlDataReader dr =
                            cmdEmp.ExecuteReader())
                        {

                            if (!dr.Read())
                            {

                                error++;
                                continue;
                            }


                            dbEmployeeName =
                                dr["First Name"]
                                .ToString()
                                .Trim();
                        }

                    }


                    // =========================================
                    // DUPLICATE CHECK
                    // SAME EMPLOYEE + DATE + PURPOSE
                    // =========================================

                    string duplicateQuery = @"

SELECT COUNT(*)
FROM [dbo].[tbl_Co_Leave_Application]
WHERE [Userid] = @Userid
AND CONVERT(date,[Atte_Date]) = @Atte_Date
AND ISNULL([Purpose],'') = ISNULL(@Purpose,'')

";


                    int existing = 0;


                    using (
                        SqlCommand cmdDup =
                        new SqlCommand(
                            duplicateQuery,
                            con,
                            tran))
                    {

                        cmdDup.Parameters.AddWithValue(
                            "@Userid",
                            employeeCode);


                        cmdDup.Parameters.AddWithValue(
                            "@Atte_Date",
                            atteDate.Date);


                        cmdDup.Parameters.AddWithValue(
                            "@Purpose",
                            purpose);


                        existing =
                            Convert.ToInt32(
                                cmdDup.ExecuteScalar());
                    }


                    if (existing > 0)
                    {

                        duplicate++;
                        continue;
                    }


                    // =========================================
                    // INSERT
                    // =========================================

                    string insertQuery = @"

INSERT INTO [dbo].[tbl_Co_Leave_Application]
(
    [Userid],
    [Uname],
    [CompanyName],
    [Atte_Date],
    [fromTime],
    [ToTime],
    [Job_location],
    [Remarks],
    [Status],
    [Working_Duration],
    [RejectedByHODRemarks],
    [HODUserID],
    [HODName],
    [Co_Leave],
    [Attendance Type],
    [HOD2 Name],
    [HOD2],
    [Approved by],
    [Approval Date],
    [Purpose],
    [ApprovalStatus],
    [Rejected],
    [CreatedDate],
    [ApplyStaff],
    [FinalApprovalID],
    [FinalApprovalStatus],
    [LeaveType]
)

VALUES
(
    @Userid,
    @Uname,
    'TMU',
    @Atte_Date,
    '00:00',
    '00:00',
    'TMU',
    @Remarks,
    'Present',
    NULL,
    NULL,
    'TMU00049',
    'HARSHI JAIN',
    1,
    'Manual',
    NULL,
    NULL,
    NULL,
    NULL,
    @Purpose,
    'Pending',
    'No',
    GETDATE(),
    @ApplyStaff,
    NULL,
    NULL,
    NULL
)

";


                    using (
                        SqlCommand cmd =
                        new SqlCommand(
                            insertQuery,
                            con,
                            tran))
                    {

                        cmd.Parameters.AddWithValue(
                            "@Userid",
                            employeeCode);


                        // DATABASE SE ACTUAL NAME
                        cmd.Parameters.AddWithValue(
                            "@Uname",
                            dbEmployeeName);


                        cmd.Parameters.AddWithValue(
                            "@Atte_Date",
                            atteDate.Date);


                        cmd.Parameters.AddWithValue(
                            "@Remarks",
                            remarks);


                        cmd.Parameters.AddWithValue(
                            "@Purpose",
                            purpose);


                        // MAKER ID
                        cmd.Parameters.AddWithValue(
                            "@ApplyStaff",
                            makerUserID);


                        cmd.ExecuteNonQuery();


                        success++;
                    }

                }


                tran.Commit();

            }
            catch (Exception ex)
            {

                try
                {
                    tran.Rollback();
                }
                catch
                {
                }


                lblMessage.Text =
                    "Error while saving: "
                    + ex.Message;

                lblMessage.CssClass =
                    "message error";

                return;
            }

        }


        Session.Remove(
            "CoLeaveExcelData");


        gvExcel.DataSource = null;
        gvExcel.DataBind();


        btnSave.Visible = false;


        lblMessage.Text =
            "Submission completed. Success: "
            + success
            + " | Duplicate: "
            + duplicate
            + " | Error: "
            + error
            + " | Skipped: "
            + skipped;


        lblMessage.CssClass =
            "message success";

    }


    // =========================================================
    // CHECKER - LOAD PENDING
    // =========================================================

    private void LoadPendingApplications()
    {

        string cs =
            ConfigurationManager
            .ConnectionStrings[
                "HRMSPortalConnectionString"]
            .ConnectionString;


        string query = @"

SELECT
       [ID],
       [Userid],
       [Uname],
       [Atte_Date],
       [Remarks],
       [Purpose],
       [ApplyStaff],
       [CreatedDate],
       [ApprovalStatus]

FROM [dbo].[tbl_Co_Leave_Application]

WHERE ISNULL([ApprovalStatus],'Pending') = 'Pending'
and HODUserID='TMU00049'
ORDER BY [CreatedDate] DESC

";


        DataTable dt =
            new DataTable();


        using (
            SqlConnection con =
            new SqlConnection(cs))
        {

            using (
                SqlDataAdapter da =
                new SqlDataAdapter(
                    query,
                    con))
            {

                da.Fill(dt);
            }

        }


        gvChecker.DataSource = dt;
        gvChecker.DataBind();


        lblCheckerMessage.Text =
            dt.Rows.Count
            + " Pending application(s) found.";

        lblCheckerMessage.CssClass =
            "message success";
    }


    // =========================================================
    // CHECKER - APPROVE
    // =========================================================

    protected void btnApprove_Click(
        object sender,
        EventArgs e)
    {

        if (!IsChecker())
        {

            lblCheckerMessage.Text =
                "You are not authorized.";

            lblCheckerMessage.CssClass =
                "message error";

            return;
        }


        int approved = 0;
        int skipped = 0;


        string checkerID =
            GetLoggedInUserID();


        string cs =
            ConfigurationManager
            .ConnectionStrings[
                "HRMSPortalConnectionString"]
            .ConnectionString;


        using (
            SqlConnection con =
            new SqlConnection(cs))
        {

            con.Open();


            SqlTransaction tran =
                con.BeginTransaction();


            try
            {

                foreach (GridViewRow row
                         in gvChecker.Rows)
                {

                    CheckBox chk =
                        row.FindControl(
                            "chkChecker")
                        as CheckBox;


                    if (chk == null
                        ||
                        !chk.Checked)
                    {

                        skipped++;
                        continue;
                    }


                    int id = Convert.ToInt32(
    gvChecker.DataKeys[row.RowIndex].Value
);


                    // This block is not used because
                    // DataKeys are configured below through
                    // Row.Cells[0] fallback.


                    string idText =
                        row.Cells[0].Text;


                    if (!int.TryParse(
                        idText,
                        out id))
                    {

                        skipped++;
                        continue;
                    }


                    string query = @"

UPDATE [dbo].[tbl_Co_Leave_Application]

SET
    [ApprovalStatus] = 'Approved',
    [Rejected] = 'No',
    [Approved by] = @ApprovedBy,
    [Approval Date] = GETDATE()

WHERE [ID] = @ID
AND ISNULL([ApprovalStatus],'Pending') = 'Pending'

";


                    using (
                        SqlCommand cmd =
                        new SqlCommand(
                            query,
                            con,
                            tran))
                    {

                        cmd.Parameters.AddWithValue(
                            "@ApprovedBy",
                            checkerID);


                        cmd.Parameters.AddWithValue(
                            "@ID",
                            id);


                        int affected =
                            cmd.ExecuteNonQuery();


                        if (affected > 0)
                            approved++;
                        else
                            skipped++;
                    }

                }


                tran.Commit();

            }
            catch (Exception ex)
            {

                try
                {
                    tran.Rollback();
                }
                catch
                {
                }


                lblCheckerMessage.Text =
                    "Approval error: "
                    + ex.Message;

                lblCheckerMessage.CssClass =
                    "message error";

                return;
            }

        }


        txtRejectRemark.Text = "";


        LoadPendingApplications();


        lblCheckerMessage.Text =
            approved
            + " record(s) approved successfully."
            + " Skipped: "
            + skipped;


        lblCheckerMessage.CssClass =
            "message success";
    }


    // =========================================================
    // CHECKER - REJECT
    // =========================================================

    protected void btnReject_Click(
        object sender,
        EventArgs e)
    {

        if (!IsChecker())
        {

            lblCheckerMessage.Text =
                "You are not authorized.";

            lblCheckerMessage.CssClass =
                "message error";

            return;
        }


        string remark =
            txtRejectRemark.Text.Trim();


        if (string.IsNullOrEmpty(remark))
        {

            lblCheckerMessage.Text =
                "Please enter rejection remark.";

            lblCheckerMessage.CssClass =
                "message error";

            return;
        }


        int rejected = 0;
        int skipped = 0;


        string checkerID =
            GetLoggedInUserID();


        string cs =
            ConfigurationManager
            .ConnectionStrings[
                "HRMSPortalConnectionString"]
            .ConnectionString;


        using (
            SqlConnection con =
            new SqlConnection(cs))
        {

            con.Open();


            SqlTransaction tran =
                con.BeginTransaction();


            try
            {

                foreach (GridViewRow row
                         in gvChecker.Rows)
                {

                    CheckBox chk =
                        row.FindControl(
                            "chkChecker")
                        as CheckBox;


                    if (chk == null
                        ||
                        !chk.Checked)
                    {

                        skipped++;
                        continue;
                    }


                    string idText =
                        row.Cells[0].Text;


                    int id;


                    if (!int.TryParse(
                        idText,
                        out id))
                    {

                        skipped++;
                        continue;
                    }


                    string query = @"

UPDATE [dbo].[tbl_Co_Leave_Application]

SET
    [ApprovalStatus] = 'Rejected',
    [Rejected] = 'Yes',
    [RejectedByHODRemarks] = @RejectedRemark,
    [Approved by] = @RejectedBy,
    [Approval Date] = GETDATE()

WHERE [ID] = @ID
AND ISNULL([ApprovalStatus],'Pending') = 'Pending'

";


                    using (
                        SqlCommand cmd =
                        new SqlCommand(
                            query,
                            con,
                            tran))
                    {

                        cmd.Parameters.AddWithValue(
                            "@RejectedRemark",
                            remark);


                        cmd.Parameters.AddWithValue(
                            "@RejectedBy",
                            checkerID);


                        cmd.Parameters.AddWithValue(
                            "@ID",
                            id);


                        int affected =
                            cmd.ExecuteNonQuery();


                        if (affected > 0)
                            rejected++;
                        else
                            skipped++;
                    }

                }


                tran.Commit();

            }
            catch (Exception ex)
            {

                try
                {
                    tran.Rollback();
                }
                catch
                {
                }


                lblCheckerMessage.Text =
                    "Rejection error: "
                    + ex.Message;

                lblCheckerMessage.CssClass =
                    "message error";

                return;
            }

        }


        txtRejectRemark.Text = "";


        LoadPendingApplications();


        lblCheckerMessage.Text =
            rejected
            + " record(s) rejected successfully."
            + " Skipped: "
            + skipped;


        lblCheckerMessage.CssClass =
            "message success";
    }


    // =========================================================
    // CHECK MAKER
    // =========================================================

    private bool IsMaker()
    {

        string userID =
            GetLoggedInUserID();


        return
            userID.Equals(
                Maker1,
                StringComparison.OrdinalIgnoreCase)
            ||
            userID.Equals(
                Maker2,
                StringComparison.OrdinalIgnoreCase);
    }


    // =========================================================
    // CHECK CHECKER
    // =========================================================

    private bool IsChecker()
    {

        string userID =
            GetLoggedInUserID();


        return
            userID.Equals(
                Checker,
                StringComparison.OrdinalIgnoreCase);
    }


    // =========================================================
    // EXCEL DATE CONVERSION
    // =========================================================

    private bool TryGetExcelDate(object value, out DateTime date)
    {
        date = DateTime.MinValue;

        try
        {
            if (value == null || value == DBNull.Value)
            {
                return false;
            }

            // If Excel/DB value is already DateTime
            if (value is DateTime)
            {
                date = (DateTime)value;
                return true;
            }

            string text = Convert.ToString(value).Trim();

            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            // Excel numeric/OLE Automation date
            double serial;

            if (double.TryParse(
                text,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out serial))
            {
                try
                {
                    date = DateTime.FromOADate(serial);
                    return true;
                }
                catch (ArgumentException)
                {
                    // Not a valid OLE Automation date
                }
            }

            // Common Excel date formats
            string[] formats =
            {
            "M/d/yyyy",
            "MM/d/yyyy",
            "M/dd/yyyy",
            "MM/dd/yyyy",

            "d/M/yyyy",
            "dd/M/yyyy",
            "d/MM/yyyy",
            "dd/MM/yyyy",

            "d-M-yyyy",
            "dd-M-yyyy",
            "d-MM-yyyy",
            "dd-MM-yyyy",

            "yyyy-MM-dd",
            "yyyy/MM/dd",

            "dd-MMM-yyyy",
            "d-MMM-yyyy",

            "M/d/yyyy HH:mm",
            "M/d/yyyy HH:mm:ss",
            "MM/dd/yyyy HH:mm",
            "MM/dd/yyyy HH:mm:ss",

            "d/M/yyyy HH:mm",
            "d/M/yyyy HH:mm:ss",
            "dd/MM/yyyy HH:mm",
            "dd/MM/yyyy HH:mm:ss",

            "yyyy-MM-dd HH:mm",
            "yyyy-MM-dd HH:mm:ss"
        };

            if (DateTime.TryParseExact(
                text,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces,
                out date))
            {
                return true;
            }

            // Final fallback
            DateTime parsedDate;

            if (DateTime.TryParse(
                text,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces,
                out parsedDate))
            {
                date = parsedDate;
                return true;
            }
        }
        catch (Exception)
        {
            date = DateTime.MinValue;
        }

        return false;
    }
}