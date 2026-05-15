using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class Profiles : System.Web.UI.Page
{
    TMUConnection con;
    string tbleName = "[Ashoka University$Student - COLLEGE]";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con = new TMUConnection();
            if (!IsPostBack)
            {
                showProfile();
            }
        }
        catch (Exception)
        {
            Response.Redirect("Index.aspx");
        }
    }
    public void showProfile()
    {
        SqlDataReader dr = con.ShowProfile(tbleName, Session["uid"].ToString());
        dr.Read();
        if (dr.HasRows)
        {
            lblName.Text = dr["Student Name"].ToString();
            txtFatherName.Text = dr["Fathers Name"].ToString();
            txtMotherName.Text = dr["Mothers Name"].ToString();
            lblDOB.Text = dr["Date of Birth"].ToString();
            DateTime dt = Convert.ToDateTime(lblDOB.Text);
            lblDOB.Text = dt.ToString("yyyy-MM-dd");
            txtAddress.Text = dr["Address1"].ToString();
            txtAddress2.Text = dr["Address2"].ToString();
            txtCity.Text = dr["City"].ToString();
            txtCountry.Text = dr["Country Code"].ToString();
            txtPin.Text = dr["Post Code"].ToString();
            txtEmail.Text = dr["E-Mail Address"].ToString();
            txtPhone.Text = dr["Mobile Number"].ToString();
            lblCourse.Text = dr["Course Code"].ToString();
            lblAcademicYRS.Text = dr["Academic Year"].ToString();
            string gender = dr["Gender"].ToString();
            if (gender == "1")
            { 
           rdmale.Checked=true;
           rdFemale.Checked = false;
            }
            if (gender == "2")
            {
                rdFemale.Checked = true;
                rdmale.Checked = false;
            }
            if (gender == "0")
            {
                rdFemale.Checked = false;
                rdmale.Checked = false;
            }
        }
        dr.Close();
        con.DisConnect();
    
    }
   

    protected void btnSave_Click(object sender, EventArgs e)
    {
      string   gender="0";
      if (rdmale.Checked == true)
      {
          gender = "1";
      }
      if (rdFemale.Checked == true)
      {
          gender = "2";
      }
      con.Update_Profile(tbleName, Session["uid"].ToString(), txtFatherName.Text.Trim(), txtMotherName.Text.Trim(), txtAddress.Text.Trim(), txtAddress2.Text, txtCountry.Text, txtPin.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), gender,txtCity.Text);
      ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Saved Successfully...........');", true);

    }
}