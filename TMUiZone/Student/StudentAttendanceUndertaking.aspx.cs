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

public partial class Student_StudentAttendanceUndertaking : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Date1.Text = DateTime.Now.ToString("yyyy-MM-dd");
                bindGrid();
                BindDate();
            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    public string GetAcademicYearNo()
    {
        string noValue = null;

        try
        {
            using (SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        // return the first "No_" from the result
                        noValue = dt.Rows[0]["No_"].ToString();
                    }
                }
            }
        }
        catch
        {
            // optionally log the error
            noValue = null;
        }

        return noValue;
    }


    public void bindGrid()
    {

        string val = GetAcademicYearNo().ToString();
        string val1 = "";
        SqlCommand cmd = new SqlCommand("StudentAttendanceNew '" + Session["uid"].ToString() + "','" + val + "','" + val1 + "','" + val1 + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdAttendanceReport.DataSource = dt;
            grdAttendanceReport.DataBind();            
        }
        else
        {
            grdAttendanceReport.DataSource = "";
            grdAttendanceReport.DataBind();
        }

    }
    public void BindDate()
    {
        string query = @"
SELECT TOP 1 
    [College],
	[Department],
	[Course Name],
	[Semester],
    [No_], 
    [Student Name], 
	[E-Mail Address],
    [Global Dimension 1 Code], 
    [Global Dimension 2 Code], 
    [Mobile Number], 
    [Phone Number], 
    [Fathers Name],
    CASE 
        WHEN LTRIM(RTRIM([Father Mobile No])) = '' OR [Father Mobile No] IS NULL 
            THEN [Father’s_Guardian Mobile No_] 
        ELSE [Father Mobile No]  
    END AS [FatherMobileNo], 
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

                // Populate controls with data
                studentName.Text = row["Student Name"].ToString();
                studentName1.InnerText = row["Student Name"].ToString();
                fatherName1.InnerText = row["Fathers Name"].ToString();
                program.Text = row["Course Name"].ToString();
                branch.Text = row["Global Dimension 1 Code"].ToString();
                semester.Text = row["Semester"].ToString();

                studentMobile.Text = row["Mobile Number"].ToString();
                studentEmail.Text = row["E-Mail Address"].ToString();

                fatherEmail.Text = row["FatherMobileNo"].ToString();
                fatherMobile.Text = row["FatherMobileNo"].ToString();
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
        try
        {
            using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
            {
                con1.Open();

                // Step 1: Generate GatePassNo based on current date and a unique number (e.g., incremented counter)
                string gatePassNo = "";
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
                    cmd.Parameters.AddWithValue("@WardenCode", "TMU03762");
                    cmd.Parameters.AddWithValue("@WardenStatus", 0);
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Gate Pass Inserted Successfully'); document.location.href='StudentGatePass.aspx';", true);
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

}