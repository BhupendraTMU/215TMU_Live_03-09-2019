using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Student_Mentorship : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *,replace(convert(NVARCHAR, [Date of Birth], 106), ' ', '-') as DOB from [TMU$Student Details Mentorship] where No_='" + Session["uid"].ToString() + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                lblRollNo.Text = dt.Rows[0]["Enrollment No_"].ToString();
                lblMentor.Text = dt.Rows[0]["Faculty"].ToString();
                lblName.Text = dt.Rows[0]["Student Name"].ToString();
                lblProgram.Text = dt.Rows[0]["Program"].ToString();
                lblBatch.Text = dt.Rows[0]["Batch"].ToString();
                lblHostlerDayScholar.Text = dt.Rows[0]["Hostler_ Day Scholar"].ToString();
                lblDOB.Text = dt.Rows[0]["DOB"].ToString();
                lblFathersName.Text = dt.Rows[0]["Fathers Name"].ToString();
                lblMothersName.Text = dt.Rows[0]["Mothers Name"].ToString();
                lblPhoneNo.Text = dt.Rows[0]["Phone Number Student"].ToString();
                lblFathersPhoneNo.Text = dt.Rows[0]["Father_Mother Mobile No_"].ToString();
                lblEMailStudent.Text = dt.Rows[0]["E-Mail Address Student"].ToString();
                lblEMailParent.Text = dt.Rows[0]["E-Mail Address Parent"].ToString();
                lblCorrespondanceAddress.Text = dt.Rows[0]["Correspondence Address"].ToString();
                lblHighSchool.Text = dt.Rows[0]["High School _"].ToString();
                lblIntermediate.Text = dt.Rows[0]["Intermediate _"].ToString();
                lblGraduation.Text = dt.Rows[0]["Graduation _"].ToString();
            }
        }
        catch
        {
            Response.Redirect("../Default.aspx");
        }
    }
}