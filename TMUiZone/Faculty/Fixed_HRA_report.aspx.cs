using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Faculty_Fixed_HRA_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMonth.Text = Session["Month"] + "-" + Session["Year"];

            BindReport();
        }
    }

    private void BindReport()
    {
        string month = Convert.ToString(Session["Month"]);
        string year = Convert.ToString(Session["Year"]);

        SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["TMUCON"].ConnectionString);

        SqlCommand cmd = new SqlCommand("HRMSPortal.dbo.proc_GetHRACount", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", Session["uid"].ToString());
        cmd.Parameters.AddWithValue("@Month", month);
        cmd.Parameters.AddWithValue("@Year", year);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        da.Fill(dt);

        gvReport.DataSource = dt;
        gvReport.DataBind();
    }
}