using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class Scanner : System.Web.UI.Page
{
    string CS = ConfigurationManager
        .ConnectionStrings["TMUCON"]
        .ConnectionString;


    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string barcode = lblstudentId.Value.Trim();

        if (!string.IsNullOrEmpty(barcode))
        {
            mainQRCode.Visible = false;
            studentDetails.Visible = true;

            bindetails(barcode);
        }
        else
        {
            mainQRCode.Visible = true;
            studentDetails.Visible = false;
        }
    }


    public void bindetails(string Code)
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(CS))
        {
            using (SqlCommand cmd =
                new SqlCommand("sp_GetStudentDetailsByCode", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 100)
                             .Value = Code;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }


        if (dt.Rows.Count == 0)
        {
            mainQRCode.Visible = true;
            studentDetails.Visible = false;

            return;
        }


        DataRow row = dt.Rows[0];


        // Student Image
        if (row["Student Image"] != DBNull.Value)
        {
            byte[] imageBytes = (byte[])row["Student Image"];

            if (imageBytes.Length > 0)
            {
                string base64String =
                    Convert.ToBase64String(imageBytes);

                stImg.ImageUrl =
                    "data:image/png;base64," + base64String;
            }
        }
        else
        {
            stImg.ImageUrl = "";
        }


        // Student Details

        txtStudentID.Text =
            row["Enrollment No_"].ToString();

        txthostler.Text =
            row["HOSTLER"].ToString();

        txttransport.Text =
            row["Transport"].ToString();

        txtReligion.Text =
            row["Religion"].ToString();

        txtName.Text =
            row["Student Name"].ToString();

        txtEmail.Text =
            row["E-Mail Address"].ToString();

        txtDOB.Text =
            row["Date of Birth"].ToString();

        txtGender.Text =
            row["Gender"].ToString();

        txtCourse.Text =
            row["Course Name"].ToString();

        txtAddress.Text =
            row["Address1"].ToString();


        // Hosteller Color

        if (row["HOSTLER"].ToString()
            .Equals("YES", StringComparison.OrdinalIgnoreCase))
        {
            txthostler.BackColor = Color.Green;
            txthostler.ForeColor = Color.White;
        }
        else
        {
            txthostler.BackColor = Color.Red;
            txthostler.ForeColor = Color.White;
        }


        // Transport Color

        if (row["Transport"].ToString()
            .Equals("YES", StringComparison.OrdinalIgnoreCase))
        {
            txttransport.BackColor = Color.Green;
            txttransport.ForeColor = Color.White;
        }
        else
        {
            txttransport.BackColor = Color.Red;
            txttransport.ForeColor = Color.White;
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mainQRCode.Visible = true;
        studentDetails.Visible = false;

        txtEnrollmentNo_.Text = "";
        lblstudentId.Value = "";
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string enrollmentNo = txtEnrollmentNo_.Text.Trim();

        if (!string.IsNullOrEmpty(enrollmentNo))
        {
            mainQRCode.Visible = false;
            studentDetails.Visible = true;

            bindetails(enrollmentNo);
        }
        else
        {
            mainQRCode.Visible = true;
            studentDetails.Visible = false;
        }
    }
}