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
using System.Configuration;//ashu

public partial class Faculty_StaffOdd : System.Web.UI.Page
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
                Response.Redirect("Error.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();


                SqlDataAdapter da1 = new SqlDataAdapter("select case when [Hospital HR Leave]=1 then 1 else 0 end as 'access'  from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where  [No_]='" + Session["uid"].ToString() + "' ", con.Con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows[0]["access"].ToString() == "1")
                {
                    SqlDataAdapter da = new SqlDataAdapter("select No_,concat([First Name],'(',No_,')') as 'First Name'  from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where   ([Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' or([Global Dimension 1 Code] ='TMNS' and [Designation Code]='D164'  )) and Status=0 and No_ !='" + Session["uid"].ToString() + "'", con1);
                    //SqlDataAdapter da = new SqlDataAdapter("select No_,concat([First Name],'(',No_,')') as 'First Name'  from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and No_ !='" + Session["uid"].ToString() + "'", con1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    drpEmployee.DataSource = dt;
                    drpEmployee.DataValueField = "No_";
                    drpEmployee.DataTextField = "First Name";
                    drpEmployee.DataBind();
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

        }


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
    protected void txtTodate_TextChanged(object sender, EventArgs e)
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
    public int DuplicateOD()
    {
        int dupDays = 0;
        DateTime FromDate = DateTime.ParseExact(txtFromDate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime ToDate = DateTime.ParseExact(txtTodate.Text.Trim(), "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string procName = "SP_OD_Duplicate_Control";

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
    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
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
            SqlDataReader dr = navconn.Show_LeaveAppliedFOROD(Pay_Daily_Attendence_Detail, txtFromDate.Text.Trim(), txtTodate.Text.Trim(), Session["uid1"].ToString());
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

                con.Insert_tbl_OD_Application(Session["uid1"].ToString(), Session["uname1"].ToString(), txtFromDate.Text.Trim(), txtFromTime.Text.Trim(), txtTodate.Text.Trim(), txtToTime.Text.Trim(), txtDestination.Text.Trim(), txtPurpose.Text.Trim(), txtRemarks.Text.Trim(), System.DateTime.Now.ToString("yyyy-MM-dd"), Session["hod_ID_Leave2"].ToString(), Session["hod_ID_Leave1"].ToString(), Session["Company"].ToString());
                con.DisConnect();
                lblODSuccess.Visible = true;
                SendSMSHODExam(drpEmployee.SelectedValue, txtFromDate.Text.Trim(), txtTodate.Text.Trim());

                SqlDataAdapter da = new SqlDataAdapter("select  * from tbl_OD_Application where [Employee No]='" + drpEmployee.SelectedValue + "' and Approval='Pending' order by id desc ", con.Con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                ShowApprovalData(dt.Rows[0]["id"].ToString());



                sendAproved(txtFromDate.Text.Trim(), txtTodate.Text.Trim(), drpEmployee.SelectedValue, "Approved");
                clear();
            }
        }

    }

    public void sendAproved(string FromDate, string todate, string Eid, string Status)
    {
        string tablenameemployeedata = "";
        string ccname = Session["Company"].ToString();
        string rccname = ccname.Replace(".", "_");

        tablenameemployeedata = "[" + rccname + "$Employee" + "]";
        string mobilnoemp = "";

        string smsdata = "Dear Applicant your OD leave from " + FromDate + " to " + todate + " has been " + Status + ".";

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





    string Emp_No_OD = ""; string frmmdate_OD = ""; string Todate_OD = ""; string Purpose_OD = ""; string fromtime_od = ""; string Totime_OD = ""; string Approval_Status_OD = "";
    public void ShowApprovalData(string id)
    {
        SqlDataReader dr = con.Show_ApprovalODid(id);
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

            if (Approval_Status_OD == "Pending")
            {

                string ccname = Session["Company"].ToString();
                string rccname = ccname.Replace(".", "_");
                string tblenameAttendence = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                navconn.Update_OD_Approval(tblenameAttendence, fromtime_od.Trim(), Totime_OD.Trim(), Emp_No_OD.Trim(), frmmdate_OD.Trim(), Todate_OD.Trim(), Purpose_OD.Trim());
                navconn.DisConnect();
                con.Update_ODStatus(id, Session["uname1"].ToString(), Session["uid1"].ToString(), "Approved");
                con.DisConnect();

            }

        }
        else
        {
            dr.Close();
            con.DisConnect();

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

    protected void btnGet_ViewStatus_Click(object sender, EventArgs e)
    {

    }
    protected void btnExporttoexcel_viewStatus_Click(object sender, EventArgs e)
    {

    }

    protected void grdView_Status_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
}