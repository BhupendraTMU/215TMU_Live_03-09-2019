using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Collections;
//using System.Transactions;

public partial class Faculty_Employee_Verification : System.Web.UI.Page
{
    Connection Portalcon;
    ServicePoratal con;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Portalcon = new Connection();
            con = new ServicePoratal();
            if (!IsPostBack)
            {
                string query = "SELECT  YEAR([Payroll Processing Month Date]) AS [Year],    DATENAME(MONTH, [Payroll Processing Month Date]) AS [Month] FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Pay Company Policy];";
                SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.DisConnect();
                Label1.Text = "Employee Verification for Month " + dt.Rows[0]["Month"].ToString() + " - " + dt.Rows[0]["year"].ToString() + "";
                DateTime today = DateTime.Today;


               

                int day = today.Day;

                bool isAllowed = (day >= 20) || (day <= 7);

                if (isAllowed)
                {
                    btnsubmit.Visible = true;
                }
                else
                {
                    btnsubmit.Visible = false;

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "dateAlert",
                        "alert('This page is accessible only from 20th of the current month to 5th of the next month.');",
                        true
                    );
                }

                if (Session["uname"].ToString() == null)
                {
                    Response.Redirect("../Default.aspx");
                }
                else
                {


                    
                    bindDesignation();
                    bindDepartment();
                    ShowEMployeeDetails();
                    BindYear();
                    ddlMonth.SelectedValue = System.DateTime.Now.ToString("MM");
                    ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");
                    DropDownList1.SelectedValue = System.DateTime.Now.ToString("MM");


                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void bindDesignation()
    {
        string query = "";
        if (Session["GlobalDimension1Code"].ToString() == "TMMC" || Session["GlobalDimension1Code"].ToString() == "TMHS")
        {
            query = "Select distinct [Designation Code], [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code] in (Select [Designation Code] from (select distinct [Designation Code],case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthID' FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [Status]='0') T where T.AuthID ='" + Session["uid"].ToString() + "')";
        }
        else
        {
            query = "Select distinct [Designation Code], [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master] where [Designation Code] in (select distinct [Designation Code] FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [Status]='0' and  HOD ='" + Session["uid"].ToString() + "')";

        }

        SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
        DataTable dt = new DataTable();

        da.Fill(dt);
        con.DisConnect();
        drpDesignation.DataSource = dt;
        drpDesignation.DataTextField = "Designation Description";
        drpDesignation.DataValueField = "Designation Code";
        drpDesignation.DataBind();
        drpDesignation.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    public void bindDepartment()
    {
        string query = "";
        if (Session["GlobalDimension1Code"].ToString() == "TMMC" || Session["GlobalDimension1Code"].ToString() == "TMHS")
        {
            query = "Select [Department Name] from (select distinct [Department Name],case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthID' FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [Status]='0')T where T.AuthID ='" + Session["uid"].ToString() + "'";
        }
        else
        {
            query = "select distinct [Department Name] FROM [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where [Status]='0' and HOD ='" + Session["uid"].ToString() + "'";
        }


        SqlDataAdapter da = new SqlDataAdapter(query, con.Con);
        DataTable dt = new DataTable();

        da.Fill(dt);
        con.DisConnect();
        drpDepartment.DataSource = dt;
        drpDepartment.DataTextField = "Department Name";
        drpDepartment.DataValueField = "Department Name";
        drpDepartment.DataBind();
        drpDepartment.Items.Insert(0, new ListItem("--Select--", "0"));

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

        if (Session["GlobalDimension1Code"].ToString() == "TMMC" || Session["GlobalDimension1Code"].ToString() == "TMHS")
        {
            string sqlStatement = "";
            if (drpDesignation.SelectedValue != "0")
            {
                sqlStatement = "select *,'Approved' PMSStatus from (select *,(Select Name from [TMU$Dimension Value]   where [Code]=[TMU$Employee].[Global Dimension 2 Code]) 'Dept',  [Reporting Incharge Name] as 'Reporting',[Reporting Incharge Name] as 'Deputed' ,  isnull((select top 1 Remark from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail]  where EmployeeCode collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and   [month] =(select Month([Payroll Processing Month Date])  from [TMU$Pay Company Policy])   and [Year] =(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy])   order by Id desc),'') as 'Remark' ,  isnull((select top 1 [Hold the Salary]   from [TMU$Pay Monthly Attendence Detail] where [Employee No]   collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and [Month] =  (select Month([Payroll Processing Month Date])  from [TMU$Pay Company Policy]) and [Year]=(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy])),'')     as 'Salary Hold',(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master]   where [Designation Code]=[TMU$Employee].[Designation Code])   as Designation,case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorisedID' from [TMU$Employee] where [Status]=0 and [Payroll]!=1) T where AuthorisedID='" + Session["uid"].ToString() + "' and   T.[Designation]='" + drpDesignation.SelectedItem.Text + "' and No_  like 'TMU%'";

            }
            if (drpDepartment.SelectedValue != "0")
            {
                sqlStatement = "select *,'Approved' PMSStatus from (select *,(Select Name from [TMU$Dimension Value]   where [Code]=[TMU$Employee].[Global Dimension 2 Code]) 'Dept',  [Reporting Incharge Name] as 'Reporting',[Reporting Incharge Name] as 'Deputed' ,  isnull((select top 1 Remark from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail]  where EmployeeCode collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and   [month] =(select Month([Payroll Processing Month Date])  from [TMU$Pay Company Policy])   and [Year] =(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy])   order by Id desc),'') as 'Remark' ,  isnull((select top 1 [Hold the Salary]   from [TMU$Pay Monthly Attendence Detail] where [Employee No]   collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and [Month] =  (select Month([Payroll Processing Month Date])  from [TMU$Pay Company Policy]) and [Year]=(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy])),'')     as 'Salary Hold',(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master]   where [Designation Code]=[TMU$Employee].[Designation Code])   as Designation,case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorisedID' from [TMU$Employee] where [Status]=0 and [Payroll]!=1) T where AuthorisedID='" + Session["uid"].ToString() + "' and   T.[Dept]='" + drpDepartment.SelectedValue + "'  and No_  like 'TMU%' ";



            }
            if (drpDepartment.SelectedValue != "0" && drpDesignation.SelectedValue != "0")
            {


                sqlStatement = "select *,'Approved' PMSStatus from (select *,(Select Name from [TMU$Dimension Value]   where [Code]=[TMU$Employee].[Global Dimension 2 Code]) 'Dept',  [Reporting Incharge Name] as 'Reporting',[Reporting Incharge Name] as 'Deputed' ,  isnull((select top 1 Remark from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail]  where EmployeeCode collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and   [month] =(select Month([Payroll Processing Month Date])  from [TMU$Pay Company Policy])   and [Year] =(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy])   order by Id desc),'') as 'Remark' ,  isnull((select top 1 [Hold the Salary]   from [TMU$Pay Monthly Attendence Detail] where [Employee No]   collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and [Month] =  (select Month([Payroll Processing Month Date])  from [TMU$Pay Company Policy]) and [Year]=(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy])),'')     as 'Salary Hold',(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master]   where [Designation Code]=[TMU$Employee].[Designation Code])   as Designation,case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorisedID' from [TMU$Employee] where [Status]=0 and [Payroll]!=1) T where AuthorisedID='" + Session["uid"].ToString() + "' and   T.[Dept]='" + drpDepartment.SelectedValue + "' and T.[Designation]='" + drpDesignation.SelectedItem.Text + "' and No_  like 'TMU%'";



            }
            if (drpDepartment.SelectedValue == "0" && drpDesignation.SelectedValue == "0")
            {
                sqlStatement = "select *,'Approved' PMSStatus from (select *,(Select Name from [TMU$Dimension Value]   where [Code]=[TMU$Employee].[Global Dimension 2 Code]) 'Dept',  [Reporting Incharge Name] as 'Reporting',[Reporting Incharge Name] as 'Deputed' ,  isnull((select top 1 Remark from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail]  where EmployeeCode collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and   [month] =(select Month([Payroll Processing Month Date])  from [TMU$Pay Company Policy])   and [Year] =(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy])   order by Id desc),'') as 'Remark' ,  isnull((select top 1 [Hold the Salary]   from [TMU$Pay Monthly Attendence Detail] where [Employee No]   collate Latin1_General_100_CS_AS =[TMU$Employee].No_ and [Month] =  (select Month([Payroll Processing Month Date])  from [TMU$Pay Company Policy]) and [Year]=(select Year([Payroll Processing Month Date])  from [TMU$Pay Company Policy])),'')     as 'Salary Hold',(Select top 1 [Designation Description] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Designation Master]   where [Designation Code]=[TMU$Employee].[Designation Code])   as Designation,case when [Sanctioning Incharge]='' then HOD else [Sanctioning Incharge] end as 'AuthorisedID' from [TMU$Employee] where [Status]=0 and [Payroll]!=1) T where AuthorisedID='" + Session["uid"].ToString() + "' and No_  like 'TMU%'";


            }

            SqlCommand sqlCmd = new SqlCommand(sqlStatement, Portalcon.Con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grdEmployee.DataSource = dt;
                grdEmployee.DataBind();
                //btnsubmit.Visible = true;
            }
            else
            {
                grdEmployee.DataSource = "";
                grdEmployee.DataBind();
                //btnsubmit.Visible = false;
            }
        }
        else
        {
            string sqlStatement = "";
            if (drpDesignation.SelectedValue != "0")
            {

                SqlCommand cmd = new SqlCommand("sp_Verify_Employee_designWise", Portalcon.Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@HODID", Session["uid"].ToString());
                cmd.Parameters.Add("@designation", drpDesignation.SelectedItem.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);               
            }
            if (drpDepartment.SelectedValue != "0")
            {
                SqlCommand cmd = new SqlCommand("sp_Verify_Employee_deptWise", Portalcon.Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@HODID", Session["uid"].ToString());
                cmd.Parameters.Add("@dept", drpDepartment.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            if (drpDepartment.SelectedValue != "0" && drpDesignation.SelectedValue != "0")
            {
                SqlCommand cmd = new SqlCommand("sp_Verify_Employee_dept_desig_Wise", Portalcon.Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@HODID", Session["uid"].ToString());
                cmd.Parameters.Add("@dept", drpDepartment.SelectedValue);
                cmd.Parameters.Add("@designation", drpDesignation.SelectedItem.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            if (drpDepartment.SelectedValue == "0" && drpDesignation.SelectedValue == "0")
            {
                SqlCommand cmd = new SqlCommand("sp_Verify_Employee", Portalcon.Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@HODID", Session["uid"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }           
            if (dt.Rows.Count > 0)
            {
                grdEmployee.DataSource = dt;
                grdEmployee.DataBind();
                //btnsubmit.Visible = true;
            }
            else
            {
                grdEmployee.DataSource = "";
                grdEmployee.DataBind();
                //btnsubmit.Visible = false;
            }
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Controls.Clear();
        grd_ViewAttendance.Visible = false;
        string ID = (sender as LinkButton).CommandArgument;
        lblNotification.Text = "Employee No :-" + ID;
        Session["EmployeeCodeV"] = ID;
        ViewAttendance(ID);
        ModalPopupExtender1.Show();

    }

    public void BindYear()
    {

        int currentYear = DateTime.Now.Year;

        ddlYear.Items.Clear();
        ddlYear1.Items.Clear();

        for (int year = currentYear - 1; year <= currentYear; year++)
        {
            ddlYear1.Items.Add(year.ToString());
            ddlYear.Items.Add(year.ToString());
        }

        ddlYear1.SelectedValue = currentYear.ToString();
        ddlYear.SelectedValue = currentYear.ToString();

    }
    public void ViewAttendance(string ID)
    {

        string ccnameUN = Session["Company"].ToString();
        string rccname = ccnameUN.Replace(".", "_");
        string tbl_EmployeeActualpunchData = "[" + rccname + "$Employee Actual Punch Data" + "]";

        SqlDataReader dr = Portalcon.Show_EmployeeActualPunchData_viewAttendance(tbl_EmployeeActualpunchData, ID, ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
        DataTable dt = new DataTable();
        dt.Load(dr);
        grd_ViewAttendance.DataSource = dt;
        grd_ViewAttendance.DataBind();
        dr.Close();
        con.DisConnect();



    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        grd_ViewAttendance.Visible = true;
        ViewAttendance(Session["EmployeeCodeV"].ToString());
        ModalPopupExtender1.Show();
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
                SqlDataReader dr = Portalcon.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["EmployeeCodeV"].ToString(), attendaye);
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
                SqlDataReader dr = Portalcon.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["EmployeeCodeV"].ToString(), attendaye);
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
                SqlDataReader dr = Portalcon.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["EmployeeCodeV"].ToString(), attendaye);
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
                SqlDataReader dr = Portalcon.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["EmployeeCodeV"].ToString(), attendaye);
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
                SqlDataReader dr = Portalcon.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["EmployeeCodeV"].ToString(), attendaye);
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
                SqlDataReader dr = Portalcon.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["EmployeeCodeV"].ToString(), attendaye);
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
                SqlDataReader dr = Portalcon.Show_PayDailyAttendance_viewAttendance(tble_paydailyAtten, Session["EmployeeCodeV"].ToString(), attendaye);
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

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdEmployee.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("CHKHold") as CheckBox);
                CheckBox chkRow1 = (row.Cells[0].FindControl("CHKVerify") as CheckBox);
                //DropDownList dropRemark = (row.Cells[0].FindControl("drpRemark") as DropDownList);
                Label lblEmployee = (row.Cells[0].FindControl("EmpCode") as Label);
                //SqlCommand cmd = new SqlCommand("HRMSPortal.dbo.sp_GetAttachmentCountfor_PMS", Portalcon.Con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@EmployeeID", Session["uid"].ToString());
                //cmd.Parameters.Add("@UserID", lblEmployee.Text);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();               
                //da.Fill(dt);
                //if(Convert.ToInt32(dt.Rows[0]["C"])<17)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Monthly PMS activity is pending for employee :-"+ lblEmployee.Text + "');", true);
                //    return;
                //}
                if (chkRow.Checked == false && chkRow1.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Either Verify Employee or Select Salary Hold');", true);
                    return;
                }
                //if (chkRow.Checked == true && txtRemark.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Remark is Mandatory for Salary Hold.');", true);
                //    return;
                //}


            }
        }
        SqlConnection Con2 = new SqlConnection(ConfigurationManager.AppSettings["str"]);

        //using (TransactionScope scope = new TransactionScope())
        //{
        try
        {
            List<string> notApprovedEmpCodes = new List<string>();
            foreach (GridViewRow row in grdEmployee.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox chkHold = (row.Cells[0].FindControl("CHKHold") as CheckBox);
                    CheckBox CHKVerify = (row.Cells[0].FindControl("CHKVerify") as CheckBox);
                    DropDownList dropRemark = (row.Cells[0].FindControl("drpRemark") as DropDownList);
                    Label lblUserID = (row.Cells[0].FindControl("EmpCode") as Label);
                    Label PMSStatus = (row.Cells[0].FindControl("lblPMSStatus") as Label);

                    if (PMSStatus.Text == "Approved")
                    {
                        SqlCommand cmd = new SqlCommand("UpdateEmployeeSalary", Con2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", lblUserID.Text);
                        if (chkHold.Checked == true)
                        {
                            cmd.Parameters.Add("@Hold", "1");
                        }
                        else
                        {
                            cmd.Parameters.Add("@Hold", "0");
                        }
                        if (CHKVerify.Checked == true)
                        {
                            cmd.Parameters.Add("@Verify", "1");
                        }
                        else
                        {
                            cmd.Parameters.Add("@Verify", "0");
                        }
                        cmd.Parameters.Add("@Remark", dropRemark.SelectedItem.Text);
                        cmd.Parameters.Add("@HOD", Session["uid"].ToString());
                        if (Con2.State == ConnectionState.Closed)
                            Con2.Open();
                        cmd.ExecuteNonQuery();
                        Con2.Close();
                    }
                    else
                    {
                        notApprovedEmpCodes.Add(lblUserID.Text);
                    }



                }
            }
            if (notApprovedEmpCodes.Count > 0)
            {
                string empList = string.Join(", ", notApprovedEmpCodes);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('These employees are NOT approved because their Monthly PMS is not approved: " + empList + "');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Employee Verify Successfully');", true);
            }
            ShowEMployeeDetails();
        }








        catch (Exception ex)
        {
            // scope.Dispose();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', 'Some Problem Occured.');", true);
        }
        // }


    }
    public void show_Report()
    {










        SqlDataAdapter da = new SqlDataAdapter("select EmployeeCode,(Select [First Name] from [EDUCOLLEGELIVE-R2].dbo.[TMU$Employee] where No_ collate Latin1_General_100_CS_AS=tblhold.EmployeeCode) as 'Employee Name',Year,month,Date,case when Hold=1 then 'YES' else 'NO' end 'HOLD',case when verify=1 then 'YES' else 'NO' end as 'Verify',Remark from HRMSPortal.[dbo].[tbl_EmployeeSalaryHoldDetail] tblhold where HOD='" + Session["uid"].ToString() + "' and month= REPLACE(LTRIM(REPLACE('" + DropDownList1.SelectedValue + "', '0', ' ')), ' ', '0')  and Year='" + ddlYear1.SelectedValue + "'", con.Con);
        DataTable dt = new DataTable();

        da.Fill(dt);
        con.DisConnect();
        GridView1.DataSource = dt;
        GridView1.DataBind();




    }
    protected void btnReport_Click(object sender, EventArgs e)
    {

        GridView1.AllowPaging = false;

        show_Report();






        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        GridView1.RenderControl(htmlWrite);

        Response.Clear();
        string str = "Employee_Verification_Report";
        Response.AddHeader("content-disposition", "attachment;filename='" + str + "'" + ".xls");

        Response.Charset = "";

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/vnd.xls";

        Response.Write(stringWrite.ToString());


        Response.End();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ShowEMployeeDetails();
    }
    protected void grdEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string Remark = (string)this.grdEmployee.DataKeys[e.Row.RowIndex]["Remark"];

            //string SubjectType = (string)this.grdEmployee.DataKeys[e.Row.RowIndex]["Subject Type"];
            //int Status = (int)this.grdEmployee.DataKeys[e.Row.RowIndex]["Status"];
            //string ExamMethod = (string)this.grdEmployee.DataKeys[e.Row.RowIndex]["ExamMethod"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drpRemark = e.Row.FindControl("drpRemark") as DropDownList;

                if (Remark == "Hold the salary")
                {
                    drpRemark.SelectedValue = "2";
                }
                else
                {
                    drpRemark.SelectedValue = "1";
                }

                CheckBox chk = (CheckBox)e.Row.FindControl("CHKVerify");


                if (chk.Checked == false)
                {

                    e.Row.BackColor = ColorTranslator.FromHtml("#eb4934");
                }


            }
        }
        catch (Exception ex)
        {
        }


    }
}