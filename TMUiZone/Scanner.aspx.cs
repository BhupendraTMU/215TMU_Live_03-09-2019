using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Scanner : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string barcode = lblstudentId.Value;

        if(barcode!="")
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
        byte[] imageBytes = null;
        
        SqlCommand cmd = new SqlCommand("[sp_GetStudentDetailsByCode]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        cmd.Parameters.Add("@Code", Code);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            imageBytes = (byte[])dt.Rows[0]["Student Image"];

            if (imageBytes != null )
            {
                // Convert image to base64 string
                string base64String = Convert.ToBase64String(imageBytes);
                stImg.ImageUrl = "data:image/png;base64," + base64String;
            }
            else
            {
                // Optionally set a placeholder image or clear
                stImg.ImageUrl = "";
            }


            txtStudentID.Text = dt.Rows[0]["Enrollment No_"].ToString();

            txthostler.Text = dt.Rows[0]["HOSTLER"].ToString();
            txtReligion.Text = dt.Rows[0]["Religion"].ToString();
            txttransport.Text= dt.Rows[0]["Transport"].ToString(); 

            txtName.Text = dt.Rows[0]["Student Name"].ToString();
            txtEmail.Text = dt.Rows[0]["E-Mail Address"].ToString();
            txtDOB.Text= dt.Rows[0]["Date of Birth"].ToString();
            txtDOB.Text = dt.Rows[0]["Date of Birth"].ToString();
            txtGender.Text = dt.Rows[0]["Gender"].ToString();
            txtCourse.Text = dt.Rows[0]["Course Name"].ToString();
            txtAddress.Text= dt.Rows[0]["Address1"].ToString();
            if(dt.Rows[0]["HOSTLER"].ToString()=="YES")
            {
                txthostler.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                txthostler.BackColor = System.Drawing.Color.Red;
            }
            if (dt.Rows[0]["Transport"].ToString() == "YES")
            {
                txttransport.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                txttransport.BackColor = System.Drawing.Color.Red;
            }
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mainQRCode.Visible = true;
        studentDetails.Visible = false;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtEnrollmentNo_.Text != "")
        {
            mainQRCode.Visible = false;
            studentDetails.Visible = true;
            bindetails(txtEnrollmentNo_.Text);
        }
        else
        {
            mainQRCode.Visible = true;
            studentDetails.Visible = false;
        }
    }
}





