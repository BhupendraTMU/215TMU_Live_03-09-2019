using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_AccomdationList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                lblMonth.Text = Session["Month"] + "-" + Session["Year"];

                BindReport();
            }

           
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


        DataSet ds = new DataSet();
        da.Fill(ds);


        gvAccommodation.DataSource = ds.Tables[1];
        gvAccommodation.DataBind();
    }
}