using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;



public partial class Faculty_WorkFHome : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    Connection navconn;
    protected void Page_Load(object sender, EventArgs e)
    {

       
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            navconn = new Connection();

            if (!IsPostBack)
            {
                //Local Process
                //Response.Redirect("~/Faculty/Error.aspx", false);
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Live
                Response.Redirect("Error.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
             

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

                //if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in" || Session["CompanyEmail"].ToString() == "registrar.tmu.ac.in")
                //{

                //    lnkApproval.Text = "Approval";
                //}

                if (Session["CompanyEmail"].ToString() != "vicechancellor@tmu.ac.in" && Session["CompanyEmail"].ToString() != "registrar.tmu.ac.in")
                {

                    lnkApproval.Text = "Approval";
                }


                con.Update_WFH(Session["hod_ID_Leave1"].ToString(), Session["hod_ID_Leave2"].ToString(), Session["uid"].ToString(), Session["Company"].ToString());
                con.DisConnect();
                String sDate = DateTime.Now.ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

                int day = datevalue.Day;
                int mn = datevalue.Month;
                int yy = datevalue.Year;


                CalendarExtender1.StartDate = new DateTime(yy, mn, 01);
                CalendarExtender2.StartDate = new DateTime(yy, mn, 01);
                //CalendarExtender1.EndDate = new DateTime(yy, mn-1, 31);
                //CalendarExtender2.EndDate = new DateTime(yy, mn-1, 31);
            
            
            }

            VisiblilitybyHOD();
            ShowPendingApprovalCount();


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
    public void SendSMSHODExam(string uname, string FromDate, string ToDate)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";

        string smsdata = "Dear sir your faculty " + uname + ",  OD  from " + FromDate + " to " + ToDate + " has been applied. " + "Thanks ";



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



    public void sendAproved(string FromDate, string todate, string Eid, string Status)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";

        string smsdata = "Dear Applicant your WFH leave from " + FromDate + " to " + todate + " has been " + Status + ".";

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




    protected void lnkODApplication_Click(object sender, EventArgs e)
    {
        lblHeader.Text = "Application";
        pnlViewStatus.Visible = false;
        pnlODApplication.Visible = true;
        pnlApproval.Visible = false;
        lblODSuccess.Visible = false;
        clear();
    }
    public void VisiblilitybyHOD()
    {
        SqlDataReader dr = navconn.SHow_showHODExhist(Session["uid"].ToString(), Session["CompanyTableEmployee"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            navconn.DisConnect();

            lnkApproval.Visible = true;
            lblCountODAppoval.Visible = true;
        }
        else
        {
            dr.Close();
            navconn.DisConnect();
            lnkApproval.Visible = false;
            lblCountODAppoval.Visible = false;
        }


        //if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
        //{
        //    lnkApproval.Visible = true;
        //    lblCountODAppoval.Visible = true;

        //}
        //if (Session["CompanyEmail"].ToString() == "registrar.tmu.ac.in")
        //{
        //    lnkApproval.Visible = true;
        //    lblCountODAppoval.Visible = true;
        //}
    }

    protected void lnkODView_Click(object sender, EventArgs e)
    {
        lblHeader.Text = "Report";
        pnlViewStatus.Visible = true;
        pnlODApplication.Visible = false;
        pnlApproval.Visible = false;
        txtFromDate_ViewStatus.Text = "";
        txtTodate_ViewStatus.Text = "";
        ddStatus_ViewStatus.SelectedValue = "Pending";
        Show_ODStatusByOwn();
        grdView_Status.Columns[8].Visible = false;
        grdView_Status.Columns[9].Visible = false;

    }
    protected void lnkApproval_Click(object sender, EventArgs e)
    {
        //if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in" || Session["CompanyEmail"].ToString() == "registrar.tmu.ac.in")
        //{
        //    btnApprove.Text = "Approval";
        //    lblHeader.Text = "Approval";
        //    lnkApproval.Text = "Approval";
        //}
        if (Session["CompanyEmail"].ToString() != "vicechancellor@tmu.ac.in" && Session["CompanyEmail"].ToString() != "registrar.tmu.ac.in")
        {
            btnApprove.Text = "Approval";
            lblHeader.Text = "Approval";
            lnkApproval.Text = "Approval";
            //btnApprove.Text = "Recommend To Vice Chancellor";
            //lblHeader.Text = "Recommend";
            //lnkApproval.Text = "Recommend";
        }

        pnlViewStatus.Visible = false;
        pnlODApplication.Visible = false;
        pnlApproval.Visible = true;
        btnFIlterGet_Approval.Visible = false;
        ddStatus_Approval.SelectedValue = "Pending";
        pnlFilterDate.Visible = false;
        pnlFilterByIDName.Visible = false;
        rdDatewise.Checked = false;
        rdEmployeeID.Checked = false;
        rdEmployeeName.Checked = false;
        Show_HODData();


        grdApproval.Columns[0].Visible = true;
        grdApproval.Columns[11].Visible = false;
        ddStatus_Approval.Enabled = false;

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
        if (ddStatus_Approval.SelectedValue == "Pending")
        {

            grdApproval.Columns[0].Visible = true;
            grdApproval.Columns[11].Visible = false;
        }
        if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in" || Session["CompanyEmail"].ToString() == "registrar.tmu.ac.in")
        {
            if (ddStatus_Approval.SelectedValue == "Recommend")
            {

                grdApproval.Columns[0].Visible = true;
                grdApproval.Columns[11].Visible = false;
            }
        }

        //if (ddStatus_Approval.SelectedValue != "Pending")
        //{

        //    grdApproval.Columns[0].Visible = false;
        //    grdApproval.Columns[11].Visible = false;
        //}
        if (ddStatus_Approval.SelectedValue == "Rejected")
        {
            grdApproval.Columns[0].Visible = false;
            grdApproval.Columns[11].Visible = true;
        }

        Show_HODData();
    }

    protected void btnGet_ViewStatus_Click(object sender, EventArgs e)
    {
        if (ddStatus_ViewStatus.SelectedValue == "Rejected" || ddStatus_ViewStatus.SelectedValue == "All")
        {
            grdView_Status.Columns[8].Visible = true;
            grdView_Status.Columns[9].Visible = true;

            Show_ODStatusByOwn();

        }
        else
        {


            if (ddStatus_ViewStatus.SelectedValue == "Recommend")
            {
                grdView_Status.Columns[8].Visible = true;
                grdView_Status.Columns[9].Visible = false;
            }
            if (ddStatus_ViewStatus.SelectedValue == "Approved")
            {
                grdView_Status.Columns[8].Visible = true;
                grdView_Status.Columns[9].Visible = false;
            }
            if (ddStatus_ViewStatus.SelectedValue == "Pending")
            {
                grdView_Status.Columns[8].Visible = false;
                grdView_Status.Columns[9].Visible = false;
            }


            Show_ODStatusByOwn();

        }

    }


    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {


        SqlDataAdapter da1 = new SqlDataAdapter("select count(*) as Holiday from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data]  where [Employee No]='" + Session["uid"].ToString() + "' and Status in (2,3) and  convert(date, [Attendance Date],103)>='" + txtFromDate.Text + "' and convert(date, [Attendance Date],103) <='" + txtTodate.Text + "' ", con.Con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (Convert.ToInt32(dt1.Rows[0]["Holiday"]) > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not Apply Work from Home for Week-Off or Holiday.');", true);
            return;
        }

        if (txtFromTime.Text.Trim() == "")
        {
            txtFromTime.Text = "00:00";
        }
        if (txtToTime.Text.Trim() == "")
        {
            txtToTime.Text = "00:00";
        }
        if (DuplicateOD() > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have already Applied  ! check the OD Applied date');", true);
            return;
        }
        else
        {
            string ccname = Session["Company"].ToString();
            string rccname = ccname.Replace(".", "_");
            string Pay_Daily_Attendence_Detail = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
            SqlDataReader dr = navconn.Show_LeaveAppliedFOROD(Pay_Daily_Attendence_Detail, txtFromDate.Text.Trim(), txtTodate.Text.Trim(), Session["uid"].ToString());
            dr.Read();
            if (dr.HasRows)
            {
                dr.Close();
                navconn.DisConnect();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have already Applied Leave ! check the OD Applied date from Leave Date');", true);
            }
            else
            {
                dr.Close();
                navconn.DisConnect();

                con.Insert_tbl_WFH_Application(Session["uid"].ToString(), Session["uname"].ToString(), txtFromDate.Text.Trim(), txtFromTime.Text.Trim(), txtTodate.Text.Trim(), txtToTime.Text.Trim(), txtDestination.Text.Trim(), txtPurpose.Text.Trim(), txtRemarks.Text.Trim(), System.DateTime.Now.ToString("yyyy-MM-dd"), Session["hod_ID_Leave1"].ToString(), Session["hod_ID_Leave2"].ToString(), Session["Company"].ToString());
                con.DisConnect();
                lblODSuccess.Visible = true;
                SendSMSHODExam(Session["uid"].ToString(), txtFromDate.Text.Trim(), txtTodate.Text.Trim());



                clear();
            }
        }
    }
    public void clear()
    {
        txtFromDate.Text = "";
        txtFromTime.Text = "00:00";
        txtTodate.Text = "";
        txtToTime.Text = "00:00";
        txtDestination.Text = "";
        txtPurpose.Text = "";
        txtRemarks.Text = "";
    }

    public void Show_ODStatusByOwn()
    {
        if (txtFromDate_ViewStatus.Text.Trim() == "" || txtTodate_ViewStatus.Text.Trim() == "")
        {
            SqlDataReader dr = con.Show_WFHApplicationByEmployee(Session["uid"].ToString(), ddStatus_ViewStatus.SelectedValue.ToString().Trim(), Session["Company"].ToString());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdView_Status.DataSource = dt;
            grdView_Status.DataBind();
            dr.Close();
            con.DisConnect();
        }
        if (txtFromDate_ViewStatus.Text.Trim() != "" || txtTodate_ViewStatus.Text.Trim() != "")
        {

            if (ddStatus_ViewStatus.SelectedValue.Trim() == "All")
            {
                SqlDataReader dr = con.Show_WFHApplicationByEmployeeDateWiseAll(Session["uid"].ToString(), txtFromDate_ViewStatus.Text.Trim(), txtTodate_ViewStatus.Text.Trim(), Session["Company"].ToString());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdView_Status.DataSource = dt;
                grdView_Status.DataBind();
                dr.Close();
                con.DisConnect();
            }
            else
            {

                SqlDataReader dr = con.Show_WFHApplicationByEmployeeDateWise(Session["uid"].ToString(), ddStatus_ViewStatus.SelectedValue.ToString().Trim(), txtFromDate_ViewStatus.Text.Trim(), txtTodate_ViewStatus.Text.Trim(), Session["Company"].ToString());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdView_Status.DataSource = dt;
                grdView_Status.DataBind();
                dr.Close();
                con.DisConnect();
            }
        }
    }

    public int DuplicateOD()
    {
        int dupDays = 0;
        DateTime FromDate = DateTime.ParseExact(txtFromDate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime ToDate = DateTime.ParseExact(txtTodate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string procName = "SP_WFH_Duplicate_Control";

        string[] ParamName = new string[4];
        object[] ParamValue = new object[4];
        ParamValue[0] = Session["uid"].ToString();
        ParamName[0] = "UserId";
        ParamValue[1] = FromDate;
        ParamName[1] = "FromDate";
        ParamValue[2] = ToDate;
        ParamName[2] = "ToDate";

        ParamValue[3] = Session["Company"].ToString();
        ParamName[3] = "comp_ny";
        DataTable dt = GetDataTableCmdParams(procName, ParamName, ParamValue);
        dupDays = Convert.ToInt32(dt.Rows[0][0].ToString());
        return dupDays;
    }
    public DataTable GetDataTableCmdParams(string ProcName, string[] ParamName, object[] ParamValue) //ashu
    {
        con.Connect();
        DataTable dt;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con.Con;
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
            con.DisConnect();
        }
        return dt;
    }

    public void Show_HODData()
    {


        if (rdEmployeeID.Checked == false && rdEmployeeName.Checked == false && rdDatewise.Checked == false)
        {
            if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
            {
                SqlDataReader dr = con.Show_WFHApplicationByHODVC(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), Session["Company"].ToString());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                dr.Close();
                con.DisConnect();
                SqlDataReader dr1 = con.Show_WFHApplicationByHODVC(Session["uid"].ToString(), "Pending", Session["uid"].ToString(), Session["Company"].ToString());
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
            else if (Session["CompanyEmail"].ToString() == "registrar.tmu.ac.in")
            {
                SqlDataReader dr = con.Show_WFHApplicationByHODVC1(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), Session["Company"].ToString());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                dr.Close();
                con.DisConnect();
                SqlDataReader dr1 = con.Show_WFHApplicationByHODVC1(Session["uid"].ToString(), "Pending", Session["uid"].ToString(), Session["Company"].ToString());
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
            else
            {
                SqlDataReader dr = con.Show_WFHApplicationByHOD(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), Session["Company"].ToString());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                dr.Close();
                con.DisConnect();
                SqlDataReader dr1 = con.Show_WFHApplicationByHOD(Session["uid"].ToString(), "Pending", Session["uid"].ToString(), Session["Company"].ToString());
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


        }
        if (rdEmployeeID.Checked == true)
        {
            if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
            {
                SqlDataReader dr = con.Show_WFHApplicationByHODWithEMPIDVC(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                dr.Close();
                con.DisConnect();

                SqlDataReader dr1 = con.Show_WFHApplicationByHODWithEMPIDVC(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
                dr1.Read();
                if (dr1.HasRows)
                {

                    string drnnn = dr1["Approval"].ToString();
                    if (drnnn == "Recommend" || drnnn == "Pending")
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

            else
            {

                SqlDataReader dr = con.Show_WFHApplicationByHODWithEMPID(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                dr.Close();
                con.DisConnect();

                SqlDataReader dr1 = con.Show_WFHApplicationByHODWithEMPID(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
                dr1.Read();
                if (dr1.HasRows)
                {

                    string drnnn = dr1["Approval"].ToString();
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
        }

        if (rdEmployeeName.Checked == true)
        {
            if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
            {
                SqlDataReader dr = con.Show_WFHApplicationByHODWithEMPNAmeVC(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                dr.Close();
                con.DisConnect();

                SqlDataReader dr1 = con.Show_WFHApplicationByHODWithEMPNAmeVC(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
                dr1.Read();
                if (dr1.HasRows)
                {
                    string appr = dr1["Approval"].ToString();
                    if (appr == "Pending" || appr == "Recommend")
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
            else
            {

                SqlDataReader dr = con.Show_WFHApplicationByHODWithEMPNAme(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                dr.Close();
                con.DisConnect();

                SqlDataReader dr1 = con.Show_WFHApplicationByHODWithEMPNAme(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), Session["Company"].ToString());
                dr1.Read();
                if (dr1.HasRows)
                {
                    string appr = dr1["Approval"].ToString();
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

        if (rdDatewise.Checked == true)
        {
            if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
            {
                SqlDataReader dr = con.Show_WFHApplicationByHODWithDatewiseVC(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), txtFromDate_Approval.Text.Trim(), txtTodate_Approval.Text.Trim(), Session["Company"].ToString());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                dr.Close();
                con.DisConnect();

                SqlDataReader dr1 = con.Show_WFHApplicationByHODWithDatewiseVC(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), txtFromDate_Approval.Text.Trim(), txtTodate_Approval.Text.Trim(), Session["Company"].ToString());
                dr1.Read();
                if (dr1.HasRows)
                {
                    string appr = dr1["Approval"].ToString();
                    if (appr == "Pending" || appr == "Recommend")
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

            else
            {
                SqlDataReader dr = con.Show_WFHApplicationByHODWithDatewise(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), txtFromDate_Approval.Text.Trim(), txtTodate_Approval.Text.Trim(), Session["Company"].ToString());
                DataTable dt = new DataTable();
                dt.Load(dr);
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                dr.Close();
                con.DisConnect();

                SqlDataReader dr1 = con.Show_WFHApplicationByHODWithDatewise(Session["uid"].ToString(), ddStatus_Approval.SelectedValue.ToString().Trim(), Session["uid"].ToString(), txtEmployeeIDNameFilter.Text.Trim(), txtFromDate_Approval.Text.Trim(), txtTodate_Approval.Text.Trim(), Session["Company"].ToString());
                dr1.Read();
                if (dr1.HasRows)
                {
                    string appr = dr1["Approval"].ToString();
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
    }

    public void ShowPendingApprovalCount()
    {
        SqlDataReader dr = con.Show_HODWFHCount(Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblCountODAppoval.Text = dr["Approval"].ToString();
            dr.Close();
            con.DisConnect();
        }
        else
        {
            dr.Close();
            con.DisConnect();
        }


        if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
        {
            SqlDataReader dr1 = con.Show_HODWFHCountVC(Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString());
            dr1.Read();
            if (dr1.HasRows)
            {
                lblCountODAppoval.Text = dr1["Approval"].ToString();
                dr1.Close();
                con.DisConnect();
            }
            else
            {
                dr1.Close();
                con.DisConnect();
            }
        }
        if (Session["CompanyEmail"].ToString() == "registrar.tmu.ac.in")
        {
            SqlDataReader dr1 = con.Show_HODWFHCountVC1(Session["uid"].ToString(), Session["uid"].ToString(), Session["Company"].ToString());
            dr1.Read();
            if (dr1.HasRows)
            {
                lblCountODAppoval.Text = dr1["Approval"].ToString();
                dr1.Close();
                con.DisConnect();
            }
            else
            {
                dr1.Close();
                con.DisConnect();
            }
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

    protected void btnApproveExport_Click(object sender, EventArgs e)
    {

        grdView_Status.AllowPaging = false;
        if (ddStatus_Approval.SelectedValue == "Pending" || ddStatus_Approval.SelectedValue == "Recommend")
        {

            grdApproval.Columns[0].Visible = true;
            grdApproval.Columns[11].Visible = false;
        }

        if (ddStatus_Approval.SelectedValue != "Pending" || ddStatus_Approval.SelectedValue != "Recommend")
        {

            grdApproval.Columns[0].Visible = false;
            grdApproval.Columns[11].Visible = false;
        }
        if (ddStatus_Approval.SelectedValue == "Rejected")
        {
            grdApproval.Columns[0].Visible = false;
            grdApproval.Columns[11].Visible = true;
        }

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

    string Emp_No_OD = ""; string frmmdate_OD = ""; string Todate_OD = ""; string Purpose_OD = ""; string fromtime_od = ""; string Totime_OD = ""; string Approval_Status_OD = "";
    public void ShowApprovalData(string id)
    {
        SqlDataReader dr = con.Show_ApprovalWFHid(id);
        dr.Read();
        if (dr.HasRows)
        {
            frmmdate_OD = dr["From Date"].ToString();
            fromtime_od = dr["From Time"].ToString();
            DateTime frmmdate_OD1 = Convert.ToDateTime(frmmdate_OD);
            frmmdate_OD = frmmdate_OD1.ToString("yyyy-MM-dd");
            Todate_OD = dr["To Date"].ToString();
            DateTime Todate_OD1 = Convert.ToDateTime(Todate_OD);
            Todate_OD = Todate_OD1.ToString("yyyy-MM-dd");
            Totime_OD = dr["To Time"].ToString();

            if (Totime_OD == "00:00")
            {
                Totime_OD = "1753-01-01 00:00:00.000";
            }

            if (fromtime_od == "00:00")
            {
                fromtime_od = "1753-01-01 00:00:00.000";
            }

            Purpose_OD = dr["Purpose"].ToString();

            Approval_Status_OD = dr["Approval"].ToString();
            Emp_No_OD = dr["Employee No"].ToString();

            dr.Close();
            con.DisConnect();
            SqlDataAdapter da1 = new SqlDataAdapter("select count(*) as Holiday from [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Daily Attendence Detail] where [Employee Code]='" + Emp_No_OD + "' and  convert(date, [Attendance Date],103) >='" + frmmdate_OD.Trim() + "' and convert(date, [Attendance Date],103) <='" + Todate_OD.Trim() + "' and ([Off Day]=1 or Holiday=1) ", con.Con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (Convert.ToInt32(dt1.Rows[0]["Holiday"]) > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You can not Approve WFH for Week-Off or Holiday for Employee No. " + Emp_No_OD + ",Please Reject WFH and ask Employee to resubmit WFH application.');", true);
                return;
            }
            else
            {
                if (Approval_Status_OD == "Recommend" || Approval_Status_OD == "Pending")
                {
                    if (Session["CompanyEmail"].ToString() == "vicechancellor@tmu.ac.in")
                    {

                        string ccname = Session["Company"].ToString();
                        string rccname = ccname.Replace(".", "_");
                        string tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";

                        navconn.Update_WFH_Approval(tblenameAttendence, fromtime_od.Trim(), Totime_OD.Trim(), Emp_No_OD.Trim(), frmmdate_OD.Trim(), Todate_OD.Trim(), Purpose_OD.Trim());
                        navconn.DisConnect();
                        con.Update_WFHStatus(id, Session["uname"].ToString(), Session["uid"].ToString(), "Approved");
                        con.DisConnect();
                    }
                    if (Session["CompanyEmail"].ToString() == "registrar.tmu.ac.in")
                    {

                        string ccname = Session["Company"].ToString();
                        string rccname = ccname.Replace(".", "_");
                        string tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";

                        navconn.Update_WFH_Approval(tblenameAttendence, fromtime_od.Trim(), Totime_OD.Trim(), Emp_No_OD.Trim(), frmmdate_OD.Trim(), Todate_OD.Trim(), Purpose_OD.Trim());
                        navconn.DisConnect();
                        con.Update_WFHStatus(id, Session["uname"].ToString(), Session["uid"].ToString(), "Approved");
                        con.DisConnect();
                    }
                }
                if (Approval_Status_OD == "Pending")
                {
                    if (Session["CompanyEmail"].ToString() != "vicechancellor@tmu.ac.in" || Session["CompanyEmail"].ToString() != "registrar.tmu.ac.in")
                    {
                        string ccname = Session["Company"].ToString();
                        string rccname = ccname.Replace(".", "_");
                        string tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";

                        navconn.Update_WFH_Approval(tblenameAttendence, fromtime_od.Trim(), Totime_OD.Trim(), Emp_No_OD.Trim(), frmmdate_OD.Trim(), Todate_OD.Trim(), Purpose_OD.Trim());
                        navconn.DisConnect();
                        con.Update_WFHStatus(id, Session["uname"].ToString(), Session["uid"].ToString(), "Approved");
                        con.DisConnect();
                        sendAproved(frmmdate_OD.Trim(), Todate_OD.Trim(), Emp_No_OD.Trim(), "Approved");
                       
                    }

                }
            }
        }
        else
        {
            dr.Close();
            con.DisConnect();

        }
       
    }
    string Approval_Status_OD_reject = "";
    public void ShowApprovalDataRejected(string id)
    {
        SqlDataReader dr = con.Show_ApprovalWFHid(id);
        dr.Read();
        if (dr.HasRows)
        {


            Approval_Status_OD_reject = dr["Approval"].ToString();

            dr.Close();
            con.DisConnect();

            if (Approval_Status_OD_reject == "Pending" || Approval_Status_OD_reject == "Recommend")
            {

                con.Update_WFHStatusReject(id, Session["uname"].ToString(), Session["uid"].ToString(), txtRemarks_ByHOD.Text.Trim());
                con.DisConnect();
            }
        }
        else
        {
            dr.Close();
            con.DisConnect();

        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdApproval.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);

                Label lblid = (row.Cells[0].FindControl("lblid") as Label);
                Label lblFDate = (Label)row.FindControl("lblFromDate");
                Label lblTdate = (Label)row.FindControl("lblToDate");
                Label EmpId = (Label)row.FindControl("lblEmployeeid");

                if (chkRow.Checked == true)
                {

                    ShowApprovalData(lblid.Text.Trim());
                    
                }
            }
        }

        //if (ddStatus_Approval.SelectedValue == "Pending" || ddStatus_Approval.SelectedValue == "Recommend")
        //{

        //    grdApproval.Columns[0].Visible = true;
        //    grdApproval.Columns[11].Visible = false;
        //}

        //if (ddStatus_Approval.SelectedValue != "Pending" || ddStatus_Approval.SelectedValue != "Recommend")
        //{

        //    grdApproval.Columns[0].Visible = false;
        //    grdApproval.Columns[11].Visible = false;
        //}
        //if (ddStatus_Approval.SelectedValue == "Rejected")
        //{
        //    grdApproval.Columns[0].Visible = false;
        //    grdApproval.Columns[11].Visible = true;
        //}

        Show_HODData();
        ShowPendingApprovalCount();

    }
    protected void grdView_Status_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdView_Status.PageIndex = e.NewPageIndex;
        if (ddStatus_ViewStatus.SelectedValue == "Rejected" || ddStatus_ViewStatus.SelectedValue == "All")
        {
            Show_ODStatusByOwn();
            grdView_Status.Columns[8].Visible = true;
            grdView_Status.Columns[9].Visible = true;

        }
        else
        {



            if (ddStatus_ViewStatus.SelectedValue == "Approved")
            {
                grdView_Status.Columns[8].Visible = true;
                grdView_Status.Columns[9].Visible = false;
            }
            if (ddStatus_ViewStatus.SelectedValue == "Pending" || ddStatus_ViewStatus.SelectedValue == "Recommend")
            {
                grdView_Status.Columns[8].Visible = false;
                grdView_Status.Columns[9].Visible = false;
            }


            Show_ODStatusByOwn();
        }

    }
    protected void grdApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdApproval.PageIndex = e.NewPageIndex;
        if (ddStatus_Approval.SelectedValue == "Pending" || ddStatus_Approval.SelectedValue == "Recommend")
        {

            grdApproval.Columns[0].Visible = true;
            grdApproval.Columns[11].Visible = false;
        }

        if (ddStatus_Approval.SelectedValue != "Pending" || ddStatus_Approval.SelectedValue != "Recommend")
        {

            grdApproval.Columns[0].Visible = false;
            grdApproval.Columns[11].Visible = false;
        }
        if (ddStatus_Approval.SelectedValue == "Rejected")
        {
            grdApproval.Columns[0].Visible = false;
            grdApproval.Columns[11].Visible = true;
        }

        Show_HODData();
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow row in grdApproval.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {

        //        CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);

        //        Label lblid = (row.Cells[0].FindControl("lblid") as Label);

        //        if (chkRow.Checked == true)
        //        {
        //            lblIDforRejection.Text = lblid.Text.Trim();
        //        }
        //    }
        //}
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdApproval.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("chkMark") as CheckBox);

                Label lblid = (row.Cells[0].FindControl("lblid") as Label);
                Label lblFDate = (Label)row.FindControl("lblFromDate");
                Label lblTdate = (Label)row.FindControl("lblToDate");
                Label EmpId = (Label)row.FindControl("lblEmployeeid");

                if (chkRow.Checked == true)
                {

                    ShowApprovalDataRejected(lblid.Text.Trim());
                    sendAproved(lblFDate.Text.Trim(), lblTdate.Text.Trim(), EmpId.Text, "Rejected");
                }
            }
        }


        //if (ddStatus_Approval.SelectedValue == "Pending" || ddStatus_Approval.SelectedValue == "Recommend")
        //{

        //    grdApproval.Columns[0].Visible = true;
        //    grdApproval.Columns[11].Visible = false;
        //}

        //if (ddStatus_Approval.SelectedValue != "Pending" || ddStatus_Approval.SelectedValue != "Recommend")
        //{

        //    grdApproval.Columns[0].Visible = false;
        //    grdApproval.Columns[11].Visible = false;
        //}
        //if (ddStatus_Approval.SelectedValue == "Rejected")
        //{
        //    grdApproval.Columns[0].Visible = false;
        //    grdApproval.Columns[11].Visible = true;
        //}

        Show_HODData();
        ShowPendingApprovalCount();


    }

    public void ValidateDate()
    {

        DateTime frodatecom = DateTime.ParseExact(txtFromDate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime Todatecom = DateTime.ParseExact(txtTodate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime frodatecom = DateTime.ParseExact(txtFromDate.Text.Trim() + " " + txtFromTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime Todatecom = DateTime.ParseExact(txtTodate.Text.Trim() + " " + txtToTime.Text.Trim(), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        if (frodatecom > Todatecom)
        {
            txtTodate.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('To Date should always be greater than From Date');", true);

        }
        else
        {


        }

    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }
    protected void txtFromTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }
    protected void txtToTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ValidateDate();
        }
        catch (Exception)
        { }
    }
}