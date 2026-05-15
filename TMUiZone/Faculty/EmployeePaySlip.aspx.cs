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
                txtEmployeeNo.Text=Session["uid"].ToString();
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

}


