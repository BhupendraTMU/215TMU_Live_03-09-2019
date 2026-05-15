using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.html;


public partial class Faculty_StudentCardGeneration : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    public void GetStudentDetails()
    {
        lblMSG.Text="";
        SqlCommand cmd = new SqlCommand("SP_GetStudentIcard", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Enrollmentno", txtEnrollmentNo.Text);        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblCollege.Text = dt.Rows[0]["CollegeName"].ToString();
            lblEnrollmentNo.Text = dt.Rows[0]["EnrollmentNo_"].ToString();
            lblBarcodeNo.Text = dt.Rows[0]["EnrollmentNo_"].ToString();
            lblName.Text = dt.Rows[0]["Name"].ToString();
            lblFatherName.Text = dt.Rows[0]["Father's Name"].ToString();
            lblDOB.Text = dt.Rows[0]["D.O.B"].ToString();
            lblProgramme.Text = dt.Rows[0]["Programme"].ToString();
            lblGuardian.Text = dt.Rows[0]["Guardian"].ToString();
            lblCollegePhoneNo.Text = dt.Rows[0]["CollegePhoneNo"].ToString();
            lblBloodGroup.Text = dt.Rows[0]["Blood Group"].ToString();
            lblAddress1.Text = dt.Rows[0]["Residance"].ToString();
            lblAdmissionSession.Text = dt.Rows[0]["Admission Session"].ToString();
            lblValidUpto.Text = dt.Rows[0]["Admission Session"].ToString();
            String Proc = "proc_GetImageByEnrollmentNo";
            imgStudent.ImageUrl = "StudentImage.aspx?ImageID=" + txtEnrollmentNo.Text + "&Proc=" + Proc + "";
            pnlSalarySlip.Visible = true;
            btnPrint.Visible = true;
            btnsendEmail.Visible = false;
        }
        else
        {
            lblMSG.Text = "* Enrollment No. not Found !";
            pnlSalarySlip.Visible = false;
            btnPrint.Visible = false;
            btnsendEmail.Visible = false;

        }
    }
    private void GenerateBacode(string _data, string _filename)
    {
        //Linear barcode = new Linear();
        //barcode.Type = BarcodeType.CODE11;
        //barcode.Data = _data;
        //barcode.drawBarcode(_filename);
    }
    protected void btnsendEmail_Click(object sender, EventArgs e)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        GetStudentDetails();
       
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}