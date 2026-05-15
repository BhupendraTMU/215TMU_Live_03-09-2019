using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_FellowshipFormApproval : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        if (!IsPostBack)
        {
            if (Session["uid"].ToString() == "TMU00283" || Session["uid"].ToString() == "TMU08026")
            {
                grdfellowshipdata.Columns[3].Visible = true;
            }
            getFellowshipdata();
        }

        //catch
        //{
        //    Response.Redirect("../Default.aspx");
        //}

    }
    public void getFellowshipdata()
    {

        SqlCommand cmd = new SqlCommand("Pro_getFellowshipdata", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grdfellowshipdata.DataSource = dtCL;
        grdfellowshipdata.DataBind();


    }


    protected void Search_Click(object sender, EventArgs e)
    {
        if (txtEmployeeCode.Text != "")
        {
            SqlCommand cmd = new SqlCommand("Pro_SearchData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Employee_Code", txtEmployeeCode.Text);
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter daCL = new SqlDataAdapter(cmd);
            DataTable dtCL = new DataTable();
            daCL.Fill(dtCL);
            grdfellowshipdata.DataSource = dtCL;
            grdfellowshipdata.DataBind();
        }


    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void Btnexporttoexel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        grdfellowshipdata.RenderControl(htmlWrite);
        Response.Clear();
        string str = "FELLOWSHIPDETAIL" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    //protected void BtnRejected_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int i = 0;
    //        foreach (GridViewRow row in grdfellowshipdata.Rows)
    //        {
    //            CheckBox check = (CheckBox)row.FindControl("Chkemployee");
    //            Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
    //            var id = grdfellowshipdata.DataKeys[row.RowIndex].Value;
    //            SqlCommand cmd = new SqlCommand("pro_Rejectedfellowship", con);
    //            if (check.Checked == true)
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.AddWithValue("@ID", lblemployeecode.Text);
    //                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
    //                cmd.Parameters.AddWithValue("@Account_Approval_Status", "Rejected");
    //                cmd.Parameters.AddWithValue("@Registrar_Approval_Status", "Rejected");
    //                cmd.Parameters.AddWithValue("@Phd_Office_Approval_Status", "Rejected");
    //                cmd.Parameters.AddWithValue("@Guide_Approval_Status", "Rejected");
    //                cmd.Parameters.AddWithValue("@Principal_Approval_Status", "Rejected");
    //                cmd.Parameters.AddWithValue("@Form_Status", "Pending");
    //                con.Open();
    //                cmd.ExecuteNonQuery();
    //                con.Close();
    //                i++;
    //            }
    //        }
    //        if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Form Rejected')", true); }
    //        else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
    //        //BtnSubmit.Visible = false;
    //        getFellowshipdata();

    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    //protected void BtnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int i = 0;
    //        foreach (GridViewRow row in grdfellowshipdata.Rows)
    //        {
    //            CheckBox check = (CheckBox)row.FindControl("Chkemployee");
    //            Label lblemployeecode = (Label)row.FindControl("lblemployeecode");
    //            Label lblMonth = (Label)row.FindControl("lblMonth");
    //            Label lblYear = (Label)row.FindControl("lblYear");
    //            var id = grdfellowshipdata.DataKeys[row.RowIndex].Value;
    //            SqlCommand cmd = new SqlCommand("pro_Approvefellowship", con);
    //            if (check.Checked == true)
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.AddWithValue("@ID", lblemployeecode.Text);
    //                cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
    //                cmd.Parameters.AddWithValue("@Month", lblMonth.Text);
    //                cmd.Parameters.AddWithValue("@Year", lblYear.Text);
    //                cmd.Parameters.AddWithValue("@Account_Approval_Status", "Approved");
    //                cmd.Parameters.AddWithValue("@Registrar_Approval_Status", "Approved");
    //                cmd.Parameters.AddWithValue("@Phd_Office_Approval_Status", "Approved");
    //                cmd.Parameters.AddWithValue("@Guide_Approval_Status", "Approved");
    //                cmd.Parameters.AddWithValue("@Principal_Approval_Status", "Approved");
    //                cmd.Parameters.AddWithValue("@Form_Status", "Save");
    //                con.Open();
    //                cmd.ExecuteNonQuery();
    //                con.Close();
    //                i++;
    //            }
    //        }
    //        if (i > 0) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' Form Approved')", true); }
    //        else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Data.. !')", true); }
    //        //BtnSubmit.Visible = false;
    //        getFellowshipdata();

    //    }
    //    catch (Exception ex)
    //    {
    //    }

    //}
    protected void lnkbutton_Click(object sender, EventArgs e)
    {

        ModalPopupExtender1.Controls.Clear();


        LinkButton btn = (LinkButton)sender;
        GridViewRow grow = (GridViewRow)btn.Parent.Parent;
        Label Month = (Label)grow.FindControl("lblMonth");
        Label Year = (Label)grow.FindControl("lblYear");

        HiddenField EmployeeStatus = (HiddenField)grow.FindControl("HfEmployeeStatus");

        MonthMonthID.Value = Month.Text;
        MonthYearID.Value = Year.Text;
        if (EmployeeStatus.Value == "Approved")
        {
            btnApprove.Visible = false;
            btnReject.Visible = false;
        }
        else
        {
            btnApprove.Visible = true;
            btnReject.Visible = true;
        }
        string ID = (sender as LinkButton).CommandArgument;
        EmployeeCode.Value = ID;
        lblNotification.Text = "Employee No :-" + ID;
        Session["EmployeeCodeV"] = ID;
        ViewAttendance(ID, Month.Text, Year.Text);
        ModalPopupExtender1.Show();
    }
    public void ViewAttendance(string ID, string Month, string Year)
    {



        con.Open();
        //SqlDataAdapter daP = new SqlDataAdapter("select CONVERT(varchar(11) ,[Attendance Date],106) as [Attendance Date] ,[Week Day], (FORMAT(CAST([Shift Time In] AS DATETIME),'HH:mm')  + '   -   ' + FORMAT(CAST([Shift Time Out] AS DATETIME),'HH:mm') ) as ShiftTime, FORMAT(CAST([Time From] AS DATETIME),'HH:mm') AS [Time From],FORMAT(CAST([Time To] AS DATETIME),'HH:mm') AS [Time To],[EDUCOLLEGELIVE-R2].[dbo].DecimalToTime([Total Hours]) as WorkingHour,cast([Total Hours] as decimal(10,2)) as WorkingHour1, cast([Morning Late] as decimal(10,2)) as LateBy, cast([Early Departure in Evening] as decimal(10,2)) as EarlyBy,case when Status=1 then 'P' when Status=9 then 'A' else convert(nvarchar,Status) end as 'Status' ,FORMAT(CAST([Shift Time In] AS DATETIME),'HH:mm') as shiftTimeIn, FORMAT(CAST([Shift Time Out] AS DATETIME),'HH:mm') as ShiftTimeOut,[Total Buffer Utilize] as 'TBU',(select [Allowed Month Buffer(in Min_)] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_=t.[Employee No])-(select SUM([Total Buffer Utilize]) from  [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] where [Employee No]=t.[Employee No] and DATEPART(mm,[Attendance Date])='" + Month + "' and  DATEPART(yyyy,[Attendance Date])='" + Year + "' and [Attendance Date]<=t.[Attendance Date] ) as RB ,isnull((Select Research_Actuvity from HRMSPortal.dbo.Tbl_FellowshipActivity where Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and [Reaserch_Date]=t.[Attendance Date]),'') Research_Actuvity, isnull((Select FellowShip_Activity from HRMSPortal.dbo.Tbl_FellowshipActivity where Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and [Reaserch_Date]=t.[Attendance Date]),'') FellowShip_Activity,[Employee No] as Employee_Code from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee Actual Punch Data] t where [Employee No]='" + ID + "' and DATEPART(mm,[Attendance Date])='" + Month + "' and DATEPART(yyyy,[Attendance Date])='" + Year + "'", con);
        //DataTable dtMinuteP = new DataTable();

        //daP.Fill(dtMinuteP);

        //grd_ViewAttendance.DataSource = dtMinuteP;
        //grd_ViewAttendance.DataBind();










        SqlCommand cmd = new SqlCommand("pro_GetFellowShipDataEmployeeWise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", ID);
        cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Month", Month);
        cmd.Parameters.AddWithValue("@Year", Year);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        grd_ViewAttendance.DataSource = dtCL;
        grd_ViewAttendance.DataBind();

        con.Close();



    }
    protected void lnkAmountView_Click(object sender, EventArgs e)
    {
        GridViewDetails.Controls.Clear();


        LinkButton btn = (LinkButton)sender;
        GridViewRow grow = (GridViewRow)btn.Parent.Parent;
        Label Month = (Label)grow.FindControl("lblMonth");
        Label Year = (Label)grow.FindControl("lblYear");


        string ID = (sender as LinkButton).CommandArgument;
        con.Open();
        SqlCommand cmd = new SqlCommand("GetFellowshipamt", con1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", ID);
        cmd.Parameters.AddWithValue("@Month", Month.Text);
        cmd.Parameters.AddWithValue("@Year", Year.Text);
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter daCL = new SqlDataAdapter(cmd);
        DataTable dtCL = new DataTable();
        daCL.Fill(dtCL);
        if (dtCL.Rows.Count > 0)
        {
            if (dtCL.Rows[0]["Global Dimension 1 Code"].ToString() == "TMMC")
            {
                Label1.Text = "Faculty of Medicine, Department : " + dtCL.Rows[0]["Department Name"].ToString() + "";
                Label16.Visible = true;
                Label17.Visible = true;
                Label25.Visible = false;
                Label26.Visible = false;
            }
            else
            {
                Label16.Visible = false;
                Label17.Visible = false;
                Label25.Visible = true;
                Label26.Visible = true;
                Label1.Text = "Faculty : " + ID + "<br/>Department : " + dtCL.Rows[0]["Department Name"].ToString() + " ";
            }


            //Label2.Text = dtCL.Rows[0]["Department Name"].ToString();
            Label5.Text = dtCL.Rows[0]["LastDayDate"].ToString();
            Label9.Text = dtCL.Rows[0]["First Name"].ToString();
            Label11.Text = dtCL.Rows[0]["Department Name"].ToString();
            Label14.Text = dtCL.Rows[0]["Month Name"].ToString();
            txtnameofscholar.Text = dtCL.Rows[0]["First Name"].ToString();
            txtdateofjoining.Text = dtCL.Rows[0]["Employment Date"].ToString();
            txtnoofactualday.Text = dtCL.Rows[0]["TotalWorkingDayE"].ToString();
            txtnoofactualday.Text = dtCL.Rows[0]["TotalWorkingDayE"].ToString();
            Label15.Text = "CL-  " + dtCL.Rows[0]["CL"].ToString();
            Label18.Text = "ML-  " + dtCL.Rows[0]["ML"].ToString();
            Label19.Text = "AL-  " + dtCL.Rows[0]["AL"].ToString();
            Label20.Text = "Sundays-  " + dtCL.Rows[0]["OffDAy"].ToString();
            Label21.Text = "Holidays-  " + dtCL.Rows[0]["Holiday"].ToString();
            txtaccountNo.Text = dtCL.Rows[0]["Acc_No"].ToString();
            txtbankname.Text = dtCL.Rows[0]["Bank Name"].ToString();
            txtifsccode.Text = dtCL.Rows[0]["IFSC Code"].ToString();
            txttotalworkingdays.Text = dtCL.Rows[0]["TotalWorkingDayI"].ToString();
            Label22.Text = "Total Amount in figure-  " + dtCL.Rows[0]["Amount"].ToString();
            Label23.Text = "Total Amount in Words-  " + dtCL.Rows[0]["WAmount"].ToString();

        }



        con.Close();
        GridViewDetails.Show();
    }

    protected void grd_ViewAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtResearch = (TextBox)e.Row.FindControl("txtResearch");
            TextBox txtFellow = (TextBox)e.Row.FindControl("txtFellow");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Button btnSave = (Button)e.Row.FindControl("BtnSave");
            LinkButton lnkApprove = (LinkButton)e.Row.FindControl("lnkApprove");
            LinkButton lnkReject = (LinkButton)e.Row.FindControl("lnkReject");
            HiddenField hiddenfield = (HiddenField)e.Row.FindControl("hfdStatus");
            string Employee_Code = grd_ViewAttendance.DataKeys[e.Row.RowIndex].Values[0].ToString();
            Label lblDate = (Label)e.Row.FindControl("lblDate");




            DateTime origDT = Convert.ToDateTime(lblDate.Text);
            DateTime lastDate = new DateTime(origDT.Year, origDT.Month, 1).AddMonths(1).AddDays(-1);


            DateTime DateStartG = lastDate + new TimeSpan(0, 48, 0, 0);
            DateTime DateStartP = lastDate + new TimeSpan(0, 72, 0, 0);
            DateTime DateStartPHD = lastDate + new TimeSpan(0, 120, 0, 0);
            DateTime currentDateTime1 = DateTime.Now;
            var currentDateTime = currentDateTime1.Date;
           
           




            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            SqlDataAdapter daP = new SqlDataAdapter("Select distinct   ( Select Guide_Approval_status from HRMSPortal.dbo.Tbl_FellowshipActivity where Employee_Code='" + Employee_Code + "' and convert(date,Reaserch_Date)='" + lblDate.Text + "' ) as Guide_Approval_Status, Guide_Approval,Principal_Approval,Phd_Office_Approval,Registrar_Approval,(Select count(Registrar_Approval_Status) from HRMSPortal.dbo.Tbl_FellowshipActivity where  Employee_Code collate Latin1_General_100_CS_AS=t.Employee_Code and Month([Reaserch_Date]) =Month(t.[Reaserch_Date])  and Year([Reaserch_Date]) =Year(t.[Reaserch_Date]) and Phd_Office_Approval_Status='Pending' ) Phd_Office_Approval_Status,(Select top 1 count(Guide_Approval_Status) from HRMSPortal.dbo.Tbl_FellowshipActivity where  Employee_Code collate Latin1_General_100_CS_AS=t.[Employee_Code] and Month([Reaserch_Date]) =Month(t.[Reaserch_Date])  and Year([Reaserch_Date]) =Year(t.[Reaserch_Date]) and Principal_Approval_Status!='' ) Guide_Approval_Status,(Select count(Principal_Approval_Status) from HRMSPortal.dbo.Tbl_FellowshipActivity where  Employee_Code collate Latin1_General_100_CS_AS=t.Employee_Code and Month([Reaserch_Date]) =Month(t.[Reaserch_Date])  and Year([Reaserch_Date]) =Year(t.[Reaserch_Date])  and (Principal_Approval_Status='Pending' or Principal_Approval_Status='Approved')  ) Principal_Approval_Status,(Select count(Phd_Office_Approval_Status) from HRMSPortal.dbo.Tbl_FellowshipActivity where  Employee_Code collate Latin1_General_100_CS_AS=t.[Employee_Code] and Month([Reaserch_Date]) =Month(t.[Reaserch_Date])  and Year([Reaserch_Date]) =Year(t.[Reaserch_Date]) and Registrar_Approval_Status='Pending' ) Registrar_Approval_Status from HRMSPortal.dbo.Tbl_FellowshipActivity t where Employee_Code collate Latin1_General_100_CS_AS='" + Employee_Code + "' and Month([Reaserch_Date])=month('" + lblDate.Text + "') and YEAR([Reaserch_Date])=YEAR('" + lblDate.Text + "')", con);
            DataTable dtMinuteP = new DataTable();

            daP.Fill(dtMinuteP);

            con.Close();

          


            if (dtMinuteP.Rows[0]["Guide_Approval"].ToString() == Session["uid"].ToString() )
            {
                if (dtMinuteP.Rows[0]["Guide_Approval_Status"].ToString() == "Approved")
                {
                    lnkApprove.ForeColor = System.Drawing.Color.White;
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                if (dtMinuteP.Rows[0]["Guide_Approval_Status"].ToString() == "Rejected")
                {
                    lnkApprove.ForeColor = System.Drawing.Color.White;
                    e.Row.BackColor = System.Drawing.Color.Red;
                }


                tdApproveFinal.Visible = true;
                grd_ViewAttendance.Columns[6].Visible = true;
                tdApprove.Visible = false;

                if (dtMinuteP.Rows[0]["Guide_Approval"].ToString() == dtMinuteP.Rows[0]["Principal_Approval"].ToString() && dtMinuteP.Rows[0]["Phd_Office_Approval_Status"].ToString() != "0")
                {
                    btnFinalApproval.Visible = false;
                    lnkApprove.Enabled = false;
                    lnkReject.Enabled = false;
                }

                if (dtMinuteP.Rows[0]["Guide_Approval"].ToString() != dtMinuteP.Rows[0]["Principal_Approval"].ToString() && dtMinuteP.Rows[0]["Principal_Approval_Status"].ToString() != "0")
                {
                    btnFinalApproval.Visible = false;
                    lnkApprove.Enabled = false;
                    lnkReject.Enabled = false;
                }


            }
            if (dtMinuteP.Rows[0]["Principal_Approval"].ToString() == Session["uid"].ToString() && dtMinuteP.Rows[0]["Guide_Approval"].ToString() != dtMinuteP.Rows[0]["Principal_Approval"].ToString() )
            {

                if (dtMinuteP.Rows[0]["Guide_Approval_Status"].ToString() == "Approved")
                {
                    lnkApprove.ForeColor = System.Drawing.Color.White;
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                if (dtMinuteP.Rows[0]["Guide_Approval_Status"].ToString() == "Rejected")
                {
                    lnkApprove.ForeColor = System.Drawing.Color.White;
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
                tdApproveFinal.Visible = false;
                grd_ViewAttendance.Columns[6].Visible = false;

                if (dtMinuteP.Rows[0]["Principal_Approval_Status"].ToString() != "0")
                {
                    tdApprove.Visible = true;




                }
                else
                {
                    tdApprove.Visible = false;
                }
            }
            if (dtMinuteP.Rows[0]["Phd_Office_Approval"].ToString() == Session["uid"].ToString())
            {

                if (dtMinuteP.Rows[0]["Guide_Approval_Status"].ToString() == "Approved")
                {
                    lnkApprove.ForeColor = System.Drawing.Color.White;
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                if (dtMinuteP.Rows[0]["Guide_Approval_Status"].ToString() == "Rejected")
                {
                    lnkApprove.ForeColor = System.Drawing.Color.White;
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
                tdApproveFinal.Visible = false;
                grd_ViewAttendance.Columns[6].Visible = false;
               
                if (dtMinuteP.Rows[0]["Phd_Office_Approval_Status"].ToString() != "0")
                {
                    tdApprove.Visible = true;
                }
                else
                {
                    tdApprove.Visible = false;
                }


                }
            if (dtMinuteP.Rows[0]["Registrar_Approval"].ToString() == Session["uid"].ToString())
            {
                if (dtMinuteP.Rows[0]["Guide_Approval_Status"].ToString() == "Approved")
                {
                    lnkApprove.ForeColor = System.Drawing.Color.White;
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                if (dtMinuteP.Rows[0]["Guide_Approval_Status"].ToString() == "Rejected")
                {
                    lnkApprove.ForeColor = System.Drawing.Color.White;
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
                tdApproveFinal.Visible = false;
                grd_ViewAttendance.Columns[6].Visible = false;
                if (dtMinuteP.Rows[0]["Registrar_Approval_Status"].ToString() != "0")
                {
                    tdApprove.Visible = true;
                }
                else
                {
                    tdApprove.Visible = false;
                }
            }












            if (hiddenfield.Value == "Pending")
            {
                lnkApprove.Enabled = true;
                lnkReject.Enabled = true;
            }
            if(hiddenfield.Value == "")
            {

                lnkApprove.Enabled = false;
                lnkReject.Enabled = false;
                //lnkApprove.ForeColor = System.Drawing.Color.White;
                //e.Row.BackColor = System.Drawing.Color.Orange;
            }
            if (hiddenfield.Value == "Approved")
            {

                lnkApprove.Enabled = false;
                lnkReject.Enabled = false;
                lnkApprove.ForeColor = System.Drawing.Color.White;
                e.Row.BackColor = System.Drawing.Color.Orange;
            }
            if (hiddenfield.Value == "Rejected")
            {

                lnkApprove.Enabled = false;
                lnkReject.Enabled = false;
                lnkApprove.ForeColor = System.Drawing.Color.White;
                e.Row.BackColor = System.Drawing.Color.Red;
            }
            if (lblStatus.Text == "2")
            {

                lnkApprove.Enabled = false;
                lnkReject.Enabled = false;
                txtResearch.Enabled = false;
                txtFellow.Enabled = false;
                lblStatus.Text = "HD";
                e.Row.BackColor = System.Drawing.Color.Gray;
                e.Row.ForeColor = System.Drawing.Color.White;

            }
            if (lblStatus.Text == "3")
            {


                lblStatus.Text = "WO";
                txtResearch.Enabled = false;
                txtFellow.Enabled = false;
                lnkApprove.Enabled = false;
                lnkReject.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.Gray;
                e.Row.ForeColor = System.Drawing.Color.White;

            }
            if (lblStatus.Text == "A")
            {


                txtResearch.Enabled = false;
                txtFellow.Enabled = false;
                lnkApprove.Enabled = false;
                lnkReject.Enabled = false;
                e.Row.BackColor = System.Drawing.Color.Gray;
                e.Row.ForeColor = System.Drawing.Color.White;

            }
            //if (currentDateTime <= DateStartG)
            //{
            //    btnFinalApproval.Enabled = true;
            //}
            //else
            //{
            //    btnFinalApproval.Enabled = false;
            //}

        }



    }

    protected void lnkApprove_Click(object sender, EventArgs e)
    {

        try
        {

            LinkButton button1 = (LinkButton)sender;
            GridViewRow row = (GridViewRow)button1.Parent.Parent;

            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;




            string EmpCode = grd_ViewAttendance.DataKeys[row.RowIndex].Value.ToString();

            Label lblDate = (Label)clickedRow.FindControl("lblDate");


            SqlCommand cmd = new SqlCommand("pro_ApprovefellowshipData", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", EmpCode);
            cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Date", lblDate.Text);

            cmd.Parameters.AddWithValue("@Account_Approval_Status", "Pending");
            cmd.Parameters.AddWithValue("@Registrar_Approval_Status", "Pending");
            cmd.Parameters.AddWithValue("@Phd_Office_Approval_Status", "Pending");
            cmd.Parameters.AddWithValue("@Guide_Approval_Status", "Approved");
            cmd.Parameters.AddWithValue("@Principal_Approval_Status", "Pending");

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Form Approved')", true); 


            //getFellowshipdata();

            ModalPopupExtender1.Controls.Clear();


            DateTime dt = Convert.ToDateTime(lblDate.Text);


            string ID = (sender as LinkButton).CommandArgument;
            lblNotification.Text = "Employee No :-" + EmpCode;
            Session["EmployeeCodeV"] = EmpCode;
            ViewAttendance(EmpCode, dt.Month.ToString(), dt.Year.ToString());
            ModalPopupExtender1.Show();

        }
        catch (Exception ex)
        {
        }

    }
    protected void lnkReject_Click(object sender, EventArgs e)
    {

        try
        {

            LinkButton button1 = (LinkButton)sender;
            GridViewRow row = (GridViewRow)button1.Parent.Parent;

            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;




            string EmpCode = grd_ViewAttendance.DataKeys[row.RowIndex].Value.ToString();

            Label lblDate = (Label)clickedRow.FindControl("lblDate");


            SqlCommand cmd = new SqlCommand("pro_ApprovefellowshipDataReject", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", EmpCode);
            cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Date", lblDate.Text);

            cmd.Parameters.AddWithValue("@Account_Approval_Status", "Pending");
            cmd.Parameters.AddWithValue("@Registrar_Approval_Status", "Pending");
            cmd.Parameters.AddWithValue("@Phd_Office_Approval_Status", "Pending");
            cmd.Parameters.AddWithValue("@Guide_Approval_Status", "Approved");
            cmd.Parameters.AddWithValue("@Principal_Approval_Status", "Pending");

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Form Approved')", true); 


            //getFellowshipdata();

            ModalPopupExtender1.Controls.Clear();


            DateTime dt = Convert.ToDateTime(lblDate.Text);


            string ID = (sender as LinkButton).CommandArgument;
            lblNotification.Text = "Employee No :-" + EmpCode;
            Session["EmployeeCodeV"] = EmpCode;
            ViewAttendance(EmpCode, dt.Month.ToString(), dt.Year.ToString());
            ModalPopupExtender1.Show();

        }
        catch (Exception ex)
        {
        }
        


    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;

            string Month = MonthMonthID.Value;
            string Year = MonthYearID.Value;
            string EMPCode = EmployeeCode.Value ;
            SqlCommand cmd = new SqlCommand("pro_Approvefellowship", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", EMPCode);
            cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Form Approved')", true); 
            getFellowshipdata();

        }
        catch (Exception ex)
        {
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {

        try
        {
            int i = 0;

            string Month = MonthMonthID.Value;
            string Year = MonthYearID.Value;
            string EMPCode = EmployeeCode.Value;
            SqlCommand cmd = new SqlCommand("pro_Rejectedfellowship", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", EMPCode);
            cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Form Approved')", true);
            getFellowshipdata();

        }
        catch (Exception ex)
        {
        }

    }
    protected void btnFinalApproval_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;

            string Month = MonthMonthID.Value;
            string Year = MonthYearID.Value;
            string EMPCode = EmployeeCode.Value;
            SqlCommand cmd = new SqlCommand("pro_Approvefellowship", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", EMPCode);
            cmd.Parameters.AddWithValue("@UserID", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Form Approved')", true);
            getFellowshipdata();

        }
        catch (Exception ex)
        {
        }
    }

    protected void grdfellowshipdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdfellowshipdata.PageIndex = e.NewPageIndex;
        getFellowshipdata();
    }
  protected void grdfellowshipdata_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hiddenfield = (HiddenField)e.Row.FindControl("hfdStatusApproval");
            if (hiddenfield.Value == "Approved")
            {

                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
        }


    }
}