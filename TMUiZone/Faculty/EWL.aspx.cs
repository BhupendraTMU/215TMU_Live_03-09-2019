using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_EWL : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    Connection navconn;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", " alert('access denied ?'); document.location.href='FacultyDetails.aspx';", true);
            Portalcon = new Connection();
            con = new ServicePoratal();
            navconn = new Connection();
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

            int day = datevalue.Day;
            int mn = datevalue.Month;
            int yy = datevalue.Year;
            DateTime now = DateTime.Now;
            DateTime lastDayOfLastMonth = now.Date.AddDays(-now.Day);
            int lastMonthLastDay = lastDayOfLastMonth.Day;
            if (day < 7)
            {


                clndAppliedate.StartDate = new DateTime(yy, mn, 25);
                clndAppliedate.EndDate = new DateTime(yy, mn, day);
            }
            else
            {
                clndAppliedate.StartDate = new DateTime(yy, mn-1, 1);
                clndAppliedate.EndDate = new DateTime(yy, mn, day);

            }
            if (!IsPostBack)
            {

                lblfirstApproval.Text = Session["hod_Name_Leave1"].ToString() + "(" + Session["hod_ID_Leave1"].ToString() + ")";
                lblSecondApproval.Text = Session["hod_Name_Leave2"].ToString() + "(" + Session["hod_ID_Leave2"].ToString() + ")";
                if (Session["hod_ID_Leave1"].ToString() == "")
                {
                    lblApprovalAuthority1.Visible = true;
                    btnSendForApproval.Enabled = false;
                }

                if (Session["hod_ID_Leave1"].ToString() != "")
                {
                    lblApprovalAuthority1.Visible = false;
                    btnSendForApproval.Enabled = true;
                }
                lnkApproval.Text = "Approval";


                //con.Update_HOD(Session["hod_ID_Leave1"].ToString(), Session["hod_ID_Leave2"].ToString(), Session["uid"].ToString(), Session["Company"].ToString());
                //con.DisConnect();
            }

            VisiblilitybyHOD();



        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }

    }
    public void SMS(String MobileNo, string Msg)
    {
        //  Website: http://www.universalsmsadvertising.com/     //  Username: 9837016352   //  Password: 9837016352 
        MobileNo = "91" + MobileNo;
        // MobileNo = "91" + 9015762885;
        string url = "http://www.universalsmsadvertising.com/universalsmsapi.php?user_name=9837016352&user_password=9837016352&mobile=" + MobileNo + "&sender_id=TMUniv&type=F&text=" + Msg + "";
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
    public void SendSMSHODExam(string uname, string userid, string FromDate)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";
        // string smsdata = "Dear sir, " + userid + ", Your Faculty form for " + course + ", " + sem + "";
        string smsdata = "Dear sir your faculty  " + userid + ", CO  from " + FromDate + " to " + FromDate + " has been applied. " + "Thanks ";

        // As per Subham Gupta 29-12-2018

        SqlDataReader dr = Portalcon.Show_AthorityNo(Session["uid"].ToString(), tablenameemployeedata);
        dr.Read();
        if (dr.HasRows)
        {
            mobilnoemp = dr["MobilePhoneNo"].ToString();
            dr.Close();
            Portalcon.DisConnect();
            if (mobilnoemp.Trim() == "")
            { }
            else
            {
                try
                {
                    SMS(mobilnoemp, smsdata);
                }
                catch (Exception)
                {

                }
            }
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
        }
    }

    public void sendAproved(string FromDate, string Eid, string Status)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";

        string smsdata = "Dear Applicant your CO leave from " + FromDate + " to " + FromDate + " has been " + Status + ".";

        // As per Subham Gupta 29-12-2018

        SqlDataReader dr = Portalcon.SHow_EmployeeMobileNo(Eid, tablenameemployeedata);
        dr.Read();
        if (dr.HasRows)
        {
            mobilnoemp = dr["MobilePhoneNo"].ToString();
            dr.Close();
            Portalcon.DisConnect();
            if (mobilnoemp.Trim() == "")
            { }
            else
            {
                try
                {
                    SMS(mobilnoemp, smsdata);
                }
                catch (Exception)
                {

                }
            }
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
        }
    }


    string holidaydatef = ""; DateTime holidaydatef1; DateTime weekofMonthlydataCount1; DateTime holidayMonthlydataCount1;
    string weekofMonthlydataCount = ""; string weekofMonthlydataDate = ""; string holidayMonthlydataCount = "";
    string countholidayyy = ""; string tblPayHolidays = ""; string countweekeadcl = ""; string weekdaycode = ""; string tblEmployeeWeekShiftMaster = "";
    public void findHolidayorweekdays()
    {
        string ccname = Session["Company"].ToString(); string rccname = ccname.Replace(".", "_");
        tblPayHolidays = "[" + rccname + "$Pay Holidays" + "]";
        tblEmployeeWeekShiftMaster = "[" + rccname + "$Employee Week Shift Master" + "]";

        if (Session["ShiftPattern"].ToString() == "0")
        {

            if (Session["ShiftDay"].ToString() == "1")
            {
                weekdaycode = "Sunday";
            }
            if (Session["ShiftDay"].ToString() == "2")
            {
                weekdaycode = "Monday";
            }
            if (Session["ShiftDay"].ToString() == "3")
            {
                weekdaycode = "Tuesday";
            }
            if (Session["ShiftDay"].ToString() == "4")
            {
                weekdaycode = "Wednesday";
            }
            if (Session["ShiftDay"].ToString() == "5")
            {
                weekdaycode = "Thursday";
            }
            if (Session["ShiftDay"].ToString() == "6")
            {
                weekdaycode = "Friday";
            }
            if (Session["ShiftDay"].ToString() == "7")
            {
                weekdaycode = "Saturday";
            }
            if (Session["ShiftDay"].ToString() == "0")
            {
                countweekeadcl = "00.00";
            }

            if (Session["CompanyHolidayAllowed"].ToString() == "0")
            {

                SqlDataReader dr = navconn.Show_HolidayListPayHolidayDate(tblPayHolidays, txtFromDate.Text.Trim(), Session["GlobalDimension1Coded"].ToString());
                dr.Read();
                if (dr.HasRows)
                {
                    holidaydatef = Convert.ToDateTime(dr["Date"].ToString()).ToString("yyyy-MM-dd");
                }
                dr.Close();
                navconn.DisConnect();
            }
            try
            {
                holidaydatef1 = DateTime.ParseExact(holidaydatef.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            { }
            DateTime appleddatetxt = DateTime.ParseExact(txtFromDate.Text.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string appleddatetxt1 = appleddatetxt.DayOfWeek.ToString();
            if (appleddatetxt1 == weekdaycode || holidaydatef1 == appleddatetxt)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please fill correct date not a valid date of week off or holiday');", true);
                txtFromDate.Text = "";
            }

        }

        if (Session["ShiftPattern"].ToString() == "2")
        {
            SqlDataReader drs = navconn.Show_CountMonthlyWeekoffemployeeDate(tblEmployeeWeekShiftMaster, txtFromDate.Text.Trim(), "2", "0", Session["uid"].ToString());
            drs.Read();
            if (drs.HasRows)
            {
                weekofMonthlydataCount = Convert.ToDateTime(drs["Start Date"].ToString()).ToString("yyyy-MM-dd");
            }

            //weekofMonthlydataCount = drs["ShiftCode"].ToString();
            drs.Close();
            navconn.DisConnect();

            if (Session["CompanyHolidayAllowed"].ToString() == "0")
            {
                //SqlDataAdapter da = new SqlDataAdapter("select *  from " + tblEmployeeWeekShiftMaster + " where convert(date, [Start Date],103)  >='" + txtFromDate.Text.Trim() + "' and [Employee No_]='" + Session["uid"].ToString() + "' and [Shift Pattern]=2 and [Shift Code]=0", navconn.Con);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "tbl_EmployeepunchData");
                //for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                //{
                //    weekofMonthlydataDate = ds.Tables[0].Rows[j]["Start Date"].ToString();
                //}

                SqlDataReader dr = navconn.Show_HolidayListPayHolidayMonthlyDate(tblPayHolidays, txtFromDate.Text.Trim(), Session["GlobalDimension1Coded"].ToString());
                dr.Read();
                if (dr.HasRows)
                {
                    holidayMonthlydataCount = Convert.ToDateTime(dr["Date"].ToString()).ToString("yyyy-MM-dd");
                }

                dr.Read();
                navconn.DisConnect();


            }
            DateTime appleddatetxt = DateTime.ParseExact(txtFromDate.Text.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                weekofMonthlydataCount1 = DateTime.ParseExact(weekofMonthlydataCount, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            { }
            try
            {
                holidayMonthlydataCount1 = DateTime.ParseExact(holidayMonthlydataCount, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            { }

            if (appleddatetxt == weekofMonthlydataCount1 || appleddatetxt == holidayMonthlydataCount1)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please fill correct date not a valid date of week off or holiday');", true);
                txtFromDate.Text = "";
            }
        }
    }


    public void VisiblilitybyHOD()
    {
        SqlDataReader dr = navconn.SHow_showHODExhistCO(Session["uid"].ToString(), Session["CompanyTableEmployee"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            navconn.DisConnect();

            lnkApproval.Visible = true;
            lblCountODAppoval.Visible = true;
            ShowPendingApprovalCount();
        }
        else
        {
            dr.Close();
            navconn.DisConnect();
            lnkApproval.Visible = false;
            lblCountODAppoval.Visible = false;
        }



    }
    string workingduration = "";
    public void ToTalWorkingHour()
    {
        try
        {

            DateTime startDateTime = DateTime.ParseExact(txtFromDate.Text.Trim() + " " + txtFromTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDateTime = DateTime.ParseExact(txtFromDate.Text.Trim() + " " + txtToTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            if (endDateTime < startDateTime)
            {
                txtFromTime.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Out time should always be greater than In Time');", true);

            }

            else
            {

                TimeSpan difference = endDateTime - startDateTime;

                string differenceString = difference.ToString();
                string s = differenceString.ToString();
                workingduration = difference.TotalHours.ToString();

            }
        }
        catch (Exception)
        {


        }
    }

    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {

       if (txtFromDate.Text == "17 Mar 2023" || txtFromDate.Text == "18 Mar 2023" || txtFromDate.Text == "19 Mar 2023" || txtFromDate.Text == "20 Mar 2023")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('You can not apply CO for this date.');", true);
            return;
        }
     

        string queryOff = "select COUNT(*) as Valid from [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Daily Attendence Detail]  where [Employee Code]='" + Session["uid"].ToString() + "' and  [Attendance Date]='" + txtFromDate.Text + "'  and ([Off Day]=1 or Holiday=1)";
        SqlDataAdapter daOff = new SqlDataAdapter(queryOff, con.Con);
        DataTable dtOff = new DataTable();

        daOff.Fill(dtOff);

        if (dtOff.Rows[0]["Valid"].ToString() == "2")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('You can apply EWL on Holiday or Offday.');", true);
            return;
        }

        string query1 = "select COUNT(*) as valid from [tble_Leave_Approval] where [UserID]='" + Session["uid"].ToString() + "' and Status in ('Approved','Pending') and Convert(date,'" + txtFromDate.Text + "') between [From_Date] and To_Date";

        SqlDataAdapter da1 = new SqlDataAdapter(query1, con.Con);
        DataTable dt1 = new DataTable();

        da1.Fill(dt1);

        if (dt1.Rows[0]["valid"].ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('You can not apply CO on Leave');", true);
            return;
        }

        string query = "  select count(*) as 'Valid' from [EDUCOLLEGELIVE-R2].dbo.[TMU$Medical OT Days Calculation] where [Employee Code]='" + Session["uid"].ToString() + "' and [Date]='" + txtFromDate.Text + "'";
        SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
        DataTable dt = new DataTable();

        da.Fill(dt);
        con.DisConnect();

        if (Convert.ToInt32(dt.Rows[0]["Valid"]) > 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('You have already Applied OT for this date');", true);
            return;
        }


        //ToTalWorkingHour();
        SqlDataReader dr = con.Duplicate_tbl_attendenceforCOApplication(txtFromDate.Text.Trim(), Session["uid"].ToString(), Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            con.DisConnect();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Already Applied of the date');", true);
        }
        else
        {
            dr.Close();
            con.DisConnect();

            insert_tbl_attendenceforCOApplication(Session["uid"].ToString(), Session["uname"].ToString(), Session["Company"].ToString(), txtFromDate.Text.Trim(), txtFromTime.Text.Trim(), txtToTime.Text.Trim(), txtDestination.Text.Trim(), txtRemarks.Text.Trim(), "Present", workingduration, System.DateTime.Now.ToString("yyyy-MM-dd"), "Pending", Session["hod_Name_Leave1"].ToString(), "1", "Manual", Session["hod_Name_Leave2"].ToString(), Session["hod_ID_Leave2"].ToString(), txtPurpose.Text.Trim(), Session["hod_ID_Leave1"].ToString());
            con.DisConnect();
            lblCOSuccess.Visible = true;
            SendSMSHODExam(Session["uname"].ToString(), Session["uid"].ToString(), txtFromDate.Text.Trim());// for sms
            clear();
        }
    }
    public void insert_tbl_attendenceforCOApplication(string Userid, string Uname, string CompanyName, string Atte_Date, string fromTime, string ToTime, string Job_location, string Remarks, string Status, string Working_Duration, string CreatedDate, string ApprovalStatus, string HODName, string Co_Leave, string AttendanceType, string HOD2Name, string HOD2, string Purpose, string HODUserID)
    {
        if (con1 != null && con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        string sqlq = "insert into HRMSPortal.dbo.tbl_Co_Leave_Application (Userid,Uname,CompanyName,Atte_Date,fromTime,ToTime,Job_location,Remarks,Status,Working_Duration,CreatedDate,ApprovalStatus,HODName,Co_Leave,[Attendance Type],[HOD2 Name],HOD2,Purpose,HODUserID,FinalApprovalID,FinalApprovalStatus,LeaveType) values('" + Userid + "','" + Uname + "','" + CompanyName + "','" + Atte_Date + "','" + fromTime + "','" + ToTime + "','" + Job_location + "','" + Remarks + "','" + Status + "','" + Working_Duration + "','" + CreatedDate + "','" + ApprovalStatus + "','" + HODName + "','" + Co_Leave + "','" + AttendanceType + "','" + HOD2Name + "','" + HOD2 + "','" + Purpose + "','" + HODUserID + "','TMU03651',0,'EWL')";
        SqlCommand cmd = new SqlCommand(sqlq, con1);
        cmd.ExecuteNonQuery();

    }
    public void clear()
    {
        txtFromDate.Text = "";
        txtToTime.Text = "";
        txtFromTime.Text = "";
        txtRemarks.Text = "";
        txtPurpose.Text = "";
    }
    protected void btnGet_ViewStatus_Click(object sender, EventArgs e)
    {
        Show_Report_ofuser();
    }

    //public SqlDataReader ShowByusertbl_attendenceforCOApplicationAll(string fromdate, string todate, string Userid, string CompanyName)
    //{
    //    con1.Open();
    //    string s = "select * from HRMSPortal.dbo.tbl_Co_Leave_Application where convert(date,Atte_Date,103)>='" + fromdate + "' and convert(date,Atte_Date,103)<='" + todate + "' and Userid='" + Userid + "' and CompanyName='" + CompanyName + "' and  LeaveType='EWL'";
    //    SqlCommand cmd = new SqlCommand(s, con1);
    //    SqlDataReader dr = cmd.ExecuteReader();
    //    con1.Close();
    //    return dr;

    //}
    //public SqlDataReader ShowByusertbl_attendenceforCOApplication(string fromdate, string todate, string Userid, string CompanyName, string ApprovalStatus)
    //{
    //    con1.Open();
    //    string s = "select * from HRMSPortal.dbo.tbl_Co_Leave_Application where convert(date,Atte_Date,103)>='" + fromdate + "' and convert(date,Atte_Date,103)<='" + todate + "' and Userid='" + Userid + "' and CompanyName='" + CompanyName + "' and ApprovalStatus='" + ApprovalStatus + "' and  LeaveType='EWL'";
    //    SqlCommand cmd = new SqlCommand(s, con1);
    //    SqlDataReader dr = cmd.ExecuteReader();
    //    return dr;
    //    con1.Close();
    //}

    public void Show_Report_ofuser()
    {
        if (ddStatus_ViewStatus.SelectedValue.Trim() == "All")
        {



            con1.Open();
            string query = "select * from HRMSPortal.dbo.tbl_Co_Leave_Application where convert(date,Atte_Date,103)>='" + txtFromDate_ViewStatus.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + txtTodate_ViewStatus.Text.Trim() + "' and Userid='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and  LeaveType='EWL'";
           

            SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grdView_Status.DataSource = dt;
            grdView_Status.DataBind();

            con1.Close();
        }
        else
        {
            con1.Open();
            string query = "select * from HRMSPortal.dbo.tbl_Co_Leave_Application where convert(date,Atte_Date,103)>='" + txtFromDate_ViewStatus.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + txtTodate_ViewStatus.Text.Trim() + "' and Userid='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and ApprovalStatus='" + ddStatus_ViewStatus.SelectedValue.Trim() + "' and  LeaveType='EWL'";


            SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grdView_Status.DataSource = dt;
            grdView_Status.DataBind();

            con1.Close();





          
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExporttoexcel_viewStatus_Click(object sender, EventArgs e)
    {
        grdView_Status.AllowPaging = false;
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdView_Status.RenderControl(htmlWrite);

        Response.Clear();
        string str = "Data" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();

    }
    protected void rdEmployeeID_CheckedChanged(object sender, EventArgs e)
    {
        pnlFilterByIDName.Visible = true;
        pnlFilterDate.Visible = false;
        btnFIlterGet_Approval.Visible = true;
        lblEmployeeIDNameText.Text = "Employee ID";
        ddStatus_Approval.Enabled = true;
    }
    protected void rdEmployeeName_CheckedChanged(object sender, EventArgs e)
    {
        pnlFilterByIDName.Visible = true;
        pnlFilterDate.Visible = false;
        btnFIlterGet_Approval.Visible = true;
        lblEmployeeIDNameText.Text = "Employee Name";
        ddStatus_Approval.Enabled = true;

    }
    protected void rdDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlFilterByIDName.Visible = false;
        pnlFilterDate.Visible = true;
        btnFIlterGet_Approval.Visible = true;
        ddStatus_Approval.Enabled = true;
    }
    protected void btnFIlterGet_Approval_Click(object sender, EventArgs e)
    {
        Show_HODData();
    }
    public void ShowPendingApprovalCount()
    {
        SqlDataReader dr = con.Show_HODODCountCOApplication(Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblCountODAppoval.Text = dr["ApprovalStatus"].ToString();
            dr.Close();
            con.DisConnect();
        }
        else
        {
            dr.Close();
            con.DisConnect();
        }



    }

    public void Show_HODData()
    {


        if (rdEmployeeID.Checked == false && rdEmployeeName.Checked == false && rdDatewise.Checked == false)
        {

            SqlDataReader dr = con.Show_CoApplicationforApprovalPending(Session["uid"].ToString(), "Pending", Session["uid"].ToString(), Session["Company"].ToString());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdApproval.DataSource = dt;
            grdApproval.DataBind();
            dr.Close();
            con.DisConnect();
            SqlDataReader dr1 = con.Show_CoApplicationforApprovalPending(Session["uid"].ToString(), "Pending", Session["uid"].ToString(), Session["Company"].ToString());
            if (dr1.HasRows)
            {
                btnApprove.Visible = true;
                btnReject.Visible = true;
            }
            else
            {
                btnApprove.Visible = false;
                btnReject.Visible = false;

            }
            dr1.Close();
            con.DisConnect();


        }
        if (rdEmployeeID.Checked == true)
        {



            SqlDataReader dr = con.Show_COApplicationByHODWithEMPID(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdApproval.DataSource = dt;
            grdApproval.DataBind();
            dr.Close();
            con.DisConnect();

            SqlDataReader dr1 = con.Show_COApplicationByHODWithEMPID(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
            dr1.Read();
            if (dr1.HasRows)
            {

                string drnnn = dr1["ApprovalStatus"].ToString();
                if (drnnn == "Pending")
                {
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                }
            }
            else
            {
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
            dr1.Close();
            con.DisConnect();
        }

        if (rdEmployeeName.Checked == true)
        {


            SqlDataReader dr = con.Show_COApplicationByHODWithEMPNAme(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdApproval.DataSource = dt;
            grdApproval.DataBind();
            dr.Close();
            con.DisConnect();

            SqlDataReader dr1 = con.Show_COApplicationByHODWithEMPNAme(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
            dr1.Read();
            if (dr1.HasRows)
            {
                string appr = dr1["ApprovalStatus"].ToString();
                if (appr == "Pending")
                {
                    btnReject.Visible = true;
                    btnApprove.Visible = true;
                }
                else
                {
                    btnReject.Visible = false;
                    btnApprove.Visible = false;
                }
            }
            else
            {
                btnReject.Visible = false;
                btnApprove.Visible = false;
            }
            dr1.Close();
            con.DisConnect();
        }

        if (rdDatewise.Checked == true)
        {

            SqlDataReader dr = con.Show_COApplicationByHODWithDatewise(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), txtFromDate_Approval.Text.Trim(), txtTodate_Approval.Text.Trim(), Session["Company"].ToString());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdApproval.DataSource = dt;
            grdApproval.DataBind();
            dr.Close();
            con.DisConnect();

            SqlDataReader dr1 = con.Show_COApplicationByHODWithDatewise(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), txtFromDate_Approval.Text.Trim(), txtTodate_Approval.Text.Trim(), Session["Company"].ToString());
            dr1.Read();
            if (dr1.HasRows)
            {
                string appr = dr1["ApprovalStatus"].ToString();
                if (appr == "Pending")
                {
                    btnReject.Visible = true;
                    btnApprove.Visible = true;
                }
                else
                {
                    btnReject.Visible = false;
                    btnApprove.Visible = false;
                }
            }
            else
            {
                btnReject.Visible = false;
                btnApprove.Visible = false;
            }
            dr1.Close();
            con.DisConnect();

        }
    }
    protected void btnApproveExport_Click(object sender, EventArgs e)
    {
        grdView_Status.AllowPaging = false;

        Show_HODData();


        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdApproval.RenderControl(htmlWrite);

        Response.Clear();
        string str = "Approvaldata" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdApproval.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);

                Label lblid = (row.Cells[0].FindControl("lblid") as Label);
                Label lblFDate = (Label)row.FindControl("lblAtte_Date_GridCo");
                Label EmpId = (Label)row.FindControl("lblEmployeeid_gridco");
                if (lblFDate.Text == "17 Mar 2023" || lblFDate.Text == "18 Mar 2023" || lblFDate.Text == "19 Mar 2023" || lblFDate.Text == "20 Mar 2023")
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('You can not approve CO for date " + lblFDate.Text + ".');", true);
                    return;
                }
                else
                {
                if (chkRow.Checked == true)
                {
                    string query = "  select count(*) as 'Valid' from [EDUCOLLEGELIVE-R2].dbo.[TMU$Medical OT Days Calculation] where [Employee Code]='" + EmpId.Text + "' and [Date]='" + lblFDate.Text + "'";
                    SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    con.DisConnect();

                    if (Convert.ToInt32(dt.Rows[0]["Valid"]) > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('OT Already Applied for this Date, kindly Reject CO.');", true);
                        return;
                    }



                    ShowApprovalData(lblid.Text.Trim());
                    sendAproved(lblFDate.Text.Trim(), EmpId.Text, "Approved");// for sms
                }
            }
          }
        }
        Show_HODData();
        ShowPendingApprovalCount();

    }


    string hourworking = ""; string CoRemarks = ""; string useridapr = ""; string Emp_No_OD = ""; string frmmdate_OD = ""; string Todate_OD = ""; string Purpose_OD = ""; string fromtime_od = ""; string Totime_OD = ""; string Approval_Status_OD = "";
    public void ShowApprovalData(string id)
    {
        SqlDataReader dr = con.Show_ApprovalCOid(id);
        dr.Read();
        if (dr.HasRows)
        {
            frmmdate_OD = Convert.ToDateTime(dr["Atte_Date"].ToString()).ToString("yyyy-MM-dd");

            DateTime frmmdate_ODa = DateTime.ParseExact(frmmdate_OD, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime frmmdate_ODa = Convert.ToDateTime(frmmdate_OD);
            frmmdate_OD = frmmdate_ODa.ToString("yyyy-MM-dd");
            useridapr = dr["Userid"].ToString();
            fromtime_od = dr["fromTime"].ToString();
            hourworking = dr["Working_Duration"].ToString();
            if (hourworking == "")
            {
                hourworking = "0.00";
            }
            //Todate_OD = dr["To Date"].ToString();
            Totime_OD = dr["ToTime"].ToString();

            if (Totime_OD == "00:00")
            {
                Totime_OD = "1753-01-01 00:00:00.000";
            }

            if (fromtime_od == "00:00")
            {
                fromtime_od = "1753-01-01 00:00:00.000";
            }



            Approval_Status_OD = dr["ApprovalStatus"].ToString();

            dr.Close();
            con.DisConnect();

            if (Approval_Status_OD == "Pending")
            {

                string ccname = Session["Company"].ToString();
                string rccname = ccname.Replace(".", "_");
                string tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string tbleEmployeeLeaveCredited = "[" + rccname + "$Pay Employee Leave Credited" + "]";
                string tble_Leave_Entitledpost = "[" + rccname + "$Pay Employee Leave Entitled" + "]";
                con.Update_CoStatus(id, Session["uname"].ToString());
                con.DisConnect();

                SqlConnection Conn = new SqlConnection();
                Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
                Conn.Open();
                SqlCommand cmd1 = new SqlCommand("select * from  [tbl_Co_Leave_Application]  where Userid='" + useridapr + "' and Co_Leave='1' and convert(date,Atte_Date,103)='" + frmmdate_OD + "' and ApprovalStatus='Approved'", Conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                Conn.Close();
                if (dt1.Rows.Count > 0)
                {

                    navconn.Update_Co_ApplicationApproval(tblenameAttendence, useridapr, frmmdate_OD, CoRemarks);

                    navconn.DisConnect();
                    navconn.insert_Table_Credit_LeaveCO(tbleEmployeeLeaveCredited, useridapr, frmmdate_OD, "CO", Convert.ToDecimal(1.00), Convert.ToInt32(1), "0", "");
                    navconn.DisConnect();
                    string coLeaBalance = "";
                    SqlDataReader drcor = navconn.ShowCo_Leave_BalancePOstCo(tble_Leave_Entitledpost, useridapr);
                    drcor.Read();
                    if (drcor.HasRows)
                    {

                        coLeaBalance = drcor["Leave Balance"].ToString();
                        drcor.Close();
                        navconn.DisConnect();
                        decimal colevb = Convert.ToDecimal(coLeaBalance);
                        decimal colevb1 = colevb + 1;

                        navconn.updateCoLeaveBalancePlusHRCO(tble_Leave_Entitledpost, colevb1.ToString(), useridapr);
                        navconn.DisConnect();

                    }
                    else
                    {
                        drcor.Close();
                        navconn.DisConnect();
                    }



                }

            }
            else
            {
                dr.Close();
                con.DisConnect();

            }
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {

    }
    protected void lnkODApplication_Click(object sender, EventArgs e)
    {
        lblHeader.Text = "Application";
        pnlViewStatus.Visible = false;
        pnlCOApplication.Visible = true;
        pnlApproval.Visible = false;
        lblCOSuccess.Visible = false;
        clear();
    }
    protected void lnkODView_Click(object sender, EventArgs e)
    {
        lblHeader.Text = "Report";
        pnlViewStatus.Visible = true;
        pnlCOApplication.Visible = false;
        pnlApproval.Visible = false;
        txtFromDate_ViewStatus.Text = "";
        txtTodate_ViewStatus.Text = "";
        ddStatus_ViewStatus.SelectedValue = "Pending";
        txtFromDate_ViewStatus.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        txtTodate_ViewStatus.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        Show_Report_ofuser();
        //Show_ODStatusByOwn();
        //grdView_Status.Columns[8].Visible = false;
        //grdView_Status.Columns[9].Visible = false;
    }
    protected void lnkApproval_Click(object sender, EventArgs e)
    {
        btnApprove.Text = "Approval";
        lblHeader.Text = "Approval";
        lnkApproval.Text = "Approval";
        pnlViewStatus.Visible = false;
        pnlCOApplication.Visible = false;
        pnlApproval.Visible = true;
        btnFIlterGet_Approval.Visible = false;
        ddStatus_Approval.SelectedValue = "Pending";
        pnlFilterDate.Visible = false;
        pnlFilterByIDName.Visible = false;
        rdDatewise.Checked = false;
        rdEmployeeID.Checked = false;
        rdEmployeeName.Checked = false;
        Show_HODData();


        //grdApproval.Columns[0].Visible = true;
        //grdApproval.Columns[11].Visible = false;
        //ddStatus_Approval.Enabled = false;

    }
    protected void grdView_Status_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdView_Status.PageIndex = e.NewPageIndex;
        Show_Report_ofuser();

    }
    protected void grdApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApproval.PageIndex = e.NewPageIndex;
        Show_HODData();

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    string Approval_Status_OD_reject = "";
    public void ShowApprovalDataRejected(string id)
    {
        SqlDataReader dr = con.Show_ApprovalCOid(id);
        dr.Read();
        if (dr.HasRows)
        {
            Approval_Status_OD_reject = dr["ApprovalStatus"].ToString();
            dr.Close();
            con.DisConnect();

            if (Approval_Status_OD_reject == "Pending")
            {

                con.Update_COStatusReject(id, Session["uname"].ToString(), txtRemarks_ByHOD.Text.Trim());
                con.DisConnect();
            }
        }
        else
        {
            dr.Close();
            con.DisConnect();

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdApproval.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);

                Label lblid = (row.Cells[0].FindControl("lblid") as Label);
                Label lblFDate = (Label)row.FindControl("lblAtte_Date_GridCo");
                Label EmpId = (Label)row.FindControl("lblEmployeeid_gridco");

                if (chkRow.Checked == true)
                {

                    ShowApprovalDataRejected(lblid.Text.Trim());
                    sendAproved(lblFDate.Text.Trim(), EmpId.Text, "Rejected");
                }
            }
        }
        Show_HODData();
        ShowPendingApprovalCount();

    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        string query = "SELECT [EmployeeCode] FROM [dbo].[tbl_CoAllowEmployee] where EmployeeCode='" + Session["uid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        SqlDataAdapter da1 = new SqlDataAdapter("select isnull(MIN([Punch Time]),'00:00') as 'Time From',isnull(MAX([Punch Time]),'00:00') as 'Time To' from  [TMU$Employee Device Punches] where [Employee Machine Code]= (select [Employee Machine Code] from [TMU$Employee] where No_='" + Session["uid"].ToString() + "' ) and [Punch Date]='" + txtFromDate.Text.Trim() + "' ", navconn.Con);
        //SqlDataAdapter da1 = new SqlDataAdapter("select *,(select [Night Exist] from [TMU$Shift Master] where [Shift Code]=[TMU$Employee Actual Punch Data].[Shift Code]) as 'Night' from  [TMU$Employee Actual Punch Data] where [Employee No]='" + Session["uid"].ToString() + "' and [Attendance Date]='" + txtFromDate.Text.Trim() + "' ", navconn.Con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            DateTime datetime = DateTime.Parse(dt1.Rows[0]["Time From"].ToString());
            DateTime dt2 = DateTime.Parse(dt1.Rows[0]["Time To"].ToString());
            //DateTime ShiftFrom = DateTime.Parse(dt1.Rows[0]["Shift Time In"].ToString());
            //DateTime ShiftTo = DateTime.Parse(dt1.Rows[0]["Shift Time Out"].ToString());
            //DateTime ShiftTo1 = DateTime.Parse(dt1.Rows[0]["Shift Time Out"].ToString());
            //ShiftTo1 = ShiftTo1.AddHours(1);
            txtFromTime.Text = datetime.ToString("HH:mm");
            txtToTime.Text = dt2.ToString("HH:mm");
            //txtFromTime1.Text = datetime.ToString("HH:mm");
            //txtToTime1.Text = dt2.ToString("HH:mm");
            //hfShiftFrom.Value = ShiftFrom.ToString("HH:mm");
            //hfShiftTo.Value = ShiftTo.ToString("HH:mm");
            //hfShiftTo1.Value = ShiftTo1.ToString("HH:mm");
            //hfMachineID.Value = dt1.Rows[0]["Employee Machine Code"].ToString();
            //hfTotBUtilize.Value = dt1.Rows[0]["Total Buffer Utilize"].ToString();
            //hfnight.Value = dt1.Rows[0]["Night"].ToString();
        }





        if ((txtFromDate.Text == "25 Jul 2022" || txtFromDate.Text == "26 Jul 2022" || txtFromDate.Text == "01 Aug 2022" || txtFromDate.Text == "08 Aug 2022") && dt.Rows.Count == 0)
        {

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('This date is not allowed for CO.');", true);
            txtFromDate.Text = "";

        }
        else
        {

            ToTalWorkingHour();
        }
        // findHolidayorweekdays();  //  by ashu on 09-03-2017
    }
    protected void txtFromTime_TextChanged(object sender, EventArgs e)
    {
        ToTalWorkingHour();
    }
    protected void txtToTime_TextChanged(object sender, EventArgs e)
    {
        ToTalWorkingHour();
    }
    protected void grdApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Button btncancel_Approvedleave = (Button)e.Row.FindControl("btncancel_Approvedleave");
            Label lblApprovalStatus_Status = (Label)e.Row.FindControl("lblApprovalStatus_Status");
            Label lblAtte_Date_GridCo = (Label)e.Row.FindControl("lblAtte_Date_GridCo");
            Label lblEmployeeid_gridco = (Label)e.Row.FindControl("lblEmployeeid_gridco");

            CheckBox chkMark = (CheckBox)e.Row.FindControl("chkMark");

            if (lblApprovalStatus_Status.Text.Trim() == "Approved")
            {

                SqlDataReader drcowrok = con.Show_tbl_Co_Leave_Details_Basedonworking(lblEmployeeid_gridco.Text.Trim(), Session["Company"].ToString().Trim(), lblAtte_Date_GridCo.Text.Trim());
                drcowrok.Read();
                if (drcowrok.HasRows)
                {
                    drcowrok.Close();
                    con.DisConnect();
                    btncancel_Approvedleave.Text = "Cancel";
                    btncancel_Approvedleave.Visible = true;

                }
                else
                {
                    drcowrok.Close();
                    con.DisConnect();
                    //btncancel_Approvedleave.Text = "Used";
                    btncancel_Approvedleave.ToolTip = "Employee taken the Co Leave based on this date";
                    btncancel_Approvedleave.Visible = false;


                }


            }
            if (lblApprovalStatus_Status.Text.Trim() != "Approved")
            {
                btncancel_Approvedleave.Visible = false;
            }
            if (lblApprovalStatus_Status.Text == "Pending")
            {
                chkMark.Visible = true;
            }

            if (lblApprovalStatus_Status.Text != "Pending")
            {
                chkMark.Visible = false;
            }
        }
    }

    protected void btncancel_Approvedleave_Command(object sender, CommandEventArgs e)
    {

        string id = e.CommandArgument.ToString();
        Session["id_Canceled"] = id.ToString();
        lblCancelmessage.Visible = false;
        ModalPopupExtender1.Show();
    }
    protected void btncancel_record_Click(object sender, EventArgs e)
    {
        SqlDataReader drtu = con.Show_Co_data_canceled(Session["id_Canceled"].ToString().Trim());
        drtu.Read();
        if (drtu.HasRows)
        {
            string Codates = Convert.ToDateTime(drtu["Atte_Date"].ToString()).ToString("yyyy-MM-dd");

            string userids = drtu["Userid"].ToString();
            string ApprovalStatus = drtu["ApprovalStatus"].ToString();

            if (ApprovalStatus == "Approved")
            {

                drtu.Close();
                con.DisConnect();


                SqlDataReader drcowrok = con.Show_tbl_Co_Leave_Details_Basedonworking(userids, Session["Company"].ToString().Trim(), Codates);
                drcowrok.Read();
                if (drcowrok.HasRows)
                {
                    drcowrok.Close();
                    con.DisConnect();

                    string ccname = Session["Company"].ToString();
                    string rccname = ccname.Replace(".", "_");
                    string tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                    string tbleEmployeeLeaveCredited = "[" + rccname + "$Pay Employee Leave Credited" + "]";
                    string tble_Leave_Entitledpost = "[" + rccname + "$Pay Employee Leave Entitled" + "]";
                    navconn.Update_Co_AfterCancelled(tblenameAttendence, userids, Codates);

                    string coLeaBalance = "";
                    SqlDataReader drcor = navconn.ShowCo_Leave_BalancePOstCo(tble_Leave_Entitledpost, userids);
                    drcor.Read();
                    if (drcor.HasRows)
                    {

                        coLeaBalance = drcor["Leave Balance"].ToString();
                        drcor.Close();

                        decimal colevb = Convert.ToDecimal(coLeaBalance);
                        decimal colevb1 = colevb - 1;

                        navconn.updateCoLeaveBalancePlusHRCO(tble_Leave_Entitledpost, colevb1.ToString(), userids);
                        navconn.Delete_Co_LeavefromCretidedtable(tbleEmployeeLeaveCredited, userids, Codates);
                        navconn.DisConnect();
                        con.Update_COStatusCanceled(Session["id_Canceled"].ToString().Trim(), Session["uname"].ToString().Trim(), txtCancel_Remarks.Text.Trim(), userids.Trim(), Codates.Trim(), ccname.Trim());
                        con.DisConnect();
                        Show_HODData();
                        lblCancelmessage.Visible = true;
                        lblCancelmessage.Text = "Successfully Cancelled. !";


                    }
                    else
                    {
                        drcor.Close();
                        navconn.DisConnect();
                        lblCancelmessage.Visible = true;
                        lblCancelmessage.Text = "CO Leave type not tag with this employee !";
                    }
                }
                else
                {
                    drcowrok.Close();
                    con.DisConnect();
                    lblCancelmessage.Visible = true;
                    lblCancelmessage.Text = "Just applied leave on this working date !";
                }
            }

        }
        else
        {
            drtu.Close();
            con.DisConnect();
            lblCancelmessage.Visible = true;
            lblCancelmessage.Text = "Something went to wrong !";
        }

        ModalPopupExtender1.Show();


    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}