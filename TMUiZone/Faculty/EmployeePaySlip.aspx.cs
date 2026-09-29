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


public partial class Faculty_EmployeePaySlip : System.Web.UI.Page
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

                BindDate(Session["uid"].ToString());
                txtEmployeeNo.Text = Session["uid"].ToString();
                txtEmployeeName.Text = Session["uname"].ToString();
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetSalarySlip(string employeeNo, string month)
    {
        List<Dictionary<string, object>> employeeDetails =
            new List<Dictionary<string, object>>();

        string connStr =
            ConfigurationManager.ConnectionStrings["TMUCON"].ToString();

        DateTime salaryMonth;

        if (!DateTime.TryParse(month, out salaryMonth))
        {
            salaryMonth = DateTime.Now;
        }

        int monthNo = salaryMonth.Month;
        int yearNo = salaryMonth.Year;

        using (SqlConnection con = new SqlConnection(connStr))
        {
            con.Open();

            // =========================================================
            // 1. EMPLOYEE + PAY DETAILS + HOLIDAY/SUNDAY
            // =========================================================

            string query = @"

        /* =====================================================
           EMPLOYEE INFORMATION
           ===================================================== */

        SELECT
             [No_] EmployeeNo,
             [First Name],
             [Middle Name],
             [Last Name],
             [Full Name],
             [Father Name],
             [Employment Date],
             [Actual Date of Joining],
             [PAN No],
             [ESI No],
             [PF No],
             [UAN No],
             [Department Code],
             [Department Name] DepartmentName,
             [Department],
             [Job Title],
             [Job Title_Grade],
             [Job Title_Grade Desc],
             (
                 SELECT [Designation Description]
                 FROM [TMU$Designation Master]
                 WHERE [Designation Code] = EMP.[Designation Code]
             ) AS DesignationName,
             [Branch Code],
             [Branch Name],
             [Location Code],
             [Company E-Mail],
             [E-Mail],
             [Branch Code] AS Unit,
             [Mobile Phone No_],
             [Office Mobile],
             [Bank Name],
             [Employee Bank Name],
             [Account No],
             [Bank IFSC Code],
             [Payment Method],
             [Gender],
             [Marital Status],
             [Spouse Name],
             [State],
             [City],
             [Address]
        FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$Employee] EMP
        WHERE [No_] = @EmployeeNo;


        /* =====================================================
           PAY DETAILS
           ===================================================== */

        SELECT
             [Year],
             [Month],
             [Employee No],
             [Pay Element Code],
             [ForMonthDate],
             [Paid Category],
             [Type],
             [Included In Pay Slip],
             [Sorting Order],
             [Paid Days] - ([Holidays] + [Off Days]) AS [Present Days],
             [LWP Days Full],
             [LWP Days Half],
             [Leave Days Full],
             [Leave Days Half],
             [Overtime Hours],
             [Paid Days],
             [Holidays],
             [Off Days],
             [Actual Amount],
             [Payable Amount],
             [Arrear Amount],
             [Employer Contribition],
             [Employer Contribition2],
             [Salary],
             [EPS Salary],
             [Department Code],
             [Branch Code],
             [Job Title_Grade],
             [Location Code],
             [Job Title Code],
             [PayRollMonthDate],
             [Document No_],
             [DateFilter],
             [Bank Code],
             [Bonus _],
             [VPF _],
             [VPF Amount],
             [Extra PF _],
             [Employer_s PF Contribution],
             [Deduction For LWP],
             [Irregular],
             [Payment Indicator],
             [Posting Group],
             [Pay Bus_ Posting Group],
             [Pay Prod_ Posting Group],
             [Currency Code],
             [Actual Amount (LCY)],
             [Payable Amount (LCY)],
             [Employee Name]
        FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$Pay Employee Pay Details]
        WHERE [Employee No] = @EmployeeNo
          AND [Month] = @Month
          AND [Year] = @Year
          AND ISNULL([Included In Pay Slip], 1) = 1
        ORDER BY [Sorting Order];


        /* =====================================================
           HOLIDAY COUNT
           Holiday + Sunday
           Same Sunday/Holiday date only counted once
           ===================================================== */

        SELECT COUNT(*) AS HolidayCount
        FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$Pay Holidays]
        WHERE [Branch Code] = (
                SELECT TOP 1 [Branch Code]
                FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$Employee]
                WHERE [No_] = @EmployeeNo
              )
          AND MONTH([Date]) = @Month
          AND YEAR([Date]) = @Year;


        /* =====================================================
           SUNDAY COUNT
           ===================================================== */

        SELECT COUNT(*) AS SundayCount
        FROM
        (
            SELECT DATEADD(
                       DAY,
                       number,
                       DATEFROMPARTS(@Year, @Month, 1)
                   ) AS [Date]
            FROM master..spt_values
            WHERE type = 'P'
              AND number < DAY(
                    EOMONTH(
                        DATEFROMPARTS(@Year, @Month, 1)
                    )
              )
        ) A
        WHERE DATENAME(WEEKDAY, [Date]) = 'Sunday';
        ";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@EmployeeNo", SqlDbType.NVarChar, 50)
                              .Value = employeeNo;

                cmd.Parameters.Add("@Month", SqlDbType.Int)
                              .Value = monthNo;

                cmd.Parameters.Add("@Year", SqlDbType.Int)
                              .Value = yearNo;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // =================================================
                    // EMPLOYEE INFORMATION
                    // =================================================

                    if (!reader.Read())
                    {
                        return null;
                    }

                    Dictionary<string, object> row =
                        new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] =
                            reader.IsDBNull(i)
                            ? null
                            : reader.GetValue(i);
                    }

                    // =================================================
                    // EMPLOYEE BASIC INFORMATION
                    // =================================================

                    row["FatherName"] =
                        GetValue(row, "Father Name");

                    row["DOJ"] =
                        GetDateString(row, "Employment Date");

                    if (string.IsNullOrEmpty(Convert.ToString(row["DOJ"])))
                    {
                        row["DOJ"] =
                            GetDateString(row, "Employment Date");
                    }

                    row["PAN"] =
                        GetValue(row, "PAN No");

                    row["ESINo"] =
                        GetValue(row, "ESI No");

                    row["UAN"] =
                        GetValue(row, "UAN No");

                    row["EmployeeName"] =
                        GetValue(row, "Full Name");

                    if (string.IsNullOrEmpty(
                        Convert.ToString(row["EmployeeName"])))
                    {
                        row["EmployeeName"] =
                            (
                                Convert.ToString(
                                    GetValue(row, "First Name"))
                                + " "
                                + Convert.ToString(
                                    GetValue(row, "Middle Name"))
                                + " "
                                + Convert.ToString(
                                    GetValue(row, "Last Name"))
                            ).Trim();
                    }

                    row["SalaryMonth"] =
                        salaryMonth.ToString("MMMM yyyy");

                    // =================================================
                    // MOVE TO PAY DETAILS RESULT
                    // =================================================

                    reader.NextResult();

                    List<Dictionary<string, object>> earnings =
                        new List<Dictionary<string, object>>();

                    List<Dictionary<string, object>> deductions =
                        new List<Dictionary<string, object>>();

                    decimal grossEarning = 0;
                    decimal grossDeduction = 0;

                    decimal totalPaidDays = 0;
                    decimal totalPresentDays = 0;
                    decimal totalLeaveDays = 0;
                    decimal totalLWPDays = 0;
                    decimal totalOffDays = 0;
                    decimal totalOvertime = 0;

                    bool firstPayRow = true;

                    // =================================================
                    // PAY DETAILS
                    // =================================================

                    while (reader.Read())
                    {
                        Dictionary<string, object> pay =
                            new Dictionary<string, object>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            pay[reader.GetName(i)] =
                                reader.IsDBNull(i)
                                ? null
                                : reader.GetValue(i);
                        }

                        decimal actualAmount =
                            ToDecimal(
                                GetValue(pay, "Actual Amount"));

                        decimal payableAmount =
                            ToDecimal(
                                GetValue(pay, "Payable Amount"));

                        decimal arrearAmount =
                            ToDecimal(
                                GetValue(pay, "Arrear Amount"));

                        decimal amount = payableAmount;

                        // =============================================
                        // ATTENDANCE
                        // =============================================

                        if (firstPayRow)
                        {
                            totalPaidDays =
                                ToDecimal(
                                    GetValue(pay, "Paid Days"));

                            totalPresentDays =
                                ToDecimal(
                                    GetValue(pay, "Present Days"));

                            totalLeaveDays =
                                ToDecimal(
                                    GetValue(pay, "Leave Days Full"))
                                +
                                ToDecimal(
                                    GetValue(pay, "Leave Days Half"));

                            totalLWPDays =
                                ToDecimal(
                                    GetValue(pay, "LWP Days Full"))
                                +
                                ToDecimal(
                                    GetValue(pay, "LWP Days Half"));

                            totalOffDays =
                                ToDecimal(
                                    GetValue(pay, "Off Days"));

                            totalOvertime =
                                ToDecimal(
                                    GetValue(pay, "Overtime Hours"));

                            firstPayRow = false;
                        }

                        // =============================================
                        // PAY ELEMENT
                        // =============================================

                        string payElement =
                            Convert.ToString(
                                GetValue(pay, "Pay Element Code"));

                        string type =
                            Convert.ToString(
                                GetValue(pay, "Type"));

                        // =============================================
                        // EARNING / DEDUCTION
                        // =============================================

                        Dictionary<string, object> salaryItem =
                            new Dictionary<string, object>();

                        salaryItem["Particular"] =
                            payElement;

                        salaryItem["PayElementCode"] =
                            payElement;

                        salaryItem["Type"] =
                            type;

                        salaryItem["PayRate"] =
                            actualAmount;

                        salaryItem["PaidDays"] =
                            GetValue(pay, "Paid Days");

                        salaryItem["PayEarned"] =
                            amount;

                        salaryItem["ActualAmount"] =
                            actualAmount;

                        salaryItem["PayableAmount"] =
                            payableAmount;

                        salaryItem["ArrearAmount"] =
                            arrearAmount;


                        // =================================================
                        // DEDUCTION
                        // =================================================
                        if (actualAmount < 0)
                        {
                            //if (type.Equals(
                            //        "Deduction",
                            //        StringComparison.OrdinalIgnoreCase)
                            //    ||
                            //    type.Equals(
                            //        "D",
                            //        StringComparison.OrdinalIgnoreCase))
                            //{
                                // ---------------------------------------------
                                // Always keep deduction positive internally
                                // ---------------------------------------------

                                decimal deductionAmount =
                                    Math.Abs(amount);

                                decimal deductionActualAmount =
                                    Math.Abs(actualAmount);

                                decimal deductionPayableAmount =
                                    Math.Abs(payableAmount);


                                // ---------------------------------------------
                                // Display amount as NEGATIVE
                                // ---------------------------------------------

                                deductions.Add(
                                    new Dictionary<string, object>
                                    {
                                {
                                    "Particular",
                                    payElement
                                },
                                {
                                    "PayElementCode",
                                    payElement
                                },
                                {
                                    "Amount",
                                    deductionAmount
                                },
                                {
                                    "ActualAmount",
                                    deductionActualAmount
                                },
                                {
                                    "PayableAmount",
                                    deductionPayableAmount
                                }
                                    });


                                // ---------------------------------------------
                                // Gross deduction remains POSITIVE
                                // ---------------------------------------------

                                grossDeduction += deductionAmount;
                            //}
                        }
                        else
                        {
                            // =================================================
                            // EARNING
                            // =================================================

                            earnings.Add(salaryItem);

                            grossEarning += amount;
                        }
                    }

                    // =================================================
                    // HOLIDAY COUNT
                    // =================================================

                    reader.NextResult();

                    int holidayCount = 0;

                    if (reader.Read())
                    {
                        holidayCount =
                            Convert.ToInt32(
                                reader["HolidayCount"]);
                    }

                    // =================================================
                    // SUNDAY COUNT
                    // =================================================

                    reader.NextResult();

                    int sundayCount = 0;

                    if (reader.Read())
                    {
                        sundayCount =
                            Convert.ToInt32(
                                reader["SundayCount"]);
                    }

                    // =================================================
                    // TOTAL HOLIDAYS
                    // =================================================

                    int totalHolidayOff =
                        holidayCount + sundayCount;

                    // =================================================
                    // FINAL ATTENDANCE
                    // =================================================

                    int totalDays =
                        DateTime.DaysInMonth(
                            yearNo,
                            monthNo);

                    row["TotalDays"] =
                        totalDays;

                    row["PaidDays"] =
                        totalPaidDays;

                    row["PresentDays"] =
                        totalPresentDays;

                    row["LeaveDays"] =
                        totalLeaveDays;

                    row["LWPDays"] =
                        totalLWPDays;

                    row["OffDays"] =
                        totalOffDays;

                    row["HolidayCount"] =
                        holidayCount;

                    row["SundayCount"] =
                        sundayCount;

                    row["TotalHolidayOff"] =
                        totalHolidayOff;

                    row["OvertimeHours"] =
                        totalOvertime;


                    // =================================================
                    // SALARY
                    // =================================================

                    row["GrossEarning"] =
                        grossEarning;

                    row["GrossDeduction"] =
                        grossDeduction;

                    // Net Salary = Gross Earning - Gross Deduction
                    row["NetSalary"] =
                        grossEarning - grossDeduction;


                    // =================================================
                    // LISTS
                    // =================================================

                    row["Earnings"] =
                        earnings;

                    row["Deductions"] =
                        deductions;


                    // =================================================
                    // SALARY IN WORDS
                    // =================================================

                    row["SalaryInWords"] =
                        NumberToWords(
                            grossEarning - grossDeduction)
                        + " Rupees Only";


                    // =================================================
                    // ATTENDANCE DETAILS
                    // =================================================

                    decimal finalPresentDays =
                        totalPresentDays
                        -
                        (
                            totalLeaveDays
                            +
                            totalHolidayOff
                        );

                    row["AttendanceDetails"] =
                        finalPresentDays.ToString("0.##")
                        + " (Present) + "
                        + totalLeaveDays.ToString("0.##")
                        + " (Leave) + "
                        + totalHolidayOff.ToString("0.##")
                        + " (Holiday/Off)";


                    // =================================================
                    // ADD EMPLOYEE DATA
                    // =================================================

                    employeeDetails.Add(row);
                }
            }
        }

        return employeeDetails;
    }


    private static object GetValue(
    Dictionary<string, object> row,
    string key)
    {
        if (row.ContainsKey(key))
            return row[key];

        return null;
    }


    private static decimal ToDecimal(object value)
    {
        if (value == null || value == DBNull.Value)
            return 0;

        decimal result;

        decimal.TryParse(
            Convert.ToString(value),
            out result);

        return result;
    }


    private static string GetDateString(
        Dictionary<string, object> row,
        string key)
    {
        object value = GetValue(row, key);

        if (value == null ||
            value == DBNull.Value ||
            string.IsNullOrEmpty(Convert.ToString(value)))
        {
            return "";
        }

        DateTime date;

        if (DateTime.TryParse(
            Convert.ToString(value),
            out date))
        {
            return date.ToString("dd/MM/yyyy");
        }

        return Convert.ToString(value);
    }
    private static string NumberToWords(decimal amount)
    {
        long number = Convert.ToInt64(
            Math.Floor(amount));

        if (number == 0)
            return "Zero";

        return NumberToWordsIndian(number);
    }


    private static string NumberToWordsIndian(long number)
    {
        if (number == 0)
            return "";

        string[] ones =
        {
        "", "One", "Two", "Three", "Four",
        "Five", "Six", "Seven", "Eight", "Nine",
        "Ten", "Eleven", "Twelve", "Thirteen",
        "Fourteen", "Fifteen", "Sixteen",
        "Seventeen", "Eighteen", "Nineteen"
    };

        string[] tens =
        {
        "", "", "Twenty", "Thirty", "Forty",
        "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
    };

        if (number < 20)
            return ones[number];

        if (number < 100)
            return tens[number / 10] +
                   (number % 10 != 0
                       ? " " + ones[number % 10]
                       : "");

        if (number < 1000)
            return ones[number / 100] +
                   " Hundred " +
                   (number % 100 != 0
                       ? NumberToWordsIndian(number % 100)
                       : "");

        if (number < 100000)
            return NumberToWordsIndian(number / 1000)
                   + " Thousand "
                   + NumberToWordsIndian(number % 1000);

        if (number < 10000000)
            return NumberToWordsIndian(number / 100000)
                   + " Lakh "
                   + NumberToWordsIndian(number % 100000);

        return NumberToWordsIndian(number / 10000000)
               + " Crore "
               + NumberToWordsIndian(number % 10000000);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetEmployeeDetailList(string employeeNo)
    {
        List<Dictionary<string, object>> employeeDetails = new List<Dictionary<string, object>>();

        string connStr = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();
        using (SqlConnection con = new SqlConnection(connStr))
        {
            string query = @"SELECT [ID]
                               ,[EmployeeNo]
                               ,[EmployeeName]
                               ,[DepartmentName]
                               ,[DepartmentCode]
                               ,[DesignationName]
                               ,[DesignationCode]
                               ,[FromMonth]
                               ,[ToMonth]
                               ,[HODCode]
                               ,[HODStatus]
                               ,[HODRemark]
                               ,[HRCode]
                               ,[HRStatus]
                               ,[HRRemark]
                               ,[Status]
                               ,[CreatedBy]
                               ,[CreatedAt]
                               ,[UpdatedBy]
                               ,[UpdatedAt]
                        FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$EmployeePaySlipInfo]
                        WHERE EmployeeNo = @EmployeeNo";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@EmployeeNo", employeeNo);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    }
                    employeeDetails.Add(row);
                }
            }
        }

        if (employeeDetails.Count == 0)
        {
            return null; // Frontend will handle "no data" case
        }

        return employeeDetails;
    }

    public void bindPaySlipRequestList()
    {
        string query = "SELECT [EmployeeNo],[EmployeeName],[DepartmentName],[DepartmentCode],[DesignationName],[DesignationCode],[FromMonth],[ToMonth],[HODCode],[HODStatus],[HODRemark],[HRCode],[HRStatus],[HRRemark],[Status],[CreatedBy],[CreatedAt],[UpdatedBy],[UpdatedAt] FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$EmployeePaySlipInfo] WHERE EmployeeNo = @UserId";

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
                // HOD Status
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

                // HR Status
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

                // Overall Status
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
        string fromMonthStr = hfFromMonth.Value; // Expect "yyyy-MM"
        string toMonthStr = hfToMonth.Value;

        DateTime fromDate, toDate;

        bool isFromValid = DateTime.TryParseExact(fromMonthStr + "-01", "yyyy-MM-dd",
                             CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);

        bool isToValid = DateTime.TryParseExact(toMonthStr + "-01", "yyyy-MM-dd",
                           CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate);

        if (!isFromValid || !isToValid)
        {
            // Handle invalid input (show error message)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                "alert('Please select valid From and To months');", true);
            return;
        }


        try
        {
            using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
            {
                con1.Open();

                // Check if the Year and Month already exist for this employee
                string checkQuery = @"
                SELECT COUNT(*) FROM [TMU$EmployeePaySlipInfo]
                WHERE EmployeeNo = @EmployeeNo AND [FromMonth] = @FromMonth AND ToMonth = @ToMonth";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, con1))
                {
                    checkCmd.Parameters.AddWithValue("@EmployeeNo", Session["uid"].ToString());
                    checkCmd.Parameters.AddWithValue("@FromMonth", fromDate);
                    checkCmd.Parameters.AddWithValue("@ToMonth", toDate);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // Already exists - show alert and stop further processing
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Data for this Months already submitted.');", true);
                        return;
                    }
                }

                // If not exists, insert the new record
                string insertQuery = @"
                INSERT INTO [TMU$EmployeePaySlipInfo] 
                (EmployeeNo, [EmployeeName], [DepartmentName], DepartmentCode, DesignationName, DesignationCode,FromMonth,ToMonth,HODCode,HODStatus,HRCode,HRStatus, Status, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
                VALUES
                (@EmployeeNo, @EmployeeName, @DepartmentName, @DepartmentCode, @DesignationName, @DesignationCode,@FromMonth,@ToMonth,@HODCode,@HODStatus,@HRCode,@HRStatus, @Status, @CreatedBy, @CreatedAt, @UpdatedBy, @UpdatedAt)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con1))
                {
                    cmd.Parameters.AddWithValue("@EmployeeNo", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
                    cmd.Parameters.AddWithValue("@DepartmentName", txtDepartment.Text);
                    cmd.Parameters.AddWithValue("@DepartmentCode", txtDepartmentCode.Value);
                    cmd.Parameters.AddWithValue("@DesignationName", txtDesignation.Text);
                    cmd.Parameters.AddWithValue("@DesignationCode", txtDesignationCode.Value);
                    cmd.Parameters.AddWithValue("@FromMonth", fromDate);
                    cmd.Parameters.AddWithValue("@ToMonth", toDate);
                    cmd.Parameters.AddWithValue("@HODCode", txtHODCode.Value);
                    cmd.Parameters.AddWithValue("@HODStatus", 0);
                    cmd.Parameters.AddWithValue("@HRCode", txtHRCode.Value);
                    cmd.Parameters.AddWithValue("@HRStatus", 0);
                    cmd.Parameters.AddWithValue("@Status", 0);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedBy", Session["uid"].ToString());
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Successful'); document.location.href='EmployeePaySlip.aspx';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Failed: No rows affected'); document.location.href='EmployeePaySlip.aspx';", true);
                    }
                }

                con1.Close();
            }
        }
        catch (SqlException sqlEx)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Failed: No rows affected'); document.location.href='EmployeePaySlip.aspx';", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Insert Failed: No rows affected'); document.location.href='EmployeePaySlip.aspx';", true);
        }
    }
    public void BindDate(string FacultyCode)
    {
        try
        {
            DL.FacultyPortalDL FDL = new DL.FacultyPortalDL();
            DataTable dt = new DataTable();
            dt = FDL.GetFacultyDetails(FacultyCode);
            if (dt.Rows.Count > 0)
            {
                txtDepartmentCode.Value = dt.Rows[0]["Global Dimension 2 Code"].ToString();
                txtDepartment.Text = dt.Rows[0]["Department Name"].ToString();
                txtDesignationCode.Value = dt.Rows[0]["Designation Code"].ToString();
                txtDesignation.Text = dt.Rows[0]["Job Title_Grade Desc"].ToString();
                txtHODCode.Value = dt.Rows[0]["HOD"].ToString();
                txtHRCode.Value = "TMU05721";// dt.Rows[0]["HR"].ToString();
            }
            else
            {
                // Blank();
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void getPaySlipRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlMonth = (DropDownList)e.Row.FindControl("ddlMonth");

            if (ddlMonth != null)
            {
                DateTime fromMonth;
                DateTime toMonth;

                object fromValue = DataBinder.Eval(e.Row.DataItem, "FromMonth");
                object toValue = DataBinder.Eval(e.Row.DataItem, "ToMonth");

                if (fromValue != null &&
                    toValue != null &&
                    DateTime.TryParse(fromValue.ToString(), out fromMonth) &&
                    DateTime.TryParse(toValue.ToString(), out toMonth))
                {
                    ddlMonth.Items.Clear();

                    DateTime currentMonth = new DateTime(
                        fromMonth.Year,
                        fromMonth.Month,
                        1);

                    DateTime endMonth = new DateTime(
                        toMonth.Year,
                        toMonth.Month,
                        1);

                    while (currentMonth <= endMonth)
                    {
                        ddlMonth.Items.Add(
                            new ListItem(
                                currentMonth.ToString("MMMM yyyy"),
                                currentMonth.ToString("yyyy-MM")
                            )
                        );

                        currentMonth = currentMonth.AddMonths(1);
                    }
                }
            }
        }
    }

}


