using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_SpecialPunch : System.Web.UI.Page
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

            if (!IsPostBack)
            {

                if (Session["uid"].ToString() == "TMU05721")
                {






                    SqlDataAdapter da = new SqlDataAdapter("select No_,concat([First Name],'(',No_,')') as 'First Name'  from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where Status=0", con1);
                    //SqlDataAdapter da = new SqlDataAdapter("select No_,concat([First Name],'(',No_,')') as 'First Name'  from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and No_ !='" + Session["uid"].ToString() + "'", con1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    drpEmployee.DataSource = dt;
                    drpEmployee.DataValueField = "No_";
                    drpEmployee.DataTextField = "First Name";
                    drpEmployee.DataBind();


                    //Response.Redirect("Error.aspx", false);
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //Local Process
                    //Response.Redirect("~/Faculty/Error.aspx", false);
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //Live
                    //Response.Redirect("Error.aspx", false);
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();

                    String sDate = DateTime.Now.ToString();
                    DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

                    int day = datevalue.Day;
                    int mn = datevalue.Month;
                    int yy = datevalue.Year;
                    DateTime now = DateTime.Now;
                    DateTime lastDayOfLastMonth = now.Date.AddDays(-now.Day);
                    int lastMonthLastDay = lastDayOfLastMonth.Day;
                    if (day <= 6)
                    {
                        //clndAppliedate.StartDate = new DateTime(2021, 12, lastMonthLastDay);

                        clndAppliedate.StartDate = new DateTime(yy, mn - 1, 1);
                    }
                    else
                    {
                        clndAppliedate.StartDate = new DateTime(yy, mn, 1);
                        //clndAppliedate.StartDate = new DateTime(yy, mn, day - 1);
                    }
                    //CalendarExtender2.StartDate = new DateTime(yy, mn, 01);


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




                }

                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
          



        }
        catch (Exception)
        {
            Response.Redirect("../Default.aspx");
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
        if (ddStatus_ViewStatus.SelectedValue.Trim() == "All")
        {
            SqlDataReader dr = con.ShowByusertbl_Punch(txtFromDate_ViewStatus.Text.Trim(), txtTodate_ViewStatus.Text.Trim(), Session["uid"].ToString(), Session["Company"].ToString());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdView_Status.DataSource = dt;
            grdView_Status.DataBind();
            dr.Close();
            con.DisConnect();
        }
        else
        {

            SqlDataReader dr = con.ShowByusertbl_PunchIn(txtFromDate_ViewStatus.Text.Trim(), txtTodate_ViewStatus.Text.Trim(), Session["uid"].ToString(), Session["Company"].ToString(), ddStatus_ViewStatus.SelectedValue.Trim());
            DataTable dt = new DataTable();
            dt.Load(dr);
            grdView_Status.DataSource = dt;
            grdView_Status.DataBind();
            dr.Close();
            con.DisConnect();
        }
    }

  






  







  
   
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter da1 = new SqlDataAdapter("select *,(select [Night Exist] from [TMU$Shift Master] where [Shift Code]=[TMU$Employee Actual Punch Data].[Shift Code]) as 'Night' from  [TMU$Employee Actual Punch Data] where [Employee No]='" + drpEmployee.SelectedValue + "' and [Attendance Date]='" + txtFromDate.Text.Trim() + "' ", navconn.Con);
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
            txtFromTime1.Text = dt.ToString("HH:mm");
            txtToTime1.Text = dt2.ToString("HH:mm");
            hfShiftFrom.Value = ShiftFrom.ToString("HH:mm");
            hfShiftTo.Value = ShiftTo.ToString("HH:mm");
            hfShiftTo1.Value = ShiftTo1.ToString("HH:mm");
            hfMachineID.Value = dt1.Rows[0]["Employee Machine Code"].ToString();
            hfTotBUtilize.Value = dt1.Rows[0]["Total Buffer Utilize"].ToString();
            hfnight.Value = dt1.Rows[0]["Night"].ToString();
            hfEmpName.Value = dt1.Rows[0]["Employee Name"].ToString();

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



            //if (duration > TimeSpan.FromMinutes(30) || durationend > TimeSpan.FromMinutes(30))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Punch correction is not allow for greater than 30 minutes.');", true);
            //    return;
            //}


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


            SqlDataAdapter daE = new SqlDataAdapter(" select [Required Punch] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [No_]='" + drpEmployee.SelectedValue + "' ", con.Con);
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
        SqlDataAdapter da1 = new SqlDataAdapter("select * from HRMSPortal.dbo.tbl_PunchCorrect_Application where convert(date,Atte_Date,103)='" + txtFromDate.Text.Trim() + "' and Userid='" + drpEmployee.SelectedValue + "' and CompanyName='" + Session["Company"].ToString() + "' and ApprovalStatus!='Rejected' ", navconn.Con);
        DataTable dtDublicate = new DataTable();
        da1.Fill(dtDublicate);

        if (dtDublicate.Rows.Count > 0)
        {
            con.DisConnect();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "myScript", "alert('Already Applied of the date');", true);
        }
        else
        {



            con.insert_tbl_PunchCorrection(drpEmployee.SelectedValue, hfEmpName.Value.ToString(), Session["Company"].ToString(), txtFromDate.Text.Trim(), txtFromTime.Text.Trim(), txtToTime.Text.Trim(), "", txtRemarks.Text.Trim(), "Present", "", System.DateTime.Now.ToString("yyyy-MM-dd"), "Pending", "ADITYA KUMAR SHARMA", "1", "Manual", "ADITYA KUMAR SHARMA", "TMU03651", txtRemarks.Text.Trim(), "TMU03651", drpEmployee.SelectedValue, txtFromTime1.Text, txtToTime1.Text);


                con2.Open();
            if (drpPunchType.SelectedValue == "0")
            {
                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_Application set Correction_Type=0 where Userid='" + drpEmployee.SelectedValue + "' and Atte_Date=convert(varchar,convert(date, '" + txtFromDate.Text.Trim() + "'), 23)  ", con2);
                cmdT.ExecuteNonQuery();
                cmdT.Dispose();
            }
            else
            {

                SqlCommand cmdT = new SqlCommand("Update tbl_PunchCorrect_Application set Correction_Type=1 where Userid='" + drpEmployee.SelectedValue + "' and Atte_Date=convert(varchar,convert(date, '" + txtFromDate.Text.Trim() + "'), 23)  ", con2);
                cmdT.ExecuteNonQuery();
                cmdT.Dispose();
            }
            con2.Close();
            lblCOSuccess.Visible = true;



            
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
   
    string hourworking = ""; string CoRemarks = ""; string useridapr = ""; string Emp_No_OD = ""; string frmmdate_OD = ""; string Todate_OD = ""; string Purpose_OD = ""; string fromtime_od = ""; string Totime_OD = ""; string Approval_Status_OD = ""; string Cfromtime_od = ""; string CTotime_OD = "";

    string Night = "";

    
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
  


    protected void drpPunchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpPunchType.SelectedValue == "0")
        {
            lblCount.Text = "0";
        }
        else
        {

            SqlDataAdapter da1 = new SqlDataAdapter("select count(*) as No from tbl_PunchCorrect_Application where Userid='" + drpEmployee.SelectedValue + "' and Month(Getdate())=Month(Atte_Date) and YEAR(Getdate())=year(Atte_Date)  and [Correction_Type]=1 ", con.Con);
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
        //Show_Report_ofuser();


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
        if (DropDownList1.SelectedValue.Trim() == "All")
        {




            SqlDataAdapter da1 = new SqlDataAdapter("select * from [tbl_PunchCorrect_Application] where convert(date,Atte_Date,103)>='" + TextBox1.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + TextBox2.Text.Trim() + "' and HODUserID='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1' and convert(date,Atte_Date,103)>'2022-03-01' ", con.Con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);


            GridView1.DataSource = dt1;
            GridView1.DataBind();

            con.DisConnect();
        }
        else
        {

            SqlDataAdapter da1 = new SqlDataAdapter("select * from [tbl_PunchCorrect_Application] where convert(date,Atte_Date,103)>='" + TextBox1.Text.Trim() + "' and convert(date,Atte_Date,103)<='" + TextBox2.Text.Trim() + "' and HODUserID='" + Session["uid"].ToString() + "' and CompanyName='" + Session["Company"].ToString() + "' and Co_Leave='1' and ApprovalStatus='" + DropDownList1.SelectedValue + "' and convert(date,Atte_Date,103)>'2022-03-01' ", con.Con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);


            GridView1.DataSource = dt1;
            GridView1.DataBind();

            con.DisConnect();
        }
    }
    protected void drpEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}