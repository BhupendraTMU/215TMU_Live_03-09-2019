using AjaxControlToolkit;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Faculty_StudentInOutAproveGate : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["uid"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                FromDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); 
                ToDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); 


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
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void btnGatePassNo_Click(object sender, EventArgs e)
    {
        string scannedCode = txtGatePassNo.Text;
        bindGatePassRequestList1(scannedCode);
    }
    public void bindGatePassRequestList()
    {

        string query = "SELECT case when [WardenStatus]=0 then 'Pending'  when [WardenStatus]=1 then 'Approve' when [WardenStatus]=2 then 'Reject'else 'Nothing' end WardenStatusText,'' CWardenStatusText,case when [InTime]=0 then 'Pending'  when [InTime]=1 then 'In' when [InTime]=2 then 'Reject' else 'Nothing' end  InTimeText,case when [OutTime]=0 then 'Pending'  when [OutTime]=1 then 'Out' when [OutTime]=2 then 'Reject' else 'Nothing' end OutTimeText,a1.ID, a1.[No_], [StudentName], [AcadmicYear], a1.[GatePassNo], [Class], [College], [Hostel], [RoomNo], [FormDate], [ToDate], [NoOfHours], [LunchStatus], [Reason], [WardenCode], [WardenStatus], [WardenRemark], [CWardenCode], [CWardenStatus], [CWardenRemark], a1.[GateMenCode], a1.OutTime, MAX(CASE WHEN a2.InOutStatus = 'out' THEN a2.DayDate END) AS OutTime1, a1.InTime, MAX(CASE WHEN a2.InOutStatus = 'in'  THEN a2.DayDate END) AS InTime1,  [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt] FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInfo] a1 LEFT JOIN [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInOutInfo] a2 ON a1.GatePassNo = a2.GatePassNo WHERE (WardenStatus = 1 OR CWardenStatus = 1) GROUP BY a1.ID,a1.[No_],[StudentName],[AcadmicYear],a1.[GatePassNo],[Class],[College], [Hostel],[RoomNo],[FormDate],[ToDate],[NoOfHours],[LunchStatus],[Reason],a1.OutTime,a1.InTime, [WardenCode],[WardenStatus],[WardenRemark],[CWardenCode],[CWardenStatus],[CWardenRemark], a1.[GateMenCode],[CreatedBy],[CreatedAt],[UpdatedBy],[UpdatedAt] order by CreatedAt desc;";

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;
        //cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());

        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter daCL = new SqlDataAdapter(cmd);
            System.Data.DataTable dtCL = new System.Data.DataTable();
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
    public void bindGatePassRequestList1(string gatPass)
    {
        string query = "SELECT case when [WardenStatus]=0 then 'Pending'  when [WardenStatus]=1 then 'Approve' when [WardenStatus]=2 then 'Reject'else 'Nothing' end WardenStatusText,'' CWardenStatusText,case when [InTime]=0 then 'Pending'  when [InTime]=1 then 'In' when [InTime]=2 then 'Reject' else 'Nothing' end  InTimeText,case when [OutTime]=0 then 'Pending'  when [OutTime]=1 then 'Out' when [OutTime]=2 then 'Reject' else 'Nothing' end OutTimeText,a1.ID, a1.[No_], [StudentName], [AcadmicYear], a1.[GatePassNo], [Class], [College], [Hostel], [RoomNo], [FormDate], [ToDate], [NoOfHours], [LunchStatus], [Reason], [WardenCode], [WardenStatus], [WardenRemark], [CWardenCode], [CWardenStatus], [CWardenRemark], a1.[GateMenCode], a1.OutTime, MAX(CASE WHEN a2.InOutStatus = 'out' THEN a2.DayDate END) AS OutTime1, a1.InTime, MAX(CASE WHEN a2.InOutStatus = 'in'  THEN a2.DayDate END) AS InTime1,  [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt] FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInfo] a1 LEFT JOIN [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInOutInfo] a2 ON a1.GatePassNo = a2.GatePassNo WHERE (WardenStatus = 1 OR CWardenStatus = 1)  AND a1.GatePassNo LIKE '%' + @GatePassNo + '%' GROUP BY a1.ID,a1.[No_],[StudentName],[AcadmicYear],a1.[GatePassNo],[Class],[College], [Hostel],[RoomNo],[FormDate],[ToDate],[NoOfHours],[LunchStatus],[Reason],a1.OutTime,a1.InTime, [WardenCode],[WardenStatus],[WardenRemark],[CWardenCode],[CWardenStatus],[CWardenRemark], a1.[GateMenCode],[CreatedBy],[CreatedAt],[UpdatedBy],[UpdatedAt] order by CreatedAt desc;";

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@GatePassNo", gatPass);

        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataAdapter daCL = new SqlDataAdapter(cmd);
            System.Data.DataTable dtCL = new System.Data.DataTable();
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

    [WebMethod]
    public static object GetStudentDetailByGatePassNo(string gatePassNo)
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = @"
            SELECT [ID]
                  ,[Student Image]
                  ,gp.[No_]
                  ,[StudentName]
                  ,[AcadmicYear]
                  ,[GatePassNo]
                  ,[Class]
                  ,gp.[College]
                  ,[Hostel]
                  ,[RoomNo]
                  ,[FormDate]
                  ,[ToDate]
                  ,[NoOfHours]
                  ,[LunchStatus]
                  ,[Reason]
                  ,[WardenCode]
                  ,[WardenStatus]
                  ,[WardenRemark]
                  ,[CWardenCode]
                  ,[CWardenStatus]
                  ,[CWardenRemark]
                  ,[GateMenCode]
                  ,[OutTime]
                  ,[InTime]
                  ,[CreatedBy]
                  ,[CreatedAt]
                  ,[UpdatedBy]
                  ,[UpdatedAt]
            FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInfo] gp 
            INNER JOIN [EDUCOLLEGELIVE-R2].[dbo].[TMU$Student - COLLEGE] s 
                ON s.No_ = gp.No_
          
       WHERE   GatePassNo = @gatPass";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@gatPass", gatePassNo);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Student image ko Base64 me convert
                        string base64Image = null;
                        if (reader["Student Image"] != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])reader["Student Image"];
                            base64Image = Convert.ToBase64String(imageBytes);
                        }

                        return new
                        {
                            StudentImage = base64Image, // Base64 string
                            No_ = reader["No_"].ToString(),
                            StudentName = reader["StudentName"].ToString(),
                            AcadmicYear = reader["AcadmicYear"].ToString(),
                            GatePassNo = reader["GatePassNo"].ToString(),
                            Class = reader["Class"].ToString(),
                            College = reader["College"].ToString(),
                            Hostel = reader["Hostel"].ToString(),
                            RoomNo = reader["RoomNo"].ToString(),
                            FormDate = reader["FormDate"].ToString(),
                            ToDate = reader["ToDate"].ToString(),
                            NoOfHours = reader["NoOfHours"].ToString(),
                            Reason = reader["Reason"].ToString(),
                            OutTime = reader["OutTime"].ToString(),
                            InTime = reader["InTime"].ToString(),
                            WardenStatus = reader["WardenStatus"].ToString(),
                            WardenRemark = reader["WardenRemark"].ToString(),
                            CreatedBy = reader["CreatedBy"].ToString(),
                            CreatedAt = reader["CreatedAt"].ToString(),
                            UpdatedBy = reader["UpdatedBy"].ToString(),
                            UpdatedAt = reader["UpdatedAt"].ToString()
                        };
                    }
                }
            }
        }
        return null;
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {
        string studentNo = hfstudentNo.Value;
        string gatePassNo = hfgatePassNo.Value;
        string gateMenCode = Session["uid"].ToString();
        TimeSpan outTime = DateTime.Now.TimeOfDay;
        DateTime dayDate = DateTime.Now;
        
        string createdBy = Session["uid"].ToString() ?? "SYSTEM";
        using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertGatePassOut", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameters
            cmd.Parameters.AddWithValue("@No_", studentNo);
            cmd.Parameters.AddWithValue("@GatePassNo", gatePassNo);
            cmd.Parameters.AddWithValue("@GateMenCode", gateMenCode);
            cmd.Parameters.AddWithValue("@OutTime", outTime);
            cmd.Parameters.AddWithValue("@DayDate", dayDate);
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                txtGatePassNo.Text = "";
                GetStudentByGatePassNo(gatePassNo);
                //string MobileNo = "8077625096";
                //string Msg = "This is to inform you that your ward, " + txtStudentName.Text + ", has been granted permission to going outside the hostel on " + dayDate + ". Out Time - " + outTime + " In Time - " + returntime + " - TEERTHANKER MAHAVEER UNIVERSITY";

                //SMS(MobileNo, Msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Checked out successfully.');document.location.href='StudentInOutAproveGate.aspx';", true);
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to record Out entry. Please try again.');document.location.href='StudentInOutAproveGate.aspx';", true);
            }
        }

    }


    protected void btnIn_Click(object sender, EventArgs e)
    {
        string OutTimeCheck = hfOutTime.Value;
        string gatePassNo = hfgatePassNo.Value;

        if (OutTimeCheck != "1")
        {
            // Escape quotes properly and inject the value of gatePassNo
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "Key",
                "alert('“Out” must happen first, then “In”.'); callMyMethod('" + gatePassNo + "');",
                true
            );
            return;
        }
        string studentNo = hfstudentNo.Value;
        string gateMenCode = Session["uid"].ToString();
        TimeSpan outTime = DateTime.Now.TimeOfDay;
        DateTime dayDate = DateTime.Now;
        string createdBy = Session["uid"].ToString() ?? "SYSTEM";
        using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertGatePassIn", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameters
            cmd.Parameters.AddWithValue("@No_", studentNo);
            cmd.Parameters.AddWithValue("@GatePassNo", gatePassNo);
            cmd.Parameters.AddWithValue("@GateMenCode", gateMenCode);
            cmd.Parameters.AddWithValue("@OutTime", outTime);
            cmd.Parameters.AddWithValue("@DayDate", dayDate);
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                txtGatePassNo.Text = "";
                GetStudentByGatePassNo1(gatePassNo);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Checked in successfully.');document.location.href='StudentInOutAproveGate.aspx';", true);
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Unable to record In entry. Please try again.');", true);
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
            string query = @"
SELECT 
    a1.ID, 
    a1.[No_], 
    [StudentName], 
    [AcadmicYear], 
    a1.[GatePassNo], 
    [Class], 
    [College], 
    [Hostel], 
    [RoomNo], 
    [FormDate], 
    [ToDate], 
    [NoOfHours], 
    [LunchStatus], 
    [Reason], 
    [WardenCode], 
    [WardenStatus], 
    [WardenRemark], 
    [CWardenCode], 
    [CWardenStatus], 
    [CWardenRemark], 
    a1.[GateMenCode], 
    a1.OutTime,
    MAX(CASE WHEN a2.InOutStatus = 'out' THEN a2.DayDate END) AS OutTime1,
    a1.InTime,
    MAX(CASE WHEN a2.InOutStatus = 'in'  THEN a2.DayDate END) AS InTime1,
    MobileNo,
	FatherMobileNo,
	FullAddress,
    [CreatedBy], 
    [CreatedAt], 
    [UpdatedBy], 
    [UpdatedAt]
FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInfo] a1
LEFT JOIN [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInOutInfo] a2 
       ON a1.GatePassNo = a2.GatePassNo
WHERE a1.[ID] = @StudentNo
GROUP BY 
    a1.ID,
    a1.[No_],
    [StudentName],
    [AcadmicYear],
    a1.[GatePassNo],
    [Class],
    [College],
    [Hostel],
    [RoomNo],
    [FormDate],
    [ToDate],
    [NoOfHours],
    [LunchStatus],
    [Reason],
    a1.OutTime,
    a1.InTime,
    [WardenCode],
    [WardenStatus],
    [WardenRemark],
    [CWardenCode],
    [CWardenStatus],
    [CWardenRemark],
    a1.[GateMenCode],
    MobileNo,
	FatherMobileNo,
	FullAddress,
    [CreatedBy],
    [CreatedAt],
    [UpdatedBy],
    [UpdatedAt];";

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

                            OutTime1 = Reader["OutTime1"] != DBNull.Value
                                          ? Convert.ToDateTime(Reader["OutTime1"]).ToString("dd-MM-yyyy hh:mm tt")
                                          : "",
                            InTime1 = Reader["InTime1"] != DBNull.Value
                                          ? Convert.ToDateTime(Reader["InTime1"]).ToString("dd-MM-yyyy hh:mm tt")
                                          : "",

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
    protected void getGatePassRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string inTime = DataBinder.Eval(e.Row.DataItem, "InTime").ToString();
            string outTime = DataBinder.Eval(e.Row.DataItem, "OutTime").ToString();

            string ToDate = DataBinder.Eval(e.Row.DataItem, "ToDate").ToString(); // "17-11-2025 17:35:00"
            DateTime toDate1;

            string InTime1= DataBinder.Eval(e.Row.DataItem, "InTime1").ToString();
            DateTime InTime11;


            // Convert string → datetime
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
                out  toDate1
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
        }
    }

    public void sendmail()
    {
        SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        using (MailMessage mm = new MailMessage("sanjayj.erp@tmu.ac.in", "bhupendras.erp@tmu.ac.in"))
        {           
            mm.Subject = "TEST MAIL - Gate Pass Status: OUT";

            mm.Body = "<html><body style='font-family: Verdana, sans-serif;'>";
            mm.Body += "<h3>Gate Pass Test Mail</h3>";
            mm.Body += "<p><b>Enrolment No:</b> " + Session["uid"].ToString() + "</p>";
            mm.Body += "<p><b>Student Name:</b> " + Session["Name"] + "</p>";
            mm.Body += "<br/>";
            mm.Body += "<p>**** This is a test mail, please ignore ****</p>";
            mm.Body += "</body></html>";

            mm.IsBodyHtml = true;
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = smtpSection.Network.Host;
                smtp.EnableSsl = smtpSection.Network.EnableSsl;
                NetworkCredential networkCred = new NetworkCredential("naverp@tmu.ac.in", "nwar yzam bcez rqop");
                smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                smtp.Credentials = networkCred;
                smtp.Port = smtpSection.Network.Port;
                smtp.Send(mm);
             //   ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Email sent.');", true);
            }
        }
    }

    public void GetStudentByGatePassNo(string gatePassNo)
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = @" SELECT a1.ID, a1.[No_], [StudentName], [AcadmicYear], a1.[GatePassNo], [Class], [College], [Hostel], [RoomNo], [FormDate], [ToDate], [NoOfHours], [LunchStatus], [Reason], [WardenCode], [WardenStatus], [WardenRemark], [CWardenCode], [CWardenStatus], [CWardenRemark], a1.[GateMenCode], a1.OutTime, MAX(CASE WHEN a2.InOutStatus = 'out' THEN a2.DayDate END) AS OutTime1, a1.InTime, MAX(CASE WHEN a2.InOutStatus = 'in' THEN a2.DayDate END) AS InTime1, MobileNo, FatherMobileNo, FullAddress, [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt] FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInfo] a1 LEFT JOIN [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInOutInfo] a2 ON a1.GatePassNo = a2.GatePassNo WHERE a1.GatePassNo = @gatPass GROUP BY a1.ID, a1.[No_], [StudentName], [AcadmicYear], a1.[GatePassNo], [Class], [College], [Hostel], [RoomNo], [FormDate], [ToDate], [NoOfHours], [LunchStatus], [Reason], a1.OutTime, a1.InTime, [WardenCode], [WardenStatus], [WardenRemark], [CWardenCode], [CWardenStatus], [CWardenRemark], a1.[GateMenCode], MobileNo, FatherMobileNo, FullAddress, [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@gatPass", gatePassNo);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string no = reader["No_"].ToString();
                        string studentName = reader["StudentName"].ToString();
                        string gatePass = reader["GatePassNo"].ToString();
                        string formDate = reader["FormDate"].ToString();
                        string formDate1 = reader["OutTime1"].ToString(); //Go Time
                        
                        string toDate = reader["ToDate"].ToString();
                        string toDate1 = reader["InTime1"].ToString(); // In Time                        
                        string MobileNo = reader["FatherMobileNo"].ToString();
                        string noOfHours = reader["NoOfHours"].ToString();
                        string reason = reader["Reason"].ToString();
                        string outTime = reader["OutTime"].ToString();
                        string inTime = reader["InTime"].ToString();
                        DateTime dt = DateTime.Parse(formDate);
                        string fromTime = dt.ToString("hh:mm tt");
                        DateTime dt1 = DateTime.Parse(toDate);
                        string ToTime = dt1.ToString("hh:mm tt");                      
                      
                        string Msg = "This is to inform you that your ward, " + studentName + ", has been granted permission to going outside the hostel on " + formDate + ". Out Time - " + fromTime + " In Time - " + ToTime + " - TEERTHANKER MAHAVEER UNIVERSITY";

                        SMS(MobileNo, Msg);


                        
                    }
                }
            }
        }
    }
    public void GetStudentByGatePassNo1(string gatePassNo)
    {
        string constr = ConfigurationManager.ConnectionStrings["TMUCON"].ToString();

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = @" SELECT a1.ID, a1.[No_], [StudentName], [AcadmicYear], a1.[GatePassNo], [Class], [College], [Hostel], [RoomNo], [FormDate], [ToDate], [NoOfHours], [LunchStatus], [Reason], [WardenCode], [WardenStatus], [WardenRemark], [CWardenCode], [CWardenStatus], [CWardenRemark], a1.[GateMenCode], a1.OutTime, MAX(CASE WHEN a2.InOutStatus = 'out' THEN a2.DayDate END) AS OutTime1, a1.InTime, MAX(CASE WHEN a2.InOutStatus = 'in' THEN a2.DayDate END) AS InTime1, MobileNo, FatherMobileNo, FullAddress, [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt] FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInfo] a1 LEFT JOIN [EDUCOLLEGELIVE-R2].[dbo].[TMU$StudentGatePassInOutInfo] a2 ON a1.GatePassNo = a2.GatePassNo WHERE a1.GatePassNo = @gatPass GROUP BY a1.ID, a1.[No_], [StudentName], [AcadmicYear], a1.[GatePassNo], [Class], [College], [Hostel], [RoomNo], [FormDate], [ToDate], [NoOfHours], [LunchStatus], [Reason], a1.OutTime, a1.InTime, [WardenCode], [WardenStatus], [WardenRemark], [CWardenCode], [CWardenStatus], [CWardenRemark], a1.[GateMenCode], MobileNo, FatherMobileNo, FullAddress, [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@gatPass", gatePassNo);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string no = reader["No_"].ToString();
                        string studentName = reader["StudentName"].ToString();
                        string gatePass = reader["GatePassNo"].ToString();
                        string formDate = reader["FormDate"].ToString();
                        string formDate1 = reader["OutTime1"].ToString(); //Go Time
                        string CurrentTime = DateTime.Now.ToString("hh:mm tt");
                        string toDate = reader["ToDate"].ToString();
                        string toDate1 = reader["InTime1"].ToString(); // In Time                        
                        string MobileNo = reader["FatherMobileNo"].ToString();
                        string noOfHours = reader["NoOfHours"].ToString();
                        string reason = reader["Reason"].ToString();
                        string outTime = reader["OutTime"].ToString();
                        string inTime = reader["InTime"].ToString();
                        DateTime dt = DateTime.Parse(formDate);
                        string fromTime = dt.ToString("hh:mm tt");
                        DateTime dt1 = DateTime.Parse(toDate);
                        string ToTime = dt1.ToString("hh:mm tt");

                        string Msg = "This is to inform you that your ward, "+ studentName + ", has returned to the hostel on "+ formDate + ". In Time - "+ CurrentTime + " - TEERTHANKER MAHAVEER UNIVERSITY";

                        SMS(MobileNo, Msg);



                    }
                }
            }
        }
    }
    public void sendmail1(string StudentName, string GatePassNo, string FormDate, string FormDate1, string ToDate, string ToDate1, string NoOfHours, string OutTime, string InTime)
    {
        SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

        using (MailMessage mm = new MailMessage("sanjayj.erp@tmu.ac.in", "bhupendras.erp@tmu.ac.in"))
        {
            mm.Subject = "Gate Pass Notification";

            string enrolNo = (Session["uid"] ?? "0000").ToString();

            // Parse planned & actual times
            DateTime plannedOut = DateTime.Parse(FormDate);
            DateTime actualOut = DateTime.Parse(FormDate1);

            DateTime plannedReturn = DateTime.Parse(ToDate);
            DateTime? actualReturn = string.IsNullOrEmpty(ToDate1) ? (DateTime?)null : DateTime.Parse(ToDate1);

            bool hasReturned = actualReturn.HasValue;
            TimeSpan lateness = TimeSpan.Zero;
            if (hasReturned && actualReturn.Value > plannedReturn)
                lateness = actualReturn.Value - plannedReturn;

            // Build HTML body using string.Format (C# 5 compatible)
            string body = "<html><body style='font-family: Verdana, sans-serif; line-height:1.4;'>";
            body += "<h3>Gate Pass Notification</h3>";
            body += "<table style='border-collapse:collapse; width:100%; max-width:600px;'>";
            body += string.Format("<tr><td style='padding:6px; font-weight:bold;'>Student Name:</td><td style='padding:6px;'>{0}</td></tr>", StudentName);
            body += string.Format("<tr><td style='padding:6px; font-weight:bold;'>Enrolment No:</td><td style='padding:6px;'>{0}</td></tr>", enrolNo);
            body += string.Format("<tr><td style='padding:6px; font-weight:bold;'>Out Time:</td><td style='padding:6px;'>{0}</td></tr>", actualOut.ToString("dd-MMM-yyyy HH:mm"));
            body += string.Format("<tr><td style='padding:6px; font-weight:bold;'>Expected Return:</td><td style='padding:6px;'>{0}</td></tr>", plannedReturn.ToString("dd-MMM-yyyy HH:mm"));

            if (hasReturned)
            {
                body += string.Format("<tr><td style='padding:6px; font-weight:bold;'>In Time:</td><td style='padding:6px;'>{0}</td></tr>", actualReturn.Value.ToString("dd-MMM-yyyy HH:mm"));
                if (lateness > TimeSpan.Zero)
                {
                    body += string.Format("<tr><td style='padding:6px; font-weight:bold; color:red;'>Late By:</td><td style='padding:6px; color:red;'>{0}</td></tr>", FormatTimeSpan(lateness));
                }
                else
                {
                    body += "<tr><td style='padding:6px; font-weight:bold;'>Status:</td><td style='padding:6px;'>Returned on time</td></tr>";
                }
            }
            else
            {
                body += "<tr><td style='padding:6px; font-weight:bold;'>Status:</td><td style='padding:6px;'>Out — not returned yet</td></tr>";
            }

            body += "</table>";
            body += "<br/><p>**** This is an automated test mail. Please do not reply ****</p>";
            body += "</body></html>";

            mm.Body = body;
            mm.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = smtpSection.Network.Host;
                smtp.EnableSsl = smtpSection.Network.EnableSsl;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("naverp@tmu.ac.in", "nwar yzam bcez rqop"); // actual SMTP credentials
                smtp.Port = smtpSection.Network.Port;
                smtp.Send(mm);

            }
        }
    }

    // Helper method for late time formatting
    private string FormatTimeSpan(TimeSpan ts)
    {
        if (ts.TotalMinutes < 1) return ts.Seconds + " sec";
        if (ts.TotalHours < 1) return ts.Minutes + " min " + ts.Seconds + " sec";
        return ((int)ts.TotalHours) + " hr " + ts.Minutes + " min";
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
            '' AS CWardenStatusText,
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
        WHERE (WardenStatus = 1 OR CWardenStatus = 1)
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
        if (fromDate.HasValue)
        {
            if (!toDate.HasValue) {
                baseQuery += " AND  CONVERT(date, a1.FormDate) =  CONVERT(date, @fromDate)  ";
            }
            else
            {
                baseQuery += " AND  CONVERT(date, a1.FormDate) >=  CONVERT(date, @fromDate)  ";
            }
                
        }
           

        if (toDate.HasValue)
            baseQuery += " AND CONVERT(date, a1.ToDate) <= CONVERT(date, @toDate) ";


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


    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered
    }
    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        // ❌ पहले Column Hide करो
        getGatePassRequestList.Columns[getGatePassRequestList.Columns.Count - 1].Visible = false;

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        // ✔ अब RenderControl करो (Blank Column नहीं आएगा)
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
    public void SMS(String MobileNo, string Msg)
    {
try
        {
        //  Website: http://www.universalsmsadvertising.com/     
        MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 9015762885;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";

        // string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=myuserid&user_password=mypassword&mobile=919834567120&sender_id=SWEET&type=F&text= " + Msg + "";



        System.Net.HttpWebRequest fr;
        Uri targetURI = new Uri(url);
        fr = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(targetURI);
        if (fr.GetResponse().ContentLength > 0)
        {
            System.IO.StreamReader str = new System.IO.StreamReader(fr.GetResponse().GetResponseStream());
            Response.Write(str.ReadToEnd());
            str.Close();
        }
 }
        catch
        {

        }
    }

}


