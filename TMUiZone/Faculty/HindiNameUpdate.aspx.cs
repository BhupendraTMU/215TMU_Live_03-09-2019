using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Text;

public partial class Faculty_HindiNameUpdate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected string StudentHindi { get; set; }
    protected string FatherHindi { get; set; }
    protected string MotherHindi { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {


    }


    protected void BtnShow_Click(object sender, EventArgs e)
    {
        getgriddata();
    }
   
    public void getgriddata()
    {

        try
        {
            SqlCommand cmd = new SqlCommand("Sp_getHindiEnlishDataForUpdation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InputText", txtEnrollNo.Text.TrimEnd().TrimStart().ToUpper());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            LblEnrollment.Text = dt.Rows[0]["Enrollment No"].ToString();
            HfStudentNo.Value = dt.Rows[0]["No_"].ToString();
            lblStudentName.Text = dt.Rows[0]["E Name"].ToString();
            lblStudentNo.Text = dt.Rows[0]["No_"].ToString();
            lblFather.Text = dt.Rows[0]["E Father"].ToString();
            lblMother.Text = dt.Rows[0]["E Mother"].ToString();
            Studentname.Value = dt.Rows[0]["H Name"].ToString();
            fathername.Value = dt.Rows[0]["H Father"].ToString();
            mothername.Value = dt.Rows[0]["H Mother"].ToString();
            //txtSH.Text = dt.Rows[0]["H Name"].ToString();
            this.StudentHindi = dt.Rows[0]["H Name"].ToString();
            this.FatherHindi = dt.Rows[0]["H Father"].ToString();
            this.MotherHindi = dt.Rows[0]["H Mother"].ToString();
        }
        catch (Exception) { }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (txtEnrollNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Enrollment or Student No For Correction in Hindi Name.')", true);
            return;
        }

        string Sname = ""; string Fname = ""; string Mname = "";
        Sname = Request.Form["SHname"];
        Fname = Request.Form["FHname"];
        Mname = Request.Form["MHname"];

        if (Sname == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Student Name')", true);
            return;
        }
        else
        {
            var Sresult = Sname.Substring(Sname.Length - 1);
            string SCodePattern = @"^[a-zA-Z0-9._^%$#!~@,-{}+=]*$";
            bool isEmailValid = Regex.IsMatch(Sresult, SCodePattern);
            if (isEmailValid)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Student Name only in hindi')", true);
                return;
            }
        }
        if (Fname == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Father Name')", true);
            return;
        }
        else
        {
            var Fresult = Fname.Substring(Fname.Length - 1);
            string FCodePattern = @"^[a-zA-Z0-9._^%$#!~@,-{}+=]*$";
            bool isZipValid = Regex.IsMatch(Fresult, FCodePattern);
            if (isZipValid)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Father Name only in hindi')", true);
                return;
            }
        }
        if (Mname == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Mother Name')", true);
            return;
        }
        else
        {
            var Mresult = Mname.Substring(Mname.Length - 1);
            string MCodePattern = @"^[a-zA-Z0-9._^%$#!~@,-{}+=]*$";
            bool isPhoneValid = Regex.IsMatch(Mresult, MCodePattern);
            if (isPhoneValid)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Mother Name only hindi')", true);
                return;
            }
        }   





        if (con.State == ConnectionState.Closed)
            con.Open();

        SqlCommand cmd = new SqlCommand("update [TMU$Student Data H_E] set [Student Name]=N'" + Sname + "',[Student Father Name]=N'" + Fname + "',[Student Mother Name]=N'" + Mname + "' where ([Student No_]='" + HfStudentNo.Value + "' or [Student No_]=(select [Student No_] from [TMU$Student - COLLEGE] where [Enrollment No_]='" + HfStudentNo.Value + "') )", con);

        cmd.ExecuteNonQuery();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Updated Successfully');", true);
        con.Close();
        getgriddata();
    }
}