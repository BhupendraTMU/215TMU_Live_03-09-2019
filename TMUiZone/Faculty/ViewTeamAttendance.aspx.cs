using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

public partial class Faculty_ViewAttendance : System.Web.UI.Page
{
    Connection con;
    SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["strPortal"]);
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
                    showHRHODisexhist();
                    BindYear();
                    ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
                    ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");
                    ShowEMployeeDetails();
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





    public void showHRHODisexhist()
    {

        string s = "";
        if (Session["uid"].ToString() == "TMU06860")
        {
             s = "select No_,Case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorisedID' from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [Global Dimension 1 Code]='TMDC' and Status=0";
        }
        else
        {
             s = "Select * from (select No_,Case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorisedID' from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] ) T where T.AuthorisedID='" + Session["uid"].ToString() + "'";
        }
        SqlCommand cmd = new SqlCommand(s, Conn);
        if (Conn != null && Conn.State == ConnectionState.Closed)
        {
            Conn.Open();
        }
        SqlDataReader dr = cmd.ExecuteReader();
     
        dr.Read();
        if (dr.HasRows)
        {
            dr.Close();
            con.DisConnect();


        }
        else
        {
            dr.Close();
            con.DisConnect();
            Response.Redirect("Default.aspx");
        }
        Conn.Close();
    }
    SqlDataReader dr;
    public void ViewAttendance()
    {

        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeeActualpunchData = "[" + rccname + "$Employee Actual Punch Data" + "]";
        //string PayCompanyPolicy = "[" + rccname + "$Pay Company Policy" + "]";

        //SqlDataReader drpaymonth = con.Show_PayCompanyPolicy(PayCompanyPolicy);
        //drpaymonth.Read();

        //if (drpaymonth.HasRows)
        //{

        //    string paymont =Convert.ToDateTime(drpaymonth["Payroll Processing Month Date"].ToString()).ToString("yyyyMM");

        //    int paymont1 = Convert.ToInt32(paymont);
        //    int filtermonthyrs=Convert.ToInt32(ddlYear.Text.Trim()+ddlMonth.SelectedValue.Trim());

        //    drpaymonth.Close();

        //    if (paymont1 < filtermonthyrs)
        //    {
        //        // From Actual punch data
        //        dr = con.Show_EmployeeActualPunchData_viewAttendance(tbl_EmployeeActualpunchData, txtUserid.SelectedValue.Trim(), ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
        //    }
        //    else
        //    {

        //        // from Pay Daily Attendance Details
        //        dr = con.Show_EmployeeActualPunchData_viewAttendance(tbl_EmployeeActualpunchData, txtUserid.SelectedValue.Trim(), ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
        //    }
        //}

        //else
        //{
        //    drpaymonth.Close();        
        //}

        dr = con.Show_EmployeeActualPunchData_viewAttendance(tbl_EmployeeActualpunchData, txtUserid.SelectedValue.Trim(), ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        if (dt.Rows.Count > 0)
        {
            GridView1.Visible = false;
            grd_ViewAttendance.Visible = true;
            grd_ViewAttendance.DataSource = dt;
            grd_ViewAttendance.DataBind();
        }
        else
        {
            GridView1.Visible = true;
            grd_ViewAttendance.Visible = false;
        }
        dr.Close();
        con.DisConnect();

    }

    private void ShowEMployeeDetails()
    {

        DataTable dt = new DataTable();
        string id = string.Empty;
        string fname = string.Empty;
        string sname = string.Empty;
        string lname = string.Empty;

        string newName = string.Empty;
        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeeActualpunchData = "[" + rccname + "$Employee" + "]";
        string sqlStatement = "";
        if (Session["uid"].ToString() == "TMU06860")
        {
            sqlStatement = "SELECT No_,[First Name],[Middle Name],[Last Name],Case When [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorizedID'  FROM [TMU$Employee] with(NOLOCK)  where [Status]='0' and [Global Dimension 1 Code]='TMDC' order by [First Name]";
        }
        else if(Session["uid"].ToString() == "TMU04173")
        {
            sqlStatement = "SELECT No_,[First Name],[Middle Name],[Last Name],Case When [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorizedID'  FROM [TMU$Employee] with(NOLOCK) where [Status]='0' and [Global Dimension 1 Code]='TMHS' order by [First Name]";
        }
        else
        {
            sqlStatement = "Select No_,[First Name],[Middle Name],[Last Name] from (SELECT *,Case When [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorizedID'  FROM [TMU$Employee] with(NOLOCK) where [Status]='0') T where T.AuthorizedID='" + Session["uid"].ToString() + "' order by [First Name] ";
        }



      
        

        SqlCommand sqlCmd = new SqlCommand(sqlStatement, con.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["No_"].ToString();

                fname = dt.Rows[i]["First Name"].ToString();
                sname = dt.Rows[i]["Middle Name"].ToString();
                lname = dt.Rows[i]["Last Name"].ToString();
                newName = id + "  " + fname + "    " + lname + " ";

                txtUserid.Items.Add(new ListItem(newName, id));

                txtUserid.SelectedValue = id;
            }
        }

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

            string lblLateEarlyBy = e.Row.Cells[6].Text.Trim();


            decimal lblLateEarlyBy1 = Convert.ToDecimal(lblLateEarlyBy.Trim());
            string lblLateEarlyBy2 = lblLateEarlyBy1.ToString("00");



            int min = int.Parse(lblLateEarlyBy2);
            int hr = (min / 60);
            int hr1 = (min % 60);

            string str = hr.ToString() + ":" + hr1.ToString();
            e.Row.Cells[6].Text = str;

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

                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
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
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
        string str = ""; 
        if (GridView1.Visible == true)
        {
            GridView1.RenderControl(htmlWrite);
            str = "ConsolidateAttendance" ;
           
        }
        else
        {
            grd_ViewAttendance.RenderControl(htmlWrite);
            str = "ActualAttendance" + Session["uid"].ToString();
        }

        Response.Clear();
       
        Response.AddHeader("content-disposition", "attachment;filename=" + str + "" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();

    }


    protected void btnConsolidate_Click(object sender, EventArgs e)
    {

        ConsolidateAttendance();

    }
    
    public void ConsolidateAttendance()
    {

        
        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeeActualpunchData = "[" + rccname + "$Employee Actual Punch Data" + "]";
        


       string sqlStatement = "select [Employee No],[Employee Name],CONVERT(varchar(11) ,[Attendance Date],106) as [Attendance Date] ,[Week Day], (FORMAT(CAST([Shift Time In] AS DATETIME),'HH:mm')  + '   -   ' + FORMAT(CAST([Shift Time Out] AS DATETIME),'HH:mm') ) as ShiftTime, FORMAT(CAST([Time From] AS DATETIME),'HH:mm') AS [Time From],FORMAT(CAST([Time To] AS DATETIME),'HH:mm') AS [Time To],[dbo].DecimalToTime([Total Hours]) as WorkingHour,cast([Total Hours] as decimal(10,2)) as WorkingHour1, cast([Morning Late] as decimal(10,2)) as LateBy, cast([Early Departure in Evening] as decimal(10,2)) as EarlyBy,Status,FORMAT(CAST([Shift Time In] AS DATETIME),'HH:mm') as shiftTimeIn, FORMAT(CAST([Shift Time Out] AS DATETIME),'HH:mm') as ShiftTimeOut from " + tbl_EmployeeActualpunchData + " where [Employee No] in (Select No_ from(SELECT *, Case When[Sanctioning Incharge] = '' then HOD else [Sanctioning Incharge] end as 'AuthorizedID'  FROM[TMU$Employee] with(NOLOCK) where[Status] = '0') T where T.AuthorizedID = '"+ Session["uid"].ToString() + "' and T.No_ not like 'TMSS%') and DATEPART(mm,[Attendance Date])='" + ddlMonth.SelectedValue + "' and DATEPART(yyyy,[Attendance Date])='" + ddlYear.SelectedValue + "' order by [Employee Name]";
        
        SqlCommand sqlCmd = new SqlCommand(sqlStatement, con.Con);
        SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
        DataTable dt = new DataTable();
        sqlDa.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.Visible = true;
            grd_ViewAttendance.Visible = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        else
        {
            GridView1.Visible = false;
            grd_ViewAttendance.Visible = true;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblStatus = (Label)e.Row.FindControl("lblStatus");

            string lblLateEarlyBy = e.Row.Cells[8].Text.Trim();


            decimal lblLateEarlyBy1 = Convert.ToDecimal(lblLateEarlyBy.Trim());
            string lblLateEarlyBy2 = lblLateEarlyBy1.ToString("00");



            int min = int.Parse(lblLateEarlyBy2);
            int hr = (min / 60);
            int hr1 = (min % 60);

            string str = hr.ToString() + ":" + hr1.ToString();
            e.Row.Cells[6].Text = str;

            string lblLateBY_GRID = e.Row.Cells[9].Text.Trim();


            decimal lblLateBY_GRID1 = Convert.ToDecimal(lblLateBY_GRID.Trim());
            string lblLateBY_GRID2 = lblLateBY_GRID1.ToString("00");



            int min1 = int.Parse(lblLateBY_GRID2);
            int hr3 = (min1 / 60);
            int hr11 = (min1 % 60);

            string str12 = hr3.ToString() + ":" + hr11.ToString();
            e.Row.Cells[9].Text = str12;

            if (lblStatus.Text == "1")
            {
                // lblStatus.Text = "PP";
                string od = "", WFH = "", MP = "";
                e.Row.BackColor = System.Drawing.Color.DarkSeaGreen;
                e.Row.ForeColor = System.Drawing.Color.Black;
                string ccnameUN = Session["Company"].ToString();
                string rccname = ccnameUN.Replace(".", "_");
                string tble_paydailyAtten = "[" + rccname + "$Pay Daily Attendence Detail" + "]";
                string attendaye = e.Row.Cells[2].Text;

                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
                string attendaye = e.Row.Cells[2].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
                string attendaye = e.Row.Cells[2].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
                string attendaye = e.Row.Cells[2].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
                string attendaye = e.Row.Cells[2].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
                dr.Read();
                if (dr.HasRows)
                {
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
                string attendaye = e.Row.Cells[2].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
                string attendaye = e.Row.Cells[2].Text;
                SqlDataReader dr = con.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, txtUserid.SelectedValue.Trim(), attendaye);
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
            string cel6 = e.Row.Cells[8].Text;
            string cel7 = e.Row.Cells[9].Text;
            if (cel6 != "0:0")
            {
                e.Row.Cells[8].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[8].ForeColor = System.Drawing.Color.Black;
            }

            if (cel7 != "0:0")
            {
                e.Row.Cells[9].BackColor = System.Drawing.Color.Pink;
                e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
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
}