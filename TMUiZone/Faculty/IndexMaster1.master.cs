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


public partial class IndexMaster : System.Web.UI.MasterPage
{
    Connection Portalcon;
    ServicePoratal con;
    string HRHODsame = "";
    string forHRisHOD = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            ////Session["IndentApproval_DH"] = dr["Indent Approval"].ToString();
            //  Session["IndentApproval_Dept_DH"] = dr["Department"].ToString();

            if (Session["uname"].ToString() == null || Session["Company"].ToString() == null || Session["UserGroup"].ToString() == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                //----------------------------------User Role-------------------
                //-----------Indent ----
                if (Session["IndentEntry_DH"].ToString() == "1")
                {
                    liIndentRequest.Visible = true;
                    liOthers.Visible = true;

                }
                if (Session["IndentApprovalID"].ToString() != "")
                {
                    liIndentApproval.Visible = true;
                    liOthers.Visible = true;

                }
                //-----------Indent ----
                if (Session["UserGroup"].ToString() == "ADMIN")
                {
                    liEnquiry.Visible = true;
                    liEnquiryList.Visible = true;
                    liReviewAttendance.Visible = true;
                    liTimeSheet.Visible = true;
                    liLeave.Visible = true;
                    liSAttendance.Visible = true;
                    liProgram.Visible = true;
                    liHR.Visible = true;
                    span1.Visible = true;
                    span2.Visible = true;
                    span3.Visible = true;
                    liMapFacultySubjects.Visible = true;
                    liViewGrievances.Visible = true;
                    liDashboard.Visible = true;
                    liDailyAttendance.Visible = true;

                    liEnquiryPhone.Visible = false;
                    liTimeTableCreation.Visible = false;
                    liTimeTableUpload.Visible = false;
                    liMarkAttendance.Visible = false;
                    liExtraAttendance.Visible = false;
                    liExtraAttendance.Visible = false;
                    liCombinedAttendance.Visible = false;
                    liFeedback.Visible = false;
                    liAssignment.Visible = false;
                    liLessonPlan.Visible = false;
                    liFine.Visible = false;
                    liMentorshipDetails.Visible = false;
                    lnkSetup.Visible = false;
                    liFacultyFreeSlot.Visible = false;
                    // lnkPaySlip.Visible = true;                     
                    liEditStudentAttendance.Visible = false;

                }
                if (Session["UserGroup"].ToString() == "STAFF")
                {
                    //liProfile.Visible = true;
                    liFileUpload.Visible = false;
                    liMarkAttendance.Visible = false;
                    liExtraAttendance.Visible = false;
                    liCombinedAttendance.Visible = false;
                    liFeedback.Visible = false;
                    liTimeSheet.Visible = false;
                    liAssignment.Visible = false;
                    liReviewAttendance.Visible = false;
                    liLessonPlan.Visible = false;
                    liFine.Visible = false;
                    liMentorshipDetails.Visible = false;

                    liLeave.Visible = true;
                    lnkSetup.Visible = false;
                    liFacultyFreeSlot.Visible = false;
                    // lnkPaySlip.Visible = true;
                    liSAttendance.Visible = false;
                    liProgram.Visible = false;
                    liHR.Visible = true;
                    span1.Visible = false;
                    span2.Visible = false;
                    span3.Visible = true;
                    liMapFacultySubjects.Visible = false;
                    liEditStudentAttendance.Visible = false;
                }
                if (Session["UserGroup"].ToString() == "COUNSELLOR")
                {
                    liFileUpload.Visible = false;
                    liEnquiryPhone.Visible = true;
                    liEnquiry.Visible = true;
                    liEnquiryList.Visible = true;
                    //liProfile.Visible = true;
                    liMarkAttendance.Visible = false;
                    liExtraAttendance.Visible = false;
                    liCombinedAttendance.Visible = false;
                    liFeedback.Visible = false;
                    liTimeSheet.Visible = false;
                    liAssignment.Visible = false;
                    liReviewAttendance.Visible = false;
                    liLessonPlan.Visible = false;
                    liFine.Visible = false;
                    liMentorshipDetails.Visible = false;
                    liLeave.Visible = true;
                    lnkSetup.Visible = false;
                    liFacultyFreeSlot.Visible = false;
                    liSAttendance.Visible = false;
                    // lnkPaySlip.Visible = true;                    
                    liProgram.Visible = false;
                    liHR.Visible = true;
                    span1.Visible = false;
                    span2.Visible = false;
                    span3.Visible = false;
                    liMapFacultySubjects.Visible = false;
                    liEditStudentAttendance.Visible = false;

                }

                if (Session["UserGroup"].ToString() == "FACULTY")// sandeep 16/02/2017
                {
                    liFine.Visible = false;
                    liMapFacultySubjects.Visible = true;
                    liEditStudentAttendance.Visible = false;
                    liDetaineeList.Visible = true;
                    liOthers.Visible = true;
                   
                    if (Session["Proctor"].ToString() != "")
                    {
                        liFine.Visible = true;
                        liStudentFineReport.Visible = true;
                    }
                    if (Session["EventCo-Ordinator"].ToString() != "")
                    {
                        LiEvent.Visible = true;
                        liEditStudentAttendance.Visible = true;
                    }
                    if (Session["Hod"].ToString() != "")
                    {
                        liAssignSubject.Visible = true;
                        liContinueAbsent.Visible = true;
                    }
                }
                if (Session["UserGroup"].ToString() == "HOD")
                {
                    liAssignSubject.Visible = true;
                    liContinueAbsent.Visible = true;
                }
                if (Session["UserGroup"].ToString() == "HR")
                {
                    liFeedbackCount.Visible = true; liFeedBackList.Visible = true;
                    liMapFacultySubjects.Visible = true;
                    liEditStudentAttendance.Visible = false;
                    liexitinterviewformDetails.Visible = true;
                    liFeedbackCount.Visible = true;
                    liFeedBackList.Visible = true;
                    liPendingFeedback.Visible = true;
                }
                if (Session["UserGroup"].ToString() == "PRINCIPAL")
                {
                    liDailyAttendance.Visible = true;
                    // liFacultyLoad.Visible = true;
                    liDetaineeList.Visible = true;
                    liContinueAbsent.Visible = true;
                    liPendingFeedback.Visible = true;
                    liSmsEmployee.Visible = true;
                    lismsStudent.Visible = true;
                    liAssignmentView.Visible = true;
                    liOthers.Visible = true;
                    liexitinterviewformDetails.Visible = true;
                    liRemedialClass.Visible = true;
                    liViewGrievances.Visible = true;
                    liSubjectAllocationReport.Visible = true;
                    LiEvent.Visible = true;
                    liDismissLecture.Visible = true;
                    liAppraisalFormList.Visible = true;
                    liAssignSubject.Visible = true;
                  

                }
                if (Session["UserGroup"].ToString() == "REGISTRAR")
                {
                    liFeedbackCount.Visible = true; liFeedBackList.Visible = true; liPendingFeedback.Visible = true;
                    liAppraisalFormList.Visible = true;
                    liSAttendance.Visible = false;
                    liMarkAttendance.Visible = false;
                    liExtraAttendance.Visible = false;
                    liProgram.Visible = false;
                    liFine.Visible = false;
                    liMentorshipDetails.Visible = false;
                    liexitinterviewformDetails.Visible = true;
                    liMapFacultySubjects.Visible = true;
                    liOthers.Visible = true; liSmsEmployee.Visible = true; lismsStudent.Visible = true;
                    liFacultyGrievancesList.Visible = true;
                    liEnquiry.Visible = true;
                    liEnquiryList.Visible = true;
                    liEnquiryPhone.Visible = true;
                    lisummeryOfJoinAndLeave.Visible = true;
                    LiEvent.Visible = true;
                }
                
                   
                Portalcon = new Connection();
                con = new ServicePoratal();
                lbluserid666.Text = Session["uid"].ToString();
                lblName.Text = Session["uname"].ToString();

                if (!IsPostBack)
                {
                    BINDimage();
                }
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
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
                {
                    liContinueAbsent.Visible = true;
                    liAssignmentView.Visible = true;
                    liDailyAttendance.Visible = true;
                }
                else
                {
                    liContinueAbsent.Visible = false;
                    liAssignmentView.Visible = false;
                    liDailyAttendance.Visible = false;
                }
               // liContinueAbsent.Visible = true;
            }
            GoogleLink1();
            showHRHODisexhist();
            if (Session["uid"].ToString() == "TMU00864")
            {
                EmployeeAttendance.Visible = true;
                viewteamattendace.Visible = false;
            }

            if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC") //12-04-2017 for DENTAL AND MEDICAL
            {
                liExtraAttendance.Visible = false;
                if (Session["UserGroup"].ToString() != "PRINCIPAL")
                {
                    liDetaineeList.Visible = false;
                    liDailyAttendance.Visible = false;
                    liContinueAbsent.Visible = false;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
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

            viewteamattendace.Visible = true;
            lnk_Team_Punch_Data.Visible = true;
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
            viewteamattendace.Visible = false;
            lnk_Team_Punch_Data.Visible = false;
        }
    }


    string HODapr = "";
    string HRapr = "";
    string Blankapr = "";
    string PriorityHRapr = "";
    string PriorityHODapr = "";
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
    string HODaprLeave = "";
    string HRaprLeave = "";
    string BlankaprLeave = "";
    string PriorityHRaprLeave = "";
    string PriorityHODaprLeave = "";
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
    string HODaprReimb = "";
    string HRaprReimb = "";
    string BlankaprReimb = "";
    string PriorityHRaprReimb = "";
    string PriorityHODaprReimb = "";
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
    string HODaprAttend = "";
    string HRaprAttend = "";
    string BlankaprAttend = "";
    string PriorityHRaprAttend = "";
    string PriorityHODaprAttend = "";
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
}
