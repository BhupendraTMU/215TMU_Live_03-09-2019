using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;


public partial class Dashboard : System.Web.UI.Page
{
    TMUConnection con;
   // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        GetDataSummary();

    }
    public void GetDataSummary()
    {
        con = new TMUConnection();
        
        SqlCommand cmd = new SqlCommand("Sp_DashboardSummary", con.Con);
        cmd.CommandType = CommandType.StoredProcedure;        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.DisConnect();
        grdSummary.DataSource = dt;
        grdSummary.DataBind();
        grdSummary.Visible = true;
    }

    protected void btnviewAttendancedNoOfCourse_Click(object sender, EventArgs e)
    {

    }
}