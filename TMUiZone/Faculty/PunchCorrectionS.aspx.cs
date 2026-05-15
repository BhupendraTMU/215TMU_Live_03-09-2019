using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Faculty_PunchCorrectionS : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    Connection navconn;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            navconn = new Connection();
            //clndAppliedate.StartDate = DateTime.Now.AddDays(-10);
            //clndAppliedate.EndDate = DateTime.Now.AddDays(0);
            if (!IsPostBack)
            {

                BindYear();
                ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
                ddlYear1.SelectedValue = System.DateTime.Now.ToString("yyyy");
                DropDownList1.SelectedValue = System.DateTime.Now.ToString("MM");
                //Response.Redirect("Error.aspx", false);
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Local Process
                //Response.Redirect("~/Faculty/Error.aspx", false);
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Live
                //Response.Redirect("Error.aspx", false);
                //HttpContext.Current.ApplicationInstance.CompleteRequest();

                //String sDate = DateTime.Now.ToString();
                //DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

                //int day = datevalue.Day;
                //int mn = datevalue.Month;
                //int yy = datevalue.Year;
                //DateTime now = DateTime.Now;
                //DateTime lastDayOfLastMonth = now.Date.AddDays(-now.Day);
                //int lastMonthLastDay = lastDayOfLastMonth.Day;
                //if (day <= 20)
                //{
                //    //clndAppliedate.StartDate = new DateTime(2021, 12, lastMonthLastDay);

                //    clndAppliedate.StartDate = new DateTime(2022, 12, 15);
                //}
                //else
                //{
                //    clndAppliedate.StartDate = new DateTime(yy, mn, 1);
                //    //clndAppliedate.StartDate = new DateTime(yy, mn, day - 1);
                //}
                ////CalendarExtender2.StartDate = new DateTime(yy, mn, 01);
                String sDate = DateTime.Now.ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

                int day = datevalue.Day;
                int mn = datevalue.Month;
                int yy = datevalue.Year;
                if (day > 3)
                {
                    clndAppliedate.StartDate = new DateTime(yy, mn, 1);



                }
                else
                {

                    if (mn == 1)
                    {
                        clndAppliedate.StartDate = new DateTime(2025, 12, 1);
                        clndAppliedate.EndDate = new DateTime(2025, 12, 31);

                    }
                    else
                    {
                        clndAppliedate.StartDate = new DateTime(yy, mn - 1, 1);
                        clndAppliedate.EndDate = new DateTime(yy, mn, 3);
                    }

                }

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
    public void BindYear()
    {

        int Currentyear = System.DateTime.Now.Year;

        for (int i = Currentyear - 1; i <= Currentyear; i++)
        {

            ddlYear1.Items.Add(i.ToString());
        }
        ddlYear1.Items.Add(Currentyear.ToString());

    }
    public void ShowPendingApprovalCount()
    {


        if (Session["uid"].ToString() == "TMU06850")
        {
            if (con2.State.ToString() != "Open")
            {
                con2.Open();
            }
            SqlDataAdapter da1 = new SqlDataAdapter("Select sum(T.ApprovalStatus) as 'ApprovalStatus' from (select count(ApprovalStatus) as ApprovalStatus from [tbl_PunchCorrect_ApplicationS] where   CompanyName='TMU' and Co_Leave='1' and ApprovalStatus='Approved' and HRStatus='Pending' and CompanyName='TMU'	 and Co_Leave='1' ) T", con2);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            lblCountODAppoval.Text = dt1.Rows[0]["ApprovalStatus"].ToString();
            con2.Close();
        }
        else if (Session["uid"].ToString() == "TMU01023")
        {
            if (con2.State.ToString() != "Open")
            {
                con2.Open();
            }
            try
            {
                SqlDataAdapter da1 = new SqlDataAdapter("Select sum(T.ApprovalStatus) as 'ApprovalStatus' from (select count(ApprovalStatus) as ApprovalStatus from [tbl_PunchCorrect_ApplicationS] where  CompanyName='TMU' and Co_Leave='1' and ApprovalStatus='Approved' and FinalStatus='Pending' and CompanyName='TMU'	 and Co_Leave='1' ) T", con2);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                lblCountODAppoval.Text = dt1.Rows[0]["ApprovalStatus"].ToString();
                con2.Close();
            }
            catch (Exception ex)
            {
            }
        }
        else
        {

            if (con2.State.ToString() != "Open")
            {
                con2.Open();
            }
            SqlDataAdapter da1 = new SqlDataAdapter("Select sum(T.ApprovalStatus) as 'ApprovalStatus' from (select count(ApprovalStatus) as ApprovalStatus from [tbl_PunchCorrect_ApplicationS] where ([HODUserID]='" + Session["uid"].ToString() + "' or [HOD2]='" + Session["uid"].ToString() + "' or  FinalApprovalID1='" + Session["uid"].ToString() + "')     and  CompanyName='TMU' and Co_Leave='1' and (ApprovalStatus='Pending' or FinalStatus1='0') and CompanyName='TMU'	 and Co_Leave='1' ) T", con2);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                lblCountODAppoval.Text = dt1.Rows[0]["ApprovalStatus"].ToString();
            }
            else
            {
                lblCountODAppoval.Text = "0";
            }
            con2.Close();
        }



    }
    public void VisiblilitybyHOD()
    {
        SqlDataReader dr = navconn.SHow_showHODExhistCO(Session["uid"].ToString(), Session["CompanyTableEmployee"].ToString());
        dr.Read();
        if (con2.State.ToString() != "Open")
        {
            con2.Open();
        }
        SqlDataAdapter da1 = new SqlDataAdapter("select * from (select distinct count(*) as 'Sanctioning',(Select distinct [Multi Approval Punch] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Dimension Value] where Code=e.[Global Dimension 1 Code]) as 'M' from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] e where ([Sanctioning Incharge]='" + Session["uid"].ToString() + "' or HOD='" + Session["uid"].ToString() + "' or  [HOD 1]='" + Session["uid"].ToString() + "') group by [Global Dimension 1 Code] ) T where T.M=1", con2);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        con2.Close();

        if (dr.HasRows)
        {
            dr.Close();
            navconn.DisConnect();

            lnkApproval.Visible = true;
            lnkEmployeeReport.Visible = true;
            lblCountODAppoval.Visible = true;
            ShowPendingApprovalCount();
        }
        else if (dt1.Rows.Count > 0)
        {
            if (Convert.ToInt32(dt1.Rows[0]["Sanctioning"]) > 0 && dt1.Rows[0]["M"].ToString() == "1")
            {

                dr.Close();
                navconn.DisConnect();

                lnkApproval.Visible = true;
                lnkEmployeeReport.Visible = true;
                lblCountODAppoval.Visible = true;
                ShowPendingApprovalCount();
            }
            else
            {
                dr.Close();
                navconn.DisConnect();
                lnkApproval.Visible = false;
                lnkEmployeeReport.Visible = false;
                lblCountODAppoval.Visible = false;
            }
        }
        else
        {


            dr.Close();
            navconn.DisConnect();
            lnkApproval.Visible = false;
            lnkEmployeeReport.Visible = false;
            lblCountODAppoval.Visible = false;
        }



    }
    protected void lnkODApplication_Click(object sender, EventArgs e)
    {
        lblHeader.Text = "Application";
        pnlViewStatus.Visible = false;
        pnlCOApplication.Visible = true;
        pnlApproval.Visible = false;
        lblCOSuccess.Visible = false;
        pnlEmployeeReport.Visible = false;
        clear();
    }
    protected void lnkODView_Click(object sender, EventArgs e)
    {
        lblHeader.Text = "Report";
        pnlViewStatus.Visible = true;
        pnlCOApplication.Visible = false;
        pnlApproval.Visible = false;
        pnlEmployeeReport.Visible = false;
        txtFromDate_ViewStatus.Text = "";
        txtTodate_ViewStatus.Text = "";
        ddStatus_ViewStatus.SelectedValue = "Pending";
        txtFromDate_ViewStatus.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        txtTodate_ViewStatus.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        Show_Report_ofuser();
    }

    public void Show_Report_ofuser()
    {
        DataTable dt = new DataTable();
        if (ddStatus_ViewStatus.SelectedValue.Trim() == "All")
        {

            if (con2.State.ToString() != "Open")
            {
                con2.Open();
            }

            if (Session["uid"].ToString() == "TMU06850")
            {
                SqlDataAdapter da1 = new SqlDataAdapter("select * from [tbl_PunchCorrect_ApplicationS] where convert(date,Atte_Date,103)>='" + txtFromDate_ViewStatus.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + txtTodate_ViewStatus.Text.Trim() + "' and Userid='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1' ", con2);

                da1.Fill(dt);

            }
            else if (Session["uid"].ToString() == "TMU01023")
            {
                SqlDataAdapter da1 = new SqlDataAdapter("select * from [tbl_PunchCorrect_ApplicationS] where convert(date,Atte_Date,103)>='" + txtFromDate_ViewStatus.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + txtTodate_ViewStatus.Text.Trim() + "' and Userid='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1' ", con2);

                da1.Fill(dt);
            }
            else
            {
                SqlDataAdapter da1 = new SqlDataAdapter("select * from [tbl_PunchCorrect_ApplicationS] where convert(date,Atte_Date,103)>='" + txtFromDate_ViewStatus.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + txtTodate_ViewStatus.Text.Trim() + "' and Userid='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1' ", con2);

                da1.Fill(dt);
            }





            grdView_Status.DataSource = dt;
            grdView_Status.DataBind();
            con2.Close();
        }
        else
        {


            if (con2.State.ToString() != "Open")
            {
                con2.Open();
            }


            SqlDataAdapter da1 = new SqlDataAdapter("select * from tbl_PunchCorrect_ApplicationS where convert(date,Atte_Date,103)>='" + txtFromDate_ViewStatus.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + txtTodate_ViewStatus.Text.Trim() + "' and Userid='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and ApprovalStatus='" + ddStatus_ViewStatus.SelectedValue.Trim() + "' and Co_Leave='1' ", con2);

            da1.Fill(dt);
            grdView_Status.DataSource = dt;
            grdView_Status.DataBind();
            con2.Close();
        }
    }

    protected void lnkApproval_Click(object sender, EventArgs e)
    {


        if (Session["uid"].ToString() == "TMU06850")
        {
            grdApproval.Columns[11].Visible = false;
            grdApproval.Columns[12].Visible = true;
            //grdApproval.Columns[12].Visible = true;
            grdApproval.Columns[16].Visible = true;
        }
        else if (Session["uid"].ToString() == "TMU01023")
        {
            grdApproval.Columns[11].Visible = false;
            grdApproval.Columns[14].Visible = true;
            grdApproval.Columns[16].Visible = true;
        }
        else
        {
            grdApproval.Columns[11].Visible = true;
            grdApproval.Columns[12].Visible = false;
            grdApproval.Columns[16].Visible = false;
        }


        btnApprove.Text = "Approval";
        lblHeader.Text = "Approval";
        lnkApproval.Text = "Approval";
        pnlViewStatus.Visible = false;
        pnlCOApplication.Visible = false;
        pnlApproval.Visible = true;
        btnFIlterGet_Approval.Visible = false;
        ddStatus_Approval.SelectedValue = "Pending";
        //pnlFilterDate.Visible = false;
        //pnlFilterByIDName.Visible = false;
        //rdDatewise.Checked = false;
        //rdEmployeeID.Checked = false;
        //rdEmployeeName.Checked = false;
        Show_HODData();
        pnlEmployeeReport.Visible = false;
    }






    public void Show_PunchApprovalPendingMedical(string HOD, string Approval_status, string HOD1, string Company)
    {

        DataTable dt1 = new DataTable();
        if (con2.State.ToString() != "Open")
        {
            con2.Open();
        }
        if (Session["uid"].ToString() == "TMU06850")
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select *,(select convert(char(5), [Shift Time In], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=RR.Userid and [Attendance Date]= RR.Atte_Date) as 'Shift IN',(select convert(char(5), [Shift Time Out], 108) from   [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=  [tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]= RR.Atte_Date) as 'Shift Out',  CONCAT( case when Correction_Type=1 then 0   else DATEDIFF(MINUTE, (select convert(char(5), [Shift Time In], 108) from    [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate    Latin1_General_CI_AI=RR.Userid and [Attendance Date]=   RR.Atte_Date) , fromTime) + case when DATEDIFF(MINUTE,ToTime,    (select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid and [Attendance Date]=RR.Atte_Date) )<0 then 0 else DATEDIFF(MINUTE,ToTime, (select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=RR.Userid and [Attendance Date]=RR.Atte_Date)) end end,'  ','(',(select COUNT(Userid) from tbl_PunchCorrect_ApplicationS where MONTH(Atte_Date)=Month(RR.Atte_Date) and YEAR(Atte_Date)=YEAR(RR.Atte_Date) and Userid=RR.Userid and (ApprovalStatus='Approved') ),')') AS MinuteDiff,HODName from [tbl_PunchCorrect_ApplicationS] RR where  ((([HODUserID]='" + Session["uid"].ToString() + "' or [HOD2]='" + Session["uid"].ToString() + "' or [HRID]='" + Session["uid"].ToString() + "') and (ApprovalStatus='Pending' or HRStatus='Pending') and (isnull(FinalStatus,0)=0 or HRStatus='Pending')  or  ((FinalStatus=0 or HRStatus='Pending') and  (FinalApprovalID='" + Session["uid"].ToString() + "' or [HRID]='" + Session["uid"].ToString() + "' )  and (ApprovalStatus='Recommend' or  HRStatus='Pending')))  and  CompanyName='TMU' and Co_Leave='1'    and CompanyName='TMU' and Co_Leave='1' ", con2);

            da1.Fill(dt1);
        }
        else if (Session["uid"].ToString() == "TMU01023")
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select *,(select convert(char(5), [Shift Time In], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=RR.Userid and [Attendance Date]= RR.Atte_Date) as 'Shift IN',(select convert(char(5), [Shift Time Out], 108) from   [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=  [tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]= RR.Atte_Date) as 'Shift Out',  CONCAT( case when Correction_Type=1 then 0   else DATEDIFF(MINUTE, (select convert(char(5), [Shift Time In], 108) from    [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate    Latin1_General_CI_AI=RR.Userid and [Attendance Date]=   RR.Atte_Date) , fromTime) + case when DATEDIFF(MINUTE,ToTime,    (select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid and [Attendance Date]=RR.Atte_Date) )<0 then 0 else DATEDIFF(MINUTE,ToTime, (select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=RR.Userid and [Attendance Date]=RR.Atte_Date)) end end,'  ','(',(select COUNT(Userid) from tbl_PunchCorrect_ApplicationS where MONTH(Atte_Date)=Month(RR.Atte_Date) and YEAR(Atte_Date)=YEAR(RR.Atte_Date) and Userid=RR.Userid and (ApprovalStatus='Approved') ),')') AS MinuteDiff,HODName from [tbl_PunchCorrect_ApplicationS] RR where  ((([HODUserID]='" + Session["uid"].ToString() + "' or [HOD2]='" + Session["uid"].ToString() + "' or [HRID]='" + Session["uid"].ToString() + "') and (ApprovalStatus='Pending' or HRStatus='Pending') and (isnull(FinalStatus,0)=0 or HRStatus='Pending')  or  ((FinalStatus=0 or HRStatus='Pending') and  (FinalApprovalID='" + Session["uid"].ToString() + "' or [HRID]='" + Session["uid"].ToString() + "' )  and (ApprovalStatus='Recommend' or  HRStatus='Pending')))  and  CompanyName='TMU' and Co_Leave='1'    and CompanyName='TMU' and Co_Leave='1' ", con2);

            da1.Fill(dt1);
        }

        else
        {

            try
            {
                SqlDataAdapter da1 = new SqlDataAdapter("select *,(select convert(char(5), [Shift Time In], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=[tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]=[tbl_PunchCorrect_ApplicationS].Atte_Date) as 'Shift IN',(select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=[tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]= [tbl_PunchCorrect_ApplicationS].Atte_Date) as 'Shift Out', case when Correction_Type=1 then 0 else  DATEDIFF(MINUTE, (select convert(char(5), [Shift Time In], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data]   where [Employee No] collate Latin1_General_CI_AI=[tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]=  [tbl_PunchCorrect_ApplicationS].Atte_Date) , fromTime) + case when DATEDIFF(MINUTE,ToTime, (select convert(char(5),   [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No]   collate Latin1_General_CI_AI=[tbl_PunchCorrect_ApplicationS].Userid and   [Attendance Date]=[tbl_PunchCorrect_ApplicationS].Atte_Date) )<0 then 0 else DATEDIFF(MINUTE,ToTime, (select convert(char(5),  [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No]   collate Latin1_General_CI_AI=[tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]=[tbl_PunchCorrect_ApplicationS].Atte_Date) )   end end AS MinuteDiff,HODName from [tbl_PunchCorrect_ApplicationS] where ((([HODUserID]='" + Session["uid"].ToString() + "' or [HOD2]='" + Session["uid"].ToString() + "') and ApprovalStatus='Pending' and isnull(FinalStatus,0)='0')  or (FinalStatus1='0' and FinalApprovalID1='" + Session["uid"].ToString() + "' and ApprovalStatus='Recommend'))   and  CompanyName='TMU' and Co_Leave='1'    and CompanyName='TMU' and Co_Leave='1' ", con2);

                da1.Fill(dt1);
            }
            catch (Exception ex)
            {
            }
        }
        con2.Close();
        //if (dt1.Rows.Count > 0)
        //{
        //    grdApproval.DataSource = dt1;
        //    grdApproval.DataBind();
        //}
        //else
        //{
        //    grdApproval.DataSource = "";
        //    grdApproval.DataBind();
        //}




        if (dt1.Rows.Count > 0)
        {
            btnApprove.Visible = true;
            btnReject.Visible = true;
        }
        else
        {
            btnApprove.Visible = false;
            btnReject.Visible = false;
        }


        if (dt1.Rows.Count > 0)
        {
            grdApproval.DataSource = dt1;
            grdApproval.DataBind();
        }
        else
        {
            grdApproval.DataSource = "";
            grdApproval.DataBind();
        }
















    }







    public void Show_PunchApprovalPending(string HOD, string Approval_status, string HOD1, string Company)
    {
        DataTable dt1 = new DataTable();
        if (con2.State.ToString() != "Open")
        {
            con2.Open();
        }
        if (Session["uid"].ToString() == "TMU06850")
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select ApprovalStatus,Userid,Uname,Atte_Date,(select  convert(nvarchar(5),convert(time,min([Punch Time]))) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Device Punches] where [Employee Machine Code]=(select top 1 [Employee Machine Code] from [EDUCOLLEGELIVE-R2].dbo. [TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid) and [Punch Date]=Atte_Date) fromTime,(select  convert(nvarchar(5),convert(time,max([Punch Time]))) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Device Punches] where [Employee Machine Code]=(select top 1 [Employee Machine Code] from [EDUCOLLEGELIVE-R2].dbo. [TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid) and [Punch Date]=Atte_Date) ToTime,CFromTime,CToTime,Remarks,HODRemark,RejectedByHODRemarks,HODName,CreatedDate,[Approved by],ID,(select convert(char(5), [Shift Time In], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=RR.Userid and [Attendance Date]=RR.Atte_Date) as 'Shift IN',(select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo. [TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid and [Attendance Date]=RR.Atte_Date) as 'Shift Out',   CONCAT( case when Correction_Type=1 then 0   else DATEDIFF(MINUTE, (select convert(char(5), [Shift Time In], 108) from    [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate    Latin1_General_CI_AI=RR.Userid and [Attendance Date]=   RR.Atte_Date) , fromTime) + case when DATEDIFF(MINUTE,ToTime,    (select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid and [Attendance Date]=RR.Atte_Date) )<0 then 0 else DATEDIFF(MINUTE,ToTime, (select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=RR.Userid and [Attendance Date]=RR.Atte_Date)) end end,'  ','(',(select COUNT(Userid) from tbl_PunchCorrect_ApplicationS where MONTH(Atte_Date)=Month(RR.Atte_Date) and YEAR(Atte_Date)=YEAR(RR.Atte_Date) and Userid=RR.Userid and (ApprovalStatus='Approved') ),')') AS MinuteDiff ,HODName from [tbl_PunchCorrect_ApplicationS] RR   where CompanyName='TMU' and Co_Leave='1'  and (ApprovalStatus='Approved' or (ApprovalStatus='Pending' And HODUserID='TMU06850')) and (HRStatus='Pending' or (ApprovalStatus='Pending' And HODUserID='TMU06850')) and CompanyName='TMU'	 and Co_Leave='1' and month(RR.Atte_Date)= " + ddlMonth.SelectedValue + " and year(RR.Atte_Date)= " + ddlYear1.SelectedValue + " order by Atte_Date desc ", con2);

            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                btnApprove.Visible = true;
                btnReject.Visible = true;
            }
            else
            {
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
        }
        else if (Session["uid"].ToString() == "TMU01023")
        {
            try
            {
                SqlDataAdapter da1 = new SqlDataAdapter("select ApprovalStatus,Userid,Uname,Atte_Date,(select  convert(nvarchar(5),convert(time,min([Punch Time]))) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Device Punches] where [Employee Machine Code]=(select top 1 [Employee Machine Code] from [EDUCOLLEGELIVE-R2].dbo. [TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid) and [Punch Date]=Atte_Date) fromTime,(select  convert(nvarchar(5),convert(time,max([Punch Time]))) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Device Punches] where [Employee Machine Code]=(select top 1 [Employee Machine Code] from [EDUCOLLEGELIVE-R2].dbo. [TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid) and [Punch Date]=Atte_Date) ToTime,CFromTime,CToTime,Remarks,HODRemark,RejectedByHODRemarks,HODName,CreatedDate,[Approved by],ID,(select convert(char(5), [Shift Time In], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=RR.Userid and [Attendance Date]=RR.Atte_Date) as 'Shift IN',(select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo. [TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid and [Attendance Date]=RR.Atte_Date) as 'Shift Out',   CONCAT( case when Correction_Type=1 then 0   else DATEDIFF(MINUTE, (select convert(char(5), [Shift Time In], 108) from    [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate    Latin1_General_CI_AI=RR.Userid and [Attendance Date]=   RR.Atte_Date) , fromTime) + case when DATEDIFF(MINUTE,ToTime,    (select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid and [Attendance Date]=RR.Atte_Date) )<0 then 0 else DATEDIFF(MINUTE,ToTime, (select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=RR.Userid and [Attendance Date]=RR.Atte_Date)) end end,'  ','(',(select COUNT(Userid) from tbl_PunchCorrect_ApplicationS where MONTH(Atte_Date)=Month(RR.Atte_Date) and YEAR(Atte_Date)=YEAR(RR.Atte_Date) and Userid=RR.Userid and (ApprovalStatus='Approved') ),')') AS MinuteDiff ,HODName from [tbl_PunchCorrect_ApplicationS] RR   	where  CompanyName='TMU' and Co_Leave='1' and ((FinalStatus='Pending' and FinalApprovalID='TMU01023') or (ApprovalStatus='Pending' and HODUserID='TMU01023')) and HRStatus!='Reject'	 and Co_Leave='1'  and month(RR.Atte_Date)= " + ddlMonth.SelectedValue + " and year(RR.Atte_Date)= " + ddlYear1.SelectedValue + " order by Atte_Date desc  ", con2);

                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        else
        {
            // SqlDataAdapter da1 = new SqlDataAdapter("select *,(select convert(char(5), [Shift Time In], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=[tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]=[tbl_PunchCorrect_ApplicationS].Atte_Date) as 'Shift IN',(select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=[tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]=[tbl_PunchCorrect_ApplicationS].Atte_Date) as 'Shift Out', case when Correction_Type=1 then 0 else DATEDIFF(MINUTE, (select convert(char(5), [Shift Time In], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=[tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]=[tbl_PunchCorrect_ApplicationS].Atte_Date) , fromTime) + case when DATEDIFF(MINUTE,ToTime, (select convert(char(5), [Shift Tim//e Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=[tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]=[tbl_PunchCorrect_ApplicationS].Atte_Date) )<0 then 0 else DATEDIFF(MINUTE,ToTime, (select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=[tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]=[tbl_PunchCorrect_ApplicationS].Atte_Date) ) end end AS MinuteDiff,HODName from [tbl_PunchCorrect_ApplicationS] where ([HODUserID]='" + Session["uid"].ToString() + "' or [HOD2]='" + Session["uid"].ToString() + "' or FinalApprovalID1='" + Session["uid"].ToString() + "')  and  CompanyName='TMU' and Co_Leave='1'   and (ApprovalStatus='" + ddStatus_Approval.SelectedValue + "'  or FinalStatus1='0') and CompanyName='TMU' and Co_Leave='1'  ", con2);
            SqlDataAdapter da1 = new SqlDataAdapter("select ApprovalStatus,Userid,Uname,Atte_Date,(select  convert(nvarchar(5),convert(time,min([Punch Time]))) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Device Punches] where [Employee Machine Code]=(select top 1 [Employee Machine Code] from [EDUCOLLEGELIVE-R2].dbo. [TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid) and [Punch Date]=Atte_Date) fromTime,(select  convert(nvarchar(5),convert(time,max([Punch Time]))) from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Device Punches] where [Employee Machine Code]=(select top 1 [Employee Machine Code] from [EDUCOLLEGELIVE-R2].dbo. [TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI= RR.Userid) and [Punch Date]=Atte_Date) ToTime,CFromTime,CToTime,Remarks,HODRemark,RejectedByHODRemarks,HODName,CreatedDate,[Approved by],ID,(select convert(char(5), [Shift Time In], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=RR.Userid and [Attendance Date]=RR.Atte_Date) as 'Shift IN',(select convert(char(5), [Shift Time Out], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_CI_AI=RR.Userid and [Attendance Date]=RR.Atte_Date) as 'Shift Out',  CONCAT( case when Correction_Type=1 then 0  else DATEDIFF(MINUTE, (select convert(char(5),   [Shift Time In], 108) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data]   where [Employee No] collate Latin1_General_CI_AI=Userid and [Attendance Date]=   Atte_Date) ,   fromTime) +  case when DATEDIFF(MINUTE,ToTime,    (select convert(char(5), [Shift Time Out], 108)   from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No]    collate Latin1_General_CI_AI= Userid and [Attendance Date]=Atte_Date) )<0 then 0	 else DATEDIFF(MINUTE,ToTime, (select convert(char(5), [Shift Time Out], 108) from 	  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] 	  collate Latin1_General_CI_AI=Userid and [Attendance Date]=Atte_Date)) end end,'  ','(',(select COUNT(Userid) from tbl_PunchCorrect_ApplicationS where MONTH(Atte_Date)=Month(RR.Atte_Date) and YEAR(Atte_Date)=YEAR(RR.Atte_Date) and Userid=RR.Userid and (ApprovalStatus='Approved') ),') ' ,' Buffer(left):'+convert(varchar(50),(select (select [Allowed Month Buffer(in Min_)] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=RR.[Userid])-  isnull(sum([Total Buffer Utilize]),0)  from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] t where [Employee No] collate Latin1_General_100_CS_AS=RR.Userid and month([Attendance Date])=month(RR.Atte_Date) and YEAR([Attendance Date])=year(RR.Atte_Date)  group by [Employee No] )+''))  AS  MinuteDiff,HODName from [tbl_PunchCorrect_ApplicationS] RR where ([HODUserID]='" + Session["uid"].ToString() + "' or [HOD2]='" + Session["uid"].ToString() + "' or FinalApprovalID1='" + Session["uid"].ToString() + "')  and  CompanyName='TMU' and Co_Leave='1'   and (ApprovalStatus='" + ddStatus_Approval.SelectedValue + "'  or FinalStatus1='0') and CompanyName='TMU' and Co_Leave='1'  and month(RR.Atte_Date)= " + ddlMonth.SelectedValue + " and year(RR.Atte_Date)= " + ddlYear1.SelectedValue + " order by Atte_Date desc ", con2);

            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                btnApprove.Visible = true;
                btnReject.Visible = true;
            }
            else
            {
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
        }
        con2.Close();
        if (dt1.Rows.Count > 0)
        {
            grdApproval.DataSource = dt1;
            grdApproval.DataBind();
        }
        else
        {
            grdApproval.DataSource = "";
            grdApproval.DataBind();
        }
    }
    public void Show_HODData()
    {

        SqlDataAdapter daD = new SqlDataAdapter("select [Multi Approval Punch] from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Dimension Value] where [Active College]=1 and Code='" + Session["GlobalDimension1Coded"].ToString() + "'", con.Con);
        DataTable dtD = new DataTable();
        daD.Fill(dtD);
        if (dtD.Rows.Count > 0)
        {
            if (dtD.Rows[0]["Multi Approval Punch"].ToString() == "1")
            {
                Show_PunchApprovalPendingMedical(Session["uid"].ToString(), "Pending", Session["uid"].ToString(), Session["Company"].ToString());
            }
            else
            {
                Show_PunchApprovalPending(Session["uid"].ToString(), "Pending", Session["uid"].ToString(), Session["Company"].ToString());
            }
        }
        else
        {

            Show_PunchApprovalPending(Session["uid"].ToString(), "Pending", Session["uid"].ToString(), Session["Company"].ToString());
            //DataTable dt = new DataTable();
            //dt.Load(dr);
            //grdApproval.DataSource = dt;
            //grdApproval.DataBind();
            //dr.Close();
            //con.DisConnect();
        }



        //if (Session["uid"].ToString() == "TMU05721")
        //{
        //    if (con2.State.ToString() != "Open")
        //    {
        //        con2.Open();
        //    }
        //    SqlDataAdapter da1 = new SqlDataAdapter("select count(ApprovalStatus) as ApprovalStatus from [tbl_PunchCorrect_ApplicationS] where ([HODUserID]='" + Session["uid"].ToString() + "' or  HRID='" + Session["uid"].ToString() + "' )  and (HRStatus='Pending' or ApprovalStatus='Pending') and CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1' or [HOD2]='" + Session["uid"].ToString() + "' or  HRID='" + Session["uid"].ToString() + "'  and (HRStatus='Pending' or ApprovalStatus='Pending') and CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1'", con2);
        //    DataTable dt1 = new DataTable();
        //    da1.Fill(dt1);
        //    if (dt1.Rows.Count > 0)
        //    {
        //        btnApprove.Visible = true;
        //        btnReject.Visible = true;
        //    }
        //    else
        //    {
        //        btnApprove.Visible = false;
        //        btnReject.Visible = false;
        //    }
        //}
        //else
        //{
        //    Show_PunchApprovalPending(Session["uid"].ToString(), "Pending", Session["uid"].ToString(), Session["Company"].ToString());


        //}

        //}
        //if (rdEmployeeID.Checked == true)
        //{



        //    SqlDataReader dr = con.Show_PunchByHODWithEMPID(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    grdApproval.DataSource = dt;
        //    grdApproval.DataBind();
        //    dr.Close();
        //    con.DisConnect();

        //    SqlDataReader dr1 = con.Show_PunchByHODWithEMPID(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
        //    dr1.Read();
        //    if (dr1.HasRows)
        //    {

        //        string drnnn = dr1["ApprovalStatus"].ToString();
        //        if (drnnn == "Pending")
        //        {
        //            btnApprove.Visible = true;
        //            btnReject.Visible = true;
        //        }
        //    }
        //    else
        //    {
        //        btnApprove.Visible = false;
        //        btnReject.Visible = false;
        //    }
        //    dr1.Close();
        //    con.DisConnect();
        //}

        //if (rdEmployeeName.Checked == true)
        //{


        //    SqlDataReader dr = con.Show_PunchByHODWithEMPNAme(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    grdApproval.DataSource = dt;
        //    grdApproval.DataBind();
        //    dr.Close();
        //    con.DisConnect();

        //    SqlDataReader dr1 = con.Show_PunchByHODWithEMPNAme(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
        //    dr1.Read();
        //    if (dr1.HasRows)
        //    {
        //        string appr = dr1["ApprovalStatus"].ToString();
        //        if (appr == "Pending")
        //        {
        //            btnReject.Visible = true;
        //            btnApprove.Visible = true;
        //        }
        //        else
        //        {
        //            btnReject.Visible = false;
        //            btnApprove.Visible = false;
        //        }
        //    }
        //    else
        //    {
        //        btnReject.Visible = false;
        //        btnApprove.Visible = false;
        //    }
        //    dr1.Close();
        //    con.DisConnect();
        //}

        //if (rdDatewise.Checked == true)
        //{

        //    SqlDataReader dr = con.Show_PunchByHODWithDatewise(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), txtFromDate_Approval.Text.Trim(), txtTodate_Approval.Text.Trim(), Session["Company"].ToString());
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    grdApproval.DataSource = dt;
        //    grdApproval.DataBind();
        //    dr.Close();
        //    con.DisConnect();

        //    SqlDataReader dr1 = con.Show_PunchByHODWithDatewise(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), txtFromDate_Approval.Text.Trim(), txtTodate_Approval.Text.Trim(), Session["Company"].ToString());
        //    dr1.Read();
        //    if (dr1.HasRows)
        //    {
        //        string appr = dr1["ApprovalStatus"].ToString();
        //        if (appr == "Pending")
        //        {
        //            btnReject.Visible = true;
        //            btnApprove.Visible = true;
        //        }
        //        else
        //        {
        //            btnReject.Visible = false;
        //            btnApprove.Visible = false;
        //        }
        //    }
        //    else
        //    {
        //        btnReject.Visible = false;
        //        btnApprove.Visible = false;
        //    }
        //    dr1.Close();
        //    con.DisConnect();

        //}
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter da1 = new SqlDataAdapter("select *,(select [Night Exist] from [TMU$Shift Master] where [Shift Code]=[TMU$Employee Actual Punch Data].[Shift Code]) as 'Night' from  [TMU$Employee Actual Punch Data] where [Employee No]='" + Session["uid"].ToString() + "' and [Attendance Date]='" + txtFromDate.Text.Trim() + "' ", navconn.Con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            DateTime dt = DateTime.Parse(dt1.Rows[0]["Time From"].ToString());
            DateTime dt2 = DateTime.Parse(dt1.Rows[0]["Time To"].ToString());
            DateTime ShiftFrom = DateTime.Parse(dt1.Rows[0]["Shift Time In"].ToString());
            DateTime ShiftTo = DateTime.Parse(dt1.Rows[0]["Shift Time Out"].ToString());
            DateTime ShiftTo1 = DateTime.Parse(dt1.Rows[0]["Shift Time Out"].ToString());
            ShiftTo1 = ShiftTo1.AddHours(1);
            txtFromTime.Text = dt.ToString("HH:mm");
            txtToTime.Text = dt2.ToString("HH:mm");
            //txtFromTime1.Text = dt.ToString("HH:mm");
            //txtToTime1.Text = dt2.ToString("HH:mm");
            hfShiftFrom.Value = ShiftFrom.ToString("HH:mm");
            hfShiftTo.Value = ShiftTo.ToString("HH:mm");
            hfShiftTo1.Value = ShiftTo1.ToString("HH:mm");
            hfMachineID.Value = dt1.Rows[0]["Employee Machine Code"].ToString();
            hfTotBUtilize.Value = dt1.Rows[0]["Total Buffer Utilize"].ToString();
            hfnight.Value = dt1.Rows[0]["Night"].ToString();


            txtFromTime1.Text = ShiftFrom.ToString("HH:mm");
            txtToTime1.Text = ShiftTo.ToString("HH:mm");
        }


    }
    protected void txtFromTime_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtToTime_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        DateTime dt1 = DateTime.Parse(txtFromTime.Text.ToString());
        DateTime dt3 = DateTime.Parse(txtFromTime1.Text.ToString());
        DateTime dt4 = DateTime.Parse(txtToTime.Text.ToString());
        DateTime dt5 = DateTime.Parse(txtToTime1.Text.ToString());
        DateTime dt6 = DateTime.Parse(hfShiftTo1.Value.ToString());
        string night = hfnight.Value;
        //TextBox txtfrom1 = new TextBox();
        //if (night == "1" && Convert.ToDateTime(dt3.ToString("HH:mm")) > Convert.ToDateTime(dt5.ToString("HH:mm")))
        //{
        //    string input = txtFromDate.Text;
        //    DateTime dateTime = Convert.ToDateTime(input);
        //    DateTime endDate = dateTime.AddDays(1);


        //    txtfrom1.Text = endDate.ToString("dd MMM yyyy");
        //}
        //else
        //{
        //    txtfrom1=txtFromDate;
        //}
        if (night != "1")
        {
            if (Convert.ToDateTime(dt3.ToString("HH:mm")) > Convert.ToDateTime(dt5.ToString("HH:mm")))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Out-Time is not less then from In-Time,Please Insert Out-Time in 24 Hours Format.');", true);
                return;
            }
        }

        if (drpPunchType.SelectedValue == "0")
        {
            //if (hfTotBUtilize.Value != "0")
            // {
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Punch correction is not allow for this date.');", true);
            //    return;
            // }
            DateTime startTime = Convert.ToDateTime(txtFromTime1.Text);
            DateTime startTime1 = Convert.ToDateTime(txtFromTime.Text);

            TimeSpan duration = startTime - startTime1;

            DateTime EndTime = Convert.ToDateTime(txtToTime1.Text);
            DateTime EndTime1 = Convert.ToDateTime(txtToTime.Text);

            TimeSpan durationend = EndTime - EndTime1;
            Decimal minutS = 0;
            Decimal minutL = 0;
            if (duration.TotalMinutes < 00.0)
            {
                minutS = Convert.ToDecimal(duration.TotalMinutes) - Convert.ToDecimal(duration.TotalMinutes) * 2;
            }
            if (durationend.TotalMinutes < 00.0)
            {
                minutL = Convert.ToDecimal(durationend.TotalMinutes) - Convert.ToDecimal(durationend.TotalMinutes) * 2;
            }

            if (Convert.ToInt32(minutS) < 30 && Convert.ToInt32(minutS) != 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Special Punch correction is not allow for less than 30 minutes.');", true);
                return;
            }
            if (Convert.ToInt32(minutL) < 30 && Convert.ToInt32(minutL) != 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Special Punch correction is not allow for less than 30 minutes.');", true);
                return;
            }

            if (dt1.ToString("HH:mm") == "00:00" || dt4.ToString("HH:mm") == "00:00")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Punch correction is not allow for this date.');", true);
                return;
            }


            if (Convert.ToDateTime(dt3.ToString("HH:mm")) < Convert.ToDateTime(hfShiftFrom.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your shift time is " + hfShiftFrom.Value + " To " + hfShiftTo.Value + ",Please check modify time.');", true);
                return;
            }
            if (Convert.ToDateTime(dt4.ToString("HH:mm")) != Convert.ToDateTime(dt5.ToString("HH:mm")))
            {
                if (Convert.ToDateTime(dt5.ToString("HH:mm")) > Convert.ToDateTime(hfShiftTo1.Value))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your shift time is " + hfShiftFrom.Value + " To " + hfShiftTo.Value + ",Please check modify time.');", true);
                    return;


                }


            }
        }


        if (drpPunchType.SelectedValue == "1")
        {


            SqlDataAdapter daE = new SqlDataAdapter(" select [Required Punch] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [No_]='" + Session["uid"].ToString() + "' ", con.Con);
            DataTable dtE = new DataTable();
            daE.Fill(dtE);
            if (dtE.Rows[0]["Required Punch"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Missed Punch is not allowed .');", true);
                return;
            }
            if (dt1.ToString("HH:mm") == "00:00" && dt4.ToString("HH:mm") == "00:00")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Missed Punch is not allowed for this date.');", true);
                return;
            }
            if (dt1.ToString("HH:mm") != "00:00" && dt4.ToString("HH:mm") != "00:00")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Missed Punch is not allowed for this date.');", true);
                return;
            }


            if (Convert.ToDateTime(dt3.ToString("HH:mm")) < Convert.ToDateTime(hfShiftFrom.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your shift time is " + hfShiftFrom.Value + " To " + hfShiftTo.Value + ",Please check modify time.');", true);
                return;
            }
            if (Convert.ToDateTime(dt4.ToString("HH:mm")) != Convert.ToDateTime(dt5.ToString("HH:mm")))
            {
                if (Convert.ToDateTime(dt5.ToString("HH:mm")) > Convert.ToDateTime(hfShiftTo1.Value))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your shift time is " + hfShiftFrom.Value + " To " + hfShiftTo.Value + ",Please check modify time.');", true);
                    return;
                }


            }
        }


        SqlDataReader dr = con.Duplicate_tbl_attendenceforPunch(txtFromDate.Text.Trim(), Session["uid"].ToString(), Session["Company"].ToString());
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

            SqlDataAdapter daD = new SqlDataAdapter("select [Multi Approval Punch] from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Dimension Value] where [Active College]=1 and Code='" + Session["GlobalDimension1Coded"].ToString() + "'", con.Con);
            DataTable dtD = new DataTable();
            daD.Fill(dtD);



            if (dtD.Rows.Count > 0)
            {
                if (dtD.Rows[0]["Multi Approval Punch"].ToString() == "1")
                {
                    con2.Open();
                    SqlCommand cmdT = new SqlCommand("insert into tbl_PunchCorrect_ApplicationS (Userid,Uname,CompanyName,Atte_Date,fromTime,ToTime,Job_location,Remarks,Status,Working_Duration,CreatedDate,ApprovalStatus,HODName,Co_Leave,[Attendance Type],[HOD2 Name],HOD2,Purpose,HODUserID,ApplyStaff,CfromTime,CToTime,FinalApprovalID1,FinalStatus1,HRID,HRStatus,FinalApprovalID,FinalStatus) values('" + Session["uid"].ToString() + "', '" + Session["uname"].ToString() + "', '" + Session["Company"].ToString() + "', '" + txtFromDate.Text.Trim() + "','" + txtFromTime.Text.Trim() + "', '" + txtToTime.Text.Trim() + "', '','" + txtRemarks.Text.Trim() + "', 'Present', '','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "', 'Pending','" + Session["hod_Name_Leave1"].ToString() + "', '1', 'Manual', '" + Session["hod_Name_Leave2"].ToString() + "', '" + Session["hod_ID_Leave2"].ToString() + "', '" + txtRemarks.Text.Trim() + "',(select [Sanctioning Incharge] from [EDUCOLLEGELIVE-R2].dbo.TMU$Employee where No_='" + Session["uid"].ToString() + "'),'" + Session["uid"].ToString() + "','" + txtFromTime1.Text + "', '" + txtToTime1.Text + "',(select [HOD] from [EDUCOLLEGELIVE-R2].dbo.TMU$Employee where No_='" + Session["uid"].ToString() + "'),0,'TMU06850','0','TMU01023','0')", con2);
                    cmdT.ExecuteNonQuery();
                    cmdT.Dispose();
                    con2.Close();
                }
                else
                {
                    con2.Open();
                    SqlCommand cmdT = new SqlCommand("insert into tbl_PunchCorrect_ApplicationS (Userid,Uname,CompanyName,Atte_Date,fromTime,ToTime,Job_location,Remarks,Status,Working_Duration,CreatedDate,ApprovalStatus,HODName,Co_Leave,[Attendance Type],[HOD2 Name],HOD2,Purpose,HODUserID,ApplyStaff,CfromTime,CToTime,HRID,HRStatus,FinalApprovalID,FinalStatus) values('" + Session["uid"].ToString() + "','" + Session["uname"].ToString() + "','" + Session["Company"].ToString() + "','" + txtFromDate.Text.Trim() + "','" + txtFromTime.Text.Trim() + "','" + txtToTime.Text.Trim() + "','','" + txtRemarks.Text.Trim() + "','Present','','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','Pending','" + Session["hod_Name_Leave1"].ToString() + "','1','Manual','" + Session["hod_Name_Leave2"].ToString() + "','" + Session["hod_ID_Leave2"].ToString() + "','" + txtRemarks.Text.Trim() + "','" + Session["hod_ID_Leave1"].ToString() + "','" + Session["uid"].ToString() + "','" + txtFromTime1.Text + "','" + txtToTime1.Text + "','TMU06850','0','TMU01023','0')", con2);
                    cmdT.ExecuteNonQuery();
                    cmdT.Dispose();
                    con2.Close();
                }
            }
            else
            {

                //con.insert_tbl_PunchCorrection(Session["uid"].ToString(), Session["uname"].ToString(), Session["Company"].ToString(), txtFromDate.Text.Trim(), txtFromTime.Text.Trim(), txtToTime.Text.Trim(), "", txtRemarks.Text.Trim(), "Present", "", System.DateTime.Now.ToString("yyyy-MM-dd"), "Pending", Session["hod_Name_Leave1"].ToString(), "1", "Manual", Session["hod_Name_Leave2"].ToString(), Session["hod_ID_Leave2"].ToString(), txtRemarks.Text.Trim(), Session["hod_ID_Leave1"].ToString(), Session["uid"].ToString(), txtFromTime1.Text, txtToTime1.Text);
                con2.Open();
                SqlCommand cmdT = new SqlCommand("insert into tbl_PunchCorrect_ApplicationS (Userid,Uname,CompanyName,Atte_Date,fromTime,ToTime,Job_location,Remarks,Status,Working_Duration,CreatedDate,ApprovalStatus,HODName,Co_Leave,[Attendance Type],[HOD2 Name],HOD2,Purpose,HODUserID,ApplyStaff,CfromTime,CToTime,HRID,HRStatus,FinalApprovalID,FinalStatus) values('" + Session["uid"].ToString() + "','" + Session["uname"].ToString() + "','" + Session["Company"].ToString() + "','" + txtFromDate.Text.Trim() + "','" + txtFromTime.Text.Trim() + "','" + txtToTime.Text.Trim() + "','','" + txtRemarks.Text.Trim() + "','Present','','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','Pending','" + Session["hod_Name_Leave1"].ToString() + "','1','Manual','" + Session["hod_Name_Leave2"].ToString() + "','" + Session["hod_ID_Leave2"].ToString() + "','" + txtRemarks.Text.Trim() + "','" + Session["hod_ID_Leave1"].ToString() + "','" + Session["uid"].ToString() + "','" + txtFromTime1.Text + "','" + txtToTime1.Text + "','TMU06850','0','TMU01023','0')", con2);
                cmdT.ExecuteNonQuery();
                cmdT.Dispose();
                con2.Close();
            }
            con2.Open();
            if (drpPunchType.SelectedValue == "0")
            {
                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set Correction_Type=0 where Userid='" + Session["uid"].ToString() + "' and Atte_Date=convert(varchar,convert(date, '" + txtFromDate.Text.Trim() + "'), 23)  ", con2);
                cmdT.ExecuteNonQuery();
                cmdT.Dispose();
            }
            else
            {

                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set Correction_Type=1 where Userid='" + Session["uid"].ToString() + "' and Atte_Date=convert(varchar,convert(date, '" + txtFromDate.Text.Trim() + "'), 23)  ", con2);
                cmdT.ExecuteNonQuery();
                cmdT.Dispose();
            }
            con2.Close();
            lblCOSuccess.Visible = true;



            SqlDataAdapter daH = new SqlDataAdapter("select COUNT(*) as TotalPunch from  [tbl_PunchCorrect_ApplicationS] where Userid='" + Session["uid"].ToString() + "' and month('" + txtFromDate.Text.Trim() + "')=MONTH(Atte_Date) and YEAR('" + txtFromDate.Text.Trim() + "')=YEAR(Atte_Date)", con.Con);
            DataTable dtH = new DataTable();
            daH.Fill(dtH);

            if (Convert.ToInt32(dtH.Rows[0]["TotalPunch"]) > 3)
            {
                con2.Open();
                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set HRStatus=0,HRID='TMU06850' where Userid='" + Session["uid"].ToString() + "' and Atte_Date=convert(varchar,convert(date, '" + txtFromDate.Text.Trim() + "'), 23)  ", con2);
                cmdT.ExecuteNonQuery();
                cmdT.Dispose();
                con2.Close();
            }

            clear();












        }



    }
    public void clear()
    {
        txtFromDate.Text = "";
        txtToTime.Text = "";
        txtFromTime.Text = "";
        txtRemarks.Text = "";
        txtToTime1.Text = "";
        txtFromTime1.Text = "";
    }
    protected void btnGet_ViewStatus_Click(object sender, EventArgs e)
    {

        Show_Report_ofuser();

    }
    protected void btnExporttoexcel_viewStatus_Click(object sender, EventArgs e)
    {

    }
    protected void grdView_Status_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Show_Report_ofEmployee();

    }
    protected void rdEmployeeID_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void rdEmployeeName_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void rdDatewise_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnFIlterGet_Approval_Click(object sender, EventArgs e)
    {

    }
    protected void btnApproveExport_Click(object sender, EventArgs e)
    {
        grdView_Status.AllowPaging = false;

        Show_HODData();


        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grdApproval.RenderControl(htmlWrite);

        Response.Clear();
        string str = "Approvaldata" + Session["uid"].ToString();
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {


        if (Session["uid"].ToString() == "TMU06850" || Session["uid"].ToString() == "TMU01023")
        {

            foreach (GridViewRow row in grdApproval.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);

                    TextBox txtRemark = (row.Cells[0].FindControl("txtHRRemark") as TextBox);

                    if (chkRow.Checked == true && txtRemark.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Remark is Mandatory for Approval.');", true);
                        return;
                    }


                }
            }
        }
        else
        {


            foreach (GridViewRow row in grdApproval.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);

                    TextBox txtRemark = (row.Cells[0].FindControl("txtHODRemark") as TextBox);

                    if (chkRow.Checked == true && txtRemark.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Remark is Mandatory for Approval.');", true);
                        return;
                    }


                }
            }
        }






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
                    TextBox txtHODRemark = (row.Cells[0].FindControl("txtHODRemark") as TextBox);
                    TextBox txtHRRemark = (row.Cells[0].FindControl("txtHRRemark") as TextBox);
                    SqlDataAdapter daD = new SqlDataAdapter("select  [Multi Approval Punch] from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Dimension Value] where [Active College]=1 and Code=(select [Global Dimension 1 Code] from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS= (select Userid from [tbl_PunchCorrect_ApplicationS] where [ID]='" + lblid.Text + "')) select * from [tbl_PunchCorrect_ApplicationS] where [ID]='" + lblid.Text + "'", con.Con);
                    DataSet dtD = new DataSet();
                    daD.Fill(dtD);
                    if (dtD.Tables[0].Rows.Count > 0)
                    {
                        if (dtD.Tables[0].Rows[0]["Multi Approval Punch"].ToString() == "1")
                        {

                            if (dtD.Tables[1].Rows[0]["HRID"].ToString() == Session["uid"].ToString() && dtD.Tables[1].Rows[0]["HRStatus"].ToString() == "Pending")
                            {
                                //ShowApprovalData(lblid.Text.Trim());
                                //sendAproved(lblFDate.Text.Trim(), EmpId.Text, "Approved");
                                if (con2.State.ToString() != "Open")
                                {
                                    con2.Open();
                                }
                                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set HRRemark='" + txtHRRemark.Text + "', ApprovalStatus='Approved',HRStatus='Approved',HRAppDate=getdate(),FinalStatus='Pending' where [ID]='" + lblid.Text + "'  ", con2);
                                cmdT.ExecuteNonQuery();
                                cmdT.Dispose();
                            }
                            else if (dtD.Tables[1].Rows[0]["FinalApprovalID"].ToString() == Session["uid"].ToString() && dtD.Tables[1].Rows[0]["FinalStatus"].ToString() == "Pending")
                            {
                                ShowApprovalData(lblid.Text.Trim());
                                sendAproved(lblFDate.Text.Trim(), EmpId.Text, "Approved");
                                if (con2.State.ToString() != "Open")
                                {
                                    con2.Open();
                                }
                                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set ApprovalStatus='Approved',HRStatus='Approved',HRAppDate=getdate(),FinalStatus='Approved',FinalRemark='" + txtHRRemark.Text + "' where [ID]='" + lblid.Text + "'  ", con2);
                                cmdT.ExecuteNonQuery();
                                cmdT.Dispose();
                            }

                            else if (dtD.Tables[1].Rows[0]["FinalApprovalID1"].ToString() != "" && dtD.Tables[1].Rows[0]["HODUserID"].ToString() == Session["uid"].ToString())
                            {
                                if (con2.State.ToString() != "Open")
                                {
                                    con2.Open();
                                }
                                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set HODRemark='" + txtHODRemark.Text + "', ApprovalStatus='Recommend' where [ID]='" + lblid.Text + "'  ", con2);
                                cmdT.ExecuteNonQuery();
                                cmdT.Dispose();
                            }
                            else if (dtD.Tables[1].Rows[0]["FinalApprovalID"].ToString() == "" && dtD.Tables[1].Rows[0]["HODUserID"].ToString() == Session["uid"].ToString())
                            {
                                if (dtD.Tables[1].Rows[0]["HRID"].ToString() == "")
                                {
                                    //ShowApprovalData(lblid.Text.Trim());
                                    // sendAproved(lblFDate.Text.Trim(), EmpId.Text, "Approved");
                                    if (con2.State.ToString() != "Open")
                                    {
                                        con2.Open();
                                    }
                                    SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set HODRemark='" + txtHODRemark.Text + "', ApprovalStatus='Approved' where [ID]='" + lblid.Text + "'  ", con2);
                                    cmdT.ExecuteNonQuery();
                                    cmdT.Dispose();
                                }
                                else
                                {
                                    if (dtD.Tables[1].Rows[0]["HRID"].ToString() == Session["uid"].ToString() && dtD.Tables[1].Rows[0]["HRStatus"].ToString() == "Pending")
                                    {
                                        // ShowApprovalData(lblid.Text.Trim());
                                        //  sendAproved(lblFDate.Text.Trim(), EmpId.Text, "Approved");// for sms
                                        if (con2.State.ToString() != "Open")
                                        {
                                            con2.Open();
                                        }
                                        SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set HRRemark='" + txtHRRemark.Text + "', ApprovalStatus='Approved',HRStatus='Approved',HRAppDate=getdate() where [ID]='" + lblid.Text + "'  ", con2);
                                        cmdT.ExecuteNonQuery();
                                        cmdT.Dispose();
                                    }
                                    else
                                    {
                                        if (con2.State.ToString() != "Open")
                                        {
                                            con2.Open();
                                        }
                                        SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set HODRemark='" + txtHODRemark.Text + "',  ApprovalStatus='Approved',HRStatus='Pending' where [ID]='" + lblid.Text + "'  ", con2);
                                        cmdT.ExecuteNonQuery();
                                        cmdT.Dispose();
                                    }

                                }
                            }
                            else if (dtD.Tables[1].Rows[0]["FinalApprovalID1"].ToString() == Session["uid"].ToString())
                            {
                                if (dtD.Tables[1].Rows[0]["HRID"].ToString() == "")
                                {
                                    //ShowApprovalData(lblid.Text.Trim());
                                    // sendAproved(lblFDate.Text.Trim(), EmpId.Text, "Approved");
                                    if (con2.State.ToString() != "Open")
                                    {
                                        con2.Open();
                                    }
                                    SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS  set HODRemark='" + txtHODRemark.Text + "', FinalStatus=1  where [ID]='" + lblid.Text + "'  ", con2);
                                    cmdT.ExecuteNonQuery();
                                    cmdT.Dispose();
                                }
                                else
                                {
                                    if (dtD.Tables[1].Rows[0]["HRID"].ToString() == Session["uid"].ToString() && dtD.Tables[1].Rows[0]["HRStatus"].ToString() == "Pending")
                                    {
                                        // ShowApprovalData(lblid.Text.Trim());
                                        // sendAproved(lblFDate.Text.Trim(), EmpId.Text, "Approved");
                                        if (con2.State.ToString() != "Open")
                                        {
                                            con2.Open();
                                        }
                                        SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set HRRemark='" + txtHRRemark.Text + "', ApprovalStatus='Approved',HRStatus='Approved',HRAppDate=getdate() where [ID]='" + lblid.Text + "'  ", con2);
                                        cmdT.ExecuteNonQuery();
                                        cmdT.Dispose();
                                    }
                                    else
                                    {
                                        if (con2.State.ToString() != "Open")
                                        {
                                            con2.Open();
                                        }
                                        SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set  ApprovalStatus='Approved',HRStatus='Pending',FinalStatus1='Approved' ,FinalApprovalID1Remark='" + txtHODRemark.Text + "' where [ID]='" + lblid.Text + "'  ", con2);
                                        cmdT.ExecuteNonQuery();
                                        cmdT.Dispose();
                                    }
                                }

                            }
                            con2.Close();



                        }



                        else
                        {


                            if (dtD.Tables[1].Rows[0]["HRID"].ToString() == Session["uid"].ToString() && dtD.Tables[1].Rows[0]["HRStatus"].ToString() == "Pending")
                            {

                                if (con2.State.ToString() != "Open")
                                {
                                    con2.Open();
                                }
                                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set HRRemark='" + txtHRRemark.Text + "', ApprovalStatus='Approved',HRStatus='Approved',HRAppDate=getdate(),FinalStatus='Pending' where [ID]='" + lblid.Text + "'  ", con2);
                                cmdT.ExecuteNonQuery();
                                cmdT.Dispose();

                            }
                            else if (dtD.Tables[1].Rows[0]["FinalApprovalID"].ToString() == Session["uid"].ToString() && dtD.Tables[1].Rows[0]["FinalStatus"].ToString() == "Pending")
                            {
                                ShowApprovalData(lblid.Text.Trim());
                                sendAproved(lblFDate.Text.Trim(), EmpId.Text, "Approved");
                                if (con2.State.ToString() != "Open")
                                {
                                    con2.Open();
                                }
                                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set  ApprovalStatus='Approved',HRStatus='Approved',HRAppDate=getdate(),FinalStatus='Approved',FinalRemark='" + txtHRRemark.Text + "' where [ID]='" + lblid.Text + "'  ", con2);
                                cmdT.ExecuteNonQuery();
                                cmdT.Dispose();
                            }
                            else
                            {

                                if (con2.State.ToString() != "Open")
                                {
                                    con2.Open();
                                }
                                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set HODRemark='" + txtHODRemark.Text + "', ApprovalStatus='Approved',HRStatus='Pending' where [ID]='" + lblid.Text + "'  ", con2);
                                cmdT.ExecuteNonQuery();
                                cmdT.Dispose();
                                con2.Close();
                            }
                            //}

                        }
                    }
                }

            }
        }
        Show_HODData();
        ShowPendingApprovalCount();
    }


    string hourworking = ""; string CoRemarks = ""; string useridapr = ""; string Emp_No_OD = ""; string frmmdate_OD = ""; string Todate_OD = ""; string Purpose_OD = ""; string fromtime_od = ""; string Totime_OD = ""; string Approval_Status_OD = ""; string Cfromtime_od = ""; string CTotime_OD = "";

    string Night = "";

    public void ShowApprovalData(string id)
    {


        if (con2.State.ToString() != "Open")
        {
            con2.Open();
        }
        string s = "select *,(select [Night Exist] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Shift Master] where [Shift Code]=(select [Shift Code] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No] collate Latin1_General_100_CS_AS=[tbl_PunchCorrect_ApplicationS].Userid and [Attendance Date]=[tbl_PunchCorrect_ApplicationS].Atte_Date)) as 'Night' from [tbl_PunchCorrect_ApplicationS] where ID='" + id + "'";
        SqlCommand cmd1 = new SqlCommand(s, con2);
        SqlDataReader dr = cmd1.ExecuteReader();
        dr.Read();
        string HRID = "", HRStatus = "";
        if (dr.HasRows)
        {
            Night = dr["Night"].ToString();
            frmmdate_OD = Convert.ToDateTime(dr["Atte_Date"].ToString()).ToString("yyyy-MM-dd");

            DateTime frmmdate_ODa = DateTime.ParseExact(frmmdate_OD, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime frmmdate_ODa = Convert.ToDateTime(frmmdate_OD);
            frmmdate_OD = frmmdate_ODa.ToString("yyyy-MM-dd");
            useridapr = dr["Userid"].ToString();
            fromtime_od = dr["fromTime"].ToString();
            Cfromtime_od = dr["CfromTime"].ToString();

            Totime_OD = dr["ToTime"].ToString();
            CTotime_OD = dr["CToTime"].ToString();


            Approval_Status_OD = dr["ApprovalStatus"].ToString();
            HRID = dr["FinalApprovalID"].ToString();
            HRStatus = dr["FinalStatus"].ToString();
            dr.Close();
            con.DisConnect();

            if (HRID == "TMU01023" && HRStatus == "Pending")
            {



                if (Cfromtime_od != fromtime_od)
                {



                    SqlCommand cmd = new SqlCommand("Sp_InserPunchData", con1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con1.State == ConnectionState.Closed)
                    {
                        con1.Open();
                    }
                    cmd.Parameters.AddWithValue("@UserId", useridapr);
                    cmd.Parameters.AddWithValue("@PunchDate", frmmdate_OD);
                    cmd.Parameters.AddWithValue("@PunchTime", Cfromtime_od);
                    cmd.Parameters.AddWithValue("@NodeNumber", "101");
                    int T = cmd.ExecuteNonQuery();

                    con1.Close();
                    if (T > 0)
                    {

                    }

                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Not Approve Try Again After Some Time')", true);
                        return;
                    }
                }
                if (Totime_OD != CTotime_OD)
                {
                    if (Night == "1")
                    {
                        if (Convert.ToDateTime(Cfromtime_od) > Convert.ToDateTime(CTotime_OD))
                        {
                            DateTime dateTime = Convert.ToDateTime(frmmdate_OD);
                            DateTime endDate = dateTime.AddDays(1);
                            frmmdate_OD = endDate.ToString("yyyy-MM-dd");
                        }
                    }
                    SqlCommand cmd = new SqlCommand("Sp_InserPunchData", con1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con1.State == ConnectionState.Closed)
                    {
                        con1.Open();
                    }
                    cmd.Parameters.AddWithValue("@UserId", useridapr);
                    cmd.Parameters.AddWithValue("@PunchDate", frmmdate_OD);
                    cmd.Parameters.AddWithValue("@PunchTime", CTotime_OD);
                    cmd.Parameters.AddWithValue("@NodeNumber", "101");
                    int T = cmd.ExecuteNonQuery();

                    con1.Close();
                    if (T > 0)
                    {

                    }

                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Not Approve Try Again After Some Time')", true);
                        return;
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
    public SqlDataReader Show_ApprovalPunch(string id)
    {
        if (con1.State == ConnectionState.Closed)
        {
            con1.Open();
        }
        string s = "select * from HRMSPortal.dbo.[tbl_PunchCorrect_ApplicationS] where ID='" + id + "'";
        SqlCommand cmd = new SqlCommand(s, con1);
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    public void ShowApprovalDataReject(string id)
    {
        string HRID = "", HRStatus = "";
        SqlDataReader dr = Show_ApprovalPunch(id);
        dr.Read();
        if (dr.HasRows)
        {
            frmmdate_OD = Convert.ToDateTime(dr["Atte_Date"].ToString()).ToString("yyyy-MM-dd");

            DateTime frmmdate_ODa = DateTime.ParseExact(frmmdate_OD, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime frmmdate_ODa = Convert.ToDateTime(frmmdate_OD);
            frmmdate_OD = frmmdate_ODa.ToString("yyyy-MM-dd");
            useridapr = dr["Userid"].ToString();
            fromtime_od = dr["fromTime"].ToString();

            Totime_OD = dr["ToTime"].ToString();
            Approval_Status_OD = dr["ApprovalStatus"].ToString();
            HRID = dr["HRID"].ToString();
            HRStatus = dr["HRStatus"].ToString();
            dr.Close();
            con.DisConnect();

            if ((Approval_Status_OD == "Pending" || Approval_Status_OD == "Recommend") || (HRID == Session["uid"].ToString() && HRStatus == "Pending"))
            {





                con.DisConnect();
                con2.Open();
                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_ApplicationS set ApprovalStatus='Rejected',[Approval Date]='" + System.DateTime.Now.ToString("yyyy-MM-dd") + "',[Approved by]='" + Session["uname"].ToString() + "' ,HRStatus='Reject',HRAppDate=Getdate() where id='" + id + "'  ", con2);




                cmdT.ExecuteNonQuery();
                cmdT.Dispose();
                con2.Close();



            }
            else
            {
                dr.Close();
                con.DisConnect();

            }
        }
    }
    public void sendAproved(string FromDate, string Eid, string Status)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";

        string smsdata = "Dear Applicant your punch correction from " + FromDate + " to " + FromDate + " has been " + Status + ".";

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
    protected void btnReject_Click(object sender, EventArgs e)
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

                    ShowApprovalDataReject(lblid.Text.Trim());
                    sendAproved(lblFDate.Text.Trim(), EmpId.Text, "Rejected");// for sms
                }
            }
        }
        Show_HODData();
        ShowPendingApprovalCount();
    }
    protected void btncancel_Approvedleave_Command(object sender, CommandEventArgs e)
    {

        string id = e.CommandArgument.ToString();
        Session["id_Canceled"] = id.ToString();
        lblCancelmessage.Visible = false;
        ModalPopupExtender1.Show();
    }
    protected void grdApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

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

                    ShowApprovalDataReject(lblid.Text.Trim());
                    sendAproved(lblFDate.Text.Trim(), EmpId.Text, "Rejected");// for sms
                }
            }
        }
        Show_HODData();
        ShowPendingApprovalCount();

    }


    protected void drpPunchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpPunchType.SelectedValue == "0")
        {
            lblCount.Text = "0";
        }
        else
        {

            SqlDataAdapter da1 = new SqlDataAdapter("select count(*) as No from tbl_PunchCorrect_ApplicationS where Userid='" + Session["uid"].ToString() + "' and Month(Getdate())=Month(Atte_Date) and YEAR(Getdate())=year(Atte_Date)  and [Correction_Type]=1 ", con.Con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            lblCount.Text = dt1.Rows[0]["No"].ToString();
        }



    }
    protected void lnkEmployeeReport_Click(object sender, EventArgs e)
    {

        pnlEmployeeReport.Visible = true;
        pnlApproval.Visible = false;
        pnlViewStatus.Visible = false;
        pnlCOApplication.Visible = false;
        lblHeader.Text = "Employee Report";


        txtFromDate_ViewStatus.Text = "";
        txtTodate_ViewStatus.Text = "";
        ddStatus_ViewStatus.SelectedValue = "Pending";
        TextBox1.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        TextBox2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
        Show_Report_ofuser();


    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        Show_Report_ofEmployee();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        GridView1.AllowPaging = false;

        Show_Report_ofEmployee();


        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        GridView1.RenderControl(htmlWrite);

        Response.Clear();
        string str = "Punchdata";
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();
    }

    public void Show_Report_ofEmployee()
    {
        if (Session["uid"].ToString() == "TMU06850")
        {
            if (DropDownList1.SelectedValue.Trim() == "All")
            {
                SqlDataAdapter da1 = new SqlDataAdapter("select * from [tbl_PunchCorrect_ApplicationS] where convert(date,Atte_Date,103)>='" + TextBox1.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + TextBox2.Text.Trim() + "' and HRID='" + Session["uid"].ToString() + "' and HRStatus='Approved' and  CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1'  ", con.Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);


                GridView1.DataSource = dt1;
                GridView1.DataBind();

                con.DisConnect();
            }
            else
            {

                SqlDataAdapter da1 = new SqlDataAdapter("select * from [tbl_PunchCorrect_ApplicationS] where convert(date,Atte_Date,103)>='" + TextBox1.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + TextBox2.Text.Trim() + "' and HRID='" + Session["uid"].ToString() + "' and HRStatus='Approved' and CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1' and ApprovalStatus='" + DropDownList1.SelectedValue + "' ", con.Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);


                GridView1.DataSource = dt1;
                GridView1.DataBind();

                con.DisConnect();
            }
        }

        else if (Session["uid"].ToString() == "TMU01023")
        {
            if (DropDownList1.SelectedValue.Trim() == "All")
            {


                SqlDataAdapter da1 = new SqlDataAdapter("select * from [tbl_PunchCorrect_ApplicationS] where convert(date,Atte_Date,103)>='" + TextBox1.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + TextBox2.Text.Trim() + "' and FinalApprovalID='" + Session["uid"].ToString() + "' and FinalStatus='Approved'  and CompanyName='" + Session["Company"].ToString() + "'  and Co_Leave='1'", con.Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);


                GridView1.DataSource = dt1;
                GridView1.DataBind();

                con.DisConnect();
            }
            else
            {

                SqlDataAdapter da1 = new SqlDataAdapter("select * from [tbl_PunchCorrect_ApplicationS] where convert(date,Atte_Date,103)>='" + TextBox1.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + TextBox2.Text.Trim() + "' and HODUserID='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1' and ApprovalStatus='" + DropDownList1.SelectedValue + "'  ", con.Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);


                GridView1.DataSource = dt1;
                GridView1.DataBind();

                con.DisConnect();
            }
        }
        else
        {
            if (DropDownList1.SelectedValue.Trim() == "All")
            {
                SqlDataAdapter da1 = new SqlDataAdapter("select * from [tbl_PunchCorrect_ApplicationS] where convert(date,Atte_Date,103)>='" + TextBox1.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + TextBox2.Text.Trim() + "' and HODUserID='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1' and convert(date,Atte_Date,103)>'2022-03-01' ", con.Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);


                GridView1.DataSource = dt1;
                GridView1.DataBind();

                con.DisConnect();
            }
            else
            {

                SqlDataAdapter da1 = new SqlDataAdapter("select * from [tbl_PunchCorrect_ApplicationS] where convert(date,Atte_Date,103)>='" + TextBox1.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + TextBox2.Text.Trim() + "' and HODUserID='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1' and ApprovalStatus='" + DropDownList1.SelectedValue + "' and convert(date,Atte_Date,103)>'2022-03-01' ", con.Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);


                GridView1.DataSource = dt1;
                GridView1.DataBind();

                con.DisConnect();
            }
        }
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        Show_HODData();
    }
}