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

public partial class Student_TimeSheet : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    string FacultyCode = "";
    DataTable dt2 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                //lblFaculty.Visible = false;
                //drpFacultyName.Visible = false;
                lblCourse.Text = "<u>" + Session["CourseCode"].ToString() + "</u>";
                lblSemester.Text = "<u>" + Session["Semester"].ToString() + "</u>";
                if (lblSemester.Text == "")
                {
                    lblSemester.Text = "<u>" + Session["Year"].ToString() + "</u>";
                }
                lblSection.Text = "<u>" + Session["Section"].ToString() + "</u>";
                lblDate.Text = "<u>" + System.DateTime.Today.ToShortDateString() + "</u>";
                BindDropdown();
                BindTable();

            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    public void BindTable()
    {
        string qur = "";
        if (drpFacultyName.SelectedValue != "-- Select --")
            // qur="and [Faculty Name] like '"+drpFacultyName.SelectedValue+"%'";
            qur = "and [Faculty Code] like '" + drpFacultyName.SelectedValue + "%'";

        //       SqlCommand cmd = new SqlCommand("select * from [TMU$Time Table Generation - COL] where [Course Code]='" + Session["CourseCode"].ToString() + "'"+@"
        //        and [Semester Code]='" + Session["Semester"].ToString() + "' and [Section Code] like '" + Session["Section"].ToString() + "%' "+qur+"", con);
        // SqlCommand cmd = new SqlCommand("select * from [TMU$Time Table Generation - COL] where [Course Code]='" + Session["CourseCode"].ToString() + "' and ([Semester Code]='" + Session["Semester"].ToString() + "' and [Year]='" + Session["Year"].ToString() + "') and [Section Code]='" + Session["Section"].ToString() + "'  and [Day No] between  1 and 6 and [Hour No] between 1 and 10 and [Attendance Date] between dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))-1),convert(date, getdate())) and " + @"
        // dateadd(day,7,dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))),convert(date, getdate())))" + qur + "", con);
        SqlCommand cmd = new SqlCommand();
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
             cmd = new SqlCommand("select *,convert(varchar,[Attendance Date],106) as 'Date' from [TMU$Time Table Generation - COL] where [Course Code]='" + Session["CourseCode"].ToString() + "' and ([Semester Code]='" + Session["Semester"].ToString() + "' and [Year]='" + Session["Year"].ToString() + "') and [Section Code]='" + Session["Section"].ToString() + "'  and [Day No] between  1 and 6 and [Hour No] between 1 and 10 and [Academic Year]=(Select Code from [TMU$Academic Year] where [Runing Year]=1) and [Attendance Date] <='" + txtToDate.Text + "' and [Attendance Date]>='" + txtFromDate.Text + "'" + qur + "", con);

        }
        else
        {

             cmd = new SqlCommand("select *,convert(varchar,[Attendance Date],106) as 'Date' from [TMU$Time Table Generation - COL] where [Course Code]='" + Session["CourseCode"].ToString() + "' and ([Semester Code]='" + Session["Semester"].ToString() + "' and [Year]='" + Session["Year"].ToString() + "') and [Section Code]='" + Session["Section"].ToString() + "'  and [Day No] between  1 and 6 and [Hour No] between 1 and 10 and [Academic Year]=(Select Code from [TMU$Academic Year] where [Runing Year]=1) " + qur + "", con);
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //DataView dv = new DataView(dt);     
        //if(drpFacultyName.SelectedValue !="-- Select --")
        //{
        //dv.RowFilter = "[Faculty Code] = '"+drpFacultyName.SelectedValue+"' ";
        //}
        //[Faculty Code]


        TableHeaderRow hr = new TableHeaderRow();
        hr.CssClass = "Bold";
        TableCell c1 = new TableCell();
        TableCell c2 = new TableCell();
        TableCell c3 = new TableCell();
        TableCell c4 = new TableCell();
        TableCell c5 = new TableCell();
        TableCell c6 = new TableCell();
        TableCell c7 = new TableCell();
        TableCell c8 = new TableCell();
        TableCell c9 = new TableCell();
        //-----------------Ashu-------------10---01-08-2016
        TableCell c10 = new TableCell();
        TableCell c11 = new TableCell();
        TableCell c12 = new TableCell();
        //-----------------Ashu-------------10---01-08-2016
        c1.Text = "Days";
        c2.Text = "I";
        c3.Text = "II";
        c4.Text = "III";
        c5.Text = "IV";
        c6.Text = "";
        c7.Text = "V";
        c8.Text = "VI";
        c9.Text = "VII";
        //-----------------Ashu-------------10---01-08-2016
        c10.Text = "VIII";
        c11.Text = "IX";
        c12.Text = "X";
        //-----------------Ashu-------------10---01-08-2016
        hr.Cells.Add(c1);
        hr.Cells.Add(c2);
        hr.Cells.Add(c3);
        hr.Cells.Add(c4);
        hr.Cells.Add(c5);
        hr.Cells.Add(c6);
        hr.Cells.Add(c7);
        hr.Cells.Add(c8);
        hr.Cells.Add(c9);
        //-----------------Ashu-------------10---01-08-2016
        hr.Cells.Add(c10);
        hr.Cells.Add(c11);
        hr.Cells.Add(c12);
        //-----------------Ashu-------------10---01-08-2016
        timeTable.Rows.Add(hr);

        for (int i = 1; i < 7; i++)
        {
            TableRow tr = new TableRow();
            for (int j = 1; j < 13; j++)
            {
                TableCell tc = new TableCell();
                Label l = new Label();
                tc.Controls.Add(l);
                tr.Cells.Add(tc);
                timeTable.Rows.Add(tr);
            }

        }
        //---------------------------------------------Clear-----------------------
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int r = Convert.ToInt16(dt.Rows[i]["Day No"]);
            int c = Convert.ToInt16(dt.Rows[i]["Hour No"]);
            if (c > 4)
                c++;
            timeTable.Rows[r].Cells[c].Text = "";
        }
        //

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int r = Convert.ToInt16(dt.Rows[i]["Day No"]);
            int c = Convert.ToInt16(dt.Rows[i]["Hour No"]);

            if (c > 4)
                c++;
            string FacultyName = dt.Rows[i]["Substitute Faculty Name"].ToString() == "" ? dt.Rows[i]["Faculty Name"].ToString() : dt.Rows[i]["Substitute Faculty Name"].ToString();
            timeTable.Rows[r].Cells[c].Text = timeTable.Rows[r].Cells[c].Text + "<br/>" + "<B>" + "==>" + "</B>" + dt.Rows[i]["Subject Description"].ToString() + " (" + dt.Rows[i]["Subject Classification"].ToString() + ")" + "/ " + FacultyName + "/" + dt.Rows[i]["Room Allocation"].ToString() + "/" + dt.Rows[i]["Date"].ToString();
            if (FacultyCode == dt.Rows[i]["Faculty Code"].ToString())
            {
                SqlCommand cmd1 = new SqlCommand("select * from [TMU$Time Table Generation - COL] where [Day No]!='" + r + "' and [Hour No]!='" + c + "' and [Course Code]='" + lblCourse.Text + "' and [Semester Code]='" + lblSemester.Text + "'", con);
                DataTable dt3 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                da2.Fill(dt3);
                if (dt3.Rows.Count > 0)
                    timeTable.Rows[r].Cells[c].Text = dt3.Rows[0]["Faculty Name"].ToString();
                else
                    timeTable.Rows[r].Cells[c].Text = "";
            }
            if (i == 0)
            {
                string[] weeks = new string[6] { "Mon.", "Tue.", "Wed.", "Thu.", "Fri.", "Sat." };
                string[] lunch = new string[6] { "L", "U", "N", "C", "H", "*" };
                for (int l = 1; l < 7; l++)
                {
                    timeTable.Rows[l].Cells[0].Text = weeks[l - 1];
                    timeTable.Rows[l].Cells[5].Text = lunch[l - 1];
                    timeTable.Rows[l].Cells[0].CssClass = "Bold";
                    timeTable.Rows[l].Cells[5].CssClass = "Color";
                }
            }

            if (dt.Rows[i]["Remedial Portal ID"].ToString() == "1")
            {
                timeTable.Rows[r].Cells[c].ForeColor = Color.Blue;
                timeTable.Rows[r].Cells[c].Font.Bold = true;
            }
        }
    }
    protected void drpFacultyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTable();
    }
    public void BindDropdown()
    {
        SqlCommand cmd = new SqlCommand("select distinct([Faculty Name]),[Faculty Code] from [TMU$Time Table Generation - COL] where [Course Code]='" +
            Session["CourseCode"].ToString() + "'and [Semester Code]='" + Session["Semester"].ToString() + "' and [Section Code] like '" + Session["Section"].ToString() + "%' AND [Academic Year]= '" + Session["AcademicYear"].ToString() + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt2);
        drpFacultyName.DataSource = dt2;
        drpFacultyName.DataTextField = "Faculty Name";
        drpFacultyName.DataValueField = "Faculty Code";
        drpFacultyName.DataBind();
        drpFacultyName.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));

        for (int i = 0; i < dt2.Rows.Count; i++)
        {
            SqlCommand cmd1 = new SqlCommand("select * from [TMU$Pay Daily Attendence Detail] where [Employee Code]='" + dt2.Rows[i]["Faculty Code"] + "' and [Attendance Date]=getdate() and Status in ('3','5','6')", con);
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
                FacultyCode = dt1.Rows[0]["Employee Code"].ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindTable();
    }
}