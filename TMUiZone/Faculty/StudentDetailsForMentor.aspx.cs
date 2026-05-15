using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_StudentDetailsForMentor : System.Web.UI.Page
{
    static string StudentNo;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
       // StudentNo = Session["MStudentId"].ToString();
        try
        {
            StudentNo = Request["search"];
            BindData();
            BINDimage();
            BindInteractionDetails();
            BindAttendanceReport();
            BindAcademicPerformance();
            
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    public void BindData()
    {
        SqlCommand cmd = new SqlCommand("proc_getStudentDetaileFromMentorship", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StudentNo", StudentNo);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtCorrespondenceAddress.Text = dt.Rows[0]["Correspondence Address"].ToString();
            txtCourse.Text = dt.Rows[0]["Program"].ToString();
            txtDOB.Text = dt.Rows[0]["DOB"].ToString();
            txtEmailIDParents.Text = dt.Rows[0]["E-Mail Address Parent"].ToString();
            txtEmailIDStudent.Text = dt.Rows[0]["E-Mail Address Student"].ToString();
            txtFatherName.Text = dt.Rows[0]["Fathers Name"].ToString();
            txtGradutationPercentage.Text = dt.Rows[0]["Graduation _"].ToString();
            txtHighSchoolPercentage.Text = dt.Rows[0]["High School _"].ToString();
            txtHostlerScholar.Text = dt.Rows[0]["Hostler_ Day Scholar"].ToString();
            txtIntermediatePercentage.Text = dt.Rows[0]["Intermediate _"].ToString();
            txtMotherName.Text = dt.Rows[0]["Mothers Name"].ToString();
            txtName.Text = dt.Rows[0]["Student Name"].ToString();
            txtPhoneNoParents.Text = dt.Rows[0]["Father_Mother Mobile No_"].ToString();
            txtPhoneNoStudent.Text = dt.Rows[0]["Phone Number Student"].ToString();
            lblMentor.Text = dt.Rows[0]["Faculty"].ToString();
            lblRollNo.Text = dt.Rows[0]["Enrollment No_"].ToString();
           
        }
    }

    public void BindInteractionDetails()
    {
        SqlCommand cmd = new SqlCommand("proc_InteractionDetailsWithMentor", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StudentNo", StudentNo);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdInteractionDetails.DataSource = dt;
        grdInteractionDetails.DataBind();
    }
    protected void grdInteractionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    public void BindAttendanceReport()
    {
        SqlCommand cmd = new SqlCommand("Proc_GetStudAttendanceRecordForMentor1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StudentCode", StudentNo);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        grdAttendanceReport.DataSource = dt;
        grdAttendanceReport.DataBind();
    }
    protected void grdAttendanceReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    public void BindAcademicPerformance()
    {
        DataTable dt = new DataTable();
        grdAcademicPerformance.DataSource = dt;
        grdAcademicPerformance.DataBind();
    }
    public void BINDimage()
    {
        byte[] bytes = GetData("select [Student Image] from [TMU$Student Details Mentorship] where No_='" + StudentNo + "'").Rows[0]["Student Image"].ToString() == "" ? null : (byte[])GetData("select [Student Image] from [TMU$Student Details Mentorship] where No_='" + StudentNo + "'").Rows[0]["Student Image"];
        if (bytes != null)
        {
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgStudent.ImageUrl = "data:image/png;base64," + base64String;
        }
    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
}