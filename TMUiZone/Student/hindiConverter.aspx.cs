using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;  


public partial class Student_hindiConverter : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //protected void btnhindi_Click(object sender, EventArgs e)
    //{
    //    if (con.State == ConnectionState.Closed)
    //        con.Open();

    //    string Sname = ""; string Fname = ""; string Mname = "";
    //    Sname = Request.Form["txtHindi"];
    //    //Fname = Request.Form["FHname"];
    //    //Mname = Request.Form["MHname"];

    //    //
    //    // Create string variables that contain the patterns   
    //    string SCodePattern = @"^[a-zA-Z0-9]*$"; // Email address pattern  
    //    //string FCodePattern = @"^[a-zA-Z0-9]*$";
    //    //string MCodePattern = @"^[a-zA-Z0-9]*$"; // US Phone number pattern   

    //    // Create a bool variable and use the Regex.IsMatch static method which returns true if a specific value matches a specific pattern  
    //    bool isEmailValid = Regex.IsMatch(Sname, SCodePattern);
    //    //bool isZipValid = Regex.IsMatch(Fname, FCodePattern);
    //    //bool isPhoneValid = Regex.IsMatch(Mname, MCodePattern);

    //    // Now you can check the result   
    //    if (isEmailValid)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Student Name only hindi')", true);
    //        return;
    //    }
    //    //if (isZipValid)
    //    //{
    //    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Father Name only hindi')", true);
    //    //    return;
    //    //}

    //    //if (isPhoneValid)
    //    //{
    //    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please enter Mother Name only hindi')", true);
    //    //    return;
    //    //}

    //    SqlCommand cmd = new SqlCommand("update [TMU$Student Data H_E] set [Student Name]=N'" + Sname + "',[Student Father Name]=N'" + Fname + "',[Student Mother Name]=N'" + Mname + "' where [Student No_]='" + Session["uid"].ToString() + "'", con);

    //    cmd.ExecuteNonQuery();
    //    con.Close();
    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "callFeedbackMessage('Success', 'Okay');", true);
    //}
}