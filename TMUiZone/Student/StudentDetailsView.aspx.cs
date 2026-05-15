using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Student_Default : System.Web.UI.Page
{
    DL.StudentDetailsViewDL sdvDL = new DL.StudentDetailsViewDL();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Local Process
        //Response.Redirect("~/Student/Error.aspx", false);
        //HttpContext.Current.ApplicationInstance.CompleteRequest();
        //Live
        Response.Redirect("Error.aspx", false);
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
    protected void txtStudentNo_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = sdvDL.GetStudentDetails(txtStudentNo.Text);

        if (dt.Rows.Count > 0)
        {
            lblName.Text = dt.Rows[0]["Student Name"].ToString();
            lblCourse.Text = dt.Rows[0]["Course Code"].ToString();
            lblSection.Text = dt.Rows[0]["Section"].ToString();
            lblSemester.Text = dt.Rows[0]["Semester"].ToString();
            lblAcademicYear.Text = dt.Rows[0]["Academic Year"].ToString();
            lblFathersName.Text = dt.Rows[0]["Fathers Name"].ToString();
            lblMothersName.Text = dt.Rows[0]["Mothers Name"].ToString();
            lblDOB.Text = dt.Rows[0]["Date of Birth"].ToString();
            lblCategory.Text = dt.Rows[0]["Quota"].ToString();
            lblMobileNumber.Text = dt.Rows[0]["Mobile Number"].ToString();

        }
        else
        {
            lblCourse.Text = "";
            lblName.Text = "";
            lblSemester.Text = "";
            lblSection.Text = "";
            lblAcademicYear.Text = "";
            lblFathersName.Text = "";
            lblMothersName.Text = "";
            lblDOB.Text = "";
            lblCategory.Text = "";
            lblMobileNumber.Text = "";
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select No_ from [TMU$Student - COLLEGE] where  No_ like '" + prefixText.ToUpper() + "%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["No_"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
}