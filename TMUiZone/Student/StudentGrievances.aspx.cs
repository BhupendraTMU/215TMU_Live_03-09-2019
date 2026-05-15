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

public partial class Student_StudentGrievances : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            BindStudentDetails();
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        byte[] bytes=null;
        
        try
        {
            if (chkboxAttechment.Checked == true)
            {
                if (UploadAttecment.HasFile)
                {

                    string filename = Path.GetFileName(UploadAttecment.PostedFile.FileName);
                    string contentType = UploadAttecment.PostedFile.ContentType;
                    using (Stream fs = UploadAttecment.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            bytes = br.ReadBytes((Int32)fs.Length);
                        }
                    }
                }
            }
            SqlCommand cmd = new SqlCommand("proc_insertIntoStudentGrievance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudentNo", lblStudentNo.Value);
            cmd.Parameters.Add("@StudentName", txtName.Text);
            cmd.Parameters.Add("@EnrollmentNo", lblRollNo.Text);
            cmd.Parameters.Add("@StudentAppeal1", txtStudentAppeal1.Text);
            cmd.Parameters.Add("@StudentAppeal2", txtStudentAppeal2.Text);
            cmd.Parameters.Add("@GroundForAppeal", txtGroundForAppeal.Text);
            cmd.Parameters.Add("@Attachment", bytes);
            cmd.Parameters.Add("@CollegeCode", Session["College"].ToString());
            cmd.Parameters.Add("@CourseCode", Session["CourseCode"].ToString());
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Clear();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Save Successfully');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Error', '" + ex + "');", true);
            //Response.Redirect("~/Default.aspx");
        }
    }
    public void BindStudentDetails()
    {
        SqlCommand cmd = new SqlCommand("proc_fatchStudentDetailsView", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ID",Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblRollNo.Text = dt.Rows[0]["Enrollment No_"].ToString();
            lblStudentNo.Value = dt.Rows[0]["No_"].ToString();
            txtName.Text = dt.Rows[0]["Student Name"].ToString();
            txtCourse.Text = dt.Rows[0]["Course Code"].ToString();
            txtDOB.Text = dt.Rows[0]["DOB"].ToString();
            txtEmailAddress.Text = dt.Rows[0]["E-Mail Address"].ToString();
            txtMobileNo.Text = dt.Rows[0]["Mobile Number"].ToString();
            if (dt.Rows[0]["Type of Course"].ToString() == "1")            
                txtSemester.Text = dt.Rows[0]["Semester"].ToString();           
            else
                txtSemester.Text = dt.Rows[0]["Year"].ToString();
        }
    }
    public void Clear()
    {
        txtStudentAppeal1.Text = "";
        txtStudentAppeal2.Text = "";
        txtGroundForAppeal.Text = "";
        chkboxAttechment.Checked = false;
    }
}