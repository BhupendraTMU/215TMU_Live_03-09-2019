using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

public partial class Student_Undertaking_NEP_Option : System.Web.UI.Page
{
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["CourseCode"].ToString() == "BCA-001" || Session["CourseCode"].ToString() == "BCA-002" || Session["CourseCode"].ToString() == "BCA-004" || Session["CourseCode"].ToString() == "BSC-001" || Session["CourseCode"].ToString() == "BBA-001" || Session["CourseCode"].ToString() == "BCOM-001" || Session["CourseCode"].ToString() == "BPES-001") && Session["Semester"].ToString()=="VI")
            {
                LoadStudentData();
            }
            else
            {
                Response.Redirect("Error.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
    }
    private void LoadStudentData()
    {



        SqlCommand cmd = new SqlCommand("sp_GetStudentDetailsByCode", con1);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Code", Session["enroll"].ToString());

        con1.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            lblEnroll.Text = dr["Enrollment No_"].ToString();
            lblName.Text = dr["Student Name"].ToString();
            lblFather.Text = dr["Fathers Name"].ToString();
            lblProgramCode.Text = dr["Course Code"].ToString();
            lblProgramName.Text = dr["Course Name"].ToString();
            lblCollege.Text = dr["Global Dimension 1 Code"].ToString();
            lblSession.Text = dr["Academic Year"].ToString();

            lblDate.Text = Convert.ToDateTime(dr["Date"]).ToString("dd-MM-yyyy");
            lblPlace.Text = dr["Place"].ToString(); 
            lblSign.Text = "";
            lblName2.Text = dr["Student Name"].ToString(); 
            lblMobile.Text = dr["Mobile Number"].ToString(); 
            lblEmail.Text = dr["E-Mail Address"].ToString();

            lblAuth.Text = "";
            lblHead.Text = "";
            lblDesg.Text = "";
            lblVerifyDate.Text ="";

            // Option selection
            string opt = dr["OptionType"].ToString();

            if (opt == "A")
            {
                chkA.Checked = true;
                OPTB.Visible = false;
                btnPrint.Visible = true;
                btnSave.Visible = false;
            }
            else if (opt == "B")
            {
                chkB.Checked = true;
                OPTA.Visible = false;
                btnPrint.Visible = true;
                btnSave.Visible = false;
            }
            else
            {
                OPTB.Visible = true;
                OPTA.Visible = true;
                btnPrint.Visible = false;
                btnSave.Visible = true;
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        string option = "";

        if (chkA.Checked)
        {
            option = "A";
        }
        else if (chkB.Checked)
        {
            option = "B";
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Atleast One Option')", true);
            return;
        }

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("InsertStudentNEPOption", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EnrollmentNo", lblEnroll.Text);
            cmd.Parameters.AddWithValue("@StudentName", lblName.Text);
            cmd.Parameters.AddWithValue("@AdmissionSession", Session["AcademicYear"].ToString());
            cmd.Parameters.AddWithValue("@OptionSelected", option);
           

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
       "alert('Form Submitted Successfully'); window.location='Undertaking_NEP_Option.aspx';", true);

    }
}