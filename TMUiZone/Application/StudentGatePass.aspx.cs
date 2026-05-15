using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Application_StudentGatePass : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string now = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
               // txtFormTime.Attributes["min"] = now;
                GetStudentGatePassList1();
                getSummaryWinterInfo();
                BindDate(Session["uid"].ToString());
                txtStudentNo.Text = Session["uid"].ToString();
                txtStudentName.Text = Session["Name"].ToString();
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    private void getSummaryWinterInfo()
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = @"SELECT TOP 1 
                            [ID],
                            [SummaryWinter],
                            [FromDate],
                            [ToDate],
                            [MaxTime]
                         FROM [EDUCOLLEGELIVE-R2].[dbo].[GatePassSummaryWinterInfo] WHERE CAST(GETDATE() AS DATE) BETWEEN CAST([FromDate] AS DATE) AND CAST([ToDate] AS DATE)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    if (Reader.Read())
                    {
                        // hidden fields fill
                       // hidWinterID.Value = Reader["ID"].ToString();

                        hidFromDate.Value = Reader["FromDate"] != DBNull.Value
                            ? Convert.ToDateTime(Reader["FromDate"]).ToString("yyyy-MM-dd HH:mm")
                            : "";

                        hidToDate.Value = Reader["ToDate"] != DBNull.Value
                            ? Convert.ToDateTime(Reader["ToDate"]).ToString("yyyy-MM-dd HH:mm")
                            : "";

                        hidMaxTime.Value = Reader["MaxTime"] != DBNull.Value
                            ? Reader["MaxTime"].ToString()
                            : "";
                    }
                }
            }
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetStudentGatePassList(string StudentNo)
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = @"SELECT [ID],[No_],[StudentName],[AcadmicYear],[GatePassNo],
                                [Class],[College],[Hostel],[RoomNo],[FormDate],[ToDate],
                                [NoOfHours],[LunchStatus],[Reason],
                                [WardenCode],[WardenStatus],[WardenRemark],
                                [CWardenCode],[CWardenStatus],[CWardenRemark],
                                [OutTime],[InTime],FullAddress,FatherMobileNo,MobileNo,
                                [CreatedBy],[CreatedAt],[UpdatedBy],[UpdatedAt]
                            FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInfo]
                            WHERE [ID] = @StudentNo";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentNo", StudentNo);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    if (Reader != null && Reader.Read())  
                    {
                        return new
                        {
                            ID = Reader["ID"].ToString(),
                            No = Reader["No_"].ToString(),
                            StudentName = Reader["StudentName"].ToString(),
                            AcademicYear = Reader["AcadmicYear"].ToString(),
                            GatePassNo = Reader["GatePassNo"].ToString(),
                            Class = Reader["Class"].ToString(),
                            College = Reader["College"].ToString(),
                            Hostel = Reader["Hostel"].ToString(),
                            RoomNo = Reader["RoomNo"].ToString(),
                            FormDate = Reader["FormDate"] != DBNull.Value
                                          ? Convert.ToDateTime(Reader["FormDate"]).ToString("dd-MM-yyyy hh:mm tt")
                                          : "",

                            StudentMobileNo = Reader["MobileNo"].ToString(),
                            FatherMobileNo = Reader["FatherMobileNo"].ToString(),
                            StudentAddress = Reader["FullAddress"].ToString(),

                            ToDate = Reader["ToDate"] != DBNull.Value
                                          ? Convert.ToDateTime(Reader["ToDate"]).ToString("dd-MM-yyyy hh:mm tt")
                                          : "",      

                            NoOfHours = Reader["NoOfHours"].ToString(),
                            LunchStatus = Reader["LunchStatus"].ToString(),
                            Reason = Reader["Reason"].ToString(),
                            WardenCode = Reader["WardenCode"].ToString(),
                            WardenStatus = Reader["WardenStatus"].ToString(),
                            WardenRemark = Reader["WardenRemark"].ToString(),
                            CWardenCode = Reader["CWardenCode"].ToString(),
                            CWardenStatus = Reader["CWardenStatus"].ToString(),
                            CWardenRemark = Reader["CWardenRemark"].ToString(),
                            OutTime = Reader["OutTime"].ToString(),
                            InTime = Reader["InTime"].ToString(),
                            CreatedBy = Reader["CreatedBy"].ToString(),
                            CreatedAt = Reader["CreatedAt"].ToString(),
                            UpdatedBy = Reader["UpdatedBy"].ToString(),
                            UpdatedAt = Reader["UpdatedAt"].ToString()
                        };
                    }
                }
            }
        }
        return null; // ✅ return null if no record
    }
    public void GetStudentGatePassList1()
    {
        string query = "SELECT [ID],[No_],[StudentName],AcadmicYear,[GatePassNo],Reason,[Class],[College],[Hostel],[RoomNo],[FormDate],[ToDate],[NoOfHours],[LunchStatus],[WardenCode],[WardenStatus],[WardenRemark],[CWardenCode],[CWardenStatus],[CWardenRemark],[CreatedBy],[CreatedAt],[UpdatedBy],[UpdatedAt] FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInfo] WHERE No_ = @UserId";

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

            dtCL.Columns.Add("WardenStatusText", typeof(string));
            dtCL.Columns.Add("CWardenStatusText", typeof(string));
            dtCL.Columns.Add("LunchStatusText", typeof(string));
            foreach (DataRow row in dtCL.Rows)
            {
                // WardenStatus
                int WardenStatus = row["WardenStatus"] != DBNull.Value ? Convert.ToInt32(row["WardenStatus"]) : -1;
                switch (WardenStatus)
                {
                    case 0:
                        row["WardenStatusText"] = "Pending";
                        break;
                    case 2:
                        row["WardenStatusText"] = "Reject";
                        break;
                    case 1:
                        row["WardenStatusText"] = "Accept";
                        break;
                    default:
                        row["WardenStatusText"] = "Unknown";
                        break;
                }
                // CWardenStatus
                int CWardenStatus = row["CWardenStatus"] != DBNull.Value ? Convert.ToInt32(row["CWardenStatus"]) : -1;
                switch (WardenStatus)
                {
                    case 0:
                        row["CWardenStatusText"] = "Pending";
                        break;
                    case 2:
                        row["CWardenStatusText"] = "Reject";
                        break;
                    case 1:
                        row["CWardenStatusText"] = "Accept";
                        break;
                    default:
                        row["CWardenStatusText"] = "Unknown";
                        break;
                }
                int LunchStatus = row["LunchStatus"] != DBNull.Value ? Convert.ToInt32(row["LunchStatus"]) : -1;
                switch (LunchStatus)
                {
                    case 0:
                        row["LunchStatusText"] = "No";
                        break;
                    case 1:
                        row["LunchStatusText"] = "Yes";
                        break;
                    default:
                        row["LunchStatusText"] = "Unknown";
                        break;
                }
            }

            getGatePassRequestList.DataSource = dtCL;
            getGatePassRequestList.DataBind();
        }
        catch (Exception ex)
        {
            // Handle or log the error as needed
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
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string reason = Request.Form["txtReason"];
        string reason1 = Request.Form["ContentPlaceHolder1_txtReason"];
        DateTime selectedDate;

        if (DateTime.TryParse(hiddenfromDate.Value, out selectedDate))
        {
            // Only take the Date part
            DateTime dateOnly = selectedDate.Date;
        }
        int WardenStatus = 0;
        DateTime todatetime = DateTime.Parse(hiddenToDate.Value);
        DateTime fromdatetime = DateTime.Parse(hiddenfromDate.Value);
        if (CheckDateIsPresentOrNot(todatetime, fromdatetime))
        {
            WardenStatus = 1;
        }
            try
        {
            using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
            {
               
                con1.Open();

                // Step 1: Generate GatePassNo based on current date and a unique number (e.g., incremented counter)
                string gatePassNo = GenerateGatePassNo();
                // Step 2: Insert the new record with the auto-generated GatePassNo
                string insertQuery = @"
            INSERT INTO [TMU$StudentGatePassInfo] 
            (No_, StudentName,AcadmicYear,Reason,  GatePassNo, [Class], College, Hostel, RoomNo, FormDate, ToDate, NoOfHours,InTime,OutTime, 
             LunchStatus, WardenCode, WardenStatus,  CWardenCode, CWardenStatus,GateMenCode,MobileNo,FatherMobileNo,FullAddress, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
            VALUES
            (@No_, @StudentName,@AcadmicYear,@Reason, @GatePassNo, @Class, @College, @Hostel, @RoomNo,@FormDate, @ToDate, @NoOfHours,@InTime,@OutTime,
             @LunchStatus, @WardenCode, @WardenStatus, @CWardenCode, @CWardenStatus,@GateMenCode,@MobileNo,@FatherMobileNo,@FullAddress, @CreatedBy, @CreatedAt, @UpdatedBy, @UpdatedAt)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con1))
                {
                    cmd.Parameters.AddWithValue("@No_", Session["uid"].ToString()); 
                    cmd.Parameters.AddWithValue("@StudentName", Session["Name"].ToString());
                    cmd.Parameters.AddWithValue("@AcadmicYear", txtAcadmicYear.Text);
                    cmd.Parameters.AddWithValue("@GatePassNo", gatePassNo); 
                    cmd.Parameters.AddWithValue("@Class", txtClass.Text);  
                    cmd.Parameters.AddWithValue("@College", txtCollege.Text);  
                    cmd.Parameters.AddWithValue("@Hostel", txtHostel.Text);  
                    cmd.Parameters.AddWithValue("@RoomNo", txtRoomNo.Text);
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(hiddenToDate.Value));
                    cmd.Parameters.AddWithValue("@FormDate", DateTime.Parse(hiddenfromDate.Value));
                    cmd.Parameters.AddWithValue("@NoOfHours", hiddenNoOfHours.Value.ToString()); 
                    cmd.Parameters.AddWithValue("@LunchStatus", int.Parse(ddlLunchStatus.SelectedValue)); 
                    cmd.Parameters.AddWithValue("@Reason", txtReason.Text);
                    cmd.Parameters.AddWithValue("@WardenCode", hfWarden.Value);  
                    cmd.Parameters.AddWithValue("@WardenStatus", WardenStatus);
                    cmd.Parameters.AddWithValue("@CWardenCode", "TMU03762");
                    cmd.Parameters.AddWithValue("@GateMenCode", "");
                    cmd.Parameters.AddWithValue("@CWardenStatus", 0);
                    cmd.Parameters.AddWithValue("@InTime", 0);
                    cmd.Parameters.AddWithValue("@OutTime", 0);
                    cmd.Parameters.AddWithValue("@MobileNo", hfMobileNumber.Value); 
                    cmd.Parameters.AddWithValue("@FatherMobileNo", hfFatherNumber.Value);
                    cmd.Parameters.AddWithValue("@FullAddress", hfFullAddress.Value);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Out Pass Inserted Successfully'); document.location.href='StudentGatePass.aspx';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insert Failed: No rows affected');", true);
                    }
                }

                con1.Close();
            }
        }
        catch (SqlException sqlEx)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('SQL Error: " + sqlEx.Message + "');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error: " + ex.Message + "');", true);
        }
    }

    private string GenerateGatePassNo()
    {
        string prefix = Session["uid"].ToString();

        string datePart = DateTime.Now.ToString("ddMMyyyy");

        string timePart = DateTime.Now.ToString("HHmmss");

        string gatePassNo = prefix + "/" + datePart + "" + timePart;

        return gatePassNo;
    }
    // Method to generate GatePassNo
    //private string GenerateGatePassNo(SqlConnection con)
    //{
    //    // Use the current date to generate the GatePassNo
    //    string datePart = DateTime.Now.ToString("yyyyMMdd"); // Format the date to yyyyMMdd
    //    string prefix = Session["uid"].ToString(); // Prefix for GatePassNo

    //    // Find the latest number for this date
    //    string query = @"
    //SELECT MAX(CAST(SUBSTRING(GatePassNo, 9, 3) AS INT)) 
    //FROM [TMU$StudentGatePassInfo]
    //WHERE GatePassNo LIKE @DatePart";

    //    using (SqlCommand cmd = new SqlCommand(query, con))
    //    {
    //        cmd.Parameters.AddWithValue("@DatePart", prefix + datePart + "%");

    //        object result = cmd.ExecuteScalar();
    //        int nextNumber = result != DBNull.Value ? (int)result + 1 : 1; // Increment the number if records exist, otherwise start at 1

    //        // Format the number with leading zeros
    //        string formattedNumber = nextNumber.ToString("D3"); // 3 digits with leading zeros

    //        return prefix + datePart + "-" + formattedNumber; // Generate GatePassNo in the format GP-yyyyMMdd-001
    //    }
    //}
    public void BindDate(string FacultyCode)
    {
        string query = @"
SELECT TOP 1 
    [College], 
    [No_], 
    [Student Name], 
    [Global Dimension 1 Code], 
    [Global Dimension 2 Code], 
    [Course Code], 
    [Hostel Acommodation], 
    [E-Mail Address], 
    [Mobile Number], 
    [Phone Number], 
    [Semester], 
    [Section], 
    [Academic Year], 
    [Current Year], 
    [Application No_], 
    [Room No_], 
    [Hostel Code], 
    [Hostel Alloted], 
    [Hostel Vacated], 
    [Room Type], 
    [Mess], 
    [Specialization], 
    [Group], 
    [Batch], 
    [Type Of Course], 
    [Final Years Course], 
    [Year], 
    [Course Name], 
    [Session], 
    [Enrollment No_], 
    [Finger No_],
   (select [Warden Emp Code] from TMU$Hostel where Code=[Hostel Code]) 'Warden Emp Code' ,
    CASE 
        WHEN LTRIM(RTRIM([Father Mobile No])) = '' OR [Father Mobile No] IS NULL 
            THEN [Father’s_Guardian Mobile No_] 
        ELSE [Father Mobile No]  
    END AS [FatherMobileNo], 
    [Mobile Number] AS [StudentMobileNumber],
    [Address1]+ ', ' + [Address2] + ', ' + [City] + ', ' + [Post Code] + ', ' + [State] AS FullAddress
FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$Student - COLLEGE] 
WHERE [No_] = @UserId";

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

            foreach (DataRow row in dtCL.Rows)
            {
                hfWarden.Value = row["Warden Emp Code"].ToString();
                // Populate controls with data
                txtStudentNo.Text = row["No_"].ToString();
                txtStudentName.Text = row["Student Name"].ToString();
                txtAcadmicYear.Text = row["Academic Year"].ToString();
                txtClass.Text = row["Course Name"].ToString();
                txtCollege.Text = row["Global Dimension 1 Code"].ToString();
                txtHostel.Text = row["Hostel Code"].ToString();
                txtRoomNo.Text = row["Room No_"].ToString();
                hfFullAddress.Value = row["FullAddress"].ToString();
                hfMobileNumber.Value = row["Mobile Number"].ToString();
                hfFatherNumber.Value = row["FatherMobileNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            // Handle or log the error as needed
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



    public bool CheckDateIsPresentOrNot(DateTime ToDate,DateTime Fromdatetime)
    {
        // First, check if the date is Sunday
        if (ToDate.DayOfWeek == DayOfWeek.Sunday && Fromdatetime.DayOfWeek == DayOfWeek.Sunday)
        {
            return true; // It's a Sunday
        }

        string query = @"
        SELECT [timestamp],
               [Branch Code],
               [Date],
               [Line No],
               [Holiday Description],
               [OT Applicable],
               [National Holiday]
        FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$Pay Holidays]
        WHERE CAST([Date] AS DATE) = @ToDate and CAST([Date] AS DATE) = @FromDate";

        try
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate.Date;
                cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = Fromdatetime.Date;
                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true; // Record found in the database (holiday)
                    }
                    else
                    {
                        return false; // No record found
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching pay slip requests: " + ex.Message, ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }


}