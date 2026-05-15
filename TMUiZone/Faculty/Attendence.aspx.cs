using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;

public partial class LeaveApproval : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    string holiday = "";
    string offDay = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Portalcon = new Connection();
        con = new ServicePoratal();
        try
        {
            if (!IsPostBack)
            {
               // if ((this.Master as IndexMaster).GetLinkYesNo("UserRoleMatrix") == "True")  { }    else  { Response.Redirect("~/Default.aspx"); }
            } 
            if (Session["uname"].ToString() == null || Session["Company"].ToString() == null)
            {

                Response.Redirect("../Default.aspx");
            }
            else
            {
                Showpermission();
                showHRHODisexhist();
                if (Session["uid"].ToString() == Session["HRID"].ToString())
                {
                    HRHODsame = "HRHOD";
                }
                if (Session["uid"].ToString() != Session["HRID"].ToString())
                {

                    HRHODsame = "HR";
                }
                if (Session["uid"].ToString() == Session["HODLoginPage"].ToString())
                {
                    forHRisHOD = "HRHOD";
                }
                if (Session["uid"].ToString() != Session["HODLoginPage"].ToString())
                {

                    forHRisHOD = "HR";
                }
                if (!IsPostBack)
                {
                    showAttendenceExpiry();
                    showAttendenceExpiryIND();
                }
                Attend_Pending_ApprovalUser_Count();
                if (!IsPostBack)
                {
                    txtJobLocation.Text = Session["LocationCodereim"].ToString();
                }
                show_Attendence();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void lnkleaveview_Click(object sender, EventArgs e)
    {

        pnlAttendenceMark.Visible = true;
        pnlviewattend.Visible = false;
        pnlMain.Visible = false;
        pnlviewAttendence.Visible = false;
        pnlProfileView.Visible = false;
        show_Attendence();
        showAttendenceExpiry();
        showAttendenceExpiryIND();

       // clear();
        showAttendenceExpiryAdd();
        showAttendenceExpiryINDforAdd();
        Calendar1.Enabled = true;
        btnSave.Visible = true;
        btnUpdate.Visible = false;

        btnClose.Visible = false;
    }
    public void showHRHODisexhist()
    {

        if (Session["UserType"].ToString() == "2")
        {
            SqlDataReader dr = Portalcon.SHow_showHRExhist(Session["uid"].ToString(), Session["CompanyTableEmployee"].ToString());
            dr.Read();
            if (dr.HasRows)
            {
                dr.Close();
                lnkViewTeamAttendance.Visible = true;
            }
            else
            {
                dr.Close();
                Portalcon.DisConnect();
                lnkViewTeamAttendance.Visible = false;
            }
        }

        else
        {
            SqlDataReader dr = Portalcon.SHow_showHODExhist(Session["uid"].ToString(), Session["CompanyTableEmployee"].ToString());
            dr.Read();
            if (dr.HasRows)
            {
                dr.Close();
                Portalcon.DisConnect();

                lnkViewTeamAttendance.Visible = true;
            }
            else
            {
                dr.Close();
                Portalcon.DisConnect();
                lnkViewTeamAttendance.Visible = false;
            }
        }
    }


    public void Attend_Pending_ApprovalUser_Count()
    {
        SqlDataReader dr = con.Attend_Approval_Count_CurrentUser(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["UserID"].ToString();
            if (countprofile == "0")
            {
                //lblAttendence.Text = "";
                lblPostAttendanceNotify.Text = "";
            }
            else
            {
                //lblAttendence.Text = dr["UserID"].ToString();
                lblPostAttendanceNotify.Text = dr["UserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    } 
   
    protected void lnkRejectLeaveDetail_Click(object sender, EventArgs e)
    {
        pnlviewAttendence.Visible = true;
        pnlAttendenceMark.Visible = false;
        pnlviewattend.Visible = true;
        pnlMain.Visible = false;
        pnlProfileView.Visible = false;

        showAttendenceExpiry();
        showAttendenceExpiryIND();
        clear();
    }

    string tHour = "";
    public void ToTalWorkingHour()
    {
        try
        {
            string curToDate ="";
            string curdate = Calendar1.SelectedDate.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Trim();
            string curTime = txtFromTime.Text;
            string curFromDatetime = curdate + " " + curTime;
            if (txtToTime.Text == "")
            { 
            curToDate=curdate+" "+"00:00";
            }
            else
            {
                curToDate = curdate + " " + txtToTime.Text.Trim();
            }
            
           
            //DateTime startDateTime = Convert.ToDateTime(curFromDatetime);
            //DateTime endDateTime = Convert.ToDateTime(CurToDate);
            DateTime startDateTime = DateTime.ParseExact(curFromDatetime, "yyyy-MM-dd H:m", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDateTime = DateTime.ParseExact(curToDate, "yyyy-MM-dd H:m", System.Globalization.CultureInfo.InvariantCulture);


            //DateTime startDateTime = DateTime.ParseExact(curFromDatetime, "yyyy-MM-dd H:m", null);
            //DateTime endDateTime = DateTime.ParseExact(CurToDate, "yyyy-MM-dd H:m", null);
            if (endDateTime < startDateTime)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Out time should always be greater than In Time');", true);
            
            }

            else
            {
                string CurToDate1="";
                string curdate1 = Calendar1.SelectedDate.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Trim();
                string curTime1 = txtFromTime.Text.Trim();
                string curFromDatetime1 = curdate1 + " " + curTime1;
                //string curToDate1 = txtToTime.Text.Trim();
                // = curdate1 + " " + curToDate1;
                if (txtToTime.Text == "")
                {
                    CurToDate1 = curdate1 + " " + "00:00";
                }
                else
                {
                    CurToDate1 = curdate1 + " " + txtToTime.Text.Trim();
                }
                DateTime startDateTime1 = DateTime.ParseExact(curFromDatetime1, "yyyy-MM-dd H:m", System.Globalization.CultureInfo.InvariantCulture);
                DateTime endDateTime1 = DateTime.ParseExact(CurToDate1, "yyyy-MM-dd H:m", System.Globalization.CultureInfo.InvariantCulture);
                TimeSpan difference = endDateTime1 - startDateTime1;

                string differenceString = difference.ToString();
                string s = differenceString.ToString();
                 tHour = difference.TotalHours.ToString();
             
            }
        }
        catch (Exception)
        {


        }
    }
    string tupdateHour = "";
    public void ToTalWorkDuration()
    {
        try
        {

            string curdate = lblDateForUpdate.Text.Trim();
            string curTime = txtFromTime.Text;
            string curFromDatetime = curdate + " " + curTime;
            string curToDate = txtToTime.Text;
            string CurToDate = curdate + " " + curToDate;
            //DateTime startDateTime = Convert.ToDateTime(curFromDatetime);
            //DateTime endDateTime = Convert.ToDateTime(CurToDate);
            DateTime startDateTime = DateTime.ParseExact(curFromDatetime, "dd-MM-yyyy H:m", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDateTime = DateTime.ParseExact(CurToDate, "dd-MM-yyyy H:m", System.Globalization.CultureInfo.InvariantCulture);


            //DateTime startDateTime = DateTime.ParseExact(curFromDatetime, "yyyy-MM-dd H:m", null);
            //DateTime endDateTime = DateTime.ParseExact(CurToDate, "yyyy-MM-dd H:m", null);
            if (endDateTime < startDateTime)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Out time should always be greater than In Time');", true);

            }

            else
            {
                string curdate1 = lblDateForUpdate.Text.Trim();
                string curTime1 = txtFromTime.Text.Trim();
                string curFromDatetime1 = curdate1 + " " + curTime1;
                string curToDate1 = txtToTime.Text.Trim();
                string CurToDate1 = curdate1 + " " + curToDate1;

                DateTime startDateTime1 = DateTime.ParseExact(curFromDatetime1, "dd-MM-yyyy H:m", System.Globalization.CultureInfo.InvariantCulture);
                DateTime endDateTime1 = DateTime.ParseExact(CurToDate1, "dd-MM-yyyy H:m", System.Globalization.CultureInfo.InvariantCulture);
                TimeSpan difference = endDateTime1 - startDateTime1;

                string differenceString = difference.ToString();
                string s = differenceString.ToString();
                tupdateHour = difference.TotalHours.ToString();

            }
        }
        catch (Exception)
        {


        }
    }


    public void show_Attendence()
    {


        SqlDataReader odr = con.Show_Attendence(Session["uid"].ToString(), Session["Company"].ToString());
        DataTable Dt = new DataTable();
        Dt.Load(odr);
        grdAttendence.DataSource = Dt;
        grdAttendence.DataBind();
        odr.Close();
        con.DisConnect();
    }

   

    public void show_Attendence_withDate()
    {
        if (ddstatus.Text == "All")
        {

            SqlDataReader odr = con.Show_AttendenceWithDateBetween(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
            DataTable Dt = new DataTable();
            Dt.Load(odr);
            grdViewdetail.DataSource = Dt;
            grdViewdetail.DataBind();
            odr.Close();
            con.DisConnect();
        }
        //if (ddstatus.Text == "Rejected")
        //{
        //    SqlDataReader odr = con.Show_AttendenceWithDateBetweenwithRejected(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
        //    DataTable Dt = new DataTable();
        //    Dt.Load(odr);
        //    grdViewdetail.DataSource = Dt;
        //    grdViewdetail.DataBind();
        //    odr.Close();
        //    con.DisConnect();

        //}
        else if(ddstatus.Text == "Pending" || ddstatus.SelectedValue.ToString()=="Approved")
        {
            SqlDataReader odr = con.Show_AttendenceWithDateBetweenwithStatus(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, ddstatus.Text, Session["Company"].ToString());
            DataTable Dt = new DataTable();
            Dt.Load(odr);
            grdViewdetail.DataSource = Dt;
            grdViewdetail.DataBind();
            odr.Close();
            con.DisConnect();
        
        }

    }
    protected void lnkApprovedApproveddetail_Click(object sender, EventArgs e)
    {
      
        pnlviewAttendence.Visible = false;
        pnlAttendenceMark.Visible = false;
        pnlMain.Visible = false;
    }


    string HODapr = "";
    string HRapr = "";
    string Blankapr = "";
    string PriorityHRapr = "";
    string PriorityHODapr = "";
    string EmailBlank = "";
    string EmailHR = "";
    string EmailHOD = "";

    public void Showpermission()
    {
        string type = "For Attendance";
        SqlDataReader dr = con.SHow_tblesetupforpriorityforApproval(type,Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            HODapr = dr["HOD"].ToString();

            HRapr = dr["HR"].ToString();

            Blankapr = dr["Blank"].ToString();

            PriorityHRapr = dr["PriorityHR"].ToString();

            PriorityHODapr = dr["PriorityHOD"].ToString();
            EmailBlank = dr["EmailBlank"].ToString();
            EmailHR = dr["EmailHR"].ToString();
            EmailHOD = dr["EmailHOD"].ToString();

            if (Blankapr == "1")
            {
                lnkpostAttendance.Visible = true;
                lblPostAttendanceNotify.Visible = true;
                imgStar.Visible = true;
                //lnkpostAttendance.Visible = true;
                //lnkAttendanceApprovalHODHR.Visible = false;
                //lblAttendence.Visible = false;
            }
            if (PriorityHRapr == "1" || PriorityHODapr == "1")
            {
                lnkpostAttendance.Visible = false;
                lblPostAttendanceNotify.Visible = false;
                imgStar.Visible = false;
            }

        }
        dr.Close();
        con.DisConnect();
    }
    public void clear()
    { 
        
        txtFromTime.Text = "";
    txtToTime.Text = "";
    txtJobLocation.Text = "";
    txtRemarks.Text = "";

    }


    string mailfrom = ""; string subject1 = ""; string Body1 = ""; string smtpfromportal = ""; int portNo1; string Pass_From = ""; string Mail_Sending_Option = ""; string Leave_Applymail = ""; string CCMail = "";
    public void ShowMailData(string mailTo1)
    {

        SqlDataReader dr = con.Show_tble_MailSetup(Session["Company"].ToString(), "Attendance");
        dr.Read();
        if (dr.HasRows)
        {
            mailfrom = dr["from_Email"].ToString();
            smtpfromportal = dr["smtp"].ToString();
            Pass_From = dr["Password_From"].ToString();
            CCMail = dr["CCMail"].ToString();
            string portNo = dr["Port_No"].ToString();
            portNo1 = Convert.ToInt32(portNo);
            Mail_Sending_Option = dr["Mail_Sending_Option"].ToString();
            Leave_Applymail = dr["Attendence_Mark"].ToString();

        }

        dr.Close();
        con.DisConnect();
        if (Mail_Sending_Option == "1" && Leave_Applymail == "True")
        {
            SendMail(mailTo1);
        }

    }


    public void SendMail(string MailTo)
    {

        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        if (mailfrom == "" && MailTo == "")
        { }

        else
        {
            if (mailfrom == "")
            {
            }

            else
            {

                msg.From = new MailAddress(Session["CompanyEmail"].ToString().Trim(), "  " + Session["Fulname"].ToString());
                if (MailTo == "")
                { }
                else
                {

                    string[] multi = MailTo.Split(',');
                    foreach (string multiTo in multi)
                    {
                        msg.To.Add(multiTo);
                    }
                }
                if (CCMail == "")
                { }
                else
                {
                    string[] ccmulti = CCMail.Split(',');
                    foreach (string ccm in ccmulti)
                    {
                        msg.CC.Add(ccm);
                    }
                }
                msg.Subject = subject1;
                msg.Body = Body1;


                SmtpClient smtp = new SmtpClient();

                smtp.Port = portNo1;
                smtp.Host = smtpfromportal;
                smtp.EnableSsl = true;
                NetworkCredential credential = new NetworkCredential(mailfrom, Pass_From);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credential;

                try
                {
                    smtp.Send(msg);
                    msg.Dispose();

                }
                catch (Exception)
                {
                    msg.Dispose();
                }
            }
        }
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        //string confirmValue = Request.Form["confirm_value"];
        //if (confirmValue == "Yes")
        //{
            showAttendenceExpiryAdd();
            showAttendenceExpiryINDforAdd();
            holidayList();
            LeaveApprovalforAttendence();
            string Curdate = Calendar1.SelectedDate.ToString("dd-MM-yyyy");

            if (Curdate == "01-01-0001")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select date from calender');", true);
            }
            else
            {
                string caldate = Calendar1.SelectedDate.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Trim();
                string cudate = System.DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).Trim();
                DateTime caldate1 = DateTime.ParseExact(caldate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime cudate1 = DateTime.ParseExact(cudate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);



                if (caldate1 > cudate1)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not mark attendance of this date');", true);

                }

                else
                {

                    SqlDataReader dr = con.Show_AttendenceWithDate(Session["uid"].ToString(), Curdate, Session["Company"].ToString());
                    dr.Read();
                    if (dr.HasRows)
                    {
                        dr.Close();
                        con.DisConnect();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have already marked attendance of this date');", true);

                    }
                    else
                    {


                        dr.Close();
                        con.DisConnect();
                        ToTalWorkingHour();
                        if (Blankapr == "1")
                        {

                        
                            if ( leavestatus == "6" || leavestatus == "7")
                            {
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not mark attendance of this date beacuse you have applied for leave on selected date');", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not mark attendance beacuse you have already applied for leave');", true);

                            }
                            else if (leavestatus == "4" && leavestypess == "3")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not mark attendance beacuse you have already applied for leave`');", true);
                            }
                            else
                            {
                                if (Session["uid"].ToString() == EmployeeIDIndv && optForFromTimeTotimeind == "Enable" && In_time_Out_TimeRquiredind == "Yes")
                                {
                                    if (txtFromTime.Text == "" )
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please fill in time ');", true);

                                    }
                                    else

                                    {
                                        string hodaprid = ""; string hruid = "";
                                        con.insert_AttendenceDetail(Session["uid"].ToString(), Session["Fulname"].ToString(), Session["Company"].ToString(), Curdate, txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, "Present", tHour, System.DateTime.Now.ToString("dd-MM-yyyy"), hodaprid, hruid, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(),Convert.ToInt32(lblCO.Text.Trim()));

                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Post the attendance from Post Attendance option');", true);

                                        con.DisConnect();

                                        show_Attendence();

                                        if (EmailHR == "True")
                                        {

                                            subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                            ShowMailData(Session["hr_email2"].ToString());
                                        }
                                        if (EmailHOD == "True")
                                        {

                                            subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, Session["hod_Name2"].ToString(), Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                            ShowMailData(Session["hod_email2"].ToString());
                                        }
                                        clear();
                                    
                                    }
                                }




                                if (Session["uid"].ToString() != EmployeeIDIndv && optForFromTimeTotimeAll == "Enable" && In_time_Out_TimeRquired == "Yes")
                                {
                                    if (txtFromTime.Text == "")
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please fill in time and out time ');", true);

                                    }
                                    else
                                    {
                                        string hodaprid = ""; string hruid = "";
                                        con.insert_AttendenceDetail(Session["uid"].ToString(), Session["Fulname"].ToString(), Session["Company"].ToString(), Curdate, txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, "Present", tHour, System.DateTime.Now.ToString("dd-MM-yyyy"), hodaprid, hruid, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), Convert.ToInt32(lblCO.Text.Trim()));

                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Post the attendance from Post Attendance option');", true);

                                        con.DisConnect();

                                        show_Attendence();

                                        if (EmailHR == "True")
                                        {

                                            subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                            ShowMailData(Session["hr_email2"].ToString());
                                        }
                                        if (EmailHOD == "True")
                                        {

                                            subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, Session["hod_Name2"].ToString(), Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                            ShowMailData(Session["hod_email2"].ToString());
                                        }
                                        clear();

                                    }
                                }


                                if (Session["uid"].ToString() != EmployeeIDIndv && optForFromTimeTotimeAll == "Disable" && In_time_Out_TimeRquired == "Yes")
                                {
                                    //if (txtFromTime.Text == "" || txtToTime.Text == "")
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please fill in time and out time ');", true);

                                    //}
                                    //else
                                    //{
                                        string hodaprid = ""; string hruid = "";
                                        con.insert_AttendenceDetail(Session["uid"].ToString(), Session["Fulname"].ToString(), Session["Company"].ToString(), Curdate, txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, "Present", tHour, System.DateTime.Now.ToString("dd-MM-yyyy"), hodaprid, hruid, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), Convert.ToInt32(lblCO.Text.Trim()));

                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Post the attendance from Post Attendance option');", true);

                                        con.DisConnect();

                                        show_Attendence();

                                        if (EmailHR == "True")
                                        {

                                            subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                            ShowMailData(Session["hr_email2"].ToString());
                                        }
                                        if (EmailHOD == "True")
                                        {

                                            subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, Session["hod_Name2"].ToString(), Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                            ShowMailData(Session["hod_email2"].ToString());
                                        }
                                        clear();

                                    //}
                                }


                                if (Session["uid"].ToString() == EmployeeIDIndv && optForFromTimeTotimeind == "Disable" && In_time_Out_TimeRquiredind == "Yes")
                                {
                                    //if (txtFromTime.Text == "" || txtToTime.Text == "")
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please fill in time and out time ');", true);

                                    //}
                                    //else
                                    //{
                                    string hodaprid = ""; string hruid = "";
                                    con.insert_AttendenceDetail(Session["uid"].ToString(), Session["Fulname"].ToString(), Session["Company"].ToString(), Curdate, txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, "Present", tHour, System.DateTime.Now.ToString("dd-MM-yyyy"), hodaprid, hruid, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), Convert.ToInt32(lblCO.Text.Trim()));

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Post the attendance from Post Attendance option');", true);

                                    con.DisConnect();

                                    show_Attendence();

                                    if (EmailHR == "True")
                                    {

                                        subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                        ShowMailData(Session["hr_email2"].ToString());
                                    }
                                    if (EmailHOD == "True")
                                    {

                                        subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, Session["hod_Name2"].ToString(), Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                        ShowMailData(Session["hod_email2"].ToString());
                                    }
                                    clear();

                                    //}
                                }



                                if (Session["uid"].ToString() == EmployeeIDIndv && optForFromTimeTotimeind == "Disable" && In_time_Out_TimeRquiredind == "No")
                                {
                                    //if (txtFromTime.Text == "" || txtToTime.Text == "")
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please fill in time and out time ');", true);

                                    //}
                                    //else
                                    //{
                                    string hodaprid = ""; string hruid = "";
                                    con.insert_AttendenceDetail(Session["uid"].ToString(), Session["Fulname"].ToString(), Session["Company"].ToString(), Curdate, txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, "Present", tHour, System.DateTime.Now.ToString("dd-MM-yyyy"), hodaprid, hruid, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), Convert.ToInt32(lblCO.Text.Trim()));

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Post the attendance from Post Attendance option');", true);

                                    con.DisConnect();

                                    show_Attendence();

                                    if (EmailHR == "True")
                                    {

                                        subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                        ShowMailData(Session["hr_email2"].ToString());
                                    }
                                    if (EmailHOD == "True")
                                    {

                                        subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, Session["hod_Name2"].ToString(), Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                        ShowMailData(Session["hod_email2"].ToString());
                                    }
                                    clear();

                                    //}
                                }

                                if (Session["uid"].ToString() != EmployeeIDIndv && optForFromTimeTotimeAll == "Disable" && In_time_Out_TimeRquired == "No")
                                {
                                    //if (txtFromTime.Text == "" || txtToTime.Text == "")
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please fill in time and out time ');", true);

                                    //}
                                    //else
                                    //{
                                    string hodaprid = ""; string hruid = "";
                                    con.insert_AttendenceDetail(Session["uid"].ToString(), Session["Fulname"].ToString(), Session["Company"].ToString(), Curdate, txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, "Present", tHour, System.DateTime.Now.ToString("dd-MM-yyyy"), hodaprid, hruid, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), Convert.ToInt32(lblCO.Text.Trim()));

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Post the attendance from Post Attendance option');", true);

                                    con.DisConnect();

                                    show_Attendence();

                                    if (EmailHR == "True")
                                    {

                                        subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                        ShowMailData(Session["hr_email2"].ToString());
                                    }
                                    if (EmailHOD == "True")
                                    {

                                        subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                        Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, Session["hod_Name2"].ToString(), Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                        ShowMailData(Session["hod_email2"].ToString());
                                    }
                                    clear();

                                    //}
                                }



                                
                            }
                            showAttendenceExpiry();
                            showAttendenceExpiryIND();
                        }
                        if (PriorityHODapr == "1")
                        {

                            //if (holiday == "1")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You cannot mark attendence beacuse this date is holiday');", true);
                            //}
                            if (leavestatus == "6" || leavestatus == "7")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not mark attendance beacuse you have already applied for leave');", true);
                            }
                            else  if (leavestatus == "4" && leavestypess == "3")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not mark attendance beacuse you have already applied for leave');", true);
                            }
                            else
                            { 
                                string hruid = "";
                                con.insert_AttendenceDetail(Session["uid"].ToString(), Session["Fulname"].ToString(), Session["Company"].ToString(), Curdate, txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, "Present", tHour, System.DateTime.Now.ToString("dd-MM-yyyy"), Session["HODLoginPage"].ToString(), hruid, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), Convert.ToInt32(lblCO.Text.Trim()));
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Post the attendance from Post Attendance option');", true);
                                con.DisConnect();
                              
                                show_Attendence();
                                if (EmailHR == "True")
                                {
                                    subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");
                                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, "Kindly Approve my Attendance ", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());


                                    ShowMailData(Session["hr_email2"].ToString());
                                
                                }
                                subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, "Kindly Approve my Attendance ", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                ShowMailData(Session["hod_email2"].ToString());
                                clear();
                            }
                        }
                        if (PriorityHRapr == "1")
                        {

                          
                            if ( leavestatus == "6" || leavestatus == "7")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not mark attendance beacuse you have already applied for leave');", true);
                            }
                           else if (leavestatus == "4" && leavestypess == "3")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not mark attendance beacuse you have already applied for leave');", true);
                            }
                            else
                            {
                                string hodaprid = "";
                                con.insert_AttendenceDetail(Session["uid"].ToString(), Session["Fulname"].ToString(), Session["Company"].ToString(), Curdate, txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, "Present", tHour, System.DateTime.Now.ToString("dd-MM-yyyy"), hodaprid, Session["HRID"].ToString(), Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), Convert.ToInt32(lblCO.Text.Trim()));
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Post the attendance from Post Attendance option');", true);
                                con.DisConnect();
                                
                                show_Attendence();
                                if (EmailHOD == "True")
                                {
                                    subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, "Kindly Approve my Attendance ", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                                    ShowMailData(Session["hod_email2"].ToString());
                                }


                                subject1 = "Application For Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");
                                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, "Kindly Approve my Attendance ", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());


                                ShowMailData(Session["hr_email2"].ToString());
                                clear();
                            }

                        }
                    }
                }
            }
        //}
            showAttendenceExpiry();
            showAttendenceExpiryIND();
    }
    public void sendMailforUpdation()
    {
        if (Blankapr == "1")
        {
            if (EmailHR == "True")
            {

                subject1 = "Application For update Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hr_email2"].ToString());
            }
            if (EmailHOD == "True")
            {
                subject1 = " Application For update Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, "Kindly Approve my Attendance ", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hod_email2"].ToString());
            }
        }
        if (PriorityHODapr == "1")
        {
            if (EmailHR == "True")
            {

                subject1 = "Application For update Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hr_email2"].ToString());
            }
            subject1 = " Application For update Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, "Kindly Approve my Attendance ", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

            ShowMailData(Session["hod_email2"].ToString());
        
        }
        if (PriorityHRapr == "1")
        {
            if (EmailHOD == "True")
            {
                subject1 = " Application For update Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, "Kindly Approve my Attendance ", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hod_email2"].ToString());
        
            }
            subject1 = "Application For update Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy");
            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have fill Attendance of " + Calendar1.SelectedDate.ToString("dd-MM-yyyy") + " , In Time : " + txtFromTime.Text + "   Out Time : " + txtToTime.Text + "   Remarks :  " + txtRemarks.Text + "", Environment.NewLine, "Kindly Approve my Attendance ", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());


            ShowMailData(Session["hr_email2"].ToString());
        }
    }

    public void sendMailforDeletion(string filldate, string fromTime,string Totime,string remarks)
    {
        if (Blankapr == "1")
        {
            if (EmailHR == "True")
            {

                subject1 = "Application For delete Attendance of " + filldate;

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have delete Attendance of " + filldate + " , In Time : " + fromTime + "   Out Time : " + Totime + "   Remarks :  " + remarks + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hr_email2"].ToString());
            }
            if (EmailHOD == "True")
            {

                subject1 = " Application For delete Attendance of " + filldate;

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have delete Attendance of " + filldate + " , In Time : " + fromTime + "   Out Time : " + Totime + "   Remarks :  " + remarks + "", Environment.NewLine, "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hod_email2"].ToString());
            }
        }
        if (PriorityHODapr == "1")
        {

            if (EmailHR == "True")
            
            {
                subject1 = "Application for delete Attendance of " + filldate;

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have delete Attendance of " + filldate + " , In Time : " + fromTime + "   Out Time : " + Totime + "   Remarks :  " + remarks + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hr_email2"].ToString());
            }
            subject1 = " Application for delete attendance of " + filldate;

            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have delete Attendance of " + filldate + " , In Time : " + fromTime + "   Out Time : " + Totime + "   Remarks :  " + remarks + "", Environment.NewLine, "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

            ShowMailData(Session["hod_email2"].ToString());

        }
        if (PriorityHRapr == "1")
        {
            if (EmailHOD == "True")
            {

                subject1 = " Application for delete Attendance of " + filldate;

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have delete Attendance of " + filldate + " , In Time : " + fromTime + "   Out Time : " + Totime + "   Remarks :  " + remarks + "", Environment.NewLine, "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hod_email2"].ToString());
            }

            subject1 = "Application for delete Attendance of " + filldate;
            Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have delete Attendance of " + filldate + " , In Time : " + fromTime + "   Out Time : " + Totime + "   Remarks :  " + remarks + "", Environment.NewLine, "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());


            ShowMailData(Session["hr_email2"].ToString());
        }
    }

    public void showNoofworkingdays()
    { 
         string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
       string  Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
       SqlDataReader dr = Portalcon.count_workingdays(Pay_Daily_Attendence_Detail_tble, txtfromDate.Text, txtTodate.Text, Session["uid"].ToString());
       dr.Read();
       if (dr.HasRows)
       {
           lblworkingdays.Text = dr["Attendance Date"].ToString();
       }
       dr.Close();
       Portalcon.DisConnect();
   
    }


    public void showNoofpresentdays()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        SqlDataReader dr = Portalcon.count_persentdays(Pay_Daily_Attendence_Detail_tble, txtfromDate.Text, txtTodate.Text, Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblpresent.Text = dr["Attendance Date"].ToString();
        }
        dr.Close();
        Portalcon.DisConnect();

    }

    public void showNoofleavedays()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        SqlDataReader dr = Portalcon.count_Leavedays(Pay_Daily_Attendence_Detail_tble, txtfromDate.Text, txtTodate.Text, Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblLeave.Text = dr["Attendance Date"].ToString();
        }
        dr.Close();
        Portalcon.DisConnect();

    }

    protected void btnLeaveViewSearch_Click(object sender, EventArgs e)
    {
        pnlworkingday.Visible=true;
        showNoofworkingdays();
        showNoofpresentdays();
        showNoofleavedays();
        show_Attendence_withDate();
    }
    protected void lnkDelete_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        SqlDataReader dr = con.Show_tbl_attendenceforEdit(id);
        dr.Read();
        string fillDate1 = dr["Atte_Date"].ToString();
        string ToTime1 = dr["ToTime"].ToString();
        string fromTime1 = dr["fromTime"].ToString();
        string remarks1 = dr["Remarks"].ToString();
        dr.Close();
        con.DisConnect();
        con.Delete_AttendenceDetail(id);
        show_Attendence();
      
      sendMailforDeletion(fillDate1, fromTime1, ToTime1, remarks1);

      showAttendenceExpiry();
      showAttendenceExpiryIND();
       
    }
    string Pay_Daily_Attendence_Detail = "";
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {


        if (EmployeeIDday == Session["uid"].ToString())
        {
            if ((e.Day.Date > System.DateTime.Now.AddDays(0)))
            {
                e.Day.IsSelectable = false;
                e.Cell.Font.Bold = false;
                e.Cell.BorderColor = System.Drawing.Color.Pink;
                e.Cell.ForeColor = System.Drawing.Color.Wheat;
            }
            if ((e.Day.Date < System.DateTime.Now.AddDays(-(EmployeeWiseNo_Of_Daysint))))
            {
                e.Day.IsSelectable = false;
                e.Cell.Font.Bold = false;
                e.Cell.BorderColor = System.Drawing.Color.Pink;
                //e.Cell.ForeColor = System.Drawing.Color.Red;
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
        
        }
        else
        {
            if ((e.Day.Date > System.DateTime.Now.AddDays(0)))
            {
                e.Day.IsSelectable = false;
                e.Cell.Font.Bold = false;
                e.Cell.BorderColor = System.Drawing.Color.Pink;
                e.Cell.ForeColor = System.Drawing.Color.Wheat;
            }
            if ((e.Day.Date < System.DateTime.Now.AddDays(-(expiryatt))))
            {
                e.Day.IsSelectable = false;
                e.Cell.Font.Bold = false;
                e.Cell.BorderColor = System.Drawing.Color.Pink;
                //e.Cell.ForeColor = System.Drawing.Color.Red;
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
        }
        DataTable dt = GetDatesofAttendence();
        DateTime eventDate;
        string eventType = String.Empty;
        if ((dt.Rows.Count > 0))
        {
            int i = 0;
            while ((i < dt.Rows.Count))
            {
                eventDate = Convert.ToDateTime(dt.Rows[i]["Atte_Date"]);

                if ((e.Day.Date == eventDate))
                {
                    //e.Cell.ForeColor = System.Drawing.Color.Green;
                    e.Cell.BackColor = System.Drawing.Color.LawnGreen;
                    e.Cell.Font.Bold = true;
                }
                i = (i + 1);
            }
        }
        //Approval Date
        try
        {
            
            DataTable dtapro = GetDatesofAttendenceApproved();
            DateTime eventDateapro;
            string eventTypeapro = String.Empty;
            if ((dtapro.Rows.Count > 0))
            {
                int ij = 0;
                while ((ij < dtapro.Rows.Count))
                {
                    eventDateapro = Convert.ToDateTime(dtapro.Rows[ij]["AtteNdnce_Date"]);

                    if ((e.Day.Date == eventDateapro))
                    {
                        e.Cell.BackColor = System.Drawing.Color.SandyBrown;
                        e.Cell.Font.Bold = true;
                    }
                    ij = (ij + 1);
                }
            }
        }
        catch (Exception)
        { }


        try
        {

            DataTable dtaprolea = GetDatesofLeaveApproved();
            DateTime eventDateaprolea;
            string eventTypeaprolea = String.Empty;
            if ((dtaprolea.Rows.Count > 0))
            {
                int ij = 0;
                while ((ij < dtaprolea.Rows.Count))
                {
                    eventDateaprolea = Convert.ToDateTime(dtaprolea.Rows[ij]["AtteNdnce_Date"]);

                    if ((e.Day.Date == eventDateaprolea))
                    {
                        e.Cell.BackColor = System.Drawing.Color.LightBlue;
                        e.Cell.Font.Bold = true;
                    }
                    ij = (ij + 1);
                }
            }
        }
        catch (Exception)
        { }

        //try
        //{

        //    DataTable dtaprolea = GetDatesofLeaveApprovedHalf();
        //    DateTime eventDateaprolea;
        //    string eventTypeaprolea = String.Empty;
        //    if ((dtaprolea.Rows.Count > 0))
        //    {
        //        int ij = 0;
        //        while ((ij < dtaprolea.Rows.Count))
        //        {
        //            eventDateaprolea = Convert.ToDateTime(dtaprolea.Rows[ij]["AtteNdnce_Date"]);

        //            if ((e.Day.Date == eventDateaprolea))
        //            {
        //                e.Cell.BackColor = System.Drawing.Color.Aqua;
        //                e.Cell.Font.Bold = true;
        //                e.Day.IsSelectable = true;
        //            }
        //            ij = (ij + 1);
        //        }
        //    }
        //}
        //catch (Exception)
        //{ }

    }

    public DataTable GetDatesofAttendence()
    {
        DataTable dt = new DataTable();
        SqlCommand sqlCmd = new SqlCommand("select CONVERT(date, [Atte_Date],103) as Atte_Date from tbl_attendence where Userid='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and [Rejected Approval]='No' and ApprovalStatus!='Rejected' and Status='Present'", con.Con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dt);
        return dt;
    }

    public DataTable GetDatesofAttendenceApproved()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
       string  Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        DataTable dt = new DataTable();
        SqlCommand sqlCmd = new SqlCommand("select CONVERT(date, [Attendance Date],103) as AtteNdnce_Date from " + Pay_Daily_Attendence_Detail_tble + " where [Attendance Marked]='1' and Status='1' and [Employee Code]='" + Session["uid"].ToString() + "'", Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        sqlDa.Fill(dt);
        return dt;
    }

    public DataTable GetDatesofLeaveApproved()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        DataTable dt = new DataTable();
        SqlCommand sqlCmd = new SqlCommand("select CONVERT(date, [Attendance Date],103) as AtteNdnce_Date from " + Pay_Daily_Attendence_Detail_tble + " where [Applied Leave]='1'  and Status!='1'  and [Employee Code]='" + Session["uid"].ToString() + "'", Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        sqlDa.Fill(dt);
        return dt;
    }

    public DataTable GetDatesofLeaveApprovedHalf()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        DataTable dt = new DataTable();
        SqlCommand sqlCmd = new SqlCommand("select CONVERT(date, [Attendance Date],103) as AtteNdnce_Date from " + Pay_Daily_Attendence_Detail_tble + " where [Applied Leave]='1' and [Leave Type]='2' and [Employee Code]='" + Session["uid"].ToString() + "'", Portalcon.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        sqlDa.Fill(dt);
        return dt;
    }

    int expiryatt; string EmployeeIDday = ""; int EmployeeWiseNo_Of_Daysint; string ToTimeAll = ""; string FromTimeALL = ""; string optForFromTimeTotimeAll = ""; string Card_Attendance_changingAll = ""; string Card_Attendance_changingINDV = "";
    string In_time_Out_TimeRquired = "";
    public void showAttendenceExpiry()
    {
        SqlDataReader dr = con.Show_tbl_Attendence_Expiry(Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
          string  attenexpprev1 = dr["No_of_Days"].ToString();
          expiryatt = Convert.ToInt32(attenexpprev1);
          FromTimeALL = dr["From_Time"].ToString();
          ToTimeAll = dr["To_Time"].ToString();
          txtFromTime.Text = dr["From_Time"].ToString();
          txtToTime.Text = dr["To_Time"].ToString();
          optForFromTimeTotimeAll = dr["Option_fromTime_toime_All"].ToString();
          In_time_Out_TimeRquired = dr["In_time_Out_TimeRquired"].ToString();
          if (optForFromTimeTotimeAll == "Enable")
          {
              txtFromTime.Enabled = true;
              txtToTime.Enabled = true;
          }
          if (optForFromTimeTotimeAll == "Disable")
          {
              txtFromTime.Enabled = false;
              txtToTime.Enabled = false;
          }


        }
        dr.Read();
        con.DisConnect();
    }


   string optForFromTimeTotimeind = "";
   string In_time_Out_TimeRquiredind = "";
    public void showAttendenceExpiryIND()
    {
        SqlDataReader dr = con.Show_tbl_Attendence_ExpiryIND(Session["Company"].ToString(),Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string EmployeeWiseNo_Of_Days = dr["Duration"].ToString();
            if (EmployeeWiseNo_Of_Days == "")
            {
                EmployeeWiseNo_Of_Days = "0";
            }
            EmployeeWiseNo_Of_Daysint = Convert.ToInt32(EmployeeWiseNo_Of_Days);
            EmployeeIDday = dr["EmployeeID"].ToString();

            txtFromTime.Text = dr["From_Time"].ToString();
            txtToTime.Text = dr["To_Time"].ToString();
            optForFromTimeTotimeind = dr["Option_fromTime_toime_Individual"].ToString();
            In_time_Out_TimeRquiredind = dr["In_time_Out_TimeRquired"].ToString();
            if (optForFromTimeTotimeind == "Enable")
            {
                txtFromTime.Enabled = true;
                txtToTime.Enabled = true;
            }
            if (optForFromTimeTotimeind == "Disable")
            {
                txtFromTime.Enabled = false;
                txtToTime.Enabled = false;
            }
        }
        else
        {

            txtFromTime.Text = FromTimeALL;
            txtToTime.Text = ToTimeAll;
            optForFromTimeTotimeind =optForFromTimeTotimeAll;
        }

        dr.Read();
        con.DisConnect();
    }




    public void showAttendenceExpiryAdd()
    {
        SqlDataReader dr = con.Show_tbl_Attendence_Expiry(Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            //string attenexpprev1 = dr["No_of_Days"].ToString();
            //expiryatt = Convert.ToInt32(attenexpprev1);
            //FromTimeALL = dr["From_Time"].ToString();
            //ToTimeAll = dr["To_Time"].ToString();
            //txtFromTime.Text = dr["From_Time"].ToString();
            //txtToTime.Text = dr["To_Time"].ToString();
            optForFromTimeTotimeAll = dr["Option_fromTime_toime_All"].ToString();
            In_time_Out_TimeRquired = dr["In_time_Out_TimeRquired"].ToString();
            Card_Attendance_changingAll = dr["Card_Attendance_changing"].ToString();
            //if (optForFromTimeTotimeAll == "Enable")
            //{
            //    txtFromTime.Enabled = true;
            //    txtToTime.Enabled = true;
            //}
            //if (optForFromTimeTotimeAll == "Disable")
            //{
            //    txtFromTime.Enabled = false;
            //    txtToTime.Enabled = false;
            //}


        }
        dr.Read();
        con.DisConnect();
    }


    string EmployeeIDIndv = "";
    public void showAttendenceExpiryINDforAdd()
    {
        SqlDataReader dr = con.Show_tbl_Attendence_ExpiryIND(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            optForFromTimeTotimeind = dr["Option_fromTime_toime_Individual"].ToString();
            In_time_Out_TimeRquiredind = dr["In_time_Out_TimeRquired"].ToString();

            EmployeeIDIndv = dr["EmployeeID"].ToString();
            Card_Attendance_changingINDV = dr["Card_Attendance_changing"].ToString();
            
        }
       dr.Read();
        con.DisConnect();
    }


    string leavestatus = "";
    string leavestypess = "";
    public void LeaveApprovalforAttendence()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        Pay_Daily_Attendence_Detail = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        SqlDataReader dr = Portalcon.Show_LeaveforAttendence(Pay_Daily_Attendence_Detail, Calendar1.SelectedDate.ToString("yyyy-MM-dd"),Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            //leavecheck = dr["Applied Leave"].ToString();
            leavestatus = dr["Status"].ToString();
            leavestypess = dr["Leave Type"].ToString();
            if (leavestatus == "6" || leavestatus == "7")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not mark attendance beacuse you have already applied for leave');", true);
            }
            if (leavestatus == "4" && leavestypess == "3")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not mark attendance beacuse you have already applied for leave');", true);
            }
        }
        dr.Close();
        Portalcon.DisConnect();

    }


    public void holidayList()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        Pay_Daily_Attendence_Detail = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        SqlDataReader dr = Portalcon.Show_HolidayList(Pay_Daily_Attendence_Detail, Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
        dr.Read();
        if (dr.HasRows)
        {
            holiday = dr["Holiday"].ToString();
            offDay = dr["Off Day"].ToString();
            if (holiday == "1")
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You cannot mark attendance beacuse this date is holiday');", true);
                lblCO.Text = "1";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Selected date is holiday');", true);
            }
           else if (offDay == "1")
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Are you sure to enter present time  because this days is off days ');", true);
                lblCO.Text = "1";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Selected date is holiday');", true);
            }
            else
            {
                lblCO.Text = "0";
            }
        }
        dr.Close();
        Portalcon.DisConnect();
    
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        show_Attendence();
        showAttendenceExpiry();
        showAttendenceExpiryIND();
        //showAttendenceExpiry();
        //showAttendenceExpiryIND();
        txtSelectedDate.Text = Calendar1.SelectedDate.ToString("dd-MM-yyyy");
        holidayList();
        LeaveApprovalforAttendence();
    }
    protected void grdAttendence_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAttendence.PageIndex = e.NewPageIndex;
        show_Attendence();
        showAttendenceExpiry();
        showAttendenceExpiryIND();
    }
    protected void grdViewdetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewdetail.PageIndex = e.NewPageIndex;
        show_Attendence_withDate();
        showAttendenceExpiry();
        showAttendenceExpiryIND();
    }
   
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        showAttendenceExpiryAdd();
        showAttendenceExpiryINDforAdd();
        string AttendanceType = "";
        Session["idedit"] = e.CommandArgument.ToString();
        SqlDataReader dr = con.Show_tbl_attendenceforEdit(Session["idedit"].ToString());
        dr.Read();
        txtFromTime.Text = dr["fromTime"].ToString();
        txtToTime.Text = dr["ToTime"].ToString();
        txtJobLocation.Text = dr["Job_location"].ToString();
        txtRemarks.Text = dr["Remarks"].ToString();
       
        lblDateForUpdate.Text = dr["Atte_Date"].ToString();
        txtSelectedDate.Text = dr["Atte_Date"].ToString();
        AttendanceType = dr["Attendance Type"].ToString();
        Session["AttendanceTypeEdit"] = AttendanceType;
        if (Session["AttendanceTypeEdit"].ToString() == "Card")
        {
            if (Card_Attendance_changingAll == "Yes")
            {
                txtFromTime.Enabled = true;
                txtToTime.Enabled = true;
            
            }
            if (Card_Attendance_changingAll == "No")
            {
                txtFromTime.Enabled = false;
                txtToTime.Enabled = false;

            }
            if (Card_Attendance_changingINDV == "Yes")
            {
                txtFromTime.Enabled = true;
                txtToTime.Enabled = true;

            }
            if (Card_Attendance_changingINDV == "No")
            {
                txtFromTime.Enabled = false;
                txtToTime.Enabled = false;

            }
        }
        dr.Close();
        con.DisConnect();
        btnUpdate.Visible = true;
        btnSave.Visible = false;
        btnClose.Visible = true;
        if (Session["AttendanceTypeEdit"].ToString() == "Manual")
        {
            showAttendenceExpiry();
            showAttendenceExpiryIND();

        }
       
        Calendar1.Enabled = false;
    }

    

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ToTalWorkDuration();
        
        showAttendenceExpiryAdd();
        showAttendenceExpiryINDforAdd();
        if (Session["AttendanceTypeEdit"].ToString() == "Manual")
        {
            if (Session["uid"].ToString() == EmployeeIDIndv && optForFromTimeTotimeind == "Enable" && In_time_Out_TimeRquiredind == "Yes")
            {
                if (txtFromTime.Text == "" || txtToTime.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please fill in time and out time ');", true);

                }
                else
                {


                    con.Update_AttendenceToTime(txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, tupdateHour, System.DateTime.Now.ToString(), Session["idedit"].ToString());
                    con.DisConnect();
                    show_Attendence();
                    btnClose.Visible = false;
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    sendMailforUpdation();
                }
            }
            if (Session["uid"].ToString() != EmployeeIDIndv && optForFromTimeTotimeAll == "Enable" && In_time_Out_TimeRquired == "Yes")
            {
                if (txtFromTime.Text == "" || txtToTime.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please fill in time and out time ');", true);

                }
                else
                {

                    con.Update_AttendenceToTime(txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, tupdateHour, System.DateTime.Now.ToString(), Session["idedit"].ToString());
                    con.DisConnect();
                    show_Attendence();
                    btnClose.Visible = false;
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    sendMailforUpdation();
                }
            }
            if (Session["uid"].ToString() != EmployeeIDIndv && optForFromTimeTotimeAll == "Disable" && In_time_Out_TimeRquired == "Yes")
            {
                con.Update_AttendenceToTime(txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, tupdateHour, System.DateTime.Now.ToString(), Session["idedit"].ToString());
                con.DisConnect();
                show_Attendence();
                btnClose.Visible = false;
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                sendMailforUpdation();

            }

            if (Session["uid"].ToString() == EmployeeIDIndv && optForFromTimeTotimeind == "Disable" && In_time_Out_TimeRquiredind == "Yes")
            {
                con.Update_AttendenceToTime(txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, tupdateHour, System.DateTime.Now.ToString(), Session["idedit"].ToString());
                con.DisConnect();
                show_Attendence();
                btnClose.Visible = false;
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                sendMailforUpdation();
            }
            if (Session["uid"].ToString() == EmployeeIDIndv && optForFromTimeTotimeind == "Disable" && In_time_Out_TimeRquiredind == "No")
            {
                con.Update_AttendenceToTime(txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, tupdateHour, System.DateTime.Now.ToString(), Session["idedit"].ToString());
                con.DisConnect();
                show_Attendence();
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnClose.Visible = false;
                sendMailforUpdation();
            }

            if (Session["uid"].ToString() != EmployeeIDIndv && optForFromTimeTotimeAll == "Disable" && In_time_Out_TimeRquired == "No")
            {
                con.Update_AttendenceToTime(txtFromTime.Text, txtToTime.Text, txtJobLocation.Text, txtRemarks.Text, tupdateHour, System.DateTime.Now.ToString(), Session["idedit"].ToString());
                con.DisConnect();
                show_Attendence();
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnClose.Visible = false;
                sendMailforUpdation();

            }

           
        }

        if (Session["AttendanceTypeEdit"].ToString() == "Card")
        {
            if (txtFromTime.Text == "" || txtToTime.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please fill in time and out time ');", true);

            }
            else
            {


                con.Update_AttendenceToTimewithCardYes(txtFromTime.Text.Trim(), txtToTime.Text.Trim(), txtJobLocation.Text.Trim(), txtRemarks.Text.Trim(), tupdateHour, System.DateTime.Now.ToString(), Session["idedit"].ToString(), txtFromTime.Text.Trim(), txtToTime.Text.Trim());
                con.DisConnect();
                show_Attendence();
                btnClose.Visible = false;
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                sendMailforUpdation();
            }
        }
        showAttendenceExpiry();
        showAttendenceExpiryIND();
        Calendar1.Enabled = true;
       // }
    }
  

    protected void clndview_DayRender(object sender, DayRenderEventArgs e)
    {

        if (EmployeeIDday == Session["uid"].ToString())
        {
            if ((e.Day.Date > System.DateTime.Now.AddDays(0)))
            {
                e.Day.IsSelectable = false;
                e.Cell.Font.Bold = false;
                e.Cell.BorderColor = System.Drawing.Color.Pink;
                e.Cell.ForeColor = System.Drawing.Color.Wheat;
            }
            if ((e.Day.Date < System.DateTime.Now.AddDays(-(EmployeeWiseNo_Of_Daysint))))
            {
                e.Day.IsSelectable = false;
                e.Cell.Font.Bold = false;
                e.Cell.BorderColor = System.Drawing.Color.Pink;
                //e.Cell.ForeColor = System.Drawing.Color.Red;
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }

        }
        else
        {
            if ((e.Day.Date > System.DateTime.Now.AddDays(0)))
            {
                e.Day.IsSelectable = false;
                e.Cell.Font.Bold = false;
                e.Cell.BorderColor = System.Drawing.Color.Pink;
                e.Cell.ForeColor = System.Drawing.Color.Wheat;
            }
            if ((e.Day.Date < System.DateTime.Now.AddDays(-(expiryatt))))
            {
                e.Day.IsSelectable = false;
                e.Cell.Font.Bold = false;
                e.Cell.BorderColor = System.Drawing.Color.Pink;
                //e.Cell.ForeColor = System.Drawing.Color.Red;

                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
        }
        DataTable dt = GetDatesofAttendence();
        DateTime eventDate;
        string eventType = String.Empty;
        if ((dt.Rows.Count > 0))
        {
            int i = 0;
            while ((i < dt.Rows.Count))
            {
                eventDate = Convert.ToDateTime(dt.Rows[i]["Atte_Date"]);

                if ((e.Day.Date == eventDate))
                {
                    //e.Cell.ForeColor = System.Drawing.Color.Green;
                    e.Cell.BackColor = System.Drawing.Color.LawnGreen;
                    e.Cell.Font.Bold = true;
                }
                i = (i + 1);
            }
        }
        //Approval Date
        try
        {

            DataTable dtapro = GetDatesofAttendenceApproved();
            DateTime eventDateapro;
            string eventTypeapro = String.Empty;
            if ((dtapro.Rows.Count > 0))
            {
                int ij = 0;
                while ((ij < dtapro.Rows.Count))
                {
                    eventDateapro = Convert.ToDateTime(dtapro.Rows[ij]["AtteNdnce_Date"]);

                    if ((e.Day.Date == eventDateapro))
                    {
                        e.Cell.BackColor = System.Drawing.Color.SandyBrown;
                        e.Cell.Font.Bold = true;
                    }
                    ij = (ij + 1);
                }
            }
        }
        catch (Exception)
        { }


        try
        {

            DataTable dtaprolea = GetDatesofLeaveApproved();
            DateTime eventDateaprolea;
            string eventTypeaprolea = String.Empty;
            if ((dtaprolea.Rows.Count > 0))
            {
                int ij = 0;
                while ((ij < dtaprolea.Rows.Count))
                {
                    eventDateaprolea = Convert.ToDateTime(dtaprolea.Rows[ij]["AtteNdnce_Date"]);

                    if ((e.Day.Date == eventDateaprolea))
                    {
                        e.Cell.BackColor = System.Drawing.Color.LightBlue;
                        e.Cell.Font.Bold = true;
                    }
                    ij = (ij + 1);
                }
            }
        }
        catch (Exception)
        { }

        //try
        //{

        //    DataTable dtaprolea = GetDatesofLeaveApprovedHalf();
        //    DateTime eventDateaprolea;
        //    string eventTypeaprolea = String.Empty;
        //    if ((dtaprolea.Rows.Count > 0))
        //    {
        //        int ij = 0;
        //        while ((ij < dtaprolea.Rows.Count))
        //        {
        //            eventDateaprolea = Convert.ToDateTime(dtaprolea.Rows[ij]["AtteNdnce_Date"]);

        //            if ((e.Day.Date == eventDateaprolea))
        //            {
        //                e.Cell.BackColor = System.Drawing.Color.Aqua;
        //                e.Cell.Font.Bold = true;
        //                e.Day.IsSelectable = true;
        //            }
        //            ij = (ij + 1);
        //        }
        //    }
        //}
        //catch (Exception)
        //{ }


    }
    protected void grdViewdetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblApprovalStatusgrid = (Label)e.Row.FindControl("lblApprovalStatusgrid");
            Label lblapprovalstatusgridAttend = (Label)e.Row.FindControl("lblapprovalstatusgridAttend");


            if (lblApprovalStatusgrid.Text == "Approved")
            {

                lblApprovalStatusgrid.Text = "Posted";
                lblApprovalStatusgrid.ForeColor = System.Drawing.Color.SandyBrown;

                lblapprovalstatusgridAttend.Text = "Present";

            }
            else
            {
                lblapprovalstatusgridAttend.Text = "Present";
            }
            
        }
    }


    protected void rdEmployeeID_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = true;
        pnlDate.Visible = false;
        btnSearch.Visible = true;
        txtSearchName.Text = "";
        txtfromDatePost.Text = "";
        txtTodatePost.Text = "";
        //btnreject.Visible = false;
        btnApprove.Visible = false;
        grdViewApproval.Visible = false;
       
        btnuncheked.Visible = false;
        btnselectchecked.Visible = false;
        //ModalPopupExtender1.Hide();

    }
    protected void rdEmployeeName_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = true;
        pnlDate.Visible = false;
        btnSearch.Visible = true;
        txtSearchName.Text = "";
        txtfromDatePost.Text = "";
        txtTodatePost.Text = "";
        grdViewApproval.Visible = false;
      
        //btnreject.Visible = false;
        btnApprove.Visible = false;
        btnuncheked.Visible = false;
        btnselectchecked.Visible = false;
        //ModalPopupExtender1.Hide();
    }
    protected void rdDatewise_CheckedChanged(object sender, EventArgs e)
    {
        pnlEmployeeidName.Visible = false;
        pnlDate.Visible = true;
        btnSearch.Visible = true;
        txtSearchName.Text = "";
        txtfromDatePost.Text = "";
        txtTodatePost.Text = "";
        //btnreject.Visible = false;
        btnApprove.Visible = false;
        grdViewApproval.Visible = false;
        
        btnuncheked.Visible = false;
        btnselectchecked.Visible = false;
        //ModalPopupExtender1.Hide();
    }


    string HRHODsame = "";
    string forHRisHOD = "";
    public void AttendenceApproval_Search()
    {
        if (rdDatewise.Checked == true)
        {



            if (Blankapr == "1")
            {
                SqlDataReader odr = con.Attendce_Approval_Detail_withdateUser(Session["uid"].ToString(), txtfromDatePost.Text, txtTodatePost.Text, Session["Company"].ToString());
                DataTable Dt = new DataTable();
                Dt.Load(odr);
                grdViewApproval.DataSource = Dt;
                grdViewApproval.DataBind();
                odr.Close();
                con.DisConnect();

                SqlDataReader dr = con.Attendce_Approval_Detail_withdateUser(Session["uid"].ToString(), txtfromDatePost.Text, txtTodatePost.Text, Session["Company"].ToString());
                dr.Read();
                if (dr.HasRows)
                {
                    btnApprove.Visible = true;
                    // btnreject.Visible = true;
                    btnselectchecked.Visible = true;
                    btnuncheked.Visible = false;
                }
                else
                {
                    btnApprove.Visible = false;
                    //  btnreject.Visible = false;
                    btnselectchecked.Visible = false;
                    btnuncheked.Visible = false;
                }
                dr.Close();
                con.DisConnect();

            }
            //if (PriorityHODapr == "1")
            //{
            //    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
            //    {
            //        SqlDataReader odr = con.Attendce_Approval_Detail_withdateHR(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
            //        DataTable Dt = new DataTable();
            //        Dt.Load(odr);
            //        grdViewApproval.DataSource = Dt;
            //        grdViewApproval.DataBind();
            //        odr.Close();
            //        con.DisConnect();

            //        SqlDataReader dr = con.Attendce_Approval_Detail_withdateHR(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
            //        dr.Read();
            //        if (dr.HasRows)
            //        {
            //            btnApprove.Visible = true;
            //           // btnreject.Visible = true;
            //            btnselectchecked.Visible = true;
            //            btnuncheked.Visible = false;
            //        }
            //        else
            //        {
            //            btnApprove.Visible = false;
            //           // btnreject.Visible = false;
            //            btnselectchecked.Visible = false;
            //            btnuncheked.Visible = false;
            //        }
            //        dr.Close();
            //        con.DisConnect();
            //    }
            //    else
            //    {

            //        SqlDataReader odr = con.Attendce_Approval_Detail_withdate(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
            //        DataTable Dt = new DataTable();
            //        Dt.Load(odr);
            //        grdViewApproval.DataSource = Dt;
            //        grdViewApproval.DataBind();
            //        odr.Close();
            //        con.DisConnect();

            //        SqlDataReader dr = con.Attendce_Approval_Detail_withdate(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
            //        dr.Read();
            //        if (dr.HasRows)
            //        {
            //            btnApprove.Visible = true;
            //            //btnreject.Visible = true;
            //            btnselectchecked.Visible = true;
            //            btnuncheked.Visible = false;
            //        }
            //        else
            //        {
            //            btnApprove.Visible = false;
            //         //   btnreject.Visible = false;
            //            btnselectchecked.Visible = false;
            //            btnuncheked.Visible = false;
            //        }
            //        dr.Close();
            //        con.DisConnect();
            //    }

            //}
            //if (PriorityHRapr == "1")
            //{
            //    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
            //    {
            //        SqlDataReader odr = con.Attendce_Approval_Detail_withdateHR(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
            //        DataTable Dt = new DataTable();
            //        Dt.Load(odr);
            //        grdViewApproval.DataSource = Dt;
            //        grdViewApproval.DataBind();
            //        odr.Close();
            //        con.DisConnect();

            //        SqlDataReader dr = con.Attendce_Approval_Detail_withdateHR(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
            //        dr.Read();
            //        if (dr.HasRows)
            //        {
            //            btnApprove.Visible = true;
            //           // btnreject.Visible = true;
            //            btnselectchecked.Visible = true;
            //            btnuncheked.Visible = false;
            //        }
            //        else
            //        {
            //            btnApprove.Visible = false;
            //         //   btnreject.Visible = false;
            //            btnselectchecked.Visible = false;
            //            btnuncheked.Visible = false;
            //        }
            //        dr.Close();
            //        con.DisConnect();

            //    }
            //    else
            //    {
            //        SqlDataReader odr = con.Attendce_Approval_Detail_withdate(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
            //        DataTable Dt = new DataTable();
            //        Dt.Load(odr);
            //        grdViewApproval.DataSource = Dt;
            //        grdViewApproval.DataBind();
            //        odr.Close();
            //        con.DisConnect();

            //        SqlDataReader dr = con.Attendce_Approval_Detail_withdate(Session["uid"].ToString(), txtfromDate.Text, txtTodate.Text, Session["Company"].ToString());
            //        dr.Read();
            //        if (dr.HasRows)
            //        {
            //            btnApprove.Visible = true;
            //          //  btnreject.Visible = true;
            //            btnselectchecked.Visible = true;
            //            btnuncheked.Visible = false;
            //        }
            //        else
            //        {
            //            btnApprove.Visible = false;
            //            //btnreject.Visible = false;
            //            btnselectchecked.Visible = false;
            //            btnuncheked.Visible = false;
            //        }
            //        dr.Close();
            //        con.DisConnect();

            //    }
            //}
            // }

        }

        //if (rdEmployeeID.Checked == true)
        //{
            
        //    if (Blankapr == "1")
        //    {
        //        SqlDataReader odr = con.Attendce_Approval_Detail_withUserIDUser(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //        DataTable Dt = new DataTable();
        //        Dt.Load(odr);
        //        grdViewApproval.DataSource = Dt;
        //        grdViewApproval.DataBind();
        //        odr.Close();
        //        con.DisConnect();

        //        SqlDataReader dr = con.Attendce_Approval_Detail_withUserIDUser(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //        dr.Read();
        //        if (dr.HasRows)
        //        {
        //            btnApprove.Visible = true;
        //            //btnreject.Visible = true;
        //            btnselectchecked.Visible = true;
        //            btnuncheked.Visible = false;
        //        }
        //        else
        //        {
        //            btnApprove.Visible = false;
        //            //btnreject.Visible = false;
        //            btnselectchecked.Visible = false;
        //            btnuncheked.Visible = false;
        //        }
        //        dr.Close();
        //        con.DisConnect();

        //    }

        //    //if (PriorityHODapr == "1")
        //    //{
        //    //    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
        //    //    {
        //    //        SqlDataReader odr = con.Attendce_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        DataTable Dt = new DataTable();
        //    //        Dt.Load(odr);
        //    //        grdViewApproval.DataSource = Dt;
        //    //        grdViewApproval.DataBind();
        //    //        odr.Close();
        //    //        con.DisConnect();

        //    //        SqlDataReader dr = con.Attendce_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        dr.Read();
        //    //        if (dr.HasRows)
        //    //        {
        //    //            btnApprove.Visible = true;
        //    //            //btnreject.Visible = true;
        //    //            btnselectchecked.Visible = true;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        else
        //    //        {
        //    //            btnApprove.Visible = false;
        //    //            //btnreject.Visible = false;
        //    //            btnselectchecked.Visible = false;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        dr.Close();
        //    //        con.DisConnect();


        //    //    }
        //    //    else
        //    //    {
        //    //        SqlDataReader odr = con.Attendce_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        DataTable Dt = new DataTable();
        //    //        Dt.Load(odr);
        //    //        grdViewApproval.DataSource = Dt;
        //    //        grdViewApproval.DataBind();
        //    //        odr.Close();
        //    //        con.DisConnect();

        //    //        SqlDataReader dr = con.Attendce_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        dr.Read();
        //    //        if (dr.HasRows)
        //    //        {
        //    //            btnApprove.Visible = true;
        //    //            //btnreject.Visible = true;
        //    //            btnselectchecked.Visible = true;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        else
        //    //        {
        //    //            btnApprove.Visible = false;
        //    //            //btnreject.Visible = false;
        //    //            btnselectchecked.Visible = false;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        dr.Close();
        //    //        con.DisConnect();
        //    //    }
        //    //}
        //    //if (PriorityHRapr == "1")
        //    //{
        //    //    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
        //    //    {
        //    //        SqlDataReader odr = con.Attendce_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        DataTable Dt = new DataTable();
        //    //        Dt.Load(odr);
        //    //        grdViewApproval.DataSource = Dt;
        //    //        grdViewApproval.DataBind();
        //    //        odr.Close();
        //    //        con.DisConnect();

        //    //        SqlDataReader dr = con.Attendce_Approval_Detail_withUserIDHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        dr.Read();
        //    //        if (dr.HasRows)
        //    //        {
        //    //            btnApprove.Visible = true;
        //    //            //btnreject.Visible = true;
        //    //            btnselectchecked.Visible = true;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        else
        //    //        {
        //    //            btnApprove.Visible = false;
        //    //            //btnreject.Visible = false;
        //    //            btnselectchecked.Visible = false;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        dr.Close();
        //    //        con.DisConnect();
        //    //    }
        //    //    else
        //    //    {
        //    //        SqlDataReader odr = con.Attendce_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        DataTable Dt = new DataTable();
        //    //        Dt.Load(odr);
        //    //        grdViewApproval.DataSource = Dt;
        //    //        grdViewApproval.DataBind();
        //    //        odr.Close();
        //    //        con.DisConnect();

        //    //        SqlDataReader dr = con.Attendce_Approval_Detail_withUserID(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        dr.Read();
        //    //        if (dr.HasRows)
        //    //        {
        //    //            btnApprove.Visible = true;
        //    //            //btnreject.Visible = true;
        //    //            btnselectchecked.Visible = true;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        else
        //    //        {
        //    //            btnApprove.Visible = false;
        //    //            //btnreject.Visible = false;
        //    //            btnselectchecked.Visible = false;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        dr.Close();
        //    //        con.DisConnect();

        //    //    }


        //    //}
        //    // }


        //}

        //if (rdEmployeeName.Checked == true)
        //{
        //    //if (Session["UserType"].ToString() == "1")
        //    //{
        //    //    SqlDataReader odr = con.Attendce_Approval_Detail_withUserNameUserAdmin( Session["Company"].ToString(), txtSearchName.Text);
        //    //    DataTable Dt = new DataTable();
        //    //    Dt.Load(odr);
        //    //    grdViewApproval.DataSource = Dt;
        //    //    grdViewApproval.DataBind();
        //    //    odr.Close();
        //    //    con.DisConnect();



        //    //}
        //    //else
        //    //{

        //    if (Blankapr == "1")
        //    {
        //        SqlDataReader odr = con.Attendce_Approval_Detail_withUserNameUser(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //        DataTable Dt = new DataTable();
        //        Dt.Load(odr);
        //        grdViewApproval.DataSource = Dt;
        //        grdViewApproval.DataBind();
        //        odr.Close();
        //        con.DisConnect();

        //        SqlDataReader dr = con.Attendce_Approval_Detail_withUserNameUser(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //        dr.Read();
        //        if (dr.HasRows)
        //        {
        //            btnApprove.Visible = true;
        //            //btnreject.Visible = true;
        //            btnselectchecked.Visible = true;
        //            btnuncheked.Visible = false;
        //        }
        //        else
        //        {
        //            btnApprove.Visible = false;
        //            //btnreject.Visible = false;
        //            btnselectchecked.Visible = false;
        //            btnuncheked.Visible = false;
        //        }
        //        dr.Close();
        //        con.DisConnect();

        //    }
        //    //if (PriorityHODapr == "1")
        //    //{
        //    //    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
        //    //    {
        //    //        SqlDataReader odr = con.Attendce_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        DataTable Dt = new DataTable();
        //    //        Dt.Load(odr);
        //    //        grdViewApproval.DataSource = Dt;
        //    //        grdViewApproval.DataBind();
        //    //        odr.Close();
        //    //        con.DisConnect();

        //    //        SqlDataReader dr = con.Attendce_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        dr.Read();
        //    //        if (dr.HasRows)
        //    //        {
        //    //            btnApprove.Visible = true;
        //    //            //btnreject.Visible = true;
        //    //            btnselectchecked.Visible = true;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        else
        //    //        {
        //    //            btnApprove.Visible = false;
        //    //            btnreject.Visible = false;
        //    //            btnselectchecked.Visible = false;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        dr.Close();
        //    //        con.DisConnect();

        //    //    }
        //    //    else
        //    //    {

        //    //        SqlDataReader odr = con.Attendce_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        DataTable Dt = new DataTable();
        //    //        Dt.Load(odr);
        //    //        grdViewApproval.DataSource = Dt;
        //    //        grdViewApproval.DataBind();
        //    //        odr.Close();
        //    //        con.DisConnect();

        //    //        SqlDataReader dr = con.Attendce_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        dr.Read();
        //    //        if (dr.HasRows)
        //    //        {
        //    //            btnApprove.Visible = true;
        //    //            btnreject.Visible = true;
        //    //            btnselectchecked.Visible = true;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        else
        //    //        {
        //    //            btnApprove.Visible = false;
        //    //            btnreject.Visible = false;
        //    //            btnselectchecked.Visible = false;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        dr.Close();
        //    //        con.DisConnect();
        //    //    }
        //    //}
        //    //if (PriorityHRapr == "1")
        //    //{
        //    //    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
        //    //    {
        //    //        SqlDataReader odr = con.Attendce_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        DataTable Dt = new DataTable();
        //    //        Dt.Load(odr);
        //    //        grdViewApproval.DataSource = Dt;
        //    //        grdViewApproval.DataBind();
        //    //        odr.Close();
        //    //        con.DisConnect();

        //    //        SqlDataReader dr = con.Attendce_Approval_Detail_withUserNameHR(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        dr.Read();
        //    //        if (dr.HasRows)
        //    //        {
        //    //            btnApprove.Visible = true;
        //    //            btnreject.Visible = true;
        //    //            btnselectchecked.Visible = true;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        else
        //    //        {
        //    //            btnApprove.Visible = false;
        //    //            btnreject.Visible = false;
        //    //            btnselectchecked.Visible = false;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        dr.Close();
        //    //        con.DisConnect();
        //    //    }
        //    //    else
        //    //    {
        //    //        SqlDataReader odr = con.Attendce_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        DataTable Dt = new DataTable();
        //    //        Dt.Load(odr);
        //    //        grdViewApproval.DataSource = Dt;
        //    //        grdViewApproval.DataBind();
        //    //        odr.Close();
        //    //        con.DisConnect();

        //    //        SqlDataReader dr = con.Attendce_Approval_Detail_withUserName(Session["uid"].ToString(), Session["Company"].ToString(), txtSearchName.Text);
        //    //        dr.Read();
        //    //        if (dr.HasRows)
        //    //        {
        //    //            btnApprove.Visible = true;
        //    //            btnreject.Visible = true;
        //    //            btnselectchecked.Visible = true;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        else
        //    //        {
        //    //            btnApprove.Visible = false;
        //    //            btnreject.Visible = false;
        //    //            btnselectchecked.Visible = false;
        //    //            btnuncheked.Visible = false;
        //    //        }
        //    //        dr.Close();
        //    //        con.DisConnect();

        //    //    }

        //    //}
        //    //  }


        //}

        if (CHKAllPending.Checked == true)
        {

            //if (Session["UserType"].ToString() == "1")
            //{
            //    SqlDataReader odr = con.Attendce_Approval_Detail_withALLPendingBlankAdmin( Session["Company"].ToString());
            //    DataTable Dt = new DataTable();
            //    Dt.Load(odr);
            //    grdViewApproval.DataSource = Dt;
            //    grdViewApproval.DataBind();
            //    odr.Close();
            //    con.DisConnect();



            //}
            //else
            //{
            if (Blankapr == "1")
            {
                SqlDataReader odr = con.Attendce_Approval_Detail_withALLPendingBlank(Session["uid"].ToString(), Session["Company"].ToString());
                DataTable Dt = new DataTable();
                Dt.Load(odr);
                grdViewApproval.DataSource = Dt;
                grdViewApproval.DataBind();
                odr.Close();
                con.DisConnect();

                SqlDataReader dr = con.Attendce_Approval_Detail_withALLPendingBlank(Session["uid"].ToString(), Session["Company"].ToString());
                dr.Read();
                if (dr.HasRows)
                {
                    btnApprove.Visible = true;
                    //btnreject.Visible = true;
                    btnselectchecked.Visible = true;
                    btnuncheked.Visible = false;
                }
                else
                {
                    btnApprove.Visible = false;
                    //btnreject.Visible = false;
                    btnselectchecked.Visible = false;
                    btnuncheked.Visible = false;
                }
                dr.Close();
                con.DisConnect();

            }
            //if (PriorityHODapr == "1")
            //{
            //    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
            //    {
            //        SqlDataReader odr = con.Attendce_Approval_Detail_withPriorityHRAllPending(Session["uid"].ToString(), Session["Company"].ToString());
            //        DataTable Dt = new DataTable();
            //        Dt.Load(odr);
            //        grdViewApproval.DataSource = Dt;
            //        grdViewApproval.DataBind();
            //        odr.Close();
            //        con.DisConnect();

            //        SqlDataReader dr = con.Attendce_Approval_Detail_withPriorityHRAllPending(Session["uid"].ToString(), Session["Company"].ToString());
            //        dr.Read();
            //        if (dr.HasRows)
            //        {
            //            btnApprove.Visible = true;
            //            btnreject.Visible = true;
            //            btnselectchecked.Visible = true;
            //            btnuncheked.Visible = false;
            //        }
            //        else
            //        {
            //            btnApprove.Visible = false;
            //            btnreject.Visible = false;
            //            btnselectchecked.Visible = false;
            //            btnuncheked.Visible = false;
            //        }
            //        dr.Close();
            //        con.DisConnect();

            //    }
            //    else
            //    {

            //        SqlDataReader odr = con.Attendce_Approval_Detail_withHODALLPending(Session["uid"].ToString(), Session["Company"].ToString());
            //        DataTable Dt = new DataTable();
            //        Dt.Load(odr);
            //        grdViewApproval.DataSource = Dt;
            //        grdViewApproval.DataBind();
            //        odr.Close();
            //        con.DisConnect();

            //        SqlDataReader dr = con.Attendce_Approval_Detail_withHODALLPending(Session["uid"].ToString(), Session["Company"].ToString());
            //        dr.Read();
            //        if (dr.HasRows)
            //        {
            //            btnApprove.Visible = true;
            //            btnreject.Visible = true;
            //            btnselectchecked.Visible = true;
            //            btnuncheked.Visible = false;
            //        }
            //        else
            //        {
            //            btnApprove.Visible = false;
            //            btnreject.Visible = false;
            //            btnselectchecked.Visible = false;
            //            btnuncheked.Visible = false;
            //        }
            //        dr.Close();
            //        con.DisConnect();
            //    }
            //}
            //if (PriorityHRapr == "1")
            //{
            //    if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" || Session["UserType"].ToString() == "2" && HRHODsame == "HR" && forHRisHOD == "HR")
            //    {
            //        SqlDataReader odr = con.Attendce_Approval_Detail_withPriorityHRAllPending(Session["uid"].ToString(), Session["Company"].ToString());
            //        DataTable Dt = new DataTable();
            //        Dt.Load(odr);
            //        grdViewApproval.DataSource = Dt;
            //        grdViewApproval.DataBind();
            //        odr.Close();
            //        con.DisConnect();

            //        SqlDataReader dr = con.Attendce_Approval_Detail_withPriorityHRAllPending(Session["uid"].ToString(), Session["Company"].ToString());
            //        dr.Read();
            //        if (dr.HasRows)
            //        {
            //            btnApprove.Visible = true;
            //            btnreject.Visible = true;
            //            btnselectchecked.Visible = true;
            //            btnuncheked.Visible = false;
            //        }
            //        else
            //        {
            //            btnApprove.Visible = false;
            //            btnreject.Visible = false;
            //            btnselectchecked.Visible = false;
            //            btnuncheked.Visible = false;
            //        }
            //        dr.Close();
            //        con.DisConnect();
            //    }
            //    else
            //    {
            //        SqlDataReader odr = con.Attendce_Approval_Detail_withHODALLPending(Session["uid"].ToString(), Session["Company"].ToString());
            //        DataTable Dt = new DataTable();
            //        Dt.Load(odr);
            //        grdViewApproval.DataSource = Dt;
            //        grdViewApproval.DataBind();
            //        odr.Close();
            //        con.DisConnect();

            //        SqlDataReader dr = con.Attendce_Approval_Detail_withHODALLPending(Session["uid"].ToString(), Session["Company"].ToString());
            //        dr.Read();
            //        if (dr.HasRows)
            //        {
            //            btnApprove.Visible = true;
            //            btnreject.Visible = true;
            //            btnselectchecked.Visible = true;
            //            btnuncheked.Visible = false;
            //        }
            //        else
            //        {
            //            btnApprove.Visible = false;
            //            btnreject.Visible = false;
            //            btnselectchecked.Visible = false;
            //            btnuncheked.Visible = false;
            //        }
            //        dr.Close();
            //        con.DisConnect();

            //    }

            //}
            //}


        }


    }

    //string mailfrom = ""; string subject1 = ""; string Body1 = ""; string smtpfromportal = ""; int portNo1; string Pass_From = ""; string Mail_Sending_Option = ""; string Leave_Applymail = ""; string CCMail = "";
    //public void ShowMailDataTo(string mailTo1)
    //{

    //    SqlDataReader dr = con.Show_tble_MailSetup(Session["Company"].ToString());
    //    dr.Read();
    //    if (dr.HasRows)
    //    {
    //        mailfrom = dr["from_Email"].ToString();
    //        smtpfromportal = dr["smtp"].ToString();
    //        Pass_From = dr["Password_From"].ToString();
    //        CCMail = dr["CCMail"].ToString();
    //        string portNo = dr["Port_No"].ToString();
    //        portNo1 = Convert.ToInt32(portNo);
    //        Mail_Sending_Option = dr["Mail_Sending_Option"].ToString();
    //        Leave_Applymail = dr["Leave_Approval"].ToString();

    //    }

    //    dr.Close();
    //    con.DisConnect();
    //    if (Mail_Sending_Option == "1" && Leave_Applymail == "True")
    //    {
    //        SendMail(mailTo1);
    //    }

    //}

    string chkUSerID = ""; string profilechangedate = ""; string lblnoofchangeu1 = ""; string HRAPRStatus = "";
    string HODAPRStatus = ""; string tblenameAttendence = "";


    string tble_Leave_Credited = "";

    public void FindCheckData()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        tble_Leave_Credited = "[" + rccname + "$Pay Employee Leave Credited" + "]";
        SqlDataReader drall = con.Approved_AttendanceData(Session["Company"].ToString(), chkUSerID, profilechangedate);
        drall.Read();
        if (drall.HasRows)
        {
            //SqlDataAdapter da = new SqlDataAdapter("select * from tbl_attendence where Userid='" + chkUSerID + "' and CompanyName='" + Session["Company"].ToString() + "' and CONVERT(date, Atte_Date,103) ='" + profilechangedate + "'", con.Con);
            //DataSet ds = new DataSet();
            //da.Fill(ds, "tbl_attendence");
            //for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            //{
            string Co_Leave = "";string Atte_Date="";
            string Uname = ""; string fromTime = ""; string ToTime = ""; string Work_period = ""; string Job_location = ""; string Remarks = ""; string Status = ""; string Working_Duration = ""; string statuspresent = ""; string comMail = ""; string HR_Userid = ""; string HREmailidapr = ""; string HODEmailIDapr = ""; string HRNameapr = ""; string HODNameapr = ""; string workingHours2 = "";
            Uname = drall["Uname"].ToString();
            fromTime = drall["fromTime"].ToString();
            Atte_Date = drall["Atte_Date"].ToString();

            ToTime = drall["ToTime"].ToString();
            Work_period = drall["Work_period"].ToString();
            Job_location = drall["Job_location"].ToString();
            Remarks = drall["Remarks"].ToString();
            comMail = drall["Comp_Mail"].ToString();
            statuspresent = drall["Status"].ToString();
            HR_Userid = drall["HR_Userid"].ToString();
            Co_Leave = drall["Co_Leave"].ToString();
            HREmailidapr = drall["HREmailid"].ToString();
            HODEmailIDapr = drall["HODEmailID"].ToString();
            HRNameapr = drall["HRName"].ToString();
            HODNameapr = drall["HODName"].ToString();
            if (fromTime !="" && ToTime == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please fill out time');", true);
            }
            else
            {
                if (statuspresent == "Present")
                {
                    Status = "1";
                }
                workingHours2 = drall["Working_Duration"].ToString();
                if (workingHours2 == "")
                {
                    Working_Duration = "0";
                }
                if (workingHours2 != "")
                {
                    Working_Duration = workingHours2.ToString();
                }


                drall.Close();
                con.DisConnect();
                if (Blankapr == "1")
                {
                    string approvalStatus = "Approved";
                    string approvalStatusHOD = "Pending";
                    string hodDate = ""; string hrdate = ""; string hodid = ""; string hrid = ""; string holidaypost = ""; string offDayPost = "";
                    SqlDataReader dr = Portalcon.Show_AttendenceDateFromNavision(tblenameAttendence, profilechangedate, chkUSerID);
                    dr.Read();
                    if (dr.HasRows)
                    {
                        dr.Close();
                        Portalcon.DisConnect();
                        Portalcon.Update_AttendencewithApproval(tblenameAttendence, fromTime, ToTime, Status, Working_Duration, chkUSerID, profilechangedate,Co_Leave,"Pending",Remarks);
                        Portalcon.DisConnect();
                        con.Update_tble_Atttendence_ResolvedBlank(approvalStatus, System.DateTime.Now.ToString("dd-MM-yyyy"), hodDate, hrdate, hrid, hodid, chkUSerID, approvalStatusHOD, approvalStatusHOD, profilechangedate, Session["Company"].ToString());
                        con.DisConnect();
                        //Pay_Daily_Attendence_Detail
                        DateTime attendate = Convert.ToDateTime(profilechangedate);
                        string fatten_date = attendate.ToString("yyyy-MM-dd");
                        SqlDataReader dr5 = Portalcon.Show_HolidayList(tblenameAttendence, fatten_date);
                        dr5.Read();
                        if (dr5.HasRows)
                        {
                            holidaypost = dr5["Holiday"].ToString();
                            offDayPost = dr5["Off Day"].ToString();
                            dr5.Close();
                            if (holidaypost == "1" && offDayPost == "0")
                            {
                                Portalcon.insert_Table_Credit_Leave(tble_Leave_Credited, chkUSerID, Convert.ToDateTime(fatten_date), "CO", Convert.ToDecimal(1.00), Convert.ToInt32(1), "0");
                                Portalcon.DisConnect();
                            }
                            if (offDayPost == "1" && holidaypost == "0")
                            {
                                Portalcon.insert_Table_Credit_Leave(tble_Leave_Credited, chkUSerID, Convert.ToDateTime(fatten_date), "CO", Convert.ToDecimal(1.00), Convert.ToInt32(1), "0");
                                Portalcon.DisConnect();
                            }
                            if (offDayPost == "1" && holidaypost == "1")
                            {
                                Portalcon.insert_Table_Credit_Leave(tble_Leave_Credited, chkUSerID, Convert.ToDateTime(fatten_date), "CO", Convert.ToDecimal(1.00), Convert.ToInt32(1), "0");
                                Portalcon.DisConnect();
                            }

                        }
                        else
                        {
                            dr5.Close();
                            Portalcon.DisConnect();
                        }

                      //  Portalcon.insert_Table_Credit_Leave(tble_Leave_Credited,chkUSerID,Convert.ToDateTime(Atte_Date

                        //if (EmailBlank == "True")
                        //{
                        //subject1 = "Posted Attendance";

                        //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}", Environment.NewLine, "" + Uname + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "Posted Attendance of  " + profilechangedate + " , " + " In Time  : " + fromTime + " Out Time :  " + ToTime + " . ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                        //ShowMailDataTo(comMail);



                        //}

                        if (EmailHR == "True")
                        {
                            //subject1 = "Attendance Approved of " + Uname + " Date " + profilechangedate;

                            //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}", Environment.NewLine, " HR Manager" + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have mark attendance of  " + profilechangedate + " , " + " In Time  : " + fromTime + " Out Time :  " + ToTime + " . ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            //ShowMailDataTo(HREmailidapr);
                        }

                        if (EmailHOD == "True")
                        {
                            //subject1 = "Attendance Approved of " + Uname + " Date " + profilechangedate;

                            //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}", Environment.NewLine, "'" + HRHODsame + Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, " I have mark attendance of  " + profilechangedate + " , " + " In Time  : " + fromTime + " Out Time :  " + ToTime + " . ", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                            //ShowMailDataTo(HODEmailIDapr);
                        }


                    }
                    else
                    {
                        dr.Close();
                        Portalcon.DisConnect();
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please contact Navision Team or Update Pay Daily Attendence Detail table this date is not available ');", true);
                    }
                }


                
           }
        }
        drall.Close();
        con.DisConnect();
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {



        foreach (GridViewRow row in grdViewApproval.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);
                Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblProfilechangedate") as Label);

                if (chkRow.Checked == true)
                {
                    DateTime lblProfilechangedate11 = DateTime.ParseExact(lblProfilechangedate1.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    profilechangedate = lblProfilechangedate11.ToString("yyyy-MM-dd");
                    chkUSerID = chkRow.Text;
                    FindCheckData();


                }
            }
        }
        AttendenceApproval_Search();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        grdViewApproval.Visible = true;
        AttendenceApproval_Search();
    }
    protected void grdViewApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViewApproval.PageIndex = e.NewPageIndex;
        AttendenceApproval_Search();
    }
    protected void CHKAllPending_CheckedChanged(object sender, EventArgs e)
    {

        pnlEmployeeidName.Visible = false;
        pnlDate.Visible = false;
        btnSearch.Visible = false;
        txtSearchName.Text = "";
        txtfromDatePost.Text = "";
        txtTodatePost.Text = "";

        
        grdViewApproval.Visible = true;
        AttendenceApproval_Search();

    }
    protected void btnchangestatus_Command(object sender, CommandEventArgs e)
    {

        Session["chgsUserid"] = e.CommandArgument.ToString();
        //     foreach (GridViewRow row in grdViewApproval.Rows)
        //     {
        //         if (row.RowType == DataControlRowType.DataRow)
        //         {

        //             Label lblProfilechangedate1 = (row.Cells[0].FindControl("lblProfilechangedate") as Label);
        //               Session["chgsProfilechangeDate"] = lblProfilechangedate1.Text;
        //             Label lblNoofchange = (row.Cells[0].FindControl("lblNoofchange") as Label);
        //             Session["chgsNoOfchanges"] = lblNoofchange.Text;



        //         }
        //     }
        //     Page.ClientScript.RegisterStartupScript(
        //this.GetType(), "OpenWindow", "window.open('changeStatus.aspx','_newtab');", true);

    }






    string chgsUserid = ""; string chgsProfilechangeDate = ""; string chgsNoOfchanges = "";
    protected void btnchangestusrejected_Command(object sender, CommandEventArgs e)
    {
        Session["chgsUserid"] = e.CommandArgument.ToString();
    }
    protected void btnresolved_Command(object sender, CommandEventArgs e)
    {
        Session["chgsUserid"] = e.CommandArgument.ToString();
    }
    private void ToggleCheckState(bool checkState)
    {

        foreach (GridViewRow row in grdViewApproval.Rows)
        {

            CheckBox cb = (CheckBox)row.FindControl("chkMark");
            if (cb != null)
                cb.Checked = checkState;
        }
    }

    protected void btnselectchecked_Click(object sender, EventArgs e)
    {
        ToggleCheckState(true);
        btnselectchecked.Visible = false;
        btnuncheked.Visible = true;
    }
    protected void btnuncheked_Click(object sender, EventArgs e)
    {
        ToggleCheckState(false);
        btnselectchecked.Visible = true;
        btnuncheked.Visible = false;
    }
    protected void lnkpostAttendance_Click(object sender, EventArgs e)
    {
        pnlProfileView.Visible = true;
        btnSearch.Visible = false;
        pnlDate.Visible = false;
        pnlEmployeeidName.Visible = false;
        //pnlProfileRejected.Visible = false;
     
        pnlMain.Visible = false;
        CHKAllPending.Checked = false;
        //ModalPopupExtender1.Hide();
        //rdEmployeeID.Checked = false;
        //rdEmployeeName.Checked = false;
        rdDatewise.Checked = false;

        pnlviewAttendence.Visible = false;
        pnlAttendenceMark.Visible = false;
        pnlviewattend.Visible = false;
        pnlMain.Visible = false;
    }
    protected void clndview_SelectionChanged(object sender, EventArgs e)
    {
        showAttendenceExpiry();
        showAttendenceExpiryIND();
    }
    public void ShowAllattendanceposted()
    { 
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        SqlDataReader dr = Portalcon.Show_AllAttendancedata(tblenameAttendence, txtfromDate.Text, txtTodate.Text, Session["uid"].ToString());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grdviewApprovalofnav.DataSource = dt;
        grdviewApprovalofnav.DataBind();
        dr.Close();
        Portalcon.DisConnect();
    }
   
    protected void btnsearchnavdata_Click(object sender, EventArgs e)
    {
        pnlworkingday.Visible=true;
        showNoofworkingdays();
        showNoofpresentdays();
        showNoofleavedays();
        ShowAllattendanceposted();
        showAttendenceExpiry();
        showAttendenceExpiryIND();
    }
    protected void grdviewApprovalofnav_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdviewApprovalofnav.PageIndex = e.NewPageIndex;
        ShowAllattendanceposted();
        showAttendenceExpiry();
        showAttendenceExpiryIND();
    }
    protected void grdviewApprovalofnav_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblStatusStatus = (Label)e.Row.FindControl("lblStatusStatus");
            Label lbltimeinnav = (Label)e.Row.FindControl("lbltimeinnav");
            Label lbltimeoutnav = (Label)e.Row.FindControl("lbltimeoutnav");

            if (lbltimeinnav.Text == lbltimeoutnav.Text)
            {
                lbltimeinnav.Text = "";
                lbltimeoutnav.Text = "";
            }
            if (lbltimeinnav.Text != lbltimeoutnav.Text)
            {
                
            }

            if (lblStatusStatus.Text == "1")
            {
                lblStatusStatus.Text = "Present";
            }

            if (lblStatusStatus.Text == "2")
            {
                lblStatusStatus.Text = "Holiday";
            }
            if (lblStatusStatus.Text == "3")
            {
                lblStatusStatus.Text = "Off-Day";
            }
            if (lblStatusStatus.Text == "4")
            {
                lblStatusStatus.Text = "Leave";
            }
            if (lblStatusStatus.Text == "5")
            {
                lblStatusStatus.Text = "LWP(Half-Day)";
            }

            if (lblStatusStatus.Text == "6")
            {
                lblStatusStatus.Text = "LWP(Full-Day)";
            }

            if (lblStatusStatus.Text == "7")
            {
                lblStatusStatus.Text = "Special Leave";
            }
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Calendar1.Enabled = true;
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        btnClose.Visible = false;
        showAttendenceExpiry();
        showAttendenceExpiryIND();
    }

    protected void grdAttendence_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Button btnEdit = (Button)e.Row.FindControl("btnEdit");
            Label lblAttendanceType = (Label)e.Row.FindControl("lblAttendanceType");
            if (lblAttendanceType.Text == "Manual")
            {

                btnEdit.Visible = true;
            }
            if (lblAttendanceType.Text == "Card")
            {
                showAttendenceExpiryAdd();
                showAttendenceExpiryINDforAdd();

                if (Card_Attendance_changingAll == "Yes")
                {
                    btnEdit.Visible = true;
                }
                if (Card_Attendance_changingAll == "No")
                {
                    btnEdit.Visible = false;
                }
                if (Card_Attendance_changingINDV == "Yes")
                {
                    btnEdit.Visible = true;
                }
                if (Card_Attendance_changingINDV == "No")
                {
                    btnEdit.Visible = false;
                }

                
            }
        }
    }
}