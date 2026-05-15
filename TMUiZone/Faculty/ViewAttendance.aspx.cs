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

public partial class Faculty_ViewAttendance : System.Web.UI.Page
{
    Connection con;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
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
                    //DataTable dtNAV = new DataTable();
                    //SqlCommand cmdNAV = new SqlCommand("Proc_GetNAVCreditionalLive", con1);
                    //cmdNAV.CommandType = CommandType.StoredProcedure;
                    //SqlDataAdapter daNAV = new SqlDataAdapter(cmdNAV);
                    //daNAV.Fill(dtNAV);
                    //VoucherPosting nvp = new VoucherPosting();
                    //nvp.UseDefaultCredentials = true;
                    //nvp.Url = dtNAV.Rows[0]["URL"].ToString();
                    //nvp.Credentials = new NetworkCredential(dtNAV.Rows[0]["UserID"].ToString(), dtNAV.Rows[0]["Password"].ToString());
                    //nvp.UpdateEmployeeAttendanceWeb(Session["uid"].ToString());

                    BindYear();
                    ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
                    ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");
                    ViewAttendance();
                }

                // show_Attendence();
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
        SqlDataAdapter daP = new SqlDataAdapter("select CONVERT(varchar(11) ,[Attendance Date],106) as [Attendance Date] ,[Week Day], (FORMAT(CAST([Shift Time In] AS DATETIME),'HH:mm')  + '   -   ' + FORMAT(CAST([Shift Time Out] AS DATETIME),'HH:mm') ) as ShiftTime, FORMAT(CAST([Time From] AS DATETIME),'HH:mm') AS [Time From],FORMAT(CAST([Time To] AS DATETIME),'HH:mm') AS [Time To],[dbo].DecimalToTime([Total Hours]) as WorkingHour,cast([Total Hours] as decimal(10,2)) as WorkingHour1, cast([Morning Late] as decimal(10,2)) as LateBy, cast([Early Departure in Evening] as decimal(10,2)) as EarlyBy,Status,FORMAT(CAST([Shift Time In] AS DATETIME),'HH:mm') as shiftTimeIn, FORMAT(CAST([Shift Time Out] AS DATETIME),'HH:mm') as ShiftTimeOut,[Total Buffer Utilize] as 'TBU',(select [Allowed Month Buffer(in Min_)] from [TMU$Employee] where No_=t.[Employee No])-(select SUM([Total Buffer Utilize]) from  [TMU$Employee Actual Punch Data] where [Employee No]=t.[Employee No] and DATEPART(mm,[Attendance Date])='" + ddlMonth.SelectedValue.Trim() + "' and  DATEPART(yyyy,[Attendance Date])='" + ddlYear.SelectedValue.Trim() + "' and [Attendance Date]<=t.[Attendance Date] ) as RB from " + tbl_EmployeeActualpunchData + " t where [Employee No]='" + Session["uid"].ToString() + "' and DATEPART(mm,[Attendance Date])='" + ddlMonth.SelectedValue + "' and DATEPART(yyyy,[Attendance Date])='" + ddlYear.SelectedValue + "'", con1);
        DataTable dtMinuteP = new DataTable();

        daP.Fill(dtMinuteP);
        //SqlDataReader dr = con.Show_EmployeeActualPunchData_viewAttendance(tbl_EmployeeActualpunchData, Session["uid"].ToString(), ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
        //DataTable dt = new DataTable();
        //dt.Load(dr);
        grd_ViewAttendance.DataSource = dtMinuteP;
        grd_ViewAttendance.DataBind();
        //dr.Close();
        //con.DisConnect();

        SqlDataAdapter da = new SqlDataAdapter("select (select [Allowed Month Buffer(in Min_)] from [TMU$Employee] where No_=t.[Employee No])-  isnull(sum([Total Buffer Utilize]),0) Total from [TMU$Employee Actual Punch Data] t where [Employee No]='" + Session["uid"].ToString() + "' and month([Attendance Date])='" + ddlMonth.SelectedValue + "' and YEAR([Attendance Date])='" + ddlYear.SelectedValue + "'  group by [Employee No] ", con1);
        DataTable dtMinute = new DataTable();

        da.Fill(dtMinute);
        if (dtMinute.Rows.Count > 0)
        {
            lblMinutes.Text = Convert.ToInt32(dtMinute.Rows[0]["Total"]).ToString();
        }
        else
        {
            lblMinutes.Text = "120";
        }
        con1.Close();

    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        ViewAttendance();
    }
    protected void grd_ViewAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblStatus = (Label)e.Row.FindControl("lblStatus");

            //Label lblLateEarlyBy = (Label)e.Row.FindControl("lblLateEarlyBy");
            //Label lblLateBY_GRID = (Label)e.Row.FindControl("lblLateBY_GRID");

            string lblLateEarlyBy = e.Row.Cells[6].Text.Trim();


            decimal lblLateEarlyBy1 = Convert.ToDecimal(lblLateEarlyBy.Trim());
            string lblLateEarlyBy2 = lblLateEarlyBy1.ToString("00");



            int min = int.Parse(lblLateEarlyBy2);
            int hr = (min / 60);
            int hr1 = (min % 60);

            string str = hr.ToString() + ":" + hr1.ToString();
            e.Row.Cells[6].Text = str;


            ///

            string lblLateBY_GRID = e.Row.Cells[7].Text.Trim();


            decimal lblLateBY_GRID1 = Convert.ToDecimal(lblLateBY_GRID.Trim());
            string lblLateBY_GRID2 = lblLateBY_GRID1.ToString("00");



            int min1 = int.Parse(lblLateBY_GRID2);
            int hr3 = (min1 / 60);
            int hr11 = (min1 % 60);

            string str12 = hr3.ToString() + ":" + hr11.ToString();
            e.Row.Cells[7].Text = str12;

            if (lblStatus.Text == "1")
            {
                // lblStatus.Text = "PP";
                string od = "", WFH = "", MP = "";
                e.Row.BackColor = System.Drawing.Color.DarkSeaGreen;
                e.Row.ForeColor = System.Drawing.Color.Black;
                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    od = dr["OD"].ToString();
                    WFH = dr["Mark WFH"].ToString();
                    MP = dr["Mark Present"].ToString();
                }
                dr.Close();
                con.DisConnect();

                if (od == "1" && WFH == "1" && MP == "0")
                {
                    lblStatus.Text = "WFH";
                    //lblStatus.Text = "OD";
                }
                if (od == "1" && WFH == "0" && MP == "1")
                {
                    lblStatus.Text = "PP";
                    //lblStatus.Text = "OD";
                }
                if (od == "1" && WFH == "0" && MP == "0")
                {
                    lblStatus.Text = "OD";
                    //lblStatus.Text = "OD";
                }
                if (od == "0")
                {
                    lblStatus.Text = "PP";
                }
            }
            if (lblStatus.Text == "2")
            {
                lblStatus.Text = "HD";
                e.Row.BackColor = System.Drawing.Color.Gray;
                e.Row.ForeColor = System.Drawing.Color.White;
            }
            if (lblStatus.Text == "3")
            {
                lblStatus.Text = "WO";
                e.Row.BackColor = System.Drawing.Color.Gray;
                e.Row.ForeColor = System.Drawing.Color.White;
            }
            if (lblStatus.Text == "4")
            {
                lblStatus.Text = "Leave";
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;
                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    string leavecode = dr["Leave Code"].ToString();
                    string halfday = dr["Leave Type"].ToString();
                    if (halfday == "2")
                    {
                        halfday = leavecode + "(" + "Half" + ")";
                    }

                    if (halfday == "3")
                    {
                        halfday = leavecode;
                    }
                    lblStatus.Text = halfday.ToString();


                }
                dr.Close();
                con.DisConnect();
            }
            if (lblStatus.Text == "5")
            {
                lblStatus.Text = "LWP(Half-Day)";
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;

                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    string leavecode = dr["Leave Code"].ToString();

                    //string halfday = dr["Half Day Leave Type"].ToString();

                    //lblStatus.Text = ("LWP-" + leavecode + "/" + halfday).ToString();

                    if (leavecode.Trim() == "")
                    {
                        lblStatus.Text = "LWPH";
                    }
                }
                dr.Close();
                con.DisConnect();
            }

            if (lblStatus.Text == "6")
            {
                lblStatus.Text = "LWP(Full-Day)";
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;
                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    lblStatus.Text = dr["Leave Code"].ToString();


                    if (lblStatus.Text.Trim() == "")
                    {
                        lblStatus.Text = "LWPF";
                    }
                }
                dr.Close();
                con.DisConnect();
            }

            if (lblStatus.Text == "7")
            {
                // lblStatus.Text = "Special Leave";
                lblStatus.Text = "LWP-Leave";
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;
                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    //lblStatus.Text = dr["Leave Code"].ToString();
                    //if (lblStatus.Text.Trim() == "")
                    //{
                    //    lblStatus.Text = "SP";
                    //}

                    string leavecode = dr["Leave Code"].ToString();
                    //string halfday = dr["Leave Type"].ToString();

                    lblStatus.Text = ("LWP-" + leavecode).ToString();


                }
                dr.Close();
                con.DisConnect();
            }
            if (lblStatus.Text == "8")
            {
                // lblStatus.Text = "Absent(Half-Day)"; Discussion
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;


                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    lblStatus.Text = dr["Leave Code"].ToString();
                    if (lblStatus.Text.Trim() == "")
                    {
                        lblStatus.Text = "AHD";
                    }
                }
                dr.Close();
                con.DisConnect();
            }

            if (lblStatus.Text == "9")
            {
                //  lblStatus.Text = "Absent(Full-Day)"; discussion
                //lblStatus.Text = "AA";
                e.Row.BackColor = System.Drawing.Color.Salmon;
                e.Row.ForeColor = System.Drawing.Color.White;

                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[0].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["uid"].ToString(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
                    lblStatus.Text = dr["Leave Code"].ToString();
                    if (lblStatus.Text.Trim() == "")
                    {
                        lblStatus.Text = "AA";
                    }
                }
                dr.Close();
                con.DisConnect();
            }

            //decimal cel6 =Convert.ToDecimal(e.Row.Cells[6].Text);
            //decimal cel7 = Convert.ToDecimal(e.Row.Cells[7].Text);
            string cel6 = e.Row.Cells[6].Text;
            string cel7 = e.Row.Cells[7].Text;
            if (cel6 != "0:0")
            {
                e.Row.Cells[6].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Black;
            }

            if (cel7 != "0:0")
            {
                e.Row.Cells[7].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Black;
            }
            //if (cel6 > cel7)
            //{
            //    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
            //    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;

            //}

            //if (cel6 < cel7)
            //{

            //    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
            //    e.Row.Cells[6].ForeColor = System.Drawing.Color.White;

            //}


        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        grd_ViewAttendance.RenderControl(htmlWrite);

        Response.Clear();
        string str = "ActualAttendance" + Session["uid"].ToString(); ;
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();

    }
}