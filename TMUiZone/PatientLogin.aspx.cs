using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.IO;


public partial class PatientLogin : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        txt_Username.Text = "";
        txt_password.Text = "";
    }
    protected void btn_Login_Click(object sender, System.EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("GetPatientLogin", con);
        cmd.Parameters.Add("@UserId",txt_Username.Text);
        cmd.Parameters.Add("@Password", txt_password.Text);

        cmd.CommandType = CommandType.StoredProcedure;


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        con.Open();
        da.Fill(dt);
      
        if (dt.Rows.Count > 0)
        {
            Session["PatientReNo"] = dt.Rows[0]["User ID"].ToString();
            Response.Redirect("Patient/PatientReport.aspx");
        }
        else
        {

            lblError.Text = "Incorrect User ID/Password.";
        }
    }
}