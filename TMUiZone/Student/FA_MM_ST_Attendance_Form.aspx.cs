using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_FA_MM_Attendance_Form : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Date1.Text = DateTime.Now.ToString("yyyy-MM-dd");
             
                BindDate();
                if(Session["College"].ToString()=="TMNS" || Session["College"].ToString() == "TMSN")
                {
                    pContent.Visible = false;
                    p1.Visible = true;
                    subjectNormal.Visible = false;
                    subjectNursing.Visible = true;
                    Nornalres.Visible = false;
                    NornalNur.Visible = true;
                }
                else
                {
                    pContent.Visible = true;
                    p1.Visible = false;
                    subjectNormal.Visible = true;
                    subjectNursing.Visible = false;
                    Nornalres.Visible = true;
                    NornalNur.Visible = false;
                }




            }
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    public string GetAcademicYearNo()
    {
        string noValue = null;

        try
        {
            using (SqlCommand cmd = new SqlCommand("proc_GetAcademicYear", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        // return the first "No_" from the result
                        noValue = dt.Rows[0]["No_"].ToString();
                    }
                }
            }
        }
        catch
        {
            // optionally log the error
            noValue = null;
        }

        return noValue;
    }


    public void bindGrid()
    {

        string val = GetAcademicYearNo().ToString();
        string val1 = Session["Semester"].ToString();
        SqlCommand cmd = new SqlCommand("StudentAttendanceNew '" + Session["uid"].ToString() + "','" + val + "','" + val1 + "','--Select--'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            grdAttendanceReport.DataSource = dt;
            grdAttendanceReport.DataBind();
        }
        else
        {
            grdAttendanceReport.DataSource = "";
            grdAttendanceReport.DataBind();
        }

    }
    public void BindDate()
    {

        try
        {
            SqlCommand cmd = new SqlCommand("HRMSPortal.dbo.SP_Get_Undertaking", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Enrollment", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Sem", Session["Semester"].ToString());
            cmd.Parameters.AddWithValue("@Year", Session["Year"].ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtCL = new DataTable();
            da.Fill(dtCL);
            con.Close();
            if (dtCL.Rows.Count > 0)
            {
                studentName.Text = dtCL.Rows[0]["Student Name"].ToString();
                studentName1.InnerText = dtCL.Rows[0]["Student Name"].ToString();
                fatherName1.InnerText = dtCL.Rows[0]["Fathers Name"].ToString();
                program.Text = dtCL.Rows[0]["program"].ToString();
                branch.Text = dtCL.Rows[0]["Global Dimension 1 Code"].ToString();              
                semester.Text = dtCL.Rows[0]["Semester"].ToString();
                CollegeDepartment.Text = dtCL.Rows[0]["College1"].ToString();
                studentMobile.Text = dtCL.Rows[0]["Mobile Number"].ToString();
                studentEmail.Text = dtCL.Rows[0]["E-Mail Address"].ToString();
                fatherEmail.Text = dtCL.Rows[0]["FatherEmail"].ToString();
                fatherMobile.Text = dtCL.Rows[0]["FatherMobileNo"].ToString();
                if(dtCL.Rows[0]["complete"].ToString()=="1")
                {
                    CheckBox1.Checked = true;
                    CheckBox1.Enabled = false;
                    btnPrint.Visible = true;
                    grdAttendanceReport.DataSource = dtCL;
                    grdAttendanceReport.DataBind();
                    
                }
                else
                {
                    CheckBox1.Checked = false;
                    CheckBox1.Enabled = true;
                    btnPrint.Visible = false;
                    bindGrid();
                }


              
            }


        }
        catch (Exception ex)
        {

            throw new Exception("Error fetching pay slip requests: " + ex.Message, ex);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }




    protected void SubmitButton_Click(object sender, EventArgs e)
    {

        try
        {


            if (!IsValidGmail(fatherEmail.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter valid E-mail address')", true);
                return;
            }


            DataTable studentTable = new DataTable();

            // Define columns based on GridView
            studentTable.Columns.Add("S_N", typeof(string));
            studentTable.Columns.Add("SubmitDate", typeof(string));
            studentTable.Columns.Add("CollegeDepartment", typeof(string));
            studentTable.Columns.Add("Studentname", typeof(string));
            studentTable.Columns.Add("fathername", typeof(string));
            studentTable.Columns.Add("coursename", typeof(string));
            studentTable.Columns.Add("coursecode", typeof(string));
            studentTable.Columns.Add("percentage", typeof(string));
            studentTable.Columns.Add("program", typeof(string));
            studentTable.Columns.Add("branch", typeof(string));
            studentTable.Columns.Add("sem_year", typeof(string));
            studentTable.Columns.Add("Studentmobile", typeof(string));
            studentTable.Columns.Add("studentemail", typeof(string));
            studentTable.Columns.Add("fathermobile", typeof(string));
            studentTable.Columns.Add("fatherEmail", typeof(string));


            foreach (GridViewRow row in grdAttendanceReport.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DataRow dr = studentTable.NewRow();
                    dr["S_N"] = row.DataItemIndex + 1;
                    dr["SubmitDate"] = Date1.Text;
                    dr["CollegeDepartment"] = CollegeDepartment.Text;
                    dr["Studentname"] = studentName1.InnerText;
                    dr["fathername"] = fatherName1.InnerText;
                    dr["program"] = program.Text;
                    dr["branch"] = branch.Text;
                    dr["sem_year"] = semester.Text;
                    dr["Studentmobile"] = studentMobile.Text;
                    dr["studentemail"] = studentEmail.Text;
                    dr["fathermobile"] = fatherMobile.Text;
                    dr["fatherEmail"] = fatherEmail.Text;
                    Label lblCourse = (Label)row.FindControl("lblCourse");
                    dr["coursename"] = lblCourse != null ? lblCourse.Text : string.Empty;
                    Label lblCourseCode = (Label)row.FindControl("lblCourseCode");
                    dr["coursecode"] = lblCourseCode != null ? lblCourseCode.Text : string.Empty;
                    Label lblPer = (Label)row.FindControl("lblPer");
                    dr["percentage"] = lblPer != null ? lblPer.Text : string.Empty;
                    studentTable.Rows.Add(dr);
                }
            }


            SqlCommand cmd = new SqlCommand("HRMSPortal.dbo.SP_insert_Undertaking", con);//Insert_proc_MarksEntryHeader1
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Enrollment", Session["uid"].ToString());
            cmd.Parameters.AddWithValue("@Sem", semester.Text);
            cmd.Parameters.AddWithValue("@dtfinal", studentTable);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Attendance Undertaking is submit Successfully'); document.location.href='FA_MM_ST_Attendance_Form.aspx';", true);





        }
        catch (Exception ex)
        {

        }
    }
    static bool IsValidGmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Regex for valid Gmail addresses (case-insensitive)
        string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";

        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            SubmitButton.Visible = true;
        }
        else
        {
            SubmitButton.Visible = false;
        }
    }
}