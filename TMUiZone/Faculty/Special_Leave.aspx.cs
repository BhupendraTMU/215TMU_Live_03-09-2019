using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using Utility;
using System.Configuration;

public partial class Faculty_Special_Leave : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    string HRHODsame = "";
    string forHRisHOD = "";
    string hodfirtapproval_ID = ""; string hodfirtapproval_Name = ""; string hodSecondapproval_ID = ""; string hodSecondapproval_Name = "";
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            if (!IsPostBack)
            {


                // SqlDataAdapter da = new SqlDataAdapter("select No_,[First Name],1 as 'access' from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where  [Global Dimension 1 Code]='TMDC'", con.Con);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //if (dt.Rows[0]["access"].ToString() == "1" && Session["GlobalDimension1Code"].ToString()=="TMDC")
                //{
                //    tdEmployee.Visible = true;
                //    drpEmployee.DataSource = dt;
                //    drpEmployee.DataValueField = "No_";
                //    drpEmployee.DataTextField = "First Name";
                //    drpEmployee.DataBind();
                //    InsertCo_Leave_ApplicationANDUpdateCo_Leave_DetailsFromNav(); //ashu 07-03-2017
                //    con.Update_HODLeave(Session["hod_ID_Leave1"].ToString(), Session["hod_ID_Leave2"].ToString(), Session["uid"].ToString(), Session["Company"].ToString());
                //    con.DisConnect();
                //}
                //else
                //{
                InsertCo_Leave_ApplicationANDUpdateCo_Leave_DetailsFromNav(); //ashu 07-03-2017
                con.Update_HODLeave(Session["hod_ID_Leave1"].ToString(), Session["hod_ID_Leave2"].ToString(), Session["uid"].ToString(), Session["Company"].ToString());
                con.DisConnect();
            }

            ExpiredataCo();


            lblfirstApproval.Text = Session["hod_Name_Leave1"].ToString() + "(" + Session["hod_ID_Leave1"].ToString() + ")";
            lblSecondApproval.Text = Session["hod_Name_Leave2"].ToString() + "(" + Session["hod_ID_Leave2"].ToString() + ")";

            if (Session["HRID_leave"].ToString() == "")
            {
                lblHRAuthority.Visible = true;
                btnSave.Enabled = false;
            }

            if (Session["HRID_leave"].ToString() != "")
            {
                lblHRAuthority.Visible = false;
                btnSave.Enabled = true;
            }
            if (Session["hod_ID_Leave1"].ToString() == "")
            {
                lblApprovalAuthority1.Visible = true;
                btnSave.Enabled = false;
            }
            if (Session["hod_ID_Leave1"].ToString() != "")
            {
                lblApprovalAuthority1.Visible = false;
                btnSave.Enabled = true;
            }

            if (Session["HRID_leave"].ToString() == "" || Session["hod_ID_Leave1"].ToString() == "")
            {
                btnSave.Enabled = false;
            }
            Showpermission();
            showAttendenceExpiry();
            showAttendenceExpiryIND();
            GetSelectedRecord();
            showHRHODisexhist();
            LeaveCoupto();
            if (!IsPostBack)
            {
                CalendarExtender1.StartDate = Convert.ToDateTime("01-01-2022 00:00:00");
                CalendarExtender2.StartDate = Convert.ToDateTime("01-01-2022 00:00:00");
                //CalendarExtender1.StartDate = DateTime.Now.AddDays(-7);
                //CalendarExtender2.StartDate = DateTime.Now.AddDays(-7);
                showLeaveBlance();
                showLeaveType();
                showoffLeavesetup(ddLeaveTypen.SelectedValue);
                show_LeaveDetailPending();
                //  ShowCoDetails();
                if (Session["UserGroup"].ToString() == "FACULTY" || Session["UserGroup"].ToString() == "PRINCIPAL")
                {
                    // tblArrangement.Visible = false;
                }
            }

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


            Showpermission();
            ShowpermissionLeave();


            if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
            {

                Show_RecommendLeaveCount();

            }

            else
            {
                Leave_Pending_ApprovalHOD_Count();
            }


        }
        catch (Exception)
        {
            // Response.Redirect("Default.aspx");
            Response.Redirect("../Default.aspx");
        }
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

    public void showHRHODisexhist()
    {

        SqlDataReader dr = Portalcon.SHow_showHODExhist(Session["uid"].ToString(), Session["CompanyTableEmployee"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            Portalcon.DisConnect();

            lnkLeaveApprovalforHOD.Visible = true;
            lblLeaveCount.Visible = true;
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
            lnkLeaveApprovalforHOD.Visible = false;
            lblLeaveCount.Visible = false;
        }
    }

    public void Show_RecommendLeaveCount()
    {

        SqlDataAdapter da = new SqlDataAdapter("select count(*) as 'Final' from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Leave] where ([SA for Non-Teach Staff]='" + Session["uid"].ToString() + "' or [SA for Teaching Staff]='" + Session["uid"].ToString() + "')and [Leave Code] collate Latin1_General_100_CS_AS in (select Leave_Type from tble_Leave_Approval where  (HODUserid='" + Session["uid"].ToString() + "' or HODUserid1='" + Session["uid"].ToString() + "' ) and Company_Name='TMU' and (Status='Approved' )  and [Rejected Approval]='No'  )", con.Con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (Convert.ToInt32(dt.Rows[0]["Final"]) > 0)
        {
            SqlDataReader dr = con.Leave_Recommend1(Session["Company"].ToString(), Session["uid"].ToString());
            dr.Read();
            lblLeaveCount.Text = dr["Status"].ToString();
            dr.Close();
            con.DisConnect();
        }
        else
        {





            SqlDataReader dr = con.Leave_Recommend(Session["Company"].ToString(), Session["uid"].ToString());
            dr.Read();
            lblLeaveCount.Text = dr["Status"].ToString();
            dr.Close();
            con.DisConnect();
        }
    }


    public void Leave_Pending_ApprovalHOD_Count()
    {
        SqlDataAdapter da = new SqlDataAdapter("select count(*) as 'Final' from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Leave] where ([SA for Non-Teach Staff]='" + Session["uid"].ToString() + "' or [SA for Teaching Staff]='" + Session["uid"].ToString() + "')and [Leave Code] collate Latin1_General_100_CS_AS in (select Leave_Type from tble_Leave_Approval where  (HODUserid='" + Session["uid"].ToString() + "' or HODUserid1='" + Session["uid"].ToString() + "' ) and Company_Name='TMU' and (Status='Approved' )  and [Rejected Approval]='No'  )", con.Con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (Convert.ToInt32(dt.Rows[0]["Final"]) > 0)
        {
            SqlDataReader dr = con.Leave_Approval_Count_Current1(Session["Company"].ToString(), Session["uid"].ToString());
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
        else
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


    private void GetSelectedRecord()
    {
        for (int i = 0; i < grdViewLeaveStatus.Rows.Count; i++)
        {
            RadioButton rb = (RadioButton)grdViewLeaveStatus.Rows[i]
                            .Cells[0].FindControl("rdSelect");
            if (rb != null)
            {
                if (rb.Checked)
                {

                    break;
                }
            }
        }
    }

    protected void lnkleaveview_Click(object sender, EventArgs e)
    {
        pnlLeaveApplication.Visible = true;
        pnlMain.Visible = false;
        pnlViewleaveDetail.Visible = false;

        clear1();
        showLeaveBlance();
        showLeaveType();
        showoffLeavesetup(ddLeaveTypen.SelectedValue);
        show_LeaveDetailPending();
        if (ddLeaveTypen.SelectedValue.Trim() == "CO")
        {
            grdCOLeave.Visible = true;
            ShowCoDetails();
        }
        else
        {
            grdCOLeave.Visible = false;
        }
    }
    string tble_Pay_Employee_Leave_Entitled = "";

    public void showLeaveBlance()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tble_Pay_Employee_Leave_Entitled = "[" + rccname + "$Pay Employee Leave Entitled" + "]";

        SqlDataReader dr = Portalcon.ShowLeaveBalance_Detail(tble_Pay_Employee_Leave_Entitled, Session["uid"].ToString());
        DataTable Dt = new DataTable();
        Dt.Load(dr);
        grdleave.DataSource = Dt;
        grdleave.DataBind();
        dr.Close();
    }

    public void showLeaveType()
    {
        SqlDataAdapter da = new SqlDataAdapter("select [Global Dimension 1 Code] from  [TMU$Employee] where [No_]='" + Session["uid"].ToString() + "' ", con1);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows[0]["Global Dimension 1 Code"].ToString() == "TMDC" || dt.Rows[0]["Global Dimension 1 Code"].ToString() == "TMMC")
        {
            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");
            string tblePayLeave = "[" + rccname + "$Pay Employee Leave Entitled" + "]";

            // SqlDataReader dr = Portalcon.ShowLeaveBalanceType(tblePayLeave);  //comment by Ashu 13-07-2016 
            SqlDataReader dr = Portalcon.ShowLeaveBalanceType(tblePayLeave, Session["uid"].ToString());  //Only those leave which have balance greater than 0 by user id
            ddLeaveTypen.DataSource = dr;
            ddLeaveTypen.DataTextField = "Leave Code";
            ddLeaveTypen.DataBind();
            dr.Close();
            Portalcon.DisConnect();
        }
        else
        {
            SqlDataAdapter da1 = new SqlDataAdapter("select '-------' as [Leave Code] union select 'LWP' as [Leave Code] union select [Leave code] as [Leave Code] from [TMU$Pay Employee Leave Entitled] where [Employee Code]='" + Session["uid"].ToString() + "' and [Leave Balance]>0   order by [Leave Code] ", con1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            ddLeaveTypen.DataSource = dt1;
            ddLeaveTypen.DataTextField = "Leave Code";
            ddLeaveTypen.DataBind();

        }
    }

    public void showHolidayCount()
    {
        //string ccname = Session["Company"].ToString();
        //string rccname = ccname.Replace(".", "_");
        //string Pay_Daily_Attendence_Detail = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        //SqlDataReader dr = Portalcon.Count_Holiday(Pay_Daily_Attendence_Detail, txtfromDate.Text, txtTodate.Text, Session["uid"].ToString());
        //dr.Read();
        //if (dr.HasRows)
        //{
        // lblcountholiday.Text = dr["Holiday"].ToString();
        //}
        //dr.Close();
        //Portalcon.DisConnect();
        lblcountholiday.Text = "0";
    }

    public void showoffDayCount()
    {
        //string ccname = Session["Company"].ToString();
        //string rccname = ccname.Replace(".", "_");
        //string Pay_Daily_Attendence_Detail = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
        //SqlDataReader dr = Portalcon.Count_Off_Day(Pay_Daily_Attendence_Detail, txtfromDate.Text, txtTodate.Text, Session["uid"].ToString());
        //dr.Read();
        //if (dr.HasRows)
        //{
        //lblCountOffDay.Text = dr["OffDay"].ToString();

        lblCountOffDay.Text = "0";
        //}
        //dr.Close();
        //Portalcon.DisConnect();
    }
    public void clear1()
    {
        txtfromDate.Text = "";
        txtTodate.Text = "";
        ddLeavePeriod.Text = "(Full-Day)";
        txtReason.Text = "";
        txtPhoneNo.Text = "";
        txtNoOfLeavePriod.Text = "0";
        ddLeaveTypen.Text = "-------";

        lblerrorMessage.Visible = false;
    }
    public void showoffLeavesetup(string LeaveType)
    {

        SqlDataReader dr = con.Show_tble_leave_setup(Session["Company"].ToString(), LeaveType);
        dr.Read();
        if (dr.HasRows)
        {
            lblHolidayexpect.Text = dr["Club Holiday"].ToString();
            lblOffdayexpect.Text = dr["Club Holiday"].ToString();
            if (lblHolidayexpect.Text == "No")
            {
                lblholiday.Text = "False";
                lblHolidayexpect.Text = "0";
                lblOffdayexpect.Text = "0";
            }
            if (lblHolidayexpect.Text == "Yes")
            {
                lblholiday.Text = "True";
                lblHolidayexpect.Text = "1";
                lblOffdayexpect.Text = "1";
            }
            if (lblOffdayexpect.Text == "No")
            {
                lbloffday.Text = "False";
                lblOffdayexpect.Text = "0";
                lblOffdayexpect.Text = "0";
            }
            if (lblOffdayexpect.Text == "Yes")
            {
                lbloffday.Text = "True";

                lblOffdayexpect.Text = "1";
                lblOffdayexpect.Text = "1";
            }
        }
        dr.Close();
        con.DisConnect();

    }
    int countg;
    public void NoofLeaveApplyinCo()
    {
        try
        {

            if (ddLeaveTypen.SelectedItem.Text == "CO")
            {

                string ss = "";
                string strname = "";
                foreach (GridViewRow gvrow in grdCOLeave.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelectCO");
                    if (chk != null & chk.Checked)
                    {
                        strname += ss + ',';
                        countg++;
                    }
                    lblerrorthanthree.Text = "";
                }


                decimal noofday = Convert.ToDecimal(txtNoOfLeavePriod.Text);
                if (noofday > countg && countg != 0)
                {
                    txtNoOfLeavePriod.Text = "0";
                    lblerrorthanthree.Text = "No of leave greater than selected week off / holiday";
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Can not apply more than three leave in CO');", true);

                }
                else if (countg == 0)
                {
                    txtNoOfLeavePriod.Text = "0";
                    lblerrorthanthree.Text = "if CO Leave balance is available then you have to select week off / holiday ";
                }
                else
                {
                    lblerrorthanthree.Text = "";
                    //lblerrorthanthree.Text = "No of leave greater than selected week off / holiday";
                    //int totalRowsCount = grdCOLeave.Rows.Count;
                    //string lblno = totalRowsCount.ToString();
                    //if (lblno >= noofday)
                    grdCOLeave.Enabled = true;
                }
            }
            else
            {
                lblerrorthanthree.Text = "";
                ShowGridCo();
            }
        }
        catch (Exception)
        {

        }
    }

    public void Show_NoOfDays()
    {


        if (ddLeaveTypen.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Select Leave Type !');", true);
            return;
        }
        showHolidayCount();
        showoffDayCount();
        showoffLeavesetup(ddLeaveTypen.SelectedValue);
        if (txtfromDate.Text == "" || txtTodate.Text == "")
        {


        }
        else
        {

            DateTime frodatecom = DateTime.ParseExact(txtfromDate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime Todatecom = DateTime.ParseExact(txtTodate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (frodatecom > Todatecom)
            {
                txtTodate.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be greater than From Date');", true);

            }
            else
            {
                if (lblHolidayexpect.Text == "0" && lblOffdayexpect.Text == "0")
                {
                    if (ddLeavePeriod.Text == "(Full-Day)")
                    {
                        string sdFom = txtfromDate.Text.Trim();
                        string sdTo = txtTodate.Text.Trim();
                        DateTime startDateTime1 = DateTime.ParseExact(sdFom, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime endDateTime1 = DateTime.ParseExact(sdTo, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        TimeSpan difference = endDateTime1 - startDateTime1;
                        string differenceString = difference.ToString();
                        string s = differenceString.ToString();
                        string h1 = difference.TotalDays.ToString();
                        double sh4 = Convert.ToDouble(h1);
                        double fl2 = Math.Floor(sh4);
                        int no1 = 1;
                        double no2 = Convert.ToDouble(no1);
                        double f13 = Convert.ToDouble(fl2 + no2);
                        lblTotalLeave.Text = f13.ToString();

                        int hcount = Convert.ToInt32(lblcountholiday.Text);
                        int offcount = Convert.ToInt32(lblCountOffDay.Text);
                        int hcoffcount = hcount + offcount;
                        int toleav = Convert.ToInt32(lblTotalLeave.Text);
                        txtNoOfLeavePriod.Text = (toleav - hcoffcount).ToString();
                        lblHalfDay.Text = "0";
                    }
                    if (ddLeavePeriod.Text == "(Half-Day)")
                    {
                        string sdFom = txtfromDate.Text.Trim();
                        string sdTo = txtTodate.Text.Trim();
                        DateTime startDateTime1 = DateTime.ParseExact(sdFom, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime endDateTime1 = DateTime.ParseExact(sdTo, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        TimeSpan difference = endDateTime1 - startDateTime1;
                        string differenceString = difference.ToString();
                        string s = differenceString.ToString();
                        string h1 = difference.TotalDays.ToString();
                        double sh4 = Convert.ToDouble(h1);
                        double fl2 = Math.Floor(sh4);
                        int no1 = 1;
                        double no2 = Convert.ToDouble(no1);
                        double f13 = Convert.ToDouble(fl2 + no2);
                        lblTotalLeave.Text = f13.ToString();

                        int hcount = Convert.ToInt32(lblcountholiday.Text);
                        int offcount = Convert.ToInt32(lblCountOffDay.Text);
                        int hcoffcount = hcount + offcount;
                        int toleav = Convert.ToInt32(lblTotalLeave.Text);
                        int tolnoflhalf = (toleav - hcoffcount);
                        double thald = Convert.ToDouble(tolnoflhalf);
                        lblHalfDay.Text = thald.ToString();
                        double ttt1 = thald / 2;
                        txtNoOfLeavePriod.Text = ttt1.ToString();
                    }
                }
                if (lblHolidayexpect.Text == "1" && lblOffdayexpect.Text == "1")
                {
                    if (ddLeavePeriod.Text == "(Full-Day)")
                    {
                        string sdFom = txtfromDate.Text.Trim();
                        string sdTo = txtTodate.Text.Trim();
                        DateTime startDateTime1 = DateTime.ParseExact(sdFom, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime endDateTime1 = DateTime.ParseExact(sdTo, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        TimeSpan difference = endDateTime1 - startDateTime1;
                        string differenceString = difference.ToString();
                        string s = differenceString.ToString();
                        string h1 = difference.TotalDays.ToString();
                        double sh4 = Convert.ToDouble(h1);
                        double fl2 = Math.Floor(sh4);
                        int no1 = 1;
                        double no2 = Convert.ToDouble(no1);
                        double f13 = Convert.ToDouble(fl2 + no2);
                        lblHalfDay.Text = "0";
                        txtNoOfLeavePriod.Text = f13.ToString();
                    }
                    if (ddLeavePeriod.Text == "(Half-Day)")
                    {
                        string sdFom = txtfromDate.Text.Trim();
                        string sdTo = txtTodate.Text.Trim();
                        DateTime startDateTime1 = DateTime.ParseExact(sdFom, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime endDateTime1 = DateTime.ParseExact(sdTo, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        TimeSpan difference = endDateTime1 - startDateTime1;
                        string differenceString = difference.ToString();
                        string s = differenceString.ToString();
                        string h1 = difference.TotalDays.ToString();
                        double sh4 = Convert.ToDouble(h1);
                        double fl2 = Math.Floor(sh4);
                        int no1 = 1;
                        double no2 = Convert.ToDouble(no1);
                        double f13 = Convert.ToDouble(fl2 + no2);

                        double thald = Convert.ToDouble(f13);
                        lblHalfDay.Text = thald.ToString();
                        double ttt1 = thald / 2;
                        txtNoOfLeavePriod.Text = ttt1.ToString();
                    }

                }
                if (lblHolidayexpect.Text == "0" && lblOffdayexpect.Text == "1")
                {
                    if (ddLeavePeriod.Text == "(Full-Day)")
                    {
                        string sdFom = txtfromDate.Text.Trim();
                        string sdTo = txtTodate.Text.Trim();
                        DateTime startDateTime1 = DateTime.ParseExact(sdFom, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime endDateTime1 = DateTime.ParseExact(sdTo, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        TimeSpan difference = endDateTime1 - startDateTime1;
                        string differenceString = difference.ToString();
                        string s = differenceString.ToString();
                        string h1 = difference.TotalDays.ToString();
                        double sh4 = Convert.ToDouble(h1);
                        double fl2 = Math.Floor(sh4);
                        int no1 = 1;
                        double no2 = Convert.ToDouble(no1);
                        double f13 = Convert.ToDouble(fl2 + no2);
                        lblTotalLeave.Text = f13.ToString();

                        int hlco = Convert.ToInt32(lblcountholiday.Text);

                        int toleav = Convert.ToInt32(lblTotalLeave.Text);
                        lblHalfDay.Text = "0";
                        txtNoOfLeavePriod.Text = (toleav - hlco).ToString();
                    }
                    if (ddLeavePeriod.Text == "(Half-Day)")
                    {
                        string sdFom = txtfromDate.Text.Trim();
                        string sdTo = txtTodate.Text.Trim();
                        DateTime startDateTime1 = DateTime.ParseExact(sdFom, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime endDateTime1 = DateTime.ParseExact(sdTo, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        TimeSpan difference = endDateTime1 - startDateTime1;
                        string differenceString = difference.ToString();
                        string s = differenceString.ToString();
                        string h1 = difference.TotalDays.ToString();
                        double sh4 = Convert.ToDouble(h1);
                        double fl2 = Math.Floor(sh4);
                        int no1 = 1;
                        double no2 = Convert.ToDouble(no1);
                        double f13 = Convert.ToDouble(fl2 + no2);
                        lblTotalLeave.Text = f13.ToString();
                        int hlco = Convert.ToInt32(lblcountholiday.Text);
                        int toleav = Convert.ToInt32(lblTotalLeave.Text);
                        int tolaminus = (toleav - hlco);
                        double tol11 = Convert.ToDouble(tolaminus);
                        lblHalfDay.Text = tol11.ToString();
                        double tol22 = tol11 / 2;
                        txtNoOfLeavePriod.Text = tol22.ToString();


                    }
                }
                if (lblHolidayexpect.Text == "1" && lblOffdayexpect.Text == "0")
                {
                    if (ddLeavePeriod.Text == "(Full-Day)")
                    {
                        string sdFom = txtfromDate.Text.Trim();
                        string sdTo = txtTodate.Text.Trim();
                        DateTime startDateTime1 = DateTime.ParseExact(sdFom, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime endDateTime1 = DateTime.ParseExact(sdTo, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        TimeSpan difference = endDateTime1 - startDateTime1;
                        string differenceString = difference.ToString();
                        string s = differenceString.ToString();
                        string h1 = difference.TotalDays.ToString();
                        double sh4 = Convert.ToDouble(h1);
                        double fl2 = Math.Floor(sh4);
                        int no1 = 1;
                        double no2 = Convert.ToDouble(no1);
                        double f13 = Convert.ToDouble(fl2 + no2);
                        lblTotalLeave.Text = f13.ToString();

                        int offco = Convert.ToInt32(lblCountOffDay.Text);
                        int toleav = Convert.ToInt32(lblTotalLeave.Text);
                        lblHalfDay.Text = "0";
                        txtNoOfLeavePriod.Text = (toleav - offco).ToString();
                    }
                    if (ddLeavePeriod.Text == "(Half-Day)")
                    {
                        string sdFom = txtfromDate.Text.Trim();
                        string sdTo = txtTodate.Text.Trim();
                        DateTime startDateTime1 = DateTime.ParseExact(sdFom, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime endDateTime1 = DateTime.ParseExact(sdTo, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        TimeSpan difference = endDateTime1 - startDateTime1;
                        string differenceString = difference.ToString();
                        string s = differenceString.ToString();
                        string h1 = difference.TotalDays.ToString();
                        double sh4 = Convert.ToDouble(h1);
                        double fl2 = Math.Floor(sh4);
                        int no1 = 1;
                        double no2 = Convert.ToDouble(no1);
                        double f13 = Convert.ToDouble(fl2 + no2);
                        lblTotalLeave.Text = f13.ToString();
                        int offco = Convert.ToInt32(lblCountOffDay.Text);
                        int toleav = Convert.ToInt32(lblTotalLeave.Text);
                        int tolaminus = (toleav - offco);
                        double tol11 = Convert.ToDouble(tolaminus);
                        lblHalfDay.Text = tol11.ToString();
                        double tol22 = tol11 / 2;
                        txtNoOfLeavePriod.Text = tol22.ToString();


                    }
                }
            }
            if (frodatecom > Todatecom)
            {
                txtTodate.Text = txtfromDate.Text;
            }
            if ((Convert.ToDecimal(txtNoOfLeavePriod.Text) >= 3 && (ddLeaveTypen.Text == "ML")) || (Convert.ToDecimal(txtNoOfLeavePriod.Text) >= 1 && (ddLeaveTypen.Text == "AL")))
            {
                flUpload.Visible = true;
                rfvflUpload.Enabled = true;
            }
            else
            {
                flUpload.Visible = false;
                rfvflUpload.Enabled = false;
            }
        }
    }





    protected void lnkRejectLeaveDetail_Click(object sender, EventArgs e)
    {
        pnlViewleaveDetail.Visible = true;
        pnlLeaveApplication.Visible = false;
        pnlMain.Visible = false;

    }
    protected void lnkApprovedApproveddetail_Click(object sender, EventArgs e)
    {
        pnlMain.Visible = false;

        pnlViewleaveDetail.Visible = false;
        pnlLeaveApplication.Visible = false;
        clear1();

    }
    public void show_LeaveDetailPending()
    {
        SqlDataReader dr = con.Show_LeavePendingDetail(Session["uid"].ToString(), Session["Company"].ToString());
        DataTable Dt = new DataTable();
        Dt.Load(dr);
        grdLeaveDetail.DataSource = Dt;
        grdLeaveDetail.DataBind();
        dr.Close();
        con.DisConnect();

    }

    public void show_LeaveStatus()
    {
        if (ddstatus.Text == "All")
        {
            SqlDataReader dr = con.Show_LeaveViewwith_Date_StatusAll(txtFromDateView.Text, txtToDateView.Text, Session["uid"].ToString(), Session["Company"].ToString());
            DataTable Dt = new DataTable();
            Dt.Load(dr);
            grdViewLeaveStatus.DataSource = Dt;
            grdViewLeaveStatus.DataBind();
            dr.Close();
            con.DisConnect();
        }
        else
        {

            SqlDataAdapter da = new SqlDataAdapter("select convert(varchar(11),(convert(date,From_Date )),113) as F_Date,convert(varchar(11),(convert(date,To_Date )),113) as T_Date,case when isnull(FinalApprovalStatus,5)=0 and isnull(FinalApprovalId,'')!='' then 'Recommend' else Status end as 'Status1',* from tble_Leave_Approval where UserID='" + Session["uid"].ToString() + "' and Company_Name='" + Session["Company"].ToString() + "' and convert(date, To_Date,111) >= '" + txtFromDateView.Text + "' and convert(date, To_Date,111) <='" + txtToDateView.Text + "' and Status='" + ddstatus.Text + "' order by convert(date,To_Date,111) desc", con.Con);
            DataTable dt = new DataTable();

            da.Fill(dt);
            con.DisConnect();
            //SqlDataReader dr = con.Show_LeaveViewwith_Date_Status(txtFromDateView.Text, txtToDateView.Text, Session["uid"].ToString(), ddstatus.Text, Session["Company"].ToString());
            //DataTable Dt = new DataTable();
            //Dt.Load(dr);
            grdViewLeaveStatus.DataSource = dt;
            grdViewLeaveStatus.DataBind();

        }
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
        string type = "For Leave";
        SqlDataReader dr = con.SHow_tblesetupforpriorityforApproval(type, Session["Company"].ToString());
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
        }
        dr.Close();
        con.DisConnect();
    }
    string fdaterep = "";
    public void showMinFromDate()
    {
        SqlDataReader dr = con.Show_tble_Leave_ApprovalRepetiondateMinFromDate(Session["uid"].ToString(), Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            fdaterep = dr["From_date"].ToString();
        }
        dr.Close();
        con.DisConnect();
    }

    public void ShowCoDetails()
    {

        string coUpto = lblleaveUpto.Text.Trim();

        //SqlDataReader drupto = con.SHowCoupto(Session["Company"].ToString());
        //drupto.Read();
        //if (drupto.HasRows)
        //{
        //    coUpto = drupto["Comp Leave Upto"].ToString();
        //}
        //drupto.Close();
        //con.DisConnect();


        string cudate = System.DateTime.Now.ToString("dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture).Trim();
        DateTime todate = DateTime.ParseExact(cudate, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime fdate = DateTime.ParseExact(cudate, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture).AddDays(-Convert.ToDouble(coUpto));
        string todatedr = todate.ToString("dd MMM yyyy");
        string fdatedr = fdate.ToString("dd MMM yyyy");
        // DateTime cudate1 = DateTime.ParseExact(cudate, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);


        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string tblePaydailyattendance = "[" + rccname + "$Pay Daily Attendence Detail" + "]";

        SqlDataAdapter da = new SqlDataAdapter("select * from " + tblePaydailyattendance + " where [Attendance Marked]='1' and [Employee Code]='" + Session["uid"].ToString() + "' and CONVERT(date, [Attendance Date],103) >='" + fdatedr + "'  and CONVERT(date, [Attendance Date],103) <='" + todatedr + "' and [CO]='1' and [CO Status]='Pending' or [Attendance Marked]='1' and [Employee Code]='" + Session["uid"].ToString() + "' and CONVERT(date, [Attendance Date],103) >='" + fdatedr + "'  and CONVERT(date, [Attendance Date],103) <='" + todatedr + "' and [CO]='1' and [CO Status]='Pending'", Portalcon.Con);
        DataSet ds = new DataSet();
        da.Fill(ds, "paydailyatt");
        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {

            string lblAttendacedate = ""; string lblTimeingrd = ""; string lblTimeinoutgrd = ""; string lblHoursPresent = ""; string lblExpireCO = ""; string coRemarks = "";
            string CODATE = "";
            lblAttendacedate = ds.Tables[0].Rows[i]["Attendance Date"].ToString();
            DateTime lblAttendacedate1 = Convert.ToDateTime(lblAttendacedate);
            lblAttendacedate = lblAttendacedate1.ToString("dd MMM yyyy");
            lblTimeingrd = ds.Tables[0].Rows[i]["Time in"].ToString();
            DateTime lblTimeingrd1 = Convert.ToDateTime(lblTimeingrd);
            lblTimeingrd = lblTimeingrd1.ToString("HH:MM");

            lblTimeinoutgrd = ds.Tables[0].Rows[i]["Time Out"].ToString();
            DateTime lblTimeinoutgrd1 = Convert.ToDateTime(lblTimeinoutgrd);
            lblTimeinoutgrd = lblTimeinoutgrd1.ToString("HH:MM");

            lblHoursPresent = ds.Tables[0].Rows[i]["Hours Present"].ToString();
            decimal lblHoursPresent2 = Convert.ToDecimal(lblHoursPresent);
            lblHoursPresent = lblHoursPresent2.ToString("00.00");
            coRemarks = ds.Tables[0].Rows[i]["Co Remarks"].ToString();
            DateTime DTCURDATE = Convert.ToDateTime(lblAttendacedate);
            CODATE = DTCURDATE.ToString("dd MMM yyyy");
            DateTime lbldatett = DateTime.ParseExact(CODATE, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture).AddDays(Convert.ToDouble(coUpto));
            lblExpireCO = lbldatett.ToString("dd MMM yyyy");

            SqlDataReader d2r = con.Show_repeation_tbl_Co_Leave_Details(Session["uid"].ToString(), CODATE, Session["Company"].ToString());
            d2r.Read();
            if (d2r.HasRows)
            {
                d2r.Close();
                con.DisConnect();
            }
            else
            {
                d2r.Close();
                con.DisConnect();
                try
                {

                    con.insert_tbl_Co_Leave_Details(Session["uid"].ToString(), Session["Fulname"].ToString(), Session["Company"].ToString(), CODATE, lblTimeingrd, lblTimeinoutgrd, lblHoursPresent, lblExpireCO, "");
                    con.DisConnect();
                    Portalcon.updateCoLeaveStatusinPaydailyAttendance(tblePaydailyattendance, CODATE, Session["uid"].ToString(), "Processing");
                    Portalcon.DisConnect();
                }
                catch (Exception)
                {

                }
            }

        }

        ShowGridCo();
        //sqldatareader dr = portalcon.showleavebalance_detail(tble_pay_employee_leave_entitled, session["uid"].tostring());
        //datatable dt = new datatable();
        //dt.load(dr);
        //grdleave.datasource = dt;
        //grdleave.databind();
        //dr.close();
    }
    public void ShowGridCo()
    {



        //SqlDataReader dr = con.Show_tbl_Co_Leave_DetailsinGrid(Session["uid"].ToString(), Session["Company"].ToString());
        //dr.Read();
        //if (dr.HasRows)
        //{
        //    ddLeaveTypen.SelectedValue = "CO";
        //}
        //dr.Close();

        SqlDataReader dr1 = con.Show_tbl_Co_Leave_DetailsinGrid(Session["uid"].ToString(), Session["Company"].ToString());
        DataTable dt = new DataTable();
        dt.Load(dr1);
        grdCOLeave.DataSource = dt;
        grdCOLeave.DataBind();
        dr1.Close();
        Portalcon.DisConnect();

    }
    public void ApplyCoLeave()
    {
        string AutoNo = "";
        SqlDataReader dr = con.Show_MaxNotble_Leave_Approval(Session["uid"].ToString(), Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            AutoNo = dr["AutoNo"].ToString();
        }
        dr.Close();
        con.DisConnect();

        foreach (GridViewRow gvrow in grdCOLeave.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelectCO");
            Label lblAttendacedate = (Label)gvrow.FindControl("lblAttendacedate");
            Label lblTimeingrd = (Label)gvrow.FindControl("lblTimeingrd");
            Label lblTimeinoutgrd = (Label)gvrow.FindControl("lblTimeinoutgrd");
            Label lblHoursPresent = (Label)gvrow.FindControl("lblHoursPresent");
            Label lblidff = (Label)gvrow.FindControl("lblidff");

            Label lblExpireCO = (Label)gvrow.FindControl("lblExpireCO");
            Label lblidff2 = (Label)gvrow.FindControl("lblidff");

            if (chk.Checked)
            {
                con.Update_tbl_Co_Leave_Details("Processing", lblidff2.Text, AutoNo);
                con.DisConnect();
            }



            //if (chk.Checked)
            //{
            //    SqlDataReader dr = con.Show_repeation_tbl_Co_Leave_Details(Session["uid"].ToString(), Convert.ToDateTime(lblAttendacedate.Text), Session["Company"].ToString());
            //    dr.Read();
            //    if (dr.HasRows)
            //    {
            //        dr.Close();
            //        con.DisConnect();
            //    }
            //    else
            //    {
            //        dr.Close();
            //        con.DisConnect();
            //  con.insert_tbl_Co_Leave_Details(Session["uid"].ToString(), Session["Fulname"].ToString(), Session["Company"].ToString(), Convert.ToDateTime(lblAttendacedate.Text), lblTimeingrd.Text, lblTimeinoutgrd.Text, lblHoursPresent.Text, Convert.ToDateTime(lblExpireCO.Text), "");
            //        con.DisConnect();
            //    }

            //    //Response.Write("ddd");
            //}
        }

        //foreach (GridViewRow row in grdCOLeave.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {

        //        CheckBox chkRow = (row.Cells[0].FindControl("chkSelectCO") as CheckBox);
        //        Label lblAttendacedate = (row.Cells[0].FindControl("lblAttendacedate") as Label);
        //        Label lblTimeingrd = (row.Cells[0].FindControl("lblTimeingrd") as Label);
        //        Label lblTimeinoutgrd = (row.Cells[0].FindControl("lblTimeinoutgrd") as Label);
        //        Label lblHoursPresent = (row.Cells[0].FindControl("lblHoursPresent") as Label);

        //        Label lblExpireCO = (row.Cells[0].FindControl("lblExpireCO") as Label);



        //        if (chkRow.Checked == true)
        //        {
        //            SqlDataReader dr = con.Show_repeation_tbl_Co_Leave_Details(Session["uid"].ToString(), Convert.ToDateTime(lblAttendacedate.Text), Session["Company"].ToString());
        //            dr.Read();
        //            if (dr.HasRows)
        //            {
        //                dr.Close();
        //                con.DisConnect();
        //            }
        //            else
        //            {
        //                dr.Close();
        //                con.DisConnect();
        //                con.insert_tbl_Co_Leave_Details(Session["uid"].ToString(), Session["Fulname"].ToString(), Session["Company"].ToString(), Convert.ToDateTime(lblAttendacedate.Text), lblTimeingrd.Text, lblTimeinoutgrd.Text, lblHoursPresent.Text, Convert.ToDateTime(lblExpireCO.Text), "");
        //                con.DisConnect();
        //            }
        //        }
        //    }
        //}
        ShowGridCo();
    }
    string slno = "";
    string Status = "Pending";
    string EmployeLeaveEntitled = "";
    protected void btnSave_Click(object sender, EventArgs e)
    {

        //--------------------------Arrangement-----------Duplicate Leave---------------06-07-2016

        if (chkArrangement.Checked == true)
        {
            if (Arrangement() > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Complete your Arrangement First !');", true);
                return;
            }
        }
        else
        {
            if (txtReason.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Remarks Couldnot be Blank');", true);
                return;
            }
        }
        if (DuplicateLeave() > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have included Applied Leave ! check the Leave From Date and To Date');", true);
            return;
        }
        //--------------------------Arrangement--------------------------06-07-2016

        //string confirmValue = Request.Form["confirm_value"];
        //if (confirmValue == "Yes")
        //{
        showLeaveBlanceOption();
        showMinFromDate();
        leavetypedrop = ddLeavePeriod.Text;
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        EmployeLeaveEntitled = "[" + rccname + "$Pay Employee Leave Entitled" + "]";

        SqlDataReader dr = con.Show_tble_Leave_ApprovalRepetiondateMaxToDate(Session["uid"].ToString(), Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            DateTime fromDate = DateTime.ParseExact(txtfromDate.Text, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime Todate = DateTime.ParseExact(txtTodate.Text, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);


            string tdaterep = dr["To_Date"].ToString();
            DateTime fdaterep1 = DateTime.ParseExact(fdaterep, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime tdaterep1 = DateTime.ParseExact(tdaterep, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dr.Close();
            con.DisConnect();

            if (fromDate >= fdaterep1 && fromDate <= tdaterep1)
            {
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('You have already applied for leave at this date you can choose another date ');", true);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Leave already applied for this date');", true);
            }
            else if (Todate <= tdaterep1 && Todate >= fdaterep1)
            {
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('You have already applied for leave at this date you can choose another date ');", true);

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Leave already applied for this date');", true);
            }


            else
            {

                if (ddLeaveTypen.SelectedItem.Text.Trim() == "CO")
                {

                    string ss = "";
                    string strname = "";
                    foreach (GridViewRow gvrow in grdCOLeave.Rows)
                    {
                        CheckBox chk = (CheckBox)gvrow.FindControl("chkSelectCO");
                        if (chk != null & chk.Checked)
                        {
                            strname += ss + ',';
                            count++;
                        }
                    }
                    if (count > Convert.ToInt32(txtNoOfLeavePriod.Text) || count < Convert.ToInt32(txtNoOfLeavePriod.Text))
                    {

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please select worked on week off / holiday equal from no of leave');", true);
                        // grdCOLeave.Enabled = false;
                    }
                    if (count == Convert.ToInt32(txtNoOfLeavePriod.Text))
                    {
                        if (lblerrorMessage.Text != "")
                        {
                            lblerrorMessage.Visible = true;
                        }
                        else
                        {
                            lblerrorMessage.Text = "";
                            lblerrorMessage.Visible = false;
                            LeaveApply();
                            ApplyCoLeave();

                            clear1();
                        }
                    }
                }
                else
                {
                    if (lblerrorMessage.Text != "")
                    {
                        lblerrorMessage.Visible = true;
                    }
                    else
                    {
                        lblerrorMessage.Text = "";
                        lblerrorMessage.Visible = false;
                        LeaveApply();

                        clear1();
                    }
                }
            }
        }
        else
        {

            dr.Close();
            con.DisConnect();

            if (ddLeaveTypen.SelectedItem.Text.Trim() == "CO")
            {

                string ss = "";
                string strname = "";
                foreach (GridViewRow gvrow in grdCOLeave.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelectCO");
                    if (chk != null & chk.Checked)
                    {
                        strname += ss + ',';
                        count++;
                    }
                }
                if (count > Convert.ToInt32(txtNoOfLeavePriod.Text) || count < Convert.ToInt32(txtNoOfLeavePriod.Text))
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please select CO leave equal from no of leave');", true);
                    // grdCOLeave.Enabled = false;
                }
                if (count == Convert.ToInt32(txtNoOfLeavePriod.Text))
                {
                    LeaveApply();
                    ApplyCoLeave();
                    clear1();
                }
            }
            else
            {
                LeaveApply();
                clear1();
            }
            //LeaveApply();
            //if (ddLeaveTypen.SelectedItem.Text.Trim() == "CO")
            //{
            //    ApplyCoLeave();
            //}
            //clear1();
        }

        //}

    }
    string PayrollProcessingMonthDate = "";
    public void Show_CompnyPolicy()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string PayCompanyPolicy = "[" + rccname + "$Pay Company Policy" + "]";
        SqlDataReader dr = Portalcon.Show_PayCompanyPolicy(PayCompanyPolicy);
        dr.Read();
        if (dr.HasRows)
        {
            PayrollProcessingMonthDate = System.DateTime.Now.ToString();
            //System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");              //dr["Payroll Processing Month Date"].ToString();
            dr.Close();
            Portalcon.DisConnect();
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
        }
    }

    string TeachingStaffLeave = ""; string NonTeachingStaffLeave = ""; string FourthClassLeave = ""; string NursingClass; string WardBoy = "";
    decimal TeachingStaffLeave1; decimal NonTeachingStaffLeave1; decimal FourthClassLeave1; decimal NursingClass1; decimal WardBoy1;

    public void OnConfirm(object sender, EventArgs e)
    {
        //ashu on 19/09/2017--start
        if (ddLeavePeriod.SelectedValue == "(Full-Day)" && ddShiftType.SelectedValue != "0".ToString()) //ashu on 19/09/2017
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('In Case of Full-Day, Day Mode should not be 1st Half/2nd Half !!');", true);
            return;
        }
        //ashu on 19/09/2017--END

        txtReason.Text = txtReason.Text.Replace("'", "").ToString().Trim();
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            if (Session["HODLoginPage"].ToString() == "" || Session["HODLoginPage"].ToString() == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Hod not Tagged , Contact System Admin !');", true);
            }

            else
            {
                if (ddLeaveTypen.SelectedItem.Text.Trim() == "CL")
                {
                    decimal txtNoOfLeavePriodCL = Convert.ToDecimal(txtNoOfLeavePriod.Text.Trim());
                    decimal lblNonof_Cl_leaveCL = Convert.ToDecimal(lblNonof_Cl_leave.Text.Trim());

                    if (txtNoOfLeavePriodCL <= lblNonof_Cl_leaveCL)
                    {
                        LeaveApplicationDetails();
                        showLeaveBlanceOption();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Can not applied more than CL leave ');", true);
                    }

                }
                else
                {


                    LeaveApplicationDetails();
                    showLeaveBlanceOption();
                }

            }
        }


    }


    public void Valadation_CL()
    {
        if (ddLeaveTypen.SelectedItem.Text == "CL")
        {
            Show_CompnyPolicy();

            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");
            string Payleavecl = "[" + rccname + "$Pay Leave" + "]";
            string LeaveAlloted;
            SqlDataReader dr = Portalcon.Show_PayLeavedetailof_CL(Payleavecl);
            dr.Read();
            if (dr.HasRows)
            {
                TeachingStaffLeave = dr["Teaching Staff Leave"].ToString();
                NonTeachingStaffLeave = dr["Non- Teaching Staff Leave"].ToString();
                FourthClassLeave = dr["Fourth Class Leave"].ToString();
                NursingClass = dr["Nursing Class"].ToString();
                WardBoy = dr["Ward Boy"].ToString();
                dr.Close();
                Portalcon.DisConnect();
                //if (Session["EmployeePostingGroupl"].ToString() == "TEACH" || Session["EmployeePostingGroupl"].ToString() == "TUTAR")
                //{
                //    TeachingStaffLeave1 = Convert.ToDecimal(TeachingStaffLeave);

                //    TeachingStaffLeave1 = TeachingStaffLeave1 / 12;
                //    DateTime PayrollProcessingMonthDate1 = Convert.ToDateTime(PayrollProcessingMonthDate);
                //    string PayrollProcessingMonthDate2 = PayrollProcessingMonthDate1.ToString("MM");
                //    Decimal PayrollProcessingMonth = Convert.ToDecimal(PayrollProcessingMonthDate2);
                //    decimal PayrollProcessingMonth1 = 12 - PayrollProcessingMonth * TeachingStaffLeave1;
                //    decimal lblLeaveBalance_CLw = Convert.ToDecimal(lblLeaveBalance_CL.Text);
                //    decimal AppliedLeavebalance_CL = lblLeaveBalance_CLw - PayrollProcessingMonth1;
                //    lblNonof_Cl_leave.Text = AppliedLeavebalance_CL.ToString("00.00");
                //}
                //if (Session["EmployeePostingGroupl"].ToString() == "NON-TEACH" || Session["EmployeePostingGroupl"].ToString() == "OTHER")
                //{

                SqlCommand cmd = new SqlCommand("  select [CL Leave] from [TMU$Posted Capitalize CWIP Header] where No_=(select [Leave Category Code] from [TMU$Employee] where [No_]='" + Session["uid"].ToString().Trim() + "')", con1);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                LeaveAlloted = dt.Rows[0]["CL Leave"].ToString();



                NonTeachingStaffLeave1 = Convert.ToDecimal(LeaveAlloted);

                NonTeachingStaffLeave1 = NonTeachingStaffLeave1 / 12;
                DateTime PayrollProcessingMonthDate1 = Convert.ToDateTime(PayrollProcessingMonthDate);
                string PayrollProcessingMonthDate2 = PayrollProcessingMonthDate1.ToString("MM");
                Decimal PayrollProcessingMonth = Convert.ToDecimal(PayrollProcessingMonthDate2);
                decimal PayrollProcessingMonth1 = Convert.ToDecimal(LeaveAlloted) - PayrollProcessingMonth * NonTeachingStaffLeave1;
                decimal lblLeaveBalance_CLw = Convert.ToDecimal(lblLeaveBalance_CL.Text);
                decimal AppliedLeavebalance_CL = lblLeaveBalance_CLw - PayrollProcessingMonth1;
                lblNonof_Cl_leave.Text = AppliedLeavebalance_CL.ToString("00.00");
                //}

                //if (Session["EmployeePostingGroupl"].ToString() == "NURSING")
                //{
                //    NursingClass1 = Convert.ToDecimal(NursingClass);
                //    NursingClass1 = NursingClass1 / 12;
                //    DateTime PayrollProcessingMonthDate1 = Convert.ToDateTime(PayrollProcessingMonthDate);
                //    string PayrollProcessingMonthDate2 = PayrollProcessingMonthDate1.ToString("MM");
                //    Decimal PayrollProcessingMonth = Convert.ToDecimal(PayrollProcessingMonthDate2);
                //    decimal PayrollProcessingMonth1 = 12 - PayrollProcessingMonth * NursingClass1;
                //    decimal lblLeaveBalance_CLw = Convert.ToDecimal(lblLeaveBalance_CL.Text);
                //    decimal AppliedLeavebalance_CL = lblLeaveBalance_CLw - PayrollProcessingMonth1;
                //    lblNonof_Cl_leave.Text = AppliedLeavebalance_CL.ToString("00.00");
                //}

                //if (Session["EmployeePostingGroupl"].ToString() == "FOURTH")
                //{
                //    FourthClassLeave1 = Convert.ToDecimal(FourthClassLeave);
                //    FourthClassLeave1 = FourthClassLeave1 / 12;
                //    DateTime PayrollProcessingMonthDate1 = Convert.ToDateTime(PayrollProcessingMonthDate);
                //    string PayrollProcessingMonthDate2 = PayrollProcessingMonthDate1.ToString("MM");
                //    Decimal PayrollProcessingMonth = Convert.ToDecimal(PayrollProcessingMonthDate2);
                //    decimal PayrollProcessingMonth1 = 12 - PayrollProcessingMonth * FourthClassLeave1;
                //    decimal lblLeaveBalance_CLw = Convert.ToDecimal(lblLeaveBalance_CL.Text);
                //    decimal AppliedLeavebalance_CL = lblLeaveBalance_CLw - PayrollProcessingMonth1;
                //    lblNonof_Cl_leave.Text = AppliedLeavebalance_CL.ToString("00.00");
                //}

                //if (Session["EmployeePostingGroupl"].ToString() == "WARD/AAYA")
                //{
                //    WardBoy1 = Convert.ToDecimal(WardBoy);
                //    WardBoy1 = WardBoy1 / 12;
                //    DateTime PayrollProcessingMonthDate1 = Convert.ToDateTime(PayrollProcessingMonthDate);
                //    string PayrollProcessingMonthDate2 = PayrollProcessingMonthDate1.ToString("MM");
                //    Decimal PayrollProcessingMonth = Convert.ToDecimal(PayrollProcessingMonthDate2);
                //    decimal PayrollProcessingMonth1 = 12 - PayrollProcessingMonth * WardBoy1;
                //    decimal lblLeaveBalance_CLw = Convert.ToDecimal(lblLeaveBalance_CL.Text);
                //    decimal AppliedLeavebalance_CL = lblLeaveBalance_CLw - PayrollProcessingMonth1;
                //    lblNonof_Cl_leave.Text = AppliedLeavebalance_CL.ToString("00.00");
                //}

            }
            else
            {
                dr.Close();
                Portalcon.DisConnect();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There is no data available in Pay leave Please Contact System Admin');", true);
            }



        }

    }


    public void LeaveApplicationDetails()
    {

        //--------------------------Arrangement-----------Duplicate Leave---------------06-07-2016        
        if (chkArrangement.Checked == true)
        {
            if (Arrangement() > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Complete your Arrangement First !');", true);
                return;
            }
        }
        else
        {
            if (txtReason.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Remarks Couldnot be Blank');", true);
                return;
            }
        }
        if (DuplicateLeave() > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have included Applied Leave ! check the Leave From Date and To Date');", true);
            return;
        }


        //--------------------------Arrangement--------------------------06-07-2016

        //-------------------------------------Hod/HR---------------------checklist------------ashu--29-07-2016
        // if (Session["hod_Name2"] == null)
        //if (Session["hod_Name2"] == null)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Hod not Tagged , Contact System Admin !');", true);
        //    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can't apply the Leave because Hod is not Tagged , Contact System Admin');", true);
        //    return;
        //}
        if (Session["hod_email2"] == null) { Session["hod_email2"] = "ashutosh.kumar@corporateserve.com"; return; }

        //if (Session["HRName"] == null)
        //{
        //    Session["HRName"] = "";
        //    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not apply the Leave because HR is not Tagged , Contact System Admin');", true);
        //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can't apply the Leave because HR is not Tagged , Contact System Admin');", true);
        //    // return;
        //}
        // if (Session["hr_email2"] == null) { Session["hr_email2"] = "ashutosh.kumahhr@corporateserve.com"; return; }
        Session["hr_email2"] = "";
        //-------------------------------------Hod/HR---------------------checklist------------ashu--29-07-2016-------end
        //string confirmValue = Request.Form["confirm_value"];
        //if (confirmValue == "Yes")
        //{
        showLeaveBlanceOption();
        showMinFromDate();
        leavetypedrop = ddLeavePeriod.Text;
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        EmployeLeaveEntitled = "[" + rccname + "$Pay Employee Leave Entitled" + "]";

        SqlDataReader dr = con.Show_tble_Leave_ApprovalRepetiondateMaxToDate(Session["uid"].ToString(), Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            DateTime fromDate = DateTime.ParseExact(txtfromDate.Text, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime Todate = DateTime.ParseExact(txtTodate.Text, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);


            string tdaterep = dr["To_Date"].ToString();
            DateTime fdaterep1 = DateTime.ParseExact(fdaterep, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime tdaterep1 = DateTime.ParseExact(tdaterep, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            dr.Close();
            con.DisConnect();



            DateTime ToDateValidate = DateTime.ParseExact(System.DateTime.Now.AddDays(-7).ToString("dd MMM yyyy"), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (Todate < ToDateValidate && 1 == 2)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('From Date Could not be less than 3day back ');", true);
            }

            else
            {

                if (ddLeaveTypen.SelectedItem.Text.Trim() == "CO")
                {

                    string ss = "";
                    string strname = "";
                    foreach (GridViewRow gvrow in grdCOLeave.Rows)
                    {
                        CheckBox chk = (CheckBox)gvrow.FindControl("chkSelectCO");
                        if (chk != null & chk.Checked)
                        {
                            strname += ss + ',';
                            count++;
                        }
                    }
                    if (count > Convert.ToInt32(txtNoOfLeavePriod.Text) || count < Convert.ToInt32(txtNoOfLeavePriod.Text))
                    {

                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please select worked on week off / holiday equal from no of leave');", true);
                        // grdCOLeave.Enabled = false;
                    }
                    if (count == Convert.ToInt32(txtNoOfLeavePriod.Text))
                    {
                        if (lblerrorMessage.Text != "")
                        {
                            lblerrorMessage.Visible = true;
                        }
                        else
                        {
                            lblerrorMessage.Text = "";
                            lblerrorMessage.Visible = false;
                            LeaveApply();
                            ApplyCoLeave();

                            clear1();
                        }
                    }
                }
                else
                {
                    if (lblerrorMessage.Text != "")
                    {
                        lblerrorMessage.Visible = true;
                    }
                    else
                    {
                        lblerrorMessage.Text = "";
                        lblerrorMessage.Visible = false;
                        LeaveApply();

                        clear1();
                    }
                }
            }////
        }
        else
        {

            dr.Close();
            con.DisConnect();

            if (ddLeaveTypen.SelectedItem.Text.Trim() == "CO")
            {

                string ss = "";
                string strname = "";
                foreach (GridViewRow gvrow in grdCOLeave.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelectCO");
                    if (chk != null & chk.Checked)
                    {
                        strname += ss + ',';
                        count++;
                    }
                }
                if (count > Convert.ToInt32(txtNoOfLeavePriod.Text) || count < Convert.ToInt32(txtNoOfLeavePriod.Text))
                {

                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please select CO leave equal from no of leave');", true);
                    // grdCOLeave.Enabled = false;
                }
                if (count == Convert.ToInt32(txtNoOfLeavePriod.Text))
                {
                    LeaveApply();
                    ApplyCoLeave();
                    clear1();
                }
            }
            else
            {
                LeaveApply();
                clear1();
            }

        }////
    }

    string mailfrom = ""; string subject1 = ""; string Body1 = ""; string smtpfromportal = ""; int portNo1; string Pass_From = ""; string Mail_Sending_Option = ""; string Leave_Applymail = ""; string CCMail = "";


    public void ShowMailData(string mailTo1)
    {
        // mailTo1 = "ashutosh.kumar@corporateserve.com";//Test --Comment it for Live
        SqlDataReader dr = con.Show_tble_MailSetup(Session["Company"].ToString(), "Leave");
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
            Leave_Applymail = dr["Leave_Apply"].ToString();

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
        MailTo = "ashutosh.kumar@corporateserve.com";//Test --Comment it for Live
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

                msg.From = new MailAddress(Session["CompanyEmail"].ToString().Trim(), "  " + Session["Fulname"].ToString());  //LIVE OPEN

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




    byte[] bytesblank; string filenameblank = ""; string contentTypeblank = ""; string prelunchdata = ""; string postlunchdata = "";
    public void LeaveApply()
    {
        DateTime fromDate = DateTime.ParseExact(txtfromDate.Text, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string fDate = fromDate.ToString("yyyy-MM-dd");
        DateTime toDate = DateTime.ParseExact(txtTodate.Text, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string tDate = toDate.ToString("yyyy-MM-dd");

        //if (ddLeaveTypen.SelectedItem.Text.Trim() == "EL")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not apply EL from Portal !');", true);
        //    return;
        //}



        if (chkPrelunch.Checked == true)
        {
            prelunchdata = "Yes";
        }

        if (chkPrelunch.Checked == false)
        {
            prelunchdata = "No";
        }

        if (chkPostlunch.Checked == true)
        {
            postlunchdata = "Yes";
        }

        if (chkPostlunch.Checked == false)
        {
            postlunchdata = "No";
        }
        if (ddLeaveTypen.SelectedValue == "ML" && Convert.ToDecimal(txtNoOfLeavePriod.Text) < 2)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not apply ML less than 2 days.');", true);
            return;
        }
        if (Session["hr_email2"] == null) { Session["hr_email2"] = "ashutosh.kumar@corporateserve.comj"; return; }
        if (Session["hod_email2"] == null) { Session["hod_email2"] = "ashutosh.kumar@corporateserve.comj"; return; }
        if (Session["hod_Name2"] == null) { ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('You can't apply the Leave because Hod is not Tagged , Contact System Admin !');", true); return; }

        // if (!string.IsNullOrEmpty(Session["HRName"] as string))
        //if (Session["HRName"].ToString() == null)
        if (Session["HRID_leave"].ToString() == null)
        {
            lblHRAuthority.Visible = true;
            // ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('You can't apply the Leave because HR is not Tagged , Contact System Admin !');", true);
            return;
        }   //comment by ashu on 29-07-2016



        string Arrangement = "";
        if (chkArrangement.Checked == true)
        {
            Arrangement = "YES";
        }
        else
        {
            Arrangement = "NO";
        }
        //if (Blankapr == "1")
        //{

        //    if (lblerrorMessage.Text != "")
        //    {
        //        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Your selected leave type balance is low so you can apply another leave on same time');", true);

        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Insufficient leave balance');", true);
        //    }
        //    else
        //    {

        //        string hodaprid = ""; string hruid = "";
        //        //-------------------------------------------Upload Attachment-----Ashutosh----16-05-2016---START---
        //        byte[] LeaveAttachment = null;
        //        if (flUpload.HasFile && flUpload.PostedFile != null)
        //        {
        //            System.IO.Stream fs = flUpload.PostedFile.InputStream;
        //            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
        //            LeaveAttachment = br.ReadBytes((Int32)fs.Length); //svae 
        //            string filename = Path.GetFileName(flUpload.PostedFile.FileName);
        //            string extension = Path.GetExtension(filename);
        //            con.Insert_tble_Leave_Approval(txtfromDate.Text, txtTodate.Text, leavetypedrop, txtReason.Text, txtPhoneNo.Text, txtNoOfLeavePriod.Text, lblTotalBalance.Text, hodaprid, Session["Company"].ToString(), slno, System.DateTime.Now.ToString("dd/MM/yyyy"), hruid, Status, Session["Fulname"].ToString(), Session["uid"].ToString(), ddLeaveTypen.Text, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), LeaveAttachment, filename, extension, Arrangement, ddShiftType.SelectedValue.Trim(), ddShiftType.SelectedItem.Text.Trim());
        //        }
        //        //-------------------------------------------Upload Attachment-----Ashutosh----16-05-2016----END
        //        else
        //        {

        //            con.Insert_tble_Leave_Approval(txtfromDate.Text, txtTodate.Text, leavetypedrop, txtReason.Text, txtPhoneNo.Text, txtNoOfLeavePriod.Text, lblTotalBalance.Text, hodaprid, Session["Company"].ToString(), slno, System.DateTime.Now.ToString("dd/MM/yyyy"), hruid, Status, Session["Fulname"].ToString(), Session["uid"].ToString(), ddLeaveTypen.Text, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), Arrangement,ddShiftType.SelectedValue.Trim(),ddShiftType.SelectedItem.Text.Trim());
        //        }
        //        con.DisConnect();
        //        leaveblance();

        //        Portalcon.Update_Pay_Employee_Leave_EntitledLeave_BalanceApply(Convert.ToDouble(UnaprovedLeave), EmployeLeaveEntitled, Session["uid"].ToString(), ddLeaveTypen.Text);
        //        Portalcon.DisConnect();

        //        //if (EmailHR == "True")
        //        //{

        //        //    subject1 = "Application For Leave";

        //        //    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "This is to request you to kindly grant me a " + ddLeaveTypen.Text + " , " + ddLeavePeriod.Text + "  for " + txtNoOfLeavePriod.Text + " day/s i.e. " + txtfromDate.Text + " to " + txtTodate.Text + " . I need this leave for an urgent work which is unavailable, I will join my duties after leave.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

        //        //    ShowMailData(Session["hr_email2"].ToString());
        //        //}
        //        //if (EmailHOD == "True")
        //        //{
        //        //    subject1 = "Application For Leave";

        //        //    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "This is to request you to kindly grant me a " + ddLeaveTypen.Text + " , " + ddLeavePeriod.Text + "  for " + txtNoOfLeavePriod.Text + " day/s i.e. " + txtfromDate.Text + " to " + txtTodate.Text + " . I need this leave for an urgent work which is unavailable, I will join my duties after leave.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

        //        //    ShowMailData(Session["hod_email2"].ToString());
        //        //}
        //    }
        //}


        //if (PriorityHODapr == "1")
        //{
        if (lblerrorMessage.Text != "")
        {
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Your selected leave type balance is low so you can apply another leave on same time');", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Insufficient leave balance');", true);
        }
        else
        {
            string hruid = "";
            //if (Session["HRName"].ToString() == null || Session["hod_Name2"].ToString() == null)
            //{
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Contact Admin To Map HOD/HR !');", true);
            //    return;
            //}
            //-------------------------------------------Upload Attachment-----Ashutosh----16-05-2016---START---
            // byte[] LeaveAttachment = null;
            // Session["HRName"] = "";//ashu comment it
            if (flUpload.HasFile)
            {

                decimal size = Math.Round(((decimal)flUpload.PostedFile.ContentLength / (decimal)1024), 2);

                int fs = 0;
                fs = Convert.ToInt32(size);
                if (fs > 700)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'File Size Could Not be Greater than 700 KB !');", true);

                    return;
                }


                DataTable dt = new DataTable();
                SqlCommand sqlCmd = new SqlCommand("select [Sanctioning Incharge],HOD from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where  No_='" + Session["uid"].ToString() + "'", con.Con);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                sqlDa.Fill(dt);
                if (dt.Rows[0]["Sanctioning Incharge"].ToString() == "TMU00223" || dt.Rows[0]["HOD"].ToString() == "TMU00223")
                {
                    string sqlq = "insert into tble_Leave_Approval (From_Date,To_Date,Leave_Period,Reason,Address_Phone_No, No_Of_Days_Leave_Period,Total_Balance,HODUserid,HODUserid1,Company_Name,SerialNo,Create_Date,HR_Userid,Status,UName,UserID,Leave_Type,user_Emailid,HREmailid,HODEmailID,HRName,HODName,Arrangement,Half_Day_type_Code,Half_Day_type_Desc,PreLunch,PostLunch,FinalApprovalStatus,FinalApprovalId) values('" + fDate + "','" + tDate + "','" + leavetypedrop + "','" + txtReason.Text + "','" + txtPhoneNo.Text + "', '" + txtNoOfLeavePriod.Text + "','" + lblTotalBalance.Text + "','TMU00223','" + Session["HODLoginPage1"].ToString() + "','" + Session["Company"].ToString() + "','" + slno + "','" + System.DateTime.Now.ToString("dd/MM/yyyy") + "','" + hruid + "','Pending','" + Session["Fulname"].ToString() + "','" + Session["uid"].ToString() + "','" + ddLeaveTypen.Text + "','" + Session["CompanyEmail"].ToString() + "','" + Session["hr_email2"].ToString() + "','" + Session["hod_email2"].ToString() + "','" + Session["HRName"].ToString() + "','" + Session["hod_Name2"].ToString() + "','" + Arrangement + "','" + ddShiftType.SelectedValue.Trim() + "','" + ddShiftType.SelectedItem.Text.Trim() + "','" + prelunchdata + "','" + postlunchdata + "',0,'TMU00223' )";


                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand(sqlq, Conn);
                    cmd.ExecuteNonQuery();
                    Conn.Close();

                }
                else
                {
                    con.Insert_tble_Leave_Approval(fDate, tDate, leavetypedrop, txtReason.Text, txtPhoneNo.Text, txtNoOfLeavePriod.Text, lblTotalBalance.Text, Session["HODLoginPage"].ToString(), Session["HODLoginPage1"].ToString(), Session["Company"].ToString(), slno, System.DateTime.Now.ToString("dd/MM/yyyy"), hruid, Status, Session["Fulname"].ToString(), Session["uid"].ToString(), ddLeaveTypen.Text, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), bytesblank, filenameblank, contentTypeblank, Arrangement, ddShiftType.SelectedValue.Trim(), ddShiftType.SelectedItem.Text.Trim(), prelunchdata, postlunchdata);

                }
                UploadFileAttachmentss();
            }
            //-------------------------------------------Upload Attachment-----Ashutosh----16-05-2016----END
            else
            {

                //con.Insert_tble_Leave_Approval(txtfromDate.Text, txtTodate.Text, leavetypedrop, txtReason.Text, txtPhoneNo.Text, txtNoOfLeavePriod.Text, lblTotalBalance.Text, Session["HODLoginPage"].ToString(), Session["Company"].ToString(), slno, System.DateTime.Now.ToString("dd/MM/yyyy"), hruid, Status, Session["Fulname"].ToString(), Session["uid"].ToString(), ddLeaveTypen.Text, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(),Arrangement);

                // add  Session["HODLoginPage"].ToString()---ashu 02-08-2016
                //comment by ashu for dateformat dd MMM yyyy ---on 31-03-2017
                //con.Insert_tble_Leave_Approval(txtfromDate.Text, txtTodate.Text, leavetypedrop, txtReason.Text, txtPhoneNo.Text, txtNoOfLeavePriod.Text, lblTotalBalance.Text, Session["HODLoginPage"].ToString(), Session["HODLoginPage1"].ToString(), Session["Company"].ToString(), slno, System.DateTime.Now.ToString("dd/MM/yyyy"), hruid, Status, Session["Fulname"].ToString(), Session["uid"].ToString(), ddLeaveTypen.Text, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), Arrangement, ddShiftType.SelectedValue.Trim(), ddShiftType.SelectedItem.Text.Trim(),prelunchdata,postlunchdata);

                DataTable dt = new DataTable();
                SqlCommand sqlCmd = new SqlCommand("select [Sanctioning Incharge],HOD from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where  No_='" + Session["uid"].ToString() + "'", con.Con);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                sqlDa.Fill(dt);
                if (dt.Rows[0]["Sanctioning Incharge"].ToString() == "TMU00223" || dt.Rows[0]["HOD"].ToString() == "TMU00223")
                {
                    string sqlq = "insert into tble_Leave_Approval (From_Date,To_Date,Leave_Period,Reason,Address_Phone_No, No_Of_Days_Leave_Period,Total_Balance,HODUserid,HODUserid1,Company_Name,SerialNo,Create_Date,HR_Userid,Status,UName,UserID,Leave_Type,user_Emailid,HREmailid,HODEmailID,HRName,HODName,Arrangement,Half_Day_type_Code,Half_Day_type_Desc,PreLunch,PostLunch,FinalApprovalStatus,FinalApprovalId) values('" + fDate + "','" + tDate + "','" + leavetypedrop + "','" + txtReason.Text + "','" + txtPhoneNo.Text + "', '" + txtNoOfLeavePriod.Text + "','" + lblTotalBalance.Text + "','TMU00223','" + Session["HODLoginPage1"].ToString() + "','" + Session["Company"].ToString() + "','" + slno + "','" + System.DateTime.Now.ToString("dd/MM/yyyy") + "','" + hruid + "','Pending','" + Session["Fulname"].ToString() + "','" + Session["uid"].ToString() + "','" + ddLeaveTypen.Text + "','" + Session["CompanyEmail"].ToString() + "','" + Session["hr_email2"].ToString() + "','" + Session["hod_email2"].ToString() + "','" + Session["HRName"].ToString() + "','" + Session["hod_Name2"].ToString() + "','" + Arrangement + "','" + ddShiftType.SelectedValue.Trim() + "','" + ddShiftType.SelectedItem.Text.Trim() + "','" + prelunchdata + "','" + postlunchdata + "',0,'TMU00223' )";
                    if (Conn.State != ConnectionState.Open)
                    {
                        Conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand(sqlq, Conn);
                    cmd.ExecuteNonQuery();
                    Conn.Close();
                }
                else
                {
                    con.Insert_tble_Leave_Approval(fDate, tDate, leavetypedrop, txtReason.Text, txtPhoneNo.Text, txtNoOfLeavePriod.Text, lblTotalBalance.Text, Session["HODLoginPage"].ToString(), Session["HODLoginPage1"].ToString(), Session["Company"].ToString(), slno, System.DateTime.Now.ToString("dd/MM/yyyy"), hruid, Status, Session["Fulname"].ToString(), Session["uid"].ToString(), ddLeaveTypen.Text, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), Arrangement, ddShiftType.SelectedValue.Trim(), ddShiftType.SelectedItem.Text.Trim(), prelunchdata, postlunchdata);
                }
            }
            con.DisConnect();
            leaveblance();
            Portalcon.Update_Pay_Employee_Leave_EntitledLeave_BalanceApply(Convert.ToDouble(UnaprovedLeave), EmployeLeaveEntitled, Session["uid"].ToString(), ddLeaveTypen.Text);
            Portalcon.DisConnect();
            //if (EmailHR == "True")
            //{
            //    subject1 = "Application For Leave";

            //    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "This is to request you to kindly grant me a " + ddLeaveTypen.Text + " , " + ddLeavePeriod.Text + "  for " + txtNoOfLeavePriod.Text + " day/s i.e. " + txtfromDate.Text + " to " + txtTodate.Text + " . I need this leave for an urgent work which is unavailable, I will join my duties after leave.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

            //    ShowMailData(Session["hr_email2"].ToString());

            //}
            //subject1 = "Application For Leave";

            //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "This is to request you to kindly grant me a " + ddLeaveTypen.Text + " , " + ddLeavePeriod.Text + "  for " + txtNoOfLeavePriod.Text + " day/s i.e. " + txtfromDate.Text + " to " + txtTodate.Text + " . I need this leave for an urgent work which is unavailable, I will join my duties after leave.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

            //ShowMailData(Session["hod_email2"].ToString());

        }
        //}

        //if (PriorityHRapr == "1")
        //{
        //    if (lblerrorMessage.Text != "")
        //    {
        //        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Your selected leave type balance is low so you can apply another leave on same time');", true);
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Insufficient leave balance');", true);
        //    }
        //    else
        //    {
        //        string hodaprid = "";
        //        con.Insert_tble_Leave_Approval(txtfromDate.Text, txtTodate.Text, leavetypedrop, txtReason.Text, txtPhoneNo.Text, txtNoOfLeavePriod.Text, lblTotalBalance.Text, hodaprid, Session["Company"].ToString(), slno, System.DateTime.Now.ToString("dd/MM/yyyy"), Session["HRID"].ToString(), Status, Session["Fulname"].ToString(), Session["uid"].ToString(), ddLeaveTypen.Text, Session["CompanyEmail"].ToString(), Session["hr_email2"].ToString(), Session["hod_email2"].ToString(), Session["HRName"].ToString(), Session["hod_Name2"].ToString(), Arrangement, ddShiftType.SelectedValue.Trim(), ddShiftType.SelectedItem.Text.Trim());
        //        con.DisConnect();
        //        leaveblance();
        //        Portalcon.Update_Pay_Employee_Leave_EntitledLeave_BalanceApply(Convert.ToDouble(UnaprovedLeave), EmployeLeaveEntitled, Session["uid"].ToString(), ddLeaveTypen.Text);
        //        Portalcon.DisConnect();
        //        //if (EmailHOD == "True")
        //        //{
        //        //    subject1 = "Application For Leave";

        //        //    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "This is to request you to kindly grant me a " + ddLeaveTypen.Text + " , " + ddLeavePeriod.Text + "  for " + txtNoOfLeavePriod.Text + " day/s i.e. " + txtfromDate.Text + " to " + txtTodate.Text + " . I need this leave for an urgent work which is unavailable, I will join my duties after leave.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

        //        //    ShowMailData(Session["hod_email2"].ToString());

        //        //}

        //        //subject1 = "Application For Leave";

        //        //Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "This is to request you to kindly grant me a " + ddLeaveTypen.Text + " , " + ddLeavePeriod.Text + "  for " + txtNoOfLeavePriod.Text + " day/s i.e. " + txtfromDate.Text + " to " + txtTodate.Text + " . I need this leave for an urgent work which is unavailable, I will join my duties after leave.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

        //        //ShowMailData(Session["hr_email2"].ToString());


        //    }
        //}

        showLeaveBlance();
        show_LeaveDetailPending();
        //clear1();
    }
    protected void grdLeaveDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdLeaveDetail.PageIndex = e.NewPageIndex;
        show_LeaveDetailPending();

        pnlLeaveApplication.Visible = true;
        pnlMain.Visible = false;
        pnlViewleaveDetail.Visible = false;

        //clear1();
        //showLeaveBlance();
        //showLeaveType();
        //showoffLeavesetup();
        //show_LeaveDetailPending();
        ShowCoDetails();
    }
    public void sendMailfor_Cancellation(string from_Date, string To_date)
    {

        if (Blankapr == "1")
        {

            if (EmailHR == "True")
            {
                subject1 = "APPLICATION FOR THE CANCELLATION OF A LEAVE";

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I had scheduled to take my leave  from " + from_Date + " To " + To_date + " . But due to some personal reasons, I wish to cancel it Hope you would consider my request favourably.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hr_email2"].ToString());
            }

            if (EmailHOD == "True")
            {
                subject1 = "APPLICATION FOR THE CANCELLATION OF A LEAVE";

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I had scheduled to take my leave  from " + from_Date + " To " + To_date + " . But due to some personal reasons, I wish to cancel it Hope you would consider my request favourably.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hod_email2"].ToString());
            }
        }

        if (PriorityHODapr == "1")
        {

            try
            {
                if (EmailHR == "True")
                {
                    subject1 = "APPLICATION FOR THE CANCELLATION OF A LEAVE";

                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I had scheduled to take my leave  from " + from_Date + " To " + To_date + " . But due to some personal reasons, I wish to cancel it Hope you would consider my request favourably.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailData(Session["hr_email2"].ToString());

                }



                subject1 = "APPLICATION FOR THE CANCELLATION OF A LEAVE";

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I had scheduled to take my leave  from " + from_Date + " To " + To_date + " . But due to some personal reasons, I wish to cancel it Hope you would consider my request favourably.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hod_email2"].ToString());
            }
            catch (Exception)
            { }

        }

        if (PriorityHRapr == "1")
        {
            try
            {

                if (EmailHOD == "True")
                {
                    subject1 = "APPLICATION FOR THE CANCELLATION OF A LEAVE";

                    Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "" + Session["hod_Name2"].ToString() + "", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I had scheduled to take my leave  from " + from_Date + " To " + To_date + " . But due to some personal reasons, I wish to cancel it Hope you would consider my request favourably.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                    ShowMailData(Session["hod_email2"].ToString());

                }
                subject1 = "APPLICATION FOR THE CANCELLATION OF A LEAVE";

                Body1 = string.Format("To{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}", Environment.NewLine, "HR Manager", Environment.NewLine, Environment.NewLine, Environment.NewLine, "Dear Sir/Madam,", Environment.NewLine, Environment.NewLine, "I had scheduled to take my leave  from " + from_Date + " To " + To_date + " . But due to some personal reasons, I wish to cancel it Hope you would consider my request favourably.", Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, "Thanking you,", Environment.NewLine, Environment.NewLine, "Your's truly", Environment.NewLine, "" + Session["Fulname"].ToString() + "" + "(" + Session["uid"].ToString() + ")", Environment.NewLine, "Designation : " + Session["DesignationCode"].ToString() + "", Environment.NewLine, "Department :  " + Session["Departmentcode"].ToString() + "", Environment.NewLine, "Date : " + System.DateTime.Now.ToString());

                ShowMailData(Session["hr_email2"].ToString());
            }

            catch (Exception)
            { }

        }


    }

    protected void btndelete_Command(object sender, CommandEventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            string id = e.CommandArgument.ToString();

            string leave_type = ""; string No_Of_Days_Leave_Period = ""; string From_Date = ""; string To_Date = ""; string autoNo = "";
            SqlDataReader dr = con.Select_leavePendingDetail1(id);
            dr.Read();
            if (dr.HasRows)
            {
                leave_type = dr["Leave_Type"].ToString();
                No_Of_Days_Leave_Period = dr["No_Of_Days_Leave_Period"].ToString();
                From_Date = dr["From_Date"].ToString();
                To_Date = dr["To_Date"].ToString();
                autoNo = dr["AutoNo"].ToString();
                dr.Close();
                con.DisConnect();

                string ccname = Session["Company"].ToString();
                string rccname = ccname.Replace(".", "_");
                EmployeLeaveEntitled = "[" + rccname + "$Pay Employee Leave Entitled" + "]";
                SqlDataReader dr1 = Portalcon.ShowLeaveBalance_DetailwithOption(EmployeLeaveEntitled, Session["uid"].ToString(), leave_type);
                dr1.Read();
                if (dr1.HasRows)
                {

                    string lb = dr1["Unapproved Leave"].ToString();
                    double lb1 = Convert.ToDouble(lb);
                    double No_Of_Days_Leave_Period1 = Convert.ToDouble(No_Of_Days_Leave_Period);
                    double lbBanl = lb1 - No_Of_Days_Leave_Period1;
                    dr1.Close();
                    con.DisConnect();
                    Portalcon.Update_Pay_Employee_Leave_EntitledLeave_BalanceApply(lbBanl, EmployeLeaveEntitled, Session["uid"].ToString(), leave_type);
                    Portalcon.DisConnect();
                }
                dr1.Close();
                con.DisConnect();


            }
            dr.Close();
            con.DisConnect();
            con.Delete_leavePendingDetail(id);
            show_LeaveDetailPending();
            showLeaveBlance();
            con.DisConnect();
            sendMailfor_Cancellation(From_Date, To_Date);
            if (leave_type == "CO")
            {
                con.Delete_tbl_Co_Leave_Details(autoNo);
                con.DisConnect();
            }
            ShowCoDetails();
        }
    }
    protected void btnLeaveViewSearch_Click(object sender, EventArgs e)
    {
        show_LeaveStatus();
    }
    protected void ddLeavePeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNoOfLeavePriod.Text = "0";
        Show_NoOfDays();
        ShiftType_CL();
        if (ddLeavePeriod.SelectedValue == "(Full-Day)")
        {
            ddShiftType.Enabled = false;
            ddShiftType.SelectedValue = "0".ToString();
        }
        try
        {
            HidExcludeddayforcl();
        }
        catch (Exception)
        { }

        if (ddLeavePeriod.SelectedValue.Trim() == "(Full-Day)")
        {
            LeaveContinious();
        }


    }
    protected void txtfromDate_TextChanged(object sender, EventArgs e)
    {
        txtNoOfLeavePriod.Text = "0";
        if (txtTodate.Text == "")
        {
            txtTodate.Text = txtfromDate.Text;
        }
        Show_NoOfDays();
        NoofLeaveApplyinCo();
        try
        {
            HidExcludeddayforcl();
        }
        catch (Exception)
        { }
        HalfLeaveforPrepost();

        LeaveContinious();

    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        txtNoOfLeavePriod.Text = "0";
        Show_NoOfDays();
        NoofLeaveApplyinCo();
        try
        {
            HidExcludeddayforcl();
        }
        catch (Exception)
        { }


        HalfLeaveforPrepost();
        LeaveContinious();
    }

    public void ValidateClCombined(string finalFromdate, string finaltodate)
    {

        SqlDataAdapter da = new SqlDataAdapter("select From_Date,To_Date,Leave_Type from(select From_Date,To_Date,Leave_Type from tble_Leave_Approval where From_Date>='" + finalFromdate.Trim() + "' and From_Date<='" + finaltodate.Trim() + "' and UserId='" + Session["uid"].ToString().Trim() + "' and [Rejected Approval]='No' Union select From_Date,To_Date,Leave_Type from tble_Leave_Approval where To_Date>='" + finalFromdate.Trim() + "' and To_Date<='" + finaltodate.Trim() + "' and UserId='" + Session["uid"].ToString().Trim() + "' and [Rejected Approval]='No' union select From_Date,To_Date,Leave_Type from tble_Leave_Approval where '" + finalFromdate.Trim() + "'>=From_Date and '" + finaltodate.Trim() + "'<=To_Date and UserId='" + Session["uid"].ToString().Trim() + "' and [Rejected Approval]='No') as p", con.Con);
        DataSet ds = new DataSet();
        da.Fill(ds, "tble_Leave_Approval");
        for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
        {

            string From_Date = ""; string To_Date = ""; string Leave_Type = "";
            From_Date = ds.Tables[0].Rows[j]["From_Date"].ToString();
            To_Date = ds.Tables[0].Rows[j]["To_Date"].ToString();
            Leave_Type = ds.Tables[0].Rows[j]["Leave_Type"].ToString().Trim();
            if ((Leave_Type == "CO" && ddLeaveTypen.SelectedValue.Trim() == "CL") || (Leave_Type == "CL" && ddLeaveTypen.SelectedValue.Trim() == "CO"))
            {

            }
            else if (ddLeaveTypen.SelectedValue.Trim() != "CL")
            {
                if (Leave_Type == "CL")
                {
                    txtNoOfLeavePriod.Text = "0";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('CL can not combined with another leave except of CO');", true);
                }
                else
                {
                    //code execute
                }
            }
            else if (Leave_Type == ddLeaveTypen.SelectedValue.Trim())
            {

            }




                //if ((Leave_Type == "CO" && ddLeaveTypen.SelectedValue.Trim() == "CL") || (Leave_Type == "CO" && ddLeaveTypen.SelectedValue.Trim() == "CO") || (Leave_Type == "CL" && ddLeaveTypen.SelectedValue.Trim() == "CL") || (Leave_Type == "CO") || (Leave_Type == ddLeaveTypen.SelectedValue.Trim()))
            //{

                //}




               //else if ((Leave_Type != ddLeaveTypen.SelectedValue.Trim()) && (Leave_Type == "CO" && ddLeaveTypen.SelectedValue.Trim() == "CL") || (Leave_Type == "CO" && ddLeaveTypen.SelectedValue.Trim() == "CO") || (Leave_Type == "CL" && ddLeaveTypen.SelectedValue.Trim() == "CL") || (Leave_Type == "CO") || (Leave_Type == ddLeaveTypen.SelectedValue.Trim()))
            // { }
            //if (Leave_Type != ddLeaveTypen.SelectedValue.Trim())
            //{
            //    if ((Leave_Type == "CO" && ddLeaveTypen.SelectedValue.Trim() == "CL") || (Leave_Type == "CO" && ddLeaveTypen.SelectedValue.Trim() == "CO") || (Leave_Type == "CL" && ddLeaveTypen.SelectedValue.Trim() == "CL") || (Leave_Type == "CO") || (Leave_Type == ddLeaveTypen.SelectedValue.Trim()))
            //    { }
            //    else if (Leave_Type != "CL" || ddLeaveTypen.SelectedValue.Trim() == "CL")

                              //    { }
            //    else
            //    {
            //        txtNoOfLeavePriod.Text = "0";
            //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('CL can not combined with another leave except of CO');", true);
            //    }
            //}


                      //else if (Leave_Type == "CO" && ddLeaveTypen.SelectedValue.Trim() == "CO")
            //{}
            //else if (Leave_Type == "CL" && ddLeaveTypen.SelectedValue.Trim() == "CO")
            //{ }

                                   //else if (Leave_Type == "CL" && ddLeaveTypen.SelectedValue.Trim() == "CL")
            //{ }

                                   //else if (Leave_Type == "CO")
            //{ }

                                   //else if (Leave_Type == ddLeaveTypen.SelectedValue.Trim())
            //{ }

            else
            {
                txtNoOfLeavePriod.Text = "0";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('CL can not combined with another leave except of CO');", true);
            }

        }
    }
    string tblEmployeeActualPunchData = "";
    public void LeaveContinious()
    {
        try
        {
            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");
            tblPayHolidays = "[" + rccname + "$Pay Holidays" + "]";
            tblEmployeeActualPunchData = "[" + rccname + "$Employee Actual Punch Data" + "]";
            //if (ddLeaveTypen.SelectedValue == "CL")
            //{
            string finalFromdate = ""; string finaltodate = ""; string holidaydateFrom = ""; string holidaydateTo = "";
            DateTime fromdatedateformat = DateTime.ParseExact(txtfromDate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (txtTodate.Text.Trim() == "")
            {
                txtTodate.Text = txtfromDate.Text.Trim();
            }
            DateTime Todatedateformat = DateTime.ParseExact(txtTodate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string finalFromdate1 = fromdatedateformat.AddDays(-1).ToString("yyyy-MM-dd");
            string finaltodate1 = Todatedateformat.AddDays(1).ToString("yyyy-MM-dd");

            // Fixed Shift
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

                DateTime fromdtatefixed = DateTime.ParseExact(finalFromdate1.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime Todtatefixed = DateTime.ParseExact(finaltodate1.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                if (weekdaycode == fromdtatefixed.DayOfWeek.ToString())
                {
                    finalFromdate = fromdtatefixed.AddDays(-1).ToString("yyyy-MM-dd");
                }

                if (weekdaycode == Todtatefixed.DayOfWeek.ToString())
                {
                    finaltodate = Todtatefixed.AddDays(1).ToString("yyyy-MM-dd");
                }


                if (weekdaycode != fromdtatefixed.DayOfWeek.ToString())
                {
                    finalFromdate = fromdtatefixed.ToString("yyyy-MM-dd");
                }

                if (weekdaycode != Todtatefixed.DayOfWeek.ToString())
                {
                    finaltodate = Todtatefixed.ToString("yyyy-MM-dd");
                }
                if (Session["CompanyHolidayAllowed"].ToString() == "0")
                {

                    // From date holiday
                    SqlDataReader drHoliday = Portalcon.Show_HolidaydateFORCL(tblPayHolidays, finalFromdate, Session["GlobalDimension1Coded"].ToString(), weekdaycode);
                    drHoliday.Read();
                    if (drHoliday.HasRows)
                    {
                        holidaydateFrom = Convert.ToDateTime(drHoliday["CountDate"].ToString()).AddDays(-1).ToString("yyyy-MM-dd");
                        DateTime holidaydateFromloop = DateTime.ParseExact(holidaydateFrom.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                        if (weekdaycode == holidaydateFromloop.DayOfWeek.ToString())
                        {
                            finalFromdate = holidaydateFromloop.AddDays(-1).ToString("yyyy-MM-dd");
                        }
                        if (weekdaycode != holidaydateFromloop.DayOfWeek.ToString())
                        {
                            finalFromdate = holidaydateFrom;
                        }
                        drHoliday.Close();
                        Portalcon.DisConnect();
                    }
                    else
                    {
                        drHoliday.Close();
                        Portalcon.DisConnect();
                        finalFromdate = finalFromdate.ToString();
                    }
                    drHoliday.Close();
                    Portalcon.DisConnect();

                    // Todate  holiday
                    SqlDataReader drtoholiday = Portalcon.Show_HolidaydateFORCL(tblPayHolidays, finaltodate, Session["GlobalDimension1Coded"].ToString(), weekdaycode);
                    drtoholiday.Read();
                    if (drtoholiday.HasRows)
                    {
                        holidaydateTo = Convert.ToDateTime(drtoholiday["CountDate"].ToString()).AddDays(1).ToString("yyyy-MM-dd");
                        //finaltodate = holidaydateTo.ToString();
                        DateTime holidaydateToloop = DateTime.ParseExact(holidaydateTo.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                        if (weekdaycode == holidaydateToloop.DayOfWeek.ToString())
                        {
                            finalFromdate = holidaydateToloop.AddDays(1).ToString("yyyy-MM-dd");
                        }
                        if (weekdaycode != holidaydateToloop.DayOfWeek.ToString())
                        {
                            finalFromdate = holidaydateTo;
                        }

                        drtoholiday.Close();
                        Portalcon.DisConnect();
                    }
                    else
                    {
                        drtoholiday.Close();
                        Portalcon.DisConnect();
                        finaltodate = finaltodate.ToString();
                    }


                }

            }
            if (Session["ShiftPattern"].ToString() == "1" || Session["ShiftPattern"].ToString() == "2")
            {
                finalFromdate = finalFromdate1;
                finaltodate = finaltodate1;

            }

            // if (Session["ShiftPattern"].ToString() == "2")
            // {

            //   SqlDataReader drfmont = Portalcon.Show_MonthlyWeekdayorholiday(tblEmployeeActualPunchData, finalFromdate1, Session["uid"].ToString().Trim());
            // drfmont.Read();
            // if (drfmont.HasRows)
            //{
            //   drfmont.Close();
            //    DateTime holidweekday_From = DateTime.ParseExact(finalFromdate1.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            //    finalFromdate = holidweekday_From.AddDays(-2).ToString("yyyy-MM-dd");
            //   }
            //  else
            // {
            //    drfmont.Close();
            //    finalFromdate = finalFromdate1;
            // }


            //  SqlDataReader drtmont = Portalcon.Show_MonthlyWeekdayorholiday(tblEmployeeActualPunchData, finaltodate1, Session["uid"].ToString().Trim());
            // drtmont.Read();
            // if (drtmont.HasRows)
            //  {
            //     drtmont.Close();
            //      DateTime holidweekday_To = DateTime.ParseExact(finaltodate1.Trim(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            //     finaltodate = holidweekday_To.AddDays(-2).ToString("yyyy-MM-dd");
            // }
            // else
            // {
            //     drtmont.Close();
            //     finaltodate = finaltodate1;
            //  }



            // }

            ValidateClCombined(finalFromdate, finaltodate);

            // }
        }
        catch (Exception)
        { }
    }


    public void HalfLeaveforPrepost()
    {
        if (ddLeaveTypen.SelectedValue == "CL")
        {
            chkPrelunch.Visible = false;
            chkPostlunch.Visible = false;
            chkPrelunch.Checked = false;
            chkPostlunch.Checked = false;
        }
        else
        {

            decimal halno1 = Convert.ToDecimal(txtNoOfLeavePriod.Text.Trim());
            if (halno1 > 1)
            {
                chkPrelunch.Visible = true;
                chkPostlunch.Visible = true;
                chkPrelunch.Checked = false;
                chkPostlunch.Checked = false;
                ddLeavePeriod.SelectedValue = "(Full-Day)";
                ddLeavePeriod.Enabled = false;
                ddShiftType.Enabled = false;
                lblPeriodText.Visible = false;
                ddLeavePeriod.Visible = false;
                ddShiftType.Visible = false;
                lblDayModeText.Visible = false;
                if (chkPrelunch.Checked == true && chkPostlunch.Checked == false || chkPrelunch.Checked == false && chkPostlunch.Checked == true)
                {
                    decimal pint5 = Convert.ToDecimal("0.5");
                    txtNoOfLeavePriod.Text = (halno1 - pint5).ToString();
                }

                if (chkPrelunch.Checked == true && chkPostlunch.Checked == true)
                {
                    decimal pint5 = Convert.ToDecimal("1.0");
                    txtNoOfLeavePriod.Text = (halno1 - pint5).ToString();
                }
            }
            if (halno1 <= 1)
            {
                chkPrelunch.Visible = false;
                chkPostlunch.Visible = false;
                chkPrelunch.Checked = false;
                chkPostlunch.Checked = false;
                ddLeavePeriod.SelectedValue = "(Full-Day)";
                ddLeavePeriod.Enabled = true;
                ddShiftType.Enabled = true;


                lblPeriodText.Visible = true;
                ddLeavePeriod.Visible = true;
                ddShiftType.Visible = true;
                lblDayModeText.Visible = true;
            }
            showdiablee();
        }
    }

    public void showdiablee()
    {
        if (txtTodate.Text.Trim() == "")
        {
            showpreposthalfdiable();
        }

        // if (Convert.ToDecimal(txtNoOfLeavePriod.Text) >= 3 && ((ddLeaveTypen.Text == "ML")|| (ddLeaveTypen.Text == "AL")) )
        if (((Convert.ToDecimal(txtNoOfLeavePriod.Text) > 4 && (ddLeaveTypen.Text == "ML")) || (Convert.ToDecimal(txtNoOfLeavePriod.Text) >= 1 && (ddLeaveTypen.Text == "AL"))))
        {
            flUpload.Visible = true;
            rfvflUpload.Enabled = true;
        }
        else
        {
            flUpload.Visible = false;
            rfvflUpload.Enabled = false;
        }
    }
    public void showpreposthalfdiable()
    {


        chkPrelunch.Visible = false;
        chkPostlunch.Visible = false;
        chkPrelunch.Checked = false;
        chkPostlunch.Checked = false;
        ddLeavePeriod.SelectedValue = "(Full-Day)";

        ddLeavePeriod.Enabled = true;

        ddShiftType.Enabled = true;

        lblPeriodText.Visible = true;
        ddLeavePeriod.Visible = true;
        ddShiftType.Visible = true;
        lblDayModeText.Visible = true;
    }
    string leavetypedrop = "";
    public void showLeaveBlanceOption()
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tble_Pay_Employee_Leave_Entitled = "[" + rccname + "$Pay Employee Leave Entitled" + "]";

        SqlDataReader dr = Portalcon.ShowLeaveBalance_DetailwithOption(tble_Pay_Employee_Leave_Entitled, Session["uid"].ToString(), ddLeaveTypen.Text);
        dr.Read();
        if (dr.HasRows)
        {
            string lb = dr["Leave Balance"].ToString();
            lblUnapprovedLeave.Text = dr["Unapproved Leave"].ToString();

            dr.Close();
            con.DisConnect();
            double UnAP1 = Convert.ToDouble(lblUnapprovedLeave.Text);
            double lbc = Convert.ToDouble(lb);
            double lbc1 = Convert.ToDouble(lbc - UnAP1);
            if (ddLeaveTypen.Text.Trim() == "CL")
            {
                //if (lbc1 < 0.5)
                //{
                //    lblLeaveBalance_CL.Text = "00.00";
                //}
                //if (lbc1 >= 0.5)
                //{
                //    lblLeaveBalance_CL.Text = lbc1.ToString();
                //}

                lblLeaveBalance_CL.Text = lbc1.ToString();
                Valadation_CL();
                pnlCLApplyLeave.Visible = true;
            }
            if (ddLeaveTypen.Text != "CL")
            {
                pnlCLApplyLeave.Visible = false;
                lblLeaveBalance_CL.Text = "0";
            }
            //double lb1 = Convert.ToDouble(lb);
            double lb1 = Convert.ToDouble(lb);
            if (txtNoOfLeavePriod.Text == "0")
            {


            }
            else
            {
                double NoOfLeavePriod1 = Convert.ToDouble(txtNoOfLeavePriod.Text);

                //if (NoOfLeavePriod1 > lb1)//comment by ashu 14-07-2016
                if (NoOfLeavePriod1 > lbc1)
                {
                    leavetypedrop = ddLeavePeriod.Text;
                    lblerror.Text = lb1.ToString("00.00");
                    lblerrorMessage.Visible = true;
                    lblerror.Visible = false;
                    //lblerrorMessage.Text = "Your selected leave type balance is low so you can apply another leave on same time";
                    lblerrorMessage.Text = "Insufficient leave balance";
                }
                // if (NoOfLeavePriod1 <= lb1) //comment by ashu 14-07-2016
                if (NoOfLeavePriod1 <= lbc1)
                {
                    leavetypedrop = ddLeavePeriod.Text;
                    lblerror.Text = lb1.ToString("00.00");
                    lblerrorMessage.Visible = false;
                    lblerror.Visible = false;
                    lblerrorMessage.Text = "";
                }
            }
        }
        else
        {
            pnlCLApplyLeave.Visible = false;
            dr.Close();
            con.DisConnect();
            if (ddLeaveTypen.Text == "LWP" || ddLeaveTypen.Text == "Special Leave")
            {
                leavetypedrop = ddLeavePeriod.Text;
                lblerrorMessage.Text = "";
                lblerrorMessage.Visible = false;
                lblerror.Visible = false;
                lblerror.Text = "00.00";
            }
        }

    }
    string UnaprovedLeave = "";
    public void leaveblance()
    {
        if (lblerror.Text.Trim() == "")
        {
            lblerror.Text = "0";
        }
        if (txtNoOfLeavePriod.Text.Trim() == "")
        {
            txtNoOfLeavePriod.Text = "0";
        }
        if (lblUnapprovedLeave.Text.Trim() == "")
        {
            lblUnapprovedLeave.Text = "0";
        }
        double lbba = Convert.ToDouble(lblerror.Text);
        double noofday = Convert.ToDouble(txtNoOfLeavePriod.Text);
        double mbalan = lbba - noofday;

        lblabalance.Text = mbalan.ToString();

        double UnapBal = Convert.ToDouble(lblUnapprovedLeave.Text);
        double UnaprovedLeave1 = noofday + UnapBal;

        UnaprovedLeave = UnaprovedLeave1.ToString();

    }

    public void ShiftType_CL()
    {
        if ((Session["EmployeePostingGroupl"].ToString() == "TEACH" || Session["EmployeePostingGroupl"].ToString() == "TUTAR") && Session["GlobalDimension1Coded"].ToString() != "TMMC")
        {

            if (ddLeaveTypen.SelectedValue == "CL" && ddLeavePeriod.SelectedValue == "(Half-Day)")
            {
                ddShiftType.Enabled = false;
                ddShiftType.SelectedValue = "2".ToString();

            }
            if (ddLeaveTypen.SelectedValue == "CL" && ddLeavePeriod.SelectedValue != "(Half-Day)")
            {
                ddShiftType.Enabled = false;
                ddShiftType.SelectedValue = "0".ToString();
            }


        }
        else // (Session["EmployeePostingGroupl"].ToString() != "TEACH" )
        {
            if (ddLeaveTypen.SelectedValue == "CL" && ddLeavePeriod.SelectedValue == "(Full-Day)")
            {

                // ddLeavePeriod.Enabled = false;
                //  ddLeavePeriod.SelectedValue = "(Full-Day)";

                ddShiftType.Enabled = false;
                ddShiftType.SelectedValue = "0".ToString();
            }
            else
            {
                ddShiftType.Enabled = true;
                //ddShiftType.SelectedValue = "0".ToString();
                // ddLeavePeriod.Enabled = true;
            }
        }
    }



    public void Getbussinessday(DateTime startD, DateTime endD, string weekday)
    {


        //double calcBusinessDays =
        //        1 + ((endD - startD).TotalDays * 6 -
        //        (startD.DayOfWeek - endD.DayOfWeek) * 1) / 7;

        if (weekday == "Sunday")
        {

            string totalnoofdays = (endD - startD).TotalDays.ToString();
            decimal totalnoofdaysplus = Convert.ToDecimal(totalnoofdays);
            totalnoofdays = (totalnoofdaysplus + 1).ToString();

            int totalweeek = 0;

            for (DateTime dt = startD; dt <= endD; dt = dt.AddDays(1.0))
            {
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    totalweeek++;
                }
            }

            string totalweeekdays = totalweeek.ToString();
            decimal totalnoofdaysdays = Convert.ToDecimal(totalnoofdays);
            decimal totalweeekdaysdec = Convert.ToDecimal(totalweeekdays);
            countweekeadcl = (totalnoofdaysdays - totalweeekdaysdec).ToString();


            //if (endD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

            //   countweekeadcl = calcBusinessDays.ToString();

        }
        if (weekday == "Monday")
        {
            string totalnoofdays = (endD - startD).TotalDays.ToString();
            decimal totalnoofdaysplus = Convert.ToDecimal(totalnoofdays);
            totalnoofdays = (totalnoofdaysplus + 1).ToString();

            int totalweeek = 0;

            for (DateTime dt = startD; dt <= endD; dt = dt.AddDays(1.0))
            {
                if (dt.DayOfWeek == DayOfWeek.Monday)
                {
                    totalweeek++;
                }
            }

            string totalweeekdays = totalweeek.ToString();
            decimal totalnoofdaysdays = Convert.ToDecimal(totalnoofdays);
            decimal totalweeekdaysdec = Convert.ToDecimal(totalweeekdays);
            countweekeadcl = (totalnoofdaysdays - totalweeekdaysdec).ToString();


            //if (endD.DayOfWeek == DayOfWeek.Monday) calcBusinessDays--;

            //countweekeadcl = calcBusinessDays.ToString();

        }
        if (weekday == "Tuesday")
        {

            string totalnoofdays = (endD - startD).TotalDays.ToString();
            decimal totalnoofdaysplus = Convert.ToDecimal(totalnoofdays);
            totalnoofdays = (totalnoofdaysplus + 1).ToString();

            int totalweeek = 0;

            for (DateTime dt = startD; dt <= endD; dt = dt.AddDays(1.0))
            {
                if (dt.DayOfWeek == DayOfWeek.Tuesday)
                {
                    totalweeek++;
                }
            }

            string totalweeekdays = totalweeek.ToString();
            decimal totalnoofdaysdays = Convert.ToDecimal(totalnoofdays);
            decimal totalweeekdaysdec = Convert.ToDecimal(totalweeekdays);
            countweekeadcl = (totalnoofdaysdays - totalweeekdaysdec).ToString();



        }

        if (weekday == "Wednesday")
        {

            string totalnoofdays = (endD - startD).TotalDays.ToString();
            decimal totalnoofdaysplus = Convert.ToDecimal(totalnoofdays);
            totalnoofdays = (totalnoofdaysplus + 1).ToString();

            int totalweeek = 0;

            for (DateTime dt = startD; dt <= endD; dt = dt.AddDays(1.0))
            {
                if (dt.DayOfWeek == DayOfWeek.Wednesday)
                {
                    totalweeek++;
                }
            }

            string totalweeekdays = totalweeek.ToString();
            decimal totalnoofdaysdays = Convert.ToDecimal(totalnoofdays);
            decimal totalweeekdaysdec = Convert.ToDecimal(totalweeekdays);
            countweekeadcl = (totalnoofdaysdays - totalweeekdaysdec).ToString();


            //if (endD.DayOfWeek == DayOfWeek.Wednesday) calcBusinessDays--;

            //countweekeadcl = calcBusinessDays.ToString();

        }

        if (weekday == "Thursday")
        {

            string totalnoofdays = (endD - startD).TotalDays.ToString();
            decimal totalnoofdaysplus = Convert.ToDecimal(totalnoofdays);
            totalnoofdays = (totalnoofdaysplus + 1).ToString();

            int totalweeek = 0;

            for (DateTime dt = startD; dt <= endD; dt = dt.AddDays(1.0))
            {
                if (dt.DayOfWeek == DayOfWeek.Thursday)
                {
                    totalweeek++;
                }
            }

            string totalweeekdays = totalweeek.ToString();
            decimal totalnoofdaysdays = Convert.ToDecimal(totalnoofdays);
            decimal totalweeekdaysdec = Convert.ToDecimal(totalweeekdays);
            countweekeadcl = (totalnoofdaysdays - totalweeekdaysdec).ToString();


            //if (endD.DayOfWeek == DayOfWeek.Thursday) calcBusinessDays--;

            //countweekeadcl = calcBusinessDays.ToString();

        }

        if (weekday == "Friday")
        {

            //if (endD.DayOfWeek == DayOfWeek.Friday) calcBusinessDays--;

            //countweekeadcl = calcBusinessDays.ToString();
            string totalnoofdays = (endD - startD).TotalDays.ToString();
            decimal totalnoofdaysplus = Convert.ToDecimal(totalnoofdays);
            totalnoofdays = (totalnoofdaysplus + 1).ToString();

            int totalweeek = 0;

            for (DateTime dt = startD; dt <= endD; dt = dt.AddDays(1.0))
            {
                if (dt.DayOfWeek == DayOfWeek.Friday)
                {
                    totalweeek++;
                }
            }

            string totalweeekdays = totalweeek.ToString();
            decimal totalnoofdaysdays = Convert.ToDecimal(totalnoofdays);
            decimal totalweeekdaysdec = Convert.ToDecimal(totalweeekdays);
            countweekeadcl = (totalnoofdaysdays - totalweeekdaysdec).ToString();

        }
        if (weekday == "Saturday")
        {

            //if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;

            //countweekeadcl = calcBusinessDays.ToString();
            string totalnoofdays = (endD - startD).TotalDays.ToString();
            decimal totalnoofdaysplus = Convert.ToDecimal(totalnoofdays);
            totalnoofdays = (totalnoofdaysplus + 1).ToString();

            int totalweeek = 0;

            for (DateTime dt = startD; dt <= endD; dt = dt.AddDays(1.0))
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    totalweeek++;
                }
            }

            string totalweeekdays = totalweeek.ToString();
            decimal totalnoofdaysdays = Convert.ToDecimal(totalnoofdays);
            decimal totalweeekdaysdec = Convert.ToDecimal(totalweeekdays);
            countweekeadcl = (totalnoofdaysdays - totalweeekdaysdec).ToString();

        }

    }
    string weekofMonthlydataCount = ""; string weekofMonthlydataDate = ""; string holidayMonthlydataCount = "";
    string countholidayyy = ""; string tblPayHolidays = ""; string countweekeadcl = ""; string weekdaycode = ""; string tblEmployeeWeekShiftMaster = "";
    public void HidExcludeddayforcl()
    {
        lblCLErrorRemarks.Visible = false;
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        tblPayHolidays = "[" + rccname + "$Pay Holidays" + "]";
        tblEmployeeWeekShiftMaster = "[" + rccname + "$Employee Week Shift Master" + "]";
        if (ddLeaveTypen.SelectedValue == "CL")
        {
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


                if (txtTodate.Text == "")
                {
                    txtTodate.Text = txtfromDate.Text;
                }

                Getbussinessday(Convert.ToDateTime(txtfromDate.Text.Trim()), Convert.ToDateTime(txtTodate.Text.Trim()), weekdaycode);


                decimal countweekeadcldec = Convert.ToDecimal(countweekeadcl.ToString());


                if (Session["CompanyHolidayAllowed"].ToString() == "0")
                {

                    SqlDataReader dr = Portalcon.Show_HolidayListPayHolidaysss(tblPayHolidays, txtfromDate.Text.Trim(), txtTodate.Text.Trim(), Session["GlobalDimension1Coded"].ToString(), weekdaycode);
                    dr.Read();
                    countholidayyy = dr["CountDate"].ToString();
                    dr.Close();
                    Portalcon.DisConnect();
                }


                decimal countholidayyydec = Convert.ToDecimal(countholidayyy);

                txtNoOfLeavePriod.Text = (countweekeadcldec - countholidayyydec).ToString();
                decimal txtNoOfLeavePrioddec = Convert.ToDecimal(txtNoOfLeavePriod.Text.Trim());

                if (ddLeavePeriod.SelectedValue == "(Half-Day)")
                {
                    txtNoOfLeavePrioddec = txtNoOfLeavePrioddec / 2;
                    txtNoOfLeavePriod.Text = txtNoOfLeavePrioddec.ToString();
                }

                if (txtNoOfLeavePrioddec > 3)
                {
                    lblCLErrorRemarks.Visible = true;
                    lblCLErrorRemarks.Text = "You can not apply more than three CL leave";
                    txtTodate.Text = "";
                    txtNoOfLeavePriod.Text = "0";
                }
                else
                {
                    lblCLErrorRemarks.Visible = false;
                }
            }

            if (Session["ShiftPattern"].ToString() == "2")
            {
                SqlDataReader drs = Portalcon.Show_CountMonthlyWeekoffemployee(tblEmployeeWeekShiftMaster, txtfromDate.Text.Trim(), txtTodate.Text.Trim(), "2", "0", Session["uid"].ToString());
                drs.Read();
                weekofMonthlydataCount = drs["ShiftCode"].ToString();
                drs.Close();
                Portalcon.DisConnect();

                if (Session["CompanyHolidayAllowed"].ToString() == "0")
                {
                    SqlDataAdapter da = new SqlDataAdapter("select *  from " + tblEmployeeWeekShiftMaster + " where convert(date, [Start Date],103)  >='" + txtfromDate.Text.Trim() + "' and  convert(date, [Start Date],103)  <='" + txtTodate.Text.Trim() + "' and [Employee No_]='" + Session["uid"].ToString() + "' and [Shift Pattern]=2 and [Shift Code]=0", Portalcon.Con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "tbl_EmployeepunchData");
                    for (int j = 0; j <= ds.Tables[0].Rows.Count - 1; j++)
                    {
                        weekofMonthlydataDate = ds.Tables[0].Rows[j]["Start Date"].ToString();
                    }

                    SqlDataReader dr = Portalcon.Show_HolidayListPayHolidayMonthly(tblPayHolidays, txtfromDate.Text.Trim(), txtTodate.Text.Trim(), Session["GlobalDimension1Coded"].ToString(), weekofMonthlydataDate);
                    dr.Read();

                    holidayMonthlydataCount = dr["CountDate"].ToString();
                    dr.Read();
                    Portalcon.DisConnect();
                    string totalnoofdays = (Convert.ToDateTime(txtTodate.Text.Trim()) - Convert.ToDateTime(txtfromDate.Text.Trim())).TotalDays.ToString();
                    decimal totalnoofdaysplus = Convert.ToDecimal(totalnoofdays);
                    totalnoofdays = (totalnoofdaysplus + 1).ToString();


                    decimal totalnoofdaysdec = Convert.ToDecimal(totalnoofdays);
                    decimal weekofMonthlydataDatedec = Convert.ToDecimal(weekofMonthlydataCount);
                    decimal holidayMonthlydataCountdec = Convert.ToDecimal(holidayMonthlydataCount);
                    decimal holidyweekdayss = weekofMonthlydataDatedec + holidayMonthlydataCountdec;

                    txtNoOfLeavePriod.Text = (totalnoofdaysdec - holidyweekdayss).ToString();
                    decimal txtNoOfLeavePrioddec = Convert.ToDecimal(txtNoOfLeavePriod.Text.Trim());

                    if (ddLeavePeriod.SelectedValue == "(Half-Day)")
                    {
                        txtNoOfLeavePrioddec = txtNoOfLeavePrioddec / 2;
                        txtNoOfLeavePriod.Text = txtNoOfLeavePrioddec.ToString();
                    }

                    if (txtNoOfLeavePrioddec > 3)
                    {
                        lblCLErrorRemarks.Visible = true;
                        lblCLErrorRemarks.Text = "You can not apply more than three CL leave";
                        txtTodate.Text = "";
                        txtNoOfLeavePriod.Text = "0";
                    }
                    else
                    {
                        lblCLErrorRemarks.Visible = false;
                    }


                }

            }

            //
            //if (Session["ShiftPattern"].ToString() == "1")
            //{


            //}
        }
        else
        {



            if (ddLeaveTypen.SelectedValue == "EL")
            {

                decimal txtNoOfLeavePrioddec = Convert.ToDecimal(txtNoOfLeavePriod.Text.Trim());

                if (txtNoOfLeavePrioddec < 3)
                {
                    lblCLErrorRemarks.Visible = true;
                    lblCLErrorRemarks.Text = "You can not apply less than three EL leave";
                    txtTodate.Text = "";
                    txtNoOfLeavePriod.Text = "0";
                }
                else
                {
                    lblCLErrorRemarks.Visible = false;
                }
            }
            else
            {
                lblCLErrorRemarks.Visible = false;

            }
        }
        if (ddLeavePeriod.SelectedValue == "(Full-Day)")
        {
            ddShiftType.Enabled = false;
            ddShiftType.SelectedValue = "0".ToString();
        }
    }
    protected void ddLeaveTypen_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddLeaveTypen.SelectedValue.Trim() == "CO")
        {
            grdCOLeave.Visible = true;
            ShowCoDetails();
        }
        else
        {
            grdCOLeave.Visible = false;
        }
        txtNoOfLeavePriod.Text = "0";
        lblerror.Text = "";
        showLeaveBlanceOption();
        Show_NoOfDays();
        NoofLeaveApplyinCo();
        ShiftType_CL();

        try
        {
            HidExcludeddayforcl();
        }
        catch (Exception)
        { }
        showdiablee();
        if (ddLeavePeriod.SelectedValue == "(Full-Day)")
        {
            ddShiftType.Enabled = false;
            ddShiftType.SelectedValue = "0".ToString();
        }

        LeaveContinious();
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
        string Pay_Daily_Attendence_Detail_tble = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
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

    int expiryatt; string EmployeeIDday = ""; int EmployeeWiseNo_Of_Daysint;
    public void showAttendenceExpiry()
    {
        SqlDataReader dr = con.Show_tbl_Attendence_Expiry(Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            string attenexpprev1 = dr["No_of_Days"].ToString();
            expiryatt = Convert.ToInt32(attenexpprev1);
            string EmployeeWiseNo_Of_Days = dr["EmployeeWiseNo_Of_Days"].ToString();
            if (EmployeeWiseNo_Of_Days == "")
            {
                EmployeeWiseNo_Of_Days = "0";
            }
            EmployeeWiseNo_Of_Daysint = Convert.ToInt32(EmployeeWiseNo_Of_Days);
            EmployeeIDday = dr["EmployeeID"].ToString();
        }
        dr.Read();
        con.DisConnect();
    }
    string optForFromTimeTotimeind = "";
    public void showAttendenceExpiryIND()
    {
        SqlDataReader dr = con.Show_tbl_Attendence_ExpiryIND(Session["Company"].ToString(), Session["uid"].ToString());
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


            optForFromTimeTotimeind = dr["Option_fromTime_toime_Individual"].ToString();

        }

        dr.Read();
        con.DisConnect();
    }

    protected void grdViewLeaveStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblleaveAttachmentFilename = (Label)e.Row.FindControl("lblleaveAttachmentFilenamView");
            LinkButton lnkDownloadgrid = (LinkButton)e.Row.FindControl("lnkDownloadgridView");
            if (lblleaveAttachmentFilename.Text.Trim() == "")
            {
                lnkDownloadgrid.Visible = false;
            }

            if (lblleaveAttachmentFilename.Text.Trim() != "")
            {
                lnkDownloadgrid.Visible = true;
            }

            Button btnleavecancApproved1 = (Button)e.Row.FindControl("btnleavecancApproved");
            Button btnWorkingdetailsview = (Button)e.Row.FindControl("btnWorkingdetailsview");
            Label lblrejectedappr1 = (Label)e.Row.FindControl("lblrejectedappr");
            Label lblLeavetype = (Label)e.Row.FindControl("lblleavetypeG");

            Label lblTodate = (Label)e.Row.FindControl("lblTodate");
            RadioButton rdSelect1 = (RadioButton)e.Row.FindControl("rdSelect");
            if (lblrejectedappr1.Text == "No")
            {

                string sd = lblTodate.Text.Trim();



                string sde11 = System.DateTime.Now.ToString("yyyy-MM-dd").Trim();

                DateTime startDateTime1 = DateTime.ParseExact(sd, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime endDateTime1 = DateTime.ParseExact(sde11, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);


                if (startDateTime1 < endDateTime1)
                {
                    btnleavecancApproved1.Visible = false;
                    rdSelect1.Visible = false;

                }
                else
                {
                    //btnleavecancApproved1.Visible = true;
                    //rdSelect1.Visible = true;
                    btnleavecancApproved1.Visible = false;
                    rdSelect1.Visible = false;


                }
            }
            if (lblrejectedappr1.Text == "Yes")
            {
                btnleavecancApproved1.Visible = false;
                rdSelect1.Visible = false;
            }
            if (lblLeavetype.Text == "CO")
            {
                if (lblrejectedappr1.Text != "Yes")
                {
                    btnWorkingdetailsview.Visible = true;
                }
                if (lblrejectedappr1.Text == "Yes")
                {
                    btnWorkingdetailsview.Visible = false;
                }
            }
            if (lblLeavetype.Text != "CO")
            {

                btnWorkingdetailsview.Visible = false;
            }

        }

    }

    string tblenameAttendence1 = ""; double lbBanl;
    protected void btnleavecancApproved_Command(object sender, CommandEventArgs e)
    {


        Button btnEdit = (Button)sender;
        GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
        RadioButton rb1 = (RadioButton)Grow.FindControl("rdSelect");
        Label lblleavetypeG1 = (Label)Grow.FindControl("lblleavetypeG");



        if (rb1.Checked == true)
        {
            showoffLeavesetup(lblleavetypeG1.Text);
            string ccname1 = Session["Company"].ToString();
            string rccname1 = ccname1.Replace(".", "_");
            tblenameAttendence1 = "[" + rccname1 + "$Pay Daily Attendence Detail" + "]";
            string id = e.CommandArgument.ToString();
            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");
            EmployeLeaveEntitled = "[" + rccname + "$Pay Employee Leave Entitled" + "]";
            string leave_type = ""; string No_Of_Days_Leave_Period = ""; string From_Date = ""; string To_Date = ""; string AutoNoh = "";
            string timein = ""; string timeout = ""; string status = ""; string hourpresent = "00.00"; string chkUSerID = ""; string Leave_Period = ""; string Leave_Type = ""; string Statusre = "";
            SqlDataReader dr = con.Select_leavePendingDetail1(id);
            dr.Read();
            if (dr.HasRows)
            {
                leave_type = dr["Leave_Type"].ToString();
                No_Of_Days_Leave_Period = dr["No_Of_Days_Leave_Period"].ToString();
                From_Date = dr["From_Date"].ToString();
                To_Date = dr["To_Date"].ToString();
                chkUSerID = dr["UserID"].ToString();
                Statusre = dr["Status"].ToString();
                AutoNoh = dr["AutoNo"].ToString();
                //dr.Close();
                //con.DisConnect();

            }
            dr.Close();
            con.DisConnect();
            SqlDataReader dr1 = Portalcon.ShowLeaveBalance_DetailwithOption(EmployeLeaveEntitled, Session["uid"].ToString(), leave_type);
            dr1.Read();
            if (dr1.HasRows)
            {
                if (Statusre != "Approved")
                {
                    string lb = dr1["Unapproved Leave"].ToString();
                    double lb1 = Convert.ToDouble(lb);
                    double No_Of_Days_Leave_Period1 = Convert.ToDouble(No_Of_Days_Leave_Period);
                    lbBanl = lb1 - No_Of_Days_Leave_Period1;
                    dr1.Close();
                    con.DisConnect();
                    Portalcon.Update_Pay_Employee_Leave_EntitledLeave_BalanceApply(lbBanl, EmployeLeaveEntitled, Session["uid"].ToString(), leave_type);
                    Portalcon.DisConnect();
                }
                else
                {



                    string lb = dr1["Leave Balance"].ToString();
                    double lb1 = Convert.ToDouble(lb);
                    double No_Of_Days_Leave_Period1 = Convert.ToDouble(No_Of_Days_Leave_Period);
                    string lbb = lb1.ToString("00.00");
                    if (lbb == "00.00")
                    {
                        lbBanl = No_Of_Days_Leave_Period1;
                    }
                    else
                    {
                        lbBanl = lb1 + No_Of_Days_Leave_Period1;
                    }
                    dr1.Close();
                    con.DisConnect();
                    Portalcon.Update_Pay_Employee_Leave_EntitledLeave_BalancecancelafterApproval(lbBanl, EmployeLeaveEntitled, Session["uid"].ToString(), leave_type);
                    Portalcon.DisConnect();
                }
            }
            else
            {
                dr1.Close();
                con.DisConnect();
            }
            if (lblHolidayexpect.Text == "1" && lblOffdayexpect.Text == "1")
            {
                Portalcon.CancelLeave_AttendencewithApprovalforLeave(tblenameAttendence1, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type);
                Portalcon.DisConnect();
            }
            if (lblHolidayexpect.Text == "0" && lblOffdayexpect.Text == "0")
            {
                Portalcon.CancelLeave_AttendencewithApprovalforLeave(tblenameAttendence1, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type);
                Portalcon.DisConnect();
            }
            if (lblHolidayexpect.Text == "1" && lblOffdayexpect.Text == "0")
            {
                Portalcon.CancelLeave_AttendencewithApprovalforLeave(tblenameAttendence1, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type);
                Portalcon.DisConnect();
            }
            if (lblHolidayexpect.Text == "0" && lblOffdayexpect.Text == "1")
            {
                Portalcon.CancelLeave_AttendencewithApprovalforLeave(tblenameAttendence1, timein, timeout, status, hourpresent, chkUSerID, From_Date, To_Date, Leave_Period, Leave_Type);
                Portalcon.DisConnect();
            }


            con.Delete_leavePendingDetail(id);
            con.DisConnect();
            if (leave_type == "CO")
            {
                con.Delete_tbl_Co_Leave_Details(AutoNoh);
                con.DisConnect();
            }
            show_LeaveStatus();
            sendMailfor_Cancellation(From_Date, To_Date);

        }
        if (rb1.Checked == false)
        {
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please select option which you want to cancel leave');", true);

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Please select leave which you want to cancel');", true);
        }
    }
    protected void grdCOLeave_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Days you worked on week off / holiday";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderGridRow.BackColor = System.Drawing.Color.Wheat;
            //HeaderGridRow.ForeColor = System.Drawing.Color.White;


            HeaderCell.CssClass = "HeaderStyleofgrid";
            HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Date";
            //HeaderCell.ColumnSpan = 2;
            //HeaderGridRow.Cells.Add(HeaderCell);

            grdCOLeave.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }

    int count = 0;
    protected void chkSelectCO_CheckedChanged(object sender, EventArgs e)
    {
        string ss = "";
        string strname = "";
        foreach (GridViewRow gvrow in grdCOLeave.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelectCO");
            if (chk != null & chk.Checked)
            {
                strname += ss + ',';
                count++;
            }
            lblerrorthanthree.Text = "";
        }
        if (count > 3)
        {
            foreach (GridViewRow gvrow in grdCOLeave.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelectCO");
                chk.Checked = false;
            }
            lblerrorthanthree.Text = "can not select more than three days";
            //grdCOLeave.Enabled = false;
        }
        // NoofLeaveApplyinCo();
    }
    protected void grdleave_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Current leave status";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderGridRow.BackColor = System.Drawing.Color.Wheat;
            //HeaderGridRow.ForeColor = System.Drawing.Color.White;


            HeaderCell.CssClass = "HeaderStyleofgrid";
            HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Date";
            //HeaderCell.ColumnSpan = 2;
            //HeaderGridRow.Cells.Add(HeaderCell);

            grdleave.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void grdLeaveDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Applied Leave";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderGridRow.BackColor = System.Drawing.Color.Wheat;
            //HeaderGridRow.ForeColor = System.Drawing.Color.White;


            HeaderCell.CssClass = "HeaderStyleofgrid";
            HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Date";
            //HeaderCell.ColumnSpan = 2;
            //HeaderGridRow.Cells.Add(HeaderCell);

            grdLeaveDetail.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }

    public void ShowGridworkingdetails(string AutoNo)
    {
        SqlDataReader dr = con.Show_tble_cowithAutono(AutoNo);

        DataTable dt = new DataTable();
        dt.Load(dr);
        grdworkingdetails.DataSource = dt;
        grdworkingdetails.DataBind();
        dr.Close();
        Portalcon.DisConnect();

    }
    protected void btnworkingDetails_Command(object sender, CommandEventArgs e)
    {
        string autono = e.CommandArgument.ToString();
        mdworkingdetails.Show();
        ShowGridworkingdetails(autono);
    }
    protected void btnWorkingdetailsview_Command(object sender, CommandEventArgs e)
    {
        string autono = e.CommandArgument.ToString();
        mdworkingdetails.Show();
        ShowGridworkingdetails(autono);
    }
    protected void grdLeaveDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Button btnworkingDetails = (Button)e.Row.FindControl("btnworkingDetails");
            Label lblLeavetypeApplied = (Label)e.Row.FindControl("lblLeavetypeApplied");

            Label lblleaveAttachmentFilename = (Label)e.Row.FindControl("lblleaveAttachmentFilename");
            LinkButton lnkDownloadgrid = (LinkButton)e.Row.FindControl("lnkDownloadgrid");
            if (lblleaveAttachmentFilename.Text.Trim() == "")
            {
                lnkDownloadgrid.Visible = false;
            }

            if (lblleaveAttachmentFilename.Text.Trim() != "")
            {
                lnkDownloadgrid.Visible = true;
            }
            if (lblLeavetypeApplied.Text == "CO")
            {
                btnworkingDetails.Visible = true;
            }
            if (lblLeavetypeApplied.Text != "CO")
            {
                btnworkingDetails.Visible = false;
            }
        }
    }

    public void LeaveCoupto()
    {
        string coUpto = "";

        SqlDataReader drupto = con.SHowCoupto(Session["Company"].ToString());
        drupto.Read();
        if (drupto.HasRows)
        {
            string useridind = drupto["Comp Leave Upto"].ToString();
            if (Session["uid"].ToString() == useridind)
            {
                lblleaveUpto.Text = drupto["Individual Duration"].ToString();
            }
            if (Session["uid"].ToString() != useridind)
            {

                lblleaveUpto.Text = drupto["Comp Leave Upto"].ToString();
            }
        }
        drupto.Close();
        con.DisConnect();

    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        pnlLeaveApplication.Visible = false;
        pnlMain.Visible = false;
        pnlViewleaveDetail.Visible = true;

    }
    public void ExpiredateDelete(string attendat1)
    {
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");
        string EmployeLeaveEntitledup = "[" + rccname + "$Pay Employee Leave Entitled" + "]";
        string tbleEmployeeLeaveCredited = "[" + rccname + "$Pay Employee Leave Credited" + "]";
        SqlDataReader dr = Portalcon.ShowCOleaveBalanceIncative(EmployeLeaveEntitledup, Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            string levabalanceco = dr["Leave Balance"].ToString();
            dr.Close();
            Portalcon.DisConnect();
            decimal levabalanceco1 = Convert.ToDecimal(levabalanceco);
            decimal levabalanceco2 = levabalanceco1 - 1;

            Portalcon.updateCoBalanceforInactivedd(EmployeLeaveEntitledup, levabalanceco2.ToString(), Session["uid"].ToString());
            Portalcon.DisConnect();

            Portalcon.updateCoLeaveInative(tbleEmployeeLeaveCredited, Session["uid"].ToString(), attendat1);
            Portalcon.DisConnect();
            con.delete_tbl_Co_Leave_DetailsIncative(Session["uid"].ToString(), Session["Company"].ToString(), attendat1);
            con.DisConnect();
            con.delete_LeaveApplicationIncative(Session["uid"].ToString(), attendat1, Session["Company"].ToString());
            con.DisConnect();
        }
        else
        {
            dr.Close();
            Portalcon.DisConnect();
        }
    }

    public void ExpiredataCo()
    {

        SqlDataAdapter da = new SqlDataAdapter("select * from tbl_Co_Leave_Details where Userid ='" + Session["uid"].ToString() + "' and Company='" + Session["Company"].ToString() + "' and Status='Pending'", con.Con);
        DataSet ds = new DataSet();
        da.Fill(ds, "tbl_Co_Leave_Details");
        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
        {
            string attendanceDateexpire = ""; string lblExpireCOexpires = ""; string attendanceDateexpiredate2 = "";
            attendanceDateexpire = ds.Tables[0].Rows[i]["Co_Date"].ToString();

            DateTime attendanceDateexpiredate = Convert.ToDateTime(attendanceDateexpire);
            attendanceDateexpiredate2 = attendanceDateexpiredate.ToString("dd MMM yyyy");
            lblExpireCOexpires = ds.Tables[0].Rows[i]["Expire on"].ToString();
            DateTime attendatss = Convert.ToDateTime(lblExpireCOexpires);
            string attendatsssss = attendatss.ToString("dd MMM yyyy");

            DateTime attendatssssrd = DateTime.ParseExact(attendatsssss, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string currentdateddd = System.DateTime.Now.ToString("dd MMM yyyy");


            DateTime Currentdatssssedr = DateTime.ParseExact(currentdateddd, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (attendatssssrd < Currentdatssssedr)
            {

                ExpiredateDelete(attendanceDateexpiredate2);
            }

        }
        con.DisConnect();

    }

    protected void grdCOLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblAttendacedate1 = (Label)e.Row.FindControl("lblAttendacedate");
            CheckBox chkSelectCO = (CheckBox)e.Row.FindControl("chkSelectCO");
            // Label lblExpireCO = (Label)e.Row.FindControl("lblExpireCO");

            DateTime attendat = Convert.ToDateTime(lblAttendacedate1.Text);
            string attendat1 = attendat.ToString("dd MMM yyyy");
            DateTime lbldatett = DateTime.ParseExact(attendat1, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //DateTime expireco = Convert.ToDateTime(lblExpireCO.Text);
            //string lblexp = expireco.ToString("dd MMM yyyy");
            //DateTime lblexpd = DateTime.ParseExact(lblexp, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DateTime expireco = Convert.ToDateTime(System.DateTime.Now.ToString("dd MMM yyyy"));
            string lblexp = expireco.ToString("dd MMM yyyy");
            DateTime lblexpd = DateTime.ParseExact(lblexp, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);


            TimeSpan span = lblexpd.Subtract(lbldatett);
            string ss = span.TotalDays.ToString();
            decimal drv = Convert.ToDecimal(ss);
            decimal exno = Convert.ToDecimal(lblleaveUpto.Text);
            if (drv > exno)
            {

                e.Row.BackColor = System.Drawing.Color.LightGray;
                e.Row.ForeColor = System.Drawing.Color.White;
                chkSelectCO.Enabled = false;

            }



        }


    }

    public int Arrangement()
    {
        int count = 0;
        if (chkArrangement.Checked == true)
        {
            DateTime FromDate = DateTime.ParseExact(txtfromDate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime ToDate = DateTime.ParseExact(txtTodate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string HalfDayStatus = ddShiftType.SelectedItem.Text;
            string procName = "sp_ArrangementValidateForLeave";
            DataUtility objDut = new DataUtility();
            string[] ParamName = new string[4];
            object[] ParamValue = new object[4];
            ParamValue[0] = Session["uid"].ToString();
            ParamName[0] = "UserId";
            ParamValue[1] = FromDate;
            ParamName[1] = "FromDate";
            ParamValue[2] = ToDate;
            ParamName[2] = "ToDate";
            ParamValue[3] = HalfDayStatus;
            ParamName[3] = "HalfDayStatus";
            DataTable dt = objDut.GetDataTableCmdParams(procName, ParamName, ParamValue);
            count = Convert.ToInt32(dt.Rows[0][0].ToString());

        }
        return count;
    }
    protected void rblArrangement_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public int DuplicateLeave()
    {
        int dupDays = 0;
        DateTime FromDate = DateTime.ParseExact(txtfromDate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime ToDate = DateTime.ParseExact(txtTodate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string procName = "SP_LeaveValidate";
        // DataUtility objDut = new DataUtility();
        string[] ParamName = new string[3];
        object[] ParamValue = new object[3];
        ParamValue[0] = Session["uid"].ToString();
        ParamName[0] = "UserId";
        ParamValue[1] = FromDate;
        ParamName[1] = "FromDate";
        ParamValue[2] = ToDate;
        ParamName[2] = "ToDate";
        DataTable dt = GetDataTableCmdParams(procName, ParamName, ParamValue);
        dupDays = Convert.ToInt32(dt.Rows[0][0].ToString());
        return dupDays;
    }

    public DataTable GetDataTableCmdParams(string ProcName, string[] ParamName, object[] ParamValue) //ashu
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
        Conn.Open();
        DataTable dt;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = Conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = ProcName;

        //this.cmd = CmdProc(ProcName);
        for (int i = 0; i < ParamName.Length; i++)
        {
            cmd.Parameters.AddWithValue(ParamName[i], ParamValue[i]);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            dt = new DataTable();
            da.Fill(dt);
        }
        catch
        {
            throw;
        }
        finally
        {
            Conn.Close();
        }
        return dt;
    }


    protected void ddShiftType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    public void UploadFileAttachmentss()
    {
        string AutoNo = "";
        SqlDataReader dr = con.Show_MAlLeaveMaxid();
        dr.Read();
        if (dr.HasRows)
        {
            AutoNo = dr["AutoNo"].ToString();
        }
        else
        {
            AutoNo = "0";
        }
        string filename = Path.GetFileName(flUpload.PostedFile.FileName);
        string contentType = flUpload.PostedFile.ContentType;
        using (Stream fs = flUpload.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);



                SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
                Conn.Open();
                //  string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                string query = "update tble_Leave_Approval set AttachmentFilename=@AttachmentFilename ,AttachmentFileType=@AttachmentFileType,Attachmentdata=@Attachmentdata where AutoNo='" + AutoNo + "'";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = Conn;
                    cmd.Parameters.AddWithValue("@AttachmentFilename", filename);
                    cmd.Parameters.AddWithValue("@AttachmentFileType", contentType);
                    cmd.Parameters.AddWithValue("@Attachmentdata", bytes);

                    cmd.ExecuteNonQuery();
                    con.DisConnect();
                }
            }
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        UploadFileAttachmentss();
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        try
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;
            SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
            Conn.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select AttachmentFilename, Attachmentdata, AttachmentFileType from tble_Leave_Approval where AutoNo=@AutoNo";
                cmd.Parameters.AddWithValue("@AutoNo", id);
                cmd.Connection = Conn;
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Attachmentdata"];
                    contentType = sdr["AttachmentFileType"].ToString();
                    fileName = sdr["AttachmentFilename"].ToString();
                }
                con.DisConnect();

            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Attachment not found ');", true);

        }
    }

    protected void chkPostlunch_CheckedChanged(object sender, EventArgs e)
    {
        txtNoOfLeavePriod.Text = "0";
        Show_NoOfDays();
        NoofLeaveApplyinCo();
        HalfLeaveforPrepostonly();
        try
        {
            HidExcludeddayforcl();
        }
        catch (Exception)
        { }


    }
    protected void chkPrelunch_CheckedChanged(object sender, EventArgs e)
    {
        txtNoOfLeavePriod.Text = "0";
        Show_NoOfDays();
        NoofLeaveApplyinCo();
        HalfLeaveforPrepostonly();
        try
        {
            HidExcludeddayforcl();
        }
        catch (Exception)
        { }

    }

    public void HalfLeaveforPrepostonly()
    {
        if (ddLeaveTypen.SelectedValue == "CL")
        {
            chkPrelunch.Visible = false;
            chkPostlunch.Visible = false;
            chkPrelunch.Checked = false;
            chkPostlunch.Checked = false;
        }
        else
        {
            decimal halno1 = Convert.ToDecimal(txtNoOfLeavePriod.Text.Trim());
            if (halno1 > 1)
            {

                if (chkPrelunch.Checked == true && chkPostlunch.Checked == false || chkPrelunch.Checked == false && chkPostlunch.Checked == true)
                {
                    decimal pint5 = Convert.ToDecimal("0.5");
                    txtNoOfLeavePriod.Text = (halno1 - pint5).ToString();
                }

                if (chkPrelunch.Checked == true && chkPostlunch.Checked == true)
                {
                    decimal pint5 = Convert.ToDecimal("1.0");
                    txtNoOfLeavePriod.Text = (halno1 - pint5).ToString();
                }
            }
            showdiablee();
        }
    }
    //----------------------Ashu---------------07-03-2017----
    public void InsertCo_Leave_ApplicationANDUpdateCo_Leave_DetailsFromNav()  //ashu 07-03-2017
    {
        con.InsertCo_Leave_ApplicationFromNav(Session["uid"].ToString(), Session["uname"].ToString());
        con.Updatetbl_Co_Leave_DetailsFromNav(Session["uid"].ToString());

    }
}