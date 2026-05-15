using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Student_ConsolidateMarksheet : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMUCON"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               bindReport();
            }
        }
        catch
        {
        }
    }
    public void bindReport()
    {
        SqlCommand cmd = new SqlCommand("[Pro_ConsolidateMarksheetFinal]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StudentNo", Session["uid"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ReportViewer1.Visible = true;
            lblmsg.Visible = false;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/ConsolidateMarksheetFinal.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet_Result", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "No Data Found.....";
            ReportViewer1.Visible = false;
        }
    }
  

}