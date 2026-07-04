using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI.HtmlControls;
using paytm;


public partial class IndexMaster : System.Web.UI.MasterPage
{
    Connection Portalcon; ServicePoratal con; string HRHODsame = ""; string forHRisHOD = "";
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlCommand com;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            if (Session["DesignationCode"].ToString() == "D0375" || Session["DesignationCode"].ToString() == "D045" || Session["DesignationCode"].ToString() == "D003" || Session["DesignationCode"].ToString() == "D016" || Session["DesignationCode"].ToString() == "D0376")
            {
                lnk_mentor_allocation.Visible = true;
            }

            if ((Session["Departmentcode"].ToString() == "D228" && Session["uid"].ToString() == "TMU05721") || Session["Departmentcode"].ToString() == "D213" || (Session["Departmentcode"].ToString() == "D039" && Session["uid"].ToString() == "TMU00245"))
            {
                //HR DepCode: D228,PAYROLL SECTION DepCode=D213,ACCOUNT SECTION DepCode=D039
                IdPHDStudentNoDuesApproval.Visible = true;
            }
            //else
            //{
            //    IdPHDStudentNoDuesApproval.Visible = false;
            //}

            con1.Open();

            SqlCommand cmdT = new SqlCommand("select count(*) as c from [HRMSPortal].dbo.[tble_Exit_Interview_Form] where [Employee Code]='" + Session["uid"].ToString() + "' AND Status='Submit' and (Hod_Status!='Rejected By HOD' and Hr_Status!='Rejected By HR')", con1);
            DataTable dtOrderT = new DataTable();
            SqlDataAdapter daT = new SqlDataAdapter(cmdT);
            daT.Fill(dtOrderT);

            if (Convert.ToInt32(dtOrderT.Rows[0]["c"]) > 0)
            {
                liHR.Visible = false;
            }

            else
            {
                liHR.Visible = true;
            }

            if (Session["uid"].ToString() == "TMU03798")
            {
                EquipmentIndent.Visible = true;
            }
            if (Session["uid"].ToString() == "TMU00308")
            {
                EquipmentApproval.Visible = true;
            }


            //SqlCommand cmdT = new SqlCommand("select count(*) as 'MAX' from [OnlinePaymentLog] where GatewayStatus=1", con1);
            //DataTable dtOrderT = new DataTable();
            //SqlDataAdapter daT = new SqlDataAdapter(cmdT);
            //daT.Fill(dtOrderT);

            //if (Convert.ToInt32(dtOrderT.Rows[0]["MAX"]) >= 300)
            //{
            //    lblPayTM.Visible = false;
            //}

            //SqlCommand cmdc = new SqlCommand("select * from [OnlinePaymentLog] where GatewayStatus=1 and UserID='" + Session["uid"].ToString() + "'", con1);
            //DataTable dtOrderc = new DataTable();
            //SqlDataAdapter dac = new SqlDataAdapter(cmdc);
            //dac.Fill(dtOrderc);
            //if (dtOrderc.Rows.Count > 0)
            //{
            //    lblPayTM.Text = "Receipt Pending";
            //    lblPayTM.Enabled = false;
            //}

            com = new SqlCommand("select [Custom System Indicator Text] from [TMU$Company Information]", con1);
            SqlDataReader reader = com.ExecuteReader();
            reader.Read();
            ind.Text = reader["Custom System Indicator Text"].ToString();
            reader.Close();
            con1.Close();


            System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
            if (((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
            {
                lblMarkAtt.Visible = false;
            }
            else
            {
                lblMarkAtt.Visible = false;
            }
            if (Session["uname"].ToString() == null || Session["Company"].ToString() == null || Session["UserGroup"].ToString() == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                //----------------------------------User Role-------------------
                //-----------Indent ----START
                if (Session["IndentEntry_DH"].ToString() == "1") { IndentRequest.Visible = true; liOthers.Visible = true; }
                if (Session["IndentApprovalID"].ToString() != "") { IndentApproval.Visible = true; liOthers.Visible = true; }
                if (Session["IndentEntry_DHIT"].ToString() == "1") { IndentIT.Visible = true; liOthers.Visible = true; }
                if (Session["IndentApprovalIDIT"].ToString() != "")
                {
                    IndentITApproval.Visible = true;
                    liOthers.Visible = true;
                }
                if (Session["uid"].ToString() == "TMU07417")
                {
                    IndentITApproval.Visible = true;
                }
                //-----------Indent ----END

                Portalcon = new Connection();
                con = new ServicePoratal();
                lbluserid666.Text = Session["uid"].ToString(); lblName.Text = Session["uname"].ToString();

                con1.Open();
                com = new SqlCommand("select case when Hold=1 and verify=0 then 'HOLD' when verify=1 and Hold=0 then 'VERIFIED' when verify=1 and Hold=1 then 'HOLD/VERIFIED'  else 'Pending' end as 'Status'  from [HRMSPortal].dbo.[tbl_EmployeeSalaryHoldDetail]  where [EmployeeCode]='" + Session["uid"].ToString() + "'and [month]=(select month([Payroll Processing Month Date]) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Company Policy])and [Year]=(select Year([Payroll Processing Month Date]) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Company Policy])", con1);
                SqlDataReader reader1 = com.ExecuteReader();
                reader1.Read();

                if (reader1.HasRows)
                {

                    lblVerifyStatus.Text = "Verification Status :" + reader1["Status"].ToString() + "";
                }
                else
                {
                    lblVerifyStatus.Text = "Verification Status : Pending";
                }






                con1.Close();

                if (!IsPostBack) { BINDimage(); }
                SqlCommand cmd1 = new SqlCommand("sp_GetAccessForTimeTable_Role", con1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@userid", Session["uid"].ToString());
                cmd1.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
                cmd1.Parameters["@Return1"].Direction = ParameterDirection.Output;
                con1.Open();
                cmd1.ExecuteNonQuery();
                string res = cmd1.Parameters["@Return1"].Value.ToString();
                con1.Close();
                if (res == "1")
                { StudentContinueAbsentReport.Visible = true; AssignmentUploadView.Visible = false; DailyAttendanceDetails.Visible = true; }
                else { StudentContinueAbsentReport.Visible = false; AssignmentUploadView.Visible = false; DailyAttendanceDetails.Visible = false; }
            }
            GoogleLink1(); showHRHODisexhist();
            if (Session["uid"].ToString() == "TMU00864")
            {
                EmployeeAttendance.Visible = true;
                ViewTeamAttendance.Visible = false;
            }
            if (Session["uid"].ToString() == "TMU00166")
            {
                HindiNameUpdate.Visible = true;
            }
            else
            {
                HindiNameUpdate.Visible = false;
            }
            if (Session["uid"].ToString() == "TMU01979" || Session["uid"].ToString() == "TMU07001" || Session["uid"].ToString() == "TMU06106")
            {

                ITIssueTrackerList.Visible = true;
            }
            if (Session["uid"].ToString() == "TMU04870")
            {
                UpdateProfile.Visible = true;
                StudentdocumentVerify.Visible = true;
            }
            if (Session["uid"].ToString() == "TMU00035")
            {
                StudentdocumentVerify.Visible = true;
            }
            if (Session["uid"].ToString() == "TMU07320" || Session["uid"].ToString() == "TMU09456")
            {

                JainStudentAttendance.Visible = true;
            }
            if (Session["uid"].ToString() == "TMU02982" || Session["uid"].ToString() == "TMU08026" || Session["uid"].ToString() == "TMU08617" || Session["uid"].ToString() == "TMU05294" || Session["uid"].ToString() == "TMU06022")
            {
                NaacDashboard.Visible = true;
            }
            if (Session["uid"].ToString() == "TMU07320" || Session["uid"].ToString() == "TMU09456" || Session["uid"].ToString() == "TMU00619" || Session["uid"].ToString() == "TMU04870" || Session["uid"].ToString() == "TMU03651" || Session["uid"].ToString() == "TMU08026" || Session["uid"].ToString() == "TMU01086" || Session["uid"].ToString() == "TMU00071")
            {
                StudentNoDuesApprovalList.Visible = true;
            }
            else
            {
                NoDuesApprovalList.Visible = true;
            }                      

            GetLinkData();//===================>Add New Function For Show Hide Link Using DataBase Role
            showlib();
            showIssuetrackerlist();
            showFellowshipEmployee();
            hidehr();
            if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC") //12-04-2017 for DENTAL AND MEDICAL
            {
                StudentExtraAttendanceMark.Visible = false;
                if (Session["UserGroup"].ToString() != "PRINCIPAL")
                {
                    StudentDetaineeReport.Visible = false;
                    DailyAttendanceDetails.Visible = false;
                    StudentContinueAbsentReport.Visible = false;
                }
            }
            if (Session["GlobalDimension1Code"].ToString() == "TMCT" || Session["GlobalDimension1Code"].ToString() == "TMEG")
            {
                string id = Session["uid"].ToString();
                DataTable dt = GetData("select case when  (select count([Principal]) from [TMU$User Role Matrix] where [Global Dimenison 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'  and [Principal]='" + Session["uid"].ToString() + "')>0 then 'PRINCIPAL' when  (select count([HOD]) from [TMU$User Role Matrix] where [Global Dimenison 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'  and [HOD]='" + Session["uid"].ToString() + "')>0 then 'HOD' end as 'UserRole'");

                if (dt.Rows[0]["UserRole"].ToString() == "PRINCIPAL")
                {
                    MapCourseSubject.Visible = false;
                    MapFacultySubject1.Visible = false;
                }
                if (dt.Rows[0]["UserRole"].ToString() == "HOD")
                {
                    MapCourseSubject.Visible = true;
                    MapFacultySubject1.Visible = true;
                }
            }
            if (Session["Departmentcode"].ToString().Trim() == "D213" && Session["uid"].ToString() != "TMU00241")
            {
                lnkCoClaimReport.Visible = true;
                lnkLeaveReport.Visible = true;
            }
            if (Session["Departmentcode"].ToString().Trim() == "ERP" || Session["uid"].ToString() == "TMU00265")
            {
                ExamFormStatus.Visible = true;
            }
            if (Session["Security"].ToString().ToUpper().Trim() == "SECURITY")
            {
                liFine.Visible = true;
                StudentSecurityFine.Visible = false; //StudentSecurityFine.Visible = true;
                liHR.Visible = false;
            }
            if (Session["FINE"].ToString().ToUpper().Trim() == "FINE")
            {
                liFine.Visible = true;
                StudentFineForAdmin.Visible = true;
                StudentFineReport.Visible = true;
                ViewAttendance.Visible = true;
                Employee_Punch_Data.Visible = true;
                liOthers.Visible = true;
                EventList.Visible = true;
                //PunchCorrection.Visible = true;
            }



            DataTable dt1 = GetData("select COUNT(*) as 'C' from [TMU$Hostel Maintenance Setup] where ([Co-Head Code]='" + Session["uid"].ToString() + "' or [Head Code]='" + Session["uid"].ToString() + "' or [Warden Code]='" + Session["uid"].ToString() + "')");



            if (dt1.Rows[0]["c"].ToString() != "0")
            {
                Li3.Visible = true;
            }
            else
            {
                Li3.Visible = false;
            }

            if (Session["uid"].ToString() == "TMU04620" || Session["uid"].ToString() == "TMU00381" || Session["uid"].ToString() == "TMU00064" || Session["uid"].ToString() == "TMU05284" || Session["uid"].ToString() == "TMU00619")
            {
                Li4.Visible = true;
            }
            else
            {
                Li4.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("../Default.aspx");
        }
    }

    public void showIssuetrackerlist()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            string strSQL = "select  count(*) AssignToID from  tbl_IssueTracker  where [AssignToID] ='" + Session["uid"].ToString() + "'";

            {
                SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (Convert.ToInt32(dt.Rows[0]["AssignToID"]) > 0)
                {
                    ITIssueTrackerList.Visible = true;
                }


            }
        }
    }

    public void showHRHODisexhist()
    {

        SqlDataReader dr = Portalcon.SHow_showHODExhistavv(Session["uid"].ToString(), Session["CompanyTableEmployee"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            Portalcon.DisConnect();

            ViewTeamAttendance.Visible = true;
            Team_Punch_Data.Visible = true;
            if (Session["GlobalDimension1Code"].ToString() == "TMHS")
            {
                UploadRoaster.Visible = true;
                PostRoaster.Visible = true;
            }
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
            ViewTeamAttendance.Visible = false;
            Team_Punch_Data.Visible = false;
            UploadRoaster.Visible = false;
            PostRoaster.Visible = false;
        }
    }
    string HODapr = ""; string HRapr = ""; string Blankapr = ""; string PriorityHRapr = ""; string PriorityHODapr = "";
    public void Showpermission()
    {
        string type = "For Profile";
        SqlDataReader dr = con.SHow_tblesetupforpriorityforApproval(type, Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            HODapr = dr["HOD"].ToString();

            HRapr = dr["HR"].ToString();

            Blankapr = dr["Blank"].ToString();

            PriorityHRapr = dr["PriorityHR"].ToString();

            PriorityHODapr = dr["PriorityHOD"].ToString();


            if (Blankapr == "1")
            {
                lnkprofileaprovalblank.Visible = false;
                lblProfileCount.Visible = false;

            }
            else
            {
                lnkprofileaprovalblank.Visible = true;
                lblProfileCount.Visible = true;
            }
        }
        dr.Close();
        con.DisConnect();
    }
    string HODaprLeave = ""; string HRaprLeave = ""; string BlankaprLeave = ""; string PriorityHRaprLeave = ""; string PriorityHODaprLeave = "";
    public void ShowpermissionLeave()
    {
        string type = "For Leave";
        SqlDataReader dr = con.SHow_tblesetupforpriorityforApproval(type, Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            HODaprLeave = dr["HOD"].ToString();

            HRaprLeave = dr["HR"].ToString();

            BlankaprLeave = dr["Blank"].ToString();

            PriorityHRaprLeave = dr["PriorityHR"].ToString();

            PriorityHODaprLeave = dr["PriorityHOD"].ToString();

        }
        dr.Close();
        con.DisConnect();
    }
    string HODaprReimb = ""; string HRaprReimb = ""; string BlankaprReimb = ""; string PriorityHRaprReimb = ""; string PriorityHODaprReimb = "";
    public void ShowpermissionReimb()
    {
        string type = "For Reimbursment";
        SqlDataReader dr = con.SHow_tblesetupforpriorityforApproval(type, Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            HODaprReimb = dr["HOD"].ToString();

            HRaprReimb = dr["HR"].ToString();

            BlankaprReimb = dr["Blank"].ToString();

            PriorityHRaprReimb = dr["PriorityHR"].ToString();

            PriorityHODaprReimb = dr["PriorityHOD"].ToString();

        }
        dr.Close();
        con.DisConnect();
    }
    string HODaprAttend = ""; string HRaprAttend = ""; string BlankaprAttend = ""; string PriorityHRaprAttend = ""; string PriorityHODaprAttend = "";
    public void ShowpermissionAttend()
    {
        string type = "For Attendance";
        SqlDataReader dr = con.SHow_tblesetupforpriorityforApproval(type, Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            HODaprAttend = dr["HOD"].ToString();

            HRaprAttend = dr["HR"].ToString();

            BlankaprAttend = dr["Blank"].ToString();

            PriorityHRaprAttend = dr["PriorityHR"].ToString();

            PriorityHODaprAttend = dr["PriorityHOD"].ToString();

        }
        dr.Close();
        con.DisConnect();
    }
    public void Attendence_Pending_ApprovalHOD_Count()
    {
        SqlDataReader dr = con.Attendence_Approval_Count_Current(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["HODuserid"].ToString();
            if (countprofile == "0")
            {
                lblAttendence.Text = "";
            }
            else
            {
                lblAttendence.Text = dr["HODuserid"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void profile_Pending_ApprovalForAdmin_Count()
    {
        SqlDataReader dr = con.Profile_Approval_Count_Current_ForAdmin(Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["UserID"].ToString();
            if (countprofile == "0")
            {
                lblProfileCount.Text = "";
            }
            else
            {
                lblProfileCount.Text = dr["UserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void profile_Pending_ApprovalHOD_Count()
    {
        SqlDataReader dr = con.Profile_Approval_Count_Current(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["HODuserid"].ToString();
            if (countprofile == "0")
            {
                lblProfileCount.Text = "";
            }
            else
            {
                lblProfileCount.Text = dr["HODuserid"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void Leave_Pending_ApprovalHOD_Count()
    {
        SqlDataReader dr = con.Leave_Approval_Count_Current(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["HODUserid"].ToString();
            if (countprofile == "0")
            {
                lblLeaveCount.Text = "";
            }
            else
            {
                lblLeaveCount.Text = dr["HODUserid"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void Reimbursment_Pending_ApprovalHOD_Count()
    {
        SqlDataReader dr = con.Reimbursment_Approval_Count_Current(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["HODuserid"].ToString();
            if (countprofile == "0")
            {
                lblReimbursmentCount.Text = "";
            }
            else
            {
                lblReimbursmentCount.Text = dr["HODuserid"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void profile_Pending_ApprovalHR_Count()
    {
        SqlDataReader dr = con.Profile_Approval_Count_CurrentHR(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["HRUserID"].ToString();
            if (countprofile == "0")
            {
                lblProfileCount.Text = "";
            }
            else
            {
                lblProfileCount.Text = dr["HRUserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void Attendece_Pending_ApprovalHR_Count()
    {
        SqlDataReader dr = con.Attend_Approval_Count_CurrentHR(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["HRUserID"].ToString();
            if (countprofile == "0")
            {
                lblAttendence.Text = "";
            }
            else
            {
                lblAttendence.Text = dr["HRUserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void Leave_Pending_ApprovalHR_Count()
    {
        SqlDataReader dr = con.Leave_Approval_Count_CurrentHR(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["HRUserID"].ToString();
            if (countprofile == "0")
            {
                lblLeaveCount.Text = "";
            }
            else
            {
                lblLeaveCount.Text = dr["HRUserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void GoogleLink1()
    {
        string Link = con.FacultyGoogleLink(Session["uid"].ToString());
        GoogleLink.NavigateUrl = Link;
    }
    public void Reimbursment_Pending_ApprovalHR_Count()
    {
        SqlDataReader dr = con.Reimbursment_Approval_Count_CurrentHR(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["HRUserID"].ToString();
            if (countprofile == "0")
            {
                lblReimbursmentCount.Text = "";
            }
            else
            {
                lblReimbursmentCount.Text = dr["HRUserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void profile_Pending_ApprovalUser_Count()
    {
        SqlDataReader dr = con.Profile_Approval_Count_CurrentUser(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["UserID"].ToString();
            if (countprofile == "0")
            {
                lblProfileCount.Text = "";
            }
            else
            {
                lblProfileCount.Text = dr["UserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
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
                lblAttendence.Text = "";
                // lblPostAttendanceNotify.Text = "";
            }
            else
            {
                lblAttendence.Text = dr["UserID"].ToString();
                //lblPostAttendanceNotify.Text = dr["UserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void Attend_Pending_ApprovalUser_CountFor_Admin()
    {
        SqlDataReader dr = con.Attend_Approval_Count_CurrentUser_ForAdmin(Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["UserID"].ToString();
            if (countprofile == "0")
            {
                lblAttendence.Text = "";
            }
            else
            {
                lblAttendence.Text = dr["UserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void Leave_Pending_ApprovalUser_Count_ForAdmin()
    {
        SqlDataReader dr = con.Leave_Approval_Count_CurrentUser_ForAdmin(Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["UserID"].ToString();
            if (countprofile == "0")
            {
                lblLeaveCount.Text = "";
            }
            else
            {
                lblLeaveCount.Text = dr["UserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void Leave_Pending_ApprovalUser_Count()
    {
        SqlDataReader dr = con.Leave_Approval_Count_CurrentUser(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["UserID"].ToString();
            if (countprofile == "0")
            {
                lblLeaveCount.Text = "";
            }
            else
            {
                lblLeaveCount.Text = dr["UserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void Reimbursment_Pending_ApprovalUser_Count()
    {
        SqlDataReader dr = con.Reimbursment_Approval_Count_CurrentUser(Session["Company"].ToString(), Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["UserID"].ToString();
            if (countprofile == "0")
            {
                lblReimbursmentCount.Text = "";
            }
            else
            {
                lblReimbursmentCount.Text = dr["UserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    public void Reimbursment_Pending_ApprovalUser_Count_ForAdmin()
    {
        SqlDataReader dr = con.Reimbursment_Approval_Count_For_Admin(Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {

            string countprofile = dr["UserID"].ToString();
            if (countprofile == "0")
            {
                lblReimbursmentCount.Text = "";
            }
            else
            {
                lblReimbursmentCount.Text = dr["UserID"].ToString();
            }
        }
        dr.Close();
        con.DisConnect();
    }
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Default.aspx");
    }
    public void BINDimage()
    {
        string id = Session["uid"].ToString();
        byte[] bytes = GetData("select Picture as FacultyImage from [TMU$Employee] where [No_]='" + id + "'").Rows[0]["FacultyImage"].ToString() == "" ? null : (byte[])GetData("select Picture as FacultyImage from [TMU$Employee] where [No_]='" + id + "'").Rows[0]["FacultyImage"];
        if (bytes != null)
        {
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgProfile.ImageUrl = "data:image/png;base64," + base64String;
        }
    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    protected void btnChangeImage_Click(object sender, EventArgs e)
    {

        if (imgUpload.HasFile)
        {
            try
            {
                string filename = Path.GetFileName(imgUpload.PostedFile.FileName);
                string contentType = imgUpload.PostedFile.ContentType;
                using (Stream fs = imgUpload.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(imgUpload.PostedFile.InputStream);
                        int height = img.Height;
                        int width = img.Width;
                        decimal size = Math.Round(((decimal)imgUpload.PostedFile.ContentLength / (decimal)1024), 2);
                        if (size > 20)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Image Size Could Not be Greater than 20 KB !');", true);
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Image Size: " + size + "KB\\nHeight: " + height + "\\nWidth: " + width + "');", true);

                            return;
                        }

                        using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
                        {
                            string query = "update [TMU$Employee] set [Picture]=@Data where No_='" + Session["uid"].ToString() + "'";
                            using (SqlCommand cmd1 = new SqlCommand(query))
                            {
                                cmd1.Connection = con1;
                                cmd1.Parameters.AddWithValue("@Data", bytes);
                                con1.Open();
                                cmd1.ExecuteNonQuery();
                                con1.Close();
                            }
                        }
                    }
                }
                BINDimage();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }

    //----------------------------Ashu Form Show-Hide------------on 06-04-2017---------------START
    public void GetLinkData()
    {

        DataTable dt = new DataTable();        
        dt = con.GetLink(Session["uid"].ToString(), Session["GlobalDimension1Code"].ToString());
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["PageId"].ToString() == "AppraisalForm") { AppraisalForm.Visible = true; }
            if (Session["GlobalDimension1Coded"].ToString().Trim() == "TMCT" || Session["GlobalDimension1Coded"].ToString().Trim() == "TMEG" || Session["GlobalDimension1Coded"].ToString().Trim() == "TMPT" || Session["uid"].ToString() == "TMU00215")
            {

                if (dt.Rows[i]["PageId"].ToString() == "AppraisalFormList") { AppraisalFormList.Visible = true; AppraisalApproval.Visible = true; }
            }
            else
            {
                if (dt.Rows[i]["PageId"].ToString() == "AppraisalFormList") { AppraisalFormList.Visible = true; AppraisalApproval.Visible = true; }
            }
            if (dt.Rows[i]["PageId"].ToString() == "AppraisalFormnonteaching") { AppraisalFormnonteaching.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "AppraisalNonTechList") { AppraisalNonTechList.Visible = true; AppraisalApproval.Visible = true; }



            if (dt.Rows[i]["PageId"].ToString() == "AssignmentUploadDownload") { AssignmentUploadDownload.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "AssignmentUploadView") { AssignmentUploadView.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "Co_Application") { Co_Application.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "EWL_Application") { EWL_Application.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "CreateTimeTable") { CreateTimeTable.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "EditTimeTable") { EditTimeTable.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "DailyAttendanceDetails") { DailyAttendanceDetails.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "EventTypeReport") { EventTypeReport.Visible = true; }


            if (dt.Rows[i]["PageId"].ToString() == "Dashboard") { Dashboard.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "DismissClass") { DismissClass.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "EditStudentAttendance") { EditStudentAttendance.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "EditStudentAttendanceECA")
            {
                if (Session["GlobalDimension1Code"].ToString() == "TMAG" || Session["GlobalDimension1Code"].ToString() == "DPT" || Session["GlobalDimension1Code"].ToString() == "TMCT")
                {
                    EditStudentAttendanceECA.Visible = true;
                }
            }
            if (dt.Rows[i]["PageId"].ToString() == "AcademicCalendar") { AcademicCalendar.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "Employee_Punch_Data") { Employee_Punch_Data.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "Punch_Correction") { PunchCorrection.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "PunchCorrectionS") { PunchCorrectionS.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "Enquiry") { Enquiry.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "Enquiry_Phone") { Enquiry_Phone.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "EnquiryList") { EnquiryList.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "EventCreation") { EventCreation.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "EventList") { EventList.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "exitinterviewform") { exitinterviewform.Visible = true; }
            if (Session["uid"].ToString() == "TMU00049" || Session["uid"].ToString() == "TMU00074" || Session["uid"].ToString() == "TMU00133")
            {
                lblLeaveBalance.Visible = true;
            }
            //if (dt.Rows[i]["PageId"].ToString() == "CovidCertificate") { CovidCertificate.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "TDSdetail") { TDSdetail.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "ArrearR") { ArrearR.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "EmployeeFine")
            {
                if (Session["uid"].ToString() == "TMU00308")
                {
                    EmployeeFine.Visible = true;
                }
            }
            if (dt.Rows[i]["PageId"].ToString() == "FineList")
            {
                if (Session["GlobalDimension1Code"].ToString() == "TMHS")
                {
                    FineList.Visible = true;
                }
            }
            if (Session["uid"].ToString() == "TMU00049")
            {
                CoStatus.Visible = true;

                if (dt.Rows[i]["PageId"].ToString() == "ArrearList")
                {
                    ArrearList.Visible = true;
                }
            }
            if (Session["uid"].ToString() == "TMU00049" || Session["uid"].ToString() == "TMU00075" || Session["uid"].ToString() == "TMU06618" || Session["uid"].ToString() == "TMU08719" || Session["uid"].ToString() == "TMU07987" || Session["uid"].ToString() == "TMU08719" || Session["uid"].ToString() == "TMU00142" || Session["uid"].ToString() == "TMU04490")
            {
                if (Session["uid"].ToString() == "TMU06618" || Session["uid"].ToString() == "TMU07987" || Session["uid"].ToString() == "TMU08719" || Session["uid"].ToString() == "TMU00142")
                {
                    if (dt.Rows[i]["PageId"].ToString() == "VerifiedEmpList") { VerifiedEmpList.Visible = true; }
                    if (dt.Rows[i]["PageId"].ToString() == "NoResponseEmpList") { NoResponseEmpList.Visible = true; }
                    if (Session["uid"].ToString() == "TMU08719")
                    {
                        EmployeeCountHODWise.Visible = true;
                    }
                }
                else
                {
                    if (dt.Rows[i]["PageId"].ToString() == "VerifiedEmpList") { VerifiedEmpList.Visible = true; }
                    if (dt.Rows[i]["PageId"].ToString() == "NoResponseEmpList") { NoResponseEmpList.Visible = true; }
                    if (dt.Rows[i]["PageId"].ToString() == "EmployeeCountHODWise")
                    {

                        EmployeeCountHODWise.Visible = true;


                    }
                    if (dt.Rows[i]["PageId"].ToString() == "Leavecancellation")
                    {
                        Leavecancellation.Visible = true;
                    }
                    if (dt.Rows[i]["PageId"].ToString() == "EmployeeLeaveDetail")
                    {
                        EmployeeLeaveDetail.Visible = true;
                    }
                }
            }
            else
            {
                if (dt.Rows[i]["PageId"].ToString() == "VerifiedEmpList") { VerifiedEmpList.Visible = false; }
                if (dt.Rows[i]["PageId"].ToString() == "EmployeeCountHODWise")
                {

                    EmployeeCountHODWise.Visible = false;


                }
                if (dt.Rows[i]["PageId"].ToString() == "Leavecancellation") { Leavecancellation.Visible = false; }
                if (dt.Rows[i]["PageId"].ToString() == "EmployeeLeaveDetail")
                {
                    EmployeeLeaveDetail.Visible = false;
                }

            }


            //if (dt.Rows[i]["PageId"].ToString() == "exitinterviewformDetails") { exitinterviewformDetails.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "FacultyCoursePlan") { FacultyCoursePlan.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "EmployeePaySlipApproval") { PaySlipApproval.Visible = false; }
            if (dt.Rows[i]["PageId"].ToString() == "FacultyCoursePlanList") { FacultyCoursePlanList.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "FacultyDetails") { FacultyDetails.Visible = true; }
            //if(dt.Rows[i]["PageId"].ToString()=="AppraisalFormList") {liFile.Visible=true;}
            if (dt.Rows[i]["PageId"].ToString() == "FacultyFreeSlot") { FacultyFreeSlot.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "FacultyGrievances") { FacultyGrievances.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "FacultyLessonPlan") { if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC") { FacultyLessonPlanMD.Visible = true; } else { FacultyLessonPlan.Visible = true; } } //------------>
            if (dt.Rows[i]["PageId"].ToString() == "FacultyLoadCalculation") { FacultyLoadCalculation.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "FacultyTimeSheet") { if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC") { FacultyTimeSheetMD.Visible = true; } else { FacultyTimeSheet.Visible = true; } }
            if (dt.Rows[i]["PageId"].ToString() == "FacultyFeedback") { FacultyFeedback.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "IndentApproval") { IndentApproval.Visible = true; }

            SqlDataAdapter da = new SqlDataAdapter("select case when [Hospital HR Leave]=1 then 1 else 0 end as 'access' ,[Global Dimension 1 Code] from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where   [No_]='" + Session["uid"].ToString() + "' ", con.Con);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            if (dt1.Rows[0]["access"].ToString() == "1")
            {
                if (dt.Rows[i]["PageId"].ToString() == "Leave1") { Leave1.Visible = true; }
                if (dt.Rows[i]["PageId"].ToString() == "CoForStaff") { CoForStaff.Visible = true; }
                if (dt.Rows[i]["PageId"].ToString() == "ODFORSTAFF") { ODFORSTAFF.Visible = true; }

            }
            else
            {
                if (dt.Rows[i]["PageId"].ToString() == "Leave1") { Leave1.Visible = false; }

                if (Session["GlobalDimension1Coded"].ToString() == "TMMC" || Session["GlobalDimension1Coded"].ToString() == "TMPG")
                {

                    if (dt.Rows[i]["PageId"].ToString() == "Leave")
                    {
                        Leave.Visible = false;

                    }
                    if (dt.Rows[i]["PageId"].ToString() == "LeaveM")
                    {
                        // Bhupii block  
                        if (Session["uid"].ToString().Contains("TMRF"))
                        {
                            //LeaveM.Visible = false;
                        }
                        else
                        {

                            LeaveM.Visible = true;
                        }

                        //LeaveM.Visible = false;

                    }
                }
                else
                {
                    if (dt.Rows[i]["PageId"].ToString() == "Leave")
                    {
                        // Bhupii block 
                        if (Session["uid"].ToString().Contains("TMRF"))
                        {
                            // Leave.Visible = false;
                        }
                        else
                        {
                            //Anshul Leave Invisible
                            Leave.Visible = true;
                            //Leave.Visible = false;
                        }
                        //  Leave.Visible = false; 


                    }
                    if (dt.Rows[i]["PageId"].ToString() == "LeaveM")
                    {
                        LeaveM.Visible = false;

                    }
                }





                if (dt.Rows[i]["PageId"].ToString() == "CoForStaff") { CoForStaff.Visible = false; }
                if (dt.Rows[i]["PageId"].ToString() == "ODFORSTAFF") { ODFORSTAFF.Visible = false; }

            }
            //if (dt1.Rows[0]["Global Dimension 1 Code"].ToString() == "TMHS" || Session["uid"].ToString()=="TMU03651")
            //{
            //     liHR.Visible = true;
            //}
            //else
            //{
            //    liHR.Visible = false;
            //}
            //if(dt.Rows[i]["PageId"].ToString()=="AppraisalFormList") {indentview.Visible=true;}

            //   if (dt.Rows[i]["PageId"].ToString() == "liFine") { liFine.Visible = true; }


            if (dt.Rows[i]["PageId"].ToString() == "liHR") { liHR.Visible = true; }


            if (dt.Rows[i]["PageId"].ToString() == "liOthers") { liOthers.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "liProgram") { liProgram.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "liSAttendance") { liSAttendance.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "LoadReport") { liload.Visible = true; LoadReport.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "SecondaryLoad") { liload.Visible = true; SecondaryLoad.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "SecondaryLoadReport") { liload.Visible = true; SecondaryLoadReport.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "MapFacultySubject") { MapFacultySubject.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "OD_Application") { OD_Application.Visible = true; }
            //if (dt.Rows[i]["PageId"].ToString() == "ODFORSTAFF") { ODFORSTAFF.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "WFH") { WFH.Visible = true; }
            //if(dt.Rows[i]["PageId"].ToString()=="AppraisalFormList") {Payslipdetail.Visible=true;}
            if (Session["uid"].ToString() == "TMU02982" || Session["uid"].ToString() == "TMU08617" || Session["uid"].ToString() == "TMU00588")
            {
                PendingStudentForFeedbackEnty.Visible = true;
                liFeedback.Visible = true;
            }
            if (dt.Rows[i]["PageId"].ToString() == "RemedialClass") { RemedialClass.Visible = true; }
            if (Session["uid"].ToString() == "TMU02982" || Session["uid"].ToString() == "TMU08617" || Session["uid"].ToString() == "TMU00588")
            {
                ResultCoutForFeedback.Visible = true; liFeedback.Visible = true;

            }
            //if (dt.Rows[i]["PageId"].ToString() == "ReviewAttendance") { ReviewAttendance.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "ReviewAttendance_New")
            {
                if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC")
                { ReviewAttendance_NewMD.Visible = true; }
                else { ReviewAttendance_New.Visible = true; }
            }

            if (dt.Rows[i]["PageId"].ToString() == "EmployeeDutySchedule") { EmployeeDutySchedule.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "SmsEmployee") { liSMS.Visible = true; SmsEmployee.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "smsStudent") { liSMS.Visible = true; smsStudent.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "StudentAttendanceMark") { StudentAttendanceMark.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "UploadAttendance") { UploadAttendance.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "DetainedMarkAttendence") { DetainedMarkAttendence.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "StudentChart") { StudentChart.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "StudentCombinedAttendanceMark") { StudentCombinedAttendanceMark.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "StudentContinueAbsentReport") { StudentContinueAbsentReport.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "StudentDetaineeReport") { StudentDetaineeReport.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "StudentExtraAttendanceMark") { StudentExtraAttendanceMark.Visible = true; }


            if (Session["uid"].ToString() == "TMU02982" || Session["uid"].ToString() == "TMU08617" || Session["uid"].ToString() == "TMU00588")
            {

                StudentFeedBackList.Visible = true;

                liFeedback.Visible = true;


            }

            if (dt.Rows[i]["PageId"].ToString() == "HindiNameUpdate") { HindiNameUpdate.Visible = true; }
            // if (dt.Rows[i]["PageId"].ToString() == "StudentFineReport") { StudentFineReport.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "SubjectAllocationReport") { SubjectAllocationReport.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "summeryOfJoinAndLeave") { summeryOfJoinAndLeave.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "Team_Punch_Data" || Session["uid"].ToString() == "TMU06860")
            {

                Team_Punch_Data.Visible = true;



            }
            if (dt.Rows[i]["PageId"].ToString() == "UploadRoaster")
            {

                if (Session["GlobalDimension1Code"].ToString() == "TMHS")
                {
                    UploadRoaster.Visible = true;


                }



            }
            if (dt.Rows[i]["PageId"].ToString() == "PostRoaster")
            {

                if (Session["GlobalDimension1Code"].ToString() == "TMHS")
                {

                    PostRoaster.Visible = true;
                }



            }
            if (dt.Rows[i]["PageId"].ToString() == "PunchDataReport")
            {
                if (Session["uid"].ToString() == "TMU03651")
                {
                    PunchDataReport.Visible = true;
                }

            }

            if (dt.Rows[i]["PageId"].ToString() == "TimeTableUpload") { TimeTableUpload.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "ViewAttendance") { ViewAttendance.Visible = true; }
            if (Session["uid"].ToString() == "TMU03651")
            {
                ViewGrievances.Visible = true;
                ApplicationDetails.Visible = true;
                UploadRoaster.Visible = false;
                PostRoaster.Visible = false;
                EmployeeFine.Visible = false;
                FineList.Visible = false;
                ArrearR.Visible = false;
                ArrearList.Visible = false;
            }

            if (dt.Rows[i]["PageId"].ToString() == "ViewGrievances") { ViewGrievances.Visible = true; }



            if (dt.Rows[i]["PageId"].ToString() == "ViewTeamAttendance" || Session["uid"].ToString() == "TMU06860")
            {

                ViewTeamAttendance.Visible = true;



            }
            if (Session["uid"].ToString() == "TMU04426" || Session["uid"].ToString() == "TMU00478")
            {
                MembershipAprrovalForm.Visible = true;

            }
            if (Session["uid"].ToString() == "TMU00310" || Session["uid"].ToString() == "TMU00075" || Session["uid"].ToString() == "TMU00049")
            {
                EmployeeJoiningForm.Visible = true;


            }
            if (dt.Rows[i]["PageId"].ToString() == "EmployeeVerification") { EmployeeVerification.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "UserRoleMatrix") { UserRoleMatrix.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "MapCourseSubject") { MapCourseSubject.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "FormActiveInactive") { FormActiveInactive.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "SubjectChoice") { SubjectChoice.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "MapFacultySubject1") { MapFacultySubject1.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "SemRegistrationApproval") { SemRegApproval.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "Results") { ResultTab.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "OBEResult") { OBEResult.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "SurveyPending") { SurveyPending.Visible = true; }
            if (Session["uid"].ToString() == "TMU02982" || Session["uid"].ToString() == "TMU08617" || Session["uid"].ToString() == "TMU00588")
            {

                Formvisibilty.Visible = true;

            }
            if (Session["uid"].ToString() == "TMU07987" || Session["uid"].ToString() == "TMU05721" || Session["uid"].ToString() == "TMU08719")
            {

                ProfileUpdate.Visible = true;

            }

            if (dt.Rows[i]["PageId"].ToString() == "liMentorshipDetails") { liMentorshipDetails.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "AcademicCalendar") { AcademicCalendar.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "ExaminationApprovalForm") { ExaminationApprovalForm.Visible = true; ExamForm.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "StudentReExamination") { StudentReExamination.Visible = true; }


            if (dt.Rows[i]["PageId"].ToString() == "ExaminationFormRelease") { ExaminationFormRelease.Visible = true; ExamForm.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "ExamTimesheetApproval") { ExamTimesheetApproval.Visible = true; Timesheet.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "ExamTimesheetRelease") { ExamTimesheetRelease.Visible = true; Timesheet.Visible = true; }


            if (dt.Rows[i]["PageId"].ToString() == "InternalTimesheetRelease") { InternalTimesheetRelease.Visible = true; Timesheet.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "InternalTimesheetApproval") { InternalTimesheetApproval.Visible = true; Timesheet.Visible = true; }


            if (dt.Rows[i]["PageId"].ToString() == "MarksEntry") { MarksEntry.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "ReMarksEntry") { ReMarksEntry.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "MarksLock") { MarksLock.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "AttendanceApproval" || Session["uid"].ToString() == "TMU06152")
            {
                AttendanceApproval.Visible = true;

            }
            if (Session["uid"].ToString() == "TMU00865")
            {
                StudentReExamination1.Visible = true;

            }



            if (dt.Rows[i]["PageId"].ToString() == "MarksApproval") { MarksApproval.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "AdmitCard") { AdmitCard.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "ReleaseAdmitCard") { ReleaseAdmitCard.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "AdmitCardDownloadAllow") { AdmitCardDownloadAllow.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "AdmitCardApproval") { AdmitCardApproval.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "ADMITCARDTAB") { ADMITCARDTAB.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "Report") { Report.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "ReportInternalAssesment") { ReportInternalAssesment.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "OBEReport") { OBEReport.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "ReportInternalAward") { ReportInternalAward.Visible = true; }
            //if (dt.Rows[i]["PageId"].ToString() == "NEPExistsReport") { ReportInternalAward.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "ExternalAwardList") { ExternalAwardList.Visible = true; }
            //if (dt.Rows[i]["PageId"].ToString() == "ReAppearAwardList") { ReAppearAwardList.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "BlankReport") { BlankReport.Visible = true; Ulblank.Visible = true; }


            if (dt.Rows[i]["PageId"].ToString() == "BlankAwardList") { BlankAwardList.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "BlankExternalAwardList") { BlankExternalAwardList.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "BlankAttendenceReport") { BlankAttendenceReport.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "REBlankExternalAwardList") { REBlankExternalAwardList.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "REBlankAttendenceReport") { REBlankAttendenceReport.Visible = true; }



            if (dt.Rows[i]["PageId"].ToString() == "TransportVehicle") { TransportVehicle.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "Vehicleapproval") { Vehicleapproval.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "TransportVehicleapproval") { TransportVehicleapproval.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "managementvehicleapproval") { managementvehicleapproval.Visible = true; }

            if (dt.Rows[i]["PageId"].ToString() == "Result")
            {


                Result.Visible = true;

            }

            if (dt.Rows[i]["PageId"].ToString() == "Studentonlineresult") { Studentonlineresult.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "CertificateApproval") { YearBackApproval.Visible = true; }
            if (dt.Rows[i]["PageId"].ToString() == "CertificateApprovalHOD") { YearBackApprovalHOD.Visible = true; }
            if (Session["uid"].ToString() == "TMU05721")
            {
                liVisitingFaculty.Visible = true;
            }



        }
        if (Session["uid"].ToString() == "TMU03871")
        {
            CRCReport.Visible = true;
        }
        if (Session["uid"].ToString() == "TMU03651" || Session["uid"].ToString() == "TMU05294" || Session["uid"].ToString() == "TMU08026")
        {
            LiTagreport.Visible = true;
            LiReview.Visible = true;
        }
        if (Session["uid"].ToString() == "TMU05294")
        {

            NoDetain.Visible = true;

        }

        if (Session["uid"].ToString() == "TMU00049" || Session["uid"].ToString() == "TMU06618" || Session["uid"].ToString() == "TMU00075" || Session["uid"].ToString() == "TMU07987" || Session["uid"].ToString() == "TMU08719" || Session["uid"].ToString() == "TMU00142")
        {
            ODApplication.Visible = true;
        }
        if (Session["uid"].ToString() == "TMU00865" || Session["uid"].ToString() == "TMU00035" || Session["uid"].ToString() == "TMU00166")
        {
            migration.Visible = true;
        }
        if (Session["uid"].ToString() == "TMU08719")
        {
            TDSApproval.Visible = true;
        }
        if (Session["uid"].ToString() == "TMU00140")
        {
            NEPExistsReport.Visible = true;
        }
        if (Session["uid"].ToString() == "TMU04621")
        {
            PunchReport.Visible = true;
            Examination.Visible = false;
            // ViewTeamAttendance.Visible = false;
            // EmployeeVerification.Visible = false;
            // PunchCorrection.Visible = false;
            // Team_Punch_Data.Visible = false;
            WFH.Visible = false;
            // EventList.Visible = false;
            Courier.Visible = false;
            // ViewAttendance.Visible = false;
            // Employee_Punch_Data.Visible = false;
            // Leave.Visible = false;
            // OD_Application.Visible = false;
            //  Co_Application.Visible = false;
            //  AppraisalForm.Visible = false;
            // exitinterviewform.Visible = false;
            //CovidCertificate.Visible = false;
            // AppraisalForm.Visible = false;
            // AppraisalFormnonteaching.Visible = false;
        }

        if (Session["uid"].ToString() == "TMU05294" || Session["uid"].ToString() == "TMU00637")
        {
            Li6.Visible = true;
        }
        SqlDataAdapter dahand = new SqlDataAdapter("select [Employee Posting Group] from [EDUCOLLEGELIVE-R2].dbo.TMU$Employee   where   [No_]='" + Session["uid"].ToString() + "' ", con.Con);
        DataTable dthand = new DataTable();
        dahand.Fill(dthand);
        if (dthand.Rows[0]["Employee Posting Group"].ToString() == "TEACH")
        {
            Li5.Visible = true;
            ResearchIncentive.Visible = true;
        }
        else
        {
            Li5.Visible = false;
            ResearchIncentive.Visible = false;
        }

        SqlDataAdapter daPaySlip = new SqlDataAdapter("SELECT isnull(SUM(case when HODCode='" + Session["uid"].ToString() + "' then 1 else 0 end),0) HODCode,isnull(SUM(case when HRCode='" + Session["uid"].ToString() + "' then 1 else 0 end),0) HRCode FROM [EDUCOLLEGELIVE-R2].[dbo].[TMU$EmployeePaySlipInfo] where (HODCode='" + Session["uid"].ToString() + "' or HRCode='" + Session["uid"].ToString() + "')", con.Con);
        DataTable dtPaySlip = new DataTable();
        daPaySlip.Fill(dtPaySlip);
        if (Convert.ToInt32(dtPaySlip.Rows[0]["HODCode"]) > 0 || Convert.ToInt32(dtPaySlip.Rows[0]["HRCode"]) > 0)
        {
            PaySlipApproval.Visible = false;

        }
        SqlDataAdapter Outhand = new SqlDataAdapter("select count([Warden Emp Code]) C from [EDUCOLLEGELIVE-R2].[dbo].TMU$Hostel where [Warden Emp Code]='" + Session["uid"].ToString() + "' ", con.Con);
        DataTable dtOut = new DataTable();
        Outhand.Fill(dtOut);
        if (Convert.ToInt32(dtOut.Rows[0]["C"]) > 0 || Session["uid"].ToString() == "TMU00459")
        {

            AOutgoing1.Visible = true;
            HostelAttendance.Visible = true;

        }
        if (Session["uid"].ToString() == "TMU01379" || Session["uid"].ToString() == "TMU02524" || Session["uid"].ToString() == "TMU01846" || Session["uid"].ToString() == "TMU05114" || Session["uid"].ToString() == "TMU06569" || Session["uid"].ToString() == "TMU05894" || Session["uid"].ToString() == "TMU08438" || Session["uid"].ToString() == "TMU03982" || Session["uid"].ToString() == "TMU03162" || Session["uid"].ToString() == "TMU07208" || Session["uid"].ToString() == "TMU07946" || Session["uid"].ToString() == "TMU09285" || Session["uid"].ToString() == "TMU09293" || Session["uid"].ToString() == "TMU09490")
        {
            AOutgoing.Visible = true;
        }

        SqlDataAdapter daEQ = new SqlDataAdapter(" SELECT Count([Asset No_]) as 'ASCOUNT'   FROM   [NAAC_ADV_TEST].dbo.[TMU Hospital$Alloted Person] with(NOLOCK) where [Issue ID]='" + Session["uid"].ToString() + "'", con.Con);
        DataTable dtEQ = new DataTable();
        daEQ.Fill(dtEQ);
        if (Convert.ToInt32(dtEQ.Rows[0]["ASCOUNT"]) > 0)
        {
            EqCompl.Visible = true;
        }
        if (Session["uid"].ToString() == "TMU00865")
        {
            StudentReExamination1.Visible = true;
        }        
        //if (Session[].ToString() == "1")
        //{
        //    ReviewAttendance_New.Visible = false;
        //    ReviewAttendance_NewMD.Visible = true;
        //    FacultyTimeSheet.Visible = false;
        //    FacultyTimeSheetMD.Visible = true;
        //    FacultyLessonPlan.Visible = false;
        //    FacultyLessonPlanMD.Visible = true;
        //}
    }
    public string GetLinkYesNo(string PageId)
    {
        SqlCommand cmd = new SqlCommand("SP_GetFormVisibilityYesNo", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@LoginId", Session["uid"].ToString());
        cmd.Parameters.Add("@PageId", PageId);
        if (con1.State == ConnectionState.Closed)
            con1.Open();
        string Result = cmd.ExecuteScalar().ToString();
        con1.Close();
        return Result;
    }
    public void showlib()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            string strSQL = "select  count(*) HOD from Tbl_LibraryMembership  where [HOD] ='" + Session["uid"].ToString() + "'";
            {
                SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (Convert.ToInt32(dt.Rows[0]["HOD"]) > 0)
                {

                    MembershipAprrovalForm.Visible = true;
                }

            }
        }
    }
    protected void lblMarkAtt_Click(object sender, EventArgs e)
    {
        Response.Redirect("MarkAttendanceMobile.aspx");
    }
    //----------------------------Ashu FormMenu Show-Hide------------on 06-04-2017---------------END
    //protected void lblPayTM_Click(object sender, EventArgs e)
    //{
    //    int temp = 0;
    //    string orderid = "TMUSPORT" + DateTime.Now.Ticks.ToString();


    //    con1.Open();
    //    SqlCommand cmd = new SqlCommand("INSERT INTO [OnlinePaymentLog]([Payment Date],[Amount],[Status],[UserID],[GATEWAY],[GatewayStatus],[OrderID],[CLE Entry No],[Semester FEE],[Year FEE],[Temp 1],[Temp 2],[Temp 3],[Orgnized Date])VALUES(GETUTCDATE(),1 ,0 ,'" + Session["uid"].ToString() + "' ,'PAYTM'  ,0 ,'" + orderid + "','','','','',0,'', DATEADD(Minute,330,Getdate()))", con1);
    //    temp = cmd.ExecuteNonQuery();
    //    con1.Close();
    //    if (temp == 1)
    //    {

    //        Dictionary<string, string> parameters = new Dictionary<string, string>();



    //        //-------->Test
    //        String merchantKey = "7v_qN#jfvvCiLSOB";
    //        // Dictionary<string, string> parameters = new Dictionary<string, string>();
    //        parameters.Add("MID", "Teerth64420690832928");
    //        parameters.Add("CHANNEL_ID", "WEB");
    //        parameters.Add("INDUSTRY_TYPE_ID", "Education");
    //        parameters.Add("WEBSITE", "DEFAULT");
    //        parameters.Add("EMAIL", "");
    //        parameters.Add("MOBILE_NO", "");
    //        //parameters.Add("CUST_ID", Session["uid"].ToString().Replace("/", "").ToString());
    //        parameters.Add("CUST_ID", Session["uid"].ToString());
    //        parameters.Add("ORDER_ID", orderid);
    //        parameters.Add("TXN_AMOUNT", "1");
    //        //parameters.Add("EXTENDINFO", Session["uid"].ToString());
    //        // parameters.Add("mercUnqRef", Session["enroll"].ToString());

    //        // parameters.Add("CALLBACK_URL", "http://172.0.1.105:82/Student/PaYTMSport.aspx");//http://14.139.238.130:82/
    //        parameters.Add("CALLBACK_URL", "http://localhost:1049/TMUiZone/Faculty/PaYTMSport.aspx");
    //        string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
    //        string paytmURL = "https://securegw.paytm.in/order/process";
    //        //string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + orderid;
    //        Session["uid"] = null;

    //        string outputHTML = "<html>";






    //        outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
    //        outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
    //        outputHTML += "<table border='1'>";
    //        outputHTML += "<tbody>";
    //        foreach (string key in parameters.Keys)
    //        {
    //            outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
    //        }
    //        outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
    //        outputHTML += "</tbody>";
    //        outputHTML += "</table>";
    //        outputHTML += "<script type='text/javascript'>";
    //        outputHTML += "document.f1.submit();";
    //        outputHTML += "</script>";
    //        outputHTML += "</form>";
    //        outputHTML += "</script>";
    //        outputHTML += "</body>";
    //        outputHTML += "</html>";
    //        Response.Write(outputHTML);
    //    }
    //}
    public void showFellowshipEmployee()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            string strSQL = "select count(*)  Employee_Code from [HRMSPortal].dbo.Tbl_ResearchFellowEmployee  where [Employee_Code] ='" + Session["uid"].ToString() + "'";

            {
                SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (Convert.ToInt32(dt.Rows[0]["Employee_Code"]) > 0)
                {
                    FellowshipForm.Visible = true;
                }
            }
        }
    }
    public void hidehr()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            string strSQL = "select replace([Total Duration],'Days','')-DATEDIFF(day, [Date Of Resignation],getdate()) as ResigDay from  [HRMSPortal].dbo.tble_Exit_Interview_Form  where [Employee Code] ='" + Session["uid"].ToString() + "' and Status='Submit' ";
            {
                SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["ResigDay"]) <= 4 && Session["GlobalDimension1Code"].ToString() != "TMMC")
                    {
                        No_Dues.Visible = true;
                    }
                    else
                    {
                        No_Dues.Visible = true;
                    }
                }
            }
        }
    }
}
