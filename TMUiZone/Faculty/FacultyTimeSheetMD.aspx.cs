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

public partial class Faculty_TimeSheetMD : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindTable(Session["uid"].ToString());
                string proc = "sp_GetAccessForLabIncharge_Role";
                if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC")
                {
                    proc = "sp_GetAccessForLabIncharge_RoleDM";
                }
                SqlCommand cmd2 = new SqlCommand(proc, con);//sp_GetAccessForLabIncharge_RoleDM
              //  SqlCommand cmd2 = new SqlCommand("sp_GetAccessForLabIncharge_Role", con);//sp_GetAccessForLabIncharge_RoleDM
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@userid", Session["uid"].ToString());
                cmd2.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
                cmd2.Parameters["@Return1"].Direction = ParameterDirection.Output;
                con.Open();
                cmd2.ExecuteNonQuery();
                string res1 = cmd2.Parameters["@Return1"].Value.ToString();
                con.Close();

                if (res1 == "1")
                {
                    tblRoom.Visible = true; drpCourseCode.Visible = false; lblCourse.Visible = false; bindRoom();
                }
                else
                {
                    BindDropdown(); bindCourseCode1(); tblRoom.Visible = false; drpCourseCode.Visible = true; lblCourse.Visible = true;
                    //  drpCourseCode.SelectedIndex = 1;
                }
            }

            string procRole = "sp_GetAccessForTimeTable_Role";
            if (Session["GlobalDimension1Code"].ToString() == "TMDC" || Session["GlobalDimension1Code"].ToString() == "TMMC")
            {
                procRole = "sp_GetAccessForTimeTable_RoleDM";
            }
            SqlCommand cmd1 = new SqlCommand(procRole, con);//sp_GetAccessForLabIncharge_RoleDM
          //  SqlCommand cmd1 = new SqlCommand("sp_GetAccessForTimeTable_Role", con);
            cmd1.CommandType=CommandType.StoredProcedure;
            cmd1.Parameters.Add("@userid",Session["uid"].ToString());
            cmd1.Parameters.Add("@Return1", SqlDbType.VarChar, 30);
            cmd1.Parameters["@Return1"].Direction = ParameterDirection.Output;
            con.Open();
            cmd1.ExecuteNonQuery();
            string res = cmd1.Parameters["@Return1"].Value.ToString();
            con.Close();
             
            if (res == "1")
                chkboxAsPrincipal.Visible = true;
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }

    }

    public void BindTable(string FacultyCode)
    {
        timeTable.Rows.Clear();
        string qur = "";
        if (drpCourseCode.SelectedValue != "-- Select --")
            qur = "and [Course Code] like '" + drpCourseCode.SelectedValue + "%'";

        string Command = "";

        if (rdbClassWise1.Checked == true || rdbSubjectWise.Checked == true)
        {
            Command = "select * from [TMU$Time Table Generation - COL] where [Course Code]='" + drpCourseCode1.SelectedItem.ToString() + "'and [Semester Code] like '" + drpSemester.SelectedValue + "%' and [Section Code] like '" + drpSection.SelectedValue + "%' " + qur + " and [Day No] between  1 and 6 and [Hour No] between 1 and 10 and [Subject Code] like '%" + drpSubject.SelectedValue + "'  and [Attendance Date] between dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))-1),convert(date, getdate())) and dateadd(day,7,dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))),convert(date, getdate()))) Union select * from [TMU$Time Table Generation - COL] where [Course Code]='" + drpCourseCode1.SelectedItem.ToString() + "' and [Year] like '" + drpSemester.SelectedValue + "%' and [Section Code] like '" + drpSection.SelectedValue + "' " + qur + " and [Day No] between  1 and 6 and [Hour No] between 1 and 10 and [Subject Code] like '%" + drpSubject.SelectedValue + "%'  and [Attendance Date] between dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))-1),convert(date, getdate())) and dateadd(day,7,dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))),convert(date, getdate())))";
        }
        else
        {
            Command = "select * from [TMU$Time Table Generation - COL] where [Faculty Code]='" + FacultyCode + "'" + qur + " and [Day No] between  1 and 6 and [Hour No] between 1 and 10 and [Attendance Date] between dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))-1),convert(date, getdate())) and " + @"
                       dateadd(day,7,dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))),convert(date, getdate())))";            
        }
        SqlCommand cmd = new SqlCommand(Command, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        SqlCommand cmd1 = new SqlCommand("select * from [TMU$Time Table Generation - COL] where [Substitute Faculty Code]='" + FacultyCode + "'" + qur + " and [Day No] between  1 and 6 and [Hour No] between 1 and 10 and [Attendance Date] between dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))-1),convert(date, getdate())) and " + @"
                                dateadd(day,7,dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))),convert(date, getdate())))", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);

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
        // add below ----ashu
        TableCell c10 = new TableCell();
        TableCell c11 = new TableCell();
        TableCell c12 = new TableCell();
        // add below ----ashu
        c1.Text = "Days";
        c2.Text = "I";
        c3.Text = "II";
        c4.Text = "III";
        c5.Text = "IV";
        c6.Text = "";
        c7.Text = "V";
        c8.Text = "VI";
        c9.Text = "VII";
        // add below ----ashu
        c10.Text = "VIII";
        c11.Text = "IX";
        c12.Text = "X";
        // add below ----ashu
        hr.Cells.Add(c1);
        hr.Cells.Add(c2);
        hr.Cells.Add(c3);
        hr.Cells.Add(c4);
        hr.Cells.Add(c5);
        hr.Cells.Add(c6);
        hr.Cells.Add(c7);
        hr.Cells.Add(c8);
        hr.Cells.Add(c9);
        // add below ----ashu
        hr.Cells.Add(c10);
        hr.Cells.Add(c11);
        hr.Cells.Add(c12);
        // add below ----ashu
        timeTable.Rows.Add(hr);

        for (int i = 0; i < 6; i++)
        {
            TableRow tr = new TableRow();
            for (int j = 0; j < 12; j++)
            {
                TableCell tc = new TableCell();
                Label l = new Label();
                tc.Controls.Add(l);
                tr.Cells.Add(tc);
            }
            timeTable.Rows.Add(tr);
        }
        int k = dt1.Rows.Count;
        int g = 0;
        string a = timeTable.Rows[1].Cells[3].Text;
        //---------------------------------------------Clear-----------------------
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int r = Convert.ToInt16(dt.Rows[i]["Day No"]);
            int c = Convert.ToInt16(dt.Rows[i]["Hour No"]);
            if (c > 4)
                c++;
            if (dt.Rows[i]["Substitute Faculty Code"].ToString() != "")// "<br/>" + "==>" + 
            {
                timeTable.Rows[r].Cells[c].Text = "";
              //  timeTable.Rows[r].Cells[c].ForeColor = Color.Green;
             //   timeTable.Rows[r].Cells[c].Font.Bold=true;
            }
            else
                timeTable.Rows[r].Cells[c].Text = "";
            if (i <= k - 1)
            {
                int r11 = Convert.ToInt16(dt1.Rows[i]["Day No"]);
                int C11 = Convert.ToInt16(dt1.Rows[i]["Hour No"]);
                if (C11 > 4)
                    C11++;
                timeTable.Rows[r11].Cells[C11].Text = "";
             //   timeTable.Rows[r11].Cells[C11].ForeColor = Color.Green;
              //  timeTable.Rows[r11].Cells[C11].Font.Bold = true;
            }
        }
        //
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int r = Convert.ToInt16(dt.Rows[i]["Day No"]);
            int c = Convert.ToInt16(dt.Rows[i]["Hour No"]);
            if (c > 4)
                c++;
            if (dt.Rows[i]["Substitute Faculty Code"].ToString() != "")// "<br/>" + "==>" + 
            {
                timeTable.Rows[r].Cells[c].Text = timeTable.Rows[r].Cells[c].Text +"<font color=Green>"+ "<br/>" + "<B>" + "==>" + "</B>" + dt.Rows[i]["Subject Description"].ToString() + "/" + dt.Rows[i]["Course Code"].ToString() + "/" + dt.Rows[i]["Section Code"].ToString() + "/" + dt.Rows[i]["Room Allocation"].ToString() + "/" + dt.Rows[i]["Group"].ToString() + "/" + dt.Rows[i]["Batch"].ToString() + "(Assigned)"+"</font>";
              //  timeTable.Rows[r].Cells[c].ForeColor = Color.Green;
                timeTable.Rows[r].Cells[c].Font.Bold = true;
            }
            else
                timeTable.Rows[r].Cells[c].Text += "<br/>" + "<b>" + "==>" + "</b>" + dt.Rows[i]["Subject Description"].ToString() + "/" + dt.Rows[i]["Course Code"].ToString() + "/" + dt.Rows[i]["Section Code"].ToString() + "/" + dt.Rows[i]["Room Allocation"].ToString() + "/" + dt.Rows[i]["Group"].ToString() + "/" + dt.Rows[i]["Batch"].ToString();
            if (i <= k - 1)
            {
                int r11 = Convert.ToInt16(dt1.Rows[i]["Day No"]);
                int C11 = Convert.ToInt16(dt1.Rows[i]["Hour No"]);
                if (C11 > 4)
                    C11++;
                timeTable.Rows[r11].Cells[C11].Text = timeTable.Rows[r11].Cells[C11].Text + "<br/>" + "==>" + timeTable.Rows[r11].Cells[C11].Text + dt1.Rows[i]["Subject Description"].ToString() + "/" + dt1.Rows[i]["Course Code"].ToString() + "/" + dt1.Rows[i]["Room Allocation"].ToString() + "/" + dt.Rows[i]["Group"].ToString() + "/" + dt.Rows[i]["Batch"].ToString() + "(Assigned)";
                timeTable.Rows[r11].Cells[C11].ForeColor = Color.Green;
                timeTable.Rows[r11].Cells[C11].Font.Bold = true; 
            }
            if (dt.Rows[i]["Remedial Portal ID"].ToString() == "1")
            {
                timeTable.Rows[r].Cells[c].ForeColor = Color.Blue;
                timeTable.Rows[r].Cells[c].Font.Bold = true; 
            }
            if (dt.Rows[i]["Remedial Portal ID"].ToString() == "3")
            {
                timeTable.Rows[r].Cells[c].ForeColor = Color.Red;
                timeTable.Rows[r].Cells[c].Font.Bold = true; 
            }
        }
        if (g == 0)
        {
            string[] weeks = new string[6] { "Mon.", "Tue.", "Wed.", "Thu.", "Fri.", "Sat." };
            string[] lunch = new string[6] { "L", "U", "N", "C", "H", "*" };
            for (int l = 1; l < 7; l++)
            {
                timeTable.Rows[l].Cells[0].Text = weeks[l - 1];
                timeTable.Rows[l].Cells[5].Text = lunch[l - 1];
                timeTable.Rows[l].Cells[0].CssClass = "Bold";
                timeTable.Rows[l].Cells[5].CssClass = "Color";
                Unit width = new Unit(30, UnitType.Pixel);
            }
            g++;
        }
    }

    public void BindDropdown()
    {
  //SqlCommand cmd = new SqlCommand("select distinct([Course Code]) from [TMU$Course Wise Faculty] where [Faculty Code]='" + Session["uid"].ToString() + "'", con); //Ashu 01-12-2106
        SqlCommand cmd = new SqlCommand("select distinct([Course Code]) from [TMU$Course Wise Faculty] where [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and [Faculty Code]='" + Session["uid"].ToString() + "' and  [Portal ID]='1'", con); //Ashu 
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpCourseCode.DataSource = dt;
        drpCourseCode.DataTextField = "Course Code";
        drpCourseCode.DataBind();
        drpCourseCode.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));
    }
    protected void drpCourseCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTable(Session["uid"].ToString());
    }
    protected void drpCourseCode1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();
        bindSemester();
    }
    protected void drpFacultyCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTable(drpFacultyCode.SelectedValue);
    }
    public void bindCourseCode1()
    {
        string proc = "Sp_GetCourseRoleWise_HOD_Role_ForTimeTableDM";
        SqlCommand cmd = new SqlCommand(proc, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@userid", Session["uid"].ToString());
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourseCode1.DataSource = dt;
        drpCourseCode1.DataTextField = "No_";
        drpCourseCode1.DataBind();
    }
    public void bindFacultyCode()
    {
        SqlCommand cmd = new SqlCommand("Sp_GetFaculty_Role_ForTimeTableDM", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourseCode1.SelectedValue);
        cmd.Parameters.Add("@SubjectCode", drpSubject.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpFacultyCode.DataSource = dt;
        drpFacultyCode.DataTextField = "Details";
        drpFacultyCode.DataValueField = "No_";
        drpFacultyCode.DataBind();

    }
    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSection();
        if (rdbClassWise1.Checked == true || rdbSubjectWise.Checked == true)
            BindTable(drpFacultyCode.SelectedValue);
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbClassWise1.Checked == true || rdbSubjectWise.Checked == true)
            BindTable(drpFacultyCode.SelectedValue);
    }
    public void bindSemester()
    {
        string Subject = "";
        if (rdbSubjectWise.Checked == true)
            Subject = drpSubject.SelectedValue;
        else
            Subject = "";
        SqlCommand cmd = new SqlCommand("proc_GetSemester_RoleMD", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourseCode1.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@SubjectCode", Subject);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }
    public void bindSection()
    {
        SqlCommand cmd = new SqlCommand("proc_GetSection_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourseCode1.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();
    }
    public void ShowHide()
    {
        if (chkboxAsPrincipal.Checked == true)
        {
            RadioButton.Visible = true;
            facultyCourse.Visible = false;
            if (rdbClassWise1.Checked == true)
            {
                PrincipalCourseCode.Visible = true;
                ClassWise.Visible = true;
                FacultyCode.Visible = false;
                DayNo.Visible = false;
                divSubject.Visible = false;
            }
            else if (rdbFacultyWise.Checked == true)
            {
                FacultyCode.Visible = true;
                PrincipalCourseCode.Visible = true;
                ClassWise.Visible = false;
                DayNo.Visible = false;
                divSubject.Visible = false;
            }
            else if (rdbDayWise.Checked == true)
            {
                FacultyCode.Visible = false;
                PrincipalCourseCode.Visible = true;
                ClassWise.Visible = true;
                DayNo.Visible = true;
                divSubject.Visible = false;
            }
            else if (rdbLabPractical.Checked==true)
            {
                FacultyCode.Visible = false;
                PrincipalCourseCode.Visible = true;
                ClassWise.Visible = true;
                DayNo.Visible = true;
                divSubject.Visible = false;
            }
            else if (rdbSubjectWise.Checked == true)
            {
                PrincipalCourseCode.Visible = true;
                ClassWise.Visible = true;
                FacultyCode.Visible = false;
                DayNo.Visible = false;
                divSubject.Visible = true;
            }
        }
        else
        {
            RadioButton.Visible = false;
            PrincipalCourseCode.Visible = false;
            ClassWise.Visible = false;
            FacultyCode.Visible = false;
            facultyCourse.Visible = true;
            DayNo.Visible = false;
        }
    }
    protected void chkboxAsPrincipal_CheckedChanged(object sender, EventArgs e)
    {
        ShowHide();
        BindTable(Session["uid"].ToString());
    }
    protected void rdbClassWise1_CheckedChanged(object sender, EventArgs e)
    {
        ShowHide();
    }
    protected void rdbFacultyWise_CheckedChanged(object sender, EventArgs e)
    {
        ShowHide();
    }
    protected void drpDayNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string proc="";
        if (rdbDayWise.Checked == true) { proc = "proc_GetFacultyDayWise_Role"; }
        if (rdbLabPractical.Checked == true) { proc = "proc_GetLabPracticalFacultyDayWise_Role"; }
        SqlCommand cmd = new SqlCommand(proc, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourseCode1.SelectedValue);
        cmd.Parameters.Add("@SemYear", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@DayNo", drpDayNo.SelectedValue);
        cmd.Parameters.Add("@Uid", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        TableHeaderRow hr = new TableHeaderRow();
        hr.CssClass = "Bold";
        TableCell c1 = new TableCell();
        TableCell c2 = new TableCell();
        TableCell c3 = new TableCell();
        TableCell c4 = new TableCell();
        TableCell c5 = new TableCell();
        TableCell c7 = new TableCell();
        TableCell c8 = new TableCell();
        TableCell c9 = new TableCell();
        // add below ----ashu
        TableCell c10 = new TableCell();
        TableCell c11 = new TableCell();
        TableCell c12 = new TableCell();
        // add below ----ashu
        c1.Text = "Name";
        c2.Text = "I";
        c3.Text = "II";
        c4.Text = "III";
        c5.Text = "IV";
        c7.Text = "V";
        c8.Text = "VI";
        c9.Text = "VII";
        // add below ----ashu
        c10.Text = "VIII";
        c11.Text = "IX";
        c12.Text = "X";
        // add below ----ashu
        hr.Cells.Add(c1);
        hr.Cells.Add(c2);
        hr.Cells.Add(c3);
        hr.Cells.Add(c4);
        hr.Cells.Add(c5);
        hr.Cells.Add(c7);
        hr.Cells.Add(c8);
        hr.Cells.Add(c9);
        // add below ----ashu
        hr.Cells.Add(c10);
        hr.Cells.Add(c11);
        hr.Cells.Add(c12);
        // add below ----ashu
        tblFacultyDayWise.Rows.Add(hr);

        int rowCnt;
        int rowCtr;
        int cellCtr;
        int cellCnt;

        rowCnt = dt.Rows.Count;
        // cellCnt = 8; //comment by Ashu
        cellCnt = 11; //ashu

        for (rowCtr = 1; rowCtr <= rowCnt; rowCtr++)
        {
            TableRow tRow = new TableRow();
            tblFacultyDayWise.Rows.Add(tRow);
            for (cellCtr = 0; cellCtr < cellCnt; cellCtr++)
            {
                TableCell tCell = new TableCell();
                if (cellCtr == 0)
                {
                    tCell.Text = dt.Rows[rowCtr - 1]["Faculty Name"].ToString();
                }
                tRow.Cells.Add(tCell);
            }
        }
        string str = "";
        if (rdbDayWise.Checked == true)
        {
            str = "select [Faculty Code],[Faculty Name],[Course Code],[Semester Code],[Section Code],[Hour No],[Subject Description],[Room Allocation] from [TMU$Time Table Generation - COL] where [Day No] ='" + drpDayNo.SelectedValue + "' and [Hour No] between 1 and 10  and [Attendance Date] between dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))-1),convert(date, getdate())) and dateadd(day,7,dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))),convert(date, getdate()))) and [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'";
        }
        if(rdbLabPractical.Checked==true)
        {
            str = "select [Faculty Code],[Faculty Name],[Course Code],[Semester Code],[Section Code],[Hour No],[Subject Description],[Room Allocation] from [TMU$Time Table Generation - COL] where [Day No] ='" + drpDayNo.SelectedValue + "' and [Hour No] between 1 and 10  and [Attendance Date] between dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))-1),convert(date, getdate())) and dateadd(day,7,dateadd(day,-(dbo.fun_GetDayNoMon2Sat(convert(date, getdate()))),convert(date, getdate()))) and [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'  and( [Subject Classification]= 'LAB'or [Subject Classification]= 'PRACTICAL') and [Course Code] in (select [Course Code] from [TMU$User Role Matrix] where ([Course Co-Ordinator]='" + Session["uid"].ToString() + "' or HOD='" + Session["uid"].ToString() + "' or Principal='" + Session["uid"].ToString() + "') )";
        }
        SqlCommand cmd1 = new SqlCommand(str, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        int l = 8;
        for (int k = 0; k < dt.Rows.Count; k++)
        {
            for (int q = 1; q < l; q++)
            {
                tblFacultyDayWise.Rows[k + 1].Cells[q].Text = "";
            }
            for (int p = 0; p < dt1.Rows.Count; p++)
            {
                if (dt.Rows[k]["HourNo"].ToString().Contains(dt1.Rows[p]["Hour No"].ToString()) && dt.Rows[k]["Faculty Code"].ToString() == dt1.Rows[p]["Faculty Code"].ToString())
                {
                    int o = Convert.ToInt16(dt1.Rows[p]["Hour No"].ToString());
                    tblFacultyDayWise.Rows[k + 1].Cells[o].Text = dt1.Rows[p]["Subject Description"].ToString() + "/" + dt1.Rows[p]["Course Code"].ToString() + "/" + dt1.Rows[p]["Semester Code"].ToString() + "/" + dt1.Rows[p]["Section Code"].ToString() + "/" + dt1.Rows[p]["Room Allocation"].ToString();
                }
            }
        }

    }
    protected void rdbDayWise_CheckedChanged(object sender, EventArgs e)
    {
        ShowHide();
    }
    public void BindPractical()
    {
        DataTable dt = new DataTable();
        timeTable.Rows.Clear();
        if (ddlRoom.SelectedValue != "")
        {
            SqlCommand cmd = new SqlCommand("PracticalTimeTableCollegeWise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
            cmd.Parameters.Add("@RoomNo", ddlRoom.SelectedValue);
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        TableHeaderRow hr = new TableHeaderRow();
        hr.CssClass = "Bold";
        TableCell c1 = new TableCell(); TableCell c2 = new TableCell(); TableCell c3 = new TableCell(); TableCell c4 = new TableCell();
        TableCell c5 = new TableCell(); TableCell c6 = new TableCell(); TableCell c7 = new TableCell(); TableCell c8 = new TableCell();
        TableCell c9 = new TableCell(); TableCell c10 = new TableCell(); TableCell c11 = new TableCell(); TableCell c12 = new TableCell();

        c1.Text = "Days"; c2.Text = "I"; c3.Text = "II"; c4.Text = "III"; c5.Text = "IV"; c6.Text = ""; c7.Text = "V";
        c8.Text = "VI"; c9.Text = "VII"; c10.Text = "VIII"; c11.Text = "IX"; c12.Text = "X";

        hr.Cells.Add(c1); hr.Cells.Add(c2); hr.Cells.Add(c3); hr.Cells.Add(c4); hr.Cells.Add(c5); hr.Cells.Add(c6);
        hr.Cells.Add(c7); hr.Cells.Add(c8); hr.Cells.Add(c9); hr.Cells.Add(c10); hr.Cells.Add(c11); hr.Cells.Add(c12);

        timeTable.Rows.Add(hr);
        for (int i = 0; i < 6; i++)
        {
            TableRow tr = new TableRow();
            for (int j = 0; j < 12; j++)
            {
                TableCell tc = new TableCell();
                Label l = new Label();
                tc.Controls.Add(l);
                tr.Cells.Add(tc);
            }
            timeTable.Rows.Add(tr);
        }

        int g = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int r = Convert.ToInt16(dt.Rows[i]["Day No"]);
            int c = Convert.ToInt16(dt.Rows[i]["Hour No"]);
            if (c > 4)
                c++;
            if (dt.Rows[i]["Substitute Faculty Code"].ToString() != "")
            {
                timeTable.Rows[r].Cells[c].Text = dt.Rows[i]["Subject Description"].ToString() + "/" + dt.Rows[i]["Course Code"].ToString() + "/" + dt.Rows[i]["Section Code"].ToString() + "/" + dt.Rows[i]["Room Allocation"].ToString() + "/" + dt.Rows[i]["Group"].ToString() + "/" + dt.Rows[i]["Batch"].ToString() + "(Assigned)";
                timeTable.Rows[r].Cells[c].ForeColor = Color.Red;
            }
            else
                timeTable.Rows[r].Cells[c].Text = dt.Rows[i]["Subject Description"].ToString() + "/" + dt.Rows[i]["Course Code"].ToString() + "/" + dt.Rows[i]["Section Code"].ToString() + "/" + dt.Rows[i]["Room Allocation"].ToString() + "/" + dt.Rows[i]["Group"].ToString() + "/" + dt.Rows[i]["Batch"].ToString();

        }
        if (g == 0)
        {
            string[] weeks = new string[6] { "Mon.", "Tue.", "Wed.", "Thu.", "Fri.", "Sat." };
            string[] lunch = new string[6] { "L", "U", "N", "C", "H", "*" };
            for (int l = 1; l < 7; l++)
            {
                timeTable.Rows[l].Cells[0].Text = weeks[l - 1];
                timeTable.Rows[l].Cells[5].Text = lunch[l - 1];
                timeTable.Rows[l].Cells[0].CssClass = "Bold";
                timeTable.Rows[l].Cells[5].CssClass = "Color";
                Unit width = new Unit(30, UnitType.Pixel);
            }
            g++;
        }
    }
    public void bindRoom()
    {
        SqlCommand cmd = new SqlCommand("Sp_GetPracticalRoomsCollegeWise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        if (con.State == ConnectionState.Closed)
            con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlRoom.DataSource = dt;
        ddlRoom.DataTextField = "Description";
        ddlRoom.DataValueField = "Code";
        ddlRoom.DataBind();
        con.Close();
    }
    protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPractical();
    }
    protected void rdbLabPractical_CheckedChanged(object sender, EventArgs e)
    {
        ShowHide();
    }
    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindFacultyCode();
        bindSemester();
        BindTable(drpFacultyCode.SelectedValue);
    }
    public void bindSubject()
    {
        SqlCommand cmd = new SqlCommand("Sp_GetSubject_Role_ForTimeTableDM", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", drpCourseCode1.SelectedValue);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();
    }
}