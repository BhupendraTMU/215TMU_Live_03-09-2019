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

public partial class PatientReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetPatientReport", con);
                cmd.Parameters.Add("@UserId", Session["PatientReNo"].ToString());


                cmd.CommandType = CommandType.StoredProcedure;


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    msg.Text = "Dear " + dt.Rows[0]["PatientName"].ToString() + ", Please download your report here:-";

                    PatientMain.DataSource = dt;
                    PatientMain.DataBind();
                }
            }
    
        catch(Exception ex)
        {
            Response.Redirect("~/PatientLogin.aspx");
        }


    }


    }
    protected void lnkPrint_Click(object sender, System.EventArgs e)
    {

        LinkButton btn = sender as LinkButton;
        GridViewRow row = btn.NamingContainer as GridViewRow;
        string pk = PatientMain.DataKeys[row.RowIndex].Values[0].ToString();
        string Service = PatientMain.DataKeys[row.RowIndex].Values[1].ToString();
        Session["Service"] = Service;
        Response.Redirect("ReportCO.aspx?No=" + pk + "&Service=" + Service + "");
       


    }
}