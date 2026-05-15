using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_CO_StaffLeave : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;
    Connection navconn;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            navconn = new Connection();
            if (!IsPostBack)
            {
                SqlDataAdapter da1 = new SqlDataAdapter("select case when [Hospital HR Leave]=1 then 1 else 0 end as 'access'  from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where  [No_]='" + Session["uid"].ToString() + "' ", con.Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows[0]["access"].ToString() == "1")
                {
                    SqlDataAdapter da = new SqlDataAdapter("select No_,concat([First Name],'(',No_,')') as 'First Name'  from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where   ([Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' or([Global Dimension 1 Code] ='TMNS' and [Designation Code]='D164'  and Status=0)) and No_ !='" + Session["uid"].ToString() + "'", con1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    drpEmployee.DataSource = dt;
                    drpEmployee.DataValueField = "No_";
                    drpEmployee.DataTextField = "First Name";
                    drpEmployee.DataBind();
                    String sDate = DateTime.Now.ToString();
                    DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));


                    int day = datevalue.Day;
                    int mn = datevalue.Month;
                    int yy = datevalue.Year;

                    if (day < 7)
                    {

                        clndAppliedate.StartDate = new DateTime(yy, mn - 1, 21);

                    }
                    else
                    {
                        clndAppliedate.StartDate = new DateTime(yy, mn, 01);

                    }


                }
                else
                {
                    Response.Redirect("../Default.aspx");
                }

            }



        }
        catch (Exception)
        {

            Response.Redirect("../Default.aspx");
        }
    }
    protected void drpEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));


            int day = datevalue.Day;
            int mn = datevalue.Month;
            int yy = datevalue.Year;

            if (day < 7)
            {

                clndAppliedate.StartDate = new DateTime(yy, mn - 1, 24);
               
            }
            else
            {
                clndAppliedate.StartDate = new DateTime(yy, mn, 01);
              
            }




            //clndAppliedate.StartDate = DateTime.Now.AddDays(-10);
            //clndAppliedate.EndDate = DateTime.Now.AddDays(0);
            SqlDataAdapter da = new SqlDataAdapter("select * from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and No_='" + drpEmployee.SelectedValue + "'", con.Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["uid1"] = dt.Rows[0]["No_"].ToString();
                Session["uname1"] = dt.Rows[0]["First Name"].ToString();
                Session["hod_ID_Leave2"] = dt.Rows[0]["HOD"].ToString();
                Session["hod_Name_Leave2"] = dt.Rows[0]["HOD Name"].ToString();
                Session["hod_ID_Leave3"] = dt.Rows[0]["HOD 1"].ToString();
                Session["hod_Name_Leave3"] = dt.Rows[0]["HOD Name 1"].ToString();
                Session["HRID_leave1"] = dt.Rows[0]["HR"].ToString();
                Session["HODLoginPage1"] = dt.Rows[0]["HOD"].ToString();
                Session["HODLoginPage2"] = dt.Rows[0]["HOD 1"].ToString();
                Session["hod_email2"] = dt.Rows[0]["HOD 1"].ToString();
                Session["Fulname1"] = dt.Rows[0]["First Name"].ToString() + "  " + dt.Rows[0]["Middle Name"].ToString() + " " + dt.Rows[0]["Last Name"].ToString();
                lblfirstApproval.Text = Session["hod_Name_Leave2"].ToString() + "(" + Session["hod_ID_Leave2"].ToString() + ")";
                lblSecondApproval.Text = Session["hod_Name_Leave3"].ToString() + "(" + Session["hod_ID_Leave3"].ToString() + ")";
                if (Session["hod_ID_Leave2"].ToString() == "")
                {
                    lblApprovalAuthority1.Visible = true;
                    btnSendForApproval.Enabled = false;
                }

                if (Session["hod_ID_Leave2"].ToString() != "")
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
        catch (Exception ex)
        {
        }
    }

    protected void lnkODApplication_Click(object sender, EventArgs e)
    {

    }
    protected void lnkODView_Click(object sender, EventArgs e)
    {

    }
    protected void lnkApproval_Click(object sender, EventArgs e)
    {

    }
    public void ShowPendingApprovalCount()
    {
        SqlDataReader dr = con.Show_HODODCountCOApplication(Session["uid1"].ToString(), Session["uid1"].ToString(), Session["Company"].ToString());
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
    public void VisiblilitybyHOD()
    {
        SqlDataReader dr = navconn.SHow_showHODExhistCO(Session["uid1"].ToString(), Session["CompanyTableEmployee"].ToString());
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
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        //ToTalWorkingHour();
    }
    protected void txtFromTime_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtToTime_TextChanged(object sender, EventArgs e)
    {

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
                con.Update_CoStatus(id, Session["uname1"].ToString());
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

                    navconn.Update_Co_ApplicationApproval(tblenameAttendence, useridapr, frmmdate_OD, "CO Approved By HOD");

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
    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        ToTalWorkingHour();
        SqlDataReader dr = con.Duplicate_tbl_attendenceforCOApplication(txtFromDate.Text.Trim(), Session["uid1"].ToString(), Session["Company"].ToString());
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

            con.insert_tbl_attendenceforCOApplication(Session["uid1"].ToString(), Session["uname1"].ToString(), Session["Company"].ToString(), txtFromDate.Text.Trim(), txtFromTime.Text.Trim(), txtToTime.Text.Trim(), txtDestination.Text.Trim(), txtRemarks.Text.Trim(), "Present", workingduration, System.DateTime.Now.ToString("yyyy-MM-dd"), "Pending", Session["hod_Name_Leave2"].ToString(), "1", "Manual", Session["hod_Name_Leave3"].ToString(), Session["hod_ID_Leave3"].ToString(), txtPurpose.Text.Trim(), Session["hod_ID_Leave2"].ToString(), Session["uid"].ToString());
            con.DisConnect();
            SqlDataAdapter da1 = new SqlDataAdapter("select Top 1 ID from tbl_Co_Leave_Application where Userid='" + Session["uid1"].ToString() + "' and ApprovalStatus='Pending' order by ID desc ", con.Con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                ShowApprovalData(dt1.Rows[0]["ID"].ToString());
            }
            lblCOSuccess.Visible = true;
            SendSMSHODExam(Session["uname1"].ToString(), Session["uid1"].ToString(), txtFromDate.Text.Trim());// for sms
            clear();
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

        SqlDataReader dr = Portalcon.Show_AthorityNo(Session["uid1"].ToString(), tablenameemployeedata);
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
    public void clear()
    {
        txtFromDate.Text = "";
        txtToTime.Text = "";
        txtFromTime.Text = "";
        txtRemarks.Text = "";
        txtPurpose.Text = "";
    }
    protected void btnExporttoexcel_viewStatus_Click(object sender, EventArgs e)
    {

    }
    protected void btnExporttoexcel_viewStatus_Click1(object sender, EventArgs e)
    {

    }
    protected void grdView_Status_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

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
    protected void btnExporttoexcel_viewStatus_Click2(object sender, EventArgs e)
    {

    }

    protected void btnGet_ViewStatus_Click(object sender, EventArgs e)
    {

    }
    protected void btnExporttoexcel_viewStatus_Click3(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void btncancel_record_Click(object sender, EventArgs e)
    {

    }
}