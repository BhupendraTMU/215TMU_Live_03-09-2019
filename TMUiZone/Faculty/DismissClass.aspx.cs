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
using System.IO;

public partial class Faculty_DismissClass_ : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserGroup"].ToString() == "PRINCIPAL")
            {

                if (Session["uid"].ToString() != null)
                {


                    if (!IsPostBack)
                    {
                        BindCourse();
                        bindAcademicYear();
                       // bindDismissClass();
                        
                    }
                }
                else
                {
                    Response.Redirect("../Default.aspx");
                }
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }

    public void bindAcademicYear()
    {
        SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        ddlAcademicYear.DataSource = dt1;
        ddlAcademicYear.DataTextField = "Details";
        ddlAcademicYear.DataValueField = "No_";
        ddlAcademicYear.DataBind();
    }

    public void BindCourse()
    {
        string AsPrincipal = "";
       
            AsPrincipal = "True";
        SqlCommand cmd = new SqlCommand("proc_GetCourseForReviewAttendance", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@AsPrincipal", AsPrincipal);
        cmd.Parameters.Add("@CollegeCode", Session["GlobalDimension1Code"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpCourse.DataSource = dt;
        drpCourse.DataTextField = "Details";
        drpCourse.DataValueField = "No_";
        drpCourse.DataBind();
    }

    public void BindSemester()
    {
        string FacultyCode = "";
          //  FacultyCode = Session["uid"].ToString();
        
        SqlCommand cmd = new SqlCommand("proc_GetSemester", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }

    public void BindSection()
    {
        string FacultyCode = "";
       
        SqlCommand cmd = new SqlCommand("proc_GetSection", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterYear", drpSemester.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FacultyCode);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();
    }

    public void BindSubjectCode()
    {
        string FCode = "";
       

        DataTable dt = new DataTable();
       // SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance", con); //sandeep 22/12/2016
        SqlCommand cmd = new SqlCommand("proc_GetSubjectFromTomeTableForReviewAttendance_FacultyWise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@FacultyCode", FCode);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        drpSubject.DataSource = dt;
        drpSubject.DataTextField = "Details";
        drpSubject.DataValueField = "No_";
        drpSubject.DataBind();
    }

    public void bindGroupList()
    {
        SqlCommand cmd = new SqlCommand("proc_GetGroupFromCourseSemester", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CourseCode", drpCourse.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "Details";
        ddlGroup.DataValueField = "No_";
        ddlGroup.DataBind();
    }

    public void bindhourno()
    {
        SqlCommand cmd = new SqlCommand("Sp_GetHourNoForDismissClass", con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("@Coursecode", drpCourse.SelectedValue);
          cmd.Parameters.Add("@SemOrYear", drpSemester.SelectedValue);
          cmd.Parameters.Add("@subjectCode", drpSubject.SelectedValue);
          cmd.Parameters.Add("@sectionCode", drpSection.SelectedValue);
          cmd.Parameters.Add("@AcademicYear", ddlAcademicYear.SelectedValue);
          if (txtDate.Text != "")
          {
              cmd.Parameters.Add("@AttendanceDate", txtDate.Text);
              
          }
          else
          {
              ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Please select Date');", true);
              txtDate.Focus();
              drpSubject.SelectedIndex = -1;
              return;
          }

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            drpLecture.DataSource = dt;
            drpLecture.DataTextField = "Hno";
            drpLecture.DataValueField = "Hno";
            drpLecture.DataBind();
            drpLecture.Items.Insert(0, new ListItem("-- Select --", "-- Select --"));

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Time table not created for selected Date');", true);
            return;
        }
        
    }




    public void Dismissclass(string str, string str1, string str2)
    {
        //[Remedial Portal ID]
        con.Close();
        SqlCommand cmd1 = new SqlCommand("select * from [TMU$Time Table Generation - COL] where [Remedial Portal ID]=3 and " + str + "and  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'and convert(varchar(12),[Attendance Date],106)='" + txtDate.Text + "'and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' ", con);
        con.Open();
        SqlDataReader dr = cmd1.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Already Class Dismiss !');", true);
            return;
        }
        else
        {

                      //  con.Close();

        //    SqlCommand cmd2 = new SqlCommand("select * from [TMU$Time Table Generation - COL] where [Remedial Portal ID]=3 and " + str + "and  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'and convert(varchar(12),[Attendance Date],106)='" + txtDate.Text + "'and [Academic Year]='" + ddlAcademicYear.SelectedValue + "' ", con);

        //     DataTable dt = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(cmd2);
        //da.Fill(dt);
        //if (dt.Rows.Count < 0)
        //{
            con.Close();
            SqlCommand cmd = new SqlCommand("update [TMU$Time Table Generation - COL] set [Remedial Portal ID]=3 where " + str + "and  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'and convert(varchar(12),[Attendance Date],106)='" + txtDate.Text + "'and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


            try
            {
                con.Close();

                SqlCommand cmd3 = new SqlCommand("update [TMU$Student Attendance Line - COL] set [Remedial]=3 where "+str1+" and [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'and convert(varchar(13),[Date],106)='" + txtDate.Text + "'", con);
                con.Open();
                cmd3.CommandTimeout = 500000;
                cmd3.ExecuteNonQuery();
                con.Close();
                SqlCommand cmd4 = new SqlCommand("update [TMU$Student Attendance Header -COL] set [Attendance Type]=3 where "+str2+" and  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'and convert(varchar(13),[Date],106)='" + txtDate.Text + "'", con);
                con.Open();
                cmd4.CommandTimeout = 500000;
                cmd4.ExecuteNonQuery();
                con.Close();
            }
            catch
            {

            }








            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Class Dismiss !');", true);
            btnDismiss.Enabled = true;
        //}


        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Time table not created for selected Date');", true);
        //    return;
        //}






           









            
        }
    }

    public void Dismissclass()
    {
        SqlCommand cmd1 = new SqlCommand("select * from [TMU$Time Table Generation - COL] where [Remedial Portal ID]=3 and  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and convert(varchar(12),[Attendance Date],106)='" + txtDate.Text + "'and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'", con);
         con.Open();
        SqlDataReader dr = cmd1.ExecuteReader();
        if (dr.Read())
        {
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Already Class Dismiss !');", true);
            return;
        }
        else
        {
            //[Remedial Portal ID]
           // con.Close();

        //    SqlCommand cmd2 = new SqlCommand("select * from [TMU$Time Table Generation - COL] where [Remedial Portal ID]=3 and  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and convert(varchar(12),[Attendance Date],106)='" + txtDate.Text + "'and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'", con);

        //     DataTable dt = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter(cmd2);
        //da.Fill(dt);
        //if (dt.Rows.Count < 0)
        //{
            con.Close();
            SqlCommand cmd = new SqlCommand("update [TMU$Time Table Generation - COL] set [Remedial Portal ID]=3 where  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' and convert(varchar(12),[Attendance Date],106)='" + txtDate.Text + "'and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            try
            {
                con.Close();

                SqlCommand cmd3 = new SqlCommand("update [TMU$Student Attendance Line - COL] set [Remedial]=3 where [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'and convert(varchar(13),[Date],106)='" + txtDate.Text + "'", con);
                con.Open();
                cmd3.CommandTimeout = 500000;
                cmd3.ExecuteNonQuery();
                con.Close();
                SqlCommand cmd4 = new SqlCommand("update [TMU$Student Attendance Header -COL] set [Attendance Type]=3 where [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "'and convert(varchar(13),[Date],106)='" + txtDate.Text + "'", con);
                con.Open();
                cmd4.CommandTimeout = 500000;
                cmd4.ExecuteNonQuery();
                con.Close();
            }
            catch
            {

            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' All Class Dismiss !');", true);
            btnDismiss.Enabled = true;
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Time table not created for selected Date');", true);
        //    return;
        //}
       







           
        }
    }


    public void bindDismissClass()
    {
        SqlCommand cmd = new SqlCommand("select [Course Code],[Subject Code]+'-'+[Subject Description] as Subject,[Faculty Name],DATENAME(weekday,[Attendance Date])'Day',format([Attendance Date],'dd/MM/yyyy')as date,[Hour No] as Lecture from [TMU$Time Table Generation - COL] where [Remedial Portal ID]=3 and  [Global Dimension 1 Code]='" + Session["GlobalDimension1Code"].ToString() + "' ", con);//and [Academic Year]='" + ddlAcademicYear.SelectedValue + "'
         con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdDismissClass.DataSource = dt;
        grdDismissClass.DataBind();
        con.Close();
       
    }

    public void enabledisable()
    {
        if (chkAllDay.Checked == true)
        {
            drpCourse.Enabled = false;
            drpSection.Enabled = false;
            drpSemester.Enabled = false;
            drpSubject.Enabled = false;
            ddlGroup.Enabled = false;
            ddlBatch.Enabled = false;
            drpLecture.Enabled = false;
        }
        if (chkAllDay.Checked == false)
        {
            drpCourse.Enabled = true;
            drpSection.Enabled = true;
            drpSemester.Enabled = true;
            drpSubject.Enabled = true;
            ddlGroup.Enabled = true;
            ddlBatch.Enabled = true;
           drpLecture.Enabled = true;
        }
    }

    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSemester();
     

    }

    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSection();
        BindSubjectCode();
        bindGroupList();
       
    }
    protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubjectCode();

    }
    protected void btnDismiss_Click(object sender, EventArgs e)
    {
        btnDismiss.Enabled = false;
        string quer = "", quer1 = "", quer2="";
        if (txtDate.Text != "")
        {
            if (chkAllDay.Checked == true)
            {
                Dismissclass();
            }
            else
            {
                if (drpCourse.SelectedValue != "") //!= "-- Course --")
                {
                    quer = " and [Course Code]='" + drpCourse.SelectedValue + "'";
                    quer1 = " and [Course Code]='" + drpCourse.SelectedValue + "'";//line
                    quer2 = " and [Course Code]='" + drpCourse.SelectedValue + "'"; //header

                }
                if (drpSemester.SelectedValue != "")
                {
                    quer += " and([Semester Code]='" + drpSemester.SelectedValue + "' or Year='" + drpSemester.SelectedValue + "') ";
                    
                    quer1 += " and([Semester]='" + drpSemester.SelectedValue + "' or Year='" + drpSemester.SelectedValue + "') ";//line
                    quer2 += " and([Semester]='" + drpSemester.SelectedValue + "' or Year='" + drpSemester.SelectedValue + "') ";//Header

                }
                if (drpSection.SelectedValue != "")
                {
                    quer += " and [Section Code]='" + drpSection.SelectedValue + "'";
                    quer1 += " and [Section]='" + drpSection.SelectedValue + "'";//line
                    quer2 += " and [Section]='" + drpSection.SelectedValue + "'";//Header
                }

                if (drpSubject.SelectedValue != "")
                {
                    quer += " and[Subject Code]='" + drpSubject.SelectedValue + "'";
                    quer1 += " and[Subject Code]='" + drpSubject.SelectedValue + "'";//line
                    quer2 += " and[Subject Code]='" + drpSubject.SelectedValue + "'";//Header
                }

                if (ddlGroup.SelectedValue != "")
                {
                    quer += " and [Group]='" + ddlGroup.SelectedValue + "'";
                    quer1 += " and[Group Code]='" + ddlGroup.SelectedValue + "'";//line
                    quer2 += " and[Group Code]='" + ddlGroup.SelectedValue + "'";//Header
                }
                if (ddlBatch.SelectedValue != "")
                {
                    quer += " and [Batch]='" + ddlBatch.SelectedValue + "'";
                    quer1 += " and[Batch Code]='" + ddlBatch.SelectedValue + "'";//line
                    quer2 += " and[Batch Code]='" + ddlBatch.SelectedValue + "'";//Header

                }
                if (drpLecture.SelectedValue != "-- Select --" && drpLecture.SelectedValue!= "")
                {
                    quer += " and [Hour No]=" + drpLecture.SelectedItem.Text + "";
                    quer1 += " and[Hour]='" + drpLecture.SelectedValue + "'";//line
                    quer2 += " and[Hour]='" + drpLecture.SelectedValue + "'";//Header

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select Lecture !');", true);
                    return;
                }
            }

            if (quer.Length != 0)
            {
                quer = quer.Remove(0, 4);
                quer1 = quer1.Remove(0, 4);
                quer2 = quer2.Remove(0, 4);
                Dismissclass(quer,quer1,quer2);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select atleast one option !');", true);
                return;
            }
        }
        else
        {
            txtDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Please select Date !');", true);

            return;
            
        }


        //bindDismissClass();


    }
    protected void chkAllDay_CheckedChanged(object sender, EventArgs e)
    {
        enabledisable();
    }

    protected void drpSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindhourno();
    }
}