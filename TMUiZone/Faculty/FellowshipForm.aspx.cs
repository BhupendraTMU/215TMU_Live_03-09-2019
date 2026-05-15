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
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using WebReference;

public partial class Faculty_FellowshipForm : System.Web.UI.Page
{
    Connection con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con = new Connection();
            if (Session["uname"].ToString() == null)
            {
                Response.Redirect("../Default.aspx");

            }
            else
            {
                if (!IsPostBack)
                {


                    BindYear();
                    ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
                    ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");
                    ViewAttendance();
                    hidesubmit();
                }


            }
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
            ddlYear.Items.Add(i.ToString());
    }
    public void ViewAttendance()
    {

        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeeActualpunchData = "[" + rccname + "$Employee Actual Punch Data" + "]";

        con1.Open();
        SqlDataAdapter daP = new SqlDataAdapter("select CONVERT(varchar(11) ,[Attendance Date],106) as [Attendance Date] ,[Week Day], (FORMAT(CAST([Shift Time In] AS DATETIME),'HH:mm')  + '   -   ' + FORMAT(CAST([Shift Time Out] AS DATETIME),'HH:mm') ) as ShiftTime, FORMAT(CAST([Time From] AS DATETIME),'HH:mm') AS [Time From],FORMAT(CAST([Time To] AS DATETIME),'HH:mm') AS [Time To],[dbo].DecimalToTime([Total Hours]) as WorkingHour,cast([Total Hours] as decimal(10,2)) as WorkingHour1, cast([Morning Late] as decimal(10,2)) as LateBy, cast([Early Departure in Evening] as decimal(10,2)) as EarlyBy,case when Status=1 then 'P' when Status=9 then 'A' else convert(nvarchar,Status) end as 'Status' ,FORMAT(CAST([Shift Time In] AS DATETIME),'HH:mm') as shiftTimeIn, FORMAT(CAST([Shift Time Out] AS DATETIME),'HH:mm') as ShiftTimeOut,[Total Buffer Utilize] as 'TBU',(select [Allowed Month Buffer(in Min_)] from [TMU$Employee] where No_=t.[Employee No])-(select SUM([Total Buffer Utilize]) from  [TMU$Employee Actual Punch Data] where [Employee No]=t.[Employee No] and DATEPART(mm,[Attendance Date])='07' and  DATEPART(yyyy,[Attendance Date])='2023' and [Attendance Date]<=t.[Attendance Date] ) as RB ,isnull((Select Research_Actuvity from HRMSPortal.dbo.Tbl_FellowshipActivity where Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and [Reaserch_Date]=t.[Attendance Date]),'') Research_Actuvity, isnull((Select FellowShip_Activity from HRMSPortal.dbo.Tbl_FellowshipActivity where Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and [Reaserch_Date]=t.[Attendance Date]),'') FellowShip_Activity,isnull((Select  Form_Status from HRMSPortal.dbo.Tbl_FellowshipActivity where Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and convert(date,[Reaserch_Date])=t.[Attendance Date] and Form_Status='Save'),'') FormStatus,'Guide_Approval :- '+isnull((Select  [Guide_Approval_Status] from   HRMSPortal.dbo.Tbl_FellowshipActivity where Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and convert(date,[Reaserch_Date]) =t.[Attendance Date]   and Form_Status='Save'),'') GFormStatus,   'Principal_Approval :- '+isnull((Select  [Principal_Approval_Status] from   HRMSPortal.dbo.Tbl_FellowshipActivity where Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and convert(date,[Reaserch_Date]) =t.[Attendance Date]   and Form_Status='Save'),'') PFormStatus,   'PHD_Approval :- '+isnull((Select  [Phd_Office_Approval_Status] from   HRMSPortal.dbo.Tbl_FellowshipActivity where Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and convert(date,[Reaserch_Date]) =t.[Attendance Date]   and Form_Status='Save'),'') PhdFormStatus,  'Registrar_Approval :- '+isnull((Select  [Registrar_Approval_Status] from   HRMSPortal.dbo.Tbl_FellowshipActivity where Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and convert(date,[Reaserch_Date]) =t.[Attendance Date]   and Form_Status='Save'),'') RFormStatus,(Select top 1 count(Guide_Approval_Status) from HRMSPortal.dbo.Tbl_FellowshipActivity where  Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and Month([Reaserch_Date]) =Month(t.[Attendance Date])  and Year([Reaserch_Date]) =Year(t.[Attendance Date]) and Principal_Approval_Status!='' ) Guide_Approval_Status,(Select count(Principal_Approval_Status) from HRMSPortal.dbo.Tbl_FellowshipActivity where  Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and Month([Reaserch_Date]) =Month(t.[Attendance Date])  and Year([Reaserch_Date]) =Year(t.[Attendance Date]) and Principal_Approval_Status!='' ) Principal_Approval_Status,(Select count(Phd_Office_Approval_Status) from HRMSPortal.dbo.Tbl_FellowshipActivity where  Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and Month([Reaserch_Date]) =Month(t.[Attendance Date])  and Year([Reaserch_Date]) =Year(t.[Attendance Date]) and Phd_Office_Approval_Status!='' ) Phd_Office_Approval_Status,(Select distinct Guide_Approval from HRMSPortal.dbo.Tbl_FellowshipActivity where  Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and Month([Reaserch_Date]) =Month(t.[Attendance Date])  and Year([Reaserch_Date]) =Year(t.[Attendance Date])  ) Guide_Approval,(Select distinct Principal_Approval from HRMSPortal.dbo.Tbl_FellowshipActivity where  Employee_Code collate Latin1_General_100_CS_AS=t.[Employee No] and Month([Reaserch_Date]) =Month(t.[Attendance Date])  and Year([Reaserch_Date]) =Year(t.[Attendance Date]) ) Principal_Approval from [TMU$Employee Actual Punch Data] t where  [Employee No]='" + Session["uid"].ToString() + "' and DATEPART(mm,[Attendance Date])='" + ddlMonth.SelectedValue + "' and DATEPART(yyyy,[Attendance Date])='" + ddlYear.SelectedValue + "'", con1);
        DataTable dtMinuteP = new DataTable();

        daP.Fill(dtMinuteP);
        if (dtMinuteP.Rows.Count > 0)
        {
            grd_ViewAttendance.DataSource = dtMinuteP;
            grd_ViewAttendance.DataBind();

            


            con1.Close();
        }
        

    }
    protected void grd_ViewAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtResearch = (TextBox)e.Row.FindControl("txtResearch");
            TextBox txtFellow = (TextBox)e.Row.FindControl("txtFellow");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Button btnSave = (Button)e.Row.FindControl("BtnSave");
            HiddenField hdfFormStatus = (HiddenField)e.Row.FindControl("hdfFormStatus");
            Label lblDate = (Label)e.Row.FindControl("lblDate");
            HiddenField hdfWHour = (HiddenField)e.Row.FindControl("hdfWHour");

            HiddenField HiddenField1 = (HiddenField)e.Row.FindControl("HiddenField1");
            HiddenField HiddenField2 = (HiddenField)e.Row.FindControl("HiddenField2");
            HiddenField HiddenField3 = (HiddenField)e.Row.FindControl("HiddenField3");
            HiddenField HiddenField4 = (HiddenField)e.Row.FindControl("HiddenField4");
            HiddenField HiddenField5 = (HiddenField)e.Row.FindControl("HiddenField5");

            DateTime DateEnd =DateTime.Parse(lblDate.Text);
            DateTime DateStart = DateEnd + new TimeSpan(0, 36, 0, 0);
            DateTime currentDateTime = DateTime.Now;


            if (DateTime.Parse(lblDate.Text) <= DateTime.Parse(DateTime.Now.ToString("dd MMMM yyyy")))
            {
                if (lblStatus.Text == "2")
                {
                    //lblResearch.Enabled = false;

                    txtResearch.Enabled = false;
                    txtFellow.Enabled = false;
                    lblStatus.Text = "HD";
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    
                    btnSave.Enabled = false;
                }
                if (lblStatus.Text == "3")
                {
                    //lblResearch.Enabled = false;

                    lblStatus.Text = "WO";
                    txtResearch.Enabled = false;
                    txtFellow.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    btnSave.Enabled = false;
                }
                if (lblStatus.Text == "A")
                {
                    //lblResearch.Enabled = false;

                    txtResearch.Enabled = false;
                    txtFellow.Enabled = false;

                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    btnSave.Enabled = false;
                }
                DateTime EndTime = Convert.ToDateTime(hdfWHour.Value);
               // DateTime StartTime = Convert.ToDateTime(5);
                int durationend = EndTime.Hour;
                if (durationend < 5)
                {
                    txtResearch.Enabled = false;
                    txtFellow.Enabled = false;

                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    btnSave.Enabled = false;
                }
                if ((txtResearch.Text == "" || txtFellow.Text == "") && (lblStatus.Text != "A" && lblStatus.Text != "WO" && lblStatus.Text != "HD" && durationend > 5))
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    if (hdfFormStatus.Value.ToString() != "Save" && (lblStatus.Text != "A" && lblStatus.Text != "WO" && lblStatus.Text != "HD" && durationend > 5))
                    {
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                    }
                }
            }
            else
            {
                if (lblStatus.Text == "2")
                {
                    //lblResearch.Enabled = false;


                    lblStatus.Text = "HD";


                }
                if (lblStatus.Text == "3")
                {


                    lblStatus.Text = "WO";

                }
                btnSave.Enabled = false;
            }

            if(HiddenField4.Value== HiddenField5.Value && HiddenField3.Value!="0")
            {
                btnSave.Enabled = false;
            }

            if (HiddenField4.Value != HiddenField5.Value && HiddenField2.Value != "0")
            {
                btnSave.Enabled = false;
            }

            //if (currentDateTime <= DateStart)
            //{
            //    btnSave.Enabled = true;
            //}
            //else
            //{
            //    btnSave.Enabled = false;
            //}





            //if (DateTime.Parse(lblDate.Text) <= DateTime.Parse(DateTime.Now.ToString("dd MMMM yyyy")))
            //{
            //    if (lblStatus.Text == "HD" || lblStatus.Text == "WO")
            //    {


            //        btnSave.Enabled = false;
            //        e.Row.BackColor = System.Drawing.Color.Green;
            //        e.Row.ForeColor = System.Drawing.Color.White;
            //        //lblResearch.Enabled = true;

            //    }
            //    else
            //    {
            //        btnSave.Enabled = true;
            //    }
            //}
        }
    }
    protected void lblStatus_Click(object sender, EventArgs e)
    {

    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        ViewAttendance();
    }
    protected void lblResearch_Click(object sender, EventArgs e)
    {

    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {


        Button btn = sender as Button;
        GridViewRow grow = btn.NamingContainer as GridViewRow;


        Label lblDate = (Label)grow.FindControl("lblDate");
        Label lblWeekDay = (Label)grow.FindControl("lblWeekDay");
        Label lblShiftTime = (Label)grow.FindControl("lblShiftTime");
        TextBox txtResearch = (TextBox)grow.FindControl("txtResearch");
        Button btnSave = (Button)grow.FindControl("BtnSave");

        TextBox txtFellow = (TextBox)grow.FindControl("txtFellow");

        if (txtResearch.Text == "" || txtFellow.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill Research/Academic Activity.'); ", true);
            return;
        }

        SqlCommand cmd = new SqlCommand("Pro_Insertfellowship", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Employee_Code", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Employee_Name", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@Shift_Time", lblShiftTime.Text);
        cmd.Parameters.AddWithValue("@Research_Actuvity", txtResearch.Text);
        cmd.Parameters.AddWithValue("@FellowShip_Actuvity", txtFellow.Text);
        cmd.Parameters.AddWithValue("@Phd_Office_Approval", "TMU00283");
        cmd.Parameters.AddWithValue("@Registrar_Approval", "TMU08026");
        cmd.Parameters.AddWithValue("@Account_Approval", "TMU01023");
        cmd.Parameters.AddWithValue("@Form_Status", "Save");
        cmd.Parameters.AddWithValue("@Reaserch_Date", lblDate.Text);
        cmd.Parameters.AddWithValue("@Reaserch_Day", lblWeekDay.Text);
        cmd.Parameters.AddWithValue("@Guide_Approval_Status", "Pending");
        cmd.Parameters.AddWithValue("@Principal_Approval_Status", "");

        cmd.Parameters.AddWithValue("@Phd_Office_Approval_Status", "");
        cmd.Parameters.AddWithValue("@Registrar_Approval_Status", "");
        cmd.Parameters.AddWithValue("@Account_Approval_Status", "");
        if (con2.State == ConnectionState.Open)
        { con2.Close(); }
        con2.Open();
        cmd.ExecuteNonQuery();
        con2.Close();
        btnSave.Enabled = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your details have been Save successfully.');", true);



    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtACNo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill Account Number.'); ", true);
            return;
        }
        if (txtIfscCode.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill Ifsc Code.'); ", true);
            return;
        }
        if (txtBankName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Fill Bank Name.');", true);
            return;
        }
        SqlCommand cmd = new SqlCommand("pro_UpdateAccountDetail", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EmployeeCode", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["uname"].ToString());
        cmd.Parameters.AddWithValue("@AC_Number", txtACNo.Text);
        cmd.Parameters.AddWithValue("@IfscCode", txtIfscCode.Text);
        cmd.Parameters.AddWithValue("@BankName", txtBankName.Text);
        cmd.Parameters.AddWithValue("Update_Status", '1');
        if (con2.State == ConnectionState.Open)
        { con2.Close(); }
        con2.Open();
        cmd.ExecuteNonQuery();
        con2.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your details update successfully.'); document.location.href='FellowshipForm.aspx';", true);



    }
    public void hidesubmit()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select * from  [EDUCOLLEGELIVE-R2].dbo. tbl_FellowshipAcDetails where EmployeeCode='" + Session["uid"].ToString() + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string Update_Status = dr["Update_Status"].ToString();

                    if (Update_Status == "1")
                    {
                        btnSubmit.Visible = false;
                        txtACNo.Text = dr["AC_Number"].ToString();
                        txtBankName.Text = dr["Bank Name"].ToString();
                        txtIfscCode.Text = dr["IFSC Code"].ToString();
                        txtACNo.Enabled = false;
                        txtBankName.Enabled = false;
                        txtIfscCode.Enabled = false;
                    }
                    else
                    {
                        btnSubmit.Visible = true;

                    }
                    con.Close();

                }
            }
        }
    }
    //protected void btnFSubmit_Click(object sender, EventArgs e)
    //{

    //    string confirmValue = Request.Form["confirm_value"];

    //    string[] values = confirmValue.Split(',');
    //    string conf = values[values.Length - 1];
    //    if (conf == "Yes")
    //    {
    //        SqlCommand cmd = new SqlCommand("Pro_Updatefellowship", con2);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Employee_Code", Session["uid"].ToString());
    //        cmd.Parameters.AddWithValue("@Form_Status", "Save");
    //        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
    //        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
    //        cmd.Parameters.AddWithValue("@Guide_Approval_Status", "Pending");
    //        cmd.Parameters.AddWithValue("@Principal_Approval_Status", "Pending");
    //        cmd.Parameters.AddWithValue("@Phd_Office_Approval_Status", "Pending");
    //        cmd.Parameters.AddWithValue("@Registrar_Approval_Status", "Pending");
    //        cmd.Parameters.AddWithValue("@Account_Approval_Status", "Pending");
    //        if (con2.State == ConnectionState.Open)
    //        { con2.Close(); }
    //        con2.Open();
    //        cmd.ExecuteNonQuery();
    //        con2.Close();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your details update successfully.'); document.location.href='FellowshipForm.aspx';", true);


    //    }
    //}
}

