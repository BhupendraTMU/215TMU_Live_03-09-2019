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

public partial class Faculty_TMVF_Activity : System.Web.UI.Page
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



        con1.Open();


        SqlCommand cmd = new SqlCommand("EDUCOLLEGELIVE-R2.dbo.GetActualPunchData_TMVF", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@Month", ddlMonth.SelectedValue);
        cmd.Parameters.Add("@Year", ddlYear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con2.Open();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grd_ViewAttendance.DataSource = dt;
            grd_ViewAttendance.DataBind();




            con1.Close();
        }


    }
    protected void grd_ViewAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Button btnSave = (Button)e.Row.FindControl("BtnSave");

            Label lblDate = (Label)e.Row.FindControl("lblDate");


            HiddenField HDStatus = (HiddenField)e.Row.FindControl("HDStatus");
           

            DateTime DateEnd = DateTime.Parse(lblDate.Text);
            DateTime DateStart = DateEnd + new TimeSpan(0, 36, 0, 0);
            DateTime currentDateTime = DateTime.Now;


            if (DateTime.Parse(lblDate.Text) <= DateTime.Parse(DateTime.Now.ToString("dd MMMM yyyy")))
            {
                if (lblStatus.Text == "2")
                {

                    lblStatus.Text = "HD";
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.ForeColor = System.Drawing.Color.White;

                    btnSave.Enabled = false;
                }
                if (lblStatus.Text == "3")
                {

                    lblStatus.Text = "WO";

                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    btnSave.Enabled = false;
                }
                if (lblStatus.Text == "A")
                {

                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    btnSave.Enabled = false;
                }

                if ((lblStatus.Text != "A" && lblStatus.Text != "WO" && lblStatus.Text != "HD"))
                {
                    btnSave.Enabled = true;
                }
                if(HDStatus.Value=="Save")
                {
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    btnSave.Enabled = false;
                }
                else
                {
                    if (lblStatus.Text != "A" && lblStatus.Text != "WO" && lblStatus.Text != "HD")
                    {
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                    }
                    if (HDStatus.Value == "Save")
                    {
                        e.Row.BackColor = System.Drawing.Color.Gray;
                        e.Row.ForeColor = System.Drawing.Color.White;
                        btnSave.Enabled = false;
                    }
                }
            }
            else
            {
                if (lblStatus.Text == "2")
                {



                    lblStatus.Text = "HD";


                }
                if (lblStatus.Text == "3")
                {


                    lblStatus.Text = "WO";

                }
                btnSave.Enabled = false;
                if (HDStatus.Value == "Save")
                {
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    btnSave.Enabled = false;
                }
            }


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
    public void bindDrpCourseList()
    {
        SqlCommand cmd = new SqlCommand("EDUCOLLEGELIVE-R2.dbo.proc_GetCourseFromCourseWiseFacultyCollege_Role", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID", Session["uid"].ToString());
        cmd.Parameters.Add("@ID1", Session["GlobalDimension1Code"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con2.Open();
        da.Fill(dt);
        con2.Close();
        drpProgram.DataSource = dt;
        drpProgram.DataTextField = "Details";
        drpProgram.DataValueField = "No_";
        drpProgram.DataBind();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        GridViewRow row = btn.NamingContainer as GridViewRow;

        string Attendance_Date = grd_ViewAttendance.DataKeys[row.RowIndex].Values["Attendance_Date"].ToString();
        txtDate.Text = Attendance_Date;

        hdfShifttime.Value = grd_ViewAttendance.DataKeys[row.RowIndex].Values["ShiftTime"].ToString();
        bindDrpCourseList();

        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal10').modal('show');</script>", false);

    }
    public void bindSubject()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("[EDUCOLLEGELIVE-R2].dbo.sp_getSubjrctCodeTMVF", con2);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());

            cmd.Parameters.Add("@CourseCode", drpProgram.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@Semester", drpSemester.SelectedItem.Value.ToString());



            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            drpCourse.DataSource = dt;
            drpCourse.DataTextField = "Description";
            drpCourse.DataValueField = "Subject Code";
            drpCourse.DataBind();
            drpCourse.Items.Insert(0, "--Course--");
        }
        catch (Exception ex) { }
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
        SqlCommand cmd = new SqlCommand("pro_UpdateVAccountDetail", con2);
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your details update successfully.'); document.location.href='TMVF_Activity.aspx';", true);



    }
    public void hidesubmit()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HRMSPortalConnectionString"].ToString());
        {
            using (SqlCommand cmd = new SqlCommand("select * from  [EDUCOLLEGELIVE-R2].dbo.tbl_VFAcDetails where EmployeeCode='" + Session["uid"].ToString() + "'", con))
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

    protected void drpProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDrpSemesterList();
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal10').modal('show');</script>", false);

    }
    public void bindDrpSemesterList()
    {
        SqlCommand cmd = new SqlCommand("EDUCOLLEGELIVE-R2.dbo.proc_GetSemesterFromCourseWiseFaculty_Role", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 1000000;
        cmd.Parameters.Add("@ID1", Session["uid"].ToString());
        cmd.Parameters.Add("@ID", drpProgram.SelectedValue);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        con2.Open();
        da.Fill(dt);
        con2.Close();
        drpSemester.DataSource = dt;
        drpSemester.DataTextField = "Details";
        drpSemester.DataValueField = "No_";
        drpSemester.DataBind();
    }

    public void bindSectionList()
    {
        SqlCommand cmd = new SqlCommand("EDUCOLLEGELIVE-R2.dbo.proc_GetSectionFromCourseWiseFaculty_Role_TimeTable", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 1000000;
        cmd.Parameters.Add("@FacultyCode", Session["uid"].ToString());
        cmd.Parameters.Add("@CourseCode", drpProgram.SelectedValue);
        cmd.Parameters.Add("@SemesterCode", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Date", txtDate.Text);        
      
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        con2.Open();
        da.Fill(dt);
        con2.Close();
        drpSection.DataSource = dt;
        drpSection.DataTextField = "Details";
        drpSection.DataValueField = "No_";
        drpSection.DataBind();
    }



    protected void drpCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSectionList();
    }

    protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubject();
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal10').modal('show');</script>", false);

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {



        SqlCommand cmd1 = new SqlCommand("[proc_GetdublicateData]", con2);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@Employee_Code", Session["uid"].ToString());
        cmd1.Parameters.Add("@Program", drpProgram.SelectedValue);
        cmd1.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd1.Parameters.Add("@Course", drpCourse.SelectedValue);
        cmd1.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd1.Parameters.Add("@Hour", txtHour.SelectedValue);
        cmd1.Parameters.Add("@Unit", txtUnit.Text);
        cmd1.Parameters.Add("@Lecture_Date", txtDate.Text);
        cmd1.Parameters.Add("@Start_Time", txtStartTime.Text);
        cmd1.Parameters.Add("@End_Time", txtEndTime.Text);

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        con2.Open();
        da.Fill(dt);
        con2.Close();
        if (Convert.ToInt32(dt.Rows[0]["C"]) > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('You have already fill the details.');", true);
            return;
        }


        SqlCommand cmd = new SqlCommand("[TMVF_InsertActivity]", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Employee_Code", Session["uid"].ToString());
        cmd.Parameters.Add("@Shift_Time", hdfShifttime.Value);
        cmd.Parameters.Add("@Program", drpProgram.SelectedValue);
        cmd.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd.Parameters.Add("@Course", drpCourse.SelectedValue);
        cmd.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd.Parameters.Add("@HOUR", txtHour.SelectedValue);
        cmd.Parameters.Add("@Unit", txtUnit.Text);
        cmd.Parameters.Add("@Nameoftopic", txtNameOfTopic.Text);
        cmd.Parameters.Add("@Lecture_Date", txtDate.Text);
        cmd.Parameters.Add("@Start_Time", txtStartTime.Text);
        cmd.Parameters.Add("@End_Time", txtEndTime.Text);
        cmd.Parameters.Add("@StudentStrength", txtPresentStudent.Text);
        cmd.Parameters.Add("@TotalStrength", txtTotalStrength.Text);
        cmd.Parameters.Add("@PrincipalID", Session["hod_ID_Leave1"].ToString());
        cmd.Parameters.Add("@HRID", "TMU05721");


        if (con2.State == ConnectionState.Closed)
            con2.Open();
        cmd.ExecuteNonQuery();
        con2.Close();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Data Save Successfully'); document.location.href='TMVF_Activity.aspx';", true);

    }

    protected void btnSubmitApplication_Click(object sender, EventArgs e)
    {

        int i = 0;

        SqlCommand cmd = new SqlCommand("[EDUCOLLEGELIVE-R2].dbo.TMVF_FormSubmit", con2);
        cmd.CommandType = CommandType.StoredProcedure;
      
        cmd.Parameters.AddWithValue("@Employee", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
      
        con2.Open();
       cmd.ExecuteNonQuery();
        con2.Close();
        i++;


        if (i > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Successfully');document.location.href='TMVF_Activity.aspx';", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Record.. !')", true);
        }



    }

    protected void txtHour_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd1 = new SqlCommand("[proc_StudentCountbyDate]", con2);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add("@Academic_Year", Session["AcademicYear"].ToString());
        cmd1.Parameters.Add("@Employee_Code", Session["uid"].ToString());
        cmd1.Parameters.Add("@Program", drpProgram.SelectedValue);
        cmd1.Parameters.Add("@Section", drpSection.SelectedValue);
        cmd1.Parameters.Add("@Semester", drpSemester.SelectedValue);
        cmd1.Parameters.Add("@Course", drpCourse.SelectedValue);
        cmd1.Parameters.Add("@Hour", txtHour.SelectedValue);
        cmd1.Parameters.Add("@Date", txtDate.Text.ToString());
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        con2.Open();
        da.Fill(dt);
        con2.Close();
        if(dt.Rows.Count>0)
        {
            txtTotalStrength.Text = dt.Rows[0]["Total"].ToString();
            txtPresentStudent.Text = dt.Rows[0]["Present"].ToString();
        }
        else
        {
            txtTotalStrength.Text = "";
            txtPresentStudent.Text = "";
        }


        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal10').modal('show');</script>", false);



    }



    protected void btndetail_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("Pro_getTMVFdatabyEmployeeCode", con2);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserId", Session["uid"].ToString());
        cmd.Parameters.Add("@Month", ddlMonth.SelectedValue.ToString());
        cmd.Parameters.Add("@Year", ddlYear.SelectedValue.ToString());




        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            grdDetail.DataSource = dt;
            grdDetail.DataBind();

        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#confirmModal101').modal('show');</script>", false);


    }
}