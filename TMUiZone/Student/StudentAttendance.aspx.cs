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
using System.Net;
using System.Net.Mail;

public partial class StudentAttendance : System.Web.UI.Page
{    
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                StudentCollege();
                BindSubject();
            }
            HideShow();
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    } 
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (rdbSubjectWise.Checked)
        {
            BindGridSubjectWise();
        }
        else
        {
            bindGrid();
        }
    }
    protected void grdAttendanceReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAttendanceReport.PageIndex = e.NewPageIndex;
        bindGrid();
    }
    public void bindGrid()
    {
        string SemYear="";
        if (Session["Year"].ToString() == "") { SemYear = Session["Semester"].ToString(); } else { SemYear = Session["Year"].ToString(); }
        //----------- add in below query befre order by on 08-03-2017 by ashu and h.[Attendance Type]=0
        SqlCommand cmd = new SqlCommand("select replace(convert(NVARCHAR, l.Date, 106), ' ', '-') as Date,l.Hour as LecturNo,l.[Subject Type],l.[Subject Code],case convert(varchar(20), l.[Attendance Type]) when '0' then 'Present'" + @"
        when '1' then 'Absent' else convert(varchar(20),l.[Attendance Type]) end as Status,h.[Topic Covered],h.[Subject Description] from [TMU$Student Attendance Line - COL] l with(NOLOCK) inner " + @"
        join [TMU$Student Attendance Header -COL] h with(NOLOCK) on l.[Document No_]=h.No_ where l.Date between '" + txtDateFrom.Text + "' and '" + txtDateTo.Text + "' " + @"
        and [Student No_]='" + Session["uid"].ToString() + "' and l.[Course Code]='" + Session["CourseCode"].ToString() + "' " + @"
        and (l.Semester='" + SemYear + "' or l.[Year]='" + SemYear + "' ) and h.[Attendance Type] in ('0','4') and h.[Academic Year]='" + Session["AcademicYear"].ToString() + "'  order by l.Date desc,l.Hour asc", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAttendanceReport.DataSource = dt;
        grdAttendanceReport.DataBind();
        if (dt.Rows.Count > 0)
        pnlAttendance.Visible = true;
        grdAttendanceReport.Visible = true;
        grdAttendanceReport1.Visible = false;

    }

    public void BindGridSubjectWise()
    {
        string qry = ""; string queryy = "";

        if (drpSubject.SelectedItem.Text != "-- All --")
        {
            if (hfCollegeCode.Value == "TMMC")
            { qry = "and [Subject Code]='" + drpSubject.SelectedValue + "' and [Academic Year]='" + Session["AcademicYear"].ToString() + "'  and Remedial=0"; }
            else
            { qry = "and [Subject Code] like '%" + drpSubject.SelectedValue + "%' and [Academic Year]='" + Session["AcademicYear"].ToString() + "'  and Remedial=0"; }
        }
                   
            if (hfCollegeCode.Value == "TMMC")
            {

                queryy = "select sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage,h.[Description] as [Subject Description],h.Code from " + @"
            (select [Subject Code],count([Attendance Type] ) as Present,'' as [Absent] from [TMU$Student Attendance Line - COL] with(NOLOCK) where Date between  case when (select [Date of Joining] from [TMU$Student - COLLEGE] with(NOLOCK) where No_='" + Session["uid"].ToString() + "') >'" + txtDateFrom.Text + "' then (select [Date of Joining] from [TMU$Student - COLLEGE] with(NOLOCK) where No_='" + Session["uid"].ToString() + "') else '" + txtDateFrom.Text + "' end  and '" + txtDateTo.Text + "' and " + @"
            [Attendance Type]=0 and [Student No_]='" + Session["uid"].ToString() + "' and [Course Code]='" + Session["CourseCode"].ToString() + "' and Semester='" + Session["Semester"].ToString() + "' " + qry + " and Remedial!=3 group by [Subject Code] " + @"
                union " + @"
            select [Subject Code],'0' as Present, count([Attendance Type]) as [Absent] from [TMU$Student Attendance Line - COL] with(NOLOCK) where Date between  case when (select [Date of Joining] from [TMU$Student - COLLEGE] with(NOLOCK) where No_='" + Session["uid"].ToString() + "') >'" + txtDateFrom.Text + "' then (select [Date of Joining] from [TMU$Student - COLLEGE] with(NOLOCK) where No_='" + Session["uid"].ToString() + "') else '" + txtDateFrom.Text + "' end  and '" + txtDateTo.Text + "' and " + @"
			 [Attendance Type]=1 and [Student No_]='" + Session["uid"].ToString() + "' and [Course Code]='" + Session["CourseCode"].ToString() + "' and Semester='" + Session["Semester"].ToString() + "' " + qry + " and Remedial!=3 group by [Subject Code] )  l inner join [TMU$Subject - COLLEGE] h " + @"
			 on l.[Subject Code]=h.[Code] and Course='" + Session["CourseCode"].ToString() + "' and Semester='" + Session["Semester"].ToString() + "' group by l.[Subject Code],h.[Description],h.Code";

            }
            else
            {             
                queryy = "select sum(Present) as Present, sum([Absent]) as [Absent],(sum(Present)*100)/(sum(Present)+sum([Absent])) as Percentage,h.[Description] as [Subject Description],h.Code from " + @"
            (select [Subject Code],count([Attendance Type] ) as Present,'' as [Absent],[Academic Year] from [TMU$Student Attendance Line - COL] with(NOLOCK) where Date between    case when (select [Date of Joining] from [TMU$Student - COLLEGE] with(NOLOCK) where No_='" + Session["uid"].ToString() + "') >'" + txtDateFrom.Text + "' then (select [Date of Joining] from [TMU$Student - COLLEGE] with(NOLOCK) where No_='" + Session["uid"].ToString() + "') else '" + txtDateFrom.Text + "' end and '" + txtDateTo.Text + "' and " + @"
            [Attendance Type]=0 and [Student No_]='" + Session["uid"].ToString() + "' and [Course Code]='" + Session["CourseCode"].ToString() + "' and Semester='" + Session["Semester"].ToString() + "' " + qry + " and Remedial!=3 group by [Subject Code] ,[Academic Year] " + @"
                union " + @"
            select [Subject Code],'0' as Present, count([Attendance Type]) as [Absent],[Academic Year] from [TMU$Student Attendance Line - COL] with(NOLOCK) where Date between case when (select [Date of Joining] from [TMU$Student - COLLEGE] with(NOLOCK) where No_='" + Session["uid"].ToString() + "') >'" + txtDateFrom.Text + "' then (select [Date of Joining] from [TMU$Student - COLLEGE] with(NOLOCK) where No_='" + Session["uid"].ToString() + "') else '" + txtDateFrom.Text + "' end and '" + txtDateTo.Text + "' and " + @"
			 [Attendance Type]=1 and [Student No_]='" + Session["uid"].ToString() + "' and [Course Code]='" + Session["CourseCode"].ToString() + "' and Semester='" + Session["Semester"].ToString() + "' " + qry + " and Remedial!=3 group by [Subject Code] ,[Academic Year] )  l inner join [TMU$Subject - COLLEGE] h " + @"
			 on l.[Subject Code]=h.[Code] and Course='" + Session["CourseCode"].ToString() + "' and h.[Academic Year]=l.[Academic Year] group by l.[Subject Code],h.[Description],h.Code";

            }
        
        SqlCommand cmd = new SqlCommand(queryy, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAttendanceReport1.DataSource = dt;
        grdAttendanceReport1.DataBind();
        if (dt.Rows.Count > 0)
            pnlAttendance.Visible = true;
        grdAttendanceReport.Visible = false;
        grdAttendanceReport1.Visible = true;
    }

    public void BindSubject()
    {
        //SqlCommand cmd = new SqlCommand("select '' as No_,'-- All --' as Details union select distinct [Subject Code] as No_,S.[Description] as Details from [TMU$Time Table Generation - COL] T inner join [TMU$Subject - COLLEGE] S on S.[Code]=T.[Subject Code] where [Course Code]='" + Session["CourseCode"].ToString() + "' and [Semester Code]='" + Session["Semester"].ToString() + "'", con); //Comment on 07-102017 

        SqlCommand cmd = new SqlCommand("Sp_GetStudentSubjects", con); //added on 07-10-2017 Created procedure
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", Session["CourseCode"].ToString());
        cmd.Parameters.Add("@SemesterCode", Session["Semester"].ToString());
        cmd.Parameters.Add("@Year", Session["Year"].ToString());
        cmd.Parameters.Add("@AcademicYear", Session["AcademicYear"].ToString()); 
        cmd.Parameters.Add("@StudentNo", Session["uid"].ToString()); 
        SqlDataAdapter da = new SqlDataAdapter(cmd);       
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();
    }
    public void HideShow()
    {
        if (rdbDateWise.Checked == true)
        {
            drpSubject.Visible = false;
            lblSubject.Visible = false;
        }
        else if (rdbSubjectWise.Checked == true)
        {
            drpSubject.Visible = true;
            lblSubject.Visible = true;
        }
    }
    protected void rdbDateWise_CheckedChanged(object sender, EventArgs e)
    {
        HideShow();
    }
    protected void rdbSubjectWise_CheckedChanged(object sender, EventArgs e)
    {
        HideShow();
    }
    public void StudentCollege()
    {
        SqlCommand cmd = new SqlCommand("select [Global Dimension 1 Code] from [TMU$Student - COLLEGE] where [No_]='" + Session["uid"].ToString() + "'", con);
        con.Open();
       hfCollegeCode.Value=cmd.ExecuteScalar().ToString();
        con.Close();
    }
    protected void grdAttendanceReport1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PresentItem")
        {
            GridViewRow rowItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int index = rowItem.RowIndex;
            GridViewRow row = grdAttendanceReport1.Rows[index];
            string SubjectCode = row.Cells[1].Text;

            string qry = "";
            if (drpSubject.SelectedItem.Text != "-- All --")
                qry = "and [Subject Code] like '%" + drpSubject.SelectedValue + "%' and Remedial=0";

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select ([Attendance Type])as Present,convert(varchar(11),Date,106) as Date,[Staff Code],Remark from [TMU$Student Attendance Line - COL] where Date between '" + txtDateFrom.Text + "' and '" + txtDateTo.Text + "' and [Attendance Type]=0 and [Student No_]='" + Session["uid"].ToString() + "' and [Course Code]='" + Session["CourseCode"].ToString() + "' and Semester='" + Session["Semester"].ToString() + "' " + qry + " and Remedial!=3 and [Subject Code]='" + SubjectCode + "' order by month(Date) desc, Date desc,Year(Date) desc", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            da.Fill(dt);

            grdAttandanceDetails.DataSource = dt;
            grdAttandanceDetails.DataBind();
            grdAttandanceDetails.DataSource = dt;
            grdAttandanceDetails.DataBind();
            MpaDetails.Show();
        }
        else if (e.CommandName == "AbsentItem")
        {
            GridViewRow rowItem = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int index = rowItem.RowIndex;
            GridViewRow row = grdAttendanceReport1.Rows[index];
            string SubjectCode = row.Cells[1].Text;

            string qry = "";
            if (drpSubject.SelectedItem.Text != "-- All --")
                qry = "and [Subject Code] like '%" + drpSubject.SelectedValue + "%' and Remedial=0";

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select ([Attendance Type])as Absent,convert(varchar(11),Date,106) as Date,[Staff Code],Remark from [TMU$Student Attendance Line - COL] where Date between '" + txtDateFrom.Text + "' and '" + txtDateTo.Text + "' and [Attendance Type]=1 and [Student No_]='" + Session["uid"].ToString() + "' and [Course Code]='" + Session["CourseCode"].ToString() + "' and Semester='" + Session["Semester"].ToString() + "' " + qry + " and Remedial!=3 and [Subject Code]='" + SubjectCode + "' order by month(Date) desc, Date desc,Year(Date) desc", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            da.Fill(dt);

            grdAttandanceDetails.DataSource = dt;
            grdAttandanceDetails.DataBind();
            grdAttandanceDetails.DataSource = dt;
            grdAttandanceDetails.DataBind();
            MpaDetails.Show();
        }
    }
}